﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Maximized")>  _
        Public Property WindowState() As Global.System.Windows.Forms.FormWindowState
            Get
                Return CType(Me("WindowState"),Global.System.Windows.Forms.FormWindowState)
            End Get
            Set
                Me("WindowState") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Normal")>  _
        Public Property Normal() As Global.System.Windows.Forms.FormWindowState
            Get
                Return CType(Me("Normal"),Global.System.Windows.Forms.FormWindowState)
            End Get
            Set
                Me("Normal") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Minimized")>  _
        Public Property Minimized() As Global.System.Windows.Forms.FormWindowState
            Get
                Return CType(Me("Minimized"),Global.System.Windows.Forms.FormWindowState)
            End Get
            Set
                Me("Minimized") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Rx Report Print Preview")>  _
        Public Property Text() As String
            Get
                Return CType(Me("Text"),String)
            End Get
            Set
                Me("Text") = value
            End Set
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://dev110:26078/_vti_bin/Webs.asmx")>  _
        Public ReadOnly Property gloEMRAdmin_gloWeb_Webs() As String
            Get
                Return CType(Me("gloEMRAdmin_gloWeb_Webs"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("https://173.167.9.140:43261/_layouts/UserManagementService.asmx")>  _
        Public ReadOnly Property gloEMRAdmin_UserManagement_UserManagementService() As String
            Get
                Return CType(Me("gloEMRAdmin_UserManagement_UserManagementService"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://dev110:23869/_vti_bin/Authentication.asmx")>  _
        Public ReadOnly Property gloEMRAdmin_SPAuthentication_Authentication() As String
            Get
                Return CType(Me("gloEMRAdmin_SPAuthentication_Authentication"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://dev110:23869/_vti_bin/lists.asmx")>  _
        Public ReadOnly Property gloEMRAdmin_gloLists_Lists() As String
            Get
                Return CType(Me("gloEMRAdmin_gloLists_Lists"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://dev110:23869/_vti_bin/meetings.asmx")>  _
        Public ReadOnly Property gloEMRAdmin_MeetingService_Meetings() As String
            Get
                Return CType(Me("gloEMRAdmin_MeetingService_Meetings"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.ConnectionString),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Data Source=GLOSVR02\SQL2008R2;Initial Catalog=glo8010_DEV;User ID=sa")>  _
        Public ReadOnly Property glo8010_DEVConnectionString() As String
            Get
                Return CType(Me("glo8010_DEVConnectionString"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.ConnectionString),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Data Source=GLOSVR02\SQL2008R2;Initial Catalog=glo8010_DEV;Persist Security Info="& _ 
            "True;User ID=sa;Password=saglosvr02")>  _
        Public ReadOnly Property glo8010_DEVConnectionString1() As String
            Get
                Return CType(Me("glo8010_DEVConnectionString1"),String)
            End Get
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.gloEMRAdmin.My.MySettings
            Get
                Return Global.gloEMRAdmin.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace