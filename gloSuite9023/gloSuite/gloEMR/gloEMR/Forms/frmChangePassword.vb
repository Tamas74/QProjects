Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class frmChangePassword
    Inherits System.Windows.Forms.Form

    Dim _LoginUserName As String = ""

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal LoginUserName As String)
        MyBase.New()
        _LoginUserName = LoginUserName
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtChangePassword As System.Windows.Forms.TextBox
    Friend WithEvents txtConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents pnl_ToolStrip As System.Windows.Forms.Panel
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChangePassword))
        Me.txtChangePassword = New System.Windows.Forms.TextBox
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnl_ToolStrip = New System.Windows.Forms.Panel
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_Ok = New System.Windows.Forms.ToolStripButton
        Me.pnl_Base = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnl_ToolStrip.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtChangePassword
        '
        Me.txtChangePassword.BackColor = System.Drawing.Color.GhostWhite
        Me.txtChangePassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChangePassword.ForeColor = System.Drawing.Color.Black
        Me.txtChangePassword.Location = New System.Drawing.Point(154, 11)
        Me.txtChangePassword.Name = "txtChangePassword"
        Me.txtChangePassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtChangePassword.Size = New System.Drawing.Size(155, 22)
        Me.txtChangePassword.TabIndex = 0
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.BackColor = System.Drawing.Color.GhostWhite
        Me.txtConfirmPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmPassword.ForeColor = System.Drawing.Color.Black
        Me.txtConfirmPassword.Location = New System.Drawing.Point(154, 39)
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmPassword.Size = New System.Drawing.Size(155, 22)
        Me.txtConfirmPassword.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 14)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Enter new password : "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(40, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Confirm password :"
        '
        'pnl_ToolStrip
        '
        Me.pnl_ToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_ToolStrip.Controls.Add(Me.tls)
        Me.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_ToolStrip.Name = "pnl_ToolStrip"
        Me.pnl_ToolStrip.Size = New System.Drawing.Size(328, 54)
        Me.pnl_ToolStrip.TabIndex = 6
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Ok})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(0, 0)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(328, 53)
        Me.tls.TabIndex = 0
        Me.tls.Text = "toolStrip1"
        '
        'btn_tls_Ok
        '
        Me.btn_tls_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Ok.Image = CType(resources.GetObject("btn_tls_Ok.Image"), System.Drawing.Image)
        Me.btn_tls_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Ok.Name = "btn_tls_Ok"
        Me.btn_tls_Ok.Size = New System.Drawing.Size(66, 50)
        Me.btn_tls_Ok.Tag = "OK"
        Me.btn_tls_Ok.Text = "Sa&ve&&Cls"
        Me.btn_tls_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Ok.ToolTipText = "Save and Close"
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.txtChangePassword)
        Me.pnl_Base.Controls.Add(Me.Label1)
        Me.pnl_Base.Controls.Add(Me.txtConfirmPassword)
        Me.pnl_Base.Controls.Add(Me.Label2)
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 54)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Base.Size = New System.Drawing.Size(328, 72)
        Me.pnl_Base.TabIndex = 7
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 68)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(320, 1)
        Me.lbl_BottomBrd.TabIndex = 4
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 65)
        Me.lbl_LeftBrd.TabIndex = 3
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(324, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 65)
        Me.lbl_RightBrd.TabIndex = 2
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(322, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'frmChangePassword
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(328, 126)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnl_ToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(336, 160)
        Me.MinimizeBox = False
        Me.Name = "frmChangePassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Password"
        Me.pnl_ToolStrip.ResumeLayout(False)
        Me.pnl_ToolStrip.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.pnl_Base.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnChangePwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Ok.Click
        'Dim blnpwdchkflag As Boolean = False
        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim cmd1 As SqlCommand = Nothing
        Dim _strSQL As String = ""
        Dim pwd As String
        Dim oDataReader As SqlDataReader = Nothing
        Dim myTrans As SqlTransaction = Nothing

        '' To Check wheter the Current User is Adminstrator
        Dim blnIsAdministrator As Boolean = False

        Dim numdigits, numletters, numspchars, numcapletters, numminlength, numdays As Integer

        Dim objEncryption As New clsencryption

        Try
            'blnpwdchkflag = ValidatePassword(txtChangePassword.Text.Trim)

            conn.Open()
            _strSQL = "select ExpCapitalLetters,ExpNoOfLetters,ExpNoOfDigits,ExpNoOfSpecChars,ExpPwdLength,ExpTimeFrameinDays from PwdSettings"
            cmd1 = New SqlCommand(_strSQL, conn)
            oDataReader = cmd1.ExecuteReader

            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        If Not IsDBNull(oDataReader.Item("ExpCapitalLetters")) Then
                            numcapletters = oDataReader.Item("ExpCapitalLetters")
                        Else
                            numcapletters = 0
                        End If
                        If Not IsDBNull(oDataReader.Item("ExpNoOfLetters")) Then
                            numletters = oDataReader.Item("ExpNoOfLetters")
                        Else
                            numletters = 0
                        End If
                        If Not IsDBNull(oDataReader.Item("ExpNoOfDigits")) Then
                            numdigits = oDataReader.Item("ExpNoOfDigits")
                        Else
                            numdigits = 0
                        End If
                        If Not IsDBNull(oDataReader.Item("ExpNoOfSpecChars")) Then
                            numspchars = oDataReader.Item("ExpNoOfSpecChars")
                        Else
                            numspchars = 0
                        End If
                        If Not IsDBNull(oDataReader.Item("ExpPwdLength")) Then
                            numminlength = oDataReader.Item("ExpPwdLength")
                        Else
                            numminlength = 0
                        End If
                        If Not IsDBNull(oDataReader.Item("ExpTimeFrameinDays")) Then
                            numdays = oDataReader.Item("ExpTimeFrameinDays")
                        Else
                            numdays = 0
                        End If
                    End While
                End If
                oDataReader.Close()
            End If

            If txtChangePassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
                'MsgBox("Confirm Password should be same as the password")
                MessageBox.Show("Enter New Password and Confirm Password should be the same.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                txtConfirmPassword.Text = ""
                txtConfirmPassword.Focus()
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

                If cmd1 IsNot Nothing Then
                    cmd1.Parameters.Clear()
                    cmd1.Dispose()
                    cmd1 = Nothing
                End If
                Exit Sub
            End If

            _strSQL = "select nAdministrator from User_MST where sLoginName = '" & _LoginUserName.Replace("'", "''") & "'"
            cmd = New SqlClient.SqlCommand(_strSQL, conn)
            oDataReader = cmd.ExecuteReader

            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        'the value can be NULL besides 0 and 1 , so chk for null value
                        If Not IsDBNull(oDataReader.Item("nAdministrator")) Then
                            'if not nulll then set the value of flag 
                            blnIsAdministrator = oDataReader.Item("nAdministrator")
                        Else
                            'if the value is null then set the flag to false
                            blnIsAdministrator = False
                        End If
                    End While
                End If
                oDataReader.Close()
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            If blnIsAdministrator = False Then
                If ValidatePassword(txtChangePassword.Text.Trim, numminlength, numcapletters, 0, numdigits, numspchars, Nothing, numletters) Then
                    pwd = objEncryption.EncryptToBase64String(txtChangePassword.Text.Trim, constEncryptDecryptKey)

                    ' conn.Open()

                    myTrans = conn.BeginTransaction

                    cmd = conn.CreateCommand
                    cmd.Transaction = myTrans

                    _strSQL = "update User_MST set sPassword = '" & pwd & "' , IsPasswordReset = 0 where sLoginName = '" & gstrLoginName.Replace("'", "''") & "'"

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = _strSQL

                    cmd.ExecuteNonQuery()


                    MessageBox.Show("You will have to log in again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

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



                    objPwdCreationTime = Nothing
                    objPassword = Nothing
                    objLoginName = Nothing

                    'Dim frm As frmSplash
                    '''' Returns the Existance of Form if any    
                    'frm = frmSplash.GetInstance()
                    '''' 
                    'If IsNothing(frm) = True Then
                    '    Exit Sub
                    'End If
                    ''Dim frm As New frmSplash
                    'frm.ShowDialog(Me)
                    'frm.Hide()
                    Me.Close()
                    Try
                        'Application.DoEvents()
                        Me.Dispose()
                    Catch exdispose As Exception

                    End Try
                   
                Else
                    txtChangePassword.Text = ""
                    txtConfirmPassword.Text = ""
                    txtChangePassword.Focus()
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

                    If cmd1 IsNot Nothing Then
                        cmd1.Parameters.Clear()
                        cmd1.Dispose()
                        cmd1 = Nothing
                    End If
                    Exit Sub
                    'End
                End If
            Else
                pwd = objEncryption.EncryptToBase64String(txtChangePassword.Text.Trim, constEncryptDecryptKey)

                ' conn.Open()

                myTrans = conn.BeginTransaction

                cmd = conn.CreateCommand
                cmd.Transaction = myTrans

                _strSQL = "update User_MST set sPassword = '" & pwd & "' , IsPasswordReset = 0 where sLoginName = '" & gstrLoginName.Replace("'", "''") & "'"

                cmd.CommandType = CommandType.Text
                cmd.CommandText = _strSQL

                cmd.ExecuteNonQuery()

                '06-May-13 Aniket: Resolving Bug #50168:
                'MessageBox.Show("Confirm Password should be same as the password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)


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
                Me.Close()
                Try
                    'Application.DoEvents()
                    Me.Dispose()
                Catch exdispose As Exception

                End Try

                objPwdCreationTime = Nothing
                objPassword = Nothing
                objLoginName = Nothing
                'Dim frm As frmSplash
                '''' Returns the Existance of Form if any    
                'frm = frmSplash.GetInstance()
                '''' 
                'If IsNothing(frm) = True Then
                '    Exit Sub
                'End If
                ''Dim frm As New frmSplash
                'frm.ShowDialog(Me)

            End If
            'If txtChangePassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
            '    MsgBox("Confirm Password should be same as the password")
            '    txtConfirmPassword.Text = ""
            '    txtConfirmPassword.Focus()
            '    Exit Sub
            'End If

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
            If conn IsNot Nothing Then ''added for bugid 79823
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If myTrans IsNot Nothing Then

                myTrans.Dispose()
                myTrans = Nothing
            End If

            If cmd1 IsNot Nothing Then
                cmd1.Parameters.Clear()
                cmd1.Dispose()
                cmd1 = Nothing
            End If
        End Try

    End Sub

    'It validates the password and also checks whether the password he is using is not recently used say 1 month
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

        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String = ""
        'Dim oDataReader As SqlDataReader
        Dim dtRecentPwds As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Dim PwdStr As New Collection
        Dim blnisexists As Boolean = False

        Try
            _strSQL = "select sPassword, PwdCreationDate from RecentPwd_MST where LoginName ='" & gstrLoginName.Replace("'", "''") & "'"

            da = New SqlDataAdapter(_strSQL, conn)
            da.Fill(dtRecentPwds)

            'conn.Open()

            'cmd = New SqlCommand(_strSQL, conn)
            'oDataReader = cmd.ExecuteReader

            'If Not oDataReader Is Nothing Then
            '    If oDataReader.HasRows = True Then
            '        While oDataReader.Read
            '            Dim tpwdstr As String = ""
            '            If Not IsDBNull(oDataReader.Item("sPassword")) Then
            '                tpwdstr = oDataReader.Item("sPassword")
            '                PwdStr.Add(tpwdstr)
            '            End If
            '        End While
            '    End If
            '    oDataReader.Close()
            'End If

            Dim Pwddate As DateTime

            conn.Open()

            Dim objEncryption As New clsencryption

            For i As Integer = 0 To dtRecentPwds.Rows.Count - 1
                Dim noofdays As Integer = 0
                Pwddate = dtRecentPwds.Rows(i)("PwdCreationDate")
                _strSQL = "SELECT DATEDIFF(day,'" & dtRecentPwds.Rows(i)("PwdCreationDate") & "', dbo.gloGetDate()) AS no_of_days"
                cmd = New SqlCommand(_strSQL, conn)
                noofdays = cmd.ExecuteScalar

                If noofdays <= 30 Then
                    '    If noofdays <= 15 Then
                    PwdStr.Add(objEncryption.DecryptFromBase64String(dtRecentPwds.Rows(i)("sPassword"), constEncryptDecryptKey))
                End If
            Next

            For i As Integer = 1 To PwdStr.Count
                If strpwd = PwdStr(i) Then
                    blnisexists = True
                End If
            Next
            'MsgBox(PwdStr)
            objEncryption = Nothing

            Return blnisexists

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ChangePassword, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
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

            If da IsNot Nothing Then

                da.Dispose()
                da = Nothing
            End If

            If dtRecentPwds IsNot Nothing Then

                dtRecentPwds.Dispose()
                dtRecentPwds = Nothing
            End If
          
        End Try

    End Function

   
End Class
