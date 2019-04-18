Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Mail
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frmUserMgt

    Inherits System.Windows.Forms.Form

    Public blnModify As Boolean
    Public blnCheckRights As Boolean = False
    Public blnClose As Boolean = False
    Public blnFinish As Boolean = False
    Public nBlockStatus As Boolean = False

    Dim _blnlogochanged As Boolean
    Dim blnIsAdministrator As Boolean
    Dim blnflagOK As Boolean

    Dim blnCoSignFlag As Boolean = False
    Dim blnMailSentFlag As Boolean = False
    Dim bIsSecurity As Boolean = False
    Dim blnCancelFlag As Boolean = False
    Dim blnPwdFlag As Boolean = False
    Dim blnResetPwdFlag As Boolean = False
    Dim blnAuditTrailFlag As Boolean = False

    Dim trvSearchNode As TreeNode

    Dim clSelectedUserRights As New Collection
    Dim clSelectedAuditRights As New Collection

    Dim currpwd As String
    Dim _encryptionKey As String = "12345678"
    Dim mailTo, mailFrom, Subject, Body, SMTPServer, CCTo As String
    Public Shared Imagepath As String

    Public oAddressContol As gloAddress.gloAddressControl


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        oAddressContol = New gloAddress.gloAddressControl(gloEMRAdmin.mdlGeneral.GetConnectionString())
        oAddressContol.Dock = DockStyle.Fill
        pnlAddresssControl.Controls.Add(oAddressContol)
    End Sub

#Region "Zip control implemented  "
    'variable are used in Show ZipControl
    Public isFormLoading As Boolean = False
    Private oZipcontrol As gloAddress.gloZipcontrol
    Private isSearchControlOpen As Boolean = False
    Private _TempZipText As String
    Private _isZipItemSelected As Boolean = False
    Private _isTextBoxLoading As Boolean = False
#End Region

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


    Private Sub frmUserMgtNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String
        Dim oDataReader As SqlDataReader
        Dim adminflag As Boolean = False

        Dim strSelectQryClose As String = ""
        Dim strSelectQryFinish As String = ""
        Dim i As Integer = 0

        Try

            '20-Mar-14 Chetan: Resolving resolution issues for bugid 65027
            Dim myScreenWidth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth
            Dim myScreenHeight As Integer = System.Windows.SystemParameters.PrimaryScreenHeight
            If Me.Width > myScreenWidth Or Me.Height > myScreenHeight Then
                Me.MaximumSize = New System.Drawing.Size(myScreenWidth, (myScreenHeight - 50))
                Me.AutoScroll = True
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
            With dtDOB
                .Format = DateTimePickerFormat.Custom
                .CustomFormat = DTFORMAT
                If blnModify = False Then
                    .Value = System.DateTime.Now
                End If
            End With

            picSignature.Visible = True

            Dim dtLocationStatus As DataTable
            Dim cmdMC As SqlCommand
            Dim connMC As SqlConnection
            Dim da As SqlDataAdapter
            Dim _strSelecteSQL As String = ""

            Try
                dtLocationStatus = New DataTable
                _strSelecteSQL = "SELECT  sMachineName FROM ClientSettings_MST "
                connMC = New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
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

            Catch ex As Exception
                MessageBox.Show("Error while retrieving location Status." & ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

            FillSpecialty()
            LoadSpecialty()

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
            End If

            ''To Enable the Co-Signature right to user other than providers
            GetCoSign(txtUserName.Text)

            GetIsSecurityUser(txtUserName.Text)
            TabCtrl1.SelectedTab = tbcUser

            'solving sales force case - GLO2011-0012372 for 6031
            If blnModify = False Then
                AuditRightsSelectAll()
            End If
            'end

            'GLO2011-0013168 : when provider is selected disabled the IsSecurityUser check box
            If cmbProvider.Text <> "Not Provider" Then
                chkIssecurityUser.Enabled = False
                chkCountforCPOE.Enabled = False
            Else
                chkCountforCPOE.Enabled = True
            End If
            'end

            'Task #67507: gloEMR & Patient Portal Send Message screen changes
            Dim objSettings As New clsSettings
            Dim isPortalEnable As String = String.Empty
            objSettings.GetSetting("PatientPortalEnabled", gnLoginID, gnClinicID, isPortalEnable)

            If isPortalEnable.ToLower() = "true" Then
                lblPortalDisplayName.Visible = True
                txtPortalDisplayName.Visible = True
                grpbPortalDisplayName.Visible = True
            End If

            objSettings = Nothing

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
            .Items.Clear()
            .Items.Add("Married")
            .Items.Add("UnMarried")
            .Items.Add("Single")
            .Items.Add("Widowed")
            .Items.Add("Divorced")
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

            'By Sandip Deshmukh 17 Oct 07 Bug#340
            'for user should not allow to associate multiple user groups
            Dim nCount As Integer
            For nCount = 0 To lstGroups.Items.Count - 1
                lstGroups.SetItemChecked(nCount, False)
            Next

            For nCount = 0 To trvUserRights.GetNodeCount(False) - 1
                trvUserRights.Nodes(nCount).Checked = False
            Next

            If blnCheckRights = True Then
                Me.Cursor = Cursors.WaitCursor

                If lstGroups.GetItemCheckState(lstGroups.SelectedIndex) = CheckState.Unchecked Then
                    Dim dtRights As New DataTable
                    Dim objUserRights As New clsUserGroups
                    'get rightId in place of right name 
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
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim cmd1 As SqlCommand
        Dim _strSQL As String = ""
        Dim pwd As String = ""
        Dim oDataReader As SqlDataReader
        Dim strInsertQryClose As String = ""
        Dim strInsertQryFinish As String = ""
        Dim strUpdateQryClose As String = ""
        Dim strUpdateQryFinish As String = ""
        Dim numdigits, numletters, numspchars, numcapletters, numminlength, numdays As Integer

        Dim objEncryption As New clsEncryption

        Try
            If Trim(txtUserName.Text) = "" Then
                MessageBox.Show("User Name must be entered.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcUser
                txtUserName.Focus()
                Exit Sub
            End If

            If txtEmailAddress.Text.Trim().Length >= 1 Then
                If (CheckEmailAddress(txtEmailAddress.Text)) = False Then
                    MessageBox.Show("Please enter a valid email id.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtEmailAddress.Focus()
                    Exit Sub
                End If
            End If
            '' ens

            If Trim(txtPassword.Text) = "" Then
                MessageBox.Show("Password must be entered.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcUser
                txtPassword.Focus()
                Exit Sub
            End If

            'Developer: Mitesh Patel Date:03-Jan-2012 PRD: Direct Ability
            If txtAbilityEmail.Text.Trim() <> "" Or txtAbilityPassword.Text <> "" Then
                If (CheckEmailAddress(txtAbilityEmail.Text)) = False Then
                    MessageBox.Show("Please enter a valid Direct address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectTab(TabCtrl1.TabPages.IndexOf(tbcUser))
                    txtAbilityEmail.Focus()
                    Exit Sub
                End If
                If Trim(txtAbilityPassword.Text) = "" Then
                    MessageBox.Show("gloStream Direct Inbox password must be entered.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectTab(TabCtrl1.TabPages.IndexOf(tbcUser))
                    txtAbilityPassword.Focus()
                    Exit Sub
                End If
            End If

            'Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart
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

                'Added Valid upto Date for Emergency Access Password as on 12032010
                If dtpValidupto.Value < Now.Date Then
                    MessageBox.Show("Emergency Access Password Validity Date should be Greater than or Equal to Today", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcUser
                    dtpValidupto.Focus()
                    Exit Sub
                End If
            End If
            'Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart

            If chkExchnageUser.Checked = True Then
                If txtExchangeLogin.Text.Trim() = "" Then
                    MessageBox.Show("Please enter exchange login name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcUser
                    txtExchangeLogin.Focus()
                    Exit Sub
                End If

                If txtExchangePwd.Text.Trim() = "" Then
                    MessageBox.Show("Please enter exchange password.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TabCtrl1.SelectedTab = tbcUser
                    txtExchangePwd.Focus()
                    Exit Sub
                End If

                If txtExchangePwd.Text.Trim() <> txtExchangePwdConfirm.Text.Trim() Then
                    MessageBox.Show("Exchange passwords do not match.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                        If Not IsDBNull(oDataReader.Item("nAdministrator")) Then
                            blnIsAdministrator = oDataReader.Item("nAdministrator")
                        Else
                            blnIsAdministrator = False
                        End If
                    End While
                End If
                oDataReader.Close()
            End If

            If chkgloEMRAdmin.Checked = False Then
                blnIsAdministrator = False

                'validate the password if he is not an administrator
                Dim blnPwdReset As Boolean = False
                blnPwdReset = GetResetPwdFlag(txtUserName.Text.Trim)

                If blnPwdReset = True Or blnResetPwdFlag = True Then
                    'dodnt validate the password for generate pwd.
                Else
                    blnPwdFlag = ValidatePassword(txtPassword.Text.Trim, numminlength, numcapletters, 0, numdigits, numspchars, Nothing, numletters, numdays)
                    If blnPwdFlag = False Then
                        txtPassword.Text = ""
                        'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                        TabCtrl1.SelectedTab = tbcUser
                        txtPassword.Focus()
                        Exit Sub
                    End If
                End If
            Else
                blnIsAdministrator = True
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

            If Trim(txtConfirmPassword.Text) = "" Then
                MessageBox.Show("Confirm Password must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcUser
                txtConfirmPassword.Focus()
                Exit Sub
            End If
            If Trim(txtPassword.Text) <> Trim(txtConfirmPassword.Text) Then
                MessageBox.Show("Password and Confirm Password must be same", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcUser
                '26 th Oct 2007 
                txtPassword.Focus()
                Exit Sub
            End If

            GetCheckedNodes()
            If clSelectedUserRights.Count <= 0 Then
                MessageBox.Show("Minimum one User Rights must be selected", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcUser
                trvUserRights.Focus()
                Exit Sub
            End If

            'Added Rahul for AuditRights on 20101019
            GetAuditRightsCheckedNodes()
            'End

            Me.Cursor = Cursors.WaitCursor
            Dim objUser As New clsUsers
            'Check User already exists or not
            If blnModify = True Then
                If objUser.CheckUserExists(txtUserName.Text, txtUserName.Tag) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("User already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    objUser = Nothing
                    'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                    TabCtrl1.SelectedTab = tbcUser
                    txtUserName.Focus()
                    Exit Sub
                End If
            Else
                If objUser.CheckUserExists(txtUserName.Text) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("User already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                    TabCtrl1.SelectedTab = tbcUser
                    objUser = Nothing
                    txtUserName.Focus()
                    Exit Sub
                End If
            End If


            '-------------------------------------------------
            'Added on 03-08-2016 by Juily for Checking window login user already exists or not
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
            '--------------------------------------------------------

            'Resolving Paste Issue
            If Regex.IsMatch(txtWindowsLoginName.Text, "[/\\[\]:;|=,+*?<>]") = True Then
                Me.Cursor = Cursors.Default
                MessageBox.Show("Windows Login Name cannot contain the characters /\[]:;|=,+*?<>.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtWindowsLoginName.Focus()
                Exit Sub
            End If

            'By Sandip Deshmukh 15 Oct 2007 11.15 a.m. Bug# 341 For bug reported the control for Phone No. is modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
            Me.Cursor = Cursors.Default
            If Len(Trim(txtPhoneNo.Text)) > 0 And Len(Trim(txtPhoneNo.Text)) < 10 Then
                MessageBox.Show("Phone Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcOther
                txtPhoneNo.Focus()
                Exit Sub
            End If

            'By Sandip Deshmukh 15 Oct 2007 12.52 p.m. Bug# 344 For bug reported the control for SSN No. is modified from Textbox to MaskedTextbox(9 digit numeric )and following code is added
            Me.Cursor = Cursors.Default
            If Len(Trim(txtSSN.Text)) > 0 And Len(Trim(txtSSN.Text)) < 9 Then
                MessageBox.Show("SSN No. Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcOther
                txtSSN.Focus()
                Exit Sub
            End If

            'By Sandip Deshmukh 15 Oct 2007 11.15 a.m. Bug# 342 For bug reported the control for Mobile No. is modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
            Me.Cursor = Cursors.Default
            If Len(Trim(txtMobileNo.Text)) > 0 And Len(Trim(txtMobileNo.Text)) < 10 Then
                MessageBox.Show("Mobile No. Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'By Sandip Deshmukh 26 th Oct 2007 to select the tabitem on the form if cause validation
                TabCtrl1.SelectedTab = tbcOther
                txtMobileNo.Focus()
                Exit Sub
            End If

            objUser.UserName = txtUserName.Text
            objUser.Password = objEncryption.EncryptToBase64String(txtPassword.Text, constEncryptDecryptKey)
            objUser.NickName = objEncryption.EncryptToBase64String(txtNickName.Text, constEncryptDecryptKey)
            objUser.WindowLoaginName = txtWindowsLoginName.Text

            'Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart            
            objUser.EAPChart = ChkEmergencyAccess.Checked
            If objUser.EAPChart = True Then
                objUser.EAPassword = objEncryption.EncryptToBase64String(txtEAPassword.Text, constEncryptDecryptKey)
                'Added Valid upto Date for Emergency Access Password as on 12032010
                objUser.ValidDt = dtpValidupto.Value
            Else
                objUser.EAPassword = ""
                'Added Valid upto Date for Emergency Access Password as on 12032010
                objUser.ValidDt = dtpValidupto.MinDate
            End If

            objEncryption = Nothing
            objUser.FirstName = txtFirstName.Text
            objUser.MiddleName = txtMiddleName.Text
            objUser.LastName = txtLastName.Text
            objUser.SSNNo = txtSSN.Text
            objUser.DOB = dtDOB.Value
            objUser.Gender = cmbGender.Text
            objUser.MaritalStatus = cmbMaritalStatus.Text

            If cmbSpecialty.Text = "[Select Specialty]" Then
                objUser.Speciality = ""
            Else
                objUser.Speciality = cmbSpecialty.Text
            End If

            objUser.Address = oAddressContol.txtAddress1.Text.Trim
            objUser.Address2 = oAddressContol.txtAddress2.Text.Trim
            objUser.City = oAddressContol.txtCity.Text
            objUser.State = oAddressContol.cmbState.Text
            objUser.ZIP = oAddressContol.txtZip.Text
            objUser.County = oAddressContol.txtCounty.Text
            objUser.Country = oAddressContol.cmbCountry.Text
            objUser.PhoneNo = txtPhoneNo.Text
            objUser.MobileNo = txtMobileNo.Text
            objUser.FAX = txtFax.Text
            objUser.Email = txtEmailAddress.Text
            objUser.BlockStatus = nBlockStatus

            Dim oEncrypt As New clsEncryption()
            objUser.IsExchangeUser = chkExchnageUser.Checked
            If objUser.IsExchangeUser = True Then
                Dim _UserName As String = txtExchangeLogin.Text.Trim()
                If _UserName.Contains("@") = True Then
                    _UserName = _UserName.Substring(0, _UserName.IndexOf("@"))
                End If
                objUser.ExchangeLogin = _UserName.Trim()
                objUser.ExchangePassward = oEncrypt.EncryptToBase64String(txtExchangePwd.Text.Trim(), _encryptionKey)
            End If

            objUser.AbilityEmail = txtAbilityEmail.Text
            objUser.AbilityPassword = oEncrypt.EncryptToBase64String(txtAbilityPassword.Text.Trim(), _encryptionKey)

            If blnlogochanged = True Then
                If optBrowse.Checked = False Then
                    picSignature.Image = Nothing
                    If File.Exists(Application.StartupPath & "\UserSign1.tif") = True Then
                        picSignature.Image = Image.FromFile(Application.StartupPath & "\UserSign1.tif")
                        picSignature.SizeMode = PictureBoxSizeMode.CenterImage
                    End If
                End If

                If IsNothing(picSignature.Image) = False Then
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
                    objUser.Signature = picSignature.Image()
                Else
                    objUser.Signature = Nothing
                End If
            Else
                If IsNothing(picSignature.Image) = False Then
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
                    objUser.Signature = picSignature.Image()
                Else
                    objUser.Signature = Nothing
                End If
            End If


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

            objUser.gloEMRAdministrator = chkgloEMRAdmin.Checked

            objUser.ISCountforCPOE = chkCountforCPOE.Checked
            objUser.ProviderID = cmbProvider.SelectedValue
            objUser.BlockStatus = nBlockStatus

            Dim nCount As Integer
            Dim clUserGroups As New Collection
            objUser.UserRights = clSelectedUserRights

            objUser.AuditRights = clSelectedAuditRights

            For nCount = 0 To lstGroups.CheckedItems.Count - 1
                clUserGroups.Add(lstGroups.CheckedItems(nCount))
            Next

            objUser.UserGroups = clUserGroups
            'Task #67507: gloEMR & Patient Portal Send Message screen changes
            objUser.PortalDisplayName = txtPortalDisplayName.Text

            'Added for updating portal display name from EMRAdmin only not from PMAdmin
            objUser.IsFromEMR = True
            Dim nuserid As Int64 = 0
            Dim objAudit As New clsAudit
            If blnModify = False Then
                nuserid = objUser.AddUser()
                If nuserid > 0 Then
                    objUser = Nothing
                    Me.DialogResult = DialogResult.OK
                    Me.Cursor = Cursors.Default
                    Me.Close()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.AdditionOfUserPrivileges, gloAuditTrail.ActivityType.Add, gstrLoginName & " has added new user " & txtUserName.Text, 0, nuserid, 0, gloAuditTrail.ActivityOutCome.Success)

                    'objAudit.CreateLog(clsAudit.enmActivityType.Add, gstrLoginName & " has added new user " & txtUserName.Text, gstrLoginName, gstrClientMachineName)
                    'objAudit = Nothing
                    Exit Sub
                Else
                    Me.Cursor = Cursors.Default
                    objAudit.CreateLog(clsAudit.enmActivityType.Other, "Unable to add user " & txtUserName.Text.Trim, gstrLoginName, gstrClientMachineName, , , clsAudit.enmOutcome.Failure)
                    MessageBox.Show("Unable to add user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else

                If objUser.EditUser(txtUserName.Tag) = True Then
                    nuserid = txtUserName.Tag
                    objUser = Nothing
                End If

                Me.DialogResult = DialogResult.OK
                Me.Cursor = Cursors.Default
                Me.Close()

                'objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Details of User " & txtUserName.Text.Trim & " are modified", gstrLoginName, gstrClientMachineName, , , , , "UserRights")

                If bchilduserright = True Or bparentuserright = True Then
                    'objAudit.CreateLog(clsAudit.enmActivityType.Modify, "User Rights of User " & txtUserName.Text.Trim & " are modified", gstrLoginName, gstrClientMachineName, "UserRights")
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ChangesToUserPrivileges, gloAuditTrail.ActivityType.Modify, "User Rights of User " & txtUserName.Text.Trim & " are modified ", 0, nuserid, 0, gloAuditTrail.ActivityOutCome.Success)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ChangesToUserPrivileges, gloAuditTrail.ActivityType.Modify, "Details of User " & txtUserName.Text.Trim & " are modified ", 0, nuserid, 0, gloAuditTrail.ActivityOutCome.Success)
                End If

                If chkAuditTrails.Checked <> blnAuditTrailFlag Then
                    If chkAuditTrails.Checked = True Then
                        objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Audit Trail of User " & txtUserName.Text.Trim & " are enabled", gstrLoginName, gstrClientMachineName, 0, False, clsAudit.enmOutcome.Success, True)
                    Else
                        objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Audit Trail of User " & txtUserName.Text.Trim & " are disabled", gstrLoginName, gstrClientMachineName, 0, False, clsAudit.enmOutcome.Success, True)
                    End If
                End If

                If blnResetPwdFlag = True Then
                    objAudit.CreateLog(clsAudit.enmActivityType.ResetPassword, "Password of User " & txtUserName.Text.Trim & " has been reset by the administrator", gstrLoginName, gstrClientMachineName)
                End If

                Exit Sub

                Me.Cursor = Cursors.Default
                objAudit.CreateLog(clsAudit.enmActivityType.Other, "Unable to Update details of user " & txtUserName.Text.Trim, gstrLoginName, gstrClientMachineName, , , clsAudit.enmOutcome.Failure)
                MessageBox.Show("Unable to Update user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            objAudit = Nothing
            objUser = Nothing
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
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
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
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
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

    'gloEMR func to validate Email address
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

    Public Sub UpdateLog_Mail(ByVal strLogMessage As String)
        Try
            Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\gloEMRAdmin_AdminMail.txt", True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            objFile = Nothing
        Catch ex As Exception
        End Try
    End Sub

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
            If bChildTrigger Then
                CheckAllChildren(e.Node, e.Node.Checked)
            End If
            If bParentTrigger Then
                CheckMyParent(e.Node, e.Node.Checked)
            End If
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
        If rbt_AuditRights.Checked = True Then
            btnAuditSelect_Click(Nothing, Nothing)
        Else
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
        End If
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
                        cmbState.Text = dt.Rows(0).Item(1)
                    Else
                        txtCity.Text = ""
                        cmbState.Text = ""
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

        Dim con As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim query As String = " SELECT nProviderID, ISNULL(sFirstName,'') + SPACE(1) + CASE Provider_MST.sMiddleName WHEN '' THEN '' WHEN Provider_MST.sMiddleName THEN Provider_MST.sMiddleName  +SPACE(1) END " _
                    & "+ ISNULL(sLastName,'') AS ProviderName " _
                    & " FROM Provider_MST WHERE ISNULL(bIsBlocked,0) = 0   ORDER BY ProviderName"
        Dim cmd As New SqlCommand(query, con)
        Dim adp As New SqlDataAdapter(cmd)
        Dim dtProvider As New DataTable
        adp.Fill(dtProvider)
        Dim dr As DataRow = dtProvider.NewRow()
        dr("ProviderName") = "Not Provider"
        dr("nProviderID") = 0
        dtProvider.Rows.InsertAt(dr, 0)
        dtProvider.AcceptChanges()
        cmbProvider.DataSource = dtProvider
        cmbProvider.DisplayMember = "ProviderName"
        cmbProvider.ValueMember = "nProviderID"
    End Sub

    Private Sub grpCommand_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            txtImagePath.Text = ""
            If optBrowse.Checked = True Then
                picSignature.Image = Nothing
            Else
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            'picSignature.SizeMode = PictureBoxSizeMode.StretchImage

            With dlgOpenFile
                .Title = "Select Signature"
                .Filter = "Images Files(*.bmp,*.tif,*.jpg,*.jpeg,*.gif)|*.bmp;*.tif;*.jpg;*.jpeg;*.gif"
                .CheckFileExists = True
                .Multiselect = False
                .ShowHelp = False
                .ShowReadOnly = False
            End With

            If dlgOpenFile.ShowDialog = DialogResult.OK Then

                picSignature.Visible = True
                picSignature.Image = Image.FromFile(dlgOpenFile.FileName)

                frmUserMgt.Imagepath = dlgOpenFile.FileName
                blnlogochanged = True
                txtImagePath.Text = dlgOpenFile.FileName
                btnCapture.Enabled = False

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
                If picSignature.Visible = False Then
                    picSignature.Visible = True
                End If
                btnCapture.Enabled = False
            Else
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
    End Sub

    'Function To genarate a string randomly*****
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

    '**
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
                Return False
            End If

            ' Check for minimum number of occurrences.
            If upper.Matches(pwd).Count < numUpper Then
                MessageBox.Show("The password should contain atleast " & numUpper & " upper case letter", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If


            If lower.Matches(pwd).Count < numLower Then
                MessageBox.Show("The password should contain atleast " & numLower & " lower case letter", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            If number.Matches(pwd).Count < numNumbers Then
                MessageBox.Show("The password should contain atleast " & numNumbers & " digits", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            If special.Matches(pwd).Count < numSpecial Then
                MessageBox.Show("The password should contain atleast " & numSpecial & " special characters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            If blnflagOK = False Then
                If UCase(pwd) = UCase(gstrLoginName) Then
                    MessageBox.Show("The password should not same as  your login name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            Else
                If UCase(pwd) = UCase(txtUserName.Text.Trim) Then
                    MessageBox.Show("The password should not same as  your login name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            End If

            If letters.Matches(pwd).Count < numLetters Then
                MessageBox.Show("The password should contain atleast " & numLetters & " alphabet", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            'Check whether the pwd is one of the recent pwds
            If GetRecentPwds(pwd) Then
                MessageBox.Show("You have already used this password recently , so select another password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            Return True

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally

        End Try
    End Function

    Public Function GetRecentPwds(ByVal strpwd As String) As Boolean
        'if the pwd exists in the recent pwds then return true 

        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
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
                    PwdStr.Add(objEncryption.DecryptFromBase64String(dtRecentPwds.Rows(i)("sPassword"), constEncryptDecryptKey))
                End If
            Next

            For i As Integer = 1 To PwdStr.Count
                If strpwd = PwdStr(i) Then
                    blnisexists = True
                End If
            Next
            Return blnisexists

        Catch ex As Exception
            ' MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Function

    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged

    End Sub

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
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
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
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
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
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
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

    Private Sub txtZip_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZip.GotFocus
        ' code Added By Dipak 20090911 for Show Zip Control on text Change of txtZip
        'Zip Code Get Store Temperorly in _TempZipText variable
        Try
            _TempZipText = txtZip.Text.Trim()
        Catch
        End Try
        'End Code By Dipak
    End Sub

    Private Sub txtZip_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtZip.KeyDown
        ' code Added By Dipak 20090911 for Show Zip Control on text Change of txtZip
        'Code added for detects '' HITS UP / DOWN '' and Set Focus to ZipControls Starting Row
        Try
            If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
                '' HITS UP / DOWN ''
                If pnlInternalControl.Visible Then
                    e.SuppressKeyPress = True
                    e.Handled = True
                    oZipcontrol.C1GridList.Focus()
                    oZipcontrol.C1GridList.[Select](oZipcontrol.C1GridList.RowSel, 0)
                End If
            End If
        Catch
        End Try
    End Sub


    'By Sandip Deshmukh 15 Oct 2007 12.58 p.m. Bug# 343 For bug reported the control for Zip No. is  modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
    Private Sub txtZip_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZip.KeyPress
        Try

            If e.KeyChar = Convert.ToChar(13) Then
                '' HITS ENTER BUTTON ''
                If pnlInternalControl.Visible Then

                    oZipcontrol_ItemSelected(Nothing, Nothing)
                End If
            ElseIf e.KeyChar = Convert.ToChar(27) Then
                '' HITS ESCAPE ''
                If txtZip.Text = "" AndAlso txtCity.Text = "" AndAlso cmbState.Text = "" Then
                    _TempZipText = txtZip.Text
                End If
                txtCity.Focus()
            End If
            ''we are allowing only alphanumeric charactors for according referring the information from the link below  
            '' http://www.postalcodedownload.com/
            'The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
            ''an alphabetic character and "N" represents a numeric character. 
            If Not e.KeyChar = Convert.ToChar(8) Then
                If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9a-zA-Z]*$") = False Then
                    e.Handled = True
                End If
            End If
        Catch

        End Try
    End Sub
    '15 Oct 2007 12.58 p.m. Bug# 343


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
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
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

    Private Sub txtZip_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZip.LostFocus
        ' code Added By Dipak 20090911 for Show Zip Control on text Change of txtZip code added for make Zip Control Invisible while los focus
        If oZipcontrol IsNot Nothing Then
            If _isZipItemSelected = False And oZipcontrol.C1GridList.Focused = False And oZipcontrol.Focused = False Then
                _isTextBoxLoading = True
                If txtCity.Text = "" AndAlso txtZip.Text = "" Then
                    _TempZipText = txtZip.Text
                End If
                pnlInternalControl.Visible = False
                _isTextBoxLoading = False
            End If
        End If
    End Sub

    Private Sub oZipcontrol_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        'code added by dipak  20090911 for show ZipControl code get a selected zip info  and store in variables
        Try
            If oZipcontrol.C1GridList.Row < 0 Then
                Exit Sub
            End If
            Dim _Zip As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 0).ToString()
            Dim _City As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 1).ToString()
            Dim _ID As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2).ToString()
            Dim _County As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 3).ToString()
            Dim _State As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 4).ToString()
            Dim _AreaCode As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 5).ToString()

            _isTextBoxLoading = True
            'code assign a selected zip info to controls
            txtZip.Text = _Zip
            txtZip.Tag = _ID
            txtCity.Text = _City
            txtCity.Tag = _AreaCode
            cmbState.Text = _State

            _isTextBoxLoading = False
            _isZipItemSelected = True
            If pnlInternalControl.Visible = True Then
                pnlInternalControl.Visible = False
                txtCity.Focus()
            End If

            isSearchControlOpen = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    Private Sub oZipcontrol_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)
        'code added by dipak  20090911 for show ZipControl
        Try
            CloseInternalControl()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
        End Try
    End Sub

    Public Function OpenInternalControl(ByVal ControlType As gloAddress.gloGridListControlType, ByVal ControlHeader As String, ByVal IsMultiSelect As Boolean, ByVal RowIndex As Integer, ByVal ColIndex As Integer, ByVal SearchText As String) As Boolean
        'code added by dipak  20090911 for show ZipControl code add control in display zip control here as parameter passed
        Dim _result As Boolean = False
        _isZipItemSelected = False
        Try
            If oZipcontrol IsNot Nothing Then
                CloseInternalControl()
            End If
            oZipcontrol = New gloAddress.gloZipcontrol(ControlType, False, 0, 0, 0, gloEMRAdmin.mdlGeneral.GetConnectionString())
            AddHandler oZipcontrol.ItemSelectedclick, AddressOf oZipcontrol_ItemSelected
            AddHandler oZipcontrol.InternalGridKeyDownclick, AddressOf oZipcontrol_InternalGridKeyDown
            oZipcontrol.ControlHeader = ControlHeader
            oZipcontrol.ShowHeader = False
            oZipcontrol.Dock = DockStyle.Fill
            pnlInternalControl.BringToFront()
            pnlInternalControl.Controls.Add(oZipcontrol)
            If Not String.IsNullOrEmpty(SearchText) Then
                oZipcontrol.Search(SearchText, gloAddress.SearchColumn.Code)
            End If
            oZipcontrol.Show()
            _result = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            _result = False
        Finally
        End Try
        isSearchControlOpen = True
        Return _result
    End Function

    Private Function CloseInternalControl() As Boolean
        If oZipcontrol IsNot Nothing Then
            _isTextBoxLoading = True
            For i As Integer = 0 To pnlInternalControl.Controls.Count - 1
                pnlInternalControl.Controls.RemoveAt(i)
            Next
            If oZipcontrol IsNot Nothing Then
                oZipcontrol.Dispose()
                oZipcontrol = Nothing
            End If
            _isTextBoxLoading = False
        End If
        Return _isTextBoxLoading
    End Function

    Private Sub txtZip_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZip.TextChanged
        Try
            pnlInternalControl.BringToFront()
            'code added for show ZipControl except Form Loading And TextLoading(mean zipControl Apears only when user try enter zip using keyboard
            If isFormLoading = False And _isTextBoxLoading = False Then
                If pnlInternalControl.Visible = False Then
                    pnlInternalControl.Visible = True
                    OpenInternalControl(gloAddress.gloGridListControlType.ZIP, "Zip", False, 0, 0, "")
                    oZipcontrol.FillControl(Convert.ToString(txtZip.Text.Trim()))
                Else
                    oZipcontrol.FillControl(Convert.ToString(txtZip.Text.Trim()))
                End If
            End If
        Catch
        Finally
        End Try
    End Sub

    Private bParentTrigger As Boolean = True
    Private bChildTrigger As Boolean = True

    Private Sub CheckAllChildren(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        bParentTrigger = False
        For Each ctn As TreeNode In tn.Nodes
            bChildTrigger = False
            ctn.Checked = bCheck
            bChildTrigger = True

            CheckAllChildren(ctn, bCheck)
        Next
        bParentTrigger = True
    End Sub

    Private Sub CheckMyParent(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        If tn Is Nothing Then
            Exit Sub
        End If
        If tn.Parent Is Nothing Then
            Exit Sub
        End If

        bChildTrigger = False
        bParentTrigger = False

        If bCheck Then
            Dim bNodeFound As Boolean = False
            For Each _Node As TreeNode In tn.Parent.Nodes
                If _Node.Checked = False Then
                    tn.Parent.Checked = False
                    bNodeFound = True
                    Exit For
                End If
            Next
            If bNodeFound = False Then
                tn.Parent.Checked = True
            End If
        Else
            tn.Parent.Checked = bCheck
        End If

        CheckMyParent(tn.Parent, bCheck)
        bParentTrigger = True
        bChildTrigger = True
    End Sub

    Private Sub TabCtrl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabCtrl1.SelectedIndexChanged

    End Sub

    Private Sub Label41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label41.Click

    End Sub

    Private Sub txtEmailAddress_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtEmailAddress.Validating
        If txtEmailAddress.Text.Trim().Length >= 1 Then
            If (CheckEmailAddress(txtEmailAddress.Text)) = False Then
                MessageBox.Show("Please enter a valid email id.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True
            End If
        End If
    End Sub

    Private Function CheckEmailAddress(ByVal input As String) As Boolean
        Dim response As Boolean = False
        If (Regex.IsMatch(input, "\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") Or input.Trim() Is "") Then
            response = True
        Else
            response = False
        End If
        Return response
    End Function

    Private Sub ChkEmergencyAccess_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkEmergencyAccess.CheckedChanged
        'Added by Ujwala Atre as on 20101004
        If ChkEmergencyAccess.Checked Then
            GroupBox9.Enabled = True
            dtpValidupto.Focus()
        Else
            txtEAPassword.Text = ""
            txtEAConfirmPassword.Text = ""
            GroupBox9.Enabled = False
        End If
    End Sub

    Private Sub btnEAGenPwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEAGenPwd.Click
        'Added by Ujwala Atre as on 20101004
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

    'Added Rahul on 20101019
#Region "Added Rahul User Audit Rights"

    Private Sub rbtn_UserRights_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtn_UserRights.CheckedChanged
        If rbtn_UserRights.Checked = True Then
            trvUserRights.Visible = True
            GroupBox6.Text = "User Rights"
            rbtn_UserRights.Font = New Font("Tahoma", 9, FontStyle.Bold)
            btnExpandAll.Visible = True
            trvAuditRights.Visible = False
        Else
            GroupBox6.Text = "User Audit Rights"
            trvUserRights.Visible = False
            btnExpandAll.Visible = False
            rbtn_UserRights.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_AuditRights_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbt_AuditRights.CheckedChanged
        If rbt_AuditRights.Checked = True Then
            trvAuditRights.Visible = True
            GroupBox6.Text = "User Audit Rights"
            rbt_AuditRights.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            GroupBox6.Text = "User Rights"
            trvAuditRights.Visible = False
            btnExpandAll.Visible = True
            rbt_AuditRights.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Public Sub Fill_AuditRights()
        With trvAuditRights
            .Nodes.Clear()
            .CheckBoxes = True
            Dim objRights As New clsAuditRights
            Dim dtRights As New DataTable
            dtRights = objRights.ScanAuditRights()

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
            Next
            dtRights = Nothing
            objRights = Nothing
        End With
    End Sub

    Private Sub AuditRightsClearAll()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = trvAuditRights.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                trvAuditRights.Nodes(nCount).Checked = False
            Next
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub AuditRightsSelectAll()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = trvAuditRights.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                trvAuditRights.Nodes(nCount).Checked = True
            Next
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Function GetAuditRightsCheckedNodes() As Collection
        Dim nCount As Integer
        For nCount = clSelectedAuditRights.Count To 1 Step -1
            clSelectedAuditRights.Remove(nCount)
        Next
        Dim trvnde As TreeNode
        For Each trvnde In trvAuditRights.Nodes
            GetAuditRightsCheckedNodes(trvnde)
        Next
        Return clSelectedAuditRights
    End Function
    Private Sub GetAuditRightsCheckedNodes(ByVal rootNode As TreeNode)
        If rootNode.Checked Then
            clSelectedAuditRights.Add(rootNode.Tag)
        End If
    End Sub

    Private Sub chkAuditTrails_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAuditTrails.CheckedChanged
        If chkAuditTrails.Checked = True Then
            rbt_AuditRights.Enabled = True
            If rbt_AuditRights.Checked = True Then
                trvAuditRights.Visible = True
                trvUserRights.Visible = False
                GroupBox6.Text = "User Audit Rights"
                rbtn_UserRights.Checked = True
                rbt_AuditRights.Font = New Font("Tahoma", 9, FontStyle.Bold)
            End If
        Else

            rbt_AuditRights.Enabled = False
            trvAuditRights.Visible = False
            trvUserRights.Visible = True
            GroupBox6.Text = "User Rights"
            rbt_AuditRights.Checked = False
            rbtn_UserRights.Checked = True
            rbtn_UserRights.Font = New Font("Tahoma", 9, FontStyle.Bold)
        End If
    End Sub

    Private Sub btnAuditSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuditSelect.Click
        Try
            If btnAuditSelect.Text = "Select All" Then
                AuditRightsSelectAll()
                btnSelectAll.Text = "Clear All"
                btnAuditSelect.Text = "Clear All"
            ElseIf btnAuditSelect.Text = "Clear All" Then
                AuditRightsClearAll()
                btnSelectAll.Text = "Select All"
                btnAuditSelect.Text = "Select All"
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
#End Region

    'Added Rahul for Load Speciality on 20101020 
    Private Sub FillSpecialty()
        Try
            Dim con As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
            Dim query As String = " SELECT ISNULL(nSpecialtyID,0) AS SpecialtyID,RTrim(ISNULL(sDescription,'')) AS Description from Specialty_MST union select 0 as SpecialtyID, '[Select Specialty]' as Description from Specialty_MST x where 1=1"
            Dim cmd As New SqlCommand(query, con)
            Dim adp As New SqlDataAdapter(cmd)
            Dim dtProvider As New DataTable
            adp.Fill(dtProvider)

            cmbSpecialty.DataSource = dtProvider
            cmbSpecialty.DisplayMember = "Description"
            cmbSpecialty.ValueMember = "SpecialtyID"
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub LoadSpecialty()
        Try
            Dim con As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
            Dim query As String = " SELECT ISNULL(sSpeciality,'') AS Speciality from User_MST where sLoginName = '" & txtUserName.Text.Replace("'", "''") & "'"
            Dim cmd As New SqlCommand(query, con)
            Dim adp As New SqlDataAdapter(cmd)
            Dim dtProvider As New DataTable
            adp.Fill(dtProvider)
            Dim strcombovalue As String = ""
            If dtProvider.Rows.Count > 0 Then
                strcombovalue = dtProvider.Rows(0)("Speciality")
            Else
                strcombovalue = ""
            End If

            If strcombovalue <> "" Then
                For i As Integer = 0 To cmbSpecialty.Items.Count - 1
                    If cmbSpecialty.Items(i)(1).ToString().Trim() = strcombovalue Then  ''here 'i' is for value member and '1' is for display member
                        cmbSpecialty.Text = strcombovalue
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    'solving sales force case - GLO2011-0012372 for 6031
    Dim bAuditRight As Boolean = False

    Private Sub trvAuditRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvAuditRights.Click
        bAuditRight = True
    End Sub

    Dim bchilduserright As Boolean = False
    Dim bparentuserright As Boolean = False

    Private Sub trvUserRights_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvUserRights.Click
        bchilduserright = True
        bparentuserright = True
    End Sub

    'GLO2011-0013168 : when provider is selected disabled the IsSecurityUser check box
    Private Sub cmbProvider_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectedIndexChanged
        Try
            If cmbProvider.Text <> "Not Provider" Then
                chkIssecurityUser.Enabled = False
                chkIssecurityUser.Checked = False
                chkCountforCPOE.Enabled = False
                chkCountforCPOE.Checked = False
            Else
                chkIssecurityUser.Enabled = True
                chkCountforCPOE.Enabled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub txtWindowsLoginName_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtWindowsLoginName.KeyPress

        '24-Apr-17 Aniket: Bug #105180: CAS-06114-H2N6Z9 : "Windows Login Name" field under EMR Admin>User Mgmt does not allow the input of special characters
        If (e.KeyChar = ChrW(47)) Or (e.KeyChar = ChrW(92)) Or (e.KeyChar = ChrW(91)) Or (e.KeyChar = ChrW(93)) Or (e.KeyChar = ChrW(58)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(124)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(44)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(63)) Or (e.KeyChar = ChrW(60)) Or (e.KeyChar = ChrW(62)) Then
            e.Handled = True
        End If

    End Sub

  
End Class