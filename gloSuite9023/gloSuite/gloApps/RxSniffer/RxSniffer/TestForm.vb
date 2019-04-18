Imports Microsoft.Win32
Imports RxSniffer.RxGeneral
Imports System.Threading
Imports System.Data.SqlClient
Imports System.Exception

Public Class TestForm
    '  Dim regKey As RegistryKey
    ' Dim objEncryption As clsEncryption
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim oSniffer As clsRxSniffer
        Try
            Dim oUTCTime As DateTime
            Dim oESTTime As DateTime
            Dim localZone As TimeZone = TimeZone.CurrentTimeZone
            oUTCTime = localZone.ToUniversalTime(DateTime.Now)
            oESTTime = oUTCTime.Subtract(New TimeSpan(5, 0, 0))

            IsSettings()
            'RxWinService.openSettings()
            Dim CurrentTimeZone As DateTime = DateTime.Now
            oSniffer = New clsRxSniffer
            oSniffer.GenerateEpcsAuditLog(CurrentTimeZone)
            oSniffer.AutoSendPendingOpportunity()
            'Dim ofrm As New frmSettings()
            'ofrm.ShowDialog()
            oSniffer.RetrieveSecureMessages()

            oSniffer.RetrieveRxResponses()

            oSniffer.SendPortalQueuedMessages()

            mdlGeneral.UpdateLog("Processing formulary eligibility request STARTED.........")
            oSniffer.ProcessAutoEligiblity()
            mdlGeneral.UpdateLog("Processing formulary eligibility request COMPLETED.........")


            'If mdlGeneral.gblnStagingServer Then
            '    oSniffer.RetrieveRxResponses()
            'Else
            '    oSniffer.RetrieveRxResponsesFromProduction()
            'End If
            'oSniffer.GetPharmacyDataFromProduction(enumDownloadType.Full)

            'If oSniffer.IsInternetConnectionAvailable() Then
            '    oSniffer.SendeRx()
            'End If

            'Else
            'oSniffer.RetrieveRxResponsesFromProduction()
            'End If


        Catch ex As Exception
            mdlGeneral.UpdateLog("Unable to download - " & ex.ToString)
        Finally

        End Try
    End Sub


    'Private Function IsSettings() As Boolean

    '    Try
    '        If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloServices", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, Security.AccessControl.RegistryRights.FullControl)) = True Then
    '            Return False
    '        End If
    '        regKey = Registry.LocalMachine.OpenSubKey("Software\gloServices", True)
    '        If IsNothing(regKey.GetValue("SQLServer")) = True Then
    '            Return False
    '        End If
    '        If IsNothing(regKey.GetValue("Database")) = True Then
    '            Return False
    '        End If
    '        If IsNothing(regKey.GetValue("SQLUser")) = True Then
    '            Return False
    '        End If
    '        If IsNothing(regKey.GetValue("SQLPassword")) = True Then
    '            Return False
    '        End If
    '        If IsNothing(regKey.GetValue("ServiceInterval")) = True Then
    '            Return False
    '        End If
    '        If IsNothing(regKey.GetValue("DirService")) = False Then
    '            If regKey.GetValue("DirService") = "True" Then
    '                mdlGeneral.gblnFullDownload = True
    '            End If
    '        End If

    '        If IsNothing(regKey.GetValue("FullDownloadDay")) = False Then
    '            mdlGeneral.gnFulldownloadDay = regKey.GetValue("FullDownloadDay")
    '        End If

    '        If IsNothing(regKey.GetValue("FullDownloadInterval")) = False Then
    '            mdlGeneral.gnFullInterval = regKey.GetValue("FullDownloadInterval")
    '        End If

    '        If IsNothing(regKey.GetValue("NightlyDownloadInterval")) = False Then
    '            mdlGeneral.gnNightlyInterval = regKey.GetValue("NightlyDownloadInterval")
    '        End If



    '        If IsNothing(regKey.GetValue("StagingServer")) = False Then
    '            mdlGeneral.gblnStagingServer = regKey.GetValue("StagingServer")
    '        End If

    '        '''''setting for Enabling auto eligibility
    '        If IsNothing(regKey.GetValue("EnableAutoEligibility")) = False Then
    '            If regKey.GetValue("EnableAutoEligibility") = "True" Then
    '                mdlGeneral.gblnEnableAutoEligibility = True
    '            Else
    '                mdlGeneral.gblnEnableAutoEligibility = False
    '            End If
    '        Else
    '            mdlGeneral.gblnEnableAutoEligibility = False
    '        End If

    '        ''''setting for generate/not generate log
    '        If IsNothing(regKey.GetValue("GenerateRxEligibilityLog")) = False Then
    '            If regKey.GetValue("GenerateRxEligibilityLog") = "True" Then
    '                mdlGeneral.gblnGenerateRxEligibilityLog = True
    '            Else
    '                mdlGeneral.gblnGenerateRxEligibilityLog = False
    '            End If
    '        Else
    '            mdlGeneral.gblnGenerateRxEligibilityLog = False
    '        End If



    '        mdlGeneral.gstrSQLServerName = regKey.GetValue("SQLServer")
    '        mdlGeneral.gstrDatabaseName = regKey.GetValue("Database")
    '        mdlGeneral.gstrUserId = regKey.GetValue("SQLUser")
    '        Dim strPassword As String
    '        strPassword = regKey.GetValue("SQLPassword")

    '        objEncryption = New clsEncryption
    '        mdlGeneral.gstrPassword = objEncryption.DecryptFromBase64String(strPassword, mdlGeneral.constEncryptDecryptKeyRegistry)
    '        'mdlGeneral.gstrPassword = regKey.GetValue("SQLPassword")
    '        mdlGeneral.time_INTERVAL = regKey.GetValue("ServiceInterval")
    '        'mdlGeneral.gblnFullDownload = regKey.GetValue("DirService")
    '        'mdlGeneral.UpdateLog("Successfully read registry values")



    '        If IsNothing(regKey.GetValue("StagingServer")) = False Then
    '            If regKey.GetValue("StagingServer") = "True" Then
    '                mdlGeneral.gblnStagingServer = True
    '            Else
    '                mdlGeneral.gblnStagingServer = False
    '            End If
    '        Else
    '            mdlGeneral.gblnStagingServer = False
    '        End If


    '        Return True
    '    Catch ex As Exception
    '        mdlGeneral.UpdateLog("Unable to read registry values - " & ex.ToString)
    '        Return False
    '    Finally

    '        regKey.Close()
    '    End Try
    'End Function


    Public Function IsSettings() As Boolean
        Dim strConnectionString As String = String.Empty
        Dim dtSettings As DataTable = Nothing
        Try
            'regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, Security.AccessControl.RegistryRights.FullControl)
            'If IsNothing(regKey) = True Then
            '    Return False
            'End If
            'If (IsNothing(regKey) = False) Then
            '    regKey.Close()
            '    regKey.Dispose()
            'End If
            'regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, True)
            'If IsNothing(regKey.GetValue("SQLServer")) = True Then
            '    Return False
            'End If
            'If IsNothing(regKey.GetValue("Database")) = True Then
            '    Return False
            'End If
            'If IsNothing(regKey.GetValue("SQLUser")) = True Then
            '    Return False
            'End If
            'If IsNothing(regKey.GetValue("SQLPassword")) = True Then
            '    Return False
            'End If
            'objEncryption = New clsEncryption

            'strConnectionString = "SERVER=" & Convert.ToString(regKey.GetValue("SQLServer")) & "; DATABASE= " & Convert.ToString(regKey.GetValue("Database")) & " ;USER id=" & Convert.ToString(regKey.GetValue("SQLUser")) & "; Password=" & objEncryption.DecryptFromBase64String(Convert.ToString(regKey.GetValue("SQLPassword")), mdlGeneral.constEncryptDecryptKeyRegistry)
            Dim gstrSQLServerName As String = ""
            Dim gstrDatabaseName As String = ""
            Dim gstrUserId As String = ""
            Dim gstrPassword As String = ""

            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ReadFromServicesRegistry(gstrSQLServerName, gstrDatabaseName, gstrUserId, gstrPassword)

            If (String.IsNullOrEmpty(gstrSQLServerName) OrElse String.IsNullOrEmpty(gstrDatabaseName) OrElse String.IsNullOrEmpty(gstrUserId)) Then
                Return False
            End If
            strConnectionString = "SERVER=" & gstrSQLServerName & "; DATABASE= " & gstrDatabaseName & " ;USER id=" & gstrUserId & "; Password=" & gstrPassword
            dtSettings = mdlGeneral.ReadSettings(strConnectionString)
            If Not IsNothing(dtSettings) Then
                For Each oDataRow As DataRow In dtSettings.Rows
                    Select Case (Convert.ToString(oDataRow("sSettingsName")).ToLower())
                        Case "serviceinterval"
                            If IsNothing(oDataRow("sSettingsValue")) = True Then
                                Return False
                            Else
                                mdlGeneral.time_INTERVAL = Convert.ToInt64(oDataRow("sSettingsValue"))
                            End If
                        Case "dirservice"
                            If IsNothing(oDataRow("sSettingsValue")) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    mdlGeneral.gblnFullDownload = True
                                Else
                                    mdlGeneral.gblnFullDownload = False
                                End If
                            End If
                        Case "fulldownloadday"
                            If IsNothing(oDataRow("sSettingsValue")) = False Then
                                mdlGeneral.gnFulldownloadDay = Convert.ToInt32(oDataRow("sSettingsValue"))
                            End If
                        Case "fulldownloadinterval"
                            mdlGeneral.gnFullInterval = Convert.ToInt32(oDataRow("sSettingsValue"))
                        Case "nightlydownloadinterval"
                            mdlGeneral.gnNightlyInterval = Convert.ToInt32(oDataRow("sSettingsValue"))
                        Case "stagingserver"
                            mdlGeneral.gblnStagingServer = Convert.ToBoolean(oDataRow("sSettingsValue"))
                        Case "rxsnifferlogarchive"
                            If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                mdlGeneral.gblLogArchive = True
                            Else
                                mdlGeneral.gblLogArchive = False
                            End If
                        Case "rxsniffermaxlogsize"
                            mdlGeneral.gblLogMaxSize = Convert.ToInt32(oDataRow("sSettingsValue"))
                        Case "rxsnifferlogarchivepath"
                            mdlGeneral.gblLogArchivePath = Convert.ToString(oDataRow("sSettingsValue"))

                        Case "enableautoeligibility"
                            If IsNothing(oDataRow("sSettingsValue")) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    mdlGeneral.gblnEnableAutoEligibility = True
                                Else
                                    mdlGeneral.gblnEnableAutoEligibility = False
                                End If
                            Else
                                mdlGeneral.gblnEnableAutoEligibility = False
                            End If
                        Case "generaterxeligibilitylog"
                            ''''setting for generate/not generate log
                            If IsNothing(oDataRow("sSettingsValue")) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    mdlGeneral.gblnGenerateRxEligibilityLog = True
                                Else
                                    mdlGeneral.gblnGenerateRxEligibilityLog = False
                                End If
                            Else
                                mdlGeneral.gblnGenerateRxEligibilityLog = False
                            End If
                        Case "EnableAutoSendForPendingOpportunity"
                            If IsNothing(oDataRow("sSettingsValue")) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    mdlGeneral.gblnEnableAutoSendForPendingOpportunity = True
                                Else
                                    mdlGeneral.gblnEnableAutoSendForPendingOpportunity = False
                                End If
                            Else
                                mdlGeneral.gblnEnableAutoSendForPendingOpportunity = False
                            End If
                    End Select
                Next
            Else
                Return False
            End If
            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ReadFromServicesRegistry(gstrSQLServerName, gstrDatabaseName, gstrUserId, gstrPassword)

            mdlGeneral.gstrSQLServerName = gstrSQLServerName
            mdlGeneral.gstrDatabaseName = gstrDatabaseName
            mdlGeneral.gstrUserId = gstrUserId
            mdlGeneral.gstrPassword = gstrPassword

            'mdlGeneral.gstrSQLServerName = regKey.GetValue("SQLServer")
            'mdlGeneral.gstrDatabaseName = regKey.GetValue("Database")
            'mdlGeneral.gstrUserId = regKey.GetValue("SQLUser")
            'Dim strPassword As String
            'strPassword = regKey.GetValue("SQLPassword")

            'objEncryption = New clsEncryption
            'mdlGeneral.gstrPassword = objEncryption.DecryptFromBase64String(strPassword, mdlGeneral.constEncryptDecryptKeyRegistry)
            ''If IsNothing(regKey.GetValue("ServiceInterval")) = True Then
            '    Return False
            'End If
            'If IsNothing(regKey.GetValue("DirService")) = False Then
            '    If regKey.GetValue("DirService") = "True" Then
            '        mdlGeneral.gblnFullDownload = True
            '        else
            '    End If
            'End If

            'If IsNothing(regKey.GetValue("FullDownloadDay")) = False Then
            '    mdlGeneral.gnFulldownloadDay = regKey.GetValue("FullDownloadDay")
            'End If

            'If IsNothing(regKey.GetValue("FullDownloadInterval")) = False Then
            '    mdlGeneral.gnFullInterval = regKey.GetValue("FullDownloadInterval")
            'End If

            'If IsNothing(regKey.GetValue("NightlyDownloadInterval")) = False Then
            '    mdlGeneral.gnNightlyInterval = regKey.GetValue("NightlyDownloadInterval")
            'End If



            'If IsNothing(regKey.GetValue("StagingServer")) = False Then
            '    mdlGeneral.gblnStagingServer = regKey.GetValue("StagingServer")
            'End If



            'mdlGeneral.gstrPassword = regKey.GetValue("SQLPassword")
            'mdlGeneral.time_INTERVAL = regKey.GetValue("ServiceInterval")
            'mdlGeneral.gblnFullDownload = regKey.GetValue("DirService")
            'mdlGeneral.UpdateLog("Successfully read registry values")

            'Code Start-Added by kanchan on 20100511 for Log setting
            'If IsNothing(regKey.GetValue("RxSnifferLogArchive")) = False Then
            '    If regKey.GetValue("RxSnifferLogArchive") = "True" Then
            '        mdlGeneral.gblLogArchive = True
            '    Else
            '        mdlGeneral.gblLogArchive = False
            '    End If
            'Else
            '    mdlGeneral.gblLogArchive = True
            'End If

            'If IsNothing(regKey.GetValue("RxSnifferMaxLogSize")) = False Then
            '    mdlGeneral.gblLogMaxSize = regKey.GetValue("RxSnifferMaxLogSize")
            'Else
            '    mdlGeneral.gblLogMaxSize = 2
            'End If

            'If IsNothing(regKey.GetValue("RxSnifferLogArchivePath")) = False Then
            '    mdlGeneral.gblLogArchivePath = regKey.GetValue("RxSnifferLogArchivePath")
            'Else
            '    mdlGeneral.gblLogArchivePath = ""
            'End If
            ''Code End-Added by kanchan on 20100511 for Log setting



            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Unable to read registry values - " & ex.ToString)
            Return False
        Finally
            '    regKey.Close()
            If (IsNothing(dtSettings) = False) Then
                dtSettings.Dispose()
                dtSettings = Nothing
            End If
        End Try
    End Function

    Public Function GetSurescriptSettings()
        Dim conn As New SqlConnection(mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        'Dim oDataReader As SqlDataReader
        'gintNoOfAttempts
        Try
            _strSQL = "select sSettingsValue from Settings where sSettingsName = 'StagingServer'"
            conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            mdlGeneral.gblnStagingServer = cmd.ExecuteScalar
            conn.Close()
            conn.Dispose()
            cmd.Parameters.Clear()
            cmd.Dispose()
        Catch ex As Exception
            mdlGeneral.UpdateLog("Unable to read surescript settings - " & ex.ToString)
            ' MsgBox(ex.Message)
            'MessageBox.Show("Error while retrieving Settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Return Nothing
    End Function
End Class