Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Microsoft.Win32
Namespace RxGeneral
    Public Enum enumDownloadType
        Full
        Daily
        Instant
    End Enum
    Public Enum enumFileType
        XMLFile
        ZipFile
    End Enum
    Public Enum SecureMessageType

        None
        Send
        OutBox
        sError
        Status
        Verify

    End Enum

    Public Enum SecureMessageStatus

        None = 0
        Send = 1
        Receive = 2
    End Enum

    Public Enum ModuleName
        None = 0
        Exam = 1
        Dms = 2
    End Enum

    Public Enum FileExtension
        pdf = 0
        docx = 1
        zip = 2
        xml = 3
        txt = 4
        rtf = 5
        html = 6
        htm = 7
    End Enum

    Module mdlGeneral

        Public gblnEnableEpcsAuditLog As Boolean = False
        Public gblnStopEpcsAuditLog As Boolean = True
        Public gblnEnableAutoEligibility As Boolean = False ''''''''by default false
        Public gblnGenerateRxEligibilityLog As Boolean = False ''''''''by default false
        Public gblnEnableAutoSendForPendingOpportunity As Boolean = False ''''''''by default false

        Public gstrDatabaseName As String
        Public gstrSQLServerName As String
        Public gstrUserId As String
        Public gstrPassword As String
        Public gblnDailyDownload As Boolean = False
        Public gblnFullDownload As Boolean = False
        Public gblnFullPrescriberDownload As Boolean = False
        Public gstrDirDownload As String = ""
        Public gstrFullDirDownload_Phr_prisb As String = ""
        Public gblnPharmacyDownload As Boolean = True
        Public gblnPDMPDownload As Boolean = True
        Public gblPDMPURL As String = ""
        Public gblPDMP_Username As String = ""
        Public gblPDMP_Password As String = ""

        Public time_INTERVAL As Int64 = 10
        'Public constEncryptDecryptKey As String = "20gloStreamInc08"
        Public blnOpen As Boolean = False
        Public gnNightlyInterval As Int32 = 1
        Public gnFullInterval As Int32 = 4

        Public gnEpcsFullInterval As Int32 = 4

        Public gstrEpcsAuditlogUsername As String
        Public gstrEpcsAuditlogPassword As String

        Public gnFulldownloadDay As Int32 = 1
        Public gblnStagingServer As Boolean

        '  Dim regKey As RegistryKey
        Public DatabaseID As Int64
        Public FaxSettingID As Int64

        Public constEncryptDecryptKeyRegistry As String = "20gloStreamInc08"
        Public constEncryptDecryptKeyDB As String = "12345678"
        Public gblDbCredentials As DataTable
        Public gblDBCount As Int16
        '  Dim objEncryption As clsEncryption

        'Code Start-Added by kanchan on 20100511 for Log setting
        Public gblLogArchive As Boolean = True
        Public gblLogMaxSize As Int32
        Public gblLogArchivePath As String = ""

        Public gblEpcsAuditLogPath As String = ""

        'Code End-Added by kanchan on 20100511 for Log setting

        'Public isSqlServerStarted As Boolean = False
        'Public isProcessInQueue As Boolean = False

        Public globalCurrentDate As DateTime = DateTime.Now
        'sarika Insert Clinic ID 20090227 
        Public gnClinicID As Int64 = 1
        '---
        Public bIsProviderAvailable As Boolean = False

        Public bEnableSecureMessageDownlaod As Boolean = False

        Public nClinicID As Integer
        Public sClinicName As String
        Public SiteID As String
        Public Location As String
        Public AUSID As String
        Public sSPIID As String

        Public gstrDomainExtension As String = ""
        Public bIsPatientSavingMessageQueue As Boolean = True


        Public Sub SetDbCredentials()
            Dim _dtTemp As DataTable = Nothing
            Dim _objConn As SqlConnection = Nothing
            Dim _strSelectQuery As String = String.Empty
            Dim _oSqlDataAdapter As SqlDataAdapter = Nothing
            Try

                'mdlGeneral.UpdateLog("start: SetDbCredentials:")
                _objConn = New SqlConnection(GetConnectionString)
                _strSelectQuery = "SELECT nDBConnectionId,sDatabaseName,sServerName,sSqlUserName,sSqlPassword FROM DBSettings where sServiceName='RxSniffer'"
                _oSqlDataAdapter = New SqlDataAdapter(_strSelectQuery, _objConn)
                _dtTemp = New DataTable()
                _oSqlDataAdapter.Fill(_dtTemp)
                gblDbCredentials = _dtTemp

                _strSelectQuery = String.Empty

                'mdlGeneral.UpdateLog(("conn found: SetDbCredentials: gblDbCredentials" + gblDbCredentials.Rows.Count))
            Catch Ex As Exception
                mdlGeneral.UpdateLog(("on SetDBcredentials :" + Ex.Message))
            Finally
                If Not IsNothing(_oSqlDataAdapter) Then
                    _oSqlDataAdapter.Dispose()
                    _oSqlDataAdapter = Nothing
                End If
                If Not IsNothing(_objConn) Then
                    _objConn.Dispose()
                    _objConn = Nothing
                End If
                'If Not IsNothing(_dtTemp) Then 'SLR: don't dispose
                '    _dtTemp.Dispose()
                '    _dtTemp = Nothing
                'End If
            End Try
            'mdlGeneral.UpdateLog("finish: SetDbCredentials:")
        End Sub


        Public Function GetTransactionID() As Int64
            Dim strID As String
            Dim dtDate As DateTime
            dtDate = System.DateTime.Now

            strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)

            strID = strID & DateDiff(DateInterval.Second, dtDate.Date, dtDate)
            strID = strID & dtDate.Millisecond
            ' strID = strID & DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate)
            Return CType(strID, Int64)
        End Function

        Public Sub DeleteTempFileAutoEligibility(ByVal strRequestPath As String, ByVal strResponcePath As String)

            Try
                If System.IO.File.Exists(strRequestPath) = True Then
                    System.IO.File.Delete(strRequestPath)
                End If
                If System.IO.File.Exists(strResponcePath) = True Then
                    System.IO.File.Delete(strResponcePath)
                End If
            Catch ex As Exception
                mdlGeneral.UpdateLog(("Deleting Temp Directory file :" + ex.Message))
            Finally

            End Try
        End Sub

        Public Sub DeleteTempDir()
            On Error Resume Next
            If File.Exists(Application.StartupPath & "\RxSniffer.log") Then
                File.Delete(Application.StartupPath & "\RxSniffer.log")
            End If
            Dim _TempFolder As String = Application.StartupPath & "\Temp"
            If Directory.Exists(_TempFolder) = True Then
                Dim oRootFolder As System.IO.DirectoryInfo = New DirectoryInfo(_TempFolder)
                Dim oFiles As FileInfo() = oRootFolder.GetFiles()
                Dim oFile As FileInfo
                For Each oFile In oFiles
                    oFile.Delete()
                Next
                oFiles = Nothing
                oFile = Nothing
            End If
        End Sub
        Public Sub DeleteTempDirFiles()
            Dim _TempFolder As String = Nothing
            Dim oRootFolder As System.IO.DirectoryInfo = Nothing
            Dim oFiles As FileInfo() = Nothing
            Dim oFile As FileInfo = Nothing
            Try
                _TempFolder = Application.StartupPath & "\Temp"
                If Directory.Exists(_TempFolder) = True Then
                    oRootFolder = New DirectoryInfo(_TempFolder)
                    oFiles = oRootFolder.GetFiles()
                    For Each oFile In oFiles
                        oFile.Delete()
                    Next
                    oFile = Nothing
                    oFiles = Nothing
                End If
            Catch ex As Exception
                mdlGeneral.UpdateLog(("Deleting Temp Directory file :" + ex.Message))
            Finally
                If Not IsNothing(oFile) Then
                    oFile = Nothing
                End If
                If Not IsNothing(oFiles) Then
                    oFiles = Nothing
                End If
            End Try

        End Sub

        Public Function ConvertFiletoBinary(ByVal strFileName As String) As Byte()
            If File.Exists(strFileName) Then
                Dim oFile As FileStream = Nothing
                Dim oReader As BinaryReader = Nothing
                Try
                    ''Please uncomment the following line of code to read the file, even the file is in use by same or another process
                    'oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous)

                    ''To read the file only when it is not in use by any process
                    oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read)

                    oReader = New BinaryReader(oFile)
                    Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)
                    Return bytesRead

                Catch ex As IOException
                    UpdateLog("Error while conversion  - " & ex.ToString)
                    MsgBox("Error while conversion  - " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("Error while conversion  - " & ex.ToString)
                    MsgBox("Error while conversion  - " & ex.ToString)
                    Return Nothing

                Finally
                    If Not IsNothing(oFile) Then
                        oFile.Close()
                        oFile.Dispose()
                        oFile = Nothing
                    End If
                    If Not IsNothing(oReader) Then
                        oReader.Close()
                        oReader.Dispose()
                        oReader = Nothing
                    End If
                    'SLR: Dispose oreader and ofile
                End Try

            Else
                Return Nothing
            End If

        End Function

        Public Function ConvertBinarytoFile(ByVal cntFromDB As Object, ByVal strFileName As String) As String
            If Not cntFromDB Is Nothing Then
                Dim content As Byte() = CType(cntFromDB, Byte())
                Dim stream As MemoryStream = New MemoryStream(content)
                Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                stream.WriteTo(oFile)

                If Not IsNothing(oFile) Then
                    oFile.Close()
                    oFile.Dispose()
                    oFile = Nothing
                End If
                If Not IsNothing(stream) Then
                    stream.Close()
                    stream.Dispose()
                    stream = Nothing
                End If
                If Not IsNothing(content) Then
                    content = Nothing
                End If
                Return strFileName
            Else
                Return ""
                UpdateLog("Unable to convert to Physical file")
            End If
            'SLR: Finaly free oFile, stream and content
        End Function

        Public ReadOnly Property GetFileName(ByVal eType As enumFileType) As String
            Get
                Dim strAppPath As String = gloSettings.FolderSettings.AppTempFolderPath
                Dim _NewDocumentName As String = ""
                Dim _Extension As String = ""
                If eType = enumFileType.XMLFile Then
                    _Extension = ".xml"
                ElseIf eType = enumFileType.ZipFile Then
                    _Extension = ".zip"
                End If

                Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

                Dim i As Integer = 0
                _NewDocumentName = Format(_dtCurrentDateTime, "MMddyyyyhhmmssffftt") & _Extension
                While File.Exists(strAppPath & _NewDocumentName) = True And i < Integer.MaxValue
                    i = i + 1
                    _NewDocumentName = Format(_dtCurrentDateTime, "MMddyyyyhhmmssffftt") & "-" & i & _Extension
                End While
                Return strAppPath & _NewDocumentName
            End Get
        End Property

        Public Sub UpdateLog(ByVal strLogMessage As String, Optional ByVal strConnectionString As String = "")
            Try

                If strConnectionString <> "" Then
                    globalCurrentDate = gloGlobal.gloTimeZone.getgloDateTime(gloGlobal.gloTimeZone.getLocalTimeZoneId(strConnectionString))
                Else
                    globalCurrentDate = DateTime.Now
                End If
                Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\RxSniffer.log", True)
                objFile.WriteLine(globalCurrentDate & ":" & globalCurrentDate.Millisecond & vbTab & strLogMessage)
                objFile.Close()
                objFile = Nothing
            Catch ex As Exception

            End Try
        End Sub

        Public Sub UpdateAutoEligLog(ByVal strLogMessage As String, Optional ByVal strConnectionString As String = "")
            Try
                If strConnectionString <> "" Then
                    globalCurrentDate = gloGlobal.gloTimeZone.getgloDateTime(gloGlobal.gloTimeZone.getLocalTimeZoneId(strConnectionString))
                Else
                    globalCurrentDate = DateTime.Now
                End If
                Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\AutoEligibility.log", True)
                objFile.WriteLine(globalCurrentDate & ":" & globalCurrentDate.Millisecond & vbTab & strLogMessage)
                objFile.Close()
                objFile = Nothing
            Catch ex As Exception

            End Try
        End Sub

        Public Function IsSettings() As Boolean
            Dim strConnectionString As String = String.Empty
            Dim dtSettings As DataTable = Nothing
            Try
                ''SLR: Write it to a vairable: since opensubkey needs to be closed.
                'regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, Security.AccessControl.RegistryRights.FullControl)

                'If IsNothing(regKey) = True Then
                '    Return False
                'End If
                '' regKey = Registry.LocalMachine.OpenSubKey("Software\gloServices", True)
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
                Dim gstrSQLServerName As String = ""
                Dim gstrDatabaseName As String = ""
                Dim gstrUserId As String = ""
                Dim gstrPassword As String = ""

                gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ReadFromServicesRegistry(gstrSQLServerName, gstrDatabaseName, gstrUserId, gstrPassword)

               
                strConnectionString = "SERVER=" & gstrSQLServerName & "; DATABASE= " & gstrDatabaseName & " ;USER id=" & gstrUserId & "; Password=" & gstrPassword
                dtSettings = ReadSettings(strConnectionString)
                If (IsNothing(dtSettings) = False) Then


                    For Each oDataRow As DataRow In dtSettings.Rows
                        Select Case Convert.ToString(Convert.ToString(oDataRow("sSettingsName")))
                            Case "ServiceInterval"
                                If IsNothing(oDataRow("sSettingsValue")) = True Then
                                    Return False
                                Else
                                    mdlGeneral.time_INTERVAL = Convert.ToInt64(oDataRow("sSettingsValue"))
                                End If
                                'For resolving case no :GLO2011-0010737 i.e Conflicts in Contacts_MST
                            Case "PharmacyDownload"
                                If IsNothing(oDataRow("sSettingsValue")) = False Then
                                    If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                        mdlGeneral.gblnPharmacyDownload = True
                                    Else
                                        mdlGeneral.gblnPharmacyDownload = False
                                    End If
                                End If
                            Case "DirService"
                                If IsNothing(oDataRow("sSettingsValue")) = False Then
                                    If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                        mdlGeneral.gblnFullDownload = True
                                    Else
                                        mdlGeneral.gblnFullDownload = False
                                    End If
                                End If
                            Case "FullDownloadDay"
                                If IsNothing(oDataRow("sSettingsValue")) = False Then
                                    mdlGeneral.gnFulldownloadDay = Convert.ToInt32(oDataRow("sSettingsValue"))
                                End If
                            Case "FullDownloadInterval"
                                mdlGeneral.gnFullInterval = Convert.ToInt32(oDataRow("sSettingsValue"))
                            Case "NightlyDownloadInterval"
                                mdlGeneral.gnNightlyInterval = Convert.ToInt32(oDataRow("sSettingsValue"))
                            Case "StagingServer"
                                mdlGeneral.gblnStagingServer = Convert.ToBoolean(oDataRow("sSettingsValue"))
                            Case "RxSnifferLogArchive"
                                If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                    mdlGeneral.gblLogArchive = True
                                Else
                                    mdlGeneral.gblLogArchive = False
                                End If
                            Case "RxSnifferMaxLogSize"
                                mdlGeneral.gblLogMaxSize = Convert.ToInt32(oDataRow("sSettingsValue"))
                            Case "RxSnifferLogArchivePath"
                                mdlGeneral.gblLogArchivePath = Convert.ToString(oDataRow("sSettingsValue"))
                            Case "EpcsAuditLogPath"
                                mdlGeneral.gblEpcsAuditLogPath = Convert.ToString(oDataRow("sSettingsValue"))
                            Case "EpcsAuditLogStartInterval"
                                mdlGeneral.gnEpcsFullInterval = Convert.ToInt32(oDataRow("sSettingsValue"))
                            Case "EnableEpcsAuditLog"
                                If IsNothing(oDataRow("sSettingsValue")) = False Then
                                    If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                        mdlGeneral.gblnEnableEpcsAuditLog = True
                                    Else
                                        mdlGeneral.gblnEnableEpcsAuditLog = False
                                    End If
                                End If
                            Case "EpcsAuditlogUsername"
                                mdlGeneral.gstrEpcsAuditlogUsername = Convert.ToString(oDataRow("sSettingsValue"))
                            Case "EpcsAuditlogPassword"
                                Dim objEncryption As clsEncryption = New clsEncryption()
                                mdlGeneral.gstrEpcsAuditlogPassword = objEncryption.DecryptFromBase64String(Convert.ToString(oDataRow("sSettingsValue")), mdlGeneral.constEncryptDecryptKeyDB)
                                objEncryption = Nothing
                            Case "SnifferPDMPDownload"
                                If IsNothing(oDataRow("sSettingsValue")) = False Then
                                    If Convert.ToString(oDataRow("sSettingsValue")) = "True" Then
                                        mdlGeneral.gblnPDMPDownload = True
                                    Else
                                        mdlGeneral.gblnPDMPDownload = False
                                    End If
                                End If
                        End Select
                    Next
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
                ''SLR: Free previously assigned memory and then assing new memory
                'If Not IsNothing(objEncryption) Then
                '    objEncryption = Nothing
                'End If
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
                'regKey.Close()
                If Not IsNothing(dtSettings) Then
                    dtSettings.Dispose()
                    dtSettings = Nothing
                End If
                'If Not IsNothing(objEncryption) Then
                '    objEncryption = Nothing
                'End If
                'If Not IsNothing(regKey) Then
                '    regKey.Dispose()
                '    regKey = Nothing
                'End If
                'SLR: Free dtSettins, objencryptiom regkey
            End Try
        End Function

        Public Function GetConnectionString() As String
            Dim strConnectionString As String = String.Empty
            If (gstrSQLServerName Is Nothing = False) And (gstrDatabaseName Is Nothing = False) And gstrSQLServerName <> "" And gstrDatabaseName <> "" Then

                '  strConnectionString = "SERVER=" & gstrSQLServerName & ";DATABASE=" & gstrDatabaseName & ";Integrated Security=SSPI"
                strConnectionString = "SERVER=" & gstrSQLServerName & ";DATABASE=" & gstrDatabaseName & ";USER id=" & gstrUserId & ";Password=" & gstrPassword
                'strConnectionString = "SERVER=192.168.123.99;DATABASE=gloCCHIT2007;USER id=sa;password=sasakar"

            Else
                'Return ""
            End If
            'strConnectionString = "SERVER=dev30;DATABASE=gloServices;USER id=sa;Password=sadev30"
            Return strConnectionString
        End Function
        Public Function GetServiceConnectionstring() As String
            ' Dim regKeySettings As RegistryKey = Nothing
            ' Dim objEncryption As clsEncryption
            Try
                ''SLR :Assign to a variable and then close & free
                'regKeySettings = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, Security.AccessControl.RegistryRights.FullControl)
                'If IsNothing(regKeySettings) = True Then
                '    Return False
                'End If
                'regKeySettings.Close()
                'regKeySettings.Dispose()
                'regKeySettings = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, True)
                'If IsNothing(regKeySettings.GetValue("SQLServer")) = True Then
                '    Return False
                'End If
                'If IsNothing(regKeySettings.GetValue("Database")) = True Then
                '    Return False
                'End If
                'If IsNothing(regKeySettings.GetValue("SQLUser")) = True Then
                '    Return False
                'End If
                'If IsNothing(regKeySettings.GetValue("SQLPassword")) = True Then
                '    Return False
                'End If
                'objEncryption = New clsEncryption
                Dim gstrSQLServerName As String = ""
                Dim gstrDatabaseName As String = ""
                Dim gstrUserId As String = ""
                Dim gstrPassword As String = ""

                gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ReadFromServicesRegistry(gstrSQLServerName, gstrDatabaseName, gstrUserId, gstrPassword)


                Return "SERVER=" & gstrSQLServerName & "; DATABASE= " & gstrDatabaseName & " ;USER id=" & gstrUserId & "; Password=" & gstrPassword
                '      Return "SERVER=" & Convert.ToString(regKeySettings.GetValue("SQLServer")) & "; DATABASE= " & Convert.ToString(regKeySettings.GetValue("Database")) & " ;USER id=" & Convert.ToString(regKeySettings.GetValue("SQLUser")) & "; Password=" & objEncryption.DecryptFromBase64String(Convert.ToString(regKeySettings.GetValue("SQLPassword")), mdlGeneral.constEncryptDecryptKeyRegistry)
            Catch ex As Exception
                mdlGeneral.UpdateLog("Error: gloService connection is not Set.")
                Return Nothing
            Finally
                'If Not IsNothing(regKeySettings) Then
                '    regKeySettings.Close()
                '    regKeySettings.Dispose()
                '    regKeySettings = Nothing
                'End If
                'If Not IsNothing(objEncryption) Then
                '    objEncryption = Nothing
                'End If
                'SLR: Fre objencryptun
            End Try
        End Function
        Public Function ReadSettings(ByVal strConnection As String) As DataTable
            'Dim objSettings As New ClsSettings()
            Dim oDbLayer As New gloDatabaseLayer.DBLayer(strConnection)
            Dim dtSettingsTable As New DataTable
            Dim _strQuery As String = String.Empty
            Dim intRecCnt As Int32 = 0
            Try
                ' Connect to Database s
                If Connect(strConnection) = False Then
                    For intRecCnt = 0 To 100
                        If Connect(strConnection) = True Then
                            Exit For
                        Else
                            System.Threading.Thread.Sleep(1000)
                            Continue For
                        End If
                        'oDbLayer.Connect(False)
                    Next
                End If

                oDbLayer.Connect(False)
                '_strQuery = "select sSettingsName,sSettingsValue from GLSettings where nReferenceId='0' or nReferenceId is null"
                _strQuery = "select sSettingsName,sSettingsValue from GLSettings "

                oDbLayer.Retrive_Query(_strQuery, dtSettingsTable)

                ' Connect to Database 
                oDbLayer.Disconnect()
                _strQuery = String.Empty

            Catch ex As Exception
                UpdateLog(ex.ToString())

                dtSettingsTable = Nothing
                _strQuery = String.Empty

            Finally
                intRecCnt = 0
                If oDbLayer IsNot Nothing Then
                    oDbLayer.Disconnect()
                    oDbLayer.Dispose()
                End If
            End Try


            Return dtSettingsTable

        End Function

        
        'Public Function checkSqlServerStarted() As Boolean
        '    'Dim sc As New System.ServiceProcess.ServiceController("MSSQL$SQLEXPRESS")
        '    If Not isSqlServerStarted Then
        '        If Not isProcessInQueue Then

        '            'System.Threading.Thread.Sleep(60000)

        '            isProcessInQueue = True
        '            Dim processes As Process() = Nothing
        '            processes = Process.GetProcesses()
        '            While Not isSqlServerStarted
        '                For Each instance_loopVariable As Process In processes
        '                    If instance_loopVariable.ProcessName.ToLower() = "sqlservr" Then
        '                        isSqlServerStarted = True
        '                        Exit For
        '                    End If
        '                    'If sc.Status = System.ServiceProcess.ServiceControllerStatus.Running Then
        '                    '    isSqlServerStarted = True
        '                    '    Exit For
        '                    'Else
        '                    '    isSqlServerStarted = False
        '                    'End If

        '                Next
        '            End While
        '        End If
        '    End If
        '    Return isSqlServerStarted
        'End Function

        Public Function getDBCount(ByVal strConnection As String) As Int16
            Dim oDbLayer As New gloDatabaseLayer.DBLayer(strConnection)
            Dim _strQuery As String = String.Empty
            Dim iCnt As Int16 = 0
            Try
                If Connect(strConnection) = False Then
                    gblDBCount = 0
                    Return iCnt
                End If
                oDbLayer.Connect(False)
                _strQuery = "SELECT count(nDBConnectionId) as nDBConnectionId FROM DBSettings where sServiceName='RxSniffer' "

                iCnt = oDbLayer.ExecuteScalar_Query(_strQuery)
                gblDBCount = iCnt

                _strQuery = String.Empty

            Catch ex As Exception
                UpdateLog(ex.ToString())
                _strQuery = String.Empty

            Finally

                If oDbLayer IsNot Nothing Then
                    oDbLayer.Disconnect()
                    oDbLayer.Dispose()
                End If
            End Try


            Return iCnt

        End Function

        Public Function Connect(ByVal strConnection As String) As Boolean
            Dim _objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Try
                _objSqlConnection = New System.Data.SqlClient.SqlConnection(strConnection)
                _objSqlConnection.Open()

                If _objSqlConnection.State = ConnectionState.Open Then
                    _objSqlConnection.Close()
                End If
                Return True
            Catch ex As Exception
                Return False
            Finally
                If Not IsNothing(_objSqlConnection) Then
                    _objSqlConnection.Dispose()
                    _objSqlConnection = Nothing
                End If
            End Try
        End Function
    End Module
End Namespace

