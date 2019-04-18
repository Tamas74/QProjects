Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Mail
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frmUserMgt
    Inherits System.Windows.Forms.Form

    Public blnModify As Boolean
    Dim trvSearchNode As TreeNode
    Public blnCheckRights As Boolean = False
    Dim clSelectedUserRights As New Collection
    Dim _blnlogochanged As Boolean
    Dim blnCancelFlag As Boolean = False

    Dim blnPwdFlag As Boolean = False
    Dim blnResetPwdFlag As Boolean = False

    Dim blnIsAdministrator As Boolean
    Dim blnCoSignFlag As Boolean = False
    Dim mailTo, mailFrom, Subject, Body, SMTPServer, CCTo As String
    Dim blnMailSentFlag As Boolean = False
    Dim bIsSecurity As Boolean = False
    Dim _encryptionKey As String = "12345678"
    Dim tooltip As New ToolTip
 

    Dim currpwd As String
    'sarika 15th feb
    Dim blnflagOK As Boolean
    Dim combo As ComboBox
    Public oAddressContol As gloAddress.gloAddressControl

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ''dhruv adding the control
        oAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oAddressContol.Dock = DockStyle.Fill
        pnlAddresssControl.Controls.Add(oAddressContol)

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Enum UserSettings
        None = 0
        HL7 = 1
    End Enum

    Public Property blnlogochanged() As Boolean
        Get
            Return _blnlogochanged
        End Get
        Set(ByVal Value As Boolean)
            _blnlogochanged = Value
        End Set
    End Property

    Public nBlockStatus As Boolean = False
    Public Shared Imagepath As String
    Public blnClose As Boolean = False
    Public blnFinish As Boolean = False
    Private Sub frmUserMgtNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '' Mahesh  20071019 
        '' User Setting Tab Make InVisible as the Functionality is not to add for now So Temp Make Invisible
        ''[for Future Enhancment]
        ''''tbcsettings.Visible = False
        '' 
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String
        Dim oDataReader As SqlDataReader
        Dim adminflag As Boolean = False
        Dim blnAuditTrailFlag As Boolean = False
        Dim strSelectQryClose As String = ""
        Dim strSelectQryFinish As String = ""
        Dim i As Integer = 0

        With dtpValidupto
            .Format = DateTimePickerFormat.Custom
            .CustomFormat = DTFORMAT
        End With
        Try
            '28-Mar-14 Chetan: Resolving resolution issues for bugid 65037
            Dim myScreenWidth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth
            Dim myScreenHeight As Integer = System.Windows.SystemParameters.PrimaryScreenHeight
            If Me.Width > myScreenWidth Or Me.Height > myScreenHeight Then
                Me.MaximumSize = New System.Drawing.Size(myScreenWidth, (myScreenHeight - 50))
                Me.AutoScroll = True
                tbcUser.AutoScroll = True
            End If

            btnGenPwd.Visible = False
            chksaveclose.Checked = False
            chksavefinish.Checked = False
            _strSQL = "select nAdministrator from User_MST where sLoginName = '" & gstrLoginName.Replace("'", "''") & "'"

            conn.Open()

            ' To get the Administrator flag
            cmd = New SqlCommand(_strSQL, conn)

            adminflag = cmd.ExecuteScalar

            If adminflag = True Then
                btnGenPwd.Visible = True
            End If

            blnlogochanged = False
            blnlogochanged = True
            Me.Cursor = Cursors.WaitCursor
            'tbUser.Focus()
            '   tbUser.Select()
            '  tbOther.Visible = False
            With dtDOB
                .Format = DateTimePickerFormat.Custom
                .CustomFormat = DTFORMAT
                If blnModify = False Then
                    .Value = System.DateTime.Now
                End If
            End With

            'tbPageOther = tbUser
            'tbcUser.TabPages.Remove(tbPageOther)


            '-------
            picSignature.Visible = True
            'If AxSigPlus1.TabletConnectQuery() Then
            '    AxSigPlus1.DisplayPenWidth = 11
            '    AxSigPlus1.ImageXSize = 1000 'sets image width in pixels
            '    AxSigPlus1.ImageYSize = 350
            '    AxSigPlus1.JustifyMode = 5
            '    AxSigPlus1.ClearTablet()
            '    AxSigPlus1.Refresh()
            '    AxSigPlus1.TabletState = 1
            'End If

            Dim dtLocationStatus As DataTable
            Dim cmdMC As SqlCommand
            Dim connMC As SqlConnection
            Dim da As SqlDataAdapter
            Dim _strSelecteSQL As String = ""

            Try
                dtLocationStatus = New DataTable
                _strSelecteSQL = "SELECT  sMachineName FROM ClientSettings_MST "
                connMC = New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString())
                cmdMC = New SqlCommand(_strSelecteSQL, connMC)
                da = New SqlDataAdapter(cmdMC)
                da.Fill(dtLocationStatus)
                trvMachinName.Nodes.Add("Machin Name")
                trvMachinName.Nodes.Clear()
                For j As Integer = 0 To dtLocationStatus.Rows.Count - 1


                    Dim objTreeNode As New TreeNode
                    objTreeNode.Text = dtLocationStatus.Rows(j)("sMachineName")
                    trvMachinName.Nodes.Add(objTreeNode)
                Next
                'SetGridStyle(dtLocationStatus)

            Catch ex As Exception
                MessageBox.Show("Error while retrieving location Status." & ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try



            If blnModify = True Then
                currpwd = txtPassword.Text.Trim

                '   to get the AuditTrail flag
                _strSQL = "select IsAuditTrail from User_MST where sLoginName = '" & txtUserName.Text.Replace("'", "''") & "'"
                cmd = New SqlCommand(_strSQL, conn)
                oDataReader = cmd.ExecuteReader

                If Not oDataReader Is Nothing Then
                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            'the value can be NULL besides 0 and 1 , so chk for null value
                            If Not IsDBNull(oDataReader.Item("IsAuditTrail")) Then
                                'if not nulll then set the value of flag 
                                blnAuditTrailFlag = oDataReader.Item("IsAuditTrail")
                            Else
                                'if the value is null then set the flag to false
                                blnAuditTrailFlag = False
                            End If
                        End While
                    End If
                    oDataReader.Close()
                End If


                'if the audit trail flag is true then set the check the checkbox else uncheck it
                If blnAuditTrailFlag = True Then
                    chkAuditTrails.Checked = True
                Else
                    chkAuditTrails.Checked = False
                End If


                ' '' Code is for the UserSetting Temp Commeneted By Mahesh 20071019
                'strSelectQryClose = " SELECT Value,MachinName  From UserSettings WHERE nUserID = '" & GetUserId(txtUserName.Text) & "' AND SettingName = 'SendOnSaveandClose' "
                'cmd = New SqlClient.SqlCommand(strSelectQryClose, conn)
                'Dim dt As New DataTable
                'Dim sda As New SqlClient.SqlDataAdapter(strSelectQryClose, conn)
                'sda.Fill(dt)
                'If IsNothing(dt) = False Then

                '    If dt.Rows.Count <> 0 Then '   Not (IsDBNull(dt)) = False Then
                '        For k As Integer = 0 To trvMachinName.Nodes.Count - 1
                '            For j As Integer = 0 To dt.Rows.Count - 1
                '                If dt.Rows(j)("MachinName") = trvMachinName.Nodes(k).Text Then
                '                    trvMachinName.Nodes(k).Checked = True
                '                End If
                '            Next
                '        Next


                '        If dt.Rows(0)("Value") = "True" Then
                '            blnClose = True
                '            chksaveclose.Checked = True
                '        Else
                '            blnClose = False
                '            chksaveclose.Checked = False
                '        End If
                '    End If

                'End If

                'dt.Clear()
                'strSelectQryFinish = "SELECT Value,MachinName From UserSettings WHERE nUserID = '" & GetUserId(txtUserName.Text) & "' AND SettingName = 'SendOnSaveandFinish' "
                'cmd = New SqlClient.SqlCommand(strSelectQryFinish, conn)
                'dt = New DataTable
                'sda = New SqlClient.SqlDataAdapter(strSelectQryFinish, conn)
                'sda.Fill(dt)
                'If IsNothing(dt) = False Then
                '    If dt.Rows.Count <> 0 Then   ' IsNothing(dt) = True Then

                '        For k As Integer = 0 To trvMachinName.Nodes.Count - 1
                '            For j As Integer = 0 To dt.Rows.Count - 1
                '                If dt.Rows(j)("MachinName") = trvMachinName.Nodes(k).Text Then
                '                    trvMachinName.Nodes(k).Checked = True
                '                End If
                '            Next
                '        Next
                '        If dt.Rows(0)("Value") = "True" Then
                '            blnFinish = True
                '            chksavefinish.Checked = True
                '        Else
                '            blnFinish = False
                '            chksavefinish.Checked = False
                '        End If
                '    End If
                'End If
                '' Above Code Temp Commented By Mahesh 20071019
            End If

            ''To Enable the Co-Signature right to user other than providers
            GetCoSign(txtUserName.Text)

            GetIsSecurityUser(txtUserName.Text)
            'TabCtrl1.Tabs(0). 

            'TabCtrl1.Tabs(0).AttachedControl.Select()


            '   txtUserName.Select()
            TabCtrl1.SelectedTab = tbcUser

            DisableAndEnableEmergencyAccess()
            Dim objSettings As New clsSettings
            Dim OCPPortalEnable As New Object
            objSettings.GetSetting("OCPPORTALENABLE", 0, gnClinicID, OCPPortalEnable)
            objSettings = Nothing
            ''Check if setting not present in DB the remove tabpage.
            If Not IsNothing(OCPPortalEnable) Then
                If (Convert.ToString(OCPPortalEnable.ToString().Trim) = "" Or Convert.ToString(OCPPortalEnable) = "0") Then
                    TabCtrl1.TabPages.Remove(tbcProviderPortalInfo)
                End If
            End If
            
            Me.Cursor = Cursors.Default

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            conn.Close()
            cmd.Dispose()

        End Try
    End Sub

    

    Private Sub btnDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If tbcUser.TabPages.Count = 1 Then
            '    tbcUser.TabPages.Add(tbPageOther)
            'End If
            'tbcUser.SelectedTab = tbPageOther
            TabCtrl1.SelectedTab = tbcOther

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        Try
            blnCancelFlag = True
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch objErr As Exception
            Me.DialogResult = DialogResult.None
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Public Sub Fill_Gender()
        With cmbGender
            .Items.Clear()
            .Items.Add("Male")
            .Items.Add("Female")
            .Items.Add("Other")
            .SelectedIndex = 0
        End With
    End Sub

    Public Sub Fill_MaritalStatus()
        With cmbMaritalStatus
            '******By Sandip Deshmukh 18 Oct 07 3.05PM Bug# 345
            '******Application should show 'Married,UnMarried,Single,Widowed,Divorced' options in
            '******Marital Status' dropdown list.
            .Items.Clear()
            .Items.Add("Married")
            .Items.Add("UnMarried")
            .Items.Add("Single")
            .Items.Add("Widowed")
            .Items.Add("Divorced")
            '.SelectedIndex = 0
            '******18 Oct 07 3.05PM Bug# 345
        End With

    End Sub

    Public Sub Fill_gloEMRGroups()
        With lstGroups
            .Items.Clear()
            Dim clUserGroups As New Collection
            Dim objUserGroups As New clsUserGroups
            clUserGroups = objUserGroups.PopulateUserGroups
            objUserGroups = Nothing
            Dim trvGroups As TreeNode = Nothing
            Dim nCount As Integer
            For nCount = 1 To clUserGroups.Count
                lstGroups.Items.Add(clUserGroups.Item(nCount))
            Next
        End With
    End Sub

    Private Sub lstGroups_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstGroups.Click
        Try


            If (lstGroups.SelectedIndex = -1) Then
                lstGroups.SelectedItem = Nothing
                Exit Sub
            End If
            '******By Sandip Deshmukh 17 Oct 07 Bug#340
            '******for user should not allow to associate multiple user groups
            Dim nCount As Integer
            For nCount = 0 To lstGroups.Items.Count - 1
                lstGroups.SetItemChecked(nCount, False)
            Next

            For nCount = 0 To trvUserRights.GetNodeCount(False) - 1
                trvUserRights.Nodes(nCount).Checked = False
            Next
            '******17 Oct 07 Bug#340
            If blnCheckRights = True Then
                Me.Cursor = Cursors.WaitCursor

                If lstGroups.GetItemCheckState(lstGroups.SelectedIndex) = CheckState.Unchecked Then
                    Dim dtRights As New DataTable
                    Dim objUserRights As New clsUserGroups
                    'dtRights = objUserRights.PopulateUserGroupsRights(Trim(lstGroups.Items(lstGroups.SelectedIndex)))
                    ''Sandip Darade 20090818 
                    ''get rightId in place of right name 
                    dtRights = GetUserGroupsRights(Trim(lstGroups.Items(lstGroups.SelectedIndex)))
                    For nCount = 0 To dtRights.Rows.Count - 1
                        If IsDBNull(dtRights.Rows(nCount).Item(0)) = False AndAlso Trim(dtRights.Rows(nCount).Item(0)) <> "" Then
                            SearchNode(trvUserRights, dtRights.Rows(nCount).Item(0))
                            If IsNothing(trvSearchNode) = False Then
                                trvUserRights.SelectedNode = trvSearchNode
                                trvUserRights.SelectedNode.Checked = True
                            End If
                        End If
                    Next
                End If

                Me.Cursor = Cursors.Default
            End If

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub lstGroups_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lstGroups.ItemCheck

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnSave.Click
        blnflagOK = True
        'Dim blnpwdchkflag As Boolean = False
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim cmd1 As SqlCommand
        Dim _strSQL As String = ""
        Dim pwd As String = ""
        Dim oDataReader As SqlDataReader
        Dim strInsertQryClose As String = ""
        Dim strInsertQryFinish As String = ""
        Dim strUpdateQryClose As String = ""
        Dim strUpdateQryFinish As String = ""
        'Dim myTrans As SqlTransaction

        Dim numdigits, numletters, numspchars, numcapletters, numminlength, numdays As Integer

        Dim objEncryption As New clsEncryption

        Try
            If Trim(txtUserName.Text) = "" Then
                MessageBox.Show("User Name must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '******By Sandip Deshmukh 26 th Oct 2007 
                '******to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcUser
                '******26 th Oct 2007 
                txtUserName.Focus()
                Exit Sub
            End If

            If Trim(txtPassword.Text) = "" Then
                MessageBox.Show("Password must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '******By Sandip Deshmukh 26 th Oct 2007 
                '******to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcUser
                '******26 th Oct 2007 
                txtPassword.Focus()
                Exit Sub
            End If


            '********************************************************* Added By mahendra for Emergency Access 
            If ChkEmergencyAccess.Checked = True Then
                If txtEAPassword.Text.Trim() = "" Then
                    MessageBox.Show("Please enter Emergency Access Password. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcUser
                    txtEAPassword.Focus()
                    Exit Sub
                End If

                If txtEAPassword.Text.Trim() <> txtEAConfirmPassword.Text.Trim() Then
                    MessageBox.Show("Emergency Access Passwords do not match. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcUser
                    txtEAPassword.Focus()
                    Exit Sub
                End If


                If dtpValidupto.Value < Now.Date Then
                    MessageBox.Show("Emergency Access Password Validity Date should be Greater than or Equal to Today", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcUser
                    dtpValidupto.Focus()
                    Exit Sub
                End If
            End If


            '*********************************************************
            If chkExchnageUser.Checked = True Then
                If txtExchangeLogin.Text.Trim() = "" Then
                    MessageBox.Show("Please enter exchange login name. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcUser
                    txtExchangeLogin.Focus()
                    Exit Sub
                End If

                If txtExchangePwd.Text.Trim() = "" Then
                    MessageBox.Show("Please enter exchange password. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcUser
                    txtExchangePwd.Focus()
                    Exit Sub
                End If

                If txtExchangePwd.Text.Trim() <> txtExchangePwdConfirm.Text.Trim() Then
                    MessageBox.Show("Exchange passwords do not match. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcUser
                    txtExchangePwd.Focus()
                    Exit Sub
                End If
            End If


            conn.Open()
            _strSQL = "select * from PwdSettings"
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

            _strSQL = "select nAdministrator from User_MST where sLoginName = '" & txtUserName.Text.Trim.Replace("'", "''") & "'"
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

            If chkgloEMRAdmin.Checked = False Then
                blnIsAdministrator = False
                'If blnIsAdministrator = False Then

                'validate the password if he is not an administrator
                Dim blnPwdReset As Boolean = False
                blnPwdReset = GetResetPwdFlag(txtUserName.Text.Trim)

                If blnPwdReset = True Or blnResetPwdFlag = True Then
                    'dodnt validate the password for generate pwd.
                Else
                    blnPwdFlag = ValidatePassword(txtPassword.Text.Trim, numminlength, numcapletters, 0, numdigits, numspchars, Nothing, numletters, numdays)
                    If blnPwdFlag = False Then
                        txtPassword.Text = ""
                        '******By Sandip Deshmukh 26 th Oct 2007 
                        '******to select the tabitem on the form if cause validation
                        TabCtrl1.SelectedTab = tbcUser
                        '******26 th Oct 2007 
                        txtPassword.Focus()
                        Exit Sub
                    End If
                End If
            Else
                blnIsAdministrator = True
                'End If
            End If
            Dim blnNodeIscheck As Boolean = False
            If chksaveclose.Checked = True Or chksavefinish.Checked = True Then
                For j As Integer = 0 To trvMachinName.Nodes.Count - 1
                    If trvMachinName.Nodes(j).Checked = True Then
                        blnNodeIscheck = True
                    End If
                Next
                If blnNodeIscheck = False Then
                    MessageBox.Show("Please Select the Machine Name", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If
            '*********************************************************
            'Dim objEncryption As New clsEncryption

            If Trim(txtConfirmPassword.Text) = "" Then
                MessageBox.Show("Confirm Password must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '******By Sandip Deshmukh 26 th Oct 2007 
                '******to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcUser
                '******26 th Oct 2007 
                txtConfirmPassword.Focus()
                Exit Sub
            End If
            If Trim(txtPassword.Text) <> Trim(txtConfirmPassword.Text) Then
                MessageBox.Show("Password and Confirm Password must be same", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '******By Sandip Deshmukh 26 th Oct 2007 
                '******to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcUser
                txtPassword.Text = ""
                txtConfirmPassword.Text = ""
                '******26 th Oct 2007 
                txtPassword.Focus()
                Exit Sub
            End If
            If chkAllowPortalAccess.Checked = True Then
                If txtOCPLoginName.Text.Trim() = "" Then
                    MessageBox.Show("Please enter OCP Login Name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcProviderPortalInfo
                    txtOCPLoginName.Focus()
                    Exit Sub
                End If

                If txtOCPLoginPassword.Text.Trim() = "" Then
                    MessageBox.Show("Please enter OCP Login Password.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcProviderPortalInfo
                    txtOCPLoginPassword.Focus()
                    Exit Sub
                End If

                If txtOCPConfirmPassword.Text.Trim() = "" Then
                    MessageBox.Show("Please enter OCP Login Confirm Password.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcProviderPortalInfo
                    txtOCPConfirmPassword.Focus()
                    Exit Sub
                End If

                If Trim(txtOCPLoginPassword.Text) <> Trim(txtOCPConfirmPassword.Text) Then
                    MessageBox.Show("OCP Login Password and OCP Login Confirm Password must be same.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '******By Sandip Deshmukh 26 th Oct 2007 
                    '******to select the tabitem on the form if cause validation
                    TabCtrl1.SelectedTab = tbcProviderPortalInfo
                    txtOCPLoginPassword.Text = ""
                    txtOCPConfirmPassword.Text = ""
                    '******26 th Oct 2007 
                    txtOCPLoginPassword.Focus()
                    Exit Sub
                End If
                

            End If
            Dim oUser As New clsUsers
            If oUser.OCPCheckUserExists(txtOCPLoginName.Text.Trim(), If(txtUserName.Tag = 0, 0, txtUserName.Tag)) = True Then
                MessageBox.Show("Login Name is already in use.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                TabCtrl1.SelectedTab = tbcProviderPortalInfo
                Exit Sub
            End If
            oUser = Nothing


            '-----------------------------------------------------------
            'sarika 25th apr 2007
            'If Trim(txtNickName.Text) = "" Then
            '    MessageBox.Show("Nick Name must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    txtNickName.Focus()
            '    Exit Sub
            '    '***************************Sarika - To avoid duplicate nicknames-------
            'Else
            '    'validate the nickname .
            '    'check whether it already exists .
            '    'encrypt the nickname
            '    Dim strnickname As String

            '    strnickname = objEncryption.EncryptToBase64String(txtNickName.Text, constEncryptDecryptKey)
            '    If IsNickNameExists(strnickname, txtUserName.Tag) = True Then
            '        MessageBox.Show("Nick Name already exists . Please enter another Nick Name .", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        txtNickName.Text = ""
            '        txtNickName.Focus()
            '        Exit Sub
            '    End If
            '    '***************************Sarika - To avoid duplicate nicknames-------
            'End If

            '-----------------------------------------------------------

            GetCheckedNodes()
            If clSelectedUserRights.Count <= 0 Then
                MessageBox.Show("Minimum one User Rights must be selected", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '******By Sandip Deshmukh 26 th Oct 2007 
                '******to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcUser
                '******26 th Oct 2007 
                trvUserRights.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim objUser As New clsUsers
            'Check User already exists or not
            If blnModify = True Then
                If objUser.CheckUserExists(txtUserName.Text, txtUserName.Tag) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("User already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    objUser = Nothing
                    '******By Sandip Deshmukh 26 th Oct 2007 
                    '******to select the tabitem on the form if cause validation
                    TabCtrl1.SelectedTab = tbcUser
                    '******26 th Oct 2007 
                    txtUserName.Focus()
                    Exit Sub
                End If
            Else
                If objUser.CheckUserExists(txtUserName.Text) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("User already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '******By Sandip Deshmukh 26 th Oct 2007 
                    '******to select the tabitem on the form if cause validation
                    TabCtrl1.SelectedTab = tbcUser
                    '******26 th Oct 2007 
                    objUser = Nothing
                    txtUserName.Focus()
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.WaitCursor
            If blnModify = True Then
                If objUser.CheckUserExists(txtUserName.Text, txtUserName.Tag, txtWindowsLoginName.Text) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Windows Login Name already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    objUser = Nothing
                    TabCtrl1.SelectedTab = tbcUser
                    txtWindowsLoginName.Focus()
                    Exit Sub
                End If
            Else
                If objUser.CheckUserExists(txtUserName.Text, , txtWindowsLoginName.Text) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Windows Login Name already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcUser
                    objUser = Nothing
                    txtWindowsLoginName.Focus()
                    Exit Sub
                End If
            End If


            '******By Sandip Deshmukh 15 Oct 2007 11.15 a.m. Bug# 341
            '******For bug reported the control for Phone No. is 
            '******modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
            Me.Cursor = Cursors.Default
            If Len(Trim(txtPhoneNo.Text)) > 0 And Len(Trim(txtPhoneNo.Text)) < 10 Then
                MessageBox.Show("Phone Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '******By Sandip Deshmukh 26 th Oct 2007 
                '******to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcOther
                '******26 th Oct 2007 
                txtPhoneNo.Focus()
                Exit Sub
            End If

            '******15 Oct 2007 11.15 a.m. Bug# 341



            '******By Sandip Deshmukh 15 Oct 2007 12.52 p.m. Bug# 344
            '******For bug reported the control for SSN No. is 
            '******modified from Textbox to MaskedTextbox(9 digit numeric )and following code is added
            Me.Cursor = Cursors.Default
            If Len(Trim(txtSSN.Text)) > 0 And Len(Trim(txtSSN.Text)) < 9 Then
                MessageBox.Show("SSN No. Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '******By Sandip Deshmukh 26 th Oct 2007 
                '******to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcOther
                '******26 th Oct 2007 
                txtSSN.Focus()
                Exit Sub
            End If

            '******15 Oct 2007 12.52 p.m. Bug# 344



            '******By Sandip Deshmukh 15 Oct 2007 12.58 p.m. Bug# 343
            '******For bug reported the control for Zip No. is 
            '******modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
            'Me.Cursor = Cursors.Default
            'If Len(Trim(txtZip.Text)) > 0 And Len(Trim(txtZip.Text)) < 9 Then
            '    MessageBox.Show("Zip Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Information)
            '    txtZip.Focus()
            '    Exit Sub
            'End If

            '******15 Oct 2007 12.58 p.m. Bug# 343



            '******By Sandip Deshmukh 15 Oct 2007 11.15 a.m. Bug# 342
            '******For bug reported the control for Mobile No. is 
            '******modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
            Me.Cursor = Cursors.Default
            If Len(Trim(txtMobileNo.Text)) > 0 And Len(Trim(txtMobileNo.Text)) < 10 Then
                MessageBox.Show("Mobile No. Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '******By Sandip Deshmukh 26 th Oct 2007 
                '******to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcOther
                '******26 th Oct 2007 
                txtMobileNo.Focus()
                Exit Sub
            End If

            ' Added by Rahul Patel on 08-09-2010   
            ' For email id validation 
            Me.Cursor = Cursors.Default
            If (txtEmailAddress.Text.Trim() <> "") Then
                If Regex.IsMatch(txtEmailAddress.Text.Trim(), "\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") = False Then
                    MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtEmailAddress.Focus()
                    Exit Sub
                End If
            End If
            ' End Of email id Validation.

            '******15 Oct 2007 11.15 a.m. Bug# 342
            '**************************for pwd chk**********************************************************************


            '**************************for pwd chk***********************************************************************
            objUser.UserName = txtUserName.Text

            objUser.Password = objEncryption.EncryptToBase64String(txtPassword.Text, constEncryptDecryptKey)
            objUser.NickName = objEncryption.EncryptToBase64String(txtNickName.Text, constEncryptDecryptKey)
            objUser.WindowLoaginName = txtWindowsLoginName.Text
            ' added by mahendra For Emergency Access 


            objUser.EAPChart = ChkEmergencyAccess.Checked
            If objUser.EAPChart = True Then
                objUser.EAPassword = objEncryption.EncryptToBase64String(txtEAPassword.Text, constEncryptDecryptKey)

                objUser.ValidDt = dtpValidupto.Value
            Else
                objUser.EAPassword = ""
                objUser.ValidDt = dtpValidupto.MinDate
            End If

            objUser.OCPLoginName = txtOCPLoginName.Text
            objUser.OCPLoginPassword = objEncryption.EncryptToBase64String(txtOCPLoginPassword.Text, constEncryptDecryptKey)

            objEncryption = Nothing
            objUser.FirstName = txtFirstName.Text
            objUser.MiddleName = txtMiddleName.Text
            objUser.LastName = txtLastName.Text
            objUser.SSNNo = txtSSN.Text
            objUser.DOB = dtDOB.Value
            objUser.Gender = cmbGender.Text
            objUser.MaritalStatus = cmbMaritalStatus.Text
            'Commented  by Rahul Patel on 08-09-2010 for removing the street text box  
            'objUser.Street = txtStreet.Text

            'objUser.Address = txtAddress.Text.Trim
            'objUser.Address2 = txtAddress2.Text.Trim '' SUDHIR 20090703 ''
            'objUser.City = txtCity.Text
            'objUser.State = txtState.Text
            'objUser.ZIP = txtZip.Text
            ''Dhruv New Assignment
            objUser.Address = oAddressContol.txtAddress1.Text.Trim          ''add1
            objUser.Address2 = oAddressContol.txtAddress2.Text.Trim         ''add2
            objUser.City = oAddressContol.txtCity.Text                      ''City
            objUser.State = oAddressContol.cmbState.Text                    ''State
            objUser.ZIP = oAddressContol.txtZip.Text                        ''ZIP
            objUser.County = oAddressContol.txtCounty.Text                 ''county
            objUser.Country = oAddressContol.cmbCountry.Text               ''Country
            ''Assigment End

            objUser.PhoneNo = txtPhoneNo.Text
            objUser.MobileNo = txtMobileNo.Text
            objUser.FAX = txtFax.Text
            objUser.Email = txtEmailAddress.Text
            objUser.BlockStatus = nBlockStatus
            'objUser.Signature = picSignature.Image()
            Dim oEncrypt As New clsEncryption()
            ''Sandip Darade 20090722
            ''Add exchange user
            objUser.IsExchangeUser = chkExchnageUser.Checked
            If objUser.IsExchangeUser = True Then
                Dim _UserName As String = txtExchangeLogin.Text.Trim()
                If _UserName.Contains("@") = True Then
                    _UserName = _UserName.Substring(0, _UserName.IndexOf("@"))
                End If
                objUser.ExchangeLogin = _UserName.Trim()
                objUser.ExchangePassward = oEncrypt.EncryptToBase64String(txtExchangePwd.Text.Trim(), _encryptionKey)
            End If
            '''''''''''''

            If blnlogochanged = True Then
                If optBrowse.Checked = False Then
                    picSignature.Image = Nothing
                    If File.Exists(Application.StartupPath & "\UserSign1.tif") = True Then
                        picSignature.Image = Image.FromFile(Application.StartupPath & "\UserSign1.tif")
                        picSignature.SizeMode = PictureBoxSizeMode.CenterImage
                    End If
                End If

                If IsNothing(picSignature.Image) = False Then
                    '****************************
                    Dim img As Image
                    Dim nWidth As Int16
                    Dim nHeight As Int16
                    img = picSignature.Image
                    nHeight = img.Height
                    nWidth = img.Width
                    'If nWidth > 150 Then
                    '    nWidth = 150
                    'End If
                    'If nHeight > 75 Then
                    '    nHeight = 75
                    'End If
                    img = New Bitmap(img, New Size(nWidth, nHeight))
                    picSignature.Image = img
                    picSignature.SizeMode = PictureBoxSizeMode.CenterImage
                    '****************************
                    objUser.Signature = picSignature.Image()
                Else
                    objUser.Signature = Nothing
                End If
            Else
                '****************************
                If IsNothing(picSignature.Image) = False Then
                    '****************************
                    Dim img As Image
                    Dim nWidth As Int16
                    Dim nHeight As Int16
                    img = picSignature.Image
                    nHeight = img.Height
                    nWidth = img.Width
                    'If nWidth > 150 Then
                    '    nWidth = 150
                    'End If
                    'If nHeight > 75 Then
                    '    nHeight = 75
                    'End If
                    img = New Bitmap(img, New Size(nWidth, nHeight))
                    picSignature.Image = img
                    picSignature.SizeMode = PictureBoxSizeMode.CenterImage
                    '****************************
                    objUser.Signature = picSignature.Image()
                Else
                    objUser.Signature = Nothing
                End If
            End If

            'objUser.IsPasswordReset = False

            If blnResetPwdFlag = True Then
                objUser.IsPasswordReset = True
            Else
                objUser.IsPasswordReset = GetResetPwdFlag(txtUserName.Text.Trim)
            End If

            If chkAuditTrails.Checked = True Then
                objUser.IsAuditTrail = True
            Else
                objUser.IsAuditTrail = False
            End If

            If chkAccessDenied.Checked = True Then
                objUser.AccessDenied = True
            Else
                objUser.AccessDenied = False
            End If

            If chckCoSign.Checked = True Then
                objUser.IsCoSign = True
            Else
                objUser.IsCoSign = False
            End If

            If chkIssecurityUser.Checked = True Then
                objUser.IsSecurityUser = True
            Else
                objUser.IsSecurityUser = False
            End If

            ' objUser.Signature = picSignature.Image()
            '  blnlogochanged = False

            'If blnlogochanged = True Then
            '    If optBrowse.Checked = False Then
            '        picSignature.Image = Nothing
            '        If File.Exists(Application.StartupPath & "\DoctorSign.tif") = True Then
            '            picSignature.Image = Image.FromFile(Application.StartupPath & "\DoctorSign.tif")
            '            picSignature.SizeMode = PictureBoxSizeMode.CenterImage
            '        End If
            '    End If

            '    If IsNothing(picSignature.Image) = False Then
            '        .Signature = picSignature.Image
            '    Else
            '        .Signature = Nothing
            '    End If
            'End If

            objUser.gloEMRAdministrator = chkgloEMRAdmin.Checked
            'Retrieve Provider ID
            If Trim(cmbProvider.Text) = "Not Provider" Then
                objUser.ProviderID = 0
            Else
                'Retrieve Provider ID
                Dim objProvider As New clsProvider
                objUser.ProviderID = objProvider.RetrieveProviderID(Trim(cmbProvider.Text))
                objProvider = Nothing
            End If
            'If nBlockStatus = 1 Then
            '    objUser.BlockStatus = False
            'Else
            '    objUser.BlockStatus = True
            'End If


            'sarika 20090518
            'If nBlockStatus = True Then
            '    objUser.BlockStatus = True
            'Else
            '    objUser.BlockStatus = False
            'End If
            objUser.BlockStatus = nBlockStatus
            '--
            'Add user Rights
            Dim nCount As Integer
            Dim clUserGroups As New Collection
            objUser.UserRights = clSelectedUserRights

            ' Added by Shweta 03302012 -Business Center- User Association 
            'Retrieve Business Center Id 
            objUser.BusinessCenterID = cmbBusinessCenter.SelectedValue

            'Add user groupsd
            For nCount = 0 To lstGroups.CheckedItems.Count - 1
                clUserGroups.Add(lstGroups.CheckedItems(nCount))
            Next
            objUser.UserGroups = clUserGroups

            If rb_UseCurrentCredential.Checked = True Then
                objUser.IsSameAsUserDetails = True
            Else
                objUser.IsSameAsUserDetails = False
            End If

            If chkAllowPortalAccess.Checked = True Then
                objUser.IsAllowPortalAccess = True
            Else
                objUser.IsAllowPortalAccess = False
            End If

            If objUser.ProviderID = 0 Then
                objUser.OCPLoginType = "User"
            Else
                objUser.OCPLoginType = "Provider"
            End If

            Dim objAudit As New clsAudit
            If blnModify = False Then
                If objUser.AddUser() = True Then
                    objUser = Nothing
                    Me.DialogResult = DialogResult.OK
                    Me.Cursor = Cursors.Default
                    Me.Close()
                    'sarika 20th feb
                    'audit trail
                    'Dim userid As Long
                    'userid = GetUserId(gstrLoginName)

                    objAudit.CreateLog(clsAudit.enmActivityType.Add, gstrLoginName & " has added new user " & txtUserName.Text, gstrLoginName, gstrClientMachineName)
                    objAudit = Nothing
                    Exit Sub
                Else
                    Me.Cursor = Cursors.Default
                    objAudit.CreateLog(clsAudit.enmActivityType.Other, "Unable to add user " & txtUserName.Text.Trim, gstrLoginName, gstrClientMachineName, , , clsAudit.enmOutcome.Failure)
                    MessageBox.Show("Unable to add user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                If objUser.EditUser(txtUserName.Tag) = True Then
                    '''''''''''''remove the mail facility
                    'If blnResetPwdFlag = True Then
                    '    '***********************************Call the function to send mail ********************

                    '    mailTo = objUser.Email
                    '    mailFrom = "support@glostream.com"
                    '    'GetAdminEmail()
                    '    Subject = "Change Of Password"

                    '    Body = "Your Password has been reset to : " & txtPassword.Text
                    '    SMTPServer = ""
                    '    '"mail.gloemr.com" '""

                    '    ' blnMailSentFlag =
                    '    'SendMailOneAttachment(mailFrom, mailTo, Subject, Body, CCTo, SMTPServer)
                    '    gloSendMail(mailFrom, mailTo, Subject, Body, CCTo, SMTPServer)


                    '    ''***********************************Call the function to send mail ********************

                    'End If
                    objUser = Nothing

                    '''''Pramod 13/09/2007 HL7 Close and finish settings
                    '''''Check User is present or not in UserSetting Table
                    'For j As Integer = 0 To trvMachinName.Nodes.Count - 1
                    '    Dim strselectqry As String = "select count(*) from usersettings where nuserid = '" & GetUserId(txtUserName.Text) & "' AND  MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '    cmd = New SqlClient.SqlCommand(strselectqry, conn)
                    '    Dim i As Integer = cmd.ExecuteScalar()
                    '    If trvMachinName.Nodes(j).Checked = True Or i = 2 Then
                    '        ''''If close checkbox and finish checkbox is unchecked
                    '        If chksaveclose.Checked = False And chksavefinish.Checked = False Then
                    '            If i = 0 Then
                    '                ''''If user is not setting Value then insert values
                    '                strInsertQryClose = "insert into usersettings(nuserid, settingname, value,MachinName, Type) values('" & GetUserId(txtUserName.Text) & "','SendOnSaveandClose','False','" & trvMachinName.Nodes(j).Text & "','" & UserSettings.HL7 & "')"
                    '                strInsertQryFinish = "insert into usersettings(nuserid, settingname, value,MachinName, Type) values('" & GetUserId(txtUserName.Text) & "','SendOnSaveandFinish ','False','" & trvMachinName.Nodes(j).Text & "','" & UserSettings.HL7 & "')"
                    '            Else
                    '                ''''If User Is have setting value then update the value
                    '                strUpdateQryClose = "update usersettings set Value = 'False' where nuserid = '" & GetUserId(txtUserName.Text) & "' And settingname = 'SendOnSaveandClose'  AND MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                strUpdateQryFinish = "update usersettings set Value = 'False' where nuserid = '" & GetUserId(txtUserName.Text) & "' And settingname = 'SendOnSaveandFinish' AND MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                If trvMachinName.Nodes(j).Checked = False Then
                    '                    Dim strDeleteClose As String = "Delete From usersettings Where nuserid = '" & GetUserId(txtUserName.Text) & "' AND  MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                    cmd = New SqlClient.SqlCommand(strDeleteClose, conn)
                    '                    cmd.ExecuteScalar()
                    '                End If
                    '            End If
                    '            ''''If close checkbox is checked and finish checkbox is unchecked
                    '        ElseIf chksaveclose.Checked = True And chksavefinish.Checked = False Then
                    '            If i = 0 Then
                    '                strInsertQryClose = "insert into usersettings(nuserid, settingname, value,MachinName, Type) values('" & GetUserId(txtUserName.Text) & "','SendOnSaveandClose','True','" & trvMachinName.Nodes(j).Text & "','" & UserSettings.HL7 & "')"
                    '                strInsertQryFinish = "insert into usersettings(nuserid, settingname, value,MachinName, Type) values('" & GetUserId(txtUserName.Text) & "','SendOnSaveandFinish ','False','" & trvMachinName.Nodes(j).Text & "','" & UserSettings.HL7 & "')"
                    '            Else
                    '                strUpdateQryClose = "update usersettings set Value = 'True'  where nuserid = '" & GetUserId(txtUserName.Text) & "' And settingname = 'SendOnSaveandClose' AND MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                strUpdateQryFinish = "update usersettings set Value = 'False' where nuserid = '" & GetUserId(txtUserName.Text) & "' And settingname = 'SendOnSaveandFinish' AND MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                If trvMachinName.Nodes(j).Checked = False Then
                    '                    Dim strDeleteClose As String = "Delete From usersettings Where nuserid = '" & GetUserId(txtUserName.Text) & "' AND  MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                    cmd = New SqlClient.SqlCommand(strDeleteClose, conn)
                    '                    cmd.ExecuteScalar()
                    '                End If
                    '            End If
                    '            ''''If close checkbox is unchecked and finish checkbox is checked
                    '        ElseIf chksaveclose.Checked = False And chksavefinish.Checked = True Then
                    '            If i = 0 Then
                    '                strInsertQryClose = "insert into usersettings(nuserid, settingname, value,MachinName, Type) values('" & GetUserId(txtUserName.Text) & "','SendOnSaveandClose','False','" & trvMachinName.Nodes(j).Text & "','" & UserSettings.HL7 & "')"
                    '                strInsertQryFinish = "insert into usersettings(nuserid, settingname, value,MachinName, Type) values('" & GetUserId(txtUserName.Text) & "','SendOnSaveandFinish ','True','" & trvMachinName.Nodes(j).Text & "','" & UserSettings.HL7 & "')"
                    '            Else
                    '                strUpdateQryClose = "update usersettings set Value = 'False' where nuserid = '" & GetUserId(txtUserName.Text) & "' And settingname = 'SendOnSaveandClose' AND MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                strUpdateQryFinish = "update usersettings set Value = 'True'  where nuserid = '" & GetUserId(txtUserName.Text) & "' And settingname = 'SendOnSaveandFinish' AND MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                If trvMachinName.Nodes(j).Checked = False Then
                    '                    Dim strDeleteClose As String = "Delete From usersettings Where nuserid = '" & GetUserId(txtUserName.Text) & "' AND  MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                    cmd = New SqlClient.SqlCommand(strDeleteClose, conn)
                    '                    cmd.ExecuteScalar()
                    '                End If
                    '            End If
                    '            ''''If close checkbox and finish checkbox is checked
                    '        ElseIf chksaveclose.Checked = True And chksavefinish.Checked = True Then
                    '            If i = 0 Then
                    '                strInsertQryClose = "insert into usersettings(nuserid, settingname, value,MachinName, Type) values('" & GetUserId(txtUserName.Text) & "','SendOnSaveandClose','True','" & trvMachinName.Nodes(j).Text & "','" & UserSettings.HL7 & "')"
                    '                strInsertQryFinish = "insert into usersettings(nuserid, settingname, value,MachinName, Type) values('" & GetUserId(txtUserName.Text) & "','SendOnSaveandFinish ','True','" & trvMachinName.Nodes(j).Text & "','" & UserSettings.HL7 & "')"
                    '            Else
                    '                strUpdateQryClose = "update usersettings set Value = 'True'  where nuserid = '" & GetUserId(txtUserName.Text) & "' And settingname = 'SendOnSaveandClose' AND MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                strUpdateQryFinish = "update usersettings set Value = 'True'  where nuserid = '" & GetUserId(txtUserName.Text) & "' And settingname = 'SendOnSaveandFinish' AND MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                If trvMachinName.Nodes(j).Checked = False Then
                    '                    Dim strDeleteClose As String = "Delete From usersettings Where nuserid = '" & GetUserId(txtUserName.Text) & "' AND  MachinName = '" & trvMachinName.Nodes(j).Text & "'"
                    '                    cmd = New SqlClient.SqlCommand(strDeleteClose, conn)
                    '                    cmd.ExecuteScalar()
                    '                End If
                    '            End If
                    '        End If

                    '        If i = 0 Then
                    '            ''''ExecuteNonQry if setting is insert
                    '            cmd = New SqlClient.SqlCommand(strInsertQryClose, conn)
                    '            cmd.ExecuteNonQuery()
                    '            cmd = New SqlClient.SqlCommand(strInsertQryFinish, conn)
                    '            cmd.ExecuteNonQuery()
                    '        Else
                    '            ''''ExecuteNonQry if setting is update.
                    '            cmd = New SqlClient.SqlCommand(strUpdateQryClose, conn)
                    '            cmd.ExecuteNonQuery()
                    '            cmd = New SqlClient.SqlCommand(strUpdateQryFinish, conn)
                    '            cmd.ExecuteNonQuery()
                    '        End If
                    '    End If
                    'Next

                End If ''''''''

                Me.DialogResult = DialogResult.OK
                Me.Cursor = Cursors.Default
                Me.Close()
                'sarika 20th feb
                'audit trail
                'Dim userid As Long
                'userid = GetUserId(gstrLoginName)

                '  Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Details of User " & txtUserName.Text.Trim & " are modified", gstrLoginName, gstrClientMachineName)
                If blnResetPwdFlag = True Then
                    objAudit.CreateLog(clsAudit.enmActivityType.ResetPassword, "Password of User " & txtUserName.Text.Trim & " has been reset by the administrator", gstrLoginName, gstrClientMachineName)
                End If

                
                objAudit = Nothing
                objUser = Nothing

                '------------
                Exit Sub

                Me.Cursor = Cursors.Default
                objAudit.CreateLog(clsAudit.enmActivityType.Other, "Unable to Update details of user " & txtUserName.Text.Trim, gstrLoginName, gstrClientMachineName, , , clsAudit.enmOutcome.Failure)
                MessageBox.Show("Unable to Update user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            'End If

            Me.Cursor = Cursors.Default

        Catch objErr As Exception
            Me.DialogResult = DialogResult.None
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            conn.Close()
        End Try

    End Sub

    Public Function IsNickNameExists(ByVal Nickname As String, ByVal nUserID As Long) As Boolean
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        '  Dim ODataReader As SqlDataReader
        Dim count As Integer
        Try
            conn.Open()
            If nUserID = 0 Then
                _strSQL = "select count(*) from User_MST where sNickName ='" & Nickname & "'"
            Else
                _strSQL = "select count(*) from User_MST where sNickName ='" & Nickname & "' and nUserID <> " & nUserID
            End If
            cmd = New SqlCommand(_strSQL, conn)

            count = cmd.ExecuteScalar

            If count > 0 Then
                Return True
            End If
        Catch ex As Exception
            '   MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Function


    Public Function GetResetPwdFlag(ByVal strUserName As String) As Boolean
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        Dim ODataReader As SqlDataReader
        Dim blnResetflg As Boolean

        Try
            _strSQL = "select IsPasswordReset from User_MST where sLoginName = '" & strUserName.Replace("'", "''") & "'"

            conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            ODataReader = cmd.ExecuteReader

            If Not ODataReader Is Nothing Then
                If ODataReader.HasRows = True Then
                    While ODataReader.Read
                        If Not IsDBNull(ODataReader.Item("IsPasswordReset")) Then
                            blnResetflg = ODataReader.Item("IsPasswordReset")
                        Else
                            blnResetflg = False
                        End If
                    End While
                End If
                ODataReader.Close()
            End If

            Return blnResetflg

        Catch ex As Exception
            ' MsgBox(ex.ToString)
            Return False
        Finally
            conn.Close()
        End Try
    End Function

    'Public Function GetAdminEmail() As String
    '    Dim strAdminEmail As String = ""
    '    Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
    '    Dim cmd As SqlCommand
    '    Dim _strSQL As String = ""

    '    Try

    '        conn.Open()

    '        _strSQL = "select sEmail from User_MST where sLoginName = '" & gstrLoginName & "'" & "  and  nAdministrator = 1 "
    '        cmd = New SqlCommand(_strSQL, conn)
    '        strAdminEmail = cmd.ExecuteScalar

    '        Return strAdminEmail

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    Finally
    '        conn.Close()
    '        cmd.Dispose()
    '    End Try
    'End Function

    'Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        Dim nCount As Byte
    '        For nCount = 0 To lstRights.Items.Count - 1
    '            lstRights.SetItemChecked(nCount, True)
    '        Next
    '        Me.Cursor = Cursors.Default
    '    Catch objErr As Exception
    '        Me.Cursor = Cursors.Default
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try
    'End Sub


    'Public Sub SendMailOneAttachment(ByVal From As String, _
    '      ByVal sendTo As String, ByVal Subject As String, _
    '      ByVal Body As String, _
    '      Optional ByVal AttachmentFile As String = "", _
    '      Optional ByVal CC As String = "", _
    '      Optional ByVal BCC As String = "", _
    '      Optional ByVal SMTPServer As String = "")

    '        Dim myMessage As MailMessage

    '        Try
    '            myMessage = New MailMessage
    '            With myMessage
    '                .To = sendTo
    '                .From = From
    '                .Subject = Subject
    '                .Body = Body
    '                .BodyFormat = MailFormat.Text
    '                'CAN USER MAILFORMAT.HTML if you prefer

    '                If CC <> "" Then .Cc = CC
    '                If BCC <> "" Then .Bcc = ""

    '                If FileExists(AttachmentFile) Then _
    '                 .Attachments.Add(AttachmentFile)

    '            End With

    '            If SMTPServer <> "" Then _
    '               SmtpMail.SmtpServer = SMTPServer
    '            SmtpMail.Send(myMessage)

    '        Catch myexp As Exception
    '            Throw myexp
    '        End Try

    '    End Sub

    '******************************Function to send the mail*****************************************

    'Public Function SendMailOneAttachment(ByVal From As String, _
    '      ByVal sendTo As String, ByVal Subject As String, _
    '      ByVal Body As String, _
    '      Optional ByVal CC As String = "", _
    '      Optional ByVal SMTPServer As String = "") As Boolean

    '    Dim myMessage As MailMessage
    '    Dim objAuthentication As New Communication.Mail.gloMailAuthentication
    '    objAuthentication.RetrieveMailAuthentication()

    '    Try
    '        myMessage = New MailMessage
    '        With myMessage
    '            .To = sendTo
    '            .From = From
    '            .Subject = Subject
    '            .Body = Body
    '            .BodyFormat = MailFormat.Text
    '            'CAN USER MAILFORMAT.HTML if you prefer

    '            If CC <> "" Then .Cc = CC

    '        End With

    '        'SMTPServer = objAuthentication.SMTPServer

    '        If SMTPServer <> "" Then _
    '           SmtpMail.SmtpServer = SMTPServer
    '        SmtpMail.Send(myMessage)

    '        Return True

    '    Catch myexp As Exception
    '        MsgBox("Could not send the Email")
    '        Return False
    '    End Try

    'End Function

    '***********************g;loEMR func to validate Email address******************************
    Public Function ValidateEmailAddress(ByVal Email As String) As Boolean
        If InStr(1, Email, "@") <> 0 Then
            If InStr(1, Email, ".") <> 0 Then
            Else
                Return False
            End If
        Else
            Return False
        End If
        Return True
    End Function

    '***********************g;loEMR func to validate Email address******************************


    '********************************************gloEMR function to send mail*****************

    'Private Function gloSendMail(ByVal mailFrom As String, ByVal mailTo As String, ByVal Subject As String, ByVal Body As String, ByVal CCTo As String, ByVal SMTPServer As String) As Boolean
    '    Try
    '        If ValidateEmailAddress(mailTo) = False Then
    '            MessageBox.Show(mailTo & " is not valid email address", gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Information)
    '            txtEmailAddress.Focus()
    '            Exit Function
    '        End If


    '        'If ValidateEmailAddress(mailFrom) = False Then
    '        '    MessageBox.Show(mailFrom & " is not valid email address", gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Information)
    '        '    ' mailFrom.Focus()
    '        '    Exit Function
    '        'End If

    '        'If Trim(txtAttachmentFilePath1.Text) <> "" Then
    '        '    If System.IO.File.Exists(txtAttachmentFilePath1.Text) = False Then
    '        '        MessageBox.Show(txtAttachmentFilePath1.Text & " is not valid path", gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Information)
    '        '        txtAttachmentFilePath1.Focus()
    '        '        Exit Function
    '        '    End If
    '        'End If
    '        'If Trim(txtAttachmentFilePath2.Text) <> "" Then
    '        '    If System.IO.File.Exists(txtAttachmentFilePath2.Text) = False Then
    '        '        MessageBox.Show(txtAttachmentFilePath2.Text & " is not valid path", gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Information)
    '        '        txtAttachmentFilePath2.Focus()
    '        '        Exit Function
    '        '    End If
    '        'End If
    '        'If Trim(txtAttachmentFilePath3.Text) <> "" Then
    '        '    If System.IO.File.Exists(txtAttachmentFilePath3.Text) = False Then
    '        '        MessageBox.Show(txtAttachmentFilePath3.Text & " is not valid path", gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Information)
    '        '        txtAttachmentFilePath3.Focus()
    '        '        Exit Function
    '        '    End If
    '        'End If


    '        If MessageBox.Show("Are you sure, you want to send the mail to gloStream Support?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
    '            Me.Cursor = Cursors.WaitCursor
    '            UpdateLog_Mail("Checking Internet Connection")
    '            If IsInternetConnectionAvailable() = False Then
    '                MessageBox.Show("Your Computer is not connected to Internet. So you problem will not be reported to gloStream.", gstrMessageBoxCaption, MessageBoxButtons.OK,MessageBoxIcon.Information)
    '                UpdateLog_Mail("Your Computer is not connected to Internet. So you problem will not be reported to gloStream.")
    '                Exit Function
    '            End If
    '            Dim strSMTPServer As String
    '            Dim strUserName As String
    '            Dim strPassword As String

    '            Dim objAuthentication As New Communication.Mail.gloMailAuthentication
    '            UpdateLog_Mail("Retrieveing Mail Authentication")
    '            objAuthentication.RetrieveMailAuthentication()
    '            'strSMTPServer = objAuthentication.SMTPServer
    '            'strUserName = objAuthentication.AuthenticationUserName
    '            strSMTPServer = "mail.glostream.com"
    '            strUserName = "support@glostream.com"
    '            strPassword = objAuthentication.AuthenticationUserPassword

    '            UpdateLog_Mail(strSMTPServer & "," & strUserName & "," & strPassword)


    '            UpdateLog_Mail("Creating object of Mail Class")
    '            Dim objMail As New Communication.Mail.gloMail(strSMTPServer, strUserName, strPassword)
    '            'Dim arrlst As New ArrayList
    '            'If Trim(txtAttachmentFilePath1.Text) <> "" AndAlso System.IO.File.Exists(txtAttachmentFilePath1.Text) Then
    '            '    arrlst.Add(txtAttachmentFilePath1.Text)
    '            'End If
    '            'If Trim(txtAttachmentFilePath2.Text) <> "" AndAlso System.IO.File.Exists(txtAttachmentFilePath2.Text) Then
    '            '    arrlst.Add(txtAttachmentFilePath2.Text)
    '            'End If
    '            'If Trim(txtAttachmentFilePath3.Text) <> "" AndAlso System.IO.File.Exists(txtAttachmentFilePath3.Text) Then
    '            '    arrlst.Add(txtAttachmentFilePath3.Text)
    '            'End If
    '            'arrlst.TrimToSize()
    '            'If arrlst.Count >= 1 Then
    '            '    objMail.Attachments = arrlst
    '            'End If
    '            UpdateLog_Mail("Setting Mail Parameters")
    '            objMail.MailFrom = mailFrom
    '            objMail.MailTo = mailTo
    '            objMail.Subject = Subject
    '            objMail.BodyFormat = Communication.Mail.gloMail.gloMailFormat.TEXT
    '            'Dim strBody As New System.Text.StringBuilder(1000)
    '            'strBody.Append("gloEMR Problem" & vbCrLf & vbCrLf)
    '            'strBody.Append("Problem Date=" & System.DateTime.Now & vbCrLf & vbCrLf)
    '            'strBody.Append("Clinic Name=" & txtClinicName.Text & vbCrLf)
    '            'strBody.Append("Clinic Email Address=" & txtClinicEmailAddress.Text & vbCrLf)
    '            'strBody.Append("Sender Name=" & txtSenderName.Text & vbCrLf)
    '            'strBody.Append("Sender Email Address=" & txtSenderEmailAddress.Text & vbCrLf & vbCrLf)
    '            'strBody.Append("Module=" & cmbModules.Text & vbCrLf)
    '            'strBody.Append("Problem Title=" & txtProblemTitle.Text & vbCrLf & vbCrLf)
    '            'strBody.Append("Description" & vbCrLf)
    '            'strBody.Append(txtDescription.Text)
    '            '  objMail.Body = strBody.ToString

    '            objMail.Body = Body
    '            'objMail.Body = strBody.ToString
    '            'strBody = Nothing
    '            Body = Nothing
    '            UpdateLog_Mail("Starting to send the Mail")
    '            If objMail.SendMail() = True Then
    '                objMail = Nothing
    '                UpdateLog_Mail("Your problem has been successfully reported to gloStream")
    '                MessageBox.Show("Your problem has been successfully reported to gloStream", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Me.Close()
    '            Else
    '                objMail = Nothing
    '                MessageBox.Show("Unable to report your problem to gloStream due to " & objMail.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                UpdateLog_Mail("Unable to report your problem to gloStream due to " & objMail.ErrorMessage)
    '            End If
    '            objAuthentication = Nothing
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Unable to report your problem to gloStream due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        UpdateLog_Mail("In Exception - Unable to report your problem to gloStream due to " & ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Function


    '******************************End of Function to send the mail*****************************************


    '**************************Maintaining LOg of Mails sent********************


    Public Sub UpdateLog_Mail(ByVal strLogMessage As String)
        Try

            Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\gloEMRAdmin_AdminMail.txt", True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            objFile = Nothing
        Catch ex As Exception

        End Try
    End Sub

    '**********************Mail Log  functions*****************************


    Public Sub Fill_UserRights()
        With trvUserRights
            .Nodes.Clear()
            .CheckBoxes = True
            Dim objRights As New clsRights
            Dim dtRights As New DataTable
            dtRights = objRights.ScanParentRights()

            Dim trvParentNodes As TreeNode
            Dim nCount As Integer
            For nCount = 0 To dtRights.Rows.Count - 1
                trvParentNodes = New TreeNode
                With trvParentNodes
                    .Text = dtRights.Rows(nCount).Item(0)
                    .Tag = dtRights.Rows(nCount).Item(1)
                    .ImageIndex = 1
                    .SelectedImageIndex = 1
                    .ForeColor = Color.Black
                End With
                .Nodes.Add(trvParentNodes)
                Dim dtChild As New DataTable
                dtChild = objRights.ScanChildRights(Trim(trvParentNodes.Text))
                Dim trvChildNode As TreeNode
                Dim nCountChild As Integer
                For nCountChild = 0 To dtChild.Rows.Count - 1
                    trvChildNode = New TreeNode
                    With trvChildNode
                        .Text = dtChild.Rows(nCountChild).Item(0)
                        .Tag = dtChild.Rows(nCountChild).Item(1)
                        .ImageIndex = 1
                        .SelectedImageIndex = 1
                        .ForeColor = Color.Blue
                    End With
                    .Nodes(nCount).Nodes.Add(trvChildNode)
                    trvChildNode = Nothing
                Next
                dtChild = Nothing
                trvParentNodes = Nothing
            Next
            dtRights = Nothing
            objRights = Nothing
        End With
    End Sub


    Private Sub trvUserRights_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvUserRights.AfterCheck
        Try
            CheckUncheckChildNodes(e.Node, e.Node.Checked)
            '******By Sandip Deshmukh 17 Oct 07 Bug#346
            '******for user should not allow to associate multiple user groups
            Dim i, j As Int16
            Dim blnChecked As Boolean = True
            With trvUserRights
                For i = 0 To .GetNodeCount(False) - 1
                    For j = 0 To .Nodes(i).GetNodeCount(False) - 1
                        If .Nodes(i).Nodes(j).Checked = False Then
                            blnChecked = False
                            Exit For
                        End If
                    Next
                    If blnChecked = False Then
                        Exit For
                    End If
                Next

                If blnChecked = False Then
                    btnSelectAll.Text = "Select All"
                Else
                    btnSelectAll.Text = "Clear All"
                End If
            End With
            '******17 Oct 07 Bug#346

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub CheckUncheckChildNodes(ByVal rootNode As TreeNode, ByVal blnCheck As Boolean)
        For Each childNode As TreeNode In rootNode.Nodes
            childNode.Checked = blnCheck
            CheckUncheckChildNodes(childNode, blnCheck)
        Next
    End Sub

    Public Sub SearchNode(ByVal Trv As TreeView, ByVal strText As String)
        Dim trvNde As TreeNode
        For Each trvNde In Trv.Nodes
            SearchNode(trvNde, strText)
        Next
    End Sub

    Private Sub SearchNode(ByVal rootNode As TreeNode, ByVal strText As String)
        For Each childNode As TreeNode In rootNode.Nodes
            'If LCase(Trim(childNode.Text)) = LCase(Trim(strText)) Then
            If childNode.Tag = Convert.ToInt64(strText) Then
                trvSearchNode = childNode
                Exit Sub
            End If
            SearchNode(childNode, strText)
        Next
    End Sub


    Private Function GetCheckedNodes() As Collection
        Dim nCount As Integer
        For nCount = clSelectedUserRights.Count To 1 Step -1
            clSelectedUserRights.Remove(nCount)
        Next
        Dim trvnde As TreeNode
        For Each trvnde In trvUserRights.Nodes
            GetCheckedNodes(trvnde)
        Next
        Return clSelectedUserRights
    End Function
    Private Sub GetCheckedNodes(ByVal rootNode As TreeNode)
        If rootNode.Checked Then
            If Trim(rootNode.Text) <> Trim(trvUserRights.Nodes(0).Text) Then
                'clSelectedUserRights.Add(rootNode.Text)
                ''Sandip Darade 20090818
                clSelectedUserRights.Add(rootNode.Tag)
            End If
        End If
        For Each childNode As TreeNode In rootNode.Nodes
            GetCheckedNodes(childNode)
        Next
    End Sub

    'To Create New User Group
    Private Sub btnNewGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewGroup.Click
        Try
            Dim frmMaster As New frmUserGroup
            frmMaster.Panel3.Enabled = True
            frmMaster.blnModify = False
            frmMaster.Fill_UserRights()
            If frmMaster.ShowDialog() = DialogResult.OK Then
                Call Fill_gloEMRGroups()
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    'To show the previous tab (i.e. User details tab)
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            'tbUser.Select()
            ''''''  TabCtrl1.SelectedTab = tbcUser
            TabCtrl1.SelectedTab = tbcUser
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnExpandAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpandAll.Click
        Try
            If btnExpandAll.Text = "Expand All" Then
                trvUserRights.ExpandAll()
                btnExpandAll.Text = "Collapse All"
            ElseIf btnExpandAll.Text = "Collapse All" Then
                trvUserRights.CollapseAll()
                btnExpandAll.Text = "Expand All"
            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnCollapseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            trvUserRights.CollapseAll()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Try
            If btnSelectAll.Text = "Select All" Then
                SelectAll()
                btnSelectAll.Text = "Clear All"
            ElseIf btnSelectAll.Text = "Clear All" Then
                ClearAll()
                btnSelectAll.Text = "Select All"
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub ClearAll()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = trvUserRights.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                trvUserRights.Nodes(nCount).Checked = False
            Next
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub SelectAll()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = trvUserRights.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                trvUserRights.Nodes(nCount).Checked = True
            Next
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub



    Private Sub btnClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = trvUserRights.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                trvUserRights.Nodes(nCount).Checked = False
            Next
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub mnuSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelectAll.Click
        Try
            btnSelectAll_Click(sender, e)
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub mnuClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClearAll.Click
        Try
            btnClearAll_Click(sender, e)
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub mnuExpandAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExpandAll.Click
        Try
            btnExpandAll_Click(sender, e)
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub mnuCollapseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCollapseAll.Click
        Try
            btnCollapseAll_Click(sender, e)
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub txtZIP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtZIP_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        If Trim(txtZip.Text) <> "" Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dt As DataTable
                Dim objCSZ As New clsCSZ
                dt = objCSZ.FetchAddressInfo(Int64.Parse(Trim(txtZip.Text)))
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        txtCity.Text = dt.Rows(0).Item(0)
                        txtState.Text = dt.Rows(0).Item(1)
                    Else
                        txtCity.Text = ""
                        txtState.Text = ""
                    End If
                End If
                Me.Cursor = Cursors.Default
            Catch ex As SqlClient.SqlException
                Me.Cursor = Cursors.Default
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End If

    End Sub
    Public Sub Fill_Providers()
        'With cmbProvider
        '    .Items.Clear()
        '    .Items.Add("Not Provider")
        '    Dim clProvider As New Collection
        '    Dim objProvider As New clsProvider
        '    clProvider = objProvider.Fill_Providers
        '    objProvider = Nothing
        '    Dim nCount As Integer
        '    For nCount = 1 To clProvider.Count
        '        .Items.Add(clProvider.Item(nCount))
        '    Next
        '    .SelectedIndex = 0
        'End With

        'GLO2011-0015056 : gloPM Admin User Settings Inconsistent
        'provider combobox populated by DataSource property.
        Dim con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim adp As SqlDataAdapter = Nothing
        Dim dtProvider As DataTable = Nothing
        Dim dr As DataRow = Nothing
        Dim sQuery As String = String.Empty
        Try
            con = New SqlConnection(mdlGeneral.GetConnectionString())

            sQuery = " SELECT nProviderID, ISNULL(sFirstName,'') + SPACE(1) + CASE Provider_MST.sMiddleName WHEN '' THEN '' WHEN Provider_MST.sMiddleName THEN Provider_MST.sMiddleName  +SPACE(1) END " _
                        & "+ ISNULL(sLastName,'') AS ProviderName " _
                        & " FROM Provider_MST WHERE ISNULL(bIsBlocked,0) = 0   ORDER BY ProviderName"

            cmd = New SqlCommand(sQuery, con)
            adp = New SqlDataAdapter(cmd)
            dtProvider = New DataTable()

            adp.Fill(dtProvider)

            If Not IsNothing(dtProvider) Then

                dr = dtProvider.NewRow()
                dr("ProviderName") = "Not Provider"
                dr("nProviderID") = 0
                dtProvider.Rows.InsertAt(dr, 0)
                dtProvider.AcceptChanges()

                cmbProvider.DisplayMember = "ProviderName"
                cmbProvider.ValueMember = "nProviderID"
                cmbProvider.DataSource = dtProvider
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(dr) Then
                dr = Nothing
            End If

            If Not IsNothing(adp) Then
                adp.Dispose()
                adp = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(con) Then
                If con.State <> ConnectionState.Closed Then
                    con.Close()
                End If

                con.Dispose()
                con = Nothing
            End If
        End Try

    End Sub


    Private Sub grpCommand_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            txtImagePath.Text = ""
            If optBrowse.Checked = True Then
                picSignature.Image = Nothing
            Else
                'btnCapture.Enabled = True
                'If AxSigPlus1.TabletConnectQuery() Then
                '    AxSigPlus1.ClearTablet()
                '    AxSigPlus1.Refresh()
                '    AxSigPlus1.TabletState = 1
                '    AxSigPlus1.Visible = True
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            'picSignature.SizeMode = PictureBoxSizeMode.StretchImage
            'blnlogochanged = True

            With dlgOpenFile

                '-----------------------
                'sarika 21st May 07
                .Title = "Select Signature"
                '-----------------------
                .Filter = "Images Files(*.bmp,*.tif,*.jpg,*.jpeg,*.gif)|*.bmp;*.tif;*.jpg;*.jpeg;*.gif"
                .CheckFileExists = True
                .Multiselect = False
                .ShowHelp = False
                .ShowReadOnly = False
            End With
            If dlgOpenFile.ShowDialog = DialogResult.OK Then

                picSignature.Visible = True
                'AxSigPlus1.Visible = False
                picSignature.Image = Image.FromFile(dlgOpenFile.FileName)

                frmUserMgt.Imagepath = dlgOpenFile.FileName
                blnlogochanged = True
                txtImagePath.Text = dlgOpenFile.FileName
                btnCapture.Enabled = False

                '****************************
                Dim img As Image
                Dim nWidth As Int16
                Dim nHeight As Int16
                img = picSignature.Image
                nHeight = img.Height
                nWidth = img.Width
                'If nWidth > 150 Then
                '    nWidth = 150
                'End If
                'If nHeight > 75 Then
                '    nHeight = 75
                'End If
                img = New Bitmap(img, New Size(nWidth, nHeight))
                picSignature.Image = img
                picSignature.SizeMode = PictureBoxSizeMode.CenterImage
            Else
                blnlogochanged = False
                btnCapture.Enabled = False

                '****************************
                Dim img As Image
                Dim nWidth As Int16
                Dim nHeight As Int16
                img = picSignature.Image
                nHeight = img.Height
                nWidth = img.Width
                'If nWidth > 150 Then
                '    nWidth = 150
                'End If
                'If nHeight > 75 Then
                '    nHeight = 75
                'End If
                img = New Bitmap(img, New Size(nWidth, nHeight))
                picSignature.Image = img
                picSignature.SizeMode = PictureBoxSizeMode.CenterImage
                '****************************
            End If
        Catch objErr As Exception
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub optSignaturePad_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSignaturePad.CheckedChanged

    End Sub

    Private Sub btnCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCapture.Click
        Try
            'blnlogochanged = True
            'If File.Exists(Application.StartupPath & "\UserSign1.tif") Then
            '    File.Delete(Application.StartupPath & "\UserSign1.tif")
            'End If
            'If AxSigPlus1.TabletConnectQuery() Then
            '    If AxSigPlus1.GetNumberOfStrokes() > 0 Then
            '        AxSigPlus1.TabletState = 0 'allows JustifyMode to be set
            '        'AxSigPlus1.ImageXSize = 500 'sets image width in pixels
            '        AxSigPlus1.ImageXSize = 150 'sets image width in pixels
            '        'AxSigPlus1.ImageYSize = 200 'sets image height in pixels
            '        AxSigPlus1.ImageYSize = 75 'sets image height in pixels
            '        AxSigPlus1.ImagePenWidth = 11 'sets width of pen stroke in pixels
            '        AxSigPlus1.JustifyMode = 0 '+expands signature to fit all of sig window
            '        AxSigPlus1.WriteImageFile(Application.StartupPath & "\UserSign1.tif")
            '        'picSignature.SizeMode = PictureBoxSizeMode.StretchImage

            '        AxSigPlus1.Refresh()
            '        AxSigPlus1.TabletState = 0
            '        btnCapture.Enabled = False

            '        'Me.Close()
            '    End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '  UpdateErrorLog("Uanble to capture the Doctor Signature due to " & ex.Message, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Others, True)
        End Try
    End Sub

    Private Sub optBrowse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBrowse.CheckedChanged
        Try
            If optBrowse.Checked = True Then
                grpBrowse.Enabled = True
                ' AxSigPlus1.Visible = False
                If picSignature.Visible = False Then
                    picSignature.Visible = True
                End If
                btnCapture.Enabled = False
            Else
                'grpBrowse.Enabled = False
                'AxSigPlus1.Visible = True
                'picSignature.Visible = False
                ''picSignature.Visible = True
                'btnCapture.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnBrowse_ContextMenuChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowse.ContextMenuChanged

    End Sub

    Private Sub txtImagePath_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImagePath.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtPassword_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.LostFocus
        'If blnCancelFlag = False Then
        '    Dim password As String = txtPassword.Text.Trim
        '    Dim blnPwdFlag As Boolean = False

        '    'check for password complexity
        '    blnPwdFlag = ValidatePassword(password)

        '    If blnPwdFlag = False Then
        '        If blnModify = False Then
        '            txtPassword.Text = ""
        '            txtPassword.Focus()

        '        End If
        '    Else
        '        If blnCancelFlag = True Then
        '            txtPassword.Text = currpwd
        '            txtNickName.Focus()
        '        End If


        '    End If

        'End If
    End Sub

    '******************Function To genarate a string randomly*****************************************
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

    '**************************************************************************************************
    'Function to check the password complexity

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

                MessageBox.Show("The  length of the password  should be atleast  " & minLength, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Text = ""
                txtConfirmPassword.Text = ""
                Return False
            End If

            ' Check for minimum number of occurrences.
            If upper.Matches(pwd).Count < numUpper Then
                ' MsgBox("The password should contain atleast " & numUpper & " upper case letter")
                MessageBox.Show("The password should contain atleast " & numUpper & " upper case letter", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Text = ""
                txtConfirmPassword.Text = ""
                Return False
            End If


            If lower.Matches(pwd).Count < numLower Then
                MessageBox.Show("The password should contain atleast " & numLower & " lower case letter", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Text = ""
                txtConfirmPassword.Text = ""
                Return False
            End If

            If number.Matches(pwd).Count < numNumbers Then
                MessageBox.Show("The password should contain atleast " & numNumbers & " digits", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Text = ""
                txtConfirmPassword.Text = ""
                Return False
            End If

            If special.Matches(pwd).Count < numSpecial Then
                MessageBox.Show("The password should contain atleast " & numSpecial & " special characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Text = ""
                txtConfirmPassword.Text = ""
                Return False
            End If

            'If InStr(UCase(pwd), UCase(txtUserName.Text.Trim)) Then
            '    MsgBox("The password should not contain your login name")
            '    Return False
            'End If
            If blnflagOK = False Then

                If UCase(pwd) = UCase(gstrLoginName) Then
                    MessageBox.Show("The password should not same as  your login name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPassword.Text = ""
                    txtConfirmPassword.Text = ""
                    Return False
                End If

            Else
                If UCase(pwd) = UCase(txtUserName.Text.Trim) Then
                    MessageBox.Show("The password should not same as  your login name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPassword.Text = ""
                    txtConfirmPassword.Text = ""
                    Return False
                End If
            End If

            If letters.Matches(pwd).Count < numLetters Then
                MessageBox.Show("The password should contain atleast " & numLetters & " alphabet", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Text = ""
                txtConfirmPassword.Text = ""
                Return False
            End If

            ''Check whether the pwd is one of the recent pwds
            If GetRecentPwds(pwd) Then
                MessageBox.Show("You have already used this password recently , so select another password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Text = ""
                txtConfirmPassword.Text = ""
                Return False
            End If

            ' Passed all checks.
            Return True

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally

        End Try
    End Function

    Public Function GetRecentPwds(ByVal strpwd As String) As Boolean
        'if the pwd exists in the recent pwds then return true 

        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        '  Dim oDataReader As SqlDataReader
        Dim dtRecentPwds As New DataTable
        Dim da As SqlDataAdapter
        Dim PwdStr As New Collection
        Dim blnisexists As Boolean = False

        Try
            _strSQL = "select sPassword, PwdCreationDate from RecentPwd_MST where LoginName ='" & gstrLoginName & "'"

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

            Dim objEncryption As New clsEncryption

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

            Return blnisexists

        Catch ex As Exception
            ' MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

    End Function

    '**************************************************************************************************

    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged

    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    GenRandomStr()
    'End Sub


    Private Sub btnGenPwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenPwd.Click

        Dim strgenpwd As String
        blnResetPwdFlag = True

        Try
            strgenpwd = GenRandomStr()
            txtPassword.Text = strgenpwd
            txtConfirmPassword.Text = strgenpwd

            MessageBox.Show("The password of the user : " & txtUserName.Text.Trim & " has been reset to " & strgenpwd, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function GetUserId(ByVal username As String) As Long
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        Dim userid As Long

        Try
            conn.Open()
            _strSQL = "select nUserID from User_MST where sLoginName ='" & username & "'"

            cmd = New SqlCommand(_strSQL, conn)
            userid = cmd.ExecuteScalar
            conn.Close()

            Return userid
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try
    End Function


    Private Function GetCoSign(ByVal username As String) As Boolean
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        If cmbProvider.Text = "Not Provider" Then

            Try
                conn.Open()
                _strSQL = "select bCoSign from User_MST where sLoginName ='" & username.Replace("'", "''") & "'"

                cmd = New SqlCommand(_strSQL, conn)
                blnCoSignFlag = cmd.ExecuteScalar
                conn.Close()

                If blnCoSignFlag = True Then
                    chckCoSign.Checked = True
                Else
                    chckCoSign.Checked = False
                End If


            Catch ex As Exception
                ' MsgBox(ex.Message)
            End Try
        Else
            chckCoSign.Visible = False
        End If
    End Function

    Private Function GetIsSecurityUser(ByVal username As String) As Boolean
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        If cmbProvider.Text = "Not Provider" Then

            Try
                conn.Open()
                _strSQL = "select bIsSecurityUser from User_MST where sLoginName ='" & username.Replace("'", "''") & "'"

                cmd = New SqlCommand(_strSQL, conn)
                bIsSecurity = cmd.ExecuteScalar
                conn.Close()

                If bIsSecurity = True Then
                    chkIssecurityUser.Checked = True
                Else
                    chkIssecurityUser.Checked = False
                End If


            Catch ex As Exception
                ' MsgBox(ex.Message)
            End Try
        Else
            chckCoSign.Visible = False
        End If
    End Function

    Private Sub cmbProvider_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectionChangeCommitted
        If cmbProvider.Text <> "Not Provider" Then
            chckCoSign.Visible = False
        End If
    End Sub

    Private Sub rdoSaveFinish_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnForwrd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForwrd.Click
        Try

            '   TabCtrl1.SelectedTab = tbcsettings

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnsetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            TabCtrl1.SelectedTab = tbcOther

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    '******By Sandip Deshmukh 15 Oct 2007 12.58 p.m. Bug# 343
    '******For bug reported the control for Zip No. is 
    '******modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added

    Private Sub txtZip_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZip.KeyPress
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or e.KeyChar = ChrW(8)) Then
            e.Handled = True
        End If
    End Sub
    '******15 Oct 2007 12.58 p.m. Bug# 343


    ''Sandip Darade 20090618
    ''not to allow to enter  special characters 
    Private Sub txtUserName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserName.KeyPress
        If (e.KeyChar = ChrW(39)) Or (e.KeyChar = ChrW(44)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar >= ChrW(123) And e.KeyChar <= ChrW(125)) Or (e.KeyChar = ChrW(63)) Or (e.KeyChar = ChrW(91)) Or (e.KeyChar = ChrW(93))) Then
            e.Handled = True
        End If
    End Sub
    ''Sandip Darade 20090618
    '' not to allow to enter  special characters 
    Private Sub txtNickName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNickName.KeyPress
        If (e.KeyChar = ChrW(39)) Or (e.KeyChar = ChrW(44)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar >= ChrW(123) And e.KeyChar <= ChrW(125)) Or (e.KeyChar = ChrW(63)) Or (e.KeyChar = ChrW(91)) Or (e.KeyChar = ChrW(93))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub tbUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub lstGroups_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstGroups.MouseClick

    End Sub

    Private Sub lstGroups_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstGroups.MouseDown
        Dim i As Int16 = lstGroups.IndexFromPoint(e.X, e.Y)
        If i < 0 Then
            lstGroups.SelectedItem = Nothing
        End If
    End Sub

    Private Sub chkExchnageUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExchnageUser.CheckedChanged
        If (chkExchnageUser.Checked = True) Then

            txtExchangeLogin.Enabled = True
            txtExchangePwd.Enabled = True
            txtExchangePwdConfirm.Enabled = True

        Else

            txtExchangeLogin.Enabled = False
            txtExchangePwd.Enabled = False
            txtExchangePwdConfirm.Enabled = False
        End If
    End Sub

    Private Function GetExchangeUser(ByVal username As String) As Long
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        Dim userid As Long

        Try
            conn.Open()
            _strSQL = "select IsExchangeUser,sExchangeLogin , sExchangePassword from User_MST where sLoginName ='" & username & "'"

            cmd = New SqlCommand(_strSQL, conn)
            userid = cmd.ExecuteScalar
            conn.Close()

            Return userid
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try
    End Function

    Private Function GetUserGroupsRights(ByVal strGroupName As String) As DataTable
        Dim odb As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        odb.Connect(False)
        Dim dt As New DataTable

        Dim _sql As String = " SELECT Rights_MST.nRightsID AS RightsID FROM GroupsRights_DTL LEFT OUTER JOIN " _
                      & " Rights_MST ON GroupsRights_DTL.nRightsID = Rights_MST.nRightsID RIGHT OUTER JOIN  " _
                      & " Groups_MST ON GroupsRights_DTL.nGroupID = Groups_MST.nGroupID WHERE Groups_MST.sGroupName ='" & strGroupName.Replace("'", "''") & "'"

        odb.Retrive_Query(_sql, dt)
        Return dt
    End Function
    Private Sub btnEAGenPwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEAGenPwd.Click
        Dim strgenpwd As String
        blnResetPwdFlag = True

        Try
            strgenpwd = GenRandomStr()
            txtEAPassword.Text = strgenpwd
            txtEAConfirmPassword.Text = strgenpwd

            MessageBox.Show("The Emergency-Patient-Chart-Access password of the user : " & txtUserName.Text.Trim & " has been reset to " & strgenpwd, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpValidupto.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub ChkEmergencyAccess_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkEmergencyAccess.CheckedChanged
        DisableAndEnableEmergencyAccess()
    End Sub

    Private Sub DisableAndEnableEmergencyAccess()
        If ChkEmergencyAccess.Checked Then
            GroupBox9.Enabled = True
            dtpValidupto.Enabled = True
            dtpValidupto.Focus()
        Else
            txtEAPassword.Text = ""
            txtEAConfirmPassword.Text = ""
            dtpValidupto.Enabled = False
            dtpValidupto.Value = Now
            GroupBox9.Enabled = False
        End If
    End Sub
    Public Function FillBusinessCenter()

        Try
            Dim dtBusinessCenter As DataTable = Nothing
            dtBusinessCenter = New DataTable()
            Dim objUser As clsUsers = New clsUsers()
            dtBusinessCenter = objUser.GetBusinessCenter()

            If Not IsNothing(dtBusinessCenter) Then
                cmbBusinessCenter.DisplayMember = "BusinessCenter"
                cmbBusinessCenter.ValueMember = "nBusinessCenterID"
                cmbBusinessCenter.DataSource = dtBusinessCenter
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try


    End Function

    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer

        Dim g As Graphics = Me.CreateGraphics()
        Dim s As SizeF = g.MeasureString(_text, combo.Font)
        Dim width As Integer = Convert.ToInt32(s.Width)
        Return width
    End Function

    Private Sub cmbBusinessCenter_MouseEnter(sender As System.Object, e As System.EventArgs) Handles cmbBusinessCenter.MouseEnter
        Try
            combo = cmbBusinessCenter
            If cmbBusinessCenter.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbBusinessCenter.Items(cmbBusinessCenter.SelectedIndex), DataRowView)("BusinessCenter")), cmbBusinessCenter) >= cmbBusinessCenter.DropDownWidth - 20 Then
                    tooltip.SetToolTip(cmbBusinessCenter, Convert.ToString(DirectCast(cmbBusinessCenter.Items(cmbBusinessCenter.SelectedIndex), DataRowView)("BusinessCenter")))
                Else
                    tooltip.SetToolTip(cmbBusinessCenter, "")
                End If
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub txtWindowsLoginName_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtWindowsLoginName.KeyPress
        If (e.KeyChar = ChrW(39)) Or (e.KeyChar = ChrW(44)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar >= ChrW(123) And e.KeyChar <= ChrW(125)) Or (e.KeyChar = ChrW(63)) Or (e.KeyChar = ChrW(91)) Or (e.KeyChar = ChrW(93))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub chkSameAsUserDetails_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If chkSameAsUserDetails.Checked Then
            If (txtUserName.Text.Trim() = "") Then
                MessageBox.Show("Enter Login Name on user details.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtOCPLoginName.Text = ""
                chkSameAsUserDetails.Checked = False
                Return
            Else
                txtOCPLoginName.Text = txtUserName.Text.Trim()
                txtOCPLoginName.Enabled = False
            End If

            If (txtPassword.Text.Trim() = "") Then
                MessageBox.Show("Enter Password on user details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtOCPLoginPassword.Text = ""
                chkSameAsUserDetails.Checked = False
                Return
            Else
                txtOCPLoginPassword.Text = txtPassword.Text.Trim()
                txtOCPLoginPassword.Enabled = False
            End If

            If (txtConfirmPassword.Text.Trim() = "") Then
                MessageBox.Show("Enter Confirm Password on user details.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtOCPConfirmPassword.Text = ""
                chkSameAsUserDetails.Checked = False
                Return
            Else
                txtOCPConfirmPassword.Text = txtConfirmPassword.Text.Trim()
                txtOCPConfirmPassword.Enabled = False
            End If

        Else
            txtOCPLoginName.Text = ""
            txtOCPLoginPassword.Text = ""
            txtOCPConfirmPassword.Text = ""
            txtOCPLoginName.Enabled = True
            txtOCPLoginPassword.Enabled = True
            txtOCPConfirmPassword.Enabled = True
        End If

    End Sub

    Private Sub rb_UseCurrentCredential_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_UseCurrentCredential.CheckedChanged
        If chkSameAsUserDetails.Checked = True Then
            ShowHidePanel(True)
            chkSameAsUserDetails.Checked = False
        Else
            ShowHidePanel(False)
        End If
    End Sub

    Private Sub rb_NewCredential_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_NewCredential.CheckedChanged
        'when call on form load chkSameAsUserDetails.Checked is set 
        If chkSameAsUserDetails.Checked = False Then
            ShowHidePanel(True)
            chkSameAsUserDetails.Checked = True
        Else
            ShowHidePanel(False)
        End If


    End Sub

    Private Sub ShowHidePanel(Optional ByVal bIsOnFormLoad = True)
        If rb_UseCurrentCredential.Checked Then
            pnlNewLoginDetails.Visible = False
            pnlCurrentLoginDetails.Visible = True

            If (txtUserName.Text.Trim() = "") Then
                MessageBox.Show("Enter Login Name on user details.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtOCPLoginName.Text = ""
                Exit Sub
            Else
                txtOCPLoginName.Text = txtUserName.Text.Trim()
                lblCurrentLoginName.Text = txtUserName.Text.Trim()
            End If

            If (txtPassword.Text.Trim() = "") Then
                MessageBox.Show("Enter Password on user details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtOCPLoginPassword.Text = ""
                Exit Sub
            Else
                txtOCPLoginPassword.Text = txtPassword.Text.Trim()
            End If

            If (txtConfirmPassword.Text.Trim() = "") Then
                MessageBox.Show("Enter Confirm Password on user details.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtOCPConfirmPassword.Text = ""
                Exit Sub
            Else
                txtOCPConfirmPassword.Text = txtConfirmPassword.Text.Trim()
            End If
            chkAllowPortalAccess.Checked = If(bIsOnFormLoad = False, False, chkAllowPortalAccess.Checked)
        Else
            pnlNewLoginDetails.Visible = True
            pnlCurrentLoginDetails.Visible = False
            txtOCPLoginName.Text = If(bIsOnFormLoad = False, "", txtOCPLoginName.Text)
            txtOCPLoginPassword.Text = If(bIsOnFormLoad = False, "", txtOCPLoginPassword.Text)
            txtOCPConfirmPassword.Text = If(bIsOnFormLoad = False, "", txtOCPConfirmPassword.Text)
            chkAllowPortalAccess.Checked = If(bIsOnFormLoad = False, False, chkAllowPortalAccess.Checked)
        End If
    End Sub

End Class