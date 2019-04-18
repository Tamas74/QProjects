Imports RxSniffer.RxGeneral
Imports System.Data.SqlClient
Imports RxSniffer.eRXStag
Imports System.IO
Imports System.ComponentModel


Public Class InsertRefillRequest
    Dim _strDbConnection As String


    Public Property strDbConnection() As String
        Get
            Return _strDbConnection
        End Get
        Set(ByVal value As String)
            _strDbConnection = value
        End Set
    End Property





    Public Sub RetrieveRxResponses()
        Dim strFileName As String = ""
        Dim objConnection As SqlConnection = Nothing
        Dim objCammand As SqlCommand = Nothing
        Dim drReader As SqlDataReader = Nothing
        Dim cntResponse As Byte() = Nothing
        Try
            'Get Provider
            objConnection = New SqlConnection(strDbConnection)
            objCammand = New SqlCommand()
            Dim strPrescriber As String = ""
            objCammand.Connection = objConnection
            objCammand.CommandType = CommandType.Text
            objCammand.CommandText = "select nProviderId as ProviderID,isnull(sFirstName,'') + space(1) + isnull(sMiddlename,'') +space(1) + isnull(slastname,'') as ProviderName,isnull(sFirstName,'') as ProviderFirstName ,isnull(sLastName,'') as ProviderLastName,isnull(sAddress,'') + space(1) + isnull(sStreet,'') as Address, isnull(sCity,'') as City,isnull(sstate,'') as State,isnull(szip,'') as Zip,isnull(sphoneno,'') as Phone,isnull(sSPIID,'') as PrescriberID from Provider_Mst "
            objConnection.Open()
            drReader = objCammand.ExecuteReader
            If Not IsDBNull(drReader) Then
                If drReader.HasRows Then
                    'Reset provider flag
                    bIsProviderAvailable = False
                    While drReader.Read
                        If Not IsDBNull(drReader.Item("PrescriberID")) And drReader.Item("PrescriberID").ToString.Trim <> "" Then
                            If strPrescriber <> "" Then
                                strPrescriber &= "|" & drReader.Item("PrescriberID").ToString
                            Else
                                strPrescriber &= drReader.Item("PrescriberID").ToString
                            End If
                        End If
                    End While
                Else
                    'mdlGeneral.UpdateLog("No Providers Available")
                    'To avoid to write multiple time log message.
                    If bIsProviderAvailable = False Then
                        mdlGeneral.UpdateLog("No Providers Available")
                        bIsProviderAvailable = True
                    End If
                End If
            Else
                'mdlGeneral.UpdateLog("No Providers Available")
                'To avoid to write multiple time log message.
                If bIsProviderAvailable = False Then
                    mdlGeneral.UpdateLog("No Providers Available")
                    bIsProviderAvailable = True
                End If
            End If

           
            'Dim strProviders As String = RetrieveProividers()
            If Not strPrescriber Is Nothing And strPrescriber <> "" Then
                Dim _Access As String = String.Empty
                'Reset provider flag
                bIsProviderAvailable = False

                Dim myWebRxService As eRxMessage = New eRxMessage
                _Access = myWebRxService.Login("sarika@ophit.net", "spX12ss@!!21nasik")
                mdlGeneral.UpdateLog("Access to Webservice is successful on Staging Server")

                cntResponse = myWebRxService.GetResponses(strPrescriber, "", _Access)


                If cntResponse Is Nothing Then
                    mdlGeneral.UpdateLog("No new Responses are available on Staging Server")
                Else

                    strFileName = mdlGeneral.GetFileName(enumFileType.XMLFile)
                    If Not cntResponse Is Nothing Then
                        Dim content As Byte() = CType(cntResponse, Byte())
                        Dim stream As MemoryStream = New MemoryStream(content)
                        Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                        stream.WriteTo(oFile)
                        oFile.Close()
                    Else
                        mdlGeneral.UpdateLog("Unable to convert to Physical file")
                    End If

                    mdlGeneral.UpdateLog("Responses are saved at " & strFileName)
                    If strFileName <> "" Then
                        If File.Exists(strFileName) Then
                            Dim dsResponses As New DataSet
                            dsResponses.ReadXml(strFileName)
                            If Not dsResponses Is Nothing Then
                                If dsResponses.Tables.Count > 0 Then
                                    Dim dtResponses As New DataTable()
                                    dtResponses = dsResponses.Tables(0)
                                    Dim strTransaction As String = String.Empty
                                    'Dim strTransaction As String = SaveRxResponses(dsResponses.Tables(0))

                                    If dtResponses Is Nothing Then
                                        strTransaction = ""
                                        mdlGeneral.UpdateLog("No Responses from Webserver")
                                    Else
                                        'Dim strMsgTransactions As String = ""
                                        If dtResponses.Rows.Count > 0 Then
                                            Dim objPrescription As EPrescription
                                            Dim objError As SureScriptErrorMessage

                                            For Each dtRow As DataRow In dtResponses.Rows
                                                If dtRow("sMsgType") = "RefillRequest" Then
                                                    Dim objclsRxSniffer As clsRxSniffer = New clsRxSniffer()
                                                    objPrescription = objclsRxSniffer.ReadRefillRxMsg(dtRow)
                                                    If InsertRefillPrescription(objPrescription, strDbConnection) Then
                                                        If InsertintoMessageTransaction(CType(objPrescription.DrugsCol.Item(0), SureScriptMessage), strDbConnection) Then
                                                            If strTransaction <> "" Then
                                                                strTransaction &= "|" & dtRow(0).ToString
                                                            Else
                                                                strTransaction &= dtRow(0).ToString
                                                            End If
                                                        End If
                                                    End If
                                                Else
                                                    Dim objclsRxSniffer As clsRxSniffer = New clsRxSniffer()
                                                    objError = objclsRxSniffer.ReadErrorMsg(dtRow)
                                                    If InsertErrorDetails(objError, strDbConnection) Then
                                                        If InsertintoMessageTransaction(CType(objError, SureScriptMessage), strDbConnection) Then
                                                            If strTransaction <> "" Then
                                                                strTransaction &= "|" & dtRow(0).ToString
                                                            Else
                                                                strTransaction &= dtRow(0).ToString
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        Else
                                            mdlGeneral.UpdateLog("No New Responses from Surescript")
                                        End If
                                        'Return strMsgTransactions
                                    End If
                                    '  mdlGeneral.UpdateLog("SaveRxResponses completed - " & strTransaction)
                                    If strTransaction <> "" Then
                                        ''Commited for testing.

                                        Dim myRxServiceUpdate As eRxMessage = New eRxMessage
                                        Dim _UpdateAccess As String = myRxServiceUpdate.Login("sarika@ophit.net", "spX12ss@!!21nasik")
                                        myRxServiceUpdate.UpdateDownloadStatus(strTransaction, _UpdateAccess)
                                        mdlGeneral.UpdateLog("UpdateDownloadStatus completed on Staging Server")
                                        myRxServiceUpdate.Dispose()
                                    Else
                                        'mdlGeneral.UpdateLog("No Responses are saved in database on Staging Server")
                                    End If

                                End If

                            End If

                        End If

                    End If
                End If
            Else
                'To avoid to write multiple time log message.
                If bIsProviderAvailable = False Then
                    mdlGeneral.UpdateLog("No Provider are available with SPIID")
                    bIsProviderAvailable = True
                End If
            End If

        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString)
        Finally
            If objConnection IsNot Nothing Then
                objConnection.Dispose()
                objConnection = Nothing
            End If

            If objCammand IsNot Nothing Then
                objCammand.Dispose()
                objCammand = Nothing
            End If

            If drReader IsNot Nothing Then
                drReader.Dispose()
                drReader = Nothing
            End If

            'delete the xml file after reading.
            If strFileName <> "" Then
                If File.Exists(strFileName) Then
                    File.Delete(strFileName)
                End If
            End If

        End Try
    End Sub
    Private Function InsertRefillPrescription(ByVal objPrescription As EPrescription, ByVal strConnection As String) As Boolean
        Dim objCon As SqlConnection = New SqlConnection(strConnection)
        Dim objcmd As SqlCommand = New SqlCommand()
        Try
            If objPrescription Is Nothing Or objPrescription.DrugsCol.Count = 0 Then
                Return False
            End If

            Dim oDrug As New EDrug
            oDrug = objPrescription.DrugsCol.Item(0)


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
            mdlGeneral.UpdateLog("Error :" & ex.ToString)
            Return False
        Finally
            objcmd.Cancel()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        End Try
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



    Private Function InsertErrorDetails(ByVal objError As SureScriptErrorMessage, ByVal strConnection As String) As Boolean
        Dim objCon As SqlConnection = New SqlConnection(strConnection)
        Dim objcmd As SqlCommand = New SqlCommand()

        Try

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
            mdlGeneral.UpdateLog("Error :" & ex.ToString)
            Return False
        Finally

            objcmd.Cancel()
            objcmd.Dispose()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        End Try
    End Function


    Private Function InsertintoMessageTransaction(ByVal oMessage As SureScriptMessage, ByVal strConnection As String) As Boolean

        Dim objCon As SqlConnection = New SqlConnection()
        Dim objcmd As SqlCommand = New SqlCommand()

        Try

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

            objcmd.Parameters.Add("@IsAlertCheck", SqlDbType.Bit)
            objcmd.Parameters("@IsAlertCheck").Value = "False"

            objCon.Open()
            objcmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Error :" & ex.ToString)
            Return False
        Finally
            objcmd.Cancel()
            objcmd.Dispose()
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        End Try
    End Function
End Class
