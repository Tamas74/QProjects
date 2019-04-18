Imports System.Data
Imports System.Data.SqlClient
Public Class ClsPatientInjuryDate
    Implements IDisposable
    'variable strTempChiefComplaint used for store previous Chief complaints 
    Dim strTempChiefComplaint As String
    Dim _PatientID As Long
    Public Sub New(ByVal PatientID As Long)
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        _PatientID = PatientID
    End Sub
    Private Conn As SqlConnection
    Public Property TempChiefComplaint()
        Get
            Return strTempChiefComplaint
        End Get
        Set(ByVal value)
            strTempChiefComplaint = value
        End Set
    End Property
    Public Function FetchData(ByVal PatientID As Long) As ArrayList
        Dim arrlist As ArrayList
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter
        Dim dreader As SqlDataReader
        Try
            arrlist = New ArrayList
            cmd = New System.Data.SqlClient.SqlCommand("gsp_getPatientInjuryDate", Conn)
            cmd.CommandType = CommandType.StoredProcedure

            objParam = cmd.Parameters.AddWithValue("@PatientId", PatientID)
            objParam.Direction = ParameterDirection.Input


            Conn.Open()
            dreader = cmd.ExecuteReader
            Do While dreader.Read()
                arrlist.Add(dreader.Item(0))
                arrlist.Add(dreader.Item(1))
                arrlist.Add(dreader.Item(2))
            Loop
            dreader.Close()
            Conn.Close()
            Return arrlist
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            Return Nothing
        Finally
            dreader = Nothing
            objParam = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
        
    End Function

    ''Sandip Dardade 21 st Feb 09 
    ''to get data from  PatientChiefComplaint table.
    Public Function GetComplaints(ByVal PatientID As Long, ByVal VisitID As Long, ByVal ExamID As Long) As ArrayList
        Dim arrlist As ArrayList
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter
        Dim dreader As SqlDataReader
        Try
            arrlist = New ArrayList
            cmd = New System.Data.SqlClient.SqlCommand("gsp_SELECT_PatientChiefComplaint", Conn)
            cmd.CommandType = CommandType.StoredProcedure

            objParam = cmd.Parameters.AddWithValue("@nPatientID", PatientID)
            objParam.Direction = ParameterDirection.Input

            objParam = cmd.Parameters.AddWithValue("@nVisitID", VisitID)
            objParam.Direction = ParameterDirection.Input

            objParam = cmd.Parameters.AddWithValue("@nExamID", ExamID)
            objParam.Direction = ParameterDirection.Input

            objParam = cmd.Parameters.AddWithValue("@nClinicID", gnClinicID)
            objParam.Direction = ParameterDirection.Input

            Conn.Open()
            dreader = cmd.ExecuteReader
            Do While dreader.Read()
                arrlist.Add(dreader.Item(0))
                arrlist.Add(dreader.Item(1))
                arrlist.Add(dreader.Item(2))
                arrlist.Add(dreader.Item(3))
                arrlist.Add(dreader.Item(4))
                arrlist.Add(dreader.Item(5))
                arrlist.Add(dreader.Item(6))
            Loop
            dreader.Close()
            Conn.Close()
            Return arrlist
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            Return Nothing
        Finally
            dreader = Nothing
            objParam = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
        
    End Function

    ''Sandip Dardade 21 st Feb 09 
    ''to add data to  PatientChiefComplaint table.
    Public Sub AddChiefComplaints(ByVal PatientID As Int64, ByRef arrlist As ArrayList)
        Dim objparam As SqlParameter
        Dim cmd As SqlCommand = Nothing
        Try

            cmd = New SqlCommand("gsp_INUP_PatientchiefComplaints", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            '@nChiefComplaintID @nPatientID, @nVisitID, @nExamID, @dtVisitDate, @sChiefComplaint, ''@NotInjuryDate @NotSurgeryDate 
            '  , @dtInjuryDate, @dtSurgeryDate, @nClinicID

            objparam = cmd.Parameters.Add("@nChiefComplaintID", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.InputOutput
            objparam.Value = CType(arrlist.Item(0), System.Int64)

            objparam = cmd.Parameters.AddWithValue("@nPatientID", PatientID)
            objparam.Direction = ParameterDirection.Input

            objparam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CType(arrlist.Item(1), System.Int64)

            objparam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CType(arrlist.Item(2), System.Int64)

            objparam = cmd.Parameters.Add("@dtVisitDate", SqlDbType.DateTime)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CType(arrlist.Item(3), System.DateTime)


            objparam = cmd.Parameters.Add("@sChiefComplaint", SqlDbType.VarChar, 1500)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CType(arrlist.Item(4), System.String)


            objparam = cmd.Parameters.Add("@dtInjuryDate", SqlDbType.DateTime)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CType(arrlist.Item(5), System.DateTime)

            objparam = cmd.Parameters.Add("@NotInjuryDate", SqlDbType.Bit)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CType(arrlist.Item(6), System.Boolean)


            objparam = cmd.Parameters.Add("@dtSurgeryDate", SqlDbType.DateTime)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CType(arrlist.Item(7), System.DateTime)

            objparam = cmd.Parameters.Add("@NotSurgeryDate", SqlDbType.Bit)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CType(arrlist.Item(8), System.Boolean)

            objparam = cmd.Parameters.Add("@sEncounterReason", SqlDbType.VarChar, 500)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CType(arrlist.Item(9), System.String)

            objparam = cmd.Parameters.Add("@sEncounterCodeType", SqlDbType.VarChar, 20)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CType(arrlist.Item(10), System.String)

            objparam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = gnClinicID


            Conn.Open()
            ''Sandip Darade 20090303
            ''implementing audittrail
            If (cmd.ExecuteNonQuery() > 0) Then
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ChiefComplaints, gloAuditTrail.ActivityType.Add, "Chief complaints added. ", gloAuditTrail.ActivityOutCome.Success)

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ChiefComplaints, gloAuditTrail.ActivityType.Add, "Chief complaints added.", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                Dim myCheifComplaintID As Object = cmd.Parameters("@nChiefComplaintID").Value
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ChiefComplaints, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw
        Finally
            Conn.Close()
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            objparam = Nothing
        End Try

    End Sub

    ''Sandip Dardade 23 st Feb 09 
    ''to get all complaints to view from  PatientChiefComplaint table.
    Public Function GetAllComplaints() As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim sQLQuery As String = ""
        Try
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'gnPatientID replaced by 
            sQLQuery = "SELECT   (ISNULL(Patient.sFirstName,'')+ SPACE(1)+ ISNULL(Patient.sMiddleName,'') +SPACE(1)+ ISNULL(Patient.sLastName,'')) AS PatientName, " _
& "  ISNULL(PatientChiefComplaint.nChiefComplaintID,0) AS ChiefComplaintID ,ISNULL(PatientChiefComplaint.nPatientID,0) AS  PatientID, ISNULL(PatientChiefComplaint.nVisitID,0) AS VisitID, ISNULL(PatientChiefComplaint.nExamID,0) AS ExamID , " _
& "  PatientChiefComplaint.dtVisitDate , ISNULL(PatientChiefComplaint.sChiefComplaint,'') AS  ChiefComplaint, PatientChiefComplaint.dtInjuryDate, PatientChiefComplaint.dtSurgeryDate  " _
& "  FROM Patient INNER JOIN PatientChiefComplaint ON Patient.nPatientID = PatientChiefComplaint.nPatientID WHERE PatientChiefComplaint.nPatientID= " & _PatientID & " ORDER BY ChiefComplaintID "
            'end modification

            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = sQLQuery
            objCmd.Connection = objCon

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            Return dt
        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
        Return dt
    End Function

    ''Sandip Dardade 23 st Feb 09 
    ''to get all complaints to view from  PatientChiefComplaint table.
    Public Function GetComplainttoEdit(ByVal ComplaintId As Int64) As ArrayList
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        'Dim dt As DataTable = Nothing
        Dim arrlist As New ArrayList
        Dim dreader As SqlDataReader
        Dim sQLQuery As String = ""
        Try
            sQLQuery = "SELECT nChiefComplaintID,dtVisitDate,sChiefComplaint,dtInjuryDate, dtSurgeryDate,sEncounterReason,sEncounterReasonType   FROM PatientChiefComplaint  " _
                       & "WHERE  nChiefComplaintID = " & ComplaintId & " "

            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = sQLQuery
            objCmd.Connection = objCon

            objCon.Open()
            dreader = objCmd.ExecuteReader
            Do While dreader.Read()
                arrlist.Add(dreader.Item(0))
                arrlist.Add(dreader.Item(1))
                arrlist.Add(dreader.Item(2))
                arrlist.Add(dreader.Item(3))
                arrlist.Add(dreader.Item(4))
                arrlist.Add(dreader.Item(5))
                arrlist.Add(dreader.Item(6))
            Loop
            dreader.Close()
            objCon.Close()
            Return arrlist
        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            Return Nothing
        Finally
            dreader = Nothing
            arrlist = Nothing
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
         
        End Try

    End Function

    Private Sub SaveToProblemList(ByVal ProblemID As Long, ByVal ChiefComplaint As String)
        Dim lst As New myList
        Dim VisitID As Long

        ' '' To ReTrive ProblemID
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        oDB.Connect(GetConnectionString)
        Dim str As String = ""
        'lines commented by dipak 20091005 as strTempChiefComplaint move to class declearation
        'Dim strTempChiefComplaint As String
        'line commented due to strTempChiefComplaint set from frmpatientInjuryDate.vb
        ''Sanjog- added to show the Chief Complaints in Problem List
        If strTempChiefComplaint = Nothing Then
            strTempChiefComplaint = Replace(ChiefComplaint, "'", "''")
        End If
        'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'str = oDB.ExecuteQueryScaler("Select nProblemID from ProblemList where Convert(Varchar(20),dtDOS,101)= '" & Format(Now, "MM/dd/yyyy") & "' AND sCheifComplaint like '%" & strTempChiefComplaint & "%' AND nPatientID =" & gnPatientID)
        'str = oDB.ExecuteQueryScaler("Select nProblemID from ProblemList where Convert(Varchar(20),dtDOS,101)= '" & Format(Now, "MM/dd/yyyy") & "' AND sCheifComplaint like '%" & strTempChiefComplaint.Trim() & "%' AND nPatientID =" & _PatientID)
        'end modification
        Dim dtProblemlistOtherDetails As DataTable = Nothing
        dtProblemlistOtherDetails = oDB.ReadQueryDataTable("Select nProblemID,ISNULL(sExamID,'') as sExamID,ISNULL(sPrescription,'') as sPrescription from ProblemList where Convert(Varchar(20),dtDOS,101)= '" & Format(Now, "MM/dd/yyyy") & "' AND sCheifComplaint like '%" & strTempChiefComplaint.Trim() & "%' AND nPatientID =" & _PatientID)
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
        If IsNothing(dtProblemlistOtherDetails) = False AndAlso dtProblemlistOtherDetails.Rows.Count > 0 Then
            str = dtProblemlistOtherDetails.Rows(0)("nProblemID").ToString()
        End If
        If str.Trim() <> "" Then
            ProblemID = Convert.ToInt64(str)
        End If
        ' ''
        '' Please Ref. Problem List Save Code for the Properties
        With lst
            .ID = ProblemID
            .Code = ""   ''  ICD9Code
            .Description = ""  ''  Icd9Desc 
            .ParameterName = ChiefComplaint  '' Problem List Desc
            .HistoryCategory = ""  '' Prescription
            .Value = frmProblemList.Status.Active  ''  Default Status of Problems
            .Index = gnLoginID '' Current UserID            
            .VisitDate = Format(Now, "MM/dd/yyyy") '' Date Of Service
            .HistoryItem = GenerateVisitID(.VisitDate, _PatientID) '' VisitID
            VisitID = .HistoryItem
            If IsNothing(dtProblemlistOtherDetails) = False AndAlso dtProblemlistOtherDetails.Rows.Count > 0 Then
                .ExamID = dtProblemlistOtherDetails.Rows(0)("sExamID").ToString()
                .HistoryCategory = dtProblemlistOtherDetails.Rows(0)("sPrescription").ToString()
            End If
            If IsNothing(dtProblemlistOtherDetails) = False Then
                dtProblemlistOtherDetails.Dispose()
                dtProblemlistOtherDetails = Nothing
            End If
        End With
        Dim ProblemArrlist As New ArrayList
        ProblemArrlist.Add(lst)
        lst = Nothing
        Dim objProbllemList As New clsPatientProblemList
        'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'objProbllemList.SaveProblemList(gnPatientID, VisitID, ProblemArrlist)
        objProbllemList.SaveProblemList(_PatientID, VisitID, ProblemArrlist)
        'end modification
        ProblemArrlist = Nothing
        objProbllemList.Dispose()
        objProbllemList = Nothing

    End Sub
    Private disposed As Boolean = False

    Public Sub Dispose() Implements System.IDisposable.Dispose
        'Disconnect();
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                If Not IsNothing(Conn) Then
                    Conn.Dispose()
                    Conn = Nothing
                End If
            End If
        End If
        disposed = True
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


End Class
