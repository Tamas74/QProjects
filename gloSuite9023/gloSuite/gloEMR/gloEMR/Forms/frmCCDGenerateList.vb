Imports System.Net.Mail
Imports gloCCDLibrary
Imports System.IO
Imports System.Text.RegularExpressions
'Imports pdftron.PDF
'Imports Microsoft.Office.Interop

Public Class frmCCDGenerateList
    Dim ogloInterface As gloCCDInterface
    Dim strFilePath As String
    Dim CCDSection As String
    Dim blnMail As Boolean = False
    Dim blnChk As Boolean = False
    Dim blnAllChkd As Boolean = False
    Private _PatientID As Long
    Dim _VisitId As Int64 = 0
    Dim _GeneratedFrom As String

    Public _IsVisitCCD As Boolean = False
    Dim _PatientLastName As String = ""

    Dim _FromDate As String = Nothing
    Dim _ToDate As String = Nothing
    Dim MUSection As String = ""
    'Dim printDocument1 As New System.Drawing.Printing.PrintDocument

    ''
    'Problems No:00000251::20121004
    Dim ownerFlag As Boolean = False
    Dim dtDOS As DateTime
    ''

    ''Event Added on 20120420
    ''Developer :Mayuri
    ''To close panel after opening CDS Window
    Public Event CDS_Closed As FormClosedEventHandler
    Public Property GeneratedFrom()
        Get
            Return _GeneratedFrom
        End Get
        Set(ByVal value)
            _GeneratedFrom = value
        End Set
    End Property


    Public Property VisitId()
        Get
            Return _VisitId
        End Get
        Set(ByVal value)
            _VisitId = value
        End Set
    End Property

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        _PatientID = PatientID

    End Sub

    ''
    ''Problems No:00000251::20121004
    'New Constructer Created for getting DOS for Past Exam
    Public Sub New(ByVal PatientID As Long, ByVal oFlag As Boolean)
        MyBase.New()
        InitializeComponent()
        _PatientID = PatientID
        ownerFlag = oFlag
        dtDOS = dtpToDate.Value
    End Sub
    ''
    Private nExamID As Int64 = 0
    Public Property ExamID() As Int64
        Get
            Return nExamID
        End Get
        Set(ByVal value As Int64)
            nExamID = value
        End Set
    End Property


    Private Sub SelectAll()
        If MUSection = "1" Then
            ChkVitals.Checked = True
            ChkImmunization.Checked = True
            ChkFamilyHistory.Checked = True
            ChkSocialHistory.Checked = True
            ChkProcedures.Checked = True
            ChkEncounter.Checked = True
            ChkAdvanceDirectives.Checked = True
        Else
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
        End If
    End Sub
    Private Sub DeselectAll()
        If MUSection = "1" Then
            ChkVitals.Checked = False
            ChkImmunization.Checked = False
            ChkFamilyHistory.Checked = False
            ChkSocialHistory.Checked = False
            ChkProcedures.Checked = False
            ChkEncounter.Checked = False
            ChkAdvanceDirectives.Checked = False
        Else
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
        End If
    End Sub
    Private Function checkCCDSections() As String
        Dim ccdStr As String
        ccdStr = ""
        If ChkAll.Checked = True Then
            ccdStr = "All"
        Else
            If chkProblems.Checked = True Then
                ccdStr = String.Concat(ccdStr, "Problems")
            End If
            If ChkAllergy.Checked = True Then
                ccdStr = String.Concat(ccdStr, "Allergy")
            End If
            If ChkVitals.Checked = True Then
                ccdStr = String.Concat(ccdStr, "Vitals")
            End If
            If ChkMedications.Checked = True Then
                ccdStr = String.Concat(ccdStr, "Medications")
            End If
            If ChkResults.Checked = True Then
                ccdStr = String.Concat(ccdStr, "Results")
            End If
            If ChkImmunization.Checked = True Then
                ccdStr = String.Concat(ccdStr, "Immunization")
            End If
            If ChkFamilyHistory.Checked = True Then
                ccdStr = String.Concat(ccdStr, "FamilyHistory")
            End If
            If ChkSocialHistory.Checked = True Then
                ccdStr = String.Concat(ccdStr, "SocialHistory")
            End If
            If ChkProcedures.Checked = True Then
                ccdStr = String.Concat(ccdStr, "Procedures")
            End If
            If ChkEncounter.Checked = True Then
                ccdStr = String.Concat(ccdStr, "Encounter")
            End If
            If ChkAdvanceDirectives.Checked = True Then
                ccdStr = String.Concat(ccdStr, "AdvanceDirectives")
            End If
        End If
        Return ccdStr
    End Function
    Private Sub tblClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblClose.Click
        Me.Close()
    End Sub

    Private Sub ChkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAll.CheckedChanged
        blnAllChkd = True
        If ChkAll.Checked = True Then
            ChkAll.Text = "Clear All"
            Call SelectAll()
        Else
            ChkAll.Text = "Select All"
            If blnChk = False Then
                Call DeselectAll()
            End If
            blnChk = False
        End If
        blnAllChkd = False
    End Sub

    Private Sub frmCCDGenerateList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Me.Opacity = 0
        Dim ogloCCDDBLayer As gloCCDDatabaseLayer 'Change made to solve memory Leak and word crash issue
        Dim objMU As New gloGlobal.clsMU
        Dim objSettings As New clsSettings
        ogloCCDDBLayer = New gloCCDDatabaseLayer

        '06-Oct-14 Aniket: Implement the Race, Ethnicity check only if there are MU dashboards created
        If objMU.GetMUReportCount > 0 Then
            'If MessageBox.Show("From date can not be greater than to date. ",  gstrMessageBoxCaption, MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
            ' If (MessageBox.Show("Generating CCD is not counted towards MU 2014+ Dashboard." + vbCrLf + "Are you sure you want to generate the CCD?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) = Windows.Forms.DialogResult.No Then
            If (MessageBox.Show("Generating CCD is not counted towards MU 2014+ Dashboard." + vbCrLf + "Instead, 'CDA', a new type of clinical file, should be used for MU 2014+." + vbCrLf + "Are you sure you want to generate the CCD?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) = Windows.Forms.DialogResult.No Then
                objMU = Nothing
                Me.Close()
                Exit Sub
            End If
        End If

        objMU = Nothing

        If Panel5.Visible = False Then
            Me.Height = 348
            Me.Width = 450
        End If


        'Problems No:00000251::20121004
        If ownerFlag Then
            If dtpFrom.Enabled = False Then
                dtpFrom.Value = "1/1/2000"
            End If
            dtDOS = dtpToDate.Value
            dtDOS = dtDOS + System.DateTime.Now.TimeOfDay
        Else
            If dtpFrom.Enabled = False Then
                dtpFrom.Value = "1/1/2000"
            Else
                dtpFrom.Value = DateTime.Now.Date
            End If
            dtpToDate.Value = DateTime.Now.Date
        End If






        _PatientLastName = ogloCCDDBLayer.GetPatientLastName(_PatientID)
        If IsNothing(ogloCCDDBLayer) = False Then
            ogloCCDDBLayer.Dispose()
            ogloCCDDBLayer = Nothing
        End If

        If _IsVisitCCD = False Then
            Checksectionsfromsettings()
        End If

        Dim dtMUSetting As New DataTable

        dtMUSetting = objSettings.GetSetting("MUCCDSECTIONS")
        If IsNothing(dtMUSetting) = False Then
            If dtMUSetting.Rows.Count > 0 Then
                MUSection = dtMUSetting.Rows(0)("sSettingsValue")
                If MUSection = "1" Then
                    ChkResults.Enabled = False
                    ChkAllergy.Enabled = False
                    chkProblems.Enabled = False
                    ChkMedications.Enabled = False
                End If
            End If
        End If

        If IsNothing(objSettings) = False Then
            objSettings.Dispose()
            objSettings = Nothing
        End If

        If IsNothing(dtMUSetting) = False Then
            dtMUSetting.Dispose()
            dtMUSetting = Nothing
        End If

        Me.Opacity = 100


    End Sub

    Private Sub Checksectionsfromsettings()
        Dim dt As New DataTable

        Dim DefaultFullCCD() As String

        Dim objSettings As New clsSettings
        Dim i As Int16
        ChkVitals.Checked = False

        ChkFamilyHistory.Checked = False

        ChkAdvanceDirectives.Checked = False

        ChkResults.Checked = False
        ChkResults.Enabled = True
        ChkProcedures.Checked = False

        ChkImmunization.Checked = False

        ChkMedications.Checked = False
        ChkMedications.Enabled = True
        ChkEncounter.Checked = False

        ChkSocialHistory.Checked = False

        ChkAllergy.Checked = False
        ChkAllergy.Enabled = True
        chkProblems.Checked = False
        chkProblems.Enabled = True
        dt = objSettings.GetSetting("FULLCCDDEFAULTSECTIONS")

        If IsNothing(dt) = False Then
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("sSettingsValue") <> "" Then
                    DefaultFullCCD = System.Convert.ToString(dt.Rows(0)("sSettingsValue")).Trim.Split(",")
                    For i = 0 To DefaultFullCCD.Length - 1
                        If DefaultFullCCD.Length > 0 Then
                            Select Case DefaultFullCCD(i)
                                Case "Fullvitals"
                                    ChkVitals.Checked = True
                                Case "FullFamHis"
                                    ChkFamilyHistory.Checked = True
                                Case "FullAdDir"
                                    ChkAdvanceDirectives.Checked = True
                                Case "FullLabs"
                                    ChkResults.Checked = True
                                Case "FullImmu"
                                    ChkImmunization.Checked = True
                                Case "FullProc"
                                    ChkProcedures.Checked = True
                                Case "FullMed"
                                    ChkMedications.Checked = True
                                Case "Fullencounter"
                                    ChkEncounter.Checked = True
                                Case "FullSocHis"
                                    ChkSocialHistory.Checked = True
                                Case "FullAllergy"
                                    ChkAllergy.Checked = True
                                Case "FullProb"
                                    chkProblems.Checked = True
                            End Select
                        End If
                    Next
                Else

                End If
            End If
        End If

        If IsNothing(objSettings) = False Then
            objSettings.Dispose()
            objSettings = Nothing
        End If
        If IsNothing(dt) = False Then
            dt.Dispose()
            dt = Nothing
        End If
    End Sub
    Private Sub tlbbtn_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_Print.Click
        Try
            'Me.WebBrowser1 = New System.Windows.Forms.WebBrowser
            'Dim strPdfFilepath As String = ""
            'Dim _strfileName As String = ""

            'Dim objpdf As pdftron.PDF.PDFDoc

            If dtpFrom.Enabled = True Then
                If dtpFrom.Value() > dtpToDate.Value() Then
                    MessageBox.Show("From date can not be greater than to date. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                _FromDate = dtpFrom.Value.ToShortDateString()
                _ToDate = dtpToDate.Value.ToShortDateString()
            Else
                _FromDate = Nothing
                _ToDate = Nothing
            End If
            pnlPrintMessage.Visible = True
            Label24.Visible = True
            Label24.BringToFront()
            lblFormularyTransactionMessage.Visible = True
            lblFormularyTransactionMessage.BringToFront()
            pnlPrintMessage.BringToFront()
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            lblCCDMessage.Visible = False
            Panel5.Visible = False
            CCDSection = checkCCDSections()
            If gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath <> "" Then
                If Directory.Exists(gstrCCDFilePath) = True Then
                    ogloInterface = New gloCCDLibrary.gloCCDInterface()

                    ogloInterface.IsNewProblemList = False

                    ''
                    'Problems No:00000251::20121004
                    ''added for case  CAS-22862-B2C8D4
                    strFilePath = ogloInterface.GenerateClinicalInformationold(_PatientID, gnLoginID, CCDSection, _VisitId, _FromDate, _ToDate, "", ownerFlag, dtDOS)
                    ''

                    If strFilePath = "" Then
                        MessageBox.Show("CCD file Not generated.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    ''Added On 20100729 by sanjog for validate the File Path
                    Dim ofile As FileInfo = New FileInfo(strFilePath)

                    Dim myXslTransform As New Xml.Xsl.XslTransform()
                    Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"

                    myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
                    myXslTransform.Transform(strFilePath, _strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
                    Me.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))



                    'Me.WebBrowser1.Navigate(ofile.FullName)
                    ''Added On 20100729 by sanjog for validate the File Path

                    ''
                    'Problems No:00000251::20121004
                    ogloInterface.SaveExportedCCD(_PatientID, ofile.FullName, "CCD File Printed", _GeneratedFrom, chkPatientRequest.Checked, ownerFlag, dtDOS, , ExamID)
                    ''

                    Dim _strCCD As String = "Demographics"
                    If ChkAll.Checked Then
                        _strCCD &= ",Problems,Allergy,Vitals,Medications,Lab Results,Immunization,Family History,Social History,Procedures,Encounter,Advance Directives Printed."
                    Else
                        If chkProblems.Checked Then
                            _strCCD &= ",Problems"
                        End If
                        If ChkAllergy.Checked Then
                            _strCCD &= ",Allergy"
                        End If
                        If ChkVitals.Checked Then
                            _strCCD &= ",Vitals"
                        End If
                        If ChkMedications.Checked Then
                            _strCCD &= ",Medications"
                        End If
                        If ChkMedications.Checked Then
                            _strCCD &= ",Medications"
                        End If
                        If ChkResults.Checked Then
                            _strCCD &= ",Lab Results"
                        End If
                        If ChkImmunization.Checked Then
                            _strCCD &= ",Immunization"
                        End If
                        If ChkFamilyHistory.Checked Then
                            _strCCD &= ",Family History"
                        End If
                        If ChkSocialHistory.Checked Then
                            _strCCD &= ",Social History"
                        End If
                        If ChkProcedures.Checked Then
                            _strCCD &= ",Procedures"
                        End If
                        If ChkEncounter.Checked Then
                            _strCCD &= ",Encounter"
                        End If
                        If ChkAdvanceDirectives.Checked Then
                            _strCCD &= ",Advance Directives"
                        End If
                        _strCCD &= " Printed"
                    End If
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Print, _strCCD, _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    lblCCDMessage.Text = "CCD file" & " ‘" & strFilePath & "’ " & "saved successfully. "

                    Panel5.Visible = True
                    lblCCDMessage.Visible = True
                    If Panel5.Visible = True Then
                        Me.Height = 414
                        Me.Width = 487
                    End If
                Else
                    'MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    MessageBox.Show("The CCD/C-CDA file path set in gloEMR admin '" & gstrCCDFilePath & "' could not be located/accessed. Please contact your system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Else
                'MessageBox.Show("Set the CCD file generation path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                MessageBox.Show("The CCD/C-CDA file path is not set in gloEMR admin. Please contact your system administrator")
                Exit Sub
            End If

            If Me.WebBrowser1.ReadyState = WebBrowserReadyState.Loaded Or Me.WebBrowser1.ReadyState = WebBrowserReadyState.Loading Then
                While Me.WebBrowser1.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            End If

            'Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            'Dim objWdDoc As Word.Document = myLoadWord.LoadWordApplication(_strfileName)

            'Try
            '    If (IsNothing(objWdDoc) = False) Then
            '        Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            '        Try
            '            thisAlertLevel = objWdDoc.Application.DisplayAlerts
            '            objWdDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
            '        Catch ex As Exception

            '        End Try

            '        strPdfFilepath = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")
            '        Try
            '            objWdDoc.SaveAs(strPdfFilepath, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, False, "", False)
            '        Catch ex2 As Exception

            '        End Try
            '        Try
            '            objWdDoc.Application.DisplayAlerts = thisAlertLevel
            '        Catch ex As Exception

            '        End Try

            '    End If
            'Catch ex As Exception

            'End Try
            'myLoadWord.CloseWordApplication(objWdDoc)
            'myLoadWord = Nothing

            If Me.WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then
                Dim Copied As Boolean = False
                If gloGlobal.gloTSPrint.isCopyPrint Then
                    Dim objWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                    Dim wd As Microsoft.Office.Interop.Word.Document
                    Dim outputFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")
                    wd = objWord.WebToDocx(Me.WebBrowser1, outputFileName)
                    Copied = gloWord.LoadAndCloseWord.CopyPrintDoc(wd, 0)
                    objWord.CloseWordApplication(wd)
                    objWord = Nothing
                End If
                If Copied = False Then
                    If gblnUseDefaultPrinter = True Then
                        Me.WebBrowser1.Print()

                    Else
                        Dim PrintDialog1 As PrintDialog = New PrintDialog()
                        If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                            Me.WebBrowser1.Print()
                        End If
                        PrintDialog1.Dispose()
                        PrintDialog1 = Nothing
                    End If
                End If

            End If

            pnlPrintMessage.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
            pnlPrintMessage.Visible = False
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
        End Try
    End Sub

    'Private Sub Print(doc As PDFDoc)
    '    Dim ogloPrintProgressController As gloPrintDialog.gloPrintProgressController = Nothing
    '    Try
    '        Using oDialog As New gloPrintDialog.gloPrintDialog()
    '            oDialog.ConnectionString = GetConnectionString()
    '            oDialog.TopMost = True
    '            oDialog.ShowPrinterProfileDialog = True
    '            Dim intFilePageCount As Integer = 0


    '            oDialog.ModuleName = "PrintCCDA"
    '            oDialog.RegistryModuleName = "CCDA"

    '            If oDialog IsNot Nothing Then

    '                doc.Lock()
    '                Dim maxPage As Integer = doc.GetPageCount()


    '                oDialog.PrinterSettings = printDocument1.PrinterSettings
    '                oDialog.AllowSomePages = True
    '                oDialog.PrinterSettings.ToPage = maxPage
    '                oDialog.PrinterSettings.FromPage = 1
    '                oDialog.PrinterSettings.MaximumPage = maxPage
    '                oDialog.PrinterSettings.MinimumPage = 1

    '                PrintDialog1.AllowSomePages = True
    '                PrintDialog1.PrinterSettings.ToPage = maxPage
    '                PrintDialog1.PrinterSettings.FromPage = 1
    '                PrintDialog1.PrinterSettings.MaximumPage = maxPage
    '                PrintDialog1.PrinterSettings.MinimumPage = 1

    '                If (intFilePageCount <= 0) Then
    '                    intFilePageCount = doc.GetPageCount() + maxPage
    '                Else
    '                    intFilePageCount = doc.GetPageCount() + intFilePageCount
    '                End If

    '                If oDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then

    '                    If (oDialog.bUseDefaultPrinter = True) Then
    '                        oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = True
    '                        oDialog.CustomPrinterExtendedSettings.IsShowProgress = True
    '                    End If

    '                    printDocument1.PrinterSettings = oDialog.PrinterSettings
    '                    Dim footer As gloPrintDialog.gloPrintProgressController.FooterInfo = New gloPrintDialog.gloPrintProgressController.FooterInfo
    '                    Dim footerList As List(Of gloPrintDialog.gloPrintProgressController.FooterInfo) = New List(Of gloPrintDialog.gloPrintProgressController.FooterInfo)
    '                    Dim dtPatientTable As DataTable = GetPatientInformation(_PatientID, GetConnectionString())
    '                    Dim StrPatientName As String = ""
    '                    StrPatientName = dtPatientTable.Rows(0).Item("Patient Name") + ", DOB : " + dtPatientTable.Rows(0).Item("DOB")
    '                    footer.FromPage = 1
    '                    footer.ToPage = intFilePageCount - 1
    '                    footer.StartingPage = 1
    '                    footer.TotalPages = intFilePageCount - maxPage
    '                    footer.CenterText = ""
    '                    footer.RightText = "[{PAGE()}] of [{TOTAL()}]"
    '                    footer.LeftText = StrPatientName
    '                    footerList.Add(footer)

    '                    ogloPrintProgressController = New gloPrintDialog.gloPrintProgressController(doc, doc.GetFileName(), oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, Nothing, footerList, True)

    '                    'ogloPrintProgressController.ShowProgress(Me)

    '                    If oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint Then
    '                        If oDialog.CustomPrinterExtendedSettings.IsShowProgress Then
    '                            ogloPrintProgressController.Show()
    '                        Else
    '                            ogloPrintProgressController.Show()
    '                        End If
    '                    Else
    '                        ogloPrintProgressController.TopMost = True
    '                        ogloPrintProgressController.ShowInTaskbar = False

    '                        ogloPrintProgressController.ShowDialog(Me)
    '                        If ogloPrintProgressController IsNot Nothing Then
    '                            ogloPrintProgressController.Dispose()
    '                        End If
    '                        ogloPrintProgressController = Nothing
    '                    End If

    '                End If

    '                doc.Unlock()
    '            Else
    '                Dim _ErrorMessage As String = "Error in Showing Print Dialog"

    '                MessageBox.Show(_ErrorMessage, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])

    '            End If

    '        End Using
    '    Catch ex As Exception
    '        Dim _ErrorMessage As String = ex.ToString()
    '        If _ErrorMessage.Trim() <> "" Then
    '            Dim _MessageString As String = ("Date Time : " & DateTime.Now.ToString()) + Environment.NewLine & "ERROR : " & _ErrorMessage
    '            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString)
    '            _MessageString = ""
    '        End If
    '        MessageBox.Show(ex.Message, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
    '        ex = Nothing
    '    Finally


    '    End Try

    'End Sub
    'Public Function GetPatientInformation(ByVal nPatinetId As Int64, ByVal strConnection As String) As DataTable
    '    Dim oDbLayer As New gloDatabaseLayer.DBLayer(GetConnectionString())
    '    Try
    '        oDbLayer.Connect(False)
    '        Dim oPatientDataTable As DataTable = Nothing
    '        oDbLayer.Retrive_Query("SELECT dbo.GET_NAME(Patient.sFirstName, Patient.sMiddleName, Patient.sLastName) As 'Patient Name',Convert(Datetime,Patient.dtDOB,101)As DOB, datediff(yy,Patient.dtDOB,dbo.gloGetDate()) AS 'Age',Patient.sGender As Gender,  Clinic_MST.sClinicName 'Practice Name' FROM Clinic_MST INNER JOIN Patient ON Clinic_MST.nClinicID = Patient.nClinicID Where Patient.nPatientId=" + nPatinetId.ToString(), oPatientDataTable)
    '        Return oPatientDataTable
    '    Catch ex As Exception
    '        Return Nothing
    '    Finally
    '        If Not IsNothing(oDbLayer) Then
    '            oDbLayer.Disconnect()
    '            oDbLayer.Dispose()
    '        End If
    '    End Try
    'End Function

    Private Sub tblShowCCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblShowCCD.Click
        Try

            If dtpFrom.Enabled = True Then
                If dtpFrom.Value() > dtpToDate.Value() Then
                    MessageBox.Show("From date can not be greater than to date. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                _FromDate = dtpFrom.Value.ToShortDateString()
                _ToDate = dtpToDate.Value.ToShortDateString()
            Else
                _FromDate = Nothing
                _ToDate = Nothing
            End If
            lblCCDMessage.Visible = False
            Panel5.Visible = False
            CCDSection = checkCCDSections()
            If gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath <> "" Then
                If Directory.Exists(gstrCCDFilePath) = True Then
                    ogloInterface = New gloCCDLibrary.gloCCDInterface()

                    ''
                    'Problems No:00000251::20121004
                    ''old added for case  CAS-22862-B2C8D4
                    strFilePath = ogloInterface.GenerateClinicalInformationold(_PatientID, gnLoginID, CCDSection, _VisitId, _FromDate, _ToDate, "", ownerFlag, dtDOS)
                    ''

                    If strFilePath = "" Then
                        MessageBox.Show("CCD file Not generated.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    ''Added On 20100729 by sanjog for validate the File Path
                    Dim ofile As FileInfo = New FileInfo(strFilePath)


                    Me.WebBrowser1.Navigate(ofile.FullName)
                    ''Added On 20100729 by sanjog for validate the File Path

                    ''
                    'Problems No:00000251::20121004
                    ogloInterface.SaveExportedCCD(_PatientID, ofile.FullName, "CCD File Displayed", _GeneratedFrom, chkPatientRequest.Checked, ownerFlag, dtDOS, , ExamID)
                    ''


                    Dim _strCCD As String = "Demographics"
                    If ChkAll.Checked Then
                        _strCCD &= ",Problems,Allergy,Vitals,Medications,Lab Results,Immunization,Family History,Social History,Procedures,Encounter,Advance Directives Displayed."
                    Else
                        If chkProblems.Checked Then
                            _strCCD &= ",Problems"
                        End If
                        If ChkAllergy.Checked Then
                            _strCCD &= ",Allergy"
                        End If
                        If ChkVitals.Checked Then
                            _strCCD &= ",Vitals"
                        End If
                        If ChkMedications.Checked Then
                            _strCCD &= ",Medications"
                        End If
                        If ChkMedications.Checked Then
                            _strCCD &= ",Medications"
                        End If
                        If ChkResults.Checked Then
                            _strCCD &= ",Lab Results"
                        End If
                        If ChkImmunization.Checked Then
                            _strCCD &= ",Immunization"
                        End If
                        If ChkFamilyHistory.Checked Then
                            _strCCD &= ",Family History"
                        End If
                        If ChkSocialHistory.Checked Then
                            _strCCD &= ",Social History"
                        End If
                        If ChkProcedures.Checked Then
                            _strCCD &= ",Procedures"
                        End If
                        If ChkEncounter.Checked Then
                            _strCCD &= ",Encounter"
                        End If
                        If ChkAdvanceDirectives.Checked Then
                            _strCCD &= ",Advance Directives"
                        End If
                        _strCCD &= " Displayed"
                    End If
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.View, _strCCD, _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    lblCCDMessage.Text = "CCD file" & " ‘" & strFilePath & "’ " & "saved successfully. "
                    Panel5.Visible = True
                    lblCCDMessage.Visible = True
                    If Panel5.Visible = True Then
                        Me.Height = 414
                        Me.Width = 487
                    End If
                    ''
                Else
                    'MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    MessageBox.Show("The CCD/C-CDA file path set in gloEMR admin '" & gstrCCDFilePath & "' could not be located/accessed. Please contact your system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Else
                'MessageBox.Show("Set the CCD file generation path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                MessageBox.Show("The CCD/C-CDA file path is not set in gloEMR admin. Please contact your system administrator")
                Exit Sub
            End If


            Dim objfrm As New frmCCDForm
            ''Added On 20100729 by sanjog for validate the File Path
            Dim ofile1 As FileInfo = New FileInfo(strFilePath)

            Dim myXslTransform As New Xml.Xsl.XslTransform()
            Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"

            myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
            myXslTransform.Transform(strFilePath, _strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))


            objfrm.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
            ''Added On 20100729 by sanjog for validate the File Path
            Me.Focus()
            objfrm.ShowInTaskbar = False
            objfrm.ShowDialog(IIf(IsNothing(objfrm.Parent), Me, objfrm.Parent))
            'Change made to solve memory Leak and word crash issue
            objfrm.Close()
            objfrm.Dispose()
            objfrm = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
        End Try
    End Sub

    Private Sub tblSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblSave.Click
        Try
            If dtpFrom.Enabled = True Then
                If dtpFrom.Value() > dtpToDate.Value() Then
                    MessageBox.Show("From date can not be greater than to date. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                _FromDate = dtpFrom.Value.ToShortDateString()
                _ToDate = dtpToDate.Value.ToShortDateString()
            Else
                _FromDate = Nothing
                _ToDate = Nothing
            End If
            Panel5.Visible = False
            lblCCDMessage.Visible = False
            If Panel5.Visible = False Then
                Me.Height = 348
                Me.Width = 450
            End If
            tblSave.Enabled = False
            CCDSection = checkCCDSections()
            If gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath <> "" Then
                If Directory.Exists(gstrCCDFilePath) = True Then

                    ' ''''Added on 20110914-to reduce number of clicks:glo6050 item
                    Dim _objfrmencrypt As New frmCCDEncryption(gstrCCDFilePath, _PatientID, _GeneratedFrom, strFilePath, CCDSection, _PatientLastName)
                    _objfrmencrypt.ShowDialog(IIf(IsNothing(_objfrmencrypt.Parent), Me, _objfrmencrypt.Parent))

                    If _objfrmencrypt._issave = True Then
                        ogloInterface = New gloCCDLibrary.gloCCDInterface()

                        ''
                        'Problems No:00000251::20121004
                        ''old added for case  CAS-22862-B2C8D4
                        strFilePath = ogloInterface.GenerateClinicalInformationold(_PatientID, gnLoginID, CCDSection, _VisitId, _FromDate, _ToDate, _objfrmencrypt.FilePath, ownerFlag, dtDOS)
                        ''

                        If strFilePath = "" Then
                            MessageBox.Show("CCD file Not generated.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        'Dim ofile As FileInfo = New FileInfo(strFilePath)
                        'Me.WebBrowser1.Navigate(ofile.FullName)
                        ''End 20111121

                        ''
                        'Problems No:00000251::20121004
                        ogloInterface.SaveExportedCCD(_PatientID, _objfrmencrypt.FilePath, "CCD File Saved", _objfrmencrypt._GeneratedFrom, chkPatientRequest.Checked, ownerFlag, dtDOS, , ExamID)
                        ''

                        If _objfrmencrypt.IsSecureDocument(_objfrmencrypt.sEncryptKey) = True Then
                            strFilePath = CompressCCDFile(_objfrmencrypt.FilePath, _objfrmencrypt.sEncryptKey)
                        End If

                        Dim _strCCD As String = "Demographics"
                        If ChkAll.Checked Then
                            _strCCD &= ",Problems,Allergy,Vitals,Medications,Lab Results,Immunization,Family History,Social History,Procedures,Encounter,Advance Directives Saved."
                        Else
                            If chkProblems.Checked Then
                                _strCCD &= ",Problems"
                            End If
                            If ChkAllergy.Checked Then
                                _strCCD &= ",Allergy"
                            End If
                            If ChkVitals.Checked Then
                                _strCCD &= ",Vitals"
                            End If
                            If ChkMedications.Checked Then
                                _strCCD &= ",Medications"
                            End If
                            If ChkMedications.Checked Then
                                _strCCD &= ",Medications"
                            End If
                            If ChkResults.Checked Then
                                _strCCD &= ",Lab Results"
                            End If
                            If ChkImmunization.Checked Then
                                _strCCD &= ",Immunization"
                            End If
                            If ChkFamilyHistory.Checked Then
                                _strCCD &= ",Family History"
                            End If
                            If ChkSocialHistory.Checked Then
                                _strCCD &= ",Social History"
                            End If
                            If ChkProcedures.Checked Then
                                _strCCD &= ",Procedures"
                            End If
                            If ChkEncounter.Checked Then
                                _strCCD &= ",Encounter"
                            End If
                            If ChkAdvanceDirectives.Checked Then
                                _strCCD &= ",Advance Directives"
                            End If
                            _strCCD &= " Saved"
                        End If

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Save, _strCCD, _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        lblCCDMessage.Text = "CCD file" & " ‘" & _objfrmencrypt.FilePath & "’ " & "saved successfully. "
                        Panel5.Visible = True
                        lblCCDMessage.Visible = True
                        If Panel5.Visible = True Then
                            Me.Height = 414
                            Me.Width = 487
                        End If
                    End If
                    'Change made to solve memory Leak and word crash issue
                    _objfrmencrypt.Close()
                    _objfrmencrypt.Dispose()
                    _objfrmencrypt = Nothing
                Else
                    'MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    MessageBox.Show("The CCD/C-CDA file path set in gloEMR admin '" & gstrCCDFilePath & "' could not be located/accessed. Please contact your system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Else
                'MessageBox.Show("Set the CCD file generation path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                MessageBox.Show("The CCD/C-CDA file path is not set in gloEMR admin. Please contact your system administrator")
                Exit Sub
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
            tblSave.Enabled = True
        End Try
    End Sub

    Private Sub ChkAllergy_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAllergy.CheckedChanged
        CheckSelection()
    End Sub
    Private Sub CheckSelection()
        If blnAllChkd = False Then
            If ChkAllergy.Checked = True AndAlso ChkDemographics.Checked = True AndAlso ChkEncounter.Checked = True AndAlso ChkFamilyHistory.Checked = True AndAlso ChkImmunization.Checked = True AndAlso ChkMedications.Checked = True AndAlso chkProblems.Checked = True AndAlso ChkProcedures.Checked = True AndAlso ChkResults.Checked = True AndAlso ChkSocialHistory.Checked = True AndAlso ChkVitals.Checked = True AndAlso ChkAdvanceDirectives.Checked = True Then
                If ChkAll.Checked = False Then
                    ''blnChk = True
                    ChkAll.Checked = True
                End If
            Else
                If ChkAll.Checked = True Then
                    blnChk = True
                    ChkAll.Checked = False
                End If
            End If
        End If
    End Sub

    Private Sub ChkDemographics_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDemographics.CheckedChanged
        CheckSelection()
    End Sub

    Private Sub ChkEncounter_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkEncounter.CheckedChanged
        CheckSelection()
    End Sub

    Private Sub ChkFamilyHistory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkFamilyHistory.CheckedChanged
        CheckSelection()
    End Sub

    Private Sub ChkImmunization_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkImmunization.CheckedChanged
        CheckSelection()
    End Sub

    Private Sub ChkMedications_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkMedications.CheckedChanged
        CheckSelection()
    End Sub

    Private Sub chkProblems_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkProblems.CheckedChanged
        CheckSelection()
    End Sub

    Private Sub ChkProcedures_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkProcedures.CheckedChanged
        CheckSelection()
    End Sub

    Private Sub ChkResults_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkResults.CheckedChanged
        CheckSelection()
    End Sub

    Private Sub ChkSocialHistory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkSocialHistory.CheckedChanged
        CheckSelection()
    End Sub

    Private Sub ChkVitals_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkVitals.CheckedChanged
        CheckSelection()
    End Sub

    Private Sub ChkAdvanceDirectives_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAdvanceDirectives.CheckedChanged
        CheckSelection()
    End Sub
    Private Function CompressCCDFile(ByVal filePath As String, ByVal sEncryotKey As String) As String

        Dim _compressedFilePath As String = ""
        Dim _fileInfo As FileInfo

        Try

            If sEncryotKey.Trim() <> "" Then

                _compressedFilePath = gloSecurity.gloEncryption.PerformFileEncryption(filePath, sEncryotKey, True)

                _fileInfo = New FileInfo(_compressedFilePath)

                If _fileInfo.Exists = True Then

                    _fileInfo = New FileInfo(filePath)
                    _fileInfo.Delete()

                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Return _compressedFilePath

    End Function
    Private Sub chkDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDate.CheckedChanged
        If chkDate.Checked = True Then
            dtpFrom.Enabled = True
            dtpToDate.Enabled = True
        Else
            dtpFrom.Enabled = False
            dtpToDate.Enabled = False
        End If

    End Sub

    'Private Sub tblSaveCDS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblSaveCDS.Click
    Public Sub SendRequestToDiagnosisOne()
        Try
            _FromDate = Nothing
            _ToDate = Nothing

            ''Cleck All for CDS
            ChkAll.Checked = True

            CCDSection = checkCCDSections()
            If gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath <> "" Then
                If Directory.Exists(gstrCCDFilePath) = True Then
                    ogloInterface = New gloCCDLibrary.gloCCDInterface()
                    ''old added for case  CAS-22862-B2C8D4
                    strFilePath = ogloInterface.GenerateClinicalInformationold(_PatientID, gnLoginID, CCDSection, _VisitId, _FromDate, _ToDate)
                    If strFilePath = "" Then
                        MessageBox.Show("CCD file Not generated.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    ''Added On 20100729 by sanjog for validate the File Path
                    Dim ofile As FileInfo = New FileInfo(strFilePath)
                    Me.WebBrowser1.Navigate(ofile.FullName)
                    ''Added On 20100729 by sanjog for validate the File Path

                    ogloInterface.SaveExportedCCD(_PatientID, ofile.FullName, "CCD File Displayed", _GeneratedFrom, chkPatientRequest.Checked, , , , ExamID)

                    Dim _strCCD As String = "Demographics"
                    If ChkAll.Checked Then
                        _strCCD &= ",Problems,Allergy,Vitals,Medications,Lab Results,Immunization,Family History,Social History,Procedures,Encounter,Advance Directives Displayed."
                    Else
                        If chkProblems.Checked Then
                            _strCCD &= ",Problems"
                        End If
                        If ChkAllergy.Checked Then
                            _strCCD &= ",Allergy"
                        End If
                        If ChkVitals.Checked Then
                            _strCCD &= ",Vitals"
                        End If
                        If ChkMedications.Checked Then
                            _strCCD &= ",Medications"
                        End If
                        If ChkMedications.Checked Then
                            _strCCD &= ",Medications"
                        End If
                        If ChkResults.Checked Then
                            _strCCD &= ",Lab Results"
                        End If
                        If ChkImmunization.Checked Then
                            _strCCD &= ",Immunization"
                        End If
                        If ChkFamilyHistory.Checked Then
                            _strCCD &= ",Family History"
                        End If
                        If ChkSocialHistory.Checked Then
                            _strCCD &= ",Social History"
                        End If
                        If ChkProcedures.Checked Then
                            _strCCD &= ",Procedures"
                        End If
                        If ChkEncounter.Checked Then
                            _strCCD &= ",Encounter"
                        End If
                        If ChkAdvanceDirectives.Checked Then
                            _strCCD &= ",Advance Directives"
                        End If
                        _strCCD &= " Displayed"
                    End If
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.View, _strCCD, _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    lblCCDMessage.Text = "CCD file" & " ‘" & strFilePath & "’ " & "saved successfully. "
                    Panel5.Visible = True
                    lblCCDMessage.Visible = True
                    If Panel5.Visible = True Then
                        Me.Height = 414
                        Me.Width = 487
                    End If
                    ''
                Else
                    'MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    MessageBox.Show("The CCD/C-CDA file path set in gloEMR admin '" & gstrCCDFilePath & "' could not be located/accessed. Please contact your system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Else
                'MessageBox.Show("Set the CCD file generation path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                MessageBox.Show("The CCD/C-CDA file path is not set in gloEMR admin. Please contact your system administrator")
                Exit Sub
            End If

            Dim oDiagnosisOneDB As New clsDiagnosisone_DBLayer
            'place here code of  insert record for request
            Dim CCDDiagnosisOneFilesID As Int64
            'Dim objSettings As New clsSettings
            Dim Pes_Url As String = ""
            Dim Pes_UserName As String = ""
            Dim Pes_Password As String = ""
            Dim Caltalog As String = ""
            Dim dtCDSSettings As DataTable = Nothing
            Try
                Dim objSettings As New clsSettings
                dtCDSSettings = objSettings.GetCDSSettingsForPatientProvider(_PatientID)
                objSettings.Dispose()
                objSettings = Nothing
            Catch ex As Exception

            End Try
            If IsNothing(dtCDSSettings) = False Then
                If (dtCDSSettings.Rows.Count > 0) Then
                    Dim objEncryption As New clsencryption
                    Pes_Url = dtCDSSettings.Rows(0)("Pes_Url").ToString()
                    Pes_UserName = dtCDSSettings.Rows(0)("Pes_UserName").ToString()
                    Pes_Password = objEncryption.DecryptFromBase64String(dtCDSSettings.Rows(0)("Pes_Password").ToString(), constEncryptDecryptKey)
                    Caltalog = dtCDSSettings.Rows(0)("Pes_Catalogs").ToString()
                    ' chkEnabledCDS.Checked = CType(dtCDSSettings.Rows(0)("CDS_Enabled").ToString(), Boolean)
                    objEncryption = Nothing
                End If
            End If

            Dim oDiagnosisOneRequest As New clsDiagnosisone_BusuinessLayer(Pes_Url, Pes_UserName, Pes_Password)
            CCDDiagnosisOneFilesID = oDiagnosisOneDB.Save_DiagnosisOne_Responce(0, _PatientID, VisitId, "", strFilePath, "", "Admin", "")
            Dim ResponcefilePath As String = oDiagnosisOneRequest.PostCDDfile(strFilePath, gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath, Caltalog)
            If (oDiagnosisOneRequest.ErrMessage.Trim = "") Then
                oDiagnosisOneDB.Save_DiagnosisOne_Responce(CCDDiagnosisOneFilesID, _PatientID, VisitId, "", "", ResponcefilePath, "", "")
                'Update Record
                'Show Genrated file.
                Dim objfrm As New frmCCDForm
                ''Added On 20100729 by sanjog for validate the File Path
                Dim ofile1 As FileInfo = New FileInfo(ResponcefilePath)

                Dim myXslTransform As New Xml.Xsl.XslTransform()
                Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"

                myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
                myXslTransform.Transform(ResponcefilePath, _strfileName) ' System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
                objfrm.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))


                ' objfrm.WebBrowser1.Navigate(ofile1.FullName)
                ''Added On 20100729 by sanjog for validate the File Path
                'Me.Focus()
                objfrm.isCDS = True
                objfrm.Text = "Preview of Diagnosis One Response"
                objfrm.ShowInTaskbar = False
                objfrm.StartPosition = FormStartPosition.CenterScreen
                RaiseEvent CDS_Closed(Nothing, Nothing)
                objfrm.ShowDialog(Me.MdiParent)
                'Change made to solve memory Leak and word crash issue
                objfrm.Close()
                objfrm.Dispose()
                objfrm = Nothing

            Else
                oDiagnosisOneDB.Save_DiagnosisOne_Responce(CCDDiagnosisOneFilesID, _PatientID, VisitId, "", "", "", "", oDiagnosisOneRequest.ErrMessage.ToString())
                Dim ofrmMessageBox As New FrmErrorDetailsMessageBox("The following error occurred while receiving CDS file.", oDiagnosisOneRequest.ErrMessage.ToString())
                RaiseEvent CDS_Closed(Nothing, Nothing)
                ofrmMessageBox.ShowDialog(Me.MdiParent)
                'Change made to solve memory Leak and word crash issue
                ofrmMessageBox.Close()
                ofrmMessageBox.Dispose()
                ofrmMessageBox = Nothing
            End If
            'Change made to solve memory Leak and word crash issue
            oDiagnosisOneDB = Nothing
            oDiagnosisOneRequest = Nothing
        Catch ex As Exception
            RaiseEvent CDS_Closed(Nothing, Nothing)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
        End Try
    End Sub


    Private Sub tblCDA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblCDA.Click
        'Dim oCDADataExtraction As gloCCDLibrary.gloCDADataExtraction = Nothing
        'Dim msg As String = String.Empty
        'Try


        '    If dtpFrom.Enabled = True Then
        '        If dtpFrom.Value() > dtpToDate.Value() Then
        '            MessageBox.Show("From date can not be greater than to date. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            Exit Sub
        '        End If
        '        _FromDate = dtpFrom.Value.ToShortDateString()
        '        _ToDate = dtpToDate.Value.ToShortDateString()
        '    Else
        '        _FromDate = Nothing
        '        _ToDate = Nothing
        '    End If
        '    lblCCDMessage.Visible = False
        '    Panel5.Visible = False
        '    CCDSection = checkCCDSections()
        '    If gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath <> "" Then
        '        If Directory.Exists(gstrCCDFilePath) = True Then
        '            oCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()
        '            oCDADataExtraction.nExamId = nExamID
        '            strFilePath = oCDADataExtraction.GenerateClinicalInformation(_PatientID, gnLoginID, CCDSection, _VisitId, _FromDate, _ToDate, "")
        '            lblCCDMessage.Text = "CCD file" & " ‘" & strFilePath & "’ " & "saved successfully. "
        '            Panel5.Visible = True
        '            lblCCDMessage.Visible = True
        '            If Panel5.Visible = True Then
        '                Me.Height = 414
        '                Me.Width = 487
        '            End If
        '            msg = oCDADataExtraction.strmsg
        '            oCDADataExtraction.SaveExportedCCD(_PatientID, strFilePath, "CCD File Displayed", _GeneratedFrom, chkPatientRequest.Checked, , , "CDA", ExamID)
        '        End If
        '    End If
        'Catch ex As Exception
        '    If msg <> "" Then
        '        MessageBox.Show(msg, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Else
        '        MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End If
        'Finally
        '    If Not IsNothing(oCDADataExtraction) Then
        '        oCDADataExtraction.Dispose()
        '        oCDADataExtraction = Nothing
        '    End If
        '    If Not IsNothing(ogloInterface) Then
        '        ogloInterface.Dispose()
        '        ogloInterface = Nothing
        '    End If
        'End Try
    End Sub
End Class
