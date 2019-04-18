Imports System.Data.SqlClient

Public Class clsDoctorSpeakerConfiguration
    Dim _nID As Int64
    Dim _sMachineName As String
    Dim _sDoctor As String
    Dim _sSpeaker As String

    Public Property MachineName() As String
        Get
            Return _sMachineName
        End Get
        Set(ByVal Value As String)
            _sMachineName = Value
        End Set
    End Property

    Public Property Speaker() As String
        Get
            Return _sSpeaker
        End Get
        Set(ByVal Value As String)
            _sSpeaker = Value
        End Set
    End Property

    Public Property Doctor() As String
        Get
            Return _sDoctor
        End Get
        Set(ByVal Value As String)
            _sDoctor = Value
        End Set
    End Property
    Public Property ID() As Int64
        Get
            Return _nID
        End Get
        Set(ByVal Value As Int64)
            _nID = Value
        End Set
    End Property
    Public Function AddDoctorSpeakerConfiguration() As Boolean
        Return AddDoctorSpeakerConfiguration(_sMachineName, _sDoctor, _sSpeaker)
    End Function

    Public Function AddDoctorSpeakerConfiguration(ByVal sMachineName As String, ByVal sDoctorName As String, ByVal sSpeakerName As String) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpDoctorSpeakerConfiguration"
            objCmd.Connection = objCon


            Dim objParaMachineName As New SqlParameter
            With objParaMachineName
                .ParameterName = "@MachineName"
                .Value = sMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMachineName)


            Dim objParaDoctorName As New SqlParameter
            With objParaDoctorName
                .ParameterName = "@DoctorName"
                .Value = sDoctorName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaDoctorName)

            Dim objParaSpeakerName As New SqlParameter
            With objParaSpeakerName
                .ParameterName = "@SpeakerName"
                .Value = sSpeakerName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaSpeakerName)

            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objParaMachineName = Nothing
            objParaDoctorName = Nothing
            objParaSpeakerName = Nothing

            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorSpeakerConfiguration -- AddDoctorSpeakerConfiguration -- " & ex.ToString)
            Return False
        Catch ex As Exception
            UpdateLog("clsDoctorSpeakerConfiguration -- AddDoctorSpeakerConfiguration -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function UpdateDoctorSpeakerConfiguration(ByVal nID As Int64) As Boolean
        Return UpdateDoctorSpeakerConfiguration(nID, _sDoctor, _sSpeaker)
    End Function

    Public Function UpdateDoctorSpeakerConfiguration(ByVal nID As Int64, ByVal sDoctorName As String, ByVal sSpeakerName As String) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpDoctorSpeakerConfiguration"
            objCmd.Connection = objCon


            Dim objParaID As New SqlParameter
            With objParaID
                .ParameterName = "@ID"
                .Value = nID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaID)


            Dim objParaDoctorName As New SqlParameter
            With objParaDoctorName
                .ParameterName = "@DoctorName"
                .Value = sDoctorName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaDoctorName)

            Dim objParaSpeakerName As New SqlParameter
            With objParaSpeakerName
                .ParameterName = "@SpeakerName"
                .Value = sSpeakerName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaSpeakerName)

            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objParaID = Nothing
            objParaDoctorName = Nothing
            objParaSpeakerName = Nothing

            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorSpeakerConfiguration -- UpdateDoctorSpeakerConfiguration -- " & ex.ToString)
            Return False
        Catch ex As Exception
            UpdateLog("clsDoctorSpeakerConfiguration -- UpdateDoctorSpeakerConfiguration -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function DeleteDoctorSpeakerConfiguration(ByVal nID As Int64) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeleteDoctorSpeakerConfiguration"
            objCmd.Connection = objCon

            Dim objParaID As New SqlParameter
            With objParaID
                .ParameterName = "@ID"
                .Value = nID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaID)

            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objParaID = Nothing

            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorSpeakerConfiguration -- DeleteDoctorSpeakerConfiguration -- " & ex.ToString)
            Return False
        Catch ex As Exception
            UpdateLog("clsDoctorSpeakerConfiguration -- DeleteDoctorSpeakerConfiguration -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function Fill_DeleteDoctorSpeakerConfiguration(ByVal sMachineName As String) As DataTable
        Dim objCon As New SqlConnection
        Dim dsData As New DataSet
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillDoctorSpeakerConfiguration"
            objCmd.Connection = objCon
            Dim objParaMachineName As New SqlParameter
            With objParaMachineName
                .ParameterName = "@MachineName"
                .Value = sMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMachineName)
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)

            objDA.Fill(dsData)
            objDA.Dispose()
            objCon.Close()
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objParaMachineName = Nothing
            objCon = Nothing
            Return dsData.Tables(0).Copy()
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorSpeakerConfiguration -- Fill_DeleteDoctorSpeakerConfiguration -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorSpeakerConfiguration -- Fill_DeleteDoctorSpeakerConfiguration -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            dsData.Dispose()
        End Try
    End Function

    Public Function CheckConfiguratuionExists(ByVal strDoctorName As String, ByVal strSpeakerName As String, ByVal strMachineName As String, Optional ByVal nID As Int64 = 0) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_CheckDoctorSpeakerConfigurationExists"
            Dim objParaDoctorName As New SqlParameter
            With objParaDoctorName
                .ParameterName = "@DoctorName"
                .Value = strDoctorName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaDoctorName)

            Dim objParaSpeakerName As New SqlParameter
            With objParaSpeakerName
                .ParameterName = "@SpeakerName"
                .Value = strSpeakerName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaSpeakerName)

            Dim objParaMachineName As New SqlParameter
            With objParaMachineName
                .ParameterName = "@MachineName"
                .Value = strMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMachineName)


            If nID <> 0 Then
                Dim objParaID As New SqlParameter
                With objParaID
                    .ParameterName = "@ID"
                    .Value = nID
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmd.Parameters.Add(objParaID)
                objParaID = Nothing
            End If
            objCmd.Connection = objCon
            Dim nCount As Integer
            objCon.Open()
            nCount = objCmd.ExecuteScalar
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objParaMachineName = Nothing
            objParaSpeakerName = Nothing
            objParaDoctorName = Nothing

            If nCount = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorSpeakerConfiguration -- CheckConfiguratuionExists -- " & ex.ToString)
            Return False
        Catch ex As Exception
            UpdateLog("clsDoctorSpeakerConfiguration -- CheckConfiguratuionExists -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Sub SaveSpeakerList(SpeakerList As DataTable, MachineName As String)

        Dim connMain As New SqlConnection
        Dim cmdMain As SqlCommand = Nothing
        Dim prmParameter As SqlParameter = Nothing

        Try

            connMain.ConnectionString = GetConnectionString()

            cmdMain = New SqlCommand("gsp_SaveSpeakerList", connMain)
            cmdMain.CommandType = CommandType.StoredProcedure

            prmParameter = New SqlParameter
            With prmParameter
                .ParameterName = "@TVP_SpeakerList"
                .Value = SpeakerList
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Structured
            End With

            cmdMain.Parameters.Add(prmParameter)

            prmParameter = New SqlParameter
            With prmParameter
                .ParameterName = "@MachineName"
                .Value = MachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With

            cmdMain.Parameters.Add(prmParameter)

            connMain.Open()
            cmdMain.ExecuteNonQuery()
            connMain.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            If IsNothing(prmParameter) = False Then
                prmParameter = Nothing
            End If

            If IsNothing(cmdMain) = False Then
                cmdMain.Dispose()
                cmdMain = Nothing
            End If

            If IsNothing(connMain) = False Then
                connMain.Dispose()
                connMain = Nothing
            End If

        End Try

    End Sub

    Public Function FillSpeakerList(MachineName As String) As DataTable

        Dim dtMain As New DataTable
        Dim daMain As SqlDataAdapter = Nothing
        Dim connMain As New SqlConnection
        Dim cmdMain As SqlCommand = Nothing
        Dim prmParameter As SqlParameter = Nothing

        Try
            connMain.ConnectionString = GetConnectionString()

            cmdMain = New SqlCommand("gsp_RetrieveSpeakerList", connMain)
            cmdMain.CommandType = CommandType.StoredProcedure

            prmParameter = New SqlParameter

            With prmParameter
                .ParameterName = "@MachineName"
                .Value = MachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            cmdMain.Parameters.Add(prmParameter)

            daMain = New SqlDataAdapter(cmdMain)

            connMain.Open()
            daMain.Fill(dtMain)
            connMain.Close()

            dtMain.TableName = "SpeakerList"

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            If IsNothing(prmParameter) = False Then
                prmParameter = Nothing
            End If

            If IsNothing(cmdMain) = False Then
                cmdMain.Dispose()
                cmdMain = Nothing
            End If

            If IsNothing(daMain) = False Then
                daMain.Dispose()
                daMain = Nothing
            End If

            If IsNothing(connMain) = False Then
                connMain.Dispose()
                connMain = Nothing
            End If

        End Try

        Return dtMain

    End Function

    Public Function RetrieveSpeaker(ByVal strMachineName As String, ByVal strDoctorName As String, _speakerRow As Int64) As DataSet
        Dim strSpeaker As String = ""
        Dim objCon As New SqlConnection
        Dim ds As New DataSet

        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveSpeaker"
            Dim objParaDoctorName As New SqlParameter
            With objParaDoctorName
                .ParameterName = "@DoctorName"
                .Value = strDoctorName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaDoctorName)

            Dim objParaMachineName As New SqlParameter
            With objParaMachineName
                .ParameterName = "@MachineName"
                .Value = strMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMachineName)

            Dim objParamSpeakerRow As New SqlParameter
            With objParamSpeakerRow
                .ParameterName = "@SpeakerRowNum"
                .Value = _speakerRow
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParamSpeakerRow)

            objCmd.Connection = objCon
            Dim objSQLDataReader As SqlDataReader
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(ds)
            ds.Tables(0).TableName = "SpeakerName"
            ds.Tables(1).TableName = "SpeakerCount"
            'objSQLDataReader = objCmd.ExecuteReader()
            'If objSQLDataReader.HasRows = True Then
            '    objSQLDataReader.Read()
            '    strSpeaker = objSQLDataReader.Item(0)
            'End If
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing

            End If
            objParaMachineName = Nothing
            objParaDoctorName = Nothing

            objSQLDataReader = Nothing
            Return ds
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorSpeakerConfiguration -- RetrieveSpeaker -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorSpeakerConfiguration -- RetrieveSpeaker -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
End Class
