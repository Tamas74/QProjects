Imports gloEMRGeneralLibrary.gloEMRDatabase

Namespace gloEMRActors
    Public Enum enuRxSortOrder
        DrugNameAsc = 0
        DrugNameDesc = 1
        DosageAsc = 2
        DosageDesc = 3
        RouteAsc = 4
        RouteDesc = 5
        FrequencyAsc = 6
        FrequencyDesc = 7
        DurationAsc = 8
        DurationDesc = 9
        DispenseAsc = 10
        DispenseDesc = 11
        RefillsAsc = 12
        RefillsDesc = 13
        StartDateAsc = 14
        StartDateDesc = 15
        EndDateAsc = 16
        EndDateDesc = 17
        UserNameAsc = 18
        UserNameDesc = 19
        RenewedAsc = 20
        RenewedDesc = 21
    End Enum

    Public Enum enmHistoryCriteria
        None = 0
        AddPendingResult = 1
        Current = 2
        Yesterday = 3
        LastWeek = 4
        LastMonth = 5
        Older = 6
    End Enum
    Public Class Visit
        Implements IDisposable
        Private disposedValue As Boolean = False        ' To detect redundant calls
        Private _VisitId As Int64
        Private _PatientId As Int64
        Private _ProviderID As Int64
        Private _VisitDate As DateTime
        Private _AppointmentID As Int64

        Public Sub New()
            MyBase.New()
        End Sub
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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
        Public Property VisitID() As Int64
            Get
                Return _VisitId
            End Get
            Set(ByVal value As Int64)
                _VisitId = value
            End Set
        End Property
        Public Property PatientID() As Int64
            Get
                Return _PatientId
            End Get
            Set(ByVal value As Int64)
                _PatientId = value
            End Set
        End Property
        Public Property ProviderID() As Int64
            Get
                Return _ProviderID
            End Get
            Set(ByVal value As Int64)
                _ProviderID = value
            End Set
        End Property
        Public Property AppointmentID() As Int64
            Get
                Return _AppointmentID
            End Get
            Set(ByVal value As Int64)
                _AppointmentID = value
            End Set
        End Property
        Public Property VisitDate() As DateTime
            Get
                Return _VisitDate
            End Get
            Set(ByVal value As DateTime)
                _VisitDate = value
            End Set
        End Property
    End Class



    'Class added by sagar on 28 may 2007 after consulting supriya madam for retrieving the drugname & drugid in medication drug interaction
    Public Class generalList
        Private _ID As Int64 'for drugId in medication code added by sagar after consulting supriya madam on 28 may 2007
        Private _Description As String 'for drugId in medication code added by sagar after consulting supriya madam on 28 may 2007
        Private _code As String

        Public Property ID() As Int64
            Get
                Return _ID
            End Get
            Set(ByVal value As Int64)
                _ID = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
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
    End Class





    '// PATIENT OBJECT //
    Public Class Patient
        Implements IDisposable

        Private _Name As PersonName
        Private _SSN As String
        Private _DOB As Date
        Private _Gender As String
        Private _MaritalStatus As String
        Private _Address As AddressDetail
        Private _Occupation As String
        Private _EmployeeStatus As String
        Private _PlaceOfEmployement As String
        Private _WorkAddress As AddressDetail
        Private _ChiefComplaints As String
        Private _Provider As PersonName
        Private _PrimaryCarePhysican As PersonName
        Private _Guranter As String
        Private _PharmacyID As Long
        Private _SpouseName As PersonName
        Private _SpousePhone As String
        Private _Race As String
        Private _Status As String
        Private _RegistrationDate As Date
        Private _InjuryDate As Date
        Private _SurgeryDate As Date
        Private _HandDominance As String
        Private _Location As String
        Private _MotherName As PersonName
        Private _MotherAddress As AddressDetail
        Private _FatherName As PersonName
        Private _FatherAddress As AddressDetail
        Private _GuardianName As PersonName
        Private _GuardianAddress As AddressDetail
        Private _PatientDirective As Boolean
        Private _ExcemptFromReport As Boolean

        Private bName As Boolean = True
        Private bAddress As Boolean = True
        Private bWorkAddress As Boolean = True
        Private bProvider As Boolean = True
        Private bPrimaryCarePhysican As Boolean = True
        Private bSpouseName As Boolean = True
        Private bMotherName As Boolean = True
        Private bMotherAddress As Boolean = True
        Private bFatherName As Boolean = True
        Private bFatherAddress As Boolean = True
        Private bGuardianName As Boolean = True
        Private bGuardianAddress As Boolean = True
       

        Public Property PatientName() As PersonName
            Get
                Return _Name
            End Get
            Set(ByVal Value As PersonName)
                If (bName) Then
                    If (IsNothing(_Name) = False) Then
                        _Name.Dispose()
                        _Name = Nothing
                    End If

                    bName = False
                End If
                _Name = Value

            End Set
        End Property

        Public Property SSN() As String
            Get
                Return _SSN
            End Get
            Set(ByVal Value As String)
                _SSN = Value
            End Set
        End Property

        Public Property DOB() As Date
            Get
                Return _DOB
            End Get
            Set(ByVal Value As Date)
                _DOB = Value
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

        Public Property MaritalStatus() As String
            Get
                Return _MaritalStatus
            End Get
            Set(ByVal Value As String)
                _MaritalStatus = Value
            End Set
        End Property

        Public Property Address() As AddressDetail
            Get
                Return _Address
            End Get
            Set(ByVal Value As AddressDetail)
                If (bAddress) Then
                    If (IsNothing(_Address) = False) Then
                        _Address.Dispose()
                        _Address = Nothing
                    End If

                    bAddress = False
                End If

                _Address = Value

            End Set
        End Property

        Public Property Occupation() As String
            Get
                Return _Occupation
            End Get
            Set(ByVal Value As String)
                _Occupation = Value
            End Set
        End Property
        Public Property EmployeeStatus() As String
            Get
                Return _EmployeeStatus
            End Get
            Set(ByVal Value As String)
                _EmployeeStatus = Value
            End Set

        End Property

        Public Property PlaceofEmployment() As String
            Get
                Return _PlaceOfEmployement
            End Get
            Set(ByVal Value As String)
                _PlaceOfEmployement = Value
            End Set
        End Property

        Public Property WorkAddress() As AddressDetail
            Get
                Return _WorkAddress
            End Get
            Set(ByVal Value As AddressDetail)
                If (bWorkAddress) Then
                    If (IsNothing(_WorkAddress) = False) Then
                        _WorkAddress.Dispose()
                        _WorkAddress = Nothing
                    End If

                    bWorkAddress = False
                End If
                _WorkAddress = Value

            End Set
        End Property

        Public Property ChiefComplaints() As String
            Get
                Return _ChiefComplaints
            End Get
            Set(ByVal Value As String)
                _ChiefComplaints = Value
            End Set
        End Property

        Public Property Provider() As PersonName
            Get
                Return _Provider
            End Get
            Set(ByVal Value As PersonName)
                If (bProvider) Then
                    If (IsNothing(_Provider) = False) Then
                        _Provider.Dispose()
                        _Provider = Nothing
                    End If

                    bProvider = False
                End If
                _Provider = Value

            End Set
        End Property

        Public Property PrimaryCarePhysican() As PersonName
            Get
                Return _PrimaryCarePhysican
            End Get
            Set(ByVal Value As PersonName)
                If (bPrimaryCarePhysican) Then
                    If (IsNothing(_PrimaryCarePhysican) = False) Then
                        _PrimaryCarePhysican.Dispose()
                        _PrimaryCarePhysican = Nothing
                    End If

                    bPrimaryCarePhysican = False
                End If

                _PrimaryCarePhysican = Value

            End Set
        End Property

        Public Property Guranter() As String
            Get
                Return _Guranter
            End Get
            Set(ByVal Value As String)
                _Guranter = Value
            End Set
        End Property

        Public Property PharmacyID() As Long
            Get
                Return _PharmacyID
            End Get
            Set(ByVal Value As Long)
                _PharmacyID = Value
            End Set
        End Property

        Public Property SpouseName() As PersonName
            Get
                Return _SpouseName
            End Get
            Set(ByVal Value As PersonName)
                If (bSpouseName) Then
                    If (IsNothing(_SpouseName) = False) Then
                        _SpouseName.Dispose()
                        _SpouseName = Nothing
                    End If

                    bSpouseName = False
                End If
                _SpouseName = Value

            End Set
        End Property

        Public Property SpousePhone() As String
            Get
                Return _SpousePhone
            End Get
            Set(ByVal Value As String)
                _SpousePhone = Value
            End Set
        End Property

        Public Property Race() As String
            Get
                Return _Race
            End Get
            Set(ByVal Value As String)
                _Race = Value
            End Set
        End Property

        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal Value As String)
                _Status = Value
            End Set
        End Property

        Public Property RegistrationDate() As Date
            Get
                Return _RegistrationDate
            End Get
            Set(ByVal Value As Date)
                _RegistrationDate = Value
            End Set
        End Property

        Public Property InjuryDate() As Date
            Get
                Return _InjuryDate
            End Get
            Set(ByVal Value As Date)
                _InjuryDate = Value
            End Set
        End Property

        Public Property SurgeryDate() As Date
            Get
                Return _SurgeryDate
            End Get
            Set(ByVal Value As Date)
                _SurgeryDate = Value
            End Set
        End Property

        Public Property HandDominance() As String
            Get
                Return _HandDominance
            End Get
            Set(ByVal Value As String)
                _HandDominance = Value
            End Set
        End Property

        Public Property Location() As String
            Get
                Return _Location
            End Get
            Set(ByVal Value As String)
                _Location = Value
            End Set
        End Property

        Public Property MotherName() As PersonName
            Get
                Return _MotherName
            End Get
            Set(ByVal Value As PersonName)
                If (bMotherName) Then
                    If (IsNothing(_MotherName) = False) Then
                        _MotherName.Dispose()
                        _MotherName = Nothing
                    End If

                    bMotherName = False
                End If

                _MotherName = Value

            End Set
        End Property

        Public Property MotherAddress() As AddressDetail
            Get
                Return _MotherAddress
            End Get
            Set(ByVal Value As AddressDetail)
                If (bMotherAddress) Then
                    If (IsNothing(_MotherAddress) = False) Then
                        _MotherAddress.Dispose()
                        _MotherAddress = Nothing
                    End If
                    bMotherAddress = False
                End If

                _MotherAddress = Value

            End Set
        End Property

        Public Property FatherName() As PersonName
            Get
                Return _FatherName
            End Get
            Set(ByVal Value As PersonName)
                If (bFatherName) Then
                    If (IsNothing(_FatherName) = False) Then
                        _FatherName.Dispose()
                        _FatherName = Nothing
                    End If

                    bFatherName = False
                End If
                _FatherName = Value

            End Set
        End Property

        Public Property FatherAddress() As AddressDetail
            Get
                Return _FatherAddress
            End Get
            Set(ByVal Value As AddressDetail)
                If (bFatherAddress) Then
                    If (IsNothing(_FatherAddress) = False) Then
                        _FatherAddress.Dispose()
                        _FatherAddress = Nothing
                    End If

                    bFatherAddress = False
                End If
                _FatherAddress = Value

            End Set
        End Property

        Public Property GuardianName() As PersonName
            Get
                Return _GuardianName
            End Get
            Set(ByVal Value As PersonName)
                If (bGuardianName) Then
                    If (IsNothing(_GuardianName) = False) Then
                        _GuardianName.Dispose()
                        _GuardianName = Nothing
                    End If

                    bGuardianName = False
                End If

                _GuardianName = Value

            End Set
        End Property

        Public Property GuardianAddress() As AddressDetail
            Get
                Return _GuardianAddress
            End Get
            Set(ByVal Value As AddressDetail)
                If (bGuardianAddress) Then
                    If (IsNothing(_GuardianAddress) = False) Then
                        _GuardianAddress.Dispose()
                        _GuardianAddress = Nothing
                    End If
                    bGuardianAddress = False
                End If

                _GuardianAddress = Value

            End Set
        End Property

        Public Property PatientDirective() As Boolean
            Get
                Return _PatientDirective
            End Get
            Set(ByVal Value As Boolean)
                _PatientDirective = Value
            End Set
        End Property

        Public Property ExcemptFromReport() As Boolean
            Get
                Return _ExcemptFromReport
            End Get
            Set(ByVal Value As Boolean)
                _ExcemptFromReport = Value
            End Set
        End Property


        Public Sub New()
            MyBase.New()
            _Name = New PersonName
            _Address = New AddressDetail
            _WorkAddress = New AddressDetail
            _Provider = New PersonName
            _PrimaryCarePhysican = New PersonName
            _SpouseName = New PersonName
            _MotherName = New PersonName
            _MotherAddress = New AddressDetail
            _FatherName = New PersonName
            _FatherAddress = New AddressDetail
            _GuardianName = New PersonName
            _GuardianAddress = New AddressDetail
        End Sub

        Protected Overrides Sub Finalize()
            If (bName) Then
                _Name = Nothing
            End If
            If (bAddress) Then
                _Address = Nothing
            End If
            If (bWorkAddress) Then
                _WorkAddress = Nothing
            End If
            If (bProvider) Then
                _Provider = Nothing
            End If
            If (bPrimaryCarePhysican) Then
                _PrimaryCarePhysican = Nothing
            End If
            If (bSpouseName) Then
                _SpouseName = Nothing
            End If
            If (bMotherName) Then
                _MotherName = Nothing
            End If
            If (bMotherAddress) Then
                _MotherAddress = Nothing
            End If
            If (bFatherName) Then
                _FatherName = Nothing
            End If
            If (bFatherAddress) Then
                _FatherAddress = Nothing
            End If
            If (bGuardianName) Then
                _GuardianName = Nothing
            End If
            If (bGuardianAddress) Then
                _GuardianAddress = Nothing
            End If

            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If (bName) Then
                        If (IsNothing(_Name) = False) Then
                            _Name.Dispose()
                            _Name = Nothing
                        End If
                        bName = False
                    End If
                    If (bAddress) Then
                        If (IsNothing(_Address) = False) Then
                            _Address.Dispose()
                            _Address = Nothing
                        End If
                       
                        bAddress = False
                    End If
                    If (bWorkAddress) Then
                        If (IsNothing(_WorkAddress) = False) Then
                            _WorkAddress.Dispose()
                            _WorkAddress = Nothing
                        End If
                       
                        bWorkAddress = False
                    End If
                    If (bProvider) Then
                        If (IsNothing(_Provider) = False) Then
                            _Provider.Dispose()
                            _Provider = Nothing
                        End If
                       
                        bProvider = False
                    End If
                    If (bPrimaryCarePhysican) Then
                        If (IsNothing(_PrimaryCarePhysican) = False) Then
                            _PrimaryCarePhysican.Dispose()
                            _PrimaryCarePhysican = Nothing
                        End If
                      
                        bPrimaryCarePhysican = False
                    End If
                    If (bSpouseName) Then
                        If (IsNothing(_SpouseName) = False) Then
                            _SpouseName.Dispose()
                            _SpouseName = Nothing
                        End If
                       
                        bSpouseName = False
                    End If
                    If (bMotherName) Then
                        If (IsNothing(_MotherName) = False) Then
                            _MotherName.Dispose()
                            _MotherName = Nothing
                        End If
                     
                        bMotherName = False
                    End If
                    If (bMotherAddress) Then
                        If (IsNothing(_MotherAddress) = False) Then
                            _MotherAddress.Dispose()
                            _MotherAddress = Nothing
                        End If
                        bMotherAddress = False
                    End If
                    If (bFatherName) Then
                        If (IsNothing(_FatherName) = False) Then
                            _FatherName.Dispose()
                            _FatherName = Nothing
                        End If
                     
                        bFatherName = False
                    End If
                    If (bFatherAddress) Then
                        If (IsNothing(_FatherAddress) = False) Then
                            _FatherAddress.Dispose()
                            _FatherAddress = Nothing
                        End If
                      
                        bFatherAddress = False
                    End If
                    If (bGuardianName) Then
                        If (IsNothing(_GuardianName) = False) Then
                            _GuardianName.Dispose()
                            _GuardianName = Nothing
                        End If
                       
                        bGuardianName = False
                    End If
                    If (bGuardianAddress) Then
                        If (IsNothing(_GuardianAddress) = False) Then
                            _GuardianAddress.Dispose()
                            _GuardianAddress = Nothing
                        End If
                        bGuardianAddress = False
                    End If
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

    '// INSURANCE OBJECT //
    Public Class Insurance
        Implements IDisposable

        Private _PatientID As Int64
        Private _SubscriberName As String
        Private _SubscriberPolicyNumber As String
        Private _SubscriberID As String
        Private _Group As String
        Private _Employer As String
        Private _Phone As String
        Private _DOB As Date
        Private _PrimaryFlag As Boolean = False
        Private _InsuranceID As Int64


        Public Property PatientID() As Int64
            Get
                Return _PatientID
            End Get
            Set(ByVal Value As Int64)
                _PatientID = Value
            End Set
        End Property

        Public Property SubscriberName() As String
            Get
                Return _SubscriberName
            End Get
            Set(ByVal Value As String)
                _SubscriberName = Value
            End Set
        End Property

        Public Property SubscriberPolicyNumber() As String
            Get
                Return _SubscriberPolicyNumber
            End Get
            Set(ByVal Value As String)
                _SubscriberPolicyNumber = Value
            End Set
        End Property

        Public Property SubscriberID() As String
            Get
                Return _SubscriberID
            End Get
            Set(ByVal Value As String)
                _SubscriberID = Value
            End Set
        End Property

        Public Property Group() As String
            Get
                Return _Group
            End Get
            Set(ByVal Value As String)
                _Group = Value
            End Set
        End Property

        Public Property Employer() As String
            Get
                Return _Employer
            End Get
            Set(ByVal Value As String)
                _Employer = Value
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

        Public Property DOB() As Date
            Get
                Return _DOB
            End Get
            Set(ByVal Value As Date)
                _DOB = Value
            End Set
        End Property

        Public Property PrimaryFlag() As Boolean
            Get
                Return _PrimaryFlag
            End Get
            Set(ByVal Value As Boolean)
                _PrimaryFlag = Value
            End Set
        End Property

        Public Property InsuredID_ContactID() As Int64
            Get
                Return _InsuranceID
            End Get
            Set(ByVal Value As Int64)
                _InsuranceID = Value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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

    Public Class ContactInformation
        Implements IDisposable

        Private _ContactID As Int64
        Private _Name As String
        Private _ContactPerson As String
        Private _Address As ContactAddress
        Private _URL As String
        Private _Notes As String
        Private _FirstName As String
        Private _MiddleName As String
        Private _LastName As String
        Private _Gender As String
        Private _SpecialtyID As Int64
        Private _InsuranceID As Int64
        Private _HospitalAffiliation As String
        Private _ContactType As String
        Private bAddress As Boolean = True

        Public Sub New()
            MyBase.New()
            _Address = New ContactAddress
        End Sub

        Public Property ContactID() As Int64
            Get
                Return _ContactID
            End Get
            Set(ByVal Value As Int64)
                _ContactID = Value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

        Public Property ContactPerson() As String
            Get
                Return _ContactPerson
            End Get
            Set(ByVal Value As String)
                _ContactPerson = Value
            End Set
        End Property

        Public Property Address() As ContactAddress
            Get
                Return _Address
            End Get
            Set(ByVal Value As ContactAddress)
                If (bAddress) Then
                    If (IsNothing(_Address) = False) Then
                        _Address.Dispose()
                        _Address = Nothing
                    End If

                    bAddress = False
                End If
                _Address = Value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return _URL
            End Get
            Set(ByVal Value As String)
                _URL = Value
            End Set
        End Property

        Public Property Notes() As String
            Get
                Return _Notes
            End Get
            Set(ByVal Value As String)
                _Notes = Value
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

        Public Property Gender() As String
            Get
                Return _Gender
            End Get
            Set(ByVal Value As String)
                _Gender = Value
            End Set
        End Property

        Public Property SpecialtyID() As Int64
            Get
                Return _SpecialtyID
            End Get
            Set(ByVal Value As Int64)
                _SpecialtyID = Value
            End Set
        End Property

        Public Property InsuranceID() As Int64
            Get
                Return _InsuranceID
            End Get
            Set(ByVal Value As Int64)
                _InsuranceID = Value
            End Set
        End Property

        Public Property HospitalAffiliation() As String
            Get
                Return _HospitalAffiliation
            End Get
            Set(ByVal Value As String)
                _HospitalAffiliation = Value
            End Set
        End Property

        Public Property ContactType() As String
            Get
                Return _ContactType
            End Get
            Set(ByVal Value As String)
                _ContactType = Value
            End Set
        End Property

        Protected Overrides Sub Finalize()
            If (bAddress) Then
                _Address = Nothing
            End If

            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If (bAddress) Then
                        If (IsNothing(_Address) = False) Then
                            _Address.Dispose()
                            _Address = Nothing
                        End If

                        bAddress = False
                    End If
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

    '// LAB OBJECT //
    Public Class LabOrder
        Implements IDisposable


        Private _OrderCode As String

        Public Property OrderCode() As String
            Get
                Return _OrderCode
            End Get
            Set(ByVal Value As String)
                _OrderCode = Value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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

    '// LAB TEST //
    Public Class LabTest
        Implements IDisposable

        Private _TestLOINCCode As String
        Private _TestNumericResult As String
        Private _TestResultUnit As String

        Public Property TestLOINCCode() As String
            Get
                Return _TestLOINCCode
            End Get
            Set(ByVal Value As String)
                _TestLOINCCode = Value
            End Set
        End Property

        Public Property TestNumericResult() As String
            Get
                Return _TestNumericResult
            End Get
            Set(ByVal Value As String)
                _TestNumericResult = Value
            End Set
        End Property

        Public Property TestResultUnit() As String
            Get
                Return _TestResultUnit
            End Get
            Set(ByVal Value As String)
                _TestResultUnit = Value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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

    Public Class LabTests
        Inherits gloBaseCollection
        Implements IDisposable

        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As LabTest
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), LabTest)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _LabTest As LabTest)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_LabTest)
        End Sub




    End Class

    '// Visit Information //
    Public Class Appointment
        Implements IDisposable

        Private _PatientID As Int64
        Private _ProviderName As PersonName
        Private _AppointmentStartDate As DateTime
        Private _AppointmentStartTime As DateTime
        Private _AppointmentEndDate As DateTime
        Private _AppointmentEndTime As DateTime
        Private _ChiefComplaint As String
        Private _AppointmentType As String
        Private bProviderName As Boolean = True


        Public Property PatientID() As Int64
            Get
                Return _PatientID
            End Get
            Set(ByVal Value As Int64)
                _PatientID = Value
            End Set
        End Property

        Public Property ProviderName() As PersonName
            Get
                Return _ProviderName
            End Get
            Set(ByVal Value As PersonName)
                If (bProviderName) Then
                    If (IsNothing(_ProviderName) = False) Then
                        _ProviderName.Dispose()
                        _ProviderName = Nothing
                    End If

                    bProviderName = False
                End If
                _ProviderName = Value
            End Set
        End Property

        Public Property StartDate() As DateTime
            Get
                Return _AppointmentStartDate
            End Get
            Set(ByVal Value As DateTime)
                _AppointmentStartDate = Value
            End Set
        End Property

        Public Property StartTime() As DateTime
            Get
                Return _AppointmentStartTime
            End Get
            Set(ByVal Value As DateTime)
                _AppointmentStartTime = Value
            End Set
        End Property

        Public Property EndDate() As DateTime
            Get
                Return _AppointmentEndDate
            End Get
            Set(ByVal Value As DateTime)
                _AppointmentEndDate = Value
            End Set
        End Property

        Public Property EndTime() As DateTime
            Get
                Return _AppointmentEndTime
            End Get
            Set(ByVal Value As DateTime)
                _AppointmentEndTime = Value
            End Set
        End Property

        Public Property ChiefComplints() As String
            Get
                Return _ChiefComplaint
            End Get
            Set(ByVal Value As String)
                _ChiefComplaint = Value
            End Set
        End Property

        Public Property AppointmentType() As String
            Get
                Return _AppointmentType
            End Get
            Set(ByVal Value As String)
                _AppointmentType = Value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
            _ProviderName = New PersonName
        End Sub

        Protected Overrides Sub Finalize()
            If (bProviderName) Then
                 
                    _ProviderName = Nothing


                bProviderName = False
            End If
            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If (bProviderName) Then
                        If (IsNothing(_ProviderName) = False) Then
                            _ProviderName.Dispose()
                            _ProviderName = Nothing
                        End If

                        bProviderName = False
                    End If
                    ' TODO: free unmanaged resources when explicitly called
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

    Public Class History
        Implements IDisposable

#Region "private variables"
        Private _intHistoryID As Int64
        Private _intVisitID As Int64
        Private _intPatientID As Int64
        Private _strHistoryCategory As String
        Private _strHistoryItem As String
        Private _strComments As String
        Private _strReaction As String
        Private _intDrugID As Int64
        'For History Denormalization
        'The  history item name can be treated as drug name
        Private _strDrugName As String
        Private _strDrugDosage As String
        Private _strNDCCode As String
        Private _intDDID As Int64
        Private _sAllergyClassID As String
        'For History Denormalization
#End Region

#Region "properties"
        Public Property HistoryID() As Int64
            Get
                Return _intHistoryID
            End Get
            Set(ByVal value As Int64)
                _intHistoryID = value
            End Set
        End Property

        Public Property VisitID() As Int64
            Get
                Return _intVisitID
            End Get
            Set(ByVal value As Int64)
                _intVisitID = value
            End Set
        End Property


        Public Property PatientID() As Int64
            Get
                Return _intPatientID
            End Get
            Set(ByVal value As Int64)
                _intPatientID = value
            End Set
        End Property


        Public Property HistoryCategory() As String
            Get
                Return _strHistoryCategory
            End Get
            Set(ByVal value As String)
                _strHistoryCategory = value
            End Set
        End Property

        'property return type changed from int16  to string by sagar on 6 june 2007 after consulting supria madam 
        'it was giving error in the RxDBLayer and MedicationBusinesslayer  "GetHistory_Categorywise" function while binding the 
        'datatable zeroth item to HistoryItem
        Public Property HistoryItem() As String
            Get
                Return _strHistoryItem
            End Get
            Set(ByVal value As String)
                _strHistoryItem = value
            End Set
        End Property
        Public Property Comments() As String
            Get
                Return _strComments
            End Get
            Set(ByVal value As String)
                _strComments = value
            End Set
        End Property

        Public Property Reaction() As String
            Get
                Return _strReaction
            End Get
            Set(ByVal value As String)
                _strReaction = value
            End Set
        End Property
        Public Property DrugID() As Int64
            Get
                Return _intDrugID
            End Get
            Set(ByVal value As Int64)
                _intDrugID = value
            End Set
        End Property

        'For History Denormalization
        Public Property DrugName() As String
            Get
                Return _strDrugName
            End Get
            Set(ByVal value As String)
                _strDrugName = value
            End Set
        End Property
        Public Property DrugDosage() As String
            Get
                Return _strDrugDosage
            End Get
            Set(ByVal value As String)
                _strDrugDosage = value
            End Set
        End Property
        Public Property NDCCode() As String
            Get
                Return _strNDCCode
            End Get
            Set(ByVal value As String)
                _strNDCCode = value
            End Set
        End Property
        Public Property DDID() As Int64
            Get
                Return _intDDID
            End Get
            Set(ByVal value As Int64)
                _intDDID = value
            End Set
        End Property
        'For History Denormalization

        Public Property AllergyClassID() As String
            Get
                Return _sAllergyClassID
            End Get
            Set(ByVal value As String)
                _sAllergyClassID = value
            End Set
        End Property


#End Region

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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

    Public Class Histories
        Inherits gloBaseCollection
        Implements IDisposable


        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As History
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), History)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _History As History)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_History)
        End Sub



    End Class


    '//---SUPPORTING OBJECTS---//

    '/-+- NAME DETAILS -+-/
    Public Class PersonName
        Implements IDisposable

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

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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

    '/-+- ADDRESS DETAILS -+-/
    Public Class AddressDetail
        Implements IDisposable

        Private _Address1 As String
        Private _Address2 As String
        Private _City As String
        Private _State As String
        Private _Zip As String
        Private _County As String
        Private _Phone As String
        Private _Mobile As String
        Private _Email As String
        Private _Fax As String

        Public Property Address1() As String
            Get
                Return _Address1
            End Get
            Set(ByVal Value As String)
                _Address1 = Value
            End Set
        End Property

        Public Property Address2() As String
            Get
                Return _Address2
            End Get
            Set(ByVal Value As String)
                _Address2 = Value
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

        Public Property County() As String
            Get
                Return _County
            End Get
            Set(ByVal Value As String)
                _County = Value
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

        Public Property Mobile() As String
            Get
                Return _Mobile
            End Get
            Set(ByVal Value As String)
                _Mobile = Value
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

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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

    '/-+- ADDRESS DETAILS -+-/
    Public Class ContactAddress
        Implements IDisposable

        Private _Street As String
        Private _City As String
        Private _State As String
        Private _Zip As String
        Private _Phone As String
        Private _Mobile As String
        Private _Email As String
        Private _Fax As String
        Private _Pager As String

        Public Property Street() As String
            Get
                Return _Street
            End Get
            Set(ByVal Value As String)
                _Street = Value
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

        Public Property Mobile() As String
            Get
                Return _Mobile
            End Get
            Set(ByVal Value As String)
                _Mobile = Value
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

        Public Property Pager() As String
            Get
                Return _Pager
            End Get
            Set(ByVal Value As String)
                _Pager = Value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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


    '/-+- COLLECTION BASE DETAILS -+-/
    Public Class RxMed
        Implements IDisposable

        Private _VisitID As Int64
        Private _PatientID As Int64
        Private _Medication As String = ""
        Private _Dosage As String = ""

        Private _OldDosage As String = "" 'variable for property proc for OldDosage.

        Private _Route As String = ""
        Private _Frequency As String = ""
        Private _Refills As String = ""
        Private _Duration As String = ""
        Private _Startdate As DateTime
        'variable added by sagar on 12 june 2007 to handle the logic of endate in prescription
        Private _CheckEndDate As Boolean
        Private _EndDate As DateTime
        Private _Amount As String = ""
        Private _sUserName As String = ""
        Private _sUpdatedByUserName As String = ""
        'Private _nPrescriptionId As Long
        Private _nUserID As Long

        'Private _Renewed As String

        'Public Property Renewed() As String
        '    Get
        '        Return _Renewed
        '    End Get
        '    Set(ByVal value As String)
        '        _Renewed = value
        '    End Set
        'End Property

        '***********For De-Normalization
        Private _sNDCCode As String = ""
        Private _sRxNormCode As String = ""

        Private _nIsNarcotics As Int16 'save the narcotic type value
        Private _nDDID As Long
        Private m_mpid As Int32
        '***********For De-Normalization


        'Formulary
        Private _RxType As String = ""
        Private _FormularyStatus As String = ""
        Private _CoverageIndicator As String = ""
        Private _CopayIndicator As String = ""
        'Formulary

        'Temprory formulary grid information
        Private _FormularyInformation_Grid As String = String.Empty
        'Temprory formulary Richtextbox information
        Private _FormularyInformation_RchTxtBx As String = String.Empty
        'Formulary

        Private _sPBMSourceName As String = ""
        Private mFileData As Byte() = Nothing
        Private _sFileDataXML As String = ""

        Public Property RxType() As String
            Get
                Return _RxType
            End Get
            Set(ByVal value As String)
                _RxType = value
            End Set
        End Property
        Public Property FormularyStatus() As String
            Get
                Return _FormularyStatus
            End Get
            Set(ByVal value As String)
                _FormularyStatus = value
            End Set
        End Property
        Public Property CoverageIndicator() As String
            Get
                Return _CoverageIndicator
            End Get
            Set(ByVal value As String)
                _CoverageIndicator = value
            End Set
        End Property
        Public Property CopayIndicator() As String
            Get
                Return _CopayIndicator
            End Get
            Set(ByVal value As String)
                _CopayIndicator = value
            End Set
        End Property
        'Formulary

        'Temperory Formulary Information grid
        Public Property FormularyInformation_Grid() As String
            Get
                Return _FormularyInformation_Grid
            End Get
            Set(ByVal value As String)
                _FormularyInformation_Grid = value
            End Set
        End Property
        'Temperory Formulary Information Rich text box
        Public Property FormularyInformation_RchTxtBx() As String
            Get
                Return _FormularyInformation_RchTxtBx
            End Get
            Set(ByVal value As String)
                _FormularyInformation_RchTxtBx = value
            End Set
        End Property

        '***********For De-Normalization
        Public Property NDCCode() As String
            Get
                Return _sNDCCode
            End Get
            Set(ByVal Value As String)
                _sNDCCode = Value
            End Set
        End Property


        '***********For De-Normalization
        Public Property RxNormCode() As String
            Get
                Return _sRxNormCode
            End Get
            Set(ByVal Value As String)
                _sRxNormCode = Value
            End Set
        End Property



        'save the narcotic type value
        Public Property IsNarcotics() As Int16
            Get
                Return _nIsNarcotics
            End Get
            Set(ByVal Value As Int16)
                _nIsNarcotics = Value
            End Set
        End Property

        Public Property mpid() As Int32
            Get
                Return m_mpid
            End Get
            Set(ByVal Value As Int32)
                m_mpid = Value
            End Set
        End Property

        Public Property DDID() As Long
            Get
                Return _nDDID
            End Get
            Set(ByVal Value As Long)
                _nDDID = Value
            End Set
        End Property

        '***********For De-Normalization

        Public Property UserId() As Long
            Get
                Return _nUserID
            End Get
            Set(ByVal Value As Long)
                _nUserID = Value
            End Set
        End Property

        'Public Property _PrescriptionId() As Long
        '    Get
        '        Return _nPrescriptionId
        '    End Get
        '    Set(ByVal Value As Long)
        '        _nPrescriptionId = Value
        '    End Set
        'End Property
        Public Property UserName() As String
            Get
                Return _sUserName
            End Get
            Set(ByVal Value As String)
                _sUserName = Value
            End Set
        End Property
        Public Property UpdatedByUserName() As String
            Get
                Return _sUpdatedByUserName
            End Get
            Set(ByVal Value As String)
                _sUpdatedByUserName = Value
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
        Public Property Medication() As String
            Get
                Return _Medication
            End Get
            Set(ByVal value As String)
                _Medication = value
            End Set
        End Property
        Public Property Dosage() As String
            Get
                Return _Dosage
            End Get
            Set(ByVal value As String)
                _Dosage = value
            End Set
        End Property

        'this property proc is taken to add the compare the dosage when we refill from Rx and that item is carry forwarded 
        'in Mx so we will compare this olddosage value to the present value and if it matched then we will update that entry in Mx.
        Public Property OldDosage() As String
            Get
                Return _OldDosage
            End Get
            Set(ByVal value As String)
                _OldDosage = value
            End Set
        End Property

        Dim _routes As List(Of String)
        Public Property routes() As List(Of String)
            Get
                Return _routes
            End Get
            Set(ByVal Value As List(Of String))
                _routes = Value
            End Set
        End Property

        Public Property Route() As String
            Get
                Return _Route
            End Get
            Set(ByVal value As String)
                _Route = value
            End Set
        End Property
        Public Property Frequency() As String
            Get
                Return _Frequency
            End Get
            Set(ByVal value As String)
                _Frequency = value
            End Set
        End Property
        Public Property Refills() As String
            Get
                Return _Refills
            End Get
            Set(ByVal value As String)
                _Refills = value
            End Set
        End Property
        Public Property Duration() As String
            Get
                Return _Duration
            End Get
            Set(ByVal value As String)
                _Duration = value
            End Set
        End Property

        Public ReadOnly Property DaysSupply() As String
            Get
                Dim nDaysSupply As Integer = 0
                If _Duration IsNot Nothing Then
                    If _Duration.Trim.Length > 0 AndAlso Val(_Duration) <> 0 Then
                        If IsNumeric(_Duration) Then
                            nDaysSupply = Val(_Duration)
                        Else
                            Dim nDuration As String() = Nothing
                            Dim numberofDays As Integer
                            nDuration = _Duration.Trim.Split(" ")
                            If nDuration.Length > 0 Then
                                Select Case nDuration(1).ToUpper
                                    Case "MONTHS"
                                        numberofDays = 30
                                    Case "DAYS"
                                        numberofDays = 1
                                    Case "WEEKS"
                                        numberofDays = 7
                                End Select
                                nDaysSupply = numberofDays * CType(nDuration(0), Integer)
                            End If
                        End If
                    End If
                End If                
                Return nDaysSupply
            End Get
        End Property


        Public Property Startdate() As DateTime
            Get
                Return _Startdate
            End Get
            Set(ByVal value As DateTime)
                _Startdate = value
            End Set
        End Property
        Public Property Enddate() As DateTime
            Get
                Return _EndDate
            End Get
            Set(ByVal value As DateTime)
                _EndDate = value
            End Set
        End Property

        'Added by sagar on 12 june 2007 to handle the logic for end date
        Public Property CheckEndDate() As Boolean
            Get
                Return _CheckEndDate
            End Get
            Set(ByVal Value As Boolean)
                _CheckEndDate = Value
            End Set
        End Property

        Public Property Amount() As String
            Get
                Return _Amount
            End Get
            Set(ByVal value As String)
                _Amount = value
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

        Public Property PBMSourceName() As String
            Get
                Return _sPBMSourceName
            End Get
            Set(ByVal value As String)
                _sPBMSourceName = value
            End Set
        End Property
        Public Property FileData() As Byte()
            Get
                Return mFileData
            End Get
            Set(ByVal value As Byte())
                mFileData = value
            End Set
        End Property

        Public Property FileDataXML() As String
            Get
                Return _sFileDataXML
            End Get
            Set(ByVal value As String)
                _sFileDataXML = value
            End Set
        End Property

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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
#Region "Prescription Class"


    Public Class Prescription
        Inherits RxMed
        Implements IDisposable

        Public Sub New()
            MyBase.new()

            If gloGeneral.clsgeneral.gblnDisableAllowSubstitution.HasValue Then
                _MaySubstitute = gloGeneral.clsgeneral.gblnDisableAllowSubstitution.Value
            Else
                _MaySubstitute = True
            End If


        End Sub
        Private _PrescriptionID As Int64
        Private _IseRxed As Int64 = 0
        Private _paReferenceID As String = String.Empty
        Private _PCTransactionID As Int64 = 0
        Private _paStatus As String = String.Empty
        Private _paNumber As String = String.Empty

        Private _Notes As String = ""
        Private _PrescriberNotes As String = "" '20090716
        Private _Method As String = ""
        Private _MaySubstitute As Boolean
        Private _PRN As Boolean
        Private _Prescriptiondate As DateTime
        Private _DrugId As Int64
        Private _blnflag As Boolean
        Private _LotNo As String = ""
        Private _Expirationdate As DateTime
        'variable added by sagar to handle the logic for end date on 12 june 2007
        Private _CheckExpiryDate As Boolean
        Private _ProviderID As Int64
        Private _PharmacyID As Int64 'new field added in Rx table on 14 Feb 2008
        Private _ChiefComplaint As String = ""
        'variable & property procedure added by sagar to handle the logic of refill Rx item so that it is reflected in the medication table on 30 july 2007
        Private _Renewed As String = ""
        Private _Problems As String = "" 'property proc to update the problemlist table is added to update the prescription against that patientid & problemlistid
        Private _ReasonToOverride As String = ""
        Private _Status As String = "" 'this will contain the value whether the Rx item is deleted or not. if deleted it will contain 'D' else have a blank value
        Private _State As String = "" 'this property used for saving rowstate uesd in TVP for saving prescription and medication
        Private _RefillQuantity As String = ""
        Private _RefillQualifier As String = ""
        'Private _IsNarcotics As Int64
        Private _ApprovalNotes As String = ""

        'For De-Normalization
        Private _DosageForm As String = ""
        Private _StrengthUnit As String = ""
        'For De-Normalization

        'For Pharmacy
        Private _PhNCPDPID As String = ""
        Private _PhContactID As Int64
        Private _PharmacyName As String = ""
        Private _PhAddressline1 As String = ""
        Private _PhAddressline2 As String = ""
        Private _PhCity As String = ""
        Private _PhState As String = ""
        Private _PhZip As String = ""
        Private _PhEmail As String = ""
        Private _PhFax As String = ""
        Private _PhPhone As String = ""
        Private _PhServiceLevel As String = ""
        'For Pharmacy

        'To save the eRxStatus and message for the eRx prescription
        Private _eRxStatus As String = ""
        Private _eRxStatusMessage As String = ""

        Private _EPCSeRxStatus As String = ""
        '' kozmary issue multiple drugs adding
        Private _ItemNumber As Integer
        '' kozmary issue multiple drugs adding

        ''for CCHIT11
        Private _DosageFrequencyValue As Int32
        Private _DosageFrequencyText As String = ""
        Private _IsCPOEOrder As Boolean = False 'added for MU2 CPOE measure
        Private _MedicationAdministered As Boolean = False
        Private _PotencyCode As String = "" 'added for 10.6 changes
        Private _PotencyUnit As String = "" 'added for 10.6 changes
        Private _MessageType As String = ""
        Private _MessageID As String = ""
        Private _AlternativeFormID As Integer = Nothing
        Private _IsFormularyQueried As Boolean = False
        Private _FlagtoDeletePrescription As Boolean = True

        Private _CheckFlag As tmpCheckFlag = Nothing
        '''''General Poperty added to  set flag of particular enum type..
        Public Property CheckFlag() As tmpCheckFlag
            Get
                Return _CheckFlag
            End Get
            Set(ByVal value As tmpCheckFlag)
                _CheckFlag = value
            End Set
        End Property
        Public Property PotencyCode() As String
            Get
                Return _PotencyCode
            End Get
            Set(ByVal value As String)
                _PotencyCode = value
            End Set
        End Property

        Public Property PotencyUnit() As String
            Get
                Return _PotencyUnit
            End Get
            Set(ByVal value As String)
                _PotencyUnit = value
            End Set
        End Property

        Public Property ApprovalNotes() As String
            Get
                Return _ApprovalNotes
            End Get
            Set(ByVal value As String)
                _ApprovalNotes = value
            End Set
        End Property
        Public Property ItemNumber() As Integer
            Get
                Return _ItemNumber
            End Get
            Set(ByVal value As Integer)
                _ItemNumber = value
            End Set
        End Property
        Public Property MessageType() As String
            Get
                Return _MessageType
            End Get
            Set(ByVal value As String)
                _MessageType = value
            End Set
        End Property
        Public Property MessageID() As String
            Get
                Return _MessageID
            End Get
            Set(ByVal value As String)
                _MessageID = value
            End Set
        End Property

        Public Property AlternativeFormId() As Integer
            Get
                Return _AlternativeFormID
            End Get
            Set(ByVal value As Integer)
                _AlternativeFormID = value
            End Set
        End Property

        'For De-Normalization

        'Public Property IsNarcotics() As Int64
        '    Get
        '        Return _IsNarcotics
        '    End Get
        '    Set(ByVal value As Int64)
        '        _IsNarcotics = value
        '    End Set
        'End Property

        ''Formulary
        'Private _DrugType As String = ""
        ''Formulary

        ''Formulary
        'Public Property DrugType() As String
        '    Get
        '        Return _DrugType
        '    End Get
        '    Set(ByVal value As String)
        '        _DrugType = value
        '    End Set
        'End Property

        ''Formulary

        ''For Pharmacy
        Public Property PhNCPDPID() As String
            Get
                Return _PhNCPDPID
            End Get
            Set(ByVal value As String)
                _PhNCPDPID = value
            End Set
        End Property

        Public Property PhContactID() As Int64
            Get
                Return _PhContactID
            End Get
            Set(ByVal value As Int64)
                _PhContactID = value
            End Set
        End Property

        Public Property PharmacyName() As String
            Get
                Return _PharmacyName
            End Get
            Set(ByVal value As String)
                _PharmacyName = value
            End Set
        End Property

        Public Property PhAddressline1() As String
            Get
                Return _PhAddressline1
            End Get
            Set(ByVal value As String)
                _PhAddressline1 = value
            End Set
        End Property
        Public Property PhAddressline2() As String
            Get
                Return _PhAddressline2
            End Get
            Set(ByVal value As String)
                _PhAddressline2 = value
            End Set
        End Property

        Public Property PhCity() As String
            Get
                Return _PhCity
            End Get
            Set(ByVal value As String)
                _PhCity = value
            End Set
        End Property
        Public Property PhState() As String
            Get
                Return _PhState
            End Get
            Set(ByVal value As String)
                _PhState = value
            End Set
        End Property

        Public Property PhZip() As String
            Get
                Return _PhZip
            End Get
            Set(ByVal value As String)
                _PhZip = value
            End Set
        End Property
        Public Property PhEmail() As String
            Get
                Return _PhEmail
            End Get
            Set(ByVal value As String)
                _PhEmail = value
            End Set
        End Property
        Public Property PhFax() As String
            Get
                Return _PhFax
            End Get
            Set(ByVal value As String)
                _PhFax = value
            End Set
        End Property
        Public Property PhPhone() As String
            Get
                Return _PhPhone
            End Get
            Set(ByVal value As String)
                _PhPhone = value
            End Set
        End Property

        Public Property PhServiceLevel() As String
            Get
                Return _PhServiceLevel
            End Get
            Set(ByVal value As String)
                _PhServiceLevel = value
            End Set
        End Property
        ''For Pharmacy

        Public Property DosageForm() As String
            Get
                Return _DosageForm
            End Get
            Set(ByVal value As String)
                _DosageForm = value
            End Set
        End Property

        Public Property StrengthUnit() As String
            Get
                Return _StrengthUnit
            End Get
            Set(ByVal value As String)
                _StrengthUnit = value
            End Set
        End Property
        'For De-Normalization


        Public Property RefillQuantity() As String
            Get
                Return _RefillQuantity
            End Get
            Set(ByVal value As String)
                _RefillQuantity = value
            End Set
        End Property
        Public Property RefillQualifier() As String
            Get
                Return _RefillQualifier
            End Get
            Set(ByVal value As String)
                _RefillQualifier = value
            End Set
        End Property
        'this property procedure is written for changes wrt surescripts
        'this will contain the value whether the Rx item is deleted or not. if deleted it will contain 'D' else have a blank value
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property
        'new property added for state
        Public Property State() As String
            Get
                Return _State
            End Get
            Set(ByVal value As String)
                _State = value
            End Set
        End Property


        Public Property ReasontoOverride() As String
            Get
                Return _ReasonToOverride
            End Get
            Set(ByVal value As String)
                _ReasonToOverride = value
            End Set
        End Property
        Public Property Problems() As String
            Get
                Return _Problems
            End Get
            Set(ByVal value As String)
                _Problems = value
            End Set
        End Property


        Public Property Renewed() As String
            Get
                Return _Renewed
            End Get
            Set(ByVal value As String)
                _Renewed = value
            End Set
        End Property

        Public Property ChiefComplaint() As String
            Get
                Return _ChiefComplaint
            End Get
            Set(ByVal value As String)
                _ChiefComplaint = value
            End Set
        End Property

        Public Property ProviderID() As Int64
            Get
                Return _ProviderID
            End Get
            Set(ByVal value As Int64)
                _ProviderID = value
            End Set
        End Property


        Public Property PharmacyID() As Int64
            Get
                Return _PharmacyID
            End Get
            Set(ByVal value As Int64)
                _PharmacyID = value
            End Set
        End Property

        Public Property PrescriptionID() As Int64
            Get
                Return _PrescriptionID
            End Get
            Set(ByVal value As Int64)
                _PrescriptionID = value
            End Set
        End Property

        Public Property IseRxed() As Int64
            Get
                Return _IseRxed
            End Get
            Set(ByVal value As Int64)
                _IseRxed = value
            End Set
        End Property

        'this is  for PHARMACY NOTES
        Public Property Notes() As String
            Get
                Return _Notes
            End Get
            Set(ByVal value As String)
                _Notes = value
            End Set
        End Property

        'this is  for PRESCRIBER NOTES
        Public Property PrescriberNotes() As String  '20090716
            Get
                Return _PrescriberNotes
            End Get
            Set(ByVal value As String)
                _PrescriberNotes = value
            End Set
        End Property

        Public Property Method() As String
            Get
                Return _Method
            End Get
            Set(ByVal value As String)
                _Method = value
            End Set
        End Property
        Public Property Maysubstitute() As Boolean
            Get
                Return _MaySubstitute
            End Get
            Set(ByVal value As Boolean)
                _MaySubstitute = value
            End Set
        End Property
        Public Property FlagtoDeletePrescription() As Boolean
            Get
                Return _FlagtoDeletePrescription
            End Get
            Set(ByVal value As Boolean)
                _FlagtoDeletePrescription = value
            End Set
        End Property
        Public Property PRN() As Boolean
            Get
                Return _PRN
            End Get
            Set(ByVal value As Boolean)
                _PRN = value
            End Set
        End Property
        Public Property Prescriptiondate() As DateTime
            Get
                Return _Prescriptiondate
            End Get
            Set(ByVal value As DateTime)
                _Prescriptiondate = value
            End Set
        End Property
        Public Property DrugID() As Int64
            Get
                Return _DrugId
            End Get
            Set(ByVal value As Int64)
                _DrugId = value
            End Set
        End Property
        Public Property Flag() As Boolean
            Get
                Return _blnflag
            End Get
            Set(ByVal value As Boolean)
                _blnflag = value
            End Set
        End Property
        Public Property LotNo() As String
            Get
                Return _LotNo
            End Get
            Set(ByVal value As String)
                _LotNo = value
            End Set
        End Property
        Public Property ExpirationDate() As DateTime
            Get
                Return _Expirationdate
            End Get
            Set(ByVal value As DateTime)
                _Expirationdate = value
            End Set
        End Property

        'added by sagar as done in old prescription proj to handle the end date logic on 12 june 2007
        Public Property CheckExpiryDate() As Boolean
            Get
                Return _CheckExpiryDate
            End Get
            Set(ByVal Value As Boolean)
                _CheckExpiryDate = Value
            End Set
        End Property

        'To save the eRxStatus and message for the eRx prescription
        Public Property eRxStatus() As String
            Get
                Return _eRxStatus
            End Get
            Set(ByVal value As String)
                _eRxStatus = value
            End Set
        End Property
        ''''EPCS eRx Status
        Public Property EPCSeRxStatus() As String
            Get
                Return _EPCSeRxStatus
            End Get
            Set(ByVal value As String)
                _EPCSeRxStatus = value
            End Set
        End Property
        Public Property eRxStatusMessage() As String
            Get
                Return _eRxStatusMessage
            End Get
            Set(ByVal value As String)
                _eRxStatusMessage = value
            End Set
        End Property
        'To save the eRxStatus and message for the eRx prescription
        ''' <summary>
        ''' ''added for CCHIT 11. this value is carry forwarded to dosage calc to calculate the end dosage val
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DosageFrequencyValue() As Int32
            Get
                Return _DosageFrequencyValue
            End Get
            Set(ByVal value As Int32)
                _DosageFrequencyValue = value
            End Set
        End Property

        ''' <summary>
        ''' ''added for CCHIT 11. this value is carry forwarded to dosage calc to show the selected frequency text
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DosageFrequencyText() As String
            Get
                Return _DosageFrequencyText
            End Get
            Set(ByVal value As String)
                _DosageFrequencyText = value
            End Set
        End Property
        'added new property for CPOE measure
        Public Property CPOEOrder() As Boolean
            Get
                Return _IsCPOEOrder
            End Get
            Set(ByVal value As Boolean)
                _IsCPOEOrder = value
            End Set
        End Property
        'added new property for Medication Administered
        Public Property MedicationAdministered() As Boolean
            Get
                Return _MedicationAdministered
            End Get
            Set(ByVal value As Boolean)
                _MedicationAdministered = value
            End Set
        End Property


        Public Property IsFormularyQueried() As Boolean
            Get
                Return _IsFormularyQueried
            End Get
            Set(ByVal value As Boolean)
                _IsFormularyQueried = value
            End Set
        End Property

        Public Property PriorAuthorizationStatus() As String
            Get
                If Not String.IsNullOrEmpty(_paStatus) Then
                    Select Case _paStatus.ToUpper()
                        Case "R"
                            _paStatus = "Requested"
                        Case "N"
                            _paStatus = "Closed"
                        Case "A"
                            _paStatus = "Approved"
                        Case "D"
                            _paStatus = "Denied"
                        Case "C"
                            _paStatus = "Cancelled"
                        Case "F"
                            _paStatus = "Deferred"
                    End Select
                End If
                Return _paStatus
            End Get
            Set(ByVal value As String)
                _paStatus = value
            End Set
        End Property

        Public Property PriorAuthorizationNumber() As String
            Get
                Return _paNumber
            End Get
            Set(ByVal value As String)
                _paNumber = value
            End Set
        End Property

        Public Property PAReferenceID() As String
            Get
                Return _paReferenceID
            End Get
            Set(ByVal value As String)
                _paReferenceID = value
            End Set
        End Property

        Public Property PCTransactionID() As Int64
            Get
                Return _PCTransactionID
            End Get
            Set(ByVal value As Int64)
                _PCTransactionID = value
            End Set
        End Property

        Dim _PDRPrograms As Object

        Public Property PDRPrograms() As Object
            Get
                Return _PDRPrograms
            End Get
            Set(ByVal value As Object)
                _PDRPrograms = value
            End Set
        End Property

        Protected Overrides Sub Finalize()

            MyBase.Finalize()

        End Sub

    End Class
#End Region

#Region "Medication"
    Public Class Medication
        Inherits RxMed
        Implements IDisposable


        Private _Medicationdate As DateTime
        Private _State As String = ""
        Private _Status As String = ""
        Private _CheckStatus As String = ""
        Private _Reason As String = ""
        Private _ReasonConceptID As String = ""
        Private _ReasonConceptDesc As String = ""


        Private _CQMId As String = ""
        Private _CQMDesc As String = ""

        Private _DDID As Int64
        Private _RxMedDMSID As Long ''''for CCHIT11 Medication reconcilation
        Private _UserID As Int64
        Private _MedicationID As Int64
        Private _IseRxed As Int64
        Private _nPrescriptionId As Long
        'variable & property procedure added by sagar to handle the logic of refill Rx item so that it is reflected in the medication table on 30 july 2007
        Private _Renewed As String = ""

        'For De-Normalization
        Private _DosageForm As String = ""
        Private _StrengthUnit As String = ""
        'For De-Normalization

        'Columns from Prescription table to be saved in Medication table
        Private _Rx_sRefills As String = ""
        Private _Rx_sNotes As String = ""
        Private _Rx_sMethod As String = ""
        Private _Rx_bMaySubstitute As Boolean
        Private _Rx_nDrugID As Int64
        Private _Rx_blnflag As Boolean
        Private _Rx_sLotNo As String = ""
        Private _Rx_dtExpirationdate As DateTime
        Private _Rx_nProviderId As Int64
        Private _Rx_sChiefComplaints As String = ""
        Private _Rx_sStatus As String = ""
        Private _Rx_sRxReferenceNumber As String = ""
        Private _Rx_sRefillQualifier As String = "R"
        Private _Rx_nPharmacyId As Int64
        Private _Rx_sNCPDPID As String = ""
        Private _Rx_nContactID As Int64
        Private _Rx_sName As String = ""
        Private _Rx_sAddressline1 As String = ""
        Private _Rx_sAddressline2 As String = ""
        Private _Rx_sCity As String = ""
        Private _Rx_sState As String = ""
        Private _Rx_sZip As String = ""
        Private _Rx_sEmail As String = ""
        Private _Rx_sFax As String = ""
        Private _Rx_sPhone As String = ""
        Private _Rx_sServiceLevel As String = ""
        Private _Rx_sPrescriberNotes As String = ""
        Private _Rx_eRxStatus As String = ""
        Private _Rx_eRxStatusMessage As String = ""
        'Columns from Prescription table to be saved in Medication table
        Private _sPBMSourceName As String = ""

        '' kozmary issue multiple drugs adding
        Private _ItemNumber As Integer
        '' kozmary issue multiple drugs adding

        Private _Rx_IsCPOEOrder As Boolean
        Private _Rx_MedicationAdministered As Boolean = False
        Private _CheckFlag As tmpCheckFlag = Nothing
        '''''General Poperty added to  set flag of particular enum type..
        Public Property CheckFlag() As tmpCheckFlag
            Get
                Return _CheckFlag
            End Get
            Set(ByVal value As tmpCheckFlag)
                _CheckFlag = value
            End Set
        End Property
        Public Property ItemNumber() As Integer
            Get
                Return _ItemNumber
            End Get
            Set(ByVal value As Integer)
                _ItemNumber = value
            End Set
        End Property

        Public Property Renewed() As String
            Get
                Return _Renewed
            End Get
            Set(ByVal value As String)
                _Renewed = value
            End Set
        End Property
        Dim _routes As List(Of String)
        Public Property routes() As List(Of String)
            Get
                Return _routes
            End Get
            Set(ByVal Value As List(Of String))
                _routes = Value
            End Set
        End Property
        Public Property _PrescriptionId() As Long
            Get
                Return _nPrescriptionId
            End Get
            Set(ByVal Value As Long)
                _nPrescriptionId = Value
            End Set
        End Property

        Public Property MedicationID() As Int64
            Get
                Return _MedicationID
            End Get
            Set(ByVal value As Int64)
                _MedicationID = value
            End Set
        End Property
        Public Property Medicationdate() As DateTime
            Get
                Return _Medicationdate
            End Get
            Set(ByVal value As DateTime)
                _Medicationdate = value
            End Set
        End Property
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property
        'new property added for state
        Public Property State() As String
            Get
                Return _State
            End Get
            Set(ByVal value As String)
                _State = value
            End Set
        End Property

        'property procedure added for adding the archieve medication logic on 20 june 2007
        'this property procedure is called from medicationbusinesslayer UpdateStatus()
        Public Property CheckStatus() As String
            Get
                Return _CheckStatus
            End Get
            Set(ByVal Value As String)
                _CheckStatus = Value
            End Set
        End Property
        Public Property Reason() As String
            Get
                Return _Reason
            End Get
            Set(ByVal value As String)
                _Reason = value
            End Set
        End Property

        Public Property ReasonConceptID() As String
            Get
                Return _ReasonConceptID
            End Get
            Set(ByVal value As String)
                _ReasonConceptID = value
            End Set
        End Property
        
        Public Property ReasonConceptDesc() As String
            Get
                Return _ReasonConceptDesc
            End Get
            Set(ByVal value As String)
                _ReasonConceptDesc = value
            End Set
        End Property
        Public Property CQMCategories() As String
            Get
                Return _CQMId
            End Get
            Set(ByVal value As String)
                _CQMId = value
            End Set
        End Property
        Public Property CQMDesc() As String
            Get
                Return _CQMDesc
            End Get
            Set(ByVal value As String)
                _CQMDesc = value
            End Set
        End Property

        Public Property DDID() As Int64
            Get
                Return _DDID
            End Get
            Set(ByVal value As Int64)
                _DDID = value
            End Set
        End Property

        ''for CCHIT11 medication reconcilation
        Public Property RxMedDMSID() As Long
            Get
                Return _RxMedDMSID
            End Get
            Set(ByVal value As Long)
                _RxMedDMSID = value
            End Set
        End Property
        Public Property UserID() As Int64
            Get
                Return _UserID
            End Get
            Set(ByVal value As Int64)
                _UserID = value
            End Set
        End Property

        'For De-Normalization
        Public Property DosageForm() As String
            Get
                Return _DosageForm
            End Get
            Set(ByVal value As String)
                _DosageForm = value
            End Set
        End Property

        Public Property StrengthUnit() As String
            Get
                Return _StrengthUnit
            End Get
            Set(ByVal value As String)
                _StrengthUnit = value
            End Set
        End Property
        'For De-Normalization

        Public Property PBMSourceName() As String
            Get
                Return _sPBMSourceName
            End Get
            Set(ByVal value As String)
                _sPBMSourceName = value
            End Set
        End Property
        'Columns from Prescription table to be saved in Medication table
        Public Property Rx_Refills() As String
            Get
                Return _Rx_sRefills
            End Get
            Set(ByVal value As String)
                _Rx_sRefills = value
            End Set
        End Property

        'Pharmacy Notes
        Public Property Rx_Notes() As String
            Get
                Return _Rx_sNotes
            End Get
            Set(ByVal value As String)
                _Rx_sNotes = value
            End Set
        End Property

        Public Property Rx_Method() As String
            Get
                Return _Rx_sMethod
            End Get
            Set(ByVal value As String)
                _Rx_sMethod = value
            End Set
        End Property

        Public Property Rx_MaySubstitute() As Boolean
            Get
                Return _Rx_bMaySubstitute
            End Get
            Set(ByVal value As Boolean)
                _Rx_bMaySubstitute = value
            End Set
        End Property

        Public Property Rx_DrugID() As Int64
            Get
                Return _Rx_nDrugID
            End Get
            Set(ByVal value As Int64)
                _Rx_nDrugID = value
            End Set
        End Property

        Public Property Rx_blnflag() As Boolean
            Get
                Return _Rx_blnflag
            End Get
            Set(ByVal value As Boolean)
                _Rx_blnflag = value
            End Set
        End Property

        Public Property Rx_LotNo() As String
            Get
                Return _Rx_sLotNo
            End Get
            Set(ByVal value As String)
                _Rx_sLotNo = value
            End Set
        End Property

        Public Property Rx_Expirationdate() As DateTime
            Get
                Return _Rx_dtExpirationdate
            End Get
            Set(ByVal value As DateTime)
                _Rx_dtExpirationdate = value
            End Set
        End Property

        Public Property Rx_ProviderId() As Int64
            Get
                Return _Rx_nProviderId
            End Get
            Set(ByVal value As Int64)
                _Rx_nProviderId = value
            End Set
        End Property

        Public Property Rx_ChiefComplaints() As String
            Get
                Return _Rx_sChiefComplaints
            End Get
            Set(ByVal value As String)
                _Rx_sChiefComplaints = value
            End Set
        End Property

        Public Property Rx_Status() As String
            Get
                Return _Rx_sStatus
            End Get
            Set(ByVal value As String)
                _Rx_sStatus = value
            End Set
        End Property

        Public Property Rx_RxReferenceNumber() As String
            Get
                Return _Rx_sRxReferenceNumber
            End Get
            Set(ByVal value As String)
                _Rx_sRxReferenceNumber = value
            End Set
        End Property

        Public Property Rx_RefillQualifier() As String
            Get
                Return _Rx_sRefillQualifier
            End Get
            Set(ByVal value As String)
                _Rx_sRefillQualifier = value
            End Set
        End Property

        Public Property Rx_PharmacyId() As Int64
            Get
                Return _Rx_nPharmacyId
            End Get
            Set(ByVal value As Int64)
                _Rx_nPharmacyId = value
            End Set
        End Property

        Public Property Rx_NCPDPID() As String
            Get
                Return _Rx_sNCPDPID
            End Get
            Set(ByVal value As String)
                _Rx_sNCPDPID = value
            End Set
        End Property

        Public Property Rx_ContactID() As Int64
            Get
                Return _Rx_nContactID
            End Get
            Set(ByVal value As Int64)
                _Rx_nContactID = value
            End Set
        End Property

        Public Property Rx_PhName() As String
            Get
                Return _Rx_sName
            End Get
            Set(ByVal value As String)
                _Rx_sName = value
            End Set
        End Property

        Public Property Rx_Addressline1() As String
            Get
                Return _Rx_sAddressline1
            End Get
            Set(ByVal value As String)
                _Rx_sAddressline1 = value
            End Set
        End Property

        Public Property Rx_Addressline2() As String
            Get
                Return _Rx_sAddressline2
            End Get
            Set(ByVal value As String)
                _Rx_sAddressline2 = value
            End Set
        End Property

        Public Property Rx_City() As String
            Get
                Return _Rx_sCity
            End Get
            Set(ByVal value As String)
                _Rx_sCity = value
            End Set
        End Property

        Public Property Rx_State() As String
            Get
                Return _Rx_sState
            End Get
            Set(ByVal value As String)
                _Rx_sState = value
            End Set
        End Property

        Public Property Rx_Zip() As String
            Get
                Return _Rx_sZip
            End Get
            Set(ByVal value As String)
                _Rx_sZip = value
            End Set
        End Property

        Public Property Rx_Email() As String
            Get
                Return _Rx_sEmail
            End Get
            Set(ByVal value As String)
                _Rx_sEmail = value
            End Set
        End Property

        Public Property Rx_Fax() As String
            Get
                Return _Rx_sFax
            End Get
            Set(ByVal value As String)
                _Rx_sFax = value
            End Set
        End Property

        Public Property Rx_Phone() As String
            Get
                Return _Rx_sPhone
            End Get
            Set(ByVal value As String)
                _Rx_sPhone = value
            End Set
        End Property

        Public Property Rx_ServiceLevel() As String
            Get
                Return _Rx_sServiceLevel
            End Get
            Set(ByVal value As String)
                _Rx_sServiceLevel = value
            End Set
        End Property

        Public Property Rx_PrescriberNotes() As String
            Get
                Return _Rx_sPrescriberNotes
            End Get
            Set(ByVal value As String)
                _Rx_sPrescriberNotes = value
            End Set
        End Property

        Public Property Rx_eRxStatus() As String
            Get
                Return _Rx_eRxStatus
            End Get
            Set(ByVal value As String)
                _Rx_eRxStatus = value
            End Set
        End Property

        Public Property Rx_eRxStatusMessage() As String
            Get
                Return _Rx_eRxStatusMessage
            End Get
            Set(ByVal value As String)
                _Rx_eRxStatusMessage = value
            End Set
        End Property

        'added new property for CPOE MU2 measure
        Public Property Rx_CPOEOrder() As Boolean
            Get
                Return _Rx_IsCPOEOrder
            End Get
            Set(ByVal value As Boolean)
                _Rx_IsCPOEOrder = value
            End Set
        End Property
        'added new property for  MU2 
        Public Property Rx_MedicationAdministered() As Boolean
            Get
                Return _Rx_MedicationAdministered
            End Get
            Set(ByVal value As Boolean)
                _Rx_MedicationAdministered = value
            End Set
        End Property


        'Columns from Prescription table to be saved in Medication table


        Public Sub New()
            MyBase.new()

        End Sub

        Protected Overrides Sub Finalize()


            MyBase.Finalize()
        End Sub
    End Class

#End Region

#Region "RxHub Formulary Class"


    Public Class RxHubFormulary
        Inherits RxMed
        Implements IDisposable

        Public Sub New()
            MyBase.new()

        End Sub

        Private _FormularyDtlID As Int64
        Private _PrescriptionID As Int64
        Private _FormularyStatus As String = ""
        Private _FlatCoPayAmount As String = ""
        Private _PercentCoPayRate As String = ""
        Private _FirstCoPayTerm As String = ""
        Private _CoPayTier As String = ""
        Private _PrescribedAgeLimitCoverageStatus As String = ""
        Private _PrescribedDrugExclusionCoverageStatus As String = ""
        Private _PrescribedGenderLimitCoverageStatus As String = ""
        Private _PrescribedMedicalNecessityCoverageStatus As String = ""
        Private _PrescribedPriorAuthorizationCoverageStatus As String = ""
        Private _PrescribedQuantityLimitCoverageStatus As String = ""
        Private _PrescribedDrugSpecificResourceLinkCoverageStatus As String = ""
        Private _PrescribedSummaryLevelResourceLinkCoverageStatus As String = ""
        Private _PrescribedStepMedicationCoverageStatus As String = ""
        Private _PrescribedStepTherapyCoverageStatus As String = ""
        Private _PrescribedTextMessageCoverageStatus As String = ""
        Private _PrescribedNDCCode As String = ""
        Private _PrescribedRXNORMCode As String = ""
        Private _PrescribedDrugName As String = ""
        Private _PrescribedDrugStrength As String = ""
        Private _PrescribedDosageForm As String = ""
        Private _PrescribedQuantity As String = ""
        Private _PrescribedDrugType As String = ""
        Private _PrescribedRefills As String = ""
        Private _DispenseasWritten As String = ""
        Private _MailOrderBenefitUtilized As String = ""
        Private _Initiative As String = ""
        Private _Platform As String = ""
        Private _PrescriptionDeliveryMethod As String = ""
        Private _DURIndicator As String = ""
        Private _OriginalScriptInitialFormularyStatus As String = ""
        Private _OriginalScriptFlatCopayAmount As String = ""
        Private _OriginalScriptCopayRate As String = ""
        Private _OriginalScriptFirstCopayTerm As String = ""
        Private _OriginalScriptCopayTier As String = ""
        Private _OriginalScriptNDC As String = ""
        Private _OriginalScriptRxNorm As String = ""
        Private _OriginalScriptDrugName As String = ""
        Private _OriginalScriptCoverageIndicator As String = ""
        Private _OriginalScriptTextMessageDisplayed As String = ""
        Private _OriginalScriptResourceLinkDisplayed As String = ""

        Private _HealthPlanID As String = "" 'this is 271_Master HealthPlanNumber col
        Private _HealthPlanGroupID As String = "" 'this is 271_Master GroupID col
        Private _FormularyID As String = "" 'this is 271_Master FormularyListID col
        Private _CoverageID As String = "" 'this is 271_Master CoverageID col
        Private _AlternativeID As String = "" 'this is 271_Master AlternativeListID col
        Private _CopayID As String = "" 'this is 271_Master CopayID col
        Private _PBM_PayerParticipantID As String = ""
        '----------------------------------

        Public Property FormularyDtlID() As Int64
            Get
                Return _FormularyDtlID
            End Get
            Set(ByVal value As Int64)
                _FormularyDtlID = value
            End Set
        End Property

        Public Property PrescriptionID() As Int64
            Get
                Return _PrescriptionID
            End Get
            Set(ByVal value As Int64)
                _PrescriptionID = value
            End Set
        End Property

        Public Property FormularyStatus() As String
            Get
                Return _FormularyStatus
            End Get
            Set(ByVal value As String)
                _FormularyStatus = value
            End Set
        End Property
        Public Property FlatCoPayAmount() As String
            Get
                Return _FlatCoPayAmount
            End Get
            Set(ByVal value As String)
                _FlatCoPayAmount = value
            End Set
        End Property
        Public Property PercentCoPayRate() As String
            Get
                Return _PercentCoPayRate
            End Get
            Set(ByVal value As String)
                _PercentCoPayRate = value
            End Set
        End Property
        Public Property FirstCoPayTerm() As String
            Get
                Return _FirstCoPayTerm
            End Get
            Set(ByVal value As String)
                _FirstCoPayTerm = value
            End Set
        End Property
        Public Property CoPayTier() As String
            Get
                Return _CoPayTier
            End Get
            Set(ByVal value As String)
                _CoPayTier = value
            End Set
        End Property
        Public Property PrescribedAgeLimitCoverageStatus() As String
            Get
                Return _PrescribedAgeLimitCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedAgeLimitCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedDrugExclusionCoverageStatus() As String
            Get
                Return _PrescribedDrugExclusionCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedDrugExclusionCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedGenderLimitCoverageStatus() As String
            Get
                Return _PrescribedGenderLimitCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedGenderLimitCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedMedicalNecessityCoverageStatus() As String
            Get
                Return _PrescribedMedicalNecessityCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedMedicalNecessityCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedPriorAuthorizationCoverageStatus() As String
            Get
                Return _PrescribedPriorAuthorizationCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedPriorAuthorizationCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedQuantityLimitCoverageStatus() As String
            Get
                Return _PrescribedQuantityLimitCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedQuantityLimitCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedDrugSpecificResourceLinkCoverageStatus() As String
            Get
                Return _PrescribedDrugSpecificResourceLinkCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedDrugSpecificResourceLinkCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedSummaryLevelResourceLinkCoverageStatus() As String
            Get
                Return _PrescribedSummaryLevelResourceLinkCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedSummaryLevelResourceLinkCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedStepMedicationCoverageStatus() As String
            Get
                Return _PrescribedStepMedicationCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedStepMedicationCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedStepTherapyCoverageStatus() As String
            Get
                Return _PrescribedStepTherapyCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedStepTherapyCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedTextMessageCoverageStatus() As String
            Get
                Return _PrescribedTextMessageCoverageStatus
            End Get
            Set(ByVal value As String)
                _PrescribedTextMessageCoverageStatus = value
            End Set
        End Property
        Public Property PrescribedNDCCode() As String
            Get
                Return _PrescribedNDCCode
            End Get
            Set(ByVal value As String)
                _PrescribedNDCCode = value
            End Set
        End Property
        Public Property PrescribedRXNORMCode() As String
            Get
                Return _PrescribedRXNORMCode
            End Get
            Set(ByVal value As String)
                _PrescribedRXNORMCode = value
            End Set
        End Property
        Public Property PrescribedDrugName() As String
            Get
                Return _PrescribedDrugName
            End Get
            Set(ByVal value As String)
                _PrescribedDrugName = value
            End Set
        End Property
        Public Property PrescribedDrugStrength() As String
            Get
                Return _PrescribedDrugStrength
            End Get
            Set(ByVal value As String)
                _PrescribedDrugStrength = value
            End Set
        End Property
        Public Property PrescribedDosageForm() As String
            Get
                Return _PrescribedDosageForm
            End Get
            Set(ByVal value As String)
                _PrescribedDosageForm = value
            End Set
        End Property
        Public Property PrescribedQuantity() As String
            Get
                Return _PrescribedQuantity
            End Get
            Set(ByVal value As String)
                _PrescribedQuantity = value
            End Set
        End Property
        Public Property PrescribedDrugType() As String
            Get
                Return _PrescribedDrugType
            End Get
            Set(ByVal value As String)
                _PrescribedDrugType = value
            End Set
        End Property
        Public Property PrescribedRefills() As String
            Get
                Return _PrescribedRefills
            End Get
            Set(ByVal value As String)
                _PrescribedRefills = value
            End Set
        End Property
        Public Property DispenseasWritten() As String
            Get
                Return _DispenseasWritten
            End Get
            Set(ByVal value As String)
                _DispenseasWritten = value
            End Set
        End Property
        Public Property MailOrderBenefitUtilized() As String
            Get
                Return _MailOrderBenefitUtilized
            End Get
            Set(ByVal value As String)
                _MailOrderBenefitUtilized = value
            End Set
        End Property
        Public Property Initiative() As String
            Get
                Return _Initiative
            End Get
            Set(ByVal value As String)
                _Initiative = value
            End Set
        End Property
        Public Property Platform() As String
            Get
                Return _Platform
            End Get
            Set(ByVal value As String)
                _Platform = value
            End Set
        End Property
        Public Property PrescriptionDeliveryMethod() As String
            Get
                Return _PrescriptionDeliveryMethod
            End Get
            Set(ByVal value As String)
                _PrescriptionDeliveryMethod = value
            End Set
        End Property
        Public Property DURIndicator() As String
            Get
                Return _DURIndicator
            End Get
            Set(ByVal value As String)
                _DURIndicator = value
            End Set
        End Property
        Public Property OriginalScriptInitialFormularyStatus() As String
            Get
                Return _OriginalScriptInitialFormularyStatus
            End Get
            Set(ByVal value As String)
                _OriginalScriptInitialFormularyStatus = value
            End Set
        End Property
        Public Property OriginalScriptFlatCopayAmount() As String
            Get
                Return _OriginalScriptFlatCopayAmount
            End Get
            Set(ByVal value As String)
                _OriginalScriptFlatCopayAmount = value
            End Set
        End Property
        Public Property OriginalScriptCopayRate() As String
            Get
                Return _OriginalScriptCopayRate
            End Get
            Set(ByVal value As String)
                _OriginalScriptCopayRate = value
            End Set
        End Property
        Public Property OriginalScriptFirstCopayTerm() As String
            Get
                Return _OriginalScriptFirstCopayTerm
            End Get
            Set(ByVal value As String)
                _OriginalScriptFirstCopayTerm = value
            End Set
        End Property
        Public Property OriginalScriptCopayTier() As String
            Get
                Return _OriginalScriptCopayTier
            End Get
            Set(ByVal value As String)
                _OriginalScriptCopayTier = value
            End Set
        End Property
        Public Property OriginalScriptNDC() As String
            Get
                Return _OriginalScriptNDC
            End Get
            Set(ByVal value As String)
                _OriginalScriptNDC = value
            End Set
        End Property
        Public Property OriginalScriptRxNorm() As String
            Get
                Return _OriginalScriptRxNorm
            End Get
            Set(ByVal value As String)
                _OriginalScriptRxNorm = value
            End Set
        End Property
        Public Property OriginalScriptDrugName() As String
            Get
                Return _OriginalScriptDrugName
            End Get
            Set(ByVal value As String)
                _OriginalScriptDrugName = value
            End Set
        End Property
        Public Property OriginalScriptCoverageIndicator() As String
            Get
                Return _OriginalScriptCoverageIndicator
            End Get
            Set(ByVal value As String)
                _OriginalScriptCoverageIndicator = value
            End Set
        End Property
        Public Property OriginalScriptTextMessageDisplayed() As String
            Get
                Return _OriginalScriptTextMessageDisplayed
            End Get
            Set(ByVal value As String)
                _OriginalScriptTextMessageDisplayed = value
            End Set
        End Property
        Public Property OriginalScriptResourceLinkDisplayed() As String
            Get
                Return _OriginalScriptResourceLinkDisplayed
            End Get
            Set(ByVal value As String)
                _OriginalScriptResourceLinkDisplayed = value
            End Set
        End Property

        Public Property HealthPlanID() As String
            Get
                Return _HealthPlanID
            End Get
            Set(ByVal value As String)
                _HealthPlanID = value
            End Set
        End Property

        Public Property HealthPlanGroupID() As String
            Get
                Return _HealthPlanGroupID
            End Get
            Set(ByVal value As String)
                _HealthPlanGroupID = value
            End Set
        End Property

        Public Property FormularyID() As String
            Get
                Return _FormularyID
            End Get
            Set(ByVal value As String)
                _FormularyID = value
            End Set
        End Property

        Public Property CoverageID() As String
            Get
                Return _CoverageID
            End Get
            Set(ByVal value As String)
                _CoverageID = value
            End Set
        End Property

        Public Property AlternativeID() As String
            Get
                Return _AlternativeID
            End Get
            Set(ByVal value As String)
                _AlternativeID = value
            End Set
        End Property

        Public Property CopayID() As String
            Get
                Return _CopayID
            End Get
            Set(ByVal value As String)
                _CopayID = value
            End Set
        End Property

        Public Property PBM_PayerParticipantID() As String
            Get
                Return _PBM_PayerParticipantID
            End Get
            Set(ByVal value As String)
                _PBM_PayerParticipantID = value
            End Set
        End Property
        Protected Overrides Sub Finalize()

            MyBase.Finalize()

        End Sub

    End Class
#End Region

    Public Class Drug
        Implements IDisposable


#Region "private variables"

        Private _intDrugsID As Int64
        Private _strDrugsName As String
        Private _strGenericName As String
        Private _strDosage As String
        Private _strRoute As String
        Private _strFrequency As String
        Private _strDuration As String
        Private _blnIsClinicalDrug As Boolean
        Private _strAmount As String
        Private _intIsNarcotics As Int64
        Private _intnddid As Int64
        Private m_mpid As Int32
        Private _intnSigId As Int64
        Private _blnIsAllergicDrug As Boolean

        Private _strDrugForm As String
        Private _strNDCCode As String
        Private _strDrugQtyQlfr As String

        Private _strDrugFormularyStatus As String = ""
        Private _strDrugRxType As String = ""

        Private _sRefills As String = "" ''''for refills in drugproviderassociation
#End Region

#Region "public properties"
        Public Property DrugsID() As Int64
            Get
                Return _intDrugsID
            End Get
            Set(ByVal value As Int64)
                _intDrugsID = value
            End Set
        End Property

        Public Property Refills() As String
            Get
                Return _sRefills
            End Get
            Set(ByVal value As String)
                _sRefills = value
            End Set
        End Property

        Public Property DrugsName() As String
            Get
                Return _strDrugsName
            End Get
            Set(ByVal value As String)
                _strDrugsName = value
            End Set
        End Property


        Public Property GenericName() As String
            Get
                Return _strGenericName
            End Get
            Set(ByVal value As String)
                _strGenericName = value
            End Set
        End Property

        Public Property Dosage() As String
            Get
                Return _strDosage
            End Get
            Set(ByVal value As String)
                _strDosage = value
            End Set
        End Property

        Public Property Route() As String
            Get
                Return _strRoute
            End Get
            Set(ByVal value As String)
                _strRoute = value
            End Set
        End Property

        Public Property Frequency() As String
            Get
                Return _strFrequency
            End Get
            Set(ByVal value As String)
                _strFrequency = value
            End Set
        End Property

        Public Property Duration() As String
            Get
                Return _strDuration
            End Get
            Set(ByVal value As String)
                _strDuration = value
            End Set
        End Property

        Public Property IsClinicalDrug() As Boolean
            Get
                Return _blnIsClinicalDrug
            End Get
            Set(ByVal value As Boolean)
                _blnIsClinicalDrug = value
            End Set
        End Property

        Public Property Amount() As String
            Get
                Return _strAmount
            End Get
            Set(ByVal value As String)
                _strAmount = value
            End Set
        End Property

        Public Property IsNarcotics() As Int64
            Get
                Return _intIsNarcotics
            End Get
            Set(ByVal value As Int64)
                _intIsNarcotics = value
            End Set
        End Property

        Public Property mpid() As Int32
            Get
                Return m_mpid
            End Get
            Set(ByVal value As Int32)
                m_mpid = value
            End Set
        End Property

        Public Property nddid() As Int64
            Get
                Return _intnddid
            End Get
            Set(ByVal value As Int64)
                _intnddid = value
            End Set
        End Property

        Public Property IsAllergicDrug() As Boolean
            Get
                Return _blnIsAllergicDrug
            End Get
            Set(ByVal value As Boolean)
                _blnIsAllergicDrug = value
            End Set
        End Property

        Public Property DrugForm() As String
            Get
                Return _strDrugForm
            End Get
            Set(ByVal value As String)
                _strDrugForm = value
            End Set
        End Property

        Public Property SIGid() As Int64
            Get
                Return _intnSigId
            End Get
            Set(ByVal value As Int64)
                _intnSigId = value
            End Set
        End Property

        Public Property NDCCode() As String
            Get
                Return _strNDCCode
            End Get
            Set(ByVal value As String)
                _strNDCCode = value
            End Set
        End Property

        Public Property DrugQtyQualifier() As String
            Get
                Return _strDrugQtyQlfr
            End Get
            Set(ByVal value As String)
                _strDrugQtyQlfr = value
            End Set
        End Property

        Public Property DrugFormularyStatus() As String
            Get
                Return _strDrugFormularyStatus
            End Get
            Set(ByVal value As String)
                _strDrugFormularyStatus = value
            End Set
        End Property

        Public Property DrugRxType() As String
            Get
                Return _strDrugRxType
            End Get
            Set(ByVal value As String)
                _strDrugRxType = value
            End Set
        End Property

        Public Property OpportunityID() As Int64 = 0
        Public Property AlternateID() As Int64 = 0

#End Region

        Public Sub New()
            MyBase.New()

        End Sub

        Protected Overrides Sub Finalize()


            MyBase.Finalize()

        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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
    Public Class Fax
        Private _FaxId As Int64
        Private _FaxDate As DateTime
        Private _PatientID As Int64
        Private _FaxTo As String
        Private _FaxNo As String
        Private _FaxType As String
        Private _PatientName As String
        Public Property PatientName() As String
            Get
                Return _PatientName
            End Get
            Set(ByVal value As String)
                _PatientName = value
            End Set
        End Property
        Public Property FaxID() As Int64
            Get
                Return _FaxId
            End Get
            Set(ByVal value As Int64)
                _FaxId = value
            End Set
        End Property
        Public Property FaxDate() As DateTime
            Get
                Return _FaxDate
            End Get
            Set(ByVal value As DateTime)
                _FaxDate = value
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
        Public Property FaxTo() As String
            Get
                Return _FaxTo
            End Get
            Set(ByVal value As String)
                _FaxTo = value
            End Set
        End Property
        Public Property FaxNo() As String
            Get
                Return _FaxNo
            End Get
            Set(ByVal value As String)
                _FaxNo = value
            End Set
        End Property
        Public Property FaxType() As String
            Get
                Return _FaxType
            End Get
            Set(ByVal value As String)
                _FaxType = value
            End Set
        End Property
    End Class
    Public Class SentFax
        Inherits Fax
        Implements IDisposable
        Private _FaxSend As Boolean
        Private _FaxStatus As String
        Private disposedValue As Boolean = False        ' To detect redundant calls

        Public Property FaxSend() As String
            Get
                Return _FaxSend
            End Get
            Set(ByVal value As String)
                _FaxSend = value
            End Set
        End Property
        Public Property FaxStatus() As String
            Get
                Return _FaxStatus
            End Get
            Set(ByVal value As String)
                _FaxStatus = value
            End Set
        End Property

        Public Sub New()
            MyBase.new()
        End Sub

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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
    Public Class PendingFax
        Inherits Fax
        Implements IDisposable
        Private _LoginUser As String
        Private _FileName As String
        Private _NoofAttempts As Int16
        Private _CurrentStatus As String
        Private _FaxPriority As Boolean
        Private disposedValue As Boolean = False        ' To detect redundant calls
        Public Property LoginUser() As String
            Get
                Return _LoginUser
            End Get
            Set(ByVal value As String)
                _LoginUser = value
            End Set
        End Property
        Public Property FileName() As String
            Get
                Return _FileName
            End Get
            Set(ByVal value As String)
                _FileName = value
            End Set
        End Property
        Public Property NoofAttempts() As Int16
            Get
                Return _NoofAttempts
            End Get
            Set(ByVal value As Int16)
                _NoofAttempts = value
            End Set
        End Property
        Public Property CurrentStatus() As String
            Get
                Return _CurrentStatus
            End Get
            Set(ByVal value As String)
                _CurrentStatus = value
            End Set
        End Property
        Public Property FaxPriority() As Boolean
            Get
                Return _FaxPriority
            End Get
            Set(ByVal value As Boolean)
                _FaxPriority = value
            End Set
        End Property

        Public Sub New()
            MyBase.new()
        End Sub
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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
    Public Class PendingFaxDetail
        Implements IDisposable

        Private _FaxDTLID As Int64
        Private _FaxID As Int64
        Private _FaxDate As DateTime
        Private _FaxResponse As String
        Private disposedValue As Boolean = False        ' To detect redundant calls

        Public Property FaxDTLID() As Int64
            Get
                Return _FaxDTLID
            End Get
            Set(ByVal value As Int64)
                _FaxDTLID = value
            End Set
        End Property
        Public Property FaxID() As Int64
            Get
                Return _FaxID
            End Get
            Set(ByVal value As Int64)
                _FaxID = value
            End Set
        End Property
        Public Property FaxDate() As DateTime
            Get
                Return _FaxDate
            End Get
            Set(ByVal value As DateTime)
                _FaxDate = value
            End Set
        End Property
        Public Property FaxResponse() As String
            Get
                Return _FaxResponse
            End Get
            Set(ByVal value As String)
                _FaxResponse = value
            End Set
        End Property
        Public Sub New()
            MyBase.New()
        End Sub
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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
    Public Class gloBaseCollection
        Inherits CollectionBase
        Implements IDisposable
        Private disposedValue As Boolean = False        ' To detect redundant calls

        'Remove Item at specified index
        Public Sub Remove(ByVal index As Integer)
            ' Check to see if there is a widget at the supplied index.
            If index > Count - 1 Or index < 0 Then
                ' If no object exists, a messagebox is shown and the operation is 
                ' cancelled.
                'System.Windows.Forms.MessageBox.Show("Index not valid!")
            Else
                ' Invokes the RemoveAt method of the List object.
                List.RemoveAt(index)
            End If
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If
                Me.Clear()
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

    '/-/-/ INDIVIDUAL OBJECTS COLLECTION DETAILS /-/-/
    Public Class SentFaxes
        Inherits gloBaseCollection

        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a SentFax object.
        Public ReadOnly Property Item(ByVal index As Integer) As SentFax
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the SentFax type, then returned to the 
                ' caller.
                Return CType(List.Item(index), SentFax)
            End Get
        End Property
        ' Restricts to SentFax types, items that can be added to the collection.
        Public Sub Add(ByVal _SentFax As SentFax)
            ' Invokes Add method of the List object to add a SentFax.
            List.Add(_SentFax)
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
    Public Class PendingFaxes
        Inherits gloBaseCollection

        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PendingFax object.
        Public ReadOnly Property Item(ByVal index As Integer) As PendingFax
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PendingFax type, then returned to the 
                ' caller.
                Return CType(List.Item(index), PendingFax)
            End Get
        End Property
        ' Restricts to PendingFax types, items that can be added to the collection.
        Public Sub Add(ByVal _PendingFax As PendingFax)
            ' Invokes Add method of the List object to add a PendingFax.
            List.Add(_PendingFax)
        End Sub
    End Class
    Public Class PendingFaxDetails
        Inherits gloBaseCollection

        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PendingFaxDetail object.
        Public ReadOnly Property Item(ByVal index As Integer) As PendingFaxDetail
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PendingFaxDetail type, then returned to the 
                ' caller.
                Return CType(List.Item(index), PendingFaxDetail)
            End Get
        End Property
        ' Restricts to PendingFaxDetail types, items that can be added to the collection.
        Public Sub Add(ByVal _PendingFaxDetail As PendingFaxDetail)
            ' Invokes Add method of the List object to add a PendingFaxDetail.
            List.Add(_PendingFaxDetail)
        End Sub
    End Class
    Public Class Patients
        Inherits gloBaseCollection
        Implements IDisposable

        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As Patient
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), Patient)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _Patient As Patient)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_Patient)
        End Sub


    End Class


    Public Class Insurances
        Inherits gloBaseCollection
        Implements IDisposable


        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As Insurance
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), Insurance)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _Insurance As Insurance)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_Insurance)
        End Sub
        Public Sub New()

        End Sub
    End Class

    'Public Class Prescriptions
    '    Inherits gloBaseCollection
    '    Implements IDisposable


    '    ' This line declares the Item property as ReadOnly, and 
    '    ' declares that it will return a PatientInterface object.
    '    Public Property Item(ByVal index As Integer) As Prescription

    '        Get
    '            ' The appropriate item is retrieved from the List object and 
    '            ' explicitly cast to the PatientInterface type, then returned to the 
    '            ' caller.
    '            Return CType(List.Item(index), Prescription)
    '        End Get

    '        Set(ByVal value As Prescription)
    '            List.Item(index) = value
    '        End Set
    '    End Property



    '    ' Restricts to PatientInterface types, items that can be added to the collection.
    '    Public Sub Add(ByVal _Prescription As Prescription)
    '        ' Invokes Add method of the List object to add a PatientInterface.
    '        List.Add(_Prescription)
    '    End Sub

    '    Public Sub New()
    '        MyBase.New()
    '    End Sub

    '    Protected Overrides Sub Finalize()
    '        MyBase.Finalize()
    '    End Sub
    'End Class

    'Public Class Medications
    '    Inherits gloBaseCollection
    '    Implements IDisposable



    '    ' This line declares the Item property as ReadOnly, and 
    '    ' declares that it will return a PatientInterface object.
    '    Public Property Item(ByVal index As Integer) As Medication
    '        Get
    '            ' The appropriate item is retrieved from the List object and 
    '            ' explicitly cast to the PatientInterface type, then returned to the 
    '            ' caller.
    '            Return CType(List.Item(index), Medication)
    '        End Get
    '        Set(ByVal value As Medication)
    '            List.Item(index) = value
    '        End Set
    '    End Property
    '    ' Restricts to PatientInterface types, items that can be added to the collection.
    '    Public Sub Add(ByVal _Medication As Medication)
    '        ' Invokes Add method of the List object to add a PatientInterface.
    '        List.Add(_Medication)
    '    End Sub

    '    Public Sub New()
    '        MyBase.New()
    '    End Sub

    '    Protected Overrides Sub Finalize()
    '        MyBase.Finalize()
    '    End Sub

    'End Class

    Public Class ActiveEPA
        Public Sub New()
            MyBase.new()
        End Sub

        Public Property PatientID As Int64
        Public Property ProviderID As Int64
        Public Property PBMMemberId As String = String.Empty
        Public Property mpid As Int32
        Public Property ndc As String = String.Empty
        Public Property PAReferenceID As String = String.Empty
        Public Property ExpirationDate As DateTime?
        Public Property DaySupply As String = String.Empty
        Public Property Qty As String = String.Empty
        Public Property Status As String = String.Empty
        Public Property PANumber As String = String.Empty

    End Class

    Public Class Prescriptions
        Inherits List(Of Prescription)
        Implements IDisposable

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
        'Private oPrescriptions As New ArrayList
        'Public Sub Add(ByVal _Prescription As Prescription)
        '    ' Invokes Add method of the List object to add a PatientInterface.
        '    oPrescriptions.Add(_Prescription)
        'End Sub
        'Public Property Item(ByVal index As Integer) As Prescription
        '    Get
        '        ' The appropriate item is retrieved from the List object and 
        '        ' explicitly cast to the PatientInterface type, then returned to the 
        '        ' caller.

        '        Return CType(oPrescriptions.Item(index), Prescription)
        '    End Get
        '    Set(ByVal value As Prescription)
        '        oPrescriptions.Item(index) = value
        '    End Set
        'End Property
        'Public Sub Clear()
        '    oPrescriptions.Clear()
        'End Sub
        ''Public Function Remove(ByVal objPrescription As Prescription)
        ''    oPrescriptions.Remove(objPrescription)
        ''End Function
        Public Overloads Sub Remove(ByVal i As Integer)
            MyBase.RemoveAt(i)
        End Sub
        'Public Sub RemoveAt(ByVal i As Integer)
        '    oPrescriptions.RemoveAt(i)
        'End Sub
        'Public Function Count() As Integer
        '    Return oPrescriptions.Count
        'End Function
        'Public Sub Sort(ByVal oComparer As IComparer)
        '    oPrescriptions.Sort(oComparer)
        'End Sub
        Public Sub New()

        End Sub
    End Class

    Public Class RxHubFormularys
        Inherits gloBaseCollection
        Implements IDisposable

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    oRxHubFormularys.Clear()
                    ' TODO: free managed resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
            Try
                MyBase.Dispose(disposing)
            Catch ex As Exception

            End Try

        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        'Public Sub Dispose() Implements IDisposable.Dispose
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(True)
        '    GC.SuppressFinalize(Me)
        'End Sub
#End Region
        Private oRxHubFormularys As New ArrayList
        Public Sub Add(ByVal _RxHubFormulary As RxHubFormulary)
            ' Invokes Add method of the List object to add a PatientInterface.
            oRxHubFormularys.Add(_RxHubFormulary)
        End Sub
        Public Property Item(ByVal index As Integer) As RxHubFormulary
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.

                Return CType(oRxHubFormularys.Item(index), RxHubFormulary)
            End Get
            Set(ByVal value As RxHubFormulary)
                oRxHubFormularys.Item(index) = value
            End Set
        End Property
        Public Sub Clear()
            oRxHubFormularys.Clear()
        End Sub
        'Public Function Remove(ByVal objPrescription As Prescription)
        '    oPrescriptions.Remove(objPrescription)
        'End Function
        Public Sub Remove(ByVal i As Integer)
            oRxHubFormularys.RemoveAt(i)
        End Sub
        Public Sub RemoveAt(ByVal i As Integer)
            oRxHubFormularys.RemoveAt(i)
        End Sub
        Public Function Count() As Integer
            Return oRxHubFormularys.Count
        End Function
        Public Sub Sort(ByVal oComparer As IComparer)
            oRxHubFormularys.Sort(oComparer)
        End Sub

        Public Sub New()

        End Sub
    End Class

    Public Class Medications
        Inherits gloBaseCollection
        Implements IDisposable

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    oMedications.Clear()
                    ' TODO: free managed resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
            Try
                MyBase.Dispose(disposing)
            Catch ex As Exception

            End Try

        End Sub


#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        'Public Sub Dispose() Implements IDisposable.Dispose
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(True)
        '    GC.SuppressFinalize(Me)
        'End Sub
#End Region
        Private oMedications As New ArrayList
        Public Sub Add(ByVal _Medication As Medication)
            ' Invokes Add method of the List object to add a PatientInterface.
            oMedications.Add(_Medication)
        End Sub
        Public Property Item(ByVal index As Integer) As Medication
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.

                Return CType(oMedications.Item(index), Medication)
            End Get
            Set(ByVal value As Medication)
                oMedications.Item(index) = value
            End Set
        End Property
        Public Sub Clear()
            oMedications.Clear()
        End Sub
        'Public Function Remove(ByVal objMedication As Medication)
        '    oMedications.Remove(objMedication)
        'End Function
        Public Sub Remove(ByVal i As Integer)
            oMedications.RemoveAt(i)
        End Sub
        Public Sub RemoveAt(ByVal i As Integer)
            oMedications.RemoveAt(i)
        End Sub
        Public Function Count() As Integer
            Return oMedications.Count
        End Function
        Public Sub Sort(ByVal oComparer As IComparer)
            oMedications.Sort(oComparer)
        End Sub

        Public Sub New()

        End Sub
    End Class
    Public Class CMySort
        Implements IComparer
        Dim eSortColumn As enuRxSortOrder
        Sub New(ByVal enSortColumn As enuRxSortOrder)
            eSortColumn = enSortColumn
        End Sub
        Public Function Compare(ByVal x As Object, _
            ByVal y As Object) As Integer Implements _
            System.Collections.IComparer.Compare
            Select Case eSortColumn
                Case enuRxSortOrder.DosageAsc
                    Compare = CType(x, Prescription).Dosage < CType(y, Prescription).Dosage  'asc
                Case enuRxSortOrder.DosageDesc
                    Compare = CType(x, Prescription).Dosage > CType(y, Prescription).Dosage  'asc

                Case enuRxSortOrder.DrugNameAsc
                    Compare = CType(x, Prescription).Medication < CType(y, Prescription).Medication  'asc
                Case enuRxSortOrder.DrugNameDesc
                    Compare = CType(x, Prescription).Medication > CType(y, Prescription).Medication  'asc

                Case enuRxSortOrder.DispenseAsc
                    Compare = CType(x, Prescription).Amount < CType(y, Prescription).Amount   'asc
                Case enuRxSortOrder.DispenseDesc
                    Compare = CType(x, Prescription).Amount > CType(y, Prescription).Amount   'asc

                Case enuRxSortOrder.FrequencyAsc
                    Compare = CType(x, Prescription).Frequency < CType(y, Prescription).Frequency    'asc
                Case enuRxSortOrder.FrequencyDesc
                    Compare = CType(x, Prescription).Frequency > CType(y, Prescription).Frequency    'asc

                Case enuRxSortOrder.DurationAsc
                    Compare = CType(x, Prescription).Duration < CType(y, Prescription).Duration     'asc
                Case enuRxSortOrder.FrequencyDesc
                    Compare = CType(x, Prescription).Duration > CType(y, Prescription).Duration     'asc

                Case enuRxSortOrder.RefillsAsc
                    Compare = CType(x, Prescription).RefillQuantity < CType(y, Prescription).RefillQuantity      'asc
                Case enuRxSortOrder.RefillsDesc
                    Compare = CType(x, Prescription).RefillQuantity > CType(y, Prescription).RefillQuantity      'asc

                Case enuRxSortOrder.StartDateAsc
                    Compare = CType(x, Prescription).Startdate < CType(y, Prescription).Startdate       'asc
                Case enuRxSortOrder.StartDateDesc
                    Compare = CType(x, Prescription).Startdate > CType(y, Prescription).Startdate       'asc

                Case enuRxSortOrder.EndDateAsc
                    Compare = CType(x, Prescription).Enddate < CType(y, Prescription).Enddate        'asc
                Case enuRxSortOrder.EndDateDesc
                    Compare = CType(x, Prescription).Enddate > CType(y, Prescription).Enddate        'asc

                Case enuRxSortOrder.UserNameAsc
                    Compare = CType(x, Prescription).UserName < CType(y, Prescription).UserName         'asc
                Case enuRxSortOrder.UserNameDesc
                    Compare = CType(x, Prescription).UserName > CType(y, Prescription).UserName         'asc
                Case Else
                    Compare = Nothing
            End Select
        End Function

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
    Public Class CMySortMed
        Implements IComparer
        Dim eSortColumn As enuRxSortOrder
        Sub New(ByVal enSortColumn As enuRxSortOrder)
            eSortColumn = enSortColumn
        End Sub
        Public Function Compare(ByVal x As Object, _
            ByVal y As Object) As Integer Implements _
            System.Collections.IComparer.Compare
            Select Case eSortColumn
                Case enuRxSortOrder.DosageAsc
                    Compare = CType(x, Medication).Dosage < CType(y, Medication).Dosage  'asc
                Case enuRxSortOrder.DosageDesc
                    Compare = CType(x, Medication).Dosage > CType(y, Medication).Dosage  'asc

                Case enuRxSortOrder.DrugNameAsc
                    Compare = CType(x, Medication).Medication < CType(y, Medication).Medication  'asc
                Case enuRxSortOrder.DrugNameDesc
                    Compare = CType(x, Medication).Medication > CType(y, Medication).Medication  'asc

                Case enuRxSortOrder.RouteAsc
                    Compare = CType(x, Medication).Route < CType(y, Medication).Route    'asc
                Case enuRxSortOrder.RouteDesc
                    Compare = CType(x, Medication).Route > CType(y, Medication).Route    'asc

                Case enuRxSortOrder.FrequencyAsc
                    Compare = CType(x, Medication).Frequency < CType(y, Medication).Frequency    'asc
                Case enuRxSortOrder.FrequencyDesc
                    Compare = CType(x, Medication).Frequency > CType(y, Medication).Frequency    'asc

                Case enuRxSortOrder.DurationAsc
                    Compare = CType(x, Medication).Duration < CType(y, Medication).Duration     'asc
                Case enuRxSortOrder.FrequencyDesc
                    Compare = CType(x, Medication).Duration > CType(y, Medication).Duration     'asc


                Case enuRxSortOrder.StartDateAsc
                    Compare = CType(x, Medication).Startdate < CType(y, Medication).Startdate       'asc
                Case enuRxSortOrder.StartDateDesc
                    Compare = CType(x, Medication).Startdate > CType(y, Medication).Startdate       'asc

                Case enuRxSortOrder.EndDateAsc
                    Compare = CType(x, Medication).Enddate < CType(y, Medication).Enddate        'asc
                Case enuRxSortOrder.EndDateDesc
                    Compare = CType(x, Medication).Enddate > CType(y, Medication).Enddate        'asc

                Case enuRxSortOrder.UserNameAsc
                    Compare = CType(x, Medication).UserName < CType(y, Medication).UserName         'asc
                Case enuRxSortOrder.UserNameDesc
                    Compare = CType(x, Medication).UserName > CType(y, Medication).UserName         'asc

                Case enuRxSortOrder.DispenseAsc
                    Compare = CType(x, Medication).Amount < CType(y, Medication).Amount     'asc
                Case enuRxSortOrder.DispenseDesc
                    Compare = CType(x, Medication).Amount > CType(y, Medication).Amount     'asc

                Case enuRxSortOrder.RenewedAsc
                    Compare = CType(x, Medication).Renewed < CType(y, Medication).Renewed      'asc
                Case enuRxSortOrder.RefillsDesc
                    Compare = CType(x, Medication).Renewed > CType(y, Medication).Renewed      'asc
                Case Else
                    Compare = Nothing
            End Select
        End Function

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class

    Public Class Drugs
        Inherits gloBaseCollection
        Implements IDisposable
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As Drug
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), Drug)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _Drug As Drug)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_Drug)
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

    End Class
    Public Class tmpCheckState
        Implements IDisposable
        Private _CheckState As Boolean
        Private _NDCCode As String = ""
        Private _PrescriptionID As String = "" 'added in 6030 
        Private _PrintedStatus As String = "" 'added in 6050  for fax changes 
        Private _DrugPharmacyID As String 'added in 6040  for fax changes 
        Private _IssueMethod As String
        Private _DrugID As Int64
        Private disposedValue As Boolean = False        ' To detect redundant calls
        Private _IsCPOEOrder As Boolean = False
        Private _MessageType As String = ""
        Private _ItemNumber As String = ""
        Public Property CheckState() As Boolean
            Get
                Return _CheckState
            End Get
            Set(ByVal value As Boolean)
                _CheckState = value
            End Set
        End Property
        Public Property DrugID() As Int64
            Get
                Return _DrugID
            End Get
            Set(ByVal value As Int64)
                _DrugID = value
            End Set
        End Property
        Public Property NDCCode() As String   ''NDC changes - remove Drug ID dependancy
            Get
                Return _NDCCode
            End Get
            Set(ByVal value As String)
                _NDCCode = value
            End Set
        End Property
        Public Property PrescriptionID() As String   ''prescription ID changes - remove NDC dependancy in 6030
            Get
                Return _PrescriptionID
            End Get
            Set(ByVal value As String)
                _PrescriptionID = value
            End Set
        End Property
        Public Property IseRxed() As String   ''prescription ID changes - remove NDC dependancy in 6030
            Get
                Return _PrescriptionID
            End Get
            Set(ByVal value As String)
                _PrescriptionID = value
            End Set
        End Property
        Public Property PrintedStatus() As String   ''Fax changes -  in 6050
            Get
                Return _PrintedStatus
            End Get
            Set(ByVal value As String)
                _PrintedStatus = value
            End Set
        End Property
        Public Property DrugPharmacyID() As String   ''Fax changes -  in 6040
            Get
                Return _DrugPharmacyID
            End Get
            Set(ByVal value As String)
                _DrugPharmacyID = value
            End Set
        End Property
        Public Property IssueMethod() As String   ''Fax changes -  in 6040
            Get
                Return _IssueMethod
            End Get
            Set(ByVal value As String)
                _IssueMethod = value
            End Set
        End Property
        Public Property CPOEOrder() As Boolean
            Get
                Return _IsCPOEOrder
            End Get
            Set(ByVal value As Boolean)
                _IsCPOEOrder = value
            End Set
        End Property
        Public Property MessageType As String
            Get
                Return _MessageType
            End Get
            Set(ByVal value As String)
                _MessageType = value
            End Set
        End Property
        Public Property ItemNumber As String
            Get
                Return _ItemNumber
            End Get
            Set(ByVal value As String)
                _ItemNumber = value
            End Set
        End Property
        Public Property EPCSeRxStatus() As String
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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

        Public Sub New()
            MyBase.New()
        End Sub
    End Class
    Public Class tmpCheckStates
        Inherits gloBaseCollection

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a tmpCheckState object.
        Public Property Item(ByVal index As Integer) As tmpCheckState

            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the tmpCheckState type, then returned to the 
                ' caller.
                Return CType(List.Item(index), tmpCheckState)
            End Get

            Set(ByVal value As tmpCheckState)
                List.Item(index) = value
            End Set
        End Property
        ' Restricts to tmpCheckState types, items that can be added to the collection.
        Public Sub Add(ByVal _tmpCheckState As tmpCheckState)
            ' Invokes Add method of the List object to add a tmpCheckState.
            List.Add(_tmpCheckState)
        End Sub
    End Class
    Public Class gloFieldBase
        Implements IDisposable


        Private _Direction As ParameterDirection
        Private _Datatype As SqlDbType
        Private _ParameterName As String

        Public Property Direction() As ParameterDirection
            Get
                Return _Direction
            End Get
            Set(ByVal value As ParameterDirection)
                _Direction = value
            End Set
        End Property

        Public Property Datatype() As SqlDbType
            Get
                Return _Datatype
            End Get
            Set(ByVal value As SqlDbType)
                _Datatype = value
            End Set
        End Property

        Public Property ParameterName() As String
            Get
                Return _ParameterName
            End Get
            Set(ByVal value As String)
                _ParameterName = value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
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
    Public Class gloFieldString
        Inherits gloFieldBase
        Implements IDisposable

        Private _FieldValue As String
        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
        Public Property FieldValue() As String
            Get
                Return _FieldValue
            End Get
            Set(ByVal value As String)
                _FieldValue = value
            End Set
        End Property

    End Class

    Public Class gloFieldInt32
        Inherits gloFieldBase
        Implements IDisposable


        Private _FieldValue As Int32

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
        Public Property FieldValue() As Int32
            Get
                Return _FieldValue
            End Get
            Set(ByVal value As Int32)
                _FieldValue = value
            End Set
        End Property
    End Class

    Public Class gloFieldInt64
        Inherits gloFieldBase
        Implements IDisposable


        Private _FieldValue As Int64

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
        Public Property FieldValue() As Int64
            Get
                Return _FieldValue
            End Get
            Set(ByVal value As Int64)
                _FieldValue = value
            End Set
        End Property
    End Class

    Public Class gloFieldBoolean
        Inherits gloFieldBase
        Implements IDisposable

        Private _FieldValue As Boolean

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Property FieldValue() As Boolean
            Get
                Return _FieldValue
            End Get
            Set(ByVal value As Boolean)
                _FieldValue = value
            End Set
        End Property

    End Class




    Public Class gloFieldDatetime
        Inherits gloFieldBase
        Implements IDisposable


        Private _FieldValue As DateTime

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Property FieldValue() As DateTime
            Get
                Return _FieldValue
            End Get
            Set(ByVal value As DateTime)
                _FieldValue = value
            End Set
        End Property
    End Class

    Public Class LabOrders
        Inherits gloBaseCollection
        Implements IDisposable

        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As LabOrder
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), LabOrder)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _LabOrder As LabOrder)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_LabOrder)
        End Sub

        Public Sub New()

        End Sub
    End Class
    '//------------------------//
    '//---LAB MODULE---//

    Public Class AgeDetail
        Public Age As String
        Public Years As Int16
        Public Months As Int16
        Public Days As Int16

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class


    '''''Enum to set flag 
    Public Enum gloTmpFlag

        RefillMedication = 1

    End Enum
    ''' <summary>
    ''' General Class added to stored some value or flag related to individual prescription and Medication
    ''' currently used in issue- System makes the medication that was refilled 'Discontinued', even though the generated prescription was deleted 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class tmpCheckFlag
        Public flag As gloTmpFlag
        Public keyVal As KeyValuePair(Of Object, Object)
        Public Sub New()
            MyBase.New()
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class


#Region "NameSpace : LabActor"

    Namespace LabActor
        '\\ New Comman Class enum type for Collection, StorageTemperature, Specimen [20090317]
        '\\ For Denormalization of Lab
        Public Enum enumLabCCSTType
            Specimen = 1
            Collection = 2
            StorageTemperature = 3
        End Enum

        Public Enum enumTestResultType
            None = 0
            SingleResult = 1
            ProfileResult = 2
        End Enum

        Public Enum enumTestResultValueType
            None = 0
            Text = 1
            Numeric = 2
        End Enum

        Public Enum enumContactType
            None = 0
            PreferredLab = 1
            ReferredBy = 2
            SampledBy = 3
            Other = 4
        End Enum

        Public Enum enumTestResultReadType
            None = 0
            Prilimnary = 1
            Final = 2
            Ammend = 3
        End Enum

        '\\ For Denormalization of Lab
#Region "Comman Class: LabCSST AND his Collection Class : LabCSSTs "

        Public Class LabCSST
            Implements IDisposable

            Private disposedValue As Boolean = False        ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: free unmanaged resources when explicitly called
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

            Public Sub New()
                MyBase.New()
            End Sub

#Region "private variables"
            Private mLabCSST_ID As Int64 = 0
            Private mLabCSST_Code As String = ""
            Private mLabCSST_Name As String = ""
            Private mLabCSST_Type As enumLabCCSTType
            Private m_nClinicID As Int64 = 1

#End Region

#Region "properties"
            Public Property LabCSST_ID() As Int64
                Get
                    Return mLabCSST_ID
                End Get
                Set(ByVal value As Int64)
                    mLabCSST_ID = value
                End Set
            End Property

            Public Property LabCSST_Code() As String
                Get
                    Return mLabCSST_Code
                End Get
                Set(ByVal value As String)
                    mLabCSST_Code = value
                End Set
            End Property

            Public Property LabCSST_Name() As String
                Get
                    Return mLabCSST_Name
                End Get
                Set(ByVal value As String)
                    mLabCSST_Name = value
                End Set
            End Property

            Public Property LabCSST_Type() As enumLabCCSTType
                Get
                    Return mLabCSST_Type
                End Get
                Set(ByVal value As enumLabCCSTType)
                    mLabCSST_Type = value
                End Set
            End Property

            Public Property nClinicID() As Int64
                Get
                    Return m_nClinicID
                End Get
                Set(ByVal value As Int64)
                    m_nClinicID = value
                End Set
            End Property

#End Region

        End Class

        Public Class LabCSSTs
            Inherits gloBaseCollection
            Implements IDisposable

            Public ReadOnly Property Item(ByVal index As Integer) As LabCSST
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), LabCSST)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.

            Public Sub Add(ByVal _LabCSST As LabCSST)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_LabCSST)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

#End Region


#Region "Comman Class: LOINcMST"
        ''Added by Mayuri:20130530-Order PRD changes done.

        Public Class LabLoincMst
            Implements IDisposable

            Private disposedValue As Boolean = False        ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: free unmanaged resources when explicitly called
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

            Public Sub New()
                MyBase.New()
            End Sub

#Region "private variables"
            Private mLabLoinc_ID As Int64 = 0
            Private mLabLoinc_Code As String = ""
            Private mLabLoinc_Name As String = ""



#End Region

#Region "properties"
            Public Property LabLoinc_ID() As Int64
                Get
                    Return mLabLoinc_ID
                End Get
                Set(ByVal value As Int64)
                    mLabLoinc_ID = value
                End Set
            End Property

            Public Property LabLoinc_Code() As String
                Get
                    Return mLabLoinc_Code
                End Get
                Set(ByVal value As String)
                    mLabLoinc_Code = value
                End Set
            End Property

            Public Property LabLoinc_Name() As String
                Get
                    Return mLabLoinc_Name
                End Get
                Set(ByVal value As String)
                    mLabLoinc_Name = value
                End Set
            End Property
#End Region

        End Class

        Public Class LabLoincMsts
            Inherits gloBaseCollection
            Implements IDisposable

            Public ReadOnly Property Item(ByVal index As Integer) As LabLoincMst
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), LabLoincMst)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.

            Public Sub Add(ByVal _LabLoincMst As LabLoincMst)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_LabLoincMst)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

#End Region
        '---Test Master---
        Public Class Test
            Private _TestID As Int64 = 0
            Private _Code As String = ""
            Private _Name As String = ""
            Private _Ordarable As Boolean = False
            Private _Specimen As String = ""
            Private _Collection As String = ""
            Private _Storage As String = ""
            Private _Instruction As String = ""
            Private _Precaution As String = ""
            Private _IsFinished As Boolean = False
            Private _LOINCID As String = ""
            'Private _AlternateResultCode As String = ""
            Private _DepartmentCategoryID As Integer = 0
            Private _TestHeadID As Integer = 0
            Private _ResultType As enumTestResultType = enumTestResultType.None
            Private _AbnormalFlag As String
            Private _Results As TestResults
            Private _PResults As PreferedLabResults
            Private _AssociatedEMField As String
            Private mSpecimenName As String = ""
            Private mCollectionName As String = ""
            Private mstorageName As String = ""
            Private mnClinicID As Int64 = 1
            Private _sLOINCLongName As String = ""
            Private _sCPTCode As String = ""
            Private _nTemplateID As Int64 = 0
            Private _sCPTDescription As String = ""
            Private _sMUReportingCategory As String = ""
            Private _sIsStructuredLabResult As String
            Private _bOutboundTransistion As Boolean = False
            Private _dtUpdatedDate As Date  ''new variable added for getting updated date
            Private bResults As Boolean = True
            Private pResults As Boolean = True

            Public Property TestID() As Int64
                Get
                    Return _TestID
                End Get
                Set(ByVal value As Int64)
                    _TestID = value
                End Set
            End Property

            Public Property Code() As String
                Get
                    Return _Code
                End Get
                Set(ByVal value As String)
                    _Code = value
                End Set
            End Property

            Public Property Name() As String
                Get
                    Return _Name
                End Get
                Set(ByVal value As String)
                    _Name = value
                End Set
            End Property

            Public Property Ordarable() As Boolean
                Get
                    Return _Ordarable
                End Get
                Set(ByVal value As Boolean)
                    _Ordarable = value
                End Set
            End Property

            Public Property Specimen() As String
                Get
                    Return _Specimen
                End Get
                Set(ByVal value As String)
                    _Specimen = value
                End Set
            End Property

            Public Property Collection() As String
                Get
                    Return _Collection
                End Get
                Set(ByVal value As String)
                    _Collection = value
                End Set
            End Property

            Public Property Storage() As String
                Get
                    Return _Storage
                End Get
                Set(ByVal value As String)
                    _Storage = value
                End Set
            End Property

            Public Property Instruction() As String
                Get
                    Return _Instruction
                End Get
                Set(ByVal value As String)
                    _Instruction = value
                End Set
            End Property

            Public Property Precaution() As String
                Get
                    Return _Precaution
                End Get
                Set(ByVal value As String)
                    _Precaution = value
                End Set
            End Property
            Public Property IsFinished() As Boolean
                Get
                    Return _IsFinished
                End Get
                Set(ByVal value As Boolean)
                    _IsFinished = value
                End Set
            End Property

            Public Property LOINCId() As String
                Get
                    Return _LOINCID
                End Get
                Set(ByVal value As String)
                    _LOINCID = value
                End Set
            End Property

            'Public Property AlternateResultCode() As String
            '    Get
            '        Return _AlternateResultCode
            '    End Get
            '    Set(ByVal value As String)
            '        _AlternateResultCode = value
            '    End Set
            'End Property

            Public Property DepartmentCategoryID() As Integer
                Get
                    Return _DepartmentCategoryID
                End Get
                Set(ByVal value As Integer)
                    _DepartmentCategoryID = value
                End Set
            End Property

            Public Property TestHeadID() As Integer
                Get
                    Return _TestHeadID
                End Get
                Set(ByVal value As Integer)
                    _TestHeadID = value
                End Set
            End Property

            Public Property ResultType() As enumTestResultType
                Get
                    Return _ResultType
                End Get
                Set(ByVal value As enumTestResultType)
                    _ResultType = value
                End Set
            End Property

            Public Property Results() As TestResults
                Get
                    Return _Results
                End Get
                Set(ByVal value As TestResults)
                    If (bResults) Then
                        If (IsNothing(_Results) = False) Then
                            _Results.Dispose()
                            _Results = Nothing
                        End If
                        bResults = False
                    End If
                    _Results = value
                End Set
            End Property

            Public Property PreferedResults() As PreferedLabResults
                Get
                    Return _PResults
                End Get
                Set(ByVal value As PreferedLabResults)
                    If (pResults) Then
                        If (IsNothing(_PResults) = False) Then
                            _PResults.Dispose()
                            _PResults = Nothing
                        End If
                        pResults = False
                    End If
                    _PResults = value
                End Set
            End Property
            Public Property AssociatedEMField() As String
                Get
                    Return _AssociatedEMField
                End Get
                Set(ByVal value As String)
                    _AssociatedEMField = value
                End Set
            End Property
            '\\ Lab Denormalization-20090318
            Public Property SpecimenName() As String
                Get
                    Return mSpecimenName
                End Get
                Set(ByVal value As String)
                    mSpecimenName = value
                End Set
            End Property
            Public Property CollectionName() As String
                Get
                    Return mCollectionName
                End Get
                Set(ByVal value As String)
                    mCollectionName = value
                End Set
            End Property
            Public Property StorageName() As String
                Get
                    Return mstorageName
                End Get
                Set(ByVal value As String)
                    mstorageName = value
                End Set
            End Property

            Public Property nClinicID() As Int64
                Get
                    Return mnClinicID
                End Get
                Set(ByVal value As Int64)
                    mnClinicID = value
                End Set
            End Property
            Public Property LOINCLongName() As String
                Get
                    Return _sLOINCLongName
                End Get
                Set(ByVal value As String)
                    _sLOINCLongName = value
                End Set
            End Property
            Public Property MUReportingCategory() As String
                Get
                    Return _sMUReportingCategory
                End Get
                Set(ByVal value As String)
                    _sMUReportingCategory = value
                End Set
            End Property

            Public Property IsStructuredLabTest() As String
                Get
                    Return _sIsStructuredLabResult
                End Get
                Set(ByVal value As String)
                    _sIsStructuredLabResult = value
                End Set
            End Property


            Public Property sCPTCode() As String
                Get
                    Return _sCPTCode
                End Get
                Set(ByVal value As String)
                    _sCPTCode = value
                End Set
            End Property
            Public Property nTemplateID() As Int64
                Get
                    Return _nTemplateID
                End Get
                Set(ByVal value As Int64)
                    _nTemplateID = value
                End Set
            End Property
            Public Property sCPTDescription() As String
                Get
                    Return _sCPTDescription
                End Get
                Set(ByVal value As String)
                    _sCPTDescription = value
                End Set
            End Property

            Public Property bOutboundTransistion() As Boolean
                Get
                    Return _bOutboundTransistion
                End Get
                Set(ByVal value As Boolean)
                    _bOutboundTransistion = value
                End Set
            End Property

            Public Property dtUpdatedDate() As Date ''new property added to getting updated date
                Get
                    Return _dtUpdatedDate
                End Get
                Set(ByVal value As Date)
                    _dtUpdatedDate = value
                End Set
            End Property
            Public Sub New()
                MyBase.New()
                _Results = New TestResults
                _PResults = New PreferedLabResults
            End Sub

            Protected Overrides Sub Finalize()
                If (bResults) Then


                    _Results = Nothing

                    bResults = False
                End If
                MyBase.Finalize()
            End Sub
            Public Sub Dispose()
                If (bResults) Then
                    If (IsNothing(_Results) = False) Then
                        _Results.Dispose()
                        _Results = Nothing
                    End If
                    bResults = False
                End If

                If (IsNothing(_PResults) = False) Then
                    _PResults.Dispose()
                    _PResults = Nothing
                End If
            End Sub
        End Class

        '---Test Collection---
        Public Class Tests

            Inherits gloBaseCollection
            Implements IDisposable

            ' This line declares the Item property as ReadOnly, and 
            ' declares that it will return a PatientInterface object.
            Public ReadOnly Property Item(ByVal index As Integer) As Test
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), Test)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _Test As Test)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_Test)
            End Sub


            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub

        End Class
        '--------------Prefered Lab Master---------- '
        Public Class PreferedLabResult
            Private _TestMstPreferredLabID As Int64 = 0
            Private _TestID As Int64 = 0
            Private _TLabCI_Id As Int64 = 0
            Private _sComments As String = ""
            Public Property TestMstPreferredLabID() As Int64
                Get
                    Return _TestMstPreferredLabID
                End Get
                Set(ByVal value As Int64)
                    _TestMstPreferredLabID = value
                End Set
            End Property
            Public Property TestID() As Int64
                Get
                    Return _TestID
                End Get
                Set(ByVal value As Int64)
                    _TestID = value
                End Set
            End Property
            Public Property TLabCI_Id() As Int64
                Get
                    Return _TLabCI_Id
                End Get
                Set(ByVal value As Int64)
                    _TLabCI_Id = value
                End Set
            End Property
            Public Property sComments() As String
                Get
                    Return _sComments
                End Get
                Set(ByVal value As String)
                    _sComments = value
                End Set
            End Property
            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class
        '--------------Prefered Lab Master---------- '

        '---Test Result Master---
        Public Class TestResult
            Private _TestID As Int64 = 0
            Private _ResultID As Integer = 0
            Private _ResultName As String = ""
            Private _ValueType As enumTestResultValueType = enumTestResultValueType.None
            Private _Unit As String = ""
            Private _DefaultValue As String = ""
            Private _ReferenceRange As String = ""
            Private _Comments As String = ""
            Private _Instruction As String = ""
            Private _BoundID As Integer = 0
            Private _BoundMaleLower As String = ""
            Private _BoundMaleUpper As String = ""
            Private _BoundFemaleLower As String = ""
            Private _BoundFemaleUpper As String = ""
            Private _LoincID As String = "" 'added by sagaarK on 10012008
            Private _AlternateResultCode As String


            Public Property TestID() As Int64
                Get
                    Return _TestID
                End Get
                Set(ByVal value As Int64)
                    _TestID = value
                End Set
            End Property

            Public Property ResultID() As Integer
                Get
                    Return _ResultID
                End Get
                Set(ByVal value As Integer)
                    _ResultID = value
                End Set
            End Property

            Public Property ResultName() As String
                Get
                    Return _ResultName
                End Get
                Set(ByVal value As String)
                    _ResultName = value
                End Set
            End Property

            Public Property ValueType() As enumTestResultValueType
                Get
                    Return _ValueType
                End Get
                Set(ByVal value As enumTestResultValueType)
                    _ValueType = value
                End Set
            End Property

            Public Property Unit() As String
                Get
                    Return _Unit
                End Get
                Set(ByVal value As String)
                    _Unit = value
                End Set
            End Property

            Public Property DefaultValue() As String
                Get
                    Return _DefaultValue
                End Get
                Set(ByVal value As String)
                    _DefaultValue = value
                End Set
            End Property

            Public Property ReferenceRange() As String
                Get
                    Return _ReferenceRange
                End Get
                Set(ByVal value As String)
                    _ReferenceRange = value
                End Set
            End Property

            Public Property Comments() As String
                Get
                    Return _Comments
                End Get
                Set(ByVal value As String)
                    _Comments = value
                End Set
            End Property

            Public Property Instruction() As String
                Get
                    Return _Instruction
                End Get
                Set(ByVal value As String)
                    _Instruction = value
                End Set
            End Property

            Public Property BoundID() As Integer
                Get
                    Return _BoundID
                End Get
                Set(ByVal value As Integer)
                    _BoundID = value
                End Set
            End Property

            Public Property BoundMaleLower() As String
                Get
                    Return _BoundMaleLower
                End Get
                Set(ByVal value As String)
                    _BoundMaleLower = value
                End Set
            End Property

            Public Property BoundMaleUpper() As String
                Get
                    Return _BoundMaleUpper
                End Get
                Set(ByVal value As String)
                    _BoundMaleUpper = value
                End Set
            End Property

            Public Property BoundFemaleLower() As String
                Get
                    Return _BoundFemaleLower
                End Get
                Set(ByVal value As String)
                    _BoundFemaleLower = value
                End Set
            End Property

            Public Property BoundFemaleUpper() As String
                Get
                    Return _BoundFemaleUpper
                End Get
                Set(ByVal value As String)
                    _BoundFemaleUpper = value
                End Set
            End Property

            'added by sagaarK on 10012008
            Public Property LoincID() As String
                Get
                    Return _LoincID
                End Get
                Set(ByVal value As String)
                    _LoincID = value
                End Set
            End Property

            Public Property AlternateResultCode() As String
                Get
                    Return _AlternateResultCode
                End Get
                Set(ByVal value As String)
                    _AlternateResultCode = value
                End Set
            End Property

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class
        '-----------Prefered Lab Result-------'
        Public Class PreferedLabResults
            Inherits gloBaseCollection
            Implements IDisposable

            ' This line declares the Item property as ReadOnly, and 
            ' declares that it will return a PatientInterface object.
            Public ReadOnly Property Item(ByVal index As Integer) As PreferedLabResult
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), PreferedLabResult)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _PreferedResult As PreferedLabResult)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_PreferedResult)
            End Sub


            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class
        '-----------Prefered Lab Result-------'

        '---Test Result Collection---
        Public Class TestResults
            Inherits gloBaseCollection
            Implements IDisposable

            ' This line declares the Item property as ReadOnly, and 
            ' declares that it will return a PatientInterface object.
            Public ReadOnly Property Item(ByVal index As Integer) As TestResult
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), TestResult)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _TestResult As TestResult)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_TestResult)
            End Sub


            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub

        End Class

        Public Class LabSpecimen
            Implements IDisposable

            Private disposedValue As Boolean = False        ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: free unmanaged resources when explicitly called
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

            Public Sub New()
                MyBase.New()
            End Sub

#Region "private variables"
            Private _intlabsm_ID As Int64 = 0
            Private _strlabsm_Code As String = ""
            Private _strlabsm_Name As String = ""
#End Region

#Region "properties"
            Public Property LabSpecimenID() As Int64
                Get
                    Return _intlabsm_ID
                End Get
                Set(ByVal value As Int64)
                    _intlabsm_ID = value
                End Set
            End Property

            Public Property LabSpecimenCode() As String
                Get
                    Return _strlabsm_Code
                End Get
                Set(ByVal value As String)
                    _strlabsm_Code = value
                End Set
            End Property

            Public Property LabSpecimenName() As String
                Get
                    Return _strlabsm_Name
                End Get
                Set(ByVal value As String)
                    _strlabsm_Name = value
                End Set
            End Property
#End Region

        End Class

        Public Class LabSpecimens
            Inherits gloBaseCollection
            Implements IDisposable

            Public ReadOnly Property Item(ByVal index As Integer) As LabSpecimen
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), LabSpecimen)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.

            Public Sub Add(ByVal _LabSpecimen As LabSpecimen)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_LabSpecimen)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class LabGroup
            Implements IDisposable

            Private disposedValue As Boolean = False        ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        If (bTests) Then
                            If (IsNothing(_Tests) = False) Then
                                _Tests.Dispose()
                                _Tests = Nothing
                            End If
                            bTests = False
                        End If
                        ' TODO: free unmanaged resources when explicitly called
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

            Public Sub New()
                MyBase.New()
                _Tests = New Tests
            End Sub

#Region "private variables"
            Private _intlabgm_ID As Int64 = 0
            Private _strlabgm_GroupCode As String = ""
            Private _strlabgm_GroupName As String = ""

            'private var for detail table collection
            Private _Tests As Tests
            Private bTests As Boolean = True

#End Region

#Region "properties"
            Public Property LabGroupID() As Int64
                Get
                    Return _intlabgm_ID
                End Get
                Set(ByVal value As Int64)
                    _intlabgm_ID = value
                End Set
            End Property

            Public Property LabGroupCode() As String
                Get
                    Return _strlabgm_GroupCode
                End Get
                Set(ByVal value As String)
                    _strlabgm_GroupCode = value
                End Set
            End Property

            Public Property LabGroupName() As String
                Get
                    Return _strlabgm_GroupName
                End Get
                Set(ByVal value As String)
                    _strlabgm_GroupName = value
                End Set
            End Property

            Public Property Tests() As Tests
                Get
                    Return _Tests
                End Get
                Set(ByVal value As Tests)
                    If (bTests) Then
                        If (IsNothing(_Tests) = False) Then
                            _Tests.Dispose()
                            _Tests = Nothing
                        End If
                        bTests = False
                    End If
                    _Tests = value
                End Set
            End Property
#End Region
        End Class

        Public Class LabGroups
            Inherits gloBaseCollection
            Implements IDisposable

            Public ReadOnly Property Item(ByVal index As Integer) As LabGroup
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), LabGroup)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _LabGroup As LabGroup)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_LabGroup)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class LabCollectionContainer
            Implements IDisposable

            Private disposedValue As Boolean = False        ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: free unmanaged resources when explicitly called
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

            Public Sub New()
                MyBase.New()
            End Sub

#Region "private variables"
            Private _intlabcm_ID As Int64 = 0
            Private _strlabcm_Code As String = ""
            Private _strlabcm_Name As String = ""
#End Region

#Region "properties"
            Public Property LabCollectionContainerID() As Int64
                Get
                    Return _intlabcm_ID
                End Get
                Set(ByVal value As Int64)
                    _intlabcm_ID = value
                End Set
            End Property

            Public Property LabCollectionContainerCode() As String
                Get
                    Return _strlabcm_Code
                End Get
                Set(ByVal value As String)
                    _strlabcm_Code = value
                End Set
            End Property

            Public Property LabCollectionContainerName() As String
                Get
                    Return _strlabcm_Name
                End Get
                Set(ByVal value As String)
                    _strlabcm_Name = value
                End Set
            End Property
#End Region

        End Class

        Public Class LabCollectionContainers
            Inherits gloBaseCollection
            Implements IDisposable


            Public ReadOnly Property Item(ByVal index As Integer) As LabCollectionContainer
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), LabCollectionContainer)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _LabCollectionContainer As LabCollectionContainer)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_LabCollectionContainer)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class LabStorageTemperature
            Implements IDisposable

            Private disposedValue As Boolean = False        ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: free unmanaged resources when explicitly called
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

            Public Sub New()
                MyBase.New()
            End Sub

#Region "private variables"
            Private _intlabstm_ID As Int64 = 0
            Private _strlabstm_Code As String = ""
            Private _strlabstm_Name As String = ""
#End Region

#Region "properties"
            Public Property LabStorageTemperatureID() As Int64
                Get
                    Return _intlabstm_ID
                End Get
                Set(ByVal value As Int64)
                    _intlabstm_ID = value
                End Set
            End Property

            Public Property LabStorageTemperatureCode() As String
                Get
                    Return _strlabstm_Code
                End Get
                Set(ByVal value As String)
                    _strlabstm_Code = value
                End Set
            End Property

            Public Property LabStorageTemperatureName() As String
                Get
                    Return _strlabstm_Name
                End Get
                Set(ByVal value As String)
                    _strlabstm_Name = value
                End Set
            End Property
#End Region

        End Class

        Public Class LabStorageTemperatures
            Inherits gloBaseCollection
            Implements IDisposable

            Public ReadOnly Property Item(ByVal index As Integer) As LabStorageTemperature
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), LabStorageTemperature)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _LabStorageTemperature As LabStorageTemperature)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_LabStorageTemperature)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class LabContactInformation
            Implements IDisposable

            Private disposedValue As Boolean = False        ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: free unmanaged resources when explicitly called
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

#Region "private variables"
            Private _intID As Int64 = 0
            Private _strContactName As String = ""
            Private _strFirstName As String = ""
            Private _strMiddleName As String = ""
            Private _strLastName As String = ""
            Private _intFlag As enumContactType

            Private _address1 As String = ""
            Private _address2 As String = ""
            Private _zip As String = ""
            Private _city As String = ""
            Private _country As String = ""
            Private _state As String = ""
            Private _county As String = ""
            Private _sPhone As String = ""

#End Region

#Region "properties"
            Public Property ContactID() As Int64
                Get
                    Return _intID
                End Get
                Set(ByVal value As Int64)
                    _intID = value
                End Set
            End Property

            Public Property ContactName() As String
                Get
                    Return _strContactName
                End Get
                Set(ByVal value As String)
                    _strContactName = value
                End Set
            End Property

            Public Property FirstName() As String
                Get
                    Return _strFirstName
                End Get
                Set(ByVal value As String)
                    _strFirstName = value
                End Set
            End Property

            Public Property MiddleName() As String
                Get
                    Return _strMiddleName
                End Get
                Set(ByVal value As String)
                    _strMiddleName = value
                End Set
            End Property

            Public Property LastName() As String
                Get
                    Return _strLastName
                End Get
                Set(ByVal value As String)
                    _strLastName = value
                End Set
            End Property

            Public Property Type() As enumContactType
                Get
                    Return _intFlag
                End Get
                Set(ByVal value As enumContactType)
                    _intFlag = value
                End Set
            End Property

            Public Property Address1() As String
                Get
                    Return _address1
                End Get
                Set(ByVal value As String)
                    _address1 = value
                End Set
            End Property
            Public Property Address2() As String
                Get
                    Return _address2
                End Get
                Set(ByVal value As String)
                    _address2 = value
                End Set
            End Property
            Public Property Zip() As String
                Get
                    Return _zip
                End Get
                Set(ByVal value As String)
                    _zip = value
                End Set
            End Property
            Public Property City() As String
                Get
                    Return _city
                End Get
                Set(ByVal value As String)
                    _city = value
                End Set
            End Property
            Public Property Country() As String
                Get
                    Return _country
                End Get
                Set(ByVal value As String)
                    _country = value
                End Set
            End Property
            Public Property State() As String
                Get
                    Return _state
                End Get
                Set(ByVal value As String)
                    _state = value
                End Set
            End Property
            Public Property County() As String
                Get
                    Return _county
                End Get
                Set(ByVal value As String)
                    _county = value
                End Set
            End Property

            Public Property Phone() As String
                Get
                    Return _sPhone
                End Get
                Set(ByVal value As String)
                    _sPhone = value
                End Set
            End Property
#End Region

            Public Sub New()
                MyBase.New()
            End Sub
        End Class

        Public Class LabContactInformations
            Inherits gloBaseCollection
            Implements IDisposable


            Public ReadOnly Property Item(ByVal index As Integer) As LabContactInformation
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), LabContactInformation)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _LabContactInformation As LabContactInformation)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_LabContactInformation)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        'Lab Order

        Public Class LabOrder

            Private _OrderID As Int64
            Private _OrderNoPrefix As String
            Private _DiagnosisCount As Int64
            Private _OrderNoID As Int64
            Private _TransactionDate As Date
            Private _PatientID As Int64
            Private _PatientAge As AgeDetail
            Private _Provider As String
            Private _ProviderID As Int64 = 0
            Private _PreferredLab As String
            Private _PreferredLabID As Int64
            Private _SampledBy As String
            Private _SampledByID As Int64
            Private _ReferredBy As String
            Private _ReferredByID As Int64
            Private _Users As ItemDetails
            Private _OrderTests As OrderTests
            Private _VisitID As Int64 = 0
            Private _ExternalCode As String = ""
            Private _DMSID As Int64 = 0
            Private _TaskDescription As String = ""
            Private _TaskDueDate As Date
            Private bPatientAge As Boolean = True
            Private bUsers As Boolean = True
            Private bOrderTestss As Boolean = True
            Private _ArrTestName As ArrayList
            Private _ReferredByFName As String = ""
            Private _ReferredByMName As String = ""
            Private _ReferredByLName As String = ""
            Private _ClinicID As Int64 = 0

            Private _blnOrderNotCPOE As Boolean = False
            Private _OrderStatusNumber As Integer = 0

            '29-Sep-2014 Aniket: Insert the Order Creator User for MU Stage 2 Report
            Private _OrderCreatorUser As String
            Private _bOutboundTransistion As Boolean = False
            Private _FastingStatus As String = ""

            '29-Sep-2014 Aniket: Insert the Order Creator User for MU Stage 2 Report
            Public Property OrderCreatorUser() As String
                Get
                    Return _OrderCreatorUser
                End Get
                Set(ByVal value As String)
                    _OrderCreatorUser = value
                End Set
            End Property

            Public Property OrderID() As Int64
                Get
                    Return _OrderID
                End Get
                Set(ByVal value As Int64)
                    _OrderID = value
                End Set
            End Property

            Public Property OrderNoPrefix() As String
                Get
                    Return _OrderNoPrefix
                End Get
                Set(ByVal value As String)
                    _OrderNoPrefix = value
                End Set
            End Property
            Public Property DiagnosisCount() As Int64
                Get
                    Return _DiagnosisCount
                End Get
                Set(ByVal value As Int64)
                    _DiagnosisCount = value
                End Set
            End Property

            'Developer:Sanjog Dhamke
            'Date: 12/07/2011
            'Bug ID/PRD Name/Sales force Case: SF Case = GLO2011-0015430 - Lab Results Not Displaying Properly
            'Reason: OrderNoID is converted to int16 format but actual OrderNoID is bigger than this so it cause the exception and the lab result is not displaying properly. Now we make this as int64 data type.
            Public Property OrderNoID() As Int64
                Get
                    Return _OrderNoID
                End Get
                Set(ByVal value As Int64)
                    _OrderNoID = value
                End Set
            End Property

            Public Property TransactionDate() As Date
                Get
                    Return _TransactionDate
                End Get
                Set(ByVal value As Date)
                    _TransactionDate = value
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

            Public Property PatientAge() As AgeDetail
                Get
                    Return _PatientAge
                End Get
                Set(ByVal value As AgeDetail)
                    If (bPatientAge) Then
                        _PatientAge = Nothing
                        bPatientAge = False
                    End If
                    _PatientAge = value
                End Set
            End Property

            Public Property Provider() As String
                Get
                    Return _Provider
                End Get
                Set(ByVal value As String)
                    _Provider = value
                End Set
            End Property

            Public Property ProviderID() As Int64
                Get
                    Return _ProviderID
                End Get
                Set(ByVal value As Int64)
                    _ProviderID = value
                End Set
            End Property

            Public Property PreferredLab() As String
                Get
                    Return _PreferredLab
                End Get
                Set(ByVal value As String)
                    _PreferredLab = value
                End Set
            End Property

            Public Property PreferredLabID() As Int64
                Get
                    Return _PreferredLabID
                End Get
                Set(ByVal value As Int64)
                    _PreferredLabID = value
                End Set
            End Property

            Public Property SampledBy() As String
                Get
                    Return _SampledBy
                End Get
                Set(ByVal value As String)
                    _SampledBy = value
                End Set
            End Property

            Public Property SampledByID() As Int64
                Get
                    Return _SampledByID
                End Get
                Set(ByVal value As Int64)
                    _SampledByID = value
                End Set
            End Property

            Public Property ReferredBy() As String
                Get
                    Return _ReferredBy
                End Get
                Set(ByVal value As String)
                    _ReferredBy = value
                End Set
            End Property

            Public Property ReferredByID() As Int64
                Get
                    Return _ReferredByID
                End Get
                Set(ByVal value As Int64)
                    _ReferredByID = value
                End Set
            End Property

            Public Property Users() As ItemDetails
                Get
                    Return _Users
                End Get
                Set(ByVal value As ItemDetails)
                    If (bUsers) Then
                        If (IsNothing(_Users) = False) Then
                            _Users.Dispose()
                            _Users = Nothing
                        End If
                        bUsers = False
                    End If
                    _Users = value
                End Set
            End Property

            Public Property OrderTests() As OrderTests
                Get
                    Return _OrderTests
                End Get
                Set(ByVal value As OrderTests)
                    If (bOrderTestss) Then
                        If (IsNothing(_OrderTests) = False) Then
                            _OrderTests.Dispose()
                            _OrderTests = Nothing
                        End If
                        bOrderTestss = False
                    End If
                    _OrderTests = value
                End Set
            End Property

            ''20070529
            Public Property VisitID() As Int64
                Get
                    Return _VisitID
                End Get
                Set(ByVal value As Int64)
                    _VisitID = value
                End Set
            End Property

            ''20070602
            Public Property ExternalCode() As String
                Get
                    Return _ExternalCode
                End Get
                Set(ByVal value As String)
                    _ExternalCode = value
                End Set
            End Property

            '' 20071121
            Public Property TaskDescription() As String
                Get
                    Return _TaskDescription
                End Get
                Set(ByVal value As String)
                    _TaskDescription = value
                End Set
            End Property

            Public Property TaskDueDate() As DateTime
                Get
                    Return _TaskDueDate
                End Get
                Set(ByVal value As DateTime)
                    _TaskDueDate = value
                End Set
            End Property
            ''

            Public Property DMSID() As Int64
                Get
                    Return _DMSID
                End Get
                Set(ByVal value As Int64)
                    _DMSID = value
                End Set
            End Property




            Public Property ArrTestName() As ArrayList
                Get
                    Return _ArrTestName
                End Get
                Set(ByVal value As ArrayList)
                    _ArrTestName = value
                End Set
            End Property



            Public Property ReferredByFName() As String
                Get
                    Return _ReferredByFName
                End Get
                Set(ByVal value As String)
                    _ReferredByFName = value
                End Set
            End Property

            Public Property ReferredByMName() As String
                Get
                    Return _ReferredByMName
                End Get
                Set(ByVal value As String)
                    _ReferredByMName = value
                End Set
            End Property

            Public Property ReferredByLName() As String
                Get
                    Return _ReferredByLName
                End Get
                Set(ByVal value As String)
                    _ReferredByLName = value
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

            Public Property blnOrderNotCPOE() As Boolean
                Get
                    Return _blnOrderNotCPOE
                End Get
                Set(ByVal value As Boolean)
                    _blnOrderNotCPOE = value
                End Set
            End Property


            Public Property bOutboundTransistion() As Boolean
                Get
                    Return _bOutboundTransistion
                End Get
                Set(ByVal value As Boolean)
                    _bOutboundTransistion = value
                End Set
            End Property


            Public Property FastingStatus() As String
                Get
                    Return _FastingStatus
                End Get
                Set(ByVal value As String)
                    _FastingStatus = value
                End Set
            End Property

            Public Property OrderStatusNumber() As Integer
                Get
                    Return _OrderStatusNumber
                End Get
                Set(ByVal value As Integer)
                    _OrderStatusNumber = value
                End Set
            End Property

            Private _SendTo As Int32

            Public Property SendTo() As Int32
                Get
                    Return _SendTo
                End Get
                Set(ByVal value As Int32)
                    _SendTo = value
                End Set
            End Property


            Private _ReferredTo As String

            Public Property ReferredTo() As String
                Get
                    Return _ReferredTo
                End Get
                Set(ByVal value As String)
                    _ReferredTo = value
                End Set
            End Property

            Private _ReferredToID As Int64
            Public Property ReferredToID() As Int64
                Get
                    Return _ReferredToID
                End Get
                Set(ByVal value As Int64)
                    _ReferredToID = value
                End Set
            End Property



            '----

            Public Sub New()
                MyBase.New()
                _PatientAge = New AgeDetail
                _Users = New ItemDetails
                _OrderTests = New OrderTests
            End Sub
            Public Sub Dispose()
                If (bPatientAge) Then
                    _PatientAge = Nothing
                    bPatientAge = False
                End If
                If (bUsers) Then
                    If (IsNothing(_Users) = False) Then
                        _Users.Dispose()
                        _Users = Nothing
                    End If
                    bUsers = False
                End If
                If (bOrderTestss) Then
                    If (IsNothing(_OrderTests) = False) Then
                        _OrderTests.Dispose()
                        _OrderTests = Nothing
                    End If
                    bOrderTestss = False
                End If

            End Sub
            Protected Overrides Sub Finalize()
                If (bPatientAge) Then
                    _PatientAge = Nothing
                    bPatientAge = False
                End If
                If (bUsers) Then
                    _Users = Nothing

                    bUsers = False
                End If
                If (bOrderTestss) Then
                    _OrderTests = Nothing

                    bOrderTestss = False
                End If

                MyBase.Finalize()
            End Sub

        End Class

        Public Class LabOrders

            Inherits gloBaseCollection
            Implements IDisposable


            Public ReadOnly Property Item(ByVal index As Integer) As LabOrder
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), LabOrder)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _LabOrder As LabOrder)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_LabOrder)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class OrderTest
#Region "private variables"
            Private _OrderID As Int64 = 0
            Private _TestID As Int64 = 0
            Private _TestName As String = ""
            Private _TestLineNo As Integer = 0

            Private _Diagonosis As ItemDetails
            Private _Treatments As ItemDetails
            Private _Note As String = ""
            Private _Specimen As String = ""
            Private _Collection As String = ""
            Private _Storage As String = ""
            ''Sandip Darade 20090403
            '' _LOINCCode as string 
            '' Private _LOINCCode As Int64 = 0
            Private _LOINCCode As String = ""
            Private _CPT As String = ""
            Private _Instruction As String = ""
            Private _Precaution As String = ""
            Private _IsFinished As Boolean = False
            Private _Datetime As DateTime
            Private _TestSpecimenCollectionDateTime As DateTime
            Private _ScheduleDateTime As DateTime
            Private _ReportedDatetime As DateTime

            Private _UserID As Int64 = 0
            Private _Comments As String = ""
            Private _OrderTestResults As OrderTestResults
            Private _DMSID As Int64 = 0

            Private _DMSIDCollection As String = ""

            Private _LabURLDocument As DataTable

            ' '\\ Lab Denormalization 20090318
            Private mSpecimenName As String = ""
            Private mCollectionName As String = ""
            Private mStorageName As String = ""
            ''\\ Lab Dicomid--Added by madan on 20101007
            Private _DicomId As Int64 = 0
            ''\\ End madan Changes

            ''Added by Abhijeet on 20101026
            Private _TestStatus As String = ""
            Private _SpecimenSource As String = ""
            Private _SpecimenConditionDisp As String = ""
            Private _TestCode As String = ""
            Private _TestType As String = ""

            Private _Conceptid As String = ""
            Private _Valsetid As String = ""
            Private _Valsetname As String = ""
            Private _IDDescription As String = ""

            ''End of changes by Abhijeet on 20101026

            'Added  By Yatin ::20130530
            Private _TestTemplate As Object

            Private bDiagonosis As Boolean = True
            Private bTreatments As Boolean = True
            Private bOrderTestResults As Boolean = True

            Private _TestPreferredLab As String = ""
            Private _TestPreferredLabID As Int64 = 0
#End Region

#Region "properties"

            ''Added by Abhijeet on 20101026
            Public Property TestType() As String
                Get
                    Return _TestType
                End Get
                Set(ByVal value As String)
                    _TestType = value
                End Set
            End Property
            Public Property TestStatus() As String
                Get
                    Return _TestStatus
                End Get
                Set(ByVal value As String)
                    _TestStatus = value
                End Set
            End Property

            'Added  By Yatin for Word Template::20130530
            Public Property TestTemplate() As Object
                Get
                    Return _TestTemplate
                End Get
                Set(ByVal value As Object)
                    _TestTemplate = value
                End Set
            End Property

            Public Property SpecimenSource() As String
                Get
                    Return _SpecimenSource
                End Get
                Set(ByVal value As String)
                    _SpecimenSource = value
                End Set
            End Property
            Public Property SpecimenConditionDisp() As String
                Get
                    Return _SpecimenConditionDisp
                End Get
                Set(ByVal value As String)
                    _SpecimenConditionDisp = value
                End Set
            End Property
            Public Property TestCode() As String
                Get
                    Return _TestCode
                End Get
                Set(ByVal value As String)
                    _TestCode = value
                End Set
            End Property
            ''End of changes by Abhijet on 20101026


            Public Property OrderTestResults() As OrderTestResults
                Get
                    Return _OrderTestResults
                End Get
                Set(ByVal value As OrderTestResults)
                    If (bOrderTestResults) Then
                        If (IsNothing(_OrderTestResults) = False) Then
                            _OrderTestResults.Dispose()
                            _OrderTestResults = Nothing
                        End If
                        bOrderTestResults = False
                    End If
                    _OrderTestResults = value
                End Set
            End Property

            Public Property OrderID() As Int64
                Get
                    Return _OrderID
                End Get
                Set(ByVal value As Int64)
                    _OrderID = value
                End Set
            End Property

            Public Property TestID() As Int64
                Get
                    Return _TestID
                End Get
                Set(ByVal value As Int64)
                    _TestID = value
                End Set
            End Property

            Public Property TestName() As String
                Get
                    Return _TestName
                End Get
                Set(ByVal value As String)
                    _TestName = value
                End Set
            End Property

            Public Property TestLineNo() As Integer
                Get
                    Return _TestLineNo
                End Get
                Set(ByVal value As Integer)
                    _TestLineNo = value
                End Set
            End Property

            Public Property Diagonosis() As ItemDetails
                Get
                    Return _Diagonosis
                End Get
                Set(ByVal value As ItemDetails)
                    If (bDiagonosis) Then
                        If (IsNothing(_Diagonosis) = False) Then
                            _Diagonosis.Dispose()
                            _Diagonosis = Nothing
                        End If
                        bDiagonosis = False
                    End If
                    _Diagonosis = value
                End Set
            End Property


            Public Property Treatments() As ItemDetails
                Get
                    Return _Treatments
                End Get
                Set(ByVal value As ItemDetails)
                    If (bTreatments) Then
                        If (IsNothing(_Treatments) = False) Then
                            _Treatments.Dispose()
                            _Treatments = Nothing
                        End If
                        bTreatments = False
                    End If
                    _Treatments = value
                End Set
            End Property
            Public Property Note() As String
                Get
                    Return _Note
                End Get
                Set(ByVal value As String)
                    _Note = value
                End Set
            End Property

            Public Property Specimen() As String
                Get
                    Return _Specimen
                End Get
                Set(ByVal value As String)
                    _Specimen = value
                End Set
            End Property

            Public Property Collection() As String
                Get
                    Return _Collection
                End Get
                Set(ByVal value As String)
                    _Collection = value
                End Set
            End Property


            Public Property Storage() As String
                Get
                    Return _Storage
                End Get
                Set(ByVal value As String)
                    _Storage = value
                End Set
            End Property
            ''Sandip Darade 20090403
            ''LOINCCode as string
            'Public Property LOINCCode() As Int64
            '    Get
            '        Return _LOINCCode
            '    End Get
            '    Set(ByVal value As Int64)
            '        _LOINCCode = value
            '    End Set
            'End Property

            Public Property LOINCCode() As String
                Get
                    Return _LOINCCode
                End Get
                Set(ByVal value As String)
                    _LOINCCode = value
                End Set
            End Property
            Public Property CPT() As String
                Get
                    Return _CPT
                End Get
                Set(ByVal value As String)
                    _CPT = value
                End Set
            End Property

            Public Property Instruction() As String
                Get
                    Return _Instruction
                End Get
                Set(ByVal value As String)
                    _Instruction = value
                End Set
            End Property

            Public Property Precaution() As String
                Get
                    Return _Precaution
                End Get
                Set(ByVal value As String)
                    _Precaution = value
                End Set
            End Property
            Public Property Is_Finished() As Boolean
                Get
                    Return _IsFinished
                End Get
                Set(ByVal value As Boolean)

                    _IsFinished = value
                End Set
            End Property
            Public Property TestDateTime() As DateTime
                Get
                    Return _Datetime
                End Get
                Set(ByVal value As DateTime)
                    _Datetime = value
                End Set
            End Property

            Public Property ScheduleDateTime() As DateTime
                Get
                    Return _ScheduleDateTime
                End Get
                Set(ByVal value As DateTime)
                    _ScheduleDateTime = value
                End Set
            End Property

            Public Property TestSpecimenCollectionDateTime() As DateTime
                Get
                    Return _TestSpecimenCollectionDateTime
                End Get
                Set(ByVal value As DateTime)
                    _TestSpecimenCollectionDateTime = value
                End Set
            End Property

            Public Property ReportedDateTime() As DateTime
                Get
                    Return _ReportedDatetime
                End Get
                Set(ByVal value As DateTime)
                    _ReportedDatetime = value
                End Set
            End Property

            Public Property UserID() As Int64
                Get
                    Return _UserID
                End Get
                Set(ByVal value As Int64)
                    _UserID = value
                End Set
            End Property

            Public Property Comments() As String
                Get
                    Return _Comments
                End Get
                Set(ByVal value As String)
                    _Comments = value
                End Set
            End Property

            Public Property DMSID() As Int64
                Get
                    Return _DMSID
                End Get
                Set(ByVal value As Int64)
                    _DMSID = value
                End Set
            End Property
            Public Property DMSIDCollection() As String
                Get
                    Return _DMSIDCollection
                End Get
                Set(ByVal value As String)
                    _DMSIDCollection = value
                End Set
            End Property

            Public Property LabURLDocument() As DataTable
                Get
                    Return _LabURLDocument
                End Get
                Set(ByVal value As DataTable)
                    _LabURLDocument = value
                End Set
            End Property

            '\\ Lab Denormalization
            Public Property SpecimenName() As String
                Get
                    Return mSpecimenName
                End Get
                Set(ByVal value As String)
                    mSpecimenName = value
                End Set
            End Property

            Public Property CollectionName() As String
                Get
                    Return mCollectionName
                End Get
                Set(ByVal value As String)
                    mCollectionName = value
                End Set
            End Property

            Public Property StorageName() As String
                Get
                    Return mStorageName
                End Get
                Set(ByVal value As String)
                    mStorageName = value
                End Set
            End Property
            '//----------
            '/** Added by madan on 20101007 ** /'

            Public Property DicomID() As Int64
                Get
                    Return _DicomId
                End Get
                Set(ByVal value As Int64)
                    _DicomId = value
                End Set
            End Property

            Public Property ConceptID() As String
                Get
                    Return _Conceptid
                End Get
                Set(ByVal value As String)
                    _Conceptid = value
                End Set
            End Property

            Public Property ConceptIDDescription() As String
                Get
                    Return _IDDescription
                End Get
                Set(ByVal value As String)
                    _IDDescription = value
                End Set
            End Property
            Public Property ValueSetID() As String
                Get
                    Return _Valsetid
                End Get
                Set(ByVal value As String)
                    _Valsetid = value
                End Set
            End Property
            Public Property ValueSetName() As String
                Get
                    Return _Valsetname

                End Get
                Set(ByVal value As String)
                    _Valsetname = value
                End Set
            End Property

            '/*** End madan Changes ***/'

            Public Property SpecimenTypeIdentifier() As String = ""
            Public Property SpecimenTypeText() As String = ""
            Public Property SpecimenTypeCodingSystem() As String = ""
            Public Property SpecimenCollectionStartDateTime() As DateTime
            Public Property SpecimenRejectReason() As String = ""
            Public Property SpecimenCondition() As String = ""
            Public Property SpecimenActionCode() As String = ""
            Public Property TestScheduledEndDateTime() As DateTime
            Public Property labotd_DateTimeUTC() As Integer = 0
            Public Property labotd_TestScheduledDateTimeUTC() As Integer = 0
            Public Property labotd_TestScheduledEndDateTimeUTC() As Integer = 0
            Public Property labotd_SpecimenCollectionStartDateTimeUTC() As Integer = 0

            Public Property TestPreferredLab() As String
                Get
                    Return _TestPreferredLab
                End Get
                Set(ByVal value As String)
                    _TestPreferredLab = value
                End Set
            End Property

            Public Property TestPreferredLabID() As Int64
                Get
                    Return _TestPreferredLabID
                End Get
                Set(ByVal value As Int64)
                    _TestPreferredLabID = value
                End Set
            End Property

#End Region

            Public Sub INUP_Test_Attachment(_PatientID As Int64, _OrderID As Int64, _TestID As Int64, _DMSCollection As String)
                Dim _gloEMRDatabase As DataBaseLayer
                _gloEMRDatabase = New DataBaseLayer
                Dim objDBParameter As DBParameter

                'Dim oDBLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
                'Dim oDBParameters As New gloDatabaseLayer.DBParameters()
                Try

                    'oDBLayer.Connect(False)

                    'oDBParameters.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    'oDBParameters.Add("@OrderID", _OrderID, ParameterDirection.Input, SqlDbType.BigInt)
                    'oDBParameters.Add("@TestID", _TestID, ParameterDirection.Input, SqlDbType.BigInt)
                    'oDBParameters.Add("@DocumentID", _DMSCollection, ParameterDirection.Input, SqlDbType.VarChar)

                    'oDBLayer.ExecuteScalar("Lab_INUP_Test_Attachment", oDBParameters)

                    'oDBParameters = Nothing

                    _gloEMRDatabase.DBParametersCol.Clear()

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = _PatientID
                    objDBParameter.Name = "@PatientID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = _OrderID
                    objDBParameter.Name = "@OrderID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = _TestID
                    objDBParameter.Name = "@TestID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _DMSCollection
                    objDBParameter.Name = "@DocumentID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    _gloEMRDatabase.Add("Lab_INUP_Test_Attachment")


                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                Finally
                    If _gloEMRDatabase IsNot Nothing Then
                        _gloEMRDatabase.Dispose()
                    End If
                End Try

            End Sub

            Public Sub INUP_Test_URLdocument(_PatientID As Int64, _OrderID As Int64, dtURLdocument As DataTable)
                Dim _gloEMRDatabase As DataBaseLayer
                _gloEMRDatabase = New DataBaseLayer
                Dim objDBParameter As DBParameter

                Try

                    _gloEMRDatabase.DBParametersCol.Clear()

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = _PatientID
                    objDBParameter.Name = "@PatientID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = _OrderID
                    objDBParameter.Name = "@OrderID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.Structured
                    objDBParameter.Value = dtURLdocument
                    objDBParameter.Name = "@TVP_Lab_URLdocument"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    _gloEMRDatabase.Add("Lab_INUP_Test_URLdocument")


                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                Finally
                    If _gloEMRDatabase IsNot Nothing Then
                        _gloEMRDatabase.Dispose()
                    End If
                End Try

            End Sub


            Public Sub New()
                MyBase.New()
                _OrderTestResults = New OrderTestResults
                _Diagonosis = New ItemDetails
                _Treatments = New ItemDetails
            End Sub
            Public Sub Dispose()
                If (bOrderTestResults) Then
                    If (IsNothing(_OrderTestResults) = False) Then
                        _OrderTestResults.Dispose()
                        _OrderTestResults = Nothing
                    End If
                    bOrderTestResults = False
                End If
                If (bDiagonosis) Then
                    If (IsNothing(_Diagonosis) = False) Then
                        _Diagonosis.Dispose()
                        _Diagonosis = Nothing
                    End If
                    bDiagonosis = False
                End If
                If (bTreatments) Then
                    If (IsNothing(_Treatments) = False) Then
                        _Treatments.Dispose()
                        _Treatments = Nothing
                    End If
                    bTreatments = False
                End If

            End Sub

            Protected Overrides Sub Finalize()
                If (bOrderTestResults) Then
                    _OrderTestResults = Nothing

                    bOrderTestResults = False
                End If
                If (bDiagonosis) Then
                    _Diagonosis = Nothing

                    bDiagonosis = False
                End If
                If (bTreatments) Then
                    _Treatments = Nothing

                    bTreatments = False
                End If
                MyBase.Finalize()
            End Sub
        End Class

        Public Class OrderTests

            Inherits gloBaseCollection
            Implements IDisposable


            Public ReadOnly Property Item(ByVal index As Integer) As OrderTest
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), OrderTest)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _OrderTest As OrderTest)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_OrderTest)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class OrderTestResult
#Region "private variables"
            Private _OrderID As Int64 = 0
            Private _TestID As Int64 = 0
            Private _TestName As String = ""
            Private _TestResultNumber As Int64 = 0
            Private _TestResultName As String = ""
            Private _TestResultDateTime As DateTime = Now
            Private _TestResultDetails As OrderTestResultDetails
            Private bTestResultDetails As Boolean = True
            Private _IsFinished As Int16 = 0
            Private _DMSID As Int64 = 0
            Private _DMSIDCollection As String = ""

            Private _SpecimenReceivedDateTime As DateTime = Now
            Private _ResultTransferDateTime As DateTime = Now
            Private _ReportedDatetime As DateTime
            Private _AlternateTestName As String = ""
            Private _AlternateTestCode As String = ""
            Private _blnSpecimenReceivedDateTime As Boolean = False
            Private _blnResultTransferDateTime As Boolean = False

            Private _TestResultDateTimeUTC As Integer = 0
            Private _SpecimenReceivedDateTimeUTC As Integer = 0
            Private _ResultTransferDateTimeUTC As Integer = 0


#End Region

            Public Property OrderID() As Int64
                Get
                    Return _OrderID
                End Get
                Set(ByVal value As Int64)
                    _OrderID = value
                End Set
            End Property

            Public Property TestID() As Int64
                Get
                    Return _TestID
                End Get
                Set(ByVal value As Int64)
                    _TestID = value
                End Set
            End Property

            Public Property TestName() As String
                Get
                    Return _TestName
                End Get
                Set(ByVal value As String)
                    _TestName = value
                End Set
            End Property

            Public Property TestResultNumber() As Int64
                Get
                    Return _TestResultNumber
                End Get
                Set(ByVal value As Int64)
                    _TestResultNumber = value
                End Set
            End Property

            Public Property TestResultName() As String
                Get
                    Return _TestResultName
                End Get
                Set(ByVal value As String)
                    _TestResultName = value
                End Set
            End Property
            Public Property AlternateTestCode() As String
                Get
                    Return _AlternateTestCode
                End Get
                Set(ByVal value As String)
                    _AlternateTestCode = value
                End Set
            End Property

            Public Property AlternateTestName() As String
                Get
                    Return _AlternateTestName
                End Get
                Set(ByVal value As String)
                    _AlternateTestName = value
                End Set
            End Property

            Public Property BlnSpecimenReceivedDateTime() As Boolean
                Get
                    Return _blnSpecimenReceivedDateTime
                End Get
                Set(ByVal value As Boolean)
                    _blnSpecimenReceivedDateTime = value
                End Set
            End Property
            Public Property SpecimenReceivedDateTime() As DateTime
                Get
                    Return _SpecimenReceivedDateTime
                End Get
                Set(ByVal value As DateTime)
                    _SpecimenReceivedDateTime = value
                End Set
            End Property
            Public Property BlnResultTransferDateTime() As Boolean
                Get
                    Return _blnResultTransferDateTime
                End Get
                Set(ByVal value As Boolean)
                    _blnResultTransferDateTime = value
                End Set
            End Property
            Public Property ResultTransferDateTime() As DateTime
                Get
                    Return _ResultTransferDateTime
                End Get
                Set(ByVal value As DateTime)
                    _ResultTransferDateTime = value
                End Set
            End Property
            ''Mitesh
            Public Property ReportedDateTime() As DateTime
                Get
                    Return _ReportedDatetime
                End Get
                Set(ByVal value As DateTime)
                    _ReportedDatetime = value
                End Set
            End Property

            Public Property TestResultDateTime() As DateTime
                Get
                    Return _TestResultDateTime
                End Get
                Set(ByVal value As DateTime)
                    _TestResultDateTime = value
                End Set
            End Property

            Public Property TestResultDetails() As OrderTestResultDetails
                Get
                    Return _TestResultDetails
                End Get
                Set(ByVal value As OrderTestResultDetails)
                    If (bTestResultDetails) Then
                        If (IsNothing(_TestResultDetails) = False) Then
                            _TestResultDetails.Dispose()
                            _TestResultDetails = Nothing
                        End If
                        bTestResultDetails = False
                    End If
                    _TestResultDetails = value
                End Set
            End Property

            Public Property IsFinished() As Int16
                Get
                    Return _IsFinished
                End Get
                Set(ByVal value As Int16)
                    _IsFinished = value
                End Set
            End Property


            Public Property DMSID() As Int64
                Get
                    Return _DMSID
                End Get
                Set(ByVal value As Int64)
                    _DMSID = value
                End Set
            End Property
            Public Property DMSIDCollection() As String
                Get
                    Return _DMSIDCollection
                End Get
                Set(ByVal value As String)
                    _DMSIDCollection = value
                End Set
            End Property

            Public Property TestResultDateTimeUTC() As Integer
                Get
                    Return _TestResultDateTimeUTC
                End Get
                Set(ByVal value As Integer)
                    _TestResultDateTimeUTC = value
                End Set
            End Property

            Public Property SpecimenReceivedDateTimeUTC() As Integer
                Get
                    Return _SpecimenReceivedDateTimeUTC
                End Get
                Set(ByVal value As Integer)
                    _SpecimenReceivedDateTimeUTC = value
                End Set
            End Property

            Public Property ResultTransferDateTimeUTC() As Integer
                Get
                    Return _ResultTransferDateTimeUTC
                End Get
                Set(ByVal value As Integer)
                    _ResultTransferDateTimeUTC = value
                End Set
            End Property

            Public Sub New()
                MyBase.New()
                _TestResultDetails = New OrderTestResultDetails
            End Sub

            Protected Overrides Sub Finalize()
                If (bTestResultDetails) Then
                    _TestResultDetails = Nothing
                    bTestResultDetails = False
                End If
                MyBase.Finalize()
            End Sub
            Public Sub Dispose()
                If (bTestResultDetails) Then
                    If (IsNothing(_TestResultDetails) = False) Then
                        _TestResultDetails.Dispose()
                        _TestResultDetails = Nothing
                    End If
                    bTestResultDetails = False
                End If
            End Sub
        End Class

        Public Class OrderTestResults

            Inherits gloBaseCollection
            Implements IDisposable


            Public ReadOnly Property Item(ByVal index As Integer) As OrderTestResult
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), OrderTestResult)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _OrderTestResult As OrderTestResult)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_OrderTestResult)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class OrderTestResultDetail
#Region "private variables"
            Private _OrderID As Int64 = 0
            Private _TestID As Int64 = 0
            Private _TestName As String = ""
            Private _TestResultNumber As Int64 = 0
            Private _ResultLineNo As Int64 = 0
            Private _ResultNameID As Int64 = 0
            Private _ResultName As String = ""
            Private _ResultValue As String = ""
            Private _ResultUnit As String = ""
            Private _ResultRange As String = ""
            'Private _ResultType As enumTestResultReadType
            Private _ResultTypeCode As String
            Private _ResultTypeDesc As String
            Private _AbnormalFlagCode As String
            Private _AbnormalFlagDesc As String
            Private _ResultComment As String = ""
            Private _ResultWord As Object
            Private _ResultDMS As Int64 = 0
            Private _ResultUserID As Int64 = 0
            Private _ResultDateTime As DateTime
            Private _IsFinished As Int16 = 0
            Private _ResultLOINCID As String = ""
            ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
            'Added by madan-- on 20100409...
            Private _AlternateResultName As String = ""
            Private _AlternateResultCode As String = ""
            'Added on 20100413
            Private _ProducerIdentifier As String = ""
            'End Madan
            'Added on 20120508
            Private _TestSpecimenCollectionDateTime As DateTime

            ''-------------

            ''Added properties for fileds  by Abhijeet on 20101026
            Private _LabFacilityName As String = ""
            Private _LabFacilityStreetAddress As String = ""
            Private _LabFacilityCity As String = ""
            Private _LabFacilityState As String = ""
            Private _LabFacilityZipCode As String = ""
            ''End of changes for Adding properties for fileds  Abhijeet on 20101026
            Private _PatientSpecificRange As String = ""
            ''Sanjog
            Private _TestSpecimenCollectionDate As String

            Private _LabFacilityIdentifierTypeCode As String = String.Empty
            Private _LabFacilityOrganizationIdentifier As String = String.Empty
            Private _LabFacilityCountry As String = String.Empty
            Private _LabFacilityCountyOrParishCode As String = String.Empty
            Private _ResultCode As String = String.Empty
            Private _ResultCodeType As String = String.Empty
            Private _LabFacilityMedicalDirectorIDNumber As String = String.Empty
            Private _LabFacilityMedicalDirectorLastName As String = String.Empty
            Private _LabFacilityMedicalDirectorMiddleName As String = String.Empty
            Private _LabFacilityMedicalDirectorSuffix As String = String.Empty
            Private _LabFacilityMedicalDirectorPrefix As String = String.Empty
            Private _LabFacilityMedicalDirectorFirstName As String = String.Empty
            Private _ResultParentChildFlag As Int64 = 0
            Private _ResultDateTimeUTC As Integer = 0
            Private _TestSpecimenCollectionDateTimeUTC As Integer = 0
            Private _LabResultConceptID As String = String.Empty
            Private _LabResultICD9 As String = String.Empty
            Private _LabResultICD10 As String = String.Empty
            Private _LabResultLOINC As String = String.Empty
         


#End Region

            Public Property OrderID() As Int64
                Get
                    Return _OrderID
                End Get
                Set(ByVal value As Int64)
                    _OrderID = value
                End Set
            End Property

            Public Property TestID() As Int64
                Get
                    Return _TestID
                End Get
                Set(ByVal value As Int64)
                    _TestID = value
                End Set
            End Property

            Public Property TestName() As String
                Get
                    Return _TestName
                End Get
                Set(ByVal value As String)
                    _TestName = value
                End Set
            End Property

            Public Property TestResultNumber() As Int64
                Get
                    Return _TestResultNumber
                End Get
                Set(ByVal value As Int64)
                    _TestResultNumber = value
                End Set
            End Property

            Public Property ResultLineNo() As Int64
                Get
                    Return _ResultLineNo
                End Get
                Set(ByVal value As Int64)
                    _ResultLineNo = value
                End Set
            End Property

            Public Property ResultNameID() As Int64
                Get
                    Return _ResultNameID
                End Get
                Set(ByVal value As Int64)
                    _ResultNameID = value
                End Set
            End Property

            Public Property ResultName() As String
                Get
                    Return _ResultName
                End Get
                Set(ByVal value As String)
                    _ResultName = value
                End Set
            End Property

            Public Property ResultValue() As String
                Get
                    Return _ResultValue
                End Get
                Set(ByVal value As String)
                    _ResultValue = value
                End Set
            End Property

            Public Property ResultUnit() As String
                Get
                    Return _ResultUnit
                End Get
                Set(ByVal value As String)
                    _ResultUnit = value
                End Set
            End Property

            Public Property ResultRange() As String
                Get
                    Return _ResultRange
                End Get
                Set(ByVal value As String)
                    _ResultRange = value
                End Set
            End Property

            Public Property ResultTypeCode() As String
                Get
                    Return _ResultTypeCode
                End Get
                Set(ByVal value As String)
                    _ResultTypeCode = value
                End Set
            End Property

            Public Property ResultTypeDesc() As String
                Get
                    Return _ResultTypeDesc
                End Get
                Set(ByVal value As String)
                    _ResultTypeDesc = value
                End Set
            End Property

            ' '' <AbNormal Flag>
            ' ''  20070602
            ' '' <As Per For HL7>
            Public Property AbnormalFlagCode() As String
                Get
                    Return _AbnormalFlagCode
                End Get
                Set(ByVal value As String)
                    _AbnormalFlagCode = value
                End Set
            End Property

            Public Property AbnormalFlagDesc() As String
                Get
                    Return _AbnormalFlagDesc
                End Get
                Set(ByVal value As String)
                    _AbnormalFlagDesc = value
                End Set
            End Property

            Public Property ResultComment() As String
                Get
                    Return _ResultComment
                End Get
                Set(ByVal value As String)
                    _ResultComment = value
                End Set
            End Property

            Public Property ResultWord() As Object
                Get
                    Return _ResultWord
                End Get
                Set(ByVal value As Object)
                    _ResultWord = value
                End Set
            End Property

            Public Property ResultDMSID() As Int64
                Get
                    Return _ResultDMS
                End Get
                Set(ByVal value As Int64)
                    _ResultDMS = value
                End Set
            End Property

            Public Property UserID() As Int64
                Get
                    Return _ResultUserID
                End Get
                Set(ByVal value As Int64)
                    _ResultUserID = value
                End Set
            End Property

            Public Property ResultDateTime() As DateTime
                Get
                    Return _ResultDateTime
                End Get
                Set(ByVal value As DateTime)
                    _ResultDateTime = value
                End Set
            End Property

            Public Property IsFinished() As Int16
                Get
                    Return _IsFinished
                End Get
                Set(ByVal value As Int16)
                    _IsFinished = value
                End Set
            End Property

            Public Property ResultLOINCID() As String
                Get
                    Return _ResultLOINCID
                End Get
                Set(ByVal value As String)
                    _ResultLOINCID = value
                End Set
            End Property
            ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
            'Added by madan-- on 20100409...
            Public Property AlternateResultName() As String
                Get
                    Return _AlternateResultName
                End Get
                Set(ByVal value As String)
                    _AlternateResultName = value
                End Set
            End Property
            Public Property AlternateResultCode() As String
                Get
                    Return _AlternateResultCode
                End Get
                Set(ByVal value As String)
                    _AlternateResultCode = value
                End Set
            End Property
            Public Property ProducerIdentifier() As String
                Get
                    Return _ProducerIdentifier
                End Get
                Set(ByVal value As String)
                    _ProducerIdentifier = value
                End Set
            End Property
            'End Madan

            'defined properties by Abhijeet on 20101026
            Public Property LabFacilityName() As String
                Get
                    Return _LabFacilityName
                End Get
                Set(ByVal value As String)
                    _LabFacilityName = value
                End Set
            End Property
            Public Property LabFacilityStreetAddress() As String
                Get
                    Return _LabFacilityStreetAddress
                End Get
                Set(ByVal value As String)
                    _LabFacilityStreetAddress = value
                End Set
            End Property
            Public Property LabFacilityCity() As String
                Get
                    Return _LabFacilityCity
                End Get
                Set(ByVal value As String)
                    _LabFacilityCity = value
                End Set
            End Property
            Public Property LabFacilityState() As String
                Get
                    Return _LabFacilityState
                End Get
                Set(ByVal value As String)
                    _LabFacilityState = value
                End Set
            End Property
            Public Property LabFacilityZipCode() As String
                Get
                    Return _LabFacilityZipCode
                End Get
                Set(ByVal value As String)
                    _LabFacilityZipCode = value
                End Set
            End Property
            Public Property PatientSpecificRange() As String
                Get
                    Return _PatientSpecificRange
                End Get
                Set(ByVal value As String)
                    _PatientSpecificRange = value
                End Set
            End Property
            'End of changes by Abhijeet on 20101026

            ''Sanjog
            Public Property TestSpecimenCollectionDate() As String
                Get
                    Return _TestSpecimenCollectionDate
                End Get
                Set(ByVal value As String)
                    _TestSpecimenCollectionDate = value
                End Set
            End Property

            Public Property TestSpecimenCollectionDateTime() As DateTime
                Get
                    Return _TestSpecimenCollectionDateTime
                End Get
                Set(ByVal value As DateTime)
                    _TestSpecimenCollectionDateTime = value
                End Set
            End Property

            Public Property LabFacilityIdentifierTypeCode() As String
                Get
                    Return _LabFacilityIdentifierTypeCode
                End Get
                Set(ByVal value As String)
                    _LabFacilityIdentifierTypeCode = value
                End Set
            End Property

            Public Property LabFacilityOrganizationIdentifier() As String
                Get
                    Return _LabFacilityOrganizationIdentifier
                End Get
                Set(ByVal value As String)
                    _LabFacilityOrganizationIdentifier = value
                End Set
            End Property

            Public Property LabFacilityCountry() As String
                Get
                    Return _LabFacilityCountry
                End Get
                Set(ByVal value As String)
                    _LabFacilityCountry = value
                End Set
            End Property

            Public Property LabFacilityCountyOrParishCode() As String
                Get
                    Return _LabFacilityCountyOrParishCode
                End Get
                Set(ByVal value As String)
                    _LabFacilityCountyOrParishCode = value
                End Set
            End Property

            Public Property ResultCode() As String
                Get
                    Return _ResultCode
                End Get
                Set(ByVal value As String)
                    _ResultCode = value
                End Set
            End Property

            Public Property ResultCodeType() As String
                Get
                    Return _ResultCodeType
                End Get
                Set(ByVal value As String)
                    _ResultCodeType = value
                End Set
            End Property

            Public Property LabFacilityMedicalDirectorIDNumber() As String
                Get
                    Return _LabFacilityMedicalDirectorIDNumber
                End Get
                Set(ByVal value As String)
                    _LabFacilityMedicalDirectorIDNumber = value
                End Set
            End Property

            Public Property LabFacilityMedicalDirectorLastName() As String
                Get
                    Return _LabFacilityMedicalDirectorLastName
                End Get
                Set(ByVal value As String)
                    _LabFacilityMedicalDirectorLastName = value
                End Set
            End Property

            Public Property LabFacilityMedicalDirectorMiddleName() As String
                Get
                    Return _LabFacilityMedicalDirectorMiddleName
                End Get
                Set(ByVal value As String)
                    _LabFacilityMedicalDirectorMiddleName = value
                End Set
            End Property

            Public Property LabFacilityMedicalDirectorSuffix() As String
                Get
                    Return _LabFacilityMedicalDirectorSuffix
                End Get
                Set(ByVal value As String)
                    _LabFacilityMedicalDirectorSuffix = value
                End Set
            End Property

            Public Property LabFacilityMedicalDirectorPrefix() As String
                Get
                    Return _LabFacilityMedicalDirectorPrefix
                End Get
                Set(ByVal value As String)
                    _LabFacilityMedicalDirectorPrefix = value
                End Set
            End Property

            Public Property LabFacilityMedicalDirectorFirstName() As String
                Get
                    Return _LabFacilityMedicalDirectorFirstName
                End Get
                Set(ByVal value As String)
                    _LabFacilityMedicalDirectorFirstName = value
                End Set
            End Property

            Public Property ResultParentChildFlag() As Long
                Get
                    Return _ResultParentChildFlag
                End Get
                Set(ByVal value As Long)
                    _ResultParentChildFlag = value
                End Set
            End Property

            Public Property ResultDateTimeUTC() As Integer
                Get
                    Return _ResultDateTimeUTC
                End Get
                Set(ByVal value As Integer)
                    _ResultDateTimeUTC = value
                End Set
            End Property

            Public Property TestSpecimenCollectionDateTimeUTC() As Integer
                Get
                    Return _TestSpecimenCollectionDateTimeUTC
                End Get
                Set(ByVal value As Integer)
                    _TestSpecimenCollectionDateTimeUTC = value
                End Set
            End Property

            Public Property LabResultConceptID() As String
                Get
                    Return _LabResultConceptID
                End Get
                Set(ByVal value As String)
                    _LabResultConceptID = value
                End Set
            End Property

            Public Property LabResultICD9() As String
                Get
                    Return _LabResultICD9
                End Get
                Set(ByVal value As String)
                    _LabResultICD9 = value
                End Set
            End Property

            Public Property LabResultICD10() As String
                Get
                    Return _LabResultICD10
                End Get
                Set(ByVal value As String)
                    _LabResultICD10 = value
                End Set
            End Property

            Public Property LabResultLOINC() As String
                Get
                    Return _LabResultLOINC
                End Get
                Set(ByVal value As String)
                    _LabResultLOINC = value
                End Set
            End Property
           


            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class OrderTestResultDetails

            Inherits gloBaseCollection
            Implements IDisposable


            Public ReadOnly Property Item(ByVal index As Integer) As OrderTestResultDetail
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), OrderTestResultDetail)
                End Get
            End Property

            ' Restricts to PatientInterface types, items that can be added to the collection.

            Public Sub Add(ByVal _OrderTestResultDetail As OrderTestResultDetail)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_OrderTestResultDetail)
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class ItemDetail

            Private _ID As Int64
            Private _Code As String
            Private _Description As String
            Private _IcdRevision As Integer
            Public Property ID() As Int64
                Get
                    Return _ID
                End Get
                Set(ByVal value As Int64)
                    _ID = value
                End Set
            End Property

            Public Property Code() As String
                Get
                    Return _Code
                End Get
                Set(ByVal value As String)
                    _Code = value
                End Set
            End Property

            Public Property Description() As String
                Get
                    Return _Description
                End Get
                Set(ByVal value As String)
                    _Description = value
                End Set
            End Property
            ''added for icd10 feature for emdeon
            Public Property IcdRevision() As Int16
                Get
                    Return _IcdRevision
                End Get
                Set(ByVal value As Int16)
                    _IcdRevision = value
                End Set
            End Property
            Public Sub New()
                MyBase.New()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class ItemDetails

            Inherits gloBaseCollection
            Implements IDisposable


            Public ReadOnly Property Item(ByVal index As Integer) As ItemDetail
                Get
                    ' The appropriate item is retrieved from the List object and 
                    ' explicitly cast to the PatientInterface type, then returned to the 
                    ' caller.
                    Return CType(List.Item(index), ItemDetail)
                End Get
            End Property
            ' Restricts to PatientInterface types, items that can be added to the collection.
            Public Sub Add(ByVal _ItemDetail As ItemDetail)
                ' Invokes Add method of the List object to add a PatientInterface.
                List.Add(_ItemDetail)
            End Sub
            ''IcdRevision added for icd10 feature for emdeon
            Public Sub Add(ByVal ID As Int64, ByVal Code As String, ByVal Description As String, Optional ByVal IcdRevision As Int16 = 9)
                Dim _ID As New ItemDetail
                With _ID
                    .ID = ID
                    .Code = Code
                    .Description = Description
                    .IcdRevision = IcdRevision
                End With
                List.Add(_ID)
                _ID = Nothing
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub


            Public Function GetCode(ByVal Desc As String) As String
                'mybase.List.IndexOf 
                Dim myString As String = String.Empty
                For i As Integer = 0 To Me.Count - 1
                    If Me.Item(i).Description = Desc Then
                        myString = Me.Item(i).Code
                        Exit For
                    End If
                Next
                Return myString
            End Function

            Public Function GetDescription(ByVal Code As String) As String
                'mybase.List.IndexOf 
                Dim myString As String = String.Empty
                For i As Integer = 0 To Me.Count - 1
                    If Me.Item(i).Code = Code Then
                        myString = Me.Item(i).Description
                        Exit For
                    End If
                Next
                Return myString
            End Function

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

    End Namespace

#End Region
End Namespace




