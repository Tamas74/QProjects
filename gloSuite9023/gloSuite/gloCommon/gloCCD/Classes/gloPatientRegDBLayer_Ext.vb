Imports System.Data.SqlClient
Imports System.Windows.Forms
Partial Public Class gloPatientRegDBLayer
    Implements IDisposable
#Region "Constructor & Destructor"

    Private _databaseconnectionstring As String = ""
    Private _ClinicID As Int64 = 1
    Private _messageBoxCaption As String = "gloEMR"
    Private _PatientID As Int64 = 0
    Private conn As SqlConnection = Nothing


    Public Function Initialize(ByVal DatabaseConnectionString As String) As Boolean
        _databaseconnectionstring = DatabaseConnectionString
        If (IsNothing(conn) = False) Then
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
        End If
        conn = New SqlConnection(_databaseconnectionstring)
        Return True
    End Function
    Public Sub New(ByVal DatabaseConnectionString As String)
        _databaseconnectionstring = DatabaseConnectionString
        If (IsNothing(conn) = False) Then
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
        End If
        conn = New SqlConnection(_databaseconnectionstring)
    End Sub

    Public Sub New()

    End Sub

    Private disposed As Boolean = False

    Public Overridable Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        ' This object will be cleaned up by the Dispose method.
        ' Therefore, you should call GC.SupressFinalize to
        ' take this object off the finalization queue 
        ' and prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    'Public Sub Dispose()
    '    Dispose(True)
    '    GC.SuppressFinalize(Me)
    'End Sub
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                If (IsNothing(conn) = False) Then
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    conn.Dispose()
                    conn = Nothing
                End If
            End If
        End If
        disposed = True
    End Sub
    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

#End Region

    Public Function Register_PatientDemographics(ByVal oPatient As gloCCDLibrary.Patient) As Int64
        Dim _PatientID As Int64
        ' Dim conn As New SqlConnection
        Dim _CCDID As Int64 = 0
        Dim _Language As String = ""
        Dim objCCDDatabaseLayer As gloCCDDatabaseLayer = Nothing
        Dim ogloPatient As gloPatient.gloPatient = New gloPatient.gloPatient(gloLibCCDGeneral.Connectionstring)
        Try

            If Not IsNothing(oPatient) Then
                _PatientID = ogloPatient.Add(oPatient.PatientDemographics)
            End If

            'Code Start-Added by kanchan on 20101011 for Modular CCD Rendering & save
            'Add Race,Ethnicity & Language in Category_Mst,if not exists
            objCCDDatabaseLayer = New gloCCDDatabaseLayer()
            'Code Start Added by kanchan on 20101113 to save patient code as external code
            If oPatient.PatientDemographics.DemographicsDetail.PatientExternalCode <> "" Then
                objCCDDatabaseLayer.UpdatePatientExternalCode(_PatientID, oPatient.PatientDemographics.DemographicsDetail.PatientExternalCode)
            End If
            If oPatient.Race <> "" Then
                objCCDDatabaseLayer.UpdateCategoryMaster(oPatient.Race, "Race", _PatientID)
            End If
            If oPatient.ethnicGroupCode <> "" Then
                objCCDDatabaseLayer.UpdateCategoryMaster(oPatient.ethnicGroupCode, "Ethnicity", _PatientID)
            End If
            If Not IsNothing(oPatient.PatientLanguages) AndAlso oPatient.PatientLanguages.Count > 0 Then
                If Not IsNothing(oPatient.PatientLanguages.Item(0).Language) Then
                    _Language = oPatient.PatientLanguages.Item(0).Language
                    If _Language <> "" Then
                        objCCDDatabaseLayer.UpdateCategoryMaster(_Language, "Language", _PatientID)
                    End If
                End If
            End If

            Return _PatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return 0
        Finally
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            'conn.Dispose()
            'conn = Nothing
            _Language = Nothing
            If Not IsNothing(objCCDDatabaseLayer) Then
                objCCDDatabaseLayer.Dispose()
            End If
            If Not IsNothing(ogloPatient) Then
                ogloPatient.Dispose()
                ogloPatient = Nothing
            End If
        End Try

    End Function

    Public Function SavePatientAccount(ByVal PatientID As Int64) As Boolean

        Dim cmd As SqlCommand = Nothing
        Dim conn As SqlConnection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
        ' conn.ConnectionString = gloLibCCDGeneral.Connectionstring
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("PA_Accounts_CreateAccounts ", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt) 'nVitalID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID

            If conn.State = ConnectionState.Closed Then conn.Open()
            cmd.ExecuteNonQuery()

            Return True
        Catch ex As SqlException
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally


            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If conn.State = ConnectionState.Open Then conn.Close()
            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    Public Function SaveReconcilationProblemLists(ByVal oReconcileList As ReconcileList) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim nListID As Int64
        If (IsNothing(conn) = False) Then
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
        End If
        conn = New SqlConnection(_databaseconnectionstring)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trReconcile As SqlTransaction
        trReconcile = conn.BeginTransaction
        Dim ExamParam As SqlParameter = Nothing
        Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(gloLibCCDGeneral.Connectionstring)
        Try



            cmd = New SqlCommand("CCD_Reconcile_ListMst", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trReconcile

            ExamParam = cmd.Parameters.AddWithValue("@nListID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = 0

            ExamParam = cmd.Parameters.AddWithValue("@nCCDID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = oReconcileList.CCDID

            ExamParam = cmd.Parameters.AddWithValue("@nPatientID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = oReconcileList.PatientID

            ExamParam = cmd.Parameters.Add("@sSourceName", SqlDbType.VarChar, 255)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(oReconcileList.SourceName) Then
                ExamParam.Value = oReconcileList.SourceName
            Else
                ExamParam.Value = ""
            End If



            ExamParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar, 50)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(oReconcileList.UserName) Then
                ExamParam.Value = oReconcileList.UserName
            Else
                ExamParam.Value = ""
            End If
            Dim _listName As String = ""
            _listName = oReconcileList.ListName
            Dim _IsExists As Boolean
            Dim i As Integer = 0
            _IsExists = ogloPatientRegDBLayer.CheckListNameExists(_listName)
            Dim _tempname As String = ""
            Do While _IsExists
                i += 1
                _tempname = _listName & "-" & i

                _IsExists = ogloPatientRegDBLayer.CheckListNameExists(_tempname)
                If _IsExists = False Then
                    _listName = _tempname
                    Exit Do
                End If
            Loop


            ExamParam = cmd.Parameters.Add("@sListName", SqlDbType.VarChar, 255)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(_listName) Then
                ExamParam.Value = _listName
            Else
                ExamParam.Value = ""
            End If

            ExamParam = cmd.Parameters.Add("@nStatus", SqlDbType.SmallInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = oReconcileList.Status


            ExamParam = cmd.Parameters.Add("@sListType", SqlDbType.VarChar)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = "Problem"


            ExamParam = cmd.Parameters.Add("@Id", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.InputOutput
            ExamParam.Value = 0

            If conn.State = ConnectionState.Closed Then conn.Open()
            cmd.ExecuteNonQuery()
            nListID = DirectCast(ExamParam.Value, Int64)

            If IsNothing(ExamParam) = False Then
                ExamParam = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If oReconcileList.NoKnownProblems Then
                cmd = New SqlCommand("CCD_Reconcile_InUpProblemList", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trReconcile
                'ogloProblem.EncounterId = GenerateVisitID(ogloProblem.DateOfService, PatientID)


                ExamParam = cmd.Parameters.AddWithValue("@nListID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = nListID

                ExamParam = cmd.Parameters.AddWithValue("@nPatientID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = oReconcileList.PatientID


                ExamParam = cmd.Parameters.Add("@DOS", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input

                ExamParam.Value = Format(Convert.ToDateTime(DateTime.Now), "MM/dd/yyyy")



                ExamParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""



                ExamParam = cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input

                ExamParam.Value = ""



                ExamParam = cmd.Parameters.Add("@CheifComplaint", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input

                ExamParam.Value = ""





                ExamParam = cmd.Parameters.Add("@ProblemStatus", SqlDbType.Int)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = 0

                ExamParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = oReconcileList.UserID


                ExamParam = cmd.Parameters.Add("@ProblemID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.InputOutput
                ExamParam.Value = 0


                ExamParam = cmd.Parameters.Add("@nImmediacy", SqlDbType.Int)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = 0



                ExamParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = oReconcileList.UserName

                ExamParam = cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input

                ExamParam.Value = ""


                ExamParam = cmd.Parameters.Add("@sSourceName", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(oReconcileList.SourceName) Then
                    ExamParam.Value = oReconcileList.SourceName
                Else
                    ExamParam.Value = ""
                End If
                ExamParam = cmd.Parameters.Add("@ResolveDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input

                ExamParam.Value = DBNull.Value


                If IsDate(oReconcileList.LastModifiedDateTime) Then
                    If oReconcileList.LastModifiedDateTime <> "12:00:00 AM" Then
                        ExamParam = cmd.Parameters.Add("@dtLastUpdated", SqlDbType.DateTime)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = oReconcileList.LastModifiedDateTime
                    End If
                End If

                ExamParam = cmd.Parameters.Add("@dtConcernStartDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input

                ExamParam.Value = DBNull.Value


                ExamParam = cmd.Parameters.Add("@dtConcernEndDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input

                ExamParam.Value = DBNull.Value

                ExamParam = cmd.Parameters.Add("@sConcernStatus", SqlDbType.VarChar, 200)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sNoKnownDesc", SqlDbType.VarChar, 200)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = "No Known Problems"

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(ExamParam) = False Then
                    ExamParam = Nothing
                End If
            End If

            For Each ogloProblem As Problems In oReconcileList.mPatient.PatientProblems
                cmd = New SqlCommand("CCD_Reconcile_InUpProblemList", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trReconcile
                'ogloProblem.EncounterId = GenerateVisitID(ogloProblem.DateOfService, PatientID)


                ExamParam = cmd.Parameters.AddWithValue("@nListID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = nListID

                ExamParam = cmd.Parameters.AddWithValue("@nPatientID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = oReconcileList.PatientID


                ExamParam = cmd.Parameters.Add("@DOS", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.DateOfService) AndAlso ogloProblem.DateOfService <> "" Then
                    ExamParam.Value = Format(Convert.ToDateTime(ogloProblem.DateOfService), "MM/dd/yyyy")
                Else
                    ExamParam.Value = Format(Convert.ToDateTime(DateTime.Now), "MM/dd/yyyy")
                End If


                ExamParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ICD9Code) Then
                    ExamParam.Value = ogloProblem.ICD9Code
                Else
                    ExamParam.Value = ""
                End If


                ExamParam = cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ICD9) Then
                    ExamParam.Value = ogloProblem.ICD9
                Else
                    ExamParam.Value = ""
                End If


                ExamParam = cmd.Parameters.Add("@CheifComplaint", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.Condition) AndAlso ogloProblem.Condition.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.Condition
                Else
                    ExamParam.Value = ""
                End If




                ExamParam = cmd.Parameters.Add("@ProblemStatus", SqlDbType.Int)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.ProblemStatus

                ExamParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = oReconcileList.UserID


                ExamParam = cmd.Parameters.Add("@ProblemID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.InputOutput
                ExamParam.Value = 0


                ExamParam = cmd.Parameters.Add("@nImmediacy", SqlDbType.Int)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.Immediacy



                ExamParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = oReconcileList.UserName

                ExamParam = cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ConceptID) AndAlso ogloProblem.ConceptID.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.ConceptID
                Else
                    ExamParam.Value = ""
                End If

                ExamParam = cmd.Parameters.Add("@sSourceName", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(oReconcileList.SourceName) Then
                    ExamParam.Value = oReconcileList.SourceName
                Else
                    ExamParam.Value = ""
                End If
                ExamParam = cmd.Parameters.Add("@ResolveDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ResolvedDate) AndAlso ogloProblem.ResolvedDate <> "" Then
                    ExamParam.Value = Format(Convert.ToDateTime(ogloProblem.ResolvedDate), "MM/dd/yyyy")
                Else
                    ExamParam.Value = DBNull.Value
                End If

                If IsDate(oReconcileList.LastModifiedDateTime) Then
                    If oReconcileList.LastModifiedDateTime <> "12:00:00 AM" Then
                        ExamParam = cmd.Parameters.Add("@dtLastUpdated", SqlDbType.DateTime)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = oReconcileList.LastModifiedDateTime
                    End If
                End If

                ExamParam = cmd.Parameters.Add("@dtConcernStartDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ConcernStartDate) AndAlso ogloProblem.ConcernStartDate <> "" Then
                    ExamParam.Value = Format(Convert.ToDateTime(ogloProblem.ConcernStartDate), "MM/dd/yyyy")
                Else
                    ExamParam.Value = DBNull.Value
                End If

                ExamParam = cmd.Parameters.Add("@dtConcernEndDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ConcernEndDate) AndAlso ogloProblem.ConcernEndDate <> "" Then
                    ExamParam.Value = Format(Convert.ToDateTime(ogloProblem.ConcernEndDate), "MM/dd/yyyy")
                Else
                    ExamParam.Value = DBNull.Value
                End If
                ExamParam = cmd.Parameters.Add("@sConcernStatus", SqlDbType.VarChar, 200)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.sConcernStatus) Then
                    ExamParam.Value = ogloProblem.sConcernStatus
                Else
                    ExamParam.Value = ""
                End If
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(ExamParam) = False Then
                    ExamParam = Nothing
                End If
                'If IsNothing(ogloProblem) = False Then
                '    ogloProblem.Dispose()
                '    ogloProblem = Nothing
                'End If
            Next

            trReconcile.Commit()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            trReconcile.Rollback()
            ex = Nothing
            Return False
        Finally
            If IsNothing(ogloPatientRegDBLayer) = False Then
                ogloPatientRegDBLayer.Dispose()
                ogloPatientRegDBLayer = Nothing
            End If

            If conn.State = ConnectionState.Open Then conn.Close()
            'If IsNothing(conn) = False Then:SLR:Don't free since it is used in many places??
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            If IsNothing(ExamParam) = False Then
                ExamParam = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(trReconcile) = False) Then
                
                trReconcile.Dispose()
                trReconcile = Nothing
            End If
        End Try

    End Function

    Public Function SaveReconcilationMedicationLists(ByVal oReconcileList As ReconcileList) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim _cntDrug As Integer = 0
        Dim nListID As Int64
        Dim strRxNormCode As String = ""
        Dim drugName As String = ""
        Dim trReconcile As SqlTransaction
        If (IsNothing(conn) = False) Then
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
        End If
        conn = New SqlConnection(_databaseconnectionstring)
        If conn.State = ConnectionState.Closed Then conn.Open()
        trReconcile = conn.BeginTransaction
        Dim objgloCCDDatabaselayer As gloCCDDatabaseLayer = Nothing
        Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(gloLibCCDGeneral.Connectionstring)
        Dim sqlParam As SqlParameter = Nothing
        Dim ExamParam As SqlParameter = Nothing
        Try



            cmd = New SqlCommand("CCD_Reconcile_ListMst", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trReconcile

            ExamParam = cmd.Parameters.AddWithValue("@nListID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = 0

            ExamParam = cmd.Parameters.AddWithValue("@nCCDID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = oReconcileList.CCDID

            ExamParam = cmd.Parameters.AddWithValue("@nPatientID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = oReconcileList.PatientID

            ExamParam = cmd.Parameters.Add("@sSourceName", SqlDbType.VarChar, 255)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(oReconcileList.SourceName) Then
                ExamParam.Value = oReconcileList.SourceName
            Else
                ExamParam.Value = ""
            End If



            ExamParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar, 50)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(oReconcileList.UserName) Then
                ExamParam.Value = oReconcileList.UserName
            Else
                ExamParam.Value = ""
            End If
            Dim _listName As String = ""
            _listName = oReconcileList.ListName
            Dim _IsExists As Boolean
            Dim i As Integer = 0
            _IsExists = ogloPatientRegDBLayer.CheckListNameExists(_listName)
            Dim _tempname As String = ""
            Do While _IsExists And i < Integer.MaxValue
                i += 1
                _tempname = _listName & "-" & i

                _IsExists = ogloPatientRegDBLayer.CheckListNameExists(_tempname)
                If _IsExists = False Then
                    _listName = _tempname
                    Exit Do
                End If
            Loop

            ExamParam = cmd.Parameters.Add("@sListName", SqlDbType.VarChar, 255)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(_listName) Then
                ExamParam.Value = _listName
            Else
                ExamParam.Value = ""
            End If

            ExamParam = cmd.Parameters.Add("@nStatus", SqlDbType.SmallInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = oReconcileList.Status


            ExamParam = cmd.Parameters.Add("@sListType", SqlDbType.VarChar)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = "Medication"


            ExamParam = cmd.Parameters.Add("@Id", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.InputOutput
            ExamParam.Value = 0

            If conn.State = ConnectionState.Closed Then conn.Open()
            cmd.ExecuteNonQuery()
            nListID = DirectCast(ExamParam.Value, Int64)
            If IsNothing(ExamParam) = False Then
                ExamParam = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If oReconcileList.NoKnownMedication Then
                objgloCCDDatabaselayer = New gloCCDDatabaseLayer
                strRxNormCode = ""
                Dim strNDCCode As String = ""
                cmd = New SqlCommand("CCD_Reconcile_InUpMedicationList", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trReconcile

                sqlParam = cmd.Parameters.AddWithValue("@nListID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nListID

                sqlParam = cmd.Parameters.Add("@nMedicationID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = oReconcileList.PatientID

                sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sMedication", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                sqlParam = cmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                sqlParam = cmd.Parameters.Add("@dtStartdate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@sAmount", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@dtEnddate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                If IsDate(oReconcileList.LastModifiedDateTime) Then
                    If oReconcileList.LastModifiedDateTime <> "12:00:00 AM" Then
                        sqlParam = cmd.Parameters.Add("@dtMedicationDate", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oReconcileList.LastModifiedDateTime
                    End If
                End If

                sqlParam = cmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = oReconcileList.UserName


                sqlParam = cmd.Parameters.Add("@sStrengthUnit", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""


                sqlParam = cmd.Parameters.Add("@Rx_sRefills", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""



                sqlParam = cmd.Parameters.Add("@sSourceName", SqlDbType.VarChar, 255)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oReconcileList.SourceName) Then
                    sqlParam.Value = oReconcileList.SourceName
                Else
                    sqlParam.Value = ""
                End If
                sqlParam = cmd.Parameters.Add("@sDrugForm", SqlDbType.VarChar, 50)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = ""


                sqlParam = cmd.Parameters.Add("@Rx_PrescriberNotes", SqlDbType.VarChar, 1500)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = ""


                sqlParam = cmd.Parameters.Add("@sRXNorms", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = ""


                sqlParam = cmd.Parameters.Add("@sGenericName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sNoKnownDesc", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = "No Known Medication"


                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If IsNothing(sqlParam) = False Then
                    sqlParam = Nothing
                End If
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

                If IsNothing(objgloCCDDatabaselayer) = False Then
                    objgloCCDDatabaselayer.Dispose()
                    objgloCCDDatabaselayer = Nothing
                End If



            End If

            For Each ogloMedication As Medication In oReconcileList.mPatient.PatientMedications
                objgloCCDDatabaselayer = New gloCCDDatabaseLayer
                strRxNormCode = ""
                Dim strNDCCode As String = ""
                cmd = New SqlCommand("CCD_Reconcile_InUpMedicationList", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trReconcile

                sqlParam = cmd.Parameters.AddWithValue("@nListID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nListID

                sqlParam = cmd.Parameters.Add("@nMedicationID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = oReconcileList.PatientID

                sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.RxNormCode) OrElse Not IsNothing(ogloMedication.ProdCode) Then
                    ' Dim oRxBusinesslayer As gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer = New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(oReconcileList.PatientID)
                    Dim objCCDDatabaseLayer As New gloCCDDatabaseLayer

                    Dim _result As gloGlobal.DIB.DrugInfo = Nothing

                    If Convert.ToString(ogloMedication.ProdCode) <> "" Then
                        Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                            _result = oGSHelper.GetNdccodebyRxnorm(ogloMedication.ProdCode, 0)
                        End Using
                    End If
                    If Convert.ToString(ogloMedication.RxNormCode) <> "" Then
                        If _result Is Nothing OrElse IsNothing(_result.ndc) Then
                            Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                                _result = oGSHelper.GetNdccodebyRxnorm(ogloMedication.RxNormCode, 1)
                            End Using
                        End If
                    End If
                    If IsNothing(_result) = False Then
                        '_cntDrug = _cntDrug + 1

                        If Not IsNothing(_result.ndc) Then
                            _cntDrug = _cntDrug + 1
                            sqlParam.Value = _result.ndc.ToString() '_result.Rows(0)("NDCCode").ToString()
                            strNDCCode = _result.ndc.ToString()
                        End If

                        If Not IsNothing(_result.gnm) Then
                            _cntDrug = _cntDrug + 1
                            ogloMedication.GenericName = _result.gnm.ToString()
                        End If

                        If Not IsNothing(_result.dnm) Then
                            _cntDrug = _cntDrug + 1
                            ogloMedication.DrugName = _result.dnm.ToString()
                        End If

                        If sqlParam.Value = "" Then
                            'MessageBox.Show("No NDC code was found for " & ogloMedication.DrugName & ". It will not be added to medication history.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If drugName = "" Then
                                drugName = vbNewLine & ogloMedication.DrugName
                            Else
                                drugName = drugName & vbNewLine & ogloMedication.DrugName
                            End If
                            If IsNothing(cmd) = False Then
                                cmd.Parameters.Clear()
                                cmd.Dispose()
                                cmd = Nothing
                            End If

                            If IsNothing(objgloCCDDatabaselayer) = False Then
                                objgloCCDDatabaselayer.Dispose()
                                objgloCCDDatabaselayer = Nothing
                            End If
                            Continue For
                        End If
                    Else
                        If drugName = "" Then
                            drugName = vbNewLine & ogloMedication.DrugName
                        Else
                            drugName = drugName & vbNewLine & ogloMedication.DrugName
                        End If
                        If IsNothing(cmd) = False Then
                            cmd.Parameters.Clear()
                            cmd.Dispose()
                            cmd = Nothing
                        End If

                        If IsNothing(objgloCCDDatabaselayer) = False Then
                            objgloCCDDatabaselayer.Dispose()
                            objgloCCDDatabaselayer = Nothing
                        End If
                        Continue For

                    End If

                    'If IsNothing(objCCDDatabaseLayer) = False Then
                    '    objCCDDatabaseLayer.Dispose()
                    '    objCCDDatabaseLayer = Nothing
                    'End If
                Else
                    If drugName = "" Then
                        drugName = vbNewLine & ogloMedication.DrugName
                    Else
                        drugName = drugName & vbNewLine & ogloMedication.DrugName
                    End If
                    If IsNothing(cmd) = False Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If

                    If IsNothing(objgloCCDDatabaselayer) = False Then
                        objgloCCDDatabaselayer.Dispose()
                        objgloCCDDatabaselayer = Nothing
                    End If
                    Continue For
                End If
                sqlParam = cmd.Parameters.Add("@sMedication", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.DrugName) Then
                    sqlParam.Value = ogloMedication.DrugName
                    If sqlParam.Value = "" Then
                        MessageBox.Show("Since there is no drug details available, therefore this will be not added in medication", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If IsNothing(cmd) = False Then
                            cmd.Parameters.Clear()
                            cmd.Dispose()
                            cmd = Nothing
                        End If

                        If IsNothing(objgloCCDDatabaselayer) = False Then
                            objgloCCDDatabaselayer.Dispose()
                            objgloCCDDatabaselayer = Nothing
                        End If
                        Continue For
                    End If

                Else
                    MessageBox.Show("Since there is no drug details available, therefore this will be not added in medication", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If IsNothing(cmd) = False Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If

                    If IsNothing(objgloCCDDatabaselayer) = False Then
                        objgloCCDDatabaselayer.Dispose()
                        objgloCCDDatabaselayer = Nothing
                    End If
                    Continue For
                End If

                sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                'If Not IsNothing(ogloMedication.DrugStrength) Then
                '    sqlParam.Value = ogloMedication.DrugStrength
                'Else
                '    sqlParam.Value = ""
                'End If
                Dim ogloRecon As gloReconciliation = New gloReconciliation()
                If Not IsNothing(strNDCCode) Then
                    sqlParam.Value = gloReconciliation.GetDosageForm(strNDCCode)
                Else
                    sqlParam.Value = ""
                End If
                ogloRecon.Dispose()

                sqlParam = cmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Route) Then
                    sqlParam.Value = ogloMedication.Route
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Frequency) Then
                    sqlParam.Value = ogloMedication.Frequency
                Else
                    sqlParam.Value = ""
                End If


                sqlParam = cmd.Parameters.Add("@dtStartdate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.StartDate) Then
                    If (ogloMedication.StartDate) <> "" Then
                        sqlParam.Value = Format(CDate(ogloMedication.StartDate), "MM/dd/yyyy")
                    Else
                        sqlParam.Value = System.DBNull.Value
                    End If
                Else
                    sqlParam.Value = System.DBNull.Value

                End If
                sqlParam = cmd.Parameters.Add("@sAmount", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.DrugQuantity) Then
                    sqlParam.Value = ogloMedication.DrugQuantity
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@dtEnddate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.EndDate) Then
                    If (ogloMedication.EndDate) <> "" Then
                        sqlParam.Value = Format(CDate(ogloMedication.EndDate), "MM/dd/yyyy")
                    Else
                        sqlParam.Value = System.DBNull.Value
                    End If
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                If IsDate(oReconcileList.LastModifiedDateTime) Then
                    If oReconcileList.LastModifiedDateTime <> "12:00:00 AM" Then
                        sqlParam = cmd.Parameters.Add("@dtMedicationDate", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oReconcileList.LastModifiedDateTime
                    End If
                End If
                'If Not IsNothing(ogloMedication.MedicationDate) Then
                '    If (ogloMedication.MedicationDate) <> "" Then
                '        sqlParam.Value = Format(CDate(ogloMedication.MedicationDate), "MM/dd/yyyy")
                '    Else
                '        sqlParam.Value = DateTime.Now
                '    End If
                'Else
                '    sqlParam.Value = DateTime.Now
                'End If



                If Not IsNothing(ogloMedication.Status) Then
                    sqlParam = cmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ogloMedication.Status
                Else
                    sqlParam = cmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""

                End If


                sqlParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = oReconcileList.UserName


                sqlParam = cmd.Parameters.Add("@sStrengthUnit", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.StrengthUnits) Then
                    sqlParam.Value = ogloMedication.StrengthUnits
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_sRefills", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Refills) Then
                    sqlParam.Value = ogloMedication.Refills
                Else
                    sqlParam.Value = ""
                End If


                sqlParam = cmd.Parameters.Add("@sSourceName", SqlDbType.VarChar, 255)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oReconcileList.SourceName) Then
                    sqlParam.Value = oReconcileList.SourceName
                Else
                    sqlParam.Value = ""
                End If
                sqlParam = cmd.Parameters.Add("@sDrugForm", SqlDbType.VarChar, 50)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.DrugForm) Then
                    sqlParam.Value = ogloMedication.DrugForm
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_PrescriberNotes", SqlDbType.VarChar, 1500)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Rx_PrescriberNotes) Then
                    sqlParam.Value = ogloMedication.Rx_PrescriberNotes
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sRXNorms", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.RxNormCode) Then
                    sqlParam.Value = ogloMedication.RxNormCode
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sGenericName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.GenericName) Then
                    sqlParam.Value = ogloMedication.GenericName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sNoKnownDesc", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If IsNothing(sqlParam) = False Then
                    sqlParam = Nothing
                End If
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

                If IsNothing(objgloCCDDatabaselayer) = False Then
                    objgloCCDDatabaselayer.Dispose()
                    objgloCCDDatabaselayer = Nothing
                End If
                'If IsNothing(ogloMedication) = False Then
                '    ogloMedication.Dispose()
                '    ogloMedication = Nothing
                'End If

            Next


            If drugName.Trim <> "" Then
                System.Windows.Forms.MessageBox.Show("No NDC codes were found for: " & drugName & "." & vbNewLine & "These will not be added to medication history.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If _cntDrug > 0 Or oReconcileList.NoKnownMedication = True Then
                trReconcile.Commit()
            Else
                trReconcile.Rollback()
                Return False
            End If
            Return True
        Catch ex As Exception

            trReconcile.Rollback()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return False
        Finally
            strRxNormCode = Nothing
            drugName = Nothing
            If IsNothing(ogloPatientRegDBLayer) = False Then
                ogloPatientRegDBLayer.Dispose()
                ogloPatientRegDBLayer = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
            If IsNothing(ExamParam) = False Then
                ExamParam = Nothing
            End If

            If conn.State = ConnectionState.Open Then conn.Close()
            'If IsNothing(conn) = False Then: SLR :Don't dispose it here, it is used in many places
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(objgloCCDDatabaselayer) = False Then
                objgloCCDDatabaselayer.Dispose()
                objgloCCDDatabaselayer = Nothing
            End If
            If IsNothing(trReconcile) = False Then
                trReconcile.Dispose()
                trReconcile = Nothing
            End If
        End Try

    End Function

    Public Function SaveReconcilationHistoryLists(ByVal oReconcileList As ReconcileList) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim nListID As Int64
        If (IsNothing(conn) = False) Then
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
        End If
        conn = New SqlConnection(_databaseconnectionstring)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trReconcile As SqlTransaction
        trReconcile = conn.BeginTransaction
        Dim ExamParam As SqlParameter = Nothing
        Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(gloLibCCDGeneral.Connectionstring)
        Dim _TempHistoryID As Long = 0

        Try



            cmd = New SqlCommand("CCD_Reconcile_ListMst", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trReconcile

            ExamParam = cmd.Parameters.AddWithValue("@nListID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = 0

            ExamParam = cmd.Parameters.AddWithValue("@nCCDID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = oReconcileList.CCDID

            ExamParam = cmd.Parameters.AddWithValue("@nPatientID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = oReconcileList.PatientID

            ExamParam = cmd.Parameters.Add("@sSourceName", SqlDbType.VarChar, 255)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(oReconcileList.SourceName) Then
                ExamParam.Value = oReconcileList.SourceName
            Else
                ExamParam.Value = ""
            End If



            ExamParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar, 50)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(oReconcileList.UserName) Then
                ExamParam.Value = oReconcileList.UserName
            Else
                ExamParam.Value = ""
            End If
            ' oReconcileList.ListName = oReconcileList.ListName & "_Allergy"
            Dim _listName As String = ""
            _listName = oReconcileList.ListName
            Dim _IsExists As Boolean
            Dim i As Integer = 0
            _IsExists = ogloPatientRegDBLayer.CheckListNameExists(_listName)
            Dim _tempname As String = ""
            Do While _IsExists And i < Integer.MaxValue
                i += 1
                _tempname = _listName & "-" & i

                _IsExists = ogloPatientRegDBLayer.CheckListNameExists(_tempname)
                If _IsExists = False Then
                    _listName = _tempname
                    Exit Do
                End If
            Loop


            ExamParam = cmd.Parameters.Add("@sListName", SqlDbType.VarChar, 255)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(_listName) Then
                ExamParam.Value = _listName
            Else
                ExamParam.Value = ""
            End If

            ExamParam = cmd.Parameters.Add("@nStatus", SqlDbType.SmallInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = oReconcileList.Status


            ExamParam = cmd.Parameters.Add("@sListType", SqlDbType.VarChar)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = "Allergy"


            ExamParam = cmd.Parameters.Add("@Id", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.InputOutput
            ExamParam.Value = 0

            If conn.State = ConnectionState.Closed Then conn.Open()
            cmd.ExecuteNonQuery()
            nListID = DirectCast(ExamParam.Value, Int64)

            If IsNothing(ExamParam) = False Then
                ExamParam = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            Dim k As Int64 = 1
            _TempHistoryID = 0

            Dim sqlParam As SqlParameter
            If oReconcileList.NoKnownallergies Then
                cmd = New SqlCommand("CCD_Reconcile_InUpHistoryList", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trReconcile



                'Dim Category_id As Int64 = GetCategoryID(oPatientHistory.HistoryCategory, "History")
                'If VerifyHistoryItemAvailability(oPatientHistory.HistoryItem, Category_id) = False Then
                '    AddHistoryItemData(oPatientHistory.HistoryItem, "", Category_id)
                'End If
                'oPatientHistory.VisitID = GenerateVisitID(oPatientHistory.DOEAllergy, PatientID)


                sqlParam = cmd.Parameters.AddWithValue("@nListID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nListID

                sqlParam = cmd.Parameters.AddWithValue("@HistoryID", 0)
                sqlParam.Direction = ParameterDirection.Input


                sqlParam = cmd.Parameters.AddWithValue("@PatientID", oReconcileList.PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.Add("@HistoryCategory", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = "Allergies"

                sqlParam = cmd.Parameters.Add("@HistoryItem", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""  ''HistoryItem




                sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""



                sqlParam = cmd.Parameters.Add("@TempHistoryID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.InputOutput
                sqlParam.Value = _TempHistoryID  '' c


                'For Deormalization of History table
                'DrugName

                sqlParam = cmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""




                'NDCCode

                sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""





                sqlParam = cmd.Parameters.Add("@DOEOAllergy", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = DateTime.Now



                sqlParam = cmd.Parameters.Add("@ConceptID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""





                sqlParam = cmd.Parameters.Add("@sICD9", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""




                sqlParam = cmd.Parameters.Add("@sRxNormID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""


                sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = oReconcileList.UserName


                sqlParam = cmd.Parameters.Add("@sSourceName", SqlDbType.VarChar, 255)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oReconcileList.SourceName) Then
                    sqlParam.Value = oReconcileList.SourceName
                Else
                    sqlParam.Value = ""
                End If

                If IsDate(oReconcileList.LastModifiedDateTime) Then
                    If oReconcileList.LastModifiedDateTime <> "12:00:00 AM" Then
                        sqlParam = cmd.Parameters.Add("@dtLastUpdated", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oReconcileList.LastModifiedDateTime
                    End If
                End If

                sqlParam = cmd.Parameters.Add("@OnsetDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = DBNull.Value


                sqlParam = cmd.Parameters.Add("@dtConcernEndDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = DBNull.Value

                sqlParam = cmd.Parameters.Add("@dtObservationEndDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = DBNull.Value


                sqlParam = cmd.Parameters.Add("@sConcernStatus", SqlDbType.VarChar, 200)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = ""


                sqlParam = cmd.Parameters.Add("@sSeverityCode", SqlDbType.VarChar, 200)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = ""


                sqlParam = cmd.Parameters.Add("@sSeverity", SqlDbType.VarChar, 200)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sNoKnownDesc", SqlDbType.VarChar, 500)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = "No Known Allergies"


                Try
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    _TempHistoryID = cmd.Parameters("@TempHistoryID").Value


                    'If oPatientHistory.Reaction <> "" Then
                    '    Dim objCCDDatabaseLayer As New gloCCDDatabaseLayer
                    '    objCCDDatabaseLayer.UpdateCategoryMaster(oPatientHistory.Reaction, "Reaction", oReconcileList.PatientID)
                    '    If Not IsNothing(objCCDDatabaseLayer) Then
                    '        objCCDDatabaseLayer.Dispose()
                    '    End If
                    'End If

                Catch ex As Exception
                    Throw ex
                End Try
                'cmd.Parameters.Clear()
                If IsNothing(sqlParam) = False Then
                    sqlParam = Nothing
                End If
                If IsNothing(ExamParam) = False Then
                    ExamParam = Nothing
                End If

                k = k + 1
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
            End If

            For Each oPatientHistory As gloPatientHistory In oReconcileList.mPatient.PatientHistory

                cmd = New SqlCommand("CCD_Reconcile_InUpHistoryList", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trReconcile



                'Dim Category_id As Int64 = GetCategoryID(oPatientHistory.HistoryCategory, "History")
                'If VerifyHistoryItemAvailability(oPatientHistory.HistoryItem, Category_id) = False Then
                '    AddHistoryItemData(oPatientHistory.HistoryItem, "", Category_id)
                'End If
                'oPatientHistory.VisitID = GenerateVisitID(oPatientHistory.DOEAllergy, PatientID)
                oPatientHistory.VisitID = 0

                If IsNothing(oPatientHistory.HistoryItem) OrElse oPatientHistory.HistoryItem = "" Then
                    If IsNothing(cmd) = False Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If
                    Continue For
                End If

                sqlParam = cmd.Parameters.AddWithValue("@nListID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nListID

                sqlParam = cmd.Parameters.AddWithValue("@HistoryID", 0)
                sqlParam.Direction = ParameterDirection.Input


                sqlParam = cmd.Parameters.AddWithValue("@PatientID", oReconcileList.PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.Add("@HistoryCategory", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = oPatientHistory.HistoryCategory  ''HistoryTable.HistoryCategory

                sqlParam = cmd.Parameters.Add("@HistoryItem", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = oPatientHistory.HistoryItem   ''HistoryItem



                If Not IsNothing(oPatientHistory.Reaction) Then
                    If IsNothing(oPatientHistory.Status) Then
                        oPatientHistory.Status = "Active"
                    End If
                    sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.Reaction & "|" & oPatientHistory.Status
                Else
                    sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = "|Active"
                End If


                sqlParam = cmd.Parameters.Add("@TempHistoryID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.InputOutput
                sqlParam.Value = _TempHistoryID  '' c


                'For Deormalization of History table
                'DrugName
                If Not IsNothing(oPatientHistory.DrugName) Then
                    sqlParam = cmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.DrugName
                Else
                    sqlParam = cmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If



                'NDCCode
                If Not IsNothing(oPatientHistory.NDCCode) Then
                    sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.NDCCode
                Else
                    sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If



                If Not IsNothing(oPatientHistory.DOEAllergy) Then
                    sqlParam = cmd.Parameters.Add("@DOEOAllergy", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.DOEAllergy
                End If

                If Not IsNothing(oPatientHistory.ConceptId) Then
                    sqlParam = cmd.Parameters.Add("@ConceptID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.ConceptId
                End If



                If Not IsNothing(oPatientHistory.ICD9) Then
                    sqlParam = cmd.Parameters.Add("@sICD9", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.ICD9
                End If

                If Not IsNothing(oPatientHistory.RxNormCode) Then
                    sqlParam = cmd.Parameters.Add("@sRxNormID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.RxNormCode
                Else
                    sqlParam = cmd.Parameters.Add("@sRxNormID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = oReconcileList.UserName


                sqlParam = cmd.Parameters.Add("@sSourceName", SqlDbType.VarChar, 255)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oReconcileList.SourceName) Then
                    sqlParam.Value = oReconcileList.SourceName
                Else
                    sqlParam.Value = ""
                End If

                If IsDate(oReconcileList.LastModifiedDateTime) Then
                    If oReconcileList.LastModifiedDateTime <> "12:00:00 AM" Then
                        sqlParam = cmd.Parameters.Add("@dtLastUpdated", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oReconcileList.LastModifiedDateTime
                    End If
                End If

                sqlParam = cmd.Parameters.Add("@OnsetDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oPatientHistory.OnsetDate) AndAlso oPatientHistory.OnsetDate <> "" Then
                    sqlParam.Value = Format(Convert.ToDateTime(oPatientHistory.OnsetDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dtConcernEndDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oPatientHistory.ConcernEndDate) AndAlso oPatientHistory.ConcernEndDate <> "" Then
                    sqlParam.Value = Format(Convert.ToDateTime(oPatientHistory.ConcernEndDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = DBNull.Value
                End If
                sqlParam = cmd.Parameters.Add("@dtObservationEndDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oPatientHistory.ObservationEndDate) AndAlso oPatientHistory.ObservationEndDate <> "" Then
                    sqlParam.Value = Format(Convert.ToDateTime(oPatientHistory.ObservationEndDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = DBNull.Value
                End If
                
                sqlParam = cmd.Parameters.Add("@sConcernStatus", SqlDbType.VarChar, 200)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oPatientHistory.ConcernStatus) Then
                    sqlParam.Value = oPatientHistory.ConcernStatus
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sSeverityCode", SqlDbType.VarChar, 200)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oPatientHistory.SeverityCode) Then
                    sqlParam.Value = oPatientHistory.SeverityCode
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sSeverity", SqlDbType.VarChar, 200)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oPatientHistory.Severity) Then
                    sqlParam.Value = oPatientHistory.Severity
                Else
                    sqlParam.Value = ""
                End If
                Try
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    _TempHistoryID = cmd.Parameters("@TempHistoryID").Value


                    'If oPatientHistory.Reaction <> "" Then
                    '    Dim objCCDDatabaseLayer As New gloCCDDatabaseLayer
                    '    objCCDDatabaseLayer.UpdateCategoryMaster(oPatientHistory.Reaction, "Reaction", oReconcileList.PatientID)
                    '    If Not IsNothing(objCCDDatabaseLayer) Then
                    '        objCCDDatabaseLayer.Dispose()
                    '    End If
                    'End If

                Catch ex As Exception
                    Throw ex
                End Try
                'cmd.Parameters.Clear()
                If IsNothing(sqlParam) = False Then
                    sqlParam = Nothing
                End If
                If IsNothing(ExamParam) = False Then
                    ExamParam = Nothing
                End If

                k = k + 1
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

            Next
            trReconcile.Commit()
            Return True




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            trReconcile.Rollback()
            ex = Nothing
            Return False
        Finally

            If IsNothing(trReconcile) = False Then
                trReconcile.Dispose()
                trReconcile = Nothing
            End If
            If IsNothing(ogloPatientRegDBLayer) = False Then
                ogloPatientRegDBLayer.Dispose()
                ogloPatientRegDBLayer = Nothing
            End If
            If IsNothing(ExamParam) = False Then
                ExamParam = Nothing
            End If
            If conn.State = ConnectionState.Open Then conn.Close()
            'If Not IsNothing(conn) Then 'SLR:Don't dispose it here
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

    End Function

    Public Function CheckListNameExists(ByVal ListName As String) As Boolean
        Dim cmd1 As SqlCommand = Nothing
        Dim sqlParam1 As SqlParameter = Nothing
        Try


            'objBusLayer.Open_Con()
            cmd1 = New SqlCommand("CCD_Reconcile_CheckListNameExists", conn)
            cmd1.CommandType = CommandType.StoredProcedure



            sqlParam1 = cmd1.Parameters.AddWithValue("@sListName", ListName)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.DbType = SqlDbType.VarChar
            sqlParam1.Value = ListName


            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd1.CommandTimeout = 0
            Dim rowAffected As Long
            rowAffected = CType(cmd1.ExecuteScalar, Long)

            If rowAffected > 0 Then
                Return True     ' Duplicate Exists
            Else
                Return False    ' Duplicate Not Exists
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If IsNothing(cmd1) = False Then
                cmd1.Parameters.Clear()
                cmd1.Dispose()
                cmd1 = Nothing
            End If
            If IsNothing(sqlParam1) = False Then
                sqlParam1 = Nothing
            End If
        End Try
    End Function

    Public Function UpdateStatus(ByVal _CCDID As Int64, ByVal _PatientID As Int64, ByVal _ListID As String, ByVal isFileStatus As Boolean, Optional ByVal isReadySatus As Boolean = False, Optional ByVal Status As Int16 = 0, Optional ByVal _LoginProviderID As Int64 = 0, Optional ByVal IsStaffUserTransaction As Boolean = 0) As Boolean

        Dim cmd As SqlClient.SqlCommand = Nothing
        Dim conn As New SqlClient.SqlConnection
        Dim sqlParam As SqlParameter = Nothing

        Try
            conn.ConnectionString = gloLibCCDGeneral.Connectionstring
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New SqlCommand("CCD_Reconcile_UpDateStatus", conn)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@nCCDID", _CCDID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.BigInt

            sqlParam = cmd.Parameters.AddWithValue("@nPatientID", _PatientID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.BigInt

            If isReadySatus = True Then
                sqlParam = cmd.Parameters.AddWithValue("@Status", ListStatus.Ready)
            Else
                If isFileStatus = True Then
                    sqlParam = cmd.Parameters.AddWithValue("@Status", Status)
                Else
                    sqlParam = cmd.Parameters.AddWithValue("@Status", Status)
                End If

            End If

            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.Int


            sqlParam = cmd.Parameters.AddWithValue("@ListID", _ListID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.VarChar

            If isFileStatus = True Then
                sqlParam = cmd.Parameters.AddWithValue("@isFileStatus", 1)
            Else
                sqlParam = cmd.Parameters.AddWithValue("@isFileStatus", 0)
            End If

            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.Bit

            sqlParam = cmd.Parameters.AddWithValue("@nProviderID", _LoginProviderID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.BigInt

            sqlParam = cmd.Parameters.AddWithValue("@IsStaffUser", IsStaffUserTransaction)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.Bit
            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            conn.Close()
            cmd.Dispose()
            Return False
        Finally
            If conn.State = ConnectionState.Closed Then
                conn.Close()
            End If

            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try

    End Function

    Public Function UpdateSkippedStatus(ByVal _CCDID As Int64, ByVal _PatientID As Int64, ByVal _SkippedID As String, ByVal _Type As String) As Boolean
        Dim cmd As SqlClient.SqlCommand = Nothing
        Dim conn As New SqlClient.SqlConnection
        'Dim strquery As String = ""
        Dim sqlParam As SqlParameter = Nothing
        Try
            conn.ConnectionString = gloLibCCDGeneral.Connectionstring
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New SqlCommand("CCD_Reconcile_UpDateSkippedStatus", conn)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@nCCDID", _CCDID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.BigInt

            sqlParam = cmd.Parameters.AddWithValue("@nPatientID", _PatientID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.BigInt


            sqlParam = cmd.Parameters.AddWithValue("@Status", 1)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.Int

            sqlParam = cmd.Parameters.AddWithValue("@SkippedID", _SkippedID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.VarChar

            sqlParam = cmd.Parameters.AddWithValue("@Type", _Type)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.DbType = SqlDbType.VarChar

            'cmd.CommandText = strquery
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            conn.Close()
            cmd.Dispose()
            Return False
        Finally
            conn.Close()
            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try

    End Function


End Class
