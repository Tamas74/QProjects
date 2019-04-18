Imports System.Web.Services
Imports System.IO
Imports System.Net
Imports System.Xml
Imports gloCCDLibrary.com.diagnosisone.glostreampes

Public Class clsDiagnosisone_BusuinessLayer
    Dim webServiceHandle As New D1_PESService()
    Public ErrMessage As String = ""
    Public Function PostCDDfile(ByVal RequestfileName As String, ByVal ResponcefilePath As String, ByVal CatalogCode As String) As String
        Try
            ' Dim strfilepath As String = RequestfileName 'Application.StartupPath
            Dim processCount As Integer = 0
            Dim ackText As String = ""
            Dim responseText As String = ""
            ErrMessage = ""
            'If Not System.IO.Directory.Exists(Application.StartupPath & "\gloCCD") Then
            '    System.IO.Directory.CreateDirectory(Application.StartupPath & "\gloCCD")
            'End If
            'Dim dir As DirectoryInfo = New DirectoryInfo(Application.StartupPath & "\gloCCD")

            'For Each file As FileInfo In dir.GetFiles("*.xml")
            Dim file As New FileInfo(RequestfileName)
            Dim input As New evaluatePatient_hl7_v3_cda_r2()

            Dim mesg As evaluatePatient_hl7_v3_cda_r2RCMR_IN000002UV01 = CreateMessage(file.FullName)

            Dim res As New evaluatePatient_hl7_v3_cda_r2Response()
            input.RCMR_IN000002UV01 = mesg
            input.catalogCode = CatalogCode
            res = webServiceHandle.evaluatePatient_hl7_v3_cda_r2(input)

            ''Batch Interaction
            ''Dim mesg1 As MCCI_MT200100Batch = CreateMessage1(file.FullName)
            ''Dim res1 As New evaluatePatient_hl7_v3_cda_r2_batchResponse()
            ''Dim input1 As New evaluatePatient_hl7_v3_cda_r2_batch()

            ' ''  input1.MCCIIN200100 = mesg1
            ''input1.catalogCode = "Base Rule Set-Screening"
            ''res1 = webServiceHandle.evaluatePatient_hl7_v3_cda_r2_batch(input1)


            '''''submit batch
            '' ''Dim mesg1 As submitPatient_hl7_v3_cda_r2_batchMCCIIN200100 = CreateMessage3(file.FullName)
            '' ''Dim res1 As New submitPatient_hl7_v3_cda_r2_batchResponse()
            '' ''Dim input1 As New submitPatient_hl7_v3_cda_r2_batch()
            '' ''input1.MCCIIN200100 = mesg1


            '' ''res1 = webServiceHandle.submitPatient_hl7_v3_cda_r2_batch(input1)
            '''''
            '''' Submit Patient
            '' ''Dim msg As submitPatient_hl7_v3_cda_r2RCMRIN000002UV01 = CreateMessage2(file.FullName)
            '' ''Dim res1 As New submitPatient_hl7_v3_cda_r2Response()
            '' ''Dim input1 As New submitPatient_hl7_v3_cda_r2()
            '' ''input1.RCMRIN000002UV01 = msg

            '' ''res1 = webServiceHandle.submitPatient_hl7_v3_cda_r2(input1)
            '''''
            ''  WriteFile(outputFileName, file.FullName + " > The ack code is: " + res.MCCIMT002300Message.acknowledgement.typeCode.code & vbCr & vbLf)

            ackText = res.MCCIMT002300Message.acknowledgement.acknowledgementDetail(0).text.Value

            '  WriteFile(outputFileName, ackText & vbCr & vbLf)

            responseText = ""
            If IsNothing(res.MCCIMT002300Message.attachment) = False Then
                For Each a As MCCI_MT002300Attachment In res.MCCIMT002300Message.attachment


                    responseText = "<?xml-stylesheet type='text/xsl' href='" & gloCCDSchema.gloCDAWriterParameters.CDAStyleSheetPath & "'?>"
                    '  responseText = "<?xml-stylesheet type='text/xsl' href='http://www.glostream.com/css/XSLT/gloCCDAcss_MU2.xsl'?>"
                    ' responseText = "<?xml-stylesheet type='text/xsl' href=' " & Application.StartupPath & "/" & "sampleCdaXform.xsl" '?>"
                    responseText += a.text.Value
                    responseText += vbCr & vbLf
                Next
            Else
                responseText = "<Root>1</Root>"
                responseText = "<Response>Responce is Nothing with Acknowllagement text:</Response>"
                'responseText = vbCr & vbLf
                responseText = "<Ack>" + ackText + "</Ack>"
            End If


            ''  MessageBox.Show(ackText)

            '  WriteFile(outputFileName, ">> The ack attachment text is: " & responseText & vbCr & vbLf)
            processCount += 1
            'Next
            ResponcefilePath = GenerateFileName("", ResponcefilePath)


            Dim empDoc As XmlDocument = New XmlDocument()
            empDoc.LoadXml(responseText)

            ''''
            'Dim xmlwriter As XmlTextWriter = Nothing
            'xmlwriter = New XmlTextWriter(strfilepath, Nothing)
            'xmlwriter.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='http://www.glostream.com/css/XSLT/gloccdCss.xsl'")
            'xmlwriter.Close()

            empDoc.Save(ResponcefilePath)

            If Not IsNothing(empDoc) Then
                empDoc = Nothing
            End If
            If Not IsNothing(res) Then
                res = Nothing
            End If
            If Not IsNothing(input) Then
                input = Nothing
            End If
            If Not IsNothing(mesg) Then
                mesg = Nothing
            End If
            'WebBrowser1.Visible = True
            'Dim ofile As FileInfo = New FileInfo(strfilepath)
            'Me.WebBrowser1.Navigate(ofile.FullName)
            'WebBrowser1.Dock = DockStyle.Fill

            ' Dim objfrm As New Form2

            'Dim ofile1 As FileInfo = New FileInfo(strfilepath)
            'objfrm.WebBrowser1.Navigate(ofile1.FullName)

            ''Me.Focus()
            'objfrm.ShowInTaskbar = False
            'objfrm.ShowDialog(Me)
        Catch ex As Exception
            'If (ex.ToString() = "Unable to connect to the remote server") Then
            '    'Throw Exception
            'End If
            'If (ex.ToString.Contains("The request failed with HTTP status 401")) Then
            '    ErrMessage = "Invalid URL."
            'ElseIf (ex.ToString.Contains("The request failed with HTTP status 401")) Then
            '    ErrMessage = ""
            'Else

            ErrMessage = ex.Message.ToString()
            If (ErrMessage.Contains("The request failed with HTTP status 401: Unauthorized.")) Then
                ErrMessage = ErrMessage & vbCrLf & "Note :The 401 Unauthorized HTTP status code means the diagnosis one credentials you entered in the admin were invalid."
            End If
            'End If
            'MessageBox.Show(ex.ToString())
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return ResponcefilePath
    End Function
    Private Function Generate(ByVal strfilepath As String) As String

        Dim xmlwriter As XmlTextWriter = Nothing

        Try
            If System.IO.File.Exists(strfilepath) Then
                System.IO.File.Delete(strfilepath)
            End If
            Try
                XmlWriter = New XmlTextWriter(strfilepath, Nothing)
            Catch ex As Exception
                ''  Throw New gloCCDException("You do not have write permissions for the CCD directory." & vbCrLf & "Contact your system administrator or configure a different directory for CCD.")
                'MessageBox.Show(ex.ToString())
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                Return Nothing
                Exit Function
            End Try
            XmlWriter.Formatting = Formatting.Indented

            XmlWriter.WriteStartDocument()
            XmlWriter.WriteStartElement("ClinicalDocument") 'Open the Main Parent Node 

            XmlWriter.WriteAttributeString("xsi:schemaLocation", "urn:hl7-org:v3 http://xreg2.nist.gov:8080/hitspValidation/schema/cdar2c32/infrastructure/cda/C32_CDA.xsd")
            XmlWriter.WriteAttributeString("xmlns:sdtc", "urn:hl7-org:sdtc")
            XmlWriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            XmlWriter.WriteAttributeString("xmlns", "urn:hl7-org:v3")


            XmlWriter.WriteStartElement("typeId")
            XmlWriter.WriteAttributeString("extension", "POCD_HD000040")
            XmlWriter.WriteAttributeString("root", "2.16.840.1.113883.1.3")
            XmlWriter.WriteAttributeString("assigningAuthorityName", "DiagnosisOne")
            XmlWriter.WriteEndElement() 'End TypeId Element

            XmlWriter.WriteStartElement("code")
            XmlWriter.WriteAttributeString("code", "34109-9")
            XmlWriter.WriteAttributeString("displayName", "EVALUATION AND MANAGEMENT NOTE")
            XmlWriter.WriteAttributeString("codeSystemName", "LOINC")
            XmlWriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
            XmlWriter.WriteEndElement() 'End Code Element

            XmlWriter.WriteElementString("title", "EVALUATION AND MANAGEMENT NOTE")

            XmlWriter.WriteStartElement("effectiveTime")
            Dim dtTodayDate As String = Now.Date.Year & Format(Now.Date.Month, "#00") & Format(Now.Date.Day, "#00") & "000000-0500" '& Now.Hour & Now.Minute
            XmlWriter.WriteAttributeString("value", dtTodayDate) '"19870618000000-0500"datetimestamp when file generated
            XmlWriter.WriteEndElement() 'End effectiveTime Element

            XmlWriter.WriteStartElement("confidentialityCode")
            XmlWriter.WriteAttributeString("code", "N")
            XmlWriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.25")
            XmlWriter.WriteEndElement() 'End confidentialityCode

            XmlWriter.WriteStartElement("languageCode")
            XmlWriter.WriteAttributeString("code", "en-US") 'datetimestamp when file generated
            XmlWriter.WriteEndElement() 'End Languagecode Element
            Return strfilepath
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            XmlWriter = Nothing
        End Try

    End Function
    Public Function GenerateFileName(ByVal PatientCode As String, Optional ByVal _path As String = "") As String
        Dim strfilename As String = ""
        Try
            strfilename = "D1_"
            ''        Dim _path As String = ""
            ''_path = Application.StartupPath & "\"
            Dim dtdate As DateTime = Date.UtcNow
            Dim strtemp As String
            If PatientCode = "" Then
                strtemp = strfilename & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
            Else
                strtemp = strfilename & PatientCode & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
            End If


            strfilename = _path & strtemp & ".xml"
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return strfilename
    End Function

    Private Function CreateMessage(ByVal fileName As String) As evaluatePatient_hl7_v3_cda_r2RCMR_IN000002UV01
        ''''/ RCMR_IN000002UV01 mesg = new RCMR_IN000002UV01();
        Dim mesg As New evaluatePatient_hl7_v3_cda_r2RCMR_IN000002UV01()
        Dim realmcode As New gloCCDLibrary.com.diagnosisone.glostreampes.CS()
        Dim typeid As New allInfrastructureRoottypeId()
        Dim templateid As New allInfrastructureRoottemplateId()
        Dim id As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim creationtime As New gloCCDLibrary.com.diagnosisone.glostreampes.TS()
        Dim interactionid As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim processingcode As New gloCCDLibrary.com.diagnosisone.glostreampes.CS()
        Dim processingmoodcode As New gloCCDLibrary.com.diagnosisone.glostreampes.CS()
        Dim acceptackcode As New gloCCDLibrary.com.diagnosisone.glostreampes.CS()
        Dim receiver As New MCCI_MT000100UV01Receiver
        Dim deviceid As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim senderdevice As New MCCI_MT000100UV01Sender()
        Dim deviceidentifier As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim cActProcess As New RCMR_IN000002UV01MCAI_MT700201UV01ControlActProcess()
        Dim code As New gloCCDLibrary.com.diagnosisone.glostreampes.CD()
        Dim cdaText As New gloCCDLibrary.com.diagnosisone.glostreampes.ED()
        Dim cdaDoc As String = ""
        Try
            mesg.ITSVersion = "XML_1.0"
            ' Adds the realmCode object

            realmcode.code = "token"
            realmcode.codeSystem = "string"
            realmcode.codeSystemName = "string"
            realmcode.codeSystemVersion = "string"
            realmcode.nullFlavor = csNullFlavor.OTH
            mesg.realmCode = New gloCCDLibrary.com.diagnosisone.glostreampes.CS() {realmcode}

            ' Added typeid to the RCMR message
            typeid.assigningAuthorityName = "string"
            typeid.displayable = True
            typeid.extension = "string"
            typeid.nullFlavor = csNullFlavor.NASK
            typeid.root = "2.16.840.1.113883.1.3"
            mesg.typeId = typeid

            ' Adds the templateid
            templateid.assigningAuthorityName = "string"
            templateid.displayable = False
            templateid.extension = "string"
            templateid.nullFlavor = csNullFlavor.NAV
            templateid.root = "string"
            templateid.unsorted = True
            mesg.templateId = New allInfrastructureRoottemplateId() {templateid}

            ' assigns the id to the RCMR message
            id.displayable = True
            id.extension = "7_27_07-1XXX"
            id.root = "2.16.840.1.113883.19.3.2409"
            mesg.id = id

            creationtime.value = "20070726103200"
            mesg.creationTime = creationtime


            interactionid.displayable = True
            interactionid.extension = "1-976-245"
            interactionid.root = "2.16.840.1.113883.19.3.2409"
            mesg.interactionId = interactionid


            processingcode.code = "P"
            processingcode.codeSystem = "string"
            processingcode.codeSystemName = "string"
            processingcode.codeSystemVersion = "string"
            mesg.processingCode = processingcode


            processingmoodcode.code = "T"
            processingmoodcode.codeSystem = "string"
            processingmoodcode.codeSystemName = "string"
            processingmoodcode.codeSystemVersion = "string"
            mesg.processingModeCode = processingmoodcode


            acceptackcode.code = "AL"
            mesg.acceptAckCode = acceptackcode


            receiver.device = New MCCI_MT000100UV01Device()
            deviceid.extension = "D1.PES"
            deviceid.root = "2.16.840.1.113883.4.49"
            receiver.device.id = New gloCCDLibrary.com.diagnosisone.glostreampes.II() {deviceid}
            mesg.receiver = New MCCI_MT000100UV01Receiver() {receiver}


            senderdevice.device = New MCCI_MT000100UV01Device()
            deviceidentifier.extension = "D1DP"
            deviceidentifier.root = "2.16.840.1.113883.4.49"
            senderdevice.device.id = New gloCCDLibrary.com.diagnosisone.glostreampes.II() {deviceid}
            mesg.sender = senderdevice

            ' Sets the ControlActProcess
            code.code = "34109-9"
            code.codeSystem = "2.16.840.1.113883.6.1"
            code.displayName = "EVALUATION AND MANAGEMENT NOTE"
            cActProcess.code = code
            cActProcess.classCode = actClassControlAct.ACTN

            cActProcess.moodCode = New String() {"RQO"}
            cdaText.mediaType = "text/plain"

            cdaDoc = ReadFile(fileName)
            'Response.Write(cdaDoc);
            ' cdaText.Value.ToString()   = new string[] { cdaDoc };
            ''''/cActProcess.text = cdaText;
            ''''/mesg.controlActProcess = cActProcess;
            ''''//
            cActProcess.text = New gloCCDLibrary.com.diagnosisone.glostreampes.ED()
            cActProcess.text.mediaType = "text/plain"
            cActProcess.text.Value = cdaDoc
            mesg.controlActProcess = cActProcess
            ''''/
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        Finally
            realmcode = Nothing
            typeid = Nothing
            templateid = Nothing
            id = Nothing
            creationtime = Nothing
            interactionid = Nothing
            processingcode = Nothing
            processingmoodcode = Nothing
            acceptackcode = Nothing
            receiver = Nothing
            deviceid = Nothing
            senderdevice = Nothing
            deviceidentifier = Nothing
            cActProcess = Nothing
            code = Nothing
            cdaText = Nothing
            cdaDoc = Nothing
        End Try
        Return mesg
    End Function

    'Private Function CreateMessage1(ByVal fileName As String) As evaluatePatient_hl7_v3_cda_r2_batchMCCIIN200100
    '    '''/ RCMR_IN000002UV01 mesg = new RCMR_IN000002UV01();
    '    Dim mesg As New evaluatePatient_hl7_v3_cda_r2_batchMCCIIN200100()
    '    mesg.ITSVersion = "XML_1.0"
    '    ' Adds the realmCode object

    '    Dim id As New II()
    '    id.displayable = True
    '    id.extension = "1-976-245XXXX"
    '    id.root = "2.16.840.1.113883.19.3.2409"
    '    mesg.id = id

    '    Dim creationtime As New TS()
    '    creationtime.value = "20070727100000"
    '    mesg.creationTime = creationtime

    '    Dim ResponseMoodCode As New CS()

    '    ResponseMoodCode.code = "I"
    '    ResponseMoodCode.codeSystem = "string"
    '    ResponseMoodCode.codeSystemName = "string"
    '    ResponseMoodCode.codeSystemVersion = "string"
    '    mesg.responseModeCode = ResponseMoodCode

    '    Dim interactionid As New II()
    '    interactionid.displayable = True
    '    interactionid.extension = "1-976-245"
    '    interactionid.root = "2.16.840.1.113883.19.3.2409"
    '    mesg.interactionId = interactionid

    '    Dim receiver As New MCCI_MT200100Receiver

    '    receiver.device = New MCCI_MT200100Device()
    '    Dim deviceid As New II()
    '    deviceid.extension = "D1.PES"
    '    deviceid.root = "2.16.840.1.113883.4.49"
    '    receiver.device.id = New II() {deviceid}
    '    mesg.receiver = New MCCI_MT200100Receiver() {receiver}



    '    Dim senderdevice As New MCCI_MT200100Sender()
    '    senderdevice.device = New MCCI_MT200100Device()
    '    Dim deviceidentifier As New II()
    '    deviceidentifier.extension = "D1DP"
    '    deviceidentifier.root = "2.16.840.1.113883.4.49"
    '    senderdevice.device.id = New II() {deviceid}
    '    mesg.sender = senderdevice

    '    ' Sets the ControlActProcess
    '    Dim cActProcess As New RCMR_IN000002UV01MCAI_MT700201UV01ControlActProcess()
    '    Dim code As New CD()
    '    code.code = "34109-9"
    '    code.codeSystem = "2.16.840.1.113883.6.1"
    '    code.displayName = "EVALUATION AND MANAGEMENT NOTE"
    '    cActProcess.code = code
    '    cActProcess.classCode = actClassControlAct.ACTN

    '    cActProcess.moodCode = New String() {"RQO"}
    '    Dim cdaText As New ED()
    '    cdaText.mediaType = "text/plain"

    '    Dim cdaDoc As String = ReadFile(fileName)
    '    'Response.Write(cdaDoc);
    '    ' cdaText.Value.ToString()   = new string[] { cdaDoc };
    '    '''/cActProcess.text = cdaText;
    '    '''/mesg.controlActProcess = cActProcess;
    '    '''//
    '    cActProcess.text = New ED()
    '    cActProcess.text.mediaType = "text/plain"
    '    cActProcess.text.Value = cdaDoc
    '    ''  mesg.controlActProcess = cActProcess
    '    '''/

    '    Return mesg
    'End Function

    Private Function CreateMessage3(ByVal fileName As String) As submitPatient_hl7_v3_cda_r2_batchMCCIIN200100
        ''''/ RCMR_IN000002UV01 mesg = new RCMR_IN000002UV01();
        Dim mesg As New submitPatient_hl7_v3_cda_r2_batchMCCIIN200100()
        Dim id As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim creationtime As New gloCCDLibrary.com.diagnosisone.glostreampes.TS()
        Dim ResponseMoodCode As New gloCCDLibrary.com.diagnosisone.glostreampes.CS()
        Dim interactionid As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim receiver As New gloCCDLibrary.com.diagnosisone.glostreampes.MCCI_MT200100Receiver
        Dim deviceid As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim senderdevice As New gloCCDLibrary.com.diagnosisone.glostreampes.MCCI_MT200100Sender()
        Dim deviceidentifier As New II()
        Dim cActProcess As New gloCCDLibrary.com.diagnosisone.glostreampes.RCMR_IN000002UV01MCAI_MT700201UV01ControlActProcess()
        Dim code As New gloCCDLibrary.com.diagnosisone.glostreampes.CD()
        Dim cdaText As New gloCCDLibrary.com.diagnosisone.glostreampes.ED()
        Dim cdaDoc As String = ""

        Try
            mesg.ITSVersion = "XML_1.0"
            ' Adds the realmCode object

            id.displayable = True
            id.extension = "1-976-245XXXX"
            id.root = "2.16.840.1.113883.19.3.2409"
            mesg.id = id

            creationtime.value = "20070727100000"
            mesg.creationTime = creationtime


            ResponseMoodCode.code = "I"
            ResponseMoodCode.codeSystem = "string"
            ResponseMoodCode.codeSystemName = "string"
            ResponseMoodCode.codeSystemVersion = "string"
            mesg.responseModeCode = ResponseMoodCode

            interactionid.displayable = True
            interactionid.extension = "1-976-245"
            interactionid.root = "2.16.840.1.113883.19.3.2409"
            mesg.interactionId = interactionid



            'Dim receiver As New MCCI_MT200100Receiver()
            'receiver.device = New MCCI_MT200100Device()
            'Dim deviceid As New II()
            'deviceid.extension = "D1.PES"
            'deviceid.root = "2.16.840.1.113883.4.49"
            'receiver.device.id = New II() {deviceid}
            'mesg.receiver = New MCCI_MT200100Receiver() {receiver}

            'Dim receiver As New MCCI_MT000100UV01Receiver
            'receiver.device = New MCCI_MT000100UV01Device
            'Dim deviceid As New II()
            'deviceid.extension = "D1.PES"
            'deviceid.root = "2.16.840.1.113883.4.49"
            'receiver.device.id = New II() {deviceid}
            'mesg.receiver = New MCCI_MT000100UV01Receiver{receiver}

            'Dim receiver As New MCCI_MT002300Receiver
            'receiver.device = New MCCI_MT002300Device()
            'Dim deviceid As New II()
            'deviceid.extension = "D1.PES"
            'deviceid.root = "2.16.840.1.113883.4.49"
            'receiver.device.id = New II() {deviceid}
            'mesg.receiver = New MCCI_MT002300Receiver() {receiver}

            'Dim receiver As New MCCI_MT200101Receiver()
            'receiver.device = New MCCI_MT200101Device()
            'Dim deviceid As New II()
            'deviceid.extension = "D1.PES"
            'deviceid.root = "2.16.840.1.113883.4.49"
            'receiver.device.id = New II() {deviceid}
            'mesg.receiver = New MCCI_MT200101Receiver() {receiver}



            'receiver.device = New MCCI_MT200100Device()
            'Dim deviceid As New II()
            'deviceid.extension = "D1.PES"
            'deviceid.root = "2.16.840.1.113883.4.49"
            'receiver.device.id = New II() {deviceid}
            'mesg.receiver = New MCCI_MT200100Receiver() {receiver}


            receiver.device = New MCCI_MT200100Device()
            deviceid.extension = "D1.PES"
            deviceid.root = "2.16.840.1.113883.4.49"
            receiver.device.id = New gloCCDLibrary.com.diagnosisone.glostreampes.II() {deviceid}
            mesg.receiver = receiver



            senderdevice.device = New MCCI_MT200100Device()
            deviceidentifier.extension = "D1DP"
            deviceidentifier.root = "2.16.840.1.113883.4.49"
            senderdevice.device.id = New gloCCDLibrary.com.diagnosisone.glostreampes.II() {deviceid}
            mesg.sender = senderdevice

            ' Sets the ControlActProcess
            code.code = "34109-9"
            code.codeSystem = "2.16.840.1.113883.6.1"
            code.displayName = "EVALUATION AND MANAGEMENT NOTE"
            cActProcess.code = code
            cActProcess.classCode = actClassControlAct.ACTN

            cActProcess.moodCode = New String() {"RQO"}
            cdaText.mediaType = "text/plain"

            cdaDoc = ReadFile(fileName)
            'Response.Write(cdaDoc);
            ' cdaText.Value.ToString()   = new string[] { cdaDoc };
            ''''/cActProcess.text = cdaText;
            ''''/mesg.controlActProcess = cActProcess;
            ''''//
            cActProcess.text = New gloCCDLibrary.com.diagnosisone.glostreampes.ED()
            cActProcess.text.mediaType = "text/plain"
            cActProcess.text.Value = cdaDoc
            ''  mesg.controlActProcess = cActProcess
            ''''/
            Return mesg
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        Finally
            id = Nothing
            creationtime = Nothing
            ResponseMoodCode = Nothing
            interactionid = Nothing
            receiver = Nothing
            deviceid = Nothing
            senderdevice = Nothing
            deviceidentifier = Nothing
            cActProcess = Nothing
            code = Nothing
            cdaText = Nothing
            cdaDoc = Nothing
        End Try
        Return mesg
    End Function

    Private Function CreateMessage2(ByVal fileName As String) As submitPatient_hl7_v3_cda_r2RCMRIN000002UV01
        Dim mesg As New submitPatient_hl7_v3_cda_r2RCMRIN000002UV01()
        Dim realmcode As New gloCCDLibrary.com.diagnosisone.glostreampes.CS()
        Dim typeid As New allInfrastructureRoottypeId()
        Dim templateid As New allInfrastructureRoottemplateId()
        Dim id As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim creationtime As New gloCCDLibrary.com.diagnosisone.glostreampes.TS()
        Dim interactionid As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim processingcode As New gloCCDLibrary.com.diagnosisone.glostreampes.CS()
        Dim processingmoodcode As New gloCCDLibrary.com.diagnosisone.glostreampes.CS()
        Dim acceptackcode As New gloCCDLibrary.com.diagnosisone.glostreampes.CS()
        Dim receiver As New gloCCDLibrary.com.diagnosisone.glostreampes.MCCI_MT000100UV01Receiver()
        Dim deviceid As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim senderdevice As New MCCI_MT000100UV01Sender()
        Dim deviceidentifier As New gloCCDLibrary.com.diagnosisone.glostreampes.II()
        Dim cActProcess As New RCMR_IN000002UV01MCAI_MT700201UV01ControlActProcess()
        Dim code As New gloCCDLibrary.com.diagnosisone.glostreampes.CD()
        Dim cdaText As New gloCCDLibrary.com.diagnosisone.glostreampes.ED()
        Dim cdaDoc As String = ""
        Try
            ''''/ RCMR_IN000002UV01 mesg = new RCMR_IN000002UV01();
            mesg.ITSVersion = "XML_1.0"
            ' Adds the realmCode object

            realmcode.code = "token"
            realmcode.codeSystem = "string"
            realmcode.codeSystemName = "string"
            realmcode.codeSystemVersion = "string"
            realmcode.nullFlavor = csNullFlavor.OTH
            mesg.realmCode = New gloCCDLibrary.com.diagnosisone.glostreampes.CS() {realmcode}

            ' Added typeid to the RCMR message
            typeid.assigningAuthorityName = "string"
            typeid.displayable = True
            typeid.extension = "string"
            typeid.nullFlavor = csNullFlavor.NASK
            typeid.root = "2.16.840.1.113883.1.3"
            mesg.typeId = typeid

            ' Adds the templateid
            templateid.assigningAuthorityName = "string"
            templateid.displayable = False
            templateid.extension = "string"
            templateid.nullFlavor = csNullFlavor.NAV
            templateid.root = "string"
            templateid.unsorted = True
            mesg.templateId = New allInfrastructureRoottemplateId() {templateid}

            ' assigns the id to the RCMR message
            id.displayable = True
            id.extension = "7_27_07-1XXX"
            id.root = "2.16.840.1.113883.19.3.2409"
            mesg.id = id

            creationtime.value = "20070726103200"
            mesg.creationTime = creationtime


            interactionid.displayable = True
            interactionid.extension = "1-976-245"
            interactionid.root = "2.16.840.1.113883.19.3.2409"
            mesg.interactionId = interactionid


            processingcode.code = "P"
            processingcode.codeSystem = "string"
            processingcode.codeSystemName = "string"
            processingcode.codeSystemVersion = "string"
            mesg.processingCode = processingcode


            processingmoodcode.code = "T"
            processingmoodcode.codeSystem = "string"
            processingmoodcode.codeSystemName = "string"
            processingmoodcode.codeSystemVersion = "string"
            mesg.processingModeCode = processingmoodcode


            acceptackcode.code = "AL"
            mesg.acceptAckCode = acceptackcode


            receiver.device = New MCCI_MT000100UV01Device()
            deviceid.extension = "D1.PES"
            deviceid.root = "2.16.840.1.113883.4.49"
            receiver.device.id = New gloCCDLibrary.com.diagnosisone.glostreampes.II() {deviceid}
            mesg.receiver = New MCCI_MT000100UV01Receiver() {receiver}


            senderdevice.device = New MCCI_MT000100UV01Device()
            deviceidentifier.extension = "D1DP"
            deviceidentifier.root = "2.16.840.1.113883.4.49"
            senderdevice.device.id = New gloCCDLibrary.com.diagnosisone.glostreampes.II() {deviceid}
            mesg.sender = senderdevice

            ' Sets the ControlActProcess
            code.code = "34109-9"
            code.codeSystem = "2.16.840.1.113883.6.1"
            code.displayName = "EVALUATION AND MANAGEMENT NOTE"
            cActProcess.code = code
            cActProcess.classCode = actClassControlAct.ACTN

            cActProcess.moodCode = New String() {"RQO"}
            cdaText.mediaType = "text/plain"

            cdaDoc = ReadFile(fileName)
            'Response.Write(cdaDoc);
            ' cdaText.Value.ToString()   = new string[] { cdaDoc };
            ''''/cActProcess.text = cdaText;
            ''''/mesg.controlActProcess = cActProcess;
            ''''//
            cActProcess.text = New gloCCDLibrary.com.diagnosisone.glostreampes.ED()
            cActProcess.text.mediaType = "text/plain"
            cActProcess.text.Value = cdaDoc
            mesg.controlActProcess = cActProcess
            ''''/
            Return mesg
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        Finally
            realmcode = Nothing
            typeid = Nothing
            templateid = Nothing
            id = Nothing
            creationtime = Nothing
            interactionid = Nothing
            processingcode = Nothing
            processingmoodcode = Nothing
            acceptackcode = Nothing
            receiver = Nothing
            deviceid = Nothing
            senderdevice = Nothing
            deviceidentifier = Nothing
            cActProcess = Nothing
            code = Nothing
            cdaText = Nothing
            cdaDoc = Nothing
        End Try
        Return mesg
    End Function
    Public Sub WriteFile(ByVal fileName As String, ByVal text As String)
        Try
            ' Specify file, instructions, and privelegdes
            Dim file As New FileStream(fileName, FileMode.Append, FileAccess.Write)

            ' Create a new stream to write to the file
            Dim sw As New StreamWriter(file)

            ' Write a string to the file
            sw.Write(text)

            ' Close StreamWriter
            sw.Close()

            ' Close file
            file.Close()

            If Not IsNothing(sw) Then
                sw.Dispose()
                sw = Nothing
            End If
            If Not IsNothing(file) Then
                file.Dispose()
                file = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Function ReadFile(ByVal fileName As String) As String
        Dim s As String = ""
        Try
            ' Specify file, instructions, and privelegdes
            Dim file As New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read)

            ' Create a new stream to read from a file
            Dim sr As New StreamReader(file)

            ' Read contents of file into a string
            s = sr.ReadToEnd()

            ' Close StreamReader
            sr.Close()

            ' Close file
            file.Close()

            If Not IsNothing(sr) Then
                sr.Dispose()
                sr = Nothing
            End If
            If Not IsNothing(file) Then
                file.Dispose()
                file = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return s
    End Function

    'Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    webServiceHandle.Url = "http://glostreampes.diagnosisone.com/PES/D1_PES?WSDL"
    '    webServiceHandle.Credentials = AuthenticateWebService("gs_pes_clnt", "glostream")
    'End Sub
    Private Function AuthenticateWebService(ByVal username As String, ByVal password As String) As CredentialCache
        Dim myCredentials As New CredentialCache()
        Try
            Dim netCred As New NetworkCredential(username, password)
            myCredentials.Add(New Uri(webServiceHandle.Url), "Basic", netCred)
            netCred = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return myCredentials
    End Function

    Public Sub New(ByVal Url As String, ByVal UserName As String, ByVal Password As String)
        Try
            webServiceHandle.Url = Url
            webServiceHandle.Credentials = AuthenticateWebService(UserName, Password)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub New()
        'constructor is only used for accessing GenerateFile 
    End Sub
    Public Function GenerateFile(ByVal cntFromDB As Object, ByVal strFileName As String) As String
        Try
            If Not cntFromDB Is Nothing Then
                Dim content() As Byte = CType(cntFromDB, Byte())
                'Dim stream As MemoryStream = New MemoryStream(content)
                Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                oFile.Write(content, 0, content.Length)
                ' stream.WriteTo(oFile)
                oFile.Close()
                oFile.Dispose()
                'stream.Close()
                'stream.Dispose()
                content = Nothing
                'stream = Nothing
                oFile = Nothing
                Return strFileName
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
    End Function
End Class
