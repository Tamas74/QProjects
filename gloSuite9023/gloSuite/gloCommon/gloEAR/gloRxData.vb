Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Win32
Imports gloEAR.mdlGeneral

Public Class gloRxData

    Public Shared gstrDatabaseConnectionString As String = ""
    Public Shared eARFileNAme As String = ""
    Shared sPrescriberConfidentialIdentifier As String = ""

#Region "Starting Conection"

    Private Shared Function GetConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal WindowsAuthentication As Boolean, Optional ByVal strUserName As String = "", Optional ByVal strPassword As String = "") As String
        ' Variable to store SQL Connection String
        Dim strConnectionString As String

        'Check the SQL Server Authentication
        If WindowsAuthentication = True Then
            'Build Connection String by Windows Authentication
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
        Else
            'Build Connection String by SQL Server Authentication
            strConnectionString = "SERVER=" & strSQLServerName & ";UID=" & strUserName & ";PWD=" & strPassword & ";DATABASE=" & strDatabase
        End If

        'Return Builded connection string
        Return strConnectionString
    End Function

    Public Shared Function IsConnect(ByVal strSQLServerName As String, ByVal strDatabase As String, Optional ByVal WindowsAuthentication As Boolean = False, Optional ByVal strUserName As String = "", Optional ByVal strPassword As String = "") As Boolean
        'Create the object of SQL Connection class
        Dim objCon As New SqlConnection
        Try
            'Assign the connection string
            objCon.ConnectionString = GetConnectionString(strSQLServerName, strDatabase, WindowsAuthentication, strUserName, strPassword)
            'UpdateLog(objCon.ConnectionString)
            'Open the connection
            objCon.Open()

            'Connection to SQL Server database successfully established
            Return True
        Catch ex As Exception
            ' Updatelog(ex.ToString)
            Return False
        Finally
            'Close the  connection
            objCon.Close()
            'Connection to SQL Server database is not established
            objCon = Nothing
        End Try

    End Function
#End Region



    Public Sub New(ByVal sConnectionString As String)
        gstrDatabaseConnectionString = sConnectionString

    End Sub

    Public Function GetEARFileHeaderDetails() As String
        Dim _strData As String = ""

        Try

        Catch ex As Exception

        End Try

        Return _strData
    End Function

    Public Function GetEARReportHeaderDetails() As String
        Dim _strData As String = ""

        Try

        Catch ex As Exception

        End Try

        Return _strData
    End Function

    Public Function GetEARReportAggregateDetails(ByVal objAggregates As gloEAR.gloEARSections.gloEARReportDetailAggregates) As DataTable
        Dim dtEARAggregateDetails As New DataTable
        Dim conn As New SqlConnection()
        Dim cmd As New SqlCommand
        Dim _strSQL As String = ""
        Dim da As SqlDataAdapter
        Dim objEARAggregate As gloEAR.gloEARSections.gloEARReportDetailAggregate

        Try
            'get the Provider Details using the ProviderID
            _strSQL = " SELECT ISNULL(Provider_MST.sDEA, '') AS PrescriberDEA, ISNULL(Provider_MST.sState, '') AS PrescriberState, ISNULL(Provider_MST.sZIP, '') AS PrescriberZIPCODE, ISNULL(Provider_MST.sMedicalLicenseNo, '') AS PrescriverStateLicenseNumber, ISNULL(Provider_MST.sNPI, '') AS PrescriberNPI, ISNULL(Provider_MST.sFirstName, '') AS PrescriberFirstName, ISNULL(Provider_MST.sLastName, '') AS PrescriberLastName, Prescription.dtPrescriptionDate AS TransactionDate, Prescription.nPrescriptionID AS PrescriptionID FROM         Prescription INNER JOIN Provider_MST ON Prescription.nProviderId = Provider_MST.nProviderID where nProviderID = " ''''& nProviderID

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = _strSQL

            da = New SqlDataAdapter(cmd)
            da.Fill(dtEARAggregateDetails)


            'fill the object with the data

            For i As Integer = 0 To dtEARAggregateDetails.Rows.Count - 1
                objEARAggregate = New gloEAR.gloEARSections.gloEARReportDetailAggregate

                objEARAggregate.PrescriberDEA = dtEARAggregateDetails.Rows(i)("PrescriberDEA")
                objEARAggregate.PrescriberNPI = dtEARAggregateDetails.Rows(i)("PrescriberNPI")
                objEARAggregate.PrescriberStateLicenseNumber = dtEARAggregateDetails.Rows(i)("PrescriverStateLicenseNumber")
                objEARAggregate.PrescriberState = dtEARAggregateDetails.Rows(i)("PrescriberState")
                objEARAggregate.PrescriberZIPCODE = dtEARAggregateDetails.Rows(i)("PrescriberZIPCODE")
                objEARAggregate.PrescriberNPI = dtEARAggregateDetails.Rows(i)("PrescriberNPI")

            Next

        Catch sqlEx As SqlException
            Throw sqlEx
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

        Return dtEARAggregateDetails
    End Function

    Public Function GetEARReportAggregateDetails() As DataTable
        Dim dtEARAggregateDetails As New DataTable
        '  Dim conn As New SqlConnection(GetConnectionString())
        Dim conn As New SqlConnection(gstrDatabaseConnectionString)
        Dim cmd As New SqlCommand
        Dim _strSQL As String = ""
        Dim da As SqlDataAdapter
        Dim objEARAggregate As gloEAR.gloEARSections.gloEARReportDetailAggregate

        Try
            'get the Provider Details using the ProviderID
            _strSQL = " SELECT ISNULL(Provider_MST.sDEA, '') AS PrescriberDEA, ISNULL(Provider_MST.sState, '') AS PrescriberState, ISNULL(Provider_MST.sZIP, '') AS PrescriberZIPCODE, ISNULL(Provider_MST.sMedicalLicenseNo, '') AS PrescriberStateLicenseNumber, ISNULL(Provider_MST.sNPI, '') AS PrescriberNPI, ISNULL(Provider_MST.sFirstName, '') AS PrescriberFirstName, ISNULL(Provider_MST.sLastName, '') AS PrescriberLastName, Prescription.dtPrescriptionDate AS TransactionDate, Prescription.nPrescriptionID AS PrescriptionID FROM         Prescription INNER JOIN Provider_MST ON Prescription.nProviderId = Provider_MST.nProviderID " '&  " where Provider_MST.nProviderID = " & nProviderID

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = _strSQL

            da = New SqlDataAdapter(cmd)
            da.Fill(dtEARAggregateDetails)


            'fill the object with the data

            For i As Integer = 0 To dtEARAggregateDetails.Rows.Count - 1
                objEARAggregate = New gloEAR.gloEARSections.gloEARReportDetailAggregate

                objEARAggregate.PrescriberDEA = dtEARAggregateDetails.Rows(i)("PrescriberDEA")
                objEARAggregate.PrescriberNPI = dtEARAggregateDetails.Rows(i)("PrescriberNPI")
                objEARAggregate.PrescriberStateLicenseNumber = dtEARAggregateDetails.Rows(i)("PrescriberStateLicenseNumber")
                objEARAggregate.PrescriberState = dtEARAggregateDetails.Rows(i)("PrescriberState")
                objEARAggregate.PrescriberZIPCODE = dtEARAggregateDetails.Rows(i)("PrescriberZIPCODE")
                objEARAggregate.PrescriberNPI = dtEARAggregateDetails.Rows(i)("PrescriberNPI")

            Next

        Catch sqlEx As SqlException
            Throw sqlEx
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

        Return dtEARAggregateDetails
    End Function

    Public Function GetEARRxLevelRecords(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime) As DataTable
        Dim dtEARRxRecords As New DataTable
        Dim conn As New SqlConnection(gstrDatabaseConnectionString)
        Dim cmd As New SqlCommand
        Dim _strSQL As String = ""
        Dim da As New SqlDataAdapter

        Try
            'get the Provider Details using the ProviderID
            '   _strSQL = " SELECT ISNULL(Provider_MST.sDEA, '') AS PrescriberDEA, ISNULL(Provider_MST.sState, '') AS PrescriberState, ISNULL(Provider_MST.sZIP, '') AS PrescriberZIPCODE, ISNULL(Provider_MST.sMedicalLicenseNo, '') AS PrescriberStateLicenseNumber, ISNULL(Provider_MST.sNPI, '') AS PrescriberNPI, ISNULL(Provider_MST.sFirstName, '') AS PrescriberFirstName, ISNULL(Provider_MST.sLastName, '') AS PrescriberLastName, Prescription.dtPrescriptionDate AS TransactionDate, Prescription.nPrescriptionID AS PrescriptionID FROM         Prescription INNER JOIN Provider_MST ON Prescription.nProviderId = Provider_MST.nProviderID " '&  " where Provider_MST.nProviderID = " & nProviderID
            _strSQL = "gloEARData"

            Dim objParameter As SqlParameter

            objParameter = New SqlParameter
            objParameter.ParameterName = "@FromDate"
            objParameter.DbType = DbType.DateTime
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = dtFromDate
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing

            objParameter = New SqlParameter
            objParameter.ParameterName = "@ToDate"
            objParameter.DbType = DbType.DateTime
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = dtToDate
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing

            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = _strSQL

            da = New SqlDataAdapter(cmd)
            da.Fill(dtEARRxRecords)
        Catch sqlEx As SqlException
            mdlGeneral.UpdateLog("GetEARRxLevelRecords() - " & sqlEx.ToString)
            Throw sqlEx
        Catch ex As Exception
            mdlGeneral.UpdateLog("GetEARRxLevelRecords() - " & ex.ToString)
            Throw ex
        Finally
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try

        Return dtEARRxRecords
    End Function

    ''' <summary>
    ''' 'this will return records for current date only
    ''' </summary>
    ''' <param name="dtFromDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEARRxLevelRecords_Today(ByVal dtFromDate As DateTime) As DataTable
        Dim dtEARRxRecords As New DataTable
        Dim conn As New SqlConnection(gstrDatabaseConnectionString)
        Dim cmd As New SqlCommand
        Dim _strSQL As String = ""
        Dim da As New SqlDataAdapter

        Try
            'get the Provider Details using the ProviderID
            '   _strSQL = " SELECT ISNULL(Provider_MST.sDEA, '') AS PrescriberDEA, ISNULL(Provider_MST.sState, '') AS PrescriberState, ISNULL(Provider_MST.sZIP, '') AS PrescriberZIPCODE, ISNULL(Provider_MST.sMedicalLicenseNo, '') AS PrescriberStateLicenseNumber, ISNULL(Provider_MST.sNPI, '') AS PrescriberNPI, ISNULL(Provider_MST.sFirstName, '') AS PrescriberFirstName, ISNULL(Provider_MST.sLastName, '') AS PrescriberLastName, Prescription.dtPrescriptionDate AS TransactionDate, Prescription.nPrescriptionID AS PrescriptionID FROM         Prescription INNER JOIN Provider_MST ON Prescription.nProviderId = Provider_MST.nProviderID " '&  " where Provider_MST.nProviderID = " & nProviderID
            _strSQL = "gloEARData_Today"

            Dim objParameter As SqlParameter

            objParameter = New SqlParameter
            objParameter.ParameterName = "@FromDate" '''''todays date
            objParameter.DbType = DbType.DateTime
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = dtFromDate
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing

            'objParameter = New SqlParameter
            'objParameter.ParameterName = "@ToDate"
            'objParameter.DbType = DbType.DateTime
            'objParameter.Direction = ParameterDirection.Input
            'objParameter.Value = dtToDate
            'cmd.Parameters.Add(objParameter)
            'objParameter = Nothing

            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = _strSQL

            da = New SqlDataAdapter(cmd)
            da.Fill(dtEARRxRecords)
        Catch sqlEx As SqlException
            mdlGeneral.UpdateLog("GetEARRxLevelRecords_Today() - " & sqlEx.ToString)
            Throw sqlEx
        Catch ex As Exception
            mdlGeneral.UpdateLog("GetEARRxLevelRecords_Today() - " & ex.ToString)
            Throw ex
        Finally
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try

        Return dtEARRxRecords
    End Function


    Public Shared Function IsFileSentForThisWeek(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime) As Integer

        Dim conn As New SqlConnection(gstrDatabaseConnectionString)

        Dim cmd As New SqlCommand
        Dim strQ As String = ""
        Try
            strQ = "gsp_RxHIsFileSentForThisWeek"

            Dim objParameter As SqlParameter

            objParameter = New SqlParameter
            objParameter.ParameterName = "@FromDate"
            objParameter.DbType = DbType.DateTime
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = dtFromDate
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing

            objParameter = New SqlParameter
            objParameter.ParameterName = "@ToDate"
            objParameter.DbType = DbType.DateTime
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = dtToDate
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing


            conn.Open()


            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandText = strQ

            Dim result = cmd.ExecuteScalar()


            Return result
            

        Catch ex As Exception
            ''''if there is a exception the return -1
            mdlGeneral.UpdateLog("IsFileSentForThisWeek() - " & ex.ToString)
            Return -1
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Shared Function IsFileSentForToday(ByVal dtFromDate As DateTime) As Integer

        Dim conn As New SqlConnection(gstrDatabaseConnectionString)

        Dim cmd As New SqlCommand
        Dim strQ As String = ""
        Try
            strQ = "gsp_RxHIsEARFileSentForToday"

            Dim objParameter As SqlParameter

            objParameter = New SqlParameter
            objParameter.ParameterName = "@FromDate" ''''''''this is considered as todays date or current date
            objParameter.DbType = DbType.DateTime
            objParameter.Direction = ParameterDirection.Input
            objParameter.Value = dtFromDate
            cmd.Parameters.Add(objParameter)
            objParameter = Nothing

            'objParameter = New SqlParameter
            'objParameter.ParameterName = "@ToDate"
            'objParameter.DbType = DbType.DateTime
            'objParameter.Direction = ParameterDirection.Input
            'objParameter.Value = dtToDate
            'cmd.Parameters.Add(objParameter)
            'objParameter = Nothing


            conn.Open()


            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandText = strQ

            Dim result = cmd.ExecuteScalar()


            Return result


        Catch ex As Exception
            ''''if there is a exception the return -1
            mdlGeneral.UpdateLog("IsFileSentForToday() - " & ex.ToString)
            Return -1
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
  

#Region "write/generate report file"

    '\\ <<NOT in Used>>
    Public Function GenerateEARTextFile(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal gstrEARDownloadDirectory As String, ByVal rptTransmission As String) As Boolean

        'use eARFileName instead of sEARFileName
        Dim dtExtractDate As DateTime
        eARFileNAme = RetrieveFileName(dtExtractDate, gstrEARDownloadDirectory)

        Dim strWriter As New StreamWriter(eARFileNAme, False)
        Dim objRxData As New gloEAR.gloRxData(gstrDatabaseConnectionString)
        Dim dtRxDetails As New DataTable
        Dim _result As Boolean = True
        Dim objeRxCredentials As New clsRxCredentials

        mdlGeneral.sEARTransmissionAction = rptTransmission '\\get New or retransmission


        '---------------report start date & end date

        Dim reportStartdate As String = dtFromDate.Date.ToString("yyyyMMdd")
        Dim reportEnddate As String = dtToDate.Date.ToString("yyyyMMdd")

        Dim days As Long = DateDiff(DateInterval.Day, dtFromDate, dtToDate)
        Dim k As Integer = DateDiff(DateInterval.Weekday, dtFromDate, dtToDate)

        'If days > 6 Then
        '    Dim dtTempfromdate As DateTime = dtToDate.AddDays(-6)  'find sunday of that week ie. start date for report
        '    reportStartdate = dtTempfromdate.Date.ToString("yyyyMMdd")
        'End If
        If k > 0 Then
            For j As Integer = 0 To k


            Next
            Dim dtTempTodate As DateTime = dtFromDate.AddDays(6)
            If dtToDate.Date = dtTempTodate.Date Then

            End If
            Dim dtTempfromdate As DateTime = dtToDate.AddDays(-6)
            If dtTempTodate = dtTempfromdate Then

            End If
        End If


        Try

            'get the SenderId and Pwd from the database

            'objSettings.GetSettings_EAR()
            ' eRxSenderID = objSettings.eARSenderID
            '  eRxSenderParticipantPassword = objSettings.eARSenderParticipantPassword
            '  objSettings = Nothing
            mdlGeneral.eRxHubParticipantId = objeRxCredentials.SenderID
            mdlGeneral.eRxHubPassword = objeRxCredentials.SenderParticipantPassword
            objeRxCredentials.Dispose()

            '#########################################################################################
            'write the file header fields
            '#########################################################################################

            strWriter.Write("HDR")
            strWriter.Write("|")

            'Version
            strWriter.Write("10")
            strWriter.Write("|")

            '   objEARFileHeader.SenderID()
            strWriter.Write(mdlGeneral.eRxHubParticipantId)
            strWriter.Write("|")

            'objEARFileHeader.SenderParticipantPwd
            strWriter.Write(mdlGeneral.eRxHubPassword)
            strWriter.Write("|")

            'objEARFileHeader.ReceiverID
            strWriter.Write("RXHUB")
            strWriter.Write("|")

            'objEARFileHeader.SourceName
            ' strWriter.Write("gloEMR5.0")
            strWriter.Write("gloStream9")
            strWriter.Write("|")

            'objEARFileHeader.TransmissionControlNumber

            '<<<<<<<<<<<<<<<<<<Generate the TransmissionControlNumber >>>>>>>>>>>>>>>>
            '<<based  on current datetime
            '<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            Dim TransmissionControlNumber As String = ""
            TransmissionControlNumber = Guid.NewGuid().ToString()
            TransmissionControlNumber = Mid(TransmissionControlNumber.ToString, 1, 8)
            strWriter.Write(TransmissionControlNumber)
            strWriter.Write("|")

            'objEARFileHeader.TransmissionDate()
            strWriter.Write(Now.ToString("yyyyMMdd"))
            strWriter.Write("|")

            'objEARFileHeader.TransmissionTime
            Dim _strTime As String
            _strTime = Now.ToString("hhmmss") + "00"
            strWriter.Write(_strTime)
            strWriter.Write("|")

            'objEARFileHeader.TransmissionFileType
            'EAR
            strWriter.Write(mdlGeneral.sEARTransmissionFileType)
            strWriter.Write("|")

            'Transmission Action -- set it to blank
            strWriter.Write("")
            strWriter.Write("|")

            'Extract Date
            '<<<<<<<<<<<<<<<<<<<<<<Needs to be Verified>>>>>>>>>>>>>>>>>>>>>
            '<<<<<<<<<<<<<<<<<<<<<<Needs to be Verified>>>>>>>>>>>>>>>>>>>>>
            strWriter.Write(Now.ToString("yyyyMMdd"))
            strWriter.Write("|")

            'File Type
            'T for test and P for Production
            strWriter.Write(mdlGeneral.sEARFileType)

            'insert a new line character
            strWriter.WriteLine() '("\\n")

            '#########################################################################################
            'write the report header fields
            '#########################################################################################

            'objEARReportHeader.RecordType
            strWriter.Write("RHD")
            strWriter.Write("|")

            'objEARReportHeader.ReportStartDate
            'strWriter.Write(sEARStartDate)
            'should be Sunday
            Dim sReportStartDate As String = ""
            sReportStartDate = reportStartdate '\\ commented on 20090812 dtFromDate.Date.ToString("yyyyMMdd")
            strWriter.Write(sReportStartDate)
            strWriter.Write("|")

            'objEARReportHeader.ReportEndDate
            'should be saturday
            Dim sReportEndDate As String = ""
            sReportEndDate = reportEnddate '\\ commented on 20090812 dtToDate.Date.ToString("yyyyMMdd")
            'strWriter.Write(sEAREndDate)
            strWriter.Write(sReportEndDate)
            strWriter.Write("|")

            'objEARReportHeader.TransmissionAction
            'N for new and R for retransmission
            '   sEARTransmissionAction = "N"
            strWriter.Write(mdlGeneral.sEARTransmissionAction)
            'strWriter.Write("|")
            strWriter.WriteLine()

            '#########################################################################################
            'write the report aggregate fields and report detail fields
            '#########################################################################################


            Dim nCurrentPrescriberID As Long = 0
            Dim nPrevPrescriberID As Long = 0


            '  dtRxDetails = objRxData.GetEARReportAggregateDetails()
            dtRxDetails = objRxData.GetEARRxLevelRecords(dtFromDate, dtToDate)

            Dim cnt As Integer = 0

            If dtRxDetails.Rows.Count > 0 Then
                nCurrentPrescriberID = dtRxDetails.Rows(0)("nProviderID")
                nPrevPrescriberID = dtRxDetails.Rows(0)("nProviderID")

                For i As Integer = 0 To dtRxDetails.Rows.Count - 1
                    nCurrentPrescriberID = dtRxDetails.Rows(i)("nProviderID")

                    If i = 0 Then
                        WriteARD(dtRxDetails, i, strWriter)
                        cnt += 1
                    Else
                        If nCurrentPrescriberID = nPrevPrescriberID Then
                            'write ARD*************************************************************
                            WriteARD(dtRxDetails, i, strWriter)
                            cnt += 1
                        Else
                            nPrevPrescriberID = nCurrentPrescriberID
                            WriteARA(dtRxDetails, i - 1, strWriter)
                            WriteARD(dtRxDetails, i, strWriter)
                            cnt += 2
                        End If
                    End If
                Next

                WriteARA(dtRxDetails, dtRxDetails.Rows.Count - 1, strWriter)
                cnt += 1
            End If

            '#########################################################################################



            'write report trailer fields
            'objEARReportTrailer.RecordType
            strWriter.Write("RTR")
            strWriter.Write("|")

            'objEARReportTrailer.TotalRecords  (HardCoded)
            '   strWriter.Write(((dtRxDetails.Rows.Count + 1)).ToString())
            strWriter.Write(cnt.ToString())
            'strWriter.Write("|")

            strWriter.WriteLine()

            'write file trailer fields
            'objEARFileTrailer.RecordType()
            strWriter.Write("TRL")
            strWriter.Write("|")

            'objEARFileTrailer.TotalRecords (HardCoded)
            strWriter.Write((cnt + 2).ToString())
            'strWriter.Write("|")

            strWriter.WriteLine()

            'close the streamwriter
            strWriter.Close()
        Catch IOex As IOException
            If Not IsNothing(strWriter) Then
                strWriter.Close()
            End If
            _result = False
            Throw IOex

        Catch ex As Exception
            _result = False
            If Not IsNothing(strWriter) Then
                strWriter.Close()
            End If

            Throw ex
        Finally
        End Try

        Return _result
    End Function

    '\\ Read file header HDR
    Public Shared Function WritefileheaderHDR(ByVal strWriter As StreamWriter)
        Try

            '#########################################################################################
            'write the file header fields
            '#########################################################################################

            strWriter.Write("HDR")
            strWriter.Write("|")

            'Version
            strWriter.Write("10")
            strWriter.Write("|")

            '   objEARFileHeader.SenderID()
            strWriter.Write(mdlGeneral.eRxHubParticipantId)
            strWriter.Write("|")

            'objEARFileHeader.SenderParticipantPwd
            strWriter.Write(mdlGeneral.eRxHubPassword)
            strWriter.Write("|")


            'objEARFileHeader.ReceiverID
            strWriter.Write("RXHUB")
            strWriter.Write("|")

            '<<<<<<<<<<<<<<<<<<Generate the TransmissionControlNumber >>>>>>>>>>>>>>>>
            '<<based  on current datetime
            '<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            Dim TransmissionControlNumber As String = ""
            TransmissionControlNumber = Guid.NewGuid().ToString()
            TransmissionControlNumber = Mid(TransmissionControlNumber.ToString, 1, 8)


            'objEARFileHeader.SourceName
            ' strWriter.Write("gloEMR5.0")
            Dim dtClinicName As DataTable = GetClinicName()
            Dim sClinicName As String = "gloClinic"
            If Not IsNothing(dtClinicName) Then
                If dtClinicName.Rows.Count > 0 Then
                    sClinicName = dtClinicName.Rows(0)("sClinicName")
                End If
            End If
            ''as per discussion we are going to send the Clinic Name added with unique identifier as prefix
            sClinicName = TransmissionControlNumber & sClinicName
            If sClinicName.Length > 35 Then ''''as per the EAR doc clinic name should be between 1-35 characters only
                sClinicName = sClinicName.Substring(0, 34)
            End If
            strWriter.Write(sClinicName) ''"gloStream9"
            strWriter.Write("|")

            strWriter.Write(TransmissionControlNumber)
            strWriter.Write("|")

            'objEARFileHeader.TransmissionDate()
            strWriter.Write(Now.ToString("yyyyMMdd"))
            strWriter.Write("|")

            'objEARFileHeader.TransmissionTime
            Dim _strTime As String
            _strTime = Now.ToString("hhmmss") + "00"
            strWriter.Write(_strTime)
            strWriter.Write("|")

            'objEARFileHeader.TransmissionFileType
            'EAR
            strWriter.Write(mdlGeneral.sEARTransmissionFileType)
            strWriter.Write("|")

            'Transmission Action -- set it to blank
            strWriter.Write("")
            strWriter.Write("|")

            'Extract Date
            '<<<<<<<<<<<<<<<<<<<<<<Needs to be Verified>>>>>>>>>>>>>>>>>>>>>
            '<<<<<<<<<<<<<<<<<<<<<<Needs to be Verified>>>>>>>>>>>>>>>>>>>>>
            strWriter.Write(Now.ToString("yyyyMMdd"))
            strWriter.Write("|")

            'File Type
            'T for test and P for Production
            strWriter.Write(mdlGeneral.sEARFileType)

            'insert a new line character
            strWriter.WriteLine() '("\\n")

        Catch ex As Exception
            mdlGeneral.UpdateLog("WritefileheaderHDR() - " & ex.ToString)
        End Try
        Return False
    End Function

    '\\New added 
    Public Shared Function NEWGenerateEARTextFile_OriginalBeforeDrewImplementation(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal gstrEARDownloadDirectory As String, ByVal rptTransmission As String) As String

        'use eARFileName instead of sEARFileName
        Dim dtExtractDate As DateTime
        '\\eARFileNAme = RetrieveFileName(dtExtractDate, gstrEARDownloadDirectory)

        ''\\Dim strWriter As New StreamWriter(eARFileNAme, False)
        Dim strWriter As StreamWriter = Nothing
        Dim objRxData As New gloEAR.gloRxData(gstrDatabaseConnectionString)
        Dim dtRxDetails As New DataTable


        Dim strCombineIDPswd As String = ""
        ' Dim ary() As String

        'Dim objeRxCredentials As New clsRxCredentials

        mdlGeneral.sEARTransmissionAction = rptTransmission '\\get New or retransmission


        '---------------report start date & end date

        Dim reportStartdate As String = dtFromDate.Date.ToString("yyyyMMdd")
        Dim reportEnddate As String = dtToDate.Date.ToString("yyyyMMdd")

        Dim days As Long = DateDiff(DateInterval.Day, dtFromDate, dtToDate)
        Dim k As Integer = DateDiff(DateInterval.Weekday, dtFromDate, dtToDate)
        Dim sunday As DateTime = Nothing
        Dim saturday As DateTime = Nothing

        'If days > 6 Then
        '    Dim dtTempfromdate As DateTime = dtToDate.AddDays(-6)  'find sunday of that week ie. start date for report
        '    reportStartdate = dtTempfromdate.Date.ToString("yyyyMMdd")
        'End If

        Dim cnt As Integer = 0  'for TRL count
        Try
            ''eRxSenderID = objeRxCredentials.SenderID
            ''eRxSenderParticipantPassword = objeRxCredentials.SenderParticipantPassword
            ''objeRxCredentials.Dispose()

            'get participantID & password from [set in glo admin] setting table
            'strCombineIDPswd = getIDPWRD()

            'If strCombineIDPswd.Length > 0 Then
            '    ary = strCombineIDPswd.ToString.Split("|")
            '    If ary.Length > 0 Then

            'mdlGeneral.eRxHubParticipantId = ary(0).ToString
            'mdlGeneral.eRxHubPassword = ary(1).ToString

            If mdlGeneral.eRxHubParticipantId <> "" And mdlGeneral.eRxHubPassword <> "" Then
                eARFileNAme = RetrieveFileName(dtExtractDate, gstrEARDownloadDirectory)

                strWriter = New StreamWriter(eARFileNAme, False)

            Else
                mdlGeneral.UpdateLog("Error : gettting rxhub participantID or rxhub password as blank valuedata")
                Return ""
            End If
            '    End If
            'End If
            '--------------------------------------------------
            ''#########################################################################################
            ''write the file header fields
            ''#########################################################################################

            'strWriter.Write("HDR")
            'strWriter.Write("|")

            ''Version
            'strWriter.Write("10")
            'strWriter.Write("|")

            ''   objEARFileHeader.SenderID()
            'strWriter.Write(eRxSenderID)
            'strWriter.Write("|")

            ''objEARFileHeader.SenderParticipantPwd
            'strWriter.Write(eRxSenderParticipantPassword)
            'strWriter.Write("|")

            ''objEARFileHeader.ReceiverID
            'strWriter.Write("RXHUB")
            'strWriter.Write("|")

            ''objEARFileHeader.SourceName
            '' strWriter.Write("gloEMR5.0")
            'strWriter.Write("gloStream9")
            'strWriter.Write("|")

            ''objEARFileHeader.TransmissionControlNumber

            ''<<<<<<<<<<<<<<<<<<Generate the TransmissionControlNumber >>>>>>>>>>>>>>>>
            ''<<based  on current datetime
            ''<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            'Dim TransmissionControlNumber As String = ""
            'TransmissionControlNumber = Guid.NewGuid().ToString()
            'TransmissionControlNumber = Mid(TransmissionControlNumber.ToString, 1, 8)
            'strWriter.Write(TransmissionControlNumber)
            'strWriter.Write("|")

            ''objEARFileHeader.TransmissionDate()
            'strWriter.Write(Now.ToString("yyyyMMdd"))
            'strWriter.Write("|")

            ''objEARFileHeader.TransmissionTime
            'Dim _strTime As String
            '_strTime = Now.ToString("hhmmss") + "00"
            'strWriter.Write(_strTime)
            'strWriter.Write("|")

            ''objEARFileHeader.TransmissionFileType
            ''EAR
            'strWriter.Write(sEARTransmissionFileType)
            'strWriter.Write("|")

            ''Transmission Action -- set it to blank
            'strWriter.Write("")
            'strWriter.Write("|")

            ''Extract Date
            ''<<<<<<<<<<<<<<<<<<<<<<Needs to be Verified>>>>>>>>>>>>>>>>>>>>>
            ''<<<<<<<<<<<<<<<<<<<<<<Needs to be Verified>>>>>>>>>>>>>>>>>>>>>
            'strWriter.Write(Now.ToString("yyyyMMdd"))
            'strWriter.Write("|")

            ''File Type
            ''T for test and P for Production
            'strWriter.Write(sEARFileType)

            ''insert a new line character
            'strWriter.WriteLine() '("\\n")
            '--------------------------------------------------

            '\\ write HDR
            WritefileheaderHDR(strWriter)




            If k >= 0 Then  'total week start from 0,1,2,3....
                For j As Integer = 0 To k

                    If j = 0 Then

                        ''sunday = dtFromDate
                        ''saturday = dtFromDate.AddDays(6)

                        saturday = dtToDate
                        sunday = dtToDate.AddDays(-6)


                        Dim cnt1 As Integer = writeRHD_RTR(strWriter, sunday, saturday)

                        cnt = cnt + cnt1   '(RHD + RTR)

                    Else

                        ''sunday = saturday.AddDays(1)
                        ''saturday = sunday.AddDays(6)

                        saturday = saturday.AddDays(-7)
                        sunday = saturday.AddDays(-6)

                        Dim cnt2 As Integer = writeRHD_RTR(strWriter, sunday, saturday)

                        cnt = cnt + cnt2

                        'reportStartdate = sunday.Date.ToString("yyyyMMdd") 'sunday
                        'reportEnddate = saturday.AddDays(6).Date.ToString("yyyyMMdd") 'saturday
                    End If

                Next
            End If

            '--------------------------------------------------
            ''#########################################################################################
            ''write the report header fields
            ''#########################################################################################

            ''objEARReportHeader.RecordType
            'strWriter.Write("RHD")
            'strWriter.Write("|")

            ''objEARReportHeader.ReportStartDate
            ''strWriter.Write(sEARStartDate)
            ''should be Sunday
            'Dim sReportStartDate As String = ""
            'sReportStartDate = reportStartdate '\\ commented on 20090812 dtFromDate.Date.ToString("yyyyMMdd")
            'strWriter.Write(sReportStartDate)
            'strWriter.Write("|")

            ''objEARReportHeader.ReportEndDate
            ''should be saturday
            'Dim sReportEndDate As String = ""
            'sReportEndDate = reportEnddate '\\ commented on 20090812 dtToDate.Date.ToString("yyyyMMdd")
            ''strWriter.Write(sEAREndDate)
            'strWriter.Write(sReportEndDate)
            'strWriter.Write("|")

            ''objEARReportHeader.TransmissionAction
            ''N for new and R for retransmission
            ''   sEARTransmissionAction = "N"
            'strWriter.Write(sEARTransmissionAction)
            ''strWriter.Write("|")
            'strWriter.WriteLine()

            ''#########################################################################################
            ''write the report aggregate fields and report detail fields
            ''#########################################################################################


            'Dim nCurrentPrescriberID As Long = 0
            'Dim nPrevPrescriberID As Long = 0


            ''  dtRxDetails = objRxData.GetEARReportAggregateDetails()
            'dtRxDetails = objRxData.GetEARRxLevelRecords(dtFromDate, dtToDate)

            'Dim cnt As Integer = 0

            'If dtRxDetails.Rows.Count > 0 Then
            '    nCurrentPrescriberID = dtRxDetails.Rows(0)("nProviderID")
            '    nPrevPrescriberID = dtRxDetails.Rows(0)("nProviderID")

            '    For i As Integer = 0 To dtRxDetails.Rows.Count - 1
            '        nCurrentPrescriberID = dtRxDetails.Rows(i)("nProviderID")

            '        If i = 0 Then
            '            WriteARD(dtRxDetails, i, strWriter)
            '            cnt += 1
            '        Else
            '            If nCurrentPrescriberID = nPrevPrescriberID Then
            '                'write ARD*************************************************************
            '                WriteARD(dtRxDetails, i, strWriter)
            '                cnt += 1
            '            Else
            '                nPrevPrescriberID = nCurrentPrescriberID
            '                WriteARA(dtRxDetails, i - 1, strWriter)
            '                WriteARD(dtRxDetails, i, strWriter)
            '                cnt += 2
            '            End If
            '        End If
            '    Next

            '    WriteARA(dtRxDetails, dtRxDetails.Rows.Count - 1, strWriter)
            '    cnt += 1
            'End If

            ''#########################################################################################

            ''write report trailer fields
            ''objEARReportTrailer.RecordType
            'strWriter.Write("RTR")
            'strWriter.Write("|")

            ''objEARReportTrailer.TotalRecords  (HardCoded)
            ''   strWriter.Write(((dtRxDetails.Rows.Count + 1)).ToString())
            'strWriter.Write(cnt.ToString())
            ''strWriter.Write("|")

            'strWriter.WriteLine()

            'write file trailer fields
            'objEARFileTrailer.RecordType()
            '--------------------------------------------------


            '\\write file trailer = TRL

            strWriter.Write("TRL")
            strWriter.Write("|")

            'objEARFileTrailer.TotalRecords (HardCoded)
            strWriter.Write((cnt).ToString())   'cnt + 2
            'strWriter.Write("|")

            strWriter.WriteLine()

            'close the streamwriter
            strWriter.Close()

            Return eARFileNAme
        Catch IOex As IOException
            mdlGeneral.UpdateLog("NEWGenerateEARTextFile() - " & IOex.ToString)
            If Not IsNothing(strWriter) Then
                strWriter.Close()
            End If
            Return ""
            Throw IOex

        Catch ex As Exception
            mdlGeneral.UpdateLog("NEWGenerateEARTextFile() - " & ex.ToString)
            Return ""
            If Not IsNothing(strWriter) Then
                strWriter.Close()
            End If

            Throw ex
        Finally

        End Try


    End Function

    Public Shared Function NEWGenerateEARTextFile(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal gstrEARDownloadDirectory As String, ByVal rptTransmission As String) As String

        'use eARFileName instead of sEARFileName
        Dim dtExtractDate As DateTime
        '\\eARFileNAme = RetrieveFileName(dtExtractDate, gstrEARDownloadDirectory)

        ''\\Dim strWriter As New StreamWriter(eARFileNAme, False)
        Dim strWriter As StreamWriter = Nothing
        Dim objRxData As New gloEAR.gloRxData(gstrDatabaseConnectionString)
        Dim dtRxDetails As New DataTable


        Dim strCombineIDPswd As String = ""
        ' Dim ary() As String

        'Dim objeRxCredentials As New clsRxCredentials

        mdlGeneral.sEARTransmissionAction = rptTransmission '\\get New or retransmission


        '---------------report start date & end date

        Dim reportStartdate As String = dtFromDate.Date.ToString("yyyyMMdd")
        Dim reportEnddate As String = dtToDate.Date.ToString("yyyyMMdd")

        Dim days As Long = DateDiff(DateInterval.Day, dtFromDate, dtToDate)
        Dim k As Integer = DateDiff(DateInterval.Weekday, dtFromDate, dtToDate)
        Dim sunday As DateTime = Nothing
        Dim saturday As DateTime = Nothing

        'If days > 6 Then
        '    Dim dtTempfromdate As DateTime = dtToDate.AddDays(-6)  'find sunday of that week ie. start date for report
        '    reportStartdate = dtTempfromdate.Date.ToString("yyyyMMdd")
        'End If

        Dim cnt As Integer = 0  'for TRL count
        Try
            ''eRxSenderID = objeRxCredentials.SenderID
            ''eRxSenderParticipantPassword = objeRxCredentials.SenderParticipantPassword
            ''objeRxCredentials.Dispose()

            'get participantID & password from [set in glo admin] setting table
            'strCombineIDPswd = getIDPWRD()

            'If strCombineIDPswd.Length > 0 Then
            '    ary = strCombineIDPswd.ToString.Split("|")
            '    If ary.Length > 0 Then

            'mdlGeneral.eRxHubParticipantId = ary(0).ToString
            'mdlGeneral.eRxHubPassword = ary(1).ToString

            If mdlGeneral.eRxHubParticipantId <> "" And mdlGeneral.eRxHubPassword <> "" Then
                eARFileNAme = RetrieveFileName(dtExtractDate, gstrEARDownloadDirectory)

                strWriter = New StreamWriter(eARFileNAme, False)

            Else
                mdlGeneral.UpdateLog("Error : gettting rxhub participantID or rxhub password as blank valuedata")
                Return ""
            End If
            '    End If
            'End If
            '--------------------------------------------------
            ''#########################################################################################
            ''write the file header fields
            ''#########################################################################################

            'strWriter.Write("HDR")
            'strWriter.Write("|")

            ''Version
            'strWriter.Write("10")
            'strWriter.Write("|")

            ''   objEARFileHeader.SenderID()
            'strWriter.Write(eRxSenderID)
            'strWriter.Write("|")

            ''objEARFileHeader.SenderParticipantPwd
            'strWriter.Write(eRxSenderParticipantPassword)
            'strWriter.Write("|")

            ''objEARFileHeader.ReceiverID
            'strWriter.Write("RXHUB")
            'strWriter.Write("|")

            ''objEARFileHeader.SourceName
            '' strWriter.Write("gloEMR5.0")
            'strWriter.Write("gloStream9")
            'strWriter.Write("|")

            ''objEARFileHeader.TransmissionControlNumber

            ''<<<<<<<<<<<<<<<<<<Generate the TransmissionControlNumber >>>>>>>>>>>>>>>>
            ''<<based  on current datetime
            ''<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            'Dim TransmissionControlNumber As String = ""
            'TransmissionControlNumber = Guid.NewGuid().ToString()
            'TransmissionControlNumber = Mid(TransmissionControlNumber.ToString, 1, 8)
            'strWriter.Write(TransmissionControlNumber)
            'strWriter.Write("|")

            ''objEARFileHeader.TransmissionDate()
            'strWriter.Write(Now.ToString("yyyyMMdd"))
            'strWriter.Write("|")

            ''objEARFileHeader.TransmissionTime
            'Dim _strTime As String
            '_strTime = Now.ToString("hhmmss") + "00"
            'strWriter.Write(_strTime)
            'strWriter.Write("|")

            ''objEARFileHeader.TransmissionFileType
            ''EAR
            'strWriter.Write(sEARTransmissionFileType)
            'strWriter.Write("|")

            ''Transmission Action -- set it to blank
            'strWriter.Write("")
            'strWriter.Write("|")

            ''Extract Date
            ''<<<<<<<<<<<<<<<<<<<<<<Needs to be Verified>>>>>>>>>>>>>>>>>>>>>
            ''<<<<<<<<<<<<<<<<<<<<<<Needs to be Verified>>>>>>>>>>>>>>>>>>>>>
            'strWriter.Write(Now.ToString("yyyyMMdd"))
            'strWriter.Write("|")

            ''File Type
            ''T for test and P for Production
            'strWriter.Write(sEARFileType)

            ''insert a new line character
            'strWriter.WriteLine() '("\\n")
            '--------------------------------------------------

            '\\ write HDR
            '''''WritefileheaderHDR(strWriter)'''''commented for new logic




            If k >= 0 Then  'total week start from 0,1,2,3....
                For j As Integer = 0 To k

                    If j = 0 Then

                        ''sunday = dtFromDate
                        ''saturday = dtFromDate.AddDays(6)

                        saturday = dtToDate
                        sunday = dtToDate.AddDays(-6)


                        Dim cnt1 As Integer = writeRHD_RTR(strWriter, sunday, saturday)

                        ''''if the returned value is 0 that means eithere there was no records/data to creat the file for that day or there must be an exception
                        If cnt1 = 0 Then
                            strWriter.Close()
                            ''delete the file because it only of 0KB and it contains empty data, changed discussed with OM on 14 May 2011
                            If eARFileNAme <> "" Then
                                File.Delete(eARFileNAme)
                            End If
                            Return ""
                        End If

                        cnt = cnt + cnt1   '(RHD + RTR)

                    Else

                        ''sunday = saturday.AddDays(1)
                        ''saturday = sunday.AddDays(6)

                        saturday = saturday.AddDays(-7)
                        sunday = saturday.AddDays(-6)

                        Dim cnt2 As Integer = writeRHD_RTR(strWriter, sunday, saturday)

                        ''''if the returned value is 0 that means eithere there was no records/data to creat the file for that day or there must be an exception
                        If cnt2 = 0 Then
                            strWriter.Close()
                            ''delete the file because it only of 0KB and it contains empty data, changed discussed with OM on 14 May 2011
                            If eARFileNAme <> "" Then
                                File.Delete(eARFileNAme)
                            End If
                            Return ""
                        End If

                        cnt = cnt + cnt2

                        'reportStartdate = sunday.Date.ToString("yyyyMMdd") 'sunday
                        'reportEnddate = saturday.AddDays(6).Date.ToString("yyyyMMdd") 'saturday
                    End If

                Next
            End If

            '--------------------------------------------------
            ''#########################################################################################
            ''write the report header fields
            ''#########################################################################################

            ''objEARReportHeader.RecordType
            'strWriter.Write("RHD")
            'strWriter.Write("|")

            ''objEARReportHeader.ReportStartDate
            ''strWriter.Write(sEARStartDate)
            ''should be Sunday
            'Dim sReportStartDate As String = ""
            'sReportStartDate = reportStartdate '\\ commented on 20090812 dtFromDate.Date.ToString("yyyyMMdd")
            'strWriter.Write(sReportStartDate)
            'strWriter.Write("|")

            ''objEARReportHeader.ReportEndDate
            ''should be saturday
            'Dim sReportEndDate As String = ""
            'sReportEndDate = reportEnddate '\\ commented on 20090812 dtToDate.Date.ToString("yyyyMMdd")
            ''strWriter.Write(sEAREndDate)
            'strWriter.Write(sReportEndDate)
            'strWriter.Write("|")

            ''objEARReportHeader.TransmissionAction
            ''N for new and R for retransmission
            ''   sEARTransmissionAction = "N"
            'strWriter.Write(sEARTransmissionAction)
            ''strWriter.Write("|")
            'strWriter.WriteLine()

            ''#########################################################################################
            ''write the report aggregate fields and report detail fields
            ''#########################################################################################


            'Dim nCurrentPrescriberID As Long = 0
            'Dim nPrevPrescriberID As Long = 0


            ''  dtRxDetails = objRxData.GetEARReportAggregateDetails()
            'dtRxDetails = objRxData.GetEARRxLevelRecords(dtFromDate, dtToDate)

            'Dim cnt As Integer = 0

            'If dtRxDetails.Rows.Count > 0 Then
            '    nCurrentPrescriberID = dtRxDetails.Rows(0)("nProviderID")
            '    nPrevPrescriberID = dtRxDetails.Rows(0)("nProviderID")

            '    For i As Integer = 0 To dtRxDetails.Rows.Count - 1
            '        nCurrentPrescriberID = dtRxDetails.Rows(i)("nProviderID")

            '        If i = 0 Then
            '            WriteARD(dtRxDetails, i, strWriter)
            '            cnt += 1
            '        Else
            '            If nCurrentPrescriberID = nPrevPrescriberID Then
            '                'write ARD*************************************************************
            '                WriteARD(dtRxDetails, i, strWriter)
            '                cnt += 1
            '            Else
            '                nPrevPrescriberID = nCurrentPrescriberID
            '                WriteARA(dtRxDetails, i - 1, strWriter)
            '                WriteARD(dtRxDetails, i, strWriter)
            '                cnt += 2
            '            End If
            '        End If
            '    Next

            '    WriteARA(dtRxDetails, dtRxDetails.Rows.Count - 1, strWriter)
            '    cnt += 1
            'End If

            ''#########################################################################################

            ''write report trailer fields
            ''objEARReportTrailer.RecordType
            'strWriter.Write("RTR")
            'strWriter.Write("|")

            ''objEARReportTrailer.TotalRecords  (HardCoded)
            ''   strWriter.Write(((dtRxDetails.Rows.Count + 1)).ToString())
            'strWriter.Write(cnt.ToString())
            ''strWriter.Write("|")

            'strWriter.WriteLine()

            'write file trailer fields
            'objEARFileTrailer.RecordType()
            '--------------------------------------------------


            '\\write file trailer = TRL

            'strWriter.Write("TRL")
            'strWriter.Write("|")

            'objEARFileTrailer.TotalRecords (HardCoded)
            'strWriter.Write((cnt).ToString())   'cnt + 2
            'strWriter.Write("|")

            'strWriter.WriteLine()

            'close the streamwriter
            strWriter.Close()

            Return eARFileNAme
        Catch IOex As IOException
            mdlGeneral.UpdateLog("NEWGenerateEARTextFile() - " & IOex.ToString)
            If Not IsNothing(strWriter) Then
                strWriter.Close()
            End If
            ''delete the file because it only of 0KB and it contains empty data, changed discussed with OM on 14 May 2011
            If eARFileNAme <> "" Then
                File.Delete(eARFileNAme)
            End If

            Return ""
            Throw IOex

        Catch ex As Exception
            mdlGeneral.UpdateLog("NEWGenerateEARTextFile() - " & ex.ToString)
            If Not IsNothing(strWriter) Then
                strWriter.Close()
            End If
            ''delete the file because it only of 0KB and it contains empty data, changed discussed with OM on 14 May 2011
            If eARFileNAme <> "" Then
                File.Delete(eARFileNAme)
            End If
            Return ""
            Throw ex
        Finally

        End Try


    End Function

    '\\ write Report file header RHD to RTR [also = ARD & ARA]
    Private Shared Function writeRHD_RTR_OriginalBeforeDrewImplementation(ByVal strWriter As StreamWriter, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime) As Integer
        Dim dtRxDetails As New DataTable
        Dim objRxData As New gloEAR.gloRxData(gstrDatabaseConnectionString)

        Dim reportStartdate As String = dtFromDate.Date.ToString("yyyyMMdd")
        Dim reportEnddate As String = dtToDate.Date.ToString("yyyyMMdd")

        Try
            '#########################################################################################
            'write the report header fields
            '#########################################################################################

            'objEARReportHeader.RecordType
            strWriter.Write("RHD")
            strWriter.Write("|")

            'objEARReportHeader.ReportStartDate
            'strWriter.Write(sEARStartDate)
            'should be Sunday
            Dim sReportStartDate As String = ""
            sReportStartDate = reportStartdate '\\ commented on 20090812 dtFromDate.Date.ToString("yyyyMMdd")
            strWriter.Write(sReportStartDate)
            strWriter.Write("|")

            'objEARReportHeader.ReportEndDate
            'should be saturday
            Dim sReportEndDate As String = ""
            sReportEndDate = reportEnddate '\\ commented on 20090812 dtToDate.Date.ToString("yyyyMMdd")
            'strWriter.Write(sEAREndDate)
            strWriter.Write(sReportEndDate)
            strWriter.Write("|")

            'objEARReportHeader.TransmissionAction
            'N for new and R for retransmission
            '   sEARTransmissionAction = "N"
            strWriter.Write(mdlGeneral.sEARTransmissionAction)
            'strWriter.Write("|")
            strWriter.WriteLine()

            '#########################################################################################
            'write the report aggregate fields and report detail fields
            '#########################################################################################


            Dim nCurrentPrescriberID As Long = 0
            Dim nPrevPrescriberID As Long = 0


            '  dtRxDetails = objRxData.GetEARReportAggregateDetails()
            dtRxDetails = objRxData.GetEARRxLevelRecords(dtFromDate, dtToDate)

            Dim cnt As Integer = 0

            If dtRxDetails.Rows.Count > 0 Then
                nCurrentPrescriberID = dtRxDetails.Rows(0)("nProviderID")
                nPrevPrescriberID = dtRxDetails.Rows(0)("nProviderID")

                For i As Integer = 0 To dtRxDetails.Rows.Count - 1
                    nCurrentPrescriberID = dtRxDetails.Rows(i)("nProviderID")

                    If i = 0 Then
                        WriteARD(dtRxDetails, i, strWriter)
                        cnt += 1
                    Else
                        If nCurrentPrescriberID = nPrevPrescriberID Then
                            'write ARD*************************************************************
                            WriteARD(dtRxDetails, i, strWriter) '<<** ARD
                            cnt += 1
                        Else
                            nPrevPrescriberID = nCurrentPrescriberID
                            WriteARA(dtRxDetails, i - 1, strWriter) '<<ARA>>
                            WriteARD(dtRxDetails, i, strWriter) '<<** ARD
                            cnt += 2
                        End If
                    End If
                Next

                WriteARA(dtRxDetails, dtRxDetails.Rows.Count - 1, strWriter) '<<ARA>>
                cnt += 1
            End If

            '#########################################################################################

            'write report trailer fields
            'objEARReportTrailer.RecordType
            strWriter.Write("RTR")
            strWriter.Write("|")

            'objEARReportTrailer.TotalRecords  (HardCoded)
            '   strWriter.Write(((dtRxDetails.Rows.Count + 1)).ToString())
            strWriter.Write(cnt.ToString())
            'strWriter.Write("|")

            strWriter.WriteLine()

            ''write file trailer fields
            ''objEARFileTrailer.RecordType()
            'strWriter.Write("TRL")
            'strWriter.Write("|")

            ''objEARFileTrailer.TotalRecords (HardCoded)
            'strWriter.Write((cnt + 2).ToString())
            ''strWriter.Write("|")

            'strWriter.WriteLine()

            ''close the streamwriter
            'strWriter.Close()

            Return cnt + 2
        Catch ex As Exception
            mdlGeneral.UpdateLog("writeRHD_RTR() - " & ex.ToString)
            Throw ex
        End Try
    End Function

    '\\ write Report file header RHD to RTR [also = ARD & ARA]
    Private Shared Function writeRHD_RTR(ByVal strWriter As StreamWriter, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime) As Integer
        Dim dtRxDetails As New DataTable
        Dim objRxData As New gloEAR.gloRxData(gstrDatabaseConnectionString)

        Dim reportStartdate As String = dtFromDate.Date.ToString("yyyyMMdd")
        Dim reportEnddate As String = dtToDate.Date.ToString("yyyyMMdd")

        Try
            '#########################################################################################
            'write the report header fields
            '#########################################################################################

            'objEARReportHeader.RecordType
            'strWriter.Write("RHD")
            'strWriter.Write("|")

            'objEARReportHeader.ReportStartDate
            'strWriter.Write(sEARStartDate)
            'should be Sunday
            'Dim sReportStartDate As String = ""
            'sReportStartDate = reportStartdate '\\ commented on 20090812 dtFromDate.Date.ToString("yyyyMMdd")
            'strWriter.Write(sReportStartDate)
            'strWriter.Write("|")

            'objEARReportHeader.ReportEndDate
            'should be saturday
            'Dim sReportEndDate As String = ""
            'sReportEndDate = reportEnddate '\\ commented on 20090812 dtToDate.Date.ToString("yyyyMMdd")
            ''strWriter.Write(sEAREndDate)
            'strWriter.Write(sReportEndDate)
            'strWriter.Write("|")

            'objEARReportHeader.TransmissionAction
            'N for new and R for retransmission
            '   sEARTransmissionAction = "N"
            'strWriter.Write(mdlGeneral.sEARTransmissionAction)
            'strWriter.Write("|")
            'strWriter.WriteLine()

            '#########################################################################################
            'write the report aggregate fields and report detail fields
            '#########################################################################################


            Dim nCurrentPrescriberID As Long = 0
            Dim nPrevPrescriberID As Long = 0


            '  dtRxDetails = objRxData.GetEARReportAggregateDetails()


            ''dtRxDetails = objRxData.GetEARRxLevelRecords(dtFromDate, dtToDate)
            ''since we are going to generate data for every day, pass the current date as parameter
            dtRxDetails = objRxData.GetEARRxLevelRecords_Today(Now) ''''from date contains todays date value

            Dim cnt As Integer = 0

            If dtRxDetails.Rows.Count > 0 Then
                nCurrentPrescriberID = dtRxDetails.Rows(0)("nProviderID")
                nPrevPrescriberID = dtRxDetails.Rows(0)("nProviderID")

                For i As Integer = 0 To dtRxDetails.Rows.Count - 1
                    WriteARD(dtRxDetails, i, strWriter)

                    'nCurrentPrescriberID = dtRxDetails.Rows(i)("nProviderID")

                    'If i = 0 Then
                    '    WriteARD(dtRxDetails, i, strWriter)
                    '    cnt += 1
                    'Else
                    '    If nCurrentPrescriberID = nPrevPrescriberID Then
                    '        'write ARD*************************************************************
                    '        WriteARD(dtRxDetails, i, strWriter) '<<** ARD
                    '        cnt += 1
                    '    Else
                    '        nPrevPrescriberID = nCurrentPrescriberID
                    '        WriteARA(dtRxDetails, i - 1, strWriter) '<<ARA>>
                    '        WriteARD(dtRxDetails, i, strWriter) '<<** ARD
                    '        cnt += 2
                    '    End If
                    'End If
                Next

                'WriteARA(dtRxDetails, dtRxDetails.Rows.Count - 1, strWriter) '<<ARA>>
                'cnt += 1
            Else
                Return 0 ''''since there is no record count dont create the file with any data
            End If

            '#########################################################################################

            'write report trailer fields
            'objEARReportTrailer.RecordType
            'strWriter.Write("RTR")
            'strWriter.Write("|")

            'objEARReportTrailer.TotalRecords  (HardCoded)
            '   strWriter.Write(((dtRxDetails.Rows.Count + 1)).ToString())
            'strWriter.Write(cnt.ToString())
            'strWriter.Write("|")

            'strWriter.WriteLine()

            ''write file trailer fields
            ''objEARFileTrailer.RecordType()
            'strWriter.Write("TRL")
            'strWriter.Write("|")

            ''objEARFileTrailer.TotalRecords (HardCoded)
            'strWriter.Write((cnt + 2).ToString())
            ''strWriter.Write("|")

            'strWriter.WriteLine()

            ''close the streamwriter
            'strWriter.Close()

            Return cnt + 2
        Catch ex As Exception
            mdlGeneral.UpdateLog("writeRHD_RTR() - " & ex.ToString)
            Return 0 '''''if  error then return 0 so that file will not get generated
            Throw ex
        End Try
    End Function

    Private Shared Sub WriteARD(ByVal dtRxDetails As DataTable, ByVal i As Integer, ByVal strWriter As StreamWriter)
        Try
            '#########################################################################################
            'write the report detail fields
            '#########################################################################################

            'RecordType= "ARD"  'col-0
            strWriter.Write("ARD")
            strWriter.Write("|")

            'SourceParticipantID    'col-1
            strWriter.Write(mdlGeneral.eRxHubParticipantId)
            strWriter.Write("|")

            'DestinationParticipantId   'col-2      
            'strWriter.Write("RXHUB")
            'strWriter.Write("|")
            If Not IsNothing(dtRxDetails.Rows(i)("PBM_PayerParticipantID")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PBM_PayerParticipantID"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")


            'PrescriberDEA      'col-3
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberDEA")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberDEA"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'PrescriberNPI      'col-4
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberNPI")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberNPI"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'PrescriberStateLicenceNumber   'col-5 
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberStateLicenceNumber")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberStateLicenceNumber").ToString.Trim)
            Else
                strWriter.Write("")
            End If

            strWriter.Write("|")

            'PrescriberState        'col-6
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberState")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberState"))
            Else
                strWriter.Write("")
            End If

            strWriter.Write("|")

            ' PrescriberZIPCODE '7
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberZIPCODE")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberZIPCODE"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' PrescriberConfidentialIdentifier
            '<<<<<<<<<<<<<<<<<<Needs to be confirmed>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            '<<<<<<<<<<<<<<<<<Temporarily use Provider NPI>>>>>>>>>>>>>>>>>>>>>>
            'sPrescriberConfidentialIdentifier = 
            strWriter.Write(sPrescriberConfidentialIdentifier)
            strWriter.Write("|")


            'If Not IsNothing(dtRxDetails.Rows(i)("PrescriptionDate")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("PrescriptionDate"))
            'Else
            '    strWriter.Write("")
            'End If

            ' PrescriptionDate 

            If Not IsNothing(dtRxDetails.Rows(i)("TransactionDate")) Then
                Dim tempdate As DateTime = dtRxDetails.Rows(i)("TransactionDate")
                strWriter.Write(tempdate.ToString("yyyyMMdd"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")


            ' PrescriberFirstName
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberFirstName")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberFirstName"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' PrescriberLastName
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberLastName")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberLastName"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'HealthPlanID 
            If Not IsNothing(dtRxDetails.Rows(i)("HealthPlanID")) Then
                strWriter.Write(dtRxDetails.Rows(i)("HealthPlanID"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'HealthPlanGroupID 
            If Not IsNothing(dtRxDetails.Rows(i)("HealthPlanGroupID")) Then
                strWriter.Write(dtRxDetails.Rows(i)("HealthPlanGroupID"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'PrescriptionID 
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriptionID")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriptionID"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            '  FormularyID 
            If Not IsNothing(dtRxDetails.Rows(i)("FormularyID")) Then
                strWriter.Write(dtRxDetails.Rows(i)("FormularyID"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            '  AlternativesID 
            If Not IsNothing(dtRxDetails.Rows(i)("AlternativesID")) Then
                strWriter.Write(dtRxDetails.Rows(i)("AlternativesID"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'CoverageID 
            If Not IsNothing(dtRxDetails.Rows(i)("CoverageID")) Then
                strWriter.Write(dtRxDetails.Rows(i)("CoverageID"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' CopayID   18
            If Not IsNothing(dtRxDetails.Rows(i)("CopayID")) Then
                strWriter.Write(dtRxDetails.Rows(i)("CopayID"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' FormularyStatus (is required) 19
            If Not IsNothing(dtRxDetails.Rows(i)("FormularyStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("FormularyStatus"))
                '    strWriter.Write("U")
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")


            ' FlatCoPayAmount 
            If Not IsNothing(dtRxDetails.Rows(i)("FlatCoPayAmount")) Then
                strWriter.Write(dtRxDetails.Rows(i)("FlatCoPayAmount"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' PercentCoPayRate 
            If Not IsNothing(dtRxDetails.Rows(i)("PercentCoPayRate")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PercentCoPayRate"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' FirstCoPayTerm
            If Not IsNothing(dtRxDetails.Rows(i)("FirstCoPayTerm")) Then
                strWriter.Write(dtRxDetails.Rows(i)("FirstCoPayTerm"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'CoPayTier
            If Not IsNothing(dtRxDetails.Rows(i)("CoPayTier")) Then
                strWriter.Write(dtRxDetails.Rows(i)("CoPayTier"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' PrescribedAgeLimitCoverageStatus
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedAgeLimitCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedAgeLimitCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' PrescribedDrugExclusionCoverageStatus  
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedDrugExclusionCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedDrugExclusionCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' PrescribedGenderLimitCoverageStatus  
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedGenderLimitCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedGenderLimitCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            '  PrescribedMedicalNecessityCoverageStatus  
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedMedicalNecessityCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedMedicalNecessityCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' PrescribedPriorAuthorizationCoverageStatus
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedPriorAuthorizationCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedPriorAuthorizationCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")


            ' PrescribedQuantityLimitCoverageStatus 
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedQuantityLimitCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedQuantityLimitCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            '  PrescribedDrugSpecificResourceLinkCoverageStatus  
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedDrugSpecificResourceLinkCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedDrugSpecificResourceLinkCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")


            '  PrescribedSummaryLevelResourceLinkCoverageStatus 
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedSummaryLevelResourceLinkCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedSummaryLevelResourceLinkCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' PrescribedStepMedicationCoverageStatus
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedStepMedicationCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedStepMedicationCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' PrescribedStepTherapyCoverageStatus
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedStepTherapyCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedStepTherapyCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")


            '   PrescribedTextMessageCoverageStatus 
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedTextMessageCoverageStatus")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedTextMessageCoverageStatus"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            ' PrescribedNDCCode  
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedNDCCode")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedNDCCode"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            '  PrescribedRXNORMCode  
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedRXNORMCode")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedRXNORMCode"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            '  PrescribedDrugName  
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedDrugName")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedDrugName"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'Public PrescribedDrugStrength As String = ""
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedDrugStrength")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedDrugStrength"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'Public PrescribedDosageForm As String = ""
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedDosageForm")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedDosageForm"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'Public PrescribedQuantity As String = ""
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedQuantity")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedQuantity"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'Public PrescribedDrugType As String = ""
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedDrugType")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedDrugType"))
                'strWriter.Write("G")
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'Public PrescribedRefills As String = ""
            If Not IsNothing(dtRxDetails.Rows(i)("PrescribedRefills")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescribedRefills"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'Public DispenseasWritten As String = ""
            If Not IsNothing(dtRxDetails.Rows(i)("DispenseasWritten")) Then
                strWriter.Write(dtRxDetails.Rows(i)("DispenseasWritten"))
                'strWriter.Write("Y")
            Else
                strWriter.Write("N")
            End If
            strWriter.Write("|")

            'Public MailOrderBenefitUtilized As String = ""
            If Not IsNothing(dtRxDetails.Rows(i)("MailOrderBenefitUtilized")) Then
                strWriter.Write(dtRxDetails.Rows(i)("MailOrderBenefitUtilized"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")


            'Public Initiative As String = ""
            If Not IsNothing(dtRxDetails.Rows(i)("Initiative")) Then
                strWriter.Write(dtRxDetails.Rows(i)("Initiative"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'Public Platform As String = ""
            If Not IsNothing(dtRxDetails.Rows(i)("Platform")) Then
                strWriter.Write(dtRxDetails.Rows(i)("Platform"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'Public PrescriptionDeliveryMethod As String = ""
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriptionDeliveryMethod")) Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriptionDeliveryMethod"))
            Else
                strWriter.Write("")
            End If
            '     strWriter.Write("|")



            '<<<<<<<<<<<<Optional Fileds>>>>>>>>>>>>>>>>>>>
            ''Public DURIndicator As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("DURIndicator")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("DURIndicator"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptInitialFormularyStatus As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptInitialFormularyStatus")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptInitialFormularyStatus"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptFlatCopayAmount As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptFlatCopayAmount")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptFlatCopayAmount"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptCopayRate As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptCopayRate")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptCopayRate"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptFirstCopayTerm As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptFirstCopayTerm")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptFirstCopayTerm"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptCopayTier As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptCopayTier")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptCopayTier"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptNDC As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptNDC")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptNDC"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptRxNorm As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptRxNorm")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptRxNorm"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptDrugName As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptDrugName")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptDrugName"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptCoverageIndicator As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptCoverageIndicator")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptCoverageIndicator"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptTextMessageDisplayed As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptTextMessageDisplayed")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptTextMessageDisplayed"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")

            ''Public OriginalScriptResourceLinkDisplayed As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("OriginalScriptResourceLinkDisplayed")) Then
            '    strWriter.Write(dtRxDetails.Rows(i)("OriginalScriptResourceLinkDisplayed"))
            'Else
            '    strWriter.Write("")
            'End If
            'strWriter.Write("|")


            strWriter.WriteLine()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Shared Sub WriteARA(ByVal dtRxDetails As DataTable, ByVal i As Integer, ByVal strWriter As StreamWriter)
        Dim dv As DataView

        Try

            '#########################################################################################
            'write the report aggregate fields
            '#########################################################################################

            strWriter.Write("ARA")
            strWriter.Write("|")

            'SourceParticipantID
            strWriter.Write(mdlGeneral.eRxHubParticipantId)
            strWriter.Write("|")

            'DestinationParticipantID
            strWriter.Write("RXHUB")
            strWriter.Write("|")


            'PrescriberDEA
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberDEA")) = True Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberDEA"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'PrescriberNPI
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberNPI")) = True Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberNPI"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")
            'PrescriberStateLicenseNumber
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberStateLicenceNumber")) = True Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberStateLicenceNumber").ToString.Trim)
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'Required if State License # is provided. (C)
            'PrescriberState
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberState")) = True Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberState"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            'Required if State License # is provided. (C)
            'PrescriberZIPCODE 
            If Not IsNothing(dtRxDetails.Rows(i)("PrescriberZIPCODE")) = True Then
                strWriter.Write(dtRxDetails.Rows(i)("PrescriberZIPCODE"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")

            '  Aggregate Record prescriber ID. Value is established by the Technology Vendor.
            'This should not be related to any known identifier like DEA, NPI or state ID.
            'Used for RxHub consolidated reports. If prescriber wants their data deidentified
            'they should use this field.
            'PrescriberConfidentialIdentifier (Confirm)
            'OPTIONAL
            '<<<<<<<<<<<<<<Needs to be confirmed>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            '<<<<<<<<<<<<<Temporarily use ProviderNPI>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            'Dim sPrescriberConfidentialIdentifier As String = ""
            'If Not IsNothing(dtRxDetails.Rows(i)("PrescriberNPI")) Then
            '    sPrescriberConfidentialIdentifier = dtRxDetails.Rows(i)("PrescriberNPI").ToString()
            'End If
            'If sPrescriberConfidentialIdentifier <> "" Then
            '    strWriter.Write(sPrescriberConfidentialIdentifier)
            '    strWriter.Write("|")
            'End If


            strWriter.Write(sPrescriberConfidentialIdentifier)
            strWriter.Write("|")

            'CCYYMMDD. Must be within the start and end report date of the header.
            'PrescriptionDate 
            'strWriter.Write(dtRxDetails.Rows(i)("PrescriptionDate"))
            'If Not IsNothing(dtRxDetails.Rows(i)("PrescriptionDate")) = True Then
            '    strWriter.Write(dtRxDetails.Rows(i)("PrescriptionDate"))
            'Else
            '    strWriter.Write("")
            'End If
            If Not IsNothing(dtRxDetails.Rows(i)("TransactionDate")) = True Then
                Dim dtTemp As DateTime = dtRxDetails.Rows(i)("TransactionDate")

                strWriter.Write(dtTemp.ToString("yyyyMMdd"))
            Else
                strWriter.Write("")
            End If
            strWriter.Write("|")


            'Number of electronically routed prescriptions written by this prescriber for this aggregate group
            'during the report period. RxHub routed prescriptions should not be included in this count.

            'ElectronicPrescriberCount (HardCoded)
            'If Not IsNothing(dtRxDetails.Rows(i)("ElectronicPrescriptionCount")) = True Then
            '    strWriter.Write(dtRxDetails.Rows(i)("ElectronicPrescriptionCount"))
            'Else
            '    strWriter.Write("")
            'End If
            '<<get the prescription count

            dv = New DataView(dtRxDetails)
            dv.RowFilter = " PrescriptionDeliveryMethod= 'E' and nProviderID = " & dtRxDetails.Rows(i)("nProviderID")

            If (dv.Count > 0) Then
                strWriter.Write(dv.Count.ToString())
            Else
                'strWriter.Write("")
                strWriter.Write("0")
            End If
            dv = Nothing
            strWriter.Write("|")

            'Number of fax routed  prescriptions written  by this prescriber for  this aggregate group
            'during the report period.
            'FaxPrescriptionCount (HardCoded)
            'If Not IsNothing(dtRxDetais.Rows(i)("FaxPrescriptionCount")) = True Then
            '    strWriter.Write(dtRxDetails.Rows(i)("FaxPrescriptionCount"))
            'Else
            '    strWriter.Write("")
            'End If


            dv = New DataView(dtRxDetails)
            dv.RowFilter = "PrescriptionDeliveryMethod= 'F' and nProviderID = " & dtRxDetails.Rows(i)("nProviderID")

            If (dv.Count > 0) Then
                strWriter.Write(dv.Count.ToString())
            Else
                'strWriter.Write("")
                strWriter.Write("0")
            End If
            dv = Nothing
            strWriter.Write("|")


            dv = New DataView(dtRxDetails)
            dv.RowFilter = "PrescriptionDeliveryMethod = 'P' and nProviderID = " & dtRxDetails.Rows(i)("nProviderID")

            If (dv.Count > 0) Then
                strWriter.Write(dv.Count.ToString())
            Else
                'strWriter.Write("")
                strWriter.Write("0")
            End If
            dv = Nothing
            strWriter.Write("|")


            'Number of printed prescriptions written by this prescriber for this aggregate group 
            'during the report period
            'PrintedPrescriptionCount  (HardCoded)
            'If Not IsNothing(dtRxDetails.Rows(i)("PrintedPrescriptionCount")) = True Then
            '    strWriter.Write(dtRxDetails.Rows(i)("PrintedPrescriptionCount"))
            'Else
            '    strWriter.Write("")
            'End If



            'Number of prescriptions routed through RxHub by this prescriber for this aggregate group
            'during the report period
            'RxHubRoutedPrescriptionCount  (HardCoded)
            Dim RxHubRoutedPrescriptionCount As String = ""
            dv = New DataView(dtRxDetails)
            dv.RowFilter = "PrescriptionDeliveryMethod= 'R' and nProviderID = " & dtRxDetails.Rows(i)("nProviderID")

            If (dv.Count > 0) Then
                strWriter.Write(dv.Count.ToString())
            Else
                'strWriter.Write("")
                strWriter.Write("0")
            End If
            dv = Nothing
            ' strWriter.Write(RxHubRoutedPrescriptionCount)
            strWriter.WriteLine()

        Catch ex As Exception
            mdlGeneral.UpdateLog("WriteARA() - " & ex.ToString)
            Throw ex
        End Try
    End Sub

#End Region

#Region "File Name Generation"
    Private Shared Function RetrieveFileName(ByVal dtExtractDate As DateTime, ByVal gstrEARDownloadDirectory As String) As String
        Dim _sFileName As String = ""


        'temprarily use current date and time
        dtExtractDate = DateTime.Now()

        Try
            Dim dtClinicName As DataTable = GetClinicName()
            Dim sClinicName As String = "gloClinic"
            If Not IsNothing(dtClinicName) Then
                If dtClinicName.Rows.Count > 0 Then
                    sClinicName = dtClinicName.Rows(0)("sClinicName")
                End If
            End If
            ''''original file naming logic with participant id etc
            '_sFileName = "EAR_" & mdlGeneral.eRxHubParticipantId & "_" & dtExtractDate.ToString("yyyyMMdd") & "_" & dtExtractDate.ToString("hhmmss") & ".rpt"

            ''''new file naming logic with clinic name
            _sFileName = "EAR_" & sClinicName & "_" & dtExtractDate.ToString("yyyyMMdd") & "_" & dtExtractDate.ToString("hhmmss") & ".rpt"

        Catch ex As Exception
            mdlGeneral.UpdateLog("RetrieveFileName() - " & ex.ToString)
            Throw ex
        End Try

        mdlGeneral.sEARFilePath = gstrEARDownloadDirectory & "\" & _sFileName
        Return gstrEARDownloadDirectory & "\" & _sFileName
    End Function
#End Region


#Region "Save Report File in Database"

    Private Shared Function ConvertFiletoBinary(ByVal strEARFileName As String) As Byte()
        If File.Exists(strEARFileName) Then
            Dim oFile As FileStream = Nothing
            Dim oReader As BinaryReader = Nothing
            Try
                oFile = New FileStream(strEARFileName, FileMode.Open, FileAccess.Read)
                oReader = New BinaryReader(oFile)
                Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)
                Return bytesRead

            Catch ex As IOException
                Throw ex
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(oFile) Then
                    oFile.Close()
                End If
                If Not IsNothing(oReader) Then
                    oReader.Close()
                End If

            End Try
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function SaveReportFileDetails(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal EARFileName As String) As Boolean
        If IsSettings() Then
            Dim conn As New SqlConnection(Clsconnect.GetConnectionString())
            Dim cmd As New SqlCommand
            Dim nTransactionID As Long = 0
            Dim objCmdParameter As SqlParameter

            Try

                nTransactionID = GetPrefixTransactionID(dtFromDate, dtToDate)

                cmd = New SqlCommand
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "RxH_InsertPendingEAR"

                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@MachineID"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.BigInt
                objCmdParameter.Value = nTransactionID
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing

                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@nReportID"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.BigInt
                objCmdParameter.Value = 0
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing


                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@dtRptGeneratedDate"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.DateTime
                objCmdParameter.Value = Now
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing

                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@sReportFile"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.VarChar
                objCmdParameter.Value = EARFileName
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing

                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@dtStartDate"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.DateTime
                objCmdParameter.Value = dtFromDate
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing

                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@dtEndDate"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.DateTime
                objCmdParameter.Value = dtToDate
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing

                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@sStatus"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.VarChar
                objCmdParameter.Value = "Pending"
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing


                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@nNoOfAttempts"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.Int
                objCmdParameter.Value = 0
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing

                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@sErrorCode"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.VarChar
                objCmdParameter.Value = ""
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing

                'iReportFileContent
                Dim XMLarrByte As Byte() = ConvertFiletoBinary(sEARFilePath)

                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@iReportFileContent"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.Image
                objCmdParameter.Value = XMLarrByte
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing

                ''''initially this val wil be NULL unless the sStatus is Pending, this will contain the Response file infor whn  the Status value is Uploaded
                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@iReportFileResponseContent"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.Image
                objCmdParameter.Value = Nothing
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing
                'execute the insert query
                conn.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch sqlEx As SqlException
                mdlGeneral.UpdateLog("SaveReportFileDetails() - " & sqlEx.ToString)
                Return True
                Throw sqlEx
            Catch ex As Exception
                mdlGeneral.UpdateLog("SaveReportFileDetails() - " & ex.ToString)
                Return True
                Throw ex

            Finally
                If Not IsNothing(conn) Then
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                        conn.Dispose()
                        conn = Nothing
                    End If
                End If
                If Not IsNothing(cmd) Then
                    cmd.Dispose()
                    cmd = Nothing
                End If
            End Try
        End If
    End Function

    'get the pending report files
    Public Shared Function GetClinicName() As DataTable
        Dim dtClinicName As New DataTable
        If IsSettings() Then

            Dim conn As New SqlConnection
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter
            Dim _strSQL As String = ""

            Try
                _strSQL = "select isNull(sClinicName,'') as sClinicName from clinic_Mst"

                conn.ConnectionString = Clsconnect.GetConnectionString()
                cmd.Connection = conn
                cmd.CommandType = CommandType.Text
                cmd.CommandText = _strSQL

                da = New SqlDataAdapter(cmd)

                da.Fill(dtClinicName)
                Return dtClinicName
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(dtClinicName) Then
                    dtClinicName.Dispose()
                End If
            End Try
        Else
            Return Nothing
        End If
    End Function

    'get the Uploaded report files
    Public Function GetUploadedFiles() As DataTable
        If IsSettings() Then

            Dim dtUploadedFiles As New DataTable
            Dim conn As New SqlConnection
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter
            Dim _strSQL As String = ""

            Try
                _strSQL = "select nReportID as ReportID, sReportFile as [Report File], dtRptGeneratedDate as [RptGenerated Date], dtStartDate as [Start Date], dtEndDate as [End Date], nNoOfAttempts as [NoOfAttempts], isnull(sStatus,'') as Status, isnull(sErrorCode,'') as ErrorCode from RxH_PendingEAR where sStatus = 'Uploaded' order by dtRptGeneratedDate desc"

                conn.ConnectionString = Clsconnect.GetConnectionString()
                cmd.Connection = conn
                cmd.CommandType = CommandType.Text
                cmd.CommandText = _strSQL

                da = New SqlDataAdapter(cmd)

                da.Fill(dtUploadedFiles)

            Catch sqlEx As SqlException
                Throw sqlEx
            Catch ex As Exception
                Throw ex
            End Try

            Return dtUploadedFiles
        Else
            Return Nothing
        End If
    End Function


   
#End Region

#Region "Save Responce file data"

    ' save responce file data in errrcode col
    Public Function SaveResponce(ByVal sResponseFileData, ByVal _reportfilename) As Boolean

        Dim conn As New SqlConnection(gstrDatabaseConnectionString)

        Dim cmd As New SqlCommand
        Dim strQ As String = ""
        Try
            conn.Open()
            strQ = "Update RxH_PendingEAR set sErrorCode='" & sResponseFileData & "' where sReportFile='" & _reportfilename & "'"

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strQ
            Dim result = cmd.ExecuteNonQuery()

            If result > 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Return False
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

#End Region

#Region "Getting All Files-<<not in used>>"
    'get the pending report files
    Public Function GetAllFiles() As DataTable
        If IsSettings() Then

            Dim dtPendingReportFiles As New DataTable
            Dim conn As New SqlConnection
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter
            Dim _strSQL As String = ""

            Try
                _strSQL = "select nReportID as ReportID, sReportFile as [Report File], dtRptGeneratedDate as [RptGenerated Date], dtStartDate as [Start Date], dtEndDate as [End Date], nNoOfAttempts as [NoOfAttempts], sStatus as Status, sErrorCode as ErrorCode from RxH_PendingEAR order by dtRptGeneratedDate desc"

                conn.ConnectionString = gstrDatabaseConnectionString
                cmd.Connection = conn
                cmd.CommandType = CommandType.Text
                cmd.CommandText = _strSQL

                da = New SqlDataAdapter(cmd)

                da.Fill(dtPendingReportFiles)

                da = Nothing

            Catch sqlEx As SqlException
                Throw sqlEx
            Catch ex As Exception
                Throw ex
            End Try

            Return dtPendingReportFiles
        Else
            Return Nothing
        End If

    End Function

#End Region

#Region "Update upload Status"
    'Public Shared Function UpdateStatus(ByVal filename As String) As Boolean
    '    Dim conn As New SqlConnection(gstrDatabaseConnectionString)

    '    Dim cmd As New SqlCommand
    '    Dim strQ As String = ""
    '    Try
    '        conn.Open()
    '        strQ = "update RxH_PendingEAR set sStatus='ResponseRecieved' where sReportFile='" & filename & "'"

    '        cmd.Connection = conn
    '        cmd.CommandType = CommandType.Text
    '        cmd.CommandText = strQ
    '        Dim result = cmd.ExecuteNonQuery()

    '        If result > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If


    '    Catch ex As Exception
    '        Return False
    '    Finally
    '        If conn.State = ConnectionState.Open Then
    '            conn.Close()
    '            conn = Nothing
    '        End If

    '        If Not IsNothing(cmd) Then
    '            cmd.Dispose()
    '            cmd = Nothing
    '        End If
    '    End Try
    'End Function

    Public Shared Function UpdateStatus(ByVal EARDBfilename As String, ByVal EARRespFilePath As String, ByVal ConnectionString As String) As Boolean
        If IsSettings() Then
            Dim conn As New SqlConnection(ConnectionString)
            Dim cmd As New SqlCommand
            Dim nTransactionID As Long = 0
            Dim objCmdParameter As SqlParameter

            Try

                cmd = New SqlCommand
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "sp_RxhUpdatePendingEAR"

                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@sReportFile"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.VarChar
                objCmdParameter.Value = EARDBfilename
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing


                'iReportFileContent
                Dim XMLarrByte As Byte() = ConvertFiletoBinary(EARRespFilePath)

                ''''update the reponse file data
                objCmdParameter = New SqlParameter
                objCmdParameter.ParameterName = "@iReportFileResponseContent"
                objCmdParameter.Direction = ParameterDirection.Input
                objCmdParameter.SqlDbType = SqlDbType.Image
                objCmdParameter.Value = XMLarrByte
                cmd.Parameters.Add(objCmdParameter)
                objCmdParameter = Nothing
                'execute the insert query
                conn.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch sqlEx As SqlException
                Return True
                Throw sqlEx
            Catch ex As Exception
                Return True
                Throw ex

            Finally
                If Not IsNothing(conn) Then
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                        conn.Dispose()
                        conn = Nothing
                    End If
                End If
                If Not IsNothing(cmd) Then
                    cmd.Dispose()
                    cmd = Nothing
                End If
            End Try
        End If
    End Function
#End Region


#Region "Get RxHUb participantID and RxHUB Password from setting table which is set by gloEMR-Admin"

    Public Shared Function getIDPWRD() As String
        If IsSettings() Then

            Dim sPartcipantID As String = ""
            Dim sPwrd As String = ""
            Dim sPIDPASS As String = ""

            Dim dtSettings As New DataTable
            Dim conn As New SqlConnection
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter
            Dim _strSQL As String = ""

            Dim objEncrypt As New clsRxencryption

            Try
                _strSQL = "select nSettingsID, sSettingsName, sSettingsValue, nClinicID, nUserID, nUserClinicFlag from Settings"

                conn.ConnectionString = gstrDatabaseConnectionString
                cmd.Connection = conn
                cmd.CommandType = CommandType.Text
                cmd.CommandText = _strSQL

                da = New SqlDataAdapter(cmd)

                da.Fill(dtSettings)

                da = Nothing

                If Not IsNothing(dtSettings) Then

                    If dtSettings.Rows.Count > 0 Then
                        For i As Integer = 0 To dtSettings.Rows.Count - 1
                            If dtSettings.Rows(i)("sSettingsName") = "RXHUB PARTICIPANTID" Then
                                sPartcipantID = dtSettings.Rows(i)("sSettingsValue").ToString.Trim
                                Exit For
                            Else
                                sPartcipantID = ""
                            End If
                        Next

                        For i As Integer = 0 To dtSettings.Rows.Count - 1
                            If dtSettings.Rows(i)("sSettingsName") = "RXHUB PASSWORD" Then
                                sPwrd = dtSettings.Rows(i)("sSettingsValue").ToString.Trim

                                sPwrd = objEncrypt.DecryptFromBase64String(sPwrd, constEncryptDecryptKey)
                                Exit For
                            Else
                                sPwrd = ""
                            End If
                        Next

                    End If
                End If

                sPIDPASS = sPartcipantID & "|" & sPwrd 'combine participantid | password

            Catch sqlEx As SqlException
                Throw sqlEx
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(objEncrypt) Then
                    'objEncrypt = Nothing
                End If
            End Try

            Return sPIDPASS
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region "Connect"

    Public Shared Function IsSettings() As Boolean
        'Dim regKey As Microsoft.Win32.RegistryKey
        'Dim objEncryption As gloEAR.clsRxencryption
        Try

            'If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
            '    UpdateLog("Registry Entry not found")
            '    Return False
            'End If

            'regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)

            'If IsNothing(regKey.GetValue("SQLServer")) = True Then
            '    UpdateLog("Server key not found")
            '    Return False
            'ElseIf IsNothing(regKey.GetValue("Database")) = True Then
            '    UpdateLog("Database key not found")
            '    Return False
            'End If
            'If IsNothing(regKey.GetValue("SQLUser")) = True Then
            '    Return False

            'End If
            'If IsNothing(regKey.GetValue("SQLPassword")) = True Then
            '    Return False
            'End If


            Clsconnect.gServerName = sServerName ''''regKey.GetValue("SQLServer")
            Clsconnect.gDataBase = sDatabaseName ''''regKey.GetValue("Database")

            Clsconnect.gstrUserId = sUserName ''''''regKey.GetValue("SQLUserEMR")
            'Dim strPassword As String
            'strPassword = sPassword ''''''regKey.GetValue("SQLPasswordEMR")

            'objEncryption = New gloEAR.clsRxencryption
            Clsconnect.gstrPassword = sPassword 'objEncryption.DecryptFromBase64String(strPassword, constEncryptDecryptKey)  'Clsconnect.constEncryptDecryptKey)
            'UpdateLog("Retrieved password successfully ")

            If Clsconnect.gServerName = "" Or Clsconnect.gDataBase = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            UpdateLog("Error- Retrieving Registry Settings" & ex.ToString)
        Finally
            'If Not IsNothing(regKey) Then
            '    regKey.Close()
            'End If
        End Try
    End Function
#End Region


#Region "Commented Code"
    'Public Function GenerateEARTextFile() As Boolean
    '    Dim _result As Boolean = True
    '    Dim strWriter As New strWriter(sEARFileName, False)
    '    Dim objEARFileHeader As New gloEAR.gloEARSections.gloEARFileHeader
    '    Dim objEARReportHeader As New gloEAR.gloEARSections.gloEARReportHeader
    '    Dim objEARReportTrailer As New gloEAR.gloEARSections.gloEARReportTrailer
    '    Dim objEARFileTrailer As New gloEAR.gloEARSections.gloEARFileTrailer

    '    'declare an obj to get all the Rx agrregate and rx level records
    '    Dim objEARRXDetails As New gloEAR.gloEARSections.gloEARReportDetailRxLevels
    '    Dim objEARRXAggregates As New gloEAR.gloEARSections.gloEARReportDetailAggregates

    '    Dim objRxData As New gloEAR.gloRxData

    '    Try

    '        'write the file header fields
    '        strWriter.Write(objEARFileHeader.RecordType)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARFileHeader.Version)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARFileHeader.SenderID)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARFileHeader.SenderParticipantPwd)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARFileHeader.ReceiverID)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARFileHeader.SourceName)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARFileHeader.TransmissionControlNumber)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARFileHeader.TransmissionDate)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARFileHeader.TransmissionTime)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARFileHeader.TransmissionFileType)
    '        strWriter.Write("|")

    '        'write the report header fields
    '        strWriter.Write(objEARReportHeader.RecordType)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARReportHeader.ReportStartDate)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARReportHeader.ReportEndDate)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARReportHeader.TransmissionAction)
    '        strWriter.Write("|")



    '        '#########################################################################################
    '        'write the report aggregate fields and report detail fields


    '        objRxData.GetEARReportAggregateDetails(objEARRXAggregates)






    '        '#########################################################################################



    '        'write report trailer fields

    '        strWriter.Write(objEARReportTrailer.RecordType)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARReportTrailer.TotalRecords)
    '        strWriter.Write("|")

    '        'write file trailer fields
    '        strWriter.Write(objEARFileTrailer.RecordType)
    '        strWriter.Write("|")
    '        strWriter.Write(objEARFileTrailer.TotalRecords)
    '        strWriter.Write("|")

    '    Catch IOex As IOException
    '        Throw IOex
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '    End Try

    '    Return _result
    'End Function
#End Region


End Class
