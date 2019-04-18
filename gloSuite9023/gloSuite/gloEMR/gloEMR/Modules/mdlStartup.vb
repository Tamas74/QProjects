Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.Management
Imports gloSettings
Module mdlStartup
    'Sub Main()

    '    Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName


    '    If GetOSInfo() = False Then
    '        Dim aProcName As String = System.IO.Path.GetFileNameWithoutExtension(aModuleName)
    '        If Process.GetProcessesByName(aProcName).Length > 1 Then
    '            MessageBox.Show("Another instance of this application is already running.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            Application.Exit()
    '            End
    '            Exit Sub
    '        End If
    '    End If

    '    If IsSettings() = False Then
    '        Dim frmSettings As New frmStartupSettings
    '        If frmSettings.ShowDialog() = DialogResult.OK Then
    '            Dim frmgloEMRLogin As New frmLogin
    '            frmgloEMRLogin.ShowDialog()
    '        End If
    '    End If
    '    Dim objSettings As New clsStartUpSettings
    '    If objSettings.IsConnect(gstrSQLServerName, gstrDatabaseName) = False Then
    '        If MessageBox.Show("Unable to connect to SQL Server " & gstrSQLServerName & " and Database " & gstrDatabaseName & vbCrLf & "Do you want to change SQL Server or Database Settings?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
    '            End
    '        End If
    '        Dim frmSettings As New frmStartupSettings
    '        If frmSettings.ShowDialog() = DialogResult.OK Then
    '            Dim frmgloEMRLogin1 As New frmLogin
    '            frmgloEMRLogin1.ShowDialog()
    '        End If
    '        Exit Sub
    '    End If
    '    Dim frmgloEMRLogin2 As New frmLogin
    '    frmgloEMRLogin2.ShowDialog()

    'End Sub
    Public Function GetOSInfo() As Boolean
        Try

            Using objMOS As New ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem")
                For Each objMgmt As ManagementObject In objMOS.Get()
                    Dim sep As Char() = {"|"}
                    Dim strOsName As String() = objMgmt("Name").ToString.Split(sep)
                    If strOsName.Length > 0 Then
                        ''''Change for 64 - bit server
                        'If strOsName(0).Contains("Microsoft Windows Server") Then
                        If strOsName(0).Contains("Microsoft") And strOsName(0).Contains("Windows") And strOsName(0).Contains("Server") Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Next
                Return Nothing
            End Using

        Catch ex As Exception
            UpdateLog(ex.ToString)
            Return False
        Finally

        End Try

    End Function

    'Private Function IsSettings() As Boolean
    '    'Dim regKey As RegistryKey
    '    'If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
    '    '    Return False
    '    'End If
    '    'regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
    '    'If IsNothing(regKey.GetValue("SQLServer")) = True Then
    '    '    regKey.Close()
    '    '    Return False
    '    'End If
    '    'If IsNothing(regKey.GetValue("Database")) = True Then
    '    '    regKey.Close()
    '    '    Return False
    '    'End If
    '    'gstrSQLServerName = regKey.GetValue("SQLServer")
    '    'gstrDatabaseName = regKey.GetValue("Database")
    '    'If IsNothing(regKey.GetValue("DefaultPatientCode")) = True Then
    '    '    regKey.SetValue("DefaultPatientCode", "")
    '    'Else
    '    '    gstrPatientCode = regKey.GetValue("DefaultPatientCode")
    '    'End If
    '    'If IsNothing(regKey.GetValue("FAXPrinterName")) = False Then
    '    '    gstrFAXPrinterName = regKey.GetValue("FAXPrinterName")
    '    'End If
    '    'If IsNothing(regKey.GetValue("FAXOutputDirectory")) = False Then
    '    '    gstrFAXOutputDirectory = regKey.GetValue("FAXOutputDirectory")
    '    'End If
    '    If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
    '        Return False
    '    End If
    '    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
    '    If IsNothing(gloRegistrySetting.GetRegistryValue("SQLServer")) = True Then
    '        'regKey.Close()
    '        gloRegistrySetting.CloseRegistryKey()
    '        Return False
    '    End If
    '    If IsNothing(gloRegistrySetting.GetRegistryValue("Database")) = True Then
    '        'regKey.Close()
    '        gloRegistrySetting.CloseRegistryKey()
    '        Return False
    '    End If
    '    gstrSQLServerName = gloRegistrySetting.GetRegistryValue("SQLServer")
    '    gstrDatabaseName = gloRegistrySetting.GetRegistryValue("Database")
    '    If IsNothing(gloRegistrySetting.GetRegistryValue("DefaultPatientCode")) = True Then
    '        gloRegistrySetting.SetRegistryValue("DefaultPatientCode", "")
    '    Else
    '        gstrPatientCode = gloRegistrySetting.GetRegistryValue("DefaultPatientCode")
    '    End If
    '    If IsNothing(gloRegistrySetting.GetRegistryValue("FAXPrinterName")) = False Then
    '        gstrFAXPrinterName = gloRegistrySetting.GetRegistryValue("FAXPrinterName")
    '    End If
    '    If IsNothing(gloRegistrySetting.GetRegistryValue("FAXOutputDirectory")) = False Then
    '        gstrFAXOutputDirectory = gloRegistrySetting.GetRegistryValue("FAXOutputDirectory")
    '    End If

    '    'Check FAX Printers necessary settings are set or not
    '    gblnFAXPrinterSettingsSet = isPrinterSettingsSet()
    '    'Set the FAX Printer Settings - i.e. FAX Printer Name, FAX  Output Directory

    '    'sarika 7
    '    ' SetFAXPrinterDefaultSettings()
    '    MainMenu.SetFAXPrinterDefaultSettings1()
    '    '-----------

    '    'Get FAX Cover Page
    '    'If IsNothing(regKey.GetValue("FAXCoverPage")) = False Then
    '    '    If regKey.GetValue("FAXCoverPage") = "1" Then
    '    If IsNothing(gloRegistrySetting.GetRegistryValue("FAXCoverPage")) = False Then
    '        If gloRegistrySetting.GetRegistryValue("FAXCoverPage") = "1" Then
    '            gblnFAXCoverPage = True
    '        Else
    '            gblnFAXCoverPage = False
    '        End If
    '    Else
    '        gblnFAXCoverPage = False
    '    End If

    '    '<Vinayak-Chnage for if Path is Drive then remove "\" - 27 Dec 2005>
    '    'If IsNothing(regKey.GetValue("DMSPath")) = False Then
    '    '    DMSRootPath = regKey.GetValue("DMSPath")
    '    If IsNothing(gloRegistrySetting.GetRegistryValue("DMSPath")) = False Then
    '        DMSRootPath = gloRegistrySetting.GetRegistryValue("DMSPath")
    '        If Mid(DMSRootPath, Len(DMSRootPath)) = "\" Then
    '            DMSRootPath = Mid(DMSRootPath, 1, Len(DMSRootPath) - 1)
    '        End If
    '    End If

    '    'If IsNothing(regKey.GetValue("ServerPath")) = False Then
    '    '    gstrServerPath = regKey.GetValue("ServerPath")
    '    'End If
    '    If IsNothing(gloRegistrySetting.GetRegistryValue("ServerPath")) = False Then
    '        gstrServerPath = gloRegistrySetting.GetRegistryValue("ServerPath")
    '    End If

    '    'Retrieve Appointment Level
    '    'If IsNothing(regKey.GetValue("AppointmentModuleLevel")) = False Then
    '    '    gnAppointmentModuleLevel = Val(regKey.GetValue("AppointmentModuleLevel"))
    '    If IsNothing(gloRegistrySetting.GetRegistryValue("AppointmentModuleLevel")) = False Then
    '        gnAppointmentModuleLevel = Val(gloRegistrySetting.GetRegistryValue("AppointmentModuleLevel"))
    '    Else
    '        gnAppointmentModuleLevel = 0
    '    End If

    '    'regKey.Close()
    '    gloRegistrySetting.CloseRegistryKey()

    '    If gstrSQLServerName = "" Or gstrDatabaseName = "" Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function
End Module
