'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.Management
Imports gloSettings

Module mdlStartup
    Sub Main()
        Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName

        Dim aProcName As String = System.IO.Path.GetFileNameWithoutExtension(aModuleName)

        If Process.GetProcessesByName(aProcName).Length > 1 Then
            MessageBox.Show("Another instance of this application is already running.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Exit()
            End
            Exit Sub
        End If

        If IsSettings() = False Then
            Dim frmSettings As New frmStartupSettings
            If frmSettings.ShowDialog() = DialogResult.OK Then
                Dim frmgloEMRLogin As New frmLogin
                frmgloEMRLogin.ShowDialog()
            End If
        End If
        Dim objSettings As New clsStartUpSettings
        If objSettings.IsConnect(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR) = False Then
            If MessageBox.Show("Unable to connect to SQL Server " & gstrSQLServerName & " and Database " & gstrDatabaseName & vbCrLf & "Do you want to change SQL Server or Database Settings?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                End
            End If
            Dim frmSettings As New frmStartupSettings
            If frmSettings.ShowDialog() = DialogResult.OK Then
                Dim frmgloEMRLogin1 As New frmLogin
                frmgloEMRLogin1.ShowDialog()
            End If
            Exit Sub
        End If
        Dim frmgloEMRLogin2 As New frmLogin
        frmgloEMRLogin2.ShowDialog()
    End Sub

    Private Function IsSettings() As Boolean
        'Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
        If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
            Return False
        End If
        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
        If IsNothing(gloRegistrySetting.GetRegistryValue("SQLServer")) = True Then
            gloRegistrySetting.CloseRegistryKey()
            Return False
        End If
        If IsNothing(gloRegistrySetting.GetRegistryValue("Database")) = True Then
            gloRegistrySetting.CloseRegistryKey()
            Return False
        End If
        If IsNothing(gloRegistrySetting.GetRegistryValue("ArchiveDatabase")) = True Then
            gloRegistrySetting.CloseRegistryKey()
            Return False
        End If
        gstrSQLServerName = gloRegistrySetting.GetRegistryValue("SQLServer")
        gstrDatabaseName = gloRegistrySetting.GetRegistryValue("Database")
        gstrArchiveDatabaseName = gloRegistrySetting.GetRegistryValue("ArchiveDatabase")
        gstrDomainName = gloRegistrySetting.GetRegistryValue("Domain")
        gstrWindowsServerName = gloRegistrySetting.GetRegistryValue("WindowsServer")
        gloRegistrySetting.CloseRegistryKey()

        If Trim(gstrSQLServerName) = "" Or Trim(gstrDatabaseName) = "" Or Trim(gstrDomainName) = "" Or Trim(gstrWindowsServerName) = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    'Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
    'Following Function added to check OS.
    Public Function GetOSInfo() As Boolean
        Try
            Dim objMOS As New ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem")
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
        Catch ex As Exception
            UpdateLog(ex.ToString)
            Return False
        End Try

    End Function
End Module
