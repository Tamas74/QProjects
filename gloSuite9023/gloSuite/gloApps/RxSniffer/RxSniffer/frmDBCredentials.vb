Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Data.SqlClient
Imports RxSniffer.RxGeneral

Public Class frmDBCredentials
    Dim _connString As String = String.Empty
    Dim regKey As RegistryKey
    Dim objEncryption As clsEncryption
    Dim bitValidate As Boolean = False


    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If (txtServer.Text.Trim = "") Then
            MessageBox.Show(Me, "Please enter SQL Server name.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            bitValidate = False
            txtServer.Focus()
            Return
        ElseIf (txtDatabase.Text.Trim = "") Then
            MessageBox.Show(Me, "Please enter Database name.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDatabase.Focus()
            bitValidate = False
            Return
        ElseIf (txtUser.Text.Trim = "") Then
            MessageBox.Show(Me, "Please enter SQL Server User.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            bitValidate = False
            txtUser.Focus()
            Return
        ElseIf (txtPassword.Text.Trim = "") Then
            MessageBox.Show(Me, "Please enter SQL Server Password.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            bitValidate = False
            txtPassword.Focus()
            Return
        ElseIf (IsConnect(txtServer.Text.Trim, txtDatabase.Text.Trim, txtUser.Text.Trim, txtPassword.Text.Trim) = False) Then
            txtServer.Focus()
            MessageBox.Show(Me, "Invalid Login Credentials.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            bitValidate = False
            Return
        ElseIf Not checkScripts("SERVER = " + txtServer.Text.Trim + ";UID = " + txtUser.Text.Trim + ";PWD=" + txtPassword.Text.Trim + ";DATABASE= " + txtDatabase.Text.Trim, "Settings") Then
            txtDatabase.Focus()
            MessageBox.Show("Please select valid EMR database", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            bitValidate = False
            Return
        Else
            bitValidate = True
        End If
        Dim dbID As Integer = 0
        Dim strPassword As String = String.Empty
        If bitValidate Then
            objEncryption = New clsEncryption
            Dim objClsDbCredentials As ClsDbCredentials = New ClsDbCredentials
            objClsDbCredentials.DatabaseName = txtDatabase.Text.Trim
            objClsDbCredentials.ServerName = txtServer.Text.Trim
            strPassword = objEncryption.EncryptToBase64String(txtPassword.Text.Trim, mdlGeneral.constEncryptDecryptKeyDB)
            objClsDbCredentials.SqlPassword = strPassword
            objClsDbCredentials.SqlUSer = txtUser.Text.Trim
            objClsDbCredentials.DatabaseID = mdlGeneral.DatabaseID
            dbID = objClsDbCredentials.saveDataBaseCredentials(objClsDbCredentials)
            If dbID = 0 Then
                Return
            Else
                Me.Close()
            End If

        End If
        mdlGeneral.SetDbCredentials()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        '  mdlGeneral.getDBCount(_connString)

        Me.Close()
    End Sub
    Public Function IsConnect(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal strUserName As String, ByVal strPassword As String) As Boolean
        Dim _bIsConnect As Boolean = False
        'Create the object of SQL Connection class 
        Dim objCon As SqlConnection = New SqlConnection
        Try
            'Assign the connection string 
            Dim strConnectionString As String = ""
            strConnectionString = "SERVER = " + strSQLServerName + ";UID = " + strUserName + ";PWD=" + strPassword + ";DATABASE= " + strDatabase
            objCon.ConnectionString = strConnectionString
            'Open the connection 
            objCon.Open()
            _bIsConnect = True
            'Connection to SQL Server database successfully established 
        Catch ex As Exception
            _bIsConnect = False
            mdlGeneral.UpdateLog(("Error in IsConnect : " + ex.ToString))
        Finally
            'Close the connection 
            objCon.Close()
            'Connection to SQL Server database is not established 
            objCon = Nothing
        End Try
        Return _bIsConnect
    End Function


    Private Sub frmDBCredentials_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (mdlGeneral.DatabaseID <> 0) Then
            Dim dtDbcredentials As DataTable = New DataTable
            Dim objClsDbCredentials As ClsDbCredentials = New ClsDbCredentials
            dtDbcredentials = objClsDbCredentials.getDataBaseCredentials(mdlGeneral.DatabaseID)
            If ((dtDbcredentials.Rows.Count > 0) AndAlso (Not (dtDbcredentials) Is Nothing)) Then
                txtServer.Text = dtDbcredentials.Rows(0)("sServerName").ToString
                txtUser.Text = dtDbcredentials.Rows(0)("sSqlUserName").ToString
                txtDatabase.Text = dtDbcredentials.Rows(0)("sDatabaseName").ToString
                mdlGeneral.DatabaseID = Convert.ToInt64(dtDbcredentials.Rows(0)("nDBConnectionId").ToString)
                objEncryption = New clsEncryption
                txtPassword.Text = objEncryption.DecryptFromBase64String(dtDbcredentials.Rows(0)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
            End If
        Else
            txtServer.Text = ""
            txtUser.Text = ""
            txtDatabase.Text = ""
            txtPassword.Text = ""
        End If
    End Sub
    Public Shared Function checkScripts(ByVal strConnection As String, ByVal strTableName As String) As Boolean
        'Checking Tables exists or not.
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(strConnection)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim Cnt As Object = Nothing
        Try
            oDBLayer.Connect(False)

            oDBParameters.Clear()
            oDBParameters.Add("@sTableName", strTableName, ParameterDirection.InputOutput, SqlDbType.VarChar)

            oDBLayer.Execute("TABLEEXISTS", oDBParameters, Cnt)

            If Convert.ToInt32(Cnt) <= 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            UpdateLog(ex.ToString())
            Return False
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
            If oDBLayer IsNot Nothing Then
                oDBLayer.Disconnect()
                oDBLayer.Dispose()
            End If
            If Cnt IsNot Nothing Then
                Cnt = Nothing
            End If
        End Try

    End Function
End Class