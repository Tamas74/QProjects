

Imports System.Net
Imports System.Data.SqlClient
Imports gloSettings
Imports System.Xml
Imports System.Xml.Linq
Imports System.ComponentModel
Imports System.Threading

Public Class clsProviderEducation

    Private ConnectionString As String = ""
    Public Sub New(ByVal ConString As String)
        ConnectionString = ConString
    End Sub

    Public Function SaveProviderEducation(ByVal nVisitID As Long, ByVal oResult As Object, ByVal sTitle As String, ByVal DocumentURL As String, Optional ProviderID As Long = 0) As Boolean

        Dim ProviderEducationID As Long = 0

        Dim Con As New SqlConnection(ConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        Dim Result As Integer = 0

        Try
            '' Insert Or Update problem List
            cmd = New SqlCommand("gsp_InUpExamEducation", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nVisitID

            sqlParam = cmd.Parameters.Add("@DocumentTitle", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sTitle

            sqlParam = cmd.Parameters.Add("@sPENotes", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(oResult) Then
                sqlParam.Value = oResult
            Else
                sqlParam.Value = DBNull.Value
            End If

            sqlParam = cmd.Parameters.Add("@DocumentURL", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DocumentURL

            sqlParam = cmd.Parameters.Add("@ProviderID", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ProviderID

            sqlParam = cmd.Parameters.Add("@ProviderEducationID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Output


            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            Result = cmd.ExecuteNonQuery()
            ProviderEducationID = cmd.Parameters("@ProviderEducationID").Value
            Con.Close()
            If ProviderEducationID > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            Return False
        Catch ex As Exception
            Return False
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try



    End Function

    Public Function GetProviderEducation(Optional ByVal ProviderId As Long = 0, Optional ByVal Flag As Long = 0, Optional ByVal ProviderEducationId As Long = 0) As DataTable
        Dim dtProviderEducation As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParameters As New gloDatabaseLayer.DBParameters()

        oDB.Connect(False)

        Try
        
            oParameters.Add("@Flag", Flag, ParameterDirection.Input, SqlDbType.Int)
            oParameters.Add("@ProviderId", ProviderId, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@ProviderEducationID", ProviderEducationId, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_getProviderEducationDocument", oParameters, dtProviderEducation)

            Return dtProviderEducation
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oParameters.Dispose()

            If dtProviderEducation IsNot Nothing Then
                dtProviderEducation.Dispose()
            End If
        End Try

    End Function

End Class
