Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Data.SqlClient
Public Class frmPrefix
    Private _ID As Long = 0
    Private _Server As String
    Private _Database As String
    Private _prefix As String

    Public Sub New(ByVal nID As String, ByVal sServer As String, ByVal sDatabase As String, ByVal sPrefix As String)
        InitializeComponent()
        _ID = nID
        _Server = sServer
        _Database = sDatabase
        _prefix = sPrefix

    End Sub
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If Trim(txtServer.Text) = "" Then
                MessageBox.Show("Enter server name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf Trim(txtDatabase.Text) = "" Then
                MessageBox.Show("Enter database name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf txtPrefix.Text.Trim.Length < 3 Then
                MessageBox.Show("Enter 3 digit prefix", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf checkExists(1) = True Then  ' value 1 to check server and database
                MessageBox.Show("Database and Server name already exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf checkExists(2) = True Then  ' value 2 to check prefix
                MessageBox.Show("Prefix already in use.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Save_Prefix()
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub frmPrefix_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If _ID > 0 Then
                txtServer.Text = _Server
                txtDatabase.Text = _Database
                txtPrefix.Text = _prefix
            Else
                txtServer.Text = ""
                txtDatabase.Text = ""
                txtPrefix.Text = ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
    Public Function Save_Prefix() As Boolean
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "sp_InUp_Prefix"

            Dim objID As New SqlParameter
            With objID
                .ParameterName = "@nPrefixID"
                .Value = _ID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objID)

            Dim objServer As New SqlParameter
            With objServer
                .ParameterName = "@sServer"
                .Value = Trim(txtServer.Text)
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objServer)

            Dim objDatabase As New SqlParameter
            With objDatabase
                .ParameterName = "@sDatabase"
                .Value = Trim(txtDatabase.Text)
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objDatabase)

            Dim objPrefix As New SqlParameter
            With objPrefix
                .ParameterName = "@sPrefix"
                .Value = Trim(txtPrefix.Text)
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objPrefix)

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCmd = Nothing
            objCon = Nothing
            Return True
       
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Function
    Public Function delete_Prefix(ByVal nprefixId As Long) As Boolean
        Try

       
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "sp_Delete_Prefix"


            Dim objServer As New SqlParameter
            With objServer
                .ParameterName = "@nPrefixID"
                .Value = nprefixId
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objServer)

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCmd = Nothing
            objCon = Nothing
            Return True
       
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Function
    Private Sub txtPrefix_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrefix.KeyPress
        ValidateNumeric(txtPrefix.Text, e)
    End Sub
    Public Function ValidateNumeric(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            If Char.IsNumber(e.KeyChar) = False And AscW(e.KeyChar) <> 8 Then
                e.Handled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Function
    Public Function checkExists(ByVal FeildName As Integer) As Boolean
        Dim _Query As String
        Dim objcon As New SqlClient.SqlConnection
        objcon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim cmd As SqlClient.SqlCommand
        Dim _blnResult As Boolean = False
        Dim _strResult As Integer = 0

        Try
            If FeildName = 1 Then
                _Query = "SELECT COUNT(*) FROM Prefix WHERE sServer = '" & Trim(txtServer.Text) & "' AND sDatabase= '" & Trim(txtDatabase.Text) & "' "
            Else
                _Query = "SELECT COUNT(*) FROM Prefix WHERE sPrefix = '" & Trim(txtPrefix.Text) & "'"
            End If

            cmd = New SqlClient.SqlCommand(_Query, objcon)

            objcon.Open()
            _strResult = cmd.ExecuteScalar
            objcon.Close()
            If _strResult > 0 Then
                _blnResult = True
            End If

            Return _blnResult
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            objcon = Nothing
        End Try
    End Function
End Class
