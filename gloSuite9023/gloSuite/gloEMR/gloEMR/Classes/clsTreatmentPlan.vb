Imports System.Data.SqlClient

Public Class clsTreatmentPlan
    Private Conn As SqlConnection

    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub

    Public Sub Dispose()

        If Conn IsNot Nothing Then
            Conn.Dispose()
            Conn = Nothing
        End If
       
    End Sub

    Public Function INUP_PlanOfTreatment(ByVal PlanOfTreatmentId As Long, ByVal PatientID As Long, ByVal Plantitle As String, ByVal PlanAssesment As String,
                                         ByVal PlanEffectiveFrom As DateTime, ByVal PlanEffectiveTo As DateTime, ByVal PlanStatus As Boolean, ByVal PlanDeleteStatus As Boolean, ByVal ExamID As Long, ByVal dtPOTDTL As DataTable, Optional ByVal Type As Integer = 1) As Long
        Dim bResult As Long = 0
        Dim Cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_INUP_PlanOfTreatment", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            sqlParam = Cmd.Parameters.Add("@nPlanOfTreatmentID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.InputOutput
            sqlParam.Value = PlanOfTreatmentId

            sqlParam = Cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID

            sqlParam = Cmd.Parameters.Add("@sTitle", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Plantitle

            sqlParam = Cmd.Parameters.Add("@sAssesment", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PlanAssesment

            sqlParam = Cmd.Parameters.Add("@dtEffectiveStartDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PlanEffectiveFrom

            sqlParam = Cmd.Parameters.Add("@dtEffectiveEndDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PlanEffectiveTo

            sqlParam = Cmd.Parameters.Add("@bIsActive", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PlanStatus

            sqlParam = Cmd.Parameters.Add("@bIsDeleted", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PlanDeleteStatus

            sqlParam = Cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ExamID

            sqlParam = Cmd.Parameters.Add("@nType", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Type

            sqlParam = Cmd.Parameters.Add("@tvpPOTDTL", SqlDbType.Structured)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = dtPOTDTL

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            Cmd.ExecuteNonQuery()
            If (IsNothing(Cmd) = False) Then
                bResult = Convert.ToInt64(Cmd.Parameters("@nPlanOfTreatmentID").Value)
            End If
        Catch ex As Exception
            bResult = 0
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Plan Of Treatment")
        Finally
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
        Return bResult
    End Function

    Public Function GetTreatmentPlans(ByVal PlanOfTreatmentId As Long, ByVal PatientID As Long) As DataTable
        Dim dtPlanOfTreatments As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParameters As New gloDatabaseLayer.DBParameters()

        oDB.Connect(False)

        Try
            oParameters.Add("@nPlanOfTreatmentID", PlanOfTreatmentId, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_GetPlanOfTreatments", oParameters, dtPlanOfTreatments)

            Return dtPlanOfTreatments
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oParameters.Dispose()

            If dtPlanOfTreatments IsNot Nothing Then
                dtPlanOfTreatments.Dispose()
            End If
        End Try
    End Function

    Public Function GetActivePlanOfTreatmentId(ByVal PatientId As Long) As Long
        Dim PlanOfTreatmentID As Long = 0
        Dim dtPlanOfTreatment As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        oDB.Connect(False)
        Dim strQry As String = "SELECT nPlanOfTreatmentID FROM POT_MST  WHERE [nPatientID] = " & PatientId & " AND [bIsActive]=1  AND [Type] =1"
        Try
            oDB.Retrive_Query(strQry, dtPlanOfTreatment)
            If Not IsNothing(dtPlanOfTreatment) Then
                If dtPlanOfTreatment.Rows.Count > 0 Then
                    PlanOfTreatmentID = Convert.ToInt64(dtPlanOfTreatment.Rows(0)("nPlanOfTreatmentID"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            If dtPlanOfTreatment IsNot Nothing Then
                dtPlanOfTreatment.Dispose()
            End If
        End Try

        Return PlanOfTreatmentID
    End Function

End Class
