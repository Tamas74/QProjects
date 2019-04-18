Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Windows.Forms
Imports Schema = gloGlobal.Schemas.Surescript
Imports System.Globalization
Imports gloSureScript
Imports gloEMRGeneralLibrary

Public Class PrescriptionBusinessLayer
    Inherits SurescriptsBusinessLayer

    Public Sub New()

    End Sub

#Region "Drug List Fuctions"

    Public Function GetClassifiedDrugs(Optional ByVal ConceptTreeID As Int32 = 0) As DataTable

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtDrugs As DataTable = Nothing

        Try
            param = New DBParameter
            param.Value = ConceptTreeID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Int
            param.Name = "@ParentConceptTreeID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtDrugs = dbHelper.GetDataTable("GSDD_GetClassifiedTree")

            Return dtDrugs

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

    Public Function GetFrequentlyUsedDrugs(ByVal keyword As String, ByVal SearchType As Int32, Optional ProviderID As Int64 = 0) As DataTable

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtDrugs As DataTable = Nothing

        Try
            param = New DBParameter
            param.Value = LCase(keyword)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@keyword"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = ProviderID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@nProviderID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            If ProviderID <> 0 Then
                param = New DBParameter
                param.Value = 1
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.Int
                param.Name = "@Type"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing
            End If

            param = New DBParameter
            param.Value = SearchType
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Int
            param.Name = "@SearchType"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtDrugs = dbHelper.GetDataTable("gsp_GetFrequentlyUsedDrugs")

            Return dtDrugs

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function
    Public Function GetPlannedDrugs(ByVal keyword As String, ByVal SearchType As Int32, ByVal ProviderID As Int64, ByVal nPatientID As Int64) As DataTable

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtDrugs As DataTable = Nothing

        Try
            param = New DBParameter
            param.Value = LCase(keyword)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@keyword"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            If ProviderID <> 0 Then
                param = New DBParameter
                param.Value = 1
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.Int
                param.Name = "@Type"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing
            End If

            param = New DBParameter
            param.Value = SearchType
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Int
            param.Name = "@SearchType"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = nPatientID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@nPatientID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtDrugs = dbHelper.GetDataTable("gsp_GetPlannedDrugs")

            Return dtDrugs

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function
    Public Function GetAllDrugs(ByVal keyword As String, ByVal SearchType As Int32, ByVal ListType As Int32) As DataTable

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtDrugs As DataTable = Nothing

        Try
            param = New DBParameter
            param.Value = LCase(keyword)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@keyword"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = SearchType
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Int
            param.Name = "@SearchType"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = ListType
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Int
            param.Name = "@listType"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtDrugs = dbHelper.GetDataTable("gsp_GetAllDrugs")

            Return dtDrugs

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

    Public Function GetProviderDrugs(ByVal keyword As String, ByVal SearchType As Int32, ByVal nLoginProviderID As Int64) As DataSet

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dsDrugs As DataSet = New DataSet()

        Try
            param = New DBParameter
            param.Value = LCase(keyword)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@keyword"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = nLoginProviderID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@nLoginProviderid"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = SearchType
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Int
            param.Name = "@SearchType"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dsDrugs = dbHelper.GetDataSet("gsp_GetProviderDrugList")
            Return dsDrugs

        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
    End Function

    Public Function GetClassifiedDrugsByMPID(ByVal ConceptTreeID As Integer) As List(Of Int32)
        Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
            Return oDIBGSHelper.GetClassifiedDrugs(ConceptTreeID)
        End Using
    End Function

    Public Function GetDrugDetails(ByVal MPIDs As DataTable) As DataTable

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtDrugs As DataTable = Nothing

        Try
            param = New DBParameter
            param.Value = MPIDs
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Structured
            param.Name = "@MPIDs"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtDrugs = dbHelper.GetDataTable("GSDD_GetDrugDetails")

            Return dtDrugs

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

#End Region

#Region "Prescription functions"
    Public Function GetPrescriptionsByPatient(ByVal PatientID As Int64, ByVal PrescriptionDate As DateTime, Optional ByVal VisitId As Int64 = 0) As DataTable

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter = Nothing
        Dim dtPrescriptions As DataTable = Nothing

        Try
            param = New DBParameter
            param.Value = VisitId
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@nVisitID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = PatientID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@nPatientID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = PrescriptionDate
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.DateTime
            param.Name = "@dtPrescriptiondate"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtPrescriptions = dbHelper.GetDataTable("gsp_scanPrescription")

            Return dtPrescriptions

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

#End Region

#Region "Drug details Functions"

    Public Function GetDrugDetailsByID(ByVal mpID As Int32, ByVal ndc As String) As DataRow

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtDrug As DataTable = Nothing
        Dim rowReturned As DataRow = Nothing
        Try
            param = New DBParameter
            param.Value = mpID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Int
            param.Name = "@mpid"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = ndc
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@NDCCode"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtDrug = dbHelper.GetDataTable("gsp_ScanDRUGS_MST_NDCCode")

            If Not IsNothing(dtDrug) Then
                If dtDrug.Rows.Count > 0 Then
                    rowReturned = dtDrug.Rows(0)
                End If
            End If
            Return rowReturned
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If dtDrug IsNot Nothing Then
                dtDrug.Dispose()
                dtDrug = Nothing
            End If

            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try

    End Function

    Public Function GetDrugDetailsBySig(ByVal SigID As Int64) As DataRow

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtDrug As DataTable = Nothing
        Dim rowReturned As DataRow = Nothing
        Try
            param = New DBParameter
            param.Value = SigID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@SIGID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtDrug = dbHelper.GetDataTable("gsp_ScanDrugProvAssociation")
            'gsp_GetDrugProvAssociation

            If Not IsNothing(dtDrug) Then
                If dtDrug.Rows.Count > 0 Then
                    rowReturned = dtDrug.Rows(0)
                End If
            End If
            Return rowReturned
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If dtDrug IsNot Nothing Then
                dtDrug.Dispose()
                dtDrug = Nothing
            End If

            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try

    End Function

#End Region

    Public Function GetMPIDByNDC(ByVal DrugPackNDCCode As String) As DataTable
        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim dtDrugs As DataTable = Nothing

        Try
            Dim _strQuery As String = "select isnull(mpid,0) as mpid,isnull(ndrugsID,0) as DrugId from Drugs_MST where sNdcCode= '" & DrugPackNDCCode & "' "
            dtDrugs = dbHelper.GetDataTable_Query(_strQuery)

            If Not IsNothing(dtDrugs) AndAlso dtDrugs.Rows.Count > 0 Then
                Return dtDrugs
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

    Public Function GetSearchTypeSetting(ByVal ClinicID As Int64, ByVal UserID As Int64) As Int32

        Dim nReturned As Int32 = 0
        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtSearchType As DataTable = Nothing

        Try
            param = New DBParameter
            param.Value = ClinicID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@ClinicID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = UserID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@UserID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtSearchType = dbHelper.GetDataTable("GetRxSearchType")

            If dtSearchType IsNot Nothing AndAlso dtSearchType.Rows.Count() > 0 Then
                Int32.TryParse(Convert.ToString(dtSearchType.Rows(0)("sSettingsValue")), nReturned)
            End If

        Catch ex As Exception
            Throw ex
            Return 0
        Finally
            If dtSearchType IsNot Nothing Then
                dtSearchType.Dispose()
                dtSearchType = Nothing
            End If

            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try

        Return nReturned
    End Function

    Public Sub SaveSearchTypeSetting(ByVal ClinicID As Int64, ByVal UserID As Int64, ByVal SearchType As Int32)

        Dim nReturned As Int32 = 0
        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtSearchType As DataTable = Nothing

        Try
            param = New DBParameter
            param.Value = ClinicID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@ClinicID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = UserID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@UserID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = SearchType
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@nSearchType"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dbHelper.Add("SaveRxSearchType")

        Catch ex As Exception
            Throw ex
        Finally
            If dtSearchType IsNot Nothing Then
                dtSearchType.Dispose()
                dtSearchType = Nothing
            End If

            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Sub


#Region "RxChange, RxFill and CancelRx functions"

    'Public Function GetDenialReasonCodes() As DataTable

    '    Dim result As New DataTable()
    '    With result.Columns
    '        .Add("key")
    '        .Add("value")
    '    End With

    '    result.Rows.Add({"AA", "Patient Unknown to the Prescriber"})
    '    result.Rows.Add({"AB", "Patient never under Prescriber care"})
    '    result.Rows.Add({"AC", "Patient no longer under Prescriber care"})
    '    result.Rows.Add({"AD", "Patient has requested refill too soon"})
    '    result.Rows.Add({"AE", "Medication never prescribed for the patient"})
    '    result.Rows.Add({"AF", "Patient should contact Prescriber first"})
    '    result.Rows.Add({"AG", "Refill not appropriate"})
    '    result.Rows.Add({"AH", "Patient has picked up prescription"})
    '    result.Rows.Add({"AK", "Patient has picked up partial fill of prescription"})
    '    result.Rows.Add({"AL", "Patient has not picked up prescription, drug returned to stock"})
    '    result.Rows.Add({"AM", "Patient needs appointment"})
    '    result.Rows.Add({"AN", "Prescriber not associated with this practice or location."})
    '    result.Rows.Add({"AO", "No attempt will be made to obtain Prior Authorization."})
    '    result.Rows.Add({"AP", "Request already responded to by other means (e.g. phone or fax})"})

    '    Return result

    'End Function

    'Public Sub SaveDeniedResponse(ByVal ResponseMessage As Schema.MessageType)
    '    Dim dbHelper As DataBaseLayer = Nothing
    '    Dim param As DBParameter = Nothing

    '    Try
    '        If ResponseMessage IsNot Nothing Then

    '            dbHelper = New DataBaseLayer()

    '            param = New DBParameter
    '            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.MessageID), "", ResponseMessage.Header.MessageID)
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@nMessageID"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = "ChangeResponse"
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@sMessageName"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.RelatesToMessageID), "", ResponseMessage.Header.RelatesToMessageID)
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@sRelatesToMessageID"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.From.Value), "", ResponseMessage.Header.From.Value)
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@sMessageFrom"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.To.Value), "", ResponseMessage.Header.To.Value)
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@sMessageTo"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = Date.Now.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@sDateTimeStamp"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = Date.Now.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@dtDateReceived"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.RxReferenceNumber), "", ResponseMessage.Header.RxReferenceNumber)
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@sReferenceNumber"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = False
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@IsAlertCheck"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = DBNull.Value
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@sSenderSoftwareVersion"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = DBNull.Value
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@sSenderSoftwareDeveloper"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = DBNull.Value
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@sSenderSoftwareProduct"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = DBNull.Value
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@FileXML"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            param = New DBParameter
    '            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.PrescriberOrderNumber), "", ResponseMessage.Header.PrescriberOrderNumber)
    '            param.Direction = ParameterDirection.Input
    '            param.DataType = SqlDbType.VarChar
    '            param.Name = "@sPrescriberOrderNumber"
    '            dbHelper.DBParametersCol.Add(param)
    '            param = Nothing

    '            dbHelper.ExecuteNon_Query("sc_InsertRxMsgTransaction")

    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        If dbHelper IsNot Nothing Then
    '            dbHelper.Dispose()
    '            dbHelper = Nothing
    '        End If
    '    End Try
    'End Sub

    'Public Sub UpdateDeniedStatus(ByVal MessageID As String)
    '    Dim dbHelper As DataBaseLayer = Nothing
    '    Dim param As DBParameter = Nothing

    '    Try
    '        dbHelper = New DataBaseLayer()

    '        param = New DBParameter
    '        param.Value = MessageID
    '        param.Direction = ParameterDirection.Input
    '        param.DataType = SqlDbType.VarChar
    '        param.Name = "@nMessageID"
    '        dbHelper.DBParametersCol.Add(param)
    '        param = Nothing

    '        param = New DBParameter
    '        param.Value = "Denied"
    '        param.Direction = ParameterDirection.Input
    '        param.DataType = SqlDbType.VarChar
    '        param.Name = "@sStatus"
    '        dbHelper.DBParametersCol.Add(param)
    '        param = Nothing

    '        dbHelper.ExecuteNon_Query("RxChg_UpdateStatus")

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        If dbHelper IsNot Nothing Then
    '            dbHelper.Dispose()
    '            dbHelper = Nothing
    '        End If

    '        param = Nothing
    '    End Try
    'End Sub

    Public Function GetRxMessageByID(ByVal MessageID As String, Optional ByVal MessageType As String = "") As DataRow
        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtFileXML As DataTable = Nothing
        Dim dReturned As DataRow = Nothing

        Try
            param = New DBParameter
            param.Value = MessageID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@ID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = MessageType
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sRequestType"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtFileXML = dbHelper.GetDataTable("gsp_GetRxMessageXMLByID")

            If dtFileXML IsNot Nothing AndAlso dtFileXML.Rows.Count > 0 Then
                dReturned = dtFileXML.Rows(0)
            End If

            Return dReturned

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If

            param = Nothing

            If dtFileXML IsNot Nothing Then
                dtFileXML.Dispose()
                dtFileXML = Nothing
            End If
        End Try
    End Function

    Public Function GetRxMessageXMLByID(ByVal MessageID As String, Optional ByVal MessageType As String = "") As String

        Dim dReturned As DataRow = Nothing
        Dim sReturned As String = ""
        Try
            dReturned = Me.GetRxMessageByID(MessageID, MessageType)

            If dReturned IsNot Nothing AndAlso dReturned.Table.Rows.Count > 0 Then
                sReturned = Convert.ToString(dReturned.Table.Rows(0)("FileXML"))
            End If

            Return sReturned

        Catch ex As Exception
            Throw ex
            Return Nothing       
        End Try
    End Function

    Public Function GetRxChangeRequests(ByVal SPID As String) As DataTable

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtDrugs As DataTable = Nothing

        Try
            param = New DBParameter
            param.Value = SPID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sSPID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtDrugs = dbHelper.GetDataTable("gsp_GetRxChangeRequests") 'RxChg_GetRequests

            Return dtDrugs

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

    Public Function GetAllRxChangeRequests(ByVal FromDate As DateTime, ByVal ToDate As DateTime, Optional ByVal PatientID As String = "") As DataTable

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtRxMessages As DataTable = Nothing

        Try
            param = New DBParameter
            param.Value = FromDate
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.DateTime
            param.Name = "@FromDate"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = ToDate
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.DateTime
            param.Name = "@ToDate"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            If Not String.IsNullOrWhiteSpace(PatientID) Then
                param = New DBParameter
                param.Value = PatientID
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.VarChar
                param.Name = "@PatientID"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing
            End If

            dtRxMessages = dbHelper.GetDataTable("gsp_GetAllRxFillRequests")

            Return dtRxMessages

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function


    Public Enum IDType
        PrescriptionID
        MessageID
    End Enum

    Public Function GetRxFillRequestsById(ByVal ID As String, type As IDType) As DataTable

        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim param As DBParameter
        Dim dtRxFillReq As DataTable = Nothing

        Try
            param = New DBParameter

            If (type = IDType.MessageID) Then
                param.Name = "@MessageID"
            Else
                param.Name = "@PrescriptionID"
            End If

            param.Value = ID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtRxFillReq = dbHelper.GetDataTable("gsp_GetRxFillRequests") 'RxChg_GetRequests

            Return dtRxFillReq

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

#End Region

    Public Function GetProviderPrescriptions(ByVal ProviderID As Int64, Optional ByVal IsCancelled As Boolean = False) As DataTable
        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim dtPrescriberList As DataTable = Nothing
        Dim param As DBParameter

        Try
            param = New DBParameter
            param.Value = ProviderID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@ProviderID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = IsCancelled
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Bit
            param.Name = "@IsCancelled"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtPrescriberList = dbHelper.GetDataTable("gspGetProviderPrescriptions")
            Return dtPrescriberList
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

    Public Function GetPatientProviderDetails(ByVal nProviderID As Int64) As DataTable
        Dim oDB As DataBaseLayer = Nothing
        Dim oParameter As DBParameter = Nothing
        Dim dtprovider As DataTable = Nothing
        Try
            oDB = New DataBaseLayer
            oParameter = New DBParameter

            oParameter = New DBParameter
            oParameter.Direction = ParameterDirection.Input
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Name = "@nProviderID"
            oParameter.Value = nProviderID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            dtprovider = oDB.GetDataTable("gsp_GetPatientProviderDetails")
            Return dtprovider
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetPharmacyDetails(ByVal _Pharmacyid As Int64) As DataTable
        Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
        Try
            Dim _strSQl = "select nContactID, isnull(sName,'') as sName, isnull(sContact,'') as sContact, isnull(sStreet,'') as sStreet,  isnull(sCity,'') as sCity, isnull(sState,'') as sState, isnull(sZIP,'') as sZIP, " _
                        & " isnull(sPhone,'') as sPhone, isnull(sFax,'') as sFax, isnull(sEmail,'') as sEmail, isnull(sServiceLevel,'') as sServiceLevel, isnull(sAddressLine1,'') as sAddressLine1, isnull(sAddressLine2,'') as sAddressLine2, " _
                        & " isnull(sNCPDPID,'') as sNCPDPID, isnull(sPharmacyNPI,'') as sNPI from Contacts_MST where sContactType = 'Pharmacy' and nContactID = " & _Pharmacyid.ToString()

            Dim dtPharmacydetails As DataTable = ogloEMRDatabase.GetDataTable_Query(_strSQl)
            Return dtPharmacydetails
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(ogloEMRDatabase) Then
                ogloEMRDatabase.Dispose()
                ogloEMRDatabase = Nothing
            End If
        End Try
    End Function

    Public Function GetPatientPrescriptions(ByVal PatientID As Int64, Optional ByVal IsCancelled As Boolean = False) As DataTable
        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim dtPrescriberList As DataTable = Nothing
        Dim param As DBParameter

        Try
            param = New DBParameter
            param.Value = PatientID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@PatientID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = IsCancelled
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Bit
            param.Name = "@IsCancelled"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dtPrescriberList = dbHelper.GetDataTable("gspGetPatientPrescriptions")
            Return dtPrescriberList
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

    Public Function GetPrescriberList() As DataTable
        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim dtPrescriberList As DataTable = Nothing

        Try
            dtPrescriberList = dbHelper.GetDataTable("GetPrescriberList")
            Return dtPrescriberList
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

    Public Function GetSurescriptServiceURL() As String
        Dim dbHelper As DataBaseLayer = New DataBaseLayer
        Dim dtURL As DataTable = Nothing
        Dim sReturned As String = String.Empty

        Try
            dtURL = dbHelper.GetDataTable("GetSurescriptServiceURL")

            If dtURL IsNot Nothing AndAlso dtURL.Rows.Count > 0 Then
                sReturned = Convert.ToString(dtURL.Rows(0)("sSettingsValue"))
            End If

            Return sReturned
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(dbHelper) Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If
        End Try
    End Function

    Public Sub SaveDeniedTransaction(ByVal RxRequestMsg As String, ByVal ResponseMsg As Schema.MessageType, ByVal ApprovalStatus As String, ByVal StatusMessageType As String, ByVal DenialReason As String, ByVal Notes As String, ByVal DenialCode As String, ByVal PatientID As String, ByVal ProviderID As String, ByVal blnType As Boolean, Optional ByVal UpdateStatus As Boolean = True)

        '' Saving the Posted Change Response to DB for further reference

        Using DBLayer As New gloSureScriptDBLayer()
            DBLayer.InsertintoMessageTransaction(ResponseMsg, "RxChangeResponse")
        End Using

        '' Saving the Status message details against the response
        InsertResponseTransaction(ApprovalStatus, StatusMessageType, DenialReason, Notes, ResponseMsg.Header.MessageID, DenialCode, PatientID, ProviderID, blnType)

        '' Updating the denied status against the response
        If UpdateStatus Then
            UpdateRxMessageStatus(RxRequestMsg, "2", "RxChange")
        End If

    End Sub

    Public Function InsertResponseTransaction(ByVal ApprovalStatus As String, ByVal StatusMessageType As String, ByVal DenialReason As String, ByVal Notes As String, ByVal MessageID As String, ByVal DenialCode As String, ByVal PatientID As String, ByVal ProviderID As String, ByVal blnType As Boolean) As Boolean

        Dim dbHelper As DataBaseLayer = Nothing
        Dim param As DBParameter = Nothing

        Try
            dbHelper = New DataBaseLayer()

            Dim strApproved As String
            Select Case ApprovalStatus
                Case True
                    strApproved = "AP"
                Case False
                    strApproved = "DN"
                Case Else
                    strApproved = "DR"
            End Select


            Dim _DeniedStatus As String = ""
            If StatusMessageType <> "" Then
                Dim sDeniedMessage As String() = Split(StatusMessageType, ":")
                If sDeniedMessage.Length > 2 Then
                    _DeniedStatus = sDeniedMessage(5)
                End If
            End If

            Dim sDenyReason As String = ""
            If Not IsNothing(DenialReason) Then
                sDenyReason = DenialReason.Replace("'", "''")
            End If

            Dim sNotes As String = ""
            If Not IsNothing(Notes) Then
                sNotes = Notes.Replace("'", "''''")
            End If

            param = New DBParameter
            param.Value = IIf(MessageID Is Nothing, "", MessageID)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sMessageID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = IIf(strApproved Is Nothing, "", strApproved)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sType"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = IIf(sNotes Is Nothing, "", sNotes)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sNotes"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = IIf(DenialCode Is Nothing, "", DenialCode)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sDenialReasoncode"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = IIf(sDenyReason Is Nothing, "", sDenyReason)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sDenialReason"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            If blnType Then
                param.Value = "1"
            Else
                param.Value = "0"
            End If

            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@bType"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = IIf(PatientID Is Nothing, "", PatientID)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@nPatientID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = IIf(ProviderID Is Nothing, "", ProviderID)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@nProviderID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = IIf(_DeniedStatus Is Nothing, "", _DeniedStatus)
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sERXStatus"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = IIf(StatusMessageType Is Nothing, "", StatusMessageType.ToString().Replace("'", "''"))
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sERXStatusMessage"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing


            dbHelper.ExecuteNon_Query("InsertResponseTranscation")

            Return True

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If dbHelper IsNot Nothing Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If

            param = Nothing
        End Try
    End Function

    Public Function GetPatientIdByDemographics(ByVal sFirstName As String, ByVal sLastName As String, ByVal sGender As String, ByVal sDOB As Date) As Int64

        Dim dbHelper As DataBaseLayer = Nothing
        Dim param As DBParameter = Nothing
        Dim oRes As Object = Nothing

        Try
            dbHelper = New DataBaseLayer()

            param = New DBParameter
            param.Value = sFirstName
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sFirstName"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = sLastName
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sLastName"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = sGender
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sGender"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = sDOB
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.Date
            param.Name = "@sDOB"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            oRes = dbHelper.GetDataValue("gsp_GetPatientIdByDemographics", True)

            If Not IsNothing(oRes) Then
                Return Convert.ToInt64(oRes)
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
            Return 0
        Finally
            If dbHelper IsNot Nothing Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If

            param = Nothing
        End Try

    End Function

    Public Function GetPharmacyIDByNCPDP(ByVal pharmacyNCPDP As String)
        Dim PharmacyId As Int64 = 0
        Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer

        Try
            Dim strquery As String = "select isnull(nContactId,0) as PharmacyId from Contacts_MST where sNCPDPID = '" & pharmacyNCPDP & "'"
            PharmacyId = _gloEMRDatabase.GetDataValue(strquery, False)

            Return PharmacyId

        Catch ex As Exception
            Throw
            Return PharmacyId
        Finally
            If Not IsNothing(_gloEMRDatabase) Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If
        End Try
    End Function

    Function UpdateCancelledMedicationStatus(ByVal PatientID As Int64, ByVal TransactionID As Long, ByVal Status As String)
        Dim _gloEMRDatabase As DataBaseLayer = Nothing
        _gloEMRDatabase = New DataBaseLayer
        Dim objParameter As DBParameter
        Try
            objParameter = New DBParameter
            objParameter.Value = TransactionID
            objParameter.Direction = ParameterDirection.Input
            objParameter.DataType = SqlDbType.BigInt
            objParameter.Name = "@nPrescription"
            _gloEMRDatabase.DBParametersCol.Add(objParameter)
            objParameter = Nothing

            objParameter = New DBParameter
            objParameter.Value = PatientID
            objParameter.Direction = ParameterDirection.Input
            objParameter.DataType = SqlDbType.BigInt
            objParameter.Name = "@nPatientID"
            _gloEMRDatabase.DBParametersCol.Add(objParameter)
            objParameter = Nothing

            objParameter = New DBParameter
            objParameter.Value = Status
            objParameter.Direction = ParameterDirection.Input
            objParameter.DataType = SqlDbType.VarChar
            objParameter.Name = "@sStatus"
            _gloEMRDatabase.DBParametersCol.Add(objParameter)
            objParameter = Nothing

            _gloEMRDatabase.ExecuteNon_Query("gspUpdateMedicationStatus")

        Catch ex As Exception
            Throw ex
            Return Nothing

        Finally
            If Not IsNothing(_gloEMRDatabase) Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Function UpdateMedicationStatus(ByVal PatientID As Int64, ByVal TransactionID As Long, ByVal IsChangeRequest As Boolean, Optional ByVal VisitID As Int64 = 0)
        Dim _gloEMRDatabase As DataBaseLayer = Nothing
        _gloEMRDatabase = New DataBaseLayer
        Dim objParameter As DBParameter
        Try
            objParameter = New DBParameter
            objParameter.Value = TransactionID
            objParameter.Direction = ParameterDirection.Input
            objParameter.DataType = SqlDbType.BigInt
            objParameter.Name = "@nPrescription"
            _gloEMRDatabase.DBParametersCol.Add(objParameter)
            objParameter = Nothing

            objParameter = New DBParameter
            objParameter.Value = PatientID
            objParameter.Direction = ParameterDirection.Input
            objParameter.DataType = SqlDbType.BigInt
            objParameter.Name = "@nPatientID"
            _gloEMRDatabase.DBParametersCol.Add(objParameter)
            objParameter = Nothing

            If IsChangeRequest Then
                objParameter = New DBParameter
                objParameter.Value = VisitID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nVisitID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                _gloEMRDatabase.ExecuteNon_Query("gspUpdateMedicationforRxChange")
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.ModifyMedication, gloAuditTrail.ActivityType.Status, "Medication status updated to Discontinued (RxChange)", PatientID, TransactionID, 0, gloAuditTrail.ActivityOutCome.Success)
            Else
                _gloEMRDatabase.ExecuteNon_Query("gspUpdateMedicationforApprovedRefillRequest")
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.ModifyMedication, gloAuditTrail.ActivityType.Status, "Medication status updated to Discontinued (RefillRequest)", PatientID, TransactionID, 0, gloAuditTrail.ActivityOutCome.Success)
            End If

        Catch ex As Exception
            Throw ex
            Return Nothing

        Finally
            If Not IsNothing(_gloEMRDatabase) Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Public Function FillMedPrescribed(ByVal PrescriptionID As Int64) As gloGlobal.Common.ServiceObjectBase.MedPrescribed

        Dim medPrescribed As gloGlobal.Common.ServiceObjectBase.MedPrescribed = New gloGlobal.Common.ServiceObjectBase.MedPrescribed()
        Dim _gloEMRDatabase As DataBaseLayer = Nothing
        _gloEMRDatabase = New DataBaseLayer
        Dim objParameter As DBParameter
        Dim dt As DataTable
        Try

            objParameter = New DBParameter
            objParameter.Value = PrescriptionID
            objParameter.Direction = ParameterDirection.Input
            objParameter.DataType = SqlDbType.BigInt
            objParameter.Name = "@PrescriptionID"
            _gloEMRDatabase.DBParametersCol.Add(objParameter)
            objParameter = Nothing

            dt = _gloEMRDatabase.GetDataTable("gsp_GetPrescriptionByID")

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    medPrescribed.medication = dt.Rows(0)("Medication")
                    medPrescribed.ndc = dt.Rows(0)("NDCCode")


                    If dt.Rows(0)("sAmount").Trim <> "" Then 'fixed bug 5453
                        Dim strDispense As String() = Split(dt.Rows(0)("sAmount").Trim, " ")
                        If strDispense.Length > 1 Then
                            medPrescribed.qty = strDispense(0)
                        Else
                            medPrescribed.qty = dt.Rows(0)("sAmount").Trim
                        End If
                    Else
                        medPrescribed.qty = dt.Rows(0)("sAmount").Trim
                    End If

                    medPrescribed.qtyUnit = dt.Rows(0)("sPotencyCode") 'Potency 

                    Dim nDaysSupply As Integer = 0
                    If dt.Rows(0)("sDuration").Trim.Length > 0 AndAlso Val(dt.Rows(0)("sDuration")) <> 0 Then
                        If IsNumeric(dt.Rows(0)("sDuration")) Then
                            nDaysSupply = Val(dt.Rows(0)("sDuration"))
                        Else
                            Dim nDuration As String() = Nothing
                            Dim numberofDays As Integer
                            nDuration = dt.Rows(0)("sDuration").Trim.Split(" ")
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
                    End If

                    medPrescribed.days = nDaysSupply

                    medPrescribed.direction = dt.Rows(0)("sFrequency") ' DrugRoute

                    medPrescribed.refill = dt.Rows(0)("sRefills")

                    'Select Case selectedRx.RefillQualifier
                    '    Case "PRN"
                    '        '"PRN"
                    '    Case "R"
                    '        ' "R"
                    '    Case "P"
                    '        '"P"
                    'End Select

                    medPrescribed.dea = dt.Rows(0)("IsNarcotics")

                    medPrescribed.substitute = dt.Rows(0)("bMaySubstitute")

                    medPrescribed.written = dt.Rows(0)("dtPrescriptionDate")

                    medPrescribed.note = dt.Rows(0)("sNotes")

                    medPrescribed.pan = dt.Rows(0)("PriorAuthorizationNumber")

                    medPrescribed.pas = dt.Rows(0)("PriorAuthorizationStatus")

                    'Diagnosis

                    Dim sICDRevPrimary As String = Nothing
                    Dim sICDCodePrimary As String = Nothing

                    Dim sICDRevSecondary As String = Nothing
                    Dim sICDCodeSecondary As String = Nothing

                    Dim dtDiagnosis As DataTable = Nothing
                    If Not String.IsNullOrEmpty(dt.Rows(0)("Problems")) AndAlso Not String.IsNullOrWhiteSpace(dt.Rows(0)("Problems")) Then
                        dtDiagnosis = GetDiagnosisCodes(dt.Rows(0)("Problems"))

                        For Each row As DataRow In dtDiagnosis.Rows
                            If IsNothing(sICDCodePrimary) Then
                                sICDRevPrimary = Convert.ToString(row("sICDRevision"))
                                sICDCodePrimary = Convert.ToString(row("sICD9Code"))
                            Else
                                If IsNothing(sICDCodeSecondary) Then
                                    sICDRevSecondary = Convert.ToString(row("sICDRevision"))
                                    sICDCodeSecondary = Convert.ToString(row("sICD9Code"))
                                End If
                            End If
                        Next

                    End If
                    dtDiagnosis = Nothing

                    If Not String.IsNullOrEmpty(sICDRevPrimary) And Not String.IsNullOrEmpty(sICDCodePrimary) Then
                        If sICDRevPrimary = "10" Then
                            medPrescribed.DxQual1 = "ABF"
                        Else
                            medPrescribed.DxQual1 = "DX"
                        End If
                        medPrescribed.DxVal1 = sICDCodePrimary

                    End If

                    If Not String.IsNullOrEmpty(sICDRevSecondary) And Not String.IsNullOrEmpty(sICDCodeSecondary) Then
                        If sICDRevSecondary = "10" Then
                            medPrescribed.DxQual2 = "ABF"
                        Else
                            medPrescribed.DxQual2 = "DX"
                        End If
                        medPrescribed.DxVal2 = sICDCodeSecondary

                    End If
                End If

            End If

        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try

        Return medPrescribed

    End Function

    Public Function GeteRxDetailsForCancelRx(ByVal sPrescriptionID As String) As DataTable

        Dim dbHelper As DataBaseLayer = Nothing
        Dim param As DBParameter = Nothing
        Dim RetDT As DataTable = Nothing
        Try
            dbHelper = New DataBaseLayer()

            'param = New DBParameter
            'param.Value = nPatientID
            'param.Direction = ParameterDirection.Input
            'param.DataType = SqlDbType.BigInt
            'param.Name = "@PatientID"
            'dbHelper.DBParametersCol.Add(param)
            'param = Nothing

            param = New DBParameter
            param.Value = sPrescriptionID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@PrescriptionId"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            RetDT = dbHelper.GetDataTable("gsp_GetERXDetailsForCancelRx", True)


            Return RetDT

        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If dbHelper IsNot Nothing Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If

            param = Nothing
        End Try

    End Function

    'End Function
    Public Function GetDiagnosisCodes(ByVal ProblemIDs As String) As DataTable

        Dim _gloEMRDatabase As New DataBaseLayer()
        Dim objParameter As DBParameter
        Dim dt As DataTable = Nothing

        Try
            objParameter = New DBParameter
            objParameter.Value = ProblemIDs
            objParameter.Direction = ParameterDirection.Input
            objParameter.DataType = SqlDbType.VarChar
            objParameter.Name = "@sProblemIDs"
            _gloEMRDatabase.DBParametersCol.Add(objParameter)
            objParameter = Nothing

            dt = _gloEMRDatabase.GetDataTable("gsp_getDiagnosisCodes") 'gsp_GetDiagnosisCodesForeRx
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

            If Not IsNothing(_gloEMRDatabase) Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If

        End Try
        Return Nothing
    End Function

    Public Function GetProblemIds(ByVal ICDRevision As String, ByVal DiagnosisCode As String) As DataRow

        Dim _gloEMRDatabase As New DataBaseLayer()
        Dim objParameter As DBParameter
        Dim dt As DataTable = Nothing

        Try
            objParameter = New DBParameter
            objParameter.Value = DiagnosisCode
            objParameter.Direction = ParameterDirection.Input
            objParameter.DataType = SqlDbType.VarChar
            objParameter.Name = "@sDiagnosisCode"
            _gloEMRDatabase.DBParametersCol.Add(objParameter)
            objParameter = Nothing

            objParameter = New DBParameter
            If ICDRevision = "DX" Then
                objParameter.Value = "9"
            ElseIf ICDRevision = "ABF" Then
                objParameter.Value = "10"
            End If

            objParameter.Direction = ParameterDirection.Input
            objParameter.DataType = SqlDbType.VarChar
            objParameter.Name = "@sICDRevision"
            _gloEMRDatabase.DBParametersCol.Add(objParameter)
            objParameter = Nothing

            dt = _gloEMRDatabase.GetDataTable("gsp_getProblemIds")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

            If Not IsNothing(_gloEMRDatabase) Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If

        End Try
        Return Nothing
    End Function

    Public Function GetPotencyCode() As DataTable
        Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
        Dim dtPotencyCode As DataTable = Nothing
        Try

            dtPotencyCode = ogloEMRDatabase.GetDataTable("gsp_GetPotencyCode")

            Return dtPotencyCode

        Catch ex As Exception
            Dim objex As New gloEMRPrescription.PrescriptionException
            Throw objex
        Finally
            If Not IsNothing(ogloEMRDatabase) Then
                ogloEMRDatabase.Dispose()
                ogloEMRDatabase = Nothing
            End If

        End Try
    End Function

    Public Function GetDrugRoutes(ByVal mpid As Integer) As List(Of String)

        Dim res As New List(Of String)

        Dim _gloEMRDatabase As New DataBaseLayer
        Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter

        Dim _dtRoutes As DataTable = Nothing
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@mpid"
            oParamater.Value = mpid
            _gloEMRDatabase.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            _dtRoutes = _gloEMRDatabase.GetDataTable("gsp_GetDrugsRoutes")


            If _dtRoutes IsNot Nothing AndAlso _dtRoutes.Rows.Count > 0 Then
                res.Add("")

                For Each dr As DataRow In _dtRoutes.Rows
                    res.Add(dr(0).ToString())
                Next
            End If

            Return (res)
        Catch ex As Exception
            Return Nothing
        Finally

        End Try

    End Function
End Class
