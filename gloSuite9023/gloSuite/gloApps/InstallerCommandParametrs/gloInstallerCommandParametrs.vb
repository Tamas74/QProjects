Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.ServiceProcess
Imports Microsoft.Win32
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports System.Threading
Imports WindowsInstaller
Imports System.Security.Principal
Imports System.Security
Imports System.Security.Permissions
Imports Microsoft.Win32.SafeHandles
Imports System.Runtime.ConstrainedExecution

Namespace gloInstaller
    Public Class gloInstallerCommandParameters
        ''' <summary>
        ''' This inner class maintains the key names for the parameter values that may be passed on the 
        ''' command line.
        ''' </summary>
        Public Shared _IsSDKExists As Boolean = False
        Private Class Keys
            Public Const SERVICENAME As String = "SERVICENAME"
            Public Const DISPLAYNAME As String = "DISPLAYNAME"
            Public Const HL7DisplayName As String = ""
        End Class

        Private Shared _ServiceNameParameter As String = Nothing
        Public Shared ReadOnly Property ServiceNameParameter As String
            Get
                Return _ServiceNameParameter
            End Get
        End Property
        Private Shared _CurrentExecutingPath As String = Nothing
        Public Shared Property CurrentExecutingPath As String
            Get
                Return _CurrentExecutingPath
            End Get
            Set(value As String)
                _CurrentExecutingPath = value
            End Set
        End Property

        Private Shared _DisplayNameParameter As String = Nothing
        Public Shared ReadOnly Property DisplayNameParameter As String
            Get
                Return _DisplayNameParameter
            End Get
        End Property

        ''' <summary>
        ''' This constructor is invoked by Install class methods that have an Install Context built from 
        ''' parameters specified in the command line. Rollback, Install, Commit, and intermediate methods like
        ''' OnAfterInstall will all be able to use this constructor.
        ''' </summary>
        ''' <param name="installContext">The install context containing the command line parameters to set the strong types variables to.</param>
        Public Shared Sub InitializeContextParameters(installContext As InstallContext, stateSaver As System.Collections.IDictionary)
            If (installContext.Parameters.ContainsKey(Keys.SERVICENAME)) Then
                _ServiceNameParameter = installContext.Parameters(Keys.SERVICENAME)
            End If
            If (installContext.Parameters.ContainsKey(Keys.DISPLAYNAME)) Then
                _DisplayNameParameter = installContext.Parameters(Keys.DISPLAYNAME)
            End If

            If (IsNothing(_ServiceNameParameter)) Then
                _ServiceNameParameter = DynamicServiceName
            End If
            If (IsNothing(_DisplayNameParameter)) Then
                _DisplayNameParameter = DynamicDisplayName
            End If

            If (IsNothing(_HL7ServiceName)) Then
                _HL7ServiceName = DynamicHL7RegistryName
            End If
            ' Add/update the "ServiceName" entry in the state saver so that it may be accessed on uninstall.
            If stateSaver.Contains(Keys.SERVICENAME) = True Then
                stateSaver(Keys.SERVICENAME) = _ServiceNameParameter
            Else
                stateSaver.Add(Keys.SERVICENAME, _ServiceNameParameter)
            End If

            ' Add/update the "DisplayName" entry in the state saver so that it may be accessed on uninstall.
            If stateSaver.Contains(Keys.DISPLAYNAME) = True Then
                stateSaver(Keys.DISPLAYNAME) = _DisplayNameParameter
            Else
                stateSaver.Add(Keys.DISPLAYNAME, _DisplayNameParameter)
            End If
        End Sub

        ''' <summary>
        ''' This constructor is used by the Install class methods that don't have an Install Context built
        ''' from the command line. This method is primarily used by the Uninstall method.
        ''' </summary>
        ''' <param name="savedState">An IDictionary object that contains the parameters that were saved from a prior installation.</param>

        Public Shared Sub FinalizeContextParameters(savedState As IDictionary)
            If (IsNothing(savedState) = False) Then
                If savedState.Contains(Keys.SERVICENAME) = True Then
                    _ServiceNameParameter = DirectCast(savedState(Keys.SERVICENAME), String)
                End If
                If savedState.Contains(Keys.DISPLAYNAME) = True Then
                    _DisplayNameParameter = DirectCast(savedState(Keys.DISPLAYNAME), String)
                End If
            End If
            If (IsNothing(_ServiceNameParameter)) Then
                _ServiceNameParameter = DynamicServiceName
            End If
            If (IsNothing(_DisplayNameParameter)) Then
                _DisplayNameParameter = DynamicDisplayName
            End If
        End Sub

        ''' <summary>
        ''' This constructor is used by the Install class methods that don't have an Install Context built
        ''' from the command line. This method is primarily used by the Uninstall method.
        ''' </summary>
        ''' <param name="savedState">An IDictionary object that contains the parameters that were saved from a prior installation.</param>

        Public Shared Sub RemoveContextParameters(savedState As IDictionary)
            FinalizeContextParameters(savedState)
        End Sub
        Private Shared _strServiceName As String = ""
        Private Shared _strAssemblyName As String = ""
        Private Shared _isAssemblyNameRead As Boolean = False
        Public Shared Property HL7ServiceName As String
            Get
                Return _HL7ServiceName
            End Get
            Set(value As String)
                _HL7ServiceName = value
            End Set
        End Property
        Public Shared Property ServiceName As String
            Get
                Return _strServiceName
            End Get
            Set(value As String)
                _strServiceName = value
            End Set
        End Property
        Public Shared ReadOnly Property DynamicServiceName As String
            Get
                If _isAssemblyNameRead Then
                    Return _strServiceName & "_" & _strAssemblyName
                Else
                    _strAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                    _isAssemblyNameRead = True
                    Return _strServiceName & "_" & _strAssemblyName
                End If
            End Get
        End Property

        Private Shared _strDisplayName As String = ""

        Public Shared Property DisplayName As String
            Get
                Return _strDisplayName
            End Get
            Set(value As String)
                _strDisplayName = value
            End Set
        End Property
        Public Shared ReadOnly Property DynamicDisplayName As String
            Get
                If _isAssemblyNameRead Then
                    Return _strDisplayName & "_" & _strAssemblyName
                Else
                    _strAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                    _isAssemblyNameRead = True
                    Return _strDisplayName & "_" & _strAssemblyName
                End If
            End Get
        End Property

        Private Shared _strRegistryName As String = "gloServices"

        Private Shared _strHL7RegistryName As String = "gloHL7"
        Public Shared ReadOnly Property DynamicRegistryName As String
            Get
                If _isAssemblyNameRead Then
                    Return _strRegistryName & "_" & _strAssemblyName
                Else
                    _strAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                    _isAssemblyNameRead = True
                    Return _strRegistryName & "_" & _strAssemblyName
                End If
            End Get
        End Property
        Private Shared _HL7ServiceName As String = "gloHL7"
        Public Shared ReadOnly Property DynamicHL7RegistryName() As String
            Get
                If _isAssemblyNameRead Then
                    Return _HL7ServiceName + "_" + _strAssemblyName
                Else
                    _strAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                    _isAssemblyNameRead = True
                    Return _HL7ServiceName + "_" + _strAssemblyName
                End If
            End Get
        End Property
        Public Shared Function Digitiser(StrVersion As String) As String
            Dim StrArray() As String = Split(StrVersion, ".")
            Dim RetString As String = ""
            For i As Integer = 0 To StrArray.Length - 1
                RetString = RetString & StrArray(i)
            Next
            Return RetString
        End Function
        Public Shared ReadOnly Property DynamicAssemblyNumber As String
            Get
                If _isAssemblyNameRead Then
                    Return Digitiser(_strAssemblyName)
                Else
                    _strAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                    _isAssemblyNameRead = True
                    Return Digitiser(_strAssemblyName)
                End If
            End Get
        End Property
        Public Shared Function GetLastServicesName(ByVal LookForServiceName As String) As String
            Dim ServiceNameStrings As ArrayList = GetAllServicesNames(LookForServiceName)
            Try
                If (ServiceNameStrings.Count > 0) Then
                    Return ServiceNameStrings(ServiceNameStrings.Count - 1)
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            Finally
                Try
                    ServiceNameStrings.Clear()
                    ServiceNameStrings = Nothing
                Catch ex As Exception

                End Try
            End Try
        End Function
        Public Shared Function GetAllServicesNames(ByVal LookForServiceName As String, Optional ByVal MachineName As String = Nothing) As ArrayList
            Dim ServiceNameStrings As ArrayList = New ArrayList()
            Try
                Dim UpperServiceName As String = ""
                If (String.IsNullOrEmpty(LookForServiceName) = False) Then
                    UpperServiceName = LookForServiceName.ToUpper()
                End If
                Dim allServices As ServiceController()
                If (String.IsNullOrEmpty(MachineName)) Then
                    allServices = ServiceController.GetServices()
                Else
                    allServices = ServiceController.GetServices(MachineName)
                End If
                For Each aService As ServiceController In allServices
                    Dim myServiceName As String = aService.ServiceName
                    If (UpperServiceName = "") OrElse (myServiceName.ToUpper().StartsWith(UpperServiceName)) Then
                        Dim myIndex As Integer = -1
                        For thisName As Integer = 0 To ServiceNameStrings.Count - 1
                            If (ServiceNameStrings(thisName) >= myServiceName) Then
                                myIndex = thisName
                                Exit For
                            End If
                        Next
                        If (myIndex = -1) Then
                            ServiceNameStrings.Insert(ServiceNameStrings.Count, myServiceName)
                        Else
                            ServiceNameStrings.Insert(myIndex, myServiceName)
                        End If
                    End If

                Next
                If (IsNothing(allServices) = False) Then
                    allServices = Nothing
                End If

                Return ServiceNameStrings

            Catch ex As Exception
                Return ServiceNameStrings
            Finally

            End Try
        End Function
        'Public Shared Function StopService(StrServiceName As String) As String
        '    Dim LastServiceName As String = Nothing
        '    Try
        '        LastServiceName = GetLastServicesName(StrServiceName)
        '    Catch ex As Exception

        '    End Try
        '    If (String.IsNullOrEmpty(LastServiceName)) Then
        '        Return "NotFound: " + StrServiceName
        '    Else
        '        Dim thisController As New ServiceController(StrServiceName)
        '        Try
        '            If (thisController.Status = ServiceControllerStatus.Running) Then
        '                thisController.Stop()
        '                thisController.WaitForStatus(ServiceControllerStatus.Stopped, New TimeSpan(0, 0, 0, 15))
        '            End If


        '            Return "Success"
        '        Catch ex As Exception
        '            Return "Failure: " + ex.ToString()
        '        Finally
        '            Try
        '                thisController.Close()
        '                thisController.Dispose()
        '            Catch ex As Exception

        '            End Try

        '        End Try
        '    End If

        'End Function
        'Public Shared Function StartService(StrServiceName As String) As String
        '    Dim LastServiceName As String = Nothing
        '    Try
        '        LastServiceName = GetLastServicesName(StrServiceName)
        '    Catch ex As Exception

        '    End Try
        '    If (String.IsNullOrEmpty(LastServiceName)) Then
        '        Return "NotFound: " + StrServiceName
        '    Else
        '        Dim thisController As New ServiceController(StrServiceName)
        '        Try

        '            thisController.Start()
        '            Return "Success"
        '        Catch ex As Exception
        '            Return "Failure: " + ex.ToString()
        '        Finally
        '            Try

        '                thisController.Dispose()
        '            Catch ex As Exception

        '            End Try

        '        End Try
        '    End If

        'End Function


        'Public Shared Function RemoveServiceOnly(StrServiceName As String) As String
        '    'Dim ServiceInstallerObj As New ServiceInstaller()
        '    'Try
        '    '    ServiceInstallerObj.Context = New InstallContext(LogFilePath, Nothing)
        '    '    ServiceInstallerObj.ServiceName = StrServiceName
        '    '    ServiceInstallerObj.Uninstall(Nothing)
        '    '    Return "Success "
        '    'Catch ex As Exception
        '    '    Return "Failure: " & ex.ToString()
        '    'Finally
        '    '    Try
        '    '        ServiceInstallerObj.Dispose()
        '    '        ServiceInstallerObj = Nothing
        '    '    Catch ex As Exception

        '    '    End Try

        '    'End Try
        '    Try
        '        Uninstall(StrServiceName)
        '        Return "Success"
        '    Catch ex As Exception
        '        Return "Failure: " & ex.ToString()
        '    End Try


        'End Function

        Public Shared Function StopAndRemoveServiceInTrayIcon(StrServiceName As String, ExecutingPath As String, RemoveProductCode As String, Optional strDomainName As String = "", Optional strDomainUser As String = "", Optional strDomainPassword As String = "") As String

            If (String.IsNullOrEmpty(ExecutingPath)) Then
                ExecutingPath = GetExecutingPath(StrServiceName)
            End If
            If (String.IsNullOrEmpty(ExecutingPath)) Then
                ExecutingPath = ""
            End If
            If (String.IsNullOrEmpty(RemoveProductCode)) Then
                RemoveProductCode = ""
            End If
            Dim RetCode As String = ""


            Try

                Try
                    StopService(StrServiceName)
                    RetCode = "Success while Stopping service"
                Catch ex As Exception
                    RetCode = "Failure while Stopping service: " & ex.ToString()
                Finally

                End Try
            Catch ex As Exception

            End Try

            Dim UnInstallerRetCode As String = ""
            Dim myProductName As String = "Error in Path"
            Try
                Dim MyProductCode As String = ""
                Dim Myinstaller As WindowsInstaller.Installer
                Dim Installertype As Type = Type.GetTypeFromProgID("WindowsInstaller.Installer")
                Myinstaller = DirectCast(Activator.CreateInstance(Installertype), WindowsInstaller.Installer)
                Dim myLocation As String = ""
                If (String.IsNullOrEmpty(ExecutingPath)) Then
                    myProductName = RemoveProductCode
                ElseIf (ExecutingPath.StartsWith("NotFound")) Then
                    myProductName = RemoveProductCode
                ElseIf (Not File.Exists(ExecutingPath)) Then
                    myProductName = RemoveProductCode
                Else
                    myLocation = Path.GetDirectoryName(ExecutingPath).ToUpper()
                    If (String.IsNullOrEmpty(RemoveProductCode)) Then
                        Dim myFileInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(ExecutingPath)
                        myProductName = myFileInfo.ProductName.ToUpper()
                    Else
                        myProductName = RemoveProductCode.ToUpper()
                    End If
                End If

                For Each productCode As String In Myinstaller.Products
                    Dim propertyName As String = "ProductName"
                    Try
                        Dim propertyValue As String = Myinstaller.ProductInfo(productCode, propertyName)
                        If (propertyValue.ToUpper() = myProductName) Then
                            MyProductCode = productCode
                            Exit For
                        End If
                    Catch ex2 As Exception
                        UnInstallerRetCode = UnInstallerRetCode & " for " & productCode & " Propery Value could not be found for " & propertyName & " with exception: " & ex2.ToString()
                    End Try
                    If (Not String.IsNullOrEmpty(myLocation)) Then
                        Dim LocationPropertyName As String = "InstallLocation"
                        Try
                            Dim propertyValue As String = Myinstaller.ProductInfo(productCode, LocationPropertyName)
                            If (Not String.IsNullOrEmpty(propertyValue)) Then
                                Try
                                    If (Path.GetDirectoryName(propertyValue).ToUpper() = myLocation) Then
                                        MyProductCode = productCode
                                        Exit For
                                    End If
                                Catch ex3 As Exception
                                    UnInstallerRetCode = UnInstallerRetCode & " for " & productCode & " Installation Path could not be found for " & propertyValue & " with exception: " & ex3.ToString()

                                End Try


                            End If
                        Catch ex2 As Exception
                            UnInstallerRetCode = UnInstallerRetCode & " for " & productCode & " Propery Value could not be found for " & LocationPropertyName & " with exception: " & ex2.ToString()
                        End Try
                    End If
                Next
                If (String.IsNullOrEmpty(MyProductCode)) Then
                    UnInstallerRetCode = UnInstallerRetCode & "Failed: Unable to uninstall due to no Product Code for " & StrServiceName & " with name " & myProductName & " for " & ExecutingPath
                Else
                    If (Not ExecutingPath.StartsWith("NotFound")) Then
                        RetCode = RetCode & KillRunningProcess(ExecutingPath)
                    End If
                    Try

                        Myinstaller.UILevel = MsiUILevel.msiUILevelNone
                        Myinstaller.ConfigureProduct(MyProductCode, 0, MsiInstallState.msiInstallStateAbsent)
                        UnInstallerRetCode = "Successfully unistalled " & StrServiceName & " with name " & myProductName & " and code " & MyProductCode & " for " & ExecutingPath & " " & UnInstallerRetCode
                    Catch ex As Exception
                        UnInstallerRetCode = "Failed: Unable to uninstall  " & ex.ToString() & " " & UnInstallerRetCode
                    End Try
                End If

            Catch ex As Exception
                UnInstallerRetCode = "Failed: Unable to load msi.dll " & ex.ToString() & UnInstallerRetCode
            End Try
            If (Not UnInstallerRetCode.StartsWith("Success")) Then
                If (myProductName <> "Error in Path") Then
                    'UnInstallerRetCode = UninstallfrmControlpanel(myProductName, strDomainName, strDomainUser, strDomainPassword) & UnInstallerRetCode
                    Dim UnRetCode As String = UninstallfrmControlpanel(myProductName, strDomainName, strDomainUser, strDomainPassword)
                    If (UnRetCode.StartsWith("Success")) Then
                        UnInstallerRetCode = UnRetCode
                    Else
                        UnInstallerRetCode = UnRetCode & " " & UnInstallerRetCode
                    End If
                End If
            End If
            If (Not ExecutingPath.StartsWith("NotFound")) Then
                RetCode = RetCode & KillRunningProcess(ExecutingPath)
            End If
            If (UnInstallerRetCode.StartsWith("Success")) Then
                Return UnInstallerRetCode
            Else
                Return UnInstallerRetCode & " " & RetCode
            End If

        End Function


        Public Shared ProductAttributes() As String = {"Language", "ProductName", "PackageCode", "Transforms", "AssignmentType", "PackageName", "InstalledProductName", "VersionString", "RegCompany", "RegOwner", "ProductID", "ProductIcon", "InstallLocation", "InstallSource", "InstallDate", "Publisher", "LocalPackage", "HelpLink", "HelpTelephone", "URLInfoAbout", "URLUpdateInfo"}
        Public Shared RequiredProductAttributes() As String = {"ProductName", "InstallLocation"}
        Public Shared Function KillRunningProcess(MyExecutingPath As String) As String
            Dim RetCode As String = ""
            If (Not String.IsNullOrEmpty(MyExecutingPath)) Then

                If (File.Exists(MyExecutingPath)) Then
                    Dim ExecutingPath As String = MyExecutingPath.ToUpper()
                    Dim ProcessFriendlyName As String = Path.GetFileNameWithoutExtension(MyExecutingPath)
                    Dim CurrentRunningProcesses() As Process = Nothing
                    CurrentRunningProcesses = Process.GetProcessesByName(ProcessFriendlyName)
                    If (IsNothing(CurrentRunningProcesses) = False) Then
                        If (CurrentRunningProcesses.Length < 1) Then
                            CurrentRunningProcesses = Process.GetProcesses()
                        End If
                    Else
                        CurrentRunningProcesses = Process.GetProcesses()
                    End If
                    For iProcess As Integer = CurrentRunningProcesses.Length - 1 To 0 Step -1
                        Dim CurrentRunningProcess As Process = CurrentRunningProcesses(iProcess)
                        Try
                            If (IsNothing(CurrentRunningProcess.MainModule) = False) Then
                                If (IsNothing(CurrentRunningProcess.MainModule.FileName) = False) Then
                                    If (ExecutingPath.StartsWith(CurrentRunningProcess.MainModule.FileName.ToUpper())) Then
                                        Try
                                            CurrentRunningProcess.Kill()
                                        Catch Ex As Exception
                                            RetCode = RetCode & " Exception while killing: " & Ex.ToString()
                                        End Try
                                    End If
                                End If
                            End If
                        Catch ex As Exception

                        End Try

                    Next
                Else
                    RetCode = RetCode & " And File Not Exists: " & MyExecutingPath
                End If
            Else
                RetCode = RetCode & " File is Empty "
            End If
            Return RetCode
        End Function
        Public Shared Function UninstallfrmControlpanel(strApplicationName As String, Optional strDomainName As String = "", Optional strDomainUser As String = "", Optional strDomainPassword As String = "") As String
            Try
                Dim uninstall As String = String.Empty
                Dim strReg As String = String.Empty

                If CheckMachineArchitecture() <> 64 Then
                    strReg = "Software\Microsoft\Windows\CurrentVersion\Uninstall"
                    uninstall = GetUnistallerString(strApplicationName, strReg)
                Else
                    strReg = "Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
                    uninstall = GetUnistallerString(strApplicationName, strReg)
                    If String.IsNullOrEmpty(uninstall) Then
                        strReg = "Software\Microsoft\Windows\CurrentVersion\Uninstall"
                        uninstall = GetUnistallerString(strApplicationName, strReg)
                    End If
                End If
                If Not String.IsNullOrEmpty(uninstall) Then
                    uninstall = uninstall.Replace("/I", "/X").Remove(0, 11) & " " & "/qn"
                    Return RunExe("", uninstall, strDomainName, strDomainUser, strDomainPassword)
                Else

                    Return "Success: No unistallation required for " & strApplicationName

                End If
            Catch ex As Exception
                Return "Exception: " & ex.ToString() & strApplicationName
            Finally
            End Try
        End Function
        Public Shared Function CheckMachineArchitecture() As Integer
            Dim strProcArchi As String = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")
            Dim Proc64running32 As Boolean = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))
            If (strProcArchi.IndexOf("64") > 0) OrElse (Not Proc64running32) Then
                Return 64
            Else
                Return 32
            End If
        End Function
        Public Shared Function GetUnistallerString(strApplicationName As String, strReg As String, Optional ByVal strApplicationVersion As String = "", Optional ByVal WhatToReturn As String = "UninstallString") As String
            Dim regInstallPath As RegistryKey = Nothing
            Dim strRetValue As String = String.Empty
            Try
                regInstallPath = Registry.LocalMachine.OpenSubKey(strReg, False)
                Dim keyName As String = ""
                Dim DisplayName As String = ""
                Dim InstallPath As String = ""
                Dim UninstallString As String = ""
                Dim DisplayVersion As String = ""
                Dim upperApplication As String = strApplicationName.ToUpper()
                Dim upperVersion As String = strApplicationVersion.ToUpper()
                For Each uninstallKey As String In regInstallPath.GetSubKeyNames()
                    GetApplicationKeys(regInstallPath, uninstallKey, DisplayName, InstallPath, UninstallString, DisplayVersion)
                    If (upperApplication = DisplayName.ToUpper()) Then
                        If (String.IsNullOrEmpty(upperVersion)) Then

                            If (WhatToReturn = "UninstallString") Then
                                Return UninstallString
                            Else
                                Return InstallPath
                            End If
                        Else
                            If (upperVersion = DisplayVersion.ToUpper()) Then
                                If (WhatToReturn = "UninstallString") Then
                                    Return UninstallString
                                Else
                                    Return InstallPath
                                End If
                            End If
                        End If
                    End If
                Next
                Return Nothing
            Catch ex As Exception
                Return " Error: " & ex.ToString()
            Finally
                If (IsNothing(regInstallPath) = False) Then
                    regInstallPath.Close()
                    regInstallPath.Dispose()
                    regInstallPath = Nothing
                End If
            End Try
        End Function
        Public Shared Sub GetApplicationKeys(uninstallKey As RegistryKey, keyName As String, ByRef DisplayName As String, ByRef InstallPath As String, ByRef UninstallString As String, ByRef DisplayVersion As String)
            DisplayName = ""
            InstallPath = ""
            UninstallString = ""
            DisplayVersion = ""
            Try
                Dim key As RegistryKey = uninstallKey.OpenSubKey(keyName, False)
                Try
                    Dim strdisplayName As Object = key.GetValue("DisplayName")
                    If strdisplayName IsNot Nothing Then
                        DisplayName = strdisplayName.ToString()
                    End If

                    Dim strInstallLocation As Object = key.GetValue("InstallLocation")
                    If strInstallLocation IsNot Nothing Then
                        InstallPath = strInstallLocation.ToString()
                    End If

                    Dim strUnistallString As Object = key.GetValue("UninstallString")
                    If strUnistallString IsNot Nothing Then
                        UninstallString = strUnistallString.ToString()
                    End If

                    Dim strDisplayVersionString As Object = key.GetValue("DisplayVersion")
                    If strDisplayVersionString IsNot Nothing Then
                        DisplayVersion = strDisplayVersionString.ToString()
                    End If
                Finally
                    key.Close()
                    key.Dispose()
                    key = Nothing
                End Try
            Catch ex As Exception

            End Try
        End Sub

        Public Shared Function RunExe(strPath As String, strCommand As String, Optional strDomainName As String = "", Optional strDomainUser As String = "", Optional strDomainPassword As String = "") As String
            Dim Success As Boolean = False
            Dim strsetup As String = String.Empty
            Dim strWinpath As String = Nothing
            Dim oProcess As Process = Nothing
            Try
                oProcess = New Process()

                strWinpath = Environment.GetEnvironmentVariable("WINDIR").ToString()

                If strPath = "" Then
                    strsetup = strWinpath & "\System32\msiexec.exe"
                Else
                    strsetup = strPath
                End If

                If oProcess IsNot Nothing Then
                    oProcess.StartInfo.FileName = strsetup
                    oProcess.StartInfo.Arguments = strCommand

                    If Not String.IsNullOrEmpty(strDomainName) AndAlso Not String.IsNullOrEmpty(strDomainUser) AndAlso Not String.IsNullOrEmpty(strDomainPassword) Then
                        Dim user As WindowsIdentity = WindowsIdentity.GetCurrent()
                        Dim principal As WindowsPrincipal = New WindowsPrincipal(user)
                        If principal.IsInRole(&H1F4) OrElse principal.IsInRole(&H220) OrElse principal.IsInRole(&H200) Then
                        Else
                            oProcess.StartInfo.Domain = strDomainName
                            oProcess.StartInfo.UserName = strDomainUser
                            oProcess.StartInfo.Password = MakeSecureString(strDomainPassword)
                        End If
                        principal = Nothing
                    End If
                    oProcess.StartInfo.UseShellExecute = False
                    oProcess.StartInfo.CreateNoWindow = True
                    oProcess.Start()
                    Do
                        'oProcess.WaitForExit(1000);
                        oProcess.Refresh()
                        Thread.Sleep(100)
                    Loop While Not oProcess.HasExited
                    Thread.Sleep(100)
                    If oProcess.ExitCode = 0 OrElse oProcess.ExitCode = 3010 Then
                        Return "Success "
                    Else
                        Return "Msi not installed successfully for file :" & strsetup & " and cmd :" & strPath & " , Exit code :" & oProcess.ExitCode
                    End If
                Else
                    Return "Unable to create a Process :" & strsetup & " and cmd :" & strPath
                End If
            Catch ex As Exception
                Return "Msi not installed successfully for file :" & strsetup & " and cmd :" & strPath & " , Exit code :" & ex.ToString()
            Finally
                If oProcess IsNot Nothing Then
                    oProcess.Close()
                    oProcess.Dispose()
                End If
            End Try
        End Function
        Public Shared Function MakeSecureString(text As String) As SecureString
            Dim secure As SecureString = Nothing
            Try
                secure = New SecureString()

                For Each c As Char In text
                    secure.AppendChar(c)
                Next
            Catch ex As Exception
                secure = Nothing
            End Try
            Return secure
        End Function

        Public Shared Sub CreateSubKey(SubKey As String, flag As Boolean, BaseKey As String)
            UpdateLog("Entering CreateSubKey " & flag.ToString() & " , " & SubKey & " , " & BaseKey)
            Dim _sRegistryName As String = Nothing
            If Not String.IsNullOrEmpty(HL7ServiceName) Then
                _sRegistryName = _strHL7RegistryName
            Else
                _sRegistryName = _strRegistryName
            End If
            Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\" & SubKey, flag)
            If regKey Is Nothing Then
                Dim lastServiceName As String = GetLastServicesName(BaseKey)
                If (String.IsNullOrEmpty(lastServiceName)) Then
                    UpdateLog("lastServiceName: Empty")
                    lastServiceName = ""
                Else
                    UpdateLog("lastServiceName: " & lastServiceName)
                End If

                Dim lastVersion As String = ""
                Dim lregkey As RegistryKey = Nothing
                If (lastServiceName.Length > BaseKey.Length) Then
                    lastVersion = lastServiceName.Substring(BaseKey.Length + 1)
                    lregkey = Registry.LocalMachine.OpenSubKey("Software\" & _strRegistryName & "_" & lastVersion, False)
                    UpdateLog("lastVersion: " & lastVersion)
                Else
                    UpdateLog("lastVersion: Empty")
                    lregkey = Registry.LocalMachine.OpenSubKey("Software\" & _strRegistryName, False)

                End If



                If lregkey IsNot Nothing Then
                    UpdateLog("Last regKey: " & lregkey.ToString())
                    regKey = Registry.LocalMachine.CreateSubKey("Software\" & SubKey, RegistryKeyPermissionCheck.ReadWriteSubTree)
                    UpdateLog("regKey: " & regKey.ToString())
                    If regKey IsNot Nothing Then
                        Dim keyNames As String() = lregkey.GetValueNames()
                        For Each keyName As String In keyNames
                            regKey.SetValue(keyName, lregkey.GetValue(keyName))
                        Next
                    End If
                    regKey.Close()
                    regKey.Dispose()
                    lregkey.Close()
                    lregkey.Dispose()
                Else
                    UpdateLog("Last regKey: Empty")
                    regKey = Registry.LocalMachine.OpenSubKey("Software", True)
                    If regKey IsNot Nothing Then
                        lregkey = regKey.CreateSubKey(SubKey)
                        regKey.Close()
                        regKey.Dispose()
                        lregkey.Close()
                        lregkey.Dispose()
                    End If
                End If
            Else
                '' Problem : #1152 (Size of gloInstallerLog.txt file was increasing every second due to the log)
                'UpdateLog("Else CreateSubKey " & regKey.ToString())
                regKey.Close()
                regKey.Dispose()
            End If
        End Sub
        Public Shared Sub CopyServicesSubKeyIfNotExists(Optional ByVal CopyFromSource As String = Nothing)
            Dim SubKey As String = Nothing
            Dim _sRegistryName As String = Nothing
            If (String.IsNullOrEmpty(CopyFromSource)) Then
                SubKey = DynamicRegistryName
            Else
                SubKey = CopyFromSource
            End If
            If Not String.IsNullOrEmpty(HL7ServiceName) Then
                _sRegistryName = _strHL7RegistryName
            Else
                _sRegistryName = _strRegistryName
            End If
            If (String.IsNullOrEmpty(SubKey)) Then
                SubKey = _strRegistryName & "_New"
            End If
            Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\" & SubKey, False)
            If regKey Is Nothing Then
                Dim lregkey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\" & _strRegistryName, False)

                If lregkey IsNot Nothing Then
                    UpdateLog("Last regKey: " & lregkey.ToString())
                    regKey = Registry.LocalMachine.CreateSubKey("Software\" & SubKey, RegistryKeyPermissionCheck.ReadWriteSubTree)
                    UpdateLog("regKey: " & regKey.ToString())
                    If regKey IsNot Nothing Then
                        Dim keyNames As String() = lregkey.GetValueNames()
                        For Each keyName As String In keyNames
                            regKey.SetValue(keyName, lregkey.GetValue(keyName))
                        Next
                    End If
                    regKey.Close()
                    regKey.Dispose()
                    lregkey.Close()
                    lregkey.Dispose()
                Else
                    UpdateLog("Last regKey: Empty")
                    regKey = Registry.LocalMachine.OpenSubKey("Software", True)
                    If regKey IsNot Nothing Then
                        lregkey = regKey.CreateSubKey(SubKey)
                        regKey.Close()
                        regKey.Dispose()
                        lregkey.Close()
                        lregkey.Dispose()
                    End If

                End If
            Else
                '' Problem : #1152 (Size of gloInstallerLog.txt file was increasing every second due to the log)
                'UpdateLog("Else CreateSubKey " & regKey.ToString())
                regKey.Close()
                regKey.Dispose()
            End If
        End Sub

        Private Shared Function ReplaceSpaceWithDash(ByVal StrSourceString As String) As String
            If (String.IsNullOrEmpty(StrSourceString)) Then
                Return StrSourceString
            Else
                Return StrSourceString.Replace("_", "__").Replace(" ", "_")
            End If
        End Function
        Private Shared Function ReplaceDashWithSpace(ByVal StrSourceString As String) As String
            If (String.IsNullOrEmpty(StrSourceString)) Then
                Return StrSourceString
            Else
                Return StrSourceString.Replace("_", " ").Replace("  ", "_")
            End If
        End Function

        Public Shared Sub AddContextParameters(ByVal MyServiceName As String, Optional MachineName As String = Nothing)
            Dim hScm As IntPtr = OpenSCManager(IIf(String.IsNullOrEmpty(MachineName), Nothing, MachineName), Nothing, SC_MANAGER_ALL_ACCESS)
            If hScm = IntPtr.Zero Then
                Throw New Win32Exception()
            End If
            Try
                Dim hSvc As IntPtr = OpenService(hScm, MyServiceName, SERVICE_ALL_ACCESS)
                If hSvc = IntPtr.Zero Then
                    Throw New Win32Exception()
                End If
                Try
                    Dim oldConfig As QUERY_SERVICE_CONFIG
                    Dim bytesAllocated As UInteger = 8192
                    ' Per documentation, 8K is max size.
                    Dim ptr As IntPtr = Marshal.AllocHGlobal(CInt(bytesAllocated))
                    Try
                        Dim bytesNeeded As UInteger
                        If Not QueryServiceConfig(hSvc, ptr, bytesAllocated, bytesNeeded) Then
                            Throw New Win32Exception()
                        End If
                        oldConfig = CType(Marshal.PtrToStructure(ptr, GetType(QUERY_SERVICE_CONFIG)), QUERY_SERVICE_CONFIG)
                    Finally
                        Marshal.FreeHGlobal(ptr)
                    End Try

                    Dim newBinaryPathAndParameters As String = Convert.ToString(oldConfig.lpBinaryPathName & Convert.ToString(" /s:")) & ReplaceSpaceWithDash(MyServiceName)

                    If Not ChangeServiceConfig(hSvc, SERVICE_NO_CHANGE, SERVICE_NO_CHANGE, SERVICE_NO_CHANGE, newBinaryPathAndParameters, Nothing, _
                     IntPtr.Zero, Nothing, Nothing, Nothing, Nothing) Then
                        Throw New Win32Exception()
                    End If
                Finally
                    If Not CloseServiceHandle(hSvc) Then
                        Throw New Win32Exception()
                    End If
                End Try
            Finally
                If Not CloseServiceHandle(hScm) Then
                    Throw New Win32Exception()
                End If
            End Try
        End Sub
        Public Shared Function GetExecutingPath(ByVal MyServiceName As String) As String
            Dim LastServiceName As String = Nothing
            Try
                LastServiceName = GetLastServicesName(MyServiceName)
            Catch ex As Exception

            End Try
            If (String.IsNullOrEmpty(LastServiceName)) Then
                Return "NotFound: " & MyServiceName
            Else
                Try

                    Dim AllContextParameters As String = GetContextParameters(MyServiceName)
                    If (String.IsNullOrEmpty(AllContextParameters) = False) Then
                        Try
                            Dim AllParameters() As String = CommandLineToArgs(AllContextParameters)
                            If (AllParameters.Length >= 1) Then
                                Try
                                    Return RemoveStartingQuotes(AllParameters(0))
                                Catch ex As Exception
                                    Return AllContextParameters
                                End Try
                            Else
                                Return AllContextParameters
                            End If
                        Catch ex As Exception
                            Return AllContextParameters
                        End Try
                    Else
                        Return AllContextParameters
                    End If
                Catch ex As Exception
                    Return Nothing
                End Try
            End If

        End Function
        Public Shared Function GetExecutingDirStriped(ByVal strAppInstallationPath As String, ByVal bStrip As Boolean) As String
            Dim strAppInstalltionPathWithVersion As String = strAppInstallationPath
            Try
                strAppInstalltionPathWithVersion = Path.GetDirectoryName(strAppInstallationPath)
                If (bStrip) Then
                    If (strAppInstalltionPathWithVersion.Length > 4) Then
                        Dim thisString As String = strAppInstalltionPathWithVersion.Substring(0, strAppInstalltionPathWithVersion.Length - 4)
                        Dim toStrip As Integer = thisString.Length
                        For i As Integer = thisString.Length - 1 To 0 Step -1
                            If Not Char.IsNumber(thisString(i)) Then
                                Exit For
                            Else
                                toStrip -= 1
                            End If
                        Next
                        Return thisString.Substring(0, toStrip)
                    End If
                End If
            Catch ex As Exception

            End Try
            Return strAppInstalltionPathWithVersion
        End Function
        Public Shared Function GetExecutingDirVersioned(ByVal strAppInstallationPath As String, ByVal strAssemblyVersion As String) As String
            Return strAppInstallationPath & Digitiser(strAssemblyVersion)
        End Function
        Private Shared Function RemoveStartingQuotes(SourceStr As String) As String
            If (String.IsNullOrEmpty(SourceStr)) Then
                Return SourceStr
            Else
                If (SourceStr.Length >= 2) Then
                    If (SourceStr.Substring(0) = """") Then
                        Return SourceStr.Substring(1, SourceStr.Length - 2)
                    Else
                        Return SourceStr
                    End If
                Else
                    Return SourceStr
                End If
            End If
        End Function
        Public Shared Function GetContextParameters(ByVal MyServiceName As String, Optional MachineName As String = Nothing) As String
            Dim hScm As IntPtr = OpenSCManager(IIf(String.IsNullOrEmpty(MachineName), Nothing, MachineName), Nothing, SC_MANAGER_ALL_ACCESS)
            If hScm = IntPtr.Zero Then
                Throw New Win32Exception()
            End If
            Try
                Dim hSvc As IntPtr = OpenService(hScm, MyServiceName, SERVICE_ALL_ACCESS)
                If hSvc = IntPtr.Zero Then
                    Throw New Win32Exception()
                End If
                Try
                    Dim thisConfig As QUERY_SERVICE_CONFIG
                    Dim bytesAllocated As UInteger = 8192
                    ' Per documentation, 8K is max size.
                    Dim ptr As IntPtr = Marshal.AllocHGlobal(CInt(bytesAllocated))
                    Try
                        Dim bytesNeeded As UInteger
                        If Not QueryServiceConfig(hSvc, ptr, bytesAllocated, bytesNeeded) Then
                            Throw New Win32Exception()
                        End If
                        thisConfig = CType(Marshal.PtrToStructure(ptr, GetType(QUERY_SERVICE_CONFIG)), QUERY_SERVICE_CONFIG)
                    Finally
                        Marshal.FreeHGlobal(ptr)
                    End Try

                    Return thisConfig.lpBinaryPathName
                Finally
                    If Not CloseServiceHandle(hSvc) Then
                        Throw New Win32Exception()
                    End If
                End Try
            Finally
                If Not CloseServiceHandle(hScm) Then
                    Throw New Win32Exception()
                End If
            End Try
            Return Nothing
        End Function
        Public Shared Function CommandLineToArgs(commandLine As String) As String()
            Dim argc As Integer
            Dim argv As IntPtr = CommandLineToArgvW(commandLine, argc)
            If argv = IntPtr.Zero Then
                Throw New Win32Exception()
            End If
            Try
                Dim args As String() = New String(argc - 1) {}
                For i As Integer = 0 To args.Length - 1
                    Dim p As IntPtr = Marshal.ReadIntPtr(argv, i * IntPtr.Size)
                    args(i) = Marshal.PtrToStringUni(p)
                Next

                Return args
            Finally
                Marshal.FreeHGlobal(argv)
            End Try
        End Function
        Public Shared Function GetServiceNameFromContextParameters(ByVal StrArgs As String()) As String
            Dim StrServiceName As String = Nothing
            For Each s As String In StrArgs
                If s.StartsWith("/s:", StringComparison.OrdinalIgnoreCase) Then
                    StrServiceName = s.Substring("/s:".Length)
                End If
            Next
            Return ReplaceDashWithSpace(StrServiceName)
        End Function
        Public Shared Sub AddApplicationToStartup(ProductName As String, ExecutablePath As String)
            Try
                Using key As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", True)
                    UpdateLog("Entering AddApplicationToStartup values: " & ProductName & """" & ExecutablePath & """"" - silent")
                    key.SetValue(ProductName, """" & ExecutablePath & """"" - silent")
                End Using
            Catch ex As Exception
                Throw ex
            End Try
           
        End Sub
        Public Shared Sub RemoveApplicationFromStartup(ProductName As String)
            Try
                Using key As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", True)
                    key.DeleteValue(ProductName, False)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
           
        End Sub

        Public Shared Sub Uninstall(serviceName As String, Optional MachineName As String = Nothing)
            Dim scm As IntPtr = OpenSCManager(ScmAccessRights.AllAccess, MachineName)

            Try
                Dim service As IntPtr = OpenService(scm, serviceName, ServiceAccessRights.AllAccess)
                If service = IntPtr.Zero Then
                    Throw New ApplicationException("Service not installed.")
                End If

                Try
                    StopService(service)
                    If Not DeleteService(service) Then
                        Throw New ApplicationException("Could not delete service " & Marshal.GetLastWin32Error())
                    End If
                Finally
                    CloseServiceHandle(service)
                End Try
            Finally
                CloseServiceHandle(scm)
            End Try
        End Sub

        Public Shared Function ServiceIsInstalled(serviceName As String, Optional MachineName As String = Nothing) As Boolean
            Dim scm As IntPtr = OpenSCManager(ScmAccessRights.Connect, MachineName)

            Try
                Dim service As IntPtr = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus)

                If service = IntPtr.Zero Then
                    Return False
                End If

                CloseServiceHandle(service)
                Return True
            Finally
                CloseServiceHandle(scm)
            End Try
        End Function

        Public Shared Sub InstallAndStart(serviceName As String, displayName As String, fileName As String, Optional MachineName As String = Nothing)
            Dim scm As IntPtr = OpenSCManager(ScmAccessRights.AllAccess, MachineName)

            Try
                Dim service As IntPtr = OpenService(scm, serviceName, ServiceAccessRights.AllAccess)

                If service = IntPtr.Zero Then
                    service = CreateService(scm, serviceName, displayName, ServiceAccessRights.AllAccess, SERVICE_WIN32_OWN_PROCESS, ServiceBootFlag.AutoStart, _
                     ServiceError.Normal, fileName, Nothing, IntPtr.Zero, Nothing, Nothing, _
                     Nothing)
                End If

                If service = IntPtr.Zero Then
                    Throw New ApplicationException("Failed to install service.")
                End If

                Try
                    StartService(service)
                Finally
                    CloseServiceHandle(service)
                End Try
            Finally
                CloseServiceHandle(scm)
            End Try
        End Sub

        Public Shared Sub StartService(serviceName As String, Optional MachineName As String = Nothing)
            Dim scm As IntPtr = OpenSCManager(ScmAccessRights.Connect, MachineName)

            Try
                Dim service As IntPtr = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus Or ServiceAccessRights.Start)
                If service = IntPtr.Zero Then
                    Throw New ApplicationException("Could not open service.")
                End If

                Try
                    StartService(service)
                Finally
                    CloseServiceHandle(service)
                End Try
            Finally
                CloseServiceHandle(scm)
            End Try
        End Sub

        Public Shared Sub StopService(serviceName As String, Optional MachineName As String = Nothing)
            Dim scm As IntPtr = OpenSCManager(ScmAccessRights.Connect, MachineName)

            Try
                Dim service As IntPtr = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus Or ServiceAccessRights.[Stop])
                If service = IntPtr.Zero Then
                    Throw New ApplicationException("Could not open service.")
                End If

                Try
                    StopService(service)
                Finally
                    CloseServiceHandle(service)
                End Try
            Finally
                CloseServiceHandle(scm)
            End Try
        End Sub

        Private Shared Sub StartService(service As IntPtr)
            '   Dim status As New SERVICE_STATUS()
            StartService(service, 0, 0)
            Dim changedStatus = WaitForServiceStatus(service, ServiceState.StartPending, ServiceState.Running)
            If Not changedStatus Then
                Throw New ApplicationException("Unable to start service")
            End If
        End Sub

        Private Shared Sub StopService(service As IntPtr)
            Dim status As New SERVICE_STATUS()
            ControlService(service, ServiceControl.[Stop], status)
            Dim changedStatus = WaitForServiceStatus(service, ServiceState.StopPending, ServiceState.Stopped)
            If Not changedStatus Then
                Throw New ApplicationException("Unable to stop service")
            End If
        End Sub

        Public Shared Function GetServiceStatus(serviceName As String, Optional MachineName As String = Nothing) As ServiceState
            Dim scm As IntPtr = OpenSCManager(ScmAccessRights.Connect, MachineName)

            Try
                Dim service As IntPtr = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus)
                If service = IntPtr.Zero Then
                    Return ServiceState.NotFound
                End If

                Try
                    Return GetServiceStatus(service)
                Finally
                    CloseServiceHandle(service)
                End Try
            Finally
                CloseServiceHandle(scm)
            End Try
        End Function

        Private Shared Function GetServiceStatus(service As IntPtr) As ServiceState
            Dim status As New SERVICE_STATUS()

            If QueryServiceStatus(service, status) = 0 Then
                Throw New ApplicationException("Failed to query service status.")
            End If

            Return status.dwCurrentState
        End Function

        Private Shared Function WaitForServiceStatus(service As IntPtr, waitStatus As ServiceState, desiredStatus As ServiceState) As Boolean
            Dim status As New SERVICE_STATUS()

            QueryServiceStatus(service, status)
            If status.dwCurrentState = desiredStatus Then
                Return True
            End If

            Dim dwStartTickCount As Integer = Environment.TickCount
            Dim dwOldCheckPoint As Integer = status.dwCheckPoint

            While status.dwCurrentState = waitStatus
                ' Do not wait longer than the wait hint. A good interval is
                ' one tenth the wait hint, but no less than 1 second and no
                ' more than 10 seconds.

                Dim dwWaitTime As Integer = status.dwWaitHint / 10

                If dwWaitTime < 1000 Then
                    dwWaitTime = 1000
                ElseIf dwWaitTime > 10000 Then
                    dwWaitTime = 10000
                End If

                Thread.Sleep(dwWaitTime)

                ' Check the status again.

                If QueryServiceStatus(service, status) = 0 Then
                    Exit While
                End If

                If status.dwCheckPoint > dwOldCheckPoint Then
                    ' The service is making progress.
                    dwStartTickCount = Environment.TickCount
                    dwOldCheckPoint = status.dwCheckPoint
                Else
                    If Environment.TickCount - dwStartTickCount > status.dwWaitHint Then
                        ' No progress made within the wait hint
                        Exit While
                    End If
                End If
            End While
            Return (status.dwCurrentState = desiredStatus)
        End Function

        Private Shared Function OpenSCManager(rights As ScmAccessRights, Optional MachineName As String = Nothing) As IntPtr
            Dim scm As IntPtr = OpenSCManager(IIf(String.IsNullOrEmpty(MachineName), Nothing, MachineName), Nothing, rights)
            If scm = IntPtr.Zero Then
                Throw New ApplicationException("Could not connect to service control manager.")
            End If

            Return scm
        End Function


        Public Shared Function InstallSDK(ByVal sFujitSDKpath As String) As Boolean
            Dim SDKInstalled As Boolean = False
            ''Dim config1 As Configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)           
            UpdateLog("Inside function : InstallSDK ")

            If True Then '' put flag

                Dim strpath As String = sFujitSDKpath & "\FiScnRun.exe"

                '' lblProgress.Text = "Installing Fujitsu Runtime. This may take few minutes."
                UpdateLog("Fujitsu Scanner Control SDK installation Started.")
                If ExecuteEXE(strpath, "-silent") = False AndAlso _IsSDKExists = False Then
                    '' lblFailed.Text = "Fujitsu Runtime installation failed." & Environment.NewLine & "Try Again or Contact System Administrator."
                    '' setPanels("Failed")
                    UpdateLog("Fujitsu Scanner Control SDK installation Failed.")
                End If
                '' lblProgress.Text = "Installed Fujitsu Runtime successfully."
                SDKInstalled = True
            Else
                UpdateLog("Fujitsu Scanner Control SDK already installed.")
            End If

            ''lblProgress.Text = "Updating gloLDSSniffer Service. This may take few minutes."
            Return SDKInstalled
        End Function

        Public Shared Function ExecuteEXE(ByVal strsetup As String, ByVal strSwitch As String) As Boolean
            Dim _success As Boolean = False
            Dim oProcess As System.Diagnostics.Process = New Process()
            Try
                oProcess.StartInfo.FileName = strsetup
                oProcess.StartInfo.Arguments = strSwitch
                oProcess.StartInfo.UseShellExecute = False
                oProcess.StartInfo.CreateNoWindow = True
                oProcess.StartInfo.RedirectStandardOutput = False
                oProcess.Start()
                Do
                    oProcess.WaitForExit(1000)
                    oProcess.Refresh()
                    '' Thread.Sleep(10000)
                    ''Application.DoEvents()
                Loop While Not oProcess.HasExited

                If oProcess.ExitCode = 0 OrElse oProcess.ExitCode = 3010 OrElse oProcess.ExitCode = 5100 Then
                    _success = True
                ElseIf oProcess.ExitCode = 1603 Then
                    _IsSDKExists = True
                End If
                UpdateLog("Fujitsu Scanner Control SDK installation Completed.")
                UpdateLog("" & strSwitch & " exit  code :" + oProcess.ExitCode.ToString() & "")
            Catch ex As System.ArgumentNullException
                UpdateLog("Exception ArgumentNullException: While Executing EXE " & strsetup & " " + ex.Message.ToString() & "")
            Catch ex As Exception
                UpdateLog("Exception : While Executing EXE " & strsetup & " " + ex.Message.ToString() & "")
            Finally
                If oProcess IsNot Nothing Then
                    oProcess.Close()
                    oProcess.Dispose()
                End If
            End Try

            Return _success
        End Function

   

#Region "OpenSCManager"
        <DllImport("advapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
        Private Shared Function OpenSCManager(lpMachineName As String, lpDatabaseName As String, dwDesiredAccess As UInteger) As IntPtr
        End Function
#End Region
#Region "OpenService"
        <DllImport("advapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
        Private Shared Function OpenService(hSCManager As IntPtr, lpServiceName As String, dwDesiredAccess As UInteger) As IntPtr
        End Function
#End Region
#Region "QUERY_SERVICE_CONFIG"
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
        Private Structure QUERY_SERVICE_CONFIG
            Public dwServiceType As UInteger
            Public dwStartType As UInteger
            Public dwErrorControl As UInteger
            Public lpBinaryPathName As String
            Public lpLoadOrderGroup As String
            Public dwTagId As UInteger
            Public lpDependencies As String
            Public lpServiceStartName As String
            Public lpDisplayName As String
        End Structure
#End Region
#Region "QueryServiceConfig"
        <DllImport("advapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
        Private Shared Function QueryServiceConfig(hService As IntPtr, lpServiceConfig As IntPtr, cbBufSize As UInteger, ByRef pcbBytesNeeded As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function
#End Region
#Region "ChangeServiceConfig"
        <DllImport("advapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
        Private Shared Function ChangeServiceConfig(hService As IntPtr, dwServiceType As UInteger, dwStartType As UInteger, dwErrorControl As UInteger, lpBinaryPathName As String, lpLoadOrderGroup As String, _
   lpdwTagId As IntPtr, lpDependencies As String, lpServiceStartName As String, lpPassword As String, lpDisplayName As String) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function
#End Region
#Region "OpenService"
        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Private Shared Function OpenService(hSCManager As IntPtr, lpServiceName As String, dwDesiredAccess As ServiceAccessRights) As IntPtr
        End Function
#End Region

#Region "CreateService"
        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Private Shared Function CreateService(hSCManager As IntPtr, lpServiceName As String, lpDisplayName As String, dwDesiredAccess As ServiceAccessRights, dwServiceType As Integer, dwStartType As ServiceBootFlag, _
  dwErrorControl As ServiceError, lpBinaryPathName As String, lpLoadOrderGroup As String, lpdwTagId As IntPtr, lpDependencies As String, lp As String, _
  lpPassword As String) As IntPtr
        End Function
#End Region

#Region "CloseServiceHandle"
        <DllImport("advapi32.dll", SetLastError:=True)> _
        Private Shared Function CloseServiceHandle(hSCObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function
#End Region

#Region "QueryServiceStatus"
        <DllImport("advapi32.dll")> _
        Private Shared Function QueryServiceStatus(hService As IntPtr, lpServiceStatus As SERVICE_STATUS) As Integer
        End Function
#End Region

#Region "DeleteService"
        <DllImport("advapi32.dll", SetLastError:=True)> _
        Private Shared Function DeleteService(hService As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function
#End Region

#Region "ControlService"
        <DllImport("advapi32.dll")> _
        Private Shared Function ControlService(hService As IntPtr, dwControl As ServiceControl, lpServiceStatus As SERVICE_STATUS) As Integer
        End Function
#End Region

#Region "StartService"
        <DllImport("advapi32.dll", SetLastError:=True)> _
        Private Shared Function StartService(hService As IntPtr, dwNumServiceArgs As Integer, lpServiceArgVectors As Integer) As Integer
        End Function
#End Region

        Private Const SERVICE_NO_CHANGE As UInteger = &HFFFFFFFFUI
        Private Const SC_MANAGER_ALL_ACCESS As UInteger = &HF003FUI
        Private Const SERVICE_ALL_ACCESS As UInteger = &HF01FFUI

        Private Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
        Private Const SERVICE_WIN32_OWN_PROCESS As Integer = &H10

        <StructLayout(LayoutKind.Sequential)> _
        Private Class SERVICE_STATUS
            Public dwServiceType As Integer = 0
            Public dwCurrentState As ServiceState = 0
            Public dwControlsAccepted As Integer = 0
            Public dwWin32ExitCode As Integer = 0
            Public dwServiceSpecificExitCode As Integer = 0
            Public dwCheckPoint As Integer = 0
            Public dwWaitHint As Integer = 0
        End Class
        Public Enum ServiceState
            Unknown = -1
            ' The state cannot be (has not been) retrieved.
            NotFound = 0
            ' The service is not known on the host server.
            Stopped = 1
            StartPending = 2
            StopPending = 3
            Running = 4
            ContinuePending = 5
            PausePending = 6
            Paused = 7
        End Enum

        <Flags()> _
        Public Enum ScmAccessRights
            Connect = &H1
            CreateService = &H2
            EnumerateService = &H4
            Lock = &H8
            QueryLockStatus = &H10
            ModifyBootConfig = &H20
            StandardRightsRequired = &HF0000
            AllAccess = (StandardRightsRequired Or Connect Or CreateService Or EnumerateService Or Lock Or QueryLockStatus Or ModifyBootConfig)
        End Enum

        <Flags()> _
        Public Enum ServiceAccessRights
            QueryConfig = &H1
            ChangeConfig = &H2
            QueryStatus = &H4
            EnumerateDependants = &H8
            Start = &H10
            [Stop] = &H20
            PauseContinue = &H40
            Interrogate = &H80
            UserDefinedControl = &H100
            Delete = &H10000
            StandardRightsRequired = &HF0000
            AllAccess = (StandardRightsRequired Or QueryConfig Or ChangeConfig Or QueryStatus Or EnumerateDependants Or Start Or [Stop] Or PauseContinue Or Interrogate Or UserDefinedControl)
        End Enum

        Public Enum ServiceBootFlag
            Start = &H0
            SystemStart = &H1
            AutoStart = &H2
            DemandStart = &H3
            Disabled = &H4
        End Enum

        Public Enum ServiceControl
            [Stop] = &H1
            Pause = &H2
            [Continue] = &H3
            Interrogate = &H4
            Shutdown = &H5
            ParamChange = &H6
            NetBindAdd = &H7
            NetBindRemove = &H8
            NetBindEnable = &H9
            NetBindDisable = &HA
        End Enum

        Public Enum ServiceError
            Ignore = &H0
            Normal = &H1
            Severe = &H2
            Critical = &H3
        End Enum
        <DllImport("shell32.dll", SetLastError:=True)> _
        Private Shared Function CommandLineToArgvW(<MarshalAs(UnmanagedType.LPWStr)> lpCmdLine As String, ByRef pNumArgs As Integer) As IntPtr
        End Function

        Private Shared LogFilePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "InstallerLog.txt")
        Private Shared constEncryptDecryptKeyRegistry As String = "20gloStreamInc08"

        Public Shared Sub UpdateLog(ByVal strLogMessage As String)
            Try

                Dim objFile As New StreamWriter(LogFilePath, True)
                objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
                objFile.Close()
                objFile.Dispose()
                objFile = Nothing
            Catch ex As Exception

            End Try
        End Sub
        Public Shared Function WriteValueToServicesRegistryKey(ByVal strKey As String, ByVal strValue As String, Optional ByVal bEncryption As Boolean = False) As String
            Try
                CopyServicesSubKeyIfNotExists()
            Catch ex As Exception

            End Try

            Dim regKey As RegistryKey = Nothing
            Try
                regKey = Registry.LocalMachine.OpenSubKey("Software\" & DynamicRegistryName, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl)
                If regKey Is Nothing Then
                    regKey = Registry.LocalMachine.OpenSubKey("Software", True)
                    If regKey IsNot Nothing Then
                        Dim lregKey As RegistryKey = regKey.CreateSubKey(DynamicRegistryName)
                        lregKey.Close()
                        lregKey.Dispose()
                    End If
                End If

            Catch ex As Exception
            Finally
                If regKey IsNot Nothing Then
                    regKey.Close()
                    regKey.Dispose()
                    regKey = Nothing
                End If
            End Try
            Try
                regKey = Registry.LocalMachine.OpenSubKey("Software\" & DynamicRegistryName, True)
                If (IsNothing(regKey) = False) Then
                    If (bEncryption) Then
                        Dim objEncryption As gloEncryption = New gloEncryption()
                        regKey.SetValue(strKey, objEncryption.EncryptToBase64String(strValue, constEncryptDecryptKeyRegistry))
                        objEncryption = Nothing
                    Else
                        regKey.SetValue(strKey, strValue.Trim())
                    End If

                    Return "Success"
                Else
                    Return "Registry Key:  Software\" & DynamicRegistryName & " not found in " & Registry.LocalMachine.ToString()
                End If

            Catch ex As Exception
                Return "Exception Occurred: " & ex.ToString()
            Finally
                If regKey IsNot Nothing Then
                    regKey.Close()
                    regKey.Dispose()
                    regKey = Nothing
                End If
            End Try
        End Function
        Public Shared Function ReadValueFromServicesRegistryKey(ByVal strKey As String, ByRef strValue As String, Optional ByVal bEncryption As Boolean = False) As String
            strValue = ""
            Try
                CopyServicesSubKeyIfNotExists()
            Catch ex As Exception

            End Try

            Dim regKey As RegistryKey = Nothing
            Try
                regKey = Registry.LocalMachine.OpenSubKey("Software\" & DynamicRegistryName, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl)
                If regKey Is Nothing Then
                    regKey = Registry.LocalMachine.OpenSubKey("Software", True)
                    If regKey IsNot Nothing Then
                        Dim lregKey As RegistryKey = regKey.CreateSubKey(DynamicRegistryName)
                        lregKey.Close()
                        lregKey.Dispose()
                    End If
                End If

            Catch ex As Exception
            Finally
                If regKey IsNot Nothing Then
                    regKey.Close()
                    regKey.Dispose()
                    regKey = Nothing
                End If
            End Try
            Try
                regKey = Registry.LocalMachine.OpenSubKey("Software\" & DynamicRegistryName, False)
                If (IsNothing(regKey) = False) Then
                    Dim objKey As Object = regKey.GetValue(strKey)
                    If (IsNothing(objKey) = False) Then
                        If (bEncryption) Then
                            Dim strPassword As String = DirectCast(objKey, String)
                            Dim objEncryption As gloEncryption = New gloEncryption()
                            strValue = objEncryption.DecryptFromBase64String(strPassword, constEncryptDecryptKeyRegistry)
                            objEncryption = Nothing
                        Else
                            strValue = DirectCast(objKey, String)
                        End If

                    End If
                    Return "Success"
                Else
                    Return "Registry Key:  Software\" & DynamicRegistryName & " not found in " & Registry.LocalMachine.ToString()
                End If

            Catch ex As Exception
                Return "Exception Occurred: " & ex.ToString()
            Finally
                If regKey IsNot Nothing Then
                    regKey.Close()
                    regKey.Dispose()
                    regKey = Nothing
                End If
            End Try
        End Function
        Public Shared Function WriteToServicesRegistry(ByVal txtServer As String, ByVal txtDatabase As String, ByVal txtUser As String, ByVal txtPassword As String) As String
            Try
                CopyServicesSubKeyIfNotExists()
            Catch ex As Exception

            End Try

            Dim regKey As RegistryKey = Nothing
            Try
                regKey = Registry.LocalMachine.OpenSubKey("Software\" & DynamicRegistryName, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl)
                If regKey Is Nothing Then
                    regKey = Registry.LocalMachine.OpenSubKey("Software", True)
                    If regKey IsNot Nothing Then
                        Dim lregKey As RegistryKey = regKey.CreateSubKey(DynamicRegistryName)
                        lregKey.Close()
                        lregKey.Dispose()
                    End If
                End If

            Catch ex As Exception
            Finally
                If regKey IsNot Nothing Then
                    regKey.Close()
                    regKey.Dispose()
                    regKey = Nothing
                End If
            End Try
            Try
                regKey = Registry.LocalMachine.OpenSubKey("Software\" & DynamicRegistryName, True)
                If (IsNothing(regKey) = False) Then
                    regKey.SetValue("SQLServer", txtServer.Trim())
                    regKey.SetValue("Database", txtDatabase.Trim())
                    regKey.SetValue("SQLUser", txtUser.Trim())
                    Dim objEncryption As gloEncryption = New gloEncryption()
                    regKey.SetValue("SQLPassword", objEncryption.EncryptToBase64String(txtPassword, constEncryptDecryptKeyRegistry))
                    objEncryption = Nothing
                    Return "Success"
                Else
                    Return "Registry Key:  Software\" & DynamicRegistryName & " not found in " & Registry.LocalMachine.ToString()
                End If

            Catch ex As Exception
                Return "Exception Occurred: " & ex.ToString()
            Finally
                If regKey IsNot Nothing Then
                    regKey.Close()
                    regKey.Dispose()
                    regKey = Nothing
                End If
            End Try
        End Function
        Public Shared Function ReadFromServicesRegistry(ByRef txtServer As String, ByRef txtDatabase As String, ByRef txtUser As String, ByRef txtPassword As String) As String
            txtServer = ""
            txtDatabase = ""
            txtUser = ""
            txtPassword = ""
            Try
                CopyServicesSubKeyIfNotExists()
            Catch ex As Exception

            End Try

            Dim regKey As RegistryKey = Nothing
            Try
                regKey = Registry.LocalMachine.OpenSubKey("Software\" & DynamicRegistryName, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl)
                If regKey Is Nothing Then
                    regKey = Registry.LocalMachine.OpenSubKey("Software", True)
                    If regKey IsNot Nothing Then
                        Dim lregKey As RegistryKey = regKey.CreateSubKey(DynamicRegistryName)
                        lregKey.Close()
                        lregKey.Dispose()
                    End If
                End If

            Catch ex As Exception
            Finally
                If regKey IsNot Nothing Then
                    regKey.Close()
                    regKey.Dispose()
                    regKey = Nothing
                End If
            End Try
            Try
                regKey = Registry.LocalMachine.OpenSubKey("Software\" & DynamicRegistryName, False)
                If (IsNothing(regKey) = False) Then
                    Dim objKey As Object = regKey.GetValue("SQLServer")
                    If (IsNothing(objKey) = False) Then
                        txtServer = DirectCast(objKey, String)
                    End If
                    objKey = regKey.GetValue("Database")
                    If (IsNothing(objKey) = False) Then
                        txtDatabase = DirectCast(objKey, String)
                    End If
                    objKey = regKey.GetValue("SQLUser")
                    If (IsNothing(objKey) = False) Then
                        txtUser = DirectCast(objKey, String)
                    End If
                    objKey = regKey.GetValue("SQLPassword")
                    If (IsNothing(objKey) = False) Then
                        Dim strPassword As String = DirectCast(objKey, String)
                        Dim objEncryption As gloEncryption = New gloEncryption()
                        txtPassword = objEncryption.DecryptFromBase64String(strPassword, constEncryptDecryptKeyRegistry)
                        objEncryption = Nothing
                    End If
                    Return "Success"
                Else
                    Return "Registry Key:  Software\" & DynamicRegistryName & " not found in " & Registry.LocalMachine.ToString()
                End If

            Catch ex As Exception
                Return "Exception Occurred: " & ex.ToString()
            Finally
                If regKey IsNot Nothing Then
                    regKey.Close()
                    regKey.Dispose()
                    regKey = Nothing
                End If
            End Try
        End Function

        Public Shared Function ReadFromHL7ServicesRegistry(ByRef txtServer As String, ByRef txtDatabase As String, ByRef txtUser As String, ByRef txtPassword As String) As String
            txtServer = ""
            txtDatabase = ""
            txtUser = ""
            txtPassword = ""
            Try
                CopyServicesSubKeyIfNotExists()
            Catch ex As Exception

            End Try

            Dim regKey As RegistryKey = Nothing
            Try
                regKey = Registry.LocalMachine.OpenSubKey("Software\" & DynamicHL7RegistryName, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl)
                If regKey Is Nothing Then
                    regKey = Registry.LocalMachine.OpenSubKey("Software", True)
                    If regKey IsNot Nothing Then
                        Dim lregKey As RegistryKey = regKey.CreateSubKey(DynamicHL7RegistryName)
                        lregKey.Close()
                        lregKey.Dispose()
                    End If
                End If

            Catch ex As Exception
            Finally
                If regKey IsNot Nothing Then
                    regKey.Close()
                    regKey.Dispose()
                    regKey = Nothing
                End If
            End Try
            Try
                regKey = Registry.LocalMachine.OpenSubKey("Software\" & DynamicHL7RegistryName, False)
                If (IsNothing(regKey) = False) Then
                    Dim objKey As Object = regKey.GetValue("SQLServer")
                    If (IsNothing(objKey) = False) Then
                        txtServer = DirectCast(objKey, String)
                    End If
                    objKey = regKey.GetValue("Database")
                    If (IsNothing(objKey) = False) Then
                        txtDatabase = DirectCast(objKey, String)
                    End If
                    objKey = regKey.GetValue("SQLUser")
                    If (IsNothing(objKey) = False) Then
                        txtUser = DirectCast(objKey, String)
                    End If
                    objKey = regKey.GetValue("SQLPassword")
                    If (IsNothing(objKey) = False) Then
                        Dim strPassword As String = DirectCast(objKey, String)
                        Dim objEncryption As gloEncryption = New gloEncryption()
                        txtPassword = objEncryption.DecryptFromBase64String(strPassword, constEncryptDecryptKeyRegistry)
                        objEncryption = Nothing
                    End If
                    Return "Success"
                Else
                    Return "Registry Key:  Software\" & DynamicHL7RegistryName & " not found in " & Registry.LocalMachine.ToString()
                End If

            Catch ex As Exception
                Return "Exception Occurred: " & ex.ToString()
            Finally
                If regKey IsNot Nothing Then
                    regKey.Close()
                    regKey.Dispose()
                    regKey = Nothing
                End If
            End Try
        End Function
    End Class


    Public Class gloEncryption

        Private key As Byte() = New Byte() {}

        Private IV As Byte() = New Byte() {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        Public Function DecryptFromBase64String(stringToDecrypt As String, sEncryptionKey As String) As String

            Try
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8))

                Using des As New DESCryptoServiceProvider()
                    ' we have a base 64 encoded string so first must decode to regular unencoded (encrypted) string
                    Dim inputByteArray As Byte() = Convert.FromBase64String(stringToDecrypt)
                    ' now decrypt the regular string
                    Using ms As New MemoryStream()
                        Using cs As New CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)
                            cs.Write(inputByteArray, 0, inputByteArray.Length)
                            cs.FlushFinalBlock()
                            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                            Return encoding.GetString(ms.ToArray())
                        End Using
                    End Using

                End Using
            Catch e As Exception
                Return e.Message
            End Try
        End Function

        Public Function EncryptToBase64String(stringToEncrypt As String, SEncryptionKey As String) As String
            Try
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8))
                Using des As New DESCryptoServiceProvider()
                    ' convert our input string to a byte array
                    Dim inputByteArray As Byte() = Encoding.UTF8.GetBytes(stringToEncrypt)
                    'now encrypt the bytearray
                    Using ms As New MemoryStream()
                        Using cs As New CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write)
                            cs.Write(inputByteArray, 0, inputByteArray.Length)
                            cs.FlushFinalBlock()
                            ' now return the byte array as a "safe for XMLDOM" Base64 String
                            Return Convert.ToBase64String(ms.ToArray())
                        End Using
                    End Using
                End Using
            Catch e As Exception
                Return e.Message
            End Try
        End Function
    End Class

    <PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
    Public Class ImpersonateUser
        Implements IDisposable

        Public Enum LogonTypes
            ''' <summary>
            ''' This logon type is intended for users who will be interactively using the computer, such as a user being logged on  
            ''' by a terminal server, remote shell, or similar process.
            ''' This logon type has the additional expense of caching logon information for disconnected operations; 
            ''' therefore, it is inappropriate for some client/server applications,
            ''' such as a mail server.
            ''' </summary>
            LOGON32_LOGON_INTERACTIVE = 2

            ''' <summary>
            ''' This logon type is intended for high performance servers to authenticate plaintext passwords.
            ''' The LogonUser function does not cache credentials for this logon type.
            ''' </summary>
            LOGON32_LOGON_NETWORK = 3

            ''' <summary>
            ''' This logon type is intended for batch servers, where processes may be executing on behalf of a user without 
            ''' their direct intervention. This type is also for higher performance servers that process many plaintext
            ''' authentication attempts at a time, such as mail or Web servers. 
            ''' The LogonUser function does not cache credentials for this logon type.
            ''' </summary>
            LOGON32_LOGON_BATCH = 4

            ''' <summary>
            ''' Indicates a service-type logon. The account provided must have the service privilege enabled. 
            ''' </summary>
            LOGON32_LOGON_SERVICE = 5

            ''' <summary>
            ''' This logon type is for GINA DLLs that log on users who will be interactively using the computer. 
            ''' This logon type can generate a unique audit record that shows when the workstation was unlocked. 
            ''' </summary>
            LOGON32_LOGON_UNLOCK = 7

            ''' <summary>
            ''' This logon type preserves the name and password in the authentication package, which allows the server to make 
            ''' connections to other network servers while impersonating the client. A server can accept plaintext credentials 
            ''' from a client, call LogonUser, verify that the user can access the system across the network, and still 
            ''' communicate with other servers.
            ''' NOTE: Windows NT:  This value is not supported. 
            ''' </summary>
            LOGON32_LOGON_NETWORK_CLEARTEXT = 8

            ''' <summary>
            ''' This logon type allows the caller to clone its current token and specify new credentials for outbound connections.
            ''' The new logon session has the same local identifier but uses different credentials for other network connections. 
            ''' NOTE: This logon type is supported only by the LOGON32_PROVIDER_WINNT50 logon provider.
            ''' NOTE: Windows NT:  This value is not supported. 
            ''' </summary>
            LOGON32_LOGON_NEW_CREDENTIALS = 9
        End Enum

        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Private Shared Function LogonUser(lpszUsername As [String], lpszDomain As [String], lpszPassword As [String], dwLogonType As Integer, dwLogonProvider As Integer, ByRef phToken As SafeTokenHandle) As Boolean
        End Function

        Public Sub New(Domain As String, UserName As String, Password As String, Optional LogonType As LogonTypes = LogonTypes.LOGON32_LOGON_INTERACTIVE)
            Dim ok = LogonUser(UserName, Domain, Password, LogonType, 0, _SafeTokenHandle)
            If Not ok Then
                Dim errorCode = Marshal.GetLastWin32Error()
                Throw New ApplicationException(String.Format("Could not impersonate the elevated user.  LogonUser returned error code {0}.", errorCode))
            End If

            WindowsImpersonationContext = WindowsIdentity.Impersonate(_SafeTokenHandle.DangerousGetHandle())
        End Sub

        Private ReadOnly _SafeTokenHandle As New SafeTokenHandle
        Private ReadOnly WindowsImpersonationContext As WindowsImpersonationContext
        Private Shared DisposedHere As Boolean = False

        Public Sub Dispose() Implements System.IDisposable.Dispose
            If (DisposedHere = False) Then
                Me.WindowsImpersonationContext.Dispose()
                Me._SafeTokenHandle.Dispose()
                DisposedHere = True
            End If

        End Sub
        ' Stops impersonation
        Public Sub Undo()
            Me.WindowsImpersonationContext.Undo()
            Me.Dispose()
        End Sub

        Public NotInheritable Class SafeTokenHandle
            Inherits SafeHandleZeroOrMinusOneIsInvalid

            <DllImport("kernel32.dll")> _
            <ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)> _
            <SuppressUnmanagedCodeSecurity()> _
            Private Shared Function CloseHandle(handle As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
            End Function

            Public Sub New()
                MyBase.New(True)
            End Sub

            Protected Overrides Function ReleaseHandle() As Boolean
                Return CloseHandle(handle)
            End Function
        End Class

    End Class
    Public Class NetworkShare
        Implements IDisposable
#Region "Member Variables"
        <DllImport("mpr.dll")> _
        Private Shared Function WNetAddConnection2(ByRef refNetResource As NetResource, inPassword As String, inUsername As String, inFlags As Integer) As Integer
        End Function
        <DllImport("mpr.dll")> _
        Private Shared Function WNetCancelConnection2(inServer As String, inFlags As Integer, inForce As Integer) As Integer
        End Function

        Private _Server As [String]
        Private _Share As [String]
        Private _DriveLetter As [String] = Nothing
        Private _Username As [String] = Nothing
        Private _Password As [String] = Nothing
        Private _Flags As Integer = 0
        Private _NetResource As New NetResource()
        Private _AllowDisconnectWhenInUse As Int32 = 0
        ' 0 = False; Any other value is True;
#End Region
#Region "Constructors"
        ''' <summary>
        ''' The default constructor
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' This constructor takes a server and a share.
        ''' </summary>
        Public Sub New(inServer As [String], inShare As [String])
            _Server = inServer
            _Share = inShare
        End Sub

        ''' <summary>
        ''' This constructor takes a server and a share and a local drive letter.
        ''' </summary>
        Public Sub New(inServer As [String], inShare As [String], inDriveLetter As [String])
            _Server = inServer
            _Share = inShare
            DriveLetter = inDriveLetter
        End Sub

        ''' <summary>
        ''' This constructor takes a server, share, username, and password.
        ''' </summary>
        Public Sub New(inServer As [String], inShare As [String], inUsername As [String], inPassword As [String])
            _Server = inServer
            _Share = inShare
            _Username = inUsername
            _Password = inPassword
        End Sub

        ''' <summary>
        ''' This constructor takes a server, share, drive letter, username, and password.
        ''' </summary>
        Public Sub New(inServer As [String], inShare As [String], inDriveLetter As [String], inUsername As [String], inPassword As [String])
            _Server = inServer
            _Share = inShare
            _DriveLetter = inDriveLetter
            _Username = inUsername
            _Password = inPassword
        End Sub
#End Region

#Region "Properties"
        Public Property Server() As [String]
            Get
                Return _Server
            End Get
            Set(value As [String])
                _Server = value
            End Set
        End Property

        Public Property Share() As [String]
            Get
                Return _Share
            End Get
            Set(value As [String])
                _Share = value
            End Set
        End Property

        Public ReadOnly Property FullPath() As [String]
            Get
                Return String.Format("\\{0}\{1}", _Server, _Share)
            End Get
        End Property

        Public Property DriveLetter() As [String]
            Get
                Return _DriveLetter
            End Get
            Set(value As [String])
                SetDriveLetter(value)
            End Set
        End Property

        Public Property Username() As [String]
            Get
                Return If([String].IsNullOrEmpty(_Username), Nothing, _Username)
            End Get
            Set(value As [String])
                _Username = value
            End Set
        End Property

        Public Property Password() As [String]
            Get
                Return If([String].IsNullOrEmpty(_Password), Nothing, _Username)
            End Get
            Set(value As [String])
                _Password = value
            End Set
        End Property

        Public Property Flags() As Integer
            Get
                Return _Flags
            End Get
            Set(value As Integer)
                _Flags = value
            End Set
        End Property

        Public Property Resource() As NetResource
            Get
                Return _NetResource
            End Get
            Set(value As NetResource)
                _NetResource = value
            End Set
        End Property

        Public Property AllowDisconnectWhenInUse() As Boolean
            Get
                Return Convert.ToBoolean(_AllowDisconnectWhenInUse)
            End Get
            Set(value As Boolean)
                _AllowDisconnectWhenInUse = Convert.ToInt32(value)
            End Set
        End Property
        Private _connected As Boolean = False
#End Region

#Region "Functions"
        ''' <summary>
        ''' Establishes a connection to the share.
        '''
        ''' Throws:
        '''
        '''
        ''' </summary>
        Public Function Connect() As Integer
            Try
                _NetResource.Scope = CInt(Scope.RESOURCE_GLOBALNET)
                _NetResource.Type = CInt(Type.RESOURCETYPE_DISK)
                _NetResource.DisplayType = CInt(DisplayType.RESOURCEDISPLAYTYPE_SHARE)
                _NetResource.Usage = CInt(Usage.RESOURCEUSAGE_CONNECTABLE)
                _NetResource.RemoteName = FullPath
                _NetResource.LocalName = DriveLetter
                _connected = True
                Return WNetAddConnection2(_NetResource, _Password, _Username, _Flags)
            Catch ex As Exception
                _connected = False
                Throw ex
            Finally

            End Try

        End Function

        ''' <summary>
        ''' Disconnects from the share.
        ''' </summary>
        Public Function Disconnect() As Integer
            Dim retVal As Integer = 0
            Try
                If (_connected) Then
                    If _DriveLetter IsNot Nothing Then
                        retVal = WNetCancelConnection2(_DriveLetter, _Flags, _AllowDisconnectWhenInUse)
                        retVal = WNetCancelConnection2(FullPath, _Flags, _AllowDisconnectWhenInUse)
                    Else
                        retVal = WNetCancelConnection2(FullPath, _Flags, _AllowDisconnectWhenInUse)
                    End If
                    _connected = False
                    DisposedHere = True
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return retVal
        End Function

        Private Sub SetDriveLetter(inString As [String])
            If inString.Length = 1 Then
                If Char.IsLetter(inString.ToCharArray()(0)) Then
                    _DriveLetter = inString & ":"
                Else
                    ' The character is not a drive letter
                    _DriveLetter = Nothing
                End If
            ElseIf inString.Length = 2 Then
                Dim drive As Char() = inString.ToCharArray()
                If Char.IsLetter(drive(0)) AndAlso drive(1) = ":"c Then
                    _DriveLetter = inString
                Else
                    ' The character is not a drive letter
                    _DriveLetter = Nothing
                End If
            Else
                ' If we get here the value passed in is not valid
                ' so make it null.
                _DriveLetter = Nothing
            End If
        End Sub
#End Region
        Private ReadOnly WindowsImpersonationContext As WindowsImpersonationContext
        Private Shared DisposedHere As Boolean = False

        Public Sub Dispose() Implements System.IDisposable.Dispose
            If (DisposedHere = False) Then
                If (_connected) Then
                    Me.Disconnect()
                End If
                DisposedHere = True
            End If

        End Sub
#Region "NetResource Struct"
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure NetResource
            Public Scope As UInteger
            Public Type As UInteger
            Public DisplayType As UInteger
            Public Usage As UInteger
            Public LocalName As String
            Public RemoteName As String
            Public Comment As String
            Public Provider As String
        End Structure
#End Region

#Region "Enums"
        Public Enum Scope
            RESOURCE_CONNECTED = 1
            RESOURCE_GLOBALNET
            RESOURCE_REMEMBERED
            RESOURCE_RECENT
            RESOURCE_CONTEXT
        End Enum

        Public Enum Type As UInteger
            RESOURCETYPE_ANY
            RESOURCETYPE_DISK
            RESOURCETYPE_PRINT
            RESOURCETYPE_RESERVED = 8
            RESOURCETYPE_UNKNOWN = 4294967295UI
        End Enum

        Public Enum DisplayType
            RESOURCEDISPLAYTYPE_GENERIC
            RESOURCEDISPLAYTYPE_DOMAIN
            RESOURCEDISPLAYTYPE_SERVER
            RESOURCEDISPLAYTYPE_SHARE
            RESOURCEDISPLAYTYPE_FILE
            RESOURCEDISPLAYTYPE_GROUP
            RESOURCEDISPLAYTYPE_NETWORK
            RESOURCEDISPLAYTYPE_ROOT
            RESOURCEDISPLAYTYPE_SHAREADMIN
            RESOURCEDISPLAYTYPE_DIRECTORY
            RESOURCEDISPLAYTYPE_TREE
            RESOURCEDISPLAYTYPE_NDSCONTAINER
        End Enum

        Public Enum Usage As UInteger
            RESOURCEUSAGE_CONNECTABLE = 1
            RESOURCEUSAGE_CONTAINER = 2
            RESOURCEUSAGE_NOLOCALDEVICE = 4
            RESOURCEUSAGE_SIBLING = 8
            RESOURCEUSAGE_ATTACHED = 16
            RESOURCEUSAGE_ALL = 31
            RESOURCEUSAGE_RESERVED = 2147483648UI
        End Enum

        Public Enum ConnectionFlags As UInteger
            CONNECT_UPDATE_PROFILE = 1
            CONNECT_UPDATE_RECENT = 2
            CONNECT_TEMPORARY = 4
            CONNECT_INTERACTIVE = 8
            CONNECT_PROMPT = 16
            CONNECT_NEED_DRIVE = 32
            CONNECT_REFCOUNT = 64
            CONNECT_REDIRECT = 128
            CONNECT_LOCALDRIVE = 256
            CONNECT_CURRENT_MEDIA = 512
            CONNECT_DEFERRED = 1024
            CONNECT_COMMANDLINE = 2048
            CONNECT_CMD_SAVECRED = 4096
            CONNECT_CRED_RESET = 8192
            CONNECT_RESERVED = 4278190080UI
        End Enum
#End Region
#Region "demo"
        '        using System;
        'using Microsoft.Win32;
        'using NetworkConnection;
        'using System.Management;

        'namespace RemoteRegistryExample
        '{
        '            Class Program
        '    {
        '        static void Main(string[] args)
        '        {
        '            String ServerName = "Server1";

        '            // Create an object that can authenticate to a network share when you
        '            // already have credentials
        '            NetworkShare share = new NetworkShare(ServerName, "ipc$");

        '            // Create an object that can authenticate to a network share when you
        '            // do NOT already have credentials
        '            //NetworkShare share = new NetworkShare(ServerName, "C$", "User", "SomePasswd");

        '            // Connect to the remote drive
        '            share.Connect();

        '            // Note: another connection option is to add a reference to System.Management,
        '            // Then add a using statement for System.Management and use ConnectionOptions
        '            // and ManagementScope objects. For more information see this link:
        '            // http://msdn.microsoft.com/en-us/library/system.management.managementscope%28v=VS.100%29.aspx

        '            // If these same credentials allow remote registry, you are now authenticated
        '            // Get the Windows ProductName from HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion
        '            String ProductName = string.Empty;
        '            RegistryKey key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ServerName);
        '            if (key != null)
        '            {
        '                key = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
        '                if (key != null)
        '                    ProductName = key.GetValue("ProductName").ToString();
        '            }

        '            // Display the value
        '            Console.WriteLine("The device " + ServerName + " is running " + ProductName + ".");

        '            // Disconnect the share
        '            share.Disconnect();
        '        }
        '    }
        '}

#End Region
    End Class

End Namespace

