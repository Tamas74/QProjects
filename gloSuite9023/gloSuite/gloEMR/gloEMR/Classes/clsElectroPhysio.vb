Public Class clsElectroPhysio

    Dim _ElectroPhysiologyID As Long = 0
    Dim _PatientID As Long = 0
    Dim _ExamID As Long = 0
    Dim _VisitID As Long = 0
    Dim _ClinicID As Long = 0
    Dim _dtProcedureDate As Date
    Dim _CPTCode As String
    Dim _Procedures As String
    Dim _UserProvider As String



    Public Property ElectroPhysiologyID() As Long
        Get
            Return _ElectroPhysiologyID
        End Get
        Set(ByVal value As Long)
            _ElectroPhysiologyID = value
        End Set
    End Property

    Public Property PatientID() As Long
        Get
            Return _PatientID
        End Get
        Set(ByVal value As Long)
            _PatientID = value
        End Set
    End Property


    Public Property ExamID() As Long
        Get
            Return _ExamID
        End Get
        Set(ByVal value As Long)
            _ExamID = value
        End Set
    End Property

    Public Property VisitID() As Long
        Get
            Return _VisitID
        End Get
        Set(ByVal value As Long)
            _VisitID = value
        End Set
    End Property

    Public Property ClinicID() As Long
        Get
            Return _ClinicID
        End Get
        Set(ByVal value As Long)
            _ClinicID = value
        End Set
    End Property

    Public Property dtProcedureDate() As Date
        Get
            Return _dtProcedureDate
        End Get
        Set(ByVal value As Date)
            _dtProcedureDate = value
        End Set
    End Property

    Public Property CPTCode() As String
        Get
            Return _CPTCode
        End Get
        Set(ByVal value As String)
            _CPTCode = value
        End Set
    End Property

    Public Property Procedures() As String
        Get
            Return _Procedures
        End Get
        Set(ByVal value As String)
            _Procedures = value
        End Set
    End Property

    Public Property UserProvider() As String
        Get
            Return _UserProvider
        End Get
        Set(ByVal value As String)
            _UserProvider = value
        End Set
    End Property

End Class
