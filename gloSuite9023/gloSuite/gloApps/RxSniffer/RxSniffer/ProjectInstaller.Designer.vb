<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
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

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ServiceProcessInstaller1 = New System.ServiceProcess.ServiceProcessInstaller
        Me.ServiceInstaller1 = New System.ServiceProcess.ServiceInstaller
        '
        'ServiceProcessInstaller1
        '
        Me.ServiceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.ServiceProcessInstaller1.Password = Nothing
        Me.ServiceProcessInstaller1.Username = Nothing
        '
        'ServiceInstaller1
        '
        Me.ServiceInstaller1.Description = "Downloads Rx Messages from eRx Webservice"
        Me.ServiceInstaller1.DisplayName = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicDisplayName
        Me.ServiceInstaller1.ServiceName = gloInstallerCommandParametrs.gloInstaller.gloInstallerCommandParameters.DynamicServiceName
        Me.ServiceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.ServiceProcessInstaller1, Me.ServiceInstaller1})

    End Sub
    Friend WithEvents ServiceProcessInstaller1 As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents ServiceInstaller1 As System.ServiceProcess.ServiceInstaller

    'Private Sub DoBeforeInstall(ByVal Sender As Object, ByVal Args As System.Configuration.Install.InstallEventArgs) Handles MyBase.BeforeInstall
    '    ' Context when Install
    '    If GetContextParameter("SERVICENAME") = "" Then
    '        Throw New Exception("SERVICENAME undefined")
    '    End If
    '    If Not Me.ServiceInstaller1 Is Nothing Then
    '        Me.ServiceInstaller1.DisplayName = GetContextParameter("SERVICENAME")
    '        Me.ServiceInstaller1.ServiceName = GetContextParameter("SERVICENAME")
    '    End If
    'End Sub

    '' Developer should set the same name in CustomActionData in "Uninstall" Custom Install Action
    'Private Sub DoBeforeUninstall(ByVal Sender As Object, ByVal Args As System.Configuration.Install.InstallEventArgs) Handles MyBase.BeforeUninstall
    '    ' Context when Uninstall
    '    If GetContextParameter("SERVICENAME") = "" Then
    '        Throw New Exception("SERVICENAME undefined")
    '    End If
    '    Me.ServiceInstaller1.DisplayName = GetContextParameter("SERVICENAME")
    '    Me.ServiceInstaller1.ServiceName = GetContextParameter("SERVICENAME")
    'End Sub

    Public Function GetContextParameter(ByVal key As String) As String
        Dim sValue As String = ""
        If Me.Context Is Nothing Then
            'WriteLog("Me.ServiceInstaller1 Is Nothing")
        End If
        Try
            sValue = Me.Context.Parameters(key).ToString()
            Return sValue
        Catch
            sValue = ""
            Return sValue
        End Try
    End Function

End Class
