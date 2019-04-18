Imports System.Data
Imports System.Data.SqlClient
Public Class clsMedicationDBLayer
    Public Sub New()
        'Dim sqlconn As String
        'sqlconn = GetConnectionString()
        'Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub
    
    Public Function Fill_LockMedication(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable

        Try
            Dim Conn As SqlConnection
            Dim sqlconn As String
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
            Conn.Open()
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachinName

            objParam = Cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TransactionType

            objParam = Cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0
            Dim sqladpt As New SqlDataAdapter

            sqladpt.SelectCommand = Cmd

            Dim dt As New DataTable
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Conn.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
End Class
