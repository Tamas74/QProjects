Imports System.Data
Imports System.Data.SqlClient
Public Class ClsPrintFlowSheet
    'Private Conn As SqlConnection
    'Private Adapter As System.Data.SqlClient.SqlDataAdapter
    ''Private sqlreader As System.Data.SqlClient.SqlDataReader
    ''Private sqlcmmd As System.Data.SqlClient.SqlCommand
    'Private Tb As DataTable
    'Private Cmd As System.Data.SqlClient.SqlCommand
    Public Sub New()
        'Dim sqlconn As String
        'sqlconn = GetConnectionString()
        'Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub
    Public Function ScanClinicInfo() As ClinicDetails
        Try
            Dim sqlconn As String
            sqlconn = GetConnectionString()
            Dim Conn As SqlConnection = New System.Data.SqlClient.SqlConnection(sqlconn)



            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanClinic", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@ClinicID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 1

            Dim dreader As SqlDataReader

            Conn.Open()
            dreader = Cmd.ExecuteReader
            Dim structClinicInfo As ClinicDetails = New ClinicDetails()

            Do While dreader.Read()
                structClinicInfo.m_Clinicname = dreader.Item(0)
                structClinicInfo.m_ClinicAddress1 = dreader.Item(1)
                structClinicInfo.m_ClincAddress2 = dreader.Item(2)
                structClinicInfo.m_PhoneNo = dreader.Item(3)
            Loop

            dreader.Close()
            Conn.Close()
            Conn.Dispose()
            Conn = Nothing
            dreader = Nothing
            objParam = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            Return structClinicInfo
        Catch ex As Exception
            
            Throw ex

        Finally

        End Try

    End Function
End Class

Public Structure ClinicDetails
    Public m_Clinicname As String
    Public m_ClinicAddress1 As String
    Public m_ClincAddress2 As String
    Public m_PhoneNo As String

    ''''added in 8081-replace DataGridPrinter
    Public m_PatientName As String
    Public m_PatDOB As String
    Public m_FlowSheetName As String
End Structure
