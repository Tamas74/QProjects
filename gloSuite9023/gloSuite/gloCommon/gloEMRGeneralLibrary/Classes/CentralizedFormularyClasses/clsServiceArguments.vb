Imports System.Runtime.Serialization

Public Interface IPBMSenderID
    <DataMember()>
    Property PBMSenderID As String
End Interface

Public Interface ICopayID
    <DataMember()>
    Property CopayID As String
End Interface

<DataContract()>
Public Class Argument_RxHGetNLFormularyStatus
    Implements IDisposable


    <DataMember()>
    Public Property SenderID As String

    <DataMember()>
    Public Property FormularyID As String

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                Me.SenderID = String.Empty
                Me.FormularyID = String.Empty

            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

<DataContract()>
Public Class Argument
    Implements IPBMSenderID, ICopayID, IDisposable


    <DataMember()>
    Property PBMSenderID As String Implements IPBMSenderID.PBMSenderID

    <DataMember()>
    Public Property CopayID As String Implements ICopayID.CopayID

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                Me.PBMSenderID = String.Empty
                Me.CopayID = String.Empty

            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

<DataContract()>
Public Class ArgumentFormularyStatus
    Inherits Argument

    <DataMember()>
    Property FormularyStatus As String

End Class

<DataContract()>
Public Class Argument_GetPayerAlternative
    Implements IPBMSenderID

    <DataMember()>
    Public Property PBMSenderID As String Implements IPBMSenderID.PBMSenderID

    <DataMember()>
    Public Property FormularyID As String

    <DataMember()>
    Public Property AlternativeID As String

    <DataMember()>
    Public Property DDID As String

End Class

<DataContract()>
Public Class Argument_RxHGetCoverageResourceLinkInfo
    Implements IPBMSenderID

    <DataMember()>
    Public Property PBMSenderID As String Implements IPBMSenderID.PBMSenderID

    <DataMember()>
    Public Property CoverageID As String

    <DataMember()>
    Public Property DDID As String

End Class

<DataContract()>
Public Class Argument_RxHGetCopayInfo
    Inherits Argument

    <DataMember()>
    Public Property DDID As String

End Class

<DataContract()>
Public Class Argument_RxHGetCopayInfo_SL
    Inherits Argument

    <DataMember()>
    Public Property DrugFormularyStatus As String

End Class