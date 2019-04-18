Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms

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
    'Added by madan on 20100514
    Public Sub New()
        gstrMessageBoxCaption = GetMessageBoxCaption()
    End Sub

    Public Function AddPendingFAX(ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXTYpe As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal sFileName As String, ByVal dtFAXDate As DateTime, Optional ByVal CurrentFAXPriority As enmFAXPriority = enmFAXPriority.NormalPriority) As Boolean
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()

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
            objParaPatientID = Nothing


            Dim objParaFAXTo As New SqlParameter
            With objParaFAXTo
                .ParameterName = "@FAXTo"
                .Value = sFAXTo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXTo)
            objParaFAXTo = Nothing


            Dim objParaFAXTYpe As New SqlParameter
            With objParaFAXTYpe
                .ParameterName = "@FAXType"
                .Value = sFAXTYpe
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXTYpe)
            objParaFAXTYpe = Nothing



            Dim objParaFAXNo As New SqlParameter
            With objParaFAXNo
                .ParameterName = "@FAXNo"
                .Value = sFAXNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXNo)
            objParaFAXNo = Nothing


            Dim objParaLoginUser As New SqlParameter
            With objParaLoginUser
                .ParameterName = "@LoginUser"
                .Value = sLoginUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLoginUser)
            objParaLoginUser = Nothing



            Dim objParaFileName As New SqlParameter
            With objParaFileName
                .ParameterName = "@FileName"
                .Value = sFileName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFileName)
            objParaFileName = Nothing


            Dim objParaFaxDate As New SqlParameter
            With objParaFaxDate
                .ParameterName = "@FAXDate"
                .Value = dtFAXDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFaxDate)
            objParaFaxDate = Nothing


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
            objParaFaxPriority = Nothing



            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            'objCmd = Nothing
            objCon.Dispose()
            objCon = Nothing
            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '   UpdateLog("clsFAX -- AddPendingFAX -- " & ex.ToString)
            Throw ex
        Catch ex As Exception
            ' UpdateLog("clsFAX -- AddPendingFAX -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
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
                dt.Dispose()
                dt = Nothing
            Else
                objmytable = New mytable("", "")
            End If
            Return objmytable
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Throw ex
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Conn) Then
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
                Conn.Dispose()
                Conn = Nothing
            End If
            If (IsNothing(sqladpt) = False) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function
    Public Function GetPharmacyFAXNo(ByVal nPatientID As Long) As mytable
        Dim objParam As SqlParameter = Nothing
        Dim Conn As SqlConnection = Nothing
        'Dim myValue As Object
        Dim dreader As SqlDataReader
        Dim objmytable As mytable = Nothing
        Dim sqlconn As String
        Dim cmdsql As SqlCommand = Nothing
        Try

            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
            cmdsql = New SqlCommand("gsp_GetFaxNo", Conn)
            cmdsql.CommandType = CommandType.StoredProcedure
            'objParam = cmdsql.Parameters.Add("@nPatientId", gnPatientID)
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
            Conn.Close()
            Return objmytable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ' UpdateLog("clsFAX -- GetPharmacyFAXNo -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            '   UpdateLog("clsFAX -- GetPharmacyFAXNo -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If

            If cmdsql IsNot Nothing Then
                cmdsql.Parameters.Clear()
                cmdsql.Dispose()
                cmdsql = Nothing
            End If

            If IsNothing(Conn) = False Then
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
                Conn.Dispose()
                Conn = Nothing
            End If
        End Try
    End Function

    Public Function GetContactFAXNo(ByVal nContactID As Long) As String
        Dim strFAXNo As String = ""
        Dim objParam As SqlParameter = Nothing
        Dim Conn As SqlConnection = Nothing

        Dim cmdsql As SqlCommand = Nothing
        Try
            Dim sqlconn As String
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
            cmdsql = New SqlCommand("gsp_GetContactFaxNo", Conn)
            cmdsql.CommandType = CommandType.StoredProcedure
            objParam = cmdsql.Parameters.Add("@nContactID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nContactID
            Conn.Open()
          
                strFAXNo = Convert.ToString(cmdsql.ExecuteScalar)

            Conn.Close()
            Return strFAXNo
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '  UpdateLog("clsFAX -- GetContactFAXNo -- " & ex.ToString)
            Return strFAXNo
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            '  UpdateLog("clsFAX -- GetContactFAXNo -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return strFAXNo
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If

            If cmdsql IsNot Nothing Then
                cmdsql.Parameters.Clear()
                cmdsql.Dispose()
                cmdsql = Nothing
            End If
            If IsNothing(Conn) = False Then
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
                Conn.Dispose()
                Conn = Nothing
            End If
        End Try
    End Function
    Public Sub RetrieveFAXDetails(ByVal strFileName As String)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim objParaFileName As New SqlParameter
        Try
            objCon.ConnectionString = GetConnectionString()

            Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrievePendingDetails"
            objCmd.Connection = objCon

            With objParaFileName
                .ParameterName = "@FileName"
                .Value = strFileName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFileName)
            objParaFileName = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            If objSQLDataReader.HasRows = True Then
                objSQLDataReader.Read()
                If IsDBNull(objSQLDataReader.Item("FAXID")) = False Then
                    _nFAXID = objSQLDataReader.Item("FAXID")
                Else
                    _nFAXID = 0
                End If
                If IsDBNull(objSQLDataReader.Item("PatientID")) = False Then
                    _nPatientID = objSQLDataReader.Item("PatientID")
                Else
                    _nPatientID = 0
                End If
                If IsDBNull(objSQLDataReader.Item("PatientName")) = False Then
                    _sPatientName = objSQLDataReader.Item("PatientName")
                Else
                    _sPatientName = ""
                End If
                If IsDBNull(objSQLDataReader.Item("FAXTo")) = False Then
                    _sFAXTo = objSQLDataReader.Item("FAXTo")
                Else
                    _sFAXTo = ""
                End If
                If IsDBNull(objSQLDataReader.Item("FAXType")) = False Then
                    _sFAXType = objSQLDataReader.Item("FAXType")
                Else
                    _sFAXType = ""
                End If
                If IsDBNull(objSQLDataReader.Item("FAXNo")) = False Then
                    _sFAXNo = objSQLDataReader.Item("FAXNo")
                Else
                    _sFAXNo = ""
                End If
                If IsDBNull(objSQLDataReader.Item("LoginUser")) = False Then
                    _sLoginUser = objSQLDataReader.Item("LoginUser")
                Else
                    _sLoginUser = ""
                End If
                If IsDBNull(objSQLDataReader.Item("FAXDate")) = False Then
                    _dtFAXDate = objSQLDataReader.Item("FAXDate")
                End If
            End If
            objSQLDataReader.Close()
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            objSQLDataReader = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' UpdateLog("clsFAX -- RetrieveFAXDetails -- " & ex.ToString)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ' UpdateLog("clsFAX -- RetrieveFAXDetails -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If Not IsNothing(objParaFileName) Then
                objParaFileName = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Sub
    Public Function RetrieveFAXAttemptsDetails(ByVal nFAXID As Int64) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()
            'Dim objSQLDataReader As SqlDataReader = Nothing
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
            objParaFAXID = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            objDA.Dispose()
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            Return dtTable
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '     UpdateLog("clsFAX -- RetrieveFAXAttemptsDetails -- " & ex.ToString)
            Throw ex
            ' Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ' UpdateLog("clsFAX -- RetrieveFAXAttemptsDetails -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            ' Return Nothing
        Finally

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function
    Public Function ReInitialisePendingFAX(ByVal nFAXID As Int64, Optional ByVal strFAXNo As String = "") As Boolean
        Dim objCmd As New SqlCommand
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            'Dim objSQLDataReader As SqlDataReader
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
            objParaFAXID = Nothing

            Dim objParaFAXNo As New SqlParameter
            With objParaFAXNo
                .ParameterName = "@FAXNo"
                .Value = strFAXNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXNo)
            objParaFAXNo = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '   UpdateLog("clsFAX -- ReInitialisePendingFAX -- " & ex.ToString)
            Throw ex
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            '   UpdateLog("clsFAX -- ReInitialisePendingFAX -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return False
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function
    Public Function DeletePendingFAX(ByVal strFileName As String) As Boolean
        Dim objCmd As New SqlCommand
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeletePendingFAX"
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
            objParaFileName = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            'objCmd = Nothing
            objCon = Nothing
            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '  UpdateLog("clsFAX -- DeletePendingFAX -- " & ex.ToString)
            Throw ex
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            '   UpdateLog("clsFAX -- DeletePendingFAX -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return False
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try

    End Function
    Public Function AddTempFAX(ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXTYpe As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal sFileName As String, ByVal dtFAXDate As DateTime) As Boolean
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand

        Try
            objCon.ConnectionString = GetConnectionString()
            ' Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InsertTempFAX"

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaPatientID)
            objParaPatientID = Nothing


            Dim objParaFAXTo As New SqlParameter
            With objParaFAXTo
                .ParameterName = "@FAXTo"
                .Value = sFAXTo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXTo)
            objParaFAXTo = Nothing


            Dim objParaFAXTYpe As New SqlParameter
            With objParaFAXTYpe
                .ParameterName = "@FAXType"
                .Value = sFAXTYpe
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXTYpe)
            objParaFAXTYpe = Nothing


            Dim objParaFAXNo As New SqlParameter
            With objParaFAXNo
                .ParameterName = "@FAXNo"
                .Value = sFAXNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXNo)
            objParaFAXNo = Nothing


            Dim objParaLoginUser As New SqlParameter
            With objParaLoginUser
                .ParameterName = "@LoginUser"
                .Value = sLoginUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLoginUser)
            objParaLoginUser = Nothing


            Dim objParaFileName As New SqlParameter
            With objParaFileName
                .ParameterName = "@FileName"
                .Value = sFileName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFileName)
            objParaFileName = Nothing


            Dim objParaFaxDate As New SqlParameter
            With objParaFaxDate
                .ParameterName = "@FAXDate"
                .Value = dtFAXDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFaxDate)
            objParaFaxDate = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            'objCmd = Nothing
            objCon = Nothing
            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' UpdateLog("clsFAX -- AddTempFAX -- " & ex.ToString)
            Throw ex
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            '  UpdateLog("clsFAX -- AddTempFAX -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return False
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function
    Public Function DeleteTempFAX(ByVal strFileName As String) As Boolean
        Dim objCmd As New SqlCommand
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()

            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeleteTempFAX"
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
            objParaFileName = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            'objCmd = Nothing
            objCon = Nothing
            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '  UpdateLog("clsFAX -- DeleteTempFAX -- " & ex.ToString)
            Throw ex
            Return False
        Catch ex As Exception
            '            UpdateLog("clsFAX -- DeleteTempFAX -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return False
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function
    Public Function Fill_TempFAXes(Optional ByVal strFileName As String = "") As DataTable
        Dim objCmd As New SqlCommand
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveTempFAX"
            objCmd.Connection = objCon

            Dim objParaFileName As New SqlParameter
            With objParaFileName
                .ParameterName = "@FileName"
                .Value = strFileName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFileName)
            objParaFileName = Nothing


            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            Return dtTable
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '  UpdateLog("clsFAX -- Fill_TempFAXes -- " & ex.ToString)
            Throw ex
            '  Return Nothing
        Catch ex As Exception
            ' UpdateLog("clsFAX -- Fill_TempFAXes -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            '   Return Nothing
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function


    'sarika 6th sept 07
    Public Function GetRecieveFaxUserID() As Int64
        'get the recieve fax user id set by admin from the Settings table
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        Dim nRecieveFaxUserID As Int64 = 0

        Try
            objCon.Open()

            objCmd.Connection = objCon
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "select isnull(sSettingsValue,'') from Settings where sSettingsName = 'Recieve Fax User'"

            nRecieveFaxUserID = objCmd.ExecuteScalar()

            Return nRecieveFaxUserID
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' UpdateLog("clsFAX -- GetRecieveFaxUserID -- " & ex.ToString)
            Throw ex
        Catch ex As Exception
            ' UpdateLog("clsFAX -- GetRecieveFaxUserID -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
        End Try
    End Function
    '------------------


    'sarika 12th nov 07

    Public Function DeletePendingFAX(ByVal FaxID As Long) As Boolean
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand

        Try
            objCon.ConnectionString = GetConnectionString()
            'get the fax filename from the faxid
            Dim sFileName As String = ""

            objCon.Open()
            With objCmd
                .Connection = objCon
                .CommandType = CommandType.Text
                .CommandText = " select isnull(sFileName,'') as sFileName from faxpending_mst where nFAXID = " & FaxID
            End With

            sFileName = objCmd.ExecuteScalar

            'sarika Delete Sent Fax 20090428 
            '    DeletePendingFAX(sFileName)
            DeletePendingFAX_ID(FaxID)

            'delete the tiff file also
            'get the filename from the fax output directory and delete the file
            sFileName = gstrFAXOutputDirectory & "\" & sFileName & ".tif"

            If (sFileName <> "") Then
                DeleteTiff(sFileName)
            End If


            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show("Error deleting the Pending fax. " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            objCon.Dispose()
            objCon = Nothing
        End Try

    End Function

    Public Function DeletePendingNormalFAX(ByVal strFileName As String) As Boolean
        Dim objCmd As New SqlCommand
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
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
            objParaFileName = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            'objCmd = Nothing
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
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try

    End Function
    'sarika Delete Sent Fax 20090428 
    Public Function DeletePendingFAX_ID(ByVal nFaxID As Long) As Boolean
        Dim objCmd As New SqlCommand
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()

            'Dim objSQLDataReader As SqlDataReader
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
            objParaFileName = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            'objCmd = Nothing
            objCon = Nothing
            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '  UpdateLog("clsFAX -- DeletePendingFAX -- " & ex.ToString)
            Throw ex
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            '   UpdateLog("clsFAX -- DeletePendingFAX -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return False
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try

    End Function

    'sarika Delete Sent Fax 20090428
    Public Function DeleteSentFAX(ByVal nFaxID As Long) As Boolean
        Dim _strSQL As String = ""
        Dim objCmd As New SqlCommand

        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()

            'Dim objSQLDataReader As SqlDataReader
            _strSQL = "delete from fax where nfaxid = " & nFaxID

            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = _strSQL
            objCmd.Connection = objCon

            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCon.Dispose()
            'objCmd = Nothing
            objCon = Nothing

            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsFAX -- DeletePendingFAX -- " & ex.ToString)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            UpdateLog("clsFAX -- DeletePendingFAX -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try

    End Function

    '-----------

    '----


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
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show("Error deleting the Pending fax. " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            objCon.Dispose()
            objCon = Nothing
        End Try
    End Function


    Private Function DeleteTiff(ByVal FileName As String) As Boolean
        Try
            If File.Exists(FileName) Then
                File.Delete(FileName)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show("Error deleting the Pending fax tiff file. " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
        Return Nothing
    End Function
    '----


    'sarika internet fax
    Public Function AddPendingFAX1(ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXTYpe As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal sFileName As String, ByVal dtFAXDate As DateTime, ByVal BinaryFile As String, ByVal EFax_DocumentExtension As String, Optional ByVal CurrentFAXPriority As enmFAXPriority = enmFAXPriority.NormalPriority, Optional ByVal EFax_CoverPageDocumentExtension As String = "docx", Optional ByVal nNoOfAttempts As Int32 = 0, Optional ByVal sCurrentStatus As String = "Pending", Optional ByVal TransactionID As String = "", Optional ByVal Status As String = "", Optional ByVal TransResultCode As String = "", Optional ByVal FaxCoverPageBinaryData As Byte() = Nothing, Optional ByVal EFax_Resolution As String = "", Optional ByVal EFax_DocumentEncodingType As String = "base64", Optional ByVal EFax_DocumentContentType As String = "", Optional ByVal EFax_BillingCode As String = "", Optional ByVal EFax_Tiff_image_flag As String = "false") As Int64

        Dim objCon As New SqlConnection
        Dim nFaxID As Int64 = 0
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()

            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "Fax_InUpPendingEFAX"

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
            End With
            objCmd.Parameters.Add(objParaPatientID)
            objParaPatientID = Nothing

            Dim objParaFAXTo As New SqlParameter
            With objParaFAXTo
                .ParameterName = "@FAXTo"
                .Value = sFAXTo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXTo)
            objParaFAXTo = Nothing


            Dim objParaFAXTYpe As New SqlParameter
            With objParaFAXTYpe
                .ParameterName = "@FAXType"
                .Value = sFAXTYpe
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXTYpe)
            objParaFAXTYpe = Nothing


            Dim objParaFAXNo As New SqlParameter
            With objParaFAXNo
                .ParameterName = "@FAXNo"
                .Value = sFAXNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAXNo)
            objParaFAXNo = Nothing


            Dim objParaLoginUser As New SqlParameter
            With objParaLoginUser
                .ParameterName = "@LoginUser"
                .Value = sLoginUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLoginUser)
            objParaLoginUser = Nothing


            Dim objParaFileName As New SqlParameter
            With objParaFileName
                .ParameterName = "@FileName"
                .Value = ""
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFileName)
            objParaFileName = Nothing


            Dim objParaFaxDate As New SqlParameter
            With objParaFaxDate
                .ParameterName = "@FAXDate"
                .Value = dtFAXDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFaxDate)
            objParaFaxDate = Nothing

            Dim objParaFaxFileBinaryData As New SqlParameter
            With objParaFaxFileBinaryData
                .ParameterName = "@FaxFileBinaryData"
                .Value = BinaryFile
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFaxFileBinaryData)
            objParaFaxFileBinaryData = Nothing


            Dim objParaEFax_DocumentExtension As New SqlParameter
            With objParaEFax_DocumentExtension
                .ParameterName = "@EFax_DocumentExtension"
                .Value = EFax_DocumentExtension
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_DocumentExtension)
            objParaEFax_DocumentExtension = Nothing


            Dim objParaEFax_CoverPageDocumentExtension As New SqlParameter
            With objParaEFax_CoverPageDocumentExtension
                .ParameterName = "@EFax_CoverPageDocumentExtension"
                .Value = EFax_CoverPageDocumentExtension
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_CoverPageDocumentExtension)
            objParaEFax_CoverPageDocumentExtension = Nothing


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
            objParaFaxPriority = Nothing


            Dim objParaNoOfAttempts As New SqlParameter
            With objParaNoOfAttempts
                .ParameterName = "@nNoOfAttempts"
                .Value = nNoOfAttempts
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaNoOfAttempts)
            objParaNoOfAttempts = Nothing


            Dim objParaCurrentStatus As New SqlParameter
            With objParaCurrentStatus
                .ParameterName = "@sCurrentStatus"
                .Value = sCurrentStatus
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCurrentStatus)
            objParaCurrentStatus = Nothing


            Dim objParaTransactionID As New SqlParameter
            With objParaTransactionID
                .ParameterName = "@TransactionID"
                .Value = TransactionID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaTransactionID)
            objParaTransactionID = Nothing



            Dim objParaStatus As New SqlParameter
            With objParaStatus
                .ParameterName = "@Status"
                .Value = Status
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaStatus)
            objParaStatus = Nothing


            Dim objParaTransResultCode As New SqlParameter
            With objParaTransResultCode
                .ParameterName = "@TransResultCode"
                .Value = TransResultCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaTransResultCode)
            objParaTransResultCode = Nothing


            Dim objParaFaxCoverPageBinaryData As New SqlParameter
            With objParaFaxCoverPageBinaryData
                .ParameterName = "@FaxCoverPageBinaryData"
                .Value = FaxCoverPageBinaryData
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Image
            End With
            objCmd.Parameters.Add(objParaFaxCoverPageBinaryData)
            objParaFaxCoverPageBinaryData = Nothing



            Dim objParaEFax_Resolution As New SqlParameter
            With objParaEFax_Resolution
                .ParameterName = "@EFax_Resolution"
                .Value = EFax_Resolution
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_Resolution)
            objParaEFax_Resolution = Nothing


            Dim objParaEFax_DocumentEncodingType As New SqlParameter
            With objParaEFax_DocumentEncodingType
                .ParameterName = "@EFax_DocumentEncodingType"
                .Value = EFax_DocumentEncodingType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_DocumentEncodingType)
            objParaEFax_DocumentEncodingType = Nothing


            Dim objParaEFax_DocumentContentType As New SqlParameter
            With objParaEFax_DocumentContentType
                .ParameterName = "@EFax_DocumentContentType"
                .Value = EFax_DocumentContentType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_DocumentContentType)
            objParaEFax_DocumentContentType = Nothing


            Dim objParaEFax_BillingCode As New SqlParameter
            With objParaEFax_BillingCode
                .ParameterName = "@EFax_BillingCode"
                .Value = EFax_BillingCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_BillingCode)
            objParaEFax_BillingCode = Nothing


            Dim objParaEFax_Tiff_image_flag As New SqlParameter
            With objParaEFax_Tiff_image_flag
                .ParameterName = "@EFax_Tiff_image_flag"
                .Value = EFax_Tiff_image_flag
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEFax_Tiff_image_flag)
            objParaEFax_Tiff_image_flag = Nothing


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
            'objCmd = Nothing
            objCon = Nothing

            Return nFaxID
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '  UpdateLog("clsFAX -- AddPendingFAX -- " & ex.ToString)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            '  UpdateLog("clsFAX -- AddPendingFAX -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function
    'sarika internet fax
    'sarika Dashboard Fill Faxes

    Public Function Scan_PendingFAXes(Optional ByVal nPatientID As Long = 0, Optional ByVal nMaxNoAttempts As Int16 = 0) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try

            objCon.ConnectionString = GetConnectionString()
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanAllPendingFAXes"
            objCmd.Connection = objCon

            'FAXID,PatientID,PatientName,FAXTo, FAXType,FAXNo, LoginUser,FAXDate, FAXFileName,NoOfAttempts,CurrentStatus,       
            'EFax_DocumentExtension,StatusDescription, StatusCode,TransStatusCode,TransStatusDescription

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
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
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            objDA.Dispose()
            objDA = Nothing
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            Return dtTable
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsFAX -- Fill_PendingFAXes(2) -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            UpdateLog("clsFAX -- Fill_PendingFAXes(2) -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
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
