Imports System.Windows.Forms
Imports System.IO
Imports gloSureScript.gloSureScriptInterface
Imports System.Data.SqlClient

Public Class gloSurescriptGeneral

    Public Shared gnLoginUserId As Int64

    Public Shared gblnIsSQLAuthentication As Boolean
    Public Shared ServerName As String
    Public Shared DatabaseName As String
    Public Shared sUserName As String
    Public Shared sPassword As String
    Public Shared RootPath As String
    Public Shared blnIsStagingServer As Boolean
    'varibles added for 10.6 implementation to stored url and download version
    Public Shared eRxStagingWebserviceURL As String
    Public Shared eRxProductionWebserviceURL As String
    Public Shared eRx10dot6StagingWebserviceURL As String
    Public Shared eRx10dot6ProductionWebserviceURL As String
    Public Shared gstrAUSIDClinic As String
    Public Shared sDIBServiceURL As String = ""
    Public Shared _isInternetAvailable As Boolean = False 'use this variable throughtout the RxForm to add validation to confirm whether there is internet connection available 


    Public Shared Function GetconnectionString() As String
        If gblnIsSQLAuthentication = False Then
            Return "SERVER= " & ServerName & ";DATABASE=" & DatabaseName & ";Integrated Security=SSPI"
        Else
            Return "SERVER= " & ServerName & ";DATABASE=" & DatabaseName & ";User ID=" & sUserName & ";Password=" & sPassword & ""
        End If

    End Function

    Public Shared Sub UpdateLog(ByVal strLogMessage As String)
        Dim objFile As System.IO.StreamWriter = Nothing
        Try

            Dim strPath As String = ""
            strPath = Application.StartupPath & "\gloSurescriptLogTest.Log"
            objFile = New System.IO.StreamWriter(strPath, True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()

        Catch ex As Exception

        Finally
            If Not IsNothing(objFile) Then
                objFile.Dispose()
                objFile = Nothing
            End If

        End Try
    End Sub

    Public Shared Sub ErrorMessage(ByVal strMessage As String)
        Try
            MessageBox.Show(strMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub InformationMessage(ByVal strMessage As String)
        Try
            MessageBox.Show(strMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub QuestionMessage(ByVal strMessage As String)
        Try
            MessageBox.Show(strMessage, "Question", MessageBoxButtons.OK, MessageBoxIcon.Question)
        Catch ex As Exception

        End Try
    End Sub
    'commented old code for checking internet connection 
    'Public Shared Function IsInternetConnectionAvailable() As Boolean
    '    ' Returns True if connection is available 
    '    ' Replace www.yoursite.com with a site that 
    '    ' is guaranteed to be online - perhaps your 
    '    ' corporate site, or microsoft.com 
    '    Dim objUrl As New System.Uri("http://www.Google.com/")
    '    ' Setup WebRequest 
    '    Dim objWebReq As System.Net.WebRequest
    '    objWebReq = System.Net.WebRequest.Create(objUrl)
    '    Dim objResp As System.Net.WebResponse
    '    Try
    '        ' Attempt to get response and return True 
    '        objResp = objWebReq.GetResponse
    '        objResp.Close()
    '        objWebReq = Nothing
    '        Return True
    '    Catch ex As Exception
    '        ' Error, exit and return False 
    '        objWebReq = Nothing
    '        Return False
    '    End Try
    'End Function

    'added new function to check internet connection
    Private Declare Function InternetGetConnectedState Lib "wininet" (ByRef conn As Long, ByVal val As Long) As Boolean

    Public Shared Function IsInternetConnectionAvailable() As Boolean
        '' Returns True if connection is available 
        '' Replace www.yoursite.com with a site that 
        '' is guaranteed to be online - perhaps your 
        '' corporate site, or microsoft.com 
        'Dim objUrl As New System.Uri("http://www.Google.com/")
        '' Setup WebRequest 
        'Dim objWebReq As System.Net.WebRequest
        'objWebReq = System.Net.WebRequest.Create(objUrl)
        'Dim objResp As System.Net.WebResponse
        'Try
        '    ' Attempt to get response and return True 
        '    objResp = objWebReq.GetResponse
        '    objResp.Close()
        '    objWebReq = Nothing
        '    Return True
        'Catch ex As Exception
        '    ' Error, exit and return False 
        '    objWebReq = Nothing
        '    Return False
        'End Try
        Dim Out As Integer
        If InternetGetConnectedState(Out, 0) = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetUniqueID() As Long
        Dim cmd As New SqlCommand
        Dim cnn As New SqlConnection(GetconnectionString())
        Dim sqlparm As SqlParameter = Nothing
        Dim _Id As Long = 0
        Try
            If cnn.State = ConnectionState.Closed Then
                cnn.Open()
            End If
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gSP_GetUniqueID"

            sqlparm = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlparm.Direction = ParameterDirection.Output

            cmd.ExecuteNonQuery()
            _Id = cmd.Parameters("@ID").Value
        Catch ex As Exception
            Throw ex
        Finally
            If Not cmd Is Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not sqlparm Is Nothing Then
                sqlparm = Nothing
            End If
            If Not cnn Is Nothing Then
                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try
        Return _Id
    End Function

    'new method added in NCPDP 10.6 implementation to check the directory download version
    Public Shared Sub checkDownloadVersion()
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim ds As DataSet = Nothing
        Try
            oDB = New gloDatabaseLayer.DBLayer(GetconnectionString())
            oDB.Connect(False)
            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) in ('eRxStagingWebserviceURL','eRxProductionWebserviceURL','eRx10dot6StagingWebserviceURL','eRx10dot6ProductionWebserviceURL') order by sSettingsName", ds)
            oDB.Disconnect()
            If Not IsNothing(ds) Then
                If ds.Tables(0).Rows.Count > 0 Then
                    eRxStagingWebserviceURL = ds.Tables(0).Rows(3)("sSettingsValue").ToString()
                    eRxProductionWebserviceURL = ds.Tables(0).Rows(2)("sSettingsValue").ToString()
                    eRx10dot6StagingWebserviceURL = ds.Tables(0).Rows(1)("sSettingsValue").ToString()
                    eRx10dot6ProductionWebserviceURL = ds.Tables(0).Rows(0)("sSettingsValue").ToString()
                End If
            End If
        Catch ex As Exception
        Finally
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Private Shared Function PostSurescriptMessage(ByVal bytesRead As Byte()) As Byte()

        Dim result As Byte() = Nothing

        Try
            If blnIsStagingServer = True Then
                Dim myService1 As eRxWCFStaging.IeRxClient = Nothing
                myService1 = ServiceClass.GetWCFSvc(eRx10dot6StagingWebserviceURL)
                result = myService1.PostClientMessage(bytesRead, "RxMessage")
                If Not IsNothing(myService1) Then
                    myService1.Close()
                    myService1 = Nothing
                End If
            Else
                Dim myService1 As eRxWCFStaging.IeRxClient = Nothing
                myService1 = ServiceClass.GetWCFSvc(eRx10dot6ProductionWebserviceURL)
                result = myService1.PostClientMessage(bytesRead, "RxMessage")
                If Not IsNothing(myService1) Then
                    myService1.Close()
                    myService1 = Nothing
                End If
            End If

            Return result

        Catch ex As IOException
            _isInternetAvailable = False
            gloSurescriptGeneral.UpdateLog("Error while conversion  - " & ex.ToString)
            MsgBox("Error while conversion  - " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            _isInternetAvailable = False
            If ex.Message.Contains("expected 'https'") Then
                Throw New GloSurescriptException(ex.Message)
            End If
            Return Nothing
        Finally

        End Try

    End Function

    Public Shared Function PostSurescriptMessage(ByVal fileName As String, Optional ByVal objPrescription As EPrescription = Nothing, Optional ByRef _isWithOutInternet As Boolean = False) As Byte()

        Dim oReader As BinaryReader = Nothing
        Dim oFile As FileStream = Nothing

        Try
            If Not File.Exists(fileName) Then
                Return Nothing
            End If

            oFile = New FileStream(fileName, FileMode.Open, FileAccess.Read)
            oReader = New BinaryReader(oFile)
            Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)

            _isInternetAvailable = IsInternetConnectionAvailable()

            If Not _isInternetAvailable AndAlso Not IsNothing(objPrescription) Then
                Using objSureScriptDBLayer As New gloSureScriptDBLayer
                    _isWithOutInternet = objSureScriptDBLayer.InsertintoERXWithoutInternet(Convert.ToBase64String(bytesRead), objPrescription)
                    Return Nothing
                End Using
            Else
                Return PostSurescriptMessage(bytesRead)
            End If

        Catch ex As IOException
            _isInternetAvailable = False
            gloSurescriptGeneral.UpdateLog("Error while conversion  - " & ex.ToString)
            MsgBox("Error while conversion  - " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            _isInternetAvailable = False
            If ex.Message.Contains("expected 'https'") Then
                Throw New GloSurescriptException(ex.Message)
            End If
            Return Nothing
        Finally
            If Not IsNothing(oReader) Then
                oReader.Close()
                oReader.Dispose()
                oReader = Nothing
            End If
            If Not IsNothing(oFile) Then
                oFile.Close()
                oFile.Dispose()
                oFile = Nothing
            End If
        End Try
    End Function

    Public Function ConvertFiletoBinaryEpcs(ByVal strFileName As String, serviceName As String) As Byte()

        Dim sAusID As String = gstrAUSIDClinic
        If File.Exists(strFileName) Then
            Dim oFile As FileStream = Nothing
            Dim oReader As BinaryReader = Nothing
            Dim readbytes As Byte()
            Try
                If Not _isInternetAvailable Then
                    _isInternetAvailable = IsInternetConnectionAvailable()
                End If

                If _isInternetAvailable Then

                    oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read)
                    oReader = New BinaryReader(oFile)
                    Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)
                    If blnIsStagingServer = True Then
                        Dim myService1 As eRxWCFStaging.IeRxClient = Nothing

                        myService1 = ServiceClass.GetWCFSvc(eRx10dot6StagingWebserviceURL)
                        readbytes = myService1.PostEPCSMessage(bytesRead, serviceName, sAusID)
                        If Not IsNothing(myService1) Then
                            myService1.Close()
                            myService1 = Nothing
                        End If
                    Else
                        Dim myService1 As eRxWCFStaging.IeRxClient = Nothing

                        myService1 = ServiceClass.GetWCFSvc(eRx10dot6ProductionWebserviceURL)
                        readbytes = myService1.PostEPCSMessage(bytesRead, serviceName, sAusID)
                        If Not IsNothing(myService1) Then
                            myService1.Close()
                            myService1 = Nothing
                        End If
                    End If
                Else
                    System.Windows.Forms.MessageBox.Show("This eRx will not be sent now as no internet connection is available. It will not be queued, send it again.", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    Return Nothing
                End If
                Return readbytes

            Catch ex As IOException
                MsgBox("Error while conversion  - " & ex.ToString)
                Return Nothing
            Catch ex As Exception
                MsgBox("Error while conversion  - " & ex.ToString)
                Return Nothing
            Finally
                oFile.Close()
                oFile.Dispose()
                oFile = Nothing
                oReader.Close()
                oReader.Dispose()
                oReader = Nothing

            End Try

        Else
            Return Nothing
        End If
    End Function

    Public Function ExtractXMLEpcs(ByVal cntFromDB As Object) As String
        Dim stream As MemoryStream = Nothing
        Dim content As Byte() = Nothing
        Dim oFile As System.IO.FileStream = Nothing
        Try
            If Not IsNothing(cntFromDB) Then
                content = CType(cntFromDB, Byte())
                stream = New MemoryStream(content)
                Dim strfilename As String = gloSettings.FolderSettings.AppTempFolderPath & (gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & ".xml")
                oFile = New System.IO.FileStream(strfilename, System.IO.FileMode.Create)
                'Dim xmlDoc = New XmlDocument()
                'xmlDoc.Load(stream)
                stream.WriteTo(oFile)
                oFile.Close()
                Return strfilename
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
        Finally
            If Not IsNothing(stream) Then
                stream.Dispose()
                stream = Nothing
            End If
            If Not IsNothing(content) Then
                content = Nothing
            End If
            If Not IsNothing(oFile) Then
                oFile.Dispose()
                oFile = Nothing
            End If
        End Try

    End Function
    Public Shared Function UpdatePresMedNDCs(ByVal ndc As String, ByVal PrescriptionID As Long) As Boolean
        Dim _RecordAffected As Integer = 0

        Try
            Using con As New SqlConnection(GetconnectionString())
                con.Open()
                Using cmd As New SqlCommand("gsp_UpdatePresMedNDCs", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add(New SqlParameter("@ndc", ndc))
                    cmd.Parameters.Add(New SqlParameter("@nprescriptionid", PrescriptionID))

                    _RecordAffected = cmd.ExecuteNonQuery()

                    If _RecordAffected >= 1 Then
                        con.Close()
                        Return True
                    Else
                        con.Close()
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception
            Return False
            Throw ex
        End Try
    End Function

    Public Shared Function UpdatePrescriptionStatusForCS(ByVal sEpcsStatus As String, ByVal PrescriptionID As Long) As Boolean
        Dim _RecordAffected As Integer = 0

        Try
            Using con As New SqlConnection(GetconnectionString())
                con.Open()
                Using cmd As New SqlCommand("gsp_UpdatePrescriptionStatusForCS", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add(New SqlParameter("@sEpcsStatus", sEpcsStatus))
                    cmd.Parameters.Add(New SqlParameter("@nprescriptionid", PrescriptionID))

                    _RecordAffected = cmd.ExecuteNonQuery()

                    If _RecordAffected >= 1 Then
                        con.Close()
                        Return True
                    Else
                        con.Close()
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception
            Return False
            Throw ex
        End Try

    End Function
#Region "Functions that are not being used"
    '    ''' <summary>
    '    ''' Function not being used as we are directly invoking GenerateXMLforNewRx
    '    ''' </summary>
    '    ''' <param name="Prescriptiondate"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateNewRx(ByVal Prescriptiondate As DateTime) As EPrescription
    '        Dim objPrescription As EPrescription
    '        Dim objSureScriptDBLayer As gloSureScriptDBLayer
    '        Try
    '            objPrescription = New EPrescription
    '            objPrescription.PrescriptionDate = Prescriptiondate
    '            objSureScriptDBLayer = New gloSureScriptDBLayer
    '            If objSureScriptDBLayer.GetNewPrescription(objPrescription) Then
    '                If objPrescription.DrugsCol.Count > 0 Then
    '                    If GenerateXMLforNewRx(objPrescription, 0) Then
    '                        'Insert the message details into messagetransaction
    '                        objSureScriptDBLayer.InsertintoMessageTransaction(CType(objPrescription.DrugsCol.Item(0), SureScriptMessage))
    '                    End If
    '                End If
    '            End If
    '            Return objPrescription
    '        Catch ex As gloSurescriptDBException

    '        Catch ex As GloSurescriptException

    '        Catch ex As Exception

    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetActivePrescriptions() As EPrescriptions
    '        Dim objSureScriptDBLayer As New gloSureScriptDBLayer
    '        Try
    '            Return objSureScriptDBLayer.GetActivePrescriptions
    '        Catch ex As gloSurescriptDBException
    '            gloSurescriptGeneral.ErrorMessage(ex.Message)
    '        Catch ex As GloSurescriptException
    '            gloSurescriptGeneral.ErrorMessage(ex.Message)

    '        Catch ex As Exception

    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use has been implemented in Webservice
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function InsertRefillPrescription() As Boolean
    '        Dim objPrescription As New EPrescription
    '        Try
    '            If ReadRefillPrescription(objPrescription) Then
    '                Dim objSureScriptDBLayer As New gloSureScriptDBLayer
    '                If objSureScriptDBLayer.InsertRefillPrescription(objPrescription) Then
    '                    'Insert the message info into DB

    '                    objSureScriptDBLayer.InsertintoMessageTransaction(CType(objPrescription.DrugsCol.Item(0), SureScriptMessage))
    '                    Dim oStatus As New StatusMessage
    '                    oStatus.MessageID = 0
    '                    oStatus.RelatesToMessageId = objPrescription.DrugsCol.Item(0).MessageID
    '                    oStatus.MessageFrom = objPrescription.DrugsCol.Item(0).MessageTo
    '                    oStatus.MessageTo = objPrescription.DrugsCol.Item(0).MessageFrom
    '                    oStatus.StatusCode = "000" 'Means the message has been accepted but is yet to be processed/verified
    '                    oStatus.MessageName = "Status"
    '                    oStatus.SMessageType = MessageType.eStatus
    '                    oStatus.DateReceived = Date.Now
    '                    'Generate status message for Refill Request
    '                    GenerateXMLForStatus(oStatus)
    '                    objSureScriptDBLayer.InsertAcknowledgements(oStatus, False)
    '                    'Insert the details of the Status Message sent in the MessageTransaction
    '                    objSureScriptDBLayer.InsertintoMessageTransaction(CType(oStatus, SureScriptMessage))
    '                    Return True
    '                Else
    '                    Return False
    '                End If
    '            Else
    '                Return False
    '            End If

    '        Catch ex As gloSurescriptDBException
    '            gloSurescriptGeneral.ErrorMessage(ex.Message)
    '        Catch ex As GloSurescriptException
    '            gloSurescriptGeneral.ErrorMessage(ex.Message)
    '        Catch ex As Exception

    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use has been implemented in webservice
    '    ''' </summary>
    '    ''' <param name="PrescriberID"></param>
    '    ''' <param name="sMessageName"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function ReadIncomingMessages(ByVal PrescriberID As String, Optional ByVal sMessageName As String = "")
    '        Try
    '            Dim oRxService As New eRxService.eRxMessage
    '            oRxService.Credentials = System.Net.CredentialCache.DefaultCredentials
    '            Dim _Key As String = oRxService.Login("sarika@ophit.net", "spX12ss@!!21nasik")
    '            Dim strfilename As String

    '            If sMessageName = "" Then
    '                strfilename = ExtractXML(oRxService.ReadResponsesonPrescriber(PrescriberID, _Key))
    '            Else
    '                strfilename = ExtractXML(oRxService.ReadResponsesonPrescriberMessages(PrescriberID, sMessageName, _Key))
    '                ReadResponse(strfilename, sMessageName)
    '            End If

    '        Catch ex As Exception

    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not being used
    '    ''' </summary>
    '    ''' <param name="objPrescription"></param>
    '    ''' <param name="icnt"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateXMLforCancelRx(ByVal objPrescription As EPrescription, ByVal icnt As Int16) As Boolean
    '        If ValidateData(objPrescription, icnt) Then

    '            Dim strfilepath As String = GenerateFileName(MessageType.eCancelRx)
    '            Dim xmlwriter As XmlTextWriter = Nothing
    '            XMLFilePath = strfilepath
    '            '--
    '            Try
    '                If File.Exists(strfilepath) Then
    '                    File.Delete(strfilepath)
    '                End If
    '                Dim oDrug As EDrug
    '                oDrug = objPrescription.DrugsCol.Item(icnt)

    '                Dim dtdate As DateTime = Date.UtcNow
    '                Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
    '                Dim strtime As String = Format(dtdate, "hh:mm:ss")

    '                Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

    '                Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
    '                strUTCFormat = strdate & "T" & strtime & ":0Z"

    '                xmlwriter = New XmlTextWriter(strfilepath, Nothing)
    '                oDrug.MessageName = "CancelRx"
    '                oDrug.MessageID = "CancelRx" & strtemp

    '                oDrug.DateTimeStamp = strUTCFormat
    '                oDrug.DateReceived = Now.Date
    '                oDrug.MessageFrom = "mailto:" & objPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
    '                oDrug.MessageTo = "mailto:" & objPrescription.RxPharmacy.PharmacyID & ".ncpdp@surescripts.com"


    '                xmlwriter.WriteStartDocument()
    '                xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 

    '                xmlwriter.WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

    '                xmlwriter.WriteStartElement("Header")

    '                xmlwriter.WriteElementString("To", oDrug.MessageTo) 'The Child 

    '                xmlwriter.WriteElementString("From", oDrug.MessageFrom) 'The Child 

    '                xmlwriter.WriteElementString("MessageID", oDrug.MessageID) 'The Child 

    '                'RelatesToMessageID is not available for this message
    '                xmlwriter.WriteElementString("SentTime", strUTCFormat) 'The Child 
    '                xmlwriter.WriteEndElement() 'End Header Element

    '                xmlwriter.WriteStartElement("Body")
    '                xmlwriter.WriteStartElement("CancelRx")

    '                xmlwriter.WriteElementString("PrescriberOrderNumber", objPrescription.RxPrescriber.PrescriberID)
    '                xmlwriter.WriteStartElement("Pharmacy")

    '                xmlwriter.WriteStartElement("Identification")
    '                xmlwriter.WriteElementString("NCPDPID", objPrescription.RxPharmacy.PharmacyID)
    '                xmlwriter.WriteEndElement() 'End Identification Element
    '                xmlwriter.WriteElementString("StoreName", objPrescription.RxPharmacy.PharmacyName)

    '                If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.City.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.State.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim <> "" Then
    '                    xmlwriter.WriteStartElement("Address")
    '                    If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim <> "" Then
    '                        xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPharmacy.PharmacyAddress.Address1 & " " & objPrescription.RxPharmacy.PharmacyAddress.Address2 & ",")
    '                    End If
    '                    If objPrescription.RxPharmacy.PharmacyAddress.City.Trim <> "" Then
    '                        xmlwriter.WriteElementString("City", objPrescription.RxPharmacy.PharmacyAddress.City)
    '                    End If
    '                    If objPrescription.RxPharmacy.PharmacyAddress.State.Trim <> "" Then
    '                        xmlwriter.WriteElementString("State", objPrescription.RxPharmacy.PharmacyAddress.State)
    '                    End If
    '                    If objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim <> "" Then
    '                        xmlwriter.WriteElementString("ZipCode", objPrescription.RxPharmacy.PharmacyAddress.Zip)
    '                    End If
    '                    xmlwriter.WriteEndElement() 'End Address Element
    '                End If

    '                If GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone).Trim <> "" Then
    '                    xmlwriter.WriteStartElement("PhoneNumbers")
    '                    xmlwriter.WriteStartElement("Phone")
    '                    xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone))
    '                    xmlwriter.WriteElementString("Qualifier", "TE")
    '                    xmlwriter.WriteEndElement() 'End Phone Element
    '                    xmlwriter.WriteEndElement() 'End PhoneNumbers element
    '                End If

    '                xmlwriter.WriteEndElement() 'End Pharmacy Element

    '                xmlwriter.WriteStartElement("Prescriber")
    '                xmlwriter.WriteStartElement("Identification")
    '                xmlwriter.WriteElementString("SPI", objPrescription.RxPrescriber.PrescriberID)
    '                xmlwriter.WriteEndElement() 'End Identification Element
    '                xmlwriter.WriteElementString("ClinicName", "gloClinic")

    '                xmlwriter.WriteStartElement("Name")
    '                xmlwriter.WriteElementString("LastName", objPrescription.RxPrescriber.PrescriberName.LastName)
    '                If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim <> "" Then
    '                    xmlwriter.WriteElementString("FirstName", objPrescription.RxPrescriber.PrescriberName.FirstName)
    '                End If
    '                xmlwriter.WriteEndElement() 'End Name Element

    '                xmlwriter.WriteStartElement("Address")
    '                xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPrescriber.PrescriberAddress.Address1)
    '                xmlwriter.WriteElementString("City", objPrescription.RxPrescriber.PrescriberAddress.City)
    '                xmlwriter.WriteElementString("State", objPrescription.RxPrescriber.PrescriberAddress.State)
    '                xmlwriter.WriteElementString("Zip", objPrescription.RxPrescriber.PrescriberAddress.Zip)
    '                xmlwriter.WriteEndElement() 'End Name Element

    '                xmlwriter.WriteStartElement("PhoneNumbers")
    '                xmlwriter.WriteStartElement("Phone")
    '                xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Phone))
    '                xmlwriter.WriteElementString("Qualifier", "TE")
    '                xmlwriter.WriteEndElement() 'End Phone Element
    '                xmlwriter.WriteEndElement() 'End PhoneNumbers element
    '                xmlwriter.WriteEndElement() 'End Prescriber Element

    '                xmlwriter.WriteStartElement("Patient")
    '                xmlwriter.WriteStartElement("Name")
    '                xmlwriter.WriteElementString("LastName", objPrescription.RxPatient.PatientName.LastName)
    '                xmlwriter.WriteElementString("FirstName", objPrescription.RxPatient.PatientName.FirstName)
    '                xmlwriter.WriteEndElement() 'End Name Element

    '                xmlwriter.WriteElementString("Gender", objPrescription.RxPatient.Gender)
    '                xmlwriter.WriteElementString("DateOfBirth", Getdatetype(objPrescription.RxPatient.DateofBirth))

    '                'xmlwriter.WriteStartElement("Address")
    '                'xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPatient.PatientAddress.Address1)
    '                'xmlwriter.WriteElementString("City", objPrescription.RxPatient.PatientAddress.City)
    '                'xmlwriter.WriteElementString("State", objPrescription.RxPatient.PatientAddress.State)
    '                'xmlwriter.WriteElementString("Zip", objPrescription.RxPatient.PatientAddress.Zip)
    '                'xmlwriter.WriteEndElement() 'End Name Element

    '                If objPrescription.RxPatient.PatientAddress.Address1.Trim <> "" Or objPrescription.RxPatient.PatientAddress.City.Trim <> "" Or objPrescription.RxPatient.PatientAddress.State.Trim <> "" Or objPrescription.RxPatient.PatientAddress.Zip.Trim <> "" Then
    '                    xmlwriter.WriteStartElement("Address")
    '                    If objPrescription.RxPatient.PatientAddress.Address1.Trim <> "" Then
    '                        xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPatient.PatientAddress.Address1 & ",")
    '                    End If
    '                    If objPrescription.RxPatient.PatientAddress.City.Trim <> "" Then
    '                        xmlwriter.WriteElementString("City", objPrescription.RxPatient.PatientAddress.City)
    '                    End If
    '                    If objPrescription.RxPatient.PatientAddress.State.Trim <> "" Then
    '                        xmlwriter.WriteElementString("State", objPrescription.RxPatient.PatientAddress.State)
    '                    End If
    '                    If objPrescription.RxPatient.PatientAddress.Zip.Trim <> "" Then
    '                        xmlwriter.WriteElementString("ZipCode", objPrescription.RxPatient.PatientAddress.Zip)
    '                    End If
    '                    xmlwriter.WriteEndElement() 'End Name Element
    '                End If

    '                If GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone).Trim <> "" Then
    '                    xmlwriter.WriteStartElement("PhoneNumbers")
    '                    xmlwriter.WriteStartElement("Phone")
    '                    xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone))
    '                    xmlwriter.WriteElementString("Qualifier", "TE")
    '                    xmlwriter.WriteEndElement() 'End Phone Element
    '                    xmlwriter.WriteEndElement() 'End PhoneNumbers element

    '                End If
    '                xmlwriter.WriteEndElement() 'End Patient Element

    '                xmlwriter.WriteStartElement("MedicationPrescribed")
    '                xmlwriter.WriteElementString("DrugDescription", objPrescription.DrugsCol.Item(icnt).DrugName & " " & objPrescription.DrugsCol.Item(icnt).Dosage & " " & objPrescription.DrugsCol.Item(icnt).Drugform)
    '                xmlwriter.WriteStartElement("Quantity")
    '                xmlwriter.WriteElementString("Qualifier", objPrescription.DrugsCol.Item(icnt).DrugQuantityQualifier)
    '                xmlwriter.WriteElementString("Value", objPrescription.DrugsCol.Item(icnt).DrugQuantity)
    '                xmlwriter.WriteEndElement() 'End Quantity Element

    '                xmlwriter.WriteElementString("Directions", objPrescription.DrugsCol.Item(icnt).Directions)

    '                xmlwriter.WriteStartElement("Refills")
    '                xmlwriter.WriteElementString("Qualifier", "R")
    '                If objPrescription.DrugsCol.Item(icnt).RefillQuantity.Trim <> "" Then
    '                    xmlwriter.WriteElementString("Quantity", objPrescription.DrugsCol.Item(icnt).RefillQuantity)
    '                End If
    '                xmlwriter.WriteEndElement() 'End Refills Element
    '                If objPrescription.DrugsCol.Item(icnt).MaySubstitute Then
    '                    xmlwriter.WriteElementString("Substitutions", 1)
    '                Else
    '                    xmlwriter.WriteElementString("Substitutions", 0)
    '                End If
    '                xmlwriter.WriteElementString("WrittenDate", Getdatetype(objPrescription.DrugsCol.Item(icnt).WrittenDate))
    '                xmlwriter.WriteEndElement() 'End MedicationPrescribed Element


    '                xmlwriter.WriteEndElement() 'End NewRx element
    '                xmlwriter.WriteEndElement() 'End Body Element
    '                xmlwriter.WriteEndElement() 'End Message Element
    '                xmlwriter.WriteEndDocument()
    '                xmlwriter.Close()
    '                gloSurescriptGeneral.ConvertFiletoBinary(strfilepath)
    '                Return True
    '            Catch ex As gloSurescriptDBException

    '            Catch ex As Exception

    '            End Try


    '        End If

    '    End Function
    '    ''' <summary>
    '    ''' Function not being used as we need permission to
    '    ''' implement Verify message
    '    ''' </summary>
    '    ''' <param name="oStatus"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateXMLForVerify(ByVal oStatus As StatusMessage) As Boolean
    '        Try
    '            Dim strfilepath As String = GenerateFileName(MessageType.eVerify)
    '            If File.Exists(strfilepath) Then
    '                File.Delete(strfilepath)
    '            End If

    '            Dim xmlwriter As XmlTextWriter = Nothing
    '            xmlwriter = New XmlTextWriter(strfilepath, Nothing)

    '            Dim dtdate As DateTime = Date.UtcNow
    '            Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
    '            Dim strtime As String = Format(dtdate, "hh:mm:ss")

    '            Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
    '            Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
    '            strUTCFormat = strdate & "T" & strtime & ":0Z"

    '            oStatus.MessageID = "Verify" & strtemp
    '            oStatus.DateTimeStamp = strUTCFormat
    '            oStatus.DateReceived = Now

    '            xmlwriter.WriteStartDocument()
    '            xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 

    '            xmlwriter.WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

    '            xmlwriter.WriteStartElement("Header")

    '            xmlwriter.WriteElementString("To", oStatus.MessageTo) 'The Child 

    '            xmlwriter.WriteElementString("From", oStatus.MessageFrom) 'The Child 

    '            xmlwriter.WriteElementString("MessageID", oStatus.MessageID) 'The Child 

    '            xmlwriter.WriteElementString("RelatesToMessageID", oStatus.RelatesToMessageId) 'The Child 
    '            xmlwriter.WriteElementString("SentTime", oStatus.DateTimeStamp) 'The Child 
    '            xmlwriter.WriteEndElement() 'End Header Element

    '            xmlwriter.WriteStartElement("Body")
    '            xmlwriter.WriteStartElement("Verify")
    '            xmlwriter.WriteElementString("Code", "010") 'Indicates that message has been verified successfully
    '            xmlwriter.WriteEndElement() 'End Verify element

    '            xmlwriter.WriteEndElement()  'End Body element
    '            xmlwriter.WriteEndElement()  'End Message Element
    '            xmlwriter.WriteEndDocument() 'End Document
    '            xmlwriter.Close()            'Close xml file
    '            gloSurescriptGeneral.ConvertFiletoBinary(strfilepath)
    '            Return True
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not being used as status message shall be generated in  webservice
    '    ''' </summary>
    '    ''' <param name="oStatus"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateXMLForStatus(ByVal oStatus As StatusMessage) As Boolean
    '        Try
    '            Dim strfilepath As String = GenerateFileName(MessageType.eStatus)
    '            If File.Exists(strfilepath) Then
    '                File.Delete(strfilepath)
    '            End If
    '            Dim xmlwriter As XmlTextWriter = Nothing
    '            xmlwriter = New XmlTextWriter(strfilepath, Nothing)

    '            Dim dtdate As DateTime = Date.UtcNow
    '            Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
    '            Dim strtime As String = Format(dtdate, "hh:mm:ss")

    '            Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

    '            Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
    '            strUTCFormat = strdate & "T" & strtime & ":0Z"


    '            oStatus.DateTimeStamp = strUTCFormat
    '            oStatus.DateReceived = Now

    '            xmlwriter.WriteStartDocument()
    '            xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 

    '            xmlwriter.WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

    '            xmlwriter.WriteStartElement("Header")

    '            xmlwriter.WriteElementString("To", oStatus.MessageTo) 'The Child 

    '            xmlwriter.WriteElementString("From", oStatus.MessageFrom) 'The Child 

    '            xmlwriter.WriteElementString("MessageID", oStatus.MessageID) 'The Child 

    '            xmlwriter.WriteElementString("RelatesToMessageID", oStatus.RelatesToMessageId) 'The Child 
    '            xmlwriter.WriteElementString("SentTime", oStatus.DateTimeStamp) 'The Child 
    '            xmlwriter.WriteEndElement() 'End Header Element

    '            xmlwriter.WriteStartElement("Body")

    '            If oStatus.StatusCode = "000" Then
    '                xmlwriter.WriteStartElement("Status")
    '            ElseIf oStatus.StatusCode = "010" Then
    '                xmlwriter.WriteStartElement("Verify")
    '            End If

    '            xmlwriter.WriteElementString("Code", oStatus.StatusCode)
    '            xmlwriter.WriteEndElement() 'End Status element

    '            xmlwriter.WriteEndElement()  'End Body element
    '            xmlwriter.WriteEndElement()  'End Message Element
    '            xmlwriter.WriteEndDocument() 'End Document
    '            xmlwriter.Close()            'Close xml file
    '            gloSurescriptGeneral.ConvertFiletoBinary(strfilepath)
    '            Return True
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' function not used as XML for Error is generated
    '    ''' in webservice
    '    ''' </summary>
    '    ''' <param name="oError"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateErrorMessage(ByVal oError As SureScriptErrorMessage) As Boolean
    '        Try
    '            If GenerateXMLForError(oError) Then
    '                Dim oSurescriptDBLayer As New gloSureScriptDBLayer
    '                If oSurescriptDBLayer.InsertErrorDetails(oError) Then
    '                    oSurescriptDBLayer.InsertintoMessageTransaction(CType(oError, SureScriptMessage))
    '                    'RaiseEvent ErrorGenerated()

    '                End If
    '            End If
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not used as this functionality has been implemented in Webservice
    '    ''' </summary>
    '    ''' <param name="oError"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateXMLForError(ByVal oError As SureScriptErrorMessage) As Boolean
    '        Try
    '            Dim strfilepath As String = GenerateFileName(MessageType.eError)
    '            If File.Exists(strfilepath) Then
    '                File.Delete(strfilepath)
    '            End If
    '            Dim xmlwriter As XmlTextWriter = Nothing
    '            xmlwriter = New XmlTextWriter(strfilepath, Nothing)
    '            XMLFilePath = strfilepath

    '            Dim dtdate As DateTime = Date.UtcNow
    '            Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
    '            Dim strtime As String = Format(dtdate, "hh:mm:ss")

    '            Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

    '            Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
    '            strUTCFormat = strdate & "T" & strtime & ":0Z"

    '            oError.DateTimeStamp = strUTCFormat
    '            oError.DateReceived = Now

    '            oError.MessageID = "Error" & strtemp

    '            oError.MessageName = "Error" & strtemp
    '            xmlwriter.WriteStartDocument()
    '            xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 

    '            xmlwriter.WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

    '            xmlwriter.WriteStartElement("Header")

    '            xmlwriter.WriteElementString("To", oError.MessageTo) 'The Child 
    '            xmlwriter.WriteElementString("From", oError.MessageFrom) 'The Child 
    '            xmlwriter.WriteElementString("MessageID", oError.MessageID) 'The Child 

    '            xmlwriter.WriteElementString("RelatesToMessageID", oError.RelatesToMessageId) 'The Child 
    '            xmlwriter.WriteElementString("SentTime", oError.DateTimeStamp) 'The Child 
    '            xmlwriter.WriteEndElement() 'End Header Element

    '            xmlwriter.WriteStartElement("Body")
    '            xmlwriter.WriteStartElement("Error")
    '            xmlwriter.WriteElementString("Code", oError.ErrorCode)
    '            xmlwriter.WriteElementString("DescriptionCode", oError.DescriptionCode)
    '            xmlwriter.WriteElementString("Description", oError.Description)

    '            xmlwriter.WriteEndElement() 'End Error element
    '            xmlwriter.WriteEndElement()  'End Body element
    '            xmlwriter.WriteEndElement()  'End Message Element
    '            xmlwriter.WriteEndDocument() 'End Document
    '            xmlwriter.Close()            'Close xml file
    '            gloSurescriptGeneral.ConvertFiletoBinary(strfilepath)
    '            Return True
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not being used as we are currently not implementing CancelRx
    '    ''' </summary>
    '    ''' <param name="objPrescription"></param>
    '    ''' <param name="icnt"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateCancelRx(ByVal objPrescription As EPrescription, ByVal icnt As Int16) As Boolean
    '        Dim objSureScriptDBLayer As gloSureScriptDBLayer

    '        Try
    '            objSureScriptDBLayer = New gloSureScriptDBLayer
    '            If GenerateXMLforCancelRx(objPrescription, icnt) Then
    '                'Insert the message details into messagetransaction
    '                objSureScriptDBLayer.InsertintoMessageTransaction(CType(objPrescription.DrugsCol.Item(icnt), SureScriptMessage))
    '            End If
    '        Catch ex As gloSurescriptDBException

    '        Catch ex As GloSurescriptException

    '        Catch ex As Exception

    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use 
    '    ''' </summary>
    '    ''' <param name="strcode"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function GetSubstitutionCode(ByVal strcode As String) As Boolean
    '        Select Case strcode

    '            'Substitutions are allowed
    '            Case "0", "2", "3", "4", "5", "8"
    '                Return False
    '                'Substitutions not allowed
    '            Case "1", "7"
    '                Return True
    '        End Select
    '    End Function
    '    ''' <summary>
    '    ''' function not being used as we are not implementing cancel drug message
    '    ''' </summary>
    '    ''' <param name="RxTransactionID"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetDrugsToCancel(ByVal RxTransactionID As String) As EPrescription
    '        Dim objPrescription As EPrescription
    '        Dim objSureScriptDBLayer As gloSureScriptDBLayer
    '        Try
    '            objPrescription = New EPrescription
    '            objPrescription.RxTransactionID = CType(RxTransactionID, Int64)

    '            objSureScriptDBLayer = New gloSureScriptDBLayer
    '            If objSureScriptDBLayer.GetNewPrescription(objPrescription, True) Then
    '                If objPrescription.DrugsCol.Count > 0 Then
    '                    Return objPrescription
    '                Else
    '                    Return Nothing
    '                End If
    '            Else
    '                Return Nothing
    '            End If

    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)

    '        Finally
    '            If Not IsNothing(objSureScriptDBLayer) Then
    '                objSureScriptDBLayer.Dispose()
    '                objSureScriptDBLayer = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' 
    '    ''' Function not is use presently used in webservice
    '    ''' </summary>
    '    ''' <param name="objPrescription"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function ReadRefillPrescription(ByVal objPrescription As EPrescription) As Boolean
    '        '    Dim areader As XmlReader
    '        '    Dim strfilepath As String = gloSurescriptGeneral.RootPath & "\RefillRequest.xml"
    '        '    Try
    '        '        areader = XmlReader.Create(strfilepath)
    '        '        Dim oDrug As Drug
    '        '        oDrug = New Drug

    '        '        While areader.Read

    '        '            Dim blnIsRefillsQualifier As Boolean = False

    '        '            If areader.NodeType = XmlNodeType.Element Then
    '        '                Select Case areader.Name

    '        '                    'this is the message id for the Refillrequest that shall go in the MessageId field
    '        '                    Case "From"
    '        '                        oDrug.MessageFrom = areader.ReadInnerXml
    '        '                    Case "To"
    '        '                        oDrug.MessageTo = areader.ReadInnerXml
    '        '                    Case "MessageID"
    '        '                        oDrug.MessageID = areader.ReadInnerXml
    '        '                    Case "RelatesToMessageID"
    '        '                        oDrug.RelatesToMessageId = areader.ReadInnerXml
    '        '                    Case "SentTime"
    '        '                        oDrug.DateTimeStamp = areader.ReadInnerXml
    '        '                        oDrug.DateReceived = Date.Now
    '        '                    Case "RxReferenceNumber"
    '        '                        oDrug.RxReferenceNumber = areader.ReadInnerXml
    '        '                        objPrescription.RxReferenceNumber = oDrug.RxReferenceNumber
    '        '                        oDrug.TransactionID = oDrug.RxReferenceNumber
    '        '                    Case "PrescriberOrderNumber"
    '        '                        objPrescription.RxTransactionID = areader.ReadInnerXml
    '        '                    Case "NCPDPID"
    '        '                        objPrescription.RxPharmacy.PharmacyID = areader.ReadInnerXml
    '        '                    Case "StoreName"
    '        '                        objPrescription.RxPharmacy.PharmacyName = areader.ReadInnerXml
    '        '                    Case "SPI"
    '        '                        objPrescription.RxPrescriber.PrescriberID = areader.ReadInnerXml
    '        '                    Case "DrugDescription"
    '        '                        oDrug.DrugName = areader.ReadInnerXml
    '        '                    Case "Strength"
    '        '                        oDrug.DrugStrength = areader.ReadInnerXml
    '        '                    Case "StrengthUnits"

    '        '                    Case "Refills"
    '        '                        blnIsRefillsQualifier = True
    '        '                    Case "Value"
    '        '                        oDrug.DrugQuantity = areader.ReadInnerXml
    '        '                    Case "Quantity"
    '        '                        oDrug.RefillQuantity = areader.ReadInnerXml
    '        '                    Case "Qualifier"
    '        '                        If blnIsRefillsQualifier = False Then
    '        '                            oDrug.DrugQuantityQualifier = areader.ReadInnerXml
    '        '                        Else
    '        '                            oDrug.RefillsQualifier = areader.ReadInnerXml
    '        '                        End If
    '        '                    Case "Substitutions"
    '        '                        oDrug.MaySubstitute = GetSubstitutionCode(areader.ReadInnerXml)
    '        '                    Case "WrittenDate"
    '        '                        oDrug.WrittenDate = areader.ReadInnerXml
    '        '                End Select

    '        '            End If
    '        '        End While
    '        '        oDrug.MessageName = "RefillRequest"

    '        '        'Validate the RefillRequest message
    '        '        Dim oError As New SureScriptErrorMessage
    '        '        oError.MessageFrom = oDrug.MessageTo
    '        '        oError.MessageTo = oDrug.MessageFrom
    '        '        oError.RelatesToMessageId = oDrug.MessageID

    '        '        If ValidateMessageHeader(CType(oDrug, SureScriptMessage), oError, True) Then
    '        '            If ValidateRefillRequest(objPrescription, 0, oError) Then
    '        '                If Not IsNothing(oDrug) Then
    '        '                    objPrescription.DrugsCol.Add(oDrug)
    '        '                End If
    '        '                Return True
    '        '            Else
    '        '                'generate error message and reject the refill request
    '        '                GenerateErrorMessage(oError)
    '        '                Return False
    '        '            End If
    '        '        Else
    '        '            GenerateErrorMessage(oError)
    '        '            Return False
    '        '        End If

    '        '    Catch ex As gloSurescriptDBException
    '        '        Throw ex
    '        '    Catch ex As GloSurescriptException
    '        '        Throw ex
    '        '    Catch ex As Exception
    '        '        Throw New GloSurescriptException(ex.Message)
    '        '    End Try




    '        Dim ds As New DataSet
    '        Dim oDrug As EDrug

    '        Try
    '            ds.ReadXml(XMLFilePath, XmlReadMode.InferSchema)
    '            If ds.Tables.Count > 0 Then
    '                For icnt As Int32 = 0 To ds.Tables(0).Rows.Count - 1
    '                    oDrug = New EDrug

    '                    oDrug.MessageFrom = ds.Tables(0).Rows(icnt)("sMessageFrom")

    '                    oDrug.MessageTo = "mailto:" & ds.Tables(0).Rows(icnt)("sMessageTo") & ":spi@surescripts.com"

    '                    oDrug.MessageID = ds.Tables(0).Rows(icnt)("nMsgID")

    '                    oDrug.RelatesToMessageId = ds.Tables(0).Rows(icnt)("sMessageID")

    '                    oDrug.DateTimeStamp = ds.Tables(0).Rows(icnt)("dtSentTime")
    '                    oDrug.DateReceived = Date.Now

    '                    oDrug.RxReferenceNumber = ds.Tables(0).Rows(icnt)("sRxReference")
    '                    objPrescription.RxReferenceNumber = oDrug.RxReferenceNumber
    '                    oDrug.TransactionID = oDrug.RxReferenceNumber

    '                    objPrescription.RxTransactionID = ds.Tables(0).Rows(icnt)("sPrescriberOrder")

    '                    objPrescription.RxPrescriber.PrescriberName.LastName = ds.Tables(0).Rows(icnt)("sPrescriberLastName")
    '                    objPrescription.RxPharmacy.PharmacyID = ds.Tables(0).Rows(icnt)("sPharmacyID")

    '                    objPrescription.RxPharmacy.PharmacyName = ds.Tables(0).Rows(icnt)("sPharmacyName")
    '                    objPrescription.RxPharmacy.PharmacyAddress.Address1 = ds.Tables(0).Rows(icnt)("sPharmacyAddress")
    '                    objPrescription.RxPharmacy.PharmacyAddress.City = ds.Tables(0).Rows(icnt)("sPharmacyCity")
    '                    objPrescription.RxPharmacy.PharmacyAddress.State = ds.Tables(0).Rows(icnt)("sPharmacyState")
    '                    objPrescription.RxPharmacy.PharmacyAddress.Zip = ds.Tables(0).Rows(icnt)("sPharmacyZipCode")

    '                    objPrescription.RxPrescriber.PrescriberID = ds.Tables(0).Rows(icnt)("sSPI")

    '                    If (ds.Tables(0).Columns.Contains("sDrugName")) = True Then
    '                        oDrug.DrugName = ds.Tables(0).Rows(icnt)("sDrugName")

    '                    End If
    '                    If (ds.Tables(0).Columns.Contains("sDrugStrength")) = True Then
    '                        oDrug.DrugStrength = ds.Tables(0).Rows(icnt)("sDrugStrength")
    '                        oDrug.Dosage = oDrug.DrugStrength
    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("sDrugQuantity")) = True Then
    '                        oDrug.DrugQuantity = ds.Tables(0).Rows(icnt)("sDrugQuantity")
    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("sRefillQuantity")) = True Then
    '                        oDrug.RefillQuantity = ds.Tables(0).Rows(icnt)("sRefillQuantity")

    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("sDrugStrengthUnits")) = True Then
    '                        oDrug.DrugQuantityQualifier = ds.Tables(0).Rows(icnt)("sDrugStrengthUnits")
    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("sRefillsQualifier")) = True Then
    '                        oDrug.RefillsQualifier = ds.Tables(0).Rows(icnt)("sRefillsQualifier")
    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("bIsSubstituitons")) = True Then
    '                        oDrug.MaySubstitute = GetSubstitutionCode(ds.Tables(0).Rows(icnt)("bIsSubstituitons"))
    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("dtWrittenDate")) = True Then
    '                        oDrug.WrittenDate = ds.Tables(0).Rows(icnt)("dtWrittenDate")
    '                    End If

    '                    oDrug.MessageName = "RefillRequest"
    '                    objPrescription.DrugsCol.Add(oDrug)
    '                    'Validate the RefillRequest message
    '                    Dim oError As New SureScriptErrorMessage
    '                    oError.MessageFrom = oDrug.MessageTo
    '                    oError.MessageTo = oDrug.MessageFrom
    '                    oError.RelatesToMessageId = oDrug.MessageID

    '                    If ValidateMessageHeader(CType(oDrug, SureScriptMessage), oError, True) Then
    '                        If ValidateRefillRequest(objPrescription, 0, oError) Then
    '                            If Not IsNothing(oDrug) Then
    '                                objPrescription.DrugsCol.Add(oDrug)
    '                            End If
    '                            Return True
    '                        Else
    '                            'generate error message and reject the refill request
    '                            GenerateErrorMessage(oError)
    '                            Return False
    '                        End If
    '                    Else
    '                        GenerateErrorMessage(oError)
    '                        Return False
    '                    End If
    '                Next
    '            End If

    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use as we are implementing this functionality of
    '    ''' reading FreeStanding Error in webservice
    '    ''' </summary>
    '    ''' <param name="objErrorMessage"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function ReadFreeStandingError(ByVal objErrorMessage As SureScriptErrorMessage) As Boolean
    '        'Dim areader As XmlReader
    '        'Dim strfilepath As String = gloSurescriptGeneral.RootPath & "\SurescriptFreeStandingError.xml"
    '        'Try
    '        '    areader = XmlReader.Create(strfilepath)
    '        '    While areader.Read

    '        '        Dim blnIsRefillsQualifier As Boolean = False

    '        '        If areader.NodeType = XmlNodeType.Element Then
    '        '            Select Case areader.Name

    '        '                'this is the message id for the Refillrequest that shall go in the MessageId field
    '        '                Case "From"
    '        '                    objErrorMessage.MessageFrom = areader.ReadInnerXml
    '        '                Case "To"
    '        '                    objErrorMessage.MessageTo = areader.ReadInnerXml
    '        '                Case "MessageID"
    '        '                    objErrorMessage.MessageID = areader.ReadInnerXml
    '        '                Case "RelatesToMessageID"
    '        '                    objErrorMessage.RelatesToMessageId = areader.ReadInnerXml
    '        '                Case "SentTime"
    '        '                    objErrorMessage.DateTimeStamp = areader.ReadInnerXml
    '        '                    objErrorMessage.DateReceived = Date.Now
    '        '                Case "Code"
    '        '                    objErrorMessage.ErrorCode = areader.ReadInnerXml
    '        '                Case "DescriptionCode"
    '        '                    objErrorMessage.DescriptionCode = areader.ReadInnerXml
    '        '                Case "Description"
    '        '                    objErrorMessage.Description = areader.ReadInnerXml
    '        '            End Select

    '        '        End If
    '        '    End While
    '        '    objErrorMessage.MessageName = "Error"

    '        '    Dim oError As New SureScriptErrorMessage

    '        '    Dim objSureScriptDBLayer As New gloSureScriptDBLayer
    '        '    If objSureScriptDBLayer.InsertErrorDetails(objErrorMessage) Then
    '        '        objSureScriptDBLayer.InsertintoMessageTransaction(CType(objErrorMessage, SureScriptMessage))
    '        '    End If

    '        '    Return True
    '        'Catch ex As gloSurescriptDBException
    '        '    Throw ex
    '        'Catch ex As GloSurescriptException
    '        '    Throw ex
    '        'Catch ex As Exception
    '        '    Throw New GloSurescriptException(ex.Message)
    '        'End Try
    '        Dim ds As New DataSet
    '        Dim oDrug As EDrug

    '        Try
    '            ds.ReadXml(XMLFilePath, XmlReadMode.InferSchema)
    '            If ds.Tables.Count > 0 Then
    '                For icnt As Int32 = 0 To ds.Tables(0).Rows.Count - 1


    '                    objErrorMessage.MessageFrom = ds.Tables(0).Rows(icnt)("sMessageFrom")

    '                    objErrorMessage.MessageTo = ds.Tables(0).Rows(icnt)("sMessageTo")

    '                    objErrorMessage.MessageID = ds.Tables(0).Rows(icnt)("sMessageID")

    '                    objErrorMessage.RelatesToMessageId = ds.Tables(0).Rows(icnt)("sRelativeMessageID")

    '                    objErrorMessage.DateTimeStamp = ds.Tables(0).Rows(icnt)("dtSentTime")

    '                    objErrorMessage.DateReceived = Date.Now
    '                    If (ds.Tables(0).Columns.Contains("sStatusCode")) = True Then
    '                        objErrorMessage.ErrorCode = ds.Tables(0).Rows(icnt)("sStatusCode")

    '                    End If
    '                    If (ds.Tables(0).Columns.Contains("sResponseCode")) = True Then
    '                        objErrorMessage.DescriptionCode = ds.Tables(0).Rows(icnt)("sResponseCode")

    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("sResponseDescription")) = True Then
    '                        objErrorMessage.Description = ds.Tables(0).Rows(icnt)("sResponseDescription")
    '                    End If


    '                    objErrorMessage.MessageName = "Error"

    '                    Dim oError As New SureScriptErrorMessage

    '                    Dim objSureScriptDBLayer As New gloSureScriptDBLayer
    '                    If objSureScriptDBLayer.InsertErrorDetails(objErrorMessage) Then
    '                        objSureScriptDBLayer.InsertintoMessageTransaction(CType(objErrorMessage, SureScriptMessage))
    '                    End If

    '                Next
    '                Return True
    '            End If
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use currently as we have not implemented the 
    '    ''' Cancel Rx functionality
    '    ''' </summary>
    '    ''' <param name="objMessage"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function ReadCancelRxResponse(ByVal objMessage As SureScriptResponseMessage) As Boolean
    '        Dim areader As XmlReader
    '        Dim strfilepath As String = gloSurescriptGeneral.RootPath & "\CancelRxResponseDenied.xml"
    '        Try
    '            areader = XmlReader.Create(strfilepath)
    '            While areader.Read

    '                Dim blnIsRefillsQualifier As Boolean = False

    '                If areader.NodeType = XmlNodeType.Element Then
    '                    Select Case areader.Name

    '                        'this is the message id for the Refillrequest that shall go in the MessageId field
    '                        Case "From"
    '                            objMessage.MessageFrom = areader.ReadInnerXml
    '                        Case "To"
    '                            objMessage.MessageTo = areader.ReadInnerXml
    '                        Case "MessageID"
    '                            objMessage.MessageID = areader.ReadInnerXml
    '                        Case "RelatesToMessageID"
    '                            objMessage.RelatesToMessageId = areader.ReadInnerXml
    '                        Case "SentTime"
    '                            objMessage.DateTimeStamp = areader.ReadInnerXml
    '                            objMessage.DateReceived = Date.Now
    '                        Case "Approved"
    '                            objMessage.ApprovalStatus = True
    '                        Case "Denied"
    '                            objMessage.ApprovalStatus = False
    '                        Case "DescriptionCode"
    '                            objMessage.Denialcode = areader.ReadInnerXml
    '                        Case "Description"
    '                            objMessage.DenialReason = areader.ReadInnerXml
    '                    End Select

    '                End If
    '            End While
    '            objMessage.MessageName = "CancelRxReponse"
    '            Dim oError As New SureScriptErrorMessage
    '            oError.MessageFrom = objMessage.MessageTo
    '            oError.MessageTo = objMessage.MessageFrom
    '            oError.RelatesToMessageId = objMessage.MessageID

    '            'Check if incoming message is valid or not
    '            If ValidateMessageHeader(CType(objMessage, SureScriptMessage), oError) Then

    '                Dim objSureScriptDBLayer As New gloSureScriptDBLayer
    '                If objSureScriptDBLayer.CallCancelDrug(objMessage) Then
    '                    objSureScriptDBLayer.InsertintoMessageTransaction(CType(objMessage, SureScriptMessage))
    '                End If
    '                objSureScriptDBLayer.InsertResponseTransaction(objMessage, True)
    '                Dim oStatus As New StatusMessage
    '                oStatus.MessageID = 0
    '                oStatus.RelatesToMessageId = objMessage.MessageID
    '                oStatus.MessageFrom = objMessage.MessageTo
    '                oStatus.MessageTo = objMessage.MessageFrom
    '                oStatus.StatusCode = "010" 'Means the message has been accepted but is yet to be processed/verified
    '                oStatus.MessageName = "Status"
    '                oStatus.SMessageType = MessageType.eStatus
    '                oStatus.DateReceived = Date.Now
    '                'Generate status message for Refill Request
    '                GenerateXMLForStatus(oStatus)
    '                objSureScriptDBLayer.InsertAcknowledgements(oStatus, False)

    '                'Insert the details of the Status Message sent in the MessageTransaction
    '                objSureScriptDBLayer.InsertintoMessageTransaction(CType(oStatus, SureScriptMessage))

    '                'generate an error message if it is not a valid message
    '            Else

    '                GenerateErrorMessage(oError)
    '            End If
    '            Return True
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
#End Region

#Region "Functions that are not being used in gloSureScriptDBLayer"
    '#Region "Functions not in use"

    '    ''' <summary>
    '    ''' Function not in use
    '    ''' </summary>
    '    ''' <param name="PatientId"></param>
    '    ''' <param name="Contactid"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function UpdatePatient(ByVal PatientId As Int64, ByVal Contactid As Int64) As Boolean
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        conn.Open()

    '        Try

    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            _strsql = "Update Patient set nPharmacyId=" & Contactid & " where npatientid=" & PatientId & ""
    '            sql.CommandText = _strsql
    '            sql.ExecuteNonQuery()

    '            Return True

    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return False
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use as Cancel Drug  not implemented
    '    ''' </summary>
    '    ''' <param name="objMessage"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function CallCancelDrug(ByVal objMessage As SureScriptResponseMessage) As Boolean
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)

    '        conn.Open()
    '        Dim sql As SqlCommand
    '        Dim _strsql As String

    '        Try
    '            sql = New SqlCommand
    '            sql.CommandType = CommandType.Text
    '            '_strsql = "select isnull(p.sfirstname,'') as PatientFName,isnull(p.slastname,'') as PatientLastName,isnull(p.sGender,'') as Gender,p.dtdob as DOB,isnull(p.sAddressLine1,'') as AddressLine1,isnull(p.sAddressLine2,'') as AddressLine2,isnull(p.sCity,'') as PatientCity,isnull(p.sState,'') as PatientState,isnull(p.sZip,'') as PatientZip,isnull(p.sPhone,'') as PatientPhone,isnull(pr.sFirstname,'') as ProviderFirstName ,isnull(pr.sLastName,'') as ProviderLastName,isnull(pr.sAddress,'') as Provideraddress1,isnull(pr.sStreet,'') as ProviderStreet,isnull(pr.sCity,'') as ProviderCity,isnull(pr.sState,'') as ProviderState,isnull(pr.sZip,'') as ProviderZip,isnull(pr.sphoneNo,'') as ProviderPhone,isnull(pr.sexternalcode,'') as ProviderCode from Patient p inner join PrescriptionTransaction pt on p.npatientid=pt.npatientid inner join Visits v on pt.nvisitid=v.nvisitid inner join Provider_Mst pr on v.nproviderid=pr.nproviderid where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
    '            _strsql = "select distinct s.sReferenceNumber from SureScriptMessageTransaction s where s.nMessageID= '" & objMessage.RelatesToMessageId & "'"
    '            sql.CommandText = _strsql
    '            sql.Connection = conn
    '            Dim ID As String = sql.ExecuteScalar
    '            If ID <> "" Then
    '                If CancelDrugs(ID, objMessage.ApprovalStatus) Then
    '                    Return True
    '                Else
    '                    Return False
    '                End If
    '            Else
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return False
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use as we are not implementing Cancel Rx
    '    ''' </summary>
    '    ''' <param name="ID"></param>
    '    ''' <param name="blnIsApproved"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function CancelDrugs(ByVal ID As String, ByVal blnIsApproved As Boolean) As Boolean
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        conn.Open()
    '        Try

    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            'The cancel request has been approved
    '            If blnIsApproved Then
    '                _strsql = "Update PrescriptionTransactionDetails set sstatus='Cancelled' where nPrescriptionID = " & ID & ""
    '                'The cancel request has not been approved
    '            Else
    '                _strsql = "Update PrescriptionTransactionDetails set sstatus='' where nPrescriptionID = " & ID & ""

    '            End If
    '            sql.CommandText = _strsql
    '            sql.ExecuteNonQuery()
    '            Return True

    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return False
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use in gloEMR
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetActivePrescriptions() As EPrescriptions
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sqladpt As New SqlDataAdapter
    '        Dim dt As New DataTable
    '        Dim objPrescription As EPrescription
    '        Dim PrescriptionCol As New EPrescriptions
    '        conn.Open()
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        Try
    '            sql = New SqlCommand
    '            sql.CommandType = CommandType.Text
    '            '_strsql = "select isnull(p.sfirstname,'') as PatientFName,isnull(p.slastname,'') as PatientLastName,isnull(p.sGender,'') as Gender,p.dtdob as DOB,isnull(p.sAddressLine1,'') as AddressLine1,isnull(p.sAddressLine2,'') as AddressLine2,isnull(p.sCity,'') as PatientCity,isnull(p.sState,'') as PatientState,isnull(p.sZip,'') as PatientZip,isnull(p.sPhone,'') as PatientPhone,isnull(pr.sFirstname,'') as ProviderFirstName ,isnull(pr.sLastName,'') as ProviderLastName,isnull(pr.sAddress,'') as Provideraddress1,isnull(pr.sStreet,'') as ProviderStreet,isnull(pr.sCity,'') as ProviderCity,isnull(pr.sState,'') as ProviderState,isnull(pr.sZip,'') as ProviderZip,isnull(pr.sphoneNo,'') as ProviderPhone,isnull(pr.sexternalcode,'') as ProviderCode from Patient p inner join PrescriptionTransaction pt on p.npatientid=pt.npatientid inner join Visits v on pt.nvisitid=v.nvisitid inner join Provider_Mst pr on v.nproviderid=pr.nproviderid where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
    '            _strsql = "select pt.nRxTransactionID,pt.dtPrescriptiondate from PrescriptionTransaction pt inner join Prescriptiontransactiondetails ptd on pt.nRxTransactionID=ptd.nRxTransactionID where ptd.sStatus <> 'Cancelled'"

    '            sql.CommandText = _strsql
    '            sql.Connection = conn

    '            sqladpt.SelectCommand = sql
    '            sqladpt.Fill(dt)
    '            If dt.Rows.Count > 0 Then

    '                For icnt As Int32 = 0 To dt.Rows.Count - 1
    '                    objPrescription = New EPrescription
    '                    objPrescription.RxTransactionID = dt.Rows(icnt)(0)
    '                    objPrescription.PrescriptionDate = dt.Rows(icnt)(1)
    '                    PrescriptionCol.Add(objPrescription)
    '                Next
    '            End If
    '            dt.Dispose()
    '            dt = Nothing

    '            sql.Dispose()
    '            sql = Nothing

    '            sqladpt.Dispose()
    '            sqladpt = Nothing
    '            Return PrescriptionCol
    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return Nothing
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not is use
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetDrugList() As DataTable
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        Dim sqladpt As New SqlDataAdapter
    '        Dim dt As New DataTable
    '        Try
    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            _strsql = "select Top 200 nDrugsId,sDrugName,sDosage from Drugs_Mst "
    '            sql.CommandText = _strsql
    '            conn.Open()


    '            sqladpt.SelectCommand = sql
    '            sqladpt.Fill(dt)

    '            If dt.Rows.Count > 0 Then
    '                Return dt
    '            Else
    '                Return Nothing
    '            End If

    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return Nothing
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetProviderList() As DataTable
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        Dim sqladpt As New SqlDataAdapter
    '        Dim dt As New DataTable
    '        Try
    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            _strsql = "select nProviderId as ProviderID,isnull(sFirstName,'') + space(1) + isnull(sMiddlename,'') +space(1) + isnull(slastname,'') as ProviderName,isnull(sFirstName,'') as ProviderFirstName ,isnull(sLastName,'') as ProviderLastName,isnull(sAddress,'') + space(1) + isnull(sStreet,'') as Address, isnull(sCity,'') as City,isnull(sstate,'') as State,isnull(szip,'') as Zip,isnull(sphoneno,'') as Phone,isnull(sexternalCode,'') as PrescriberID from Provider_Mst "
    '            sql.CommandText = _strsql
    '            conn.Open()

    '            sqladpt.SelectCommand = sql
    '            sqladpt.Fill(dt)

    '            If dt.Rows.Count > 0 Then
    '                Return dt
    '            Else
    '                Return Nothing
    '            End If

    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return Nothing
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' 
    '    ''' function not in use
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetPharmacyNameList() As DataTable
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        Dim sqladpt As New SqlDataAdapter
    '        Dim dt As New DataTable
    '        Try
    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            _strsql = "select nContactId as ContactID,isnull(sName,'') as PharmacyName,isnull(sexternalcode,'')as NCPDPID from Contacts_mst"
    '            sql.CommandText = _strsql
    '            conn.Open()

    '            sqladpt.SelectCommand = sql
    '            sqladpt.Fill(dt)

    '            If dt.Rows.Count > 0 Then
    '                Return dt
    '            Else
    '                Return Nothing
    '            End If

    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return Nothing
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetPatientNameList() As DataTable
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        Dim sqladpt As New SqlDataAdapter
    '        Dim dt As New DataTable
    '        Try
    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            _strsql = "select nPatientId as PatientID,isnull(sFirstName,'') + space(1)+ isnull(slastname,'') as PatientName,isnull(sPatientcode,'')as PatientCode from patient"
    '            sql.CommandText = _strsql
    '            conn.Open()

    '            sqladpt.SelectCommand = sql
    '            sqladpt.Fill(dt)

    '            If dt.Rows.Count > 0 Then
    '                Return dt
    '            Else
    '                Return Nothing
    '            End If

    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return Nothing
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetPatientList() As DataTable
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        Dim sqladpt As New SqlDataAdapter
    '        Dim dt As New DataTable
    '        Try
    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            _strsql = "select p.nPatientId as PatientID,isnull(p.sFirstName,'') as FirstName, isnull(p.slastname,'') as LastName,isnull(p.sPatientcode,'')as PatientCode,isnull(p.sAddressLine1,'') + space(1) + isnull(p.sAddressLine2,'') as Address, isnull(p.sCity,'') as City,isnull(p.sstate,'') as State,isnull(p.szip,'') as Zip,isnull(p.sphone,'') as Phone,isnull(c.sName,'')  as PharmacyName,isnull(c.sStreet,'')  as PharmacyAddress, isnull(c.sCity,'') as PharmacyCity,isnull(c.sstate,'') as PharmacyState,isnull(c.szip,'') as PharmacyZip,isnull(c.sphone,'') as PharmacyPhone,isnull(c.sExternalcode,'') as PharmacyID ,p.dtdob as DOB,isnull(p.sgender,'') as Gender from Patient p inner join Contacts_mst c on p.npharmacyId=c.ncontactId where c.sContacttype='Pharmacy'"
    '            sql.CommandText = _strsql
    '            conn.Open()

    '            sqladpt.SelectCommand = sql
    '            sqladpt.Fill(dt)

    '            If dt.Rows.Count > 0 Then
    '                Return dt
    '            Else
    '                Return Nothing
    '            End If

    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return Nothing
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' function not in use
    '    ''' </summary>
    '    ''' <param name="objPrescription"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function InsertRefillPrescription(ByVal objPrescription As EPrescription) As Boolean
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        Try
    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            _strsql = "Insert into PrescriptionRefillTransaction values ( '" & objPrescription.RxReferenceNumber & "' , " & objPrescription.RxTransactionID & " ,'Pending')"
    '            sql.CommandText = _strsql
    '            conn.Open()
    '            sql.ExecuteNonQuery()

    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            _strsql = ""

    '            Dim oDrug As New EDrug
    '            oDrug = objPrescription.DrugsCol.Item(0)
    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            Dim bln As Boolean = oDrug.MaySubstitute
    '            '_strsql = "Insert into PrescriptionRefillTransactiondetail (nRxTransactionID,sDrugName,sStrength,sQuantity,sQuantityQualifier,sRefillQuantity,sRefillQualifier,bMaySubstitutions,dtWrittendate) values (" & objPrescription.RxTransactionID & ",'" & oDrug.DrugName & "','" & oDrug.DrugStrength & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "' ,'" & oDrug.RefillsQualifier & "','" & oDrug.MaySubstitute & "','" & oDrug.WrittenDate & "')"
    '            _strsql = "Insert into PrescriptionRefillTransactiondetail (nRxTransactionID,sDrugName,sStrength,sQuantity,sQuantityQualifier,sRefillQuantity,sRefillQualifier,bMaySubstitutions,dtWrittendate,sRxReferenceNumber) " _
    '                        & " values (" & objPrescription.RxTransactionID & ",'" & oDrug.DrugName & "','" & oDrug.DrugStrength & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "' ,'" & oDrug.RefillsQualifier & "','" & bln & "','" & oDrug.WrittenDate & "', '" & oDrug.RxReferenceNumber & "')"

    '            sql.CommandText = _strsql
    '            sql.ExecuteNonQuery()
    '            Return True
    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return False
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use
    '    ''' </summary>
    '    ''' <param name="objPrescription"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function InsertPrescription(ByVal objPrescription As EPrescription) As Boolean
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        Try

    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            _strsql = "select isnull(max(isnull(nRxTransactionId,0)),0)+1 from PrescriptionTransaction"
    '            sql.CommandText = _strsql
    '            conn.Open()
    '            Dim Id As Int64 = sql.ExecuteScalar

    '            objPrescription.RxTransactionID = Id.ToString

    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If

    '            sql = New SqlCommand
    '            sql.Connection = conn
    '            sql.CommandType = CommandType.Text
    '            objPrescription.PrescriptionDate = Now.Date
    '            _strsql = "Insert into PrescriptionTransaction values (" & objPrescription.RxTransactionID & " ,'" & objPrescription.PrescriptionDate & "'," & objPrescription.ProviderID & "," & objPrescription.PatientID & ")"
    '            sql.CommandText = _strsql
    '            sql.ExecuteNonQuery()

    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            _strsql = ""
    '            Dim oDrug As New EDrug
    '            For icnt As Int16 = 0 To objPrescription.DrugsCol.Count - 1
    '                oDrug = objPrescription.DrugsCol.Item(0)

    '                sql = New SqlCommand
    '                sql.Connection = conn
    '                sql.CommandType = CommandType.Text
    '                _strsql = "select isnull(max(isnull(nPrescriptionID,0)),0)+1 from PrescriptionTransactiondetails"
    '                sql.CommandText = _strsql

    '                Dim PrescriptionItemId As Int64 = sql.ExecuteScalar

    '                oDrug.PrescriptionID = PrescriptionItemId.ToString

    '                If Not IsNothing(sql) Then
    '                    sql.Dispose()
    '                    sql = Nothing
    '                End If

    '                sql = New SqlCommand
    '                sql.Connection = conn
    '                sql.CommandType = CommandType.Text
    '                Dim bln As Boolean = oDrug.MaySubstitute
    '                oDrug.WrittenDate = Now.Date
    '                oDrug.Directions = oDrug.DrugName & " " & oDrug.Dosage & " " & oDrug.DrugRoute & " " & oDrug.DrugFrequency & " " & oDrug.DrugDuration
    '                '_strsql = "Insert into PrescriptionRefillTransactiondetail (nRxTransactionID,sDrugName,sStrength,sQuantity,sQuantityQualifier,sRefillQuantity,sRefillQualifier,bMaySubstitutions,dtWrittendate) values (" & objPrescription.RxTransactionID & ",'" & oDrug.DrugName & "','" & oDrug.DrugStrength & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "' ,'" & oDrug.RefillsQualifier & "','" & oDrug.MaySubstitute & "','" & oDrug.WrittenDate & "')"
    '                _strsql = "Insert into PrescriptionTransactiondetails (nPrescriptionID,nRxTransactionID,nDrugID,sDrugName,sDrugForm,sStrength,sDosage,sroute,sFrequency,sDuration,sQuantity,sQuantityQualifier,sRefillQuantity,sRefillQualifier,bMaySubstitutions,dtWrittendate,sRxReferenceNumber,sStatus) " _
    '                            & " values (" & oDrug.PrescriptionID & "," & objPrescription.RxTransactionID & ",'" & oDrug.DrugID & "','" & oDrug.DrugName & "','" & oDrug.Drugform & "','" & oDrug.DrugStrength & "','" & oDrug.Dosage & "','" & oDrug.DrugRoute & "','" & oDrug.DrugFrequency & "','" & oDrug.DrugDuration & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "' ,'" & oDrug.RefillsQualifier & "','" & bln & "','" & oDrug.WrittenDate & "', '" & oDrug.RxReferenceNumber & "','')"

    '                sql.CommandText = _strsql
    '                sql.ExecuteNonQuery()
    '            Next

    '            Return True
    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return False
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use
    '    ''' </summary>
    '    ''' <param name="Id"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetAllActiveDrugs(ByVal Id As String) As DataTable
    '        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
    '        Dim sqladpt As New SqlDataAdapter
    '        Dim dt As New DataTable

    '        conn.Open()
    '        Dim sql As SqlCommand
    '        Dim _strsql As String
    '        Try
    '            'Fetch the pharmacy information for given patient
    '            sql = New SqlCommand
    '            sql.CommandType = CommandType.Text
    '            sql.Connection = conn
    '            _strsql = "select isnull(ptd.sDrugName,'') as DrugName ,isnull(ptd.sDrugForm,'') as DrugForm,isnull(ptd.sStrength,'') as DrugStrength,isnull(ptd.sDosage,'') as DrugDosage,isnull(ptd.sRoute,'') as DrugRoute,isnull(ptd.sFrequency,'') as DrugFrequency,isnull(ptd.sDuration,'') as DrugDuration,isnull(ptd.sQuantity,'') as DrugQuantity,isnull(ptd.sQuantityQualifier,'') as DrugQuantityQualifier,isnull(ptd.sRefillQuantity,'') as RefillQuantity,isnull(ptd.sRefillQualifier,'') as RefillQualifier,isnull(ptd.bMaySubstitutions,'False') as Maysubstitute ,nPrescriptionID as PrescriptionID,sStatus as Status from Prescriptiontransaction pt inner join PrescriptionTransactionDetails ptd on pt.nRxTransactionID=ptd.nRxTransactionID where ptd.sStatus<>'Cancelled' and pt.nRxTransactionID =" & ID & ""  'where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"

    '            sql.CommandText = _strsql

    '            dt = New DataTable
    '            sqladpt = New SqlDataAdapter(sql)
    '            sqladpt.Fill(dt)

    '            If dt.Rows.Count > 0 Then
    '                Return dt
    '            Else
    '                Return Nothing
    '            End If
    '        Catch ex As Exception
    '            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
    '            Throw New gloSurescriptDBException(ex.Message)
    '            Return Nothing
    '        Finally
    '            If Not IsNothing(sql) Then
    '                sql.Dispose()
    '                sql = Nothing
    '            End If
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '                conn.Dispose()
    '                conn = Nothing
    '            End If
    '        End Try
    '    End Function

    '#End Region

#End Region

#Region "Functions not used in gloSurescriptInterface"
    '''' <summary>
    ''    ''' Function not in use as we are implementing this functionality of
    ''    ''' reading FreeStanding Error in webservice
    ''    ''' </summary>
    ''    ''' <param name="objErrorMessage"></param>
    ''    ''' <returns></returns>
    ''    ''' <remarks></remarks>
    'Public Function ReadFreeStandingError(ByVal objErrorMessage As SureScriptErrorMessage) As Boolean
    '    'Dim areader As XmlReader
    '    'Dim strfilepath As String = gloSurescriptGeneral.RootPath & "\SurescriptFreeStandingError.xml"
    '    'Try
    '    '    areader = XmlReader.Create(strfilepath)
    '    '    While areader.Read

    '    '        Dim blnIsRefillsQualifier As Boolean = False

    '    '        If areader.NodeType = XmlNodeType.Element Then
    '    '            Select Case areader.Name

    '    '                'this is the message id for the Refillrequest that shall go in the MessageId field
    '    '                Case "From"
    '    '                    objErrorMessage.MessageFrom = areader.ReadInnerXml
    '    '                Case "To"
    '    '                    objErrorMessage.MessageTo = areader.ReadInnerXml
    '    '                Case "MessageID"
    '    '                    objErrorMessage.MessageID = areader.ReadInnerXml
    '    '                Case "RelatesToMessageID"
    '    '                    objErrorMessage.RelatesToMessageId = areader.ReadInnerXml
    '    '                Case "SentTime"
    '    '                    objErrorMessage.DateTimeStamp = areader.ReadInnerXml
    '    '                    objErrorMessage.DateReceived = Date.Now
    '    '                Case "Code"
    '    '                    objErrorMessage.ErrorCode = areader.ReadInnerXml
    '    '                Case "DescriptionCode"
    '    '                    objErrorMessage.DescriptionCode = areader.ReadInnerXml
    '    '                Case "Description"
    '    '                    objErrorMessage.Description = areader.ReadInnerXml
    '    '            End Select

    '    '        End If
    '    '    End While
    '    '    objErrorMessage.MessageName = "Error"

    '    '    Dim oError As New SureScriptErrorMessage

    '    '    Dim objSureScriptDBLayer As New gloSureScriptDBLayer
    '    '    If objSureScriptDBLayer.InsertErrorDetails(objErrorMessage) Then
    '    '        objSureScriptDBLayer.InsertintoMessageTransaction(CType(objErrorMessage, SureScriptMessage))
    '    '    End If

    '    '    Return True
    '    'Catch ex As gloSurescriptDBException
    '    '    Throw ex
    '    'Catch ex As GloSurescriptException
    '    '    Throw ex
    '    'Catch ex As Exception
    '    '    Throw New GloSurescriptException(ex.Message)
    '    'End Try
    '    Dim ds As New DataSet
    '    Dim oDrug As EDrug

    '    Try
    '        ds.ReadXml(XMLFilePath, XmlReadMode.InferSchema)
    '        If ds.Tables.Count > 0 Then
    '            For icnt As Int32 = 0 To ds.Tables(0).Rows.Count - 1


    '                objErrorMessage.MessageFrom = ds.Tables(0).Rows(icnt)("sMessageFrom")

    '                objErrorMessage.MessageTo = ds.Tables(0).Rows(icnt)("sMessageTo")

    '                objErrorMessage.MessageID = ds.Tables(0).Rows(icnt)("sMessageID")

    '                objErrorMessage.RelatesToMessageId = ds.Tables(0).Rows(icnt)("sRelativeMessageID")

    '                objErrorMessage.DateTimeStamp = ds.Tables(0).Rows(icnt)("dtSentTime")

    '                objErrorMessage.DateReceived = Date.Now
    '                If (ds.Tables(0).Columns.Contains("sStatusCode")) = True Then
    '                    objErrorMessage.ErrorCode = ds.Tables(0).Rows(icnt)("sStatusCode")

    '                End If
    '                If (ds.Tables(0).Columns.Contains("sResponseCode")) = True Then
    '                    objErrorMessage.DescriptionCode = ds.Tables(0).Rows(icnt)("sResponseCode")

    '                End If

    '                If (ds.Tables(0).Columns.Contains("sResponseDescription")) = True Then
    '                    objErrorMessage.Description = ds.Tables(0).Rows(icnt)("sResponseDescription")
    '                End If


    '                objErrorMessage.MessageName = "Error"

    '                Dim oError As New SureScriptErrorMessage

    '                Dim objSureScriptDBLayer As New gloSureScriptDBLayer
    '                If objSureScriptDBLayer.InsertErrorDetails(objErrorMessage) Then
    '                    objSureScriptDBLayer.InsertintoMessageTransaction(CType(objErrorMessage, SureScriptMessage))
    '                End If

    '            Next
    '            Return True
    '        End If
    '    Catch ex As gloSurescriptDBException
    '        Throw ex
    '    Catch ex As GloSurescriptException
    '        Throw ex
    '    Catch ex As Exception
    '        Throw New GloSurescriptException(ex.Message)
    '    End Try
    'End Function
    'Public Function InsertRefillPrescription() As Boolean
    '    Dim objPrescription As New EPrescription
    '    Try
    '        If ReadRefillPrescription(objPrescription) Then
    '            Dim objSureScriptDBLayer As New gloSureScriptDBLayer
    '            If objSureScriptDBLayer.InsertRefillPrescription(objPrescription) Then
    '                'Insert the message info into DB

    '                objSureScriptDBLayer.InsertintoMessageTransaction(CType(objPrescription.DrugsCol.Item(0), SureScriptMessage))
    '                Dim oStatus As New StatusMessage
    '                oStatus.MessageID = 0
    '                oStatus.RelatesToMessageId = objPrescription.DrugsCol.Item(0).MessageID
    '                oStatus.MessageFrom = objPrescription.DrugsCol.Item(0).MessageTo
    '                oStatus.MessageTo = objPrescription.DrugsCol.Item(0).MessageFrom
    '                oStatus.StatusCode = "000" 'Means the message has been accepted but is yet to be processed/verified
    '                oStatus.MessageName = "Status"
    '                oStatus.SMessageType = MessageType.eStatus
    '                oStatus.DateReceived = Date.Now
    '                'Generate status message for Refill Request
    '                GenerateXMLForStatus(oStatus)
    '                objSureScriptDBLayer.InsertAcknowledgements(oStatus, False)
    '                'Insert the details of the Status Message sent in the MessageTransaction
    '                objSureScriptDBLayer.InsertintoMessageTransaction(CType(oStatus, SureScriptMessage))
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        Else
    '            Return False
    '        End If

    '    Catch ex As gloSurescriptDBException
    '        gloSurescriptGeneral.ErrorMessage(ex.Message)
    '    Catch ex As GloSurescriptException
    '        gloSurescriptGeneral.ErrorMessage(ex.Message)
    '    Catch ex As Exception

    '    End Try
    'End Function
    '#Region "Functions that are not being used"
    '    ''' <summary>
    '    ''' Function not being used as we are directly invoking GenerateXMLforNewRx
    '    ''' </summary>
    '    ''' <param name="Prescriptiondate"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateNewRx(ByVal Prescriptiondate As DateTime) As EPrescription
    '        Dim objPrescription As EPrescription
    '        Dim objSureScriptDBLayer As gloSureScriptDBLayer
    '        Try
    '            objPrescription = New EPrescription
    '            objPrescription.PrescriptionDate = Prescriptiondate
    '            objSureScriptDBLayer = New gloSureScriptDBLayer
    '            If objSureScriptDBLayer.GetNewPrescription(objPrescription) Then
    '                If objPrescription.DrugsCol.Count > 0 Then
    '                    If GenerateXMLforNewRx(objPrescription, 0) Then
    '                        'Insert the message details into messagetransaction
    '                        objSureScriptDBLayer.InsertintoMessageTransaction(CType(objPrescription.DrugsCol.Item(0), SureScriptMessage))
    '                    End If
    '                End If
    '            End If
    '            Return objPrescription
    '        Catch ex As gloSurescriptDBException

    '        Catch ex As GloSurescriptException

    '        Catch ex As Exception

    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetActivePrescriptions() As EPrescriptions
    '        Dim objSureScriptDBLayer As New gloSureScriptDBLayer
    '        Try
    '            Return objSureScriptDBLayer.GetActivePrescriptions
    '        Catch ex As gloSurescriptDBException
    '            gloSurescriptGeneral.ErrorMessage(ex.Message)
    '        Catch ex As GloSurescriptException
    '            gloSurescriptGeneral.ErrorMessage(ex.Message)

    '        Catch ex As Exception

    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Function not in use has been implemented in webservice
    '    ''' </summary>
    '    ''' <param name="PrescriberID"></param>
    '    ''' <param name="sMessageName"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function ReadIncomingMessages(ByVal PrescriberID As String, Optional ByVal sMessageName As String = "")
    '        Try
    '            Dim oRxService As New eRxService.eRxMessage
    '            oRxService.Credentials = System.Net.CredentialCache.DefaultCredentials
    '            Dim _Key As String = oRxService.Login("sarika@ophit.net", "spX12ss@!!21nasik")
    '            Dim strfilename As String

    '            If sMessageName = "" Then
    '                strfilename = ExtractXML(oRxService.ReadResponsesonPrescriber(PrescriberID, _Key))
    '            Else
    '                strfilename = ExtractXML(oRxService.ReadResponsesonPrescriberMessages(PrescriberID, sMessageName, _Key))
    '                ReadResponse(strfilename, sMessageName)
    '            End If

    '        Catch ex As Exception

    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not being used
    '    ''' </summary>
    '    ''' <param name="objPrescription"></param>
    '    ''' <param name="icnt"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateXMLforCancelRx(ByVal objPrescription As EPrescription, ByVal icnt As Int16) As Boolean
    '        If ValidateData(objPrescription, icnt) Then

    '            Dim strfilepath As String = GenerateFileName(MessageType.eCancelRx)
    '            Dim xmlwriter As XmlTextWriter = Nothing
    '            XMLFilePath = strfilepath
    '            '--
    '            Try
    '                If File.Exists(strfilepath) Then
    '                    File.Delete(strfilepath)
    '                End If
    '                Dim oDrug As EDrug
    '                oDrug = objPrescription.DrugsCol.Item(icnt)

    '                Dim dtdate As DateTime = Date.UtcNow
    '                Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
    '                Dim strtime As String = Format(dtdate, "hh:mm:ss")

    '                Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

    '                Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
    '                strUTCFormat = strdate & "T" & strtime & ":0Z"

    '                xmlwriter = New XmlTextWriter(strfilepath, Nothing)
    '                oDrug.MessageName = "CancelRx"
    '                oDrug.MessageID = "CancelRx" & strtemp

    '                oDrug.DateTimeStamp = strUTCFormat
    '                oDrug.DateReceived = Now.Date
    '                oDrug.MessageFrom = "mailto:" & objPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
    '                oDrug.MessageTo = "mailto:" & objPrescription.RxPharmacy.PharmacyID & ".ncpdp@surescripts.com"


    '                xmlwriter.WriteStartDocument()
    '                xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 

    '                xmlwriter.WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

    '                xmlwriter.WriteStartElement("Header")

    '                xmlwriter.WriteElementString("To", oDrug.MessageTo) 'The Child 

    '                xmlwriter.WriteElementString("From", oDrug.MessageFrom) 'The Child 

    '                xmlwriter.WriteElementString("MessageID", oDrug.MessageID) 'The Child 

    '                'RelatesToMessageID is not available for this message
    '                xmlwriter.WriteElementString("SentTime", strUTCFormat) 'The Child 
    '                xmlwriter.WriteEndElement() 'End Header Element

    '                xmlwriter.WriteStartElement("Body")
    '                xmlwriter.WriteStartElement("CancelRx")

    '                xmlwriter.WriteElementString("PrescriberOrderNumber", objPrescription.RxPrescriber.PrescriberID)
    '                xmlwriter.WriteStartElement("Pharmacy")

    '                xmlwriter.WriteStartElement("Identification")
    '                xmlwriter.WriteElementString("NCPDPID", objPrescription.RxPharmacy.PharmacyID)
    '                xmlwriter.WriteEndElement() 'End Identification Element
    '                xmlwriter.WriteElementString("StoreName", objPrescription.RxPharmacy.PharmacyName)

    '                If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.City.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.State.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim <> "" Then
    '                    xmlwriter.WriteStartElement("Address")
    '                    If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim <> "" Then
    '                        xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPharmacy.PharmacyAddress.Address1 & " " & objPrescription.RxPharmacy.PharmacyAddress.Address2 & ",")
    '                    End If
    '                    If objPrescription.RxPharmacy.PharmacyAddress.City.Trim <> "" Then
    '                        xmlwriter.WriteElementString("City", objPrescription.RxPharmacy.PharmacyAddress.City)
    '                    End If
    '                    If objPrescription.RxPharmacy.PharmacyAddress.State.Trim <> "" Then
    '                        xmlwriter.WriteElementString("State", objPrescription.RxPharmacy.PharmacyAddress.State)
    '                    End If
    '                    If objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim <> "" Then
    '                        xmlwriter.WriteElementString("ZipCode", objPrescription.RxPharmacy.PharmacyAddress.Zip)
    '                    End If
    '                    xmlwriter.WriteEndElement() 'End Address Element
    '                End If

    '                If GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone).Trim <> "" Then
    '                    xmlwriter.WriteStartElement("PhoneNumbers")
    '                    xmlwriter.WriteStartElement("Phone")
    '                    xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone))
    '                    xmlwriter.WriteElementString("Qualifier", "TE")
    '                    xmlwriter.WriteEndElement() 'End Phone Element
    '                    xmlwriter.WriteEndElement() 'End PhoneNumbers element
    '                End If

    '                xmlwriter.WriteEndElement() 'End Pharmacy Element

    '                xmlwriter.WriteStartElement("Prescriber")
    '                xmlwriter.WriteStartElement("Identification")
    '                xmlwriter.WriteElementString("SPI", objPrescription.RxPrescriber.PrescriberID)
    '                xmlwriter.WriteEndElement() 'End Identification Element
    '                xmlwriter.WriteElementString("ClinicName", "gloClinic")

    '                xmlwriter.WriteStartElement("Name")
    '                xmlwriter.WriteElementString("LastName", objPrescription.RxPrescriber.PrescriberName.LastName)
    '                If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim <> "" Then
    '                    xmlwriter.WriteElementString("FirstName", objPrescription.RxPrescriber.PrescriberName.FirstName)
    '                End If
    '                xmlwriter.WriteEndElement() 'End Name Element

    '                xmlwriter.WriteStartElement("Address")
    '                xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPrescriber.PrescriberAddress.Address1)
    '                xmlwriter.WriteElementString("City", objPrescription.RxPrescriber.PrescriberAddress.City)
    '                xmlwriter.WriteElementString("State", objPrescription.RxPrescriber.PrescriberAddress.State)
    '                xmlwriter.WriteElementString("Zip", objPrescription.RxPrescriber.PrescriberAddress.Zip)
    '                xmlwriter.WriteEndElement() 'End Name Element

    '                xmlwriter.WriteStartElement("PhoneNumbers")
    '                xmlwriter.WriteStartElement("Phone")
    '                xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Phone))
    '                xmlwriter.WriteElementString("Qualifier", "TE")
    '                xmlwriter.WriteEndElement() 'End Phone Element
    '                xmlwriter.WriteEndElement() 'End PhoneNumbers element
    '                xmlwriter.WriteEndElement() 'End Prescriber Element

    '                xmlwriter.WriteStartElement("Patient")
    '                xmlwriter.WriteStartElement("Name")
    '                xmlwriter.WriteElementString("LastName", objPrescription.RxPatient.PatientName.LastName)
    '                xmlwriter.WriteElementString("FirstName", objPrescription.RxPatient.PatientName.FirstName)
    '                xmlwriter.WriteEndElement() 'End Name Element

    '                xmlwriter.WriteElementString("Gender", objPrescription.RxPatient.Gender)
    '                xmlwriter.WriteElementString("DateOfBirth", Getdatetype(objPrescription.RxPatient.DateofBirth))

    '                'xmlwriter.WriteStartElement("Address")
    '                'xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPatient.PatientAddress.Address1)
    '                'xmlwriter.WriteElementString("City", objPrescription.RxPatient.PatientAddress.City)
    '                'xmlwriter.WriteElementString("State", objPrescription.RxPatient.PatientAddress.State)
    '                'xmlwriter.WriteElementString("Zip", objPrescription.RxPatient.PatientAddress.Zip)
    '                'xmlwriter.WriteEndElement() 'End Name Element

    '                If objPrescription.RxPatient.PatientAddress.Address1.Trim <> "" Or objPrescription.RxPatient.PatientAddress.City.Trim <> "" Or objPrescription.RxPatient.PatientAddress.State.Trim <> "" Or objPrescription.RxPatient.PatientAddress.Zip.Trim <> "" Then
    '                    xmlwriter.WriteStartElement("Address")
    '                    If objPrescription.RxPatient.PatientAddress.Address1.Trim <> "" Then
    '                        xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPatient.PatientAddress.Address1 & ",")
    '                    End If
    '                    If objPrescription.RxPatient.PatientAddress.City.Trim <> "" Then
    '                        xmlwriter.WriteElementString("City", objPrescription.RxPatient.PatientAddress.City)
    '                    End If
    '                    If objPrescription.RxPatient.PatientAddress.State.Trim <> "" Then
    '                        xmlwriter.WriteElementString("State", objPrescription.RxPatient.PatientAddress.State)
    '                    End If
    '                    If objPrescription.RxPatient.PatientAddress.Zip.Trim <> "" Then
    '                        xmlwriter.WriteElementString("ZipCode", objPrescription.RxPatient.PatientAddress.Zip)
    '                    End If
    '                    xmlwriter.WriteEndElement() 'End Name Element
    '                End If

    '                If GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone).Trim <> "" Then
    '                    xmlwriter.WriteStartElement("PhoneNumbers")
    '                    xmlwriter.WriteStartElement("Phone")
    '                    xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone))
    '                    xmlwriter.WriteElementString("Qualifier", "TE")
    '                    xmlwriter.WriteEndElement() 'End Phone Element
    '                    xmlwriter.WriteEndElement() 'End PhoneNumbers element

    '                End If
    '                xmlwriter.WriteEndElement() 'End Patient Element

    '                xmlwriter.WriteStartElement("MedicationPrescribed")
    '                xmlwriter.WriteElementString("DrugDescription", objPrescription.DrugsCol.Item(icnt).DrugName & " " & objPrescription.DrugsCol.Item(icnt).Dosage & " " & objPrescription.DrugsCol.Item(icnt).Drugform)
    '                xmlwriter.WriteStartElement("Quantity")
    '                xmlwriter.WriteElementString("Qualifier", objPrescription.DrugsCol.Item(icnt).DrugQuantityQualifier)
    '                xmlwriter.WriteElementString("Value", objPrescription.DrugsCol.Item(icnt).DrugQuantity)
    '                xmlwriter.WriteEndElement() 'End Quantity Element

    '                xmlwriter.WriteElementString("Directions", objPrescription.DrugsCol.Item(icnt).Directions)

    '                xmlwriter.WriteStartElement("Refills")
    '                xmlwriter.WriteElementString("Qualifier", "R")
    '                If objPrescription.DrugsCol.Item(icnt).RefillQuantity.Trim <> "" Then
    '                    xmlwriter.WriteElementString("Quantity", objPrescription.DrugsCol.Item(icnt).RefillQuantity)
    '                End If
    '                xmlwriter.WriteEndElement() 'End Refills Element
    '                If objPrescription.DrugsCol.Item(icnt).MaySubstitute Then
    '                    xmlwriter.WriteElementString("Substitutions", 1)
    '                Else
    '                    xmlwriter.WriteElementString("Substitutions", 0)
    '                End If
    '                xmlwriter.WriteElementString("WrittenDate", Getdatetype(objPrescription.DrugsCol.Item(icnt).WrittenDate))
    '                xmlwriter.WriteEndElement() 'End MedicationPrescribed Element


    '                xmlwriter.WriteEndElement() 'End NewRx element
    '                xmlwriter.WriteEndElement() 'End Body Element
    '                xmlwriter.WriteEndElement() 'End Message Element
    '                xmlwriter.WriteEndDocument()
    '                xmlwriter.Close()
    '                gloSurescriptGeneral.ConvertFiletoBinary(strfilepath)
    '                Return True
    '            Catch ex As gloSurescriptDBException

    '            Catch ex As Exception

    '            End Try


    '        End If

    '    End Function
    '    ''' <summary>
    '    ''' Function not being used as we need permission to
    '    ''' implement Verify message
    '    ''' </summary>
    '    ''' <param name="oStatus"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateXMLForVerify(ByVal oStatus As StatusMessage) As Boolean
    '        Try
    '            Dim strfilepath As String = GenerateFileName(MessageType.eVerify)
    '            If File.Exists(strfilepath) Then
    '                File.Delete(strfilepath)
    '            End If

    '            Dim xmlwriter As XmlTextWriter = Nothing
    '            xmlwriter = New XmlTextWriter(strfilepath, Nothing)

    '            Dim dtdate As DateTime = Date.UtcNow
    '            Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
    '            Dim strtime As String = Format(dtdate, "hh:mm:ss")

    '            Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
    '            Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
    '            strUTCFormat = strdate & "T" & strtime & ":0Z"

    '            oStatus.MessageID = "Verify" & strtemp
    '            oStatus.DateTimeStamp = strUTCFormat
    '            oStatus.DateReceived = Now

    '            xmlwriter.WriteStartDocument()
    '            xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 

    '            xmlwriter.WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

    '            xmlwriter.WriteStartElement("Header")

    '            xmlwriter.WriteElementString("To", oStatus.MessageTo) 'The Child 

    '            xmlwriter.WriteElementString("From", oStatus.MessageFrom) 'The Child 

    '            xmlwriter.WriteElementString("MessageID", oStatus.MessageID) 'The Child 

    '            xmlwriter.WriteElementString("RelatesToMessageID", oStatus.RelatesToMessageId) 'The Child 
    '            xmlwriter.WriteElementString("SentTime", oStatus.DateTimeStamp) 'The Child 
    '            xmlwriter.WriteEndElement() 'End Header Element

    '            xmlwriter.WriteStartElement("Body")
    '            xmlwriter.WriteStartElement("Verify")
    '            xmlwriter.WriteElementString("Code", "010") 'Indicates that message has been verified successfully
    '            xmlwriter.WriteEndElement() 'End Verify element

    '            xmlwriter.WriteEndElement()  'End Body element
    '            xmlwriter.WriteEndElement()  'End Message Element
    '            xmlwriter.WriteEndDocument() 'End Document
    '            xmlwriter.Close()            'Close xml file
    '            gloSurescriptGeneral.ConvertFiletoBinary(strfilepath)
    '            Return True
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not being used as status message shall be generated in  webservice
    '    ''' </summary>
    '    ''' <param name="oStatus"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateXMLForStatus(ByVal oStatus As StatusMessage) As Boolean
    '        Try
    '            Dim strfilepath As String = GenerateFileName(MessageType.eStatus)
    '            If File.Exists(strfilepath) Then
    '                File.Delete(strfilepath)
    '            End If
    '            Dim xmlwriter As XmlTextWriter = Nothing
    '            xmlwriter = New XmlTextWriter(strfilepath, Nothing)

    '            Dim dtdate As DateTime = Date.UtcNow
    '            Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
    '            Dim strtime As String = Format(dtdate, "hh:mm:ss")

    '            Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

    '            Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
    '            strUTCFormat = strdate & "T" & strtime & ":0Z"


    '            oStatus.DateTimeStamp = strUTCFormat
    '            oStatus.DateReceived = Now

    '            xmlwriter.WriteStartDocument()
    '            xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 

    '            xmlwriter.WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

    '            xmlwriter.WriteStartElement("Header")

    '            xmlwriter.WriteElementString("To", oStatus.MessageTo) 'The Child 

    '            xmlwriter.WriteElementString("From", oStatus.MessageFrom) 'The Child 

    '            xmlwriter.WriteElementString("MessageID", oStatus.MessageID) 'The Child 

    '            xmlwriter.WriteElementString("RelatesToMessageID", oStatus.RelatesToMessageId) 'The Child 
    '            xmlwriter.WriteElementString("SentTime", oStatus.DateTimeStamp) 'The Child 
    '            xmlwriter.WriteEndElement() 'End Header Element

    '            xmlwriter.WriteStartElement("Body")

    '            If oStatus.StatusCode = "000" Then
    '                xmlwriter.WriteStartElement("Status")
    '            ElseIf oStatus.StatusCode = "010" Then
    '                xmlwriter.WriteStartElement("Verify")
    '            End If

    '            xmlwriter.WriteElementString("Code", oStatus.StatusCode)
    '            xmlwriter.WriteEndElement() 'End Status element

    '            xmlwriter.WriteEndElement()  'End Body element
    '            xmlwriter.WriteEndElement()  'End Message Element
    '            xmlwriter.WriteEndDocument() 'End Document
    '            xmlwriter.Close()            'Close xml file
    '            gloSurescriptGeneral.ConvertFiletoBinary(strfilepath)
    '            Return True
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' function not used as XML for Error is generated
    '    ''' in webservice
    '    ''' </summary>
    '    ''' <param name="oError"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateErrorMessage(ByVal oError As SureScriptErrorMessage) As Boolean
    '        Try
    '            If GenerateXMLForError(oError) Then
    '                Dim oSurescriptDBLayer As New gloSureScriptDBLayer
    '                If oSurescriptDBLayer.InsertErrorDetails(oError) Then
    '                    oSurescriptDBLayer.InsertintoMessageTransaction(CType(oError, SureScriptMessage))
    '                    'RaiseEvent ErrorGenerated()

    '                End If
    '            End If
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not used as this functionality has been implemented in Webservice
    '    ''' </summary>
    '    ''' <param name="oError"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateXMLForError(ByVal oError As SureScriptErrorMessage) As Boolean
    '        Try
    '            Dim strfilepath As String = GenerateFileName(MessageType.eError)
    '            If File.Exists(strfilepath) Then
    '                File.Delete(strfilepath)
    '            End If
    '            Dim xmlwriter As XmlTextWriter = Nothing
    '            xmlwriter = New XmlTextWriter(strfilepath, Nothing)
    '            XMLFilePath = strfilepath

    '            Dim dtdate As DateTime = Date.UtcNow
    '            Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
    '            Dim strtime As String = Format(dtdate, "hh:mm:ss")

    '            Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

    '            Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
    '            strUTCFormat = strdate & "T" & strtime & ":0Z"

    '            oError.DateTimeStamp = strUTCFormat
    '            oError.DateReceived = Now

    '            oError.MessageID = "Error" & strtemp

    '            oError.MessageName = "Error" & strtemp
    '            xmlwriter.WriteStartDocument()
    '            xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 

    '            xmlwriter.WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

    '            xmlwriter.WriteStartElement("Header")

    '            xmlwriter.WriteElementString("To", oError.MessageTo) 'The Child 
    '            xmlwriter.WriteElementString("From", oError.MessageFrom) 'The Child 
    '            xmlwriter.WriteElementString("MessageID", oError.MessageID) 'The Child 

    '            xmlwriter.WriteElementString("RelatesToMessageID", oError.RelatesToMessageId) 'The Child 
    '            xmlwriter.WriteElementString("SentTime", oError.DateTimeStamp) 'The Child 
    '            xmlwriter.WriteEndElement() 'End Header Element

    '            xmlwriter.WriteStartElement("Body")
    '            xmlwriter.WriteStartElement("Error")
    '            xmlwriter.WriteElementString("Code", oError.ErrorCode)
    '            xmlwriter.WriteElementString("DescriptionCode", oError.DescriptionCode)
    '            xmlwriter.WriteElementString("Description", oError.Description)

    '            xmlwriter.WriteEndElement() 'End Error element
    '            xmlwriter.WriteEndElement()  'End Body element
    '            xmlwriter.WriteEndElement()  'End Message Element
    '            xmlwriter.WriteEndDocument() 'End Document
    '            xmlwriter.Close()            'Close xml file
    '            gloSurescriptGeneral.ConvertFiletoBinary(strfilepath)
    '            Return True
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not being used as we are currently not implementing CancelRx
    '    ''' </summary>
    '    ''' <param name="objPrescription"></param>
    '    ''' <param name="icnt"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GenerateCancelRx(ByVal objPrescription As EPrescription, ByVal icnt As Int16) As Boolean
    '        Dim objSureScriptDBLayer As gloSureScriptDBLayer

    '        Try
    '            objSureScriptDBLayer = New gloSureScriptDBLayer
    '            If GenerateXMLforCancelRx(objPrescription, icnt) Then
    '                'Insert the message details into messagetransaction
    '                objSureScriptDBLayer.InsertintoMessageTransaction(CType(objPrescription.DrugsCol.Item(icnt), SureScriptMessage))
    '            End If
    '        Catch ex As gloSurescriptDBException

    '        Catch ex As GloSurescriptException

    '        Catch ex As Exception

    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Function not in use 
    '    ''' </summary>
    '    ''' <param name="strcode"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function GetSubstitutionCode(ByVal strcode As String) As Boolean
    '        Select Case strcode

    '            'Substitutions are allowed
    '            Case "0", "2", "3", "4", "5", "8"
    '                Return False
    '                'Substitutions not allowed
    '            Case "1", "7"
    '                Return True
    '        End Select
    '    End Function
    '    ''' <summary>
    '    ''' function not being used as we are not implementing cancel drug message
    '    ''' </summary>
    '    ''' <param name="RxTransactionID"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function GetDrugsToCancel(ByVal RxTransactionID As String) As EPrescription
    '        Dim objPrescription As EPrescription
    '        Dim objSureScriptDBLayer As gloSureScriptDBLayer
    '        Try
    '            objPrescription = New EPrescription
    '            objPrescription.RxTransactionID = CType(RxTransactionID, Int64)

    '            objSureScriptDBLayer = New gloSureScriptDBLayer
    '            If objSureScriptDBLayer.GetNewPrescription(objPrescription, True) Then
    '                If objPrescription.DrugsCol.Count > 0 Then
    '                    Return objPrescription
    '                Else
    '                    Return Nothing
    '                End If
    '            Else
    '                Return Nothing
    '            End If

    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)

    '        Finally
    '            If Not IsNothing(objSureScriptDBLayer) Then
    '                objSureScriptDBLayer.Dispose()
    '                objSureScriptDBLayer = Nothing
    '            End If
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' 
    '    ''' Function not is use presently used in webservice
    '    ''' </summary>
    '    ''' <param name="objPrescription"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function ReadRefillPrescription(ByVal objPrescription As EPrescription) As Boolean
    '        '    Dim areader As XmlReader
    '        '    Dim strfilepath As String = gloSurescriptGeneral.RootPath & "\RefillRequest.xml"
    '        '    Try
    '        '        areader = XmlReader.Create(strfilepath)
    '        '        Dim oDrug As Drug
    '        '        oDrug = New Drug

    '        '        While areader.Read

    '        '            Dim blnIsRefillsQualifier As Boolean = False

    '        '            If areader.NodeType = XmlNodeType.Element Then
    '        '                Select Case areader.Name

    '        '                    'this is the message id for the Refillrequest that shall go in the MessageId field
    '        '                    Case "From"
    '        '                        oDrug.MessageFrom = areader.ReadInnerXml
    '        '                    Case "To"
    '        '                        oDrug.MessageTo = areader.ReadInnerXml
    '        '                    Case "MessageID"
    '        '                        oDrug.MessageID = areader.ReadInnerXml
    '        '                    Case "RelatesToMessageID"
    '        '                        oDrug.RelatesToMessageId = areader.ReadInnerXml
    '        '                    Case "SentTime"
    '        '                        oDrug.DateTimeStamp = areader.ReadInnerXml
    '        '                        oDrug.DateReceived = Date.Now
    '        '                    Case "RxReferenceNumber"
    '        '                        oDrug.RxReferenceNumber = areader.ReadInnerXml
    '        '                        objPrescription.RxReferenceNumber = oDrug.RxReferenceNumber
    '        '                        oDrug.TransactionID = oDrug.RxReferenceNumber
    '        '                    Case "PrescriberOrderNumber"
    '        '                        objPrescription.RxTransactionID = areader.ReadInnerXml
    '        '                    Case "NCPDPID"
    '        '                        objPrescription.RxPharmacy.PharmacyID = areader.ReadInnerXml
    '        '                    Case "StoreName"
    '        '                        objPrescription.RxPharmacy.PharmacyName = areader.ReadInnerXml
    '        '                    Case "SPI"
    '        '                        objPrescription.RxPrescriber.PrescriberID = areader.ReadInnerXml
    '        '                    Case "DrugDescription"
    '        '                        oDrug.DrugName = areader.ReadInnerXml
    '        '                    Case "Strength"
    '        '                        oDrug.DrugStrength = areader.ReadInnerXml
    '        '                    Case "StrengthUnits"

    '        '                    Case "Refills"
    '        '                        blnIsRefillsQualifier = True
    '        '                    Case "Value"
    '        '                        oDrug.DrugQuantity = areader.ReadInnerXml
    '        '                    Case "Quantity"
    '        '                        oDrug.RefillQuantity = areader.ReadInnerXml
    '        '                    Case "Qualifier"
    '        '                        If blnIsRefillsQualifier = False Then
    '        '                            oDrug.DrugQuantityQualifier = areader.ReadInnerXml
    '        '                        Else
    '        '                            oDrug.RefillsQualifier = areader.ReadInnerXml
    '        '                        End If
    '        '                    Case "Substitutions"
    '        '                        oDrug.MaySubstitute = GetSubstitutionCode(areader.ReadInnerXml)
    '        '                    Case "WrittenDate"
    '        '                        oDrug.WrittenDate = areader.ReadInnerXml
    '        '                End Select

    '        '            End If
    '        '        End While
    '        '        oDrug.MessageName = "RefillRequest"

    '        '        'Validate the RefillRequest message
    '        '        Dim oError As New SureScriptErrorMessage
    '        '        oError.MessageFrom = oDrug.MessageTo
    '        '        oError.MessageTo = oDrug.MessageFrom
    '        '        oError.RelatesToMessageId = oDrug.MessageID

    '        '        If ValidateMessageHeader(CType(oDrug, SureScriptMessage), oError, True) Then
    '        '            If ValidateRefillRequest(objPrescription, 0, oError) Then
    '        '                If Not IsNothing(oDrug) Then
    '        '                    objPrescription.DrugsCol.Add(oDrug)
    '        '                End If
    '        '                Return True
    '        '            Else
    '        '                'generate error message and reject the refill request
    '        '                GenerateErrorMessage(oError)
    '        '                Return False
    '        '            End If
    '        '        Else
    '        '            GenerateErrorMessage(oError)
    '        '            Return False
    '        '        End If

    '        '    Catch ex As gloSurescriptDBException
    '        '        Throw ex
    '        '    Catch ex As GloSurescriptException
    '        '        Throw ex
    '        '    Catch ex As Exception
    '        '        Throw New GloSurescriptException(ex.Message)
    '        '    End Try




    '        Dim ds As New DataSet
    '        Dim oDrug As EDrug

    '        Try
    '            ds.ReadXml(XMLFilePath, XmlReadMode.InferSchema)
    '            If ds.Tables.Count > 0 Then
    '                For icnt As Int32 = 0 To ds.Tables(0).Rows.Count - 1
    '                    oDrug = New EDrug

    '                    oDrug.MessageFrom = ds.Tables(0).Rows(icnt)("sMessageFrom")

    '                    oDrug.MessageTo = "mailto:" & ds.Tables(0).Rows(icnt)("sMessageTo") & ":spi@surescripts.com"

    '                    oDrug.MessageID = ds.Tables(0).Rows(icnt)("nMsgID")

    '                    oDrug.RelatesToMessageId = ds.Tables(0).Rows(icnt)("sMessageID")

    '                    oDrug.DateTimeStamp = ds.Tables(0).Rows(icnt)("dtSentTime")
    '                    oDrug.DateReceived = Date.Now

    '                    oDrug.RxReferenceNumber = ds.Tables(0).Rows(icnt)("sRxReference")
    '                    objPrescription.RxReferenceNumber = oDrug.RxReferenceNumber
    '                    oDrug.TransactionID = oDrug.RxReferenceNumber

    '                    objPrescription.RxTransactionID = ds.Tables(0).Rows(icnt)("sPrescriberOrder")

    '                    objPrescription.RxPrescriber.PrescriberName.LastName = ds.Tables(0).Rows(icnt)("sPrescriberLastName")
    '                    objPrescription.RxPharmacy.PharmacyID = ds.Tables(0).Rows(icnt)("sPharmacyID")

    '                    objPrescription.RxPharmacy.PharmacyName = ds.Tables(0).Rows(icnt)("sPharmacyName")
    '                    objPrescription.RxPharmacy.PharmacyAddress.Address1 = ds.Tables(0).Rows(icnt)("sPharmacyAddress")
    '                    objPrescription.RxPharmacy.PharmacyAddress.City = ds.Tables(0).Rows(icnt)("sPharmacyCity")
    '                    objPrescription.RxPharmacy.PharmacyAddress.State = ds.Tables(0).Rows(icnt)("sPharmacyState")
    '                    objPrescription.RxPharmacy.PharmacyAddress.Zip = ds.Tables(0).Rows(icnt)("sPharmacyZipCode")

    '                    objPrescription.RxPrescriber.PrescriberID = ds.Tables(0).Rows(icnt)("sSPI")

    '                    If (ds.Tables(0).Columns.Contains("sDrugName")) = True Then
    '                        oDrug.DrugName = ds.Tables(0).Rows(icnt)("sDrugName")

    '                    End If
    '                    If (ds.Tables(0).Columns.Contains("sDrugStrength")) = True Then
    '                        oDrug.DrugStrength = ds.Tables(0).Rows(icnt)("sDrugStrength")
    '                        oDrug.Dosage = oDrug.DrugStrength
    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("sDrugQuantity")) = True Then
    '                        oDrug.DrugQuantity = ds.Tables(0).Rows(icnt)("sDrugQuantity")
    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("sRefillQuantity")) = True Then
    '                        oDrug.RefillQuantity = ds.Tables(0).Rows(icnt)("sRefillQuantity")

    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("sDrugStrengthUnits")) = True Then
    '                        oDrug.DrugQuantityQualifier = ds.Tables(0).Rows(icnt)("sDrugStrengthUnits")
    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("sRefillsQualifier")) = True Then
    '                        oDrug.RefillsQualifier = ds.Tables(0).Rows(icnt)("sRefillsQualifier")
    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("bIsSubstituitons")) = True Then
    '                        oDrug.MaySubstitute = GetSubstitutionCode(ds.Tables(0).Rows(icnt)("bIsSubstituitons"))
    '                    End If

    '                    If (ds.Tables(0).Columns.Contains("dtWrittenDate")) = True Then
    '                        oDrug.WrittenDate = ds.Tables(0).Rows(icnt)("dtWrittenDate")
    '                    End If

    '                    oDrug.MessageName = "RefillRequest"
    '                    objPrescription.DrugsCol.Add(oDrug)
    '                    'Validate the RefillRequest message
    '                    Dim oError As New SureScriptErrorMessage
    '                    oError.MessageFrom = oDrug.MessageTo
    '                    oError.MessageTo = oDrug.MessageFrom
    '                    oError.RelatesToMessageId = oDrug.MessageID

    '                    If ValidateMessageHeader(CType(oDrug, SureScriptMessage), oError, True) Then
    '                        If ValidateRefillRequest(objPrescription, 0, oError) Then
    '                            If Not IsNothing(oDrug) Then
    '                                objPrescription.DrugsCol.Add(oDrug)
    '                            End If
    '                            Return True
    '                        Else
    '                            'generate error message and reject the refill request
    '                            GenerateErrorMessage(oError)
    '                            Return False
    '                        End If
    '                    Else
    '                        GenerateErrorMessage(oError)
    '                        Return False
    '                    End If
    '                Next
    '            End If

    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '    
    '    ''' <summary>
    '    ''' Function not in use currently as we have not implemented the 
    '    ''' Cancel Rx functionality
    '    ''' </summary>
    '    ''' <param name="objMessage"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function ReadCancelRxResponse(ByVal objMessage As SureScriptResponseMessage) As Boolean
    '        Dim areader As XmlReader
    '        Dim strfilepath As String = gloSurescriptGeneral.RootPath & "\CancelRxResponseDenied.xml"
    '        Try
    '            areader = XmlReader.Create(strfilepath)
    '            While areader.Read

    '                Dim blnIsRefillsQualifier As Boolean = False

    '                If areader.NodeType = XmlNodeType.Element Then
    '                    Select Case areader.Name

    '                        'this is the message id for the Refillrequest that shall go in the MessageId field
    '                        Case "From"
    '                            objMessage.MessageFrom = areader.ReadInnerXml
    '                        Case "To"
    '                            objMessage.MessageTo = areader.ReadInnerXml
    '                        Case "MessageID"
    '                            objMessage.MessageID = areader.ReadInnerXml
    '                        Case "RelatesToMessageID"
    '                            objMessage.RelatesToMessageId = areader.ReadInnerXml
    '                        Case "SentTime"
    '                            objMessage.DateTimeStamp = areader.ReadInnerXml
    '                            objMessage.DateReceived = Date.Now
    '                        Case "Approved"
    '                            objMessage.ApprovalStatus = True
    '                        Case "Denied"
    '                            objMessage.ApprovalStatus = False
    '                        Case "DescriptionCode"
    '                            objMessage.Denialcode = areader.ReadInnerXml
    '                        Case "Description"
    '                            objMessage.DenialReason = areader.ReadInnerXml
    '                    End Select

    '                End If
    '            End While
    '            objMessage.MessageName = "CancelRxReponse"
    '            Dim oError As New SureScriptErrorMessage
    '            oError.MessageFrom = objMessage.MessageTo
    '            oError.MessageTo = objMessage.MessageFrom
    '            oError.RelatesToMessageId = objMessage.MessageID

    '            'Check if incoming message is valid or not
    '            If ValidateMessageHeader(CType(objMessage, SureScriptMessage), oError) Then

    '                Dim objSureScriptDBLayer As New gloSureScriptDBLayer
    '                If objSureScriptDBLayer.CallCancelDrug(objMessage) Then
    '                    objSureScriptDBLayer.InsertintoMessageTransaction(CType(objMessage, SureScriptMessage))
    '                End If
    '                objSureScriptDBLayer.InsertResponseTransaction(objMessage, True)
    '                Dim oStatus As New StatusMessage
    '                oStatus.MessageID = 0
    '                oStatus.RelatesToMessageId = objMessage.MessageID
    '                oStatus.MessageFrom = objMessage.MessageTo
    '                oStatus.MessageTo = objMessage.MessageFrom
    '                oStatus.StatusCode = "010" 'Means the message has been accepted but is yet to be processed/verified
    '                oStatus.MessageName = "Status"
    '                oStatus.SMessageType = MessageType.eStatus
    '                oStatus.DateReceived = Date.Now
    '                'Generate status message for Refill Request
    '                GenerateXMLForStatus(oStatus)
    '                objSureScriptDBLayer.InsertAcknowledgements(oStatus, False)

    '                'Insert the details of the Status Message sent in the MessageTransaction
    '                objSureScriptDBLayer.InsertintoMessageTransaction(CType(oStatus, SureScriptMessage))

    '                'generate an error message if it is not a valid message
    '            Else

    '                GenerateErrorMessage(oError)
    '            End If
    '            Return True
    '        Catch ex As gloSurescriptDBException
    '            Throw ex
    '        Catch ex As GloSurescriptException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New GloSurescriptException(ex.Message)
    '        End Try
    '    End Function
    '#End Region

    'Private Function ValidateRefillRequest(ByVal objPrescription As EPrescription, ByVal RefillItem As Int16, ByRef objError As SureScriptErrorMessage) As Boolean

    '    objError.RelatesToMessageId = objPrescription.DrugsCol.Item(RefillItem).MessageID
    '    objError.MessageTo = objPrescription.DrugsCol.Item(RefillItem).MessageFrom
    '    objError.MessageFrom = objPrescription.DrugsCol.Item(RefillItem).MessageTo

    '    Try
    '        If objPrescription.RxReferenceNumber.Trim = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "039"
    '            objError.Description = "UIH initiator reference identifier is invalid"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as RxReference Number not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberID not available")
    '            Exit Function
    '        ElseIf objPrescription.RxTransactionID.Trim = "" Then
    '            gloSurescriptGeneral.UpdateLog("Message could not be read as PrescriberOrder Number not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberOrder Number not available")

    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "038"
    '            objError.Description = "UIH initiator reference is invalid"
    '            Exit Function
    '        ElseIf objPrescription.RxPharmacy.PharmacyID.Trim = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "001"
    '            objError.Description = "Sender ID not on file"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyID not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyID not available")
    '            Exit Function
    '        ElseIf objPrescription.RxPharmacy.PharmacyName.Trim = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "000"
    '            objError.Description = "Pharmacy Name Invalid"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyName not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyName not available")
    '            Exit Function
    '        ElseIf objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "000"
    '            objError.Description = "Pharmacy Address1 Invalid"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as Pharmacy Address not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not read as Pharmacy Address not available")
    '            Exit Function
    '        ElseIf objPrescription.RxPharmacy.PharmacyAddress.City.Trim = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "000"
    '            objError.Description = "Pharmacy City Invalid"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as Pharmacy City not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not read as Pharmacy City not available")
    '            Exit Function
    '        ElseIf objPrescription.RxPharmacy.PharmacyAddress.State.Trim = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "000"
    '            objError.Description = "Pharmacy State Invalid"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as Pharmacy State not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not read as Pharmacy State not available")
    '            Exit Function
    '        ElseIf objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "000"
    '            objError.Description = "Pharmacy Zip Invalid"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as Pharmacy Zipcode not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not read as Pharmacy Zipcode not available")
    '            Exit Function
    '        ElseIf objPrescription.RxPrescriber.PrescriberID.Trim = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "002"
    '            objError.Description = "Receiver ID not on file"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as PrescriberID not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberID not available")
    '            Exit Function
    '        ElseIf objPrescription.RxPrescriber.PrescriberName.LastName.Trim = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "059"
    '            objError.Description = "PVD prescriber last name is invalid"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as LastName not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not read as LastName not available")
    '            Exit Function
    '        ElseIf objPrescription.DrugsCol.Item(RefillItem).DrugName = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "126"
    '            objError.Description = "DRU drug name is invalid"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as DrugName not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not be read as DrugName not available")
    '            Exit Function
    '            'ElseIf objPrescription.DrugsCol.Item(RefillItem).Dosage = "" Then
    '            '    objError.ErrorCode = "900"
    '            '    objError.DescriptionCode = "130"
    '            '    objError.Description = "DRU Drug Strength is invalid"

    '            '    gloSurescriptGeneral.UpdateLog("Message could not be read as Drug Dosage not available")
    '            '    gloSurescriptGeneral.InformationMessage("Message could not be read as Drug Dosage not available")
    '            '    Exit Function
    '            'ElseIf objPrescription.DrugsCol.Item(RefillItem).Drugform = "" Then
    '            '    gloSurescriptGeneral.UpdateLog("Message could not be read as Drug Form not available")
    '            '    gloSurescriptGeneral.InformationMessage("Message could not be read as Drug Form not available")
    '            '    Exit Function
    '        ElseIf objPrescription.DrugsCol.Item(RefillItem).DrugQuantity = "" Then
    '            objError.ErrorCode = "900"
    '            objError.DescriptionCode = "135"
    '            objError.Description = "DRU Dosage Quantity is invalid"

    '            gloSurescriptGeneral.UpdateLog("Message could not be read as Drug Quantity not available")
    '            gloSurescriptGeneral.InformationMessage("Message could not be read as Drug Quantity not available")
    '            Exit Function
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Throw New GloSurescriptException(ex.Message)
    '    End Try
    'End Function
#End Region

End Class
