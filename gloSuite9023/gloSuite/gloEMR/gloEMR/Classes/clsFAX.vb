Imports System.Data.SqlClient
Imports System.IO

Public Class clsFAX
    Implements IDisposable

#Region "   Private Variables"
    Enum enmDateCriteria
        Today
        Yesterday
        LastWeek
        LastMonth
        Customize
    End Enum
    Enum enmFAXPriority
        NormalPriority
        SendImmediately
    End Enum

    Dim _nFAXID As Int16
    Dim _nPatientID As Long
    Dim _sPatientName As String
    Dim _sFAXTo As String
    Dim _sFAXType As String
    Dim _sFAXNo As String
    Dim _sLoginUser As String
    Dim _sFileName As String
    Dim _dtFAXDate As DateTime
    Dim m_FAXPriority As enmFAXPriority
#End Region
#Region "   Public Properties"
    Public Property FAXID() As Long
        Get
            Return _nFAXID
        End Get
        Set(ByVal Value As Long)
            _nFAXID = Value
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
    Public Property PatientName() As String
        Get
            Return _sPatientName
        End Get
        Set(ByVal Value As String)
            _sPatientName = Value
        End Set
    End Property
    Public Property FAXTo() As String
        Get
            Return _sFAXTo
        End Get
        Set(ByVal Value As String)
            _sFAXTo = Value
        End Set
    End Property
    Public Property FAXType() As String
        Get
            Return _sFAXType
        End Get
        Set(ByVal Value As String)
            _sFAXType = Value
        End Set
    End Property
    Public Property FAXNo() As String
        Get
            Return _sFAXNo
        End Get
        Set(ByVal Value As String)
            _sFAXNo = Value
        End Set
    End Property
    Public Property LoginUser() As String
        Get
            Return _sLoginUser
        End Get
        Set(ByVal Value As String)
            _sLoginUser = Value
        End Set
    End Property
    Public Property FileName() As String
        Get
            Return _sFileName
        End Get
        Set(ByVal Value As String)
            _sFileName = Value
        End Set
    End Property
    Public Property FAXDate() As DateTime
        Get
            Return _dtFAXDate
        End Get
        Set(ByVal Value As DateTime)
            _dtFAXDate = Value
        End Set
    End Property
    Public Property FAXPriority() As enmFAXPriority
        Get
            Return m_FAXPriority
        End Get
        Set(ByVal Value As enmFAXPriority)
            m_FAXPriority = Value
        End Set
    End Property
#End Region
#Region "   Public Functions"    
    Public Function AddPendingFAX(ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXTYpe As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal sFileName As String, ByVal dtFAXDate As DateTime, Optional ByVal CurrentFAXPriority As enmFAXPriority = enmFAXPriority.NormalPriority) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpPendingFAX"

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaPatientID)
            objParaPatientID = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFAXTo As New SqlParameter
            With objParaFAXTo
                .ParameterName = "@FAXTo"
                .Value = sFAXTo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXTo)
            objParaFAXTo = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFAXTYpe As New SqlParameter
            With objParaFAXTYpe
                .ParameterName = "@FAXType"
                .Value = sFAXTYpe
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXTYpe)
            objParaFAXTYpe = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFAXNo As New SqlParameter
            With objParaFAXNo
                .ParameterName = "@FAXNo"
                .Value = sFAXNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXNo)
            objParaFAXNo = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaLoginUser As New SqlParameter
            With objParaLoginUser
                .ParameterName = "@LoginUser"
                .Value = sLoginUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLoginUser)
            objParaLoginUser = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFileName As New SqlParameter
            With objParaFileName
                .ParameterName = "@FileName"
                .Value = sFileName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFileName)
            objParaFileName = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFaxDate As New SqlParameter
            With objParaFaxDate
                .ParameterName = "@FAXDate"
                .Value = dtFAXDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFaxDate)
            objParaFaxDate = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFaxPriority As New SqlParameter
            With objParaFaxPriority
                .ParameterName = "@FAXPriority"
                Select Case CurrentFAXPriority
                    Case enmFAXPriority.NormalPriority
                        .Value = 0
                    Case enmFAXPriority.SendImmediately
                        .Value = 1
                End Select
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaFaxPriority)
            objParaFaxPriority = Nothing 'Change made to solve memory Leak and word crash issue

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()

            objCon.Close()
            objCon.Dispose() 'Change made to solve memory Leak and word crash issue           
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objCon = Nothing

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Fax, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Fax, "Fax Send", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)            
            Throw ex
        Catch ex As Exception            
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Function
    Public Function Fill_PendingFAXes(ByVal enmFAXDateCriteria As enmDateCriteria, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal nPatientID As Long = 0) As DataTable
        Dim dtFAXFromDate As DateTime
        Dim dtFAXToDate As DateTime
        Dim _DateRange() As DateTime
        '''''''''''''''''''Code modifications are done by Anil on 20071113
        Try
            Select Case enmFAXDateCriteria
                Case enmDateCriteria.Today
                    _DateRange = GetDateRange(DateCategory.Today)
                    If _DateRange.Length > 0 Then
                        dtFAXFromDate = _DateRange(0)
                        dtFAXToDate = _DateRange(1)
                    End If
                Case enmDateCriteria.Yesterday
                    _DateRange = GetDateRange(DateCategory.Yesterday)
                    If _DateRange.Length > 0 Then
                        dtFAXFromDate = _DateRange(0)
                        dtFAXToDate = _DateRange(1)
                    End If
                Case enmDateCriteria.LastWeek
                    _DateRange = GetDateRange(DateCategory.LastWeek)
                    If _DateRange.Length > 0 Then
                        dtFAXFromDate = _DateRange(0)
                        dtFAXToDate = _DateRange(1)
                    End If
                Case enmDateCriteria.LastMonth
                    _DateRange = GetDateRange(DateCategory.LastMonth)
                    If _DateRange.Length > 0 Then
                        dtFAXFromDate = _DateRange(0)
                        dtFAXToDate = _DateRange(1)
                    End If
                Case enmDateCriteria.Customize
                    dtFAXFromDate = dtFromDate.Date
                    dtFAXToDate = dtToDate.Date 'AddDays(1)
            End Select
            ''''''''''''''''''''''''''''''''''''''''''''''
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrievePendingFAXes"
            objCmd.Connection = objCon
            Dim objParaFrom As New SqlParameter
            With objParaFrom
                .ParameterName = "@FromDate"
                .Value = dtFAXFromDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFrom)
            objParaFrom = Nothing   'Change made to solve memory Leak and word crash issue

            Dim objParaTo As New SqlParameter
            With objParaTo
                .ParameterName = "@ToDate"
                .Value = dtFAXToDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaTo)
            objParaTo = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaPatientID)
            objParaPatientID = Nothing 'Change made to solve memory Leak and word crash issue

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            objCon.Close()
            objCon.Dispose() 'Change made to solve memory Leak and word crash issue
            objCon = Nothing
            objDA.Dispose() 'Change made to solve memory Leak and word crash issue

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            Return dtTable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Function
    
    Public Function Fill_AllFAXes_Withstatus(ByVal sStatus As String, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal nToprecords As Integer, Optional ByVal nPatientID As Long = 0) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim dtTable As New DataTable
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetAllfaxesWithStatus"
            objCmd.Connection = objCon
            Dim objParaFrom As New SqlParameter
            With objParaFrom
                .ParameterName = "@FromDate"
                .Value = dtFromDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFrom)
            objParaFrom = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaTo As New SqlParameter
            With objParaTo
                .ParameterName = "@ToDate"
                .Value = dtToDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaTo)
            objParaTo = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaPatientID)
            objParaPatientID = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaStatus As New SqlParameter
            With objParaStatus
                .ParameterName = "@Status"
                .Value = sStatus
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaStatus)
            objParaStatus = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaToprec As New SqlParameter
            With objParaToprec
                .ParameterName = "@TopRecords"
                .Value = nToprecords
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaToprec)
            objParaToprec = Nothing 'Change made to solve memory Leak and word crash issue

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)

            objDA.Fill(dtTable)
            objCon.Close()
            'Change made to solve memory Leak and word crash issue
            objDA.Dispose()
            objDA = Nothing
            Return dtTable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Catch ex As Exception            
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            'If Not IsNothing(dtTable) Then
            '    dtTable.Dispose()
            '    dtTable = Nothing
            'End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If           
        End Try
    End Function

    '00000136 : out of memory errors when there are a large number of pending faxes
    'take only those faxes from database which user wants to preview instead of taking all faxes for a patient 
    Public Function RetrievePendingFAX(ByVal nFaxID As Long, Optional ByVal nMaxNoAttempts As Int16 = 0) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim dtTable As New DataTable
        Dim objDA As SqlDataAdapter = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrievePendingFAX"
            objCmd.Connection = objCon

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@FaxID"
                .Value = nFaxID
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaPatientID)

            objParaPatientID = Nothing

            If nMaxNoAttempts > 0 Then
                Dim objParaMAXAttempts As New SqlParameter
                With objParaMAXAttempts
                    .ParameterName = "@MaxNoOfAttempts"
                    .Value = nMaxNoAttempts
                    .Direction = ParameterDirection.Input
                End With
                objCmd.Parameters.Add(objParaMAXAttempts)
                objParaMAXAttempts = Nothing
            End If

            objCmd.Connection = objCon
            objCon.Open()

            objDA = New SqlDataAdapter(objCmd)
            objDA.Fill(dtTable)

            objCon.Close()
            Return dtTable
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            'If Not IsNothing(dtTable) Then
            '    dtTable.Dispose()
            '    dtTable = Nothing
            'End If

            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If

            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            If Not IsNothing(objCon) Then
                If objCon.State <> ConnectionState.Closed Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function Fill_PendingFAXes_Report(ByVal sStatus As String, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal nTopRecords As Integer, Optional ByVal nPatientID As Long = 0, Optional ByVal nMaxNoAttempts As Int16 = 0) As DataTable

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim dtTable As New DataTable
        Try
            objCon.ConnectionString = GetConnectionString()

            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveAllPendingFAXes_Report"
            objCmd.Connection = objCon

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaPatientID)
            objParaPatientID = Nothing 'Change made to solve memory Leak and word crash issue

            If nMaxNoAttempts > 0 Then
                Dim objParaMAXAttempts As New SqlParameter
                With objParaMAXAttempts
                    .ParameterName = "@MaxNoOfAttempts"
                    .Value = nMaxNoAttempts
                    .Direction = ParameterDirection.Input
                End With
                objCmd.Parameters.Add(objParaMAXAttempts)
                objParaMAXAttempts = Nothing 'Change made to solve memory Leak and word crash issue
            End If
            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFromDate
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaFromDate)
            objParaFromDate = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaToDate As New SqlParameter
            With objParaToDate
                .ParameterName = "@ToDate"
                .Value = dtToDate
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaToDate)
            objParaToDate = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaStatus As New SqlParameter
            With objParaStatus
                .ParameterName = "@Status"
                .Value = sStatus
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaStatus)
            objParaStatus = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaToprec As New SqlParameter
            With objParaToprec
                .ParameterName = "@TopRecords"
                .Value = nTopRecords
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaToprec)
            objParaToprec = Nothing 'Change made to solve memory Leak and word crash issue

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)

            objDA.Fill(dtTable)
            objCon.Close()
            'Change made to solve memory Leak and word crash issue
            objDA.Dispose()
            objDA = Nothing
            Return dtTable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            'If Not IsNothing(dtTable) Then
            '    dtTable.Dispose()
            '    dtTable = Nothing
            'End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
    Public Function GetPharmacyFaxNoForRx(ByVal PharmacyId As Long) As mytable
        Dim Conn As SqlConnection = Nothing
        Dim sqlconn As String
        Dim dt As New DataTable
        Dim cmd As New SqlCommand
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim objmytable As mytable
        Try
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
            Dim strquery As String = ""
            strquery = " select isnull(c.sFax,''),isnull(c.sname,''),isnull(c.sPhone,'') from contacts_mst c where c.ncontactid=" & PharmacyId & ""

            cmd.Connection = Conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strquery
            sqladpt = New SqlDataAdapter(cmd)
            sqladpt.Fill(dt)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    objmytable = New mytable(CType(dt.Rows(0)(0), System.String), CType(dt.Rows(0)(1), System.String))
                Else
                    objmytable = New mytable("", "")
                End If
            Else
                objmytable = New mytable("", "")
            End If
            Return objmytable
        Catch ex As Exception
            Throw ex
        Finally
            'Change made to solve memory Leak and word crash issue
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            If Not IsNothing(Conn) Then
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
                Conn.Dispose()
                Conn = Nothing
            End If            
            If Not dt Is Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not sqladpt Is Nothing Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function
    Public Function GetPharmacyFAXNo(ByVal nPatientID As Long) As mytable
        Dim objParam As SqlParameter
        Dim Conn As SqlConnection = Nothing        
        Dim dreader As SqlDataReader
        Dim objmytable As mytable = Nothing
        Dim sqlconn As String
        Try
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
            Dim cmdsql As New SqlCommand("gsp_GetFaxNo", Conn)
            cmdsql.CommandType = CommandType.StoredProcedure
            objParam = cmdsql.Parameters.AddWithValue("@nPatientId", nPatientID)            
            objParam.Direction = ParameterDirection.Input
            Conn.Open()
            dreader = cmdsql.ExecuteReader
            If Not IsNothing(dreader) Then
                If dreader.HasRows = True Then
                    dreader.Read()
                    objmytable = New mytable(CType(dreader.Item(0), System.String), CType(dreader.Item(1), System.String))
                Else
                    objmytable = New mytable("", "")
                End If
            End If
            dreader.Close()
            dreader = Nothing 'Change made to solve memory Leak and word crash issue
            Conn.Close()

            If cmdsql IsNot Nothing Then
                cmdsql.Parameters.Clear()
                cmdsql.Dispose()
                cmdsql = Nothing
            End If

            Return objmytable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If IsNothing(Conn) = False Then
                Conn.Dispose()
                Conn = Nothing
            End If
            objParam = Nothing
        End Try
    End Function

    Public Function GetContactFAXNo(ByVal nContactID As Long) As String
        Dim strFAXNo As String = ""
        Dim objParam As SqlParameter = Nothing
        Dim Conn As SqlConnection = Nothing

        Try
            Dim sqlconn As String
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
            Dim cmdsql As New SqlCommand("gsp_GetContactFaxNo", Conn)
            cmdsql.CommandType = CommandType.StoredProcedure
            objParam = cmdsql.Parameters.Add("@nContactID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nContactID

            Conn.Open()
            strFAXNo = Convert.ToString(cmdsql.ExecuteScalar)

            Conn.Close()

            If cmdsql IsNot Nothing Then
                cmdsql.Parameters.Clear()
                cmdsql.Dispose()
                cmdsql = Nothing
            End If

            Return strFAXNo
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)            
            Return strFAXNo
        Catch ex As Exception            
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return strFAXNo
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not Conn Is Nothing Then
                Conn.Dispose()
                Conn = Nothing
            End If

            objParam = Nothing
        End Try
    End Function    
    Public Function RetrieveFAXAttemptsDetails(ByVal nFAXID As Int64) As DataTable
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand            
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillFAXAttemptsDetails"
            objCmd.Connection = objCon

            Dim objParaFAXID As New SqlParameter
            With objParaFAXID
                .ParameterName = "@FAXID"
                .Value = nFAXID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaFAXID)

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            'Change made to solve memory Leak and word crash issue

            objParaFAXID = Nothing
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            objDA.Dispose()
            objDA = Nothing
            Return dtTable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex            
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Function
    Public Function ReInitialisePendingFAX(ByVal nFAXID As Int64, Optional ByVal strFAXNo As String = "") As Boolean
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand            
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ReinitialisePendingFAX"
            objCmd.Connection = objCon

            Dim objParaFAXID As New SqlParameter
            With objParaFAXID
                .ParameterName = "@FAXID"
                .Value = nFAXID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaFAXID)

            Dim objParaFAXNo As New SqlParameter
            With objParaFAXNo
                .ParameterName = "@FAXNo"
                .Value = strFAXNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXNo)

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

            objParaFAXNo = Nothing
            objParaFAXID = Nothing
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)            
            Throw ex
            Return False
        Catch ex As Exception            
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return False
        End Try
    End Function
    ''Added by Mayuri:20100527
    Public Function DeletePendingNormalFAX(ByVal strFileName As String) As Boolean
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand            
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeletePendingNormalFAX"
            objCmd.Connection = objCon

            Dim objParaFileName As New SqlParameter
            With objParaFileName
                .ParameterName = "@FileName"
                If IsDBNull(strFileName) = False Then
                    .Value = strFileName
                Else
                    .Value = ""
                End If
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFileName)

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

            objParaFileName = Nothing

            objCon = Nothing
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)            
            Throw ex
            Return False
        Catch ex As Exception            
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return False
        End Try

    End Function
    ''End 20100527           
    'sarika Delete Sent Fax 20090428 
    Public Function DeletePendingFAX_ID(ByVal nFaxID As Long) As Boolean
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand            
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeletePendingFAX"
            objCmd.Connection = objCon

            Dim objParaFileName As New SqlParameter
            With objParaFileName
                .ParameterName = "@FAXID"
                .Value = nFaxID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaFileName)
            objParaFileName = Nothing 'Change made to solve memory Leak and word crash issue
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

            objCon = Nothing
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)            
            Throw ex
            Return False
        Catch ex As Exception            
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return False
        End Try
    End Function

    'sarika Delete Sent Fax 20090428
    Public Function DeleteSentFAX(ByVal nFaxID As Long) As Boolean
        Dim _strSQL As String = ""
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand            
            _strSQL = "delete from fax where nfaxid = " & nFaxID

            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = _strSQL
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

            objCon = Nothing

            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsFAX -- DeletePendingFAX -- " & ex.ToString)
            Return False
        Catch ex As Exception
            UpdateLog("clsFAX -- DeletePendingFAX -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

    End Function
    Public Function GetFaxStatus(ByVal FaxID As Long) As String
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim sCurrentStatus As String = ""
        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            With objCmd
                .Connection = objCon
                .CommandType = CommandType.Text
                .CommandText = "select isnull(sCurrentStatus,'') as sCurrentStatus from faxPending_mst where nFAXID = " & FaxID
            End With

            sCurrentStatus = objCmd.ExecuteScalar

            Return sCurrentStatus
        Catch ex As Exception
            MessageBox.Show("Error deleting the Pending fax. " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            'Change made to solve memory Leak and word crash issue
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            objCon.Dispose()
            objCon = Nothing 'Change made to solve memory Leak and word crash issue
        End Try
    End Function

    'sarika internet fax
    Public Function AddPendingFAX1(ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXTYpe As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal sFileName As String, ByVal dtFAXDate As DateTime, ByVal BinaryFile As String, ByVal EFax_DocumentExtension As String, Optional ByVal CurrentFAXPriority As enmFAXPriority = enmFAXPriority.NormalPriority, Optional ByVal EFax_CoverPageDocumentExtension As String = "docx", Optional ByVal nNoOfAttempts As Int32 = 0, Optional ByVal sCurrentStatus As String = "Pending", Optional ByVal TransactionID As String = "", Optional ByVal Status As String = "", Optional ByVal TransResultCode As String = "", Optional ByVal FaxCoverPageBinaryData As Byte() = Nothing, Optional ByVal EFax_Resolution As String = "", Optional ByVal EFax_DocumentEncodingType As String = "base64", Optional ByVal EFax_DocumentContentType As String = "", Optional ByVal EFax_BillingCode As String = "", Optional ByVal EFax_Tiff_image_flag As String = "false") As Int64
        Dim objCon As New SqlConnection
        Dim nFaxID As Int64 = 0

        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand            
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "Fax_InUpPendingEFAX"

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaPatientID)
            objParaPatientID = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFAXTo As New SqlParameter
            With objParaFAXTo
                .ParameterName = "@FAXTo"
                .Value = sFAXTo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXTo)
            objParaFAXTo = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFAXTYpe As New SqlParameter
            With objParaFAXTYpe
                .ParameterName = "@FAXType"
                .Value = sFAXTYpe
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXTYpe)
            objParaFAXTYpe = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFAXNo As New SqlParameter
            With objParaFAXNo
                .ParameterName = "@FAXNo"
                .Value = sFAXNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXNo)
            objParaFAXNo = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaLoginUser As New SqlParameter
            With objParaLoginUser
                .ParameterName = "@LoginUser"
                .Value = sLoginUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLoginUser)
            objParaLoginUser = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFileName As New SqlParameter
            With objParaFileName
                .ParameterName = "@FileName"
                .Value = ""
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFileName)
            objParaFileName = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFaxDate As New SqlParameter
            With objParaFaxDate
                .ParameterName = "@FAXDate"
                .Value = dtFAXDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFaxDate)
            objParaFaxDate = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFaxFileBinaryData As New SqlParameter
            With objParaFaxFileBinaryData
                .ParameterName = "@FaxFileBinaryData"
                .Value = BinaryFile
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFaxFileBinaryData)
            objParaFaxFileBinaryData = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaEFax_DocumentExtension As New SqlParameter
            With objParaEFax_DocumentExtension
                .ParameterName = "@EFax_DocumentExtension"
                .Value = EFax_DocumentExtension
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_DocumentExtension)
            objParaEFax_DocumentExtension = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaEFax_CoverPageDocumentExtension As New SqlParameter
            With objParaEFax_CoverPageDocumentExtension
                .ParameterName = "@EFax_CoverPageDocumentExtension"
                .Value = EFax_CoverPageDocumentExtension
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_CoverPageDocumentExtension)
            objParaEFax_CoverPageDocumentExtension = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFaxPriority As New SqlParameter
            With objParaFaxPriority
                .ParameterName = "@FAXPriority"
                Select Case CurrentFAXPriority
                    Case enmFAXPriority.NormalPriority
                        .Value = 0
                    Case enmFAXPriority.SendImmediately
                        .Value = 1
                End Select
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaFaxPriority)
            objParaFaxPriority = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaNoOfAttempts As New SqlParameter
            With objParaNoOfAttempts
                .ParameterName = "@nNoOfAttempts"
                .Value = nNoOfAttempts
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaNoOfAttempts)
            objParaNoOfAttempts = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaCurrentStatus As New SqlParameter
            With objParaCurrentStatus
                .ParameterName = "@sCurrentStatus"
                .Value = sCurrentStatus
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCurrentStatus)
            objParaCurrentStatus = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaTransactionID As New SqlParameter
            With objParaTransactionID
                .ParameterName = "@TransactionID"
                .Value = TransactionID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaTransactionID)
            objParaTransactionID = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaStatus As New SqlParameter
            With objParaStatus
                .ParameterName = "@Status"
                .Value = Status
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaStatus)
            objParaStatus = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaTransResultCode As New SqlParameter
            With objParaTransResultCode
                .ParameterName = "@TransResultCode"
                .Value = TransResultCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaTransResultCode)
            objParaTransResultCode = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFaxCoverPageBinaryData As New SqlParameter
            With objParaFaxCoverPageBinaryData
                .ParameterName = "@FaxCoverPageBinaryData"
                .Value = FaxCoverPageBinaryData
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Image
            End With
            objCmd.Parameters.Add(objParaFaxCoverPageBinaryData)
            objParaFaxCoverPageBinaryData = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaEFax_Resolution As New SqlParameter
            With objParaEFax_Resolution
                .ParameterName = "@EFax_Resolution"
                .Value = EFax_Resolution
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_Resolution)
            objParaEFax_Resolution = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaEFax_DocumentEncodingType As New SqlParameter
            With objParaEFax_DocumentEncodingType
                .ParameterName = "@EFax_DocumentEncodingType"
                .Value = EFax_DocumentEncodingType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_DocumentEncodingType)
            objParaEFax_DocumentEncodingType = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaEFax_DocumentContentType As New SqlParameter
            With objParaEFax_DocumentContentType
                .ParameterName = "@EFax_DocumentContentType"
                .Value = EFax_DocumentContentType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_DocumentContentType)
            objParaEFax_DocumentContentType = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaEFax_BillingCode As New SqlParameter
            With objParaEFax_BillingCode
                .ParameterName = "@EFax_BillingCode"
                .Value = EFax_BillingCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_BillingCode)
            objParaEFax_BillingCode = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaEFax_Tiff_image_flag As New SqlParameter
            With objParaEFax_Tiff_image_flag
                .ParameterName = "@EFax_Tiff_image_flag"
                .Value = EFax_Tiff_image_flag
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_Tiff_image_flag)
            objParaEFax_Tiff_image_flag = Nothing 'Change made to solve memory Leak and word crash issue

            Dim objParaFaxID As New SqlParameter
            With objParaFaxID
                .ParameterName = "@FAXID"
                .Value = 0
                .Direction = ParameterDirection.InputOutput
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaFaxID)            

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()

            nFaxID = objParaFaxID.Value
            objParaFaxID = Nothing

            objCon.Close()
            objCon.Dispose()

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objCon = Nothing

            Return nFaxID
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)            
            Throw ex
        Catch ex As Exception            
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Function
    'sarika internet fax

    'sarika Dashboard Fill Faxes
    Public Function Scan_PendingFAXes(Optional ByVal nPatientID As Long = 0, Optional ByVal nMaxNoAttempts As Int16 = 0) As DataTable
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand            
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanAllPendingFAXes"
            objCmd.Connection = objCon

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaPatientID)
            objParaPatientID = Nothing 'Change made to solve memory Leak and word crash issue

            If nMaxNoAttempts > 0 Then
                Dim objParaMAXAttempts As New SqlParameter
                With objParaMAXAttempts
                    .ParameterName = "@MaxNoOfAttempts"
                    .Value = nMaxNoAttempts
                    .Direction = ParameterDirection.Input
                End With
                objCmd.Parameters.Add(objParaMAXAttempts)
                objParaMAXAttempts = Nothing 'Change made to solve memory Leak and word crash issue
            End If
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            'Change made to solve memory Leak and word crash issue
            objDA.Dispose()
            objDA = Nothing

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            Return dtTable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)            
            Return Nothing
        Catch ex As Exception            
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Start :: Pending Fax without Binary Data"
    Public Function Fill_RetrieveAllPendingFaxesWithoutBinary(Optional ByVal nPatientID As Long = 0, Optional ByVal nMaxNoAttempts As Int16 = 0) As DataTable
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand            
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveAllPendingFaxesWithoutBinary"
            objCmd.Connection = objCon

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaPatientID)
            objParaPatientID = Nothing 'Change made to solve memory Leak and word crash issue

            If nMaxNoAttempts > 0 Then
                Dim objParaMAXAttempts As New SqlParameter
                With objParaMAXAttempts
                    .ParameterName = "@MaxNoOfAttempts"
                    .Value = nMaxNoAttempts
                    .Direction = ParameterDirection.Input
                End With
                objCmd.Parameters.Add(objParaMAXAttempts)
                objParaMAXAttempts = Nothing 'Change made to solve memory Leak and word crash issue
            End If
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            objDA.Dispose()
            objDA = Nothing
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            Return dtTable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)            
            Throw ex
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Function
#End Region

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
