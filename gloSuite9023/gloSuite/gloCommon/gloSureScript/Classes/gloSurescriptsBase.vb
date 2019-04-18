Imports System.IO
Imports System.ServiceModel
Imports System.Xml
Imports Schema = gloGlobal.Schemas.Surescript
Imports System.Xml.Serialization

Public Class gloSurescriptsBase

    Public _serviceURL As String

    Public Class ServiceResponse
        Public file As String
        Public bytes As Byte()

        Public Sub New(ByVal FileName As String, ByVal BytesData As Byte())
            file = FileName
            bytes = BytesData
        End Sub

    End Class

    Private ReadOnly Property DefaultServiceBinding
        Get
            Dim binding As New WSHttpBinding()
            Try
                binding.Security.Mode = SecurityMode.Transport
                binding.ReliableSession.Enabled = False
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None
                binding.UseDefaultWebProxy = True
                binding.OpenTimeout = New TimeSpan(0, 10, 0)
                binding.CloseTimeout = New TimeSpan(0, 10, 0)
                binding.SendTimeout = New TimeSpan(0, 10, 10)
                binding.ReceiveTimeout = New TimeSpan(0, 10, 0)
                binding.MaxBufferPoolSize = 99999999999999
                binding.MaxReceivedMessageSize = 2147483647
                binding.ReaderQuotas.MaxArrayLength = 2147483647
                binding.ReaderQuotas.MaxDepth = 64
                binding.ReaderQuotas.MaxStringContentLength = 2147483647
                binding.ReaderQuotas.MaxBytesPerRead = 568556652
                binding.ReaderQuotas.MaxNameTableCharCount = 568556652
                Return (binding)

            Catch ex As Exception
                Throw ex
                Return binding

            Finally
                If Not IsNothing(binding) Then
                    binding = Nothing
                End If

            End Try
        End Get
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal serviceURL As String)
        _serviceURL = serviceURL
    End Sub

    Private Function GetWCFSvc(ByVal siteUrl As String) As eRxWCFStaging.IeRxClient
        Dim client As eRxWCFStaging.IeRxClient = Nothing
        Dim serviceUri As Uri = Nothing
        Dim endpointAddress As EndpointAddress = Nothing
        Dim binding As WSHttpBinding = Nothing
        Try
            serviceUri = New Uri(siteUrl)
            endpointAddress = New EndpointAddress(serviceUri)
            binding = DefaultServiceBinding
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

    Public Function PostRxMessage(ByVal messageType As Schema.MessageType) As ServiceResponse

        Dim strfilename As String = gloSettings.FolderSettings.AppTempFolderPath & (gloGlobal.clsFileExtensions.GetUniqueDateString() & ".xml")

        Dim xmlSerializer As XmlSerializer = Nothing
        xmlSerializer = New XmlSerializer(messageType.GetType())

        Using sWriter As New StreamWriter(strfilename, False)
            xmlSerializer.Serialize(sWriter, messageType)
        End Using

        Dim oFile As FileStream = Nothing
        Dim oReader As BinaryReader = Nothing
        oFile = New FileStream(strfilename, FileMode.Open, FileAccess.Read)

        oReader = New BinaryReader(oFile)
        Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)

        Return PostMessage(bytesRead)

    End Function

    Public Function PostMessage(ByVal fileBytes As Byte()) As ServiceResponse

        Dim responseFile As String
        Dim _isInternetAvailable As Boolean
        Dim resultBytes As Byte() = Nothing

        Dim eRxService As eRxWCFStaging.IeRxClient = Nothing
        Dim response As ServiceResponse = Nothing

        Try
            _isInternetAvailable = gloSurescriptGeneral.IsInternetConnectionAvailable()

            If _isInternetAvailable Then
                eRxService = GetWCFSvc(_serviceURL)
                resultBytes = eRxService.PostClientMessage(fileBytes, "RxMessage")

                If Not IsNothing(eRxService) Then
                    eRxService.Close()
                    eRxService = Nothing
                End If
            Else

            End If

            responseFile = WriteXMLFile(resultBytes)

            response = New ServiceResponse(responseFile, resultBytes)
            Return response

        Catch ex As IOException
            _isInternetAvailable = False
            gloSurescriptGeneral.UpdateLog("Error while conversion  - " & ex.ToString)
            MsgBox("Error while conversion  - " & ex.ToString)
            Return Nothing

        Catch ex As Exception
            _isInternetAvailable = False
            gloSurescriptGeneral.UpdateLog("Error while conversion  - " & ex.ToString)

            If ex.Message.Contains("expected 'https'") Then
                Throw New GloSurescriptException(ex.Message)
            End If

            WriteXMLFile(fileBytes)

            Return Nothing

        Finally

        End Try

    End Function

    Public Function WriteXMLFile(ByVal bytesData As Object) As String
        Dim strfilename As String = gloSettings.FolderSettings.AppTempFolderPath & (gloGlobal.clsFileExtensions.GetUniqueDateString() & ".xml")

        Try
            If Not IsNothing(bytesData) Then
                Dim content() As Byte = CType(bytesData, Byte())
                Dim oFile As New System.IO.FileStream(strfilename, System.IO.FileMode.Create)

                oFile.Write(content, 0, content.Length)
                oFile.Close()

                If Not IsNothing(oFile) Then
                    oFile.Dispose()
                    oFile = Nothing
                End If

                Return strfilename
            Else
                If gloSurescriptGeneral._isInternetAvailable = False Then
                    gloSurescriptGeneral.UpdateLog("This eRx will not be sent now as no internet connection is available. It will be queued and sent when internet connection will again be detected. Do not send it again.")
                    Return ""
                Else
                    gloSurescriptGeneral.UpdateLog("Response Object not returned")
                    Return ""
                End If

            End If
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog("Error Extracting Response Object: " & ex.Message)
            Throw New GloSurescriptException(ex.Message)

        End Try

    End Function

    Public Function GetStatusMessage(ByVal statusMsg As StatusMessage, ByVal msgResponse As gloSureScriptInterface.SentMessageType, ByVal msgType As String, ByVal drug As String)
        Dim strmessage = New System.Text.StringBuilder()

        Select Case msgResponse
            Case gloSureScriptInterface.SentMessageType.eApproved
                strmessage.Append("" & vbCrLf & "Drug Name: " & drug & vbCrLf & "Transaction : " & msgType & " Response Approved")
            Case gloSureScriptInterface.SentMessageType.eApprovedWithChanges
                strmessage.Append("" & vbCrLf & "Drug Name: " & drug & vbCrLf & "Transaction :  " & msgType & " Response Approved With Changes")
            Case gloSureScriptInterface.SentMessageType.eDenied
                strmessage.Append("" & vbCrLf & "Drug Name: " & drug & vbCrLf & "Transaction :  " & msgType & " Response Denied")
            Case gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow
                strmessage.Append("" & vbCrLf & "Drug Name: " & drug & vbCrLf & "Transaction :  " & msgType & " Response Denied With NewRx To Follow")
            Case gloSureScriptInterface.SentMessageType.eNewRx
                strmessage.Append("" & vbCrLf & "Drug Name: " & drug & vbCrLf & "Transaction :  New Rx")
        End Select

        If statusMsg.MessageName = "Status" Then
            strmessage.Append(vbCrLf)
            strmessage.Append("Status: Posted Successfully")
            If Not IsNothing(statusMsg.StatusCode) Then
                strmessage.Append(vbCrLf)
                strmessage.Append("Status code: " & statusMsg.StatusCode)
                strmessage.Append(vbCrLf)
                If statusMsg.StatusCode = "000" Then
                    strmessage.Append("Description :Transmission successful ")
                ElseIf statusMsg.StatusCode = "010" Then
                    strmessage.Append("Description :Successfully accepted by ultimate receiver ")
                End If
            End If
            If Not IsNothing(statusMsg.Description) Then
                strmessage.Append(vbCrLf)
                strmessage.Append(statusMsg.Description)
            End If
        ElseIf statusMsg.MessageName = "Verify" Then
            strmessage.Append(vbCrLf)
            strmessage.Append("Status: Posted Successfully")
            If Not IsNothing(statusMsg.StatusCode) Then
                strmessage.Append(vbCrLf)
                strmessage.Append("Status code: " & statusMsg.StatusCode)
            End If
            If Not IsNothing(statusMsg.Description) Then
                strmessage.Append(vbCrLf)
                strmessage.Append(statusMsg.Description)
            End If
        ElseIf statusMsg.MessageName = "Error" Then
            strmessage.Append(vbCrLf)
            strmessage.Append("Status: Could not be Posted Successfully")
            If Not IsNothing(statusMsg.StatusCode) Then
                strmessage.Append(vbCrLf)
                strmessage.Append("Error code: " & statusMsg.StatusCode)
            End If
            If Not IsNothing(statusMsg.Description) Then
                strmessage.Append(vbCrLf)
                strmessage.Append("Error Description: " & statusMsg.Description)
            End If
        End If


        If Not String.IsNullOrWhiteSpace(statusMsg.MessageID) Then
            strmessage.Append(vbCrLf)
            strmessage.Append("MessageID: " & statusMsg.MessageID)
        End If        

        Return strmessage.ToString

    End Function

    Public StatusMessageType As String = ""
    Public MessageName As String = ""
    Public MessagestatusCode As String = ""

    Public Function ReadStatusMessage(ByVal strfilepath As String, ByVal eMessageType As gloSureScriptInterface.SentMessageType, Optional ByVal strdrugname As String = "") As StatusMessage
        Dim areader1 As XmlReader = Nothing
        Dim areader As XmlReader = Nothing
        gloSurescriptGeneral.UpdateLog("Entering gloSureScriptsInterface.ReadStatusMessage")

        Dim strmessage As System.Text.StringBuilder = Nothing
        Dim sFileContent As String = ""
        Dim statusMsg As New StatusMessage

        Try
            areader = XmlReader.Create(strfilepath)

            Dim sTemp As String = Nothing
            sFileContent = File.ReadAllText(strfilepath)
            If sFileContent.ToLower().Contains("listener not started") Then
                System.Windows.Forms.MessageBox.Show(sFileContent, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                Return Nothing
            End If

            Dim xReader As XmlReader = XmlReader.Create(strfilepath)

            While xReader.Read()
                Select Case xReader.NodeType
                    Case XmlNodeType.Element
                        sTemp = xReader.Name
                        Exit Select
                    Case XmlNodeType.Text
                        Select Case sTemp 'areader.Name
                            Case "From"
                                statusMsg.MessageFrom = xReader.Value 'areader.ReadInnerXml
                            Case "To"
                                statusMsg.MessageTo = xReader.Value 'areader.ReadInnerXml
                            Case "MessageID"
                                statusMsg.MessageID = xReader.Value ' areader.ReadInnerXml

                            Case "RelatesToMessageID"
                                statusMsg.RelatesToMessageId = xReader.Value 'areader.ReadInnerXml
                            Case "SentTime"
                                statusMsg.DateTimeStamp = xReader.Value 'areader.ReadInnerXml
                                statusMsg.DateReceived = Date.Now
                            Case "Status"
                                statusMsg.MessageName = "Status"
                                StatusMessageType = "Status"
                            Case "Verify"
                                If statusMsg.MessageID = "0" Then
                                    statusMsg.MessageName = "Status"
                                Else
                                    statusMsg.MessageName = "Verify"
                                End If
                            Case "Error"
                                statusMsg.MessageName = "Error"
                                'If areader.NodeType = XmlNodeType.Element Then
                                '    areader1 = areader.ReadSubtree

                                '    While areader1.Read
                                '        If areader1.NodeType = XmlNodeType.Element Then
                                '            Select Case areader1.Name
                                '                Case "Code"
                                '                    statusMsg.StatusCode = areader1.ReadString()
                                '                    If statusMsg.StatusCode = "000" Or statusMsg.StatusCode = "010" Then
                                '                        MessageName = "Status"
                                '                    Else
                                '                        MessageName = "Error"
                                '                    End If
                                '                Case "Description"
                                '                    statusMsg.Description = areader1.ReadString()
                                '            End Select
                                '        End If
                                '    End While
                                '    If Not IsNothing(areader1) Then
                                '        areader1.Close()
                                '        areader1 = Nothing
                                '    End If
                                'End If
                            Case "Code"
                                statusMsg.StatusCode = xReader.Value 'areader.ReadInnerXml
                                If statusMsg.StatusCode = "000" Or statusMsg.StatusCode = "010" Then
                                    MessageName = "Status"
                                Else
                                    MessageName = "Error"
                                End If
                                statusMsg.MessageName = MessageName

                                MessagestatusCode = statusMsg.StatusCode
                            Case "Description"
                                statusMsg.Description = xReader.Value ' areader.ReadInnerXml
                        End Select

                        sTemp = ""
                        Exit Select
                    Case XmlNodeType.EndElement
                        sTemp = ""
                        Exit Select
                End Select
            End While

            'While areader.Read

            '    Dim blnIsRefillsQualifier As Boolean = False

            '    If areader.NodeType = XmlNodeType.Element Then
            '        Select Case areader.Name
            '            'this is the message id for the Refillrequest that shall go in the MessageId field
            '            Case "From"
            '                objErrorMessage.MessageFrom = areader.ReadInnerXml
            '            Case "To"
            '                objErrorMessage.MessageTo = areader.ReadInnerXml
            '            Case "MessageID"
            '                objErrorMessage.MessageID = areader.ReadInnerXml

            '            Case "RelatesToMessageID"
            '                objErrorMessage.RelatesToMessageId = areader.ReadInnerXml
            '            Case "SentTime"
            '                objErrorMessage.DateTimeStamp = areader.ReadInnerXml
            '                objErrorMessage.DateReceived = Date.Now
            '            Case "Status"
            '                objErrorMessage.MessageName = "Status"
            '            Case "Verify"
            '                If objErrorMessage.MessageID = "0" Then
            '                    objErrorMessage.MessageName = "Status"
            '                Else
            '                    objErrorMessage.MessageName = "Verify"
            '                End If
            '            Case "Error"
            '                objErrorMessage.MessageName = "Error"
            '                If areader.NodeType = XmlNodeType.Element Then
            '                    areader1 = areader.ReadSubtree

            '                    While areader1.Read
            '                        If areader1.NodeType = XmlNodeType.Element Then
            '                            Select Case areader1.Name
            '                                Case "Code"
            '                                    objErrorMessage.StatusCode = areader1.ReadString()
            '                                    If objErrorMessage.StatusCode = "000" Or objErrorMessage.StatusCode = "010" Then
            '                                        'MessageName = "Status"
            '                                    Else
            '                                        'MessageName = "Error"
            '                                    End If
            '                                Case "Description"
            '                                    objErrorMessage.Description = areader1.ReadString()
            '                            End Select
            '                        End If
            '                    End While
            '                    If Not IsNothing(areader1) Then
            '                        areader1.Close()
            '                        areader1 = Nothing
            '                    End If
            '                End If
            '            Case "Code"
            '                objErrorMessage.StatusCode = areader.ReadInnerXml
            '                If objErrorMessage.StatusCode = "000" Or objErrorMessage.StatusCode = "010" Then
            '                    'MessageName = "Status"
            '                Else
            '                    'MessageName = "Error"
            '                End If
            '                'MessagestatusCode = objErrorMessage.StatusCode
            '            Case "Description"
            '                objErrorMessage.Description = areader.ReadInnerXml
            '        End Select

            '    End If
            'End While

            If Not IsNothing(areader) Then
                areader.Close()
                areader = Nothing
            End If

            Return statusMsg

            gloSurescriptGeneral.UpdateLog("Exiting gloSureScriptsInterface.ReadStatusMessage")
        Catch ex As gloSurescriptDBException
            Windows.Forms.MessageBox.Show(ex.ToString(), "gloEMR")
            Throw (ex)
        Catch ex As GloSurescriptException
            Windows.Forms.MessageBox.Show(ex.ToString(), "gloEMR")
            Throw ex
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.ToString(), "gloEMR")
            Throw New GloSurescriptException(ex.Message)
        Finally
            If Not IsNothing(strmessage) Then
                strmessage = Nothing
            End If
            If Not IsNothing(areader) Then
                areader.Close()
                areader = Nothing
            End If

        End Try
    End Function


End Class

