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
Public Class clsRights

#Region " Private Variables"
    Dim _nRightsID As Integer
    Dim _sRightsName As String
    Dim _sRightsValue As String
    Dim _sParentRightsName As String
#End Region

#Region " Public Properties"
    Public ReadOnly Property RightsID() As Integer
        Get
            Return _nRightsID
        End Get
    End Property
    Public ReadOnly Property RightsName() As String
        Get
            Return _sRightsName
        End Get
    End Property
    Public ReadOnly Property RightsValue() As String
        Get
            Return _sRightsValue
        End Get
    End Property
    Public ReadOnly Property ParentRightsName() As String
        Get
            Return _sParentRightsName
        End Get
    End Property
#End Region

#Region " Public Functions"
    Public Function ScanParentRights() As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        '    Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillParentRights"

        ''Sandip Darade 20091103
        ''Added new parameter ApplicationType
        Dim objApplicationType As New SqlParameter
        With objApplicationType
            .ParameterName = "@ApplicationType"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objApplicationType)

        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        Return dsData.Tables(0)
    End Function
    Public Function ScanChildRights(ByVal strParentRightsName As String) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        '  Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillChildRights"
        Dim objParaParentRightsName As New SqlParameter
        With objParaParentRightsName
            .ParameterName = "@ParentRightsName"
            .Value = strParentRightsName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaParentRightsName)

        ''Sandip Darade 20091103
        ''Added new parameter ApplicationType
        Dim objApplicationType As New SqlParameter
        With objApplicationType
            .ParameterName = "@ApplicationType"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objApplicationType)


        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        Return dsData.Tables(0)
    End Function
#End Region

End Class
