Imports EPA = gloGlobal.EPA
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class ePADatabaseLayer

    Function GetUserRoles(nProviderID As Long) As DataSet

        Dim sqlConnection As SqlConnection = Nothing
        Dim sqlParameter As SqlParameter = Nothing
        Dim dsReturned As DataSet = New DataSet()
        Dim selectCommand As SqlCommand = Nothing
        Try
            selectCommand = New SqlCommand("epa_getUserRoles", New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString()))
            sqlParameter = New SqlParameter()

            With sqlParameter
                .ParameterName = "@nProviderID"
                .Value = nProviderID
                .SqlDbType = SqlDbType.BigInt
                .Direction = ParameterDirection.Input
            End With

            With selectCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add(sqlParameter)
            End With

            Using dataAdapter As New SqlDataAdapter(selectCommand)
                dataAdapter.Fill(dsReturned)
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        Return dsReturned
    End Function

    Public Function SaveRoles(ByVal ProviderID As Int64, ByVal TVP As DataTable) As Boolean
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand
            Dim oparam As SqlParameter
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "epa_INUPUserRoles"

            oparam = New SqlParameter("@nProviderID", SqlDbType.BigInt)
            oparam.Value = ProviderID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            oparam = New SqlParameter("@TVP", SqlDbType.Structured)
            oparam.Value = TVP
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

    Public Sub SaveRoles(ByVal ProviderID As Long, ByRef ePAList As List(Of EPARole))

        Dim dtTVPAssociation As New DataTable("TVP_EPA_UserRole")

        With dtTVPAssociation
            .Columns.Add(New DataColumn("nProviderID", System.Type.GetType("System.Int64")))
            .Columns.Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
            .Columns.Add(New DataColumn("nPARoleID", System.Type.GetType("System.Int64")))
        End With

        For Each element As EPARole In ePAList
            Dim row As DataRow = dtTVPAssociation.NewRow()
            row("nProviderID") = ProviderID
            row("nUserID") = element.UserID
            row("nPARoleID") = element.EPARole
            dtTVPAssociation.Rows.Add(row)
            row = Nothing
        Next

        Me.SaveRoles(ProviderID, dtTVPAssociation)

    End Sub


End Class

Public Class EPARole

#Region "Fields"
    Private _ePARole As EPA.RoleType
#End Region

#Region "Properties"
    Public Property UserID As Int64 = 0
    Public Property LoginName As String = ""   

    Public Property EPARole() As EPA.RoleType
        Get
            Return _ePARole
        End Get
        Set(ByVal value As EPA.RoleType)
            _ePARole = value
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New(ByVal UserID As Int64, LoginName As String, ByVal EPARole As EPA.RoleType)
        Me.UserID = UserID
        Me.LoginName = LoginName
        Me.EPARole = EPARole
    End Sub
#End Region

End Class
