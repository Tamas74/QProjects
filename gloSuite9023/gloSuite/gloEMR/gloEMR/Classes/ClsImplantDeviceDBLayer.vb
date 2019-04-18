Imports System.Data
Imports System.Data.SqlClient

Public Class ClsImplantDeviceDBLayer
    Implements IDisposable

    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub
    Private Conn As SqlConnection
    Private Ds As System.Data.DataSet
    Private Dv As DataView

    Public Sub FetchData(ByVal sIssuingAgency As String, ByVal sSearchstring As String)
        Try
            Dim Adapter As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            Dim Cmd As SqlCommand = New SqlCommand("gsp_viewImplantDeviceSetup", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandTimeout = 0
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@issueingAgency", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sIssuingAgency

            objParam = Cmd.Parameters.Add("@Searchstring", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sSearchstring

            Adapter.SelectCommand = Cmd
            If (IsNothing(Ds) = False) Then
                Ds.Dispose()
                Ds = Nothing
            End If
            Ds = New DataSet
            Adapter.Fill(Ds)
            'Tb = Ds.Tables(0)
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            Dv = New DataView(Ds.Tables(0))
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Adapter.Dispose()
            Adapter = Nothing
            objParam = Nothing
        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Conn.Close()
        End Try

    End Sub
    Public Function AddData(ByVal lnDeviceMSTid As Long, ByVal sDeviceID As String, ByVal sIssuingAgency As String, ByVal sBrandName As String, ByVal sCompanyName As String, ByVal sVersion As String, ByVal sMRIStatus As String, ByVal bNRL As Boolean, ByVal sGMDNterms As String) As Long
        Dim objParam As SqlParameter
        Dim myObject As Long = 0
        Try

            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpImplantDeviceSetup", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandTimeout = 0

            objParam = Cmd.Parameters.Add("@DeviceMSTID", SqlDbType.BigInt, 255)
            objParam.Direction = ParameterDirection.InputOutput
            objParam.Value = 0

            objParam = Cmd.Parameters.Add("@sDeviceID", SqlDbType.VarChar, 30)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sDeviceID

            objParam = Cmd.Parameters.Add("@sIssuingAgency", SqlDbType.VarChar, 15)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sIssuingAgency

            objParam = Cmd.Parameters.Add("@sBrandName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sBrandName

            objParam = Cmd.Parameters.Add("@sCompanyName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sCompanyName

            objParam = Cmd.Parameters.Add("@sVersionModelNumber", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sVersion

            objParam = Cmd.Parameters.Add("@sMRISafetyStatus", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sMRIStatus

            objParam = Cmd.Parameters.Add("@bNRL", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = bNRL

            objParam = Cmd.Parameters.Add("@sGMDNTerms", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sGMDNterms

            Conn.Open()
            Cmd.ExecuteNonQuery()
            myObject = Cmd.Parameters("@DeviceMSTID").Value
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, " Implantable Device Master Added", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Implantable Device Master entry added", 0, myObject, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
            Conn.Close()
        Catch ex As SqlException
            If Conn.State <> ConnectionState.Closed Then
                Conn.Close()
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Catch ex As Exception
            If Conn.State <> ConnectionState.Closed Then
                Conn.Close()
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            objParam = Nothing
        End Try
        Return myObject
    End Function
    Public Sub UpdateData(ByVal lnDeviceMSTid As Long, ByVal sDeviceID As String, ByVal sIssuingAgency As String, ByVal sBrandName As String, ByVal sCompanyName As String, ByVal sVersion As String, ByVal sMRIStatus As String, ByVal bNRL As Boolean, ByVal sGMDNterms As String)
        Dim objParam As SqlParameter
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpImplantDeviceSetup", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandTimeout = 0

            objParam = Cmd.Parameters.Add("@DeviceMSTID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = lnDeviceMSTid

            objParam = Cmd.Parameters.Add("@sDeviceID", SqlDbType.VarChar, 30)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sDeviceID

            objParam = Cmd.Parameters.Add("@sIssuingAgency", SqlDbType.VarChar, 15)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sIssuingAgency

            objParam = Cmd.Parameters.Add("@sBrandName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sBrandName

            objParam = Cmd.Parameters.Add("@sCompanyName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sCompanyName

            objParam = Cmd.Parameters.Add("@sVersionModelNumber", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sVersion

            objParam = Cmd.Parameters.Add("@sMRISafetyStatus", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sMRIStatus

            objParam = Cmd.Parameters.Add("@bNRL", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = bNRL

            objParam = Cmd.Parameters.Add("@sGMDNTerms", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sGMDNterms

            Conn.Open()
            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            Conn.Close()            
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Implantable Device Master entry modified", 0, lnDeviceMSTid, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
        Catch ex As SqlException
            If Conn.State <> ConnectionState.Closed Then
                Conn.Close()
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)            
        Catch ex As Exception
            If Conn.State <> ConnectionState.Closed Then
                Conn.Close()
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            objParam = Nothing
        End Try
    End Sub
    Public Sub DeleteData(ByVal DeviceMSTid As Long)
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_DeleteImplantDeviceSetup", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandTimeout = 0
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nDeviceMSTID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = DeviceMSTid
            Conn.Open()
            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            Conn.Close()

            objParam = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "Implantable Device Master entry deleted", 0, DeviceMSTid, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
        Catch ex As SqlException
            Conn.Close()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "Implant device master Delete " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function Fill_LockImplantDevices(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        
        Dim objParam As SqlParameter
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            objParam = Cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachinName

            objParam = Cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TransactionType

            objParam = Cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            sqladpt.SelectCommand = Cmd

            sqladpt.Fill(dt)

            Conn.Close()
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Public Function FetchDataForUpdate(ByVal _DeviceMSTid As Long, ByVal _DeviceID As String) As ArrayList
        Try
            Dim arrlist As New ArrayList
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_ScanImplantableDeviceSetup", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandTimeout = 0
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nDeviceMSTId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _DeviceMSTid

            objParam = Cmd.Parameters.Add("@sDeviceID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _DeviceID

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader
            Do While dreader.Read()
                arrlist.Add(dreader.Item(0))    '[DEVICE_MSTID]
                arrlist.Add(dreader.Item(1))    '[DEVICE_ID]
                arrlist.Add(dreader.Item(2))    '[DEVICE_ID_ISSUING_AGENCY]
                arrlist.Add(dreader.Item(3))    '[BRAND_NAME]
                arrlist.Add(dreader.Item(4))    '[COMPANY_NAME]
                arrlist.Add(dreader.Item(5))    '[VERSION_MODEL_NUMBER]
                arrlist.Add(dreader.Item(6))    '[MRI_SAFETY_STATUS]
                arrlist.Add(dreader.Item(7))    '[LABELED_CONTAINS_NRL]
                arrlist.Add(dreader.Item(8))    '[GMDN_TERMS]               
            Loop
            dreader.Close()
            dreader = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Conn.Close()
            objParam = Nothing
            Return arrlist

        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function
    
    Public ReadOnly Property DsDataview() As DataView
        Get
            Return Dv
        End Get

    End Property
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        Dv.Sort = "[" & strsort & "]" & strSortOrder
    End Sub

    Public Function FillControls() As DataTable 'used
        Dim ds As DataSet = Nothing
        Try
            Dim adpt As New SqlDataAdapter


            Dim Cmd As SqlCommand = New SqlCommand("gsp_FillIssuingAgencies", Conn)
            Dim objParam As SqlParameter

            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandTimeout = 0
            adpt.SelectCommand = Cmd

            ds = New DataSet
            adpt.Fill(ds)
            adpt.Dispose()
            adpt = Nothing

            Conn.Close()
            objParam = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return ds.Tables(0).Copy()
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (IsNothing(ds) = False) Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function
    Public Function ValidateDescription(ByVal _DeviceMSTId As System.Int64, ByVal _DeviceID As String, ByVal _IssuingAgency As String, ByVal _BrandName As String, ByVal _CompanyName As String, ByVal _VersionModel As String) As Boolean
        Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_checkDuplicateImplantDeviceSetup", Conn)
        Try

            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nDeviceMSTId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _DeviceMSTId

            objParam = Cmd.Parameters.Add("@sDeviceID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _DeviceID

            objParam = Cmd.Parameters.Add("@sIssuingAgency", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _IssuingAgency

            objParam = Cmd.Parameters.Add("@sBrandName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _BrandName

            objParam = Cmd.Parameters.Add("@sCompanyName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _CompanyName

            objParam = Cmd.Parameters.Add("@sVersionModel", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _VersionModel

            Dim dreader As SqlDataReader
            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
            End If

            dreader = Cmd.ExecuteReader

            Dim i As Int64
            Do While dreader.Read
                i = CType(dreader.Item(0), System.Int64)
                If i > 0 Then
                    Conn.Close()
                    dreader.Close()
                    Return False
                Else
                    Conn.Close()
                    dreader.Close()
                    Return True
                End If
            Loop

            objParam = Nothing
            Return Nothing
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Cmd.Parameters.Clear()
            Cmd.Dispose()

        End Try

    End Function
    
    Public Function GetStandardTypes() As DataTable 'used
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            Cmd = New SqlCommand("gsp_FillIssuingAgencies", Conn)
            Dim objParam As SqlParameter

            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandTimeout = 0
            adpt.SelectCommand = Cmd


            adpt.Fill(ds)
            Conn.Close()
            objParam = Nothing
            Return ds.Tables(0).Copy()
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If IsNothing(adpt) = False Then
                adpt.Dispose()
                adpt = Nothing
            End If
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                If (IsNothing(Ds) = False) Then
                    Ds.Dispose()
                    Ds = Nothing
                End If

                If (IsNothing(Dv) = False) Then
                    Dv.Dispose()
                    Dv = Nothing
                End If
                If (IsNothing(Conn) = False) Then
                    Conn.Dispose()
                    Conn = Nothing
                End If

            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class



