Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class ClsPatientVerification
    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)

    End Sub
    Private Conn As SqlConnection
    Public Sub Dispose()

        
        'slr free Con
        If Not IsNothing(Conn) Then
            Conn.Dispose()
            Conn = Nothing
        End If

    End Sub


    Public Function FetchInsuranceDetails(ByVal strInsurancename As String, ByVal strtype As String) As Long
        Dim objParam As SqlParameter
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Try
            Dim intInsuranceId As Int64
            Cmd = New SqlCommand("gsp_scanContacts_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@nContactId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            objParam = Cmd.Parameters.Add("@ContactType", SqlDbType.VarChar, 15)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strtype

            objParam = Cmd.Parameters.Add("@ContactfNAme", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strInsurancename

            Conn.Open()
            intInsuranceId = Cmd.ExecuteScalar

            Conn.Close()
            If Not IsDBNull(intInsuranceId) Then
                Return intInsuranceId
            End If
            Return Nothing

        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
        End Try
    End Function
    Public Function FetchPhysicianDetails(ByVal strPhysicianName As String, ByVal strphysicianmname As String, ByVal strphysicianlname As String) As Int64
        Dim objParam As SqlParameter
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Try
            Dim intInsuranceId As Int64
            Cmd = New SqlCommand("gsp_scanContacts_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure



            objParam = Cmd.Parameters.Add("@nContactId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            objParam = Cmd.Parameters.Add("@ContactType", SqlDbType.VarChar, 15)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = "Physician"

            objParam = Cmd.Parameters.Add("@ContactfName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strPhysicianName

            objParam = Cmd.Parameters.Add("@ContactmName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strphysicianmname

            objParam = Cmd.Parameters.Add("@ContactlName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strphysicianlname

            Conn.Open()
            intInsuranceId = Cmd.ExecuteScalar

            Conn.Close()
            If Not IsDBNull(intInsuranceId) Then
                Return intInsuranceId
            End If
            Return Nothing
        Catch ex As Exception
            'MsgBox(ex.Message)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
        End Try
    End Function
End Class
