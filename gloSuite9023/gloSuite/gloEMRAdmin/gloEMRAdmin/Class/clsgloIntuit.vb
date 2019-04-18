Imports System.Data.SqlClient
Public Class clsgloIntuit
    Implements IDisposable
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
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
    Public Function GetStaffMapping() As DataTable
        Dim dt As DataTable = Nothing
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetAndDeleteMapping"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@MappingID", SqlDbType.BigInt)
            oparam.Value = 0
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            Dim objDA As New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()


            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function DeleteStaffMapping(ByVal MappingID As Long) As Boolean
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetAndDeleteMapping"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@MappingID", SqlDbType.BigInt)
            oparam.Value = MappingID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objCmd.ExecuteNonQuery()
            objCon.Close()
            Return True

        Catch ex As Exception
            Throw ex
            Return False
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function GetMappedUsers(ByVal MappingID As Long) As DataTable
        Dim dt As DataTable = Nothing
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetMappedUsers"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@MappingID", SqlDbType.BigInt)
            oparam.Value = MappingID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            Dim objDA As New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()


            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function SaveStaffMapping(ByVal MappingID As Long, ByVal staffID As Long, ByVal Description As String, ByVal dtUsers As DataTable) As Boolean
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand
            Dim oparam As SqlParameter
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUp_IntuitStaffMapping"


            oparam = New SqlParameter("@nIntuitStaffMappingID", SqlDbType.BigInt)
            oparam.Value = IIf(MappingID > 0, MappingID, 0)
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@nStaffID", SqlDbType.BigInt)
            oparam.Value = staffID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@sDescription", SqlDbType.VarChar)
            oparam.Value = Description
            objCmd.Parameters.Add(oparam)
            oparam = Nothing


            oparam = New SqlParameter("@TVP_Detail", SqlDbType.Structured)
            oparam.Value = dtUsers
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            Return True
        Catch ex As SqlException
            Throw ex
            Return False
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function checkStaffIDAssociation(ByVal MappingID As Long) As Integer

        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Dim _ocnt As Integer
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand
            Dim oparam As SqlParameter
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_checkStaffIDAssociation"


            oparam = New SqlParameter("@nStaffID", SqlDbType.BigInt)
            oparam.Value = MappingID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            _ocnt = objCmd.ExecuteScalar()
            objCon.Close()
            Return _ocnt
        Catch ex As SqlException
            Throw ex
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

#Region "Provider Mapping"
    Public Function GetProviderMapping() As DataTable
        Dim dt As DataTable = Nothing
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetAndDeleteProviderMapping"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@MappingID", SqlDbType.BigInt)
            oparam.Value = 0
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            Dim objDA As New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()


            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function DeleteProviderMapping(ByVal MappingID As Long) As Boolean
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetAndDeleteProviderMapping"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@MappingID", SqlDbType.BigInt)
            oparam.Value = MappingID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objCmd.ExecuteNonQuery()
            objCon.Close()
            Return True

        Catch ex As Exception
            Throw ex
            Return False
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function SaveProviderMapping(ByVal MappingID As Long, ByVal ProviderID As Long, ByVal IntuitProviderId As Integer) As Boolean
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand
            Dim oparam As SqlParameter
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUp_IntuitProviderMapping"


            oparam = New SqlParameter("@nMappingID", SqlDbType.BigInt)
            oparam.Value = IIf(MappingID > 0, MappingID, 0)
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@nProviderID", SqlDbType.BigInt)
            oparam.Value = ProviderID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@nIntuitProviderID", SqlDbType.Int)
            oparam.Value = IntuitProviderId
            objCmd.Parameters.Add(oparam)
            oparam = Nothing


            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            Return True
        Catch ex As SqlException
            Throw ex
            Return False
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function checkProviderMappingExists(ByVal ProviderId As Long, ByVal IntuitProviderId As Integer, ByVal bMode As Boolean) As String

        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Dim _oresult As String = String.Empty
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand
            Dim oparam As SqlParameter
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_CheckProviderMapping_Exists"


            oparam = New SqlParameter("@nProviderID", SqlDbType.BigInt)
            oparam.Value = ProviderId
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@nIntuitProviderID", SqlDbType.Int)
            oparam.Value = IntuitProviderId
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@IsModifyMode", SqlDbType.Bit)
            oparam.Value = bMode
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@Result", SqlDbType.VarChar)
            oparam.Value = _oresult
            oparam.Direction = ParameterDirection.Output
            objCmd.Parameters.Add(oparam)
            oparam = Nothing



            objCmd.Connection = objCon
            objCon.Open()
            _oresult = objCmd.ExecuteScalar()
            objCon.Close()
            Return _oresult
        Catch ex As SqlException
            Throw ex
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
#End Region

#Region "Location Mapping"
    Public Function GetLocationMapping() As DataTable
        Dim dt As DataTable = Nothing
        Dim objCon As SqlConnection = Nothing
        Dim objDA As New SqlDataAdapter
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetAndDeleteLocationMapping"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@MappingID", SqlDbType.BigInt)
            oparam.Value = 0
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objDA = New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()


            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function DeleteLocationMapping(ByVal MappingID As Long) As Boolean
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetAndDeleteLocationMapping"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@MappingID", SqlDbType.BigInt)
            oparam.Value = MappingID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objCmd.ExecuteNonQuery()
            objCon.Close()
            Return True

        Catch ex As Exception
            Throw ex
            Return False
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function SaveLocationMapping(ByVal MappingID As Long, ByVal LocationID As Long, ByVal IntuitLocationID As Integer) As Boolean
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand
            Dim oparam As SqlParameter
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUp_IntuitLocationMapping"


            oparam = New SqlParameter("@nMappingID", SqlDbType.BigInt)
            oparam.Value = IIf(MappingID > 0, MappingID, 0)
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@nLocationID", SqlDbType.BigInt)
            oparam.Value = LocationID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@nIntuitLocationID", SqlDbType.Int)
            oparam.Value = IntuitLocationID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing


            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            Return True
        Catch ex As SqlException
            Throw ex
            Return False
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function checkLocationMappingExists(ByVal LocationId As Long, ByVal IntuitLocationId As Integer, ByVal bMode As Boolean) As String

        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Dim _oresult As String = String.Empty
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand
            Dim oparam As SqlParameter
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_CheckLocationMapping_Exists"


            oparam = New SqlParameter("@nLocationID", SqlDbType.BigInt)
            oparam.Value = LocationId
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@nIntuitLocationID", SqlDbType.Int)
            oparam.Value = IntuitLocationId
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@IsModifyMode", SqlDbType.Bit)
            oparam.Value = bMode
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@Result", SqlDbType.VarChar)
            oparam.Value = _oresult
            oparam.Direction = ParameterDirection.Output
            objCmd.Parameters.Add(oparam)
            oparam = Nothing



            objCmd.Connection = objCon
            objCon.Open()
            _oresult = objCmd.ExecuteScalar()
            objCon.Close()
            Return _oresult
        Catch ex As SqlException
            Throw ex
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
#End Region
    Public Function IsLocation_ProviderInMessageMapping(ByVal nID As Long, ByVal sType As String) As String

        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Dim _oresult As String = String.Empty
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand
            Dim oparam As SqlParameter
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_IsProvider_Location_Associated"


            oparam = New SqlParameter("@nID", SqlDbType.BigInt)
            oparam.Value = nID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@sType", SqlDbType.VarChar)
            oparam.Value = sType
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@Result", SqlDbType.VarChar)
            oparam.Value = _oresult
            oparam.Direction = ParameterDirection.Output
            objCmd.Parameters.Add(oparam)
            oparam = Nothing



            objCmd.Connection = objCon
            objCon.Open()
            _oresult = objCmd.ExecuteScalar()
            objCon.Close()
            Return _oresult
        Catch ex As SqlException
            Throw ex
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function GetIntuitMappedProviderandLocation(ByVal sCategory As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim objCon As SqlConnection = Nothing
        Dim objDA As SqlDataAdapter = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetIntuitMappedProviderandLocation"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@sCategory", SqlDbType.VarChar)
            oparam.Value = sCategory
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objDA = New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()


            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function GetUsers() As DataTable
        Dim dt As DataTable = Nothing
        Dim objCon As SqlConnection = Nothing
        Dim objDA As SqlDataAdapter
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillUsers"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@flag", SqlDbType.Int)
            oparam.Value = vbNull
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objDA = New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()


            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function GetMapping(ByVal sMessageType As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim objCon As SqlConnection = Nothing
        Dim objDA As SqlDataAdapter = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetMessageMapping"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@sMessageType", SqlDbType.VarChar)
            oparam.Value = sMessageType
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objDA = New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()


            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Function SaveMapping(ByVal dtMapping As DataTable, ByVal sMessageType As String) As Boolean

        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpDel_MessageMapping"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@TVP_IntuitMessageMapping", SqlDbType.Structured)
            oparam.Value = dtMapping
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@sMessageType", SqlDbType.VarChar)
            oparam.Value = sMessageType
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objCmd.ExecuteNonQuery()
            objCon.Close()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function GetDetaultMappedUsers(ByVal sMessageType As String, ByVal sSettingType As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetDefaultMappedUsers"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@sMessageType", SqlDbType.VarChar)
            oparam.Value = sMessageType
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@sSettingType", SqlDbType.VarChar)
            oparam.Value = sSettingType
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            Dim objDA As New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()


            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function IsPatientPortalEnabled() As Boolean
        Dim IsPortalEnabled As Boolean = False
        Try
            Dim objSettings As New clsSettings
            Dim isPortalEnable As Object = Nothing
            objSettings.GetSetting("PatientPortalEnabled", gnLoginID, gnClinicID, isPortalEnable)
            If isPortalEnable IsNot Nothing Then
                If Convert.ToString(isPortalEnable).ToLower() = "true" Then
                    IsPortalEnabled = True
                End If
            End If
            objSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
            Return IsPortalEnabled
        End Try
        Return IsPortalEnabled
    End Function
End Class
