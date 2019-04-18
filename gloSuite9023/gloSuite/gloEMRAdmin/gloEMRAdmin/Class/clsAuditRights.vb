'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Rahul Purkar

'***************************************************************************

Imports System.Data.SqlClient
Public Class clsAuditRights

#Region " Private Variables"
    Dim _nModuleId As Integer
    Dim _sModuleName As String
#End Region

#Region " Public Properties"
    Public ReadOnly Property ModuleId() As Integer
        Get
            Return _nModuleId
        End Get
    End Property
    Public ReadOnly Property RightsName() As String
        Get
            Return _sModuleName
        End Get
    End Property

#End Region

#Region " Public Functions"
    Public Function ScanAuditRights() As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand

        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillAuditRights"
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function

#End Region

End Class
