Imports System.Xml
Imports System.IO
Imports System.Windows.Forms
Imports System.ServiceModel
Imports System.Text
Imports System.Xml.XPath
Imports System.Xml.Xsl
Imports System.Configuration
Imports System.Xml.Serialization

Imports schema = gloGlobal.Schemas.Surescript

Public Enum RefillStatus
    eApproved
    eApprovedWithChanges
    eDenied
    eDeniedWithNewRxToFollow
End Enum

Public Enum PrescriberType
    eAddPrescriber
    eAddPrescriberToLocation
End Enum
Public Class gloSureScriptInterface

    Implements IDisposable
    Public Enum SentMessageType
        eApproved
        eApprovedWithChanges
        eDenied
        eDeniedWithNewRxToFollow
        eNewRx
        Cancel
    End Enum
    Public XMLFilePath As String = ""
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

    'Dim helper As PrescriptionBusinessLayer = Nothing
    Dim WithEvents ss_helper As gloSureScript.gloSurescriptsHelper = Nothing
    Public StatusMessageType As String = ""
    Public StatusMessageType2 As String = ""
    Public MessageName As String = ""
    Public MessagestatusCode As String = ""
    Public Event MessageInvalidated()
    Public ValidationMessage As String = ""
    Public ValidationMessageforDrug As String = ""
    Public ValidationMessageBuilderforDrug As System.Text.StringBuilder
    Public ValidationMessageBuilder As System.Text.StringBuilder
    Public ValidationMessageBuilderfor10dot6 As System.Text.StringBuilder
    ' Public ResEPCSDNTFRelatesToMessageId As String = ""
    Public ClinicName As String = ""


#Region "EPCS Request Header Property"
    Public Property OrganizationName() As String
    Public Property OrganizationLabel() As String
    Public Property VendorName() As String
    Public Property VendorLabel() As String
    Public Property VendorNodeLabel() As String
    Public Property VendorNodeName() As String
    Public Property AppName() As String
    Public Property ApplicationVersion() As String
    Public Property SourceOrganizationId() As String
    Public Property HeaderDate() As Date
    Public Property RouterName() As String
#End Region

    '''''''this will store the NewRxEDIFACT file data and will be updated in the surescriptmessagetransaction table 
    '''''against the messageid and Rxreferencenumber for those files that are posted sucessfully. Done for MU certification.
    Public NewRxEDIFACTFileData As System.Text.StringBuilder

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public Event ErrorGenerated()
#Region "Private Functions"

    Public Function ExtractXML(ByVal bytes As Object) As String
        Try
            If Not IsNothing(bytes) Then
                Dim sFileContent As String = ""
                Dim strfilename As String = gloSettings.FolderSettings.AppTempFolderPath & (gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & ".xml")
                Dim content() As Byte = CType(bytes, Byte())
                Dim oFile As New System.IO.FileStream(strfilename, System.IO.FileMode.Create)

                oFile.Write(content, 0, content.Length)
                oFile.Close()

                If Not IsNothing(oFile) Then
                    oFile.Dispose()
                    oFile = Nothing
                End If

                sFileContent = File.ReadAllText(strfilename)
                If sFileContent.ToLower().Contains("listener not started") Then
                    Return strfilename
                End If

                Dim odoc As XmlDocument = New XmlDocument
                odoc.Load(strfilename)

                If Not IsNothing(odoc) Then
                    odoc = Nothing
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

    Public Function GenerateFileName(ByVal eMessageType As MessageType) As String
        Dim strfilename As String = ""

        Select Case eMessageType

            Case MessageType.eError
                strfilename = "Error"
            Case MessageType.eNewRx
                strfilename = "NewRx"
            Case MessageType.eRefillRequest
                strfilename = "RefillRequest"
            Case MessageType.eRefillResponse
                strfilename = "RefillResponse"
            Case MessageType.eStatus
                strfilename = "Status"
            Case MessageType.eVerify
                strfilename = "Verify"
            Case MessageType.eCancelRx
                strfilename = "CancelRx"
            Case MessageType.eNewRxEPCS
                strfilename = "NewRxEPCS"
            Case MessageType.eRxChangeRequest
                strfilename = "RxChangeRequest"
            Case MessageType.eRxChangeResponse
                strfilename = "RxChangeResponse"
        End Select
        ' Dim dtdate As DateTime = Date.UtcNow
        Dim strtemp As String = gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") 'strfilename & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

        strfilename = gloSettings.FolderSettings.AppTempFolderPath & strtemp & ".xml"
        Return strfilename
    End Function

#End Region
    ''' <summary>
    ''' Called from View RefillRequest form to load pending refills
    ''' </summary>
    ''' <param name="PrescriberId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPendingRefills(ByVal PrescriberId As String) As DataTable
        Dim objSureScriptDBLayer As New gloSureScriptDBLayer
        Dim dt As DataTable = Nothing
        Try
            dt = objSureScriptDBLayer.GetPendingRefills(PrescriberId)
            Return dt
        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return dt
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return dt
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
            Return dt
        Finally
            If Not IsNothing(objSureScriptDBLayer) Then
                objSureScriptDBLayer.Dispose()
                objSureScriptDBLayer = Nothing
            End If
        End Try
    End Function

    Public Function ReadStatusMessageRevised(ByVal bytes As Byte(), ByVal messageResponse As SentMessageType, ByVal drugName As String, Optional ByVal responseType As String = Nothing) As Boolean
        Dim areader1 As XmlReader = Nothing
        Dim areader As XmlReader = Nothing
        gloSurescriptGeneral.UpdateLog("Entering gloSureScriptsInterface.ReadStatusMessage")

        MessageName = ""
        StatusMessageType = ""
        Dim strmessage As System.Text.StringBuilder = Nothing

        Dim statusMsg As New StatusMessage
        Dim sFileText As String = ""
        Try
            Dim statusFile As String = ExtractXML(bytes)
            areader = XmlReader.Create(statusFile)

            sFileText = File.ReadAllText(statusFile)

            If sFileText.ToLower().Contains("listener not started") Then
                MessageBox.Show(sFileText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            Dim sTemp As String = Nothing
            Dim xReader As XmlReader = XmlReader.Create(statusFile)

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
            '            Case "From"
            '                statusMsg.MessageFrom = areader.ReadInnerXml
            '            Case "To"
            '                statusMsg.MessageTo = areader.ReadInnerXml
            '            Case "MessageID"
            '                statusMsg.MessageID = areader.ReadInnerXml

            '            Case "RelatesToMessageID"
            '                statusMsg.RelatesToMessageId = areader.ReadInnerXml
            '            Case "SentTime"
            '                statusMsg.DateTimeStamp = areader.ReadInnerXml
            '                statusMsg.DateReceived = Date.Now
            '            Case "Status"
            '                statusMsg.MessageName = "Status"
            '                StatusMessageType = "Status"
            '            Case "Verify"
            '                If statusMsg.MessageID = "0" Then
            '                    statusMsg.MessageName = "Status"
            '                Else
            '                    statusMsg.MessageName = "Verify"
            '                End If
            '            Case "Error"
            '                statusMsg.MessageName = "Error"
            '                If areader.NodeType = XmlNodeType.Element Then
            '                    areader1 = areader.ReadSubtree

            '                    While areader1.Read
            '                        If areader1.NodeType = XmlNodeType.Element Then
            '                            Select Case areader1.Name
            '                                Case "Code"
            '                                    statusMsg.StatusCode = areader1.ReadString()
            '                                    If statusMsg.StatusCode = "000" Or statusMsg.StatusCode = "010" Then
            '                                        MessageName = "Status"
            '                                    Else
            '                                        MessageName = "Error"
            '                                    End If
            '                                Case "Description"
            '                                    statusMsg.Description = areader1.ReadString()
            '                            End Select
            '                        End If
            '                    End While
            '                    If Not IsNothing(areader1) Then
            '                        areader1.Close()
            '                        areader1 = Nothing
            '                    End If
            '                End If
            '            Case "Code"
            '                statusMsg.StatusCode = areader.ReadInnerXml
            '                If statusMsg.StatusCode = "000" Or statusMsg.StatusCode = "010" Then
            '                    MessageName = "Status"
            '                Else
            '                    MessageName = "Error"
            '                End If
            '                MessagestatusCode = statusMsg.StatusCode
            '            Case "Description"
            '                statusMsg.Description = areader.ReadInnerXml
            '        End Select

            '    End If
            'End While

            'If Not IsNothing(areader) Then
            '    areader.Close()
            '    areader = Nothing
            'End If

            If Not IsNothing(xReader) Then
                xReader.Close()
                xReader = Nothing
            End If

            strmessage = New System.Text.StringBuilder

            Select Case messageResponse
                Case SentMessageType.eApproved
                    strmessage.Append("" & vbCrLf & "Drug Name: " & drugName & vbCrLf & "Transaction :  " & responseType & " Response Approved")
                Case SentMessageType.eApprovedWithChanges
                    strmessage.Append("" & vbCrLf & "Drug Name: " & drugName & vbCrLf & "Transaction :  " & responseType & " Response Approved With Changes")
                Case SentMessageType.eDenied
                    strmessage.Append("" & vbCrLf & "Drug Name: " & drugName & vbCrLf & "Transaction :  " & responseType & " Response Denied")
                Case SentMessageType.eDeniedWithNewRxToFollow
                    strmessage.Append("" & vbCrLf & "Drug Name: " & drugName & vbCrLf & "Transaction :  " & responseType & " Response Denied With NewRx To Follow")
                Case SentMessageType.eNewRx
                    strmessage.Append("" & vbCrLf & "Drug Name: " & drugName & vbCrLf & "Transaction :  New Rx")
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

            strmessage.Append(vbCrLf)
            strmessage.Append("MessageID: " & statusMsg.MessageID)

            StatusMessageType = strmessage.ToString
            Dim objSureScriptDBLayer As New gloSureScriptDBLayer

            ' Problem #00000245 : Error Prompted on Pending Message Pop Box But After click on it no longer in screen.
            ' as per discussion in status meeting decided that no need to insert Status (Error) Messages in Database.
            If statusMsg.MessageName <> "Error" Then
                'Insert acknowledgement details in acknowledgement transaction
                If objSureScriptDBLayer.InsertAcknowledgements(statusMsg, True) Then
                    'Insert data in message transaction
                    If objSureScriptDBLayer.InsertintoMessageTransaction(CType(statusMsg, SureScriptMessage)) Then

                    End If
                End If
            End If
            If Not objSureScriptDBLayer Is Nothing Then
                objSureScriptDBLayer.Dispose()
                objSureScriptDBLayer = Nothing
            End If

            gloSurescriptGeneral.UpdateLog("Exiting gloSureScriptsInterface.ReadStatusMessage")
            Return True
        Catch ex As gloSurescriptDBException
            Throw ex
        Catch ex As GloSurescriptException
            Throw ex
        Catch ex As Exception
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

    ''' <summary>
    ''' Function is called to view the drugs that need to be refilled
    ''' </summary>
    ''' <param name="RxReferenceNumber"></param>
    ''' <param name="RxTransactionID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDrugsToRefill(ByVal MessageID As String, Optional ByVal RxReferenceNumber As String = Nothing, Optional ByVal RxTransactionID As String = Nothing) As EPrescription

        Dim objPrescription As EPrescription = Nothing
        Dim objSureScriptDBLayer As New gloSureScriptDBLayer
        Try
            objPrescription = New EPrescription

            If Not String.IsNullOrWhiteSpace(RxReferenceNumber) Then
                objPrescription.RxReferenceNumber = RxReferenceNumber
            End If
            If Not String.IsNullOrWhiteSpace(RxTransactionID) Then
                objPrescription.RxTransactionID = RxTransactionID
            End If

            objPrescription = objSureScriptDBLayer.GetRefillPrescription(MessageID)
            Return objPrescription

        Catch ex As gloSurescriptDBException
            Return Nothing
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As GloSurescriptException
            Return Nothing
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As Exception
            Return Nothing
            Throw New GloSurescriptException(ex.Message)
        Finally
            If Not IsNothing(objSureScriptDBLayer) Then
                objSureScriptDBLayer.Dispose()
                objSureScriptDBLayer = Nothing
            End If

        End Try
    End Function
    ''' <summary>
    ''' After Refill Response message is generated this function
    ''' Will update status flag in PrescriptionRefillTransactiondetail table
    ''' </summary>
    ''' <param name="objPrescription"></param>
    ''' <param name="estatus"></param>
    ''' <remarks></remarks>
    Public Sub UpdateRefillStatus(ByVal objPrescription As EPrescription, ByVal estatus As RefillStatus, ByVal item As Integer)
        Dim objSureScriptDBLayer As New gloSureScriptDBLayer

        Try
            objSureScriptDBLayer.UpdateRefillStatus(objPrescription, estatus, item)
        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
        Finally
            If Not IsNothing(objSureScriptDBLayer) Then
                objSureScriptDBLayer.Dispose()
                objSureScriptDBLayer = Nothing
            End If
        End Try
    End Sub
    ''' <summary>
    ''' Generate XML for Refill Response Message
    ''' </summary>

    Public Sub LoadtoDataset(ByRef RequestDataset As DataSet, ByVal filedata As Byte(), Optional sFileData As String = "")
        Dim xmlSR As System.IO.StringReader = Nothing
        Dim byte16Array As Byte() = Nothing
        Dim byte8Array As Byte() = Nothing
        Dim byteArray64 As String = Nothing
        Try
            Dim xmlData As String = ""
            If Not IsNothing(filedata) Then
                byte16Array = filedata
                RequestDataset.ReadXmlSchema(System.Windows.Forms.Application.StartupPath & "\SS_SCRIPT_XML_10_6MU.xsd")
                For iCount As Integer = 0 To RequestDataset.Tables.Count - 1
                    If RequestDataset.Tables(iCount).Columns.Contains("Date") = True Then
                        Dim newColumn As DataColumn = New DataColumn("ShortDate", System.Type.GetType("System.String"))
                        RequestDataset.Tables(iCount).Columns.Add(newColumn)
                        RequestDataset.Tables(iCount).Columns.Remove("Date")
                        RequestDataset.Tables(iCount).Columns("ShortDate").ColumnName = "Date"
                    End If
                    If RequestDataset.Tables(iCount).Columns.Contains("SentTime") = True Then
                        Dim newColumn As DataColumn = New DataColumn("ShortDate", System.Type.GetType("System.String"))
                        RequestDataset.Tables(iCount).Columns.Add(newColumn)
                        RequestDataset.Tables(iCount).Columns.Remove("SentTime")
                        RequestDataset.Tables(iCount).Columns("ShortDate").ColumnName = "SentTime"
                    End If
                    If RequestDataset.Tables(iCount).Columns.Contains("DateTime") = True Then
                        Dim newColumn As DataColumn = New DataColumn("ShortDate", System.Type.GetType("System.String"))
                        RequestDataset.Tables(iCount).Columns.Add(newColumn)
                        RequestDataset.Tables(iCount).Columns.Remove("DateTime")
                        RequestDataset.Tables(iCount).Columns("ShortDate").ColumnName = "DateTime"
                    End If
                Next

                'Dim dtPotency As New DataTable
                'Dim newPotencyColumn = New DataColumn("Potency", System.Type.GetType("System.String"))
                'dtPotency.Columns.Add(newPotencyColumn)
                'Dim newMDPotencyColumn = New DataColumn("MDPotency", System.Type.GetType("System.String"))
                'dtPotency.Columns.Add(newMDPotencyColumn)
                'RequestDataset.Tables.Add(dtPotency)


                Dim oendcode As System.Text.Encoding = EncodingDetector.DetectEncoding(byte16Array)

                If IsNothing(oendcode) Then
                    oendcode = Encoding.Unicode
                End If

                byte8Array = Encoding.Convert(oendcode, Encoding.UTF8, byte16Array)
                byteArray64 = Encoding.Default.GetString(byte8Array)
                Try
                    byte16Array = Convert.FromBase64String(byteArray64)
                Catch ex As Exception
                    Try
                        byte16Array = Convert.FromBase64String(Encoding.Default.GetString(byte16Array))
                    Catch
                        byte16Array = filedata
                    End Try
                End Try

                xmlData = System.Text.Encoding.Default.GetString(byte16Array)
            Else
                xmlData = sFileData
            End If

            Dim myIndex As Integer = xmlData.IndexOf("<")
            Dim myString = If((myIndex > 0) And (myIndex < 4), xmlData.Substring(myIndex), xmlData)

            Dim firsttranform As String = ""
            Dim secondtranform As String = ""
            Dim thirdtranform As String = ""
            firsttranform = Transform(myString, System.Windows.Forms.Application.StartupPath & "\namespaceremoval.xsl")
            secondtranform = Transform(firsttranform, System.Windows.Forms.Application.StartupPath & "\RequestMessageXSLTFile.xsl")
            thirdtranform = Transform(secondtranform, System.Windows.Forms.Application.StartupPath & "\namespaceadd.xsl")
            xmlSR = New System.IO.StringReader(thirdtranform)
            Try
                RequestDataset.ReadXml(xmlSR)
            Catch ex As Exception

            End Try
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(xmlSR) Then
                xmlSR.Close()
                xmlSR.Dispose()
                xmlSR = Nothing
            End If
        End Try
    End Sub

    Public Function GenerateRefillResponse10dot6New(ByVal objPrescription As EPrescription, ByVal eRefillStatus As RefillStatus, Optional ByVal denialreasoncode As String = "", Optional ByVal strnotes As String = "", Optional ByVal RefReqPatientID As String = "", Optional ByVal PONNumber As Long = 0)
        gloSurescriptGeneral.UpdateLog("Entering gloSureScriptsInterface.GenerateRefillResponse")
        Dim dsRequestData As DataSet = Nothing
        Dim _strfileName2 As String = ""
        Dim oResponse As SureScriptResponseMessage = Nothing
        Try
            dsRequestData = New DataSet
            Dim strfilepath As String = GenerateFileName(MessageType.eRefillResponse)

            XMLFilePath = strfilepath
            'Dim xmlwriter As XmlTextWriter = Nothing
            Dim eMessageType As SentMessageType

            ''Modiy to From and Header Feilds

            If File.Exists(strfilepath) Then
                File.Delete(strfilepath)
            End If

            Dim dtdate As DateTime = Date.UtcNow
            Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
            Dim strtime As String = Format(dtdate, "hh:mm:ss")
            Dim strmillisec As String = Format(dtdate, "mmm")
            Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

            'Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
            'strUTCFormat = strdate & "T" & strtime & ".0Z"
            Dim strUTCFormat As String = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.FZ")
            Dim oDrug As EDrug = Nothing

            For index As Int32 = 0 To objPrescription.DrugsCol.Count - 1
                If objPrescription.DrugsCol.Item(index).MessageType = "RefillRequest" Then
                    oDrug = objPrescription.DrugsCol.Item(index)
                End If                
            Next

            If oDrug Is Nothing Then
                oDrug = objPrescription.DrugsCol.Item(0)
            End If

            ''Load the request file in dataset
            Dim filedata As Byte() = Nothing

            filedata = objPrescription.FileData
            If IsNothing(filedata) Then
                filedata = oDrug.FileData
            End If

            LoadtoDataset(dsRequestData, filedata)

            If File.Exists(strfilepath) Then
                File.Delete(strfilepath)
            End If


            oDrug.MessageID = "RefillResponse" & strtemp
            oDrug.MessageName = "RefillResponse"
            oDrug.DateTimeStamp = strUTCFormat
            oDrug.DateReceived = Date.Now
            oDrug.MessageFrom = objPrescription.RxPrescriber.PrescriberID
            If oDrug.PhNCPDPID = "" Then
                oDrug.MessageTo = objPrescription.RxPharmacy.PharmacyID
            Else
                oDrug.MessageTo = oDrug.PhNCPDPID
            End If

            oResponse = New SureScriptResponseMessage


            dsRequestData.Tables("Message").Rows(0)("version") = "010"

            dsRequestData.Tables("Message").Rows(0)("release") = "006"


            dsRequestData.Tables("To").Rows(0)("Qualifier") = "P"

            dsRequestData.Tables("To").Rows(0)("To_Text") = oDrug.MessageTo

            dsRequestData.Tables("From").Rows(0)("Qualifier") = "D"

            dsRequestData.Tables("From").Rows(0)("From_Text") = oDrug.MessageFrom

            dsRequestData.Tables("Header").Rows(0)("MessageID") = oDrug.MessageID

            dsRequestData.Tables("Header").Rows(0)("SentTime") = strUTCFormat

            Dim strVersion As String = My.Application.Info.Version.ToString.Substring(0, 3)
            Dim strProductName As String = My.Application.Info.ProductName.ToString()
            Dim strCompany As String = My.Application.Info.CompanyName

            'dsRequestData.Tables("SenderSoftware").Rows(0)("SenderSoftwareDeveloper") = strCompany

            'dsRequestData.Tables("SenderSoftware").Rows(0)("SenderSoftwareProduct") = strProductName

            'dsRequestData.Tables("SenderSoftware").Rows(0)("SenderSoftwareVersionRelease") = strVersion


            dsRequestData.Tables("Header").Rows(0)("RelatesToMessageID") = oDrug.RelatesToMessageId



            If oDrug.RxReferenceNumber <> "" Then
                dsRequestData.Tables("Header").Rows(0)("RxReferenceNumber") = oDrug.RxReferenceNumber
            End If

            objPrescription.RxTransactionID = oDrug.PrescriptionID
            If Not IsNothing(oDrug.TransactionID) Then

                If objPrescription.RxTransactionID.Trim.Length > 0 Then
                    If Not IsNumeric(objPrescription.RxTransactionID) Then
                        dsRequestData.Tables("Header").Rows(0)("PrescriberOrderNumber") = objPrescription.RxTransactionID
                    Else
                        If Val(objPrescription.RxTransactionID) <> 0 Then
                            dsRequestData.Tables("Header").Rows(0)("PrescriberOrderNumber") = objPrescription.RxTransactionID
                        End If
                    End If

                    'Else
                    '    Dim nPrescOrdNo As Long = gloSurescriptGeneral.GetUniqueID()
                    '    dsRequestData.Tables("Header").Rows(0)("PrescriberOrderNumber") = nPrescOrdNo
                End If
            End If

            oResponse.MessageID = oDrug.MessageID
            ''end

            ''Depending upon response add the required table

            'Dim dsResponse As DataSet
            Try

                If dsRequestData.Tables.Count > 0 Then

                    'dsResponse = New DataSet
                    'dsResponse.ReadXmlSchema("D:\Schema\SS_SCRIPT_XML_10_6Mod.xsd")

                    Dim MyRow As DataRow = dsRequestData.Tables("RefillResponse").NewRow()
                    MyRow("RefillResponse_Id") = dsRequestData.Tables("RefillRequest").Rows(0)("RefillRequest_Id")
                    MyRow("Body_Id") = dsRequestData.Tables("RefillRequest").Rows(0)("Body_Id")
                    dsRequestData.Tables("RefillResponse").Rows.Add(MyRow)

                    MyRow = dsRequestData.Tables("Response").NewRow()
                    MyRow("Response_Id") = 0
                    MyRow("RefillResponse_Id") = dsRequestData.Tables("RefillResponse").Rows(0)("RefillResponse_Id")
                    dsRequestData.Tables("Response").Rows.Add(MyRow)


                    For iCount As Integer = 0 To dsRequestData.Tables.Count - 1

                        If (dsRequestData.Tables(iCount).TableName <> "RefillRequest") Then
                            If dsRequestData.Tables(iCount).Columns.Contains("RefillRequest_Id") = True Then

                                If dsRequestData.Tables(iCount).Columns.Contains("RefillResponse_Id") = True Then
                                    For Each row As DataRow In dsRequestData.Tables(iCount).Rows
                                        row("RefillResponse_Id") = row("RefillRequest_Id")
                                        row("RefillRequest_Id") = DBNull.Value
                                    Next
                                Else
                                    ' dsRequestData.Tables(iCount).Columns("RefillRequest_Id").ColumnName = "RefillResponse_Id"

                                    'For jCount As Integer = 0 To dsRequestData.Tables(iCount).Columns.Count - 1
                                    '    If dsRequestData.Tables(iCount).Columns(jCount).ColumnName = "RefillRequest_Id" Then

                                    For Each row As DataRow In dsRequestData.Tables(iCount).Rows
                                        'row("RefillResponse_Id") = row("RefillRequest_Id")
                                        row("RefillRequest_Id") = DBNull.Value
                                    Next
                                End If
                            End If
                        End If

                        'If dsRequestData.Tables(iCount).Columns.Contains("DateTime") = True Then
                        '    If dsRequestData.Tables(iCount).Columns.Contains("Date") = True Then
                        '        For Each row As DataRow In dsRequestData.Tables(iCount).Rows
                        '            If Not IsDBNull(row("DateTime")) And IsDBNull(row("Date")) Then
                        '                row("Date") = row("DateTime")
                        '                row("DateTime") = DBNull.Value
                        '            End If

                        '        Next
                        '    End If
                        'End If
                        'If dsRequestData.Tables(iCount).Columns.Contains("Date") = True Then

                        '    Dim newColumn As DataColumn = New DataColumn("ShortDate", System.Type.GetType("System.String"))
                        '    dsRequestData.Tables(iCount).Columns.Add(newColumn)

                        '    For Each row As DataRow In dsRequestData.Tables(iCount).Rows
                        '        If Not IsDBNull(row("Date")) Then
                        '            row("ShortDate") = CDate(row("Date")).ToString("yyyy-MM-dd")
                        '        Else
                        '            row("ShortDate") = DBNull.Value
                        '        End If
                        '    Next
                        '    dsRequestData.Tables(iCount).Columns.Remove("Date")
                        '    dsRequestData.Tables(iCount).Columns("ShortDate").ColumnName = "Date"
                        'End If
                        'If dsRequestData.Tables(iCount).Columns.Contains("DateTime") = True Then
                        '    Dim newColumn As DataColumn = New DataColumn("ShortDate", System.Type.GetType("System.String"))
                        '    dsRequestData.Tables(iCount).Columns.Add(newColumn)

                        '    For Each row As DataRow In dsRequestData.Tables(iCount).Rows
                        '        If Not IsDBNull(row("DateTime")) Then
                        '            row("ShortDate") = ConvertToUTC(CDate(row("DateTime")))
                        '        Else
                        '            row("ShortDate") = DBNull.Value
                        '        End If
                        '    Next
                        '    dsRequestData.Tables(iCount).Columns.Remove("DateTime")
                        '    dsRequestData.Tables(iCount).Columns("ShortDate").ColumnName = "DateTime"
                        'End If
                    Next


                    Dim dtNewMedicationPrescribedRow As DataRow = Nothing
                    Dim foundNewMedicationPrescribedRow As Boolean = False
                    Dim dtNewMedicationDispensedRow As DataRow = Nothing
                    Dim foundNewMedicationDispensedRow As Boolean = False

                    For Each row As DataRow In dsRequestData.Tables("Refills").Rows
                        If Not IsDBNull(row("MedicationPrescribed_Id")) Then
                            If (row("MedicationPrescribed_Id") = 0) Then
                                dtNewMedicationPrescribedRow = row
                                foundNewMedicationPrescribedRow = True
                                If (foundNewMedicationDispensedRow) Then
                                    Exit For

                                End If
                            End If
                        End If
                        If Not IsDBNull(row("MedicationDispensed_Id")) Then
                            If (row("MedicationDispensed_Id") = 0) Then
                                dtNewMedicationDispensedRow = row
                                foundNewMedicationDispensedRow = True
                                If (foundNewMedicationPrescribedRow) Then
                                    Exit For

                                End If
                            End If
                        End If
                    Next

                    If (foundNewMedicationPrescribedRow = False) Then
                        dtNewMedicationPrescribedRow = dsRequestData.Tables("Refills").NewRow()
                        dtNewMedicationPrescribedRow("MedicationPrescribed_Id") = 0
                    End If

                    If (foundNewMedicationDispensedRow = False) Then
                        dtNewMedicationDispensedRow = dsRequestData.Tables("Refills").NewRow()
                        dtNewMedicationDispensedRow("MedicationDispensed_Id") = 0
                    End If

                    If oDrug.RefillsQualifier = "PRN" Then
                        dtNewMedicationPrescribedRow("Qualifier") = "PRN"
                        dtNewMedicationPrescribedRow("Value") = DBNull.Value
                        dtNewMedicationDispensedRow("Qualifier") = "PRN"
                        dtNewMedicationDispensedRow("Value") = DBNull.Value
                    ElseIf oDrug.RefillsQualifier = "R" Or oDrug.RefillsQualifier = "P" Then
                        dtNewMedicationPrescribedRow("Qualifier") = "A"
                        dtNewMedicationDispensedRow("Qualifier") = "A"
                        'dtNewMedicationPrescribedRow("Value") = oDrug.RefillQuantity
                        If eRefillStatus = RefillStatus.eApproved Or eRefillStatus = RefillStatus.eApprovedWithChanges Then
                            If oDrug.RefillQuantity = "0" Then
                                dtNewMedicationPrescribedRow("Value") = "1"
                                dtNewMedicationDispensedRow("Value") = "1"
                            Else
                                dtNewMedicationPrescribedRow("Value") = oDrug.RefillQuantity
                                dtNewMedicationDispensedRow("Value") = oDrug.RefillQuantity
                            End If
                        Else
                            If eRefillStatus = RefillStatus.eDenied Or eRefillStatus = RefillStatus.eDeniedWithNewRxToFollow Then
                                dtNewMedicationPrescribedRow("Value") = 0
                                dtNewMedicationDispensedRow("Value") = 0
                            Else
                                dtNewMedicationPrescribedRow("Value") = oDrug.RefillQuantity
                                dtNewMedicationDispensedRow("Value") = oDrug.RefillQuantity
                            End If

                        End If
                    ElseIf oDrug.RefillsQualifier.Trim = "" Then
                        dtNewMedicationPrescribedRow("Qualifier") = "A"
                        dtNewMedicationDispensedRow("Qualifier") = "A"
                        'dtNewMedicationPrescribedRow("Value") = oDrug.RefillQuantity
                        dtNewMedicationPrescribedRow("Value") = 0
                        dtNewMedicationDispensedRow("Value") = 0
                    End If

                    If (foundNewMedicationPrescribedRow = False) Then
                        dsRequestData.Tables("Refills").Rows.Add(dtNewMedicationPrescribedRow)
                    End If
                    If (foundNewMedicationDispensedRow = False) Then
                        dsRequestData.Tables("Refills").Rows.Add(dtNewMedicationDispensedRow)
                    End If
                    dsRequestData.AcceptChanges()
                    'If dsRequestData.Tables("Refills").Rows.Count > 1 Then
                    '    dsRequestData.Tables("Refills").Rows(0)("Qualifier") = "A"
                    '    dsRequestData.Tables("Refills").Rows(1)("Qualifier") = "A"
                    'Else

                    '    Dim dtNewRefillRow As DataRow = dsRequestData.Tables("Refills").NewRow()
                    '    dtNewRefillRow("Qualifier") = "A"
                    '    dtNewRefillRow("MedicationPrescribed_Id") = 0
                    '    dsRequestData.Tables("Refills").Rows.Add(dtNewRefillRow)
                    '    dtNewRefillRow = Nothing
                    '    dtNewRefillRow = dsRequestData.Tables("Refills").NewRow()
                    '    dtNewRefillRow("Qualifier") = "A"
                    '    dtNewRefillRow("MedicationDispensed_Id") = 0
                    '    dsRequestData.Tables("Refills").Rows.Add(dtNewRefillRow)



                    'End If


                    ' ''Dim dtNewMedicationPrescribedRow As DataRow = Nothing
                    ' ''Dim foundNewMedicationPrescribedRow As Boolean = False
                    ' ''Dim dtNewMedicationDispensedRow As DataRow = Nothing
                    ' ''Dim foundNewMedicationDispensedRow As Boolean = False

                    ''For Each row As DataRow In dsRequestData.Tables("Diagnosis").Rows
                    ''    If Not IsDBNull(row("MedicationPrescribed_Id")) Then
                    ''        If (row("MedicationPrescribed_Id") = 0) Then
                    ''            dtNewMedicationPrescribedRow = row
                    ''            foundNewMedicationPrescribedRow = True
                    ''            If (foundNewMedicationDispensedRow) Then
                    ''                Exit For

                    ''            End If
                    ''        End If
                    ''    End If
                    ''    'If Not IsDBNull(row("MedicationDispensed_Id")) Then
                    ''    '    If (row("MedicationDispensed_Id") = 0) Then
                    ''    '        dtNewMedicationDispensedRow = row
                    ''    '        foundNewMedicationDispensedRow = True
                    ''    '        If (foundNewMedicationPrescribedRow) Then
                    ''    '            Exit For

                    ''    '        End If
                    ''    '    End If
                    ''    'End If
                    ''Next

                    ''If (foundNewMedicationPrescribedRow = False) Then
                    ''    dtNewMedicationPrescribedRow = dsRequestData.Tables("Diagnosis").NewRow()
                    ''    dtNewMedicationPrescribedRow("MedicationPrescribed_Id") = 0
                    ''End If

                    ' ''If (foundNewMedicationDispensedRow = False) Then
                    ' ''    dtNewMedicationDispensedRow = dsRequestData.Tables("Refills").NewRow()
                    ' ''    dtNewMedicationDispensedRow("MedicationDispensed_Id") = 0
                    ' ''End If

                    ''dtNewMedicationPrescribedRow("ClinicalInformationQualifier") = "1"
                    ''dtNewMedicationPrescribedRow("Qualifier") = oDrug.DiagnosisQualifier
                    ''dtNewMedicationPrescribedRow("Value") = oDrug.DiagnosisValue


                    ''If (foundNewMedicationPrescribedRow = False) Then
                    ''    dsRequestData.Tables("Diagnosis").Rows.Add(dtNewMedicationPrescribedRow)
                    ''End If

                    ''dsRequestData.AcceptChanges()



                    ''***############ DEA Schedule For CS ***
                    Dim dtNewMedPrescribedRow As DataRow = Nothing
                    Dim foundNewMedPrescribedRow As Boolean = False
                    For Each row As DataRow In dsRequestData.Tables("DrugCoded").Rows
                        If Not IsDBNull(row("MedicationPrescribed_Id")) Then
                            If (row("MedicationPrescribed_Id") = 0) Then
                                dtNewMedPrescribedRow = row
                                foundNewMedPrescribedRow = True
                                If (foundNewMedPrescribedRow) Then
                                    Exit For

                                End If
                            End If
                        End If
                    Next

                    If (foundNewMedPrescribedRow = False) Then
                        dtNewMedPrescribedRow = dsRequestData.Tables("DrugCoded").NewRow()
                        dtNewMedPrescribedRow("MedicationPrescribed_Id") = 0
                    End If
                    If oDrug.EPCSDEASchedule <> "" Then
                        dtNewMedPrescribedRow("DEASchedule") = oDrug.EPCSDEASchedule
                    End If

                    '' ** DNTF Fields ---
                    If oDrug.ProdCode <> "" Then
                        dtNewMedPrescribedRow("ProductCode") = oDrug.ProdCode
                    End If
                    ''---- **  end --
                    dsRequestData.AcceptChanges()
                    ''***############ End of DEA Schedule For CS ***


                    dsRequestData.Tables("RefillRequest").Rows.RemoveAt(0)

                End If

                '''''
                '' Diagnosis Codes - Primary 
                Dim dtNewRowPri As DataRow = Nothing
                Dim foundNewPriRow As Boolean = False
                For Each row As DataRow In dsRequestData.Tables("Primary").Rows
                    If Not IsDBNull(row("Diagnosis_Id")) Then
                        If (row("Diagnosis_Id") = 0) Then
                            dtNewRowPri = row
                            foundNewPriRow = True
                            If (foundNewPriRow) Then
                                Exit For
                            End If
                        End If
                    End If
                Next
                If Not String.IsNullOrEmpty(oDrug.PrimaryDXQualifier) Then
                    If (foundNewPriRow = False) Then
                        dtNewRowPri = dsRequestData.Tables("Primary").NewRow()
                        dtNewRowPri("Diagnosis_Id") = 0
                        ' Else
                    End If
                    dtNewRowPri("Qualifier") = oDrug.PrimaryDXQualifier
                    dtNewRowPri("Value") = oDrug.PrimaryDXValue

                    If (foundNewPriRow = False) Then
                        dsRequestData.Tables("Primary").Rows.Add(dtNewRowPri)
                    End If
                    dsRequestData.AcceptChanges()
                End If
                '''''
                '' Diagnosis Codes - Secondary

                Dim dtNewRowSec As DataRow = Nothing
                Dim foundNewSecRow As Boolean = False
                For Each row As DataRow In dsRequestData.Tables("Secondary").Rows
                    If Not IsDBNull(row("Diagnosis_Id")) Then
                        If (row("Diagnosis_Id") = 0) Then
                            dtNewRowSec = row
                            foundNewSecRow = True
                            If (foundNewSecRow) Then
                                Exit For
                            End If
                        End If
                    End If
                Next

                If Not String.IsNullOrEmpty(oDrug.SecondaryDXQualifier) Then
                    If (foundNewSecRow = False) Then
                        dtNewRowSec = dsRequestData.Tables("Secondary").NewRow()
                        dtNewRowSec("Diagnosis_Id") = 0
                        ' Else
                    End If
                    dtNewRowSec("Qualifier") = oDrug.SecondaryDXQualifier
                    dtNewRowSec("Value") = oDrug.SecondaryDXValue

                    If (foundNewSecRow = False) Then
                        dsRequestData.Tables("Secondary").Rows.Add(dtNewRowSec)
                    End If
                    dsRequestData.AcceptChanges()
                End If

                '''''
                '' Diagnosis Codes END

                Dim dtNewRow As DataRow = Nothing
                Dim dtNewWrittenDateRow As DataRow = Nothing
                Dim foundNewWrittenDateRow As Boolean = False
                Select Case eRefillStatus
                    Case RefillStatus.eApproved
                        If PONNumber <> 0 Then
                            dsRequestData.Tables("Header").Rows(0)("PrescriberOrderNumber") = PONNumber
                            PONNumber = 0
                        End If
                        For Each row As DataRow In dsRequestData.Tables("WrittenDate").Rows
                            If Not IsDBNull(row("MedicationPrescribed_Id")) Then
                                If (row("MedicationPrescribed_Id") = 0) Then
                                    dtNewWrittenDateRow = row
                                    foundNewWrittenDateRow = True
                                    If (foundNewWrittenDateRow) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If (foundNewWrittenDateRow = False) Then
                            dtNewWrittenDateRow = dsRequestData.Tables("WrittenDate").NewRow()
                            dtNewWrittenDateRow("MedicationPrescribed_Id") = 0
                        Else
                            If Not IsDBNull(dtNewWrittenDateRow("DateTime")) Then
                                dtNewWrittenDateRow("DateTime") = DBNull.Value
                            End If
                            dtNewWrittenDateRow("Date") = strdate
                        End If
                        If (foundNewWrittenDateRow = False) Then
                            dsRequestData.Tables("WrittenDate").Rows.Add(foundNewWrittenDateRow)
                        End If
                        dsRequestData.AcceptChanges()

                        dtNewRow = Nothing
                        dtNewWrittenDateRow = Nothing
                        foundNewWrittenDateRow = False
                        For Each row As DataRow In dsRequestData.Tables("WrittenDate").Rows
                            If Not IsDBNull(row("MedicationDispensed_Id")) Then
                                If (row("MedicationDispensed_Id") = 0) Then
                                    dtNewWrittenDateRow = row
                                    foundNewWrittenDateRow = True
                                    If (foundNewWrittenDateRow) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If (foundNewWrittenDateRow = False) Then
                            dtNewWrittenDateRow = dsRequestData.Tables("WrittenDate").NewRow()
                            dtNewWrittenDateRow("MedicationDispensed_Id") = 0
                        Else
                            If Not IsDBNull(dtNewWrittenDateRow("DateTime")) Then
                                dtNewWrittenDateRow("DateTime") = DBNull.Value
                            End If
                            dtNewWrittenDateRow("Date") = strdate
                        End If
                        If (foundNewWrittenDateRow = False) Then
                            dsRequestData.Tables("WrittenDate").Rows.Add(foundNewWrittenDateRow)
                        End If
                        dsRequestData.AcceptChanges()
                        dtNewRow = dsRequestData.Tables("Approved").NewRow()
                        If strnotes.Trim.Length > 0 Then
                            If strnotes.Trim.Length > 70 Then
                                RaiseEvent MessageInvalidated()
                                ValidationMessageBuilderforDrug.Append("The Response Note is too long for " & oDrug.DrugName & vbCrLf)
                                Return Nothing
                                Exit Function
                            Else
                                dtNewRow("Note") = strnotes
                            End If
                        End If
                        dtNewRow("Response_Id") = 0
                        dsRequestData.Tables("Approved").Rows.Add(dtNewRow)

                        ''########## EPCS Prescriber DEA for CS ***--
                        Dim foundRow As Boolean = False

                        For Each row As DataRow In dsRequestData.Tables("Identification").Rows
                            If Not IsDBNull(row("Prescriber_Id")) Then
                                If (row("Prescriber_Id") = 0) Then
                                    dtNewRow = row
                                    foundRow = True
                                    Exit For
                                End If
                            End If
                        Next
                        If (foundRow = False) Then
                            dtNewRow = dsRequestData.Tables("Identification").NewRow()
                            dtNewRow("Presecriber_Id") = 0
                        End If
                        Dim MaxCount As Integer = 0
                        For Each column As DataColumn In dsRequestData.Tables("Identification").Columns
                            If (column.ColumnName.Length <= 3) Then
                                If Not IsDBNull(dtNewRow(column)) Then
                                    MaxCount = MaxCount + 1
                                End If
                            Else
                                If (column.ColumnName.Substring(column.ColumnName.Length - 3) <> "_Id") Then
                                    If Not IsDBNull(dtNewRow(column)) Then
                                        MaxCount = MaxCount + 1
                                    End If
                                End If
                            End If
                        Next
                        If (MaxCount < 3) Then
                            If (objPrescription.RxPrescriber.PrescriberDEA <> "") AndAlso IsDBNull(dtNewRow("DEANumber")) Then
                                dtNewRow("DEANumber") = objPrescription.RxPrescriber.PrescriberDEA
                                MaxCount = MaxCount + 1
                            End If
                        End If
                        'If (MaxCount < 3) Then
                        '    If (objPrescription.RxPrescriber.PrescriberSSN <> "") And IsDBNull(dtNewRow("SocialSecurity")) Then
                        '        ' dtNewRow("SocialSecurity") = objPrescription.RxPrescriber.PrescriberSSN
                        '        MaxCount = MaxCount + 1
                        '    End If
                        'End If
                        'If (MaxCount < 3) Then
                        '    If (objPrescription.RxPrescriber.PrescriberNPI <> "") And IsDBNull(dtNewRow("NPI")) Then
                        '        dtNewRow("NPI") = objPrescription.RxPrescriber.PrescriberNPI
                        '        MaxCount = MaxCount + 1
                        '    End If
                        'End If

                        If (foundRow = False) Then
                            dsRequestData.Tables("Identification").Rows.Add(dtNewRow)
                        End If
                        dsRequestData.AcceptChanges()


                        dtNewRow = Nothing
                        foundRow = False

                        ''##########--xx  End EPCS Prescriber DEA for CS ***--

                        oResponse.ApprovalStatus = True
                        eMessageType = SentMessageType.eApproved

                    Case RefillStatus.eApprovedWithChanges
                        If PONNumber <> 0 Then
                            dsRequestData.Tables("Header").Rows(0)("PrescriberOrderNumber") = PONNumber
                            PONNumber = 0
                        End If
                        For Each row As DataRow In dsRequestData.Tables("WrittenDate").Rows
                            If Not IsDBNull(row("MedicationPrescribed_Id")) Then
                                If (row("MedicationPrescribed_Id") = 0) Then
                                    dtNewWrittenDateRow = row
                                    foundNewWrittenDateRow = True
                                    If (foundNewWrittenDateRow) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If (foundNewWrittenDateRow = False) Then
                            dtNewWrittenDateRow = dsRequestData.Tables("WrittenDate").NewRow()
                            dtNewWrittenDateRow("MedicationPrescribed_Id") = 0
                        Else
                            If Not IsDBNull(dtNewWrittenDateRow("DateTime")) Then
                                dtNewWrittenDateRow("DateTime") = DBNull.Value
                            End If
                            dtNewWrittenDateRow("Date") = strdate
                        End If
                        If (foundNewWrittenDateRow = False) Then
                            dsRequestData.Tables("WrittenDate").Rows.Add(foundNewWrittenDateRow)
                        End If
                        dsRequestData.AcceptChanges()

                        dtNewRow = Nothing
                        dtNewWrittenDateRow = Nothing
                        foundNewWrittenDateRow = False

                        For Each row As DataRow In dsRequestData.Tables("WrittenDate").Rows
                            If Not IsDBNull(row("MedicationDispensed_Id")) Then
                                If (row("MedicationDispensed_Id") = 0) Then
                                    dtNewWrittenDateRow = row
                                    foundNewWrittenDateRow = True
                                    If (foundNewWrittenDateRow) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If (foundNewWrittenDateRow = False) Then
                            dtNewWrittenDateRow = dsRequestData.Tables("WrittenDate").NewRow()
                            dtNewWrittenDateRow("MedicationDispensed_Id") = 0
                        Else
                            If Not IsDBNull(dtNewWrittenDateRow("DateTime")) Then
                                dtNewWrittenDateRow("DateTime") = DBNull.Value
                            End If
                            dtNewWrittenDateRow("Date") = strdate
                        End If
                        If (foundNewWrittenDateRow = False) Then
                            dsRequestData.Tables("WrittenDate").Rows.Add(foundNewWrittenDateRow)
                        End If
                        dsRequestData.AcceptChanges()

                        dtNewRow = dsRequestData.Tables("ApprovedWithChanges").NewRow()
                        If strnotes.Trim.Length > 0 Then
                            If strnotes.Trim.Length > 70 Then
                                ValidationMessageBuilderforDrug.Append("The Response Note is too long for " & oDrug.DrugName & vbCrLf)
                                Return Nothing
                                Exit Function
                            Else
                                dtNewRow("Note") = strnotes
                            End If
                        End If
                        'If oDrug.RefillsQualifier = "PRN" Then
                        '    dsRequestData.Tables("Refills").Rows(0)("Qualifier") = "PRN"
                        '    dsRequestData.Tables("Refills").Rows(0)("Value") = DBNull.Value
                        'ElseIf oDrug.RefillsQualifier.Trim = "R" Then
                        '    dsRequestData.Tables("Refills").Rows(0)("Qualifier") = "A"
                        '    dsRequestData.Tables("Refills").Rows(0)("Value") = oDrug.RefillQuantity
                        '    dsRequestData.Tables("Refills").Rows(1)("Qualifier") = "A"
                        '    dsRequestData.Tables("Refills").Rows(1)("Value") = oDrug.RefillQuantity
                        'End If
                        'dsRequestData.Tables("Refills").Rows(1)("value") = oDrug.MDRefillQuantity
                        dtNewRow("Response_Id") = 0
                        dsRequestData.Tables("ApprovedWithChanges").Rows.Add(dtNewRow)

                        eMessageType = SentMessageType.eApprovedWithChanges

                        dtNewRow = Nothing
                        Dim foundRow As Boolean = False

                        For Each row As DataRow In dsRequestData.Tables("Identification").Rows
                            If Not IsDBNull(row("Prescriber_Id")) Then
                                If (row("Prescriber_Id") = 0) Then
                                    dtNewRow = row
                                    foundRow = True
                                    Exit For
                                End If
                            End If
                        Next
                        If (foundRow = False) Then
                            dtNewRow = dsRequestData.Tables("Identification").NewRow()
                            dtNewRow("Presecriber_Id") = 0
                        End If
                        Dim MaxCount As Integer = 0
                        For Each column As DataColumn In dsRequestData.Tables("Identification").Columns
                            If (column.ColumnName.Length <= 3) Then
                                If Not IsDBNull(dtNewRow(column)) Then
                                    MaxCount = MaxCount + 1
                                End If
                            Else
                                If (column.ColumnName.Substring(column.ColumnName.Length - 3) <> "_Id") Then
                                    If Not IsDBNull(dtNewRow(column)) Then
                                        MaxCount = MaxCount + 1
                                    End If
                                End If
                            End If
                        Next
                        If (MaxCount < 3) Then
                            If (objPrescription.RxPrescriber.PrescriberDEA <> "") AndAlso IsDBNull(dtNewRow("DEANumber")) Then
                                dtNewRow("DEANumber") = objPrescription.RxPrescriber.PrescriberDEA
                                MaxCount = MaxCount + 1
                            End If
                        End If
                        If (MaxCount < 3) Then
                            If (objPrescription.RxPrescriber.PrescriberSSN <> "") AndAlso IsDBNull(dtNewRow("SocialSecurity")) Then
                                ' dtNewRow("SocialSecurity") = objPrescription.RxPrescriber.PrescriberSSN
                                MaxCount = MaxCount + 1
                            End If
                        End If
                        If (MaxCount < 3) Then
                            If (objPrescription.RxPrescriber.PrescriberNPI <> "") AndAlso IsDBNull(dtNewRow("NPI")) Then
                                dtNewRow("NPI") = objPrescription.RxPrescriber.PrescriberNPI
                                MaxCount = MaxCount + 1
                            End If
                        End If

                        If (foundRow = False) Then
                            dsRequestData.Tables("Identification").Rows.Add(dtNewRow)
                        End If
                        dsRequestData.AcceptChanges()


                        dtNewRow = Nothing
                        foundRow = False

                        For Each row As DataRow In dsRequestData.Tables("DrugCoded").Rows
                            If Not IsDBNull(row("MedicationDispensed_Id")) Then
                                If (row("MedicationDispensed_Id") = 0) Then
                                    dtNewRow = row
                                    foundRow = True
                                    Exit For
                                End If
                            End If
                        Next
                        If (foundRow = False) Then
                            dtNewRow = dsRequestData.Tables("DrugCoded").NewRow()
                            dtNewRow("MedicationDispensed_Id") = 0
                        End If

                        'If oDrug.ProdCode.Trim <> "" Then
                        '    dtNewRow("ProductCode") = oDrug.ProdCode.Trim
                        'End If

                        If (foundRow = False) Then
                            dsRequestData.Tables("DrugCoded").Rows.Add(dtNewRow)
                        End If
                        dsRequestData.AcceptChanges()



                        ''*** DNTF Changes *** ---
                        dtNewRow = Nothing
                        foundRow = False

                        ' ''For Each row As DataRow In dsRequestData.Tables("Quantity").Rows
                        ' ''    If Not IsDBNull(row("MedicationDispensed_Id")) Then
                        ' ''        If (row("MedicationDispensed_Id") = 0) Then
                        ' ''            dtNewRow = row
                        ' ''            foundRow = True
                        ' ''            Exit For
                        ' ''        End If
                        ' ''    End If
                        ' ''Next
                        '' ''If (foundRow = False) Then
                        '' ''    dtNewRow = dsRequestData.Tables("Quantity").NewRow()
                        '' ''    dtNewRow("MedicationDispensed_Id") = 0
                        '' ''End If
                        ' ''If (foundRow = True) Then
                        ' ''    If oDrug.DrugQuantity.Trim <> "" Then
                        ' ''        Dim _strQty As String() = Split(oDrug.DrugQuantity, " ")
                        ' ''        dtNewRow("Value") = Convert.ToString(_strQty(0))
                        ' ''        _strQty = Nothing
                        ' ''    End If
                        ' ''    If oDrug.PotencyCode.Trim <> "" Then
                        ' ''        dtNewRow("PotencyUnitCode") = Convert.ToString(oDrug.PotencyCode)
                        ' ''    End If
                        ' ''End If

                        '' ''If (foundRow = False) Then
                        '' ''    dsRequestData.Tables("Quantity").Rows.Add(dtNewRow)
                        '' ''End If


                        Dim dtNewMedicationPrescribedRow As DataRow = Nothing
                        Dim foundNewMedicationPrescribedRow As Boolean = False
                        Dim dtNewMedicationDispensedRow As DataRow = Nothing
                        Dim foundNewMedicationDispensedRow As Boolean = False

                        For Each row As DataRow In dsRequestData.Tables("Quantity").Rows
                            If Not IsDBNull(row("MedicationPrescribed_Id")) Then
                                If (row("MedicationPrescribed_Id") = 0) Then
                                    dtNewMedicationPrescribedRow = row
                                    foundNewMedicationPrescribedRow = True
                                    If (foundNewMedicationDispensedRow) Then
                                        Exit For

                                    End If
                                End If
                            End If
                            If Not IsDBNull(row("MedicationDispensed_Id")) Then
                                If (row("MedicationDispensed_Id") = 0) Then
                                    dtNewMedicationDispensedRow = row
                                    foundNewMedicationDispensedRow = True
                                    If (foundNewMedicationPrescribedRow) Then
                                        Exit For

                                    End If
                                End If
                            End If
                        Next



                        If (oDrug.DrugQuantity <> "") Then
                            dtNewMedicationPrescribedRow("Value") = oDrug.DrugQuantity
                            dtNewMedicationDispensedRow("Value") = oDrug.DrugQuantity
                        End If


                        If (oDrug.PotencyCode <> "") Then
                            dtNewMedicationPrescribedRow("PotencyUnitCode") = oDrug.PotencyCode.Trim
                            dtNewMedicationDispensedRow("PotencyUnitCode") = oDrug.PotencyCode.Trim
                        End If

                        'If (foundRow = False) Then
                        '    dsRequestData.Tables("Quantity").Rows.Add(dtNewRow)
                        'End If

                        dsRequestData.AcceptChanges()

                        ''** Days Supply
                        dtNewMedicationPrescribedRow = Nothing
                        foundNewMedicationPrescribedRow = False
                        dtNewMedicationDispensedRow = Nothing
                        foundNewMedicationDispensedRow = False

                        For Each row As DataRow In dsRequestData.Tables("MedicationPrescribed").Rows
                            If Not IsDBNull(row("MedicationPrescribed_Id")) Then
                                If (row("MedicationPrescribed_Id") = 0) Then
                                    dtNewMedicationPrescribedRow = row
                                    foundNewMedicationPrescribedRow = True
                                    Exit For
                                End If
                            End If
                        Next

                        For Each row As DataRow In dsRequestData.Tables("MedicationDispensed").Rows
                            If Not IsDBNull(row("MedicationDispensed_Id")) Then
                                If (row("MedicationDispensed_Id") = 0) Then
                                    dtNewMedicationDispensedRow = row
                                    foundNewMedicationDispensedRow = True
                                    Exit For
                                End If
                            End If
                        Next



                        'If (foundNewMedicationPrescribedRow = True) Then

                        '    If oDrug.DrugDuration.Trim <> "" Then
                        '        Dim _strDuration As String() = Split(oDrug.DrugDuration, " ")
                        '        dtNewMedicationPrescribedRow("DaysSupply") = Convert.ToString(_strDuration(0))
                        '        _strDuration = Nothing
                        '    End If

                        '    If oDrug.Directions.Trim <> "" Then
                        '        dtNewMedicationPrescribedRow("Directions") = Convert.ToString(oDrug.Directions.Trim)
                        '    End If

                        '    If oDrug.Notes.Trim <> "" Then
                        '        dtNewMedicationPrescribedRow("Note") = Convert.ToString(oDrug.Notes.Trim)
                        '    End If

                        '    If Not IsNothing(oDrug.MaySubstitute) Then
                        '        dtNewMedicationPrescribedRow("Substitutions") = If(Convert.ToBoolean(oDrug.MaySubstitute) = False, "1", "0")
                        '    End If

                        '    'If Not IsNothing(oDrug.PrimaryDXQualifier) Then

                        '    ' dtNewMedicationPrescribedRow("ClinicalInformationQualifier") = 1
                        '    '      dtNewMedicationPrescribedRow("ClinicalInformationQualifier") = 1
                        '    'dtNewMedicationPrescribedRow("Substitutions") = If(Convert.ToBoolean(oDrug.MaySubstitute) = False, "1", "0")
                        '    'End If

                        'End If
                        If (foundNewMedicationDispensedRow = True) Then

                            If oDrug.DrugDuration.Trim <> "" Then
                                Dim _strDuration As String() = Split(oDrug.DrugDuration, " ")
                                dtNewMedicationDispensedRow("DaysSupply") = Convert.ToString(_strDuration(0))
                                _strDuration = Nothing
                            End If

                            If oDrug.Directions.Trim <> "" Then
                                dtNewMedicationDispensedRow("Directions") = Convert.ToString(oDrug.Directions.Trim)
                            End If

                            If oDrug.Notes.Trim <> "" Then
                                dtNewMedicationDispensedRow("Note") = Convert.ToString(oDrug.Notes.Trim)
                            End If

                            If Not IsNothing(oDrug.MaySubstitute) Then
                                dtNewMedicationDispensedRow("Substitutions") = If(Convert.ToBoolean(oDrug.MaySubstitute) = False, "1", "0")
                            End If
                        End If
                        dsRequestData.AcceptChanges()
                        '' --- end

                        ''*** end DNTF *****---


                        ''---** DNTF Field **--
                        dtNewRow = Nothing
                        foundRow = False

                        For Each row As DataRow In dsRequestData.Tables("Identification").Rows
                            If Not IsDBNull(row("Pharmacy_Id")) Then
                                If (row("Pharmacy_Id") = 0) Then
                                    dtNewRow = row
                                    foundRow = True
                                    Exit For
                                End If
                            End If
                        Next
                        If (foundRow = False) Then
                            dtNewRow = dsRequestData.Tables("Identification").NewRow()
                            dtNewRow("Pharmacy_Id") = 0
                        End If
                        'MaxCount = 0
                        'For Each column As DataColumn In dsRequestData.Tables("Identification").Columns
                        '    If (column.ColumnName.Length <= 3) Then
                        '        If Not IsDBNull(dtNewRow(column)) Then
                        '            MaxCount = MaxCount + 1
                        '        End If
                        '    Else
                        '        If (column.ColumnName.Substring(column.ColumnName.Length - 3) <> "_Id") Then
                        '            If Not IsDBNull(dtNewRow(column)) Then
                        '                MaxCount = MaxCount + 1
                        '            End If
                        '        End If
                        '    End If
                        'Next
                        'If (MaxCount < 3) Then
                        If (oDrug.PhNCPDPID <> "") Then
                            dtNewRow("NCPDPID") = oDrug.PhNCPDPID
                            ' MaxCount = MaxCount + 1
                        End If
                        'End If


                        If (foundRow = False) Then
                            dsRequestData.Tables("Identification").Rows.Add(dtNewRow)
                        End If
                        dsRequestData.AcceptChanges()

                        '' -- ** End ---------



                        dtNewRow = Nothing
                        foundRow = False

                        For Each row As DataRow In dsRequestData.Tables("Address").Rows
                            If Not IsDBNull(row("Prescriber_Id")) Then
                                If (row("Prescriber_Id") = 0) Then
                                    dtNewRow = row
                                    foundRow = True
                                    Exit For
                                End If
                            End If
                        Next
                        If (foundRow = False) Then
                            dtNewRow = dsRequestData.Tables("Address").NewRow()
                            dtNewRow("Presecriber_Id") = 0
                        End If
                        If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim <> "" Then
                            If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Length > 35 Then
                                dtNewRow("AddressLine1") = objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Substring(0, 35)
                            Else
                                dtNewRow("AddressLine1") = objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim
                            End If
                        End If
                        If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim <> "" Then
                            If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Length > 35 Then
                                dtNewRow("PlaceLocationQualifier") = "AD2"
                                dtNewRow("AddressLine2") = objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Substring(0, 35)
                            Else
                                dtNewRow("PlaceLocationQualifier") = "AD2"
                                dtNewRow("AddressLine2") = objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim
                            End If
                        Else
                            dtNewRow("PlaceLocationQualifier") = DBNull.Value
                            dtNewRow("AddressLine2") = DBNull.Value
                        End If

                        If objPrescription.RxPrescriber.PrescriberAddress.City.Trim <> "" Then
                            If objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Length > 35 Then
                                dtNewRow("City") = objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Substring(0, 35)
                            Else
                                dtNewRow("City") = objPrescription.RxPrescriber.PrescriberAddress.City.Trim
                            End If
                        End If

                        If objPrescription.RxPrescriber.PrescriberAddress.State.Trim <> "" Then
                            If objPrescription.RxPrescriber.PrescriberAddress.State.Trim.Length > 9 Then
                                dtNewRow("State") = objPrescription.RxPrescriber.PrescriberAddress.State.Trim.Substring(0, 9)
                            Else
                                dtNewRow("State") = objPrescription.RxPrescriber.PrescriberAddress.State
                            End If
                        End If

                        If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim <> "" Then
                            If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim.Length > 11 Then
                                dtNewRow("ZipCode") = objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim.Substring(0, 11)
                            Else
                                dtNewRow("ZipCode") = objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim
                            End If
                        End If

                        If (foundRow = False) Then
                            dsRequestData.Tables("Address").Rows.Add(dtNewRow)
                        End If
                        dsRequestData.AcceptChanges()

                        dtNewRow = Nothing
                        foundRow = False
                        Dim comID As Integer = Nothing
                        For Each row As DataRow In dsRequestData.Tables("CommunicationNumbers").Rows
                            If Not IsDBNull(row("Prescriber_Id")) Then
                                If (row("Prescriber_Id") = 0) Then
                                    dtNewRow = row
                                    foundRow = True
                                    Exit For
                                End If
                            End If
                        Next
                        If (foundRow = False) Then
                            dtNewRow = dsRequestData.Tables("CommunicationNumbers").NewRow()
                            dtNewRow("Prescriber_Id") = 0
                            If dsRequestData.Tables("CommunicationNumbers").Rows.Count = 0 Then
                                comID = 0
                            Else
                                comID = dsRequestData.Tables("CommunicationNumbers").Rows.Count
                            End If
                            dtNewRow("CommunicationNumbers_ID") = comID
                        Else
                            comID = dtNewRow("CommunicationNumbers_ID")
                        End If
                        If (foundRow = False) Then
                            dsRequestData.Tables("CommunicationNumbers").Rows.Add(dtNewRow)
                        End If
                        dsRequestData.AcceptChanges()
                        dtNewRow = Nothing
                        foundRow = False


                        Dim dtPhoneNewRow As DataRow = Nothing
                        Dim dtFaxNewRow As DataRow = Nothing
                        Dim foundPhoneRow As Boolean = False
                        Dim foundFaxRow As Boolean = False

                        Dim myCount As Integer = dsRequestData.Tables("Communication").Rows.Count

                        For iCount As Integer = myCount - 1 To 0 Step -1
                            Dim row As DataRow = dsRequestData.Tables("Communication").Rows(iCount)
                            If Not IsDBNull(row("CommunicationNumbers_ID")) Then
                                If (row("CommunicationNumbers_ID") = comID) Then
                                    If (row("Qualifier") = "TE") Then
                                        If foundPhoneRow Then
                                            dsRequestData.Tables("Communication").Rows.Remove(row)
                                        Else
                                            dtPhoneNewRow = row
                                            foundPhoneRow = True
                                        End If

                                    ElseIf row("Qualifier") = "FX" Then
                                        If foundFaxRow Then
                                            dsRequestData.Tables("Communication").Rows.Remove(row)
                                        Else
                                            dtFaxNewRow = row
                                            foundFaxRow = True
                                        End If
                                    Else
                                        dsRequestData.Tables("Communication").Rows.Remove(row)
                                    End If

                                End If
                            End If
                        Next
                        ''Fax number not present

                        If objPrescription.RxPrescriber.PrescriberPhone.Fax.Trim <> "" AndAlso foundFaxRow = False Then
                            'If Then
                            dtFaxNewRow = dsRequestData.Tables("Communication").NewRow()
                            dtFaxNewRow("CommunicationNumbers_ID") = comID
                            dtFaxNewRow("Qualifier") = "FX"
                            dtFaxNewRow("Number") = objPrescription.RxPrescriber.PrescriberPhone.Fax.Trim

                            dsRequestData.Tables("Communication").Rows.Add(dtFaxNewRow)
                            'End If
                        End If
                        'If (foundFaxRow = False) Then

                        'End If
                        ''Phone number not present
                        If foundPhoneRow = False Then
                            dtPhoneNewRow = dsRequestData.Tables("Communication").NewRow()
                            dtPhoneNewRow("CommunicationNumbers_ID") = comID
                            dtPhoneNewRow("Qualifier") = "TE"
                        End If

                        If objPrescription.RxPrescriber.PrescriberPhone.Phone.Trim <> "" Then
                            dtPhoneNewRow("Number") = objPrescription.RxPrescriber.PrescriberPhone.Phone.Trim
                        End If
                        If (foundPhoneRow = False) Then
                            dsRequestData.Tables("Communication").Rows.Add(dtPhoneNewRow)
                        End If

                        dsRequestData.AcceptChanges()



                        ''Supervisor
                        If objPrescription.RxSupervisorProviderID <> 0 Then
                            dtNewRow = Nothing
                            foundRow = False
                            For Each row As DataRow In dsRequestData.Tables("Supervisor").Rows
                                If Not IsDBNull(row("Supervisor_Id")) Then
                                    If (row("Supervisor_Id") = 0) Then
                                        dtNewRow = row
                                        foundRow = True
                                        Exit For
                                    End If
                                End If
                            Next


                            If (foundRow = False) Then
                                dtNewRow = dsRequestData.Tables("Supervisor").NewRow()
                                dtNewRow("Supervisor_Id") = 0
                                dtNewRow("RefillResponse_Id") = 0
                            End If

                            dtNewRow("Specialty") = DBNull.Value
                            dtNewRow("ClinicName") = DBNull.Value

                            If (foundRow = False) Then
                                dsRequestData.Tables("Supervisor").Rows.Add(dtNewRow)
                            End If
                            dsRequestData.AcceptChanges()

                            dtNewRow = Nothing
                            foundRow = False

                            For Each row As DataRow In dsRequestData.Tables("Identification").Rows
                                If Not IsDBNull(row("Supervisor_Id")) Then
                                    If (row("Supervisor_Id") = 0) Then
                                        dtNewRow = row
                                        foundRow = True
                                        Exit For
                                    End If
                                End If
                            Next

                            If (foundRow = False) Then
                                dtNewRow = dsRequestData.Tables("Identification").NewRow()
                                dtNewRow("Supervisor_Id") = 0
                            End If



                            For Each column As DataColumn In dsRequestData.Tables("Identification").Columns
                                If (column.ColumnName.Length <= 3) Then
                                    If Not IsDBNull(dtNewRow(column)) Then
                                        dtNewRow(column) = DBNull.Value
                                    End If
                                Else
                                    If (column.ColumnName.Substring(column.ColumnName.Length - 3) <> "_Id") Then
                                        If Not IsDBNull(dtNewRow(column)) Then
                                            dtNewRow(column) = DBNull.Value
                                        End If
                                    End If
                                End If
                            Next

                            If (objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI <> "") AndAlso IsDBNull(dtNewRow("NPI")) Then
                                dtNewRow("NPI") = objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI
                            End If

                            If (objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA <> "") AndAlso IsDBNull(dtNewRow("DEANumber")) Then
                                dtNewRow("DEANumber") = objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA
                            End If

                            'If (objPrescription.RxSupervisorPrescriber.SupervisorProviderSSN <> "") And IsDBNull(dtNewRow("SocialSecurity")) Then
                            '    dtNewRow("SocialSecurity") = objPrescription.RxSupervisorPrescriber.SupervisorProviderSSN
                            'End If


                            If (foundRow = False) Then
                                dsRequestData.Tables("Identification").Rows.Add(dtNewRow)
                            End If
                            dsRequestData.AcceptChanges()


                            dtNewRow = Nothing
                            foundRow = False

                            For Each row As DataRow In dsRequestData.Tables("Name").Rows
                                If Not IsDBNull(row("Supervisor_Id")) Then
                                    If (row("Supervisor_Id") = 0) Then
                                        dtNewRow = row
                                        foundRow = True
                                        Exit For
                                    End If
                                End If
                            Next
                            If (foundRow = False) Then
                                dtNewRow = dsRequestData.Tables("Name").NewRow()
                                dtNewRow("Supervisor_Id") = 0
                            End If
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim.Length > 35 Then
                                dtNewRow("LastName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim.Substring(0, 35)
                            Else
                                dtNewRow("LastName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim()
                            End If
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Length > 0 Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Length > 35 Then
                                    dtNewRow("FirstName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Substring(0, 35)
                                Else
                                    dtNewRow("FirstName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim
                                End If
                            Else
                                dtNewRow("FirstName") = DBNull.Value
                            End If
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Length > 0 Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Length > 35 Then
                                    dtNewRow("MiddleName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Substring(0, 35)
                                Else
                                    dtNewRow("MiddleName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim
                                End If
                            Else
                                dtNewRow("MiddleName") = DBNull.Value
                            End If

                            dtNewRow("Suffix") = DBNull.Value
                            dtNewRow("Prefix") = DBNull.Value
                            If (foundRow = False) Then
                                dsRequestData.Tables("Name").Rows.Add(dtNewRow)
                            End If
                            dsRequestData.AcceptChanges()

                            dtNewRow = Nothing
                            foundRow = False

                            For Each row As DataRow In dsRequestData.Tables("Address").Rows
                                If Not IsDBNull(row("Supervisor_Id")) Then
                                    If (row("Supervisor_Id") = 0) Then
                                        dtNewRow = row
                                        foundRow = True
                                        Exit For
                                    End If
                                End If
                            Next
                            If (foundRow = False) Then
                                dtNewRow = dsRequestData.Tables("Address").NewRow()
                                dtNewRow("Supervisor_Id") = 0
                            End If
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim <> "" Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim.Length > 35 Then
                                    dtNewRow("AddressLine1") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim.Substring(0, 35)
                                Else
                                    dtNewRow("AddressLine1") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim
                                End If
                            End If
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim <> "" Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim.Length > 35 Then
                                    dtNewRow("PlaceLocationQualifier") = "AD2"
                                    dtNewRow("AddressLine2") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim.Substring(0, 35)
                                Else
                                    dtNewRow("PlaceLocationQualifier") = "AD2"
                                    dtNewRow("AddressLine2") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim
                                End If
                            Else
                                dtNewRow("PlaceLocationQualifier") = DBNull.Value
                                dtNewRow("AddressLine2") = DBNull.Value
                            End If

                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim <> "" Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim.Length > 35 Then
                                    dtNewRow("City") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim.Substring(0, 35)
                                Else
                                    dtNewRow("City") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim
                                End If
                            End If

                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Trim <> "" Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Trim.Length > 9 Then
                                    dtNewRow("State") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Trim.Substring(0, 9)
                                Else
                                    dtNewRow("State") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State
                                End If
                            End If

                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim <> "" Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim.Length > 11 Then
                                    dtNewRow("ZipCode") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim.Substring(0, 11)
                                Else
                                    dtNewRow("ZipCode") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim
                                End If
                            End If

                            If (foundRow = False) Then
                                dsRequestData.Tables("Address").Rows.Add(dtNewRow)
                            End If
                            dsRequestData.AcceptChanges()


                            dtNewRow = Nothing
                            foundRow = False
                            comID = Nothing
                            For Each row As DataRow In dsRequestData.Tables("CommunicationNumbers").Rows
                                If Not IsDBNull(row("Supervisor_Id")) Then
                                    If (row("Supervisor_Id") = 0) Then
                                        dtNewRow = row
                                        foundRow = True
                                        Exit For
                                    End If
                                End If
                            Next
                            If (foundRow = False) Then
                                dtNewRow = dsRequestData.Tables("CommunicationNumbers").NewRow()
                                dtNewRow("Supervisor_Id") = 0
                                If dsRequestData.Tables("CommunicationNumbers").Rows.Count = 0 Then
                                    comID = 0
                                Else
                                    comID = dsRequestData.Tables("CommunicationNumbers").Rows.Count
                                End If
                                dtNewRow("CommunicationNumbers_ID") = comID
                            Else
                                comID = dtNewRow("CommunicationNumbers_ID")
                            End If
                            If (foundRow = False) Then
                                dsRequestData.Tables("CommunicationNumbers").Rows.Add(dtNewRow)
                            End If
                            dsRequestData.AcceptChanges()
                            dtNewRow = Nothing
                            foundRow = False


                            dtPhoneNewRow = Nothing
                            dtFaxNewRow = Nothing
                            foundPhoneRow = False
                            foundFaxRow = False

                            myCount = dsRequestData.Tables("Communication").Rows.Count

                            For iCount As Integer = myCount - 1 To 0 Step -1
                                Dim row As DataRow = dsRequestData.Tables("Communication").Rows(iCount)
                                If Not IsDBNull(row("CommunicationNumbers_ID")) Then
                                    If (row("CommunicationNumbers_ID") = comID) Then
                                        If (row("Qualifier") = "TE") Then
                                            If foundPhoneRow Then
                                                dsRequestData.Tables("Communication").Rows.Remove(row)
                                            Else
                                                dtPhoneNewRow = row
                                                foundPhoneRow = True
                                            End If
                                        ElseIf row("Qualifier") = "FX" Then
                                            If foundFaxRow Then
                                                dsRequestData.Tables("Communication").Rows.Remove(row)
                                            Else
                                                dtFaxNewRow = row
                                                foundFaxRow = True
                                            End If
                                        Else
                                            dsRequestData.Tables("Communication").Rows.Remove(row)
                                        End If

                                    End If
                                End If
                            Next

                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim <> "" And Not foundFaxRow = False Then

                                dtFaxNewRow = dsRequestData.Tables("Communication").NewRow()
                                dtFaxNewRow("CommunicationNumbers_ID") = comID
                                dtFaxNewRow("Qualifier") = "FX"

                                dtFaxNewRow("Number") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax.Trim

                                dsRequestData.Tables("Communication").Rows.Add(dtFaxNewRow)

                            End If

                            If foundPhoneRow = False Then
                                dtPhoneNewRow = dsRequestData.Tables("Communication").NewRow()
                                dtPhoneNewRow("CommunicationNumbers_ID") = comID
                                dtPhoneNewRow("Qualifier") = "TE"
                            End If

                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim <> "" Then
                                dtPhoneNewRow("Number") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim
                            End If
                            If (foundPhoneRow = False) Then
                                dsRequestData.Tables("Communication").Rows.Add(dtPhoneNewRow)
                            End If

                            'If foundFaxRow = False Then

                            'End If

                            'If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax.Trim <> "" Then

                            'End If
                            'If (foundFaxRow = False) Then

                            'End If
                            dsRequestData.AcceptChanges()
                            'dtNewRow = dsViewDataset.Tables("CommunicationNumbers").NewRow()
                            'dtNewRow("CommunicationNumbers_ID") = dsViewDataset.Tables("CommunicationNumbers").Rows.Count + 1
                            'dtNewRow("Prescriber_ID") = 0
                            'dsViewDataset.Tables("CommunicationNumbers").Rows.Add(dtNewRow)
                            'dtNewRow = Nothing

                            'dtNewRow = dsViewDataset.Tables("Communication").NewRow()
                            'dtNewRow("CommunicationNumbers_ID") = dsViewDataset.Tables("CommunicationNumbers").Rows.Count + 1
                            'dtNewRow("Number") = strPrescriberPhone
                            'dtNewRow("Qualifier") = "TE"
                            'dsViewDataset.Tables("Communication").Rows.Add(dtNewRow)
                            'dtNewRow = Nothing

                            'dtNewRow = dsViewDataset.Tables("Communication").NewRow()
                            'dtNewRow("CommunicationNumbers_ID") = dsViewDataset.Tables("CommunicationNumbers").Rows.Count + 1
                            'dtNewRow("Number") = strPrescriberFax
                            'dtNewRow("Qualifier") = "FX"
                            'dsViewDataset.Tables("Communication").Rows.Add(dtNewRow)
                            'dtNewRow = Nothing

                            '' ****    ---- DNTF Fields Changes ----- ***
                            dtNewRow = Nothing
                            foundRow = False

                            ''*** end DNTF ***




                        End If

                        oResponse.ApprovalStatus = True
                        eMessageType = SentMessageType.eApprovedWithChanges
                    Case RefillStatus.eDenied
                        If dsRequestData.Tables("Refills").Rows(0)("Qualifier") <> "PRN" Then
                            dsRequestData.Tables("Refills").Rows(0)("Value") = 0
                        End If
                        If dsRequestData.Tables("Refills").Rows(1)("Qualifier") <> "PRN" Then
                            dsRequestData.Tables("Refills").Rows(1)("Value") = 0
                        End If
                        dtNewRow = dsRequestData.Tables("Denied").NewRow()
                        If strnotes.Trim.Length > 0 Then
                            If strnotes.Trim.Length > 70 Then
                                RaiseEvent MessageInvalidated()
                                ValidationMessageBuilderforDrug.Append("The Response Note is too long for " & oDrug.DrugName & vbCrLf)
                                Return Nothing
                                Exit Function

                            Else
                                dtNewRow("DenialReason") = strnotes
                            End If
                        End If
                        'dtNewRow = dsRequestData.Tables("Denied").NewRow()
                        If Not IsNothing(denialreasoncode) Then
                            If denialreasoncode.Trim.Length > 0 Then
                                dtNewRow("DenialReasonCode") = denialreasoncode
                            Else
                                dtNewRow("DenialReasonCode") = "AG"
                            End If
                        Else
                            dtNewRow("DenialReasonCode") = "AG"
                        End If

                        dtNewRow("Response_Id") = 0
                        dsRequestData.Tables("Denied").Rows.Add(dtNewRow)
                        dsRequestData.AcceptChanges()
                        oResponse.ApprovalStatus = False
                        oResponse.Notes = strnotes
                        eMessageType = SentMessageType.eDenied
                    Case RefillStatus.eDeniedWithNewRxToFollow
                        If dsRequestData.Tables("Refills").Rows(0)("Qualifier") <> "PRN" Then
                            dsRequestData.Tables("Refills").Rows(0)("Value") = 0
                        End If
                        If dsRequestData.Tables("Refills").Rows(1)("Qualifier") <> "PRN" Then
                            dsRequestData.Tables("Refills").Rows(1)("Value") = 0
                        End If
                        dtNewRow = dsRequestData.Tables("DeniedNewPrescriptionToFollow").NewRow()

                        ''do not send note "Approved drug is Schedule 3 to 5, Rx will be faxed to pharmacy" when the drug is narcotic in DTNF case.
                        'If strnotes.Trim.Length > 0 Then
                        '    If strnotes.Trim.Length > 70 Then
                        '        RaiseEvent MessageInvalidated()
                        '        ValidationMessageBuilderforDrug.Append("The Response Note is too long for " & oDrug.DrugName & vbCrLf)
                        '        Return Nothing
                        '        Exit Function

                        '    Else
                        '        dtNewRow("DenialReason") = strnotes
                        '    End If
                        'End If
                        dtNewRow("DenialReasonCode") = "AG"
                        If strnotes <> "" Then
                            dtNewRow("DenialReason") = strnotes
                        End If
                        dtNewRow("ReferenceNumber") = oDrug.RxReferenceNumber
                        dtNewRow("Response_Id") = 0
                        dsRequestData.Tables("DeniedNewPrescriptionToFollow").Rows.Add(dtNewRow)
                        dsRequestData.AcceptChanges()
                        oResponse.ApprovalStatus = False
                        oResponse.Notes = strnotes
                        eMessageType = SentMessageType.eDeniedWithNewRxToFollow
                End Select

                dsRequestData.Tables("Message").WriteXml(strfilepath)

            Catch ex As Exception
                MessageBox.Show(ex.ToString(), "gloEMR")
            End Try


            '''''Change the order of elements using Xsl
            'Dim _strfileName2 As String = ""
            Dim myXslTransform As Xsl.XslCompiledTransform = Nothing
            Dim _strfileName As String = gloSettings.FolderSettings.AppTempFolderPath & "0" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") & ".xml"
            myXslTransform = New Xml.Xsl.XslCompiledTransform()
            myXslTransform.Load(System.Windows.Forms.Application.StartupPath & "\namespaceremoval.xsl")
            myXslTransform.Transform(strfilepath, _strfileName)
            'SLR: Free myXslTransofrm
            If Not myXslTransform Is Nothing Then
                myXslTransform = Nothing
            End If
            Dim _strfileName1 As String = gloSettings.FolderSettings.AppTempFolderPath & "1" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") & ".xml"
            myXslTransform = New Xml.Xsl.XslCompiledTransform()
            myXslTransform.Load(System.Windows.Forms.Application.StartupPath & "\MessageXSLTFile.xsl")
            myXslTransform.Transform(_strfileName, _strfileName1)
            'SLR: Free myXslTransofrm
            If Not myXslTransform Is Nothing Then
                myXslTransform = Nothing
            End If
            _strfileName2 = gloSettings.FolderSettings.AppTempFolderPath & "2" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") & ".xml"
            myXslTransform = New Xml.Xsl.XslCompiledTransform()
            myXslTransform.Load(System.Windows.Forms.Application.StartupPath & "\namespaceadd.xsl")
            myXslTransform.Transform(_strfileName1, _strfileName2)
            'SLR: Free myXslTransofrm
            If Not myXslTransform Is Nothing Then
                myXslTransform = Nothing
            End If

            '''''Change the order of elements using Xsl

            'Dim strfilename As String = ""
            'Dim NoOfAttempt As Integer = 1
            'While (NoOfAttempt <= 5)
            '    strfilename = ExtractXML(gloSurescriptGeneral.ConvertFiletoBinary(_strfileName2))
            '    If strfilename <> "" Then
            '        Exit While
            '    Else
            '        NoOfAttempt = NoOfAttempt + 1
            '    End If
            'End While

            'If strfilename <> "" Then
            '    ReadResponse(strfilename, "Status", eMessageType, oDrug.MDDrugName)
            '    oDrug.MessageName = "RefillResponse"
            '    'Once the RefillResponse is generated now generate a FreeStanding verify message
            '    'to inform surescript that RefillRequest has been processed successfully
            '    Dim oSurescriptDBLayer As New gloSureScriptDBLayer
            '    oSurescriptDBLayer.InsertintoMessageTransaction(CType(oDrug, SureScriptMessage))
            '    If MessageName = "Status" Then
            '        oResponse.StatusMessageType = StatusMessageType
            '        oResponse.ProviderId = Convert.ToInt64(objPrescription.RxPrescriber.PrescriberID)
            '        If RefReqPatientID.ToString <> "" Then ''''''''refreqpatientid is used for denied ref req patient report
            '            oResponse.RefReqPatientId = Convert.ToInt64(RefReqPatientID.ToString)
            '        Else
            '            oResponse.RefReqPatientId = 0
            '        End If
            '        oSurescriptDBLayer.InsertResponseTransaction(oResponse, False)
            '    ElseIf MessageName = "Error" Then
            '        oResponse.StatusMessageType = StatusMessageType
            '        oResponse.ProviderId = Convert.ToInt64(objPrescription.RxPrescriber.PrescriberID)
            '        If RefReqPatientID.ToString <> "" Then ''''''''refreqpatientid is used for denied ref req patient report
            '            oResponse.RefReqPatientId = Convert.ToInt64(RefReqPatientID.ToString)
            '        Else
            '            oResponse.RefReqPatientId = 0
            '        End If

            '        oSurescriptDBLayer.InsertResponseTransaction(oResponse, False)
            '    End If
            '    If Not IsNothing(oSurescriptDBLayer) Then
            '        oSurescriptDBLayer.Dispose()
            '        oSurescriptDBLayer = Nothing
            '    End If
            '    Return True
            'Else
            '    RaiseEvent MessageInvalidated()
            '    Return False
            'End If


            ' gloSurescriptGeneral.ConvertFiletoBinary(strfilepath)

            oDrug.TransactionID = objPrescription.RxTransactionID
            Return _strfileName2

            gloSurescriptGeneral.UpdateLog("Exiting gloSureScriptsInterface.GenerateRefillResponse")


        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            gloSurescriptGeneral.UpdateLog("Error Occurred processing refill response: " & ex.Message)
            Return Nothing
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            gloSurescriptGeneral.UpdateLog("Error Occurred processing refill response: " & ex.Message)
            Return Nothing
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(oResponse) Then
                oResponse.Dispose()
                oResponse = Nothing
            End If
            If Not IsNothing(dsRequestData) Then
                dsRequestData.Dispose()
                dsRequestData = Nothing
            End If
        End Try

        'Else
        'RaiseEvent MessageInvalidated()
        'End If
        ''ValidationMessageforDrug = ValidationMessageBuilderforDrug.ToString.Trim
        'Return Nothing
    End Function

    Public Function GenerateRefillResponseForDNTF(ByVal icnt As Int16, ByVal objPrescription As EPrescription, ByVal eRefillStatus As RefillStatus, ByVal RefillItem As Int32, Optional ByVal denialreasoncode As String = "", Optional ByVal strnotes As String = "", Optional ByVal RefReqPatientID As String = "", Optional ByVal PONNumber As Long = 0)
        gloSurescriptGeneral.UpdateLog("Entering gloSureScriptsInterface.GenerateRefillResponse")
        Dim dsRequestData As DataSet = Nothing
        Dim _strfileName2 As String = ""
        Dim oResponse As SureScriptResponseMessage = Nothing
        Try
            dsRequestData = New DataSet
            Dim strfilepath As String = GenerateFileName(MessageType.eRefillResponse)
            XMLFilePath = strfilepath
            'Dim xmlwriter As XmlTextWriter = Nothing
            Dim eMessageType As SentMessageType


            ''Modiy to From and Header Feilds

            If File.Exists(strfilepath) Then
                File.Delete(strfilepath)
            End If

            Dim dtdate As DateTime = Date.UtcNow
            Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
            Dim strtime As String = Format(dtdate, "hh:mm:ss")
            Dim strmillisec As String = Format(dtdate, "mmm")
            Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

            'Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
            'strUTCFormat = strdate & "T" & strtime & ".0Z"
            Dim strUTCFormat As String = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.FZ")
            Dim oDrug As EDrug
            oDrug = objPrescription.DrugsCol.Item(RefillItem)
            ''Load the request file in dataset
            Dim filedata As Byte() = Nothing

            filedata = objPrescription.FileData
            If IsNothing(filedata) Then
                filedata = oDrug.FileData
            End If

            LoadtoDataset(dsRequestData, filedata)

            If File.Exists(strfilepath) Then
                File.Delete(strfilepath)
            End If


            oDrug.MessageID = "RefillResponse" & strtemp

            oDrug.DateTimeStamp = strUTCFormat
            oDrug.DateReceived = Date.Now
            oDrug.MessageFrom = objPrescription.RxPrescriber.PrescriberID
            If oDrug.PhNCPDPID = "" Then
                oDrug.MessageTo = objPrescription.RxPharmacy.PharmacyID
            Else
                oDrug.MessageTo = oDrug.PhNCPDPID
            End If

            oResponse = New SureScriptResponseMessage


            dsRequestData.Tables("Message").Rows(0)("version") = "010"

            dsRequestData.Tables("Message").Rows(0)("release") = "006"


            dsRequestData.Tables("To").Rows(0)("Qualifier") = "P"

            dsRequestData.Tables("To").Rows(0)("To_Text") = oDrug.MessageTo

            dsRequestData.Tables("From").Rows(0)("Qualifier") = "D"

            dsRequestData.Tables("From").Rows(0)("From_Text") = oDrug.MessageFrom

            dsRequestData.Tables("Header").Rows(0)("MessageID") = oDrug.MessageID

            dsRequestData.Tables("Header").Rows(0)("SentTime") = strUTCFormat

            Dim strVersion As String = My.Application.Info.Version.ToString.Substring(0, 3)
            Dim strProductName As String = My.Application.Info.ProductName.ToString()
            Dim strCompany As String = My.Application.Info.CompanyName

            'dsRequestData.Tables("SenderSoftware").Rows(0)("SenderSoftwareDeveloper") = strCompany

            'dsRequestData.Tables("SenderSoftware").Rows(0)("SenderSoftwareProduct") = strProductName

            'dsRequestData.Tables("SenderSoftware").Rows(0)("SenderSoftwareVersionRelease") = strVersion


            dsRequestData.Tables("Header").Rows(0)("RelatesToMessageID") = oDrug.RelatesToMessageId



            If oDrug.RxReferenceNumber <> "" Then
                dsRequestData.Tables("Header").Rows(0)("RxReferenceNumber") = oDrug.RxReferenceNumber
            End If

            objPrescription.RxTransactionID = oDrug.PrescriptionID
            If Not IsNothing(oDrug.TransactionID) Then

                If objPrescription.RxTransactionID.Trim.Length > 0 Then
                    If Not IsNumeric(objPrescription.RxTransactionID) Then
                        dsRequestData.Tables("Header").Rows(0)("PrescriberOrderNumber") = objPrescription.RxTransactionID
                    Else
                        If Val(objPrescription.RxTransactionID) <> 0 Then
                            dsRequestData.Tables("Header").Rows(0)("PrescriberOrderNumber") = objPrescription.RxTransactionID
                        End If
                    End If

                    'Else
                    '    Dim nPrescOrdNo As Long = gloSurescriptGeneral.GetUniqueID()
                    '    dsRequestData.Tables("Header").Rows(0)("PrescriberOrderNumber") = nPrescOrdNo
                End If
            End If

            oResponse.MessageID = oDrug.MessageID
            ''end

            ''Depending upon response add the required table

            'Dim dsResponse As DataSet
            Try

                If dsRequestData.Tables.Count > 0 Then

                    'dsResponse = New DataSet
                    'dsResponse.ReadXmlSchema("D:\Schema\SS_SCRIPT_XML_10_6Mod.xsd")

                    Dim MyRow As DataRow = dsRequestData.Tables("RefillResponse").NewRow()
                    MyRow("RefillResponse_Id") = dsRequestData.Tables("RefillRequest").Rows(0)("RefillRequest_Id")
                    MyRow("Body_Id") = dsRequestData.Tables("RefillRequest").Rows(0)("Body_Id")
                    dsRequestData.Tables("RefillResponse").Rows.Add(MyRow)

                    MyRow = dsRequestData.Tables("Response").NewRow()
                    MyRow("Response_Id") = 0
                    MyRow("RefillResponse_Id") = dsRequestData.Tables("RefillResponse").Rows(0)("RefillResponse_Id")
                    dsRequestData.Tables("Response").Rows.Add(MyRow)


                    For iCount As Integer = 0 To dsRequestData.Tables.Count - 1

                        If (dsRequestData.Tables(iCount).TableName <> "RefillRequest") Then
                            If dsRequestData.Tables(iCount).Columns.Contains("RefillRequest_Id") = True Then

                                If dsRequestData.Tables(iCount).Columns.Contains("RefillResponse_Id") = True Then
                                    For Each row As DataRow In dsRequestData.Tables(iCount).Rows
                                        row("RefillResponse_Id") = row("RefillRequest_Id")
                                        row("RefillRequest_Id") = DBNull.Value
                                    Next
                                Else
                                    ' dsRequestData.Tables(iCount).Columns("RefillRequest_Id").ColumnName = "RefillResponse_Id"

                                    'For jCount As Integer = 0 To dsRequestData.Tables(iCount).Columns.Count - 1
                                    '    If dsRequestData.Tables(iCount).Columns(jCount).ColumnName = "RefillRequest_Id" Then

                                    For Each row As DataRow In dsRequestData.Tables(iCount).Rows
                                        'row("RefillResponse_Id") = row("RefillRequest_Id")
                                        row("RefillRequest_Id") = DBNull.Value
                                    Next
                                End If
                            End If
                        End If

                        'If dsRequestData.Tables(iCount).Columns.Contains("DateTime") = True Then
                        '    If dsRequestData.Tables(iCount).Columns.Contains("Date") = True Then
                        '        For Each row As DataRow In dsRequestData.Tables(iCount).Rows
                        '            If Not IsDBNull(row("DateTime")) And IsDBNull(row("Date")) Then
                        '                row("Date") = row("DateTime")
                        '                row("DateTime") = DBNull.Value
                        '            End If

                        '        Next
                        '    End If
                        'End If
                        'If dsRequestData.Tables(iCount).Columns.Contains("Date") = True Then

                        '    Dim newColumn As DataColumn = New DataColumn("ShortDate", System.Type.GetType("System.String"))
                        '    dsRequestData.Tables(iCount).Columns.Add(newColumn)

                        '    For Each row As DataRow In dsRequestData.Tables(iCount).Rows
                        '        If Not IsDBNull(row("Date")) Then
                        '            row("ShortDate") = CDate(row("Date")).ToString("yyyy-MM-dd")
                        '        Else
                        '            row("ShortDate") = DBNull.Value
                        '        End If
                        '    Next
                        '    dsRequestData.Tables(iCount).Columns.Remove("Date")
                        '    dsRequestData.Tables(iCount).Columns("ShortDate").ColumnName = "Date"
                        'End If
                        'If dsRequestData.Tables(iCount).Columns.Contains("DateTime") = True Then
                        '    Dim newColumn As DataColumn = New DataColumn("ShortDate", System.Type.GetType("System.String"))
                        '    dsRequestData.Tables(iCount).Columns.Add(newColumn)

                        '    For Each row As DataRow In dsRequestData.Tables(iCount).Rows
                        '        If Not IsDBNull(row("DateTime")) Then
                        '            row("ShortDate") = ConvertToUTC(CDate(row("DateTime")))
                        '        Else
                        '            row("ShortDate") = DBNull.Value
                        '        End If
                        '    Next
                        '    dsRequestData.Tables(iCount).Columns.Remove("DateTime")
                        '    dsRequestData.Tables(iCount).Columns("ShortDate").ColumnName = "DateTime"
                        'End If
                    Next


                    Dim dtNewMedicationPrescribedRow As DataRow = Nothing
                    Dim foundNewMedicationPrescribedRow As Boolean = False
                    Dim dtNewMedicationDispensedRow As DataRow = Nothing
                    Dim foundNewMedicationDispensedRow As Boolean = False

                    For Each row As DataRow In dsRequestData.Tables("Refills").Rows
                        If Not IsDBNull(row("MedicationPrescribed_Id")) Then
                            If (row("MedicationPrescribed_Id") = 0) Then
                                dtNewMedicationPrescribedRow = row
                                foundNewMedicationPrescribedRow = True
                                If (foundNewMedicationDispensedRow) Then
                                    Exit For

                                End If
                            End If
                        End If
                        If Not IsDBNull(row("MedicationDispensed_Id")) Then
                            If (row("MedicationDispensed_Id") = 0) Then
                                dtNewMedicationDispensedRow = row
                                foundNewMedicationDispensedRow = True
                                If (foundNewMedicationPrescribedRow) Then
                                    Exit For

                                End If
                            End If
                        End If
                    Next

                    If (foundNewMedicationPrescribedRow = False) Then
                        dtNewMedicationPrescribedRow = dsRequestData.Tables("Refills").NewRow()
                        dtNewMedicationPrescribedRow("MedicationPrescribed_Id") = 0
                    End If

                    If (foundNewMedicationDispensedRow = False) Then
                        dtNewMedicationDispensedRow = dsRequestData.Tables("Refills").NewRow()
                        dtNewMedicationDispensedRow("MedicationDispensed_Id") = 0
                    End If

                    If oDrug.RefillsQualifier = "PRN" Then
                        dtNewMedicationPrescribedRow("Qualifier") = "PRN"
                        dtNewMedicationPrescribedRow("Value") = DBNull.Value
                        dtNewMedicationDispensedRow("Qualifier") = "PRN"
                        dtNewMedicationDispensedRow("Value") = DBNull.Value
                    ElseIf oDrug.RefillsQualifier = "R" Or oDrug.RefillsQualifier = "P" Then
                        dtNewMedicationPrescribedRow("Qualifier") = "A"
                        dtNewMedicationDispensedRow("Qualifier") = "A"
                        'dtNewMedicationPrescribedRow("Value") = objPrescription.DrugsCol.Item(0).RefillQuantity
                        dtNewMedicationDispensedRow("Value") = oDrug.RefillQuantity
                    End If

                    If (foundNewMedicationPrescribedRow = False) Then
                        dsRequestData.Tables("Refills").Rows.Add(dtNewMedicationPrescribedRow)
                    End If
                    If (foundNewMedicationDispensedRow = False) Then
                        dsRequestData.Tables("Refills").Rows.Add(dtNewMedicationDispensedRow)
                    End If
                    dsRequestData.AcceptChanges()
                    'If dsRequestData.Tables("Refills").Rows.Count > 1 Then
                    '    dsRequestData.Tables("Refills").Rows(0)("Qualifier") = "A"
                    '    dsRequestData.Tables("Refills").Rows(1)("Qualifier") = "A"
                    'Else

                    '    Dim dtNewRefillRow As DataRow = dsRequestData.Tables("Refills").NewRow()
                    '    dtNewRefillRow("Qualifier") = "A"
                    '    dtNewRefillRow("MedicationPrescribed_Id") = 0
                    '    dsRequestData.Tables("Refills").Rows.Add(dtNewRefillRow)
                    '    dtNewRefillRow = Nothing
                    '    dtNewRefillRow = dsRequestData.Tables("Refills").NewRow()
                    '    dtNewRefillRow("Qualifier") = "A"
                    '    dtNewRefillRow("MedicationDispensed_Id") = 0
                    '    dsRequestData.Tables("Refills").Rows.Add(dtNewRefillRow)



                    'End If

                    dsRequestData.Tables("RefillRequest").Rows.RemoveAt(0)

                End If
                Dim dtNewRow As DataRow = Nothing
                Dim dtNewWrittenDateRow As DataRow = Nothing
                Dim foundNewWrittenDateRow As Boolean = False
                Select Case eRefillStatus
                    Case RefillStatus.eApproved
                        If PONNumber <> 0 Then
                            dsRequestData.Tables("Header").Rows(0)("PrescriberOrderNumber") = PONNumber
                            PONNumber = 0
                        End If
                        For Each row As DataRow In dsRequestData.Tables("WrittenDate").Rows
                            If Not IsDBNull(row("MedicationPrescribed_Id")) Then
                                If (row("MedicationPrescribed_Id") = 0) Then
                                    dtNewWrittenDateRow = row
                                    foundNewWrittenDateRow = True
                                    If (foundNewWrittenDateRow) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If (foundNewWrittenDateRow = False) Then
                            dtNewWrittenDateRow = dsRequestData.Tables("WrittenDate").NewRow()
                            dtNewWrittenDateRow("MedicationPrescribed_Id") = 0
                        Else
                            If Not IsDBNull(dtNewWrittenDateRow("DateTime")) Then
                                dtNewWrittenDateRow("DateTime") = DBNull.Value
                            End If
                            dtNewWrittenDateRow("Date") = strdate
                        End If
                        If (foundNewWrittenDateRow = False) Then
                            dsRequestData.Tables("WrittenDate").Rows.Add(foundNewWrittenDateRow)
                        End If
                        dsRequestData.AcceptChanges()

                        dtNewRow = Nothing
                        dtNewWrittenDateRow = Nothing
                        foundNewWrittenDateRow = False
                        For Each row As DataRow In dsRequestData.Tables("WrittenDate").Rows
                            If Not IsDBNull(row("MedicationDispensed_Id")) Then
                                If (row("MedicationDispensed_Id") = 0) Then
                                    dtNewWrittenDateRow = row
                                    foundNewWrittenDateRow = True
                                    If (foundNewWrittenDateRow) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If (foundNewWrittenDateRow = False) Then
                            dtNewWrittenDateRow = dsRequestData.Tables("WrittenDate").NewRow()
                            dtNewWrittenDateRow("MedicationDispensed_Id") = 0
                        Else
                            If Not IsDBNull(dtNewWrittenDateRow("DateTime")) Then
                                dtNewWrittenDateRow("DateTime") = DBNull.Value
                            End If
                            dtNewWrittenDateRow("Date") = strdate
                        End If
                        If (foundNewWrittenDateRow = False) Then
                            dsRequestData.Tables("WrittenDate").Rows.Add(foundNewWrittenDateRow)
                        End If
                        dsRequestData.AcceptChanges()
                        dtNewRow = dsRequestData.Tables("Approved").NewRow()
                        If strnotes.Trim.Length > 0 Then
                            If strnotes.Trim.Length > 70 Then
                                RaiseEvent MessageInvalidated()
                                ValidationMessageBuilderforDrug.Append("The Response Note is too long for " & oDrug.DrugName & vbCrLf)
                                Return Nothing
                                Exit Function
                            Else
                                dtNewRow("Note") = strnotes
                            End If
                        End If
                        dtNewRow("Response_Id") = 0
                        dsRequestData.Tables("Approved").Rows.Add(dtNewRow)
                        oResponse.ApprovalStatus = True
                        eMessageType = SentMessageType.eApproved

                    Case RefillStatus.eApprovedWithChanges
                        If PONNumber <> 0 Then
                            dsRequestData.Tables("Header").Rows(0)("PrescriberOrderNumber") = PONNumber
                            PONNumber = 0
                        End If
                        For Each row As DataRow In dsRequestData.Tables("WrittenDate").Rows
                            If Not IsDBNull(row("MedicationPrescribed_Id")) Then
                                If (row("MedicationPrescribed_Id") = 0) Then
                                    dtNewWrittenDateRow = row
                                    foundNewWrittenDateRow = True
                                    If (foundNewWrittenDateRow) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If (foundNewWrittenDateRow = False) Then
                            dtNewWrittenDateRow = dsRequestData.Tables("WrittenDate").NewRow()
                            dtNewWrittenDateRow("MedicationPrescribed_Id") = 0
                        Else
                            If Not IsDBNull(dtNewWrittenDateRow("DateTime")) Then
                                dtNewWrittenDateRow("DateTime") = DBNull.Value
                            End If
                            dtNewWrittenDateRow("Date") = strdate
                        End If
                        If (foundNewWrittenDateRow = False) Then
                            dsRequestData.Tables("WrittenDate").Rows.Add(foundNewWrittenDateRow)
                        End If
                        dsRequestData.AcceptChanges()

                        dtNewRow = Nothing
                        dtNewWrittenDateRow = Nothing
                        foundNewWrittenDateRow = False

                        For Each row As DataRow In dsRequestData.Tables("WrittenDate").Rows
                            If Not IsDBNull(row("MedicationDispensed_Id")) Then
                                If (row("MedicationDispensed_Id") = 0) Then
                                    dtNewWrittenDateRow = row
                                    foundNewWrittenDateRow = True
                                    If (foundNewWrittenDateRow) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If (foundNewWrittenDateRow = False) Then
                            dtNewWrittenDateRow = dsRequestData.Tables("WrittenDate").NewRow()
                            dtNewWrittenDateRow("MedicationDispensed_Id") = 0
                        Else
                            If Not IsDBNull(dtNewWrittenDateRow("DateTime")) Then
                                dtNewWrittenDateRow("DateTime") = DBNull.Value
                            End If
                            dtNewWrittenDateRow("Date") = strdate
                        End If
                        If (foundNewWrittenDateRow = False) Then
                            dsRequestData.Tables("WrittenDate").Rows.Add(foundNewWrittenDateRow)
                        End If
                        dsRequestData.AcceptChanges()

                        dtNewRow = dsRequestData.Tables("ApprovedWithChanges").NewRow()
                        If strnotes.Trim.Length > 0 Then
                            If strnotes.Trim.Length > 70 Then
                                ValidationMessageBuilderforDrug.Append("The Response Note is too long for " & oDrug.DrugName & vbCrLf)
                                Return Nothing
                                Exit Function
                            Else
                                dtNewRow("Note") = strnotes
                            End If
                        End If
                        'If objPrescription.DrugsCol.Item(0).RefillsQualifier = "PRN" Then
                        '    dsRequestData.Tables("Refills").Rows(0)("Qualifier") = "PRN"
                        '    dsRequestData.Tables("Refills").Rows(0)("Value") = DBNull.Value
                        'ElseIf objPrescription.DrugsCol.Item(0).RefillsQualifier.Trim = "R" Then
                        '    dsRequestData.Tables("Refills").Rows(0)("Qualifier") = "A"
                        '    dsRequestData.Tables("Refills").Rows(0)("Value") = objPrescription.DrugsCol.Item(0).RefillQuantity
                        '    dsRequestData.Tables("Refills").Rows(1)("Qualifier") = "A"
                        '    dsRequestData.Tables("Refills").Rows(1)("Value") = objPrescription.DrugsCol.Item(0).RefillQuantity
                        'End If
                        'dsRequestData.Tables("Refills").Rows(1)("value") = objPrescription.DrugsCol.Item(0).MDRefillQuantity
                        dtNewRow("Response_Id") = 0
                        dsRequestData.Tables("ApprovedWithChanges").Rows.Add(dtNewRow)

                        eMessageType = SentMessageType.eApprovedWithChanges

                        dtNewRow = Nothing
                        Dim foundRow As Boolean = False

                        For Each row As DataRow In dsRequestData.Tables("Identification").Rows
                            If Not IsDBNull(row("Prescriber_Id")) Then
                                If (row("Prescriber_Id") = 0) Then
                                    dtNewRow = row
                                    foundRow = True
                                    Exit For
                                End If
                            End If
                        Next
                        If (foundRow = False) Then
                            dtNewRow = dsRequestData.Tables("Identification").NewRow()
                            dtNewRow("Presecriber_Id") = 0
                        End If
                        Dim MaxCount As Integer = 0
                        For Each column As DataColumn In dsRequestData.Tables("Identification").Columns
                            If (column.ColumnName.Length <= 3) Then
                                If Not IsDBNull(dtNewRow(column)) Then
                                    MaxCount = MaxCount + 1
                                End If
                            Else
                                If (column.ColumnName.Substring(column.ColumnName.Length - 3) <> "_Id") Then
                                    If Not IsDBNull(dtNewRow(column)) Then
                                        MaxCount = MaxCount + 1
                                    End If
                                End If
                            End If
                        Next
                        If (MaxCount < 3) Then
                            If (objPrescription.RxPrescriber.PrescriberDEA <> "") AndAlso IsDBNull(dtNewRow("DEANumber")) Then
                                dtNewRow("DEANumber") = objPrescription.RxPrescriber.PrescriberDEA
                                MaxCount = MaxCount + 1
                            End If
                        End If
                        If (MaxCount < 3) Then
                            If (objPrescription.RxPrescriber.PrescriberSSN <> "") AndAlso IsDBNull(dtNewRow("SocialSecurity")) Then
                                'dtNewRow("SocialSecurity") = objPrescription.RxPrescriber.PrescriberSSN
                                MaxCount = MaxCount + 1
                            End If
                        End If
                        If (MaxCount < 3) Then
                            If (objPrescription.RxPrescriber.PrescriberNPI <> "") AndAlso IsDBNull(dtNewRow("NPI")) Then
                                dtNewRow("NPI") = objPrescription.RxPrescriber.PrescriberNPI
                                MaxCount = MaxCount + 1
                            End If
                        End If

                        If (foundRow = False) Then
                            dsRequestData.Tables("Identification").Rows.Add(dtNewRow)
                        End If
                        dsRequestData.AcceptChanges()


                        dtNewRow = Nothing
                        foundRow = False

                        For Each row As DataRow In dsRequestData.Tables("DrugCoded").Rows
                            If Not IsDBNull(row("MedicationDispensed_Id")) Then
                                If (row("MedicationDispensed_Id") = 0) Then
                                    dtNewRow = row
                                    foundRow = True
                                    Exit For
                                End If
                            End If
                        Next
                        If (foundRow = False) Then
                            dtNewRow = dsRequestData.Tables("DrugCoded").NewRow()
                            dtNewRow("MedicationDispensed_Id") = 0
                        End If

                        'If objPrescription.DrugsCol.Item(0).ProdCode.Trim <> "" Then
                        '    dtNewRow("ProductCode") = objPrescription.DrugsCol.Item(0).ProdCode.Trim
                        'End If

                        If (foundRow = False) Then
                            dsRequestData.Tables("DrugCoded").Rows.Add(dtNewRow)
                        End If
                        dsRequestData.AcceptChanges()

                        'dtNewRow = Nothing
                        'foundRow = False

                        'For Each row As DataRow In dsRequestData.Tables("Name").Rows
                        '    If Not IsDBNull(row("Prescriber_Id")) Then
                        '        If (row("Prescriber_Id") = 0) Then
                        '            dtNewRow = row
                        '            foundRow = True
                        '            Exit For
                        '        End If
                        '    End If
                        'Next
                        'If (foundRow = False) Then
                        '    dtNewRow = dsRequestData.Tables("Name").NewRow()
                        '    dtNewRow("Presecriber_Id") = 0
                        'End If
                        'If objPrescription.RxPrescriber.PrescriberName.LastName.Trim.Length > 35 Then
                        '    dtNewRow("LastName") = objPrescription.RxPrescriber.PrescriberName.LastName.Trim.Substring(0, 35)
                        'Else
                        '    dtNewRow("LastName") = objPrescription.RxPrescriber.PrescriberName.LastName.Trim()
                        'End If
                        'If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Length > 0 Then
                        '    If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Length > 35 Then
                        '        dtNewRow("FirstName") = objPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Substring(0, 35)
                        '    Else
                        '        dtNewRow("FirstName") = objPrescription.RxPrescriber.PrescriberName.FirstName.Trim
                        '    End If
                        'Else
                        '    dtNewRow("FirstName") = DBNull.Value
                        'End If
                        'If objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim.Length > 0 Then
                        '    If objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim.Length > 35 Then
                        '        dtNewRow("MiddleName") = objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim.Substring(0, 35)
                        '    Else
                        '        dtNewRow("MiddleName") = objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim
                        '    End If
                        'Else
                        '    dtNewRow("MiddleName") = DBNull.Value
                        'End If

                        'If (foundRow = False) Then
                        '    dsRequestData.Tables("Identification").Rows.Add(dtNewRow)
                        'End If
                        'dsRequestData.AcceptChanges()

                        dtNewRow = Nothing
                        foundRow = False

                        For Each row As DataRow In dsRequestData.Tables("Address").Rows
                            If Not IsDBNull(row("Prescriber_Id")) Then
                                If (row("Prescriber_Id") = 0) Then
                                    dtNewRow = row
                                    foundRow = True
                                    Exit For
                                End If
                            End If
                        Next
                        If (foundRow = False) Then
                            dtNewRow = dsRequestData.Tables("Address").NewRow()
                            dtNewRow("Presecriber_Id") = 0
                        End If
                        If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim <> "" Then
                            If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Length > 35 Then
                                dtNewRow("AddressLine1") = objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Substring(0, 35)
                            Else
                                dtNewRow("AddressLine1") = objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim
                            End If
                        End If
                        If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim <> "" Then
                            If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Length > 35 Then
                                dtNewRow("PlaceLocationQualifier") = "AD2"
                                dtNewRow("AddressLine2") = objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Substring(0, 35)
                            Else
                                dtNewRow("PlaceLocationQualifier") = "AD2"
                                dtNewRow("AddressLine2") = objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim
                            End If
                        Else
                            dtNewRow("PlaceLocationQualifier") = DBNull.Value
                            dtNewRow("AddressLine2") = DBNull.Value
                        End If

                        If objPrescription.RxPrescriber.PrescriberAddress.City.Trim <> "" Then
                            If objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Length > 35 Then
                                dtNewRow("City") = objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Substring(0, 35)
                            Else
                                dtNewRow("City") = objPrescription.RxPrescriber.PrescriberAddress.City.Trim
                            End If
                        End If

                        If objPrescription.RxPrescriber.PrescriberAddress.State.Trim <> "" Then
                            If objPrescription.RxPrescriber.PrescriberAddress.State.Trim.Length > 9 Then
                                dtNewRow("State") = objPrescription.RxPrescriber.PrescriberAddress.State.Trim.Substring(0, 9)
                            Else
                                dtNewRow("State") = objPrescription.RxPrescriber.PrescriberAddress.State
                            End If
                        End If

                        If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim <> "" Then
                            If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim.Length > 11 Then
                                dtNewRow("ZipCode") = objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim.Substring(0, 11)
                            Else
                                dtNewRow("ZipCode") = objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim
                            End If
                        End If

                        If (foundRow = False) Then
                            dsRequestData.Tables("Address").Rows.Add(dtNewRow)
                        End If
                        dsRequestData.AcceptChanges()

                        dtNewRow = Nothing
                        foundRow = False
                        Dim comID As Integer = Nothing
                        For Each row As DataRow In dsRequestData.Tables("CommunicationNumbers").Rows
                            If Not IsDBNull(row("Prescriber_Id")) Then
                                If (row("Prescriber_Id") = 0) Then
                                    dtNewRow = row
                                    foundRow = True
                                    Exit For
                                End If
                            End If
                        Next
                        If (foundRow = False) Then
                            dtNewRow = dsRequestData.Tables("CommunicationNumbers").NewRow()
                            dtNewRow("Prescriber_Id") = 0
                            If dsRequestData.Tables("CommunicationNumbers").Rows.Count = 0 Then
                                comID = 0
                            Else
                                comID = dsRequestData.Tables("CommunicationNumbers").Rows.Count
                            End If
                            dtNewRow("CommunicationNumbers_ID") = comID
                        Else
                            comID = dtNewRow("CommunicationNumbers_ID")
                        End If
                        If (foundRow = False) Then
                            dsRequestData.Tables("CommunicationNumbers").Rows.Add(dtNewRow)
                        End If
                        dsRequestData.AcceptChanges()
                        dtNewRow = Nothing
                        foundRow = False


                        Dim dtPhoneNewRow As DataRow = Nothing
                        Dim dtFaxNewRow As DataRow = Nothing
                        Dim foundPhoneRow As Boolean = False
                        Dim foundFaxRow As Boolean = False

                        Dim myCount As Integer = dsRequestData.Tables("Communication").Rows.Count

                        For iCount As Integer = myCount - 1 To 0 Step -1
                            Dim row As DataRow = dsRequestData.Tables("Communication").Rows(iCount)
                            If Not IsDBNull(row("CommunicationNumbers_ID")) Then
                                If (row("CommunicationNumbers_ID") = comID) Then
                                    If (row("Qualifier") = "TE") Then
                                        If foundPhoneRow Then
                                            dsRequestData.Tables("Communication").Rows.Remove(row)
                                        Else
                                            dtPhoneNewRow = row
                                            foundPhoneRow = True
                                        End If

                                    ElseIf row("Qualifier") = "FX" Then
                                        If foundFaxRow Then
                                            dsRequestData.Tables("Communication").Rows.Remove(row)
                                        Else
                                            dtFaxNewRow = row
                                            foundFaxRow = True
                                        End If
                                    Else
                                        dsRequestData.Tables("Communication").Rows.Remove(row)
                                    End If

                                End If
                            End If
                        Next
                        ''Fax number not present
                        If foundFaxRow = False Then
                            dtFaxNewRow = dsRequestData.Tables("Communication").NewRow()
                            dtFaxNewRow("CommunicationNumbers_ID") = comID
                            dtFaxNewRow("Qualifier") = "FX"
                        End If

                        If objPrescription.RxPrescriber.PrescriberPhone.Fax.Trim <> "" Then
                            dtFaxNewRow("Number") = objPrescription.RxPrescriber.PrescriberPhone.Fax.Trim
                        End If
                        If (foundFaxRow = False) Then
                            dsRequestData.Tables("Communication").Rows.Add(dtFaxNewRow)
                        End If
                        ''Phone number not present
                        If foundPhoneRow = False Then
                            dtPhoneNewRow = dsRequestData.Tables("Communication").NewRow()
                            dtPhoneNewRow("CommunicationNumbers_ID") = comID
                            dtPhoneNewRow("Qualifier") = "TE"
                        End If

                        If objPrescription.RxPrescriber.PrescriberPhone.Phone.Trim <> "" Then
                            dtPhoneNewRow("Number") = objPrescription.RxPrescriber.PrescriberPhone.Phone.Trim
                        End If
                        If (foundPhoneRow = False) Then
                            dsRequestData.Tables("Communication").Rows.Add(dtPhoneNewRow)
                        End If

                        dsRequestData.AcceptChanges()



                        ''Supervisor
                        If objPrescription.RxSupervisorProviderID <> 0 Then
                            dtNewRow = Nothing
                            foundRow = False
                            For Each row As DataRow In dsRequestData.Tables("Supervisor").Rows
                                If Not IsDBNull(row("Supervisor_Id")) Then
                                    If (row("Supervisor_Id") = 0) Then
                                        dtNewRow = row
                                        foundRow = True
                                        Exit For
                                    End If
                                End If
                            Next


                            If (foundRow = False) Then
                                dtNewRow = dsRequestData.Tables("Supervisor").NewRow()
                                dtNewRow("Supervisor_Id") = 0
                                dtNewRow("RefillResponse_Id") = 0
                            End If

                            dtNewRow("Specialty") = DBNull.Value
                            dtNewRow("ClinicName") = DBNull.Value

                            If (foundRow = False) Then
                                dsRequestData.Tables("Supervisor").Rows.Add(dtNewRow)
                            End If
                            dsRequestData.AcceptChanges()

                            dtNewRow = Nothing
                            foundRow = False

                            For Each row As DataRow In dsRequestData.Tables("Identification").Rows
                                If Not IsDBNull(row("Supervisor_Id")) Then
                                    If (row("Supervisor_Id") = 0) Then
                                        dtNewRow = row
                                        foundRow = True
                                        Exit For
                                    End If
                                End If
                            Next

                            If (foundRow = False) Then
                                dtNewRow = dsRequestData.Tables("Identification").NewRow()
                                dtNewRow("Supervisor_Id") = 0
                            End If



                            For Each column As DataColumn In dsRequestData.Tables("Identification").Columns
                                If (column.ColumnName.Length <= 3) Then
                                    If Not IsDBNull(dtNewRow(column)) Then
                                        dtNewRow(column) = DBNull.Value
                                    End If
                                Else
                                    If (column.ColumnName.Substring(column.ColumnName.Length - 3) <> "_Id") Then
                                        If Not IsDBNull(dtNewRow(column)) Then
                                            dtNewRow(column) = DBNull.Value
                                        End If
                                    End If
                                End If
                            Next

                            If (objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI <> "") AndAlso IsDBNull(dtNewRow("NPI")) Then
                                dtNewRow("NPI") = objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI
                            End If

                            If (objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA <> "") AndAlso IsDBNull(dtNewRow("DEANumber")) Then
                                dtNewRow("DEANumber") = objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA
                            End If

                            If (objPrescription.RxSupervisorPrescriber.SupervisorProviderSSN <> "") AndAlso IsDBNull(dtNewRow("SocialSecurity")) Then
                                'dtNewRow("SocialSecurity") = objPrescription.RxSupervisorPrescriber.SupervisorProviderSSN
                            End If


                            If (foundRow = False) Then
                                dsRequestData.Tables("Identification").Rows.Add(dtNewRow)
                            End If
                            dsRequestData.AcceptChanges()


                            dtNewRow = Nothing
                            foundRow = False

                            For Each row As DataRow In dsRequestData.Tables("Name").Rows
                                If Not IsDBNull(row("Supervisor_Id")) Then
                                    If (row("Supervisor_Id") = 0) Then
                                        dtNewRow = row
                                        foundRow = True
                                        Exit For
                                    End If
                                End If
                            Next
                            If (foundRow = False) Then
                                dtNewRow = dsRequestData.Tables("Name").NewRow()
                                dtNewRow("Supervisor_Id") = 0
                            End If
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim.Length > 35 Then
                                dtNewRow("LastName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim.Substring(0, 35)
                            Else
                                dtNewRow("LastName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim()
                            End If
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Length > 0 Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Length > 35 Then
                                    dtNewRow("FirstName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Substring(0, 35)
                                Else
                                    dtNewRow("FirstName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim
                                End If
                            Else
                                dtNewRow("FirstName") = DBNull.Value
                            End If
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Length > 0 Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Length > 35 Then
                                    dtNewRow("MiddleName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Substring(0, 35)
                                Else
                                    dtNewRow("MiddleName") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim
                                End If
                            Else
                                dtNewRow("MiddleName") = DBNull.Value
                            End If

                            dtNewRow("Suffix") = DBNull.Value
                            dtNewRow("Prefix") = DBNull.Value
                            If (foundRow = False) Then
                                dsRequestData.Tables("Name").Rows.Add(dtNewRow)
                            End If
                            dsRequestData.AcceptChanges()

                            dtNewRow = Nothing
                            foundRow = False

                            For Each row As DataRow In dsRequestData.Tables("Address").Rows
                                If Not IsDBNull(row("Supervisor_Id")) Then
                                    If (row("Supervisor_Id") = 0) Then
                                        dtNewRow = row
                                        foundRow = True
                                        Exit For
                                    End If
                                End If
                            Next
                            If (foundRow = False) Then
                                dtNewRow = dsRequestData.Tables("Address").NewRow()
                                dtNewRow("Supervisor_Id") = 0
                            End If
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim <> "" Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim.Length > 35 Then
                                    dtNewRow("AddressLine1") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim.Substring(0, 35)
                                Else
                                    dtNewRow("AddressLine1") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim
                                End If
                            End If
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim <> "" Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim.Length > 35 Then
                                    dtNewRow("PlaceLocationQualifier") = "AD2"
                                    dtNewRow("AddressLine2") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim.Substring(0, 35)
                                Else
                                    dtNewRow("PlaceLocationQualifier") = "AD2"
                                    dtNewRow("AddressLine2") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim
                                End If
                            Else
                                dtNewRow("PlaceLocationQualifier") = DBNull.Value
                                dtNewRow("AddressLine2") = DBNull.Value
                            End If

                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim <> "" Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim.Length > 35 Then
                                    dtNewRow("City") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim.Substring(0, 35)
                                Else
                                    dtNewRow("City") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim
                                End If
                            End If

                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Trim <> "" Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Trim.Length > 9 Then
                                    dtNewRow("State") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Trim.Substring(0, 9)
                                Else
                                    dtNewRow("State") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State
                                End If
                            End If

                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim <> "" Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim.Length > 11 Then
                                    dtNewRow("ZipCode") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim.Substring(0, 11)
                                Else
                                    dtNewRow("ZipCode") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim
                                End If
                            End If

                            If (foundRow = False) Then
                                dsRequestData.Tables("Address").Rows.Add(dtNewRow)
                            End If
                            dsRequestData.AcceptChanges()


                            dtNewRow = Nothing
                            foundRow = False
                            comID = Nothing
                            For Each row As DataRow In dsRequestData.Tables("CommunicationNumbers").Rows
                                If Not IsDBNull(row("Supervisor_Id")) Then
                                    If (row("Supervisor_Id") = 0) Then
                                        dtNewRow = row
                                        foundRow = True
                                        Exit For
                                    End If
                                End If
                            Next
                            If (foundRow = False) Then
                                dtNewRow = dsRequestData.Tables("CommunicationNumbers").NewRow()
                                dtNewRow("Supervisor_Id") = 0
                                If dsRequestData.Tables("CommunicationNumbers").Rows.Count = 0 Then
                                    comID = 0
                                Else
                                    comID = dsRequestData.Tables("CommunicationNumbers").Rows.Count
                                End If
                                dtNewRow("CommunicationNumbers_ID") = comID
                            Else
                                comID = dtNewRow("CommunicationNumbers_ID")
                            End If
                            If (foundRow = False) Then
                                dsRequestData.Tables("CommunicationNumbers").Rows.Add(dtNewRow)
                            End If
                            dsRequestData.AcceptChanges()
                            dtNewRow = Nothing
                            foundRow = False


                            dtPhoneNewRow = Nothing
                            dtFaxNewRow = Nothing
                            foundPhoneRow = False
                            foundFaxRow = False

                            myCount = dsRequestData.Tables("Communication").Rows.Count

                            For iCount As Integer = myCount - 1 To 0 Step -1
                                Dim row As DataRow = dsRequestData.Tables("Communication").Rows(iCount)
                                If Not IsDBNull(row("CommunicationNumbers_ID")) Then
                                    If (row("CommunicationNumbers_ID") = comID) Then
                                        If (row("Qualifier") = "TE") Then
                                            If foundPhoneRow Then
                                                dsRequestData.Tables("Communication").Rows.Remove(row)
                                            Else
                                                dtPhoneNewRow = row
                                                foundPhoneRow = True
                                            End If
                                        ElseIf row("Qualifier") = "FX" Then
                                            If foundFaxRow Then
                                                dsRequestData.Tables("Communication").Rows.Remove(row)
                                            Else
                                                dtFaxNewRow = row
                                                foundFaxRow = True
                                            End If
                                        Else
                                            dsRequestData.Tables("Communication").Rows.Remove(row)
                                        End If

                                    End If
                                End If
                            Next

                            If foundPhoneRow = False Then
                                dtPhoneNewRow = dsRequestData.Tables("Communication").NewRow()
                                dtPhoneNewRow("CommunicationNumbers_ID") = comID
                                dtPhoneNewRow("Qualifier") = "TE"
                            End If

                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim <> "" Then
                                dtPhoneNewRow("Number") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim
                            End If
                            If (foundPhoneRow = False) Then
                                dsRequestData.Tables("Communication").Rows.Add(dtPhoneNewRow)
                            End If

                            If foundFaxRow = False Then
                                dtFaxNewRow = dsRequestData.Tables("Communication").NewRow()
                                dtFaxNewRow("CommunicationNumbers_ID") = comID
                                dtFaxNewRow("Qualifier") = "FX"
                            End If

                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax.Trim <> "" Then
                                dtFaxNewRow("Number") = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax.Trim
                            End If
                            If (foundFaxRow = False) Then
                                dsRequestData.Tables("Communication").Rows.Add(dtFaxNewRow)
                            End If
                            dsRequestData.AcceptChanges()
                            'dtNewRow = dsViewDataset.Tables("CommunicationNumbers").NewRow()
                            'dtNewRow("CommunicationNumbers_ID") = dsViewDataset.Tables("CommunicationNumbers").Rows.Count + 1
                            'dtNewRow("Prescriber_ID") = 0
                            'dsViewDataset.Tables("CommunicationNumbers").Rows.Add(dtNewRow)
                            'dtNewRow = Nothing

                            'dtNewRow = dsViewDataset.Tables("Communication").NewRow()
                            'dtNewRow("CommunicationNumbers_ID") = dsViewDataset.Tables("CommunicationNumbers").Rows.Count + 1
                            'dtNewRow("Number") = strPrescriberPhone
                            'dtNewRow("Qualifier") = "TE"
                            'dsViewDataset.Tables("Communication").Rows.Add(dtNewRow)
                            'dtNewRow = Nothing

                            'dtNewRow = dsViewDataset.Tables("Communication").NewRow()
                            'dtNewRow("CommunicationNumbers_ID") = dsViewDataset.Tables("CommunicationNumbers").Rows.Count + 1
                            'dtNewRow("Number") = strPrescriberFax
                            'dtNewRow("Qualifier") = "FX"
                            'dsViewDataset.Tables("Communication").Rows.Add(dtNewRow)
                            'dtNewRow = Nothing






                        End If

                        oResponse.ApprovalStatus = True
                        eMessageType = SentMessageType.eApprovedWithChanges
                    Case RefillStatus.eDenied
                        If dsRequestData.Tables("Refills").Rows(0)("Qualifier") <> "PRN" Then
                            dsRequestData.Tables("Refills").Rows(0)("Value") = 0
                        End If
                        If dsRequestData.Tables("Refills").Rows(1)("Qualifier") <> "PRN" Then
                            dsRequestData.Tables("Refills").Rows(1)("Value") = 0
                        End If
                        dtNewRow = dsRequestData.Tables("Denied").NewRow()
                        If strnotes.Trim.Length > 0 Then
                            If strnotes.Trim.Length > 70 Then
                                RaiseEvent MessageInvalidated()
                                ValidationMessageBuilderforDrug.Append("The Response Note is too long for " & oDrug.DrugName & vbCrLf)
                                Return Nothing
                                Exit Function

                            Else
                                dtNewRow("DenialReason") = strnotes
                            End If
                        End If
                        'dtNewRow = dsRequestData.Tables("Denied").NewRow()
                        If Not IsNothing(denialreasoncode) Then
                            If denialreasoncode.Trim.Length > 0 Then
                                dtNewRow("DenialReasonCode") = denialreasoncode
                            Else
                                dtNewRow("DenialReasonCode") = "AG"
                            End If
                        Else
                            dtNewRow("DenialReasonCode") = "AG"
                        End If

                        dtNewRow("Response_Id") = 0
                        dsRequestData.Tables("Denied").Rows.Add(dtNewRow)
                        dsRequestData.AcceptChanges()
                        oResponse.ApprovalStatus = False
                        oResponse.Notes = strnotes
                        eMessageType = SentMessageType.eDenied
                    Case RefillStatus.eDeniedWithNewRxToFollow
                        If dsRequestData.Tables("Refills").Rows(0)("Qualifier") <> "PRN" Then
                            dsRequestData.Tables("Refills").Rows(0)("Value") = 0
                        End If
                        If dsRequestData.Tables("Refills").Rows(1)("Qualifier") <> "PRN" Then
                            dsRequestData.Tables("Refills").Rows(1)("Value") = 0
                        End If
                        dtNewRow = dsRequestData.Tables("DeniedNewPrescriptionToFollow").NewRow()

                        ''do not send note "Approved drug is Schedule 3 to 5, Rx will be faxed to pharmacy" when the drug is narcotic in DTNF case.
                        'If strnotes.Trim.Length > 0 Then
                        '    If strnotes.Trim.Length > 70 Then
                        '        RaiseEvent MessageInvalidated()
                        '        ValidationMessageBuilderforDrug.Append("The Response Note is too long for " & oDrug.DrugName & vbCrLf)
                        '        Return Nothing
                        '        Exit Function

                        '    Else
                        '        dtNewRow("DenialReason") = strnotes
                        '    End If
                        'End If
                        dtNewRow("DenialReasonCode") = "AG"
                        If strnotes <> "" Then
                            dtNewRow("DenialReason") = strnotes
                        End If
                        dtNewRow("ReferenceNumber") = oDrug.RxReferenceNumber
                        dtNewRow("Response_Id") = 0
                        dsRequestData.Tables("DeniedNewPrescriptionToFollow").Rows.Add(dtNewRow)
                        dsRequestData.AcceptChanges()
                        oResponse.ApprovalStatus = False
                        oResponse.Notes = strnotes
                        eMessageType = SentMessageType.eDeniedWithNewRxToFollow
                End Select

                dsRequestData.Tables("Message").WriteXml(strfilepath)

            Catch ex As Exception
                MessageBox.Show(ex.ToString(), "gloEMR")
            End Try


            '''''Change the order of elements using Xsl
            'Dim _strfileName2 As String = ""
            Dim myXslTransform As Xsl.XslCompiledTransform = Nothing
            Dim _strfileName As String = gloSettings.FolderSettings.AppTempFolderPath & "3" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") & ".xml"
            myXslTransform = New Xml.Xsl.XslCompiledTransform()
            myXslTransform.Load(System.Windows.Forms.Application.StartupPath & "\namespaceremoval.xsl")
            myXslTransform.Transform(strfilepath, _strfileName)
            'SLR: Free myXslTransofrm
            If Not myXslTransform Is Nothing Then
                myXslTransform = Nothing
            End If
            Dim _strfileName1 As String = gloSettings.FolderSettings.AppTempFolderPath & "4" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") & ".xml"
            myXslTransform = New Xml.Xsl.XslCompiledTransform()
            myXslTransform.Load(System.Windows.Forms.Application.StartupPath & "\MessageXSLTFile.xsl")
            myXslTransform.Transform(_strfileName, _strfileName1)
            'SLR: Free myXslTransofrm
            If Not myXslTransform Is Nothing Then
                myXslTransform = Nothing
            End If
            _strfileName2 = gloSettings.FolderSettings.AppTempFolderPath & "5" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") & ".xml"
            myXslTransform = New Xml.Xsl.XslCompiledTransform()
            myXslTransform.Load(System.Windows.Forms.Application.StartupPath & "\namespaceadd.xsl")
            myXslTransform.Transform(_strfileName1, _strfileName2)
            'SLR: Free myXslTransofrm
            If Not myXslTransform Is Nothing Then
                myXslTransform = Nothing
            End If

            Dim NoOfAttempt As Integer = 1
            Dim result As Byte() = Nothing

            While (NoOfAttempt <= 5)
                result = gloSurescriptGeneral.PostSurescriptMessage(_strfileName2)

                If Not IsNothing(result) Then
                    Exit While
                Else
                    NoOfAttempt = NoOfAttempt + 1
                End If

            End While

            If Not IsNothing(result) Then
                ReadStatusMessageRevised(result, eMessageType, oDrug.MDDrugName, "Refill")
                oDrug.MessageName = "RefillResponse"

                'Once the RefillResponse is generated now generate a FreeStanding verify message
                'to inform surescript that RefillRequest has been processed successfully
                Dim oSurescriptDBLayer As New gloSureScriptDBLayer
                oSurescriptDBLayer.InsertintoMessageTransaction(CType(oDrug, SureScriptMessage))
                If MessageName = "Status" Then
                    oResponse.StatusMessageType = StatusMessageType
                    oResponse.ProviderId = Convert.ToInt64(objPrescription.RxPrescriber.PrescriberID)
                    If RefReqPatientID.ToString <> "" Then ''''''''refreqpatientid is used for denied ref req patient report
                        oResponse.RefReqPatientId = Convert.ToInt64(RefReqPatientID.ToString)
                    Else
                        oResponse.RefReqPatientId = 0
                    End If
                    oSurescriptDBLayer.InsertResponseTransaction(oResponse, False)
                ElseIf MessageName = "Error" Then
                    oResponse.StatusMessageType = StatusMessageType
                    oResponse.ProviderId = Convert.ToInt64(objPrescription.RxPrescriber.PrescriberID)
                    If RefReqPatientID.ToString <> "" Then ''''''''refreqpatientid is used for denied ref req patient report
                        oResponse.RefReqPatientId = Convert.ToInt64(RefReqPatientID.ToString)
                    Else
                        oResponse.RefReqPatientId = 0
                    End If

                    oSurescriptDBLayer.InsertResponseTransaction(oResponse, False)
                End If
                If Not IsNothing(oSurescriptDBLayer) Then
                    oSurescriptDBLayer.Dispose()
                    oSurescriptDBLayer = Nothing
                End If
                Return True
            Else
                RaiseEvent MessageInvalidated()
                Return False
            End If

            gloSurescriptGeneral.UpdateLog("Exiting gloSureScriptsInterface.GenerateRefillResponse")
            Return False

        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            gloSurescriptGeneral.UpdateLog("Error Occurred processing refill response: " & ex.Message)
            Return False
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            gloSurescriptGeneral.UpdateLog("Error Occurred processing refill response: " & ex.Message)
            Return False
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
            Return False
        Finally
            If Not IsNothing(oResponse) Then
                oResponse.Dispose()
                oResponse = Nothing
            End If
            If Not IsNothing(dsRequestData) Then
                dsRequestData.Dispose()
                dsRequestData = Nothing
            End If
        End Try

        'Else
        'RaiseEvent MessageInvalidated()
        'End If
        ''ValidationMessageforDrug = ValidationMessageBuilderforDrug.ToString.Trim
        'Return Nothing
    End Function

    Public Function ViewXML(ByVal dsFiledata As DataSet) As String
        Dim _strfileName1 As String = ""
        Try
            Dim sPrescribedpotencyCode As String = ""
            Dim sDispensedpotencyCode As String = ""

            sPrescribedpotencyCode = dsFiledata.Tables("Quantity").Rows(0)("PotencyUnitCode")
            sDispensedpotencyCode = dsFiledata.Tables("Quantity").Rows(1)("PotencyUnitCode")

            Dim objSureScriptDBLayer As New gloSureScriptDBLayer

            dsFiledata.Tables("Quantity").Rows(0)("PotencyUnitCode") = objSureScriptDBLayer.GetDescriptionFromPotency(sPrescribedpotencyCode)
            dsFiledata.Tables("Quantity").Rows(1)("PotencyUnitCode") = objSureScriptDBLayer.GetDescriptionFromPotency(sDispensedpotencyCode)

            If Not IsNothing(objSureScriptDBLayer) Then
                objSureScriptDBLayer.Dispose()
                objSureScriptDBLayer = Nothing
            End If
            Dim strfilepath As String = GenerateFileName(MessageType.eRefillResponse)

            dsFiledata.Tables("Message").WriteXml(strfilepath)
            Dim strContent As String = ""
            strContent = File.ReadAllText(strfilepath)




            'Dim myXslTransform As Xsl.XslCompiledTransform = Nothing
            'Dim _strfileName As String = gloSettings.FolderSettings.AppTempFolderPath & "0" & DateTime.Now.ToString("yyyyMMddhhmmssffff") & ".xml"
            'myXslTransform = New Xml.Xsl.XslCompiledTransform()
            'myXslTransform.Load(System.Windows.Forms.Application.StartupPath & "\namespaceremoval.xsl")
            'myXslTransform.Transform(strfilepath, _strfileName)
            ''SLR: Free myXslTransofrm
            'If Not myXslTransform Is Nothing Then
            '    myXslTransform = Nothing
            'End If

            _strfileName1 = gloSettings.FolderSettings.AppTempFolderPath & "9" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") & ".html"
            'Dim myXslTransform2 As New Xml.Xsl.XslTransform
            'myXslTransform2.Load(System.Windows.Forms.Application.StartupPath & "\Max_Response.xsl")
            'myXslTransform2.Transform(_strfileName, _strfileName1)
            ''SLR: Free myXslTransofrm
            'If Not myXslTransform2 Is Nothing Then
            '    myXslTransform2 = Nothing
            'End If

            Dim _firstTransformation As String = ""
            Dim _secondTransforamtion As String = ""

            _firstTransformation = Transform(strContent, System.Windows.Forms.Application.StartupPath & "\namespaceremoval.xsl")
            _secondTransforamtion = Transform(_firstTransformation, System.Windows.Forms.Application.StartupPath & "\Max_Response.xsl")
            _strfileName1 = gloSettings.FolderSettings.AppTempFolderPath & "10" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") & ".html"
            File.WriteAllText(_strfileName1, _secondTransforamtion)
            Return _strfileName1

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR")
            Return _strfileName1
        End Try
    End Function

    Public Sub AddPotencyDescription(ByVal sFilename As String, ByVal Potency As String, ByVal MDPotency As String)
        Dim XmlDoc As XmlDocument = Nothing
        Dim ODblayer As gloSureScriptDBLayer = Nothing
        Try

            'loading our xmal
            XmlDoc = New XmlDocument
            ODblayer = New gloSureScriptDBLayer
            XmlDoc.Load(sFilename)
            'creating tages
            Dim node As XmlNode
            Dim Prescribed As XmlNode
            Dim dispense As XmlNode
            node = XmlDoc.CreateNode(XmlNodeType.Element, "PotencyDescription", Nothing)
            Prescribed = XmlDoc.CreateElement("MP")
            Prescribed.InnerText = ODblayer.GetDescriptionFromPotency(Potency)

            dispense = XmlDoc.CreateElement("MD")
            dispense.InnerText = ODblayer.GetDescriptionFromPotency(MDPotency)

            node.AppendChild(Prescribed)
            node.AppendChild(dispense)

            XmlDoc.DocumentElement.AppendChild(node)

            XmlDoc.Save(sFilename)

        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(ODblayer) Then
                ODblayer.Dispose()
                ODblayer = Nothing
            End If
            If Not IsNothing(XmlDoc) Then
                XmlDoc = Nothing
            End If
        End Try
    End Sub

    Public Function ViewXML(ByVal sFilename As String, ByVal Potency As String, ByVal MDPotency As String) As String
        Dim _strfileName1 As String = ""
        Try
            If Potency <> "" Or MDPotency <> "" Then
                AddPotencyDescription(sFilename, Potency, MDPotency)
            End If
            'Dim myXslTransform As Xml.Xsl.XslTransform = Nothing
            'Dim _strfileName As String = gloSettings.FolderSettings.AppTempFolderPath & "0" & DateTime.Now.ToString("yyyyMMddhhmmssffff") & ".xml"
            'myXslTransform = New Xml.Xsl.XslTransform
            'myXslTransform.Load(System.Windows.Forms.Application.StartupPath & "\namespaceremoval.xsl")
            'myXslTransform.Transform(sFilename, _strfileName)
            ''SLR: Free myXslTransofrm
            'If Not myXslTransform Is Nothing Then
            '    myXslTransform = Nothing
            'End If

            '_strfileName1 = gloSettings.FolderSettings.AppTempFolderPath & "0" & DateTime.Now.ToString("yyyyMMddhhmmssffff") & ".html"
            'Dim myXslTransform2 As New Xml.Xsl.XslTransform
            'myXslTransform2.Load(System.Windows.Forms.Application.StartupPath & "\Max_SummaryResponse.xsl")
            'myXslTransform2.Transform(_strfileName, _strfileName1)
            ''SLR: Free myXslTransofrm
            'If Not myXslTransform2 Is Nothing Then
            '    myXslTransform2 = Nothing
            'End If
            Dim strContent As String = ""
            Dim _firstTransformation As String = ""
            Dim _secondTransforamtion As String = ""
            strContent = File.ReadAllText(sFilename)
            _firstTransformation = Transform(strContent, System.Windows.Forms.Application.StartupPath & "\namespaceremoval.xsl")
            _secondTransforamtion = Transform(_firstTransformation, System.Windows.Forms.Application.StartupPath & "\Max_SummaryResponse.xsl")
            _strfileName1 = gloSettings.FolderSettings.AppTempFolderPath & "11" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") & ".html"
            File.WriteAllText(_strfileName1, _secondTransforamtion)
            Return _strfileName1

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR")
            Return _strfileName1
        End Try
    End Function
    Public Function Transform(ByVal xmlString As String, ByVal xslFile As String) As String
        Try
            Dim output As String = [String].Empty
            Using srt As New StringReader(xmlString)
                ' xslInput is a string that contains xsl
                Using sri As New StringReader(xmlString)
                    ' xmlInput is a string that contains xml
                    Dim setting As New XmlReaderSettings
                    setting.DtdProcessing = DtdProcessing.Ignore
                    Using xrt As XmlReader = XmlReader.Create(xslFile, setting)
                        Using xri As XmlReader = XmlReader.Create(sri, setting)
                            Dim xslt As New XslCompiledTransform()
                            xslt.Load(xrt)
                            Using sw As New StringWriter()
                                Using xwo As XmlWriter = XmlWriter.Create(sw, xslt.OutputSettings)
                                    ' use OutputSettings of xsl, so it can be output as HTML
                                    xslt.Transform(xri, xwo)
                                    output = sw.ToString()
                                    setting = Nothing
                                    Return output
                                End Using
                            End Using
                        End Using
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub UpdateBlankRows(ByVal TableName As String, ByRef dsRequestData As DataSet)
        If dsRequestData.Tables(TableName).Rows.Count <= 0 Then
            Dim dtRequestNewRow As DataRow = Nothing
            dtRequestNewRow = dsRequestData.Tables(TableName).NewRow()
            dtRequestNewRow.Item(TableName & "_Id") = 0
            dtRequestNewRow.Item("RefillRequest_Id") = 0
            dsRequestData.Tables(TableName).Rows.Add(dtRequestNewRow)
        End If

    End Sub

    Private Sub recursivePolpulate(ByVal table As DataTable, ByRef dsMessage2 As DataSet, ByRef dsResponse As DataSet)
        Try
            If dsResponse.Tables(table.TableName).Rows.Count = 0 Then
                For Each relation As DataRelation In table.ParentRelations
                    recursivePolpulate(relation.ParentTable, dsMessage2, dsResponse)
                Next

                For Each row As DataRow In table.Rows
                    Dim newRow As DataRow = dsResponse.Tables(table.TableName).NewRow()
                    For Each column As DataColumn In table.Columns
                        newRow(column.ColumnName) = row(column.ColumnName)
                        If (column.ColumnName = "Date") Then
                            If IsDBNull(newRow(column.ColumnName)) Then
                                newRow(column.ColumnName) = row("DateTime")
                            End If
                        End If
                        If (column.ColumnName = "DateTime") Then
                            If IsDBNull(newRow(column.ColumnName)) Then
                                newRow(column.ColumnName) = row("Date")
                            End If
                        End If
                    Next
                    If table.TableName = "Identification" Then
                        For Each column As DataColumn In dsResponse.Tables(table.TableName).Columns
                            Dim idColumn As Boolean = False
                            If (column.ColumnName.Length > 3) Then
                                If (column.ColumnName.Substring(column.ColumnName.Length - 3) = "_Id") Then
                                    idColumn = True
                                End If
                            End If
                            If IsDBNull(newRow(column.ColumnName)) AndAlso Not (idColumn) Then
                                Dim myType As System.Type = newRow(column.ColumnName).GetType()
                                If (myType.ToString() = "System.String") Then
                                    newRow(column.ColumnName) = ""
                                Else
                                    newRow(column.ColumnName) = 0
                                End If
                            End If
                        Next
                    End If

                    'If table.TableName = "Identification" Then
                    '    For i As Integer = 0 To newRow.ItemArray.Length
                    '        If IsDBNull(newRow.Item(i)) Then
                    '            If i = 25 Or i = 26 Or i = 27 Or i = 28 Or i = 29 Then
                    '                newRow.Item(i) = 0
                    '            Else
                    '                newRow.Item(i) = ""
                    '            End If

                    '        End If
                    '    Next
                    'End If
                    dsResponse.Tables(table.TableName).Rows.Add(newRow)
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR")

        End Try

    End Sub

    Public Sub UpdateStatusCancel(ByVal Rxreferencenumber As String, ByVal MessageId As String)
        Dim objSureScriptDBLayer As New gloSureScriptDBLayer


        Try

            objSureScriptDBLayer.UpdateStatusCancel(Rxreferencenumber, MessageId)

        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.UpdateLog(ex.ToString)
            gloSurescriptGeneral.ErrorMessage(ex.ToString)
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.UpdateLog(ex.ToString)
            gloSurescriptGeneral.ErrorMessage(ex.ToString)
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.ToString)
            Throw New GloSurescriptException(ex.ToString)
        Finally
            If Not IsNothing(objSureScriptDBLayer) Then
                objSureScriptDBLayer.Dispose()
                objSureScriptDBLayer = Nothing
            End If
        End Try
    End Sub

    Private Sub GetIdentifierType(ByRef xmlwriter As XmlWriter, ByVal identifier As String, ByVal identifiertype As String)
        Try
            Select Case identifier
                Case "FileID"
                    xmlwriter.WriteElementString("FileID", identifier)
                Case "MedicareNumber"
                    xmlwriter.WriteElementString("MedicareNumber", identifier)
                Case "MedicaidNumber"
                    xmlwriter.WriteElementString("MedicaidNumber", identifier)
                Case "PPONumber"
                    xmlwriter.WriteElementString("PPONumber", identifier)
                Case "SocialSecurity"
                    xmlwriter.WriteElementString("SocialSecurity", identifier)
                Case "PayerIDNumber"
                    xmlwriter.WriteElementString("PayerIDNumber", identifier)
                Case "PriorAuthorization"
                    xmlwriter.WriteElementString("PriorAuthorization", identifier)
                Case "BIN"
                    xmlwriter.WriteElementString("BIN", identifier)
                Case "MutuallyDefined"
                    xmlwriter.WriteElementString("MutuallyDefined", identifier)
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Public Function GenerateXMLforNewRx(ByVal objPrescription As EPrescription, ByVal RefillItem As Int16, ByVal icnt As Int32) As Boolean
        If ValidateData(objPrescription, RefillItem, icnt) Then

            Dim strfilepath As String = GenerateFileName(MessageType.eNewRx)
            XMLFilePath = strfilepath
            Dim xmlwriter As XmlTextWriter = Nothing
            Dim _txtData As System.Text.StringBuilder = Nothing

            Try
                If File.Exists(strfilepath) Then
                    File.Delete(strfilepath)
                End If
                Dim oDrug As EDrug
                oDrug = objPrescription.DrugsCol.Item(RefillItem)

                Dim dtdate As DateTime = Date.UtcNow
                Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
                Dim strtime As String = Format(dtdate, "HH:mm:ss")
                Dim strmillisec As String = Format(dtdate, "mmm")
                Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                '                UNA:+.*/'
                'UIB+UNOA:0++TEST_8-4-2009-1731_20-685+++9994210002001:D+9995001:P+20090804:173120'
                'UIH+SCRIPT:008:001:NEWRX+GCP215943+HMGACU12345'
                'PVD+PC+TST999999:DH/9994210002001:SPI/1234567890:HPI+++Smith:Mary++Test Clinic+2033 Meadowview Lane:Kingsport:TN:37660:AD2:Suite 200+4235551234:TE/4235551235x1234:FX+Jones:Martin'
                'PVD+P2+9995001:D3+++++Gate City Pharmacy+101 US Hwy 23:Gate City:VA:24251+7035557732:TE/7035557733:FX/7035557734:WP'
                'PTT++20050624+Conrad:Caroline+F+123PBMPAYERUNIQUEID123456789%2%5555:2U/55598733%%001%001542115615485411324:2U+RT 6 BOX 559:GATE CITY:VA:24251:AD2:RT 6 BOX 559+test@surescripts.com:EM/4232927522:TE'
                'DRU+P:Test Drug Description:777770777:ND+EA:90.5:87+:Apply topically 2 times every day in a sufficient amount to cover the :affected area+ZDS:1:804/85:20070612:102+0+R:3+++Notes to Pharmacist.'
                '                COO(+4459) : BO(+PBMA + +444332222 + Conrad, Neal + DEF7890) '
                '                UIT(+GCP215943) '
                '                UIZ(++1) '
                '''''''-----------------------------------------------------------------------------------
                '                UNA:+\ *'
                'UIB+UNOA:0++NewRx929201017283624+++6045801183001:D+9199293:P+20100921:012945,0'
                'UIH+SCRIPT:008:001:NEWRX++404489676062686901'
                'PVD+P2+9199293:D3+++++Edifact4dot20+:Woodbridge:VA:22315+7035551234:TE*7035554321:FX'
                'PVD+PC+6045801183001:SPI+++Butler:Internist:E++gloClinic Institute+1949 Gillam
                'Way:Fairbanks:AK:99701:AD2:Ste D+9074554567:TE*9074557675:FX'
                'PTT++19520526+Johnson:Esther+F++1600 Rockville Pike:Trenton:NJ:08608+6098553055:TE'
                'DRU+P:Lipitor 10 MG Tablet:68258600009:ND+ZZ:30+:Take 1 tablet
                'QD+ZDS:8:804*85:20100921:102+0+R:1'
                '                UIT() '
                '                UIZ(++1) '

                Dim _txtFilePath = strfilepath.Replace(".xml", ".txt")
                _txtData = New System.Text.StringBuilder
                Dim _segmentCount As Int32 = 1
                _txtData.Append("UNA:+\ *'")
                _txtData.Append(Environment.NewLine)



                'Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
                'strUTCFormat = strdate & "T" & strtime & ".0Z"
                Dim strUTCFormat As String = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.FZ")
                xmlwriter = New XmlTextWriter(strfilepath, Nothing)
                oDrug.MessageName = "NewRx"
                oDrug.MessageID = "NewRx" & strtemp

                oDrug.DateTimeStamp = strUTCFormat
                oDrug.DateReceived = Now.Date
                oDrug.MessageFrom = "mailto:" & objPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
                oDrug.MessageTo = "mailto:" & objPrescription.RxPharmacy.PharmacyID & ".ncpdp@surescripts.com"


                xmlwriter.WriteStartDocument()
                xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 

                xmlwriter.WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

                xmlwriter.WriteStartElement("Header")

                xmlwriter.WriteElementString("To", oDrug.MessageTo) 'The Child 

                xmlwriter.WriteElementString("From", oDrug.MessageFrom) 'The Child 

                Dim strUTCFormat1 As String = strUTCFormat.Replace("-", "").Replace(":", "")
                strUTCFormat1 = strUTCFormat1.Replace("T", ":").Replace(".0Z", "")

                _txtData.Append("UIB+UNOA:0++")
                xmlwriter.WriteElementString("MessageID", oDrug.MessageID) 'The Child 
                _txtData.Append(oDrug.MessageID)
                _txtData.Append("+++" & objPrescription.RxPrescriber.PrescriberID & ":D+" & objPrescription.RxPharmacy.PharmacyID & ":P+" & strUTCFormat1 & ",0'")
                _txtData.Append(Environment.NewLine)

                'RelatesToMessageID is not available for this message
                xmlwriter.WriteElementString("SentTime", strUTCFormat) 'The Child 
                xmlwriter.WriteEndElement() 'End Header Element

                xmlwriter.WriteStartElement("Body")
                xmlwriter.WriteStartElement("NewRx")

                xmlwriter.WriteElementString("PrescriberOrderNumber", objPrescription.RxTransactionID)
                _txtData.Append("UIH+SCRIPT:008:001:NEWRX+" & objPrescription.RxTransactionID & "+" & objPrescription.RxTransactionID & "'")
                _txtData.Append(Environment.NewLine)

                xmlwriter.WriteStartElement("Pharmacy")

                xmlwriter.WriteStartElement("Identification")

                _txtData.Append("PVD+P2+" & objPrescription.RxPharmacy.PharmacyID & ":D3+++++" & objPrescription.RxPharmacy.PharmacyName.Trim & "+:")
                xmlwriter.WriteElementString("NCPDPID", objPrescription.RxPharmacy.PharmacyID)
                xmlwriter.WriteEndElement() 'End Identification Element
                If Not IsNothing(objPrescription.RxPharmacy.PharmacyName) Then
                    xmlwriter.WriteElementString("StoreName", objPrescription.RxPharmacy.PharmacyName.Trim)
                Else
                    xmlwriter.WriteElementString("StoreName", "")
                End If


                If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.City.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.State.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim <> "" Then
                    xmlwriter.WriteStartElement("Address")

                    If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim.Length > 35 Then
                        If icnt = 0 Then
                            'MessageBox.Show("This prescription request can not be sent because the pharmacy address has too many characters.  Please update the address so it has less than 35 characters. ", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy address line1 has too many characters." & vbCrLf) 'Please update the address so it has less than 35 characters.
                        End If
                        _txtData.Append(objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim & ":")
                    Else
                        If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim <> "" Then
                            xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim)
                        End If
                    End If

                    If objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim.Length > 35 Then
                        If icnt = 0 Then
                            'MessageBox.Show("This prescription request can not be sent because the pharmacy address has too many characters.  Please update the address so it has less than 35 characters.  ", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy address line2 has too many characters." & vbCrLf) '  Please update the address so it has less than 35 characters.  
                        End If
                    Else
                        If objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim <> "" Then
                            xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim)
                        End If

                    End If

                    If objPrescription.RxPharmacy.PharmacyAddress.City.Trim.Length > 35 Then
                        If icnt = 0 Then
                            'MessageBox.Show("This prescription request can not be sent because the pharmacy city name has too many characters.  Please update the city so it has less than 35 characters.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy city name has too many characters." & vbCrLf) '  Please update the city so it has less than 35 characters.
                        End If
                    Else
                        If objPrescription.RxPharmacy.PharmacyAddress.City.Trim <> "" Then
                            xmlwriter.WriteElementString("City", objPrescription.RxPharmacy.PharmacyAddress.City.Trim)
                        End If
                    End If
                    _txtData.Append(objPrescription.RxPharmacy.PharmacyAddress.City.Trim & ":")
                    If objPrescription.RxPharmacy.PharmacyAddress.State.Trim <> "" Then
                        If objPrescription.RxPharmacy.PharmacyAddress.State.Trim.Length > 2 Then
                            xmlwriter.WriteElementString("State", objPrescription.RxPharmacy.PharmacyAddress.State.Substring(0, 2))
                        Else
                            xmlwriter.WriteElementString("State", objPrescription.RxPharmacy.PharmacyAddress.State.Trim)
                        End If
                        _txtData.Append(objPrescription.RxPharmacy.PharmacyAddress.State.Trim & ":")
                    End If

                    If objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim <> "" Then
                        Dim objZipCode As New System.Text.RegularExpressions.Regex("(\d{5})|(\d{9})")
                        If objZipCode.Match(objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim).Success = False Then
                            'MessageBox.Show("This prescription request can not be sent because the pharmacy zip code has too many characters.Please update the zip code.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy zip code has too many characters." & vbCrLf) 'Please update the zip code.
                            End If
                        Else
                            If objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim <> "" Then
                                xmlwriter.WriteElementString("ZipCode", objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim)
                            End If
                        End If
                        _txtData.Append(objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim)
                    End If


                    xmlwriter.WriteEndElement() 'End Address Element
                End If
                If objPrescription.RxPharmacy.PharmacyPhone.Email.Trim <> "" Then
                    If objPrescription.RxPharmacy.PharmacyPhone.Email.Trim.Length > 80 Then
                        'MessageBox.Show("This prescription request can not be sent because the pharmacy email has too many characters.Please update the email.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy email has too many characters." & vbCrLf) 'Please update the email.
                        End If
                    Else
                        If objPrescription.RxPharmacy.PharmacyPhone.Email.Trim <> "" Then
                            xmlwriter.WriteElementString("Email", objPrescription.RxPharmacy.PharmacyPhone.Email.Trim)
                        End If
                    End If
                End If

                If objPrescription.RxPharmacy.PharmacyPhone.Phone.Trim <> "" Or objPrescription.RxPharmacy.PharmacyPhone.Fax.Trim <> "" Then

                    If objPrescription.RxPharmacy.PharmacyPhone.Phone.Trim <> "" Then
                        If GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone).Trim.Length > 25 Then
                            'MessageBox.Show("This prescription request can not be sent because the pharmacy phone number has too many characters.  Please update the phone number.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy phone number has too many characters." & vbCrLf) '  Please update the phone number.
                            End If
                        End If
                    End If
                    If objPrescription.RxPharmacy.PharmacyPhone.Fax.Trim <> "" Then
                        If GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Fax).Trim.Length > 25 Then
                            'MessageBox.Show("This prescription request can not be sent because the pharmacy fax has too many characters.  Please update the fax number.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy fax has too many characters." & vbCrLf) '  Please update the fax number.
                            End If
                        End If
                    End If
                    xmlwriter.WriteStartElement("PhoneNumbers")
                    If objPrescription.RxPharmacy.PharmacyPhone.Phone.Trim <> "" Then
                        xmlwriter.WriteStartElement("Phone")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone.Trim))
                        xmlwriter.WriteElementString("Qualifier", "TE")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If
                    _txtData.Append("+" & GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone.Trim) & ":TE*")
                    If objPrescription.RxPharmacy.PharmacyPhone.Fax.Trim <> "" Then
                        xmlwriter.WriteStartElement("Phone")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Fax.Trim))
                        xmlwriter.WriteElementString("Qualifier", "FX")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If
                    xmlwriter.WriteEndElement() 'End PhoneNumbers element
                    _txtData.Append(GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Fax.Trim) & ":FX'")
                    _txtData.Append(Environment.NewLine)
                End If

                xmlwriter.WriteEndElement() 'End Pharmacy Element

                'start <Prescriber> Tag
                xmlwriter.WriteStartElement("Prescriber")

                'start <Identification> sub element Tag
                xmlwriter.WriteStartElement("Identification")
                _txtData.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                xmlwriter.WriteElementString("SPI", objPrescription.RxPrescriber.PrescriberID)
                xmlwriter.WriteEndElement()
                'End <Identification> sub element Tag

                If objPrescription.ClinicName.Length > 35 Then
                    xmlwriter.WriteElementString("ClinicName", objPrescription.ClinicName.Substring(0, 35))
                Else
                    xmlwriter.WriteElementString("ClinicName", objPrescription.ClinicName.Trim)
                End If
                'xmlwriter.WriteElementString("ClinicName", objPrescription.ClinicName.Trim)

                'start <Name> sub element Tag
                xmlwriter.WriteStartElement("Name")
                xmlwriter.WriteElementString("LastName", objPrescription.RxPrescriber.PrescriberName.LastName.Trim)
                _txtData.Append(objPrescription.RxPrescriber.PrescriberName.LastName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.FirstName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim & "++" & objPrescription.ClinicName.Trim & "+")
                If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim <> "" Then
                    If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Length > 35 Then
                        'MessageBox.Show("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name." & vbCrLf)
                        End If

                    Else
                        xmlwriter.WriteElementString("FirstName", objPrescription.RxPrescriber.PrescriberName.FirstName.Trim)
                    End If

                End If

                If objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim <> "" Then
                    If objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim.Length > 35 Then
                        'MessageBox.Show("This prescription request can not be sent because the prescriber middle name has too many characters.  Please update the middle name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber middle name has too many characters.  Please update the middle name." & vbCrLf)
                        End If
                    Else
                        xmlwriter.WriteElementString("MiddleName", objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim)
                    End If
                End If

                xmlwriter.WriteEndElement() 'End Name Element
                'End <Name> sub element Tag


                xmlwriter.WriteStartElement("Address")
                xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim)
                _txtData.Append(objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim & ":")
                If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Length > 35 Then
                    'MessageBox.Show("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                    If icnt = 0 Then
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2." & vbCrLf)
                    End If
                Else
                    If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim <> "" Then
                        xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim)
                    End If
                End If


                xmlwriter.WriteElementString("City", objPrescription.RxPrescriber.PrescriberAddress.City)
                xmlwriter.WriteElementString("State", If(objPrescription.RxPrescriber.PrescriberAddress.State.Length > 2, objPrescription.RxPrescriber.PrescriberAddress.State.Substring(0, 2), objPrescription.RxPrescriber.PrescriberAddress.State))
                xmlwriter.WriteElementString("ZipCode", objPrescription.RxPrescriber.PrescriberAddress.Zip)
                xmlwriter.WriteEndElement() 'End Address Element
                _txtData.Append(objPrescription.RxPrescriber.PrescriberAddress.City & ":" & objPrescription.RxPrescriber.PrescriberAddress.State & ":" & objPrescription.RxPrescriber.PrescriberAddress.Zip & ":AD2:" & objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim & "+")

                If objPrescription.RxPrescriber.PrescriberPhone.Email.Trim.Length > 80 Then
                    'MessageBox.Show("This prescription request can not be sent because the prescriber email has too many characters.Please update the email.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                    If icnt = 0 Then
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber email has too many characters.Please update the email." & vbCrLf)
                    End If
                Else
                    If objPrescription.RxPrescriber.PrescriberPhone.Email.Trim <> "" Then
                        xmlwriter.WriteElementString("Email", objPrescription.RxPrescriber.PrescriberPhone.Email.Trim)
                    End If
                End If

                xmlwriter.WriteStartElement("PhoneNumbers")
                xmlwriter.WriteStartElement("Phone")
                xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Phone))
                xmlwriter.WriteElementString("Qualifier", "TE")
                xmlwriter.WriteEndElement() 'End Phone Element

                _txtData.Append(GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Phone) & ":TE*")
                If objPrescription.RxPrescriber.PrescriberPhone.Fax.Trim <> "" Then
                    xmlwriter.WriteStartElement("Phone")
                    xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Fax.Trim))
                    xmlwriter.WriteElementString("Qualifier", "FX")
                    xmlwriter.WriteEndElement() 'End Phone Element
                End If
                xmlwriter.WriteEndElement() 'End PhoneNumbers element
                _txtData.Append(GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Fax) & ":FX'")
                _txtData.Append(Environment.NewLine)

                xmlwriter.WriteEndElement() 'End Prescriber Element

                'added new Supervisor Tag
                If objPrescription.RxSupervisorProviderID <> 0 Then

                    'Start Supervisor Tag
                    xmlwriter.WriteStartElement("Supervisor")
                    'Start Identification Tag
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberID <> "" Then
                        xmlwriter.WriteStartElement("Identification")
                        '_txtData.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                        xmlwriter.WriteElementString("SPI", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberID)
                        xmlwriter.WriteEndElement()
                    ElseIf objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA <> "" Then
                        xmlwriter.WriteStartElement("Identification")
                        '_txtData.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                        xmlwriter.WriteElementString("DEANumber", objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA)
                        xmlwriter.WriteEndElement()
                    ElseIf objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI <> "" Then
                        xmlwriter.WriteStartElement("Identification")
                        '_txtData.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                        xmlwriter.WriteElementString("NPI", objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI)
                        xmlwriter.WriteEndElement()
                    End If
                    'End Identification Tag

                    'Start Name Tag
                    xmlwriter.WriteStartElement("Name")
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim <> "" Then
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim.Length > 35 Then
                            MessageBox.Show("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber last name has too many characters.  Please update the first name." & vbCrLf)
                            End If

                        Else
                            xmlwriter.WriteElementString("LastName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim)
                        End If

                    End If

                    '_txtData.Append(objPrescription.RxPrescriber.PrescriberName.LastName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.FirstName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim & "++" & objPrescription.ClinicName.Trim & "+")
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim <> "" Then
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Length > 35 Then
                            'MessageBox.Show("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber first name has too many characters.  Please update the first name." & vbCrLf)
                            End If

                        Else
                            xmlwriter.WriteElementString("FirstName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim)
                        End If

                    End If

                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim <> "" Then
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Length > 35 Then
                            'MessageBox.Show("This prescription request can not be sent because the prescriber middle name has too many characters.Please update the middle name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber middle name has too many characters.  Please update the middle name." & vbCrLf)
                            End If
                        Else
                            xmlwriter.WriteElementString("MiddleName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim)
                        End If
                    End If

                    xmlwriter.WriteEndElement()
                    'End Name Tag


                    ''ADDRESS--------------- tag start
                    xmlwriter.WriteStartElement("Address")

                    ' ''ADDRESS 1
                    xmlwriter.WriteElementString("AddressLine1", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim)
                    ''_txtData.Append(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim & ":")

                    ' ''ADDRESS 2
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim.Length > 35 Then
                        '    'MessageBox.Show("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber address line2 has too many characters.  Please update the address line2." & vbCrLf)
                        End If
                    Else
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim <> "" Then
                            xmlwriter.WriteElementString("AddressLine2", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim)
                        End If
                    End If

                    ''CITY
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim <> "" Then
                        xmlwriter.WriteElementString("City", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City)
                    End If

                    ''STATE
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Trim <> "" Then
                        xmlwriter.WriteElementString("State", If(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Length > 2, objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Substring(0, 2), objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State))
                    End If


                    ''ZIP
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim <> "" Then
                        xmlwriter.WriteElementString("ZipCode", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip)
                    End If


                    xmlwriter.WriteEndElement()
                    ''ADDRESS---------------End Address Element

                    ' _txtData.Append(objPrescription.RxPrescriber.PrescriberAddress.City & ":" & objPrescription.RxPrescriber.PrescriberAddress.State & ":" & objPrescription.RxPrescriber.PrescriberAddress.Zip & ":AD2:" & objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim & "+")

                    ' ''START EMAIL
                    'If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email.Trim.Length > 80 Then
                    '    'MessageBox.Show("This prescription request can not be sent because the prescriber email has too many characters.Please update the email.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    'Exit Function
                    '    If icnt = 0 Then
                    '        ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber email has too many characters.Please update the email." & vbCrLf)
                    '    End If
                    'Else
                    '    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email.Trim <> "" Then
                    '        xmlwriter.WriteElementString("Email", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email.Trim)
                    '    End If
                    'End If
                    ' ''END EMAIL

                    ' ''PHONE
                    'If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim <> "" Then
                    '    xmlwriter.WriteStartElement("PhoneNumbers")
                    '    xmlwriter.WriteStartElement("Phone")
                    '    xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone))
                    '    xmlwriter.WriteElementString("Qualifier", "TE")
                    '    xmlwriter.WriteEndElement() 'End Phone Element
                    'End If
                    ' ''END PHONE

                    ' ''FAX TAG
                    ''_txtData.Append(GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Phone) & ":TE*")
                    'If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax.Trim <> "" Then
                    '    xmlwriter.WriteStartElement("Phone")
                    '    xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax.Trim))
                    '    xmlwriter.WriteElementString("Qualifier", "FX")
                    '    xmlwriter.WriteEndElement() 'End Phone Element
                    'End If
                    ' ''END FAX TAG



                    xmlwriter.WriteEndElement()
                    'END Supervisor Tag

                End If
                _txtData.Append("PTT++" & Getdatetype(objPrescription.RxPatient.DateofBirth.Trim) & "+" & objPrescription.RxPatient.PatientName.LastName.Trim & ":" & objPrescription.RxPatient.PatientName.FirstName.Trim & "+" & GetGender(objPrescription.RxPatient.Gender.Trim) & "++")
                xmlwriter.WriteStartElement("Patient")
                If objPrescription.RxPatient.SSN.Trim <> "" Then
                    If objPrescription.RxPatient.SSN.Trim.Length > 35 Then
                        'MessageBox.Show("This prescription request can not be sent because the patient SSN has too many characters.  Please update the SSN.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the patient SSN has too many characters.  Please update the SSN." & vbCrLf)
                        End If
                    End If
                    xmlwriter.WriteStartElement("Identification")
                    xmlwriter.WriteElementString("SocialSecurity", objPrescription.RxPatient.SSN.Trim)
                    xmlwriter.WriteEndElement() 'End Identification Element
                End If

                xmlwriter.WriteStartElement("Name")
                xmlwriter.WriteElementString("LastName", objPrescription.RxPatient.PatientName.LastName.Trim)
                xmlwriter.WriteElementString("FirstName", objPrescription.RxPatient.PatientName.FirstName.Trim)

                If objPrescription.RxPatient.PatientName.MiddleName.Trim <> "" Then
                    If objPrescription.RxPatient.PatientName.MiddleName.Trim.Length > 35 Then
                        'MessageBox.Show("This prescription request can not be sent because the patient middle name has too many characters.Please update the middle name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the patient middle name has too many characters.Please update the middle name." & vbCrLf)
                        End If
                    Else
                        '' Problem 00000054 : Wrongly Written Prescriber Middle Name instead of Patient Middle Name
                        xmlwriter.WriteElementString("MiddleName", objPrescription.RxPatient.PatientName.MiddleName.Trim)
                    End If
                End If
                xmlwriter.WriteEndElement() 'End Name Element
                xmlwriter.WriteElementString("Gender", GetGender(objPrescription.RxPatient.Gender.Trim))
                xmlwriter.WriteElementString("DateOfBirth", Getdatetypewithoutdash(objPrescription.RxPatient.DateofBirth.Trim))

                If objPrescription.RxPatient.PatientAddress.Address1.Trim <> "" Or objPrescription.RxPatient.PatientAddress.City.Trim <> "" Or objPrescription.RxPatient.PatientAddress.State.Trim <> "" Or objPrescription.RxPatient.PatientAddress.Zip.Trim <> "" Then
                    xmlwriter.WriteStartElement("Address")
                    If objPrescription.RxPatient.PatientAddress.Address1.Trim <> "" Then
                        If objPrescription.RxPatient.PatientAddress.Address1.Trim.Length > 35 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient address line1 has too many characters.Please update the patient address line1.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient address line1 has too many characters.Please update the patient address line1." & vbCrLf)
                            End If
                        Else
                            xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPatient.PatientAddress.Address1.Trim)
                        End If
                        _txtData.Append(objPrescription.RxPatient.PatientAddress.Address1.Trim & ":")
                    End If
                    If objPrescription.RxPatient.PatientAddress.Address2.Trim <> "" Then
                        If objPrescription.RxPatient.PatientAddress.Address2.Trim.Length > 35 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient address line2 has too many characters.Please update the patient address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient address line2 has too many characters.Please update the patient address line2." & vbCrLf)
                            End If
                        Else
                            xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPatient.PatientAddress.Address2.Trim)
                        End If
                    End If
                    If objPrescription.RxPatient.PatientAddress.City.Trim <> "" Then
                        If objPrescription.RxPatient.PatientAddress.City.Trim.Length > 35 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient city has too many characters.Please update the patient address line1.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient city has too many characters.Please update the patient city." & vbCrLf)
                            End If
                        Else
                            xmlwriter.WriteElementString("City", objPrescription.RxPatient.PatientAddress.City.Trim)
                        End If
                        _txtData.Append(objPrescription.RxPatient.PatientAddress.City.Trim & ":")
                    End If
                    If objPrescription.RxPatient.PatientAddress.State.Trim <> "" Then
                        If objPrescription.RxPatient.PatientAddress.State.Length > 2 Then
                            xmlwriter.WriteElementString("State", objPrescription.RxPatient.PatientAddress.State.Substring(0, 2))
                        Else
                            xmlwriter.WriteElementString("State", objPrescription.RxPatient.PatientAddress.State.Trim)
                        End If
                        _txtData.Append(objPrescription.RxPatient.PatientAddress.State.Trim & ":")
                    End If
                    If objPrescription.RxPatient.PatientAddress.Zip.Trim <> "" Then
                        Dim objZipCode As New System.Text.RegularExpressions.Regex("(\d{5})|(\d{9})")
                        If objZipCode.Match(objPrescription.RxPatient.PatientAddress.Zip.Trim).Success = False Then
                            'MessageBox.Show("This prescription request can not be sent because the patient zipcode has too many characters.Please update the patient zipcode.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient zipcode has too many characters.Please update the patient zipcode." & vbCrLf)

                            End If
                        Else
                            xmlwriter.WriteElementString("ZipCode", objPrescription.RxPatient.PatientAddress.Zip.Trim)
                        End If
                        _txtData.Append(objPrescription.RxPatient.PatientAddress.Zip.Trim & "+")
                    End If
                    xmlwriter.WriteEndElement() 'End Address Element
                End If

                'Patient Email
                If objPrescription.RxPatient.PatientPhone.Email.Trim <> "" Then
                    If objPrescription.RxPatient.PatientPhone.Email.Trim.Length > 80 Then
                        'MessageBox.Show("This prescription request can not be sent because the patient email has too many characters.Please update the patient email.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the patient email has too many characters.Please update the patient email." & vbCrLf)
                        End If
                    Else
                        If objPrescription.RxPatient.PatientPhone.Email.Trim <> "" Then
                            xmlwriter.WriteElementString("Email", objPrescription.RxPatient.PatientPhone.Email.Trim)
                        End If
                    End If
                End If

                If objPrescription.RxPatient.PatientPhone.Phone.Trim <> "" Or objPrescription.RxPatient.PatientPhone.Fax.Trim <> "" Or objPrescription.RxPatient.PatientWorkPhone.Phone.Trim <> "" Then
                    If objPrescription.RxPatient.PatientPhone.Phone.Trim <> "" Then
                        If GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone).Trim.Length > 25 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient phone has too many characters.Please update the patient phone.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient phone has too many characters.Please update the patient phone." & vbCrLf)
                            End If
                        End If
                    End If
                    If objPrescription.RxPatient.PatientPhone.Fax.Trim <> "" Then
                        If GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Fax.Trim).Length > 25 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient fax number has too many characters.Please update the patient fax number.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient fax number has too many characters.Please update the patient fax number." & vbCrLf)
                            End If
                        End If
                    End If
                    If objPrescription.RxPatient.PatientWorkPhone.Phone.Trim <> "" Then
                        If GetPhoneNumber(objPrescription.RxPatient.PatientWorkPhone.Phone).Trim.Length > 25 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient Work phone has too many characters.Please update the patient Work phone.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient Work phone has too many characters.Please update the patient Work phone." & vbCrLf)
                            End If
                        End If
                    End If
                    xmlwriter.WriteStartElement("PhoneNumbers")
                    If objPrescription.RxPatient.PatientPhone.Phone.Trim <> "" Then
                        xmlwriter.WriteStartElement("Phone")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone.Trim))
                        xmlwriter.WriteElementString("Qualifier", "TE")
                        xmlwriter.WriteEndElement() 'End Phone Element
                        _txtData.Append(GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone.Trim) & ":TE'")
                        _txtData.Append(Environment.NewLine)
                    End If

                    If objPrescription.RxPatient.PatientPhone.Fax.Trim <> "" Then
                        xmlwriter.WriteStartElement("Phone")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Fax.Trim))
                        xmlwriter.WriteElementString("Qualifier", "FX")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If

                    If objPrescription.RxPatient.PatientWorkPhone.Phone.Trim <> "" Then
                        xmlwriter.WriteStartElement("Phone")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientWorkPhone.Phone.Trim))
                        xmlwriter.WriteElementString("Qualifier", "WP")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If
                    xmlwriter.WriteEndElement() 'End PhoneNumbers element

                End If

                xmlwriter.WriteEndElement() 'End Patient Element

                xmlwriter.WriteStartElement("MedicationPrescribed")
                xmlwriter.WriteElementString("DrugDescription", objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim)

                _txtData.Append("DRU+P:" & objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).Drugform.Trim & ":" & oDrug.ProdCode & ":" & oDrug.ProdCodeQualifier & "::::" & oDrug.DDID & ":MD+")

                xmlwriter.WriteStartElement("DrugCoded") 'Drugcoded element starts
                If oDrug.ProdCode.Trim.Length > 0 Then
                    xmlwriter.WriteElementString("ProductCode", oDrug.ProdCode)
                End If
                If oDrug.ProdCodeQualifier.Trim.Length > 0 Then
                    xmlwriter.WriteElementString("ProductCodeQualifier", oDrug.ProdCodeQualifier)
                End If
                xmlwriter.WriteElementString("DrugDBCode", "DDID") ''''Medi-Span Master Drug Data Base
                xmlwriter.WriteElementString("DrugDBCodeQualifier", "MD") ''''pass DDID
                xmlwriter.WriteEndElement() 'DrugCoded Element Ends

                '_txtData.Append(oDrug.DrugQuantityQualifier & ":" & Val(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim) & ":38+:" & objPrescription.DrugsCol.Item(RefillItem).Directions.Trim & "+ZDS:" & Val(oDrug.DrugDuration.Trim) & ":804*85:")

                xmlwriter.WriteStartElement("Quantity")

                xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim))
                ' xmlwriter.WriteElementString("Qualifier", "38") ''''''from PRN imple guide CodeList
                xmlwriter.WriteEndElement() 'End Quantity Element

                Try
                    If Not IsNothing(oDrug) Then
                        If oDrug.DrugDuration.Trim.Length > 0 AndAlso oDrug.DrugDuration.Trim.Length <= 3 Then
                            If IsNumeric(oDrug.DrugDuration) Then
                                xmlwriter.WriteElementString("DaysSupply", Val(oDrug.DrugDuration.Trim))
                            End If
                        End If
                    End If
                Catch ex As Exception

                End Try
                ''Handling Special character for NewRx
                Dim repstr As String = "&#176;"
                Dim degreestr As String = "°"
                objPrescription.DrugsCol.Item(RefillItem).Directions = objPrescription.DrugsCol.Item(RefillItem).Directions.Replace(degreestr, repstr)
                xmlwriter.WriteElementString("Directions", objPrescription.DrugsCol.Item(RefillItem).Directions.Trim)

                If Not IsNothing(oDrug.Notes) Then
                    If oDrug.Notes.Trim.Length > 0 Then
                        If oDrug.Notes.Trim.Length > 210 Then
                            'MessageBox.Show("This prescription request can not be sent because the drug notes has too many characters.Please update the drug notes.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug notes has too many characters for " & oDrug.DrugName & ".Please update the drug notes." & vbCrLf)
                        Else
                            xmlwriter.WriteElementString("Note", oDrug.Notes.Trim.Replace(Environment.NewLine, " "))
                        End If

                    End If
                End If

                _txtData.Append(Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate.Trim) & ":102+0+R:1'")
                _txtData.Append(Environment.NewLine)

                xmlwriter.WriteStartElement("Refills")
                If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier.Trim.Length <= 0 Then
                    xmlwriter.WriteElementString("Qualifier", "R")
                Else
                    xmlwriter.WriteElementString("Qualifier", objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier.Trim)
                End If

                If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier = "R" Then
                    If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim <> "" Then
                        xmlwriter.WriteElementString("Quantity", Val(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim))
                    End If
                End If
                xmlwriter.WriteEndElement() 'End Refills Element

                If objPrescription.DrugsCol.Item(RefillItem).MaySubstitute Then
                    xmlwriter.WriteElementString("Substitutions", 0)
                Else
                    xmlwriter.WriteElementString("Substitutions", 1)
                End If
                xmlwriter.WriteElementString("WrittenDate", Getdatetypewithoutdash(objPrescription.DrugsCol.Item(RefillItem).WrittenDate.Trim))
                xmlwriter.WriteEndElement() 'End MedicationPrescribed Element
                xmlwriter.WriteEndElement() 'End NewRx element
                xmlwriter.WriteEndElement() 'End Body Element
                xmlwriter.WriteEndElement() 'End Message Element
                xmlwriter.WriteEndDocument()
                xmlwriter.Close()

                _txtData.Append("UIT+" & objPrescription.RxTransactionID & "+9'")
                _txtData.Append(Environment.NewLine)
                _txtData.Append("UIZ++1'")
                _txtData.Append(Environment.NewLine)

                If ValidationMessageBuilder.Length > 0 Or ValidationMessageBuilderforDrug.Length > 0 Then
                    GenerateXMLforNewRx = Nothing
                    Exit Function
                End If

                Dim NoOfAttempt As Integer = 1
                Dim _isWithOutInternet As Boolean = False
                Dim result As Byte() = Nothing

                While (NoOfAttempt <= 5)

                    If _isWithOutInternet = True Then
                        Exit While
                    End If

                    result = gloSurescriptGeneral.PostSurescriptMessage(XMLFilePath, objPrescription, _isWithOutInternet)

                    If Not IsNothing(result) Then
                        Exit While
                    Else
                        NoOfAttempt = NoOfAttempt + 1
                    End If

                End While

                If Not IsNothing(result) Then
                    ReadStatusMessageRevised(result, SentMessageType.eNewRx, objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim)
                    My.Computer.FileSystem.WriteAllText(_txtFilePath, _txtData.ToString(), False)
                    NewRxEDIFACTFileData = _txtData ''''assign the EDIfact file data to the glosurescript interface so that we can acces it on clsgloemrprescription class to save this info in database
                End If

                _txtData = Nothing
                Return True

            Catch ex As gloSurescriptDBException
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                GenerateXMLforNewRx = Nothing
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                GenerateXMLforNewRx = Nothing
            Catch ex As Exception
                GenerateXMLforNewRx = Nothing
                Throw New GloSurescriptException(ex.Message)
            Finally
                If Not IsNothing(_txtData) Then
                    _txtData = Nothing
                End If
                If Not IsNothing(xmlwriter) Then
                    xmlwriter.Close()
                    xmlwriter = Nothing
                End If
            End Try
        Else
            GenerateXMLforNewRx = Nothing
        End If

    End Function

    Public Function Generate10dot6XMLforNewRx(ByVal objPrescription As EPrescription, ByVal RefillItem As Int16, ByVal icnt As Int32, Optional ByVal SendMessageType As String = "NewRx", Optional ByVal sRelatestoMessageID As String = "", Optional ByVal IsDenied As Boolean = False) As Boolean
        If Validate10dot6Data(objPrescription, RefillItem, icnt) Then
            If ValidationMessageBuilderfor10dot6.Length > 0 Then
                If System.Windows.Forms.MessageBox.Show("Following data fields exceed number of characters allowed in Surescripts standards and will therefore be truncated before sending to Surescripts and  (Allowed characters are shown in parenthesis) " + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine + ValidationMessageBuilderfor10dot6.ToString & System.Environment.NewLine + System.Environment.NewLine & System.Environment.NewLine & "Do you want to continue?", "gloEMR", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = MsgBoxResult.No Then
                    Return Nothing
                    Exit Function
                End If
            End If
            Dim strfilepath As String = GenerateFileName(MessageType.eNewRx)
            XMLFilePath = strfilepath
            Dim xmlwriter As XmlTextWriter = Nothing
            Dim _txtdata As System.Text.StringBuilder = Nothing

            Try
                If File.Exists(strfilepath) Then
                    File.Delete(strfilepath)
                End If
                Dim oDrug As EDrug
                oDrug = objPrescription.DrugsCol.Item(RefillItem)

                Dim dtdate As DateTime = Date.UtcNow
                Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
                Dim strtime As String = Format(dtdate, "HH:mm:ss")
                Dim strmillisec As String = Format(dtdate, "mmm")
                Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
                '                UNA:+.*/'
                'UIB+UNOA:0++TEST_8-4-2009-1731_20-685+++9994210002001:D+9995001:P+20090804:173120'
                'UIH+SCRIPT:008:001:NEWRX+GCP215943+HMGACU12345'
                'PVD+PC+TST999999:DH/9994210002001:SPI/1234567890:HPI+++Smith:Mary++Test Clinic+2033 Meadowview Lane:Kingsport:TN:37660:AD2:Suite 200+4235551234:TE/4235551235x1234:FX+Jones:Martin'
                'PVD+P2+9995001:D3+++++Gate City Pharmacy+101 US Hwy 23:Gate City:VA:24251+7035557732:TE/7035557733:FX/7035557734:WP'
                'PTT++20050624+Conrad:Caroline+F+123PBMPAYERUNIQUEID123456789%2%5555:2U/55598733%%001%001542115615485411324:2U+RT 6 BOX 559:GATE CITY:VA:24251:AD2:RT 6 BOX 559+test@surescripts.com:EM/4232927522:TE'
                'DRU+P:Test Drug Description:777770777:ND+EA:90.5:87+:Apply topically 2 times every day in a sufficient amount to cover the :affected area+ZDS:1:804/85:20070612:102+0+R:3+++Notes to Pharmacist.'
                '                COO(+4459) : BO(+PBMA + +444332222 + Conrad, Neal + DEF7890) '
                '                UIT(+GCP215943) '
                '                UIZ(++1) '
                '''''''-----------------------------------------------------------------------------------
                '                UNA:+\ *'
                'UIB+UNOA:0++NewRx929201017283624+++6045801183001:D+9199293:P+20100921:012945,0'
                'UIH+SCRIPT:008:001:NEWRX++404489676062686901'
                'PVD+P2+9199293:D3+++++Edifact4dot20+:Woodbridge:VA:22315+7035551234:TE*7035554321:FX'
                'PVD+PC+6045801183001:SPI+++Butler:Internist:E++gloClinic Institute+1949 Gillam
                'Way:Fairbanks:AK:99701:AD2:Ste D+9074554567:TE*9074557675:FX'
                'PTT++19520526+Johnson:Esther+F++1600 Rockville Pike:Trenton:NJ:08608+6098553055:TE'
                'DRU+P:Lipitor 10 MG Tablet:68258600009:ND+ZZ:30+:Take 1 tablet
                'QD+ZDS:8:804*85:20100921:102+0+R:1'
                '                UIT() '
                '                UIZ(++1) '

                Dim _txtFilePath = strfilepath.Replace(".xml", ".txt")
                _txtdata = New System.Text.StringBuilder
                Dim _segmentCount As Int32 = 1
                _txtdata.Append("UNA:+\ *'")
                _txtdata.Append(Environment.NewLine)



                'Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
                'strUTCFormat = strdate & "T" & strtime & ".0Z"
                Dim strUTCFormat As String = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.FZ")
                xmlwriter = New XmlTextWriter(strfilepath, Nothing)
                oDrug.MessageName = "NewRx"
                oDrug.MessageID = "NewRx" & strtemp

                oDrug.DateTimeStamp = strUTCFormat
                oDrug.DateReceived = Now.Date
                'oDrug.MessageFrom = "mailto:" & objPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
                'oDrug.MessageTo = "mailto:" & objPrescription.RxPharmacy.PharmacyID & ".ncpdp@surescripts.com"
                oDrug.MessageFrom = objPrescription.RxPrescriber.PrescriberID
                oDrug.MessageTo = objPrescription.RxPharmacy.PharmacyID


                xmlwriter.WriteStartDocument()
                xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 
                xmlwriter.WriteAttributeString("xmlns", "http://www.surescripts.com/messaging")

                ''added new tags in 10.6 

                xmlwriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
                xmlwriter.WriteAttributeString("version", "010")
                xmlwriter.WriteAttributeString("release", "006")
                xmlwriter.WriteAttributeString("xsi:schemaLocation", "http://www.surescripts.com/messaging SS_SCRIPT_XML_10_6.xsd")

                xmlwriter.WriteStartElement("Header")

                ' xmlwriter.WriteElementString("To", oDrug.MessageTo) 'The Child 
                xmlwriter.WriteStartElement("To")
                xmlwriter.WriteAttributeString("Qualifier", "P")
                xmlwriter.WriteString(oDrug.MessageTo)
                xmlwriter.WriteEndElement()

                'xmlwriter.WriteElementString("From", oDrug.MessageFrom) 'The Child 
                xmlwriter.WriteStartElement("From")
                xmlwriter.WriteAttributeString("Qualifier", "D")
                xmlwriter.WriteString(oDrug.MessageFrom)
                xmlwriter.WriteEndElement()

                Dim strUTCFormat1 As String = strUTCFormat.Replace("-", "").Replace(":", "")
                strUTCFormat1 = strUTCFormat1.Replace("T", ":").Replace(".0Z", "")

                _txtdata.Append("UIB+UNOA:0++")
                xmlwriter.WriteElementString("MessageID", oDrug.MessageID) 'The Child 
                _txtdata.Append(oDrug.MessageID)
                _txtdata.Append("+++" & objPrescription.RxPrescriber.PrescriberID & ":D+" & objPrescription.RxPharmacy.PharmacyID & ":P+" & strUTCFormat1 & ",0'")
                _txtdata.Append(Environment.NewLine)

                If sRelatestoMessageID <> "" Then
                    If IsDenied = False Then
                        xmlwriter.WriteElementString("RelatesToMessageID", oDrug.RelatesToMessageId)
                    End If
                End If
                'RelatesToMessageID is not available for this message
                xmlwriter.WriteElementString("SentTime", strUTCFormat) 'The Child 


                'new tag for 10.6
                xmlwriter.WriteStartElement("SenderSoftware")
                Dim strVersion As String = My.Application.Info.Version.ToString.Substring(0, 3)
                Dim strProductName As String = My.Application.Info.ProductName.ToString()
                Dim strCompany As String = My.Application.Info.CompanyName
                xmlwriter.WriteElementString("SenderSoftwareDeveloper", strCompany) 'The Child 
                xmlwriter.WriteElementString("SenderSoftwareProduct", strProductName)
                xmlwriter.WriteElementString("SenderSoftwareVersionRelease", strVersion)
                xmlwriter.WriteEndElement()
                ''end of new tag
                If oDrug.RxReferenceNumber.Trim <> "" Then
                    If IsDenied = False Then
                        xmlwriter.WriteElementString("RxReferenceNumber", oDrug.RxReferenceNumber)
                    End If
                End If
                If objPrescription.RxTransactionID <> "" And objPrescription.RxTransactionID <> "0" Then
                    xmlwriter.WriteElementString("PrescriberOrderNumber", objPrescription.RxTransactionID)
                Else
                    Dim nPrescOrdNo As Long = gloSurescriptGeneral.GetUniqueID()
                    xmlwriter.WriteElementString("PrescriberOrderNumber", nPrescOrdNo)
                End If

                xmlwriter.WriteEndElement() 'End Header Element

                xmlwriter.WriteStartElement("Body")

                xmlwriter.WriteStartElement("NewRx")


                _txtdata.Append("UIH+SCRIPT:008:001:NEWRX+" & objPrescription.RxTransactionID & "+" & objPrescription.RxTransactionID & "'")
                _txtdata.Append(Environment.NewLine)

                xmlwriter.WriteStartElement("Pharmacy")

                xmlwriter.WriteStartElement("Identification")

                _txtdata.Append("PVD+P2+" & objPrescription.RxPharmacy.PharmacyID & ":D3+++++" & objPrescription.RxPharmacy.PharmacyName.Trim & "+:")
                xmlwriter.WriteElementString("NCPDPID", objPrescription.RxPharmacy.PharmacyID.Trim)

                xmlwriter.WriteEndElement() 'End Identification Element
                If Not IsNothing(objPrescription.RxPharmacy.PharmacyName) Then
                    If objPrescription.RxPharmacy.PharmacyName.Trim.Length > 35 Then
                        xmlwriter.WriteElementString("StoreName", objPrescription.RxPharmacy.PharmacyName.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("StoreName", objPrescription.RxPharmacy.PharmacyName.Trim)
                    End If
                Else
                    xmlwriter.WriteElementString("StoreName", "")
                End If


                If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.City.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.State.Trim <> "" Or objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim <> "" Then
                    xmlwriter.WriteStartElement("Address")

                    If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim.Length > 35 Then
                        'If icnt = 0 Then
                        '    'MessageBox.Show("This prescription request can not be sent because the pharmacy address has too many characters.  Please update the address so it has less than 35 characters. ", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    'Exit Function
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy address line1 has too many characters." & vbCrLf) 'Please update the address so it has less than 35 characters.
                        'End If
                        xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim.Substring(0, 35))
                        _txtdata.Append(objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim.Substring(0, 35) & ":")
                    Else
                        If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim <> "" Then
                            xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim)
                        End If
                    End If

                    If objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim.Length > 35 Then
                        'If icnt = 0 Then
                        '    'MessageBox.Show("This prescription request can not be sent because the pharmacy address has too many characters.  Please update the address so it has less than 35 characters.  ", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    'Exit Function
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy address line2 has too many characters." & vbCrLf) '  Please update the address so it has less than 35 characters.  
                        'End If
                        xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim.Substring(0, 35))
                    Else
                        If objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim <> "" Then
                            xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim)
                        End If

                    End If

                    If objPrescription.RxPharmacy.PharmacyAddress.City.Trim.Length > 35 Then
                        'If icnt = 0 Then
                        '    'MessageBox.Show("This prescription request can not be sent because the pharmacy city name has too many characters.  Please update the city so it has less than 35 characters.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    'Exit Function
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy city name has too many characters." & vbCrLf) '  Please update the city so it has less than 35 characters.
                        'End If
                        xmlwriter.WriteElementString("City", objPrescription.RxPharmacy.PharmacyAddress.City.Trim.Substring(0, 35))
                    Else
                        If objPrescription.RxPharmacy.PharmacyAddress.City.Trim <> "" Then
                            xmlwriter.WriteElementString("City", objPrescription.RxPharmacy.PharmacyAddress.City.Trim)
                        End If
                    End If
                    _txtdata.Append(objPrescription.RxPharmacy.PharmacyAddress.City.Trim & ":")
                    If objPrescription.RxPharmacy.PharmacyAddress.State.Trim <> "" Then
                        If objPrescription.RxPharmacy.PharmacyAddress.State.Trim.Length > 2 Then
                            xmlwriter.WriteElementString("State", objPrescription.RxPharmacy.PharmacyAddress.State.Substring(0, 2))
                        Else
                            xmlwriter.WriteElementString("State", objPrescription.RxPharmacy.PharmacyAddress.State.Trim)
                        End If
                        _txtdata.Append(objPrescription.RxPharmacy.PharmacyAddress.State.Trim & ":")
                    End If

                    If objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim <> "" Then
                        Dim objZipCode As New System.Text.RegularExpressions.Regex("(\d{5})|(\d{9})")
                        If objZipCode.Match(objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim).Success = False Then
                            'MessageBox.Show("This prescription request can not be sent because the pharmacy zip code has too many characters.Please update the zip code.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            'If icnt = 0 Then
                            '    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy zip code has too many characters." & vbCrLf) 'Please update the zip code.
                            'End If
                        Else
                            If objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim <> "" Then
                                xmlwriter.WriteElementString("ZipCode", objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim)
                            End If
                        End If
                        _txtdata.Append(objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim)
                    End If

                    If objPrescription.RxPharmacy.PharmacyAddress.Address2.Trim <> "" Then
                        xmlwriter.WriteElementString("PlaceLocationQualifier", "AD2")
                    End If

                    xmlwriter.WriteEndElement() 'End Address Element
                End If


                If objPrescription.RxPharmacy.PharmacyPhone.Phone.Trim <> "" Or objPrescription.RxPharmacy.PharmacyPhone.Fax.Trim <> "" Then

                    If objPrescription.RxPharmacy.PharmacyPhone.Phone.Trim <> "" Then
                        If GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone).Trim.Length > 25 Then
                            'MessageBox.Show("This prescription request can not be sent because the pharmacy phone number has too many characters.  Please update the phone number.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy phone number has too many characters." & vbCrLf) '  Please update the phone number.
                            End If
                        End If
                    End If
                    If objPrescription.RxPharmacy.PharmacyPhone.Fax.Trim <> "" Then
                        If GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Fax).Trim.Length > 25 Then
                            'MessageBox.Show("This prescription request can not be sent because the pharmacy fax has too many characters.  Please update the fax number.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy fax has too many characters." & vbCrLf) '  Please update the fax number.
                            End If
                        End If
                    End If
                    xmlwriter.WriteStartElement("CommunicationNumbers")
                    If objPrescription.RxPharmacy.PharmacyPhone.Phone.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone.Trim))
                        xmlwriter.WriteElementString("Qualifier", "TE")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If
                    _txtdata.Append("+" & GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone.Trim) & ":TE*")
                    If objPrescription.RxPharmacy.PharmacyPhone.Fax.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Fax.Trim))
                        xmlwriter.WriteElementString("Qualifier", "FX")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If

                    ''resolved bug 50462 in 7030 
                    If objPrescription.RxPharmacy.PharmacyPhone.Email.Trim <> "" Then
                        If objPrescription.RxPharmacy.PharmacyPhone.Email.Trim.Length > 80 Then
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy email has too many characters." & vbCrLf) 'Please update the email.
                            End If
                        Else
                            xmlwriter.WriteStartElement("Communication")
                            xmlwriter.WriteElementString("Number", objPrescription.RxPharmacy.PharmacyPhone.Email.Trim)
                            xmlwriter.WriteElementString("Qualifier", "EM")
                            xmlwriter.WriteEndElement() 'End Phone Element
                        End If
                    End If

                    xmlwriter.WriteEndElement() 'End CommunicationNumbers element
                    _txtdata.Append(GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Fax.Trim) & ":FX'")
                    _txtdata.Append(Environment.NewLine)
                End If

                xmlwriter.WriteEndElement() 'End Pharmacy Element

                'start <Prescriber> Tag
                xmlwriter.WriteStartElement("Prescriber")

                'start <Identification> sub element Tag
                xmlwriter.WriteStartElement("Identification")
                _txtdata.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                If objPrescription.RxPrescriber.PrescriberDEA <> "" Then
                    xmlwriter.WriteElementString("DEANumber", objPrescription.RxPrescriber.PrescriberDEA)
                End If
                'If objPrescription.RxPrescriber.PrescriberSSN <> "" Then
                '    xmlwriter.WriteElementString("SocialSecurity", objPrescription.RxPrescriber.PrescriberSSN)
                'End If

                xmlwriter.WriteElementString("NPI", objPrescription.RxPrescriber.PrescriberNPI)

                xmlwriter.WriteEndElement()
                'End <Identification> sub element Tag

                If objPrescription.ClinicName.Length > 35 Then
                    xmlwriter.WriteElementString("ClinicName", objPrescription.ClinicName.Substring(0, 35))
                Else
                    xmlwriter.WriteElementString("ClinicName", objPrescription.ClinicName.Trim)
                End If
                'xmlwriter.WriteElementString("ClinicName", objPrescription.ClinicName.Trim)

                'start <Name> sub element Tag
                xmlwriter.WriteStartElement("Name")
                If objPrescription.RxPrescriber.PrescriberName.LastName.Trim.Length > 35 Then
                    xmlwriter.WriteElementString("LastName", objPrescription.RxPrescriber.PrescriberName.LastName.Trim.Substring(0, 35))
                    _txtdata.Append(objPrescription.RxPrescriber.PrescriberName.LastName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.FirstName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim & "++" & objPrescription.ClinicName.Trim & "+")

                Else
                    xmlwriter.WriteElementString("LastName", objPrescription.RxPrescriber.PrescriberName.LastName.Trim)
                    _txtdata.Append(objPrescription.RxPrescriber.PrescriberName.LastName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.FirstName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim & "++" & objPrescription.ClinicName.Trim & "+")
                End If
                If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim <> "" Then
                    If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Length > 35 Then
                        'MessageBox.Show("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        'If icnt = 0 Then
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name." & vbCrLf)
                        'End If
                        xmlwriter.WriteElementString("FirstName", objPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("FirstName", objPrescription.RxPrescriber.PrescriberName.FirstName.Trim)
                    End If

                End If

                'If objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim <> "" Then
                '    If objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim.Length > 35 Then
                '        'MessageBox.Show("This prescription request can not be sent because the prescriber middle name has too many characters.  Please update the middle name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        'Exit Function
                '        'If icnt = 0 Then
                '        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber middle name has too many characters.  Please update the middle name." & vbCrLf)
                '        'End If
                '        xmlwriter.WriteElementString("MiddleName", objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim.Substring(0, 35))
                '    Else
                '        xmlwriter.WriteElementString("MiddleName", objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim)
                '    End If
                'End If

                xmlwriter.WriteEndElement() 'End Name Element
                'End <Name> sub element Tag


                xmlwriter.WriteStartElement("Address")
                If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Length > 35 Then
                    xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Substring(0, 35))
                    _txtdata.Append(objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Substring(0, 35) & ":")
                Else
                    xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim)
                    _txtdata.Append(objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim & ":")
                End If
                If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Length > 35 Then
                    'MessageBox.Show("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                    'If icnt = 0 Then
                    '    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2." & vbCrLf)
                    'End If
                    xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Substring(0, 35))
                Else
                    If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim <> "" Then
                        xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim)
                    End If
                End If

                If objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Length > 35 Then
                    xmlwriter.WriteElementString("City", objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Substring(0, 35))
                Else
                    xmlwriter.WriteElementString("City", objPrescription.RxPrescriber.PrescriberAddress.City)
                End If

                If objPrescription.RxPrescriber.PrescriberAddress.State.Trim.Length > 2 Then
                    xmlwriter.WriteElementString("State", objPrescription.RxPrescriber.PrescriberAddress.State.Substring(0, 2))
                Else
                    xmlwriter.WriteElementString("State", objPrescription.RxPrescriber.PrescriberAddress.State)
                End If

                If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim.Length > 11 Then
                    xmlwriter.WriteElementString("ZipCode", objPrescription.RxPrescriber.PrescriberAddress.Zip.Substring(0, 11))
                Else
                    xmlwriter.WriteElementString("ZipCode", objPrescription.RxPrescriber.PrescriberAddress.Zip)
                End If

                If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim <> "" Then
                    xmlwriter.WriteElementString("PlaceLocationQualifier", "AD2")
                End If

                xmlwriter.WriteEndElement() 'End Address Element
                _txtdata.Append(objPrescription.RxPrescriber.PrescriberAddress.City & ":" & objPrescription.RxPrescriber.PrescriberAddress.State & ":" & objPrescription.RxPrescriber.PrescriberAddress.Zip & ":AD2:" & objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim & "+")



                xmlwriter.WriteStartElement("CommunicationNumbers")
                xmlwriter.WriteStartElement("Communication")
                xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Phone))
                xmlwriter.WriteElementString("Qualifier", "TE")
                xmlwriter.WriteEndElement() 'End Phone Element

                _txtdata.Append(GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Phone) & ":TE*")
                If objPrescription.RxPrescriber.PrescriberPhone.Fax.Trim <> "" Then
                    xmlwriter.WriteStartElement("Communication")
                    xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Fax.Trim))
                    xmlwriter.WriteElementString("Qualifier", "FX")
                    xmlwriter.WriteEndElement() 'End Phone Element
                End If

                ''resolved bug 50462 in 7030 
                If objPrescription.RxPrescriber.PrescriberPhone.Email.Trim <> "" Then
                    If objPrescription.RxPrescriber.PrescriberPhone.Email.Trim.Length > 80 Then
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber email has too many characters.Please update the email." & vbCrLf)
                        End If
                    Else
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", objPrescription.RxPrescriber.PrescriberPhone.Email.Trim)
                        xmlwriter.WriteElementString("Qualifier", "EM")
                        xmlwriter.WriteEndElement() 'End Phone Element

                    End If
                End If


                xmlwriter.WriteEndElement() 'End CommunicationNumbers element
                _txtdata.Append(GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Fax) & ":FX'")
                _txtdata.Append(Environment.NewLine)

                xmlwriter.WriteEndElement() 'End Prescriber Element

                'added new Supervisor Tag
                If objPrescription.RxSupervisorProviderID <> 0 Then
                    'Start Supervisor Tag
                    xmlwriter.WriteStartElement("Supervisor")
                    'Start Identification Tag
                    If objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI <> "" Then
                        xmlwriter.WriteStartElement("Identification")
                        '_txtData.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                        xmlwriter.WriteElementString("NPI", objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI)
                        xmlwriter.WriteEndElement()
                    ElseIf objPrescription.RxSupervisorPrescriber.SupervisorPrescriberID <> "" Then
                        xmlwriter.WriteStartElement("Identification")
                        '_txtData.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                        xmlwriter.WriteElementString("SPI", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberID)
                        xmlwriter.WriteEndElement()
                    ElseIf objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA <> "" Then
                        xmlwriter.WriteStartElement("Identification")
                        '_txtData.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                        xmlwriter.WriteElementString("DEANumber", objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA)
                        xmlwriter.WriteEndElement()
                    End If
                    'End Identification Tag

                    'Start Name Tag
                    xmlwriter.WriteStartElement("Name")
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim <> "" Then
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim.Length > 35 Then
                            'MessageBox.Show("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ''Exit Function
                            'If icnt = 0 Then
                            '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber last name has too many characters.  Please update the first name." & vbCrLf)
                            'End If
                            xmlwriter.WriteElementString("LastName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("LastName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim)
                        End If

                    End If

                    '_txtData.Append(objPrescription.RxPrescriber.PrescriberName.LastName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.FirstName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim & "++" & objPrescription.ClinicName.Trim & "+")
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim <> "" Then
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Length > 35 Then
                            ''MessageBox.Show("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ''Exit Function
                            'If icnt = 0 Then
                            '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber first name has too many characters.  Please update the first name." & vbCrLf)
                            'End If
                            xmlwriter.WriteElementString("FirstName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("FirstName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim)
                        End If

                    End If

                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim <> "" Then
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Length > 35 Then
                            ''MessageBox.Show("This prescription request can not be sent because the prescriber middle name has too many characters.Please update the middle name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ''Exit Function
                            'If icnt = 0 Then
                            '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber middle name has too many characters.  Please update the middle name." & vbCrLf)
                            'End If
                            xmlwriter.WriteElementString("MiddleName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("MiddleName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim)
                        End If
                    End If

                    xmlwriter.WriteEndElement()
                    'End Name Tag


                    ''ADDRESS--------------- tag start
                    xmlwriter.WriteStartElement("Address")

                    ' ''ADDRESS 1
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim.Length > 35 Then
                        '    'MessageBox.Show("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    'Exit Function
                        'If icnt = 0 Then
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber address line2 has too many characters.  Please update the address line2." & vbCrLf)
                        'End If
                        xmlwriter.WriteElementString("AddressLine1", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim.Substring(0, 35))
                    Else
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim <> "" Then
                            xmlwriter.WriteElementString("AddressLine1", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim)
                        End If
                    End If

                    ''_txtData.Append(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim & ":")

                    ' ''ADDRESS 2
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim.Length > 35 Then
                        '    'MessageBox.Show("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    'Exit Function
                        'If icnt = 0 Then
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber address line2 has too many characters.  Please update the address line2." & vbCrLf)
                        'End If
                        xmlwriter.WriteElementString("AddressLine2", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim.Substring(0, 35))
                    Else
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim <> "" Then
                            xmlwriter.WriteElementString("AddressLine2", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim)
                        End If
                    End If

                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim.Length > 35 Then
                        '    'MessageBox.Show("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    'Exit Function
                        'If icnt = 0 Then
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber address line2 has too many characters.  Please update the address line2." & vbCrLf)
                        'End If
                        xmlwriter.WriteElementString("City", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim.Substring(0, 35))
                    Else
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim <> "" Then
                            xmlwriter.WriteElementString("City", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim)
                        End If
                    End If


                    ''STATE
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Trim <> "" Then
                        xmlwriter.WriteElementString("State", If(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Length > 2, objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Substring(0, 2), objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State))
                    End If


                    ''ZIP
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim <> "" Then
                        xmlwriter.WriteElementString("ZipCode", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip)
                    End If

                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim <> "" Then
                        xmlwriter.WriteElementString("PlaceLocationQualifier", "AD2")
                    End If
                    xmlwriter.WriteEndElement()
                    ''ADDRESS---------------End Address Element

                    ' _txtData.Append(objPrescription.RxPrescriber.PrescriberAddress.City & ":" & objPrescription.RxPrescriber.PrescriberAddress.State & ":" & objPrescription.RxPrescriber.PrescriberAddress.Zip & ":AD2:" & objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim & "+")


                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim <> "" Then
                        xmlwriter.WriteStartElement("CommunicationNumbers")
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim <> "" Then
                            xmlwriter.WriteStartElement("Communication")
                            xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim))
                            xmlwriter.WriteElementString("Qualifier", "TE")
                            xmlwriter.WriteEndElement() 'End Phone Element

                        End If
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax.Trim <> "" Then
                            xmlwriter.WriteStartElement("Communication")
                            xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax.Trim))
                            xmlwriter.WriteElementString("Qualifier", "FX")
                            xmlwriter.WriteEndElement() 'End Phone Element

                        End If

                        ''resolved bug 50462 in 7030 
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email.Trim <> "" Then
                            If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email.Trim.Length > 80 Then
                                If icnt = 0 Then
                                    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber email has too many characters.Please update the email." & vbCrLf)
                                End If
                            Else
                                xmlwriter.WriteStartElement("Communication")
                                xmlwriter.WriteElementString("Number", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email.Trim)
                                xmlwriter.WriteElementString("Qualifier", "EM")
                                xmlwriter.WriteEndElement() 'End Phone Element
                            End If


                        End If

                        xmlwriter.WriteEndElement()
                    End If
                    ' ''END PHONE

                    xmlwriter.WriteEndElement()
                    'END Supervisor Tag

                End If
                _txtdata.Append("PTT++" & Getdatetype(objPrescription.RxPatient.DateofBirth.Trim) & "+" & objPrescription.RxPatient.PatientName.LastName.Trim & ":" & objPrescription.RxPatient.PatientName.FirstName.Trim & "+" & GetGender(objPrescription.RxPatient.Gender.Trim) & "++")
                xmlwriter.WriteStartElement("Patient")

                Dim blnIdentificationTag As Boolean = False

                If objPrescription.RxPatient.SSN.Trim <> "" Then
                    If objPrescription.RxPatient.SSN.Trim.Length > 35 Then
                        'MessageBox.Show("This prescription request can not be sent because the patient SSN has too many characters.  Please update the SSN.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the patient SSN has too many characters.  Please update the SSN." & vbCrLf)
                        End If
                    End If
                    blnIdentificationTag = True
                End If

                If blnIdentificationTag Then
                    If objPrescription.FormularyCol.Count > 0 Then
                        If objPrescription.FormularyCol.Item(0).PatientRelationShip <> "" Then
                            xmlwriter.WriteElementString("PatientRelationship", Convert.ToInt16(objPrescription.FormularyCol.Item(0).PatientRelationShip))
                        End If
                    End If
                    xmlwriter.WriteStartElement("Identification")
                    xmlwriter.WriteElementString("SocialSecurity", objPrescription.RxPatient.SSN.Trim)
                    If objPrescription.FormularyCol.Count > 0 Then
                        If objPrescription.FormularyCol.Item(0).PBM_PayerMemberId <> "" Then
                            Dim myString As String = objPrescription.FormularyCol.Item(0).PBM_PayerMemberId.Trim()

                            If myString.Length > 35 Then
                                xmlwriter.WriteElementString("PayerID", myString.Substring(0, 35))
                                xmlwriter.WriteElementString("PayerID", Left(myString.Substring(35, Len(myString) - 35), 35))

                            Else
                                xmlwriter.WriteElementString("PayerID", myString)
                            End If
                        End If

                    End If
                    xmlwriter.WriteEndElement() 'End Identification Element
                Else
                    If objPrescription.FormularyCol.Count > 0 Then
                        If objPrescription.FormularyCol.Item(0).PBM_PayerMemberId <> "" Then
                            If objPrescription.FormularyCol.Item(0).PatientRelationShip <> "" Then
                                xmlwriter.WriteElementString("PatientRelationship", Convert.ToInt16(objPrescription.FormularyCol.Item(0).PatientRelationShip))
                            End If

                            xmlwriter.WriteStartElement("Identification")
                            'If objPrescription.FormularyCol.Item(0).PBM_PayerMemberId.Trim.Length > 35 Then
                            '    xmlwriter.WriteElementString("PayerID", objPrescription.FormularyCol.Item(0).PBM_PayerMemberId.Trim.Substring(0, 35))
                            'Else
                            '    xmlwriter.WriteElementString("PayerID", objPrescription.FormularyCol.Item(0).PBM_PayerMemberId)
                            'End If
                            Dim myString As String = objPrescription.FormularyCol.Item(0).PBM_PayerMemberId.Trim()

                            If myString.Length > 35 Then
                                xmlwriter.WriteElementString("PayerID", myString.Substring(0, 35))
                                xmlwriter.WriteElementString("PayerID", Left(myString.Substring(35, Len(myString) - 35), 35))

                            Else
                                xmlwriter.WriteElementString("PayerID", myString)
                            End If
                            xmlwriter.WriteEndElement() 'End PayerIdentification Element
                        End If

                    End If
                End If

                xmlwriter.WriteStartElement("Name")

                If objPrescription.RxPatient.PatientName.LastName.Trim.Length > 35 Then
                    xmlwriter.WriteElementString("LastName", objPrescription.RxPatient.PatientName.LastName.Trim.Substring(0, 35))
                Else
                    xmlwriter.WriteElementString("LastName", objPrescription.RxPatient.PatientName.LastName.Trim)
                End If

                If objPrescription.RxPatient.PatientName.FirstName.Trim.Length > 35 Then
                    xmlwriter.WriteElementString("FirstName", objPrescription.RxPatient.PatientName.FirstName.Trim.Substring(0, 35))
                Else
                    xmlwriter.WriteElementString("FirstName", objPrescription.RxPatient.PatientName.FirstName.Trim)
                End If



                'If objPrescription.RxPatient.PatientName.MiddleName.Trim <> "" Then
                '    If objPrescription.RxPatient.PatientName.MiddleName.Trim.Length > 35 Then
                '        'MessageBox.Show("This prescription request can not be sent because the patient middle name has too many characters.Please update the middle name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        'Exit Function
                '        'If icnt = 0 Then
                '        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient middle name has too many characters.Please update the middle name." & vbCrLf)
                '        xmlwriter.WriteElementString("MiddleName", objPrescription.RxPatient.PatientName.MiddleName.Trim.Substring(0, 35))
                '        'End If
                '    Else
                '        '' Problem 00000054 : Wrongly Written Prescriber Middle Name instead of Patient Middle Name
                '        xmlwriter.WriteElementString("MiddleName", objPrescription.RxPatient.PatientName.MiddleName.Trim)
                '    End If
                'End If
                xmlwriter.WriteEndElement() 'End Name Element
                xmlwriter.WriteElementString("Gender", GetGender(objPrescription.RxPatient.Gender.Trim))

                xmlwriter.WriteStartElement("DateOfBirth")
                xmlwriter.WriteElementString("Date", Getdatetype(objPrescription.RxPatient.DateofBirth.Trim))
                xmlwriter.WriteEndElement()

                If objPrescription.RxPatient.PatientAddress.Address1.Trim <> "" Or objPrescription.RxPatient.PatientAddress.City.Trim <> "" Or objPrescription.RxPatient.PatientAddress.State.Trim <> "" Or objPrescription.RxPatient.PatientAddress.Zip.Trim <> "" Then
                    xmlwriter.WriteStartElement("Address")
                    If objPrescription.RxPatient.PatientAddress.Address1.Trim <> "" Then
                        If objPrescription.RxPatient.PatientAddress.Address1.Trim.Length > 35 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient address line1 has too many characters.Please update the patient address line1.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            'If icnt = 0 Then
                            '    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient address line1 has too many characters.Please update the patient address line1." & vbCrLf)
                            xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPatient.PatientAddress.Address1.Trim.Substring(0, 35))
                            'End If
                        Else
                            xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPatient.PatientAddress.Address1.Trim)
                        End If
                        _txtdata.Append(objPrescription.RxPatient.PatientAddress.Address1.Trim & ":")
                    End If
                    If objPrescription.RxPatient.PatientAddress.Address2.Trim <> "" Then
                        If objPrescription.RxPatient.PatientAddress.Address2.Trim.Length > 35 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient address line2 has too many characters.Please update the patient address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            'If icnt = 0 Then
                            '    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient address line2 has too many characters.Please update the patient address line2." & vbCrLf)
                            'End If
                            xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPatient.PatientAddress.Address2.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPatient.PatientAddress.Address2.Trim)
                        End If
                    End If
                    If objPrescription.RxPatient.PatientAddress.City.Trim <> "" Then
                        If objPrescription.RxPatient.PatientAddress.City.Trim.Length > 35 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient city has too many characters.Please update the patient address line1.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            'If icnt = 0 Then
                            '    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient city has too many characters.Please update the patient city." & vbCrLf)
                            'End If
                            xmlwriter.WriteElementString("City", objPrescription.RxPatient.PatientAddress.City.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("City", objPrescription.RxPatient.PatientAddress.City.Trim)
                        End If
                        _txtdata.Append(objPrescription.RxPatient.PatientAddress.City.Trim & ":")
                    End If
                    If objPrescription.RxPatient.PatientAddress.State.Trim <> "" Then
                        If objPrescription.RxPatient.PatientAddress.State.Length > 2 Then
                            xmlwriter.WriteElementString("State", objPrescription.RxPatient.PatientAddress.State.Substring(0, 2))
                        Else
                            xmlwriter.WriteElementString("State", objPrescription.RxPatient.PatientAddress.State.Trim)
                        End If
                        _txtdata.Append(objPrescription.RxPatient.PatientAddress.State.Trim & ":")
                    End If
                    If objPrescription.RxPatient.PatientAddress.Zip.Trim <> "" Then
                        Dim objZipCode As New System.Text.RegularExpressions.Regex("(\d{5})|(\d{9})")
                        If objZipCode.Match(objPrescription.RxPatient.PatientAddress.Zip.Trim).Success = False Then
                            'MessageBox.Show("This prescription request can not be sent because the patient zipcode has too many characters.Please update the patient zipcode.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            'If icnt = 0 Then
                            '    ''ValidationMessageBuilder.Append("This prescription request can not be sent because the patient zipcode has too many characters.Please update the patient zipcode." & vbCrLf)
                            '    ValidationMessageBuilder.Append("Patient zipcode is invalid or has too many characters and will not be sent in the prescription request." & vbCrLf)
                            'End If
                        Else
                            If objPrescription.RxPatient.PatientAddress.Zip.Trim.Length > 11 Then
                                xmlwriter.WriteElementString("ZipCode", objPrescription.RxPatient.PatientAddress.Zip.Trim.Substring(0, 11))
                            Else
                                xmlwriter.WriteElementString("ZipCode", objPrescription.RxPatient.PatientAddress.Zip.Trim)
                            End If
                        End If
                        _txtdata.Append(objPrescription.RxPatient.PatientAddress.Zip.Trim & "+")
                    End If

                    If objPrescription.RxPatient.PatientAddress.Address2.Trim <> "" Then
                        xmlwriter.WriteElementString("PlaceLocationQualifier", "AD2")
                    End If
                    xmlwriter.WriteEndElement() 'End Address Element
                End If


                If objPrescription.RxPatient.PatientPhone.Phone.Trim <> "" Or objPrescription.RxPatient.PatientPhone.Fax.Trim <> "" Or objPrescription.RxPatient.PatientWorkPhone.Phone.Trim <> "" Then
                    If objPrescription.RxPatient.PatientPhone.Phone.Trim <> "" Then
                        If GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone).Trim.Length > 25 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient phone has too many characters.Please update the patient phone.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient phone has too many characters.Please update the patient phone." & vbCrLf)
                            End If
                        End If
                    End If
                    If objPrescription.RxPatient.PatientPhone.Fax.Trim <> "" Then
                        If GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Fax.Trim).Length > 25 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient fax number has too many characters.Please update the patient fax number.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient fax number has too many characters.Please update the patient fax number." & vbCrLf)
                            End If
                        End If
                    End If
                    If objPrescription.RxPatient.PatientWorkPhone.Phone.Trim <> "" Then
                        If GetPhoneNumber(objPrescription.RxPatient.PatientWorkPhone.Phone).Trim.Length > 25 Then
                            'MessageBox.Show("This prescription request can not be sent because the patient Work phone has too many characters.Please update the patient Work phone.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient Work phone has too many characters.Please update the patient Work phone." & vbCrLf)
                            End If
                        End If
                    End If
                    xmlwriter.WriteStartElement("CommunicationNumbers")
                    If objPrescription.RxPatient.PatientPhone.Phone.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone.Trim))
                        xmlwriter.WriteElementString("Qualifier", "TE")
                        xmlwriter.WriteEndElement() 'End Phone Element
                        _txtdata.Append(GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone.Trim) & ":TE'")
                        _txtdata.Append(Environment.NewLine)
                    End If

                    If objPrescription.RxPatient.PatientPhone.Fax.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Fax.Trim))
                        xmlwriter.WriteElementString("Qualifier", "FX")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If

                    If objPrescription.RxPatient.PatientWorkPhone.Phone.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientWorkPhone.Phone.Trim))
                        xmlwriter.WriteElementString("Qualifier", "WP")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If


                    ''resolved bug 50462 in 7030
                    If objPrescription.RxPatient.PatientPhone.Email.Trim <> "" Then
                        If objPrescription.RxPatient.PatientPhone.Email.Trim.Length > 80 Then
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient email has too many characters.Please update the patient email." & vbCrLf)
                            End If
                        Else
                            xmlwriter.WriteStartElement("Communication")
                            xmlwriter.WriteElementString("Number", objPrescription.RxPatient.PatientPhone.Email.Trim)
                            xmlwriter.WriteElementString("Qualifier", "EM")
                            xmlwriter.WriteEndElement() 'End Phone Element
                        End If
                    End If

                    xmlwriter.WriteEndElement() 'End PhoneNumbers element

                End If

                xmlwriter.WriteEndElement() 'End Patient Element

                xmlwriter.WriteStartElement("MedicationPrescribed")

                ''shelly issue in SS 10.6 Glostream Issues List 7-17-2013, Your Drug Description contained a trailing space: "BACTROBAN 2% OINTMENT  "
                Dim sDrgDesc As String = objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim
                xmlwriter.WriteElementString("DrugDescription", sDrgDesc.Trim)

                _txtdata.Append("DRU+P:" & objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).Drugform.Trim & ":" & oDrug.ProdCode & ":" & oDrug.ProdCodeQualifier & "::::" & oDrug.DDID & ":MD+")

                xmlwriter.WriteStartElement("DrugCoded") 'Drugcoded element starts
                If oDrug.ProdCode.Trim.Length > 0 Then
                    xmlwriter.WriteElementString("ProductCode", oDrug.ProdCode)
                End If
                If oDrug.ProdCodeQualifier.Trim.Length > 0 Then
                    xmlwriter.WriteElementString("ProductCodeQualifier", oDrug.ProdCodeQualifier)
                End If
                xmlwriter.WriteElementString("DrugDBCode", "DDID") ''''for 10.6 MU2 Send what we sent in 8.1 for this tag, as per discussion.
                xmlwriter.WriteElementString("DrugDBCodeQualifier", "MD") ''''keep it MD, refer the <\\GLOSVR01\gloDocuments\gloSuite 7031\WEEK 0 - REQ - DRAFT_PRD\gloEMR\Estimation for Supply Drug in Refill Request and Surescripts MU2 Changes - V1.xlsx>
                xmlwriter.WriteEndElement() 'DrugCoded Element Ends



                xmlwriter.WriteStartElement("Quantity")
                'xmlwriter.WriteElementString("Qualifier", oDrug.DrugQuantityQualifier) 'objPrescription.DrugsCol.Item(RefillItem).DrugQuantityQualifier) 
                If objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim.Length > 11 Then
                    xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim.Substring(0, 11)))
                Else
                    xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim))
                End If

                xmlwriter.WriteElementString("CodeListQualifier", "38")
                xmlwriter.WriteElementString("UnitSourceCode", "AC")
                xmlwriter.WriteElementString("PotencyUnitCode", objPrescription.DrugsCol.Item(RefillItem).PotencyCode.Trim)
                ' xmlwriter.WriteElementString("Qualifier", "38") ''''''from PRN imple guide CodeList
                xmlwriter.WriteEndElement() 'End Quantity Element

                'Try
                '    If Not IsNothing(oDrug) Then
                '        If oDrug.DrugDuration.Trim.Length > 0 AndAlso oDrug.DrugDuration.Trim.Length <= 3 Then
                '            If IsNumeric(oDrug.DrugDuration) Then
                '                xmlwriter.WriteElementString("DaysSupply", Val(oDrug.DrugDuration.Trim))
                '            End If
                '        End If
                '    End If
                'Catch ex As Exception

                'End Try
                Dim nDaysSupply As Integer = 0
                If oDrug.DrugDuration.Trim.Length > 0 AndAlso Val(oDrug.DrugDuration) <> 0 Then
                    If IsNumeric(oDrug.DrugDuration) Then
                        nDaysSupply = Val(oDrug.DrugDuration)
                    Else
                        Dim nDuration As String() = Nothing
                        Dim numberofDays As Integer
                        nDuration = oDrug.DrugDuration.Trim.Split(" ")
                        If nDuration.Length > 0 Then
                            Select Case nDuration(1).ToUpper
                                Case "MONTHS"
                                    numberofDays = 30
                                Case "DAYS"
                                    numberofDays = 1
                                Case "WEEKS"
                                    numberofDays = 7
                            End Select
                            nDaysSupply = numberofDays * CType(nDuration(0), Integer)
                        End If
                    End If

                    xmlwriter.WriteElementString("DaysSupply", nDaysSupply)
                End If


                If objPrescription.DrugsCol.Item(RefillItem).Directions.Trim.Length > 140 Then
                    xmlwriter.WriteElementString("Directions", objPrescription.DrugsCol.Item(RefillItem).Directions.Trim.Substring(0, 140))
                Else
                    xmlwriter.WriteElementString("Directions", objPrescription.DrugsCol.Item(RefillItem).Directions.Trim)
                End If


                If Not IsNothing(oDrug.Notes) Then
                    If oDrug.Notes.Trim.Length > 0 Then
                        If oDrug.Notes.Trim.Length > 210 Then
                            'MessageBox.Show("This prescription request can not be sent because the drug notes has too many characters.Please update the drug notes.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Exit Function
                            'ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug notes has too many characters for " & oDrug.DrugName & ".Please update the drug notes." & vbCrLf)
                            xmlwriter.WriteElementString("Note", oDrug.Notes.Trim.Substring(0, 210))
                        Else
                            xmlwriter.WriteElementString("Note", oDrug.Notes.Trim.Replace(Environment.NewLine, " "))
                        End If

                    End If
                End If

                _txtdata.Append(Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate.Trim) & ":102+0+R:1'")
                _txtdata.Append(Environment.NewLine)

                xmlwriter.WriteStartElement("Refills")
                If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier.Trim.Length <= 0 Then
                    xmlwriter.WriteElementString("Qualifier", "R")
                Else
                    If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier = "P" Then
                        xmlwriter.WriteElementString("Qualifier", "R")
                    Else
                        xmlwriter.WriteElementString("Qualifier", objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier.Trim)
                    End If

                End If

                If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier = "R" Or objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier = "P" Then
                    If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim <> "" Then
                        If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim.Length > 2 Then
                            xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim.Substring(0, 2)))
                        Else
                            xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim))
                        End If
                    End If                    'Else
                    '    If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim <> "" Then
                    '        If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim.Length > 2 Then
                    '            xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim.Substring(0, 2)))
                    '        Else
                    '            xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim))
                    '        End If
                    '    End If
                End If
                xmlwriter.WriteEndElement() 'End Refills Element
                'If SendMessageType = "NewRx" Then
                '    If objPrescription.DrugsCol.Item(RefillItem).MaySubstitute Then
                '        xmlwriter.WriteElementString("Substitutions", "1")
                '    Else
                '        xmlwriter.WriteElementString("Substitutions", "0")
                '    End If
                'Else
                If objPrescription.DrugsCol.Item(RefillItem).MaySubstitute Then
                    xmlwriter.WriteElementString("Substitutions", "0")
                Else
                    xmlwriter.WriteElementString("Substitutions", "1")
                End If
                ' End If


                xmlwriter.WriteStartElement("WrittenDate")
                If objPrescription.DrugsCol.Item(RefillItem).WrittenDate <> Now.Date.ToString Then
                    xmlwriter.WriteElementString("Date", Getdatetype(Now.Date))
                Else
                    xmlwriter.WriteElementString("Date", Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate.Trim))
                End If
                xmlwriter.WriteEndElement() 'End Writtendate Element
                xmlwriter.WriteEndElement() 'End MedicationPrescribed Element

                If objPrescription.FormularyCol.Count > 0 Then
                    xmlwriter.WriteStartElement("BenefitsCoordination")

                    xmlwriter.WriteStartElement("PayerIdentification")
                    If objPrescription.FormularyCol.Item(0).PBMPayerParticipantId <> "" Then
                        'xmlwriter.WriteElementString("PayerID", objPrescription.FormularyCol.Item(0).PBMPayerParticipantId)
                        If objPrescription.FormularyCol.Item(0).PBMPayerParticipantId.Trim.Length > 35 Then
                            xmlwriter.WriteElementString("PayerID", objPrescription.FormularyCol.Item(0).PBMPayerParticipantId.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("PayerID", objPrescription.FormularyCol.Item(0).PBMPayerParticipantId)
                        End If
                    End If


                    If objPrescription.FormularyCol.Item(0).BINPCNNumber <> "" Then
                        Dim _strBINPCN As String() = Split(objPrescription.FormularyCol.Item(0).BINPCNNumber, "/")
                        Dim _BIN As String = ""
                        Dim _PCN As String = ""

                        If _strBINPCN.Length = 1 Then
                            _BIN = _strBINPCN(0)
                        ElseIf _strBINPCN.Length > 1 Then
                            _BIN = _strBINPCN(0)
                            _PCN = _strBINPCN(1)
                        End If

                        If _BIN <> "" Then
                            xmlwriter.WriteElementString("BINLocationNumber", _BIN)
                        End If
                        If _PCN <> "" Then
                            xmlwriter.WriteElementString("BINLocationNumber", _PCN)
                        End If

                    End If

                    If objPrescription.FormularyCol.Item(0).ISAControlNumber <> "" Then
                        xmlwriter.WriteElementString("MutuallyDefined", objPrescription.FormularyCol.Item(0).ISAControlNumber)
                    End If
                    xmlwriter.WriteEndElement() 'End PayerIdentification Element

                    '<PayerName>AMERICARE INSURANCE</PayerName>
                    If objPrescription.FormularyCol.Item(0).PBMPayerName <> "" Then
                        ' xmlwriter.WriteElementString("PayerName", objPrescription.FormularyCol.Item(0).PBMPayerName)
                        If objPrescription.FormularyCol.Item(0).PBMPayerName.Trim.Length > 35 Then
                            xmlwriter.WriteElementString("PayerName", objPrescription.FormularyCol.Item(0).PBMPayerName.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("PayerName", objPrescription.FormularyCol.Item(0).PBMPayerName)
                        End If
                    End If

                    '<CardholderID>MemberId</CardholderID>
                    If objPrescription.FormularyCol.Item(0).CardHolderId <> "" Then
                        '  xmlwriter.WriteElementString("CardholderID", objPrescription.FormularyCol.Item(0).CardHolderId)
                        If objPrescription.FormularyCol.Item(0).CardHolderId.Trim.Length > 35 Then
                            xmlwriter.WriteElementString("CardholderID", objPrescription.FormularyCol.Item(0).CardHolderId.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("CardholderID", objPrescription.FormularyCol.Item(0).CardHolderId)
                        End If
                    End If

                    '<CardHolderName>
                    '	<LastName>JAMES</LastName>
                    '	<FirstName>TINA</FirstName>
                    '</CardHolderName>
                    If objPrescription.FormularyCol.Item(0).SubscriberFirstName <> "" And objPrescription.FormularyCol.Item(0).SubscriberLastName <> "" Then
                        xmlwriter.WriteStartElement("CardHolderName")
                        'xmlwriter.WriteElementString("LastName", objPrescription.FormularyCol.Item(0).SubscriberLastName)
                        'xmlwriter.WriteElementString("FirstName", objPrescription.FormularyCol.Item(0).SubscriberFirstName)
                        If objPrescription.FormularyCol.Item(0).SubscriberLastName.Trim.Length > 35 Then
                            xmlwriter.WriteElementString("LastName", objPrescription.FormularyCol.Item(0).SubscriberLastName.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("LastName", objPrescription.FormularyCol.Item(0).SubscriberLastName)
                        End If
                        If objPrescription.FormularyCol.Item(0).SubscriberFirstName.Trim.Length > 35 Then
                            xmlwriter.WriteElementString("FirstName", objPrescription.FormularyCol.Item(0).SubscriberFirstName.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("FirstName", objPrescription.FormularyCol.Item(0).SubscriberFirstName)
                        End If
                        xmlwriter.WriteEndElement() 'End CardHolderName Element
                    End If

                    '<GroupID>Group Number</GroupID>
                    If objPrescription.FormularyCol.Item(0).GroupId <> "" Then
                        ' xmlwriter.WriteElementString("GroupID", objPrescription.FormularyCol.Item(0).GroupId)
                        If objPrescription.FormularyCol.Item(0).GroupId.Trim.Length > 35 Then
                            xmlwriter.WriteElementString("GroupID", objPrescription.FormularyCol.Item(0).GroupId.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("GroupID", objPrescription.FormularyCol.Item(0).GroupId)
                        End If
                    End If

                    xmlwriter.WriteEndElement() 'End BenefitsCoordination Element
                End If
                XmlWriter.WriteEndElement() 'End NewRx element
                XmlWriter.WriteEndElement() 'End Body Element
                XmlWriter.WriteEndElement() 'End Message Element
                XmlWriter.WriteEndDocument()
                XmlWriter.Close()

                _txtdata.Append("UIT+" & objPrescription.RxTransactionID & "+9'")
                _txtdata.Append(Environment.NewLine)
                _txtdata.Append("UIZ++1'")
                _txtdata.Append(Environment.NewLine)


                Dim NoOfAttempt As Integer = 1
                Dim _isWithOutInternet As Boolean = False

                Dim result As Byte() = Nothing

                While (NoOfAttempt <= 5)
                    If _isWithOutInternet = True Then
                        Exit While
                    End If

                    result = gloSurescriptGeneral.PostSurescriptMessage(XMLFilePath, objPrescription, _isWithOutInternet)

                    If Not IsNothing(result) Then
                        Exit While
                    Else
                        NoOfAttempt = NoOfAttempt + 1
                    End If

                End While

                If Not IsNothing(result) Then
                    ReadStatusMessageRevised(result, SentMessageType.eNewRx, objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim)
                    My.Computer.FileSystem.WriteAllText(_txtFilePath, _txtdata.ToString(), False)

                    NewRxEDIFACTFileData = _txtdata ''''assign the EDIfact file data to the glosurescript interface so that we can acces it on clsgloemrprescription class to save this info in database
                End If

                _txtdata = Nothing
                Return True

            Catch ex As gloSurescriptDBException
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                Return False
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                Return False
            Catch ex As Exception
                Generate10dot6XMLforNewRx = Nothing
                Throw New GloSurescriptException(ex.Message)
            Finally
                If Not IsNothing(_txtdata) Then
                    _txtdata = Nothing
                End If
                If Not IsNothing(XmlWriter) Then
                    XmlWriter.Close()
                    XmlWriter = Nothing
                End If
            End Try
        Else
            Generate10dot6XMLforNewRx = Nothing
        End If
        'If ValidationMessageforDrug.Length > 0 Then
        '    ValidationMessageforDrug = ValidationMessageforDrug & vbCrLf & ValidationMessageBuilderforDrug.ToString
        'Else
        '    ValidationMessageforDrug = ValidationMessageBuilderforDrug.ToString

        'End If
    End Function


    Public Function GenerateMultipleMU210dot6XMLforNewRx(ByVal objPrescription As EPrescription, ByVal RefillItem As Int16, ByVal icnt As Int32, Optional ByVal SendMessageType As String = "NewRx", Optional ByVal sRelatestoMessageID As String = "", Optional ByVal sRxReffNumber As String = "") As String

        Dim strfilepath As String = GenerateFileName(MessageType.eNewRx)
        XMLFilePath = strfilepath
        Dim xmlwriter As XmlTextWriter = Nothing
        Dim _txtdata As System.Text.StringBuilder = Nothing

        Try
            If File.Exists(strfilepath) Then
                File.Delete(strfilepath)
            End If
            Dim oDrug As EDrug
            oDrug = objPrescription.DrugsCol.Item(RefillItem)

            Dim dtdate As DateTime = Date.UtcNow
            Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
            Dim strtime As String = Format(dtdate, "hh:mm:ss")
            Dim strmillisec As String = Format(dtdate, "mmm")
            Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

            Dim _txtFilePath = strfilepath.Replace(".xml", ".txt")
            _txtdata = New System.Text.StringBuilder
            Dim _segmentCount As Int32 = 1
            _txtdata.Append("UNA:+\ *'")
            _txtdata.Append(Environment.NewLine)



            'Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
            'strUTCFormat = strdate & "T" & strtime & ".0Z"
            Dim strUTCFormat As String = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.FZ")
            xmlwriter = New XmlTextWriter(strfilepath, Nothing)
            oDrug.MessageName = "NewRx"
            oDrug.MessageID = "NewRx" & strtemp

            oDrug.DateTimeStamp = strUTCFormat
            oDrug.DateReceived = Now.Date
            'oDrug.MessageFrom = "mailto:" & objPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
            'oDrug.MessageTo = "mailto:" & objPrescription.RxPharmacy.PharmacyID & ".ncpdp@surescripts.com"
            oDrug.MessageFrom = objPrescription.RxPrescriber.PrescriberID
            oDrug.MessageTo = oDrug.PhNCPDPID


            xmlwriter.WriteStartDocument()
            xmlwriter.WriteStartElement("Message") 'Open the Main Parent Node 
            xmlwriter.WriteAttributeString("xmlns", "http://www.ncpdp.org/schema/SCRIPT")

            ''added new tags in 10.6 

            xmlwriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            xmlwriter.WriteAttributeString("version", "010")
            xmlwriter.WriteAttributeString("release", "006")
            xmlwriter.WriteAttributeString("xsi:schemaLocation", "http://www.ncpdp.org/schema/SCRIPT SS_SCRIPT_XML_10_6MU.xsd")

            xmlwriter.WriteStartElement("Header")

            ' xmlwriter.WriteElementString("To", oDrug.MessageTo) 'The Child 
            xmlwriter.WriteStartElement("To")
            xmlwriter.WriteAttributeString("Qualifier", "P")
            xmlwriter.WriteString(oDrug.MessageTo)
            xmlwriter.WriteEndElement()

            'xmlwriter.WriteElementString("From", oDrug.MessageFrom) 'The Child 
            xmlwriter.WriteStartElement("From")
            xmlwriter.WriteAttributeString("Qualifier", "D")
            xmlwriter.WriteString(oDrug.MessageFrom)
            xmlwriter.WriteEndElement()

            Dim strUTCFormat1 As String = strUTCFormat.Replace("-", "").Replace(":", "")
            strUTCFormat1 = strUTCFormat1.Replace("T", ":").Replace(".0Z", "")

            _txtdata.Append("UIB+UNOA:0++")
            xmlwriter.WriteElementString("MessageID", oDrug.MessageID) 'The Child 
            _txtdata.Append(oDrug.MessageID)
            _txtdata.Append("+++" & objPrescription.RxPrescriber.PrescriberID & ":D+" & oDrug.PhNCPDPID & ":P+" & strUTCFormat1 & ",0'")
            _txtdata.Append(Environment.NewLine)

            '' Commented Not Used for Normal erX Date: 27-03-2015
            'If sRelatestoMessageID <> "" Then 
            '    xmlwriter.WriteElementString("RelatesToMessageID", oDrug.RelatesToMessageId)
            'End If

            If SendMessageType = "eDeniedWithNewRxToFollow" And sRelatestoMessageID <> "" Then
                xmlwriter.WriteElementString("RelatesToMessageID", sRelatestoMessageID) '' This Message id is Update after Denied Responce Message ID will get (DNTF)
            End If

            'RelatesToMessageID is not available for this message
            xmlwriter.WriteElementString("SentTime", strUTCFormat) 'The Child 


            'new tag for 10.6
            'xmlwriter.WriteStartElement("SenderSoftware")
            'Dim strVersion As String = My.Application.Info.Version.ToString.Substring(0, 3)
            'Dim strProductName As String = My.Application.Info.ProductName.ToString()
            'Dim strCompany As String = My.Application.Info.CompanyName
            'xmlwriter.WriteElementString("SenderSoftwareDeveloper", strCompany) 'The Child 
            'xmlwriter.WriteElementString("SenderSoftwareProduct", strProductName)
            'xmlwriter.WriteElementString("SenderSoftwareVersionRelease", strVersion)
            'xmlwriter.WriteEndElement()
            ''end of new tag
            'If oDrug.RxReferenceNumber.Trim <> "" Then
            '    xmlwriter.WriteElementString("RxReferenceNumber", oDrug.RxReferenceNumber)
            'End If
            If sRxReffNumber <> "" Then
                xmlwriter.WriteElementString("RxReferenceNumber", sRxReffNumber)
            End If


            If oDrug.PrescriptionID <> "" And oDrug.PrescriptionID <> "0" Then
                xmlwriter.WriteElementString("PrescriberOrderNumber", oDrug.PrescriptionID)
            Else
                Dim nPrescOrdNo As Long = gloSurescriptGeneral.GetUniqueID()
                xmlwriter.WriteElementString("PrescriberOrderNumber", nPrescOrdNo)
            End If

            xmlwriter.WriteEndElement() 'End Header Element

            xmlwriter.WriteStartElement("Body")

            xmlwriter.WriteStartElement("NewRx")


            _txtdata.Append("UIH+SCRIPT:008:001:NEWRX+" & oDrug.PrescriptionID & "+" & oDrug.PrescriptionID & "'")
            _txtdata.Append(Environment.NewLine)


            ''*****************************************************PHARMACY SECTION START*******************************************************************

            xmlwriter.WriteStartElement("Pharmacy")

            xmlwriter.WriteStartElement("Identification")

            _txtdata.Append("PVD+P2+" & oDrug.PhNCPDPID & ":D3+++++" & oDrug.PharmacyName.Trim & "+:")
            xmlwriter.WriteElementString("NCPDPID", oDrug.PhNCPDPID.Trim)
            If Not IsNothing(oDrug.PhNPI) Then
                If oDrug.PhNPI.Trim <> "" Then
                    xmlwriter.WriteElementString("NPI", oDrug.PhNPI.Trim)
                End If
            End If

            xmlwriter.WriteEndElement() 'End Identification Element
            If Not IsNothing(oDrug.PharmacyName) Then
                If oDrug.PharmacyName.Trim.Length > 35 Then
                    xmlwriter.WriteElementString("StoreName", oDrug.PharmacyName.Trim.Substring(0, 35))
                Else
                    xmlwriter.WriteElementString("StoreName", oDrug.PharmacyName.Trim)
                End If
            Else
                xmlwriter.WriteElementString("StoreName", "")
            End If


            If oDrug.PhAddressline1.Trim <> "" Or oDrug.PhAddressline2.Trim <> "" Or oDrug.PhCity.Trim <> "" Or oDrug.PhState.Trim <> "" Or oDrug.PhZip.Trim <> "" Then
                xmlwriter.WriteStartElement("Address")

                If oDrug.PhAddressline1.Trim.Length > 35 Then
                    'If icnt = 0 Then
                    '    'MessageBox.Show("This prescription request can not be sent because the pharmacy address has too many characters.  Please update the address so it has less than 35 characters. ", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    'Exit Function
                    '    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy address line1 has too many characters." & vbCrLf) 'Please update the address so it has less than 35 characters.
                    'End If
                    xmlwriter.WriteElementString("AddressLine1", oDrug.PhAddressline1.Trim.Substring(0, 35))
                    _txtdata.Append(oDrug.PhAddressline1.Trim.Substring(0, 35) & ":")
                Else
                    If oDrug.PhAddressline1.Trim <> "" Then
                        xmlwriter.WriteElementString("AddressLine1", oDrug.PhAddressline1.Trim)
                    End If
                End If

                If oDrug.PhAddressline2.Trim.Length > 35 Then
                    'If icnt = 0 Then
                    '    'MessageBox.Show("This prescription request can not be sent because the pharmacy address has too many characters.  Please update the address so it has less than 35 characters.  ", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    'Exit Function
                    '    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy address line2 has too many characters." & vbCrLf) '  Please update the address so it has less than 35 characters.  
                    'End If
                    xmlwriter.WriteElementString("AddressLine2", oDrug.PhAddressline2.Trim.Substring(0, 35))
                Else
                    If oDrug.PhAddressline2.Trim <> "" Then
                        xmlwriter.WriteElementString("AddressLine2", oDrug.PhAddressline2.Trim)
                    End If

                End If

                If oDrug.PhCity.Trim.Length > 35 Then
                    'If icnt = 0 Then
                    '    'MessageBox.Show("This prescription request can not be sent because the pharmacy city name has too many characters.  Please update the city so it has less than 35 characters.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    'Exit Function
                    '    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy city name has too many characters." & vbCrLf) '  Please update the city so it has less than 35 characters.
                    'End If
                    xmlwriter.WriteElementString("City", oDrug.PhCity.Trim.Substring(0, 35))
                Else
                    If oDrug.PhCity.Trim <> "" Then
                        xmlwriter.WriteElementString("City", oDrug.PhCity.Trim)
                    End If
                End If
                _txtdata.Append(oDrug.PhCity.Trim & ":")
                If oDrug.PhState.Trim <> "" Then
                    If oDrug.PhState.Trim.Length > 2 Then
                        xmlwriter.WriteElementString("State", oDrug.PhState.Substring(0, 2))
                    Else
                        xmlwriter.WriteElementString("State", oDrug.PhState.Trim)
                    End If
                    _txtdata.Append(objPrescription.RxPharmacy.PharmacyAddress.State.Trim & ":")
                End If

                If oDrug.PhZip.Trim <> "" Then
                    Dim objZipCode As New System.Text.RegularExpressions.Regex("(\d{5})|(\d{9})")
                    If objZipCode.Match(oDrug.PhZip.Trim).Success = False Then
                        'MessageBox.Show("This prescription request can not be sent because the pharmacy zip code has too many characters.Please update the zip code.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy zip code has too many characters." & vbCrLf) 'Please update the zip code.
                        End If
                    Else
                        If oDrug.PhZip.Trim <> "" Then
                            xmlwriter.WriteElementString("ZipCode", oDrug.PhZip.Trim)
                        End If
                    End If
                    _txtdata.Append(oDrug.PhZip.Trim)
                End If

                If oDrug.PhAddressline2.Trim <> "" Then
                    xmlwriter.WriteElementString("PlaceLocationQualifier", "AD2")
                End If

                xmlwriter.WriteEndElement() 'End Address Element
            End If

            'Resolve Incident #88659: 00053537: eRx CommunicationNumber missing 
            'validation Message added
            If oDrug.PhPhone.Trim <> "" Or oDrug.PhFax.Trim <> "" Then

                If oDrug.PhPhone.Trim <> "" Then
                    If GetPhoneNumber(oDrug.PhPhone.Trim).Trim.Length > 25 Then
                        'MessageBox.Show("This prescription request can not be sent because the pharmacy phone number has too many characters.  Please update the phone number.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy phone number has too many characters." & vbCrLf) '  Please update the phone number.
                        End If
                    End If
                End If
                If oDrug.PhFax.Trim <> "" Then
                    If GetPhoneNumber(oDrug.PhFax.Trim).Trim.Length > 25 Then
                        'MessageBox.Show("This prescription request can not be sent because the pharmacy fax has too many characters.  Please update the fax number.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy fax has too many characters." & vbCrLf) '  Please update the fax number.
                        End If
                    End If
                End If


                ''code changed to fix case Incident #00025992 . 
                ''if we have Telephone (TE) then only create <communicationNumbers> tag. "Prescription Routing 10.6 IG 2012-12-03.pdf" pg 171
                If oDrug.PhPhone.Trim <> "" Then
                    xmlwriter.WriteStartElement("CommunicationNumbers")
                    If oDrug.PhPhone.Trim <> "" Then


                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(oDrug.PhPhone.Trim))
                        xmlwriter.WriteElementString("Qualifier", "TE")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If
                    _txtdata.Append("+" & GetPhoneNumber(oDrug.PhPhone.Trim) & ":TE*")
                    If oDrug.PhFax.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(oDrug.PhFax.Trim))
                        xmlwriter.WriteElementString("Qualifier", "FX")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If

                    ''resolved bug 50462 in 7030 
                    If oDrug.PhEmail.Trim <> "" Then
                        If oDrug.PhEmail.Trim.Length > 80 Then
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy email has too many characters." & vbCrLf) 'Please update the email.
                            End If
                        Else
                            xmlwriter.WriteStartElement("Communication")
                            xmlwriter.WriteElementString("Number", oDrug.PhEmail.Trim)
                            xmlwriter.WriteElementString("Qualifier", "EM")
                            xmlwriter.WriteEndElement() 'End Phone Element
                        End If
                    End If

                    xmlwriter.WriteEndElement() 'End CommunicationNumbers element
                    _txtdata.Append(GetPhoneNumber(oDrug.PhFax.Trim) & ":FX'")
                    _txtdata.Append(Environment.NewLine)
                Else
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy phone number is missing.Please update the phone number" & vbCrLf) '  Please update the phone number.
                End If

            Else
                ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy phone number is missing.Please update the phone number" & vbCrLf) '  Please update the phone number.

            End If

            xmlwriter.WriteEndElement() 'End Pharmacy Element
            ''*****************************************************PHARMACY SECTION END*******************************************************************

            ''*****************************************************PRESCRIBER SECTION END*******************************************************************
            'start <Prescriber> Tag
            xmlwriter.WriteStartElement("Prescriber")

            'start <Identification> sub element Tag
            xmlwriter.WriteStartElement("Identification")
            _txtdata.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
            If objPrescription.RxPrescriber.PrescriberDEA <> "" Then
                xmlwriter.WriteElementString("DEANumber", objPrescription.RxPrescriber.PrescriberDEA)
            End If
            'If objPrescription.RxPrescriber.PrescriberSSN <> "" Then
            '    xmlwriter.WriteElementString("SocialSecurity", objPrescription.RxPrescriber.PrescriberSSN)
            'End If

            xmlwriter.WriteElementString("NPI", objPrescription.RxPrescriber.PrescriberNPI)

            xmlwriter.WriteEndElement()
            'End <Identification> sub element Tag

            If objPrescription.ClinicName.Length > 35 Then
                xmlwriter.WriteElementString("ClinicName", objPrescription.ClinicName.Substring(0, 35))
            Else
                xmlwriter.WriteElementString("ClinicName", objPrescription.ClinicName.Trim)
            End If
            'xmlwriter.WriteElementString("ClinicName", objPrescription.ClinicName.Trim)

            'start <Name> sub element Tag
            xmlwriter.WriteStartElement("Name")
            If objPrescription.RxPrescriber.PrescriberName.LastName.Trim.Length > 35 Then
                xmlwriter.WriteElementString("LastName", objPrescription.RxPrescriber.PrescriberName.LastName.Trim.Substring(0, 35))
                _txtdata.Append(objPrescription.RxPrescriber.PrescriberName.LastName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.FirstName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim & "++" & objPrescription.ClinicName.Trim & "+")

            Else
                xmlwriter.WriteElementString("LastName", objPrescription.RxPrescriber.PrescriberName.LastName.Trim)
                _txtdata.Append(objPrescription.RxPrescriber.PrescriberName.LastName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.FirstName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim & "++" & objPrescription.ClinicName.Trim & "+")
            End If
            If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim <> "" Then
                If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Length > 35 Then
                    'MessageBox.Show("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                    'If icnt = 0 Then
                    '    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name." & vbCrLf)
                    'End If
                    xmlwriter.WriteElementString("FirstName", objPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Substring(0, 35))
                Else
                    xmlwriter.WriteElementString("FirstName", objPrescription.RxPrescriber.PrescriberName.FirstName.Trim)
                End If

            End If

            If objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim <> "" Then
                If objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim.Length > 35 Then
                    xmlwriter.WriteElementString("MiddleName", objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim.Substring(0, 35))
                Else
                    xmlwriter.WriteElementString("MiddleName", objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim)
                End If

            End If


            xmlwriter.WriteEndElement() 'End Name Element
            'End <Name> sub element Tag


            xmlwriter.WriteStartElement("Address")
            If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Length > 35 Then
                xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Substring(0, 35))
                _txtdata.Append(objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Substring(0, 35) & ":")
            Else
                xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim)
                _txtdata.Append(objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim & ":")
            End If
            If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Length > 35 Then
                'MessageBox.Show("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
                'If icnt = 0 Then
                '    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2." & vbCrLf)
                'End If
                xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Substring(0, 35))
            Else
                If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim <> "" Then
                    xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim)
                End If
            End If

            If objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Length > 35 Then
                xmlwriter.WriteElementString("City", objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Substring(0, 35))
            Else
                xmlwriter.WriteElementString("City", objPrescription.RxPrescriber.PrescriberAddress.City)
            End If

            If objPrescription.RxPrescriber.PrescriberAddress.State.Trim.Length > 2 Then
                xmlwriter.WriteElementString("State", objPrescription.RxPrescriber.PrescriberAddress.State.Substring(0, 2))
            Else
                xmlwriter.WriteElementString("State", objPrescription.RxPrescriber.PrescriberAddress.State)
            End If

            If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim.Length > 11 Then
                xmlwriter.WriteElementString("ZipCode", objPrescription.RxPrescriber.PrescriberAddress.Zip.Substring(0, 11))
            Else
                xmlwriter.WriteElementString("ZipCode", objPrescription.RxPrescriber.PrescriberAddress.Zip)
            End If

            If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim <> "" Then
                xmlwriter.WriteElementString("PlaceLocationQualifier", "AD2")
            End If

            xmlwriter.WriteEndElement() 'End Address Element
            _txtdata.Append(objPrescription.RxPrescriber.PrescriberAddress.City & ":" & objPrescription.RxPrescriber.PrescriberAddress.State & ":" & objPrescription.RxPrescriber.PrescriberAddress.Zip & ":AD2:" & objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim & "+")

            ''code changed to fix case Incident #00025992 . 
            ''if we have Telephone (TE) then only create <communicationNumbers> tag. "Prescription Routing 10.6 IG 2012-12-03.pdf" pg 171
            If objPrescription.RxPrescriber.PrescriberPhone.Phone.Trim <> "" Then
                xmlwriter.WriteStartElement("CommunicationNumbers")






                xmlwriter.WriteStartElement("Communication")
                xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Phone))
                xmlwriter.WriteElementString("Qualifier", "TE")
                xmlwriter.WriteEndElement() 'End Phone Element


                _txtdata.Append(GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Phone) & ":TE*")
                If objPrescription.RxPrescriber.PrescriberPhone.Fax.Trim <> "" Then





                    xmlwriter.WriteStartElement("Communication")
                    xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Fax.Trim))
                    xmlwriter.WriteElementString("Qualifier", "FX")
                    xmlwriter.WriteEndElement() 'End Phone Element
                End If

                ''resolved bug 50462 in 7030 
                If objPrescription.RxPrescriber.PrescriberPhone.Email.Trim <> "" Then
                    If objPrescription.RxPrescriber.PrescriberPhone.Email.Trim.Length > 80 Then
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber email has too many characters.Please update the email." & vbCrLf)
                        End If
                    Else
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", objPrescription.RxPrescriber.PrescriberPhone.Email.Trim)
                        xmlwriter.WriteElementString("Qualifier", "EM")
                        xmlwriter.WriteEndElement() 'End Phone Element

                    End If
                End If



                xmlwriter.WriteEndElement() 'End CommunicationNumbers element
                _txtdata.Append(GetPhoneNumber(objPrescription.RxPrescriber.PrescriberPhone.Fax) & ":FX'")
                _txtdata.Append(Environment.NewLine)
            End If


            xmlwriter.WriteEndElement() 'End Prescriber Element

            ''*****************************************************PRESCRIBER SECTION END*******************************************************************

            ''*****************************************************SUPERVISING PROVIDER SECTION START*******************************************************************

            'added new Supervisor Tag
            If objPrescription.RxSupervisorProviderID <> 0 Then
                'Start Supervisor Tag
                xmlwriter.WriteStartElement("Supervisor")
                'Start Identification Tag
                If objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI <> "" Then
                    xmlwriter.WriteStartElement("Identification")
                    '_txtData.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                    xmlwriter.WriteElementString("NPI", objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI)
                    xmlwriter.WriteEndElement()
                ElseIf objPrescription.RxSupervisorPrescriber.SupervisorPrescriberID <> "" Then
                    xmlwriter.WriteStartElement("Identification")
                    '_txtData.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                    xmlwriter.WriteElementString("SPI", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberID)
                    xmlwriter.WriteEndElement()
                ElseIf objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA <> "" Then
                    xmlwriter.WriteStartElement("Identification")
                    '_txtData.Append("PVD+PC+" & objPrescription.RxPrescriber.PrescriberID & ":SPI+++")
                    xmlwriter.WriteElementString("DEANumber", objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA)
                    xmlwriter.WriteEndElement()
                End If
                'End Identification Tag

                'Start Name Tag
                xmlwriter.WriteStartElement("Name")
                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim <> "" Then
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim.Length > 35 Then
                        'MessageBox.Show("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ''Exit Function
                        'If icnt = 0 Then
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber last name has too many characters.  Please update the first name." & vbCrLf)
                        'End If
                        xmlwriter.WriteElementString("LastName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("LastName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Trim)
                    End If

                End If

                '_txtData.Append(objPrescription.RxPrescriber.PrescriberName.LastName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.FirstName.Trim & ":" & objPrescription.RxPrescriber.PrescriberName.MiddleName.Trim & "++" & objPrescription.ClinicName.Trim & "+")
                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim <> "" Then
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Length > 35 Then
                        ''MessageBox.Show("This prescription request can not be sent because the prescriber first name has too many characters.  Please update the first name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ''Exit Function
                        'If icnt = 0 Then
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber first name has too many characters.  Please update the first name." & vbCrLf)
                        'End If
                        xmlwriter.WriteElementString("FirstName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("FirstName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Trim)
                    End If

                End If

                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim <> "" Then
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Length > 35 Then
                        ''MessageBox.Show("This prescription request can not be sent because the prescriber middle name has too many characters.Please update the middle name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ''Exit Function
                        'If icnt = 0 Then
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber middle name has too many characters.  Please update the middle name." & vbCrLf)
                        'End If
                        xmlwriter.WriteElementString("MiddleName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("MiddleName", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName.Trim)
                    End If
                End If

                xmlwriter.WriteEndElement()
                'End Name Tag


                ''ADDRESS--------------- tag start
                xmlwriter.WriteStartElement("Address")

                ' ''ADDRESS 1
                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim.Length > 35 Then
                    '    'MessageBox.Show("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    'Exit Function
                    'If icnt = 0 Then
                    '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber address line2 has too many characters.  Please update the address line2." & vbCrLf)
                    'End If
                    xmlwriter.WriteElementString("AddressLine1", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim.Substring(0, 35))
                Else
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim <> "" Then
                        xmlwriter.WriteElementString("AddressLine1", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim)
                    End If
                End If

                ''_txtData.Append(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Trim & ":")

                ' ''ADDRESS 2
                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim.Length > 35 Then
                    '    'MessageBox.Show("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    'Exit Function
                    'If icnt = 0 Then
                    '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber address line2 has too many characters.  Please update the address line2." & vbCrLf)
                    'End If
                    xmlwriter.WriteElementString("AddressLine2", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim.Substring(0, 35))
                Else
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim <> "" Then
                        xmlwriter.WriteElementString("AddressLine2", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim)
                    End If
                End If

                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim.Length > 35 Then
                    '    'MessageBox.Show("This prescription request can not be sent because the prescriber address line2 has too many characters.  Please update the address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    'Exit Function
                    'If icnt = 0 Then
                    '    ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber address line2 has too many characters.  Please update the address line2." & vbCrLf)
                    'End If
                    xmlwriter.WriteElementString("City", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim.Substring(0, 35))
                Else
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim <> "" Then
                        xmlwriter.WriteElementString("City", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Trim)
                    End If
                End If


                ''STATE
                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Trim <> "" Then
                    xmlwriter.WriteElementString("State", If(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Length > 2, objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Substring(0, 2), objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State))
                End If


                ''ZIP
                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Trim <> "" Then
                    xmlwriter.WriteElementString("ZipCode", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip)
                End If

                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Trim <> "" Then
                    xmlwriter.WriteElementString("PlaceLocationQualifier", "AD2")
                End If
                xmlwriter.WriteEndElement()
                ''ADDRESS---------------End Address Element

                ' _txtData.Append(objPrescription.RxPrescriber.PrescriberAddress.City & ":" & objPrescription.RxPrescriber.PrescriberAddress.State & ":" & objPrescription.RxPrescriber.PrescriberAddress.Zip & ":AD2:" & objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim & "+")

                ''code changed to fix case Incident #00025992 . 
                ''if we have Telephone (TE) then only create <communicationNumbers> tag. "Prescription Routing 10.6 IG 2012-12-03.pdf" pg 171
                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim <> "" Then
                    xmlwriter.WriteStartElement("CommunicationNumbers")
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone.Trim))
                        xmlwriter.WriteElementString("Qualifier", "TE")
                        xmlwriter.WriteEndElement() 'End Phone Element

                    End If
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax.Trim))
                        xmlwriter.WriteElementString("Qualifier", "FX")
                        xmlwriter.WriteEndElement() 'End Phone Element

                    End If

                    ''resolved bug 50462 in 7030 
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email.Trim <> "" Then
                        If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email.Trim.Length > 80 Then
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the supervising prescriber email has too many characters.Please update the email." & vbCrLf)
                            End If
                        Else
                            xmlwriter.WriteStartElement("Communication")
                            xmlwriter.WriteElementString("Number", objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email.Trim)
                            xmlwriter.WriteElementString("Qualifier", "EM")
                            xmlwriter.WriteEndElement() 'End Phone Element
                        End If


                    End If

                    xmlwriter.WriteEndElement()
                End If
                ' ''END PHONE

                xmlwriter.WriteEndElement()
                'END Supervisor Tag

            End If
            _txtdata.Append("PTT++" & Getdatetype(objPrescription.RxPatient.DateofBirth.Trim) & "+" & objPrescription.RxPatient.PatientName.LastName.Trim & ":" & objPrescription.RxPatient.PatientName.FirstName.Trim & "+" & GetGender(objPrescription.RxPatient.Gender.Trim) & "++")


            ''*****************************************************SUPERVISING PROVIDER SECTION END*******************************************************************



            ''*****************************************************PATIENT SECTION START*******************************************************************
            xmlwriter.WriteStartElement("Patient")

            Dim blnIdentificationTag As Boolean = False

            If objPrescription.RxPatient.SSN.Trim <> "" Then
                If objPrescription.RxPatient.SSN.Trim.Length > 35 Then
                    'MessageBox.Show("This prescription request can not be sent because the patient SSN has too many characters.  Please update the SSN.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If icnt = 0 Then
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the patient SSN has too many characters.  Please update the SSN." & vbCrLf)
                    End If
                End If
                blnIdentificationTag = True
            End If

            If blnIdentificationTag Then
                ''do not send any <PatientRelationship> tag, commented for Incident Number: 00025760, as discussed with PP
                'If objPrescription.FormularyCol.Count > 0 Then
                '    If objPrescription.FormularyCol.Item(0).PatientRelationShip <> "" Then
                '        xmlwriter.WriteElementString("PatientRelationship", Convert.ToInt16(objPrescription.FormularyCol.Item(0).PatientRelationShip))
                '    End If
                'End If
                xmlwriter.WriteStartElement("Identification")
                xmlwriter.WriteElementString("SocialSecurity", objPrescription.RxPatient.SSN.Trim)
                If objPrescription.FormularyCol.Count > 0 Then
                    If objPrescription.FormularyCol.Item(0).PBMPayerParticipantId <> "" Then
                        If objPrescription.FormularyCol.Item(0).PBM_PayerMemberId.Trim <> "" Then
                            If objPrescription.FormularyCol.Item(0).PBM_PayerMemberId.Trim.Length > 35 Then
                                xmlwriter.WriteElementString("PayerID", objPrescription.FormularyCol.Item(0).PBM_PayerMemberId.Trim.Substring(0, 35))
                            Else
                                xmlwriter.WriteElementString("PayerID", objPrescription.FormularyCol.Item(0).PBM_PayerMemberId)
                            End If
                        End If
                    End If
                End If
                xmlwriter.WriteEndElement() 'End Identification Element
            Else
                If objPrescription.FormularyCol.Count > 0 Then
                    ''do not send any <PatientRelationship> tag, commented for Incident Number: 00025760, as discussed with PP
                    'If objPrescription.FormularyCol.Item(0).PatientRelationShip <> "" Then''commented for Incident Number: 00025760, as discussed with PP
                    '    xmlwriter.WriteElementString("PatientRelationship", Convert.ToInt16(objPrescription.FormularyCol.Item(0).PatientRelationShip))
                    'End If
                    If objPrescription.FormularyCol.Item(0).PBM_PayerMemberId.Trim <> "" Then
                        xmlwriter.WriteStartElement("Identification")
                        If objPrescription.FormularyCol.Item(0).PBM_PayerMemberId.Trim.Length > 35 Then
                            xmlwriter.WriteElementString("PayerID", objPrescription.FormularyCol.Item(0).PBM_PayerMemberId.Trim.Substring(0, 35))
                        Else
                            xmlwriter.WriteElementString("PayerID", objPrescription.FormularyCol.Item(0).PBM_PayerMemberId)
                        End If

                        xmlwriter.WriteEndElement() 'End PayerIdentification Element
                    End If
                End If
            End If

            xmlwriter.WriteStartElement("Name")

            If objPrescription.RxPatient.PatientName.LastName.Trim.Length > 35 Then
                xmlwriter.WriteElementString("LastName", objPrescription.RxPatient.PatientName.LastName.Trim.Substring(0, 35))
            Else
                xmlwriter.WriteElementString("LastName", objPrescription.RxPatient.PatientName.LastName.Trim)
            End If

            If objPrescription.RxPatient.PatientName.FirstName.Trim.Length > 35 Then
                xmlwriter.WriteElementString("FirstName", objPrescription.RxPatient.PatientName.FirstName.Trim.Substring(0, 35))
            Else
                xmlwriter.WriteElementString("FirstName", objPrescription.RxPatient.PatientName.FirstName.Trim)
            End If

            ''added for e-prescribing tool validation
            If objPrescription.RxPatient.PatientName.MiddleName.Trim <> "" Then
                If objPrescription.RxPatient.PatientName.MiddleName.Trim.Length > 35 Then
                    xmlwriter.WriteElementString("MiddleName", objPrescription.RxPatient.PatientName.MiddleName.Trim.Substring(0, 35))
                Else
                    xmlwriter.WriteElementString("MiddleName", objPrescription.RxPatient.PatientName.MiddleName.Trim)
                End If
            End If



            'If objPrescription.RxPatient.PatientName.MiddleName.Trim <> "" Then
            '    If objPrescription.RxPatient.PatientName.MiddleName.Trim.Length > 35 Then
            '        'MessageBox.Show("This prescription request can not be sent because the patient middle name has too many characters.Please update the middle name.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        'Exit Function
            '        'If icnt = 0 Then
            '        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient middle name has too many characters.Please update the middle name." & vbCrLf)
            '        xmlwriter.WriteElementString("MiddleName", objPrescription.RxPatient.PatientName.MiddleName.Trim.Substring(0, 35))
            '        'End If
            '    Else
            '        '' Problem 00000054 : Wrongly Written Prescriber Middle Name instead of Patient Middle Name
            '        xmlwriter.WriteElementString("MiddleName", objPrescription.RxPatient.PatientName.MiddleName.Trim)
            '    End If
            'End If
            xmlwriter.WriteEndElement() 'End Name Element
            xmlwriter.WriteElementString("Gender", GetGender(objPrescription.RxPatient.Gender.Trim))

            xmlwriter.WriteStartElement("DateOfBirth")
            xmlwriter.WriteElementString("Date", Getdatetype(objPrescription.RxPatient.DateofBirth.Trim))
            xmlwriter.WriteEndElement()

            If objPrescription.RxPatient.PatientAddress.Address1.Trim <> "" Or objPrescription.RxPatient.PatientAddress.City.Trim <> "" Or objPrescription.RxPatient.PatientAddress.State.Trim <> "" Or objPrescription.RxPatient.PatientAddress.Zip.Trim <> "" Then
                xmlwriter.WriteStartElement("Address")
                If objPrescription.RxPatient.PatientAddress.Address1.Trim <> "" Then
                    If objPrescription.RxPatient.PatientAddress.Address1.Trim.Length > 35 Then
                        'MessageBox.Show("This prescription request can not be sent because the patient address line1 has too many characters.Please update the patient address line1.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        'If icnt = 0 Then
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient address line1 has too many characters.Please update the patient address line1." & vbCrLf)
                        xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPatient.PatientAddress.Address1.Trim.Substring(0, 35))
                        'End If
                    Else
                        xmlwriter.WriteElementString("AddressLine1", objPrescription.RxPatient.PatientAddress.Address1.Trim)
                    End If
                    _txtdata.Append(objPrescription.RxPatient.PatientAddress.Address1.Trim & ":")
                End If
                If objPrescription.RxPatient.PatientAddress.Address2.Trim <> "" Then
                    If objPrescription.RxPatient.PatientAddress.Address2.Trim.Length > 35 Then
                        'MessageBox.Show("This prescription request can not be sent because the patient address line2 has too many characters.Please update the patient address line2.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        'If icnt = 0 Then
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient address line2 has too many characters.Please update the patient address line2." & vbCrLf)
                        'End If
                        xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPatient.PatientAddress.Address2.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("AddressLine2", objPrescription.RxPatient.PatientAddress.Address2.Trim)
                    End If
                End If
                If objPrescription.RxPatient.PatientAddress.City.Trim <> "" Then
                    If objPrescription.RxPatient.PatientAddress.City.Trim.Length > 35 Then
                        'MessageBox.Show("This prescription request can not be sent because the patient city has too many characters.Please update the patient address line1.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        'If icnt = 0 Then
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient city has too many characters.Please update the patient city." & vbCrLf)
                        'End If
                        xmlwriter.WriteElementString("City", objPrescription.RxPatient.PatientAddress.City.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("City", objPrescription.RxPatient.PatientAddress.City.Trim)
                    End If
                    _txtdata.Append(objPrescription.RxPatient.PatientAddress.City.Trim & ":")
                End If
                If objPrescription.RxPatient.PatientAddress.State.Trim <> "" Then
                    If objPrescription.RxPatient.PatientAddress.State.Length > 2 Then
                        xmlwriter.WriteElementString("State", objPrescription.RxPatient.PatientAddress.State.Substring(0, 2))
                    Else
                        xmlwriter.WriteElementString("State", objPrescription.RxPatient.PatientAddress.State.Trim)
                    End If
                    _txtdata.Append(objPrescription.RxPatient.PatientAddress.State.Trim & ":")
                End If
                If objPrescription.RxPatient.PatientAddress.Zip.Trim <> "" Then
                    Dim objZipCode As New System.Text.RegularExpressions.Regex("(\d{5})|(\d{9})")
                    If objZipCode.Match(objPrescription.RxPatient.PatientAddress.Zip.Trim).Success = False Then
                        'MessageBox.Show("This prescription request can not be sent because the patient zipcode has too many characters.Please update the patient zipcode.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the patient zipcode has too many characters.Please update the patient zipcode." & vbCrLf)

                        End If
                    Else
                        If objPrescription.RxPatient.PatientAddress.Zip.Trim.Length > 11 Then
                            xmlwriter.WriteElementString("ZipCode", objPrescription.RxPatient.PatientAddress.Zip.Trim.Substring(0, 11))
                        Else
                            xmlwriter.WriteElementString("ZipCode", objPrescription.RxPatient.PatientAddress.Zip.Trim)
                        End If
                    End If
                    _txtdata.Append(objPrescription.RxPatient.PatientAddress.Zip.Trim & "+")
                End If

                If objPrescription.RxPatient.PatientAddress.Address2.Trim <> "" Then
                    xmlwriter.WriteElementString("PlaceLocationQualifier", "AD2")
                End If
                xmlwriter.WriteEndElement() 'End Address Element
            End If


            If objPrescription.RxPatient.PatientPhone.Phone.Trim <> "" Or objPrescription.RxPatient.PatientPhone.Fax.Trim <> "" Or objPrescription.RxPatient.PatientWorkPhone.Phone.Trim <> "" Then
                If objPrescription.RxPatient.PatientPhone.Phone.Trim <> "" Then
                    If GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone).Trim.Length > 25 Then
                        'MessageBox.Show("This prescription request can not be sent because the patient phone has too many characters.Please update the patient phone.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the patient phone has too many characters.Please update the patient phone." & vbCrLf)
                        End If
                    End If
                End If
                If objPrescription.RxPatient.PatientPhone.Fax.Trim <> "" Then
                    If GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Fax.Trim).Length > 25 Then
                        'MessageBox.Show("This prescription request can not be sent because the patient fax number has too many characters.Please update the patient fax number.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the patient fax number has too many characters.Please update the patient fax number." & vbCrLf)
                        End If
                    End If
                End If
                If objPrescription.RxPatient.PatientWorkPhone.Phone.Trim <> "" Then
                    If GetPhoneNumber(objPrescription.RxPatient.PatientWorkPhone.Phone).Trim.Length > 25 Then
                        'MessageBox.Show("This prescription request can not be sent because the patient Work phone has too many characters.Please update the patient Work phone.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        If icnt = 0 Then
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the patient Work phone has too many characters.Please update the patient Work phone." & vbCrLf)
                        End If
                    End If
                End If

                ''code changed to fix case Incident #00025992 . 
                ''if we have Telephone (TE) then only create <communicationNumbers> tag. "Prescription Routing 10.6 IG 2012-12-03.pdf" pg 171
                If objPrescription.RxPatient.PatientPhone.Phone.Trim <> "" Then
                    xmlwriter.WriteStartElement("CommunicationNumbers")
                    If objPrescription.RxPatient.PatientPhone.Phone.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone.Trim))
                        xmlwriter.WriteElementString("Qualifier", "TE")
                        xmlwriter.WriteEndElement() 'End Phone Element
                        _txtdata.Append(GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Phone.Trim) & ":TE'")
                        _txtdata.Append(Environment.NewLine)
                    End If

                    If objPrescription.RxPatient.PatientPhone.Fax.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientPhone.Fax.Trim))
                        xmlwriter.WriteElementString("Qualifier", "FX")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If

                    If objPrescription.RxPatient.PatientWorkPhone.Phone.Trim <> "" Then
                        xmlwriter.WriteStartElement("Communication")
                        xmlwriter.WriteElementString("Number", GetPhoneNumber(objPrescription.RxPatient.PatientWorkPhone.Phone.Trim))
                        xmlwriter.WriteElementString("Qualifier", "WP")
                        xmlwriter.WriteEndElement() 'End Phone Element
                    End If


                    ''resolved bug 50462 in 7030
                    If objPrescription.RxPatient.PatientPhone.Email.Trim <> "" Then
                        If objPrescription.RxPatient.PatientPhone.Email.Trim.Length > 80 Then
                            If icnt = 0 Then
                                ValidationMessageBuilder.Append("This prescription request can not be sent because the patient email has too many characters.Please update the patient email." & vbCrLf)
                            End If
                        Else
                            xmlwriter.WriteStartElement("Communication")
                            xmlwriter.WriteElementString("Number", objPrescription.RxPatient.PatientPhone.Email.Trim)
                            xmlwriter.WriteElementString("Qualifier", "EM")
                            xmlwriter.WriteEndElement() 'End Phone Element
                        End If





                    End If


                    xmlwriter.WriteEndElement() 'End PhoneNumbers element
                End If


            End If

            xmlwriter.WriteEndElement() 'End Patient Element


            ''*****************************************************PATIENT SECTION END*******************************************************************


            xmlwriter.WriteStartElement("MedicationPrescribed")

            ''shelly issue in SS 10.6 Glostream Issues List 7-17-2013, Your Drug Description contained a trailing space: "BACTROBAN 2% OINTMENT  "
            Dim sDrgDesc As String = objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim
            xmlwriter.WriteElementString("DrugDescription", sDrgDesc.Trim)

            _txtdata.Append("DRU+P:" & objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).Drugform.Trim & ":" & oDrug.ProdCode & ":" & oDrug.ProdCodeQualifier & "::::" & oDrug.DDID & ":MD+")

            xmlwriter.WriteStartElement("DrugCoded") 'Drugcoded element starts
            If oDrug.ProdCode.Trim.Length > 0 Then
                xmlwriter.WriteElementString("ProductCode", oDrug.ProdCode)
            End If
            If oDrug.ProdCodeQualifier.Trim.Length > 0 Then
                xmlwriter.WriteElementString("ProductCodeQualifier", oDrug.ProdCodeQualifier)
            End If

            Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloSurescriptGeneral.sDIBServiceURL)
                Dim DrugDBCodeResponse As gloGlobal.DIB.RxnormFlagInfo = oGSHelper.GetRxNormCode(oDrug.ProdCode)

                If DrugDBCodeResponse IsNot Nothing Then
                    xmlwriter.WriteElementString("DrugDBCode", DrugDBCodeResponse.Code)

                    If DrugDBCodeResponse.Type = "CD" Then
                        xmlwriter.WriteElementString("DrugDBCodeQualifier", "SCD")
                    Else
                        xmlwriter.WriteElementString("DrugDBCodeQualifier", DrugDBCodeResponse.Type)
                    End If
                    DrugDBCodeResponse = Nothing
                End If
            End Using

            If Not IsNothing(oDrug.EPCSDEASchedule) Then
                If oDrug.EPCSDEASchedule.Trim.Length > 0 Then
                    xmlwriter.WriteElementString("DEASchedule", oDrug.EPCSDEASchedule.Trim)
                End If
            End If

            xmlwriter.WriteEndElement() 'DrugCoded Element Ends

            '_txtdata.Append(oDrug.DrugQuantityQualifier & ":" & Val(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim) & ":38+:" & objPrescription.DrugsCol.Item(RefillItem).Directions.Trim & "+ZDS:" & Val(oDrug.DrugDuration.Trim) & ":804*85:")

            xmlwriter.WriteStartElement("Quantity")
            'xmlwriter.WriteElementString("Qualifier", oDrug.DrugQuantityQualifier) 'objPrescription.DrugsCol.Item(RefillItem).DrugQuantityQualifier) 
            If objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim.Length > 11 Then
                xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim.Substring(0, 11)))
            Else
                xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim))
            End If

            xmlwriter.WriteElementString("CodeListQualifier", "38")
            xmlwriter.WriteElementString("UnitSourceCode", "AC")
            xmlwriter.WriteElementString("PotencyUnitCode", objPrescription.DrugsCol.Item(RefillItem).PotencyCode.Trim)
            ' xmlwriter.WriteElementString("Qualifier", "38") ''''''from PRN imple guide CodeList
            xmlwriter.WriteEndElement() 'End Quantity Element

            'Try
            '    If Not IsNothing(oDrug) Then
            '        If oDrug.DrugDuration.Trim.Length > 0 AndAlso oDrug.DrugDuration.Trim.Length <= 3 Then
            '            If IsNumeric(oDrug.DrugDuration) Then
            '                xmlwriter.WriteElementString("DaysSupply", Val(oDrug.DrugDuration.Trim))
            '            End If
            '        End If
            '    End If
            'Catch ex As Exception

            'End Try
            Dim nDaysSupply As Integer = 0
            If oDrug.DrugDuration.Trim.Length > 0 AndAlso Val(oDrug.DrugDuration) <> 0 Then
                If IsNumeric(oDrug.DrugDuration) Then
                    nDaysSupply = Val(oDrug.DrugDuration)
                Else
                    Dim nDuration As String() = Nothing
                    Dim numberofDays As Integer
                    nDuration = oDrug.DrugDuration.Trim.Split(" ")
                    If nDuration.Length > 0 Then
                        Select Case nDuration(1).ToUpper
                            Case "MONTHS"
                                numberofDays = 30
                            Case "DAYS"
                                numberofDays = 1
                            Case "WEEKS"
                                numberofDays = 7
                        End Select
                        nDaysSupply = numberofDays * CType(nDuration(0), Integer)
                    End If
                End If

                xmlwriter.WriteElementString("DaysSupply", nDaysSupply)
            End If

            ''Handling Special character for NewRx
            Dim repstr As String = "&#176;"
            Dim degreestr As String = "°"
            objPrescription.DrugsCol.Item(RefillItem).Directions = objPrescription.DrugsCol.Item(RefillItem).Directions.Replace(degreestr, repstr)
            ''''''''''''''''''''''''''''''''''''''''''''''''''
            If objPrescription.DrugsCol.Item(RefillItem).Directions.Trim.Length > 140 Then
                xmlwriter.WriteElementString("Directions", objPrescription.DrugsCol.Item(RefillItem).Directions.Trim.Substring(0, 140))
            Else
                xmlwriter.WriteElementString("Directions", objPrescription.DrugsCol.Item(RefillItem).Directions.Trim)
            End If


            If Not IsNothing(oDrug.Notes) Then
                If oDrug.Notes.Trim.Length > 0 Then
                    If oDrug.Notes.Trim.Length > 210 Then
                        'MessageBox.Show("This prescription request can not be sent because the drug notes has too many characters.Please update the drug notes.", "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Exit Function
                        'ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug notes has too many characters for " & oDrug.DrugName & ".Please update the drug notes." & vbCrLf)
                        xmlwriter.WriteElementString("Note", oDrug.Notes.Trim.Substring(0, 210))
                    Else
                        xmlwriter.WriteElementString("Note", oDrug.Notes.Trim.Replace(Environment.NewLine, " "))
                    End If

                End If
            End If

            _txtdata.Append(Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate.Trim) & ":102+0+R:1'")
            _txtdata.Append(Environment.NewLine)

            xmlwriter.WriteStartElement("Refills")
            If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier.Trim.Length <= 0 Then
                xmlwriter.WriteElementString("Qualifier", "R")
                objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier = "R" ''if blank then bydefault set to "R"
            Else
                If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier = "P" Then
                    xmlwriter.WriteElementString("Qualifier", "R")
                Else
                    xmlwriter.WriteElementString("Qualifier", objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier.Trim)
                End If

            End If

            If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier = "R" Or objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier = "P" Then
                If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim <> "" Then
                    If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim.Length > 2 Then
                        xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim.Substring(0, 2)))
                    Else
                        xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim))
                    End If
                End If                    'Else
                '    If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim <> "" Then
                '        If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim.Length > 2 Then
                '            xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim.Substring(0, 2)))
                '        Else
                '            xmlwriter.WriteElementString("Value", Val(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim))
                '        End If
                '    End If
            End If
            xmlwriter.WriteEndElement() 'End Refills Element
            'If SendMessageType = "NewRx" Then
            '    If objPrescription.DrugsCol.Item(RefillItem).MaySubstitute Then
            '        xmlwriter.WriteElementString("Substitutions", "1")
            '    Else
            '        xmlwriter.WriteElementString("Substitutions", "0")
            '    End If
            'Else
            If objPrescription.DrugsCol.Item(RefillItem).MaySubstitute Then
                xmlwriter.WriteElementString("Substitutions", "0")
            Else
                xmlwriter.WriteElementString("Substitutions", "1")
            End If

            xmlwriter.WriteStartElement("WrittenDate")
            If objPrescription.DrugsCol.Item(RefillItem).WrittenDate <> Now.Date.ToString Then
                xmlwriter.WriteElementString("Date", Getdatetype(Now.Date))
            Else
                xmlwriter.WriteElementString("Date", Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate.Trim))
            End If
            xmlwriter.WriteEndElement() 'End Writtendate Element

            'Start DIAGNOSIS Element

            If Not String.IsNullOrEmpty(objPrescription.DrugsCol.Item(RefillItem).PrimaryDXQualifier) Then
                xmlwriter.WriteStartElement("Diagnosis")
                xmlwriter.WriteElementString("ClinicalInformationQualifier", "1")
                xmlwriter.WriteStartElement("Primary")
                xmlwriter.WriteElementString("Qualifier", objPrescription.DrugsCol.Item(RefillItem).PrimaryDXQualifier)
                xmlwriter.WriteElementString("Value", objPrescription.DrugsCol.Item(RefillItem).PrimaryDXValue)
                xmlwriter.WriteEndElement()

                If Not String.IsNullOrEmpty(objPrescription.DrugsCol.Item(RefillItem).SecondaryDXQualifier) Then
                    xmlwriter.WriteStartElement("Secondary")
                    xmlwriter.WriteElementString("Qualifier", objPrescription.DrugsCol.Item(RefillItem).SecondaryDXQualifier)
                    xmlwriter.WriteElementString("Value", objPrescription.DrugsCol.Item(RefillItem).SecondaryDXValue)
                    xmlwriter.WriteEndElement()
                End If
                xmlwriter.WriteEndElement()
            End If

            'End DIAGNOSIS Element

            If Not String.IsNullOrEmpty(objPrescription.DrugsCol.Item(RefillItem).PriorAuthorizationStatus) Then
                If objPrescription.DrugsCol.Item(RefillItem).PriorAuthorizationStatus.ToUpper() = "APPROVED" Then
                    xmlwriter.WriteStartElement("PriorAuthorization")
                    xmlwriter.WriteElementString("Qualifier", objPrescription.DrugsCol.Item(RefillItem).PriorAuthorizationQualifier)
                    xmlwriter.WriteElementString("Value", objPrescription.DrugsCol.Item(RefillItem).PriorAuthorizationValue)
                    xmlwriter.WriteEndElement()
                End If

                If Convert.ToString(objPrescription.DrugsCol.Item(RefillItem).PriorAuthorizationStatus).ToUpper() <> "CANCELLED" Then
                    xmlwriter.WriteElementString("PriorAuthorizationStatus", Me.GetEPAStatusAsRequiredInXML(objPrescription.DrugsCol.Item(RefillItem).PriorAuthorizationStatus))
                End If
            End If
            xmlwriter.WriteEndElement() 'End MedicationPrescribed Element

            If objPrescription.FormularyCol.Count > 0 Then
                xmlwriter.WriteStartElement("BenefitsCoordination")
                If objPrescription.FormularyCol.Item(0).PBMPayerParticipantId <> "" Then
                    xmlwriter.WriteStartElement("PayerIdentification")
                    xmlwriter.WriteElementString("PayerID", objPrescription.FormularyCol.Item(0).PBMPayerParticipantId)
                    xmlwriter.WriteEndElement()
                End If
                If objPrescription.FormularyCol.Item(0).BINPCNNumber <> "" Then
                    Dim _strBINPCN As String() = Split(objPrescription.FormularyCol.Item(0).BINPCNNumber, "/")
                    Dim _BIN As String = ""
                    Dim _PCN As String = ""
                    If _strBINPCN.Length = 1 Then
                        _BIN = _strBINPCN(0)
                    ElseIf _strBINPCN.Length > 1 Then
                        _BIN = _strBINPCN(0)
                        _PCN = _strBINPCN(1)
                    End If
                    If _BIN <> "" Then
                        xmlwriter.WriteStartElement("PayerIdentification")
                        xmlwriter.WriteElementString("BINLocationNumber", _BIN)
                        xmlwriter.WriteEndElement()
                    End If
                    If _PCN <> "" Then
                        xmlwriter.WriteStartElement("PayerIdentification")
                        xmlwriter.WriteElementString("BINLocationNumber", _PCN)
                        xmlwriter.WriteEndElement()
                    End If

                    'Else
                    '    xmlwriter.WriteStartElement("PayerIdentification")
                    '    xmlwriter.WriteElementString("MutuallyDefined", "ZZ")
                    '    xmlwriter.WriteEndElement()
                End If
                'End PayerIdentification Element
                If objPrescription.FormularyCol.Item(0).ISAControlNumber <> "" Then
                    xmlwriter.WriteStartElement("PayerIdentification")
                    'xmlwriter.WriteElementString("MutuallyDefined", objPrescription.FormularyCol.Item(0).ISAControlNumber)
                    If objPrescription.FormularyCol.Item(0).ISAControlNumber.Trim.Length > 35 Then
                        xmlwriter.WriteElementString("MutuallyDefined", objPrescription.FormularyCol.Item(0).ISAControlNumber.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("MutuallyDefined", objPrescription.FormularyCol.Item(0).ISAControlNumber)
                    End If
                    xmlwriter.WriteEndElement() 'End PayerIdentification Element
                End If


                '<PayerName>AMERICARE INSURANCE</PayerName>
                If objPrescription.FormularyCol.Item(0).PBMPayerName <> "" Then
                    'xmlwriter.WriteElementString("PayerName", objPrescription.FormularyCol.Item(0).PBMPayerName)
                    If objPrescription.FormularyCol.Item(0).PBMPayerName.Trim.Length > 35 Then
                        xmlwriter.WriteElementString("PayerName", objPrescription.FormularyCol.Item(0).PBMPayerName.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("PayerName", objPrescription.FormularyCol.Item(0).PBMPayerName)
                    End If
                End If

                '<CardholderID>MemberId</CardholderID>
                If objPrescription.FormularyCol.Item(0).CardHolderId <> "" Then
                    '  xmlwriter.WriteElementString("CardholderID", objPrescription.FormularyCol.Item(0).CardHolderId)
                    If objPrescription.FormularyCol.Item(0).CardHolderId.Trim.Length > 35 Then
                        xmlwriter.WriteElementString("CardholderID", objPrescription.FormularyCol.Item(0).CardHolderId.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("CardholderID", objPrescription.FormularyCol.Item(0).CardHolderId)
                    End If
                End If

                '<CardHolderName>
                '	<LastName>JAMES</LastName>
                '	<FirstName>TINA</FirstName>
                '</CardHolderName>
                If objPrescription.FormularyCol.Item(0).SubscriberFirstName <> "" And objPrescription.FormularyCol.Item(0).SubscriberLastName <> "" Then
                    xmlwriter.WriteStartElement("CardHolderName")
                    If objPrescription.FormularyCol.Item(0).SubscriberLastName.Trim.Length > 35 Then
                        xmlwriter.WriteElementString("LastName", objPrescription.FormularyCol.Item(0).SubscriberLastName.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("LastName", objPrescription.FormularyCol.Item(0).SubscriberLastName)
                    End If
                    If objPrescription.FormularyCol.Item(0).SubscriberFirstName.Trim.Length > 35 Then
                        xmlwriter.WriteElementString("FirstName", objPrescription.FormularyCol.Item(0).SubscriberFirstName.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("FirstName", objPrescription.FormularyCol.Item(0).SubscriberFirstName)
                    End If
                    'xmlwriter.WriteElementString("LastName", objPrescription.FormularyCol.Item(0).SubscriberLastName)
                    'xmlwriter.WriteElementString("FirstName", objPrescription.FormularyCol.Item(0).SubscriberFirstName)
                    xmlwriter.WriteEndElement() 'End CardHolderName Element
                End If

                '<GroupID>Group Number</GroupID>
                If objPrescription.FormularyCol.Item(0).GroupId <> "" Then
                    If objPrescription.FormularyCol.Item(0).GroupId.Trim.Length > 35 Then
                        xmlwriter.WriteElementString("GroupID", objPrescription.FormularyCol.Item(0).GroupId.Trim.Substring(0, 35))
                    Else
                        xmlwriter.WriteElementString("GroupID", objPrescription.FormularyCol.Item(0).GroupId)
                    End If

                End If

                xmlwriter.WriteEndElement() 'End BenefitsCoordination Element
            End If
            xmlwriter.WriteEndElement() 'End NewRx element
            xmlwriter.WriteEndElement() 'End Body Element
            xmlwriter.WriteEndElement() 'End Message Element
            xmlwriter.WriteEndDocument()
            xmlwriter.Close()

            _txtdata.Append("UIT+" & objPrescription.RxTransactionID & "+9'")
            _txtdata.Append(Environment.NewLine)
            _txtdata.Append("UIZ++1'")
            _txtdata.Append(Environment.NewLine)


            Return strfilepath

        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return Nothing
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return Nothing
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(_txtdata) Then
                _txtdata = Nothing
            End If
            If Not IsNothing(xmlwriter) Then
                xmlwriter.Close()
                xmlwriter = Nothing
            End If
        End Try

    End Function

    Public Function GenerateEPCSDrugCheck(ByRef ogloPrescription As EPrescription, ByVal RefillItem As Int16, ByVal Epcsval As EpcsSeviceCall) As String
        Dim strfilepath As String = String.Empty
        Try

            Dim request As EpcsRequest = New EpcsRequest

            request.RequestHeder = getRequestHeder()
            If EpcsSeviceCall.WSEPCSDrugCheckService = Epcsval Then
                'Dim requestObj As EDrug = oPrescription.DrugsCol.Item(RefillItem)
                Dim objInterface As clsEPCSHelper = New clsEPCSHelper
                request.FlagEpcsSeviceCall = EpcsSeviceCall.WSEPCSDrugCheckService
                request.RequestBody = ogloPrescription
                Dim epcsRequest As EpcsRequest = request
                strfilepath = objInterface.GenerateWSEPCSDrugCheckService(epcsRequest, RefillItem)
                request = Nothing
                epcsRequest = Nothing
                objInterface = Nothing
            ElseIf EpcsSeviceCall.WSEPCSDrugCheckServiceMultiple = Epcsval Then
                'Dim requestObj As EDrug = oPrescription.DrugsCol.Item(RefillItem)
                Dim objInterface As clsEPCSHelper = New clsEPCSHelper
                request.FlagEpcsSeviceCall = EpcsSeviceCall.WSEPCSDrugCheckServiceMultiple
                request.RequestBody = ogloPrescription
                Dim epcsRequest As EpcsRequest = request
                strfilepath = objInterface.GenerateWSEPCSDrugCheckService(epcsRequest, RefillItem)
                request = Nothing
                epcsRequest = Nothing
                objInterface = Nothing
            End If

            Return strfilepath
        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.UpdateLog("Error Occurred processing EPCS DrugCheck: " & ex.Message)
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return Nothing
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.UpdateLog("Error Occurred processing EPCS DrugCheck: " & ex.Message)
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return Nothing
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
            Return Nothing
        Finally

        End Try
    End Function

    Public Function GenerateEPCSWSGetPrescriptionStatus(ByVal oPrescription As EPrescription, ByVal RefillItem As Int16, ByVal Epcsval As EpcsSeviceCall) As String

        Dim strfilepath As String = String.Empty
        Try

            Dim request As EpcsRequest = New EpcsRequest

            request.RequestHeder = getRequestHeder()
            If EpcsSeviceCall.WSGetPrescriptionStatus = Epcsval Then
                Dim requestObj As EPrescription = oPrescription
                Dim objInterface As clsEPCSHelper = New clsEPCSHelper
                request.FlagEpcsSeviceCall = EpcsSeviceCall.WSGetPrescriptionStatus
                request.RequestBody = requestObj
                Dim epcsRequest As EpcsRequest = request
                strfilepath = objInterface.GenerateWSGetPrescriptionStatusService(epcsRequest)
            End If

            Return strfilepath
        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.UpdateLog("Error Occurred processing EPCS NewRx: " & ex.Message)
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return Nothing
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.UpdateLog("Error Occurred processing EPCS NewRx: " & ex.Message)
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return Nothing
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
            Return Nothing
        Finally

        End Try
    End Function


    Public Function GenerateEPCSUIlaunchSigning(ByVal oPrescription As EPrescription, ByVal RefillItem As Int16, ByRef erxFiles As List(Of Dictionary(Of String, String))(), ByVal Epcsval As EpcsSeviceCall) As String
        'Dim strfilepath As String = GenerateFileName(MessageType.eNewRxEPCS)
        Dim strfilepath As String = String.Empty
        Try
            Dim request As EpcsRequest = New EpcsRequest

            request.RequestHeder = getRequestHeder(EpcsSeviceCall.UILaunchSigning)
            If EpcsSeviceCall.UILaunchSigning = Epcsval Then
                Dim requestObj As EPrescription = oPrescription 'oPrescription.DrugsCol.Item(RefillItem)

                Dim objInterface As clsEPCSHelper = New clsEPCSHelper
                request.FlagEpcsSeviceCall = EpcsSeviceCall.UILaunchSigning
                request.RequestBody = requestObj
                Dim epcsRequest As EpcsRequest = request
                strfilepath = objInterface.GenerateUILaunchSigning(epcsRequest, RefillItem, erxFiles)
            End If

            Return strfilepath
        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.UpdateLog("Error Occurred processing EPCS NewRx: " & ex.Message)
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return Nothing
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.UpdateLog("Error Occurred processing EPCS NewRx: " & ex.Message)
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return Nothing
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
            Return Nothing
        Finally

        End Try
    End Function

    Public Function getRequestHeder(Optional ByVal val As EpcsSeviceCall = Nothing) As clsEpcsRequestHeder
        Dim requestHeder As clsEpcsRequestHeder = New clsEpcsRequestHeder
        Try

            Dim dt As DataTable
            Dim sclinicname, Externalcode As String
            Using oDBlayer As New gloSureScriptDBLayer
                dt = oDBlayer.GetClinicInformation(1)
                sclinicname = dt.Rows(0)("sClinicName").ToString()
                Externalcode = dt.Rows(0)("sExternalcode").ToString()
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End Using


            requestHeder.VendorName = VendorName
            requestHeder.VendorLabel = VendorLabel
            requestHeder.VendorNodeName = VendorNodeName
            requestHeder.VendorNodeLabel = VendorNodeLabel
            requestHeder.ApplicationVersion = ApplicationVersion
            requestHeder.AppName = sclinicname 'ClinicName 
            requestHeder.SourceOrganizationId = Externalcode 'ClinicName
            Dim dtdate As DateTime = System.DateTime.UtcNow
            Dim strdate As String = dtdate.ToString("yyyy-MM-dd")
            Dim strtime As String = dtdate.ToString("HH:mm:ss")
            Dim strUTCFormat As String = (Convert.ToString(strdate & Convert.ToString("T")) & strtime) & "Z"
            requestHeder.HeaderDate = strUTCFormat
            requestHeder.OrganizationName = sclinicname  'ClinicName 
            requestHeder.OrganizationLabel = requestHeder.OrganizationName

            If val = EpcsSeviceCall.UILaunchSigning Then
                Dim stagingProductionUrl As String = ""
                Dim ArrayUrl As String() = Nothing
                If gloSureScript.gloSurescriptGeneral.blnIsStagingServer Then
                    stagingProductionUrl = gloSureScript.gloSurescriptGeneral.eRx10dot6StagingWebserviceURL
                    If Not IsNothing(stagingProductionUrl) Then
                        ArrayUrl = stagingProductionUrl.Split("/")
                        If ArrayUrl.Length > 4 Then
                            requestHeder.LogoUrl = ArrayUrl(0) & "//" & ArrayUrl(2) & "/" & ArrayUrl(3) & "/images/logo.png"
                        End If
                    End If
                Else
                    stagingProductionUrl = gloSureScript.gloSurescriptGeneral.eRx10dot6ProductionWebserviceURL
                    If Not IsNothing(stagingProductionUrl) Then
                        ArrayUrl = stagingProductionUrl.Split("/")
                        If ArrayUrl.Length > 4 Then
                            requestHeder.LogoUrl = ArrayUrl(0) & "//" & ArrayUrl(2) & "/" & ArrayUrl(3) & "/images/logo.png"
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
        Return requestHeder
    End Function

    Public Function PostXMLFile(ByVal objPrescription As EPrescription, ByVal RefillItem As Int16, ByVal DNTF As Boolean, ByVal requestType As String, Optional ByVal sOldDrugName As String = "") As Boolean
        Dim drug As EDrug = Nothing
        Dim bSuccess As Boolean = True
        Try
            drug = objPrescription.DrugsCol.Item(RefillItem)

            If DNTF Then
                XMLFilePath = drug.eRxFilePath2
            Else
                XMLFilePath = drug.eRxFilePath
                If drug.MessageType <> "" Then
                    If drug.MessageType = gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow Or drug.MessageType = gloSureScriptInterface.SentMessageType.eDenied Then
                        drug.MessageType = gloSureScriptInterface.SentMessageType.eNewRx
                    End If
                Else
                    drug.MessageType = gloSureScriptInterface.SentMessageType.eDenied
                End If
            End If

            objPrescription.RxTransactionID = objPrescription.DrugsCol.Item(RefillItem).PrescriptionID ''set the value in order to insert value in erxWithoutInternet table because the prescriptionID was going same always when we updated the erxWithoutInternet table 

            Dim NoOfAttempt As Integer = 1
            Dim _isWithOutInternet As Boolean = False

            Dim result As Byte() = Nothing

            While (NoOfAttempt <= 5)
                If _isWithOutInternet = True Then
                    Exit While
                End If

                result = gloSurescriptGeneral.PostSurescriptMessage(XMLFilePath, objPrescription, _isWithOutInternet)

                If Not IsNothing(result) Then
                    Exit While
                Else
                    NoOfAttempt = NoOfAttempt + 1
                End If

            End While

            If Not IsNothing(result) Then
                If DNTF Then
                    bSuccess = ReadStatusMessageRevised(result, drug.MessageType, sOldDrugName.Trim, requestType)
                Else
                    bSuccess = ReadStatusMessageRevised(result, drug.MessageType, drug.DrugName.Trim, requestType)
                End If
            End If

        Catch ex As Exception
            If ex.Message.Contains("expected 'https'") Then
                MessageBox.Show("The provided URI scheme 'http' is invalid; expected 'https'.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            bSuccess = False
        Finally
            If Not IsNothing(drug) Then
                drug.Dispose()
                drug = Nothing
            End If
        End Try

        Return bSuccess
    End Function

    Public Shared Function ReadResponseOfEpcsDrugCheckMultiple(ByVal strfilename As String, valCase As EpcsSeviceCall, ByVal sPatientId As String, ByVal sDrugName As String, Optional ByRef sDEAScheduleCodeOUT As String = Nothing) As SS_Resp_WSEPCSDrugCheckServiceMultiple.EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusType
        Dim sDEAScheduleCode As String = String.Empty
        Dim _itemResponce As New SS_Resp_WSEPCSDrugCheckServiceMultiple.EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusType

        Dim objResponce As SS_Resp_WSEPCSDrugCheckServiceMultiple.EpcsResponse = Nothing
        Dim serializer As New XmlSerializer(GetType(SS_Resp_WSEPCSDrugCheckServiceMultiple.EpcsResponse))
        Dim xs As XmlSerializer = Nothing
        Dim fs As FileStream = Nothing
        Try
            Try
                Using file As New System.IO.StreamReader(strfilename, Encoding.Unicode, True)
                    objResponce = DirectCast(serializer.Deserialize(file), SS_Resp_WSEPCSDrugCheckServiceMultiple.EpcsResponse)
                    file.Close()
                End Using
                serializer = Nothing
            Catch ex As Exception
                Dim oErrorResponce As WS_ErrorResponce.EpcsResponse = Nothing
                Dim ErrSerializer As New XmlSerializer(GetType(WS_ErrorResponce.EpcsResponse))

                Using errfile As New System.IO.StreamReader(strfilename, Encoding.Unicode, True)
                    oErrorResponce = DirectCast(ErrSerializer.Deserialize(errfile), WS_ErrorResponce.EpcsResponse)
                    errfile.Close()
                End Using
                ErrSerializer = Nothing
                If Not IsNothing(oErrorResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error) Then
                    If oErrorResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorCode.Trim = "14" Then
                        Throw New Exception("Transmission failed. Please try again.")
                        'MessageBox.Show("Transmission failed. Please try again. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        Throw New Exception(oErrorResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText)
                        'MessageBox.Show(oErrorResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    _itemResponce.DEASchedule = String.Empty
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error Responce in WSEPCSDrugCheckService. " & oErrorResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText, sPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
                ErrSerializer = Nothing
                oErrorResponce = Nothing
                Return _itemResponce
            End Try

            If Not IsNothing(objResponce.EpcsResponseBody.WsGetEPCSDrugCheckResponse) Then
                If Not IsNothing(objResponce.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType) Then
                    For Each _item As SS_Resp_WSEPCSDrugCheckServiceMultiple.EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusType In objResponce.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType
                        If (_item.Status = "SUCCESS") Then
                            If _item.PharmacyNotePrefix.Contains("NADEAN") Then
                                _itemResponce.DEASchedule = String.Empty
                                sDEAScheduleCode = Convert.ToString(_item.DEASchedule)
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Narcotics Addiction DEA Number (NADEAN) is attempted to eRx", gloAuditTrail.ActivityOutCome.Failure)
                                Throw New Exception("NADEAN")
                                'MessageBox.Show("Because the prescription " & sDrugName & " is a Narcotics Addiction DEA Number (NADEAN) it cannot be sent electronically.  This prescription can be printed and will require a wet signature before handed over to patient.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ElseIf _item.PharmacyNotePrefix.Contains("GHB") Then
                                _itemResponce.DEASchedule = String.Empty
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Gamma hydroxybutyric acid (GHB) is attempted to eRx", gloAuditTrail.ActivityOutCome.Failure)
                                Throw New Exception("GHB")
                                'MessageBox.Show("Because the prescription " & sDrugName & " is of type gamma hydroxybutyric acid (GHB), it cannot be sent electronically.  This prescription can be printed and will require a wet signature before handed over to patient.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                _itemResponce.DEASchedule = Convert.ToString(_item.DEASchedule)
                                _itemResponce.NdcId = Convert.ToString(_item.NdcId)
                                ''BDO Audit
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Successfully get Drug Check response.", gloAuditTrail.ActivityOutCome.Success)
                                Return _itemResponce
                            End If
                            ''BDO Audit
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Successfully get Drug Check response.", gloAuditTrail.ActivityOutCome.Success)
                        ElseIf (_item.Status = "FAILURE") Then
                            If Not IsNothing(_item.ErrorList) Then
                                _itemResponce.Status = _item.ErrorList.Error.ErrorText
                                _itemResponce.DEASchedule = String.Empty
                                ''BDO Audit
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error in Drug Check response.", gloAuditTrail.ActivityOutCome.Failure)
                            End If
                        End If
                    Next
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Not Valid WSEPCSDrugCheckService Responce. ", sPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
            If Not IsNothing(objResponce.EpcsResponseBody.ErrorResponse) Then
                If Not IsNothing(objResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error) Then
                    If objResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorCode.Trim = "14" Then
                        Throw New Exception("Transmission failed. Please try again.")
                        'MessageBox.Show("Transmission failed. Please try again. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        Throw New Exception(objResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText)
                        'MessageBox.Show(objResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    _itemResponce.DEASchedule = String.Empty
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error Responce in WSEPCSDrugCheckService. " & objResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText, sPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        Catch ex As Exception
            sDEAScheduleCodeOUT = sDEAScheduleCode
            _itemResponce.DEASchedule = String.Empty
            'gloSurescriptGeneral.UpdateLog("Error in EPCS Drug Check Responce: " & ex.Message)
            ''BDO Audit
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error in WSEPCSDrugCheckService Responce. ", sPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            Throw ex
        Finally
            objResponce = Nothing
            serializer = Nothing
            xs = Nothing
            fs = Nothing
        End Try
        Return _itemResponce
    End Function


    Public Shared Function ReadResponseOfEpcsDrugCheck(ByVal strfilename As String, valCase As EpcsSeviceCall, ByVal sPatientId As String, ByVal sDrugName As String, Optional ByRef sDEAScheduleCodeOUT As String = Nothing) As String

        Dim sDEAScheduleCode As String = String.Empty

        Dim objRequest As SS_Resp_WSEPCSDrugCheckService.EpcsResponse = Nothing
        Dim serializer As New XmlSerializer(GetType(SS_Resp_WSEPCSDrugCheckService.EpcsResponse))
        Dim xs As XmlSerializer = Nothing
        Dim fs As FileStream = Nothing
        Try
            Try
                Using file As New System.IO.StreamReader(strfilename, Encoding.Unicode, True)
                    objRequest = DirectCast(serializer.Deserialize(file), SS_Resp_WSEPCSDrugCheckService.EpcsResponse)
                    file.Close()
                End Using
                serializer = Nothing
            Catch ex As Exception
                Dim oErrorResponce As WS_ErrorResponce.EpcsResponse = Nothing
                Dim ErrSerializer As New XmlSerializer(GetType(WS_ErrorResponce.EpcsResponse))

                Using errfile As New System.IO.StreamReader(strfilename, Encoding.Unicode, True)
                    oErrorResponce = DirectCast(ErrSerializer.Deserialize(errfile), WS_ErrorResponce.EpcsResponse)
                    errfile.Close()
                End Using
                ErrSerializer = Nothing
                If Not IsNothing(oErrorResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error) Then
                    If oErrorResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorCode.Trim = "14" Then
                        Throw New Exception("Transmission failed. Please try again.")
                        'MessageBox.Show("Transmission failed. Please try again. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        Throw New Exception(oErrorResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText)
                        'MessageBox.Show(oErrorResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    sDEAScheduleCode = String.Empty
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error Responce in WSEPCSDrugCheckService. " & oErrorResponce.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText, sPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
                ErrSerializer = Nothing
                oErrorResponce = Nothing
                Return sDEAScheduleCode
            End Try

            If Not IsNothing(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse) Then
                If Not IsNothing(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType) Then
                    If (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.Status = "SUCCESS") Then
                        If objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.PharmacyNotePrefix.Contains("NADEAN") Then
                            'sDEAScheduleCode = String.Empty
                            sDEAScheduleCode = Convert.ToString(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.DEASchedule)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Narcotics Addiction DEA Number (NADEAN) is attempted to eRx", gloAuditTrail.ActivityOutCome.Failure)
                            Throw New Exception("NADEAN")
                            'MessageBox.Show("Because the prescription " & sDrugName & " is a Narcotics Addiction DEA Number (NADEAN) it cannot be sent electronically.  This prescription can be printed and will require a wet signature before handed over to patient.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.PharmacyNotePrefix.Contains("GHB") Then
                            sDEAScheduleCode = String.Empty
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Gamma hydroxybutyric acid (GHB) is attempted to eRx", gloAuditTrail.ActivityOutCome.Failure)
                            Throw New Exception("GHB")
                            'MessageBox.Show("Because the prescription " & sDrugName & " is of type gamma hydroxybutyric acid (GHB), it cannot be sent electronically.  This prescription can be printed and will require a wet signature before handed over to patient.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            sDEAScheduleCode = Convert.ToString(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.DEASchedule)
                        End If
                        ''BDO Audit
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "SUCCESS in WSEPCSDrugCheckService Responce. ", sPrescriptionId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Successfully get Drug Check response.", gloAuditTrail.ActivityOutCome.Success)
                    ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.Status = "FAILURE") Then
                    End If
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Not Valid WSEPCSDrugCheckService Responce. ", sPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
            If Not IsNothing(objRequest.EpcsResponseBody.ErrorResponse) Then
                If Not IsNothing(objRequest.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error) Then
                    If objRequest.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorCode.Trim = "14" Then
                        Throw New Exception("Transmission failed. Please try again.")
                        'MessageBox.Show("Transmission failed. Please try again. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        Throw New Exception(objRequest.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText)
                        'MessageBox.Show(objRequest.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    sDEAScheduleCode = String.Empty
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error Responce in WSEPCSDrugCheckService. " & objRequest.EpcsResponseBody.ErrorResponse.ErrorResponseList.Error.ErrorText, sPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        Catch ex As Exception
            ' sDEAScheduleCode = String.Empty
            sDEAScheduleCodeOUT = sDEAScheduleCode
            'gloSurescriptGeneral.UpdateLog("Error in EPCS Drug Check Responce: " & ex.Message)
            ''BDO Audit
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error in WSEPCSDrugCheckService Responce. ", sPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            Throw ex
        Finally
            objRequest = Nothing
            serializer = Nothing
            xs = Nothing
            fs = Nothing
        End Try
        Return sDEAScheduleCode
    End Function

    'Private Shared Function ConvertXMLToDataSet(xmlData As String) As DataSet
    '    Dim sData As String = xmlData.Replace("encoding=""UTF-8"" standalone=""yes""", "")
    '    Dim stream As StringReader = Nothing
    '    Dim reader As XmlTextReader = Nothing
    '    Try
    '        Dim xmlDS As New DataSet()
    '        stream = New StringReader(sData)
    '        ' Load the XmlTextReader from the stream
    '        reader = New XmlTextReader(stream)
    '        xmlDS.ReadXml(reader)
    '        Return xmlDS
    '    Catch ex As Exception
    '        Return Nothing
    '        Throw ex
    '    Finally
    '        If reader IsNot Nothing Then
    '            reader.Close()
    '        End If
    '    End Try
    'End Function


    Public Shared Function ReadResponseOfEpcs(ByVal strfilename As String, ByVal sMessageName As String, valCase As EpcsSeviceCall, ByRef ogloPrescription As EPrescription) As Boolean
        Try
            Dim sStatus As Boolean = False
            Select Case valCase
                Case EpcsSeviceCall.WSEPCSDrugCheckService
                    'Dim objRequest As SS_Resp_WSEPCSDrugCheckService.EpcsResponse = Nothing
                    'Dim serializer As New XmlSerializer(GetType(SS_Resp_WSEPCSDrugCheckService.EpcsResponse))
                    'Dim xs As XmlSerializer = Nothing
                    'Dim fs As FileStream = Nothing
                    'Try

                    '    Using file As New System.IO.StreamReader(strfilename, Encoding.Unicode, True)
                    '        objRequest = DirectCast(serializer.Deserialize(file), SS_Resp_WSEPCSDrugCheckService.EpcsResponse)
                    '        file.Close()

                    '    End Using
                    '    serializer = Nothing
                    '    If Not IsNothing(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType) Then
                    '        If (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.Status = "SUCCESS") Then
                    '            'MessageBox.Show("Drug checked successfully .  ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '            sStatus = True
                    '        ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.Status = "FAILURE") Then
                    '            If (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "10") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "11") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "12") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "13") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "14") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "100") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "604") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "611") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "612") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "613") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "613") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "614") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "609") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "610") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "616") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "617") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorCode = "618") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetEPCSDrugCheckResponse.EPCSDrugCheckResponseListType.EPCSDrugPermissionStatusType.ErrorList.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            End If
                    '        End If
                    '    End If
                    'Catch ex As Exception
                    '    sStatus = False
                    '    Throw ex
                    'Finally
                    '    objRequest = Nothing
                    '    serializer = Nothing
                    '    xs = Nothing
                    '    fs = Nothing
                    'End Try
                Case EpcsSeviceCall.WSGetPrescriptionStatus
                    ' Dim ds As DataSet
                    Dim sXml As String
                    Using reader As New StreamReader(strfilename, Encoding.Unicode, True)
                        sXml = reader.ReadToEnd()
                        reader.Close()
                    End Using
                    ' Dim sData As String = sXml.Replace("encoding=""UTF-8"" standalone=""yes""", "")
                    Dim xdoc As Xml.XmlDocument = New Xml.XmlDocument()
                    xdoc.LoadXml(RemovedrfNamespace(sXml))

                    Dim ResponceNodes As XmlNodeList = xdoc.DocumentElement.SelectNodes("/EpcsResponse/EpcsResponseBody/WsGetPrescriptionStatusResponse/PrescriptionResponseList/PrescriptionResponse")
                    Dim sbuilder As StringBuilder = Nothing

                    If Not IsNothing(ResponceNodes) Then
                        sbuilder = New StringBuilder
                        Dim oDrugInfo As EDrug = Nothing
                        Dim strStatuslabel As String = ""
                        For rNo As Int16 = 0 To ogloPrescription.DrugsCol.Count - 1
                            If ogloPrescription.DrugsCol.Item(rNo).IsNarcotics > 1 Then
                                oDrugInfo = ogloPrescription.DrugsCol.Item(rNo)
                                If ResponceNodes.Count <> 0 Then
                                    For i As Int16 = 0 To ResponceNodes.Count - 1
                                        sbuilder.Clear()
                                        'MessageBox.Show(xdoc.GetElementsByTagName("SourcePrescriptionId").Item(i).InnerText.ToString())
                                        ' MessageBox.Show(xdoc.GetElementsByTagName("StatusLabel").Item(i).InnerText.ToString())
                                        If Convert.ToString(xdoc.GetElementsByTagName("SourcePrescriptionId").Item(i).InnerText) = oDrugInfo.PrescriptionID.ToString() Then
                                            sbuilder.Append("Drug Name : " & oDrugInfo.DrugName & vbCrLf)
                                            sbuilder.Append("NDC Code : " & oDrugInfo.ProdCode & vbCrLf)
                                            If oDrugInfo.MessageID <> "" Then
                                                sbuilder.Append("Message ID : " & oDrugInfo.MessageID & vbCrLf)
                                            End If
                                            If xdoc.GetElementsByTagName("StatusLabel").Count > 0 Then
                                                strStatuslabel = xdoc.GetElementsByTagName("StatusLabel").Item(i).InnerText.ToString()
                                                sbuilder.Append("Status Message : " & PrescriptionResponceText(strStatuslabel) & vbCrLf)
                                                ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Prescription Id" & xdoc.GetElementsByTagName("SourcePrescriptionId").Item(i).InnerText.ToString() & "Status Responce of WsGetPrescriptionStatus - " & xdoc.GetElementsByTagName("StatusLabel").Item(i).InnerText.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                                            Else
                                                If xdoc.GetElementsByTagName("ErrorText").Count > 0 Then
                                                    strStatuslabel = xdoc.GetElementsByTagName("ErrorText").Item(i).InnerText.ToString()
                                                    sbuilder.Append("Status Message : " & strStatuslabel & vbCrLf)
                                                Else
                                                    strStatuslabel = "Error In File."
                                                    sbuilder.Append("Status Message : " & strStatuslabel & vbCrLf)
                                                End If
                                                ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "ERROR Responce in WsGetPrescriptionStatus .", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                                            End If
                                            sbuilder.Append(vbCrLf)
                                            ogloPrescription.DrugsCol.Item(rNo).EPCSPrescriptionStatusMessage = sbuilder.ToString()

                                            If strStatuslabel = "RXDELIVEREDTOPHARMACY" Or strStatuslabel = "RXSIGNED" Or strStatuslabel = "RXTRANSMITTED" Then
                                                strStatuslabel = "SUCCESS"  ''BDO Audit
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Controlled substance drug transmitted successfully", ogloPrescription.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                            Else
                                                strStatuslabel = "FAILURE"   ''BDO Audit
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Controlled substance drug transmitted unsuccessfully", ogloPrescription.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                                            End If
                                            ogloPrescription.DrugsCol.Item(rNo).EPCSPrescriptionStatusLabel = strStatuslabel
                                            gloSurescriptGeneral.UpdatePrescriptionStatusForCS(strStatuslabel, oDrugInfo.PrescriptionID)
                                            strStatuslabel = ""
                                            Exit For
                                        End If
                                    Next
                                Else
                                    ''ResponceNodes = xdoc.DocumentElement.SelectNodes("/EpcsResponse/EpcsResponseBody/ErrorResponse/ErrorResponseList/Error")
                                    If xdoc.GetElementsByTagName("ErrorText").Count > 0 Then
                                        strStatuslabel = "FAILURE"
                                        ogloPrescription.DrugsCol.Item(rNo).EPCSPrescriptionStatusMessage = "Error : " & xdoc.GetElementsByTagName("ErrorText").Item(0).InnerText.ToString()
                                        ogloPrescription.DrugsCol.Item(rNo).EPCSPrescriptionStatusLabel = strStatuslabel
                                        gloSurescriptGeneral.UpdatePrescriptionStatusForCS(strStatuslabel, oDrugInfo.PrescriptionID)
                                    Else
                                        strStatuslabel = "FAILURE"
                                        ogloPrescription.DrugsCol.Item(rNo).EPCSPrescriptionStatusMessage = "Error in prescription status responce."
                                        ogloPrescription.DrugsCol.Item(rNo).EPCSPrescriptionStatusLabel = strStatuslabel
                                        gloSurescriptGeneral.UpdatePrescriptionStatusForCS(strStatuslabel, oDrugInfo.PrescriptionID)
                                    End If
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error Responce in WsGetPrescriptionStatus. ", ogloPrescription.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                End If
                            End If
                        Next
                        '  MessageBox.Show(sbuilder.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Dim ErrorResponceNodes As XmlNodeList = xdoc.DocumentElement.SelectNodes("/ErrorResponse/EpcsResponseBody/ErrorResponse/ErrorResponseList/Error")
                        If Not IsNothing(ErrorResponceNodes) Then
                            For i As Int16 = 0 To ErrorResponceNodes.Count - 1
                                MessageBox.Show("Error Code : " & xdoc.GetElementsByTagName("ErrorCode").Item(i).InnerText.ToString() & vbCrLf & " Description : " & xdoc.GetElementsByTagName("ErrorText").Item(i).InnerText.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error Responce in WsGetPrescriptionStatus. " & "Error Code : " & xdoc.GetElementsByTagName("ErrorCode").Item(i).InnerText.ToString(), ogloPrescription.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            Next
                        End If
                    End If
                    If Not IsNothing(xdoc) Then
                        xdoc = Nothing
                    End If

                    'Dim xdoc As Xml.XmlDocument = New Xml.XmlDocument()
                    'xdoc.LoadXml(strfilename)
                    'Dim reader As New XmlNodeReader(xdoc)
                    'Dim ds As New DataSet
                    'ds.ReadXml(reader)

                    'Dim objRequest As SS_Res_WSGetPrescriptionStatus.EpcsResponse = Nothing
                    'Dim serializer As New XmlSerializer(GetType(SS_Res_WSGetPrescriptionStatus.EpcsResponse))
                    'Dim xs As XmlSerializer = Nothing
                    'Dim fs As FileStream = Nothing
                    'Try

                    '    Using file As New System.IO.StreamReader(strfilename, Encoding.Unicode, True)
                    '        objRequest = DirectCast(serializer.Deserialize(file), SS_Res_WSGetPrescriptionStatus.EpcsResponse)
                    '        file.Close()
                    '    End Using
                    '    serializer = Nothing


                    '    If Not IsNothing(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse) Then
                    '        If Not IsNothing(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.PrescriptionStatusDetailList) Then

                    '            If (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.PrescriptionStatusDetailList.PrescriptionStatusDetail.StatusLabel = "RXRECEIVED") Then
                    '                MessageBox.Show("Rx has been received by EPCS but not yet signed.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = True
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.PrescriptionStatusDetailList.PrescriptionStatusDetail.StatusLabel = "RXCANCELED") Then
                    '                MessageBox.Show("Prescriber has exited the signing UI without signing the prescription. This status only occurs if the prescription is not yet signed. EPCS Gold does not currently support cancellation of a prescription either from the prescriber or pharmacy after signing.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = True
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.PrescriptionStatusDetailList.PrescriptionStatusDetail.StatusLabel = "RXSIGNED") Then
                    '                MessageBox.Show("Rx has been signed by the prescriber in EPCS", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = True
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.PrescriptionStatusDetailList.PrescriptionStatusDetail.StatusLabel = "RXINQUEUE") Then
                    '                MessageBox.Show("Rx has been signed and queued for transmission", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = True
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.PrescriptionStatusDetailList.PrescriptionStatusDetail.StatusLabel = "RXTRANSMITTED") Then
                    '                MessageBox.Show("Rx transmitted to pharmacy", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = True
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.PrescriptionStatusDetailList.PrescriptionStatusDetail.StatusLabel = "RXERROR") Then
                    '                MessageBox.Show("Rx transmission resulted in an error or the Rx was rejected by SS or the pharmacy", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = True
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.PrescriptionStatusDetailList.PrescriptionStatusDetail.StatusLabel = "RXDELIVEREDTOPHARMACY") Then
                    '                MessageBox.Show("Final verification has been received that Rx was accepted by the pharmacy", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = True
                    '            End If
                    '        End If

                    '        If Not IsNothing(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error) Then
                    '            If (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorCode = "10") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorCode = "11") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorCode = "12") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorCode = "13") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorCode = "14") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorCode = "100") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorCode = "501") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            ElseIf (objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorCode = "502") Then
                    '                MessageBox.Show(objRequest.EpcsResponseBody.WsGetPrescriptionStatusResponse.PrescriptionResponseList.PrescriptionResponse.Error.ErrorText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                sStatus = False
                    '            End If
                    '        End If
                    '    End If

                    'Catch ex As Exception
                    '    sStatus = False
                    '    Throw ex
                    'Finally
                    '    objRequest = Nothing
                    '    serializer = Nothing
                    '    xs = Nothing
                    '    fs = Nothing
                    'End Try
                Case EpcsSeviceCall.UILaunchSigning


            End Select
            Return sStatus
        Catch ex As gloSurescriptDBException
            ''BDO Audit
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error While Prescription Status Responce. ", ogloPrescription.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            Throw ex
        Catch ex As GloSurescriptException
            ''BDO Audit
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error while Prescription Status Responce. ", ogloPrescription.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            Throw ex
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)

        End Try
    End Function

    Private Shared Function PrescriptionResponceText(statuscode As String) As String
        Dim sResult As String = ""
        Try
            Select Case statuscode
                Case "RXRECEIVED"
                    sResult = "Rx has been received by EPCS but not yet signed."
                Case "RXCANCELED"
                    sResult = "Prescriber has exited the signing UI without signing the prescription. This status only occurs if the prescription is not yet signed. EPCS Gold does not currently support cancellation of a prescription either from the prescriber or pharmacy after signing."
                Case "RXSIGNED"
                    sResult = "Rx has been signed by the prescriber in EPCS"
                Case "RXINQUEUE"
                    sResult = "Rx has been signed and queued for transmission"
                Case "RXTRANSMITTED"
                    sResult = "Rx transmitted to pharmacy"
                Case "RXERROR"
                    sResult = "Rx transmission resulted in an error or the Rx was rejected by SS or the pharmacy"
                Case "RXDELIVEREDTOPHARMACY"
                    sResult = "Final verification has been received that Rx was accepted by the pharmacy"
                Case "10"
                    sResult = "Invalid EPCS API version"
                Case "11"
                    sResult = "Vendor Label is not valid"
                Case "12"
                    sResult = "Vendor Node Label is not valid"
                Case "13"
                    sResult = "Organization ID is not valid"
                Case "14"
                    sResult = "Payload data does not generate the given hash value"
                Case "100"
                    sResult = "XSD validations failed"
                Case "501"
                    sResult = "No Data found for sourcePrescriptionId"
                Case "502"
                    sResult = "Organization Id not found. Please provide Organization Input Data"

            End Select

        Catch ex As Exception
            Throw ex
        End Try
        Return sResult
    End Function

    Private Shared Function RemovedrfNamespace(xml As String) As String
        'Dim xmlnsPattern As String = "\s+xmlns:\s*(:\w)?\s*=\s*\""(?<url>[^\""]*)\"""
        'Dim matchCol As MatchCollection = Regex.Matches(xml, xmlnsPattern)
        'For Each m As Match In matchCol
        '    xml = xml.Replace(m.ToString(), "")
        'Next
        Dim str1 As String = xml.Replace("encoding=""UTF-8"" standalone=""yes""", "")
        Dim st As String = str1.Replace("xmlns:drf=""urn:drfirst.com:epcsapi:v1_0"" xmlns:ss=""http://www.surescripts.com/messaging""", "")

        xml = st.Replace("drf:", "")
        Return xml
    End Function

    'code added to access WCF service binding runtime
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
        End Function

    End Class

#Region "Validate data before processing"
    ''' <summary>
    ''' Used to validate message header before it is sent
    ''' </summary>
    ''' <param name="omessage"></param>
    ''' <param name="oError"></param>
    ''' <param name="blnIsRefill"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateMessageHeader(ByVal omessage As SureScriptMessage, ByVal oError As SureScriptErrorMessage, Optional ByVal blnIsRefill As Boolean = False) As Boolean
        Dim objto As New System.Text.RegularExpressions.Regex("mailto:(\d{7}|\d{10}|\d{13}).(ncpdp|spi|tp)@surescripts.com")
        Dim objdate As New System.Text.RegularExpressions.Regex("((19|20)\d{2})-(((0[1-9]|1[0-2])-(0[1-9]|[12][0-9]))|(((0[13-9])|(1[0-2]))-30)|((0[13578]|1[02])-31))T(([0-1]\d)|(2[0-4])):([0-5]\d):([0-5]\d)(.\d|.|)Z")
        Try
            If omessage.MessageID.Trim = "" Then
                oError.ErrorCode = "900"
                oError.DescriptionCode = "000"
                oError.Description = "MessageId invalid"

                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the message ID is invalid")
                gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the message ID is invalid")
                ValidateMessageHeader = Nothing
                Exit Function
            ElseIf omessage.MessageFrom.Trim = "" Then
                oError.ErrorCode = "900"
                oError.DescriptionCode = "001"
                oError.Description = "SenderId not on file"


                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the sender information is invalid")
                gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the sender information is invalid")
                ValidateMessageHeader = Nothing
                Exit Function


            ElseIf omessage.MessageTo.Trim = "" Then
                oError.ErrorCode = "900"
                oError.DescriptionCode = "002"
                oError.Description = "ReceiverId not on file"

                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the recipient information is invalid")
                gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the recipient information is invalid")
                ValidateMessageHeader = Nothing
                Exit Function

            ElseIf omessage.DateTimeStamp.ToString = "" Then
                oError.ErrorCode = "900"
                oError.DescriptionCode = "029"
                oError.Description = "Date is invalid"

                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the senttime is invalid")
                gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the senttime is invalid")
                ValidateMessageHeader = Nothing
                Exit Function
            End If

            If blnIsRefill = False Then
                If omessage.RelatesToMessageId = "" Then
                    oError.ErrorCode = "900"
                    oError.DescriptionCode = "026"
                    oError.Description = "Initiator Reference is invalid"

                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the relates to messsageid is invalid")
                    gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the relates to messsageid is invalid")
                    ValidateMessageHeader = Nothing
                    Exit Function
                End If
            End If
            If objto.Match(omessage.MessageFrom).Success = False Then
                oError.ErrorCode = "900"
                oError.DescriptionCode = "001"
                oError.Description = "SenderId not on file"

                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the sender information is invalid")
                gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the sender information is invalid")
                ValidateMessageHeader = Nothing
                Exit Function
            End If
            If objto.Match(omessage.MessageTo).Success = False Then
                oError.ErrorCode = "900"
                oError.DescriptionCode = "001"
                oError.Description = "SenderId not on file"

                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the recipient information is invalid")
                gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the recipient information is invalid")
                ValidateMessageHeader = Nothing
                Exit Function
            End If

            If objdate.Match(omessage.DateTimeStamp).Success = False Then
                oError.ErrorCode = "900"
                oError.DescriptionCode = "029"
                oError.Description = "Date is invalid"

                gloSurescriptGeneral.UpdateLog("Message could not be read as datetimestamp is invalid")
                gloSurescriptGeneral.InformationMessage("Message could not read as datetimestamp is invalid")
                ValidateMessageHeader = Nothing
                Exit Function
            End If
            Return True
        Catch ex As Exception
            ValidateMessageHeader = Nothing
            Throw New GloSurescriptException(ex.Message)
        Finally
            If Not IsNothing(objto) Then
                objto = Nothing
            End If
            If Not IsNothing(objdate) Then
                objdate = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Used to validate Refill Response message before it is sent
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRefillResponse(ByVal objPrescription As EPrescription, ByVal RefillItem As Int16, ByVal eRefillstatus As RefillStatus, ByVal icnt As Int16) As Boolean
        Try
            gloSurescriptGeneral.UpdateLog("Entering gloSureScriptsInterface.ValidateRefillResponse")
            Dim blnIsValid As Boolean = True
            If icnt = 0 Then
                If Not IsNothing(objPrescription.RxReferenceNumber) Then
                    If objPrescription.RxReferenceNumber.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescription reference number is not available. ")
                        'gloSurescriptGeneral.InformationMessage("This Refill Response can not be sent because the prescription reference number is not available. ")
                        'Exit Function
                        ValidationMessageBuilder.Append("This Refill Response can not be sent because the prescription reference number is not available. " & vbCrLf)
                        'ElseIf objPrescription.RxReferenceNumber.Trim.Length > 35 Then
                        '    gloSurescriptGeneral.UpdateLog("Message could not be read as RxReference Number length is invalid")
                        '    gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberID length is invalid")
                        '    Exit Function
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescription reference number is not available. ")
                    'gloSurescriptGeneral.InformationMessage("This Refill Response can not be sent because the prescription reference number is not available. ")
                    'Exit Function
                    ValidationMessageBuilder.Append("This Refill Response can not be sent because the prescription reference number is not available. " & vbCrLf)
                    'ElseIf objPrescription.RxReferenceNumber.Trim.Length > 35 Then
                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as RxReference Number length is invalid")
                    '    gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberID length is invalid")
                    '    Exit Function
                    blnIsValid = False
                End If


                If Not IsNothing(objPrescription.RxTransactionID) Then
                    If objPrescription.RxTransactionID.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescriber order number is not available.")
                        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the prescriber order number is not available.")
                        'Exit Function
                        ValidationMessageBuilder.Append("This refill response can not be sent because the prescriber order number is not available." & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescriber order number is not available.")
                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the prescriber order number is not available.")
                    'Exit Function
                    ValidationMessageBuilder.Append("This refill response can not be sent because the prescriber order number is not available." & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxTransactionID) Then
                    If objPrescription.RxTransactionID.Trim.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("Message could not be read as PrescriberOrder Number length is invalid")
                        'gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberOrder Number length is invalid")
                        'Exit Function
                        ValidationMessageBuilder.Append("Message could not read as PrescriberOrder Number length is invalid" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("Message could not be read as PrescriberOrder Number length is invalid")
                    'gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberOrder Number length is invalid")
                    'Exit Function
                    ValidationMessageBuilder.Append("Message could not read as PrescriberOrder Number length is invalid" & vbCrLf)
                    blnIsValid = False
                End If


                If Not IsNothing(objPrescription.RxPharmacy.PharmacyID) Then
                    If objPrescription.RxPharmacy.PharmacyID.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyID not available")
                        'gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyID not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("Message could not be read as PharmacyID not available" & vbCrLf)
                        blnIsValid = False
                        'ElseIf objPrescription.RxPharmacy.PharmacyID.Trim.Length > 9 Then
                        '    gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyID length is invalid")
                        '    gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyID length is invalid")
                        '    Exit Function
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyID not available")
                    'gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyID not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("Message could not be read as PharmacyID not available" & vbCrLf)
                    blnIsValid = False
                    'ElseIf objPrescription.RxPharmacy.PharmacyID.Trim.Length > 9 Then
                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyID length is invalid")
                    '    gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyID length is invalid")
                    '    Exit Function
                End If

                If Not IsNothing(objPrescription.RxPharmacy.PharmacyName) Then
                    If objPrescription.RxPharmacy.PharmacyName.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the pharmacy name is not available")
                        ValidationMessageBuilder.Append("This refill response can not be sent because the pharmacy name is not available" & vbCrLf)
                        blnIsValid = False
                        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the pharmacy name is not available")
                        'Exit Function
                        'ElseIf objPrescription.RxPharmacy.PharmacyName.Trim.Length > 35 Then
                        '    gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyName length is invalid")
                        '    gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyName length is invalid")
                        '    Exit Function
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the pharmacy name is not available")
                    ValidationMessageBuilder.Append("This refill response can not be sent because the pharmacy name is not available" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberID) Then
                    If objPrescription.RxPrescriber.PrescriberID.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the SPI ID is not available for this Prescriber.")
                        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the SPI ID is not available for this Prescriber.")
                        'Exit Function
                        ValidationMessageBuilder.Append("This refill response can not be sent because the SPI ID is not available for this Prescriber." & vbCrLf)
                        blnIsValid = False
                        'ElseIf objPrescription.RxPrescriber.PrescriberID.Trim.Length > 15 Then
                        '    gloSurescriptGeneral.UpdateLog("Message could not be read as PrescriberID length is invalid")
                        '    gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberID length is invalid")
                        '    Exit Function
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the SPI ID is not available for this Prescriber.")
                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the SPI ID is not available for this Prescriber.")
                    'Exit Function
                    ValidationMessageBuilder.Append("This refill response can not be sent because the SPI ID is not available for this Prescriber." & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberName.LastName) Then
                    If objPrescription.RxPrescriber.PrescriberName.LastName.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescriber's last name is not available")
                        ValidationMessageBuilder.Append("This refill response can not be sent because the prescriber's last name is not available" & vbCrLf)
                        blnIsValid = False
                        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the prescriber's last name is not available")
                        'Exit Function
                        'ElseIf objPrescription.RxPrescriber.PrescriberName.LastName.Trim.Length > 35 Then
                        '    gloSurescriptGeneral.UpdateLog("Message could not be read as Prescriber LastName length is invalid")
                        '    gloSurescriptGeneral.InformationMessage("Message could not read as Prescriber LastName length is invalid")
                        '    Exit Function
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescriber's last name is not available")
                    ValidationMessageBuilder.Append("This refill response can not be sent because the prescriber's last name is not available" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.PatientName.FirstName) Then
                    If objPrescription.RxPatient.PatientName.FirstName = "" Then
                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's first name is not available.")
                        ValidationMessageBuilder.Append("This refill response can not be sent because the patient's first name is not available." & vbCrLf)
                        blnIsValid = False
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's first name is not available.")
                        'Exit Function
                        'ElseIf objPrescription.RxPatient.PatientName.FirstName.Length > 35 Then
                        '    gloSurescriptGeneral.UpdateLog("Message could not be read as Patient FirstName length is invalid")
                        '    gloSurescriptGeneral.InformationMessage("Message could not be read as Patient FirstName length is invalid")
                        '    Exit Function
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's first name is not available.")
                    ValidationMessageBuilder.Append("This refill response can not be sent because the patient's first name is not available." & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.PatientName.LastName) Then
                    If objPrescription.RxPatient.PatientName.LastName = "" Then
                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's last name is not available.")
                        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the patient's last name is not available.")
                        'Exit Function
                        ValidationMessageBuilder.Append("This refill response can not be sent because the patient's last name is not available." & vbCrLf)
                        blnIsValid = False
                        'ElseIf objPrescription.RxPatient.PatientName.LastName.Length > 35 Then
                        '    gloSurescriptGeneral.UpdateLog("Message could not be read as Patient LastName length is invalid")
                        '    gloSurescriptGeneral.InformationMessage("Message could not be read as Patient LastName length is invalid")
                        '    Exit Function
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's last name is not available.")
                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the patient's last name is not available.")
                    'Exit Function
                    ValidationMessageBuilder.Append("This refill response can not be sent because the patient's last name is not available." & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.Gender) Then
                    If objPrescription.RxPatient.Gender = "" Then
                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's gender is not available.")
                        ValidationMessageBuilder.Append("This refill response can not be sent because the patient's gender is not available." & vbCrLf)
                        blnIsValid = False
                        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the patient's gender is not available.")
                        'Exit Function
                        'ElseIf objPrescription.RxPatient.Gender.Trim <> "M" Or objPrescription.RxPatient.Gender.Trim <> "F" Or objPrescription.RxPatient.Gender.Trim <> "U" Then
                        '    gloSurescriptGeneral.UpdateLog("Message could not be read as Patient Gender is invalid")
                        '    gloSurescriptGeneral.InformationMessage("Message could not be read as Patient Gender is invalid")
                        '    Exit Function
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's gender is not available.")
                    ValidationMessageBuilder.Append("This refill response can not be sent because the patient's gender is not available." & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.DateofBirth) Then
                    If Getdatetimetype(objPrescription.RxPatient.DateofBirth) = "" Then
                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's date of birth is not available")
                        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the patient's date of birth is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This refill response can not be sent because the patient's date of birth is not available" & vbCrLf)
                        blnIsValid = False
                        'ElseIf Getdatetype(objPrescription.RxPatient.DateofBirth).Trim <> "" Then
                        '    Dim objdate As New System.Text.RegularExpressions.Regex("((18|19|20)\d{2})(((0[1-9]|1[0-2])(0[1-9]|[12][0-9]))|(((0[13-9])|(1[0-2]))30)|((0[13578]|1[02])31))")
                        '    If objdate.Match(objPrescription.RxPatient.DateofBirth).Success = False Then
                        '        gloSurescriptGeneral.UpdateLog("Message could not be read as Patient DOB is invalid")
                        '        gloSurescriptGeneral.InformationMessage("Message could not be read as Patient DOB is invalid")
                        '        Exit Function
                        '    End If
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's date of birth is not available")
                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the patient's date of birth is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This refill response can not be sent because the patient's date of birth is not available" & vbCrLf)
                    blnIsValid = False
                End If

                If ValidationMessageBuilder.ToString.Trim.Length > 0 Then
                    ValidationMessage = ValidationMessageBuilder.ToString
                    ValidationMessageBuilder.Remove(0, ValidationMessageBuilder.Length)
                End If
            End If
            Dim drugname As String = ""

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugName) Then
                If objPrescription.DrugsCol.Item(RefillItem).DrugName = "" Then
                    gloSurescriptGeneral.UpdateLog("This refill response cannot be sent because the drug name is not available")
                    ValidationMessageBuilderforDrug.Append("This refill response cannot be sent because the drug name is not available" & vbCrLf)
                    blnIsValid = False
                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the drug name is not available")
                    'Exit Function
                    'ElseIf objPrescription.DrugsCol.Item(RefillItem).Dosage = "" Then
                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as Drug Dosage not available")
                    '    gloSurescriptGeneral.InformationMessage("Message could not be read as Drug Dosage not available")
                    '    Exit Function
                    'ElseIf objPrescription.DrugsCol.Item(RefillItem).Drugform = "" Then
                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as Drug Form not available")
                    '    gloSurescriptGeneral.InformationMessage("Message could not be read as Drug Form not available")
                    '    Exit Function

                    'validation for Drug Description
                    'ElseIf objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim <> "" And objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim <> "" And objPrescription.DrugsCol.Item(RefillItem).DosageForm.Trim <> "" Then
                    '    Dim strDrugDescription As String = objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).DosageForm.Trim
                    '    If strDrugDescription.Length > 105 Then
                    '        gloSurescriptGeneral.UpdateLog("Message could not be read as DrugName Length is invalid")
                    '        gloSurescriptGeneral.InformationMessage("Message could not be read as DrugName Length is invalid")
                    '        Exit Function
                    '    End If
                Else
                    drugname = objPrescription.DrugsCol.Item(RefillItem).DrugName
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This refill response cannot be sent because the drug name is not available")
                ValidationMessageBuilderforDrug.Append("This refill response cannot be sent because the drug name is not available" & vbCrLf)
                blnIsValid = False
            End If


            If IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) AndAlso objPrescription.DrugsCol.Item(RefillItem).DrugQuantity = "" Then
                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the drug quantity is not specified.")
                ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the drug quantity is not specified for " & drugname & vbCrLf)
                blnIsValid = False
                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the drug quantity is not specified.")
                'Exit Function
            ElseIf Not IsNumeric(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim) Then
                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the drug quantity is not a number.  Please specify the quantity with a number.")
                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the quantity is not a number.  Please specify the quantity with a number.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the drug quantity is not a number for " & drugname & " Please specify the quantity with a number." & vbCrLf)
                blnIsValid = False
            ElseIf Val(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) = 0 Then
                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the drug quantity is Zero.  Please specify the quantity.")
                ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the drug quantity is Zero for " & drugname & " Please specify the quantity." & vbCrLf)
                blnIsValid = False
                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the quantity is Zero.  Please specify the quantity.")
                'Exit Function
            End If

            'If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugQuantityQualifier) Then
            '    If objPrescription.DrugsCol.Item(RefillItem).DrugQuantityQualifier = "" Then
            '        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Drug Quantity Qualifier is not specified")
            '        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the Drug Quantity Qualifier is not specified")
            '        'Exit Function
            '        ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Drug Quantity Qualifier is not specified for " & drugname & vbCrLf)
            '        blnIsValid = False
            '        'ElseIf objPrescription.DrugsCol.Item(RefillItem).DrugQuantityQualifier.Length > 2 Then
            '        '    gloSurescriptGeneral.UpdateLog("Message could not be read as Drug Quantity Qualifier Length is invalid")
            '        '    gloSurescriptGeneral.InformationMessage("Message could not be read as Drug Quantity Qualifier Length is invalid")
            '        '    Exit Function
            '    End If
            'Else
            '    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Drug Quantity Qualifier is not specified")
            '    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the Drug Quantity Qualifier is not specified")
            '    'Exit Function
            '    ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Drug Quantity Qualifier is not specified for " & drugname & vbCrLf)
            '    blnIsValid = False
            'End If

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).Directions) Then
                If objPrescription.DrugsCol.Item(RefillItem).Directions = "" Then
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Directions is not specified")
                    ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Directions is not specified for " & drugname & vbCrLf)
                    blnIsValid = False
                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the Directions is not specified")
                    'Exit Function
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Directions is not specified")
                ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Directions is not specified for " & drugname & vbCrLf)
                blnIsValid = False
            End If

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).Directions) Then
                If objPrescription.DrugsCol.Item(RefillItem).Directions.Trim.Length > 140 Then
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the directions field has too many characters")
                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the directions field has too many characters")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the directions field has too many characters for " & drugname & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the directions field has too many characters")
                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the directions field has too many characters")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the directions field has too many characters for " & drugname & vbCrLf)
                blnIsValid = False
            End If

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity) Then
                If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim = "" Then
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Refill Quantity is not specified")
                    ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Refill Quantity is not specified for " & drugname & vbCrLf)
                    blnIsValid = False
                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the Refill Quantity is not specified")
                    'Exit Function
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Refill Quantity is not specified")
                ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Refill Quantity is not specified for " & drugname & vbCrLf)
                blnIsValid = False
            End If

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) Then
                If objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim.Length > 15 Then
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the drug quantity is too many characters.  Please enter the quantity in less than 15 characters")
                    ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the drug quantity is too many characters for " & drugname & " Please enter the quantity in less than 15 characters" & vbCrLf)
                    blnIsValid = False
                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the quantity is too many characters.  Please enter the quantity in less than 15 characters")
                    'Exit Function
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the drug quantity is too many characters.  Please enter the quantity in less than 15 characters")
                ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the drug quantity is too many characters for " & drugname & " Please enter the quantity in less than 15 characters" & vbCrLf)
                blnIsValid = False
            End If

            If Not IsNumeric(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim) Then
                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the refill quantity is not a number.  Please specify the refill quantity with a number.")
                ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the refill quantity is not a number for " & drugname & " Please specify the refill quantity with a number." & vbCrLf)
                blnIsValid = False
                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the refill quantity is not a number.  Please specify the refill quantity with a number.  ")
                'Exit Function
            End If

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity) Then
                If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim.Length > 3 Then
                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the refill quantity is too many characters.  Please enter the refill quantity in less than X characters.")
                    ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the refill quantity is too many characters for " & drugname & " Please enter the refill quantity in less than X characters." & vbCrLf)
                    blnIsValid = False
                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the refill quantity is too many characters.  Please enter the refill quantity in less than X characters.")
                    'Exit Function
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the refill quantity is too many characters.  Please enter the refill quantity in less than X characters.")
                ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the refill quantity is too many characters for " & drugname & " Please enter the refill quantity in less than X characters." & vbCrLf)
                blnIsValid = False
            End If

            Select Case eRefillstatus
                Case RefillStatus.eApproved, RefillStatus.eApprovedWithChanges
                    If Val(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim) = 0 Then
                        gloSurescriptGeneral.UpdateLog("Message could not be generated as Refill Quantity is Zero")
                        'gloSurescriptGeneral.InformationMessage("Message could not be generated as Refill Quantity is Zero")
                        'Exit Function
                        ValidationMessageBuilderforDrug.Append("Message could not be generated as Refill Quantity is Zero for " & drugname & vbCrLf)
                        blnIsValid = False
                    End If
            End Select

            gloSurescriptGeneral.UpdateLog("Exiting gloSureScriptsInterface.ValidateRefillResponse. Validation=" & blnIsValid)

            Return blnIsValid
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
        End Try
    End Function
 

    'Private Function ValidateRefillResponse10dot6(ByVal objPrescription As EPrescription, ByVal RefillItem As Int16, ByVal eRefillstatus As RefillStatus, ByVal icnt As Int16) As Boolean
    '    Try
    '        gloSurescriptGeneral.UpdateLog("Entering gloSureScriptsInterface.ValidateRefillResponse")
    '        Dim blnIsValid As Boolean = True
    '        If icnt = 0 Then
    '            If Not IsNothing(objPrescription.RxReferenceNumber) Then
    '                If objPrescription.RxReferenceNumber.Trim = "" Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescription reference number is not available. ")
    '                    'gloSurescriptGeneral.InformationMessage("This Refill Response can not be sent because the prescription reference number is not available. ")
    '                    'Exit Function
    '                    ValidationMessageBuilder.Append("This Refill Response can not be sent because the prescription reference number is not available. " & vbCrLf)
    '                    'ElseIf objPrescription.RxReferenceNumber.Trim.Length > 35 Then
    '                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as RxReference Number length is invalid")
    '                    '    gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberID length is invalid")
    '                    '    Exit Function
    '                    blnIsValid = False
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescription reference number is not available. ")
    '                'gloSurescriptGeneral.InformationMessage("This Refill Response can not be sent because the prescription reference number is not available. ")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This Refill Response can not be sent because the prescription reference number is not available. " & vbCrLf)
    '                'ElseIf objPrescription.RxReferenceNumber.Trim.Length > 35 Then
    '                '    gloSurescriptGeneral.UpdateLog("Message could not be read as RxReference Number length is invalid")
    '                '    gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberID length is invalid")
    '                '    Exit Function
    '                blnIsValid = False
    '            End If


    '            If Not IsNothing(objPrescription.RxTransactionID) Then
    '                If objPrescription.RxTransactionID.Trim = "" Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescriber order number is not available.")
    '                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the prescriber order number is not available.")
    '                    'Exit Function
    '                    ValidationMessageBuilder.Append("This refill response can not be sent because the prescriber order number is not available." & vbCrLf)
    '                    blnIsValid = False
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescriber order number is not available.")
    '                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the prescriber order number is not available.")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This refill response can not be sent because the prescriber order number is not available." & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If Not IsNothing(objPrescription.RxTransactionID) Then
    '                If objPrescription.RxTransactionID.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("Message could not be read as PrescriberOrder Number length is invalid")
    '                    'gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberOrder Number length is invalid")
    '                    'Exit Function
    '                    ValidationMessageBuilder.Append("Message could not read as PrescriberOrder Number length is invalid" & vbCrLf)
    '                    blnIsValid = False
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("Message could not be read as PrescriberOrder Number length is invalid")
    '                'gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberOrder Number length is invalid")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("Message could not read as PrescriberOrder Number length is invalid" & vbCrLf)
    '                blnIsValid = False
    '            End If


    '            If Not IsNothing(objPrescription.RxPharmacy.PharmacyID) Then
    '                If objPrescription.RxPharmacy.PharmacyID.Trim = "" Then
    '                    gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyID not available")
    '                    'gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyID not available")
    '                    'Exit Function
    '                    ValidationMessageBuilder.Append("Message could not be read as PharmacyID not available" & vbCrLf)
    '                    blnIsValid = False
    '                    'ElseIf objPrescription.RxPharmacy.PharmacyID.Trim.Length > 9 Then
    '                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyID length is invalid")
    '                    '    gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyID length is invalid")
    '                    '    Exit Function
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyID not available")
    '                'gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyID not available")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("Message could not be read as PharmacyID not available" & vbCrLf)
    '                blnIsValid = False
    '                'ElseIf objPrescription.RxPharmacy.PharmacyID.Trim.Length > 9 Then
    '                '    gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyID length is invalid")
    '                '    gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyID length is invalid")
    '                '    Exit Function
    '            End If

    '            If Not IsNothing(objPrescription.RxPharmacy.PharmacyID) Then
    '                If objPrescription.RxPharmacy.PharmacyID.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the pharmacy NCPDPID has too many characters")
    '                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy NCPDPID has too many characters")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Pharmacy NCPDPID(35)")
    '                    'ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy NCPDPID has too many characters" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the pharmacy NCPDPID has too many characters")
    '                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy NCPDPID has too many characters")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This refill response can not be sent because the pharmacy NCPDPID has too many characters" & vbCrLf)
    '                blnIsValid = False
    '            End If



    '            If Not IsNothing(objPrescription.RxPharmacy.PharmacyName) Then
    '                If objPrescription.RxPharmacy.PharmacyName.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy Name length is invalid")
    '                    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy Name length is invalid")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Pharmacy Name(35)")
    '                    'ValidationMessageBuilderforDrug.Append("Message could not be generated as Pharmacy Name length is invalid for Drug " & drugname & "." & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy Name length is invalid")
    '                'gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy Name length is invalid")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("Message could not be generated as Pharmacy Name length is invalid for Drug " & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If Not IsNothing(objPrescription.RxPharmacy.PharmacyName) Then
    '                If objPrescription.RxPharmacy.PharmacyName.Trim = "" Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the pharmacy name is not available")
    '                    ValidationMessageBuilder.Append("This refill response can not be sent because the pharmacy name is not available" & vbCrLf)
    '                    blnIsValid = False
    '                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the pharmacy name is not available")
    '                    'Exit Function
    '                    'ElseIf objPrescription.RxPharmacy.PharmacyName.Trim.Length > 35 Then
    '                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as PharmacyName length is invalid")
    '                    '    gloSurescriptGeneral.InformationMessage("Message could not read as PharmacyName length is invalid")
    '                    '    Exit Function
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the pharmacy name is not available")
    '                ValidationMessageBuilder.Append("This refill response can not be sent because the pharmacy name is not available" & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If Not IsNothing(objPrescription.RxPrescriber.PrescriberID) Then
    '                If objPrescription.RxPrescriber.PrescriberID.Trim = "" Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the SPI ID is not available for this Prescriber.")
    '                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the SPI ID is not available for this Prescriber.")
    '                    'Exit Function
    '                    ValidationMessageBuilder.Append("This refill response can not be sent because the SPI ID is not available for this Prescriber." & vbCrLf)
    '                    blnIsValid = False
    '                    'ElseIf objPrescription.RxPrescriber.PrescriberID.Trim.Length > 15 Then
    '                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as PrescriberID length is invalid")
    '                    '    gloSurescriptGeneral.InformationMessage("Message could not read as PrescriberID length is invalid")
    '                    '    Exit Function
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the SPI ID is not available for this Prescriber.")
    '                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the SPI ID is not available for this Prescriber.")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This refill response can not be sent because the SPI ID is not available for this Prescriber." & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If Not IsNothing(objPrescription.RxPrescriber.PrescriberID) Then
    '                If objPrescription.RxPrescriber.PrescriberID.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescriber SPI has too many characters")
    '                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber SPI has too many characters")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Prescriber SPI(35)")
    '                    'ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber SPI has too many characters" & vbCrLf)
    '                    'blnIsValid = False
    '                End If

    '            End If

    '            If Not IsNothing(objPrescription.RxPrescriber.PrescriberName.LastName) Then
    '                If objPrescription.RxPrescriber.PrescriberName.LastName.Trim = "" Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescriber's last name is not available")
    '                    ValidationMessageBuilder.Append("This refill response can not be sent because the prescriber's last name is not available" & vbCrLf)
    '                    blnIsValid = False
    '                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the prescriber's last name is not available")
    '                    'Exit Function
    '                    'ElseIf objPrescription.RxPrescriber.PrescriberName.LastName.Trim.Length > 35 Then
    '                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as Prescriber LastName length is invalid")
    '                    '    gloSurescriptGeneral.InformationMessage("Message could not read as Prescriber LastName length is invalid")
    '                    '    Exit Function
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the prescriber's last name is not available")
    '                ValidationMessageBuilder.Append("This refill response can not be sent because the prescriber's last name is not available" & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If Not IsNothing(objPrescription.RxPrescriber.PrescriberName.LastName) Then
    '                If objPrescription.RxPrescriber.PrescriberName.LastName.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response could not be generated as Prescriber Last Name length is invalid")
    '                    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber Last Name length is invalid")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Prescriber LastName(35)")
    '                    'ValidationMessageBuilder.Append("Message could not be generated as Prescriber Last Name length is invalid" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response could not be generated as Prescriber Last Name length is invalid")
    '                'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber Last Name length is invalid")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This refill response could not be generated as Prescriber Last Name length is invalid" & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If Not IsNothing(objPrescription.RxPrescriber.PrescriberName.FirstName) Then
    '                If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response could not be generated as Prescriber First Name length is invalid")
    '                    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber Last Name length is invalid")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Prescriber FirstName(35)")
    '                    'ValidationMessageBuilder.Append("Message could not be generated as Prescriber FirstName Name length is invalid" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            End If
    '            'Prescriber First Name and Middle Name


    '            If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.Address1) Then
    '                If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response could not be generated as Prescriber Address length is invalid")
    '                    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber length is invalid")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Prescriber Address1(35)")
    '                    'ValidationMessageBuilder.Append("Message could not be generated as Prescriber length is invalid" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response could not be generated as Prescriber Address length is invalid")
    '                'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber length is invalid")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This refill response could not be generated as Prescriber length is invalid" & vbCrLf)
    '                blnIsValid = False
    '            End If



    '            If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.City) Then
    '                If objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response could not be generated as Prescriber City length is invalid")
    '                    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber City length is invalid")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Prescriber City(35)")
    '                    'ValidationMessageBuilder.Append("Message could not be generated as Prescriber City length is invalid" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response could not be generated as Prescriber City length is invalid")
    '                'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber City length is invalid")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This refill response could not be generated as Prescriber City length is invalid" & vbCrLf)
    '                blnIsValid = False
    '            End If



    '            If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.State) Then
    '                If objPrescription.RxPrescriber.PrescriberAddress.State.Length > 9 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response could not be generated as Prescriber State length is invalid")

    '                    'ValidationMessageBuilderfor10dot6.Append("Prescriber State(9)")
    '                    'ValidationMessageBuilder.Append("Message could not be generated as Prescriber State length is invalid" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            End If


    '            If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.Zip) Then
    '                If objPrescription.RxPrescriber.PrescriberAddress.Zip.Length > 11 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response could not be generated as Prescriber Zip length is invalid")

    '                    'ValidationMessageBuilderfor10dot6.Append("Prescriber Zip(11)")
    '                    'ValidationMessageBuilder.Append("Message could not be generated as Prescriber Zip length is invalid" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            End If
    '            If Not IsNothing(objPrescription.RxPatient.PatientName.FirstName) Then
    '                If objPrescription.RxPatient.PatientName.FirstName = "" Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's first name is not available.")
    '                    ValidationMessageBuilder.Append("This refill response can not be sent because the patient's first name is not available." & vbCrLf)
    '                    blnIsValid = False
    '                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's first name is not available.")
    '                    'Exit Function
    '                    'ElseIf objPrescription.RxPatient.PatientName.FirstName.Length > 35 Then
    '                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as Patient FirstName length is invalid")
    '                    '    gloSurescriptGeneral.InformationMessage("Message could not be read as Patient FirstName length is invalid")
    '                    '    Exit Function
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's first name is not available.")
    '                ValidationMessageBuilder.Append("This refill response can not be sent because the patient's first name is not available." & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If Not IsNothing(objPrescription.RxPatient.PatientName.LastName) Then
    '                If objPrescription.RxPatient.PatientName.LastName = "" Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's last name is not available.")
    '                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the patient's last name is not available.")
    '                    'Exit Function
    '                    ValidationMessageBuilder.Append("This refill response can not be sent because the patient's last name is not available." & vbCrLf)
    '                    blnIsValid = False
    '                    'ElseIf objPrescription.RxPatient.PatientName.LastName.Length > 35 Then
    '                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as Patient LastName length is invalid")
    '                    '    gloSurescriptGeneral.InformationMessage("Message could not be read as Patient LastName length is invalid")
    '                    '    Exit Function
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's last name is not available.")
    '                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the patient's last name is not available.")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This refill response can not be sent because the patient's last name is not available." & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If Not IsNothing(objPrescription.RxPatient.PatientName.LastName) Then
    '                If objPrescription.RxPatient.PatientName.LastName.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname")
    '                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Patient LastName(35)")
    '                    'ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname")
    '                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This refill response can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname" & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If Not IsNothing(objPrescription.RxPatient.PatientName.FirstName) Then
    '                If objPrescription.RxPatient.PatientName.FirstName.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
    '                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Patient FirstName(35)")
    '                    'ValidationMessageBuilder.Append("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
    '                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This refill response can not be sent because Patient FirstName has too many characters,Please Update Patient First Name" & vbCrLf)
    '                blnIsValid = False
    '            End If


    '            '''''10.6 length validations
    '            If Not IsNothing(objPrescription.RxPatient.PatientAddress.Address1) Then
    '                If objPrescription.RxPatient.PatientAddress.Address1.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because Patient Address1 has too many characters,Please Update Patient Address1")
    '                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Patient Address1(35)")
    '                    'ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Address1 has too many characters,Please Update Patient Address1" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            End If
    '            If Not IsNothing(objPrescription.RxPatient.PatientAddress.City) Then
    '                If objPrescription.RxPatient.PatientAddress.City.Trim.Length > 35 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because Patient City has too many characters,Please Update Patient City")
    '                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Patient City(35)")
    '                    'ValidationMessageBuilder.Append("This prescription request can not be sent because Patient City has too many characters,Please Update Patient City" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            End If
    '            If Not IsNothing(objPrescription.RxPatient.PatientAddress.State) Then
    '                If objPrescription.RxPatient.PatientAddress.State.Trim.Length > 9 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because Patient State has too many characters,Please Update Patient State")
    '                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Patient State(9)")
    '                    'ValidationMessageBuilder.Append("This prescription request can not be sent because Patient State has too many characters,Please Update Patient State" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            End If
    '            If Not IsNothing(objPrescription.RxPatient.PatientAddress.Zip) Then
    '                If objPrescription.RxPatient.PatientAddress.Zip.Trim.Length > 11 Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because Patient Zip has too many characters,Please Update Patient Zip")
    '                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
    '                    'Exit Function
    '                    'ValidationMessageBuilderfor10dot6.Append("Patient Zip(11)")
    '                    'ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Zip has too many characters,Please Update Patient Zip" & vbCrLf)
    '                    'blnIsValid = False
    '                End If
    '            End If

    '            '''''10.6 length validations
    '            If Not IsNothing(objPrescription.RxPatient.Gender) Then
    '                If objPrescription.RxPatient.Gender = "" Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's gender is not available.")
    '                    ValidationMessageBuilder.Append("This refill response can not be sent because the patient's gender is not available." & vbCrLf)
    '                    blnIsValid = False
    '                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the patient's gender is not available.")
    '                    'Exit Function
    '                    'ElseIf objPrescription.RxPatient.Gender.Trim <> "M" Or objPrescription.RxPatient.Gender.Trim <> "F" Or objPrescription.RxPatient.Gender.Trim <> "U" Then
    '                    '    gloSurescriptGeneral.UpdateLog("Message could not be read as Patient Gender is invalid")
    '                    '    gloSurescriptGeneral.InformationMessage("Message could not be read as Patient Gender is invalid")
    '                    '    Exit Function
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's gender is not available.")
    '                ValidationMessageBuilder.Append("This refill response can not be sent because the patient's gender is not available." & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If Not IsNothing(objPrescription.RxPatient.DateofBirth) Then
    '                If Getdatetimetype(objPrescription.RxPatient.DateofBirth) = "" Then
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's date of birth is not available")
    '                    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the patient's date of birth is not available")
    '                    'Exit Function
    '                    ValidationMessageBuilder.Append("This refill response can not be sent because the patient's date of birth is not available" & vbCrLf)
    '                    blnIsValid = False
    '                    'ElseIf Getdatetype(objPrescription.RxPatient.DateofBirth).Trim <> "" Then
    '                    '    Dim objdate As New System.Text.RegularExpressions.Regex("((18|19|20)\d{2})(((0[1-9]|1[0-2])(0[1-9]|[12][0-9]))|(((0[13-9])|(1[0-2]))30)|((0[13578]|1[02])31))")
    '                    '    If objdate.Match(objPrescription.RxPatient.DateofBirth).Success = False Then
    '                    '        gloSurescriptGeneral.UpdateLog("Message could not be read as Patient DOB is invalid")
    '                    '        gloSurescriptGeneral.InformationMessage("Message could not be read as Patient DOB is invalid")
    '                    '        Exit Function
    '                    '    End If
    '                End If
    '            Else
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the patient's date of birth is not available")
    '                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the patient's date of birth is not available")
    '                'Exit Function
    '                ValidationMessageBuilder.Append("This refill response can not be sent because the patient's date of birth is not available" & vbCrLf)
    '                blnIsValid = False
    '            End If

    '            If ValidationMessageBuilder.ToString.Trim.Length > 0 Then
    '                ValidationMessage = ValidationMessageBuilder.ToString
    '                ValidationMessageBuilder.Remove(0, ValidationMessageBuilder.Length)
    '            End If
    '        End If
    '        Dim drugname As String = ""

    '        If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugName) Then
    '            If objPrescription.DrugsCol.Item(RefillItem).DrugName = "" Then
    '                gloSurescriptGeneral.UpdateLog("This refill response cannot be sent because the drug name is not available")
    '                ValidationMessageBuilderforDrug.Append("This refill response cannot be sent because the drug name is not available" & vbCrLf)
    '                blnIsValid = False
    '                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the drug name is not available")
    '                'Exit Function
    '                'ElseIf objPrescription.DrugsCol.Item(RefillItem).Dosage = "" Then
    '                '    gloSurescriptGeneral.UpdateLog("Message could not be read as Drug Dosage not available")
    '                '    gloSurescriptGeneral.InformationMessage("Message could not be read as Drug Dosage not available")
    '                '    Exit Function
    '                'ElseIf objPrescription.DrugsCol.Item(RefillItem).Drugform = "" Then
    '                '    gloSurescriptGeneral.UpdateLog("Message could not be read as Drug Form not available")
    '                '    gloSurescriptGeneral.InformationMessage("Message could not be read as Drug Form not available")
    '                '    Exit Function

    '                'validation for Drug Description
    '                'ElseIf objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim <> "" And objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim <> "" And objPrescription.DrugsCol.Item(RefillItem).DosageForm.Trim <> "" Then
    '                '    Dim strDrugDescription As String = objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).DosageForm.Trim
    '                '    If strDrugDescription.Length > 105 Then
    '                '        gloSurescriptGeneral.UpdateLog("Message could not be read as DrugName Length is invalid")
    '                '        gloSurescriptGeneral.InformationMessage("Message could not be read as DrugName Length is invalid")
    '                '        Exit Function
    '                '    End If
    '            Else
    '                drugname = objPrescription.DrugsCol.Item(RefillItem).DrugName
    '            End If
    '        Else
    '            gloSurescriptGeneral.UpdateLog("This refill response cannot be sent because the drug name is not available")
    '            ValidationMessageBuilderforDrug.Append("This refill response cannot be sent because the drug name is not available" & vbCrLf)
    '            blnIsValid = False
    '        End If


    '        If IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) AndAlso objPrescription.DrugsCol.Item(RefillItem).DrugQuantity = "" Then
    '            gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the drug quantity is not specified.")
    '            ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the drug quantity is not specified for " & drugname & vbCrLf)
    '            blnIsValid = False
    '            'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the drug quantity is not specified.")
    '            'Exit Function
    '        ElseIf Not IsNumeric(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim) Then
    '            gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the drug quantity is not a number.  Please specify the quantity with a number.")
    '            'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the quantity is not a number.  Please specify the quantity with a number.")
    '            'Exit Function
    '            ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the drug quantity is not a number for " & drugname & " Please specify the quantity with a number." & vbCrLf)
    '            blnIsValid = False
    '        ElseIf Val(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) = 0 Then
    '            'gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the drug quantity is Zero.  Please specify the quantity.")
    '            'ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the drug quantity is Zero for " & drugname & " Please specify the quantity." & vbCrLf)
    '            'blnIsValid = False
    '            ''gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the quantity is Zero.  Please specify the quantity.")
    '            ''Exit Function
    '        End If

    '        'If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugQuantityQualifier) Then
    '        '    If objPrescription.DrugsCol.Item(RefillItem).DrugQuantityQualifier = "" Then
    '        '        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Drug Quantity Qualifier is not specified")
    '        '        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the Drug Quantity Qualifier is not specified")
    '        '        'Exit Function
    '        '        ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Drug Quantity Qualifier is not specified for " & drugname & vbCrLf)
    '        '        blnIsValid = False
    '        '        'ElseIf objPrescription.DrugsCol.Item(RefillItem).DrugQuantityQualifier.Length > 2 Then
    '        '        '    gloSurescriptGeneral.UpdateLog("Message could not be read as Drug Quantity Qualifier Length is invalid")
    '        '        '    gloSurescriptGeneral.InformationMessage("Message could not be read as Drug Quantity Qualifier Length is invalid")
    '        '        '    Exit Function
    '        '    End If
    '        'Else
    '        '    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Drug Quantity Qualifier is not specified")
    '        '    'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the Drug Quantity Qualifier is not specified")
    '        '    'Exit Function
    '        '    ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Drug Quantity Qualifier is not specified for " & drugname & vbCrLf)
    '        '    blnIsValid = False
    '        'End If

    '        If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).Directions) Then
    '            If objPrescription.DrugsCol.Item(RefillItem).Directions = "" Then
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Directions is not specified")
    '                ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Directions is not specified for " & drugname & vbCrLf)
    '                blnIsValid = False
    '                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the Directions is not specified")
    '                'Exit Function
    '            End If
    '        Else
    '            gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Directions is not specified")
    '            ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Directions is not specified for " & drugname & vbCrLf)
    '            blnIsValid = False
    '        End If

    '        If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).Directions) Then
    '            If objPrescription.DrugsCol.Item(RefillItem).Directions.Trim.Length > 140 Then
    '                gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the directions field has too many characters")
    '                'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the directions field has too many characters")
    '                'Exit Function
    '                'ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the directions field has too many characters for " & drugname & vbCrLf)
    '                'blnIsValid = False
    '                'ValidationMessageBuilderfor10dot6.Append("Drug Directions(140)")
    '            End If
    '        Else
    '            gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the directions field has too many characters")
    '            'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the directions field has too many characters")
    '            'Exit Function
    '            ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the directions field has too many characters for " & drugname & vbCrLf)
    '            blnIsValid = False
    '        End If


    '        Select Case eRefillstatus
    '            Case RefillStatus.eApproved, RefillStatus.eApprovedWithChanges
    '                If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) Then
    '                    If objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim.Length > 11 Then
    '                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the drug quantity is too many characters.  Please enter the quantity in less than 15 characters")
    '                        'ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the drug quantity is too many characters for " & drugname & " Please enter the quantity in less than 15 characters" & vbCrLf)
    '                        'blnIsValid = False
    '                        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the quantity is too many characters.  Please enter the quantity in less than 15 characters")
    '                        'Exit Function
    '                        'ValidationMessageBuilderfor10dot6.Append("Drug Quantity(11)")
    '                    End If
    '                Else
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the drug quantity is too many characters.  Please enter the quantity in less than 15 characters")
    '                    ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the drug quantity is too many characters for " & drugname & " Please enter the quantity in less than 15 characters" & vbCrLf)
    '                    blnIsValid = False
    '                End If
    '                If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity) Then
    '                    If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier.Trim <> "PRN" Then
    '                        If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim = "" Then
    '                            gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Refill Quantity is not specified")
    '                            ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Refill Quantity is not specified for " & drugname & vbCrLf)
    '                            blnIsValid = False
    '                            'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the Refill Quantity is not specified")
    '                            'Exit Function
    '                        End If
    '                    End If
    '                Else
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the Refill Quantity is not specified")
    '                    ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the Refill Quantity is not specified for " & drugname & vbCrLf)
    '                    blnIsValid = False
    '                End If

    '                If Not IsNumeric(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim) Then
    '                    If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier.Trim <> "PRN" Then
    '                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the refill quantity is not a number.  Please specify the refill quantity with a number.")
    '                        ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the refill quantity is not a number for " & drugname & " Please specify the refill quantity with a number." & vbCrLf)
    '                        blnIsValid = False
    '                        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the refill quantity is not a number.  Please specify the refill quantity with a number.  ")
    '                        'Exit Function
    '                    End If
    '                End If

    '                If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity) Then
    '                    If objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim.Length > 2 Then
    '                        gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the refill quantity is too many characters.  Please enter the refill quantity in less than X characters.")
    '                        'ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the refill quantity is too many characters for " & drugname & " Please enter the refill quantity in less than X characters." & vbCrLf)
    '                        'blnIsValid = False
    '                        'gloSurescriptGeneral.InformationMessage("This refill response can not be sent because the refill quantity is too many characters.  Please enter the refill quantity in less than X characters.")
    '                        'Exit Function
    '                        'ValidationMessageBuilderfor10dot6.Append("Drug RefillQuantity(2)")
    '                    End If
    '                Else
    '                    gloSurescriptGeneral.UpdateLog("This refill response can not be sent because the refill quantity is too many characters.  Please enter the refill quantity in less than X characters.")
    '                    ValidationMessageBuilderforDrug.Append("This refill response can not be sent because the refill quantity is too many characters for " & drugname & " Please enter the refill quantity in less than X characters." & vbCrLf)
    '                    blnIsValid = False
    '                End If


    '                If (objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Trim) = "0" Then
    '                    gloSurescriptGeneral.UpdateLog("Message could not be generated as Refill Quantity is Zero")
    '                    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Refill Quantity is Zero")
    '                    'Exit Function
    '                    ValidationMessageBuilderforDrug.Append("Message could not be generated as Refill Quantity is Zero for " & drugname & vbCrLf)
    '                    blnIsValid = False
    '                End If
    '        End Select

    '        gloSurescriptGeneral.UpdateLog("Exiting gloSureScriptsInterface.ValidateRefillResponse. Validation=" & blnIsValid)

    '        Return blnIsValid
    '    Catch ex As Exception
    '        Throw New GloSurescriptException(ex.Message)
    '    End Try
    'End Function

    Private Function ValidateData(ByVal objPrescription As EPrescription, ByVal RefillItem As Int16, ByVal icnt As Int16) As Boolean
        Dim blnIsValid As Boolean = True
        Try
            If icnt = 0 Then
                '''''''''''''''''Prescriber Validation------------------------------------------------------------------
                If Not IsNothing(objPrescription.RxPrescriber.PrescriberID) Then
                    If objPrescription.RxPrescriber.PrescriberID = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber is not currently on the SureScripts network")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber is not currently on the SureScripts network")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber is not currently on the SureScripts network" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber is not currently on the SureScripts network")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber is not currently on the SureScripts network")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber is not currently on the SureScripts network" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberID) Then
                    If objPrescription.RxPrescriber.PrescriberID.Trim.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber SPI has too many characters")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber SPI has too many characters")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber SPI has too many characters" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber SPI has too many characters")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber SPI has too many characters")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber SPI has too many characters" & vbCrLf)
                    blnIsValid = False
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                'If Not IsNothing(objPrescription.RxPharmacy.PharmacyID) Then
                '    If objPrescription.RxPharmacy.PharmacyID = "" And objPrescription.RxPharmacy.PhNCPDPId = "" Then '' added new condition of NCPDPid for case case GLO2011-0013803
                '        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network")
                '        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network")
                '        'Exit Function
                '        ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network" & vbCrLf)
                '        blnIsValid = False
                '    End If
                'Else
                '    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network")
                '    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network")
                '    'Exit Function
                '    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network" & vbCrLf)
                '    blnIsValid = False
                'End If

                'If Not IsNothing(objPrescription.RxPharmacy.PharmacyID) Then
                '    If objPrescription.RxPharmacy.PharmacyID.Trim.Length > 35 Then
                '        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy NCPDPID has too many characters")
                '        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy NCPDPID has too many characters")
                '        'Exit Function
                '        ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy NCPDPID has too many characters" & vbCrLf)
                '        blnIsValid = False
                '    End If
                'Else
                '    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy NCPDPID has too many characters")
                '    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy NCPDPID has too many characters")
                '    'Exit Function
                '    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy NCPDPID has too many characters" & vbCrLf)
                '    blnIsValid = False
                'End If


                'If Not IsNothing(objPrescription.RxPharmacy.PharmacyName) Then
                '    If objPrescription.RxPharmacy.PharmacyName.Trim = "" Then
                '        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy name is not available")
                '        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy name is not available")
                '        'Exit Function
                '        ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy name is not available" & vbCrLf)
                '        blnIsValid = False
                '    End If
                'Else
                '    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy name is not available")
                '    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy name is not available")
                '    'Exit Function
                '    ValidationMessageBuilder.Append("This prescription request can not be sent because the pharmacy name is not available" & vbCrLf)
                '    blnIsValid = False
                'End If

                'If Not IsNothing(objPrescription.RxPharmacy.PharmacyName) Then
                '    If objPrescription.RxPharmacy.PharmacyName.Trim.Length > 35 Then
                '        gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy Name length is invalid")
                '        'gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy Name length is invalid")
                '        'Exit Function
                '        ValidationMessageBuilder.Append("Message could not be generated as Pharmacy Name length is invalid" & vbCrLf)
                '        blnIsValid = False
                '    End If
                'Else
                '    gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy Name length is invalid")
                '    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy Name length is invalid")
                '    'Exit Function
                '    ValidationMessageBuilder.Append("Message could not be generated as Pharmacy Name length is invalid" & vbCrLf)
                '    blnIsValid = False
                'End If

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                'If objPrescription.RxPharmacy.PharmacyAddress.Address1.Trim = "" Then
                '    gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy Address not available")
                '    gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy Address not available")
                '    Exit Function
                'ElseIf objPrescription.RxPharmacy.PharmacyAddress.City.Trim = "" Then
                '    gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy City not available")
                '    gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy City not available")
                '    Exit Function
                'ElseIf objPrescription.RxPharmacy.PharmacyAddress.State.Trim = "" Then
                '    gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy State not available")
                '    gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy State not available")
                '    Exit Function
                'ElseIf objPrescription.RxPharmacy.PharmacyAddress.Zip.Trim = "" Then
                '    gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy ZipCode not available")
                '    gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy Zipcode not available")
                '    Exit Function
                'End If
                'If GetPhoneNumber(objPrescription.RxPharmacy.PharmacyPhone.Phone).Trim = "" Then
                '    gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy Phone not available")
                '    gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy Phone not available")
                '    Exit Function

                'End If
                If Not IsNothing(objPrescription.RxPrescriber.PrescriberName.LastName) Then
                    If objPrescription.RxPrescriber.PrescriberName.LastName.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's last name is not available")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's last name is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's last name is not available" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's last name is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's last name is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's last name is not available" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberName.LastName) Then
                    If objPrescription.RxPrescriber.PrescriberName.LastName.Trim.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber Last Name length is invalid")
                        'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber Last Name length is invalid")
                        'Exit Function
                        ValidationMessageBuilder.Append("Message could not be generated as Prescriber Last Name length is invalid" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber Last Name length is invalid")
                    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber Last Name length is invalid")
                    'Exit Function
                    ValidationMessageBuilder.Append("Message could not be generated as Prescriber Last Name length is invalid" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberName.FirstName) Then
                    If objPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber First Name length is invalid")
                        'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber Last Name length is invalid")
                        'Exit Function

                        ValidationMessageBuilder.Append("Message could not be generated as Prescriber FirstName Name length is invalid" & vbCrLf)
                        blnIsValid = False
                    End If
                End If
                'Prescriber First Name and Middle Name

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.Address1) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's address is not available")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's address is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's address is not available" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's address is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's address is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's address is not available" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.Address1) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber Address length is invalid")
                        'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber length is invalid")
                        'Exit Function

                        ValidationMessageBuilder.Append("Message could not be generated as Prescriber length is invalid" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber Address length is invalid")
                    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber length is invalid")
                    'Exit Function
                    ValidationMessageBuilder.Append("Message could not be generated as Prescriber length is invalid" & vbCrLf)
                    blnIsValid = False
                End If


                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.City) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.City.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's city is not available")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's city is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's city is not available" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's city is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's city is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's city is not available" & vbCrLf)
                    blnIsValid = False
                End If


                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.City) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber City length is invalid")
                        'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber City length is invalid")
                        'Exit Function
                        ValidationMessageBuilder.Append("Message could not be generated as Prescriber City length is invalid" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber City length is invalid")
                    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber City length is invalid")
                    'Exit Function
                    ValidationMessageBuilder.Append("Message could not be generated as Prescriber City length is invalid" & vbCrLf)
                    blnIsValid = False
                End If


                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.State) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.State.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's state is not available")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's state is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's state is not available" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's state is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's state is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's state is not available" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.State) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.State.Length > 9 Then
                        gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber State length is invalid")
                        ValidationMessageBuilder.Append("Message could not be generated as Prescriber State length is invalid" & vbCrLf)
                        blnIsValid = False
                    End If
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.Zip) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's zip code is not available")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's zip code is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's zip code is not available" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's zip code is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's zip code is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's zip code is not available" & vbCrLf)
                    blnIsValid = False
                End If


                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.Zip) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim <> "" Then
                        Dim objZipCode As New System.Text.RegularExpressions.Regex("(\d{5})|(\d{9})")
                        If objZipCode.Match(objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim).Success = False Then
                            gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber ZipCode length is invalid")
                            'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber ZipCode length is invalid")
                            'Exit Function
                            ValidationMessageBuilder.Append("Message could not be generated as Prescriber ZipCode length is invalid" & vbCrLf)
                            blnIsValid = False
                        End If
                    End If
                Else
                    If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim <> "" Then
                        Dim objZipCode As New System.Text.RegularExpressions.Regex("(\d{5})|(\d{9})")
                        If objZipCode.Match(objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim).Success = False Then
                            gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber ZipCode length is invalid")
                            'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber ZipCode length is invalid")
                            'Exit Function
                            ValidationMessageBuilder.Append("Message could not be generated as Prescriber ZipCode length is invalid" & vbCrLf)
                            blnIsValid = False
                        End If
                    End If
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.Zip) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.Zip.Length > 11 Then
                        gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber Zip length is invalid")
                        ValidationMessageBuilder.Append("Message could not be generated as Prescriber Zip length is invalid" & vbCrLf)
                        blnIsValid = False
                    End If
                End If
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                '''''''''Patient Validation------------------------------------------------------------------------------------------------- 
                If Not IsNothing(objPrescription.RxPatient.PatientName.LastName) Then
                    If objPrescription.RxPatient.PatientName.LastName.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.PatientName.LastName) Then
                    If objPrescription.RxPatient.PatientName.LastName.Trim.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname")
                        'Exit Function

                        ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Lastname has too many characters,Please Update Patient Lastname" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.PatientName.FirstName) Then
                    If objPrescription.RxPatient.PatientName.FirstName.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information.")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information.")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information." & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information.")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information." & vbCrLf)
                    blnIsValid = False
                End If


                If Not IsNothing(objPrescription.RxPatient.PatientName.FirstName) Then
                    If objPrescription.RxPatient.PatientName.FirstName.Trim.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
                        'Exit Function

                        ValidationMessageBuilder.Append("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name" & vbCrLf)
                    blnIsValid = False
                End If


                '''''10.6 length validations
                If Not IsNothing(objPrescription.RxPatient.PatientAddress.Address1) Then
                    If objPrescription.RxPatient.PatientAddress.Address1.Trim.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient Address1 has too many characters,Please Update Patient Address1")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
                        'Exit Function

                        ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Address1 has too many characters,Please Update Patient Address1" & vbCrLf)
                        blnIsValid = False
                    End If
                End If
                If Not IsNothing(objPrescription.RxPatient.PatientAddress.City) Then
                    If objPrescription.RxPatient.PatientAddress.City.Trim.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient City has too many characters,Please Update Patient City")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
                        'Exit Function

                        ValidationMessageBuilder.Append("This prescription request can not be sent because Patient City has too many characters,Please Update Patient City" & vbCrLf)
                        blnIsValid = False
                    End If
                End If
                If Not IsNothing(objPrescription.RxPatient.PatientAddress.State) Then
                    If objPrescription.RxPatient.PatientAddress.State.Trim.Length > 9 Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient State has too many characters,Please Update Patient State")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
                        'Exit Function

                        ValidationMessageBuilder.Append("This prescription request can not be sent because Patient State has too many characters,Please Update Patient State" & vbCrLf)
                        blnIsValid = False
                    End If
                End If
                If Not IsNothing(objPrescription.RxPatient.PatientAddress.Zip) Then
                    If objPrescription.RxPatient.PatientAddress.Zip.Trim.Length > 11 Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient Zip has too many characters,Please Update Patient Zip")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient FirstName has too many characters,Please Update Patient First Name")
                        'Exit Function

                        ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Zip has too many characters,Please Update Patient Zip" & vbCrLf)
                        blnIsValid = False
                    End If
                End If

                '''''10.6 length validations

                If Not IsNothing(objPrescription.RxPatient.Gender) Then
                    If objPrescription.RxPatient.Gender.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information.")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information.")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information." & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information.")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information." & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.DateofBirth) Then
                    If Getdatetype(objPrescription.RxPatient.DateofBirth).Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information.")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information.")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information." & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information.")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information." & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.DateofBirth) Then
                    If Getdatetype(objPrescription.RxPatient.DateofBirth).Trim <> "" Then
                        'Dim objdate As New System.Text.RegularExpressions.Regex("((18|19|20)\d{2})(((0[1-9]|1[0-2])(0[1-9]|[12][0-9]))|(((0[13-9])|(1[0-2]))30)|((0[13578]|1[02])31))")
                        'If objdate.Match(Getdatetype(objPrescription.RxPatient.DateofBirth)).Success = False Then
                        '    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient Date of birth is invalid")
                        '    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient Date of birth is invalid")
                        '    'Exit Function
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Date of birth is invalid" & vbCrLf)
                        '    blnIsValid = False
                        'End If
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient Date of birth is invalid")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient Date of birth is invalid")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Date of birth is invalid" & vbCrLf)
                    blnIsValid = False
                End If


                Dim strgender As String = GetGender(objPrescription.RxPatient.Gender)
                If strgender.Trim.Length > 0 Then
                    Select Case strgender

                        Case "M", "F", "U"

                        Case Else
                            gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's gender is Invalid.  Please update the patient's demographic information")
                            'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's gender is Invaild.  Please update the patient's demographic information")
                            'Exit Function
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's gender is Invaild.  Please update the patient's demographic information" & vbCrLf)
                            blnIsValid = False
                    End Select

                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's gender is Invaild.  Please update the patient's demographic information")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's gender is Invalid.  Please update the patient's demographic information")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's gender is Invalid.  Please update the patient's demographic information" & vbCrLf)
                    blnIsValid = False
                End If
                If ValidationMessageBuilder.ToString.Trim.Length > 0 Then
                    ValidationMessage = ValidationMessageBuilder.ToString
                    ValidationMessageBuilder.Remove(0, ValidationMessageBuilder.Length)
                End If


            End If


            Dim drugname As String = ""

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugName) Then
                If objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim = "" Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug name is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the drug name is not available")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug name is not available" & vbCrLf)
                    blnIsValid = False
                Else
                    drugname = objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug name is not available")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the drug name is not available")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug name is not available" & vbCrLf)
                blnIsValid = False
            End If

            ''added in 6060  for multiple pharmacy logic

            If Not IsNothing(objPrescription.RxPharmacy.PharmacyID) AndAlso Not IsNothing(objPrescription.RxPharmacy.PhNCPDPId) Then
                If objPrescription.RxPharmacy.PharmacyID = "" Or objPrescription.RxPharmacy.PhNCPDPId = "" Then '' added new condition of NCPDPid for case case GLO2011-0013803
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network ")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network for Drug " & drugname & "." & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network for Drug " & drugname & "." & vbCrLf)
                blnIsValid = False
            End If

            If Not IsNothing(objPrescription.RxPharmacy.PharmacyID) Then
                If objPrescription.RxPharmacy.PharmacyID.Trim.Length > 35 Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy NCPDPID has too many characters")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy NCPDPID has too many characters")
                    'Exit Function

                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy NCPDPID has too many characters" & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy NCPDPID has too many characters")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy NCPDPID has too many characters")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy NCPDPID has too many characters for Drug " & drugname & "." & vbCrLf)
                blnIsValid = False
            End If


            If Not IsNothing(objPrescription.RxPharmacy.PharmacyName) Then
                If objPrescription.RxPharmacy.PharmacyName.Trim = "" Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy name is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy name is not available")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy name is not available for Drug " & drugname & "." & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy name is not available")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy name is not available")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy name is not available for Drug " & drugname & "." & vbCrLf)
                blnIsValid = False
            End If

            If Not IsNothing(objPrescription.RxPharmacy.PharmacyName) Then
                If objPrescription.RxPharmacy.PharmacyName.Trim.Length > 35 Then
                    gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy Name length is invalid")
                    'gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy Name length is invalid")
                    'Exit Function

                    ValidationMessageBuilderforDrug.Append("Message could not be generated as Pharmacy Name length is invalid for Drug " & drugname & "." & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("Message could not be generated as Pharmacy Name length is invalid")
                'gloSurescriptGeneral.InformationMessage("Message could not be generated as Pharmacy Name length is invalid")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("Message could not be generated as Pharmacy Name length is invalid for Drug " & drugname & "." & vbCrLf)
                blnIsValid = False
            End If
            '' multiple pharmacy logic

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).Dosage) Then
                If objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim = "" Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the dosage is not specified.  Please specify the dosage." & vbCrLf)
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the dosage is not specified.  Please specify the dosage.  ")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the dosage is not specified for " & drugname & ".Please specify the dosage." & vbCrLf)
                    blnIsValid = False
                Else
                    drugname = drugname & " " & objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the dosage is not specified.  Please specify the dosage." & vbCrLf)
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the dosage is not specified.  Please specify the dosage.  ")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the dosage is not specified for " & drugname & ".Please specify the dosage." & vbCrLf)
                blnIsValid = False
            End If

            'validation for Drug Description
            'ElseIf objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim <> "" And objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim <> "" And objPrescription.DrugsCol.Item(RefillItem).DosageForm.Trim <> "" Then
            '    Dim strDrugDescription As String = objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).Dosage.Trim & " " & objPrescription.DrugsCol.Item(RefillItem).DosageForm.Trim
            '    If strDrugDescription.Length > 105 Then
            '        gloSurescriptGeneral.UpdateLog("Message could not be generated as Drug Description Length is invalid")
            '        gloSurescriptGeneral.InformationMessage("Message could not be generated as Drug Description Length is invalid")
            '        Exit Function
            '    End If

            If IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) AndAlso objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Trim = "" Then
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug quantity is not specified for Drug " & drugname & ".Please specify the quantity.")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the quantity is not specified.  Please specify the quantity.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug quantity is not specified for " & drugname & ".Please specify the quantity." & vbCrLf)
                blnIsValid = False
            ElseIf Not IsNumeric(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) Then
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug quantity is not a number.  Please specify the quantity with a number.")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the quantity is not a number.  Please specify the quantity with a number.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug quantity is not a number for " & drugname & ".Please specify the quantity with a number." & vbCrLf)
                blnIsValid = False
            ElseIf Val(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) = 0 Then
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug quantity is Zero for " & drugname & "  Please specify the quantity.")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the quantity is Zero.  Please specify the quantity.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug quantity is Zero for " & drugname & " Please specify the quantity." & vbCrLf)
                blnIsValid = False
            End If

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).Directions) Then
                If objPrescription.DrugsCol.Item(RefillItem).Directions.Trim = "" Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug directions field is blank.  Please enter directions for taking this medication.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the directions field is blank.  Please enter directions for taking this medication.")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug directions field is blank for " & drugname & ".Please enter directions for taking this medication." & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug directions field is blank.  Please enter directions for taking this medication.")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the directions field is blank.  Please enter directions for taking this medication.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug directions field is blank for " & drugname & ".Please enter directions for taking this medication." & vbCrLf)
                blnIsValid = False
            End If

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).Directions) Then
                If objPrescription.DrugsCol.Item(RefillItem).Directions.Trim.Length > If(True, 70, 140) Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug directions field has too many characters.Please update directions field.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the directions field has too many characters.Please update directions field.")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug directions field has too many characters " & drugname & ".Please update directions field." & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug directions field has too many characters.Please update directions field.")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the directions field has too many characters.Please update directions field.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug directions field has too many characters " & drugname & ".Please update directions field." & vbCrLf)
                blnIsValid = False
            End If

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).WrittenDate) Then
                If Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate).Trim = "" Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the date of prescription is blank.  Please enter the date")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the date of prescription is blank.  Please enter the date")
                    'Exit Function
                    'validation of WrittenDate
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the date of prescription is blank " & drugname & ".Please enter the date" & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the date of prescription is blank.  Please enter the date")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the date of prescription is blank.  Please enter the date")
                'Exit Function
                'validation of WrittenDate
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the date of prescription is blank " & drugname & ".Please enter the date" & vbCrLf)
                blnIsValid = False
            End If


            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).WrittenDate) Then
                If Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate).Trim <> "" Then
                    Dim strWrittenDate As String = Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate).Trim
                    If strWrittenDate.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("Message could not be generated as WrittenDate is invalid")
                        'gloSurescriptGeneral.InformationMessage("Message could not be generated as WrittenDate is invalid")
                        'Exit Function
                        ValidationMessageBuilderforDrug.Append("Message could not be generated as WrittenDate is invalid for " & drugname & vbCrLf)
                        blnIsValid = False
                    End If
                End If
            Else
                gloSurescriptGeneral.UpdateLog("Message could not be generated as WrittenDate is invalid")
                'gloSurescriptGeneral.InformationMessage("Message could not be generated as WrittenDate is invalid")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("Message could not be generated as WrittenDate is invalid for " & drugname & vbCrLf)
                blnIsValid = False
            End If


            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) Then
                If objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Length > If(True, 11, 15) Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug quantity is too many characters.  Please enter the quantity in less than 15 characters.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the quantity is too many characters.  Please enter the quantity in less than 15 characters.")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug quantity is too many characters for " & drugname & ".Please enter the quantity in less than 15 characters." & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug quantity is too many characters.  Please enter the quantity in less than 15 characters.")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the quantity is too many characters.  Please enter the quantity in less than 15 characters.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug quantity is too many characters for " & drugname & ".Please enter the quantity in less than 15 characters." & vbCrLf)
                blnIsValid = False
            End If


            If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier <> "PRN" Then
                If Not IsNumeric(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity) Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the refill quantity is not a number.  Please specify the refill quantity with a number.  ")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the refill quantity is not a number.  Please specify the refill quantity with a number.  ")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the refill quantity is not a number for " & drugname & ".Please specify the refill quantity with a number." & vbCrLf)
                    blnIsValid = False
                ElseIf objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Length > If(True, 2, 3) Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the refill quantity is too many characters.  Please enter the refill quantity in less than X characters.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the refill quantity is too many characters.  Please enter the refill quantity in less than X characters.")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the refill quantity is too many characters for " & drugname & ".Please enter the refill quantity in less than X characters." & vbCrLf)
                    blnIsValid = False
                End If
            End If

            Return blnIsValid
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
        End Try
    End Function

    Private Function Validate10dot6Data(ByVal objPrescription As EPrescription, ByVal RefillItem As Int16, ByVal icnt As Int16) As Boolean
        Dim blnIsValid As Boolean = True
        Try
            If icnt = 0 Then
                '''''''''''''''''Prescriber Validation------------------------------------------------------------------
                If Not IsNothing(objPrescription.RxPrescriber.PrescriberID) Then
                    If objPrescription.RxPrescriber.PrescriberID = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber is not currently on the SureScripts network")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber is not currently on the SureScripts network")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber is not currently on the SureScripts network" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber is not currently on the SureScripts network")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber is not currently on the SureScripts network")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber is not currently on the SureScripts network" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberID) Then

                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber SPI has too many characters")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber SPI has too many characters")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber SPI has too many characters" & vbCrLf)
                    blnIsValid = False
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                If Not IsNothing(objPrescription.RxPrescriber.PrescriberName.LastName) Then
                    If objPrescription.RxPrescriber.PrescriberName.LastName.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's last name is not available")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's last name is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's last name is not available" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's last name is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's last name is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's last name is not available" & vbCrLf)
                    blnIsValid = False
                End If


                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.Address1) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's address is not available")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's address is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's address is not available" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's address is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's address is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's address is not available" & vbCrLf)
                    blnIsValid = False
                End If




                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.City) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.City.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's city is not available")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's city is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's city is not available" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's city is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's city is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's city is not available" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.State) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.State.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's state is not available")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's state is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's state is not available" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's state is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's state is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's state is not available" & vbCrLf)
                    blnIsValid = False
                End If



                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.Zip) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's zip code is not available")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's zip code is not available")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's zip code is not available" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the prescriber's zip code is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the prescriber's zip code is not available")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the prescriber's zip code is not available" & vbCrLf)
                    blnIsValid = False
                End If


                If Not IsNothing(objPrescription.RxPrescriber.PrescriberAddress.Zip) Then
                    If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim <> "" Then
                        Dim objZipCode As New System.Text.RegularExpressions.Regex("(\d{5})|(\d{9})")
                        If objZipCode.Match(objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim).Success = False Then
                            gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber ZipCode length is invalid")
                            'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber ZipCode length is invalid")
                            'Exit Function
                            ValidationMessageBuilder.Append("Message could not be generated as Prescriber ZipCode length is invalid" & vbCrLf)
                            blnIsValid = False
                        End If
                    End If
                Else
                    If objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim <> "" Then
                        Dim objZipCode As New System.Text.RegularExpressions.Regex("(\d{5})|(\d{9})")
                        If objZipCode.Match(objPrescription.RxPrescriber.PrescriberAddress.Zip.Trim).Success = False Then
                            gloSurescriptGeneral.UpdateLog("Message could not be generated as Prescriber ZipCode length is invalid")
                            'gloSurescriptGeneral.InformationMessage("Message could not be generated as Prescriber ZipCode length is invalid")
                            'Exit Function
                            ValidationMessageBuilder.Append("Message could not be generated as Prescriber ZipCode length is invalid" & vbCrLf)
                            blnIsValid = False
                        End If
                    End If
                End If

                If Not IsNothing(objPrescription.RxPatient.PatientName.LastName) Then
                    If objPrescription.RxPatient.PatientName.LastName.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information" & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's last name is not available.  Please update the patient's demographic information" & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.PatientName.FirstName) Then
                    If objPrescription.RxPatient.PatientName.FirstName.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information.")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information.")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information." & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information.")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's first name is not available.  Please update the patient's demographic information." & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.Gender) Then
                    If objPrescription.RxPatient.Gender.Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information.")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information.")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information." & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information.")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's gender is not available.  Please update the patient's demographic information." & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.DateofBirth) Then
                    If Getdatetype(objPrescription.RxPatient.DateofBirth).Trim = "" Then
                        gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information.")
                        'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information.")
                        'Exit Function
                        ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information." & vbCrLf)
                        blnIsValid = False
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information.")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's date of birth is not available.  Please update the patient's demographic information." & vbCrLf)
                    blnIsValid = False
                End If

                If Not IsNothing(objPrescription.RxPatient.DateofBirth) Then
                    If Getdatetype(objPrescription.RxPatient.DateofBirth).Trim <> "" Then
                        'Dim objdate As New System.Text.RegularExpressions.Regex("((18|19|20)\d{2})(((0[1-9]|1[0-2])(0[1-9]|[12][0-9]))|(((0[13-9])|(1[0-2]))30)|((0[13578]|1[02])31))")
                        'If objdate.Match(Getdatetype(objPrescription.RxPatient.DateofBirth)).Success = False Then
                        '    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient Date of birth is invalid")
                        '    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient Date of birth is invalid")
                        '    'Exit Function
                        '    ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Date of birth is invalid" & vbCrLf)
                        '    blnIsValid = False
                        'End If
                    End If
                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because Patient Date of birth is invalid")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because Patient Date of birth is invalid")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because Patient Date of birth is invalid" & vbCrLf)
                    blnIsValid = False
                End If


                Dim strgender As String = GetGender(objPrescription.RxPatient.Gender)
                If strgender.Trim.Length > 0 Then
                    Select Case strgender

                        Case "M", "F", "U"

                        Case Else
                            gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's gender is Invalid.  Please update the patient's demographic information")
                            'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's gender is Invaild.  Please update the patient's demographic information")
                            'Exit Function
                            ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's gender is Invaild.  Please update the patient's demographic information" & vbCrLf)
                            blnIsValid = False
                    End Select

                Else
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the patient's gender is Invaild.  Please update the patient's demographic information")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the patient's gender is Invalid.  Please update the patient's demographic information")
                    'Exit Function
                    ValidationMessageBuilder.Append("This prescription request can not be sent because the patient's gender is Invalid.  Please update the patient's demographic information" & vbCrLf)
                    blnIsValid = False
                End If
                If ValidationMessageBuilder.ToString.Trim.Length > 0 Then
                    ValidationMessage = ValidationMessageBuilder.ToString
                    ValidationMessageBuilder.Remove(0, ValidationMessageBuilder.Length)
                End If


            End If


            Dim drugname As String = ""

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugName) Then
                If objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim = "" Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug name is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the drug name is not available")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug name is not available" & vbCrLf)
                    blnIsValid = False
                Else
                    drugname = objPrescription.DrugsCol.Item(RefillItem).DrugName.Trim
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug name is not available")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the drug name is not available")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug name is not available" & vbCrLf)
                blnIsValid = False
            End If

            ''added in 6060  for multiple pharmacy logic

            If Not IsNothing(objPrescription.RxPharmacy.PharmacyID) Then
                If objPrescription.RxPharmacy.PharmacyID = "" Then '' added new condition of NCPDPid for case case GLO2011-0013803
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network ")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network for Drug " & drugname & "." & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy is not currently on the SureScripts network for Drug " & drugname & "." & vbCrLf)
                blnIsValid = False
            End If



            If Not IsNothing(objPrescription.RxPharmacy.PharmacyName) Then
                If objPrescription.RxPharmacy.PharmacyName.Trim = "" Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy name is not available")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy name is not available")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy name is not available for Drug " & drugname & "." & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the pharmacy name is not available")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the pharmacy name is not available")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the pharmacy name is not available for Drug " & drugname & "." & vbCrLf)
                blnIsValid = False
            End If


            Dim stramount As String = ""
            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) Then
                Dim sdrugAmountArray As String()
                sdrugAmountArray = objPrescription.DrugsCol.Item(RefillItem).DrugQuantity.Split(" ")
                If sdrugAmountArray.Length > 0 Then
                    stramount = sdrugAmountArray(0).Trim
                End If
            End If
            If IsNothing(objPrescription.DrugsCol.Item(RefillItem).DrugQuantity) AndAlso stramount = "" Then
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug quantity is not specified for Drug " & drugname & ".Please specify the quantity.")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the quantity is not specified.  Please specify the quantity.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug quantity is not specified for " & drugname & ".Please specify the quantity." & vbCrLf)
                blnIsValid = False
            ElseIf Not IsNumeric(stramount) Then
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug quantity is not a number.  Please specify the quantity with a number.")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the quantity is not a number.  Please specify the quantity with a number.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug quantity is not a number for " & drugname & ".Please specify the quantity with a number." & vbCrLf)
                blnIsValid = False
            ElseIf Val(stramount) = 0 Then
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug quantity is Zero for " & drugname & "  Please specify the quantity.")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the quantity is Zero.  Please specify the quantity.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug quantity is Zero for " & drugname & " Please specify the quantity." & vbCrLf)
                blnIsValid = False
            End If

            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).Directions) Then
                If objPrescription.DrugsCol.Item(RefillItem).Directions.Trim = "" Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug directions field is blank.  Please enter directions for taking this medication.")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the directions field is blank.  Please enter directions for taking this medication.")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug directions field is blank for " & drugname & ".Please enter directions for taking this medication." & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the drug directions field is blank.  Please enter directions for taking this medication.")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the directions field is blank.  Please enter directions for taking this medication.")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the drug directions field is blank for " & drugname & ".Please enter directions for taking this medication." & vbCrLf)
                blnIsValid = False
            End If


            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).WrittenDate) Then
                If Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate).Trim = "" Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the date of prescription is blank.  Please enter the date")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the date of prescription is blank.  Please enter the date")
                    'Exit Function
                    'validation of WrittenDate
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the date of prescription is blank " & drugname & ".Please enter the date" & vbCrLf)
                    blnIsValid = False
                End If
            Else
                gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the date of prescription is blank.  Please enter the date")
                'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the date of prescription is blank.  Please enter the date")
                'Exit Function
                'validation of WrittenDate
                ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the date of prescription is blank " & drugname & ".Please enter the date" & vbCrLf)
                blnIsValid = False
            End If


            If Not IsNothing(objPrescription.DrugsCol.Item(RefillItem).WrittenDate) Then
                If Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate).Trim <> "" Then
                    Dim strWrittenDate As String = Getdatetype(objPrescription.DrugsCol.Item(RefillItem).WrittenDate).Trim
                    If strWrittenDate.Length > 35 Then
                        gloSurescriptGeneral.UpdateLog("Message could not be generated as WrittenDate is invalid")
                        'gloSurescriptGeneral.InformationMessage("Message could not be generated as WrittenDate is invalid")
                        'Exit Function
                        ValidationMessageBuilderforDrug.Append("Message could not be generated as WrittenDate is invalid for " & drugname & vbCrLf)
                        blnIsValid = False
                    End If
                End If
            Else
                gloSurescriptGeneral.UpdateLog("Message could not be generated as WrittenDate is invalid")
                'gloSurescriptGeneral.InformationMessage("Message could not be generated as WrittenDate is invalid")
                'Exit Function
                ValidationMessageBuilderforDrug.Append("Message could not be generated as WrittenDate is invalid for " & drugname & vbCrLf)
                blnIsValid = False
            End If




            If objPrescription.DrugsCol.Item(RefillItem).RefillsQualifier <> "PRN" Then
                If Not IsNumeric(objPrescription.DrugsCol.Item(RefillItem).RefillQuantity) Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the refill quantity is not a number.  Please specify the refill quantity with a number.  ")
                    'gloSurescriptGeneral.InformationMessage("This prescription request can not be sent because the refill quantity is not a number.  Please specify the refill quantity with a number.  ")
                    'Exit Function
                    ValidationMessageBuilderforDrug.Append("This prescription request can not be sent because the refill quantity is not a number for " & drugname & ".Please specify the refill quantity with a number." & vbCrLf)
                    blnIsValid = False
                ElseIf objPrescription.DrugsCol.Item(RefillItem).RefillQuantity.Length > 2 Then
                    gloSurescriptGeneral.UpdateLog("This prescription request can not be sent because the refill quantity is too many characters.  Please enter the refill quantity in less than X characters.")

                End If
            End If

            Return blnIsValid
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
        End Try
    End Function

#End Region

#Region "Convert data before processing"

    Public Shared Function GetGender(ByVal strgender As String) As String
        Try
            Select Case strgender

                Case "Male"
                    Return "M"
                Case "Female"
                    Return "F"
                Case "M"
                    Return "M"
                Case "F"
                    Return "F"
                Case Else
                    Return "U"
            End Select
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function GetPhoneNumber(ByVal strphone As String) As String
        Dim strtemp As String = ""
        Try

            strtemp = strphone.Replace("(", "")
            strtemp = strtemp.Replace(")", "")
            strtemp = strtemp.Replace("-", "")
            strtemp = strtemp.Replace(" ", "")
            Return strtemp
        Catch ex As Exception
            Return strtemp
        End Try
    End Function
    Public Function Getdatetype(ByVal dtdate As DateTime) As String
        Dim strdate As String = ""
        Try

            If IsDate(dtdate) Then
                strdate = Format(dtdate, "yyyy-MM-dd")
                'Dim dttempdate As Date = dtdate.Date
                'Dim strtemp As String = dttempdate.Year.ToString & dttempdate.Month.ToString & dttempdate.Day.ToString
                Return strdate
            Else
                Return ""
            End If
        Catch ex As Exception
            Return strdate
        End Try
    End Function
    Private Function Getdatetypewithoutdash(ByVal dtdate As DateTime) As String
        Dim strdate As String = ""
        Try

            If IsDate(dtdate) Then
                strdate = Format(dtdate, "yyyyMMdd")
                Return strdate
            Else
                Return ""
            End If
        Catch ex As Exception
            Return strdate
        End Try
    End Function
    Private Function Getdatetimetype(ByVal dtdate As DateTime) As String
        Dim strdate As String = ""
        Try
            strdate = Format(dtdate, "yyyyMMddhhmmss")
            'Dim strtemp As String = dtdate.Year.ToString & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString
            Return strdate
        Catch ex As Exception
            Return strdate
        End Try
    End Function
#End Region



    Public Sub New()
        ValidationMessage = ""
        ValidationMessageBuilder = New System.Text.StringBuilder
        ValidationMessageBuilderforDrug = New System.Text.StringBuilder
        ValidationMessageBuilderfor10dot6 = New System.Text.StringBuilder
        ValidationMessageforDrug = ""
    End Sub

    Public Function GetEPAStatusAsRequiredInXML(ByVal Status As String) As String
        Dim sReturned As String = String.Empty

        Select Case Status.ToLower.Trim()
            Case "approved"
                sReturned = "A"
            Case "denied"
                sReturned = "D"
            Case "deferred"
                sReturned = "F"
            Case "closed"
                sReturned = "N"
            Case "requested"
                sReturned = "R"            
        End Select

        Return sReturned
    End Function

End Class 'end of class

