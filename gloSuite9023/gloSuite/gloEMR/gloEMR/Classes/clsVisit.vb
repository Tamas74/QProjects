Imports System.Data.SqlClient
Public Class clsVisit
    Dim _nVisitID As Long
    Dim _nPatientID As Long
    Dim _sPatientCode As String
    Dim _sPatientFirstName As String
    Dim _sPatientLastName As String
    Dim _nProviderID As Long
    Dim _sProvider As String
    Dim _dtVisitDate As DateTime
    Dim _sComplaints As String
    Dim _sVisitType As String
    Dim _blnConfirmation As Boolean
    '  Dim Dv As DataView
    

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
    Public Property VisitID() As Long
        Get
            Return _nVisitID
        End Get
        Set(ByVal Value As Long)
            _nVisitID = Value
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
    Public Property ProviderID() As Long
        Get
            Return _nProviderID
        End Get
        Set(ByVal Value As Long)
            _nProviderID = Value
        End Set
    End Property
    Public Property VisitDate() As DateTime
        Get
            Return _dtVisitDate
        End Get
        Set(ByVal Value As DateTime)
            _dtVisitDate = Value
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

    Public Property VisitType() As String
        Get
            Return _sVisitType
        End Get
        Set(ByVal Value As String)
            _sVisitType = Value
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
    Public Function Fill_Visits_1(ByVal dtFrom As Date, ByVal strProviderName As String) As DataSet
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        '''''Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillVisits"
        objCmd.Connection = objCon
        Dim objParaFrom As New SqlParameter
        With objParaFrom
            .ParameterName = "@FromDate"
            .Value = dtFrom
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaFrom)

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
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon.Dispose()
        objCon = Nothing
        objDA.Dispose()
        objDA = Nothing

        objParaFrom = Nothing
        objParaProvider = Nothing

        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If

        Return dsData
    End Function
    Public Function Fill_Visits_New(ByVal strInterval As String, ByVal strProviderName As String) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim cmd As New SqlCommand("gsp_ViewVisits", objCon)
        cmd.CommandType = CommandType.StoredProcedure
        'cmd.Parameters.Add("@Interval", strInterval)
        'cmd.Parameters.Add("@ProviderName", strProviderName)
        'cmd.Parameters.Add("@dtSysdate", Now)
        cmd.Parameters.AddWithValue("@Interval", strInterval)
        cmd.Parameters.AddWithValue("@ProviderName", strProviderName)
        cmd.Parameters.AddWithValue("@dtSysdate", Now)

        Dim da As SqlDataAdapter
        Dim dt As DataTable
        objCon.Open()
        da = New SqlDataAdapter
        da.SelectCommand = cmd
        dt = New DataTable
        da.Fill(dt)
        objCon.Close()
        objCon.Dispose()
        objCon = Nothing
        da.Dispose()
        da = Nothing

        If cmd IsNot Nothing Then
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End If
        Return dt
    End Function
    Public Function Fill_Visits(ByVal dtFrom As Date, ByVal dtTo As Date, ByVal strProviderName As String) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim cmd As New SqlCommand("gsp_FillAllVisits", objCon)
        cmd.CommandType = CommandType.StoredProcedure

        'cmd.Parameters.Add("@FromDate", dtFrom)
        'cmd.Parameters.Add("@ToDate", dtTo)
        'cmd.Parameters.Add("@ProviderName", strProviderName)
        'cmd.Parameters.Add("@FromDate", dtFrom)
        'cmd.Parameters.Add("@ToDate", dtTo)
        'cmd.Parameters.Add("@ProviderName", strProviderName)

        cmd.Parameters.AddWithValue("@FromDate", dtFrom)
        cmd.Parameters.AddWithValue("@ToDate", dtTo)
        cmd.Parameters.AddWithValue("@ProviderName", strProviderName)
        cmd.Parameters.AddWithValue("@FromDate", dtFrom)
        cmd.Parameters.AddWithValue("@ToDate", dtTo)
        cmd.Parameters.AddWithValue("@ProviderName", strProviderName)

        Dim da As SqlDataAdapter
        Dim dt As DataTable
        objCon.Open()
        da = New SqlDataAdapter
        da.SelectCommand = cmd
        dt = New DataTable
        da.Fill(dt)
        objCon.Close()
        objCon.Dispose()
        objCon = Nothing
        da.Dispose()
        da = Nothing
        If cmd IsNot Nothing Then
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End If

        Return dt
    End Function
    Public Function GetAllProvider() As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Try
            Dim cmd As New SqlCommand("gsp_FillProvider_MST", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            Dim da As SqlDataAdapter
            Dim dt As DataTable
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd

            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            da.Dispose()
            da = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
        End Try
    End Function

    Public Sub AddData(ByVal ArrList As ArrayList)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()

        Dim TrVisit As SqlTransaction = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter
        Try
            cmd = New System.Data.SqlClient.SqlCommand("gsp_InUpVisits", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            Dim objParamPatientId As SqlParameter


            objParamPatientId = cmd.Parameters.Add("@nVisitId", SqlDbType.BigInt)
            objParamPatientId.Direction = ParameterDirection.InputOutput
            If ArrList.Item(0) = 0 Then
                objParamPatientId.Value = 0
            Else
                objParamPatientId.Value = ArrList.Item(0)
            End If

            objParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(1), Long)

            objParam = cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(2), Long)


            objParam = cmd.Parameters.Add("@nAppointmentID", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(3), System.Int64) '0 'Appointment ID 0 for without appointment

            objParam = cmd.Parameters.Add("@dtVisitDate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            'objParam.Value = Format(ArrList.Item(2), "D")
            objParam.Value = ArrList.Item(4)

            objCon.Open()
            TrVisit = objCon.BeginTransaction
            cmd.Transaction = TrVisit
            cmd.ExecuteNonQuery()
            TrVisit.Commit()
            objCon.Close()
            objParamPatientId = Nothing
            'Dim objAudit As New clsAudit
            'If ArrList.Item(0) = 0 Then
            '    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Visit Added for " & gstrPatientFirstName & " " & gstrPatientLastName, gloAuditTrail.ActivityOutCome.Success)
            '    ''Added Rahul P on 20101011
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Visit Added for " & gstrPatientFirstName & " " & gstrPatientLastName, CType(ArrList.Item(1), Long), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '    ''
            '    'objAudit.CreateLog(clsAudit.enmActivityType.Add, "Visit Added for " & gstrPatientFirstName & " " & gstrPatientLastName, gstrLoginName, gstrClientMachineName, CType(ArrList.Item(1), System.Int64))
            'Else
            '    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Visit modified for " & gstrPatientFirstName & " " & gstrPatientLastName, gloAuditTrail.ActivityOutCome.Success)
            '    ''Added Rahul P on 20101011
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Visit modified for " & gstrPatientFirstName & " " & gstrPatientLastName, CType(ArrList.Item(1), Long), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '    ''
            '    'objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Visit modified for " & gstrPatientFirstName & " " & gstrPatientLastName, gstrLoginName, gstrClientMachineName, CType(ArrList.Item(1), System.Int64))
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            TrVisit.Rollback()
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If TrVisit IsNot Nothing Then

                TrVisit.Dispose()
                TrVisit = Nothing
            End If
            If objCon IsNot Nothing Then

                objCon.Dispose()
                objCon = Nothing
            End If
            objParam = Nothing
        End Try
    End Sub

    Public Sub SearchVisit(ByVal nVisitID As Long)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader = Nothing
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveVisits"

        Dim objParaNotesID As New SqlParameter
        With objParaNotesID
            .ParameterName = "@VisitID"
            .Value = nVisitID
            .Direction = ParameterDirection.Input
            '.SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaNotesID)
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        objSQLDataReader.Read()
        _nPatientID = objSQLDataReader.Item("PatientID")
        _sPatientCode = objSQLDataReader.Item("PatientCode")
        _sPatientFirstName = objSQLDataReader.Item("PatientFirstName")
        _sPatientLastName = objSQLDataReader.Item("PatientLastName")
        '_sProvider = objSQLDataReader.Item("Provider")
        '_dtVisitDate = objSQLDataReader.Item("VisitDate")
        '_sComplaints = objSQLDataReader.Item("Complaints")
        '_sVisitType = objSQLDataReader.Item("VisitType")
        objSQLDataReader.Close()
        objSQLDataReader = Nothing
        objCon.Close()
        objCon.Dispose()
        objCon = Nothing
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaNotesID = Nothing
        objCon = Nothing
    End Sub

    Public Function DeleteVisit(ByVal nVisitID As Long) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_DeleteVisit"
        Dim objParaFromDate As New SqlParameter
        With objParaFromDate
            .ParameterName = "@VisitID"
            .Value = nVisitID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaFromDate)
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
        objParaFromDate = Nothing
        objCon = Nothing
        Return True
    End Function

    Public Sub UpdateVisitDateTime(ByVal nVisitID As Long, ByVal dtVisitDateTime As DateTime, ByVal strProviderName As String)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_UpdateVisitDateTime"
        Dim objParaFromDate As New SqlParameter
        With objParaFromDate
            .ParameterName = "@VisitID"
            .Value = nVisitID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaFromDate)

        Dim objParaAppDate As New SqlParameter
        With objParaAppDate
            .ParameterName = "@VisitDateTime"
            .Value = dtVisitDateTime
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaAppDate)


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
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCon.Dispose()
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaProvider = Nothing
        objParaAppDate = Nothing
        objParaFromDate = Nothing
        objCmd = Nothing
        objCon = Nothing
    End Sub
    Public Function RetrieveProvider(ByVal nVisitID As Long) As String
        Dim strProvider As String = ""
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveProviderFromVisit"

        Dim objParaVisitID As New SqlParameter
        With objParaVisitID
            .ParameterName = "@VisitID"
            .Value = nVisitID
            .Direction = ParameterDirection.Input
            '.SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaVisitID)
        objCmd.Connection = objCon
        objCon.Open()
        If IsNothing(objCmd.ExecuteScalar) = False Then
            strProvider = objCmd.ExecuteScalar()
        End If
        objCon.Close()
        objCon.Dispose()
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaVisitID = Nothing
        objCon = Nothing
        Return strProvider
    End Function

    Public Function ChangeVisitsProvider(ByVal nVisitID As Long, ByVal nProviderID As Long) As Boolean
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_UpdateVisitsProvider"
            objCmd.Connection = objCon

            Dim objParaVisitID As New SqlParameter
            With objParaVisitID
                .ParameterName = "@VisitID"
                .Value = nVisitID
                .Direction = ParameterDirection.Input
                '.SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaVisitID)

            Dim objParaProviderID As New SqlParameter
            With objParaProviderID
                .ParameterName = "@ProviderID"
                .Value = nProviderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaProviderID)


            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objParaProviderID = Nothing
            objParaVisitID = Nothing
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

   
End Class
