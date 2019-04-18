Public Class clsEjectionFraction

    Dim _EjectionFractionID As Long = 0
    Dim _PatientID As Long
    Dim _ExamID As Long
    Dim _VisitID As Long
    Dim _ClinicID As Long
    Dim _TestDate As Date
    Dim _ModilityTest As String
    Dim _QuantityPercent As String
    Dim _QuantityDesc As String
    Dim mRowIndex As Int32
    Public Property RowIndex() As Int32
        Get
            Return mRowIndex
        End Get
        Set(ByVal value As Int32)
            mRowIndex = value
        End Set
    End Property
    Public Property EjectionFractionID() As Long
        Get
            Return _EjectionFractionID
        End Get
        Set(ByVal value As Long)
            _EjectionFractionID = value
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
   
    Public Property TestDate() As Date
        Get
            Return _TestDate
        End Get
        Set(ByVal value As Date)
            _TestDate = value
        End Set
    End Property

    Public Property ModalityTest() As String
        Get
            Return _ModilityTest
        End Get
        Set(ByVal value As String)
            _ModilityTest = value
        End Set
    End Property


    Public Property QuantityPercent() As String
        Get
            Return _QuantityPercent
        End Get
        Set(ByVal value As String)
            _QuantityPercent = value
        End Set
    End Property
    Public Property QuantityDescription() As String
        Get
            Return _QuantityDesc
        End Get
        Set(ByVal value As String)
            _QuantityDesc = value
        End Set
    End Property

End Class
