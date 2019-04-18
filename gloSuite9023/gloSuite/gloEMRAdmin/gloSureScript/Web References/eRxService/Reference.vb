﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.269
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
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.269.
'
Namespace eRxService
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="eRxMessageSoap", [Namespace]:="https://ophit.net/")>  _
    Partial Public Class eRxMessage
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private PostClientRxMessageOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetXMLDocumentOperationCompleted As System.Threading.SendOrPostCallback
        
        Private LoginOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetResponsesOperationCompleted As System.Threading.SendOrPostCallback
        
        Private UpdateDownloadStatusOperationCompleted As System.Threading.SendOrPostCallback
        
        Private RetrieveSurescriptRxMessagesOperationCompleted As System.Threading.SendOrPostCallback
        
        Private DirectoryDownloadOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ConvertToBytesOperationCompleted As System.Threading.SendOrPostCallback
        
        Private DirectoryDownloadLocalOperationCompleted As System.Threading.SendOrPostCallback
        
        Private getLoginCredentialsOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.gloSureScript.My.MySettings.Default.gloSureScript_eRxService_eRxMessage
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
        Public Event PostClientRxMessageCompleted As PostClientRxMessageCompletedEventHandler
        
        '''<remarks/>
        Public Event GetXMLDocumentCompleted As GetXMLDocumentCompletedEventHandler
        
        '''<remarks/>
        Public Event LoginCompleted As LoginCompletedEventHandler
        
        '''<remarks/>
        Public Event GetResponsesCompleted As GetResponsesCompletedEventHandler
        
        '''<remarks/>
        Public Event UpdateDownloadStatusCompleted As UpdateDownloadStatusCompletedEventHandler
        
        '''<remarks/>
        Public Event RetrieveSurescriptRxMessagesCompleted As RetrieveSurescriptRxMessagesCompletedEventHandler
        
        '''<remarks/>
        Public Event DirectoryDownloadCompleted As DirectoryDownloadCompletedEventHandler
        
        '''<remarks/>
        Public Event ConvertToBytesCompleted As ConvertToBytesCompletedEventHandler
        
        '''<remarks/>
        Public Event DirectoryDownloadLocalCompleted As DirectoryDownloadLocalCompletedEventHandler
        
        '''<remarks/>
        Public Event getLoginCredentialsCompleted As getLoginCredentialsCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ophit.net/PostClientRxMessage", RequestNamespace:="https://ophit.net/", ResponseNamespace:="https://ophit.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function PostClientRxMessage(ByVal cntFromDB As Object, ByVal _key As String, ByVal _MsgType As String) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("PostClientRxMessage", New Object() {cntFromDB, _key, _MsgType})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub PostClientRxMessageAsync(ByVal cntFromDB As Object, ByVal _key As String, ByVal _MsgType As String)
            Me.PostClientRxMessageAsync(cntFromDB, _key, _MsgType, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub PostClientRxMessageAsync(ByVal cntFromDB As Object, ByVal _key As String, ByVal _MsgType As String, ByVal userState As Object)
            If (Me.PostClientRxMessageOperationCompleted Is Nothing) Then
                Me.PostClientRxMessageOperationCompleted = AddressOf Me.OnPostClientRxMessageOperationCompleted
            End If
            Me.InvokeAsync("PostClientRxMessage", New Object() {cntFromDB, _key, _MsgType}, Me.PostClientRxMessageOperationCompleted, userState)
        End Sub
        
        Private Sub OnPostClientRxMessageOperationCompleted(ByVal arg As Object)
            If (Not (Me.PostClientRxMessageCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent PostClientRxMessageCompleted(Me, New PostClientRxMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ophit.net/GetXMLDocument", RequestNamespace:="https://ophit.net/", ResponseNamespace:="https://ophit.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetXMLDocument(ByVal inURL As String) As String
            Dim results() As Object = Me.Invoke("GetXMLDocument", New Object() {inURL})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetXMLDocumentAsync(ByVal inURL As String)
            Me.GetXMLDocumentAsync(inURL, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetXMLDocumentAsync(ByVal inURL As String, ByVal userState As Object)
            If (Me.GetXMLDocumentOperationCompleted Is Nothing) Then
                Me.GetXMLDocumentOperationCompleted = AddressOf Me.OnGetXMLDocumentOperationCompleted
            End If
            Me.InvokeAsync("GetXMLDocument", New Object() {inURL}, Me.GetXMLDocumentOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetXMLDocumentOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetXMLDocumentCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetXMLDocumentCompleted(Me, New GetXMLDocumentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ophit.net/Login", RequestNamespace:="https://ophit.net/", ResponseNamespace:="https://ophit.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Login(ByVal userid As String, ByVal password As String) As String
            Dim results() As Object = Me.Invoke("Login", New Object() {userid, password})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub LoginAsync(ByVal userid As String, ByVal password As String)
            Me.LoginAsync(userid, password, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub LoginAsync(ByVal userid As String, ByVal password As String, ByVal userState As Object)
            If (Me.LoginOperationCompleted Is Nothing) Then
                Me.LoginOperationCompleted = AddressOf Me.OnLoginOperationCompleted
            End If
            Me.InvokeAsync("Login", New Object() {userid, password}, Me.LoginOperationCompleted, userState)
        End Sub
        
        Private Sub OnLoginOperationCompleted(ByVal arg As Object)
            If (Not (Me.LoginCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent LoginCompleted(Me, New LoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ophit.net/GetResponses", RequestNamespace:="https://ophit.net/", ResponseNamespace:="https://ophit.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetResponses(ByVal strPrescribers As String, ByVal strMessageType As String, ByVal _key As String) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("GetResponses", New Object() {strPrescribers, strMessageType, _key})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetResponsesAsync(ByVal strPrescribers As String, ByVal strMessageType As String, ByVal _key As String)
            Me.GetResponsesAsync(strPrescribers, strMessageType, _key, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetResponsesAsync(ByVal strPrescribers As String, ByVal strMessageType As String, ByVal _key As String, ByVal userState As Object)
            If (Me.GetResponsesOperationCompleted Is Nothing) Then
                Me.GetResponsesOperationCompleted = AddressOf Me.OnGetResponsesOperationCompleted
            End If
            Me.InvokeAsync("GetResponses", New Object() {strPrescribers, strMessageType, _key}, Me.GetResponsesOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetResponsesOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetResponsesCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetResponsesCompleted(Me, New GetResponsesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ophit.net/UpdateDownloadStatus", RequestNamespace:="https://ophit.net/", ResponseNamespace:="https://ophit.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Sub UpdateDownloadStatus(ByVal Transactions As String, ByVal _key As String)
            Me.Invoke("UpdateDownloadStatus", New Object() {Transactions, _key})
        End Sub
        
        '''<remarks/>
        Public Overloads Sub UpdateDownloadStatusAsync(ByVal Transactions As String, ByVal _key As String)
            Me.UpdateDownloadStatusAsync(Transactions, _key, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub UpdateDownloadStatusAsync(ByVal Transactions As String, ByVal _key As String, ByVal userState As Object)
            If (Me.UpdateDownloadStatusOperationCompleted Is Nothing) Then
                Me.UpdateDownloadStatusOperationCompleted = AddressOf Me.OnUpdateDownloadStatusOperationCompleted
            End If
            Me.InvokeAsync("UpdateDownloadStatus", New Object() {Transactions, _key}, Me.UpdateDownloadStatusOperationCompleted, userState)
        End Sub
        
        Private Sub OnUpdateDownloadStatusOperationCompleted(ByVal arg As Object)
            If (Not (Me.UpdateDownloadStatusCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent UpdateDownloadStatusCompleted(Me, New System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ophit.net/RetrieveSurescriptRxMessages", RequestNamespace:="https://ophit.net/", ResponseNamespace:="https://ophit.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Sub RetrieveSurescriptRxMessages()
            Me.Invoke("RetrieveSurescriptRxMessages", New Object(-1) {})
        End Sub
        
        '''<remarks/>
        Public Overloads Sub RetrieveSurescriptRxMessagesAsync()
            Me.RetrieveSurescriptRxMessagesAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub RetrieveSurescriptRxMessagesAsync(ByVal userState As Object)
            If (Me.RetrieveSurescriptRxMessagesOperationCompleted Is Nothing) Then
                Me.RetrieveSurescriptRxMessagesOperationCompleted = AddressOf Me.OnRetrieveSurescriptRxMessagesOperationCompleted
            End If
            Me.InvokeAsync("RetrieveSurescriptRxMessages", New Object(-1) {}, Me.RetrieveSurescriptRxMessagesOperationCompleted, userState)
        End Sub
        
        Private Sub OnRetrieveSurescriptRxMessagesOperationCompleted(ByVal arg As Object)
            If (Not (Me.RetrieveSurescriptRxMessagesCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent RetrieveSurescriptRxMessagesCompleted(Me, New System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ophit.net/DirectoryDownload", RequestNamespace:="https://ophit.net/", ResponseNamespace:="https://ophit.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function DirectoryDownload(ByVal eType As String) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("DirectoryDownload", New Object() {eType})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub DirectoryDownloadAsync(ByVal eType As String)
            Me.DirectoryDownloadAsync(eType, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub DirectoryDownloadAsync(ByVal eType As String, ByVal userState As Object)
            If (Me.DirectoryDownloadOperationCompleted Is Nothing) Then
                Me.DirectoryDownloadOperationCompleted = AddressOf Me.OnDirectoryDownloadOperationCompleted
            End If
            Me.InvokeAsync("DirectoryDownload", New Object() {eType}, Me.DirectoryDownloadOperationCompleted, userState)
        End Sub
        
        Private Sub OnDirectoryDownloadOperationCompleted(ByVal arg As Object)
            If (Not (Me.DirectoryDownloadCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent DirectoryDownloadCompleted(Me, New DirectoryDownloadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ophit.net/ConvertToBytes", RequestNamespace:="https://ophit.net/", ResponseNamespace:="https://ophit.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ConvertToBytes(ByVal strDownlaod As String) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("ConvertToBytes", New Object() {strDownlaod})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub ConvertToBytesAsync(ByVal strDownlaod As String)
            Me.ConvertToBytesAsync(strDownlaod, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ConvertToBytesAsync(ByVal strDownlaod As String, ByVal userState As Object)
            If (Me.ConvertToBytesOperationCompleted Is Nothing) Then
                Me.ConvertToBytesOperationCompleted = AddressOf Me.OnConvertToBytesOperationCompleted
            End If
            Me.InvokeAsync("ConvertToBytes", New Object() {strDownlaod}, Me.ConvertToBytesOperationCompleted, userState)
        End Sub
        
        Private Sub OnConvertToBytesOperationCompleted(ByVal arg As Object)
            If (Not (Me.ConvertToBytesCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ConvertToBytesCompleted(Me, New ConvertToBytesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ophit.net/DirectoryDownloadLocal", RequestNamespace:="https://ophit.net/", ResponseNamespace:="https://ophit.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function DirectoryDownloadLocal(ByVal eType As String) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("DirectoryDownloadLocal", New Object() {eType})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub DirectoryDownloadLocalAsync(ByVal eType As String)
            Me.DirectoryDownloadLocalAsync(eType, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub DirectoryDownloadLocalAsync(ByVal eType As String, ByVal userState As Object)
            If (Me.DirectoryDownloadLocalOperationCompleted Is Nothing) Then
                Me.DirectoryDownloadLocalOperationCompleted = AddressOf Me.OnDirectoryDownloadLocalOperationCompleted
            End If
            Me.InvokeAsync("DirectoryDownloadLocal", New Object() {eType}, Me.DirectoryDownloadLocalOperationCompleted, userState)
        End Sub
        
        Private Sub OnDirectoryDownloadLocalOperationCompleted(ByVal arg As Object)
            If (Not (Me.DirectoryDownloadLocalCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent DirectoryDownloadLocalCompleted(Me, New DirectoryDownloadLocalCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ophit.net/getLoginCredentials", RequestNamespace:="https://ophit.net/", ResponseNamespace:="https://ophit.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function getLoginCredentials() As String
            Dim results() As Object = Me.Invoke("getLoginCredentials", New Object(-1) {})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub getLoginCredentialsAsync()
            Me.getLoginCredentialsAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub getLoginCredentialsAsync(ByVal userState As Object)
            If (Me.getLoginCredentialsOperationCompleted Is Nothing) Then
                Me.getLoginCredentialsOperationCompleted = AddressOf Me.OngetLoginCredentialsOperationCompleted
            End If
            Me.InvokeAsync("getLoginCredentials", New Object(-1) {}, Me.getLoginCredentialsOperationCompleted, userState)
        End Sub
        
        Private Sub OngetLoginCredentialsOperationCompleted(ByVal arg As Object)
            If (Not (Me.getLoginCredentialsCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent getLoginCredentialsCompleted(Me, New getLoginCredentialsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
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
    Public Delegate Sub PostClientRxMessageCompletedEventHandler(ByVal sender As Object, ByVal e As PostClientRxMessageCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class PostClientRxMessageCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub GetXMLDocumentCompletedEventHandler(ByVal sender As Object, ByVal e As GetXMLDocumentCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetXMLDocumentCompletedEventArgs
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
    Public Delegate Sub LoginCompletedEventHandler(ByVal sender As Object, ByVal e As LoginCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class LoginCompletedEventArgs
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
    Public Delegate Sub GetResponsesCompletedEventHandler(ByVal sender As Object, ByVal e As GetResponsesCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetResponsesCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub UpdateDownloadStatusCompletedEventHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub RetrieveSurescriptRxMessagesCompletedEventHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub DirectoryDownloadCompletedEventHandler(ByVal sender As Object, ByVal e As DirectoryDownloadCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class DirectoryDownloadCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub ConvertToBytesCompletedEventHandler(ByVal sender As Object, ByVal e As ConvertToBytesCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ConvertToBytesCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub DirectoryDownloadLocalCompletedEventHandler(ByVal sender As Object, ByVal e As DirectoryDownloadLocalCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class DirectoryDownloadLocalCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub getLoginCredentialsCompletedEventHandler(ByVal sender As Object, ByVal e As getLoginCredentialsCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class getLoginCredentialsCompletedEventArgs
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
End Namespace
