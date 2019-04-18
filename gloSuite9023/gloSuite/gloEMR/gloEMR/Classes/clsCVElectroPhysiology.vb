Public Class clsCVElectroPhysiology
    Implements IDisposable

    Private _nElectroPhysiologyID As Int64 = 0
    Private _nPatientID As Int64 = 0
    Private _nExamID As Int64 = 0
    Private _nVisitID As Int64 = 0
    Private _nClinicID As Int64 = 0
    Private _dtProcedureDate As Date
    Private _sCPTCode As String = ""
    Private _sProcedures As String = ""
    Private _sUserProvider As String = ""
    Public Sub New()
        _nElectroPhysiologyID = 0
        _nPatientID = 0
        _nExamID = 0
        _nVisitID = 0
        _nClinicID = 0
    End Sub
    Public Property ElectroPhysiologyID()
        Get
            Return _nElectroPhysiologyID
        End Get
        Set(ByVal value)
            _nElectroPhysiologyID = value
        End Set
    End Property
    Public Property PatientID()
        Get
            Return _nPatientID
        End Get
        Set(ByVal value)
            _nPatientID = value
        End Set
    End Property
    Public Property ExamID()
        Get
            Return _nExamID
        End Get
        Set(ByVal value)
            _nExamID = value
        End Set
    End Property
    Public Property VisitID()
        Get
            Return _nVisitID
        End Get
        Set(ByVal value)
            _nVisitID = value
        End Set
    End Property
    Public Property ClinicID()
        Get
            Return _nClinicID
        End Get
        Set(ByVal value)
            _nClinicID = value
        End Set
    End Property

    Public Property ProcedureDate()
        Get
            Return _dtProcedureDate
        End Get
        Set(ByVal value)
            _dtProcedureDate = value
        End Set
    End Property
    Public Property CPTCode()
        Get
            Return _sCPTCode
        End Get
        Set(ByVal value)
            _sCPTCode = value
        End Set
    End Property
    Public Property Procedures()
        Get
            Return _sProcedures
        End Get
        Set(ByVal value)
            _sProcedures = value
        End Set
    End Property
    Public Property UserProvider()
        Get
            Return _sUserProvider
        End Get
        Set(ByVal value)
            _sUserProvider = value
        End Set
    End Property

    Private disposedValue As Boolean = False        ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If
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

Public Class ClsCVElectroPhysiologyCollection
    Implements IDisposable

    Protected _innerlist As ArrayList

    Public Sub New()
        _innerlist = New ArrayList()
    End Sub

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

    Public ReadOnly Property Count() As Integer
        Get
            If Not IsNothing(_innerlist) Then
                Return _innerlist.Count
            Else
                Return 0
            End If

        End Get
    End Property

    Public Sub Add(ByVal item As clsCVElectroPhysiology)
        If Not IsNothing(_innerlist) Then
            _innerlist.Add(item)
        End If
    End Sub

    ''Public Function Add(ByVal SendingApplicationName As String)
    ''    Dim item As New ClsClients(SendingApplicationName)
    ''    _innerlist.Add(item)
    ''End Function
    ''Public Function Add(ByVal SendingApplicationName As String, ByVal SendingApplicationFacility As String, ByVal sAppField As String)
    ''    Dim item As New ClsClients(SendingApplicationName, SendingApplicationName, SendingApplicationFacility, sAppField)
    ''    _innerlist.Add(item)
    ''End Function

    Public Sub Insert(ByVal index As Integer, ByVal item As clsCVElectroPhysiology)
        _innerlist.Insert(index, item)
    End Sub

    Public Function Remove(ByVal item As clsCVElectroPhysiology) As Boolean
        Dim result As Boolean = False
        Dim obj As clsCVElectroPhysiology

        For i As Integer = 0 To _innerlist.Count - 1
            'store current index being checked 
            ' obj = New clsCVElectroPhysiology()
            obj = CType(_innerlist(i), clsCVElectroPhysiology)
            If obj.ElectroPhysiologyID = item.ElectroPhysiologyID AndAlso obj.PatientID = item.PatientID AndAlso obj.VisitID = item.VisitID Then
                _innerlist.RemoveAt(i)
                result = True
                Exit For
            End If
            obj = Nothing
        Next

        Return result
    End Function

    Public Function RemoveAt(ByVal index As Integer) As Boolean
        Dim result As Boolean = False
        _innerlist.RemoveAt(index)
        result = True
        Return result
    End Function

    Public Sub Clear()
        _innerlist.Clear()
    End Sub

    Default Public ReadOnly Property Item(ByVal index As Integer) As clsCVElectroPhysiology
        Get
            Return CType(_innerlist(index), clsCVElectroPhysiology)
        End Get
    End Property

    Public Function Contains(ByVal item As clsCVElectroPhysiology) As Boolean
        Return _innerlist.Contains(item)
    End Function

    Public Function IndexOf(ByVal item As clsCVElectroPhysiology) As Integer
        Return _innerlist.IndexOf(item)
    End Function

    Public Sub CopyTo(ByVal array As clsCVElectroPhysiology(), ByVal index As Integer)
        _innerlist.CopyTo(array, index)
    End Sub

End Class

