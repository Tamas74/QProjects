Public Class ClsUserMappingField
    Implements IDisposable

    Private _gloEMRFieldName As String
    Private _sOtherFieldName As String
    Private _eventName As String
    Private _sgloEMRDisplayName As String
    Private _sDataType As String
    Private _sModuleName As String
    Private _sNodeTYpe As String
    Private _sReferenceNodePath As String
    Private _sReferenceNodeValue As String
    Private _sReferenceNode As String


    Public Sub New()

    End Sub
    Public Property NodeType()
        Get
            Return _sNodeTYpe
        End Get
        Set(ByVal value)
            _sNodeTYpe = value
        End Set
    End Property
    Public Property ReferenceNode()
        Get
            Return _sReferenceNode
        End Get
        Set(ByVal value)
            _sReferenceNode = value
        End Set
    End Property
    Public Property ReferenceNodePath()
        Get
            Return _sReferenceNodePath
        End Get
        Set(ByVal value)
            _sReferenceNodePath = value
        End Set
    End Property
    Public Property ReferenceNodeValue()
        Get
            Return _sReferenceNodeValue
        End Get
        Set(ByVal value)
            _sReferenceNodeValue = value
        End Set
    End Property
    Public Property gloEMRModuleName()
        Get
            Return _sModuleName
        End Get
        Set(ByVal value)
            _sModuleName = value
        End Set
    End Property

    Public Property gloEMRFieldName()
        Get
            Return _gloEMRFieldName
        End Get
        Set(ByVal value)
            _gloEMRFieldName = value
        End Set
    End Property
    Public Property gloEMRDisplayName()
        Get
            Return _sgloEMRDisplayName
        End Get
        Set(ByVal value)
            _sgloEMRDisplayName = value
        End Set
    End Property
    Public Property OtherFieldName()
        Get
            Return _sOtherFieldName
        End Get
        Set(ByVal value)
            _sOtherFieldName = value
        End Set
    End Property
    Public Property EventName()
        Get
            Return _eventName
        End Get
        Set(ByVal value)
            _eventName = value
        End Set
    End Property
    Public Property DataType()
        Get
            Return _sDataType
        End Get
        Set(ByVal value)
            _sDataType = value
        End Set
    End Property
    
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
