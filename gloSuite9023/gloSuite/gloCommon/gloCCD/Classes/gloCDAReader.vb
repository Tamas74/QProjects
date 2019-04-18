Imports gloPatient
Imports gloCCDSchema
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.IO.Compression
Imports gloGlobal
Imports System.Web.Script.Serialization
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Threading
Imports System.Net
Imports System.Text
Imports System.Linq
Imports System.Windows.Forms



Public Class gloCDAReader
    Implements IDisposable

#Region " IDisposable  "

    Private disposedValue As Boolean = False        ' To detect redundant calls
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#End Region

    Dim oTemplateIDMaster As TemplateIDMaster
    Private Const NumberOfRetries As Int16 = 3
    Private Const DelayOnRetry As Integer = 1000
    Dim fileBytes As Byte() = Nothing
    Dim _IsErrorinFile As Boolean = False
    Public gstrCCDAImportCategory As String = String.Empty
    Dim _NoKnownAllergies As Boolean = False
    Dim _NoKnownProblems As Boolean = False
    Dim _NoKnownMedications As Boolean = False

    Public Property ErrorInFile() As Boolean
        Get
            Return _IsErrorinFile
        End Get
        Set(value As Boolean)
            _IsErrorinFile = value
        End Set
    End Property
    Public Property NoKnownAllerggies As Boolean
        Get
            Return _NoKnownAllergies
        End Get
        Set(value As Boolean)
            _NoKnownAllergies = value
        End Set
    End Property
    Public Property NoKnownProblems As Boolean
        Get
            Return _NoKnownProblems
        End Get
        Set(value As Boolean)
            _NoKnownProblems = value
        End Set
    End Property
    Public Property NoKnownMedications As Boolean
        Get
            Return _NoKnownMedications
        End Get
        Set(value As Boolean)
            _NoKnownMedications = value
        End Set
    End Property




    Public Function ExtractCDA_DemographicsOnly(ByVal strCCDFilePath As String, Optional ByVal isPreview As Boolean = False) As ReconcileList
        Dim strhtmlfile As String = String.Empty
        If gloLibCCDGeneral.sCDAValidatorUrl <> "" Then
            fileBytes = Nothing
            Dim r As RootObject = SendFileToServer(strCCDFilePath)

            If Not IsNothing(r) Then
                If r.Validation.resultsMetaData.resultMetaData.Count > 0 Then
                    Dim ErrorList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "C-CDA MDHT Conformance Error").First()
                    Dim VocabErrorList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "ONC 2015 S&CC Vocabulary Validation Conformance Error").First()
                    Dim ReferenceErrorList = r.Validation.resultsMetaData.resultMetaData.Where(Function(p) p.type = "ONC 2015 S&CC Reference C-CDA Validation Error").First()
                    If IsNothing(ErrorList) = False Or IsNothing(VocabErrorList) = False Or IsNothing(ReferenceErrorList) = False Then
                        If DirectCast(ErrorList, gloCCDLibrary.ResultMetaData).count > 0 Or DirectCast(VocabErrorList, gloCCDLibrary.ResultMetaData).count > 0 Or DirectCast(ReferenceErrorList, gloCCDLibrary.ResultMetaData).count > 0 Then
                            _IsErrorinFile = True
                            Dim cdaErrorId As Int64 = 0
                            Dim fi As New FileInfo(strCCDFilePath)
                            If IsNothing(fileBytes) = False Then
                                Dim databaselayer As gloCCDDatabaseLayer = New gloCCDDatabaseLayer()
                                cdaErrorId = databaselayer.InsertCDAErrorResponse(fi.Name, fileBytes)
                                If Not IsNothing(fileBytes) Then
                                    fileBytes = Nothing
                                End If
                                If Not IsNothing(databaselayer) Then
                                    databaselayer.Dispose()
                                End If
                            End If
                            If isPreview = False Then
                                Dim frmReview As New frmReviewDialog()
                                Dim DigResult As DialogResult
                                DigResult = frmReview.ShowDialog()
                                If Not IsNothing(frmReview) Then
                                    frmReview.Dispose()
                                End If

                                If DigResult = Windows.Forms.DialogResult.No Then
                                    Dim frm As frmDisplayCDAErrors = New frmDisplayCDAErrors(cdaErrorId)
                                    'Dim fi As New FileInfo(strCCDFilePath)
                                    frm.SetRootOBJECT = r
                                    frm.lblDocTypeHeader.Text = "                                                                                                              CCDA Document : " & fi.Name
                                    frm.ShowDialog()
                                    Return Nothing
                                ElseIf DigResult = Windows.Forms.DialogResult.Yes Then

                                ElseIf DigResult = Windows.Forms.DialogResult.Cancel Then
                                    Return Nothing

                                End If
                            Else
                                _IsErrorinFile = False
                                Dim DigResult As DialogResult
                                Dim frmReview As New frmReviewDialog()
                                frmReview.Changecaption()
                                DigResult = frmReview.ShowDialog()
                                If Not IsNothing(frmReview) Then
                                    frmReview.Dispose()
                                End If

                                If DigResult = Windows.Forms.DialogResult.No Then
                                    Dim frm As frmDisplayCDAErrors = New frmDisplayCDAErrors(cdaErrorId)
                                    'Dim fi As New FileInfo(strCCDFilePath)
                                    frm.SetRootOBJECT = r
                                    frm.lblDocTypeHeader.Text = "                                                                                                              CCDA Document : " & fi.Name
                                    frm.ShowDialog()
                                    Return Nothing
                                ElseIf DigResult = Windows.Forms.DialogResult.Yes Then
                                ElseIf DigResult = Windows.Forms.DialogResult.Cancel Then
                                    Return Nothing
                                End If

                            End If
                        End If
                    End If
                End If
                strhtmlfile = r.html
            Else
                'Continue import or preview if the server is not available or file cannot be validated due to any reason 
                'Continue preview/Import change is done for case CAS-18585-F0M2B3
                Dim frmReview As New frmReviewDialog()
                Dim DigResult As DialogResult
                frmReview.SetReviewBtnVisibility()
                frmReview.lblMessage.Text = "The selected CCDA file could not be validated. If you continue importing it, it might be non-conformance to the following certification clause." & vbCrLf & "" & vbCrLf & "  (b)(5)(ii) Validate and display" & vbCrLf & "Demonstrate the following functionalities for the document received in accordance with paragraph (b)(5)(i) of this section:" & vbCrLf & "     (A) Validate C-CDA conformance - system performance. Detect valid and invalid transition of care/referral summaries including the ability to:" & vbCrLf & "          (1) Parse each of the document types formatted according to the following document templates: Continuity of Care Document, Referral Note, and (inpatient setting only) Discharge Summary." & vbCrLf & "          (2) Detect errors in corresponding ""document-templates,""section-templates,""and ""entry-templates,"" including invalid vocabulary standards and codes not specified in the standards adopted in § 170.205(a)(3) and § 170.205(a)(4)." & vbCrLf & "          (3) Identify valid document-templates and process the data elements required in the corresponding section-templates and entry-templates from the standards adopted in § 170.205(a)(3) and § 170.205(a)(4)." & vbCrLf & "          (4) Correctly interpret empty sections and null combinations; and" & vbCrLf & "          (5) Record errors encountered and allow a user through at least one of the following ways to:" & vbCrLf & "               (i) Be notified of the errors produced." & vbCrLf & "               (ii) Review the errors produced." & vbCrLf & ""

                If isPreview = False Then
                    DigResult = frmReview.ShowDialog()
                    If Not IsNothing(frmReview) Then
                        frmReview.Dispose()
                    End If
                Else
                    frmReview.Changecaption()
                    DigResult = frmReview.ShowDialog()
                    If Not IsNothing(frmReview) Then
                        frmReview.Dispose()
                    End If
                End If

                If DigResult = Windows.Forms.DialogResult.No Then
                    Return Nothing
                ElseIf DigResult = Windows.Forms.DialogResult.Yes Then
                ElseIf DigResult = Windows.Forms.DialogResult.Cancel Then
                    Return Nothing
                End If


                'Dim result As DialogResult = Nothing
                'result = MessageBox.Show("Unable to validate the file. Do you want to continue Preview/Import? ", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                'If Not IsNothing(result) Then
                '    If result = DialogResult.No Then
                '        Return Nothing
                '    Else
                '        ' continue the preview/ import
                '    End If
                'End If

                'MessageBox.Show("Unable to validate the file as the server is not available. Please try again later.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Return Nothing
            End If
        Else
            MessageBox.Show("C-CDA validator Url is not set in gloEMR admin.Please contact your system administrator.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        End If
        Dim oReconcileList As ReconcileList = New ReconcileList
        Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing
        Dim _assignPerson As POCD_MT000040UV02Person = Nothing
        'Dim oassignAuthor As POCD_MT000040UV02Author = Nothing
        oTemplateIDMaster = New TemplateIDMaster
        oReconcileList.cdaviewerhtmlFile = strhtmlfile
        Dim oProvider As gloCCDLibrary.PatientProvider = Nothing

        Try


            oProvider = New gloCCDLibrary.PatientProvider
            oCCDSchema = gloSerialization.GetClinicalDocument(strCCDFilePath)

            If Not IsNothing(oCCDSchema) Then

                oReconcileList.mPatient = New Patient()
                oReconcileList.mPatient.PatientDemographics = getPatientDemographics(oCCDSchema)


                If Not IsNothing(oCCDSchema.custodian) Then
                    If Not IsNothing(oCCDSchema.custodian.assignedCustodian) Then
                        If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization) Then
                            If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name) Then
                                If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text) Then
                                    If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text.Length > 1) Then
                                        oReconcileList.FileHeaderSource = Convert.ToString(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text(0))
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                If Not IsNothing(oCCDSchema.effectiveTime) Then
                    If Not IsNothing(oCCDSchema.effectiveTime.value) Then
                        oReconcileList.FileHeaderDateTime = Convert.ToDateTime(gloReconciliation.DateFromHL7(oCCDSchema.effectiveTime.value))

                    End If
                End If


                If Not IsNothing(oCCDSchema.author) Then
                    If Not IsNothing(oCCDSchema.author.Length > 0) Then

                        _assignPerson = oCCDSchema.author(0).assignedAuthor.Item
                        ''
                        If Not IsNothing(_assignPerson) Then
                            If Not IsNothing(_assignPerson.name(0).Items) Then
                                If Not IsNothing(_assignPerson.name(0).Items.Length > 0) Then
                                    Dim k As Integer = 0
                                    For k = 0 To _assignPerson.name(0).Items.Length - 1
                                        If (Convert.ToString(_assignPerson.name(0).Items(k).ToString().Contains("given") = True)) Then
                                            If oProvider.FirstName = "" Then
                                                If Not IsNothing(_assignPerson.name(0).Items(k).Text) Then
                                                    oProvider.FirstName = Convert.ToString(_assignPerson.name(0).Items(k).Text(0))
                                                End If

                                                'oReconcileList .mPatient .PatientProviders(0
                                                'Else
                                                ' PatDemographics.DemographicsDetail.PatientMiddleName = Convert.ToString(b.name(0).Items(k).Text(0))
                                            End If
                                        ElseIf (Convert.ToString(_assignPerson.name(0).Items(k).ToString().Contains("family") = True)) Then
                                            If Not IsNothing(_assignPerson.name(0).Items(k).Text) Then
                                                oProvider.LastName = Convert.ToString(_assignPerson.name(0).Items(k).Text(0))
                                            End If

                                        End If

                                    Next
                                    If oProvider.FirstName <> "" Or oProvider.LastName <> "" Then
                                        oReconcileList.mPatient.PatientProviders.Add(oProvider)
                                    End If

                                End If
                            End If
                        End If


                        If Not IsNothing(oCCDSchema.author(0).time) Then
                            If Not IsNothing(oCCDSchema.author(0).time.value) Then
                                If oCCDSchema.author(0).time.value <> "" Then
                                    oReconcileList.LastModifiedDateTime = Convert.ToDateTime(gloReconciliation.DateFromHL7(oCCDSchema.author(0).time.value))
                                End If

                            End If
                        End If
                    End If
                End If

                '' add condition for sText
                If isPreview = True Then
                    '' add condition for sText
                    If oCCDSchema.confidentialityCode.code = "R" Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Extract, "CCDA file previewed with confidentiality: Restricted.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    ElseIf oCCDSchema.confidentialityCode.code = "N" Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Extract, "CCDA file previewed with confidentiality: Normal.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    End If
                End If
            End If


        Catch ex As Exception
            oReconcileList = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            oCCDSchema = Nothing
            If Not IsNothing(oTemplateIDMaster) Then
                oTemplateIDMaster.Dispose()
            End If
            If Not IsNothing(oProvider) Then
                oProvider.Dispose()
                oProvider = Nothing
            End If
        End Try

        Return oReconcileList

    End Function


    Public Function getDocConfidentiality(CCDAfullpath As String) As String
        Dim sConf As String = "N"
        Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing
        Try
            oCCDSchema = gloSerialization.GetClinicalDocument(CCDAfullpath)

            If Not IsNothing(oCCDSchema) Then
                sConf = oCCDSchema.confidentialityCode.code
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
        oCCDSchema = Nothing
        Return sConf
    End Function

    Private Function SendFileToServer(fileFullPath As String) As RootObject
        Try
            Cursor.Current = Cursors.WaitCursor
            'validationobjective is the parent directory of the file to be uploaded and referencefilename = filename 
            Dim r As RootObject = Nothing
            Dim fi As New FileInfo(fileFullPath)
            Dim di As New DirectoryInfo(fileFullPath)
            Dim fileName As String = fi.Name
            Dim fileContents As Byte() = File.ReadAllBytes(fi.FullName)
            Dim validationobjective As String = Convert.ToString(di.Parent)
            Dim javas As New JavaScriptSerializer()
            Dim httpRequest As Task(Of HttpResponseMessage) = Nothing
            Dim httpResponse As HttpResponseMessage = Nothing
            Dim multipartcontent As MultipartFormDataContent = Nothing
            Dim byteArrayContent As ByteArrayContent = Nothing
            Dim httpClient As New HttpClient()
            Try

                'For index As Integer = 1 To NumberOfRetries
                Try
                    '' Dim webService As New Uri("" & gloLibCCDGeneral.sCDAValidatorUrl & "?validationObjective=" & validationobjective & "&referenceFileName=" & fileName & "")
                    ' '  gloLibCCDGeneral.sCDAValidatorUrl = "http://dev56:8080/referenceccdaservice/"
                    Dim webService As New Uri("" & gloLibCCDGeneral.sCDAValidatorUrl & "?ccdaSections=" & gstrCCDAImportCategory & " &validationObjective=" & validationobjective & "&referenceFileName=" & fileName & "")
                    Dim requestMessage As New HttpRequestMessage(HttpMethod.Post, webService)
                    requestMessage.Headers.ExpectContinue = False
                    multipartcontent = New MultipartFormDataContent("----MyGreatBoundary")
                    byteArrayContent = New ByteArrayContent(fileContents)
                    byteArrayContent.Headers.Add("Content-Type", "application/octet-stream")
                    multipartcontent.Add(byteArrayContent, "ccdaFile", fileName)
                    requestMessage.Content = multipartcontent
                    httpRequest = httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, CancellationToken.None)
                    httpResponse = httpRequest.Result
                    'Exit For
                Catch ex As Exception
                    'If index = 3 Then
                    '    If ex.InnerException.InnerException.Message.Contains("The remote name could not be resolved") Or ex.InnerException.InnerException.Message.Contains("Unable to connect to the remote server") Then
                    '        Dim result As DialogResult = Nothing
                    '         result = MessageBox.Show("Unable to validate the file as the server is not available. Do you want to continue Preview/Import? ", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    '        If Not IsNothing(result) Then
                    '            If result = DialogResult.Yes Then

                    '            End If
                    '        End If
                    '    End If
                    'End If

                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    multipartcontent = Nothing
                    byteArrayContent = Nothing
                    httpRequest = Nothing
                    Thread.Sleep(DelayOnRetry)
                End Try
                'Next

                Dim statusCode As HttpStatusCode = httpResponse.StatusCode
                Dim responseContent As HttpContent = httpResponse.Content
                Dim stringContents As New StringBuilder

                If responseContent IsNot Nothing Then

                    Dim stringContentsTask = responseContent.ReadAsStringAsync()

                    Dim bytes As Task(Of [Byte]()) = responseContent.ReadAsByteArrayAsync()
                    fileBytes = bytes.Result

                    stringContents.Append(stringContentsTask.Result)

                    javas.MaxJsonLength = Int32.MaxValue
                    r = javas.Deserialize(Of RootObject)(stringContents.ToString())

                End If
                If Not IsNothing(httpRequest) Then
                    httpRequest.Dispose()
                End If
                If Not IsNothing(httpResponse) Then
                    httpResponse.Dispose()
                End If
                If Not IsNothing(statusCode) Then
                    statusCode = Nothing
                End If
                If Not IsNothing(responseContent) Then
                    responseContent.Dispose()
                End If
                Return r

            Catch ex As Exception
                If ex.Message.Contains("Invalid") Then
                    'Dim InvalidObject As Object = New RootObject
                    If fileName.Equals("NT_BadXml_r11_v2.xml", StringComparison.OrdinalIgnoreCase) Then
                        'MessageBox.Show("The service has encountered an error parsing the document. Please verify the document does not contain in-line XSL styling and/or address the following error: Element type ""templateId"" must be followed by either attribute specifications, ""> or />", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Throw New Exception("The service has encountered an error parsing the document. Please verify the document does not contain in-line XSL styling and/or address the following error: Element type ""templateId"" must be followed by either attribute specifications, ""> or />.")
                    Else
                        'MessageBox.Show("The service has encountered an error parsing the document. Please verify the document does not contain in-line XSL styling.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Throw New Exception("The service has encountered an error parsing the document. Please verify the document does not contain in-line XSL styling.")
                    End If
                    'Return InvalidObject
                End If

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                Return Nothing
            Finally
                If Not IsNothing(multipartcontent) Then
                    multipartcontent.Dispose()
                End If
                If Not IsNothing(byteArrayContent) Then
                    byteArrayContent.Dispose()
                End If
                If Not IsNothing(httpClient) Then
                    httpClient.Dispose()
                End If
                If Not IsNothing(fileContents) Then
                    fileContents = Nothing
                End If
                If Not IsNothing(javas) Then
                    javas = Nothing
                End If
                Cursor.Current = Cursors.Default
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        End Try
    End Function

    Public Sub ShowResponse(ByVal result As String, ByVal filename As String, ByVal ErrorId As Int64)
        Dim javas As JavaScriptSerializer = Nothing
        Try
            javas = New JavaScriptSerializer()
            javas.MaxJsonLength = Int32.MaxValue
            Dim r As RootObject = javas.Deserialize(Of RootObject)(result)
            If IsNothing(r) = False Then
                Dim frm As frmDisplayCDAErrors = New frmDisplayCDAErrors(ErrorId)
                frm.SetRootOBJECT = r
                frm.lblDocTypeHeader.Text = "                                                                                                              CCDA Document : " & filename
                frm.ShowDialog()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewDocumentErrors, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(javas) Then
                javas = Nothing
            End If
        End Try

    End Sub



    Public Function ExtractCDA(ByVal strCCDFilePath As String) As ReconcileList
        Dim oReconcileList As ReconcileList = New ReconcileList


        Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing

        oTemplateIDMaster = New TemplateIDMaster
        Try
            oCCDSchema = gloSerialization.GetClinicalDocument(strCCDFilePath)

            If Not IsNothing(oCCDSchema) Then

                oReconcileList.mPatient = New Patient()
                oReconcileList.mPatient.PatientDemographics = getPatientDemographics(oCCDSchema)
                Dim strNonXMLPath As String = ""
                Dim mediatype As String = ""
                strNonXMLPath = IsExistsCCDANonXMLBody(strCCDFilePath, mediatype)
                If strNonXMLPath <> "" Then

                    Return oReconcileList
                End If
                oReconcileList.mPatient.PatientHistory = getPatientHistory(oCCDSchema)
                oReconcileList.mPatient.PatientProblems = getPatientProblems(oCCDSchema)
                oReconcileList.mPatient.PatientMedications = getPatientMedications(oCCDSchema)


                If Not IsNothing(oCCDSchema.custodian) Then
                    If Not IsNothing(oCCDSchema.custodian.assignedCustodian) Then
                        If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization) Then
                            If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name) Then
                                If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text) Then
                                    If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text.Length > 1) Then
                                        oReconcileList.FileHeaderSource = Convert.ToString(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text(0))
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If


                If Not IsNothing(oCCDSchema.effectiveTime) Then
                    If Not IsNothing(oCCDSchema.effectiveTime.value) Then
                        oReconcileList.FileHeaderDateTime = Convert.ToDateTime(gloReconciliation.DateFromHL7(oCCDSchema.effectiveTime.value))
                    End If
                End If

                If Not IsNothing(oCCDSchema.author) Then
                    If Not IsNothing(oCCDSchema.author.Length > 0) Then
                        If Not IsNothing(oCCDSchema.author(0).time) Then
                            If Not IsNothing(oCCDSchema.author(0).time.value) Then
                                oReconcileList.LastModifiedDateTime = Convert.ToDateTime(gloReconciliation.DateFromHL7(oCCDSchema.author(0).time.value))
                            End If
                        End If
                    End If
                End If


            End If


        Catch ex As Exception
            oReconcileList = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            oCCDSchema = Nothing
            If Not IsNothing(oTemplateIDMaster) Then
                oTemplateIDMaster.Dispose()
            End If
        End Try

        oReconcileList.NoKnownMedication = NoKnownMedications
        oReconcileList.NoKnownProblems = NoKnownProblems
        oReconcileList.NoKnownallergies = NoKnownAllerggies
        Return oReconcileList

    End Function

    Private Function getPatientDemographics(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument) As gloPatient.Patient
        Dim PatDemographics As gloPatient.Patient = New gloPatient.Patient
        Try
            If Not IsNothing(oCCDSchema.recordTarget) Then
                If oCCDSchema.recordTarget.Length > 0 Then
                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole) Then

                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.id) Then
                            ''PatDemographics.DemographicsDetail.PatientCode = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.id)

                            PatDemographics.DemographicsDetail.PatientExternalCode = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.id(0).extension)
                        End If

                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient) Then
                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name) Then
                                Dim nNameLength As Integer
                                If oCCDSchema.recordTarget(0).patientRole.patient.name.Length > 0 Then
                                    nNameLength = oCCDSchema.recordTarget(0).patientRole.patient.name.Length
                                    If nNameLength = 1 Then
                                        For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.name.Length - 1
                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items) Then
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items.Length > 0) Then
                                                    Dim k As Integer = 0
                                                    For k = 0 To oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items.Length - 1
                                                        If (Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).ToString().Contains("given") = True)) Then
                                                            ''Added to check patient Previous name exist
                                                            If IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).qualifier) Then
                                                                If PatDemographics.DemographicsDetail.PatientFirstName = "" Then
                                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                        If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                            PatDemographics.DemographicsDetail.PatientFirstName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                        End If

                                                                    End If

                                                                Else
                                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                        If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                            PatDemographics.DemographicsDetail.PatientMiddleName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                        End If

                                                                    End If

                                                                End If
                                                            Else
                                                                If PatDemographics.PatientDemographicOtherInfo.sPatientPrevFName = "" Then
                                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                        If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                            PatDemographics.PatientDemographicOtherInfo.sPatientPrevFName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                        End If

                                                                    End If

                                                                Else
                                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                        If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                            PatDemographics.PatientDemographicOtherInfo.sPatientPrevMName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                        End If

                                                                    End If

                                                                End If
                                                            End If
                                                        ElseIf (Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).ToString().Contains("family") = True)) Then
                                                            'If PatDemographics.PatientDemographicOtherInfo.sPatientPrevFName <> "" Then
                                                            '    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                            '        If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                            '            PatDemographics.PatientDemographicOtherInfo.sPatientPrevLName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                            '        End If
                                                            '    End If
                                                            'End If

                                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                    PatDemographics.DemographicsDetail.PatientLastName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                End If

                                                            End If
                                                        ElseIf (Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).ToString().Contains("suffix") = True)) Then
                                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                    PatDemographics.DemographicsDetail.PatientSuffix = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                End If

                                                            End If

                                                        End If
                                                    Next
                                                End If
                                            End If

                                        Next
                                    Else
                                        For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.name.Length - 1
                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items) Then
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items.Length > 0) Then
                                                    Dim k As Integer = 0
                                                    For k = 0 To oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items.Length - 1
                                                        If (Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).ToString().Contains("given") = True)) Then
                                                            ''Added to check patient Previous name exist
                                                            If IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).qualifier) And PatDemographics.PatientDemographicOtherInfo.sPatientPrevFName = "" Then
                                                                If PatDemographics.DemographicsDetail.PatientFirstName = "" Then
                                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                        If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                            PatDemographics.DemographicsDetail.PatientFirstName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                        End If

                                                                    End If

                                                                Else
                                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                        If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                            PatDemographics.DemographicsDetail.PatientMiddleName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                        End If

                                                                    End If

                                                                End If
                                                            Else
                                                                If PatDemographics.PatientDemographicOtherInfo.sPatientPrevFName = "" Then
                                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                        If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                            PatDemographics.PatientDemographicOtherInfo.sPatientPrevFName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                        End If

                                                                    End If

                                                                Else
                                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                        If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                            PatDemographics.PatientDemographicOtherInfo.sPatientPrevMName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                        End If

                                                                    End If

                                                                End If
                                                            End If
                                                        ElseIf (Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).ToString().Contains("family") = True)) Then
                                                            If PatDemographics.PatientDemographicOtherInfo.sPatientPrevFName = "" Then
                                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                    If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                        PatDemographics.DemographicsDetail.PatientLastName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                    End If

                                                                End If
                                                            Else
                                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                    If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                        PatDemographics.PatientDemographicOtherInfo.sPatientPrevLName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                    End If

                                                                End If
                                                            End If
                                                        ElseIf (Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).ToString().Contains("suffix") = True)) Then
                                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text) Then
                                                                If oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text.Length > 0 Then
                                                                    PatDemographics.DemographicsDetail.PatientSuffix = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(i).Items(k).Text(0))
                                                                End If

                                                            End If

                                                        End If
                                                    Next
                                                End If
                                            End If

                                        Next
                                    End If



                                    'If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items) Then
                                    '    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items.Length > 0) Then
                                    '        Dim k As Integer = 0
                                    '        For k = 0 To oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items.Length - 1
                                    '            If (Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).ToString().Contains("given") = True)) Then
                                    '                If PatDemographics.DemographicsDetail.PatientFirstName = "" Then
                                    '                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text) Then
                                    '                        If oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text.Length > 0 Then
                                    '                            PatDemographics.DemographicsDetail.PatientFirstName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text(0))
                                    '                        End If

                                    '                    End If

                                    '                Else
                                    '                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text) Then
                                    '                        If oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text.Length > 0 Then
                                    '                            PatDemographics.DemographicsDetail.PatientMiddleName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text(0))
                                    '                        End If

                                    '                    End If

                                    '                End If
                                    '            ElseIf (Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).ToString().Contains("family") = True)) Then
                                    '                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text) Then
                                    '                    If oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text.Length > 0 Then
                                    '                        PatDemographics.DemographicsDetail.PatientLastName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text(0))
                                    '                    End If

                                    '                End If

                                    '            End If
                                    '        Next
                                    '    End If
                                    'End If
                                End If
                            End If
                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.birthTime) Then
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.birthTime.value) Then
                                    If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.birthTime.value).Length >= 8 Then
                                        PatDemographics.DemographicsDetail.PatientDOB = gloDateMaster.gloDate.DateAsStringDate(Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.birthTime.value).Substring(0, 8))
                                    End If
                                End If
                            End If

                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode) Then
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code) Then


                                    If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "f" Or Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "female" Then
                                        PatDemographics.DemographicsDetail.PatientGender = "Female"
                                    ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "m" Or Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "male" Then
                                        PatDemographics.DemographicsDetail.PatientGender = "Male"
                                    ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "un" Or Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "unknown" Then
                                        PatDemographics.DemographicsDetail.PatientGender = "Other"


                                    End If
                                ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.nullFlavor) = "UNK" Then
                                    PatDemographics.DemographicsDetail.PatientGender = "Unknown"

                                End If
                            End If


                            Dim sRaceCode As String = String.Empty
                            Dim sRace As String = String.Empty
                            Dim sEthnicityCode As String = String.Empty
                            Dim sEthnicity As String = String.Empty
                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient) Then
                                'If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode) Then


                                '    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length > 0) Then
                                '        For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length - 1
                                '            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i)) Then
                                '                If PatDemographics.DemographicsDetail.PatientRace = "" Then
                                '                    PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).displayName)
                                '                Else
                                '                    PatDemographics.DemographicsDetail.PatientRace = PatDemographics.DemographicsDetail.PatientRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).displayName)
                                '                End If

                                '            End If
                                '        Next

                                '    End If
                                'End If

                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode) Then

                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length > 0) Then
                                        If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).nullFlavor) = "ASKU" Then
                                            sRaceCode = "ASKU"
                                            sRace = "Declined to specify"
                                        ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).nullFlavor) = "UNK" Then
                                            sRaceCode = "UNK"
                                            sRace = "Unknown"
                                        Else
                                            For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length - 1
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i)) Then
                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation) Then
                                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation.Length > 0) Then
                                                            For j As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation.Length - 1
                                                                If sRaceCode = "" Then
                                                                    sRaceCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation(j).code) + "'"
                                                                    ''sRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation(j).displayName)
                                                                Else
                                                                    sRaceCode = sRaceCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation(j).code) + "'"
                                                                    ''sRace = sRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation(j).displayName)
                                                                End If

                                                            Next
                                                        End If
                                                    Else
                                                        If sRaceCode = "" Then
                                                            sRaceCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).code) + "'"
                                                            ''sRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).displayName)
                                                        Else
                                                            sRaceCode = sRaceCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).code) + "'"
                                                            ''sRace = sRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).displayName)
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If


                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1) Then

                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1.Length > 0) Then
                                        If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(0).nullFlavor) = "ASKU" Then
                                            If sRaceCode = "ASKU" Or sRaceCode = "UNK" Then
                                                sRaceCode = "ASKU"
                                                sRace = "Declined to specify"
                                            End If
                                        ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(0).nullFlavor) = "UNK" Then
                                            If sRaceCode = "ASKU" Or sRaceCode = "UNK" Then
                                                sRaceCode = "UNK"
                                                sRace = "Unknown"
                                            End If
                                        Else
                                            For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.raceCode1.Length - 1
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i)) Then
                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation) Then
                                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation.Length > 0) Then
                                                            For j As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation.Length - 1
                                                                If sRaceCode = "" Then
                                                                    sRaceCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation(j).code) + "'"
                                                                    ''sRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation(j).displayName)
                                                                Else
                                                                    sRaceCode = sRaceCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation(j).code) + "'"
                                                                    ''sRace = sRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation(j).displayName)
                                                                End If

                                                            Next
                                                        End If
                                                    Else
                                                        If sRaceCode = "" Then
                                                            sRaceCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).code) + "'"
                                                            ''sRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).displayName)
                                                        Else
                                                            sRaceCode = sRaceCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).code) + "'"
                                                            ''sRace = sRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).displayName)
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If


                                If sRaceCode <> "ASKU" And sRaceCode <> "UNK" Then
                                    Dim databaselayer As gloCCDDatabaseLayer = New gloCCDDatabaseLayer()
                                    sRace = databaselayer.getRaceEthnicityDescription("race", sRaceCode)
                                    If Not IsNothing(databaselayer) Then
                                        databaselayer.Dispose()
                                    End If
                                End If

                                PatDemographics.DemographicsDetail.PatientRace = sRace
                                ''

                                'If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode) Then
                                '    If oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length > 0 Then
                                '        If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).nullFlavor) = "ASKU" Then
                                '            PatDemographics.DemographicsDetail.PatientRace = "Declined to specify"
                                '        Else
                                '            'If PatDemographics.DemographicsDetail.PatientRace = "" Then
                                '            '    PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).displayName)
                                '            'Else
                                '            '    PatDemographics.DemographicsDetail.PatientRace = PatDemographics.DemographicsDetail.PatientRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).displayName)
                                '            'End If
                                '            '  PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode.displayName)
                                '        End If
                                '    End If



                                '    ' PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode.displayName)

                                'End If

                                'If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode) Then
                                '    If oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode.Length > 0 Then
                                '        If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).nullFlavor) = "ASKU" Then
                                '            PatDemographics.DemographicsDetail.PatientEthnicities = "Declined to specify"
                                '        Else
                                '            PatDemographics.DemographicsDetail.PatientEthnicities = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).displayName)
                                '        End If

                                '    End If

                                'End If



                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode) Then

                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode.Length > 0) Then
                                        If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).nullFlavor) = "ASKU" Then
                                            sEthnicityCode = "ASKU"
                                            sEthnicity = "Declined to specify"
                                        ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).nullFlavor) = "UNK" Then
                                            sEthnicityCode = "UNK"
                                            sEthnicity = "Unknown"
                                        Else
                                            For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode.Length - 1
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i)) Then
                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation) Then
                                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation.Length > 0) Then
                                                            For j As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation.Length - 1
                                                                If sEthnicityCode = "" Then
                                                                    sEthnicityCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation(j).code) + "'"
                                                                    ''sEthnicity = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation(j).displayName)
                                                                Else
                                                                    sEthnicityCode = sEthnicityCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation(j).code) + "'"
                                                                    ''sEthnicity = sEthnicity + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation(j).displayName)
                                                                End If

                                                            Next
                                                        End If
                                                    Else
                                                        If sEthnicityCode = "" Then
                                                            sEthnicityCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).code) + "'"
                                                            ''sEthnicity = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).displayName)
                                                        Else
                                                            sEthnicityCode = sEthnicityCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).code) + "'"
                                                            ''sEthnicity = sEthnicity + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).displayName)
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If


                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1) Then
                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1.Length > 0) Then
                                        If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(0).nullFlavor) = "ASKU" Then
                                            If sEthnicityCode = "ASKU" Or sEthnicityCode = "UNK" Then
                                                sEthnicityCode = "ASKU"
                                                sEthnicity = "Declined to specify"
                                            End If
                                        ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(0).nullFlavor) = "UNK" Then
                                            If sEthnicityCode = "ASKU" Or sEthnicityCode = "UNK" Then
                                                sEthnicityCode = "UNK"
                                                sEthnicity = "Unknown"
                                            End If
                                        Else
                                            For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1.Length - 1
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i)) Then
                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation) Then
                                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation.Length > 0) Then
                                                            For j As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation.Length - 1
                                                                If sEthnicityCode = "" Then
                                                                    sEthnicityCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation(j).code) + "'"
                                                                    ''sEthnicity = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation(j).displayName)
                                                                Else
                                                                    sEthnicityCode = sEthnicityCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation(j).code) + "'"
                                                                    ''sEthnicity = sEthnicity + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation(j).displayName)
                                                                End If

                                                            Next
                                                        End If
                                                    Else
                                                        If sEthnicityCode = "" Then
                                                            sEthnicityCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).code) + "'"
                                                            ''sEthnicity = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).displayName)
                                                        Else
                                                            sEthnicityCode = sEthnicityCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).code) + "'"
                                                            ''sEthnicity = sEthnicity + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).displayName)
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If

                            End If

                            If sEthnicityCode <> "ASKU" And sEthnicityCode <> "UNK" Then
                                Dim databaselayer1 As gloCCDDatabaseLayer = New gloCCDDatabaseLayer()
                                sEthnicity = databaselayer1.getRaceEthnicityDescription("ethnicity", sEthnicityCode)
                                If Not IsNothing(databaselayer1) Then
                                    databaselayer1.Dispose()
                                End If
                            End If

                            PatDemographics.DemographicsDetail.PatientEthnicities = sEthnicity


                        End If

                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr) Then
                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr.Length > 0) Then
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr(0).Items) Then
                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr(0).Items.Length > 0) Then
                                        Dim i As Integer = 0

                                        For i = 0 To oCCDSchema.recordTarget(0).patientRole.addr(0).Items.Length - 1
                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text) Then

                                                If oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text.Length > 0 Then


                                                    If (oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).ToString().ToLower().Contains("city") = True) Then
                                                        PatDemographics.DemographicsDetail.PatientCity = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                    ElseIf (oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).ToString().ToLower().Contains("state") = True) Then
                                                        PatDemographics.DemographicsDetail.PatientState = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                    ElseIf (oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).ToString().ToLower().Contains("country") = True) Then
                                                        PatDemographics.DemographicsDetail.PatientCountry = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                    ElseIf (oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).ToString().ToLower().Contains("postal") = True) Then
                                                        PatDemographics.DemographicsDetail.PatientZip = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                    ElseIf (oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).ToString().ToLower().Contains("streetaddressline") = True) Then
                                                        If PatDemographics.DemographicsDetail.PatientAddress1 = "" Then
                                                            PatDemographics.DemographicsDetail.PatientAddress1 = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                        Else
                                                            PatDemographics.DemographicsDetail.PatientAddress2 = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                        End If

                                                    End If
                                                End If
                                            End If
                                        Next

                                    End If
                                End If
                            End If
                        End If

                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.telecom) Then
                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.telecom.Length > 0) Then
                                For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.telecom.Length - 1
                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.telecom(i).use) Then
                                        If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.telecom(i).use(0)) = "6" Then
                                            'HP {6}
                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.telecom(i).value) Then
                                                PatDemographics.DemographicsDetail.PatientPhone = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.telecom(i).value).Replace(")", "").Replace("(", "").Replace("-", "").Replace("tel:", "").Replace("+1", "").Replace(" ", "")
                                            End If
                                        ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.telecom(i).use(0)) = "8" Then
                                            'MC {8}
                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.telecom(i).value) Then
                                                PatDemographics.DemographicsDetail.PatientMobile = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.telecom(i).value).Replace(")", "").Replace("(", "").Replace("-", "").Replace("tel:", "").Replace("+1", "").Replace(" ", "")
                                            End If
                                        End If
                                    End If
                                Next


                                'If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.telecom(0).value) Then
                                '    ' PatDemographics.DemographicsDetail.PatientPhone = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.telecom(0).value)
                                '    PatDemographics.DemographicsDetail.PatientPhone = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.telecom(0).value).Replace(")", "").Replace("(", "").Replace("-", "").Replace("tel:+1", "").Replace(" ", "")
                                'End If
                            End If
                        End If

                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.maritalStatusCode) Then
                            'UnMarried/Married/Single/Widowed/Divorced
                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.maritalStatusCode.code) Then


                                Select Case Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.maritalStatusCode.code)
                                    Case "M"
                                        PatDemographics.DemographicsDetail.PatientMaritalStatus = "Married"
                                    Case "S"
                                        PatDemographics.DemographicsDetail.PatientMaritalStatus = "Single"
                                    Case "W"
                                        PatDemographics.DemographicsDetail.PatientMaritalStatus = "Widowed"
                                    Case "D"
                                        PatDemographics.DemographicsDetail.PatientMaritalStatus = "Divorced"
                                End Select
                            End If
                        End If

                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication) Then
                            If oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication.Length > 0 Then


                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication(0).languageCode) Then
                                    If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication(0).languageCode.nullFlavor) = "ASKU" Then
                                        PatDemographics.DemographicsDetail.PatientLanguage = "Declined to specify"
                                    Else
                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication(0).languageCode.code) Then
                                            Dim strLangCode As String() = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication(0).languageCode.code).Split("-")
                                            Dim databaselayer1 As gloCCDDatabaseLayer = New gloCCDDatabaseLayer()
                                            PatDemographics.DemographicsDetail.PatientLanguage = databaselayer1.GetPatientPrefferedLanguage(strLangCode(0))
                                            If Not IsNothing(databaselayer1) Then
                                                databaselayer1.Dispose()
                                            End If
                                        End If


                                        'Select Case Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication(0).languageCode.code)
                                        '    Case "eng", "en"
                                        '        PatDemographics.DemographicsDetail.PatientLanguage = "English"

                                        '    Case "spa"
                                        '        PatDemographics.DemographicsDetail.PatientLanguage = "Spanish"
                                        '    Case "zho"
                                        '        PatDemographics.DemographicsDetail.PatientLanguage = "Chinese"

                                        '    Case "fra"
                                        '        PatDemographics.DemographicsDetail.PatientLanguage = "French"
                                        '    Case "deu"
                                        '        PatDemographics.DemographicsDetail.PatientLanguage = "German"
                                        '    Case "el"
                                        '        PatDemographics.DemographicsDetail.PatientLanguage = "Greek"
                                        '    Case "guj"
                                        '        PatDemographics.DemographicsDetail.PatientLanguage = "Gujarati"
                                        '    Case "jpn"
                                        '        PatDemographics.DemographicsDetail.PatientLanguage = "Japanese"
                                        '    Case "mar"
                                        '        PatDemographics.DemographicsDetail.PatientLanguage = "Marathi"
                                        '    Case "ASKU"
                                        '        PatDemographics.DemographicsDetail.PatientLanguage = "Declined to specify"



                                        'End Select
                                    End If
                                End If
                            End If

                        End If




                        PatDemographics.DemographicsDetail.PatientProviderID = 0
                        'PatDemographics.DemographicsDetail.PatientLanguage = ""
                        PatDemographics.DemographicsDetail.PatientSSN = ""
                        ' PatDemographics.DemographicsDetail.PatientAddress2 = ""
                        PatDemographics.DemographicsDetail.PatientCounty = ""
                        'PatDemographics.DemographicsDetail.PatientMobile = ""
                        PatDemographics.DemographicsDetail.PatientOccupation = ""
                        PatDemographics.DemographicsDetail.PatientFax = ""
                        PatDemographics.DemographicsDetail.EmergencyContact = ""
                        PatDemographics.DemographicsDetail.EmergencyPhone = ""
                        PatDemographics.GuardianDetail.PatientMotherFirstName = ""
                        PatDemographics.GuardianDetail.PatientMotherMiddleName = ""
                        PatDemographics.GuardianDetail.PatientMotherLastName = ""
                        PatDemographics.OccupationDetail.PatientEmploymentStatus = ""
                        PatDemographics.OccupationDetail.PatientPlaceofEmployment = ""
                        PatDemographics.OccupationDetail.PatientWorkAddress1 = ""
                        PatDemographics.OccupationDetail.PatientWorkAddress2 = ""
                        PatDemographics.OccupationDetail.PatientWorkCity = ""
                        PatDemographics.OccupationDetail.PatientWorkState = ""
                        PatDemographics.OccupationDetail.PatientWorkZip = ""
                        PatDemographics.OccupationDetail.PatientWorkPhone = ""
                        PatDemographics.OccupationDetail.PatientWorkFax = ""
                        PatDemographics.PatientDemographicOtherInfo.RegistrationDate = Nothing
                        PatDemographics.PatientDemographicOtherInfo.Status = ""

                    End If


                End If

            End If



            Return PatDemographics
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            PatDemographics = Nothing
            Return Nothing
        End Try
    End Function

    Public Function IsExistsCCDANonXMLBody(ByVal strCCDFilePath As String, ByRef mediatype As String) As String
        Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing
        Dim _FilePath As String = ""

        Try


            oCCDSchema = gloSerialization.GetClinicalDocument(strCCDFilePath)

            If Not IsNothing(oCCDSchema) Then
                If Convert.ToString(oCCDSchema.component.Item).Contains("POCD_MT000040UV02NonXMLBody") Then
                    Dim str As String
                    '  Dim mediatype1 As String
                    If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02NonXMLBody).text) Then
                        If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02NonXMLBody).text.Text) Then
                            If CType(oCCDSchema.component.Item, POCD_MT000040UV02NonXMLBody).text.Text.Length > 0 Then
                                str = CType(oCCDSchema.component.Item, POCD_MT000040UV02NonXMLBody).text.Text(0).Trim()
                                If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02NonXMLBody).text.mediaType) Then
                                    mediatype = CType(oCCDSchema.component.Item, POCD_MT000040UV02NonXMLBody).text.mediaType.Trim()
                                    _FilePath = ConvertBase64ToPDF(str, mediatype)

                                    oCCDSchema = Nothing
                                End If

                            End If

                        End If

                    End If

                    Return _FilePath
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

        Return _FilePath

    End Function
    Public Function IsInValidCCDA(ByVal strCCDFilePath As String) As Boolean
        Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing
        Dim _FilePath As String = ""
        oCCDSchema = gloSerialization.GetClinicalDocument(strCCDFilePath)
        If Not IsNothing(oCCDSchema) Then
            If Convert.ToString(oCCDSchema.component.Item).Contains("POCD_MT000040UV02NonXMLBody") Then
                Return True
            End If
        End If
        oCCDSchema = Nothing
        Return False
    End Function
    Private Function ConvertBase64ToPDF(ByVal str As String, ByVal mediatype As String) As String


        'Dim binaryData() As Byte = Convert.FromBase64String(str)
        Dim binaryData() As Byte = Convert.FromBase64String(str)
        Dim strFile As String = ""
        Try



            If mediatype = "application/pdf" Then
                strFile = NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf")
            ElseIf mediatype = "image/jpeg" Then
                strFile = NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".jpg")
            ElseIf mediatype = "application/msword" Then
                strFile = NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
            ElseIf mediatype = "text/plain" Then
                strFile = NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".txt")
            ElseIf mediatype = "text/rtf" Then
                strFile = NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".rtf")
            ElseIf mediatype = "text/html" Then
                strFile = NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".htm")
            ElseIf mediatype = "image/gif" Then
                strFile = NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".jpg")
            ElseIf mediatype = "image/tiff" Then
                strFile = NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".tif")
            ElseIf mediatype = "image/png" Then
                strFile = NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".png")
            End If
            File.WriteAllBytes(strFile, binaryData)


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            binaryData = Nothing
        End Try
        Return strFile
    End Function
    Public Shared ReadOnly Property NewDocumentName(_Path As String, Optional ByVal strExtension As String = ".pdf") As String
        Get
            'Dim _NewDocumentName As String = ""

            'Dim _dtCurrentDateTimeAndGUID As String = System.DateTime.Now.ToString("MM dd yyyy hh mm ss tt") & " " & Guid.NewGuid().ToString()
            '_dtCurrentDateTimeAndGUID = Regex.Replace(_dtCurrentDateTimeAndGUID, "(.*?\\)", "")
            '_dtCurrentDateTimeAndGUID = Regex.Replace(_dtCurrentDateTimeAndGUID, "['()\n]", "")
            'Dim i As Integer = 0
            '_NewDocumentName = _dtCurrentDateTimeAndGUID & strExtension
            'While File.Exists(Path.Combine(_Path, _NewDocumentName)) And (i < Integer.MaxValue) = True
            '    i = i + 1
            '    _NewDocumentName = _dtCurrentDateTimeAndGUID & "-" & i & strExtension
            'End While
            'Return Path.Combine(_Path, _NewDocumentName)
            Return gloGlobal.clsFileExtensions.NewDocumentName(_Path, strExtension, "MMddyyyyHHmmssffff")

        End Get
    End Property
    Private Function getPatientHistory(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument) As gloPatientHistoryCol

        Dim oPatientHistoryList As gloPatientHistoryCol = New gloPatientHistoryCol
        Dim _sStatusTemplateID As String = ""
        Dim _sReactionTemplateID As String = ""
        Dim _sSeverityTemplateID As String = ""
        Try

            Dim oPatientHistory As gloPatientHistory

            If Not IsNothing(oCCDSchema.component.Item) Then

                If (CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length > 0) Then

                    Dim i As Integer = 0
                    Dim TemplateID As String = ""

                    ''TemplateID = oCDADataBaseLayer.getCDATemplateID("Allergies")
                    TemplateID = oTemplateIDMaster.GetSectionID("Allergies")
                    Dim SectionFound As Boolean = False
                    For i = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length - 1
                        If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId) Then
                            If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length > 0 Then
                                Dim TemlateCount As Integer = 0
                                SectionFound = False
                                For TemlateCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length - 1
                                    If TemplateID = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId(TemlateCount).root.ToString() Then
                                        SectionFound = True
                                        Exit For
                                    End If
                                Next
                                If SectionFound = False Then
                                    Continue For
                                Else
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    If SectionFound = True Then


                        If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry) Then
                            If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length > 0 Then

                                Dim oHistorySchema As POCD_MT000040UV02Entry = Nothing
                                Dim oHistoryObservation As POCD_MT000040UV02Observation = Nothing
                                Dim oHistoryPlayingEntity As POCD_MT000040UV02PlayingEntity = Nothing

                                Dim EntryCount As Integer = 0
                                For EntryCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length - 1


                                    oHistorySchema = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry(EntryCount)
                                    If Not IsNothing(oHistorySchema) Then
                                        If Not IsNothing(CType(oHistorySchema.Item, POCD_MT000040UV02Act).entryRelationship) Then

                                            If CType(oHistorySchema.Item, POCD_MT000040UV02Act).entryRelationship.Length > 0 Then

                                                If Not IsNothing(CType(oHistorySchema.Item, POCD_MT000040UV02Act).entryRelationship(0).Item) Then


                                                    oHistoryObservation = TryCast(CType(oHistorySchema.Item, POCD_MT000040UV02Act).entryRelationship(0).Item, POCD_MT000040UV02Observation)
                                                    If Not IsNothing(oHistoryObservation) Then
                                                        If Not IsNothing(oHistoryObservation.participant) Then
                                                            If oHistoryObservation.participant.Length > 0 Then

                                                                If Not IsNothing(oHistoryObservation.participant(0).participantRole) Then
                                                                    If Not IsNothing(oHistoryObservation.participant(0).participantRole.Item) Then

                                                                        oHistoryPlayingEntity = TryCast(oHistoryObservation.participant(0).participantRole.Item, POCD_MT000040UV02PlayingEntity)


                                                                    End If

                                                                End If
                                                            End If

                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                    If Not IsNothing(oHistoryObservation) Then
                                        If Not IsNothing(oHistoryObservation.value) Then
                                            If oHistoryObservation.value.Length > 0 Then
                                                If oHistoryObservation.negationInd = True Then
                                                    NoKnownAllerggies = True
                                                    Return oPatientHistoryList
                                                End If
                                            End If
                                        End If
                                    End If


                                    oPatientHistory = New gloPatientHistory()

                                    oPatientHistory.HistoryCategory = "Allergies"
                                    oPatientHistory.Comments = ""
                                    If Not IsNothing(oHistoryPlayingEntity) Then
                                        If Not IsNothing(oHistoryPlayingEntity.code) Then

                                            ''History Item/Drug Name
                                            oPatientHistory.HistoryItem = Convert.ToString(oHistoryPlayingEntity.code.displayName)
                                            oPatientHistory.DrugName = Convert.ToString(oHistoryPlayingEntity.code.displayName)


                                            ''RxNormCode
                                            If Not IsNothing(oHistoryPlayingEntity.code.codeSystem) Then
                                                If oHistoryPlayingEntity.code.codeSystem = CodeSystem.RxNorm Then
                                                    oPatientHistory.RxNormCode = Convert.ToString(oHistoryPlayingEntity.code.code)
                                                End If
                                            End If
                                            If (IsNothing(oPatientHistory.HistoryItem) Or IsNothing(oPatientHistory.DrugName)) And (Not IsNothing(oPatientHistory.RxNormCode) And oPatientHistory.RxNormCode <> "") Then
                                                Dim RxNormAllergyname As String = ""
                                                Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                                                    RxNormAllergyname = oGSHelper.GetRxNormDrugAllergyName(oPatientHistory.RxNormCode)
                                                End Using
                                                oPatientHistory.HistoryItem = RxNormAllergyname
                                            End If


                                        End If
                                    End If
                                    Dim strConcernStatus As String = ""
                                    Dim strProblemStatus As String = ""
                                    If Not IsNothing(DirectCast(oHistorySchema.Item, POCD_MT000040UV02Act).statusCode) Then
                                        strConcernStatus = Convert.ToString(DirectCast(oHistorySchema.Item, POCD_MT000040UV02Act).statusCode.code)
                                        oPatientHistory.ConcernStatus = strConcernStatus
                                    End If

                                    ''ConceptDI/ SNOMED CT Code
                                    'If Not IsNothing(oHistoryObservation) Then
                                    '    If Not IsNothing(oHistoryObservation.value) Then
                                    '        If Not IsNothing(CType(oHistoryObservation.value(0),CD)) Then
                                    '            If Not IsNothing(CType(oHistoryObservation.value(0),CD).codeSystem) Then
                                    '                If Convert.ToString(CType(oHistoryObservation.value(0),CD).codeSystem) = CodeSystem.SNOMED_CT Then
                                    '                    oPatientHistory.ConceptId = Convert.ToString(CType(oHistoryObservation.value(0),CD).code)
                                    '                End If
                                    '            End If
                                    '        End If
                                    '    End If
                                    'End If


                                    '                                   Table Column                                                            Reconcilation table column      gloPatientHistory Properties
                                    'ConcernStartDate	         DOEAllergy (date of history created)		                  DOEAllergy		                       DOEAllergy
                                    'ConcernEndDate	         Date (When the concern status mark as completed) 	   dtConcernEndDate		        ConcernEndDate
                                    'ObservationStartDate	    OccurDate	(Resolved date from history form)		        dtOnsetdate		                  dtOnsetdate
                                    'ObservationEndDate	    ResolvedDate	(OccurDate enter in history screen) 	   dtObservationEndDate		   ObservationEndDate
                                    'HistoryStatus	              sProcStatus			                                                sProcStatus		                       ConcernStatus


                                    ''DOEAllergy
                                    Try
                                        If Convert.ToString(CType(CType(oHistorySchema.Item, POCD_MT000040UV02Act).effectiveTime.Items(0), IVXB_TS).value) <> "" Then
                                            oPatientHistory.DOEAllergy = gloDateMaster.gloDate.DateAsStringDate(Convert.ToString(CType(CType(oHistorySchema.Item, POCD_MT000040UV02Act).effectiveTime.Items(0), IVXB_TS).value))
                                        Else
                                            oPatientHistory.DOEAllergy = DateTime.Now
                                        End If
                                        If strConcernStatus.ToLower() = "completed" Then
                                            If Convert.ToString(CType(CType(oHistorySchema.Item, POCD_MT000040UV02Act).effectiveTime.Items(1), IVXB_TS).value) <> "" Then
                                                oPatientHistory.ConcernEndDate = gloDateMaster.gloDate.DateAsStringDate(Convert.ToString(CType(CType(oHistorySchema.Item, POCD_MT000040UV02Act).effectiveTime.Items(1), IVXB_TS).value))
                                            Else
                                                oPatientHistory.ConcernEndDate = DateTime.Now
                                            End If
                                        Else
                                            oPatientHistory.ConcernEndDate = Nothing
                                        End If
                                    Catch ex As Exception
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                        ex = Nothing
                                        oPatientHistory.DOEAllergy = DateTime.Now
                                    End Try


                                    _sStatusTemplateID = oTemplateIDMaster.GetEntryID("Allergy Status Observation")
                                    _sReactionTemplateID = oTemplateIDMaster.GetEntryID("Reaction Observation")
                                    _sSeverityTemplateID = oTemplateIDMaster.GetEntryID("Severity Observation")

                                    If Not IsNothing(oHistoryObservation) Then
                                        If Not IsNothing(oHistoryObservation.value) Then
                                            If oHistoryObservation.value.Length > 0 Then
                                                If Not IsNothing(oHistoryObservation.value(0)) Then

                                                    Try
                                                        If Not IsNothing(oHistoryObservation.effectiveTime) Then
                                                            If Not IsNothing(oHistoryObservation.effectiveTime.Items) Then
                                                                If oHistoryObservation.effectiveTime.Items.Length > 0 Then
                                                                    If Convert.ToString(CType(oHistoryObservation.effectiveTime.Items(0), IVXB_TS).value) <> "" Then
                                                                        oPatientHistory.OnsetDate = gloDateMaster.gloDate.DateAsStringDate((Convert.ToString(CType(oHistoryObservation.effectiveTime.Items(0), IVXB_TS).value)))
                                                                    Else
                                                                        oPatientHistory.OnsetDate = DateTime.Now
                                                                    End If
                                                                End If
                                                            End If

                                                        End If
                                                    Catch ex As Exception
                                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                                        oPatientHistory.OnsetDate = DateTime.Now
                                                    End Try

                                                    Try
                                                        If Not IsNothing(oHistoryObservation.effectiveTime.Items) Then
                                                            If oHistoryObservation.effectiveTime.Items.Length > 1 Then
                                                                If Convert.ToString(CType(oHistoryObservation.effectiveTime.Items(1), IVXB_TS).value) <> "" Then
                                                                    oPatientHistory.ObservationEndDate = gloDateMaster.gloDate.DateAsStringDate((Convert.ToString(CType(oHistoryObservation.effectiveTime.Items(1), IVXB_TS).value)))
                                                                End If

                                                            End If
                                                        End If

                                                        If Not IsNothing(oPatientHistory.ObservationEndDate) Then
                                                            strProblemStatus = "RESOLVED"
                                                        Else
                                                            strProblemStatus = "ACTIVE"
                                                        End If
                                                    Catch ex As Exception
                                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                                    End Try
                                                End If
                                            End If
                                        End If
                                    End If


                                    If Not IsNothing(oHistoryObservation) Then
                                        If Not IsNothing(oHistoryObservation.entryRelationship) Then
                                            If oHistoryObservation.entryRelationship.Length > 0 Then
                                                If oHistoryObservation.entryRelationship.Length = 1 Then
                                                    Dim oObservation_ReactionStatus As POCD_MT000040UV02Observation = Nothing

                                                    Dim indexAllergyEntry As Int32
                                                    For indexAllergyEntry = 0 To oHistoryObservation.entryRelationship.Length - 1

                                                        If Not IsNothing(oHistoryObservation.entryRelationship(indexAllergyEntry).Item) Then

                                                            oObservation_ReactionStatus = TryCast(oHistoryObservation.entryRelationship(indexAllergyEntry).Item, POCD_MT000040UV02Observation)

                                                            If Not IsNothing(oObservation_ReactionStatus) Then
                                                                If Not IsNothing(oObservation_ReactionStatus.templateId) Then


                                                                    If oObservation_ReactionStatus.templateId.Length > 0 Then
                                                                        If Convert.ToString(oObservation_ReactionStatus.templateId(0).root) = _sStatusTemplateID Then
                                                                            ''Allergy Status Section
                                                                            oPatientHistory.Status = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CE).displayName)
                                                                        ElseIf Convert.ToString(oObservation_ReactionStatus.templateId(0).root) = _sReactionTemplateID Then
                                                                            ''Allergy Reaction Section
                                                                            oPatientHistory.Reaction = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CD).displayName)
                                                                            oPatientHistory.ReactionCode = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CD).code)
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                            If Not IsNothing(oObservation_ReactionStatus.entryRelationship) Then
                                                                If oObservation_ReactionStatus.entryRelationship.Length > 0 Then
                                                                    Dim oObservation_Severity As POCD_MT000040UV02Observation = Nothing
                                                                    Dim indexSeverityEntry As Int32
                                                                    For indexSeverityEntry = 0 To oObservation_ReactionStatus.entryRelationship.Length - 1
                                                                        If Not IsNothing(oObservation_ReactionStatus.entryRelationship(indexSeverityEntry).Item) Then
                                                                            oObservation_Severity = TryCast(oObservation_ReactionStatus.entryRelationship(indexSeverityEntry).Item, POCD_MT000040UV02Observation)
                                                                            If Not IsNothing(oObservation_Severity) Then
                                                                                If Not IsNothing(oObservation_Severity.templateId) Then
                                                                                    If oObservation_Severity.templateId.Length > 0 Then
                                                                                        If Convert.ToString(oObservation_Severity.templateId(0).root) = _sSeverityTemplateID Then
                                                                                            oPatientHistory.Severity = Convert.ToString(CType(oObservation_Severity.value(0), CD).displayName)
                                                                                            oPatientHistory.SeverityCode = Convert.ToString(CType(oObservation_Severity.value(0), CD).code)
                                                                                        End If
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        End If

                                                                    Next
                                                                End If
                                                            End If
                                                        End If

                                                        oObservation_ReactionStatus = Nothing
                                                    Next
                                                Else
                                                    Dim oObservation_ReactionStatus As POCD_MT000040UV02Observation = Nothing

                                                    Dim indexAllergyEntry As Int32
                                                    For indexAllergyEntry = 0 To oHistoryObservation.entryRelationship.Length - 1

                                                        If Not IsNothing(oHistoryObservation.entryRelationship(indexAllergyEntry).Item) Then

                                                            oObservation_ReactionStatus = TryCast(oHistoryObservation.entryRelationship(indexAllergyEntry).Item, POCD_MT000040UV02Observation)

                                                            If Not IsNothing(oObservation_ReactionStatus) Then
                                                                If Not IsNothing(oObservation_ReactionStatus.templateId) Then


                                                                    If oObservation_ReactionStatus.templateId.Length > 0 Then
                                                                        If Convert.ToString(oObservation_ReactionStatus.templateId(0).root) = _sStatusTemplateID Then
                                                                            ''Allergy Status Section
                                                                            oPatientHistory.Status = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CE).displayName)
                                                                        ElseIf Convert.ToString(oObservation_ReactionStatus.templateId(0).root) = _sReactionTemplateID Then
                                                                            ''Allergy Reaction Section
                                                                            oPatientHistory.Reaction = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CD).displayName)
                                                                            oPatientHistory.ReactionCode = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CD).code)
                                                                        ElseIf Convert.ToString(oObservation_ReactionStatus.templateId(0).root) = _sSeverityTemplateID Then
                                                                            oPatientHistory.Severity = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CD).displayName)
                                                                            oPatientHistory.SeverityCode = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CD).code)
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If

                                                        oObservation_ReactionStatus = Nothing
                                                    Next
                                                End If


                                            End If
                                        End If
                                    End If


                                    ''Add History Item to collection
                                    If (Convert.ToString(oPatientHistory.HistoryItem) <> "") Then
                                        oPatientHistoryList.Add(oPatientHistory)
                                    End If

                                    If Not IsNothing(oPatientHistory) Then
                                        oPatientHistory.Dispose()
                                        oPatientHistory = Nothing
                                    End If


                                    oHistorySchema = Nothing
                                    oHistoryObservation = Nothing
                                    oHistoryPlayingEntity = Nothing
                                Next

                            End If
                        End If
                    End If
                End If
            End If



        Catch ex As Exception
            oPatientHistoryList = Nothing
            Throw ex
        Finally
            _sStatusTemplateID = Nothing
            _sReactionTemplateID = Nothing
        End Try

        Return oPatientHistoryList

    End Function

    Private Function getPatientProblems(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument) As ProblemsCol

        Dim oProblemList As ProblemsCol = New ProblemsCol()
        Dim TemplateID As String = ""
        Try
            If Not IsNothing(oCCDSchema) Then
                If Not IsNothing(oCCDSchema.component) Then
                    If Not IsNothing(oCCDSchema.component.Item) Then
                        If (CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length > 0) Then

                            ''TemplateID = oCDADataBaseLayer.getCDATemplateID("ProblemList")
                            TemplateID = oTemplateIDMaster.GetSectionID("Problem")


                            Dim SectionFound As Boolean = False
                            Dim i As Integer = 0
                            For i = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length - 1
                                If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId) Then
                                    If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length > 0 Then

                                        Dim TemlateCount As Integer = 0
                                        SectionFound = False
                                        For TemlateCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length - 1
                                            If TemplateID = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId(TemlateCount).root.ToString() Then
                                                SectionFound = True
                                                Exit For
                                            End If
                                        Next
                                        If SectionFound = False Then
                                            Continue For
                                        Else
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                            If SectionFound = True Then


                                ''---Read Propblem Entry---------------------------------
                                If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry) Then
                                    If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length > 0 Then

                                        Dim oProblem As Problems = Nothing
                                        Dim oCDAProblemSchema As POCD_MT000040UV02Entry = Nothing
                                        Dim oCDAProblemEntry As POCD_MT000040UV02Act = Nothing
                                        Dim oCDAProblemObservation As POCD_MT000040UV02Observation = Nothing
                                        Dim oCDAProblemEntryRelationship As POCD_MT000040UV02EntryRelationship = Nothing

                                        Dim EntryCount As Integer = 0

                                        For EntryCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length - 1

                                            oCDAProblemSchema = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry(EntryCount)

                                            If Not IsNothing(oCDAProblemSchema) Then
                                                oCDAProblemEntry = TryCast(oCDAProblemSchema.Item, POCD_MT000040UV02Act)
                                                If Not IsNothing(oCDAProblemEntry) Then
                                                    If Not IsNothing(oCDAProblemEntry.entryRelationship) Then

                                                        If oCDAProblemEntry.entryRelationship.Length > 0 Then
                                                            If Not IsNothing(oCDAProblemEntry.entryRelationship(0).Item) Then
                                                                oCDAProblemObservation = TryCast(oCDAProblemEntry.entryRelationship(0).Item, POCD_MT000040UV02Observation)
                                                            End If

                                                        End If

                                                    End If
                                                End If
                                            End If
                                            If Not IsNothing(oCDAProblemObservation) Then
                                                If Not IsNothing(oCDAProblemObservation.value) Then
                                                    If oCDAProblemObservation.value.Length > 0 Then
                                                        If oCDAProblemObservation.negationInd = True Then
                                                            NoKnownProblems = True
                                                            Return oProblemList
                                                        End If
                                                    End If
                                                End If
                                            End If


                                            '''''
                                            Dim _sStatusTemplateID As String = ""


                                            ''_sStatusTemplateID = oCDADataBaseLayer.getCDATemplateID("Problems Status")
                                            _sStatusTemplateID = oTemplateIDMaster.GetEntryID("Problem Status")

                                            ''Status
                                            Dim strConcernStatus As String = ""
                                            Dim strProblemStatus As String = ""
                                            If Not IsNothing(oCDAProblemEntry.statusCode) Then
                                                strConcernStatus = Convert.ToString(oCDAProblemEntry.statusCode.code)
                                            End If

                                            If Not IsNothing(oCDAProblemObservation) Then
                                                If Not IsNothing(oCDAProblemObservation.entryRelationship) Then
                                                    If oCDAProblemObservation.entryRelationship.Length > 0 Then

                                                        Dim oProblemStatus As POCD_MT000040UV02Observation = Nothing

                                                        Dim indexAllergyEntry As Int32
                                                        For indexAllergyEntry = 0 To oCDAProblemObservation.entryRelationship.Length - 1

                                                            If Not IsNothing(oCDAProblemObservation.entryRelationship(indexAllergyEntry).Item) Then

                                                                oProblemStatus = TryCast(oCDAProblemObservation.entryRelationship(indexAllergyEntry).Item, POCD_MT000040UV02Observation)

                                                                If Not IsNothing(oProblemStatus) Then
                                                                    If Not IsNothing(oProblemStatus.templateId) Then
                                                                        If oProblemStatus.templateId.Length > 0 Then
                                                                            If Convert.ToString(oProblemStatus.templateId(0).root) = _sStatusTemplateID Then
                                                                                ''Problems Status Section
                                                                                strProblemStatus = Convert.ToString(CType(oProblemStatus.value(0), CD).displayName)

                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If

                                                            oProblemStatus = Nothing
                                                        Next

                                                    End If
                                                End If
                                            End If
                                            oProblem = New Problems()

                                            '''''
                                            '                                   Table Column                                                                                                        Reconcilation table column      Problems Properties
                                            'ConcernStartDate	       VisitDate (when Problem is created Visit date from visitID	)	                                      dtConcernStartDate		       ConcernStartDate
                                            'ConcernEndDate	            Date	(When the concern status mark as completed) 		                                           dtConcernEndDate		       ConcernEndDate
                                            'ObservationStartDate	  OnsetDate (OnsetDate from Problem Table	)	                                                          dtDOS		                           DateOfService
                                            'ObservationEndDate	       ResolvedDate (When the status mark as resolved enter date is resolved date)		        dtResolvedDate		            ResolvedDate
                                            'ProblemStatus	            nProblemStatus			                                                                                             nProblemStatus		            ProblemStatus
                                            'ConcernStatus	            nCuncernStatus			                                                                                             nCurncernStatus		            ConcernStatus

                                            Try
                                                If Convert.ToString(DirectCast(oCDAProblemEntry.effectiveTime.Items(0), IVXB_TS).value) <> "" Then
                                                    oProblem.ConcernStartDate = gloDateMaster.gloDate.DateAsStringDate(Convert.ToString(DirectCast(oCDAProblemEntry.effectiveTime.Items(0), IVXB_TS).value))
                                                Else
                                                    oProblem.ConcernStartDate = DateTime.Now
                                                End If
                                                If strConcernStatus.ToLower() = "completed" Then
                                                    If Convert.ToString(DirectCast(oCDAProblemEntry.effectiveTime.Items(1), IVXB_TS).value) <> "" Then
                                                        oProblem.ConcernEndDate = gloDateMaster.gloDate.DateAsStringDate(Convert.ToString(DirectCast(oCDAProblemEntry.effectiveTime.Items(1), IVXB_TS).value))
                                                    Else
                                                        oProblem.ConcernEndDate = DateTime.Now
                                                    End If
                                                Else
                                                    oProblem.ConcernEndDate = Nothing
                                                End If

                                            Catch ex As Exception
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                                ex = Nothing
                                                oProblem.ConcernStartDate = DateTime.Now
                                                oProblem.ConcernEndDate = Nothing
                                            End Try


                                            If Not IsNothing(oCDAProblemObservation) Then
                                                If Not IsNothing(oCDAProblemObservation.value) Then
                                                    If oCDAProblemObservation.value.Length > 0 Then
                                                        If Not IsNothing(oCDAProblemObservation.value(0)) Then

                                                            ''oProblem.Condition
                                                            oProblem.Condition = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).displayName)

                                                            ''oProblem.DateOfService
                                                            Try
                                                                If Not IsNothing(oCDAProblemObservation.effectiveTime) Then
                                                                    If oCDAProblemObservation.effectiveTime.Items.Length > 0 Then
                                                                        If Convert.ToString(CType(oCDAProblemObservation.effectiveTime.Items(0), IVXB_TS).value) <> "" Then
                                                                            oProblem.DateOfService = gloDateMaster.gloDate.DateAsStringDate((Convert.ToString(CType(oCDAProblemObservation.effectiveTime.Items(0), IVXB_TS).value)))
                                                                        Else
                                                                            oProblem.DateOfService = DateTime.Now
                                                                        End If

                                                                    End If
                                                                End If

                                                            Catch ex As Exception
                                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                                                oProblem.DateOfService = DateTime.Now
                                                            End Try

                                                            ''oProblem.ResolvedDate
                                                            Try
                                                                If oCDAProblemObservation.effectiveTime.Items.Length > 1 Then
                                                                    If Convert.ToString(CType(oCDAProblemObservation.effectiveTime.Items(1), IVXB_TS).value) <> "" Then
                                                                        oProblem.ResolvedDate = gloDateMaster.gloDate.DateAsStringDate((Convert.ToString(CType(oCDAProblemObservation.effectiveTime.Items(1), IVXB_TS).value)))
                                                                    End If

                                                                End If
                                                                If Not IsNothing(oProblem.ResolvedDate) Then
                                                                    strProblemStatus = "RESOLVED"
                                                                Else
                                                                    strProblemStatus = "ACTIVE"
                                                                End If
                                                            Catch ex As Exception
                                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                                            End Try


                                                            ''Value element SNOMED or ICD9
                                                            If Not IsNothing(CType(oCDAProblemObservation.value(0), CD).codeSystem) Then
                                                                If gloLibCCDGeneral.gblnCCDAICD10Transition = True Then
                                                                    If CType(oCDAProblemObservation.value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then
                                                                        oProblem.ConceptID = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).code)
                                                                    ElseIf CType(oCDAProblemObservation.value(0), CD).codeSystem = CodeSystem.ICD10 Then
                                                                        oProblem.ICD9Code = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).code)
                                                                        oProblem.ICD9 = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).displayName)
                                                                        oProblem.ICDRevision = 10
                                                                    End If
                                                                Else
                                                                    If CType(oCDAProblemObservation.value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then
                                                                        oProblem.ConceptID = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).code)
                                                                    ElseIf CType(oCDAProblemObservation.value(0), CD).codeSystem = CodeSystem.ICD9 Then
                                                                        oProblem.ICD9Code = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).code)
                                                                        oProblem.ICD9 = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).displayName)
                                                                        oProblem.ICDRevision = 9
                                                                    End If
                                                                End If


                                                            End If

                                                            ' ''Transaction element SNOMED or ICD9
                                                            If Not IsNothing(CType(oCDAProblemObservation.value(0), CD).translation) Then
                                                                If CType(oCDAProblemObservation.value(0), CD).translation.Length > 0 Then
                                                                    For index As Integer = 0 To CType(oCDAProblemObservation.value(0), CD).translation.Length - 1
                                                                        If oProblem.Condition = "" Then
                                                                            oProblem.Condition = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).displayName)
                                                                        End If
                                                                        If gloLibCCDGeneral.gblnCCDAICD10Transition = True Then

                                                                            If (Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                                                                oProblem.ConceptID = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).code)
                                                                            ElseIf (Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).codeSystem) = CodeSystem.ICD10) Then
                                                                                oProblem.ICD9Code = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).code)
                                                                                oProblem.ICD9 = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).displayName)
                                                                                oProblem.ICDRevision = 10
                                                                            End If
                                                                        Else
                                                                            If (Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).codeSystem) = CodeSystem.ICD9) Then
                                                                                oProblem.ICD9Code = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).code)
                                                                                oProblem.ICD9 = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).displayName)
                                                                                oProblem.ICDRevision = 9
                                                                            ElseIf (Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                                                                oProblem.ConceptID = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(index).code)

                                                                            End If
                                                                        End If

                                                                    Next
                                                                End If
                                                            End If
                                                            'If Not IsNothing(CType(oCDAProblemObservation.value(0), CD).translation) Then
                                                            '    If oProblem.Condition = "" Then
                                                            '        oProblem.Condition = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).displayName)
                                                            '    End If
                                                            '    If (Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).codeSystem) = CodeSystem.ICD9) Then
                                                            '        oProblem.ICD9Code = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).code)
                                                            '        oProblem.ICD9 = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).displayName)
                                                            '    ElseIf (Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).codeSystem) = CodeSystem.SNOMED_CT) Then
                                                            '        oProblem.ConceptID = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).code)
                                                            '    End If

                                                            'End If

                                                            oProblem.Immediacy = 3




                                                        End If
                                                    End If

                                                End If
                                            End If

                                            Select Case strProblemStatus.ToUpper()
                                                Case "RESOLVED"
                                                    oProblem.ProblemStatus = Problems.Status.Resolved
                                                Case "ACTIVE"
                                                    oProblem.ProblemStatus = Problems.Status.Active
                                                Case "INACTIVE"
                                                    oProblem.ProblemStatus = Problems.Status.Inactive
                                                Case "CHRONIC"
                                                    oProblem.Immediacy = 2 'hardcode
                                                    oProblem.ProblemStatus = Problems.Status.Active
                                                Case "ALL"
                                                    oProblem.ProblemStatus = Problems.Status.All

                                                Case Else
                                                    oProblem.ProblemStatus = Problems.Status.Active
                                            End Select
                                            oProblem.sConcernStatus = strConcernStatus
                                            If (Convert.ToString(oProblem.Condition) <> "") Then
                                                oProblemList.Add(oProblem)
                                            End If
                                            If Not IsNothing(oProblem) Then
                                                oProblem.Dispose()
                                                oProblem = Nothing
                                            End If

                                            oCDAProblemSchema = Nothing
                                            oCDAProblemEntry = Nothing
                                            oCDAProblemObservation = Nothing

                                        Next ''For each Problem 
                                    End If
                                End If
                                ''---Read Propblem Entry-----------------------------------------
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            oProblemList = Nothing
            Throw ex
        Finally
            TemplateID = Nothing
        End Try

        Return oProblemList

    End Function

    Private Function getPatientMedications(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument) As MedicationsCol

        Dim oMedicationList As MedicationsCol = New MedicationsCol()
        Dim TemplateID As String = ""
        Try

            If Not IsNothing(oCCDSchema) Then
                Dim oMedication As Medication = Nothing


                If (Not IsNothing(oCCDSchema.component)) AndAlso (Not IsNothing(oCCDSchema.component.Item)) Then

                    Dim thisStructuredBody As POCD_MT000040UV02StructuredBody = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody)

                    If (Not IsNothing(thisStructuredBody)) AndAlso (Not IsNothing(thisStructuredBody.component)) Then


                        If (thisStructuredBody.component.Length > 0) Then

                            Dim i As Integer = 0

                            ''TemplateID = oCDADataBaseLayer.getCDATemplateID("Medication")
                            TemplateID = oTemplateIDMaster.GetSectionID("Medications")

                            Dim SectionFound As Boolean = False
                            For i = 0 To thisStructuredBody.component.Length - 1
                                If (Not IsNothing(thisStructuredBody.component(i))) AndAlso (Not IsNothing(thisStructuredBody.component(i).section)) AndAlso (Not IsNothing(thisStructuredBody.component(i).section.templateId)) Then

                                    If thisStructuredBody.component(i).section.templateId.Length > 0 Then

                                        Dim TemlateCount As Integer = 0
                                        SectionFound = False
                                        For TemlateCount = 0 To thisStructuredBody.component(i).section.templateId.Length - 1
                                            If (Not IsNothing(thisStructuredBody.component(i).section.templateId(TemlateCount).root)) AndAlso (TemplateID = thisStructuredBody.component(i).section.templateId(TemlateCount).root.ToString()) Then
                                                SectionFound = True
                                                Exit For
                                            End If
                                        Next
                                        If SectionFound = False Then
                                            Continue For
                                        Else
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                            If SectionFound = True Then

                                If Not IsNothing(thisStructuredBody.component(i).section.entry) Then
                                    If thisStructuredBody.component(i).section.entry.Length > 0 Then

                                        Dim oCDAMedicationSchema As POCD_MT000040UV02Entry = Nothing
                                        Dim oCDAMedicationAdmin As POCD_MT000040UV02SubstanceAdministration = Nothing
                                        Dim oCDAMedicationMaterial As POCD_MT000040UV02Material = Nothing
                                        Dim oCDAMedicationRelationShip As POCD_MT000040UV02EntryRelationship = Nothing

                                        Dim EntryCount As Integer = 0
                                        For EntryCount = 0 To thisStructuredBody.component(i).section.entry.Length - 1

                                            oCDAMedicationSchema = thisStructuredBody.component(i).section.entry(EntryCount)
                                            If (Not IsNothing(oCDAMedicationSchema)) AndAlso (Not IsNothing(oCDAMedicationSchema.Item)) Then
                                                oCDAMedicationAdmin = TryCast(oCDAMedicationSchema.Item, POCD_MT000040UV02SubstanceAdministration)
                                                If Not IsNothing(oCDAMedicationAdmin) Then
                                                    If Not IsNothing(oCDAMedicationAdmin.consumable) Then
                                                        If Not IsNothing(oCDAMedicationAdmin.consumable.manufacturedProduct) Then
                                                            If Not IsNothing(oCDAMedicationAdmin.consumable.manufacturedProduct.Item) Then
                                                                oCDAMedicationMaterial = TryCast(oCDAMedicationAdmin.consumable.manufacturedProduct.Item, POCD_MT000040UV02Material)
                                                            End If

                                                        End If
                                                    End If

                                                End If
                                            End If

                                            If Not IsNothing(oCDAMedicationAdmin) Then
                                                If Not IsNothing(oCDAMedicationAdmin.negationInd) Then
                                                    If oCDAMedicationAdmin.negationInd = True Then
                                                        NoKnownMedications = True
                                                        Continue For
                                                    End If
                                                End If
                                            End If


                                            oMedication = New Medication

                                            ''Set as Todays date. This is Update date in gloEMR
                                            oMedication.MedicationDate = DateTime.Now.ToString("MM/dd/yyyy")

                                            If Not IsNothing(oCDAMedicationAdmin) Then
                                                If Not IsNothing(oCDAMedicationAdmin.effectiveTime) Then



                                                    ''Get StartTime and Frequncy CDA objects
                                                    Dim oCDAStartTime As IVL_TS = Nothing
                                                    Dim oCDAFrequency As PIVL_TS = Nothing
                                                    Dim indexEffective As Int32
                                                    For indexEffective = 0 To oCDAMedicationAdmin.effectiveTime.Length - 1

                                                        ''IVL_TS = EffectiveDate code for Start and End DAte
                                                        If IsNothing(oCDAStartTime) Then
                                                            oCDAStartTime = TryCast(oCDAMedicationAdmin.effectiveTime(indexEffective), IVL_TS)
                                                        End If

                                                        ''PIVL_TS = Frequency and unit information
                                                        If IsNothing(oCDAFrequency) Then
                                                            oCDAFrequency = TryCast(oCDAMedicationAdmin.effectiveTime(indexEffective), PIVL_TS)
                                                        End If

                                                    Next

                                                    ''Read Low and Hight elements for Start and End time
                                                    If Not IsNothing(oCDAStartTime) Then

                                                        If (Not IsNothing(oCDAStartTime.Items)) AndAlso (Not IsNothing(oCDAStartTime.ItemsElementName)) Then
                                                            Dim indexStartTime As Int32

                                                            For indexStartTime = 0 To oCDAStartTime.Items.Length - 1
                                                                Try
                                                                    If oCDAStartTime.ItemsElementName(indexStartTime) = ItemsChoiceType2.low Then
                                                                        Dim thisIVXBTS As IVXB_TS = CType(oCDAStartTime.Items(indexStartTime), IVXB_TS)

                                                                        If (Not IsNothing(thisIVXBTS)) AndAlso (thisIVXBTS.value <> "") Then
                                                                            oMedication.StartDate = gloDateMaster.gloDate.DateAsStringDate(thisIVXBTS.value)
                                                                        End If

                                                                    End If

                                                                    If oCDAStartTime.ItemsElementName(indexStartTime) = ItemsChoiceType2.high Then
                                                                        Dim thisIVXBTS As IVXB_TS = CType(oCDAStartTime.Items(indexStartTime), IVXB_TS)

                                                                        If (Not IsNothing(thisIVXBTS)) AndAlso (thisIVXBTS.value <> "") Then
                                                                            oMedication.EndDate = gloDateMaster.gloDate.DateAsStringDate(thisIVXBTS.value)
                                                                        End If

                                                                    End If
                                                                Catch ex As Exception
                                                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                                                End Try
                                                            Next
                                                        End If
                                                    End If
                                                    oCDAStartTime = Nothing

                                                    ''Frequency
                                                    If Not IsNothing(oCDAFrequency) Then
                                                        If Not IsNothing(oCDAFrequency.period) Then
                                                            If Not IsNothing(oCDAFrequency.period.unit) Then
                                                                Dim _sUnit As String = ""
                                                                Dim objDataExt As New gloCDADataExtraction()
                                                                Dim _frequency As String = objDataExt.GetFrequencyDescription(oCDAFrequency.period.value, oCDAFrequency.period.unit)
                                                                If _frequency = "" Then
                                                                    If Convert.ToString(oCDAFrequency.period.unit) <> "1" Then
                                                                        Select Case Convert.ToString(oCDAFrequency.period.unit)
                                                                            Case "h"
                                                                                _sUnit = " Hours"
                                                                            Case "d"
                                                                                _sUnit = " Day"
                                                                        End Select
                                                                    End If
                                                                    oMedication.Frequency = Convert.ToString(oCDAFrequency.period.value) + _sUnit
                                                                Else
                                                                    oMedication.Frequency = _frequency
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                    oCDAFrequency = Nothing

                                                End If ''IsNothing(oCDAMedicationAdmin.effectiveTime)

                                                ''Dose Quantity
                                                If Not IsNothing(oCDAMedicationAdmin.doseQuantity) Then

                                                    If Not IsNothing(oCDAMedicationAdmin.doseQuantity.unit) AndAlso Convert.ToString(oCDAMedicationAdmin.doseQuantity.unit) <> "1" Then
                                                        oMedication.DrugQuantity = Convert.ToString(oCDAMedicationAdmin.doseQuantity.value) + " " + Convert.ToString(oCDAMedicationAdmin.doseQuantity.unit)
                                                    Else
                                                        oMedication.DrugQuantity = Convert.ToString(oCDAMedicationAdmin.doseQuantity.value)
                                                    End If
                                                ElseIf Not IsNothing(oCDAMedicationAdmin.rateQuantity) Then

                                                    If Not IsNothing(oCDAMedicationAdmin.rateQuantity.unit) AndAlso Convert.ToString(oCDAMedicationAdmin.rateQuantity.unit) <> "1" Then
                                                        oMedication.DrugQuantity = Convert.ToString(oCDAMedicationAdmin.rateQuantity.value) + " " + Convert.ToString(oCDAMedicationAdmin.rateQuantity.unit)
                                                    Else
                                                        oMedication.DrugQuantity = Convert.ToString(oCDAMedicationAdmin.rateQuantity.value)
                                                    End If
                                                End If

                                                ''Route
                                                If Not IsNothing(oCDAMedicationAdmin.routeCode) Then
                                                    If (Not IsNothing(oCDAMedicationAdmin.routeCode.codeSystem)) AndAlso (Not IsNothing(oCDAMedicationAdmin.routeCode.code)) Then
                                                        Dim sDescription As String = gloReconciliation.GetRouteCodeSystem(oCDAMedicationAdmin.routeCode.codeSystem, oCDAMedicationAdmin.routeCode.code)
                                                        Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                                                            If ogloGSHelper.IsRouteExist(sDescription) Then
                                                                oMedication.Route = sDescription
                                                            End If
                                                        End Using
                                                    End If
                                                End If

                                                ''DrugForm //i.e. Tablet etc
                                                If Not IsNothing(oCDAMedicationAdmin.administrationUnitCode) Then
                                                    oMedication.DrugForm = Convert.ToString(oCDAMedicationAdmin.administrationUnitCode.displayName)
                                                End If

                                                ''Status
                                                If Not IsNothing(oCDAMedicationAdmin.statusCode) Then
                                                    Dim sMedicationStatus As String = Convert.ToString(oCDAMedicationAdmin.statusCode.code)
                                                    Dim databaselayer As gloCCDDatabaseLayer = New gloCCDDatabaseLayer()
                                                    oMedication.Status = databaselayer.GetPatientMedicationStatus(sMedicationStatus)
                                                    If Not IsNothing(databaselayer) Then
                                                        databaselayer.Dispose()
                                                    End If
                                                    'oMedication.Status = Convert.ToString(oCDAMedicationAdmin.statusCode.code)
                                                End If

                                                ''Prescriber Note
                                                If (Not IsNothing(oCDAMedicationAdmin.entryRelationship)) AndAlso (oCDAMedicationAdmin.entryRelationship.Length >= 1) Then

                                                    Dim oCDAMedicationAct As POCD_MT000040UV02Act = Nothing
                                                    If Not IsNothing(oCDAMedicationAdmin.entryRelationship(0).Item) Then
                                                        oCDAMedicationAct = TryCast(oCDAMedicationAdmin.entryRelationship(0).Item, POCD_MT000040UV02Act)
                                                    End If


                                                    If Not IsNothing(oCDAMedicationAct) Then
                                                        ''Dim _TemplateID As String = oCDADataBaseLayer.getCDATemplateID("Medication Instructions")
                                                        Dim _TemplateID As String = oTemplateIDMaster.GetEntryID("Instructions")
                                                        If (Not IsNothing(oCDAMedicationAct.templateId)) AndAlso (oCDAMedicationAct.templateId.Length >= 1) AndAlso (oCDAMedicationAct.templateId(0).root = _TemplateID) Then
                                                            If Not IsNothing(oCDAMedicationAct.text) Then
                                                                If (Not IsNothing(oCDAMedicationAct.text.Text)) AndAlso (oCDAMedicationAct.text.Text.Length >= 1) Then
                                                                    oMedication.Rx_PrescriberNotes = oCDAMedicationAct.text.Text(0).Trim()
                                                                End If

                                                            End If
                                                        End If
                                                        _TemplateID = ""
                                                    End If
                                                    oCDAMedicationAct = Nothing
                                                End If


                                            End If  ''IF oCDAMedicationAdmin

                                            If Not IsNothing(oCDAMedicationMaterial) Then
                                                If Not IsNothing(oCDAMedicationMaterial.code) Then

                                                    ''RxNormCode
                                                    If Convert.ToString(oCDAMedicationMaterial.code.codeSystem) = CodeSystem.RxNorm Then
                                                        oMedication.RxNormCode = Convert.ToString(oCDAMedicationMaterial.code.code)
                                                    End If

                                                    ''Drug Name
                                                    oMedication.DrugName = Convert.ToString(oCDAMedicationMaterial.code.displayName)
                                                    oMedication.GenericName = Convert.ToString(oCDAMedicationMaterial.code.displayName)
                                                End If

                                                If Not IsNothing(oCDAMedicationMaterial.code.translation) Then
                                                    If oCDAMedicationMaterial.code.translation.Length > 0 Then
                                                        For index As Integer = 0 To oCDAMedicationMaterial.code.translation.Length - 1
                                                            If Not IsNothing(oCDAMedicationMaterial.code.translation(index)) Then
                                                                If oCDAMedicationMaterial.code.translation(index).codeSystem = CodeSystem.NDC Then
                                                                    oMedication.ProdCode = oCDAMedicationMaterial.code.translation(index).code
                                                                ElseIf oCDAMedicationMaterial.code.translation(index).codeSystem = CodeSystem.RxNorm Then
                                                                    oMedication.RxNormCode = oCDAMedicationMaterial.code.translation(index).code
                                                                    If oMedication.GenericName = "" Then
                                                                        oMedication.GenericName = oCDAMedicationMaterial.code.translation(index).displayName
                                                                    End If
                                                                End If
                                                            End If
                                                        Next
                                                    End If
                                                End If
                                            End If

                                            oMedicationList.Add(oMedication)
                                            If Not IsNothing(oMedication) Then
                                                oMedication.Dispose()
                                                oMedication = Nothing
                                            End If
                                        Next

                                        oCDAMedicationMaterial = Nothing
                                        oCDAMedicationAdmin = Nothing
                                        oCDAMedicationSchema = Nothing
                                        oCDAMedicationRelationShip = Nothing
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If
            End If




        Catch ex As Exception
            oMedicationList = Nothing
            Throw ex
        Finally
            TemplateID = Nothing
        End Try
        Return oMedicationList


    End Function
    Public Function getNONXMLBody(ByVal Filename As String, ByRef _MediaType As String, ByRef IsUnstructuredCDA As Boolean, ByRef ActualFileName As String) As String
        Dim base64String As String = String.Empty
        Try

            Dim nonXMLBody As POCD_MT000040UV02NonXMLBody = Nothing
            Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing
            oCCDSchema = gloSerialization.GetClinicalDocument(Filename)
            If Not IsNothing(oCCDSchema.component) Then
                'oCCDSchema.component = New POCD_MT000040UV02Component2
                If Not IsNothing(oCCDSchema.templateId) Then
                    If Not IsNothing(oCCDSchema.templateId) Then
                        If Not IsNothing(oCCDSchema.templateId.Length > 0) Then
                            For index As Integer = 0 To oCCDSchema.templateId.Length - 1
                                If oCCDSchema.templateId(index).root = "2.16.840.1.113883.10.20.22.1.10" Then
                                    IsUnstructuredCDA = True
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                    'If oCCDSchema.templateId = "2.16.840.1.113883.10.20.22.1.10" Then
                    '    IsUnstructuredCDA = True
                    'End If

                End If
                If IsUnstructuredCDA = True Then
                    If Not IsNothing(oCCDSchema.title.Text) Then
                        ActualFileName = oCCDSchema.title.Text(0)
                    End If

                    If Not IsNothing(oCCDSchema.component.Item) Then
                        nonXMLBody = oCCDSchema.component.Item
                        If Not IsNothing(nonXMLBody.text) Then
                            'nonXMLBody.text = New ED()
                            _MediaType = nonXMLBody.text.mediaType
                            If Not IsNothing(nonXMLBody.text.representation) Then
                                If nonXMLBody.text.representation = BinaryDataEncoding.B64 AndAlso Not IsNothing(nonXMLBody.text.Text) Then
                                    base64String = nonXMLBody.text.Text(0)
                                End If
                            End If
                        End If
                    End If
                End If


            End If
            Return base64String
        Catch ex As Exception
            Return base64String
        End Try
    End Function
End Class
