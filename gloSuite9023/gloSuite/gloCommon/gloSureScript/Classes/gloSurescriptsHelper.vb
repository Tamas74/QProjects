Imports Schema = gloGlobal.Schemas.Surescript
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Public Class gloSurescriptsHelper
    Inherits gloSurescriptsBase

    Public Event RequestDenied(ByVal statusMessage As StatusMessage, ByVal ResponseMessage As Object)
    Public Event RequestApproved(ByVal statusMessage As StatusMessage, ByVal ResponseMessage As Object)

    Public Property DrugIndex As Int32 = 0
    Public Property LoginProviderID As Int64 = 0


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal ServiceURL As String)
        MyBase.New(ServiceURL)
    End Sub

    Public Function GenerateRxChangeResponse(ByVal objPrescription As EPrescription, ByVal Index As Int32, ByVal XMLRequest As String, ByVal ReasonCode As String) As String

        Dim sFilePath As String = Nothing
        Dim xmlSerializer As Xml.Serialization.XmlSerializer = Nothing

        Dim RequestMessage As Schema.MessageType = Nothing
        Dim ResponseMessage As Schema.MessageType = Nothing

        Dim RequestBody As Schema.RxChangeRequest = Nothing
        Dim ResponseBody As Schema.RxChangeResponse = Nothing

        Dim strtemp As String = String.Empty
        Dim dtdate As DateTime = Date.UtcNow
        Dim ssInterface As gloSureScriptInterface = Nothing

        Try
            If Not String.IsNullOrEmpty(XMLRequest) AndAlso Not String.IsNullOrWhiteSpace(XMLRequest) Then
                Me.DrugIndex = Index
                RequestMessage = New gloGlobal.Schemas.Surescript.MessageType()

                Using reader As New StringReader(XMLRequest)
                    xmlSerializer = New Xml.Serialization.XmlSerializer(RequestMessage.GetType())
                    RequestMessage = xmlSerializer.Deserialize(reader)
                End Using

                If RequestMessage IsNot Nothing Then
                    If RequestMessage.Body IsNot Nothing AndAlso TypeOf (RequestMessage.Body.Item) Is Schema.RxChangeRequest Then
                        RequestBody = DirectCast(RequestMessage.Body.Item, Schema.RxChangeRequest)
                        ssInterface = New gloSureScriptInterface()

                        ResponseBody = Me.CloneResponse(DirectCast(RequestMessage.Body.Item, Schema.RxChangeRequest), ResponseBody)

                        ResponseMessage = Me.GetResponseEnvelope(RequestMessage, ResponseBody, objPrescription, ReasonCode)

                        If objPrescription.RxPharmacy IsNot Nothing Then
                            ResponseBody.Pharmacy = Me.GetPharmacy(objPrescription, ResponseBody.Pharmacy)
                        End If

                        If objPrescription.RxPrescriber IsNot Nothing Then
                            ResponseBody.Prescriber = Me.GetPrescriber(objPrescription, ResponseBody.Prescriber)
                        End If

                        If objPrescription.RxSupervisorPrescriber IsNot Nothing AndAlso objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName) Then
                            ResponseBody.Supervisor = Me.GetSupervisor(objPrescription, ResponseBody.Supervisor)
                        End If

                        ResponseBody.Patient = Me.GetPatient(objPrescription, ResponseBody.Patient)
                        ResponseBody.MedicationPrescribed = Me.GetMedicationPrescribed(objPrescription, ResponseBody.MedicationPrescribed)

                        objPrescription.DrugsCol.Item(DrugIndex).MessageID = ResponseMessage.Header.MessageID
                        objPrescription.DrugsCol.Item(DrugIndex).MessageName = "RxChangeResponse"
                        objPrescription.DrugsCol.Item(DrugIndex).RelatesToMessageId = ResponseMessage.Header.RelatesToMessageID
                        objPrescription.DrugsCol.Item(DrugIndex).MessageFrom = ResponseMessage.Header.From.Value
                        objPrescription.DrugsCol.Item(DrugIndex).MessageTo = ResponseMessage.Header.To.Value
                        objPrescription.DrugsCol.Item(DrugIndex).DateTimeStamp = Date.Now() 'ResponseMessage.Header.SentTime
                        objPrescription.DrugsCol.Item(DrugIndex).DateReceived = Date.Now()
                        objPrescription.DrugsCol.Item(DrugIndex).TransactionID = ResponseMessage.Header.PrescriberOrderNumber

                    End If
                End If

                sFilePath = gloSettings.FolderSettings.AppTempFolderPath & (gloGlobal.clsFileExtensions.GetUniqueDateString() & ".xml")
                xmlSerializer = New Xml.Serialization.XmlSerializer(ResponseMessage.GetType)

                Using sWriter As New StreamWriter(sFilePath, False)
                    xmlSerializer.Serialize(sWriter, ResponseMessage)
                End Using

                Return sFilePath
            End If

        Catch ex As Exception
            Throw ex
        Finally

            xmlSerializer = Nothing

            If ssInterface IsNot Nothing Then
                ssInterface.Dispose()
                ssInterface = Nothing
            End If
        End Try

        Return Nothing
    End Function

    Public Function GenerateRxChangeResponse(ByVal MessageID As String, XMLRequest As String, ReasonCode As String, Note As String) As Object

        Dim sReturned As String = String.Empty
        Dim xmlSerializer As Xml.Serialization.XmlSerializer = Nothing

        Dim RequestMessage As Schema.MessageType = Nothing
        Dim ResponseMessage As Schema.MessageType = Nothing

        Dim RequestBody As Schema.RxChangeRequest = Nothing
        Dim ResponseBody As Schema.RxChangeResponse = Nothing

        Dim DeniedResponse As Schema.DeniedType = Nothing

        Dim lstDenialReason As List(Of String) = Nothing

        Dim strtemp As String = String.Empty
        Dim dtdate As DateTime = Date.UtcNow
        Dim ssInterface As gloSureScriptInterface = Nothing

        Try
            If Not String.IsNullOrEmpty(XMLRequest) AndAlso Not String.IsNullOrWhiteSpace(XMLRequest) Then
                lstDenialReason = New List(Of String)
                lstDenialReason.Add(ReasonCode)
                RequestMessage = New gloGlobal.Schemas.Surescript.MessageType()

                Using reader As New StringReader(XMLRequest)
                    xmlSerializer = New Xml.Serialization.XmlSerializer(RequestMessage.GetType())
                    RequestMessage = xmlSerializer.Deserialize(reader)
                End Using

                If RequestMessage IsNot Nothing Then
                    If RequestMessage.Body IsNot Nothing AndAlso TypeOf (RequestMessage.Body.Item) Is Schema.RxChangeRequest Then
                        RequestBody = DirectCast(RequestMessage.Body.Item, Schema.RxChangeRequest)
                        ssInterface = New gloSureScriptInterface()

                        ResponseMessage = New Schema.MessageType()
                        ResponseMessage.Header = New Schema.HeaderType()
                        ResponseMessage.Header.To = New Schema.QualifiedMailAddressType()
                        ResponseMessage.Header.From = New Schema.QualifiedMailAddressType()
                        ResponseMessage.Body = New Schema.BodyType()

                        ResponseBody = New Schema.RxChangeResponse()
                        ResponseBody.Response = New Schema.ChangeResponseType()

                        DeniedResponse = New Schema.DeniedType()
                        DeniedResponse.DenialReason = Note
                        DeniedResponse.DenialReasonCode = lstDenialReason.ToArray()

                        ResponseMessage.version = "010"
                        ResponseMessage.release = "006"

                        ResponseMessage.Body.Item = ResponseBody
                        ResponseBody.Response.Item = DeniedResponse

                        strtemp = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

                        With ResponseMessage.Header
                            .To.Value = RequestMessage.Header.From.Value
                            .To.Qualifier = RequestMessage.Header.From.Qualifier

                            .From.Value = RequestMessage.Header.To.Value
                            .From.Qualifier = RequestMessage.Header.To.Qualifier

                            .MessageID = "DeniedResponse" & strtemp
                            .RelatesToMessageID = RequestMessage.Header.MessageID
                            .SentTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.FZ")

                            If Not String.IsNullOrEmpty(RequestMessage.Header.RxReferenceNumber) AndAlso Not String.IsNullOrWhiteSpace(RequestMessage.Header.RxReferenceNumber) Then
                                .RxReferenceNumber = RequestMessage.Header.RxReferenceNumber
                            End If

                            If Not String.IsNullOrEmpty(RequestMessage.Header.PrescriberOrderNumber) AndAlso Not String.IsNullOrWhiteSpace(RequestMessage.Header.PrescriberOrderNumber) Then
                                .PrescriberOrderNumber = RequestMessage.Header.PrescriberOrderNumber
                            End If
                        End With

                        ResponseBody = Me.CloneResponse(RequestBody, ResponseBody)

                    End If
                End If

                Return ResponseMessage

            End If
        Catch ex As Exception
            Throw ex
        Finally
            If lstDenialReason IsNot Nothing Then
                lstDenialReason.Clear()
                lstDenialReason = Nothing
            End If

            xmlSerializer = Nothing

            If ssInterface IsNot Nothing Then
                ssInterface.Dispose()
                ssInterface = Nothing
            End If
        End Try

        Return sReturned
    End Function

    Public Function SendCancelRxMessage(ByVal PrescriptionID As Int64, ByVal objPrescription As gloGlobal.Common.ServiceObjectBase.MedPrescribed, ByVal drPatient As DataRow, ByVal drProvider As DataRow, ByVal drPharmcy As DataRow, ByVal flag As String, ByVal sLoginName As String) As String
        Dim msg As String = String.Empty

        msg = Me.CancelRxMessage(PrescriptionID, objPrescription, drPatient, drProvider, drPharmcy, flag, sLoginName, Nothing)

        Return msg
    End Function

    Public Function SendCancelRxMessage(ByVal PrescriptionID As Int64, ByVal objPrescription As gloGlobal.Common.ServiceObjectBase.MedPrescribed, ByVal drPatient As DataRow, ByVal dtMessage As DataTable, ByVal flag As String, ByVal sLoginName As String) As String
        Dim msg As String = String.Empty
        Dim sFinalRetMsg As System.Text.StringBuilder = Nothing

        If dtMessage IsNot Nothing Then
            If dtMessage.Rows.Count > 0 Then
                sFinalRetMsg = New System.Text.StringBuilder
                For Each CurrRow As DataRow In dtMessage.Rows
                    msg = Me.CancelRxMessage(PrescriptionID, objPrescription, drPatient, Nothing, Nothing, flag, sLoginName, CurrRow)

                    If Not String.IsNullOrEmpty(msg) Then
                        sFinalRetMsg.Append(vbCrLf)
                        sFinalRetMsg.Append(msg)
                        sFinalRetMsg.Append(vbCrLf)
                    End If
                Next
            End If
        End If

        If sFinalRetMsg IsNot Nothing Then
            Return sFinalRetMsg.ToString()
        Else
            Return String.Empty
        End If

    End Function

    Public Function CancelRxMessage(ByVal PrescriptionID As Int64, ByVal objPrescription As gloGlobal.Common.ServiceObjectBase.MedPrescribed, ByVal drPatient As DataRow, ByVal drProvider As DataRow, ByVal drPharmcy As DataRow, ByVal flag As String, ByVal sLoginName As String, Optional ByVal drMessage As DataRow = Nothing) As String


        Dim xmlSerializer As Xml.Serialization.XmlSerializer = Nothing

        Dim CancelRxRequestMessage As Schema.MessageType = Nothing

        Dim CancelRxRequestBody As Schema.CancelRx = Nothing

        Dim strtemp As String = String.Empty
        Dim dtdate As DateTime = Date.UtcNow
        Dim ssInterface As gloSureScriptInterface = Nothing
        Dim changeResponseStatus As gloSurescriptsBase.ServiceResponse = Nothing
        Dim objSureScriptDBLayer As gloSureScriptDBLayer = Nothing
        Dim statusMesage As New StatusMessage
        Dim strmessage As System.Text.StringBuilder = Nothing
        Dim StatusMessageType As String = ""
        Dim sCancellationType As String = ""
        Dim nPatientID As Int64 = 0
        Try

            objSureScriptDBLayer = New gloSureScriptDBLayer()

            ssInterface = New gloSureScriptInterface()

            CancelRxRequestMessage = New Schema.MessageType()
            CancelRxRequestMessage.Header = New Schema.HeaderType()
            CancelRxRequestMessage.Header.To = New Schema.QualifiedMailAddressType()
            CancelRxRequestMessage.Header.From = New Schema.QualifiedMailAddressType()
            CancelRxRequestMessage.Body = New Schema.BodyType()

            CancelRxRequestBody = New Schema.CancelRx()
            CancelRxRequestBody.Request = New Schema.CancelRequestType()

            CancelRxRequestMessage.version = "010"
            CancelRxRequestMessage.release = "006"

            strtemp = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

            CancelRxRequestMessage.Body.Item = CancelRxRequestBody

            CancelRxRequestBody.Request = New Schema.CancelRequestType()
            CancelRxRequestBody.Request.ChangeofPrescriptionStatusFlag = flag '"C"

            If flag = "C" Then
                sCancellationType = "Cancel"
            ElseIf flag = "D" Then
                sCancellationType = "Discontinue"
            End If

            If drPatient.Table.Columns.Contains("nPatientID") AndAlso Not IsDBNull(drPatient("nPatientID")) Then
                Dim sPatientID As String = Convert.ToString(drPatient("nPatientID"))
                Int64.TryParse(sPatientID, nPatientID)
            End If

            With CancelRxRequestMessage.Header
                If drMessage IsNot Nothing Then
                    .To.Value = drMessage("sNCPDPID")
                Else
                    .To.Value = drPharmcy("sNCPDPID")
                End If

                .To.Qualifier = "P"

                If drMessage IsNot Nothing Then
                    .From.Value = drMessage("sPrSPIID")
                Else
                    .From.Value = drProvider("sPrSPIID")
                End If

                .From.Qualifier = "D"

                .MessageID = "CancelRx" & strtemp

                If drMessage IsNot Nothing Then
                    .RelatesToMessageID = drMessage("MessageID")
                Else
                    .RelatesToMessageID = ""
                End If

                .SentTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.FZ")

                .PrescriberOrderNumber = PrescriptionID

            End With

            CancelRxRequestBody.Patient = Me.GetPatient(drPatient, CancelRxRequestBody.Patient)

            If drMessage IsNot Nothing Then
                CancelRxRequestBody.Pharmacy = Me.GetPharmacy(drMessage, CancelRxRequestBody.Pharmacy)
                CancelRxRequestBody.Prescriber = Me.GetPrescriber(drMessage, CancelRxRequestBody.Prescriber)
            Else
                CancelRxRequestBody.Pharmacy = Me.GetPharmacy(drPharmcy, CancelRxRequestBody.Pharmacy)
                CancelRxRequestBody.Prescriber = Me.GetPrescriber(drProvider, CancelRxRequestBody.Prescriber)
            End If


            CancelRxRequestBody.MedicationPrescribed = Me.GetMedicationPrescribed(objPrescription, CancelRxRequestBody.MedicationPrescribed)

            If Not IsNothing(CancelRxRequestMessage) Then
                changeResponseStatus = PostRxMessage(CancelRxRequestMessage)
            End If

            Dim bSuccess As Boolean = True

            Try
                statusMesage = ReadStatusMessage(changeResponseStatus.file, gloSureScriptInterface.SentMessageType.Cancel, "")
            Catch ex As Exception
                bSuccess = False
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.Cancle, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

            If Not bSuccess Or statusMesage Is Nothing Then
                Return StatusMessageType
            End If

            objSureScriptDBLayer.InsertintoMessageTransaction(CancelRxRequestMessage, "CancelRxRequest")


            If statusMesage.MessageName <> "Error" Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.CancelOperation, "CancelRx (" + sCancellationType + ") request sent", nPatientID, PrescriptionID, LoginProviderID, gloAuditTrail.ActivityOutCome.Success)
                If objSureScriptDBLayer.InsertAcknowledgements(statusMesage, True) Then

                    If objSureScriptDBLayer.InsertintoMessageTransaction(CType(statusMesage, SureScriptMessage)) Then

                    End If
                End If

                Using p As New gloSureScript.gloSureScriptDBLayer()
                    If flag = "C" Then
                        p.UpdateMedicationStatus(nPatientID, PrescriptionID, "Cancelled", sLoginName)
                    ElseIf flag = "D" Then
                        p.UpdateMedicationStatus(nPatientID, PrescriptionID, "Discontinued", sLoginName)
                    End If

                End Using
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.CancelOperation, "CancelRx (" + sCancellationType + ") request sent", nPatientID, PrescriptionID, LoginProviderID, gloAuditTrail.ActivityOutCome.Failure)
            End If


            strmessage = New System.Text.StringBuilder
            strmessage.Append("Drug Name: " & CancelRxRequestBody.MedicationPrescribed.DrugDescription)
            strmessage.Append(vbCrLf)
            If flag = "C" Then
                strmessage.Append("Transaction: CancelRx (Cancel)")
            ElseIf flag = "D" Then
                strmessage.Append("Transaction: CancelRx (Discontinue)")
            End If

            If statusMesage.MessageName = "Status" Then
                strmessage.Append(vbCrLf)
                strmessage.Append("Status: Posted Successfully")
                If Not IsNothing(statusMesage.StatusCode) Then
                    strmessage.Append(vbCrLf)
                    strmessage.Append("Status code: " & statusMesage.StatusCode)
                    strmessage.Append(vbCrLf)
                    If statusMesage.StatusCode = "000" Then
                        strmessage.Append("Description :Transmission successful ")
                    ElseIf statusMesage.StatusCode = "010" Then
                        strmessage.Append("Description :Successfully accepted by ultimate receiver ")
                    End If
                End If
                If Not IsNothing(statusMesage.Description) Then
                    strmessage.Append(vbCrLf)
                    strmessage.Append(statusMesage.Description)
                End If
            ElseIf statusMesage.MessageName = "Verify" Then
                strmessage.Append(vbCrLf)
                strmessage.Append("Status: Posted Successfully")
                If Not IsNothing(statusMesage.StatusCode) Then
                    strmessage.Append(vbCrLf)
                    strmessage.Append("Status code: " & statusMesage.StatusCode)
                End If
                If Not IsNothing(statusMesage.Description) Then
                    strmessage.Append(vbCrLf)
                    strmessage.Append(statusMesage.Description)
                End If
            ElseIf statusMesage.MessageName = "Error" Then
                strmessage.Append(vbCrLf)
                strmessage.Append("Status: Could not be Posted Successfully")
                If Not IsNothing(statusMesage.StatusCode) Then
                    strmessage.Append(vbCrLf)
                    strmessage.Append("Error code: " & statusMesage.StatusCode)
                End If
                If Not IsNothing(statusMesage.Description) Then
                    strmessage.Append(vbCrLf)
                    strmessage.Append("Error Description: " & statusMesage.Description)
                End If
            End If

            strmessage.Append(vbCrLf)
            strmessage.Append("MessageID: " & statusMesage.MessageID)

            StatusMessageType = strmessage.ToString

        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.ToString(), "gloEMR")
        End Try

        Return StatusMessageType
    End Function

#Region "Deny Request"

    Public Sub DenyRxChangeRequest(ByVal MessageID As String, XMLRequest As String, ReasonCode As String, Note As String)
        Dim changeResponse = Nothing
        Dim changeResponseStatus As gloSurescriptsBase.ServiceResponse = Nothing
        Dim statusMesage As New StatusMessage
        Dim sFileText As String = ""
        Try
            changeResponse = GenerateRxChangeResponse(MessageID, XMLRequest, ReasonCode, Note)

            If Not IsNothing(changeResponse) Then
                changeResponseStatus = PostRxMessage(changeResponse)
            End If

            If changeResponseStatus IsNot Nothing AndAlso File.Exists(changeResponseStatus.file) Then
                sFileText = File.ReadAllText(changeResponseStatus.file)
                If sFileText.ToLower().Contains("listener not started") Then
                    MessageBox.Show(sFileText, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If

            statusMesage = ReadStatusMessage(changeResponseStatus.file, gloSureScriptInterface.SentMessageType.eDenied, "")
            RaiseEvent RequestDenied(statusMesage, changeResponse)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Windows.Forms.MessageBox.Show(ex.ToString(), "gloEMR")
        End Try

    End Sub

#End Region

#Region "Clone Elements"

    Private Function CloneResponse(ByVal RequestBody As Schema.RxChangeRequest, ByVal ResponseBody As Schema.RxChangeResponse) As Schema.RxChangeResponse

        If ResponseBody Is Nothing Then
            ResponseBody = New Schema.RxChangeResponse()
        End If

        Try
            If RequestBody.Request IsNot Nothing Then
                ResponseBody.Request = RequestBody.Request
            End If

            If RequestBody.Pharmacy IsNot Nothing Then
                ResponseBody.Pharmacy = Me.ClonePharmacy(RequestBody)
            End If

            If RequestBody.Prescriber IsNot Nothing Then
                ResponseBody.Prescriber = RequestBody.Prescriber
            End If

            If RequestBody.Supervisor IsNot Nothing Then
                ResponseBody.Supervisor = RequestBody.Supervisor
            End If

            'If RequestBody.Facility IsNot Nothing Then
            '    returned.Facility = RequestBody.Facility
            'End If

            If RequestBody.Patient IsNot Nothing Then
                ResponseBody.Patient = RequestBody.Patient
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return ResponseBody
    End Function

    Private Function ClonePharmacy(RequestBody As gloGlobal.Schemas.Surescript.RxChangeRequest) As gloGlobal.Schemas.Surescript.MandatoryPharmacyType
        Dim returned As New Schema.MandatoryPharmacyType()
        Try
            With returned
                If RequestBody.Pharmacy.Address IsNot Nothing Then
                    .Address = RequestBody.Pharmacy.Address
                End If

                If RequestBody.Pharmacy.CommunicationNumbers IsNot Nothing Then
                    .CommunicationNumbers = RequestBody.Pharmacy.CommunicationNumbers
                End If

                If RequestBody.Pharmacy.Identification IsNot Nothing Then
                    .Identification = RequestBody.Pharmacy.Identification
                End If

                If RequestBody.Pharmacy.Pharmacist IsNot Nothing Then
                    .Pharmacist = RequestBody.Pharmacy.Pharmacist
                End If

                If RequestBody.Pharmacy.Specialty IsNot Nothing Then
                    .Specialty = RequestBody.Pharmacy.Specialty
                End If

                If RequestBody.Pharmacy.StoreName IsNot Nothing Then
                    .StoreName = RequestBody.Pharmacy.StoreName
                End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return returned
    End Function

#End Region

#Region "Get Elements"

    Private Function GetPrescriber(objPrescription As EPrescription, ByVal InputPrescriber As Schema.PrescriberType) As Schema.PrescriberType

        Dim PrescriberAddress As Schema.AddressType = Nothing
        Dim lstPrescriberComm As New List(Of Schema.CommunicationType)
        Dim PrescriberIdentification As Schema.MandatoryProviderIDType = Nothing
        Dim lstPrescriberID As New List(Of String)
        Dim lstPrescriberIDType As New List(Of Schema.ItemsChoiceType)
        Dim Name As Schema.MandatoryNameType = Nothing

        Try
            If InputPrescriber Is Nothing Then
                InputPrescriber = New Schema.PrescriberType()
            End If

            If InputPrescriber.Address Is Nothing Then
                PrescriberAddress = New Schema.AddressType()
                InputPrescriber.Address = PrescriberAddress
            Else
                PrescriberAddress = InputPrescriber.Address
            End If

            If objPrescription.RxPrescriber.PrescriberAddress IsNot Nothing Then
                With PrescriberAddress
                    If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberAddress.Address1) Then
                        If objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Length > 35 Then
                            .AddressLine1 = objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Substring(0, 35)
                        Else
                            .AddressLine1 = objPrescription.RxPrescriber.PrescriberAddress.Address1.Trim()
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberAddress.Address2) Then
                        If objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Length > 35 Then
                            .AddressLine2 = objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Substring(0, 35)
                        Else
                            .AddressLine2 = objPrescription.RxPrescriber.PrescriberAddress.Address2.Trim()
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberAddress.City) Then
                        If objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Length > 35 Then
                            .City = objPrescription.RxPrescriber.PrescriberAddress.City.Trim.Substring(0, 35)
                        Else
                            .City = objPrescription.RxPrescriber.PrescriberAddress.City.Trim()
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberAddress.State) Then
                        If objPrescription.RxPrescriber.PrescriberAddress.State.Trim.Length > 9 Then
                            .State = objPrescription.RxPrescriber.PrescriberAddress.State.Trim.Substring(0, 9)
                        Else
                            .State = objPrescription.RxPrescriber.PrescriberAddress.State.Trim()
                        End If

                    End If

                    If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberAddress.Zip) Then
                        If objPrescription.RxPrescriber.PrescriberAddress.Zip.Length > 11 Then
                            .ZipCode = objPrescription.RxPrescriber.PrescriberAddress.Zip.Substring(0, 11)
                        Else
                            .ZipCode = objPrescription.RxPrescriber.PrescriberAddress.Zip
                        End If
                    End If


                    If Not String.IsNullOrWhiteSpace(.AddressLine2) Then
                        .PlaceLocationQualifier = "AD2"
                    End If
                End With
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.ClinicName) Then
                InputPrescriber.ClinicName = objPrescription.ClinicName
            End If

            If objPrescription.RxPrescriber.PrescriberPhone IsNot Nothing Then
                Dim commPrescriber As Schema.CommunicationType = Nothing

                Dim gloCommPrescriber As gloSureScript.PhoneNumber = objPrescription.RxPrescriber.PrescriberPhone

                If Not String.IsNullOrWhiteSpace(gloCommPrescriber.Email) Then
                    commPrescriber = New Schema.CommunicationType()
                    commPrescriber.Number = gloCommPrescriber.Email
                    commPrescriber.Qualifier = "EM"
                    lstPrescriberComm.Add(commPrescriber)
                End If

                If Not String.IsNullOrWhiteSpace(gloCommPrescriber.Fax) Then
                    commPrescriber = New Schema.CommunicationType()
                    commPrescriber.Number = gloCommPrescriber.Fax
                    commPrescriber.Qualifier = "FX"
                    lstPrescriberComm.Add(commPrescriber)
                End If

                If Not String.IsNullOrWhiteSpace(gloCommPrescriber.Mobile) Then
                    commPrescriber = New Schema.CommunicationType()
                    commPrescriber.Number = gloCommPrescriber.Mobile
                    commPrescriber.Qualifier = "CP"
                    lstPrescriberComm.Add(commPrescriber)
                End If

                If Not String.IsNullOrWhiteSpace(gloCommPrescriber.Pager) Then
                    commPrescriber = New Schema.CommunicationType()
                    commPrescriber.Number = gloCommPrescriber.Pager
                    commPrescriber.Qualifier = "BN"
                    lstPrescriberComm.Add(commPrescriber)
                End If

                If Not String.IsNullOrWhiteSpace(gloCommPrescriber.Phone) Then
                    commPrescriber = New Schema.CommunicationType()
                    commPrescriber.Number = gloCommPrescriber.Phone
                    commPrescriber.Qualifier = "TE"
                    lstPrescriberComm.Add(commPrescriber)
                End If

                If lstPrescriberComm.Any() Then
                    InputPrescriber.CommunicationNumbers = lstPrescriberComm.ToArray()
                End If
            End If

            If InputPrescriber.Identification Is Nothing Then
                PrescriberIdentification = New Schema.MandatoryProviderIDType()
                InputPrescriber.Identification = PrescriberIdentification
            Else
                PrescriberIdentification = InputPrescriber.Identification
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberNPI) Then
                lstPrescriberID.Add(objPrescription.RxPrescriber.PrescriberNPI)
                lstPrescriberIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.NPI)
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberDEA) Then
                lstPrescriberID.Add(objPrescription.RxPrescriber.PrescriberDEA)
                lstPrescriberIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.DEANumber)
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberSSN) Then
                lstPrescriberID.Add(objPrescription.RxPrescriber.PrescriberSSN)
                lstPrescriberIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.SocialSecurity)
            End If

            If lstPrescriberID.Any() AndAlso lstPrescriberIDType.Any() Then
                PrescriberIdentification.Items = lstPrescriberID.ToArray()
                PrescriberIdentification.ItemsElementName = lstPrescriberIDType.ToArray()
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberName.FirstName) Then

                If InputPrescriber.Name Is Nothing Then
                    Name = New Schema.MandatoryNameType()
                    InputPrescriber.Name = Name
                Else
                    Name = InputPrescriber.Name
                End If

                With Name
                    If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberName.FirstName) Then
                        If objPrescription.RxPrescriber.PrescriberName.FirstName.Length > 35 Then
                            .FirstName = objPrescription.RxPrescriber.PrescriberName.FirstName.Substring(0, 35)
                        Else
                            .FirstName = objPrescription.RxPrescriber.PrescriberName.FirstName
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberName.MiddleName) Then
                        .MiddleName = objPrescription.RxPrescriber.PrescriberName.MiddleName
                    End If

                    If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberName.LastName) Then
                        If objPrescription.RxPrescriber.PrescriberName.LastName.Length > 35 Then
                            .LastName = objPrescription.RxPrescriber.PrescriberName.LastName.Substring(0, 35)
                        Else
                            .LastName = objPrescription.RxPrescriber.PrescriberName.LastName
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberName.Prefix) Then
                        .Prefix = objPrescription.RxPrescriber.PrescriberName.Prefix
                    End If

                    If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescriberName.Suffix) Then
                        .Suffix = objPrescription.RxPrescriber.PrescriberName.Suffix
                    End If
                End With
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescSpcltyCode) Then
                InputPrescriber.Specialty = objPrescription.RxPrescriber.PrescSpcltyCode
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return InputPrescriber
    End Function

    Private Function GetPrescriber(drPrescriber As DataRow, ByVal InputPrescriber As Schema.CancelPrescriberType) As Schema.CancelPrescriberType

        Dim PrescriberAddress As Schema.AddressType = Nothing
        Dim lstPrescriberComm As New List(Of Schema.CommunicationType)
        Dim PrescriberIdentification As Schema.MandatoryProviderIDType = Nothing
        Dim lstPrescriberID As New List(Of String)
        Dim lstPrescriberIDType As New List(Of Schema.ItemsChoiceType)
        Dim Name As Schema.MandatoryNameType = Nothing

        Try
            If InputPrescriber Is Nothing Then
                InputPrescriber = New Schema.CancelPrescriberType()
            End If

            If InputPrescriber.Address Is Nothing Then
                PrescriberAddress = New Schema.AddressType()
                InputPrescriber.Address = PrescriberAddress
            Else
                PrescriberAddress = InputPrescriber.Address
            End If

            If drPrescriber IsNot Nothing Then
                With PrescriberAddress
                    If Not String.IsNullOrWhiteSpace(drPrescriber("sPrAddressLine")) Then
                        If drPrescriber("sPrAddressLine").Trim.Length > 35 Then
                            .AddressLine1 = drPrescriber("sPrAddressLine").Trim.Substring(0, 35)
                        Else
                            .AddressLine1 = drPrescriber("sPrAddressLine").Trim()
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPrescriber("sPrAddressLine2")) Then
                        If drPrescriber("sPrAddressLine2").Trim.Length > 35 Then
                            .AddressLine2 = drPrescriber("sPrAddressLine2").Trim.Substring(0, 35)
                        Else
                            .AddressLine2 = drPrescriber("sPrAddressLine2").Trim()
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPrescriber("sPrCity")) Then
                        If drPrescriber("sPrCity").Trim.Length > 35 Then
                            .City = drPrescriber("sPrCity").Trim.Substring(0, 35)
                        Else
                            .City = drPrescriber("sPrCity").Trim()
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPrescriber("sPrState")) Then
                        If drPrescriber("sPrState").Trim.Length > 9 Then
                            .State = drPrescriber("sPrState").Trim.Substring(0, 9)
                        Else
                            .State = drPrescriber("sPrState").Trim()
                        End If

                    End If

                    If Not String.IsNullOrWhiteSpace(drPrescriber("sPrZip")) Then
                        If drPrescriber("sPrZip").Length > 11 Then
                            .ZipCode = drPrescriber("sPrZip").Substring(0, 11)
                        Else
                            .ZipCode = drPrescriber("sPrZip")
                        End If
                    End If


                    If Not String.IsNullOrWhiteSpace(.AddressLine2) Then
                        .PlaceLocationQualifier = "AD2"
                    End If
                End With
            End If

            'If Not String.IsNullOrWhiteSpace(objPrescription.ClinicName) Then
            '    InputPrescriber.ClinicName = objPrescription.ClinicName
            'End If

            'If objPrescription.RxPrescriber.PrescriberPhone IsNot Nothing Then
            Dim commPrescriber As Schema.CommunicationType = Nothing

            ' Dim gloCommPrescriber As gloSureScript.PhoneNumber = dtPrescriber("Provideraddress1")

            If Not String.IsNullOrWhiteSpace(drPrescriber("sPrEMail")) Then 'EMAIL
                commPrescriber = New Schema.CommunicationType()
                commPrescriber.Number = drPrescriber("sPrEMail")
                commPrescriber.Qualifier = "EM"
                lstPrescriberComm.Add(commPrescriber)
            End If

            If Not String.IsNullOrWhiteSpace(drPrescriber("sPrFax")) Then 'FAX
                commPrescriber = New Schema.CommunicationType()
                commPrescriber.Number = drPrescriber("sPrFax")
                commPrescriber.Qualifier = "FX"
                lstPrescriberComm.Add(commPrescriber)
            End If

            If Not String.IsNullOrWhiteSpace(drPrescriber("sPrMobile")) Then 'M
                commPrescriber = New Schema.CommunicationType()
                commPrescriber.Number = drPrescriber("sPrMobile")
                commPrescriber.Qualifier = "CP"
                lstPrescriberComm.Add(commPrescriber)
            End If

            If Not String.IsNullOrWhiteSpace(drPrescriber("sPrPager")) Then 'Pag
                commPrescriber = New Schema.CommunicationType()
                commPrescriber.Number = drPrescriber("sPrPager")
                commPrescriber.Qualifier = "BN"
                lstPrescriberComm.Add(commPrescriber)
            End If

            If Not String.IsNullOrWhiteSpace(drPrescriber("sPrPhone")) Then 'Ph
                commPrescriber = New Schema.CommunicationType()
                commPrescriber.Number = drPrescriber("sPrPhone")
                commPrescriber.Qualifier = "TE"
                lstPrescriberComm.Add(commPrescriber)
            End If

            If lstPrescriberComm.Any() Then
                InputPrescriber.CommunicationNumbers = lstPrescriberComm.ToArray()
            End If
            'End If

            If InputPrescriber.Identification Is Nothing Then
                PrescriberIdentification = New Schema.MandatoryProviderIDType()
                InputPrescriber.Identification = PrescriberIdentification
            Else
                PrescriberIdentification = InputPrescriber.Identification
            End If

            If Not String.IsNullOrWhiteSpace(drPrescriber("sPrNPI")) Then
                lstPrescriberID.Add(drPrescriber("sPrNPI"))
                lstPrescriberIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.NPI)
            End If

            If Not String.IsNullOrWhiteSpace(drPrescriber("sPrDEA")) Then
                lstPrescriberID.Add(drPrescriber("sPrDEA"))
                lstPrescriberIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.DEANumber)
            End If

            'If Not String.IsNullOrWhiteSpace(drPrescriber("sPrSSN")) Then
            '    lstPrescriberID.Add(drPrescriber("sPrSSN"))
            '    lstPrescriberIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.SocialSecurity)
            'End If

            If lstPrescriberID.Any() AndAlso lstPrescriberIDType.Any() Then
                PrescriberIdentification.Items = lstPrescriberID.ToArray()
                PrescriberIdentification.ItemsElementName = lstPrescriberIDType.ToArray()
            End If

            If Not String.IsNullOrWhiteSpace(drPrescriber("sPrFirstName")) Then

                If InputPrescriber.Name Is Nothing Then
                    Name = New Schema.MandatoryNameType()
                    InputPrescriber.Name = Name
                Else
                    Name = InputPrescriber.Name
                End If

                With Name
                    If Not String.IsNullOrWhiteSpace(drPrescriber("sPrFirstName")) Then
                        If drPrescriber("sPrFirstName").Length > 35 Then
                            .FirstName = drPrescriber("sPrFirstName").Substring(0, 35)
                        Else
                            .FirstName = drPrescriber("sPrFirstName")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPrescriber("sPrMiddleName")) Then
                        .MiddleName = drPrescriber("sPrMiddleName")
                    End If

                    If Not String.IsNullOrWhiteSpace(drPrescriber("sPrLastName")) Then
                        If drPrescriber("sPrLastName").Length > 35 Then
                            .LastName = drPrescriber("sPrLastName").Substring(0, 35)
                        Else
                            .LastName = drPrescriber("sPrLastName")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPrescriber("sPrefix")) Then
                        .Prefix = drPrescriber("sPrefix")
                    End If

                    If Not String.IsNullOrWhiteSpace(drPrescriber("sSuffix")) Then
                        .Suffix = drPrescriber("sSuffix")
                    End If
                End With
            End If

            'If Not String.IsNullOrWhiteSpace(objPrescription.RxPrescriber.PrescSpcltyCode) Then
            '    InputPrescriber.Specialty = objPrescription.RxPrescriber.PrescSpcltyCode
            'End If
        Catch ex As Exception
            Throw ex
        End Try
        Return InputPrescriber
    End Function

    Private Function GetPharmacy(objPrescription As EPrescription, ByVal Pharmacy As Schema.MandatoryPharmacyType) As Schema.MandatoryPharmacyType
        Dim lstPharmacyID As New List(Of String)
        Dim lstPharmacyIDType As New List(Of Schema.ItemsChoiceType)

        Dim lstPrescriberID As New List(Of String)
        Dim lstPrescriberIDType As New List(Of Schema.ItemsChoiceType)

        Dim PharmacyIdentification As New Schema.MandatoryProviderIDType()
        Dim PrescriberIdentification As New Schema.MandatoryProviderIDType()

        Dim PharmacistName As New Schema.NameType()

        Try
            If Pharmacy Is Nothing Then
                Pharmacy = New Schema.MandatoryPharmacyType()
            End If

            If Pharmacy.Address Is Nothing Then
                Pharmacy.Address = New Schema.AddressType()
            End If

            With Pharmacy.Address
                If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PhAddressline1) Then
                    If objPrescription.DrugsCol.Item(DrugIndex).PhAddressline1.Length > 35 Then
                        .AddressLine1 = objPrescription.DrugsCol.Item(DrugIndex).PhAddressline1.Substring(0, 35)
                    Else
                        .AddressLine1 = objPrescription.DrugsCol.Item(DrugIndex).PhAddressline1
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PhAddressline2) Then
                    If objPrescription.DrugsCol.Item(DrugIndex).PhAddressline2.Length > 35 Then
                        .AddressLine2 = objPrescription.DrugsCol.Item(DrugIndex).PhAddressline2.Substring(0, 35)
                    Else
                        .AddressLine2 = objPrescription.DrugsCol.Item(DrugIndex).PhAddressline2
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PhCity) Then
                    If objPrescription.DrugsCol.Item(DrugIndex).PhCity.Length > 35 Then
                        .City = objPrescription.DrugsCol.Item(DrugIndex).PhCity.Substring(0, 35)
                    Else
                        .City = objPrescription.DrugsCol.Item(DrugIndex).PhCity
                    End If

                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PhState) Then
                    If objPrescription.DrugsCol.Item(DrugIndex).PhState.Length > 9 Then
                        .State = objPrescription.DrugsCol.Item(DrugIndex).PhState.Substring(0, 9)
                    Else
                        .State = objPrescription.DrugsCol.Item(DrugIndex).PhState
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PhZip) Then
                    If objPrescription.DrugsCol.Item(DrugIndex).PhZip.Length > 11 Then
                        .ZipCode = objPrescription.DrugsCol.Item(DrugIndex).PhZip.Substring(0, 11)
                    Else
                        .ZipCode = objPrescription.DrugsCol.Item(DrugIndex).PhZip
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(.AddressLine2) Then
                    .PlaceLocationQualifier = "AD2"
                End If
            End With

            Dim lstComm As New List(Of Schema.CommunicationType)

            Dim comm As Schema.CommunicationType = Nothing

            If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PhEmail) Then
                comm = New Schema.CommunicationType()
                comm.Number = objPrescription.DrugsCol.Item(DrugIndex).PhEmail
                comm.Qualifier = "EM"
                lstComm.Add(comm)
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PhFax) Then
                comm = New Schema.CommunicationType()
                comm.Number = objPrescription.DrugsCol.Item(DrugIndex).PhFax
                comm.Qualifier = "FX"
                lstComm.Add(comm)
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PhPhone) Then
                comm = New Schema.CommunicationType()
                comm.Number = objPrescription.DrugsCol.Item(DrugIndex).PhPhone
                comm.Qualifier = "TE"
                lstComm.Add(comm)
            End If

            If lstComm.Any() Then
                Pharmacy.CommunicationNumbers = lstComm.ToArray()
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PhNPI) Then
                lstPharmacyID.Add(objPrescription.DrugsCol.Item(DrugIndex).PhNPI)
                lstPharmacyIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.NPI)
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PhNCPDPID) Then
                If objPrescription.DrugsCol.Item(DrugIndex).PhNCPDPID.Length > 35 Then
                    lstPharmacyID.Add(objPrescription.DrugsCol.Item(DrugIndex).PhNCPDPID.Substring(0, 35))
                Else
                    lstPharmacyID.Add(objPrescription.DrugsCol.Item(DrugIndex).PhNCPDPID)
                End If
                lstPharmacyIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.NCPDPID)
            End If

            If lstPharmacyID.Any() AndAlso lstPharmacyIDType.Any() Then
                PharmacyIdentification.Items = lstPharmacyID.ToArray()
                PharmacyIdentification.ItemsElementName = lstPharmacyIDType.ToArray()
                Pharmacy.Identification = PharmacyIdentification
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.RxPharmacy.PharmacySpeciality) Then
                Pharmacy.Specialty = objPrescription.RxPharmacy.PharmacySpeciality
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PharmacyName) Then
                If objPrescription.DrugsCol.Item(DrugIndex).PharmacyName.Length > 35 Then
                    Pharmacy.StoreName = objPrescription.DrugsCol.Item(DrugIndex).PharmacyName.Substring(0, 35)
                Else
                    Pharmacy.StoreName = objPrescription.DrugsCol.Item(DrugIndex).PharmacyName
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return Pharmacy
    End Function

    Private Function GetPharmacy(drPharmacy As DataRow, ByVal Pharmacy As Schema.MandatoryPharmacyType) As Schema.MandatoryPharmacyType
        Dim lstPharmacyID As New List(Of String)
        Dim lstPharmacyIDType As New List(Of Schema.ItemsChoiceType)

        Dim lstPrescriberID As New List(Of String)
        Dim lstPrescriberIDType As New List(Of Schema.ItemsChoiceType)

        Dim PharmacyIdentification As New Schema.MandatoryProviderIDType()
        Dim PrescriberIdentification As New Schema.MandatoryProviderIDType()

        Dim PharmacistName As New Schema.NameType()

        Try
            If Pharmacy Is Nothing Then
                Pharmacy = New Schema.MandatoryPharmacyType()
            End If

            If drPharmacy IsNot Nothing Then
                If Pharmacy.Address Is Nothing Then
                    Pharmacy.Address = New Schema.AddressType()
                End If

                With Pharmacy.Address
                    If Not String.IsNullOrWhiteSpace(drPharmacy("sAddressLine1")) Then
                        If drPharmacy("sAddressLine1").Length > 35 Then
                            .AddressLine1 = drPharmacy("sAddressLine1").Substring(0, 35)
                        Else
                            .AddressLine1 = drPharmacy("sAddressLine1")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPharmacy("sAddressLine2")) Then
                        If drPharmacy("sAddressLine2").Length > 35 Then
                            .AddressLine2 = drPharmacy("sAddressLine2").Substring(0, 35)
                        Else
                            .AddressLine2 = drPharmacy("sAddressLine2")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPharmacy("sCity")) Then
                        If drPharmacy("sCity").Length > 35 Then
                            .City = drPharmacy("sCity").Substring(0, 35)
                        Else
                            .City = drPharmacy("sCity")
                        End If

                    End If

                    If Not String.IsNullOrWhiteSpace(drPharmacy("sState")) Then
                        If drPharmacy("sState").Length > 9 Then
                            .State = drPharmacy("sState").Substring(0, 9)
                        Else
                            .State = drPharmacy("sState")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPharmacy("sZIP")) Then
                        If drPharmacy("sZIP").Length > 11 Then
                            .ZipCode = drPharmacy("sZIP").Substring(0, 11)
                        Else
                            .ZipCode = drPharmacy("sZIP")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(.AddressLine2) Then
                        .PlaceLocationQualifier = "AD2"
                    End If
                End With

                Dim lstComm As New List(Of Schema.CommunicationType)

                Dim comm As Schema.CommunicationType = Nothing

                If Not String.IsNullOrWhiteSpace(drPharmacy("sEmail")) Then
                    comm = New Schema.CommunicationType()
                    comm.Number = drPharmacy("sEmail")
                    comm.Qualifier = "EM"
                    lstComm.Add(comm)
                End If

                If Not String.IsNullOrWhiteSpace(drPharmacy("sFax")) Then
                    comm = New Schema.CommunicationType()
                    comm.Number = drPharmacy("sFax")
                    comm.Qualifier = "FX"
                    lstComm.Add(comm)
                End If

                If Not String.IsNullOrWhiteSpace(drPharmacy("sPhone")) Then
                    comm = New Schema.CommunicationType()
                    comm.Number = drPharmacy("sPhone")
                    comm.Qualifier = "TE"
                    lstComm.Add(comm)
                End If

                If lstComm.Any() Then
                    Pharmacy.CommunicationNumbers = lstComm.ToArray()
                End If

                If Not String.IsNullOrWhiteSpace(drPharmacy("sNPI")) Then  'NPI
                    lstPharmacyID.Add(drPharmacy("sNPI"))
                    lstPharmacyIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.NPI)
                End If

                If Not String.IsNullOrWhiteSpace(drPharmacy("sNCPDPID")) Then  'NCPDPID
                    If drPharmacy("sNCPDPID").Length > 35 Then
                        lstPharmacyID.Add(drPharmacy("sNCPDPID").Substring(0, 35))
                    Else
                        lstPharmacyID.Add(drPharmacy("sNCPDPID"))
                    End If
                    lstPharmacyIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.NCPDPID)
                End If

                If lstPharmacyID.Any() AndAlso lstPharmacyIDType.Any() Then
                    PharmacyIdentification.Items = lstPharmacyID.ToArray()
                    PharmacyIdentification.ItemsElementName = lstPharmacyIDType.ToArray()
                    Pharmacy.Identification = PharmacyIdentification
                End If

                'If Not String.IsNullOrWhiteSpace(dtPharmacy("sAddressLine1")) Then
                '    Pharmacy.Specialty = dtPharmacy("sAddressLine1")
                'End If

                If Not String.IsNullOrWhiteSpace(drPharmacy("sName")) Then
                    If drPharmacy("sName").Length > 35 Then
                        Pharmacy.StoreName = drPharmacy("sName").Substring(0, 35)
                    Else
                        Pharmacy.StoreName = drPharmacy("sName")
                    End If
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
        Return Pharmacy
    End Function

    Private Function GetResponseEnvelope(ByVal RequestMessage As Schema.MessageType, ByVal RxChangeResponseBody As Schema.RxChangeResponse, ByVal EPrescription As gloSureScript.EPrescription, ByVal ResponseType As String) As Schema.MessageType
        Dim ResponseMessage As Schema.MessageType = Nothing
        Dim ResponseBody As Schema.RxChangeResponse = Nothing
        'Dim ApproveResponse As Schema.ApprovedType = Nothing
        Dim ResponseTypeObject As Object = Nothing
        Dim dtdate As DateTime = Date.UtcNow

        Try
            ResponseMessage = New Schema.MessageType()
            ResponseMessage.Header = New Schema.HeaderType()
            ResponseMessage.Header.To = New Schema.QualifiedMailAddressType()
            ResponseMessage.Header.From = New Schema.QualifiedMailAddressType()

            ResponseMessage.Body = New Schema.BodyType()
            ResponseMessage.Body.Item = RxChangeResponseBody

            ResponseMessage.version = "010"
            ResponseMessage.release = "006"

            Select Case ResponseType.ToUpper()
                Case "A"
                    ResponseTypeObject = New Schema.ApprovedType()
                Case "C"
                    ResponseTypeObject = New Schema.ApprovedWithChangesType
                Case "N"
                    ResponseTypeObject = New Schema.DeniedNewRxToFollowType
            End Select

            If RxChangeResponseBody Is Nothing Then
                ResponseBody = New Schema.RxChangeResponse()
                ResponseBody.Response = New Schema.ChangeResponseType()
                ResponseMessage.Body.Item = ResponseBody
                ResponseBody.Response.Item = ResponseTypeObject
            Else
                RxChangeResponseBody.Response = New Schema.ChangeResponseType()
            End If

            RxChangeResponseBody.Response.Item = ResponseTypeObject

            Try
                If DirectCast(RequestMessage.Body.Item, Schema.RxChangeRequest).Request.ChangeRequestType = "P" And ResponseType.ToUpper() = "A" Then
                    If Not String.IsNullOrEmpty(EPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationValue) Then
                        DirectCast(RxChangeResponseBody.Response.Item, gloGlobal.Schemas.Surescript.ApprovedType).Note = "PA has been approved. PA Approval Code " & EPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationValue
                    End If
                End If
            Catch ex As Exception

            End Try

            With ResponseMessage.Header
                .MessageID = "ApprovedResponse" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

                .To.Value = RequestMessage.Header.From.Value
                .To.Qualifier = RequestMessage.Header.From.Qualifier

                .From.Value = RequestMessage.Header.To.Value
                .From.Qualifier = RequestMessage.Header.To.Qualifier

                .RelatesToMessageID = RequestMessage.Header.MessageID
                .SentTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.FZ")

                If Not String.IsNullOrEmpty(RequestMessage.Header.RxReferenceNumber) AndAlso Not String.IsNullOrWhiteSpace(RequestMessage.Header.RxReferenceNumber) Then
                    .RxReferenceNumber = RequestMessage.Header.RxReferenceNumber
                End If

                If Not String.IsNullOrWhiteSpace(EPrescription.DrugsCol.Item(DrugIndex).TransactionID) Then
                    If Val(EPrescription.DrugsCol.Item(DrugIndex).TransactionID <> 0) Then
                        .PrescriberOrderNumber = EPrescription.DrugsCol.Item(DrugIndex).TransactionID
                    End If
                End If
            End With

        Catch ex As Exception
            Throw ex
        End Try

        Return ResponseMessage
    End Function

    Private Function GetPatient(ByVal objPrescription As gloSureScript.EPrescription, ByVal Patient As Schema.PatientType) As Schema.PatientType

        Try
            If Patient Is Nothing Then
                Patient = New Schema.PatientType()
            End If

            If Patient.Name Is Nothing Then
                Patient.Name = New Schema.MandatoryPatientNameType()
            End If

            If Patient.Address Is Nothing Then
                Patient.Address = New Schema.AddressType()
            End If

            With Patient.Name
                If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.PatientName.FirstName) Then
                    If objPrescription.RxPatient.PatientName.FirstName.Length > 35 Then
                        .FirstName = objPrescription.RxPatient.PatientName.FirstName.Substring(0, 35)
                    Else
                        .FirstName = objPrescription.RxPatient.PatientName.FirstName
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.PatientName.MiddleName) Then
                    .MiddleName = objPrescription.RxPatient.PatientName.MiddleName
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.PatientName.LastName) Then
                    If objPrescription.RxPatient.PatientName.LastName.Length > 35 Then
                        .LastName = objPrescription.RxPatient.PatientName.LastName.Substring(0, 35)
                    Else
                        .LastName = objPrescription.RxPatient.PatientName.LastName
                    End If


                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.PatientName.Prefix) Then
                    .Prefix = objPrescription.RxPatient.PatientName.Prefix
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.PatientName.Suffix) Then
                    .Suffix = objPrescription.RxPatient.PatientName.Suffix
                End If
            End With

            With Patient.Address
                If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.PatientAddress.Address1) Then
                    If objPrescription.RxPatient.PatientAddress.Address1.Length > 35 Then
                        .AddressLine1 = objPrescription.RxPatient.PatientAddress.Address1.Substring(0, 35)
                    Else
                        .AddressLine1 = objPrescription.RxPatient.PatientAddress.Address1
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.PatientAddress.Address2) Then
                    If objPrescription.RxPatient.PatientAddress.Address2.Length > 35 Then
                        .AddressLine2 = objPrescription.RxPatient.PatientAddress.Address2.Substring(0, 35)
                    Else
                        .AddressLine2 = objPrescription.RxPatient.PatientAddress.Address2
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.PatientAddress.City) Then
                    If objPrescription.RxPatient.PatientAddress.City.Length > 35 Then
                        .City = objPrescription.RxPatient.PatientAddress.City.Substring(0, 35)
                    Else
                        .City = objPrescription.RxPatient.PatientAddress.City
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.PatientAddress.State) Then
                    If objPrescription.RxPatient.PatientAddress.State.Length > 9 Then
                        .State = objPrescription.RxPatient.PatientAddress.State.Substring(0, 9)
                    Else
                        .State = objPrescription.RxPatient.PatientAddress.State
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.PatientAddress.Zip) Then
                    If objPrescription.RxPatient.PatientAddress.Zip.Length > 11 Then
                        .ZipCode = objPrescription.RxPatient.PatientAddress.Zip.Substring(0, 11)
                    Else
                        .ZipCode = objPrescription.RxPatient.PatientAddress.Zip
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(.AddressLine2) Then
                    .PlaceLocationQualifier = "AD2"
                End If

            End With

            If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.Gender) Then
                Select Case objPrescription.RxPatient.Gender.ToUpper()
                    Case "MALE"
                        Patient.Gender = "M"
                    Case "FEMALE"
                        Patient.Gender = "F"
                    Case Else
                        Patient.Gender = "U"
                End Select
            End If

            If Not String.IsNullOrWhiteSpace(objPrescription.RxPatient.DateofBirth) Then
                Patient.DateOfBirth.Item = objPrescription.RxPatient.DateofBirth
            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return Patient
    End Function

    Private Function GetPatient(ByVal drPatient As DataRow, ByVal Patient As Schema.PatientType) As Schema.PatientType

        Try
            If Patient Is Nothing Then
                Patient = New Schema.PatientType()
            End If

            If (drPatient IsNot Nothing) Then


                If Patient.Name Is Nothing Then
                    Patient.Name = New Schema.MandatoryPatientNameType()
                End If

                If Patient.Address Is Nothing Then
                    Patient.Address = New Schema.AddressType()
                End If

                With Patient.Name
                    If Not String.IsNullOrWhiteSpace(drPatient("sFirstName")) Then
                        If drPatient("sFirstName").Length > 35 Then
                            .FirstName = drPatient("sFirstName").Substring(0, 35)
                        Else
                            .FirstName = drPatient("sFirstName")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPatient("sMiddleName")) Then
                        .MiddleName = drPatient("sMiddleName")
                    End If

                    If Not String.IsNullOrWhiteSpace(drPatient("sLastName")) Then
                        If drPatient("sLastName").Length > 35 Then
                            .LastName = drPatient("sLastName").Substring(0, 35)
                        Else
                            .LastName = drPatient("sLastName")
                        End If


                    End If

                    If Not String.IsNullOrWhiteSpace(drPatient("Prefix")) Then
                        .Prefix = drPatient("Prefix")
                    End If

                    If Not String.IsNullOrWhiteSpace(drPatient("Suffix")) Then
                        .Suffix = drPatient("Suffix")
                    End If
                End With

                With Patient.Address
                    If Not String.IsNullOrWhiteSpace(drPatient("sAddressLine")) Then
                        If drPatient("sAddressLine").Length > 35 Then
                            .AddressLine1 = drPatient("sAddressLine").Substring(0, 35)
                        Else
                            .AddressLine1 = drPatient("sAddressLine")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPatient("sAddressLine2")) Then
                        If drPatient("sAddressLine2").Length > 35 Then
                            .AddressLine2 = drPatient("sAddressLine2").Substring(0, 35)
                        Else
                            .AddressLine2 = drPatient("sAddressLine2")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPatient("sCity")) Then
                        If drPatient("sCity").Length > 35 Then
                            .City = drPatient("sCity").Substring(0, 35)
                        Else
                            .City = drPatient("sCity")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPatient("sState")) Then
                        If drPatient("sState").Length > 9 Then
                            .State = drPatient("sState").Substring(0, 9)
                        Else
                            .State = drPatient("sState")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(drPatient("sZip")) Then
                        If drPatient("sZip").Length > 11 Then
                            .ZipCode = drPatient("sZip").Substring(0, 11)
                        Else
                            .ZipCode = drPatient("sZip")
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(.AddressLine2) Then
                        .PlaceLocationQualifier = "AD2"
                    End If

                End With

                If Not String.IsNullOrWhiteSpace(drPatient("sGender")) Then
                    Select Case drPatient("sGender").ToUpper()
                        Case "MALE"
                            Patient.Gender = "M"
                        Case "FEMALE"
                            Patient.Gender = "F"
                        Case Else
                            Patient.Gender = "U"
                    End Select
                End If

                If Not String.IsNullOrWhiteSpace(drPatient("dtDOB")) Then
                    If Patient.DateOfBirth Is Nothing Then
                        Patient.DateOfBirth = New Schema.DateType()
                    End If
                    Patient.DateOfBirth.Item = drPatient("dtDOB")
                End If

                Dim lstPatientComm As New List(Of Schema.CommunicationType)

                Dim commPatient As Schema.CommunicationType = Nothing

                Dim gloCommPatient As gloSureScript.PhoneNumber = New gloSureScript.PhoneNumber()

                If Not String.IsNullOrWhiteSpace(drPatient("sEmail")) Then
                    gloCommPatient.Email = drPatient("sEmail")
                    commPatient = New Schema.CommunicationType()
                    commPatient.Number = gloCommPatient.Email
                    commPatient.Qualifier = "EM"
                    lstPatientComm.Add(commPatient)
                End If

                If Not String.IsNullOrWhiteSpace(drPatient("sFax")) Then
                    gloCommPatient.Fax = drPatient("sFax")
                    commPatient = New Schema.CommunicationType()
                    commPatient.Number = gloCommPatient.Fax
                    commPatient.Qualifier = "FX"
                    lstPatientComm.Add(commPatient)
                End If

                If Not String.IsNullOrWhiteSpace(drPatient("sPhone")) Then
                    gloCommPatient.Phone = drPatient("sPhone")
                    commPatient = New Schema.CommunicationType()
                    commPatient.Number = gloCommPatient.Phone
                    commPatient.Qualifier = "TE"
                    lstPatientComm.Add(commPatient)
                End If

                If lstPatientComm.Any() Then
                    Patient.CommunicationNumbers = lstPatientComm.ToArray()
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return Patient
    End Function

    Private Function GetMedicationPrescribed(objPrescription As EPrescription, ByVal InputMedication As Schema.RxChangeResponseMedicationType) As Schema.RxChangeResponseMedicationType
        Dim dtRxNormCode As DataTable = Nothing
        Dim DrugDBCode As String = String.Empty
        Dim DrugDBcodeQualifier As String = String.Empty

        Try
            If InputMedication Is Nothing Then
                InputMedication = New Schema.RxChangeResponseMedicationType()
            End If

            With InputMedication
                .DrugDescription = objPrescription.DrugsCol.Item(DrugIndex).DrugName

                If .DrugCoded Is Nothing Then
                    .DrugCoded = New Schema.DrugCodedType()
                End If

                .DrugCoded.ProductCode = objPrescription.DrugsCol.Item(DrugIndex).ProdCode
                .DrugCoded.ProductCodeQualifier = objPrescription.DrugsCol.Item(DrugIndex).ProdCodeQualifier

                If .Quantity Is Nothing Then
                    .Quantity = New Schema.QuantityType()
                End If

                If objPrescription.DrugsCol.Item(DrugIndex).DrugQuantity.Length > 11 Then
                    .Quantity.Value = objPrescription.DrugsCol.Item(DrugIndex).DrugQuantity.Substring(0, 11)
                Else
                    .Quantity.Value = objPrescription.DrugsCol.Item(DrugIndex).DrugQuantity
                End If

                .Quantity.CodeListQualifier = "38"
                .Quantity.PotencyUnitCode = objPrescription.DrugsCol.Item(DrugIndex).PotencyCode.Trim()
                .Quantity.UnitSourceCode = "AC"

                .DaysSupply = objPrescription.DrugsCol.Item(DrugIndex).DrugDuration.Trim

                If objPrescription.DrugsCol.Item(DrugIndex).Directions.Length > 140 Then
                    .Directions = objPrescription.DrugsCol.Item(DrugIndex).Directions.Substring(0, 140)
                Else
                    .Directions = objPrescription.DrugsCol.Item(DrugIndex).Directions
                End If

                If .Refills Is Nothing Then
                    .Refills = New Schema.RxChangeResponseMedicationTypeRefills()
                End If

                Select Case objPrescription.DrugsCol.Item(DrugIndex).RefillsQualifier
                    Case "PRN"
                        .Refills.Qualifier = "PRN"
                        .Refills.Value = Nothing
                    Case "R"
                        .Refills.Qualifier = "R"
                        .Refills.Value = objPrescription.DrugsCol.Item(DrugIndex).RefillQuantity
                    Case "P"
                        .Refills.Qualifier = "P"
                        .Refills.Value = objPrescription.DrugsCol.Item(DrugIndex).RefillQuantity
                End Select

                If .Refills.Value.Length > 2 Then
                    .Refills.Value.Substring(0, 2)
                End If

                Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloSurescriptGeneral.sDIBServiceURL)
                    Dim DrugDBCodeResponse As gloGlobal.DIB.RxnormFlagInfo = oGSHelper.GetRxNormCode(.DrugCoded.ProductCode)

                    If DrugDBCodeResponse IsNot Nothing Then
                        .DrugCoded.DrugDBCode = DrugDBCodeResponse.Code

                        If DrugDBCodeResponse.Type = "CD" Then
                            .DrugCoded.DrugDBCodeQualifier = "SCD"
                        Else
                            .DrugCoded.DrugDBCodeQualifier = DrugDBCodeResponse.Type
                        End If
                        DrugDBCodeResponse = Nothing
                    End If
                End Using

                If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).EPCSDEASchedule) Then
                    Select Case objPrescription.DrugsCol.Item(DrugIndex).EPCSDEASchedule.ToUpper()
                        Case "C48672"
                            .DrugCoded.DEASchedule = gloGlobal.Schemas.Surescript.DrugCodedTypeDEASchedule.C48672
                            .DrugCoded.DEAScheduleSpecified = True
                        Case "C48675"
                            .DrugCoded.DEASchedule = gloGlobal.Schemas.Surescript.DrugCodedTypeDEASchedule.C48675
                            .DrugCoded.DEAScheduleSpecified = True
                        Case "C48676"
                            .DrugCoded.DEASchedule = gloGlobal.Schemas.Surescript.DrugCodedTypeDEASchedule.C48676
                            .DrugCoded.DEAScheduleSpecified = True
                        Case "C48677"
                            .DrugCoded.DEASchedule = gloGlobal.Schemas.Surescript.DrugCodedTypeDEASchedule.C48677
                            .DrugCoded.DEAScheduleSpecified = True
                        Case "C48679"
                            .DrugCoded.DEASchedule = gloGlobal.Schemas.Surescript.DrugCodedTypeDEASchedule.C48679
                            .DrugCoded.DEAScheduleSpecified = True
                    End Select
                End If

                Select Case objPrescription.DrugsCol.Item(DrugIndex).MaySubstitute
                    Case True
                        .Substitutions = "0"
                    Case False
                        .Substitutions = "1"
                End Select

                If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PrimaryDXQualifier) Then

                    Dim dxl As New List(Of Schema.Diagnosis)
                    Dim dx As New Schema.Diagnosis
                    dx.ClinicalInformationQualifier = "1"

                    If dx.Primary Is Nothing Then
                        dx.Primary = New Schema.PrimaryDiagnosisType
                    End If

                    dx.Primary.Qualifier = objPrescription.DrugsCol.Item(DrugIndex).PrimaryDXQualifier
                    dx.Primary.Value = objPrescription.DrugsCol.Item(DrugIndex).PrimaryDXValue

                    If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).SecondaryDXQualifier) Then
                        If dx.Secondary Is Nothing Then
                            dx.Secondary = New Schema.DiagnosisType
                        End If

                        dx.Secondary.Qualifier = objPrescription.DrugsCol.Item(DrugIndex).SecondaryDXQualifier
                        dx.Secondary.Value = objPrescription.DrugsCol.Item(DrugIndex).SecondaryDXValue
                    End If

                    dxl.Add(dx)

                    If .Diagnosis Is Nothing Then
                        .Diagnosis = dxl.ToArray()
                    End If

                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).WrittenDate) Then
                    If .WrittenDate Is Nothing Then
                        .WrittenDate = New Schema.DateType()
                    End If

                    .WrittenDate.Item = Convert.ToDateTime(objPrescription.DrugsCol.Item(DrugIndex).WrittenDate)
                    .WrittenDate.ItemElementName = gloGlobal.Schemas.Surescript.ItemChoiceType.Date
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).Notes) Then
                    .Note = objPrescription.DrugsCol.Item(DrugIndex).Notes
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationStatus) Then
                    If .PriorAuthorization Is Nothing Then
                        .PriorAuthorization = New Schema.PriorAuthorizationType()
                        If objPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationStatus.ToUpper() = "APPROVED" Then
                            If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationValue) Then
                                .PriorAuthorization.Value = objPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationValue
                            End If

                            If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationQualifier) Then
                                .PriorAuthorization.Qualifier = objPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationQualifier
                            End If

                            If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationStatus) AndAlso Convert.ToString(objPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationStatus).ToUpper() <> "CANCELLED" Then
                                Select Case objPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationStatus.ToUpper()
                                    Case "DEFERRED"
                                        .PriorAuthorizationStatus = "F"
                                    Case "OPEN"
                                        .PriorAuthorizationStatus = "R"
                                    Case "CLOSED"
                                        .PriorAuthorizationStatus = "N"
                                    Case "DENIED"
                                        .PriorAuthorizationStatus = "D"
                                    Case "APPROVED"
                                        .PriorAuthorizationStatus = "A"
                                    Case "REQUESTED"
                                        .PriorAuthorizationStatus = "R"
                                End Select

                            End If
                        End If
                    End If
                End If

            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return InputMedication
    End Function

    Private Function GetMedicationPrescribed(objPrescription As gloGlobal.Common.ServiceObjectBase.MedPrescribed, ByVal InputMedication As Schema.PrescribedMedicationType) As Schema.PrescribedMedicationType
        Dim dtRxNormCode As DataTable = Nothing
        Dim DrugDBcodeQualifier As String = String.Empty
        Dim DrugDBCodeResponse As gloGlobal.DIB.RxnormFlagInfo = Nothing
        Try
            If InputMedication Is Nothing Then
                InputMedication = New Schema.PrescribedMedicationType()
            End If

            With InputMedication
                .DrugDescription = objPrescription.medication

                If .DrugCoded Is Nothing Then
                    .DrugCoded = New Schema.DrugCodedType()
                End If

                .DrugCoded.ProductCode = objPrescription.ndc
                .DrugCoded.ProductCodeQualifier = "ND"

                If .Quantity Is Nothing Then
                    .Quantity = New Schema.QuantityType()
                End If

                If objPrescription.qty.Length > 11 Then
                    .Quantity.Value = objPrescription.qty.Substring(0, 11)
                Else
                    .Quantity.Value = objPrescription.qty
                End If

                .Quantity.CodeListQualifier = "38"
                .Quantity.PotencyUnitCode = objPrescription.qtyUnit.Trim()
                .Quantity.UnitSourceCode = "AC"

                .DaysSupply = objPrescription.days.Trim

                If objPrescription.direction.Length > 140 Then
                    .Directions = objPrescription.direction.Substring(0, 140)
                Else
                    .Directions = objPrescription.direction
                End If

                If .Refills Is Nothing Then
                    .Refills = New Schema.RefillsType
                End If

                'Select Case objPrescription.DrugsCol.Item(DrugIndex).RefillsQualifier
                '    Case "PRN"
                '        .Refills.Qualifier = "PRN"
                '        .Refills.Value = Nothing
                '    Case "R"
                '        .Refills.Qualifier = "R"
                '        .Refills.Value = objPrescription.DrugsCol.Item(DrugIndex).RefillQuantity
                '    Case "P"
                '        .Refills.Qualifier = "P"
                '        .Refills.Value = objPrescription.DrugsCol.Item(DrugIndex).RefillQuantity
                'End Select

                .Refills.Qualifier = "R"

                If String.IsNullOrWhiteSpace(objPrescription.refill) Then
                    .Refills.Value = "0"
                Else
                    .Refills.Value = objPrescription.refill
                End If


                If .Refills.Value.Length > 2 Then
                    .Refills.Value.Substring(0, 2)
                End If

                Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloSurescriptGeneral.sDIBServiceURL)
                    DrugDBCodeResponse = oGSHelper.GetRxNormCode(.DrugCoded.ProductCode)
                End Using

                If DrugDBCodeResponse IsNot Nothing Then
                    .DrugCoded.DrugDBCode = DrugDBCodeResponse.Code

                    If DrugDBCodeResponse.Type = "CD" Then
                        .DrugCoded.DrugDBCodeQualifier = "SCD"
                    Else
                        .DrugCoded.DrugDBCodeQualifier = DrugDBCodeResponse.Type
                    End If
                    DrugDBCodeResponse = Nothing
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.dea) Then
                    Select Case objPrescription.dea.ToUpper()
                        Case "C48672"
                            .DrugCoded.DEASchedule = gloGlobal.Schemas.Surescript.DrugCodedTypeDEASchedule.C48672
                            .DrugCoded.DEAScheduleSpecified = True
                        Case "C48675"
                            .DrugCoded.DEASchedule = gloGlobal.Schemas.Surescript.DrugCodedTypeDEASchedule.C48675
                            .DrugCoded.DEAScheduleSpecified = True
                        Case "C48676"
                            .DrugCoded.DEASchedule = gloGlobal.Schemas.Surescript.DrugCodedTypeDEASchedule.C48676
                            .DrugCoded.DEAScheduleSpecified = True
                        Case "C48677"
                            .DrugCoded.DEASchedule = gloGlobal.Schemas.Surescript.DrugCodedTypeDEASchedule.C48677
                            .DrugCoded.DEAScheduleSpecified = True
                        Case "C48679"
                            .DrugCoded.DEASchedule = gloGlobal.Schemas.Surescript.DrugCodedTypeDEASchedule.C48679
                            .DrugCoded.DEAScheduleSpecified = True
                    End Select
                End If

                Select Case objPrescription.substitute
                    Case True
                        .Substitutions = "0"
                    Case False
                        .Substitutions = "1"
                End Select

                If Not String.IsNullOrWhiteSpace(objPrescription.written) Then
                    If .WrittenDate Is Nothing Then
                        .WrittenDate = New Schema.DateType()
                    End If

                    .WrittenDate.Item = Convert.ToDateTime(objPrescription.written)
                    .WrittenDate.ItemElementName = gloGlobal.Schemas.Surescript.ItemChoiceType.Date
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.note) Then
                    .Note = objPrescription.note
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.pan) Then
                    If .PriorAuthorization Is Nothing Then
                        .PriorAuthorization = New Schema.PriorAuthorizationType()

                        '        If Not String.IsNullOrWhiteSpace(objPrescription.pan) Then
                        .PriorAuthorization.Value = objPrescription.pan
                        'End If

                        'If Not String.IsNullOrWhiteSpace(objPrescription.DrugsCol.Item(DrugIndex).PriorAuthorizationQualifier) Then
                        .PriorAuthorization.Qualifier = "G1"
                        'End If

                        If Not String.IsNullOrWhiteSpace(objPrescription.pas) Then
                            Select Case objPrescription.pas.ToUpper()
                                Case "DEFERRED"
                                    .PriorAuthorizationStatus = "F"
                                Case "OPEN"
                                    .PriorAuthorizationStatus = "R"
                                Case "CLOSED"
                                    .PriorAuthorizationStatus = "N"
                                Case "DENIED"
                                    .PriorAuthorizationStatus = "D"
                                Case "APPROVED"
                                    .PriorAuthorizationStatus = "A"
                            End Select

                        End If
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.DxQual1) Then

                    Dim dxl As New List(Of Schema.Diagnosis)
                    Dim dx As New Schema.Diagnosis
                    dx.ClinicalInformationQualifier = "1"

                    If dx.Primary Is Nothing Then
                        dx.Primary = New Schema.PrimaryDiagnosisType
                    End If

                    dx.Primary.Qualifier = objPrescription.DxQual1
                    dx.Primary.Value = objPrescription.DxVal1

                    If Not String.IsNullOrWhiteSpace(objPrescription.DxQual2) Then
                        If dx.Secondary Is Nothing Then
                            dx.Secondary = New Schema.DiagnosisType
                        End If

                        dx.Secondary.Qualifier = objPrescription.DxQual2
                        dx.Secondary.Value = objPrescription.DxVal2
                    End If

                    dxl.Add(dx)

                    If .Diagnosis Is Nothing Then
                        .Diagnosis = dxl.ToArray()
                    End If

                End If


            End With

        Catch ex As Exception
            Throw ex
        End Try
        Return InputMedication
    End Function
    Private Function GetSupervisor(objPrescription As EPrescription, ByVal Supervisor As Schema.SupervisorType) As gloGlobal.Schemas.Surescript.SupervisorType

        Dim SupervisorName As Schema.MandatoryNameType = Nothing
        Dim SupervisorAddress As Schema.AddressType = Nothing

        Dim PrescriberIdentification As Schema.MandatoryProviderIDType = Nothing
        Dim lstPrescriberID As New List(Of String)
        Dim lstPrescriberIDType As New List(Of Schema.ItemsChoiceType)

        Dim lstSupervisorComm As New List(Of Schema.CommunicationType)

        Try
            If Supervisor IsNot Nothing OrElse Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName) Then
                If Supervisor Is Nothing Then
                    Supervisor = New Schema.SupervisorType()
                End If

                If Supervisor.Name Is Nothing Then
                    SupervisorName = New Schema.MandatoryNameType()
                    Supervisor.Name = SupervisorName
                Else
                    SupervisorName = Supervisor.Name
                End If

                If Supervisor.Address Is Nothing Then
                    SupervisorAddress = New Schema.AddressType()
                    Supervisor.Address = SupervisorAddress
                Else
                    SupervisorAddress = Supervisor.Address
                End If

                If Supervisor.Identification Is Nothing Then
                    PrescriberIdentification = New Schema.MandatoryProviderIDType()
                    Supervisor.Identification = PrescriberIdentification
                Else
                    PrescriberIdentification = Supervisor.Identification
                End If


                If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName) Then
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Length > 35 Then
                        SupervisorName.FirstName = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName.Substring(0, 35)
                    Else
                        SupervisorName.FirstName = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName) Then
                    SupervisorName.MiddleName = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName) Then
                    If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Length > 35 Then
                        SupervisorName.LastName = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName.Substring(0, 35)
                    Else
                        SupervisorName.LastName = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.Prefix) Then
                    SupervisorName.Prefix = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.Prefix
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.Suffix) Then
                    SupervisorName.Suffix = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.Suffix
                End If

                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress IsNot Nothing AndAlso objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1 IsNot Nothing Then
                    If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1) Then
                        With SupervisorAddress
                            If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1) Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Length > 35 Then
                                    .AddressLine1 = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1.Substring(0, 35)
                                Else
                                    .AddressLine1 = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1
                                End If
                            End If

                            If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2) Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Length > 35 Then
                                    .AddressLine2 = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2.Substring(0, 35)
                                Else
                                    .AddressLine2 = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2
                                End If
                            End If

                            If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City) Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Length > 35 Then
                                    .City = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City.Substring(0, 35)
                                Else
                                    .City = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City
                                End If
                            End If

                            If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State) Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Length > 9 Then
                                    .State = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State.Substring(0, 9)
                                Else
                                    .State = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State
                                End If
                            End If

                            If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip) Then
                                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Length > 11 Then
                                    .ZipCode = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip.Substring(0, 11)
                                Else
                                    .ZipCode = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip
                                End If
                            End If

                            If Not String.IsNullOrWhiteSpace(.AddressLine2) Then
                                .PlaceLocationQualifier = "AD2"
                            End If

                        End With

                        Supervisor.Address = SupervisorAddress
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI) Then
                    lstPrescriberID.Add(objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI)
                    lstPrescriberIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.NPI)
                End If

                If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA) Then
                    lstPrescriberID.Add(objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA)
                    lstPrescriberIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.DEANumber)
                End If

                'Commented because its commented in GenerateRefillResponse10dot6New
                'If Not String.IsNullOrWhiteSpace(objPrescription.RxSupervisorPrescriber.SupervisorProviderSSN) Then
                '    lstPrescriberID.Add(objPrescription.RxPrescriber.PrescriberSSN)
                '    lstPrescriberIDType.Add(gloGlobal.Schemas.Surescript.ItemsChoiceType.SocialSecurity)
                'End If

                If lstPrescriberID.Any() AndAlso lstPrescriberIDType.Any() Then
                    PrescriberIdentification.Items = lstPrescriberID.ToArray()
                    PrescriberIdentification.ItemsElementName = lstPrescriberIDType.ToArray()
                End If

                If objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone IsNot Nothing Then
                    Dim commPrescriber As Schema.CommunicationType = Nothing

                    Dim gloCommPrescriber As gloSureScript.PhoneNumber = objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone

                    If Not String.IsNullOrWhiteSpace(gloCommPrescriber.Email) Then
                        commPrescriber = New Schema.CommunicationType()
                        commPrescriber.Number = gloCommPrescriber.Email
                        commPrescriber.Qualifier = "EM"
                        lstSupervisorComm.Add(commPrescriber)
                    End If

                    If Not String.IsNullOrWhiteSpace(gloCommPrescriber.Fax) Then
                        commPrescriber = New Schema.CommunicationType()
                        commPrescriber.Number = gloCommPrescriber.Fax
                        commPrescriber.Qualifier = "FX"
                        lstSupervisorComm.Add(commPrescriber)
                    End If

                    If Not String.IsNullOrWhiteSpace(gloCommPrescriber.Mobile) Then
                        commPrescriber = New Schema.CommunicationType()
                        commPrescriber.Number = gloCommPrescriber.Mobile
                        commPrescriber.Qualifier = "CP"
                        lstSupervisorComm.Add(commPrescriber)
                    End If

                    If Not String.IsNullOrWhiteSpace(gloCommPrescriber.Pager) Then
                        commPrescriber = New Schema.CommunicationType()
                        commPrescriber.Number = gloCommPrescriber.Pager
                        commPrescriber.Qualifier = "BN"
                        lstSupervisorComm.Add(commPrescriber)
                    End If

                    If Not String.IsNullOrWhiteSpace(gloCommPrescriber.Phone) Then
                        commPrescriber = New Schema.CommunicationType()
                        commPrescriber.Number = gloCommPrescriber.Phone
                        commPrescriber.Qualifier = "TE"
                        lstSupervisorComm.Add(commPrescriber)
                    End If

                    If lstSupervisorComm.Any() Then
                        Supervisor.CommunicationNumbers = lstSupervisorComm.ToArray()
                    End If
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return Supervisor

    End Function

#End Region


End Class

