Imports System.Drawing
Imports Microsoft.Win32
Imports System.Drawing.Printing
Imports System.IO
Imports PegasusImaging.WinForms.TwainPro5
Imports gloEMRGeneralLibrary
Imports gloSettings
Imports System.Data.SqlClient
Imports System.Threading

Public Class frmSettings_New
    Public oMainFormRef As Form

    Structure ARGBColor
        Dim Red As Byte
        Dim Green As Byte
        Dim Blue As Byte
        Dim Alpha As Byte
        Dim Value As Integer
    End Structure

    Public Enum ClinicEnvironment
        ENV_1 = 1
        ENV_2 = 2
        ENV_3 = 3
        ENV_4 = 4
        ENV_5 = 5
        ENV_6 = 6
    End Enum

    Public Enum ItemClick
        DX = 1
        CPT = 2
        Orders = 3
    End Enum
    Dim objClinicEnv As New Collection

    Private WithEvents dgCustomGrid As CustomTask
    Dim twainDevice As PegasusImaging.WinForms.TwainPro5.TwainDevice
    'For Remote Scan
    Private dtScanner As DataTable = Nothing
    Private dtScanMode As DataTable = Nothing
    Private dtScanDepth As DataTable = Nothing
    Private dtResolution As DataTable = Nothing
    Private dtBrightness As DataTable = Nothing
    Private dtContrast As DataTable = Nothing
    Private dtScanSide As DataTable = Nothing
    Private dtSupportedSizes As DataTable = Nothing


    Public _ErrorMessage As String = ""

    Dim oRemoteScanCommon As gloEDocumentV3.Common.RemoteScanCommon = New gloEDocumentV3.Common.RemoteScanCommon()

    'Dim sSetting_DMSScanner As String = "DMSScanner"
    'Dim sSetting_DMSScanBrightness As String = "DMSBrightness"
    'Dim sSetting_DMSResolution As String = "DMSResolution"
    'Dim sSetting_DMSScanSide As String = "DMSScanSide"
    'Dim sSetting_DMSScanMode As String = "DMSScanMode"
    ' Dim sSetting_DMSScanBrightness As String = "DMSBrightness"
    'Dim sSetting_DMSScanContrast As String = "DMSContrast"
    'Dim sSetting_DMSShowScanner As String = "DMSShowScanner"
    ''Sudhir 20090203
    'Private sSetting_CardWidth As String = "CardWidth"
    Dim myScanLayout As RectangleF
    'Private sSetting_CardLength As String = "CardLength"
    Dim objScannerSettings As gloEDocumentV3.Forms.ScannerSettings
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

    ''Added Rahul For Send Task on 20101026
    Dim COL_ChkForm As Integer = 0
    Dim COL_FormNm As Integer = 1
    Dim COL_Users As Integer = 2
    Dim COL_BrowseBtn As Integer = 3
    Dim COL_ChkViewForm As Integer = 4
    Dim COL_Hidden As Integer = 5
    Dim strUserList As String = "" ''UserName
    Dim strUserID As String = "" ''UserId
    Dim nSelectedRow As Integer = 0
    Dim COLUMN_COUNT As Integer = 6
    ''End

    Private Col_Check As Integer = 2
    Private Col_Name As Integer = 0
    Private Col_Dosage As Integer = 1
    Private Col_Count As Integer = 3
    Private nonNumberEntered As Boolean = False
    Dim eItemSelected As ItemClick
    Dim myBoldFont As Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
    Dim myNormalFont As Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
    Private dsSettings As DataSet
    Private dtProvider As DataTable
    Dim bChkPegasusValues As Boolean = False

    Dim bIsScannerConnected As Boolean = True

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'TwainPro1.Licensing.UnlockRuntime(1808984205, 249325542, 1216513884, 14413)
        objScannerSettings = New gloEDocumentV3.Forms.ScannerSettings()
        myScanLayout = New RectangleF(1.0F, 1.0F, 8.5F, 14.0F)
    End Sub

    Private Sub DisposingTwain()
        If (IsNothing(twainDevice) = False) Then
            twainDevice.Dispose()
            twainDevice = Nothing
        End If
    End Sub

    Private Sub InitPagasusTwainDevice()
        If (IsNothing(twainDevice) = True) Then
            TwainPro1.Licensing.UnlockRuntime(1808984205, 249325542, 1216513884, 14413)
            twainDevice = New TwainDevice(TwainPro1)
        End If
    End Sub

    Friend Sub ResizeControl(ByRef ctl As Control)

        '---------------------------- GET SCALES -------------------------
        Dim DesignScreenWidth As Integer = 1920
        Dim DesignScreenHeight As Integer = 1080
        Dim CurrentScreenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim CurrentScreenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

        'Ratios
        Dim ratioX As Double = CurrentScreenWidth / DesignScreenWidth        ' e.g.  800/1920
        Dim ratioY As Double = CurrentScreenHeight / DesignScreenHeight

        With ctl
            Dim height As Integer = Math.Min(.Height, CurrentScreenHeight)
            Dim width As Integer = Math.Min(.Width, CurrentScreenWidth)
            'Position
            If (.GetType.GetProperty("Top").CanRead) Then .Top = CInt(.Top * ratioY)
            If (.GetType.GetProperty("Left").CanRead) Then .Left = CInt(.Left * ratioX)
            'Size
            If (.GetType.GetProperty("Width").CanRead) Then .Width = CInt(width * ratioX)
            If (.GetType.GetProperty("Height").CanRead) Then .Height = CInt(height * ratioY)

        End With

        '---------------------- RESIZE SUB CONTROLS -------------------------------
        For Each subCtl As Control In ctl.Controls
            ResizeControl(subCtl)
        Next subCtl
    End Sub

    Private Sub frmSettings_New_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        objClinicEnv.Clear()

        RemoveControl()

        DisposingTwain()

        If (IsNothing(objScannerSettings) = False) Then
            objScannerSettings.Dispose()
            objScannerSettings = Nothing
        End If

        If IsNothing(dtProvider) = False Then
            dtProvider.Dispose()
            dtProvider = Nothing
        End If

        If dtScanner IsNot Nothing Then
            dtScanner.Dispose()
            dtScanner = Nothing
        End If
        If dtScanMode IsNot Nothing Then
            dtScanMode.Dispose()
            dtScanMode = Nothing
        End If
        If dtScanDepth IsNot Nothing Then
            dtScanDepth.Dispose()
            dtScanDepth = Nothing
        End If
        If dtResolution IsNot Nothing Then
            dtResolution.Dispose()
            dtResolution = Nothing
        End If
        If dtBrightness IsNot Nothing Then
            dtBrightness.Dispose()
            dtBrightness = Nothing
        End If
        If dtContrast IsNot Nothing Then
            dtContrast.Dispose()
            dtContrast = Nothing
        End If
        If dtScanSide IsNot Nothing Then
            dtScanSide.Dispose()
            dtScanSide = Nothing
        End If
        If dtSupportedSizes IsNot Nothing Then
            dtSupportedSizes.Dispose()
            dtSupportedSizes = Nothing
        End If
        'myBoldFont.Dispose()

        'myNormalFont.Dispose()
    End Sub

    Private Sub frmSettings_New_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load


        UpdateVoiceLog("Client Settings Started")

        Me.DoubleBuffered = True

        Dim oThreadFill_Printers As Thread
        Dim oThreadFill_FillDMSSettings As Thread

        oThreadFill_Printers = New Thread(New ThreadStart(AddressOf Fill_Printers))
        oThreadFill_Printers.Start()

        oThreadFill_FillDMSSettings = New Thread(New ThreadStart(AddressOf FillDMSSettings))
        oThreadFill_FillDMSSettings.Start()

        ' Dim conn As New SqlClient.SqlConnection(GetConnectionString())
        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)

        Try

            dsSettings = ogloSettings.GetEMRClientSettings(gnLoginID, 8)

            dsSettings.Tables(0).TableName = "GetClinicEnvironment"
            dsSettings.Tables(1).TableName = "PatientSearch"
            dsSettings.Tables(2).TableName = "PatientSearchUser"
            dsSettings.Tables(3).TableName = "PatientColumns"
            dsSettings.Tables(4).TableName = "PatientSearchColumn"
            dsSettings.Tables(5).TableName = "PatientDemographics"
            dsSettings.Tables(6).TableName = "Providers"
            dsSettings.Tables(7).TableName = "PatientDefaultProvider"
            dsSettings.Tables(8).TableName = "BlinkingAlert"
            dsSettings.Tables(9).TableName = "AlertColor"
            dsSettings.Tables(10).TableName = "MaxAppointmentsInSlot"
            dsSettings.Tables(11).TableName = "SHOWTEMPLATE"
            dsSettings.Tables(12).TableName = "FOLLOUPDATE"
            dsSettings.Tables(13).TableName = "CheckedOutAppointment"
            dsSettings.Tables(14).TableName = "MEDICATIONINFO"
            dsSettings.Tables(15).TableName = "SmartDX"
            dsSettings.Tables(16).TableName = "SmartCPT"
            dsSettings.Tables(17).TableName = "SmartOrder"
            dsSettings.Tables(18).TableName = "IsShowPatientConfiInfo"
            dsSettings.Tables(21).TableName = "ExamAutoRecovery"
            dsSettings.Tables(22).TableName = "ClearDashboardSearch"
            dsSettings.Tables(23).TableName = "EnableLocalWelchAllynECGDevice"

            If (gnPatientSynopsisTabCount > 0) Then
                'txtPatientSypnosisTabCount.Text = gnPatientSynopsisTabCount 'bydefault show 1 patient tab in patient synopsis module
                numPatientSypnosisTabCount.Value = gnPatientSynopsisTabCount 'bydefault show 1 patient tab in patient synopsis module
            End If
            GetNoOfColsOnCalendar()

        

            'Call Fill_Printers()
            Call Fill_Interval()

            'to add setting  for user to choose the default drug button in MX and RX
            Call Fill_RxMxDrugBtn()



            If gblnResultsBoxVisible = True Then
                optYes.Checked = True
                pnlResultBoxPosition.Enabled = True
                Select Case genmResultsBoxPosition
                    Case mdlVoice.enmResultsBoxPosition.TopLeft
                        optTopLeft.Checked = True
                    Case mdlVoice.enmResultsBoxPosition.TopRight
                        optTopRight.Checked = True
                    Case mdlVoice.enmResultsBoxPosition.BottomLeft
                        optBottomLeft.Checked = True
                    Case mdlVoice.enmResultsBoxPosition.BottomRight
                        optBottomRight.Checked = True
                End Select
            Else
                optNo.Checked = True
                pnlResultBoxPosition.Enabled = False

            End If

            ''set word highlighted color
            FillColorsindex()

            ExamNotesSelection(True)




            'FAX Settings
            If Trim(gstrFAXPrinterName) <> "" Then
                If cmbFAXPrinter.Items.IndexOf(gstrFAXPrinterName) >= 0 Then
                    cmbFAXPrinter.SelectedIndex = cmbFAXPrinter.Items.IndexOf(gstrFAXPrinterName)
                End If
            End If
            If Trim(gstrFAXOutputDirectory) <> "" Then
                txtFAXOutputDirectory.Text = gstrFAXOutputDirectory
            End If

            '' CR00000126 : FAX for Terminal Server
            '' New setting added for ReceivedFaxFolder useful only for Terminal Server 
            If (gloSettings.gloRegistrySetting.IsServerOS) Then
                If Trim(gstrFAXReceivedDirectoryTS) <> "" Then
                    txtFaxDownloadPath.Text = gstrFAXReceivedDirectoryTS
                End If
            Else
                '' Do not show setting panel if not terminal server
                txtFaxDownloadPath.Enabled = False
                btnBrowseDownloadPath.Enabled = False
                GroupBox14.Visible = False
            End If

            'sarika DICOM Settings 20090214
            If Trim(DICOMPath) <> "" Then
                txtDICOMPath.Text = DICOMPath
            End If
            '---

            'FAX Cover Page
            chkCoverPage.Checked = gblnFAXCoverPage
            chkHandleFAXIssue.Checked = gblnSameCoverPageForAllReferrals

            txtDMSPath.Text = DMSRootPath.Trim
            lblDMSMessage.Visible = False


            Dim fchk As Form
            Dim strDMS As String = ""

            For Each fchk In oMainFormRef.MdiChildren
                If fchk.Name = "frmDMS_MaintainDocument" Then
                    strDMS = "User can not change this path, because Scan Document is opened"
                    Exit For
                ElseIf fchk.Name = "frmDMS_ViewDocument" Then
                    strDMS = "User can not change this path, because View Document is opened"
                    Exit For
                End If
            Next

            If strDMS.Trim = "" Then
                lblDMSMessage.Visible = False
                txtDMSPath.Enabled = True
                btnBrowseDMSPath.Enabled = True
            Else
                lblDMSMessage.Visible = True
                lblDMSMessage.Text = strDMS
                txtDMSPath.Enabled = False
                btnBrowseDMSPath.Enabled = False
            End If

            txtVMSPath.Text = VMSRootPath.Trim
            If Trim(gstrServerPath) <> "" Then
                txtServerPath.Text = gstrServerPath
            End If

            '' 20081121 - Setting Added for Auto Lock Screen
            numLockTime.Value = gLockTime
            chkAutoApplicationLock.Checked = gblnAutoLockEnable

            If gblnAutoLockEnable = False Then
                numLockTime.Enabled = False
            End If

            num_MessagesRefreshTime.Value = gMessageUpdateTime

            If gblnClinicDISetting And gblnAllowUserDISetting Then
                pnlDrugInteraction.Enabled = True
            Else
                tc_Settings.TabPages.Remove(tp_DrugInteraction)
            End If

            If gblnDrugAlertMsg = True Then
                chkDrugAlert.Checked = True
            Else
                chkDrugAlert.Checked = False
            End If

            Call DiAlertSetup()

            'Check if Clinic level Formulary setting set to true 
            If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled Then

                'Below code commented by Ashish on 17th March 2015 for Formulary 3.0 changes
                'gblnLoadFormularyDrugs = True 'allow application to load formulary drugs

                ''''''below code is only written to hide the formulary groupbox  wrt to user rights 
                If gblnIsRxEligUserRight = True Then    ''20100119
                    grpFormularySettings.Visible = True
                Else
                    grpFormularySettings.Visible = False
                End If

            Else
                'Check if Formulary alert has been set true for the Clinic 
                'if not don't show the panel to make DI settings for the machine
                'pnlDrugInteraction.Enabled = False
                '' chkMachineFormularyAlert.Checked = False
                gblnLoadFormularyDrugs = False 'Do not allow application to load formulary drugs
                ''''''Clinic DI setting should be OFF(Disabled) for currently used client machine through gloEMR Admin settings.
                ''tc_Settings.TabPages.RemoveAt(9) ''9= formulary tab
                grpFormularySettings.Visible = False
            End If



            grSettings.Visible = False





            'If gblnDFAAlert = True Then
            '    chkDFAAlert.Checked = True
            'Else
            '    chkDFAAlert.Checked = False
            'End If

            ''to Supress Drug alert Message
            'If gblnDrugAlertMsg = True Then
            '    chkDrugAlert.Checked = True
            'Else
            '    chkDrugAlert.Checked = False
            'End If
            ''to Supress Drug alert Message




            'If gblnDIAlert Then
            '    chckDIAlert.Checked = True
            'End If
            'If gblnMedAlert = True Then
            '    chckMedAlert.Checked = True
            'Else
            '    chckMedAlert.Checked = False
            'End If
            'If gblnRxAlert = True Then
            '    chckRxAlert.Checked = True
            'Else
            '    chckRxAlert.Checked = False
            'End If

            If gblnBdayReminder = True Then
                chkBdayReminder.Checked = True
                numBdayReminder.Value = gnBDayReminderDays
            End If

            'if global All drugs setting is true
            If gblnFormularyAlertnativesAllDrgs = True Then
                'if setting for alternatives of all drugs is true then make the check box for OFF & NR drugs to false
                chkFormularyAlertnativesAllDrgs.Checked = True
                chkFormularyAlertnativesOffFormularyDrgs.Checked = False
                chkFormularyAlertnativesNRDrgs.Checked = False
            Else
                chkFormularyAlertnativesAllDrgs.Checked = False
            End If

            'if global Off formulary drugs setting is true
            If gblnFormularyAlertnativesOffFormularyDrgs = True Then
                chkFormularyAlertnativesOffFormularyDrgs.Checked = True
                'make the All Drugs check box to false
                chkFormularyAlertnativesAllDrgs.Checked = False
            End If

            'if global Not Reimbursable formulary drugs setting is true
            If gblnFormularyAlertnativesNRDrgs = True Then
                chkFormularyAlertnativesNRDrgs.Checked = True
                'make the All Drugs check box to false
                chkFormularyAlertnativesAllDrgs.Checked = False
            End If

            If gblnShowNDCInAlternatives = True Then
                chkNDCInAlternativeGrid.Checked = True
            Else
                chkNDCInAlternativeGrid.Checked = False
            End If

            '------
            'Show Off formulary alternatives
            If gblnShowOffformularyalternatives = True Then
                chkShowOffFormularyAlternatives.Checked = True
            Else
                chkShowOffFormularyAlternatives.Checked = False
            End If

            'Show NDC in Medication History
            If gblnShowNDCInMedicationHistory = True Then
                chkShowNDCInMedication.Checked = True
            Else
                chkShowNDCInMedication.Checked = False
            End If




            Dim comboSourceADE As New Dictionary(Of String, String)()
            comboSourceADE.Add("3", "Severe")
            comboSourceADE.Add("1", "Mild")
            comboSourceADE.Add("2", "Moderate")
            cmbADE.DataSource = New BindingSource(comboSourceADE, Nothing)
            cmbADE.DisplayMember = "Value"
            cmbADE.ValueMember = "Key"

            Dim comboSourceADEOnset As New Dictionary(Of String, String)()
            comboSourceADEOnset.Add("1", "Rapid")
            comboSourceADEOnset.Add("2", "Early")
            comboSourceADEOnset.Add("3", "Delayed")
            cmbADEOnset.DataSource = New BindingSource(comboSourceADEOnset, Nothing)
            cmbADEOnset.DisplayMember = "Value"
            cmbADEOnset.ValueMember = "Key"


            Dim comboSourceDI As New Dictionary(Of String, String)()
            comboSourceDI.Add("1", "Severe")
            comboSourceDI.Add("2", "Major")
            comboSourceDI.Add("3", "Moderate")
            comboSourceDI.Add("4", "Minor")
            cmbDI.DataSource = New BindingSource(comboSourceDI, Nothing)
            cmbDI.DisplayMember = "Value"
            cmbDI.ValueMember = "Key"

            Dim comboSourceDIDoc As New Dictionary(Of String, String)()
            comboSourceDIDoc.Add("2", "Likely Established")
            comboSourceDIDoc.Add("1", "Established")
            comboSourceDIDoc.Add("3", "Not Established")
            cmbDIDoc.DataSource = New BindingSource(comboSourceDIDoc, Nothing)
            cmbDIDoc.DisplayMember = "Value"
            cmbDIDoc.ValueMember = "Key"


            Dim comboSourceDFA As New Dictionary(Of String, String)()
            comboSourceDFA.Add("1", "Severe")
            comboSourceDFA.Add("2", "Major")
            comboSourceDFA.Add("3", "Moderate")
            comboSourceDFA.Add("4", "Minor")
            cmbDFA.DataSource = New BindingSource(comboSourceDFA, Nothing)
            cmbDFA.DisplayMember = "Value"
            cmbDFA.ValueMember = "Key"


            Dim comboSourceDFADoc As New Dictionary(Of String, String)()
            comboSourceDFADoc.Add("2", "Likely Established")
            comboSourceDFADoc.Add("1", "Established")
            comboSourceDFADoc.Add("3", "Not Established")
            cmbDFADoc.DataSource = New BindingSource(comboSourceDFADoc, Nothing)
            cmbDFADoc.DisplayMember = "Value"
            cmbDFADoc.ValueMember = "Key"



            cmbDI.Text = gstrDISeverityLevel
            cmbDIDoc.Text = gstrDIDocLevel
            cmbDFADoc.Text = gstrDFADocLevel
            cmbADE.Text = gstrADESeverityLevel
            cmbADEOnset.Text = gstrADEOnsetLevel
            cmbDFA.Text = gstrDFASeverityLevel






            chksureAlert.Checked = gblnSurescriptAlert

            chksecureSureScriptsetting.Checked = gblnIsReferalNoteadd
            cmbInterval.Text = gStrSurescriptAlertmin

            chkSurescriptFaxSettings.Checked = gblnIsFaxEnabled
            If gblnSurescriptEnabled = False Then
                If tc_Settings.TabPages.Count > 5 Then

                    tc_Settings.TabPages.Remove(tp_SureScriptSettings)
                End If
            End If


            'FillDMSSettings()
            SetDMSSettings()

            Try
                gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

            GetSigPlusSettings()

            GetGreyScreenIssueSettings()

            Dim oNode As TreeNode

            oNode = New TreeNode("Middle Name")
            oNode.Tag = "MI"
            trvPatientColumns.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("SSN")
            oNode.Tag = "SSN"
            trvPatientColumns.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Date Of Birth")
            oNode.Tag = "DOB"
            trvPatientColumns.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Provider")
            oNode.Tag = "Provider"
            trvPatientColumns.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Phone")
            oNode.Tag = "Phone"
            trvPatientColumns.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Mobile")
            oNode.Tag = "Mobile"
            trvPatientColumns.Nodes.Add(oNode)
            oNode = Nothing

            '0
            Get_ClinicEnvironment()

            '1,2
            FillPatientSearch()

            '3
            FillPatientColumnsSetting()

            '4
            FillPatientSearchSettings()

            'FillExchangeServerSettings()

            '5
            FillPatientDemographics()


            If gblnPageNo = True Then
                rdo_IncludePageNo_Yes.Checked = True
                rdo_IncludePageNo_No.Checked = False
            Else
                rdo_IncludePageNo_Yes.Checked = False
                rdo_IncludePageNo_No.Checked = True
            End If



            chkUseDefaultPrinter.Checked = gblnUseDefaultPrinter

            'Set local print settings
            If (gloGlobal.gloTSPrint.TerminalServer() <> "RDP") OrElse (Not gloGlobal.gloTSPrint.isMapped()) Then
                gbRemotePrintSetting.Enabled = False
            Else
                chkAddFooterService.Enabled = False
                cmbNoPagesSplit.Enabled = False
                cmbNoTemplatesJob.Enabled = False
                rbPrintWordDocPDF.Enabled = False
                rbPrintWordDocEMF.Enabled = False
                rbPrintSSRSReportPDF.Enabled = False
                rbPrintSSRSReportEMF.Enabled = False
                rbPrintClaimsPDF.Enabled = False
                rbPrintClaimsEMF.Enabled = False
                rbPrintImagesPNG.Enabled = False
                rbPrintImagesEMF.Enabled = False
                chkZipMetadata.Enabled = False

                chkEnableLocalPrinter.Checked = gblnEnableLocalPrinter
                chkEnableLocalPrinter_CheckedChanged(Nothing, Nothing)
                chkAddFooterService.Checked = gblnAddFooterInService
                'Fill No of pages to split combo box
                cmbNoPagesSplit.Items.Clear()
                Dim i As Integer = 0
                While i <= 20
                    cmbNoPagesSplit.Items.Add(i)
                    i = i + 2
                End While
                Dim index As Integer
                If cmbNoPagesSplit.Enabled = False Then
                    index = 0
                Else
                    index = cmbNoPagesSplit.Items.IndexOf(gloGlobal.gloTSPrint.NoOfPages)
                    If index = -1 Then
                        index = 0
                    End If
                End If
                cmbNoPagesSplit.SelectedIndex = index
                'Fill No of templates per job combo box
                cmbNoTemplatesJob.Items.Clear()
                Dim j As Integer = 0
                While j <= 30
                    cmbNoTemplatesJob.Items.Add(j)
                    j = j + 1
                End While
                Dim jindex As Integer
                If cmbNoTemplatesJob.Enabled = False Then
                    jindex = 1
                Else
                    jindex = cmbNoTemplatesJob.Items.IndexOf(gloGlobal.gloTSPrint.NoOfTemplatesPerJob)
                    If jindex = -1 Then
                        jindex = 1
                    End If
                End If
                cmbNoTemplatesJob.SelectedIndex = jindex

                If gloGlobal.gloTSPrint.UseEMFForWord Then
                    rbPrintWordDocEMF.Checked = True
                    rbPrintWordDocPDF.Checked = False
                Else
                    rbPrintWordDocEMF.Checked = False
                    rbPrintWordDocPDF.Checked = True
                End If
                If gloGlobal.gloTSPrint.UseEMFForSSRS Then
                    rbPrintSSRSReportEMF.Checked = True
                    rbPrintSSRSReportPDF.Checked = False
                Else
                    rbPrintSSRSReportEMF.Checked = False
                    rbPrintSSRSReportPDF.Checked = True
                End If
                If gloGlobal.gloTSPrint.UseEMFForClaims Then
                    rbPrintClaimsEMF.Checked = True
                    rbPrintClaimsPDF.Checked = False
                Else
                    rbPrintClaimsEMF.Checked = False
                    rbPrintClaimsPDF.Checked = True
                End If
                If gloGlobal.gloTSPrint.UseEMFForImages Then
                    rbPrintImagesEMF.Checked = True
                    rbPrintImagesPNG.Checked = False
                Else
                    rbPrintImagesEMF.Checked = False
                    rbPrintImagesPNG.Checked = True
                End If
                chkZipMetadata.Checked = gloGlobal.gloTSPrint.UseZippedMetadata
            End If

            

            chkResetSearch.Checked = gblnResetSearchTextBox




            FillNavigationPnls()

            FillDefaultNavigation()


            Dim value As New Object

            '6
            FillProviders()

            'ogloSettings.GetSetting("PatientDefaultProvider", value)

            '7
            If dsSettings.Tables("PatientDefaultProvider").Rows.Count > 0 Then
                value = dsSettings.Tables("PatientDefaultProvider").Rows(0)("sSettingsValue")

                If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                    cmbDefaultProvider.SelectedValue = Convert.ToInt64(value)
                End If
            End If

           

            value = Nothing



            'value = New Object

            'ogloSettings.GetSetting("BlinkingAlert", gnLoginID, gnClinicID, value)

            '8
            If dsSettings.Tables("BlinkingAlert").Rows.Count > 0 Then
                value = dsSettings.Tables("BlinkingAlert").Rows(0)("sSettingsValue")

                If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                    chkShowBlinkingAlert.Checked = Convert.ToBoolean(value)
                End If
            End If

           

            'ogloSettings.GetSetting("AlertColor", gnLoginID, gnClinicID, value)

            '9
            If dsSettings.Tables("AlertColor").Rows.Count > 0 Then
                value = dsSettings.Tables("AlertColor").Rows(0)("sSettingsValue")

                If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                    txtAlertColor.BackColor = Color.FromArgb(Convert.ToInt32(value))
                End If
            End If

            


            Dim oSettings As New gloSettings.DatabaseSetting.DataBaseSetting()

            If Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation")) <> "" Then
                chkExportReport.Checked = Convert.ToBoolean(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation"))
            Else
                chkExportReport.Checked = False
            End If
            txtExportReportPath.Text = Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocationPath"))

            oSettings.Dispose()


            value = Nothing
            'ogloSettings.GetSetting("MaxAppointmentsInSlot", value)

            '10
            If dsSettings.Tables("MaxAppointmentsInSlot").Rows.Count > 0 Then
                value = dsSettings.Tables("MaxAppointmentsInSlot").Rows(0)("sSettingsValue")

                If value IsNot Nothing Then
                    If value <> "" Then
                        num_NoofApptInaSlot.Value = Convert.ToInt16(value)
                    End If
                End If
            End If


           
            value = Nothing

            'ogloSettings.GetSetting("ShowTemplate", value)

            '11
            If dsSettings.Tables("ShowTemplate").Rows.Count > 0 Then
                value = dsSettings.Tables("ShowTemplate").Rows(0)("sSettingsValue")

                If value IsNot Nothing Then
                    If value <> "" Then
                        chkShowTemplate.Checked = Convert.ToBoolean(value)
                    End If
                End If
            End If

            
            value = Nothing

            'ogloSettings.GetSetting("FolloupDate", value)

            '12
            If dsSettings.Tables("FOLLOUPDATE").Rows.Count > 0 Then
                value = dsSettings.Tables("FOLLOUPDATE").Rows(0)("sSettingsValue")

                If value IsNot Nothing Then
                    If Convert.ToString(value) = "FolloupDate" Then
                        rbFolloupFromDate.Checked = True
                    Else
                        rbFollowupFromToday.Checked = True
                    End If
                End If
            End If

           
            value = Nothing



            'ogloSettings.GetSetting("CheckedOutAppointment", gnLoginID, gnClinicID, value)

            '13
            If dsSettings.Tables("CheckedOutAppointment").Rows.Count > 0 Then
                value = dsSettings.Tables("CheckedOutAppointment").Rows(0)("sSettingsValue")

                If value IsNot Nothing Then
                    If value <> "" Then
                        chkCheckedoutAppointments.Checked = Convert.ToBoolean(value)
                    End If
                End If
            End If

           
            Try

                'ogloSettings.GetSetting("MEDICATIONINFO", 0, 1, value)

                '14
                If dsSettings.Tables("MEDICATIONINFO").Rows.Count > 0 Then
                    value = dsSettings.Tables("MEDICATIONINFO").Rows(0)("sSettingsValue")

                    If value IsNot Nothing Then
                        If value <> "" Then
                            txtInfo.Text = value
                        End If
                    End If
                End If

               
            Catch ex As Exception

            End Try

            '21 Exam Auto Recovery Settings  

            If dsSettings.Tables("ExamAutoRecovery") IsNot Nothing AndAlso dsSettings.Tables("ExamAutoRecovery").Rows.Count > 0 Then
                value = nothing
                If dsSettings.Tables("ExamAutoRecovery").AsEnumerable().Any(Function(p) p("sSettingsName") = "IsExamAutoSaveEnable") Then
                    value = dsSettings.Tables("ExamAutoRecovery").AsEnumerable().FirstOrDefault(Function(p) p("sSettingsName") = "IsExamAutoSaveEnable")("sSettingsValue")
                End If

                If value IsNot Nothing Then
                    mdlGeneral.IsExamAutoSaveEnable = Convert.ToBoolean(value)
                    value = Nothing
                End If

                If dsSettings.Tables("ExamAutoRecovery").AsEnumerable().Any(Function(p) p("sSettingsName") = "ExamAutoSaveTime") Then
                    value = dsSettings.Tables("ExamAutoRecovery").AsEnumerable().FirstOrDefault(Function(p) p("sSettingsName") = "ExamAutoSaveTime")("sSettingsValue")
                End If

                If value IsNot Nothing AndAlso Convert.ToInt32(value) > 0 Then
                    mdlGeneral.ExamAutoSaveTime = Convert.ToInt32(value)
                    value = Nothing
                End If

                Me.chkAutoSaveExam.Checked = mdlGeneral.IsExamAutoSaveEnable
                Me.numAutoSaveMinutes.Value = mdlGeneral.ExamAutoSaveTime

            End If

            '22 Clear Dashboard Search
            If dsSettings.Tables("ClearDashboardSearch").Rows.Count > 0 Then
                value = dsSettings.Tables("ClearDashboardSearch").Rows(0)("sSettingsValue")

                If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                    chkClearDashboardSearch.Checked = Convert.ToBoolean(value)
                End If
            End If

            '23 Enable Local WelchAllyn ECG Device
            If dsSettings.Tables("EnableLocalWelchAllynECGDevice").Rows.Count > 0 Then
                value = dsSettings.Tables("EnableLocalWelchAllynECGDevice").Rows(0)("sSettingsValue")

                If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                    chkEnableLocalWelchAllynECGDevice.Checked = Convert.ToBoolean(value)
                End If
            End If

            FillSmartData()



            gblIsConfidentialInfo = getPatientConfidentialInfo()
            ChkPatientConfiInfo.Checked = gblIsConfidentialInfo


            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Add, "Setting added.", gloAuditTrail.ActivityOutCome.Success)

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Add, "Setting added.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            Read_ErrorlogSettings()

            UpdateVoiceLog("Client Settings Completed")

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            ogloSettings.Dispose()
            ogloSettings = Nothing

            tc_Settings.TabPages.Remove(tp_ExchangeServer)

        End Try
    End Sub

    Private Sub DiAlertSetup()

        If gblnClinicDISetting And gblnAllowUserDISetting Then
            If gblnDrugToDiseaseInteractionAlert Then
                chkDrugDiseaseInteraction.Checked = True
            Else
                chkDrugDiseaseInteraction.Checked = False
            End If

            If gblnDrugToFoodInteractionAlert Then
                chkDrugFoodInteraction.Checked = True
            Else
                chkDrugFoodInteraction.Checked = False
            End If

            If gblnDuplicateTherapyInteractionAlert Then
                chkDuplicateTherapy.Checked = True
            Else
                chkDuplicateTherapy.Checked = False
            End If

            If gblnAdverseDrugEffectAlert Then
                chkAdverseDrugEffect.Checked = True
            Else
                chkAdverseDrugEffect.Checked = False
            End If
        Else
            chkDrugDiseaseInteraction.Checked = False
            chkDrugFoodInteraction.Checked = False
            chkDuplicateTherapy.Checked = False
            chkAdverseDrugEffect.Checked = False
        End If
    End Sub

    ''Sandip Darade 2009729
    ''Patient demographics 
    Private Sub GetNoOfColsOnCalendar()
        Dim oSettings As New gloSettings.DatabaseSetting.DataBaseSetting()
        Try
            Dim _sNoOfColsOnCalendar As String = oSettings.ReadSettings_XML("Appointment", "NoOfColsOnCalendar")
            If _sNoOfColsOnCalendar.Trim() <> "" Then
                num_NoofColOnCalndr.Value = Convert.ToInt32(_sNoOfColsOnCalendar)
            Else
                num_NoofColOnCalndr.Value = 3
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oSettings IsNot Nothing Then
                oSettings.Dispose()
            End If
        End Try
    End Sub

    Private Sub FillPatientDemographics()
        Try
            Dim oNode As TreeNode

            oNode = New TreeNode("Gender")
            oNode.Tag = "Gender"
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            ''Patient Phone field added here in order to fix issue:#5687-should be below Gender & above Mobile
            oNode = New TreeNode("Phone")
            oNode.Tag = "PatientPhone"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing
            ''
            oNode = New TreeNode("Mobile")
            oNode.Tag = "Mobile"
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing
          

            oNode = New TreeNode("Fax")
            oNode.Tag = "Fax"
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Email")
            oNode.Tag = "Email"
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Emergency Contact")
            oNode.Tag = "Emergency Contact"
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Emergency Phone")
            oNode.Tag = "Emergency Phone"
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Emergency Mobile")
            oNode.Tag = "Emergency Mobile"
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing


            oNode = New TreeNode("Provider")
            oNode.Tag = "Provider"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Pharmacy")
            oNode.Tag = "Pharmacy"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Pharmacy Address")
            oNode.Tag = "Pharmacy Address"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            ''Added by Mayuri:20101028
            oNode = New TreeNode("Pharmacy Phone")
            oNode.Tag = "Pharmacy Phone"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            ''end code Added on 20101028
            oNode = New TreeNode("PCP")
            oNode.Tag = "PCP"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            'oNode = New TreeNode("PCP Business Phone ")
            'oNode.Tag = "PCPBusPh"
            oNode = New TreeNode("PCP Phone ")
            oNode.Tag = "PCPPh"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Primary Insurance")
            oNode.Tag = "Primary Insurance"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Secondary Insurance")
            oNode.Tag = "Secondary Insurance"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Tertiary Insurance")
            oNode.Tag = "Tertiary Insurance"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Status")
            oNode.Tag = "Status"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Lab Status")
            oNode.Tag = "Lab Status"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Race")
            oNode.Tag = "Race"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Ethnicity")
            oNode.Tag = "Ethnicity"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Language")
            oNode.Tag = "Language"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing


            oNode = New TreeNode("Medical Category")
            oNode.Tag = "Medical Category"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            ''occupation,Work Phone,Business Center added for 8070 prd  
            oNode = New TreeNode("Occupation")
            oNode.Tag = "Occupation"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Business Center")
            oNode.Tag = "Business Center"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            oNode = New TreeNode("Work Phone")
            oNode.Tag = "Work Phone"
            oNode.Checked = False
            trvDemographics.Nodes.Add(oNode)
            oNode = Nothing

            trvDemographics.Nodes(trvDemographics.Nodes.Count - 1).EnsureVisible()

            ' #Region "Patient Demographics" 
            'Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
            Dim value As Object = Nothing
            '' Retrive Demographics setting from current user
            'ogloSettings.GetSetting("Patient Demographics", gnLoginID, gnClinicID, value)

            If dsSettings.Tables("PatientDemographics").Rows.Count > 0 Then
                value = dsSettings.Tables("PatientDemographics").Rows(0)("sSettingsValue")
            End If

            If value IsNot Nothing Then
                If Convert.ToString(value).Trim() <> "" Then
                    Dim PatientDemographics As String() = Convert.ToString(value).Trim().Split(",")
                    For i As Integer = 0 To PatientDemographics.Length - 1
                        For j As Integer = 0 To trvDemographics.Nodes.Count - 1
                            If trvDemographics.Nodes(j).Tag.ToString().Trim() = PatientDemographics(i).Trim() Then
                                trvDemographics.Nodes(j).Checked = True
                            End If
                        Next
                    Next
                End If
            End If

            'ogloSettings.Dispose()
            'ogloSettings = Nothing


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try
    End Sub

    Private Sub FillProviders()



        Try

            'dtProvider = GetProviders()
            dtProvider = dsSettings.Tables("Providers") ' = GetProviders()

            If dtProvider IsNot Nothing Then
                Dim dr As DataRow = dtProvider.NewRow()
                dr("nProviderID") = "0"
                dr("ProviderName") = ""
                dtProvider.Rows.InsertAt(dr, 0)
                dtProvider.AcceptChanges()

                cmbDefaultProvider.DataSource = dtProvider
                cmbDefaultProvider.DisplayMember = "ProviderName"
                cmbDefaultProvider.ValueMember = "nProviderID"
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)




        End Try

    End Sub
    Private Function GetProviders() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim _strSQL As [String] = ""
        Dim dtProviderType As DataTable = Nothing
        Try
            If gnClinicID = 0 Then
                'ADD THE CONDITION FOR MIDDLE NAME WHEHTER IT IS BLANK 20100619
                _strSQL = "SELECT nProviderID , (ISNULL(sFirstName,'')+ SPACE(1) +CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When ISNULL(Provider_MST.sMiddleName,'') then  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) END +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST  Where bIsblocked <> 1 ORDER BY ProviderName"
            Else
                'ADD THE CONDITION FOR MIDDLE NAME WHEHTER IT IS BLANK 20100619
                _strSQL = "SELECT nProviderID , (ISNULL(sFirstName,'')+ SPACE(1) + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When ISNULL(Provider_MST.sMiddleName,'') then  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) END +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST WHERE bIsblocked <> 1 And nClinicID = " & gnClinicID & " ORDER BY ProviderName"
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function


    Private Sub SetDMSSettings()
        
        Try


            If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                Exit Sub
            End If

            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)

            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScan)) = True Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScan, "")
            Else
                cmbScanner.Text = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScan)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSResol)) = True Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSResol, "")
            Else
                cmbResolution.Text = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSResol)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSBright)) = True Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSBright, "")
            Else
                cmbBrightness.Text = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSBright)
            End If





            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSContrast)) = True Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSContrast, "")
            Else
                cmbContrast.Text = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSContrast)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanMode)) = True Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanMode, "")
            Else
                cmbScanMode.Text = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanMode)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanSide)) = True Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanSide, "")
            Else
                cmbScanSide.Text = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSScanSide)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSShowScann)) = True Then   '"DMSShowScanner"
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSShowScann, False)
            Else
                chkShowScannerDialog.Checked = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSShowScann)
            End If



            ''CardLength
            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardLength)) = True Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardLength, "4.0")

            End If

            txtCardLength.Text = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardLength)


            ''CardWidth
            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardWidth)) = True Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardWidth, "4.0")

            End If
            txtCardWidth.Text = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardWidth)


            ''CardTop
            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardLeftX)) = True Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardLeftX, "0.0")
            Else
                txtStartX.Text = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardLeftX)
            End If


            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardTopY)) = True Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardTopY, "0.0")
            Else
                txtStartY.Text = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSCardTopY)
            End If




            If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSBuffSz)) = False Then
                If gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSBuffSz).ToString() <> "" Then
                    Dim nBufferSizeInMB As Decimal = CType(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSBuffSz), Decimal) / (1024 * 1024)
                    If nBufferSizeInMB > numBufferSize.Minimum And nBufferSizeInMB <= numBufferSize.Maximum Then
                        numBufferSize.Value = Convert.ToDecimal(nBufferSizeInMB)
                    Else
                        numBufferSize.Value = numBufferSize.Maximum
                    End If
                Else
                    numBufferSize.Value = numBufferSize.Maximum
                End If
            Else
                gloRegistrySetting.SetRegistryValue("DMSBufferSize", (numBufferSize.Minimum) * (1024 * 1024))
                numBufferSize.Value = numBufferSize.Maximum
            End If
            gnBufferSize = CType((numBufferSize.Value) * (1024 * 1024), Integer)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try


    End Sub

    Private Sub Read_ErrorlogSettings()
        Try
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            If IsNothing(gloRegistrySetting.GetRegistryValue("EnableApplicationLogs")) = True Then
                gloRegistrySetting.SetRegistryValue("EnableApplicationLogs", False)
                chk_ApplicationLog.Checked = False

            Else
                chk_ApplicationLog.Checked = gloRegistrySetting.GetRegistryValue("EnableApplicationLogs")

            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue("EnableErrorLogs")) = True Then
                gloRegistrySetting.SetRegistryValue("EnableErrorLogs", True)
                chk_ErrorLogs.Checked = True

            Else
                chk_ErrorLogs.Checked = gloRegistrySetting.GetRegistryValue("EnableErrorLogs")

            End If
            gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = chk_ApplicationLog.Checked
            gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = chk_ErrorLogs.Checked
            ' gloRegistrySetting.CloseRegistryKey()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Set_ErrorlogSettings()
        Try
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            If chk_ApplicationLog.Checked = True Then
                gloRegistrySetting.SetRegistryValue("EnableApplicationLogs", True)
            Else
                gloRegistrySetting.SetRegistryValue("EnableApplicationLogs", False)
            End If

            If chk_ErrorLogs.Checked = True Then
                gloRegistrySetting.SetRegistryValue("EnableErrorLogs", True)
            Else
                gloRegistrySetting.SetRegistryValue("EnableErrorLogs", False)
            End If
            '  gloRegistrySetting.CloseRegistryKey()
            gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = chk_ApplicationLog.Checked
            gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = chk_ErrorLogs.Checked
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetRegistryValue(strRegistryKey As String) As String
        Dim RegValue As String = Nothing

        Try
            If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True, "") = False Then
                _ErrorMessage = "Unable to open registry. " + gloRegistrySetting.gstrSoftEMR
                'AuditLogErrorMessage(_ErrorMessage)
                MessageBox.Show(_ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return RegValue
            End If

            Dim oSetting As Object = gloRegistrySetting.GetRegistryValue(strRegistryKey)

            If oSetting IsNot Nothing Then
                RegValue = oSetting.ToString()
            End If

            gloRegistrySetting.CloseRegistryKey()
        Catch ex As Exception
            _ErrorMessage = ex.ToString()
            MessageBox.Show(_ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegValue = Nothing
        End Try

        Return RegValue
    End Function

    Private Function CallRemoteScanSettingsLoad() As Boolean
        If chkEnableRemoteScanner.Checked Then
            If Not gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg:=True) Then
                Return False
            End If
        End If

        pnlRemoteScan.Enabled = True
        GroupBox3.Enabled = False

        pnlRemoteScan.Visible = True
        GroupBox3.Visible = False

        If gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings Is Nothing Then
			Try
				gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject(chkEliminatePegasus.Checked)
			Catch ex As Exception
				bIsScannerConnected = False
			End Try
		End If
		
        bIsScannerConnected = True
       

        If gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings IsNot Nothing Then
            If gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner Is Nothing Then
                bIsScannerConnected = False
                'MessageBox.Show("Local scanner settings not found.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Return False
            End If

            Dim sCurrRemoteShowScann As String = GetRegistryValue(gloRegistrySetting.gstrRemoteShowScann)
            If Not String.IsNullOrEmpty(sCurrRemoteShowScann) Then
                chkRemoteShowScannerDialog.Checked = Convert.ToBoolean(sCurrRemoteShowScann)
            Else
                chkRemoteShowScannerDialog.Checked = True
            End If
            Dim sCurrRemoteScanner As String = GetRegistryValue(gloRegistrySetting.gstrRemoteScanner)
            Dim indexi As Int32 = 0
            Dim iRowCnt As Int32 = 0

            dtScanner = New DataTable()
            dtScanner.Columns.Add("ScannerId", GetType(String))
            dtScanner.Columns.Add("ScannerName", GetType(String))
            For i As Integer = 0 To gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner.Length - 1
                If sCurrRemoteScanner = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(i).Name Then
                    indexi = iRowCnt ' Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(i).ScannerID)
                End If

                dtScanner.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(i).ScannerID, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(i).Name)
                iRowCnt = iRowCnt + 1
            Next
            cmbRemoteScanner.DataSource = Nothing
            cmbRemoteScanner.ValueMember = "ScannerId"
            cmbRemoteScanner.DisplayMember = "ScannerName"
            cmbRemoteScanner.DataSource = dtScanner
            cmbRemoteScanner.SelectedIndex = indexi

            cmbRemoteScanner_SelectedIndexChanged(Nothing, Nothing)
            Return True
        Else
            bIsScannerConnected = False
            'MessageBox.Show("2.Local scanner settings not found.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return False
        End If
    End Function
    Private Sub FillDMSSettings()

        If _isScannerSemaphore = True Then
            Return
        End If

        ' GroupBox3.Enabled = False

        _isScannerSemaphore = True

        System.Threading.Thread.Sleep(1000) 'Added to enable setting check box  chkEnableRemoteScanner

        If (gloGlobal.gloTSPrint.TerminalServer() <> "RDP") OrElse (Not gloGlobal.gloRemoteScanSettings.isScanServiceWorking()) Then
            chkEnableRemoteScanner.Enabled = False
            chkEnableRemoteScanner.Checked = False

            chkEliminatePegasus.Checked = gloGlobal.gloEliminatePegasus.bEliminatePegasus
            chkEliminatePegasus.Enabled = True

            chkZipScannerSettings.Enabled = False
            chkZipScannerSettings.Checked = False
        Else
            chkEnableRemoteScanner.Enabled = True
            chkEnableRemoteScanner.Checked = gloGlobal.gloRemoteScanSettings.EnableRemoteScan

            'chkEliminatePegasus.Checked = False
            'chkEliminatePegasus.Enabled = False

            If gloGlobal.gloRemoteScanSettings.EnableRemoteScan Then
                chkEliminatePegasus.Enabled = False
                chkEliminatePegasus.Checked = False
                chkEliminatePegasus.Visible = False

                chkZipScannerSettings.Enabled = True
                chkZipScannerSettings.Checked = gloGlobal.gloRemoteScanSettings.bZipScanSettings
            Else
                chkEliminatePegasus.Enabled = True
                chkEliminatePegasus.Checked = gloGlobal.gloEliminatePegasus.bEliminatePegasus
                chkEliminatePegasus.Visible = True

                chkZipScannerSettings.Enabled = False
                chkZipScannerSettings.Checked = False
            End If
        End If



        'cmbImageFormat.Enabled = False

        If chkEliminatePegasus.Checked Then
            btnRefreshTwainScanners.Visible = True
            ' cmbRemoteImageFormat.Enabled = True
        ElseIf chkEnableRemoteScanner.Checked Then

            btnRefreshTwainScanners.Visible = False
            'cmbRemoteImageFormat.Enabled = False
        Else
            btnRefreshTwainScanners.Visible = False
        End If

        FillImageFormatCMB()


        If chkEliminatePegasus.Checked AndAlso Not chkEnableRemoteScanner.Checked Then
            cmbRemoteImageFormat.Enabled = True
        ElseIf Not chkEliminatePegasus.Checked OrElse chkEnableRemoteScanner.Checked Then
            cmbRemoteImageFormat.Enabled = False
        End If

        btnRefreshScanners.Visible = chkEnableRemoteScanner.Checked
        pnlRemoteScan.Enabled = chkEnableRemoteScanner.Checked
        GroupBox3.Enabled = Not chkEnableRemoteScanner.Checked

        pnlRemoteScan.Visible = chkEnableRemoteScanner.Checked
        GroupBox3.Visible = Not chkEnableRemoteScanner.Checked

        DisposingTwain()

        If chkEnableRemoteScanner.Checked Or chkEliminatePegasus.Checked Then
            FillFeederCombo()
            CallRemoteScanSettingsLoad()
        Else
            InitPagasusTwainDevice()

            objScannerSettings.GetAndSetScanners(twainDevice, cmbScanner, gloRegistrySetting.gstrDMSScan)
            objScannerSettings.ObtainScannerSettings(twainDevice, cmbScanner, cmbScanner, cmbScanMode, cmbBitDepth, cmbResolution, cmbBrightness, cmbContrast, cmbScanSide, chkShowScannerDialog, cmbSupportedSize, txtCardWidth, txtCardLength, txtStartX, txtStartY, myScanLayout)
        End If
        _isScannerSemaphore = False

        '  GroupBox3.Enabled = True

    End Sub

    Private Sub FillImageFormatCMB()
        cmbRemoteImageFormat.Items.Insert(0, "Default")
        cmbRemoteImageFormat.Items.Insert(1, "Bmp")
        'cmbRemoteImageFormat.Items.Insert(2, "Emf")
        'cmbRemoteImageFormat.Items.Insert(3, "Exif")
        'cmbRemoteImageFormat.Items.Insert(4, "Gif")
        'cmbRemoteImageFormat.Items.Insert(5, "Icon")
        cmbRemoteImageFormat.Items.Insert(2, "Jpeg")
        'cmbRemoteImageFormat.Items.Insert(7, "MemoryBmp")
        cmbRemoteImageFormat.Items.Insert(3, "Png")
        cmbRemoteImageFormat.Items.Insert(4, "Tiff")
        'cmbRemoteImageFormat.Items.Insert(10, "Wmf")

        cmbRemoteImageFormat.SelectedIndex = 0

        If chkEliminatePegasus.Checked Then
            If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True, "") = False Then
                Return
            End If
            Dim oScanImageFormat As Object = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrScanImageFormat)

            Dim sScanImageFormat As String = Convert.ToString(oScanImageFormat).Trim()

            For i As Integer = 0 To cmbRemoteImageFormat.Items.Count - 1
                If Convert.ToString(cmbRemoteImageFormat.Items(i)) = sScanImageFormat Then
                    cmbRemoteImageFormat.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub SaveDMSSettings()

        Try

            If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                Exit Sub
                'Else
                '    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScan, cmbScanner.Text)
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSResol, cmbResolution.Text)
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSBright, cmbBrightness.Text)
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSContrast, cmbContrast.Text)
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanMode, cmbScanMode.Text)
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanSide, cmbScanSide.Text)
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSShowScann, chkShowScannerDialog.Checked)
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSupporedSize, cmbSupportedSize.Text) ''Supported Size


                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardWidth, txtCardWidth.Text.Trim())
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardLength, txtCardLength.Text.Trim())
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardLeftX, txtStartX.Text.Trim()) ''txtstartX Position
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardTopY, txtStartY.Text.Trim()) ''txtStartYPosistion


                '    ''BufferSize Settings 
                '    Dim nBufferSizeInBytes As Integer = CType((numBufferSize.Value) * (1024 * 1024), Integer)
                '    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSBuffSz, nBufferSizeInBytes)
                '    gnBufferSize = nBufferSizeInBytes
                '    gloRegistrySetting.CloseRegistryKey()

            End If
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)

            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrEnableRemoteScan, chkEnableRemoteScanner.Checked)
            gloGlobal.gloRemoteScanSettings.EnableRemoteScan = chkEnableRemoteScanner.Checked

            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrZipScannerSettings, chkZipScannerSettings.Checked)
            gloGlobal.gloRemoteScanSettings.bZipScanSettings = chkZipScannerSettings.Checked

            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrEliminatePegasus, chkEliminatePegasus.Checked)
            gloGlobal.gloEliminatePegasus.bEliminatePegasus = chkEliminatePegasus.Checked

            If chkEliminatePegasus.Checked Then
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrScanImageFormat, cmbRemoteImageFormat.Text)
            End If

            If chkEnableRemoteScanner.Checked Or chkEliminatePegasus.Checked Then
                '(gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanner, cmbRemoteScanner.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanResol, cmbRemoteResolution.Text)
                'gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanBright, cmbRemoteBrightness.Text)
                'gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanContrast, cmbRemoteContrast.Text)

                If bChkForBrightness Then
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanBright, (Convert.ToInt64(cmbRemoteBrightness.Text) - BrightnessScale))
                Else
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanBright, cmbRemoteBrightness.Text)
                End If
                If bChkForContrast Then
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanContrast, (Convert.ToInt64(cmbRemoteContrast.Text) - ContrastScale))
                Else
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanContrast, cmbRemoteContrast.Text)
                End If

                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanMode, cmbRemoteScanMode.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanSide, cmbRemoteScanSide.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanFeeder, cmbRemoteScanSideFeeder.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteShowScann, chkRemoteShowScannerDialog.Checked)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteSupporedSize, cmbRemoteSupportedSize.Text)
                'Supported Size
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteScanDepth, cmbRemoteBitDepth.Text)
                '0007876::/Card scan from within scan documents not working properly/[ScanDepth Was not setted]
                ' Add card size setting 
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteCardWidth, txtRemoteCardWidth.Text.Trim())
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteCardLength, txtRemoteCardLength.Text.Trim())
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteCardLeftX, txtRemoteStartX.Text.Trim())
                'txtstartX Position
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrRemoteCardTopY, txtRemoteStartY.Text.Trim())
                'txtStartYPosistion
                If oRemoteScanCommon Is Nothing Then
                    oRemoteScanCommon = New gloEDocumentV3.Common.RemoteScanCommon()
                End If
                Dim sRetVal As String = oRemoteScanCommon.SetRemoteScannerCurrentSettings(Nothing, Nothing, Nothing)
                If Not String.IsNullOrEmpty(sRetVal) Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.Save, sRetVal, gloAuditTrail.ActivityOutCome.Failure)
                End If
            Else
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScan, cmbScanner.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSResol, cmbResolution.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSBright, cmbBrightness.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSContrast, cmbContrast.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanMode, cmbScanMode.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanSide, cmbScanSide.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSShowScann, chkShowScannerDialog.Checked)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSupporedSize, cmbSupportedSize.Text)
                'Supported Size
                'gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSScanDepth, cmbBitDepth.Text)
                '0007876::/Card scan from within scan documents not working properly/[ScanDepth Was not setted]
                'Sandip Darade   20090926
                ' Add card size setting 
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardWidth, txtCardWidth.Text.Trim())
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardLength, txtCardLength.Text.Trim())
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardLeftX, txtStartX.Text.Trim())
                'txtstartX Position
                'txtStartYPosistion
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSCardTopY, txtStartY.Text.Trim())


                '    ''BufferSize Settings 
                Dim nBufferSizeInBytes As Integer = CType((numBufferSize.Value) * (1024 * 1024), Integer)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrDMSBuffSz, nBufferSizeInBytes)
                gnBufferSize = nBufferSizeInBytes

                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrPegasusBright, cmbBrightness.Text)
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrPegasusContrast, cmbContrast.Text)

                gloGlobal.gloEliminatePegasus.sPegasusBright = cmbBrightness.Text
                gloGlobal.gloEliminatePegasus.sPegasusContrast = cmbContrast.Text
            End If
            bChkPegasusValues = False
            gloRegistrySetting.CloseRegistryKey()

            Try
                gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub

    Public Sub Fill_Interval()
        cmbInterval.Items.Add("15 Sec")
        cmbInterval.Items.Add("30 Sec")
        cmbInterval.Items.Add("45 Sec")
        For i As Integer = 1 To 120
            cmbInterval.Items.Add(i & " Min")
        Next
    End Sub

    Public Sub Fill_RxMxDrugBtn()
        Try
            'fill the Prescription button in the combo box
            cmbRxDrugBtn.Items.Add("Frequently Used Drugs")
            cmbRxDrugBtn.Items.Add("All Drugs")
            cmbRxDrugBtn.Items.Add("Practice Favorites")
            cmbRxDrugBtn.Items.Add("Provider Specific Drugs")

            If gnDrugListButton = 13 Then
                cmbRxDrugBtn.SelectedIndex = 0
            ElseIf gnDrugListButton = 11 Then
                cmbRxDrugBtn.SelectedIndex = 1
            ElseIf gnDrugListButton = 12 Then
                cmbRxDrugBtn.SelectedIndex = 2
            ElseIf gnDrugListButton = 21 Then
                cmbRxDrugBtn.SelectedIndex = 3
            Else
                cmbRxDrugBtn.SelectedIndex = 0
            End If



            ''commented for Bugno:4441 
            ' ''fill the Medication button in the combo box
            ''cmbMxDrugBtn.Items.Add("All Drugs")
            ''cmbMxDrugBtn.Items.Add("Practice Favorites")
            ''cmbMxDrugBtn.Items.Add("Provider Specific Drugs")
            ''cmbMxDrugBtn.SelectedIndex = gnMedDrugButton

        Catch ex As Exception

        End Try
    End Sub

    Private Sub optNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optNo.Click
        Try
            pnlResultBoxPosition.Enabled = False
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub optYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optYes.Click
        Try
            pnlResultBoxPosition.Enabled = True
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CancelSetting()
        Try
            bChkPegasusValues = False
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch objErr As Exception
            Me.DialogResult = DialogResult.Cancel
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OKSetting()
        '        Dim conn As New SqlClient.SqlConnection(GetConnectionString())

        Try


            If Trim(txtFAXOutputDirectory.Text) <> "" Then
                If Directory.Exists(txtFAXOutputDirectory.Text) = False Then
                    MessageBox.Show(txtFAXOutputDirectory.Text & " is not valid path." & vbCrLf & "Please browse for valid FAX Output Directory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtFAXOutputDirectory.Focus()
                    Exit Sub
                End If
            End If

            '' CR00000126 : FAX for Terminal Server
            '' Validate FAX Download Directory only in case of Terminal Server 
            If (gloSettings.gloRegistrySetting.IsServerOS) Then
                If Trim(txtFaxDownloadPath.Text) <> "" Then
                    If Directory.Exists(txtFaxDownloadPath.Text) = False Then
                        MessageBox.Show(txtFaxDownloadPath.Text & " is not valid path." & vbCrLf & "Please browse for valid FAX Download Directory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtFaxDownloadPath.Focus()
                        Exit Sub
                    End If
                End If
            End If

            If Trim(txtDMSPath.Text) <> "" Then
                If Directory.Exists(txtDMSPath.Text) = False Then
                    MessageBox.Show(txtDMSPath.Text & " is not valid path." & vbCrLf & "Please browse for valid DMS Directory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtDMSPath.Focus()
                    Exit Sub
                End If
            End If
            If Trim(txtVMSPath.Text) <> "" Then
                If Directory.Exists(txtVMSPath.Text) = False Then
                    MessageBox.Show(txtVMSPath.Text & " is not valid path." & vbCrLf & "Please browse for valid VMS Directory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtVMSPath.Focus()
                    Exit Sub
                End If
            End If
            If Trim(txtServerPath.Text) <> "" Then
                If Directory.Exists(txtServerPath.Text) = False Then
                    MessageBox.Show(txtServerPath.Text & " is not valid path." & vbCrLf & "Please browse for valid Server Directory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtServerPath.Focus()
                    Exit Sub
                End If
            End If
            If numLockTime.Value <= 0 Then
                MessageBox.Show("Lock Time Period must be greater than or equal to 1 minute.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                numLockTime.Focus()
                Exit Sub
            End If
            ' Problem #28303: 00000156 : Signature Pad not working on Terminal Server
            If (cbSigPlusTS.Checked) Then
                If gloRegistrySetting.IsServerOS Then
                    If Trim(txtTabletPortPath.Text) = "" Then
                        MessageBox.Show("Enter TabletPortPath for SigPlus.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtTabletPortPath.Focus()
                        Exit Sub
                    End If
                End If
            End If
            '------------

            If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft, True)
                gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrEMR)
                gloRegistrySetting.CloseRegistryKey()
            End If
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            If optYes.Checked = True Then
                gblnResultsBoxVisible = True
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrShwresBox, "True") ' "ShowResultBox", "True")
            Else
                gblnResultsBoxVisible = False
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrShwresBox, "False")
            End If

            ' Problem #28303: 00000156 : Signature Pad not working on Terminal Server
            '' Saving SigPlus Settings to Registry, Also setting globalVariables
            SetSigPlusSettings()

            SetGreyScreenIssueSettings()

            'Set Error log settings
            Set_ErrorlogSettings()
            '---


            'if formulary machine alert is false then dont allow to load the formulary drug 
            If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled Then

                'gblnLoadFormularyDrugs = True
                CType(Me.Owner, gloEMR.MainMenu).tlbbtn_RxHub.Visible = True
            Else
                gblnLoadFormularyDrugs = False

                CType(Me.Owner, gloEMR.MainMenu).tlbbtn_RxHub.Visible = False
            End If
            'if formulary machine alert is false then dont allow to load the formulary drug 


            ''Formulary Alertnatives AllDrgs
            'If chkFormularyAlertnativesAllDrgs.Checked = True Then
            '    If IsNothing(regKey.GetValue(FormularyAlertnativesAllDrgs)) = True Then
            '        regKey.SetValue(FormularyAlertnativesAllDrgs, True)
            '    Else
            '        regKey.SetValue(FormularyAlertnativesAllDrgs, True)
            '    End If
            '    gblnFormularyAlertnativesAllDrgs = True
            '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = True

            '    gblnFormularyAlertnativesOffFormularyDrgs = False
            '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_OffFormularyDrugs = False

            '    gblnFormularyAlertnativesNRDrgs = False
            '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_NRDrugs = False
            'Else
            '    If IsNothing(regKey.GetValue(FormularyAlertnativesAllDrgs)) = True Then
            '        regKey.SetValue(FormularyAlertnativesAllDrgs, False)
            '    Else
            '        regKey.SetValue(FormularyAlertnativesAllDrgs, False)
            '    End If
            '    gblnFormularyAlertnativesAllDrgs = False
            '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = False
            'End If
            ''Formulary Alertnatives AllDrgs

            'Formulary Alertnatives AllDrgs
            If chkFormularyAlertnativesAllDrgs.Checked = True Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(FormularyAlertnativesAllDrgs)) = True Then
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesAllDrgs, True)
                Else
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesAllDrgs, True)
                End If
                gblnFormularyAlertnativesAllDrgs = True
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = True

                gblnFormularyAlertnativesOffFormularyDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_OffFormularyDrugs = False

                gblnFormularyAlertnativesNRDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_NRDrugs = False
            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(FormularyAlertnativesAllDrgs)) = True Then
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesAllDrgs, False)
                Else
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesAllDrgs, False)
                End If
                gblnFormularyAlertnativesAllDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = False
            End If
            'Formulary Alertnatives AllDrgs

            'Formulary Alertnatives OffFormulary Drgs
            'If chkFormularyAlertnativesOffFormularyDrgs.Checked = True Then
            '    If IsNothing(regKey.GetValue(FormularyAlertnativesOffFormularyDrgs)) = True Then
            '        regKey.SetValue(FormularyAlertnativesOffFormularyDrgs, True)
            '    Else
            '        regKey.SetValue(FormularyAlertnativesOffFormularyDrgs, True)
            '    End If
            '    gblnFormularyAlertnativesOffFormularyDrgs = True
            '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_OffFormularyDrugs = True

            '    gblnFormularyAlertnativesAllDrgs = False
            '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = False
            'Else
            '    If IsNothing(regKey.GetValue(FormularyAlertnativesOffFormularyDrgs)) = True Then
            '        regKey.SetValue(FormularyAlertnativesOffFormularyDrgs, False)
            '    Else
            '        regKey.SetValue(FormularyAlertnativesOffFormularyDrgs, False)
            '    End If
            '    gblnFormularyAlertnativesOffFormularyDrgs = False
            '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_OffFormularyDrugs = False
            'End If

            If chkFormularyAlertnativesOffFormularyDrgs.Checked = True Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(FormularyAlertnativesOffFormularyDrgs)) = True Then
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesOffFormularyDrgs, True)
                Else
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesOffFormularyDrgs, True)
                End If
                gblnFormularyAlertnativesOffFormularyDrgs = True
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_OffFormularyDrugs = True

                gblnFormularyAlertnativesAllDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = False
            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(FormularyAlertnativesOffFormularyDrgs)) = True Then
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesOffFormularyDrgs, False)
                Else
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesOffFormularyDrgs, False)
                End If
                gblnFormularyAlertnativesOffFormularyDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_OffFormularyDrugs = False
            End If
            'Formulary Alertnatives OffFormulary Drgs



            'Formulary Alertnatives NR Drgs

            'If chkFormularyAlertnativesNRDrgs.Checked = True Then
            '    If IsNothing(regKey.GetValue(FormularyAlertnativesNRDrgs)) = True Then
            '        regKey.SetValue(FormularyAlertnativesNRDrgs, True)
            '    Else
            '        regKey.SetValue(FormularyAlertnativesNRDrgs, True)
            '    End If
            '    gblnFormularyAlertnativesNRDrgs = True
            '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_NRDrugs = True

            '    gblnFormularyAlertnativesAllDrgs = False
            '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = False
            'Else
            '    If IsNothing(regKey.GetValue(FormularyAlertnativesNRDrgs)) = True Then
            '        regKey.SetValue(FormularyAlertnativesNRDrgs, False)
            '    Else
            '        regKey.SetValue(FormularyAlertnativesNRDrgs, False)
            '    End If
            '    gblnFormularyAlertnativesNRDrgs = False
            '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_NRDrugs = False

            'End If

            If chkFormularyAlertnativesNRDrgs.Checked = True Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(FormularyAlertnativesNRDrgs)) = True Then
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesNRDrgs, True)
                Else
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesNRDrgs, True)
                End If
                gblnFormularyAlertnativesNRDrgs = True
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_NRDrugs = True

                gblnFormularyAlertnativesAllDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = False
            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(FormularyAlertnativesNRDrgs)) = True Then
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesNRDrgs, False)
                Else
                    gloRegistrySetting.SetRegistryValue(FormularyAlertnativesNRDrgs, False)
                End If
                gblnFormularyAlertnativesNRDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_NRDrugs = False

            End If
            'Formulary Alertnatives NR Drgs

            'Show NdC in alternatives grid
            If chkNDCInAlternativeGrid.Checked = True Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(ShowNDCInAlternatives)) = True Then
                    gloRegistrySetting.SetRegistryValue(ShowNDCInAlternatives, True)
                Else
                    gloRegistrySetting.SetRegistryValue(ShowNDCInAlternatives, True)
                End If
                gblnShowNDCInAlternatives = True
            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(ShowNDCInAlternatives)) = True Then
                    gloRegistrySetting.SetRegistryValue(ShowNDCInAlternatives, False)
                Else
                    gloRegistrySetting.SetRegistryValue(ShowNDCInAlternatives, False)
                End If
                gblnShowNDCInAlternatives = False
            End If
            '-----x---


            'Show Off formulary alternatives
            If chkShowOffFormularyAlternatives.Checked = True Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(ShowOffformularyalternatives)) = True Then
                    gloRegistrySetting.SetRegistryValue(ShowOffformularyalternatives, True)
                Else
                    gloRegistrySetting.SetRegistryValue(ShowOffformularyalternatives, True)
                End If
                gblnShowOffformularyalternatives = True

                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyShowOFFFormulary_Alt = True

            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(ShowOffformularyalternatives)) = True Then
                    gloRegistrySetting.SetRegistryValue(ShowOffformularyalternatives, False)
                Else
                    gloRegistrySetting.SetRegistryValue(ShowOffformularyalternatives, False)
                End If
                gblnShowOffformularyalternatives = False

                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyShowOFFFormulary_Alt = False

            End If
            '-----x---


            'Show NDC in Medication History
            If chkShowNDCInMedication.Checked = True Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(ShowNDCInMedicationHistory)) = True Then
                    gloRegistrySetting.SetRegistryValue(ShowNDCInMedicationHistory, True)
                Else
                    gloRegistrySetting.SetRegistryValue(ShowNDCInMedicationHistory, True)
                End If
                gblnShowNDCInMedicationHistory = True

                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnShowNDCInMedication_History = True

            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(ShowNDCInMedicationHistory)) = True Then
                    gloRegistrySetting.SetRegistryValue(ShowNDCInMedicationHistory, False)
                Else
                    gloRegistrySetting.SetRegistryValue(ShowNDCInMedicationHistory, False)
                End If
                gblnShowNDCInMedicationHistory = False

                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnShowNDCInMedication_History = False

            End If
            '-----x---



            'If optTopLeft.Checked = True Then
            '    genmResultsBoxPosition = mdlVoice.enmResultsBoxPosition.TopLeft
            '    regKey.SetValue("ResultBoxPosition", "TopLeft")
            'ElseIf optTopRight.Checked = True Then
            '    genmResultsBoxPosition = mdlVoice.enmResultsBoxPosition.TopRight
            '    regKey.SetValue("ResultBoxPosition", "TopRight")
            'ElseIf optBottomLeft.Checked = True Then
            '    genmResultsBoxPosition = mdlVoice.enmResultsBoxPosition.BottomLeft
            '    regKey.SetValue("ResultBoxPosition", "BottomLeft")
            'Else
            '    genmResultsBoxPosition = mdlVoice.enmResultsBoxPosition.BottomRight
            '    regKey.SetValue("ResultBoxPosition", "BottomRight")
            'End If

            'To Save Show NDC in Alternative grid Settings
            'SaveNDCAlternativeGridSetting()
            '-----




            If optTopLeft.Checked = True Then
                genmResultsBoxPosition = mdlVoice.enmResultsBoxPosition.TopLeft
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrResBoxPos, gloRegistrySetting.gstrtoplft)
            ElseIf optTopRight.Checked = True Then
                genmResultsBoxPosition = mdlVoice.enmResultsBoxPosition.TopRight
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrResBoxPos, gloRegistrySetting.gstrtoprght)
            ElseIf optBottomLeft.Checked = True Then
                genmResultsBoxPosition = mdlVoice.enmResultsBoxPosition.BottomLeft
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrResBoxPos, gloRegistrySetting.gstrbtmlft)
            Else
                genmResultsBoxPosition = mdlVoice.enmResultsBoxPosition.BottomRight
                gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrResBoxPos, gloRegistrySetting.gstrbtmrght)
            End If

            'color code setting 

            'color code setting 

            'Check for FAX Settings
            If Trim(cmbFAXPrinter.Text) <> "" Then
                gstrFAXPrinterName = cmbFAXPrinter.Text
                'regKey.SetValue("FAXPrinterName", gstrFAXPrinterName)
                gloRegistrySetting.SetRegistryValue("FAXPrinterName", gstrFAXPrinterName)
            End If
            If Trim(txtFAXOutputDirectory.Text) <> "" Then
                gstrFAXOutputDirectory = txtFAXOutputDirectory.Text
                'regKey.SetValue("FAXOutputDirectory", gstrFAXOutputDirectory)
                gloRegistrySetting.SetRegistryValue("FAXOutputDirectory", gstrFAXOutputDirectory)
                'sarika 29th nov 07
            ElseIf Trim(cmbFAXPrinter.Text) <> "" Then
                MessageBox.Show("Please select the fax output directory for sending faxes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                gloRegistrySetting.CloseRegistryKey()
                Exit Sub
                '----------- --------------
            End If


            '' CR00000126 : FAX for Terminal Server
            '' Save FAXDownloadDirectory in registry, new setting added for Terminal server case
            If (gloSettings.gloRegistrySetting.IsServerOS) Then
                If Trim(txtFaxDownloadPath.Text) <> "" Then
                    gstrFAXReceivedDirectoryTS = txtFaxDownloadPath.Text
                    gloRegistrySetting.SetRegistryValue("FAXDownloadDirectory", gstrFAXReceivedDirectoryTS)
                End If
            End If

            'Check FAX Printers necessary settings are set or not
            gblnFAXPrinterSettingsSet = isPrinterSettingsSet()


            'Set the FAX Printer Settings - i.e. FAX Printer Name, FAX  Output Directory
            ' SetFAXPrinterDefaultSettings()
            Try
                MainMenu.SetFAXPrinterDefaultSettings1()
            Catch ex As Exception
                MessageBox.Show("Error while doing the settings. " & ex.ToString, "gloEMR Settings", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


            gblnSameCoverPageForAllReferrals = chkHandleFAXIssue.Checked
            'regKey.SetValue("SameCoverPageForAllReferrals", gblnSameCoverPageForAllReferrals)
            gloRegistrySetting.SetRegistryValue("SameCoverPageForAllReferrals", gblnSameCoverPageForAllReferrals)

            'sarika 3rd july 07
            gblnBdayReminder = chkBdayReminder.Checked
            gnBDayReminderDays = numBdayReminder.Value

            'If gblnBdayReminder = True Then
            '    regKey.SetValue("ShowBdayReminder", "1")
            'Else
            '    regKey.SetValue("ShowBdayReminder", "0")
            'End If

            'regKey.SetValue("BdayReminderDays", gnBDayReminderDays)

            If gblnBdayReminder = True Then
                gloRegistrySetting.SetRegistryValue("ShowBdayReminder", "1")
            Else
                gloRegistrySetting.SetRegistryValue("ShowBdayReminder", "0")
            End If

            gloRegistrySetting.SetRegistryValue("BdayReminderDays", gnBDayReminderDays)


            '----------------For Rx drug button 
            If cmbRxDrugBtn.Text <> "" Then
                If cmbRxDrugBtn.Text = "All Drugs" Then
                    gnDrugListButton = DrugListDefault.AllDrugs
                ElseIf cmbRxDrugBtn.Text = "Practice Favorites" Then 'change caption from Clinical drugs to Practice Favorites. email dated 30 sep 2009 "Issue 54 Information"
                    gnDrugListButton = DrugListDefault.PracticeFavorites
                ElseIf cmbRxDrugBtn.Text = "Provider Specific Drugs" Then
                    gnDrugListButton = DrugListDefault.ProviderDrugs
                Else
                    gnDrugListButton = DrugListDefault.FrequentlyUsed
                End If

                'regKey.SetValue("RxSelectedDrugButton", gnRxDrugButton)
                gloRegistrySetting.SetRegistryValue("RxSelectedDrugButton", gnDrugListButton)
            End If
            '----------------For Rx drug button 

            ''commented for Bugno:4441 
            ' ''----------------For Mx drug button 
            ''If cmbMxDrugBtn.Text <> "" Then
            ''    If cmbMxDrugBtn.Text = "Practice Favorites" Then 'change caption from Clinical drugs to Practice Favorites. email dated 30 sep 2009 "Issue 54 Information"
            ''        gnMedDrugButton = MxSelectedbutton.PracticeFavorites
            ''    ElseIf cmbMxDrugBtn.Text = "Provider Specific Drugs" Then
            ''        gnMedDrugButton = MxSelectedbutton.ProviderDrugs
            ''    Else
            ''        gnMedDrugButton = MxSelectedbutton.AllDrugs
            ''    End If
            ''    regKey.SetValue("MxSelectedDrugButton", gnMedDrugButton)
            ''End If
            ' ''----------------For Mx drug button 

            '' *******
            ' ''HL& Settings Code was Commented by Pramod 21092007 
            ' ''Value Take from database and fileds are read only
            '' But For Some Days we Have to Take Values From Registory as we have not Created DB Table for it, so Again it was Uncommented
            '' *******






            ''code below commented on 23-nov-2012 to hide hl7outbound setting

            'gblnGenerateOutboundMsg = chkOutbound.Checked


            'If gblnGenerateOutboundMsg = True Then
            '    appSettings("GenerateHL7Message") = True   ' Added by kanchan on 20100102 for appointment settings in PM
            '    gblnSaveandClose = chkSaveandClose.Checked
            '    gblnSaveandFinish = chkSaveandFinish.Checked
            '    'Code Start-Added by kanchan on 20100521 for HL7 setting for immunization
            '    gblnSendImmunization = chkHL7Immunization.Checked
            '    'Code End-Added by kanchan on 20100521 for HL7 setting for immunization

            '    ''Added by Abhijeet on 20110919
            '    gblnSendHL7Appointment = chkHL7Appointment.Checked
            '    ''End of changes by Abhijeet on 20110919

            '    '''''''regKey.SetValue("GenerateOutboundMessage", "True")
            '    gloRegistrySetting.SetRegistryValue("GenerateOutboundMessage", "True")
            '    '' Setting For HL7/ Genius

            '    'Start-code added by kanchan on 20091202 for genius & Hl7 work simultaneously as case 3176
            '    If rbHL7.Checked = True Then
            '        gblnSendChargesToHL7 = True
            '        gblnAddModPatient = chkPatientReg.Checked

            '        '' by Abhijeet on Date 20100614 
            '        '' set the glolab varible for setting of HL7 Outbound variable
            '        gloEmdeonCommon.mdlGeneral.gblnSendChargesToHL7 = True
            '        '' End of changes by Abhijeet for setting of HL7 Outbound variable

            '        '''''''''''''''''' regKey.SetValue("SendChargesToHL7", "True")
            '        gloRegistrySetting.SetRegistryValue("SendChargesToHL7", "True")
            '    Else
            '        gblnSendChargesToHL7 = False
            '        gblnAddModPatient = chkPatientReg.Checked

            '        '' by Abhijeet on Date 20100614 
            '        '' set the glolab varible for setting of HL7 Outbound variable
            '        gloEmdeonCommon.mdlGeneral.gblnSendChargesToHL7 = False
            '        '' End of changes by Abhijeet for setting of gloLab HL7 Outbound variable

            '        '''''''''''''''''regKey.SetValue("SendChargesToHL7", "False")
            '        gloRegistrySetting.SetRegistryValue("SendChargesToHL7", "False")
            '    End If
            '    If rbGenius.Checked = True Then
            '        gblnSendChargesToGenius = True
            '        '''''''''''''''''regKey.SetValue("SendChargesToGenius", "True")
            '        gloRegistrySetting.SetRegistryValue("SendChargesToGenius", "True")
            '        ''''''''''''''''''gblnSendChargesToHL7 = False
            '        ''''''''''''''''chkPatientReg.Checked = False
            '        ''''''''''''''''''''gblnAddModPatient = False
            '    Else
            '        gblnSendChargesToGenius = False
            '        '''''''''''''''''regKey.SetValue("SendChargesToGenius", "False")
            '        gloRegistrySetting.SetRegistryValue("SendChargesToGenius", "False")
            '    End If
            '    'End-code added by kanchan on 20091202 for genius & Hl7 work simultaneously as case 3176
            '    'SendOnSaveandClose

            '    ''''''''''''regKey.SetValue("SendOnSaveandClose", gblnSaveandClose)
            '    ''SendOnSaveandFinish
            '    '''''''''''''regKey.SetValue("SendOnSaveandFinish", gblnSaveandFinish)
            '    ''''''''''''regKey.SetValue("SendPatientDetails", gblnAddModPatient)

            '    gloRegistrySetting.SetRegistryValue("SendOnSaveandClose", gblnSaveandClose)
            '    'SendOnSaveandFinish
            '    gloRegistrySetting.SetRegistryValue("SendOnSaveandFinish", gblnSaveandFinish)
            '    gloRegistrySetting.SetRegistryValue("SendPatientDetails", gblnAddModPatient)
            '    'Code Start-Added by kanchan on 20100521 for HL7 setting for immunization
            '    gloRegistrySetting.SetRegistryValue("SendImmunizationDetails", gblnSendImmunization)
            '    '''''''''''''''regKey.SetValue("SendImmunizationDetails", gblnSendImmunization)

            '    ''Added by Abhijeet on 20110919
            '    gloRegistrySetting.SetRegistryValue("SendAppointmentDetails", gblnSendHL7Appointment)
            '    ''End of code by Abhijeet on 20110919
            'Else
            '    ''''''''''regKey.SetValue("GenerateOutboundMessage", "False")
            '    ''''''''''''regKey.SetValue("SendChargesToHL7", "False")
            '    '''''''''''regKey.SetValue("SendChargesToGenius", "False")
            '    ''''''''''''' 'regKey.SetValue("SendOnSaveandClose", "False")
            '    '''''''''''''''regKey.SetValue("SendOnSaveandFinish", "False")
            '    '''''''''''''''' 'regKey.SetValue("SendPatientDetails", "False")
            '    gloRegistrySetting.SetRegistryValue("GenerateOutboundMessage", "False")
            '    gloRegistrySetting.SetRegistryValue("SendChargesToHL7", "False")
            '    gloRegistrySetting.SetRegistryValue("SendChargesToGenius", "False")
            '    gloRegistrySetting.SetRegistryValue("SendOnSaveandClose", "False")
            '    gloRegistrySetting.SetRegistryValue("SendOnSaveandFinish", "False")
            '    gloRegistrySetting.SetRegistryValue("SendPatientDetails", "False")
            '    'Code Start-Added by kanchan on 20100521 for HL7 setting for immunization
            '    gloRegistrySetting.SetRegistryValue("SendImmunizationDetails", "False")
            '    gblnSendImmunization = False
            '    'Code End-Added by kanchan on 20100521 for HL7 setting for immunization

            '    ''Added by Abhijeet on 20110919
            '    gloRegistrySetting.SetRegistryValue("SendAppointmentDetails", "False")
            '    gblnSendHL7Appointment = False
            '    ''End of code by Abhijeet on 20110919

            '    gblnSaveandClose = False
            '    gblnSaveandFinish = False
            '    gblnAddModPatient = False
            '    gblnSendChargesToHL7 = False

            '    '' by Abhijeet on Date 20100614 
            '    '' set the glolab varible for setting of HL7 Outbound variable
            '    gloEmdeonCommon.mdlGeneral.gblnSendChargesToHL7 = False
            '    '' End of changes by Abhijeet for setting of HL7 Outbound variable

            '    gblnSendChargesToGenius = False

            'End If

            ''code above commented on 23-nov-2012 to hide hl7outbound setting









            ''''''''END
            '----------------------


            'Patient Synopsis Tab Count
            gnPatientSynopsisTabCount = numPatientSypnosisTabCount.Value 'txtPatientSypnosisTabCount.Text

            'Shubhangi 20091003
            'Use Radio button to Clear search text box
            If chkResetSearch.Checked = True Then
                gblnResetSearchTextBox = True
                gloSettings.gloEMRSettings._gblnResetSearchTextBox = True
                'regKey.SetValue("ResetSearch", "True")
                gloRegistrySetting.SetRegistryValue("ResetSearch", "True")
            Else
                gblnResetSearchTextBox = False
                gloSettings.gloEMRSettings._gblnResetSearchTextBox = False
                'regKey.SetValue("ResetSearch", "False")
                gloRegistrySetting.SetRegistryValue("ResetSearch", "False")
            End If
            gloUserControlLibrary.gloUC_TreeView.blnResetSearch = gblnResetSearchTextBox

            gloRegistrySetting.SetRegistryValue("PatientSynopsisTabCount", gnPatientSynopsisTabCount)

            DMSRootPath = txtDMSPath.Text.Trim
            gloRegistrySetting.SetRegistryValue("DMSPath", DMSRootPath)


            VMSRootPath = txtVMSPath.Text.Trim
            'regKey.SetValue("VMSPath", VMSRootPath)
            gloRegistrySetting.SetRegistryValue("VMSPath", VMSRootPath)

            ''Sandip Darade 20090824
            '  If Trim(txtServerPath.Text) <> "" Then
            gstrServerPath = txtServerPath.Text
            ' regKey.SetValue("ServerPath", gstrServerPath)
            gloRegistrySetting.SetRegistryValue("ServerPath", gstrServerPath)
            'End If
            'FAX Cover Page Settings
            If chkCoverPage.Checked = True Then
                gblnFAXCoverPage = True
                'regKey.SetValue("FAXCoverPage", "1")
                gloRegistrySetting.SetRegistryValue("FAXCoverPage", "1")
            Else
                gblnFAXCoverPage = False
                'regKey.SetValue("FAXCoverPage", "0")
                gloRegistrySetting.SetRegistryValue("FAXCoverPage", "0")
            End If

            'Set Lock Time
            gLockTime = numLockTime.Value
            gloRegistrySetting.SetRegistryValue("LockTime", numLockTime.Value)
            gblnAutoLockEnable = chkAutoApplicationLock.Checked

            'regKey.SetValue("LockTime", gLockTime)
            gblnAutoLockEnable = chkAutoApplicationLock.Checked
            If gblnAutoLockEnable = True Then
                'regKey.SetValue("AutoLockEnable", "1")
                gloRegistrySetting.SetRegistryValue("AutoLockEnable", "1")
            Else
                'regKey.SetValue("AutoLockEnable", "0")
                gloRegistrySetting.SetRegistryValue("AutoLockEnable", "0")
            End If

            gMessageUpdateTime = num_MessagesRefreshTime.Value
            gloRegistrySetting.SetRegistryValue("MessageRefreshTime", gMessageUpdateTime)
 
            Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)
            Dim _DemographicsSettingValue As String = ""
            For i As Integer = 0 To trvDemographics.Nodes.Count - 1
                If trvDemographics.Nodes(i).Checked = True Then
                    _DemographicsSettingValue += "," & trvDemographics.Nodes(i).Tag.ToString().Trim()
                End If
            Next
            If _DemographicsSettingValue.Trim() <> "" Then
                _DemographicsSettingValue = _DemographicsSettingValue.Substring(1, _DemographicsSettingValue.Length - 1)
            End If
            ogloSettings.AddSetting("Patient Demographics", _DemographicsSettingValue, gnClinicID, gnLoginID, gloSettings.SettingFlag.User)

            If IsNothing(gloRegistrySetting.GetRegistryValue(DrugToDiseaseInteractionAlert)) OrElse Convert.ToString(gloRegistrySetting.GetRegistryValue(DrugToDiseaseInteractionAlert)) <> chkDrugDiseaseInteraction.Checked Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "Drug to Disease interaction alert turned " + IIf(chkDrugDiseaseInteraction.Checked, "On", "Off"), gloAuditTrail.ActivityOutCome.Success)
            End If

            If chkDrugDiseaseInteraction.Checked Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(DrugToDiseaseInteractionAlert)) = True Then
                    gloRegistrySetting.SetRegistryValue(DrugToDiseaseInteractionAlert, True)
                Else
                    gloRegistrySetting.SetRegistryValue(DrugToDiseaseInteractionAlert, True)
                End If
                gblnDrugToDiseaseInteractionAlert = True
            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(DrugToDiseaseInteractionAlert)) = True Then
                    gloRegistrySetting.SetRegistryValue(DrugToDiseaseInteractionAlert, False)
                Else
                    gloRegistrySetting.SetRegistryValue(DrugToDiseaseInteractionAlert, False)
                End If
                gblnDrugToDiseaseInteractionAlert = False
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DrugToFoodInteractionAlert)) = True OrElse Convert.ToString(gloRegistrySetting.GetRegistryValue(DrugToFoodInteractionAlert)) <> chkDrugFoodInteraction.Checked Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "Drug to Food interaction alert turned " + IIf(chkDrugFoodInteraction.Checked, "On", "Off"), gloAuditTrail.ActivityOutCome.Success)
            End If

            If chkDrugFoodInteraction.Checked Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(DrugToFoodInteractionAlert)) = True Then
                    gloRegistrySetting.SetRegistryValue(DrugToFoodInteractionAlert, True)
                Else
                    gloRegistrySetting.SetRegistryValue(DrugToFoodInteractionAlert, True)
                End If
                gblnDrugToFoodInteractionAlert = True
            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(DrugToFoodInteractionAlert)) = True Then
                    gloRegistrySetting.SetRegistryValue(DrugToFoodInteractionAlert, False)
                Else
                    gloRegistrySetting.SetRegistryValue(DrugToFoodInteractionAlert, False)
                End If
                gblnDrugToFoodInteractionAlert = False

            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DuplicateTherapyInteractionAlert)) OrElse Convert.ToString(gloRegistrySetting.GetRegistryValue(DuplicateTherapyInteractionAlert)) <> chkDuplicateTherapy.Checked Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "Duplicate therapy alert turned " + IIf(chkDuplicateTherapy.Checked, "On", "Off"), gloAuditTrail.ActivityOutCome.Success)
            End If

            If chkDuplicateTherapy.Checked Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(DuplicateTherapyInteractionAlert)) = True Then
                    gloRegistrySetting.SetRegistryValue(DuplicateTherapyInteractionAlert, True)
                Else
                    gloRegistrySetting.SetRegistryValue(DuplicateTherapyInteractionAlert, True)
                End If
                gblnDuplicateTherapyInteractionAlert = True
            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(DuplicateTherapyInteractionAlert)) = True Then
                    gloRegistrySetting.SetRegistryValue(DuplicateTherapyInteractionAlert, False)
                Else
                    gloRegistrySetting.SetRegistryValue(DuplicateTherapyInteractionAlert, False)
                End If
                gblnDuplicateTherapyInteractionAlert = False
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(AdverseDrugEffectAlert)) OrElse Convert.ToString(gloRegistrySetting.GetRegistryValue(AdverseDrugEffectAlert)) <> chkAdverseDrugEffect.Checked Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "Adverse drug effect alert turned " + IIf(chkAdverseDrugEffect.Checked, "On", "Off"), gloAuditTrail.ActivityOutCome.Success)
            End If

            If chkAdverseDrugEffect.Checked Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(AdverseDrugEffectAlert)) = True Then
                    gloRegistrySetting.SetRegistryValue(AdverseDrugEffectAlert, True)
                Else
                    gloRegistrySetting.SetRegistryValue(AdverseDrugEffectAlert, True)
                End If
                gblnAdverseDrugEffectAlert = True
            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(AdverseDrugEffectAlert)) = True Then
                    gloRegistrySetting.SetRegistryValue(AdverseDrugEffectAlert, False)
                Else
                    gloRegistrySetting.SetRegistryValue(AdverseDrugEffectAlert, False)
                End If
                gblnAdverseDrugEffectAlert = False
            End If

            Dim sADESeverityLevel As String = ""
            If Not IsNothing(gloRegistrySetting.GetRegistryValue(ADESeverityLevel)) Then
                sADESeverityLevel = Convert.ToString(gloRegistrySetting.GetRegistryValue(ADESeverityLevel))
            End If

            If sADESeverityLevel <> cmbADE.Text Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "ADE Severity level changed to " + cmbADE.Text + " from " + sADESeverityLevel, gloAuditTrail.ActivityOutCome.Success)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(ADESeverityLevel)) = True Then
                gloRegistrySetting.SetRegistryValue(ADESeverityLevel, cmbADE.Text)
            Else
                gloRegistrySetting.SetRegistryValue(ADESeverityLevel, cmbADE.Text)
            End If

            Dim sADEOnsetLevel As String = ""
            If Not IsNothing(gloRegistrySetting.GetRegistryValue(ADEOnsetLevel)) Then
                sADEOnsetLevel = Convert.ToString(gloRegistrySetting.GetRegistryValue(ADEOnsetLevel))
            End If

            If sADEOnsetLevel <> cmbADEOnset.Text Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "ADE Onset level changed to " + cmbADEOnset.Text + " from " + sADEOnsetLevel, gloAuditTrail.ActivityOutCome.Success)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(ADEOnsetLevel)) = True Then
                gloRegistrySetting.SetRegistryValue(ADEOnsetLevel, cmbADEOnset.Text)
            Else
                gloRegistrySetting.SetRegistryValue(ADEOnsetLevel, cmbADEOnset.Text)
            End If

            Dim sDISeverityLevel As String = ""
            If Not IsNothing(gloRegistrySetting.GetRegistryValue(DISeverityLevel)) Then
                sDISeverityLevel = Convert.ToString(gloRegistrySetting.GetRegistryValue(DISeverityLevel))
            End If

            If sDISeverityLevel <> cmbDI.Text Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "DI Severity level changed to " + cmbDI.Text + " from " + sDISeverityLevel, gloAuditTrail.ActivityOutCome.Success)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DISeverityLevel)) = True Then
                gloRegistrySetting.SetRegistryValue(DISeverityLevel, cmbDI.Text)
            Else
                gloRegistrySetting.SetRegistryValue(DISeverityLevel, cmbDI.Text)
            End If

            Dim sDFASeverityLevel As String = ""
            If Not IsNothing(gloRegistrySetting.GetRegistryValue(DFASeverityLevel)) Then
                sDFASeverityLevel = Convert.ToString(gloRegistrySetting.GetRegistryValue(DFASeverityLevel))
            End If

            If Convert.ToString(gloRegistrySetting.GetRegistryValue(DFASeverityLevel)) <> cmbDFA.Text Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "DFA Severity level changed to " + cmbDFA.Text + " from " + sDFASeverityLevel, gloAuditTrail.ActivityOutCome.Success)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DFASeverityLevel)) = True Then
                gloRegistrySetting.SetRegistryValue(DFASeverityLevel, cmbDFA.Text)
            Else
                gloRegistrySetting.SetRegistryValue(DFASeverityLevel, cmbDFA.Text)
            End If

            Dim sDIDocLevel As String = ""
            If Not IsNothing(gloRegistrySetting.GetRegistryValue(DIDocLevel)) Then
                sDIDocLevel = Convert.ToString(gloRegistrySetting.GetRegistryValue(DIDocLevel))
            End If

            If sDIDocLevel <> cmbDIDoc.Text Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "DIDoc level changed to " + cmbDIDoc.Text + " from " + sDIDocLevel, gloAuditTrail.ActivityOutCome.Success)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DIDocLevel)) = True Then
                gloRegistrySetting.SetRegistryValue(DIDocLevel, cmbDIDoc.Text)
            Else
                gloRegistrySetting.SetRegistryValue(DIDocLevel, cmbDIDoc.Text)
            End If

            Dim sDFADocLevel As String = ""
            If Not IsNothing(gloRegistrySetting.GetRegistryValue(DFADocLevel)) Then
                sDFADocLevel = Convert.ToString(gloRegistrySetting.GetRegistryValue(DFADocLevel))
            End If

            If sDFADocLevel <> cmbDFADoc.Text Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "DFADoc Level changed to " + cmbDFADoc.Text + " from " + sDFADocLevel, gloAuditTrail.ActivityOutCome.Success)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DFADocLevel)) = True Then
                gloRegistrySetting.SetRegistryValue(DFADocLevel, cmbDFADoc.Text)
            Else
                gloRegistrySetting.SetRegistryValue(DFADocLevel, cmbDFADoc.Text)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(ShowDrugAlertMsg)) OrElse Convert.ToString(gloRegistrySetting.GetRegistryValue(ShowDrugAlertMsg)) <> chkDrugAlert.Checked Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, "Show Override Alert switched " + IIf(chkDrugAlert.Checked, "On", "Off"), gloAuditTrail.ActivityOutCome.Success)
            End If

            If chkDrugAlert.Checked = True Then
                If IsNothing(gloRegistrySetting.GetRegistryValue(ShowDrugAlertMsg)) = True Then
                    gloRegistrySetting.SetRegistryValue(ShowDrugAlertMsg, True)
                Else
                    gloRegistrySetting.SetRegistryValue(ShowDrugAlertMsg, True)
                End If
                gblnDrugAlertMsg = True
            Else
                If IsNothing(gloRegistrySetting.GetRegistryValue(ShowDrugAlertMsg)) = True Then
                    gloRegistrySetting.SetRegistryValue(ShowDrugAlertMsg, False)
                Else
                    gloRegistrySetting.SetRegistryValue(ShowDrugAlertMsg, False)
                End If
                gblnDrugAlertMsg = False
            End If

            If chksethighlightcolr.Checked = True Then
                gblnWordColorHighlight = True
                'If IsNothing(regKey.GetValue("HighLightWord")) = True Then
                '    regKey.SetValue("HighLightWord", True)
                'Else
                '    regKey.SetValue("HighLightWord", True)
                'End If
                If IsNothing(gloRegistrySetting.GetRegistryValue("HighLightWord")) = True Then
                    gloRegistrySetting.SetRegistryValue("HighLightWord", True)
                Else
                    gloRegistrySetting.SetRegistryValue("HighLightWord", True)
                End If
                If IsNothing(cmbHighlight.SelectedValue) = True Then
                    gblnWordBackColor = 7  '' Default Set Yellow Colour
                Else
                    gblnWordBackColor = cmbHighlight.SelectedValue
                End If

                'If IsNothing(regKey.GetValue("HighlightedColor")) = True Then
                '    regKey.SetValue("HighlightedColor", gblnWordBackColor)
                'Else
                '    regKey.SetValue("HighlightedColor", gblnWordBackColor)
                'End If
                If IsNothing(gloRegistrySetting.GetRegistryValue("HighlightedColor")) = True Then
                    gloRegistrySetting.SetRegistryValue("HighlightedColor", gblnWordBackColor)
                Else
                    gloRegistrySetting.SetRegistryValue("HighlightedColor", gblnWordBackColor)
                End If
            Else
                gblnWordColorHighlight = False
                'If IsNothing(regKey.GetValue("HighLightWord")) = True Then
                '    regKey.SetValue("HighLightWord", False)
                'Else
                '    regKey.SetValue("HighLightWord", False)
                'End If
                If IsNothing(gloRegistrySetting.GetRegistryValue("HighLightWord")) = True Then
                    gloRegistrySetting.SetRegistryValue("HighLightWord", False)
                Else
                    gloRegistrySetting.SetRegistryValue("HighLightWord", False)
                End If

                'gblnWordBackColor = cmbHighlight.SelectedValue
                'If IsNothing(regKey.GetValue("HighlightedColor")) = True Then
                '    regKey.SetValue("HighlightedColor", cmbHighlight.SelectedValue)
                'Else
                '    regKey.SetValue("HighlightedColor", cmbHighlight.SelectedValue)
                'End If
            End If

            ExamNotesSelection(False)

            '' Settings for incuding Exam notes in Referral letters 
            'If IsNothing(regKey.GetValue("ExamNotesSelection")) = True Then
            '    regKey.SetValue("ExamNotesSelection", gblnExamSelection)
            'Else
            '    regKey.SetValue("ExamNotesSelection", gblnExamSelection)
            'End If
            If IsNothing(gloRegistrySetting.GetRegistryValue("ExamNotesSelection")) = True Then
                gloRegistrySetting.SetRegistryValue("ExamNotesSelection", gblnExamSelection)
            Else
                gloRegistrySetting.SetRegistryValue("ExamNotesSelection", gblnExamSelection)
            End If

            ''''Added by Pramod For SureScriptAlert Start 180220008
            gblnSurescriptAlert = chksureAlert.Checked
            gblnIsFaxEnabled = chkSurescriptFaxSettings.Checked
            gblnIsReferalNoteadd = chksecureSureScriptsetting.Checked
            gStrSurescriptAlertmin = cmbInterval.Text 'numAlertTime.Value
            If gblnSurescriptAlert = True Then
                'If IsNothing(regKey.GetValue(SureScriptAlert)) = True Then
                '    regKey.SetValue(SureScriptAlert, "1")
                'Else
                '    regKey.SetValue(SureScriptAlert, "1")
                'End If
                If IsNothing(gloRegistrySetting.GetRegistryValue(SureScriptAlert)) = True Then
                    gloRegistrySetting.SetRegistryValue(SureScriptAlert, "1")
                Else
                    gloRegistrySetting.SetRegistryValue(SureScriptAlert, "1")
                End If
            Else
                'If IsNothing(regKey.GetValue(SureScriptAlert)) = True Then
                '    regKey.SetValue(SureScriptAlert, "0")
                'Else
                '    regKey.SetValue(SureScriptAlert, "0")
                'End If
                If IsNothing(gloRegistrySetting.GetRegistryValue(SureScriptAlert)) = True Then
                    gloRegistrySetting.SetRegistryValue(SureScriptAlert, "0")
                Else
                    gloRegistrySetting.SetRegistryValue(SureScriptAlert, "0")
                End If
            End If

            If gblnIsReferalNoteadd = True Then

                If IsNothing(gloRegistrySetting.GetRegistryValue(AddReferralsNote)) = True Then
                    gloRegistrySetting.SetRegistryValue(AddReferralsNote, "1")
                Else
                    gloRegistrySetting.SetRegistryValue(AddReferralsNote, "1")
                End If
            Else

                If IsNothing(gloRegistrySetting.GetRegistryValue(AddReferralsNote)) = True Then
                    gloRegistrySetting.SetRegistryValue(AddReferralsNote, "0")
                Else
                    gloRegistrySetting.SetRegistryValue(AddReferralsNote, "0")
                End If
            End If
            'If IsNothing(regKey.GetValue(SureScriptAlertTime)) = True Then
            '    regKey.SetValue(SureScriptAlertTime, gStrSurescriptAlertmin)
            'Else
            '    regKey.SetValue(SureScriptAlertTime, gStrSurescriptAlertmin)
            'End If
            If IsNothing(gloRegistrySetting.GetRegistryValue(SureScriptAlertTime)) = True Then
                gloRegistrySetting.SetRegistryValue(SureScriptAlertTime, gStrSurescriptAlertmin)
            Else
                gloRegistrySetting.SetRegistryValue(SureScriptAlertTime, gStrSurescriptAlertmin)
            End If
            If gblnIsFaxEnabled = True Then
                'If IsNothing(regKey.GetValue(SurescriptFaxSetting)) = True Then
                '    regKey.SetValue(SurescriptFaxSetting, "1")
                'Else
                '    regKey.SetValue(SurescriptFaxSetting, "1")
                'End If
                If IsNothing(gloRegistrySetting.GetRegistryValue(SurescriptFaxSetting)) = True Then
                    gloRegistrySetting.SetRegistryValue(SurescriptFaxSetting, "1")
                Else
                    gloRegistrySetting.SetRegistryValue(SurescriptFaxSetting, "1")
                End If
            Else
                'If IsNothing(regKey.GetValue(SurescriptFaxSetting)) = True Then
                '    regKey.SetValue(SurescriptFaxSetting, "0")
                'Else
                '    regKey.SetValue(SurescriptFaxSetting, "0")
                'End If
                If IsNothing(gloRegistrySetting.GetRegistryValue(SurescriptFaxSetting)) = True Then
                    gloRegistrySetting.SetRegistryValue(SurescriptFaxSetting, "0")
                Else
                    gloRegistrySetting.SetRegistryValue(SurescriptFaxSetting, "0")
                End If
            End If
            'If gblnSurescriptAlert = True And MainMenu.tmrsurescriptAlert.Enabled = False Then

            '    MainMenu.tmrsurescriptAlert.Enabled = True
            '    MainMenu.tmrsurescriptAlert.Interval = ((gnSurescriptAlertmin * 60) * 1000)
            'Else

            '    MainMenu.tmrsurescriptAlert.Interval = ((gnSurescriptAlertmin * 60) * 1000)

            'End If
            'If gblnSurescriptAlert = False Then
            '    MainMenu.tmrsurescriptAlert.Enabled = False
            'End If
            ''''Added by Pramod For SureScriptAlert End 180220008

            If rdo_IncludePageNo_Yes.Checked = True Then
                gblnPageNo = True
                'regKey.SetValue("PageNoSetting", "True")
                gloRegistrySetting.SetRegistryValue("PageNoSetting", "True")
            ElseIf rdo_IncludePageNo_No.Checked = True Then
                gblnPageNo = False
                'regKey.SetValue("PageNoSetting", "False")
                gloRegistrySetting.SetRegistryValue("PageNoSetting", "False")
            End If

            '' Use Default Printer 20091004
            If chkUseDefaultPrinter.Checked = True Then
                gblnUseDefaultPrinter = True

                '' by Abhijeet on Date 20100419 
                '' set the glolab varible for setting of default printer value
                gloEmdeonCommon.mdlGeneral.gblnIsDefaultPrinter = True
                '' End of changes by Abhijeet for default printer setting variable value

                'regKey.SetValue("UseDefaultPrinter", "1")
                gloRegistrySetting.SetRegistryValue("UseDefaultPrinter", "1")
            Else
                gblnUseDefaultPrinter = False
                '' by Abhijeet on Date 20100419 
                '' set the glolab varible for setting of default printer value
                gloEmdeonCommon.mdlGeneral.gblnIsDefaultPrinter = False
                '' End of changes by Abhijeet for default printer setting variable value


                'regKey.SetValue("UseDefaultPrinter", "0")
                gloRegistrySetting.SetRegistryValue("UseDefaultPrinter", "0")
            End If

            appSettings("UseDefaultPrinter") = Convert.ToString(gblnUseDefaultPrinter)


            '' Enable Local Print
            If chkEnableLocalPrinter.Checked = True Then
                gblnEnableLocalPrinter = True
                gloGlobal.gloTSPrint.isCopyPrint = True
                gloRegistrySetting.SetRegistryValue("EnableLocalPrinter", "1")
            Else
                gblnEnableLocalPrinter = False
                gloGlobal.gloTSPrint.isCopyPrint = False
                gloRegistrySetting.SetRegistryValue("EnableLocalPrinter", "0")
            End If

            '' Add Footer in Service
            If chkAddFooterService.Checked = True Then
                gblnAddFooterInService = True
                gloGlobal.gloTSPrint.AddFooterInService = True
                gloRegistrySetting.SetRegistryValue("AddFooterInService", "1")
            Else
                gblnAddFooterInService = False
                gloGlobal.gloTSPrint.AddFooterInService = False
                gloRegistrySetting.SetRegistryValue("AddFooterInService", "0")
            End If
            'Page Split Settings
            If cmbNoPagesSplit.Items.Count > 0 Then
                gloRegistrySetting.SetRegistryValue("NoOfPagesToSplit", cmbNoPagesSplit.SelectedItem)
                Dim result As Integer = 0
                Integer.TryParse(cmbNoPagesSplit.SelectedItem.ToString(), result)
                gloGlobal.gloTSPrint.NoOfPages = result
            End If
            'No of templates per job setting
            If cmbNoTemplatesJob.Items.Count > 0 Then
                gloRegistrySetting.SetRegistryValue("NoOfTemplatesPerJob", cmbNoTemplatesJob.SelectedItem)
                Dim result As Integer = 0
                Integer.TryParse(cmbNoTemplatesJob.SelectedItem.ToString(), result)
                gloGlobal.gloTSPrint.NoOfTemplatesPerJob = result
            End If
            '' Use EMF/PDF for word printing
            If rbPrintWordDocEMF.Checked = True Then
                gloGlobal.gloTSPrint.UseEMFForWord = True
                gloRegistrySetting.SetRegistryValue("UseEMFFile", "1")
            Else
                gloGlobal.gloTSPrint.UseEMFForWord = False
                gloRegistrySetting.SetRegistryValue("UseEMFFile", "0")
            End If
            '' Use EMF/PDF for SSRS printing
            If rbPrintSSRSReportEMF.Checked = True Then
                gloGlobal.gloTSPrint.UseEMFForSSRS = True
                gloRegistrySetting.SetRegistryValue("UseEMFFileSSRS", "1")
            Else
                gloGlobal.gloTSPrint.UseEMFForSSRS = False
                gloRegistrySetting.SetRegistryValue("UseEMFFileSSRS", "0")
            End If
            '' Use EMF/PDF for Claims printing
            If rbPrintClaimsEMF.Checked = True Then
                gloGlobal.gloTSPrint.UseEMFForClaims = True
                gloRegistrySetting.SetRegistryValue("UseEMFForClaims", "1")
            Else
                gloGlobal.gloTSPrint.UseEMFForClaims = False
                gloRegistrySetting.SetRegistryValue("UseEMFForClaims", "0")
            End If
            '' Use EMF/PDF for Images printing
            If rbPrintImagesEMF.Checked = True Then
                gloGlobal.gloTSPrint.UseEMFForImages = True
                gloRegistrySetting.SetRegistryValue("UseEMFForImages", "1")
            Else
                gloGlobal.gloTSPrint.UseEMFForImages = False
                gloRegistrySetting.SetRegistryValue("UseEMFForImages", "0")
            End If
            If chkZipMetadata.Checked = True Then
                gloGlobal.gloTSPrint.UseZippedMetadata = True
                gloRegistrySetting.SetRegistryValue("UseZippedMetadata", "1")
            Else
                gloGlobal.gloTSPrint.UseZippedMetadata = False
                gloRegistrySetting.SetRegistryValue("UseZippedMetadata", "0")
            End If

            ' sarika DICOM Settings 20090214
            ''Sandip Darade 20090824
            ''store no value if not entered
            '            If Trim(txtDICOMPath.Text) <> "" Then
            DICOMPath = txtDICOMPath.Text
            'regKey.SetValue("DICOMPath", DICOMPath)
            gloRegistrySetting.SetRegistryValue("DICOMPath", DICOMPath)
            'ElseIf Trim(cmbFAXPrinter.Text) <> "" Then
            '    MessageBox.Show("Please select the DICOM Path for saving DICOM files.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            '    Exit Sub
            '-------------------------
            'End If

            '----------
            ''Sandip Darade 20090303
            'Add Default Navigation panel setting 
            'regKey.SetValue("DefaultNavigation", Cmb_NavgtnPnl.Text)
            'regKey.Close()
            'regKey = Nothing
            gloRegistrySetting.SetRegistryValue("DefaultNavigation", Cmb_NavgtnPnl.Text)
            gloRegistrySetting.CloseRegistryKey()

            gblnVoiceSettingsChange = True

            gstrADESeverityLevel = cmbADE.Text
            gstrADEOnsetLevel = cmbADEOnset.Text
            gstrDISeverityLevel = cmbDI.Text
            gstrDFASeverityLevel = cmbDFA.Text
            gstrDIDocLevel = cmbDIDoc.Text
            gstrDFADocLevel = cmbDFADoc.Text


            Dim clsDashBoard As New clsDoctorsDashBoard
            clsDashBoard.Set_Clinic_Environment(objClinicEnv)

            clsDashBoard = Nothing

            If Not chkEnableRemoteScanner.Checked And Not chkEliminatePegasus.Checked Then
                '''' Save DMS Settings
                If (ValidateCardSize() = False) Then
                    ogloSettings.Dispose()
                    ogloSettings = Nothing
                    Exit Sub
                End If
            End If

            SaveDMSSettings()

            '------------------------Saket 20080602
            SavePatientSearchSettings()
            SaveExchangeServerSettings()
            SavePatientColumnsSetting()
            '--------------------------------------

            ''-------------------------------------
            ''CR00000334 :Add new setting for searching patient.
            ''Added to save checked node in settings table for patient search.
            SavePatientSearchSetting_New()
            ''------------------------------------

            ogloSettings.AddSetting("ClearDashboardSearch", Convert.ToString(chkClearDashboardSearch.Checked.ToString()), gnClinicID, gnLoginID, gloSettings.SettingFlag.Clinic)
            bClearDashboardSearch = chkClearDashboardSearch.Checked

            '' SUDHIR 20090725 ''
            '' DEFAULT PROVIDER ''
            If cmbDefaultProvider.SelectedIndex > -1 Then
                ogloSettings.AddSetting("PatientDefaultProvider", cmbDefaultProvider.SelectedValue.ToString(), gnClinicID, 0, gloSettings.SettingFlag.Clinic)
            End If

            '' BILLING ALERT SETTINGS ''
            ogloSettings.AddSetting("BlinkingAlert", Convert.ToString(chkShowBlinkingAlert.Checked.ToString()), gnClinicID, gnLoginID, gloSettings.SettingFlag.Clinic)
            ogloSettings.AddSetting("AlertColor", Convert.ToString(txtAlertColor.BackColor.ToArgb()), gnClinicID, gnLoginID, gloSettings.SettingFlag.Clinic)

            '' EXPORT REPORT SETTING '' 
            Dim oSettings As New gloSettings.DatabaseSetting.DataBaseSetting()
            oSettings.WriteSettings_XML("Reports", "ExportToDefaultLocation", chkExportReport.Checked.ToString())
            oSettings.WriteSettings_XML("Reports", "ExportToDefaultLocationPath", Convert.ToString(txtExportReportPath.Text))
            oSettings.Dispose()

            '' SUDHIR 20090728 '' APPOINTMETN SETTING ''
            ogloSettings.AddSetting("MaxAppointmentsInSlot", Math.Round(num_NoofApptInaSlot.Value).ToString(), gnClinicID, 0, gloSettings.SettingFlag.Clinic)
            ogloSettings.AddSetting("ShowTemplate", chkShowTemplate.Checked.ToString(), gnClinicID, 0, gloSettings.SettingFlag.Clinic)


            oSettings.WriteSettings_XML("Appointment", "NoOfColsOnCalendar", Math.Round(num_NoofColOnCalndr.Value).ToString())

            If rbFollowupFromToday.Checked = True Then
                ogloSettings.AddSetting("FolloupDate", "Today", gnClinicID, 0, gloSettings.SettingFlag.Clinic)
            Else
                ogloSettings.AddSetting("FolloupDate", "FolloupDate", gnClinicID, 0, gloSettings.SettingFlag.Clinic)
            End If

            'ogloSettings.AddSetting("RegisterTemplateAppointmentOnly", chbox_restrictedaptmnt.Checked.ToString(), gnClinicID, 0, gloSettings.SettingFlag.Clinic)


            ogloSettings.AddSetting("CheckedOutAppointment", chkCheckedoutAppointments.Checked.ToString(), gnClinicID, gnLoginID, gloSettings.SettingFlag.User)
            gblnShowCheckedOutAppointment = chkCheckedoutAppointments.Checked
            '' END SUDHIR ''

            'Code start-Added by kanchan on 20101112 for patient confidential info

            gblIsConfidentialInfo = ChkPatientConfiInfo.Checked
            ogloSettings.AddSetting("IsShowPatientConfiInfo", gblIsConfidentialInfo, gnClinicID, 0, gloSettings.SettingFlag.User)
            'Code End-Added by kanchan on 20101112 for patient confidential info

            'Code added by Ashish Tamhane on 27th November 2014
            'for Exam auto recovery information
            ogloSettings.AddSetting("IsExamAutoSaveEnable", chkAutoSaveExam.Checked, gnClinicID, gnLoginID, gloSettings.SettingFlag.Clinic)
            If chkAutoSaveExam.Checked Then
                ogloSettings.AddSetting("ExamAutoSaveTime", Convert.ToInt16(numAutoSaveMinutes.Value), gnClinicID, gnLoginID, gloSettings.SettingFlag.Clinic)
            End If

            ogloSettings.AddSetting("EnableLocalWelchAllynECGDevice", chkEnableLocalWelchAllynECGDevice.Checked.ToString(), gnClinicID, gnLoginID, gloSettings.SettingFlag.User)
            bEnableLocalWelchAllynECGDevice = chkEnableLocalWelchAllynECGDevice.Checked

            ' ''Start :: smart Settting
            'Dim _myInternalList As New myList
            'Dim _myArrLisCollection As New ArrayList
            'Dim myList As New ArrayList()
            'If Not IsNothing(_myInternalList) Then
            '_myArrLisCollection = fillSmartSetting(chkLstBx_smartDiagnosis, "SmartDX", _myInternalList)
            '    myList.Add(_myArrLisCollection)
            '    _myArrLisCollection = fillSmartSetting(chkLstBx_smartTreatment, "SmartCPT", _myInternalList)
            '    myList.Add(_myArrLisCollection)
            '    _myArrLisCollection = fillSmartSetting(chkLstBx_smartOrders, "SmartOrder", _myInternalList)
            '    myList.Add(_myArrLisCollection)
            'dbOperationSmartTreatment(mylist)
            'End If
            ' ''End :: Smart Settings

            Dim ostyle As C1.Win.C1FlexGrid.CellStyle
            Dim mylist As myList
            Dim oChkCol As CheckedListBox.CheckedIndexCollection
            Dim oChkCol1 As CheckedListBox.CheckedIndexCollection
            Dim arrlist As New ArrayList

            ''  SmartDiagnosis  ''
            With C1SmartDiagnosisSendTask
                For i As Integer = 0 To chklst_SmartDiagnosis.Items.Count - 1
                    mylist = New myList
                    mylist.AssociatedItem = chklst_SmartDiagnosis.Items(i).ToString() '' FieldName
                    oChkCol = chklst_SmartDiagnosis.CheckedIndices
                    oChkCol1 = chklst_SmartDiagnosisSendTask.CheckedIndices

                    If mylist.AssociatedItem.ToString() = "Orders and Results" Then
                        For j As Integer = 0 To C1SmartDiagnosisSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Orders and Results" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then

                                    MessageBox.Show("Please select atleast One user for Smart Diagnosis Orders", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If
                    If mylist.AssociatedItem.ToString() = "Order Templates" Then
                        For j As Integer = 0 To C1SmartDiagnosisSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Order Templates" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Diagnosis Orders", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If
                    'Code Start-Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                    If mylist.AssociatedItem.ToString() = "Flowsheet" Then
                        For j As Integer = 0 To C1SmartDiagnosisSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Flowsheet" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Diagnosis Flowsheet", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If
                    If mylist.AssociatedItem.ToString() = "Drugs" Then
                        For j As Integer = 0 To C1SmartDiagnosisSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Drugs" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Diagnosis Drugs", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If
                    'Code End-Added by kanchan on 20100619 for Task generation for Flowsheet/Drug

                    If oChkCol.Count > 0 Then '' Field Status
                        If oChkCol.Contains(i) = True Then
                            mylist.IsFinished = True
                        Else
                            mylist.IsFinished = False
                        End If
                    Else
                        mylist.IsFinished = False
                    End If
                    mylist.CPTCount = i + 1 '' Field Sequence
                    mylist.AssociatedCategory = "SmartDX"
                    arrlist.Add(mylist)
                Next
            End With

            '' SmartTreatment  ''
            With C1SmartTreatmentSendTask
                For i As Integer = 0 To chklst_SmartTreatment.Items.Count - 1
                    mylist = New myList
                    mylist.AssociatedItem = chklst_SmartTreatment.Items(i).ToString() '' FieldName
                    oChkCol = chklst_SmartTreatment.CheckedIndices

                    oChkCol1 = chklst_SmartTreatmentSendTask.CheckedIndices

                    '' If mylist.AssociatedItem.Contains("Orders and Results") Then
                    If mylist.AssociatedItem.ToString() = "Orders and Results" Then
                        For j As Integer = 0 To C1SmartTreatmentSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Orders and Results" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Treatment Orders", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If
                    If mylist.AssociatedItem.ToString() = "Order Templates" Then
                        For j As Integer = 0 To C1SmartTreatmentSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Order Templates" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Treatment Orders", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If
                    'Code Start-Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                    If mylist.AssociatedItem.ToString() = "Flowsheet" Then
                        For j As Integer = 0 To C1SmartTreatmentSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Flowsheet" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Treatment Flowsheet", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If
                    If mylist.AssociatedItem.ToString() = "Drugs" Then
                        For j As Integer = 0 To C1SmartTreatmentSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Drugs" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Treatment Drugs", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If
                    'Code End-Added by kanchan on 20100619 for Task generation for Flowsheet/Drug

                    If oChkCol.Count > 0 Then '' Field Status
                        If oChkCol.Contains(i) = True Then
                            mylist.IsFinished = True
                        Else
                            mylist.IsFinished = False
                        End If
                    Else
                        mylist.IsFinished = False
                    End If
                    mylist.CPTCount = i + 1 '' Field Sequence
                    mylist.AssociatedCategory = "SmartCPT"
                    arrlist.Add(mylist)
                Next
            End With

            '' SmartOrders ''
            With C1SmartOrdersSendTask

                For i As Integer = 0 To chklst_SmartOrders.Items.Count - 1
                    mylist = New myList
                    mylist.AssociatedItem = chklst_SmartOrders.Items(i).ToString() '' FieldName
                    oChkCol = chklst_SmartOrders.CheckedIndices
                    oChkCol1 = chklst_SmartOrdersSendTask.CheckedIndices

                    '' If mylist.AssociatedItem.Contains("Orders and Results") Then
                    If mylist.AssociatedItem.ToString() = "Orders and Results" Then
                        For j As Integer = 0 To C1SmartOrdersSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Orders and Results" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Orders Lab", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If
                    If mylist.AssociatedItem.ToString() = "Order Templates" Then
                        For j As Integer = 0 To C1SmartOrdersSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Order Templates" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Orders Orders", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If
                    'Code Start-Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                    If mylist.AssociatedItem.ToString() = "Flowsheet" Then
                        For j As Integer = 0 To C1SmartOrdersSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Flowsheet" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Orders Flowsheet", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If
                    If mylist.AssociatedItem.ToString() = "Drugs" Then
                        For j As Integer = 0 To C1SmartOrdersSendTask.Rows.Count - 1
                            If .GetData(j, COL_FormNm) = "Drugs" Then
                                'If oChkCol1.Count > 0 Then
                                If .GetCellCheck(j, COL_ChkForm) = 1 Then
                                    mylist.SendTask = True
                                Else
                                    mylist.SendTask = False
                                End If

                                If Not IsNothing(.GetCellStyle(j, COL_Users)) Then
                                    ostyle = .GetCellStyle(j, COL_Users)
                                    mylist.Value = ostyle.ComboList ''mylist.Value is use for store Users.

                                ElseIf Not IsNothing(.GetData(j, COL_Users)) Then
                                    If .GetData(j, COL_Users) <> "" Then
                                        mylist.Value = .GetData(j, COL_Users)
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    mylist.Value = ""
                                End If

                                If Not IsNothing(.GetData(j, COL_Hidden)) Then
                                    mylist.UserID = .GetData(j, COL_Hidden)
                                Else
                                    mylist.UserID = ""
                                End If

                                If .GetCellCheck(j, COL_ChkViewForm) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    mylist.ItemChecked = True  ''mylist.ItemChecked is use for store CheckViewForm
                                Else
                                    mylist.ItemChecked = False
                                End If

                                If mylist.SendTask = True And mylist.ItemChecked = False And mylist.Value = "" Then
                                    MessageBox.Show("Please select atleast One user for Smart Orders Drugs", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    ogloSettings.Dispose()
                                    ogloSettings = Nothing
                                    mylist.Dispose()
                                    mylist = Nothing
                                    Exit Sub
                                End If

                            End If
                        Next
                    End If

                    'Code End-Added by kanchan on 20100619 for Task generation for Flowsheet/Drug

                    If oChkCol.Count > 0 Then '' Field Status
                        If oChkCol.Contains(i) = True Then
                            mylist.IsFinished = True
                        Else
                            mylist.IsFinished = False
                        End If
                    Else
                        mylist.IsFinished = False
                    End If
                    mylist.CPTCount = i + 1 '' Field Sequence
                    mylist.AssociatedCategory = "SmartOrder"
                    arrlist.Add(mylist)
                Next
            End With



            Dim oclsSetting As clsSettings

            oclsSetting = New clsSettings()
            oclsSetting.SetLinkSetting(txtInfo.Text.Trim)
            CheckUserInformation(arrlist)
            oclsSetting.Dispose()
            oclsSetting = Nothing
            If (IsNothing(ogloSettings) = False) Then
                ogloSettings.Dispose()
                ogloSettings = Nothing
            End If

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch objErr As Exception
            Me.DialogResult = DialogResult.Cancel
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If

        End Try
    End Sub



    Private Sub Fill_Printers()

        GroupBox7.Enabled = False

        With cmbFAXPrinter
            .Enabled = False
            .Items.Clear()

            Dim nCount As Int16

            For nCount = 0 To PrinterSettings.InstalledPrinters.Count - 1
                .Items.Add(PrinterSettings.InstalledPrinters(nCount).ToString)
            Next

            .Enabled = True
        End With

        GroupBox7.Enabled = True

    End Sub

    Private Sub btnBrowseFAXOutputDirectory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFAXOutputDirectory.Click
        Try
            With FolderBrowserDialog1
                .ShowNewFolderButton = True
                .Description = "Select FAX Output Directory"
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    txtFAXOutputDirectory.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnBrowseDMSPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseDMSPath.Click
        Try
            With FolderBrowserDialog1
                .ShowNewFolderButton = True
                .Description = "Select DMS Server Directory"
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    txtDMSPath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnServerpath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServerpath.Click
        Try
            With FolderBrowserDialog1
                .ShowNewFolderButton = True
                .Description = "Select Server Data Directory"
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    txtServerPath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#Region " Clinic Environment "

    Private Sub lblENV_01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblENV_01.Click
        Try
            lblENV_01.Tag = 1
            lblENV_Click(lblENV_01)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblENV_02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblENV_02.Click
        Try
            lblENV_02.Tag = 2
            lblENV_Click(lblENV_02)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblENV_03_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblENV_03.Click
        Try
            lblENV_03.Tag = 3
            lblENV_Click(lblENV_03)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblENV_04_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblENV_04.Click
        Try
            lblENV_04.Tag = 4
            lblENV_Click(lblENV_04)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblENV_05_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblENV_05.Click
        Try
            lblENV_05.Tag = 5
            lblENV_Click(lblENV_05)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblENV_06_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblENV_06.Click
        Try
            lblENV_06.Tag = 6
            lblENV_Click(lblENV_06)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblENV_Click(ByVal lbl As Label)

        With ColorDialog1
            .AllowFullOpen = True
            .ShowHelp = False
            .Color = lbl.BackColor
            Try
                .CustomColors = gloGlobal.gloCustomColor.customColor
            Catch ex As Exception

            End Try
            If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                Try
                    gloGlobal.gloCustomColor.customColor = .CustomColors
                Catch ex As Exception

                End Try
                Dim cls As New clsDoctorsDashBoard
                Dim IsValid As Boolean = False
                Select Case lbl.Tag
                    Case 1
                        IsValid = Set_ClinicEnvironment(ClinicEnvironment.ENV_1, .Color)
                        'IsValid = cls.Set_Clinic_Environment(ClinicEnvironment.ENV_1, .Color.ToArgb)
                    Case 2
                        IsValid = Set_ClinicEnvironment(ClinicEnvironment.ENV_2, .Color)
                        'IsValid = cls.Set_Clinic_Environment(ClinicEnvironment.ENV_2, .Color.ToArgb)
                    Case 3
                        IsValid = Set_ClinicEnvironment(ClinicEnvironment.ENV_3, .Color)
                        'IsValid = cls.Set_Clinic_Environment(ClinicEnvironment.ENV_3, .Color.ToArgb)
                    Case 4
                        IsValid = Set_ClinicEnvironment(ClinicEnvironment.ENV_4, .Color)
                        'IsValid = cls.Set_Clinic_Environment(ClinicEnvironment.ENV_4, .Color.ToArgb)
                    Case 5
                        IsValid = Set_ClinicEnvironment(ClinicEnvironment.ENV_5, .Color)
                        'IsValid = cls.Set_Clinic_Environment(ClinicEnvironment.ENV_5, .Color.ToArgb)
                    Case 6
                        IsValid = Set_ClinicEnvironment(ClinicEnvironment.ENV_6, .Color)
                        'IsValid = cls.Set_Clinic_Environment(ClinicEnvironment.ENV_6, .Color.ToArgb)
                End Select

                If IsValid = True Then
                    lbl.BackColor = .Color
                End If

            End If
        End With

    End Sub

    Private Function Set_ClinicEnvironment(ByVal ENV As ClinicEnvironment, ByVal colorCode As System.Drawing.Color) As Boolean
        Dim i As Integer
        Dim IsExists As Boolean = False

        For i = 1 To objClinicEnv.Count
            If Split(objClinicEnv(i), "|", 2).GetValue(0) = ENV Then
                objClinicEnv.Remove(i)
                objClinicEnv.Add(ENV & "|" & colorCode.ToArgb)
                Return True
            End If
        Next

        If IsExists = False Then
            objClinicEnv.Add(ENV & "|" & colorCode.ToArgb)
            IsExists = True
        End If

        Return IsExists

    End Function
    ''' <summary>
    ''' To fill the Drop with Colrs for highlighting the  Word Document Form Fields
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillColorsindex()
        Dim MyDT As New DataTable
        Dim MyRow As DataRow
        MyDT.Columns.Add(New DataColumn("ColorIndex", GetType(Int32)))
        MyDT.Columns.Add(New DataColumn("Color", GetType(String)))

        MyRow = MyDT.NewRow()
        MyRow(0) = 1
        MyRow(1) = "Black"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 2
        MyRow(1) = "Blue"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 4
        MyRow(1) = "BrightGreen"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 9
        MyRow(1) = "DarkBlue"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 13
        MyRow(1) = "DarkRed"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 14
        MyRow(1) = "DarkYellow"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 15
        MyRow(1) = "Gray50"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 16
        MyRow(1) = "Gray25"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 11
        MyRow(1) = "Green"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 5
        MyRow(1) = "Pink"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 6
        MyRow(1) = "Red"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 10
        MyRow(1) = "Teal"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 3
        MyRow(1) = "Turquoise"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 12
        MyRow(1) = "Violet"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 8
        MyRow(1) = "White"
        MyDT.Rows.Add(MyRow)


        MyRow = MyDT.NewRow()
        MyRow(0) = 7
        MyRow(1) = "Yellow"
        MyDT.Rows.Add(MyRow)

        cmbHighlight.DataSource = MyDT
        cmbHighlight.DisplayMember = "Color"
        cmbHighlight.ValueMember = "ColorIndex"

        If gblnWordColorHighlight Then
            chksethighlightcolr.Checked = True
            cmbHighlight.Enabled = True
            cmbHighlight.SelectedValue = gblnWordBackColor
        Else
            chksethighlightcolr.Checked = False
            cmbHighlight.SelectedValue = gblnWordBackColor
            cmbHighlight.Enabled = False
        End If


    End Sub

    Private Sub ExamNotesSelection(ByVal blnLoad As Boolean)
        If blnLoad Then
            If gblnExamSelection = 0 Then
                rbtnNone.Checked = True
            ElseIf gblnExamSelection = 1 Then
                rbtnNotes.Checked = True
            ElseIf gblnExamSelection Then
                rbtnSelect.Checked = True
            End If
        Else

            If rbtnNotes.Checked Then
                gblnExamSelection = 1
            ElseIf rbtnSelect.Checked Then
                gblnExamSelection = 2
            ElseIf rbtnNone.Checked Then
                gblnExamSelection = 0
            End If
        End If


    End Sub
    Private Sub Get_ClinicEnvironment()

        Dim dt As DataTable

        dt = dsSettings.Tables("GetClinicEnvironment")

        If IsNothing(dt) = False Then
            For i As Int16 = 0 To dt.Rows.Count - 1
                Select Case dt.Rows(i)("nEnvironment")
                    Case ClinicEnvironment.ENV_1
                        lblENV_01.BackColor = Color.FromArgb(dt.Rows(i)("nColor"))
                    Case ClinicEnvironment.ENV_2
                        lblENV_02.BackColor = Color.FromArgb(dt.Rows(i)("nColor"))
                    Case ClinicEnvironment.ENV_3
                        lblENV_03.BackColor = Color.FromArgb(dt.Rows(i)("nColor"))
                    Case ClinicEnvironment.ENV_4
                        lblENV_04.BackColor = Color.FromArgb(dt.Rows(i)("nColor"))
                    Case ClinicEnvironment.ENV_5
                        lblENV_05.BackColor = Color.FromArgb(dt.Rows(i)("nColor"))
                    Case ClinicEnvironment.ENV_6
                        lblENV_06.BackColor = Color.FromArgb(dt.Rows(i)("nColor"))
                End Select
            Next
            dt.Dispose()
            dt = Nothing
        End If

    End Sub

#End Region

    Private Sub chksethighlightcolr_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chksethighlightcolr.Checked = True Then
            cmbHighlight.SelectedValue = gblnWordBackColor
            cmbHighlight.Enabled = True
        Else
            cmbHighlight.Enabled = False
        End If
    End Sub

    Private Sub chkBdayReminder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBdayReminder.CheckedChanged
        If chkBdayReminder.Checked = True Then
            pnlBday.Visible = True
        Else
            pnlBday.Visible = False
        End If
    End Sub

    Private Sub btnBrowseVMSPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseVMSPath.Click
        Try
            With FolderBrowserDialog1
                .ShowNewFolderButton = True
                .Description = "Select VMS Server Directory"
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    txtVMSPath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub tlsp_Settings_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_Settings.ItemClicked
        Try
            'Code Added on 20091126-Mayuri
            'To fix Bug Id:#5183-enter number with  minus sign into  'Auto Refresh messages/Tasks After every'gives exception
            tlsp_Settings.Focus()
            'End code Added on 20091126
            Select Case e.ClickedItem.Tag
                Case "OK"
                    OKSetting()

                Case "Cancel"
                    CancelSetting()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub grHL7Settings_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grSettings.Enter

    End Sub

    Private Sub SavePatientSearchSettings()
        Try
            If cmbSearchColumns.SelectedIndex <> -1 Then
                Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)
                ogloSettings.AddSetting("Patient Search Column", cmbSearchColumns.SelectedItem.ToString(), gnClinicID, 0, gloSettings.SettingFlag.Clinic)
                ogloSettings.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveExchangeServerSettings()
        Try
            Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)
            ogloSettings.AddSetting("ExchangeDomain", txtExchangeDomain.Text.Trim(), gnClinicID, 0, gloSettings.SettingFlag.Clinic)
            ogloSettings.AddSetting("ExchangeURL", txtExchangeURL.Text.Trim(), gnClinicID, 0, gloSettings.SettingFlag.Clinic)
            ogloSettings.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SavePatientColumnsSetting()
        Try
            Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)

            Dim _SettingValue As String = ""
            Dim i As Integer
            For i = 0 To trvPatientColumns.Nodes.Count - 1
                If trvPatientColumns.Nodes(i).Checked = True Then
                    _SettingValue = _SettingValue + "," & trvPatientColumns.Nodes(i).Tag.ToString().Trim()
                End If
            Next
            If _SettingValue.Trim() <> "" Then
                _SettingValue = _SettingValue.Substring(1, _SettingValue.Length - 1)
            End If

            ogloSettings.AddSetting("Patient Columns", _SettingValue, gnClinicID, gnLoginID, gloSettings.SettingFlag.Clinic)
            ogloSettings.Dispose()
            ogloSettings = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''-----------------------------------------------------
    ''CR00000334 :Add new setting for searching patient.
    '' To Save Patient Search Settings
    Private Sub SavePatientSearchSetting_New()
        Try
            Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)

            Dim _PatientSearchSettingValue As String = ""
            Dim i As Integer
            For i = 0 To trvPatientSearch.Nodes.Count - 1
                If trvPatientSearch.Nodes(i).Checked = True Then
                    _PatientSearchSettingValue = _PatientSearchSettingValue + "," & trvPatientSearch.Nodes(i).Tag.ToString().Trim()
                End If
            Next
            If _PatientSearchSettingValue.Trim() <> "" Then
                _PatientSearchSettingValue = _PatientSearchSettingValue.Substring(1, _PatientSearchSettingValue.Length - 1)
            End If

            ogloSettings.AddSetting("Patient Search", _PatientSearchSettingValue, gnClinicID, gnLoginID, gloSettings.SettingFlag.Clinic)
            ogloSettings.Dispose()
            ogloSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub FillPatientSearchSettings()

        Try

            'Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)
            Dim patientSearchColumn As New Object()

            'ogloSettings.GetSetting("Patient Search Column", gnLoginID, gnClinicID, patientSearchColumn)

            If dsSettings.Tables("PatientSearchColumn").Rows.Count > 0 Then
                patientSearchColumn = dsSettings.Tables("PatientSearchColumn").Rows(0)("sSettingsValue")
            End If

            If IsNothing(patientSearchColumn) = False Then
                cmbSearchColumns.SelectedItem = patientSearchColumn.ToString()
            End If

            patientSearchColumn = Nothing

            'ogloSettings.Dispose()
            'ogloSettings = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    ''----------------------------
    ''CR00000334 :Add new setting for searching patient.
    Private Sub FillPatientSearch()
        Try
            '' Patient Search Tree
            ''----------------------------------------------
            Dim PatientSearchNode As TreeNode

            PatientSearchNode = New TreeNode("Middle Name")
            PatientSearchNode.Tag = "MI"
            trvPatientSearch.Nodes.Add(PatientSearchNode)
            PatientSearchNode = Nothing

            PatientSearchNode = New TreeNode("SSN")
            PatientSearchNode.Tag = "SSN"
            trvPatientSearch.Nodes.Add(PatientSearchNode)
            PatientSearchNode = Nothing

            PatientSearchNode = New TreeNode("Date Of Birth")
            PatientSearchNode.Tag = "DOB"
            trvPatientSearch.Nodes.Add(PatientSearchNode)
            PatientSearchNode = Nothing

            PatientSearchNode = New TreeNode("Phone")
            PatientSearchNode.Tag = "Phone"
            trvPatientSearch.Nodes.Add(PatientSearchNode)
            PatientSearchNode = Nothing

            PatientSearchNode = New TreeNode("Mobile")
            PatientSearchNode.Tag = "Mobile"
            trvPatientSearch.Nodes.Add(PatientSearchNode)
            PatientSearchNode = Nothing

            ''----------------------------------------------


            trvPatientSearch.Nodes(trvPatientSearch.Nodes.Count - 1).EnsureVisible()
            ''CR00000334 :Add new setting for searching patient.
            ''Retrive setting from DB and mark node as check/uncheck.
            ' #Region "Patient Search" 
            'Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
            Dim value As Object = Nothing

            ''Added to check whether setting is present in table or not (for first time).
            Dim PatientSearchDt As DataTable = Nothing
            PatientSearchDt = dsSettings.Tables("PatientSearch") 'ogloSettings.GetSetting("Patient Search")


            ''If setting is present then retrive it and checked node according to patient search setting.
            '' Retrive Patient Search setting for current user
            Dim chkUser As Boolean = False
            If (IsNothing(PatientSearchDt) = False) Then


                For s As Int32 = 0 To PatientSearchDt.Rows.Count - 1
                    If PatientSearchDt.Rows(s)(1).ToString().Trim() = gnLoginID.ToString().Trim() Then
                        chkUser = True
                        Exit For
                    End If
                Next
                PatientSearchDt.Dispose()
                PatientSearchDt = Nothing
            End If
            'If setting is present then retrive it and checked node according to patient search setting.

            PatientSearchDt = New DataTable

            PatientSearchDt = dsSettings.Tables("PatientSearchUser")

            If PatientSearchDt.Rows.Count > 0 Then
                value = PatientSearchDt.Rows(0)("sSettingsValue")
            End If

            If chkUser Then
                'ogloSettings.GetSetting("Patient Search", gnLoginID, gnClinicID, value)

                If value IsNot Nothing Then
                    If Convert.ToString(value).Trim() <> "" Then
                        Dim PatientSearch As String() = Convert.ToString(value).Trim().Split(",")
                        For i As Integer = 0 To PatientSearch.Length - 1
                            For j As Integer = 0 To trvPatientSearch.Nodes.Count - 1
                                If trvPatientSearch.Nodes(j).Tag.ToString().Trim() = PatientSearch(i).Trim() Then
                                    trvPatientSearch.Nodes(j).Checked = True
                                End If
                            Next
                        Next
                    End If
                End If
            Else

                '04-Apr-2017 Aniket: If there is no setting present in the database, then do not select the search nodes
                For j As Int32 = 0 To trvPatientSearch.Nodes.Count - 1
                    trvPatientSearch.Nodes(j).Checked = False
                Next

            End If
            

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    ''-----------------------------

    Private Sub FillExchangeServerSettings()
        Try
            Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)
            Dim value As New Object()

            ogloSettings.GetSetting("ExchangeDomain", value)
            If IsNothing(value) = False Then
                txtExchangeDomain.Text = Convert.ToString(value)
                value = Nothing
            End If

            ogloSettings.GetSetting("ExchangeURL", value)
            If IsNothing(value) = False Then
                txtExchangeURL.Text = Convert.ToString(value)
                value = Nothing
            End If
            ogloSettings.Dispose()
            ogloSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillPatientColumnsSetting()

        Try

            'Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)
            Dim patientDataColumn As New Object()


            'ogloSettings.GetSetting("Patient Columns", gnLoginID, gnClinicID, patientDataColumn)

            If dsSettings.Tables("PatientColumns").Rows.Count > 0 Then
                patientDataColumn = dsSettings.Tables("PatientColumns").Rows(0)("sSettingsValue")
            End If

            If IsNothing(patientDataColumn) = False Then
                Dim PatientColumns() As String = Convert.ToString(patientDataColumn).Split(",")
                For i As Integer = 0 To PatientColumns.Length - 1
                    For j As Integer = 0 To trvPatientColumns.Nodes.Count - 1
                        If trvPatientColumns.Nodes(j).Tag.ToString().Trim() = PatientColumns(i).Trim() Then
                            trvPatientColumns.Nodes(j).Checked = True
                        End If
                    Next
                Next
                patientDataColumn = Nothing
            End If

            'ogloSettings.Dispose()
            'ogloSettings = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    'Private Sub FillNDCAlternativeGridSetting()
    '    Try

    '        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)
    '        Dim oNDCAlternative As String

    '        ogloSettings.GetSetting("Show NDC in alternative grid", gnLoginID, gnClinicID, oNDCAlternative)

    '        If oNDCAlternative <> "" Then
    '            If String.Compare(oNDCAlternative, "True", True) = 0 Then
    '                chkNDCInAlternativeGrid.Checked = True
    '            Else
    '                chkNDCInAlternativeGrid.Checked = False
    '            End If

    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub chkAutoApplicationLock_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoApplicationLock.CheckedChanged
        Try
            If chkAutoApplicationLock.Checked = True Then
                numLockTime.Enabled = True
            Else
                numLockTime.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#Region "DICOM Settings"
    'sarika DICOM Settings 20090214
    Private Sub btnBrowseDICOMPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseDICOMPath.Click
        Try
            With FolderBrowserDialog1
                .ShowNewFolderButton = True
                .Description = "Select DICOM Path"
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    txtDICOMPath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '------
#End Region

    ''Sandip Darade 20090303
    '' Default Navigation settings

#Region "Default Navigation Settings"

    Private Sub FillNavigationPnls()
        ''List navigation panels to combolist
        Dim dtpnls As New DataTable
        Dim dr As DataRow
        dtpnls.Columns.Add(New DataColumn("NavigationPnl", GetType(String)))

        dr = dtpnls.NewRow()
        dr(0) = "Appointment"
        dtpnls.Rows.Add(dr)

        'dr = dtpnls.NewRow()
        'dr(0) = "Calender"
        'dtpnls.Rows.Add(dr)
        'commented for remove row as default navigation from dasboard-we only use triage and appoinment as default navigation
        'dr = dtpnls.NewRow()
        'dr(0) = "Messages"
        'dtpnls.Rows.Add(dr)

        'dr = dtpnls.NewRow()
        'dr(0) = "MyDay"
        'dtpnls.Rows.Add(dr)

        'commented for remove row as default navigation from dasboard-we only use triage and appoinment as default navigation
        'dr = dtpnls.NewRow()
        'dr(0) = "Tasks"
        'dtpnls.Rows.Add(dr)

        dr = dtpnls.NewRow()
        dr(0) = "Triage"
        dtpnls.Rows.Add(dr)



        'dr = dtpnls.NewRow()
        'dr(0) = "Unfinished Exam"
        'dtpnls.Rows.Add(dr)

        Cmb_NavgtnPnl.DataSource = dtpnls
        Cmb_NavgtnPnl.DisplayMember = "NavigationPnl"
        Cmb_NavgtnPnl.ValueMember = "NavigationPnl"

    End Sub

    Private Sub FillDefaultNavigation()
        Try
            ''Retrieve default navigation setting from registry
            'Dim regKey As RegistryKey
            'If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
            '    Exit Sub
            'End If
            'regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
            'If IsNothing(regKey.GetValue("DefaultNavigation")) = True Then
            '    regKey.SetValue("DefaultNavigation", "Appointment") ''If defaultNavigation value in registry is null   
            'Else
            '    Cmb_NavgtnPnl.Text = regKey.GetValue("DefaultNavigation")
            'End If

            If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                Exit Sub
            End If
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            If IsNothing(gloRegistrySetting.GetRegistryValue("DefaultNavigation")) = True Then
                gloRegistrySetting.SetRegistryValue("DefaultNavigation", "Appointment") ''If defaultNavigation value in registry is null   
            Else
                Cmb_NavgtnPnl.Text = gloRegistrySetting.GetRegistryValue("DefaultNavigation")
            End If
            ' gloRegistrySetting.CloseRegistryKey()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    ''>>>>>>>>>>>>>>>>>>>>Ojeswini06032009<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    ''Button mouse hover and leave images.

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFAXOutputDirectory.MouseHover, btnServerpath.MouseHover, btnBrowseVMSPath.MouseHover, btnBrowseDMSPath.MouseHover, btnBrowseDICOMPath.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFAXOutputDirectory.MouseLeave, btnServerpath.MouseLeave, btnBrowseVMSPath.MouseLeave, btnBrowseDMSPath.MouseLeave, btnBrowseDICOMPath.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    ''>>>>>>>>>>>>>>>>>>>>Ojeswini06032009<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    ''RadioButton Bold and Regular effect.

    Private Sub rbGenius_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbGenius.Checked = True Then
            rbGenius.Font = myBoldFont
            'chkPatientReg.Visible = False
        Else
            rbGenius.Font = myNormalFont
            'chkPatientReg.Visible = True
        End If
    End Sub

    Private Sub rbHL7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbHL7.Checked = True Then
            rbHL7.Font = myBoldFont
            chkPatientReg.Visible = True
        Else
            rbHL7.Font = myNormalFont
            'chkPatientReg.Visible = False
        End If
    End Sub

    Private Sub optYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optYes.CheckedChanged
        If optYes.Checked = True Then
            optYes.Font = myBoldFont
        Else
            optYes.Font = myNormalFont
        End If
    End Sub

    Private Sub optNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNo.CheckedChanged
        If optNo.Checked = True Then
            optNo.Font = myBoldFont
        Else
            optNo.Font = myNormalFont
        End If
    End Sub

    Private Sub optTopLeft_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTopLeft.CheckedChanged
        If optTopLeft.Checked = True Then
            optTopLeft.Font = myBoldFont
        Else
            optTopLeft.Font = myNormalFont
        End If
    End Sub

    Private Sub optTopRight_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTopRight.CheckedChanged
        If optTopRight.Checked = True Then
            optTopRight.Font = myBoldFont
        Else
            optTopRight.Font = myNormalFont
        End If
    End Sub

    Private Sub optBottomLeft_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBottomLeft.CheckedChanged
        If optBottomLeft.Checked = True Then
            optBottomLeft.Font = myBoldFont
        Else
            optBottomLeft.Font = myNormalFont
        End If
    End Sub

    Private Sub optBottomRight_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBottomRight.CheckedChanged
        If optBottomRight.Checked = True Then
            optBottomRight.Font = myBoldFont
        Else
            optBottomRight.Font = myNormalFont
        End If
    End Sub

    Private Sub rbtnNotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnNotes.CheckedChanged
        If rbtnNotes.Checked = True Then
            rbtnNotes.Font = myBoldFont
        Else
            rbtnNotes.Font = myNormalFont
        End If
    End Sub

    Private Sub rbtnSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSelect.CheckedChanged
        If rbtnSelect.Checked = True Then
            rbtnSelect.Font = myBoldFont
        Else
            rbtnSelect.Font = myNormalFont
        End If
    End Sub

    Private Sub rbtnNone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnNone.CheckedChanged
        If rbtnNone.Checked = True Then
            rbtnNone.Font = myBoldFont
        Else
            rbtnNone.Font = myNormalFont
        End If
    End Sub

    Private Sub rdo_IncludePageNo_Yes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdo_IncludePageNo_Yes.CheckedChanged
        If rdo_IncludePageNo_Yes.Checked = True Then
            rdo_IncludePageNo_Yes.Font = myBoldFont
        Else
            rdo_IncludePageNo_Yes.Font = myNormalFont
        End If
    End Sub

    Private Sub rdo_IncludePageNo_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdo_IncludePageNo_No.CheckedChanged
        If rdo_IncludePageNo_No.Checked = True Then
            rdo_IncludePageNo_No.Font = myBoldFont
        Else
            rdo_IncludePageNo_No.Font = myNormalFont
        End If
    End Sub

    Private Sub chkOutbound_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOutbound.CheckedChanged
        'code below commented on 23-nov-2012 to hide hl7outbound setting
        'If chkOutbound.Checked = True Then
        '    grHL7Settings.Enabled = True
        '    gblnGenerateOutboundMsg = True
        '    appSettings("GenerateHL7Message") = True ' Added by kanchan on 20100102 for appointment settings in PM
        'Else
        '    grHL7Settings.Enabled = False
        '    rbGenius.Checked = False
        '    rbHL7.Checked = False
        '    chkPatientReg.Checked = False
        '    chkSaveandClose.Checked = False
        '    chkSaveandFinish.Checked = False
        '    'Code Start-Added by kanchan on 20100521 for HL7 setting for immunization
        '    chkHL7Immunization.Checked = False
        '    'Code End-Added by kanchan on 20100521 for HL7 setting for immunization

        '    '' Added by Abhijeet on 20110919
        '    chkHL7Appointment.Checked = False
        '    ''End of changes by Abhijeet on 20110919
        'End If
        'code above commented on 23-nov-2012 to hide hl7outbound setting
    End Sub

    ''>>>>>>>>>>>>>>>>>>>>Ojeswini06032009<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    Private Sub chkOutbound_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOutbound.CheckStateChanged
        'code below commented on 23-nov-2012 to hide hl7outbound setting

        'If chkOutbound.Checked = True Then
        '    grHL7Settings.Enabled = True
        'Else
        '    grHL7Settings.Enabled = False
        'End If
        'code above commented on 23-nov-2012 to hide hl7outbound setting

    End Sub

    Private Sub btnBrowseAlertColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowseAlertColor.Click
        Try
            ' Dim oColor As New ColorDialog
            Try
                clordialogWord.CustomColors = gloGlobal.gloCustomColor.customColor
            Catch ex As Exception

            End Try
            If clordialogWord.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                txtAlertColor.BackColor = clordialogWord.Color
                Try
                    gloGlobal.gloCustomColor.customColor = clordialogWord.CustomColors
                Catch ex As Exception

                End Try
            End If
            'oColor.Dispose()
            'oColor = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnBrowseReportPath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowseReportPath.Click
        Try
            Dim oFolderDialog As New FolderBrowserDialog
            If oFolderDialog.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                txtExportReportPath.Text = oFolderDialog.SelectedPath
            End If
            oFolderDialog.Dispose()
            oFolderDialog = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnClearReportPath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearReportPath.Click
        txtExportReportPath.Clear()
    End Sub

    Private Sub rbFolloupFromDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbFolloupFromDate.CheckedChanged
        If rbFolloupFromDate.Checked Then
            rbFolloupFromDate.Font = myBoldFont
        Else
            rbFolloupFromDate.Font = myNormalFont
        End If
    End Sub

    Private Sub rbFollowupFromToday_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbFollowupFromToday.CheckedChanged
        If rbFollowupFromToday.Checked Then
            rbFollowupFromToday.Font = myBoldFont
        Else
            rbFollowupFromToday.Font = myNormalFont
        End If
    End Sub

    Private Sub numBufferSize_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numBufferSize.KeyPress
        'added by dipak 20091026 Bug(4437)
        'allow only numeric value
        AllowNumaric(numBufferSize.Text, e)
    End Sub

    'Allow only numeric and decimal point keys

    Private Sub AllowDecimal(ByVal txtTextBox As TextBox, ByVal e As KeyPressEventArgs)
        Try
            If InStr(Trim(txtTextBox.Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
                e.Handled = True
            Else
                If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8))) Then
                    e.Handled = True
                End If
                ''added by dipak 20091027
                'validation for '.' and 3 digit after .
                If txtTextBox.SelectionStart > txtTextBox.Text.IndexOf(".") Then
                    If txtTextBox.Text.Contains(".") = True Then
                        If txtTextBox.Text.Substring(txtTextBox.Text.IndexOf(".") + 1, txtTextBox.Text.Length - (txtTextBox.Text.IndexOf(".") + 1)).Length = 3 Then
                            e.Handled = True
                        End If
                    End If
                End If
            End If
            'added by dipak 20091027
            ''If Backspace pressed
            If (e.KeyChar = ChrW(8)) Then
                e.Handled = False
            End If
            ''end added by dipak 20091027
        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' 'Allow only numeric and Not decimal point keys
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <param name="e"></param>
    ''' <remarks>added by dipak 20091026</remarks>
    Private Sub AllowNumaric(ByVal Text As String, ByVal e As KeyPressEventArgs)
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    Private Sub txtCardWidth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'added by dipak 20091026 Bug 4438
        'Allows only decimal
        AllowDecimal(txtCardWidth, e)
    End Sub

    Private Sub txtCardLength_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'added by dipak 20091026 to fix issue Bug 4438
        'allows only decimal values
        AllowDecimal(txtCardLength, e)
    End Sub





    ''Under Surescript Tab -> Formulary settings chkbox events

    Private Sub chkFormularyAlertnativesAllDrgs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFormularyAlertnativesAllDrgs.CheckedChanged
        Try
            If chkFormularyAlertnativesAllDrgs.Checked = True Then
                chkFormularyAlertnativesOffFormularyDrgs.Checked = False

                gblnFormularyAlertnativesOffFormularyDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_OffFormularyDrugs = False

                chkFormularyAlertnativesNRDrgs.Checked = False
                gblnFormularyAlertnativesNRDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_NRDrugs = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkFormularyAlertnativesOffFormularyDrgs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFormularyAlertnativesOffFormularyDrgs.CheckedChanged
        Try
            If chkFormularyAlertnativesOffFormularyDrgs.Checked = True Then
                chkFormularyAlertnativesAllDrgs.Checked = False
                gblnFormularyAlertnativesAllDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = False

                'If gblnFormularyAlertnativesNRDrgs = True Then
                '    chkFormularyAlertnativesNRDrgs.Checked = True
                'Else
                '    chkFormularyAlertnativesNRDrgs.Checked = False
                'End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkFormularyAlertnativesNRDrgs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFormularyAlertnativesNRDrgs.CheckedChanged
        Try
            If chkFormularyAlertnativesNRDrgs.Checked = True Then
                chkFormularyAlertnativesAllDrgs.Checked = False
                gblnFormularyAlertnativesAllDrgs = False
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = False

                'If gblnFormularyAlertnativesOffFormularyDrgs = True Then
                '    chkFormularyAlertnativesOffFormularyDrgs.Checked = True
                'Else
                '    chkFormularyAlertnativesOffFormularyDrgs.Checked = False
                'End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub num_MessagesRefreshTime_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles num_MessagesRefreshTime.KeyPress

    End Sub

    Private Sub txtCardWidth_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'AllowDecimal(txtCardWidth, e)
    End Sub
    Private Function ValidateCardSize() As Boolean
        If txtCardWidth.Text.Trim() <> "" Then

            Dim _CardWidth As Single
            If txtCardWidth.Text.Trim() <> "" Then
                Try
                    _CardWidth = CSng(Convert.ToDouble(txtCardWidth.Text.Trim()))
                Catch generatedExceptionName As FormatException
                    MessageBox.Show("Card Width is invalid", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCardWidth.Focus()
                    ValidateCardSize = Nothing
                    Exit Function
                End Try
                If _CardWidth < CSng(2.1) Then
                    MessageBox.Show("Card Width must be greater than 2.0 Inches", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCardWidth.Focus()
                    ValidateCardSize = Nothing
                    Exit Function
                End If


            End If
        Else
            MessageBox.Show("Please enter card width.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCardWidth.Focus()
            ValidateCardSize = Nothing
            Exit Function
        End If

        If txtCardLength.Text.Trim() <> "" Then


            Dim _CardLength As Single
            If txtCardLength.Text.Trim() <> "" Then
                Try

                    _CardLength = CSng(Convert.ToDouble(txtCardLength.Text.Trim()))
                Catch generatedExceptionName As FormatException
                    MessageBox.Show("Card Length is invalid", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCardLength.Focus()
                    ValidateCardSize = Nothing
                    Exit Function
                End Try
                If _CardLength < CSng(2.2) Then
                    MessageBox.Show("Card Length must be greater than 2.1 Inches", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCardLength.Focus()
                    ValidateCardSize = Nothing
                    Exit Function
                End If


            End If
        Else
            MessageBox.Show("Please enter card Length.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCardLength.Focus()
            ValidateCardSize = Nothing
            Exit Function
        End If

        Return True
    End Function
    Dim _isScannerSemaphore As Boolean = False
#Region "Selected Index change in CMBScanner + cmbScanMode + cmbBitDepth"
    Private Sub cmbScanner_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbScanner.SelectedIndexChanged
        If cmbScanner.SelectedIndex <> -1 Then
            If _isScannerSemaphore = True Then
                Return
            End If
            _isScannerSemaphore = True
            ''objScannerSettings.SetScanners(twainDevice, cmbScanner)
            Dim mycombocollections As gloEDocumentV3.Forms.ComboCollection
            mycombocollections = CType(cmbScanner.Items(cmbScanner.SelectedIndex), gloEDocumentV3.Forms.ComboCollection)

            InitPagasusTwainDevice()

            objScannerSettings.GetAndSetScanners(mycombocollections.MyStrings, twainDevice, cmbScanner)
            objScannerSettings.ObtainScannerSettings(twainDevice, cmbScanner, cmbScanner, cmbScanMode, cmbBitDepth, cmbResolution, cmbBrightness, cmbContrast, cmbScanSide, chkShowScannerDialog, cmbSupportedSize, txtCardWidth, txtCardLength, txtStartX, txtStartY, myScanLayout)
            _isScannerSemaphore = False

            If bChkPegasusValues Then
                setPegasusBrightNContrast()
            End If
        End If



    End Sub

    Private Sub cmbScanMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbScanMode.SelectedIndexChanged
        If cmbScanner.SelectedIndex <> -1 Then
            If _isScannerSemaphore = True Then
                Return
            End If

            InitPagasusTwainDevice()

            objScannerSettings.ObtainScannerSettings(twainDevice, cmbScanMode, cmbScanner, cmbScanMode, cmbBitDepth, cmbResolution, cmbBrightness, cmbContrast, cmbScanSide, chkShowScannerDialog, cmbSupportedSize, txtCardWidth, txtCardLength, txtStartX, txtStartY, myScanLayout)
            _isScannerSemaphore = False
        End If

    End Sub

    Private Sub cmbBitDepth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBitDepth.SelectedIndexChanged
        If cmbScanner.SelectedIndex <> -1 Then
            If _isScannerSemaphore = True Then
                Return
            End If
            _isScannerSemaphore = True
            InitPagasusTwainDevice()
            objScannerSettings.ObtainScannerSettings(twainDevice, cmbBitDepth, cmbScanner, cmbScanMode, cmbBitDepth, cmbResolution, cmbBrightness, cmbContrast, cmbScanSide, chkShowScannerDialog, cmbSupportedSize, txtCardWidth, txtCardLength, txtStartX, txtStartY, myScanLayout)
            _isScannerSemaphore = False
        End If
    End Sub
#End Region

#Region "FillCheckBox"
    Private Sub chkLstBx_smartDiagnosis_fun()
        chklst_SmartDiagnosis.Items.Add("CPT")
        chklst_SmartDiagnosis.Items.Add("Drugs")
        chklst_SmartDiagnosis.Items.Add("Patient Education")
        chklst_SmartDiagnosis.Items.Add("Tags")
        chklst_SmartDiagnosis.Items.Add("Flowsheet")
        chklst_SmartDiagnosis.Items.Add("Orders and Results")
        chklst_SmartDiagnosis.Items.Add("Order Templates")
        chklst_SmartDiagnosis.Items.Add("Referral Letter")
    End Sub
    Private Sub chklst_SmartTreatment_fun()
        chklst_SmartTreatment.Items.Add("ICD9")
        chklst_SmartTreatment.Items.Add("Drugs")
        chklst_SmartTreatment.Items.Add("Patient Education")
        chklst_SmartTreatment.Items.Add("Tags")
        chklst_SmartTreatment.Items.Add("Flowsheet")
        chklst_SmartTreatment.Items.Add("Orders and Results")
        chklst_SmartTreatment.Items.Add("Order Templates")
        chklst_SmartTreatment.Items.Add("Referral Letter")
    End Sub
    Private Sub chkLstBx_smartOrders_fun()
        chklst_SmartOrders.Items.Add("Orders and Results")
        chklst_SmartOrders.Items.Add("Order Templates")
        chklst_SmartOrders.Items.Add("Referral Letter")
        chklst_SmartOrders.Items.Add("Drugs")
        chklst_SmartOrders.Items.Add("Flowsheet")
    End Sub
    Private Sub chkLstBx_smartDiagnosis_Treatment_Order_fun(ByVal chkBx As CheckedListBox)

        chkBx.Items.Add("Drugs") ''
        chkBx.Items.Add("FlowSheet") ''
        chkBx.Items.Add("Orders and Results") ''
        chkBx.Items.Add("Order Templates") ''

    End Sub
#End Region

#Region "Added Rahul for Smart Diagnosis,Treatment,Orders Send Task on 20101022."
    Private Sub chk_SmartDiagnosis_SendTask_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_SmartDiagnosis_SendTask.Click
        If chk_SmartDiagnosis_SendTask.Checked Then
            chk_SmartDiagnosis_SendTask.Text = "Clear All"
            For i As Integer = 0 To chklst_SmartDiagnosisSendTask.Items.Count - 1
                chklst_SmartDiagnosisSendTask.SetItemChecked(i, True)
            Next
        Else
            chk_SmartDiagnosis_SendTask.Text = "Select All"
            For i As Integer = 0 To chklst_SmartDiagnosisSendTask.Items.Count - 1
                chklst_SmartDiagnosisSendTask.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub chklst_SmartDiagnosisSendTask_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chklst_SmartDiagnosisSendTask.MouseUp
        Try
            Dim chkall As Boolean = True

            For i As Integer = 0 To chklst_SmartDiagnosisSendTask.Items.Count - 1
                If chklst_SmartDiagnosisSendTask.GetItemCheckState(i) = False Then
                    chkall = False
                    Exit For
                End If
            Next

            'if items are selected by clicking checkbox ''
            If chkall = False Then
                chk_SmartDiagnosis_SendTask.Text = "Select All"
            Else
                chk_SmartDiagnosis_SendTask.Text = "Clear All"
            End If

            ' if items are selected individually ''
            If chk_SmartDiagnosis_SendTask.Text = "Select All" Then
                chk_SmartDiagnosis_SendTask.Checked = False
            Else
                chk_SmartDiagnosis_SendTask.Checked = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chk_SmartTreatment_SendTask_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_SmartTreatment_SendTask.Click
        If chk_SmartTreatment_SendTask.Checked Then
            chk_SmartTreatment_SendTask.Text = "Clear All"
            For i As Integer = 0 To chklst_SmartTreatmentSendTask.Items.Count - 1
                chklst_SmartTreatmentSendTask.SetItemChecked(i, True)
            Next
        Else
            chk_SmartTreatment_SendTask.Text = "Select All"
            For i As Integer = 0 To chklst_SmartTreatmentSendTask.Items.Count - 1
                chklst_SmartTreatmentSendTask.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub chklst_SmartTreatmentSendTask_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chklst_SmartTreatmentSendTask.MouseUp
        Try
            Dim chkall As Boolean = True

            For i As Integer = 0 To chklst_SmartTreatmentSendTask.Items.Count - 1
                If chklst_SmartTreatmentSendTask.GetItemCheckState(i) = False Then
                    chkall = False
                    Exit For
                End If
            Next

            ' if items are selected by clicking checkbox ''
            If chkall = False Then
                chk_SmartTreatment_SendTask.Text = "Select All"
            Else
                chk_SmartTreatment_SendTask.Text = "Clear All"
            End If

            ' if items are selected individually ''
            If chk_SmartTreatment_SendTask.Text = "Select All" Then
                chk_SmartTreatment_SendTask.Checked = False
            Else
                chk_SmartTreatment_SendTask.Checked = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chk_SmartOrder_SendTask_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_SmartOrder_SendTask.Click
        If chk_SmartOrder_SendTask.Checked Then
            chk_SmartOrder_SendTask.Text = "Clear All"
            For i As Integer = 0 To chklst_SmartOrdersSendTask.Items.Count - 1
                chklst_SmartOrdersSendTask.SetItemChecked(i, True)
            Next
        Else
            chk_SmartOrder_SendTask.Text = "Select All"
            For i As Integer = 0 To chklst_SmartOrdersSendTask.Items.Count - 1
                chklst_SmartOrdersSendTask.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub chklst_SmartOrdersSendTask_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chklst_SmartOrdersSendTask.MouseUp
        Try
            Dim chkall As Boolean = True

            For i As Integer = 0 To chklst_SmartOrdersSendTask.Items.Count - 1
                If chklst_SmartOrdersSendTask.GetItemCheckState(i) = False Then
                    chkall = False
                    Exit For
                End If
            Next

            ' if items are selected by clicking checkbox ''
            If chkall = False Then
                chk_SmartOrder_SendTask.Text = "Select All"
            Else
                chk_SmartOrder_SendTask.Text = "Clear All"
            End If

            ' if items are selected individually ''
            If chk_SmartOrder_SendTask.Text = "Select All" Then
                chk_SmartOrder_SendTask.Checked = False
            Else
                chk_SmartOrder_SendTask.Checked = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub CheckUserInformation(ByVal arrSmart As ArrayList)

        Dim objsqlconnection As SqlConnection = Nothing
        Dim _Result As Object
        Dim objsqlcommand As SqlCommand = Nothing
        Dim _strcon As String
        Dim _strdelete As String
        Dim _strinsert As String
        'Dim _nuserid As String
        Dim _bfieldstatus As Boolean
        Dim _nfieldsequence As Integer
        Dim _sfieldname As String
        Dim _sfieldtype As String
        Dim _bSendTask As String
        Dim _sUsers As String
        Dim _bAllowViewTask As String
        Dim _sUserID As String

        Try

            If arrSmart.Count > 0 Then
                _strcon = "select Count(*) from SmartConfig where nUserId='" & gnLoginID & "'"

                objsqlconnection = New SqlConnection(GetConnectionString())
                objsqlcommand = New SqlCommand(_strcon, objsqlconnection)
                objsqlconnection.Open()

                _Result = objsqlcommand.ExecuteScalar()


                If _Result > 0 Then
                    _strdelete = "Delete from SmartConfig where nUserId='" & gnLoginID & "'"
                    objsqlcommand.Parameters.Clear()
                    objsqlcommand.Dispose()
                    objsqlcommand = Nothing
                    objsqlcommand = New SqlCommand(_strdelete, objsqlconnection)
                    objsqlcommand.ExecuteNonQuery()
                End If


                For i As Integer = 0 To arrSmart.Count - 1
                    _sfieldname = CType(arrSmart.Item(i), myList).AssociatedItem
                    _bfieldstatus = CType(arrSmart.Item(i), myList).IsFinished
                    _nfieldsequence = CType(arrSmart.Item(i), myList).CPTCount
                    _sfieldtype = CType(arrSmart.Item(i), myList).AssociatedCategory
                    _bSendTask = CType(arrSmart.Item(i), myList).SendTask
                    _sUsers = CType(arrSmart.Item(i), myList).Value ''For Users
                    _bAllowViewTask = CType(arrSmart.Item(i), myList).ItemChecked  ''For AllowViewTask
                    _sUserID = CType(arrSmart.Item(i), myList).UserID


                    _strinsert = "INSERT INTO SmartConfig(nUserID, sFieldName, bFieldStatus, nFieldSequence, sFieldType, bSendTask, sTaskusers, bAllowviewtsk,sUserID) VALUES " _
                                 & " ('" & gnLoginID & "','" & _sfieldname & "','" & _bfieldstatus & "','" & _nfieldsequence & "','" & _sfieldtype & "','" & _bSendTask & "','" & _sUsers & "','" & _bAllowViewTask & "','" & _sUserID & "')"
                    objsqlcommand.Parameters.Clear()
                    objsqlcommand.Dispose()
                    objsqlcommand = Nothing
                    objsqlcommand = New SqlCommand(_strinsert, objsqlconnection)
                    objsqlcommand.ExecuteNonQuery()
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(objsqlconnection) = False) Then
                objsqlconnection.Close()
                objsqlconnection.Dispose()
                objsqlconnection = Nothing
            End If

            If objsqlcommand IsNot Nothing Then
                objsqlcommand.Parameters.Clear()
                objsqlcommand.Dispose()
                objsqlcommand = Nothing
            End If
        End Try
    End Sub

    Private Sub FillSmartData()

        Dim objsqlconnection As New SqlConnection(GetConnectionString())

        Dim objSDA As New SqlDataAdapter
        Dim dt As DataTable
        '        Dim _strcon As String

        Dim _sUserID As String = ""

        chk_SmartDiagnosis.Text = "Select All"
        chk_SmartDiagnosis_SendTask.Text = "Select All"
        chk_SmartTreatment.Text = "Select All"
        chk_SmartTreatment_SendTask.Text = "Select All"
        chk_SmartOrder.Text = "Select All"
        chk_SmartOrder_SendTask.Text = "Select All"





        Try

            '_strcon = "select ISNULL(nUserID,0) AS nUserID, ISNULL(sFieldName,'') AS sFieldName, ISNULL(bFieldStatus,'true') AS bFieldStatus, ISNULL(nFieldSequence,0) AS nFieldSequence, ISNULL(sFieldType,'') AS sFieldType, ISNULL(bSendTask,'false') AS bSendTask,ISNULL(sTaskusers,'') as sTaskusers,ISNULL(bAllowviewtsk,0) as bAllowviewtsk,ISNULL(sUserID,'') as sUserID from SmartConfig where nUserId='" & gnLoginID & "' AND sFieldType = 'SmartDX' Order by nFieldSequence"
            'objSDA = New SqlDataAdapter(_strcon, objsqlconnection)
            ''dt = New DataTable
            'objSDA.Fill(dt)
            'objSDA.Dispose()
            'objSDA = Nothing

            dt = dsSettings.Tables("SmartDX")
            FillSmartDxTask(dt, "SmartDX")
            dt.Dispose()
            dt = Nothing

            '_strcon = "select ISNULL(nUserID,0) AS nUserID, ISNULL(sFieldName,'') AS sFieldName, ISNULL(bFieldStatus,'true') AS bFieldStatus, ISNULL(nFieldSequence,0) AS nFieldSequence, ISNULL(sFieldType,'') AS sFieldType, ISNULL(bSendTask,'False') AS bSendTask,ISNULL(sTaskusers,'') as sTaskusers,ISNULL(bAllowviewtsk,0) as bAllowviewtsk,ISNULL(sUserID,'') as sUserID from SmartConfig where nUserId='" & gnLoginID & "' AND sFieldType = 'SmartCPT' Order by nFieldSequence"
            'objSDA = New SqlDataAdapter(_strcon, objsqlconnection)
            'dt = New DataTable
            'objSDA.Fill(dt)
            'objSDA.Dispose()
            'objSDA = Nothing

            dt = dsSettings.Tables("SmartCPT")
            FillSmartDxTask(dt, "SmartCPT")
            dt.Dispose()
            dt = Nothing

            '_strcon = "select ISNULL(nUserID,0) AS nUserID, ISNULL(sFieldName,'') AS sFieldName, ISNULL(bFieldStatus,'true') AS bFieldStatus, ISNULL(nFieldSequence,0) AS nFieldSequence, ISNULL(sFieldType,'') AS sFieldType, ISNULL(bSendTask,'False') AS bSendTask,ISNULL(sTaskusers,'') as sTaskusers,ISNULL(bAllowviewtsk,0) as bAllowviewtsk,ISNULL(sUserID,'') as sUserID from SmartConfig where nUserId='" & gnLoginID & "' AND sFieldType = 'SmartOrder' Order by nFieldSequence"
            'objSDA = New SqlDataAdapter(_strcon, objsqlconnection)
            'dt = New DataTable
            'objSDA.Fill(dt)
            'objSDA.Dispose()
            'objSDA = Nothing

            dt = dsSettings.Tables("SmartOrder")
            FillSmartDxTask(dt, "SmartOrder")
            dt.Dispose()
            dt = Nothing


            OnLoadCheck(chklst_SmartDiagnosis, btnSelectAllDX, btnClearAllDX)
            OnLoadCheck(chklst_SmartTreatment, Button14, btnClearAllCPT)
            OnLoadCheck(chklst_SmartOrders, Button13, btnClearAllOrder)



            OnLoadCheck(C1SmartDiagnosisSendTask, btnSmartDiagnosisSelect_All, btnSmartDiagnosisSelect_Cancel, 0)
            OnLoadCheck(C1SmartTreatmentSendTask, btnSmartTreatmentSelect_All, btnSmartTreatmentSelect_Cancel, 0)
            OnLoadCheck(C1SmartOrdersSendTask, btnSmartOrderSelect_All, btnSmartOrderSelect_Cancel, 0)




            OnLoadCheck(C1SmartDiagnosisSendTask, btnSmartDiagnosisView_All, btnSmartDiagnosisView_Cancel, 4)
            OnLoadCheck(C1SmartTreatmentSendTask, btnSmartTreatmentView_All, btnSmartTreatmentView_Cancel, 4)
            OnLoadCheck(C1SmartOrdersSendTask, btnSmartOrderView_All, btnSmartOrderView_Cancel, 4)



        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objsqlconnection.Close()
            objsqlconnection.Dispose()
            objsqlconnection = Nothing

        End Try

    End Sub
    ''Added Rahul For Send Task on 20101026
    Private Sub FillSmartDxTask(ByVal dt As DataTable, ByVal IsFrom As String)
        ''Set C1SmartDiagnosisSendTask
        If IsFrom = "SmartDX" Then
            With C1SmartDiagnosisSendTask
                Dim i As Int16
                .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0

                _TotalWidth = (.Width - 20) / 11
                .Cols.Count = COLUMN_COUNT
                '.Rows.Count = 1
                .AllowEditing = True

                .Rows.Fixed = 1
                .Styles.ClearUnused()

                .Cols(COL_ChkForm).Width = _TotalWidth * 1
                .SetData(0, COL_ChkForm, "")
                .Cols(COL_ChkForm).AllowEditing = True
                .Cols(COL_ChkForm).DataType = GetType(Boolean)


                .Cols(COL_FormNm).Width = _TotalWidth * 3
                .SetData(0, COL_FormNm, "Screen")
                .Cols(COL_FormNm).AllowEditing = False

                .Cols(COL_Users).Width = _TotalWidth * 2.5
                .SetData(0, COL_Users, "Users")
                .Cols(COL_Users).AllowEditing = True

                .Cols(COL_BrowseBtn).Width = _TotalWidth * 1
                .SetData(0, COL_BrowseBtn, "")
                .Cols(COL_BrowseBtn).AllowEditing = False

                .Cols(COL_ChkViewForm).Width = _TotalWidth * 3.8
                .SetData(0, COL_ChkViewForm, "Preview")
                .Cols(COL_ChkViewForm).AllowEditing = True
                .Cols(COL_ChkViewForm).DataType = GetType(Boolean)

                '' setting the UserID to Hiddencolumn
                .Cols(COL_Hidden).Width = _TotalWidth * 1.5
                .SetData(0, COL_Hidden, "")
                .Cols(COL_Hidden).AllowEditing = False
                .Cols(COL_Hidden).Visible = False
                '' setting the UserID to Hiddencolumn

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Drugs")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Flowsheet")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Orders and Results")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Order Templates")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                Dim strUser As String
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle
                Dim ocell As C1.Win.C1FlexGrid.CellRange
                chklst_SmartDiagnosis.Items.Clear()
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To .Rows.Count - 1
                            If dt.Rows(i)("sFieldName") = .GetData(j, COL_FormNm) Then
                                If Not IsNothing(dt.Rows(i)("sTaskusers")) Then
                                    If dt.Rows(i)("sTaskusers") <> "" Then
                                        If dt.Rows(i)("sTaskusers").ToString.Contains("|") Then
                                            strUser = dt.Rows(i)("sTaskusers")
                                            ' cstyle = .Styles.Add("BubbleValues" & j)
                                            Try
                                                If (.Styles.Contains("BubbleValues" & j)) Then
                                                    cstyle = .Styles("BubbleValues" & j)
                                                Else
                                                    cstyle = .Styles.Add("BubbleValues" & j)

                                                End If
                                            Catch ex As Exception
                                                cstyle = .Styles.Add("BubbleValues" & j)

                                            End Try
                                            cstyle.ComboList = strUser
                                            ocell = .GetCellRange(j, COL_Users, j, COL_Users)
                                            ocell.Style = cstyle

                                            Dim _Ulst() As String = dt.Rows(i)("sTaskusers").ToString.Split("|")
                                            If _Ulst.Length > 0 Then
                                                ocell.Data = _Ulst(0)
                                            End If

                                        Else
                                            .SetData(j, COL_Users, dt.Rows(i)("sTaskusers"))
                                        End If
                                    End If
                                End If

                                If Not IsNothing(dt.Rows(i)("bAllowviewtsk")) Then
                                    If dt.Rows(i)("bAllowviewtsk") <> False Then
                                        .SetCellCheck(j, COL_ChkViewForm, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    Else
                                        .SetCellCheck(j, COL_ChkViewForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                    End If
                                Else
                                    .SetCellCheck(j, COL_ChkViewForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                End If

                                If Not IsNothing(dt.Rows(i)("bSendTask")) Then
                                    If dt.Rows(i)("bSendTask") <> False Then
                                        .SetCellCheck(j, COL_ChkForm, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    Else
                                        .SetCellCheck(j, COL_ChkForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                    End If
                                Else
                                    .SetCellCheck(j, COL_ChkForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                End If

                                If Not IsNothing(dt.Rows(i)("sUserID")) Then
                                    If dt.Rows(i)("sUserID") <> "" Then
                                        .SetData(j, COL_Hidden, dt.Rows(i)("sUserID"))
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    .SetData(j, COL_Hidden, "")
                                End If
                            End If
                        Next
                        chklst_SmartDiagnosis.Items.Add(dt.Rows(i)("sFieldName").ToString().Replace("Referral Letter Templates", "Referral Letter"), dt.Rows(i)("bFieldStatus"))
                    Next
                Else
                    chkLstBx_smartDiagnosis_fun()
                    '''''FillDefaultUser(C1SmartDiagnosisSendTask)
                End If
            End With
        End If


        ''End C1SmartDiagnosisSendTask

        ''Set C1SmartTreatmentSendTask
        If IsFrom = "SmartCPT" Then
            With C1SmartTreatmentSendTask
                Dim i As Int16
                .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = (.Width - 20) / 11
                .Cols.Count = COLUMN_COUNT
                .Rows.Fixed = 1
                .AllowEditing = True
                .Styles.ClearUnused()

                .Cols(COL_ChkForm).Width = _TotalWidth * 1
                .SetData(0, COL_ChkForm, "")
                .Cols(COL_ChkForm).AllowEditing = True
                .Cols(COL_ChkForm).DataType = GetType(Boolean)


                .Cols(COL_FormNm).Width = _TotalWidth * 3
                .SetData(0, COL_FormNm, "Screen")
                .Cols(COL_FormNm).AllowEditing = False

                .Cols(COL_Users).Width = _TotalWidth * 2.5
                .SetData(0, COL_Users, "Users")
                .Cols(COL_Users).AllowEditing = True

                .Cols(COL_BrowseBtn).Width = _TotalWidth * 1
                .SetData(0, COL_BrowseBtn, "")
                .Cols(COL_BrowseBtn).AllowEditing = False

                .Cols(COL_ChkViewForm).Width = _TotalWidth * 3.8
                .SetData(0, COL_ChkViewForm, "Preview")
                .Cols(COL_ChkViewForm).AllowEditing = True
                .Cols(COL_ChkViewForm).DataType = GetType(Boolean)

                ''Storing the UserID
                .Cols(COL_Hidden).Width = _TotalWidth * 1.5
                .SetData(0, COL_Hidden, "")
                .Cols(COL_Hidden).AllowEditing = False
                .Cols(COL_Hidden).Visible = False
                ''Storing the UserID

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Drugs")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Flowsheet")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Orders and Results")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Order Templates")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                Dim strUser As String
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle
                Dim ocell As C1.Win.C1FlexGrid.CellRange
                chklst_SmartTreatment.Items.Clear()
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To .Rows.Count - 1
                            If dt.Rows(i)("sFieldName") = .GetData(j, COL_FormNm) Then
                                If Not IsNothing(dt.Rows(i)("sTaskusers")) Then
                                    If dt.Rows(i)("sTaskusers").ToString.Contains("|") Then
                                        strUser = dt.Rows(i)("sTaskusers")
                                        'cstyle = .Styles.Add("BubbleValues" & j)
                                        Try
                                            If (.Styles.Contains("BubbleValues" & j)) Then
                                                cstyle = .Styles("BubbleValues" & j)
                                            Else
                                                cstyle = .Styles.Add("BubbleValues" & j)

                                            End If
                                        Catch ex As Exception
                                            cstyle = .Styles.Add("BubbleValues" & j)

                                        End Try
                                        cstyle.ComboList = strUser
                                        ocell = .GetCellRange(j, COL_Users, j, COL_Users)
                                        ocell.Style = cstyle

                                        Dim _Ulst() As String = dt.Rows(i)("sTaskusers").ToString.Split("|")
                                        If _Ulst.Length > 0 Then
                                            ocell.Data = _Ulst(0)
                                        End If

                                    Else
                                        .SetData(j, COL_Users, dt.Rows(i)("sTaskusers"))
                                    End If
                                End If

                                If Not IsNothing(dt.Rows(i)("bAllowviewtsk")) Then
                                    If dt.Rows(i)("bAllowviewtsk") <> False Then
                                        .SetCellCheck(j, COL_ChkViewForm, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    Else
                                        .SetCellCheck(j, COL_ChkViewForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                    End If
                                Else
                                    .SetCellCheck(j, COL_ChkViewForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                End If

                                If Not IsNothing(dt.Rows(i)("bSendTask")) Then
                                    If dt.Rows(i)("bSendTask") <> False Then
                                        .SetCellCheck(j, COL_ChkForm, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    Else
                                        .SetCellCheck(j, COL_ChkForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                    End If
                                Else
                                    .SetCellCheck(j, COL_ChkForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                End If

                                If Not IsNothing(dt.Rows(i)("sUserID")) Then
                                    If dt.Rows(i)("sUserID") <> "" Then
                                        .SetData(j, COL_Hidden, dt.Rows(i)("sUserID"))
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    .SetData(j, COL_Hidden, "")
                                End If

                            End If
                        Next
                        chklst_SmartTreatment.Items.Add(dt.Rows(i)("sFieldName").ToString().Replace("Referral Letter Templates", "Referral Letter"), dt.Rows(i)("bFieldStatus"))
                    Next
                Else
                    chklst_SmartTreatment_fun()
                    ' '' ''FillDefaultUser(C1SmartTreatmentSendTask)
                End If
            End With
        End If
        ''End C1SmartTreatmentSendTask

        ''Set C1SmartOrdersSendTask
        If IsFrom = "SmartOrder" Then
            With C1SmartOrdersSendTask
                Dim i As Int16
                .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = (.Width - 20) / 11
                .Cols.Count = COLUMN_COUNT
                .Rows.Fixed = 1
                .AllowEditing = True


                .Styles.ClearUnused()

                .Cols(COL_ChkForm).Width = _TotalWidth * 1
                .SetData(0, COL_ChkForm, "")
                .Cols(COL_ChkForm).AllowEditing = True
                .Cols(COL_ChkForm).DataType = GetType(Boolean)


                .Cols(COL_FormNm).Width = _TotalWidth * 3
                .SetData(0, COL_FormNm, "Screen")
                .Cols(COL_FormNm).AllowEditing = False

                .Cols(COL_Users).Width = _TotalWidth * 2.5
                .SetData(0, COL_Users, "Users")
                .Cols(COL_Users).AllowEditing = True

                .Cols(COL_BrowseBtn).Width = _TotalWidth * 1
                .SetData(0, COL_BrowseBtn, "")
                .Cols(COL_BrowseBtn).AllowEditing = False

                .Cols(COL_ChkViewForm).Width = _TotalWidth * 3.8
                .SetData(0, COL_ChkViewForm, "Preview")
                .Cols(COL_ChkViewForm).AllowEditing = True
                .Cols(COL_ChkViewForm).DataType = GetType(Boolean)


                ''Storing the USerID
                .Cols(COL_Hidden).Width = _TotalWidth * 1.5
                .SetData(0, COL_Hidden, "")
                .Cols(COL_Hidden).AllowEditing = False
                .Cols(COL_Hidden).Visible = False
                ''Storing the USerID


                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Drugs")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Flowsheet")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Orders and Results")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_FormNm, "Order Templates")
                .SetCellImage(.Rows.Count - 1, COL_BrowseBtn, imgTreeVIew.Images(0))

                Dim strUser As String
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle
                ' cstyle = .Styles.Add("BubbleValues")
                Try
                    If (.Styles.Contains("BubbleValues")) Then
                        cstyle = .Styles("BubbleValues")
                    Else
                        cstyle = .Styles.Add("BubbleValues")

                    End If
                Catch ex As Exception
                    cstyle = .Styles.Add("BubbleValues")

                End Try
                Dim ocell As C1.Win.C1FlexGrid.CellRange
                chklst_SmartOrders.Items.Clear()
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To .Rows.Count - 1
                            If dt.Rows(i)("sFieldName") = .GetData(j, COL_FormNm) Then
                                If Not IsNothing(dt.Rows(i)("sTaskusers")) Then
                                    If dt.Rows(i)("sTaskusers").ToString.Contains("|") Then
                                        strUser = dt.Rows(i)("sTaskusers")
                                        ' cstyle = .Styles.Add("BubbleValues" & j)
                                        Try
                                            If (.Styles.Contains("BubbleValues" & j)) Then
                                                cstyle = .Styles("BubbleValues" & j)
                                            Else
                                                cstyle = .Styles.Add("BubbleValues" & j)

                                            End If
                                        Catch ex As Exception
                                            cstyle = .Styles.Add("BubbleValues" & j)

                                        End Try
                                        cstyle.ComboList = strUser
                                        ocell = .GetCellRange(j, COL_Users, j, COL_Users)
                                        ocell.Style = cstyle

                                        Dim _Ulst() As String = dt.Rows(i)("sTaskusers").ToString.Split("|")
                                        If _Ulst.Length > 0 Then
                                            ocell.Data = _Ulst(0)
                                        End If

                                    Else
                                        .SetData(j, COL_Users, dt.Rows(i)("sTaskusers"))
                                    End If
                                End If

                                If Not IsNothing(dt.Rows(i)("bAllowviewtsk")) Then
                                    If dt.Rows(i)("bAllowviewtsk") <> False Then
                                        .SetCellCheck(j, COL_ChkViewForm, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    Else
                                        .SetCellCheck(j, COL_ChkViewForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                    End If
                                Else
                                    .SetCellCheck(j, COL_ChkViewForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                End If

                                If Not IsNothing(dt.Rows(i)("bSendTask")) Then
                                    If dt.Rows(i)("bSendTask") <> False Then
                                        .SetCellCheck(j, COL_ChkForm, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    Else
                                        .SetCellCheck(j, COL_ChkForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                    End If
                                Else
                                    .SetCellCheck(j, COL_ChkForm, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                End If

                                If Not IsNothing(dt.Rows(i)("sUserID")) Then
                                    If dt.Rows(i)("sUserID") <> "" Then
                                        .SetData(j, COL_Hidden, dt.Rows(i)("sUserID"))
                                    Else
                                        .SetData(j, COL_Hidden, "")
                                    End If
                                Else
                                    .SetData(j, COL_Hidden, "")
                                End If

                            End If
                        Next
                        chklst_SmartOrders.Items.Add(dt.Rows(i)("sFieldName").ToString().Replace("Referral Letter Templates", "Referral Letter"), dt.Rows(i)("bFieldStatus"))
                    Next
                Else
                    chkLstBx_smartOrders_fun()
                    ' '' ''FillDefaultUser(C1SmartOrdersSendTask)
                End If
            End With
        End If
        ''End C1SmartOrdersSendTask
    End Sub

    Private Sub chk_SmartDiagnosis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_SmartDiagnosis.Click
        If chk_SmartDiagnosis.Checked Then
            chk_SmartDiagnosis.Text = "Clear All"
            For i As Integer = 0 To chklst_SmartDiagnosis.Items.Count - 1
                chklst_SmartDiagnosis.SetItemChecked(i, True)
            Next
        Else
            chk_SmartDiagnosis.Text = "Select All"
            For i As Integer = 0 To chklst_SmartDiagnosis.Items.Count - 1
                chklst_SmartDiagnosis.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub chklst_SmartDiagnosis_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chklst_SmartDiagnosis.MouseUp
        Try
            Dim chkall As Boolean = True

            For i As Integer = 0 To chklst_SmartDiagnosis.Items.Count - 1
                If chklst_SmartDiagnosis.GetItemCheckState(i) = False Then
                    chkall = False
                    Exit For
                End If
            Next

            ' if items are selected by clicking checkbox ''
            If chkall = False Then
                chk_SmartDiagnosis.Text = "Select All"
            Else
                chk_SmartDiagnosis.Text = "Clear All"
            End If

            ' if items are selected individually ''
            If chk_SmartDiagnosis.Text = "Select All" Then
                chk_SmartDiagnosis.Checked = False
            Else
                chk_SmartDiagnosis.Checked = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btn_UpDiagnosis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UpDiagnosis.Click
        Try
            upSmartSettings(chklst_SmartDiagnosis)
            'Dim tmpItem1, tmpItem2 As Object
            'Dim intIndex As Integer
            'If chklst_SmartDiagnosis.SelectedIndex > 0 Then
            '    intIndex = chklst_SmartDiagnosis.SelectedIndex
            '    tmpItem1 = chklst_SmartDiagnosis.Items.Item(intIndex)
            '    tmpItem2 = chklst_SmartDiagnosis.Items.Item(intIndex - 1)
            '    chklst_SmartDiagnosis.Items.RemoveAt(intIndex)
            '    chklst_SmartDiagnosis.Items.RemoveAt(intIndex - 1)
            '    chklst_SmartDiagnosis.Items.Insert(intIndex - 1, tmpItem2)
            '    chklst_SmartDiagnosis.Items.Insert(intIndex - 1, tmpItem1)
            '    chklst_SmartDiagnosis.SelectedItem = chklst_SmartDiagnosis.Items.Item(intIndex - 1)
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_DownDiagnosis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_DownDiagnosis.Click
        Try
            downSmartSettings(chklst_SmartDiagnosis)
            'If chklst_SmartDiagnosis.SelectedIndex > -1 Then
            '    Dim tmpItem1, tmpItem2 As Object
            '    Dim intIndex As Integer

            '    If chklst_SmartDiagnosis.SelectedIndex < chklst_SmartDiagnosis.Items.Count - 1 Then
            '        intIndex = chklst_SmartDiagnosis.SelectedIndex
            '        tmpItem1 = chklst_SmartDiagnosis.Items.Item(intIndex)
            '        tmpItem2 = chklst_SmartDiagnosis.Items.Item(intIndex + 1)
            '        chklst_SmartDiagnosis.Items.RemoveAt(intIndex)
            '        chklst_SmartDiagnosis.Items.RemoveAt(intIndex)
            '        chklst_SmartDiagnosis.Items.Insert(intIndex, tmpItem2)
            '        chklst_SmartDiagnosis.Items.Insert(intIndex + 1, tmpItem1)
            '        chklst_SmartDiagnosis.SelectedItem = chklst_SmartDiagnosis.Items.Item(intIndex + 1)

            '    End If

            'End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chk_SmartTreatment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_SmartTreatment.Click
        If chk_SmartTreatment.Checked Then
            chk_SmartTreatment.Text = "Clear All"
            For i As Integer = 0 To chklst_SmartTreatment.Items.Count - 1
                chklst_SmartTreatment.SetItemChecked(i, True)
            Next
        Else
            chk_SmartTreatment.Text = "Select All"
            For i As Integer = 0 To chklst_SmartTreatment.Items.Count - 1
                chklst_SmartTreatment.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub chklst_SmartTreatment_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chklst_SmartTreatment.MouseUp
        Try
            Dim chkall As Boolean = True

            For i As Integer = 0 To chklst_SmartTreatment.Items.Count - 1
                If chklst_SmartTreatment.GetItemCheckState(i) = False Then
                    chkall = False
                    Exit For
                End If
            Next

            ' if items are selected by clicking checkbox ''
            If chkall = False Then
                chk_SmartTreatment.Text = "Select All"
            Else
                chk_SmartTreatment.Text = "Clear All"
            End If

            'if items are selected individually ''
            If chk_SmartTreatment.Text = "Select All" Then
                chk_SmartTreatment.Checked = False
            Else
                chk_SmartTreatment.Checked = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btn_UpTreatment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UpTreatment.Click
        Try
            upSmartSettings(chklst_SmartTreatment)
            'Dim tmpItem1, tmpItem2 As Object
            'Dim intIndex As Integer
            'If chklst_SmartTreatment.SelectedIndex > 0 Then
            '    intIndex = chklst_SmartTreatment.SelectedIndex
            '    tmpItem1 = chklst_SmartTreatment.Items.Item(intIndex)
            '    tmpItem2 = chklst_SmartTreatment.Items.Item(intIndex - 1)
            '    chklst_SmartTreatment.Items.RemoveAt(intIndex)
            '    chklst_SmartTreatment.Items.RemoveAt(intIndex - 1)
            '    chklst_SmartTreatment.Items.Insert(intIndex - 1, tmpItem2)
            '    chklst_SmartTreatment.Items.Insert(intIndex - 1, tmpItem1)
            '    chklst_SmartTreatment.SelectedItem = chklst_SmartTreatment.Items.Item(intIndex - 1)
            'End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_DownTreatment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_DownTreatment.Click
        Try
            downSmartSettings(chklst_SmartTreatment)
            'If chklst_SmartTreatment.SelectedIndex > -1 Then

            '    Dim tmpItem1, tmpItem2 As Object
            '    Dim intIndex As Integer
            '    If chklst_SmartTreatment.SelectedIndex < chklst_SmartTreatment.Items.Count - 1 Then
            '        intIndex = chklst_SmartTreatment.SelectedIndex
            '        tmpItem1 = chklst_SmartTreatment.Items.Item(intIndex)
            '        tmpItem2 = chklst_SmartTreatment.Items.Item(intIndex + 1)
            '        chklst_SmartTreatment.Items.RemoveAt(intIndex)
            '        chklst_SmartTreatment.Items.RemoveAt(intIndex)
            '        chklst_SmartTreatment.Items.Insert(intIndex, tmpItem2)
            '        chklst_SmartTreatment.Items.Insert(intIndex + 1, tmpItem1)
            '        chklst_SmartTreatment.SelectedItem = chklst_SmartTreatment.Items.Item(intIndex + 1)
            '    End If

            'End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chk_SmartOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_SmartOrder.Click
        If chk_SmartOrder.Checked Then
            chk_SmartOrder.Text = "Clear All"
            For i As Integer = 0 To chklst_SmartOrders.Items.Count - 1
                chklst_SmartOrders.SetItemChecked(i, True)
            Next
        Else
            chk_SmartOrder.Text = "Select All"
            For i As Integer = 0 To chklst_SmartOrders.Items.Count - 1
                chklst_SmartOrders.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub chklst_SmartOrders_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chklst_SmartOrders.MouseUp
        Try
            Dim chkall As Boolean = True

            For i As Integer = 0 To chklst_SmartOrders.Items.Count - 1
                If chklst_SmartOrders.GetItemCheckState(i) = False Then
                    chkall = False
                    Exit For
                End If
            Next

            ' if items are selected by clicking checkbox ''
            If chkall = False Then
                chk_SmartOrder.Text = "Select All"
            Else
                chk_SmartOrder.Text = "Clear All"
            End If

            ' if items are selected individually ''
            If chk_SmartOrder.Text = "Select All" Then
                chk_SmartOrder.Checked = False
            Else
                chk_SmartOrder.Checked = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btn_UpOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UpOrders.Click
        Try
            upSmartSettings(chklst_SmartOrders)
            'Dim tmpItem1, tmpItem2 As Object
            'Dim intIndex As Integer
            'If chklst_SmartOrders.SelectedIndex > 0 Then
            '    intIndex = chklst_SmartOrders.SelectedIndex
            '    tmpItem1 = chklst_SmartOrders.Items.Item(intIndex)
            '    tmpItem2 = chklst_SmartOrders.Items.Item(intIndex - 1)
            '    chklst_SmartOrders.Items.RemoveAt(intIndex)
            '    chklst_SmartOrders.Items.RemoveAt(intIndex - 1)
            '    chklst_SmartOrders.Items.Insert(intIndex - 1, tmpItem2)
            '    chklst_SmartOrders.Items.Insert(intIndex - 1, tmpItem1)
            '    chklst_SmartOrders.SelectedItem = chklst_SmartOrders.Items.Item(intIndex - 1)
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_DownOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_DownOrders.Click
        Try
            downSmartSettings(chklst_SmartOrders)
            'If chklst_SmartOrders.SelectedIndex > -1 Then

            '    Dim tmpItem1, tmpItem2 As Object
            '    Dim intIndex As Integer
            '    If chklst_SmartOrders.SelectedIndex < chklst_SmartOrders.Items.Count - 1 Then
            '        intIndex = chklst_SmartOrders.SelectedIndex
            '        tmpItem1 = chklst_SmartOrders.Items.Item(intIndex)
            '        tmpItem2 = chklst_SmartOrders.Items.Item(intIndex + 1)
            '        chklst_SmartOrders.Items.RemoveAt(intIndex)
            '        chklst_SmartOrders.Items.RemoveAt(intIndex)
            '        chklst_SmartOrders.Items.Insert(intIndex, tmpItem2)
            '        chklst_SmartOrders.Items.Insert(intIndex + 1, tmpItem1)
            '        chklst_SmartOrders.SelectedItem = chklst_SmartOrders.Items.Item(intIndex + 1)
            '    End If

            'End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

    Private Sub C1SmartDiagnosisSendTask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1SmartDiagnosisSendTask.Click
        ' Rahul Added
        Dim strUsers As String = ""
        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
        With C1SmartDiagnosisSendTask
            Dim r As Integer = .RowSel
            Try
                If r >= 0 Then
                    If .ColSel = COL_BrowseBtn Then
                        eItemSelected = ItemClick.DX
                        nSelectedRow = r
                        LoadUserGrid()
                        If Not IsNothing(.GetCellStyle(r, COL_Users)) Then
                            ostyle = .GetCellStyle(r, COL_Users)
                            strUsers = ostyle.ComboList
                            SetCheckValues(strUsers)
                        Else
                            If Not IsNothing(.GetData(r, COL_Users)) Then
                                If .GetData(r, COL_Users) <> "" Then
                                    strUsers = .GetData(r, COL_Users)
                                    SetCheckValues(strUsers)
                                End If
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception

            End Try

        End With
    End Sub
    Private Sub SetCheckValues(ByVal struser As String)
        Dim strusers() As String
        strusers = struser.Split("|")
        For k As Integer = 0 To strusers.Length - 1
            For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.GetItem(i, 2).ToString.Trim = strusers.GetValue(k) Then
                    dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Exit For
                End If
            Next
        Next
    End Sub
    Private Sub LoadUserGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                PnlCustomTask.Width = 300
                PnlCustomTask.Height = 220
                '  dgCustomGrid.Width = pnlcustomTask.Width
                'pnlcustomTask.Width = dgCustomGrid.Width
                ' dgCustomGrid.Height = pnlcustomTask.Height
                dgCustomGrid.txtsearch.Width = 120

                dgCustomGrid.BringToFront()
                ' dgCustomGrid.SetVisible = False
                BindUserGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddControl()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomTask
        dgCustomGrid.Dock = DockStyle.Fill
        PnlCustomTask.Controls.Add(dgCustomGrid)
        PnlCustomTask.BringToFront()
        dgCustomGrid.tsbtn_New.Visible = False
        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250

        PnlCustomTask.Location = New Point(300, PnlCustomTask.Location.Y)
        PnlCustomTask.Visible = True
        dgCustomGrid.Visible = True

        dgCustomGrid.BringToFront()
        PnlCustomTask.BringToFront()
    End Sub


    Private Sub BindUserGrid()
        Try
            ' Dim dt As DataTable
            'Dim objclsPatientHistory As New clsPatientHistory

            Dim dt As DataTable
            dt = FillUsers()


            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                ''dt.Columns("sICD9Display").Caption = "Diagnosis Name"
                dgCustomGrid.datasource(dt.DefaultView)
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True

            ''Check box
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.1

            ''nUserID
            dgCustomGrid.C1Task.Cols(1).Visible = False

            ''Login Name
            dgCustomGrid.C1Task.Cols(2).Visible = True
            dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 2

            dgCustomGrid.Visible = True
            'Dim r As Integer = C1HistoryDetails.RowSel
            'If Not IsNothing(C1HistoryDetails.GetData(r, Col_Reaction)) Then
            '    If C1HistoryDetails.GetData(r, Col_Reaction).ToString().Trim() <> "" Then
            '        CheckDGCustomGrid()
            '    End If
            'End If
            'dgCustomGrid.C1Task.Cols(2).AllowEditing = True
            ' dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            'dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0.5
            '  UserCount = dt.Rows.Count
            'objclsPatientHistory.Dispose()
            'objclsPatientHistory = Nothing
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Show Reports", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function FillUsers() As DataTable
        Dim oDB As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim _strSQL As String = ""
        Dim dt As DataTable
        Try

            '_strSQL = "Select sLoginName + ' - ' + isnull(sfirstname,'') + Space(1) + isnull(slastname,'') as [Login Name] from User_MST order by sLoginName "
            _strSQL = "Select nUserID as [UserID],sLoginName AS [Login Name] from User_MST order by sLoginName "
            dt = oDB.GetDataTable_Query(_strSQL)
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
    Public Sub CustomDrugsGridStyle()

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_Count
            .AllowEditing = True

            .SetData(0, Col_Check, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Check).Width = _TotalWidth * 0.1
            .Cols(Col_Check).AllowEditing = True
            .Cols(Col_Check).DataType = System.Type.GetType("System.Boolean")

            .SetData(0, Col_Name, "User Name")
            .Cols(Col_Name).Width = _TotalWidth * 0.45
            ' .Cols(Col_DrugName).AllowEditing = False

        End With

    End Sub



    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            'pnlWordObj.Controls.Remove(dgCustomGrid)
            PnlCustomTask.Controls.Remove(dgCustomGrid)
            ' dgCustomGrid.Visible = False
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
        End If
    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        dgCustomGrid.Visible = False
        PnlCustomTask.Visible = False
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        strUserList = ""
        strUserID = ""
        For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
            If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                If strUserList = "" Then
                    strUserID = dgCustomGrid.C1Task.GetData(i, 1)
                    strUserList = dgCustomGrid.C1Task.GetData(i, 2)
                Else
                    strUserID = strUserID & "|" & dgCustomGrid.C1Task.GetData(i, 1)
                    strUserList = strUserList & "|" & dgCustomGrid.C1Task.GetData(i, 2)
                End If
            End If

        Next
        PnlCustomTask.Visible = False

        If strUserList <> "" And strUserID <> "" Then
            Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing

            If strUserList.Contains("|") Then
                If eItemSelected = ItemClick.DX Then
                    ' cstyle = C1SmartDiagnosisSendTask.Styles.Add("BubbleValues" & nSelectedRow)
                    Try
                        If (C1SmartDiagnosisSendTask.Styles.Contains("BubbleValues" & nSelectedRow)) Then
                            cstyle = C1SmartDiagnosisSendTask.Styles("BubbleValues" & nSelectedRow)
                        Else
                            cstyle = C1SmartDiagnosisSendTask.Styles.Add("BubbleValues" & nSelectedRow)

                        End If
                    Catch ex As Exception
                        cstyle = C1SmartDiagnosisSendTask.Styles.Add("BubbleValues" & nSelectedRow)

                    End Try
                ElseIf eItemSelected = ItemClick.CPT Then
                    '  cstyle = C1SmartTreatmentSendTask.Styles.Add("BubbleValues" & nSelectedRow)
                    Try
                        If (C1SmartTreatmentSendTask.Styles.Contains("BubbleValues" & nSelectedRow)) Then
                            cstyle = C1SmartTreatmentSendTask.Styles("BubbleValues" & nSelectedRow)
                        Else
                            cstyle = C1SmartTreatmentSendTask.Styles.Add("BubbleValues" & nSelectedRow)

                        End If
                    Catch ex As Exception
                        cstyle = C1SmartTreatmentSendTask.Styles.Add("BubbleValues" & nSelectedRow)

                    End Try

                ElseIf eItemSelected = ItemClick.Orders Then
                    'cstyle = C1SmartOrdersSendTask.Styles.Add("BubbleValues" & nSelectedRow)
                    Try
                        If (C1SmartOrdersSendTask.Styles.Contains("BubbleValues" & nSelectedRow)) Then
                            cstyle = C1SmartOrdersSendTask.Styles("BubbleValues" & nSelectedRow)
                        Else
                            cstyle = C1SmartOrdersSendTask.Styles.Add("BubbleValues" & nSelectedRow)

                        End If
                    Catch ex As Exception
                        cstyle = C1SmartOrdersSendTask.Styles.Add("BubbleValues" & nSelectedRow)

                    End Try
                End If
            End If

            Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
            If strUserList.Contains("|") Then
                cstyle.ComboList = strUserList
                If eItemSelected = ItemClick.DX Then
                    C1SmartDiagnosisSendTask.SetData(nSelectedRow, COL_Users, "")
                    ocell = C1SmartDiagnosisSendTask.GetCellRange(nSelectedRow, COL_Users, nSelectedRow, COL_Users)
                ElseIf eItemSelected = ItemClick.CPT Then
                    C1SmartTreatmentSendTask.SetData(nSelectedRow, COL_Users, "")
                    ocell = C1SmartTreatmentSendTask.GetCellRange(nSelectedRow, COL_Users, nSelectedRow, COL_Users)
                ElseIf eItemSelected = ItemClick.Orders Then
                    C1SmartOrdersSendTask.SetData(nSelectedRow, COL_Users, "")
                    ocell = C1SmartOrdersSendTask.GetCellRange(nSelectedRow, COL_Users, nSelectedRow, COL_Users)
                End If

                ocell.Style = cstyle
                Dim splstruser As String() = strUserList.Split("|")
                If splstruser.Length > 0 Then
                    ocell.Data = splstruser(0)
                End If

            Else

                Dim ocellrng As C1.Win.C1FlexGrid.CellRange
                If eItemSelected = ItemClick.DX Then
                    ocellrng = C1SmartDiagnosisSendTask.GetCellRange(nSelectedRow, COL_Users)
                    ocellrng.Style = Nothing
                    C1SmartDiagnosisSendTask.SetData(nSelectedRow, COL_Users, strUserList)
                ElseIf eItemSelected = ItemClick.CPT Then
                    ocellrng = C1SmartTreatmentSendTask.GetCellRange(nSelectedRow, COL_Users)
                    ocellrng.Style = Nothing
                    C1SmartTreatmentSendTask.SetData(nSelectedRow, COL_Users, strUserList)
                ElseIf eItemSelected = ItemClick.Orders Then
                    ocellrng = C1SmartOrdersSendTask.GetCellRange(nSelectedRow, COL_Users)
                    ocellrng.Style = Nothing
                    C1SmartOrdersSendTask.SetData(nSelectedRow, COL_Users, strUserList)
                End If

            End If




            If eItemSelected = ItemClick.DX Then
                C1SmartDiagnosisSendTask.SetData(nSelectedRow, COL_Hidden, strUserID)

            ElseIf eItemSelected = ItemClick.CPT Then
                C1SmartTreatmentSendTask.SetData(nSelectedRow, COL_Hidden, strUserID)
            ElseIf eItemSelected = ItemClick.Orders Then
                C1SmartOrdersSendTask.SetData(nSelectedRow, COL_Hidden, strUserID)
            End If

        Else
            Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
            If eItemSelected = ItemClick.DX Then
                ocell = C1SmartDiagnosisSendTask.GetCellRange(nSelectedRow, COL_Users, nSelectedRow, COL_Users)
                C1SmartDiagnosisSendTask.SetData(nSelectedRow, COL_Hidden, "")
                C1SmartDiagnosisSendTask.SetData(nSelectedRow, COL_Users, "")
            ElseIf eItemSelected = ItemClick.CPT Then
                ocell = C1SmartTreatmentSendTask.GetCellRange(nSelectedRow, COL_Users, nSelectedRow, COL_Users)
                C1SmartTreatmentSendTask.SetData(nSelectedRow, COL_Hidden, "")
                C1SmartTreatmentSendTask.SetData(nSelectedRow, COL_Users, "")
            ElseIf eItemSelected = ItemClick.Orders Then
                ocell = C1SmartOrdersSendTask.GetCellRange(nSelectedRow, COL_Users, nSelectedRow, COL_Users)
                C1SmartOrdersSendTask.SetData(nSelectedRow, COL_Hidden, "")
                C1SmartOrdersSendTask.SetData(nSelectedRow, COL_Users, "")
            End If

            ocell.Style = Nothing
        End If
    End Sub

    Private Sub C1SmartOrdersSendTask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1SmartOrdersSendTask.Click
        ' Rahul Added
        Dim strUsers As String = ""
        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
        With C1SmartOrdersSendTask
            Dim r As Integer = .RowSel
            Try
                If r >= 0 Then
                    If .ColSel = COL_BrowseBtn Then
                        eItemSelected = ItemClick.Orders
                        nSelectedRow = r
                        LoadUserGrid()
                        If Not IsNothing(.GetCellStyle(r, COL_Users)) Then
                            ostyle = .GetCellStyle(r, COL_Users)
                            strUsers = ostyle.ComboList
                            SetCheckValues(strUsers)
                        Else
                            If Not IsNothing(.GetData(r, COL_Users)) Then
                                If .GetData(r, COL_Users) <> "" Then
                                    strUsers = .GetData(r, COL_Users)
                                    SetCheckValues(strUsers)
                                End If
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception

            End Try

        End With
    End Sub

    Private Sub C1SmartTreatmentSendTask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1SmartTreatmentSendTask.Click
        ' Rahul Added
        Dim strUsers As String = ""
        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
        With C1SmartTreatmentSendTask
            Dim r As Integer = .RowSel
            Try
                If r >= 0 Then
                    If .ColSel = COL_BrowseBtn Then
                        eItemSelected = ItemClick.CPT
                        nSelectedRow = r
                        LoadUserGrid()
                        If Not IsNothing(.GetCellStyle(r, COL_Users)) Then
                            ostyle = .GetCellStyle(r, COL_Users)
                            strUsers = ostyle.ComboList
                            SetCheckValues(strUsers)
                        Else
                            If Not IsNothing(.GetData(r, COL_Users)) Then
                                If .GetData(r, COL_Users) <> "" Then
                                    strUsers = .GetData(r, COL_Users)
                                    SetCheckValues(strUsers)
                                End If
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception

            End Try

        End With
    End Sub

#Region "UP/Down Logic"
    Private Sub upSmartSettings(ByVal chkBx As CheckedListBox)
        Dim _nextIndex As Integer
        Dim _currentIndex As Integer
        Dim _oItem As Object
        Dim _isChecked As Boolean = False
        If chkBx.Items.Count >= 0 Then
            If chkBx.SelectedIndex >= 0 Then

                _currentIndex = chkBx.SelectedIndex
                If chkBx.GetItemCheckState(_currentIndex) = CheckState.Checked Then
                    _isChecked = True
                End If
                _oItem = chkBx.Items.Item(_currentIndex)
                If _currentIndex = 0 Then
                    Return
                Else
                    _nextIndex = chkBx.SelectedIndex - 1
                End If
                chkBx.Items.Remove(_oItem)
                chkBx.Items.Insert(_nextIndex, _oItem)
                chkBx.SelectedItem = _oItem

            End If
            If _isChecked = True Then
                chkBx.SetItemChecked(_nextIndex, True)
            Else

                chkBx.SetItemChecked(_nextIndex, False)
            End If
        End If
    End Sub

    Private Sub downSmartSettings(ByVal chkBx As CheckedListBox)
        Dim _nextIndex As Integer
        Dim _currentIndex As Integer
        Dim _oItem As Object
        Dim _isChecked As Boolean = False
        If chkBx.Items.Count >= 0 Then
            If chkBx.SelectedIndex >= 0 Then

                _currentIndex = chkBx.SelectedIndex
                If chkBx.GetItemCheckState(_currentIndex) = CheckState.Checked Then
                    _isChecked = True
                End If
                _oItem = chkBx.Items.Item(_currentIndex)
                If _currentIndex = chkBx.Items.Count - 1 Then
                    Return
                Else
                    _nextIndex = chkBx.SelectedIndex + 1
                End If
                chkBx.Items.Remove(_oItem)
                chkBx.Items.Insert(_nextIndex, _oItem)
                chkBx.SelectedItem = _oItem
            End If
            If _isChecked = True Then
                chkBx.SetItemChecked(_nextIndex, True)
            Else

                chkBx.SetItemChecked(_nextIndex, False)
            End If
        End If
    End Sub
#End Region

#Region "Filling the Data on the Tab Click"
    'Private Sub tc_settings_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tc_Settings.Click
    '    Dim mystring As String = tc_settings.selectedtab.name
    '    If mystring = "tb_smartsettings" Then
    '        fillsmartdata()
    '    End If
    'End Sub

    Public Sub OnLoadCheck(ByVal btnSelectAllVisiblity As Button, ByVal btnClearAllVisisbility As Button)
        btnSelectAllVisiblity.Visible = True
        btnClearAllVisisbility.Visible = False
    End Sub


    Public Sub OnLoadCheck(ByVal chkLstBox As CheckedListBox, ByVal btnSelectAllVisiblity As Button, ByVal btnClearAllVisisbility As Button)
        Dim _isChecked As Boolean = False
        Dim iCount As Integer = 0
        Dim iChkItemCount = chkLstBox.Items.Count - 1
        For i As Integer = 0 To iChkItemCount
            If chkLstBox.GetItemCheckState(i) = CheckState.Checked Then
                _isChecked = True
                iCount = iCount + 1
            End If
        Next
        iCount -= 1
        If iCount = iChkItemCount Then
            btnSelectAllVisiblity.Visible = False
            btnClearAllVisisbility.Visible = True
        ElseIf iCount = -1 Then
            btnSelectAllVisiblity.Visible = True
            btnClearAllVisisbility.Visible = False
        Else
            btnSelectAllVisiblity.Visible = True
            btnClearAllVisisbility.Visible = False
            'btnSelectAllVisiblity.Visible = True
            'btnClearAllVisisbility.Visible = True
        End If

    End Sub

    Public Sub OnLoadCheck(ByVal myC1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal btnSelectAllVisiblity As Button, ByVal btnClearAllVisisbility As Button, ByVal myColumn As Integer)
        Dim _isChecked As Boolean = False
        Dim iCount As Integer = 0
        Dim iChkItemCount = myC1FlexGrid.Rows.Count - 1
        For i As Integer = 1 To iChkItemCount
            If myC1FlexGrid.GetCellCheck(i, myColumn) = CheckState.Checked Then
                _isChecked = True
                iCount = iCount + 1
            End If
        Next

        If iCount = iChkItemCount Then
            btnSelectAllVisiblity.Visible = False
            btnClearAllVisisbility.Visible = True
        ElseIf iCount = 0 Then
            btnSelectAllVisiblity.Visible = True
            btnClearAllVisisbility.Visible = False
        Else
            btnSelectAllVisiblity.Visible = True
            btnClearAllVisisbility.Visible = False
            'btnSelectAllVisiblity.Visible = True
            'btnClearAllVisisbility.Visible = True
        End If

    End Sub

    Public Sub selectAll(ByVal chkLstBox As CheckedListBox, ByVal btnSelectAllVisiblity As Button, ByVal btnClearAllVisisbility As Button)
        For i As Integer = 0 To chkLstBox.Items.Count - 1
            chkLstBox.SetItemChecked(i, True)
        Next
        btnSelectAllVisiblity.Visible = False
        btnClearAllVisisbility.Visible = True
    End Sub
    Public Sub clearAll(ByVal chkLstBox As CheckedListBox, ByVal btnSelectAllVisiblity As Button, ByVal btnClearAllVisisbility As Button)
        For i As Integer = 0 To chkLstBox.Items.Count - 1
            chkLstBox.SetItemChecked(i, False)
        Next
        btnSelectAllVisiblity.Visible = True
        btnClearAllVisisbility.Visible = False
    End Sub

    Private Sub btnSelectAllDX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAllDX.Click
        selectAll(chklst_SmartDiagnosis, btnSelectAllDX, btnClearAllDX)

    End Sub

    Private Sub btnClearAllDX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAllDX.Click
        clearAll(chklst_SmartDiagnosis, btnSelectAllDX, btnClearAllDX)
    End Sub
    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        selectAll(chklst_SmartTreatment, Button14, btnClearAllCPT)

    End Sub

    Private Sub btnClearAllCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAllCPT.Click
        clearAll(chklst_SmartTreatment, Button14, btnClearAllCPT)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        selectAll(chklst_SmartOrders, Button13, btnClearAllOrder)

    End Sub

    Private Sub btnClearAllOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAllOrder.Click
        clearAll(chklst_SmartOrders, Button13, btnClearAllOrder)

    End Sub
#End Region

#Region "Select/Clear all Task"

    Public Sub selectAllGrid(ByVal MyC1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal myColumn As Integer, ByVal btnSelectAllVisiblity As Button, ByVal btnClearAllVisisbility As Button)
        For i As Integer = 1 To MyC1FlexGrid.Rows.Count - 1
            MyC1FlexGrid.SetCellCheck(i, myColumn, C1.Win.C1FlexGrid.CheckEnum.Checked)
        Next
        btnSelectAllVisiblity.Visible = False
        btnClearAllVisisbility.Visible = True
    End Sub

    Public Sub clearAllGrid(ByVal MyC1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal myColumn As Integer, ByVal btnSelectAllVisiblity As Button, ByVal btnClearAllVisisbility As Button)
        For i As Integer = 1 To MyC1FlexGrid.Rows.Count - 1
            MyC1FlexGrid.SetCellCheck(i, myColumn, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
        Next
        btnSelectAllVisiblity.Visible = True
        btnClearAllVisisbility.Visible = False
    End Sub

    ''-----------------------------------------------------------------------------



    Private Sub btnSmartDiagnosisSelect_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartDiagnosisSelect_All.Click
        selectAllGrid(C1SmartDiagnosisSendTask, 0, btnSmartDiagnosisSelect_All, btnSmartDiagnosisSelect_Cancel)
    End Sub

    Private Sub btnSmartDiagnosisSelect_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartDiagnosisSelect_Cancel.Click
        clearAllGrid(C1SmartDiagnosisSendTask, 0, btnSmartDiagnosisSelect_All, btnSmartDiagnosisSelect_Cancel)
    End Sub

    Private Sub btnSmartTreatmentSelect_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartTreatmentSelect_All.Click
        selectAllGrid(C1SmartTreatmentSendTask, 0, btnSmartTreatmentSelect_All, btnSmartTreatmentSelect_Cancel)

    End Sub

    Private Sub btnSmartTreatmentSelect_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartTreatmentSelect_Cancel.Click
        clearAllGrid(C1SmartTreatmentSendTask, 0, btnSmartTreatmentSelect_All, btnSmartTreatmentSelect_Cancel)
    End Sub

    Private Sub btnSmartOrderSelect_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartOrderSelect_All.Click
        selectAllGrid(C1SmartOrdersSendTask, 0, btnSmartOrderSelect_All, btnSmartOrderSelect_Cancel)

    End Sub

    Private Sub btnSmartOrderSelect_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartOrderSelect_Cancel.Click
        clearAllGrid(C1SmartOrdersSendTask, 0, btnSmartOrderSelect_All, btnSmartOrderSelect_Cancel)
    End Sub


    ''------------------------------------------------------------------------------------------------------------------------






    Private Sub btnSmartDiagnosisView_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartDiagnosisView_All.Click
        selectAllGrid(C1SmartDiagnosisSendTask, 4, btnSmartDiagnosisView_All, btnSmartDiagnosisView_Cancel)
    End Sub

    Private Sub btnSmartDiagnosisView_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartDiagnosisView_Cancel.Click
        clearAllGrid(C1SmartDiagnosisSendTask, 4, btnSmartDiagnosisView_All, btnSmartDiagnosisView_Cancel)
    End Sub

    Private Sub btnSmartTreatmentView_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartTreatmentView_All.Click
        selectAllGrid(C1SmartTreatmentSendTask, 4, btnSmartTreatmentView_All, btnSmartTreatmentView_Cancel)

    End Sub

    Private Sub btnSmartTreatmentView_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartTreatmentView_Cancel.Click
        clearAllGrid(C1SmartTreatmentSendTask, 4, btnSmartTreatmentView_All, btnSmartTreatmentView_Cancel)
    End Sub

    Private Sub btnSmartOrderView_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartOrderView_All.Click
        selectAllGrid(C1SmartOrdersSendTask, 4, btnSmartOrderView_All, btnSmartOrderView_Cancel)
    End Sub

    Private Sub btnSmartOrderView_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmartOrderView_Cancel.Click
        clearAllGrid(C1SmartOrdersSendTask, 4, btnSmartOrderView_All, btnSmartOrderView_Cancel)
    End Sub
#End Region

    Private Sub chksethighlightcolr_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chksethighlightcolr.CheckedChanged
        If chksethighlightcolr.Checked = True Then
            cmbHighlight.Enabled = True
        Else
            cmbHighlight.Enabled = False
        End If
    End Sub

    Private Sub dgCustomGrid_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.SearchChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView
            dvPatient = CType(dgCustomGrid.C1Task.DataSource(), DataView) '' (CType(dt.DefaultView, DataView))
            'CType(, DataView)
            'CType(dgCustomGrid.datasource(dt.DefaultView), DataView)
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

            dvPatient.RowFilter = "[" & dvPatient.Table.Columns(1).ColumnName & "] Like '%" & strPatientSearchDetails & "%' "
            'OR " _
            '                                                 & dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
            '                                                & dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' "



            dgCustomGrid.Enabled = False
            dgCustomGrid.datasource(dvPatient)
            dgCustomGrid.Enabled = True
            Me.Cursor = Cursors.Default
            dgCustomGrid.txtsearch.Focus()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function getPatientConfidentialInfo() As Boolean

        'Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        'Dim _strSQL As String = ""
        Dim _Result As Boolean
        Dim obj As Object = Nothing

        Try

            ' oDBLayer.Connect(False)
            '_strSQL = "select isnull(sSettingsValue,'False') as sSettingsValue from settings where sSettingsName='IsShowPatientConfiInfo'"

            If dsSettings.Tables("IsShowPatientConfiInfo").Rows.Count > 0 Then
                obj = dsSettings.Tables("IsShowPatientConfiInfo").Rows(0)("sSettingsValue")
            End If

            'Dim obj As Object = oDBLayer.ExecuteScalar_Query(_strSQL)

            If Not obj Is Nothing AndAlso obj.ToString.Trim() <> "" Then
                _Result = CType(obj, Boolean)
                Return _Result
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        Finally
            ' oDBLayer.Disconnect()
            'oDBLayer.Dispose()
            ' oDBLayer = Nothing
        End Try
    End Function
    Private Sub num_NoofColOnCalndr_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles num_NoofColOnCalndr.KeyDown
        nonNumberEntered = False


        If e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9 Then

            If e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9 Then

                If e.KeyCode <> Keys.Back Or e.KeyCode = Keys.[Decimal] Then
                    nonNumberEntered = True
                End If
            End If
        End If

        If Control.ModifierKeys = Keys.Shift Then
            nonNumberEntered = True
        End If

    End Sub

    Private Sub num_NoofColOnCalndr_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles num_NoofColOnCalndr.KeyPress
        If nonNumberEntered = True Then
            e.Handled = True
        End If
    End Sub

    '' CR00000126 : FAX for Terminal Server
    '' Browse button added for FAXDownloadDirectory, new setting added for Terminal server case
    Private Sub btnBrowseDownloadPath_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseDownloadPath.Click
        Try
            With FolderBrowserDialog1
                .ShowNewFolderButton = True
                .Description = "Select FAX Download Directory"
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    txtFaxDownloadPath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "SigPlus"
    ' Problem #28303: 00000156 : Signature Pad not working on Terminal Server
    Private Sub SetSigPlusSettings()
        Try
            gloRegistrySetting.SetRegistryValue("SigPlusSupportTS", cbSigPlusTS.Checked)
            gblnSigPlusSupportTS = cbSigPlusTS.Checked

            gloRegistrySetting.SetRegistryValue("SigPlusTabletPortPath", Trim(txtTabletPortPath.Text))
            gstrSigPlusTabletPortPath = Trim(txtTabletPortPath.Text)

            gloRegistrySetting.SetRegistryValue("SigPlusTabletType", Trim(txtTabletType.Text))
            If Trim(txtTabletType.Text) <> "" Then
                gshortSigPlusTabletType = Trim(txtTabletType.Text)
            End If

            gloRegistrySetting.SetRegistryValue("SigPlusLocalSignaturePad", chkLocalSigature.Checked)
            gblnLocalSignaturePad = chkLocalSigature.Checked

            gblnIsSigPlusSettingsAvailable = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            gblnIsSigPlusSettingsAvailable = False
        End Try

    End Sub
    Private Sub GetSigPlusSettings()
        Try
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)

            '' Support for TS
            If IsNothing(gloRegistrySetting.GetRegistryValue("SigPlusSupportTS")) Then
                cbSigPlusTS.Checked = False
            Else
                cbSigPlusTS.Checked = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("SigPlusSupportTS"))
            End If

            '' Tablet Port Path for SigPlus
            If IsNothing(gloRegistrySetting.GetRegistryValue("SigPlusTabletPortPath")) Then
                txtTabletPortPath.Text = ""
            Else
                txtTabletPortPath.Text = Convert.ToString(gloRegistrySetting.GetRegistryValue("SigPlusTabletPortPath"))
            End If

            '' Tablet Type for SigPlus
            If IsNothing(gloRegistrySetting.GetRegistryValue("SigPlusTabletType")) Then
                If cbSigPlusTS.Checked And gloRegistrySetting.IsServerOS Then
                    txtTabletType.Text = "7"    '' Default for TS
                Else
                    txtTabletType.Text = "6"    '' Default for Normal OS
                End If

            Else
                txtTabletType.Text = Convert.ToString(gloRegistrySetting.GetRegistryValue("SigPlusTabletType"))
            End If

            'Local Signature
            If IsNothing(gloRegistrySetting.GetRegistryValue("SigPlusLocalSignaturePad")) Then
                chkLocalSigature.Checked = False
            Else
                chkLocalSigature.Checked = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("SigPlusLocalSignaturePad"))
            End If

            gloRegistrySetting.CloseRegistryKey()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub cbSigPlusTS_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbSigPlusTS.CheckedChanged
        If cbSigPlusTS.Checked Then
            If gloRegistrySetting.IsServerOS Then
                txtTabletType.Text = "7"    '' Default for TS
            Else
                txtTabletType.Text = "6"    '' Default for Normal OS
            End If
            txtTabletPortPath.Enabled = True
        Else
            txtTabletType.Text = "6"    '' Default for Normal OS
            txtTabletPortPath.Enabled = False
        End If
    End Sub
#End Region

#Region "Grry Screen issue setting"

    Private Sub SetGreyScreenIssueSettings()
        Try
            gloRegistrySetting.SetRegistryValue("GreyScreenIssue", chkGreyScreenIssue.Checked)
            gblnGreyScreenIssue = chkGreyScreenIssue.Checked

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GetGreyScreenIssueSettings()
        Try
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)

            If IsNothing(gloRegistrySetting.GetRegistryValue("GreyScreenIssue")) Then
                chkGreyScreenIssue.Checked = False
            Else
                chkGreyScreenIssue.Checked = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("GreyScreenIssue"))
            End If

            gloRegistrySetting.CloseRegistryKey()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#End Region

    Private Sub chkAutoSaveExam_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAutoSaveExam.CheckedChanged
        Me.numAutoSaveMinutes.Enabled = Me.chkAutoSaveExam.Checked
        Me.numAutoSaveMinutes.Value = 1
    End Sub

    Private Sub chkEnableLocalPrinter_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkEnableLocalPrinter.CheckedChanged
        If chkEnableLocalPrinter.Checked = False Then
            chkAddFooterService.Checked = False
            chkAddFooterService.Enabled = False
            cmbNoPagesSplit.Enabled = False
            If cmbNoPagesSplit.Items.Count > 0 Then
                cmbNoPagesSplit.SelectedIndex = 0
            End If
            cmbNoTemplatesJob.Enabled = False
            If cmbNoTemplatesJob.Items.Count > 1 Then
                cmbNoTemplatesJob.SelectedIndex = 1
            End If
            rbPrintWordDocPDF.Enabled = False
            rbPrintWordDocEMF.Enabled = False
            rbPrintSSRSReportPDF.Enabled = False
            rbPrintSSRSReportEMF.Enabled = False
            rbPrintClaimsPDF.Enabled = False
            rbPrintClaimsEMF.Enabled = False
            rbPrintImagesPNG.Enabled = False
            rbPrintImagesEMF.Enabled = False
            chkZipMetadata.Checked = False
            chkZipMetadata.Enabled = False
        ElseIf chkEnableLocalPrinter.Checked = True Then
            chkAddFooterService.Enabled = True
            cmbNoPagesSplit.Enabled = True
            cmbNoTemplatesJob.Enabled = True
            rbPrintWordDocPDF.Enabled = True
            rbPrintWordDocEMF.Enabled = True
            rbPrintSSRSReportPDF.Enabled = True
            rbPrintSSRSReportEMF.Enabled = True
            rbPrintClaimsPDF.Enabled = True
            rbPrintClaimsEMF.Enabled = True
            rbPrintImagesPNG.Enabled = True
            rbPrintImagesEMF.Enabled = True
            chkZipMetadata.Enabled = True
            chkZipMetadata.Checked = gloGlobal.gloTSPrint.UseZippedMetadata
        End If
    End Sub

    Private Sub rbPrintWordDocPDF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPrintWordDocPDF.CheckedChanged

        If rbPrintWordDocPDF.Checked = True Then
            rbPrintWordDocPDF.Font = gloGlobal.clsgloFont.gFont_BOLD
        Else
            rbPrintWordDocPDF.Font = gloGlobal.clsgloFont.gFont
        End If

    End Sub

    Private Sub rbPrintWordDocEMF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPrintWordDocEMF.CheckedChanged
        If rbPrintWordDocEMF.Checked = True Then
            rbPrintWordDocEMF.Font = gloGlobal.clsgloFont.gFont_BOLD
        Else
            rbPrintWordDocEMF.Font = gloGlobal.clsgloFont.gFont
        End If
    End Sub

    Private Sub rbPrintSSRSReportPDF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPrintSSRSReportPDF.CheckedChanged
        If rbPrintSSRSReportPDF.Checked = True Then
            rbPrintSSRSReportPDF.Font = gloGlobal.clsgloFont.gFont_BOLD
        Else
            rbPrintSSRSReportPDF.Font = gloGlobal.clsgloFont.gFont
        End If
    End Sub

    Private Sub rbPrintSSRSReportEMF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPrintSSRSReportEMF.CheckedChanged
        If rbPrintSSRSReportEMF.Checked = True Then
            rbPrintSSRSReportEMF.Font = gloGlobal.clsgloFont.gFont_BOLD
        Else
            rbPrintSSRSReportEMF.Font = gloGlobal.clsgloFont.gFont
        End If
    End Sub

    Private Sub rbPrintClaimsPDF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPrintClaimsPDF.CheckedChanged
        If rbPrintClaimsPDF.Checked = True Then
            rbPrintClaimsPDF.Font = gloGlobal.clsgloFont.gFont_BOLD
        Else
            rbPrintClaimsPDF.Font = gloGlobal.clsgloFont.gFont
        End If
    End Sub

    Private Sub rbPrintClaimsEMF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPrintClaimsEMF.CheckedChanged
        If rbPrintClaimsEMF.Checked = True Then
            rbPrintClaimsEMF.Font = gloGlobal.clsgloFont.gFont_BOLD
        Else
            rbPrintClaimsEMF.Font = gloGlobal.clsgloFont.gFont
        End If
    End Sub

    Private Sub rbPrintImagesPNG_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPrintImagesPNG.CheckedChanged
        If rbPrintImagesPNG.Checked = True Then
            rbPrintImagesPNG.Font = gloGlobal.clsgloFont.gFont_BOLD
        Else
            rbPrintImagesPNG.Font = gloGlobal.clsgloFont.gFont
        End If
    End Sub

    Private Sub rbPrintImagesEMF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPrintImagesEMF.CheckedChanged
        If rbPrintImagesEMF.Checked = True Then
            rbPrintImagesEMF.Font = gloGlobal.clsgloFont.gFont_BOLD
        Else
            rbPrintImagesEMF.Font = gloGlobal.clsgloFont.gFont
        End If
    End Sub


    Private Sub btnEnableRemoteScanner_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub chkEnableRemoteScanner_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub btnEnableRemoteScanner_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnRefreshScanners.MouseHover, btnRefreshTwainScanners.MouseHover
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_LongYellow
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try

    End Sub

    Private Sub btnEnableRemoteScanner_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnRefreshScanners.MouseLeave, btnRefreshTwainScanners.MouseLeave
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_LongButton
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub

    Dim bChkForBrightness As Boolean = False
    Dim bChkForContrast As Boolean = False
    Dim BrightnessScale As Int64 = 0
    Dim ContrastScale As Int64 = 0

    Private Sub cmbRemoteScanner_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRemoteScanner.SelectedIndexChanged
        If cmbRemoteScanner.SelectedIndex = -1 Then
            Return
        End If


        If oRemoteScanCommon Is Nothing Then
            oRemoteScanCommon = New gloEDocumentV3.Common.RemoteScanCommon()
        End If
        Dim currScanner As Int64 = Convert.ToInt64(cmbRemoteScanner.SelectedValue)
        Dim sCurrRemoteScanMode As String = GetRegistryValue(gloRegistrySetting.gstrRemoteScanMode)
        Dim indexi As Int32 = 0
        Dim iRowCnt As Int32 = 0
        'ScanMode
        dtScanMode = New DataTable()
        dtScanMode.Columns.Add("ScanModeId", GetType(String))
        dtScanMode.Columns.Add("ScanModeName", GetType(String))
        cmbRemoteScanMode.DataSource = Nothing

        If Not gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode Is Nothing Then
            For i As Integer = 0 To gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode.Length - 1
                If oRemoteScanCommon.GetXMLTagNameForMode(sCurrRemoteScanMode) = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode(i).Name Then
                    indexi = iRowCnt 'Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode(i).ScanModeID)
                End If
                dtScanMode.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode(i).ScanModeID, oRemoteScanCommon.GetXMLTagNameForMode(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode(i).Name, True))
                iRowCnt = iRowCnt + 1
            Next
            cmbRemoteScanMode.ValueMember = "ScanModeId"
            cmbRemoteScanMode.DisplayMember = "ScanModeName"
            cmbRemoteScanMode.DataSource = dtScanMode

            cmbRemoteScanMode.SelectedIndex = indexi

        End If

        'ScanMode -End
        ' inCmbBoxSelection = False
        'cmbScanMode_SelectedIndexChanged(null, null);
        cmbRemoteScanMode_SelectedIndexChanged(Nothing, Nothing)

        'Scan Resolution

        Dim sCurrRemoteScanResol As String = GetRegistryValue(gloRegistrySetting.gstrRemoteScanResol)
        indexi = -1
        iRowCnt = 0

        dtResolution = New DataTable()
        dtResolution.Columns.Add("ResolutionId", GetType(String))
        dtResolution.Columns.Add("ResolutionName", GetType(String))
        cmbRemoteResolution.DataSource = Nothing


        If Not gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Resolution Is Nothing Then
            Dim iStep As Int32

            If (cmbRemoteScanner.Text.StartsWith("WIA")) Then
                If gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Resolution.Length > 25 Then
                    iStep = 25
                Else
                    iStep = 1
                End If
            Else
                If gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Resolution.Length > 50 Then
                    iStep = 50
                Else
                    iStep = 1
                End If
            End If

            For i As Integer = 0 To gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Resolution.Length - 1 Step iStep
                If sCurrRemoteScanResol = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Resolution(i).Name Then
                    indexi = iRowCnt 'Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Resolution(i).ResolutionID)
                End If

                dtResolution.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Resolution(i).ResolutionID, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Resolution(i).Name)
                iRowCnt = iRowCnt + 1
            Next
            cmbRemoteResolution.ValueMember = "ResolutionId"
            cmbRemoteResolution.DisplayMember = "ResolutionName"
            cmbRemoteResolution.DataSource = dtResolution

            If indexi = -1 Then
                If dtResolution IsNot Nothing AndAlso dtResolution.Rows.Count > 0 Then
                    indexi = (dtResolution.Rows.Count) / 2
                    cmbRemoteResolution.SelectedIndex = indexi
                End If
            Else
                cmbRemoteResolution.SelectedIndex = indexi
            End If
            ''cmbRemoteResolution.SelectedIndex = indexi
            'cmbRemoteResolution.SelectedValue = indexi
        End If
        'Scan Resolution -End


        'Scan Brightness

        Dim sCurrRemoteScanBright As String = GetRegistryValue(gloRegistrySetting.gstrRemoteScanBright)
        indexi = -1
        iRowCnt = 0

        dtBrightness = New DataTable()
        dtBrightness.Columns.Add("BrightnessId", GetType(String))
        dtBrightness.Columns.Add("BrightnessName", GetType(String))
        cmbRemoteBrightness.DataSource = Nothing
        Dim iBrightnessName As Int64 = 0
        bChkForBrightness = False

        If Not gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness Is Nothing Then

            'Int64.TryParse(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness(0).Name, BrightnessScale)
            'If BrightnessScale < 0 Then
            '    BrightnessScale = -(BrightnessScale) + 1
            '    bChkForBrightness = True
            'End If

            For i As Integer = 0 To gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness.Length - 1
                If sCurrRemoteScanBright = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness(i).Name Then
                    indexi = iRowCnt ' Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness(i).BrightnessID)
                End If

                If bChkForBrightness Then
                    iBrightnessName = Convert.ToInt64(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness(i).Name) + BrightnessScale
                    dtBrightness.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness(i).BrightnessID, Convert.ToString(iBrightnessName))
                    iBrightnessName = 0
                Else
                    dtBrightness.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness(i).BrightnessID, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness(i).Name)
                End If
                iRowCnt = iRowCnt + 1
                'dtBrightness.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness(i).BrightnessID, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Brightness(i).Name)
            Next
            cmbRemoteBrightness.ValueMember = "BrightnessId"
            cmbRemoteBrightness.DisplayMember = "BrightnessName"
            cmbRemoteBrightness.DataSource = dtBrightness

            If indexi = -1 Then
                If dtBrightness IsNot Nothing AndAlso dtBrightness.Rows.Count > 0 Then
                    indexi = (dtBrightness.Rows.Count) / 2
                    cmbRemoteBrightness.SelectedIndex = indexi
                End If
            Else
                cmbRemoteBrightness.SelectedIndex = indexi
            End If
            'cmbRemoteBrightness.SelectedIndex = indexi
        End If
        'Scan Brightness -End

        'Scan Contrast

        Dim sCurrRemoteScanContrast As String = GetRegistryValue(gloRegistrySetting.gstrRemoteScanContrast)
        indexi = -1
        iRowCnt = 0
        Dim iContrastName As Int64 = 0
        bChkForContrast = False

        dtContrast = New DataTable()
        dtContrast.Columns.Add("ContrastId", GetType(String))
        dtContrast.Columns.Add("ContrastName", GetType(String))
        cmbRemoteContrast.DataSource = Nothing

        If Not gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast Is Nothing Then

            'Int64.TryParse(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast(0).Name, ContrastScale)
            'If ContrastScale < 0 Then
            '    ContrastScale = -(ContrastScale) + 1
            '    bChkForContrast = True
            'End If

            For i As Integer = 0 To gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast.Length - 1
                If sCurrRemoteScanContrast = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast(i).Name Then
                    indexi = iRowCnt 'Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast(i).ContrastID)
                End If

                If bChkForContrast Then
                    iContrastName = Convert.ToInt64(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast(i).Name) + ContrastScale
                    dtContrast.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast(i).ContrastID, Convert.ToString(iContrastName))
                    iContrastName = 0
                Else
                    dtContrast.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast(i).ContrastID, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast(i).Name)
                End If
                'dtContrast.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast(i).ContrastID, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).Contrast(i).Name)
                iRowCnt = iRowCnt + 1
            Next
            cmbRemoteContrast.ValueMember = "ContrastId"
            cmbRemoteContrast.DisplayMember = "ContrastName"
            cmbRemoteContrast.DataSource = dtContrast

            If indexi = -1 Then
                If dtContrast IsNot Nothing AndAlso dtContrast.Rows.Count > 0 Then
                    indexi = (dtContrast.Rows.Count) / 2
                    cmbRemoteContrast.SelectedIndex = indexi
                End If
            Else
                cmbRemoteContrast.SelectedIndex = indexi
            End If
            'cmbRemoteContrast.SelectedIndex = indexi
        End If

        'Scan Contrast -End


        ' Scan Side

        Dim sCurrRemoteScanSide As String = GetRegistryValue(gloRegistrySetting.gstrRemoteScanSide)
        indexi = 0
        iRowCnt = 0
        dtScanSide = New DataTable()
        dtScanSide.Columns.Add("ScanSideID", GetType(String))
        dtScanSide.Columns.Add("ScanSideName", GetType(String))
        cmbRemoteScanSide.DataSource = Nothing

        If Not gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanSide Is Nothing Then

            For i As Integer = 0 To gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanSide.Length - 1
                If sCurrRemoteScanSide = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanSide(i).Name Then
                    indexi = iRowCnt 'Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanSide(i).ScanSideID)
                End If

                dtScanSide.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanSide(i).ScanSideID, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanSide(i).Name)
                iRowCnt = iRowCnt + 1
            Next
            cmbRemoteScanSide.ValueMember = "ScanSideID"
            cmbRemoteScanSide.DisplayMember = "ScanSideName"
            cmbRemoteScanSide.DataSource = dtScanSide

            cmbRemoteScanSide.SelectedIndex = indexi
            cmbRemoteScanSide_SelectedIndexChanged(Nothing, Nothing)
        End If

        Try
            If cmbRemoteScanSideFeeder.Items.Count < 1 Then
                FillFeederCombo()
            End If
            Dim sCurrRemoteScanSideFeeder As String = GetRegistryValue(gloRegistrySetting.gstrDMSScanFeeder)
            If Not (String.IsNullOrEmpty(sCurrRemoteScanSideFeeder)) Then
                Dim currIndex As Integer = cmbRemoteScanSideFeeder.FindStringExact(sCurrRemoteScanSideFeeder)
                If (currIndex < 0) Then
                    currIndex = 0
                End If
                cmbRemoteScanSideFeeder.SelectedIndex = currIndex
            Else
                If cmbRemoteScanSide.Text.ToLower() = "duplex" Then
                    cmbRemoteScanSideFeeder.Text = "Feeder"
                Else
                    cmbRemoteScanSideFeeder.SelectedIndex = 0
                End If
            End If
        Catch

        End Try
        ' Scan Side - End

        'Scan SupportedSizes

        'From Registry
        txtRemoteCardLength.Text = GetRegistryValue(gloRegistrySetting.gstrRemoteCardLength)
        txtRemoteCardWidth.Text = GetRegistryValue(gloRegistrySetting.gstrRemoteCardWidth)
        txtRemoteStartX.Text = GetRegistryValue(gloRegistrySetting.gstrRemoteCardLeftX)
        txtRemoteStartY.Text = GetRegistryValue(gloRegistrySetting.gstrRemoteCardTopY)

        Dim sCurrRemoteSupporedSize As String = GetRegistryValue(gloRegistrySetting.gstrRemoteSupporedSize)
        indexi = 0
        iRowCnt = 0
        dtSupportedSizes = New DataTable()
        dtSupportedSizes.Columns.Add("SupportedSizeID", GetType(String))
        dtSupportedSizes.Columns.Add("SupportedSizeName", GetType(String))
        dtSupportedSizes.Columns.Add("Length", GetType(String))
        dtSupportedSizes.Columns.Add("Left", GetType(String))
        dtSupportedSizes.Columns.Add("Top", GetType(String))
        dtSupportedSizes.Columns.Add("Width", GetType(String))
        cmbRemoteSupportedSize.DataSource = Nothing

        If Not gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).SupportedSize Is Nothing Then

            For i As Integer = 0 To gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).SupportedSize.Length - 1
                If sCurrRemoteSupporedSize = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).SupportedSize(i).Name Then
                    indexi = iRowCnt 'Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).SupportedSize(i).SupportedSizeID)
                End If

                dtSupportedSizes.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).SupportedSize(i).SupportedSizeID, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).SupportedSize(i).Name, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).SupportedSize(i).Length, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).SupportedSize(i).Left, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).SupportedSize(i).Top, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).SupportedSize(i).Width)
                iRowCnt = iRowCnt + 1
            Next
            cmbRemoteSupportedSize.ValueMember = "SupportedSizeID"
            cmbRemoteSupportedSize.DisplayMember = "SupportedSizeName"
            cmbRemoteSupportedSize.DataSource = dtSupportedSizes

            cmbRemoteSupportedSize.SelectedIndex = indexi

            'Scan SupportedSizes -End

        End If

    End Sub

    Private Sub FillFeederCombo()
        cmbRemoteScanSideFeeder.Items.Clear()
        cmbRemoteScanSideFeeder.Items.Add("Default")
        cmbRemoteScanSideFeeder.Items.Add("Flatbed")
        cmbRemoteScanSideFeeder.Items.Add("Feeder")
        cmbRemoteScanSideFeeder.SelectedIndex = 0
    End Sub

    Private Sub cmbRemoteScanMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRemoteScanMode.SelectedIndexChanged
        If cmbRemoteScanMode.SelectedIndex = -1 Then
            Return
        End If

        Dim sCurrRemoteScanDepth As String = GetRegistryValue(gloRegistrySetting.gstrRemoteScanDepth)
        Dim indexi As Int32 = 0
        Dim iRowCnt As Int32 = 0
        Dim currScanner As Int64 = Convert.ToInt64(cmbRemoteScanner.SelectedValue)
        Dim currScanMode As Int64 = Convert.ToInt64(cmbRemoteScanMode.SelectedValue)

        dtScanDepth = New DataTable()
        dtScanDepth.Columns.Add("ScanDepthId", GetType(String))
        dtScanDepth.Columns.Add("ScanDepthName", GetType(String))
        cmbRemoteBitDepth.DataSource = Nothing

        If Not gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode(currScanMode).ScanDepth Is Nothing Then
            For i As Integer = 0 To gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode(currScanMode).ScanDepth.Length - 1
                If sCurrRemoteScanDepth = gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode(currScanMode).ScanDepth(i).Name Then
                    indexi = iRowCnt 'Convert.ToInt32(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode(currScanMode).ScanDepth(i).ScanDepthId)
                End If
                dtScanDepth.Rows.Add(gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode(currScanMode).ScanDepth(i).ScanDepthId, gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings.Scanner(currScanner).ScanMode(currScanMode).ScanDepth(i).Name)
                iRowCnt = iRowCnt + 1
            Next
            cmbRemoteBitDepth.ValueMember = "ScanDepthId"
            cmbRemoteBitDepth.DisplayMember = "ScanDepthName"
            cmbRemoteBitDepth.DataSource = dtScanDepth

            cmbRemoteBitDepth.SelectedIndex = indexi

        End If

    End Sub

    Private Sub cmbRemoteBitDepth_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRemoteBitDepth.SelectedIndexChanged

    End Sub

    Private Sub cmbRemoteResolution_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRemoteResolution.SelectedIndexChanged

    End Sub

    Private Sub cmbRemoteSupportedSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRemoteSupportedSize.SelectedIndexChanged
        If cmbRemoteSupportedSize.SelectedIndex = -1 Then
            Return
        End If

        'Dim currSupportedSize As Int64 = Convert.ToInt64(cmbRemoteSupportedSize.SelectedValue)

        'If dtSupportedSizes IsNot Nothing Then
        '    Dim results As DataRow() = dtSupportedSizes.[Select]("SupportedSizeID='" + Convert.ToString(currSupportedSize) + "'")
        '    If results.Length <> 0 Then
        '        txtRemoteCardWidth.Text = Convert.ToString(results(0)("Width"))
        '        txtRemoteCardLength.Text = Convert.ToString(results(0)("Length"))
        '        txtRemoteStartX.Text = Convert.ToString(results(0)("Left"))
        '        txtRemoteStartY.Text = Convert.ToString(results(0)("Top"))
        '    Else
        '    End If
        'End If

    End Sub

    Private Sub btnRefreshScanners_Click(sender As System.Object, e As System.EventArgs) Handles btnRefreshScanners.Click

        Try
            Me.Cursor = Cursors.WaitCursor
            btnRefreshScanners.Enabled = False
            Application.DoEvents()

            If Not gloGlobal.gloRemoteScanSettings.isScanServiceWorking(showMsg:=True) Then
                Return
            End If

            If gloRemoteScanGeneral.RemoteScanSettings.RefreshScanners() = True Then
                Dim sRetVal As String = Nothing
                'Current Settings
                sRetVal = oRemoteScanCommon.SetRemoteScannerCurrentSettings(Nothing, Nothing, Nothing)
                If Not String.IsNullOrEmpty(sRetVal) Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, sRetVal, gloAuditTrail.ActivityOutCome.Failure)
                End If

				gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject()
				
                If CallRemoteScanSettingsLoad() Then
                    'Update clients machine name
                    Dim myRemoteMachine As gloAuditTrail.MachineDetails.MachineInfo = gloAuditTrail.MachineDetails.RemoteMachineDetails(True)
                    gloGlobal.gloTSPrint.sClientLocalMachineName = myRemoteMachine.MachineName

                    MessageBox.Show("Scanners Refreshed", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Unable to refresh Scanners. One/multiple of the following might be the possible reason(s)." + Environment.NewLine + "1. Unable to retrieve scanner list." + Environment.NewLine + "2. Unable to write scanner list to configuration file.", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("Unable to update scanner list, Please try after some time.", "Local Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally

            Me.Cursor = Cursors.[Default]
            btnRefreshScanners.Enabled = True
            Application.DoEvents()
        End Try


    End Sub

    Private Sub chkEnableRemoteScanner_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEnableRemoteScanner.CheckedChanged

        If (gloGlobal.gloTSPrint.TerminalServer() = "RDP") AndAlso (gloGlobal.gloRemoteScanSettings.isScanServiceWorking()) Then
            If Not chkEnableRemoteScanner.Checked Then
                chkEliminatePegasus.Enabled = True
                chkEliminatePegasus.Checked = gloGlobal.gloEliminatePegasus.bEliminatePegasus
                chkEliminatePegasus.Visible = True

                chkZipScannerSettings.Enabled = False
                chkZipScannerSettings.Checked = False
            Else
                chkEliminatePegasus.Enabled = False
                chkEliminatePegasus.Checked = False
                chkEliminatePegasus.Visible = False

                chkZipScannerSettings.Enabled = True
                chkZipScannerSettings.Checked = gloGlobal.gloRemoteScanSettings.bZipScanSettings
            End If
        End If
        SwitchSettingsPanels()


        If chkEnableRemoteScanner.Checked Then
            cmbRemoteImageFormat.Enabled = False
        Else
            'Unchecked
            'cmbRemoteImageFormat.Enabled = true;
            'cmbRemoteImageFormat.BringToFront();
            If chkEliminatePegasus.Checked Then
                cmbRemoteImageFormat.Enabled = True
                cmbRemoteImageFormat.BringToFront()
            Else
                cmbRemoteImageFormat.Enabled = False
            End If
        End If

        btnRefreshScanners.Visible = chkEnableRemoteScanner.Checked
        'pnlRemoteScan.Enabled = chkEnableRemoteScanner.Checked
        'GroupBox3.Enabled = Not chkEnableRemoteScanner.Checked

        'pnlRemoteScan.Visible = chkEnableRemoteScanner.Checked
        'GroupBox3.Visible = Not chkEnableRemoteScanner.Checked
        DisposingTwain()

        If chkEnableRemoteScanner.Checked Then
            gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings = Nothing
            CallRemoteScanSettingsLoad()
        Else
            InitPagasusTwainDevice()

            objScannerSettings.GetAndSetScanners(twainDevice, cmbScanner, gloRegistrySetting.gstrDMSScan)
            objScannerSettings.ObtainScannerSettings(twainDevice, cmbScanner, cmbScanner, cmbScanMode, cmbBitDepth, cmbResolution, _
             cmbBrightness, cmbContrast, cmbScanSide, chkShowScannerDialog, cmbSupportedSize, txtCardWidth, _
             txtCardLength, txtStartX, txtStartY, myScanLayout)
        End If

    End Sub

    Private Sub SwitchSettingsPanels()
        If Not chkEnableRemoteScanner.Checked AndAlso Not chkEliminatePegasus.Checked Then
            GroupBox3.Enabled = True
            GroupBox3.Visible = True
            pnlRemoteScan.Enabled = False
            pnlRemoteScan.Visible = False
        ElseIf chkEnableRemoteScanner.Checked OrElse chkEliminatePegasus.Checked Then
            GroupBox3.Enabled = False
            GroupBox3.Visible = False
            pnlRemoteScan.Enabled = True
            pnlRemoteScan.Visible = True
        End If
    End Sub

    Private Sub txtRemoteCardWidth_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemoteCardWidth.KeyPress

        If (e.KeyChar > ChrW(47)) AndAlso (e.KeyChar < ChrW(58)) OrElse e.KeyChar = ChrW(8) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtRemoteCardLength_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemoteCardLength.KeyPress

        If (e.KeyChar > ChrW(47)) AndAlso (e.KeyChar < ChrW(58)) OrElse e.KeyChar = ChrW(8) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtRemoteStartX_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemoteStartX.KeyPress

        If (e.KeyChar > ChrW(47)) AndAlso (e.KeyChar < ChrW(58)) OrElse e.KeyChar = ChrW(8) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtRemoteStartY_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemoteStartY.KeyPress

        If (e.KeyChar > ChrW(47)) AndAlso (e.KeyChar < ChrW(58)) OrElse e.KeyChar = ChrW(8) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub chkEliminatePegasus_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEliminatePegasus.CheckedChanged

        SwitchSettingsPanels()

        If (gloGlobal.gloTSPrint.TerminalServer() = "RDP") Then

        Else
        End If

        DisposingTwain()

        If chkEliminatePegasus.Checked Then
            btnRefreshTwainScanners.Visible = True

            cmbRemoteImageFormat.Enabled = True
            cmbRemoteImageFormat.BringToFront()
            gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings = Nothing
            CallRemoteScanSettingsLoad()
        Else
            cmbRemoteImageFormat.Enabled = False
            btnRefreshTwainScanners.Visible = False

            InitPagasusTwainDevice()

            objScannerSettings.GetAndSetScanners(twainDevice, cmbScanner, gloRegistrySetting.gstrDMSScan)
            objScannerSettings.ObtainScannerSettings(twainDevice, cmbScanner, cmbScanner, cmbScanMode, cmbBitDepth, cmbResolution, cmbBrightness, cmbContrast, cmbScanSide, chkShowScannerDialog, cmbSupportedSize, txtCardWidth, txtCardLength, txtStartX, txtStartY, myScanLayout)

            setPegasusBrightNContrast()
            bChkPegasusValues = True
            'objScannerSettings.GetAndSetScanners(twainDevice, cmbScanner, gloRegistrySetting.gstrDMSScan)
            'objScannerSettings.ObtainScannerSettings(twainDevice, cmbScanner, cmbScanner, cmbScanMode, cmbBitDepth, cmbResolution, _
            ' cmbBrightness, cmbContrast, cmbScanSide, chkShowScannerDialog, cmbSupportedSize, txtCardWidth, _
            ' txtCardLength, txtStartX, txtStartY, myScanLayout)
        End If
    End Sub

    Private Sub setPegasusBrightNContrast()
        'Set previous pegasus brightness
        Try
            gloGlobal.gloEliminatePegasus.setComboIndex(gloGlobal.gloEliminatePegasus.sPegasusBright, cmbBrightness)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try

        'Set previous pegasus Contrast
        Try
            gloGlobal.gloEliminatePegasus.setComboIndex(gloGlobal.gloEliminatePegasus.sPegasusContrast, cmbContrast)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
    End Sub

    Private Sub btnRefreshTwainScanners_Click(sender As Object, e As System.EventArgs) Handles btnRefreshTwainScanners.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            btnRefreshTwainScanners.Enabled = False
            Application.DoEvents()

            If gloRemoteScanGeneral.TwainScanFunctionality.CreateTwainScanSettingsFile() Then
                Dim sRetVal As String = Nothing
                gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings = Nothing
                'Current Settings
                sRetVal = oRemoteScanCommon.SetRemoteScannerCurrentSettings(Nothing, Nothing, Nothing)
                If Not String.IsNullOrEmpty(sRetVal) Then
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, sRetVal, gloAuditTrail.ActivityOutCome.Failure)
                End If

                If CallRemoteScanSettingsLoad() Then
                    MessageBox.Show("Scanners Refreshed", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Unable to refresh Scanners. One/multiple of the following might be the possible reason(s)." + Environment.NewLine + "1. Unable to retrieve scanner list." + Environment.NewLine + "2. Unable to write scanner list to configuration file.", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                If String.IsNullOrEmpty(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg) Then
                    MessageBox.Show("Unable to update scanner list, Please try after some time.", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg, gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg = Nothing
                    EmptyAllDropDown()
                End If

                ' MessageBox.Show("Unable to update scanner list, Please try after some time.", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally

            Me.Cursor = Cursors.[Default]
            btnRefreshTwainScanners.Enabled = True
            Application.DoEvents()
        End Try
    End Sub

    Private Sub EmptyAllDropDown()
        Try
            cmbRemoteScanner.DataSource = Nothing
            cmbRemoteScanMode.DataSource = Nothing
            cmbRemoteBitDepth.DataSource = Nothing
            cmbRemoteResolution.DataSource = Nothing
            cmbRemoteBrightness.DataSource = Nothing
            cmbRemoteContrast.DataSource = Nothing
            cmbRemoteScanSide.DataSource = Nothing
            cmbRemoteSupportedSize.DataSource = Nothing
            cmbRemoteImageFormat.Items.Clear()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        End Try
    End Sub


    Private Sub tc_Settings_Click(sender As System.Object, e As System.EventArgs) Handles tc_Settings.Click
        Try
            If (DirectCast(sender, System.Windows.Forms.TabControl).SelectedTab.Text = "DMS Settings") Then
                If Not bIsScannerConnected Then
                    If String.IsNullOrEmpty(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg) Then
                        MessageBox.Show("Local scanner settings not found.", gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Else
                        MessageBox.Show(gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg, gloGlobal.gloRemoteScanSettings.msgCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                        gloRemoteScanGeneral.RemoteScanSettings.sErrorMsg = Nothing
                        EmptyAllDropDown()
                    End If

                    '   MessageBox.Show("Local scanner settings not found.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        End Try
    End Sub

    Private Sub chkZipScannerSettings_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkZipScannerSettings.CheckedChanged

    End Sub

    Private Sub cmbRemoteScanSide_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbRemoteScanSide.SelectedIndexChanged
        Try
            If cmbRemoteScanSide.Text.ToLower() = "duplex" Then
                If cmbRemoteScanSideFeeder.Items.Count < 1 Then
                    FillFeederCombo()
                End If
                cmbRemoteScanSideFeeder.Text = "Feeder"
            End If
        Catch

        End Try
    End Sub
End Class
