Imports gloSettings
Public Class clsUnitTesting

    Public Shared Sub SetupTestingProject()

        'Dim regKey As Microsoft.Win32.RegistryKey

        'regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)


        'If IsNothing(regKey.GetValue("SQLServer")) = True Then
        '    regKey.Close()
        'End If
        If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrSqlServ)) = True Then '"SQLServer"
            gloRegistrySetting.CloseRegistryKey()
        End If
        'If IsNothing(regKey.GetValue("Database")) = True Then
        '    regKey.Close()
        'End If
        If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDB)) = True Then '"Database"
            gloRegistrySetting.CloseRegistryKey()
        End If
        'gstrSQLServerName = regKey.GetValue("SQLServer")
        'gstrDatabaseName = regKey.GetValue("Database")
        gstrSQLServerName = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrSqlServ)
        gstrDatabaseName = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDB)


        'If regKey.GetValue("IsSQLAuthentication") IsNot Nothing Then
        '    gblnSQLAuthentication = regKey.GetValue("IsSQLAuthentication")
        'End If
        If gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrSqlAuthen) IsNot Nothing Then 'IsSQLAuthentication
            gblnSQLAuthentication = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrSqlAuthen)
        End If
        'If regKey.GetValue("SQLUserEMR") IsNot Nothing Then
        '    gstrSQLUserEMR = regKey.GetValue("SQLUserEMR")
        'End If
        If gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrSqlUsrEMR) IsNot Nothing Then '"SQLUserEMR"
            gstrSQLUserEMR = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrSqlUsrEMR)
        End If

        'If regKey.GetValue("SQLPasswordEMR") IsNot Nothing Then
        '    If regKey.GetValue("SQLPasswordEMR") <> "" Then
        '        Dim oEncryption As New clsencryption
        '        gstrSQLPasswordEMR = oEncryption.DecryptFromBase64String(regKey.GetValue("SQLPasswordEMR"), constEncryptDecryptKey)
        '        oEncryption = Nothing
        '    End If
        'End If
        If gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrSqlPwdEMR) IsNot Nothing Then 'SQLPasswordEMR
            If gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrSqlPwdEMR) <> "" Then
                Dim oEncryption As New clsencryption
                gstrSQLPasswordEMR = oEncryption.DecryptFromBase64String(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrSqlPwdEMR), constEncryptDecryptKey)
                oEncryption = Nothing
            End If
        End If

        'If IsNothing(regKey.GetValue("ServerPath")) = False Then
        '    gstrServerPath = regKey.GetValue("ServerPath")
        'End If

        'regKey.Close()
        If IsNothing(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrServpth)) = False Then
            gstrServerPath = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrServpth) 'ServerPath
        End If

        gloRegistrySetting.CloseRegistryKey()


    End Sub

End Class
