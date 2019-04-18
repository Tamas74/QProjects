Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports RxSniffer.eRXStag
Imports System.ComponentModel
Imports RxSniffer.RxGeneral
Imports UnZipFile
Imports System.Threading
Imports System.Data
Imports Microsoft.Win32
Imports System.ServiceModel
Imports System.Text
Imports System.Linq
Imports RxSniffer.gloSecureMessage
Imports System.Xml.Serialization
Imports RxSniffer.gloDatabaseLayer
Imports gloSettings
Imports System.Windows.Forms
Imports Ionic.Zip
Imports gloSureScript
Imports System.Globalization
Imports Schema = gloGlobal.Schemas.Surescript



Public Class clsRxSniffer
    Dim myRxService As eRxMessage
    Dim myRxServiceProd As eRXProd.eRxMessage
    Dim myRxWCFService As eRxWCFStaging.IeRxClient
    Dim mygloDirectService As gloDirectStaging.IgloDirectClient
    Dim strConnection As String
    Dim strgloServiceConnection As String
    Dim objCon As SqlConnection
    Dim objcmd As SqlCommand
    Dim strSQL As String
    Dim sFrom As String = ""
    Dim sTo As String = ""
    'Dim _strServiceConnection As String


    Public Property ZipSourceFilePath() As String
        Get
            Return _ZipSourceFilePath
        End Get
        Set(value As String)
            _ZipSourceFilePath = value
        End Set
    End Property

    Private _ZipSourceFilePath As String


    Private Function RetrieveeRx(ByVal strConnection As String) As DataTable
        objCon = New SqlConnection(strConnection)
        Dim DataCommand As SqlCommand = Nothing
        Dim DataAdapter As SqlDataAdapter = Nothing
        Dim objeRxTable = New DataTable
        Try
            DataCommand = New SqlCommand("gsp_GetERxWithoutInternet", objCon)
            DataCommand.CommandType = CommandType.StoredProcedure
            DataAdapter = New SqlDataAdapter(DataCommand)
            DataAdapter.Fill(objeRxTable)

            Return objeRxTable


        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return Nothing
        Finally
            If DataAdapter IsNot Nothing Then
                DataAdapter.Dispose()
                DataAdapter = Nothing
            End If
            If DataCommand IsNot Nothing Then
                DataCommand.Dispose()
                DataCommand = Nothing
            End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Private Function RetrieveSecureMessageStagging(ByVal strConnection As String) As DataSet
        objCon = New SqlConnection(strConnection)
        Dim DataCommand As SqlCommand = Nothing
        Dim DataAdapter As SqlDataAdapter = Nothing
        Dim messageDataset As DataSet = Nothing
        Try
            DataCommand = New SqlCommand("SureScriptsGetFromDBtoXML", objCon)
            DataCommand.CommandType = CommandType.StoredProcedure
            DataAdapter = New SqlDataAdapter(DataCommand)
            messageDataset = New DataSet
            DataAdapter.Fill(messageDataset)

            messageDataset.Tables(0).TableName = "MessagesDataTable"
            messageDataset.Tables(1).TableName = "AttachmentsDataTable"

            Return messageDataset


        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString)
            Return messageDataset
        Finally
            If DataAdapter IsNot Nothing Then
                DataAdapter.Dispose()
                DataAdapter = Nothing
            End If
            If DataCommand IsNot Nothing Then
                DataCommand.Dispose()
                DataCommand = Nothing
            End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function


    Private Function RetrieveQueuedMessages(ByVal strConnection As String) As DataSet
        objCon = New SqlConnection(strConnection)
        Dim DataCommand As SqlCommand = Nothing
        Dim DataAdapter As SqlDataAdapter = Nothing
        Dim messageDataset As DataSet = Nothing
        Try
            DataCommand = New SqlCommand("GetQueuedMessageFromPortal", objCon)
            DataCommand.CommandType = CommandType.StoredProcedure
            DataAdapter = New SqlDataAdapter(DataCommand)
            messageDataset = New DataSet
            DataAdapter.Fill(messageDataset)
            messageDataset.Tables(0).TableName = "MessagesDataTable"
            Return messageDataset
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return messageDataset
        Finally
            If DataAdapter IsNot Nothing Then
                DataAdapter.Dispose()
                DataAdapter = Nothing
            End If

            If DataCommand IsNot Nothing Then
                DataCommand.Dispose()
                DataCommand = Nothing
            End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Private Function GetSecureMessageDetailsUsingMessageID(ByVal sMessageID As String, ByVal strConnection As String) As DataSet
        objCon = New SqlConnection(strConnection)
        Dim DataCommand As SqlCommand = Nothing
        Dim DataAdapter As SqlDataAdapter = Nothing
        Dim messageDataset As DataSet = Nothing
        Dim oParam As SqlParameter = Nothing
        Try
            DataCommand = New SqlCommand("GetSecureMessageDetailsUsingMessageID", objCon)
            DataCommand.CommandType = CommandType.StoredProcedure
            oParam = New SqlParameter()
            oParam.ParameterName = "@MessageID"
            oParam.DbType = DbType.String
            oParam.Direction = ParameterDirection.Input
            oParam.Value = sMessageID
            DataCommand.Parameters.Add(oParam)
            DataAdapter = New SqlDataAdapter(DataCommand)
            messageDataset = New DataSet
            DataAdapter.Fill(messageDataset)
            messageDataset.Tables(0).TableName = "MessagesDataTable"
            Return messageDataset
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return messageDataset
        Finally
            If DataAdapter IsNot Nothing Then
                DataAdapter.Dispose()
                DataAdapter = Nothing
            End If

            If DataCommand IsNot Nothing Then
                DataCommand.Dispose()
                DataCommand = Nothing
            End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Private Function PopulateSecureMessage(ByVal Element As DataRow) As SecureMessage
        Dim SecureMessage As SecureMessage = Nothing
        Try
            SecureMessage = New SecureMessage
            With SecureMessage
                .secureMessageInboxID = Element("nSecureMessageInboxID")
                .messageID = Element("sMessageID")
                .relateMessageID = Element("sRelatesToMessageID")
                .version = Element("sMessageVersionNo")
                .release = Element("sMessageReleaseNo")
                .highVersion = Element("sMessageHighestVersion")

                .senderID = Element("nSenderID")
                .receiverID = Element("nReceiverID")
                .From = Element("sFrom")
                .FromQualifier = Element("sFromQualifier")

                .To = Element("sTo")
                .ToQualifier = Element("sToQualifier")
                .subject = Element("sSubject")
                .messageBody = Element("sMessageBody")

                .dateTimeUTC = Element("dtSendReceiveDateTime_UTC")
                .dateTimeNormal = Element("dtSendReceiveDateTime")
                .isRead = Element("bIsRead")
                .patientID = Element("nPatientID")

                .noofAttachements = Element("nNoOfAttachments")
                .MessageStatus = Element("bMessageStatus")
                .messageType = Element("bMessageType")
                .associated = Element("bIsAssociated")

                .deliveryStatusCode = Element("sDeliveryStatusCode")
                .deliveryStatusDescription = Element("sDeliveryStatusDescription")
                .softwareVersion = Element("sSoftwareVersion")
                .softwareProduct = Element("sSoftwareProduct")

                .companyName = Element("sCompanyName")
                .userName = Element("sUserName")
                .machineName = Element("sMachineName")
                .deleted = Element("bIsDeleted")
            End With

            Return SecureMessage
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return Nothing
        Finally

        End Try

    End Function

    Private Function PopulateAttachment(ByVal Element As DataRow) As Attachment

        Dim Attachment As Attachment = Nothing
        Try
            Attachment = New Attachment

            With Attachment
                .nSecureMessageInboxID = Element("nSecureMessageInboxID")
                .attachmentID = Element("nAttachmentID")
                .moduleName = Element("nModuleName")
                .fileExtension = Element("nFileExtension")
                .documentName = Element("sDocumentName")
                .iContent = Element("iContent")
                ''.sCDAConfidentiality = Element("sCDAConfidentiality")
            End With

            Return Attachment

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString)
            Return Nothing
        Finally

        End Try

    End Function


    Public Function ImageToBase64(image As Drawing.Image, format As System.Drawing.Imaging.ImageFormat) As String
        Using ms As New MemoryStream()
            ' Convert Image to byte[]
            image.Save(ms, format)
            Dim imageBytes As Byte() = ms.ToArray()

            ' Convert byte[] to Base64 String
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function

    Private Function GenerateXML(objSecureMessage As SecureMessage, oLsAttach As List(Of Attachment)) As Byte()
        Dim NewN2NMessage As N2NMessageType = Nothing
        Dim byteArray As Byte() = Nothing

        Try
            NewN2NMessage = CreateN2NMessage(objSecureMessage, oLsAttach)
            byteArray = GenerateXML(NewN2NMessage)
            Return byteArray
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString)
            Return byteArray
        Finally
            byteArray = Nothing
            If NewN2NMessage IsNot Nothing Then
                NewN2NMessage = Nothing
            End If
        End Try

        Return byteArray

    End Function

    Private Function GenerateXML(message As N2NMessageType) As Byte()
        Dim byteArray As Byte() = Nothing
        Dim xs As XmlSerializer = Nothing
        Dim fs As FileStream = Nothing

        Try
            Dim strFileName As String = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
            xs = New XmlSerializer(GetType(N2NMessageType))
            fs = New FileStream(strFileName, FileMode.Create)
            xs.Serialize(fs, message)
            fs.Close()

            byteArray = System.IO.File.ReadAllBytes(strFileName)
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
        Finally
            If fs IsNot Nothing Then
                fs.Dispose()
                fs = Nothing
            End If
            If xs IsNot Nothing Then
                xs = Nothing
            End If
        End Try

        Return byteArray



    End Function

    Public Shared Function GetFileName(strAppPath As [String]) As [String]
        Try

            'clsException.UpdateLog("start GetFileName", LogFilePath, EnableLog);
            Dim _NewDocumentName As String = ""

            Dim _Extension As String = ".xml"
            Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            Dim i As Integer = 0
            _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") & _Extension
            While File.Exists(Convert.ToString(strAppPath) & "\" & _NewDocumentName) = True And i <= 32767
                i = i + 1

                _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") & "-" & i & _Extension
            End While
            'clsException.UpdateLog("End GetFileName", LogFilePath, EnableLog);
            Return Convert.ToString(strAppPath) & _NewDocumentName
        Catch ex As Exception
            'System.Windows.MessageBox.Show(ex.ToString())
            Return ""

        Finally
        End Try
    End Function
    Public Shared Function GetFileNameOnly(strAppPath As [String]) As [String]
        Try

            'clsException.UpdateLog("start GetFileName", LogFilePath, EnableLog);
            Dim _NewDocumentName As String = ""

            Dim _Extension As String = ".xml"
            Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            Dim i As Integer = 0
            _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") & _Extension
            While File.Exists(Convert.ToString(strAppPath) & "\" & _NewDocumentName) = True And i <= 32767
                i = i + 1

                _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") & "-" & i & _Extension
            End While
            'clsException.UpdateLog("End GetFileName", LogFilePath, EnableLog);
            Return _NewDocumentName
        Catch ex As Exception
            'System.Windows.MessageBox.Show(ex.ToString())
            Return ""

        Finally
        End Try
    End Function

    Private Function CreateN2NMessage(objSecureMessage As SecureMessage, oLsAttach As List(Of Attachment)) As N2NMessageType

        Dim _messageContent As N2NMessageType = Nothing
        Dim _messageHeader As N2NHeaderType = Nothing
        Dim _messageBody As N2NBodyType = Nothing
        Dim _messageTo As N2NAddressType = Nothing
        Dim _messageFrom As N2NAddressType = Nothing
        Dim _senderDetails As SenderSoftwareType = Nothing
        Dim _messageClinical As ClinicalMessageType = Nothing
        Dim _documentType As DocumentType = Nothing
        Dim _attachmentType As AttachmentType() = Nothing
        Dim _attachment As AttachmentType = Nothing
        Dim _file As FileType = Nothing

        Try
            _messageContent = New N2NMessageType()
            _messageHeader = New N2NHeaderType()
            _messageBody = New N2NBodyType()
            _messageTo = New N2NAddressType()
            _messageFrom = New N2NAddressType()
            _senderDetails = New SenderSoftwareType()
            _messageClinical = New ClinicalMessageType()
            _documentType = New DocumentType()
            _attachmentType = New AttachmentType(0) {}
            _attachment = New AttachmentType()
            _file = New FileType()

            Dim dtdate As DateTime = System.DateTime.UtcNow
            Dim strdate As String = dtdate.ToString("yyyy-MM-dd")
            Dim strtime As String = dtdate.ToString("hh:mm:ss")
            Dim utc As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, _
             DateTime.Now.Millisecond, DateTimeKind.Utc)
            Dim strUTCFormat As String = dtdate.Year.ToString() & "-" & "0" & dtdate.Month.ToString() & "-" & dtdate.Day.ToString() & "T" & dtdate.Hour.ToString() & ":" & dtdate.Minute.ToString() & ":" & dtdate.Second.ToString() & ".0Z"
            strUTCFormat = strdate & "T" & strtime & ":0Z"

            _messageContent.release = "006"
            _messageContent.version = "010"

            _messageTo.Qualifier = "DIRECT"
            _messageTo.Value = objSecureMessage.[To]

            _messageFrom.Qualifier = "DIRECT"
            _messageFrom.Value = objSecureMessage.From

            'Header
            _messageHeader.[To] = _messageTo
            _messageHeader.From = _messageFrom
            _messageHeader.MessageID = objSecureMessage.messageID
            _messageHeader.RelatesToMessageID = ""
            _messageHeader.SentTime = utc
            _messageHeader.TestMessage = "1"

            'Added Use Case in Header
            If objSecureMessage.UseCase = 1 Then
                _messageHeader.UseCase = "ADVSAVE"
                _messageHeader.RelatesToMessageID = objSecureMessage.relateMessageID
            End If


            'Sender Software Developer
            _senderDetails.SenderSoftwareDeveloper = objSecureMessage.companyName
            _senderDetails.SenderSoftwareProduct = objSecureMessage.softwareProduct
            _senderDetails.SenderSoftwareVersionRelease = objSecureMessage.softwareVersion
            _messageHeader.SenderSoftware = _senderDetails
            _messageContent.Header = _messageHeader

            'Document Type
            _documentType.PlainText = objSecureMessage.messageBody

            ' Attachments
            If Not IsNothing(oLsAttach) Then

                If oLsAttach.Count > 0 Then

                    _attachmentType = New AttachmentType(oLsAttach.Count - 1) {}
                    For i As Integer = 0 To oLsAttach.Count - 1
                        _attachment = New AttachmentType()
                        _file = New FileType()

                        'File Type
                        _file.DocumentData = oLsAttach(i).base64
                        _file.DocumentType = GetMimeType(oLsAttach(i).fileExtension)

                        'Attachment
                        _attachment.DocumentName = oLsAttach(i).documentName
                        _attachment.Item = _file
                        _attachmentType(i) = _attachment

                        '_attachmentType(i).DocumentName = oLsAttach(i).documentName
                        '_attachmentType(i).Item = _file

                    Next

                    _messageClinical.Attachment = _attachmentType
                End If
            End If

            'clinical message
            _messageClinical.Document = _documentType
            _messageClinical.Subject = objSecureMessage.subject

            'Body
            _messageBody.Item = _messageClinical
            _messageContent.Body = _messageBody

            Return _messageContent
        Catch ex As Exception
            'System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.[Error], MessageBoxResult.OK)
            Return _messageContent
        Finally

            If _file IsNot Nothing Then
                _file = Nothing
            End If

            If _attachment IsNot Nothing Then
                _attachment = Nothing
            End If

            If _attachmentType IsNot Nothing Then
                _attachmentType = Nothing
            End If

            If _documentType IsNot Nothing Then
                _documentType = Nothing
            End If

            If _messageClinical IsNot Nothing Then
                _messageClinical = Nothing
            End If

            If _senderDetails IsNot Nothing Then
                _senderDetails = Nothing
            End If

            If _messageFrom IsNot Nothing Then
                _messageFrom = Nothing
            End If

            If _messageTo IsNot Nothing Then
                _messageTo = Nothing
            End If

            If _messageBody IsNot Nothing Then
                _messageBody = Nothing
            End If

            If _messageHeader IsNot Nothing Then
                _messageHeader = Nothing
            End If

            If _messageContent IsNot Nothing Then
                _messageContent = Nothing

            End If
        End Try

    End Function

    Private Function GetMimeType(extension As Int16) As String
        Dim documentType As String = String.Empty

        Select Case extension
            Case 3
                If True Then
                    documentType = "application/xml"
                    Exit Select
                End If
            Case 0
                If True Then
                    documentType = "application/pdf"
                    Exit Select
                End If

            Case 1
                If True Then
                    documentType = "application/msword"
                    Exit Select
                End If
            Case 2
                If True Then
                    documentType = "application/zip"
                    Exit Select
                End If
            Case 6
                If True Then
                    documentType = "text/html"
                    Exit Select
                End If

            Case 4
                If True Then
                    documentType = "text/plain"
                    Exit Select
                End If
            Case 5
                If True Then
                    documentType = "text/RTF"
                    Exit Select
                End If
            Case Else

                Exit Select

        End Select

        Return documentType
    End Function

    Private Function ExtractXML(objN2N As N2NMessageType, objMessage As SecureMessage) As SecureMessage
        objMessage.release = objN2N.release
        objMessage.version = objN2N.version

        objMessage.ToQualifier = objN2N.Header.[To].Qualifier
        objMessage.[To] = objN2N.Header.[To].Value

        objMessage.FromQualifier = objN2N.Header.From.Qualifier
        objMessage.From = objN2N.Header.From.Value

        ' objMessage.messageID = objN2N.Header.MessageID;
        objMessage.relateMessageID = objN2N.Header.MessageID
        objMessage.dateUTC = objN2N.Header.SentTime

        objMessage.companyName = objN2N.Header.SenderSoftware.SenderSoftwareDeveloper
        objMessage.softwareProduct = objN2N.Header.SenderSoftware.SenderSoftwareProduct
        objMessage.softwareVersion = objN2N.Header.SenderSoftware.SenderSoftwareVersionRelease

        If Convert.ToString(objN2N.Body.Item) = "RxSniffer.Error" Then
            Dim _messagestatus As [Error] = Nothing
            _messagestatus = DirectCast(objN2N.Body.Item, [Error])

            objMessage.deliveryStatusCode = _messagestatus.Code
            objMessage.deliveryStatusDescription = _messagestatus.Description
        Else
            Dim _messagestatus As Status = Nothing
            _messagestatus = DirectCast(objN2N.Body.Item, Status)

            objMessage.deliveryStatusCode = _messagestatus.Code
            objMessage.deliveryStatusDescription = _messagestatus.Description
        End If
        Return objMessage
    End Function

    'Private Sub GenerateXML(ByVal objSecureMessage As SecureMessage, ByVal AttachmentList As List(Of Attachment))

    '    Dim xmlStringBuilder As New StringBuilder()

    '    Dim xmlSettings As XmlWriterSettings = New XmlWriterSettings()
    '    xmlSettings.Indent = True
    '    xmlSettings.NamespaceHandling = False

    '    Using XmlWriter As XmlWriter = XmlWriter.Create(xmlStringBuilder, xmlSettings)

    '        With XmlWriter
    '            .WriteStartElement("N2NMessage", "http://www.surescripts.com/messaging")

    '            .WriteAttributeString("release", objSecureMessage.release)
    '            .WriteAttributeString("version", objSecureMessage.version)
    '            .WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

    '            '--------------------------HEADER-------------------------------------------
    '            .WriteStartElement("Header")


    '            .WriteStartElement("To")
    '            .WriteAttributeString("Qualifier", objSecureMessage.ToQualifier)
    '            .WriteString(objSecureMessage.To)
    '            .WriteEndElement()


    '            .WriteStartElement("From")
    '            .WriteAttributeString("Qualifier", objSecureMessage.FromQualifier)
    '            .WriteString(objSecureMessage.From)
    '            .WriteEndElement()

    '            .WriteElementString("MessageID", objSecureMessage.messageID)

    '            .WriteElementString("RelatesToMessageID", objSecureMessage.relateMessageID)
    '            .WriteElementString("SentTime", objSecureMessage.dateTimeUTC)

    '            .WriteStartElement("SenderSoftware")
    '            .WriteElementString("SenderSoftwareDeveloper", objSecureMessage.companyName)
    '            .WriteElementString("SenderSoftwareProduct", objSecureMessage.softwareProduct)
    '            .WriteElementString("SenderSoftwareVersionRelease", objSecureMessage.softwareVersion)
    '            .WriteEndElement()

    '            .WriteEndElement() '--------Header End element------------------------------
    '            '--------------------------HEADER-------------------------------------------


    '            '--------------------------BODY---------------------------------------------
    '            .WriteStartElement("Body")
    '            .WriteStartElement("ClinicalMessage")
    '            .WriteElementString("Subject", objSecureMessage.subject)

    '            .WriteStartElement("Document")
    '            .WriteElementString("PlainText", objSecureMessage.messageBody)

    '            .WriteEndElement()

    '            '--------------------------ATTACHMENTS--------------------------------------
    '            If AttachmentList.Count > 0 Then

    '                For Each AttachmentElement As Attachment In AttachmentList

    '                    .WriteStartElement("Attachment")
    '                    .WriteElementString("DocumentName", AttachmentElement.documentName)
    '                    .WriteStartElement("File")
    '                    .WriteElementString("DocumentType", AttachmentElement.mimeType)
    '                    .WriteElementString("DocumentData", Convert.ToBase64String(AttachmentElement.iContent))

    '                    .WriteEndElement()
    '                    .WriteEndElement()
    '                Next

    '            End If
    '            '--------------------------ATTACHMENTS--------------------------------------

    '            .WriteEndElement()
    '            '--------------------------BODY---------------------------------------------
    '        End With
    '    End Using

    '    Console.WriteLine("-------------------------------------")
    '    Console.WriteLine(xmlStringBuilder.ToString)

    '    xmlStringBuilder = Nothing
    '    xmlSettings = Nothing

    'End Sub


    Private Function RetrieveProividers() As String
        objCon = New SqlConnection(strConnection)
        objcmd = New SqlCommand
        Dim drReader As SqlDataReader = Nothing

        Try
            Dim strPrescriber As String = ""
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.Text
            strSQL = "select nProviderId as ProviderID,isnull(sFirstName,'') + space(1) + isnull(sMiddlename,'') +space(1) + isnull(slastname,'') as ProviderName,isnull(sFirstName,'') as ProviderFirstName ,isnull(sLastName,'') as ProviderLastName,isnull(sAddress,'') + space(1) + isnull(sStreet,'') as Address, isnull(sCity,'') as City,isnull(sstate,'') as State,isnull(szip,'') as Zip,isnull(sphoneno,'') as Phone,isnull(sSPIID,'') as PrescriberID from Provider_Mst where bIsblocked <> 'True' "
            objcmd.CommandText = strSQL
            objCon.Open()
            drReader = objcmd.ExecuteReader
            If Not IsDBNull(drReader) Then
                If drReader.HasRows Then

                    While drReader.Read
                        If Not IsDBNull(drReader.Item("PrescriberID")) And drReader.Item("PrescriberID").ToString.Trim <> "" Then
                            If strPrescriber <> "" Then
                                strPrescriber &= "|" & drReader.Item("PrescriberID").ToString
                            Else
                                strPrescriber &= drReader.Item("PrescriberID").ToString
                            End If

                        End If
                    End While
                End If
                If drReader.IsClosed = False Then
                    drReader.Close()
                End If
            End If

            Return strPrescriber

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return ""
        Finally
            strSQL = String.Empty
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(drReader) Then
                drReader = Nothing
            End If
            If objcmd IsNot Nothing Then
                objcmd.Cancel()
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try

    End Function

    Private Function RetrieveProividers(ByVal strConnection As String) As String
        objCon = New SqlConnection(strConnection)
        objcmd = New SqlCommand
        Dim drReader As SqlDataReader = Nothing

        Try
            Dim strPrescriber As String = ""
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.Text
            strSQL = "select nProviderId as ProviderID,isnull(sFirstName,'') + space(1) + isnull(sMiddlename,'') +space(1) + isnull(slastname,'') as ProviderName,isnull(sFirstName,'') as ProviderFirstName ,isnull(sLastName,'') as ProviderLastName,isnull(sAddress,'') + space(1) + isnull(sStreet,'') as Address, isnull(sCity,'') as City,isnull(sstate,'') as State,isnull(szip,'') as Zip,isnull(sphoneno,'') as Phone,isnull(sSPIID,'') as PrescriberID from Provider_Mst where bIsblocked <> 'True' "
            objcmd.CommandText = strSQL
            objCon.Open()
            drReader = objcmd.ExecuteReader
            If Not IsDBNull(drReader) Then
                If drReader.HasRows Then

                    While drReader.Read
                        If Not IsDBNull(drReader.Item("PrescriberID")) And drReader.Item("PrescriberID").ToString.Trim <> "" Then
                            If strPrescriber <> "" Then
                                strPrescriber &= "|" & drReader.Item("PrescriberID").ToString
                            Else
                                strPrescriber &= drReader.Item("PrescriberID").ToString
                            End If

                        End If
                    End While
                End If
                If drReader.IsClosed = False Then
                    drReader.Close()
                End If
            End If

            Return strPrescriber

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return ""
        Finally
            strSQL = String.Empty
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(drReader) Then
                drReader = Nothing
            End If
            If objcmd IsNot Nothing Then
                objcmd.Cancel()
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try

    End Function

    Private Function RetrieveSecureMsgProividers(ByVal strConnection As String) As String
        objCon = New SqlConnection(strConnection)
        objcmd = New SqlCommand
        Dim drReader As SqlDataReader = Nothing
        Try
            Dim strPrescriberDA As String = ""
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.Text
            strSQL = "select nProviderId as ProviderID,isnull(sFirstName,'') + space(1) + isnull(sMiddlename,'') +space(1) + isnull(slastname,'') as ProviderName,isnull(sFirstName,'') as ProviderFirstName ,isnull(sLastName,'') as ProviderLastName,isnull(sDirectAddress,'') as DirectAddress ,isnull(sSPIID,'') as PrescriberID from Provider_Mst where  isnull(sDirectAddress,'')  <> ''"
            objcmd.CommandText = strSQL
            objCon.Open()
            drReader = objcmd.ExecuteReader
            If Not IsDBNull(drReader) Then
                If drReader.HasRows Then

                    While drReader.Read
                        If Not IsDBNull(drReader.Item("DirectAddress")) And drReader.Item("DirectAddress").ToString.Trim <> "" Then
                            If strPrescriberDA <> "" Then
                                strPrescriberDA &= "," & drReader.Item("DirectAddress").ToString
                            Else
                                strPrescriberDA &= drReader.Item("DirectAddress").ToString
                            End If

                        End If
                    End While
                End If
            End If

            Return strPrescriberDA

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return ""
        Finally
            strSQL = String.Empty
            If drReader IsNot Nothing Then
                drReader.Close()
                drReader = Nothing
            End If

            If objcmd IsNot Nothing Then
                objcmd.Cancel()
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If objCon IsNot Nothing Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try

    End Function

    Private Function SaveRxResponses(ByVal dtResponses As DataTable) As String
        Try

            If dtResponses Is Nothing Then
                Return ""
                mdlGeneral.UpdateLog("No Responses from Webserver", strConnection)
            Else
                Dim strMsgTransactions As String = ""
                If dtResponses.Rows.Count > 0 Then
                    Dim objPrescription As EPrescription = Nothing
                    Dim objError As SureScriptErrorMessage = Nothing

                    For Each dtRow As DataRow In dtResponses.Rows
                        If dtRow("sMsgType") = "RefillRequest" Then
                            objPrescription = ReadRefillRxMsg(dtRow)
                            If InsertRefillPrescription(objPrescription) Then
                                If InsertintoMessageTransaction(CType(objPrescription.DrugsCol.Item(0), SureScriptMessage)) Then
                                    If strMsgTransactions <> "" Then
                                        strMsgTransactions &= "|" & dtRow(0).ToString
                                    Else
                                        strMsgTransactions &= dtRow(0).ToString
                                    End If

                                End If
                            End If
                        Else
                            objError = ReadErrorMsg(dtRow)
                            If InsertErrorDetails(objError) Then
                                If InsertintoMessageTransaction(CType(objError, SureScriptMessage)) Then
                                    If strMsgTransactions <> "" Then
                                        strMsgTransactions &= "|" & dtRow(0).ToString
                                    Else
                                        strMsgTransactions &= dtRow(0).ToString
                                    End If
                                End If
                            End If
                        End If
                        If Not IsNothing(objPrescription) Then
                            objPrescription.Dispose()
                            objPrescription = Nothing
                        End If
                        If Not IsNothing(objError) Then
                            objError.Dispose()
                            objError = Nothing
                        End If
                    Next
                Else
                    mdlGeneral.UpdateLog("No New Responses from Surescript", strConnection)
                End If
                Return strMsgTransactions
            End If
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return ""
        End Try
    End Function

    Private Function SaveRxResponses(ByVal dtResponses As DataTable, ByVal strConnection As String) As String
        Try
            globalCurrentDate = gloGlobal.gloTimeZone.getgloDateTime(gloGlobal.gloTimeZone.getLocalTimeZoneId(strConnection))

            If dtResponses Is Nothing Then
                Return ""
                mdlGeneral.UpdateLog("No Responses from Webserver", strConnection)
            Else
                Dim strMsgTransactions As String = ""
                If dtResponses.Rows.Count > 0 Then
                    Dim objPrescription As EPrescription = Nothing
                    Dim objError As SureScriptErrorMessage = Nothing

                    For Each dtRow As DataRow In dtResponses.Rows
                        If dtRow("sMsgType") = "RefillRequest" Then
                            objPrescription = ReadRefillRxMsg(dtRow)
                            If InsertRefillPrescription(objPrescription, strConnection) Then
                                If InsertintoMessageTransaction(CType(objPrescription.DrugsCol.Item(0), SureScriptMessage), strConnection) Then
                                    If strMsgTransactions <> "" Then
                                        strMsgTransactions &= "|" & dtRow(0).ToString
                                    Else
                                        strMsgTransactions &= dtRow(0).ToString
                                    End If
                                End If
                            End If

                        ElseIf Convert.ToString(dtRow("sMsgType")).ToLower() = "cancelrxresponse" Then
                            Me.InsertintoMessageTransaction(dtRow, strConnection)
                            Me.UpdateMedicationStatus(dtRow)

                            If strMsgTransactions <> "" Then
                                strMsgTransactions &= "|" & dtRow(0).ToString
                            Else
                                strMsgTransactions &= dtRow(0).ToString
                            End If

                        ElseIf Convert.ToString(dtRow("sMsgType")).ToLower() = "rxchangerequest" Then
                            Me.InsertintoMessageTransaction(dtRow, strConnection)
                            Me.InsertRxChangeRequestDetails(dtRow, strConnection)

                            If strMsgTransactions <> "" Then
                                strMsgTransactions &= "|" & dtRow(0).ToString
                            Else
                                strMsgTransactions &= dtRow(0).ToString
                            End If

                        ElseIf Convert.ToString(dtRow("sMsgType")).ToLower() = "rxfill" Then
                            Me.InsertintoMessageTransaction(dtRow, strConnection)
                            Me.InsertRxFillRequestDetails(dtRow, strConnection)

                            If strMsgTransactions <> "" Then
                                strMsgTransactions &= "|" & dtRow(0).ToString
                            Else
                                strMsgTransactions &= dtRow(0).ToString
                            End If                        
                        Else
                            If dtRow("sMsgType") = "Error" Then
                                objError = ReadErrorMsg(dtRow)
                                If InsertErrorDetails(objError, strConnection) Then
                                    If InsertintoMessageTransaction(CType(objError, SureScriptMessage), strConnection) Then
                                        If strMsgTransactions <> "" Then
                                            strMsgTransactions &= "|" & dtRow(0).ToString
                                        Else
                                            strMsgTransactions &= dtRow(0).ToString
                                        End If
                                    End If
                                End If
                            Else
                                If strMsgTransactions <> "" Then
                                    strMsgTransactions &= "|" & dtRow(0).ToString
                                Else
                                    strMsgTransactions &= dtRow(0).ToString
                                End If
                            End If
                            ''EPCS Router API Call
                            PostPrescriptionUpdateStatus(dtRow, mdlGeneral.gblnStagingServer, strConnection)
                        End If
                        If Not IsNothing(objPrescription) Then
                            objPrescription.Dispose()
                            objPrescription = Nothing
                        End If
                        If Not IsNothing(objError) Then
                            objError.Dispose()
                            objError = Nothing
                        End If
                    Next
                Else
                    mdlGeneral.UpdateLog("No New Responses from Surescript", strConnection)
                End If
                Return strMsgTransactions
            End If
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return ""
        End Try
    End Function

    Private Function SaveSecureMessage(ByVal dtResponses As DataTable, ByVal strConnection As String) As String
        Try

            If dtResponses Is Nothing Then
                Return ""
                mdlGeneral.UpdateLog("No Responses from Webserver", strConnection)
            Else
                Dim strMsgTransactions As String = ""
                If dtResponses.Rows.Count > 0 Then
                    Dim objPrescription As EPrescription = Nothing
                    Dim objError As SureScriptErrorMessage = Nothing

                    For Each dtRow As DataRow In dtResponses.Rows
                        If dtRow("sMsgType") = "RefillRequest" Then
                            objPrescription = ReadRefillRxMsg(dtRow)
                            If InsertRefillPrescription(objPrescription, strConnection) Then
                                If InsertintoMessageTransaction(CType(objPrescription.DrugsCol.Item(0), SureScriptMessage), strConnection) Then
                                    If strMsgTransactions <> "" Then
                                        strMsgTransactions &= "|" & dtRow(0).ToString
                                    Else
                                        strMsgTransactions &= dtRow(0).ToString
                                    End If
                                End If
                            End If
                        Else
                            objError = ReadErrorMsg(dtRow)
                            If InsertErrorDetails(objError, strConnection) Then
                                If InsertintoMessageTransaction(CType(objError, SureScriptMessage), strConnection) Then
                                    If strMsgTransactions <> "" Then
                                        strMsgTransactions &= "|" & dtRow(0).ToString
                                    Else
                                        strMsgTransactions &= dtRow(0).ToString
                                    End If
                                End If
                            End If
                        End If
                        If Not IsNothing(objPrescription) Then
                            objPrescription.Dispose()
                            objPrescription = Nothing
                        End If
                        If Not IsNothing(objError) Then
                            objError.Dispose()
                            objError = Nothing
                        End If
                    Next
                Else
                    mdlGeneral.UpdateLog("No New Responses from Surescript", strConnection)
                End If
                Return strMsgTransactions
            End If
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return ""
        End Try
    End Function

    Public Function ReadRefillRxMsg(ByVal drRow As DataRow) As EPrescription
        Try

            Dim oDrug As New EDrug
            Dim objPrescription As New EPrescription

            If (drRow.Table.Columns.Contains("sMessageFrom")) = True Then
                If IsDBNull(drRow("sMessageFrom")) = False Then
                    oDrug.MessageFrom = drRow("sMessageFrom")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMessageTo")) = True Then
                If IsDBNull(drRow("sMessageTo")) = False Then
                    oDrug.MessageTo = drRow("sMessageTo")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMessageID")) = True Then
                If IsDBNull(drRow("sMessageID")) = False Then
                    oDrug.MessageID = drRow("sMessageID")
                End If
            End If

            If (drRow.Table.Columns.Contains("sRelativeMessageID")) = True Then
                If IsDBNull(drRow("sRelativeMessageID")) = False Then
                    oDrug.RelatesToMessageId = drRow("sRelativeMessageID")
                End If
            End If

            If (drRow.Table.Columns.Contains("dtSentTime")) = True Then
                If IsDBNull(drRow("dtSentTime")) = False Then                    
                    oDrug.DateTimeStamp = drRow("dtSentTime")
                End If
            End If

            oDrug.DateReceived = globalCurrentDate

            If (drRow.Table.Columns.Contains("sSenderSoftwareVersion")) = True Then
                If IsDBNull(drRow("sSenderSoftwareVersion")) = False Then
                    oDrug.SenderSoftwareVersion = drRow("sSenderSoftwareVersion")
                End If
            End If

            If (drRow.Table.Columns.Contains("SenderSoftwareDeveloper")) = True Then
                If IsDBNull(drRow("SenderSoftwareDeveloper")) = False Then
                    oDrug.SenderSoftwareDeveloper = drRow("SenderSoftwareDeveloper")
                End If
            End If

            If (drRow.Table.Columns.Contains("SenderSoftwareProduct")) = True Then
                If IsDBNull(drRow("SenderSoftwareProduct")) = False Then
                    oDrug.SenderSoftwareProduct = drRow("SenderSoftwareProduct")
                End If
            End If

            If (drRow.Table.Columns.Contains("sRxReference")) = True Then
                If IsDBNull(drRow("sRxReference")) = False Then
                    oDrug.RxReferenceNumber = drRow("sRxReference")
                End If
            End If

            objPrescription.RxReferenceNumber = oDrug.RxReferenceNumber
            oDrug.TransactionID = oDrug.RxReferenceNumber

            If (drRow.Table.Columns.Contains("sPrescriberOrder")) = True Then
                If IsDBNull(drRow("sPrescriberOrder")) = False Then
                    objPrescription.RxTransactionID = drRow("sPrescriberOrder")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacyID")) = True Then
                If IsDBNull(drRow("sPharmacyID")) = False Then
                    objPrescription.RxPharmacy.PharmacyID = drRow("sPharmacyID")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacyName")) = True Then
                If IsDBNull(drRow("sPharmacyName")) = False Then
                    objPrescription.RxPharmacy.Pharmacyname = drRow("sPharmacyName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacyNPI")) = True Then
                If IsDBNull(drRow("sPharmacyNPI")) = False Then
                    objPrescription.RxPharmacy.PharmacyNPI = drRow("sPharmacyNPI")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacyAddress1")) = True Then
                If IsDBNull(drRow("sPharmacyAddress1")) = False Then
                    objPrescription.RxPharmacy.PharmacyAddress.Address1 = drRow("sPharmacyAddress1")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacyAddress2")) = True Then
                If IsDBNull(drRow("sPharmacyAddress2")) = False Then
                    objPrescription.RxPharmacy.PharmacyAddress.Address2 = drRow("sPharmacyAddress2")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacyCity")) = True Then
                If IsDBNull(drRow("sPharmacyCity")) = False Then
                    objPrescription.RxPharmacy.PharmacyAddress.City = drRow("sPharmacyCity")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacyState")) = True Then
                If Not IsDBNull(drRow("sPharmacyState")) Then
                    objPrescription.RxPharmacy.PharmacyAddress.State = drRow("sPharmacyState")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacyZipCode")) = True Then
                If Not IsDBNull(drRow("sPharmacyZipCode")) Then
                    objPrescription.RxPharmacy.PharmacyAddress.Zip = drRow("sPharmacyZipCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacyPhone")) = True Then
                If IsDBNull(drRow("sPharmacyPhone")) = False Then
                    objPrescription.RxPharmacy.PharmacyAddress.Phone = drRow("sPharmacyPhone")
                End If

            End If

            If (drRow.Table.Columns.Contains("sPharmacyPhQualifier")) = True Then
                If IsDBNull(drRow("sPharmacyPhQualifier")) = False Then
                    objPrescription.RxPharmacy.PharmacyAddress.PhQualifier = drRow("sPharmacyPhQualifier")
                End If

            End If

            If (drRow.Table.Columns.Contains("sPharmacyEmail")) = True Then
                If IsDBNull(drRow("sPharmacyEmail")) = False Then
                    objPrescription.RxPharmacy.PharmacyAddress.Email = drRow("sPharmacyEmail")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacyFax")) = True Then
                If IsDBNull(drRow("sPharmacyFax")) = False Then
                    objPrescription.RxPharmacy.PharmacyAddress.Fax = drRow("sPharmacyFax")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacistFirstName")) = True Then
                If IsDBNull(drRow("sPharmacistFirstName")) = False Then
                    objPrescription.RxPharmacy.PharmacistName.FirstName = drRow("sPharmacistFirstName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacistLastName")) = True Then
                If IsDBNull(drRow("sPharmacistLastName")) = False Then
                    objPrescription.RxPharmacy.PharmacistName.LastName = drRow("sPharmacistLastName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacistMiddleName")) = True Then
                If IsDBNull(drRow("sPharmacistMiddleName")) = False Then
                    objPrescription.RxPharmacy.PharmacistName.MiddleName = drRow("sPharmacistMiddleName")
                End If

            End If

            If (drRow.Table.Columns.Contains("sPharmacistSuffix")) = True Then
                If IsDBNull(drRow("sPharmacistSuffix")) = False Then
                    objPrescription.RxPharmacy.PharmacistName.Suffix = drRow("sPharmacistSuffix")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacistPrefix")) = True Then
                If IsDBNull(drRow("sPharmacistPrefix")) = False Then
                    objPrescription.RxPharmacy.PharmacistName.Prefix = drRow("sPharmacistPrefix")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacistAgentFirstName")) = True Then
                If IsDBNull(drRow("sPharmacistAgentFirstName")) = False Then
                    objPrescription.RxPharmacy.PharmacistAgentName.FirstName = drRow("sPharmacistAgentFirstName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacistAgentLastName")) = True Then
                If IsDBNull(drRow("sPharmacistAgentLastName")) = False Then
                    objPrescription.RxPharmacy.PharmacistAgentName.LastName = drRow("sPharmacistAgentLastName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacistAgentMiddleName")) = True Then
                If IsDBNull(drRow("sPharmacistAgentMiddleName")) = False Then
                    objPrescription.RxPharmacy.PharmacistAgentName.MiddleName = drRow("sPharmacistAgentMiddleName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacistAgentSuffix")) = True Then
                If IsDBNull(drRow("sPharmacistAgentSuffix")) = False Then
                    objPrescription.RxPharmacy.PharmacistAgentName.Suffix = drRow("sPharmacistAgentSuffix")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacistAgentPrefix")) = True Then
                If IsDBNull(drRow("sPharmacistAgentPrefix")) = False Then
                    objPrescription.RxPharmacy.PharmacistAgentName.Prefix = drRow("sPharmacistAgentPrefix")
                End If
            End If

            If (drRow.Table.Columns.Contains("sCodeListQualifier")) = True Then
                If IsDBNull(drRow("sCodeListQualifier")) = False Then
                    objPrescription.RxPharmacy.CodeListQualifier = drRow("sCodeListQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sUnitSourceCode")) = True Then
                If IsDBNull(drRow("sUnitSourceCode")) = False Then
                    objPrescription.RxPharmacy.UnitSourceCode = drRow("sUnitSourceCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPotencyUnitCode")) = True Then
                If IsDBNull(drRow("sPotencyUnitCode")) = False Then
                    objPrescription.RxPharmacy.PotencyUnitCode = drRow("sPotencyUnitCode")
                End If
            End If            

            If (drRow.Table.Columns.Contains("sSPI")) = True Then
                If IsDBNull(drRow("sSPI")) = False Then
                    objPrescription.RxPrescriber.PrescriberID = drRow("sSPI")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberNPI")) = True Then
                If IsDBNull(drRow("sPrescriberNPI")) = False Then
                    objPrescription.RxPrescriber.PrescriberNPI = drRow("sPrescriberNPI")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDEA")) = True Then
                If IsDBNull(drRow("sDEA")) = False Then
                    objPrescription.RxPrescriber.PrescriberDEA = drRow("sDEA")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberClinic")) = True Then
                If IsDBNull(drRow("sPrescriberClinic")) = False Then
                    objPrescription.RxPrescriber.PrescriberClinic = drRow("sPrescriberClinic")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberFirstName")) = True Then
                If IsDBNull(drRow("sPrescriberFirstName")) = False Then
                    objPrescription.RxPrescriber.PrescriberName.FirstName = drRow("sPrescriberFirstName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberLastName")) = True Then
                If IsDBNull(drRow("sPrescriberLastName")) = False Then
                    objPrescription.RxPrescriber.PrescriberName.LastName = drRow("sPrescriberLastName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberMiddleName")) = True Then
                If IsDBNull(drRow("sPrescriberMiddleName")) = False Then
                    objPrescription.RxPrescriber.PrescriberName.MiddleName = drRow("sPrescriberMiddleName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberSuffix")) = True Then
                If IsDBNull(drRow("sPrescriberSuffix")) = False Then
                    objPrescription.RxPrescriber.PrescriberName.Suffix = drRow("sPrescriberSuffix")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberPrefix")) = True Then
                If IsDBNull(drRow("sPrescriberPrefix")) = False Then
                    objPrescription.RxPrescriber.PrescriberName.Prefix = drRow("sPrescriberPrefix")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberAddress1")) = True Then
                If IsDBNull(drRow("sPrescriberAddress1")) = False Then
                    objPrescription.RxPrescriber.PrescriberAddress.Address1 = drRow("sPrescriberAddress1")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberAddress2")) = True Then
                If Not IsDBNull(drRow("sPrescriberAddress2")) Then
                    objPrescription.RxPrescriber.PrescriberAddress.Address2 = drRow("sPrescriberAddress2")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberCity")) = True Then
                If IsDBNull(drRow("sPrescriberCity")) = False Then
                    objPrescription.RxPrescriber.PrescriberAddress.City = drRow("sPrescriberCity")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberState")) = True Then
                If IsDBNull(drRow("sPrescriberState")) = False Then
                    objPrescription.RxPrescriber.PrescriberAddress.State = drRow("sPrescriberState")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberZipCode")) = True Then
                If IsDBNull(drRow("sPrescriberZipCode")) = False Then
                    objPrescription.RxPrescriber.PrescriberAddress.Zip = drRow("sPrescriberZipCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberPhone")) = True Then
                If IsDBNull(drRow("sPrescriberPhone")) = False Then
                    objPrescription.RxPrescriber.PrescriberAddress.Phone = drRow("sPrescriberPhone")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberPhQualifier")) = True Then
                If IsDBNull(drRow("sPrescriberPhQualifier")) = False Then
                    objPrescription.RxPrescriber.PrescriberAddress.PhQualifier = drRow("sPrescriberPhQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberEmail")) = True Then
                If IsDBNull(drRow("sPrescriberEmail")) = False Then
                    objPrescription.RxPrescriber.PrescriberAddress.Email = drRow("sPrescriberEmail")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberFax")) = True Then
                If IsDBNull(drRow("sPrescriberFax")) = False Then
                    objPrescription.RxPrescriber.PrescriberAddress.Fax = drRow("sPrescriberFax")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberAgentFirstName")) = True Then
                If IsDBNull(drRow("sPrescriberAgentFirstName")) = False Then
                    objPrescription.RxPrescriber.PrescriberAgentName.FirstName = drRow("sPrescriberAgentFirstName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberAgentLastName")) = True Then
                If IsDBNull(drRow("sPrescriberAgentLastName")) = False Then
                    objPrescription.RxPrescriber.PrescriberAgentName.LastName = drRow("sPrescriberAgentLastName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberAgentMiddleName")) = True Then
                If IsDBNull(drRow("sPrescriberAgentMiddleName")) = False Then
                    objPrescription.RxPrescriber.PrescriberAgentName.MiddleName = drRow("sPrescriberAgentMiddleName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberAgentSuffix")) = True Then
                If IsDBNull(drRow("sPrescriberAgentSuffix")) = False Then
                    objPrescription.RxPrescriber.PrescriberAgentName.Suffix = drRow("sPrescriberAgentSuffix")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberAgentPrefix")) = True Then
                If IsDBNull(drRow("sPrescriberAgentPrefix")) = False Then
                    objPrescription.RxPrescriber.PrescriberAgentName.Prefix = drRow("sPrescriberAgentPrefix")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberSpecialtyQualifier")) = True Then
                If IsDBNull(drRow("sPrescriberSpecialtyQualifier")) = False Then
                    objPrescription.RxPrescriber.PrescriberSpecialtyQualifier = drRow("sPrescriberSpecialtyQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberSpecialtyCode")) = True Then
                If IsDBNull(drRow("sPrescriberSpecialtyCode")) = False Then
                    objPrescription.RxPrescriber.PrescriberSpecialtyCode = drRow("sPrescriberSpecialtyCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientIdentifier1")) = True Then
                If IsDBNull(drRow("sPatientIdentifier1")) = False Then
                    objPrescription.RxPatient.PatientName.ID = drRow("sPatientIdentifier1")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientIdentifierType1")) = True Then
                If IsDBNull(drRow("sPatientIdentifierType1")) = False Then
                    objPrescription.RxPatient.PatientName.IDType = drRow("sPatientIdentifierType1")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientIdentifier2")) = True Then
                If IsDBNull(drRow("sPatientIdentifier2")) = False Then
                    objPrescription.RxPatient.PatientName.Code = drRow("sPatientIdentifier2")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientIdentifierType2")) = True Then
                If IsDBNull(drRow("sPatientIdentifierType2")) = False Then
                    objPrescription.RxPatient.PatientName.CodeType = drRow("sPatientIdentifierType2")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientFirstName")) = True Then
                If IsDBNull(drRow("sPatientFirstName")) = False Then
                    objPrescription.RxPatient.PatientName.FirstName = drRow("sPatientFirstName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientLastName")) = True Then
                If IsDBNull(drRow("sPatientLastName")) = False Then
                    objPrescription.RxPatient.PatientName.LastName = drRow("sPatientLastName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientMiddleName")) = True Then
                If IsDBNull(drRow("sPatientMiddleName")) = False Then
                    objPrescription.RxPatient.PatientName.MiddleName = drRow("sPatientMiddleName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientSuffix")) = True Then
                If IsDBNull(drRow("sPatientSuffix")) = False Then
                    objPrescription.RxPatient.PatientName.Suffix = drRow("sPatientSuffix")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientPrefix")) = True Then
                If IsDBNull(drRow("sPatientPrefix")) = False Then
                    objPrescription.RxPatient.PatientName.Prefix = drRow("sPatientPrefix")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientGender")) = True Then
                If IsDBNull(drRow("sPatientGender")) = False Then
                    objPrescription.RxPatient.Gender = drRow("sPatientGender")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientDOB")) = True Then
                If IsDBNull(drRow("sPatientDOB")) = False Then
                    objPrescription.RxPatient.DateofBirth = drRow("sPatientDOB")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientAddress1")) = True Then
                If IsDBNull(drRow("sPatientAddress1")) = False Then
                    objPrescription.RxPatient.PatientAddress.Address1 = drRow("sPatientAddress1")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientAddress2")) = True Then
                If IsDBNull(drRow("sPatientAddress2")) = False Then
                    objPrescription.RxPatient.PatientAddress.Address2 = drRow("sPatientAddress2")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientCity")) = True Then
                If IsDBNull(drRow("sPatientCity")) = False Then
                    objPrescription.RxPatient.PatientAddress.City = drRow("sPatientCity")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientState")) = True Then
                If IsDBNull(drRow("sPatientState")) = False Then
                    objPrescription.RxPatient.PatientAddress.State = drRow("sPatientState")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientZipCode")) = True Then
                If IsDBNull(drRow("sPatientZipCode")) = False Then
                    objPrescription.RxPatient.PatientAddress.Zip = drRow("sPatientZipCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientPhone")) = True Then
                If IsDBNull(drRow("sPatientPhone")) = False Then
                    objPrescription.RxPatient.PatientAddress.Phone = drRow("sPatientPhone")
                End If
            End If
            If (drRow.Table.Columns.Contains("sPatientPhQualifier")) = True Then
                If IsDBNull(drRow("sPatientPhQualifier")) = False Then
                    objPrescription.RxPatient.PatientAddress.PhQualifier = drRow("sPatientPhQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientEmail")) = True Then
                If IsDBNull(drRow("sPatientEmail")) = False Then
                    objPrescription.RxPatient.PatientAddress.Email = drRow("sPatientEmail")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientFax")) = True Then
                If IsDBNull(drRow("sPatientFax")) = False Then
                    objPrescription.RxPatient.PatientAddress.Fax = drRow("sPatientFax")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientWorkPhone")) = True Then
                If IsDBNull(drRow("sPatientWorkPhone")) = False Then
                    objPrescription.RxPatient.PatientAddress.WorkPhone = drRow("sPatientWorkPhone")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDrugName")) = True Then
                If IsDBNull(drRow("sDrugName")) = False Then
                    oDrug.DrugName = drRow("sDrugName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDrugDirections")) = True Then
                If IsDBNull(drRow("sDrugDirections")) = False Then
                    oDrug.Directions = drRow("sDrugDirections")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDrugDuration")) = True Then
                If IsDBNull(drRow("sDrugDuration")) = False Then
                    oDrug.DrugDuration = drRow("sDrugDuration")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDrugQuantity")) = True Then
                If IsDBNull(drRow("sDrugQuantity")) = False Then
                    oDrug.DrugQuantity = drRow("sDrugQuantity")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDrugQualifier")) = True Then
                If IsDBNull(drRow("sDrugQualifier")) = False Then
                    oDrug.DrugQuantityQualifier = drRow("sDrugQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sRefillQuantity")) = True Then
                If IsDBNull(drRow("sRefillQuantity")) = False Then
                    oDrug.RefillQuantity = drRow("sRefillQuantity")
                End If
            End If

            If (drRow.Table.Columns.Contains("sRefillsQualifier")) = True Then
                If IsDBNull(drRow("sRefillsQualifier")) = False Then
                    oDrug.RefillsQualifier = drRow("sRefillsQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("bIsSubstituitons")) = True Then
                If IsDBNull(drRow("bIsSubstituitons")) = False Then
                    oDrug.MaySubstitute = drRow("bIsSubstituitons")
                End If
            End If

            If (drRow.Table.Columns.Contains("dtWrittenDate")) = True Then
                If IsDBNull(drRow("dtWrittenDate")) = False Then
                    oDrug.WrittenDate = drRow("dtWrittenDate")
                End If
            End If

            If (drRow.Table.Columns.Contains("dtLastFillDate")) = True Then
                If IsDBNull(drRow("dtLastFillDate")) = False Then
                    oDrug.LastFillDate = drRow("dtLastFillDate")
                End If
            End If

            If (drRow.Table.Columns.Contains("sNotes")) = True Then
                If IsDBNull(drRow("sNotes")) = False Then
                    oDrug.Notes = drRow("sNotes")
                End If
            End If

            If (drRow.Table.Columns.Contains("sProductCode")) = True Then
                If IsDBNull(drRow("sProductCode")) = False Then
                    oDrug.ProductCode = drRow("sProductCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sProductCodeQualifier")) = True Then
                If IsDBNull(drRow("sProductCodeQualifier")) = False Then
                    oDrug.ProductCodeQualifier = drRow("sProductCodeQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDosageForm")) = True Then
                If IsDBNull(drRow("sDosageForm")) = False Then
                    oDrug.DosageForm = drRow("sDosageForm")
                    oDrug.Dosage = drRow("sDosageForm")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDrugStrength")) = True Then
                If IsDBNull(drRow("sDrugStrength")) = False Then
                    oDrug.DrugStrength = drRow("sDrugStrength")                    
                End If
            End If

            If (drRow.Table.Columns.Contains("sDrugStrengthUnits")) = True Then
                If IsDBNull(drRow("sDrugStrengthUnits")) = False Then
                    oDrug.DrugStrengthUnits = drRow("sDrugStrengthUnits")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDrugDBCode")) = True Then
                If IsDBNull(drRow("sDrugDBCode")) = False Then
                    oDrug.DrugDBCode = drRow("sDrugDBCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDrugDBCodeQualifier")) = True Then
                If IsDBNull(drRow("sDrugDBCodeQualifier")) = False Then
                    oDrug.DrugDBCodeQualifier = drRow("sDrugDBCodeQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sClinicalInformationQualifier1")) = True Then
                If IsDBNull(drRow("sClinicalInformationQualifier1")) = False Then
                    oDrug.ClinicalInformationQualifier1 = drRow("sClinicalInformationQualifier1")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrimaryQualifier1")) = True Then
                If IsDBNull(drRow("sPrimaryQualifier1")) = False Then
                    oDrug.PrimaryQualifier1 = drRow("sPrimaryQualifier1")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrimaryValue1")) = True Then
                If IsDBNull(drRow("sPrimaryValue1")) = False Then
                    oDrug.PrimaryValue1 = drRow("sPrimaryValue1")
                End If
            End If

            If (drRow.Table.Columns.Contains("sSecondaryQualifier1")) = True Then
                If IsDBNull(drRow("sSecondaryQualifier1")) = False Then
                    oDrug.SecondaryQualifier1 = drRow("sSecondaryQualifier1")
                End If
            End If

            If (drRow.Table.Columns.Contains("sSecondaryValue1")) = True Then
                If IsDBNull(drRow("sSecondaryValue1")) = False Then
                    oDrug.SecondaryValue1 = drRow("sSecondaryValue1")
                End If
            End If

            If (drRow.Table.Columns.Contains("sClinicalInformationQualifier2")) = True Then
                If IsDBNull(drRow("sClinicalInformationQualifier2")) = False Then
                    oDrug.ClinicalInformationQualifier2 = drRow("sClinicalInformationQualifier2")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrimaryQualifier2")) = True Then
                If IsDBNull(drRow("sPrimaryQualifier2")) = False Then
                    oDrug.PrimaryQualifier2 = drRow("sPrimaryQualifier2")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrimaryValue2")) = True Then
                If IsDBNull(drRow("sPrimaryValue2")) = False Then
                    oDrug.PrimaryValue2 = drRow("sPrimaryValue2")
                End If
            End If

            If (drRow.Table.Columns.Contains("sSecondaryQualifier2")) = True Then
                If IsDBNull(drRow("sSecondaryQualifier2")) = False Then
                    oDrug.SecondaryQualifier2 = drRow("sSecondaryQualifier2")
                End If
            End If

            If (drRow.Table.Columns.Contains("sSecondaryValue2")) = True Then
                If IsDBNull(drRow("sSecondaryValue2")) = False Then
                    oDrug.SecondaryValue2 = drRow("sSecondaryValue2")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPriorAuthorizationQualifier")) = True Then
                If IsDBNull(drRow("sPriorAuthorizationQualifier")) = False Then
                    oDrug.PriorAuthorizationQualifier = drRow("sPriorAuthorizationQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPriorAuthorizationValue")) = True Then
                If IsDBNull(drRow("sPriorAuthorizationValue")) = False Then
                    oDrug.PriorAuthorizationValue = drRow("sPriorAuthorizationValue")
                End If
            End If

            oDrug.MessageName = "RefillRequest"

            If (drRow.Table.Columns.Contains("sMDDrugName")) = True Then
                If IsDBNull(drRow("sMDDrugName")) = False Then
                    oDrug.MDDrugName = drRow("sMDDrugName")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDDrugQuantity")) = True Then
                If IsDBNull(drRow("sMDDrugQuantity")) = False Then
                    oDrug.MDDrugQuantity = drRow("sMDDrugQuantity")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDDrugQualifier")) = True Then
                If IsDBNull(drRow("sMDDrugQualifier")) = False Then
                    oDrug.MDDrugQualifier = drRow("sMDDrugQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDRefillQuantity")) = True Then
                If IsDBNull(drRow("sMDRefillQuantity")) = False Then
                    oDrug.MDRefillQuantity = drRow("sMDRefillQuantity")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDRefillsQualifier")) = True Then
                If IsDBNull(drRow("sMDRefillsQualifier")) = False Then
                    oDrug.MDRefillsQualifier = drRow("sMDRefillsQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("MDdtWrittenDate")) = True Then
                If IsDBNull(drRow("MDdtWrittenDate")) = False Then
                    oDrug.MDdtWrittenDate = drRow("MDdtWrittenDate")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDDrugDuration")) = True Then
                If IsDBNull(drRow("sMDDrugDuration")) = False Then
                    oDrug.MDDrugDuration = drRow("sMDDrugDuration")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDDrugDirections")) = True Then
                If IsDBNull(drRow("sMDDrugDirections")) = False Then
                    oDrug.MDDrugDirections = drRow("sMDDrugDirections")
                End If
            End If

            If (drRow.Table.Columns.Contains("MDbIsSubstituitons")) = True Then
                If IsDBNull(drRow("MDbIsSubstituitons")) = False Then
                    oDrug.MDbIsSubstituitons = drRow("MDbIsSubstituitons")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDNotes")) = True Then
                If IsDBNull(drRow("sMDNotes")) = False Then
                    oDrug.MDNotes = drRow("sMDNotes")
                End If
            End If

            If (drRow.Table.Columns.Contains("MDdtLastFillDate")) = True Then
                If IsDBNull(drRow("MDdtLastFillDate")) = False Then
                    oDrug.MDdtLastFillDate = drRow("MDdtLastFillDate")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDProductCode")) = True Then
                If IsDBNull(drRow("sMDProductCode")) = False Then
                    oDrug.MDProductCode = drRow("sMDProductCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDProductCodeQualifier")) = True Then
                If IsDBNull(drRow("sMDProductCodeQualifier")) = False Then
                    oDrug.MDProductCodeQualifier = drRow("sMDProductCodeQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDDosageForm")) = True Then
                If IsDBNull(drRow("sMDDosageForm")) = False Then
                    oDrug.MDDosageForm = drRow("sMDDosageForm")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDDrugStrength")) = True Then
                If IsDBNull(drRow("sMDDrugStrength")) = False Then
                    oDrug.MDDrugStrength = drRow("sMDDrugStrength")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDDrugStrengthUnits")) = True Then
                If IsDBNull(drRow("sMDDrugStrengthUnits")) = False Then
                    oDrug.MDDrugStrengthUnits = drRow("sMDDrugStrengthUnits")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDCodeListQualifier")) = True Then
                If IsDBNull(drRow("sMDCodeListQualifier")) = False Then
                    oDrug.MDCodeListQualifier = drRow("sMDCodeListQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDUnitSourceCode")) = True Then
                If IsDBNull(drRow("sMDUnitSourceCode")) = False Then
                    oDrug.MDUnitSourceCode = drRow("sMDUnitSourceCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDPotencyUnitCode")) = True Then
                If IsDBNull(drRow("sMDPotencyUnitCode")) = False Then
                    oDrug.MDPotencyUnitCode = drRow("sMDPotencyUnitCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDDrugDBCode")) = True Then
                If IsDBNull(drRow("sMDDrugDBCode")) = False Then
                    oDrug.MDDrugDBCode = drRow("sMDDrugDBCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDDrugDBCodeQualifier")) = True Then
                If IsDBNull(drRow("sMDDrugDBCodeQualifier")) = False Then
                    oDrug.MDDrugDBCodeQualifier = drRow("sMDDrugDBCodeQualifier")
                End If
            End If

            If (drRow.Table.Columns.Contains("sDrugCoverageStatusCode")) = True Then
                If IsDBNull(drRow("sDrugCoverageStatusCode")) = False Then
                    oDrug.DrugCoverageStatusCode = drRow("sDrugCoverageStatusCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sMDDrugCoverageStatusCode")) = True Then
                If IsDBNull(drRow("sMDDrugCoverageStatusCode")) = False Then
                    oDrug.MDDrugCoverageStatusCode = drRow("sMDDrugCoverageStatusCode")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPharmacySpecialty")) = True Then
                If IsDBNull(drRow("sPharmacySpecialty")) = False Then
                    oDrug.PharmacySpecialty = drRow("sPharmacySpecialty")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPatientRelationship")) = True Then
                If IsDBNull(drRow("sPatientRelationship")) = False Then
                    oDrug.PatientRelationship = drRow("sPatientRelationship")
                End If
            End If

            If (drRow.Table.Columns.Contains("sPrescriberSSN")) = True Then
                If IsDBNull(drRow("sPrescriberSSN")) = False Then
                    oDrug.PrescriberSSN = drRow("sPrescriberSSN")
                End If
            End If

            If (drRow.Table.Columns.Contains("FileData")) = True Then
                If IsDBNull(drRow("FileData")) = False Then
                    oDrug.FileData = System.Text.Encoding.Default.GetBytes(drRow("FileData"))
                End If
            End If
            
            objPrescription.DrugsCol.Add(oDrug)

            Return objPrescription
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return Nothing
        End Try
    End Function

    Public Function ReadErrorMsg(ByVal drRow As DataRow) As SureScriptErrorMessage
        Try
            Dim objErrorMessage As New SureScriptErrorMessage
            
            If (drRow.Table.Columns.Contains("sMessageFrom")) AndAlso Not IsDBNull(drRow("sMessageFrom")) Then                
                objErrorMessage.MessageFrom = drRow("sMessageFrom")
            End If

            If (drRow.Table.Columns.Contains("sMessageTo")) AndAlso Not IsDBNull(drRow("sMessageTo")) Then                
                objErrorMessage.MessageTo = drRow("sMessageTo")
            End If

            If (drRow.Table.Columns.Contains("sMessageID")) AndAlso Not IsDBNull(drRow("sMessageID")) Then
                objErrorMessage.MessageID = drRow("sMessageID")
            End If

            If (drRow.Table.Columns.Contains("sRelativeMessageID")) AndAlso Not IsDBNull(drRow("sRelativeMessageID")) Then
                objErrorMessage.RelatesToMessageId = drRow("sRelativeMessageID")
            End If

            If (drRow.Table.Columns.Contains("dtSentTime")) AndAlso Not IsDBNull(drRow("dtSentTime")) Then                
                objErrorMessage.DateTimeStamp = drRow("dtSentTime")
            End If

            objErrorMessage.DateReceived = globalCurrentDate

            If (drRow.Table.Columns.Contains("sStatusCode")) AndAlso Not IsDBNull(drRow("sStatusCode")) Then
                objErrorMessage.ErrorCode = drRow("sStatusCode")
            End If

            If (drRow.Table.Columns.Contains("sResponseCode")) AndAlso Not IsDBNull(drRow("sResponseCode")) Then
                objErrorMessage.DescriptionCode = drRow("sResponseCode")
            End If

            If (drRow.Table.Columns.Contains("sResponseDescription")) AndAlso Not IsDBNull(drRow("sResponseDescription")) Then
                objErrorMessage.Description = drRow("sResponseDescription")
            End If

            objErrorMessage.MessageName = "Error"

            Return objErrorMessage
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return Nothing
        End Try
    End Function

    Private Function GetSubstitutionCode(ByVal strcode As String) As Boolean
        Select Case strcode

            'Substitutions are allowed
            Case "0", "2", "3", "4", "5", "8"
                Return False
                'Substitutions not allowed
            Case "1", "7"
                Return True
        End Select
    End Function

    Private Function InsertRefillPrescription(ByVal objPrescription As EPrescription) As Boolean

        Try
            'objcmd = New SqlCommand
            'objcmd.Connection = objCon
            'objcmd.CommandType = CommandType.Text
            'strSQL = "Insert into PrescriptionRefillTransaction values ( '" & objPrescription.RxReferenceNumber & "' , " & objPrescription.RxTransactionID & " ,'Pending')"
            'objcmd.CommandText = strSQL
            'objCon.Open()
            'objcmd.ExecuteNonQuery()
            'objcmd.Cancel()
            'strSQL = String.Empty
            If objPrescription Is Nothing Or objPrescription.DrugsCol.Count = 0 Then
                Return False
            End If

            Dim oDrug As EDrug = Nothing
            oDrug = objPrescription.DrugsCol.Item(0)

            ''_strsql = "Insert into PrescriptionRefillTransactiondetail (nRxTransactionID,sDrugName,sStrength,sQuantity,sQuantityQualifier,sRefillQuantity,sRefillQualifier,bMaySubstitutions,dtWrittendate) values (" & objPrescription.RxTransactionID & ",'" & oDrug.DrugName & "','" & oDrug.DrugStrength & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "' ,'" & oDrug.RefillsQualifier & "','" & oDrug.MaySubstitute & "','" & oDrug.WrittenDate & "')"
            ''strSQL = "Insert into PrescriptionRefillTransactiondetail (nRxTransactionID,sDrugName,sStrength,sFrequency,sDuration,sQuantity,sQuantityQualifier,sRefillQuantity,sRefillQualifier,bMaySubstitutions,dtWrittendate,sRxReferenceNumber,sStatus,sPharmacyID,dtlastdate,sNotes,sPrescriberID,sPatientFirstName,sPatientLastName,sPatientGender,dtPatientDOB) " _
            ''            & " values (" & objPrescription.RxTransactionID & ",'" & oDrug.DrugName & "','" & oDrug.DrugStrength & "','" & oDrug.Directions & "','" & oDrug.DrugDuration & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "' ,'" & oDrug.RefillsQualifier & "','" & bln & "','" & oDrug.WrittenDate & "', '" & oDrug.RxReferenceNumber & "','Pending','" & objPrescription.RxPharmacy.PharmacyID & "','" & Number2Date(oDrug.LastFillDate) & "','" & oDrug.Notes & "','" & objPrescription.RxPrescriber.PrescriberID & "','" & objPrescription.RxPatient.PatientName.FirstName & "','" & objPrescription.RxPatient.PatientName.LastName & "','" & GetPatGender(objPrescription.RxPatient.Gender) & "','" & Number2Date(objPrescription.RxPatient.DateofBirth).Date & "')"
            'If oDrug.LastFillDate <> "" And oDrug.WrittenDate <> "" Then
            '    strSQL = "INSERT INTO PrescriptionRefillTransactiondetail(nRxTransactionID,sDrugName,sDrugForm,sStrength,sDosage,sFrequency,sDuration ,sQuantity ,sQuantityQualifier,sRefillQuantity,sRefillQualifier,bMaySubstitutions,dtWrittendate,sRxReferenceNumber,sStatus,sPharmacyID,dtlastdate,sNotes,sPrescriberID," _
            '                       & "sPatientFirstName,sPatientLastName,sPatientGender,dtPatientDOB,sPatientMName,sPatientPrefix,sPatientSuffix,sPatientAddress1,sPatientAddress2,sPatientCity,sPatientState,sPatientZipcode,sPatientNumber,sPatientQualifier,sPatientEmail," _
            '                       & "sPrFirstName,sPrMName,sPrLastName,sPrPrefix,sPrSuffix,sPrAddress1,sPrAddress2,sPrCity,sPrState,sPrZipcode,sPrNumber,sPrQualifier,sPrEmail,sPrAgentFirstName,sPrAgentMiddleName,sPrAgentLastName,sPrAgentPrefix,sPrAgentSuffix,sPrSpecialtyType,sPrSpecialtyCode," _
            '                       & "sPhName,sPhFirstName,sPhMName,sPhLastName,sPhPrefix,sPhSuffix,sPhAddress1,sPhAddress2,sPhCity,sPhState,sPhZipcode,sPhNumber,sPhQualifier,sPhEmail,sPhAgentFirstName,sPhAgentMName,sPhAgentLastName,sPhAgentPrefix,sPhAgentSuffix," _
            '                       & "sDgClQualifier1,sPrimaryQualifier1,sPrimaryValue1,sSecQualifier1,sSecValue1,sDgClQualifier2,sPrimaryQualifier2,sPrimaryValue2,sSecQualifier2,sSecValue2,sProductCode,sProductCodeQualifier,sDosageForm,sStrengthUnits,sDrugDBCode,sDrugDBCodeQualifier,sPriorAuthorizationQualifier,sPriorAuthorizationValue,sPrescriberClinic)" _
            '                       & " VALUES(" & objPrescription.RxTransactionID & ",'" & oDrug.DrugName & "','" & oDrug.Drugform & "','" & oDrug.DrugStrength & "','" & oDrug.Dosage & "','" & oDrug.Directions & "','" & oDrug.DrugDuration & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "','" & oDrug.RefillsQualifier & "','" & oDrug.MaySubstitute & "','" & Number2Date(oDrug.WrittenDate) & "','" & oDrug.RxReferenceNumber & "','Pending','" & objPrescription.RxPharmacy.PharmacyID & "','" & Number2Date(oDrug.LastFillDate) & "','" & oDrug.Notes & "','" & objPrescription.RxPrescriber.PrescriberID & "','" _
            '                       & objPrescription.RxPatient.PatientName.FirstName & "','" & objPrescription.RxPatient.PatientName.LastName & "','" & GetPatGender(objPrescription.RxPatient.Gender) & "','" & Number2Date(objPrescription.RxPatient.DateofBirth) & "','" & objPrescription.RxPatient.PatientName.MiddleName & "','" & objPrescription.RxPatient.PatientName.Prefix & "','" & objPrescription.RxPatient.PatientName.Suffix & "','" & objPrescription.RxPatient.PatientAddress.Address1 & "','" & objPrescription.RxPatient.PatientAddress.Address2 & "','" & objPrescription.RxPatient.PatientAddress.City & "','" & objPrescription.RxPatient.PatientAddress.State & "','" & objPrescription.RxPatient.PatientAddress.Zip & "','" & objPrescription.RxPatient.PatientAddress.Phone & "','" & objPrescription.RxPatient.PatientAddress.PhQualifier & "','" & objPrescription.RxPatient.PatientAddress.Email & "','" _
            '                       & objPrescription.RxPrescriber.PrescriberName.FirstName & "','" & objPrescription.RxPrescriber.PrescriberName.MiddleName & "','" & objPrescription.RxPrescriber.PrescriberName.LastName & "','" & objPrescription.RxPrescriber.PrescriberName.Prefix & "','" & objPrescription.RxPrescriber.PrescriberName.Suffix & "','" & objPrescription.RxPrescriber.PrescriberAddress.Address1 & "','" & objPrescription.RxPrescriber.PrescriberAddress.Address2 & "','" & objPrescription.RxPrescriber.PrescriberAddress.City & "','" & objPrescription.RxPrescriber.PrescriberAddress.State & "','" & objPrescription.RxPrescriber.PrescriberAddress.Zip & "','" & objPrescription.RxPrescriber.PrescriberAddress.Phone & "','" & objPrescription.RxPrescriber.PrescriberAddress.PhQualifier & "','" & objPrescription.RxPrescriber.PrescriberAddress.Email & "','" & objPrescription.RxPrescriber.PrescriberAgentName.FirstName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.MiddleName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.LastName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.Prefix & "','" & objPrescription.RxPrescriber.PrescriberAgentName.Suffix & "','" & objPrescription.RxPrescriber.PrescriberSpecialtyQualifier & "','" & objPrescription.RxPrescriber.PrescriberSpecialtyCode & "','" _
            '                       & objPrescription.RxPharmacy.Pharmacyname & "','" & objPrescription.RxPharmacy.PharmacistName.FirstName & "','" & objPrescription.RxPharmacy.PharmacistName.MiddleName & "','" & objPrescription.RxPharmacy.PharmacistName.LastName & "','" & objPrescription.RxPharmacy.PharmacistName.Prefix & "','" & objPrescription.RxPharmacy.PharmacistName.Suffix & "','" & objPrescription.RxPharmacy.PharmacyAddress.Address1 & "','" & objPrescription.RxPharmacy.PharmacyAddress.Address2 & "','" & objPrescription.RxPharmacy.PharmacyAddress.City & "','" & objPrescription.RxPharmacy.PharmacyAddress.State & "','" & objPrescription.RxPharmacy.PharmacyAddress.Zip & "','" & objPrescription.RxPharmacy.PharmacyAddress.Phone & "','" & objPrescription.RxPharmacy.PharmacyAddress.PhQualifier & "','" & objPrescription.RxPharmacy.PharmacyAddress.Email & "','" & objPrescription.RxPharmacy.PharmacistAgentName.FirstName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.MiddleName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.LastName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.Prefix & "','" & objPrescription.RxPharmacy.PharmacistAgentName.Suffix & "','" _
            '                       & oDrug.ClinicalInformationQualifier1 & "','" & oDrug.PrimaryQualifier1 & "','" & oDrug.PrimaryValue1 & "','" & oDrug.SecondaryQualifier1 & "','" & oDrug.SecondaryValue1 & "','" & oDrug.ClinicalInformationQualifier2 & "','" & oDrug.PrimaryQualifier2 & "','" & oDrug.PrimaryValue2 & "','" & oDrug.SecondaryQualifier2 & "','" & oDrug.SecondaryValue2 & "','" & oDrug.ProductCode & "','" & oDrug.ProductCodeQualifier & "','" & oDrug.DosageForm & "','" & oDrug.DrugStrengthUnits & "','" & oDrug.DrugDBCode & "','" & oDrug.DrugDBCodeQualifier & "','" & oDrug.PriorAuthorizationQualifier & "','" & oDrug.PriorAuthorizationValue & "','" & objPrescription.RxPrescriber.PrescriberClinic & "')"

            'ElseIf oDrug.WrittenDate <> "" And oDrug.LastFillDate = "" Then

            '    strSQL = "INSERT INTO PrescriptionRefillTransactiondetail(nRxTransactionID,sDrugName,sDrugForm,sStrength,sDosage,sFrequency,sDuration ,sQuantity ,sQuantityQualifier,sRefillQuantity,sRefillQualifier,bMaySubstitutions,dtWrittendate,sRxReferenceNumber,sStatus,sPharmacyID,sNotes,sPrescriberID," _
            '                       & "sPatientFirstName,sPatientLastName,sPatientGender,dtPatientDOB,sPatientMName,sPatientPrefix,sPatientSuffix,sPatientAddress1,sPatientAddress2,sPatientCity,sPatientState,sPatientZipcode,sPatientNumber,sPatientQualifier,sPatientEmail," _
            '                       & "sPrFirstName,sPrMName,sPrLastName,sPrPrefix,sPrSuffix,sPrAddress1,sPrAddress2,sPrCity,sPrState,sPrZipcode,sPrNumber,sPrQualifier,sPrEmail,sPrAgentFirstName,sPrAgentMiddleName,sPrAgentLastName,sPrAgentPrefix,sPrAgentSuffix,sPrSpecialtyType,sPrSpecialtyCode," _
            '                       & "sPhName,sPhFirstName,sPhMName,sPhLastName,sPhPrefix,sPhSuffix,sPhAddress1,sPhAddress2,sPhCity,sPhState,sPhZipcode,sPhNumber,sPhQualifier,sPhEmail,sPhAgentFirstName,sPhAgentMName,sPhAgentLastName,sPhAgentPrefix,sPhAgentSuffix," _
            '                       & "sDgClQualifier1,sPrimaryQualifier1,sPrimaryValue1,sSecQualifier1,sSecValue1,sDgClQualifier2,sPrimaryQualifier2,sPrimaryValue2,sSecQualifier2,sSecValue2,sProductCode,sProductCodeQualifier,sDosageForm,sStrengthUnits,sDrugDBCode,sDrugDBCodeQualifier,sPriorAuthorizationQualifier,sPriorAuthorizationValue,sPrescriberClinic)" _
            '                       & " VALUES(" & objPrescription.RxTransactionID & ",'" & oDrug.DrugName & "','" & oDrug.Drugform & "','" & oDrug.DrugStrength & "','" & oDrug.Dosage & "','" & oDrug.Directions & "','" & oDrug.DrugDuration & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "','" & oDrug.RefillsQualifier & "','" & oDrug.MaySubstitute & "','" & Number2Date(oDrug.WrittenDate) & "','" & oDrug.RxReferenceNumber & "','Pending','" & objPrescription.RxPharmacy.PharmacyID & "','" & oDrug.Notes & "','" & objPrescription.RxPrescriber.PrescriberID & "','" _
            '                       & objPrescription.RxPatient.PatientName.FirstName & "','" & objPrescription.RxPatient.PatientName.LastName & "','" & GetPatGender(objPrescription.RxPatient.Gender) & "','" & Number2Date(objPrescription.RxPatient.DateofBirth) & "','" & objPrescription.RxPatient.PatientName.MiddleName & "','" & objPrescription.RxPatient.PatientName.Prefix & "','" & objPrescription.RxPatient.PatientName.Suffix & "','" & objPrescription.RxPatient.PatientAddress.Address1 & "','" & objPrescription.RxPatient.PatientAddress.Address2 & "','" & objPrescription.RxPatient.PatientAddress.City & "','" & objPrescription.RxPatient.PatientAddress.State & "','" & objPrescription.RxPatient.PatientAddress.Zip & "','" & objPrescription.RxPatient.PatientAddress.Phone & "','" & objPrescription.RxPatient.PatientAddress.PhQualifier & "','" & objPrescription.RxPatient.PatientAddress.Email & "','" _
            '                       & objPrescription.RxPrescriber.PrescriberName.FirstName & "','" & objPrescription.RxPrescriber.PrescriberName.MiddleName & "','" & objPrescription.RxPrescriber.PrescriberName.LastName & "','" & objPrescription.RxPrescriber.PrescriberName.Prefix & "','" & objPrescription.RxPrescriber.PrescriberName.Suffix & "','" & objPrescription.RxPrescriber.PrescriberAddress.Address1 & "','" & objPrescription.RxPrescriber.PrescriberAddress.Address2 & "','" & objPrescription.RxPrescriber.PrescriberAddress.City & "','" & objPrescription.RxPrescriber.PrescriberAddress.State & "','" & objPrescription.RxPrescriber.PrescriberAddress.Zip & "','" & objPrescription.RxPrescriber.PrescriberAddress.Phone & "','" & objPrescription.RxPrescriber.PrescriberAddress.PhQualifier & "','" & objPrescription.RxPrescriber.PrescriberAddress.Email & "','" & objPrescription.RxPrescriber.PrescriberAgentName.FirstName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.MiddleName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.LastName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.Prefix & "','" & objPrescription.RxPrescriber.PrescriberAgentName.Suffix & "','" & objPrescription.RxPrescriber.PrescriberSpecialtyQualifier & "','" & objPrescription.RxPrescriber.PrescriberSpecialtyCode & "','" _
            '                       & objPrescription.RxPharmacy.Pharmacyname & "','" & objPrescription.RxPharmacy.PharmacistName.FirstName & "','" & objPrescription.RxPharmacy.PharmacistName.MiddleName & "','" & objPrescription.RxPharmacy.PharmacistName.LastName & "','" & objPrescription.RxPharmacy.PharmacistName.Prefix & "','" & objPrescription.RxPharmacy.PharmacistName.Suffix & "','" & objPrescription.RxPharmacy.PharmacyAddress.Address1 & "','" & objPrescription.RxPharmacy.PharmacyAddress.Address2 & "','" & objPrescription.RxPharmacy.PharmacyAddress.City & "','" & objPrescription.RxPharmacy.PharmacyAddress.State & "','" & objPrescription.RxPharmacy.PharmacyAddress.Zip & "','" & objPrescription.RxPharmacy.PharmacyAddress.Phone & "','" & objPrescription.RxPharmacy.PharmacyAddress.PhQualifier & "','" & objPrescription.RxPharmacy.PharmacyAddress.Email & "','" & objPrescription.RxPharmacy.PharmacistAgentName.FirstName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.MiddleName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.LastName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.Prefix & "','" & objPrescription.RxPharmacy.PharmacistAgentName.Suffix & "','" _
            '                       & oDrug.ClinicalInformationQualifier1 & "','" & oDrug.PrimaryQualifier1 & "','" & oDrug.PrimaryValue1 & "','" & oDrug.SecondaryQualifier1 & "','" & oDrug.SecondaryValue1 & "','" & oDrug.ClinicalInformationQualifier2 & "','" & oDrug.PrimaryQualifier2 & "','" & oDrug.PrimaryValue2 & "','" & oDrug.SecondaryQualifier2 & "','" & oDrug.SecondaryValue2 & "','" & oDrug.ProductCode & "','" & oDrug.ProductCodeQualifier & "','" & oDrug.DosageForm & "','" & oDrug.DrugStrengthUnits & "','" & oDrug.DrugDBCode & "','" & oDrug.DrugDBCodeQualifier & "','" & oDrug.PriorAuthorizationQualifier & "','" & oDrug.PriorAuthorizationValue & "','" & objPrescription.RxPrescriber.PrescriberClinic & "')"
            'ElseIf oDrug.WrittenDate = "" And oDrug.LastFillDate <> "" Then
            '    strSQL = "INSERT INTO PrescriptionRefillTransactiondetail(nRxTransactionID,sDrugName,sDrugForm,sStrength,sDosage,sFrequency,sDuration ,sQuantity ,sQuantityQualifier,sRefillQuantity,sRefillQualifier,bMaySubstitutions,sRxReferenceNumber,sStatus,sPharmacyID,dtlastdate,sNotes,sPrescriberID," _
            '                                     & "sPatientFirstName,sPatientLastName,sPatientGender,dtPatientDOB,sPatientMName,sPatientPrefix,sPatientSuffix,sPatientAddress1,sPatientAddress2,sPatientCity,sPatientState,sPatientZipcode,sPatientNumber,sPatientQualifier,sPatientEmail," _
            '                                     & "sPrFirstName,sPrMName,sPrLastName,sPrPrefix,sPrSuffix,sPrAddress1,sPrAddress2,sPrCity,sPrState,sPrZipcode,sPrNumber,sPrQualifier,sPrEmail,sPrAgentFirstName,sPrAgentMiddleName,sPrAgentLastName,sPrAgentPrefix,sPrAgentSuffix,sPrSpecialtyType,sPrSpecialtyCode," _
            '                                     & "sPhName,sPhFirstName,sPhMName,sPhLastName,sPhPrefix,sPhSuffix,sPhAddress1,sPhAddress2,sPhCity,sPhState,sPhZipcode,sPhNumber,sPhQualifier,sPhEmail,sPhAgentFirstName,sPhAgentMName,sPhAgentLastName,sPhAgentPrefix,sPhAgentSuffix," _
            '                                     & "sDgClQualifier1,sPrimaryQualifier1,sPrimaryValue1,sSecQualifier1,sSecValue1,sDgClQualifier2,sPrimaryQualifier2,sPrimaryValue2,sSecQualifier2,sSecValue2,sProductCode,sProductCodeQualifier,sDosageForm,sStrengthUnits,sDrugDBCode,sDrugDBCodeQualifier,sPriorAuthorizationQualifier,sPriorAuthorizationValue,sPrescriberClinic)" _
            '                                     & " VALUES(" & objPrescription.RxTransactionID & ",'" & oDrug.DrugName & "','" & oDrug.Drugform & "','" & oDrug.DrugStrength & "','" & oDrug.Dosage & "','" & oDrug.Directions & "','" & oDrug.DrugDuration & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "','" & oDrug.RefillsQualifier & "','" & oDrug.MaySubstitute & "','" & oDrug.RxReferenceNumber & "','Pending','" & objPrescription.RxPharmacy.PharmacyID & "','" & Number2Date(oDrug.LastFillDate) & "','" & oDrug.Notes & "','" & objPrescription.RxPrescriber.PrescriberID & "','" _
            '                                     & objPrescription.RxPatient.PatientName.FirstName & "','" & objPrescription.RxPatient.PatientName.LastName & "','" & GetPatGender(objPrescription.RxPatient.Gender) & "','" & Number2Date(objPrescription.RxPatient.DateofBirth) & "','" & objPrescription.RxPatient.PatientName.MiddleName & "','" & objPrescription.RxPatient.PatientName.Prefix & "','" & objPrescription.RxPatient.PatientName.Suffix & "','" & objPrescription.RxPatient.PatientAddress.Address1 & "','" & objPrescription.RxPatient.PatientAddress.Address2 & "','" & objPrescription.RxPatient.PatientAddress.City & "','" & objPrescription.RxPatient.PatientAddress.State & "','" & objPrescription.RxPatient.PatientAddress.Zip & "','" & objPrescription.RxPatient.PatientAddress.Phone & "','" & objPrescription.RxPatient.PatientAddress.PhQualifier & "','" & objPrescription.RxPatient.PatientAddress.Email & "','" _
            '                                     & objPrescription.RxPrescriber.PrescriberName.FirstName & "','" & objPrescription.RxPrescriber.PrescriberName.MiddleName & "','" & objPrescription.RxPrescriber.PrescriberName.LastName & "','" & objPrescription.RxPrescriber.PrescriberName.Prefix & "','" & objPrescription.RxPrescriber.PrescriberName.Suffix & "','" & objPrescription.RxPrescriber.PrescriberAddress.Address1 & "','" & objPrescription.RxPrescriber.PrescriberAddress.Address2 & "','" & objPrescription.RxPrescriber.PrescriberAddress.City & "','" & objPrescription.RxPrescriber.PrescriberAddress.State & "','" & objPrescription.RxPrescriber.PrescriberAddress.Zip & "','" & objPrescription.RxPrescriber.PrescriberAddress.Phone & "','" & objPrescription.RxPrescriber.PrescriberAddress.PhQualifier & "','" & objPrescription.RxPrescriber.PrescriberAddress.Email & "','" & objPrescription.RxPrescriber.PrescriberAgentName.FirstName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.MiddleName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.LastName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.Prefix & "','" & objPrescription.RxPrescriber.PrescriberAgentName.Suffix & "','" & objPrescription.RxPrescriber.PrescriberSpecialtyQualifier & "','" & objPrescription.RxPrescriber.PrescriberSpecialtyCode & "','" _
            '                                     & objPrescription.RxPharmacy.Pharmacyname & "','" & objPrescription.RxPharmacy.PharmacistName.FirstName & "','" & objPrescription.RxPharmacy.PharmacistName.MiddleName & "','" & objPrescription.RxPharmacy.PharmacistName.LastName & "','" & objPrescription.RxPharmacy.PharmacistName.Prefix & "','" & objPrescription.RxPharmacy.PharmacistName.Suffix & "','" & objPrescription.RxPharmacy.PharmacyAddress.Address1 & "','" & objPrescription.RxPharmacy.PharmacyAddress.Address2 & "','" & objPrescription.RxPharmacy.PharmacyAddress.City & "','" & objPrescription.RxPharmacy.PharmacyAddress.State & "','" & objPrescription.RxPharmacy.PharmacyAddress.Zip & "','" & objPrescription.RxPharmacy.PharmacyAddress.Phone & "','" & objPrescription.RxPharmacy.PharmacyAddress.PhQualifier & "','" & objPrescription.RxPharmacy.PharmacyAddress.Email & "','" & objPrescription.RxPharmacy.PharmacistAgentName.FirstName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.MiddleName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.LastName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.Prefix & "','" & objPrescription.RxPharmacy.PharmacistAgentName.Suffix & "','" _
            '                                     & oDrug.ClinicalInformationQualifier1 & "','" & oDrug.PrimaryQualifier1 & "','" & oDrug.PrimaryValue1 & "','" & oDrug.SecondaryQualifier1 & "','" & oDrug.SecondaryValue1 & "','" & oDrug.ClinicalInformationQualifier2 & "','" & oDrug.PrimaryQualifier2 & "','" & oDrug.PrimaryValue2 & "','" & oDrug.SecondaryQualifier2 & "','" & oDrug.SecondaryValue2 & "','" & oDrug.ProductCode & "','" & oDrug.ProductCodeQualifier & "','" & oDrug.DosageForm & "','" & oDrug.DrugStrengthUnits & "','" & oDrug.DrugDBCode & "','" & oDrug.DrugDBCodeQualifier & "','" & oDrug.PriorAuthorizationQualifier & "','" & oDrug.PriorAuthorizationValue & "','" & objPrescription.RxPrescriber.PrescriberClinic & "')"

            'ElseIf oDrug.WrittenDate = "" And oDrug.LastFillDate = "" Then
            '    strSQL = "INSERT INTO PrescriptionRefillTransactiondetail(nRxTransactionID,sDrugName,sDrugForm,sStrength,sDosage,sFrequency,sDuration ,sQuantity ,sQuantityQualifier,sRefillQuantity,sRefillQualifier,bMaySubstitutions,sRxReferenceNumber,sStatus,sPharmacyID,sNotes,sPrescriberID," _
            '                                                    & "sPatientFirstName,sPatientLastName,sPatientGender,dtPatientDOB,sPatientMName,sPatientPrefix,sPatientSuffix,sPatientAddress1,sPatientAddress2,sPatientCity,sPatientState,sPatientZipcode,sPatientNumber,sPatientQualifier,sPatientEmail," _
            '                                                    & "sPrFirstName,sPrMName,sPrLastName,sPrPrefix,sPrSuffix,sPrAddress1,sPrAddress2,sPrCity,sPrState,sPrZipcode,sPrNumber,sPrQualifier,sPrEmail,sPrAgentFirstName,sPrAgentMiddleName,sPrAgentLastName,sPrAgentPrefix,sPrAgentSuffix,sPrSpecialtyType,sPrSpecialtyCode," _
            '                                                    & "sPhName,sPhFirstName,sPhMName,sPhLastName,sPhPrefix,sPhSuffix,sPhAddress1,sPhAddress2,sPhCity,sPhState,sPhZipcode,sPhNumber,sPhQualifier,sPhEmail,sPhAgentFirstName,sPhAgentMName,sPhAgentLastName,sPhAgentPrefix,sPhAgentSuffix," _
            '                                                    & "sDgClQualifier1,sPrimaryQualifier1,sPrimaryValue1,sSecQualifier1,sSecValue1,sDgClQualifier2,sPrimaryQualifier2,sPrimaryValue2,sSecQualifier2,sSecValue2,sProductCode,sProductCodeQualifier,sDosageForm,sStrengthUnits,sDrugDBCode,sDrugDBCodeQualifier,sPriorAuthorizationQualifier,sPriorAuthorizationValue,sPrescriberClinic)" _
            '                                                    & " VALUES(" & objPrescription.RxTransactionID & ",'" & oDrug.DrugName & "','" & oDrug.Drugform & "','" & oDrug.DrugStrength & "','" & oDrug.Dosage & "','" & oDrug.Directions & "','" & oDrug.DrugDuration & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "','" & oDrug.RefillsQualifier & "','" & oDrug.MaySubstitute & "','" & oDrug.RxReferenceNumber & "','Pending','" & objPrescription.RxPharmacy.PharmacyID & "','" & oDrug.Notes & "','" & objPrescription.RxPrescriber.PrescriberID & "','" _
            '                                                    & objPrescription.RxPatient.PatientName.FirstName & "','" & objPrescription.RxPatient.PatientName.LastName & "','" & GetPatGender(objPrescription.RxPatient.Gender) & "','" & Number2Date(objPrescription.RxPatient.DateofBirth) & "','" & objPrescription.RxPatient.PatientName.MiddleName & "','" & objPrescription.RxPatient.PatientName.Prefix & "','" & objPrescription.RxPatient.PatientName.Suffix & "','" & objPrescription.RxPatient.PatientAddress.Address1 & "','" & objPrescription.RxPatient.PatientAddress.Address2 & "','" & objPrescription.RxPatient.PatientAddress.City & "','" & objPrescription.RxPatient.PatientAddress.State & "','" & objPrescription.RxPatient.PatientAddress.Zip & "','" & objPrescription.RxPatient.PatientAddress.Phone & "','" & objPrescription.RxPatient.PatientAddress.PhQualifier & "','" & objPrescription.RxPatient.PatientAddress.Email & "','" _
            '                                                    & objPrescription.RxPrescriber.PrescriberName.FirstName & "','" & objPrescription.RxPrescriber.PrescriberName.MiddleName & "','" & objPrescription.RxPrescriber.PrescriberName.LastName & "','" & objPrescription.RxPrescriber.PrescriberName.Prefix & "','" & objPrescription.RxPrescriber.PrescriberName.Suffix & "','" & objPrescription.RxPrescriber.PrescriberAddress.Address1 & "','" & objPrescription.RxPrescriber.PrescriberAddress.Address2 & "','" & objPrescription.RxPrescriber.PrescriberAddress.City & "','" & objPrescription.RxPrescriber.PrescriberAddress.State & "','" & objPrescription.RxPrescriber.PrescriberAddress.Zip & "','" & objPrescription.RxPrescriber.PrescriberAddress.Phone & "','" & objPrescription.RxPrescriber.PrescriberAddress.PhQualifier & "','" & objPrescription.RxPrescriber.PrescriberAddress.Email & "','" & objPrescription.RxPrescriber.PrescriberAgentName.FirstName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.MiddleName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.LastName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.Prefix & "','" & objPrescription.RxPrescriber.PrescriberAgentName.Suffix & "','" & objPrescription.RxPrescriber.PrescriberSpecialtyQualifier & "','" & objPrescription.RxPrescriber.PrescriberSpecialtyCode & "','" _
            '                                                    & objPrescription.RxPharmacy.Pharmacyname & "','" & objPrescription.RxPharmacy.PharmacistName.FirstName & "','" & objPrescription.RxPharmacy.PharmacistName.MiddleName & "','" & objPrescription.RxPharmacy.PharmacistName.LastName & "','" & objPrescription.RxPharmacy.PharmacistName.Prefix & "','" & objPrescription.RxPharmacy.PharmacistName.Suffix & "','" & objPrescription.RxPharmacy.PharmacyAddress.Address1 & "','" & objPrescription.RxPharmacy.PharmacyAddress.Address2 & "','" & objPrescription.RxPharmacy.PharmacyAddress.City & "','" & objPrescription.RxPharmacy.PharmacyAddress.State & "','" & objPrescription.RxPharmacy.PharmacyAddress.Zip & "','" & objPrescription.RxPharmacy.PharmacyAddress.Phone & "','" & objPrescription.RxPharmacy.PharmacyAddress.PhQualifier & "','" & objPrescription.RxPharmacy.PharmacyAddress.Email & "','" & objPrescription.RxPharmacy.PharmacistAgentName.FirstName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.MiddleName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.LastName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.Prefix & "','" & objPrescription.RxPharmacy.PharmacistAgentName.Suffix & "','" _
            '                                                    & oDrug.ClinicalInformationQualifier1 & "','" & oDrug.PrimaryQualifier1 & "','" & oDrug.PrimaryValue1 & "','" & oDrug.SecondaryQualifier1 & "','" & oDrug.SecondaryValue1 & "','" & oDrug.ClinicalInformationQualifier2 & "','" & oDrug.PrimaryQualifier2 & "','" & oDrug.PrimaryValue2 & "','" & oDrug.SecondaryQualifier2 & "','" & oDrug.SecondaryValue2 & "','" & oDrug.ProductCode & "','" & oDrug.ProductCodeQualifier & "','" & oDrug.DosageForm & "','" & oDrug.DrugStrengthUnits & "','" & oDrug.DrugDBCode & "','" & oDrug.DrugDBCodeQualifier & "','" & oDrug.PriorAuthorizationQualifier & "','" & oDrug.PriorAuthorizationValue & "','" & objPrescription.RxPrescriber.PrescriberClinic & "')"

            'End If

            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "sc_InsertRxRefill"
            objcmd.CommandType = CommandType.StoredProcedure

            ''sc_InsertRxRefill
            ''@nRxTransactionID varchar(35),
            ''@sDrugName varchar(50),
            ''@sDrugForm varchar(50),
            ''@sStrength varchar(50),
            ''@sDosage varchar(50),
            ''@sRoute varchar(50),
            ''@sFrequency varchar(50),
            ''@sDuration varchar(50),
            ''@sQuantity varchar(50),
            ''@sQuantityQualifier varchar(50),
            ''@sRefillQuantity varchar(50),
            ''@sRefillQualifier varchar(50),
            ''@bMaySubstitutions bit,
            ''@dtWrittendate datetime,
            ''@sRxReferenceNumber varchar(50),
            ''@sStatus varchar(50),
            ''@sPharmacyID varchar(50),
            ''@dtlastdate varchar(50),
            ''@sNotes varchar(210),
            ''@sPrescriberID varchar(35),

            ' objPrescription.RxTransactionID & ",'" & oDrug.DrugName & "','" & oDrug.Drugform & "','" & oDrug.DrugStrength & "','" & oDrug.Dosage & "',
            '" & oDrug.Directions & "','" & oDrug.DrugDuration & "','" & oDrug.DrugQuantity & "','" & oDrug.DrugQuantityQualifier & "','" & oDrug.RefillQuantity & "','" & oDrug.RefillsQualifier & "',
            '" & oDrug.MaySubstitute & "','" & Number2Date(oDrug.WrittenDate) & "','" & oDrug.RxReferenceNumber & "','Pending','" & objPrescription.RxPharmacy.PharmacyID & "','" & Number2Date(oDrug.LastFillDate) & "','" & oDrug.Notes & "','" & objPrescription.RxPrescriber.PrescriberID & "','" _


            objcmd.Parameters.Add("@nRxTransactionID", SqlDbType.VarChar)
            If objPrescription.RxTransactionID Is Nothing = False Then
                objcmd.Parameters("@nRxTransactionID").Value = objPrescription.RxTransactionID
            Else
                objcmd.Parameters("@nRxTransactionID").Value = 0
            End If

            objcmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
            If oDrug.DrugName Is Nothing = False Then
                objcmd.Parameters("@sDrugName").Value = oDrug.DrugName
            Else
                objcmd.Parameters("@sDrugName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
            If oDrug.Drugform Is Nothing = False Then
                objcmd.Parameters("@sDrugForm").Value = oDrug.Drugform
            Else
                objcmd.Parameters("@sDrugForm").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sStrength", SqlDbType.VarChar)
            If oDrug.DrugStrength Is Nothing = False Then
                objcmd.Parameters("@sStrength").Value = oDrug.DrugStrength
            Else
                objcmd.Parameters("@sStrength").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
            If oDrug.Dosage Is Nothing = False Then
                objcmd.Parameters("@sDosage").Value = oDrug.Dosage
            Else
                objcmd.Parameters("@sDosage").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
            objcmd.Parameters("@sRoute").Value = DBNull.Value

            objcmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
            If oDrug.Directions Is Nothing = False Then
                objcmd.Parameters("@sFrequency").Value = oDrug.Directions
            Else
                objcmd.Parameters("@sFrequency").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDuration", SqlDbType.VarChar)
            If oDrug.DrugDuration Is Nothing = False Then
                objcmd.Parameters("@sDuration").Value = oDrug.DrugDuration
            Else
                objcmd.Parameters("@sDuration").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sQuantity", SqlDbType.VarChar)
            If oDrug.DrugQuantity Is Nothing = False Then
                objcmd.Parameters("@sQuantity").Value = oDrug.DrugQuantity
            Else
                objcmd.Parameters("@sQuantity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sQuantityQualifier", SqlDbType.VarChar)
            If oDrug.DrugQuantityQualifier Is Nothing = False Then
                objcmd.Parameters("@sQuantityQualifier").Value = oDrug.DrugQuantityQualifier
            Else
                objcmd.Parameters("@sQuantityQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sRefillQuantity", SqlDbType.VarChar)
            If oDrug.RefillQuantity Is Nothing = False Then
                objcmd.Parameters("@sRefillQuantity").Value = oDrug.RefillQuantity
            Else
                objcmd.Parameters("@sRefillQuantity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sRefillQualifier", SqlDbType.VarChar)
            If oDrug.RefillsQualifier Is Nothing = False Then
                objcmd.Parameters("@sRefillQualifier").Value = oDrug.RefillsQualifier
            Else
                objcmd.Parameters("@sRefillQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@bMaySubstitutions", SqlDbType.Bit)
            If IsDBNull(oDrug.MaySubstitute) = False Then
                objcmd.Parameters("@bMaySubstitutions").Value = oDrug.MaySubstitute
            Else
                objcmd.Parameters("@bMaySubstitutions").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@dtWrittendate", SqlDbType.DateTime)
            If oDrug.WrittenDate Is Nothing = False Then
                If IsDBNull(Number2Date(oDrug.WrittenDate)) = False Then
                    objcmd.Parameters("@dtWrittendate").Value = Number2Date(oDrug.WrittenDate)
                Else
                    objcmd.Parameters("@dtWrittendate").Value = DBNull.Value
                End If
            Else
                objcmd.Parameters("@dtWrittendate").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sRxReferenceNumber", SqlDbType.VarChar)
            If oDrug.RxReferenceNumber Is Nothing = False Then
                objcmd.Parameters("@sRxReferenceNumber").Value = oDrug.RxReferenceNumber
            Else
                objcmd.Parameters("@sRxReferenceNumber").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
            objcmd.Parameters("@sStatus").Value = "Pending"

            objcmd.Parameters.Add("@sPharmacyID", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyID Is Nothing = False Then
                objcmd.Parameters("@sPharmacyID").Value = objPrescription.RxPharmacy.PharmacyID
            Else
                objcmd.Parameters("@sPharmacyID").Value = DBNull.Value
            End If
            ''new field
            objcmd.Parameters.Add("@sPharmacyNPI", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyNPI Is Nothing = False Then
                objcmd.Parameters("@sPharmacyNPI").Value = objPrescription.RxPharmacy.PharmacyNPI
            Else
                objcmd.Parameters("@sPharmacyNPI").Value = DBNull.Value
            End If


            objcmd.Parameters.Add("@dtlastdate", SqlDbType.DateTime)
            If oDrug.LastFillDate Is Nothing = False Then
                If IsDBNull(Number2Date(oDrug.LastFillDate)) = False Then
                    objcmd.Parameters("@dtlastdate").Value = Number2Date(oDrug.LastFillDate)
                Else
                    objcmd.Parameters("@dtlastdate").Value = DBNull.Value
                End If
            Else
                objcmd.Parameters("@dtlastdate").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sNotes", SqlDbType.VarChar)
            If oDrug.Notes Is Nothing = False Then
                objcmd.Parameters("@sNotes").Value = oDrug.Notes
            Else
                objcmd.Parameters("@sNotes").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrescriberID", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberID Is Nothing = False Then
                objcmd.Parameters("@sPrescriberID").Value = objPrescription.RxPrescriber.PrescriberID
            Else
                objcmd.Parameters("@sPrescriberID").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrescriberNPI", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberNPI Is Nothing = False Then
                objcmd.Parameters("@sPrescriberNPI").Value = objPrescription.RxPrescriber.PrescriberNPI
            Else
                objcmd.Parameters("@sPrescriberNPI").Value = DBNull.Value
            End If


            '' @sPatientFirstName varchar(50),
            '' @sPatientLastName varchar(50),
            '' @sPatientGender varchar(50),
            '' @dtPatientDOB datetime,
            '' @sPatientMName varchar(50),
            '' @sPatientPrefix varchar(10),
            '' @sPatientSuffix varchar(10),
            '' @sPatientAddress1 varchar(50),
            '' @sPatientAddress2 varchar(50),
            '' @sPatientCity varchar(50),
            '' @sPatientState varchar(50),
            '' @sPatientZipcode varchar(50),
            '' @sPatientNumber varchar(25),
            '' @sPatientQualifier varchar(3),
            '' @sPatientEmail varchar(80),

            '& objPrescription.RxPatient.PatientName.FirstName & "','" & objPrescription.RxPatient.PatientName.LastName & "','" & GetPatGender(objPrescription.RxPatient.Gender) & "','" & Number2Date(objPrescription.RxPatient.DateofBirth) & "','" & objPrescription.RxPatient.PatientName.MiddleName & "','" & objPrescription.RxPatient.PatientName.Prefix & "','" & objPrescription.RxPatient.PatientName.Suffix & "','
            '" & objPrescription.RxPatient.PatientAddress.Address1 & "','" & objPrescription.RxPatient.PatientAddress.Address2 & "','" & objPrescription.RxPatient.PatientAddress.City & "','" & objPrescription.RxPatient.PatientAddress.State & "','" & objPrescription.RxPatient.PatientAddress.Zip & "',
            '" & objPrescription.RxPatient.PatientAddress.Phone & "','" & objPrescription.RxPatient.PatientAddress.PhQualifier & "','" & objPrescription.RxPatient.PatientAddress.Email & "','" _

            objcmd.Parameters.Add("@sPatientFirstName", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.FirstName Is Nothing = False Then
                objcmd.Parameters("@sPatientFirstName").Value = objPrescription.RxPatient.PatientName.FirstName
            Else
                objcmd.Parameters("@sPatientFirstName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientLastName", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.LastName Is Nothing = False Then
                objcmd.Parameters("@sPatientLastName").Value = objPrescription.RxPatient.PatientName.LastName
            Else
                objcmd.Parameters("@sPatientLastName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientMName", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.MiddleName Is Nothing = False Then
                objcmd.Parameters("@sPatientMName").Value = objPrescription.RxPatient.PatientName.MiddleName
            Else
                objcmd.Parameters("@sPatientMName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientPrefix", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.Prefix Is Nothing = False Then
                objcmd.Parameters("@sPatientPrefix").Value = objPrescription.RxPatient.PatientName.Prefix
            Else
                objcmd.Parameters("@sPatientPrefix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientSuffix", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.Suffix Is Nothing = False Then
                objcmd.Parameters("@sPatientSuffix").Value = objPrescription.RxPatient.PatientName.Suffix
            Else
                objcmd.Parameters("@sPatientSuffix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientGender", SqlDbType.VarChar)
            If objPrescription.RxPatient.Gender Is Nothing = False Then
                objcmd.Parameters("@sPatientGender").Value = objPrescription.RxPatient.Gender
            Else
                objcmd.Parameters("@sPatientGender").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@dtPatientDOB", SqlDbType.DateTime)
            If objPrescription.RxPatient.DateofBirth Is Nothing = False Then
                If IsDBNull(Number2Date(objPrescription.RxPatient.DateofBirth)) = False Then
                    objcmd.Parameters("@dtPatientDOB").Value = Number2Date(objPrescription.RxPatient.DateofBirth)
                Else
                    objcmd.Parameters("@dtPatientDOB").Value = DBNull.Value
                End If
            Else
                objcmd.Parameters("@dtPatientDOB").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientAddress1", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Address1 Is Nothing = False Then
                objcmd.Parameters("@sPatientAddress1").Value = objPrescription.RxPatient.PatientAddress.Address1
            Else
                objcmd.Parameters("@sPatientAddress1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientAddress2", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Address2 Is Nothing = False Then
                objcmd.Parameters("@sPatientAddress2").Value = objPrescription.RxPatient.PatientAddress.Address2
            Else
                objcmd.Parameters("@sPatientAddress2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientCity", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.City Is Nothing = False Then
                objcmd.Parameters("@sPatientCity").Value = objPrescription.RxPatient.PatientAddress.City
            Else
                objcmd.Parameters("@sPatientCity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientState", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.State Is Nothing = False Then
                objcmd.Parameters("@sPatientState").Value = objPrescription.RxPatient.PatientAddress.State
            Else
                objcmd.Parameters("@sPatientState").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientZipcode", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Zip Is Nothing = False Then
                objcmd.Parameters("@sPatientZipcode").Value = objPrescription.RxPatient.PatientAddress.Zip
            Else
                objcmd.Parameters("@sPatientZipcode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientNumber", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Phone Is Nothing = False Then
                objcmd.Parameters("@sPatientNumber").Value = objPrescription.RxPatient.PatientAddress.Phone
            Else
                objcmd.Parameters("@sPatientNumber").Value = DBNull.Value
            End If
            objcmd.Parameters.Add("@sPatientQualifier", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.PhQualifier Is Nothing = False Then
                objcmd.Parameters("@sPatientQualifier").Value = objPrescription.RxPatient.PatientAddress.PhQualifier
            Else
                objcmd.Parameters("@sPatientQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientEmail", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Email Is Nothing = False Then
                objcmd.Parameters("@sPatientEmail").Value = objPrescription.RxPatient.PatientAddress.Email
            Else
                objcmd.Parameters("@sPatientEmail").Value = DBNull.Value
            End If

            ''@sPrFirstName varchar(50),
            ''@sPrMName varchar(50),
            ''@sPrLastName varchar(50),
            ''@sPrPrefix varchar(50),
            ''@sPrSuffix varchar(10),
            ''@sPrAddress1 varchar(50),
            ''@sPrAddress2 varchar(50),
            ''@sPrCity varchar(50),
            ''@sPrState varchar(50),
            ''@sPrZipcode varchar(50),
            ''@sPrNumber varchar(25),
            ''@sPrQualifier varchar(3),
            ''@sPrEmail varchar(80),
            ''@sPrAgentFirstName varchar(50),
            ''@sPrAgentMiddleName varchar(50),
            ''@sPrAgentLastName varchar(50),
            ''@sPrAgentPrefix varchar(10),
            ''@sPrAgentSuffix varchar(10),
            ''@sPrSpecialtyType varchar(10),
            ''@sPrSpecialtyCode varchar(10),

            '& objPrescription.RxPrescriber.PrescriberName.FirstName & "','" & objPrescription.RxPrescriber.PrescriberName.MiddleName & "','" & objPrescription.RxPrescriber.PrescriberName.LastName & "','" & objPrescription.RxPrescriber.PrescriberName.Prefix & "','" & objPrescription.RxPrescriber.PrescriberName.Suffix & "',
            '" & objPrescription.RxPrescriber.PrescriberAddress.Address1 & "','" & objPrescription.RxPrescriber.PrescriberAddress.Address2 & "','" & objPrescription.RxPrescriber.PrescriberAddress.City & "','" & objPrescription.RxPrescriber.PrescriberAddress.State & "','" & objPrescription.RxPrescriber.PrescriberAddress.Zip & "',
            '" & objPrescription.RxPrescriber.PrescriberAddress.Phone & "','" & objPrescription.RxPrescriber.PrescriberAddress.PhQualifier & "','" & objPrescription.RxPrescriber.PrescriberAddress.Email & "','" & objPrescription.RxPrescriber.PrescriberAgentName.FirstName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.MiddleName & "',
            '" & objPrescription.RxPrescriber.PrescriberAgentName.LastName & "','" & objPrescription.RxPrescriber.PrescriberAgentName.Prefix & "','" & objPrescription.RxPrescriber.PrescriberAgentName.Suffix & "','" & objPrescription.RxPrescriber.PrescriberSpecialtyQualifier & "','" & objPrescription.RxPrescriber.PrescriberSpecialtyCode & "','" _


            objcmd.Parameters.Add("@sPrFirstName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberName.FirstName Is Nothing = False Then
                objcmd.Parameters("@sPrFirstName").Value = objPrescription.RxPrescriber.PrescriberName.FirstName
            Else
                objcmd.Parameters("@sPrFirstName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrLastName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberName.LastName Is Nothing = False Then
                objcmd.Parameters("@sPrLastName").Value = objPrescription.RxPrescriber.PrescriberName.LastName
            Else
                objcmd.Parameters("@sPrLastName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrMName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberName.MiddleName Is Nothing = False Then
                objcmd.Parameters("@sPrMName").Value = objPrescription.RxPrescriber.PrescriberName.MiddleName
            Else
                objcmd.Parameters("@sPrMName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrPrefix", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberName.Prefix Is Nothing = False Then
                objcmd.Parameters("@sPrPrefix").Value = objPrescription.RxPrescriber.PrescriberName.Prefix
            Else
                objcmd.Parameters("@sPrPrefix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrSuffix", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberName.Suffix Is Nothing = False Then
                objcmd.Parameters("@sPrSuffix").Value = objPrescription.RxPrescriber.PrescriberName.Suffix
            Else
                objcmd.Parameters("@sPrSuffix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAddress1", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Address1 Is Nothing = False Then
                objcmd.Parameters("@sPrAddress1").Value = objPrescription.RxPrescriber.PrescriberAddress.Address1
            Else
                objcmd.Parameters("@sPrAddress1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAddress2", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Address2 Is Nothing = False Then
                objcmd.Parameters("@sPrAddress2").Value = objPrescription.RxPrescriber.PrescriberAddress.Address2
            Else
                objcmd.Parameters("@sPrAddress2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrCity", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.City Is Nothing = False Then
                objcmd.Parameters("@sPrCity").Value = objPrescription.RxPrescriber.PrescriberAddress.City
            Else
                objcmd.Parameters("@sPrCity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrState", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.State Is Nothing = False Then
                objcmd.Parameters("@sPrState").Value = objPrescription.RxPrescriber.PrescriberAddress.State
            Else
                objcmd.Parameters("@sPrState").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrZipcode", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Zip Is Nothing = False Then
                objcmd.Parameters("@sPrZipcode").Value = objPrescription.RxPrescriber.PrescriberAddress.Zip
            Else
                objcmd.Parameters("@sPrZipcode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrNumber", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Phone Is Nothing = False Then
                objcmd.Parameters("@sPrNumber").Value = objPrescription.RxPrescriber.PrescriberAddress.Phone
            Else
                objcmd.Parameters("@sPrNumber").Value = DBNull.Value
            End If
            objcmd.Parameters.Add("@sPrQualifier", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.PhQualifier Is Nothing = False Then
                objcmd.Parameters("@sPrQualifier").Value = objPrescription.RxPrescriber.PrescriberAddress.PhQualifier
            Else
                objcmd.Parameters("@sPrQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrEmail", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Email Is Nothing = False Then
                objcmd.Parameters("@sPrEmail").Value = objPrescription.RxPrescriber.PrescriberAddress.Email
            Else
                objcmd.Parameters("@sPrEmail").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAgentFirstName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAgentName.FirstName Is Nothing = False Then
                objcmd.Parameters("@sPrAgentFirstName").Value = objPrescription.RxPrescriber.PrescriberAgentName.FirstName
            Else
                objcmd.Parameters("@sPrAgentFirstName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAgentLastName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAgentName.LastName Is Nothing = False Then
                objcmd.Parameters("@sPrAgentLastName").Value = objPrescription.RxPrescriber.PrescriberAgentName.LastName
            Else
                objcmd.Parameters("@sPrAgentLastName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAgentMiddleName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAgentName.MiddleName Is Nothing = False Then
                objcmd.Parameters("@sPrAgentMiddleName").Value = objPrescription.RxPrescriber.PrescriberAgentName.MiddleName
            Else
                objcmd.Parameters("@sPrAgentMiddleName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAgentPrefix", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAgentName.Prefix Is Nothing = False Then
                objcmd.Parameters("@sPrAgentPrefix").Value = objPrescription.RxPrescriber.PrescriberAgentName.Prefix
            Else
                objcmd.Parameters("@sPrAgentPrefix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAgentSuffix", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAgentName.Suffix Is Nothing = False Then
                objcmd.Parameters("@sPrAgentSuffix").Value = objPrescription.RxPrescriber.PrescriberAgentName.Suffix
            Else
                objcmd.Parameters("@sPrAgentSuffix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrSpecialtyType", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberSpecialtyQualifier Is Nothing = False Then
                objcmd.Parameters("@sPrSpecialtyType").Value = objPrescription.RxPrescriber.PrescriberSpecialtyQualifier
            Else
                objcmd.Parameters("@sPrSpecialtyType").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrSpecialtyCode", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberSpecialtyCode Is Nothing = False Then
                objcmd.Parameters("@sPrSpecialtyCode").Value = objPrescription.RxPrescriber.PrescriberSpecialtyCode
            Else
                objcmd.Parameters("@sPrSpecialtyCode").Value = DBNull.Value
            End If

            ''@sPhName varchar(50),
            ''@sPhFirstName varchar(50),
            ''@sPhMName varchar(50),
            ''@sPhLastName varchar(50),
            ''@sPhPrefix varchar(10),
            ''@sPhSuffix varchar(10),
            ''@sPhAddress1 varchar(50),
            ''@sPhAddress2 varchar(50),
            ''@sPhCity varchar(50),
            ''@sPhState varchar(50),
            ''@sPhZipcode varchar(50),
            ''@sPhNumber varchar(50),
            ''@sPhQualifier varchar(3),
            ''@sPhEmail varchar(80),
            ''@sPhAgentFirstName varchar(50),
            ''@sPhAgentMName varchar(50),
            ''@sPhAgentLastName varchar(50),
            ''@sPhAgentPrefix varchar(10),
            ''@sPhAgentSuffix varchar(10),

            '& objPrescription.RxPharmacy.Pharmacyname & "','" & objPrescription.RxPharmacy.PharmacistName.FirstName & "','" & objPrescription.RxPharmacy.PharmacistName.MiddleName & "','" & objPrescription.RxPharmacy.PharmacistName.LastName & "','" & objPrescription.RxPharmacy.PharmacistName.Prefix & "','" & objPrescription.RxPharmacy.PharmacistName.Suffix & "',
            '" & objPrescription.RxPharmacy.PharmacyAddress.Address1 & "','" & objPrescription.RxPharmacy.PharmacyAddress.Address2 & "','" & objPrescription.RxPharmacy.PharmacyAddress.City & "','" & objPrescription.RxPharmacy.PharmacyAddress.State & "','" & objPrescription.RxPharmacy.PharmacyAddress.Zip & "','" & objPrescription.RxPharmacy.PharmacyAddress.Phone & "',
            '" & objPrescription.RxPharmacy.PharmacyAddress.PhQualifier & "','" & objPrescription.RxPharmacy.PharmacyAddress.Email & "','" & objPrescription.RxPharmacy.PharmacistAgentName.FirstName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.MiddleName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.LastName & "','" & objPrescription.RxPharmacy.PharmacistAgentName.Prefix & "','" & objPrescription.RxPharmacy.PharmacistAgentName.Suffix & "','" _

            objcmd.Parameters.Add("@sPhName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.Pharmacyname Is Nothing = False Then
                objcmd.Parameters("@sPhName").Value = objPrescription.RxPharmacy.Pharmacyname
            Else
                objcmd.Parameters("@sPhName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhFirstName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistName.FirstName Is Nothing = False Then
                objcmd.Parameters("@sPhFirstName").Value = objPrescription.RxPharmacy.PharmacistName.FirstName
            Else
                objcmd.Parameters("@sPhFirstName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhLastName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistName.LastName Is Nothing = False Then
                objcmd.Parameters("@sPhLastName").Value = objPrescription.RxPharmacy.PharmacistName.LastName
            Else
                objcmd.Parameters("@sPhLastName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhMName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistName.MiddleName Is Nothing = False Then
                objcmd.Parameters("@sPhMName").Value = objPrescription.RxPharmacy.PharmacistName.MiddleName
            Else
                objcmd.Parameters("@sPhMName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhPrefix", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistName.Prefix Is Nothing = False Then
                objcmd.Parameters("@sPhPrefix").Value = objPrescription.RxPharmacy.PharmacistName.Prefix
            Else
                objcmd.Parameters("@sPhPrefix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhSuffix", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistName.Suffix Is Nothing = False Then
                objcmd.Parameters("@sPhSuffix").Value = objPrescription.RxPharmacy.PharmacistName.Suffix
            Else
                objcmd.Parameters("@sPhSuffix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAddress1", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Address1 Is Nothing = False Then
                objcmd.Parameters("@sPhAddress1").Value = objPrescription.RxPharmacy.PharmacyAddress.Address1
            Else
                objcmd.Parameters("@sPhAddress1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAddress2", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Address2 Is Nothing = False Then
                objcmd.Parameters("@sPhAddress2").Value = objPrescription.RxPharmacy.PharmacyAddress.Address2
            Else
                objcmd.Parameters("@sPhAddress2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhCity", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.City Is Nothing = False Then
                objcmd.Parameters("@sPhCity").Value = objPrescription.RxPharmacy.PharmacyAddress.City
            Else
                objcmd.Parameters("@sPhCity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhState", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.State Is Nothing = False Then
                objcmd.Parameters("@sPhState").Value = objPrescription.RxPharmacy.PharmacyAddress.State
            Else
                objcmd.Parameters("@sPhState").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhZipcode", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Zip Is Nothing = False Then
                objcmd.Parameters("@sPhZipcode").Value = objPrescription.RxPharmacy.PharmacyAddress.Zip
            Else
                objcmd.Parameters("@sPhZipcode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhNumber", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Phone Is Nothing = False Then
                objcmd.Parameters("@sPhNumber").Value = objPrescription.RxPharmacy.PharmacyAddress.Phone
            Else
                objcmd.Parameters("@sPhNumber").Value = DBNull.Value
            End If
            objcmd.Parameters.Add("@sPhQualifier", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.PhQualifier Is Nothing = False Then
                objcmd.Parameters("@sPhQualifier").Value = objPrescription.RxPharmacy.PharmacyAddress.PhQualifier
            Else
                objcmd.Parameters("@sPhQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhEmail", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Email Is Nothing = False Then
                objcmd.Parameters("@sPhEmail").Value = objPrescription.RxPharmacy.PharmacyAddress.Email
            Else
                objcmd.Parameters("@sPhEmail").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAgentFirstName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistAgentName.FirstName Is Nothing = False Then
                objcmd.Parameters("@sPhAgentFirstName").Value = objPrescription.RxPharmacy.PharmacistAgentName.FirstName
            Else
                objcmd.Parameters("@sPhAgentFirstName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAgentLastName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistAgentName.LastName Is Nothing = False Then
                objcmd.Parameters("@sPhAgentLastName").Value = objPrescription.RxPharmacy.PharmacistAgentName.LastName
            Else
                objcmd.Parameters("@sPhAgentLastName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAgentMName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistAgentName.MiddleName Is Nothing = False Then
                objcmd.Parameters("@sPhAgentMName").Value = objPrescription.RxPharmacy.PharmacistAgentName.MiddleName
            Else
                objcmd.Parameters("@sPhAgentMName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAgentPrefix", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistAgentName.Prefix Is Nothing = False Then
                objcmd.Parameters("@sPhAgentPrefix").Value = objPrescription.RxPharmacy.PharmacistAgentName.Prefix
            Else
                objcmd.Parameters("@sPhAgentPrefix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAgentSuffix", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistAgentName.Suffix Is Nothing = False Then
                objcmd.Parameters("@sPhAgentSuffix").Value = objPrescription.RxPharmacy.PharmacistAgentName.Suffix
            Else
                objcmd.Parameters("@sPhAgentSuffix").Value = DBNull.Value
            End If

            ''New fields
            objcmd.Parameters.Add("@sCodeListQualifier", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.CodeListQualifier Is Nothing = False Then
                objcmd.Parameters("@sCodeListQualifier").Value = objPrescription.RxPharmacy.CodeListQualifier
            Else
                objcmd.Parameters("@sCodeListQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sUnitSourceCode", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.UnitSourceCode Is Nothing = False Then
                objcmd.Parameters("@sUnitSourceCode").Value = objPrescription.RxPharmacy.UnitSourceCode
            Else
                objcmd.Parameters("@sUnitSourceCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPotencyUnitCode", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PotencyUnitCode Is Nothing = False Then
                objcmd.Parameters("@sPotencyUnitCode").Value = objPrescription.RxPharmacy.PotencyUnitCode
            Else
                objcmd.Parameters("@sPotencyUnitCode").Value = DBNull.Value
            End If

            ''---x-------
            ''@sDgClQualifier1 varchar(50),
            ''@sPrimaryQualifier1 varchar(10),
            ''@sPrimaryValue1 varchar(17),
            ''@sSecQualifier1 varchar(10),
            ''@sSecValue1 varchar(17),
            ''@sDgClQualifier2 varchar(50),
            ''@sPrimaryQualifier2 varchar(10),
            ''@sPrimaryValue2 varchar(17),
            ''@sSecQualifier2 varchar(10),
            ''@sSecValue2 varchar(17),
            ''@sProductCode varchar(35),
            ''@sProductCodeQualifier varchar(3),
            ''@sDosageForm varchar(50),
            ''@sStrengthUnits varchar(5),
            ''@sDrugDBCode varchar(35),
            ''@sDrugDBCodeQualifier varchar(3),
            ''@sPriorAuthorizationQualifier varchar(3),
            ''@sPriorAuthorizationValue varchar(35),
            ''@sPrescriberClinic varchar(35),
            ''@sMessageID varchar(35)

            '& oDrug.ClinicalInformationQualifier1 & "','" & oDrug.PrimaryQualifier1 & "','" & oDrug.PrimaryValue1 & "','" & oDrug.SecondaryQualifier1 & "','" & oDrug.SecondaryValue1 & "',
            '" & oDrug.ClinicalInformationQualifier2 & "','" & oDrug.PrimaryQualifier2 & "','" & oDrug.PrimaryValue2 & "','" & oDrug.SecondaryQualifier2 & "','" & oDrug.SecondaryValue2 & "',
            '" & oDrug.ProductCode & "','" & oDrug.ProductCodeQualifier & "','" & oDrug.DosageForm & "','" & oDrug.DrugStrengthUnits & "','" & oDrug.DrugDBCode & "','" & oDrug.DrugDBCodeQualifier & "',
            '" & oDrug.PriorAuthorizationQualifier & "','" & oDrug.PriorAuthorizationValue & "','" & objPrescription.RxPrescriber.PrescriberClinic & "')"


            'New fields MD drug  -----------
            objcmd.Parameters.Add("@sMDDrugName", SqlDbType.VarChar)
            If oDrug.MDDrugName Is Nothing = False Then
                objcmd.Parameters("@sMDDrugName").Value = oDrug.MDDrugName
            Else
                objcmd.Parameters("@sMDDrugName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugQuantity", SqlDbType.VarChar)
            If oDrug.MDDrugQuantity Is Nothing = False Then
                objcmd.Parameters("@sMDDrugQuantity").Value = oDrug.MDDrugQuantity
            Else
                objcmd.Parameters("@sMDDrugQuantity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugQualifier", SqlDbType.VarChar)
            If oDrug.MDDrugQualifier Is Nothing = False Then
                objcmd.Parameters("@sMDDrugQualifier").Value = oDrug.MDDrugQualifier
            Else
                objcmd.Parameters("@sMDDrugQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDRefillQuantity", SqlDbType.VarChar)
            If oDrug.MDRefillQuantity Is Nothing = False Then
                objcmd.Parameters("@sMDRefillQuantity").Value = oDrug.MDRefillQuantity
            Else
                objcmd.Parameters("@sMDRefillQuantity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDRefillsQualifier", SqlDbType.VarChar)
            If oDrug.MDRefillsQualifier Is Nothing = False Then
                objcmd.Parameters("@sMDRefillsQualifier").Value = oDrug.MDRefillsQualifier
            Else
                objcmd.Parameters("@sMDRefillsQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@MDdtWrittenDate", SqlDbType.VarChar)
            If oDrug.MDdtWrittenDate Is Nothing = False Then
                objcmd.Parameters("@MDdtWrittenDate").Value = oDrug.MDdtWrittenDate
            Else
                objcmd.Parameters("@MDdtWrittenDate").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugDuration", SqlDbType.VarChar)
            If oDrug.MDDrugDuration Is Nothing = False Then
                objcmd.Parameters("@sMDDrugDuration").Value = oDrug.MDDrugDuration
            Else
                objcmd.Parameters("@sMDDrugDuration").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugDirections", SqlDbType.VarChar)
            If oDrug.MDDrugDirections Is Nothing = False Then
                objcmd.Parameters("@sMDDrugDirections").Value = oDrug.MDDrugDirections
            Else
                objcmd.Parameters("@sMDDrugDirections").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@MDbIsSubstituitons", SqlDbType.Bit)
            If IsNothing(oDrug.MDbIsSubstituitons) = False Then
                objcmd.Parameters("@MDbIsSubstituitons").Value = oDrug.MDbIsSubstituitons
            Else
                objcmd.Parameters("@MDbIsSubstituitons").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDNotes", SqlDbType.VarChar)
            If oDrug.MDNotes Is Nothing = False Then
                objcmd.Parameters("@sMDNotes").Value = oDrug.MDNotes
            Else
                objcmd.Parameters("@sMDNotes").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@MDdtLastFillDate", SqlDbType.VarChar)
            If oDrug.MDdtLastFillDate Is Nothing = False Then
                objcmd.Parameters("@MDdtLastFillDate").Value = oDrug.MDdtLastFillDate
            Else
                objcmd.Parameters("@MDdtLastFillDate").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDProductCode", SqlDbType.VarChar)
            If oDrug.MDProductCode Is Nothing = False Then
                objcmd.Parameters("@sMDProductCode").Value = oDrug.MDProductCode
            Else
                objcmd.Parameters("@sMDProductCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDProductCodeQualifier", SqlDbType.VarChar)
            If oDrug.MDProductCodeQualifier Is Nothing = False Then
                objcmd.Parameters("@sMDProductCodeQualifier").Value = oDrug.MDProductCodeQualifier
            Else
                objcmd.Parameters("@sMDProductCodeQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDosageForm", SqlDbType.VarChar)
            If oDrug.MDDosageForm Is Nothing = False Then
                objcmd.Parameters("@sMDDosageForm").Value = oDrug.MDDosageForm
            Else
                objcmd.Parameters("@sMDDosageForm").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugStrength", SqlDbType.VarChar)
            If oDrug.MDDrugStrength Is Nothing = False Then
                objcmd.Parameters("@sMDDrugStrength").Value = oDrug.MDDrugStrength
            Else
                objcmd.Parameters("@sMDDrugStrength").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugStrengthUnits", SqlDbType.VarChar)
            If oDrug.MDDrugStrengthUnits Is Nothing = False Then
                objcmd.Parameters("@sMDDrugStrengthUnits").Value = oDrug.MDDrugStrengthUnits
            Else
                objcmd.Parameters("@sMDDrugStrengthUnits").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDCodeListQualifier", SqlDbType.VarChar)
            If oDrug.MDCodeListQualifier Is Nothing = False Then
                objcmd.Parameters("@sMDCodeListQualifier").Value = oDrug.MDCodeListQualifier
            Else
                objcmd.Parameters("@sMDCodeListQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDUnitSourceCode", SqlDbType.VarChar)
            If oDrug.MDUnitSourceCode Is Nothing = False Then
                objcmd.Parameters("@sMDUnitSourceCode").Value = oDrug.MDUnitSourceCode
            Else
                objcmd.Parameters("@sMDUnitSourceCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDPotencyUnitCode", SqlDbType.VarChar)
            If oDrug.MDPotencyUnitCode Is Nothing = False Then
                objcmd.Parameters("@sMDPotencyUnitCode").Value = oDrug.MDPotencyUnitCode
            Else
                objcmd.Parameters("@sMDPotencyUnitCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugDBCode", SqlDbType.VarChar)
            If oDrug.MDDrugDBCode Is Nothing = False Then
                objcmd.Parameters("@sMDDrugDBCode").Value = oDrug.MDDrugDBCode
            Else
                objcmd.Parameters("@sMDDrugDBCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugDBCodeQualifier", SqlDbType.VarChar)
            If oDrug.MDDrugDBCodeQualifier Is Nothing = False Then
                objcmd.Parameters("@sMDDrugDBCodeQualifier").Value = oDrug.MDDrugDBCodeQualifier
            Else
                objcmd.Parameters("@sMDDrugDBCodeQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDrugCoverageStatusCode", SqlDbType.VarChar)
            If oDrug.DrugCoverageStatusCode Is Nothing = False Then
                objcmd.Parameters("@sDrugCoverageStatusCode").Value = oDrug.DrugCoverageStatusCode
            Else
                objcmd.Parameters("@sDrugCoverageStatusCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugCoverageStatusCode", SqlDbType.VarChar)
            If oDrug.MDDrugCoverageStatusCode Is Nothing = False Then
                objcmd.Parameters("@sMDDrugCoverageStatusCode").Value = oDrug.MDDrugCoverageStatusCode
            Else
                objcmd.Parameters("@sMDDrugCoverageStatusCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPharmacySpecialty", SqlDbType.VarChar)
            If oDrug.PharmacySpecialty Is Nothing = False Then
                objcmd.Parameters("@sPharmacySpecialty").Value = oDrug.PharmacySpecialty
            Else
                objcmd.Parameters("@sPharmacySpecialty").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientRelationship", SqlDbType.VarChar)
            If oDrug.PatientRelationship Is Nothing = False Then
                objcmd.Parameters("@sPatientRelationship").Value = oDrug.PatientRelationship
            Else
                objcmd.Parameters("@sPatientRelationship").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrescriberSSN", SqlDbType.VarChar)
            If oDrug.PrescriberSSN Is Nothing = False Then
                objcmd.Parameters("@sPrescriberSSN").Value = oDrug.PrescriberSSN
            Else
                objcmd.Parameters("@sPrescriberSSN").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@FileData", SqlDbType.VarBinary)
            If oDrug.FileData Is Nothing = False Then
                objcmd.Parameters("@FileData").Value = oDrug.FileData
            Else
                objcmd.Parameters("@FileData").Value = DBNull.Value
            End If

            '--xx------------------------------------x----






            objcmd.Parameters.Add("@sDgClQualifier1", SqlDbType.VarChar)
            If oDrug.ClinicalInformationQualifier1 Is Nothing = False Then
                objcmd.Parameters("@sDgClQualifier1").Value = oDrug.ClinicalInformationQualifier1
            Else
                objcmd.Parameters("@sDgClQualifier1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrimaryQualifier1", SqlDbType.VarChar)
            If oDrug.PrimaryQualifier1 Is Nothing = False Then
                objcmd.Parameters("@sPrimaryQualifier1").Value = oDrug.PrimaryQualifier1
            Else
                objcmd.Parameters("@sPrimaryQualifier1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrimaryValue1", SqlDbType.VarChar)
            If oDrug.PrimaryValue1 Is Nothing = False Then
                objcmd.Parameters("@sPrimaryValue1").Value = oDrug.PrimaryValue1
            Else
                objcmd.Parameters("@sPrimaryValue1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sSecQualifier1", SqlDbType.VarChar)
            If oDrug.SecondaryQualifier1 Is Nothing = False Then
                objcmd.Parameters("@sSecQualifier1").Value = oDrug.SecondaryQualifier1
            Else
                objcmd.Parameters("@sSecQualifier1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sSecValue1", SqlDbType.VarChar)
            If oDrug.SecondaryValue1 Is Nothing = False Then
                objcmd.Parameters("@sSecValue1").Value = oDrug.SecondaryValue1
            Else
                objcmd.Parameters("@sSecValue1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDgClQualifier2", SqlDbType.VarChar)
            If oDrug.ClinicalInformationQualifier2 Is Nothing = False Then
                objcmd.Parameters("@sDgClQualifier2").Value = oDrug.ClinicalInformationQualifier2
            Else
                objcmd.Parameters("@sDgClQualifier2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrimaryQualifier2", SqlDbType.VarChar)
            If oDrug.PrimaryQualifier2 Is Nothing = False Then
                objcmd.Parameters("@sPrimaryQualifier2").Value = oDrug.PrimaryQualifier2
            Else
                objcmd.Parameters("@sPrimaryQualifier2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrimaryValue2", SqlDbType.VarChar)
            If oDrug.PrimaryValue2 Is Nothing = False Then
                objcmd.Parameters("@sPrimaryValue2").Value = oDrug.PrimaryValue2
            Else
                objcmd.Parameters("@sPrimaryValue2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sSecQualifier2", SqlDbType.VarChar)
            If oDrug.SecondaryQualifier2 Is Nothing = False Then
                objcmd.Parameters("@sSecQualifier2").Value = oDrug.SecondaryQualifier2
            Else
                objcmd.Parameters("@sSecQualifier2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sSecValue2", SqlDbType.VarChar)
            If oDrug.SecondaryValue2 Is Nothing = False Then
                objcmd.Parameters("@sSecValue2").Value = oDrug.SecondaryValue2
            Else
                objcmd.Parameters("@sSecValue2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sProductCode", SqlDbType.VarChar)
            If oDrug.ProductCode Is Nothing = False Then
                objcmd.Parameters("@sProductCode").Value = oDrug.ProductCode
            Else
                objcmd.Parameters("@sProductCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sProductCodeQualifier", SqlDbType.VarChar)
            If oDrug.ProductCodeQualifier Is Nothing = False Then
                objcmd.Parameters("@sProductCodeQualifier").Value = oDrug.ProductCodeQualifier
            Else
                objcmd.Parameters("@sProductCodeQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDosageForm", SqlDbType.VarChar)
            If oDrug.DosageForm Is Nothing = False Then
                objcmd.Parameters("@sDosageForm").Value = oDrug.DosageForm
            Else
                objcmd.Parameters("@sDosageForm").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sStrengthUnits", SqlDbType.VarChar)
            If oDrug.DrugStrengthUnits Is Nothing = False Then
                objcmd.Parameters("@sStrengthUnits").Value = oDrug.DrugStrengthUnits
            Else
                objcmd.Parameters("@sStrengthUnits").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDrugDBCode", SqlDbType.VarChar)
            If oDrug.DrugDBCode Is Nothing = False Then
                objcmd.Parameters("@sDrugDBCode").Value = oDrug.DrugDBCode
            Else
                objcmd.Parameters("@sDrugDBCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDrugDBCodeQualifier", SqlDbType.VarChar)
            If oDrug.DrugDBCodeQualifier Is Nothing = False Then
                objcmd.Parameters("@sDrugDBCodeQualifier").Value = oDrug.DrugDBCodeQualifier
            Else
                objcmd.Parameters("@sDrugDBCodeQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPriorAuthorizationQualifier", SqlDbType.VarChar)
            If oDrug.PriorAuthorizationQualifier Is Nothing = False Then
                objcmd.Parameters("@sPriorAuthorizationQualifier").Value = oDrug.PriorAuthorizationQualifier
            Else
                objcmd.Parameters("@sPriorAuthorizationQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPriorAuthorizationValue", SqlDbType.VarChar)
            If oDrug.PriorAuthorizationValue Is Nothing = False Then
                objcmd.Parameters("@sPriorAuthorizationValue").Value = oDrug.PriorAuthorizationValue
            Else
                objcmd.Parameters("@sPriorAuthorizationValue").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrescriberClinic", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberClinic Is Nothing = False Then
                objcmd.Parameters("@sPrescriberClinic").Value = objPrescription.RxPrescriber.PrescriberClinic
            Else
                objcmd.Parameters("@sPrescriberClinic").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMessageID", SqlDbType.VarChar)
            If oDrug.MessageID Is Nothing = False Then
                objcmd.Parameters("@sMessageID").Value = oDrug.MessageID
            Else
                objcmd.Parameters("@sMessageID").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhFax", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Fax Is Nothing = False Then
                objcmd.Parameters("@sPhFax").Value = objPrescription.RxPharmacy.PharmacyAddress.Fax
            Else
                objcmd.Parameters("@sPhFax").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrFax", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Fax Is Nothing = False Then
                objcmd.Parameters("@sPrFax").Value = objPrescription.RxPrescriber.PrescriberAddress.Fax
            Else
                objcmd.Parameters("@sPrFax").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientFax", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Fax Is Nothing = False Then
                objcmd.Parameters("@sPatientFax").Value = objPrescription.RxPatient.PatientAddress.Fax
            Else
                objcmd.Parameters("@sPatientFax").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientWorkPhone", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.WorkPhone Is Nothing = False Then
                objcmd.Parameters("@sPatientWorkPhone").Value = objPrescription.RxPatient.PatientAddress.WorkPhone
            Else
                objcmd.Parameters("@sPatientWorkPhone").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientIdentifier", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.ID Is Nothing = False Then
                objcmd.Parameters("@sPatientIdentifier").Value = objPrescription.RxPatient.PatientName.ID
            Else
                objcmd.Parameters("@sPatientIdentifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientIdentifierType", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.IDType Is Nothing = False Then
                objcmd.Parameters("@sPatientIdentifierType").Value = objPrescription.RxPatient.PatientName.IDType
            Else
                objcmd.Parameters("@sPatientIdentifierType").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientIdentifier1", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.Code Is Nothing = False Then
                objcmd.Parameters("@sPatientIdentifier1").Value = objPrescription.RxPatient.PatientName.ID
            Else
                objcmd.Parameters("@sPatientIdentifier1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientIdentifierType1", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.CodeType Is Nothing = False Then
                objcmd.Parameters("@sPatientIdentifierType1").Value = objPrescription.RxPatient.PatientName.CodeType
            Else
                objcmd.Parameters("@sPatientIdentifierType1").Value = DBNull.Value
            End If

            objCon.Open()
            objcmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        End Try
    End Function

    Private Function InsertRefillPrescription(ByVal objPrescription As EPrescription, ByVal strConnection As String) As Boolean

        Dim oconn As New SqlConnection(strConnection)

        mdlGeneral.UpdateLog("Start InsertRefillPrescription :")

        Try
            If objPrescription IsNot Nothing Then
                If objPrescription.DrugsCol.Count = 0 Then
                    mdlGeneral.UpdateLog("Returned without Save : Since objPrescription.DrugsCol.Count = 0 for database " & oconn.Database)
                    Return False
                End If
            Else
                mdlGeneral.UpdateLog("Returned without Save: Since objPrescription isnothing for database " & oconn.Database)
                Return False
            End If

            Dim oDrug As EDrug = Nothing
            oDrug = objPrescription.DrugsCol.Item(0)


            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "sc_InsertRxRefill"
            objcmd.CommandType = CommandType.StoredProcedure



            objcmd.Parameters.Add("@nRxTransactionID", SqlDbType.VarChar)
            If objPrescription.RxTransactionID Is Nothing = False Then
                objcmd.Parameters("@nRxTransactionID").Value = objPrescription.RxTransactionID
            Else
                objcmd.Parameters("@nRxTransactionID").Value = 0
            End If

            objcmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
            If oDrug.DrugName Is Nothing = False Then
                objcmd.Parameters("@sDrugName").Value = oDrug.DrugName
            Else
                objcmd.Parameters("@sDrugName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
            If oDrug.Drugform Is Nothing = False Then
                objcmd.Parameters("@sDrugForm").Value = oDrug.Drugform
            Else
                objcmd.Parameters("@sDrugForm").Value = DBNull.Value
            End If


            objcmd.Parameters.Add("@sStrength", SqlDbType.VarChar)
            If oDrug.DrugStrength Is Nothing = False Then
                objcmd.Parameters("@sStrength").Value = oDrug.DrugStrength
            Else
                objcmd.Parameters("@sStrength").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
            If oDrug.Dosage Is Nothing = False Then
                objcmd.Parameters("@sDosage").Value = oDrug.Dosage
            Else
                objcmd.Parameters("@sDosage").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
            objcmd.Parameters("@sRoute").Value = DBNull.Value

            objcmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
            If oDrug.Directions Is Nothing = False Then
                objcmd.Parameters("@sFrequency").Value = oDrug.Directions
            Else
                objcmd.Parameters("@sFrequency").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDuration", SqlDbType.VarChar)
            If oDrug.DrugDuration Is Nothing = False Then
                objcmd.Parameters("@sDuration").Value = oDrug.DrugDuration
            Else
                objcmd.Parameters("@sDuration").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sQuantity", SqlDbType.VarChar)
            If oDrug.DrugQuantity Is Nothing = False Then
                objcmd.Parameters("@sQuantity").Value = oDrug.DrugQuantity
            Else
                objcmd.Parameters("@sQuantity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sQuantityQualifier", SqlDbType.VarChar)
            If oDrug.DrugQuantityQualifier Is Nothing = False Then
                objcmd.Parameters("@sQuantityQualifier").Value = oDrug.DrugQuantityQualifier
            Else
                objcmd.Parameters("@sQuantityQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sRefillQuantity", SqlDbType.VarChar)
            If oDrug.RefillQuantity Is Nothing = False Then
                objcmd.Parameters("@sRefillQuantity").Value = oDrug.RefillQuantity
            Else
                objcmd.Parameters("@sRefillQuantity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sRefillQualifier", SqlDbType.VarChar)
            If oDrug.RefillsQualifier Is Nothing = False Then
                objcmd.Parameters("@sRefillQualifier").Value = oDrug.RefillsQualifier
            Else
                objcmd.Parameters("@sRefillQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@bMaySubstitutions", SqlDbType.Bit)
            If IsDBNull(oDrug.MaySubstitute) = False Then
                objcmd.Parameters("@bMaySubstitutions").Value = oDrug.MaySubstitute
            Else
                objcmd.Parameters("@bMaySubstitutions").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@dtWrittendate", SqlDbType.DateTime)
            If oDrug.WrittenDate Is Nothing = False Then
                If IsDBNull(Number2Date(oDrug.WrittenDate)) = False Then
                    objcmd.Parameters("@dtWrittendate").Value = Number2Date(oDrug.WrittenDate)
                Else
                    objcmd.Parameters("@dtWrittendate").Value = DBNull.Value
                End If
            Else
                objcmd.Parameters("@dtWrittendate").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sRxReferenceNumber", SqlDbType.VarChar)
            If oDrug.RxReferenceNumber Is Nothing = False Then
                objcmd.Parameters("@sRxReferenceNumber").Value = oDrug.RxReferenceNumber
            Else
                objcmd.Parameters("@sRxReferenceNumber").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
            objcmd.Parameters("@sStatus").Value = "Pending"

            objcmd.Parameters.Add("@sPharmacyID", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyID Is Nothing = False Then
                objcmd.Parameters("@sPharmacyID").Value = objPrescription.RxPharmacy.PharmacyID
            Else
                objcmd.Parameters("@sPharmacyID").Value = DBNull.Value
            End If

            ''new field
            objcmd.Parameters.Add("@sPharmacyNPI", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyNPI Is Nothing = False Then
                objcmd.Parameters("@sPharmacyNPI").Value = objPrescription.RxPharmacy.PharmacyNPI
            Else
                objcmd.Parameters("@sPharmacyNPI").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@dtlastdate", SqlDbType.DateTime)
            If oDrug.LastFillDate Is Nothing = False Then
                If IsDBNull(Number2Date(oDrug.LastFillDate)) = False Then
                    objcmd.Parameters("@dtlastdate").Value = Number2Date(oDrug.LastFillDate)
                Else
                    objcmd.Parameters("@dtlastdate").Value = DBNull.Value
                End If
            Else
                objcmd.Parameters("@dtlastdate").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sNotes", SqlDbType.VarChar)
            If oDrug.Notes Is Nothing = False Then
                objcmd.Parameters("@sNotes").Value = oDrug.Notes
            Else
                objcmd.Parameters("@sNotes").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrescriberID", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberID Is Nothing = False Then
                objcmd.Parameters("@sPrescriberID").Value = objPrescription.RxPrescriber.PrescriberID
            Else
                objcmd.Parameters("@sPrescriberID").Value = DBNull.Value
            End If


            objcmd.Parameters.Add("@sPatientFirstName", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.FirstName Is Nothing = False Then
                objcmd.Parameters("@sPatientFirstName").Value = objPrescription.RxPatient.PatientName.FirstName
            Else
                objcmd.Parameters("@sPatientFirstName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientLastName", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.LastName Is Nothing = False Then
                objcmd.Parameters("@sPatientLastName").Value = objPrescription.RxPatient.PatientName.LastName
            Else
                objcmd.Parameters("@sPatientLastName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientMName", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.MiddleName Is Nothing = False Then
                objcmd.Parameters("@sPatientMName").Value = objPrescription.RxPatient.PatientName.MiddleName
            Else
                objcmd.Parameters("@sPatientMName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientPrefix", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.Prefix Is Nothing = False Then
                objcmd.Parameters("@sPatientPrefix").Value = objPrescription.RxPatient.PatientName.Prefix
            Else
                objcmd.Parameters("@sPatientPrefix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientSuffix", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.Suffix Is Nothing = False Then
                objcmd.Parameters("@sPatientSuffix").Value = objPrescription.RxPatient.PatientName.Suffix
            Else
                objcmd.Parameters("@sPatientSuffix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientGender", SqlDbType.VarChar)
            If objPrescription.RxPatient.Gender Is Nothing = False Then
                objcmd.Parameters("@sPatientGender").Value = objPrescription.RxPatient.Gender
            Else
                objcmd.Parameters("@sPatientGender").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@dtPatientDOB", SqlDbType.DateTime)
            If objPrescription.RxPatient.DateofBirth Is Nothing = False Then
                If IsDBNull(Number2Date(objPrescription.RxPatient.DateofBirth)) = False Then
                    objcmd.Parameters("@dtPatientDOB").Value = Number2Date(objPrescription.RxPatient.DateofBirth)
                Else
                    objcmd.Parameters("@dtPatientDOB").Value = DBNull.Value
                End If
            Else
                objcmd.Parameters("@dtPatientDOB").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientAddress1", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Address1 Is Nothing = False Then
                objcmd.Parameters("@sPatientAddress1").Value = objPrescription.RxPatient.PatientAddress.Address1
            Else
                objcmd.Parameters("@sPatientAddress1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientAddress2", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Address2 Is Nothing = False Then
                objcmd.Parameters("@sPatientAddress2").Value = objPrescription.RxPatient.PatientAddress.Address2
            Else
                objcmd.Parameters("@sPatientAddress2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientCity", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.City Is Nothing = False Then
                objcmd.Parameters("@sPatientCity").Value = objPrescription.RxPatient.PatientAddress.City
            Else
                objcmd.Parameters("@sPatientCity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientState", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.State Is Nothing = False Then
                objcmd.Parameters("@sPatientState").Value = objPrescription.RxPatient.PatientAddress.State
            Else
                objcmd.Parameters("@sPatientState").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientZipcode", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Zip Is Nothing = False Then
                objcmd.Parameters("@sPatientZipcode").Value = objPrescription.RxPatient.PatientAddress.Zip
            Else
                objcmd.Parameters("@sPatientZipcode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientNumber", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Phone Is Nothing = False Then
                objcmd.Parameters("@sPatientNumber").Value = objPrescription.RxPatient.PatientAddress.Phone
            Else
                objcmd.Parameters("@sPatientNumber").Value = DBNull.Value
            End If
            objcmd.Parameters.Add("@sPatientQualifier", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.PhQualifier Is Nothing = False Then
                objcmd.Parameters("@sPatientQualifier").Value = objPrescription.RxPatient.PatientAddress.PhQualifier
            Else
                objcmd.Parameters("@sPatientQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientEmail", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Email Is Nothing = False Then
                objcmd.Parameters("@sPatientEmail").Value = objPrescription.RxPatient.PatientAddress.Email
            Else
                objcmd.Parameters("@sPatientEmail").Value = DBNull.Value
            End If

            ''New field
            objcmd.Parameters.Add("@sPrNPI", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberNPI Is Nothing = False Then
                objcmd.Parameters("@sPrNPI").Value = objPrescription.RxPrescriber.PrescriberNPI
            Else
                objcmd.Parameters("@sPrNPI").Value = DBNull.Value
            End If

            ''New field
            objcmd.Parameters.Add("@sDEA", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberDEA Is Nothing = False Then
                objcmd.Parameters("@sDEA").Value = objPrescription.RxPrescriber.PrescriberDEA
            Else
                objcmd.Parameters("@sDEA").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrFirstName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberName.FirstName Is Nothing = False Then
                objcmd.Parameters("@sPrFirstName").Value = objPrescription.RxPrescriber.PrescriberName.FirstName
            Else
                objcmd.Parameters("@sPrFirstName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrLastName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberName.LastName Is Nothing = False Then
                objcmd.Parameters("@sPrLastName").Value = objPrescription.RxPrescriber.PrescriberName.LastName
            Else
                objcmd.Parameters("@sPrLastName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrMName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberName.MiddleName Is Nothing = False Then
                objcmd.Parameters("@sPrMName").Value = objPrescription.RxPrescriber.PrescriberName.MiddleName
            Else
                objcmd.Parameters("@sPrMName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrPrefix", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberName.Prefix Is Nothing = False Then
                objcmd.Parameters("@sPrPrefix").Value = objPrescription.RxPrescriber.PrescriberName.Prefix
            Else
                objcmd.Parameters("@sPrPrefix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrSuffix", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberName.Suffix Is Nothing = False Then
                objcmd.Parameters("@sPrSuffix").Value = objPrescription.RxPrescriber.PrescriberName.Suffix
            Else
                objcmd.Parameters("@sPrSuffix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAddress1", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Address1 Is Nothing = False Then
                objcmd.Parameters("@sPrAddress1").Value = objPrescription.RxPrescriber.PrescriberAddress.Address1
            Else
                objcmd.Parameters("@sPrAddress1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAddress2", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Address2 Is Nothing = False Then
                objcmd.Parameters("@sPrAddress2").Value = objPrescription.RxPrescriber.PrescriberAddress.Address2
            Else
                objcmd.Parameters("@sPrAddress2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrCity", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.City Is Nothing = False Then
                objcmd.Parameters("@sPrCity").Value = objPrescription.RxPrescriber.PrescriberAddress.City
            Else
                objcmd.Parameters("@sPrCity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrState", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.State Is Nothing = False Then
                objcmd.Parameters("@sPrState").Value = objPrescription.RxPrescriber.PrescriberAddress.State
            Else
                objcmd.Parameters("@sPrState").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrZipcode", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Zip Is Nothing = False Then
                objcmd.Parameters("@sPrZipcode").Value = objPrescription.RxPrescriber.PrescriberAddress.Zip
            Else
                objcmd.Parameters("@sPrZipcode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrNumber", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Phone Is Nothing = False Then
                objcmd.Parameters("@sPrNumber").Value = objPrescription.RxPrescriber.PrescriberAddress.Phone
            Else
                objcmd.Parameters("@sPrNumber").Value = DBNull.Value
            End If
            objcmd.Parameters.Add("@sPrQualifier", SqlDbType.VarChar)


            If objPrescription.RxPrescriber.PrescriberAddress.PhQualifier Is Nothing = False Then
                objcmd.Parameters("@sPrQualifier").Value = objPrescription.RxPrescriber.PrescriberAddress.PhQualifier
            Else
                objcmd.Parameters("@sPrQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrEmail", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Email Is Nothing = False Then
                objcmd.Parameters("@sPrEmail").Value = objPrescription.RxPrescriber.PrescriberAddress.Email
            Else
                objcmd.Parameters("@sPrEmail").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAgentFirstName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAgentName.FirstName Is Nothing = False Then
                objcmd.Parameters("@sPrAgentFirstName").Value = objPrescription.RxPrescriber.PrescriberAgentName.FirstName
            Else
                objcmd.Parameters("@sPrAgentFirstName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAgentLastName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAgentName.LastName Is Nothing = False Then
                objcmd.Parameters("@sPrAgentLastName").Value = objPrescription.RxPrescriber.PrescriberAgentName.LastName
            Else
                objcmd.Parameters("@sPrAgentLastName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAgentMiddleName", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAgentName.MiddleName Is Nothing = False Then
                objcmd.Parameters("@sPrAgentMiddleName").Value = objPrescription.RxPrescriber.PrescriberAgentName.MiddleName
            Else
                objcmd.Parameters("@sPrAgentMiddleName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAgentPrefix", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAgentName.Prefix Is Nothing = False Then
                objcmd.Parameters("@sPrAgentPrefix").Value = objPrescription.RxPrescriber.PrescriberAgentName.Prefix
            Else
                objcmd.Parameters("@sPrAgentPrefix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrAgentSuffix", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAgentName.Suffix Is Nothing = False Then
                objcmd.Parameters("@sPrAgentSuffix").Value = objPrescription.RxPrescriber.PrescriberAgentName.Suffix
            Else
                objcmd.Parameters("@sPrAgentSuffix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrSpecialtyType", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberSpecialtyQualifier Is Nothing = False Then
                objcmd.Parameters("@sPrSpecialtyType").Value = objPrescription.RxPrescriber.PrescriberSpecialtyQualifier
            Else
                objcmd.Parameters("@sPrSpecialtyType").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrSpecialtyCode", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberSpecialtyCode Is Nothing = False Then
                objcmd.Parameters("@sPrSpecialtyCode").Value = objPrescription.RxPrescriber.PrescriberSpecialtyCode
            Else
                objcmd.Parameters("@sPrSpecialtyCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.Pharmacyname Is Nothing = False Then
                objcmd.Parameters("@sPhName").Value = objPrescription.RxPharmacy.Pharmacyname
            Else
                objcmd.Parameters("@sPhName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhFirstName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistName.FirstName Is Nothing = False Then
                objcmd.Parameters("@sPhFirstName").Value = objPrescription.RxPharmacy.PharmacistName.FirstName
            Else
                objcmd.Parameters("@sPhFirstName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhLastName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistName.LastName Is Nothing = False Then
                objcmd.Parameters("@sPhLastName").Value = objPrescription.RxPharmacy.PharmacistName.LastName
            Else
                objcmd.Parameters("@sPhLastName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhMName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistName.MiddleName Is Nothing = False Then
                objcmd.Parameters("@sPhMName").Value = objPrescription.RxPharmacy.PharmacistName.MiddleName
            Else
                objcmd.Parameters("@sPhMName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhPrefix", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistName.Prefix Is Nothing = False Then
                objcmd.Parameters("@sPhPrefix").Value = objPrescription.RxPharmacy.PharmacistName.Prefix
            Else
                objcmd.Parameters("@sPhPrefix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhSuffix", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistName.Suffix Is Nothing = False Then
                objcmd.Parameters("@sPhSuffix").Value = objPrescription.RxPharmacy.PharmacistName.Suffix
            Else
                objcmd.Parameters("@sPhSuffix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAddress1", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Address1 Is Nothing = False Then
                objcmd.Parameters("@sPhAddress1").Value = objPrescription.RxPharmacy.PharmacyAddress.Address1
            Else
                objcmd.Parameters("@sPhAddress1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAddress2", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Address2 Is Nothing = False Then
                objcmd.Parameters("@sPhAddress2").Value = objPrescription.RxPharmacy.PharmacyAddress.Address2
            Else
                objcmd.Parameters("@sPhAddress2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhCity", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.City Is Nothing = False Then
                objcmd.Parameters("@sPhCity").Value = objPrescription.RxPharmacy.PharmacyAddress.City
            Else
                objcmd.Parameters("@sPhCity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhState", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.State Is Nothing = False Then
                objcmd.Parameters("@sPhState").Value = objPrescription.RxPharmacy.PharmacyAddress.State
            Else
                objcmd.Parameters("@sPhState").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhZipcode", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Zip Is Nothing = False Then
                objcmd.Parameters("@sPhZipcode").Value = objPrescription.RxPharmacy.PharmacyAddress.Zip
            Else
                objcmd.Parameters("@sPhZipcode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhNumber", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Phone Is Nothing = False Then
                objcmd.Parameters("@sPhNumber").Value = objPrescription.RxPharmacy.PharmacyAddress.Phone
            Else
                objcmd.Parameters("@sPhNumber").Value = DBNull.Value
            End If
            objcmd.Parameters.Add("@sPhQualifier", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.PhQualifier Is Nothing = False Then
                objcmd.Parameters("@sPhQualifier").Value = objPrescription.RxPharmacy.PharmacyAddress.PhQualifier
            Else
                objcmd.Parameters("@sPhQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhEmail", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Email Is Nothing = False Then
                objcmd.Parameters("@sPhEmail").Value = objPrescription.RxPharmacy.PharmacyAddress.Email
            Else
                objcmd.Parameters("@sPhEmail").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAgentFirstName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistAgentName.FirstName Is Nothing = False Then
                objcmd.Parameters("@sPhAgentFirstName").Value = objPrescription.RxPharmacy.PharmacistAgentName.FirstName
            Else
                objcmd.Parameters("@sPhAgentFirstName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAgentLastName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistAgentName.LastName Is Nothing = False Then
                objcmd.Parameters("@sPhAgentLastName").Value = objPrescription.RxPharmacy.PharmacistAgentName.LastName
            Else
                objcmd.Parameters("@sPhAgentLastName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAgentMName", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistAgentName.MiddleName Is Nothing = False Then
                objcmd.Parameters("@sPhAgentMName").Value = objPrescription.RxPharmacy.PharmacistAgentName.MiddleName
            Else
                objcmd.Parameters("@sPhAgentMName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAgentPrefix", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistAgentName.Prefix Is Nothing = False Then
                objcmd.Parameters("@sPhAgentPrefix").Value = objPrescription.RxPharmacy.PharmacistAgentName.Prefix
            Else
                objcmd.Parameters("@sPhAgentPrefix").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPhAgentSuffix", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacistAgentName.Suffix Is Nothing = False Then
                objcmd.Parameters("@sPhAgentSuffix").Value = objPrescription.RxPharmacy.PharmacistAgentName.Suffix
            Else
                objcmd.Parameters("@sPhAgentSuffix").Value = DBNull.Value
            End If


            ''New fields
            objcmd.Parameters.Add("@sCodeListQualifier", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.CodeListQualifier Is Nothing = False Then
                objcmd.Parameters("@sCodeListQualifier").Value = objPrescription.RxPharmacy.CodeListQualifier
            Else
                objcmd.Parameters("@sCodeListQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sUnitSourceCode", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.UnitSourceCode Is Nothing = False Then
                objcmd.Parameters("@sUnitSourceCode").Value = objPrescription.RxPharmacy.UnitSourceCode
            Else
                objcmd.Parameters("@sUnitSourceCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPotencyUnitCode", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PotencyUnitCode Is Nothing = False Then
                objcmd.Parameters("@sPotencyUnitCode").Value = objPrescription.RxPharmacy.PotencyUnitCode
            Else
                objcmd.Parameters("@sPotencyUnitCode").Value = DBNull.Value
            End If

            ''---x-------


            objcmd.Parameters.Add("@sDgClQualifier1", SqlDbType.VarChar)
            If oDrug.ClinicalInformationQualifier1 Is Nothing = False Then
                objcmd.Parameters("@sDgClQualifier1").Value = oDrug.ClinicalInformationQualifier1
            Else
                objcmd.Parameters("@sDgClQualifier1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrimaryQualifier1", SqlDbType.VarChar)
            If oDrug.PrimaryQualifier1 Is Nothing = False Then
                objcmd.Parameters("@sPrimaryQualifier1").Value = oDrug.PrimaryQualifier1
            Else
                objcmd.Parameters("@sPrimaryQualifier1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrimaryValue1", SqlDbType.VarChar)
            If oDrug.PrimaryValue1 Is Nothing = False Then
                objcmd.Parameters("@sPrimaryValue1").Value = oDrug.PrimaryValue1
            Else
                objcmd.Parameters("@sPrimaryValue1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sSecQualifier1", SqlDbType.VarChar)
            If oDrug.SecondaryQualifier1 Is Nothing = False Then
                objcmd.Parameters("@sSecQualifier1").Value = oDrug.SecondaryQualifier1
            Else
                objcmd.Parameters("@sSecQualifier1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sSecValue1", SqlDbType.VarChar)
            If oDrug.SecondaryValue1 Is Nothing = False Then
                objcmd.Parameters("@sSecValue1").Value = oDrug.SecondaryValue1
            Else
                objcmd.Parameters("@sSecValue1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDgClQualifier2", SqlDbType.VarChar)
            If oDrug.ClinicalInformationQualifier2 Is Nothing = False Then
                objcmd.Parameters("@sDgClQualifier2").Value = oDrug.ClinicalInformationQualifier2
            Else
                objcmd.Parameters("@sDgClQualifier2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrimaryQualifier2", SqlDbType.VarChar)
            If oDrug.PrimaryQualifier2 Is Nothing = False Then
                objcmd.Parameters("@sPrimaryQualifier2").Value = oDrug.PrimaryQualifier2
            Else
                objcmd.Parameters("@sPrimaryQualifier2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrimaryValue2", SqlDbType.VarChar)
            If oDrug.PrimaryValue2 Is Nothing = False Then
                objcmd.Parameters("@sPrimaryValue2").Value = oDrug.PrimaryValue2
            Else
                objcmd.Parameters("@sPrimaryValue2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sSecQualifier2", SqlDbType.VarChar)
            If oDrug.SecondaryQualifier2 Is Nothing = False Then
                objcmd.Parameters("@sSecQualifier2").Value = oDrug.SecondaryQualifier2
            Else
                objcmd.Parameters("@sSecQualifier2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sSecValue2", SqlDbType.VarChar)
            If oDrug.SecondaryValue2 Is Nothing = False Then
                objcmd.Parameters("@sSecValue2").Value = oDrug.SecondaryValue2
            Else
                objcmd.Parameters("@sSecValue2").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sProductCode", SqlDbType.VarChar)
            If oDrug.ProductCode Is Nothing = False Then
                objcmd.Parameters("@sProductCode").Value = oDrug.ProductCode
            Else
                objcmd.Parameters("@sProductCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sProductCodeQualifier", SqlDbType.VarChar)
            If oDrug.ProductCodeQualifier Is Nothing = False Then
                objcmd.Parameters("@sProductCodeQualifier").Value = oDrug.ProductCodeQualifier
            Else
                objcmd.Parameters("@sProductCodeQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDosageForm", SqlDbType.VarChar)
            If oDrug.DosageForm Is Nothing = False Then
                objcmd.Parameters("@sDosageForm").Value = oDrug.DosageForm
            Else
                objcmd.Parameters("@sDosageForm").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sStrengthUnits", SqlDbType.VarChar)
            If oDrug.DrugStrengthUnits Is Nothing = False Then
                objcmd.Parameters("@sStrengthUnits").Value = oDrug.DrugStrengthUnits
            Else
                objcmd.Parameters("@sStrengthUnits").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDrugDBCode", SqlDbType.VarChar)
            If oDrug.DrugDBCode Is Nothing = False Then
                objcmd.Parameters("@sDrugDBCode").Value = oDrug.DrugDBCode
            Else
                objcmd.Parameters("@sDrugDBCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDrugDBCodeQualifier", SqlDbType.VarChar)
            If oDrug.DrugDBCodeQualifier Is Nothing = False Then
                objcmd.Parameters("@sDrugDBCodeQualifier").Value = oDrug.DrugDBCodeQualifier
            Else
                objcmd.Parameters("@sDrugDBCodeQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPriorAuthorizationQualifier", SqlDbType.VarChar)
            If oDrug.PriorAuthorizationQualifier Is Nothing = False Then
                objcmd.Parameters("@sPriorAuthorizationQualifier").Value = oDrug.PriorAuthorizationQualifier
            Else
                objcmd.Parameters("@sPriorAuthorizationQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPriorAuthorizationValue", SqlDbType.VarChar)
            If oDrug.PriorAuthorizationValue Is Nothing = False Then
                objcmd.Parameters("@sPriorAuthorizationValue").Value = oDrug.PriorAuthorizationValue
            Else
                objcmd.Parameters("@sPriorAuthorizationValue").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrescriberClinic", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberClinic Is Nothing = False Then
                objcmd.Parameters("@sPrescriberClinic").Value = objPrescription.RxPrescriber.PrescriberClinic
            Else
                objcmd.Parameters("@sPrescriberClinic").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMessageID", SqlDbType.VarChar)
            If oDrug.MessageID Is Nothing = False Then
                objcmd.Parameters("@sMessageID").Value = oDrug.MessageID
            Else
                objcmd.Parameters("@sMessageID").Value = DBNull.Value
            End If


            'New fields MD drug  -----------
            objcmd.Parameters.Add("@sMDDrugName", SqlDbType.VarChar)
            If oDrug.MDDrugName Is Nothing = False Then
                objcmd.Parameters("@sMDDrugName").Value = oDrug.MDDrugName
            Else
                objcmd.Parameters("@sMDDrugName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugForm", SqlDbType.VarChar)
            objcmd.Parameters("@sMDDrugForm").Value = DBNull.Value
            'If oDrug.Drugform Is Nothing = False Then
            '    objcmd.Parameters("@sMDDrugForm").Value = oDrug.mdDrugform
            'Else
            '    objcmd.Parameters("@sMDDrugForm").Value = DBNull.Value
            'End If

            objcmd.Parameters.Add("@sMDDosage", SqlDbType.VarChar)
            objcmd.Parameters("@sMDDosage").Value = DBNull.Value

            objcmd.Parameters.Add("@sMDRoute", SqlDbType.VarChar)
            objcmd.Parameters("@sMDRoute").Value = DBNull.Value

            objcmd.Parameters.Add("@sMDFrequency", SqlDbType.VarChar)
            If oDrug.MDDrugDirections Is Nothing = False Then
                objcmd.Parameters("@sMDFrequency").Value = oDrug.MDDrugDirections
            Else
                objcmd.Parameters("@sMDFrequency").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDuration", SqlDbType.VarChar)
            If oDrug.MDDrugDuration Is Nothing = False Then
                objcmd.Parameters("@sMDDuration").Value = oDrug.MDDrugDuration
            Else
                objcmd.Parameters("@sMDDuration").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDQuantity", SqlDbType.VarChar)
            If oDrug.MDDrugQuantity Is Nothing = False Then
                objcmd.Parameters("@sMDQuantity").Value = oDrug.MDDrugQuantity
            Else
                objcmd.Parameters("@sMDQuantity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDQuantityQualifier", SqlDbType.VarChar)
            If oDrug.MDDrugQualifier Is Nothing = False Then
                objcmd.Parameters("@sMDQuantityQualifier").Value = oDrug.MDDrugQualifier
            Else
                objcmd.Parameters("@sMDQuantityQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDRefillQuantity", SqlDbType.VarChar)
            If oDrug.MDRefillQuantity Is Nothing = False Then
                objcmd.Parameters("@sMDRefillQuantity").Value = oDrug.MDRefillQuantity
            Else
                objcmd.Parameters("@sMDRefillQuantity").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDRefillQualifier", SqlDbType.VarChar)
            If oDrug.MDRefillsQualifier Is Nothing = False Then
                objcmd.Parameters("@sMDRefillQualifier").Value = oDrug.MDRefillsQualifier
            Else
                objcmd.Parameters("@sMDRefillQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@MDdtWrittenDate", SqlDbType.VarChar)
            If oDrug.MDdtWrittenDate Is Nothing = False Then
                objcmd.Parameters("@MDdtWrittenDate").Value = oDrug.MDdtWrittenDate
            Else
                objcmd.Parameters("@MDdtWrittenDate").Value = DBNull.Value
            End If



            objcmd.Parameters.Add("@MDbMaySubstitutions", SqlDbType.Bit)
            If IsNothing(oDrug.MDbIsSubstituitons) = False Then
                objcmd.Parameters("@MDbMaySubstitutions").Value = oDrug.MDbIsSubstituitons
            Else
                objcmd.Parameters("@MDbMaySubstitutions").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDNotes", SqlDbType.VarChar)
            If oDrug.MDNotes Is Nothing = False Then
                objcmd.Parameters("@sMDNotes").Value = oDrug.MDNotes
            Else
                objcmd.Parameters("@sMDNotes").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@MDdtlastdate", SqlDbType.VarChar)
            If oDrug.MDdtLastFillDate Is Nothing = False Then
                objcmd.Parameters("@MDdtlastdate").Value = oDrug.MDdtLastFillDate
            Else
                objcmd.Parameters("@MDdtlastdate").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDProductCode", SqlDbType.VarChar)
            If oDrug.MDProductCode Is Nothing = False Then
                objcmd.Parameters("@sMDProductCode").Value = oDrug.MDProductCode
            Else
                objcmd.Parameters("@sMDProductCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDProductCodeQualifier", SqlDbType.VarChar)
            If oDrug.MDProductCodeQualifier Is Nothing = False Then
                objcmd.Parameters("@sMDProductCodeQualifier").Value = oDrug.MDProductCodeQualifier
            Else
                objcmd.Parameters("@sMDProductCodeQualifier").Value = DBNull.Value
            End If







            objcmd.Parameters.Add("@sMDDosageForm", SqlDbType.VarChar)
            If oDrug.MDDosageForm Is Nothing = False Then
                objcmd.Parameters("@sMDDosageForm").Value = oDrug.MDDosageForm
            Else
                objcmd.Parameters("@sMDDosageForm").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDStrength", SqlDbType.VarChar)
            If oDrug.MDDrugStrength Is Nothing = False Then
                objcmd.Parameters("@sMDStrength").Value = oDrug.MDDrugStrength
            Else
                objcmd.Parameters("@sMDStrength").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDStrengthUnits", SqlDbType.VarChar)
            If oDrug.MDDrugStrengthUnits Is Nothing = False Then
                objcmd.Parameters("@sMDStrengthUnits").Value = oDrug.MDDrugStrengthUnits
            Else
                objcmd.Parameters("@sMDStrengthUnits").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDCodeListQualifier", SqlDbType.VarChar)
            If oDrug.MDCodeListQualifier Is Nothing = False Then
                objcmd.Parameters("@sMDCodeListQualifier").Value = oDrug.MDCodeListQualifier
            Else
                objcmd.Parameters("@sMDCodeListQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDUnitSourceCode", SqlDbType.VarChar)
            If oDrug.MDUnitSourceCode Is Nothing = False Then
                objcmd.Parameters("@sMDUnitSourceCode").Value = oDrug.MDUnitSourceCode
            Else
                objcmd.Parameters("@sMDUnitSourceCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDPotencyUnitCode", SqlDbType.VarChar)
            If oDrug.MDPotencyUnitCode Is Nothing = False Then
                objcmd.Parameters("@sMDPotencyUnitCode").Value = oDrug.MDPotencyUnitCode
            Else
                objcmd.Parameters("@sMDPotencyUnitCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugDBCode", SqlDbType.VarChar)
            If oDrug.MDDrugDBCode Is Nothing = False Then
                objcmd.Parameters("@sMDDrugDBCode").Value = oDrug.MDDrugDBCode
            Else
                objcmd.Parameters("@sMDDrugDBCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugDBCodeQualifier", SqlDbType.VarChar)
            If oDrug.MDDrugDBCodeQualifier Is Nothing = False Then
                objcmd.Parameters("@sMDDrugDBCodeQualifier").Value = oDrug.MDDrugDBCodeQualifier
            Else
                objcmd.Parameters("@sMDDrugDBCodeQualifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDrugCoverageStatusCode", SqlDbType.VarChar)
            If oDrug.DrugCoverageStatusCode Is Nothing = False Then
                objcmd.Parameters("@sDrugCoverageStatusCode").Value = oDrug.DrugCoverageStatusCode
            Else
                objcmd.Parameters("@sDrugCoverageStatusCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMDDrugCoverageStatusCode", SqlDbType.VarChar)
            If oDrug.MDDrugCoverageStatusCode Is Nothing = False Then
                objcmd.Parameters("@sMDDrugCoverageStatusCode").Value = oDrug.MDDrugCoverageStatusCode
            Else
                objcmd.Parameters("@sMDDrugCoverageStatusCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPharmacySpecialty", SqlDbType.VarChar)
            If oDrug.PharmacySpecialty Is Nothing = False Then
                objcmd.Parameters("@sPharmacySpecialty").Value = oDrug.PharmacySpecialty
            Else
                objcmd.Parameters("@sPharmacySpecialty").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientRelationship", SqlDbType.VarChar)
            If oDrug.PatientRelationship Is Nothing = False Then
                objcmd.Parameters("@sPatientRelationship").Value = oDrug.PatientRelationship
            Else
                objcmd.Parameters("@sPatientRelationship").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrescriberSSN", SqlDbType.VarChar)
            If oDrug.PrescriberSSN Is Nothing = False Then
                objcmd.Parameters("@sPrescriberSSN").Value = oDrug.PrescriberSSN
            Else
                objcmd.Parameters("@sPrescriberSSN").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@FileData", SqlDbType.VarBinary)
            If oDrug.FileData Is Nothing = False Then
                objcmd.Parameters("@FileData").Value = oDrug.FileData
            Else
                objcmd.Parameters("@FileData").Value = DBNull.Value
            End If

            '--xx------------------------------------x----


            objcmd.Parameters.Add("@sPhFax", SqlDbType.VarChar)
            If objPrescription.RxPharmacy.PharmacyAddress.Fax Is Nothing = False Then
                objcmd.Parameters("@sPhFax").Value = objPrescription.RxPharmacy.PharmacyAddress.Fax
            Else
                objcmd.Parameters("@sPhFax").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPrFax", SqlDbType.VarChar)
            If objPrescription.RxPrescriber.PrescriberAddress.Fax Is Nothing = False Then
                objcmd.Parameters("@sPrFax").Value = objPrescription.RxPrescriber.PrescriberAddress.Fax
            Else
                objcmd.Parameters("@sPrFax").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientFax", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.Fax Is Nothing = False Then
                objcmd.Parameters("@sPatientFax").Value = objPrescription.RxPatient.PatientAddress.Fax
            Else
                objcmd.Parameters("@sPatientFax").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientWorkPhone", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientAddress.WorkPhone Is Nothing = False Then
                objcmd.Parameters("@sPatientWorkPhone").Value = objPrescription.RxPatient.PatientAddress.WorkPhone
            Else
                objcmd.Parameters("@sPatientWorkPhone").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientIdentifier", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.ID Is Nothing = False Then
                objcmd.Parameters("@sPatientIdentifier").Value = objPrescription.RxPatient.PatientName.ID
            Else
                objcmd.Parameters("@sPatientIdentifier").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientIdentifierType", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.IDType Is Nothing = False Then
                objcmd.Parameters("@sPatientIdentifierType").Value = objPrescription.RxPatient.PatientName.IDType
            Else
                objcmd.Parameters("@sPatientIdentifierType").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientIdentifier1", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.Code Is Nothing = False Then
                objcmd.Parameters("@sPatientIdentifier1").Value = objPrescription.RxPatient.PatientName.ID
            Else
                objcmd.Parameters("@sPatientIdentifier1").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sPatientIdentifierType1", SqlDbType.VarChar)
            If objPrescription.RxPatient.PatientName.CodeType Is Nothing = False Then
                objcmd.Parameters("@sPatientIdentifierType1").Value = objPrescription.RxPatient.PatientName.CodeType
            Else
                objcmd.Parameters("@sPatientIdentifierType1").Value = DBNull.Value
            End If

            objCon.Open()
            objcmd.ExecuteNonQuery()

            mdlGeneral.UpdateLog("Finished InsertRefillPrescription For database :" & oconn.Database)

            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally
            mdlGeneral.UpdateLog("End InsertRefillPrescription For database :" & oconn.Database)
            If Not IsNothing(objcmd) Then
                objcmd.Cancel()
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Private Function GetPatGender(ByVal strGender As String) As String
        Dim strPatGender As String = ""
        Select Case strGender
            Case "M"
                strPatGender = "Male"
            Case "F"
                strPatGender = "Female"
            Case "U"
                strPatGender = "Other"
        End Select

        Return strPatGender
    End Function

    Private Function Number2Date(ByVal strWritten As String) As Date
        If strWritten = "" Then
            Return Nothing
        ElseIf strWritten.Length < 8 Then
            Return Nothing
        ElseIf strWritten.Length = 8 Then
            Dim D, M, Y As Integer
            M = strWritten.Substring(4, 2)
            D = strWritten.Substring(6, 2)
            Y = strWritten.Substring(0, 4)
            Return New Date(Y, M, D)
        Else
            Dim strSentTime As String = strWritten.Replace("T", " ")
            strSentTime = strSentTime.Replace("Z", "")
            Return CType(TypeDescriptor.GetConverter("System.DateTime").ConvertFromString(strSentTime), DateTime).Date
        End If



    End Function

    Public Function GetPrefixTransactionID(ByVal PatientID As Long, ByVal strConnection As String) As Long
        Dim PatientDOB As DateTime
        Dim strID As String = String.Empty
        Dim dtDate As DateTime
        Dim objcmd1 = New SqlCommand
        Dim objCon1 = New SqlConnection
        Try

            objCon1.ConnectionString = strConnection
            objcmd1.Connection = objCon1
            objcmd1.CommandText = "SELECT dtDOB FROM Patient WHERE nPatientID = " & PatientID & ""
            objcmd1.CommandType = CommandType.Text
            objCon1.Open()
            PatientDOB = objcmd1.ExecuteScalar()
            dtDate = System.DateTime.Now
            strID = System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)) & System.Math.Abs(DateDiff(DateInterval.Second, dtDate.Date, dtDate)) & System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), PatientDOB.Date))
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        Finally
            objcmd1.Cancel()
            If objCon1.State = ConnectionState.Open Then
                objCon1.Close()
            End If
            If Not IsNothing(objcmd1) Then
                objcmd1.dispose()
                objcmd1 = Nothing
            End If
            If Not IsNothing(objCon1) Then
                objCon1.dispose()
                objCon1 = Nothing
            End If
        End Try

        Return CLng(strID)



    End Function

    Private Function InsertErrorDetails(ByVal objError As SureScriptErrorMessage) As Boolean

        Try

            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "sc_InsertErrorDetails"
            objcmd.CommandType = CommandType.StoredProcedure


            objcmd.Parameters.Add("@sErrorCode", SqlDbType.VarChar)
            If objError.ErrorCode Is Nothing = False Then
                objcmd.Parameters("@sErrorCode").Value = objError.ErrorCode
            Else
                objcmd.Parameters("@sErrorCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDescriptionCode", SqlDbType.VarChar)
            If objError.DescriptionCode Is Nothing = False Then
                objcmd.Parameters("@sDescriptionCode").Value = objError.DescriptionCode
            Else
                objcmd.Parameters("@sDescriptionCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
            If objError.Description Is Nothing = False Then
                objcmd.Parameters("@sDescription").Value = objError.Description
            Else
                objcmd.Parameters("@sDescription").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objcmd.Parameters("@MachineID").Value = mdlGeneral.GetTransactionID()

            objcmd.Parameters.Add("@nTransactionId", SqlDbType.BigInt)
            objcmd.Parameters("@nTransactionId").Direction = ParameterDirection.InputOutput
            objcmd.Parameters("@nTransactionId").Value = 0


            objCon.Open()
            objcmd.ExecuteNonQuery()

            objError.TransactionID = objcmd.Parameters("@nTransactionId").Value

            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally

            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Private Function InsertErrorDetails(ByVal objError As SureScriptErrorMessage, ByVal strConnection As String) As Boolean

        Try

            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "sc_InsertErrorDetails"
            objcmd.CommandType = CommandType.StoredProcedure


            objcmd.Parameters.Add("@sErrorCode", SqlDbType.VarChar)
            If objError.ErrorCode Is Nothing = False Then
                objcmd.Parameters("@sErrorCode").Value = objError.ErrorCode
            Else
                objcmd.Parameters("@sErrorCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDescriptionCode", SqlDbType.VarChar)
            If objError.DescriptionCode Is Nothing = False Then
                objcmd.Parameters("@sDescriptionCode").Value = objError.DescriptionCode
            Else
                objcmd.Parameters("@sDescriptionCode").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
            If objError.Description Is Nothing = False Then
                objcmd.Parameters("@sDescription").Value = objError.Description
            Else
                objcmd.Parameters("@sDescription").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objcmd.Parameters("@MachineID").Value = mdlGeneral.GetTransactionID()

            objcmd.Parameters.Add("@nTransactionId", SqlDbType.BigInt)
            objcmd.Parameters("@nTransactionId").Direction = ParameterDirection.InputOutput
            objcmd.Parameters("@nTransactionId").Value = 0


            objCon.Open()
            objcmd.ExecuteNonQuery()

            objError.TransactionID = objcmd.Parameters("@nTransactionId").Value

            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally

            objcmd.Cancel()

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Private Function InsertintoMessageTransaction(ByVal DataRow As DataRow, ByVal strConnection As String) As Boolean

        Try
            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "sc_InsertRxMsgTransaction"
            objcmd.CommandType = CommandType.StoredProcedure


            objcmd.Parameters.Add("@nMessageID", SqlDbType.VarChar)
            objcmd.Parameters.Add("@sMessageName", SqlDbType.VarChar)

            objcmd.Parameters.Add("@sRelatesToMessageID", SqlDbType.VarChar)
            objcmd.Parameters.Add("@sMessageFrom", SqlDbType.VarChar)
            objcmd.Parameters.Add("@sMessageTo", SqlDbType.VarChar)

            objcmd.Parameters.AddWithValue("@sDateTimeStamp", Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture))
            objcmd.Parameters.Add("@dtDateReceived", SqlDbType.DateTime)
            objcmd.Parameters.Add("@sReferenceNumber", SqlDbType.VarChar)

            objcmd.Parameters.AddWithValue("@IsAlertCheck", "False")
            objcmd.Parameters.Add("@sSenderSoftwareVersion", SqlDbType.VarChar)
            objcmd.Parameters.Add("@sSenderSoftwareDeveloper", SqlDbType.VarChar)

            objcmd.Parameters.Add("@sSenderSoftwareProduct", SqlDbType.VarChar)
            objcmd.Parameters.Add("@FileXML", SqlDbType.Xml)
            objcmd.Parameters.Add("@sPrescriberOrderNumber", SqlDbType.VarChar)

            If DataRow.Table.Columns.Contains("sMessageID") Then
                objcmd.Parameters("@nMessageID").Value = Convert.ToString(DataRow("sMessageID"))
            Else
                objcmd.Parameters("@nMessageID").Value = ""
            End If

            If DataRow.Table.Columns.Contains("sMsgType") Then
                objcmd.Parameters("@sMessageName").Value = Convert.ToString(DataRow("sMsgType"))
            Else
                objcmd.Parameters("@sMessageName").Value = ""
            End If

            If DataRow.Table.Columns.Contains("sRelativeMessageID") Then
                objcmd.Parameters("@sRelatesToMessageID").Value = Convert.ToString(DataRow("sRelativeMessageID"))
            Else
                objcmd.Parameters("@sRelatesToMessageID").Value = ""
            End If

            If DataRow.Table.Columns.Contains("sMessageFrom") Then
                objcmd.Parameters("@sMessageFrom").Value = Convert.ToString(DataRow("sMessageFrom"))
            Else
                objcmd.Parameters("@sMessageFrom").Value = ""
            End If

            If DataRow.Table.Columns.Contains("sMessageTo") Then
                objcmd.Parameters("@sMessageTo").Value = Convert.ToString(DataRow("sMessageTo"))
            Else
                objcmd.Parameters("@sMessageTo").Value = ""
            End If

            If DataRow.Table.Columns.Contains("dtSentTime") Then
                objcmd.Parameters("@dtDateReceived").Value = Convert.ToString(DataRow("dtSentTime"))
            Else
                objcmd.Parameters("@dtDateReceived").Value = ""
            End If

            If DataRow.Table.Columns.Contains("sRxReference") Then
                objcmd.Parameters("@sReferenceNumber").Value = Convert.ToString(DataRow("sRxReference"))
            Else
                objcmd.Parameters("@sReferenceNumber").Value = ""
            End If

            If DataRow.Table.Columns.Contains("sSenderSoftwareVersion") Then
                objcmd.Parameters("@sSenderSoftwareVersion").Value = Convert.ToString(DataRow("sSenderSoftwareVersion"))
            Else
                objcmd.Parameters("@sSenderSoftwareVersion").Value = ""
            End If

            If DataRow.Table.Columns.Contains("SenderSoftwareDeveloper") Then
                objcmd.Parameters("@sSenderSoftwareDeveloper").Value = Convert.ToString(DataRow("SenderSoftwareDeveloper"))
            Else
                objcmd.Parameters("@sSenderSoftwareDeveloper").Value = ""
            End If

            If DataRow.Table.Columns.Contains("SenderSoftwareProduct") Then
                objcmd.Parameters("@sSenderSoftwareProduct").Value = Convert.ToString(DataRow("SenderSoftwareProduct"))
            Else
                objcmd.Parameters("@sSenderSoftwareProduct").Value = ""
            End If

            If DataRow.Table.Columns.Contains("FileXML") Then
                objcmd.Parameters("@FileXML").Value = Convert.ToString(DataRow("FileXML"))
            Else
                objcmd.Parameters("@FileXML").Value = ""
            End If

            If DataRow.Table.Columns.Contains("sPrescriberOrder") Then
                objcmd.Parameters("@sPrescriberOrderNumber").Value = Convert.ToString(DataRow("sPrescriberOrder"))
            Else
                objcmd.Parameters("@sPrescriberOrderNumber").Value = ""
            End If

            objCon.Open()
            objcmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally
            objcmd.Cancel()

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Private Function InsertintoMessageTransaction(ByVal oMessage As SureScriptMessage, ByVal strConnection As String) As Boolean

        Try
            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "sc_InsertRxMsgTransaction"
            objcmd.CommandType = CommandType.StoredProcedure

            objcmd.Parameters.Add("@nMessageID", SqlDbType.VarChar)
            If oMessage.MessageID Is Nothing = False Then
                objcmd.Parameters("@nMessageID").Value = oMessage.MessageID
            Else
                objcmd.Parameters("@nMessageID").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMessageName", SqlDbType.VarChar)
            If oMessage.MessageName Is Nothing = False Then
                objcmd.Parameters("@sMessageName").Value = oMessage.MessageName
            Else
                objcmd.Parameters("@sMessageName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sRelatesToMessageID", SqlDbType.VarChar)
            If oMessage.RelatesToMessageId Is Nothing = False Then
                objcmd.Parameters("@sRelatesToMessageID").Value = oMessage.RelatesToMessageId
            Else
                objcmd.Parameters("@sRelatesToMessageID").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMessageFrom", SqlDbType.VarChar)
            If oMessage.MessageFrom Is Nothing = False Then
                objcmd.Parameters("@sMessageFrom").Value = oMessage.MessageFrom
            Else
                objcmd.Parameters("@sMessageFrom").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMessageTo", SqlDbType.VarChar)
            If oMessage.MessageTo Is Nothing = False Then
                objcmd.Parameters("@sMessageTo").Value = oMessage.MessageTo
            Else
                objcmd.Parameters("@sMessageTo").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDateTimeStamp", SqlDbType.VarChar)
            If oMessage.DateTimeStamp Is Nothing = False Then
                objcmd.Parameters("@sDateTimeStamp").Value = oMessage.DateTimeStamp
            Else
                objcmd.Parameters("@sDateTimeStamp").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@dtDateReceived", SqlDbType.DateTime)
            If oMessage.MessageTo Is Nothing = False Then
                objcmd.Parameters("@dtDateReceived").Value = oMessage.DateReceived
            Else
                objcmd.Parameters("@dtDateReceived").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sReferenceNumber", SqlDbType.VarChar)
            If oMessage.TransactionID Is Nothing = False Then
                objcmd.Parameters("@sReferenceNumber").Value = oMessage.TransactionID
            Else
                objcmd.Parameters("@sReferenceNumber").Value = DBNull.Value
            End If

            'New Field
            objcmd.Parameters.Add("@sSenderSoftwareVersion", SqlDbType.VarChar)
            If oMessage.SenderSoftwareVersion Is Nothing = False Then
                objcmd.Parameters("@sSenderSoftwareVersion").Value = oMessage.SenderSoftwareVersion
            Else
                objcmd.Parameters("@sSenderSoftwareVersion").Value = DBNull.Value
            End If
            objcmd.Parameters.Add("@sSenderSoftwareDeveloper", SqlDbType.VarChar)
            If oMessage.SenderSoftwareDeveloper Is Nothing = False Then
                objcmd.Parameters("@sSenderSoftwareDeveloper").Value = oMessage.SenderSoftwareDeveloper
            Else
                objcmd.Parameters("@sSenderSoftwareDeveloper").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sSenderSoftwareProduct", SqlDbType.VarChar)
            If oMessage.SenderSoftwareProduct Is Nothing = False Then
                objcmd.Parameters("@sSenderSoftwareProduct").Value = oMessage.SenderSoftwareProduct
            Else
                objcmd.Parameters("@sSenderSoftwareProduct").Value = DBNull.Value
            End If

            ''-----x-------

            objcmd.Parameters.Add("@IsAlertCheck", SqlDbType.Bit)
            objcmd.Parameters("@IsAlertCheck").Value = "False"


            objCon.Open()
            objcmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally
            objcmd.Cancel()

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Private Function InsertintoMessageTransaction(ByVal oMessage As SureScriptMessage) As Boolean

        Try
            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "sc_InsertRxMsgTransaction"
            objcmd.CommandType = CommandType.StoredProcedure


            objcmd.Parameters.Add("@nMessageID", SqlDbType.VarChar)
            If oMessage.MessageID Is Nothing = False Then
                objcmd.Parameters("@nMessageID").Value = oMessage.MessageID
            Else
                objcmd.Parameters("@nMessageID").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMessageName", SqlDbType.VarChar)
            If oMessage.MessageName Is Nothing = False Then
                objcmd.Parameters("@sMessageName").Value = oMessage.MessageName
            Else
                objcmd.Parameters("@sMessageName").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sRelatesToMessageID", SqlDbType.VarChar)
            If oMessage.RelatesToMessageId Is Nothing = False Then
                objcmd.Parameters("@sRelatesToMessageID").Value = oMessage.RelatesToMessageId
            Else
                objcmd.Parameters("@sRelatesToMessageID").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMessageFrom", SqlDbType.VarChar)
            If oMessage.MessageFrom Is Nothing = False Then
                objcmd.Parameters("@sMessageFrom").Value = oMessage.MessageFrom
            Else
                objcmd.Parameters("@sMessageFrom").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sMessageTo", SqlDbType.VarChar)
            If oMessage.MessageTo Is Nothing = False Then
                objcmd.Parameters("@sMessageTo").Value = oMessage.MessageTo
            Else
                objcmd.Parameters("@sMessageTo").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sDateTimeStamp", SqlDbType.VarChar)
            If oMessage.DateTimeStamp Is Nothing = False Then
                objcmd.Parameters("@sDateTimeStamp").Value = oMessage.DateTimeStamp
            Else
                objcmd.Parameters("@sDateTimeStamp").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@dtDateReceived", SqlDbType.DateTime)
            If oMessage.MessageTo Is Nothing = False Then
                objcmd.Parameters("@dtDateReceived").Value = oMessage.DateReceived
            Else
                objcmd.Parameters("@dtDateReceived").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@sReferenceNumber", SqlDbType.VarChar)
            If oMessage.TransactionID Is Nothing = False Then
                objcmd.Parameters("@sReferenceNumber").Value = oMessage.TransactionID
            Else
                objcmd.Parameters("@sReferenceNumber").Value = DBNull.Value
            End If
            'New Field
            objcmd.Parameters.Add("@sSenderSoftwareVersion", SqlDbType.VarChar)
            If oMessage.TransactionID Is Nothing = False Then
                objcmd.Parameters("@sSenderSoftwareVersion").Value = oMessage.SenderSoftwareVersion
            Else
                objcmd.Parameters("@sSenderSoftwareVersion").Value = DBNull.Value
            End If
            objcmd.Parameters.Add("@SenderSoftwareDeveloper", SqlDbType.VarChar)
            If oMessage.TransactionID Is Nothing = False Then
                objcmd.Parameters("@SenderSoftwareDeveloper").Value = oMessage.SenderSoftwareDeveloper
            Else
                objcmd.Parameters("@SenderSoftwareDeveloper").Value = DBNull.Value
            End If

            objcmd.Parameters.Add("@SenderSoftwareProduct", SqlDbType.VarChar)
            If oMessage.TransactionID Is Nothing = False Then
                objcmd.Parameters("@SenderSoftwareProduct").Value = oMessage.SenderSoftwareProduct
            Else
                objcmd.Parameters("@SenderSoftwareProduct").Value = DBNull.Value
            End If

            ''-----x-------
            objcmd.Parameters.Add("@IsAlertCheck", SqlDbType.Bit)
            objcmd.Parameters("@IsAlertCheck").Value = "False"


            objCon.Open()
            objcmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally
            objcmd.Cancel()

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Sub RetrieveRxResponses()
        Dim strReponseFile As String = ""
        Dim i As Integer = 0

        Try


            mdlGeneral.SetDbCredentials()
            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
                Dim objSendEncryption As clsEncryption = New clsEncryption
                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

                Dim ds As New DataSet


                Dim oDB As New gloDatabaseLayer.DBLayer(strgloServiceConnection)
                oDB.Connect(False)

                oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('EnableDirectory4dot4','eRxStagingWebserviceURL','eRx10dot6StagingWebserviceURL') ORDER BY sSettingsName", ds)

                oDB.Disconnect()
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If




                Dim strProviders As String = RetrieveProividers(strConnection)
                If Not strProviders Is Nothing And strProviders <> "" Then



                    'Dim _Access As String
                    ''Reset provider flag Flag
                    'bIsProviderAvailable = False

                    '_Access = myRxService.Login("sarika@ophit.net", "spX12ss@!!21nasik")
                    Dim cntResponse As Byte() = Nothing

                    ' mdlGeneral.UpdateLog("Access to Webservice is successful on Staging Server")
                    ' Dim cntResponse As Byte() = myRxService.GetResponses(strProviders, "", _Access)
                    ' _Access = String.Empty

                    If ds.Tables(0).Rows.Count > 2 Then
                        myRxWCFService = ServiceClass.GetWCFSvc(ds.Tables(0).Rows(1)("sSettingsValue").ToString())
                        mdlGeneral.UpdateLog("Access to Webservice is successful on Staging Server", strConnection)
                        cntResponse = myRxWCFService.GetResponses(strProviders, "")

                        'If ds.Tables(0).Rows(0)("sSettingsValue").ToString() = "True" Then


                        'Else
                        '    If IsNothing(myRxService) Then
                        '        myRxService = New eRxMessage
                        '    End If
                        '    myRxService.Url = ds.Tables(0).Rows(2)("sSettingsValue").ToString()
                        '    _Access = myRxService.Login("sarika@ophit.net", "spX12ss@!!21nasik")
                        '    mdlGeneral.UpdateLog("Access to Webservice is successful on Staging Server", strConnection)
                        '    cntResponse = myRxService.GetResponses(strProviders, "", _Access)
                        'End If
                    Else
                        mdlGeneral.UpdateLog("No eRx Web Service Settings Found ", strConnection)
                        Exit Sub
                    End If


                    If cntResponse Is Nothing Then
                        'mdlGeneral.UpdateLog("No new Responses are available on Staging Server")
                    Else
                        strReponseFile = mdlGeneral.ConvertBinarytoFile(cntResponse, mdlGeneral.GetFileName(enumFileType.XMLFile))
                        ''mdlGeneral.UpdateLog("Responses are saved at " & strReponseFile)
                        If strReponseFile <> "" Then
                            If File.Exists(strReponseFile) Then
                                Dim dsResponses As New DataSet
                                dsResponses.ReadXml(strReponseFile)
                                If Not dsResponses Is Nothing Then
                                    If dsResponses.Tables.Count > 0 Then

                                        gloSurescriptGeneral.blnIsStagingServer = mdlGeneral.gblnStagingServer
                                        gloSurescriptGeneral.eRx10dot6StagingWebserviceURL = ds.Tables(0).Rows(1)("sSettingsValue").ToString()

                                        Dim strTransaction As String = SaveRxResponses(dsResponses.Tables(0), strConnection)
                                        '  mdlGeneral.UpdateLog("SaveRxResponses completed - " & strTransaction)
                                        If strTransaction <> "" Then
                                            If ds.Tables(0).Rows.Count > 2 Then
                                                myRxWCFService.UpdateDownloadStatus(strTransaction)
                                                mdlGeneral.UpdateLog("UpdateDownloadStatus completed on Staging Server", strConnection)

                                                'If ds.Tables(0).Rows(0)("sSettingsValue").ToString() = "True" Then

                                                'Else
                                                '    _Access = myRxService.Login("sarika@ophit.net", "spX12ss@!!21nasik")
                                                '    myRxService.UpdateDownloadStatus(strTransaction, _Access)
                                                '    mdlGeneral.UpdateLog("UpdateDownloadStatus completed on Staging Server", strConnection)
                                                'End If
                                            Else
                                                mdlGeneral.UpdateLog("No eRx Web Service Settings Found ", strConnection)
                                                Exit Sub
                                            End If
                                        End If
                                    End If

                                End If

                            End If

                        End If
                    End If
                Else
                    'To avoid to write multiple time log message.
                    If bIsProviderAvailable = False Then
                        mdlGeneral.UpdateLog("No Provider are available with SPIID", strConnection)
                        bIsProviderAvailable = True
                    End If
                End If

                If Not IsNothing(ds) Then
                    ds.Dispose()
                    ds = Nothing
                End If
                If Not IsNothing(objSendEncryption) Then
                    objSendEncryption = Nothing
                End If
                If Not IsNothing(myRxWCFService) Then
                    myRxWCFService.Close()
                    myRxWCFService = Nothing
                End If
                If Not IsNothing(myRxService) Then
                    myRxService.Dispose()
                    myRxService = Nothing
                End If

                i = (i + 1)
            Loop
            i = 0

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
        Finally

            'delete the xml file after reading.
            If strReponseFile <> "" Then
                If File.Exists(strReponseFile) Then
                    File.Delete(strReponseFile)
                End If
            End If
        End Try

    End Sub

    Private Declare Function InternetGetConnectedState Lib "wininet" (ByRef conn As Long, ByVal val As Long) As Boolean

    Public Function IsInternetConnectionAvailable() As Boolean
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

    'Public Sub RetrieveSecureMessages()
    '    Dim strMessageFile As String = ""
    '    Dim i As Integer = 0

    '    Try


    '        mdlGeneral.SetDbCredentials()
    '        Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
    '            Dim objSendEncryption As clsEncryption = New clsEncryption
    '            Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
    '            Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword
    '            Dim MessageID As String = String.Empty
    '            Dim ds As New DataSet


    '            Dim oDB As New gloDatabaseLayer.DBLayer(strgloServiceConnection)
    '            oDB.Connect(False)

    '            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('SECUREMSGSTAGINGWEBSERVICEURL') ORDER BY sSettingsName", ds)

    '            oDB.Disconnect()
    '            If Not IsNothing(oDB) Then
    '                oDB.Dispose()
    '                oDB = Nothing
    '            End If




    '            Dim strProviders As String = RetrieveSecureMsgProividers(strConnection)

    '            If Not strProviders Is Nothing And strProviders <> "" Then


    '                Dim _Access As String

    '                Dim cntResponse As Byte() = Nothing



    '                If ds.Tables(0).Rows.Count >= 1 Then

    '                    mygloDirectService = ServiceClass.GetSecureWCFSvc(ds.Tables(0).Rows(0)("sSettingsValue").ToString())
    '                    ''mygloDirectService = ServiceClass.GetSecureWCFSvc("http://localhost:1454/gloDirect.svc")
    '                    _Access = mygloDirectService.Login("gloSecureMsg@ophit.net", "spX12ss@!!21nasik")
    '                    mdlGeneral.UpdateLog("Access to Webservice is successful on Staging Server - Secure Message")
    '                    cntResponse = mygloDirectService.GetResponse(strProviders)

    '                Else
    '                    mdlGeneral.UpdateLog("No Secure Message Web Service Settings Found ")
    '                    Exit Sub
    '                End If


    '                If cntResponse Is Nothing Then
    '                    'mdlGeneral.UpdateLog("No new Responses are available on Staging Server")
    '                Else
    '                    strMessageFile = mdlGeneral.ConvertBinarytoFile(cntResponse, mdlGeneral.GetFileName(enumFileType.XMLFile))
    '                    mdlGeneral.UpdateLog("Secure Message Responses are saved at " & strMessageFile)
    '                    If strMessageFile <> "" Then
    '                        If File.Exists(strMessageFile) Then
    '                            mdlGeneral.UpdateLog("Extract Recieved XML")
    '                            MessageID = ExtractXML(strMessageFile, strConnection)
    '                            If MessageID <> "" Then
    '                                mdlGeneral.UpdateLog("Update the download status")
    '                                mygloDirectService.UpdateMessages(MessageID)
    '                            End If

    '                        End If

    '                    End If
    '                End If
    '            Else
    '                'To avoid to write multiple time log message.
    '                If bIsProviderAvailable = False Then
    '                    mdlGeneral.UpdateLog("No Provider are available with Secure Message Address")
    '                    bIsProviderAvailable = True
    '                End If
    '            End If



    '            i = (i + 1)
    '        Loop
    '        i = 0

    '    Catch ex As Exception
    '        mdlGeneral.UpdateLog("Error :" & ex.ToString)
    '    Finally

    '        'delete the xml file after reading.
    '        If strMessageFile <> "" Then
    '            If File.Exists(strMessageFile) Then
    '                File.Delete(strMessageFile)
    '            End If
    '        End If


    '        If Not IsNothing(mygloDirectService) Then
    '            mygloDirectService.Close()
    '            mygloDirectService = Nothing
    '        End If


    '        'If Not IsNothing(myRxService) Then
    '        '    myRxService.Dispose()
    '        '    myRxService = Nothing
    '        'End If
    '    End Try





    'End Sub

    Public Sub RetrieveSecureMessages()
        Dim strMessageFile As String = ""
        Dim i As Integer = 0
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim objSendEncryption As clsEncryption = Nothing
        Dim ds As DataSet = Nothing
        Dim cntResponse As Byte() = Nothing

        Try


            mdlGeneral.SetDbCredentials()

            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
                objSendEncryption = New clsEncryption

                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword
                Dim MessageID As String = String.Empty
                'strConnection = "SERVER=glosvr02\sql2008r2;DATABASE=glo8000_dev;USER id=sa;Password=saglosvr02"
                ds = New DataSet
                oDB = New gloDatabaseLayer.DBLayer(strgloServiceConnection)
                oDB.Connect(False)
                If mdlGeneral.gblnStagingServer Then
                    oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('SECUREMSGSTAGINGWEBSERVICEURL') ORDER BY sSettingsName", ds)
                Else
                    oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('SECUREMSGPRODUCTIONWEBSERVICEURL') ORDER BY sSettingsName", ds)
                End If

                oDB.Disconnect()
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If


                Dim strProviders As String = RetrieveSecureMsgProividers(strConnection)

                If Not strProviders Is Nothing And strProviders <> "" Then

                    Dim _Access As String


                    If ds.Tables(0).Rows.Count >= 1 Then

                        mygloDirectService = ServiceClass.GetSecureWCFSvc(ds.Tables(0).Rows(0)("sSettingsValue").ToString())
                        ''mygloDirectService = ServiceClass.GetSecureWCFSvc("http://localhost:42677/gloDirect.svc/secure")
                        _Access = mygloDirectService.Login("gloSecureMsg@ophit.net", "spX12ss@!!21nasik")

                        If _Access <> "" Then

                            If mdlGeneral.gblnStagingServer Then
                                mdlGeneral.UpdateLog("Access to Web service is successful on Staging Server", strConnection)
                            Else
                                mdlGeneral.UpdateLog("Access to Web service is successful on Production Server", strConnection)
                            End If
                            Dim sMessageId As String
                            sMessageId = mygloDirectService.GetMessageID(strProviders)

                            If sMessageId <> "" Then

                                Dim ArrayMessageID() As String
                                ArrayMessageID = sMessageId.Split(",")
                                Dim sFileAttr As String
                                Dim sFileAttrs() As String
                                'Dim BufferSize As Long
                                'BufferSize = 1024 * 64

                                If ArrayMessageID.Length > 0 Then

                                    Dim index As Integer

                                    For index = 0 To ArrayMessageID.Length - 1

                                        sFileAttr = mygloDirectService.GetResponse(ArrayMessageID(index).ToString(), 1)
                                        strMessageFile = mdlGeneral.GetFileName(enumFileType.XMLFile)
                                        Dim stream As FileStream = File.Open(strMessageFile, FileMode.Append)
                                        Dim myFile As String = ""
                                        If sFileAttr <> "" Then
                                            sFileAttrs = sFileAttr.Split("|")
                                            Dim MaxReadBuffsize As Int32 = Convert.ToInt32(sFileAttrs(2))
                                            Dim myFileSize As Long = Convert.ToInt64(sFileAttrs(1))
                                            myFile = Convert.ToString(sFileAttrs(0))
                                            Dim myRemainingSize As Long = myFileSize
                                            Dim myReadOffset As Long = 0

                                            While myRemainingSize > 0

                                                cntResponse = mygloDirectService.DownloadChunk(myFile, myReadOffset, MaxReadBuffsize)
                                                If (cntResponse.Length > 0) Then
                                                    stream.Write(cntResponse, 0, cntResponse.Length)
                                                    myRemainingSize -= cntResponse.Length
                                                    myReadOffset += cntResponse.Length
                                                    cntResponse = Nothing
                                                End If

                                            End While

                                            stream.Flush()
                                            stream.Close()
                                        End If

                                        stream.Dispose()

                                        If strMessageFile = "" Then
                                            mdlGeneral.UpdateLog("No new Responses are available on Staging Server", strConnection)
                                        Else
                                            ''strMessageFile = mdlGeneral.ConvertBinarytoFile(cntResponse, mdlGeneral.GetFileName(enumFileType.XMLFile))
                                            mdlGeneral.UpdateLog("Responses are saved at " & strMessageFile, strConnection)
                                            If strMessageFile <> "" Then

                                                Dim ackFile As String = String.Empty


                                                If File.Exists(strMessageFile) Then
                                                    MessageID = ExtractXML(strMessageFile, strConnection, ackFile)


                                                    If MessageID <> "" Then
                                                        mygloDirectService.UpdateMessages(MessageID, myFile)
                                                        If Not ackFile Is Nothing Then
                                                            If ackFile <> "" And File.Exists(ackFile) Then
                                                                Dim cntReponse As Byte() = ConvertFiletoBinary(ackFile)

                                                                ' Region Retrieve SSPI ,Clinic,Location
                                                                Dim ackbyte() As Byte

                                                                If sTo <> "" Then
                                                                    ds = New DataSet
                                                                    oDB = New gloDatabaseLayer.DBLayer(strConnection)
                                                                    Dim odbParameters As New gloDatabaseLayer.DBParameters
                                                                    odbParameters.Add("@sDirectAddress", sTo, ParameterDirection.Input, SqlDbType.NVarChar)
                                                                    oDB.Connect(False)
                                                                    oDB.Retrive("gsp_GetClinic_ProviderDetails", odbParameters, ds)
                                                                    If (ds Is Nothing = False) Then
                                                                        If (ds.Tables.Count > 0) Then
                                                                            If (ds.Tables(0).Rows.Count > 0) Then
                                                                                mdlGeneral.nClinicID = ds.Tables(0).Rows(0)("nClinicID")
                                                                                mdlGeneral.sClinicName = ds.Tables(0).Rows(0)("sClinicName")
                                                                                mdlGeneral.SiteID = ds.Tables(0).Rows(0)("SiteID")
                                                                                mdlGeneral.Location = ds.Tables(0).Rows(0)("Location")
                                                                                mdlGeneral.AUSID = ds.Tables(0).Rows(0)("AUSID")
                                                                            End If
                                                                            If (ds.Tables(1).Rows.Count > 0) Then
                                                                                mdlGeneral.sSPIID = ds.Tables(1).Rows(0)("sSPIID")
                                                                            End If

                                                                        End If
                                                                    End If

                                                                    oDB.Disconnect()
                                                                    If Not IsNothing(odbParameters) Then
                                                                        odbParameters.Dispose()
                                                                        odbParameters = Nothing
                                                                    End If
                                                                    If Not IsNothing(oDB) Then
                                                                        oDB.Dispose()
                                                                        oDB = Nothing
                                                                    End If
                                                                Else
                                                                    mdlGeneral.nClinicID = 1
                                                                    mdlGeneral.sClinicName = "gloClinic"
                                                                    mdlGeneral.SiteID = "1"
                                                                    mdlGeneral.Location = "1"
                                                                    mdlGeneral.AUSID = "1"

                                                                End If
                                                                'End Region

                                                                ackbyte = mygloDirectService.PostSecureMessage(MessageID.Replace("'", ""), sFrom, sTo, mdlGeneral.sSPIID, mdlGeneral.sClinicName, mdlGeneral.AUSID, mdlGeneral.SiteID, mdlGeneral.Location, cntReponse, SecureMessageType.Verify)
                                                                Dim strAckVerifystatus As String = ConvertBinarytoFile(ackbyte, mdlGeneral.GetFileName(enumFileType.XMLFile))
                                                                If System.IO.File.Exists(ackFile) Then
                                                                    System.IO.File.Delete(ackFile)
                                                                End If
                                                                If System.IO.File.Exists(strAckVerifystatus) Then
                                                                    System.IO.File.Delete(strAckVerifystatus)
                                                                End If
                                                            End If
                                                        End If
                                                    End If

                                                End If

                                            End If
                                        End If

                                        If System.IO.File.Exists(strMessageFile) Then
                                            System.IO.File.Delete(strMessageFile)
                                        End If
                                    Next

                                End If
                            Else
                                mdlGeneral.UpdateLog("No Message ID Found To Retrieve", strConnection)
                            End If
                        Else
                            mdlGeneral.UpdateLog("Login Failed", strConnection)
                        End If

                    Else
                        mdlGeneral.UpdateLog("No Secure Message Web Service Settings Found ", strConnection)

                        If Not IsNothing(ds) Then
                            ds.Dispose()
                            ds = Nothing
                        End If
                        Exit Sub
                    End If



                Else
                    'To avoid to write multiple time log message.
                    If bIsProviderAvailable = False Then
                        mdlGeneral.UpdateLog("No Provider are available with Secure Message Address")
                        bIsProviderAvailable = True
                    End If
                End If

                If Not IsNothing(ds) Then
                    ds.Dispose()
                    ds = Nothing
                End If
                If Not IsNothing(mygloDirectService) Then
                    mygloDirectService.Close()
                    mygloDirectService = Nothing
                End If
                If Not IsNothing(objSendEncryption) Then
                    objSendEncryption = Nothing
                End If
                i = (i + 1)
            Loop
            i = 0
           

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
        Finally

            'delete the xml file after reading.
            If strMessageFile <> "" Then
                If File.Exists(strMessageFile) Then
                    File.Delete(strMessageFile)
                End If
            End If

            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(mygloDirectService) Then
                mygloDirectService.Close()
                mygloDirectService = Nothing
            End If
            cntResponse = Nothing

        End Try





    End Sub
    'Public Function GenerateXMLforSecureStatus(ByVal dtAck As DataTable) As String


    '    Dim strfilepath As String '= mdlGeneral.GetFileName("D:\\temp")
    '    strfilepath = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
    '    Dim _messageContent As N2NMessageType = Nothing
    '    Dim _messageHeader As N2NHeaderType = Nothing
    '    Dim _messageBody As N2NBodyType = Nothing
    '    Dim _messageTo As N2NAddressType = Nothing
    '    Dim _messageFrom As N2NAddressType = Nothing
    '    Dim _senderDetails As SenderSoftwareType = Nothing
    '    Dim _VerifyStatus As Verify = Nothing
    '    Dim _verifyverifyStatus As VerifyVerifyStatus = Nothing

    '    _messageContent = New N2NMessageType()
    '    _messageHeader = New N2NHeaderType()
    '    _messageBody = New N2NBodyType()
    '    _messageTo = New N2NAddressType()
    '    _messageFrom = New N2NAddressType()
    '    _senderDetails = New SenderSoftwareType()
    '    _verifyverifyStatus = New VerifyVerifyStatus()
    '    _VerifyStatus = New Verify()

    '    Dim dtdate As DateTime = System.DateTime.UtcNow
    '    Dim strdate As String = dtdate.ToString("yyyy-MM-dd")
    '    Dim strtime As String = dtdate.ToString("hh:mm:ss")
    '    Dim utc As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, _
    '     DateTime.Now.Millisecond, DateTimeKind.Utc)

    '    _messageContent.release = Convert.ToString(dtAck.Rows(0)("sMessageReleaseNo"))
    '    _messageContent.version = Convert.ToString(dtAck.Rows(0)("sMessageVersionNo"))
    '    '' _messageContent.HighestVersionSupported = Convert.ToString(dtAck.Rows(0)("sMessageHighestVersion"))

    '    _messageTo.Qualifier = Convert.ToString(dtAck.Rows(0)("sFromQualifier"))
    '    _messageTo.Value = Convert.ToString(dtAck.Rows(0)("sFrom"))

    '    _messageFrom.Qualifier = Convert.ToString(dtAck.Rows(0)("sToQualifier"))
    '    _messageFrom.Value = Convert.ToString(dtAck.Rows(0)("sTo"))

    '    'Header
    '    _messageHeader.To = _messageTo
    '    _messageHeader.From = _messageFrom

    '    Dim ds As New DataSet
    '    Dim oDB As New gloDatabaseLayer.DBLayer(strConnection)
    '    oDB.Connect(False)
    '    Dim _strQuery As String = "SELECT REPLACE(NEWID(), '-', '') as sNEWID "
    '    oDB.Retrive_Query(_strQuery, ds)

    '    oDB.Disconnect()
    '    If Not IsNothing(oDB) Then
    '        oDB.Dispose()
    '        oDB = Nothing
    '    End If

    '    If ds.Tables.Count > 0 Then
    '        _messageHeader.MessageID = ds.Tables(0).Rows(0)("sNEWID").ToString()
    '    End If




    '    _messageHeader.RelatesToMessageID = Convert.ToString(dtAck.Rows(0)("sMessageID"))
    '    _messageHeader.SentTime = utc


    '    'Sender Software Developer
    '    _senderDetails.SenderSoftwareDeveloper = "gloStream Inc."
    '    _senderDetails.SenderSoftwareProduct = "gloEMR"
    '    _senderDetails.SenderSoftwareVersionRelease = System.Windows.Forms.Application.ProductVersion

    '    _messageHeader.SenderSoftware = _senderDetails
    '    _messageContent.Header = _messageHeader


    '    'Body
    '    _verifyverifyStatus.Code = "010"
    '    _verifyverifyStatus.Description = "Clinical message delivered"
    '    _VerifyStatus.VerifyStatus = _verifyverifyStatus

    '    _messageBody.Item = _VerifyStatus
    '    _messageContent.Body = _messageBody



    '    Dim xs As XmlSerializer = Nothing
    '    Dim fs As FileStream = Nothing

    '    Try

    '        xs = New XmlSerializer(GetType(N2NMessageType))
    '        fs = New FileStream(strfilepath, FileMode.Create)
    '        xs.Serialize(fs, _messageContent)
    '        fs.Close()

    '    Catch ex As Exception

    '    Finally
    '        If fs IsNot Nothing Then
    '            fs = Nothing
    '        End If
    '        If xs IsNot Nothing Then
    '            xs = Nothing
    '        End If
    '    End Try
    'End Function

    Public Function GenerateXMLforSecureStatus(ByVal dtAck As DataTable) As String


        Dim strfilepath As String '= mdlGeneral.GetFileName("D:\\temp")
        strfilepath = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
        Dim _messageContent As N2NMessageType = Nothing
        Dim _messageHeader As N2NHeaderType = Nothing
        Dim _messageBody As N2NBodyType = Nothing
        Dim _messageTo As N2NAddressType = Nothing
        Dim _messageFrom As N2NAddressType = Nothing
        Dim _senderDetails As SenderSoftwareType = Nothing
        Dim _VerifyStatus As Verify = Nothing
        Dim _verifyverifyStatus As VerifyVerifyStatus = Nothing

        _messageContent = New N2NMessageType()
        _messageHeader = New N2NHeaderType()
        _messageBody = New N2NBodyType()
        _messageTo = New N2NAddressType()
        _messageFrom = New N2NAddressType()
        _senderDetails = New SenderSoftwareType()
        _verifyverifyStatus = New VerifyVerifyStatus()
        _VerifyStatus = New Verify()

        Dim dtdate As DateTime = System.DateTime.UtcNow
        Dim strdate As String = dtdate.ToString("yyyy-MM-dd")
        Dim strtime As String = dtdate.ToString("hh:mm:ss")
        Dim utc As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, _
         DateTime.Now.Millisecond, DateTimeKind.Utc)

        _messageContent.release = Convert.ToString(dtAck.Rows(0)("sMessageReleaseNo"))
        _messageContent.version = Convert.ToString(dtAck.Rows(0)("sMessageVersionNo"))
        '' _messageContent.HighestVersionSupported = Convert.ToString(dtAck.Rows(0)("sMessageHighestVersion"))

        _messageTo.Qualifier = Convert.ToString(dtAck.Rows(0)("sFromQualifier"))
        _messageTo.Value = Convert.ToString(dtAck.Rows(0)("sFrom"))


        If gstrDomainExtension <> String.Empty Then
            Dim bContainsString As Boolean = _messageTo.Value.EndsWith(gstrDomainExtension)
            If (bContainsString) Then
                _messageTo.Value = _messageTo.Value.Remove(_messageTo.Value.LastIndexOf(gstrDomainExtension))
            End If
        End If

        _messageFrom.Qualifier = Convert.ToString(dtAck.Rows(0)("sToQualifier"))
        _messageFrom.Value = Convert.ToString(dtAck.Rows(0)("sTo"))

        'Header
        _messageHeader.To = _messageTo
        _messageHeader.From = _messageFrom

        Dim ds As New DataSet
        Dim oDB As New gloDatabaseLayer.DBLayer(strConnection)
        oDB.Connect(False)
        Dim _strQuery As String = "SELECT REPLACE(NEWID(), '-', '') as sNEWID "
        oDB.Retrive_Query(_strQuery, ds)

        oDB.Disconnect()
        If Not IsNothing(oDB) Then
            oDB.Dispose()
            oDB = Nothing
        End If

        If ds.Tables.Count > 0 Then
            _messageHeader.MessageID = ds.Tables(0).Rows(0)("sNEWID").ToString()
        End If




        _messageHeader.RelatesToMessageID = Convert.ToString(dtAck.Rows(0)("sMessageID"))
        _messageHeader.SentTime = utc


        'Sender Software Developer
        _senderDetails.SenderSoftwareDeveloper = "TRIARQ Practice Services"
        _senderDetails.SenderSoftwareProduct = "QEMR"
        _senderDetails.SenderSoftwareVersionRelease = System.Windows.Forms.Application.ProductVersion

        _messageHeader.SenderSoftware = _senderDetails
        _messageContent.Header = _messageHeader


        'Body
        _verifyverifyStatus.Code = "010"
        _verifyverifyStatus.Description = "Clinical message delivered"
        _VerifyStatus.VerifyStatus = _verifyverifyStatus

        _messageBody.Item = _VerifyStatus
        _messageContent.Body = _messageBody



        Dim xs As XmlSerializer = Nothing
        Dim fs As FileStream = Nothing

        Try

            xs = New XmlSerializer(GetType(N2NMessageType))
            fs = New FileStream(strfilepath, FileMode.Create)
            xs.Serialize(fs, _messageContent)
            fs.Close()

        Catch ex As Exception

        Finally
            If fs IsNot Nothing Then
                fs.Dispose()
                fs = Nothing
            End If
            If xs IsNot Nothing Then
                xs = Nothing
            End If
        End Try



        Return strfilepath


        If _senderDetails IsNot Nothing Then
            _senderDetails = Nothing
        End If

        If _messageFrom IsNot Nothing Then
            _messageFrom = Nothing
        End If

        If _messageTo IsNot Nothing Then
            _messageTo = Nothing
        End If

        If _messageBody IsNot Nothing Then
            _messageBody = Nothing
        End If

        If _messageHeader IsNot Nothing Then
            _messageHeader = Nothing
        End If

        If _messageContent IsNot Nothing Then
            _messageContent = Nothing

        End If
    End Function

    Public Function GenerateXMLforSecureStatus(ByVal dtAck As DataTable, ByVal PatientSaving As Int16) As String


        Dim strfilepath As String '= mdlGeneral.GetFileName("D:\\temp")
        strfilepath = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
        Dim _messageContent As N2NMessageType = Nothing
        Dim _messageHeader As N2NHeaderType = Nothing
        Dim _messageBody As N2NBodyType = Nothing
        Dim _messageTo As N2NAddressType = Nothing
        Dim _messageFrom As N2NAddressType = Nothing
        Dim _senderDetails As SenderSoftwareType = Nothing
        Dim _VerifyStatus As Verify = Nothing
        Dim _verifyverifyStatus As VerifyVerifyStatus = Nothing

        _messageContent = New N2NMessageType()
        _messageHeader = New N2NHeaderType()
        _messageBody = New N2NBodyType()
        _messageTo = New N2NAddressType()
        _messageFrom = New N2NAddressType()
        _senderDetails = New SenderSoftwareType()
        _verifyverifyStatus = New VerifyVerifyStatus()
        _VerifyStatus = New Verify()

        Dim dtdate As DateTime = System.DateTime.UtcNow
        Dim strdate As String = dtdate.ToString("yyyy-MM-dd")
        Dim strtime As String = dtdate.ToString("hh:mm:ss")
        Dim utc As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, _
         DateTime.Now.Millisecond, DateTimeKind.Utc)

        _messageContent.release = Convert.ToString(dtAck.Rows(0)("sMessageReleaseNo"))
        _messageContent.version = Convert.ToString(dtAck.Rows(0)("sMessageVersionNo"))
        '' _messageContent.HighestVersionSupported = Convert.ToString(dtAck.Rows(0)("sMessageHighestVersion"))

        _messageTo.Qualifier = Convert.ToString(dtAck.Rows(0)("sFromQualifier"))
        _messageTo.Value = Convert.ToString(dtAck.Rows(0)("sFrom"))


        If gstrDomainExtension <> String.Empty Then
            Dim bContainsString As Boolean = _messageTo.Value.EndsWith(gstrDomainExtension)
            If (bContainsString) Then
                _messageTo.Value = _messageTo.Value.Remove(_messageTo.Value.LastIndexOf(gstrDomainExtension))
            End If
        End If

        _messageFrom.Qualifier = Convert.ToString(dtAck.Rows(0)("sToQualifier"))
        _messageFrom.Value = Convert.ToString(dtAck.Rows(0)("sTo"))

        'Header
        _messageHeader.To = _messageTo
        _messageHeader.From = _messageFrom

        Dim ds As New DataSet
        Dim oDB As New gloDatabaseLayer.DBLayer(strConnection)
        oDB.Connect(False)
        Dim _strQuery As String = "SELECT REPLACE(NEWID(), '-', '') as sNEWID "
        oDB.Retrive_Query(_strQuery, ds)

        oDB.Disconnect()
        If Not IsNothing(oDB) Then
            oDB.Dispose()
            oDB = Nothing
        End If

        If ds.Tables.Count > 0 Then
            _messageHeader.MessageID = ds.Tables(0).Rows(0)("sNEWID").ToString()
        End If




        _messageHeader.RelatesToMessageID = Convert.ToString(dtAck.Rows(0)("sMessageID"))
        _messageHeader.SentTime = utc


        'Sender Software Developer
        _senderDetails.SenderSoftwareDeveloper = "TRIARQ Practice Services"
        _senderDetails.SenderSoftwareProduct = "QEMR"
        _senderDetails.SenderSoftwareVersionRelease = System.Windows.Forms.Application.ProductVersion

        _messageHeader.SenderSoftware = _senderDetails
        _messageContent.Header = _messageHeader


        'Body
        If PatientSaving = 0 Then
            _verifyverifyStatus.Code = "010"
            _verifyverifyStatus.Description = "Clinical message delivered"
            _VerifyStatus.VerifyStatus = _verifyverifyStatus

        Else
            _verifyverifyStatus.Code = "010"
            _verifyverifyStatus.Description = "Patient saving message delivered"
            _VerifyStatus.VerifyStatus = _verifyverifyStatus

        End If

        _messageBody.Item = _VerifyStatus
        _messageContent.Body = _messageBody



        Dim xs As XmlSerializer = Nothing
        Dim fs As FileStream = Nothing

        Try

            xs = New XmlSerializer(GetType(N2NMessageType))
            fs = New FileStream(strfilepath, FileMode.Create)
            xs.Serialize(fs, _messageContent)
            fs.Close()

        Catch ex As Exception

        Finally
            If fs IsNot Nothing Then
                fs.Dispose()
                fs = Nothing
            End If
            If xs IsNot Nothing Then
                xs = Nothing
            End If
        End Try



        Return strfilepath


        If _senderDetails IsNot Nothing Then
            _senderDetails = Nothing
        End If

        If _messageFrom IsNot Nothing Then
            _messageFrom = Nothing
        End If

        If _messageTo IsNot Nothing Then
            _messageTo = Nothing
        End If

        If _messageBody IsNot Nothing Then
            _messageBody = Nothing
        End If

        If _messageHeader IsNot Nothing Then
            _messageHeader = Nothing
        End If

        If _messageContent IsNot Nothing Then
            _messageContent = Nothing

        End If
    End Function

    Public Function ExtractXML(ByVal strFile As String, ByVal strConnString As String, ByRef sAck As String) As String
        Dim MessageId As String = ""
        Dim dsResponse As DataSet = Nothing
        Dim objDataExtraction As gloRxPatientSaving.clsDataExtraction = Nothing
        Dim dtAttach As DataTable = Nothing
        Dim dtMessage As DataTable = Nothing
        Dim dtCloned As DataTable = Nothing

        Try

            Dim isPatientSaving As Int16 = 0
            Dim strstatusMessage As String = String.Empty

            Dim strFileName As String = strFile

            If (strFileName.Trim() <> "") Then
                dsResponse = New DataSet()
                dsResponse.ReadXml(strFileName)


                If dsResponse.Tables.Count > 1 Then
                    dsResponse.Tables(1).Constraints.Clear()
                End If
                dsResponse.Tables(0).Constraints.Clear()


                If dsResponse.Tables.Count > 1 Then
                    dsResponse.Tables(0).ChildRelations.Clear()
                    dsResponse.Tables(1).ParentRelations.Clear()
                End If
                dsResponse.AcceptChanges()

                If dsResponse.Tables(0).Columns.Contains("Table_ID") Then
                    dsResponse.Tables(0).Columns.Remove("Table_ID")
                End If

                If dsResponse.Tables.Count > 1 Then
                    If dsResponse.Tables(1).Columns.Contains("Table_ID") Then
                        dsResponse.Tables(1).Columns.Remove("Table_ID")
                    End If
                End If
                dsResponse.AcceptChanges()

                dtAttach = Nothing
                dtMessage = Nothing

                'dtMessage = New DataTable
                dtMessage = dsResponse.Tables(0)
                'dtAttach = New DataTable
                If dsResponse.Tables.Count > 1 Then
                    dtAttach = dsResponse.Tables(1)
                Else
                    dtAttach = New DataTable
                End If



                dtCloned = Nothing
                If dsResponse.Tables.Count > 1 Then
                    dtCloned = dtAttach.Clone()
                    dtCloned.Columns(5).DataType = GetType(Byte())
                End If

                Dim byte16Array As Byte()
                Dim byte8Array As Byte()
                Dim byteArray64 As String
                Dim index As Integer

                If Not IsNothing(dtAttach) Then
                    For index = 0 To dtAttach.Rows.Count - 1
                        dtCloned.Rows.Add()
                        dtCloned.Rows(index)(0) = dtAttach.Rows(index)(0)
                        dtCloned.Rows(index)(1) = dtAttach.Rows(index)(1)
                        dtCloned.Rows(index)(2) = dtAttach.Rows(index)(2)
                        dtCloned.Rows(index)(3) = dtAttach.Rows(index)(3)
                        dtCloned.Rows(index)(4) = dtAttach.Rows(index)(4)
                        dtCloned.Rows(index)(6) = dtAttach.Rows(index)(6)


                        Dim myBytes As Byte() = Encoding.Default.GetBytes(dtAttach.Rows(index)(5))
                        Dim myObj As Object = dtAttach.Rows(index)(5)


                        Try
                            byte16Array = Convert.FromBase64String(myObj)
                        Catch
                            byte16Array = myBytes
                        End Try

                        Dim oendcode As System.Text.Encoding = EncodingDetector.DetectEncoding(byte16Array)

                        byte8Array = Nothing
                        If Not IsNothing(oendcode) Then
                            If Not (oendcode Is Encoding.UTF8) Then
                                byte8Array = Encoding.Convert(oendcode, Encoding.UTF8, byte16Array)
                            Else

                                byte8Array = byte16Array
                            End If
                        Else
                            byte8Array = byte16Array
                        End If
                        byteArray64 = Encoding.Default.GetString(byte8Array)
                        Try
                            byte16Array = Convert.FromBase64String(byteArray64)
                        Catch ex As Exception
                            Try
                                byte16Array = Convert.FromBase64String(Encoding.Default.GetString(byte16Array))
                            Catch
                                Try
                                    byte16Array = Convert.FromBase64String(myObj)
                                Catch
                                    byte16Array = myBytes
                                End Try
                            End Try
                        End Try

                        dtCloned.Rows(index)(5) = byte16Array
                    Next
                End If

                MessageId = 0

                'Generate the Response for the Clinical Message Only    
                If dsResponse.Tables(0).Columns.Contains("bMessageType") Then

                    If Convert.ToString(dsResponse.Tables(0).Rows(0)("bMessageType")) = "1" Then
                        sFrom = Convert.ToString(dsResponse.Tables(0).Rows(0)("sTo"))
                        sTo = Convert.ToString(dsResponse.Tables(0).Rows(0)("sFrom"))

                        If gstrDomainExtension <> String.Empty Then
                            Dim bContainsString As Boolean = sTo.EndsWith(gstrDomainExtension)
                            If (bContainsString) Then
                                sTo = sTo.Remove(sTo.LastIndexOf(gstrDomainExtension))
                            End If
                        End If

                        dsResponse.Tables(0).Rows(0)("bMessageStatus") = Convert.ToInt16(SecureMessageStatus.Receive.GetHashCode())
                        dsResponse.Tables(0).Rows(0)("bMessageType") = Convert.ToInt16(SecureMessageType.Status.GetHashCode())
                        MessageId = InsertXMLToTable(dtMessage, dtCloned, strConnString)


                        If dsResponse.Tables(0).Columns.Contains("nUseCase") Then
                            If dsResponse.Tables(0).Columns.Contains("nUseCase") <> Nothing Then
                                If Convert.ToInt16(dsResponse.Tables(0).Rows(0)("nUseCase")) = 1 Then
                                    isPatientSaving = 1
                                End If
                            End If

                        End If

                        If isPatientSaving = 1 Then
                            If bIsPatientSavingMessageQueue AndAlso MessageId <> "" Then

                                gloRxPatientSaving.PatSavGeneral.FilePath = gloSettings.FolderSettings.AppTempFolderPath()
                                gloRxPatientSaving.PatSavGeneral.ConnectionString = strConnString

                                Dim QueueID As Long = InsertToPatientSavingsQueue(MessageId, strConnString)
                                If QueueID <> 0 Then
                                    objDataExtraction = New gloRxPatientSaving.clsDataExtraction
                                    If objDataExtraction.ExtractQueueData(QueueID) Then
                                        strstatusMessage = GetStatusForPatientSavingsUsingQueueID(QueueID, strConnString)
                                    End If


                                End If
                            End If
                        End If

                        If isPatientSaving = 0 Then
                            sAck = GenerateXMLforSecureStatus(dtMessage, isPatientSaving)
                        Else
                            If strstatusMessage.ToUpper = "PROCESSED" Then
                                sAck = GenerateXMLforSecureStatus(dtMessage, isPatientSaving)
                            End If
                        End If



                    Else
                        ' String strSql="Update SecureMessage_Inbox SET sDeliveryStatusCode="+ dsResponse.Tables(0).Rows(0)("bMessageType") +" ,sDeliveryStatusDescription= "+ +"  "
                        MessageId = UpdateAcknowledgmentStatus(dsResponse, strConnString)

                    End If
                End If


            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error in ExtractXML() :" & ex.ToString, strConnString)
        Finally
            If Not IsNothing(dsResponse) Then
                dsResponse.Dispose()
                dsResponse = Nothing
            End If
            If Not IsNothing(dtMessage) Then
                dtMessage.Dispose()
                dtMessage = Nothing
            End If
            If Not IsNothing(dtAttach) Then
                dtAttach.Dispose()
                dtAttach = Nothing
            End If
            If Not IsNothing(dtCloned) Then
                dtCloned.Dispose()
                dtCloned = Nothing
            End If
            If Not IsNothing(objDataExtraction) Then
                objDataExtraction = Nothing
            End If
        End Try

        Return MessageId


    End Function
    Public Function InsertToPatientSavingsQueue(ByVal _MessageID As String, ByVal _strConnString As String) As Long

        Dim ogloDatabaseLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim strTransID As String = String.Empty
        Dim result As Object = Nothing
        _MessageID = _MessageID.Replace("'", "")

        Try
            ogloDatabaseLayer = New gloDatabaseLayer.DBLayer(_strConnString)
            oDBParameters = New gloDatabaseLayer.DBParameters()
            oDBParameters.Add("@nPatSavQID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBParameters.Add("@sMessageID", _MessageID, ParameterDirection.Input, SqlDbType.VarChar)
            ogloDatabaseLayer.Connect(False)
            result = ogloDatabaseLayer.ExecuteScalar("PatSav_PatientSavingsQueue", oDBParameters)
            ogloDatabaseLayer.Disconnect()

            Return result

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return 0

        Finally

            If Not IsNothing(result) Then
                ''result.Dispose()
                result = Nothing
            End If

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(ogloDatabaseLayer) Then
                ogloDatabaseLayer.Dispose()
                ogloDatabaseLayer = Nothing
            End If

        End Try

    End Function
    Private Function UpdateAcknowledgmentStatus(ByVal dsResponse As DataSet, ByVal _strConnString As String) As String
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(_strConnString)
        Dim objResult As Object = Nothing
        Dim _messageID As String = ""
        Dim _dt As DataTable = Nothing
        Try

            If dsResponse Is Nothing = False Then

                If dsResponse.Tables.Count > 0 Then
                    Dim strSql As String = ""
                    oDBLayer.Connect(False)

                    strSql = "SELECT nSecureMessageInboxID FROM SecureMessage_Inbox WHERE bMessageType = 1 and sMessageID='" + Convert.ToString(dsResponse.Tables(0).Rows(0)("sRelatesToMessageID")) + "' "
                    oDBLayer.Retrive_Query(strSql, _dt)

                    If Not _dt Is Nothing Then
                        If _dt.Rows.Count > 0 Then
                            Dim sTransmitStatusDesc As String = Convert.ToString(dsResponse.Tables(0).Rows(0)("sDeliveryStatusCode")) + " - " + Convert.ToString(dsResponse.Tables(0).Rows(0)("sDeliveryStatusDescription"))
                            Dim nSecureMessageInboxID As Int64 = 0
                            nSecureMessageInboxID = Convert.ToDecimal(_dt.Rows(0)("nSecureMessageInboxID"))
                            strSql = ""
                            strSql = "UPDATE IntuitCommDetails SET sTransmitStatusDesc ='" & sTransmitStatusDesc & "'  WHERE nMessageID= " & nSecureMessageInboxID & " "
                            objResult = oDBLayer.Execute_Query(strSql)

                            strSql = ""
                        End If
                    End If

                    strSql = ""
                    strSql = "UPDATE SecureMessage_Inbox SET sDeliveryStatusCode='" + Convert.ToString(dsResponse.Tables(0).Rows(0)("sDeliveryStatusCode")) + "' ,sDeliveryStatusDescription= '" + Convert.ToString(dsResponse.Tables(0).Rows(0)("sDeliveryStatusDescription")) + "' WHERE sMessageID= '" + Convert.ToString(dsResponse.Tables(0).Rows(0)("sRelatesToMessageID")) + "' "
                    objResult = oDBLayer.Execute_Query(strSql)

                    oDBLayer.Disconnect()
                    _messageID = Convert.ToString(dsResponse.Tables(0).Rows(0)("sMessageID"))
                    _messageID = "'" & _messageID & "'"
                End If
            End If

        Catch ex As SqlException
            mdlGeneral.UpdateLog("DBError in Updating Response status :" & ex.ToString, strConnection)
            Return False
        Catch ex As Exception
            mdlGeneral.UpdateLog("DBError in Updating Response status :" & ex.ToString, strConnection)
            Return False
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
            End If

        End Try

        Return _messageID
    End Function


    Public Function InsertXMLToTable(ByVal dtMessage As DataTable, ByVal dtAttach As DataTable, ByVal _strConnString As String) As String

        Dim MessageId As String = String.Empty
        Dim ogloDatabaseLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim strTransID As String = String.Empty
        Dim result As Object = Nothing
        Dim strResult As String = String.Empty

        Try
            ogloDatabaseLayer = New gloDatabaseLayer.DBLayer(_strConnString)
            oDBParameters = New gloDatabaseLayer.DBParameters()
            oDBParameters.Add("@TVP_Messages", dtMessage, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@TVP_Attachment", dtAttach, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@sTranresult", strTransID, ParameterDirection.Output, SqlDbType.VarChar)
            ogloDatabaseLayer.Connect(False)
            result = ogloDatabaseLayer.ExecuteScalar("SaveSecureMessage_TVP", oDBParameters)
            ogloDatabaseLayer.Disconnect()

            strResult = result.ToString()

            If strResult = "sucess" Then

                Dim cnt As Int32
                For Each dr As DataRow In dtMessage.Rows
                    If cnt = 0 Then
                        MessageId = "'" & dr.Item("sMessageID") & "'"
                    Else
                        MessageId = MessageId & "," & "'" & dr.Item("sMessageID") & "'"
                    End If
                    cnt = cnt + 1
                Next

            End If

            Return MessageId

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, _strConnString)
            Return MessageId

        Finally

            If Not IsNothing(result) Then
                ''result.Dispose()
                result = Nothing
            End If

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(ogloDatabaseLayer) Then
                ogloDatabaseLayer.Dispose()
                ogloDatabaseLayer = Nothing
            End If

        End Try

    End Function

#Region "Auto Eligibility Functions"
    'Public Sub ProcessAutoEligiblity()
    '    Dim i As Integer = 0 ''''this var is for database counter
    '    Dim dtPatAppt As DataTable = Nothing
    '    Dim dtRxhubSettings As DataTable = Nothing
    '    Dim dtMsgType As DataTable = Nothing
    '    'Dim ofrmEDIGeneration As gloRxHub.frmRxhub270EDIGeneration = Nothing


    '    Dim sAdvanceRxEnabled As String = ""
    '    Dim sAdvanceStagingServer As String = ""
    '    Dim sRxHubParticipantId As String = ""
    '    Dim sRxHubPassword As String = ""
    '    Dim sRXELIGIBILITYEMR As String = "BYCODE" '''''byDefault set to BYCODE, if the setting is BYSERVICE then call the code wrt to WEBService 
    '    Dim sEDISERVICEPATH As String = "" '''''set this url to the Rxeligibility web service object

    '    Dim objSendEncryption As clsEncryption = Nothing

    '    Dim oClsRxH_271Master As gloRxHub.ClsRxH_271Master = Nothing
    '    Dim oCls271Information As gloRxHub.Cls271Information = Nothing
    '    Dim ofrmPostSample As gloRxHub.frmSamplePost = Nothing
    '    Dim oClsRxHubInterface As gloRxHub.ClsRxHubInterface = Nothing
    '    Dim oEligibilityCheck As gloRxHub.clsEligibilityCheckDBLayer = Nothing

    '    Try
    '        dtPatAppt = New DataTable
    '        dtRxhubSettings = New DataTable
    '        dtMsgType = New DataTable

    '        'gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath = System.Windows.Forms.Application.StartupPath
    '        mdlGeneral.SetDbCredentials()
    '        Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
    '            objSendEncryption = New clsEncryption
    '            Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
    '            Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

    '            'Dim ofrmDBCredentials As New frmDBCredentials
    '            'Dim isTablePresent As Boolean = ofrmDBCredentials.checkScripts(strConnection, "Settings")
    '            'If isTablePresent = False Then
    '            '    i = i + 1
    '            '    Continue Do
    '            'End If

    '            dtRxhubSettings = GetRxHubSettings(strConnection)
    '            If Not IsNothing(dtRxhubSettings) Then


    '                If dtRxhubSettings.Rows.Count > 0 Then
    '                    If gblnGenerateRxEligibilityLog = True Then
    '                        mdlGeneral.UpdateLog("Retrieved RxHub settings on on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)
    '                    End If

    '                    gloRxHub.ClsgloRxHubGeneral.ConnectionString = strConnection
    '                    'mdlGeneral.UpdateLog("Assigned connection string " & strConnection & " to   gloRxHub.ClsgloRxHubGeneral.ConnectionString.........")

    '                    For rwcnt As Integer = 0 To dtRxhubSettings.Rows.Count - 1
    '                        If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "ADVANCE RX ENABLED" Then
    '                            sAdvanceRxEnabled = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
    '                            If sAdvanceRxEnabled <> "1" Then ''''''''''''advance Rx hub setting is OFF
    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("Invalid setting for auto eligibility since Advance Rxhub Settings is OFF on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)

    '                                    ''since the RxHub setting is off on this server and for this database then continue with the next database 
    '                                    i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                                    Continue Do
    '                                End If
    '                            End If
    '                        End If


    '                        If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "ADVANCE RX STAGING SERVER" Then
    '                            sAdvanceStagingServer = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
    '                            If sAdvanceStagingServer <> "1" Then ''''''''''''web service Pointed to Production server
    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("The service is pointed to Production server")
    '                                End If
    '                                gloRxHub.ClsgloRxHubGeneral.gblnIsRxhubStagingServer = False
    '                            Else
    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("The service is pointed to Staging server")
    '                                End If
    '                                gloRxHub.ClsgloRxHubGeneral.gblnIsRxhubStagingServer = True
    '                            End If
    '                        End If

    '                        If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "RXHUB PARTICIPANTID" Then
    '                            sRxHubParticipantId = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
    '                            If sRxHubParticipantId = "" Then ''''''''''''Participant ID
    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("Invalid setting for auto eligibility since the Participant Id is blank on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)
    '                                End If
    '                                ''since the RxHub Participant Id is blank on this server and for this database then continue with the next database 
    '                                i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                                Continue Do
    '                            End If
    '                        End If

    '                        'If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "RXHUB PASSWORD" Then
    '                        '    Dim objEncryption As New clsEncryption
    '                        '    sRxHubPassword = objEncryption.DecryptFromBase64String(dtRxhubSettings.Rows(rwcnt)("sSettingsValue"), mdlGeneral.constEncryptDecryptKeyDB)
    '                        '    If sRxHubPassword = "" Then ''''''''''''RxHubPassword
    '                        '        If gblnGenerateRxEligibilityLog = True Then
    '                        '            mdlGeneral.UpdateLog("Invalid setting for auto eligibility since the RxHub Password is blank on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)
    '                        '        End If
    '                        '        ''since the RxHub Password is blank on this server and for this database then continue with the next database 
    '                        '        i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                        '        Continue Do
    '                        '    ElseIf sRxHubPassword.Contains("Bad Data.") Then
    '                        '        If gblnGenerateRxEligibilityLog = True Then
    '                        '            mdlGeneral.UpdateLog("Invalid setting for auto eligibility since the RxHub Password contains decrypted value as " & sRxHubPassword)
    '                        '        End If
    '                        '        ''since the RxHub Password contains Bad Data. on this server and for this database then continue with the next database 
    '                        '        i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                        '        Continue Do
    '                        '    End If
    '                        'End If

    '                        If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "RXHUB PASSWORD" Then
    '                            ' Dim objEncryption As New clsEncryption
    '                            sRxHubPassword = objSendEncryption.DecryptFromBase64String(dtRxhubSettings.Rows(rwcnt)("sSettingsValue"), mdlGeneral.constEncryptDecryptKeyDB)
    '                            If sRxHubPassword = "" Then ''''''''''''RxHubPassword
    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("Invalid setting for auto eligibility since the RxHub Password is blank on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)
    '                                End If
    '                                ''since the RxHub Password is blank on this server and for this database then continue with the next database 
    '                                i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                                Continue Do
    '                            ElseIf sRxHubPassword.Contains("Bad Data.") Then
    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("Invalid setting for auto eligibility since the RxHub Password contains decrypted value as " & sRxHubPassword)
    '                                End If
    '                                ''since the RxHub Password contains Bad Data. on this server and for this database then continue with the next database 
    '                                i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                                Continue Do
    '                            End If
    '                        End If
    '                        If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "RXELIGIBILITYEMR" Then
    '                            ''''''Read the setting whether the Eligibility is to be processed BYCODE/BYSERVICE
    '                            sRXELIGIBILITYEMR = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
    '                            If IsDBNull(dtRxhubSettings.Rows(rwcnt)("sSettingsValue")) Then ''''''''''''RXELIGIBILITYEMR
    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("The RXELIGIBILITYEMR setting is blank therefore the eligibility process will be done thrrough code instead of webservice")
    '                                End If
    '                                sRXELIGIBILITYEMR = "BYCODE"
    '                            ElseIf IsNothing(dtRxhubSettings.Rows(rwcnt)("sSettingsValue")) Then
    '                                sRXELIGIBILITYEMR = "BYCODE"
    '                            ElseIf sRXELIGIBILITYEMR = "BYCODE" Then
    '                                sRXELIGIBILITYEMR = "BYCODE"
    '                            ElseIf sRXELIGIBILITYEMR = "BYSERVICE" Then
    '                                sRXELIGIBILITYEMR = "BYSERVICE"
    '                            Else
    '                                sRXELIGIBILITYEMR = "BYCODE"
    '                            End If
    '                        End If

    '                        If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "EDISERVICEPATH" Then
    '                            If IsDBNull(dtRxhubSettings.Rows(rwcnt)("sSettingsValue")) Then
    '                                sEDISERVICEPATH = ""
    '                            ElseIf IsNothing(dtRxhubSettings.Rows(rwcnt)("sSettingsValue")) Then
    '                                sEDISERVICEPATH = ""
    '                            Else
    '                                sEDISERVICEPATH = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
    '                            End If
    '                            If sEDISERVICEPATH = "" Then ''''''''''''Participant ID
    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("The EDI service url is blank")
    '                                End If
    '                            End If
    '                        End If

    '                    Next



    '                    Dim PatApptRowCount As Integer = 0
    '                    If sAdvanceRxEnabled <> "1" Then '''''if 0 means advance erx is OFF and therefore dont check the RxEligibility
    '                        If gblnGenerateRxEligibilityLog = True Then
    '                            mdlGeneral.UpdateLog("Advance Rxhub seting is OFF for server name " + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + " and database name " + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ".........")
    '                        End If

    '                    Else
    '                        PatApptRowCount = 0
    '                        If sRxHubParticipantId <> "" Or sRxHubPassword <> "" Then
    '                            dtPatAppt = GetPatientWithAppointments(strConnection)
    '                            PatApptRowCount = dtPatAppt.Rows.Count
    '                        Else
    '                            If gblnGenerateRxEligibilityLog = True Then
    '                                mdlGeneral.UpdateLog("Invalid Rxhub credentials for server name " + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + " and database name " + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ".........")
    '                            End If
    '                            PatApptRowCount = 0
    '                        End If
    '                    End If



    '                    If PatApptRowCount > 0 Then
    '                        If gblnGenerateRxEligibilityLog = True Then
    '                            mdlGeneral.UpdateLog(" " & dtPatAppt.Rows.Count.ToString & " Patients found for today's appointment.........")
    '                        End If

    '                        'Dim oClsRxHubInterface As gloRxHub.ClsRxHubInterface
    '                        'Dim oEligibilityCheck As gloRxHub.clsEligibilityCheckDBLayer


    '                        For rwCnt As Integer = 0 To dtPatAppt.Rows.Count - 1
    '                            ''Dim PatientName As String = dtPatAppt.Rows(rwCnt)("sFirstName").ToString & " " & dtPatAppt.Rows(rwCnt)("sLastName").ToString & "--" & dtPatAppt.Rows(rwCnt)("nPatientId").ToString



    '                            'Developer: Saagar K
    '                            'Date: 10 Dec 2011
    '                            'Bug ID/PRD Name/Salesforce Case:  Change requested from Drew Nolan
    '                            'Reason: mail sent from Drew Nolan with subject "Surescripts Eligibility Request Change", removed the subscriber validation functionality because we will be sending the patient information and not subscriber information
    '                            ''due to this whatever patients are having checked in appointment will be considered eligible for doing auto eligibility

    '                            ''''''Check the subscriber validation,
    '                            'Dim blnProcessFile As Boolean = True ''''''if the patient have valid subscriber information then only send the eligibility request or dont process 
    '                            'Dim dt_ins As DataTable = Nothing
    '                            'Try
    '                            'dt_ins = getPatientInsurances(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long), strConnection)

    '                            '    If Not IsNothing(dt_ins) Then
    '                            '        If dt_ins.Rows.Count > 0 Then
    '                            '            '''''''check the Subscriber insurance validation. if any of the 5 values like SubFName, SubLName,sSubscriberGender, SubscriberZip, dtDOB are blank then keep the RxElig btn Disabled
    '                            '            If dt_ins.Rows(0)("SubFName") = "" Or dt_ins.Rows(0)("SubLName") = "" Or dt_ins.Rows(0)("sSubscriberGender") = "" Or dt_ins.Rows(0)("SubscriberZip") = "" Or dt_ins.Rows(0)("dtDOB").ToString = "" Then
    '                            '                'dont process the file
    '                            '                blnProcessFile = False
    '                            '                'Exit Sub
    '                            '            Else
    '                            '                'process the file
    '                            '                blnProcessFile = True
    '                            '            End If
    '                            '        Else
    '                            '            'dont process the file
    '                            '            blnProcessFile = False
    '                            '        End If
    '                            '    Else
    '                            '        'dont process the file
    '                            '        blnProcessFile = False
    '                            '    End If

    '                            'Catch ex As Exception
    '                            '    'dont process the file
    '                            '    blnProcessFile = False
    '                            '    dt_ins = Nothing
    '                            'Finally
    '                            '    If Not IsNothing(dt_ins) Then
    '                            '        dt_ins.Dispose()
    '                            '        dt_ins = Nothing
    '                            '    End If
    '                            'End Try
    '                            ''''''Check the subscriber validation

    '                            'Developer: Saagar K
    '                            'Date: 10 Dec 2011
    '                            'Bug ID/PRD Name/Salesforce Case:  Change requested from Drew Nolan
    '                            'Reason: mail sent from Drew Nolan with subject "Surescripts Eligibility Request Change", commented the code with respect to blnProcessfile flag since we are not checking the PatientInsurance validation.
    '                            ''due to this whatever patients are having checked in appointment will be considered eligible for doing auto eligibility

    '                            'If blnProcessFile = True Then


    '                            If sRXELIGIBILITYEMR = "BYSERVICE" Then ''''use the web service to process eligibility
    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("RxEligibility will be processed through WebService........")
    '                                End If
    '                                oClsRxHubInterface = New gloRxHub.ClsRxHubInterface(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long))
    '                                oEligibilityCheck = New gloRxHub.clsEligibilityCheckDBLayer


    '                                Dim retVal As Boolean = oClsRxHubInterface.ProcessRxEligibilityUsingWebService(dtPatAppt.Rows(rwCnt)("nPatientId").ToString, strConnection, sEDISERVICEPATH)
    '                                ''''''''generate task for patient whose sMessage type in the response file is PNF/NCP/GSE/997
    '                                If retVal = True Then
    '                                    Dim sTaskDescription As String = ""
    '                                    dtMsgType = Get271ResponseMessageType(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long), strConnection)
    '                                    If dtMsgType.Rows.Count > 0 Then
    '                                        If dtMsgType.Rows(0)("sMessageType").ToString.Contains("PNF") Then
    '                                            sTaskDescription = "Patient not found"
    '                                        ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("NCP") Then
    '                                            sTaskDescription = "No Contract with payer"
    '                                        ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("GSE") Then
    '                                            sTaskDescription = "General system error"
    '                                        ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("997") Then
    '                                            sTaskDescription = "270 eligibility request file contains invalid patient Information" 'Error in 270 file")
    '                                        End If
    '                                    Else

    '                                        sTaskDescription = "" ''''''"No eligibility response file returned from surescript("")
    '                                    End If

    '                                    If sTaskDescription <> "" Then

    '                                        oEligibilityCheck.GetEligibilityCheck(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long))
    '                                        oClsRxHubInterface.Patient = oEligibilityCheck.Patient
    '                                        AddTaskFromAutoEligiblity(oClsRxHubInterface, sTaskDescription, strConnection)
    '                                        If gblnGenerateRxEligibilityLog = True Then
    '                                            mdlGeneral.UpdateLog("Generated task against patient  " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " with task type as " & sTaskDescription & " ")
    '                                        End If

    '                                    End If
    '                                Else
    '                                    If gblnGenerateRxEligibilityLog = True Then
    '                                        mdlGeneral.UpdateLog("The Web service returned false when processing eligibility information for  " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " ")
    '                                    End If
    '                                End If


    '                            Else ''''''use the code to process the eligibility file
    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("RxEligibility will be processed through Code........")
    '                                End If
    '                                'ofrmEDIGeneration = New gloRxHub.frmRxhub270EDIGeneration
    '                                'ofrmEDIGeneration.LoadEDIObject()

    '                                If gblnGenerateRxEligibilityLog = True Then
    '                                    mdlGeneral.UpdateLog("Loaded 270 SEF EDI file object for processing 270 file........")
    '                                End If


    '                                oClsRxHubInterface = New gloRxHub.ClsRxHubInterface(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long))
    '                                oEligibilityCheck = New gloRxHub.clsEligibilityCheckDBLayer

    '                                Try

    '                                    If oEligibilityCheck.IsEligibilitygGenerated(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long)) = True Then
    '                                        If gblnGenerateRxEligibilityLog = True Then
    '                                            mdlGeneral.UpdateLog("Eligibility NOT generated for today's date........")
    '                                        End If


    '                                        ''''''''first check if internet is available and then only send the eRx, if not then exit from the function
    '                                        If IsInternetConnectionAvailable() = False Then
    '                                            If gblnGenerateRxEligibilityLog = True Then
    '                                                mdlGeneral.UpdateLog("You are not connected to the internet.")
    '                                            End If
    '                                            Continue For
    '                                        End If

    '                                        oEligibilityCheck.GetEligibilityCheck(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long))

    '                                        Dim strFileName As String = ""
    '                                        If Not IsNothing(oEligibilityCheck.Patient) Then
    '                                            If gblnGenerateRxEligibilityLog = True Then
    '                                                mdlGeneral.UpdateLog("Populated patient object for patient  " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " ")
    '                                            End If


    '                                            'ofrmEDIGeneration.Patient = oEligibilityCheck.Patient

    '                                            If oClsRxHubInterface.CheckOutboxFolder(gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath) Then
    '                                                If gblnGenerateRxEligibilityLog = True Then
    '                                                    mdlGeneral.UpdateLog(" Before Generated 270 eligibility request file on" & gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath)
    '                                                End If

    '                                                Try
    '                                                    'strFileName = ofrmEDIGeneration.Generate270SingleCoveragePharmacyBenefitEDI(sRxHubParticipantId, sRxHubPassword)
    '                                                Catch ex As Exception
    '                                                    mdlGeneral.UpdateLog(ex.ToString())
    '                                                End Try

    '                                                If gblnGenerateRxEligibilityLog = True Then
    '                                                    mdlGeneral.UpdateLog("Generated 270 eligibility request file for patient " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " at path " & strFileName.ToString & "")
    '                                                End If

    '                                                ''post EDI
    '                                                ofrmPostSample = New gloRxHub.frmSamplePost
    '                                                Try
    '                                                    If strFileName <> "" Then
    '                                                        ofrmPostSample.strFileName = strFileName
    '                                                        ofrmPostSample.Button3_Click(Nothing, Nothing)

    '                                                        Dim gnInsuranceId As Long = 0
    '                                                        Dim EDI271FilePath As String = ofrmPostSample.strOutputFile

    '                                                        If gblnGenerateRxEligibilityLog = True Then
    '                                                            mdlGeneral.UpdateLog("Posted and received 271 eligibility response file for patient  " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " at path " & EDI271FilePath.ToString & "")
    '                                                            mdlGeneral.UpdateLog("---------------------------------------------------------------------------------------------------------------------------")
    '                                                        End If

    '                                                        ofrmPostSample.Dispose()
    '                                                        'Dim ofrmEligibilityResponse_new As gloRxHub.frmEligiblityResponse_new
    '                                                        Try

    '                                                            'FillEligibilityResponse(EDI271FilePath, ofrmEDIGeneration.Patient)

    '                                                            oClsRxHubInterface.Patient = oEligibilityCheck.Patient
    '                                                            oClsRxH_271Master = New gloRxHub.ClsRxH_271Master()
    '                                                            oCls271Information = New gloRxHub.Cls271Information()
    '                                                            If EDI271FilePath <> "" Then
    '                                                                ''in case of NAK first check the file is NAK otherwise it will not be loaded in EDI framework
    '                                                                If oClsRxHubInterface.LoadEDIObject_271(EDI271FilePath) = False Then
    '                                                                    'means file is not valid EDI file and is not able to get loaded in the EDI doc object.
    '                                                                    'therefore this must be the NAK file
    '                                                                    'mdlGeneral.UpdateLog("NAK : TRANSACTION CANNOT BE IDENTIFIED NOR PROCESSED")
    '                                                                    mdlGeneral.UpdateLog("Unable to load the 271 SEF file. NAK : TRANSACTION CANNOT BE IDENTIFIED NOR PROCESSED")
    '                                                                    'Exit Sub
    '                                                                Else
    '                                                                    '//file is valid edi file and has got loaded in the ediDOC object
    '                                                                End If
    '                                                                oClsRxHubInterface.Translate271Response_AutoEligibility()

    '                                                                If gblnGenerateRxEligibilityLog = True Then
    '                                                                    mdlGeneral.UpdateLog("Updated the records against patient " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " in database")
    '                                                                End If


    '                                                            End If


    '                                                            ''''''''generate task for patient whose sMessage type in the response file is PNF/NCP/GSE/997

    '                                                            Dim sTaskDescription As String = ""
    '                                                            dtMsgType = Get271ResponseMessageType(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long), strConnection)
    '                                                            If dtMsgType.Rows.Count > 0 Then
    '                                                                If dtMsgType.Rows(0)("sMessageType").ToString.Contains("PNF") Then
    '                                                                    sTaskDescription = "Patient not found"
    '                                                                ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("NCP") Then
    '                                                                    sTaskDescription = "No Contract with payer"
    '                                                                ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("GSE") Then
    '                                                                    sTaskDescription = "General system error"
    '                                                                ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("997") Then
    '                                                                    sTaskDescription = "270 eligibility request file contains invalid patient information" 'Error in 270 file
    '                                                                End If
    '                                                            Else
    '                                                                If EDI271FilePath = "" Then ''''ckeck the condition whether if the 271 file was not sent by surescript and if not sent then create a task with following message
    '                                                                    sTaskDescription = "No eligibility response file returned from surescript"
    '                                                                End If
    '                                                            End If

    '                                                            If sTaskDescription <> "" Then
    '                                                                AddTaskFromAutoEligiblity(oClsRxHubInterface, sTaskDescription, strConnection)
    '                                                                If gblnGenerateRxEligibilityLog = True Then
    '                                                                    mdlGeneral.UpdateLog("Generated task against patient  " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " with task type as " & sTaskDescription & " ")
    '                                                                End If

    '                                                            End If


    '                                                            oClsRxH_271Master.Dispose()
    '                                                            oCls271Information.Dispose()
    '                                                        Catch ex As Exception
    '                                                            mdlGeneral.UpdateLog("Error :" & ex.ToString)
    '                                                        Finally
    '                                                            If Not IsNothing(oClsRxH_271Master) Then
    '                                                                oClsRxH_271Master.Dispose()
    '                                                                oClsRxH_271Master = Nothing
    '                                                            End If
    '                                                            If Not IsNothing(oCls271Information) Then
    '                                                                oCls271Information.Dispose()
    '                                                                oCls271Information = Nothing
    '                                                            End If
    '                                                        End Try
    '                                                    End If

    '                                                Catch ex As Exception
    '                                                    mdlGeneral.UpdateLog("Error :" & ex.ToString)
    '                                                Finally

    '                                                    If Not IsNothing(ofrmPostSample) Then
    '                                                        ofrmPostSample.Dispose()
    '                                                        ofrmPostSample = Nothing
    '                                                    End If

    '                                                End Try



    '                                            Else
    '                                                If gblnGenerateRxEligibilityLog = True Then
    '                                                    mdlGeneral.UpdateLog("The 270 EDI request cannot be generated since there is no outbox folder.")
    '                                                End If

    '                                            End If
    '                                        End If
    '                                    Else
    '                                        If gblnGenerateRxEligibilityLog = True Then
    '                                            mdlGeneral.UpdateLog("Eligibility request already processed for patient " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " for today.")
    '                                        End If

    '                                    End If
    '                                Catch ex As Exception
    '                                    mdlGeneral.UpdateLog("Error :" & ex.ToString)
    '                                Finally
    '                                    'If Not IsNothing(ofrmEDIGeneration) Then
    '                                    '    ofrmEDIGeneration.Dispose()
    '                                    '    ofrmEDIGeneration = Nothing
    '                                    'End If
    '                                    If Not IsNothing(oClsRxHubInterface) Then
    '                                        oClsRxHubInterface.Dispose()
    '                                        oClsRxHubInterface = Nothing
    '                                    End If
    '                                    If Not IsNothing(oEligibilityCheck) Then
    '                                        oEligibilityCheck.Dispose()
    '                                        oEligibilityCheck = Nothing
    '                                    End If

    '                                End Try


    '                            End If ''''''''if EDISERVICE



    '                            'Else
    '                            ''If gblnGenerateRxEligibilityLog = True Then
    '                            ''    mdlGeneral.UpdateLog("Eligiblity file will not be sent for patient " & PatientName & " due to incomplete subscriber information.")
    '                            ''    mdlGeneral.UpdateLog("-------------------------------------------------------------------------------------------------------------------")
    '                            ''End If

    '                            'blnProcessFile = True
    '                            'End If '''''''''''end blnProcessfile
    '                        Next

    '                    End If
    '                Else
    '                    If gblnGenerateRxEligibilityLog = True Then
    '                        mdlGeneral.UpdateLog("No RxHub settings found for server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)
    '                    End If


    '                End If '''''dtRxHubSettings.Rows.Count > 0 

    '            End If '''''''if not isnothing(dtRxHubSettings)

    '            i = (i + 1)
    '        Loop
    '        i = 0

    '    Catch ex As Exception
    '        mdlGeneral.UpdateLog("Error :" & ex.ToString)
    '    Finally
    '        If Not IsNothing(dtPatAppt) Then
    '            dtPatAppt.Dispose()
    '            dtPatAppt = Nothing
    '        End If

    '        If Not IsNothing(dtRxhubSettings) Then
    '            dtRxhubSettings.Dispose()
    '            dtRxhubSettings = Nothing
    '        End If

    '        If Not IsNothing(dtMsgType) Then
    '            dtMsgType.Dispose()
    '            dtMsgType = Nothing
    '        End If
    '    End Try


    'End Sub

    Public Sub ProcessAutoEligiblity()
        Dim i As Integer = 0 ''''this var is for database counter
        Dim dtPatAppt As DataTable = Nothing
        Dim dtRxhubSettings As DataTable = Nothing
        Dim dtMsgType As DataTable = Nothing
        'Dim ofrmEDIGeneration As gloRxHub.frmRxhub270EDIGeneration = Nothing

        Dim sAdvanceRxEnabled As String = ""
        Dim sAdvanceStagingServer As String = ""
        Dim sRxHubParticipantId As String = ""
        Dim sRxHubPassword As String = ""
        Dim sRXELIGIBILITYEMR As String = "BYCODE" '''''byDefault set to BYCODE, if the setting is BYSERVICE then call the code wrt to WEBService 
        Dim sEDISERVICEPATH As String = "" '''''set this url to the Rxeligibility web service object

        Dim objSendEncryption As clsEncryption = Nothing

        'Dim oClsRxH_271Master As gloRxHub.ClsRxH_271Master = Nothing
        'Dim oCls271Information As gloRxHub.Cls271Information = Nothing
        'Dim ofrmPostSample As gloRxHub.frmSamplePost = Nothing
        Dim oClsRxHubInterface As gloRxHub.ClsRxHubInterface = Nothing
        'Dim oEligibilityCheck As gloRxHub.clsEligibilityCheckDBLayer = Nothing

        Try
            'dtPatAppt = New DataTable
            'dtRxhubSettings = New DataTable
            'dtMsgType = New DataTable

            'gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath = System.Windows.Forms.Application.StartupPath
            mdlGeneral.SetDbCredentials()
            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
                objSendEncryption = New clsEncryption
                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

                'Dim ofrmDBCredentials As New frmDBCredentials
                'Dim isTablePresent As Boolean = ofrmDBCredentials.checkScripts(strConnection, "Settings")
                'If isTablePresent = False Then
                '    i = i + 1
                '    Continue Do
                'End If

                dtRxhubSettings = GetRxHubSettings(strConnection)
                If Not IsNothing(dtRxhubSettings) Then


                    If dtRxhubSettings.Rows.Count > 0 Then
                        If gblnGenerateRxEligibilityLog = True Then
                            mdlGeneral.UpdateLog("Retrieved RxHub settings on on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString, strConnection)
                        End If

                        gloRxHub.ClsgloRxHubGeneral.ConnectionString = strConnection
                        'mdlGeneral.UpdateLog("Assigned connection string " & strConnection & " to   gloRxHub.ClsgloRxHubGeneral.ConnectionString.........")

                        For rwcnt As Integer = 0 To dtRxhubSettings.Rows.Count - 1
                            If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "ADVANCE RX ENABLED" Then
                                sAdvanceRxEnabled = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
                                If sAdvanceRxEnabled <> "1" Then ''''''''''''advance Rx hub setting is OFF
                                    If gblnGenerateRxEligibilityLog = True Then
                                        mdlGeneral.UpdateLog("Invalid setting for auto eligibility since Advance Rxhub Settings is OFF on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString, strConnection)

                                        ''since the RxHub setting is off on this server and for this database then continue with the next database 
                                        i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
                                        Continue Do
                                    End If
                                End If
                            End If


                            If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "ADVANCE RX STAGING SERVER" Then
                                sAdvanceStagingServer = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
                                If sAdvanceStagingServer <> "1" Then ''''''''''''web service Pointed to Production server
                                    If gblnGenerateRxEligibilityLog = True Then
                                        mdlGeneral.UpdateLog("The service is pointed to Production server", strConnection)
                                    End If
                                    gloRxHub.ClsgloRxHubGeneral.gblnIsRxhubStagingServer = False
                                Else
                                    If gblnGenerateRxEligibilityLog = True Then
                                        mdlGeneral.UpdateLog("The service is pointed to Staging server", strConnection)
                                    End If
                                    gloRxHub.ClsgloRxHubGeneral.gblnIsRxhubStagingServer = True
                                End If
                            End If

                            If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "RXHUB PARTICIPANTID" Then
                                sRxHubParticipantId = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
                                If sRxHubParticipantId = "" Then ''''''''''''Participant ID
                                    If gblnGenerateRxEligibilityLog = True Then
                                        mdlGeneral.UpdateLog("Invalid setting for auto eligibility since the Participant Id is blank on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString, strConnection)
                                    End If
                                    ''since the RxHub Participant Id is blank on this server and for this database then continue with the next database 
                                    i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
                                    Continue Do
                                End If
                            End If

                            'If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "RXHUB PASSWORD" Then
                            '    Dim objEncryption As New clsEncryption
                            '    sRxHubPassword = objEncryption.DecryptFromBase64String(dtRxhubSettings.Rows(rwcnt)("sSettingsValue"), mdlGeneral.constEncryptDecryptKeyDB)
                            '    If sRxHubPassword = "" Then ''''''''''''RxHubPassword
                            '        If gblnGenerateRxEligibilityLog = True Then
                            '            mdlGeneral.UpdateLog("Invalid setting for auto eligibility since the RxHub Password is blank on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)
                            '        End If
                            '        ''since the RxHub Password is blank on this server and for this database then continue with the next database 
                            '        i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
                            '        Continue Do
                            '    ElseIf sRxHubPassword.Contains("Bad Data.") Then
                            '        If gblnGenerateRxEligibilityLog = True Then
                            '            mdlGeneral.UpdateLog("Invalid setting for auto eligibility since the RxHub Password contains decrypted value as " & sRxHubPassword)
                            '        End If
                            '        ''since the RxHub Password contains Bad Data. on this server and for this database then continue with the next database 
                            '        i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
                            '        Continue Do
                            '    End If
                            'End If

                            If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "RXHUB PASSWORD" Then
                                ' Dim objEncryption As New clsEncryption
                                sRxHubPassword = objSendEncryption.DecryptFromBase64String(dtRxhubSettings.Rows(rwcnt)("sSettingsValue"), mdlGeneral.constEncryptDecryptKeyDB)
                                If sRxHubPassword = "" Then ''''''''''''RxHubPassword
                                    If gblnGenerateRxEligibilityLog = True Then
                                        mdlGeneral.UpdateLog("Invalid setting for auto eligibility since the RxHub Password is blank on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString, strConnection)
                                    End If
                                    ''since the RxHub Password is blank on this server and for this database then continue with the next database 
                                    i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
                                    Continue Do
                                ElseIf sRxHubPassword.Contains("Bad Data.") Then
                                    If gblnGenerateRxEligibilityLog = True Then
                                        mdlGeneral.UpdateLog("Invalid setting for auto eligibility since the RxHub Password contains decrypted value as " & sRxHubPassword, strConnection)
                                    End If
                                    ''since the RxHub Password contains Bad Data. on this server and for this database then continue with the next database 
                                    i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
                                    Continue Do
                                End If
                            End If
                            If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "RXELIGIBILITYEMR" Then
                                ''''''Read the setting whether the Eligibility is to be processed BYCODE/BYSERVICE
                                sRXELIGIBILITYEMR = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
                                If IsDBNull(dtRxhubSettings.Rows(rwcnt)("sSettingsValue")) Then ''''''''''''RXELIGIBILITYEMR
                                    If gblnGenerateRxEligibilityLog = True Then
                                        mdlGeneral.UpdateLog("The RXELIGIBILITYEMR setting is blank therefore the eligibility process will be done thrrough code instead of webservice", strConnection)
                                    End If
                                    sRXELIGIBILITYEMR = "BYCODE"
                                ElseIf IsNothing(dtRxhubSettings.Rows(rwcnt)("sSettingsValue")) Then
                                    sRXELIGIBILITYEMR = "BYCODE"
                                ElseIf sRXELIGIBILITYEMR = "BYCODE" Then
                                    sRXELIGIBILITYEMR = "BYCODE"
                                ElseIf sRXELIGIBILITYEMR = "BYSERVICE" Then
                                    sRXELIGIBILITYEMR = "BYSERVICE"
                                Else
                                    sRXELIGIBILITYEMR = "BYCODE"
                                End If
                            End If

                            If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "EDISERVICEPATH" Then
                                If IsDBNull(dtRxhubSettings.Rows(rwcnt)("sSettingsValue")) Then
                                    sEDISERVICEPATH = ""
                                ElseIf IsNothing(dtRxhubSettings.Rows(rwcnt)("sSettingsValue")) Then
                                    sEDISERVICEPATH = ""
                                Else
                                    sEDISERVICEPATH = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
                                End If
                                If sEDISERVICEPATH = "" Then ''''''''''''Participant ID
                                    If gblnGenerateRxEligibilityLog = True Then
                                        mdlGeneral.UpdateLog("The EDI service url is blank", strConnection)
                                    End If
                                End If
                            End If

                        Next



                        Dim PatApptRowCount As Integer = 0
                        If sAdvanceRxEnabled <> "1" Then '''''if 0 means advance erx is OFF and therefore dont check the RxEligibility
                            If gblnGenerateRxEligibilityLog = True Then
                                mdlGeneral.UpdateLog("Advance Rxhub seting is OFF for server name " + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + " and database name " + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ".........", strConnection)
                            End If

                        Else
                            PatApptRowCount = 0
                            If sRxHubParticipantId <> "" Or sRxHubPassword <> "" Then
                                dtPatAppt = GetPatientWithAppointments(strConnection)
                                PatApptRowCount = dtPatAppt.Rows.Count
                            Else
                                If gblnGenerateRxEligibilityLog = True Then
                                    mdlGeneral.UpdateLog("Invalid Rxhub credentials for server name " + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + " and database name " + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ".........", strConnection)
                                End If
                                PatApptRowCount = 0
                            End If
                        End If



                        If PatApptRowCount > 0 Then
                            If gblnGenerateRxEligibilityLog = True Then
                                mdlGeneral.UpdateLog(" " & dtPatAppt.Rows.Count.ToString & " Patients found for today's appointment.........", strConnection)
                            End If

                            'Dim oClsRxHubInterface As gloRxHub.ClsRxHubInterface
                            'Dim oEligibilityCheck As gloRxHub.clsEligibilityCheckDBLayer


                            For rwCnt As Integer = 0 To dtPatAppt.Rows.Count - 1
                                ''Dim PatientName As String = dtPatAppt.Rows(rwCnt)("sFirstName").ToString & " " & dtPatAppt.Rows(rwCnt)("sLastName").ToString & "--" & dtPatAppt.Rows(rwCnt)("nPatientId").ToString



                                'Developer: Saagar K
                                'Date: 10 Dec 2011
                                'Bug ID/PRD Name/Salesforce Case:  Change requested from Drew Nolan
                                'Reason: mail sent from Drew Nolan with subject "Surescripts Eligibility Request Change", removed the subscriber validation functionality because we will be sending the patient information and not subscriber information
                                ''due to this whatever patients are having checked in appointment will be considered eligible for doing auto eligibility

                                ''''''Check the subscriber validation,
                                'Dim blnProcessFile As Boolean = True ''''''if the patient have valid subscriber information then only send the eligibility request or dont process 
                                'Dim dt_ins As DataTable = Nothing
                                'Try
                                'dt_ins = getPatientInsurances(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long), strConnection)

                                '    If Not IsNothing(dt_ins) Then
                                '        If dt_ins.Rows.Count > 0 Then
                                '            '''''''check the Subscriber insurance validation. if any of the 5 values like SubFName, SubLName,sSubscriberGender, SubscriberZip, dtDOB are blank then keep the RxElig btn Disabled
                                '            If dt_ins.Rows(0)("SubFName") = "" Or dt_ins.Rows(0)("SubLName") = "" Or dt_ins.Rows(0)("sSubscriberGender") = "" Or dt_ins.Rows(0)("SubscriberZip") = "" Or dt_ins.Rows(0)("dtDOB").ToString = "" Then
                                '                'dont process the file
                                '                blnProcessFile = False
                                '                'Exit Sub
                                '            Else
                                '                'process the file
                                '                blnProcessFile = True
                                '            End If
                                '        Else
                                '            'dont process the file
                                '            blnProcessFile = False
                                '        End If
                                '    Else
                                '        'dont process the file
                                '        blnProcessFile = False
                                '    End If

                                'Catch ex As Exception
                                '    'dont process the file
                                '    blnProcessFile = False
                                '    dt_ins = Nothing
                                'Finally
                                '    If Not IsNothing(dt_ins) Then
                                '        dt_ins.Dispose()
                                '        dt_ins = Nothing
                                '    End If
                                'End Try
                                ''''''Check the subscriber validation

                                'Developer: Saagar K
                                'Date: 10 Dec 2011
                                'Bug ID/PRD Name/Salesforce Case:  Change requested from Drew Nolan
                                'Reason: mail sent from Drew Nolan with subject "Surescripts Eligibility Request Change", commented the code with respect to blnProcessfile flag since we are not checking the PatientInsurance validation.
                                ''due to this whatever patients are having checked in appointment will be considered eligible for doing auto eligibility

                                'If blnProcessFile = True Then


                                If sRXELIGIBILITYEMR = "BYSERVICE" Then ''''use the web service to process eligibility
                                    If gblnGenerateRxEligibilityLog = True Then
                                        mdlGeneral.UpdateLog("RxEligibility will be processed through WebService........", strConnection)
                                    End If
                                    oClsRxHubInterface = New gloRxHub.ClsRxHubInterface(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long))
                                    'oEligibilityCheck = New gloRxHub.clsEligibilityCheckDBLayer


                                    Dim retVal As Boolean = oClsRxHubInterface.ProcessRxEligibilityUsingWebService(dtPatAppt.Rows(rwCnt)("nPatientId").ToString, strConnection, sEDISERVICEPATH)
                                    ''''''''generate task for patient whose sMessage type in the response file is PNF/NCP/GSE/997
                                    If retVal = True Then
                                        Dim sTaskDescription As String = ""
                                        dtMsgType = Get271ResponseMessageType(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long), strConnection)
                                        If dtMsgType.Rows.Count > 0 Then
                                            If dtMsgType.Rows(0)("sMessageType").ToString.Contains("PNF") Then
                                                sTaskDescription = "Patient not found"
                                            ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("NCP") Then
                                                sTaskDescription = "No Contract with payer"
                                            ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("GSE") Then
                                                sTaskDescription = "General system error"
                                            ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("997") Then
                                                sTaskDescription = "270 eligibility request file contains invalid patient Information" 'Error in 270 file")
                                            End If
                                        Else

                                            sTaskDescription = "" ''''''"No eligibility response file returned from surescript("")
                                        End If

                                        If sTaskDescription <> "" Then
                                            'oEligibilityCheck.GetEligibilityCheck(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long))
                                            'oClsRxHubInterface.Patient = oEligibilityCheck.Patient
                                            AddTaskFromAutoEligiblity(oClsRxHubInterface, sTaskDescription, strConnection)
                                            If gblnGenerateRxEligibilityLog = True Then
                                                mdlGeneral.UpdateLog("Generated task against patient  " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " with task type as " & sTaskDescription & " ", strConnection)
                                            End If

                                        End If
                                    Else
                                        If gblnGenerateRxEligibilityLog = True Then
                                            mdlGeneral.UpdateLog("The Web service returned false when processing eligibility information for  " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " ", strConnection)
                                        End If
                                    End If


                                Else ''''''use the code to process the eligibility file
                                    If gblnGenerateRxEligibilityLog = True Then
                                        mdlGeneral.UpdateLog("RxEligibility will be processed through Code........", strConnection)
                                    End If
                                    'ofrmEDIGeneration = New gloRxHub.frmRxhub270EDIGeneration
                                    'ofrmEDIGeneration.LoadEDIObject()

                                    'If gblnGenerateRxEligibilityLog = True Then
                                    '    mdlGeneral.UpdateLog("Loaded 270 SEF EDI file object for processing 270 file........")
                                    'End If
                                    oClsRxHubInterface = New gloRxHub.ClsRxHubInterface(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long))

                                    'oEligibilityCheck = New gloRxHub.clsEligibilityCheckDBLayer

                                    Try

                                        If oClsRxHubInterface.IsEligibilitygGenerated_validation(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long), "3") = True Then
                                            If gblnGenerateRxEligibilityLog = True Then
                                                mdlGeneral.UpdateLog("Eligibility NOT generated for today's date........", strConnection)
                                            End If

                                            ''''''''first check if internet is available and then only send the eRx, if not then exit from the function
                                            If IsInternetConnectionAvailable() = False Then
                                                If gblnGenerateRxEligibilityLog = True Then
                                                    mdlGeneral.UpdateLog("You are not connected to the internet.", strConnection)
                                                End If
                                                Continue For
                                            End If

                                            'oEligibilityCheck.GetEligibilityCheck(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long))

                                            Dim strFileName As String = ""

                                            'ofrmEDIGeneration.Patient = oEligibilityCheck.Patient

                                            If oClsRxHubInterface.CheckOutboxFolder(gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath) Then
                                                If gblnGenerateRxEligibilityLog = True Then
                                                    mdlGeneral.UpdateLog(" Before Generated 270 eligibility request file on" & gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath, strConnection)
                                                End If

                                                Try
                                                    If gblnGenerateRxEligibilityLog = True Then
                                                        'strFileName = ofrmEDIGeneration.Generate270SingleCoveragePharmacyBenefitEDI(sRxHubParticipantId, sRxHubPassword)
                                                        If oClsRxHubInterface.FillPatientInfo_270(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long), 0) = False Then
                                                            mdlGeneral.UpdateLog("Error in populating patient object of  " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " at path " & strFileName.ToString & "", strConnection)
                                                            GoTo labelnxt
                                                        Else
                                                            mdlGeneral.UpdateLog("Filled the Patient Object" & dtPatAppt.Rows(rwCnt)("nPatientId").ToString, strConnection)
                                                        End If

                                                        strFileName = oClsRxHubInterface.Generate270_5010(sRxHubParticipantId, sRxHubPassword)
                                                    Else
                                                        mdlGeneral.UpdateLog("Eligibilty already generated", strConnection)
                                                    End If
                                                Catch ex As Exception
                                                    mdlGeneral.UpdateLog(ex.ToString(), strConnection)
                                                End Try

                                                If gblnGenerateRxEligibilityLog = True Then
                                                    mdlGeneral.UpdateLog("Generated 270 eligibility request file for patient " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " at path " & strFileName.ToString & "", strConnection)
                                                End If

                                                ''post EDI
                                                'ofrmPostSample = New gloRxHub.frmSamplePost
                                                Try
                                                    If strFileName <> "" Then
                                                        'ofrmPostSample.strFileName = strFileName
                                                        'ofrmPostSample.Button3_Click(Nothing, Nothing)

                                                        Dim gnInsuranceId As Long = 0
                                                        Dim EDI271FilePath As String = ""

                                                        'ofrmPostSample.Dispose()
                                                        'Dim ofrmEligibilityResponse_new As gloRxHub.frmEligiblityResponse_new
                                                        Try

                                                            'FillEligibilityResponse(EDI271FilePath, ofrmEDIGeneration.Patient)
                                                            For postCnt As Integer = 0 To 4
                                                                EDI271FilePath = oClsRxHubInterface.PostEDIFile_5010(strFileName, "EDI270")
                                                                If EDI271FilePath <> "" Then
                                                                    If gblnGenerateRxEligibilityLog = True Then
                                                                        mdlGeneral.UpdateLog("Posted and received 271 eligibility response file for patient  " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " at path " & EDI271FilePath.ToString & "", strConnection)
                                                                        mdlGeneral.UpdateLog("---------------------------------------------------------------------------------------------------------------------------", strConnection)
                                                                    End If
                                                                    Exit For
                                                                Else
                                                                    Continue For
                                                                End If
                                                            Next

                                                            'oClsRxHubInterface.Patient = oEligibilityCheck.Patient
                                                            'oClsRxH_271Master = New gloRxHub.ClsRxH_271Master()
                                                            'oCls271Information = New gloRxHub.Cls271Information()
                                                            If EDI271FilePath <> "" Then
                                                                ''in case of NAK first check the file is NAK otherwise it will not be loaded in EDI framework
                                                                If oClsRxHubInterface.LoadEDIObject_271_5010(EDI271FilePath) = False Then
                                                                    'means file is not valid EDI file and is not able to get loaded in the EDI doc object.
                                                                    'therefore this must be the NAK file
                                                                    'mdlGeneral.UpdateLog("NAK : TRANSACTION CANNOT BE IDENTIFIED NOR PROCESSED")
                                                                    mdlGeneral.UpdateLog("Unable to load the 271 SEF file. NAK : TRANSACTION CANNOT BE IDENTIFIED NOR PROCESSED", strConnection)
                                                                    'Exit Sub
                                                                Else
                                                                    '//file is valid edi file and has got loaded in the ediDOC object
                                                                End If

                                                                oClsRxHubInterface.Read271Response_5010()

                                                                If gblnGenerateRxEligibilityLog = True Then
                                                                    mdlGeneral.UpdateLog("Updated the records against patient " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " in database", strConnection)
                                                                End If

                                                                '' delete 270 & 271 file
                                                                mdlGeneral.DeleteTempFileAutoEligibility(strFileName, EDI271FilePath)

                                                            End If


                                                            ''''''''generate task for patient whose sMessage type in the response file is PNF/NCP/GSE/997

                                                            Dim sTaskDescription As String = ""
                                                            dtMsgType = Get271ResponseMessageType(CType(dtPatAppt.Rows(rwCnt)("nPatientId"), Long), strConnection)
                                                            If dtMsgType.Rows.Count > 0 Then
                                                                If dtMsgType.Rows(0)("sMessageType").ToString.Contains("PNF") Then
                                                                    sTaskDescription = "Patient not found"
                                                                ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("NCP") Then
                                                                    sTaskDescription = "No Contract with payer"
                                                                ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("GSE") Then
                                                                    sTaskDescription = "General system error"
                                                                ElseIf dtMsgType.Rows(0)("sMessageType").ToString.Contains("997") Then
                                                                    sTaskDescription = "270 eligibility request file contains invalid patient information" 'Error in 270 file
                                                                End If
                                                            Else
                                                                If EDI271FilePath = "" Then ''''ckeck the condition whether if the 271 file was not sent by surescript and if not sent then create a task with following message
                                                                    sTaskDescription = "No eligibility response file returned from surescript"
                                                                End If
                                                            End If

                                                            If sTaskDescription <> "" Then
                                                                AddTaskFromAutoEligiblity(oClsRxHubInterface, sTaskDescription, strConnection)
                                                                If gblnGenerateRxEligibilityLog = True Then
                                                                    mdlGeneral.UpdateLog("Generated task against patient  " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " with task type as " & sTaskDescription & " ", strConnection)
                                                                End If

                                                            End If


                                                            'oClsRxH_271Master.Dispose()
                                                            'oCls271Information.Dispose()
                                                        Catch ex As Exception
                                                            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
                                                        Finally
                                                            'If Not IsNothing(oClsRxH_271Master) Then
                                                            '    oClsRxH_271Master.Dispose()
                                                            '    oClsRxH_271Master = Nothing
                                                            'End If
                                                            'If Not IsNothing(oCls271Information) Then
                                                            '    oCls271Information.Dispose()
                                                            '    oCls271Information = Nothing
                                                            'End If
                                                        End Try
                                                    End If
                                                Catch ex As Exception
                                                    mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
                                                Finally
                                                    'If Not IsNothing(ofrmPostSample) Then
                                                    '    ofrmPostSample.Dispose()
                                                    '    ofrmPostSample = Nothing
                                                    'End If

                                                End Try
                                            Else
                                                If gblnGenerateRxEligibilityLog = True Then
                                                    mdlGeneral.UpdateLog("The 270 EDI request cannot be generated since there is no outbox folder.", strConnection)
                                                End If

                                            End If
                                            'End If
                                        Else
                                            If gblnGenerateRxEligibilityLog = True Then
                                                mdlGeneral.UpdateLog("Eligibility request already processed for patient " & dtPatAppt.Rows(rwCnt)("nPatientId").ToString & " for today.", strConnection)
                                            End If
                                        End If
                                    Catch ex As Exception
                                        mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
                                    Finally
                                        'If Not IsNothing(ofrmEDIGeneration) Then
                                        '    ofrmEDIGeneration.Dispose()
                                        '    ofrmEDIGeneration = Nothing
                                        'End If
                                        If Not IsNothing(oClsRxHubInterface) Then
                                            oClsRxHubInterface.Dispose()
                                            oClsRxHubInterface = Nothing
                                        End If
                                        If Not IsNothing(dtMsgType) Then
                                            dtMsgType.Dispose()
                                            dtMsgType = Nothing
                                        End If
                                        'If Not IsNothing(oEligibilityCheck) Then
                                        '    oEligibilityCheck.Dispose()
                                        '    oEligibilityCheck = Nothing
                                        'End If

                                    End Try


                                End If ''''''''if EDISERVICE



                                'Else
                                ''If gblnGenerateRxEligibilityLog = True Then
                                ''    mdlGeneral.UpdateLog("Eligiblity file will not be sent for patient " & PatientName & " due to incomplete subscriber information.")
                                ''    mdlGeneral.UpdateLog("-------------------------------------------------------------------------------------------------------------------")
                                ''End If

                                'blnProcessFile = True
                                'End If '''''''''''end blnProcessfile
labelnxt:                   Next

                        End If
                    Else
                        If gblnGenerateRxEligibilityLog = True Then
                            mdlGeneral.UpdateLog("No RxHub settings found for server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString, strConnection)
                        End If

                    End If '''''dtRxHubSettings.Rows.Count > 0 
                    If Not IsNothing(dtRxhubSettings) Then
                        dtRxhubSettings.Dispose()
                        dtRxhubSettings = Nothing
                    End If
                End If '''''''if not isnothing(dtRxHubSettings)

                i = (i + 1)
            Loop
            i = 0

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
        Finally
            If Not IsNothing(dtPatAppt) Then
                dtPatAppt.Dispose()
                dtPatAppt = Nothing
            End If

            If Not IsNothing(dtRxhubSettings) Then
                dtRxhubSettings.Dispose()
                dtRxhubSettings = Nothing
            End If

            If Not IsNothing(dtMsgType) Then
                dtMsgType.Dispose()
                dtMsgType = Nothing
            End If
        End Try


    End Sub




    Private Function Get271ResponseMessageType(ByVal PatientId As Long, ByVal strConnection As String) As DataTable
        objCon = New SqlConnection(strConnection)
        Dim objDataAdapter As SqlDataAdapter = Nothing
        Dim dtPatMessageType = New DataTable
        Dim strSql As String = ""
        Try
            strSql = "select * from RxH_271Response_Details where  ISNULL(CONVERT(VARCHAR(10), CONVERT(DATETIME,dt270ResponseDateTimeStamp) , 112),'-') =  ISNULL(CONVERT(VARCHAR(10), CONVERT(DATETIME,'" & Now.Date & "') , 112),'-')  AND SUBSTRING(sMessageType,4,1) = '|' and npatientid = " & PatientId & " "
            objDataAdapter = New SqlDataAdapter(strSql, objCon)
            'objCon.Open()
            objDataAdapter.Fill(dtPatMessageType)
            Return dtPatMessageType

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return Nothing
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objDataAdapter) Then
                objDataAdapter.Dispose()
                objDataAdapter = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Method to get Insurance Information of Particular Patient.
    ''' </summary>
    ''' <param name="PatientID">ID of Patient of whose Insurance Info is to be fetched are to be fetched.</param>
    ''' <returns></returns>
    Public Function getPatientInsurances(ByVal PatientID As Int64, ByVal strConnection As String) As DataTable
        objCon = New SqlConnection(strConnection)
        Dim objDataAdapter As SqlDataAdapter = Nothing
        Dim dtInsurance = New DataTable
        Dim _strQuery As String = ""
        Try


            _strQuery = " SELECT PatientInsurance_DTL.nInsuranceID, " _
                                    & " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " _
                                    & " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " _
                                    & " ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) +  " _
                                    & " ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)   +  " _
                                    & " ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName,  " _
                                    & " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " _
                                    & " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " _
                                    & " PatientInsurance_DTL.sPhone, " _
                                    & " PatientInsurance_DTL.dtDOB,  " _
                                    & " PatientInsurance_DTL.dtEffectiveDate,  " _
                                    & " PatientInsurance_DTL.dtExpiryDate,  " _
                                    & " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " _
                                    & " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " _
                                    & " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " _
                                    & " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " _
                                    & " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " _
                                    & " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " _
                                    & " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " _
                                    & " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " _
                                    & " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " _
                                    & " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " _
                                    & " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " _
                                    & " PatientInsurance_DTL.sSubscriberGender,  " _
                                    & " PatientInsurance_DTL.sPayerID,  " _
                                    & " ISNULL(Patient.sCity, '') AS sCity, " _
                                    & " ISNULL(Patient.sState, '') AS sState,  " _
                                    & " ISNULL(Patient.sZIP, '') AS sZIP,   " _
                                    & " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " _
                                    & " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " _
                                    & " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " _
                                    & " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " _
                                    & " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " _
                                    & " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " _
                                    & " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " _
                                    & " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " _
                                    & " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " _
                                    & " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " _
                                    & " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " _
                                    & " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " _
                                    & " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " _
                                    & " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " _
                                    & " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " _
                                    & " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " _
                                    & " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " _
                                    & " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " _
                                    & " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " _
                                    & " ELSE '' END  AS sInsuranceFlag,  " _
                                    & " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " _
                                    & " WHEN 0 THEN 4 " _
                                    & " ELSE nInsuranceFlag END  AS SortOrder,  " _
                                    & " PatientInsurance_DTL.sInsurancePhone, " _
                                    & " ISNULL(PatientInsurance_DTL.bworkerscomp,0) AS bWorkersComp, ISNULL(PatientInsurance_DTL.bautoclaim,0) AS bAutoClaim " _
                                    & " FROM PatientInsurance_DTL  " _
                                    & " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " _
                                    & " LEFT OUTER JOIN PatientRelationship ON  " _
                                    & " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " _
                                    & " WHERE PatientInsurance_DTL.nPatientID='" & PatientID & "'   ORDER BY SortOrder "


            objDataAdapter = New SqlDataAdapter(_strQuery, objCon)
            'objCon.Open()
            objDataAdapter.Fill(dtInsurance)
            Return dtInsurance

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return Nothing
        Finally

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objDataAdapter) Then
                objDataAdapter.Dispose()
                objDataAdapter = Nothing
            End If

        End Try
    End Function

    Private Function GetPatientForRxEligibility(ByVal strConnection As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(strConnection)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)
            oDB.Retrive("GetPatientsForRxEligibility", dt)
            oDB.Disconnect()

            Return dt
        Catch ex As Exception
            dt = Nothing
            Return Nothing
        Finally

            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If

            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Private Function GetPatientWithAppointments(ByVal strConnection As String) As DataTable

        Return GetPatientForRxEligibility(strConnection)

        'objCon = New SqlConnection(strConnection)
        'Dim objDataAdapter As SqlDataAdapter = Nothing
        'Dim objPatAppt = New DataTable
        'Dim strSql As String = ""
        'Try

        '    'Developer: Saagar K
        '    'Date: 10 Dec 2011
        '    'Bug ID/PRD Name/Salesforce Case:  Change requested from Drew Nolan
        '    'Reason: mail sent from Drew Nolan with subject "Surescripts Eligibility Request Change", removed the subscriber validation functionality because we will be sending the patient information and not subscriber information
        '    ''due to this whatever patients are having checked in appointment will be considered eligible for doing auto eligibility
        '    ''''''this query returns only patients having only appointments for todays date
        '    strSql = "SELECT DISTINCT AS_Appointment_MST.nPatientID, isnull(Patient.sFirstName,'') as sFirstName, isnull(Patient.sLastName,'') as sLastName " _
        '            & " FROM         AS_Appointment_MST INNER JOIN " _
        '            & " AS_Appointment_DTL ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID INNER JOIN " _
        '            & " Patient ON AS_Appointment_MST.nPatientID = Patient.nPatientID " _
        '            & " WHERE     (AS_Appointment_DTL.dtStartDate = ISNULL(CONVERT(VARCHAR(10), CONVERT(DATETIME, '" & Now.Date & "'), 112), '-')) AND " _
        '            & " (AS_Appointment_DTL.nUsedStatus NOT IN (7, 0)) "

        '    ''''''this query returns patients having only appointments for todays date as well as it checks whether patient is having all subscriber information in PatientInsurance_Dtl table
        '    'strSql = "SELECT DISTINCT AS_Appointment_MST.nPatientID, ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName, ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName," _
        '    '        & " isnull(PatientInsurance_DTL.dtDOB,'') dtDOB, ISNULL(PatientInsurance_DTL.sSubscriberGender,'') as sSubscriberGender," _
        '    '        & " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip FROM         AS_Appointment_MST INNER JOIN " _
        '    '        & " AS_Appointment_DTL ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID INNER JOIN " _
        '    '        & " PatientInsurance_DTL ON AS_Appointment_MST.nPatientID = PatientInsurance_DTL.nPatientID " _
        '    '        & " WHERE     (AS_Appointment_DTL.dtStartDate = ISNULL(CONVERT(VARCHAR(10), CONVERT(DATETIME, '" & Now.Date & "'), 112), '-')) AND  " _
        '    '        & " (AS_Appointment_DTL.nUsedStatus NOT IN (7, 0))  "



        '    objDataAdapter = New SqlDataAdapter(strSql, objCon)
        '    'objCon.Open()
        '    objDataAdapter.Fill(objPatAppt)
        '    Return objPatAppt

        'Catch ex As Exception
        '    mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
        '    Return Nothing
        'Finally

        '    If objCon.State = ConnectionState.Open Then
        '        objCon.Close()
        '    End If
        '    If Not IsNothing(objCon) Then
        '        objCon.Dispose()
        '        objCon = Nothing
        '    End If
        '    If Not IsNothing(objDataAdapter) Then
        '        objDataAdapter.Dispose()
        '        objDataAdapter = Nothing
        '    End If
        'End Try
    End Function
    'Added function to check if the user is already having task associated with it if yes then do not create another task.
    Private Function IsTaskGeneratedForUser(ByVal oClsRxHubInterface As gloRxHub.ClsRxHubInterface, ByVal UserID As Long, ByVal strConnection As String) As Long
        objCon = New SqlConnection(strConnection)
        Dim strSql As String = ""
        objcmd = New SqlCommand
        objcmd.Connection = objCon

        Try
            strSql = "SELECT count(*) FROM TM_TaskMST INNER JOIN TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID " _
                & " INNER JOIN TM_TASK_Assign on TM_TaskMST.nTaskID = TM_TASK_Assign.nTaskID " _
            & "WHERE TM_TaskMST.nPatientID = '" & oClsRxHubInterface.Patient.PatientID & "' " _
            & "AND TM_TaskMST.sSubject = 'Send formulary eligibility request'" _
            & "AND TM_TaskMST.nUserID = '" & UserID & "'" _
            & "AND TM_Task_Progress.dComplete <>100 AND TM_Task_Assign.nAcceptRejectHold <>4"

            objcmd.CommandText = strSql
            objcmd.CommandType = CommandType.Text
            objCon.Open()

            Dim _ReturnTaskID As Long = objcmd.ExecuteScalar()

            Return _ReturnTaskID

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return Nothing
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function


    Private Function GenerateTaskForUser(ByVal oClsRxHubInterface As gloRxHub.ClsRxHubInterface, ByVal TaskDescription As String, ByVal UserId As Long, ByVal strConnection As String) As Boolean
        Dim oPara As SqlParameter = Nothing
        Try
            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "TM_IN_Task"
            objcmd.CommandType = CommandType.StoredProcedure

            oPara = New SqlParameter("@nTaskID", SqlDbType.BigInt)
            oPara.Direction = ParameterDirection.InputOutput
            oPara.Value = 0

            'objcmd.Parameters.Add("@nRxTransactionID", SqlDbType.VarChar)
            objcmd.Parameters.Add(oPara)
            objcmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@sSubject", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nStartDate", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nDueDate", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nPriorityID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nCategoryID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nFollowUpID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@bIsPrivate", SqlDbType.Bit)
            objcmd.Parameters.Add("@nOwnerID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nDateCreated", SqlDbType.BigInt)
            objcmd.Parameters.Add("@sNoteExt", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nTaskType", SqlDbType.Int)
            objcmd.Parameters.Add("@sFaxTiffFileName", SqlDbType.VarChar)
            objcmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nReferenceID1", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nReferenceID2", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@MachineID", SqlDbType.BigInt)

            objcmd.Parameters("@nProviderID").Value = oClsRxHubInterface.Patient.Provider.ProviderID ''''''' nProviderID
            objcmd.Parameters("@nPatientID").Value = oClsRxHubInterface.Patient.PatientID '''''''nPatientID
            objcmd.Parameters("@sSubject").Value = "Send formulary eligibility request"
            objcmd.Parameters("@nStartDate").Value = DateAsNumber(DateTime.Now.ToString())
            objcmd.Parameters("@nDueDate").Value = DateAsNumber(DateTime.Now.ToString())
            objcmd.Parameters("@nPriorityID").Value = 1
            objcmd.Parameters("@nCategoryID").Value = 1
            objcmd.Parameters("@nFollowUpID").Value = 0
            objcmd.Parameters("@bIsPrivate").Value = False
            objcmd.Parameters("@nOwnerID").Value = UserId
            objcmd.Parameters("@nDateCreated").Value = DateAsNumber(DateTime.Now.ToString())
            objcmd.Parameters("@sNoteExt").Value = ""
            objcmd.Parameters("@nUserID").Value = UserId
            objcmd.Parameters("@nTaskType").Value = 1
            objcmd.Parameters("@sFaxTiffFileName").Value = ""
            objcmd.Parameters("@sMachineName").Value = System.Environment.MachineName
            objcmd.Parameters("@nReferenceID1").Value = 0
            objcmd.Parameters("@nReferenceID2").Value = 0
            objcmd.Parameters("@nClinicID").Value = mdlGeneral.gnClinicID
            objcmd.Parameters("@MachineID").Value = GetPrefixTransactionID(oClsRxHubInterface.Patient.PatientID, strConnection)

            objCon.Open()
            Dim _ReturnTaskID As Long = objcmd.ExecuteNonQuery()
            _ReturnTaskID = oPara.Value

            objcmd.Cancel()
            objcmd.Dispose()
            objcmd = Nothing

            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandText = "TM_IN_TaskAssign"
            objcmd.CommandType = CommandType.StoredProcedure

            objcmd.Parameters.Add("@nTaskID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nAssignToID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nAssignFromID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nSelfAssigned", SqlDbType.SmallInt)
            objcmd.Parameters.Add("@nAcceptRejectHold", SqlDbType.SmallInt)
            objcmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)

            objcmd.Parameters("@nTaskID").Value = _ReturnTaskID
            objcmd.Parameters("@nAssignToID").Value = UserId
            objcmd.Parameters("@nAssignFromID").Value = UserId
            objcmd.Parameters("@nSelfAssigned").Value = 1
            objcmd.Parameters("@nAcceptRejectHold").Value = 1
            objcmd.Parameters("@nClinicID").Value = mdlGeneral.gnClinicID
            objcmd.ExecuteNonQuery()

            objcmd.Cancel()
            objcmd.Dispose()
            objcmd = Nothing

            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandText = "TM_IN_TaskProgress"
            objcmd.CommandType = CommandType.StoredProcedure

            objcmd.Parameters.Add("@nTaskID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nStatusID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@dComplete", SqlDbType.Decimal)
            objcmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nDateTime", SqlDbType.DateTime)
            objcmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)

            objcmd.Parameters("@nTaskID").Value = _ReturnTaskID
            objcmd.Parameters("@nStatusID").Value = 1
            objcmd.Parameters("@dComplete").Value = 0
            objcmd.Parameters("@sDescription").Value = TaskDescription '''''''''''eRxStatusMessage.ToString()
            objcmd.Parameters("@nDateTime").Value = DateTime.Now
            objcmd.Parameters("@nClinicID").Value = mdlGeneral.gnClinicID

            objcmd.ExecuteNonQuery()
            objcmd.Cancel()
            objcmd.Dispose()
            objcmd = Nothing
            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally
            'objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(oPara) Then
                oPara = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Private Function GenerateTaskForAdmin(ByVal oClsRxHubInterface As gloRxHub.ClsRxHubInterface, ByVal TaskDescription As String, ByVal UserId As Long, ByVal strConnection As String) As Boolean
        Dim oPara As SqlParameter = Nothing
        Try
            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "TM_IN_Task"
            objcmd.CommandType = CommandType.StoredProcedure

            oPara = New SqlParameter("@nTaskID", SqlDbType.BigInt)
            oPara.Direction = ParameterDirection.InputOutput
            oPara.Value = 0

            'objcmd.Parameters.Add("@nRxTransactionID", SqlDbType.VarChar)
            objcmd.Parameters.Add(oPara)
            objcmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@sSubject", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nStartDate", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nDueDate", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nPriorityID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nCategoryID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nFollowUpID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@bIsPrivate", SqlDbType.Bit)
            objcmd.Parameters.Add("@nOwnerID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nDateCreated", SqlDbType.BigInt)
            objcmd.Parameters.Add("@sNoteExt", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nTaskType", SqlDbType.Int)
            objcmd.Parameters.Add("@sFaxTiffFileName", SqlDbType.VarChar)
            objcmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nReferenceID1", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nReferenceID2", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@MachineID", SqlDbType.BigInt)

            objcmd.Parameters("@nProviderID").Value = oClsRxHubInterface.Patient.Provider.ProviderID ''''''' nProviderID
            objcmd.Parameters("@nPatientID").Value = oClsRxHubInterface.Patient.PatientID '''''''nPatientID
            objcmd.Parameters("@sSubject").Value = "Send formulary eligibility request"
            objcmd.Parameters("@nStartDate").Value = DateAsNumber(DateTime.Now.ToString())
            objcmd.Parameters("@nDueDate").Value = DateAsNumber(DateTime.Now.ToString())
            objcmd.Parameters("@nPriorityID").Value = 1
            objcmd.Parameters("@nCategoryID").Value = 1
            objcmd.Parameters("@nFollowUpID").Value = 0
            objcmd.Parameters("@bIsPrivate").Value = False
            objcmd.Parameters("@nOwnerID").Value = UserId
            objcmd.Parameters("@nDateCreated").Value = DateAsNumber(DateTime.Now.ToString())
            objcmd.Parameters("@sNoteExt").Value = ""
            objcmd.Parameters("@nUserID").Value = UserId
            objcmd.Parameters("@nTaskType").Value = 1
            objcmd.Parameters("@sFaxTiffFileName").Value = ""
            objcmd.Parameters("@sMachineName").Value = System.Environment.MachineName
            objcmd.Parameters("@nReferenceID1").Value = 0
            objcmd.Parameters("@nReferenceID2").Value = 0
            objcmd.Parameters("@nClinicID").Value = mdlGeneral.gnClinicID
            objcmd.Parameters("@MachineID").Value = GetPrefixTransactionID(oClsRxHubInterface.Patient.PatientID, strConnection)



            objCon.Open()
            Dim _ReturnTaskID As Long = objcmd.ExecuteNonQuery()
            _ReturnTaskID = oPara.Value

            objcmd.Cancel()
            objcmd.Dispose()
            objcmd = Nothing

            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandText = "TM_IN_TaskAssign"
            objcmd.CommandType = CommandType.StoredProcedure

            objcmd.Parameters.Add("@nTaskID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nAssignToID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nAssignFromID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nSelfAssigned", SqlDbType.SmallInt)
            objcmd.Parameters.Add("@nAcceptRejectHold", SqlDbType.SmallInt)
            objcmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)

            objcmd.Parameters("@nTaskID").Value = _ReturnTaskID
            objcmd.Parameters("@nAssignToID").Value = UserId
            objcmd.Parameters("@nAssignFromID").Value = UserId
            objcmd.Parameters("@nSelfAssigned").Value = 1
            objcmd.Parameters("@nAcceptRejectHold").Value = 1
            objcmd.Parameters("@nClinicID").Value = mdlGeneral.gnClinicID
            objcmd.ExecuteNonQuery()

            objcmd.Cancel()
            objcmd.Dispose()
            objcmd = Nothing

            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandText = "TM_IN_TaskProgress"
            objcmd.CommandType = CommandType.StoredProcedure

            objcmd.Parameters.Add("@nTaskID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nStatusID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@dComplete", SqlDbType.Decimal)
            objcmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nDateTime", SqlDbType.DateTime)
            objcmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)

            objcmd.Parameters("@nTaskID").Value = _ReturnTaskID
            objcmd.Parameters("@nStatusID").Value = 1
            objcmd.Parameters("@dComplete").Value = 0
            objcmd.Parameters("@sDescription").Value = TaskDescription '''''''''''eRxStatusMessage.ToString()
            objcmd.Parameters("@nDateTime").Value = DateTime.Now
            objcmd.Parameters("@nClinicID").Value = mdlGeneral.gnClinicID

            objcmd.ExecuteNonQuery()
            objcmd.Cancel()
            objcmd.Dispose()
            objcmd = Nothing
            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally
            'objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(oPara) Then
                oPara = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Private Function AddTaskFromAutoEligiblity(ByVal oClsRxHubInterface As gloRxHub.ClsRxHubInterface, ByVal TaskDescription As String, ByVal strConnection As String) As Boolean
        Dim dtProviderUser As DataTable = Nothing
        Try
            dtProviderUser = New DataTable

            dtProviderUser = GetUserAssociatedWithProvider(oClsRxHubInterface.Patient.Provider.ProviderID, strConnection)

            If Not IsNothing(dtProviderUser) Then
                If dtProviderUser.Rows.Count > 0 Then
                    For i As Integer = 0 To dtProviderUser.Rows.Count - 1
                        If dtProviderUser.Rows(i)("nOthersID") <> 0 Then
                            'Added code to check if the user is already having task associated with it if yes then do not create another task
                            If IsTaskGeneratedForUser(oClsRxHubInterface, dtProviderUser.Rows(i)("nOthersID"), strConnection) = 0 Then
                                Dim retval As Boolean = GenerateTaskForUser(oClsRxHubInterface, TaskDescription, dtProviderUser.Rows(i)("nOthersID"), strConnection)
                                If gblnGenerateRxEligibilityLog = True Then
                                    If retval = False Then
                                        mdlGeneral.UpdateLog("Error : in task generation in function  GenerateTaskForUser() for User id " & dtProviderUser.Rows(i)("nOthersID").ToString, strConnection)
                                    Else
                                        mdlGeneral.UpdateLog("Generated task for admin user against userid  " & dtProviderUser.Rows(i)("nOthersID").ToString, strConnection)
                                    End If
                                End If
                            End If
                        Else ''''if 0 returned then generate task against admin
                            dtProviderUser = GetUserAssociatedWithProvider(oClsRxHubInterface.Patient.Provider.ProviderID, strConnection, "admin")
                            If Not IsNothing(dtProviderUser) Then
                                If dtProviderUser.Rows.Count > 0 Then
                                    'Added code to check if the user is already having task associated with it if yes then do not create another task
                                    If IsTaskGeneratedForUser(oClsRxHubInterface, dtProviderUser.Rows(0)("nUserId"), strConnection) = 0 Then
                                        Dim retval As Boolean = GenerateTaskForAdmin(oClsRxHubInterface, TaskDescription, dtProviderUser.Rows(0)("nUserId"), strConnection)
                                        If gblnGenerateRxEligibilityLog = True Then
                                            If retval = False Then
                                                mdlGeneral.UpdateLog("Error : in task generation in function  GenerateTaskForAdmin() for User id " & dtProviderUser.Rows(0)("nUserId").ToString, strConnection)
                                            Else
                                                mdlGeneral.UpdateLog("Generated task for admin user against userid  " & dtProviderUser.Rows(0)("nUserId").ToString, strConnection)
                                            End If
                                        End If

                                    End If
                                End If
                            End If
                        End If


                    Next ''''generate task against next user id
                Else '''''since there are no providers associated with the user then get generate task against admin which is stored in User_Mst and Login Name = admin
                    dtProviderUser = GetUserAssociatedWithProvider(oClsRxHubInterface.Patient.Provider.ProviderID, strConnection, "admin")
                    If Not IsNothing(dtProviderUser) Then
                        If dtProviderUser.Rows.Count > 0 Then
                            'Added code to check if the user is already having task associated with it if yes then do not create another task
                            If IsTaskGeneratedForUser(oClsRxHubInterface, dtProviderUser.Rows(0)("nUserId"), strConnection) = 0 Then
                                Dim retval As Boolean = GenerateTaskForAdmin(oClsRxHubInterface, TaskDescription, dtProviderUser.Rows(0)("nUserId"), strConnection)
                                If gblnGenerateRxEligibilityLog = True Then
                                    If retval = False Then
                                        mdlGeneral.UpdateLog("Error : in task generation in function  GenerateTaskForAdmin() for User id " & dtProviderUser.Rows(0)("nUserId").ToString, strConnection)
                                    Else
                                        mdlGeneral.UpdateLog("Generated task for admin user against userid  " & dtProviderUser.Rows(0)("nUserId").ToString, strConnection)
                                    End If
                                End If
                            End If

                        End If
                    End If
                End If
            End If


            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally
            If Not IsNothing(dtProviderUser) Then
                dtProviderUser.Dispose()
            End If

        End Try


    End Function
#End Region

    Public Sub RetrieveRxResponses(ByVal IsStaging As Boolean)
        Dim sSettingsNameOne As String = ""
        Dim sSettingsNameTwo As String = ""
        Dim sLogString As String = ""

        If IsStaging Then
            sSettingsNameOne = "eRxStagingWebserviceURL"
            sSettingsNameTwo = "eRx10dot6StagingWebserviceURL"
            sLogString = "Staging"
        Else
            sSettingsNameOne = "eRxProductionWebserviceURL"
            sSettingsNameTwo = "eRx10dot6ProductionWebserviceURL"
            sLogString = "Production"
        End If

        Dim strReponseFile As String = ""
        Dim i As Integer = 0
        Try
            mdlGeneral.SetDbCredentials()
            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
                Dim objSendEncryption As clsEncryption = New clsEncryption
                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword
                Dim ds As New DataSet
                Dim oDB As New gloDatabaseLayer.DBLayer(strgloServiceConnection)
                oDB.Connect(False)
                oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('EnableDirectory4dot4','" + sSettingsNameOne + "','" + sSettingsNameTwo + "') ORDER BY sSettingsName", ds)
                oDB.Disconnect()
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
                Dim strProviders As String = RetrieveProividers(strConnection)
                If Not strProviders Is Nothing And strProviders <> "" Then

                    Dim cntResponse As Byte() = Nothing

                    If ds.Tables(0).Rows.Count > 2 Then
                        myRxWCFService = ServiceClass.GetWCFSvc(ds.Tables(0).Rows(1)("sSettingsValue").ToString())
                        mdlGeneral.UpdateLog("Access to Webservice is successful on " + sLogString + " Server", strConnection)
                        cntResponse = myRxWCFService.GetResponseMessages(strProviders, "")
                    Else
                        mdlGeneral.UpdateLog("No eRx Web Service Settings Found ", strConnection)
                        Exit Sub
                    End If

                    gloSurescriptGeneral.blnIsStagingServer = mdlGeneral.gblnStagingServer

                    If IsStaging Then
                        gloSurescriptGeneral.eRx10dot6StagingWebserviceURL = ds.Tables(0).Rows(1)("sSettingsValue").ToString()
                    Else
                        gloSurescriptGeneral.eRx10dot6ProductionWebserviceURL = ds.Tables(0).Rows(1)("sSettingsValue").ToString()
                    End If

                    If cntResponse IsNot Nothing Then
                        Me.InsertIntoDatabase(cntResponse, strConnection, sLogString)
                    End If

                Else
                    'To avoid to write multiple time log message.
                    If bIsProviderAvailable = False Then
                        mdlGeneral.UpdateLog("No Provider are available with SPIID", strConnection)
                        bIsProviderAvailable = True
                    End If
                End If
                If Not IsNothing(ds) Then
                    ds.Dispose()
                    ds = Nothing
                End If
                If Not IsNothing(objSendEncryption) Then
                    objSendEncryption = Nothing
                End If
                If Not IsNothing(myRxWCFService) Then
                    myRxWCFService.Close()
                    myRxWCFService = Nothing
                End If
                If Not IsNothing(myRxService) Then
                    myRxService.Dispose()
                    myRxService = Nothing
                End If
                i = (i + 1)
            Loop
            i = 0
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
        Finally
            'delete the xml file after reading.
            If strReponseFile <> "" Then
                If File.Exists(strReponseFile) Then
                    File.Delete(strReponseFile)
                End If
            End If
        End Try
    End Sub

    Private Sub InsertIntoDatabase(ByVal cntResponse As Byte(), ByVal ConnectionString As String, ByVal Server As String)
        Dim strReponseFile As String = ""

        Try
            strReponseFile = mdlGeneral.ConvertBinarytoFile(cntResponse, mdlGeneral.GetFileName(enumFileType.XMLFile))            

            If strReponseFile <> "" Then
                If File.Exists(strReponseFile) Then
                    Dim dsResponses As New DataSet
                    dsResponses.ReadXml(strReponseFile)
                    If Not dsResponses Is Nothing Then
                        If dsResponses.Tables.Count > 0 Then
                            Dim strTransaction As String = SaveRxResponses(dsResponses.Tables(0), ConnectionString)

                            If strTransaction <> "" Then
                                myRxWCFService.UpdateDownloadStatus(strTransaction)
                                mdlGeneral.UpdateLog("UpdateDownloadStatus completed on " + Server + " Server", strConnection)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
        End Try
    End Sub

    Private Function GetUserAssociatedWithProvider(ByVal ProviderID As Long, ByVal strConnection As String, Optional ByVal AdminName As String = "") As DataTable
        objCon = New SqlConnection(strConnection)
        Dim objDataAdapter As SqlDataAdapter = Nothing
        Dim objProviderUsers = New DataTable
        Dim strSql As String = ""
        Try
            If AdminName <> "" Then
                strSql = "select nUserId from User_Mst where sLoginName = '" & AdminName & "'"
            Else
                'strSql = "select nUserId from User_Mst where nProviderId = " & ProviderID
                strSql = "Select ISNULL(nOthersID,0) as nOthersID from ProviderSettings where nProviderID=" & ProviderID & " and UPPER(sSettingsType)='RXELIGIBILITYUSER'"
            End If

            objDataAdapter = New SqlDataAdapter(strSql, objCon)
            'objCon.Open()
            objDataAdapter.Fill(objProviderUsers)

            Return objProviderUsers

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return Nothing
        Finally

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objDataAdapter) Then
                objDataAdapter.Dispose()
                objDataAdapter = Nothing
            End If
        End Try
    End Function

    Private Function GetRxHubSettings(ByVal strConnection As String) As DataTable
        objCon = New SqlConnection(strConnection)
        Dim objDataAdapter As SqlDataAdapter = Nothing
        Dim objRxhubSettings = New DataTable
        Dim strSql As String = ""
        Try
            strSql = "select * from settings where ssettingsname in ('ADVANCE RX ENABLED','ADVANCE RX STAGING SERVER','RXHUB PARTICIPANTID','RXHUB PASSWORD','RXELIGIBILITYEMR','EDISERVICEPATH')"
            objDataAdapter = New SqlDataAdapter(strSql, objCon)
            'objCon.Open()
            objDataAdapter.Fill(objRxhubSettings)
            Return objRxhubSettings

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return Nothing
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objDataAdapter) Then
                objDataAdapter.Dispose()
                objDataAdapter = Nothing
            End If
        End Try
    End Function

    Public Sub New()
        myRxService = New eRxMessage
        myRxServiceProd = New eRXProd.eRxMessage
        myRxService.Credentials = System.Net.CredentialCache.DefaultCredentials
        strConnection = mdlGeneral.GetConnectionString()
        strgloServiceConnection = mdlGeneral.GetServiceConnectionstring()
        mygloDirectService = New gloDirectStaging.IgloDirectClient
        'mdlGeneral.UpdateLog(strConnection)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        If Not IsNothing(myRxService) Then
            myRxService.Dispose()
            myRxService = Nothing
        End If
        If Not IsNothing(myRxServiceProd) Then
            myRxServiceProd.Dispose()
            myRxServiceProd = Nothing
        End If
        If Not IsNothing(myRxWCFService) Then
            myRxWCFService = Nothing
        End If
        If Not IsNothing(mygloDirectService) Then
            mygloDirectService = Nothing
        End If
        strConnection = String.Empty
        strgloServiceConnection = String.Empty
    End Sub

    Private Function GetDrugName(ByVal nPrescriptionID As Long, ByVal strConnection As String) As String
        Dim DrugName As String = String.Empty
        objCon = New SqlConnection(strConnection)
        Try
            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.Text
            Dim strquery As String = "select sMedication + ' ' + sDosage As DrugName from Prescription where nprescriptionid = " & nPrescriptionID & "   "
            objcmd.CommandText = strquery
            objCon.Open()
            DrugName = objcmd.ExecuteScalar()
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        Finally
            strSQL = String.Empty
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
        Return DrugName
    End Function


#Region "EAR Reporting"
    'Public Function SendEARFile_OriginalBeforeOfferMgmtChange() As Boolean
    '    Dim i As Integer = 0
    '    Dim dtRxhubSettings As New DataTable
    '    Try


    '        mdlGeneral.SetDbCredentials()
    '        Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)


    '            Dim objSendEncryption As clsEncryption = New clsEncryption
    '            Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
    '            Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

    '            gloEAR.mdlGeneral.sServerName = mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString
    '            gloEAR.mdlGeneral.sDatabaseName = mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString
    '            gloEAR.mdlGeneral.sUserName = mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString
    '            gloEAR.mdlGeneral.sPassword = strDbPassword

    '            Dim _boolExistTable As Boolean = frmDBCredentials.checkScripts(strConnection, "Rxh_PendingEAR")
    '            If _boolExistTable = False Then
    '                mdlGeneral.UpdateLog("EAR Table does not exist for server " & gloEAR.mdlGeneral.sServerName & " and database " & gloEAR.mdlGeneral.sDatabaseName)
    '                ''since the EAR Table does not exist this server and for this database then continue with the next database 
    '                i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                Continue Do
    '            End If

    '            dtRxhubSettings = GetRxHubSettings(strConnection)
    '            If Not IsNothing(dtRxhubSettings) Then

    '                If dtRxhubSettings.Rows.Count > 0 Then

    '                    mdlGeneral.UpdateLog("Retrieved RxHub settings .........")

    '                    gloRxHub.ClsgloRxHubGeneral.ConnectionString = strConnection
    '                    'mdlGeneral.UpdateLog("Assigned connection string " & strConnection & " to   gloRxHub.ClsgloRxHubGeneral.ConnectionString.........")

    '                    For rwcnt As Integer = 0 To dtRxhubSettings.Rows.Count - 1


    '                        If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "ADVANCE RX ENABLED" Then
    '                            gloEAR.mdlGeneral.sAdvanceRxEnabled = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
    '                            If gloEAR.mdlGeneral.sAdvanceRxEnabled <> "1" Then ''''''''''''advance Rx hub setting is OFF
    '                                mdlGeneral.UpdateLog("Invalid setting for auto eligibility since Advance Rxhub Settings is OFF on server " & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and on database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)
    '                                ''since the RxHub setting is off on this server and for this database then continue with the next database 
    '                                i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                                Continue Do
    '                            End If
    '                        End If


    '                        If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "ADVANCE RX STAGING SERVER" Then
    '                            gloEAR.mdlGeneral.sAdvanceStagingServer = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
    '                            If gloEAR.mdlGeneral.sAdvanceStagingServer <> "1" Then ''''''''''''web service Pointed to Production server
    '                                mdlGeneral.UpdateLog("The service is pointed to Production server")
    '                                gloRxHub.ClsgloRxHubGeneral.gblnIsRxhubStagingServer = False
    '                            Else
    '                                mdlGeneral.UpdateLog("The service is pointed to Staging server")
    '                                gloRxHub.ClsgloRxHubGeneral.gblnIsRxhubStagingServer = True
    '                            End If
    '                        End If


    '                        If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "RXHUB PARTICIPANTID" Then
    '                            gloEAR.mdlGeneral.eRxHubParticipantId = dtRxhubSettings.Rows(rwcnt)("sSettingsValue")
    '                            If gloEAR.mdlGeneral.eRxHubParticipantId = "" Then ''''''''''''Participant ID
    '                                ''since the RxHub Participant Id is blank on this server and for this database then continue with the next database 
    '                                i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                                Continue Do
    '                            End If
    '                        End If


    '                        If dtRxhubSettings.Rows(rwcnt)("sSettingsName") = "RXHUB PASSWORD" Then
    '                            Dim objEncryption As New clsEncryption
    '                            gloEAR.mdlGeneral.eRxHubPassword = objEncryption.DecryptFromBase64String(dtRxhubSettings.Rows(rwcnt)("sSettingsValue"), mdlGeneral.constEncryptDecryptKeyDB)
    '                            If gloEAR.mdlGeneral.eRxHubPassword = "" Then ''''''''''''RxHubPassword
    '                                ''since the RxHub Password is blank on this server and for this database then continue with the next database 
    '                                i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                                Continue Do
    '                            ElseIf gloEAR.mdlGeneral.eRxHubPassword.Contains("Bad Data.") Then
    '                                ''since the RxHub Password contains Bad Data. on this server and for this database then continue with the next database 
    '                                i = i + 1 ''''proceed to the next database in the while loop, no need to execute futher code
    '                                Continue Do
    '                            End If
    '                        End If
    '                    Next



    '                    ''''''Create Outbox folder if not present, create 1 and create the EAR request file in this Outbox folder to route it to "to_Rxhub" folder which will be done by the web service.
    '                    If System.Windows.Forms.Application.StartupPath <> "" Then
    '                        If Not Directory.Exists(System.Windows.Forms.Application.StartupPath & "\Outbox") Then
    '                            'create the Outbox directory, to save the EAR generated files.
    '                            Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath & "\Outbox")
    '                        End If
    '                    End If
    '                    'gloEAR.mdlGeneral.sEARReqDownloadDir = System.Windows.Forms.Application.StartupPath & "\Outbox"


    '                    gloEAR.gloRxData.gstrDatabaseConnectionString = strConnection

    '                    ''''''calculate the from date i.e. the date of Sunday, so that we can generate the report from period Sunday to Saturday
    '                    Dim dtEARFromDate_Sunday As DateTime = Nothing
    '                    Dim dtEARToDate_Saturday As DateTime = Now.Date '''''to date will be always todays date i.e. saturday, for which we have done the validation, before comming in this function

    '                    dtEARFromDate_Sunday = dtEARToDate_Saturday.AddDays(-6) ''''''substract 6 days from todays date i.e Saturday to get the date of the day which is Sunday


    '                    '''''''this module will check whether the EAR file was sent for the selected week depending on whether there is any record present for that week period
    '                    Dim isFileSent As Integer = gloEAR.gloRxData.IsFileSentForThisWeek(dtEARFromDate_Sunday, dtEARToDate_Saturday)

    '                    '''''if returned val is >= 1 that means record was present and, therfore do not send the file for that week period
    '                    If isFileSent >= 1 Then
    '                        mdlGeneral.UpdateLog("EAR file already sent for current week period.........")
    '                    Else '''''since file is not created for that week then create and send the file
    '                        '''''assign the gloEAR.mdlGeneral.sEARFileName with strEARFilepath so that it will save in the database, also create the EAR request file in the application startup path.
    '                        gloEAR.mdlGeneral.sEARFilePath = gloEAR.gloRxData.NEWGenerateEARTextFile(dtEARFromDate_Sunday, dtEARToDate_Saturday, System.Windows.Forms.Application.StartupPath & "\Outbox", "N")

    '                        'If blnIsStagingServer = True Then
    '                        If File.Exists(gloEAR.mdlGeneral.sEARFilePath) Then
    '                            Try
    '                                Dim oFile As FileStream
    '                                Dim oReader As BinaryReader
    '                                Dim sEARFileName As String = ""
    '                                ''To read the file only when it is not in use by any process
    '                                oFile = New FileStream(gloEAR.mdlGeneral.sEARFilePath, FileMode.Open, FileAccess.Read)

    '                                oReader = New BinaryReader(oFile)
    '                                Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)
    '                                Dim myService As New StagingEARTesting.eRxMessage
    '                                myService.Credentials = System.Net.CredentialCache.DefaultCredentials
    '                                Dim _Key As String = myService.Login("sarika@ophit.net", "spX12ss@!!21nasik")


    '                                If gloEAR.mdlGeneral.sEARFilePath.Contains("\") Then ''''this means that the selected drug is a drug from ProviderDrugs subnode and so we need to fetch the druginfo from the DrugsProviderAssociation table
    '                                    Dim sEARFileInfo As String() = SplitEARFilePath(gloEAR.mdlGeneral.sEARFilePath)

    '                                    If Not IsNothing(sEARFileInfo) Then
    '                                        If sEARFileInfo.Length > 1 Then
    '                                            For cnt As Integer = 0 To sEARFileInfo.Length - 1
    '                                                If sEARFileInfo(cnt).Contains(".rpt") Then
    '                                                    sEARFileName = sEARFileInfo(cnt)
    '                                                End If
    '                                            Next

    '                                        End If
    '                                    End If

    '                                Else

    '                                End If

    '                                If myService.SendRequestFile(bytesRead, _Key, sEARFileName) Then
    '                                    mdlGeneral.UpdateLog("EAR file sent sucessfully ")
    '                                    'System.Windows.Forms.MessageBox.Show("EAR file sent sucessfully", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)


    '                                    '''''''if sucessful save the EAR request file information to database 
    '                                    If gloEAR.gloRxData.SaveReportFileDetails(dtEARFromDate_Sunday, dtEARToDate_Saturday, sEARFileName) Then
    '                                        '''''since we save the EAR file information id the database, delete the created EAR request file from Inbox folder
    '                                        ''''''My.Computer.FileSystem.DeleteFile(gloEAR.mdlGeneral.sEARFileName, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
    '                                        'File.Delete(gloEAR.mdlGeneral.sEARFilePath)
    '                                    Else '''''there must be error while saving in database 
    '                                        mdlGeneral.UpdateLog("Unable to save the EAR report file ")
    '                                    End If


    '                                Else
    '                                    'System.Windows.Forms.MessageBox.Show("Unable to send EAR file.", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
    '                                    mdlGeneral.UpdateLog("Unable to send EAR file.")
    '                                End If

    '                            Catch ex As Exception
    '                                mdlGeneral.UpdateLog("SendEARFile - " & ex.ToString)
    '                                'System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
    '                            End Try

    '                        End If
    '                    End If


    '                End If '''''''''dtRxhubSettings.Rows.Count > 0

    '            End If '''''if not isnothing(dtRxhubSettings)


    '            i = (i + 1)
    '        Loop
    '        i = 0


    '        Return True
    '    Catch ex As Exception
    '        If Not IsNothing(dtRxhubSettings) Then
    '            dtRxhubSettings.Dispose()
    '        End If
    '        Return False
    '    Finally
    '        If Not IsNothing(dtRxhubSettings) Then
    '            dtRxhubSettings.Dispose()
    '        End If
    '    End Try
    'End Function


    ''' <summary>
    ''' Function in use it is used to extract the binary response from
    ''' Surescript for NewRx and RefillResponse and process the same
    ''' </summary>
    ''' <param name="cntFromDB"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExtractEARRsp(ByVal cntFromDB As Object, ByVal EARResponseFileName As String) As String
        Dim stream As MemoryStream = Nothing
        Dim oFile As System.IO.FileStream = Nothing
        Try
            If Not IsNothing(cntFromDB) Then
                Dim strEARRspfilename As String = System.Windows.Forms.Application.StartupPath & "\Outbox\" & EARResponseFileName & ".rsp"
                Dim content As Byte() = CType(cntFromDB, Byte())
                stream = New MemoryStream(content)
                oFile = New System.IO.FileStream(strEARRspfilename, System.IO.FileMode.Create)
                stream.WriteTo(oFile)
                oFile.Close()

                Return strEARRspfilename
            Else
                'If gloSurescriptGeneral._isInternetAvailable = False Then
                '    gloSurescriptGeneral.UpdateLog("This eRx will not be sent now as no internet connection is available. It will be queued and sent when internet connection will again be detected. Do not send it again.")
                '    Return ""
                'Else
                '    MessageBox.Show("Response not available from Surescript Server", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    gloSurescriptGeneral.UpdateLog("Response Object not returned")
                '    Return ""
                'End If
                Return ""
            End If
        Catch ex As Exception
            Return ""
        Finally
            If Not IsNothing(stream) Then
                stream.Dispose()
                stream = Nothing
            End If
            If Not IsNothing(oFile) Then
                oFile.Dispose()
                oFile = Nothing
            End If
        End Try

    End Function

    Private Function SplitEARFilePath(ByVal EARFilePath As String) As Array
        Try
            Dim _result As String()
            _result = EARFilePath.Split("\")
            Return _result
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GetEARPendingReportFiles(ByVal EARConnectionString) As DataTable
        Dim dtEARPendingFileName As New DataTable
        Dim conn As New SqlConnection()
        Dim cmd As New SqlCommand
        Dim _strSQL As String = ""
        Dim da As SqlDataAdapter = Nothing

        Try
            'get the Provider Details using the ProviderID
            _strSQL = " SELECT ISNULL(sReportFile,'') as EARReportFileName from RxH_PendingEAR where sStatus = 'Pending'"
            conn.ConnectionString = EARConnectionString
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = _strSQL

            da = New SqlDataAdapter(cmd)
            da.Fill(dtEARPendingFileName)


            Return dtEARPendingFileName

        Catch sqlEx As SqlException
            mdlGeneral.UpdateLog("GetEARPendingReportFiles() - " & sqlEx.ToString, EARConnectionString)
            Return Nothing
        Catch ex As Exception
            mdlGeneral.UpdateLog("GetEARPendingReportFiles() - " & ex.ToString, EARConnectionString)
            Return Nothing
        Finally
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
        End Try


    End Function

    'Public Function ChecknUpdateEARResponse() As Boolean
    '    Dim i As Integer = 0
    '    Dim dtEARFiles As New DataTable
    '    Try
    '        mdlGeneral.SetDbCredentials()
    '        Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
    '            Dim objSendEncryption As clsEncryption = New clsEncryption
    '            Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
    '            Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

    '            gloEAR.mdlGeneral.sServerName = mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString
    '            gloEAR.mdlGeneral.sDatabaseName = mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString
    '            gloEAR.mdlGeneral.sUserName = mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString
    '            gloEAR.mdlGeneral.sPassword = strDbPassword

    '            Dim _boolExistTable As Boolean = frmDBCredentials.checkScripts(strConnection, "Rxh_PendingEAR")
    '            If _boolExistTable Then

    '                gloEAR.gloRxData.gstrDatabaseConnectionString = strConnection

    '                dtEARFiles = GetEARPendingReportFiles(gloEAR.gloRxData.gstrDatabaseConnectionString)

    '                If Not IsNothing(dtEARFiles) Then
    '                    If dtEARFiles.Rows.Count > 0 Then
    '                        Dim readbytes As Byte()
    '                        '''''''Create outbox folder if not present
    '                        'Using the connection string get the EAR filename who has status as "Pending", take this file and check whether the response is recieved in the "from_rxHub"


    '                        '''''''check the Outbox folder with file having .rsp (response file) extension
    '                        '''''read this file and update the Rxh_PendingEAR table with the response file contents, and delete the file.
    '                        Dim myService As New EARStaging.eRxMessage
    '                        myService.Credentials = System.Net.CredentialCache.DefaultCredentials
    '                        Dim _Key As String = myService.Login("sarika@ophit.net", "spX12ss@!!21nasik")

    '                        For cnt As Integer = 0 To dtEARFiles.Rows.Count - 1

    '                            Dim sEARFileName As String = dtEARFiles.Rows(cnt)("EARReportFileName")
    '                            readbytes = myService.RecieveResposeFile(_Key, sEARFileName)
    '                            If Not IsNothing(readbytes) Then
    '                                Dim strEARRespFileNamePath As String = ExtractEARRsp(readbytes, sEARFileName) ''''''save the EAR response file with .rsp extension in the Inbox folder of application startup path
    '                                If strEARRespFileNamePath <> "" Then
    '                                    mdlGeneral.UpdateLog("Downloaded and Recieved response for file name " & strEARRespFileNamePath)
    '                                    '''''update the RxH_PendingEAR table rec with the matching EAR filename, and changed the status to "Responce recieved"
    '                                    gloEAR.gloRxData.UpdateStatus(sEARFileName, strEARRespFileNamePath, gloEAR.gloRxData.gstrDatabaseConnectionString)
    '                                    mdlGeneral.UpdateLog("Updated status for file as Responce recieved")
    '                                    '''''after updation delete the response file as done for EAR Request file
    '                                    File.Delete(strEARRespFileNamePath)
    '                                Else

    '                                    'System.Windows.Forms.MessageBox.Show("Unable to create EAR Response file...", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
    '                                    mdlGeneral.UpdateLog("Unable to create EAR Response file..." & sEARFileName)
    '                                End If

    '                            Else
    '                                'System.Windows.Forms.MessageBox.Show("No EAR Reponse recieved.....", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
    '                                mdlGeneral.UpdateLog("No EAR Reponse recieved for file ....." & sEARFileName)
    '                            End If
    '                        Next
    '                    End If
    '                End If
    '            Else
    '                mdlGeneral.UpdateLog("Table does not exist for server ........." & mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString & " and database " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)
    '            End If ''''''_boolExistTable

    '            i = (i + 1)
    '        Loop
    '        i = 0
    '        Return True
    '    Catch ex As Exception
    '        mdlGeneral.UpdateLog("ChecknUpdateEARResponse() - " & ex.ToString)
    '        If Not IsNothing(dtEARFiles) Then
    '            dtEARFiles.Dispose()
    '        End If
    '    Finally
    '        If Not IsNothing(dtEARFiles) Then
    '            dtEARFiles.Dispose()
    '        End If
    '    End Try

    'End Function
#End Region

    Public Sub SendeRx()

        Dim i As Integer = 0
        Dim myService As New eRXStag.eRxMessage


        'Dim bytesRead As Byte()
        Dim readbytes As Byte()
        Dim _eRxStatus As String = ""
        Dim _eRxStatusMessage As String = ""
        Dim _nPrescriptionID As Long = 0
        Dim _sDrugName As String = ""
        Dim _nProviderD As Long = 0
        Dim _nPatientID As Long = 0
        Dim _nUserID As Long = 0



        mdlGeneral.SetDbCredentials()
        Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
            Dim objSendEncryption As clsEncryption = New clsEncryption
            Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
            Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword
            If Not IsNothing(objSendEncryption) Then
                objSendEncryption = Nothing
            End If
            Dim ds As New DataSet
            Dim oDB As New gloDatabaseLayer.DBLayer(strConnection)
            oDB.Connect(False)

            'oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE sSettingsName = 'eRxStagingWebserviceURL'", ds)
            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM settings WITH (NOLOCK) WHERE sSettingsName in ('eRxStagingWebserviceURL','eRx10dot6stagingWebserviceURL') ORDER BY sSettingsName desc", ds)

            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If



            gloSureScript.gloSurescriptGeneral.DatabaseName = mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString
            gloSureScript.gloSurescriptGeneral.ServerName = mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString
            gloSureScript.gloSurescriptGeneral.sUserName = mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString
            gloSureScript.gloSurescriptGeneral.sPassword = strDbPassword
            gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = True
            Dim eRxTable As DataTable = RetrieveeRx(strConnection)
            Dim eRxCount As Integer = 0
            Do While (eRxCount < eRxTable.Rows.Count)


                Dim data As Byte() = Convert.FromBase64String(eRxTable.Rows(eRxCount)("eRxFile")) ' CType(eRxTable.Rows(eRxCount)("eRxFile").ToString(), Byte())

                'Dim data As [Byte]() = New Byte(DirectCast(Convert.FromBase64String(eRxTable.Rows(eRxCount)("eRxFile").ToString()), [Byte]()).ToString().Length - 1) {}
                _nPrescriptionID = eRxTable.Rows(eRxCount)("nPrescriptionID")
                _nProviderD = eRxTable.Rows(eRxCount)("nProviderId")
                _nPatientID = eRxTable.Rows(eRxCount)("nPatientID")
                _nUserID = eRxTable.Rows(eRxCount)("nloginUserId")

                Dim objSureScriptMessage As New gloSureScript.SureScriptMessage

                objSureScriptMessage.MessageID = eRxTable.Rows(eRxCount)("nMessageID")
                objSureScriptMessage.MessageName = eRxTable.Rows(eRxCount)("sMessageName")
                objSureScriptMessage.RelatesToMessageId = eRxTable.Rows(eRxCount)("sRelatesToMessageID")
                objSureScriptMessage.MessageFrom = eRxTable.Rows(eRxCount)("sMessageFrom")
                objSureScriptMessage.MessageTo = eRxTable.Rows(eRxCount)("sMessageTo")
                objSureScriptMessage.DateTimeStamp = eRxTable.Rows(eRxCount)("sDateTimeStamp")
                objSureScriptMessage.DateReceived = eRxTable.Rows(eRxCount)("dtDateReceived")
                'objSureScriptMessage.ReferenceNumber = eRxTable.Rows(eRxCount)("sReferenceNumber")
                'objSureScriptMessage.IsAlertCheck = eRxTable.Rows(eRxCount)("IsAlertCheck")

                _sDrugName = GetDrugName(_nPrescriptionID, strConnection)

                myService.Credentials = System.Net.CredentialCache.DefaultCredentials

                'If ds.Tables.Count > 0 Then
                '    myService.Url = ds.Tables(0).Rows(0)("sSettingsValue").ToString()
                'Else
                '    mdlGeneral.UpdateLog("Web service URL was not Set")
                'End If



                Dim cntResponse As Byte() = Nothing

                If ds.Tables(0).Rows.Count > 2 Then
                    myRxWCFService = ServiceClass.GetWCFSvc(ds.Tables(0).Rows(2)("sSettingsValue").ToString())
                    mdlGeneral.UpdateLog("Access to Webservice is successful on Staging Server", strConnection)
                    readbytes = myRxWCFService.PostClientMessage(data, "RxMessage")
                Else
                    mdlGeneral.UpdateLog("No eRx Web Service Settings Found ", strConnection)
                    Exit Sub
                End If

                Dim objgloSureScript As New gloSureScript.gloSureScriptInterface

                If readbytes IsNot Nothing Then
                    objgloSureScript.ReadStatusMessageRevised(readbytes, gloSureScriptInterface.SentMessageType.eNewRx, _sDrugName)
                    If objgloSureScript.StatusMessageType.Length > 0 Then
                        _eRxStatus = objgloSureScript.MessageName
                        _eRxStatusMessage = "" 'ogloInterface.StatusMessageType
                        If _eRxStatus <> "" Then

                            Select Case _eRxStatus
                                Case "Status"
                                    _eRxStatus = "Posted Sucessfully"
                                Case "Verify" 'for case verify we have same value in message variable as of status case
                                    _eRxStatus = "Posted Sucessfully"
                                Case "Error"
                                    _eRxStatus = "Could not be Posted Successfully"
                                    _eRxStatusMessage = objgloSureScript.StatusMessageType
                            End Select

                        End If
                    End If

                    'Dim oSurescriptDBLayer As New gloSureScript.gloSureScriptDBLayer
                    'ogloPrescription.DrugsCol.Item(0).TransactionID = ogloPrescription.DrugsCol.Item(0).PrescriptionID
                    'oSurescriptDBLayer.InsertintoMessageTransaction(CType(ogloPrescription.DrugsCol.Item(0), SureScriptMessage))
                    'Update the Prescription table with the updated values of eRxStatus against the rescpective PrescriptionID and NDC Code
                    'InsertMessageTransaction(objSureScriptMessage, strConnection)
                    UpdatePrescription(_eRxStatus, _eRxStatusMessage, _nPrescriptionID, strConnection) '

                    If _eRxStatus = "Posted Sucessfully" Then
                        DeleteFromeRxWithoutInternet(_nPrescriptionID, strConnection)
                    Else
                        ''Bug #69944: 00000715: Duplicate Task created with subject as "Send Rx" so condition added to generate task only once.
                        If eRxTable.Rows(eRxCount)("IsTaskGenerated") = 0 Then
                            AddTask(_nProviderD, _nPatientID, _nUserID, _nPrescriptionID, strConnection, _eRxStatusMessage)
                        End If
                        Dim _HitCount As Int16 = Convert.ToInt16(eRxTable.Rows(eRxCount)("nAttempts")) + 1
                        UpdateHitCountForeRxWithoutInternet(_nPrescriptionID, strConnection, _HitCount)
                    End If
                End If

                If Not IsNothing(objgloSureScript) Then
                    objgloSureScript.Dispose()
                    objgloSureScript = Nothing
                End If
                'objgloSureScript.e()
                eRxCount = (eRxCount + 1)

                'If Not IsNothing(ds) Then
                '    ds.Dispose()
                '    ds = Nothing
                'End If

            Loop
            If Not IsNothing(eRxTable) Then
                eRxTable.Dispose()
                eRxTable = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
            eRxCount = 0

            i = (i + 1)
        Loop
        i = 0


    End Sub


    Public Sub SendSecureMessageStagging()

        Dim i As Integer = 0
        Dim sureScriptTables As DataSet = Nothing
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim objSendEncryption As clsEncryption = Nothing
        Dim ds As DataSet = Nothing
        Dim cntResponse As Byte() = Nothing
        Dim objSecureMessage As SecureMessage = Nothing
        Dim lstAttachment As List(Of Attachment) = Nothing
        Dim AllAttachments As EnumerableRowCollection = Nothing
        Dim byteArray() As Byte = Nothing
        Dim xs As XmlSerializer = Nothing
        Dim fs As FileStream = Nothing
        Dim objN2N As N2NMessageType = Nothing
        Dim dtClinics As DataTable = Nothing
        Dim objGloSureScriptGeneral As New gloSureScript.gloSurescriptGeneral


        Try


            mdlGeneral.SetDbCredentials()
            objSendEncryption = New clsEncryption
            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)

                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

                ds = New DataSet
                oDB = New gloDatabaseLayer.DBLayer(strgloServiceConnection)
                oDB.Connect(False)

                If mdlGeneral.gblnStagingServer Then
                    oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('SECUREMSGSTAGINGWEBSERVICEURL') ORDER BY sSettingsName", ds)
                Else
                    oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('SECUREMSGPRODUCTIONWEBSERVICEURL') ORDER BY sSettingsName", ds)
                End If

                oDB.Disconnect()
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If


                'objGloSureScriptGeneral = New gloSureScript.gloSurescriptGeneral
                gloSureScript.gloSurescriptGeneral.DatabaseName = mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString
                gloSureScript.gloSurescriptGeneral.ServerName = mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString
                gloSureScript.gloSurescriptGeneral.sUserName = mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString
                gloSureScript.gloSurescriptGeneral.sPassword = strDbPassword
                gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = True

                'strConnection = "Integrated Security="";Persist Security Info=False;User ID=sa;Initial Catalog=glo7030_DEV;Data Source=GLOSVR02\SQL2008R2"
                sureScriptTables = RetrieveSecureMessageStagging(strConnection)
                If Not sureScriptTables Is Nothing Then
                    If sureScriptTables.Tables("MessagesDataTable").Rows.Count > 0 Then
                        Dim AttachmentListDataRowCollection As EnumerableRowCollection = Nothing

                        If sureScriptTables.Tables("AttachmentsDataTable").Rows.Count > 0 Then
                            AttachmentListDataRowCollection = sureScriptTables.Tables("AttachmentsDataTable").AsEnumerable()
                        End If

                        Dim sureScriptCount As Integer = 0

                        Do While (sureScriptCount < sureScriptTables.Tables("MessagesDataTable").Rows.Count)
                            objSecureMessage = PopulateSecureMessage(sureScriptTables.Tables("MessagesDataTable").Rows(sureScriptCount))

                            If objSecureMessage.noofAttachements > 0 Then
                                AllAttachments = From inner As DataRow In AttachmentListDataRowCollection
                                                 Where (Convert.ToInt64(inner.Field(Of Decimal)("nSecureMessageInboxID")) = objSecureMessage.secureMessageInboxID)
                                                 Select inner
                                lstAttachment = New List(Of Attachment)
                                For Each AttachmentElement As DataRow In AllAttachments
                                    lstAttachment.Add(PopulateAttachment(AttachmentElement))
                                Next
                            Else
                                lstAttachment = Nothing
                            End If

                            byteArray = GenerateXML(objSecureMessage, lstAttachment)
                            Dim Response As Byte() = Nothing
                            Dim key As String = String.Empty

                            dtClinics = Fill_SurescriptClinic(strConnection, objSecureMessage.senderID)

                            Dim clinicName As String = String.Empty
                            Dim siteID As String = String.Empty
                            Dim location As String = String.Empty
                            Dim ausID As String = String.Empty
                            Dim sSPIID As String = String.Empty

                            If dtClinics IsNot Nothing AndAlso dtClinics.Rows.Count > 0 Then
                                clinicName = dtClinics.Rows(0)("sClinicName")
                                siteID = dtClinics.Rows(0)("SiteID")
                                location = dtClinics.Rows(0)("Location")
                                ausID = dtClinics.Rows(0)("AUSID")
                                sSPIID = dtClinics.Rows(0)("SPID")
                            End If


                            'oDirect = gloSurescriptSecureMessage.SecureMessage.GetSecureMsgFSvc("http://localhost:1454/gloDirect.svc/Secure");
                            If Not IsNothing(mygloDirectService) Then
                                mygloDirectService.Close()
                                mygloDirectService = Nothing
                            End If
                            mygloDirectService = ServiceClass.GetSecureWCFSvc(ds.Tables(0).Rows(0)("sSettingsValue").ToString())

                            key = mygloDirectService.Login("gloSecureMsg@ophit.net", "spX12ss@!!21nasik")
                            Response = mygloDirectService.PostSecureMessage(objSecureMessage.messageID, objSecureMessage.From, objSecureMessage.[To], sSPIID, clinicName, ausID, siteID, location, byteArray, SecureMessageType.Send)

                            If Response IsNot Nothing Then
                                Dim strFileName As String = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
                                ConvertBinarytoFile(Response, strFileName)

                                If strFileName.Trim() <> "" Then
                                    xs = New XmlSerializer(GetType(N2NMessageType))
                                    fs = New FileStream(strFileName, FileMode.Open)
                                    Try
                                        objN2N = DirectCast(xs.Deserialize(fs), N2NMessageType)
                                    Catch ex As Exception
                                        mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
                                        mdlGeneral.UpdateLog("Sure Script Unable to process Message.", strConnection)
                                    End Try

                                    fs.Close()

                                    If Not IsNothing(fs) Then
                                        fs.Dispose()
                                        fs = Nothing
                                    End If

                                    If Not IsNothing(xs) Then
                                        xs = Nothing
                                    End If

                                    objSecureMessage = ExtractXML(objN2N, objSecureMessage)

                                    objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, strConnection)
                                Else
                                    UndoFlags(objSecureMessage.secureMessageInboxID, strConnection)
                                End If


                            Else
                                UndoFlags(objSecureMessage.secureMessageInboxID, strConnection)

                            End If
                            sureScriptCount = (sureScriptCount + 1)

                            If Not IsNothing(mygloDirectService) Then
                                mygloDirectService = Nothing
                            End If


                            If Not IsNothing(objSecureMessage) Then
                                objSecureMessage.Dispose()
                                objSecureMessage = Nothing
                            End If

                            Response = Nothing

                            If Not IsNothing(dtClinics) Then
                                dtClinics.Dispose()
                                dtClinics = Nothing
                            End If

                            byteArray = Nothing
                        Loop

                        sureScriptCount = 0
                    End If

                    If Not IsNothing(sureScriptTables) Then
                        sureScriptTables.Dispose()
                        sureScriptTables = Nothing
                    End If



                End If

                If Not IsNothing(objGloSureScriptGeneral) Then
                    objGloSureScriptGeneral = Nothing
                End If

                If Not IsNothing(mygloDirectService) Then
                    mygloDirectService.Close()
                    mygloDirectService = Nothing
                End If

                If Not IsNothing(ds) Then
                    ds.Dispose()
                    ds = Nothing
                End If

                i = (i + 1)
            Loop
            i = 0
            If Not IsNothing(objSendEncryption) Then
                objSendEncryption = Nothing
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)

        Finally

            If Not IsNothing(dtClinics) Then
                dtClinics.Dispose()
                dtClinics = Nothing
            End If

            If Not IsNothing(objN2N) Then
                objN2N = Nothing
            End If

            If Not IsNothing(fs) Then
                fs.Dispose()
                fs = Nothing
            End If

            If Not IsNothing(xs) Then
                xs = Nothing
            End If

            byteArray = Nothing

            If Not IsNothing(AllAttachments) Then
                AllAttachments = Nothing
            End If

            If Not IsNothing(lstAttachment) Then
                lstAttachment = Nothing
            End If

            If Not IsNothing(objSecureMessage) Then
                objSecureMessage = Nothing
            End If

            cntResponse = Nothing

            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            If Not IsNothing(objSendEncryption) Then
                objSendEncryption = Nothing
            End If

            cntResponse = Nothing

            If Not IsNothing(sureScriptTables) Then
                sureScriptTables.Dispose()
                sureScriptTables = Nothing
            End If

            If Not IsNothing(mygloDirectService) Then
                mygloDirectService.Close()
                mygloDirectService = Nothing
            End If
        End Try



    End Sub

    Public Sub SendPortalQueuedMessages()

        Dim oLsAttach As List(Of Attachment) = Nothing
        Dim objAttach As Attachment = Nothing
        Dim objSecureMessage As SecureMessage = Nothing
        Dim dsPatientQueueMessages As DataSet = Nothing
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim objSendEncryption As clsEncryption = Nothing
        Dim i As Integer = 0
        Dim ds As DataSet = Nothing
        Dim sCommDetailId = ""
        Try
            mdlGeneral.SetDbCredentials()
            objSendEncryption = New clsEncryption
            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)

                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword
                'strConnection = "SERVER=testing101;DATABASE=8000_glo;USER id=sa;Password=satesting101"

                dsPatientQueueMessages = RetrieveQueuedMessages(strConnection)

                If Not dsPatientQueueMessages Is Nothing Then

                    If dsPatientQueueMessages.Tables("MessagesDataTable").Rows.Count > 0 Then

                        mdlGeneral.UpdateLog("Retreiving Portal Queued Messages", strConnection)

                        Dim dtdate As DateTime
                        Dim Messagedescription As String = ""
                        Dim bIsSuccessful As Boolean = False

                        For countTo As Integer = 0 To dsPatientQueueMessages.Tables("MessagesDataTable").Rows.Count - 1

                            bIsSuccessful = False
                            sCommDetailId = ""

                            Dim fileName As String = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sFileName"))
                            sCommDetailId = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("nCommDetailID"))

                            If sCommDetailId <> "" Then
                                UpdatePortalQueuedMessage(Convert.ToInt64(sCommDetailId), "", 3, strConnection)
                            End If


                            If Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sDirectAddress")) <> "" Then

                                If Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sTo")) <> "" Then

                                    objSecureMessage = New SecureMessage()
                                    objSecureMessage.relateMessageID = ""
                                    objSecureMessage.version = "010"
                                    objSecureMessage.release = "006"
                                    objSecureMessage.highVersion = "010"
                                    objSecureMessage.senderID = Convert.ToInt64(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("nPatientProviderID"))
                                    objSecureMessage.receiverID = Convert.ToInt64(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("nReceiverId"))
                                    objSecureMessage.From = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sDirectAddress"))
                                    objSecureMessage.FromQualifier = "DIRECT"
                                    objSecureMessage.[To] = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sTo"))
                                    objSecureMessage.ToQualifier = "DIRECT"
                                    objSecureMessage.subject = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sCommSubject"))
                                    objSecureMessage.messageBody = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sCommMessage"))

                                    dtdate = Convert.ToDateTime(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("dCommCreationDate"))
                                    Dim strdate As String = dtdate.ToString("yyyy-MM-dd")
                                    Dim strtime As String = dtdate.ToString("hh:mm:ss")
                                    Dim strUTCFormat As String = strdate & "T" & strtime & ".0Z"

                                    objSecureMessage.dateTimeUTC = strUTCFormat
                                    objSecureMessage.dateTimeNormal = Convert.ToDateTime(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("dCommCreationDate"))
                                    objSecureMessage.isRead = 0
                                    objSecureMessage.patientID = Convert.ToInt64(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(0)("nPatientID"))

                                    If objSecureMessage.patientID > 0 Then

                                        Dim dtPatient As DataTable
                                        dtPatient = GetPatientDetailsforSecureMessage(objSecureMessage.patientID, strConnection)
                                        If dtPatient IsNot Nothing Then
                                            If dtPatient.Rows.Count > 0 Then
                                                objSecureMessage.firstName = Convert.ToString(dtPatient.Rows(0)("sfirstname"))
                                                objSecureMessage.lastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                                                objSecureMessage.Dob = Convert.ToString(dtPatient.Rows(0)("dob")).Trim()
                                                objSecureMessage.gender = Convert.ToString(dtPatient.Rows(0)("Gender"))
                                                objSecureMessage.zip = Convert.ToString(dtPatient.Rows(0)("sZIP"))
                                            End If
                                        End If

                                        If dtPatient IsNot Nothing Then
                                            dtPatient.Dispose()
                                            dtPatient = Nothing
                                        End If

                                    End If

                                    objSecureMessage.noofAttachements = 1
                                    objSecureMessage.MessageStatus = Convert.ToInt16(SecureMessageStatus.Send.GetHashCode())
                                    objSecureMessage.messageType = Convert.ToInt16(SecureMessageType.Send.GetHashCode())
                                    objSecureMessage.associated = 0
                                    objSecureMessage.deliveryStatusCode = ""
                                    objSecureMessage.deliveryStatusDescription = ""
                                    objSecureMessage.softwareVersion = System.Windows.Forms.Application.ProductVersion
                                    objSecureMessage.softwareProduct = System.Windows.Forms.Application.ProductName
                                    objSecureMessage.companyName = System.Windows.Forms.Application.CompanyName
                                    objSecureMessage.userName = "Admin"
                                    objSecureMessage.machineName = System.Environment.MachineName
                                    objSecureMessage.deleted = 0
                                    objSecureMessage.DocumentReferenceID = Convert.ToInt64(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("nCCDID"))
                                    objSecureMessage.sModuleName = "Patient Portal"
                                    objSecureMessage.DelegatedUser = "Admin"

                                    objSecureMessage.messageID = "aaaaabbbbbcccccdddddeeeeefffffggggghhhhh"

                                    oLsAttach = New List(Of Attachment)()

                                    If Not IsDBNull(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("iXMLData")) Then

                                        Dim oFileInfo As New FileInfo(fileName)

                                        If oFileInfo.Name.Length <= 35 Then

                                            If oFileInfo.Extension = ".xml" Then

                                                objAttach = New Attachment()
                                                Dim sFileExtension As String = String.Empty
                                                Dim Base64String As String = String.Empty

                                                Dim strDestFolderName As String = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
                                                strDestFolderName = strDestFolderName.Replace(".xml", ".zip")


                                                Dim strAttachmentFileName As String = ZipSourceFilePath & "\DirectCCDA.xml"
                                                System.IO.File.WriteAllBytes(strAttachmentFileName, dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("iXMLData"))

                                                Dim RootZipSourceFilePath As String = ZipSourceFilePath.Replace("\IHE_XDM", "")

                                                ZipMyFile(RootZipSourceFilePath, strDestFolderName)

                                                Base64String = Convert.ToBase64String(File.ReadAllBytes(strDestFolderName), Base64FormattingOptions.InsertLineBreaks)

                                                Dim oExtFileName As New FileInfo(strDestFolderName)

                                                Dim nFileExtension As Int16 = 0
                                                sFileExtension = oExtFileName.Extension.Replace(".", "")

                                                Dim sExtensionNames As [String]() = [Enum].GetNames(GetType(FileExtension))
                                                For iExtensionnames As Integer = 0 To sExtensionNames.Length - 1
                                                    If sFileExtension.ToLower().Trim() = Convert.ToString(sExtensionNames(iExtensionnames)) Then
                                                        nFileExtension = iExtensionnames
                                                        Exit For
                                                    End If
                                                Next

                                                objAttach.mimeType = GetMimeType(nFileExtension)
                                                objAttach.base64 = Base64String
                                                objAttach.moduleName = Convert.ToInt16(ModuleName.None.GetHashCode())
                                                objAttach.fileExtension = nFileExtension
                                                objAttach.documentName = oExtFileName.Name
                                                objAttach.iContent = ConvertFiletoBinary(strDestFolderName)
                                                objAttach.sCDAConfidentiality = ""
                                                oLsAttach.Add(objAttach)


                                                'Delete the Zip Files From Temp
                                                If File.Exists(strDestFolderName) = True Then
                                                    File.SetAttributes(strDestFolderName, FileAttributes.Normal)
                                                    File.Delete(strDestFolderName)
                                                End If

                                                'Delete the Xml Files From Temp
                                                If File.Exists(strAttachmentFileName) = True Then
                                                    File.SetAttributes(strAttachmentFileName, FileAttributes.Normal)
                                                    File.Delete(strAttachmentFileName)
                                                End If
                                            ElseIf oFileInfo.Extension = ".zip" Then

                                                '' To Implement Zip File 
                                                objAttach = New Attachment()
                                                Dim sFileExtension As String = String.Empty
                                                Dim Base64String As String = String.Empty
                                                Dim nFileExtension As Int16 = 2

                                                Base64String = Encoding.UTF8.GetString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("iXMLData"))
                                                objAttach.mimeType = GetMimeType(nFileExtension)
                                                objAttach.base64 = Base64String
                                                objAttach.moduleName = Convert.ToInt16(ModuleName.None.GetHashCode())
                                                objAttach.fileExtension = nFileExtension
                                                objAttach.documentName = oFileInfo.Name
                                                objAttach.iContent = dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("iXMLData")                                               
                                                objAttach.sCDAConfidentiality = ""
                                                oLsAttach.Add(objAttach)

                                            End If
                                        Else
                                            mdlGeneral.UpdateLog("Length of file Name should be less than 35 characters" & fileName, strConnection)
                                            objSecureMessage.deliveryStatusCode = ""
                                            objSecureMessage.deliveryStatusDescription = "Length of file Name should be less than 35 characters"
                                            Continue For
                                        End If
                                        If Not IsNothing(oFileInfo) Then
                                            oFileInfo = Nothing
                                        End If
                                        If Not IsNothing(objAttach) Then
                                            objAttach.Dispose()
                                            objAttach = Nothing
                                        End If
                                    Else
                                        objSecureMessage.noofAttachements = 0
                                    End If

                                    If CheckFinalFileLength(objSecureMessage, oLsAttach) Then

                                        objSecureMessage.messageID = Nothing

                                        If objSecureMessage.noofAttachements > 0 Then
                                            objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, oLsAttach, strConnection)
                                        Else
                                            objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, Nothing, strConnection)
                                        End If


                                        If Not String.IsNullOrEmpty(objSecureMessage.messageID) Then

                                            UpdateMessageIDForPortal(objSecureMessage.messageID, Convert.ToInt64(sCommDetailId), strConnection)

                                            Dim xs As XmlSerializer = Nothing
                                            Dim fs As FileStream = Nothing
                                            Dim objN2N As N2NMessageType = Nothing

                                            Try
                                                Dim byteArray As Byte() = GenerateXML(objSecureMessage, oLsAttach)
                                                Dim Response As Byte() = Nothing
                                                Dim key As String = String.Empty

                                                ds = New DataSet
                                                oDB = New gloDatabaseLayer.DBLayer(strgloServiceConnection)
                                                oDB.Connect(False)

                                                If mdlGeneral.gblnStagingServer Then
                                                    oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('SECUREMSGSTAGINGWEBSERVICEURL') ORDER BY sSettingsName", ds)
                                                Else
                                                    oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('SECUREMSGPRODUCTIONWEBSERVICEURL') ORDER BY sSettingsName", ds)
                                                End If

                                                oDB.Disconnect()
                                                If Not IsNothing(oDB) Then
                                                    oDB.Dispose()
                                                    oDB = Nothing
                                                End If

                                                Dim dtClinics As DataTable = Nothing

                                                dtClinics = Fill_SurescriptClinic(strConnection, objSecureMessage.senderID)

                                                Dim clinicName As String = String.Empty
                                                Dim siteID As String = String.Empty
                                                Dim location As String = String.Empty
                                                Dim ausID As String = String.Empty
                                                Dim sSPIID As String = String.Empty

                                                If dtClinics IsNot Nothing AndAlso dtClinics.Rows.Count > 0 Then
                                                    clinicName = dtClinics.Rows(0)("sClinicName")
                                                    siteID = dtClinics.Rows(0)("SiteID")
                                                    location = dtClinics.Rows(0)("Location")
                                                    ausID = dtClinics.Rows(0)("AUSID")
                                                    sSPIID = dtClinics.Rows(0)("SPID")
                                                End If
                                                If Not IsNothing(dtClinics) Then
                                                    dtClinics.Dispose()
                                                    dtClinics = Nothing
                                                End If
                                                If Not IsNothing(mygloDirectService) Then
                                                    mygloDirectService = Nothing
                                                End If
                                                mygloDirectService = ServiceClass.GetSecureWCFSvc(ds.Tables(0).Rows(0)("sSettingsValue").ToString())
                                                key = mygloDirectService.Login("gloSecureMsg@ophit.net", "spX12ss@!!21nasik")
                                                Response = mygloDirectService.PostSecureMessage(objSecureMessage.messageID, objSecureMessage.From, objSecureMessage.[To], sSPIID, clinicName, ausID, siteID, location, byteArray, SecureMessageType.Send)

                                                If Response IsNot Nothing Then

                                                    Dim strFileName As String = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
                                                    ConvertBinarytoFile(Response, strFileName)

                                                    If strFileName.Trim() <> "" Then
                                                        xs = New XmlSerializer(GetType(N2NMessageType))
                                                        fs = New FileStream(strFileName, FileMode.Open)
                                                        Try
                                                            objN2N = DirectCast(xs.Deserialize(fs), N2NMessageType)
                                                        Catch ex As Exception
                                                            mdlGeneral.UpdateLog("Sure Script Unable to process Message", strConnection)
                                                            objSecureMessage.deliveryStatusCode = ""
                                                            objSecureMessage.deliveryStatusDescription = "Sure Script Unable to process Message"
                                                        Finally

                                                            If Not IsNothing(fs) Then
                                                                fs.Close()
                                                                fs.Dispose()
                                                                fs = Nothing
                                                            End If
                                                            If Not IsNothing(xs) Then
                                                                xs = Nothing
                                                            End If
                                                        End Try



                                                        If objN2N IsNot Nothing Then

                                                            objSecureMessage = ExtractXML(objN2N, objSecureMessage)

                                                            If objSecureMessage.deliveryStatusDescription = "" AndAlso objSecureMessage.deliveryStatusCode = "010" Then
                                                                objSecureMessage.deliveryStatusDescription = "Clinical message delivered"
                                                                bIsSuccessful = True
                                                            ElseIf objSecureMessage.deliveryStatusDescription = "" AndAlso objSecureMessage.deliveryStatusCode = "000" Then
                                                                objSecureMessage.deliveryStatusDescription = "Clinical message sent to partner network"
                                                                bIsSuccessful = True
                                                            ElseIf objSecureMessage.deliveryStatusCode = "010" Then
                                                                bIsSuccessful = True
                                                            ElseIf objSecureMessage.deliveryStatusCode = "000" Then
                                                                bIsSuccessful = True
                                                            End If

                                                            objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, strConnection)

                                                            If objSecureMessage.deliveryStatusDescription <> "" Then
                                                                If Messagedescription <> "" Then
                                                                    If objSecureMessage.deliveryStatusDescription.Contains("NetworkAddress has an invalid format") OrElse objSecureMessage.deliveryStatusDescription.Contains("@") Then
                                                                        Messagedescription = Messagedescription + objSecureMessage.deliveryStatusDescription & vbLf & vbLf
                                                                    Else
                                                                        Messagedescription = (Messagedescription + objSecureMessage.From & vbLf & "     ") + objSecureMessage.deliveryStatusDescription & vbLf & vbLf
                                                                    End If
                                                                Else
                                                                    If objSecureMessage.deliveryStatusDescription.Contains("NetworkAddress has an invalid format") OrElse objSecureMessage.deliveryStatusDescription.Contains("@") Then
                                                                        Messagedescription = objSecureMessage.deliveryStatusDescription + vbLf & vbLf
                                                                    Else
                                                                        Messagedescription = objSecureMessage.From + vbLf & "    " + objSecureMessage.deliveryStatusDescription & vbLf & vbLf
                                                                    End If

                                                                End If

                                                            End If
                                                        Else

                                                            objSecureMessage.deliveryStatusCode = "999"
                                                            objSecureMessage.deliveryStatusDescription = "Processing"
                                                            If Messagedescription <> "" Then
                                                                Messagedescription = Messagedescription + objSecureMessage.[To] & vbLf & "     " & "Message sending is in queue" & vbLf & vbLf
                                                                bIsSuccessful = True
                                                            Else
                                                                Messagedescription = objSecureMessage.[To] + vbLf & "    " & "Message sending is in queue" & vbLf & vbLf
                                                                bIsSuccessful = True
                                                            End If

                                                            objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, strConnection)

                                                        End If

                                                    End If

                                                Else

                                                    objSecureMessage.deliveryStatusCode = "999"
                                                    objSecureMessage.deliveryStatusDescription = "Processing"
                                                    If Messagedescription <> "" Then
                                                        Messagedescription = Messagedescription + objSecureMessage.[To] & vbLf & "     " & "Message sending is in queue" & vbLf & vbLf
                                                        bIsSuccessful = True
                                                    Else
                                                        Messagedescription = objSecureMessage.[To] + vbLf & "    " & "Message sending is in queue" & vbLf & vbLf
                                                        bIsSuccessful = True
                                                    End If
                                                    objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, strConnection)
                                                End If
                                                mdlGeneral.UpdateLog(Messagedescription & " For " & sCommDetailId, strConnection)
                                                mygloDirectService.Close()
                                                mygloDirectService = Nothing

                                            Catch ex As Exception

                                                objSecureMessage.deliveryStatusCode = "999"
                                                objSecureMessage.deliveryStatusDescription = "Processing"
                                                If Messagedescription <> "" Then
                                                    Messagedescription = Messagedescription + objSecureMessage.[To] & vbLf & "     " & "Message sending is in queue" & vbLf & vbLf
                                                    bIsSuccessful = True
                                                Else
                                                    Messagedescription = objSecureMessage.[To] + vbLf & "    " & "Message sending is in queue" & vbLf & vbLf
                                                    bIsSuccessful = True
                                                End If
                                                objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, strConnection)
                                                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.[Default]
                                                mdlGeneral.UpdateLog(ex.ToString(), strConnection)

                                            Finally

                                                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.[Default]
                                                If objN2N IsNot Nothing Then
                                                    objN2N = Nothing
                                                End If
                                                If fs IsNot Nothing Then
                                                    fs.Close()
                                                    fs.Dispose()
                                                    fs = Nothing
                                                End If
                                                If xs IsNot Nothing Then
                                                    xs = Nothing
                                                End If

                                            End Try
                                        End If
                                    Else
                                        mdlGeneral.UpdateLog("Message size exceeded more than 20MB. Please remove some content and try again", strConnection)
                                        objSecureMessage = New SecureMessage()
                                        objSecureMessage.deliveryStatusCode = ""
                                        objSecureMessage.deliveryStatusDescription = "Message size exceeded more than 20MB. Please remove some content and try again"
                                        Continue For
                                    End If
                                Else
                                    mdlGeneral.UpdateLog("To Address Missing For CommDetailID :" & sCommDetailId, strConnection)
                                    objSecureMessage = New SecureMessage()
                                    objSecureMessage.deliveryStatusCode = ""
                                    objSecureMessage.deliveryStatusDescription = "To Address Missing"
                                End If
                            Else
                                mdlGeneral.UpdateLog("From Address Missing For CommDetailID :" & sCommDetailId, strConnection)
                                objSecureMessage = New SecureMessage()
                                objSecureMessage.deliveryStatusCode = ""
                                objSecureMessage.deliveryStatusDescription = "From Address Missing"
                            End If

                            Dim sStatusMessage = ""
                            If objSecureMessage.deliveryStatusCode <> "" Then
                                sStatusMessage = objSecureMessage.deliveryStatusCode & " - " & objSecureMessage.deliveryStatusDescription
                            Else
                                sStatusMessage = objSecureMessage.deliveryStatusDescription
                            End If

                            If bIsSuccessful = True Then
                                UpdatePortalQueuedMessage(Convert.ToInt64(sCommDetailId), sStatusMessage, 1, strConnection)
                            Else
                                UpdatePortalQueuedMessage(Convert.ToInt64(sCommDetailId), sStatusMessage, 2, strConnection)
                            End If

                        Next

                        If Messagedescription <> "" Then
                            mdlGeneral.UpdateLog(Messagedescription, strConnection)
                        End If

                        objSecureMessage = Nothing
                        oLsAttach = Nothing

                    End If

                End If
                If Not IsNothing(mygloDirectService) Then
                    mygloDirectService.Close()
                    mygloDirectService = Nothing
                End If
                i = (i + 1)
            Loop
            i = 0

        Catch ex As Exception

            If sCommDetailId <> "" Then
                UpdatePortalQueuedMessage(Convert.ToInt64(sCommDetailId), "", 0, strConnection)
            End If

            mdlGeneral.UpdateLog(ex.ToString(), strConnection)
        Finally
            If dsPatientQueueMessages IsNot Nothing Then
                dsPatientQueueMessages.Dispose()
                dsPatientQueueMessages = Nothing
            End If
            If objSendEncryption IsNot Nothing Then
                objSendEncryption = Nothing
            End If
            If objSecureMessage IsNot Nothing Then
                objSecureMessage.Dispose()
                objSecureMessage = Nothing
            End If
            If objAttach IsNot Nothing Then
                objAttach.Dispose()
                objAttach = Nothing
            End If
            If oLsAttach IsNot Nothing Then
                oLsAttach = Nothing
            End If

        End Try


    End Sub

    Public Sub SendPatientSavingDispositionMessages(ByVal sMessageID As String, ByVal sFilePath As String, ByVal npatientID As Long, ByVal strconnection As String)

        Dim oLsAttach As List(Of Attachment) = Nothing
        Dim objAttach As Attachment = Nothing
        Dim objSecureMessage As SecureMessage = Nothing
        Dim dsPatientQueueMessages As DataSet = Nothing
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim objSendEncryption As clsEncryption = Nothing
        Dim i As Integer = 0
        Dim ds As DataSet = Nothing
        Dim sCommDetailId = ""
        Try
            mdlGeneral.SetDbCredentials()
            objSendEncryption = New clsEncryption
            'Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)

            'Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
            'Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword
            'strConnection = "SERVER=glosvr02\sql2008r2;DATABASE=glo8000_dev;USER id=sa;Password=saglosvr02"

            dsPatientQueueMessages = GetSecureMessageDetailsUsingMessageID(sMessageID, strConnection)

            If Not dsPatientQueueMessages Is Nothing Then

                If dsPatientQueueMessages.Tables("MessagesDataTable").Rows.Count > 0 Then

                    mdlGeneral.UpdateLog("Retreiving Pending Pating Saving Messages", strconnection)

                    Dim dtdate As DateTime
                    Dim Messagedescription As String = ""
                    Dim bIsSuccessful As Boolean = False

                    For countTo As Integer = 0 To dsPatientQueueMessages.Tables("MessagesDataTable").Rows.Count - 1

                        Dim fileName As String = sFilePath

                        objSecureMessage = New SecureMessage()
                        objSecureMessage.relateMessageID = sMessageID
                        objSecureMessage.version = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(0)("sMessageVersionNo"))
                        objSecureMessage.release = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(0)("sMessageReleaseNo"))
                        objSecureMessage.highVersion = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(0)("sMessageHighestVersion"))
                        objSecureMessage.senderID = Convert.ToInt64(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("nsenderID"))
                        objSecureMessage.receiverID = Convert.ToInt64(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("nReceiverID"))
                        objSecureMessage.From = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sTo"))
                        objSecureMessage.FromQualifier = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sFromQualifier"))
                        objSecureMessage.[To] = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sFrom"))
                        objSecureMessage.ToQualifier = Convert.ToString(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("sToQualifier"))
                        objSecureMessage.subject = "Opportunity Disposition"
                        objSecureMessage.messageBody = "Please find attachment for Opportunity Disposition"

                        dtdate = System.DateTime.UtcNow
                        Dim strdate As String = dtdate.ToString("yyyy-MM-dd")
                        Dim strtime As String = dtdate.ToString("hh:mm:ss")
                        Dim strUTCFormat As String = strdate & "T" & strtime & ".0Z"

                        objSecureMessage.dateTimeUTC = strUTCFormat
                        globalCurrentDate = gloGlobal.gloTimeZone.getgloDateTime(gloGlobal.gloTimeZone.getLocalTimeZoneId(strconnection))
                        objSecureMessage.dateTimeNormal = globalCurrentDate
                        objSecureMessage.isRead = 0
                        objSecureMessage.patientID = npatientID

                        If objSecureMessage.patientID > 0 Then

                            Dim dtPatient As DataTable
                            dtPatient = GetPatientDetailsforSecureMessage(objSecureMessage.patientID, strConnection)
                            If dtPatient IsNot Nothing Then
                                If dtPatient.Rows.Count > 0 Then
                                    objSecureMessage.firstName = Convert.ToString(dtPatient.Rows(0)("sfirstname"))
                                    objSecureMessage.lastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                                    objSecureMessage.Dob = Convert.ToString(dtPatient.Rows(0)("dob")).Trim()
                                    objSecureMessage.gender = Convert.ToString(dtPatient.Rows(0)("Gender"))
                                    objSecureMessage.zip = Convert.ToString(dtPatient.Rows(0)("sZIP"))
                                End If
                            End If

                            If dtPatient IsNot Nothing Then
                                dtPatient.Dispose()
                                dtPatient = Nothing
                            End If

                        End If

                        objSecureMessage.noofAttachements = 1
                        objSecureMessage.MessageStatus = Convert.ToInt16(SecureMessageStatus.Send.GetHashCode())
                        objSecureMessage.messageType = Convert.ToInt16(SecureMessageType.Send.GetHashCode())
                        objSecureMessage.associated = 0
                        objSecureMessage.deliveryStatusCode = ""
                        objSecureMessage.deliveryStatusDescription = ""
                        objSecureMessage.softwareVersion = System.Windows.Forms.Application.ProductVersion
                        objSecureMessage.softwareProduct = System.Windows.Forms.Application.ProductName
                        objSecureMessage.companyName = System.Windows.Forms.Application.CompanyName
                        objSecureMessage.userName = "Admin"
                        objSecureMessage.machineName = System.Environment.MachineName
                        objSecureMessage.deleted = 0
                        objSecureMessage.DocumentReferenceID = 0
                        objSecureMessage.sModuleName = "Rx Savings"
                        objSecureMessage.DelegatedUser = "Admin"
                        objSecureMessage.messageID = "aaaaabbbbbcccccdddddeeeeefffffggggghhhhh"
                        'Add use case
                        objSecureMessage.UseCase = 1
                        oLsAttach = New List(Of Attachment)()

                        'If Not IsDBNull(dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("iXMLData")) Then

                        Dim oFileInfo As New FileInfo(fileName)

                        If oFileInfo.Name.Length <= 35 Then

                            objAttach = New Attachment()
                            Dim sFileExtension As String = String.Empty
                            Dim Base64String As String = String.Empty

                            'Dim strDestFolderName As String = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
                            'strDestFolderName = strDestFolderName.Replace(".xml", ".zip")

                            Dim strAttachmentFileName As String = fileName
                            'System.IO.File.WriteAllBytes(strAttachmentFileName, dsPatientQueueMessages.Tables("MessagesDataTable").Rows(countTo)("iXMLData"))

                            'Dim strCCDCssFilePath = Application.StartupPath & "\gloCCDAcss_MU2.xsl"

                            'ZipMyFile(strAttachmentFileName, strCCDCssFilePath, strDestFolderName)

                            Base64String = Convert.ToBase64String(File.ReadAllBytes(fileName), Base64FormattingOptions.InsertLineBreaks)
                            'Base64String = Convert.ToBase64String(AttachmentElement("iXMLData"), Base64FormattingOptions.InsertLineBreaks)

                            Dim oExtFileName As New FileInfo(fileName)

                            Dim nFileExtension As Int16 = 0
                            sFileExtension = oExtFileName.Extension.Replace(".", "")

                            Dim sExtensionNames As [String]() = [Enum].GetNames(GetType(FileExtension))
                            For iExtensionnames As Integer = 0 To sExtensionNames.Length - 1
                                If sFileExtension.ToLower().Trim() = Convert.ToString(sExtensionNames(iExtensionnames)) Then
                                    nFileExtension = iExtensionnames
                                    Exit For
                                End If
                            Next

                            objAttach.mimeType = GetMimeType(nFileExtension)
                            objAttach.base64 = Base64String
                            objAttach.moduleName = Convert.ToInt16(ModuleName.None.GetHashCode())
                            objAttach.fileExtension = nFileExtension
                            objAttach.documentName = oExtFileName.Name
                            objAttach.iContent = Convert.FromBase64String(Base64String)
                            objAttach.sCDAConfidentiality = ""
                            oLsAttach.Add(objAttach)

                            'Delete the Zip Files From Temp
                            'If File.Exists(strDestFolderName) = True Then
                            '    File.SetAttributes(strDestFolderName, FileAttributes.Normal)
                            '    File.Delete(strDestFolderName)
                            'End If

                            'Delete the Xml Files From Temp
                            If File.Exists(strAttachmentFileName) = True Then
                                File.SetAttributes(strAttachmentFileName, FileAttributes.Normal)
                                File.Delete(strAttachmentFileName)
                            End If
                            If Not IsNothing(oExtFileName) Then
                                oExtFileName = Nothing
                            End If
                        Else
                            mdlGeneral.UpdateLog("Length of file Name should be less than 35 characters" & fileName, strconnection)
                            objSecureMessage.deliveryStatusCode = ""
                            objSecureMessage.deliveryStatusDescription = "Length of file Name should be less than 35 characters"
                            Continue For
                        End If
                        If Not IsNothing(oFileInfo) Then
                            oFileInfo = Nothing
                        End If
                        If Not IsNothing(objAttach) Then
                            objAttach.Dispose()
                            objAttach = Nothing
                        End If
                        'Else
                        'objSecureMessage.noofAttachements = 0
                        'End If

                        If CheckFinalFileLength(objSecureMessage, oLsAttach) Then

                            objSecureMessage.messageID = Nothing

                            If objSecureMessage.noofAttachements > 0 Then
                                objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, oLsAttach, strConnection)
                            Else
                                objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, Nothing, strConnection)
                            End If


                            If Not String.IsNullOrEmpty(objSecureMessage.messageID) Then

                                Dim xs As XmlSerializer = Nothing
                                Dim fs As FileStream = Nothing
                                Dim objN2N As N2NMessageType = Nothing

                                Try
                                    Dim byteArray As Byte() = GenerateXML(objSecureMessage, oLsAttach)
                                    Dim Response As Byte() = Nothing
                                    Dim key As String = String.Empty

                                    ds = New DataSet
                                    oDB = New gloDatabaseLayer.DBLayer(strgloServiceConnection)
                                    oDB.Connect(False)

                                    If mdlGeneral.gblnStagingServer Then
                                        oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('SECUREMSGSTAGINGWEBSERVICEURL') ORDER BY sSettingsName", ds)
                                    Else
                                        oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('SECUREMSGPRODUCTIONWEBSERVICEURL') ORDER BY sSettingsName", ds)
                                    End If

                                    oDB.Disconnect()
                                    If Not IsNothing(oDB) Then
                                        oDB.Dispose()
                                        oDB = Nothing
                                    End If

                                    Dim dtClinics As DataTable = Nothing

                                    dtClinics = Fill_SurescriptClinic(strConnection, objSecureMessage.senderID)

                                    Dim clinicName As String = String.Empty
                                    Dim siteID As String = String.Empty
                                    Dim location As String = String.Empty
                                    Dim ausID As String = String.Empty
                                    Dim sSPIID As String = String.Empty

                                    If dtClinics IsNot Nothing AndAlso dtClinics.Rows.Count > 0 Then
                                        clinicName = dtClinics.Rows(0)("sClinicName")
                                        siteID = dtClinics.Rows(0)("SiteID")
                                        location = dtClinics.Rows(0)("Location")
                                        ausID = dtClinics.Rows(0)("AUSID")
                                        sSPIID = dtClinics.Rows(0)("SPID")
                                    End If

                                    If Not IsNothing(dtClinics) Then
                                        dtClinics.Dispose()
                                        dtClinics = Nothing
                                    End If

                                    mygloDirectService = ServiceClass.GetSecureWCFSvc(ds.Tables(0).Rows(0)("sSettingsValue").ToString())
                                    key = mygloDirectService.Login("gloSecureMsg@ophit.net", "spX12ss@!!21nasik")
                                    Response = mygloDirectService.PostSecureMessage(objSecureMessage.messageID, objSecureMessage.From, objSecureMessage.[To], sSPIID, clinicName, ausID, siteID, location, byteArray, SecureMessageType.Send)

                                    If Response IsNot Nothing Then

                                        Dim strFileName As String = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
                                        ConvertBinarytoFile(Response, strFileName)

                                        If strFileName.Trim() <> "" Then
                                            xs = New XmlSerializer(GetType(N2NMessageType))
                                            fs = New FileStream(strFileName, FileMode.Open)
                                            Try
                                                objN2N = DirectCast(xs.Deserialize(fs), N2NMessageType)
                                            Catch ex As Exception
                                                mdlGeneral.UpdateLog("Sure Script Unable to process Message", strconnection)
                                                objSecureMessage.deliveryStatusCode = ""
                                                objSecureMessage.deliveryStatusDescription = "Sure Script Unable to process Message"
                                            End Try
                                            fs.Close()


                                            If objN2N IsNot Nothing Then

                                                objSecureMessage = ExtractXML(objN2N, objSecureMessage)

                                                If objSecureMessage.deliveryStatusDescription = "" AndAlso objSecureMessage.deliveryStatusCode = "010" Then
                                                    objSecureMessage.deliveryStatusDescription = "Clinical message delivered"
                                                    bIsSuccessful = True
                                                ElseIf objSecureMessage.deliveryStatusDescription = "" AndAlso objSecureMessage.deliveryStatusCode = "000" Then
                                                    objSecureMessage.deliveryStatusDescription = "Clinical message sent to partner network"
                                                    bIsSuccessful = True
                                                ElseIf objSecureMessage.deliveryStatusCode = "010" Then
                                                    bIsSuccessful = True
                                                ElseIf objSecureMessage.deliveryStatusCode = "000" Then
                                                    bIsSuccessful = True
                                                End If

                                                objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, strConnection)

                                                If objSecureMessage.deliveryStatusDescription <> "" Then
                                                    If Messagedescription <> "" Then
                                                        If objSecureMessage.deliveryStatusDescription.Contains("NetworkAddress has an invalid format") OrElse objSecureMessage.deliveryStatusDescription.Contains("@") Then
                                                            Messagedescription = Messagedescription + objSecureMessage.deliveryStatusDescription & vbLf & vbLf
                                                        Else
                                                            Messagedescription = (Messagedescription + objSecureMessage.From & vbLf & "     ") + objSecureMessage.deliveryStatusDescription & vbLf & vbLf
                                                        End If
                                                    Else
                                                        If objSecureMessage.deliveryStatusDescription.Contains("NetworkAddress has an invalid format") OrElse objSecureMessage.deliveryStatusDescription.Contains("@") Then
                                                            Messagedescription = objSecureMessage.deliveryStatusDescription + vbLf & vbLf
                                                        Else
                                                            Messagedescription = objSecureMessage.From + vbLf & "    " + objSecureMessage.deliveryStatusDescription & vbLf & vbLf
                                                        End If

                                                    End If

                                                End If
                                            Else

                                                objSecureMessage.deliveryStatusCode = "999"
                                                objSecureMessage.deliveryStatusDescription = "Processing"
                                                If Messagedescription <> "" Then
                                                    Messagedescription = Messagedescription + objSecureMessage.[To] & vbLf & "     " & "Message sending is in queue" & vbLf & vbLf
                                                    bIsSuccessful = True
                                                Else
                                                    Messagedescription = objSecureMessage.[To] + vbLf & "    " & "Message sending is in queue" & vbLf & vbLf
                                                    bIsSuccessful = True
                                                End If

                                                objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, strConnection)

                                            End If

                                        End If

                                    Else

                                        objSecureMessage.deliveryStatusCode = "999"
                                        objSecureMessage.deliveryStatusDescription = "Processing"
                                        If Messagedescription <> "" Then
                                            Messagedescription = Messagedescription + objSecureMessage.[To] & vbLf & "     " & "Message sending is in queue" & vbLf & vbLf
                                            bIsSuccessful = True
                                        Else
                                            Messagedescription = objSecureMessage.[To] + vbLf & "    " & "Message sending is in queue" & vbLf & vbLf
                                            bIsSuccessful = True
                                        End If
                                        objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, strConnection)
                                    End If

                                    mygloDirectService.Close()
                                    mygloDirectService = Nothing

                                Catch ex As Exception

                                    objSecureMessage.deliveryStatusCode = "999"
                                    objSecureMessage.deliveryStatusDescription = "Processing"
                                    If Messagedescription <> "" Then
                                        Messagedescription = Messagedescription + objSecureMessage.[To] & vbLf & "     " & "Message sending is in queue" & vbLf & vbLf
                                        bIsSuccessful = True
                                    Else
                                        Messagedescription = objSecureMessage.[To] + vbLf & "    " & "Message sending is in queue" & vbLf & vbLf
                                        bIsSuccessful = True
                                    End If
                                    objSecureMessage.messageID = InsertSureScriptMessageInDB(objSecureMessage, strConnection)
                                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.[Default]
                                    mdlGeneral.UpdateLog(ex.ToString(), strconnection)

                                Finally

                                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.[Default]
                                    If objN2N IsNot Nothing Then
                                        objN2N = Nothing
                                    End If
                                    If fs IsNot Nothing Then
                                        fs.Close()
                                        fs.Dispose()
                                        fs = Nothing
                                    End If
                                    If xs IsNot Nothing Then
                                        xs = Nothing
                                    End If

                                End Try
                            End If
                        Else
                            mdlGeneral.UpdateLog("Message size exceeded more than 20MB. Please remove some content and try again", strconnection)
                            objSecureMessage = New SecureMessage()
                            objSecureMessage.deliveryStatusCode = ""
                            objSecureMessage.deliveryStatusDescription = "Message size exceeded more than 20MB. Please remove some content and try again"
                            Continue For
                        End If


                        Dim sStatusMessage = ""
                        If objSecureMessage.deliveryStatusCode <> "" Then
                            sStatusMessage = objSecureMessage.deliveryStatusCode & " - " & objSecureMessage.deliveryStatusDescription
                        Else
                            sStatusMessage = objSecureMessage.deliveryStatusDescription
                        End If



                    Next

                    If Messagedescription <> "" Then
                        mdlGeneral.UpdateLog(Messagedescription, strconnection)
                    End If

                    objSecureMessage = Nothing
                    oLsAttach = Nothing

                End If

            End If

            '    i = (i + 1)
            'Loop
            'i = 0

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString(), strconnection)
        Finally

            If dsPatientQueueMessages IsNot Nothing Then
                dsPatientQueueMessages.Dispose()
                dsPatientQueueMessages = Nothing
            End If
            If objSendEncryption IsNot Nothing Then
                objSendEncryption = Nothing
            End If
            If objSecureMessage IsNot Nothing Then
                objSecureMessage.Dispose()
                objSecureMessage = Nothing
            End If
            If objAttach IsNot Nothing Then
                objAttach.Dispose()
                objAttach = Nothing
            End If
            If oLsAttach IsNot Nothing Then
                oLsAttach = Nothing
            End If

        End Try


    End Sub

    Public Shared Sub ZipMyFile(strAttachmentFileName As String, strDestFolderName As String)

        Try
            Using zipFile As New ZipFile()
                zipFile.CompressionLevel = 0
                zipFile.AddItem(strAttachmentFileName)
                zipFile.Save(strDestFolderName)

            End Using
        Catch zipEx As ZipException
            mdlGeneral.UpdateLog(zipEx.ToString())
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString())
        End Try
    End Sub

    Public Function GetPatientDetailsforSecureMessage(PatientID As Long, ByVal strConnection As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(strConnection)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)
            oDBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("GetPatientDetailsforSecureMessage", oDBParameters, dt)
            oDB.Disconnect()

            Return dt
        Catch ex As Exception
            dt = Nothing
            Return Nothing
        Finally

            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If

            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Function

    Public Function GetStatusForPatientSavingsUsingQueueID(ByVal QueueID As Long, ByVal strConnection As String) As String
        Dim strStatus As String = String.Empty
        Dim oDB As New gloDatabaseLayer.DBLayer(strConnection)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)
            oDBParameters.Add("@QueueID", QueueID, ParameterDirection.Input, SqlDbType.BigInt)
            strStatus = oDB.ExecuteScalar("GetStatusForPatientSavingsUsingQueueID", oDBParameters)
            oDB.Disconnect()

            Return strStatus
        Catch ex As Exception
            Return strStatus
        Finally

            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Private Function CheckFinalFileLength(objSecureMessage As SecureMessage, oLsAttach As List(Of Attachment)) As Boolean
        Dim NewN2NMessage As N2NMessageType = Nothing
        Dim isValid As Boolean = False
        Try
            NewN2NMessage = CreateN2NMessage(objSecureMessage, oLsAttach)
            isValid = CheckAndGenerateXML(NewN2NMessage)
            Return isValid
        Catch ex As Exception
            Return isValid
        Finally
            If NewN2NMessage IsNot Nothing Then
                NewN2NMessage = Nothing
            End If
        End Try

    End Function

    Private Function CheckAndGenerateXML(message As N2NMessageType) As Boolean

        Dim xs As XmlSerializer = Nothing
        Dim fs As FileStream = Nothing
        Dim fileLength As Double = 0.0
        Dim isVaild As Boolean = False
        Dim ofileInfo As FileInfo = Nothing
        Try
            Dim strFileName As String = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
            xs = New XmlSerializer(GetType(N2NMessageType))
            fs = New FileStream(strFileName, FileMode.Create)
            xs.Serialize(fs, message)
            fs.Close()

            ofileInfo = New FileInfo(strFileName)
            fileLength = Convert.ToDouble(ofileInfo.Length)

            isVaild = CalFileSize(fileLength)

            Return isVaild
        Catch ex As Exception
            Return isVaild
        Finally
            If Not IsNothing(ofileInfo) Then
                ofileInfo = Nothing
            End If
            If fs IsNot Nothing Then
                fs.Dispose()
                fs = Nothing
            End If
            If xs IsNot Nothing Then
                xs = Nothing
            End If
        End Try

    End Function

    Private Function CalFileSize(Length As Double) As Boolean
        Dim fileLength As Double = 0.0

        If Length > 1024 Then
            fileLength = Length / 1024
        Else
            fileLength = (Length / 1024.0F) / 1024.0F
            fileLength = Math.Round(fileLength, 4)
        End If

        If fileLength >= 20480 Then
            ' 20.0 mb
            Return False
        Else
            Return True
        End If

    End Function

    Private Function GetMessageParameters(secureMessage As SecureMessage) As DBParameters
        Dim localDBParameters As DBParameters = Nothing

        Try

            localDBParameters = New DBParameters()

            localDBParameters.Add(New DBParameter("@sSecureMessageID", secureMessage.messageID, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sRelatesToMessageID", secureMessage.relateMessageID, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sMessageVersionNo", secureMessage.version, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sMessageReleaseNo", secureMessage.release, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sMessageHighestVersion", secureMessage.highVersion, ParameterDirection.Input, SqlDbType.VarChar))

            localDBParameters.Add(New DBParameter("@nSenderID", secureMessage.senderID, ParameterDirection.Input, SqlDbType.BigInt))
            localDBParameters.Add(New DBParameter("@nReceiverID", secureMessage.receiverID, ParameterDirection.Input, SqlDbType.BigInt))
            localDBParameters.Add(New DBParameter("@sFrom", secureMessage.From, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sFromQualifier", secureMessage.FromQualifier, ParameterDirection.Input, SqlDbType.VarChar))

            localDBParameters.Add(New DBParameter("@sTo", secureMessage.[To], ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sToQualifier", secureMessage.ToQualifier, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sSubject", secureMessage.subject, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sMessageBody", secureMessage.messageBody, ParameterDirection.Input, SqlDbType.VarChar))

            localDBParameters.Add(New DBParameter("@dtSendReceiveDateTime_UTC", secureMessage.dateTimeUTC, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@dtSendReceiveDateTime", secureMessage.dateTimeNormal, ParameterDirection.Input, SqlDbType.DateTime))
            localDBParameters.Add(New DBParameter("@bIsRead", secureMessage.isRead, ParameterDirection.Input, SqlDbType.Bit))
            localDBParameters.Add(New DBParameter("@nPatientID", secureMessage.patientID, ParameterDirection.Input, SqlDbType.BigInt))

            localDBParameters.Add(New DBParameter("@nNoOfAttachments", secureMessage.noofAttachements, ParameterDirection.Input, SqlDbType.Int))
            localDBParameters.Add(New DBParameter("@bMessageStatus", secureMessage.MessageStatus, ParameterDirection.Input, SqlDbType.Int))
            localDBParameters.Add(New DBParameter("@bMessageType", secureMessage.messageType, ParameterDirection.Input, SqlDbType.Int))
            localDBParameters.Add(New DBParameter("@bIsAssociated", secureMessage.associated, ParameterDirection.Input, SqlDbType.Bit))

            localDBParameters.Add(New DBParameter("@sDeliveryStatusCode", secureMessage.deliveryStatusCode, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sDeliveryStatusDescription", secureMessage.deliveryStatusDescription, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sSoftwareVersion", secureMessage.softwareVersion, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sSoftwareProduct", secureMessage.softwareProduct, ParameterDirection.Input, SqlDbType.VarChar))

            localDBParameters.Add(New DBParameter("@sCompanyName", secureMessage.companyName, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sUserName", secureMessage.userName, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@sMachineName", secureMessage.machineName, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@bIsDeleted", secureMessage.deleted, ParameterDirection.Input, SqlDbType.Bit))
            localDBParameters.Add(New DBParameter("@sModuleName", secureMessage.sModuleName, ParameterDirection.Input, SqlDbType.VarChar))
            localDBParameters.Add(New DBParameter("@nUseCase", secureMessage.UseCase, ParameterDirection.Input, SqlDbType.Int))
            localDBParameters.Add(New DBParameter("@sDelegatedUser", secureMessage.DelegatedUser, ParameterDirection.Input, SqlDbType.VarChar))

            Return localDBParameters

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString)
            Return Nothing
        Finally




        End Try



        Return localDBParameters
    End Function

    Private Function GetAttachmentDataTable(AttachmentList As List(Of Attachment)) As DataTable
        Dim TVPAttachmentDataTable As DataTable = Nothing

        Try
            TVPAttachmentDataTable = New DataTable("TVPAttachmentDataTable")
            TVPAttachmentDataTable.Columns.Add(New DataColumn("nSecureMessageInboxID", System.Type.[GetType]("System.Int64")))
            TVPAttachmentDataTable.Columns.Add(New DataColumn("nAttachmentID", System.Type.[GetType]("System.Int64")))
            TVPAttachmentDataTable.Columns.Add(New DataColumn("nModuleName", System.Type.[GetType]("System.Int64")))

            TVPAttachmentDataTable.Columns.Add(New DataColumn("nFileExtension", System.Type.[GetType]("System.Int64")))
            TVPAttachmentDataTable.Columns.Add(New DataColumn("sDocumentName", System.Type.[GetType]("System.String")))
            TVPAttachmentDataTable.Columns.Add(New DataColumn("iContent", System.Type.[GetType]("System.Byte[]")))
            TVPAttachmentDataTable.Columns.Add(New DataColumn("sCDAConfidentiality", System.Type.[GetType]("System.String")))

            For Each attachmentInList As Attachment In AttachmentList

                Dim TVPAttachmentDataRow As DataRow = TVPAttachmentDataTable.NewRow()

                TVPAttachmentDataRow("nSecureMessageInboxID") = attachmentInList.nSecureMessageInboxID
                TVPAttachmentDataRow("nAttachmentID") = attachmentInList.attachmentID
                TVPAttachmentDataRow("nModuleName") = attachmentInList.moduleName

                TVPAttachmentDataRow("nFileExtension") = attachmentInList.fileExtension
                TVPAttachmentDataRow("sDocumentName") = attachmentInList.documentName
                TVPAttachmentDataRow("iContent") = attachmentInList.iContent

                TVPAttachmentDataRow("sCDAConfidentiality") = ""
                TVPAttachmentDataRow("sCDAConfidentiality") = attachmentInList.sCDAConfidentiality

                TVPAttachmentDataTable.Rows.Add(TVPAttachmentDataRow)
                TVPAttachmentDataRow = Nothing
            Next

            Return TVPAttachmentDataTable

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString)
            Return TVPAttachmentDataTable
        Finally



        End Try


        Return TVPAttachmentDataTable
    End Function

    Private Function SerializeMessage(ByVal DBParameters As DBParameters, ByVal strconnection As String) As String
        Return SerializeMessage(DBParameters, Nothing, strconnection)
    End Function

    Private Function SerializeMessage(ByVal DBParameters As DBParameters, ByVal TVPDataTable As DataTable, ByVal strconnection As String) As String
        Dim sMessageID As String = String.Empty
        Dim localParameterList As DBParameters = DBParameters
        Dim TVPParameter As DBParameter = Nothing
        Dim gloDatabaseLayer As gloDatabaseLayer.DBLayer = Nothing
        Try
            localParameterList = DBParameters
            If TVPDataTable IsNot Nothing Then
                TVPParameter = New DBParameter("@TVP_Attachment", TVPDataTable, ParameterDirection.Input, SqlDbType.Structured)
                localParameterList.Add(TVPParameter)

            End If

            gloDatabaseLayer = New gloDatabaseLayer.DBLayer(strconnection)
            gloDatabaseLayer.Connect(False)
            sMessageID = Convert.ToString(gloDatabaseLayer.ExecuteScalar("SureScriptsInsertMessageAttachment", localParameterList))
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strconnection)
            Return sMessageID
        Finally
            gloDatabaseLayer.Disconnect()

            If gloDatabaseLayer IsNot Nothing Then
                gloDatabaseLayer.Dispose()
                gloDatabaseLayer = Nothing
            End If

            If TVPParameter IsNot Nothing Then
                TVPParameter = Nothing
            End If

            If localParameterList IsNot Nothing Then
                localParameterList = Nothing
            End If


        End Try
        ' End Using
        ' DBParameters disposed here.
        Return sMessageID
    End Function

    Public Function InsertSureScriptMessageInDB(ByVal SecureMessage As SecureMessage, ByVal AttachmentList As List(Of Attachment), ByVal strconnection As String) As String

        Dim sMessageID As String = String.Empty
        Dim localDBParameters As DBParameters = Nothing
        Dim TVPAttachmentDataTable As DataTable = Nothing

        Try

            If Not IsNothing(SecureMessage) Then
                localDBParameters = GetMessageParameters(SecureMessage)
                If AttachmentList IsNot Nothing Then
                    TVPAttachmentDataTable = GetAttachmentDataTable(AttachmentList)
                    sMessageID = SerializeMessage(localDBParameters, TVPAttachmentDataTable, strconnection)
                Else
                    sMessageID = SerializeMessage(localDBParameters, strconnection)
                End If

            End If

            Return sMessageID



        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strconnection)
            Return sMessageID

        Finally

            If Not IsNothing(TVPAttachmentDataTable) Then
                TVPAttachmentDataTable.Dispose()
                TVPAttachmentDataTable = Nothing
            End If

            If Not IsNothing(localDBParameters) Then
                localDBParameters.Dispose()
                localDBParameters = Nothing
            End If
        End Try



    End Function

    Private Sub UndoFlags(ByVal nSecureMessageInboxID As Long, ByVal strConnection As String)

        Dim MessageID As New DBParameter("@nSecureMessageInboxID", nSecureMessageInboxID, ParameterDirection.Input, SqlDbType.BigInt)
        Dim localParameterList As DBParameters = Nothing
        Dim gloDatabaseLayer As gloDatabaseLayer.DBLayer = Nothing

        Try
            localParameterList = New DBParameters
            localParameterList.Add(MessageID)
            MessageID = Nothing
            gloDatabaseLayer = New gloDatabaseLayer.DBLayer(strConnection)
            gloDatabaseLayer.Connect(False)
            gloDatabaseLayer.ExecuteScalar("SureScriptUndoflags", localParameterList)
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
        Finally
            gloDatabaseLayer.Disconnect()

            If gloDatabaseLayer IsNot Nothing Then
                gloDatabaseLayer.Dispose()
                gloDatabaseLayer = Nothing
            End If

            If localParameterList IsNot Nothing Then
                localParameterList.Dispose()
                localParameterList = Nothing
            End If

        End Try



    End Sub

    Public Function InsertSureScriptMessageInDB(ByVal SecureMessage As SecureMessage, ByVal strconnection As String) As String
        Dim returnedString As String = String.Empty

        If SecureMessage Is Nothing Then
            Throw New Exception("Message not supplied")
        Else
            returnedString = InsertSureScriptMessageInDB(SecureMessage, Nothing, strconnection)
        End If

        Return returnedString
    End Function

    Private Sub UpdatePortalQueuedMessage(ByVal nCommDetailID As Int64, ByVal sStatusMessage As String, ByVal bIsUpdated As Int64, ByVal strConnection As String)
        objCon = New SqlConnection(strConnection)
        Try
            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.Text
            Dim strquery As String = "UPDATE IntuitCommDetails SET bIsTransmitStatus = " & bIsUpdated & ",sTransmitStatusDesc= '" & sStatusMessage & "' WHERE nCommDetailID = " & nCommDetailID & "   "
            objcmd.CommandText = strquery
            objCon.Open()
            objcmd.ExecuteNonQuery()
            mdlGeneral.UpdateLog("updated Portal Queued Message " + nCommDetailID.ToString(), strConnection)
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        Finally
            'strquery = String.Empty
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Sub

    Private Sub UpdateMessageIDForPortal(ByVal sMessageID As String, ByVal nCommDetailID As Int64, ByVal strConnection As String)
        Dim oDB As New gloDatabaseLayer.DBLayer(strConnection)
        Try
            Dim ds As New DataSet
            oDB.Connect(False)
            oDB.Retrive_Query("SELECT nSecureMessageInboxID FROM SecureMessage_Inbox WHERE sMessageID= '" & sMessageID & "'", ds)

            If Not IsNothing(ds) Then
                If ds.Tables.Count > 0 Then
                    Dim nSecureMessageInboxID As Int64 = Convert.ToInt64(ds.Tables(0).Rows(0)("nSecureMessageInboxID"))
                    If nSecureMessageInboxID > 0 Then
                        Dim strquery As String = "UPDATE IntuitCommDetails SET nMessageID = " & nSecureMessageInboxID & " WHERE nCommDetailID = " & nCommDetailID & "   "
                        oDB.Execute_Query(strquery)
                    End If

                End If

            End If
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        Finally
            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub


    Private Function Fill_SurescriptClinic(ByVal strConnection As String, ByVal nSenderID As Long) As DataTable
        Dim oCon As New SqlConnection(strConnection)
        Dim oCmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strQuery As String = String.Empty
        Dim dtClinics As New DataTable()
        Dim oParam As New SqlParameter
        Try
            'Get the Clinic Information
            oCmd.Connection = oCon

            oCmd.CommandText = "surescriptGetClinicdetails"
            oCmd.CommandType = CommandType.StoredProcedure
            oParam.ParameterName = "@nSenderId"
            oParam.DbType = DbType.Int64
            oParam.Direction = ParameterDirection.Input
            oParam.Value = nSenderID
            oCmd.Parameters.Add(oParam)
            da.SelectCommand = oCmd
            da.Fill(dtClinics)

            Return dtClinics

        Catch ex As Exception
            mdlGeneral.UpdateLog("Clinic Information not Getting" + ex.ToString(), strConnection)
            Return Nothing
        Finally

            strQuery = Nothing

            If Not IsNothing(oCon) Then
                oCon.Dispose() : oCon = Nothing
            End If

            If Not IsNothing(oCmd) Then
                oCmd.Dispose() : oCmd = Nothing
            End If

            If Not IsNothing(da) Then
                da.Dispose() : da = Nothing
            End If

            'If Not IsNothing(dtClinics) Then
            '    dtClinics.Dispose() : dtClinics = Nothing
            'End If

        End Try
    End Function

    Public Sub SendeRxProduction()

        Dim i As Integer = 0
        Dim myService As New eRXProd.eRxMessage


        ' Dim bytesRead As Byte()
        Dim readbytes As Byte()
        Dim _eRxStatus As String = ""
        Dim _eRxStatusMessage As String = ""
        Dim _nPrescriptionID As Long = 0
        Dim _sDrugName As String = ""
        Dim _nProviderD As Long = 0
        Dim _nPatientID As Long = 0
        Dim _nUserID As Long = 0



        mdlGeneral.SetDbCredentials()
        Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
            Dim objSendEncryption As clsEncryption = New clsEncryption
            Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
            Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

            Dim ds As New DataSet
            Dim oDB As New gloDatabaseLayer.DBLayer(strConnection)
            oDB.Connect(False)

            'oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE sSettingsName = 'eRxProductionWebserviceURL'", ds)
            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM settings WITH (NOLOCK) WHERE sSettingsName in ('eRxProductionWebserviceURL','eRx10dot6ProductionWebserviceURL') ORDER BY sSettingsName desc", ds)


            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            'Dim objGloSureScriptGeneral As New gloSureScript.gloSurescriptGeneral
            gloSureScript.gloSurescriptGeneral.DatabaseName = mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString
            gloSureScript.gloSurescriptGeneral.ServerName = mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString
            gloSureScript.gloSurescriptGeneral.sUserName = mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString
            gloSureScript.gloSurescriptGeneral.sPassword = strDbPassword
            gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = True
            Dim eRxTable As DataTable = RetrieveeRx(strConnection)
            Dim eRxCount As Integer = 0

            Do While (eRxCount < eRxTable.Rows.Count)

                Dim data As Byte() = Convert.FromBase64String(eRxTable.Rows(eRxCount)("eRxFile")) ' CType(eRxTable.Rows(eRxCount)("eRxFile").ToString(), Byte())

                'Dim data As [Byte]() = New Byte(DirectCast(Convert.FromBase64String(eRxTable.Rows(eRxCount)("eRxFile").ToString()), [Byte]()).ToString().Length - 1) {}
                _nPrescriptionID = eRxTable.Rows(eRxCount)("nPrescriptionID")
                _nProviderD = eRxTable.Rows(eRxCount)("nProviderId")
                _nPatientID = eRxTable.Rows(eRxCount)("nPatientID")
                _nUserID = eRxTable.Rows(eRxCount)("nloginUserId")

                Dim objSureScriptMessage As New gloSureScript.SureScriptMessage

                objSureScriptMessage.MessageID = eRxTable.Rows(eRxCount)("nMessageID")
                objSureScriptMessage.MessageName = eRxTable.Rows(eRxCount)("sMessageName")
                objSureScriptMessage.RelatesToMessageId = eRxTable.Rows(eRxCount)("sRelatesToMessageID")
                objSureScriptMessage.MessageFrom = eRxTable.Rows(eRxCount)("sMessageFrom")
                objSureScriptMessage.MessageTo = eRxTable.Rows(eRxCount)("sMessageTo")
                objSureScriptMessage.DateTimeStamp = eRxTable.Rows(eRxCount)("sDateTimeStamp")
                objSureScriptMessage.DateReceived = eRxTable.Rows(eRxCount)("dtDateReceived")
                'objSureScriptMessage.ReferenceNumber = eRxTable.Rows(eRxCount)("sReferenceNumber")
                'objSureScriptMessage.IsAlertCheck = eRxTable.Rows(eRxCount)("IsAlertCheck")

                _sDrugName = GetDrugName(_nPrescriptionID, strConnection)



                'If ds.Tables.Count > 0 Then
                '    myService.Url = ds.Tables(0).Rows(0)("sSettingsValue").ToString()
                'Else
                '    mdlGeneral.UpdateLog("Web service URL was not Set")
                'End If



                Dim cntResponse As Byte() = Nothing
                If ds.Tables(0).Rows.Count > 2 Then
                    myRxWCFService = ServiceClass.GetWCFSvc(ds.Tables(0).Rows(2)("sSettingsValue").ToString())
                    mdlGeneral.UpdateLog("Access to Webservice is successful on Production Server", strConnection)
                    readbytes = myRxWCFService.PostClientMessage(data, "RxMessage")
                Else
                    mdlGeneral.UpdateLog("No eRx Web Service Settings Found ", strConnection)
                    Exit Sub
                End If
                Dim objgloSureScript As New gloSureScript.gloSureScriptInterface

                If readbytes IsNot Nothing Then
                    objgloSureScript.ReadStatusMessageRevised(readbytes, gloSureScriptInterface.SentMessageType.eNewRx, _sDrugName)
                    If objgloSureScript.StatusMessageType.Length > 0 Then
                        _eRxStatus = objgloSureScript.MessageName
                        _eRxStatusMessage = "" 'ogloInterface.StatusMessageType
                        If _eRxStatus <> "" Then

                            Select Case _eRxStatus
                                Case "Status"
                                    _eRxStatus = "Posted Sucessfully"
                                Case "Verify" 'for case verify we have same value in message variable as of status case
                                    _eRxStatus = "Posted Sucessfully"
                                Case "Error"
                                    _eRxStatus = "Could not be Posted Successfully"
                                    _eRxStatusMessage = objgloSureScript.StatusMessageType
                            End Select

                        End If
                    End If

                    'Dim oSurescriptDBLayer As New gloSureScript.gloSureScriptDBLayer
                    'ogloPrescription.DrugsCol.Item(0).TransactionID = ogloPrescription.DrugsCol.Item(0).PrescriptionID
                    'oSurescriptDBLayer.InsertintoMessageTransaction(CType(ogloPrescription.DrugsCol.Item(0), SureScriptMessage))
                    'Update the Prescription table with the updated values of eRxStatus against the rescpective PrescriptionID and NDC Code
                    'InsertMessageTransaction(objSureScriptMessage, strConnection)
                    UpdatePrescription(_eRxStatus, _eRxStatusMessage, _nPrescriptionID, strConnection) '

                    If _eRxStatus = "Posted Sucessfully" Then
                        DeleteFromeRxWithoutInternet(_nPrescriptionID, strConnection)
                    Else
                        ''Bug #69944: 00000715: Duplicate Task created with subject as "Send Rx" so condition added to generate task only once.
                        If eRxTable.Rows(eRxCount)("IsTaskGenerated") = 0 Then
                            AddTask(_nProviderD, _nPatientID, _nUserID, _nPrescriptionID, strConnection, _eRxStatusMessage)
                        End If
                        Dim _HitCount As Int16 = Convert.ToInt16(eRxTable.Rows(eRxCount)("nAttempts")) + 1
                        UpdateHitCountForeRxWithoutInternet(_nPrescriptionID, strConnection, _HitCount)
                    End If
                End If

                'objgloSureScript.e()
                eRxCount = (eRxCount + 1)
                'If Not IsNothing(ds) Then
                '    ds.Dispose()
                '    ds = Nothing
                'End If
            Loop
            ''Bug #69944: 00000715: Duplicate Task created with subject as "Send Rx"
            If Not IsNothing(eRxTable) Then
                eRxTable.Dispose()
                eRxTable = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
            eRxCount = 0

            i = (i + 1)
        Loop
        i = 0



    End Sub

    Private Sub UpdateHitCountForeRxWithoutInternet(ByVal PrescriptionID As Long, ByVal strConnection As String, ByVal HitCount As Int16)
        objCon = New SqlConnection(strConnection)
        Try
            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.Text
            Dim strquery As String = "Update eRxWithoutInternet Set nAttempts = " & HitCount & "  where nprescriptionid = " & PrescriptionID & "   "
            objcmd.CommandText = strquery
            objCon.Open()
            objcmd.ExecuteNonQuery()
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        Finally
            strSQL = String.Empty
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Sub

    Private Sub DeleteFromeRxWithoutInternet(ByVal PrescriptionID As Long, ByVal strConnection As String)
        objCon = New SqlConnection(strConnection)
        Try
            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.Text
            Dim strquery As String = "Delete from eRxWithoutInternet where nprescriptionid = " & PrescriptionID & "   "
            objcmd.CommandText = strquery
            objCon.Open()
            objcmd.ExecuteNonQuery()
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        Finally
            strSQL = String.Empty
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Sub

    Public Function InsertMessageTransaction(ByVal oMessage As SureScriptMessage, ByVal strConnection As String) As Boolean
        Dim conn As New SqlConnection(strConnection)
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
        conn.Open()
        Dim sql As SqlCommand = Nothing
        Dim _strsql As String
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            Dim intstatus As Int16 = 0
            _strsql = "Insert into SurescriptMessageTransaction (nMessageID,sMessageName,sRelatesToMessageID,sMessageFrom,sMessageTo,sDateTimeStamp,dtDateReceived,sReferenceNumber,IsAlertCheck) values ('" & oMessage.MessageID & "','" & oMessage.MessageName & "','" & oMessage.RelatesToMessageId & "','" & oMessage.MessageFrom & "','" & oMessage.MessageTo & "','" & oMessage.DateTimeStamp & "','" & oMessage.DateReceived & "','" & oMessage.TransactionID & "'," & intstatus & ")"
            sql.CommandText = _strsql
            sql.Connection = conn

            sql.ExecuteNonQuery()
            If oMessage.MessageName = "NewRx" Then
                Try
                    sql = New SqlCommand
                    sql.CommandType = CommandType.Text

                    _strsql = "Update surescriptmessagetransaction set isalertcheck='True'" _
                            & " where nMessageID in (select s.nMessageID " _
                            & " from surescriptmessagetransaction s inner join surescriptmessagetransaction s1 " _
                            & " on s.sRelatesToMessageID= s1.nMessageID where s1.sReferenceNumber='" & oMessage.TransactionID & "'" _
                            & " and s.sMessageName='Error' and s1.sMessageName='NewRx') and sMessageName='Error'"
                    sql.CommandText = _strsql
                    sql.Connection = conn

                    sql.ExecuteNonQuery()
                Catch ex As Exception

                End Try
            End If

            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.Message & ":" & ex.Source, strConnection)
            'Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Private Sub UpdatePrescription(ByVal eRxStatus As String, ByVal eRxStatusMessage As String, ByVal PrescriptionID As Long, ByVal strConnection As String)
        objCon = New SqlConnection(strConnection)
        Try
            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.Text
            Dim strquery As String = "update prescription set erxstatus = '" & eRxStatus & "',eRxStatusMessage = '" & eRxStatusMessage & "'  where nprescriptionid = " & PrescriptionID & "   "
            objcmd.CommandText = strquery
            objCon.Open()
            objcmd.ExecuteNonQuery()
            mdlGeneral.UpdateLog("update prescription " + PrescriptionID.ToString() + " With " + eRxStatus.ToString(), strConnection)
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        Finally
            'strquery = String.Empty
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
        End Try
    End Sub

    Public Sub GetPharmacyData(ByVal downloadType As enumDownloadType)
        Dim strZipFile As String = ""
        Dim strReqFile As String = ""
        Dim objSendEncryption As clsEncryption = Nothing
        Dim oPharmacies As Pharmacies = Nothing
        Dim bytePharmacy As Byte() = Nothing
        Dim dt As DataTable = Nothing
        Dim ds As DataSet = Nothing
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(strgloServiceConnection)
            oDB.Connect(False)

            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('EnableDirectory4dot4','eRxStagingWebserviceURL','eRx10dot6StagingWebserviceURL') ORDER BY sSettingsName", ds)

            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If ds.Tables(0).Rows.Count > 2 Then
                If ds.Tables(0).Rows(0)("sSettingsValue").ToString() = "True" Then
                    myRxWCFService = ServiceClass.GetWCFSvc(ds.Tables(0).Rows(1)("sSettingsValue").ToString())

                    Select Case downloadType
                        Case enumDownloadType.Full
                            'Dim str As String
                            'str = myRxWCFService.checkPharmacyDownload("Full")
                            bytePharmacy = myRxWCFService.PharmacyDownLoad("Full") 'myRxService.DirectoryDownload("Full")
                        Case enumDownloadType.Daily
                            bytePharmacy = myRxWCFService.PharmacyDownLoad("Daily") 'myRxService.DirectoryDownload("Daily")
                        Case enumDownloadType.Instant
                            bytePharmacy = myRxWCFService.PharmacyDownLoad("Instant") 'myRxService.DirectoryDownload("Instant")
                    End Select

                    If Not IsNothing(myRxWCFService) Then
                        myRxWCFService.Close()
                        myRxWCFService = Nothing
                    End If

                Else
                    If IsNothing(myRxService) Then
                        myRxService = New eRxMessage
                    End If
                    myRxService.Url = ds.Tables(0).Rows(2)("sSettingsValue").ToString()

                    Select Case downloadType
                        Case enumDownloadType.Full
                            bytePharmacy = myRxService.DirectoryDownload("Full")
                        Case enumDownloadType.Daily
                            bytePharmacy = myRxService.DirectoryDownload("Daily")
                        Case enumDownloadType.Instant
                            bytePharmacy = myRxService.DirectoryDownload("Instant")
                    End Select
                End If
            Else
                mdlGeneral.UpdateLog("No eRx Web Service Settings Found ", strConnection)
                Exit Sub
            End If



            If Not bytePharmacy Is Nothing Then
                strZipFile = mdlGeneral.GetFileName(enumFileType.ZipFile)
                strZipFile = mdlGeneral.ConvertBinarytoFile(bytePharmacy, strZipFile)
                If System.IO.File.Exists(strZipFile) Then

                    strReqFile = clsExtractFile.ExtractZipFile(strZipFile)
                    mdlGeneral.UpdateLog("Document is Extracted to get Pharmacy Data from Staging Server", strConnection)
                    If File.Exists(strReqFile) Then
                        If ds.Tables(0).Rows(0)("sSettingsValue").ToString() = "True" Then
                            dt = Read_PharmacyData_4dot4(strReqFile)   'Read_PharmacyData(strReqFile)  '4.4
                        Else
                            dt = Read_PharmacyData(strReqFile)
                        End If


                        '  oPharmacies = ReadPharmacyData(strReqFile)

                        Dim i As Integer = 0

                        'Developer:Mitesh Patel
                        'Date:13-Dec-2011
                        'Bug ID/PRD Name/Salesforce Case:
                        'Reason: Performance

                        ''If Not oPharmacies Is Nothing Then
                        ''    If oPharmacies.Count > 0 Then
                        ''        mdlGeneral.SetDbCredentials()
                        ''        Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
                        ''            objSendEncryption = New clsEncryption
                        ''            Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                        ''            Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword
                        ''            'Dim objInsPharmacy As InsertPharmacy = New InsertPharmacy
                        ''            'objInsPharmacy.strDbConnection = strConnection
                        ''            'objInsPharmacy.insPharmacies = oPharmacies
                        ''            'objPharmacyThread = New Thread(AddressOf objInsPharmacy.InsertPharmacy)
                        ''            'objPharmacyThread.Start()
                        ''            InsertPharmacy(oPharmacies, strConnection)
                        ''            i = (i + 1)
                        ''        Loop
                        ''        i = 0
                        ''    End If
                        ''Else
                        ''    mdlGeneral.UpdateLog("No new Pharmacy updates available for download from Staging Server")
                        ''End If


                        If dt.Rows.Count > 0 Then
                            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)

                                mdlGeneral.UpdateLog("Started Pharmacy data update for practice " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)

                                objSendEncryption = New clsEncryption
                                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

                                Dim iUpdateCount As String = GetPrescriberAndPharmacyUpdateCount()

                                If iUpdateCount = "0" Then
                                    InsertAllPharmacies(dt, strConnection)
                                Else
                                    InsertAllPharmaciesInBatch(dt, strConnection, iUpdateCount)
                                End If

                                mdlGeneral.UpdateLog("Finished Pharmacy data update for practice " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)

                                i = (i + 1)
                            Loop
                        Else
                            mdlGeneral.UpdateLog("No new Pharmacy updates available for download from Staging Server", strConnection)
                        End If


                    Else
                        mdlGeneral.UpdateLog("No new Pharmacy updates available for download from Staging Server", strConnection)
                    End If
                Else
                    mdlGeneral.UpdateLog("No new Pharmacy updates available for download from Staging Server", strConnection)
                End If
            Else
                mdlGeneral.UpdateLog("No new Pharmacy updates available for download from Staging Server", strConnection)
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error : " & ex.ToString, strConnection)

        Finally
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(oPharmacies) Then
                oPharmacies.Dispose()
                oPharmacies = Nothing
            End If
            If Not IsNothing(objSendEncryption) Then
                objSendEncryption = Nothing
            End If
            If System.IO.File.Exists(strZipFile) Then
                System.IO.File.Delete(strZipFile)
            End If
            If System.IO.File.Exists(strReqFile) Then
                System.IO.File.Delete(strReqFile)
            End If
            If Not IsNothing(bytePharmacy) Then
                bytePharmacy = Nothing
            End If
        End Try
    End Sub

    Public Sub GetPharmacyDataFromProduction(ByVal downloadType As enumDownloadType)
        Dim strZipFile As String = ""
        Dim strReqFile As String = ""
        Dim objSendEncryption As clsEncryption = Nothing
        Dim oPharmacies As Pharmacies = Nothing
        Dim dt As DataTable = Nothing
        Dim bytePharmacy As Byte() = Nothing
        Dim ds As DataSet = Nothing
        Try
            Dim oDB As New gloDatabaseLayer.DBLayer(strgloServiceConnection)
            oDB.Connect(False)

            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('EnableDirectory4dot4','eRxProductionWebserviceURL','eRx10dot6ProductionWebserviceURL') ORDER BY sSettingsName", ds)

            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If ds.Tables(0).Rows.Count > 2 Then
                If ds.Tables(0).Rows(0)("sSettingsValue").ToString() = "True" Then
                    myRxWCFService = ServiceClass.GetWCFSvc(ds.Tables(0).Rows(1)("sSettingsValue").ToString())

                    Select Case downloadType
                        Case enumDownloadType.Full
                            'Dim str As String
                            'str = myRxWCFService.checkPharmacyDownload("Full")
                            bytePharmacy = myRxWCFService.PharmacyDownLoad("Full") 'myRxService.DirectoryDownload("Full")
                        Case enumDownloadType.Daily
                            bytePharmacy = myRxWCFService.PharmacyDownLoad("Daily") 'myRxService.DirectoryDownload("Daily")
                        Case enumDownloadType.Instant
                            bytePharmacy = myRxWCFService.PharmacyDownLoad("Instant") 'myRxService.DirectoryDownload("Instant")
                    End Select

                    If Not IsNothing(myRxWCFService) Then
                        myRxWCFService.Close()
                        myRxWCFService = Nothing
                    End If

                Else
                    If IsNothing(myRxServiceProd) Then
                        myRxServiceProd = New eRXProd.eRxMessage
                    End If
                    myRxServiceProd.Url = ds.Tables(0).Rows(2)("sSettingsValue").ToString()

                    Select Case downloadType
                        Case enumDownloadType.Full
                            bytePharmacy = myRxServiceProd.DirectoryDownload("Full")
                        Case enumDownloadType.Daily
                            bytePharmacy = myRxServiceProd.DirectoryDownload("Daily")
                        Case enumDownloadType.Instant
                            bytePharmacy = myRxServiceProd.DirectoryDownload("Instant")
                    End Select
                End If
            Else
                mdlGeneral.UpdateLog("No eRx Web Service Settings Found ", strConnection)
                Exit Sub
            End If

            If Not bytePharmacy Is Nothing Then
                strZipFile = mdlGeneral.GetFileName(enumFileType.ZipFile)
                strZipFile = mdlGeneral.ConvertBinarytoFile(bytePharmacy, strZipFile)
                Dim i As Integer = 0
                If System.IO.File.Exists(strZipFile) Then
                    strReqFile = clsExtractFile.ExtractZipFile(strZipFile)
                    mdlGeneral.UpdateLog("Document is Extracted to get Pharmacy Data from Production Server", strConnection)

                    If File.Exists(strReqFile) Then
                        ' oPharmacies = ReadPharmacyData(strReqFile)
                        If ds.Tables(0).Rows(0)("sSettingsValue").ToString() = "True" Then
                            dt = Read_PharmacyData_4dot4(strReqFile)   'Read_PharmacyData(strReqFile)  '4.4
                        Else
                            dt = Read_PharmacyData(strReqFile)
                        End If

                        'If Not oPharmacies Is Nothing Then
                        ' If oPharmacies.Count > 0 Then
                        ' mdlGeneral.SetDbCredentials()
                        'Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
                        '    objSendEncryption = New clsEncryption
                        '    Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                        '    Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

                        '    InsertPharmacy(oPharmacies, strConnection)
                        '    i = (i + 1)
                        'Loop

                        'Developer:Mitesh Patel
                        'Date:13-Dec-2011
                        'Bug ID/PRD Name/Salesforce Case:
                        'Reason: Performance

                        If dt.Rows.Count > 0 Then
                            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)

                                mdlGeneral.UpdateLog("Started Pharmacy data update for practice " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)

                                objSendEncryption = New clsEncryption
                                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

                                Dim iUpdateCount As String = GetPrescriberAndPharmacyUpdateCount()

                                If iUpdateCount = "0" Then
                                    InsertAllPharmacies(dt, strConnection)
                                Else
                                    InsertAllPharmaciesInBatch(dt, strConnection, iUpdateCount)
                                End If

                                mdlGeneral.UpdateLog("Finished Pharmacy data update for practice " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)

                                i = (i + 1)
                            Loop
                        Else
                            mdlGeneral.UpdateLog("No new Pharmacy updates available for download from Production Server", strConnection)
                        End If
                        i = 0
                        'InsertPharmacy(oPharmacies)
                        'End If
                        'Else
                        ' mdlGeneral.UpdateLog("No new Pharmacy updates available for download from Production Server")
                        ' End If
                    Else
                        mdlGeneral.UpdateLog("No new Pharmacy updates available for download from Production Server", strConnection)
                    End If
                Else
                    mdlGeneral.UpdateLog("No new Pharmacy updates available for download from Production Server", strConnection)
                End If
            Else
                mdlGeneral.UpdateLog("No new Pharmacy updates available for download from Production Server", strConnection)
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error : " & ex.ToString, strConnection)
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(objSendEncryption) Then
                objSendEncryption = Nothing
            End If
            If Not IsNothing(oPharmacies) Then
                oPharmacies.Dispose()
                oPharmacies = Nothing
            End If
            If System.IO.File.Exists(strZipFile) Then
                System.IO.File.Delete(strZipFile)
            End If
            If System.IO.File.Exists(strReqFile) Then
                System.IO.File.Delete(strReqFile)
            End If
        End Try
    End Sub

    Public Sub GetPrescriberData(ByVal downloadType As enumDownloadType)
        Dim strZipFile As String = ""
        Dim strReqFile As String = ""
        Dim objSendEncryption As clsEncryption = Nothing
        Dim bytePrescriber As Byte() = Nothing
        Dim dt As DataTable = Nothing
        Dim ds As DataSet = Nothing
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(strgloServiceConnection)
            oDB.Connect(False)

            ' oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('EnableDirectory4dot4','eRxStagingWebserviceURL','eRx10dot6StagingWebserviceURL') ORDER BY sSettingsName", ds)
            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName ='eRx10dot6StagingWebserviceURL'", ds)

            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If ds.Tables(0).Rows.Count > 0 Then
                'If ds.Tables(0).Rows(0)("sSettingsValue").ToString() = "True" Then
                myRxWCFService = ServiceClass.GetWCFSvc(ds.Tables(0).Rows(0)("sSettingsValue").ToString())

                Select Case downloadType
                    Case enumDownloadType.Full
                        bytePrescriber = myRxWCFService.PrescriberDownload("Full")
                    Case enumDownloadType.Daily
                        bytePrescriber = myRxWCFService.PrescriberDownload("Daily")
                End Select

                If Not IsNothing(mygloDirectService) Then
                    mygloDirectService.Close()
                    mygloDirectService = Nothing
                End If

                '    Else
                'myRxService.Url = ds.Tables(0).Rows(2)("sSettingsValue").ToString()


                'End If
            Else
                mdlGeneral.UpdateLog("No eRx Web Service Settings Found ", strConnection)
                Exit Sub
            End If



            If Not bytePrescriber Is Nothing Then
                strZipFile = mdlGeneral.GetFileName(enumFileType.ZipFile)
                strZipFile = mdlGeneral.ConvertBinarytoFile(bytePrescriber, strZipFile)
                If System.IO.File.Exists(strZipFile) Then

                    strReqFile = clsExtractFile.ExtractZipFile(strZipFile)
                    mdlGeneral.UpdateLog("Document is Extracted to get Prescriber Data from Staging Server", strConnection)
                    If File.Exists(strReqFile) Then
                        ''If ds.Tables(0).Rows(0)("sSettingsValue").ToString() = "True" Then
                        dt = Read_PrescriberData_4dot5(strReqFile, strgloServiceConnection)   'Read_PharmacyData(strReqFile)  '4.4
                        ''Else

                        ''End If

                        Dim i As Integer = 0
                        If dt.Rows.Count > 0 Then
                          

                            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)

                                mdlGeneral.UpdateLog("Started Prescriber data update for practice " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)

                                objSendEncryption = New clsEncryption
                                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

                                Dim iUpdateCount As String = GetPrescriberAndPharmacyUpdateCount()
                                If downloadType = enumDownloadType.Full Then
                                    Dim oDBLayer As New gloDatabaseLayer.DBLayer(strConnection)
                                    oDBLayer.Connect(False)
                                    mdlGeneral.UpdateLog("Started Update Contacts_Mst set bIsBlocked = 1", strConnection)
                                    oDBLayer.Execute("gsp_UpdatePrescriberBlock")
                                    ' oDBLayer.Execute_Query("Update Contacts_Mst set bIsBlocked = 1 WHERE  Contacts_MST.sContactType = 'Physician' AND ISNULL(sDirectAddress, '') <> '' AND ISNULL(Contacts_MST.nClinicID, 1) = 1 AND ISNULL(Contacts_MST.sSPI, '') <> ''")
                                    oDBLayer.Disconnect()
                                    mdlGeneral.UpdateLog("End Update Contacts_Mst set bIsBlocked = 1", strConnection)
                                    If Not IsNothing(oDB) Then
                                        oDBLayer.Dispose()
                                        oDBLayer = Nothing
                                    End If
                                End If
                                If iUpdateCount = "0" Then
                                    InsertAllPrescribers(dt, strConnection)
                                Else
                                    InsertAllPrescribersInBatch(dt, strConnection, iUpdateCount)
                                End If

                                mdlGeneral.UpdateLog("Finished Prescriber data update for practice " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)

                                i = (i + 1)
                            Loop
                        Else
                            mdlGeneral.UpdateLog("No new Prescriber updates available for download from Staging Server", strConnection)
                        End If


                    Else
                        mdlGeneral.UpdateLog("No new Prescriber updates available for download from Staging Server", strConnection)
                    End If
                Else
                    mdlGeneral.UpdateLog("No new Prescriber updates available for download from Staging Server", strConnection)
                End If
            Else
                mdlGeneral.UpdateLog("No new Prescriber updates available for download from Staging Server", strConnection)
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error : " & ex.ToString, strConnection)

        Finally
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(objSendEncryption) Then
                objSendEncryption = Nothing
            End If
            If System.IO.File.Exists(strZipFile) Then
                System.IO.File.Delete(strZipFile)
            End If
            If System.IO.File.Exists(strReqFile) Then
                System.IO.File.Delete(strReqFile)
            End If
            If Not IsNothing(bytePrescriber) Then
                bytePrescriber = Nothing
            End If
        End Try
    End Sub

    Public Sub GetPrescriberDataFromProduction(ByVal downloadType As enumDownloadType)
        Dim strZipFile As String = ""
        Dim strReqFile As String = ""
        Dim objSendEncryption As clsEncryption = Nothing
        Dim bytePrescriber As Byte() = Nothing
        Dim dt As DataTable = Nothing
        Dim ds As DataSet = Nothing
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(strgloServiceConnection)
            oDB.Connect(False)

            ' oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('EnableDirectory4dot4','eRxProductionWebserviceURL','eRx10dot6ProductionWebserviceURL') ORDER BY sSettingsName", ds)
            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName ='eRx10dot6ProductionWebserviceURL'", ds)

            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If ds.Tables(0).Rows.Count > 0 Then
                'If ds.Tables(0).Rows(0)("sSettingsValue").ToString() = "True" Then
                myRxWCFService = ServiceClass.GetWCFSvc(ds.Tables(0).Rows(0)("sSettingsValue").ToString())

                Select Case downloadType
                    Case enumDownloadType.Full
                        bytePrescriber = myRxWCFService.PrescriberDownload("Full")
                    Case enumDownloadType.Daily
                        bytePrescriber = myRxWCFService.PrescriberDownload("Daily")
                End Select

                If Not IsNothing(mygloDirectService) Then
                    mygloDirectService.Close()
                    mygloDirectService = Nothing
                End If

                '    Else
                'myRxService.Url = ds.Tables(0).Rows(2)("sSettingsValue").ToString()


                'End If
            Else
                mdlGeneral.UpdateLog("No eRx Web Service Settings Found ", strConnection)
                Exit Sub
            End If



            If Not bytePrescriber Is Nothing Then
                strZipFile = mdlGeneral.GetFileName(enumFileType.ZipFile)
                strZipFile = mdlGeneral.ConvertBinarytoFile(bytePrescriber, strZipFile)
                If System.IO.File.Exists(strZipFile) Then

                    strReqFile = clsExtractFile.ExtractZipFile(strZipFile)
                    mdlGeneral.UpdateLog("Document is Extracted to get Prescriber Data from Production Server", strConnection)
                    If File.Exists(strReqFile) Then
                        ''If ds.Tables(0).Rows(0)("sSettingsValue").ToString() = "True" Then

                        mdlGeneral.UpdateLog("Started Read_PrescriberData_4dot5", strConnection)

                        dt = Read_PrescriberData_4dot5(strReqFile, strgloServiceConnection)   'Read_PharmacyData(strReqFile)  '4.4

                        mdlGeneral.UpdateLog("Finished Read_PrescriberData_4dot5", strConnection)

                        ''Else

                        ''End If

                        mdlGeneral.UpdateLog("Database count " & dt.Rows.Count, strConnection)

                        Dim i As Integer = 0
                        If dt.Rows.Count > 0 Then
                            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)

                                mdlGeneral.UpdateLog("Started Prescriber data update for practice " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)

                                objSendEncryption = New clsEncryption
                                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

                                If downloadType = enumDownloadType.Full Then
                                    Dim oDBLayer As New gloDatabaseLayer.DBLayer(strConnection)
                                    oDBLayer.Connect(False)
                                    ' oDBLayer.Execute_Query("Update Contacts_Mst set bIsBlocked = 1 WHERE  Contacts_MST.sContactType = 'Physician' AND ISNULL(sDirectAddress, '') <> '' AND ISNULL(Contacts_MST.nClinicID, 1) = 1 AND ISNULL(Contacts_MST.sSPI, '') <> ''")
                                    oDBLayer.Execute("gsp_UpdatePrescriberBlock")
                                    oDBLayer.Disconnect()
                                    If Not IsNothing(oDB) Then
                                        oDBLayer.Dispose()
                                        oDBLayer = Nothing
                                    End If
                                End If
                                Dim iUpdateCount As String = GetPrescriberAndPharmacyUpdateCount()

                                If iUpdateCount = "0" Then
                                    InsertAllPrescribers(dt, strConnection)
                                Else
                                    InsertAllPrescribersInBatch(dt, strConnection, iUpdateCount)
                                End If

                                mdlGeneral.UpdateLog("Finished Prescriber data update for practice " & mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString)

                                i = (i + 1)
                            Loop
                        Else
                            mdlGeneral.UpdateLog("No new Prescriber updates available for download from Production Server", strConnection)
                        End If


                    Else
                        mdlGeneral.UpdateLog("No new Prescriber updates available for download from Production Server", strConnection)
                    End If
                Else
                    mdlGeneral.UpdateLog("No new Prescriber updates available for download from Production Server", strConnection)
                End If
            Else
                mdlGeneral.UpdateLog("No new Prescriber updates available for download from Production Server", strConnection)
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error : " & ex.ToString, strConnection)

        Finally
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(objSendEncryption) Then
                objSendEncryption = Nothing
            End If
            If System.IO.File.Exists(strZipFile) Then
                System.IO.File.Delete(strZipFile)
            End If
            If System.IO.File.Exists(strReqFile) Then
                System.IO.File.Delete(strReqFile)
            End If
            If Not IsNothing(bytePrescriber) Then
                bytePrescriber = Nothing
            End If
        End Try
    End Sub

    'Private Sub ExtractPharmacydata()
    '    Try

    '        'Dim _ExamTempFolder As String = gloSurescriptGeneral.RootPath & "\Temp"

    '        If System.IO.Directory.Exists(_ExamTempFolder) = True Then
    '            Dim oRootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(_ExamTempFolder)
    '            Dim oFiles As System.IO.FileInfo() = oRootFolder.GetFiles()
    '            Dim oFile As System.IO.FileInfo
    '            For Each oFile In oFiles
    '                If oFile.Extension <> "txt" Then
    '                    ReadPharmacyData(oFile.FullName)
    '                End If
    '            Next
    '            oFiles = Nothing
    '            oFile = Nothing
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Function ReadPharmacyData(ByVal strfilename As String) As Pharmacies
        Dim oFile As FileStream = New FileStream(strfilename, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim oReader As StreamReader = New StreamReader(oFile)
        Dim strTXT As String = Nothing
        Dim oPharmacy As Pharmacy = Nothing
        Dim oPharmacies As Pharmacies = Nothing
        Try
            oPharmacies = New Pharmacies
            Do While oReader.Peek() <> -1

                strTXT = oReader.ReadLine()

                oPharmacy = New Pharmacy
                oPharmacy.PharmacyID = strTXT.Substring(0, 7).Trim
                oPharmacy.Pharmacyname = strTXT.Substring(80, 35).Trim.Replace("'", "''")
                oPharmacy.PharmacyAddress.Address1 = strTXT.Substring(115, 35).Trim.Replace("'", "''")
                oPharmacy.PharmacyAddress.Address2 = strTXT.Substring(150, 35).Trim.Replace("'", "''")
                oPharmacy.PharmacyAddress.City = strTXT.Substring(185, 35).Trim.Replace("'", "''")
                oPharmacy.PharmacyAddress.State = strTXT.Substring(220, 2).Trim.Replace("'", "''")
                oPharmacy.PharmacyAddress.Zip = strTXT.Substring(222, 11).Trim.Replace("'", "''")
                oPharmacy.PharmacyAddress.Phone = strTXT.Substring(233, 25).Trim.Replace("'", "''")
                oPharmacy.PharmacyAddress.Fax = strTXT.Substring(258, 25).Trim.Replace("'", "''")
                oPharmacy.ActiveStartTime = Number2Date(strTXT.Substring(503, 22).Trim.Replace("'", "''"))
                oPharmacy.ActiveEndTime = Number2Date(strTXT.Substring(525, 22).Trim.Replace("'", "''"))
                oPharmacy.Servicelevel = strTXT.Substring(547, 5).Trim.Replace("'", "''")
                If strTXT.Trim.Length > 645 Then
                    oPharmacy.PharmacyStatus = strTXT.Substring(645, 1).Trim.Replace("'", "''")
                Else
                    oPharmacy.PharmacyStatus = ""
                End If
                If oPharmacy.PharmacyAddress.Address2 Is Nothing Then
                    oPharmacy.PharmacyAddress.Address2 = ""
                End If
                oPharmacies.Add(oPharmacy)

            Loop
            If oPharmacies.Count > 0 Then
                Return oPharmacies
            Else
                Return Nothing
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString)
            Return Nothing

        Finally
            oReader.Close()
            If Not IsNothing(oReader) Then
                oReader.Dispose()
                oReader = Nothing
            End If
            oFile.Close()
            If Not IsNothing(oFile) Then
                oFile.Dispose()
                oFile = Nothing
            End If
            'If Not IsNothing(oPharmacy) Then
            '    oPharmacy.Dispose()
            '    oPharmacy = Nothing
            'End If

        End Try
    End Function

    'Developer:Mitesh Patel
    'Date:13-Dec-2011
    'Bug ID/PRD Name/Salesforce Case:
    'Reason: Performance

    Private Function Read_PharmacyData(ByVal strfilename As String) As DataTable
        Dim oFile As FileStream = New FileStream(strfilename, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim oReader As StreamReader = New StreamReader(oFile)
        Dim strTXT As String = Nothing


        Dim TempDt As New DataTable("Contacts_Mst")

        Dim colPharmacyID As New DataColumn("sNCPDPID")
        colPharmacyID.DataType = GetType(String)
        Dim colPharmacyname As New DataColumn("sName")
        colPharmacyname.DataType = GetType(String)
        Dim colAddress1 As New DataColumn("sAddress1")
        colAddress1.DataType = GetType(String)
        Dim colAddress2 As New DataColumn("sAddress2")
        colAddress2.DataType = GetType(String)
        Dim colCity As New DataColumn("sCity")
        colCity.DataType = GetType(String)
        Dim colState As New DataColumn("sState")
        colState.DataType = GetType(String)
        Dim colZip As New DataColumn("sZip")
        colZip.DataType = GetType(String)
        Dim colPhone As New DataColumn("sPhone")
        colPhone.DataType = GetType(String)
        Dim colFax As New DataColumn("sFax")
        colFax.DataType = GetType(String)
        Dim colActiveStartTime As New DataColumn("ActiveStartTime")
        colActiveStartTime.DataType = GetType(Date)
        Dim colActiveEndTime As New DataColumn("ActiveEndTime")
        colActiveEndTime.DataType = GetType(Date)
        Dim colServicelevel As New DataColumn("sServicelevel")
        colServicelevel.DataType = GetType(String)
        Dim colPartnerAccount As New DataColumn("PartnerAccount")
        colPartnerAccount.DataType = GetType(String)
        Dim colLastModifiedDate As New DataColumn("LastModifiedDate")
        colLastModifiedDate.DataType = GetType(Date)
        Dim colPharmacyStatus As New DataColumn("sPharmacyStatus")
        colPharmacyStatus.DataType = GetType(String)
        Dim colSpecialtytype1 As New DataColumn("sSpecialtytype1")
        colSpecialtytype1.DataType = GetType(String)
        Dim colSpecialtytype2 As New DataColumn("sSpecialtytype2")
        colSpecialtytype2.DataType = GetType(String)
        Dim colSpecialtytype3 As New DataColumn("sSpecialtytype3")
        colSpecialtytype3.DataType = GetType(String)
        Dim colSpecialtytype4 As New DataColumn("sSpecialtytype4")
        colSpecialtytype4.DataType = GetType(String)
        Dim colContactType As New DataColumn("sContactType")
        colContactType.DataType = GetType(String)
        Dim colClinicID As New DataColumn("nClinicID")
        colClinicID.DataType = GetType(Long)
        'Problem #710: 00032076 Code added for column sPharmacyNPI present in table. 

        Dim colPharmacyNPI As New DataColumn("sPharmacyNPI")
        colContactType.DataType = GetType(String)




        TempDt.Columns.Add(colPharmacyID)
        TempDt.Columns.Add(colPharmacyname)
        TempDt.Columns.Add(colAddress1)
        TempDt.Columns.Add(colAddress2)
        TempDt.Columns.Add(colCity)
        TempDt.Columns.Add(colState)
        TempDt.Columns.Add(colZip)
        TempDt.Columns.Add(colPhone)
        TempDt.Columns.Add(colFax)
        TempDt.Columns.Add(colActiveStartTime)
        TempDt.Columns.Add(colActiveEndTime)
        TempDt.Columns.Add(colServicelevel)
        TempDt.Columns.Add(colPartnerAccount)
        TempDt.Columns.Add(colLastModifiedDate)
        TempDt.Columns.Add(colPharmacyStatus)
        TempDt.Columns.Add(colSpecialtytype1)
        TempDt.Columns.Add(colSpecialtytype2)
        TempDt.Columns.Add(colSpecialtytype3)
        TempDt.Columns.Add(colSpecialtytype4)
        TempDt.Columns.Add(colContactType)
        TempDt.Columns.Add(colClinicID)
        'Problem #710: 00032076 Code added for column sPharmacyNPI present in table. 
        TempDt.Columns.Add(colPharmacyNPI)




        Try

            Do While oReader.Peek() <> -1

                strTXT = oReader.ReadLine()

                Dim dr As DataRow = TempDt.NewRow()
                dr.Item("sNCPDPID") = strTXT.Substring(0, 7).Trim
                dr.Item("sName") = strTXT.Substring(80, 35).Trim.Replace("'", "''")
                dr.Item("sAddress1") = strTXT.Substring(115, 35).Trim.Replace("'", "''")
                dr.Item("sAddress2") = strTXT.Substring(150, 35).Trim.Replace("'", "''")
                dr.Item("sCity") = strTXT.Substring(185, 35).Trim.Replace("'", "''")
                dr.Item("sState") = strTXT.Substring(220, 2).Trim.Replace("'", "''")
                dr.Item("sZip") = strTXT.Substring(222, 11).Trim.Replace("'", "''")
                dr.Item("sPhone") = strTXT.Substring(233, 25).Trim.Replace("'", "''")
                dr.Item("sFax") = strTXT.Substring(258, 25).Trim.Replace("'", "''")
                dr.Item("ActiveStartTime") = CType(Number2Date(strTXT.Substring(503, 22).Trim.Replace("'", "''")), Date)
                dr.Item("ActiveEndTime") = CType(Number2Date(strTXT.Substring(525, 22).Trim.Replace("'", "''")), Date)
                dr.Item("sServicelevel") = strTXT.Substring(547, 5).Trim.Replace("'", "''")
                dr.Item("PartnerAccount") = strTXT.Substring(552, 35).Trim.Replace("'", "''")
                dr.Item("LastModifiedDate") = CType(Number2Date(strTXT.Substring(587, 22).Trim.Replace("'", "''")), Date) ''strTXT.Substring(549, 22).Trim.Replace("'", "''")
                If strTXT.Trim.Length > 645 Then
                    dr.Item("sPharmacyStatus") = strTXT.Substring(645, 1).Trim.Replace("'", "''")
                Else
                    dr.Item("sPharmacyStatus") = ""
                End If
                dr.Item("sSpecialtytype1") = ""
                dr.Item("sSpecialtytype2") = ""
                dr.Item("sSpecialtytype3") = ""
                dr.Item("sSpecialtytype4") = ""

                If dr.Item("sAddress2") Is Nothing Then
                    dr.Item("sAddress2") = ""
                End If
                dr.Item("sContactType") = "Pharmacy"
                dr.Item("nClinicID") = mdlGeneral.gnClinicID
                'Problem #710: 00032076 Code added for column sPharmacyNPI present in table. 
                dr.Item("sPharmacyNPI") = strTXT.Substring(817, 10).Trim.Replace("'", "''")
                TempDt.Rows.Add(dr)

            Loop
            If TempDt.Rows.Count > 0 Then
                Return TempDt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString)
            Return Nothing

        Finally
            'If Not IsNothing(TempDt) Then
            '    TempDt.Dispose()
            '    TempDt = Nothing
            'End If
            oFile.Flush()
            oReader.Close()
            If Not IsNothing(oReader) Then
                oReader.Dispose()
                oReader = Nothing
            End If
            oFile.Close()
            If Not IsNothing(oFile) Then
                oFile.Dispose()
                oFile = Nothing
            End If

        End Try
    End Function

    Private Function Read_PharmacyData_4dot4(ByVal strfilename As String) As DataTable
        Dim oFile As FileStream = New FileStream(strfilename, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim oReader As StreamReader = New StreamReader(oFile)
        Dim strTXT As String = Nothing


        Dim TempDt As New DataTable("Contacts_Mst")

        Dim colPharmacyID As New DataColumn("sNCPDPID")
        colPharmacyID.DataType = GetType(String)
        Dim colPharmacyname As New DataColumn("sName")
        colPharmacyname.DataType = GetType(String)
        Dim colAddress1 As New DataColumn("sAddress1")
        colAddress1.DataType = GetType(String)
        Dim colAddress2 As New DataColumn("sAddress2")
        colAddress2.DataType = GetType(String)
        Dim colCity As New DataColumn("sCity")
        colCity.DataType = GetType(String)
        Dim colState As New DataColumn("sState")
        colState.DataType = GetType(String)
        Dim colZip As New DataColumn("sZip")
        colZip.DataType = GetType(String)
        Dim colPhone As New DataColumn("sPhone")
        colPhone.DataType = GetType(String)
        Dim colFax As New DataColumn("sFax")
        colFax.DataType = GetType(String)
        Dim colActiveStartTime As New DataColumn("ActiveStartTime")
        colActiveStartTime.DataType = GetType(Date)
        Dim colActiveEndTime As New DataColumn("ActiveEndTime")
        colActiveEndTime.DataType = GetType(Date)
        Dim colServicelevel As New DataColumn("sServicelevel")
        colServicelevel.DataType = GetType(String)
        Dim colPartnerAccount As New DataColumn("PartnerAccount")
        colPartnerAccount.DataType = GetType(String)
        Dim colLastModifiedDate As New DataColumn("LastModifiedDate")
        colLastModifiedDate.DataType = GetType(Date)
        Dim colPharmacyStatus As New DataColumn("sPharmacyStatus")
        colPharmacyStatus.DataType = GetType(String)
        Dim colSpecialtytype1 As New DataColumn("sSpecialtytype1")
        colSpecialtytype1.DataType = GetType(String)
        Dim colSpecialtytype2 As New DataColumn("sSpecialtytype2")
        colSpecialtytype2.DataType = GetType(String)
        Dim colSpecialtytype3 As New DataColumn("sSpecialtytype3")
        colSpecialtytype3.DataType = GetType(String)
        Dim colSpecialtytype4 As New DataColumn("sSpecialtytype4")
        colSpecialtytype4.DataType = GetType(String)
        Dim colContactType As New DataColumn("sContactType")
        colContactType.DataType = GetType(String)
        Dim colClinicID As New DataColumn("nClinicID")
        colClinicID.DataType = GetType(Long)
        Dim colPharmacyNPI As New DataColumn("sPharmacyNPI")
        colContactType.DataType = GetType(String)


        TempDt.Columns.Add(colPharmacyID)
        TempDt.Columns.Add(colPharmacyname)
        TempDt.Columns.Add(colAddress1)
        TempDt.Columns.Add(colAddress2)
        TempDt.Columns.Add(colCity)
        TempDt.Columns.Add(colState)
        TempDt.Columns.Add(colZip)
        TempDt.Columns.Add(colPhone)
        TempDt.Columns.Add(colFax)
        TempDt.Columns.Add(colActiveStartTime)
        TempDt.Columns.Add(colActiveEndTime)
        TempDt.Columns.Add(colServicelevel)
        TempDt.Columns.Add(colPartnerAccount)
        TempDt.Columns.Add(colLastModifiedDate)
        TempDt.Columns.Add(colPharmacyStatus)
        TempDt.Columns.Add(colSpecialtytype1)
        TempDt.Columns.Add(colSpecialtytype2)
        TempDt.Columns.Add(colSpecialtytype3)
        TempDt.Columns.Add(colSpecialtytype4)
        TempDt.Columns.Add(colContactType)
        TempDt.Columns.Add(colClinicID)
        TempDt.Columns.Add(colPharmacyNPI)

        Try

            Do While oReader.Peek() <> -1

                strTXT = oReader.ReadLine()

                Dim dr As DataRow = TempDt.NewRow()
                dr.Item("sNCPDPID") = strTXT.Substring(0, 7).Trim
                dr.Item("sName") = strTXT.Substring(42, 35).Trim.Replace("'", "''")
                dr.Item("sAddress1") = strTXT.Substring(77, 35).Trim.Replace("'", "''")
                dr.Item("sAddress2") = strTXT.Substring(112, 35).Trim.Replace("'", "''")
                dr.Item("sCity") = strTXT.Substring(147, 35).Trim.Replace("'", "''")
                dr.Item("sState") = strTXT.Substring(182, 2).Trim.Replace("'", "''")
                dr.Item("sZip") = strTXT.Substring(184, 11).Trim.Replace("'", "''")
                dr.Item("sPhone") = strTXT.Substring(195, 25).Trim.Replace("'", "''")
                dr.Item("sFax") = strTXT.Substring(220, 25).Trim.Replace("'", "''")
                dr.Item("ActiveStartTime") = CType(Number2Date(strTXT.Substring(465, 22).Trim.Replace("'", "''")), Date)
                dr.Item("ActiveEndTime") = CType(Number2Date(strTXT.Substring(487, 22).Trim.Replace("'", "''")), Date)
                dr.Item("sServicelevel") = strTXT.Substring(509, 5).Trim.Replace("'", "''")
                dr.Item("PartnerAccount") = strTXT.Substring(514, 35).Trim.Replace("'", "''")
                dr.Item("LastModifiedDate") = CType(Number2Date(strTXT.Substring(549, 22).Trim.Replace("'", "''")), Date) ''strTXT.Substring(549, 22).Trim.Replace("'", "''")
                If strTXT.Trim.Length > 645 Then
                    dr.Item("sPharmacyStatus") = strTXT.Substring(606, 1).Trim.Replace("'", "''")
                Else
                    dr.Item("sPharmacyStatus") = ""
                End If
                dr.Item("sSpecialtytype1") = strTXT.Substring(827, 35).Trim.Replace("'", "''")
                dr.Item("sSpecialtytype2") = strTXT.Substring(862, 35).Trim.Replace("'", "''")
                dr.Item("sSpecialtytype3") = strTXT.Substring(897, 35).Trim.Replace("'", "''")
                dr.Item("sSpecialtytype4") = strTXT.Substring(932, 35).Trim.Replace("'", "''")

                If dr.Item("sAddress2") Is Nothing Then
                    dr.Item("sAddress2") = ""
                End If
                dr.Item("sContactType") = "Pharmacy"
                dr.Item("nClinicID") = mdlGeneral.gnClinicID
                dr.Item("sPharmacyNPI") = strTXT.Substring(817, 10).Trim.Replace("'", "''")
                TempDt.Rows.Add(dr)

            Loop
            If TempDt.Rows.Count > 0 Then
                Return TempDt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString)
            Return Nothing

        Finally
            'If Not IsNothing(TempDt) Then
            '    TempDt.Dispose()
            '    TempDt = Nothing
            'End If
            oFile.Flush()
            oReader.Close()
            If Not IsNothing(oReader) Then
                oReader.Dispose()
                oReader = Nothing
            End If
            oFile.Close()
            If Not IsNothing(oFile) Then
                oFile.Dispose()
                oFile = Nothing
            End If

        End Try
    End Function

    Private Function Read_PrescriberData_4dot5(ByVal strfilename As String, ByVal sgloServicesDBConnection As String) As DataTable
        Dim oFile As FileStream = New FileStream(strfilename, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim oReader As StreamReader = New StreamReader(oFile)
        Dim strTXT As String = Nothing


        Dim TempDt As New DataTable("Contacts_Mst")

        Dim colContactID As New DataColumn("nContactID") '**
        colContactID.DataType = GetType(Int64)
        Dim colSPIID As New DataColumn("sSPI") '**
        colSPIID.DataType = GetType(String)
        Dim colName As New DataColumn("sName") '**
        colName.DataType = GetType(String)
        Dim colLastname As New DataColumn("sLastName") '**
        colLastname.DataType = GetType(String)
        Dim colFirstname As New DataColumn("sFirstName") '**
        colFirstname.DataType = GetType(String)
        Dim colMiddlename As New DataColumn("sMiddleName")  '**
        colMiddlename.DataType = GetType(String)
        Dim colAddress1 As New DataColumn("sAddress1")
        colAddress1.DataType = GetType(String)
        Dim colAddress2 As New DataColumn("sAddress2")
        colAddress2.DataType = GetType(String)
        Dim colCity As New DataColumn("sCity")
        colCity.DataType = GetType(String)
        Dim colState As New DataColumn("sState")
        colState.DataType = GetType(String)
        Dim colZip As New DataColumn("sZip")
        colZip.DataType = GetType(String)
        Dim colPhone As New DataColumn("sPhone")
        colPhone.DataType = GetType(String)
        Dim colFax As New DataColumn("sFax")
        colFax.DataType = GetType(String)
        Dim colEmail As New DataColumn("sEmail")  '**
        colEmail.DataType = GetType(String)
        Dim colActiveStartTime As New DataColumn("ActiveStartTime")
        colActiveStartTime.DataType = GetType(Date)
        Dim colActiveEndTime As New DataColumn("ActiveEndTime")
        colActiveEndTime.DataType = GetType(Date)
        Dim colServicelevel As New DataColumn("sServicelevel")
        colServicelevel.DataType = GetType(String)
        Dim colPartnerAccount As New DataColumn("PartnerAccount")
        colPartnerAccount.DataType = GetType(String)
        Dim colLastModifiedDate As New DataColumn("LastModifiedDate")
        colLastModifiedDate.DataType = GetType(Date)
        'Dim colPharmacyStatus As New DataColumn("sPharmacyStatus")
        'colPharmacyStatus.DataType = GetType(String)
        Dim colSpecialtytype1 As New DataColumn("sSpecialtytype1")
        colSpecialtytype1.DataType = GetType(String)
        Dim colSpecialtytype2 As New DataColumn("sSpecialtytype2")
        colSpecialtytype2.DataType = GetType(String)
        Dim colSpecialtytype3 As New DataColumn("sSpecialtytype3")
        colSpecialtytype3.DataType = GetType(String)
        Dim colSpecialtytype4 As New DataColumn("sSpecialtytype4")
        colSpecialtytype4.DataType = GetType(String)
        Dim colDirectAddress As New DataColumn("sDirectAddress")
        colDirectAddress.DataType = GetType(String)

        Dim colPrefixname As New DataColumn("PrefixName")
        colPrefixname.DataType = GetType(String)
        Dim colSuffixname As New DataColumn("SuffixName")
        colSuffixname.DataType = GetType(String)
        Dim colClinicname As New DataColumn("ClinicName")
        colClinicname.DataType = GetType(String)
        Dim colNPI As New DataColumn("NPI")
        colNPI.DataType = GetType(String)
        Dim colUPIN As New DataColumn("UPIN")
        colUPIN.DataType = GetType(String)

        Dim colContactType As New DataColumn("sContactType")
        colContactType.DataType = GetType(String)
        Dim colClinicID As New DataColumn("nClinicID")
        colClinicID.DataType = GetType(Long)


        TempDt.Columns.Add(colContactID)
        TempDt.Columns.Add(colSPIID)
        TempDt.Columns.Add(colName)
        TempDt.Columns.Add(colLastname)
        TempDt.Columns.Add(colFirstname)
        TempDt.Columns.Add(colMiddlename)
        TempDt.Columns.Add(colAddress1)
        TempDt.Columns.Add(colAddress2)
        TempDt.Columns.Add(colCity)
        TempDt.Columns.Add(colState)
        TempDt.Columns.Add(colZip)
        TempDt.Columns.Add(colPhone)
        TempDt.Columns.Add(colFax)
        TempDt.Columns.Add(colEmail)
        TempDt.Columns.Add(colActiveStartTime)
        TempDt.Columns.Add(colActiveEndTime)
        TempDt.Columns.Add(colServicelevel)
        TempDt.Columns.Add(colPartnerAccount)
        TempDt.Columns.Add(colLastModifiedDate)
        ' TempDt.Columns.Add(colPharmacyStatus)
        TempDt.Columns.Add(colSpecialtytype1)
        TempDt.Columns.Add(colSpecialtytype2)
        TempDt.Columns.Add(colSpecialtytype3)
        TempDt.Columns.Add(colSpecialtytype4)
        TempDt.Columns.Add(colDirectAddress)

        TempDt.Columns.Add(colPrefixname)
        TempDt.Columns.Add(colSuffixname)
        TempDt.Columns.Add(colClinicname)
        TempDt.Columns.Add(colNPI)
        TempDt.Columns.Add(colUPIN)

        TempDt.Columns.Add(colContactType)
        TempDt.Columns.Add(colClinicID)

        Try

            Do While oReader.Peek() <> -1

                strTXT = oReader.ReadLine()

                Dim dr As DataRow = TempDt.NewRow()
                dr.Item("nContactID") = Get_UniqueueId(sgloServicesDBConnection)
                dr.Item("sSPI") = strTXT.Substring(0, 13).Trim
                dr.Item("sName") = ""
                dr.Item("sLastName") = strTXT.Substring(76, 35).Trim.Replace("'", "''")
                dr.Item("sFirstName") = strTXT.Substring(111, 35).Trim.Replace("'", "''")
                dr.Item("sMiddleName") = strTXT.Substring(146, 35).Trim.Replace("'", "''")


                dr.Item("sAddress1") = strTXT.Substring(226, 35).Trim.Replace("'", "''")
                dr.Item("sAddress2") = strTXT.Substring(261, 35).Trim.Replace("'", "''")
                dr.Item("sCity") = strTXT.Substring(296, 35).Trim.Replace("'", "''")
                dr.Item("sState") = strTXT.Substring(331, 2).Trim.Replace("'", "''")
                dr.Item("sZip") = strTXT.Substring(333, 11).Trim.Replace("'", "''")
                dr.Item("sPhone") = strTXT.Substring(344, 25).Trim.Replace("'", "''")
                dr.Item("sFax") = strTXT.Substring(369, 25).Trim.Replace("'", "''")

                dr.Item("sEmail") = strTXT.Substring(394, 80).Trim.Replace("'", "''")

                dr.Item("ActiveStartTime") = CType(Number2Date(strTXT.Substring(614, 22).Trim.Replace("'", "''")), Date)
                dr.Item("ActiveEndTime") = CType(Number2Date(strTXT.Substring(636, 22).Trim.Replace("'", "''")), Date)
                dr.Item("sServicelevel") = strTXT.Substring(658, 5).Trim.Replace("'", "''")
                dr.Item("PartnerAccount") = strTXT.Substring(663, 35).Trim.Replace("'", "''")
                dr.Item("LastModifiedDate") = CType(Number2Date(strTXT.Substring(698, 22).Trim.Replace("'", "''")), Date) ''strTXT.Substring(549, 22).Trim.Replace("'", "''")
                'If strTXT.Trim.Length > 645 Then
                '    dr.Item("sPharmacyStatus") = strTXT.Substring(606, 1).Trim.Replace("'", "''")
                'Else
                '    dr.Item("sPharmacyStatus") = ""
                'End If
                dr.Item("sSpecialtytype1") = strTXT.Substring(954, 35).Trim.Replace("'", "''")
                dr.Item("sSpecialtytype2") = strTXT.Substring(989, 35).Trim.Replace("'", "''")
                dr.Item("sSpecialtytype3") = strTXT.Substring(1024, 35).Trim.Replace("'", "''")
                dr.Item("sSpecialtytype4") = strTXT.Substring(1094, 35).Trim.Replace("'", "''")
                dr.Item("sDirectAddress") = strTXT.Substring(1416, 255).Trim.Replace("'", "''")

                dr.Item("PrefixName") = strTXT.Substring(66, 10).Trim.Replace("'", "''")
                dr.Item("SuffixName") = strTXT.Substring(181, 10).Trim.Replace("'", "''")
                dr.Item("ClinicName") = strTXT.Substring(191, 35).Trim.Replace("'", "''")
                dr.Item("NPI") = strTXT.Substring(931, 10).Trim.Replace("'", "''")
                dr.Item("UPIN") = strTXT.Substring(1234, 35).Trim.Replace("'", "''")

                If dr.Item("sAddress2") Is Nothing Then
                    dr.Item("sAddress2") = ""
                End If
                dr.Item("sContactType") = "Physician"
                dr.Item("nClinicID") = mdlGeneral.gnClinicID
                TempDt.Rows.Add(dr)

            Loop
            If TempDt.Rows.Count > 0 Then
                Return TempDt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, sgloServicesDBConnection)
            Return Nothing

        Finally
            'If Not IsNothing(TempDt) Then
            '    TempDt.Dispose()
            '    TempDt = Nothing
            'End If
            oFile.Flush()
            oReader.Close()
            If Not IsNothing(oReader) Then
                oReader.Dispose()
                oReader = Nothing
            End If
            oFile.Close()
            If Not IsNothing(oFile) Then
                oFile.Dispose()
                oFile = Nothing
            End If

        End Try
    End Function

    Private Function AddTask(ByVal nProviderID As Long, ByVal nPatientID As Long, ByVal UserID As Long, ByVal PrescriptionID As Long, ByVal strConnection As String, ByVal eRxStatusMessage As String) As Boolean
        Try

            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "TM_IN_Task"
            objcmd.CommandType = CommandType.StoredProcedure

            Dim oPara As New SqlParameter("@nTaskID", SqlDbType.BigInt)
            oPara.Direction = ParameterDirection.InputOutput
            oPara.Value = 0

            'objcmd.Parameters.Add("@nRxTransactionID", SqlDbType.VarChar)
            objcmd.Parameters.Add(oPara)
            objcmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@sSubject", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nStartDate", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nDueDate", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nPriorityID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nCategoryID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nFollowUpID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@bIsPrivate", SqlDbType.Bit)
            objcmd.Parameters.Add("@nOwnerID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nDateCreated", SqlDbType.BigInt)
            objcmd.Parameters.Add("@sNoteExt", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nTaskType", SqlDbType.Int)
            objcmd.Parameters.Add("@sFaxTiffFileName", SqlDbType.VarChar)
            objcmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nReferenceID1", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nReferenceID2", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@MachineID", SqlDbType.BigInt)

            objcmd.Parameters("@nProviderID").Value = nProviderID
            objcmd.Parameters("@nPatientID").Value = nPatientID
            objcmd.Parameters("@sSubject").Value = "Send eRx"
            objcmd.Parameters("@nStartDate").Value = DateAsNumber(DateTime.Now.ToString())
            objcmd.Parameters("@nDueDate").Value = DateAsNumber(DateTime.Now.ToString())
            objcmd.Parameters("@nPriorityID").Value = 1
            objcmd.Parameters("@nCategoryID").Value = 1
            objcmd.Parameters("@nFollowUpID").Value = 0
            objcmd.Parameters("@bIsPrivate").Value = False
            objcmd.Parameters("@nOwnerID").Value = UserID
            objcmd.Parameters("@nDateCreated").Value = DateAsNumber(DateTime.Now.ToString())
            objcmd.Parameters("@sNoteExt").Value = ""
            objcmd.Parameters("@nUserID").Value = UserID
            objcmd.Parameters("@nTaskType").Value = 1
            objcmd.Parameters("@sFaxTiffFileName").Value = ""
            objcmd.Parameters("@sMachineName").Value = System.Environment.MachineName
            objcmd.Parameters("@nReferenceID1").Value = 0
            objcmd.Parameters("@nReferenceID2").Value = 0
            objcmd.Parameters("@nClinicID").Value = mdlGeneral.gnClinicID
            objcmd.Parameters("@MachineID").Value = GetPrefixTransactionID(nPatientID, strConnection)



            objCon.Open()
            Dim _ReturnTaskID As Long = objcmd.ExecuteNonQuery()
            _ReturnTaskID = oPara.Value

            objcmd.Cancel()
            objcmd.Dispose()
            objcmd = Nothing

            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandText = "TM_IN_TaskAssign"
            objcmd.CommandType = CommandType.StoredProcedure

            objcmd.Parameters.Add("@nTaskID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nAssignToID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nAssignFromID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nSelfAssigned", SqlDbType.SmallInt)
            objcmd.Parameters.Add("@nAcceptRejectHold", SqlDbType.SmallInt)
            objcmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)

            objcmd.Parameters("@nTaskID").Value = _ReturnTaskID
            objcmd.Parameters("@nAssignToID").Value = UserID
            objcmd.Parameters("@nAssignFromID").Value = UserID
            objcmd.Parameters("@nSelfAssigned").Value = 1
            objcmd.Parameters("@nAcceptRejectHold").Value = 1
            objcmd.Parameters("@nClinicID").Value = mdlGeneral.gnClinicID
            objcmd.ExecuteNonQuery()

            objcmd.Cancel()
            objcmd.Dispose()
            objcmd = Nothing

            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandText = "TM_IN_TaskProgress"
            objcmd.CommandType = CommandType.StoredProcedure

            objcmd.Parameters.Add("@nTaskID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@nStatusID", SqlDbType.BigInt)
            objcmd.Parameters.Add("@dComplete", SqlDbType.Decimal)
            objcmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
            objcmd.Parameters.Add("@nDateTime", SqlDbType.DateTime)
            objcmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)

            objcmd.Parameters("@nTaskID").Value = _ReturnTaskID
            objcmd.Parameters("@nStatusID").Value = 1
            objcmd.Parameters("@dComplete").Value = 0
            objcmd.Parameters("@sDescription").Value = eRxStatusMessage.ToString()
            objcmd.Parameters("@nDateTime").Value = DateTime.Now
            objcmd.Parameters("@nClinicID").Value = mdlGeneral.gnClinicID

            objcmd.ExecuteNonQuery()
            objcmd.Cancel()
            objcmd.Dispose()
            objcmd = Nothing


            objcmd = New SqlCommand
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.Text
            Dim strquery As String = "Update eRxWithoutInternet Set IsTaskGenerated=1 where nPrescriptionID = " & PrescriptionID & "   "
            objcmd.CommandText = strquery
            objcmd.ExecuteNonQuery()

            mdlGeneral.UpdateLog("Task Added and Update eRxWithoutInternet Set IsTaskGenerated=1", strConnection)

            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString, strConnection)
            Return False
        Finally
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try


    End Function

    Public Function DateAsNumber(ByVal datevalue As String) As Int32
        Dim _result As Int32 = 0
        Dim _internaldate As DateTime = Convert.ToDateTime(datevalue)
        datevalue = String.Format(_internaldate.ToShortDateString(), "MM/dd/yyyy")
        Try
            If datevalue.Length = 10 Then
                Dim _internalresult As String = ""
                _internalresult = datevalue.Substring(6, 4) + datevalue.Substring(0, 2) + datevalue.Substring(3, 2)
                _result = Convert.ToInt32(_internalresult)
            ElseIf datevalue.Length = 9 Then
                Dim _internalresult As String = ""
                If _internaldate.Month <= 9 Then
                    _internalresult = datevalue.Substring(5, 4) + "0" + datevalue.Substring(0, 1) + datevalue.Substring(2, 2)
                ElseIf _internaldate.Day <= 9 Then
                    _internalresult = datevalue.Substring(5, 4) + datevalue.Substring(0, 2) + "0" + datevalue.Substring(3, 1)
                End If


                _result = Convert.ToInt32(_internalresult)
            ElseIf datevalue.Length = 8 Then
                Dim _internalresult As String = ""
                _internalresult = datevalue.Substring(4, 4) + "0" + datevalue.Substring(0, 1) + "0" + datevalue.Substring(2, 1)
                _result = Convert.ToInt32(_internalresult)
            End If
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString)
        End Try
        Return _result
    End Function

    '----------------------------------------------------------------
    ' Converted from C# to VB .NET using CSharpToVBConverter(1.2).
    ' Developed by: Kamal Patel (http://www.KamalPatel.net) 
    '----------------------------------------------------------------

    Private Function InsertPharmacy(ByVal oPharmacies As Object, ByVal strConnection As String) As Boolean
        objCon = New SqlConnection(strConnection)
        Dim opharmacy As Pharmacy = Nothing
        Try
            For icnt As Int64 = 0 To oPharmacies.Count - 1
                opharmacy = oPharmacies.Item(icnt)

                objcmd = New SqlCommand
                objcmd.Connection = objCon
                objcmd.CommandType = CommandType.Text
                strSQL = "select ncontactId from Contacts_mst where sNCPDPID='" & opharmacy.PharmacyID & "'"

                objcmd.CommandText = strSQL
                objCon.Open()
                Dim ncontactId As Int64 = objcmd.ExecuteScalar

                objcmd.Cancel()
                strSQL = String.Empty
                objcmd = New SqlCommand
                objcmd.Connection = objCon
                objcmd.CommandType = CommandType.Text

                If ncontactId <> 0 Then
                    strSQL = "Update Contacts_Mst set sName='" & opharmacy.Pharmacyname & "',sAddressLine1='" & opharmacy.PharmacyAddress.Address1 & "',sAddressLine2='" & opharmacy.PharmacyAddress.Address2 & "',sCity='" & opharmacy.PharmacyAddress.City & "',sState='" & opharmacy.PharmacyAddress.State & "',sZip='" & opharmacy.PharmacyAddress.Zip & "',sPhone='" & opharmacy.PharmacyAddress.Phone & "',sFax='" & opharmacy.PharmacyAddress.Fax & "',ActiveStartTime='" & opharmacy.ActiveStartTime & "',ActiveEndTime='" & opharmacy.ActiveEndTime & "',sServiceLevel='" & opharmacy.Servicelevel & "',sPharmacyStatus='" & opharmacy.PharmacyStatus & "' where nContactId=" & ncontactId
                    '' strSQL = "Update Contacts_Mst set sName='" & opharmacy.Pharmacyname & "',sStreet='" & opharmacy.PharmacyAddress.Address1 & "',sCity='" & opharmacy.PharmacyAddress.City & "',sState='" & opharmacy.PharmacyAddress.State & "',sZip='" & opharmacy.PharmacyAddress.Zip & "',sPhone='" & opharmacy.PharmacyAddress.Phone & "',sFax='" & opharmacy.PharmacyAddress.Fax & "',ActiveStartTime='" & opharmacy.ActiveStartTime & "',ActiveEndTime='" & opharmacy.ActiveEndTime & "',sServiceLevel='" & opharmacy.Servicelevel & "' where nContactId=" & ncontactId
                    objcmd.CommandText = strSQL
                    objcmd.ExecuteNonQuery()
                Else

                    'strSQL = "select isnull(max(isnull(nContactId,0)),0)+1 from Contacts_Mst"
                    'objcmd.CommandText = strSQL
                    'Dim Id As Int64 = objcmd.ExecuteScalar
                    'objcmd.Cancel()
                    Dim Id As Int64 = Get_UniqueueId(strConnection)
                    strSQL = String.Empty

                    objcmd = New SqlCommand
                    objcmd.Connection = objCon
                    objcmd.CommandType = CommandType.Text

                    'sarika Insert Clinic ID 20090227 
                    'insert clinicid as 1
                    ' strSQL = "Insert into Contacts_Mst (nContactID,sName,sStreet,sCity,sState,sZip,sPhone,sFax,scontacttype,sNCPDPID,ActiveStartTime,ActiveEndTime,sServiceLevel,sPharmacyStatus,sAddressLine2) values ( " & Id & ",'" & opharmacy.Pharmacyname & "','" & opharmacy.PharmacyAddress.Address1 & "','" & opharmacy.PharmacyAddress.City & "','" & opharmacy.PharmacyAddress.State & "','" & opharmacy.PharmacyAddress.Zip & "','" & opharmacy.PharmacyAddress.Phone & "','" & opharmacy.PharmacyAddress.Fax & "','Pharmacy','" & opharmacy.PharmacyID & "','" & opharmacy.ActiveStartTime & "','" & opharmacy.ActiveEndTime & "','" & opharmacy.Servicelevel & "','" & opharmacy.PharmacyStatus & "','" & opharmacy.PharmacyAddress.Address2 & "')"
                    strSQL = "Insert into Contacts_Mst (nContactID,sName,sAddressLine1,sCity,sState,sZip,sPhone,sFax,scontacttype,sNCPDPID,ActiveStartTime,ActiveEndTime,sServiceLevel,sPharmacyStatus,sAddressLine2,nClinicID) values ( " & Id & ",'" & opharmacy.Pharmacyname & "','" & opharmacy.PharmacyAddress.Address1 & "','" & opharmacy.PharmacyAddress.City & "','" & opharmacy.PharmacyAddress.State & "','" & opharmacy.PharmacyAddress.Zip & "','" & opharmacy.PharmacyAddress.Phone & "','" & opharmacy.PharmacyAddress.Fax & "','Pharmacy','" & opharmacy.PharmacyID & "','" & opharmacy.ActiveStartTime & "','" & opharmacy.ActiveEndTime & "','" & opharmacy.Servicelevel & "','" & opharmacy.PharmacyStatus & "','" & opharmacy.PharmacyAddress.Address2 & "'," & mdlGeneral.gnClinicID & ")"
                    '----

                    ''strSQL = "Insert into Contacts_Mst (nContactID,sName,sStreet,sCity,sState,sZip,sPhone,sFax,scontacttype,sexternalcode,ActiveStartTime,ActiveEndTime,sServiceLevel) values ( " & Id & ",'" & opharmacy.Pharmacyname & "','" & opharmacy.PharmacyAddress.Address1 & "','" & opharmacy.PharmacyAddress.City & "','" & opharmacy.PharmacyAddress.State & "','" & opharmacy.PharmacyAddress.Zip & "','" & opharmacy.PharmacyAddress.Phone & "','" & opharmacy.PharmacyAddress.Fax & "','Pharmacy','" & opharmacy.PharmacyID & "','" & opharmacy.ActiveStartTime & "','" & opharmacy.ActiveEndTime & "','" & opharmacy.Servicelevel & "')"
                    objcmd.CommandText = strSQL
                    objcmd.ExecuteNonQuery()
                End If
                objCon.Close()
            Next
            mdlGeneral.UpdateLog("Pharmacy data was updated", strConnection)
            Return True

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
            Return False
        Finally
            strSQL = String.Empty
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(opharmacy) Then
                opharmacy.Dispose()
                opharmacy = Nothing
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
        End Try
    End Function

    Private Function InsertPharmacy(ByVal oPharmacies As Pharmacies) As Boolean
        objCon = New SqlConnection(strConnection)
        Try
            Dim opharmacy As Pharmacy
            For icnt As Int64 = 0 To oPharmacies.Count - 1
                opharmacy = oPharmacies.Item(icnt)

                objcmd = New SqlCommand
                objcmd.Connection = objCon
                objcmd.CommandType = CommandType.Text
                strSQL = "select ncontactId from Contacts_mst where sNCPDPID='" & opharmacy.PharmacyID & "'"

                objcmd.CommandText = strSQL
                objCon.Open()
                Dim ncontactId As Int64 = objcmd.ExecuteScalar

                objcmd.Cancel()
                strSQL = String.Empty
                objcmd = New SqlCommand
                objcmd.Connection = objCon
                objcmd.CommandType = CommandType.Text

                If ncontactId <> 0 Then
                    strSQL = "Update Contacts_Mst set sName='" & opharmacy.Pharmacyname & "',sStreet='" & opharmacy.PharmacyAddress.Address1 & "',sAddressLine2='" & opharmacy.PharmacyAddress.Address2 & "',sCity='" & opharmacy.PharmacyAddress.City & "',sState='" & opharmacy.PharmacyAddress.State & "',sZip='" & opharmacy.PharmacyAddress.Zip & "',sPhone='" & opharmacy.PharmacyAddress.Phone & "',sFax='" & opharmacy.PharmacyAddress.Fax & "',ActiveStartTime='" & opharmacy.ActiveStartTime & "',ActiveEndTime='" & opharmacy.ActiveEndTime & "',sServiceLevel='" & opharmacy.Servicelevel & "',sPharmacyStatus='" & opharmacy.PharmacyStatus & "' where nContactId=" & ncontactId
                    '' strSQL = "Update Contacts_Mst set sName='" & opharmacy.Pharmacyname & "',sStreet='" & opharmacy.PharmacyAddress.Address1 & "',sCity='" & opharmacy.PharmacyAddress.City & "',sState='" & opharmacy.PharmacyAddress.State & "',sZip='" & opharmacy.PharmacyAddress.Zip & "',sPhone='" & opharmacy.PharmacyAddress.Phone & "',sFax='" & opharmacy.PharmacyAddress.Fax & "',ActiveStartTime='" & opharmacy.ActiveStartTime & "',ActiveEndTime='" & opharmacy.ActiveEndTime & "',sServiceLevel='" & opharmacy.Servicelevel & "' where nContactId=" & ncontactId
                    objcmd.CommandText = strSQL
                    objcmd.ExecuteNonQuery()
                Else

                    'strSQL = "select isnull(max(isnull(nContactId,0)),0)+1 from Contacts_Mst"
                    'objcmd.CommandText = strSQL
                    'Dim Id As Int64 = objcmd.ExecuteScalar
                    'objcmd.Cancel()
                    Dim Id As Int64 = Get_UniqueueId(strConnection)

                    strSQL = String.Empty

                    objcmd = New SqlCommand
                    objcmd.Connection = objCon
                    objcmd.CommandType = CommandType.Text

                    'sarika Insert Clinic ID 20090227 
                    'insert clinicid as 1
                    ' strSQL = "Insert into Contacts_Mst (nContactID,sName,sStreet,sCity,sState,sZip,sPhone,sFax,scontacttype,sNCPDPID,ActiveStartTime,ActiveEndTime,sServiceLevel,sPharmacyStatus,sAddressLine2) values ( " & Id & ",'" & opharmacy.Pharmacyname & "','" & opharmacy.PharmacyAddress.Address1 & "','" & opharmacy.PharmacyAddress.City & "','" & opharmacy.PharmacyAddress.State & "','" & opharmacy.PharmacyAddress.Zip & "','" & opharmacy.PharmacyAddress.Phone & "','" & opharmacy.PharmacyAddress.Fax & "','Pharmacy','" & opharmacy.PharmacyID & "','" & opharmacy.ActiveStartTime & "','" & opharmacy.ActiveEndTime & "','" & opharmacy.Servicelevel & "','" & opharmacy.PharmacyStatus & "','" & opharmacy.PharmacyAddress.Address2 & "')"
                    strSQL = "Insert into Contacts_Mst (nContactID,sName,sStreet,sCity,sState,sZip,sPhone,sFax,scontacttype,sNCPDPID,ActiveStartTime,ActiveEndTime,sServiceLevel,sPharmacyStatus,sAddressLine2,nClinicID) values ( " & Id & ",'" & opharmacy.Pharmacyname & "','" & opharmacy.PharmacyAddress.Address1 & "','" & opharmacy.PharmacyAddress.City & "','" & opharmacy.PharmacyAddress.State & "','" & opharmacy.PharmacyAddress.Zip & "','" & opharmacy.PharmacyAddress.Phone & "','" & opharmacy.PharmacyAddress.Fax & "','Pharmacy','" & opharmacy.PharmacyID & "','" & opharmacy.ActiveStartTime & "','" & opharmacy.ActiveEndTime & "','" & opharmacy.Servicelevel & "','" & opharmacy.PharmacyStatus & "','" & opharmacy.PharmacyAddress.Address2 & "'," & mdlGeneral.gnClinicID & ")"
                    '----

                    ''strSQL = "Insert into Contacts_Mst (nContactID,sName,sStreet,sCity,sState,sZip,sPhone,sFax,scontacttype,sexternalcode,ActiveStartTime,ActiveEndTime,sServiceLevel) values ( " & Id & ",'" & opharmacy.Pharmacyname & "','" & opharmacy.PharmacyAddress.Address1 & "','" & opharmacy.PharmacyAddress.City & "','" & opharmacy.PharmacyAddress.State & "','" & opharmacy.PharmacyAddress.Zip & "','" & opharmacy.PharmacyAddress.Phone & "','" & opharmacy.PharmacyAddress.Fax & "','Pharmacy','" & opharmacy.PharmacyID & "','" & opharmacy.ActiveStartTime & "','" & opharmacy.ActiveEndTime & "','" & opharmacy.Servicelevel & "')"
                    objcmd.CommandText = strSQL
                    objcmd.ExecuteNonQuery()
                End If
                objCon.Close()
            Next
            mdlGeneral.UpdateLog("Pharmacy data was updated", strConnection)
            Return True

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
            Return False
        Finally
            strSQL = String.Empty
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
    Public Sub AutoSendPendingOpportunity()
        Dim dtPendingOpportunity As DataTable = Nothing
        Dim objclsDataInsertionLayer As gloRxPatientSaving.clsDataInsertionLayer = Nothing
        Dim dispositionlist As Generic.List(Of gloRxPatientSaving.clsDataInsertionLayer.DispositionData) = Nothing
        Try
            mdlGeneral.SetDbCredentials()
            Dim i As Integer
            Dim strConnection As String = ""
            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
                Dim objSendEncryption As clsEncryption = New clsEncryption
                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                strConnection = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword
                i = i + 1


                dtPendingOpportunity = GetPendingOpportunity(strConnection)
                If Not IsNothing(dtPendingOpportunity) And dtPendingOpportunity.Rows.Count > 0 Then
                    gloRxPatientSaving.PatSavGeneral.ConnectionString = strConnection
                    objclsDataInsertionLayer = New gloRxPatientSaving.clsDataInsertionLayer
                    For Each dr As DataRow In dtPendingOpportunity.Rows
                        dispositionlist = objclsDataInsertionLayer.generateOpportunityResponse(dr("PSID"))
                        If dispositionlist.Count > 0 Then
                            For icnt As Integer = 0 To dispositionlist.Count - 1
                                SendPatientSavingDispositionMessages(dispositionlist.Item(icnt).MessageID, dispositionlist.Item(icnt).FileName, dr("nPatientID"), strConnection)
                            Next
                            If Not IsNothing(dispositionlist) Then
                                dispositionlist = Nothing
                            End If
                        End If
                    Next
                    If Not IsNothing(objclsDataInsertionLayer) Then
                        objclsDataInsertionLayer = Nothing
                    End If
                End If
                If Not IsNothing(dtPendingOpportunity) Then
                    dtPendingOpportunity.Dispose()
                    dtPendingOpportunity = Nothing
                End If
            Loop
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        End Try

    End Sub
    Private Function GetPendingOpportunity(ByVal SqlConnectionString As String) As DataTable
        Dim oDBLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim objResult As DataTable = Nothing
        Try
            oDBLayer = New gloDatabaseLayer.DBLayer(SqlConnectionString)
            oDBLayer.Connect(False)
            oDBLayer.Retrive("gsp_GetPendingPatSavOpportunity", objResult)
            oDBLayer.Disconnect()

        Catch ex As SqlException
            mdlGeneral.UpdateLog("DBError in Fetching Pending Opportunity :" & ex.ToString, SqlConnectionString)
            Return objResult
        Catch ex As Exception
            mdlGeneral.UpdateLog("DBError in Fetching Pending Opportunity :" & ex.ToString, SqlConnectionString)
            Return objResult
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try
        Return objResult
    End Function
    'For Rx-Sniffer Kill Case
    Public Function UpdateFullPharmacyDownload(ByVal sSettingname As String) As Boolean
        Dim strSqlStatement As String = String.Empty
        Try
            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandText = "Update GLSettings set sSettingsValue = 'False' Where sSettingsName='" & sSettingname & "'"

            objCon.Open()
            objcmd.ExecuteNonQuery()

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If sSettingname = "DirService" Then
                mdlGeneral.UpdateLog("Full Pharmacy download completed.", strConnection)
            Else
                mdlGeneral.UpdateLog("Full Prescriber download completed.", strConnection)
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        Finally
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try

    End Function

    Public Function GetPDMPUserNamePassword(ByVal strConnection As String) As DataTable
        Dim strSqlStatement As String = String.Empty
        Dim DA As New SqlDataAdapter
        Dim DT As New DataTable
        Try
            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.StoredProcedure
            objcmd.CommandText = "PDMP_GetUserNamePassword"
            objCon.Open()
            DA.SelectCommand = objcmd
            DA.Fill(DT)

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If


        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        Finally
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
        Return DT
    End Function

    Public Function GetPDMPDetails(ByVal strConnection As String) As DataTable
        Dim strSqlStatement As String = String.Empty
        Dim DA As New SqlDataAdapter
        Dim DT As New DataTable
        Try
            objCon = New SqlConnection
            objcmd = New SqlCommand
            objCon.ConnectionString = strConnection
            objcmd.Connection = objCon
            objcmd.CommandType = CommandType.StoredProcedure
            objcmd.CommandText = "PDMP_GetPatients"
            objCon.Open()
            DA.SelectCommand = objcmd
            DA.Fill(DT)

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If


        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        Finally
            If Not IsNothing(objcmd) Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
        Return DT
    End Function

    'Developer:Mitesh Patel
    'Date:13-Dec-2011
    'Bug ID/PRD Name/Salesforce Case:
    'Reason: Performance

    Private Function InsertAllPharmacies(ByVal dtPharmacy As DataTable, ByVal strConnection As String) As Boolean
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(strConnection)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim objResult As Object = Nothing
        Try
            oDBLayer.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@TmpContact_Mst", dtPharmacy, ParameterDirection.Input, SqlDbType.Structured)
            oDBLayer.Execute("gsp_UpdatePharmacy", oDBParameters, objResult)
            oDBLayer.Disconnect()
        Catch ex As SqlException
            mdlGeneral.UpdateLog("DBError in Updation of Pharmacy :" & ex.ToString, strConnection)
            Return False
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error in Updation of Pharmacy :" & ex.ToString, strConnection)
            Return False
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
            End If
        End Try
        Return True
    End Function

    Private Function InsertAllPharmaciesInBatch(ByVal dtPharmacy As DataTable, ByVal strConnection As String, nincrement As String) As Boolean
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(strConnection)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim objResult As Object = Nothing
        Dim increment As Integer = Integer.Parse(nincrement)
        Dim nToCopyRecords As Integer
        Dim nFromCopyRecords As Integer = 0
        Dim dtsub As New DataTable
        dtsub = dtPharmacy.Clone
        Try
            nToCopyRecords = increment
            If Not IsNothing(dtsub) Then
                While nFromCopyRecords < dtPharmacy.Rows.Count
                    dtsub.Clear()
                    If dtPharmacy.Rows.Count < nToCopyRecords Then
                        nToCopyRecords = dtPharmacy.Rows.Count
                    End If
                    For i As Integer = nFromCopyRecords To nToCopyRecords - 1
                        dtsub.ImportRow(dtPharmacy.Rows(i))
                    Next
                    oDBLayer.Connect(False)
                    oDBParameters.Clear()
                    oDBParameters.Add("@TmpContact_Mst", dtsub, ParameterDirection.Input, SqlDbType.Structured)
                    oDBLayer.Execute("gsp_UpdatePharmacy", oDBParameters, objResult)
                    oDBLayer.Disconnect()
                    nFromCopyRecords = nToCopyRecords
                    nToCopyRecords = nToCopyRecords + increment
                End While
            End If
        Catch ex As SqlException
            mdlGeneral.UpdateLog("DBError in Updation of Pharmacy :" & ex.ToString, strConnection)
            Return False
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error in Updation of Pharmacy :" & ex.ToString, strConnection)
            Return False
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
            End If
            If Not IsNothing(dtsub) Then
                dtsub.Dispose()
                dtsub = Nothing
            End If
        End Try
        Return True
    End Function

    Private Function InsertAllPrescribersInBatch(ByVal dtPrescriber As DataTable, ByVal strConnection As String, nincrement As String) As Boolean
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(strConnection)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim objResult As Object = Nothing
        Dim increment As Integer = Integer.Parse(nincrement)
        Dim nToCopyRecords As Integer
        Dim nFromCopyRecords As Integer = 0
        Dim dtsub As New DataTable
        dtsub = dtPrescriber.Clone

        Try
            nToCopyRecords = increment
            If Not IsNothing(dtsub) Then
                While nFromCopyRecords < dtPrescriber.Rows.Count
                    dtsub.Clear()
                    If dtPrescriber.Rows.Count < nToCopyRecords Then
                        nToCopyRecords = dtPrescriber.Rows.Count
                    End If
                    For i As Integer = nFromCopyRecords To nToCopyRecords - 1
                        dtsub.ImportRow(dtPrescriber.Rows(i))
                    Next
                    oDBLayer.Connect(False)
                    oDBParameters.Clear()
                    oDBParameters.Add("@TmpContact_Mst", dtsub, ParameterDirection.Input, SqlDbType.Structured)
                    oDBLayer.Execute("gsp_UpdatePrescriber", oDBParameters, objResult)
                    oDBLayer.Disconnect()
                    nFromCopyRecords = nToCopyRecords
                    nToCopyRecords = nToCopyRecords + increment
                End While
            End If
        Catch ex As SqlException
            mdlGeneral.UpdateLog("DBError in Updation of Prescriber :" & ex.ToString, strConnection)
            Return False
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error in Updation of Prescriber :" & ex.ToString, strConnection)
            Return False
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If
            If Not IsNothing(dtsub) Then
                dtsub.Dispose()
                dtsub = Nothing
            End If
        End Try
        Return True
    End Function

    Private Function InsertAllPrescribers(ByVal dtPrescriber As DataTable, ByVal strConnection As String) As Boolean
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(strConnection)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim objResult As Object = Nothing
        Try
            oDBLayer.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@TmpContact_Mst", dtPrescriber, ParameterDirection.Input, SqlDbType.Structured)
            oDBLayer.Execute("gsp_UpdatePrescriber", oDBParameters, objResult)
            oDBLayer.Disconnect()
        Catch ex As SqlException
            mdlGeneral.UpdateLog("DBError in Updation of Prescriber :" & ex.ToString, strConnection)
            Return False
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error in Updation of Prescriber :" & ex.ToString, strConnection)
            Return False
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If
        End Try
        Return True
    End Function

    Private Function GetPrescriberAndPharmacyUpdateCount() As String
        Dim ds As New DataSet
        Dim oDB As New gloDatabaseLayer.DBLayer(strgloServiceConnection)
        Try

            oDB.Connect(False)
            oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName = 'PrescriberAndPharmacyUpdateCount'", ds)
            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0)("sSettingsValue").ToString()
            Else
                Return "0"
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error in retrieving get prescriber and pharmacy records update count")
            Return 0
        End Try

    End Function

    Private Function Get_UniqueueId(ByVal strDBConnection As String) As Long
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(strDBConnection)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim objResult As Object = Nothing
        Try
            oDBLayer.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@ID", "0", ParameterDirection.Output, SqlDbType.BigInt)
            oDBLayer.Execute("gsp_GetUniqueID", oDBParameters, objResult)
            oDBLayer.Disconnect()
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error in generation of UniqueId :" & ex.ToString, strDBConnection)
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
            End If
        End Try
        Return Convert.ToInt64(objResult)
    End Function

    'code added to access WCF service binding runtime
    'Public Class ServiceClass
    '    Private Sub New()
    '    End Sub

    '    Friend Shared Function GetWCFSvc(ByVal siteUrl As String) As eRxWCFStaging.IeRxClient
    '        Dim client As eRxWCFStaging.IeRxClient = Nothing
    '        Try
    '            Dim serviceUri As New Uri(siteUrl)
    '            Dim endpointAddress As New ServiceModel.EndpointAddress(serviceUri)
    '            'Create the binding here
    '            Dim binding As WSHttpBinding = BindingFactory.CreateInstance()
    '            client = New eRxWCFStaging.IeRxClient(binding, endpointAddress)
    '            Return client
    '        Catch ex As Exception
    '            Throw ex
    '            Return client
    '        End Try
    '    End Function
    'End Class

    'Friend NotInheritable Class BindingFactory
    '    Private Sub New()
    '    End Sub
    '    Friend Shared Function CreateInstance() As WSHttpBinding
    '        Dim binding As New WSHttpBinding()
    '        Try
    '            binding.Security.Mode = SecurityMode.None
    '            binding.ReliableSession.Enabled = True
    '            binding.ReliableSession.InactivityTimeout = New TimeSpan(0, 10, 0)
    '            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None
    '            binding.UseDefaultWebProxy = True
    '            binding.OpenTimeout = New TimeSpan(0, 10, 0)
    '            binding.CloseTimeout = New TimeSpan(0, 10, 0)
    '            binding.SendTimeout = New TimeSpan(0, 10, 0)
    '            binding.ReceiveTimeout = New TimeSpan(0, 10, 0)
    '            binding.MaxBufferPoolSize = "2147483647"
    '            binding.MaxReceivedMessageSize = "2147483647"
    '            binding.ReaderQuotas.MaxArrayLength = "2147483647"
    '            Return binding
    '        Catch ex As Exception
    '            Throw ex
    '            Return binding
    '        Finally
    '            If Not IsNothing(binding) Then
    '                binding = Nothing
    '            End If
    '        End Try
    '    End Function

    'End Class

    Function GenerateEpcsAuditLog(ByVal currentTime As DateTime)
        Dim i As Integer = 0
        Dim placeholderforBoldFont As String = "<!@#$%^Font-Bold!@#$%^>"
        Dim dtClinicDetails As DataTable = Nothing
        Try
            'mdlGeneral.UpdateLog("EpcsAuditLog Start.....")
            mdlGeneral.SetDbCredentials()
            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
                Dim objSendEncryption As clsEncryption = New clsEncryption
                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword

                Dim ds As New DataSet
                Dim oDB As New gloDatabaseLayer.DBLayer(strgloServiceConnection)
                oDB.Connect(False)
                oDB.Retrive_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM GLSettings WITH (NOLOCK) WHERE sSettingsName in ('EnableDirectory4dot4','eRxStagingWebserviceURL','eRx10dot6StagingWebserviceURL') ORDER BY sSettingsName", ds)
                oDB.Disconnect()
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If

                Dim dtAuditLog As DataTable = RetrieveAuditLog(currentTime.AddDays(-1).AddMinutes(-currentTime.Minute).AddSeconds(-currentTime.Second).AddMilliseconds(-currentTime.Millisecond), currentTime.AddMinutes(-currentTime.Minute).AddSeconds(-currentTime.Second).AddMilliseconds(-currentTime.Millisecond), strConnection)

                If Not dtAuditLog Is Nothing Then
                    If dtAuditLog.Rows.Count > 0 Then
                        If mdlGeneral.gblEpcsAuditLogPath <> "" Then
                            Dim strFolderName As String = GetFolderName()
                            Dim strUsername As String = ""
                            Dim strPassword As String = ""
                            Dim strDomainname As String = ""
                            If strFolderName <> "" Then
                                dtClinicDetails = Fill_SurescriptClinic(strConnection, 0)
                                Dim strlist As String() = mdlGeneral.gstrEpcsAuditlogUsername.Split("\")
                                If strlist.Length >= 1 Then
                                     strDomainname = strlist(0)
                                    strUsername = strlist(1)
                                    strPassword = mdlGeneral.gstrEpcsAuditlogPassword
                                End If
                                
                                Using unc As New ConnectUNCWithCredentials.UNCAccessWithCredentials()
                                    If unc.NetUseWithCredentials(mdlGeneral.gblEpcsAuditLogPath, strUsername, strDomainname, strPassword) Then
                                        If Not Directory.Exists(mdlGeneral.gblEpcsAuditLogPath + "\" + strFolderName) Then
                                            Directory.CreateDirectory(mdlGeneral.gblEpcsAuditLogPath + "\" + strFolderName)
                                        End If
                                        Dim filename As String = mdlGeneral.gblEpcsAuditLogPath + "\" + strFolderName + "\" + "EPCS Audit Report " + dtClinicDetails.Rows(0)("sClinicName") + " " + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + " " + DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond().ToString() + ".pdf"
                                        Dim pdfwrite As New gloPDFWriter.gloPdfWriter(filename, 842.0F, 1190.0F, 30.0F, 13.0F)
                                        Dim tblString As New StringBuilder
                                        mdlGeneral.UpdateLog("File Path " + mdlGeneral.gblEpcsAuditLogPath, strConnection)
                                        If Not IsNothing(dtClinicDetails) Then
                                            tblString.Append("Clinic Name: " + dtClinicDetails.Rows(0)("sClinicName") + "")
                                            tblString.Append(vbNewLine)
                                        End If

                                        tblString.Append("EPCS Audit Report from: " + currentTime.AddDays(-1).Date + " To " + currentTime.Date + "")
                                        tblString.Append(vbNewLine)
                                        tblString.Append("Generated: " + currentTime.ToString() + "")
                                        tblString.Append(vbNewLine)
                                        tblString.Append(vbNewLine)
                                        tblString.Append(vbNewLine)
                                        tblString.Append(placeholderforBoldFont)
                                        tblString.Append("DateTime   Action   User   Machine   Category  PatientCode  SoftwareComponent  Outcome  Description")
                                        tblString.Append(vbNewLine)
                                        tblString.Append(vbNewLine)

                                        For Each dRow As DataRow In dtAuditLog.Rows
                                            Dim firstColumn As Boolean = True
                                            For Each dbColumn As DataColumn In dtAuditLog.Columns
                                                If firstColumn Then
                                                    firstColumn = False
                                                Else

                                                    If dbColumn.ColumnName() <> "PatientName" Then
                                                        If dRow(dbColumn).ToString() = "" Then
                                                            tblString.Append("-----" + "    ")
                                                        Else
                                                            tblString.Append(dRow(dbColumn).ToString() + "    ")
                                                        End If
                                                    End If
                                                End If
                                            Next
                                            tblString.Append(vbNewLine)
                                            tblString.Append(vbNewLine)
                                            tblString.Append(vbNewLine)
                                        Next
                                        pdfwrite.Write(Encoding.ASCII.GetBytes(tblString.ToString()))
                                        mdlGeneral.UpdateLog("Epcs Audit Pdf Generated.", strConnection)
                                    Else
                                        If Not mdlGeneral.gblEpcsAuditLogPath.Trim.StartsWith("\\") Then
                                            If Not Directory.Exists(mdlGeneral.gblEpcsAuditLogPath + "\" + strFolderName) Then
                                                Directory.CreateDirectory(mdlGeneral.gblEpcsAuditLogPath + "\" + strFolderName)
                                            End If
                                            Dim filename As String = mdlGeneral.gblEpcsAuditLogPath + "\" + strFolderName + "\" + "EPCS Audit Report " + dtClinicDetails.Rows(0)("sClinicName") + " " + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + " " + DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond().ToString() + ".pdf"
                                            Dim pdfwrite As New gloPDFWriter.gloPdfWriter(filename, 842.0F, 1190.0F, 30.0F, 13.0F)
                                            Dim tblString As New StringBuilder
                                            mdlGeneral.UpdateLog("File Path " + mdlGeneral.gblEpcsAuditLogPath, strConnection)
                                            If Not IsNothing(dtClinicDetails) Then
                                                tblString.Append("Clinic Name: " + dtClinicDetails.Rows(0)("sClinicName") + "")
                                                tblString.Append(vbNewLine)
                                            End If

                                            tblString.Append("EPCS Audit Report from: " + currentTime.AddDays(-1).Date + " To " + currentTime.Date + "")
                                            tblString.Append(vbNewLine)
                                            tblString.Append("Generated: " + currentTime.ToString() + "")
                                            tblString.Append(vbNewLine)
                                            tblString.Append(vbNewLine)
                                            tblString.Append(vbNewLine)
                                            tblString.Append(placeholderforBoldFont)
                                            tblString.Append("DateTime   Action   User   Machine   Category  PatientCode  SoftwareComponent  Outcome  Description")
                                            tblString.Append(vbNewLine)
                                            tblString.Append(vbNewLine)

                                            For Each dRow As DataRow In dtAuditLog.Rows
                                                Dim firstColumn As Boolean = True
                                                For Each dbColumn As DataColumn In dtAuditLog.Columns
                                                    If firstColumn Then
                                                        firstColumn = False
                                                    Else

                                                        If dbColumn.ColumnName() <> "PatientName" Then
                                                            If dRow(dbColumn).ToString() = "" Then
                                                                tblString.Append("-----" + "    ")
                                                            Else
                                                                tblString.Append(dRow(dbColumn).ToString() + "    ")
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                                tblString.Append(vbNewLine)
                                                tblString.Append(vbNewLine)
                                                tblString.Append(vbNewLine)
                                            Next
                                            pdfwrite.Write(Encoding.ASCII.GetBytes(tblString.ToString()))
                                            mdlGeneral.UpdateLog("Epcs Audit Pdf Generated.", strConnection)
                                        Else
                                            mdlGeneral.UpdateLog("Not connected to " + "(" + mdlGeneral.gstrEpcsAuditlogUsername + ")", strConnection)
                                        End If
                                    End If
                                End Using
                            Else
                                mdlGeneral.UpdateLog("Unable to create folder.", strConnection)
                            End If
                        End If
                    Else
                        mdlGeneral.UpdateLog("No Data for selcted time frame in Database.", strConnection)
                    End If
                Else
                    mdlGeneral.UpdateLog("Data Table is null.", strConnection)
                End If
                If Not IsNothing(dtClinicDetails) Then
                    dtClinicDetails.Dispose()
                    dtClinicDetails = Nothing
                End If
                If Not IsNothing(dtAuditLog) Then
                    dtAuditLog.Dispose()
                    dtAuditLog = Nothing
                End If
                If Not IsNothing(ds) Then
                    ds.Dispose()
                    ds = Nothing
                End If
                If Not IsNothing(objSendEncryption) Then
                    objSendEncryption = Nothing
                End If
                i = (i + 1)
            Loop
            i = 0
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error during Loading Epcs Audit Log :" & ex.ToString, strConnection)
            Return Nothing
        End Try
        Return Nothing
    End Function

    Private Function RetrieveAuditLog(lastDate As DateTime, currentDate As DateTime, strConnection As String) As DataTable
        Dim dsAuditReports As New DataSet
        Dim objCon As New SqlConnection
        objCon.ConnectionString = strConnection
        Dim objCmd As New SqlCommand
        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanAuditTrailsForEpcs"
            objCmd.Parameters.Clear()
            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = lastDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFromDate)

            Dim objParaToDate As New SqlParameter
            With objParaToDate
                .ParameterName = "@ToDate"
                .Value = currentDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaToDate)

            Dim objParaUser As New SqlParameter
            With objParaUser
                .ParameterName = "@UserName"
                .Value = "All" 'strUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaUser)

            Dim objParaCategory As New SqlParameter
            With objParaCategory
                .ParameterName = "@Category"
                .Value = "All" 'sCategory
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCategory)

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objCon.Close()
            objCmd.CommandTimeout = 0
            objDA.Fill(dsAuditReports)
            objCon = Nothing
            If dsAuditReports.Tables.Count() > 0 Then
                Return dsAuditReports.Tables(0)
            Else
                Return Nothing
            End If


        Catch ex As Exception
            mdlGeneral.UpdateLog("Error during Loading AuditLog :" & ex.ToString, strConnection)
            Return Nothing
        End Try
    End Function

    Public Function GetFolderName() As String
        ''Dim _dtCurrentDateTime As String = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")
        Dim _dtCurrentDateTime As String = DateTime.Now.ToString("yyyy/MM/dd")

        Dim _arrdtCurrentdateTime() As String = _dtCurrentDateTime.Split("/")
        If _arrdtCurrentdateTime.Length > 0 Then
            Return _arrdtCurrentdateTime(0) & _arrdtCurrentdateTime(1) & _arrdtCurrentdateTime(2)
        Else
            Return ""
        End If

        '' Return _dtCurrentDateTime.Year & _dtCurrentDateTime.Month & _dtCurrentDateTime.Day
    End Function

#Region "WSUpdatePrescriptionStatus"

    Public Sub PostPrescriptionUpdateStatus(ByVal messageRow As DataRow, ByVal isStaging As Boolean, ByVal connectionString As String)
        Dim statusRequest As New EpcsRequest
        Dim oSSGenral As New gloSurescriptGeneral
        Dim objHelper As New clsEPCSHelper
        Dim responseData As Object = Nothing
        Dim responseFile As String = ""
        Dim requestFile As String = ""
        Dim messageID As String = ""
        Dim messageDataFile As String = ""
        Dim statusResult As String = String.Empty

        Try
            If messageRow.Table.Columns.Contains("sRelativeMessageID") AndAlso Not IsDBNull(messageRow("sRelativeMessageID")) Then
                messageID = messageRow("sRelativeMessageID")
                If String.IsNullOrEmpty(messageID) Then
                    Exit Sub
                End If
            End If

            If messageRow("sMsgType") = "Verify" OrElse messageRow("sMsgType") = "Error" Then
                If IsEPCSMessage(messageID, connectionString) Then
                    If messageRow.Table.Columns.Contains("FileData") AndAlso Not IsDBNull(messageRow("FileData")) Then
                        messageDataFile = GetMessageDataFile(messageRow("FileData"))
                    End If
                End If
            End If
            If Not String.IsNullOrEmpty(messageDataFile) Then
                statusRequest.RequestHeder = GetRequestHeader(isStaging, connectionString)
                If statusRequest.RequestHeder IsNot Nothing Then
                    statusRequest.RequestBody = messageDataFile
                    requestFile = objHelper.GenerateWSUpdatePrescriptionStatus(statusRequest)
                    gloSurescriptGeneral.gstrAUSIDClinic = statusRequest.RequestHeder.SourceOrganizationId
                    Dim statusResp As String = String.Empty
                    For postCnt As Integer = 0 To 3
                        responseData = oSSGenral.ConvertFiletoBinaryEpcs(requestFile, "WSUpdatePrescriptionStatus")
                        responseFile = oSSGenral.ExtractXMLEpcs(responseData)
                        If Not String.IsNullOrEmpty(responseFile) Then
                            statusResp = ReadUpdateStatusResponse(responseFile)
                        End If
                        If statusResp.Equals("ok") Then
                            Exit For
                        Else
                            Continue For
                        End If
                    Next
                    mdlGeneral.UpdateLog("WSUpdatePrescriptionStatus  " + "  msg id : " + messageID + "   msg type : " + messageRow("sMsgType") + "   status : " + statusResp, connectionString)
                End If
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error in PostPrescriptionUpdateStatus : " + ex.ToString(), connectionString)
        Finally
            If File.Exists(requestFile) Then
                File.Delete(requestFile)
            End If
            If File.Exists(responseFile) Then
                File.Delete(responseFile)
            End If
            If File.Exists(messageDataFile) Then
                File.Delete(messageDataFile)
            End If
            If statusRequest IsNot Nothing Then
                statusRequest = Nothing
            End If

            If objHelper IsNot Nothing Then
                objHelper = Nothing
            End If
            If oSSGenral IsNot Nothing Then
                oSSGenral = Nothing
            End If
            If responseData IsNot Nothing Then
                responseData = Nothing
            End If
        End Try
    End Sub

    Public Function GetRequestHeader(ByVal isStaging As Boolean, ByVal constringLocal As String) As clsEpcsRequestHeder
        Dim requestHeader As New clsEpcsRequestHeder

        Dim dtTable As New DataTable
        Dim StagingVendorName As String = ""
        Dim StagingVendorLabel As String = ""
        Dim StagingVendorNodeName As String = ""
        Dim StagingVendorNodeLabel As String = ""
        Dim StagingEpcsUrl As String = ""
        Dim StagingsharedSecret As String = ""

        Dim ProductionVendorName As String = ""
        Dim ProductionVendorLabel As String = ""
        Dim ProductionVendorNodeName As String = ""
        Dim ProductionVendorNodeLabel As String = ""
        Dim ProductionEpcsUrl As String = ""
        Dim ProductionSharedSecret As String = ""
        Dim RouterName As String = ""
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = constringLocal
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetVendorInformationForEpcs"
            objCmd.Connection = objCon

            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dtTable)
            objCon.Close()

            objCon.Dispose() : objCon = Nothing

            objCmd.Parameters.Clear()
            objCmd.Dispose() : objCmd = Nothing

            objDA.Dispose() : objDA = Nothing

            Dim nCount As Integer
            For nCount = 0 To dtTable.Rows.Count - 1
                Select Case dtTable.Rows(nCount).Item(1).ToString.ToUpper
                    Case "StagingVendorName".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingVendorName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingVendorName = ""
                        End If
                    Case "StagingVendorLabel".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingVendorLabel = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingVendorLabel = ""
                        End If

                    Case "StagingVendorNodeName".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingVendorNodeName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingVendorNodeName = ""
                        End If
                    Case "StagingVendorNodeLabel".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingVendorNodeLabel = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingVendorNodeLabel = ""
                        End If

                    Case "StagingEpcsUrl".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingEpcsUrl = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingEpcsUrl = ""
                        End If

                    Case "StagingSharedSecret".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingsharedSecret = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingsharedSecret = ""
                        End If

                    Case "ProductionVendorName".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionVendorName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionVendorName = ""
                        End If

                    Case "ProductionVendorLabel".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionVendorLabel = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionVendorLabel = ""
                        End If

                    Case "ProductionVendorNodeName".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionVendorNodeName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionVendorNodeName = ""
                        End If

                    Case "ProductionVendorNodeLabel".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionVendorNodeLabel = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionVendorNodeLabel = ""
                        End If

                    Case "ProductionEpcsUrl".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionEpcsUrl = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionEpcsUrl = ""
                        End If

                    Case "ProductionSharedSecret".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionSharedSecret = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionSharedSecret = ""
                        End If
                    Case "RouterName".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            RouterName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            RouterName = ""
                        End If

                End Select
            Next

            requestHeader.ApplicationVersion = System.Windows.Forms.Application.ProductVersion

            If isStaging Then
                requestHeader.VendorName = StagingVendorName
                requestHeader.VendorLabel = StagingVendorLabel
                requestHeader.VendorNodeName = StagingVendorNodeName
                requestHeader.VendorNodeLabel = StagingVendorNodeLabel
            Else
                requestHeader.VendorName = ProductionVendorName
                requestHeader.VendorLabel = ProductionVendorLabel
                requestHeader.VendorNodeName = ProductionVendorNodeName
                requestHeader.VendorNodeLabel = ProductionVendorNodeLabel
            End If

            Using dtClinicDetails As DataTable = Fill_SurescriptClinic(constringLocal, 0)
                If Not IsNothing(dtClinicDetails) Then
                    If dtClinicDetails.Rows.Count > 0 Then
                        requestHeader.AppName = dtClinicDetails.Rows(0)("sClinicName").ToString()
                        requestHeader.OrganizationName = requestHeader.AppName
                        requestHeader.OrganizationLabel = requestHeader.AppName
                        requestHeader.SourceOrganizationId = dtClinicDetails.Rows(0)("AUSID").ToString()
                    End If
                End If
            End Using

            Return requestHeader

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error in GetRequestHeader " + ex.ToString(), constringLocal)
            Return Nothing
        Finally
            If IsNothing(dtTable) = False Then
                dtTable.Dispose() : dtTable = Nothing
            End If
        End Try

    End Function

    Private Function GetMessageDataFile(FileData As Object) As String
        Dim fileDataBytes As Byte() = Nothing
        Dim requestFile As String = String.Empty

        Try
            If FileData IsNot Nothing Then
                Try
                    fileDataBytes = Convert.FromBase64String(FileData)
                Catch ex As Exception
                    Try
                        fileDataBytes = Convert.FromBase64String(FileData)
                    Catch exx As Exception
                        mdlGeneral.UpdateLog("Error in GetMessageDataFile " + exx.ToString(), "")
                    End Try
                End Try
            End If

            If fileDataBytes IsNot Nothing Then
                If Not Directory.Exists(gloSettings.FolderSettings.AppTempFolderPath + "Inbox") Then
                    Directory.CreateDirectory(gloSettings.FolderSettings.AppTempFolderPath + "Inbox")
                End If
                requestFile = GetFileName(gloSettings.FolderSettings.AppTempFolderPath + "Inbox")
                ConvertBinarytoFile(fileDataBytes, requestFile)
            End If

            Return requestFile

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error in GetMessageDataFile " + ex.ToString() + " " + requestFile, "")
            Return Nothing
        Finally
            fileDataBytes = Nothing
        End Try
    End Function

    Private Function IsEPCSMessage(ByVal messagID As String, ByVal constr As String) As Integer
        Try
            Dim sqlquery As String = "SELECT Pres.nIsNarcotic FROM dbo.SureScriptMessageTransaction AS SMT INNER JOIN dbo.Prescription AS Pres ON CONVERT(VARCHAR,Pres.nPrescriptionID) = smt.sReferenceNumber INNER JOIN dbo.Drugs_MST AS DM ON DM.mpid = Pres.mpid WHERE   SMT.nMessageID ='" + messagID + "' or  SMT.sRelatesToMessageID ='" + messagID + "'"
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand(sqlquery, con)
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Dim _result As Object = cmd.ExecuteScalar()
                    If _result IsNot Nothing AndAlso Convert.ToInt16(_result) > 1 Then
                        Return True
                    End If
                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            mdlGeneral.UpdateLog(" Error in IsEPCSMessage " + ex.ToString(), constr)
        End Try
        Return False
    End Function

    Private Function RemovedrfNamespace(xml As String) As String
        Dim str1 As String = xml.Replace("encoding=""UTF-8"" standalone=""yes""", "")
        Dim st As String = str1.Replace("xmlns:drf=""urn:drfirst.com:epcsapi:v1_0"" xmlns:ss=""http://www.surescripts.com/messaging""", "")

        xml = st.Replace("drf:", "")
        Return xml
    End Function

    Private Function ReadUpdateStatusResponse(ByVal responseFile As String) As String
        Dim respResult As String = String.Empty
        Dim sXml As String = String.Empty
        Dim xdoc As Xml.XmlDocument = Nothing
        Dim statuslist As XmlNodeList = Nothing
        Try
            Using reader As New StreamReader(responseFile, Encoding.Unicode, True)
                sXml = reader.ReadToEnd()
                reader.Close()
            End Using
            xdoc = New Xml.XmlDocument()
            xdoc.LoadXml(RemovedrfNamespace(sXml))
            statuslist = xdoc.DocumentElement.SelectNodes("/EpcsResponse/EpcsResponseBody/WsUpdatePrescriptionStatusResponse/Success")
            If Not IsNothing(statuslist) Then
                If statuslist.Count <> 0 Then
                    respResult = statuslist.Item(0).InnerText
                End If
            End If
            If String.IsNullOrEmpty(respResult) Then
                If xdoc.GetElementsByTagName("ErrorText").Count > 0 Then
                    respResult = "Error : " & xdoc.GetElementsByTagName("ErrorText").Item(0).InnerText.ToString()
                End If
                If xdoc.GetElementsByTagName("ErrorCode").Count > 0 Then
                    respResult += " ErrorCode : " & xdoc.GetElementsByTagName("ErrorCode").Item(0).InnerText.ToString()
                End If
            End If
            Return respResult
        Catch ex As Exception
            Return ex.ToString()
        Finally
            If Not IsNothing(xdoc) Then
                xdoc = Nothing
            End If
            If Not IsNothing(statuslist) Then
                statuslist = Nothing
            End If
        End Try

    End Function
#End Region

    Private Function GetMessage(ByVal XML As String) As gloGlobal.Schemas.Surescript.MessageType
        Dim xmlDeserializer As XmlSerializer = Nothing
        Dim message As New gloGlobal.Schemas.Surescript.MessageType()

        Try
            xmlDeserializer = New XmlSerializer(GetType(gloGlobal.Schemas.Surescript.MessageType))            

            Using StringReader As New StringReader(XML)
                message = xmlDeserializer.Deserialize(StringReader)
            End Using

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        End Try

        Return message
    End Function

    Private Sub InsertRxChangeRequestDetails(dtRow As DataRow, ByVal ConnectionString As String)
        Dim message As Schema.MessageType
        Dim RxMessage As Schema.RxChangeRequest = Nothing

        Dim phonenumber As String = Nothing
        Dim fax As String = Nothing
        Dim email As String = Nothing
        Dim WorkPhone As String = Nothing

        Try

            message = Me.GetMessage(dtRow("FileXML"))

            If TypeOf (message.Body.Item) Is Schema.RxChangeRequest Then
                RxMessage = DirectCast(message.Body.Item, Schema.RxChangeRequest)
            End If

            Using objCon As New SqlConnection(ConnectionString)
                Using objCmd As New SqlCommand("sc_InsertRxMessageDetails", objCon)
                    objCmd.CommandType = CommandType.StoredProcedure

                    objCmd.Parameters.Add("@nMessageID", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sFillStatus", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sChangeRequestType", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientFirstName", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientLastName", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientMiddleName", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientSuffix", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientPrefix", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientGender", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientDOB", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientAddress1", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientAddress2", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientCity", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientState", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientZipCode", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientPhone", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientPhQualifier", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientEmail", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientFax", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientWorkPhone", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sDrugQuantity", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sDrugQualifier", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sRefillQuantity", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sRefillsQualifier", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@dtWrittenDate", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sDrugDuration", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sDrugDirections", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@bIsSubstituitons", SqlDbType.Bit)

                    objCmd.Parameters.Add("@sNotes", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sProductCode", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sProductCodeQualifier", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sDosageForm", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sDrugStrength", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sDrugStrengthUnits", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sCodeListQualifier", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sDrugDBCode", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sDrugDBCodeQualifier", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sUnitSourceCode", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPotencyUnitCode", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sStatus", SqlDbType.VarChar)

                    objCmd.Parameters("@nMessageID").Value = dtRow("sMessageID")
                    objCmd.Parameters("@sStatus").Value = "Pending"

                    objCmd.Parameters("@sFillStatus").Value = DBNull.Value

                    If RxMessage.Request IsNot Nothing Then
                        If (RxMessage.Request.ChangeRequestType Is Nothing = False) AndAlso (IsDBNull(RxMessage.Request.ChangeRequestType) = False) Then
                            objCmd.Parameters("@sChangeRequestType").Value = RxMessage.Request.ChangeRequestType
                        Else
                            objCmd.Parameters("@sChangeRequestType").Value = DBNull.Value
                        End If
                    End If

                    If RxMessage.Patient IsNot Nothing Then
                        If RxMessage.Patient.Name IsNot Nothing Then
                            If (RxMessage.Patient.Name.FirstName Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Name.FirstName) = False) Then
                                objCmd.Parameters("@sPatientFirstName").Value = RxMessage.Patient.Name.FirstName
                            Else
                                objCmd.Parameters("@sPatientFirstName").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Name.LastName Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Name.LastName) = False) Then
                                objCmd.Parameters("@sPatientLastName").Value = RxMessage.Patient.Name.LastName
                            Else
                                objCmd.Parameters("@sPatientLastName").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Name.MiddleName Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Name.MiddleName) = False) Then
                                objCmd.Parameters("@sPatientMiddleName").Value = RxMessage.Patient.Name.MiddleName
                            Else
                                objCmd.Parameters("@sPatientMiddleName").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Name.Suffix Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Name.Suffix) = False) Then
                                objCmd.Parameters("@sPatientSuffix").Value = RxMessage.Patient.Name.Suffix
                            Else
                                objCmd.Parameters("@sPatientSuffix").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Name.Prefix Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Name.Prefix) = False) Then
                                objCmd.Parameters("@sPatientPrefix").Value = RxMessage.Patient.Name.Prefix
                            Else
                                objCmd.Parameters("@sPatientPrefix").Value = DBNull.Value
                            End If
                        End If

                        If (RxMessage.Patient.Gender Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Gender) = False) Then
                            objCmd.Parameters("@sPatientGender").Value = RxMessage.Patient.Gender
                        Else
                            objCmd.Parameters("@sPatientGender").Value = DBNull.Value
                        End If

                        If (RxMessage.Patient.DateOfBirth Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.DateOfBirth) = False) Then
                            objCmd.Parameters("@sPatientDOB").Value = RxMessage.Patient.DateOfBirth.Item
                        Else
                            objCmd.Parameters("@sPatientDOB").Value = DBNull.Value
                        End If

                        If RxMessage.Patient.Address IsNot Nothing Then

                            If (RxMessage.Patient.Address.AddressLine1 Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Address.AddressLine1) = False) Then
                                objCmd.Parameters("@sPatientAddress1").Value = RxMessage.Patient.Address.AddressLine1
                            Else
                                objCmd.Parameters("@sPatientAddress1").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Address.AddressLine2 Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Address.AddressLine2) = False) Then
                                objCmd.Parameters("@sPatientAddress2").Value = RxMessage.Patient.Address.AddressLine2
                            Else
                                objCmd.Parameters("@sPatientAddress2").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Address.City Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Address.City) = False) Then
                                objCmd.Parameters("@sPatientCity").Value = RxMessage.Patient.Address.City
                            Else
                                objCmd.Parameters("@sPatientCity").Value = DBNull.Value
                            End If


                            If (RxMessage.Patient.Address.State Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Address.State) = False) Then
                                objCmd.Parameters("@sPatientState").Value = RxMessage.Patient.Address.State
                            Else
                                objCmd.Parameters("@sPatientState").Value = DBNull.Value
                            End If


                            If (RxMessage.Patient.Address.ZipCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Address.ZipCode) = False) Then
                                objCmd.Parameters("@sPatientZipCode").Value = RxMessage.Patient.Address.ZipCode
                            Else
                                objCmd.Parameters("@sPatientZipCode").Value = DBNull.Value
                            End If
                        End If

                        If RxMessage.Patient.CommunicationNumbers IsNot Nothing Then
                            If RxMessage.Patient.CommunicationNumbers IsNot Nothing Then
                                For i As Integer = 0 To RxMessage.Patient.CommunicationNumbers.Length - 1

                                    If RxMessage.Patient.CommunicationNumbers(i).Qualifier = "TE" Then
                                        phonenumber = RxMessage.Patient.CommunicationNumbers(i).Number
                                    ElseIf RxMessage.Patient.CommunicationNumbers(i).Qualifier = "FX" Then
                                        fax = RxMessage.Patient.CommunicationNumbers(i).Number
                                    ElseIf RxMessage.Patient.CommunicationNumbers(i).Qualifier = "EM" Then
                                        email = RxMessage.Patient.CommunicationNumbers(i).Number
                                    ElseIf RxMessage.Patient.CommunicationNumbers(i).Qualifier = "WP" Then
                                        WorkPhone = RxMessage.Patient.CommunicationNumbers(i).Number
                                    End If
                                Next
                            End If

                            If (phonenumber Is Nothing = False) AndAlso (IsDBNull(phonenumber) = False) Then
                                objCmd.Parameters("@sPatientPhone").Value = phonenumber
                            Else
                                objCmd.Parameters("@sPatientPhone").Value = DBNull.Value
                            End If


                            If (phonenumber Is Nothing = False) AndAlso (IsDBNull(phonenumber) = False) Then
                                objCmd.Parameters("@sPatientPhQualifier").Value = phonenumber
                            Else
                                objCmd.Parameters("@sPatientPhQualifier").Value = DBNull.Value
                            End If

                            If (email Is Nothing = False) AndAlso (IsDBNull(email) = False) Then
                                objCmd.Parameters("@sPatientEmail").Value = email
                            Else
                                objCmd.Parameters("@sPatientEmail").Value = DBNull.Value
                            End If

                            If (fax Is Nothing = False) AndAlso (IsDBNull(fax) = False) Then
                                objCmd.Parameters("@sPatientFax").Value = fax
                            Else
                                objCmd.Parameters("@sPatientFax").Value = DBNull.Value
                            End If


                            If (WorkPhone Is Nothing = False) AndAlso (IsDBNull(WorkPhone) = False) Then
                                objCmd.Parameters("@sPatientWorkPhone").Value = WorkPhone
                            Else
                                objCmd.Parameters("@sPatientWorkPhone").Value = DBNull.Value
                            End If
                        End If
                    End If

                    If RxMessage.MedicationPrescribed IsNot Nothing Then
                        If (RxMessage.MedicationPrescribed.DrugDescription Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugDescription) = False) Then
                            objCmd.Parameters("@sDrugName").Value = RxMessage.MedicationPrescribed.DrugDescription
                        Else
                            objCmd.Parameters("@sDrugName").Value = DBNull.Value
                        End If


                        If (RxMessage.MedicationPrescribed.Quantity Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Quantity) = False) Then
                            objCmd.Parameters("@sDrugQuantity").Value = RxMessage.MedicationPrescribed.Quantity.Value
                        Else
                            objCmd.Parameters("@sDrugQuantity").Value = DBNull.Value
                        End If


                        objCmd.Parameters("@sDrugQualifier").Value = DBNull.Value


                        If RxMessage.MedicationPrescribed.Refills IsNot Nothing Then


                            If (RxMessage.MedicationPrescribed.Refills Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Refills) = False) Then
                                If Not IsNothing(RxMessage.MedicationPrescribed.Refills.Value) Then
                                    objCmd.Parameters("@sRefillQuantity").Value = RxMessage.MedicationPrescribed.Refills.Value
                                Else
                                    objCmd.Parameters("@sRefillQuantity").Value = DBNull.Value
                                End If
                            Else
                                objCmd.Parameters("@sRefillQuantity").Value = DBNull.Value
                            End If


                            If (RxMessage.MedicationPrescribed.Refills.Qualifier Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Refills.Qualifier) = False) Then
                                objCmd.Parameters("@sRefillsQualifier").Value = RxMessage.MedicationPrescribed.Refills.Qualifier
                            Else
                                objCmd.Parameters("@sRefillsQualifier").Value = DBNull.Value
                            End If

                        End If

                        If (RxMessage.MedicationPrescribed.WrittenDate Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.WrittenDate) = False) Then
                            objCmd.Parameters("@dtWrittenDate").Value = RxMessage.MedicationPrescribed.WrittenDate.Item
                        Else
                            objCmd.Parameters("@dtWrittenDate").Value = DBNull.Value
                        End If


                        If (RxMessage.MedicationPrescribed.DaysSupply Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DaysSupply) = False) Then
                            objCmd.Parameters("@sDrugDuration").Value = RxMessage.MedicationPrescribed.DaysSupply
                        Else
                            objCmd.Parameters("@sDrugDuration").Value = DBNull.Value
                        End If


                        If (RxMessage.MedicationPrescribed.Directions Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Directions) = False) Then
                            objCmd.Parameters("@sDrugDirections").Value = RxMessage.MedicationPrescribed.Directions
                        Else
                            objCmd.Parameters("@sDrugDirections").Value = DBNull.Value
                        End If


                        If IsDBNull(RxMessage.MedicationPrescribed.Substitutions) = False Then
                            objCmd.Parameters("@bIsSubstituitons").Value = Convert.ToBoolean(Convert.ToInt16(RxMessage.MedicationPrescribed.Substitutions))
                        Else
                            objCmd.Parameters("@bIsSubstituitons").Value = DBNull.Value
                        End If


                        If (RxMessage.MedicationPrescribed.Note Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Note) = False) Then
                            objCmd.Parameters("@sNotes").Value = RxMessage.MedicationPrescribed.Note
                        Else
                            objCmd.Parameters("@sNotes").Value = DBNull.Value
                        End If

                        If RxMessage.MedicationPrescribed.DrugCoded IsNot Nothing Then

                            If (RxMessage.MedicationPrescribed.DrugCoded.ProductCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.ProductCode) = False) Then
                                objCmd.Parameters("@sProductCode").Value = RxMessage.MedicationPrescribed.DrugCoded.ProductCode
                            Else
                                objCmd.Parameters("@sProductCode").Value = DBNull.Value
                            End If


                            If (RxMessage.MedicationPrescribed.DrugCoded.ProductCodeQualifier Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.ProductCodeQualifier) = False) Then
                                objCmd.Parameters("@sProductCodeQualifier").Value = RxMessage.MedicationPrescribed.DrugCoded.ProductCodeQualifier
                            Else
                                objCmd.Parameters("@sProductCodeQualifier").Value = DBNull.Value
                            End If


                            objCmd.Parameters("@sDosageForm").Value = DBNull.Value


                            If (RxMessage.MedicationPrescribed.DrugCoded.Strength Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.Strength) = False) Then
                                objCmd.Parameters("@sDrugStrength").Value = RxMessage.MedicationPrescribed.DrugCoded.Strength
                            Else
                                objCmd.Parameters("@sDrugStrength").Value = DBNull.Value
                            End If


                            If (RxMessage.MedicationPrescribed.DrugCoded.StrengthCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.StrengthCode) = False) Then
                                objCmd.Parameters("@sDrugStrengthUnits").Value = RxMessage.MedicationPrescribed.DrugCoded.StrengthCode
                            Else
                                objCmd.Parameters("@sDrugStrengthUnits").Value = DBNull.Value
                            End If
                        End If

                        If RxMessage.MedicationPrescribed.Quantity IsNot Nothing Then


                            If (RxMessage.MedicationPrescribed.Quantity.CodeListQualifier Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Quantity.CodeListQualifier) = False) Then
                                objCmd.Parameters("@sCodeListQualifier").Value = RxMessage.MedicationPrescribed.Quantity.CodeListQualifier
                            Else
                                objCmd.Parameters("@sCodeListQualifier").Value = DBNull.Value
                            End If


                            If (RxMessage.MedicationPrescribed.Quantity.UnitSourceCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Quantity.UnitSourceCode) = False) Then
                                objCmd.Parameters("@sUnitSourceCode").Value = RxMessage.MedicationPrescribed.Quantity.UnitSourceCode
                            Else
                                objCmd.Parameters("@sUnitSourceCode").Value = DBNull.Value
                            End If

                            If (RxMessage.MedicationPrescribed.Quantity.PotencyUnitCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Quantity.PotencyUnitCode) = False) Then
                                objCmd.Parameters("@sPotencyUnitCode").Value = RxMessage.MedicationPrescribed.Quantity.PotencyUnitCode
                            Else
                                objCmd.Parameters("@sPotencyUnitCode").Value = DBNull.Value
                            End If
                        End If


                        If RxMessage.MedicationPrescribed.DrugCoded IsNot Nothing Then

                            If (RxMessage.MedicationPrescribed.DrugCoded.DrugDBCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.DrugDBCode) = False) Then
                                objCmd.Parameters("@sDrugDBCode").Value = RxMessage.MedicationPrescribed.DrugCoded.DrugDBCode
                            Else
                                objCmd.Parameters("@sDrugDBCode").Value = DBNull.Value
                            End If


                            If (RxMessage.MedicationPrescribed.DrugCoded.DrugDBCodeQualifier Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.DrugDBCodeQualifier) = False) Then
                                objCmd.Parameters("@sDrugDBCodeQualifier").Value = RxMessage.MedicationPrescribed.DrugCoded.DrugDBCodeQualifier
                            Else
                                objCmd.Parameters("@sDrugDBCodeQualifier").Value = DBNull.Value
                            End If
                        End If
                    End If

                    objCmd.Connection.Open()
                    objCmd.ExecuteNonQuery()
                    objCmd.Connection.Close()

                End Using
            End Using

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        End Try
    End Sub

    Private Sub InsertRxFillRequestDetails(dtRow As DataRow, ByVal ConnectionString As String)
        Dim message As Schema.MessageType
        Dim RxMessage As Schema.RxFill = Nothing

        Dim phonenumber As String = Nothing
        Dim fax As String = Nothing
        Dim email As String = Nothing
        Dim WorkPhone As String = Nothing

        Try

            message = Me.GetMessage(dtRow("FileXML"))

            If TypeOf (message.Body.Item) Is Schema.RxFill Then
                RxMessage = DirectCast(message.Body.Item, Schema.RxFill)
            End If

            Using objCon As New SqlConnection(ConnectionString)
                Using objCmd As New SqlCommand("sc_InsertRxMessageDetails", objCon)
                    objCmd.CommandType = CommandType.StoredProcedure

                    objCmd.Parameters.Add("@nMessageID", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sFillStatus", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sChangeRequestType", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientFirstName", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientLastName", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientMiddleName", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientSuffix", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientPrefix", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientGender", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientDOB", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientAddress1", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientAddress2", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientCity", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientState", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientZipCode", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientPhone", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientPhQualifier", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sPatientEmail", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientFax", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPatientWorkPhone", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sDrugQuantity", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sDrugQualifier", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sRefillQuantity", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sRefillsQualifier", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@dtWrittenDate", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sDrugDuration", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sDrugDirections", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@bIsSubstituitons", SqlDbType.Bit)

                    objCmd.Parameters.Add("@sNotes", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sProductCode", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sProductCodeQualifier", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sDosageForm", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sDrugStrength", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sDrugStrengthUnits", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sCodeListQualifier", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sDrugDBCode", SqlDbType.VarChar)

                    objCmd.Parameters.Add("@sDrugDBCodeQualifier", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sUnitSourceCode", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sPotencyUnitCode", SqlDbType.VarChar)
                    objCmd.Parameters.Add("@sStatus", SqlDbType.VarChar)

                    objCmd.Parameters("@nMessageID").Value = dtRow("sMessageID")
                    objCmd.Parameters("@sStatus").Value = "Pending"

                    objCmd.Parameters("@sChangeRequestType").Value = DBNull.Value
                    objCmd.Parameters("@sFillStatus").Value = DBNull.Value

                    If RxMessage.FillStatus IsNot Nothing Then
                        objCmd.Parameters("@sFillStatus").Value = RxMessage.FillStatus.ItemElementName.ToString()
                    End If

                    If RxMessage.Patient IsNot Nothing Then
                        If RxMessage.Patient.Name IsNot Nothing Then
                            If (RxMessage.Patient.Name.FirstName Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Name.FirstName) = False) Then
                                objCmd.Parameters("@sPatientFirstName").Value = RxMessage.Patient.Name.FirstName
                            Else
                                objCmd.Parameters("@sPatientFirstName").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Name.LastName Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Name.LastName) = False) Then
                                objCmd.Parameters("@sPatientLastName").Value = RxMessage.Patient.Name.LastName
                            Else
                                objCmd.Parameters("@sPatientLastName").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Name.MiddleName Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Name.MiddleName) = False) Then
                                objCmd.Parameters("@sPatientMiddleName").Value = RxMessage.Patient.Name.MiddleName
                            Else
                                objCmd.Parameters("@sPatientMiddleName").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Name.Suffix Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Name.Suffix) = False) Then
                                objCmd.Parameters("@sPatientSuffix").Value = RxMessage.Patient.Name.Suffix
                            Else
                                objCmd.Parameters("@sPatientSuffix").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Name.Prefix Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Name.Prefix) = False) Then
                                objCmd.Parameters("@sPatientPrefix").Value = RxMessage.Patient.Name.Prefix
                            Else
                                objCmd.Parameters("@sPatientPrefix").Value = DBNull.Value
                            End If
                        End If

                        If (RxMessage.Patient.Gender Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Gender) = False) Then
                            objCmd.Parameters("@sPatientGender").Value = RxMessage.Patient.Gender
                        Else
                            objCmd.Parameters("@sPatientGender").Value = DBNull.Value
                        End If

                        If (RxMessage.Patient.DateOfBirth Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.DateOfBirth) = False) Then
                            objCmd.Parameters("@sPatientDOB").Value = RxMessage.Patient.DateOfBirth.Item
                        Else
                            objCmd.Parameters("@sPatientDOB").Value = DBNull.Value
                        End If

                        If RxMessage.Patient.Address IsNot Nothing Then

                            If (RxMessage.Patient.Address.AddressLine1 Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Address.AddressLine1) = False) Then
                                objCmd.Parameters("@sPatientAddress1").Value = RxMessage.Patient.Address.AddressLine1
                            Else
                                objCmd.Parameters("@sPatientAddress1").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Address.AddressLine2 Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Address.AddressLine2) = False) Then
                                objCmd.Parameters("@sPatientAddress2").Value = RxMessage.Patient.Address.AddressLine2
                            Else
                                objCmd.Parameters("@sPatientAddress2").Value = DBNull.Value
                            End If

                            If (RxMessage.Patient.Address.City Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Address.City) = False) Then
                                objCmd.Parameters("@sPatientCity").Value = RxMessage.Patient.Address.City
                            Else
                                objCmd.Parameters("@sPatientCity").Value = DBNull.Value
                            End If


                            If (RxMessage.Patient.Address.State Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Address.State) = False) Then
                                objCmd.Parameters("@sPatientState").Value = RxMessage.Patient.Address.State
                            Else
                                objCmd.Parameters("@sPatientState").Value = DBNull.Value
                            End If


                            If (RxMessage.Patient.Address.ZipCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.Patient.Address.ZipCode) = False) Then
                                objCmd.Parameters("@sPatientZipCode").Value = RxMessage.Patient.Address.ZipCode
                            Else
                                objCmd.Parameters("@sPatientZipCode").Value = DBNull.Value
                            End If
                        End If

                        If RxMessage.Patient.CommunicationNumbers IsNot Nothing Then
                            If RxMessage.Patient.CommunicationNumbers IsNot Nothing Then
                                For i As Integer = 0 To RxMessage.Patient.CommunicationNumbers.Length - 1

                                    If RxMessage.Patient.CommunicationNumbers(i).Qualifier = "TE" Then
                                        phonenumber = RxMessage.Patient.CommunicationNumbers(i).Number
                                    ElseIf RxMessage.Patient.CommunicationNumbers(i).Qualifier = "FX" Then
                                        fax = RxMessage.Patient.CommunicationNumbers(i).Number
                                    ElseIf RxMessage.Patient.CommunicationNumbers(i).Qualifier = "EM" Then
                                        email = RxMessage.Patient.CommunicationNumbers(i).Number
                                    ElseIf RxMessage.Patient.CommunicationNumbers(i).Qualifier = "WP" Then
                                        WorkPhone = RxMessage.Patient.CommunicationNumbers(i).Number
                                    End If
                                Next
                            End If

                            If (phonenumber Is Nothing = False) AndAlso (IsDBNull(phonenumber) = False) Then
                                objCmd.Parameters("@sPatientPhone").Value = phonenumber
                            Else
                                objCmd.Parameters("@sPatientPhone").Value = DBNull.Value
                            End If


                            If (phonenumber Is Nothing = False) AndAlso (IsDBNull(phonenumber) = False) Then
                                objCmd.Parameters("@sPatientPhQualifier").Value = phonenumber
                            Else
                                objCmd.Parameters("@sPatientPhQualifier").Value = DBNull.Value
                            End If

                            If (email Is Nothing = False) AndAlso (IsDBNull(email) = False) Then
                                objCmd.Parameters("@sPatientEmail").Value = email
                            Else
                                objCmd.Parameters("@sPatientEmail").Value = DBNull.Value
                            End If

                            If (fax Is Nothing = False) AndAlso (IsDBNull(fax) = False) Then
                                objCmd.Parameters("@sPatientFax").Value = fax
                            Else
                                objCmd.Parameters("@sPatientFax").Value = DBNull.Value
                            End If


                            If (WorkPhone Is Nothing = False) AndAlso (IsDBNull(WorkPhone) = False) Then
                                objCmd.Parameters("@sPatientWorkPhone").Value = WorkPhone
                            Else
                                objCmd.Parameters("@sPatientWorkPhone").Value = DBNull.Value
                            End If
                        End If
                    End If

                    If RxMessage.MedicationPrescribed IsNot Nothing Then
                        If (RxMessage.MedicationPrescribed.DrugDescription Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugDescription) = False) Then
                            objCmd.Parameters("@sDrugName").Value = RxMessage.MedicationPrescribed.DrugDescription
                        Else
                            objCmd.Parameters("@sDrugName").Value = DBNull.Value
                        End If


                        If (RxMessage.MedicationPrescribed.Quantity Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Quantity) = False) Then
                            objCmd.Parameters("@sDrugQuantity").Value = RxMessage.MedicationPrescribed.Quantity.Value
                        Else
                            objCmd.Parameters("@sDrugQuantity").Value = DBNull.Value
                        End If


                        objCmd.Parameters("@sDrugQualifier").Value = DBNull.Value


                        If RxMessage.MedicationPrescribed.Refills IsNot Nothing Then


                            If (RxMessage.MedicationPrescribed.Refills Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Refills) = False) Then
                                If Not IsNothing(RxMessage.MedicationPrescribed.Refills.Value) Then
                                    objCmd.Parameters("@sRefillQuantity").Value = RxMessage.MedicationPrescribed.Refills.Value
                                Else
                                    objCmd.Parameters("@sRefillQuantity").Value = DBNull.Value
                                End If
                            Else
                                objCmd.Parameters("@sRefillQuantity").Value = DBNull.Value
                            End If


                            If (RxMessage.MedicationPrescribed.Refills.Qualifier Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Refills.Qualifier) = False) Then
                                objCmd.Parameters("@sRefillsQualifier").Value = RxMessage.MedicationPrescribed.Refills.Qualifier
                            Else
                                objCmd.Parameters("@sRefillsQualifier").Value = DBNull.Value
                            End If

                        End If

                        If (RxMessage.MedicationPrescribed.WrittenDate Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.WrittenDate) = False) Then
                            objCmd.Parameters("@dtWrittenDate").Value = RxMessage.MedicationPrescribed.WrittenDate.Item
                        Else
                            objCmd.Parameters("@dtWrittenDate").Value = DBNull.Value
                        End If


                        If (RxMessage.MedicationPrescribed.DaysSupply Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DaysSupply) = False) Then
                            objCmd.Parameters("@sDrugDuration").Value = RxMessage.MedicationPrescribed.DaysSupply
                        Else
                            objCmd.Parameters("@sDrugDuration").Value = DBNull.Value
                        End If


                        If (RxMessage.MedicationPrescribed.Directions Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Directions) = False) Then
                            objCmd.Parameters("@sDrugDirections").Value = RxMessage.MedicationPrescribed.Directions
                        Else
                            objCmd.Parameters("@sDrugDirections").Value = DBNull.Value
                        End If


                        If IsDBNull(RxMessage.MedicationPrescribed.Substitutions) = False Then
                            objCmd.Parameters("@bIsSubstituitons").Value = Convert.ToBoolean(Convert.ToInt16(RxMessage.MedicationPrescribed.Substitutions))
                        Else
                            objCmd.Parameters("@bIsSubstituitons").Value = DBNull.Value
                        End If


                        If (RxMessage.MedicationPrescribed.Note Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Note) = False) Then
                            objCmd.Parameters("@sNotes").Value = RxMessage.MedicationPrescribed.Note
                        Else
                            objCmd.Parameters("@sNotes").Value = DBNull.Value
                        End If

                        If RxMessage.MedicationPrescribed.DrugCoded IsNot Nothing Then

                            If (RxMessage.MedicationPrescribed.DrugCoded.ProductCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.ProductCode) = False) Then
                                objCmd.Parameters("@sProductCode").Value = RxMessage.MedicationPrescribed.DrugCoded.ProductCode
                            Else
                                objCmd.Parameters("@sProductCode").Value = DBNull.Value
                            End If


                            If (RxMessage.MedicationPrescribed.DrugCoded.ProductCodeQualifier Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.ProductCodeQualifier) = False) Then
                                objCmd.Parameters("@sProductCodeQualifier").Value = RxMessage.MedicationPrescribed.DrugCoded.ProductCodeQualifier
                            Else
                                objCmd.Parameters("@sProductCodeQualifier").Value = DBNull.Value
                            End If


                            objCmd.Parameters("@sDosageForm").Value = DBNull.Value


                            If (RxMessage.MedicationPrescribed.DrugCoded.Strength Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.Strength) = False) Then
                                objCmd.Parameters("@sDrugStrength").Value = RxMessage.MedicationPrescribed.DrugCoded.Strength
                            Else
                                objCmd.Parameters("@sDrugStrength").Value = DBNull.Value
                            End If


                            If (RxMessage.MedicationPrescribed.DrugCoded.StrengthCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.StrengthCode) = False) Then
                                objCmd.Parameters("@sDrugStrengthUnits").Value = RxMessage.MedicationPrescribed.DrugCoded.StrengthCode
                            Else
                                objCmd.Parameters("@sDrugStrengthUnits").Value = DBNull.Value
                            End If
                        End If

                        If RxMessage.MedicationPrescribed.Quantity IsNot Nothing Then


                            If (RxMessage.MedicationPrescribed.Quantity.CodeListQualifier Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Quantity.CodeListQualifier) = False) Then
                                objCmd.Parameters("@sCodeListQualifier").Value = RxMessage.MedicationPrescribed.Quantity.CodeListQualifier
                            Else
                                objCmd.Parameters("@sCodeListQualifier").Value = DBNull.Value
                            End If


                            If (RxMessage.MedicationPrescribed.Quantity.UnitSourceCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Quantity.UnitSourceCode) = False) Then
                                objCmd.Parameters("@sUnitSourceCode").Value = RxMessage.MedicationPrescribed.Quantity.UnitSourceCode
                            Else
                                objCmd.Parameters("@sUnitSourceCode").Value = DBNull.Value
                            End If

                            If (RxMessage.MedicationPrescribed.Quantity.PotencyUnitCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.Quantity.PotencyUnitCode) = False) Then
                                objCmd.Parameters("@sPotencyUnitCode").Value = RxMessage.MedicationPrescribed.Quantity.PotencyUnitCode
                            Else
                                objCmd.Parameters("@sPotencyUnitCode").Value = DBNull.Value
                            End If
                        End If


                        If RxMessage.MedicationPrescribed.DrugCoded IsNot Nothing Then

                            If (RxMessage.MedicationPrescribed.DrugCoded.DrugDBCode Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.DrugDBCode) = False) Then
                                objCmd.Parameters("@sDrugDBCode").Value = RxMessage.MedicationPrescribed.DrugCoded.DrugDBCode
                            Else
                                objCmd.Parameters("@sDrugDBCode").Value = DBNull.Value
                            End If


                            If (RxMessage.MedicationPrescribed.DrugCoded.DrugDBCodeQualifier Is Nothing = False) AndAlso (IsDBNull(RxMessage.MedicationPrescribed.DrugCoded.DrugDBCodeQualifier) = False) Then
                                objCmd.Parameters("@sDrugDBCodeQualifier").Value = RxMessage.MedicationPrescribed.DrugCoded.DrugDBCodeQualifier
                            Else
                                objCmd.Parameters("@sDrugDBCodeQualifier").Value = DBNull.Value
                            End If
                        End If
                    End If

                    objCmd.Connection.Open()
                    objCmd.ExecuteNonQuery()
                    objCmd.Connection.Close()

                End Using
            End Using

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)
        End Try
    End Sub

    Private Sub UpdateMedicationStatus(ByVal DataRow As DataRow)
        Dim sPON As String = String.Empty
        Dim nPrescriptionID As Int64 = 0
        Dim sFileXML As String = String.Empty

        Dim xmlSerializer As Xml.Serialization.XmlSerializer = Nothing

        Dim SSMessage As Schema.MessageType = Nothing
        Dim SSBody As Schema.BodyType = Nothing
        Dim SSCancelResponse As Schema.CancelRxResponse = Nothing

        Try
            If DataRow.Table.Columns.Contains("FileXML") AndAlso DataRow.Table.Columns.Contains("sPrescriberOrder") Then

                sFileXML = Convert.ToString(DataRow("FileXML"))
                sPON = Convert.ToString(DataRow("sPrescriberOrder"))

                If Int64.TryParse(sPON, nPrescriptionID) AndAlso Not String.IsNullOrWhiteSpace(sFileXML) Then

                    Using reader As New StringReader(sFileXML)
                        SSMessage = New Schema.MessageType()
                        xmlSerializer = New Xml.Serialization.XmlSerializer(SSMessage.GetType())
                        SSMessage = xmlSerializer.Deserialize(reader)
                    End Using

                    If SSMessage IsNot Nothing AndAlso TypeOf (SSMessage) Is Schema.MessageType Then
                        If SSMessage.Body IsNot Nothing AndAlso SSMessage.Body.Item IsNot Nothing AndAlso TypeOf (SSMessage.Body.Item) Is Schema.CancelRxResponse Then

                            SSCancelResponse = DirectCast(SSMessage.Body.Item, Schema.CancelRxResponse)

                            If SSCancelResponse.Response IsNot Nothing AndAlso TypeOf (SSCancelResponse.Response.Item) Is Schema.DeniedType Then
                                Using p As New gloSureScriptDBLayer()
                                    p.UpdateMedicationStatus(0, nPrescriptionID, "Active")
                                End Using
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString, strConnection)

            SSMessage = Nothing
            SSBody = Nothing
            xmlSerializer = Nothing
            SSCancelResponse = Nothing
        End Try
    End Sub
End Class
Public Class ServiceClass
    Private Sub New()
    End Sub

    Friend Shared Function GetWCFSvc(ByVal siteUrl As String) As eRxWCFStaging.IeRxClient
        Dim client As eRxWCFStaging.IeRxClient = Nothing
        Dim serviceUri As Uri = Nothing
        Dim endpointAddress As ServiceModel.EndpointAddress = Nothing
        Dim binding As WSHttpBinding = Nothing
        Try
            serviceUri = New Uri(siteUrl)
            endpointAddress = New ServiceModel.EndpointAddress(serviceUri)
            'Create the binding here
            binding = BindingFactory.CreateInstance()
            client = New eRxWCFStaging.IeRxClient(binding, endpointAddress)
            Return client
        Catch ex As Exception
            Throw ex
            Return client
        Finally
            If Not IsNothing(binding) Then
                binding = Nothing
            End If
            If Not IsNothing(endpointAddress) Then
                endpointAddress = Nothing
            End If
            If Not IsNothing(serviceUri) Then
                serviceUri = Nothing
            End If
        End Try
    End Function

    Friend Shared Function GetSecureWCFSvc(ByVal siteUrl As String) As gloDirectStaging.IgloDirectClient
        Dim client As gloDirectStaging.IgloDirectClient = Nothing
        Dim serviceUri As Uri = Nothing
        Dim endpointAddress As ServiceModel.EndpointAddress = Nothing
        Dim binding As WSHttpBinding = Nothing
        Try
            serviceUri = New Uri(siteUrl)
            endpointAddress = New ServiceModel.EndpointAddress(serviceUri)
            'Create the binding here
            binding = BindingFactory.CreateInstance()
            client = New gloDirectStaging.IgloDirectClient(binding, endpointAddress)
            Return client
        Catch ex As Exception
            Throw ex
            Return client
        Finally
            If Not IsNothing(binding) Then
                binding = Nothing
            End If
            If endpointAddress IsNot Nothing Then
                endpointAddress = Nothing
            End If
            If serviceUri IsNot Nothing Then
                serviceUri = Nothing
            End If
        End Try
    End Function
End Class
Friend NotInheritable Class BindingFactory

    Private Sub New()
    End Sub

    Friend Shared Function CreateInstance() As WSHttpBinding
        Dim binding As New WSHttpBinding()
        Try
            binding.Security.Mode = SecurityMode.Transport
            binding.ReliableSession.Enabled = False
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None
            binding.UseDefaultWebProxy = True

            binding.ReliableSession.InactivityTimeout = New TimeSpan(0, 10, 0)


            binding.OpenTimeout = New TimeSpan(4, 10, 0)
            binding.CloseTimeout = New TimeSpan(4, 10, 0)
            binding.SendTimeout = New TimeSpan(4, 10, 10)
            binding.ReceiveTimeout = New TimeSpan(4, 10, 0)
            binding.MaxBufferPoolSize = 99999999999999
            binding.MaxReceivedMessageSize = 2147483647


            binding.ReaderQuotas.MaxArrayLength = 2147483647
            binding.ReaderQuotas.MaxDepth = 64
            binding.ReaderQuotas.MaxStringContentLength = 2147483647
            binding.ReaderQuotas.MaxBytesPerRead = 568556652
            binding.ReaderQuotas.MaxNameTableCharCount = 568556652

            Return binding
        Catch ex As Exception
            Throw ex
            Return binding
        Finally
            If Not IsNothing(binding) Then
                binding = Nothing
            End If
        End Try
    End Function

End Class
