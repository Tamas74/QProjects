Public Class clsstresstest
    Dim _StressID As Int64 = 0
    Dim _PatientID As Int64 = 0
    Dim _VisitID As Int64 = 0
    Dim _ClinicID As Int64 = 0
    Dim _DateofStudy As Date
    Dim _TestType As String
    Dim _Procedure As String
    Dim _Result As String
    Dim _UserName As String
    Dim _examid As Int64 = 0
    'Dim struser As String


    Public Property Examid() As Int64
        Get
            Return _examid
        End Get
        Set(ByVal value As Int64)
            _examid = value
        End Set
    End Property
    Public Property StressID() As Int64
        Get
            Return _StressID
        End Get
        Set(ByVal value As Int64)
            _StressID = value
        End Set
    End Property
    Public Property PatientID() As Int64
        Get
            Return _PatientID
        End Get
        Set(ByVal value As Int64)
            _PatientID = value
        End Set
    End Property

    Public Property VisitID() As Int64
        Get
            Return _VisitID
        End Get
        Set(ByVal value As Int64)
            _VisitID = value
        End Set
    End Property

    Public Property ClinicID() As Int64
        Get
            Return _ClinicID
        End Get
        Set(ByVal value As Int64)
            _ClinicID = value
        End Set
    End Property

    Public Property DateofStudy() As Date
        Get
            Return _DateofStudy
        End Get
        Set(ByVal value As Date)
            _DateofStudy = value
        End Set
    End Property

    Public Property TestType() As String
        Get
            Return _TestType
        End Get
        Set(ByVal value As String)
            _TestType = value
        End Set
    End Property

    Public Property procedure() As String
        Get
            Return _Procedure
        End Get
        Set(ByVal value As String)
            _Procedure = value
        End Set
    End Property

    Public Property Result() As String
        Get
            Return _Result
        End Get
        Set(ByVal value As String)
            _Result = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
End Class
