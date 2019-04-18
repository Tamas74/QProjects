Imports gloPatient
Public Class Patient
    Implements IDisposable

    Private npatientid As Int64
    Private mPatientName As PersonName
    Private mRace As RaceCol
    Private mEthnicity As EthnicityCol
    Private mCDCRace As CDCCol
    Private mPatientAuthor As PatientAuthor

    Private mDateofBBirth As String
    Private mGender As String = ""
    Private mMaritalStatus As String
    Private mReligiousAffiliationCode As String
    Private mRaceCode As String
    Private methnicGroupCode As String
    Private mPregnant As String

    Private sCounty As String
    Private nSSN As String
    Private sMaritalStatus As String
    Private sPhone As String
    Private sMobile As String
    Private sEmail As String
    Private sRace As String
    Private sGuardian_fName As String
    Private sGuardian_mName As String
    Private sGuardian_lName As String
    Private sGuardian_Address1 As String
    Private sGuardian_Address2 As String
    Private sGuardian_City As String
    Private sGuardian_State As String
    Private sGuardian_ZIP As String
    Private sGuardian_County As String
    Private sGuardian_Phone As String
    Private sGuardian_Mobile As String  'Added by kanchan on 20100712
    Private sGuardian_Relation As String 'Added by kanchan on 20100712
    Private sGuardian_Email As String
    Private sGuardian_Country As String
    Private sEthn As String
    Private sLanguage As String 'Added by kanchan on 20100916
    Private sLanguageCode As String 'Added by kanchan on 20100916
    Private sCommPreference As String
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private oMedications As MedicationsCol
    Private oMedicationsAdmin As MedicationsCol
    Private oInsurances As InsuranceCol
    Private oAdvDirectives As AdvDirectiveCol

    Private mPatientInsurance As Insurance
    Private oAllergies As AllergiesCol
    Private oConditions As ConditionsCol
    Private oAdvancedirective As Advancedirective
    Private oPatientImmunizatios As ImmunizationCol
    Private oPatientVitals As VitalsCol
    Private oPatientProblems As ProblemsCol
    Private oPatientResults As ResultsCol
    Private oPatientComments As CommentsCol
    Private oPatientEncounters As EncountersCol
    Private oPatientVisitDateAndLocation As EncountersCol

    Private oPatientProcedures As ProceduresCol

    Private oPatientLanguage As LanguageCol
    Private oPatientProvider As ProviderCol
    Private oInformationRecipient As InfoRecipientCol
    Private oPatientSupport As PatientSupport
    'Private mLabResults As LabResults
    Private oLabTests As LabTestCol
    Private oLabResultsCol As LabResultsCol
    Private oPatientFamilyHistory As FamilyHistoryCol
    Private oPatientSocialHistory As SocialHistoryCol

    Private ogloPatientHistoryCol As gloPatientHistoryCol
    Private ogloPatient As gloPatient.Patient
    Private objUser As User
    Private objClinic As Clinic
    Private objPracticeContact As PracticeContact
    Private sChiefcomplaint As String
    Private oClinicalInstruction As PatientClinicalInstructionCol
    Private oPatientSupportCol As PatientSupportCol
    Private oPatientEducationCol As PatientEducationCol
    Private oAssessment As String
    Private oPatientCarePlanCol As PatientCarePlanCol
    Private oAppointmentCol As AppointmentCol
    Private oReferralstoProvider As PatientSupportCol 'Referral to provider
    Private oScheduleTests As LabTestCol 'Future schedule test
    Private oPendingTests As LabTestCol 'Diagnostic Pending test
    Private sFunctionalStatus As String
    Private sCognitiveStatus As String
    Private sReasonForReferral As String
    Private oEncounterDiagnosis As ProblemsCol
    Private oFunctionalStatus As AllergiesCol
    Private objloginprovider As LoginProvider
    Private oorderscol As OrderCol
    Private oMentalStatus As AllergiesCol
    Private _ImplantCol As gloPatientImplantCol
    Private _HealthConcernCol As HealthConcernCol
    Private _Goals As GoalsCol
    Private _InterventionCol As InterventionCol
    Private _PlannedModuleCol As PlannedModuleCol
    Private _OutcomeCol As OutcomeCol

 Public Property OutcomeCol As OutcomeCol
        Get
            Return _OutcomeCol
        End Get
        Set(value As OutcomeCol)
            _OutcomeCol = value
        End Set
    End Property
    Public Property PlannedModuleCol
        Get
            Return _PlannedModuleCol
        End Get
        Set(value)
            _PlannedModuleCol = value
        End Set
    End Property
    Public Property InterventionCol
        Get
            Return _InterventionCol
        End Get
        Set(value)
            _InterventionCol = value
        End Set
    End Property
    Public Property HealthConcernCol
        Get
            Return _HealthConcernCol
        End Get
        Set(value)
            _HealthConcernCol = value
        End Set
    End Property
    Public Property GoalsCol
        Get
            Return _Goals
        End Get
        Set(value)
            _Goals = value
        End Set
    End Property

    Public Property ImplantCol As gloPatientImplantCol
        Get
            Return _ImplantCol
        End Get
        Set(value As gloPatientImplantCol)
            _ImplantCol = value
        End Set
    End Property

    Public Property MentalStatus As AllergiesCol
        Get
            Return oMentalStatus
        End Get
        Set(value As AllergiesCol)
            oMentalStatus = value
        End Set
    End Property

    Public Property FunctionalStatus() As AllergiesCol
        Get
            Return oFunctionalStatus
        End Get
        Set(ByVal Value As AllergiesCol)
            oFunctionalStatus = Value
        End Set
    End Property
    'Private oAdvanceDirective as 
    Public Property EncounterDiagnosis() As ProblemsCol
        Get
            Return oEncounterDiagnosis
        End Get
        Set(ByVal Value As ProblemsCol)
            oEncounterDiagnosis = Value
        End Set
    End Property

    Public Property CognitiveStatus() As String
        Get
            Return sCognitiveStatus
        End Get
        Set(ByVal Value As String)
            sCognitiveStatus = Value
        End Set
    End Property
    Public Property ReasonForReferral() As String
        Get
            Return sReasonForReferral
        End Get
        Set(ByVal Value As String)
            sReasonForReferral = Value
        End Set
    End Property
    'Public Property FunctionalStatus() As String
    '    Get
    '        Return sFunctionalStatus
    '    End Get
    '    Set(ByVal Value As String)
    '        sFunctionalStatus = Value
    '    End Set
    'End Property
    Public Property PendingTests() As LabTestCol
        Get
            Return oPendingTests
        End Get
        Set(ByVal Value As LabTestCol)
            oPendingTests = Value
        End Set
    End Property
    'Added by kanchan on 20100916
    Public Property FutureScheduleTests() As LabTestCol
        Get
            Return oScheduleTests
        End Get
        Set(ByVal Value As LabTestCol)
            oScheduleTests = Value
        End Set
    End Property
    Public Property ReferralstoProvider() As PatientSupportCol
        Get
            Return oReferralstoProvider
        End Get
        Set(ByVal Value As PatientSupportCol)
            oReferralstoProvider = Value
        End Set
    End Property
    Public Property FutureScheduleApt() As AppointmentCol
        Get
            Return oAppointmentCol
        End Get
        Set(ByVal Value As AppointmentCol)
            oAppointmentCol = Value
        End Set
    End Property
    Public Property PatientCarePlan() As PatientCarePlanCol
        Get
            Return oPatientCarePlanCol
        End Get
        Set(ByVal Value As PatientCarePlanCol)
            oPatientCarePlanCol = Value
        End Set
    End Property

    Public Property PatientEducation() As PatientEducationCol
        Get
            Return oPatientEducationCol
        End Get
        Set(ByVal Value As PatientEducationCol)
            oPatientEducationCol = Value
        End Set
    End Property
    Public Property Assessment() As String
        Get
            Return oAssessment
        End Get
        Set(ByVal Value As String)
            oAssessment = Value
        End Set
    End Property
    Public Property PatientCareTeam() As PatientSupportCol
        Get
            Return oPatientSupportCol
        End Get
        Set(ByVal Value As PatientSupportCol)
            oPatientSupportCol = Value
        End Set
    End Property
    Public Property ClinicalInstruction() As PatientClinicalInstructionCol
        Get
            Return oClinicalInstruction
        End Get
        Set(ByVal Value As PatientClinicalInstructionCol)
            oClinicalInstruction = Value
        End Set
    End Property
    Public Property Chiefcomplaint() As String
        Get
            Return sChiefcomplaint
        End Get
        Set(ByVal Value As String)
            sChiefcomplaint = Value
        End Set
    End Property
    Public Property Clinic() As Clinic
        Get
            Return objClinic
        End Get
        Set(ByVal Value As Clinic)
            objClinic = Value
        End Set
    End Property
    Public Property LoginProvider() As LoginProvider
        Get
            Return objloginprovider
        End Get
        Set(value As LoginProvider)
            objloginprovider = value
        End Set
    End Property




    Public Property PracticeContact() As PracticeContact
        Get
            Return objPracticeContact
        End Get
        Set(ByVal Value As PracticeContact)
            objPracticeContact = Value
        End Set
    End Property
    Public Property Language() As String
        Get

            Return sLanguage
        End Get
        Set(ByVal Value As String)
            sLanguage = Value
        End Set
    End Property
    'Added by kanchan on 20100916
    Public Property LanguageCode() As String
        Get

            Return sLanguageCode
        End Get
        Set(ByVal Value As String)
            sLanguageCode = Value
        End Set
    End Property
    Public Property CommPreference() As String
        Get

            Return sCommPreference
        End Get
        Set(ByVal Value As String)
            sCommPreference = Value
        End Set
    End Property
    Public Property LabTests() As LabTestCol
        Get
            If IsNothing(oLabTests) Then
                oLabTests = New LabTestCol
            End If
            Return oLabTests
        End Get
        Set(ByVal Value As LabTestCol)
            oLabTests = Value
        End Set
    End Property
    Public Property Orders() As OrderCol
        Get
            If IsNothing(oorderscol) Then
                oorderscol = New OrderCol
            End If

            Return oorderscol
        End Get
        Set(ByVal value As OrderCol)
            oorderscol = value
        End Set
    End Property

    Public Property PatientSupport() As PatientSupport
        Get
            Return oPatientSupport
        End Get
        Set(ByVal Value As PatientSupport)
            oPatientSupport = Value
        End Set
    End Property
    Public Property Author() As PatientAuthor
        Get

            Return mPatientAuthor
        End Get
        Set(ByVal Value As PatientAuthor)
            mPatientAuthor = Value
        End Set
    End Property
    Private mInfoRecipent As InfoRecipent
    Public Property InfoRecipent() As InfoRecipent
        Get

            Return mInfoRecipent
        End Get
        Set(ByVal Value As InfoRecipent)
            mInfoRecipent = Value
        End Set
    End Property
    Public Property PatientProviders() As ProviderCol
        Get
            If IsNothing(oPatientProvider) Then
                oPatientProvider = New ProviderCol
            End If
            Return oPatientProvider
        End Get
        Set(ByVal Value As ProviderCol)
            oPatientProvider = Value
        End Set
    End Property
    Public Property ethnicGroupCode() As String
        Get
            Return methnicGroupCode
        End Get
        Set(ByVal Value As String)
            methnicGroupCode = Value
        End Set
    End Property

    Public Property Ethnicity() As String
        Get

            Return sEthn
        End Get
        Set(ByVal Value As String)
            sEthn = Value
        End Set
    End Property
    Public Property RaceCode() As String
        Get

            Return mRaceCode
        End Get
        Set(ByVal Value As String)
            mRaceCode = Value
        End Set
    End Property
    Public Property ReligiousAffiliationCode() As String
        Get

            Return mReligiousAffiliationCode
        End Get
        Set(ByVal Value As String)
            mReligiousAffiliationCode = Value
        End Set
    End Property
    Public Property MaritalStatus() As String
        Get

            Return mMaritalStatus
        End Get
        Set(ByVal Value As String)
            mMaritalStatus = Value
        End Set
    End Property

    Public Property County() As String
        Get

            Return sCounty
        End Get
        Set(ByVal Value As String)
            sCounty = Value
        End Set
    End Property

    Public Property SSN() As String
        Get

            Return nSSN
        End Get
        Set(ByVal Value As String)
            nSSN = Value
        End Set
    End Property

    Public Property Phone() As String
        Get

            Return sPhone
        End Get
        Set(ByVal Value As String)
            sPhone = Value
        End Set
    End Property
    Public Property Mobile() As String
        Get

            Return sMobile
        End Get
        Set(ByVal Value As String)
            sMobile = Value
        End Set
    End Property
    Public Property Email() As String
        Get

            Return sEmail
        End Get
        Set(ByVal Value As String)
            sEmail = Value
        End Set
    End Property
    Public Property Race() As String
        Get

            Return sRace
        End Get
        Set(ByVal Value As String)
            sRace = Value
        End Set
    End Property
    Public Property Guardian_fName() As String
        Get

            Return sGuardian_fName
        End Get
        Set(ByVal Value As String)
            sGuardian_fName = Value
        End Set
    End Property
    Public Property Guardian_mName() As String
        Get

            Return sGuardian_mName
        End Get
        Set(ByVal Value As String)
            sGuardian_mName = Value
        End Set
    End Property
    Public Property Guardian_lName() As String
        Get

            Return sGuardian_lName
        End Get
        Set(ByVal Value As String)
            sGuardian_lName = Value
        End Set
    End Property
    Public Property Guardian_Address1() As String
        Get

            Return sGuardian_Address1
        End Get
        Set(ByVal Value As String)
            sGuardian_Address1 = Value
        End Set
    End Property

    Public Property Guardian_Address2() As String
        Get

            Return sGuardian_Address2
        End Get
        Set(ByVal Value As String)
            sGuardian_Address2 = Value
        End Set
    End Property
    Public Property Guardian_City() As String
        Get

            Return sGuardian_City
        End Get
        Set(ByVal Value As String)
            sGuardian_City = Value
        End Set
    End Property

    Public Property Guardian_State() As String
        Get

            Return sGuardian_State
        End Get
        Set(ByVal Value As String)
            sGuardian_State = Value
        End Set
    End Property

    Public Property Guardian_ZIP() As String
        Get

            Return sGuardian_ZIP
        End Get
        Set(ByVal Value As String)
            sGuardian_ZIP = Value
        End Set
    End Property


    Public Property Guardian_County() As String
        Get

            Return sGuardian_County
        End Get
        Set(ByVal Value As String)
            sGuardian_County = Value
        End Set
    End Property

    Public Property Guardian_Phone() As String
        Get

            Return sGuardian_Phone
        End Get
        Set(ByVal Value As String)
            sGuardian_Phone = Value
        End Set
    End Property
    'Added by kanchan on 20100712
    Public Property Guardian_Mobile() As String
        Get

            Return sGuardian_Mobile
        End Get
        Set(ByVal Value As String)
            sGuardian_Mobile = Value
        End Set
    End Property
    'Added by kanchan on 20100712
    Public Property Guardian_Relation() As String
        Get

            Return sGuardian_Relation
        End Get
        Set(ByVal Value As String)
            sGuardian_Relation = Value
        End Set
    End Property
    Public Property Guardian_Email() As String
        Get

            Return sGuardian_Email
        End Get
        Set(ByVal Value As String)
            sGuardian_Email = Value
        End Set
    End Property

    Public Property Guardian_Country() As String
        Get

            Return sGuardian_Country
        End Get
        Set(ByVal Value As String)
            sGuardian_Country = Value
        End Set
    End Property

    Public Property PatientLanguages() As LanguageCol
        Get
            If IsNothing(oPatientLanguage) Then
                oPatientLanguage = New LanguageCol
            End If
            Return oPatientLanguage
        End Get
        Set(ByVal Value As LanguageCol)
            oPatientLanguage = Value
        End Set
    End Property
    Public Property PatientEncounters() As EncountersCol
        Get

            Return oPatientEncounters
        End Get
        Set(ByVal Value As EncountersCol)
            oPatientEncounters = Value
        End Set
    End Property

    Public Property PatientVisitDateAndLocation() As EncountersCol
        Get

            Return oPatientVisitDateAndLocation
        End Get
        Set(ByVal Value As EncountersCol)
            oPatientVisitDateAndLocation = Value
        End Set
    End Property

    Public Property PatientProcedures() As ProceduresCol
        Get

            Return oPatientProcedures
        End Get
        Set(ByVal Value As ProceduresCol)
            oPatientProcedures = Value
        End Set
    End Property

    Public Property PatientResults() As ResultsCol
        Get
            If IsNothing(oPatientResults) Then
                oPatientResults = New ResultsCol
            End If
            Return oPatientResults
        End Get
        Set(ByVal Value As ResultsCol)
            oPatientResults = Value
        End Set
    End Property
    Public Property PatientVitals() As VitalsCol
        Get
            If IsNothing(oPatientVitals) Then
                oPatientVitals = New VitalsCol
            End If
            Return oPatientVitals
        End Get
        Set(ByVal Value As VitalsCol)
            oPatientVitals = Value
        End Set
    End Property

    Public Property PatientProblems() As ProblemsCol
        Get
            If IsNothing(oPatientProblems) Then
                oPatientProblems = New ProblemsCol
            End If
            Return oPatientProblems
        End Get
        Set(ByVal Value As ProblemsCol)
            oPatientProblems = Value
        End Set
    End Property


    Public Property PatientImmunizations() As ImmunizationCol
        Get
            Return oPatientImmunizatios
        End Get
        Set(ByVal Value As ImmunizationCol)
            oPatientImmunizatios = Value
        End Set
    End Property
    Public Property PatientConditions() As ConditionsCol
        Get
            If IsNothing(oConditions) Then
                oConditions = New ConditionsCol
            End If
            Return oConditions
        End Get
        Set(ByVal Value As ConditionsCol)
            oConditions = Value
        End Set
    End Property
    Public Property PatientMedications() As MedicationsCol
        Get
            Return oMedications
        End Get
        Set(ByVal Value As MedicationsCol)
            oMedications = Value
        End Set
    End Property
    Public Property PatientMedicationsAdmin() As MedicationsCol
        Get
            Return oMedicationsAdmin
        End Get
        Set(ByVal Value As MedicationsCol)
            oMedicationsAdmin = Value
        End Set
    End Property
    Public Property PatientInsurances() As InsuranceCol
        Get
            Return oInsurances
        End Get
        Set(ByVal Value As InsuranceCol)
            oInsurances = Value
        End Set
    End Property

    Public Property PatientFamilyHistory() As FamilyHistoryCol
        Get
            Return oPatientFamilyHistory
        End Get
        Set(ByVal Value As FamilyHistoryCol)
            oPatientFamilyHistory = Value
        End Set
    End Property

    Public Property PatientSocialHistory() As SocialHistoryCol
        Get
            Return oPatientSocialHistory
        End Get
        Set(ByVal Value As SocialHistoryCol)
            oPatientSocialHistory = Value
        End Set
    End Property

    Public Property Advancedirective() As AdvDirectiveCol
        Get
            Return oAdvDirectives
        End Get
        Set(ByVal Value As AdvDirectiveCol)
            oAdvDirectives = Value
        End Set
    End Property

    Public Property PatientAllergies() As AllergiesCol
        Get
            If IsNothing(oAllergies) Then
                oAllergies = New AllergiesCol
            End If
            Return oAllergies
        End Get
        Set(ByVal Value As AllergiesCol)
            oAllergies = Value
        End Set
    End Property
    Public Property PatientHistory() As gloPatientHistoryCol
        Get
            If IsNothing(ogloPatientHistoryCol) Then
                ogloPatientHistoryCol = New gloPatientHistoryCol
            End If
            Return ogloPatientHistoryCol
        End Get
        Set(ByVal Value As gloPatientHistoryCol)
            ogloPatientHistoryCol = Value
        End Set
    End Property
    Public Property UserDetails() As User
        Get

            Return objUser
        End Get
        Set(ByVal Value As User)
            objUser = Value
        End Set
    End Property
    Public Property PatientDemographics() As gloPatient.Patient
        Get
            If IsNothing(ogloPatient) Then
                ogloPatient = New gloPatient.Patient
            End If
            Return ogloPatient
        End Get
        Set(ByVal Value As gloPatient.Patient)
            ogloPatient = Value
        End Set
    End Property
    Public Property PatientLabResult() As LabResultsCol
        Get
            If IsNothing(oLabResultsCol) Then
                oLabResultsCol = New LabResultsCol
            End If
            Return oLabResultsCol
        End Get
        Set(ByVal Value As LabResultsCol)
            oLabResultsCol = Value
        End Set
    End Property
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                If Not IsNothing(oPatientSupport) Then
                    If Not IsNothing(oPatientSupport.PersonContactAddress) Then
                        oPatientSupport.PersonContactAddress.Dispose()
                        oPatientSupport.PersonContactAddress = Nothing

                    End If
                    If Not IsNothing(oPatientSupport.PersonContactPhone) Then
                        oPatientSupport.PersonContactPhone.Dispose()
                        oPatientSupport.PersonContactPhone = Nothing

                    End If
                    If Not IsNothing(oPatientSupport.PersonName) Then

                        If Not IsNothing(oPatientSupport.PersonName.PersonContactAddress) Then
                            oPatientSupport.PersonName.PersonContactAddress.Dispose()
                            oPatientSupport.PersonName.PersonContactAddress = Nothing
                        End If
                        If Not IsNothing(oPatientSupport.PersonName.PersonContactPhone) Then
                            oPatientSupport.PersonName.PersonContactPhone.Dispose()
                            oPatientSupport.PersonName.PersonContactPhone = Nothing
                        End If
                        oPatientSupport.PersonName.Dispose()
                        oPatientSupport.PersonName = Nothing
                    End If
                    oPatientSupport.Dispose()
                    oPatientSupport = Nothing
                End If
                If Not IsNothing(Author) Then
                    If Not IsNothing(Author.PersonContactAddress) Then
                        Author.PersonContactAddress.Dispose()
                        Author.PersonContactAddress = Nothing
                    End If
                    If Not IsNothing(Author.PersonName) Then
                        If Not IsNothing(Author.PersonName.PersonContactPhone) Then
                            Author.PersonName.PersonContactPhone.Dispose()
                            Author.PersonName.PersonContactPhone = Nothing
                        End If
                        If Not IsNothing(Author.PersonName.PersonContactAddress) Then
                            Author.PersonName.PersonContactAddress.Dispose()
                            Author.PersonName.PersonContactAddress = Nothing
                        End If
                        Author.PersonName.Dispose()
                        Author.PersonName = Nothing
                    End If

                    Author.Dispose()
                    Author = Nothing
                End If
                If Not IsNothing(PatientProviders) Then

                    PatientProviders.Dispose()
                    PatientProviders = Nothing
                End If
                If Not IsNothing(oMedications) Then
                    oMedications.Dispose()
                    oMedications = Nothing
                End If
                If Not IsNothing(oPatientSocialHistory) Then
                    oPatientSocialHistory.Dispose()
                    oPatientSocialHistory = Nothing

                End If
                If Not IsNothing(oLabTests) Then
                    oLabTests.Dispose()
                    oLabTests = Nothing
                End If
                'mPatient.Dispose()
                'mPatient = Nothing

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
    'Public Property Advancedirective() As Advancedirective
    '    Get

    '        Return oAdvancedirective
    '    End Get
    '    Set(ByVal value As Advancedirective)
    '        oAdvancedirective = value
    '    End Set
    'End Property
    'Public Property PatientInsurance() As Insurance
    '    Get
    '        If IsNothing(mPatientInsurance) Then
    '            mPatientInsurance = New Insurance
    '        End If
    '        Return mPatientInsurance
    '    End Get
    '    Set(ByVal value As Insurance)
    '        mPatientInsurance = value
    '    End Set
    'End Property
    'Public Property PatientMedication() As Medication
    '    Get
    '        If IsNothing(mPatientMedication) Then
    '            mPatientMedication = New Medication
    '        End If
    '        Return mPatientMedication
    '    End Get
    '    Set(ByVal value As Medication)
    '        mPatientMedication = value
    '    End Set
    'End Property
    Public Property PatientName() As PersonName
        Get
            If IsNothing(mPatientName) Then
                mPatientName = New PersonName
            End If
            Return mPatientName
        End Get
        Set(ByVal value As PersonName)
            mPatientName = value
        End Set
    End Property
    Public Property Races() As RaceCol
        Get
            If IsNothing(mRace) Then
                mRace = New RaceCol
            End If
            Return mRace
        End Get
        Set(ByVal value As RaceCol)
            mRace = value
        End Set
    End Property
    Public Property Ethnicities() As EthnicityCol
        Get
            If IsNothing(mEthnicity) Then
                mEthnicity = New EthnicityCol
            End If
            Return mEthnicity
        End Get
        Set(ByVal value As EthnicityCol)
            mEthnicity = value
        End Set
    End Property
    Public Property CDCRaces() As CDCCol
        Get
            If IsNothing(mCDCRace) Then
                mCDCRace = New CDCCol
            End If
            Return mCDCRace
        End Get
        Set(ByVal value As CDCCol)
            mCDCRace = value
        End Set
    End Property
    Public Property DateofBirth() As String
        Get
            Return mDateofBBirth
        End Get
        Set(ByVal value As String)
            mDateofBBirth = value
        End Set
    End Property
    Public Property Gender() As String
        Get
            Return mGender
        End Get
        Set(ByVal value As String)
            mGender = value
        End Set
    End Property
    Public Property PatientID As Int64
        Get

            Return npatientid
        End Get
        Set(ByVal Value As Int64)
            npatientid = Value
        End Set
    End Property

    Public Sub New()
        oPatientSupport = New PatientSupport
        mPatientAuthor = New PatientAuthor
    End Sub
End Class
Public Class PersonName
    Implements IDisposable
    Private _ProvTaXID As String
    Private _ID As Long
    Private _FirstName As String
    Private _MiddleName As String
    Private _LastName As String
    Private _Code As String
    Private _Prefix As String
    Private _Suffix As String
    Private _PersonContactAddress As ContactAddress
    Private _PersonContactPhone As PhoneNumber
    Private _ProvNPI As String
    Private _TaxonomyCode As String
    Private _TaxonomyDesc As String
    Private _ncontactflag As String
    Private _ProvidersTaxID As String
    Private _PreviousFirstName As String
    Private _PreviousMiddleName As String
    Private _PreviousLastName As String
    Private _Birthsex As String
    Public Property BirthSex() As String
        Get
            Return _Birthsex
        End Get
        Set(value As String)
            _Birthsex = value
        End Set
    End Property


    Public Property PreviousFirstName() As String
        Get
            Return _PreviousFirstName
        End Get
        Set(value As String)
            _PreviousFirstName = value
        End Set
    End Property
    Public Property PreviousMiddleName() As String
        Get
            Return _PreviousMiddleName
        End Get
        Set(value As String)
            _PreviousMiddleName = value
        End Set
    End Property
    Public Property PreviousLastName() As String
        Get
            Return _PreviousLastName
        End Get
        Set(value As String)
            _PreviousLastName = value
        End Set
    End Property


    Public Property PersonContactPhone() As PhoneNumber
        Get
            If IsNothing(_PersonContactPhone) Then
                _PersonContactPhone = New PhoneNumber
            End If
            Return _PersonContactPhone
        End Get
        Set(ByVal Value As PhoneNumber)
            _PersonContactPhone = Value
        End Set
    End Property
    Public Property PersonContactAddress() As ContactAddress
        Get
            If IsNothing(_PersonContactAddress) Then
                _PersonContactAddress = New ContactAddress
            End If
            Return _PersonContactAddress
        End Get
        Set(ByVal Value As ContactAddress)
            _PersonContactAddress = Value
        End Set
    End Property
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


    Public Property Prefix() As String
        Get
            Return _Prefix
        End Get
        Set(ByVal Value As String)
            _Prefix = Value
        End Set
    End Property
    Public Property ProvNPI() As String
        Get
            Return _ProvNPI
        End Get
        Set(ByVal Value As String)
            _ProvNPI = Value
        End Set
    End Property
    Public Property TaxonomyCode() As String
        Get
            Return _TaxonomyCode
        End Get
        Set(ByVal Value As String)
            _TaxonomyCode = Value
        End Set
    End Property

    Public Property TaxonomyDesc() As String
        '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - Start
        Get
            Return _TaxonomyDesc
        End Get
        Set(ByVal Value As String)
            _TaxonomyDesc = Value
        End Set
        '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End
    End Property
    
    Public Property Suffix() As String
        Get
            Return _Suffix
        End Get
        Set(ByVal Value As String)
            _Suffix = Value
        End Set
    End Property
    Public Property ProvTaXID() As String
        Get
            Return _ProvTaXID
        End Get
        Set(ByVal Value As String)
            _ProvTaXID = Value
        End Set
    End Property
    Public Property ProvidersTaxID() As String
        Get
            Return _ProvidersTaxID
        End Get
        Set(ByVal Value As String)
            _ProvidersTaxID = Value
        End Set
    End Property

    Public Property ContactFlag() As String
        Get
            Return _ncontactflag
        End Get
        Set(value As String)
            _ncontactflag = value
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

    Public Sub New()
        _PersonContactPhone = New PhoneNumber
        _PersonContactAddress = New ContactAddress
    End Sub
End Class

Public Class RaceDetails
    Implements IDisposable
    Private _OMBCode As String
    Private _OMBDescription As String
    Private _CDCCode As String
    Private _CDCDescription As String

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
    Private _RaceCount As Int64 = 0
    Public Property OMBCode() As String
        Get
            Return _OMBCode
        End Get
        Set(ByVal Value As String)
            _OMBCode = Value
        End Set
    End Property
    Public Property RaceCount() As Int64
        Get
            Return _RaceCount
        End Get
        Set(ByVal Value As Int64)
            _RaceCount = Value
        End Set
    End Property

    Public Property OMBDescription() As String
        Get
            Return _OMBDescription
        End Get
        Set(ByVal Value As String)
            _OMBDescription = Value
        End Set
    End Property
    Public Property CDCCode() As String
        Get
            Return _CDCCode
        End Get
        Set(ByVal Value As String)
            _CDCCode = Value
        End Set
    End Property

    Public Property CDCDescription() As String
        Get
            Return _CDCDescription
        End Get
        Set(ByVal Value As String)
            _CDCDescription = Value
        End Set
    End Property
   
    Private _CDCCol As CDCCol
    Public Property CDCCol() As CDCCol
        Get
            Return _CDCCol
        End Get
        Set(ByVal Value As CDCCol)
            _CDCCol = Value
        End Set
    End Property
    
End Class


Public Class CDCRaceDetails
    Implements IDisposable
    'Private _OMBCode As String
    'Private _OMBDescription As String
    Private _CDCCode As String
    Private _CDCDescription As String

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
    'Public Property OMBCode() As String
    '    Get
    '        Return _OMBCode
    '    End Get
    '    Set(ByVal Value As String)
    '        _OMBCode = Value
    '    End Set
    'End Property

    'Public Property OMBDescription() As String
    '    Get
    '        Return _OMBDescription
    '    End Get
    '    Set(ByVal Value As String)
    '        _OMBDescription = Value
    '    End Set
    'End Property

    Public Property CDCCode() As String
        Get
            Return _CDCCode
        End Get
        Set(ByVal Value As String)
            _CDCCode = Value
        End Set
    End Property

    Public Property CDCDescription() As String
        Get
            Return _CDCDescription
        End Get
        Set(ByVal Value As String)
            _CDCDescription = Value
        End Set
    End Property
End Class
Public Class ContactAddress
    Implements IDisposable

    Private _Street As String
    Private _City As String
    Private _State As String
    Private _Zip As String
    Private _Country As String
    Private _County As String
    Private _AddressLine2 As String 'Added by kanchan on 20100709

    Public Property County() As String
        Get
            Return _County
        End Get
        Set(ByVal Value As String)
            _County = Value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return _Country
        End Get
        Set(ByVal Value As String)
            _Country = Value
        End Set
    End Property
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
    'Added by kanchan on 20100709
    Public Property AddressLine2() As String
        Get
            Return _AddressLine2
        End Get
        Set(ByVal Value As String)
            _AddressLine2 = Value
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
Public Class PhoneNumber
    Implements IDisposable

    Private _Phone As String

    Private _WorkPhone As String

    Private _VacationPhone As String
    Private _Mobile As String
    Private _Email As String
    Private _Fax As String
    Private _Pager As String
    Private _Qualifier As String
    Private _URL As String
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
    Public Property URL() As String
        Get
            Return _URL
        End Get
        Set(ByVal Value As String)
            _URL = Value
        End Set
    End Property
    Public Property Qualifier() As String
        Get
            Return _Qualifier
        End Get
        Set(ByVal Value As String)
            _Qualifier = Value
        End Set
    End Property
    Public Property VacationPhone() As String
        Get
            Return _VacationPhone
        End Get
        Set(ByVal Value As String)
            _VacationPhone = Value
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

    End Sub
End Class
Public Class Medication
    Implements IDisposable
    Private sGPI As String
    Private _sDrugDDID As String
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private sCheifComplaint As String
    Private mDrugName As String
    Private mDrugQuantity As String
    Private mWrittendate As String
    Private mProdCode As String
    Private mStrength As String
    Private mStrengthUnits As String
    Private mcodingsystem As String
    Private mMedicationType As String
    Private mFreeTextBrandName As String
    Private sDuration As String
    Private sRefills As String
    Private sFrequency As String
    Private sDuration1 As String
    Private nDrugDDID As Int64
    Private _mpid As Int32
    Private nProviderID As Int64
    Private sPharmacy As String
    Private sMedicationDate As String
    Private sStartDate As String
    Private _IsPrescription As String
    Private sEndDate As String
    Private sStatus As String
    Private sReason As String
    Private _ReasonConceptID As String
    Private _ReasonConceptDesc As String
    Private nDDID As Int64
    Private sRoute As String
    Private sDrugform As String
    Private sRxNormCode As String
    Private sGenericName As String
    Private sUser As String
    Private nPatientID As Int64
    Private nVisitID As Int64
    Private _nIsNarcotics As Int16
    Private _RxMedDMSID As Long
    Private _Rx_sPrescriberNotes As String
    Private _Rx_sName As String
    Private _Renewed As String
    Private _nPrescriptionId As Long
    Private _MedicationID As Int64
    Private _State As String
    Private _sPBMSourceName As String
    Private _Rx_sNotes As String
    Private _Rx_bMaySubstitute As String
    Private _Rx_sMethod As String
    'Added by Rahul Patel on 25-10-2010
    'For Hybrid Database change for RxNorm database setting
    Private _FDACode As String
    Private _HL7Code As String
    'End of Coed Added by Rahul patel
    Private _FrequencyAbb As String
    Private _FrequencyDesc As String
    Private _isMedicationAdministered As Boolean
    Dim _Rx_sPhone As String
    Dim _Rx_sFax As String
    Dim _Rx_sEmail As String
    Dim _Rx_sZip As String
    Dim _Rx_sState As String
    Dim _Rx_sCity As String
    Dim _Rx_sAddressline2 As String
    Dim _Rx_sAddressline1 As String
    Dim _Rx_sNCPDPID As String
    Dim _sCategoryValue As String
    Dim _sValuesetdescription As String
    Dim _GUID As String
    Dim _PreviousMedicationId As Long
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub
    Public Property PreviousMedicationid As Long
        Get
            Return _PreviousMedicationId
        End Get
        Set(value As Long)
            _PreviousMedicationId = value
        End Set
    End Property
    Public Property GUID As String
        Get
            Return _GUID
        End Get
        Set(value As String)
            _GUID = value
        End Set
    End Property
    Public Property sDrugDDID() As String
        Get
            Return _sDrugDDID
        End Get
        Set(ByVal value As String)
            _sDrugDDID = value
        End Set
    End Property
    Public Property GPI() As String
        Get
            Return sGPI
        End Get
        Set(ByVal value As String)
            sGPI = value
        End Set
    End Property
    Public Property IsMedicationAdministered() As Boolean
        Get
            Return _isMedicationAdministered
        End Get
        Set(ByVal value As Boolean)
            _isMedicationAdministered = value
        End Set
    End Property
    Public Property FrequencyAbb() As String
        Get
            Return _FrequencyAbb
        End Get
        Set(ByVal value As String)
            _FrequencyAbb = value
        End Set
    End Property
    Public Property FrequencyDesc() As String
        Get
            Return _FrequencyDesc
        End Get
        Set(ByVal value As String)
            _FrequencyDesc = value
        End Set
    End Property
    'Added by Rahul Patel on 25-10-2010
    'For Hybrid Database change for RxNorm database setting
    Public Property FDACode() As String
        Get
            Return _FDACode
        End Get
        Set(ByVal value As String)
            _FDACode = value
        End Set
    End Property
    Public Property HL7Code() As String
        Get
            Return _HL7Code
        End Get
        Set(ByVal value As String)
            _HL7Code = value
        End Set
    End Property
    'End of code by Rahul Patel on 25-10-2010
    Public Property User() As String
        Get
            Return sUser
        End Get
        Set(ByVal value As String)
            sUser = value
        End Set
    End Property
    Public Property GenericName() As String
        Get
            Return sGenericName
        End Get
        Set(ByVal value As String)
            sGenericName = value
        End Set
    End Property
    Public Property RxNormCode() As String
        Get
            Return sRxNormCode
        End Get
        Set(ByVal value As String)
            sRxNormCode = value
        End Set
    End Property
    Public Property DrugForm() As String
        Get
            Return sDrugform
        End Get
        Set(ByVal value As String)
            sDrugform = value
        End Set
    End Property
    Public Property FreeTextBrandName() As String
        Get
            Return mFreeTextBrandName
        End Get
        Set(ByVal value As String)
            mFreeTextBrandName = value
        End Set
    End Property
    Public Property MedicationType() As String
        Get
            Return mMedicationType
        End Get
        Set(ByVal value As String)
            mMedicationType = value
        End Set
    End Property
    Public Property CodingSystem() As String
        Get
            Return mcodingsystem
        End Get
        Set(ByVal value As String)
            mcodingsystem = value
        End Set
    End Property
    Public Property DrugStrength() As String
        Get
            Return mStrength
        End Get
        Set(ByVal value As String)
            mStrength = value
        End Set
    End Property
    Public Property StrengthUnits() As String
        Get
            Return mStrengthUnits
        End Get
        Set(ByVal value As String)
            mStrengthUnits = value
        End Set
    End Property

    Public Property WrittenDate() As String
        Get
            Return mWrittendate
        End Get
        Set(ByVal value As String)
            mWrittendate = value
        End Set
    End Property
    Public Property DrugQuantity() As String
        Get
            Return mDrugQuantity
        End Get
        Set(ByVal value As String)
            mDrugQuantity = value
        End Set
    End Property

    Public Property DrugName() As String
        Get
            Return mDrugName
        End Get
        Set(ByVal value As String)
            mDrugName = value
        End Set
    End Property
    Public Property mpid() As Int32
        Get
            Return _mpid
        End Get
        Set(ByVal value As Int32)
            _mpid = value
        End Set
    End Property

    Public Property DrugDDID() As Int64
        Get
            Return nDrugDDID
        End Get
        Set(ByVal value As Int64)
            nDrugDDID = value
        End Set
    End Property
    Public Property ProviderID() As Int64
        Get
            Return nProviderID
        End Get
        Set(ByVal value As Int64)
            nProviderID = value
        End Set
    End Property

    Public Property PatientID() As Int64
        Get
            Return nPatientID
        End Get
        Set(ByVal value As Int64)
            nPatientID = value
        End Set
    End Property
    Public Property VisitID() As Int64
        Get
            Return nVisitID
        End Get
        Set(ByVal value As Int64)
            nVisitID = value
        End Set
    End Property

    Public Property Days() As String
        Get
            Return sDuration
        End Get
        Set(ByVal value As String)
            sDuration = value
        End Set
    End Property

    Public Property Refills() As String
        Get
            Return sRefills
        End Get
        Set(ByVal value As String)
            sRefills = value
        End Set
    End Property

    Public Property CheifComplaint() As String
        Get
            Return sCheifComplaint
        End Get
        Set(ByVal value As String)
            sCheifComplaint = value
        End Set
    End Property
    Public Property Frequency() As String
        Get
            Return sFrequency
        End Get
        Set(ByVal value As String)
            sFrequency = value
        End Set
    End Property
    Public Property Duration() As String
        Get
            Return sDuration1
        End Get
        Set(ByVal value As String)
            sDuration1 = value
        End Set
    End Property

    Public Property Pharmacy() As String
        Get
            Return sPharmacy
        End Get
        Set(ByVal value As String)
            sPharmacy = value
        End Set
    End Property

    Public Property MedicationDate() As String
        Get
            Return sMedicationDate
        End Get
        Set(ByVal value As String)
            sMedicationDate = value
        End Set
    End Property


    Public Property StartDate() As String
        Get
            Return sStartDate
        End Get
        Set(ByVal value As String)
            sStartDate = value
        End Set
    End Property
    Public Property IsPrescription() As Boolean
        Get
            Return _IsPrescription
        End Get
        Set(ByVal value As Boolean)
            _IsPrescription = value
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Return sEndDate
        End Get
        Set(ByVal value As String)
            sEndDate = value
        End Set
    End Property


    Public Property Status() As String
        Get
            Return sStatus
        End Get
        Set(ByVal value As String)
            sStatus = value
        End Set
    End Property
    Public Property Reason() As String
        Get
            Return sReason
        End Get
        Set(ByVal value As String)
            sReason = value
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
   
    Public Property DDID() As Int64
        Get
            Return nDDID
        End Get
        Set(ByVal value As Int64)
            nDDID = value
        End Set
    End Property

    Public Property Route() As String
        Get
            Return sRoute
        End Get
        Set(ByVal value As String)
            sRoute = value
        End Set
    End Property
    '***********************************************************************
    Public Property ProdCode() As String
        Get
            Return mProdCode
        End Get
        Set(ByVal value As String)
            mProdCode = value
        End Set
    End Property
    '***********************************************************************

    ''Added by Mayuri:20130403-before reconciliation we need to carry forward records of active medications inetranlly
    Public Property IsNarcotics() As Int16
        Get
            Return _nIsNarcotics
        End Get
        Set(ByVal Value As Int16)
            _nIsNarcotics = Value
        End Set
    End Property
    Public Property RxMedDMSID() As Long
        Get
            Return _RxMedDMSID
        End Get
        Set(ByVal value As Long)
            _RxMedDMSID = value
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
    Public Property Rx_PhName() As String
        Get
            Return _Rx_sName
        End Get
        Set(ByVal value As String)
            _Rx_sName = value
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
    Public Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal value As String)
            _State = value
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
    Public Property Rx_Notes() As String
        Get
            Return _Rx_sNotes
        End Get
        Set(ByVal value As String)
            _Rx_sNotes = value
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
    Public Property Rx_Method() As String
        Get
            Return _Rx_sMethod
        End Get
        Set(ByVal value As String)
            _Rx_sMethod = value
        End Set
    End Property
    Public Property Rx_sNCPDPID() As String
        Get
            Return _Rx_sNCPDPID
        End Get
        Set(ByVal Value As String)
            _Rx_sNCPDPID = Value
        End Set
    End Property
    Public Property Rx_sAddressline1() As String
        Get
            Return _Rx_sAddressline1
        End Get
        Set(ByVal Value As String)
            _Rx_sAddressline1 = Value
        End Set
    End Property

    Public Property Rx_sAddressline2() As String
        Get
            Return _Rx_sAddressline2
        End Get
        Set(ByVal Value As String)
            _Rx_sAddressline2 = Value
        End Set
    End Property


    Public Property Rx_sCity() As String
        Get
            Return _Rx_sCity
        End Get
        Set(ByVal Value As String)
            _Rx_sCity = Value
        End Set
    End Property

    Public Property Rx_sState() As String
        Get
            Return _Rx_sState
        End Get
        Set(ByVal Value As String)
            _Rx_sState = Value
        End Set
    End Property

    Public Property Rx_sZip() As String
        Get
            Return _Rx_sZip
        End Get
        Set(ByVal Value As String)
            _Rx_sZip = Value
        End Set
    End Property


    Public Property Rx_sEmail() As String
        Get
            Return _Rx_sEmail
        End Get
        Set(ByVal Value As String)
            _Rx_sEmail = Value
        End Set
    End Property


    Public Property Rx_sFax() As String
        Get
            Return _Rx_sFax
        End Get
        Set(ByVal Value As String)
            _Rx_sFax = Value
        End Set
    End Property


    Public Property Rx_sPhone() As String
        Get
            Return _Rx_sPhone
        End Get
        Set(ByVal Value As String)
            _Rx_sPhone = Value
        End Set
    End Property
    Public Property ValuesetOID() As String
        Get
            Return _sCategoryValue
        End Get
        Set(ByVal value As String)
            _sCategoryValue = value
        End Set
    End Property
    Public Property Valueset() As String
        Get
            Return _sValuesetdescription
        End Get
        Set(ByVal value As String)
            _sValuesetdescription = value
        End Set
    End Property

End Class

Public Class FamilyHistory
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

    Private _FmlyHxDescription As String = ""
    Private _FmlyHxQualifiers As String = ""
    Private _FmlyHxComments As String = ""
    Private _FmlyHxDateReported As String = ""
    Private _FmlyHxConceptID As String = ""
    Private _FmlyHxStatus As String = ""
    Private _FmlyHxReaction As String = ""
    Private _FmlyHxMemberId As String = ""
    Private _FmlyHxRelation As String = ""
    Private _FmlyHxRelConceptID As String = ""
    Private _FmlyHxHistoryId As String = ""
    Private _FmlyMemberCode As String = ""
    Private _OccurDate As String = ""


    Public Sub New()
        MyBase.New()
    End Sub
    Public Property OccurDate As String
        Get
            Return _OccurDate
        End Get
        Set(value As String)
            _OccurDate = value
        End Set
    End Property
    Public Property FmlyHxConceptID() As String
        Get
            Return _FmlyHxConceptID
        End Get
        Set(ByVal value As String)
            _FmlyHxConceptID = value
        End Set
    End Property
    Public Property FmlyHxDescription() As String
        Get
            Return _FmlyHxDescription
        End Get
        Set(ByVal value As String)
            _FmlyHxDescription = value
        End Set
    End Property
    Public Property FmlyHxQualifiers() As String
        Get
            Return _FmlyHxQualifiers
        End Get
        Set(ByVal value As String)
            _FmlyHxQualifiers = value
        End Set
    End Property
    Public Property FmlyHxComments() As String
        Get
            Return _FmlyHxComments
        End Get
        Set(ByVal value As String)
            _FmlyHxComments = value
        End Set
    End Property
    Public Property FmlyHxDateReported() As String
        Get
            Return _FmlyHxDateReported
        End Get
        Set(ByVal value As String)
            _FmlyHxDateReported = value
        End Set
    End Property
    Public Property FmlyHxStatus() As String
        Get
            Return _FmlyHxStatus
        End Get
        Set(ByVal value As String)
            _FmlyHxStatus = value
        End Set
    End Property
    Public Property FmlyHxReaction() As String
        Get
            Return _FmlyHxReaction
        End Get
        Set(ByVal value As String)
            _FmlyHxReaction = value
        End Set
    End Property
    Public Property FmlyHxMemberId() As String
        Get
            Return _FmlyHxMemberId
        End Get
        Set(ByVal value As String)
            _FmlyHxMemberId = value
        End Set
    End Property
    Public Property FmlyHxRelation() As String
        Get
            Return _FmlyHxRelation
        End Get
        Set(ByVal value As String)
            _FmlyHxRelation = value
        End Set
    End Property
    Public Property FmlyHxRelConceptID() As String
        Get
            Return _FmlyHxRelConceptID
        End Get
        Set(ByVal value As String)
            _FmlyHxRelConceptID = value
        End Set
    End Property

    Public Property FmlyHxHistoryId() As String
        Get
            Return _FmlyHxHistoryId
        End Get
        Set(ByVal value As String)
            _FmlyHxHistoryId = value
        End Set
    End Property
    Public Property FmlyMemberCode() As String
        Get
            Return _FmlyMemberCode
        End Get
        Set(ByVal value As String)
            _FmlyMemberCode = value
        End Set
    End Property


End Class

Public Class SocialHistory
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

    Private _SocialHxDescription As String = ""
    Private _SocialHxQualifiers As String = ""
    Private _SocialHxComments As String = ""
    Private _SocialHxDateReported As String = ""
    Private _SocialHXConceptID As String = ""
    Private _SocialHxStatus As String = ""
    Private _SocialHxCategory As String = ""
    Private _SocialOnsetDate As String = ""
    Private _SocialEndDate As String = ""
    Private _TobaccoUseCode As String
    Private _SnomedDescription As String = ""
    Private _HistoricalStatusDesc As String = ""
    Public Sub New()
        MyBase.New()
    End Sub
    Public Property HistoricalStatusDesc As String
        Get
            Return _HistoricalStatusDesc
        End Get
        Set(value As String)
            _HistoricalStatusDesc = value
        End Set
    End Property
    Public Property SnomedDesc As String
        Get
            Return _SnomedDescription
        End Get
        Set(value As String)
            _SnomedDescription = value
        End Set
    End Property
    Public Property TobaccoUseCode As String
        Get
            Return _TobaccoUseCode
        End Get
        Set(value As String)
            _TobaccoUseCode = value
        End Set
    End Property
    Public Property SocialOnsetDate As String
        Get
            Return _SocialOnsetDate
        End Get
        Set(value As String)
            _SocialOnsetDate = value
        End Set
    End Property
    Public Property SocialEndDate As String
        Get
            Return _SocialEndDate
        End Get
        Set(value As String)
            _SocialEndDate = value
        End Set
    End Property
    Public Property SocialHxCategory() As String
        Get
            Return _SocialHxCategory
        End Get
        Set(ByVal value As String)
            _SocialHxCategory = value
        End Set
    End Property
    Public Property SocialHxConceptID() As String
        Get
            Return _SocialHXConceptID
        End Get
        Set(ByVal value As String)
            _SocialHXConceptID = value
        End Set
    End Property

    Public Property SocialHxDescription() As String
        Get
            Return _SocialHxDescription
        End Get
        Set(ByVal value As String)
            _SocialHxDescription = value
        End Set
    End Property
    Public Property SocialHxQualifiers() As String
        Get
            Return _SocialHxQualifiers
        End Get
        Set(ByVal value As String)
            _SocialHxQualifiers = value
        End Set
    End Property
    Public Property SocialHxComments() As String
        Get
            Return _SocialHxComments
        End Get
        Set(ByVal value As String)
            _SocialHxComments = value
        End Set
    End Property
    Public Property SocialHxDateReported() As String
        Get
            Return _SocialHxDateReported
        End Get
        Set(ByVal value As String)
            _SocialHxDateReported = value
        End Set
    End Property
    Public Property SocialHxStatus() As String
        Get
            Return _SocialHxStatus
        End Get
        Set(ByVal value As String)
            _SocialHxStatus = value
        End Set
    End Property
End Class

Public Class Insurance
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

    Private _InsuranceName As String = ""
    Private _InsSubsGender As String = ""
    Private _InsSubsAddressLine1 As String = ""
    Private _InsSubsAddressLine2 As String = ""
    Private _InsSubsCity As String = ""
    Private _InsSubsState As String = ""
    Private _InsSubsZip As String = ""
    Private sInsuranceId As String = ""
    Private sGroupNo As String = ""
    Private sInsSubscriberName As String = ""
    Private sInsRelation As String = ""
    Private _Group As String = ""
    Private sMemberId As String = ""
    Private sInsStartdate As String = ""
    Private sInsEndDate As String = ""
    Private sTypeCode As String = ""
    Private sInsuranceType As String = ""

    Private sInsuranceSubscriber1 As PersonName
    Private sinsuranceSubscriber2 As PersonName
    Private sInsuranceGuarantor As PersonName

    Private sInsCompanyName As String = ""
    Private sInsCompanyPhoneNumber As String = ""



    Public Sub New()
        MyBase.New()
    End Sub


    Public Property InsSubsAddressLine2() As String
        Get
            Return _InsSubsAddressLine2
        End Get
        Set(ByVal value As String)
            _InsSubsAddressLine2 = value
        End Set
    End Property
    Public Property InsSubsCity() As String
        Get
            Return _InsSubsCity
        End Get
        Set(ByVal value As String)
            _InsSubsCity = value
        End Set
    End Property
    Public Property InsSubsState() As String
        Get
            Return _InsSubsState
        End Get
        Set(ByVal value As String)
            _InsSubsState = value
        End Set
    End Property
    Public Property InsSubsZip() As String
        Get
            Return _InsSubsZip
        End Get
        Set(ByVal value As String)
            _InsSubsZip = value
        End Set
    End Property
    Public Property InsSubsAddressLine1() As String
        Get
            Return _InsSubsAddressLine1
        End Get
        Set(ByVal value As String)
            _InsSubsAddressLine1 = value
        End Set
    End Property

    Public Property InsSubsGender() As String
        Get
            Return _InsSubsGender
        End Get
        Set(ByVal value As String)
            _InsSubsGender = value
        End Set
    End Property

    Public Property InsStartdate() As String
        Get
            Return sInsStartdate
        End Get
        Set(ByVal value As String)
            sInsStartdate = value
        End Set
    End Property

    Public Property InsEndDate() As String
        Get
            Return sInsEndDate
        End Get
        Set(ByVal value As String)
            sInsEndDate = value
        End Set
    End Property


    Public Property InsuranceName() As String
        Get
            Return _InsuranceName
        End Get
        Set(ByVal value As String)
            _InsuranceName = value
        End Set
    End Property

    Public Property GroupNo() As String
        Get
            Return sGroupNo
        End Get
        Set(ByVal value As String)
            sGroupNo = value
        End Set
    End Property

    Public Property InsuranceId() As String
        Get
            Return sInsuranceId
        End Get
        Set(ByVal value As String)
            sInsuranceId = value
        End Set
    End Property


    Public Property InsSubscriberName() As String
        Get
            Return sInsSubscriberName
        End Get
        Set(ByVal value As String)
            sInsSubscriberName = value
        End Set
    End Property

    Public Property InsRelation() As String
        Get
            Return sInsRelation
        End Get
        Set(ByVal value As String)
            sInsRelation = value
        End Set
    End Property

    Public Property InsTypeCode() As String
        Get
            Return sTypeCode
        End Get
        Set(ByVal value As String)
            sTypeCode = value
        End Set
    End Property
    Public Property InsuranceType() As String
        Get
            Return sInsuranceType
        End Get
        Set(ByVal value As String)
            sInsuranceType = value
        End Set
    End Property
    Public Property InsuranceSubscriber1() As PersonName
        Get
            If IsNothing(sInsuranceSubscriber1) Then
                sInsuranceSubscriber1 = New PersonName
            End If
            Return sInsuranceSubscriber1
        End Get
        Set(ByVal value As PersonName)
            sInsuranceSubscriber1 = value
        End Set
    End Property

    Public Property InsuranceSubscriber2() As PersonName
        Get
            If IsNothing(sinsuranceSubscriber2) Then
                sinsuranceSubscriber2 = New PersonName
            End If
            Return sinsuranceSubscriber2
        End Get
        Set(ByVal value As PersonName)
            sinsuranceSubscriber2 = value
        End Set
    End Property
    Public Property InsuranceGuarantor() As PersonName
        Get
            If IsNothing(sInsuranceGuarantor) Then
                sInsuranceGuarantor = New PersonName
            End If
            Return sInsuranceGuarantor
        End Get
        Set(ByVal value As PersonName)
            sInsuranceGuarantor = value
        End Set
    End Property

    Public Property InsCompanyName As String
        Get
            Return sInsCompanyName
        End Get
        Set(value As String)
            sInsCompanyName = value
        End Set
    End Property

    Public Property InsCompanyPhoneNumber As String
        Get
            Return sInsCompanyPhoneNumber
        End Get
        Set(value As String)
            sInsCompanyPhoneNumber = value
        End Set
    End Property
   

    
End Class
Public Class Allergies
    Implements IDisposable
    Private sReactionCode As String = ""
    Private mProductcode As String = ""
    Private mProductName As String = ""
    Private mSeverity As String = ""
    Private sEffectiveStartTime As String
    Private sEffectiveEndTime As String
    Private sAllergyType As String = ""
    Private sComments As String = ""
    Private sReaction As String = ""
    Private sStatus As String = ""
    Private sConceptID As String = ""
    'Added by Rahul Patel on 25-10-2010
    'For Hybrid Database change for RxNorm database setting
    Private sRxNormID As String = ""
    'End of code added by  rahul patel
    Private disposedValue As Boolean = False        ' To detect redundant calls
    'Added by Rahul Patel on 25-10-2010
    'For Hybrid Database change for RxNorm database setting
    Private _allergyStartTime As String
    Private _allergyEndTime As String
    Private _concernstatus As String
    Private _AllergyIntoleranceType As String
    Public Property AllergyIntoleranceType As String
        Get
            Return _AllergyIntoleranceType
        End Get
        Set(value As String)
            _AllergyIntoleranceType = value
        End Set
    End Property
    Public Property ConcernStatus As String
        Get
            Return _concernstatus
        End Get
        Set(value As String)
            _concernstatus = value
        End Set
    End Property
    Public Property AllergyStartTime As String
        Get
            Return _allergyStartTime
        End Get
        Set(value As String)
            _allergyStartTime = value
        End Set
    End Property
    Public Property AllergyEndTime As String
        Get
            Return _allergyEndTime
        End Get
        Set(value As String)
            _allergyEndTime = value
        End Set
    End Property
    Public Property RxNormID() As String
        Get
            Return sRxNormID
        End Get
        Set(ByVal value As String)
            sRxNormID = value
        End Set
    End Property
    Public Property ReactionCode() As String
        Get
            Return sReactionCode
        End Get
        Set(ByVal value As String)
            sReactionCode = value
        End Set
    End Property
    'End of code added by  rahul patel
    Public Property ConceptID() As String
        Get
            Return sConceptID
        End Get
        Set(ByVal value As String)
            sConceptID = value
        End Set
    End Property
    Public Property EffectiveStartTime() As String
        Get
            Return sEffectiveStartTime
        End Get
        Set(ByVal value As String)
            sEffectiveStartTime = value
        End Set
    End Property
    Public Property EffectiveEndTime() As String
        Get
            Return sEffectiveEndTime
        End Get
        Set(ByVal value As String)
            sEffectiveEndTime = value
        End Set
    End Property
    Public Property ProductCode() As String
        Get
            Return mProductcode
        End Get
        Set(ByVal value As String)
            mProductcode = value
        End Set
    End Property
    Public Property ProductName() As String
        Get
            Return mProductName
        End Get
        Set(ByVal value As String)
            mProductName = value
        End Set
    End Property
    Public Property AllergyType() As String
        Get
            Return sAllergyType
        End Get
        Set(ByVal value As String)
            sAllergyType = value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return sStatus
        End Get
        Set(ByVal value As String)
            sStatus = value
        End Set
    End Property
    Public Property Severity() As String
        Get
            Return mSeverity
        End Get
        Set(ByVal value As String)
            mSeverity = value
        End Set
    End Property

    Public Property Comments() As String
        Get
            Return sComments
        End Get
        Set(ByVal value As String)
            sComments = value
        End Set
    End Property
    Public Property Reaction() As String
        Get
            Return sReaction
        End Get
        Set(ByVal value As String)
            sReaction = value
        End Set
    End Property
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
Public Class Conditions
    Implements IDisposable
    Private sProblemType As String = ""
    Private sConditionType As String = ""
    Private sEffectiveStartTime As String
    Private sEffectiveEndTime As String
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
    Public Property EffectiveStartTime() As String
        Get
            Return sEffectiveStartTime
        End Get
        Set(ByVal value As String)
            sEffectiveStartTime = value
        End Set
    End Property
    Public Property EffectiveEndTime() As String
        Get
            Return sEffectiveEndTime
        End Get
        Set(ByVal value As String)
            sEffectiveEndTime = value
        End Set
    End Property
    Public Property ConditionType() As String
        Get
            Return sConditionType
        End Get
        Set(ByVal value As String)
            sConditionType = value
        End Set
    End Property
    Public Property ProblemType() As String
        Get
            Return sProblemType
        End Get
        Set(ByVal value As String)
            sProblemType = value
        End Set
    End Property
End Class
Public Class OrderComments
    Implements IDisposable

    Private oReviewedBy As String = ""
    Private opatientnote As String = ""
    Private olabom_orderid As Int64 = 0
    Public Property ReviewedBy As String
        Get
            Return oReviewedBy
        End Get
        Set(ByVal value As String)
            oReviewedBy = value
        End Set
    End Property
    Public Property Patientnote As String
        Get
            Return opatientnote
        End Get
        Set(ByVal value As String)
            opatientnote = value
        End Set
    End Property
    Public Property Labom_OrderId As String
        Get
            Return olabom_orderid
        End Get
        Set(ByVal value As String)
            olabom_orderid = value
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class OrderCommentsCol
    Inherits CollectionBase
    Implements IDisposable

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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As OrderComments
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), OrderComments)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As OrderComments)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class Order
    Implements IDisposable
    Private oordercommentscol As OrderCommentsCol
    Private olabtests As LabTestCol
    Public Property OrderComments() As OrderCommentsCol
        Get
            If IsNothing(oordercommentscol) Then
                oordercommentscol = New OrderCommentsCol
            End If
            Return oordercommentscol
        End Get
        Set(ByVal Value As OrderCommentsCol)
            oordercommentscol = Value
        End Set
    End Property ' To d
    Public Property OrderTests() As LabTestCol
        Get
            If IsNothing(olabtests) Then
                olabtests = New LabTestCol
            End If
            Return olabtests
        End Get
        Set(ByVal Value As LabTestCol)
            olabtests = Value
        End Set
    End Property ' To d


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class OrderCol
    Inherits CollectionBase
    Implements IDisposable
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Order
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Order)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Order)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class LabTest
    Implements IDisposable

    Private oLabResults As LabResultsCol
    Private _LabLocationDetails As LabLocation
    Private mProductcode As String = ""
    Private mProductName As String = ""
    Private _ScheduledDateTime As String = ""
    Private _CPTCode As String = ""
    Private _OrderId As String = ""
    Private _TestId As String = ""
    Private _GUID As String = ""
    Private disposedValue As Boolean = False
    Public Property LabLocationDetails As LabLocation
        Get
            Return _LabLocationDetails
        End Get
        Set(value As LabLocation)
            _LabLocationDetails = value
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
    Public Property GUID() As String
        Get
            Return _GUID
        End Get
        Set(ByVal value As String)
            _GUID = value
        End Set
    End Property
    Public Property OrderId() As String
        Get
            Return _OrderId
        End Get
        Set(ByVal value As String)
            _OrderId = value
        End Set
    End Property
    Public Property TestId() As String
        Get
            Return _TestId
        End Get
        Set(ByVal value As String)
            _TestId = value
        End Set
    End Property
    Public Property ScheduledDateTime() As String
        Get
            Return _ScheduledDateTime
        End Get
        Set(ByVal value As String)
            _ScheduledDateTime = value
        End Set
    End Property
    Public Property TestLOINCcode() As String
        Get
            Return mProductcode
        End Get
        Set(ByVal value As String)
            mProductcode = value
        End Set
    End Property
    Public Property TestName() As String
        Get
            Return mProductName
        End Get
        Set(ByVal value As String)
            mProductName = value
        End Set
    End Property
    Public Property LabResults() As LabResultsCol
        Get
            If IsNothing(oLabResults) Then
                oLabResults = New LabResultsCol
            End If
            Return oLabResults
        End Get
        Set(ByVal Value As LabResultsCol)
            oLabResults = Value
        End Set
    End Property ' To detect redundant calls
    Public Property EffectiveStartTime() As String
        Get
            Return mProductcode
        End Get
        Set(ByVal value As String)
            mProductcode = value
        End Set
    End Property
   

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
Public Class LabTestCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As LabTest
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), LabTest)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As LabTest)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class LabResults
    Implements IDisposable

    Private labotrd_ResultLineNo As String = ""
    Private labotrd_TestResultNumber As String = ""
    Private labotrd_ResultNameID As String = ""
    Private labotrd_ResultName As String = ""
    Private labotrd_ResultValue As String = ""
    Private labotrd_ResultUnit As String = ""
    Private labotrd_ResultRange As String = ""
    Private labotrd_ResultType As String = ""
    Private labotrd_AbnormalFlag As String = ""
    Private labotrd_ResultComment As String = ""
    Private labotrd_ResultWord As String = ""
    Private labotrd_ResultDMSID As String = ""
    Private labotrd_ResultUserID As String = ""
    Private labotrd_ResultDateTime As String = ""
    Private labotrd_IsFinished As String = ""
    Private labotrd_LOINCID As String = ""
    Private labotrd_ReasonConceptID As String = ""
    Private labotrd_ReasonICD9 As String = ""
    Private labotrd_ReasonICD10 As String = ""
    Private labotrd_ReasonLOINC As String = ""

    Private labotd_LOINCCode As String = ""
    Private labotrd_TestName As String = ""
    Private labom_TransactionDate As String = ""
    Private labom_ProviderID As String = ""
    Private labotr_TestResultDateTime As String = ""
    Private labotr_SpecimenReceivedDateTime As String = ""
    Private labotr_ResultTransferDateTime As String = ""
    Private labom_ReceivingFacilityCode As String = ""
    Private labom_OrderNoID As String = ""
    Private labotrd_TestCode As String = ""
    Private _OrderStatusNo As Integer = 0
    Private _IsAcknowledge As Boolean = False
    Private disposedValue As Boolean = False ' To detect redundant calls
    Private scategoryvalue As String = ""
    Private sValueset As String = ""
    Private _scheduleDate As String = ""
    Public Property ScheduleDate As String
        Get
            Return _scheduleDate
        End Get
        Set(value As String)
            _scheduleDate = value
        End Set
    End Property
    Public Property Valueset() As String
        Get
            Return scategoryvalue
        End Get
        Set(ByVal value As String)
            scategoryvalue = value
        End Set
    End Property
    Public Property OrderStatusNo() As String
        Get
            Return _OrderStatusNo
        End Get
        Set(ByVal value As String)
            _OrderStatusNo = value
        End Set
    End Property
    Public Property ResultType() As String
        Get
            Return labotrd_ResultType
        End Get
        Set(ByVal value As String)
            labotrd_ResultType = value
        End Set
    End Property
    Public Property TestCode() As String
        Get
            Return labotrd_TestCode
        End Get
        Set(ByVal value As String)
            labotrd_TestCode = value
        End Set
    End Property
    'Public Property ResultLOINCID() As String
    '    Get
    '        Return labotrd_LOINCID
    '    End Get
    '    Set(ByVal value As String)
    '        labotrd_LOINCID = value
    '    End Set
    'End Property
    Public Property ResultReasonConceptID() As String
        Get
            Return labotrd_ReasonConceptID
        End Get
        Set(ByVal value As String)
            labotrd_ReasonConceptID = value
        End Set
    End Property
    Public Property ResultReasonICD9() As String
        Get
            Return labotrd_ReasonICD9
        End Get
        Set(ByVal value As String)
            labotrd_ReasonICD9 = value
        End Set
    End Property
    Public Property ResultReasonICD10() As String
        Get
            Return labotrd_ReasonICD10
        End Get
        Set(ByVal value As String)
            labotrd_ReasonICD10 = value
        End Set
    End Property
    Public Property ResultReasonLOINC() As String
        Get
            Return labotrd_ReasonLOINC
        End Get
        Set(ByVal value As String)
            labotrd_ReasonLOINC = value
        End Set
    End Property
    'Public Property ResultReasonICDRev() As Int16
    '    Get
    '        Return labotrd_ReasonICDRev
    '    End Get
    '    Set(ByVal value As Int16)
    '        labotrd_ReasonICDRev = value
    '    End Set
    'End Property
    Public Property ResultLOINCID() As String
        Get
            Return labotrd_LOINCID
        End Get
        Set(ByVal value As String)
            labotrd_LOINCID = value
        End Set
    End Property
    Public Property TestLOINCID() As String
        Get
            Return labotd_LOINCCode
        End Get
        Set(ByVal value As String)
            labotd_LOINCCode = value
        End Set
    End Property
    Public Property ResultComment() As String
        Get
            Return labotrd_ResultComment
        End Get
        Set(ByVal value As String)
            labotrd_ResultComment = value
        End Set
    End Property
    Public Property ResultRange() As String
        Get
            Return labotrd_ResultRange
        End Get
        Set(ByVal value As String)
            labotrd_ResultRange = value
        End Set
    End Property
    Public Property ResultUnit() As String
        Get
            Return labotrd_ResultUnit
        End Get
        Set(ByVal value As String)
            labotrd_ResultUnit = value
        End Set
    End Property
    Public Property AbnormalFlag() As String
        Get
            Return labotrd_AbnormalFlag
        End Get
        Set(ByVal value As String)
            labotrd_AbnormalFlag = value
        End Set
    End Property
    Public Property ResultValue() As String
        Get
            Return labotrd_ResultValue
        End Get
        Set(ByVal value As String)
            labotrd_ResultValue = value
        End Set
    End Property
    Public Property ResultName() As String
        Get
            Return labotrd_ResultName
        End Get
        Set(ByVal value As String)
            labotrd_ResultName = value
        End Set
    End Property
    Public Property TestName() As String
        Get
            Return labotrd_TestName
        End Get
        Set(ByVal value As String)
            labotrd_TestName = value
        End Set
    End Property
    Public Property ResultDate() As String
        Get
            Return labotrd_ResultDateTime
        End Get
        Set(ByVal value As String)
            labotrd_ResultDateTime = value
        End Set
    End Property
    Public Property ProviderName() As String
        Get
            Return labom_ProviderID
        End Get
        Set(ByVal value As String)
            labom_ProviderID = value
        End Set
    End Property
    Public Property SpecimenDate() As String
        Get
            Return labotr_SpecimenReceivedDateTime
        End Get
        Set(ByVal value As String)
            labotr_SpecimenReceivedDateTime = value
        End Set
    End Property
    Public Property LabFacility() As String
        Get
            Return labom_ReceivingFacilityCode
        End Get
        Set(ByVal value As String)
            labom_ReceivingFacilityCode = value
        End Set
    End Property
    Public Property OrderNo() As String
        Get
            Return labom_OrderNoID
        End Get
        Set(ByVal value As String)
            labom_OrderNoID = value
        End Set
    End Property
    Public Property IsAcknowledge() As Boolean
        Get
            Return _IsAcknowledge
        End Get
        Set(ByVal value As Boolean)
            _IsAcknowledge = value
        End Set
    End Property
    Public Property ValusetOID As String
        Get
            Return sValueset
        End Get
        Set(value As String)
            sValueset = value
        End Set
    End Property

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
Public Class LabResultsCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As LabResults
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), LabResults)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As LabResults)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class VitalsCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Vitals
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Vitals)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Vitals)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class AllergiesCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Allergies
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Allergies)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Allergies)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class ConditionsCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Conditions
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Conditions)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Conditions)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class MedicationsCol
    Inherits List(Of Medication)

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    'Remove Item at specified index
    'Public Sub Remove(ByVal index As Integer)
    '    ' Check to see if there is a widget at the supplied index.
    '    If index > Count - 1 Or index < 0 Then
    '        ' If no object exists, a messagebox is shown and the operation is 
    '        ' cancelled.
    '        'System.Windows.Forms.MessageBox.Show("Index not valid!")
    '    Else
    '        ' Invokes the RemoveAt method of the List object.
    '        List.RemoveAt(index)
    '    End If
    'End Sub
    '' This line declares the Item property as ReadOnly, and 
    '' declares that it will return a SentFax object.
    'Public ReadOnly Property Item(ByVal index As Integer) As Medication
    '    Get
    '        ' The appropriate item is retrieved from the List object and 
    '        ' explicitly cast to the SentFax type, then returned to the 
    '        ' caller.
    '        Return CType(List.Item(index), Medication)
    '    End Get
    'End Property
    '' Restricts to SentFax types, items that can be added to the collection.
    'Public Sub Add(ByVal oConditionfield As Medication)
    '    ' Invokes Add method of the List object to add a SentFax.
    '    List.Add(oConditionfield)
    'End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class FamilyHistoryCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As FamilyHistory
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), FamilyHistory)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As FamilyHistory)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class SocialHistoryCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As SocialHistory
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), SocialHistory)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As SocialHistory)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class InsuranceCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Insurance
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Insurance)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Insurance)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class AdvDirectiveCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Advancedirective
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Advancedirective)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Advancedirective)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class ImmunizationCol
    Inherits List(Of Immunization)

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    'Remove Item at specified index
    'Public Sub Remove(ByVal index As Integer)
    '    ' Check to see if there is a widget at the supplied index.
    '    If index > Count - 1 Or index < 0 Then
    '        ' If no object exists, a messagebox is shown and the operation is 
    '        ' cancelled.
    '        'System.Windows.Forms.MessageBox.Show("Index not valid!")
    '    Else
    '        ' Invokes the RemoveAt method of the List object.
    '        List.RemoveAt(index)
    '    End If
    'End Sub
    '' This line declares the Item property as ReadOnly, and 
    '' declares that it will return a SentFax object.
    'Public ReadOnly Property Item(ByVal index As Integer) As Immunization
    '    Get
    '        ' The appropriate item is retrieved from the List object and 
    '        ' explicitly cast to the SentFax type, then returned to the 
    '        ' caller.
    '        Return CType(List.Item(index), Immunization)
    '    End Get
    'End Property
    '' Restricts to SentFax types, items that can be added to the collection.
    'Public Sub Add(ByVal oConditionfield As Immunization)
    '    ' Invokes Add method of the List object to add a SentFax.
    '    List.Add(oConditionfield)
    'End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class ResultsCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Results
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Results)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Results)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class CommentsCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Comments
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Comments)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Comments)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class EncountersCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Encounters
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Encounters)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Encounters)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class ProblemsCol
    Inherits List(Of Problems)

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    'Remove Item at specified index
    'Public Sub Remove(ByVal index As Integer)
    '    ' Check to see if there is a widget at the supplied index.
    '    If index > Count - 1 Or index < 0 Then
    '        ' If no object exists, a messagebox is shown and the operation is 
    '        ' cancelled.
    '        'System.Windows.Forms.MessageBox.Show("Index not valid!")
    '    Else
    '        ' Invokes the RemoveAt method of the List object.
    '        List.RemoveAt(index)
    '    End If
    'End Sub
    '' This line declares the Item property as ReadOnly, and 
    '' declares that it will return a SentFax object.
    'Public ReadOnly Property Item(ByVal index As Integer) As Problems
    '    Get
    '        ' The appropriate item is retrieved from the List object and 
    '        ' explicitly cast to the SentFax type, then returned to the 
    '        ' caller.
    '        Return CType(List.Item(index), Problems)
    '    End Get
    'End Property
    '' Restricts to SentFax types, items that can be added to the collection.
    'Public Sub Add(ByVal oConditionfield As Problems)
    '    ' Invokes Add method of the List object to add a SentFax.
    '    List.Add(oConditionfield)
    'End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class ProceduresCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Procedures
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Procedures)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Procedures)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class Advancedirective
    Implements IDisposable
    Private sAdvancedirectiveType As String
    Private sAdvancedirectiveDescription As String
    Private dtstartdate As String
    Private dtenddate As String
    Private mPersonName As PersonName
    Private _PersonContactAddress As ContactAddress
    Private _PersonContactPhone As PhoneNumber
    Private sAdvDirectiveName As String
    Private sAdvDirectivePatAware As String
    Private sAdvDirectiveThirdParty As String
    Private sAdvDirectiveVerification As String
    Private sAdvDirectiveReviewed As String

    Public Property AdvDirectiveReviewed() As String
        Get
            Return sAdvDirectiveReviewed
        End Get
        Set(ByVal value As String)
            sAdvDirectiveReviewed = value
        End Set
    End Property

    Public Property AdvDirectiveVerification() As String
        Get
            Return sAdvDirectiveVerification
        End Get
        Set(ByVal value As String)
            sAdvDirectiveVerification = value
        End Set
    End Property
    Public Property AdvDirectiveName() As String
        Get
            Return sAdvDirectiveName
        End Get
        Set(ByVal value As String)
            sAdvDirectiveName = value
        End Set
    End Property
    Public Property AdvDirectivePatAware() As String
        Get
            Return sAdvDirectivePatAware
        End Get
        Set(ByVal value As String)
            sAdvDirectivePatAware = value
        End Set
    End Property
    Public Property AdvDirectiveThirdParty() As String
        Get
            Return sAdvDirectiveThirdParty
        End Get
        Set(ByVal value As String)
            sAdvDirectiveThirdParty = value
        End Set
    End Property

    Public Property PersonContactPhone() As PhoneNumber
        Get
           
            Return _PersonContactPhone
        End Get
        Set(ByVal Value As PhoneNumber)
            _PersonContactPhone = Value
        End Set
    End Property
    Public Property PersonContactAddress() As ContactAddress
        Get
            Return _PersonContactAddress
        End Get
        Set(ByVal Value As ContactAddress)
            _PersonContactAddress = Value
        End Set
    End Property
    Public Property PersonName() As PersonName
        Get
            Return mPersonName
        End Get
        Set(ByVal value As PersonName)
            mPersonName = value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            Return dtstartdate
        End Get
        Set(ByVal value As String)
            dtstartdate = value
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Return dtenddate
        End Get
        Set(ByVal value As String)
            dtenddate = value
        End Set
    End Property
    Public Property AdvanceDirectiveType() As String
        Get
            Return sAdvancedirectiveType
        End Get
        Set(ByVal value As String)
            sAdvancedirectiveType = value
        End Set
    End Property
    Public Property AdvancedirectiveDescription() As String
        Get
            Return sAdvancedirectiveDescription
        End Get
        Set(ByVal value As String)
            sAdvancedirectiveDescription = value
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

    Public Sub New()
        mPersonName = New PersonName
        _PersonContactPhone = New PhoneNumber
        _PersonContactAddress = New ContactAddress
    End Sub
End Class
Public Class Immunization
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
    Private dtImmunizationdate As String
    Private mVaccineName As String
    Private mVaccineCode As String 'Added by kanchan on 20100626 
    Private mLotNumber As String
    Private mPregnant As String
    Private mreason As String
    Private sRoute As String 'Added by kanchan on 20100917
    Private sRouteCode As String 'Added by kanchan on 20100917
    Private sReasonConceptID As String
    'Add by Rohit on 20101014
    Private sImmunizationNotes As String
    Private sTransactionTimeGiven As String
    Private sSite As String
    Private sDose As Decimal
    Private sManufacture As String
    Private sadmin_report_refused As String
    Private bPatienthasreaction As Boolean
    Private dtDueDate As String
    Private dtExpirationDate As String
    Private sCPTCode As String
    Private sICDCode As String
    Private sConceptID As String
    Private stradename As String
    Private dAmount As Decimal
    Private sunits As String
    Private sValuesetOID As String = Nothing
    Private sValuesetdescription As String = Nothing
    Private sImmStatus As String = Nothing
    Private Imm_trn_ID As Long = Nothing
    Private _GUID As String = ""
    Private _sNDCCode As String = ""
    Public Property NDCCODE As String
        Get
            Return _sNDCCode
        End Get
        Set(value As String)
            _sNDCCode = value
        End Set
    End Property
    Public Property GUID As String
        Get
            Return _GUID
        End Get
        Set(value As String)
            _GUID = value
        End Set
    End Property
    Public Property ImmtransactionId As Long
        Get
            Return Imm_trn_ID
        End Get
        Set(value As Long)
            Imm_trn_ID = value
        End Set
    End Property
    Public Property ImmunizationStatus As String
        Get
            Return sImmStatus
        End Get
        Set(value As String)
            sImmStatus = value
        End Set
    End Property

    Public Property ImmunizationTrade() As String
        Get
            Return stradename
        End Get
        Set(ByVal value As String)
            stradename = value
        End Set
    End Property
    Public Property ImmunizationNotes() As String
        Get
            Return sImmunizationNotes
        End Get
        Set(ByVal value As String)
            sImmunizationNotes = value
        End Set
    End Property

    Public Property TransactionTimeGiven() As String
        Get
            Return sTransactionTimeGiven
        End Get
        Set(ByVal value As String)
            sTransactionTimeGiven = value
        End Set
    End Property

    Public Property Site() As String
        Get
            Return sSite
        End Get
        Set(ByVal value As String)
            sSite = value
        End Set
    End Property

    Public Property Dose() As Decimal
        Get
            Return sDose
        End Get
        Set(ByVal value As Decimal)
            sDose = value
        End Set
    End Property

    Public Property Manufacture() As String
        Get
            Return sManufacture
        End Get
        Set(ByVal value As String)
            sManufacture = value
        End Set
    End Property
    Public Property admin_report_refused() As String
        Get
            Return sadmin_report_refused
        End Get
        Set(ByVal value As String)
            sadmin_report_refused = value
        End Set
    End Property
    Public Property Patienthasreaction() As Boolean
        Get
            Return bPatienthasreaction
        End Get
        Set(ByVal value As Boolean)
            bPatienthasreaction = value
        End Set
    End Property
    Public Property CPTCode() As String
        Get
            Return sCPTCode
        End Get
        Set(ByVal value As String)
            sCPTCode = value
        End Set
    End Property
    Public Property ICDCode() As String
        Get
            Return sICDCode
        End Get
        Set(ByVal value As String)
            sICDCode = value
        End Set
    End Property
    Public Property ConceptID() As String
        Get
            Return sConceptID
        End Get
        Set(ByVal value As String)
            sConceptID = value
        End Set
    End Property

    Public Property DueDate() As String
        Get
            Return dtDueDate
        End Get
        Set(ByVal value As String)
            dtDueDate = value
        End Set
    End Property

    Public Property ExpirationDate() As String
        Get
            Return dtExpirationDate
        End Get
        Set(ByVal value As String)
            dtExpirationDate = value
        End Set
    End Property
    'End by Rohit on 20101014

    'Added by kanchan on 20100917
    Public Property Route() As String
        Get
            Return sRoute
        End Get
        Set(ByVal value As String)
            sRoute = value
        End Set
    End Property
    'Added by kanchan on 20100917
    Public Property RouteCode() As String
        Get
            Return sRouteCode
        End Get
        Set(ByVal value As String)
            sRouteCode = value
        End Set
    End Property
    Public Property ReasonConceptID() As String
        Get
            Return sReasonConceptID
        End Get
        Set(ByVal value As String)
            sReasonConceptID = value
        End Set
    End Property
    Public Property ImmunizationDate() As String
        Get
            Return dtImmunizationdate
        End Get
        Set(ByVal value As String)
            dtImmunizationdate = value
        End Set
    End Property
    'Added by kanchan on 20100626 
    Public Property VaccineCode() As String
        Get
            Return mVaccineCode
        End Get
        Set(ByVal value As String)
            mVaccineCode = value
        End Set
    End Property
    Public Property VaccineName() As String
        Get
            Return mVaccineName
        End Get
        Set(ByVal value As String)
            mVaccineName = value
        End Set
    End Property
    Public Property LotNumber() As String
        Get
            Return mLotNumber
        End Get
        Set(ByVal value As String)
            mLotNumber = value
        End Set
    End Property
    Public Property Pregnant() As String
        Get
            Return mPregnant
        End Get
        Set(ByVal value As String)
            mPregnant = value
        End Set
    End Property
    Public Property Reason() As String
        Get
            Return mreason
        End Get
        Set(ByVal value As String)
            mreason = value
        End Set
    End Property
    Public Property AmountGiven() As Decimal
        Get
            Return dAmount
        End Get
        Set(ByVal value As Decimal)
            dAmount = value
        End Set
    End Property
    Public Property Units() As String
        Get
            Return sunits
        End Get
        Set(ByVal value As String)
            sunits = value
        End Set
    End Property
    Public Property ValuesetOID As String
        Get
            Return sValuesetOID
        End Get
        Set(value As String)
            sValuesetOID = value
        End Set
    End Property
    Public Property Valueset As String
        Get
            Return sValuesetdescription
        End Get
        Set(value As String)
            sValuesetdescription = value
        End Set
    End Property
    Public Sub New()
       
    End Sub
End Class
Public Class Vitals
    Implements IDisposable

    Private dtVitalDate As String
    Private dBloodPressureSittingMax As String
    Private dBloodPressureSittingMin As String
    Private dPulsePerMinute As String
    Private dRespiratoryRate As String
    Private dTemperature As String
    Private dHeightinInch As String
    ' Private dWeightinKg As String
    Private dWeightinlbs As String
    Private dPulseOX As String
    Private dBMI As String
    Private BodySurfaceArea As String
    Private dTHRPerMin As Long
    Private dTHRPerMax As Long
    Private dTHRMin As Long
    Private dTHRMax As Long
    Private mResultID As String
    Private mResultDate As String
    Private mCodeSystem As String
    Private mResultCode As String
    Private mResultCodeDisplay As String
    Private mStatusCode As String
    Private mResultValue As String
    Private mResultUnit As String
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mComment As String  'Added by Rohit on 20101008
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

    ''''''property for comment field of Vital
    'Added by Rohit on 20101008
    Public Property Comment() As String
        Get
            Return mComment
        End Get
        Set(ByVal value As String)
            mComment = value
        End Set
    End Property

    Public Property VitalDate() As String
        Get
            Return dtVitalDate
        End Get
        Set(ByVal value As String)
            dtVitalDate = value
        End Set
    End Property

    Public Property BloodPressureSittingMax() As String
        Get
            Return dBloodPressureSittingMax
        End Get
        Set(ByVal value As String)
            dBloodPressureSittingMax = value
        End Set
    End Property

    Public Property BloodPressureSittingMin() As String
        Get
            Return dBloodPressureSittingMin
        End Get
        Set(ByVal value As String)
            dBloodPressureSittingMin = value
        End Set
    End Property

    Public Property PulsePerMinute() As String
        Get
            Return dPulsePerMinute
        End Get
        Set(ByVal value As String)
            dPulsePerMinute = value
        End Set
    End Property
    Public Property RespiratoryRate() As String
        Get
            Return dRespiratoryRate
        End Get
        Set(ByVal value As String)
            dRespiratoryRate = value
        End Set
    End Property
    Public Property Temperature() As String
        Get
            Return dTemperature
        End Get
        Set(ByVal value As String)
            dTemperature = value
        End Set
    End Property
    Public Property HeightinInch() As String
        Get
            Return dHeightinInch
        End Get
        Set(ByVal value As String)
            dHeightinInch = value
        End Set
    End Property
    'Public Property WeightinKg() As String
    '    Get
    '        Return dWeightinKg
    '    End Get
    '    Set(ByVal value As String)
    '        dWeightinKg = value
    '    End Set
    'End Property
    Public Property Weightinlbs() As String
        Get
            Return dWeightinlbs
        End Get
        Set(ByVal value As String)
            dWeightinlbs = value
        End Set
    End Property
    Public Property PulseOx() As String
        Get
            Return dPulseOX
        End Get
        Set(ByVal value As String)
            dPulseOX = value
        End Set
    End Property
    Public Property BMI() As String
        Get
            Return dBMI
        End Get
        Set(ByVal value As String)
            dBMI = value
        End Set
    End Property

    Public Property BSA() As String
        Get
            Return BodySurfaceArea
        End Get
        Set(ByVal value As String)
            BodySurfaceArea = value
        End Set
    End Property
    Public Property ResultID() As String
        Get
            Return mResultID
        End Get
        Set(ByVal value As String)
            mResultID = value
        End Set
    End Property
    Public Property ResultUnit() As String
        Get
            Return mResultUnit
        End Get
        Set(ByVal value As String)
            mResultUnit = value
        End Set
    End Property
    Public Property ResultDate() As String
        Get
            Return mResultDate
        End Get
        Set(ByVal value As String)
            mResultDate = value
        End Set
    End Property
    Public Property CodeSystem() As String
        Get
            Return mCodeSystem
        End Get
        Set(ByVal value As String)
            mCodeSystem = value
        End Set
    End Property
    Public Property ResultCode() As String
        Get
            Return mResultCode
        End Get
        Set(ByVal value As String)
            mResultCode = value
        End Set
    End Property
    Public Property ResultCodeDisplay() As String
        Get
            Return mResultCodeDisplay
        End Get
        Set(ByVal value As String)
            mResultCodeDisplay = value
        End Set
    End Property
    Public Property StatusCode() As String
        Get
            Return mStatusCode
        End Get
        Set(ByVal value As String)
            mStatusCode = value
        End Set
    End Property
    Public Property ResultValue() As String
        Get
            Return mResultValue
        End Get
        Set(ByVal value As String)
            mResultValue = value
        End Set
    End Property
    Public Property THRperMin As Long
        Get
            Return dTHRPerMin
        End Get
        Set(ByVal value As Long)
            dTHRPerMin = value
        End Set
    End Property
    Public Property THRperMax As Long
        Get
            Return dTHRPerMax
        End Get
        Set(ByVal value As Long)
            dTHRPerMax = value
        End Set
    End Property
    Public Property THRMin As Long
        Get
            Return dTHRMin
        End Get
        Set(ByVal value As Long)
            dTHRMin = value
        End Set
    End Property
    Public Property THRMax As Long
        Get
            Return dTHRMax
        End Get
        Set(ByVal value As Long)
            dTHRMax = value
        End Set
    End Property
End Class
Public Class Results
    Implements IDisposable
    Private mResultID As String
    Private mResultDate As String
    Private mCodeSystem As String
    Private mResultCode As String
    Private mResultCodeDisplay As String
    Private mStatusCode As String
    Private mResultValue As String
    Private mResultUnit As String
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
    Public Property ResultID() As String
        Get
            Return mResultID
        End Get
        Set(ByVal value As String)
            mResultID = value
        End Set
    End Property
    Public Property ResultUnit() As String
        Get
            Return mResultUnit
        End Get
        Set(ByVal value As String)
            mResultUnit = value
        End Set
    End Property
    Public Property ResultDate() As String
        Get
            Return mResultDate
        End Get
        Set(ByVal value As String)
            mResultDate = value
        End Set
    End Property
    Public Property CodeSystem() As String
        Get
            Return mCodeSystem
        End Get
        Set(ByVal value As String)
            mCodeSystem = value
        End Set
    End Property
    Public Property ResultCode() As String
        Get
            Return mResultCode
        End Get
        Set(ByVal value As String)
            mResultCode = value
        End Set
    End Property
    Public Property ResultCodeDisplay() As String
        Get
            Return mResultCodeDisplay
        End Get
        Set(ByVal value As String)
            mResultCodeDisplay = value
        End Set
    End Property
    Public Property StatusCode() As String
        Get
            Return mStatusCode
        End Get
        Set(ByVal value As String)
            mStatusCode = value
        End Set
    End Property
    Public Property ResultValue() As String
        Get
            Return mResultValue
        End Get
        Set(ByVal value As String)
            mResultValue = value
        End Set
    End Property
End Class
Public Class Comments
    Implements IDisposable
    Private sSuffix As String
    Private sFirstName As String
    Private sLastName As String
    Private sComment As String
    Private sPrefix As String

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
    Public Property Suffix() As String
        Get
            Return sSuffix
        End Get
        Set(ByVal value As String)
            sSuffix = value
        End Set
    End Property
    Public Property FirstName() As String
        Get
            Return sFirstName
        End Get
        Set(ByVal value As String)
            sFirstName = value
        End Set
    End Property
    Public Property LastName() As String
        Get
            Return sLastName
        End Get
        Set(ByVal value As String)
            sLastName = value
        End Set
    End Property
    Public Property Comment() As String
        Get
            Return sComment
        End Get
        Set(ByVal value As String)
            sComment = value
        End Set
    End Property
    Public Property Prefix() As String
        Get
            Return sPrefix
        End Get
        Set(ByVal value As String)
            sPrefix = value
        End Set
    End Property
End Class
Public Class Encounters
    Implements IDisposable

    Private sExamName As String
    Private sExamid As String
    Private sProvFirstName As String
    Private sProvMName As String
    Private sProvLastName As String
    Private sProvSuffix As String
    Private sProvNPI As String
    Private sLocation As String
    Private sDateOfService As String
    Private sDateOfExamCreated As String
    Private sDischargeDate As String
    Private sLocationAdd As ContactAddress
    Private mEncounterDate As String
    Private mEncounterId As String
    Private mFreeText As String

    Private mEncounterName As String
    Private mEncounterCode As String

    Private mSuffix As String
    Private mPrefix As String
    Private mFirstName As String
    Private mLastName As String
    Private mStreetAddress As String
    Private mCity As String
    Private mState As String
    Private mPostalCode As String
    Private mCountry As String

    Private mHomePhone As String
    Private mWorkPhone As String
    Private mVacationPhone As String
    Private mMobilePhone As String

    Private mEmail As String
    Private mURL As String
    Private sStartDateTime As String
    Private sEndDateTime As String
    Private examsnomed As String
    Private examsnomeddesc As String
    Private shspcscode As String

    
    Public Property HcpcsCode As String
        Get
            Return shspcscode
        End Get
        Set(ByVal value As String)
            shspcscode = value
        End Set
    End Property
    Public Property SnomedCode As String
        Get
            Return examsnomed
        End Get
        Set(ByVal value As String)
            examsnomed = value
        End Set
    End Property
    Public Property SnomedCodeDeSc As String
        Get
            Return examsnomeddesc
        End Get
        Set(ByVal value As String)
            examsnomeddesc = value
        End Set
    End Property
    Public Property StartDateTime() As String
        Get
            Return sStartDateTime
        End Get
        Set(ByVal value As String)
            sStartDateTime = value
        End Set
    End Property
    Public Property EndDateTime() As String
        Get
            Return sEndDateTime
        End Get
        Set(ByVal value As String)
            sEndDateTime = value
        End Set
    End Property
    Public Property LocationAdd As ContactAddress
        Get
            Return sLocationAdd
        End Get
        Set(ByVal value As ContactAddress)
            sLocationAdd = value
        End Set
    End Property
    Public Property ProvNPI() As String
        Get
            Return sProvNPI
        End Get
        Set(ByVal value As String)
            sProvNPI = value
        End Set
    End Property
    Public Property ExamName() As String
        Get
            Return sExamName
        End Get
        Set(ByVal value As String)
            sExamName = value
        End Set
    End Property
    Public Property ExamID() As String
        Get
            Return sExamid
        End Get
        Set(ByVal value As String)
            sExamid = value
        End Set
    End Property

    Public Property ProvFirstName() As String
        Get
            Return sProvFirstName
        End Get
        Set(ByVal value As String)
            sProvFirstName = value
        End Set
    End Property

    Public Property ProvMName() As String
        Get
            Return sProvMName
        End Get
        Set(ByVal value As String)
            sProvMName = value
        End Set
    End Property

    Public Property ProvLastName() As String
        Get
            Return sProvLastName
        End Get
        Set(ByVal value As String)
            sProvLastName = value
        End Set
    End Property

    Public Property ProvSuffix() As String
        Get
            Return sProvSuffix
        End Get
        Set(ByVal value As String)
            sProvSuffix = value
        End Set
    End Property

    Public Property Location() As String
        Get
            Return sLocation
        End Get
        Set(ByVal value As String)
            sLocation = value
        End Set
    End Property

    Public Property DateOfService() As String
        Get
            Return sDateOfService
        End Get
        Set(ByVal value As String)
            sDateOfService = value
        End Set
    End Property
    Public Property DateOfExamCreated() As String
        Get
            Return sDateOfExamCreated
        End Get
        Set(ByVal value As String)
            sDateOfExamCreated = value
        End Set
    End Property
    Public Property DischargeDate() As String
        Get
            Return sDischargeDate
        End Get
        Set(ByVal value As String)
            sDischargeDate = value
        End Set
    End Property
  
    Public Property Email() As String
        Get
            Return mEmail
        End Get
        Set(ByVal value As String)
            mEmail = value
        End Set
    End Property

    Public Property URL() As String
        Get
            Return mURL
        End Get
        Set(ByVal value As String)
            mURL = value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return mCountry
        End Get
        Set(ByVal value As String)
            mCountry = value
        End Set
    End Property

    Public Property PostalCode() As String
        Get
            Return mPostalCode
        End Get
        Set(ByVal value As String)
            mPostalCode = value
        End Set
    End Property

    Public Property EncounterDate() As String
        Get
            Return mEncounterDate
        End Get
        Set(ByVal value As String)
            mEncounterDate = value
        End Set
    End Property

    Public Property EncounterId() As String
        Get
            Return mEncounterId
        End Get
        Set(ByVal value As String)
            mEncounterId = value
        End Set
    End Property

    Public Property FreeText() As String
        Get
            Return mFreeText
        End Get
        Set(ByVal value As String)
            mFreeText = value
        End Set
    End Property
    Public Property EncounterName() As String
        Get
            Return mEncounterName
        End Get
        Set(ByVal value As String)
            mEncounterName = value
        End Set
    End Property
    Public Property EncounterCode() As String
        Get
            Return mEncounterCode
        End Get
        Set(ByVal value As String)
            mEncounterCode = value
        End Set
    End Property
    Public Property Suffix() As String
        Get
            Return mSuffix
        End Get
        Set(ByVal value As String)
            mSuffix = value
        End Set
    End Property
    Public Property Prefix() As String
        Get
            Return mPrefix
        End Get
        Set(ByVal value As String)
            mPrefix = value
        End Set
    End Property
    Public Property FirstName() As String
        Get
            Return mFirstName
        End Get
        Set(ByVal value As String)
            mFirstName = value
        End Set
    End Property
    Public Property LastName() As String
        Get
            Return mLastName
        End Get
        Set(ByVal value As String)
            mLastName = value
        End Set
    End Property
    Public Property StreetAddress() As String
        Get
            Return mStreetAddress
        End Get
        Set(ByVal value As String)
            mStreetAddress = value
        End Set
    End Property
    Public Property City() As String
        Get
            Return mCity
        End Get
        Set(ByVal value As String)
            mCity = value
        End Set
    End Property
    Public Property State() As String
        Get
            Return mState
        End Get
        Set(ByVal value As String)
            mState = value
        End Set
    End Property
    Public Property HomePhone() As String
        Get
            Return mHomePhone
        End Get
        Set(ByVal value As String)
            mHomePhone = value
        End Set
    End Property
    Public Property WorkPhone() As String
        Get
            Return mWorkPhone
        End Get
        Set(ByVal value As String)
            mWorkPhone = value
        End Set
    End Property
    Public Property VacationPhone() As String
        Get
            Return mVacationPhone
        End Get
        Set(ByVal value As String)
            mVacationPhone = value
        End Set
    End Property
    Public Property MobilePhone() As String
        Get
            Return mMobilePhone
        End Get
        Set(ByVal value As String)
            mMobilePhone = value
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

    Public Sub New()

    End Sub
End Class

Public Class Problems

    Implements IDisposable
    Enum Status
        Resolved = 1
        'Pending = 2
        Active = 2
        Inactive = 3
        Chronic = 4
        All = 5
    End Enum
    Private sCondition As String
    Private sDateOfService As String
    Private sConcernStartDate As String
    Private sConcernENDDate As String
    Private sResolvedDate As String
    Private sDischargeDate As String
    Private sModifiedDate As String
    Private sConditionStatus As String
    Private nICDRevision As Int16
    Private mEncounterDate As String
    Private mEncounterId As String
    Private mFreeText As String
    Private mEncounterName As String
    Private mEncounterCode As String

    Private mSuffix As String
    Private mPrefix As String
    Private mFirstName As String
    Private mLastName As String
    Private mStreetAddress As String
    Private mCity As String
    Private mState As String
    Private mPostalCode As String
    Private mCountry As String

    Private mHomePhone As String
    Private mWorkPhone As String
    Private mVacationPhone As String
    Private mMobilePhone As String

    Private mEmail As String
    Private mURL As String
    'Code Start-Added by kanchan on 20100916
    Private sICD9Code As String
    Private sICD10Code As String
    Private sICD9 As String
    Private sConceptID As String
    Private _sReasonConceptID As String
    Private _sReasonDesc As String
    Private sProblemType As String
    Private sProblemStatus As Int32
    Private nImmediacy As Int32 = 3
    Private sProblmReaction As String
    Private nPatientID As Int64
    Private nUserID As Int64
    Private _ProviderName As String
    Private _User As String
    Private nProblemID As Int64
    Private _sproblemtypeCode As String
    Private _sEncounterTypeCode As String
    Private _GUID As String
    Private _sConcernStatus As String
    Public Property sConcernStatus As String
        Get
            Return _sConcernStatus
        End Get
        Set(value As String)
            _sConcernStatus = value
        End Set
    End Property
    Public Property GUID As String
        Get
            Return _GUID
        End Get
        Set(value As String)
            _GUID = value
        End Set
    End Property
    Public Property ProblemTypeCode As String
        Get
            Return _sproblemtypeCode
        End Get
        Set(value As String)
            _sproblemtypeCode = value
        End Set
    End Property
    Public Property EncounterTypeCode As String
        Get
            Return _sEncounterTypeCode
        End Get
        Set(value As String)
            _sEncounterTypeCode = value
        End Set
    End Property

    Public Property Immediacy() As Int32
        Get
            Return nImmediacy
        End Get
        Set(ByVal value As Int32)
            nImmediacy = value
        End Set
    End Property


    Public Property ProblemType() As String
        Get
            Return sProblemType
        End Get
        Set(ByVal value As String)
            sProblemType = value
        End Set
    End Property
    Public Property ProblmReaction() As String
        Get
            Return sProblmReaction
        End Get
        Set(ByVal value As String)
            sProblmReaction = value
        End Set
    End Property
    Public Property ICD9Code() As String
        Get
            Return sICD9Code
        End Get
        Set(ByVal value As String)
            sICD9Code = value
        End Set
    End Property
    Public Property ICD10Code() As String
        Get
            Return sICD10Code
        End Get
        Set(ByVal value As String)
            sICD10Code = value
        End Set
    End Property
    Public Property ICD9() As String
        Get
            Return sICD9
        End Get
        Set(ByVal value As String)
            sICD9 = value
        End Set
    End Property
    Public Property ConceptID() As String
        Get
            Return sConceptID
        End Get
        Set(ByVal value As String)
            sConceptID = value
        End Set
    End Property
    Public Property ReasonConceptID() As String
        Get
            Return _sReasonConceptID
        End Get
        Set(ByVal value As String)
            _sReasonConceptID = value
        End Set
    End Property
    Public Property ReasonDesc() As String
        Get
            Return _sReasonDesc
        End Get
        Set(ByVal value As String)
            _sReasonDesc = value
        End Set
    End Property
    Public Property ProviderName() As String
        Get
            Return _ProviderName
        End Get
        Set(ByVal value As String)
            _ProviderName = value
        End Set
    End Property
    Public Property User() As String
        Get
            Return _User
        End Get
        Set(ByVal value As String)
            _User = value
        End Set
    End Property
    Public Property PatientID() As Int64
        Get
            Return nPatientID
        End Get
        Set(ByVal value As Int64)
            nPatientID = value
        End Set
    End Property
    Public Property ProblemID() As Int64
        Get
            Return nProblemID
        End Get
        Set(ByVal value As Int64)
            nProblemID = value
        End Set
    End Property
    Public Property UserID() As Int64
        Get
            Return nUserID
        End Get
        Set(ByVal value As Int64)
            nUserID = value
        End Set
    End Property
    Public Property ProblemStatus() As Int32
        Get
            Return sProblemStatus
        End Get
        Set(ByVal value As Int32)
            sProblemStatus = value
        End Set
    End Property
    'Code End-Added by kanchan on 20100916
    Public Property Condition() As String
        Get
            Return sCondition
        End Get
        Set(ByVal value As String)
            sCondition = value
        End Set
    End Property


    Public Property DateOfService() As String
        Get
            Return sDateOfService
        End Get
        Set(ByVal value As String)
            sDateOfService = value
        End Set
    End Property
    Public Property ConcernStartDate() As String
        Get
            Return sConcernStartDate
        End Get
        Set(ByVal value As String)
            sConcernStartDate = value
        End Set
    End Property
    Public Property ConcernEndDate() As String
        Get
            Return sConcernEndDate
        End Get
        Set(ByVal value As String)
            sConcernEndDate = value
        End Set
    End Property
    Public Property ResolvedDate() As String
        Get
            Return sResolvedDate
        End Get
        Set(ByVal value As String)
            sResolvedDate = value
        End Set
    End Property
    Public Property DischargeDate() As String
        Get
            Return sDischargeDate
        End Get
        Set(ByVal value As String)
            sDischargeDate = value
        End Set
    End Property
    Public Property ModifiedDate() As String
        Get
            Return sModifiedDate
        End Get
        Set(ByVal value As String)
            sModifiedDate = value
        End Set
    End Property

    Public Property ConditionStatus() As String
        Get
            Return sConditionStatus
        End Get
        Set(ByVal value As String)
            sConditionStatus = value
        End Set
    End Property
    Public Property ICDRevision() As Int16
        Get
            Return nICDRevision
        End Get
        Set(ByVal value As Int16)
            nICDRevision = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return mEmail
        End Get
        Set(ByVal value As String)
            mEmail = value
        End Set
    End Property

    Public Property URL() As String
        Get
            Return mURL
        End Get
        Set(ByVal value As String)
            mURL = value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return mCountry
        End Get
        Set(ByVal value As String)
            mCountry = value
        End Set
    End Property

    Public Property PostalCode() As String
        Get
            Return mPostalCode
        End Get
        Set(ByVal value As String)
            mPostalCode = value
        End Set
    End Property

    Public Property EncounterDate() As String
        Get
            Return mEncounterDate
        End Get
        Set(ByVal value As String)
            mEncounterDate = value
        End Set
    End Property

    Public Property EncounterId() As String
        Get
            Return mEncounterId
        End Get
        Set(ByVal value As String)
            mEncounterId = value
        End Set
    End Property

    Public Property FreeText() As String
        Get
            Return mFreeText
        End Get
        Set(ByVal value As String)
            mFreeText = value
        End Set
    End Property
    Public Property EncounterName() As String
        Get
            Return mEncounterName
        End Get
        Set(ByVal value As String)
            mEncounterName = value
        End Set
    End Property
    Public Property EncounterCode() As String
        Get
            Return mEncounterCode
        End Get
        Set(ByVal value As String)
            mEncounterCode = value
        End Set
    End Property
    Public Property Suffix() As String
        Get
            Return mSuffix
        End Get
        Set(ByVal value As String)
            mSuffix = value
        End Set
    End Property
    Public Property Prefix() As String
        Get
            Return mPrefix
        End Get
        Set(ByVal value As String)
            mPrefix = value
        End Set
    End Property
    Public Property FirstName() As String
        Get
            Return mFirstName
        End Get
        Set(ByVal value As String)
            mFirstName = value
        End Set
    End Property
    Public Property LastName() As String
        Get
            Return mLastName
        End Get
        Set(ByVal value As String)
            mLastName = value
        End Set
    End Property
    Public Property StreetAddress() As String
        Get
            Return mStreetAddress
        End Get
        Set(ByVal value As String)
            mStreetAddress = value
        End Set
    End Property
    Public Property City() As String
        Get
            Return mCity
        End Get
        Set(ByVal value As String)
            mCity = value
        End Set
    End Property
    Public Property State() As String
        Get
            Return mState
        End Get
        Set(ByVal value As String)
            mState = value
        End Set
    End Property
    Public Property HomePhone() As String
        Get
            Return mHomePhone
        End Get
        Set(ByVal value As String)
            mHomePhone = value
        End Set
    End Property
    Public Property WorkPhone() As String
        Get
            Return mWorkPhone
        End Get
        Set(ByVal value As String)
            mWorkPhone = value
        End Set
    End Property
    Public Property VacationPhone() As String
        Get
            Return mVacationPhone
        End Get
        Set(ByVal value As String)
            mVacationPhone = value
        End Set
    End Property
    Public Property MobilePhone() As String
        Get
            Return mMobilePhone
        End Get
        Set(ByVal value As String)
            mMobilePhone = value
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

    Public Sub New()

    End Sub
End Class

Public Class Procedures
    Implements IDisposable

    Private sExamName As String
    Private sCPTCode As String
    Private sCPTDescription As String
    Private sProviderFirstName As String
    Private sProviderMiddleName As String
    Private sProviderLastName As String
    Private sProviderSuffix As String
    Private sDateOfService As String
    Private _ProcedureStatus As String
    Private sICD9Description As String
    Private mEncounterDate As String
    Private mEncounterId As String
    Private mFreeText As String
    Private mEncounterName As String
    Private mEncounterCode As String

    Private mSuffix As String
    Private mPrefix As String
    Private mFirstName As String
    Private mLastName As String
    Private mStreetAddress As String
    Private mCity As String
    Private mState As String
    Private mPostalCode As String
    Private mCountry As String

    Private mHomePhone As String
    Private mWorkPhone As String
    Private mVacationPhone As String
    Private mMobilePhone As String

    Private mEmail As String
    Private mURL As String
    Private ICD9code As String
    Private _SnomedCode As String
    Private _ICDRevision As Int16
    Private _nDeviceListId As Int64
    Private _SSnomedDescription As String
    Private _sstatus As String
    Public Property Status As String
        Get
            Return _sstatus
        End Get
        Set(value As String)
            _sstatus = value
        End Set
    End Property
    Public Property SnomedDescription As String
        Get
            Return _SSnomedDescription
        End Get
        Set(value As String)
            _SSnomedDescription = value
        End Set
    End Property
    Public Property ImplantDeviceId As Int64
        Get
            Return _nDeviceListId
        End Get
        Set(value As Int64)
            _nDeviceListId = value
        End Set
    End Property
    Public Property ICD9Description() As String
        Get
            Return sICD9Description
        End Get
        Set(ByVal value As String)
            sICD9Description = value
        End Set
    End Property
    Public Property ICD9_code() As String
        Get
            Return ICD9code
        End Get
        Set(ByVal value As String)
            ICD9code = value
        End Set
    End Property
    Public Property SnomedCode() As String
        Get
            Return _SnomedCode
        End Get
        Set(ByVal value As String)
            _SnomedCode = value
        End Set
    End Property
    Public Property ICDRevision() As Int16
        Get
            Return _ICDRevision
        End Get
        Set(ByVal value As Int16)
            _ICDRevision = value
        End Set
    End Property
    Public Property ExamName() As String
        Get
            Return sExamName
        End Get
        Set(ByVal value As String)
            sExamName = value
        End Set
    End Property

    Public Property CPTCode() As String
        Get
            Return sCPTCode
        End Get
        Set(ByVal value As String)
            sCPTCode = value
        End Set
    End Property

    Public Property CPTDescription() As String
        Get
            Return sCPTDescription
        End Get
        Set(ByVal value As String)
            sCPTDescription = value
        End Set
    End Property

    Public Property ProviderFirstName() As String
        Get
            Return sProviderFirstName
        End Get
        Set(ByVal value As String)
            sProviderFirstName = value
        End Set
    End Property

    Public Property ProviderMiddleName() As String
        Get
            Return sProviderMiddleName
        End Get
        Set(ByVal value As String)
            sProviderMiddleName = value
        End Set
    End Property

    Public Property ProviderLastName() As String
        Get
            Return sProviderLastName
        End Get
        Set(ByVal value As String)
            sProviderLastName = value
        End Set
    End Property

    Public Property ProviderSuffix() As String
        Get
            Return sProviderSuffix
        End Get
        Set(ByVal value As String)
            sProviderSuffix = value
        End Set
    End Property

    Public Property DateOfService() As String
        Get
            Return sDateOfService
        End Get
        Set(ByVal value As String)
            sDateOfService = value
        End Set
    End Property
    Public Property ProcedureStatus() As String
        Get
            Return _ProcedureStatus
        End Get
        Set(ByVal value As String)
            _ProcedureStatus = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return mEmail
        End Get
        Set(ByVal value As String)
            mEmail = value
        End Set
    End Property

    Public Property URL() As String
        Get
            Return mURL
        End Get
        Set(ByVal value As String)
            mURL = value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return mCountry
        End Get
        Set(ByVal value As String)
            mCountry = value
        End Set
    End Property

    Public Property PostalCode() As String
        Get
            Return mPostalCode
        End Get
        Set(ByVal value As String)
            mPostalCode = value
        End Set
    End Property

    Public Property EncounterDate() As String
        Get
            Return mEncounterDate
        End Get
        Set(ByVal value As String)
            mEncounterDate = value
        End Set
    End Property

    Public Property EncounterId() As String
        Get
            Return mEncounterId
        End Get
        Set(ByVal value As String)
            mEncounterId = value
        End Set
    End Property

    Public Property FreeText() As String
        Get
            Return mFreeText
        End Get
        Set(ByVal value As String)
            mFreeText = value
        End Set
    End Property
    Public Property EncounterName() As String
        Get
            Return mEncounterName
        End Get
        Set(ByVal value As String)
            mEncounterName = value
        End Set
    End Property
    Public Property EncounterCode() As String
        Get
            Return mEncounterCode
        End Get
        Set(ByVal value As String)
            mEncounterCode = value
        End Set
    End Property
    Public Property Suffix() As String
        Get
            Return mSuffix
        End Get
        Set(ByVal value As String)
            mSuffix = value
        End Set
    End Property
    Public Property Prefix() As String
        Get
            Return mPrefix
        End Get
        Set(ByVal value As String)
            mPrefix = value
        End Set
    End Property
    Public Property FirstName() As String
        Get
            Return mFirstName
        End Get
        Set(ByVal value As String)
            mFirstName = value
        End Set
    End Property
    Public Property LastName() As String
        Get
            Return mLastName
        End Get
        Set(ByVal value As String)
            mLastName = value
        End Set
    End Property
    Public Property StreetAddress() As String
        Get
            Return mStreetAddress
        End Get
        Set(ByVal value As String)
            mStreetAddress = value
        End Set
    End Property
    Public Property City() As String
        Get
            Return mCity
        End Get
        Set(ByVal value As String)
            mCity = value
        End Set
    End Property
    Public Property State() As String
        Get
            Return mState
        End Get
        Set(ByVal value As String)
            mState = value
        End Set
    End Property
    Public Property HomePhone() As String
        Get
            Return mHomePhone
        End Get
        Set(ByVal value As String)
            mHomePhone = value
        End Set
    End Property
    Public Property WorkPhone() As String
        Get
            Return mWorkPhone
        End Get
        Set(ByVal value As String)
            mWorkPhone = value
        End Set
    End Property
    Public Property VacationPhone() As String
        Get
            Return mVacationPhone
        End Get
        Set(ByVal value As String)
            mVacationPhone = value
        End Set
    End Property
    Public Property MobilePhone() As String
        Get
            Return mMobilePhone
        End Get
        Set(ByVal value As String)
            mMobilePhone = value
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

    Public Sub New()

    End Sub
End Class

Public Class Language
    Private mLanguage As String
    Private mMode As String
    Private mPreferred As String
    Private mCountry As String

    Public Property Language() As String
        Get
            Return mLanguage
        End Get
        Set(ByVal value As String)
            mLanguage = value
        End Set
    End Property
    Public Property Mode() As String
        Get
            Return mMode
        End Get
        Set(ByVal value As String)
            mMode = value
        End Set
    End Property
    Public Property Preferred() As String
        Get
            Return mPreferred
        End Get
        Set(ByVal value As String)
            mPreferred = value
        End Set
    End Property
    Public Property Country() As String
        Get
            Return mCountry
        End Get
        Set(ByVal value As String)
            mCountry = value
        End Set
    End Property
End Class
Public Class LanguageCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Language
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Language)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Language)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class PatientProvider
    Implements IDisposable
    Private mStartServiceDate As String
    Private mEndServiceDate As String
    Private mPrefix As String
    Private mFirstName As String
    Private mMiddleName As String
    Private mLastName As String
    Private mSuffix As String
    Private mProviderRole As String
    Private mRoleDescription As String
    Private mProviderType As String
    Private mStreetAddress As String
    Private mCity As String
    Private mState As String
    Private mzip As String
    Private mCountry As String
    Private mHomePhone As String
    Private mWorkPhone As String
    Private mMobilePhone As String
    Private mVacationPhone As String
    Private mEmail As String
    Private mURL As String
    Private mOrganization As String
    Private mPatientIdentifier As String
    Private mNPI As String
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private _ProvTaxID As String
    Public Property NPI() As String
        Get
            Return mNPI
        End Get
        Set(ByVal value As String)
            mNPI = value
        End Set
    End Property
    Public Property ProvTaxID() As String
        Get
            Return _ProvTaxID
        End Get
        Set(ByVal value As String)
            _ProvTaxID = value
        End Set
    End Property
    Public Property PatientIdentifier() As String
        Get
            Return mPatientIdentifier
        End Get
        Set(ByVal value As String)
            mPatientIdentifier = value
        End Set
    End Property
    Public Property Organization() As String
        Get
            Return mOrganization
        End Get
        Set(ByVal value As String)
            mOrganization = value
        End Set
    End Property
    Public Property URL() As String
        Get
            Return mURL
        End Get
        Set(ByVal value As String)
            mURL = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return mEmail
        End Get
        Set(ByVal value As String)
            mEmail = value
        End Set
    End Property
    Public Property City() As String
        Get
            Return mCity
        End Get
        Set(ByVal value As String)
            mCity = value
        End Set
    End Property
    Public Property State() As String
        Get
            Return mState
        End Get
        Set(ByVal value As String)
            mState = value
        End Set
    End Property
    Public Property zip() As String
        Get
            Return mzip
        End Get
        Set(ByVal value As String)
            mzip = value
        End Set
    End Property
    Public Property Country() As String
        Get
            Return mCountry
        End Get
        Set(ByVal value As String)
            mCountry = value
        End Set
    End Property
    Public Property HomePhone() As String
        Get
            Return mHomePhone
        End Get
        Set(ByVal value As String)
            mHomePhone = value
        End Set
    End Property
    Public Property WorkPhone() As String
        Get
            Return mWorkPhone
        End Get
        Set(ByVal value As String)
            mWorkPhone = value
        End Set
    End Property
    Public Property MobilePhone() As String
        Get
            Return mMobilePhone
        End Get
        Set(ByVal value As String)
            mMobilePhone = value
        End Set
    End Property
    Public Property VacationPhone() As String
        Get
            Return mVacationPhone
        End Get
        Set(ByVal value As String)
            mVacationPhone = value
        End Set
    End Property
    Public Property StreetAddress() As String
        Get
            Return mStreetAddress
        End Get
        Set(ByVal value As String)
            mStreetAddress = value
        End Set
    End Property
    Public Property StartServiceDate() As String
        Get
            Return mStartServiceDate
        End Get
        Set(ByVal value As String)
            mStartServiceDate = value
        End Set
    End Property
    Public Property EndServiceDate() As String
        Get
            Return mEndServiceDate
        End Get
        Set(ByVal value As String)
            mEndServiceDate = value
        End Set
    End Property
    Public Property Prefix() As String
        Get
            Return mPrefix
        End Get
        Set(ByVal value As String)
            mPrefix = value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return mFirstName
        End Get
        Set(ByVal value As String)
            mFirstName = value
        End Set
    End Property
    Public Property MiddleName() As String
        Get
            Return mMiddleName
        End Get
        Set(ByVal value As String)
            mMiddleName = value
        End Set
    End Property
    Public Property LastName() As String
        Get
            Return mLastName
        End Get
        Set(ByVal value As String)
            mLastName = value
        End Set
    End Property
    Public Property Suffix() As String
        Get
            Return mSuffix
        End Get
        Set(ByVal value As String)
            mSuffix = value
        End Set
    End Property
    Public Property ProviderRole() As String
        Get
            Return mProviderRole
        End Get
        Set(ByVal value As String)
            mProviderRole = value
        End Set
    End Property
    Public Property RoleDescription() As String
        Get
            Return mRoleDescription
        End Get
        Set(ByVal value As String)
            mRoleDescription = value
        End Set
    End Property
    Public Property ProviderType() As String
        Get
            Return mProviderType
        End Get
        Set(ByVal value As String)
            mProviderType = value
        End Set
    End Property
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
Public Class ProviderCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As PatientProvider
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), PatientProvider)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As PatientProvider)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class InfoRecipent
    Implements IDisposable
    Private disposedValue As Boolean = False
    Private mPrefix As String
    Private mFirstName As String
    Private mMiddleName As String
    Private mLastName As String
    Private mSuffix As String


    Public Property Prefix() As String
        Get
            Return mPrefix
        End Get
        Set(ByVal value As String)
            mPrefix = value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return mFirstName
        End Get
        Set(ByVal value As String)
            mFirstName = value
        End Set
    End Property
    Public Property MiddleName() As String
        Get
            Return mMiddleName
        End Get
        Set(ByVal value As String)
            mMiddleName = value
        End Set
    End Property
    Public Property LastName() As String
        Get
            Return mLastName
        End Get
        Set(ByVal value As String)
            mLastName = value
        End Set
    End Property
    Public Property Suffix() As String
        Get
            Return mSuffix
        End Get
        Set(ByVal value As String)
            mSuffix = value
        End Set
    End Property

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
Public Class InfoRecipientCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As InfoRecipent
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), InfoRecipent)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As InfoRecipent)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class RaceCol
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
    ' This line declares the Item property as ReadOnly, and 
    '
    Public ReadOnly Property Item(ByVal index As Integer) As RaceDetails
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), RaceDetails)
        End Get
    End Property
    Dim _RaceCount As Int64 = 0
    Public Property RaceCount() As Int64
        Get
            Return _RaceCount
        End Get
        Set(ByVal Value As Int64)
            _RaceCount = Value
        End Set
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As RaceDetails)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class EthnicityCol
    Inherits CollectionBase

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Dim _EthCount As Int64 = 0
    Public Property EthCount() As Int64
        Get
            Return _EthCount
        End Get
        Set(ByVal Value As Int64)
            _EthCount = Value
        End Set
    End Property
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
    ' This line declares the Item property as ReadOnly, and 
    '
    Public ReadOnly Property Item(ByVal index As Integer) As RaceDetails
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), RaceDetails)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As RaceDetails)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class CDCCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As CDCRaceDetails
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), CDCRaceDetails)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As CDCRaceDetails)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class


Public Class PatientSupport
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
    Private mStartSupport As String
    Private mEndSupport As String
    Private mContacttype As String
    Private mRelationShip As String
    Private mPersonName As PersonName
    Private _PersonContactAddress As ContactAddress
    Private _PersonContactPhone As PhoneNumber
    Private mComments As String
    Public Property Comments() As String
        Get
            Return mComments
        End Get
        Set(ByVal Value As String)
            mComments = Value
        End Set
    End Property
    Public Property RelationShip() As String
        Get
            Return mRelationShip
        End Get
        Set(ByVal Value As String)
            mRelationShip = Value
        End Set
    End Property
    Public Property Contacttype() As String
        Get
            Return mContacttype
        End Get
        Set(ByVal Value As String)
            mContacttype = Value
        End Set
    End Property
    Public Property StartSupport() As String
        Get
            Return mStartSupport
        End Get
        Set(ByVal Value As String)
            mStartSupport = Value
        End Set
    End Property
    Public Property EndSupport() As String
        Get
            Return mEndSupport
        End Get
        Set(ByVal Value As String)
            mEndSupport = Value
        End Set
    End Property
    Public Property PersonContactPhone() As PhoneNumber
        Get
            Return _PersonContactPhone
        End Get
        Set(ByVal Value As PhoneNumber)
            _PersonContactPhone = Value
        End Set
    End Property
    Public Property PersonContactAddress() As ContactAddress
        Get
            Return _PersonContactAddress
        End Get
        Set(ByVal Value As ContactAddress)
            _PersonContactAddress = Value
        End Set
    End Property
    Public Property PersonName() As PersonName
        Get
            Return mPersonName
        End Get
        Set(ByVal value As PersonName)
            mPersonName = value
        End Set
    End Property

    Public Sub New()
        _PersonContactPhone = New PhoneNumber
        _PersonContactAddress = New ContactAddress
        mPersonName = New PersonName
    End Sub
End Class
Public Class PatientSupportCol
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
    ' This line declares the Item property as ReadOnly, and 
    Public ReadOnly Property Item(ByVal index As Integer) As PatientSupport
        Get
            ' The appropriate item is retrieved from the List object and 
            Return CType(List.Item(index), PatientSupport)
        End Get
    End Property
    'items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As PatientSupport)
        ' Invokes Add method of the List object to add a object
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class Clinic
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
    Private _ClinicId As Int32
    Private mClinicName As String
    Private _PersonContactAddress As ContactAddress
    Private _PersonContactPhone As PhoneNumber
    Private mClinicTaxID As String
    Private sTaxonomyCode As String
    Private sTaxonomyDesc As String
    Private _sOfficeContactFirstName As String
    Private _sOfficeContactlastName As String
    Public Property ClinicId As Int32
        Get
            Return _ClinicId
        End Get
        Set(value As Int32)
            _ClinicId = value
        End Set
    End Property
    Public Property OfficeContactFirstname As String
        Get
            Return _sOfficeContactFirstName
        End Get
        Set(value As String)
            _sOfficeContactFirstName = value
        End Set
    End Property
    Public Property OfficeContactLastname As String
        Get
            Return _sOfficeContactlastName
        End Get
        Set(value As String)
            _sOfficeContactlastName = value
        End Set
    End Property

    Public Property TaxonomyDesc
        Get
            Return sTaxonomyDesc
        End Get
        Set(value)
            sTaxonomyDesc = value
        End Set
    End Property
    Public Property TaxonomyCode
        Get
            Return sTaxonomyCode
        End Get
        Set(value)
            sTaxonomyCode = value
        End Set
    End Property
    Public Property ClinicName() As String
        Get
            Return mClinicName
        End Get
        Set(ByVal Value As String)
            mClinicName = Value
        End Set
    End Property
    Public Property ClinicTaxID() As String
        Get
            Return mClinicTaxID
        End Get
        Set(ByVal Value As String)
            mClinicTaxID = Value
        End Set
    End Property
    Public Property PersonContactPhone() As PhoneNumber
        Get
            Return _PersonContactPhone
        End Get
        Set(ByVal Value As PhoneNumber)
            _PersonContactPhone = Value
        End Set
    End Property
    Public Property PersonContactAddress() As ContactAddress
        Get
            Return _PersonContactAddress
        End Get
        Set(ByVal Value As ContactAddress)
            _PersonContactAddress = Value
        End Set
    End Property
    Public Sub New()
        _PersonContactPhone = New PhoneNumber
        _PersonContactAddress = New ContactAddress
    End Sub
End Class


Public Class PracticeContact
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
    Private mContactName As String
    'Private _ContactAddLine1 As String
    'Private _ContactAddLine2 As String
    'Private _ContactCity As String
    'Private _ContactState As String
    'Private _ContactZip As String
    'Private _ContactCountry As String
    'Private _ContactPhone As String
    Private _ContactFax As String
    Private _ContactEmail As String
    Private _PracticeID As String
    Dim _PersonContactPhone As PhoneNumber
    Dim _PersonContactAddress As ContactAddress

    Public Property ContactName() As String
        Get
            Return mContactName
        End Get
        Set(ByVal Value As String)
            mContactName = Value
        End Set
    End Property

    Public Property PersonContactPhone() As PhoneNumber
        Get
            Return _PersonContactPhone
        End Get
        Set(ByVal Value As PhoneNumber)
            _PersonContactPhone = Value
        End Set
    End Property
    Public Property PersonContactAddress() As ContactAddress
        Get
            Return _PersonContactAddress
        End Get
        Set(ByVal Value As ContactAddress)
            _PersonContactAddress = Value
        End Set
    End Property
    'Public Property ContactAddLine1() As String
    '    Get
    '        Return _ContactAddLine1
    '    End Get
    '    Set(ByVal Value As String)
    '        _ContactAddLine1 = Value
    '    End Set
    'End Property
    'Public Property ContactAddLine2() As String
    '    Get
    '        Return _ContactAddLine2
    '    End Get
    '    Set(ByVal Value As String)
    '        _ContactAddLine2 = Value
    '    End Set
    'End Property
    'Public Property ContactCity() As String
    '    Get
    '        Return _ContactCity
    '    End Get
    '    Set(ByVal Value As String)
    '        _ContactCity = Value
    '    End Set
    'End Property
    'Public Property ContactState() As String
    '    Get
    '        Return _ContactState
    '    End Get
    '    Set(ByVal Value As String)
    '        _ContactState = Value
    '    End Set
    'End Property
    'Public Property ContactZip() As String
    '    Get
    '        Return _ContactZip
    '    End Get
    '    Set(ByVal Value As String)
    '        _ContactZip = Value
    '    End Set
    'End Property
    'Public Property ContactCountry() As String
    '    Get
    '        Return _ContactCountry
    '    End Get
    '    Set(ByVal Value As String)
    '        _ContactCountry = Value
    '    End Set
    'End Property
    'Public Property ContactPhone() As String
    '    Get
    '        Return _ContactPhone
    '    End Get
    '    Set(ByVal Value As String)
    '        _ContactPhone = Value
    '    End Set
    'End Property
    Public Property ContactFax() As String
        Get
            Return _ContactFax
        End Get
        Set(ByVal Value As String)
            _ContactFax = Value
        End Set
    End Property
    Public Property ContactEmail() As String
        Get
            Return _ContactEmail
        End Get
        Set(ByVal Value As String)
            _ContactEmail = Value
        End Set
    End Property
    Public Property PracticeID() As String
        Get
            Return _PracticeID
        End Get
        Set(ByVal Value As String)
            _PracticeID = Value
        End Set
    End Property
    Public Sub New()
        _PersonContactPhone = New PhoneNumber
        _PersonContactAddress = New ContactAddress
    End Sub
End Class
Public Class PatientAuthor
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
    Private mPersonName As PersonName
    Private mDocumentId As String
    Private mOrganization As String
    Private mInformationTime As String
    Dim mPhone As String
    Dim mContactAddress As ContactAddress
    Public Property InformationTime() As String
        Get

            Return mInformationTime
        End Get
        Set(ByVal value As String)
            mInformationTime = value
        End Set
    End Property
    Public Property Organization() As String
        Get

            Return mOrganization
        End Get
        Set(ByVal value As String)
            mOrganization = value
        End Set
    End Property
    Public Property DocumentId() As String
        Get

            Return mDocumentId
        End Get
        Set(ByVal value As String)
            mDocumentId = value
        End Set
    End Property
    Public Property PersonName() As PersonName
        Get
            'If Not IsNothing(mPersonName) Then
            '    mPersonName = New PersonName
            'End If
            Return mPersonName
        End Get
        Set(ByVal value As PersonName)
            mPersonName = value
        End Set
    End Property
    Public Property Phone() As String
        Get
            Return mPhone
        End Get
        Set(ByVal Value As String)
            mPhone = Value
        End Set
    End Property
    Public Property PersonContactAddress() As ContactAddress
        Get
            Return mContactAddress
        End Get
        Set(ByVal Value As ContactAddress)
            mContactAddress = Value
        End Set
    End Property
    Public Sub New()
        mPersonName = New PersonName
        mContactAddress = New ContactAddress
    End Sub
End Class

Public Class gloPatientHistory
    Implements IDisposable
#Region "Constructor & Destructor"

    Public Sub New()

    End Sub

    Private disposed As Boolean = False

    'Public Sub Dispose()
    '    Dispose(True)
    '    GC.SuppressFinalize(Me)
    'End Sub

    Public Overridable Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        ' This object will be cleaned up by the Dispose method.
        ' Therefore, you should call GC.SupressFinalize to
        ' take this object off the finalization queue 
        ' and prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then

            End If
        End If
        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        'Try
        Dispose(False)
        'Finally
        MyBase.Finalize()
        'End Try
    End Sub

#End Region

#Region " Private Variables "
    Private _ClinicID As Int64 = 1
    Private _nHistoryID As Int64 = 0
    Private _nVisitID As Int64 = 0

    Private _nPatientID As Int64 = 0
    Private _blnIsDeleted As Boolean
    Private _sHistoryCategory As String = Nothing
    Private _sHistorySource As String = Nothing
    Private _sHistoryItem As String = Nothing
    Private _sComments As String = Nothing
    Private _sReaction As String = Nothing
    Private _sReactionCode As String = Nothing

    Private _nDrugID As Int64 = 0
    Private _sUserName As String = Nothing
    Private _sMachineName As String = Nothing
    Private _nmedicalconditionid As Long = 0

    Private _sDrugName As String = Nothing
    Private _sDosage As String = Nothing
    Private _sNDCCode As String = Nothing
    Private _nDDID As Int64 = 0
    Private _mpid As Int32 = 0
    Private _DOEAllergy As String = Nothing
    Private _ConceptId As String = Nothing
    Private _ReasonConceptId As String = Nothing
    Private _ReasonDesc As String = Nothing
    Private _DescId As String = Nothing
    Private _SnoMedId As String = Nothing
    Private _SnoDescription As String = Nothing
    Private _ICD9 As String = Nothing
    Private _ICD10 As String = Nothing
    Private _ICDRevision As Int16
    Private _RxNormCode As String = Nothing
    Private _sStatus As String = Nothing
    Private _HistoryType As String
    Private _CPT As String
    Private _HCPCS As String
    Private _OnsetDate As String
    Private _sSource As String
    Private _svalueset As String
    Private _svaluesetdescription As String
    Private _sConcernEndDate As String
    Private _sConcernstatus As String
    Private _sObservationEndDate As String
    Private _sSeverity As String
    Private _sSeverityCode As String
    Private _loinccode As String
#End Region


#Region " Property Procedures "
    Public Property Loinccode As String
        Get
            Return _loinccode
        End Get
        Set(value As String)
            _loinccode = value
        End Set
    End Property
    Public Property Severity As String
        Get
            Return _sSeverity
        End Get
        Set(value As String)
            _sSeverity = value
        End Set
    End Property
    Public Property SeverityCode As String
        Get
            Return _sSeverityCode
        End Get
        Set(value As String)
            _sSeverityCode = value
        End Set
    End Property
    Public Property ConcernStatus As String
        Get
            Return _sConcernstatus
        End Get
        Set(value As String)
            _sConcernstatus = value
        End Set
    End Property
    Public Property ConcernEndDate As String
        Get
            Return _sConcernEndDate
        End Get
        Set(value As String)
            _sConcernEndDate = value
        End Set
    End Property
    Public Property ObservationEndDate As String
        Get
            Return _sObservationEndDate
        End Get
        Set(ByVal value As String)
            _sObservationEndDate = value
        End Set
    End Property
    Public Property HistoryID() As Int64
        Get
            Return _nHistoryID
        End Get
        Set(ByVal value As Int64)
            _nHistoryID = value
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
    Public Property PatientID() As Int64
        Get
            Return _nPatientID
        End Get
        Set(ByVal value As Int64)
            _nPatientID = value
        End Set
    End Property
    Public Property IsDeleted() As Boolean
        Get
            Return _blnIsDeleted
        End Get
        Set(ByVal value As Boolean)
            _blnIsDeleted = value
        End Set
    End Property
    Public Property HistoryCategory() As String
        Get
            Return _sHistoryCategory
        End Get
        Set(ByVal value As String)
            _sHistoryCategory = value
        End Set
    End Property
    Public Property HistorySource() As String
        Get
            Return _sHistorySource
        End Get
        Set(ByVal value As String)
            _sHistorySource = value
        End Set
    End Property
    Public Property HistoryItem() As String
        Get
            Return _sHistoryItem
        End Get
        Set(ByVal value As String)
            _sHistoryItem = value
        End Set
    End Property
    Public Property Comments() As String
        Get
            Return _sComments
        End Get
        Set(ByVal value As String)
            _sComments = value
        End Set
    End Property
    Public Property Reaction() As String
        Get
            Return _sReaction
        End Get
        Set(ByVal value As String)
            _sReaction = value
        End Set
    End Property

    Public Property ReactionCode() As String
        Get
            Return _sReactionCode
        End Get
        Set(ByVal value As String)
            _sReactionCode = value
        End Set
    End Property

    Public Property DrugID() As Int64
        Get
            Return _nDrugID
        End Get
        Set(ByVal value As Int64)
            _nDrugID = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return _sUserName
        End Get
        Set(ByVal value As String)
            _sUserName = value
        End Set
    End Property
    Public Property MachineName() As String
        Get
            Return _sMachineName
        End Get
        Set(ByVal value As String)
            _sMachineName = value
        End Set
    End Property
    Public Property Medicalconditionid() As Long
        Get
            Return _nmedicalconditionid
        End Get
        Set(ByVal value As Long)
            _nmedicalconditionid = value
        End Set
    End Property
    Public Property DrugName() As String
        Get
            Return _sDrugName
        End Get
        Set(ByVal value As String)
            _sDrugName = value
        End Set
    End Property
    Public Property Dosage() As String
        Get
            Return _sDosage
        End Get
        Set(ByVal value As String)
            _sDosage = value
        End Set
    End Property
    Public Property NDCCode() As String
        Get
            Return _sNDCCode
        End Get
        Set(ByVal value As String)
            _sNDCCode = value
        End Set
    End Property
    Public Property mpid() As Int32
        Get
            Return _mpid
        End Get
        Set(ByVal value As Int32)
            _mpid = value
        End Set
    End Property

    Public Property DDID() As Int64
        Get
            Return _nDDID
        End Get
        Set(ByVal value As Int64)
            _nDDID = value
        End Set
    End Property
    Public Property DOEAllergy() As String
        Get
            Return _DOEAllergy
        End Get
        Set(ByVal value As String)
            _DOEAllergy = value
        End Set
    End Property
    Public Property ConceptId() As String
        Get
            Return _ConceptId
        End Get
        Set(ByVal value As String)
            _ConceptId = value
        End Set
    End Property
    Public Property ReasonConceptId() As String
        Get
            Return _ReasonConceptId
        End Get
        Set(ByVal value As String)
            _ReasonConceptId = value
        End Set
    End Property
    Public Property ReasonDesc() As String
        Get
            Return _ReasonDesc
        End Get
        Set(ByVal value As String)
            _ReasonDesc = value
        End Set
    End Property
    Public Property DescId() As String
        Get
            Return _DescId
        End Get
        Set(ByVal value As String)
            _DescId = value
        End Set
    End Property
    Public Property SnoMedId() As String
        Get
            Return _SnoMedId
        End Get
        Set(ByVal value As String)
            _SnoMedId = value
        End Set
    End Property
    Public Property SnoDescription() As String
        Get
            Return _SnoDescription
        End Get
        Set(ByVal value As String)
            _SnoDescription = value
        End Set
    End Property
    Public Property ICD9() As String
        Get
            Return _ICD9
        End Get
        Set(ByVal value As String)
            _ICD9 = value
        End Set
    End Property
    Public Property ICD10() As String
        Get
            Return _ICD10
        End Get
        Set(ByVal value As String)
            _ICD10 = value
        End Set
    End Property
    Public Property ICDRevision() As Int16
        Get
            Return _ICDRevision
        End Get
        Set(ByVal value As Int16)
            _ICDRevision = value
        End Set
    End Property
    Public Property RxNormCode() As String
        Get
            Return _RxNormCode
        End Get
        Set(ByVal value As String)
            _RxNormCode = value
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
    Public Property HCPCS() As String
        Get
            Return _HCPCS
        End Get
        Set(ByVal value As String)
            _HCPCS = value
        End Set
    End Property
    Public Property OnsetDate() As String
        Get
            Return _OnsetDate
        End Get
        Set(ByVal value As String)
            _OnsetDate = value
        End Set
    End Property
    Public Property HistoryType() As String
        Get
            Return _HistoryType
        End Get
        Set(ByVal value As String)
            _HistoryType = value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return _sStatus
        End Get
        Set(ByVal value As String)
            _sStatus = value
        End Set
    End Property
    Public Property Source() As String
        Get
            Return _sSource
        End Get
        Set(ByVal value As String)
            _sSource = value
        End Set
    End Property
    Public Property ValueSetOID As String
        Get
            Return _svalueset
        End Get
        Set(value As String)
            _svalueset = value
        End Set
    End Property
    Public Property ValueSet As String
        Get
            Return _svaluesetdescription
        End Get
        Set(value As String)
            _svaluesetdescription = value
        End Set
    End Property
#End Region
End Class

Public Class gloPatientHistoryCol
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
    ' This line declares the Item property as ReadOnly, and 
    Public ReadOnly Property Item(ByVal index As Integer) As gloPatientHistory
        Get
            ' The appropriate item is retrieved from the List object and 
            Return CType(List.Item(index), gloPatientHistory)
        End Get
    End Property
    'items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As gloPatientHistory)
        ' Invokes Add method of the List object to add a object
        List.Add(oConditionfield)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class User
    Private sCCRVersion As String = ""
    Private sUserName As String = ""
    Private nLoginID As Int64 = 0
    Private _ClinicID As Int64 = 1
    Public Property ClinicID() As Int64
        Get
            Return _ClinicID
        End Get
        Set(ByVal value As Int64)
            _ClinicID = value
        End Set
    End Property
    Public Property UserID() As Int64
        Get
            Return nLoginID
        End Get
        Set(ByVal value As Int64)
            nLoginID = value
        End Set
    End Property
    Public Property Version() As String
        Get
            Return sCCRVersion
        End Get
        Set(ByVal value As String)
            sCCRVersion = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return sUserName
        End Get
        Set(ByVal value As String)
            sUserName = value
        End Set
    End Property
End Class

Public Class PatientEducation
    Implements IDisposable

    Private sTemplateName As String
    Private _VisitDate As String

    Public Property TemplateName() As String
        Get
            Return sTemplateName
        End Get
        Set(ByVal value As String)
            sTemplateName = value
        End Set
    End Property
    Public Property VisitDate() As String
        Get
            Return _VisitDate
        End Get
        Set(ByVal value As String)
            _VisitDate = value
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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

Public Class Assessment
    Implements IDisposable

    Private sAssessmentdetail As String
    'Private _VisitDate As String

    Public Property Assessmentdetail() As String
        Get
            Return sAssessmentdetail
        End Get
        Set(ByVal value As String)
            sAssessmentdetail = value
        End Set
    End Property


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class PatientEducationCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As PatientEducation
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), PatientEducation)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oPatientEducation As PatientEducation)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oPatientEducation)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class PatientCarePlan
    Implements IDisposable

    Private sGoal As String
    Private sInstructions As String
    Public Property Goal() As String
        Get
            Return sGoal
        End Get
        Set(ByVal value As String)
            sGoal = value
        End Set
    End Property
    Public Property Instructions() As String
        Get
            Return sInstructions
        End Get
        Set(ByVal value As String)
            sInstructions = value
        End Set
    End Property


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class PatientCarePlanCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As PatientCarePlan
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), PatientCarePlan)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oPatientCarePlan As PatientCarePlan)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oPatientCarePlan)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class PatientClinicalInstruction
    Implements IDisposable


    Private sInstructions As String

    Public Property Instructions() As String
        Get
            Return sInstructions
        End Get
        Set(ByVal value As String)
            sInstructions = value
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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

Public Class PatientClinicalInstructionCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As PatientClinicalInstruction
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), PatientClinicalInstruction)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oPatientCarePlan As PatientClinicalInstruction)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oPatientCarePlan)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class Appointment
    Implements IDisposable

    Private dtStartDate As String
    Private sProvider As String
    Private sLocation As String
    Private nDuration As String
    Private _AppStreet As String
    Private _AppCity As String
    Private _AppState As String
    Private _AppZip As String
    Public Property AppState() As String
        Get
            Return _AppState
        End Get
        Set(ByVal value As String)
            _AppState = value
        End Set
    End Property
    Public Property AppZip() As String
        Get
            Return _AppZip
        End Get
        Set(ByVal value As String)
            _AppZip = value
        End Set
    End Property
    Public Property Duration() As String
        Get
            Return nDuration
        End Get
        Set(ByVal value As String)
            nDuration = value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            Return dtStartDate
        End Get
        Set(ByVal value As String)
            dtStartDate = value
        End Set
    End Property
    Public Property AppStreet() As String
        Get
            Return _AppStreet
        End Get
        Set(ByVal Value As String)
            _AppStreet = Value
        End Set
    End Property
    Public Property AppCity() As String
        Get
            Return _AppCity
        End Get
        Set(ByVal Value As String)
            _AppCity = Value
        End Set
    End Property
    Public Property Provider() As String
        Get
            Return sProvider
        End Get
        Set(ByVal value As String)
            sProvider = value
        End Set
    End Property
    Public Property Location() As String
        Get
            Return sLocation
        End Get
        Set(ByVal value As String)
            sLocation = value
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class AppointmentCol
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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Appointment
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Appointment)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oAppointment As Appointment)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oAppointment)
    End Sub
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
Public Class LoginProvider
    Implements IDisposable

    Private lPrefix As String
    Private lFirstName As String
    Private lMiddleName As String
    Private lLastName As String
    Private lSuffix As String

    Private lStreetAddress As String
    Private lCity As String
    Private lState As String
    Private lzip As String
    Private lCountry As String
    Private lHomePhone As String
    Private lWorkPhone As String
    Private lMobilePhone As String

    Private lNPI As String
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private _lProvTaxID As String
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub


    Public Property NPI() As String
        Get
            Return lNPI
        End Get
        Set(ByVal value As String)
            lNPI = value
        End Set
    End Property
    Public Property ProvTaxID() As String
        Get
            Return _lProvTaxID
        End Get
        Set(ByVal value As String)
            _lProvTaxID = value
        End Set
    End Property

    Public Property City() As String
        Get
            Return lCity
        End Get
        Set(ByVal value As String)
            lCity = value
        End Set
    End Property
    Public Property State() As String
        Get
            Return lState
        End Get
        Set(ByVal value As String)
            lState = value
        End Set
    End Property
    Public Property zip() As String
        Get
            Return lzip
        End Get
        Set(ByVal value As String)
            lzip = value
        End Set
    End Property
    Public Property Country() As String
        Get
            Return lCountry
        End Get
        Set(ByVal value As String)
            lCountry = value
        End Set
    End Property
    Public Property HomePhone() As String
        Get
            Return lHomePhone
        End Get
        Set(ByVal value As String)
            lHomePhone = value
        End Set
    End Property
    Public Property WorkPhone() As String
        Get
            Return lWorkPhone
        End Get
        Set(ByVal value As String)
            lWorkPhone = value
        End Set
    End Property
    Public Property MobilePhone() As String
        Get
            Return lMobilePhone
        End Get
        Set(ByVal value As String)
            lMobilePhone = value
        End Set
    End Property

    Public Property StreetAddress() As String
        Get
            Return lStreetAddress
        End Get
        Set(ByVal value As String)
            lStreetAddress = value
        End Set
    End Property

    Public Property Prefix() As String
        Get
            Return lPrefix
        End Get
        Set(ByVal value As String)
            lPrefix = value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return lFirstName
        End Get
        Set(ByVal value As String)
            lFirstName = value
        End Set
    End Property
    Public Property MiddleName() As String
        Get
            Return lMiddleName
        End Get
        Set(ByVal value As String)
            lMiddleName = value
        End Set
    End Property
    Public Property LastName() As String
        Get
            Return lLastName
        End Get
        Set(ByVal value As String)
            lLastName = value
        End Set
    End Property
    Public Property Suffix() As String
        Get
            Return lSuffix
        End Get
        Set(ByVal value As String)
            lSuffix = value
        End Set
    End Property

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class

Public Class Implant
    Implements IDisposable
    Dim _UDI As String = ""
    Dim _sConceptId As String = ""
    Dim _sDescription As String = ""
    Dim _dtImplantDate As String = ""
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private _DeviceId As Int64
    Private _IsUDI As Boolean = False
    Private _IsBloodcontainer As Boolean = False
    Private _IssuingAgency As String = ""
    Public Property IssueingAgency As String
        Get
            Return _IssuingAgency
        End Get
        Set(value As String)
            _IssuingAgency = value
        End Set
    End Property
    Public Property ISBloodContainer As Boolean
        Get
            Return _IsBloodcontainer
        End Get
        Set(value As Boolean)
            _IsBloodcontainer = value
        End Set
    End Property
    Public Property ISUDI As Boolean
        Get
            Return _IsUDI
        End Get
        Set(value As Boolean)
            _IsUDI = value
        End Set
    End Property
    Public Property DeviceID As Int64
        Get
            Return _DeviceId
        End Get
        Set(value As Int64)
            _DeviceId = value
        End Set
    End Property
    Public Property UDI As String
        Get
            Return _UDI
        End Get
        Set(value As String)
            _UDI = value
        End Set
    End Property

    Public Property DeviceCode As String
        Get
            Return _sConceptId
        End Get
        Set(value As String)
            _sConceptId = value
        End Set
    End Property
    Public Property Device_Description As String
        Get
            Return _sDescription
        End Get
        Set(value As String)
            _sDescription = value
        End Set
    End Property
    Public Property ImplantDate As String
        Get
            Return _dtImplantDate
        End Get
        Set(value As String)
            _dtImplantDate = value
        End Set
    End Property
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
Public Class gloPatientImplantCol
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
    ' This line declares the Item property as ReadOnly, and 
    Public ReadOnly Property Item(ByVal index As Integer) As Implant
        Get
            ' The appropriate item is retrieved from the List object and 
            Return CType(List.Item(index), Implant)
        End Get
    End Property
    ''items that can be added to the collection.
    'Public Sub Add(ByVal oConditionfield As Implant)
    '    ' Invokes Add method of the List object to add a object
    '    List.Add(oConditionfield)
    'End Sub
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
    Public Property implants() As List(Of Implant)
        Get
            Return _implants
        End Get
        Set(value As List(Of Implant))
            _implants = value
        End Set
    End Property
    Private _implants As List(Of Implant)

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub
End Class

Public Class LabLocation
    Implements IDisposable
    Private _labAdd1 As String = ""
    Private _labAdd2 As String = ""
    Private _labCity As String = ""
    Private _labState As String = ""
    Private _labZip As String = ""
    Private _labLocationName As String = ""
    Private _labPhone As String = ""
    Public Property LabZip As String
        Get
            Return _labZip
        End Get
        Set(value As String)
            _labZip = value
        End Set
    End Property
    Public Property LabState As String
        Get
            Return _labState
        End Get
        Set(value As String)
            _labState = value
        End Set
    End Property
    Public Property LabLocationName As String
        Get
            Return _labLocationName
        End Get
        Set(value As String)
            _labLocationName = value
        End Set
    End Property
    Public Property LabLocationTelephone As String
        Get
            Return _labPhone
        End Get
        Set(value As String)
            _labPhone = value
        End Set
    End Property
    Public Property LabLocationAdd1 As String
        Get
            Return _labAdd1
        End Get
        Set(value As String)
            _labAdd1 = value
        End Set
    End Property
    Public Property LabLocationAdd2 As String
        Get
            Return _labAdd2
        End Get
        Set(value As String)
            _labAdd2 = value
        End Set
    End Property
    Public Property LabCity As String
        Get
            Return _labCity
        End Get
        Set(value As String)
            _labCity = value
        End Set
    End Property
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class Goal
    Implements IDisposable
    Private _GoalId As String = ""
    Private _GoalName As String = ""
    Private _GoalLoincCode As String = ""
    Private _GoalLoincDesc As String = ""
    Private _GoalNarrative As String = ""
    Private _GoalValue As String = ""
    Private _GoalUnit As String = ""
    Private _GoalDate As String = ""
    Private _GoalAuthor As String = ""
    Private _AssociateId As String
    Private _AssociateType As Int16
    Private _GoalAuthorFirstName As String = ""
    Private _GoalAuthorMiddleName As String = ""
    Private _GoalAuthorLastName As String = ""
    Private _Guid As String = ""
    Public Property GUID As String
        Get
            Return _Guid
        End Get
        Set(value As String)
            _Guid = value
        End Set
    End Property
    Public Property GoalAuthorFirstName
        Get
            Return _GoalAuthorFirstName
        End Get
        Set(value)
            _GoalAuthorFirstName = value
        End Set
    End Property
    Public Property GoalAuthorMiddleName
        Get
            Return _GoalAuthorMiddleName
        End Get
        Set(value)
            _GoalAuthorMiddleName = value
        End Set
    End Property
    Public Property GoalAuthorLastName
        Get
            Return _GoalAuthorLastName
        End Get
        Set(value)
            _GoalAuthorLastName = value
        End Set
    End Property
    Public Property AssociateType As Int16
        Get
            Return _AssociateType
        End Get
        Set(value As Int16)
            _AssociateType = value
        End Set
    End Property
    Public Property AssociateId As String
        Get
            Return _AssociateId
        End Get
        Set(value As String)
            _AssociateId = value
        End Set
    End Property
    Public Property GoalId As String
        Get
            Return _GoalId
        End Get
        Set(value As String)
            _GoalId = value
        End Set
    End Property
    Public Property GoalName As String
        Get
            Return _GoalName
        End Get
        Set(value As String)
            _GoalName = value
        End Set
    End Property
    Public Property GoalLoincCode As String
        Get
            Return _GoalLoincCode
        End Get
        Set(value As String)
            _GoalLoincCode = value
        End Set
    End Property
    Public Property GoalLoincDesc As String
        Get
            Return _GoalLoincDesc
        End Get
        Set(value As String)
            _GoalLoincDesc = value
        End Set
    End Property
    Public Property GoalNarrative As String
        Get
            Return _GoalNarrative
        End Get
        Set(value As String)
            _GoalNarrative = value
        End Set
    End Property
    Public Property GoalValue As String
        Get
            Return _GoalValue
        End Get
        Set(value As String)
            _GoalValue = value
        End Set
    End Property
    Public Property GoalUnit As String
        Get
            Return _GoalUnit
        End Get
        Set(value As String)
            _GoalUnit = value
        End Set
    End Property

    Public Property GoalAuthor As String
        Get
            Return _GoalAuthor
        End Get
        Set(value As String)
            _GoalAuthor = value
        End Set
    End Property
    Public Property GoalDate As String
        Get
            Return _GoalDate
        End Get
        Set(value As String)
            _GoalDate = value
        End Set
    End Property



#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class GoalsCol
    Private _GoalList As List(Of Goal)
    Public Property GoalsList As List(Of Goal)
        Get
            Return _GoalList
        End Get
        Set(value As List(Of Goal))
            _GoalList = value
        End Set
    End Property
End Class
Public Class HealthConcern
    Implements IDisposable
    Private _ConcernId As String = ""
    Private _ConcernName As String = ""
    Private _ConcernSnomedID As String = ""
    Private _ConcernSnomedDesc As String = ""
    Private _ConcernAuthor As String = ""
    Private _ConcernStatus As String = ""
    Private _ConcernStartdate As String = ""
    Private _ConcernEnddate As String = ""
    Private _ConcernAssociateConcernid As String = ""
    Private _HealthStatusDesc As String = ""
    Private _HealthConcernNarrative As String = ""
    Private _GUID As String = ""
    Public Property GUID As String
        Get
            Return _GUID
        End Get
        Set(value As String)
            _GUID = value
        End Set
    End Property
    Public Property HealthConcernID
        Get
            Return _ConcernId
        End Get
        Set(value)
            _ConcernId = value
        End Set
    End Property
    Public Property HealthConcernName
        Get
            Return _ConcernName
        End Get
        Set(value)
            _ConcernName = value
        End Set
    End Property
    Public Property HealthConcernSNOMEDID
        Get
            Return _ConcernSnomedID
        End Get
        Set(value)
            _ConcernSnomedID = value
        End Set
    End Property
    Public Property HealthConcernSnomedDesc
        Get
            Return _ConcernSnomedDesc
        End Get
        Set(value)
            _ConcernSnomedDesc = value
        End Set
    End Property
    Public Property HealthConcernAuthor
        Get
            Return _ConcernAuthor
        End Get
        Set(value)
            _ConcernAuthor = value
        End Set
    End Property
    Public Property HealthConcernStatus
        Get
            Return _ConcernStatus
        End Get
        Set(value)
            _ConcernStatus = value
        End Set
    End Property
    Public Property ConcernStartDate
        Get
            Return _ConcernStartdate
        End Get
        Set(value)
            _ConcernStartdate = value
        End Set
    End Property
    Public Property ConcernEndDate
        Get
            Return _ConcernEnddate
        End Get
        Set(value)
            _ConcernEnddate = value
        End Set
    End Property
    Public Property AssociateConcernID
        Get
            Return _ConcernAssociateConcernid
        End Get
        Set(value)
            _ConcernAssociateConcernid = value
        End Set
    End Property
    Public Property HealthStatusDesc
        Get
            Return _HealthStatusDesc
        End Get
        Set(value)
            _HealthStatusDesc = value
        End Set
    End Property
    Public Property HealthConcernNarrative
        Get
            Return _HealthConcernNarrative
        End Get
        Set(value)
            _HealthConcernNarrative = value
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class HealthConcernCol
    Inherits CollectionBase
    Private _HealthConcernList As List(Of HealthConcern)
    Public Property HealthConcernList As List(Of HealthConcern)
        Get
            Return _HealthConcernList
        End Get
        Set(value As List(Of HealthConcern))
            _HealthConcernList = value
        End Set
    End Property
End Class
Public Class InterventionCol
    Private _InterventionList As List(Of Intervention)
    Private _PLnnedInterventionList As List(Of Intervention)
    Public Property InterventionList As List(Of intervention)
        Get
            Return _InterventionList
        End Get
        Set(value As List(Of intervention))
            _InterventionList = value
        End Set
    End Property
    Public Property PlannedIntervention As List(Of Intervention)
        Get
            Return _PLnnedInterventionList
        End Get
        Set(value As List(Of Intervention))
            _PLnnedInterventionList = value
        End Set
    End Property
End Class
Public Class Intervention
    Implements IDisposable

    Private _interventiontype As String = ""
    Private _ninterventionId As Long
    Private sInterventionName As String = ""
    Private sInterventionNotes As String = ""
    Private _InterventinRecordDate As String = ""
    Private _nAssociateId As String = ""
    Private _nRelativeId As String = ""
    Private _nPlanOfTreatmentId As Long
    Private _nassociationType As Int16
    Private snutritionRecomendationCode As String
    Private snutritionRecomendationDesc As String
    Private sPlannedLOINCCode As String = ""
    Private sPlannedLOINCName As String = ""
    Private sPlannedObsdate As String = ""
    Private sPlannedEncounterCode As String = ""
    Private sPlannedEncounterName As String = ""
    Private sPlannedEncounterDate As String = ""
    Private sPlannedMedCode As String = ""
    Private sPlannedMedicationName As String = ""
    Private sPlannedMedicationdate As String = ""
    Private sPlannedRxNormCode As String = ""
    Private _GUID As String = "'"
    Private _sNutritionInstruction As String = ""
    Public Property NutritionInstruction As String
        Get
            Return _sNutritionInstruction
        End Get
        Set(value As String)
            _sNutritionInstruction = value
        End Set
    End Property
    Public Property GUID As String
        Get
            Return _GUID
        End Get
        Set(value As String)
            _GUID = value
        End Set
    End Property

    Public Property PlannedRxNormCode As String
        Get
            Return sPlannedRxNormCode
        End Get
        Set(value As String)
            sPlannedRxNormCode = value
        End Set
    End Property
    Public Property PlannedmedicationDate As String
        Get
            Return sPlannedMedicationdate
        End Get
        Set(value As String)
            sPlannedMedicationdate = value
        End Set
    End Property
    Public Property PlannedMedicatinName As String
        Get
            Return sPlannedMedicationName
        End Get
        Set(value As String)
            sPlannedMedicationName = value
        End Set
    End Property
    Public Property PlannedMedicationCode As String
        Get
            Return sPlannedMedCode
        End Get
        Set(value As String)
            sPlannedMedCode = value
        End Set
    End Property
    Public Property PlannedEncounterDate As String
        Get
            Return sPlannedEncounterDate
        End Get
        Set(value As String)
            sPlannedEncounterDate = value
        End Set
    End Property
    Public Property PlannedEncounterName As String
        Get
            Return sPlannedEncounterName
        End Get
        Set(value As String)
            sPlannedEncounterName = value
        End Set
    End Property
    Public Property PlannedEncounterCode As String
        Get
            Return sPlannedEncounterCode
        End Get
        Set(value As String)
            sPlannedEncounterCode = value
        End Set
    End Property
    Public Property PlannedObsDate
        Get
            Return sPlannedObsdate
        End Get
        Set(value)
            sPlannedObsdate = value
        End Set
    End Property
    Public Property PlannedLOINCName As String
        Get
            Return sPlannedLOINCName
        End Get
        Set(value As String)
            sPlannedLOINCName = value
        End Set
    End Property
    Public Property PlannedLOINCCode As String
        Get
            Return sPlannedLOINCCode
        End Get
        Set(value As String)
            sPlannedLOINCCode = value
        End Set
    End Property
    Public Property NutritionRecomendationDesc As String
        Get
            Return snutritionRecomendationDesc
        End Get
        Set(value As String)
            snutritionRecomendationDesc = value
        End Set
    End Property
    Public Property NutritionRecomendation As String
        Get
            Return snutritionRecomendationCode
        End Get
        Set(value As String)
            snutritionRecomendationCode = value
        End Set
    End Property
    Public Property AssociateId As String
        Get
            Return _nAssociateId
        End Get
        Set(value As String)
            _nAssociateId = value
        End Set
    End Property
    Public Property RelativeId As String
        Get
            Return _nRelativeId
        End Get
        Set(value As String)
            _nRelativeId = value
        End Set
    End Property
    Public Property AssociateType As Long
        Get
            Return _nassociationType
        End Get
        Set(value As Long)
            _nassociationType = value
        End Set
    End Property

    Public Property InterventionrecordDate As String
        Get
            Return _InterventinRecordDate
        End Get
        Set(value As String)
            _InterventinRecordDate = value
        End Set
    End Property
    Public Property InterventionNotes As String
        Get
            Return sInterventionNotes
        End Get
        Set(value As String)
            sInterventionNotes = value
        End Set
    End Property
    Public Property InterventionName As String
        Get
            Return sInterventionName
        End Get
        Set(value As String)
            sInterventionName = value
        End Set
    End Property
    Public Property InterventionId As Long
        Get
            Return _ninterventionId
        End Get
        Set(value As Long)
            _ninterventionId = value
        End Set
    End Property
    Public Property InterventionType As String
        Get
            Return _interventiontype
        End Get
        Set(value As String)
            _interventiontype = value
        End Set
    End Property
    Public Property PlanOfTreatmentId As Long
        Get
            Return _nPlanOfTreatmentId
        End Get
        Set(value As Long)
            _nPlanOfTreatmentId = value
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class PlannedModule
    Implements IDisposable
    Private _PlannedCode As String = ""
    Private _plannedName As String = ""
    Private _plannedmoduleType As String = ""
    Private _EffectivePlanneddate As String = "'"
    Private _Details As String = "'"
    Private _plannedRxNormCode As String = ""
    Private _PlannedStatus As String = ""
    Private _EffectivePlannedEndDate As String = ""

    Public Property PlannedStatus As String
        Get
            Return _PlannedStatus
        End Get
        Set(value As String)
            _PlannedStatus = value
        End Set
    End Property
    Public Property PlannedRxNormcode As String
        Get
            Return _plannedRxNormCode
        End Get
        Set(value As String)
            _plannedRxNormCode = value
        End Set
    End Property
    Public Property Details As String
        Get
            Return _Details
        End Get
        Set(value As String)
            _Details = value
        End Set
    End Property
    Public Property EffectivePlannedDate As String
        Get
            Return _EffectivePlanneddate
        End Get
        Set(value As String)
            _EffectivePlanneddate = value
        End Set
    End Property
    Public Property PlannedModuletype
        Get
            Return _plannedmoduleType
        End Get
        Set(value)
            _plannedmoduleType = value
        End Set
    End Property
    Public Property PlannedCode As String
        Get
            Return _PlannedCode
        End Get
        Set(value As String)
            _PlannedCode = value
        End Set
    End Property
    Public Property PlannedName As String
        Get
            Return _plannedName
        End Get
        Set(value As String)
            _plannedName = value
        End Set
    End Property
    Public Property PlannedEndDate As String
        Get
            Return _EffectivePlannedEndDate
        End Get
        Set(value As String)
            _EffectivePlannedEndDate = value
        End Set
    End Property
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class PlannedModuleCol
    Inherits CollectionBase
    Private _plannedModuleList As List(Of PlannedModule)
    Public Property PlannedModuleList As List(Of PlannedModule)
        Get
            Return _plannedModuleList
        End Get
        Set(value As List(Of PlannedModule))
            _plannedModuleList = value
        End Set
    End Property
End Class

Public Class Outcome
    Implements IDisposable
    Private noutcomeid As Long
    Private soutcomeName As String
    Private soutcomestatus As String
    Private soutcomenotes As String
    Private dtoutcomedate As String
    Private nAssociateId As Long
    Private nAssociateType As Int16
    Private _OutcomeValue As String
    Private _OutcomeValueUnit As String
    Public Property OutcomeValueUnit As String
        Get
            Return _OutcomeValueUnit
        End Get
        Set(value As String)
            _OutcomeValueUnit = value
        End Set
    End Property
    Public Property OutcomeValue As String
        Get
            Return _OutcomeValue
        End Get
        Set(value As String)
            _OutcomeValue = value
        End Set
    End Property
    Public Property AssociateType As Int16
        Get
            Return nAssociateType
        End Get
        Set(value As Int16)
            nAssociateType = value
        End Set
    End Property
    Public Property AssociateId As Long
        Get
            Return nAssociateId
        End Get
        Set(value As Long)
            nAssociateId = value
        End Set
    End Property
    Public Property Outcomedate As String
        Get
            Return dtoutcomedate
        End Get
        Set(value As String)
            dtoutcomedate = value
        End Set
    End Property
    Public Property OutcomeNotes As String
        Get
            Return soutcomenotes
        End Get
        Set(value As String)
            soutcomenotes = value
        End Set
    End Property
    Public Property Outcomestatus As String
        Get
            Return soutcomestatus
        End Get
        Set(value As String)
            soutcomestatus = value
        End Set
    End Property
    Public Property OutcomeName As String
        Get
            Return soutcomeName
        End Get
        Set(value As String)
            soutcomeName = value
        End Set
    End Property
    Public Property OutcomeId As Long
        Get
            Return noutcomeid
        End Get
        Set(value As Long)
            noutcomeid = value
        End Set
    End Property

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

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
Public Class OutcomeCol
    Private _OutcomeList As List(Of Outcome)
    Public Property OutcomeList As List(Of Outcome)
        Get
            Return _OutcomeList
        End Get
        Set(value As List(Of Outcome))
            _OutcomeList = value
        End Set
    End Property
End Class



