Imports System.Data.SqlClient
Public Class clsAppointmentScheduler
#Region "   Private Variables"
    Enum enmAppointmentUpToDurationType
        Days
        Weeks
        Months
    End Enum
    Enum enmAppointmentIntervalType
        Daily
        Weekly
        Monthly
    End Enum
    Dim _nAppointmentSchedulerTypeID As Integer
    Dim _sAppointmentType As String
    Dim _nAppointmentUpToDuration As Integer
    Dim _nAppointmentUpToDurationType As Byte
    Dim _nAppointmentIntervalType As Byte
    Dim _nAppointmentInterval As Integer
    Dim _nAppointmentDuration As Integer
    Dim _sColorCode As String
#End Region
#Region "   Public Properties"
    Public Property AppointmentSchedulerTypeID() As Integer
        Get
            Return _nAppointmentSchedulerTypeID
        End Get
        Set(ByVal Value As Integer)
            _nAppointmentSchedulerTypeID = Value
        End Set
    End Property
    Public Property AppointmentType() As String
        Get
            Return _sAppointmentType
        End Get
        Set(ByVal Value As String)
            _sAppointmentType = Value
        End Set
    End Property
    Public Property AppointmentUpToDuration() As Integer
        Get
            Return _nAppointmentUpToDuration
        End Get
        Set(ByVal Value As Integer)
            _nAppointmentUpToDuration = Value
        End Set
    End Property
    Public Property AppointmentUpToDurationType() As enmAppointmentUpToDurationType
        Get
            Select Case _nAppointmentUpToDurationType
                Case 0
                    Return enmAppointmentUpToDurationType.Days
                Case 1
                    Return enmAppointmentUpToDurationType.Weeks
                Case 2
                    Return enmAppointmentUpToDurationType.Months
                Case Else
                    Return 0
            End Select
        End Get
        Set(ByVal Value As enmAppointmentUpToDurationType)
            Select Case Value
                Case enmAppointmentUpToDurationType.Days
                    _nAppointmentUpToDurationType = 0
                Case enmAppointmentUpToDurationType.Weeks
                    _nAppointmentUpToDurationType = 1
                Case enmAppointmentUpToDurationType.Months
                    _nAppointmentUpToDurationType = 2
            End Select
        End Set
    End Property
    Public Property AppointmentIntervalType() As enmAppointmentIntervalType
        Get
            Select Case _nAppointmentIntervalType
                Case 0
                    Return enmAppointmentIntervalType.Daily
                Case 1
                    Return enmAppointmentIntervalType.Weekly
                Case 2
                    Return enmAppointmentIntervalType.Monthly
                Case Else
                    Return 0
            End Select
        End Get
        Set(ByVal Value As enmAppointmentIntervalType)
            Select Case Value
                Case enmAppointmentIntervalType.Daily
                    _nAppointmentIntervalType = 0
                Case enmAppointmentIntervalType.Weekly
                    _nAppointmentIntervalType = 1
                Case enmAppointmentIntervalType.Monthly
                    _nAppointmentIntervalType = 2
            End Select
        End Set
    End Property
    Public Property AppointmentInterval() As Integer
        Get
            Return _nAppointmentInterval
        End Get
        Set(ByVal Value As Integer)
            _nAppointmentInterval = Value
        End Set
    End Property
    Public Property AppointmentDuration() As Integer
        Get
            Return _nAppointmentDuration
        End Get
        Set(ByVal Value As Integer)
            _nAppointmentDuration = Value
        End Set
    End Property
    Public Property ColorCode() As String
        Get
            Return _sColorCode
        End Get
        Set(ByVal Value As String)
            _sColorCode = Value
        End Set
    End Property

#End Region
#Region "   Public Functions"
    Public Function DeleteAppointmentSchedulerType(ByVal sAppointmentSchedulerType As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeleteAppointmentSchedulerType"
            Dim objParaAppointmentSchedulerType As New SqlParameter
            With objParaAppointmentSchedulerType
                .ParameterName = "@AppointmentSchedulerType"
                .Value = sAppointmentSchedulerType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaAppointmentSchedulerType)
            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()

            objParaAppointmentSchedulerType = Nothing
            'objCon = Nothing
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Appointment Scheduler Deleted", gstrLoginName, gstrClientMachineName)
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsAppointmentScheduler -- DeleteAppointmentSchedulerType -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
            UpdateLog("clsAppointmentScheduler -- DeleteAppointmentSchedulerType -- " & ex.ToString)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function
    Public Function AddAppointmentSchedulerType() As Boolean
        Return AddAppointmentSchedulerType(_sAppointmentType, _nAppointmentUpToDuration, AppointmentUpToDurationType, AppointmentIntervalType, _nAppointmentInterval, _nAppointmentDuration, _sColorCode)
    End Function
    Public Function AddAppointmentSchedulerType(ByVal sAppointmentType As String, ByVal nAppointmentUpToDuration As Integer, ByVal enmAppUpToDurationType As enmAppointmentUpToDurationType, ByVal enmAppIntervalType As enmAppointmentIntervalType, ByVal nAppointmentInterval As Integer, ByVal nAppointmentDuration As Integer, ByVal sColorCode As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpAppointmentSchedulerType"

            Dim objParaAppointmentType As New SqlParameter
            With objParaAppointmentType
                .ParameterName = "@AppointmentType"
                .Value = sAppointmentType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaAppointmentType)


            Dim objParaAppointmentUpToDuration As New SqlParameter
            With objParaAppointmentUpToDuration
                .ParameterName = "@AppointmentUpToDuration"
                .Value = nAppointmentUpToDuration
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentUpToDuration)

            Dim objParaAppointmentUpToDurationType As New SqlParameter
            With objParaAppointmentUpToDurationType
                .ParameterName = "@AppointmentUpToDurationType"
                Select Case enmAppUpToDurationType
                    Case enmAppointmentUpToDurationType.Days
                        .Value = 0
                    Case enmAppointmentUpToDurationType.Weeks
                        .Value = 1
                    Case enmAppointmentUpToDurationType.Months
                        .Value = 2
                End Select
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentUpToDurationType)

            Dim objParaAppointmentIntervalType As New SqlParameter
            With objParaAppointmentIntervalType
                .ParameterName = "@AppointmentIntervalType"
                Select Case enmAppIntervalType
                    Case enmAppointmentIntervalType.Daily
                        .Value = 0
                    Case enmAppointmentIntervalType.Weekly
                        .Value = 1
                    Case enmAppointmentIntervalType.Monthly
                        .Value = 2
                End Select
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentIntervalType)

            Dim objParaAppointmentInterval As New SqlParameter
            With objParaAppointmentInterval
                .ParameterName = "@AppointmentInterval"
                .Value = nAppointmentInterval
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentInterval)

            Dim objParaAppointmentDuration As New SqlParameter
            With objParaAppointmentDuration
                .ParameterName = "@AppointmentDuration"
                .Value = nAppointmentDuration
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentDuration)

            Dim objParaColorCode As New SqlParameter
            With objParaColorCode
                .ParameterName = "@ColorCode"
                .Value = sColorCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaColorCode)

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
            'objCon = Nothing

            objParaAppointmentType = Nothing
            objParaAppointmentUpToDuration = Nothing
            objParaAppointmentUpToDurationType = Nothing
            objParaAppointmentIntervalType = Nothing
            objParaAppointmentInterval = Nothing
            objParaAppointmentDuration = Nothing
            objParaColorCode = Nothing

            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Appointment Scheduler Added", gstrLoginName, gstrClientMachineName)
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsAppointmentScheduler -- AddAppointmentSchedulerType -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsAppointmentScheduler -- AddAppointmentSchedulerType -- " & ex.ToString)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
    Public Function ModifyAppointmentSchedulerType(ByVal nAppointmentSchedulerTypeID As Integer) As Boolean
        Return ModifyAppointmentSchedulerType(nAppointmentSchedulerTypeID, _sAppointmentType, _nAppointmentUpToDuration, AppointmentUpToDurationType, AppointmentIntervalType, _nAppointmentInterval, _nAppointmentDuration, _sColorCode)
    End Function
    Public Function ModifyAppointmentSchedulerType(ByVal nAppointmentSchedulerTypeID As Integer, ByVal sAppointmentType As String, ByVal nAppointmentUpToDuration As Integer, ByVal enmAppUpToDurationType As enmAppointmentUpToDurationType, ByVal enmAppIntervalType As enmAppointmentIntervalType, ByVal nAppointmentInterval As Integer, ByVal nAppointmentDuration As Integer, ByVal sColorCode As String) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            ' Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpAppointmentSchedulerType"

            Dim objParaAppointmentID As New SqlParameter
            With objParaAppointmentID
                .ParameterName = "@AppointmentSchedulerTypeID"
                .Value = nAppointmentSchedulerTypeID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentID)

            Dim objParaAppointmentType As New SqlParameter
            With objParaAppointmentType
                .ParameterName = "@AppointmentType"
                .Value = sAppointmentType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaAppointmentType)


            Dim objParaAppointmentUpToDuration As New SqlParameter
            With objParaAppointmentUpToDuration
                .ParameterName = "@AppointmentUpToDuration"
                .Value = nAppointmentUpToDuration
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentUpToDuration)

            Dim objParaAppointmentUpToDurationType As New SqlParameter
            With objParaAppointmentUpToDurationType
                .ParameterName = "@AppointmentUpToDurationType"
                Select Case enmAppUpToDurationType
                    Case enmAppointmentUpToDurationType.Days
                        .Value = 0
                    Case enmAppointmentUpToDurationType.Weeks
                        .Value = 1
                    Case enmAppointmentUpToDurationType.Months
                        .Value = 2
                End Select
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentUpToDurationType)

            Dim objParaAppointmentIntervalType As New SqlParameter
            With objParaAppointmentIntervalType
                .ParameterName = "@AppointmentIntervalType"
                Select Case enmAppIntervalType
                    Case enmAppointmentIntervalType.Daily
                        .Value = 0
                    Case enmAppointmentIntervalType.Weekly
                        .Value = 1
                    Case enmAppointmentIntervalType.Monthly
                        .Value = 2
                End Select
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentIntervalType)

            Dim objParaAppointmentInterval As New SqlParameter
            With objParaAppointmentInterval
                .ParameterName = "@AppointmentInterval"
                .Value = nAppointmentInterval
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentInterval)

            Dim objParaAppointmentDuration As New SqlParameter
            With objParaAppointmentDuration
                .ParameterName = "@AppointmentDuration"
                .Value = nAppointmentDuration
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAppointmentDuration)

            Dim objParaColorCode As New SqlParameter
            With objParaColorCode
                .ParameterName = "@ColorCode"
                .Value = sColorCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaColorCode)

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
            'objCon = Nothing

            objParaAppointmentID = Nothing
            objParaAppointmentType = Nothing
            objParaAppointmentUpToDuration = Nothing
            objParaAppointmentUpToDurationType = Nothing
            objParaAppointmentIntervalType = Nothing
            objParaAppointmentInterval = Nothing
            objParaAppointmentDuration = Nothing
            objParaColorCode = Nothing

            Return True
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Appointment Scheduler Modified", gstrLoginName, gstrClientMachineName)
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsAppointmentScheduler -- ModifyAppointmentSchedulerType -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
            UpdateLog("clsAppointmentScheduler -- ModifyAppointmentSchedulerType -- " & ex.ToString)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
    Public Function FillAppointmentSchedulerTypes() As Collection
        Dim clAppointmentSchedulerTypes As New Collection
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            Dim objSQLDataReader As SqlDataReader
            objCmd.Connection = objCon
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillAppointmentSchedulerType"
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader()
            While objSQLDataReader.Read
                clAppointmentSchedulerTypes.Add(objSQLDataReader.Item(0))
            End While
            objSQLDataReader.Close()
            objCon.Close()

            objCmd.Dispose()
            objCmd = Nothing

            objSQLDataReader = Nothing
            'objCon = Nothing
            Return clAppointmentSchedulerTypes
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsAppointmentScheduler -- FillAppointmentSchedulerTypes -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
            UpdateLog("clsAppointmentScheduler -- FillAppointmentSchedulerTypes -- " & ex.ToString)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
    Public Sub FillAppointmentSchedulerTypesDetails(ByVal strAppointmentType As String)
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            Dim objSQLDataReader As SqlDataReader
            objCmd.Connection = objCon
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveAppointmentSchedulerTypes"
            Dim objParaAppointmentType As New SqlParameter
            With objParaAppointmentType
                .ParameterName = "@AppointmentType"
                .Value = strAppointmentType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaAppointmentType)
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader()
            If objSQLDataReader.HasRows = True Then
                objSQLDataReader.Read()
                If IsDBNull(objSQLDataReader.Item(0)) = False Then
                    _nAppointmentSchedulerTypeID = objSQLDataReader.Item(0)
                End If
                If IsDBNull(objSQLDataReader.Item(1)) = False Then
                    _nAppointmentUpToDuration = objSQLDataReader.Item(1)
                End If
                If IsDBNull(objSQLDataReader.Item(2)) = False Then
                    _nAppointmentUpToDurationType = objSQLDataReader.Item(2)
                End If
                If IsDBNull(objSQLDataReader.Item(3)) = False Then
                    _nAppointmentIntervalType = objSQLDataReader.Item(3)
                End If
                If IsDBNull(objSQLDataReader.Item(4)) = False Then
                    _nAppointmentInterval = objSQLDataReader.Item(4)
                End If
                If IsDBNull(objSQLDataReader.Item(5)) = False Then
                    _nAppointmentDuration = objSQLDataReader.Item(5)
                End If
                If IsDBNull(objSQLDataReader.Item(6)) = False Then
                    _sColorCode = objSQLDataReader.Item(6)
                End If
            End If
            objSQLDataReader.Close()
            objCon.Close()

            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing

            objParaAppointmentType = Nothing

            objSQLDataReader = Nothing
            'objCon = Nothing
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsAppointmentScheduler -- FillAppointmentSchedulerTypesDetails -- " & ex.ToString)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsAppointmentScheduler -- FillAppointmentSchedulerTypesDetails -- " & ex.ToString)
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Sub

#End Region

End Class
