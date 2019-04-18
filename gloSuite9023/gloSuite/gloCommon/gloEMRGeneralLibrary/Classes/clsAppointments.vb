Imports System.Data.SqlClient
Imports System.Windows.Forms

Namespace gloGeneral
    Public Class clsAppointments
#Region "   Private Variables"
        Dim _nAppointmentID As Long
        Dim _nPatientID As Long
        Dim _sPatientCode As String
        Dim _sPatientFirstName As String
        Dim _sPatientLastName As String
        Dim _nProviderID As Integer
        Dim _sProvider As String
        Dim _dtAppointmentDate As DateTime
        Dim _nAppointmentInterval As Integer
        Dim _sComplaints As String
        Dim _sAppointmentType As String
        Dim _blnConfirmation As Boolean
        Dim _sAppointmentSchedulerType As String
        Dim _sColorCode As String
        Dim _blnPULLChartAppointment As Boolean
        Dim _nAppointmentGroupID As Integer
        'Dim Dv As DataView
#End Region
#Region "   Public Properties"
        Public Property AppointmentGroupID() As Integer
            Get
                Return _nAppointmentGroupID
            End Get
            Set(ByVal Value As Integer)
                _nAppointmentGroupID = Value
            End Set
        End Property
        Public Property IsPullChartsAppointment() As Boolean
            Get
                Return _blnPULLChartAppointment
            End Get
            Set(ByVal Value As Boolean)
                _blnPULLChartAppointment = Value
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
        Public Property AppointmentSchedulerType() As String
            Get
                Return _sAppointmentSchedulerType
            End Get
            Set(ByVal Value As String)
                _sAppointmentSchedulerType = Value
            End Set
        End Property
        Public Property PatientCode() As String
            Get
                Return _sPatientCode
            End Get
            Set(ByVal Value As String)
                _sPatientCode = Value
            End Set
        End Property
        Public Property Provider() As String
            Get
                Return _sProvider
            End Get
            Set(ByVal Value As String)
                _sProvider = Value
            End Set
        End Property
        Public Property PatientLastName() As String
            Get
                Return _sPatientLastName
            End Get
            Set(ByVal Value As String)
                _sPatientLastName = Value
            End Set
        End Property
        Public Property PatientFirstName() As String
            Get
                Return _sPatientFirstName
            End Get
            Set(ByVal Value As String)
                _sPatientFirstName = Value
            End Set
        End Property
        Public Property AppointmentID() As Long
            Get
                Return _nAppointmentID
            End Get
            Set(ByVal Value As Long)
                _nAppointmentID = Value
            End Set
        End Property
        Public Property PatientID() As Long
            Get
                Return _nPatientID
            End Get
            Set(ByVal Value As Long)
                _nPatientID = Value
            End Set
        End Property
        Public Property ProviderID() As Integer
            Get
                Return _nProviderID
            End Get
            Set(ByVal Value As Integer)
                _nProviderID = Value
            End Set
        End Property
        Public Property AppointmentDate() As DateTime
            Get
                Return _dtAppointmentDate
            End Get
            Set(ByVal Value As DateTime)
                _dtAppointmentDate = Value
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
        Public Property Complaints() As String
            Get
                Return _sComplaints
            End Get
            Set(ByVal Value As String)
                _sComplaints = Value
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
        Public Property Confirmation() As Boolean
            Get
                Return _blnConfirmation
            End Get
            Set(ByVal Value As Boolean)
                _blnConfirmation = Value
            End Set
        End Property

#End Region
#Region "   Public Functions"
        ''Public Function Fill_Appointments(ByVal dtFrom As Date, ByVal strProviderName As String) As DataSet
        ''    Dim objCon As New SqlConnection
        ''    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        ''    Dim objCmd As New SqlCommand
        ''    ' Dim objSQLDataReader As SqlDataReader
        ''    objCmd.CommandType = CommandType.StoredProcedure
        ''    objCmd.CommandText = "gsp_FillAppointments"
        ''    objCmd.Connection = objCon
        ''    Dim objParaFrom As New SqlParameter
        ''    With objParaFrom
        ''        .ParameterName = "@FromDate"
        ''        .Value = dtFrom
        ''        .Direction = ParameterDirection.Input
        ''        .SqlDbType = SqlDbType.DateTime
        ''    End With
        ''    objCmd.Parameters.Add(objParaFrom)

        ''    Dim objParaProvider As New SqlParameter
        ''    With objParaProvider
        ''        .ParameterName = "@ProviderName"
        ''        .Value = strProviderName
        ''        .Direction = ParameterDirection.Input
        ''        .SqlDbType = SqlDbType.VarChar
        ''    End With
        ''    objCmd.Parameters.Add(objParaProvider)

        ''    objCmd.Connection = objCon
        ''    objCon.Open()
        ''    Dim objDA As New SqlDataAdapter(objCmd)
        ''    Dim dsData As New DataSet
        ''    objDA.Fill(dsData)
        ''    objCon.Close()
        ''    objCon = Nothing
        ''    Return dsData
        ''End Function
        ''Public Function Fill_Appointments(ByVal dtFrom As Date, ByVal dtTo As Date, ByVal strProviderName As String) As DataSet
        ''    Dim objCon As New SqlConnection
        ''    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        ''    Dim objCmd As New SqlCommand
        ''    'Dim objSQLDataReader As SqlDataReader
        ''    objCmd.CommandType = CommandType.StoredProcedure
        ''    objCmd.CommandText = "gsp_FillAppointments"
        ''    objCmd.Connection = objCon
        ''    Dim objParaFrom As New SqlParameter
        ''    With objParaFrom
        ''        .ParameterName = "@FromDate"
        ''        .Value = dtFrom
        ''        .Direction = ParameterDirection.Input
        ''        .SqlDbType = SqlDbType.DateTime
        ''    End With
        ''    objCmd.Parameters.Add(objParaFrom)

        ''    Dim objParaTo As New SqlParameter
        ''    With objParaTo
        ''        .ParameterName = "@ToDate"
        ''        .Value = dtTo
        ''        .Direction = ParameterDirection.Input
        ''        .SqlDbType = SqlDbType.DateTime
        ''    End With
        ''    objCmd.Parameters.Add(objParaTo)

        ''    Dim objParaProvider As New SqlParameter
        ''    With objParaProvider
        ''        .ParameterName = "@ProviderName"
        ''        .Value = strProviderName
        ''        .Direction = ParameterDirection.Input
        ''        .SqlDbType = SqlDbType.VarChar
        ''    End With
        ''    objCmd.Parameters.Add(objParaProvider)

        ''    objCmd.Connection = objCon
        ''    objCon.Open()
        ''    Dim objDA As New SqlDataAdapter(objCmd)
        ''    Dim dsData As New DataSet
        ''    objDA.Fill(dsData)
        ''    objCon.Close()
        ''    objCon = Nothing
        ''    Return dsData
        ''End Function
        ''Public Function Fill_AppointmentedPatients(ByVal dtAppointmentDate As DateTime, Optional ByVal strProviderName As String = "All", Optional ByVal strLocation As String = "All") As DataTable
        ''    Dim objCon As New SqlConnection
        ''    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        ''    Dim objCmd As New SqlCommand
        ''    'Dim objSQLDataReader As SqlDataReader
        ''    objCmd.CommandType = CommandType.StoredProcedure
        ''    objCmd.CommandText = "gsp_FillAppointmentedPatients"
        ''    objCmd.Connection = objCon

        ''    Dim objParaAppointmentDate As New SqlParameter
        ''    With objParaAppointmentDate
        ''        .ParameterName = "@AppointmentDate"
        ''        .Value = dtAppointmentDate.Date
        ''        .Direction = ParameterDirection.Input
        ''        .SqlDbType = SqlDbType.VarChar
        ''    End With
        ''    objCmd.Parameters.Add(objParaAppointmentDate)


        ''    Dim objParaProvider As New SqlParameter
        ''    With objParaProvider
        ''        .ParameterName = "@ProviderName"
        ''        .Value = Trim(strProviderName)
        ''        .Direction = ParameterDirection.Input
        ''        .SqlDbType = SqlDbType.VarChar
        ''    End With
        ''    objCmd.Parameters.Add(objParaProvider)


        ''    Dim objParaLocation As New SqlParameter
        ''    With objParaLocation
        ''        .ParameterName = "@Location"
        ''        .Value = Trim(strLocation)
        ''        .Direction = ParameterDirection.Input
        ''        .SqlDbType = SqlDbType.VarChar
        ''    End With
        ''    objCmd.Parameters.Add(objParaLocation)

        ''    objCmd.Connection = objCon
        ''    objCon.Open()
        ''    Dim objDA As New SqlDataAdapter(objCmd)
        ''    Dim dtTable As New DataTable
        ''    objDA.Fill(dtTable)
        ''    objCon.Close()
        ''    objCon = Nothing
        ''    Return dtTable
        ''End Function
        ''Public Function GetAllProvider() As DataTable
        ''    Dim objCon As New SqlConnection
        ''    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        ''    Try
        ''        Dim cmd As New SqlCommand("gsp_FillProvider_MST", objCon)
        ''        cmd.CommandType = CommandType.StoredProcedure
        ''        Dim da As SqlDataAdapter
        ''        Dim dt As DataTable
        ''        objCon.Open()
        ''        da = New SqlDataAdapter
        ''        da.SelectCommand = cmd

        ''        dt = New DataTable
        ''        da.Fill(dt)
        ''        objCon.Close()
        ''        Return dt
        ''    Catch ex As SqlException
        ''        MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''        Return Nothing
        ''    Catch ex As Exception
        ''        MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''        Return Nothing
        ''    Finally
        ''        objCon.Close()
        ''    End Try
        ''End Function
        ''Public Sub AddData(ByVal ArrList As ArrayList)
        ''    Dim objCon As New SqlConnection
        ''    objCon.ConnectionString = clsgeneral.GetConnectionstring()()

        ''    Dim TrAppointment As SqlTransaction
        ''    Dim cmd As SqlCommand
        ''    Try
        ''        cmd = New System.Data.SqlClient.SqlCommand("gsp_InUpAppointments", objCon)
        ''        cmd.CommandType = CommandType.StoredProcedure
        ''        Dim objParamPatientId As SqlParameter

        ''        Dim objParam As SqlParameter
        ''        objParamPatientId = cmd.Parameters.Add("@nAppointmentId", SqlDbType.BigInt)
        ''        objParamPatientId.Direction = ParameterDirection.InputOutput
        ''        If ArrList.Item(0) = 0 Then
        ''            objParamPatientId.Value = 0
        ''        Else
        ''            objParamPatientId.Value = ArrList.Item(0)
        ''        End If

        ''        objParam = cmd.Parameters.AddWithValue("@nPatientID", CType(ArrList.Item(1), Long))
        ''        objParam.Direction = ParameterDirection.Input

        ''        objParam = cmd.Parameters.AddWithValue("@nProviderID", CType(ArrList.Item(2), Long))
        ''        objParam.Direction = ParameterDirection.Input


        ''        objParam = cmd.Parameters.Add("@dtAppointmentDate", SqlDbType.DateTime)
        ''        objParam.Direction = ParameterDirection.Input
        ''        'objParam.Value = Format(ArrList.Item(2), "D")
        ''        objParam.Value = ArrList.Item(3)

        ''        'objParam = cmd.Parameters.Add("@dtAppointmentTime", SqlDbType.DateTime)
        ''        'objParam.Direction = ParameterDirection.Input
        ''        ''objParam.Value = Format(ArrList.Item(3), "Medium Time")
        ''        'objParam.Value = ArrList.Item(3)

        ''        objParam = cmd.Parameters.Add("@sComplaints", SqlDbType.VarChar, 255)
        ''        objParam.Direction = ParameterDirection.Input
        ''        objParam.Value = CType(ArrList.Item(4), System.String)

        ''        objParam = cmd.Parameters.Add("@sAppointmentType", SqlDbType.VarChar, 50)
        ''        objParam.Direction = ParameterDirection.Input
        ''        objParam.Value = CType(ArrList.Item(5), System.String)

        ''        objParam = cmd.Parameters.Add("@AppointmentInterval", SqlDbType.Int, 9)
        ''        objParam.Direction = ParameterDirection.Input
        ''        objParam.Value = CType(ArrList.Item(6), System.Int64)

        ''        If ArrList.Item(0) = 0 Then
        ''            objParam = cmd.Parameters.Add("@AppointmentSchedulerType", SqlDbType.VarChar, 255)
        ''            objParam.Direction = ParameterDirection.Input
        ''            objParam.Value = ArrList.Item(7)

        ''            objParam = cmd.Parameters.Add("@ColorCode", SqlDbType.VarChar, 255)
        ''            objParam.Direction = ParameterDirection.Input
        ''            If CStr(ArrList.Item(10)) = "" And ArrList.Item(7) <> "None" Then
        ''                Dim objAppScheduler As New clsAppointmentScheduler
        ''                objAppScheduler.FillAppointmentSchedulerTypesDetails(Trim(ArrList.Item(7)))
        ''                objParam.Value = objAppScheduler.ColorCode
        ''                objAppScheduler = Nothing
        ''            Else
        ''                objParam.Value = CStr(ArrList.Item(10))
        ''            End If
        ''        End If

        ''        If IsNothing(ArrList.Item(8)) = False Then
        ''            objParam = cmd.Parameters.Add("@bIsPullCharts", SqlDbType.Bit, 1)
        ''            objParam.Direction = ParameterDirection.Input
        ''            objParam.Value = ArrList.Item(8)
        ''        End If

        ''        If IsNothing(ArrList.Item(9)) = False Then
        ''            objParam = cmd.Parameters.Add("@AppointmentGroupID", SqlDbType.Int, 1)
        ''            objParam.Direction = ParameterDirection.Input
        ''            objParam.Value = ArrList.Item(9)
        ''        End If

        ''        objParam = cmd.Parameters.AddWithValue("@MachineID", clsgeneral.GetPrefixTransactionID())
        ''        objParam.Direction = ParameterDirection.Input



        ''        objCon.Open()
        ''        TrAppointment = objCon.BeginTransaction
        ''        cmd.Transaction = TrAppointment
        ''        cmd.ExecuteNonQuery()
        ''        TrAppointment.Commit()
        ''        Dim objAudit As New clsAudit
        ''        If ArrList.Item(0) = 0 Then
        ''            objAudit.CreateLog(clsAudit.enmActivityType.Add, "Appointment Added for " & globalPatient.gstrPatientFirstName & " " & globalPatient.gstrPatientLastName, globalSecurity.gstrLoginName, globalSecurity.gstrClientMachineName, CType(ArrList.Item(1), System.Int64))
        ''        Else
        ''            objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Appointment modified for " & globalPatient.gstrPatientFirstName & " " & globalPatient.gstrPatientLastName, globalSecurity.gstrLoginName, globalSecurity.gstrClientMachineName, CType(ArrList.Item(1), System.Int64))
        ''        End If
        ''        objCon.Close()
        ''    Catch ex As Exception
        ''        MsgBox(ex.Message)
        ''        TrAppointment.Rollback()
        ''    End Try
        ''End Sub
        'Public Sub SearchAppointment(ByVal nAppointmentID As Long)
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        '    Dim objCmd As New SqlCommand
        '    Dim objSQLDataReader As SqlDataReader
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_RetrieveAppointments"

        '    Dim objParaNotesID As New SqlParameter
        '    With objParaNotesID
        '        .ParameterName = "@AppointmentID"
        '        .Value = nAppointmentID
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.BigInt
        '    End With
        '    objCmd.Parameters.Add(objParaNotesID)
        '    objCmd.Connection = objCon
        '    objCon.Open()
        '    objSQLDataReader = objCmd.ExecuteReader()
        '    objSQLDataReader.Read()
        '    _nPatientID = objSQLDataReader.Item("PatientID")
        '    _sPatientCode = objSQLDataReader.Item("PatientCode")
        '    _sPatientFirstName = objSQLDataReader.Item("PatientFirstName")
        '    _sPatientLastName = objSQLDataReader.Item("PatientLastName")
        '    _sProvider = objSQLDataReader.Item("Provider")
        '    _dtAppointmentDate = objSQLDataReader.Item("AppointmentDate")
        '    _sComplaints = objSQLDataReader.Item("Complaints")
        '    _sAppointmentType = objSQLDataReader.Item("AppointmentType")
        '    _nAppointmentInterval = objSQLDataReader.Item("AppointmentInterval")
        '    _sAppointmentSchedulerType = objSQLDataReader.Item("AppointmentSchedulerType")

        '    'Pull Charts Appointment
        '    If IsNothing(objSQLDataReader.Item("PullCharts")) = False Then
        '        If objSQLDataReader.Item("PullCharts") = 0 Then
        '            _blnPULLChartAppointment = False
        '        Else
        '            _blnPULLChartAppointment = True
        '        End If
        '    Else
        '        _blnPULLChartAppointment = False
        '    End If
        '    'Appointment Group ID
        '    If IsNothing(objSQLDataReader.Item("AppointmentGroupID")) = False Then
        '        _nAppointmentGroupID = objSQLDataReader.Item("AppointmentGroupID")
        '    Else
        '        _nAppointmentGroupID = 0
        '    End If
        '    objCon.Close()
        '    objCmd = Nothing
        '    objCon = Nothing
        'End Sub
        'Public Function DeleteAppointment(ByVal nAppointmentID As Long) As Boolean
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        '    Dim objCmd As New SqlCommand
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_DeleteAppointment"
        '    Dim objParaFromDate As New SqlParameter
        '    With objParaFromDate
        '        .ParameterName = "@AppointmentID"
        '        .Value = nAppointmentID
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.BigInt
        '    End With
        '    objCmd.Parameters.Add(objParaFromDate)
        '    objCmd.Connection = objCon
        '    objCon.Open()
        '    objCmd.ExecuteNonQuery()
        '    objCon.Close()
        '    objCmd = Nothing
        '    objCon = Nothing
        '    Return True
        'End Function
        ''Delete Appointment Group
        ''This will delete all those appointments in the same group except those appointments.. Patient Visits are registered
        'Public Function DeleteAppointmentGroup(ByVal nAppointmentID As Long) As Boolean
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        '    Dim objCmd As New SqlCommand
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_DeleteAppointmentGroup"
        '    Dim objParaFromDate As New SqlParameter
        '    With objParaFromDate
        '        .ParameterName = "@AppointmentID"
        '        .Value = nAppointmentID
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.BigInt
        '    End With
        '    objCmd.Parameters.Add(objParaFromDate)
        '    objCmd.Connection = objCon
        '    objCon.Open()
        '    objCmd.ExecuteNonQuery()
        '    objCon.Close()
        '    objCmd = Nothing
        '    objCon = Nothing
        '    Return True
        'End Function
        'Public Sub UpdateAppointmentDateTime(ByVal nAppointmentID As Long, ByVal dtAppointmentDateTime As DateTime, ByVal strProviderName As String)
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        '    Dim objCmd As New SqlCommand
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_UpdateAppointmentDateTime"
        '    Dim objParaFromDate As New SqlParameter
        '    With objParaFromDate
        '        .ParameterName = "@AppointmentID"
        '        .Value = nAppointmentID
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.BigInt
        '    End With
        '    objCmd.Parameters.Add(objParaFromDate)

        '    Dim objParaAppDate As New SqlParameter
        '    With objParaAppDate
        '        .ParameterName = "@AppointmentDateTime"
        '        .Value = dtAppointmentDateTime
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.DateTime
        '    End With
        '    objCmd.Parameters.Add(objParaAppDate)


        '    Dim objParaProvider As New SqlParameter
        '    With objParaProvider
        '        .ParameterName = "@ProviderName"
        '        .Value = strProviderName
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.VarChar
        '    End With
        '    objCmd.Parameters.Add(objParaProvider)


        '    objCmd.Connection = objCon
        '    objCon.Open()
        '    objCmd.ExecuteNonQuery()
        '    objCon.Close()
        '    objCmd = Nothing
        '    objCon = Nothing
        'End Sub
        'Public Function CheckAppointmentAvailable(ByVal dtAppointmentDate As DateTime, ByVal nAppointmentInterval As Int16, ByVal strProviderName As String, Optional ByVal nAppointmentID As Long = 0) As Boolean
        '    Dim nAppointments As Long = 0
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        '    Dim objCmd As New SqlCommand
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_CheckAppointmentExists"

        '    Dim objParaAppDate As New SqlParameter
        '    With objParaAppDate
        '        .ParameterName = "@AppointmentDate"
        '        .Value = dtAppointmentDate
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.DateTime
        '    End With
        '    objCmd.Parameters.Add(objParaAppDate)


        '    Dim objParaProvider As New SqlParameter
        '    With objParaProvider
        '        .ParameterName = "@ProviderName"
        '        .Value = strProviderName
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.VarChar
        '    End With
        '    objCmd.Parameters.Add(objParaProvider)

        '    If nAppointmentID <> 0 Then
        '        Dim objParaAppointmentID As New SqlParameter
        '        With objParaAppointmentID
        '            .ParameterName = "@AppointmentID"
        '            .Value = nAppointmentID
        '            .Direction = ParameterDirection.Input
        '            .SqlDbType = SqlDbType.BigInt
        '        End With
        '        objCmd.Parameters.Add(objParaAppointmentID)
        '    End If

        '    objCmd.Connection = objCon
        '    objCon.Open()
        '    If IsNothing(objCmd.ExecuteScalar) = False Then
        '        nAppointments = objCmd.ExecuteScalar
        '    End If

        '    If nAppointments = 0 Then
        '        'Now Check for Appointment End Date
        '        objParaAppDate.Value = dtAppointmentDate.AddMinutes(nAppointmentInterval - 1)
        '        If IsNothing(objCmd.ExecuteScalar) = False Then
        '            nAppointments = objCmd.ExecuteScalar
        '        End If
        '    End If
        '    objCon.Close()
        '    objCmd = Nothing
        '    objCon = Nothing
        '    If nAppointments = 0 Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'End Function
        'Public Function CheckAppointmentAvailable(ByVal dtAppointmentStartDate As DateTime, ByVal dtAppointmentEndDate As DateTime, ByVal strProviderName As String, Optional ByVal nAppointmentID As Long = 0) As Boolean
        '    Dim nAppointments As Long = 0
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        '    Dim objCmd As New SqlCommand
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_CheckAppointmentExists"

        '    Dim objParaAppDate As New SqlParameter
        '    With objParaAppDate
        '        .ParameterName = "@AppointmentDate"
        '        .Value = dtAppointmentStartDate
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.DateTime
        '    End With
        '    objCmd.Parameters.Add(objParaAppDate)


        '    Dim objParaProvider As New SqlParameter
        '    With objParaProvider
        '        .ParameterName = "@ProviderName"
        '        .Value = strProviderName
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.VarChar
        '    End With
        '    objCmd.Parameters.Add(objParaProvider)

        '    If nAppointmentID <> 0 Then
        '        Dim objParaAppointmentID As New SqlParameter
        '        With objParaAppointmentID
        '            .ParameterName = "@AppointmentID"
        '            .Value = nAppointmentID
        '            .Direction = ParameterDirection.Input
        '            .SqlDbType = SqlDbType.BigInt
        '        End With
        '        objCmd.Parameters.Add(objParaAppointmentID)
        '    End If

        '    objCmd.Connection = objCon
        '    objCon.Open()
        '    If IsNothing(objCmd.ExecuteScalar) = False Then
        '        nAppointments = objCmd.ExecuteScalar
        '    End If

        '    If nAppointments = 0 Then
        '        'Now Check for Appointment End Date
        '        objParaAppDate.Value = dtAppointmentEndDate.AddMinutes(-1)
        '        If IsNothing(objCmd.ExecuteScalar) = False Then
        '            nAppointments = objCmd.ExecuteScalar
        '        End If
        '    End If
        '    objCon.Close()
        '    objCmd = Nothing
        '    objCon = Nothing
        '    If nAppointments = 0 Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'End Function
        'Public Function MaxAppointmentGroupID() As Integer
        '    Dim nMaxAppointmentGroupID As Integer
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        '    Dim objCmd As New SqlCommand
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_RetrieveMAXAppointmentGroupID"
        '    objCmd.Connection = objCon
        '    objCon.Open()
        '    nMaxAppointmentGroupID = objCmd.ExecuteScalar
        '    objCon.Close()
        '    objCon = Nothing
        '    Return nMaxAppointmentGroupID
        'End Function
        'Public Function IsMissingAppointment(ByVal nAppointmentID As Long) As Boolean
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        '    Dim objCmd As New SqlCommand
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_CheckMissingAppointment"
        '    objCmd.Connection = objCon

        '    'Appointment ID
        '    Dim objParaAppointmentID As New SqlParameter
        '    With objParaAppointmentID
        '        .ParameterName = "@AppointmentID"
        '        .Value = nAppointmentID
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.BigInt
        '    End With
        '    objCmd.Parameters.Add(objParaAppointmentID)

        '    'Missing Appointment
        '    Dim nMissingAppointment As Byte

        '    objCon.Open()
        '    nMissingAppointment = objCmd.ExecuteScalar()
        '    objCon.Close()
        '    objCon = Nothing
        '    If nMissingAppointment = 0 Then
        '        Return False
        '    Else
        '        Return True
        '    End If
        'End Function
        'Public Function IsVisitRegistered(ByVal nAppointmentID As Long) As Boolean
        '    Return Not IsMissingAppointment(nAppointmentID)
        'End Function
        'Public Function GetLastPullChartsDate(ByVal dtPullChartsDate As DateTime) As DateTime
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        '    Dim objCmd As New SqlCommand
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_GetLastPullCharts"
        '    objCmd.Connection = objCon

        '    'PULL CHARTS Date
        '    Dim objParaPullChartsDate As New SqlParameter
        '    With objParaPullChartsDate
        '        .ParameterName = "@PullChartsDate"
        '        .Value = dtPullChartsDate.Date
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.DateTime
        '    End With
        '    objCmd.Parameters.Add(objParaPullChartsDate)

        '    'Missing Appointment
        '    Dim dtLastPullChartsDate As DateTime

        '    objCon.Open()
        '    If IsNothing(objCmd.ExecuteScalar()) = False Then
        '        If IsDBNull(objCmd.ExecuteScalar()) = False Then
        '            dtLastPullChartsDate = objCmd.ExecuteScalar()
        '        Else
        '            dtLastPullChartsDate = dtPullChartsDate
        '        End If
        '    Else
        '        dtLastPullChartsDate = dtPullChartsDate
        '    End If
        '    objCon.Close()
        '    objCon = Nothing
        '    Return dtLastPullChartsDate
        'End Function

        ''To get the appointment of specific Patient on Specific Date
        ''If appointment exists then it will return Appointment ID otherwise 0
        'Public Function GetPatientAppointment(ByVal dtAppointmentDate As DateTime, ByVal nPatientID As Long) As Long
        '    Dim objCon As New SqlConnection
        '    objCon.ConnectionString = clsgeneral.GetConnectionstring()()
        '    Dim objCmd As New SqlCommand
        '    objCmd.CommandType = CommandType.StoredProcedure
        '    objCmd.CommandText = "gsp_GetAppointment"
        '    objCmd.Connection = objCon

        '    'Appointment Date
        '    Dim objParaAppointmentDate As New SqlParameter
        '    With objParaAppointmentDate
        '        .ParameterName = "@AppointmentDate"
        '        .Value = dtAppointmentDate.Date
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.DateTime
        '    End With
        '    objCmd.Parameters.Add(objParaAppointmentDate)

        '    'Patient ID
        '    Dim objParaPatientID As New SqlParameter
        '    With objParaPatientID
        '        .ParameterName = "@PatientID"
        '        .Value = nPatientID
        '        .Direction = ParameterDirection.Input
        '        ' .SqlDbType = SqlDbType.Int
        '    End With
        '    objCmd.Parameters.Add(objParaPatientID)


        '    'Appointment ID
        '    Dim nAppointmentID As Long

        '    objCon.Open()
        '    nAppointmentID = objCmd.ExecuteScalar()
        '    If IsNothing(nAppointmentID) = False Then
        '        nAppointmentID = 0
        '    End If
        '    objCon.Close()
        '    objCon = Nothing
        '    Return nAppointmentID
        'End Function

#End Region
    End Class
End Namespace