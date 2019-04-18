'' By Mahesh 20070112
'' Doctors Dash Board

Imports System.Data.SqlClient

Public Class clsDoctorsDashBoard


    ''''' By Mahesh  20070112
    '''' TO Get the Provider FullName , Login Name, ProviderID, UserID OF Current User
    Public Function Get_LoginProviderDetails(ByVal LoginName As String) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_GetLoginProviderDetails", con)
            cmd.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = cmd.Parameters.Add("@LoginName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = LoginName

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            con.Close()
            da.Dispose()

            objParam = Nothing

            Return dt

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- Get_LoginProviderDetails -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- Get_LoginProviderDetails -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

    Public Function Fill_Data(ByVal Type As MainMenu.Type, ByVal ProviderID As Long, ByVal UserID As Long) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            cmd = New SqlCommand("gsp_DDB_CheckRecordcount", con)
            cmd.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = cmd.Parameters.Add("@dtSysdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Now

            objParam = cmd.Parameters.Add("@Flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Type

            objParam = cmd.Parameters.Add("@ProviderID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ProviderID

            objParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = UserID

            da = New SqlDataAdapter
            dt = New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            con.Close()

            objParam = Nothing
            Return dt

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- Fill_Data -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- Fill_Data -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

    Public Function Fill_Appointments(ByVal Flag As Int16, ByVal ProviderID As Long, ByVal UserID As Long, ByVal dtAppointmentDate As Date) As DataTable
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim dt As DataTable
        Try
            '' Flag =1 -- Appointment FOR Selected Provider
            '' Flag =2 -- Appointment FOR All Providers

            ' '' gsp_GetAppointments_OnDate

            ' '' @dtDate		DATETIME,
            ' '' @Flag		INT,
            ' '' @ProviderID	Numeric(18,0),
            ' '' @UserID		Numeric(18,0)
            With oDB
                .DBParameters.Add("@dtDate", dtAppointmentDate, ParameterDirection.Input, SqlDbType.DateTime)
                .DBParameters.Add("@Flag", Flag, ParameterDirection.Input, SqlDbType.Int)
                .DBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt)
                .Connect(GetConnectionString)
                dt = .ReadData("gsp_GetAppointments_OnDate")
                .Disconnect()
            End With
            
            Return dt

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- Fill_Appointments -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- Fill_Appointments -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try

    End Function

    Public Function Fill_History(ByVal PatientID As Long, ByVal VisitID As Long, ByVal Flag As Integer) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_GetHistory", con)
            cmd.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = cmd.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Now.Date

            objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            objParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Flag

            objParam = cmd.Parameters.Add("@VisitId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            con.Close()
            da.Dispose()
            objParam = Nothing
            Return dt

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- Fill_History -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- Fill_History -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function
    Public Function Fill_StandardHistoryTypes() As DataSet
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("History_GetStandardHistoryTypes", con)
            cmd.CommandType = CommandType.StoredProcedure



            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            da.SelectCommand = cmd
            da.Fill(ds)
            con.Close()
            da.Dispose()
            ds.Tables(1).TableName = "CategoryType"
            ds.Tables(0).TableName = "HistoryTypes"
            Return ds

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- Fill_History -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- Fill_History -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

#Region "Clinical Environment"

    Public Function Set_Clinic_Environment(ByVal Environment As frmSettings_New.ClinicEnvironment, ByVal ColorCode As Long) As Boolean
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_InsertClinicEnvironment", con)
            cmd.CommandType = CommandType.StoredProcedure
            'nEnvironment()
            'nColor()
            Dim Param As SqlParameter
            Param = cmd.Parameters.Add("@nEnvironment", SqlDbType.BigInt)
            With Param
                .Value = Environment
                .Direction = ParameterDirection.Input
            End With

            Param = cmd.Parameters.Add("@nColor", SqlDbType.BigInt)
            With Param
                .Value = ColorCode
                .Direction = ParameterDirection.Input
            End With
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Param = Nothing
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

    Public Function Set_Clinic_Environment(ByVal objClinicENV As Collection) As Boolean
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_InsertClinicEnvironment", con)
            cmd.CommandType = CommandType.StoredProcedure
            'nEnvironment()
            'nColor()
            con.Open()
            For i As Int16 = 1 To objClinicENV.Count
                Dim Param As SqlParameter
                Param = cmd.Parameters.Add("@nEnvironment", SqlDbType.BigInt)
                Dim splstrvalue As String() = Split(objClinicENV(i), "|", 2)
                With Param
                    .Value = splstrvalue.GetValue(0)
                    .Direction = ParameterDirection.Input
                End With

                Param = cmd.Parameters.Add("@nColor", SqlDbType.BigInt)
                With Param
                    If (splstrvalue.Length > 1) Then
                        .Value = splstrvalue.GetValue(1)
                    Else
                        .Value = 0
                    End If
                    .Direction = ParameterDirection.Input
                End With

                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                Param = Nothing
            Next
            con.Close()
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

    Public Function Get_Clinic_Environment() As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            cmd = New SqlCommand("gsp_GetClinicEnvironment_Settings", con)
            da = New SqlDataAdapter
            dt = New DataTable

            cmd.CommandType = CommandType.StoredProcedure
            da.SelectCommand() = cmd
            da.Fill(dt)
            con.Close()
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

    'Public Function Get_Patient_ClinicEnvironment(ByVal PatientID As Long, ByVal nVisitID As Int64) As DataTable
    '    Try
    '        Dim con As New SqlConnection(GetConnectionString)
    '        Dim cmd As New SqlCommand("gsp_GetPatientClinicEnvironment", con)
    '        Dim da As New SqlDataAdapter
    '        Dim dt As New DataTable
    '        Dim para As SqlParameter

    '        para = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
    '        para.Direction = ParameterDirection.Input
    '        para.Value = PatientID

    '        '' SUDHIR 20090709 ''
    '        para = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
    '        With para
    '            .Value = nVisitID
    '            .Direction = ParameterDirection.Input
    '        End With
    '        '' END SUDHIR ''

    '        cmd.CommandType = CommandType.StoredProcedure
    '        da.SelectCommand() = cmd
    '        da.Fill(dt)
    '        Return dt
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    End Try
    'End Function
    Public Function Get_Patient_ClinicEnvironment(ByVal PatientID As Long) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            cmd = New SqlCommand("gsp_GetPatientClinicEnvironment", con)
            da = New SqlDataAdapter
            dt = New DataTable
            Dim para As SqlParameter

            para = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            para.Direction = ParameterDirection.Input
            para.Value = PatientID

            para = cmd.Parameters.Add("@VisitDate", SqlDbType.DateTime)
            para.Direction = ParameterDirection.Input
            para.Value = Now.Date

            ' '' SUDHIR 20090709 ''
            'para = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            'With para
            '    .Value = nVisitID
            '    .Direction = ParameterDirection.Input
            'End With
            ' '' END SUDHIR ''

            cmd.CommandType = CommandType.StoredProcedure
            da.SelectCommand() = cmd
            da.Fill(dt)
            con.Close()
            para = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

    Public Function Set_Patient_ClinicEnvironment(ByVal PatientID As Long, ByVal Environment As frmSettings_New.ClinicEnvironment, ByVal ColorCode As Long, ByVal nVisitID As Int64) As Boolean
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_InsertPatientClinicEnvironment", con)
            cmd.CommandType = CommandType.StoredProcedure
            'nEnvironment()
            'nColor()
            Dim Param As SqlParameter
            Param = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            With Param
                .Value = PatientID
                .Direction = ParameterDirection.Input
            End With

            Param = cmd.Parameters.Add("@nEnvironment", SqlDbType.BigInt)
            With Param
                .Value = Environment
                .Direction = ParameterDirection.Input
            End With

            Param = cmd.Parameters.Add("@nColor", SqlDbType.BigInt)
            With Param
                .Value = ColorCode
                .Direction = ParameterDirection.Input
            End With

            '' SUDHIR 20090709 ''
            Param = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            With Param
                .Value = nVisitID
                .Direction = ParameterDirection.Input
            End With
            '' END SUDHIR ''

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Param = Nothing
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

#End Region

    Public Function GetLocation() As DataTable
        'Sarika 23rd Apr
        Dim dt As DataTable = Nothing
        Dim _strSQL As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString())
        Dim da As SqlDataAdapter = Nothing

        Try
            dt = New DataTable

            _strSQL = "select nCategoryID,sDescription from Category_MST where sCategoryType='Location' Order By sDescription"
            '    conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            conn.Close()

            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- GetLocation -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- GetLocation -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Public Function GetUsers() As DataTable
        'Sarika 23rd Apr
        Dim dt As DataTable = Nothing
        Dim _strSQL As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString())
        Dim da As SqlDataAdapter = Nothing

        Try
            dt = New DataTable
            _strSQL = "SELECT nUserID, sLoginName, ISNULL(sFirstName,'') + ' ' + ISNULL(sLastName,'') AS Name FROM User_MST WHERE ISNULL(nBlockStatus,0)=0 Order by sLoginName"

            '_strSQL = "select nCategoryID,sDescription from Category_MST where sCategoryType='Status' Order By sDescription"
            '    conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            conn.Close()
            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- GetUsers -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- GetUsers -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Public Function GetStatus() As DataTable
        'Sarika 23rd Apr
        Dim dt As DataTable = Nothing
        Dim _strSQL As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString())
        Dim da As SqlDataAdapter = Nothing

        Try
            dt = New DataTable

            _strSQL = "select nCategoryID,sDescription from Category_MST where sCategoryType='Status' Order By sDescription"
            '    conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            conn.Close()
            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- GetStatus -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- GetStatus -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If

        End Try
    End Function

    Public Function GetAssociates(ByVal StatusID As Long) As DataTable
        'Sarika 23rd Apr
        Dim dt As DataTable = Nothing
        Dim _strSQL As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString())
        Dim da As SqlDataAdapter = Nothing

        Try
            dt = New DataTable

            _strSQL = " SELECT  User_MST.nUserID, User_MST.sLoginName, ISNULL(sFirstName,'') + ' ' + ISNULL(sLastName,'') AS Name,StatusUsers.nColorCode FROM StatusUsers INNER JOIN " _
                    & " User_MST ON StatusUsers.nUserID = User_MST.nUserID " _
                    & " WHERE(StatusUsers.nStatusID = " & StatusID & ") ORDER BY User_MST.sLoginName"

            '    conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            conn.Close()
            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- GetAssociates -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- GetAssociates -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Public Function GetLocationStatus(ByVal UserID As Long, ByVal dtDate As DateTime, ByVal Flag As Integer) As DataTable
        '' Get Location & Status of the Patients at the Given Date
        ' Flag = 1 '' For Selected User
        ' Flag = 2 '' For All User
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try
            '_strSQL = "gsp_GetLocationStatus"

            cmd = New SqlCommand("gsp_GetLocationStatus", Con)
            cmd.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = cmd.Parameters.Add("@dtDate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = dtDate

            objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = UserID

            objParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Flag

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            da.Dispose()
            Con.Close()
            objParam = Nothing
            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- GetLocationStatus -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- GetLocationStatus -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Function

    Public Function Delete_UserDisplaySettings(ByVal UserID As Long, ByVal MachineName As String) As Boolean
        '' Delete The Display Setting of the User on the M/C
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try
            ''Parameters : @nUserID, @sMachineName
            cmd = New SqlCommand("gsp_DELETE_UserDisplaySettings", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = UserID

            objParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachineName

            Con.Open()
            cmd.ExecuteNonQuery()
            Con.Close()
            objParam = Nothing
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- Delete_UserDisplaySettings -- " & ex.ToString)
            Return False
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- Delete_UserDisplaySettings -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Function

    Public Function Save_UserDisplaySettings(ByVal UserID As Long, ByVal MachineName As String, ByVal col_Settings As Collection) As Boolean
        '' Save The Display to the DataBase
        Dim trn As SqlTransaction = Nothing
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try


            Dim objParam As SqlParameter
            Con.Open()

            trn = Con.BeginTransaction

            ''''
            ''Parameters : @nUserID, @sMachineName
            cmd = New SqlCommand("gsp_DELETE_UserDisplaySettings", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trn

            objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = UserID

            objParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachineName

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            ''''


            ''Parameters : @nUserID, @sMachineName, @nPanel, @nSize
            cmd = New SqlCommand("gsp_INUP_UserDisplaySettings", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trn

            ' Dim objParam As SqlParameter
            Dim lst As myList

            UpdateLog("  --- col_Settings.Count  " & col_Settings.Count)
            For i As Integer = 1 To col_Settings.Count
                'lst = New myList
                lst = CType(col_Settings(i), myList)

                objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = UserID

                objParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = MachineName

                objParam = cmd.Parameters.Add("@nPanel", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                ' UpdateLog("  --- lst.ID(" & i & ")= " & lst.ID)
                objParam.Value = lst.ID ''''' Control Enum

                objParam = cmd.Parameters.Add("@nSize", SqlDbType.Decimal)
                objParam.Direction = ParameterDirection.Input
                'UpdateLog("  --- lst.TemplateResult(" & i & ")= " & lst.TemplateResult)
                objParam.Value = CDbl(lst.TemplateResult) ''''' Control Size

                ' Con.Open()
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                objParam = Nothing
            Next
            cmd.Dispose()
            cmd = Nothing
            trn.Commit()

            Return True
        Catch ex As SqlException
            UpdateLog("clsDoctorsDashBoard -- Save_UserDisplaySettings -- " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trn.Rollback()
            Return False
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- Save_UserDisplaySettings -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trn.Rollback()
            Return False
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
            If Not IsNothing(trn) Then
                trn.Dispose()
                trn = Nothing
            End If
        End Try
    End Function

    Public Function Get_UserDisplaySettings(ByVal UserID As Long, ByVal MachineName As String) As DataTable
        '' Get The Display Setting of the User on the M/C
        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            Con = New SqlConnection(GetConnectionString)
            ''Parameters : @nUserID, @sMachineName
            cmd = New SqlCommand("gsp_GET_UserDisplaySettings", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = UserID

            objParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachineName

            da = New SqlDataAdapter
            dt = New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            Con.Close()
            objParam = Nothing
            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- Get_UserDisplaySettings -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- Get_UserDisplaySettings -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Function

    Public Function GetUserName(ByVal userID As Int64) As String
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim result As Object
        Try
            Con.Open()
            cmd = New SqlCommand("SELECT ISNULL(sLoginName,'') AS sLoginName FROM User_MST WHERE nUserID = " & userID & "", Con)
            result = cmd.ExecuteScalar()
            Con.Close()
            If IsNothing(result) Then
                result = ""
            End If
            Return result.ToString()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Function
    
    ''Fuction By Sudhir - 20090206
    Public Function GetPatientCodeAndNameFromID(ByVal PatientID As Int64) As DataTable
        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            Con = New SqlConnection(GetConnectionString)
            Dim Query As String = "SELECT sPatientCode As PatientCode, ISNULL(sFirstName,'') + '  ' + ISNULL(sLastName,'') As PatientName, ISNULL(sFirstName,'') As FirstName , ISNULL(sMiddleName,'') As MiddleName, ISNULL(sLastName,'') As LastName FROM Patient WHERE nPatientID = " + PatientID.ToString()
            cmd = New SqlCommand(Query, Con)
            da = New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Con.Close()
            If IsNothing(dt) = False Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Function
    Public Function GetUnmatchedPatientCodeAndName(ByVal TaskID As Int64) As DataTable
        '' Function written by Abhijeet on date 20100415 for accessinf unmatch patient details
        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            Con = New SqlConnection(GetConnectionString)
            Dim Query As String = "SELECT sPatientCode As PatientCode, ISNULL(sFirstName,'') + ' ' + ISNULL(sLastName,'') As PatientName, ISNULL(sFirstName,'') As FirstName , ISNULL(sMiddleName,'') As MiddleName, ISNULL(sLastName,'') As LastName FROM PatientsUnmatchedInLab WHERE ntaskID = " + TaskID.ToString()
            cmd = New SqlCommand(Query, Con)
            da = New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Con.Close()
            If IsNothing(dt) = False Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Function
End Class
