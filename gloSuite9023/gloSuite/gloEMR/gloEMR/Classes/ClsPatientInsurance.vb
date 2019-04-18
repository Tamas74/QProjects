Public Class ClsPatientInsurance
    Private m_PatientId As Long
    Private m_InsuranceID As Long
    Private m_Insurancename As String
    Private m_SubscriberId As String
    Private m_SubscriberName As String
    Private m_SubscriberPolicy As String
    Private m_Group As String
    Private m_Employer As String
    Private m_DOB As DateTime
    Private m_Phone As String
    Private m_Checked As Boolean
    Private m_Primaryflag As Boolean
    Public Property InsuranceName() As String
        Get
            Return m_Insurancename
        End Get
        Set(ByVal Value As String)
            m_Insurancename = Value
        End Set
    End Property
    Public Property Primaryflag() As Boolean
        Get
            Return m_Primaryflag
        End Get
        Set(ByVal Value As Boolean)
            m_Primaryflag = Value
        End Set
    End Property
    Public Property Checked() As Boolean
        Get
            Return m_Checked
        End Get
        Set(ByVal Value As Boolean)
            m_Checked = Value
        End Set
    End Property

    Public Property PatientId() As Long
        Get
            Return m_PatientId
        End Get
        Set(ByVal Value As Long)
            m_PatientId = Value
        End Set
    End Property

    Public Property InsuranceId() As Int64
        Get
            Return m_InsuranceID
        End Get
        Set(ByVal Value As Int64)
            m_InsuranceID = Value
        End Set
    End Property
    Public Property SubscriberId() As String
        Get
            Return m_SubscriberId
        End Get
        Set(ByVal Value As String)
            m_SubscriberId = Value
        End Set
    End Property
    Public Property Subscribername() As String
        Get
            Return m_SubscriberName
        End Get
        Set(ByVal Value As String)
            m_SubscriberName = Value
        End Set
    End Property
    Public Property SubscriberPolicy() As String
        Get
            Return m_SubscriberPolicy
        End Get
        Set(ByVal Value As String)
            m_SubscriberPolicy = Value
        End Set
    End Property
    Public Property Group() As String
        Get
            Return m_Group
        End Get
        Set(ByVal Value As String)
            m_Group = Value
        End Set
    End Property
    Public Property Employer() As String
        Get
            Return m_Employer
        End Get
        Set(ByVal Value As String)
            m_Employer = Value
        End Set
    End Property
    Public Property Phone() As String
        Get
            Return m_Phone
        End Get
        Set(ByVal Value As String)
            m_Phone = Value
        End Set
    End Property
    Public Property DOB() As DateTime
        Get
            Return m_DOB
        End Get
        Set(ByVal Value As DateTime)
            m_DOB = Value
        End Set
    End Property
End Class
