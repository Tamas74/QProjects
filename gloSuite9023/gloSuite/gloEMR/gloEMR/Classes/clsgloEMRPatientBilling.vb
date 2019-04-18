'Contains information for a patient 
Namespace PatientBillingInfo

    Public Class PatientInterface
        Private _LastName As String = ""
        Private _FirstName As String = ""
        Private _MidName As String = ""
        Private _Address As String = ""
        Private _City As String = ""
        Private _State As String = ""
        Private _Zip As String = ""
        Private _Phone As String = ""
        Private _WorkPhone As String = ""
        Private _Fax As String = ""
        Private _Email As String = ""
        Private _DateOfBirth As String
        Private _Gender As String = ""
        Private _SSN As Int64 = 0
        Private _Marital As String = ""
        Private _DoctorCode As String = ""
        Private _ReferralDoctor As PatientReferralDoctor
        Private bReferralDoctor As Boolean = False
        Private _PatientCode As String = ""
        Private _InsuranceCollection As InsuranceCollection
        Private bInsuranceCollection As Boolean = False

        Public Property InsuranceCollection() As InsuranceCollection
            Get
                If IsNothing(_InsuranceCollection) Then
                    _InsuranceCollection = New InsuranceCollection
                    bInsuranceCollection = True
                End If
                Return _InsuranceCollection
            End Get
            Set(ByVal Value As InsuranceCollection)
                If (bInsuranceCollection) Then
                    If (IsNothing(_InsuranceCollection) = False) Then
                        _InsuranceCollection.Dispose()
                        _InsuranceCollection = Nothing
                    End If
                    bInsuranceCollection = False
                End If
                _InsuranceCollection = Value
            End Set
        End Property
        Public Property ReferralDoctor() As PatientReferralDoctor
            Get
                Return _ReferralDoctor
            End Get
            Set(ByVal Value As PatientReferralDoctor)
                If (bReferralDoctor) Then
                    If (IsNothing(_ReferralDoctor) = False) Then
                        _ReferralDoctor.Dispose()
                        _ReferralDoctor = Nothing
                    End If
                    bReferralDoctor = False
                End If
                _ReferralDoctor = Value
            End Set
        End Property
        Public Property PatientCode() As String
            Get
                Return _PatientCode
            End Get
            Set(ByVal Value As String)
                _PatientCode = Value
            End Set
        End Property
        Public Property Doctorcode() As String
            Get
                Return _DoctorCode
            End Get
            Set(ByVal Value As String)
                _DoctorCode = Value
            End Set
        End Property
        Public Property LastName() As String
            Get
                Return _LastName
            End Get
            Set(ByVal Value As String)
                _LastName = Value
            End Set
        End Property

        Public Property FirstName() As String
            Get
                Return _FirstName
            End Get
            Set(ByVal Value As String)
                _FirstName = Value
            End Set
        End Property
        Public Property MidName() As String
            Get
                Return _MidName
            End Get
            Set(ByVal Value As String)
                _MidName = Value
            End Set
        End Property
        Public Property Address() As String
            Get
                Return _Address
            End Get
            Set(ByVal Value As String)
                _Address = Value
            End Set
        End Property
        Public Property City() As String
            Get
                Return _City
            End Get
            Set(ByVal Value As String)
                _City = Value
            End Set
        End Property
        Public Property State() As String
            Get
                Return _State
            End Get
            Set(ByVal Value As String)
                _State = Value
            End Set
        End Property
        Public Property Zip() As String
            Get
                Return _Zip
            End Get
            Set(ByVal Value As String)
                _Zip = Value
            End Set
        End Property
        Public Property Phone() As String
            Get
                Return _Phone
            End Get
            Set(ByVal Value As String)
                _Phone = Value
            End Set
        End Property
        Public Property WorkPhone() As String
            Get
                Return _WorkPhone
            End Get
            Set(ByVal Value As String)
                _WorkPhone = Value
            End Set
        End Property
        Public Property Fax() As String
            Get
                Return _Fax
            End Get
            Set(ByVal Value As String)
                _Fax = Value
            End Set
        End Property
        Public Property Email() As String
            Get
                Return _Email
            End Get
            Set(ByVal Value As String)
                _Email = Value
            End Set
        End Property
        Public Property DateofBirth() As String
            Get
                Return _DateOfBirth
            End Get
            Set(ByVal Value As String)
                _DateOfBirth = Value
            End Set
        End Property
        Public Property Gender() As String
            Get
                Return _Gender
            End Get
            Set(ByVal Value As String)
                _Gender = Value
            End Set
        End Property
        Public Property Marital() As String
            Get
                Return _Marital
            End Get
            Set(ByVal Value As String)
                _Marital = Value
            End Set
        End Property
        Public Property SSN() As Int64
            Get
                Return _SSN
            End Get
            Set(ByVal Value As Int64)
                _SSN = Value
            End Set
        End Property
        Public Sub Dispose()

            If (bReferralDoctor) Then
                If (IsNothing(_ReferralDoctor) = False) Then
                    _ReferralDoctor.Dispose()
                    _ReferralDoctor = Nothing
                End If
                bReferralDoctor = False
            End If
            If (bInsuranceCollection) Then
                If (IsNothing(_InsuranceCollection) = False) Then
                    _InsuranceCollection.Dispose()
                    _InsuranceCollection = Nothing
                End If
                bInsuranceCollection = False
            End If

        End Sub
        'Contains information for Patient Insurance
        Public Class PatientInsuranceInterface
            Private _InsuredFirstName As String
            Private _InsuredLastName As String
            Private _InsuredMidName As String
            Private _InsuredBirth As String
            Private _InsuredPhone As String
            Private _InsuredGroup As String
            Private _InsuredPolicyNumber As String
            Private _InsuranceName As String
            Private _InsuranceAddress1 As String
            Private _InsuranceAddress2 As String
            Private _InsuranceCity As String
            Private _InsuranceState As String
            Private _InsuranceZip As String
            Private _InsuranceContact As String
            Private _InsurancePhone As String
            Private _InsuranceFax As String
            Public Property InsuranceName() As String
                Get
                    Return _InsuranceName
                End Get
                Set(ByVal Value As String)
                    _InsuranceName = Value
                End Set
            End Property
            Public Property InsuranceAddress1() As String
                Get
                    Return _InsuranceAddress1
                End Get
                Set(ByVal Value As String)
                    _InsuranceAddress1 = Value
                End Set
            End Property
            Public Property InsuranceAddress2() As String
                Get
                    Return _InsuranceAddress2
                End Get
                Set(ByVal Value As String)
                    _InsuranceAddress2 = Value
                End Set
            End Property
            Public Property InsuranceCity() As String
                Get
                    Return _InsuranceCity
                End Get
                Set(ByVal Value As String)
                    _InsuranceCity = Value
                End Set
            End Property
            Public Property InsuranceState() As String
                Get
                    Return _InsuranceState
                End Get
                Set(ByVal Value As String)
                    _InsuranceState = Value
                End Set
            End Property
            Public Property InsuranceZip() As String
                Get
                    Return _InsuranceZip
                End Get
                Set(ByVal Value As String)
                    _InsuranceZip = Value
                End Set
            End Property
            Public Property InsuranceContact() As String
                Get
                    Return _InsuranceContact
                End Get
                Set(ByVal Value As String)
                    _InsuranceContact = Value
                End Set
            End Property
            Public Property InsurancePhone() As String
                Get
                    Return _InsurancePhone
                End Get
                Set(ByVal Value As String)
                    _InsurancePhone = Value
                End Set
            End Property
            Public Property InsuranceFax() As String
                Get
                    Return _InsuranceFax
                End Get
                Set(ByVal Value As String)
                    _InsuranceFax = Value
                End Set
            End Property
            Public Property InsuredPolicyNumber() As String
                Get
                    Return _InsuredPolicyNumber
                End Get
                Set(ByVal Value As String)
                    _InsuredPolicyNumber = Value
                End Set
            End Property
            Public Property InsuredFirstName() As String
                Get
                    Return _InsuredFirstName
                End Get
                Set(ByVal Value As String)
                    _InsuredFirstName = Value
                End Set
            End Property
            Public Property InsuredLastName() As String
                Get
                    Return _InsuredLastName
                End Get
                Set(ByVal Value As String)

                End Set
            End Property
            Public Property InsuredMidName() As String
                Get
                    Return _InsuredMidName
                End Get
                Set(ByVal Value As String)
                    _InsuredMidName = Value
                End Set
            End Property
            Public Property InsuredBirth() As String
                Get
                    Return _InsuredBirth
                End Get
                Set(ByVal Value As String)
                    _InsuredBirth = Value
                End Set
            End Property
            Public Property InsuredGroup() As String
                Get
                    Return _InsuredGroup
                End Get
                Set(ByVal Value As String)
                    _InsuredGroup = Value
                End Set
            End Property
            Public Property InsuredPhone() As String
                Get
                    Return _InsuredPhone
                End Get
                Set(ByVal Value As String)
                    _InsuredPhone = Value

                End Set
            End Property

            Public Sub Dispose()

            End Sub
        End Class
        'Contains information for Patient Appointment
        Public Class PatientReferralDoctor
            Private _Code As String
            Private _FirstName As String
            Private _LastName As String
            Private _Address As String
            Private _City As String
            Private _State As String
            Private _Zip As String
            Private _Phone As String

            Public Property Code() As String
                Get
                    Return _Code
                End Get
                Set(ByVal Value As String)
                    _Code = Value
                End Set
            End Property
            Public Property FirstName() As String
                Get
                    Return _FirstName
                End Get
                Set(ByVal Value As String)
                    _FirstName = Value
                End Set
            End Property
            Public Property LastName() As String
                Get
                    Return _LastName
                End Get
                Set(ByVal Value As String)
                    _LastName = Value
                End Set
            End Property
            Public Property Address() As String
                Get
                    Return _Address
                End Get
                Set(ByVal Value As String)
                    _Address = Value
                End Set
            End Property
            Public Property City() As String
                Get
                    Return _City
                End Get
                Set(ByVal Value As String)
                    _City = Value
                End Set
            End Property
            Public Property State() As String
                Get
                    Return _State
                End Get
                Set(ByVal Value As String)
                    _State = Value
                End Set
            End Property
            Public Property Zip() As String
                Get
                    Return _Zip
                End Get
                Set(ByVal Value As String)
                    _Zip = Value
                End Set
            End Property
            Public Property Phone() As String
                Get
                    Return _Phone
                End Get
                Set(ByVal Value As String)
                    _Phone = Value

                End Set

            End Property
            Public Sub Dispose()

            End Sub
        End Class
    End Class
    Public Class PatientDoctor
        Private _Code As String
        Private _FirstName As String
        Private _LastName As String
        Private _Address As String
        Private _City As String
        Private _State As String
        Private _Zip As String
        Private _Phone As String

        Public Property Code() As String
            Get
                Return _Code
            End Get
            Set(ByVal Value As String)
                _Code = Value
            End Set
        End Property
        Public Property FirstName() As String
            Get
                Return _FirstName
            End Get
            Set(ByVal Value As String)
                _FirstName = Value
            End Set
        End Property
        Public Property LastName() As String
            Get
                Return _LastName
            End Get
            Set(ByVal Value As String)
                _LastName = Value
            End Set
        End Property
        Public Property Address() As String
            Get
                Return _Address
            End Get
            Set(ByVal Value As String)
                _Address = Value
            End Set
        End Property
        Public Property City() As String
            Get
                Return _City
            End Get
            Set(ByVal Value As String)
                _City = Value
            End Set
        End Property
        Public Property State() As String
            Get
                Return _State
            End Get
            Set(ByVal Value As String)
                _State = Value
            End Set
        End Property
        Public Property Zip() As String
            Get
                Return _Zip
            End Get
            Set(ByVal Value As String)
                _Zip = Value
            End Set
        End Property
        Public Property Phone() As String
            Get
                Return _Phone
            End Get
            Set(ByVal Value As String)
                _Phone = Value

            End Set

        End Property
        Public Sub Dispose()

        End Sub
    End Class
    Public Class PatientAppointmentInterface
        Public Sub Dispose()
            If (bDoctor) Then
                If (IsNothing(bDoctor) = False) Then
                    _Doctor.Dispose()
                    _Doctor = Nothing
                End If
                bDoctor = False
            End If
        End Sub
        Private _KeyID As String
        Private _PatientCode As String
        Private _Doctor As PatientDoctor
        Private bDoctor As Boolean = False
        Private _FirstName As String
        Private _MiddleName As String
        Private _LastName As String
        Private _AppointmentDate As String
        Private _AppointmentInterval As String

        Public Property KeyID() As String
            Get
                Return _KeyID
            End Get
            Set(ByVal value As String)
                _KeyID = value
            End Set
        End Property
        Public Property PatientCode() As String
            Get
                Return _PatientCode
            End Get
            Set(ByVal value As String)
                _PatientCode = value
            End Set
        End Property
        Public Property Doctor() As PatientDoctor
            Get
                If IsNothing(_Doctor) Then
                    _Doctor = New PatientDoctor
                    bDoctor = True
                End If
                Return _Doctor
            End Get
            Set(ByVal value As PatientDoctor)
                If (bDoctor) Then
                    If (IsNothing(bDoctor) = False) Then
                        _Doctor.Dispose()
                        _Doctor = Nothing
                    End If
                    bDoctor = False
                End If
                _Doctor = value
            End Set
        End Property
        Public Property FirstName() As String
            Get
                Return _FirstName
            End Get
            Set(ByVal Value As String)
                _FirstName = Value
            End Set
        End Property
        Public Property MiddleName() As String
            Get
                Return _MiddleName
            End Get
            Set(ByVal Value As String)
                _MiddleName = Value
            End Set
        End Property
        Public Property LastName() As String
            Get
                Return _LastName
            End Get
            Set(ByVal Value As String)
                _LastName = Value
            End Set
        End Property
        Public Property AppointmentDate() As String
            Get
                Return _AppointmentDate
            End Get
            Set(ByVal Value As String)
                _AppointmentDate = Value
            End Set
        End Property

        Public Property AppointmentInterval() As String
            Get
                Return _AppointmentInterval
            End Get
            Set(ByVal Value As String)
                _AppointmentInterval = Value
            End Set
        End Property
    End Class
    Public Class Visit
        Private _ProviderDoctor As PersonName
        Private bProviderDoctor As Boolean = False
        Private _VisitID As Int64
        Private _PatientID As Int64
        Private _VisitDate As DateTime
        Private _CPTs As CPTs
        Private bCPTs As Boolean = False
        Private _Externalcode As String
        Public Sub Dispose()
            If (bCPTs) Then
                If (IsNothing(_CPTs) = False) Then
                    _CPTs.Dispose()
                    _CPTs = Nothing
                End If
                bCPTs = False
            End If
            If (bProviderDoctor) Then
                If (IsNothing(_ProviderDoctor) = False) Then
                    _ProviderDoctor.Dispose()
                    _ProviderDoctor = Nothing
                End If
                bProviderDoctor = False
            End If
        End Sub
        Public Property ExternalCode() As String
            Get
                Return _Externalcode
            End Get
            Set(ByVal value As String)
                _Externalcode = value
            End Set
        End Property
        Public Property CPTCol() As CPTs
            Get
                If IsNothing(_CPTs) Then
                    _CPTs = New CPTs
                    bCPTs = True
                End If
                Return _CPTs
            End Get
            Set(ByVal value As CPTs)
                If (bCPTs) Then
                    If (IsNothing(_CPTs) = False) Then
                        _CPTs.Dispose()
                        _CPTs = Nothing
                    End If
                    bCPTs = False
                End If
                _CPTs = value
            End Set
        End Property
        Public Property ProviderDoctor() As PersonName
            Get
                If IsNothing(_ProviderDoctor) Then
                    _ProviderDoctor = New PersonName
                    bProviderDoctor = True
                End If
                Return _ProviderDoctor
            End Get
            Set(ByVal Value As PersonName)
                If (bProviderDoctor) Then
                    If (IsNothing(_ProviderDoctor) = False) Then
                        _ProviderDoctor.Dispose()
                        _ProviderDoctor = Nothing
                    End If
                    bProviderDoctor = False
                End If
                _ProviderDoctor = Value
            End Set
        End Property
        Public Property PatientID() As Int64
            Get
                Return _PatientID
            End Get
            Set(ByVal Value As Int64)
                _PatientID = Value
            End Set
        End Property
        Public Property VisitID() As Int64
            Get
                Return _VisitID
            End Get
            Set(ByVal Value As Int64)
                _VisitID = Value
            End Set
        End Property
        Public Property VisitDate() As DateTime
            Get
                Return _VisitDate
            End Get
            Set(ByVal Value As DateTime)
                _VisitDate = Value
            End Set
        End Property
    End Class
    Public Class PatientICD9CPT
        Private _ICD9s As ICD9s
        Private bICD9s As Boolean = False
        'Private _Patient As Patient
        Private _ExamDate As DateTime
        Private _VisitDoctor As PersonName
        Private bVisitDoctor As Boolean = False
        Private _examid As Int64
        Private _patientKeyId As String
        Public Sub Dispose()
            If (bVisitDoctor) Then
                If (IsNothing(_VisitDoctor) = False) Then
                    _VisitDoctor.Dispose()
                    _VisitDoctor = Nothing
                End If
                bVisitDoctor = False
            End If
            If (bICD9s) Then
                If (IsNothing(_ICD9s) = False) Then
                    _ICD9s.Dispose()
                    _ICD9s = Nothing
                End If
                bICD9s = False
            End If
        End Sub
        Public Property PatientKeyID() As String
            Get
                Return _patientKeyId
            End Get
            Set(ByVal value As String)
                _patientKeyId = value
            End Set
        End Property
        Public Property ExamID() As Int64
            Get
                Return _examid
            End Get
            Set(ByVal value As Int64)
                _examid = value
            End Set
        End Property
        Public Property VisitDoctor() As PersonName
            Get
                If IsNothing(_VisitDoctor) Then
                    _VisitDoctor = New PersonName
                    bVisitDoctor = True
                End If
                Return _VisitDoctor
            End Get
            Set(ByVal value As PersonName)
                If (bVisitDoctor) Then
                    If (IsNothing(_VisitDoctor) = False) Then
                        _VisitDoctor.Dispose()
                        _VisitDoctor = Nothing
                    End If
                    bVisitDoctor = False
                End If
                _VisitDoctor = value
            End Set
        End Property
        Public Property ExamDate() As String
            Get
                Return _ExamDate
            End Get
            Set(ByVal value As String)
                _ExamDate = value
            End Set
        End Property
        'Public Property PatientObject() As Patient
        '    Get
        '        If IsNothing(_Patient) Then
        '            _Patient = New Patient
        '        End If
        '        Return _Patient
        '    End Get
        '    Set(ByVal value As Patient)
        '        _Patient = value
        '    End Set
        'End Property
        Public Property ICD9Col() As ICD9s
            Get
                If IsNothing(_ICD9s) Then
                    _ICD9s = New ICD9s
                    bICD9s = True
                End If
                Return _ICD9s
            End Get
            Set(ByVal value As ICD9s)
                If (bICD9s) Then
                    If (IsNothing(_ICD9s) = False) Then
                        _ICD9s.Dispose()
                        _ICD9s = Nothing
                    End If
                    bICD9s = False
                End If
                _ICD9s = value
            End Set
        End Property
    End Class
    Public Class CODEDetails
        Private _code As String
        Private _description As String
        Private _examid As Int64
        Public Property ExamID() As Int64
            Get
                Return _examid
            End Get
            Set(ByVal value As Int64)
                _examid = value
            End Set
        End Property
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property
    End Class
    Public Class ICD9
        Inherits CODEDetails

    End Class
    Public Class CPT
        Inherits CODEDetails
        Private _Modifiers As Modifiers
        Private bModifiers As Boolean = False
        Private _ICD9s As ICD9s
        Private bICD9s As Boolean = False
        Private _Unit As String
        Public Property Unit() As String
            Get
                Return _Unit
            End Get
            Set(ByVal value As String)
                _Unit = value
            End Set
        End Property
        Public Property ICD9Col() As ICD9s
            Get
                If IsNothing(_ICD9s) Then
                    _ICD9s = New ICD9s
                    bICD9s = True
                End If
                Return _ICD9s
            End Get
            Set(ByVal value As ICD9s)
                If (bICD9s) Then
                    If (IsNothing(_ICD9s) = False) Then
                        _ICD9s.Dispose()
                        _ICD9s = Nothing
                    End If
                    bICD9s = False
                End If
                _ICD9s = value
            End Set
        End Property
        Public Property ModfierCol() As Modifiers
            Get
                If IsNothing(_Modifiers) Then
                    _Modifiers = New Modifiers
                    bModifiers = True
                End If
                Return _Modifiers
            End Get
            Set(ByVal value As Modifiers)
                If (bModifiers) Then
                    If (IsNothing(_Modifiers) = False) Then
                        _Modifiers.Dispose()
                        _Modifiers = Nothing
                    End If
                    bModifiers = False
                End If
                _Modifiers = value
            End Set
        End Property
        Public Sub Dispose()
            If (bICD9s) Then
                If (IsNothing(_ICD9s) = False) Then
                    _ICD9s.Dispose()
                    _ICD9s = Nothing
                End If
                bICD9s = False
            End If
            If (bModifiers) Then
                If (IsNothing(_Modifiers) = False) Then
                    _Modifiers.Dispose()
                    _Modifiers = Nothing
                End If
                bModifiers = False
            End If
        End Sub
    End Class
    Public Class Modifier
        Inherits CODEDetails
        'Private _Unit As String
        'Public Property Unit() As String
        '    Get
        '        Return _Unit
        '    End Get
        '    Set(ByVal value As String)
        '        _Unit = value
        '    End Set
        'End Property
    End Class
    Public Class PersonName

        Private _ID As Long
        Private _FirstName As String
        Private _MiddleName As String
        Private _LastName As String
        Private _Code As String

        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal Value As Long)
                _ID = Value
            End Set
        End Property

        Public Property FirstName() As String
            Get
                Return _FirstName
            End Get
            Set(ByVal Value As String)
                _FirstName = Value
            End Set
        End Property

        Public Property MiddleName() As String
            Get
                Return _MiddleName
            End Get
            Set(ByVal Value As String)
                _MiddleName = Value
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return _LastName
            End Get
            Set(ByVal Value As String)
                _LastName = Value
            End Set
        End Property

        Public Property Code() As String
            Get
                Return _Code
            End Get
            Set(ByVal Value As String)
                _Code = Value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub Dispose()

        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class

#Region "gloDBCollection"
    Public Class gloDBCollection
        Inherits CollectionBase

        'Remove Item at specified index
        Public Sub Remove(ByVal index As Integer)
            ' Check to see if there is a widget at the supplied index.
            If index > Count - 1 Or index < 0 Then
                ' If no widget exists, a messagebox is shown and the operation is 
                ' cancelled.
                'System.Windows.Forms.MessageBox.Show("Index not valid!")
            Else
                ' Invokes the RemoveAt method of the List object.
                List.RemoveAt(index)
            End If
        End Sub
        ''Remove All items.
        'Public Sub RemoveAll()
        '    If List.Count > 0 Then
        '        Dim i As Int16
        '        For i = List.Count To 1 Step -1
        '            Me.Remove(i)
        '        Next
        '    End If
        'End Sub
        Public Overridable Sub Dispose()
            Me.Clear()
        End Sub
    End Class

#End Region
#Region "PatientCollection"
    Public Class PatientCollection
        Inherits gloDBCollection
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As PatientInterface
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), PatientInterface)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _PatientInterface As PatientInterface)
            ' Invokes Add method of the List object to add a widget.
            List.Add(_PatientInterface)
        End Sub
        Public Overrides Sub Dispose()
            Me.Clear()
        End Sub

    End Class

#End Region
#Region "InsuranceCollection"
    Public Class InsuranceCollection
        Inherits gloDBCollection
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInsuranceInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As PatientInterface.PatientInsuranceInterface
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInsuranceInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), PatientInterface.PatientInsuranceInterface)
            End Get
        End Property
        ' Restricts to PatientInsuranceInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _PatientInsuranceInterface As PatientInterface.PatientInsuranceInterface)
            ' Invokes Add method of the List object to add a PatientInsuranceInterface.
            List.Add(_PatientInsuranceInterface)

        End Sub
        Public Overrides Sub Dispose()
            Me.Clear()
        End Sub

    End Class
    Public Class AppointmentsCollection
        Inherits gloDBCollection
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInsuranceInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As PatientAppointmentInterface
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInsuranceInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), PatientAppointmentInterface)
            End Get
        End Property
        ' Restricts to PatientInsuranceInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _PatientAppointmentInterface As PatientAppointmentInterface)
            ' Invokes Add method of the List object to add a PatientInsuranceInterface.
            List.Add(_PatientAppointmentInterface)
        End Sub
        Public Overrides Sub Dispose()
            Me.Clear()
        End Sub
    End Class
#End Region

    Public Class ICD9s
        Inherits gloDBCollection
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As ICD9
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), ICD9)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _ICD9 As ICD9)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_ICD9)
        End Sub
        Public Overrides Sub Dispose()
            Me.Clear()
        End Sub

    End Class
    Public Class CPTs
        Inherits gloDBCollection
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As CPT
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), CPT)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _CPT As CPT)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_CPT)
        End Sub
        Public Overrides Sub Dispose()
            Me.Clear()
        End Sub
    End Class
    Public Class Modifiers
        Inherits gloDBCollection
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As Modifier
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), Modifier)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _Modifier As Modifier)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_Modifier)
        End Sub
        Public Overrides Sub Dispose()
            Me.Clear()
        End Sub
    End Class
    Public Class PatientICD9CPTs
        Inherits gloDBCollection
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As PatientICD9CPT
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), PatientICD9CPT)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _ICD9CPT As PatientICD9CPT)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_ICD9CPT)
        End Sub
        Public Overrides Sub Dispose()
            Me.Clear()
        End Sub
    End Class

End Namespace