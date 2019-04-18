Imports System.Net.Security
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.Xml
Imports Microsoft.IdentityModel.Protocols.WSTrust

Namespace ClientOmAuth
    Public Class RequestBodyWriter
        Inherits BodyWriter
        Private _serializer As WSTrustRequestSerializer
        Private _rst As RequestSecurityToken

        ''' <summary>
        ''' Constructs the Body Writer.
        ''' </summary>
        ''' <param name="serializer">Serializer to use for serializing the rst.</param>
        ''' <param name="rst">The RequestSecurityToken object to be serialized to the outgoing Message.</param>
        Public Sub New(serializer As WSTrustRequestSerializer, rst As RequestSecurityToken)
            MyBase.New(False)
            If serializer Is Nothing Then
                Throw New ArgumentNullException("serializer")
            End If

            Me._serializer = serializer
            Me._rst = rst
        End Sub


        ''' <summary>
        ''' Override of the base class method. Serializes the rst to the outgoing stream.
        ''' </summary>
        ''' <param name="writer">Writer to which the rst should be written.</param>
        Protected Overrides Sub OnWriteBodyContents(writer As XmlDictionaryWriter)
            _serializer.WriteXml(_rst, writer, New WSTrustSerializationContext())
        End Sub
    End Class
End Namespace
