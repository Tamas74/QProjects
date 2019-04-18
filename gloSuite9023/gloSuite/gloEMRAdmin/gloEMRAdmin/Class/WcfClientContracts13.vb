Imports System.Net.Security
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.Xml
Imports Microsoft.IdentityModel.Protocols.WSTrust

Namespace ClientOmAuth
    <ServiceContract()> _
    Public Interface IWSTrust13Contract
        <OperationContract(ProtectionLevel:=ProtectionLevel.EncryptAndSign, Action:="http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue", ReplyAction:="http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal", AsyncPattern:=True)> _
        Function BeginIssue(ByVal request As System.ServiceModel.Channels.Message, ByVal callback As AsyncCallback, ByVal state As Object) As IAsyncResult
        Function EndIssue(ByVal asyncResult As IAsyncResult) As System.ServiceModel.Channels.Message
    End Interface

    Partial Public Class WSTrust13ContractClient
        Inherits ClientBase(Of IWSTrust13Contract)
        Implements IWSTrust13Contract
        Public Sub New(ByVal binding As Binding, ByVal remoteAddress As EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub

        Public Function BeginIssue(ByVal request As Message, ByVal callback As AsyncCallback, ByVal state As Object) As IAsyncResult Implements IWSTrust13Contract.BeginIssue
            Return MyBase.Channel.BeginIssue(request, callback, state)
        End Function

        Public Function EndIssue(ByVal asyncResult As IAsyncResult) As Message Implements IWSTrust13Contract.EndIssue
            Return MyBase.Channel.EndIssue(asyncResult)
        End Function
    End Class
End Namespace

