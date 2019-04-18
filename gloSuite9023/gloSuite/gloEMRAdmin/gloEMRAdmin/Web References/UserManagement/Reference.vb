﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.235
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.235.
'
Namespace UserManagement
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="UserManagementServiceSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class UserManagementService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private HelloWorldOperationCompleted As System.Threading.SendOrPostCallback
        
        Private CreateUserOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ChangePasswordOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ChangePasswordQuestionAndAnswerOperationCompleted As System.Threading.SendOrPostCallback
        
        Private UnlockUserOperationCompleted As System.Threading.SendOrPostCallback
        
        Private SPChangePasswordOperationCompleted As System.Threading.SendOrPostCallback
        
        Private CheckUserConnectionOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.gloEMRAdmin.My.MySettings.Default.gloEMRAdmin_UserManagement_UserManagementService
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event HelloWorldCompleted As HelloWorldCompletedEventHandler
        
        '''<remarks/>
        Public Event CreateUserCompleted As CreateUserCompletedEventHandler
        
        '''<remarks/>
        Public Event ChangePasswordCompleted As ChangePasswordCompletedEventHandler
        
        '''<remarks/>
        Public Event ChangePasswordQuestionAndAnswerCompleted As ChangePasswordQuestionAndAnswerCompletedEventHandler
        
        '''<remarks/>
        Public Event UnlockUserCompleted As UnlockUserCompletedEventHandler
        
        '''<remarks/>
        Public Event SPChangePasswordCompleted As SPChangePasswordCompletedEventHandler
        
        '''<remarks/>
        Public Event CheckUserConnectionCompleted As CheckUserConnectionCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function HelloWorld() As String
            Dim results() As Object = Me.Invoke("HelloWorld", New Object(-1) {})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub HelloWorldAsync()
            Me.HelloWorldAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub HelloWorldAsync(ByVal userState As Object)
            If (Me.HelloWorldOperationCompleted Is Nothing) Then
                Me.HelloWorldOperationCompleted = AddressOf Me.OnHelloWorldOperationCompleted
            End If
            Me.InvokeAsync("HelloWorld", New Object(-1) {}, Me.HelloWorldOperationCompleted, userState)
        End Sub
        
        Private Sub OnHelloWorldOperationCompleted(ByVal arg As Object)
            If (Not (Me.HelloWorldCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent HelloWorldCompleted(Me, New HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CreateUser", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function CreateUser(ByVal UserName As String, ByVal Password As String, ByVal Email As String, ByVal PasswordQuestion As String, ByVal PasswordAnswer As String, ByVal IsApproved As Boolean, ByVal ClinicName As String, ByVal ExternalCode As String) As String
            Dim results() As Object = Me.Invoke("CreateUser", New Object() {UserName, Password, Email, PasswordQuestion, PasswordAnswer, IsApproved, ClinicName, ExternalCode})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub CreateUserAsync(ByVal UserName As String, ByVal Password As String, ByVal Email As String, ByVal PasswordQuestion As String, ByVal PasswordAnswer As String, ByVal IsApproved As Boolean, ByVal ClinicName As String, ByVal ExternalCode As String)
            Me.CreateUserAsync(UserName, Password, Email, PasswordQuestion, PasswordAnswer, IsApproved, ClinicName, ExternalCode, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub CreateUserAsync(ByVal UserName As String, ByVal Password As String, ByVal Email As String, ByVal PasswordQuestion As String, ByVal PasswordAnswer As String, ByVal IsApproved As Boolean, ByVal ClinicName As String, ByVal ExternalCode As String, ByVal userState As Object)
            If (Me.CreateUserOperationCompleted Is Nothing) Then
                Me.CreateUserOperationCompleted = AddressOf Me.OnCreateUserOperationCompleted
            End If
            Me.InvokeAsync("CreateUser", New Object() {UserName, Password, Email, PasswordQuestion, PasswordAnswer, IsApproved, ClinicName, ExternalCode}, Me.CreateUserOperationCompleted, userState)
        End Sub
        
        Private Sub OnCreateUserOperationCompleted(ByVal arg As Object)
            If (Not (Me.CreateUserCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent CreateUserCompleted(Me, New CreateUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ChangePassword", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ChangePassword(ByVal UserName As String, ByVal OldPassword As String, ByVal NewPassword As String) As Boolean
            Dim results() As Object = Me.Invoke("ChangePassword", New Object() {UserName, OldPassword, NewPassword})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ChangePasswordAsync(ByVal UserName As String, ByVal OldPassword As String, ByVal NewPassword As String)
            Me.ChangePasswordAsync(UserName, OldPassword, NewPassword, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ChangePasswordAsync(ByVal UserName As String, ByVal OldPassword As String, ByVal NewPassword As String, ByVal userState As Object)
            If (Me.ChangePasswordOperationCompleted Is Nothing) Then
                Me.ChangePasswordOperationCompleted = AddressOf Me.OnChangePasswordOperationCompleted
            End If
            Me.InvokeAsync("ChangePassword", New Object() {UserName, OldPassword, NewPassword}, Me.ChangePasswordOperationCompleted, userState)
        End Sub
        
        Private Sub OnChangePasswordOperationCompleted(ByVal arg As Object)
            If (Not (Me.ChangePasswordCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ChangePasswordCompleted(Me, New ChangePasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ChangePasswordQuestionAndAnswer", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ChangePasswordQuestionAndAnswer(ByVal UserName As String, ByVal Password As String, ByVal NewPasswordQuestion As String, ByVal NewPasswordAnswer As String) As Boolean
            Dim results() As Object = Me.Invoke("ChangePasswordQuestionAndAnswer", New Object() {UserName, Password, NewPasswordQuestion, NewPasswordAnswer})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ChangePasswordQuestionAndAnswerAsync(ByVal UserName As String, ByVal Password As String, ByVal NewPasswordQuestion As String, ByVal NewPasswordAnswer As String)
            Me.ChangePasswordQuestionAndAnswerAsync(UserName, Password, NewPasswordQuestion, NewPasswordAnswer, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ChangePasswordQuestionAndAnswerAsync(ByVal UserName As String, ByVal Password As String, ByVal NewPasswordQuestion As String, ByVal NewPasswordAnswer As String, ByVal userState As Object)
            If (Me.ChangePasswordQuestionAndAnswerOperationCompleted Is Nothing) Then
                Me.ChangePasswordQuestionAndAnswerOperationCompleted = AddressOf Me.OnChangePasswordQuestionAndAnswerOperationCompleted
            End If
            Me.InvokeAsync("ChangePasswordQuestionAndAnswer", New Object() {UserName, Password, NewPasswordQuestion, NewPasswordAnswer}, Me.ChangePasswordQuestionAndAnswerOperationCompleted, userState)
        End Sub
        
        Private Sub OnChangePasswordQuestionAndAnswerOperationCompleted(ByVal arg As Object)
            If (Not (Me.ChangePasswordQuestionAndAnswerCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ChangePasswordQuestionAndAnswerCompleted(Me, New ChangePasswordQuestionAndAnswerCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UnlockUser", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function UnlockUser(ByVal UserName As String) As Boolean
            Dim results() As Object = Me.Invoke("UnlockUser", New Object() {UserName})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub UnlockUserAsync(ByVal UserName As String)
            Me.UnlockUserAsync(UserName, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub UnlockUserAsync(ByVal UserName As String, ByVal userState As Object)
            If (Me.UnlockUserOperationCompleted Is Nothing) Then
                Me.UnlockUserOperationCompleted = AddressOf Me.OnUnlockUserOperationCompleted
            End If
            Me.InvokeAsync("UnlockUser", New Object() {UserName}, Me.UnlockUserOperationCompleted, userState)
        End Sub
        
        Private Sub OnUnlockUserOperationCompleted(ByVal arg As Object)
            If (Not (Me.UnlockUserCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent UnlockUserCompleted(Me, New UnlockUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPChangePassword", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function SPChangePassword(ByVal UserName As String, ByVal OldPassword As String, ByVal NewPassword As String) As Boolean
            Dim results() As Object = Me.Invoke("SPChangePassword", New Object() {UserName, OldPassword, NewPassword})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub SPChangePasswordAsync(ByVal UserName As String, ByVal OldPassword As String, ByVal NewPassword As String)
            Me.SPChangePasswordAsync(UserName, OldPassword, NewPassword, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub SPChangePasswordAsync(ByVal UserName As String, ByVal OldPassword As String, ByVal NewPassword As String, ByVal userState As Object)
            If (Me.SPChangePasswordOperationCompleted Is Nothing) Then
                Me.SPChangePasswordOperationCompleted = AddressOf Me.OnSPChangePasswordOperationCompleted
            End If
            Me.InvokeAsync("SPChangePassword", New Object() {UserName, OldPassword, NewPassword}, Me.SPChangePasswordOperationCompleted, userState)
        End Sub
        
        Private Sub OnSPChangePasswordOperationCompleted(ByVal arg As Object)
            If (Not (Me.SPChangePasswordCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent SPChangePasswordCompleted(Me, New SPChangePasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CheckUserConnection", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function CheckUserConnection(ByVal UserName As String) As Boolean
            Dim results() As Object = Me.Invoke("CheckUserConnection", New Object() {UserName})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub CheckUserConnectionAsync(ByVal UserName As String)
            Me.CheckUserConnectionAsync(UserName, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub CheckUserConnectionAsync(ByVal UserName As String, ByVal userState As Object)
            If (Me.CheckUserConnectionOperationCompleted Is Nothing) Then
                Me.CheckUserConnectionOperationCompleted = AddressOf Me.OnCheckUserConnectionOperationCompleted
            End If
            Me.InvokeAsync("CheckUserConnection", New Object() {UserName}, Me.CheckUserConnectionOperationCompleted, userState)
        End Sub
        
        Private Sub OnCheckUserConnectionOperationCompleted(ByVal arg As Object)
            If (Not (Me.CheckUserConnectionCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent CheckUserConnectionCompleted(Me, New CheckUserConnectionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub HelloWorldCompletedEventHandler(ByVal sender As Object, ByVal e As HelloWorldCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class HelloWorldCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub CreateUserCompletedEventHandler(ByVal sender As Object, ByVal e As CreateUserCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class CreateUserCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub ChangePasswordCompletedEventHandler(ByVal sender As Object, ByVal e As ChangePasswordCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ChangePasswordCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub ChangePasswordQuestionAndAnswerCompletedEventHandler(ByVal sender As Object, ByVal e As ChangePasswordQuestionAndAnswerCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ChangePasswordQuestionAndAnswerCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub UnlockUserCompletedEventHandler(ByVal sender As Object, ByVal e As UnlockUserCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class UnlockUserCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub SPChangePasswordCompletedEventHandler(ByVal sender As Object, ByVal e As SPChangePasswordCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class SPChangePasswordCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub CheckUserConnectionCompletedEventHandler(ByVal sender As Object, ByVal e As CheckUserConnectionCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class CheckUserConnectionCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
    End Class
End Namespace