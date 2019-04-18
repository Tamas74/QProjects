Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class clsProviderType
#Region "   Private Variable"
    Dim m_ProviderTypeID As Integer
    Dim m_ProviderType As String
    Dim m_ErrorMessage As String
    Dim m_ConnectionString As String = ""
#End Region
#Region "   Public Prioperties"
    Public Property ProviderTypeID() As Integer
        Get
            Return m_ProviderTypeID
        End Get
        Set(ByVal Value As Integer)
            m_ProviderTypeID = Value
        End Set
    End Property
    Public Property ProviderType() As String
        Get
            Return m_ProviderType
        End Get
        Set(ByVal Value As String)
            m_ProviderType = Value
        End Set
    End Property
    Public ReadOnly Property ErrorMessage() As String
        Get
            Return m_ErrorMessage
        End Get
    End Property
    Public ReadOnly Property ConnectionString() As String
        Get
            Return m_ConnectionString
        End Get
    End Property
#End Region
#Region "   Constructor"

    Public Sub New()

    End Sub
    Public Sub New(ByVal ConnectionString As String)
        m_ConnectionString = ConnectionString
    End Sub
#End Region
#Region "   Public Methods and Functions"
    Public Function Fill_ProviderTypes() As Collection
        Dim clProviderTypes As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillProviderTypes_MST"
            objCmd.Connection = objCon

            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            If objSQLDataReader.HasRows = True Then
                While objSQLDataReader.Read
                    clProviderTypes.Add(objSQLDataReader.Item("ProviderType"))
                End While
            End If

            Return clProviderTypes

        Catch ex As Exception
        Finally
            objCon.Close()
            objCon = Nothing
            objCmd = Nothing
            objSQLDataReader = Nothing
        End Try

        Return clProviderTypes
    End Function
    Public Function ScanProviderType(ByVal strProviderType As String) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = m_ConnectionString
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanProviderTypes_MST"
        objCmd.Connection = objCon
        Dim objParaProviderType As New SqlParameter
        With objParaProviderType
            .ParameterName = "@ProviderType"
            .Value = strProviderType
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProviderType)
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function


    Public Sub SearchProviderType(ByVal strProviderType As String)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = m_ConnectionString
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanProviderTypes_MST"
        objCmd.Connection = objCon
        Dim objParaProviderType As New SqlParameter
        With objParaProviderType
            .ParameterName = "@ProviderType"
            .Value = strProviderType
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProviderType)
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()
            m_ProviderTypeID = objSQLDataReader.Item("ProviderTypeID")
            m_ProviderType = objSQLDataReader.Item("ProviderType")
        Else
        End If
        objSQLDataReader.Close()
        objCon.Close()
        objCon = Nothing
    End Sub
    Public Sub SearchProviderType(ByVal nProviderTypeID As Integer)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = m_ConnectionString
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanProviderTypesByID"
        objCmd.Connection = objCon
        Dim objParaProviderTypeID As New SqlParameter
        With objParaProviderTypeID
            .ParameterName = "@ProviderTypeID"
            .Value = nProviderTypeID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaProviderTypeID)
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()
            m_ProviderTypeID = objSQLDataReader.Item("ProviderTypeID")
            m_ProviderType = objSQLDataReader.Item("ProviderType")
        Else
        End If
        objSQLDataReader.Close()
        objCon.Close()
        objCon = Nothing
    End Sub

    Public Function InsertProviderType() As Boolean
        Return InsertProviderType(m_ProviderType)
    End Function
    Public Function InsertProviderType(ByVal ProviderType As String) As Boolean
        Try
            m_ErrorMessage = ""
            Dim objCon As New SqlConnection
            objCon.ConnectionString = m_ConnectionString
            Dim objCmd As New SqlCommand

            'Block the respective user
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpProviderType_MST"
            objCmd.Connection = objCon


            Dim objParaProviderType As New SqlParameter
            With objParaProviderType
                .ParameterName = "@ProviderType"
                .Value = ProviderType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProviderType)

            Dim objParaClinicID As New SqlParameter
            With objParaClinicID
                .ParameterName = "@ClinicID"
                .Value = 1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicID)

            objCon.Open()

            objCmd.Connection = objCon
            objCmd.ExecuteNonQuery()
            objCon.Close()
            Return True
        Catch ex As Exception
            m_ErrorMessage = ex.Message
            Return False
        End Try

    End Function
    Public Function UpdateProviderType() As Boolean
        Return UpdateProviderType(m_ProviderTypeID, m_ProviderType)
    End Function
    Public Function UpdateProviderType(ByVal ProviderTypeID As Integer, ByVal ProviderType As String) As Boolean
        Try
            m_ErrorMessage = ""
            Dim objCon As New SqlConnection
            objCon.ConnectionString = m_ConnectionString
            Dim objCmd As New SqlCommand

            'Block the respective user
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpProviderType_MST"
            objCmd.Connection = objCon

            Dim objParaProviderTypeID As New SqlParameter
            With objParaProviderTypeID
                .ParameterName = "@ProviderTypeID"
                .Value = ProviderTypeID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaProviderTypeID)


            Dim objParaProviderType As New SqlParameter
            With objParaProviderType
                .ParameterName = "@ProviderType"
                .Value = ProviderType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProviderType)

            objCon.Open()

            objCmd.Connection = objCon
            objCmd.ExecuteNonQuery()
            objCon.Close()
            Return True
        Catch ex As Exception
            m_ErrorMessage = ex.Message
            Return False
        End Try


    End Function
    Public Function CheckProviderTypeExists(ByVal ProviderType As String, Optional ByVal ProviderTypeID As Integer = -1) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = m_ConnectionString
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_CheckProviderTypeExists"
        Dim objParaProviderType As New SqlParameter
        With objParaProviderType
            .ParameterName = "@ProviderType"
            .Value = ProviderType
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProviderType)

        If ProviderTypeID <> -1 Then
            Dim objParaProviderTypeID As New SqlParameter
            With objParaProviderTypeID
                .ParameterName = "@ProviderTypeID"
                .Value = ProviderTypeID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaProviderTypeID)
        End If
        objCmd.Connection = objCon
        Dim nCount As Integer
        objCon.Open()
        nCount = objCmd.ExecuteScalar
        objCon.Close()
        objCon = Nothing
        If nCount = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function DeleteProviderType(ByVal ProviderType As String) As Boolean
        Try
            m_ErrorMessage = ""
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeleteProviderType"
            objCmd.Parameters.Clear()
            Dim objParaProviderType As New SqlParameter
            With objParaProviderType
                .ParameterName = "@ProviderType"
                .Value = ProviderType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProviderType)


            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()

            objCon.Close()

            ''Sandip Darade 20090717
            ''above code deletes doctor type from ProviderType_MST
            ''to delete  doctor type from AB_Resource_ProviderType

            Dim obj As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
            obj.Connect(False)
            obj.Execute_Query(" DELETE AB_Resource_ProviderType WHERE sProviderType= '" & ProviderType & "'")
            obj.Dispose()

            Return True
        Catch ex As Exception
            m_ErrorMessage = ex.Message
            Return False
        End Try
    End Function

#End Region
End Class
