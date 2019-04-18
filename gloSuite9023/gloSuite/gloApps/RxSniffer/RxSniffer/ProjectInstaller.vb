Imports System.ComponentModel
Imports System.Configuration.Install
Imports Microsoft.Win32
Imports RxSniffer.RxGeneral
Imports System.Data.SqlClient
Imports gloInstallerCommandParametrs

Public Class ProjectInstaller

    Public Sub New()
        MyBase.New()
        gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ServiceName = "RxSniffer"
        gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DisplayName = "RxSniffer"
        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent

    End Sub

#Region "Overridden Event Handlers"
    Protected Overrides Sub OnBeforeInstall(savedState As System.Collections.IDictionary)
        Try


            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.CreateSubKey(gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicRegistryName, False, gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ServiceName)
        Catch generatedExceptionName As Exception
            ' ex)
        End Try
        Try

            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.InitializeContextParameters(Me.Context, savedState)
        Catch generatedExceptionName As Exception
            ' ex)
        End Try
        Dim lastServiceName As String = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.GetLastServicesName(gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ServiceNameParameter)
        Try
            If (lastServiceName.ToUpper() = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ServiceNameParameter.ToUpper()) Then
                gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.StopService(gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ServiceNameParameter)
                Uninstall(Nothing)
            End If
        Catch generatedExceptionName As Exception
            ' ex)
        End Try
        Try
            If (Me.serviceInstaller1 IsNot Nothing) Then
                Me.serviceInstaller1.DisplayName = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DisplayNameParameter
                Me.serviceInstaller1.ServiceName = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ServiceNameParameter
            End If
        Catch generatedExceptionName As Exception
            ' ex)
        End Try

        MyBase.OnBeforeInstall(savedState)
    End Sub
    Protected Overrides Sub OnAfterInstall(savedState As System.Collections.IDictionary)
        MyBase.OnAfterInstall(savedState)
        Try
            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.AddContextParameters(gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ServiceNameParameter)
        Catch generatedExceptionName As Exception
            ' ex)
        End Try
    End Sub

    Protected Overrides Sub OnBeforeUninstall(savedState As System.Collections.IDictionary)
        Try

            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.FinalizeContextParameters(savedState)
        Catch generatedExceptionName As Exception
            ' ex)
        End Try
        Try
            If (Me.serviceInstaller1 IsNot Nothing) Then
                Me.serviceInstaller1.DisplayName = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DisplayNameParameter
                Me.serviceInstaller1.ServiceName = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ServiceNameParameter
            End If
        Catch generatedExceptionName As Exception
            'ex)
        End Try
        Try
            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.CurrentExecutingPath = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.GetExecutingPath(gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ServiceNameParameter)
        Catch
        End Try
        MyBase.OnBeforeUninstall(savedState)
    End Sub

    Protected Overrides Sub OnAfterUninstall(savedState As System.Collections.IDictionary)
        Try

            gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.RemoveContextParameters(savedState)
        Catch generatedExceptionName As Exception
            ' ex)
        End Try
        MyBase.OnAfterUninstall(savedState)
        Try
            If gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.CurrentExecutingPath Is Nothing Then
                gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.CurrentExecutingPath = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.GetExecutingPath(gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.ServiceNameParameter)
            End If
        Catch
        End Try

        Try
            Dim thisPath As String = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.CurrentExecutingPath
            If Not String.IsNullOrEmpty(thisPath) Then
                If Not thisPath.StartsWith("NotFound") Then
                    Try
                        gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.KillRunningProcess(thisPath)
                    Catch
                    End Try
                    Dim thisDir As System.IO.DirectoryInfo = Nothing
                    Try
                        thisDir = New System.IO.DirectoryInfo(System.IO.Path.GetDirectoryName(thisPath))
                        thisDir.Delete(True)
                    Catch
                    End Try
                End If

            End If
        Catch
        End Try
    End Sub

#End Region



    Private Sub ProjectInstaller_Committed(ByVal sender As Object, ByVal e As System.Configuration.Install.InstallEventArgs) Handles Me.Committed
        'Dim oSettings As New frmSettings
        'oSettings.ShowDialog()
        'Dim dbID As Integer = 0
        'Dim orgPassword As String = String.Empty
        'Dim regKey As RegistryKey
        'Dim objEncryption As clsEncryption
        'If (Registry.LocalMachine.OpenSubKey("Software\\gloEMR", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl) Is Nothing) Then
        '    Return
        'End If
        'regKey = Registry.LocalMachine.OpenSubKey("Software\\gloEMR", True)
        'Dim objClsDbCredentials As ClsDbCredentials = New ClsDbCredentials
        'objClsDbCredentials.DatabaseID = 0
        'If ((regKey.GetValue("SQLServer") Is Nothing) = False) Then
        '    objClsDbCredentials.ServerName = CType(regKey.GetValue("SQLServer"), String)
        'End If
        'If ((regKey.GetValue("Database") Is Nothing) = False) Then
        '    objClsDbCredentials.DatabaseName = CType(regKey.GetValue("Database"), String)
        'End If
        'If ((regKey.GetValue("SQLUser") Is Nothing) = False) Then
        '    objClsDbCredentials.SqlUSer = CType(regKey.GetValue("SQLUser"), String)
        'End If
        'If ((regKey.GetValue("SQLPassword") Is Nothing) = False) Then
        '    Dim strPassword As String
        '    strPassword = CType(regKey.GetValue("SQLPassword"), String)
        '    objEncryption = New clsEncryption
        '    orgPassword = objEncryption.DecryptFromBase64String(strPassword, mdlGeneral.constEncryptDecryptKeyRegistry)
        '    objClsDbCredentials.SqlPassword = objEncryption.EncryptToBase64String(orgPassword, mdlGeneral.constEncryptDecryptKeyDB)
        'End If

        'Dim strConnection = "SERVER=" + objClsDbCredentials.ServerName + ";DATABASE=" + objClsDbCredentials.DatabaseName + ";USER id=" + objClsDbCredentials.SqlUSer + ";Password=" + orgPassword
        'Dim objstrConnection As New SqlConnection(strConnection)
        'mdlGeneral.DatabaseID = dbID
        'Try
        '    objstrConnection.Open()
        '    dbID = objClsDbCredentials.saveDataBaseCredentials(objClsDbCredentials)
        '    If dbID > 0 Then
        '        Dim objfrmDBCredentials As frmDBCredentials = New frmDBCredentials
        '        objfrmDBCredentials.ShowDialog()
        '    End If
        'Catch ex As Exception
        'Finally
        '    If objstrConnection.State = ConnectionState.Open Then
        '        objstrConnection.Close()
        '    End If
        'End Try

        'RxWinService.ApplicationStartup()
    End Sub
End Class
