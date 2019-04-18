Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.OleDb


Partial Public Class frmUpdatePayerID
    Inherits Form
    Private _databaseconnectionstring As String = ""
    Private _messageboxcaption As String = "gloPM"
    Private _ClinicID As Int64 = 0
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings


    Public Sub New(ByVal DatabaseConnectionString As String)
        InitializeComponent()

        _databaseconnectionstring = DatabaseConnectionString

        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID") <> "" Then
                _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
            Else
                _ClinicID = 1
            End If
        Else
            _ClinicID = 1
        End If
    End Sub

    Private Sub tsb_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsb_Cancel.Click
        Me.Close()
    End Sub

    Private Sub tsb_OK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsb_OK.Click
        Try
            If txtCSVFileName.Text.Trim() <> "" Then
                Dim _fileName As String = txtCSVFileName.Text.Trim()
                If File.Exists(_fileName) = True Then
                    Me.Cursor = Cursors.WaitCursor
                    If UpdatePayerID(_fileName) = True Then
                        Me.Close()
                    End If
                Else
                    MessageBox.Show("File not found. ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                Me.Cursor = Cursors.[Default]
                MessageBox.Show("Please select file. ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

            MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            Me.Cursor = Cursors.[Default]
        End Try
    End Sub

    Private Sub btnBrowseFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseFile.Click
        Try
            dlgBrowseFile.Title = " Browse File "
            dlgBrowseFile.Filter = "Excel Files(*.xls;*.xlsx)|*.xls;*.xlsx"
            dlgBrowseFile.CheckFileExists = True
            dlgBrowseFile.Multiselect = False
            dlgBrowseFile.ShowHelp = False
            dlgBrowseFile.ShowReadOnly = False

            If dlgBrowseFile.ShowDialog() = DialogResult.OK Then
                txtCSVFileName.Text = dlgBrowseFile.FileName
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnClearFileName_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearFileName.Click

        Try
            txtCSVFileName.Clear()
        Catch ex As Exception
        End Try
    End Sub


    Private Function UpdatePayerID(ByVal _fileName As String) As Boolean
        Dim _Result As Boolean = False
        Dim oFileConnection As New System.Data.OleDb.OleDbConnection()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Try

            Dim sConnectionString As [String] = "Data Source=" & _fileName & "; Provider=Microsoft.Jet.oledb.4.0; Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;"""
            'String sConnectionString = "Data Source=" + _fileName + @"; Provider=Microsoft.Jet.OLEDB.4.0; Extended Properties="""; 

            If File.Exists(_fileName) = False Then
                Return _Result
            End If

            Dim sDirectoryName As String = ""
            Dim sFileName As String = ""
            sDirectoryName = System.IO.Path.GetDirectoryName(_fileName)


            Dim oFileInfo As New FileInfo(_fileName)
            sFileName = oFileInfo.Name

            oFileConnection.ConnectionString = sConnectionString
            oFileConnection.Open()

            Dim dtPayers As New DataTable()
            Dim schemaTable As DataTable = oFileConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

            If schemaTable.Rows.Count > 0 Then
                Dim sheetName As String = schemaTable.Rows(0)("TABLE_NAME").ToString()

                Dim oAdapter As New OleDbDataAdapter("SELECT * FROM [" & sheetName & "]", oFileConnection)
                oAdapter.Fill(dtPayers)
            End If
            'payerid,payername,states,phone 
            If dtPayers IsNot Nothing AndAlso dtPayers.Columns.Count >= 3 Then
                oDB.Connect(False)

                Dim _sqlQuery As String = ""
                Dim ContactUpdateCount As Integer = 0
                Dim PatientUpdateCount As Integer = 0
                For i As Integer = 0 To dtPayers.Rows.Count - 1


                    _sqlQuery = "UPDATE Contacts_Insurance_DTL SET sPayerId = '" & Convert.ToString(dtPayers.Rows(i)(0)).Replace("'", "''").Trim().ToUpper() & "' " _
                    & "  FROM Contacts_MST INNER JOIN Contacts_Insurance_DTL ON Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID " & " WHERE UPPER(Contacts_MST.sName) = '" & Convert.ToString(dtPayers.Rows(i)(1)).Replace("'", "''").Replace("""", "").Trim().ToUpper() & "' AND Contacts_MST.sContactType = 'Insurance' AND Contacts_MST.nClinicID = " & _ClinicID & ""


                    ContactUpdateCount += oDB.Execute_Query(_sqlQuery)

                    _sqlQuery = " UPDATE PatientInsurance_DTL SET sPayerID = '" & Convert.ToString(dtPayers.Rows(i)(0)).Replace("'", "''").Trim().ToUpper() & "' " _
                                & " WHERE UPPER(sInsuranceName) = '" & Convert.ToString(dtPayers.Rows(i)(1)).Replace("'", "''").Replace("""", "").Trim().ToUpper() & "'"

                    PatientUpdateCount += oDB.Execute_Query(_sqlQuery)
                Next

                oDB.Disconnect()
            End If

            oFileConnection.Close()
            _Result = True
        Catch ex As Exception
            _Result = False
        Finally

        End Try
        Return _Result
    End Function
End Class

