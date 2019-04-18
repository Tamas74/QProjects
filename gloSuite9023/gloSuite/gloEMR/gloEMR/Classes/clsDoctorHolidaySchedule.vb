Imports System.Data.SqlClient
Public Class clsDoctorHolidaySchedule
#Region "   Private Variables"
    Enum enmScheduleCriteria
        Today
        Tommarrow
        NextWeek
        NextMonth
        All
    End Enum
    Dim _nScheduleID As Long
    Dim _nDoctorID As Int64
    Dim _sDoctorName As String = ""
    Dim _dtFromDate As DateTime
    Dim _dtToDate As DateTime
    Dim _sComments As String = ""
#End Region
#Region "   Public Properties"
    Public Property Comments() As String
        Get
            Return _sComments
        End Get
        Set(ByVal Value As String)
            _sComments = Value
        End Set
    End Property
    Public Property ToDate() As DateTime
        Get
            Return _dtToDate
        End Get
        Set(ByVal Value As DateTime)
            _dtToDate = Value
        End Set
    End Property
    Public Property FromDate() As DateTime
        Get
            Return _dtFromDate
        End Get
        Set(ByVal Value As DateTime)
            _dtFromDate = Value
        End Set
    End Property
    Public Property DoctorName() As String
        Get
            Return _sDoctorName
        End Get
        Set(ByVal Value As String)
            _sDoctorName = Value
        End Set
    End Property
    Public Property DoctorID() As Int64
        Get
            Return _nDoctorID
        End Get
        Set(ByVal Value As Int64)
            _nDoctorID = Value
        End Set
    End Property
    Public Property ScheduleID() As Long
        Get
            Return _nScheduleID
        End Get
        Set(ByVal Value As Long)
            _nScheduleID = Value
        End Set
    End Property

#End Region
#Region "   Public Functions"
    Public Function AddSchedule() As Boolean
        Return AddSchedule(_sDoctorName, _dtFromDate, _dtToDate, _sComments)
    End Function
    Public Function AddSchedule(ByVal strDoctorName As String, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal strComments As String) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpDoctorHolidaySchedule"

            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFromDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFromDate)

            Dim objParaToDate As New SqlParameter
            With objParaToDate
                .ParameterName = "@ToDate"
                .Value = dtToDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaToDate)

            Dim objParaProvider As New SqlParameter
            With objParaProvider
                .ParameterName = "@ProviderName"
                .Value = strDoctorName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProvider)

            Dim objParaComments As New SqlParameter
            With objParaComments
                .ParameterName = "@Comments"
                .Value = strComments
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaComments)

            Dim objParaMachine As New SqlParameter
            With objParaMachine
                .ParameterName = "@MachineID"
                .Value = GetPrefixTransactionID()
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaMachine)
            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objParaMachine = Nothing
            objParaComments = Nothing
            objParaProvider = Nothing
            objParaToDate = Nothing
            objParaFromDate = Nothing

            objCon = Nothing
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorHolidaySchedule -- AddSchedule -- " & ex.ToString)
            objCon.Close()
            objCon.Dispose()

            Return (False)
        Catch ex As Exception
            UpdateLog("clsDoctorHolidaySchedule -- AddSchedule -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objCon.Close()
            objCon.Dispose()
            Return False
        End Try
    End Function
    Public Function UpdateSchedule(ByVal nScheduleID As Long, ByVal strDoctorName As String, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal strComments As String) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpDoctorHolidaySchedule"

            Dim objParaScheduleID As New SqlParameter
            With objParaScheduleID
                .ParameterName = "@ScheduleID"
                .Value = nScheduleID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaScheduleID)


            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFromDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFromDate)

            Dim objParaToDate As New SqlParameter
            With objParaToDate
                .ParameterName = "@ToDate"
                .Value = dtToDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaToDate)



            Dim objParaProvider As New SqlParameter
            With objParaProvider
                .ParameterName = "@ProviderName"
                .Value = strDoctorName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProvider)

            Dim objParaComments As New SqlParameter
            With objParaComments
                .ParameterName = "@Comments"
                .Value = strComments
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaComments)

            Dim objParaMachine As New SqlParameter
            With objParaMachine
                .ParameterName = "@MachineID"
                .Value = GetPrefixTransactionID()
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaMachine)

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objParaMachine = Nothing
            objParaComments = Nothing
            objParaProvider = Nothing
            objParaToDate = Nothing
            objParaFromDate = Nothing
            objParaScheduleID = Nothing

            objCmd = Nothing
            objCon = Nothing
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorHolidaySchedule -- UpdateSchedule -- " & ex.ToString)
            objCon.Close()
            objCon.Dispose()
            Return False
        Catch ex As Exception
            UpdateLog("clsDoctorHolidaySchedule -- UpdateSchedule -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objCon.Close()
            objCon.Dispose()
            Return False
        End Try
    End Function
    Public Function UpdateSchedule(ByVal nScheduleID As Long) As Boolean
        Return UpdateSchedule(nScheduleID, _sDoctorName, _dtFromDate, _dtToDate, _sComments)
    End Function
    Public Function DeleteSchedule(ByVal nScheduleID As Long) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeleteDoctorHolidaySchedule"

            Dim objParaScheduleID As New SqlParameter
            With objParaScheduleID
                .ParameterName = "@ScheduleID"
                .Value = nScheduleID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaScheduleID)

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objParaScheduleID = Nothing
            objCon = Nothing
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorHolidaySchedule -- DeleteSchedule -- " & ex.ToString)
            objCon.Close()
            objCon.Dispose()
            Return False
        Catch ex As Exception
            UpdateLog("clsDoctorHolidaySchedule -- DeleteSchedule -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objCon.Close()
            objCon.Dispose()
            Return False
        End Try
    End Function
    Public Function IsDoctorAvailable(ByVal strProviderName As String, ByVal dtDate As DateTime) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_CheckDoctorAvailability"
            Dim objParaProviderName As New SqlParameter
            With objParaProviderName
                .ParameterName = "@ProviderName"
                .Value = strProviderName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProviderName)

            Dim objParaDate As New SqlParameter
            With objParaDate
                .ParameterName = "@Date"
                .Value = dtDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaDate)

            objCmd.Connection = objCon
            Dim nCount As Integer
            objCon.Open()
            nCount = objCmd.ExecuteScalar
            objCon.Close()
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objParaDate = Nothing
            objParaProviderName = Nothing

            objCon = Nothing
            If nCount = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorHolidaySchedule -- IsDoctorAvailable -- " & ex.ToString)
            objCon.Close()
            objCon.Dispose()
            Return False
        Catch ex As Exception
            UpdateLog("clsDoctorHolidaySchedule -- IsDoctorAvailable -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objCon.Close()
            objCon.Dispose()
            Return False
        End Try
    End Function
    Public Function RetrieveSchedule(ByVal strProviderName As String, ByVal dtFrom As Date, ByVal dtTo As Date) As DataTable
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillDoctorHolidaySchedule"
            objCmd.Connection = objCon
            Dim objParaFrom As New SqlParameter
            With objParaFrom
                .ParameterName = "@FromDate"
                .Value = dtFrom.AddDays(-1)
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFrom)

            'Dim objParaTo As New SqlParameter
            'With objParaTo
            '    .ParameterName = "@ToDate"
            '    '.Value = dtTo
            '    .Value = dtFrom.AddMonths(6)
            '    .Direction = ParameterDirection.Input
            '    .SqlDbType = SqlDbType.DateTime
            'End With
            'objCmd.Parameters.Add(objParaTo)

            Dim objParaProvider As New SqlParameter
            With objParaProvider
                .ParameterName = "@ProviderName"
                .Value = strProviderName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProvider)

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            objDA.Dispose()
            objCon.Close()
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objParaProvider = Nothing
            objParaFrom = Nothing

            Return dtTable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorHolidaySchedule -- RetrieveSchedule(1) -- " & ex.ToString)
            objCon.Close()
            objCon.Dispose()
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorHolidaySchedule -- RetrieveSchedule(1) -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objCon.Close()
            objCon.Dispose()
            Return Nothing
        End Try
    End Function
    Public Function RetrieveSchedule(ByVal strProvideName As String, ByVal enmCriteria As enmScheduleCriteria) As DataTable
        Dim dtToDate As DateTime
        Dim dtFromDate As DateTime
        Try
            dtFromDate = System.DateTime.Now.Date
            Select Case enmCriteria
                Case enmScheduleCriteria.Today
                    dtToDate = System.DateTime.Now.Date
                Case enmScheduleCriteria.Tommarrow
                    dtFromDate = System.DateTime.Now.Date.AddDays(1)
                    dtToDate = System.DateTime.Now.Date.AddDays(1)
                Case enmScheduleCriteria.NextWeek
                    dtFromDate = System.DateTime.Now.Date.AddDays(1)
                    dtToDate = System.DateTime.Now.Date.AddDays(7)
                Case enmScheduleCriteria.NextMonth
                    dtFromDate = System.DateTime.Now.Date.AddDays(1)
                    dtToDate = System.DateTime.Now.Date.AddMonths(1)
            End Select
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            ' Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillDoctorHolidaySchedule"
            objCmd.Parameters.Clear()
            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFromDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFromDate)

            If Not (enmCriteria = enmScheduleCriteria.All) Then
                Dim objParaToDate As New SqlParameter
                With objParaToDate
                    .ParameterName = "@ToDate"
                    .Value = dtToDate.Date
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.DateTime
                End With
                objCmd.Parameters.Add(objParaToDate)
                objParaToDate = Nothing
            End If

            Dim objParaProvider As New SqlParameter
            With objParaProvider
                .ParameterName = "@ProviderName"
                .Value = strProvideName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProvider)

            If enmCriteria = enmScheduleCriteria.All Then
                Dim objParaAll As New SqlParameter
                With objParaAll
                    .ParameterName = "@blnAll"
                    .Value = 1
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaAll)
                objParaAll = Nothing
            End If

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            objDA.Dispose()
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objParaFromDate = Nothing
            objParaProvider = Nothing
            Return dtTable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorHolidaySchedule -- RetrieveSchedule(2) -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorHolidaySchedule -- RetrieveSchedule(2) -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
#End Region
End Class
