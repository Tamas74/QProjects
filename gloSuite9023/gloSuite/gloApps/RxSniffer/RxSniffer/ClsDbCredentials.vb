Imports System.Windows.Forms
Imports RxSniffer.RxGeneral

Public Class ClsDbCredentials

    Dim _DatabaseName As String
    Dim _ServerName As String
    Dim _SqlUSer As String
    Dim _SqlPassword As String
    Dim _DatabaseID As Int64



    Public Property DatabaseID() As Int64
        Get
            Return _DatabaseID
        End Get
        Set(ByVal value As Int64)
            _DatabaseID = value
        End Set
    End Property

    Public Property DatabaseName() As String
        Get
            Return _DatabaseName
        End Get
        Set(ByVal value As String)
            _DatabaseName = value
        End Set
    End Property

    Public Property ServerName() As String
        Get
            Return _ServerName
        End Get
        Set(ByVal value As String)
            _ServerName = value
        End Set
    End Property

    Public Property SqlUSer() As String
        Get
            Return _SqlUSer
        End Get
        Set(ByVal value As String)
            _SqlUSer = value
        End Set
    End Property

    Public Property SqlPassword() As String
        Get
            Return _SqlPassword
        End Get
        Set(ByVal value As String)
            _SqlPassword = value
        End Set
    End Property

    Public Function saveDataBaseCredentials(ByVal objClsDbCredentials As ClsDbCredentials) As Integer
        Dim oDbParameter As gloDatabaseLayer.DBParameters
        Dim oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
        oDbLayer.Connect(False)
        Dim databaseId As Object = Nothing
        Try
            'string strInsertQuery = string.Empty;
            oDbParameter = New gloDatabaseLayer.DBParameters
            oDbParameter.Add("@nDBConnectionId", objClsDbCredentials.DatabaseID, ParameterDirection.Output, SqlDbType.Int)
            oDbParameter.Add("@nDBId", objClsDbCredentials.DatabaseID, ParameterDirection.Input, SqlDbType.Int)
            oDbParameter.Add("@sDatabaseName", objClsDbCredentials.DatabaseName, ParameterDirection.Input, SqlDbType.VarChar)
            oDbParameter.Add("@sServerName", objClsDbCredentials.ServerName, ParameterDirection.Input, SqlDbType.VarChar)
            oDbParameter.Add("@sSqlUserName", objClsDbCredentials.SqlUSer, ParameterDirection.Input, SqlDbType.VarChar)
            oDbParameter.Add("@sSqlPassword", objClsDbCredentials.SqlPassword, ParameterDirection.Input, SqlDbType.VarChar)
            oDbParameter.Add("@bEnabled", True, ParameterDirection.Input, SqlDbType.Bit)
            oDbParameter.Add("@bIsConnected", True, ParameterDirection.Input, SqlDbType.Bit)
            oDbParameter.Add("@sServiceName", "RxSniffer", ParameterDirection.Input, SqlDbType.VarChar)
            oDbLayer.Execute("INUP_DbCredentialds", oDbParameter, databaseId)
            'Check of duplicate entry for DatabaseName and Sql server is done in SP.
            'It returns zero if same DatabaseName and Sql server exists.
            If (Convert.ToInt32(databaseId) = 0) Then
                'MessageBox.Show("Database Credentials are already exists.")
            End If
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString)
        Finally
            If (Not (oDbLayer) Is Nothing) Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
            End If
        End Try
        Return Convert.ToInt32(databaseId)
    End Function

    ''' <summary>
    ''' Method to delete DataBase Credentials.
    ''' </summary>
    Public Sub deleteDataBaseCredentials(ByVal DatabaseID As Int64)
        Dim oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
        'string strInsertQuery = "";
        Try
            Dim strInsertQuery As String = String.Empty
            strInsertQuery = "Delete from  DBSettings where nDBConnectionId=" & mdlGeneral.DatabaseID
            oDbLayer.Connect(False)
            oDbLayer.Execute_Query(strInsertQuery)
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString)
        Finally
            If (Not (oDbLayer) Is Nothing) Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Method to retrive all database credentials.
    ''' </summary>
    ''' <returns></returns>
    Public Function getDataBaseCredentials(ByVal DatabaseID As Int64) As DataTable
        Dim oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
        Dim DbCredentialsTable As DataTable = New DataTable
        Dim strSelectQuery As String = String.Empty
        Try
            oDbLayer.Connect(False)
            If (DatabaseID = 0) Then
                strSelectQuery = "SELECT nDBConnectionId,sServerName,sDatabaseName,sSqlUserName,sSqlPassword FROM DBSettings where sServiceName='RxSniffer'"
            Else
                strSelectQuery = "SELECT nDBConnectionId,sServerName,sDatabaseName,sSqlUserName,sSqlPassword FROM DBSettings where nDBConnectionId=" & DatabaseID
            End If
            oDbLayer.Retrive_Query(strSelectQuery, DbCredentialsTable)
        Catch ex As Exception
            mdlGeneral.UpdateLog(ex.ToString)
            DbCredentialsTable = Nothing
        Finally
            If (Not (oDbLayer) Is Nothing) Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
            End If
        End Try
        Return DbCredentialsTable
    End Function




End Class
