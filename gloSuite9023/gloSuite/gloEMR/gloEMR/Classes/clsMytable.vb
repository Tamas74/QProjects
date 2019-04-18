Public Class mytable

    Implements IDisposable

    Private itemDescription As String
    Private itemCode As String
    Private snomeddesc As String
    Private snomedcode As String
    Private m_Modcode As String
    Private m_unit As Int64
    Private _nICDRevision As Int16
    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
    Private m_blnIsSnoMedOneToOneMapping As Boolean

    Sub New()
        MyBase.new()
    End Sub

    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
    Sub New(ByVal strDescription As String, ByVal strcode As String, ByVal strSnomedDesc As String, ByVal strSnomedCode As String, ByVal nICDRevision As Int16, ByVal blnIsSnoMedOneToOneMapping As Boolean)
        MyBase.new()
        itemCode = strcode
        itemDescription = strDescription
        snomeddesc = strSnomedDesc
        snomedcode = strSnomedCode
        _nICDRevision = nICDRevision
        m_blnIsSnoMedOneToOneMapping = blnIsSnoMedOneToOneMapping
    End Sub

    'Sub New(ByVal strDescription As String, ByVal strcode As String, ByVal strSnomedDesc As String, ByVal strSnomedCode As String, ByVal nICDRevision As Int16, ByVal blnIsSnoMedOneToOneMapping As Boolean)
    '    MyBase.new()
    '    itemCode = strcode
    '    itemDescription = strDescription
    '    snomeddesc = strSnomedDesc
    '    snomedcode = strSnomedCode
    '    _nICDRevision = nICDRevision
    'End Sub

    Sub New(ByVal strDescription As String, ByVal strcode As String, ByVal ICDRevision As Int16)
        MyBase.new()
        itemCode = strcode
        itemDescription = strDescription
        nICDRevision = ICDRevision
        'snomeddesc = strSnomedDesc
        'snomedcode = strSnomedCode
    End Sub
    Sub New(ByVal strDescription As String, ByVal strcode As String)
        MyBase.new()
        itemCode = strcode
        itemDescription = strDescription
        'snomeddesc = strSnomedDesc
        'snomedcode = strSnomedCode
    End Sub
    Sub New(ByVal strcode As String, ByVal strModCode As String, ByVal strDescription As String, ByVal intunit As Int64)
        MyBase.new()
        itemCode = strcode
        itemDescription = strDescription
        m_unit = intunit
        m_Modcode = strModCode
    End Sub
    Sub New(ByVal strcode As String, ByVal intunit As Int64)
        MyBase.new()
        itemCode = strcode
        m_unit = intunit
    End Sub
    Sub New(ByVal strcode As String, ByVal strDescription As String, ByVal strModCode As String)
        MyBase.new()
        itemCode = strcode
        itemDescription = strDescription
        m_Modcode = strModCode
    End Sub
    Public Property Description() As String
        Get
            Return itemDescription
        End Get
        Set(ByVal Value As String)
            itemDescription = Value
        End Set
    End Property

    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
    Public Property blnIsSnoMedOneToOneMapping() As Boolean
        Get
            Return m_blnIsSnoMedOneToOneMapping
        End Get
        Set(ByVal Value As Boolean)
            m_blnIsSnoMedOneToOneMapping = Value
        End Set
    End Property

    Public Property nICDRevision() As Int16
        Get
            Return _nICDRevision
        End Get
        Set(ByVal Value As Int16)
            _nICDRevision = Value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return itemCode
        End Get
        Set(ByVal Value As String)
            itemCode = Value
        End Set
    End Property
    Public Property snomeddescription() As String
        Get
            Return snomeddesc
        End Get
        Set(ByVal Value As String)
            snomeddesc = Value
        End Set
    End Property
    Public Property SnoCode() As String
        Get
            Return snomedcode
        End Get
        Set(ByVal Value As String)
            snomedcode = Value
        End Set
    End Property
    Public Property ModCode() As String
        Get
            Return m_Modcode
        End Get
        Set(ByVal Value As String)
            m_Modcode = Value
        End Set
    End Property
    Public Property Unit() As Int64
        Get
            Return m_unit
        End Get
        Set(ByVal Value As Int64)
            m_unit = Value
        End Set
    End Property
    Public Overrides Function Tostring() As String
        Return itemDescription
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If


            itemDescription = Nothing
            itemCode = Nothing
            m_Modcode = Nothing

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
