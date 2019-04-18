Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Text
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Linq

Namespace gloGeneral

    Public Class FormularyServiceException
        Inherits Exception

        Public Sub New(ByVal ExceptionString As String)
            MyBase.New(ExceptionString)
        End Sub

    End Class

    Public Class FormularyRESTService(Of ListReturningType)

        Public Shared Function GetFormularyRESTService() As FormularyRESTService(Of ListReturningType)
            Return New FormularyRESTService(Of ListReturningType)
        End Function

        Private Sub New()

        End Sub

        Public Function GetResponseString(jsonRequestString As String, ByVal URLOption As URLOption) As String
            Try
                Dim sURL As String = clsgeneral.gstrFormularyServiceURL + URLOption.ToString()
                Dim bytes As Byte() = Encoding.UTF8.GetBytes(jsonRequestString)
                Dim jsonResponseString As String = Nothing

                Dim request As HttpWebRequest = WebRequest.Create(sURL)

                With request
                    .Method = "POST"
                    .ContentType = "application/json"
                    .Accept = "application/json"
                    .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                End With

                Using postStream As Stream = request.GetRequestStream()
                    With postStream
                        .Write(bytes, 0, bytes.Length)
                        .Close()
                    End With
                End Using

                jsonResponseString = Me.GetResponse(request)

                Return jsonResponseString
            Catch ex As Exception
                Throw New FormularyServiceException(ex.ToString())                
            End Try
        End Function

        Private Function GetResponse(ByVal Request As HttpWebRequest) As String
            Dim sReturned As String = ""

            Try
                Using response As HttpWebResponse = Request.GetResponse()
                    Using reader As StreamReader = New StreamReader(response.GetResponseStream())
                        sReturned = reader.ReadToEnd()
                        reader.Close()
                    End Using
                    response.Close()
                End Using
            Catch ex As Exception
                Throw New Exception("Problem in internet connectivity.")                
            End Try

            Return sReturned
        End Function

        Public Function GetList(ByVal jsonRequestString As String) As List(Of ListReturningType)
            Try
                Dim Deserializer As New JavaScriptSerializer()
                Deserializer.MaxJsonLength = Int32.MaxValue
                Dim lstReturningObjects As New List(Of ListReturningType)

                lstReturningObjects = Deserializer.Deserialize(Of List(Of ListReturningType))(jsonRequestString)
                Deserializer = Nothing

                Return lstReturningObjects
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function CheckURLConnectivity(ByVal BaseURL As String, ByVal IsForDIB As Boolean) As Boolean
            Dim bReturned As Boolean = False
            Try
                Dim sURL As String = BaseURL + URLOption.CheckURLConnectivity.ToString()
                Dim bytes As Byte() = Encoding.UTF8.GetBytes(String.Empty)
                Dim jsonResponseString As String = Nothing

                Dim request As HttpWebRequest = WebRequest.Create(sURL)

                With request
                    .Method = "POST"
                    .ContentType = "application/json"
                    .Accept = "application/json"
                    .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
                End With

                Using postStream As Stream = request.GetRequestStream()
                    With postStream
                        .Write(bytes, 0, bytes.Length)
                        .Close()
                    End With
                End Using

                Using response As HttpWebResponse = request.GetResponse()
                    Using reader As StreamReader = New StreamReader(response.GetResponseStream())
                        jsonResponseString = reader.ReadToEnd()
                        reader.Close()
                    End Using
                    response.Close()
                End Using

                If jsonResponseString.Any() Then
                    bReturned = True
                End If
            Catch ex As WebException
                If ex.Status = WebExceptionStatus.TrustFailure Then
                    Dim sMessage As String

                    If IsForDIB Then
                        sMessage = "Error occured while installation of centralized DIB service certificate. " + vbCrLf + "Please contact System Administrator."
                    Else
                        sMessage = "Error occured while installation of centralized formulary service certificate. " + vbCrLf + "Please contact System Administrator."
                    End If

                    Throw New FormularyServiceException(sMessage)
                ElseIf ex.Status = WebExceptionStatus.ConnectFailure Then
                    Throw New FormularyServiceException("Error in internet connectivity. Please try again.")
                Else
                    Throw New FormularyServiceException(ex.ToString())
                End If
            End Try

            Return bReturned
        End Function

    End Class

    Public Class clsgeneral
        'Added by Ashish Tamhane for Centralized Formulary Web Service
        Public Shared gstrFormularyServiceURL As String = String.Empty ' "http://173.167.9.140/gloFormalaryRest/gloCentralizedFormulary/"
        Public Shared gblnUseFormularyService As Boolean = False 'True

        'Setting added by Ashish on 2nd March 2015 for Centralized Formulary 3.0 changes
        Public Shared gblnIsFormularyServiceEnabled As Boolean = False
        Public Shared gblnIsEpaServiceEnabledProvider As Boolean = True
        Public Shared gblnIsPDREnabled As Boolean = False
        Public Shared gblnIsPDMPEnabled As Boolean = False
        Public Shared gblnCustomPrescEdited As Boolean = False '''''For CCHIT11 for audit log
        Public Shared gblnCustomMediEdited As Boolean = False '''''For CCHIT11 for audit log

        Public Shared gstrPatientAgeInYears As Integer ''''''used for dose check
        Public Shared gstrPatientAgeInMonths As Integer ''''''used for dose check
        Public Shared gstrPatientDOB As String
        ''Added on 20101006
        Public Shared blnSMDBSetting As Boolean = False '''''variable that will check whether SnoMed setting is on.
        ' Public Shared blnRemovePatientDataSetting As Boolean = False '''''variable that will check whether SnoMed setting is on.
        ''
        ''variables used for constructing Formulary connection string and other purposes
        Public Shared _EMRConnectionString As String = "" ''''var will contain EMR connection string whnever we are working with Formulary database string
        Public Shared sFORMULARYAUTHENTICATION As String = ""
        Public Shared sFORMULARYSERVERNAME As String = ""
        Public Shared sFORMULARYDATABASENAME As String = ""
        Public Shared sFORMULARYUSERID As String = ""
        Public Shared sFORMULARYPASSWORD As String = ""
        Public Shared sFORMULARYCONNECTIONSTRING As String = ""
        ''variables used for constructing Formulary connection string and other purposes

        ''Added new variables by Abhijeet on 20101109 for HL7 Hybrid database connection string
        Public Shared gstrHL7ServerName As String = String.Empty
        Public Shared gstrHL7DatabaseName As String = String.Empty
        Public Shared gstrHL7UserId As String = String.Empty
        Public Shared gstrHL7Password As String = String.Empty
        Public Shared gboolHL7IsSQLAuthentication As Boolean = False
        ''End of changes for adding new variables by Abhijeet on 20101109 for HL7 Hybrid database connection string

        ''Added by Abhijeet on 20101120
        Public Shared gstrSpecificResultRange As String = String.Empty
        ''End of changes by Abhijeet on 20101120

        ''Added by Abhijeet on 20110422
        Public Shared gboolIMRegistryHL7Format As Boolean = False
        ''End of changes by Abhijeet on 20110422


        ' Added by Rahul Patel for MMW Hybrid Database changes on 22-10-2010
        Public Shared sMmwServerName As String = ""
        Public Shared sMmwDatabaseName As String = ""
        Public Shared sMmwUserId As String = ""
        Public Shared sMmwPassword As String = ""
        Public Shared sIsSqlAuthentication As Boolean = False
        'end of Code by Rahul Patel

        'Public Shared sDIBServiceURL As String = ""

        Private Shared _sDIBServiceURL As String = ""
        Public Shared Property sDIBServiceURL() As String
            Get
                Return _sDIBServiceURL
            End Get
            Set(ByVal value As String)
                _sDIBServiceURL = value
                gloSureScript.gloSurescriptGeneral.sDIBServiceURL = value
            End Set
        End Property


        Public Shared sDruginteractionServiceURL As String = ""

        ' Added by Rahul Patel for DMS Hybrid Database changes on 26-10-2010
        Public Shared sDmsServerName As String = ""
        Public Shared sDmsDatabaseName As String = ""
        Public Shared sDmsUserId As String = ""
        Public Shared sDmsPassword As String = ""
        Public Shared sDmsIsSqlAuthentication As Boolean = False
        'end of Code by Rahul Patel

        'Code Start-Added  by Rahul Patel on 23-10-2010 for rxnorm db settings
        Public Shared gblnRxNDBSetting As Boolean
        Public Shared gRxNDBConnstr As String = ""
        Public Shared gRxNServerName As String = ""
        Public Shared gRxNDatabaseName As String = ""
        Public Shared gRxNUserID As String = ""
        Public Shared gRxNPassWord As String = ""
        Public Shared gRxNIsSQLAUTHEN As Boolean
        'Code End-Added  by Rahul Patel on 23-10-2010 for rxnorm db settings

        ''Added by Mayuri:20101119-SNOMED settings
        Public Shared gstrSMDBServerName As String = ""
        Public Shared gstrSMDBDatabaseName As String = ""
        Public Shared gstrSMDBUserID As String = ""
        Public Shared gstrSMDBPassWord As String = ""
        Public Shared gblnSMDBAuthen As Boolean
        ''SNOMED settings

        Public Shared blnIsRxMedsFormEdited As Boolean = False ''''''this var will track if there are any changes done to the Rx-Meds form. this is added wrt to drugs added and form is closed directly then the user should be prompted before closing. Only done on Form Close event. this var wil be traced from gloEMR and also the gloUserControl Library

        'by default keep the Prescription C1flexgrid = true, becaz we r on the Prescription form
        Public Shared blnRxC1FlexClick As Boolean = True

        ''used for drug delete flag on rx, mx grid 
        Public Shared blnRxDeleteFlag As Boolean = False
        'Formulary
        Public Shared gstrSelectedPBM As String = ""
        Public Shared gnFormularyPatientID As Long = 0
        'Formulary

        'Formulary Settings
        Public Shared blnFormlyAlt_AllDrugs As Boolean = False
        Public Shared blnFormlyAlt_OffFormularyDrugs As Boolean = False
        Public Shared blnFormlyAlt_NRDrugs As Boolean = False
        Public Shared blnFormlyShowOFFFormulary_Alt As Boolean = False
        Public Shared blnShowNDCInMedication_History As Boolean = False
        'Formulary Settings

        ''in RxDrugs tree if provider specific drugs btn is selected then when we click on the dosage btn in custom Rx control pull the sig info from drugs provider configuration table else pull info from sigMST table
        Public Shared blnIsProviderSpecificDrugsBtnSelected As Boolean = False

        Public Shared gblnPatientAdded As Boolean = False
        '<Vinayak>
        Public Shared gOMRCategory_History As String = ""
        Public Shared gOMRCategory_ROS As String = ""
        Public Shared gOMRCategory_PatientRegistration As String = ""
        Public Shared gDMSCategory_PatientDirective As String = ""
        '<Vinayak>
        Public Shared gstrServerPath As String
        Public Shared gnAppointmentModuleLevel As Byte
        Public Shared gLockTime As Byte
        '' BY Mahesh , 20070423
        '''''<Patient Alert ForeColor>
        Public Shared gnAlertForeColor As Long
        Public Shared gnAlertBackColor As Long
        '''''<//Patient Alert ForeColor>\
        Public Shared StartUpPath As String
        Public Shared gstrMessageBoxCaption As String = "gloEMR"
        Private Shared _ConnectionString As String
        Public Shared gnThresholdSetting As Double

        Public Shared gblnRecordLocking As Boolean
        'Public Shared _blnRecordLock_Med As Boolean
        'Public Shared _blnRecordLock_Pres As Boolean
        Public Shared gblnIsStagingServer As Boolean

        'Public Shared _gblnFormLocked As Boolean = False
        'Public Shared _gblnFormLevelID As Int64 = 0

        Public Shared gblnDisableAllowSubstitution As Boolean?

        Public Shared gstrTempDirPath As String '' This will hold the path for the server OS
        Public Shared gDMSV3TempPath As String = ""
        Public Shared ds As DataSet ' This dataset added in 6040 used in saving prescription and medication using TVP 
        Public Shared gblnIcd10Transition As Boolean
        Public Shared Function GetConnectionstring()
            Return ""
        End Function
        Public Shared WriteOnly Property ConnectionString() As String
            Set(ByVal value As String)
                _ConnectionString = value
                gloEMRDatabase.DataBaseLayer.ConnectionString = _ConnectionString
                'Debug.Print("Conn: " & _ConnectionString)
            End Set
        End Property

        ''Added by Abhijeet on 20101109 for HL7 HYbrid database connection string 

        Public Shared Function GetHL7ConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
            Dim strConnectionString As String
            If isSQLAuthentication = False Then
                strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
            Else
                strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
            End If
            Return strConnectionString
        End Function
        Public Shared Function GetHL7ConnectionString() As String
            Return GetHL7ConnectionString(gstrHL7ServerName, gstrHL7DatabaseName, gboolHL7IsSQLAuthentication, gstrHL7UserId, gstrHL7Password)
        End Function
        'Public Shared Function GetDatabaseConnectionIdInHL7DB() As Int64

        '    Dim conn As SqlConnection = New SqlConnection(GetHL7ConnectionString())
        '    Dim cmd As SqlCommand = Nothing
        '    Try
        '        cmd = New SqlCommand()
        '        conn.Open()
        '        cmd.Connection = conn
        '        cmd.CommandType = CommandType.Text
        '        cmd.CommandText = "select "
        '    Catch ex As Exception
        '    Finally
        '        If cmd IsNot Nothing Then
        '            cmd.Parameters.Clear()
        '            cmd.Dispose()
        '            cmd = Nothing
        '        End If
        '    End Try
        '    Return 0
        'End Function
        ''End of change by Abhijeet on 20101109 for HL7 Hybid Database connection string

#Region "Function added to resolve the problem : 00000199"
        'Function added to resolve the problem : 00000199
        Public Shared Function GetUniqueID() As Long
            Dim cmd As New SqlCommand
            Dim cnn As New SqlConnection(_ConnectionString)
            Dim _Id As Long = 0
            Try
                If cnn.State = ConnectionState.Closed Then
                    cnn.Open()
                End If
                cmd.Connection = cnn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "gSP_GetUniqueID"

                Dim sqlparm As SqlParameter = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
                sqlparm.Direction = ParameterDirection.Output

                cmd.ExecuteNonQuery()
                _Id = cmd.Parameters("@ID").Value
                sqlparm = Nothing
            Catch ex As Exception
                Throw ex
            Finally
                If Not cmd Is Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If Not cnn Is Nothing Then
                    If cnn.State = ConnectionState.Open Then cnn.Close()
                    cnn.Dispose()
                    cnn = Nothing
                End If
            End Try
            Return _Id
        End Function
#End Region

        Public Shared Function GetPrefixTransactionID(ByVal PatientID As Long) As Long
            Dim strID As String
            Dim strPatientID As String
            Dim strPatientTempID As String
            strPatientID = CStr(PatientID)
            If strPatientID.Length >= 15 Then
                strPatientTempID = strPatientID.Substring(4, 1) & strPatientID.Substring(9, 1) & strPatientID.Substring(14, 1)
            Else
                Select Case strPatientID.Length
                    Case 1
                        strPatientTempID = "00" & strPatientID
                    Case 2
                        strPatientTempID = "0" & strPatientID
                    Case Else
                        strPatientTempID = Right(strPatientID, 3)
                End Select
            End If
            Dim dtDate As DateTime
            dtDate = System.DateTime.Now

            strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)
            strID = strID & strPatientTempID.Substring(0, 1)
            strID = strID & DateDiff(DateInterval.Second, dtDate.Date, dtDate)
            strID = strID & strPatientTempID.Substring(1, 1)
            strID = strID & dtDate.Millisecond
            strID = strID & strPatientTempID.Substring(2, 1)
            Return CLng(strID)
        End Function

        Public Shared Function GetPrefixTransactionID(ByVal PatientDOB As DateTime) As Long

            Dim strID As String
            Dim dtDate As DateTime
            dtDate = System.DateTime.Now
            strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), PatientDOB.Date)
            Return CLng(strID)
        End Function


#Region " Record Locking "
        Public Enum TrnType
            None = 0
            PatientRegistration = 1
            PatientROS = 2
            PatientHistory = 3
            Medication = 4
            Prescription = 5
            PatientVitals = 6
            Radiology = 7
            Labs = 8
            Messages = 9
            Letters = 10
            PTProtocol = 11
            PatientConsent = 12
            Flowsheet = 13
            Task = 14
            Immunization = 15
            ReferralLetters = 16
            DMS = 17
            ProblemList = 18
        End Enum

        'Public Shared Function Scan_n_Lock_Transaction(ByVal TransactionType As TrnType, ByVal RecordID As Long, ByVal VisitID As Long, ByVal VisitDate As DateTime) As gloEMRGeneralLibrary.gloEMRActors.generalList

        '    Dim Con As New SqlConnection(_ConnectionString)
        '    Try
        '        '''' gsp_Scan_n_Lock_Record this Stored Procedure Checks the User Name & Machine Name if Record Exist in then
        '        ''  @nRecordID	    NUMERIC(18,0),
        '        ''  @nVisitID	    NUMERIC(18,0),
        '        ''  @dtVisitDate	DATETIME,
        '        ''  @nTrnType	    Int,
        '        ''  @sUserName	    VARCHAR(50) Output,
        '        ''  @sMachineName	VARCHAR(50) Output 

        '        Dim cmd As New SqlCommand("gsp_Scan_n_Lock_Record", Con)
        '        cmd.CommandType = CommandType.StoredProcedure
        '        Dim sqlParam As New SqlParameter

        '        sqlParam = cmd.Parameters.Add("@nRecordID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = RecordID

        '        sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = VisitID

        '        sqlParam = cmd.Parameters.Add("@dtVisitDate", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = VisitDate

        '        sqlParam = cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = TransactionType

        '        Dim sqlParamUserName As New SqlParameter
        '        sqlParamUserName = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar, 50)
        '        sqlParamUserName.Direction = ParameterDirection.InputOutput
        '        sqlParamUserName.Value = globalSecurity.gstrLoginName

        '        Dim sqlParamMachineName As New SqlParameter
        '        sqlParamMachineName = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar, 50)
        '        sqlParamMachineName.Direction = ParameterDirection.InputOutput
        '        sqlParamMachineName.Value = globalSecurity.gstrClientMachineName

        '        Con.Open()
        '        cmd.ExecuteNonQuery()

        '        Dim mygenerallist As New gloEMRActors.generalList

        '        If IsDBNull(sqlParamUserName.Value) Then
        '            mygenerallist.Code = ""
        '        Else
        '            mygenerallist.Code = sqlParamUserName.Value
        '        End If

        '        If IsDBNull(sqlParamMachineName.Value) Then
        '            mygenerallist.Description = ""
        '        Else
        '            mygenerallist.Description = sqlParamMachineName.Value
        '        End If

        '        Return mygenerallist
        '        ' ''mydt.Code = User Name
        '        ' ''mydt.Description = Machine Name

        '    Catch ex As SqlException
        '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return Nothing
        '    Catch ex As Exception
        '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return Nothing
        '    Finally
        '        Con.Close()
        '    End Try
        'End Function

        'Public Shared Function UnLock_Transaction(ByVal TransactionType As TrnType, ByVal RecordID As Long, ByVal VisitID As Long, ByVal VisitDate As DateTime) As Boolean
        '    Dim Con As New SqlConnection(_ConnectionString)
        '    Try
        '        '''' IN gsp_UnLock_Record Stored Procedure we Unlock the record 
        '        ''  @nRecordID	NUMERIC(18,0),
        '        ''  @nVisitID	NUMERIC(18,0),
        '        ''  @dtVisitDate	DATETIME,
        '        ''  @nTrnType	Int

        '        Dim cmd As New SqlCommand("gsp_UnLock_Record", Con)
        '        cmd.CommandType = CommandType.StoredProcedure
        '        Dim sqlParam As SqlParameter

        '        sqlParam = cmd.Parameters.Add("@nRecordID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = RecordID

        '        sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = VisitID

        '        sqlParam = cmd.Parameters.Add("@dtVisitDate", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = VisitDate

        '        sqlParam = cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = TransactionType


        '        Con.Open()
        '        cmd.ExecuteNonQuery()

        '        Return True

        '    Catch ex As SqlException
        '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    Catch ex As Exception
        '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    Finally
        '        Con.Close()
        '    End Try
        'End Function


        Public Shared Function Scan_n_Lock_FormLevel(ByVal PatID As Long, ByVal VisitID As Long, ByVal TrasnsID As Long, ByVal FormName As String) As DataTable
            Dim Con As New SqlConnection(_ConnectionString)
            Dim cmd As SqlCommand = Nothing
            Dim sqlParam As New SqlParameter
            Try
                cmd = New SqlCommand("Scan_n_Lock_FormLevel", Con)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@FormLockingID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.InputOutput
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatID

                sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = VisitID

                sqlParam = cmd.Parameters.Add("@TransactionID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = TrasnsID

                sqlParam = cmd.Parameters.Add("@FormName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = FormName

                ''Add ProcessID 
                sqlParam = cmd.Parameters.Add("@ProcessID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.Diagnostics.Process.GetCurrentProcess().Id

                ''Add TVP All processes
                sqlParam = cmd.Parameters.Add("@TmpFormLocks", SqlDbType.Structured)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = getAllInstances(FormName)

                sqlParam = cmd.Parameters.Add("@MachineName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = globalSecurity.gstrClientMachineName

                sqlParam = cmd.Parameters.Add("@UserName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = globalSecurity.gstrLoginName

                Dim dt As New DataTable
                Dim da As SqlDataAdapter

                da = New SqlDataAdapter(cmd)
                da.Fill(dt)
                da.Dispose()
                da = Nothing
                Return dt

            Catch ex As SqlException
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally

                If Not IsNothing(cmd) Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

                If Not IsNothing(sqlParam) Then
                    sqlParam = Nothing
                End If

                If Not IsNothing(Con) Then
                    Con.Close()
                    Con.Dispose()
                    Con = Nothing
                End If
            End Try
        End Function

        Public Shared Function getAllInstances(ByVal FormName As String) As DataTable
            Dim TempDt As New DataTable("Processes")
            Try

                Dim colProcessID As New DataColumn("ProcessID")
                colProcessID.DataType = GetType(Long)
                'Dim colFormName As New DataColumn("FormName")
                'colFormName.DataType = GetType(String)

                TempDt.Columns.Add(colProcessID)
                'TempDt.Columns.Add(colFormName)

                ' Dim _currentSessionID As Int32 = System.Diagnostics.Process.GetCurrentProcess().SessionId
                Dim _currentProcName As String = System.Diagnostics.Process.GetCurrentProcess().ProcessName
                'Dim _currentProcessID As Int32 = System.Diagnostics.Process.GetCurrentProcess().Id

                Dim _Process() As System.Diagnostics.Process = Process.GetProcessesByName(_currentProcName)

                'If _Process.Length > 1 Then
                For Each _proc As System.Diagnostics.Process In _Process
                    'If _proc.SessionId = _currentSessionID And _proc.Id <> _currentProcessID And _proc.ProcessName = _currentProcName Then
                    Dim dr As DataRow = TempDt.NewRow()
                    dr.Item("ProcessID") = _proc.Id
                    TempDt.Rows.Add(dr)
                    'End If
                Next
                'End If
                Return TempDt
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally
                'If Not IsNothing(TempDt) Then
                '    TempDt.Dispose()
                '    TempDt = Nothing
                'End If
            End Try
        End Function


#End Region

        Public Sub New()

        End Sub
    End Class
    'Public Class globalPatient
    '    'Public Shared gnVisitID As Long
    '    Public Shared gnPatientID As Long
    '    'Public Shared gstrPatientCode As String
    '    'Public Shared gstrPatientFirstName As String
    '    ' Public Shared gstrPatientLastName As String
    '    'Public Shared gstrPatientDOB As String
    '    'Public Shared gstrPatientAge As String
    '    'Public Shared gstrDoctorName As String
    '    'Public Shared gnDoctorID As Int64
    '    'Public Shared gstrProviderName As String 'Global Provider name
    '    'Public Shared gtsrPatientStatus_Pending As String = "Legal Pending"
    '    'Public Shared gtsrPatientStatus_Deceased As String = "Deceased"
    '    'Public Shared gtsrPatientStatus_LockCharts As String = "Lock Charts"
    'End Class
    Public Class globalSecurity
        Public Shared gstrLoginName As String
        Public Shared gstrNickName As String
        Public Shared gstrLoginPassword As String
        Public Shared gstrLoginTime As String
        Public Shared gblnIsAdmin As Boolean
        Public Shared gstrDatabaseName As String
        Public Shared gstrSQLServerName As String

        Public Shared gblnSQLAuthentication As Boolean ''20100128
        Public Shared gstrSQLUserEMR As String
        Public Shared gstrSQLPasswordEMR As String

        Public Shared gblnCheckNewVersion As Boolean
        Public Shared gstrMessageBoxCaption As String = "gloEMR"
        Public Shared gstrClientMachineName As String
        Public Shared gnClientMachineID As String
        Public Shared gIsCCHITSecurityAdmin As Boolean = False
        Public Shared constEncryptDecryptKey = "12345678"
        Public Shared nWorkingTimeColor As Integer
        Public Shared nNonWorkingTimeColor As Integer
        Public Shared nBusyTimeColor As Integer
        Public Shared nMissingAppointmentsColor As Integer
        Public Shared nPullChartsAppointmentsColor As Integer
        Public Shared gintNoOfAttempts As Integer = 0
        Public Shared gnUserID As Int64
    End Class
    Public Class globalAppointment
        Public Shared gClinicStartTime As DateTime
        Public Shared gClinicEndTime As DateTime
        Public Shared gnAppointmentInterval As Int16
        Public Shared gnPULLCHARTSInterval As Int16

        Public Sub New()

        End Sub
    End Class


End Namespace