Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class frm_changepass
    'Dim conn As New SqlConnection(GetConnectionString)
    '  Dim cmd As SqlCommand
    'Dim cmd1 As SqlCommand
    Dim _strSQL As String = ""
    Dim pwd As String = ""
    ' Dim oDataReader As SqlDataReader
    'Dim myTrans As SqlTransaction
    Dim blngenpass As Boolean = False
    Dim _LoginUserName As String = ""
    'Dim objEncryption As New clsencryption
    Dim numdigits, numletters, numspchars, numcapletters, numminlength, numdays As Integer
    Dim blnPassComplexity As Boolean = False
    Dim _PatientId As Long = 0
    'Private Sub btnGenPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) , tsbtnGenPass.Click, tsbtnGenPass.Click, ToolStripButton1.Click

    'Dim strgenpwd As String

    '    Try
    '        strgenpwd = GenRandomStr()

    '        If UpdatePassword(strgenpwd) Then
    '            MessageBox.Show("The password of the user : " & gstrLoginName.Trim & " has been reset to " & strgenpwd, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '    Catch ex As Exception

    '        MessageBox.Show(ex.Message, "gloEMR ", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Finally
    '        btnGenPass.Enabled = False
    '        txtConfirmPass.Text = ""
    '        txtChangePassword.Text = ""
    '        txtOldPass.Text = ""
    '        txtOldPass.Focus()
    '        txtConfirmPass.Enabled = False
    '    End Try

    'End Sub
    Private Function UpdatePassword(ByVal genPass As String) As Boolean

        If CheckPassSetting() Then
            Dim cmd As SqlCommand = Nothing
            Dim myTrans As SqlTransaction = Nothing


            _LoginUserName = gstrLoginName

            Dim objEncryption As New clsencryption
            pwd = objEncryption.EncryptToBase64String(genPass, constEncryptDecryptKey)
            objEncryption = Nothing
            Dim conn As New SqlConnection(GetConnectionString)
            Try
                ' _strSQL = "Update User_MST set sPassword='" & pwd & "' from  where sLoginName = '" & _LoginUserName.Replace("'", "''") & "'"


                ' cmd = New SqlClient.SqlCommand(_strSQL, conn)
                conn.Open()

                ' cmd.ExecuteNonQuery()

                myTrans = conn.BeginTransaction

                cmd = conn.CreateCommand
                cmd.Transaction = myTrans

                _strSQL = "update User_MST set sPassword = '" & pwd & "' , IsPasswordReset = 0 where sLoginName = '" & gstrLoginName.Replace("'", "''") & "'"

                cmd.CommandType = CommandType.Text
                cmd.CommandText = _strSQL

                cmd.ExecuteNonQuery()


                ' MessageBox.Show("You will have to log in again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                'make the entry of the changed passord and the creation date and time in the RecentPwd_MST table
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "InsertUserNamePwd"

                'add the parameters
                Dim objLoginName As New SqlParameter
                With objLoginName
                    .ParameterName = "@LoginName"
                    .Value = gstrLoginName
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                cmd.Parameters.Add(objLoginName)

                Dim objPassword As New SqlParameter
                With objPassword
                    .ParameterName = "@sPassword"
                    .Value = pwd
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                cmd.Parameters.Add(objPassword)

                Dim objPwdCreationTime As New SqlParameter
                With objPwdCreationTime
                    .ParameterName = "@PwdCreationDate"
                    .Value = Now
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.DateTime
                End With
                cmd.Parameters.Add(objPwdCreationTime)

                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()

                myTrans.Commit()

                objLoginName = Nothing
                objPassword = Nothing
                objPwdCreationTime = Nothing
                Try
                    'Application.DoEvents()
                    Me.Dispose()
                Catch exdispose As Exception

                End Try

                Return True

            Catch ex As Exception
                Try
                    myTrans.Rollback()
                Catch ex1 As SqlException
                    If Not myTrans.Connection Is Nothing Then
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        MsgBox("An exception of type " & ex1.GetType().ToString() & "has occured while attempting to roll back the transaction.")
                        '" was encountered while attempting to roll back the transaction.")
                        'ErrorMessage = ex.Message
                    End If
                End Try
                Return Nothing
            Finally
                conn.Close()
                conn.Dispose()
                conn = Nothing
                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If myTrans IsNot Nothing Then
                    myTrans.Dispose()
                    myTrans = Nothing
                End If

                txtConfirmPass.Enabled = False
                txtChangePassword.Enabled = True
                tsbtnGenPass.Enabled = False
            End Try
        Else
            Return False
        End If

    End Function

    Function GenRandomStr() As String
        Dim str As Guid
        Dim sPwdSrting As String
        Try

            str = System.Guid.NewGuid
            sPwdSrting = Mid(str.ToString, 1, 8)

            Return sPwdSrting
        Catch ex As Exception
            Return ""
        End Try
    End Function

  

    Private Sub frm_changepass_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtConfirmPass.Enabled = False
        Dim objEncryption As New clsencryption
        Dim str As String = objEncryption.DecryptFromBase64String("Z95WSXmg7aCKg3hC8ryjGA==", constEncryptDecryptKey)
        objEncryption = Nothing
        tsbtnGenPass.Enabled = False
        checkPasswordComplexity()

    End Sub
    Private  Sub checkPasswordComplexity()
  
        Dim oda As SqlDataAdapter
        Dim dt As DataTable
        Try


            _strSQL = "select sSettingsValue from Settings where sSettingsName = 'PASSWORD COMPLEXITY'"
            oda = New SqlDataAdapter(_strSQL, GetConnectionString())
            dt = New DataTable
            oda.Fill(dt)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)(0).ToString().Trim() = "1" Then
                        blnPassComplexity = True
                    Else
                        blnPassComplexity = False
                    End If
                End If
                dt.Dispose()
                dt = Nothing
            End If
            oda.Dispose()
            oda = Nothing


        Catch ex As Exception

        End Try


 'blnPassComplexity
            
End Sub

    Private Sub txtChangePassword_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChangePassword.GotFocus
        Dim cmd As SqlCommand = Nothing
        Dim objEncryption As New clsencryption
        Dim conn As New SqlConnection(GetConnectionString)
        pwd = objEncryption.EncryptToBase64String(txtOldPass.Text.Trim(), constEncryptDecryptKey)
        objEncryption = Nothing
        _LoginUserName = gstrLoginName
        _strSQL = "select sPassword from User_MST where sLoginName = '" & _LoginUserName.Replace("'", "''") & "'"
        Try

            cmd = New SqlClient.SqlCommand(_strSQL, conn)
            conn.Open()



            Dim obj = cmd.ExecuteScalar()
            If pwd = obj.ToString() Then
                txtConfirmPass.Enabled = True
                txtChangePassword.Enabled = True
                tsbtnGenPass.Enabled = True
            Else
                txtOldPass.Text = ""
                txtChangePassword.Enabled = True
                txtConfirmPass.Enabled = False
                tsbtnGenPass.Enabled = False
                txtOldPass.Focus()
                MessageBox.Show("Enter the Correct Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception

        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
            If (IsNothing(cmd) = False) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            
        End Try
        'End If
    End Sub
    Private Function CheckPassSetting() As Boolean
        Dim oda As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try

            If blnPassComplexity = False Or blngenpass = True Then
                Return True
            End If
            '  conn.Open()
            _strSQL = "select ExpCapitalLetters, ExpNoOfLetters, ExpNoOfDigits, ExpNoOfSpecChars, ExpPwdLength, ExpTimeFrameinDays from PwdSettings"
            oda = New SqlDataAdapter(_strSQL, GetConnectionString())
            dt = New DataTable
            oda.Fill(dt)

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then

                    If Not IsDBNull(dt.Rows(0)("ExpCapitalLetters")) Then
                        numcapletters = dt.Rows(0)("ExpCapitalLetters")
                    Else
                        numcapletters = 0
                    End If
                    If Not IsDBNull(dt.Rows(0)("ExpNoOfLetters")) Then
                        numletters = dt.Rows(0)("ExpNoOfLetters")
                    Else
                        numletters = 0
                    End If
                    If Not IsDBNull(dt.Rows(0)("ExpNoOfDigits")) Then
                        numdigits = dt.Rows(0)("ExpNoOfDigits")
                    Else
                        numdigits = 0
                    End If
                    If Not IsDBNull(dt.Rows(0)("ExpNoOfSpecChars")) Then
                        numspchars = dt.Rows(0)("ExpNoOfSpecChars")
                    Else
                        numspchars = 0
                    End If
                    If Not IsDBNull(dt.Rows(0)("ExpPwdLength")) Then
                        numminlength = dt.Rows(0)("ExpPwdLength")
                    Else
                        numminlength = 0
                    End If
                    If Not IsDBNull(dt.Rows(0)("ExpTimeFrameinDays")) Then
                        numdays = dt.Rows(0)("ExpTimeFrameinDays")
                    Else
                        numdays = 0
                    End If

                Else
                    Return True
                End If

            End If

            If ValidatePassword(txtChangePassword.Text.Trim, numminlength, numcapletters, 0, numdigits, numspchars, Nothing, numletters) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            Try

                If (IsNothing(oda) = False) Then
                    oda.Dispose()
                End If
                If (IsNothing(dt) = False) Then
                    dt.Dispose()
                End If

            Catch ex As Exception

            End Try
        End Try
    End Function

    Function ValidatePassword(ByVal pwd As String, _
                  Optional ByVal minLength As Integer = 8, _
                  Optional ByVal numUpper As Integer = 0, _
                  Optional ByVal numLower As Integer = 0, _
                  Optional ByVal numNumbers As Integer = 1, _
                  Optional ByVal numSpecial As Integer = 0, _
                  Optional ByVal resStrs() As String = Nothing, _
                  Optional ByVal numLetters As Integer = 1, _
                  Optional ByVal numofdays As Integer = 0) As Boolean

        Try




            ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
            Dim upper As New System.Text.RegularExpressions.Regex("[A-Z]")
            Dim lower As New System.Text.RegularExpressions.Regex("[a-z]")
            Dim letters As New System.Text.RegularExpressions.Regex("[a-zA-Z]")
            Dim number As New System.Text.RegularExpressions.Regex("[0-9]")
            ' Special is "none of the above".
            Dim special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")


            ' Check the length.
            If Len(pwd) < minLength Then
                ' MsgBox("The  length of the password  should be atleast  " & minLength)
                MessageBox.Show("The  length of the password  should be atleast  " & minLength, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' txtPassword.Text = ""
                Return False
            End If

            ' Check for minimum number of occurrences.
            If upper.Matches(pwd).Count < numUpper Then

                MessageBox.Show("The password should contain atleast " & numUpper & " upper case letter", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '  txtPassword.Text = ""
                Return False
            End If


            If lower.Matches(pwd).Count < numLower Then

                MessageBox.Show("The password should contain atleast " & numLower & " lower case letter", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' txtPassword.Text = ""
                Return False
            End If

            If number.Matches(pwd).Count < numNumbers Then
                'MsgBox("The password should contain atleast " & numNumbers & " digits")
                MessageBox.Show("The password should contain atleast " & numNumbers & " digits", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                '  txtPassword.Text = ""
                Return False
            End If

            If special.Matches(pwd).Count < numSpecial Then
                '   MsgBox("The password should contain atleast " & numSpecial & " special characters")
                MessageBox.Show("The password should contain atleast " & numSpecial & " special characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Return False
            End If

            'If InStr(UCase(pwd), UCase(txtUserName.Text.Trim)) Then
            '    MsgBox("The password should not contain your login name")
            '    Return False
            'End If

            If UCase(pwd) = UCase(gstrLoginName) Then
                MessageBox.Show("The password should not be same as your login name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            If letters.Matches(pwd).Count < numLetters Then
                '  MsgBox("The password should contain atleast " & numLetters & " alphabet")
                MessageBox.Show("The password should contain atleast " & numLetters & " alphabet", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            '' Check whether the pwd is one of the recent pwds
            If GetRecentPwds(pwd) Then
                '   MsgBox("You have already used this password recently, so select another password")
                MessageBox.Show("You have already used this password recently, so select another password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Return False
            End If

            '' Passed all checks.
            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
            Return Nothing
        Finally

        End Try
    End Function


    Public Function GetRecentPwds(ByVal strpwd As String) As Boolean
        'if the pwd exists in the recent pwds then return true 

        '  Dim conn As New SqlConnection(GetConnectionString)
        '  Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        Dim dtRecentPwds As New DataTable
        Dim dtval As New DataTable
        Dim da As SqlDataAdapter
        Dim da2 As SqlDataAdapter

        Dim PwdStr As New Collection
        Dim blnisexists As Boolean = False

        Try
            _strSQL = "select sPassword, PwdCreationDate from RecentPwd_MST where LoginName ='" & gstrLoginName.Replace("'", "''") & "'"

            da = New SqlDataAdapter(_strSQL, GetConnectionString)
            da.Fill(dtRecentPwds)

          

            Dim Pwddate As DateTime
            
            Dim objEncryption As New clsencryption

            For i As Integer = 0 To dtRecentPwds.Rows.Count - 1
                Dim noofdays As Int64 = 0
                Pwddate = dtRecentPwds.Rows(i)("PwdCreationDate")
                _strSQL = "SELECT DATEDIFF(day,'" & dtRecentPwds.Rows(i)("PwdCreationDate") & "', dbo.gloGetDate()) AS no_of_days"
                '  cmd = New SqlCommand(_strSQL, conn)
                da2 = New SqlDataAdapter(_strSQL, GetConnectionString)
                da2.Fill(dtval)

                noofdays = Convert.ToInt64(dtval.Rows(0)(0))

                If noofdays <= 30 Then
                    '    If noofdays <= 15 Then
                    PwdStr.Add(objEncryption.DecryptFromBase64String(dtRecentPwds.Rows(i)("sPassword"), constEncryptDecryptKey))
                End If
                da2.Dispose()
                dtval.Dispose()
            Next
            objEncryption = Nothing
            For i As Integer = 1 To PwdStr.Count
                If strpwd = PwdStr(i) Then
                    blnisexists = True
                End If
            Next
            'MsgBox(PwdStr)
            da.Dispose()
            dtRecentPwds.Dispose()
            Return blnisexists

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
            Return Nothing
        Finally
            'conn.Close()
        End Try

    End Function


    Private Sub btn_tls_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Ok.Click
        If txtChangePassword.Enabled = True AndAlso txtChangePassword.Text.Trim() <> "" Then
            If txtConfirmPass.Text.Trim() = txtChangePassword.Text.Trim() Then
                If CheckPassSetting() Then

                    _LoginUserName = gstrLoginName
                    Dim objEncryption As New clsencryption
                    Dim myTrans As SqlTransaction = Nothing
                    Dim cmd As SqlCommand = Nothing
                    Dim conn As New SqlConnection(GetConnectionString)
                    pwd = objEncryption.EncryptToBase64String(txtConfirmPass.Text.Trim(), constEncryptDecryptKey)
                    objEncryption = Nothing

                    Try
                        ' _strSQL = "Update User_MST set sPassword='" & pwd & "' from  where sLoginName = '" & _LoginUserName.Replace("'", "''") & "'"


                        ' cmd = New SqlClient.SqlCommand(_strSQL, conn)
                        conn.Open()

                        ' cmd.ExecuteNonQuery()

                        myTrans = conn.BeginTransaction

                        cmd = conn.CreateCommand
                        cmd.Transaction = myTrans

                        _strSQL = "update User_MST set sPassword = '" & pwd & "' , IsPasswordReset = 0 where sLoginName = '" & gstrLoginName.Replace("'", "''") & "'"

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = _strSQL

                        cmd.ExecuteNonQuery()


                        'MessageBox.Show("You will have to log in again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        'make the entry of the changed passord and the creation date and time in the RecentPwd_MST table
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "InsertUserNamePwd"

                        'add the parameters
                        Dim objLoginName As New SqlParameter
                        With objLoginName
                            .ParameterName = "@LoginName"
                            .Value = gstrLoginName
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.VarChar
                        End With
                        cmd.Parameters.Add(objLoginName)

                        Dim objPassword As New SqlParameter
                        With objPassword
                            .ParameterName = "@sPassword"
                            .Value = pwd
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.VarChar
                        End With
                        cmd.Parameters.Add(objPassword)

                        Dim objPwdCreationTime As New SqlParameter
                        With objPwdCreationTime
                            .ParameterName = "@PwdCreationDate"
                            .Value = Now
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.DateTime
                        End With
                        cmd.Parameters.Add(objPwdCreationTime)

                        cmd.ExecuteNonQuery()
                        cmd.Parameters.Clear()

                        myTrans.Commit()

                        conn.Close()

                        objPassword = Nothing
                        objLoginName = Nothing
                        objPwdCreationTime = Nothing

                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        MessageBox.Show("You will have to log in again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Add, "Change password Successful ", _PatientId, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    Catch ex As Exception
                        Try
                            myTrans.Rollback()
                        Catch ex1 As SqlException
                            If Not myTrans.Connection Is Nothing Then
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                MsgBox("An exception of type " & ex1.GetType().ToString() & "has occured while attempting to roll back the transaction.")
                                '" was encountered while attempting to roll back the transaction.")
                                'ErrorMessage = ex.Message
                            End If
                        End Try

                    Finally
                        conn.Close()
                        conn.Dispose()
                        conn = Nothing

                        txtOldPass.Text = ""
                        txtChangePassword.Text = ""
                        txtConfirmPass.Text = ""
                        txtConfirmPass.Enabled = False
                        txtChangePassword.Enabled = True
                        tsbtnGenPass.Enabled = False
                        If cmd IsNot Nothing Then
                            cmd.Parameters.Clear()
                            cmd.Dispose()
                            cmd = Nothing
                        End If
                        If myTrans IsNot Nothing Then
                            myTrans.Dispose()
                            myTrans = Nothing
                        End If
                    End Try
                Else
                    txtOldPass.Text = ""
                    txtChangePassword.Text = ""
                    txtConfirmPass.Text = ""
                    txtConfirmPass.Enabled = False
                    txtChangePassword.Enabled = True
                    tsbtnGenPass.Enabled = False
                End If
            Else
                MessageBox.Show("New and ConfirmPassword should be same", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        Else
            MessageBox.Show("Please Enter the Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    
    Private Sub txbtncl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txbtncl.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Change password closed", _PatientId, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    End Sub

    Private Sub tsbtnGenPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnGenPass.Click
        If txtOldPass.Text.Trim() = "" Then
            MessageBox.Show("Please Enter the Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim strgenpwd As String

        Try
            strgenpwd = GenRandomStr()
            blngenpass = True
            If UpdatePassword(strgenpwd) Then
                MessageBox.Show("The password of the user : " & gstrLoginName.Trim & " has been reset to " & strgenpwd, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = Windows.Forms.DialogResult.OK

                MessageBox.Show("You will have to log in again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Me.Close()

            End If
        Catch ex As Exception

            MessageBox.Show(ex.Message, "gloEMR ", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            tsbtnGenPass.Enabled = False
            txtConfirmPass.Text = ""
            txtChangePassword.Text = ""
            txtOldPass.Text = ""
            txtOldPass.Focus()
            txtConfirmPass.Enabled = False
            blngenpass = False
        End Try
    End Sub

    




    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientId = PatientID
    End Sub
End Class