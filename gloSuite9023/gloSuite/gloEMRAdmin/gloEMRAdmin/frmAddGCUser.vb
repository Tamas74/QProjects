Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Net
Imports System.Xml
Imports System.Security.Cryptography.X509Certificates
Imports Microsoft.Web.Administration

Public Class frmAddGCUser 'Code Start-Added by kanchan on 20120731 for gloCommunity
    Private _encryptionKey As String = "12345678"

    Private Sub btnCreateUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreateUser.Click
        Me.Cursor = Cursors.WaitCursor
        Dim objUserManagementService As UserManagement.UserManagementService = Nothing
        Dim objclsgloCommunityUsers As clsgloCommunityUsers = Nothing
        Dim oclsEncryption As clsEncryption = Nothing
        Dim dtClinic As DataTable = Nothing
        Dim ObjGCUSer As clsgloCommunityUsers = Nothing
        Try
            '#Region "Validations"
            Me.Cursor = Cursors.WaitCursor

            ''Install certificate as per staging/production environment.
            If InstallCertificate() = False Then
                MessageBox.Show("Error in installing certificate", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtUserNm.Focus()
                Return
            End If
            ''end

            If String.IsNullOrEmpty(txtUserNm.Text.Trim()) Then
                MessageBox.Show("Please enter User Name", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtUserNm.Focus()
                Return
            End If
            If CheckSpecialCharacters(txtUserNm.Text) = False Then
                MessageBox.Show("Please enter Valid User Name", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtUserNm.Focus()
                Return
            End If
            If String.IsNullOrEmpty(txtPassword.Text.Trim()) Then
                MessageBox.Show("Please enter Password", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Focus()
                Return
            End If
            If CheckPassword(txtPassword.Text) = False Then
                txtPassword.Focus()
                Return
            End If
            If String.IsNullOrEmpty(txtConfirmPwd.Text.Trim()) Then
                MessageBox.Show("Please enter Confirm Password", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtConfirmPwd.Focus()
                Return
            End If
            If String.IsNullOrEmpty(txtEmail.Text.Trim()) Then
                MessageBox.Show("Please enter E-mail", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtEmail.Focus()
                Return
            End If
            If CheckEmail(txtEmail.Text) = False Then
                MessageBox.Show("Please enter valid E-mail address", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtEmail.Focus()
                Return
            End If
            If String.IsNullOrEmpty(txtQuestion.Text.Trim()) Then
                MessageBox.Show("Please enter Security Question", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtQuestion.Focus()
                Return
            End If
            If String.IsNullOrEmpty(txtAnswer.Text.Trim()) Then
                MessageBox.Show("Please enter Security Answer", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtAnswer.Focus()
                Return
            End If
            If txtPassword.Text.Trim() <> txtConfirmPwd.Text.Trim() Then
                MessageBox.Show("Password & Confirmation Password must match", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtConfirmPwd.Focus()
                Return
            End If
            '#End Region

            'Add User
            objUserManagementService = New UserManagement.UserManagementService()
            objUserManagementService.Url = mdlGeneral.gstrSharepointSrvNm + "/" + mdlGeneral.gstr_Layouts & "/UserManagementService.asmx"

            oclsEncryption = New clsEncryption()
            Dim _strEncryptPWD As String = oclsEncryption.EncryptToBase64String(txtPassword.Text.Trim(), _encryptionKey)

            ObjGCUSer = New clsgloCommunityUsers()
            dtClinic = ObjGCUSer.getClinicData()
            If IsNothing(dtClinic) Then
                MessageBox.Show("Clinic Data is not available to register User.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If dtClinic.Rows.Count > 0 Then
                Dim strStatus As String = objUserManagementService.CreateUser(txtUserNm.Text, txtPassword.Text, txtEmail.Text, txtQuestion.Text, txtAnswer.Text, True, dtClinic.Rows(0)(1), dtClinic.Rows(0)(2))

                Select Case strStatus
                    Case "DuplicateUserName"
                        MessageBox.Show("Username already exists. Please enter a different user name.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Case "DuplicateEmail"
                        MessageBox.Show("A username for that e-mail address already exists. Please enter a different e-mail address.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Case "InvalidPassword"
                        MessageBox.Show("The password provided is invalid. Please enter a valid password value.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Case "InvalidEmail"
                        MessageBox.Show("The E-mail address provided is invalid. Please check the value and try again.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Case "InvalidAnswer"
                        MessageBox.Show("The password retrieval answer provided is invalid. Please check the value and try again.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Case "InvalidQuestion"
                        MessageBox.Show("The password retrieval question provided is invalid. Please check the value and try again.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Case "InvalidUserName"
                        MessageBox.Show("The user name provided is invalid. Please check the value and try again.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Case "ProviderError"
                        MessageBox.Show("The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Case "UserRejected"
                        MessageBox.Show("The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    Case "Success"
                        If True Then
                            objclsgloCommunityUsers = New clsgloCommunityUsers()
                            objclsgloCommunityUsers.InsertGCUSer(mdlGeneral.gnLoginID, txtUserNm.Text, _strEncryptPWD, gIscommunitystaging)
                            mdlGeneral.gstrGCUserName = txtUserNm.Text
                            mdlGeneral.gstrGCPassword = txtPassword.Text

                            If IsSiteExist() = False Then
                                If CreateSite() = True Then
                                    MessageBox.Show("User has been created successfully", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Else
                                    MessageBox.Show("User has been created successfully.\n Please Note: Failed to create site please contact to system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Else
                                MessageBox.Show("User has been created successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                            Me.Close()
                            Exit Select
                        End If
                End Select
            End If
        Catch generatedExceptionName As Exception
        Finally
            Me.Cursor = Cursors.Default
            If objUserManagementService IsNot Nothing Then
                objUserManagementService.Dispose()
                objUserManagementService = Nothing
            End If
            If objclsgloCommunityUsers IsNot Nothing Then
                objclsgloCommunityUsers = Nothing
            End If
            If oclsEncryption IsNot Nothing Then
                oclsEncryption = Nothing
            End If
            If Not IsNothing(ObjGCUSer) Then
                ObjGCUSer = Nothing
            End If
            dtClinic = Nothing
            Me.Cursor = Cursors.[Default]
        End Try

    End Sub

    Private Function CheckEmail(ByVal EmailAddress As String) As Boolean
        Dim strPattern As String = "^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"

        If System.Text.RegularExpressions.Regex.IsMatch(EmailAddress, strPattern) Then

            Return True
        End If
        Return False
    End Function
    'code start-Added by kanchan on 20120802 for form based authentication
    Private Sub txtConfirmPwd_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConfirmPwd.Enter
        If String.IsNullOrEmpty(txtPassword.Text.Trim()) Then
            MessageBox.Show("Please enter Password", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPassword.Focus()
            Return
        End If
    End Sub
    'Code Start-Added by kanchan on 20120803 for CheckSpecialCharacters
    Private Function CheckSpecialCharacters(ByVal _text As String) As Boolean
        Dim strPattern As String = "^[a-zA-Z0-9_.-]+$"
        If System.Text.RegularExpressions.Regex.IsMatch(_text, strPattern) Then
            Return True
        End If
        Return False
    End Function
    'Code Start-Added by kanchan on 20120803 for CheckPassword
    Private Function CheckPassword(ByVal _text As String) As Boolean
        Dim strPattern As String = String.Empty
        Try
            strPattern = "^.*(?=.{7,}).*$"
            If System.Text.RegularExpressions.Regex.IsMatch(_text, strPattern) Then
                strPattern = "^.*(?=.*[!@#$%^&+=*]).*$"
                If System.Text.RegularExpressions.Regex.IsMatch(_text, strPattern) Then
                    Return True
                Else
                    MessageBox.Show("Password should contain at least 1 special character", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            Else
                MessageBox.Show("Password should contain atleast 7 characters,which include 1 special character", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            Return False
        Catch ex As Exception
            Return False
        Finally
            strPattern = String.Empty
        End Try
    End Function

#Region "Create site"
    Private Function CreateSite() As Boolean
        Dim _result As Boolean = False
        Dim WMService As MeetingService.Meetings = Nothing
        Dim ObjGCUSer As clsgloCommunityUsers = Nothing
        Try
            WMService = New MeetingService.Meetings()
            WMService.Url = gstrSharepointSrvNm
            'ConfigurationManager.AppSettings["SharePointSiteAddress"];
            WMService.CookieContainer = New CookieContainer()
            ObjGCUSer = New clsgloCommunityUsers()

            If oFormCookie Is Nothing Then

                WMService.CookieContainer.Add(ObjGCUSer.QueryToSharePoint(mdlGeneral.gstrGCUserName, mdlGeneral.gstrGCPassword))
            Else
                WMService.CookieContainer.Add(oFormCookie)
            End If

            Dim tz As New MeetingService.TimeZoneInf()

            If (WMService.Url.EndsWith("/")) Then
                WMService.Url = WMService.Url.Remove(WMService.Url.Length - 1, 1)
            End If

            WMService.Url = WMService.Url + gstrSharepointSiteNm + "/_vti_bin/meetings.asmx"

            Dim CreateWorkspaceResponse As System.Xml.XmlNode = Nothing

            If (Not String.IsNullOrEmpty(WMService.Url)) Then
                Dim temp As XmlNode = WMService.GetMeetingWorkspaces(False)
                ' "STS#0" for Team site & 1033 for language(Eng-Us)
                CreateWorkspaceResponse = WMService.CreateWorkspace(gstrClinicName.Trim(), "STS#0", System.UInt32.Parse("1033"), tz)

                Dim objList As New gloLists.Lists()

                objList.CookieContainer = New CookieContainer()
                If oFormCookie Is Nothing Then
                    objList.CookieContainer.Add(ObjGCUSer.QueryToSharePoint(mdlGeneral.gstrGCUserName, mdlGeneral.gstrGCPassword))
                Else
                    objList.CookieContainer.Add(oFormCookie)
                End If


                objList.Url = gstrSharepointSrvNm + "/" + gstrSharepointSiteNm & "/" & gstrClinicName.Trim() & "/_vti_bin/lists.asmx"

                objList.AddList("DataConnections", Nothing, 130)
                '130 for DataConnections Library
                Dim xml As String = "<Batch OnError='Continue'>"
                xml += "<List EnableModeration='False'/>"
                xml += "</Batch>"
                Dim listNode As XmlNode = objList.GetList("DataConnections")
                Dim version As XmlNode = listNode.Attributes("Version")
                Dim doc As New XmlDocument()
                doc.LoadXml(xml)
                Dim propertiesNode As XmlNode = doc.SelectSingleNode("//List")
                Dim resultNode As XmlNode = objList.UpdateList("DataConnections", propertiesNode, Nothing, Nothing, Nothing, version.Value)

                objList.AddList("Repository", Nothing, 101)
                '101 for Document Library
                _result = True
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.[Default]
        Finally
            If WMService IsNot Nothing Then
                WMService.Dispose()
            End If
            If ObjGCUSer IsNot Nothing Then
                ObjGCUSer = Nothing
            End If
        End Try
        Return _result
    End Function

    Private Function IsSiteExist() As Boolean
        Dim _result As Boolean = True
        Dim ObjGCUSer As clsgloCommunityUsers = Nothing
        Try
            Dim olst As New gloLists.Lists()

            olst.CookieContainer = New CookieContainer()
            If oFormCookie Is Nothing Then
                ObjGCUSer = New clsgloCommunityUsers()
                olst.CookieContainer.Add(ObjGCUSer.QueryToSharePoint(mdlGeneral.gstrGCUserName, mdlGeneral.gstrGCPassword))
            Else
                olst.CookieContainer.Add(oFormCookie)
            End If

            olst.Url = gstrSharepointSrvNm + "/" + gstrSharepointSiteNm & "/" & gstrClinicName.Trim() & "/_vti_bin/lists.asmx"

            Dim node As XmlNode = olst.GetListCollection()
        Catch ex As Exception
            _result = False
        End Try
        Return _result
    End Function

    Private Function InstallCertificate() As Boolean
        Dim _IsCertInstalled As Boolean = False

        Try
            Dim mgr As ServerManager = Nothing
            Dim server As String = Environment.MachineName
            ' or remote machine name
            If [String].IsNullOrEmpty(server) Then
                mgr = New ServerManager()
            Else
                mgr = ServerManager.OpenRemote(server)
            End If

            'For some reason this created a new store called 'Personal' - we'll have to figure that out
            Dim store As New X509Store(StoreName.Root, StoreLocation.LocalMachine)
            store.Open(OpenFlags.OpenExistingOnly Or OpenFlags.ReadWrite)

            'Looks like we can create this from a byte array as well
            Dim _strgloCommunityCert As String = "StagingCert.cer"
            If gIscommunitystaging = False Then
                _strgloCommunityCert = "ProductionCert.cer"
            End If

            Dim certificate As New X509Certificate2(System.Windows.Forms.Application.StartupPath & "\gloCommunityCertificates\" & _strgloCommunityCert)

            store.Add(certificate)
            store.Close()
            _IsCertInstalled = True
        Catch ex As Exception
            Me.Cursor = Cursors.[Default]
        End Try
        Return _IsCertInstalled
    End Function
#End Region

    Private Sub frmAddGCUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mdlGeneral.Fill_Clinic()
    End Sub
End Class