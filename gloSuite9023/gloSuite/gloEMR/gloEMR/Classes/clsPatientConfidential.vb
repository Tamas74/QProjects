Public Class clsPatientConfidential

    Private _nConfidentialID As Int64
    Private _nPatientID As Int64 = 0
    Private _sDescription As String = ""

    Public Property ConfidentialID() As Int64
        Get
            Return _nConfidentialID
        End Get
        Set(ByVal Value As Int64)
            _nConfidentialID = Value
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
    Public Property Description() As String
        Get
            Return _sDescription
        End Get
        Set(ByVal value As String)
            _sDescription = value
        End Set
    End Property
    Public Sub New()
        _nConfidentialID = 0
        _nPatientID = 0
        _sDescription = ""
    End Sub

    
End Class
