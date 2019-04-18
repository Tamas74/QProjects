Imports System.Data.SqlClient

Public Class clsDiagnosisone_DBLayer

    Public Function Save_DiagnosisOne_Responce(ByVal CCDDiagnosisOneFilesID As Int64, ByVal _PatientID As Int64, ByVal VisitID As Int64, ByVal sMachineName As String, ByVal RequestFileName As String, ByVal ResponceFileName As String, ByVal UserNAme As String, ByVal ErrorString As String) As Int64

        Dim sqlParam As SqlParameter = Nothing
        Dim sqlParamCCDDiagnosisOneFilesID As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            Dim ogloCCDInterface As New gloCCDInterface

            Dim RequestFileImage As Byte() = Nothing
            Dim ResponceFileImage As Byte() = Nothing
            If (CCDDiagnosisOneFilesID = 0) Then
                RequestFileImage = ogloCCDInterface.ConvertFiletoBinary(RequestFileName)
            Else
                If (IO.File.Exists(ResponceFileName) = True) Then
                    ResponceFileImage = ogloCCDInterface.ConvertFiletoBinary(ResponceFileName)
                End If
            End If
            ogloCCDInterface.Dispose()
            ogloCCDInterface = Nothing

            Dim conn As New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd = New SqlCommand("gsp_InUpCCDDiagnosisOneFiles", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParamCCDDiagnosisOneFilesID = cmd.Parameters.Add("@CCDDiagnosisOneFilesID", SqlDbType.BigInt)
            sqlParamCCDDiagnosisOneFilesID.Direction = ParameterDirection.InputOutput
            sqlParamCCDDiagnosisOneFilesID.Value = CCDDiagnosisOneFilesID

            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _PatientID

            sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.Add("@dtTrnDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DateTime.Now

            'sqlParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar, 50)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = sMachineName

            sqlParam = cmd.Parameters.Add("@RequestFile", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = RequestFileImage

            sqlParam = cmd.Parameters.Add("@ResponseFile", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ResponceFileImage

            sqlParam = cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sMachineName

            sqlParam = cmd.Parameters.Add("@ErrorString", SqlDbType.Text)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ErrorString


            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()
            CCDDiagnosisOneFilesID = sqlParamCCDDiagnosisOneFilesID.Value
            conn.Close()
            conn.Dispose()
            conn = Nothing
            Return CCDDiagnosisOneFilesID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If Not IsNothing(sqlParamCCDDiagnosisOneFilesID) Then
                sqlParamCCDDiagnosisOneFilesID = Nothing
            End If

        End Try
    End Function
    Public Function GetCCDDiagnosisOneFiles(ByVal PatientID As Int64, Optional ByVal CCDDiagnosisOneFilesID As Int64 = 0) As DataTable
        Dim sqlParam As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(gloLibCCDGeneral.Connectionstring)
        Dim dt As DataTable = Nothing
        Dim da As SqlDataAdapter = Nothing
        Try

            cmd = New SqlCommand("gsp_GetCCDDiagnosisOneFiles", conn)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = cmd.Parameters.Add("@CCDDiagnosisOneFilesID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CCDDiagnosisOneFilesID

            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da = New SqlDataAdapter(cmd)
            dt = New DataTable
            ' fill data into the data table
            da.Fill(dt)
            conn.Close()
            conn.Dispose()
            conn = Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If da IsNot Nothing Then
                da.Dispose()
            End If

        End Try
        Return dt
        'cmd.ExecuteNonQuery()
    End Function


    Public Function GetPAtCDASection(ByVal PatientID As Int64) As DataTable
        Dim sqlParam As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(gloLibCCDGeneral.Connectionstring)
        Dim dt As DataTable = Nothing
        Dim da As SqlDataAdapter = Nothing
        Try

            cmd = New SqlCommand("Getpatient_CDASections", conn)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID

           
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da = New SqlDataAdapter(cmd)


            dt = New DataTable
            ' fill data into the data table
            da.Fill(dt)
           
        Catch ex As Exception
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If da IsNot Nothing Then
                da.Dispose()
            End If

        End Try
        Return dt
        'cmd.ExecuteNonQuery()
    End Function

End Class
