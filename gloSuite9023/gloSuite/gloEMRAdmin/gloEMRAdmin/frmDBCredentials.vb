Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Data.SqlClient
'Imports RxSniffer.RxGeneral

Public Class frmDBCredentials


    Dim _connString As String = String.Empty
    Dim regKey As RegistryKey
    Dim objEncryption As clsEncryption
    Dim bitValidate As Boolean = False
    '''''''Added on 20100701 by sanjog 
    Private _UserName As String
    Private _PassWord As String
    '''''''Added on 20100701 by sanjog 
    Private _ID As Int64 = 0
    Private _ServerName As String
    Private _DatabaseName As String
    Private _Default As Boolean

    Public Sub New(ByVal nID As Int64, ByVal sServerName As String, ByVal sDatabaseName As String, ByVal blnDefault As Boolean, ByVal username As String, ByVal passwrd As String)
        InitializeComponent()
        
        _ID = nID
        _ServerName = sServerName
        _DatabaseName = sDatabaseName
        _Default = blnDefault
        '''''''Added on 20100701 by sanjog 
        _UserName = username
        _PassWord = passwrd
        '''''''Added on 20100701 by sanjog 
    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If (txtServer.Text.Trim = "") Then
            ''MessageBox.Show(Me, "Please enter SQL Server name.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            MessageBox.Show(Me, "Please enter SQL Server name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            bitValidate = False
            txtServer.Focus()
            Return
        ElseIf (txtDatabase.Text.Trim = "") Then
            ''MessageBox.Show(Me, "Please enter Database name.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            MessageBox.Show(Me, "Please enter Database name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDatabase.Focus()
            bitValidate = False
            Return
        ElseIf (txtUser.Text.Trim = "") Then
            ''MessageBox.Show(Me, "Please enter SQL Server User.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            MessageBox.Show(Me, "Please enter SQL Server User.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            bitValidate = False
            txtUser.Focus()
            Return
        ElseIf (txtPassword.Text.Trim = "") Then
            ''MessageBox.Show(Me, "Please enter SQL Server Password.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            MessageBox.Show(Me, "Please enter SQL Server Password.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            bitValidate = False
            txtPassword.Focus()
            Return
        ElseIf (IsConnect(txtServer.Text.Trim, txtDatabase.Text.Trim, txtUser.Text.Trim, txtPassword.Text.Trim) = False) Then
            txtServer.Focus()
            ''MessageBox.Show(Me, "Invalid Login Credentials.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MessageBox.Show(Me, "Invalid Login Credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            bitValidate = False
            Return
        Else
            bitValidate = True
        End If

        

        '' check whether entry exists or not ''
        Dim objDBExists As New ClsMultipleDb
        Dim objAdminNew As New frmgloEMRAdmin()
        objEncryption = New clsEncryption
        Dim oDataBase As New ClsMultipleDb

        ''Checking the checkbox state
        oDataBase.isDefaultDatabase = chkDefaultDatabase.Checked

        If objDBExists.IsDatabaseExists(_ID, txtServer.Text.Trim(), txtDatabase.Text.Trim()) Then
            MessageBox.Show("Database already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        '' --------------------------------- '' 
        
        Dim dbID As Int64 = 0
        Dim strPassword As String = String.Empty
        If bitValidate Then
           
            
            oDataBase.DatabaseNames = txtDatabase.Text.Trim
            oDataBase.ServerNames = txtServer.Text.Trim
            '' TO Encrypt the Password to save it to Database 
            strPassword = objEncryption.EncryptToBase64String(txtPassword.Text, mdlGeneral.constEncryptDecryptKey)
            oDataBase.SqlPasswords = strPassword
            oDataBase.SqlUserName = txtUser.Text
            oDataBase.DBConnectionId = _ID
            dbID = oDataBase.saveDataBaseCredentials(oDataBase)
            Me.Close()
        End If
        ' mdlGeneral.SetDbCredentials()


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
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
            'mdlGeneral.UpdateLog(("Error in IsConnect : " + ex.ToString))
        Finally
            'Close the connection 
            objCon.Close()
            'Connection to SQL Server database is not established 
            objCon = Nothing
        End Try
        Return _bIsConnect
    End Function


    Private Sub frmDBCredentials_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If _ID > 0 Then
            txtServer.Text = _ServerName
            txtDatabase.Text = _DatabaseName
            '''''''Added on 20100701 by sanjog to show UserName and password 
            txtUser.Text = _UserName
            '' TO Encrypt the Password to save it to Database 
            Dim oEncryption As New clsEncryption
            _PassWord = oEncryption.DecryptFromBase64String(_PassWord, mdlGeneral.constEncryptDecryptKey)
            oEncryption = Nothing
            ''TO Encrypt the Password to save it to Database 
            txtPassword.Text = _PassWord
            '''''''Added on 20100701 by sanjog to show UserName and password 
            chkDefaultDatabase.Checked = _Default
            chkDefaultDatabase.Enabled = Not chkDefaultDatabase.Checked
        Else
            txtServer.Text = ""
            txtUser.Text = ""
            txtDatabase.Text = ""
            txtPassword.Text = ""
            chkDefaultDatabase.Checked = False
        End If
    End Sub

End Class