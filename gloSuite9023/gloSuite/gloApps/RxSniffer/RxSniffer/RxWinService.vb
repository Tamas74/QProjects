Imports Microsoft.Win32
Imports RxSniffer.RxGeneral
Imports System.Threading
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports RxSniffer.gloSecureMessage
Imports System.Windows.Forms
Imports System.Linq

Public Class RxWinService
    Private WithEvents RxResponseTimer As System.Timers.Timer
    ''Added this timer for gloVaultTimer.
    'Private WithEvents gloVaultTimer As System.Timers.Timer
    'Private WithEvents eRxTimer As System.Timers.Timer
    'Private WithEvents PharmacyTimer As System.Timers.Timer
    ' Private WithEvents oTimer As System.Threading.Timer
    Private WithEvents gloAutoEligTimer As System.Timers.Timer

    'Added by madan for generating key id for glovault
    'Dim sKey As String = String.Empty
    'Dim dtLastAccessTime As DateTime = DateTime.Now
    'End madan ,,


    Dim oUTCTime As DateTime
    Dim oESTTime As DateTime
    Dim localZone As TimeZone = TimeZone.CurrentTimeZone
    ' Dim regKey As RegistryKey
    'Dim objEncryption As clsEncryption
    ''Added by madan.
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    Dim strZipSourceDirectory As String

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        mdlGeneral.UpdateLog("RxSniffer Service was started")
        'If mdlGeneral.checkSqlServerStarted() Then
        If IsSettings() Then


            Dim oProcess As Process = Process.GetCurrentProcess()
            Dim nProcessID As Integer = oProcess.Id


            strZipSourceDirectory = gloSettings.FolderSettings.AppTempFolderPath & nProcessID & "\IHE_XDM"

            If Directory.Exists(strZipSourceDirectory) Then
                Directory.Delete(strZipSourceDirectory, True)
            End If

            Directory.CreateDirectory(strZipSourceDirectory)

            'Bug #93312: 00001073: Rxsniffer Service 
            ' Giving exception if the file is already present at location and causing service getting stopped.
            ' mostly happens when machine restarts
            Dim strCCDCssFilePath = Application.StartupPath & "\gloCCDAcss_MU2.xsl"
            File.Copy(strCCDCssFilePath, strZipSourceDirectory & "\gloCCDAcss_MU2.xsl", True)

            Dim strIndexFilePath = Application.StartupPath & "\index.htm"
            File.Copy(strIndexFilePath, gloSettings.FolderSettings.AppTempFolderPath & nProcessID & "\index.htm", True)

            Dim strReadmeFilePath = Application.StartupPath & "\Readme.txt"
            File.Copy(strReadmeFilePath, gloSettings.FolderSettings.AppTempFolderPath & nProcessID & "\Readme.txt", True)

            'Timer to download RxResponces.
            RxResponseTimer = New System.Timers.Timer
            AddHandler RxResponseTimer.Elapsed, AddressOf myTimer_Elapsed
            RxResponseTimer.AutoReset = True
            RxResponseTimer.Interval = mdlGeneral.time_INTERVAL * 1000
            RxResponseTimer.Start()

        Else
            'mdlGeneral.UpdateLog("Unable to read registry values")
        End If
        'End If

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        If Not IsNothing(RxResponseTimer) Then
            RxResponseTimer.Stop()
        End If
        ' PharmacyTimer.Stop()

        'Delete Folders Created as Per Process ID
        If Directory.Exists(strZipSourceDirectory.Replace("\IHE_XDM", "")) Then
            Directory.Delete(strZipSourceDirectory.Replace("\IHE_XDM", ""), True)
        End If
        mdlGeneral.UpdateLog("RxSniffer Service was stopped")
    End Sub
    'Rx-Sniffer Killing case
    Public Function IsSettings() As Boolean
        Dim strConnectionString As String = String.Empty
        Dim dtSettings As DataTable = Nothing

        Try

            Dim gstrSQLServerName As String = ""
            Dim gstrDatabaseName As String = ""
            Dim gstrUserId As String = ""
            Dim gstrPassword As String = ""

            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ReadFromServicesRegistry(gstrSQLServerName, gstrDatabaseName, gstrUserId, gstrPassword)


            strConnectionString = "SERVER=" & gstrSQLServerName & "; DATABASE= " & gstrDatabaseName & " ;USER id=" & gstrUserId & "; Password=" & gstrPassword
            dtSettings = mdlGeneral.ReadSettings(strConnectionString)

            If Not IsNothing(dtSettings) Then
                gblDBCount = mdlGeneral.getDBCount(strConnectionString)
            Else
                gblDBCount = 0
            End If

            If Not IsNothing(dtSettings) Then
                For Each oDataRow As DataRow In dtSettings.Rows
                    Select Case (Convert.ToString(oDataRow("sSettingsName")).ToLower())
                        Case "serviceinterval"
                            If IsNothing(oDataRow("sSettingsValue")) = True Then
                                Return False
                            Else
                                mdlGeneral.time_INTERVAL = Convert.ToInt64(oDataRow("sSettingsValue"))
                            End If
                            'For resolving case no :GLO2011-0010737 i.e Conflicts in Contacts_MST
                        Case "pharmacydownload"
                            If IsNothing(oDataRow("sSettingsValue")) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    mdlGeneral.gblnPharmacyDownload = True
                                Else
                                    mdlGeneral.gblnPharmacyDownload = False
                                End If
                            End If
                        Case "prescriberfulldownload"
                            If IsNothing(oDataRow("sSettingsValue")) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    mdlGeneral.gblnFullPrescriberDownload = True
                                Else
                                    mdlGeneral.gblnFullPrescriberDownload = False
                                End If
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
                        Case "epcsauditlogpath"
                            mdlGeneral.gblEpcsAuditLogPath = Convert.ToString(oDataRow("sSettingsValue"))
                        Case "epcsauditlogstartinterval"
                            mdlGeneral.gnEpcsFullInterval = Convert.ToInt32(oDataRow("sSettingsValue"))
                        Case "enableepcsauditlog"
                            If IsNothing(oDataRow("sSettingsValue")) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    mdlGeneral.gblnEnableEpcsAuditLog = True
                                Else
                                    mdlGeneral.gblnEnableEpcsAuditLog = False
                                End If
                            Else
                                mdlGeneral.gblnEnableEpcsAuditLog = False
                            End If
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

                        Case "enable_securemessage_download"
                            If IsNothing(oDataRow("sSettingsValue")) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    mdlGeneral.bEnableSecureMessageDownlaod = True
                                Else
                                    mdlGeneral.bEnableSecureMessageDownlaod = False
                                End If
                            Else
                                mdlGeneral.bEnableSecureMessageDownlaod = False
                            End If
                        Case "surescriptdomainextension"
                            mdlGeneral.gstrDomainExtension = Convert.ToString(oDataRow("sSettingsValue"))
                        Case "enableautosendforpendingopportunity"
                            If IsNothing(oDataRow("sSettingsValue")) = False Then
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    mdlGeneral.gblnEnableAutoSendForPendingOpportunity = True
                                Else
                                    mdlGeneral.gblnEnableAutoSendForPendingOpportunity = False
                                End If
                            Else
                                mdlGeneral.gblnEnableAutoSendForPendingOpportunity = False
                            End If
                        Case "epcsauditlogusername"
                            mdlGeneral.gstrEpcsAuditlogUsername = Convert.ToString(oDataRow("sSettingsValue"))
                        Case "epcsauditlogpassword"
                            Dim objEncryption As clsEncryption = New clsEncryption()
                            mdlGeneral.gstrEpcsAuditlogPassword = objEncryption.DecryptFromBase64String(Convert.ToString(oDataRow("sSettingsValue")), mdlGeneral.constEncryptDecryptKeyDB)
                            objEncryption = Nothing
                        Case "pdmpservice"
                            mdlGeneral.gblPDMPURL = Convert.ToString(oDataRow("sSettingsValue"))                       
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

            'If IsNothing(regKeySettings.GetValue("ServiceInterval")) = True Then
            '    Return False
            'End If
            'If IsNothing(regKeySettings.GetValue("DirService")) = False Then
            '    If regKeySettings.GetValue("DirService") = "True" Then
            '        mdlGeneral.gblnFullDownload = True
            '        else
            '    End If
            'End If

            'If IsNothing(regKeySettings.GetValue("FullDownloadDay")) = False Then
            '    mdlGeneral.gnFulldownloadDay = regKeySettings.GetValue("FullDownloadDay")
            'End If

            'If IsNothing(regKeySettings.GetValue("FullDownloadInterval")) = False Then
            '    mdlGeneral.gnFullInterval = regKeySettings.GetValue("FullDownloadInterval")
            'End If

            'If IsNothing(regKeySettings.GetValue("NightlyDownloadInterval")) = False Then
            '    mdlGeneral.gnNightlyInterval = regKeySettings.GetValue("NightlyDownloadInterval")
            'End If


            'If IsNothing(regKeySettings.GetValue("StagingServer")) = False Then
            '    mdlGeneral.gblnStagingServer = regKeySettings.GetValue("StagingServer")
            'End If



            'mdlGeneral.gstrPassword = regKeySettings.GetValue("SQLPassword")
            'mdlGeneral.time_INTERVAL = regKeySettings.GetValue("ServiceInterval")
            'mdlGeneral.gblnFullDownload = regKeySettings.GetValue("DirService")
            'mdlGeneral.UpdateLog("Successfully read registry values")

            'Code Start-Added by kanchan on 20100511 for Log setting
            'If IsNothing(regKeySettings.GetValue("RxSnifferLogArchive")) = False Then
            '    If regKeySettings.GetValue("RxSnifferLogArchive") = "True" Then
            '        mdlGeneral.gblLogArchive = True
            '    Else
            '        mdlGeneral.gblLogArchive = False
            '    End If
            'Else
            '    mdlGeneral.gblLogArchive = True
            'End If

            'If IsNothing(regKeySettings.GetValue("RxSnifferMaxLogSize")) = False Then
            '    mdlGeneral.gblLogMaxSize = regKeySettings.GetValue("RxSnifferMaxLogSize")
            'Else
            '    mdlGeneral.gblLogMaxSize = 2
            'End If

            'If IsNothing(regKeySettings.GetValue("RxSnifferLogArchivePath")) = False Then
            '    mdlGeneral.gblLogArchivePath = regKeySettings.GetValue("RxSnifferLogArchivePath")
            'Else
            '    mdlGeneral.gblLogArchivePath = ""
            'End If
            ''Code End-Added by kanchan on 20100511 for Log setting


            Return True
        Catch ex As Exception
            mdlGeneral.UpdateLog("Unable to read registry values - " & ex.ToString)
            Return False
        Finally
            If (IsNothing(dtSettings) = False) Then
                dtSettings.Dispose()
                dtSettings = Nothing
            End If
            'regKeySettings.Close()
            'regKeySettings.Dispose()
            'regKeySettings = Nothing
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

    'Private Sub eRxTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
    '    eRxTimer.Enabled = False
    '    Dim oSniffer As clsRxSniffer
    '    Try
    '        IsSettings()

    '        oSniffer = New clsRxSniffer


    '        If IsInternetConnectionAvailable() Then
    '            If mdlGeneral.gblnStagingServer Then
    '                oSniffer.SendeRx()
    '            Else
    '                oSniffer.SendeRxProduction()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        mdlGeneral.UpdateLog("Unable to Send eRx - " & ex.ToString)
    '    Finally
    '        eRxTimer.Enabled = True
    '    End Try
    'End Sub

    Private Sub myTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        RxResponseTimer.Enabled = False
        RxResponseTimer.Stop()
        'mdlGeneral.UpdateLog("RxWinService Timer event was disabled")
        Dim oSniffer As clsRxSniffer
        Try

            'For pharamcy downloads
            'GetSurescriptSettings()
            IsSettings()

            GetLogSettings()
            mdlGeneral.DeleteTempDirFiles()
            oSniffer = New clsRxSniffer
            'mdlGeneral.UpdateLog("Checking for Reponses on webserver started")

            oSniffer.RetrieveRxResponses(mdlGeneral.gblnStagingServer)

            If mdlGeneral.bEnableSecureMessageDownlaod Then
                oSniffer.RetrieveSecureMessages()
            End If



            oSniffer.SendSecureMessageStagging()

            oSniffer.ZipSourceFilePath = strZipSourceDirectory
            oSniffer.SendPortalQueuedMessages()

            ''     RxResponseTimer.Start()


            If oSniffer.IsInternetConnectionAvailable() Then
                If mdlGeneral.gblnStagingServer Then
                    oSniffer.SendeRx()
                Else
                    oSniffer.SendeRxProduction()
                End If
            End If



            If (mdlGeneral.gblnPharmacyDownload = True) Then
                ''Commented to Generate unnecessary log.
                '' mdlGeneral.UpdateLog("Pharmacy download enabled")
                '    regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, Security.AccessControl.RegistryRights.FullControl)
                Try
                    ''  RxResponseTimer.Enabled = False
                    If mdlGeneral.gblnFullDownload Then
                        mdlGeneral.UpdateLog("Full Pharmacy download enabled.")
                        If mdlGeneral.gblnStagingServer Then
                            oSniffer.GetPharmacyData(enumDownloadType.Full)
                        Else
                            oSniffer.GetPharmacyDataFromProduction(enumDownloadType.Full)
                        End If
                        mdlGeneral.gblnFullDownload = False
                        ' regKey.SetValue("DirService", "False")
                        gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.WriteValueToServicesRegistryKey("DirService", "False")

                        'For RxSniffer Kill Case
                        oSniffer.UpdateFullPharmacyDownload("DirService")
                    End If

                    If mdlGeneral.bEnableSecureMessageDownlaod Then  'If Secure message download is Enable then Prescriber download is on.
                        If mdlGeneral.gblnFullPrescriberDownload Then
                            mdlGeneral.UpdateLog("Full Prescriber download enabled.")
                            If mdlGeneral.gblnStagingServer Then
                                oSniffer.GetPrescriberData(enumDownloadType.Full)
                            Else
                                oSniffer.GetPrescriberDataFromProduction(enumDownloadType.Full)
                            End If
                            mdlGeneral.gblnFullPrescriberDownload = False
                            'regKey.SetValue("DirService", "False")
                            ''For RxSniffer Kill Case
                            oSniffer.UpdateFullPharmacyDownload("PrescriberFullDownload")
                        End If
                    End If

                Catch ex As Exception
                    mdlGeneral.UpdateLog("Unable to download - " & ex.ToString)
                Finally
                    'regKey.Close()
                    'regKey.Dispose()
                    '  RxResponseTimer.Enabled = True
                End Try

                oUTCTime = localZone.ToUniversalTime(DateTime.Now)
                oESTTime = oUTCTime.Subtract(New TimeSpan(5, 0, 0))
                Try
                    ' RxResponseTimer.Enabled = False
                    If oESTTime.Hour = mdlGeneral.gnNightlyInterval Then
                        If mdlGeneral.gblnDailyDownload = False Or mdlGeneral.gstrDirDownload <> oESTTime.ToShortDateString Then
                            If mdlGeneral.gblnStagingServer Then
                                oSniffer.GetPharmacyData(enumDownloadType.Daily)
                            Else
                                oSniffer.GetPharmacyDataFromProduction(enumDownloadType.Daily)
                            End If

                            'mdlGeneral.gblnDailyDownload = True
                            'mdlGeneral.gstrDirDownload = oESTTime.ToShortDateString
                        End If
                    End If
                Catch ex As Exception
                    mdlGeneral.gblnDailyDownload = False
                    mdlGeneral.gstrDirDownload = oESTTime.ToShortDateString
                    mdlGeneral.UpdateLog("Unable to Daily Pharmacy download - " & ex.ToString)
                Finally
                    '     RxResponseTimer.Enabled = True
                End Try
                'Prescriber Daily Download
                Try
                    '    RxResponseTimer.Enabled = False
                    If mdlGeneral.bEnableSecureMessageDownlaod Then  'If Secure message download is Enable then Prescriber download is on.
                        If oESTTime.Hour = mdlGeneral.gnNightlyInterval Then
                            If mdlGeneral.gblnDailyDownload = False Or mdlGeneral.gstrDirDownload <> oESTTime.ToShortDateString Then
                                If mdlGeneral.gblnStagingServer Then
                                    oSniffer.GetPrescriberData(enumDownloadType.Daily)
                                Else
                                    oSniffer.GetPrescriberDataFromProduction(enumDownloadType.Daily)
                                End If
                                mdlGeneral.gblnDailyDownload = True
                                mdlGeneral.gstrDirDownload = oESTTime.ToShortDateString
                            End If
                        End If
                    End If
                Catch ex As Exception
                    mdlGeneral.gblnDailyDownload = False
                    mdlGeneral.gstrDirDownload = oESTTime.ToShortDateString
                    mdlGeneral.UpdateLog("Unable to Daily Prescriber download - " & ex.ToString)
                Finally
                    '               RxResponseTimer.Enabled = True
                End Try

                '          RxResponseTimer.Enabled = False

                If oESTTime.DayOfWeek = mdlGeneral.gnFulldownloadDay Or mdlGeneral.gnFulldownloadDay = 7 Then
                    If oESTTime.Hour = mdlGeneral.gnFullInterval Then
                        If mdlGeneral.gblnFullDownload Or mdlGeneral.gstrFullDirDownload_Phr_prisb <> oESTTime.ToShortDateString Then
                            If mdlGeneral.gblnStagingServer Then
                                oSniffer.GetPharmacyData(enumDownloadType.Full)
                            Else
                                oSniffer.GetPharmacyDataFromProduction(enumDownloadType.Full)
                            End If
                            'mdlGeneral.UpdateLog("Complete Pharmacy data download is success")
                            mdlGeneral.gblnFullDownload = False
                            ' mdlGeneral.gstrDirDownload = oESTTime.ToShortDateString
                        End If


                        If mdlGeneral.gblnFullPrescriberDownload Or mdlGeneral.gstrFullDirDownload_Phr_prisb <> oESTTime.ToShortDateString Then
                            If mdlGeneral.gblnStagingServer Then
                                oSniffer.GetPrescriberData(enumDownloadType.Full)
                            Else
                                oSniffer.GetPrescriberDataFromProduction(enumDownloadType.Full)
                            End If

                            mdlGeneral.gblnFullPrescriberDownload = False
                            mdlGeneral.gstrFullDirDownload_Phr_prisb = oESTTime.ToShortDateString
                        End If



                    End If
                End If

                '      RxResponseTimer.Enabled = True
            Else
                ''Commented to Generate unnecessary log.
                '' mdlGeneral.UpdateLog("Pharmacy download disabled")
            End If



            mdlGeneral.UpdateLog("PDMP setting is " + mdlGeneral.gblnPDMPDownload.ToString())
            If (mdlGeneral.gblnPDMPDownload = True) Then
                Dim dtPDMP As DataTable = Nothing
                Dim PatientID As Long
                Dim objSendEncryption As clsEncryption = Nothing
                Dim ProviderID As Long
                Dim i As Integer = 0
                Try
                    If oSniffer Is Nothing Then
                        oSniffer = New clsRxSniffer()
                    End If

                    mdlGeneral.SetDbCredentials()
                    objSendEncryption = New clsEncryption

                    For Each row As DataRow In mdlGeneral.gblDbCredentials.Rows
                        Dim strDbPassword As String = objSendEncryption.DecryptFromBase64String(row("sSqlPassword").ToString, mdlGeneral.constEncryptDecryptKeyDB)
                        Dim strConnection As String = "SERVER=" + row("sServerName").ToString + ";DATABASE=" + row("sDatabaseName").ToString + ";USER id=" + row("sSqlUserName").ToString + ";Password=" + strDbPassword

                        Dim dtPDMPUserNamePassword As DataTable = oSniffer.GetPDMPUserNamePassword(strConnection)

                        If dtPDMPUserNamePassword IsNot Nothing AndAlso dtPDMPUserNamePassword.Rows.Count() > 0 Then
                            If dtPDMPUserNamePassword.AsEnumerable.Any(Function(p) p("sSettingsName") = "PDMP_Password") AndAlso dtPDMPUserNamePassword.AsEnumerable.Any(Function(p) p("sSettingsName") = "PDMP_Username") Then
                                mdlGeneral.gblPDMP_Username = Convert.ToString(dtPDMPUserNamePassword.AsEnumerable().FirstOrDefault(Function(p) p("sSettingsName") = "PDMP_Username")("sSettingsValue"))
                                mdlGeneral.gblPDMP_Password = Convert.ToString(dtPDMPUserNamePassword.AsEnumerable().FirstOrDefault(Function(p) p("sSettingsName") = "PDMP_Password")("sSettingsValue"))

                                If Not String.IsNullOrEmpty(mdlGeneral.gblPDMP_Username) AndAlso Not String.IsNullOrWhiteSpace(mdlGeneral.gblPDMP_Username) Then
                                    If Not String.IsNullOrEmpty(mdlGeneral.gblPDMP_Password) AndAlso Not String.IsNullOrWhiteSpace(mdlGeneral.gblPDMP_Password) Then
                                        dtPDMP = oSniffer.GetPDMPDetails(strConnection)

                                        If dtPDMP IsNot Nothing AndAlso dtPDMP.Rows.Count() > 0 Then
                                            mdlGeneral.UpdateLog("Found " + Convert.ToString(dtPDMP.Rows.Count()) + " PDMP rows to fetch")

                                            Dim PDMPService As New gloRxHub.PDMP.PDMP(mdlGeneral.gblPDMP_Username, mdlGeneral.gblPDMP_Password) With {.ConnectionString = strConnection, .WebURL = mdlGeneral.gblPDMPURL}

                                            For Each rowpdmpservice As DataRow In dtPDMP.Rows
                                                PatientID = Convert.ToInt64(rowpdmpservice("nPatientID"))
                                                ProviderID = Convert.ToInt64(rowpdmpservice("nProviderID"))
                                                PDMPService.PatientRequest(PatientID, ProviderID)
                                            Next

                                            PDMPService = Nothing
                                            dtPDMP.Clear()

                                            mdlGeneral.gblPDMP_Username = ""
                                            mdlGeneral.gblPDMP_Password = ""
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        If dtPDMPUserNamePassword IsNot Nothing Then
                            dtPDMPUserNamePassword.Dispose()
                            dtPDMPUserNamePassword = Nothing
                        End If
                    Next row

                Catch ex As Exception
                    mdlGeneral.UpdateLog("Unable to get patient request - " & ex.ToString)
                Finally
                    If dtPDMP IsNot Nothing Then
                        dtPDMP.Dispose()
                        dtPDMP = Nothing
                    End If

                    objSendEncryption = Nothing
                End Try
            End If


            If gblnEnableAutoEligibility = True Then
                If gblnGenerateRxEligibilityLog = True Then
                    mdlGeneral.UpdateLog("Processing formulary eligibility request STARTED.........")
                End If

                oSniffer.ProcessAutoEligiblity()

                If gblnGenerateRxEligibilityLog = True Then
                    mdlGeneral.UpdateLog("Processing formulary eligibility request COMPLETED.........")
                End If
            End If

            If gblnEnableAutoSendForPendingOpportunity = True Then
                oSniffer.AutoSendPendingOpportunity()
            End If

            Try
                '''''For Save Epcs AuditLog
                Dim CurrentTimeZone As DateTime = DateTime.Now
                'mdlGeneral.UpdateLog("Epcs Audit Time :" + " CurrentTimeZone " + CurrentTimeZone.Hour.ToString() + "H " + CurrentTimeZone.Minute.ToString() + "M  SetTime " + mdlGeneral.gnEpcsFullInterval.ToString())
                If mdlGeneral.gblnEnableEpcsAuditLog Then
                    Dim strFolderName As String = oSniffer.GetFolderName()
                    If strFolderName <> "" Then
                        If Not Directory.Exists(mdlGeneral.gblEpcsAuditLogPath + "\" + strFolderName) Then
                            mdlGeneral.gblnStopEpcsAuditLog = True
                        Else
                            mdlGeneral.gblnStopEpcsAuditLog = False
                        End If
                    End If
                    If mdlGeneral.gblnStopEpcsAuditLog Then
                        ' mdlGeneral.UpdateLog(" Flag Updated" + mdlGeneral.gblnStopEpcsAuditLog.ToString() + " Settings " + mdlGeneral.gblnEnableEpcsAuditLog.ToString())
                        If CurrentTimeZone.Hour = mdlGeneral.gnEpcsFullInterval Then
                            'mdlGeneral.UpdateLog("Inside..........")
                            oSniffer.GenerateEpcsAuditLog(CurrentTimeZone)
                            'mdlGeneral.UpdateLog("Outside...............")
                        End If
                    End If
                End If
            Catch ex As Exception
                mdlGeneral.UpdateLog("Unable to Do Epcs Audit Log - " & ex.ToString)
            Finally

            End Try

        Catch ex As Exception
            mdlGeneral.UpdateLog("Unable to download - " & ex.ToString)
        Finally
            oSniffer = Nothing
            RxResponseTimer.Start()
            RxResponseTimer.Enabled = True

            'mdlGeneral.UpdateLog("RxWinService Timer event was enabled")
        End Try
    End Sub

    ''   Private Sub PharmacyTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
    'PharmacyTimer.Enabled = False
    'Dim oSniffer As clsRxSniffer
    'Try
    '    'GetSurescriptSettings()
    '    IsSettings()
    '    oSniffer = New clsRxSniffer
    '    'If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, Security.AccessControl.RegistryRights.FullControl)) = False Then

    '    Try

    '        If mdlGeneral.gblnFullDownload Then
    '            If mdlGeneral.gblnStagingServer Then
    '                oSniffer.GetPharmacyData(enumDownloadType.Full)
    '            Else
    '                oSniffer.GetPharmacyDataFromProduction(enumDownloadType.Full)
    '            End If
    '            mdlGeneral.gblnFullDownload = False

    '            RxResponseTimer.Enabled = False
    '            regKey = Registry.LocalMachine.OpenSubKey("Software\gloServices", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, Security.AccessControl.RegistryRights.FullControl)
    '            regKey.SetValue("DirService", "False")

    '        End If

    '    Catch ex As Exception
    '        mdlGeneral.UpdateLog("Unable to download - " & ex.ToString)
    '    Finally
    '        regKey.Close()
    '        RxResponseTimer.Enabled = True
    '    End Try

    '    oUTCTime = localZone.ToUniversalTime(DateTime.Now)
    '    oESTTime = oUTCTime.Subtract(New TimeSpan(5, 0, 0))
    '    If oESTTime.Hour = mdlGeneral.gnNightlyInterval Then
    '        If mdlGeneral.gblnDailyDownload = False Or mdlGeneral.gstrDirDownload <> oESTTime.ToShortDateString Then
    '            If mdlGeneral.gblnStagingServer Then
    '                oSniffer.GetPharmacyData(enumDownloadType.Daily)
    '            Else
    '                oSniffer.GetPharmacyDataFromProduction(enumDownloadType.Daily)
    '            End If

    '            mdlGeneral.UpdateLog("Nightly Pharmacy data download is success")
    '            mdlGeneral.gblnDailyDownload = True
    '            mdlGeneral.gstrDirDownload = oESTTime.ToShortDateString
    '        End If
    '    End If

    '    If oESTTime.DayOfWeek = mdlGeneral.gnFulldownloadDay Then
    '        If oESTTime.Hour = mdlGeneral.gnFullInterval Then
    '            If mdlGeneral.gblnFullDownload Or mdlGeneral.gstrDirDownload <> oESTTime.ToShortDateString Then
    '                If mdlGeneral.gblnStagingServer Then
    '                    oSniffer.GetPharmacyData(enumDownloadType.Full)
    '                Else
    '                    oSniffer.GetPharmacyDataFromProduction(enumDownloadType.Full)
    '                End If
    '                mdlGeneral.UpdateLog("Complete Pharmacy data download is success")
    '                mdlGeneral.gblnFullDownload = False
    '                mdlGeneral.gstrDirDownload = oESTTime.ToShortDateString
    '            End If
    '        End If
    '    End If
    'Catch ex As Exception
    '    mdlGeneral.UpdateLog("Unable to download - " & ex.ToString)
    'Finally
    '    oSniffer = Nothing
    '    PharmacyTimer.Enabled = True
    'End Try
    '' End Sub

    'Code Start-Added by kanchan on 20100511 for Log setting
    Public Function GetLogSettings()
        'Region for archive log file
        Dim FilePath As String = Nothing
        Dim temp As String = Nothing
        Dim oFileInfo As System.IO.FileInfo = Nothing
        Try
            temp = DateTime.Now.Year.ToString() & DateTime.Now.Month.ToString() & DateTime.Now.Day.ToString() & DateTime.Now.Hour.ToString() & DateTime.Now.Minute.ToString() & DateTime.Now.Second.ToString()
            FilePath = System.Windows.Forms.Application.StartupPath & "\RxSniffer.log"

            oFileInfo = New System.IO.FileInfo(FilePath)

            If oFileInfo.Exists Then
                If mdlGeneral.gblLogMaxSize.ToString <> "" Then
                    If oFileInfo.Length > 1048576 * CType(mdlGeneral.gblLogMaxSize, Int32) Then
                        If mdlGeneral.gblLogArchive = True Then
                            'archive log file on Path provided in setting if it's exceeds limit set in Log setting
                            If mdlGeneral.gblLogArchivePath <> "" Then
                                FilePath = mdlGeneral.gblLogArchivePath & "\" & temp & ".Log"
                                oFileInfo.MoveTo(FilePath)
                            Else
                                FilePath = System.Windows.Forms.Application.StartupPath & "\" & temp & ".Log"
                                oFileInfo.MoveTo(FilePath)
                            End If
                        Else
                            'Delete log file if it's exceeds limit set in Log setting
                            oFileInfo.Delete()
                        End If
                    End If
                End If
                oFileInfo = Nothing
            End If

            'Dim _TempFolder As String = System.Windows.Forms.Application.StartupPath & "\Temp"
            'If Directory.Exists(_TempFolder) = True Then
            '    Dim oRootFolder As System.IO.DirectoryInfo = New DirectoryInfo(_TempFolder)
            '    Dim oFiles As FileInfo() = oRootFolder.GetFiles()
            '    Dim oFile As FileInfo
            '    For Each oFile In oFiles
            '        oFile.Delete()
            '    Next
            '    oFiles = Nothing
            '    oFile = Nothing
            'End If
        Catch ex As Exception
            UpdateLog("Error while Log maintenance ****" & ex.ToString)
        Finally
            If Not IsNothing(oFileInfo) Then
                oFileInfo = Nothing
            End If
        End Try
        Return Nothing
    End Function
    'Code End-Added by kanchan on 20100511 for Log setting
    '' 'REmoved glovault Code as per gloVault service implimentation. by madan on 20110110
    '''' <summary>
    '''' Added by madan for gloVault service.
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub myGloVaultTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
    '    gloVaultTimer.Enabled = False
    '    Dim sConnectionString As String = String.Empty
    '    Dim sWebUrl As String = String.Empty
    '    Dim dtMessageQueue As DataTable = Nothing
    '    Dim sAusId As String = String.Empty
    '    Dim objClsDbCredentials As ClsDbCredentials = New ClsDbCredentials
    '    Dim dtDatabaseCredentials As New DataTable
    '    Try
    '        'If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, Security.AccessControl.RegistryRights.FullControl)) = True Then
    '        '    Exit Sub
    '        'End If

    '        'regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
    '        'If IsNothing(regKey.GetValue("SQLServer")) = True Then
    '        '    gloVault.Classes.clsGeneralInterface.UpdateLog("gloEMR sqlServer settings not found in registry")
    '        '    Exit Sub
    '        'End If
    '        'If IsNothing(regKey.GetValue("Database")) = True Then
    '        '    gloVault.Classes.clsGeneralInterface.UpdateLog("gloEMR database settings not found in registry")
    '        '    Exit Sub
    '        'End If
    '        'If IsNothing(regKey.GetValue("SQLUserEMR")) = True Then
    '        '    gloVault.Classes.clsGeneralInterface.UpdateLog("gloEMR SQLUser settings not found in registry")
    '        '    Exit Sub
    '        'End If
    '        'If IsNothing(regKey.GetValue("SQLPasswordEMR")) = True Then
    '        '    gloVault.Classes.clsGeneralInterface.UpdateLog("gloEMR SqlPassword settings not found in registry")
    '        '    Exit Sub
    '        'End If
    '        'gloVault.Classes.clsGeneralInterface.UpdateLog("gloVault Service Started.")

    '        'If IsSettings() = False Then
    '        '    gloVault.Classes.clsGeneralInterface.UpdateLog("rxSniffer database configuration not found.")
    '        '    Exit Sub
    '        'End If

    '        Dim sSqlUser As String = String.Empty
    '        Dim sSqlPassword As String = String.Empty
    '        Dim sSqlServer As String = String.Empty
    '        Dim sSqlDatabase As String = String.Empty


    '        dtDatabaseCredentials = objClsDbCredentials.getDataBaseCredentials(0)

    '        ''sServerName,sDatabaseName,sSqlUserName,sSqlPassword
    '        If IsNothing(dtDatabaseCredentials) Then
    '            gloVault.Classes.clsGeneralInterface.UpdateLog("gloEMR database configuration not found.")
    '            Exit Sub
    '        End If


    '        For iIndex As Integer = 0 To dtDatabaseCredentials.Rows.Count - 1


    '            sSqlUser = Convert.ToString(dtDatabaseCredentials.Rows(iIndex)("sSqlUserName").ToString())
    '            sSqlPassword = Convert.ToString(objEncryption.DecryptFromBase64String(Convert.ToString(dtDatabaseCredentials.Rows(iIndex)("sSqlPassword").ToString()), mdlGeneral.constEncryptDecryptKeyDB))
    '            sSqlDatabase = Convert.ToString(dtDatabaseCredentials.Rows(iIndex)("sDatabaseName").ToString())
    '            sSqlServer = Convert.ToString(dtDatabaseCredentials.Rows(iIndex)("sServerName").ToString())

    '            objEncryption = New clsEncryption

    '            sConnectionString = "SERVER=" & sSqlServer & "; DATABASE= " & sSqlDatabase & " ;USER id=" & sSqlUser & "; Password=" & sSqlPassword

    '            ''Retrive gloVault configuration from App.config.
    '            If Not IsNothing(appSettings) Then
    '                If Not IsNothing(appSettings("gloVaultURL")) Then
    '                    If appSettings("gloVaultURL") <> "" Then
    '                        sWebUrl = appSettings("gloVaultURL").ToString()
    '                    End If
    '                End If
    '            End If


    '            If sWebUrl.Length <= 0 Then
    '                gloVault.Classes.clsGeneralInterface.UpdateLog("gloVault Web configuration not found.")
    '                Exit Sub
    '            End If


    '            Dim objgloVaultDbLayer As New gloVault.Classes.clsVaultDbLayer(sConnectionString)
    '            Dim objgloVaultBussinessLayer As New gloVault.Classes.clsBussinessLayer(sConnectionString)

    '            'Settings to trouble shoot.
    '            If Not IsNothing(appSettings) Then
    '                If Not IsNothing(appSettings("gloVault-TroubleShoot")) Then
    '                    If appSettings("gloVault-TroubleShoot") <> "" Then
    '                        If appSettings("gloVault-TroubleShoot").ToString() = "1" Then
    '                            gloVault.Classes.clsBussinessLayer.IsTroubleShoot = True
    '                            gloVault.Classes.clsGeneralInterface.UpdateLog("gloVault troubleshoot mode activated.")
    '                        End If
    '                    End If
    '                End If
    '            End If

    '            If objgloVaultBussinessLayer.TestConnection(sWebUrl) = False Then
    '                gloVault.Classes.clsGeneralInterface.UpdateLog("gloVault Web connection failed.")
    '                Exit Sub
    '            End If


    '            ''Method to retrive ausid.
    '            sAusId = objgloVaultDbLayer.RetriveAusId()

    '            If sAusId.Length <= 0 Then
    '                gloVault.Classes.clsGeneralInterface.UpdateLog("SiteId not found in the system.(Please configure siteid in gloEMR-Admin-> Clinic settings)")
    '                Exit Sub
    '            End If

    '            'Generate key.

    '            If sKey.Length <= 0 Then
    '                sKey = objgloVaultBussinessLayer.GetAccessKey(sAusId)
    '            Else
    '                ''Generate key if the difference is 30 Minutes
    '                Dim DtDifference As TimeSpan
    '                DtDifference = DateTime.Now - dtLastAccessTime

    '                If DtDifference.Minutes > 29 Then
    '                    dtLastAccessTime = DateTime.Now
    '                    sKey = objgloVaultBussinessLayer.GetAccessKey(sAusId)
    '                End If
    '            End If


    '            If sKey.Length <= 0 Then
    '                gloVault.Classes.clsGeneralInterface.UpdateLog("Failed to generate accesskey.")
    '                Exit Sub
    '            End If

    '            'End generating key.

    '            dtMessageQueue = New DataTable()

    '            Try
    '                ''Retrive all message queue for EMAIL Request.
    '                dtMessageQueue = objgloVaultDbLayer.GetglMessageQueue(gloVault.Classes.clsVaultDbLayer.enmMessageTypes.email)

    '                If Not IsNothing(dtMessageQueue) And dtMessageQueue.Rows.Count > 0 Then

    '                    gloVault.Classes.clsGeneralInterface.UpdateLog("Processing Request Access.")
    '                    ''Send all email request.
    '                    For index As Integer = 0 To dtMessageQueue.Rows.Count - 1

    '                        'nMessageId,nPatientId,nOtherID,sField1
    '                        Dim nPatientId As Int64 = 0
    '                        Dim nMessageId As Int64 = 0
    '                        Dim nnUserID As Int64 = 0
    '                        Dim smMachineName As String = String.Empty

    '                        nnUserID = Convert.ToInt64(dtMessageQueue.Rows(index)("nOtherID"))

    '                        If Not IsNothing(dtMessageQueue.Rows(index)("sMachineName")) Then
    '                            smMachineName = Convert.ToString(dtMessageQueue.Rows(index)("sMachineName"))
    '                        End If

    '                        nPatientId = Convert.ToInt64(dtMessageQueue.Rows(index)("nPatientId"))
    '                        nMessageId = Convert.ToInt64(dtMessageQueue.Rows(index)("nMessageId"))

    '                        If nPatientId > 0 And nMessageId > 0 Then
    '                            If objgloVaultBussinessLayer.GenerateEmailRequest(nPatientId, sKey) Then
    '                                objgloVaultDbLayer.UpdateMessageQueue(nMessageId, 0)
    '                                objgloVaultDbLayer.InsertAuditLog(nPatientId, "Request access success", True, nnUserID, smMachineName, "email")
    '                            Else
    '                                objgloVaultDbLayer.UpdateMessageQueue(nMessageId, 2)
    '                                objgloVaultDbLayer.InsertAuditLog(nPatientId, "Request access failed", False, nnUserID, smMachineName, "email")
    '                            End If
    '                        End If
    '                    Next
    '                End If

    '                dtMessageQueue = Nothing

    '            Catch ex As Exception
    '                gloVault.Classes.clsGeneralInterface.UpdateLog(ex.ToString())
    '            End Try

    '            ''Retrive approved patients.
    '            objgloVaultBussinessLayer.GetPatientApprovals(sKey, sAusId)

    '            dtMessageQueue = New DataTable()

    '            Try
    '                dtMessageQueue = objgloVaultDbLayer.GetglMessageQueue(gloVault.Classes.clsVaultDbLayer.enmMessageTypes.data)

    '                If Not IsNothing(dtMessageQueue) And dtMessageQueue.Rows.Count > 0 Then

    '                    gloVault.Classes.clsGeneralInterface.UpdateLog("Processing for sending information.")

    '                    ''Send all Pending informaion.
    '                    For index1 As Integer = 0 To dtMessageQueue.Rows.Count - 1

    '                        'nMessageId,nPatientId,nOtherID,sField1
    '                        Dim nPatientId As Int64 = 0
    '                        Dim nMessageId As Int64 = 0
    '                        Dim sCCDFilePath As String = String.Empty
    '                        Dim nUserID As Int64 = 0
    '                        Dim sMachineName As String = String.Empty
    '                        Dim ListIds As List(Of Int64) = New List(Of Long)()


    '                        nPatientId = Convert.ToInt64(dtMessageQueue.Rows(index1)("nPatientId"))
    '                        nMessageId = Convert.ToInt64(dtMessageQueue.Rows(index1)("nMessageId"))
    '                        nUserID = Convert.ToInt64(dtMessageQueue.Rows(index1)("nOtherID"))

    '                        If Not IsNothing(dtMessageQueue.Rows(index1)("sMachineName")) Then
    '                            sMachineName = Convert.ToString(dtMessageQueue.Rows(index1)("sMachineName"))
    '                        End If


    '                        If Not IsNothing(dtMessageQueue.Rows(index1)("sField2")) Then
    '                            sCCDFilePath = Convert.ToString(dtMessageQueue.Rows(index1)("sField2"))
    '                        End If

    '                        Dim sConfigurations As String() = dtMessageQueue.Rows(index1)("sField1").ToString().Split(","c)


    '                        For j As Integer = 0 To sConfigurations.Length - 1
    '                            ListIds.Add(Convert.ToInt64(sConfigurations(j)))
    '                        Next

    '                        If nPatientId > 0 And nMessageId > 0 And ListIds.Count > 0 Then
    '                            If objgloVaultBussinessLayer.ProcessPatientsInfo(nPatientId, ListIds, sKey, nUserID, sMachineName) Then
    '                                ''Send CCD File
    '                                If sCCDFilePath.Length > 0 Then
    '                                    objgloVaultBussinessLayer.SendCCDFile(nPatientId, sCCDFilePath, sKey, nUserID, sMachineName)
    '                                End If

    '                                objgloVaultDbLayer.UpdateMessageQueue(nMessageId, "0")
    '                            Else
    '                                objgloVaultDbLayer.UpdateMessageQueue(nMessageId, "2")
    '                            End If



    '                        End If
    '                        nPatientId = 0
    '                        nMessageId = 0
    '                        nUserID = 0
    '                        sMachineName = String.Empty
    '                    Next

    '                End If

    '            Catch ex As Exception
    '                gloVault.Classes.clsGeneralInterface.UpdateLog(ex.ToString())

    '            End Try

    '        Next





    '    Catch ex As Exception
    '        gloVault.Classes.clsGeneralInterface.UpdateLog(ex.ToString())

    '    Finally
    '        gloVaultTimer.Enabled = True
    '    End Try
    'End Sub


    ''' <summary>
    ''' the autoeligibility functionality will run in different timer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub myAutoEligTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        gloAutoEligTimer.Enabled = False
        'mdlGeneral.UpdateLog("RxWinService Timer event was disabled")
        Dim oSniffer As clsRxSniffer
        Try

            'For pharamcy downloads
            'GetSurescriptSettings()
            'IsSettings()
            GetLogSettings()
            oSniffer = New clsRxSniffer
            'mdlGeneral.UpdateLog("Checking for Reponses on webserver started")

            Try

                If gblnEnableAutoEligibility = True Then
                    If gblnGenerateRxEligibilityLog = True Then
                        mdlGeneral.UpdateLog("Processing formulary eligibility request STARTED.........")
                    End If
                    'mdlGeneral.UpdateLog("Auto eligibility through timer.........")
                    oSniffer.ProcessAutoEligiblity()

                    If gblnGenerateRxEligibilityLog = True Then
                        mdlGeneral.UpdateLog("Processing formulary eligibility request COMPLETED.........")
                    End If
                End If
            Catch ex As Exception
                mdlGeneral.UpdateLog("Unable to download - " & ex.ToString)
            End Try


        Catch ex As Exception
            mdlGeneral.UpdateLog("Unable to download - " & ex.ToString)
        Finally
            oSniffer = Nothing
            gloAutoEligTimer.Enabled = True
            'mdlGeneral.UpdateLog("RxWinService Timer event was enabled")
        End Try
    End Sub
End Class
