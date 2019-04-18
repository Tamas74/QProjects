Imports System.Data.SqlClient

Module mdlGenerateVisitID

    'Private Con As SqlConnection

    'Private Sub InitialzeCon()
    '    Try
    '        Dim conString As String
    '        conString = GetConnectionString()
    '        Con = New SqlConnection(conString)
    '    Catch ex As Exception   ' Catch the error.
    '        MessageBox.Show(ex.ToString, "Patient Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Con.Close()
    '        finally

    '    End Try
    'End Sub
    Private _PreviousVisitID As Int64 = -1
    Private _PreviousVisitDate As Date
    Public Property PreviousVisitID() As Int64
        Get
            Return _PreviousVisitID
        End Get
        Set(ByVal Value As Int64)
            _PreviousVisitID = Value
        End Set
    End Property
    Public Property PreviousVisitDate() As Date
        Get
            Return _PreviousVisitDate
        End Get
        Set(ByVal Value As Date)
            _PreviousVisitDate = Value
        End Set
    End Property

    Public Function GenerateVisitID(ByVal VisitDate As DateTime, ByVal PatientID As Int64) As Long  ''date change to datetime for certification changes
        Dim con As SqlConnection = Nothing
        Dim cmdVisits As SqlCommand = Nothing
        Dim objParam As SqlParameter = Nothing
        Dim objFlagParam As SqlParameter = Nothing

        Try
            'Call InitialzeCon()
            con = New SqlConnection(GetConnectionString())
            cmdVisits = New SqlCommand("gsp_InsertVisits", con)
            cmdVisits.CommandType = CommandType.StoredProcedure

            objParam = cmdVisits.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            objParam = cmdVisits.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitDate

            Dim nAppointmentID As Long
            nAppointmentID = 0

            objParam = cmdVisits.Parameters.Add("@AppointmentID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nAppointmentID

            objFlagParam = cmdVisits.Parameters.Add("@flag", SqlDbType.Int)
            objFlagParam.Direction = ParameterDirection.Output

            objParam = cmdVisits.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID(PatientID)

            objParam = cmdVisits.Parameters.Add("@VisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Output
            objParam.Value = 0

            con.Open()
            cmdVisits.ExecuteNonQuery()
            con.Close()

            If objFlagParam.Value = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Visit, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Visit Added on " & CType(Now, String), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            objFlagParam = Nothing

            cmdVisits.Parameters.Clear()
            cmdVisits.Dispose()
            cmdVisits = Nothing

            con.Dispose()
            con = Nothing

            Return objParam.Value
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function


    Public Function GenerateVisitID(ByVal PatientID As Long) As Long
        Dim con As SqlConnection = Nothing
        Dim cmdVisits As SqlCommand = Nothing
        Dim objParamFlag As SqlParameter = Nothing
        Dim nVisitID As Long
        Dim objParam As SqlParameter = Nothing
        Try
            con = New SqlConnection(GetConnectionString())
            cmdVisits = New SqlCommand("gsp_InsertVisits", con)
            cmdVisits.CommandType = CommandType.StoredProcedure

            objParam = cmdVisits.Parameters.AddWithValue("@nPatientID", PatientID)

            objParam.Direction = ParameterDirection.Input

            objParam = cmdVisits.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Now

            'Retrieve Appointment ID
            Dim nAppointmentID As Long
            nAppointmentID = 0

            objParam = cmdVisits.Parameters.Add("@AppointmentID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nAppointmentID

            objParamFlag = cmdVisits.Parameters.Add("@Flag", SqlDbType.Int)
            objParamFlag.Direction = ParameterDirection.Output

            objParam = cmdVisits.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID(PatientID)

            objParam = cmdVisits.Parameters.Add("@VisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Output
            objParam.Value = 0

            con.Open()
            cmdVisits.ExecuteNonQuery()
            con.Close()
            nVisitID = objParam.Value

            If objParamFlag.Value = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Visit, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Visit Added on " & CType(Now, String), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            objParam = Nothing
            objParamFlag = Nothing


            cmdVisits.Parameters.Clear()
            cmdVisits.Dispose()
            cmdVisits = Nothing

            con.Dispose()
            con = Nothing
            Return nVisitID

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

    Public Function GetVisitdate(ByVal VisitID As Long) As Date
        Dim con As SqlConnection = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter = Nothing
        Try

            'Call InitialzeCon()
            con = New SqlConnection(GetConnectionString())
            Cmd = New SqlCommand("gsp_GetVisitDate", con)
            objParam = Nothing
            objParam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            Cmd.CommandType = CommandType.StoredProcedure

            con.Open()
            Dim VisitDate As Date
            VisitDate = Cmd.ExecuteScalar
            con.Close()

            objParam = Nothing

            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            con.Dispose()
            con = Nothing
            Return VisitDate
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

    Public Function GetVisitID(ByVal VisitDate As Date, Optional ByVal PatientID As Long = 0) As Long
        Dim con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            'Call InitialzeCon()
            con = New SqlConnection(GetConnectionString())
            cmd = New SqlCommand("gsp_GetVisitID", con)
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@VisitDate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitDate

            objParam = Cmd.Parameters.Add("@PatientID", SqlDbType.BigInt, 18)
            objParam.Direction = ParameterDirection.Input
            'Lines commented by dipak 20100007 as we not using gnPatientID in local scope.
            'If PatientID = 0 Then
            '    PatientID = gnPatientID
            'End If
            objParam.Value = PatientID

            Cmd.CommandType = CommandType.StoredProcedure

            If Con.State <> ConnectionState.Open Then
                Con.Open()
            End If
            Dim VisitID As Long
            VisitID = Cmd.ExecuteScalar
            Con.Close()

            If IsDBNull(VisitID) = True Then
                '' If VisitId is Not Found then Return 0
                VisitID = 0
            End If

            Return VisitID

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
            If (IsNothing(cmd) = False) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

End Module
