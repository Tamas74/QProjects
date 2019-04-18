Imports System.ServiceProcess
Imports System.Windows.Forms
Imports RxSniffer.RxGeneral
Imports Microsoft.Win32
Imports gloInstallerCommandParametrs

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RxWinService
    Inherits System.ServiceProcess.ServiceBase
    Private Shared WithEvents mobNotifyIcon As NotifyIcon
    ' Private Shared mobContextMenu As ContextMenu
    Private Shared cntRxMenu As ContextMenuStrip
    Private Shared mobTimer As System.Timers.Timer
    Private Shared mobServiceController As System.ServiceProcess.ServiceController
    Private Shared isSqlServerStarted As Boolean = False
    'UserService overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    Public Shared MyServiceName As String = Nothing
    ' The main entry point for the process

    '<STAThread()> _
    <MTAThread()> _
    <System.Diagnostics.DebuggerNonUserCode()> _
    Shared Sub Main(ByVal sArgs As String())

        'Application.Run(New TestForm())
        'Application.Run(New frmViewDBCredentials())
        'Application.Run(New frmSettings())
        'GetHl7LogSettings()
        'Return
        gloInstaller.gloInstallerCommandParameters.ServiceName = "RxSniffer"
        gloInstaller.gloInstallerCommandParameters.DisplayName = "RxSniffer"

        If sArgs.Length > 0 Then
            Select Case sArgs(0).ToLower()
                Case "-silent"
                    MyServiceName = gloInstaller.gloInstallerCommandParameters.DynamicServiceName
                    DisplayIcon()
                    Return
                Case "-settings"
                    MyServiceName = gloInstaller.gloInstallerCommandParameters.DynamicServiceName
                    Application.Run(New frmSettings())
                    Return
                Case Else
                    MyServiceName = gloInstaller.gloInstallerCommandParameters.GetServiceNameFromContextParameters(sArgs)
                    If (String.IsNullOrEmpty(MyServiceName)) Then
                        Return
                    End If
            End Select
        End If
        If (String.IsNullOrEmpty(MyServiceName)) Then
            MyServiceName = gloInstaller.gloInstallerCommandParameters.DynamicServiceName
        End If
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        Dim newRxWinService As RxWinService = New RxWinService
        newRxWinService.ServiceName = MyServiceName
        ServicesToRun = New System.ServiceProcess.ServiceBase() {newRxWinService}
        System.ServiceProcess.ServiceBase.Run(ServicesToRun)

    End Sub
    Private Shared Sub DisplayIcon()
        Try
            ''SET UP CONECTION TO SERVICE. 
            'gloInstaller.gloInstallerCommandParameters.ServiceName = "RxSniffer"
            'gloInstaller.gloInstallerCommandParameters.DisplayName = "RxSniffer"
            Try
                If (IsNothing(mobServiceController) = False) Then
                    mobServiceController.Dispose()
                End If
            Catch ex As Exception

            End Try
           
            mobServiceController = New System.ServiceProcess.ServiceController(MyServiceName)
            ''KEEP NOTIFY ICON HIDDEN UNTIL ICON AND MENU IS SET. 
            mobNotifyIcon = New NotifyIcon()
            ''// Added by Ujwala - to Add service icon for user who has installed it - on 05022016
            ''  ////////// gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.AddApplicationToStartup(Application.ProductName, Application.ExecutablePath);
            mdlGeneral.UpdateLog(" In DisplayIcon: Executable path: " + gloGlobal.gloServicesCommon.ExecutablePath + " ProductName: " + gloGlobal.gloServicesCommon.ProductName)
            mdlGeneral.UpdateLog(" In DisplayIcon: Application.ExecutablePath: " + Application.ExecutablePath + " Application.ProductName: " + Application.ProductName)
            gloGlobal.gloServicesCommon.AddApplicationToStartup()
            ''// Added by Ujwala - to Add service icon for user who has installed it - End - on 05022016
            mobNotifyIcon.Icon = RxSniffer.My.Resources.RxSniffer
            mobNotifyIcon.Visible = False
            mobNotifyIcon.Text = "RxSniffer Service"

            AddHandler mobNotifyIcon.BalloonTipClicked, AddressOf mobNotifyIcon_BalloonTipClicked

            '  mobContextMenu = New ContextMenu()
            CreateMenu()
            ' mobNotifyIcon.ContextMenu = mobContextMenu
            mobNotifyIcon.ContextMenuStrip = cntRxMenu
            SetUpTimer()
            mobNotifyIcon.Visible = True

            Application.Run()
        Catch obEx As Exception
            RxGeneral.mdlGeneral.UpdateLog(obEx.ToString)
        End Try
    End Sub

    Private Shared Sub mobNotifyIcon_BalloonTipClicked(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        '    If Not IsSettingFormOpened Then
        '        IsSettingFormOpened = True
        '        Dim oSettings As New frmSettings
        '        oSettings.ShowDialog()
        '        oSettings.Dispose()
        '        IsSettingFormOpened = False
        '    End If
        'Catch obEx As Exception
        '    UpdateLog(obEx.ToString())
        'End Try

    End Sub

    Private Shared Sub SetUpTimer()
        Try
            mobTimer = New System.Timers.Timer()
            AddHandler mobTimer.Elapsed, AddressOf mobTimer_Elapsed
            mobTimer.AutoReset = True
            mobTimer.Interval = 2000
            mobTimer.Start()
        Catch obEx As Exception
            RxGeneral.mdlGeneral.UpdateLog(obEx.ToString)
        End Try
    End Sub
    Private Shared Sub CreateMenu()

        Try
            Dim oChildMenu As New ToolStripMenuItem
            cntRxMenu = New ContextMenuStrip
            cntRxMenu.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            cntRxMenu.ShowImageMargin = False
            cntRxMenu.ShowCheckMargin = False
            oChildMenu = New ToolStripMenuItem
            oChildMenu.Text = "Settings"
            oChildMenu.Name = "Settings"
            oChildMenu.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            AddHandler oChildMenu.Click, AddressOf ServiceSettings
            cntRxMenu.Items.Add(oChildMenu)
            oChildMenu = Nothing

            cntRxMenu.Items.Add("-")



            oChildMenu = New ToolStripMenuItem
            oChildMenu.Text = "Database Connections"
            oChildMenu.Name = "Database Connections"
            oChildMenu.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            AddHandler oChildMenu.Click, AddressOf DBConnections
            cntRxMenu.Items.Add(oChildMenu)
            oChildMenu = Nothing

            cntRxMenu.Items.Add("-")




            oChildMenu = New ToolStripMenuItem
            oChildMenu.Text = "Start Service"
            oChildMenu.Name = "Start"
            AddHandler oChildMenu.Click, AddressOf StartService
            cntRxMenu.Items.Add(oChildMenu)
            oChildMenu = Nothing

            oChildMenu = New ToolStripMenuItem
            oChildMenu.Text = "Stop Service"
            oChildMenu.Name = "Stop"
            AddHandler oChildMenu.Click, AddressOf StopService
            cntRxMenu.Items.Add(oChildMenu)
            oChildMenu = Nothing


            cntRxMenu.Items.Add("-")

            oChildMenu = New ToolStripMenuItem
            oChildMenu.Text = "About RxSniffer"
            oChildMenu.Name = "About"
            AddHandler oChildMenu.Click, AddressOf AboutBox
            cntRxMenu.Items.Add(oChildMenu)
            oChildMenu = Nothing

            oChildMenu = New ToolStripMenuItem()
            oChildMenu.Text = "Help"
            oChildMenu.Name = "Help"
            AddHandler oChildMenu.Click, AddressOf HelpController
            cntRxMenu.Items.Add(oChildMenu)
            oChildMenu = Nothing

            cntRxMenu.Items.Add("-")

            oChildMenu = New ToolStripMenuItem
            oChildMenu.Text = "Exit"
            oChildMenu.Name = "Exit"
            AddHandler oChildMenu.Click, AddressOf ExitController
            cntRxMenu.Items.Add(oChildMenu)
            oChildMenu = Nothing

            'mobContextMenu.MenuItems.Add(New MenuItem("Settings", New EventHandler(AddressOf ServiceSettings)))
            'mobContextMenu.MenuItems.Add("-")
            'mobContextMenu.MenuItems.Add(New MenuItem("Stop Service", New EventHandler(AddressOf StopService)))
            'mobContextMenu.MenuItems.Add(New MenuItem("Start Service", New EventHandler(AddressOf StartService)))
            'mobContextMenu.MenuItems.Add("-")
            'mobContextMenu.MenuItems.Add(New MenuItem("About RxSniffer", New EventHandler(AddressOf AboutBox)))
            'mobContextMenu.MenuItems.Add(New MenuItem("Exit", New EventHandler(AddressOf ExitController)))

        Catch obEx As Exception
            mdlGeneral.UpdateLog(obEx.ToString)
        End Try
    End Sub



    Private Shared Sub DBConnections(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If mdlGeneral.blnOpen Then
                Exit Sub
            End If
            mdlGeneral.blnOpen = True
            If mdlGeneral.IsSettings() Then
                Dim objViewDBCredentials As New frmViewDBCredentials()
                objViewDBCredentials.ShowDialog()
            Else
                MessageBox.Show("Please configure RxSniffer service", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch obEx As Exception
            mdlGeneral.UpdateLog(obEx.ToString)
        Finally
            mdlGeneral.blnOpen = False
        End Try
    End Sub

    Private Shared Sub StopService(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If mobServiceController.Status = System.ServiceProcess.ServiceControllerStatus.Running Then
                If mobServiceController.CanStop = True Then
                    mobServiceController.Stop()
                    ' mobNotifyIcon.Icon = My.Resources.gloRxSnifferStopped
                    If gblDBCount = 0 Then
                        mobNotifyIcon.Text = "RxSniffer Service - is not configured."
                        mobNotifyIcon.Icon = My.Resources.gloRxSnifferNotConfig
                    End If
                End If
            End If
        Catch obEx As Exception
            mdlGeneral.UpdateLog(obEx.ToString)
        End Try
    End Sub
    Private Shared Sub ServiceSettings(ByVal sender As Object, ByVal e As EventArgs)
        openSettings()
    End Sub
    Public Shared Sub ApplicationStartup()
        Try
            gloInstaller.gloInstallerCommandParameters.ServiceName = "RxSniffer"
            gloInstaller.gloInstallerCommandParameters.DisplayName = "RxSniffer"
            Try
                If (IsNothing(mobServiceController) = False) Then
                    mobServiceController.Dispose()
                End If
            Catch ex As Exception

            End Try
            mobServiceController = New System.ServiceProcess.ServiceController(MyServiceName)
            ''KEEP NOTIFY ICON HIDDEN UNTIL ICON AND MENU IS SET. 
            mobNotifyIcon = New NotifyIcon()
            ''// Added by Ujwala - to Add service icon for user who has installed it - on 05022016
            ''  ////////// gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.AddApplicationToStartup(Application.ProductName, Application.ExecutablePath);
            mdlGeneral.UpdateLog(" In DisplayIcon: Executable path: " + gloGlobal.gloServicesCommon.ExecutablePath + " ProductName: " + gloGlobal.gloServicesCommon.ProductName)
            mdlGeneral.UpdateLog(" In DisplayIcon: Application.ExecutablePath: " + Application.ExecutablePath + " Application.ProductName: " + Application.ProductName)
            gloGlobal.gloServicesCommon.AddApplicationToStartup()
            ''// Added by Ujwala - to Add service icon for user who has installed it - End - on 05022016
            mobNotifyIcon.Icon = RxSniffer.My.Resources.RxSniffer
            mobNotifyIcon.Visible = False
            ' mobContextMenu = New ContextMenu()
            CreateMenu()
            ' mobNotifyIcon.ContextMenu = mobContextMenu
            mobNotifyIcon.ContextMenuStrip = cntRxMenu
            SetUpTimer()
            mobNotifyIcon.Visible = True
            If mobServiceController.Status = System.ServiceProcess.ServiceControllerStatus.Running Then
                If mobServiceController.CanStop = True Then
                    mobServiceController.Stop()
                End If
            End If
            If mobServiceController.Status = System.ServiceProcess.ServiceControllerStatus.Stopped Then
                mobServiceController.Start()
            End If
        Catch obEx As Exception
            mdlGeneral.UpdateLog(obEx.ToString)
        End Try
    End Sub
    Public Shared Sub StartService(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If mobServiceController.Status = System.ServiceProcess.ServiceControllerStatus.Stopped Then
                mobServiceController.Start()
                ' mobNotifyIcon.Icon = My.Resources.gloRxSnifferRunning
                If gblDBCount = 0 Then
                    mobNotifyIcon.Text = "RxSniffer Service - is not configured."
                    mobNotifyIcon.Icon = My.Resources.gloRxSnifferNotConfig
                End If
            End If
        Catch obEx As Exception
            mdlGeneral.UpdateLog(obEx.ToString)
        End Try
    End Sub

    Private Shared Sub AboutBox(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If mdlGeneral.blnOpen Then
                Exit Sub
            End If
            mdlGeneral.blnOpen = True
            'Comment the code to call old FrmAbout by Rohit on 20110125 
            ' Dim objAbt As New frmAbout()
            'Added new FrmAbout Us form calling
            Dim objAbt As New frmAboutus()
            objAbt.ShowDialog()
            objAbt.Dispose()
        Catch obEx As Exception
            mdlGeneral.UpdateLog(obEx.ToString)
        Finally
            mdlGeneral.blnOpen = False
        End Try
    End Sub

    Private Shared Sub HelpController(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'calling Help form
            Dim helpFileName As String = System.IO.Path.Combine(Application.StartupPath, "Help\\gloRxSniffer.chm")
            If System.IO.File.Exists(helpFileName) Then

                Help.ShowHelp(Nothing, Convert.ToString("file://") & helpFileName, "Welcome_User_Manual.htm")
                Help.ShowHelp(Nothing, Convert.ToString("file://") & helpFileName, HelpNavigator.TableOfContents, "Welcome_User_Manual.htm")
            End If
        Catch ex As Exception
            mdlGeneral.UpdateLog("Exiting Help()  with error " + ex.ToString())
        End Try

    End Sub

    Private Shared Sub ExitController(ByVal sender As Object, ByVal e As EventArgs)
        Try
            mobTimer.Stop()
            mobTimer.Dispose()
            mobNotifyIcon.Visible = False
            mobNotifyIcon.Dispose()
            Try
                If (IsNothing(mobServiceController) = False) Then
                    mobServiceController.Dispose()
                End If
            Catch ex As Exception

            End Try
            ''// Added by Ujwala - to Add service icon for user who has installed it - on 05022016
            mdlGeneral.UpdateLog("Calling RemoveApplicationFromStartup ")
            ''////// gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.RemoveApplicationFromStartup(Application.ProductName);
            gloGlobal.gloServicesCommon.RemoveApplicationFromStartup()
            mdlGeneral.UpdateLog("Exiting gloRxSniffer service ")
            ''// Added by Ujwala - to Add service icon for user who has installed it - End - on 05022016       
            Application.Exit()
        Catch obEx As Exception
            mdlGeneral.UpdateLog(obEx.ToString)
        End Try
    End Sub

    Public Shared Sub mobTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)

        Try
            mobTimer.Enabled = False
            Select Case mobServiceController.Status
                Case System.ServiceProcess.ServiceControllerStatus.Running
                    ' mobNotifyIcon.Icon = new Icon("Running.ico"); 
                    'mobContextMenu.MenuItems(2).Enabled = True
                    'mobContextMenu.MenuItems(3).Enabled = False
                    cntRxMenu.Items("Start").Enabled = False
                    cntRxMenu.Items("Stop").Enabled = True
                    mobNotifyIcon.Text = "gloRxSniffer - Running"
                    mobNotifyIcon.Icon = My.Resources.gloRxSnifferRunning
                    Exit Select
                Case System.ServiceProcess.ServiceControllerStatus.Stopped
                    ' mobNotifyIcon.Icon = new Icon("Stopped.ico"); 
                    'mobContextMenu.MenuItems(2).Enabled = False
                    'mobContextMenu.MenuItems(3).Enabled = True
                    cntRxMenu.Items("Start").Enabled = True
                    cntRxMenu.Items("Stop").Enabled = False
                    mobNotifyIcon.Text = "gloRxSniffer - Stopped"
                    mobNotifyIcon.Icon = My.Resources.gloRxSnifferStopped
                    Exit Select

                    ''///FINALLY CHECK IF PAUSE & CONTINUE IS POSSIBLE ON THIS SERVICE. 
                    'if (mobServiceController.CanPauseAndContinue == false) 
                    '{ 
                    ' mobContextMenu.MenuItems(1).Enabled = false; 
                    ' mobContextMenu.MenuItems(2).Enabled = false; 
                    '} 
            End Select
            'Dim processes As Process() = Nothing
            'processes = Process.GetProcesses()
            'While Not isSqlServerStarted
            '    For Each instance_loopVariable As Process In processes
            '        If instance_loopVariable.ProcessName.ToLower() = "sqlservr" Then
            '            isSqlServerStarted = True
            '            Exit For
            '        End If
            '    Next
            'End While
            'If mdlGeneral.checkSqlServerStarted() Then
            If mobServiceController.Status = System.ServiceProcess.ServiceControllerStatus.Stopped Then
                mobNotifyIcon.Text = "gloRxSniffer - Stopped"
                mobNotifyIcon.Icon = My.Resources.gloRxSnifferStopped
            Else

                If Not IsValidregistry() Then
                    mobNotifyIcon.Icon = My.Resources.gloRxSnifferNotConfig
                    mobNotifyIcon.Text = "gloRxSniffer - Not configured"
                    'mobNotifyIcon.BalloonTipText = "Please configure RxSniffer settings"
                    'mobNotifyIcon.BalloonTipTitle = "RxSniffer Service"
                    'mobNotifyIcon.BalloonTipIcon = ToolTipIcon.Info
                    'mobNotifyIcon.ShowBalloonTip(5000)
                    'System.Threading.Thread.Sleep(10000)
                    Exit Sub
                Else
                    'mdlGeneral.SetDbCredentials()

                    If gblDBCount = 0 Then
                        mobNotifyIcon.Text = "gloRxSniffer - Not configured"
                        mobNotifyIcon.Icon = My.Resources.gloRxSnifferNotConfig
                    End If
                End If
            End If
            'End If
            ''/REFRESH PROPERTIES BEFORE READING. 
            mobServiceController.Refresh()
            ''/REFLECT STATUS IN THE CONTEXT MENU. 

        Catch obEx As Exception
            mdlGeneral.UpdateLog(obEx.ToString)
        Finally
            mobTimer.Enabled = True
        End Try

    End Sub
    'Public Shared Function IsValidregistry() As Boolean
    '    Dim _regKey As RegistryKey
    '    If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloServices")) = True Then
    '        _regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
    '        _regKey.CreateSubKey("gloServices")
    '        _regKey.Close()
    '    End If

    '    If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloServices")) = True Then
    '        Return False
    '    End If
    '    _regKey = Registry.LocalMachine.OpenSubKey("Software\gloServices", True)

    '    If Not IsNothing(_regKey) Then

    '        If IsNothing(_regKey.GetValue("SQLServer")) = False Then
    '            '_objSettingsField.SQLServer = _regKey.GetValue("SQLServer")
    '        Else
    '            Return False
    '        End If
    '        If IsNothing(_regKey.GetValue("Database")) = False Then
    '            '_objSettingsField.Database = _regKey.GetValue("Database")
    '        Else
    '            Return False
    '        End If
    '        If IsNothing(_regKey.GetValue("SQLUser")) = False Then
    '            '_objSettingsField.User = _regKey.GetValue("SQLUser")
    '        Else
    '            Return False
    '        End If
    '        If IsNothing(_regKey.GetValue("SQLPassword")) = False Then
    '            'Dim strPassword As String
    '            'strPassword = _regKey.GetValue("SQLPassword")
    '            'objEncryption = New clsgloEncryption
    '            '_objSettingsField.Password = objEncryption.DecryptFromBase64String(strPassword, Clsconnect.constEncryptDecryptKey)
    '        Else
    '            Return False
    '        End If
    '    End If
    '    Return True
    'End Function

    Public Shared Function IsValidregistry() As Boolean
        'Dim _regKey As RegistryKey
        'Dim strServerName As String = String.Empty
        'Dim strDatabaseName As String = String.Empty
        'Dim strUserName As String = String.Empty
        'Dim strPassword As String = String.Empty
        Dim strConnectionString As String = String.Empty
        Dim dtSettings As DataTable = Nothing
        '_regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName)
        'If IsNothing(_regKey) = True Then
        '    _regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
        '    _regKey.CreateSubKey(gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName)

        'End If
        'If (IsNothing(_regKey) = False) Then
        '    _regKey.Close()
        '    _regKey.Dispose()
        'End If
        '_regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName)

        'If IsNothing(_regKey) = True Then
        '    Return False
        'End If
        'If (IsNothing(_regKey) = False) Then
        '    _regKey.Close()
        '    _regKey.Dispose()
        'End If
        '_regKey = Registry.LocalMachine.OpenSubKey("Software\" & gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, True)
        'Try
        '    If Not IsNothing(_regKey) Then

        '        If IsNothing(_regKey.GetValue("SQLServer")) = False Then
        '            '_objSettingsField.SQLServer = _regKey.GetValue("SQLServer")
        '            strServerName = _regKey.GetValue("SQLServer")
        '        Else
        '            Return False
        '        End If
        '        If IsNothing(_regKey.GetValue("Database")) = False Then
        '            '_objSettingsField.Database = _regKey.GetValue("Database")
        '            strDatabaseName = _regKey.GetValue("Database")
        '        Else
        '            Return False
        '        End If
        '        If IsNothing(_regKey.GetValue("SQLUser")) = False Then
        '            strUserName = _regKey.GetValue("SQLUser")
        '        Else
        '            Return False
        '        End If
        '        If IsNothing(_regKey.GetValue("SQLPassword")) = False Then
        '            'Dim strPassword As String
        '            strPassword = _regKey.GetValue("SQLPassword")
        '            Dim objEncryption As clsEncryption
        '            objEncryption = New clsEncryption
        '            strPassword = objEncryption.DecryptFromBase64String(strPassword, mdlGeneral.constEncryptDecryptKeyRegistry)
        '        Else
        '            Return False
        '        End If
        Try

            Dim gstrSQLServerName As String = ""
            Dim gstrDatabaseName As String = ""
            Dim gstrUserId As String = ""
            Dim gstrPassword As String = ""

            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ReadFromServicesRegistry(gstrSQLServerName, gstrDatabaseName, gstrUserId, gstrPassword)

            If (String.IsNullOrEmpty(gstrSQLServerName) OrElse String.IsNullOrEmpty(gstrDatabaseName) OrElse String.IsNullOrEmpty(gstrUserId)) Then
                Return False
            End If
            strConnectionString = "SERVER=" & gstrSQLServerName & "; DATABASE= " & gstrDatabaseName & " ;USER id=" & gstrUserId & "; Password=" & gstrPassword
            '  strConnectionString = "SERVER=" & strServerName & "; DATABASE= " & strDatabaseName & " ;USER id=" & strUserName & "; Password=" & strPassword
            dtSettings = mdlGeneral.ReadSettings(strConnectionString)
            If Not IsNothing(dtSettings) Then
                gblDBCount = mdlGeneral.getDBCount(strConnectionString)
            Else
                gblDBCount = 0
            End If


            Dim _dvSettingstest As DataView
            If (Not IsNothing(dtSettings)) AndAlso dtSettings.Rows.Count > 0 Then
                _dvSettingstest = dtSettings.DefaultView
                _dvSettingstest.RowFilter = "sSettingsName='ServiceInterval'"

                If _dvSettingstest.Count = 0 Then
                    Return False
                End If

                For Each oDataRow As DataRow In dtSettings.Rows
                    Select Case Convert.ToString(Convert.ToString(oDataRow("sSettingsName"))).ToLower()
                        Case "serviceinterval"
                            If (IsNothing(oDataRow.Item("sSettingsValue")) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "")) Then
                                Return False
                            End If
                        Case "dirservice"
                            If (IsNothing(oDataRow.Item("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                                Return False
                            End If
                        Case "prescriberfulldownload"
                            If (IsNothing(oDataRow.Item("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                                Return False
                            End If
                        Case "fulldownloadday"
                            If (IsNothing(oDataRow.Item("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                                Return False
                            End If
                        Case "fulldownloadinterval"
                            If (IsNothing(oDataRow.Item("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                                Return False
                            End If
                        Case "nightlydownloadinterval"
                            If (IsNothing(oDataRow.Item("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                                Return False
                            End If
                        Case "stagingserver"
                            If (IsNothing(oDataRow.Item("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                                Return False
                            End If
                        Case "rxsnifferlogarchive"
                            If (IsNothing(oDataRow.Item("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                                Return False
                            End If
                        Case "rxsniffermaxlogsize"
                            If (IsNothing(oDataRow.Item("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                                Return False
                            End If
                            'Case "rxsnifferlogarchivepath"
                            '    If (Not IsNothing(oDataRow.Item("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                            '        Return False
                            '    End If

                        Case "enableautoeligibility"
                            If (IsNothing(oDataRow("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                                Return False
                            End If
                        Case "generaterxeligibilitylog"
                            If (IsNothing(oDataRow("sSettingsValue"))) AndAlso (oDataRow.Item("sSettingsValue").ToString.Trim() = "") Then
                                Return False
                            End If

                    End Select
                Next
                Try
                    _dvSettingstest.Dispose()
                    _dvSettingstest = Nothing
                Catch ex As Exception

                End Try
            Else
                Return False
            End If
            '     End If

        Catch ex As Exception
        Finally
            'If (IsNothing(_regKey) = False) Then
            '    _regKey.Close()
            '    _regKey.Dispose()
            'End If
            If (IsNothing(dtSettings) = False) Then
                dtSettings.Dispose()
                dtSettings = Nothing
            End If
        End Try
        Return True
    End Function

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        '
        'RxWinService
        '
        'gloInstaller.gloInstallerCommandParameters.ServiceName = "RxSniffer"
        'gloInstaller.gloInstallerCommandParameters.DisplayName = "RxSniffer"
        '  Me.ServiceName = gloInstaller.gloInstallerCommandParameters.DynamicServiceName

    End Sub

    Private Shared Sub mobNotifyIcon_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mobNotifyIcon.MouseDoubleClick
        openSettings()
    End Sub
    Public Shared Sub openSettings()
        Try
            If mdlGeneral.blnOpen Then
                Exit Sub
            End If
            mdlGeneral.blnOpen = True
            Dim oSettings As New frmSettings()
            oSettings.ShowDialog()
            mdlGeneral.blnOpen = False
        Catch obEx As Exception
            mdlGeneral.UpdateLog(obEx.ToString)
        Finally

        End Try
    End Sub
End Class
