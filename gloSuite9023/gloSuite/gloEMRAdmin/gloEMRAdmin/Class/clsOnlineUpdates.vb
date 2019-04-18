'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Imports System.Data.SqlClient
Public Class clsOnlineUpdates

#Region " Private Variables"
    Dim _nUpdatesID As Integer
    Dim _dtUpdatesDate As DateTime
    Dim _sVersionNo As String
    Dim _sComments As String
#End Region

#Region " Public Properties"
    Public Property UpdatesID() As Integer
        Get
            Return _nUpdatesID
        End Get
        Set(ByVal Value As Integer)
            _nUpdatesID = Value
        End Set
    End Property
    Public Property UpdatesDate() As DateTime
        Get
            Return _dtUpdatesDate
        End Get
        Set(ByVal Value As DateTime)
            _dtUpdatesDate = Value
        End Set
    End Property
    Public Property VersionNo() As String
        Get
            Return _sVersionNo
        End Get
        Set(ByVal Value As String)
            _sVersionNo = Value
        End Set
    End Property
    Public Property Comments() As String
        Get
            Return _sComments
        End Get
        Set(ByVal Value As String)
            _sComments = Value
        End Set
    End Property
#End Region

#Region " Public Functions"
    Public Function Fill_Versions() As Collection
        Dim clVersions As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillVersions"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            While objSQLDataReader.Read
                clVersions.Add(objSQLDataReader.Item(0))
            End While
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return clVersions
    End Function
    Public Function ViewOnlineUpdates(ByVal strVersion As String) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ViewOnlineUpdates"
        objCmd.Parameters.Clear()
        Dim objParaVersionNo As New SqlParameter
        With objParaVersionNo
            .ParameterName = "@VersionNo"
            .Value = strVersion
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaVersionNo)

        objCmd.Connection = objCon
        Dim dsData As New DataSet
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function DoOnlineUpdates(ByVal dtUpdatesDate As DateTime, ByVal strVersionNo As String, ByVal strComments As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        ' Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_InsertUpdates"

        Dim objParaUpdatesDate As New SqlParameter
        With objParaUpdatesDate
            .ParameterName = "@UpdatesDate"
            .Value = Date.Now.Date
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaUpdatesDate)


        Dim objParaVersion As New SqlParameter
        With objParaVersion
            .ParameterName = "@VersionNo"
            .Value = strVersionNo
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaVersion)

        Dim objParaComments As New SqlParameter
        With objParaComments
            .ParameterName = "@Comments"
            .Value = strComments
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaComments)

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function
#End Region
End Class
