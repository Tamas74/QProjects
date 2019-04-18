Public Class clsCardiologyDevice
    Implements IDisposable

    Private _nCardiologyDeviceID = 0

    Private _nPatientID As Int64 = 0
    Private _nExamID As Int64 = 0
    Private _nVisitID As Int64 = 0
    Private _nClinicID As Int64 = 0


    Private _dtDateofImplant As String = ""

    Private _sDeviceType As String = ""
    Private _sProductName As String = ""
    Private _sDeviceManufacturer As String = ""
    Private _sProductSpecification As String = ""

    Private _sProductSerialNo As String = ""
    Private _sManufacturerModelNo As String = ""
    Private _sLeadType As String = ""    
    Private _dtDateRemoved As DateTime
    Private _sPhysicalLocation As String = ""
    Private mProcedure As String = ""
    Private mProceduredate As Date

    Private _sLeadLocation As String = ""
    Private _sThresholdAtrial As String = ""
    Private _sThresholdVentricular As String = ""
    Private _sSensingAtrial As String = ""
    Private _sSensingVentricular As String = ""
    Private _sImpedenceAtrial As String = ""
    Private _sImpedenceVentricular As String = ""
    Private _sCPT As String = ""
    Private _sProc As String = ""


    Public Sub New()
        _nCardiologyDeviceID = 0
        _nPatientID = 0
        _nExamID = 0
        _nVisitID = 0
        _nClinicID = 0
    End Sub
    Public Property CardiologyDeviceID() As Int64
        Get
            Return _nCardiologyDeviceID
        End Get
        Set(ByVal value As Int64)
            _nCardiologyDeviceID = value
        End Set
    End Property
    Public Property PatientID() As Int64
        Get
            Return _nPatientID
        End Get
        Set(ByVal value As Int64)
            _nPatientID = value
        End Set
    End Property
    Public Property ExamID() As Int64
        Get
            Return _nExamID
        End Get
        Set(ByVal value As Int64)
            _nExamID = value
        End Set
    End Property
    Public Property VisitID() As Int64
        Get
            Return _nVisitID
        End Get
        Set(ByVal value As Int64)
            _nVisitID = value
        End Set
    End Property
    Public Property ClinicID() As Int64
        Get
            Return _nClinicID
        End Get
        Set(ByVal value As Int64)
            _nClinicID = value
        End Set
    End Property
    Public Property ProcedureDate() As DateTime
        Get
            Return mProceduredate
        End Get
        Set(ByVal value As DateTime)
            mProceduredate = value
        End Set
    End Property
    Public Property DateofImplant() As DateTime
        Get
            Return _dtDateofImplant
        End Get
        Set(ByVal value As DateTime)
            _dtDateofImplant = value
        End Set
    End Property
    Public Property DeviceType() As String
        Get
            Return _sDeviceType
        End Get
        Set(ByVal value As String)
            _sDeviceType = value
        End Set
    End Property
    Public Property ProductName() As String
        Get
            Return _sProductName
        End Get
        Set(ByVal value As String)
            _sProductName = value
        End Set
    End Property

    Public Property DeviceManufacturer() As String
        Get
            Return _sDeviceManufacturer
        End Get
        Set(ByVal value As String)
            _sDeviceManufacturer = value
        End Set
    End Property
    Public Property ProductSpecification() As String
        Get
            Return _sProductSpecification
        End Get
        Set(ByVal value As String)
            _sProductSpecification = value
        End Set
    End Property
    Public Property ProductSerialNo() As String
        Get
            Return _sProductSerialNo
        End Get
        Set(ByVal value As String)
            _sProductSerialNo = value
        End Set
    End Property
    Public Property ManufacturerModelNo() As String
        Get
            Return _sManufacturerModelNo
        End Get
        Set(ByVal value As String)
            _sManufacturerModelNo = value
        End Set
    End Property
    Public Property LeadType() As String
        Get
            Return _sLeadType
        End Get
        Set(ByVal value As String)
            _sLeadType = value
        End Set
    End Property
   
    Public Property DateRemoved() As DateTime
        Get
            Return _dtDateRemoved
        End Get
        Set(ByVal value As DateTime)
            _dtDateRemoved = value
        End Set
    End Property
    Public Property PhysicalLocation() As String
        Get
            Return _sPhysicalLocation
        End Get
        Set(ByVal value As String)
            _sPhysicalLocation = value
        End Set
    End Property
    Public Property Procedures() As String
        Get
            Return mProcedure
        End Get
        Set(ByVal value As String)
            mProcedure = value
        End Set
    End Property

    Public Property sCPT() As String
        Get
            Return _sCPT
        End Get
        Set(ByVal value As String)
            _sCPT = value
        End Set
    End Property

    Public Property sProc() As String
        Get
            Return _sProc
        End Get
        Set(ByVal value As String)
            _sProc = value
        End Set
    End Property

    Public Property sLeadLocation() As String
        Get
            Return _sLeadLocation
        End Get
        Set(ByVal value As String)
            _sLeadLocation = value
        End Set
    End Property

    Public Property sThresholdAtrial() As String
        Get
            Return _sThresholdAtrial
        End Get
        Set(ByVal value As String)
            _sThresholdAtrial = value
        End Set
    End Property

    Public Property sThresholdVentricular() As String
        Get
            Return _sThresholdVentricular
        End Get
        Set(ByVal value As String)
            _sThresholdVentricular = value
        End Set
    End Property

    Public Property sSensingAtrial() As String
        Get
            Return _sSensingAtrial
        End Get
        Set(ByVal value As String)
            _sSensingAtrial = value
        End Set
    End Property

    Public Property sSensingVentricular() As String
        Get
            Return _sSensingVentricular
        End Get
        Set(ByVal value As String)
            _sSensingVentricular = value
        End Set
    End Property

    Public Property sImpedenceAtrial() As String
        Get
            Return _sImpedenceAtrial
        End Get
        Set(ByVal value As String)
            _sImpedenceAtrial = value
        End Set
    End Property

    Public Property sImpedenceVentricular() As String
        Get
            Return _sImpedenceVentricular
        End Get
        Set(ByVal value As String)
            _sImpedenceVentricular = value
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


Public Class ClsCardioDeviceCollection
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

    Public Sub Add(ByVal item As clsCardiologyDevice)
        If Not IsNothing(_innerlist) Then
            _innerlist.Add(item)
        End If
    End Sub

    'Public Function Add(ByVal SendingApplicationName As String)
    '    Dim item As New ClsClients(SendingApplicationName)
    '    _innerlist.Add(item)
    'End Function

    'Public Function Add(ByVal SendingApplicationName As String, ByVal SendingApplicationFacility As String, ByVal sAppField As String)
    '    Dim item As New ClsClients(SendingApplicationName, SendingApplicationName, SendingApplicationFacility, sAppField)
    '    _innerlist.Add(item)
    'End Function

    Public Sub Insert(ByVal index As Integer, ByVal item As clsCardiologyDevice)
        _innerlist.Insert(index, item)
    End Sub

    Public Function Remove(ByVal item As clsCardiologyDevice) As Boolean
        Dim result As Boolean = False
        Dim obj As clsCardiologyDevice

        For i As Integer = 0 To _innerlist.Count - 1
            'store current index being checked 
            ' obj = New clsCardiologyDevice()
            obj = CType(_innerlist(i), clsCardiologyDevice)
            If obj.CardiologyDeviceID = item.CardiologyDeviceID AndAlso obj.PatientID = item.PatientID AndAlso obj.VisitID = item.VisitID Then
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

    Default Public ReadOnly Property Item(ByVal index As Integer) As clsCardiologyDevice
        Get
            Return CType(_innerlist(index), clsCardiologyDevice)
        End Get
    End Property

    Public Function Contains(ByVal item As clsCardiologyDevice) As Boolean
        Return _innerlist.Contains(item)
    End Function

    Public Function IndexOf(ByVal item As clsCardiologyDevice) As Integer
        Return _innerlist.IndexOf(item)
    End Function

    Public Sub CopyTo(ByVal array As clsCardiologyDevice(), ByVal index As Integer)
        _innerlist.CopyTo(array, index)
    End Sub

End Class
