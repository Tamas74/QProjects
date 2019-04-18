Imports System.Windows.Forms
Imports RxSniffer.RxGeneral
Imports Microsoft.Win32
Imports System.IO
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Drawing
Imports gloInstallerCommandParametrs

Public Class frmSettings
    ' Dim regKey As RegistryKey
    ' Dim objEncryption As clsEncryption
    Dim sStagingURl As String = String.Empty
    Dim sProductionURl As String = String.Empty
    Dim s10dot6StagingURl As String = String.Empty
    Dim s10dot6ProductionURl As String = String.Empty
    Dim sTmprRxURl As String = String.Empty
    Dim sSecureMsgStagingURL As String = String.Empty
    Dim sSecureMsgProductionURL As String = String.Empty
    Dim sTempSecurMsgUrl As String = String.Empty
    Structure structSettings
        Dim strSettingName As String
        Dim strSettingValue As String
        Dim DbId As Long
    End Structure
    Dim strgloServiceConnString As String = String.Empty
    Dim arrSettingsCollection As New List(Of structSettings)
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try

            'If chckLog.Checked Then
            '    mdlGeneral.DeleteTempDir()
            'End If
            'Me.DialogResult = System.Windows.Forms.DialogResult.OK
            If txtServer.Text = "" Then
                MessageBox.Show("Please enter SQL Server name.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtServer.Focus()
                Exit Sub
            End If
            If txtDatabase.Text = "" Then
                MessageBox.Show("Please enter Database name.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDatabase.Focus()
                Exit Sub
            End If
            If txtUser.Text = "" Then
                MessageBox.Show("Please enter SQL Server User.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtUser.Focus()
                Exit Sub
            End If
            If txtPassword.Text = "" Then
                MessageBox.Show("Please enter SQL Server Password.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Focus()
                Exit Sub
            End If
            If Not IsConnect(txtServer.Text.Trim(), txtDatabase.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text.Trim) Then
                MessageBox.Show("Invalid Credentials.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            'regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, Security.AccessControl.RegistryRights.FullControl)
            'If IsNothing(regKey) = True Then
            '    regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
            '    regKey.CreateSubKey(gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName)
            'End If
            'If (IsNothing(regKey) = False) Then
            '    regKey.Close()
            '    regKey.Dispose()
            'End If
            If rbStagging.Checked = False And rbProduction.Checked = False Then
                MessageBox.Show("Please select Surescript Server.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If TxtErxWebService.Text = "" Then
                MessageBox.Show("Please enter eRx Web Service. ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                TxtErxWebService.Focus()
                Exit Sub
            End If

            If txtSecureMsgSrv.Text = "" Then
                MessageBox.Show("Please enter Secure Message Web Service. ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtSecureMsgSrv.Focus()
                Exit Sub
            End If
            If chkEnableEpcsLog.Checked Then
                If txtEpcsAuditlogUsername.Text.Trim = "" Then
                    MessageBox.Show("Username must be entered. ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtEpcsAuditlogUsername.Focus()
                    Exit Sub
                End If
                If txtEpcsAuditlogPassword.Text.Trim = "" Then
                    MessageBox.Show("Password must be entered. ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtEpcsAuditlogPassword.Focus()
                    Exit Sub
                End If
            End If

            'Settings for Dirservice
            If chckFullDownload.Checked = True Then
                'regKey.SetValue("DirService", "True")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "DirService"
                objStructSettings.strSettingValue = "True"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnFullDownload = True
            Else
                'regKey.SetValue("DirService", "False")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "DirService"
                objStructSettings.strSettingValue = "False"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnFullDownload = False
            End If

            'Settings for PharmacyDownload
            If chkEnbPharmacy.Checked = True Then
                ' regKey.SetValue("PharmacyDownload", "True")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "PharmacyDownload"
                objStructSettings.strSettingValue = "True"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                'For resolving case no :GLO2011-0010737 i.e Conflicts in Contacts_MST
                mdlGeneral.gblnPharmacyDownload = True
            Else
                'regKey.SetValue("PharmacyDownload", "False")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "PharmacyDownload"
                objStructSettings.strSettingValue = "False"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnPharmacyDownload = False
            End If

            'Settings for PdmpDownload
            If chkPDMPAutoDownload.Checked = True Then
                ' regKey.SetValue("PharmacyDownload", "True")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "SnifferPDMPDownload"
                objStructSettings.strSettingValue = "True"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                'For resolving case no :GLO2011-0010737 i.e Conflicts in Contacts_MST
                mdlGeneral.gblnPDMPDownload = True
            Else
                'regKey.SetValue("PharmacyDownload", "False")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "SnifferPDMPDownload"
                objStructSettings.strSettingValue = "False"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnPDMPDownload = False
            End If

            'Settings for Prescriber Full download
            If chckFullPrescriberDownload.Checked = True Then
                'regKey.SetValue("DirService", "True")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "PrescriberFullDownload"
                objStructSettings.strSettingValue = "True"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnFullDownload = True
            Else
                'regKey.SetValue("DirService", "False")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "PrescriberFullDownload"
                objStructSettings.strSettingValue = "False"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnFullDownload = False
            End If

            'Settings for EnableEpcsAuditLog
            If chkEnableEpcsLog.Checked = True Then
                'regKey.SetValue("EnableAutoEligibility", "True")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "EnableEpcsAuditLog"
                objStructSettings.strSettingValue = "True"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnEnableEpcsAuditLog = True
            Else
                'regKey.SetValue("EnableAutoEligibility", "False")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "EnableEpcsAuditLog"
                objStructSettings.strSettingValue = "False"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnEnableEpcsAuditLog = False
            End If


            'Settings for EnableAutoEligibility
            If chkBoxEnableEligibility.Checked = True Then
                'regKey.SetValue("EnableAutoEligibility", "True")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "EnableAutoEligibility"
                objStructSettings.strSettingValue = "True"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnEnableAutoEligibility = True
            Else
                'regKey.SetValue("EnableAutoEligibility", "False")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "EnableAutoEligibility"
                objStructSettings.strSettingValue = "False"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnEnableAutoEligibility = False
            End If

            'Settings for GenerateRxEligibilityLog
            If chkGenrateRxEligLog.Checked = True Then
                'regKey.SetValue("GenerateRxEligibilityLog", "True")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "GenerateRxEligibilityLog"
                objStructSettings.strSettingValue = "True"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnGenerateRxEligibilityLog = True
            Else
                ' regKey.SetValue("GenerateRxEligibilityLog", "False")
                Dim objStructSettings As New structSettings()
                objStructSettings.strSettingName = "GenerateRxEligibilityLog"
                objStructSettings.strSettingValue = "False"
                objStructSettings.DbId = 0
                arrSettingsCollection.Add(objStructSettings)
                mdlGeneral.gblnGenerateRxEligibilityLog = False
            End If



            Dim objstruct As structSettings
            objstruct = New structSettings()
            objstruct.strSettingName = "ServiceInterval"
            objstruct.strSettingValue = numInterval.Value
            objstruct.DbId = 0
            arrSettingsCollection.Add(objstruct)

            objstruct = New structSettings()
            objstruct.strSettingName = "FullDownloadDay"
            objstruct.strSettingValue = cmbFullDay.SelectedValue
            objstruct.DbId = 0
            arrSettingsCollection.Add(objstruct)

            objstruct = New structSettings()
            objstruct.strSettingName = "FullDownloadInterval"
            objstruct.strSettingValue = cmbFullTime.SelectedValue
            objstruct.DbId = 0
            arrSettingsCollection.Add(objstruct)

            objstruct = New structSettings()
            objstruct.strSettingName = "NightlyDownloadInterval"
            objstruct.strSettingValue = cmbPartialTime.SelectedValue
            objstruct.DbId = 0
            arrSettingsCollection.Add(objstruct)

            objstruct = New structSettings()
            objstruct.strSettingName = "EpcsAuditLogStartInterval"
            objstruct.strSettingValue = cmdEpcsFullTime.SelectedValue
            objstruct.DbId = 0
            arrSettingsCollection.Add(objstruct)

            If rbStagging.Checked = True Then   ''Staging URL
                'regKey.SetValue("StagingServer", True)
                objstruct = New structSettings()
                objstruct.strSettingName = "StagingServer"
                objstruct.strSettingValue = "True"
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)

                'objstruct = New structSettings()
                'objstruct.strSettingName = "eRxStagingWebserviceURL"
                'objstruct.strSettingValue = TxtErxWebService.Text
                'objstruct.DbId = 0
                'arrSettingsCollection.Add(objstruct)
                '''' Code addded to save secure Message staging URL
                objstruct = New structSettings()
                objstruct.strSettingName = "SECUREMSGSTAGINGWEBSERVICEURL"
                objstruct.strSettingValue = txtSecureMsgSrv.Text
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)

                'newly added url
                objstruct = New structSettings()
                objstruct.strSettingName = "pdmpservice"
                objstruct.strSettingValue = txtPDMPServiceURL.Text
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)

                'Directory Download 4.0 or 4.4
                If chkPharmacyType.Checked = True Then
                    objstruct = New structSettings()
                    objstruct.strSettingName = "EnableDirectory4dot4"
                    objstruct.strSettingValue = "True"
                    objstruct.DbId = 0
                    arrSettingsCollection.Add(objstruct)

                    objstruct = New structSettings()
                    objstruct.strSettingName = "eRx10dot6StagingWebserviceURL"
                    objstruct.strSettingValue = TxtErxWebService.Text
                    objstruct.DbId = 0
                    arrSettingsCollection.Add(objstruct)

                Else
                    objstruct = New structSettings()
                    objstruct.strSettingName = "EnableDirectory4dot4"
                    objstruct.strSettingValue = "False"
                    objstruct.DbId = 0
                    arrSettingsCollection.Add(objstruct)

                    objstruct = New structSettings()
                    objstruct.strSettingName = "eRxStagingWebserviceURL"
                    objstruct.strSettingValue = TxtErxWebService.Text
                    objstruct.DbId = 0
                    arrSettingsCollection.Add(objstruct)
                End If

            Else         ''Production URL
                'regKey.SetValue("StagingServer", False)
                objstruct = New structSettings()
                objstruct.strSettingName = "StagingServer"
                objstruct.strSettingValue = "False"
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)

                '''' Code addded to save secure Message Production URL
                objstruct = New structSettings()
                objstruct.strSettingName = "SECUREMSGPRODUCTIONWEBSERVICEURL"
                objstruct.strSettingValue = txtSecureMsgSrv.Text
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)

                'objstruct = New structSettings()
                'objstruct.strSettingName = "eRxProductionWebserviceURL"
                'objstruct.strSettingValue = TxtErxWebService.Text
                'objstruct.DbId = 0
                'arrSettingsCollection.Add(objstruct)
                'Directory Download 4.0 or 4.4
                If chkPharmacyType.Checked = True Then
                    objstruct = New structSettings()
                    objstruct.strSettingName = "EnableDirectory4dot4"
                    objstruct.strSettingValue = "True"
                    objstruct.DbId = 0
                    arrSettingsCollection.Add(objstruct)

                    objstruct = New structSettings()
                    objstruct.strSettingName = "eRx10dot6ProductionWebserviceURL"
                    objstruct.strSettingValue = TxtErxWebService.Text
                    objstruct.DbId = 0
                    arrSettingsCollection.Add(objstruct)

                Else
                    objstruct = New structSettings()
                    objstruct.strSettingName = "EnableDirectory4dot4"
                    objstruct.strSettingValue = "False"
                    objstruct.DbId = 0
                    arrSettingsCollection.Add(objstruct)

                    objstruct = New structSettings()
                    objstruct.strSettingName = "eRxProductionWebserviceURL"
                    objstruct.strSettingValue = TxtErxWebService.Text
                    objstruct.DbId = 0
                    arrSettingsCollection.Add(objstruct)
                End If


            End If
            ''Directory Download 4.0 or 4.4
            'If chkPharmacyType.Checked = True Then
            '    objstruct = New structSettings()
            '    objstruct.strSettingName = "EnableDirectory4dot4"
            '    objstruct.strSettingValue = "True"
            '    objstruct.DbId = 0
            '    arrSettingsCollection.Add(objstruct)
            '    If rbStagging.Checked = True Then
            '        objstruct = New structSettings()
            '        objstruct.strSettingName = "eRx10dot6StagingWebserviceURL"
            '        objstruct.strSettingValue = TxtErxWebService.Text
            '        objstruct.DbId = 0
            '        arrSettingsCollection.Add(objstruct)
            '    End If
            'Else
            '    objstruct = New structSettings()
            '    objstruct.strSettingName = "EnableDirectory4dot4"
            '    objstruct.strSettingValue = "False"
            '    objstruct.DbId = 0
            '    arrSettingsCollection.Add(objstruct)
            'End If




            mdlGeneral.gstrSQLServerName = txtServer.Text.Trim
            mdlGeneral.gstrDatabaseName = txtDatabase.Text.Trim
            mdlGeneral.gstrUserId = txtUser.Text.Trim
            mdlGeneral.gstrPassword = txtPassword.Text.Trim

            mdlGeneral.time_INTERVAL = numInterval.Value
            mdlGeneral.gnNightlyInterval = cmbPartialTime.SelectedValue
            mdlGeneral.gnFullInterval = cmbFullTime.SelectedValue
            mdlGeneral.gnEpcsFullInterval = cmdEpcsFullTime.SelectedValue
            mdlGeneral.gnFulldownloadDay = cmbFullDay.SelectedValue
            mdlGeneral.gblnStagingServer = rbStagging.Checked
            mdlGeneral.gstrEpcsAuditlogUsername = txtEpcsAuditlogUsername.Text.Trim
            mdlGeneral.gstrEpcsAuditlogPassword = txtEpcsAuditlogPassword.Text.Trim

            If rbArchive.Checked = True Then
                ' regKey.SetValue("RxSnifferLogArchive", True)
                objstruct = New structSettings()
                objstruct.strSettingName = "RxSnifferLogArchive"
                objstruct.strSettingValue = "True"
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)
                mdlGeneral.gblLogArchive = True
            Else
                'regKey.SetValue("RxSnifferLogArchive", False)
                objstruct = New structSettings()
                objstruct.strSettingName = "RxSnifferLogArchive"
                objstruct.strSettingValue = "False"
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)
                mdlGeneral.gblLogArchive = False
            End If
            objstruct = New structSettings()
            objstruct.strSettingName = "RxSnifferMaxLogSize"
            objstruct.strSettingValue = MaxLogSize.Value
            objstruct.DbId = 0
            arrSettingsCollection.Add(objstruct)

            objstruct = New structSettings()
            objstruct.strSettingName = "RxSnifferLogArchivePath"
            objstruct.strSettingValue = txtLogFilePath.Text
            objstruct.DbId = 0
            arrSettingsCollection.Add(objstruct)
            mdlGeneral.gblLogMaxSize = MaxLogSize.Value
            mdlGeneral.gblLogArchivePath = txtLogFilePath.Text

            objstruct = New structSettings()
            objstruct.strSettingName = "EpcsAuditLogPath"
            objstruct.strSettingValue = txtEpcsLogFilePath.Text
            objstruct.DbId = 0
            arrSettingsCollection.Add(objstruct)
            mdlGeneral.gblEpcsAuditLogPath = txtEpcsLogFilePath.Text



            'Directory Download 4.0 or 4.4
            If chkEnableSMDownload.Checked = True Then
                objstruct = New structSettings()
                objstruct.strSettingName = "ENABLE_SECUREMESSAGE_DOWNLOAD"
                objstruct.strSettingValue = "True"
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)

            Else
                objstruct = New structSettings()
                objstruct.strSettingName = "ENABLE_SECUREMESSAGE_DOWNLOAD"
                objstruct.strSettingValue = "False"
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)

            End If

            If ChkAutoResponseforPendingApportunity.Checked = True Then
                objstruct = New structSettings()
                objstruct.strSettingName = "EnableAutoSendForPendingOpportunity"
                objstruct.strSettingValue = "True"
                objstruct.DbId = 0
                mdlGeneral.gblnEnableAutoSendForPendingOpportunity = True
                arrSettingsCollection.Add(objstruct)
            Else
                objstruct = New structSettings()
                objstruct.strSettingName = "EnableAutoSendForPendingOpportunity"
                objstruct.strSettingValue = "False"
                objstruct.DbId = 0
                mdlGeneral.gblnEnableAutoSendForPendingOpportunity = False
                arrSettingsCollection.Add(objstruct)
            End If

            'regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, True)

            'regKey.SetValue("SQLServer", txtServer.Text.Trim)
            'regKey.SetValue("Database", txtDatabase.Text.Trim)
            'regKey.SetValue("SQLUser", txtUser.Text.Trim)

            'Dim strPassword As String
            'objEncryption = New clsEncryption
            'strPassword = objEncryption.EncryptToBase64String(txtPassword.Text.Trim, mdlGeneral.constEncryptDecryptKeyRegistry)
            'regKey.SetValue("SQLPassword", strPassword)
            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.WriteToServicesRegistry(txtServer.Text.Trim(), txtDatabase.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text)


            strgloServiceConnString = "SERVER=" & txtServer.Text.Trim() & "; DATABASE=" & txtDatabase.Text.Trim() & ";USER id=" & txtUser.Text.Trim() & "; Password=" & txtPassword.Text.Trim()

            If chkEnableEpcsLog.Checked Then
                Dim objEncryption As clsEncryption = New clsEncryption()
                Dim strEpcsAuditlogPassword As String = objEncryption.EncryptToBase64String(txtEpcsAuditlogPassword.Text.Trim, mdlGeneral.constEncryptDecryptKeyDB)
                objEncryption = Nothing
                Dim strEpcsAuditlogUsername As String = txtEpcsAuditlogUsername.Text.Trim

                objstruct = New structSettings()
                objstruct.strSettingName = "EpcsAuditlogPassword"
                objstruct.strSettingValue = strEpcsAuditlogPassword
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)
                mdlGeneral.gstrEpcsAuditlogPassword = txtEpcsAuditlogPassword.Text.Trim

                objstruct = New structSettings()
                objstruct.strSettingName = "EpcsAuditlogUsername"
                objstruct.strSettingValue = strEpcsAuditlogUsername
                objstruct.DbId = 0
                arrSettingsCollection.Add(objstruct)
                mdlGeneral.gstrEpcsAuditlogUsername = txtEpcsAuditlogUsername.Text.Trim
            End If


            saveSettingsList(arrSettingsCollection)

            'regKey.SetValue("RxSnifferMaxLogSize", MaxLogSize.Value)
            'regKey.SetValue("RxSnifferLogArchivePath", txtLogFilePath.Text)
            'mdlGeneral.gblLogMaxSize = MaxLogSize.Value
            'mdlGeneral.gblLogArchivePath = txtLogFilePath.Text
            'regKey.SetValue("ServiceInterval", numInterval.Value)
            'regKey.SetValue("FullDownloadDay", cmbFullDay.SelectedValue)
            'regKey.SetValue("FullDownloadInterval", cmbFullTime.SelectedValue)
            'regKey.SetValue("NightlyDownloadInterval", cmbPartialTime.SelectedValue)



            'If rbStagging.Checked = True Then
            '    regKey.SetValue("StagingServer", True)
            'Else
            '    regKey.SetValue("StagingServer", False)
            'End If



            'If rbStagging.Checked = False And rbProduction.Checked = False Then
            '    MessageBox.Show("Please select Surescript Server")
            '    Exit Sub
            'End If

            'If chckFullDownload.Checked = True Then
            '    regKey.SetValue("DirService", "True")
            '    mdlGeneral.gblnFullDownload = True
            'Else
            '    regKey.SetValue("DirService", "False")
            '    mdlGeneral.gblnFullDownload = False
            'End If


            'If chkEnbPharmacy.Checked = True Then
            '    regKey.SetValue("PharmacyDownload", "True")
            '    mdlGeneral.gblnPharmacyDownload = True
            'Else
            '    regKey.SetValue("PharmacyDownload", "False")
            '    mdlGeneral.gblnPharmacyDownload = False
            'End If


            'If chkBoxEnableEligibility.Checked = True Then
            '    regKey.SetValue("EnableAutoEligibility", "True")
            '    mdlGeneral.gblnEnableAutoEligibility = True
            'Else
            '    regKey.SetValue("EnableAutoEligibility", "False")
            '    mdlGeneral.gblnEnableAutoEligibility = True
            'End If

            'If chkGenrateRxEligLog.Checked = True Then
            '    regKey.SetValue("GenerateRxEligibilityLog", "True")
            '    mdlGeneral.gblnGenerateRxEligibilityLog = True
            'Else
            '    regKey.SetValue("GenerateRxEligibilityLog", "False")
            '    mdlGeneral.gblnGenerateRxEligibilityLog = False
            'End If




            'regKey.SetValue("ServiceInterval", numInterval.Value)
            'regKey.SetValue("FullDownloadDay", cmbFullDay.SelectedValue)
            'regKey.SetValue("FullDownloadInterval", cmbFullTime.SelectedValue)
            'regKey.SetValue("NightlyDownloadInterval", cmbPartialTime.SelectedValue)



            'If rbStagging.Checked = True Then
            '    regKey.SetValue("StagingServer", True)
            'Else
            '    regKey.SetValue("StagingServer", False)
            'End If



            ''Code Start-Added by kanchan on 20100511 for Log setting
            'If rbArchive.Checked = True Then
            '    regKey.SetValue("RxSnifferLogArchive", True)
            '    mdlGeneral.gblLogArchive = True
            'Else
            '    regKey.SetValue("RxSnifferLogArchive", False)
            '    mdlGeneral.gblLogArchive = False
            'End If
            'regKey.SetValue("RxSnifferMaxLogSize", MaxLogSize.Value)
            'regKey.SetValue("RxSnifferLogArchivePath", txtLogFilePath.Text)
            'mdlGeneral.gblLogMaxSize = MaxLogSize.Value
            'mdlGeneral.gblLogArchivePath = txtLogFilePath.Text
            ''Code End-Added by kanchan on 20100511 for Log setting

            'RestartService()
            Me.Close()

        Catch ex As Exception
            mdlGeneral.UpdateLog("Unable to read registry values - " & ex.ToString)

        Finally
            'regKey.Close()
            'regKey.Dispose()
            'objEncryption = Nothing
        End Try
    End Sub
    Public Sub SaveSettings(ByVal settingName As String, ByVal settingsValue As String, ByVal nDatabaseId As Long, ByVal strConnection As String)
        'Dim objSettings As New ClsSettings()
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(strConnection)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDBLayer.Connect(False)
            oDBParameters.Add("@nDBConnectionId", nDatabaseId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sSettingsName", settingName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sSettingsValue", settingsValue, ParameterDirection.Input, SqlDbType.VarChar)
            oDBLayer.Execute("INUP_ServiceSettings", oDBParameters)
        Catch ex As Exception
            UpdateLog("Error in saving the settings : " & ex.ToString())
        Finally
            If oDBLayer IsNot Nothing Then
                oDBLayer.Disconnect()
                oDBLayer.Dispose()
            End If
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
        End Try
    End Sub

    Public Sub saveSettingsList(ByVal objList As List(Of structSettings))
        Dim objSendEncryption As clsEncryption = Nothing
        Try
            For Each objstruct As structSettings In objList
                SaveSettings(objstruct.strSettingName, objstruct.strSettingValue, 0, strgloServiceConnString)
            Next
            If sTmprRxURl <> TxtErxWebService.Text Then
                MessageBox.Show("eRx web service Url will be updated in gloSuite Database. ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            If sTempSecurMsgUrl <> txtSecureMsgSrv.Text Then
                MessageBox.Show("Secure Message Url will be updated in gloSuite Database. ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            Dim i As Integer = 0

            mdlGeneral.SetDbCredentials()
            Do While (i < mdlGeneral.gblDbCredentials.Rows.Count)
                objSendEncryption = New clsEncryption
                Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(mdlGeneral.gblDbCredentials.Rows(i)("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                Dim strConnection As String = "SERVER=" + mdlGeneral.gblDbCredentials.Rows(i)("sServerName").ToString + ";DATABASE=" + mdlGeneral.gblDbCredentials.Rows(i)("sDatabaseName").ToString + ";USER id=" + mdlGeneral.gblDbCredentials.Rows(i)("sSqlUserName").ToString + ";Password=" + strDbPassword
                If rbStagging.Checked = True Then
                    'UpdateeRxWebserviceSetting(strConnection, "eRxStagingWebserviceURL")
                    If chkPharmacyType.Checked = True Then
                        UpdateeRxWebserviceSetting(strConnection, "eRx10dot6StagingWebserviceURL", TxtErxWebService.Text)
                    Else
                        UpdateeRxWebserviceSetting(strConnection, "eRxStagingWebserviceURL", TxtErxWebService.Text)
                    End If
                    '''' Update secure message staging url
                    UpdateeRxWebserviceSetting(strConnection, "SECUREMSGSTAGINGWEBSERVICEURL", txtSecureMsgSrv.Text)
                Else
                    'UpdateeRxWebserviceSetting(strConnection, "eRxProductionWebserviceURL")
                    If chkPharmacyType.Checked = True Then
                        UpdateeRxWebserviceSetting(strConnection, "eRx10dot6ProductionWebserviceURL", TxtErxWebService.Text)
                    Else
                        UpdateeRxWebserviceSetting(strConnection, "eRxProductionWebserviceURL", TxtErxWebService.Text)
                    End If
                    '''' Update secure message Production url
                    UpdateeRxWebserviceSetting(strConnection, "SECUREMSGPRODUCTIONWEBSERVICEURL", txtSecureMsgSrv.Text)
                End If
                i = (i + 1)
            Loop

        Catch ex As Exception
            UpdateLog("Error in saving the settings : " & ex.ToString())
        Finally
            If Not IsNothing(objSendEncryption) Then
                objSendEncryption = Nothing
            End If
        End Try
    End Sub
    Private Sub UpdateeRxWebserviceSetting(ByVal strConnection As String, ByVal sSettingname As String, ByVal sSettingValue As String)
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(strConnection)
        Dim dtSettingsTable As New DataTable
        Dim _strQuery As String = String.Empty
        Try

            oDbLayer.Connect(False)

            _strQuery = "Update settings Set sSettingsValue='" & sSettingValue & "'  where ssettingsname = '" & sSettingname & "'"

            oDbLayer.Execute_Query(_strQuery)
            oDbLayer.Disconnect()

        Catch ex As Exception
            UpdateLog(ex.ToString())
            dtSettingsTable = Nothing
        Finally
            If oDbLayer IsNot Nothing Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
            End If
        End Try
    End Sub


    Public Function ReadSettings(ByVal strConnection As String, Optional ByVal sSettingname As String = "") As DataTable
        'Dim objSettings As New ClsSettings()
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(strConnection)
        Dim dtSettingsTable As New DataTable
        Dim _strQuery As String = String.Empty
        Try

            oDbLayer.Connect(False)
            If sSettingname = "" Then
                _strQuery = "select sSettingsName,sSettingsValue from GLSettings where nReferenceId='0'"
            Else
                _strQuery = "select sSettingsName,sSettingsValue from GLSettings where nReferenceId='0' and sSettingsName='" & sSettingname & "'"
            End If

            oDbLayer.Retrive_Query(_strQuery, dtSettingsTable)
            oDbLayer.Disconnect()

        Catch ex As Exception
            UpdateLog(ex.ToString())
            dtSettingsTable = Nothing
        Finally
            If oDbLayer IsNot Nothing Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
            End If
        End Try
        Return dtSettingsTable
    End Function


    Private Function IsConnect(ByVal SQLServer As String, ByVal Database As String, ByVal SQLUser As String, ByVal SQLPassword As String) As Boolean
        Dim strConnectionString As String = "SERVER=" & SQLServer & ";DATABASE=" & Database & ";USER id=" & SQLUser & ";Password=" & SQLPassword
        Dim objConnection As New SqlConnection(strConnectionString)
        Try
            objConnection.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub RestartService()
        Dim mobServiceController As System.ServiceProcess.ServiceController
        mobServiceController = New System.ServiceProcess.ServiceController(gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicServiceName)
        Dim blnStop As Boolean = False
        If mobServiceController.Status = System.ServiceProcess.ServiceControllerStatus.Running Then
            If mobServiceController.CanStop = True Then
                mobServiceController.Stop()
                blnStop = True
                System.Threading.Thread.Sleep(1000)
                mobServiceController.Refresh()
            End If
        End If

        If blnStop Then
            If mobServiceController.Status = System.ServiceProcess.ServiceControllerStatus.Stopped Then
                mobServiceController.Start()

            End If
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSettings_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        sStagingURl = Nothing
        sProductionURl = Nothing
        s10dot6StagingURl = Nothing
        s10dot6ProductionURl = Nothing
        sTmprRxURl = Nothing
        sSecureMsgStagingURL = Nothing
        sSecureMsgProductionURL = Nothing
        sTempSecurMsgUrl = Nothing
    End Sub

    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtSettingstable As DataTable = Nothing
        Try
            RemoveHandler rbStagging.CheckedChanged, AddressOf rbStagging_CheckedChanged
            RemoveHandler chkPharmacyType.CheckedChanged, AddressOf chkPharmacyType_CheckedChanged


            FillDayDropdowns()
            FillFullTimeDropdowns()
            FillNightlyTimeDropdowns()
            FillEARUploadTime()
            FillFullEpcsTimeDropdowns()
            '   regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, Security.AccessControl.RegistryRights.FullControl)

            'If IsNothing(regKey) = True Then
            '    chckFullDownload.Checked = False
            '    chkEnbPharmacy.Checked = True
            '    rbStagging.Checked = True
            '    cmbFullDay.Text = "Every Monday"
            '    cmbFullTime.Text = "4.00 AM"
            '    cmbPartialTime.Text = "1.00 AM"
            '    'Code Start-Added by kanchan on 20100511 for Log setting
            '    rbArchive.Checked = True
            '    MaxLogSize.Value = 2
            '    txtLogFilePath.Text = ""
            '    'Code End-Added by kanchan on 20100511 for Log setting

            '    chkBoxEnableEligibility.Checked = False
            '    chkGenrateRxEligLog.Checked = False

            '    Exit Sub
            'End If

            'If IsNothing(regKey) = False Then
            '    regKey.Close()
            '    regKey.Dispose()
            '    regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, True)
            '    If IsNothing(regKey.GetValue("SQLServer")) = False Then
            '        txtServer.Text = regKey.GetValue("SQLServer")
            '    End If

            '    ''Uncommented by Ujwala on 19022015 to remove Hardcoding of DBs

            '    If IsNothing(regKey.GetValue("Database")) = False Then
            '        txtDatabase.Text = regKey.GetValue("Database")
            '    End If

            '    ''Uncommented by Ujwala on 19022015 to remove Hardcoding of DBs

            '    If IsNothing(regKey.GetValue("SQLUser")) = False Then
            '        txtUser.Text = regKey.GetValue("SQLUser")
            '    End If
            '    If IsNothing(regKey.GetValue("SQLPassword")) = False Then
            '        Dim strPassword As String
            '        strPassword = regKey.GetValue("SQLPassword")
            '        objEncryption = New clsEncryption
            '        txtPassword.Text = objEncryption.DecryptFromBase64String(strPassword, mdlGeneral.constEncryptDecryptKeyRegistry)
            '    End If

            Dim gstrSQLServerName As String = ""
            Dim gstrDatabaseName As String = ""
            Dim gstrUserId As String = ""
            Dim gstrPassword As String = ""

            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ReadFromServicesRegistry(gstrSQLServerName, gstrDatabaseName, gstrUserId, gstrPassword)

            txtServer.Text = gstrSQLServerName
            txtDatabase.Text = gstrDatabaseName
            txtUser.Text = gstrUserId
            txtPassword.Text = gstrPassword

            ''Added txtDatabase by Ujwala on 19022015 to remove Hardcoding of DBs

            ''dtSettingstable = ReadSettings("SERVER=" & txtServer.Text.Trim() & "; DATABASE=gloServices;USER id=" & txtUser.Text.Trim() & "; Password=" & txtPassword.Text.Trim())
            dtSettingstable = ReadSettings("SERVER=" & txtServer.Text.Trim() & "; DATABASE=" & txtDatabase.Text.Trim() & ";USER id=" & txtUser.Text.Trim() & "; Password=" & txtPassword.Text.Trim())
            ''Added txtDatabase by Ujwala on 19022015 to remove Hardcoding of DBs
            If (IsNothing(dtSettingstable) = False) Then


                ' Dim dvSetting As New DataView(dtSettingstable)
                For Each oDataRow As DataRow In dtSettingstable.Rows
                    Select Case Convert.ToString(Convert.ToString(oDataRow("sSettingsName")))
                        Case "ServiceInterval"
                            numInterval.Value = Convert.ToString(oDataRow("sSettingsValue"))
                        Case "DirService"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    chckFullDownload.Checked = True
                                Else
                                    chckFullDownload.Checked = False
                                End If
                            Else
                                chckFullDownload.Checked = False
                            End If

                        Case "PrescriberFullDownload"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    chckFullPrescriberDownload.Checked = True
                                Else
                                    chckFullPrescriberDownload.Checked = False
                                End If
                            Else
                                chckFullPrescriberDownload.Checked = False
                            End If

                        Case "PharmacyDownload"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    chkEnbPharmacy.Checked = True
                                    PnlPharmacyDownload.Enabled = True
                                Else
                                    chkEnbPharmacy.Checked = False
                                    PnlPharmacyDownload.Enabled = False
                                End If
                            Else
                                chkEnbPharmacy.Checked = True
                                PnlPharmacyDownload.Enabled = True
                            End If

                        Case "SnifferPDMPDownload"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    chkPDMPAutoDownload.Checked = True

                                Else
                                    chkPDMPAutoDownload.Checked = False

                                End If
                            Else
                                chkPDMPAutoDownload.Checked = False

                            End If

                        Case "FullDownloadDay"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                cmbFullDay.SelectedValue = Convert.ToString(oDataRow("sSettingsValue"))
                            Else
                                cmbFullDay.Text = "Every Monday"
                            End If
                        Case "FullDownloadInterval"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                cmbFullTime.SelectedValue = Convert.ToString(oDataRow("sSettingsValue"))
                            Else
                                cmbFullTime.Text = "4.00 AM"
                            End If

                        Case "NightlyDownloadInterval"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                cmbPartialTime.SelectedValue = Convert.ToString(oDataRow("sSettingsValue"))
                            Else
                                cmbPartialTime.Text = "1.00 AM"
                            End If
                        Case "StagingServer"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    rbStagging.Checked = True

                                    'dvSetting.RowFilter = "sSettingsName = 'eRxStagingWebserviceURL'"
                                    'If dvSetting.Count > 0 Then
                                    '    TxtErxWebService.Text = CType(dvSetting(0)("sSettingsValue"), String)  ''dvSetting.Item("sSettingsValue").Row(0).ToString()
                                    'End If
                                    'dvSetting.Dispose()
                                Else
                                    rbProduction.Checked = True

                                    'dvSetting.RowFilter = "sSettingsName = 'eRxProductionWebserviceURL'"
                                    'If dvSetting.Count > 0 Then
                                    '    TxtErxWebService.Text = CType(dvSetting(0)("sSettingsValue"), String)
                                    'End If
                                    'dvSetting.Dispose()
                                End If
                            Else
                                rbStagging.Checked = True
                            End If

                        Case "RxSnifferLogArchive"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    rbArchive.Checked = True
                                Else
                                    rbLogDelete.Checked = True
                                End If
                            Else
                                rbArchive.Checked = True
                            End If
                        Case "RxSnifferMaxLogSize"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                MaxLogSize.Value = Convert.ToString(oDataRow("sSettingsValue"))
                            Else
                                MaxLogSize.Value = 2
                            End If
                        Case "RxSnifferLogArchivePath"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                txtLogFilePath.Text = Convert.ToString(oDataRow("sSettingsValue"))
                            Else
                                txtLogFilePath.Text = ""
                            End If
                        Case "EnableAutoEligibility"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    chkBoxEnableEligibility.Checked = True
                                Else
                                    chkBoxEnableEligibility.Checked = False
                                End If
                            Else
                                chkBoxEnableEligibility.Checked = True
                            End If
                        Case "GenerateRxEligibilityLog"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    chkGenrateRxEligLog.Checked = True
                                Else
                                    chkGenrateRxEligLog.Checked = False
                                End If
                            Else
                                chkGenrateRxEligLog.Checked = True
                            End If


                        Case "EnableDirectory4dot4"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    chkPharmacyType.Checked = True
                                Else
                                    chkPharmacyType.Checked = False
                                End If
                            Else
                                chkPharmacyType.Checked = False
                            End If
                        Case "eRxStagingWebserviceURL"
                            sStagingURl = Convert.ToString(oDataRow("sSettingsValue"))
                        Case "eRxProductionWebserviceURL"
                            sProductionURl = Convert.ToString(oDataRow("sSettingsValue"))
                        Case "eRx10dot6StagingWebserviceURL"
                            s10dot6StagingURl = Convert.ToString(oDataRow("sSettingsValue"))
                        Case "eRx10dot6ProductionWebserviceURL"
                            s10dot6ProductionURl = Convert.ToString(oDataRow("sSettingsValue"))

                        Case "SECUREMSGSTAGINGWEBSERVICEURL"
                            sSecureMsgStagingURL = Convert.ToString(oDataRow("sSettingsValue"))
                        Case "SECUREMSGPRODUCTIONWEBSERVICEURL"
                            sSecureMsgProductionURL = Convert.ToString(oDataRow("sSettingsValue"))

                        Case "ENABLE_SECUREMESSAGE_DOWNLOAD"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    chkEnableSMDownload.Checked = True
                                    chckFullPrescriberDownload.Enabled = True
                                Else
                                    chkEnableSMDownload.Checked = False
                                    chckFullPrescriberDownload.Enabled = False
                                End If
                            Else
                                chkEnableSMDownload.Checked = False
                            End If
                        Case "EnableAutoSendForPendingOpportunity"
                            If Not IsNothing(oDataRow("sSettingsValue")) Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    ChkAutoResponseforPendingApportunity.Checked = True
                                End If
                            End If
                        Case "EpcsAuditLogPath"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                txtEpcsLogFilePath.Text = Convert.ToString(oDataRow("sSettingsValue"))
                            Else
                                txtEpcsLogFilePath.Text = ""
                            End If
                        Case "EnableEpcsAuditLog"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    chkEnableEpcsLog.Checked = True
                                Else
                                    chkEnableEpcsLog.Checked = False
                                End If
                            Else
                                chkEnableEpcsLog.Checked = True
                            End If
                        Case "EpcsAuditLogStartInterval"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                cmdEpcsFullTime.SelectedValue = Convert.ToString(oDataRow("sSettingsValue"))
                            Else
                                cmdEpcsFullTime.Text = "4.00 AM"
                            End If
                        Case "EpcsAuditlogUsername"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                txtEpcsAuditlogUsername.Text = Convert.ToString(oDataRow("sSettingsValue"))
                            Else
                                txtEpcsAuditlogUsername.Text = ""
                            End If
                        Case "EpcsAuditlogPassword"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                Dim objEncryption As clsEncryption = New clsEncryption()
                                txtEpcsAuditlogPassword.Text = objEncryption.DecryptFromBase64String(Convert.ToString(oDataRow("sSettingsValue")), mdlGeneral.constEncryptDecryptKeyDB)
                                objEncryption = Nothing
                            Else
                                txtEpcsAuditlogPassword.Text = ""
                            End If
                        Case "PDMPService"
                            If IsNothing(Convert.ToString(oDataRow("sSettingsValue"))) = False Then
                                txtPDMPServiceURL.Text = Convert.ToString(oDataRow("sSettingsValue"))
                            Else
                                txtPDMPServiceURL.Text = ""
                            End If
                    End Select
                Next
            End If
            If rbStagging.Checked = True Then
                If chkPharmacyType.Checked = True Then
                    'dvSetting.RowFilter = "sSettingsName = 'eRx10dot6StagingWebserviceURL'"
                    'If dvSetting.Count > 0 Then
                    TxtErxWebService.Text = s10dot6StagingURl  'CType(dvSetting(0)("sSettingsValue"), String)  

                    'End If
                    'dvSetting.Dispose()
                Else
                    TxtErxWebService.Text = sStagingURl
                End If
                txtSecureMsgSrv.Text = sSecureMsgStagingURL
            Else

                If chkPharmacyType.Checked = True Then
                    TxtErxWebService.Text = s10dot6ProductionURl
                Else
                    TxtErxWebService.Text = sProductionURl ''dvSetting.Item("sSettingsValue").Row(0).ToString()

                End If
                txtSecureMsgSrv.Text = sSecureMsgProductionURL
            End If
            sTmprRxURl = TxtErxWebService.Text
            sTempSecurMsgUrl = txtSecureMsgSrv.Text
            'If IsNothing(regKey.GetValue("ServiceInterval")) = False Then
            '    numInterval.Value = regKey.GetValue("ServiceInterval")
            'End If
            'If IsNothing(regKey.GetValue("DirService")) = False Then
            '    If regKey.GetValue("DirService") = "True" Then
            '        chckFullDownload.Checked = True
            '    Else
            '        chckFullDownload.Checked = False
            '    End If
            'Else
            '    chckFullDownload.Checked = False
            'End If

            'If IsNothing(regKey.GetValue("PharmacyDownload")) = False Then
            '    If regKey.GetValue("PharmacyDownload") = "True" Then
            '        chkEnbPharmacy.Checked = True
            '        PnlPharmacyDownload.Enabled = True
            '    Else
            '        chkEnbPharmacy.Checked = False
            '        PnlPharmacyDownload.Enabled = False
            '    End If
            'Else
            '    chkEnbPharmacy.Checked = True
            '    PnlPharmacyDownload.Enabled = True
            'End If



            'If IsNothing(regKey.GetValue("FullDownloadDay")) = False Then
            '    cmbFullDay.SelectedValue = regKey.GetValue("FullDownloadDay")
            'Else
            '    cmbFullDay.Text = "Every Monday"
            'End If
            'If IsNothing(regKey.GetValue("FullDownloadInterval")) = False Then
            '    cmbFullTime.SelectedValue = regKey.GetValue("FullDownloadInterval")
            'Else
            '    cmbFullTime.Text = "4.00 AM"
            'End If
            'If IsNothing(regKey.GetValue("NightlyDownloadInterval")) = False Then
            '    cmbPartialTime.SelectedValue = regKey.GetValue("NightlyDownloadInterval")
            'Else
            '    cmbPartialTime.Text = "1.00 AM"
            'End If


            'If IsNothing(regKey.GetValue("StagingServer")) = False Then
            '    If regKey.GetValue("StagingServer") = "True" Then
            '        rbStagging.Checked = True
            '    Else
            '        rbProduction.Checked = True
            '    End If
            'Else
            '    rbStagging.Checked = True
            'End If

            ''Code Start-Added by kanchan on 20100511 for Log setting
            'If IsNothing(regKey.GetValue("RxSnifferLogArchive")) = False Then
            '    If regKey.GetValue("RxSnifferLogArchive") = "True" Then
            '        rbArchive.Checked = True
            '    Else
            '        rbLogDelete.Checked = True
            '    End If
            'Else
            '    rbArchive.Checked = True
            'End If

            'If IsNothing(regKey.GetValue("RxSnifferMaxLogSize")) = False Then
            '    MaxLogSize.Value = regKey.GetValue("RxSnifferMaxLogSize")
            'Else
            '    MaxLogSize.Value = 2
            'End If

            'If IsNothing(regKey.GetValue("RxSnifferLogArchivePath")) = False Then
            '    txtLogFilePath.Text = regKey.GetValue("RxSnifferLogArchivePath")
            'Else
            '    txtLogFilePath.Text = ""
            'End If
            ''Code End-Added by kanchan on 20100511 for Log setting

            '''''setting for Enabling auto eligibility
            'If IsNothing(regKey.GetValue("EnableAutoEligibility")) = False Then
            '    If regKey.GetValue("EnableAutoEligibility") = "True" Then
            '        chkBoxEnableEligibility.Checked = True
            '    Else
            '        chkBoxEnableEligibility.Checked = False
            '    End If
            'Else
            '    chkBoxEnableEligibility.Checked = True
            'End If

            '''''setting for generate/not generate log
            'If IsNothing(regKey.GetValue("GenerateRxEligibilityLog")) = False Then
            '    If regKey.GetValue("GenerateRxEligibilityLog") = "True" Then
            '        chkGenrateRxEligLog.Checked = True
            '    Else
            '        chkGenrateRxEligLog.Checked = False
            '    End If
            'Else
            '    chkGenrateRxEligLog.Checked = True
            'End If



            'Else

            'chckFullDownload.Checked = False
            'chkEnbPharmacy.Checked = True
            'rbStagging.Checked = True
            'cmbFullDay.Text = "Every Monday"
            'cmbFullTime.Text = "4.00 AM"
            'cmbPartialTime.Text = "1.00 AM"
            ''Code Start-Added by kanchan on 20100511 for Log setting
            'rbArchive.Checked = True
            'MaxLogSize.Value = 2
            'txtLogFilePath.Text = ""
            ''Code End-Added by kanchan on 20100511 for Log setting

            'chkBoxEnableEligibility.Checked = False
            'chkGenrateRxEligLog.Checked = False

            'End If

            ''Commented by Ujwala on 19022015 to remove Hardcoding of DBs
            ''''txtDatabase.Text = "gloServices"
            ''Commented by Ujwala on 19022015 to remove Hardcoding of DBs
        Catch ex As Exception
            mdlGeneral.UpdateLog("Unable to read registry values - " & ex.ToString)

        Finally
            'If Not IsNothing(regKey) Then
            '    regKey.Close()
            '    regKey.Dispose()
            'End If
            If (IsNothing(dtSettingstable) = False) Then
                dtSettingstable.Dispose()
                dtSettingstable = Nothing
            End If
            AddHandler rbStagging.CheckedChanged, AddressOf rbStagging_CheckedChanged
            AddHandler chkPharmacyType.CheckedChanged, AddressOf chkPharmacyType_CheckedChanged
        End Try
    End Sub
    Private Sub FillDayDropdowns()
        Dim MyDT As New DataTable
        Dim MyRow As DataRow
        MyDT.Columns.Add(New DataColumn("DisplayIndex", GetType(Int32)))
        MyDT.Columns.Add(New DataColumn("Display", GetType(String)))

        MyRow = MyDT.NewRow()
        MyRow(0) = 0
        MyRow(1) = "Every Sunday"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 1
        MyRow(1) = "Every Monday"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 2
        MyRow(1) = "Every Tuesday"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 3
        MyRow(1) = "Every Wednesday"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 4
        MyRow(1) = "Every Thursday"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 5
        MyRow(1) = "Every Friday"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 6
        MyRow(1) = "Every Saturday"
        MyDT.Rows.Add(MyRow)

        MyRow = MyDT.NewRow()
        MyRow(0) = 7
        MyRow(1) = "Every Day"
        MyDT.Rows.Add(MyRow)

        cmbFullDay.DataSource = MyDT
        cmbFullDay.DisplayMember = "Display"
        cmbFullDay.ValueMember = "DisplayIndex"

    End Sub

    Private Sub FillFullEpcsTimeDropdowns()
        Dim MyIntervals As New DataTable
        Dim MyRow As DataRow
        MyIntervals.Columns.Add(New DataColumn("DisplayIndex", GetType(Int32)))
        MyIntervals.Columns.Add(New DataColumn("Display", GetType(String)))

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 0
        MyRow(1) = "12.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 1
        MyRow(1) = "1.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 2
        MyRow(1) = "2.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 3
        MyRow(1) = "3.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 4
        MyRow(1) = "4.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 5
        MyRow(1) = "5.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 6
        MyRow(1) = "6.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 7
        MyRow(1) = "7.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 8
        MyRow(1) = "8.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 9
        MyRow(1) = "9.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 10
        MyRow(1) = "10.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 11
        MyRow(1) = "11.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 12
        MyRow(1) = "12.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 13
        MyRow(1) = "1.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 14
        MyRow(1) = "2.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 15
        MyRow(1) = "3.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 16
        MyRow(1) = "4.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 17
        MyRow(1) = "5.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 18
        MyRow(1) = "6.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 19
        MyRow(1) = "7.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 20
        MyRow(1) = "8.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 21
        MyRow(1) = "9.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 22
        MyRow(1) = "10.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 23
        MyRow(1) = "11.00 PM"
        MyIntervals.Rows.Add(MyRow)

        cmdEpcsFullTime.DataSource = MyIntervals
        cmdEpcsFullTime.DisplayMember = "Display"
        cmdEpcsFullTime.ValueMember = "DisplayIndex"

    End Sub
    Private Sub FillFullTimeDropdowns()
        Dim MyIntervals As New DataTable
        Dim MyRow As DataRow
        MyIntervals.Columns.Add(New DataColumn("DisplayIndex", GetType(Int32)))
        MyIntervals.Columns.Add(New DataColumn("Display", GetType(String)))

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 0
        MyRow(1) = "12.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 1
        MyRow(1) = "1.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 2
        MyRow(1) = "2.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 3
        MyRow(1) = "3.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 4
        MyRow(1) = "4.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 5
        MyRow(1) = "5.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 6
        MyRow(1) = "6.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 7
        MyRow(1) = "7.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 8
        MyRow(1) = "8.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 9
        MyRow(1) = "9.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 10
        MyRow(1) = "10.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 11
        MyRow(1) = "11.00 AM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 12
        MyRow(1) = "12.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 13
        MyRow(1) = "1.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 14
        MyRow(1) = "2.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 15
        MyRow(1) = "3.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 16
        MyRow(1) = "4.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 17
        MyRow(1) = "5.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 18
        MyRow(1) = "6.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 19
        MyRow(1) = "7.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 20
        MyRow(1) = "8.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 21
        MyRow(1) = "9.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 22
        MyRow(1) = "10.00 PM"
        MyIntervals.Rows.Add(MyRow)

        MyRow = MyIntervals.NewRow()
        MyRow(0) = 23
        MyRow(1) = "11.00 PM"
        MyIntervals.Rows.Add(MyRow)

        cmbFullTime.DataSource = MyIntervals
        cmbFullTime.DisplayMember = "Display"
        cmbFullTime.ValueMember = "DisplayIndex"

    End Sub

    Private Sub FillNightlyTimeDropdowns()
        Dim MyNighltyIntervals As New DataTable
        Dim MyRow As DataRow
        MyNighltyIntervals.Columns.Add(New DataColumn("DisplayIndex", GetType(Int32)))
        MyNighltyIntervals.Columns.Add(New DataColumn("Display", GetType(String)))

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 0
        MyRow(1) = "12.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 1
        MyRow(1) = "1.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 2
        MyRow(1) = "2.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 3
        MyRow(1) = "3.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 4
        MyRow(1) = "4.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 5
        MyRow(1) = "5.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 6
        MyRow(1) = "6.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 7
        MyRow(1) = "7.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 8
        MyRow(1) = "8.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 9
        MyRow(1) = "9.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 10
        MyRow(1) = "10.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 11
        MyRow(1) = "11.00 AM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 12
        MyRow(1) = "12.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 13
        MyRow(1) = "1.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 14
        MyRow(1) = "2.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 15
        MyRow(1) = "3.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 16
        MyRow(1) = "4.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 17
        MyRow(1) = "5.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 18
        MyRow(1) = "6.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 19
        MyRow(1) = "7.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 20
        MyRow(1) = "8.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 21
        MyRow(1) = "9.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 22
        MyRow(1) = "10.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        MyRow = MyNighltyIntervals.NewRow()
        MyRow(0) = 23
        MyRow(1) = "11.00 PM"
        MyNighltyIntervals.Rows.Add(MyRow)

        cmbPartialTime.DataSource = MyNighltyIntervals
        cmbPartialTime.DisplayMember = "Display"
        cmbPartialTime.ValueMember = "DisplayIndex"

    End Sub


    Private Sub FillEARUploadTime()
        Dim MyEARUploadTime As New DataTable
        Dim MyRow As DataRow
        MyEARUploadTime.Columns.Add(New DataColumn("DisplayIndex", GetType(Int32)))
        MyEARUploadTime.Columns.Add(New DataColumn("Display", GetType(String)))

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 0
        MyRow(1) = "12.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 1
        MyRow(1) = "1.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 2
        MyRow(1) = "2.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 3
        MyRow(1) = "3.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 4
        MyRow(1) = "4.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 5
        MyRow(1) = "5.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 6
        MyRow(1) = "6.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 7
        MyRow(1) = "7.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 8
        MyRow(1) = "8.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 9
        MyRow(1) = "9.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 10
        MyRow(1) = "10.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 11
        MyRow(1) = "11.00 AM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 12
        MyRow(1) = "12.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 13
        MyRow(1) = "1.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 14
        MyRow(1) = "2.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 15
        MyRow(1) = "3.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 16
        MyRow(1) = "4.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 17
        MyRow(1) = "5.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 18
        MyRow(1) = "6.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 19
        MyRow(1) = "7.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 20
        MyRow(1) = "8.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 21
        MyRow(1) = "9.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 22
        MyRow(1) = "10.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)

        MyRow = MyEARUploadTime.NewRow()
        MyRow(0) = 23
        MyRow(1) = "11.00 PM"
        MyEARUploadTime.Rows.Add(MyRow)


    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkEnbPharmacy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnbPharmacy.CheckedChanged
        If (chkEnbPharmacy.Checked = True) Then
            PnlPharmacyDownload.Enabled = True
        Else
            PnlPharmacyDownload.Enabled = False
        End If
    End Sub


    'Code Start-Added by kanchan on 20100511 for Log setting
    Private Sub btnLogFilePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogFilePath.Click
        Try
            'code written from "http://stackoverflow.com/questions/6860153/exception-when-using-folderbrowserdialog"
            Dim selectedPath As String = ""
            Dim t = New Thread(DirectCast(Function()
                                              Dim fbd As New FolderBrowserDialog()
                                              fbd.RootFolder = System.Environment.SpecialFolder.MyComputer
                                              fbd.ShowNewFolderButton = True
                                              If fbd.ShowDialog() = DialogResult.Cancel Then
                                                  fbd.Dispose()
                                                  fbd = Nothing
                                                  Return Nothing
                                              End If

                                              selectedPath = fbd.SelectedPath
                                              fbd.Dispose()
                                              fbd = Nothing
                                              Return selectedPath
                                          End Function, ThreadStart))

            t.SetApartmentState(ApartmentState.STA)
            t.Start()
            t.Join()
            If selectedPath <> "" Then
                txtLogFilePath.Text = selectedPath
            End If


        Catch ex As Exception
            UpdateLog("Error Selecting Log File Path Sttings *****" & ex.ToString())

        End Try
    End Sub
    'Code End-Added by kanchan on 20100511 for Log setting

    'Code Start-Added by kanchan on 20100511 for Log setting
    Private Sub rbArchive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbArchive.CheckedChanged
        If rbArchive.Checked = False Then
            rbArchive.Font = New Font("Tahoma", 9, FontStyle.Bold)
            btnLogFilePath.Enabled = False
            txtLogFilePath.Enabled = False
        Else
            rbArchive.Font = New Font("Tahoma", 9, FontStyle.Regular)
            btnLogFilePath.Enabled = True
            txtLogFilePath.Enabled = True
        End If
    End Sub
    'Code End-Added by kanchan on 20100511 for Log setting

    Private Sub rbStagging_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbStagging.CheckedChanged
        Dim dtErxSetting As New DataTable
        Try

            'If rbStagging.Checked = True Then
            '    dtErxSetting = ReadSettings("SERVER=" & txtServer.Text.Trim() & "; DATABASE=gloServices;USER id=" & txtUser.Text.Trim() & "; Password=" & txtPassword.Text.Trim(), "eRxStagingWebserviceURL")
            '    If dtErxSetting.Rows.Count > 0 Then
            '        TxtErxWebService.Text = dtErxSetting.Rows(0)("sSettingsValue").ToString()
            '    Else
            '        TxtErxWebService.Text = ""
            '    End If
            'Else
            '    dtErxSetting = ReadSettings("SERVER=" & txtServer.Text.Trim() & "; DATABASE=gloServices;USER id=" & txtUser.Text.Trim() & "; Password=" & txtPassword.Text.Trim(), "eRxProductionWebserviceURL")
            '    If dtErxSetting.Rows.Count > 0 Then
            '        TxtErxWebService.Text = dtErxSetting.Rows(0)("sSettingsValue").ToString()
            '    Else
            '        TxtErxWebService.Text = ""
            '    End If

            'End If

            If rbStagging.Checked = True Then
                If chkPharmacyType.Checked = True Then
                    TxtErxWebService.Text = s10dot6StagingURl
                Else
                    TxtErxWebService.Text = sStagingURl
                End If
                txtSecureMsgSrv.Text = sSecureMsgStagingURL
            Else

                If chkPharmacyType.Checked = True Then
                    TxtErxWebService.Text = s10dot6ProductionURl
                Else
                    TxtErxWebService.Text = sProductionURl
                End If
                txtSecureMsgSrv.Text = sSecureMsgProductionURL
            End If



        Catch ex As Exception
            UpdateLog("Error in Getting eRx setting : " & ex.ToString())
        Finally
            If Not IsNothing(dtErxSetting) Then
                dtErxSetting.Dispose()
                dtErxSetting = Nothing
            End If
        End Try
    End Sub

    Private Sub chkPharmacyType_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkPharmacyType.CheckedChanged
        If rbStagging.Checked = True Then
            If chkPharmacyType.Checked = True Then
                chkPharmacyType.Font = New Font("Tahoma", 9, FontStyle.Bold)
                TxtErxWebService.Text = s10dot6StagingURl
            Else
                chkPharmacyType.Font = New Font("Tahoma", 9, FontStyle.Regular)
                TxtErxWebService.Text = sStagingURl
            End If

        Else

            If chkPharmacyType.Checked = True Then
                TxtErxWebService.Text = s10dot6ProductionURl
            Else
                TxtErxWebService.Text = sProductionURl
            End If

        End If
    End Sub


    Private Sub chkEnableSMDownload_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEnableSMDownload.CheckedChanged
        If chkEnableSMDownload.Checked = True Then
            chckFullPrescriberDownload.Enabled = True
        Else
            chckFullPrescriberDownload.Enabled = False
        End If
    End Sub


    Private Sub ChkAutoResponseforPendingApportunity_CheckedChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub rbProduction_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbProduction.CheckedChanged
        If rbProduction.Checked = True Then
            rbProduction.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbProduction.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbLogDelete_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbLogDelete.CheckedChanged
        If rbLogDelete.Checked = True Then
            rbLogDelete.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbLogDelete.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub btnLogFilePath_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnLogFilePath.MouseHover
        btnLogFilePath.BackgroundImage = Global.RxSniffer.My.Resources.Img_LongYellow
        btnLogFilePath.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnLogFilePath_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnLogFilePath.MouseLeave
        btnLogFilePath.BackgroundImage = Global.RxSniffer.My.Resources.Img_LongButton
        btnLogFilePath.BackgroundImageLayout = ImageLayout.Stretch
    End Sub




    Private Sub btnEpcsLogFilePath_Click(sender As System.Object, e As System.EventArgs) Handles btnEpcsLogFilePath.Click
        Try
            Dim selectedPath As String = ""
            Dim t = New Thread(DirectCast(Function()

                                              Dim fbd As New FolderBrowserDialog()
                                              fbd.ShowNewFolderButton = True
                                              If fbd.ShowDialog() = DialogResult.Cancel Then
                                                  fbd.Dispose()
                                                  fbd = Nothing
                                                  Return Nothing
                                              End If

                                              selectedPath = fbd.SelectedPath
                                              fbd.Dispose()
                                              fbd = Nothing
                                              Return selectedPath
                                          End Function, ThreadStart))

            t.SetApartmentState(ApartmentState.STA)
            t.Start()
            t.Join()
            If selectedPath <> "" Then
                txtEpcsLogFilePath.Text = selectedPath
            End If
        Catch ex As Exception
            UpdateLog("Error Selecting Log File Path Sttings *****" & ex.ToString())
        End Try
    End Sub

    Private Sub btnEpcsLogFilePath_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnEpcsLogFilePath.MouseHover
        btnEpcsLogFilePath.BackgroundImage = Global.RxSniffer.My.Resources.Img_LongYellow
        btnEpcsLogFilePath.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnEpcsLogFilePath_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnEpcsLogFilePath.MouseLeave
        btnEpcsLogFilePath.BackgroundImage = Global.RxSniffer.My.Resources.Img_LongButton
        btnEpcsLogFilePath.BackgroundImageLayout = ImageLayout.Stretch
    End Sub
End Class
