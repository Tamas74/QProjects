Imports gloGlobal.Schemas.PDR

Public Class EPrescription

    Implements IDisposable


    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mFormularyCol As BenefitsCoordinations
    Private mDrugsCol As EDrugs
    Private mPrescriber As Prescriber
    Private mRxReferenceNumber As String = ""
    Private mFileData As Byte() = Nothing
    Private mPharmacy As Pharmacy
    Private mPatient As Patient
    Private mPrescriptiondate As String
    Private mRxTransactionID As String
    Private mVisitId As Int64
    Private mPatientID As Int64
    Private mProviderID As Int64 'Provider ID
    Private mClinicName As String
    Private mMessageStatus As String = ""

    'this wil save the eRx message status i.e. it it is posted or error
    Private mERxStatus As String = ""
    Private mERxStatusMessage As String = ""
    Private mSupervisorPrescriber As Supervisor
    Private mSupervisorProviderID As Int64
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                If Not IsNothing(mFormularyCol) Then
                    mFormularyCol.Dispose()
                    mFormularyCol = Nothing
                End If
                If Not IsNothing(mDrugsCol) Then
                    mDrugsCol.Dispose()
                    mDrugsCol = Nothing
                End If
                If Not IsNothing(mPrescriber) Then
                    mPrescriber.Dispose()
                    mPrescriber = Nothing
                End If
                If Not IsNothing(mPharmacy) Then
                    mPharmacy.Dispose()
                    mPharmacy = Nothing
                End If
                If Not IsNothing(mPatient) Then
                    mPatient.Dispose()
                    mPatient = Nothing
                End If
                If Not IsNothing(mSupervisorPrescriber) Then
                    mSupervisorPrescriber.Dispose()
                    mSupervisorPrescriber = Nothing
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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Property MessageStatus() As String
        Get
            Return mMessageStatus
        End Get
        Set(ByVal value As String)
            mMessageStatus = value
        End Set
    End Property

    Public Property ERxStatus() As String
        Get
            Return mERxStatus
        End Get
        Set(ByVal value As String)
            mERxStatus = value
        End Set
    End Property

    Public Property ERxStatusMessage() As String
        Get
            Return mERxStatusMessage
        End Get
        Set(ByVal value As String)
            mERxStatusMessage = value
        End Set
    End Property

    Public Property ClinicName() As String
        Get
            Return mClinicName
        End Get
        Set(ByVal value As String)
            mClinicName = value
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

    Public Property RxReferenceNumber() As String
        Get
            Return mRxReferenceNumber
        End Get
        Set(ByVal value As String)
            mRxReferenceNumber = value
        End Set
    End Property

    Public Property ProviderID() As String
        Get
            Return mProviderID
        End Get
        Set(ByVal value As String)
            mProviderID = value
        End Set
    End Property

    Public Property RxTransactionID() As String
        Get
            Return mRxTransactionID
        End Get
        Set(ByVal value As String)
            mRxTransactionID = value
        End Set
    End Property

    Public Property VisitID() As Int64
        Get
            Return mVisitId
        End Get
        Set(ByVal value As Int64)
            mVisitId = value
        End Set
    End Property

    Public Property PatientID() As Int64
        Get
            Return mPatientID
        End Get
        Set(ByVal value As Int64)
            mPatientID = value
        End Set
    End Property

    Public Property PrescriptionDate() As String
        Get
            Return mPrescriptiondate
        End Get
        Set(ByVal value As String)
            mPrescriptiondate = value
        End Set
    End Property

    Public Property RxPatient() As Patient
        Get
            Return mPatient
        End Get
        Set(ByVal value As Patient)
            mPatient = value
        End Set
    End Property

    Public Property RxPharmacy() As Pharmacy
        Get
            Return mPharmacy
        End Get
        Set(ByVal value As Pharmacy)
            mPharmacy = value
        End Set
    End Property

    Public Property DrugsCol() As EDrugs
        Get
            Return mDrugsCol
        End Get
        Set(ByVal value As EDrugs)
            mDrugsCol = value
        End Set
    End Property
    Public Property eRxMultipleFilePath As List(Of KeyValuePair(Of String, String))
    Public Property eRxUILaunchSigningFilePath() As String
    Public Property eRxWSGetPrescriptionStatusFilePath() As String
    Public Property RouterName() As String
    Public Property FormularyCol() As BenefitsCoordinations
        Get
            Return mFormularyCol
        End Get
        Set(ByVal value As BenefitsCoordinations)
            mFormularyCol = value
        End Set
    End Property

    Public Property RxPrescriber() As Prescriber
        Get
            Return mPrescriber
        End Get
        Set(ByVal value As Prescriber)
            mPrescriber = value
        End Set
    End Property

    Public Property RxSupervisorPrescriber() As Supervisor
        Get
            Return mSupervisorPrescriber
        End Get
        Set(ByVal value As Supervisor)
            mSupervisorPrescriber = value
        End Set
    End Property
    Public Property RxSupervisorProviderID() As Int64
        Get
            Return mSupervisorProviderID
        End Get
        Set(ByVal value As Int64)
            mSupervisorProviderID = value
        End Set
    End Property
    Public Sub New()
        MyBase.New()
        mDrugsCol = New EDrugs
        mFormularyCol = New BenefitsCoordinations
        mPharmacy = New Pharmacy
        mPrescriber = New Prescriber
        mPatient = New Patient
        mSupervisorPrescriber = New Supervisor
    End Sub
End Class

Public Class EDrug
    Inherits SureScriptMessage
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mDrugName As String = ""
    Private mDrugForm As String = ""
    Private mDosage As String = ""
    Private mDrugRoute As String = ""
    Private mDrugFrequency As String = ""
    Private mDrugDuration As String = ""
    Private mDrugStrength As String = ""
    Private mDrugQuantity As String = ""
    Private mDrugQuantityQualifier As String = ""
    Private mRefillQuantity As String = ""
    Private mDirections As String = ""
    Private mMaySubstitute As Boolean = "False"
    Private mWrittendate As String = ""
    Private mRefillsQualifier As String = "R"
    Private mPrescriptionID As String = ""
    Private mIseRxed As Int32 = 0
    Private mRxReferenceNumber As String = ""
    Private mDrugID As String = ""
    Private mStatus As String = ""
    Private mNotes As String = ""
    Private mIsNarcotics As Int16

    Private mProdCode As String = ""
    Private mProdCodeQualifier As String = ""
    Private mDosageForm As String = ""
    Private mDosageDescription As String = ""
    Private mMDDosageDescription As String = ""
    Private mStrength As String = ""
    Private mStrengthUnits As String = ""
    Private mDrugDBCode As String = ""
    Private mDrugDBCodeQualifier As String = ""
    Private mPrimaryDXQualifier As String = ""
    Private mPrimaryDXValue As String = ""
    Private mSecondaryDXQualifier As String = ""
    Private mSecondaryDXValue As String = ""
    Private mPriorAuthorizationQualifier As String = ""
    Private mPriorAuthorizationValue As String = ""
    Private mDgClQualifier1 As String = ""
    Private mPrimaryQualifier1 As String = ""
    Private mPrimaryValue1 As String = ""
    Private mSecQualifier1 As String = ""
    Private mSecValue1 As String = ""
    Private mDgClQualifier2 As String = ""
    Private mPrimaryQualifier2 As String = ""
    Private mPrimaryValue2 As String = ""
    Private mSecQualifier2 As String = ""
    Private mSecValue2 As String = ""
    Private mLastfillDate As String = ""

    Private mPotencyCode As String = ""
    Private mDrugCoverageStatusCode As String = ""

    ''medication dispensed
    Private mMDDrugName As String = ""
    Private mMDDrugForm As String = ""
    Private mMDStrength As String = ""
    Private mMDDosage As String = ""
    Private mMDRoute As String = ""
    Private mMDFrequency As String = ""
    Private mMDDuration As String = ""
    Private mMDQuantity As String = ""
    Private mMDRefillQuantity As String = ""
    Private mMDRefillQualifier As String = ""
    Private mMDbMaySubstitutions As String = ""
    Private mMDdtWrittendate As String = ""
    Private mMDProductCode As String = ""
    Private mMDPotencyUnitCode As String = ""
    Private mMDdtlastdate As String = ""

    Private sMDProductCodeQualifier As String = ""
    Private sMDDosageForm As String = ""
    Private sMDFrequency As String = ""
    Private sMDStrengthUnits As String = ""
    Private sMDDrugDBCode As String = ""
    Private sMDDrugDBCodeQualifier As String = ""
    Private sMDNotes As String = ""
    Private sMDDrugCoverageStatusCode As String = ""

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
    Private _PhNPI As String = ""
    'For Pharmacy
    Private _MessageType As String = ""
    Private _eRxFilePath As String = ""
    Private _eRxFilePath2 As String = ""
    Private _ItemNumber As Integer
    Private mFileData As Byte() = Nothing

    'For ePA Prior Authorization Status. Values will be
    'either of A,D,F,N,R as according to NCPDP External Code List document
    'page 160
    Private sPriorAuthorizationStatus As String = ""

    Public Property PriorAuthorizationStatus() As String
        Get
            Return sPriorAuthorizationStatus
        End Get
        Set(ByVal value As String)
            sPriorAuthorizationStatus = value
        End Set
    End Property

    Private sPCTransactionID As Long

    Public Property PCTransactionID() As Long
        Get
            Return sPCTransactionID
        End Get
        Set(ByVal value As Long)
            sPCTransactionID = value
        End Set
    End Property

    Private _pcPrograms As ProgramResponse

    Public Property PCPrograms() As ProgramResponse
        Get
            Return _pcPrograms
        End Get
        Set(ByVal value As ProgramResponse)
            _pcPrograms = value
        End Set
    End Property


    Private mmpid As Int32 = 0
    Private mDDID As String = "" ''''MU Certification

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
    Public Property mpid() As Int32
        Get
            Return mmpid
        End Get
        Set(ByVal value As Int32)
            mmpid = value
        End Set
    End Property
    Public Property DDID() As String
        Get
            Return mDDID
        End Get
        Set(ByVal value As String)
            mDDID = value
        End Set
    End Property
    Public Property Notes() As String
        Get
            Return mNotes
        End Get
        Set(ByVal value As String)
            mNotes = value
        End Set
    End Property

    Public Property Status() As String
        Get
            Return mStatus
        End Get
        Set(ByVal value As String)
            mStatus = value
        End Set
    End Property

    Public Property DrugID() As String
        Get
            Return mDrugID
        End Get
        Set(ByVal value As String)
            mDrugID = value
        End Set
    End Property

    Public Property RxReferenceNumber() As String
        Get
            Return mRxReferenceNumber
        End Get
        Set(ByVal value As String)
            mRxReferenceNumber = value
        End Set
    End Property

    Public Property PrescriptionID() As String
        Get
            Return mPrescriptionID
        End Get
        Set(ByVal value As String)
            mPrescriptionID = value
        End Set
    End Property

    Public Property IseRxed() As Int32
        Get
            Return mIseRxed
        End Get
        Set(ByVal value As Int32)
            mIseRxed = value
        End Set
    End Property

    Private bIseRxSuccessful As Boolean
    Public Property IsERXSuccessful() As Boolean
        Get
            Return bIseRxSuccessful
        End Get
        Set(ByVal value As Boolean)
            bIseRxSuccessful = value
        End Set
    End Property


    Public Property RefillsQualifier() As String
        Get
            Return mRefillsQualifier
        End Get
        Set(ByVal value As String)
            mRefillsQualifier = value
        End Set
    End Property

    Public Property MaySubstitute() As Boolean
        Get
            Return mMaySubstitute
        End Get
        Set(ByVal value As Boolean)
            mMaySubstitute = value
        End Set
    End Property

    Public Property DrugStrength() As String
        Get
            Return mDrugStrength
        End Get
        Set(ByVal value As String)
            mDrugStrength = value
        End Set
    End Property

    Public Property DrugDuration() As String
        Get
            Return mDrugDuration
        End Get
        Set(ByVal value As String)
            mDrugDuration = value
        End Set
    End Property
    Public Property DrugFrequency() As String
        Get
            Return mDrugFrequency
        End Get
        Set(ByVal value As String)
            mDrugFrequency = value
        End Set
    End Property

    Public Property DrugRoute() As String
        Get
            Return mDrugRoute
        End Get
        Set(ByVal value As String)
            mDrugRoute = value
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

    Public Property LastfillDate() As String
        Get
            Return mLastfillDate
        End Get
        Set(ByVal value As String)
            mLastfillDate = value
        End Set
    End Property

    'Public Property DrugQuantityQualifier() As String
    '    Get
    '        Return mDrugQuantityQualifier
    '    End Get
    '    Set(ByVal value As String)
    '        mDrugQuantityQualifier = value
    '    End Set
    'End Property

    Public Property Directions() As String
        Get
            If String.IsNullOrWhiteSpace(mDirections) Then
                Return mDirections
            Else
                Return mDirections.Trim()
            End If
        End Get
        Set(ByVal value As String)
            mDirections = value
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

    Public Property RefillQuantity() As String
        Get
            Return mRefillQuantity
        End Get
        Set(ByVal value As String)
            mRefillQuantity = value
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

    Public Property Drugform() As String
        Get
            Return mDrugForm
        End Get
        Set(ByVal value As String)
            mDrugForm = value
        End Set
    End Property

    Public Property Dosage() As String
        Get
            Return mDosage
        End Get
        Set(ByVal value As String)
            mDosage = value
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

    Public Property ProdCodeQualifier() As String
        Get
            Return mProdCodeQualifier
        End Get
        Set(ByVal value As String)
            mProdCodeQualifier = value
        End Set
    End Property
    Public Property IsNarcotics() As Int16
        Get
            Return mIsNarcotics
        End Get
        Set(ByVal Value As Int16)
            mIsNarcotics = Value
        End Set
    End Property
    Public Property DosageForm() As String
        Get
            Return mDosageForm
        End Get
        Set(ByVal value As String)
            mDosageForm = value
        End Set
    End Property
    Public Property DosageDescription() As String
        Get
            Return mDosageDescription
        End Get
        Set(ByVal value As String)
            mDosageDescription = value
        End Set
    End Property
    Public Property MDDosageDescription() As String
        Get
            Return mMDDosageDescription
        End Get
        Set(ByVal value As String)
            mMDDosageDescription = value
        End Set
    End Property
    Public Property Strength() As String
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

    Public Property DrugDBCode() As String
        Get
            Return mDrugDBCode
        End Get
        Set(ByVal value As String)
            mDrugDBCode = value
        End Set
    End Property

    Public Property DrugDBCodeQualifier() As String
        Get
            Return mDrugDBCodeQualifier
        End Get
        Set(ByVal value As String)
            mDrugDBCodeQualifier = value
        End Set
    End Property

    Public Property PrimaryDXQualifier() As String
        Get
            Return mPrimaryDXQualifier
        End Get
        Set(ByVal value As String)
            mPrimaryDXQualifier = value
        End Set
    End Property

    Public Property PrimaryDXValue() As String
        Get
            Return mPrimaryDXValue
        End Get
        Set(ByVal value As String)
            mPrimaryDXValue = value
        End Set
    End Property

    Public Property SecondaryDXQualifier() As String
        Get
            Return mSecondaryDXQualifier
        End Get
        Set(ByVal value As String)
            mSecondaryDXQualifier = value
        End Set
    End Property

    Public Property SecondaryDXValue() As String
        Get
            Return mSecondaryDXValue
        End Get
        Set(ByVal value As String)
            mSecondaryDXValue = value
        End Set
    End Property

    Public Property PriorAuthorizationQualifier() As String
        Get
            Return "G1"
        End Get
        Set(ByVal value As String)
            mPriorAuthorizationQualifier = value
        End Set
    End Property

    Public Property PriorAuthorizationValue() As String
        Get
            Return mPriorAuthorizationValue
        End Get
        Set(ByVal value As String)
            mPriorAuthorizationValue = value
        End Set
    End Property

    Public Property DgClQualifier1() As String
        Get
            Return mDgClQualifier1
        End Get
        Set(ByVal value As String)
            mDgClQualifier1 = value
        End Set
    End Property

    Public Property PrimaryQualifier1() As String
        Get
            Return mPrimaryQualifier1
        End Get
        Set(ByVal value As String)
            mPrimaryQualifier1 = value
        End Set
    End Property

    Public Property PrimaryValue1() As String
        Get
            Return mPrimaryValue1
        End Get
        Set(ByVal value As String)
            mPrimaryValue1 = value
        End Set
    End Property

    Public Property SecQualifier1() As String
        Get
            Return mSecQualifier1
        End Get
        Set(ByVal value As String)
            mSecQualifier1 = value
        End Set
    End Property

    Public Property SecValue1() As String
        Get
            Return mSecValue1
        End Get
        Set(ByVal value As String)
            mSecValue1 = value
        End Set
    End Property

    Public Property DgClQualifier2() As String
        Get
            Return mDgClQualifier2
        End Get
        Set(ByVal value As String)
            mDgClQualifier2 = value
        End Set
    End Property

    Public Property PrimaryQualifier2() As String
        Get
            Return mPrimaryQualifier2
        End Get
        Set(ByVal value As String)
            mPrimaryQualifier2 = value
        End Set
    End Property

    Public Property PrimaryValue2() As String
        Get
            Return mPrimaryValue2
        End Get
        Set(ByVal value As String)
            mPrimaryValue2 = value
        End Set
    End Property

    Public Property SecQualifier2() As String
        Get
            Return mSecQualifier2
        End Get
        Set(ByVal value As String)
            mSecQualifier2 = value
        End Set
    End Property

    Public Property SecValue2() As String
        Get
            Return mSecValue2
        End Get
        Set(ByVal value As String)
            mSecValue2 = value
        End Set
    End Property

    Public Property PotencyCode() As String
        Get
            Return mPotencyCode
        End Get
        Set(ByVal value As String)
            mPotencyCode = value
        End Set
    End Property
    Public Property DrugCoverageStatusCode As String
        Get
            Return mDrugCoverageStatusCode
        End Get
        Set(ByVal value As String)
            mDrugCoverageStatusCode = value
        End Set
    End Property
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
    Public Property PhNPI As String
        Get
            Return _PhNPI
        End Get
        Set(ByVal value As String)
            _PhNPI = value
        End Set
    End Property
    ''For Pharmacy
    Public Property eRxFilePath() As String
        Get
            Return _eRxFilePath
        End Get
        Set(ByVal value As String)
            _eRxFilePath = value
        End Set
    End Property
    Public Property eRxFilePath2() As String
        Get
            Return _eRxFilePath2
        End Get
        Set(ByVal value As String)
            _eRxFilePath2 = value
        End Set
    End Property
    Public Property eRxEPCSDrugCheckFilePath() As String
    Public Property IsEPCSDrugCheckSuccess As Boolean
    Public Property eRxUILaunchSigningFilePath() As String
    ' Public Property eRxWSGetPrescriptionStatusFilePath() As String
    Public Property EPCSDEASchedule() As String
    'Public Property EPCSDrugCheckStatus() As String
    ''''EPCS eRx Status
    Public Property EPCSPrescriptionStatusMessage() As String
    Public Property EPCSPrescriptionStatusLabel() As String
    Public Property MessageType() As String
        Get
            Return _MessageType
        End Get
        Set(ByVal value As String)
            _MessageType = value
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
    Public Property FileData() As Byte()
        Get
            Return mFileData
        End Get
        Set(ByVal value As Byte())
            mFileData = value
        End Set
    End Property


    ''medication dispensed

    Public Property MDDrugName() As String
        Get
            Return mMDDrugName
        End Get
        Set(ByVal value As String)
            mMDDrugName = value
        End Set
    End Property
    Public Property MDDrugForm() As String
        Get
            Return mMDDrugForm
        End Get
        Set(ByVal value As String)
            mMDDrugForm = value
        End Set
    End Property

    Public Property MDDosageForm() As String
        Get
            Return sMDDosageForm
        End Get
        Set(ByVal value As String)
            sMDDosageForm = value
        End Set
    End Property


    Public Property MDStrength() As String
        Get
            Return mMDStrength
        End Get
        Set(ByVal value As String)
            mMDStrength = value
        End Set
    End Property

    Public Property MDDosage() As String
        Get
            Return mMDDosage
        End Get
        Set(ByVal value As String)
            mMDDosage = value
        End Set
    End Property

    Public Property MDRoute() As String
        Get
            Return mMDRoute
        End Get
        Set(ByVal value As String)
            mMDRoute = value
        End Set
    End Property

    Public Property MDFrequency() As String
        Get
            Return mMDFrequency
        End Get
        Set(ByVal value As String)
            mMDFrequency = value
        End Set
    End Property

    Public Property MDDuration() As String
        Get
            Return mMDDuration
        End Get
        Set(ByVal value As String)
            mMDDuration = value
        End Set
    End Property

    Public Property MDQuantity() As String
        Get
            Return mMDQuantity
        End Get
        Set(ByVal value As String)
            mMDQuantity = value
        End Set
    End Property

    Public Property MDRefillQuantity() As String
        Get
            Return mMDRefillQuantity
        End Get
        Set(ByVal value As String)
            mMDRefillQuantity = value
        End Set
    End Property

    Public Property MDRefillQualifier() As String
        Get
            Return mMDRefillQualifier
        End Get
        Set(ByVal value As String)
            mMDRefillQualifier = value
        End Set
    End Property

    Public Property MDbMaySubstitutions() As String
        Get
            Return mMDbMaySubstitutions
        End Get
        Set(ByVal value As String)
            mMDbMaySubstitutions = value
        End Set
    End Property

    Public Property MDdtWrittendate() As String
        Get
            Return mMDdtWrittendate
        End Get
        Set(ByVal value As String)
            mMDdtWrittendate = value
        End Set
    End Property


    Public Property MDProductCode() As String
        Get
            Return mMDProductCode
        End Get
        Set(ByVal value As String)
            mMDProductCode = value
        End Set
    End Property

    Public Property MDPotencyUnitCode() As String
        Get
            Return mMDPotencyUnitCode
        End Get
        Set(ByVal value As String)
            mMDPotencyUnitCode = value
        End Set
    End Property

    Public Property MDdtlastdate() As String
        Get
            Return mMDdtlastdate
        End Get
        Set(ByVal value As String)
            mMDdtlastdate = value
        End Set
    End Property


    Public Property MDProductCodeQualifier() As String
        Get
            Return sMDProductCodeQualifier
        End Get
        Set(ByVal value As String)
            sMDProductCodeQualifier = value
        End Set
    End Property



    Public Property MDStrengthUnits() As String
        Get
            Return sMDStrengthUnits
        End Get
        Set(ByVal value As String)
            sMDStrengthUnits = value
        End Set
    End Property

    Public Property MDDrugDBCode() As String
        Get
            Return sMDDrugDBCode
        End Get
        Set(ByVal value As String)
            sMDDrugDBCode = value
        End Set
    End Property

    Public Property MDDrugDBCodeQualifier() As String
        Get
            Return sMDDrugDBCodeQualifier
        End Get
        Set(ByVal value As String)
            sMDDrugDBCodeQualifier = value
        End Set
    End Property

    Public Property MDNotes() As String
        Get
            Return sMDNotes
        End Get
        Set(ByVal value As String)
            sMDNotes = value
        End Set
    End Property

    Public Property MDDrugCoverageStatusCode As String
        Get
            Return sMDDrugCoverageStatusCode
        End Get
        Set(ByVal value As String)
            sMDDrugCoverageStatusCode = value
        End Set
    End Property
    '***********************************************************************


End Class
Public Class EDrugs
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
    Public ReadOnly Property Item(ByVal index As Integer) As EDrug
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), EDrug)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As EDrug)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                If (IsNothing(List) = False) Then
                    List.Clear()
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class


Public Class BenefitsCoordination

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    Private mPatientRelationShip As String = ""
    Private mPBMPayerParticipantId As String = ""
    Private mPBMPayerName As String = ""
    Private mHealthPlanName As String = ""
    Private mHealthPlanNumber As String = ""
    Private mPBM_PayerMemberId As String = ""
    Private mCardHolderId As String = ""
    Private mCardHolderName As String = ""
    Private mGroupId As String = ""
    Private mGroupName As String = ""
    Private mBINPCNNumber As String = ""
    Private mSocialSecurityNumber As String = ""
    Private mPatientAccountNumber As String = ""
    Private mHealthPlanBenefitCoverageName As String = ""
    Private mSubscriberFirstName As String = ""
    Private mSubscriberMiddleName As String = ""
    Private mSubscriberLastName As String = ""
    Private mSubscriberSuffix As String = ""
    Private mSubscriberGender As String = ""
    Private mSubscriberDOB As String = ""
    Private mSubscriberAddress1 As String = ""
    Private mSubscriberAddress2 As String = ""
    Private mSubscriberCity As String = ""
    Private mSubscriberState As String = ""
    Private mSubscriberZip As String = ""
    Private mISAControlNumber As String = ""
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




    Public Property ISAControlNumber() As String
        Get
            Return mISAControlNumber
        End Get
        Set(ByVal value As String)
            mISAControlNumber = value
        End Set
    End Property

    Public Property PBMPayerParticipantId() As String
        Get
            Return mPBMPayerParticipantId
        End Get
        Set(ByVal value As String)
            mPBMPayerParticipantId = value
        End Set
    End Property

    Public Property PatientRelationShip() As String
        Get
            Return mPatientRelationShip
        End Get
        Set(ByVal value As String)
            mPatientRelationShip = value
        End Set
    End Property
    Public Property PBM_PayerMemberId() As String
        Get
            Return mPBM_PayerMemberId
        End Get
        Set(ByVal value As String)
            mPBM_PayerMemberId = value
        End Set
    End Property


    Public Property HealthPlanNumber() As String
        Get
            Return mHealthPlanNumber
        End Get
        Set(ByVal value As String)
            mHealthPlanNumber = value
        End Set
    End Property


    Public Property HealthPlanName() As String
        Get
            Return mHealthPlanName
        End Get
        Set(ByVal value As String)
            mHealthPlanName = value
        End Set
    End Property

    Public Property CardHolderId() As String
        Get
            Return mCardHolderId
        End Get
        Set(ByVal value As String)
            mCardHolderId = value
        End Set
    End Property

    Public Property CardHolderName() As String
        Get
            Return mCardHolderName
        End Get
        Set(ByVal value As String)
            mCardHolderName = value
        End Set
    End Property

    Public Property GroupId() As String
        Get
            Return mGroupId
        End Get
        Set(ByVal value As String)
            mGroupId = value
        End Set
    End Property

    Public Property GroupName() As String
        Get
            Return mGroupName
        End Get
        Set(ByVal value As String)
            mGroupName = value
        End Set
    End Property

    Public Property BINPCNNumber() As String
        Get
            Return mBINPCNNumber
        End Get
        Set(ByVal value As String)
            mBINPCNNumber = value
        End Set
    End Property


    Public Property PBMPayerName() As String
        Get
            Return mPBMPayerName
        End Get
        Set(ByVal value As String)
            mPBMPayerName = value
        End Set
    End Property


    Public Property SocialSecurityNumber() As String
        Get
            Return mSocialSecurityNumber
        End Get
        Set(ByVal value As String)
            mSocialSecurityNumber = value
        End Set
    End Property

    Public Property PatientAccountNumber() As String
        Get
            Return mPatientAccountNumber
        End Get
        Set(ByVal value As String)
            mPatientAccountNumber = value
        End Set
    End Property

    Public Property HealthPlanBenefitCoverageName() As String
        Get
            Return mHealthPlanBenefitCoverageName
        End Get
        Set(ByVal value As String)
            mHealthPlanBenefitCoverageName = value
        End Set
    End Property

    Public Property SubscriberFirstName() As String
        Get
            Return mSubscriberFirstName
        End Get
        Set(ByVal value As String)
            mSubscriberFirstName = value
        End Set
    End Property

    Public Property SubscriberMiddleName() As String
        Get
            Return mSubscriberMiddleName
        End Get
        Set(ByVal value As String)
            mSubscriberMiddleName = value
        End Set
    End Property

    Public Property SubscriberLastName() As String
        Get
            Return mSubscriberLastName
        End Get
        Set(ByVal value As String)
            mSubscriberLastName = value
        End Set
    End Property

    Public Property SubscriberSuffix() As String
        Get
            Return mSubscriberSuffix
        End Get
        Set(ByVal value As String)
            mSubscriberSuffix = value
        End Set
    End Property

    Public Property SubscriberGender() As String
        Get
            Return mSubscriberGender
        End Get
        Set(ByVal value As String)
            mSubscriberGender = value
        End Set
    End Property

    Public Property SubscriberDOB() As String
        Get
            Return mSubscriberDOB
        End Get
        Set(ByVal value As String)
            mSubscriberDOB = value
        End Set
    End Property

    Public Property SubscriberAddress1() As String
        Get
            Return mSubscriberAddress1
        End Get
        Set(ByVal value As String)
            mSubscriberAddress1 = value
        End Set
    End Property

    Public Property SubscriberAddress2() As String
        Get
            Return mSubscriberAddress2
        End Get
        Set(ByVal value As String)
            mSubscriberAddress2 = value
        End Set
    End Property

    Public Property SubscriberCity() As String
        Get
            Return mSubscriberCity
        End Get
        Set(ByVal value As String)
            mSubscriberCity = value
        End Set
    End Property

    Public Property SubscriberState() As String
        Get
            Return mSubscriberState
        End Get
        Set(ByVal value As String)
            mSubscriberState = value
        End Set
    End Property

    Public Property SubscriberZip() As String
        Get
            Return mSubscriberZip
        End Get
        Set(ByVal value As String)
            mSubscriberZip = value
        End Set
    End Property

    '***********************************************************************


End Class
Public Class BenefitsCoordinations
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
    Public ReadOnly Property Item(ByVal index As Integer) As BenefitsCoordination
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), BenefitsCoordination)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As BenefitsCoordination)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                If (IsNothing(List) = False) Then
                    List.Clear()
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub



End Class


Public Class Prescriber
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mPrescriberName As PersonName
    Private mPrescriberAddress As AddressDetail
    Private mPrescriberPhone As PhoneNumber
    Private mPrescriberID As String = ""
    Private mPrescriberNPI As String = ""
    Private mProviderID As String = ""
    Private mPrescriberSSN As String = ""
    Private mPrescriberDEA As String = ""

    Private mPrescSpcltyType As String = ""
    Private mPrescSpcltyCode As String = ""
    Private mPrescriberEmail As String = ""
    Private mPrescAgntFName As String = ""
    Private mPrescAgntMName As String = ""
    Private mPrescAgntLName As String = ""
    Private mPrescAgntPrefix As String = ""
    Private mPrescAgntSuffix As String = ""

    Private mIsEPCSEnable As Boolean = False
    Public Property IsEPCSEnable() As Boolean
        Get
            Return mIsEPCSEnable
        End Get
        Set(ByVal Value As Boolean)
            mIsEPCSEnable = Value
        End Set
    End Property

    Public Property PrescAgntPrefix() As String
        Get
            Return mPrescAgntPrefix
        End Get
        Set(ByVal Value As String)
            mPrescAgntPrefix = Value
        End Set
    End Property
    Public Property PrescAgntSuffix() As String
        Get
            Return mPrescAgntSuffix
        End Get
        Set(ByVal Value As String)
            mPrescAgntSuffix = Value
        End Set
    End Property

    Public Property ProviderID() As String
        Get
            Return mProviderID
        End Get
        Set(ByVal value As String)
            mProviderID = value
        End Set
    End Property
    Public Property PrescriberID() As String
        Get
            Return mPrescriberID
        End Get
        Set(ByVal value As String)
            mPrescriberID = value
        End Set
    End Property
    Public Property PrescriberNPI() As String
        Get
            Return mPrescriberNPI
        End Get
        Set(ByVal value As String)
            mPrescriberNPI = value
        End Set
    End Property
    Public Property PrescriberSSN() As String
        Get
            Return mPrescriberSSN
        End Get
        Set(ByVal value As String)
            mPrescriberSSN = value
        End Set
    End Property
    Public Property PrescriberDEA() As String
        Get
            Return mPrescriberDEA
        End Get
        Set(ByVal value As String)
            mPrescriberDEA = value
        End Set
    End Property
    Public Property PrescriberName() As PersonName
        Get
            Return mPrescriberName
        End Get
        Set(ByVal value As PersonName)
            mPrescriberName = value
        End Set
    End Property
    Public Property PrescriberAddress() As AddressDetail
        Get
            Return mPrescriberAddress
        End Get
        Set(ByVal value As AddressDetail)
            mPrescriberAddress = value
        End Set
    End Property
    Public Property PrescriberPhone() As PhoneNumber
        Get
            If (IsNothing(mPrescriberPhone)) Then
                mPrescriberPhone = New PhoneNumber
            End If
            Return mPrescriberPhone
        End Get
        Set(ByVal value As PhoneNumber)
            If (IsNothing(mPrescriberPhone) = False) Then
                mPrescriberPhone.Dispose()
                mPrescriberPhone = Nothing
            End If
            mPrescriberPhone = value
        End Set
    End Property

    Public Property PrescSpcltyType() As String
        Get
            Return mPrescSpcltyType
        End Get
        Set(ByVal value As String)
            mPrescSpcltyType = value
        End Set
    End Property

    Public Property PrescSpcltyCode() As String
        Get
            Return mPrescSpcltyCode
        End Get
        Set(ByVal value As String)
            mPrescSpcltyCode = value
        End Set
    End Property

    Public Property PrescriberEmail() As String
        Get
            Return mPrescriberEmail
        End Get
        Set(ByVal value As String)
            mPrescriberEmail = value
        End Set
    End Property

    Public Property PrescAgntFName() As String
        Get
            Return mPrescAgntFName
        End Get
        Set(ByVal value As String)
            mPrescAgntFName = value
        End Set
    End Property

    Public Property PrescAgntMName() As String
        Get
            Return mPrescAgntMName
        End Get
        Set(ByVal value As String)
            mPrescAgntMName = value
        End Set
    End Property

    Public Property PrescAgntLName() As String
        Get
            Return mPrescAgntLName
        End Get
        Set(ByVal value As String)
            mPrescAgntLName = value
        End Set
    End Property
    

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Not IsNothing(mPrescriberPhone) Then
                    mPrescriberPhone.Dispose()
                    mPrescriberPhone = Nothing
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Public Sub New()
        MyBase.New()

        mPrescriberName = New PersonName
        mPrescriberAddress = New AddressDetail
        mPrescriberPhone = New PhoneNumber
    End Sub
End Class

''New class added in 7020 Incident #00006175, to accomodate Supervising provider info while doing eRx
Public Class Supervisor
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mSupervisorPrescriberName As PersonName
    Private mSupervisorPrescriberAddress As AddressDetail
    Private mSupervisorPrescriberPhone As PhoneNumber
    Private mSupervisorPrescriberID As String = ""
    Private mSupervisorProviderID As String = ""
    Private mSupervisorProviderNPI As String = ""
    Private mSupervisorProviderDEA As String = ""
    Private mSupervisorProviderSSN As String = ""

    Private mSupervisorPrescSpcltyType As String = ""
    Private mSupervisorPrescSpcltyCode As String = ""
    Private mSupervisorPrescriberEmail As String = ""
    Private mSupervisorPrescAgntFName As String = ""
    Private mSupervisorPrescAgntMName As String = ""
    Private mSupervisorPrescAgntLName As String = ""
    Private mSupervisorPrescAgntPrefix As String = ""
    Private mSupervisorPrescAgntSuffix As String = ""

    Public Property SupervisorPrescAgntPrefix() As String
        Get
            Return mSupervisorPrescAgntPrefix
        End Get
        Set(ByVal Value As String)
            mSupervisorPrescAgntPrefix = Value
        End Set
    End Property
    Public Property SupervisorPrescAgntSuffix() As String
        Get
            Return mSupervisorPrescAgntSuffix
        End Get
        Set(ByVal Value As String)
            mSupervisorPrescAgntSuffix = Value
        End Set
    End Property

    Public Property SupervisorProviderID() As String
        Get
            Return mSupervisorProviderID
        End Get
        Set(ByVal value As String)
            mSupervisorProviderID = value
        End Set
    End Property
    Public Property SupervisorProviderNPI() As String
        Get
            Return mSupervisorProviderNPI
        End Get
        Set(ByVal value As String)
            mSupervisorProviderNPI = value
        End Set
    End Property
    Public Property SupervisorProviderDEA() As String
        Get
            Return mSupervisorProviderDEA
        End Get
        Set(ByVal value As String)
            mSupervisorProviderDEA = value
        End Set
    End Property
    Public Property SupervisorProviderSSN() As String
        Get
            Return mSupervisorProviderSSN
        End Get
        Set(ByVal value As String)
            mSupervisorProviderSSN = value
        End Set
    End Property
    Public Property SupervisorPrescriberID() As String
        Get
            Return mSupervisorPrescriberID
        End Get
        Set(ByVal value As String)
            mSupervisorPrescriberID = value
        End Set
    End Property
    Public Property SupervisorPrescriberName() As PersonName
        Get
            Return mSupervisorPrescriberName
        End Get
        Set(ByVal value As PersonName)
            mSupervisorPrescriberName = value
        End Set
    End Property
    Public Property SupervisorPrescriberAddress() As AddressDetail
        Get
            Return mSupervisorPrescriberAddress
        End Get
        Set(ByVal value As AddressDetail)
            mSupervisorPrescriberAddress = value
        End Set
    End Property
    Public Property SupervisorPrescriberPhone() As PhoneNumber
        Get
            If (IsNothing(mSupervisorPrescriberPhone)) Then
                mSupervisorPrescriberPhone = New PhoneNumber
            End If
            Return mSupervisorPrescriberPhone
        End Get
        Set(ByVal value As PhoneNumber)
            If (IsNothing(mSupervisorPrescriberPhone) = False) Then
                mSupervisorPrescriberPhone.Dispose()
                mSupervisorPrescriberPhone = Nothing
            End If
            mSupervisorPrescriberPhone = value
        End Set
    End Property

    Public Property SupervisorPrescSpcltyType() As String
        Get
            Return mSupervisorPrescSpcltyType
        End Get
        Set(ByVal value As String)
            mSupervisorPrescSpcltyType = value
        End Set
    End Property

    Public Property SupervisorPrescSpcltyCode() As String
        Get
            Return mSupervisorPrescSpcltyCode
        End Get
        Set(ByVal value As String)
            mSupervisorPrescSpcltyCode = value
        End Set
    End Property

    Public Property SupervisorPrescriberEmail() As String
        Get
            Return mSupervisorPrescriberEmail
        End Get
        Set(ByVal value As String)
            mSupervisorPrescriberEmail = value
        End Set
    End Property

    Public Property SupervisorPrescAgntFName() As String
        Get
            Return mSupervisorPrescAgntFName
        End Get
        Set(ByVal value As String)
            mSupervisorPrescAgntFName = value
        End Set
    End Property

    Public Property SupervisorPrescAgntMName() As String
        Get
            Return mSupervisorPrescAgntMName
        End Get
        Set(ByVal value As String)
            mSupervisorPrescAgntMName = value
        End Set
    End Property

    Public Property SupervisorPrescAgntLName() As String
        Get
            Return mSupervisorPrescAgntLName
        End Get
        Set(ByVal value As String)
            mSupervisorPrescAgntLName = value
        End Set
    End Property
    Public Property SupervisorSupervisingProviderName() As String
        Get
            Return mSupervisorPrescAgntLName
        End Get
        Set(ByVal value As String)
            mSupervisorPrescAgntLName = value
        End Set
    End Property

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Not IsNothing(mSupervisorPrescriberPhone) Then
                    mSupervisorPrescriberPhone.Dispose()
                    mSupervisorPrescriberPhone = Nothing
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Public Sub New()
        MyBase.New()

        mSupervisorPrescriberName = New PersonName
        mSupervisorPrescriberAddress = New AddressDetail
        mSupervisorPrescriberPhone = New PhoneNumber
    End Sub
End Class

Public Class Pharmacy
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mPharmacyAddress As AddressDetail
    Private mPharmacyname As String
    Private mPharmacyPhone As PhoneNumber
    Private mPharmacyID As String
    Private mContactID As String

    Private mPharmAgntFName As String = ""
    Private mPharmAgntMName As String = ""
    Private mPharmAgntLName As String = ""
    Private mPharmacistFName As String = ""
    Private mPharmacistMName As String = ""
    Private mPharmacistLName As String = ""
    Private mPharmacyEmail As String = ""
    Private mPharmacistPrefix As String = ""
    Private mPharmacistSuffix As String = ""
    Private mPharmacistAgentPrefix As String = ""
    Private mPharmacistAgentSuffix As String = ""
    Private mPhNCPDPId As String = "" 'added for case GLO2011-0013803
    Private mPharmacyNPI As String = ""
    Private mPharmacySpeciality As String = ""
    Private mPhServiceLevel As Int64 = 0 ''added in 7031 to check pharamcy service level and do the validation against it.

    Public Property PhServiceLevel() As Int64
        Get
            Return mPhServiceLevel
        End Get
        Set(ByVal Value As Int64)
            mPhServiceLevel = Value
        End Set
    End Property

    Public Property PharmacistAgentPrefix() As String
        Get
            Return mPharmacistAgentPrefix
        End Get
        Set(ByVal Value As String)
            mPharmacistAgentPrefix = Value
        End Set
    End Property
    Public Property PharmacistAgentSuffix() As String
        Get
            Return mPharmacistAgentSuffix
        End Get
        Set(ByVal Value As String)
            mPharmacistAgentSuffix = Value
        End Set
    End Property
    Public Property PharmacistPrefix() As String
        Get
            Return mPharmacistPrefix
        End Get
        Set(ByVal Value As String)
            mPharmacistPrefix = Value
        End Set
    End Property


    Public Property PharmacistSuffix() As String
        Get
            Return mPharmacistSuffix
        End Get
        Set(ByVal Value As String)
            mPharmacistSuffix = Value
        End Set
    End Property


    Public Property ContactID() As String
        Get
            Return mContactID
        End Get
        Set(ByVal value As String)
            mContactID = value
        End Set
    End Property
    Public Property PharmacyID() As String
        Get
            Return mPharmacyID
        End Get
        Set(ByVal value As String)
            mPharmacyID = value
        End Set
    End Property
    'added new property to fix case GLO2011-0013803
    Public Property PhNCPDPId() As String
        Get
            Return mPhNCPDPId
        End Get
        Set(ByVal value As String)
            mPhNCPDPId = value
        End Set
    End Property
    Public Property PharmacyAddress() As AddressDetail
        Get
            Return mPharmacyAddress
        End Get
        Set(ByVal value As AddressDetail)
            mPharmacyAddress = value
        End Set
    End Property
    Public Property PharmacyPhone() As PhoneNumber
        Get
            If (IsNothing(mPharmacyPhone)) Then
                mPharmacyPhone = New PhoneNumber
            End If
            Return mPharmacyPhone
        End Get
        Set(ByVal value As PhoneNumber)
            If (IsNothing(mPharmacyPhone) = False) Then
                mPharmacyPhone.Dispose()
                mPharmacyPhone = Nothing
            End If
            mPharmacyPhone = value
        End Set
    End Property
    Public Property PharmacyName() As String
        Get
            Return mPharmacyname
        End Get
        Set(ByVal value As String)
            mPharmacyname = value
        End Set
    End Property


    Public Property PharmAgntFName() As String
        Get
            Return mPharmAgntFName
        End Get
        Set(ByVal value As String)
            mPharmAgntFName = value
        End Set
    End Property


    Public Property PharmAgntMName() As String
        Get
            Return mPharmAgntMName
        End Get
        Set(ByVal value As String)
            mPharmAgntMName = value
        End Set
    End Property


    Public Property PharmAgntLName() As String
        Get
            Return mPharmAgntLName
        End Get
        Set(ByVal value As String)
            mPharmAgntLName = value
        End Set
    End Property


    Public Property PharmacistFName() As String
        Get
            Return mPharmacistFName
        End Get
        Set(ByVal value As String)
            mPharmacistFName = value
        End Set
    End Property


    Public Property PharmacistMName() As String
        Get
            Return mPharmacistMName
        End Get
        Set(ByVal value As String)
            mPharmacistMName = value
        End Set
    End Property


    Public Property PharmacistLName() As String
        Get
            Return mPharmacistLName
        End Get
        Set(ByVal value As String)
            mPharmacistLName = value
        End Set
    End Property


    Public Property PharmacyEmail() As String
        Get
            Return mPharmacyEmail
        End Get
        Set(ByVal value As String)
            mPharmacyEmail = value
        End Set
    End Property
    Public Property PharmacyNPI() As String
        Get
            Return mPharmacyNPI
        End Get
        Set(ByVal value As String)
            mPharmacyNPI = value
        End Set
    End Property
    Public Property PharmacySpeciality() As String
        Get
            Return mPharmacySpeciality
        End Get
        Set(ByVal value As String)
            mPharmacySpeciality = value
        End Set
    End Property




    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Not IsNothing(mPharmacyPhone) Then
                    mPharmacyPhone.Dispose()
                    mPharmacyPhone = Nothing
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

    Public Sub New()
        MyBase.New()
        mPharmacyAddress = New AddressDetail
        mPharmacyPhone = New PhoneNumber
    End Sub
End Class
Public Class PersonName

    Private _ID As Long
    Private _FirstName As String = ""
    Private _MiddleName As String = ""
    Private _LastName As String = ""
    Private _Code As String = ""
    Private _Prefix As String = ""
    Private _Suffix As String = ""


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


    Public Property Suffix() As String
        Get
            Return _Suffix
        End Get
        Set(ByVal Value As String)
            _Suffix = Value
        End Set
    End Property

    

End Class
Public Class AddressDetail
    Private _Address1 As String = ""
    Private _Address2 As String = ""
    Private _City As String = ""
    Private _State As String = ""
    Private _Zip As String = ""
    Private _County As String = ""
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
    Public Sub New()
        MyBase.New()
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
Public Class ContactAddress
    Private _Street As String = ""
    Private _City As String = ""
    Private _State As String = ""
    Private _Zip As String = ""
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
    Public Sub New()
        MyBase.New()
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
Public Class PhoneNumber
    Implements IDisposable

    Private _Phone As String = ""
    Private _Mobile As String = ""
    Private _Email As String = ""
    Private _Fax As String = ""
    Private _Pager As String = ""
    Private _Qualifier As String = ""
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
    Public Property Qualifier() As String
        Get
            Return _Qualifier
        End Get
        Set(ByVal Value As String)
            _Qualifier = Value
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
Public Class Patient
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mPatientName As PersonName
    Private mDateofBBirth As String
    Private mGender As String = ""
    Private mPatientAddress As AddressDetail
    Private mPatientPhone As PhoneNumber
    Private mPatientWorkPhone As PhoneNumber
    Private mSSN As String = ""
    Private mIdentifier As String = ""
    Private mIdentifier1 As String = ""
    Private mIdentifierType As String = ""
    Private mIdentifierType1 As String = ""
    Private mPatientRelationship As String = ""

    Public Property PatientRelationship() As String
        Get
            Return mPatientRelationship
        End Get
        Set(ByVal value As String)
            mPatientRelationship = value
        End Set
    End Property
    Public Property Identifier() As String
        Get
            Return mIdentifier
        End Get
        Set(ByVal value As String)
            mIdentifier = value
        End Set
    End Property
    Public Property Identifier1() As String
        Get
            Return mIdentifier
        End Get
        Set(ByVal value As String)
            mIdentifier = value
        End Set
    End Property
    Public Property IdentifierType() As String
        Get
            Return mIdentifierType
        End Get
        Set(ByVal value As String)
            mIdentifierType = value
        End Set
    End Property
    Public Property IdentifierType1() As String
        Get
            Return mIdentifierType1
        End Get
        Set(ByVal value As String)
            mIdentifierType1 = value
        End Set
    End Property
    Public Property PatientName() As PersonName
        Get
            Return mPatientName
        End Get
        Set(ByVal value As PersonName)
            mPatientName = value
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
    Public Property PatientAddress() As AddressDetail
        Get
            Return mPatientAddress
        End Get
        Set(ByVal value As AddressDetail)
            mPatientAddress = value
        End Set
    End Property
    Public Property PatientPhone() As PhoneNumber
        Get
            If (IsNothing(mPatientPhone)) Then
                mPatientPhone = New PhoneNumber
            End If
            Return mPatientPhone
        End Get
        Set(ByVal value As PhoneNumber)
            If (IsNothing(mPatientPhone) = False) Then
                mPatientPhone.Dispose()
                mPatientPhone = Nothing
            End If
            mPatientPhone = value
        End Set
    End Property
    Public Property PatientWorkPhone() As PhoneNumber
        Get
            If IsNothing(mPatientWorkPhone) Then
                mPatientWorkPhone = New PhoneNumber
            End If
            Return mPatientWorkPhone
        End Get
        Set(ByVal value As PhoneNumber)
            If (IsNothing(mPatientWorkPhone) = False) Then
                mPatientWorkPhone.Dispose()
                mPatientWorkPhone = Nothing
            End If
            mPatientWorkPhone = value
        End Set
    End Property
    Public Property SSN() As String
        Get
            Return mSSN
        End Get
        Set(ByVal value As String)
            mSSN = value
        End Set
    End Property
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                If Not IsNothing(mPatientPhone) Then
                    mPatientPhone.Dispose()
                    mPatientPhone = Nothing
                End If
                If Not IsNothing(mPatientWorkPhone) Then
                    mPatientWorkPhone.Dispose()
                    mPatientWorkPhone = Nothing
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

    Public Sub New()
        MyBase.New()
        mPatientName = New PersonName
        mPatientAddress = New AddressDetail
        mPatientPhone = New PhoneNumber
        mPatientPhone = New PhoneNumber
    End Sub
    
End Class
Public Class EPrescriptions
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
    Public ReadOnly Property Item(ByVal index As Integer) As EPrescription
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), EPrescription)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As EPrescription)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If (IsNothing(List) = False) Then
                    List.Clear()
                End If

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
Public Enum MessageType
    eStatus
    eError
    eVerify
    eNewRx
    eRefillRequest
    eRefillResponse
    eCancelRx
    eNewRxEPCS
    eRxChangeRequest
    eRxChangeResponse
End Enum
Public Class SureScriptMessage
    Private mMessageId As String = ""
    Private mMessageName As String = ""
    Private mRelatesToMessageId As String = ""
    Private mMessageFrom As String = ""
    Private mMessageTo As String = ""
    Private mDateTimeStamp As String = ""
    Private mDateReceived As DateTime
    Private mMessageType As MessageType
    Private mTransactionID As String
    Public Property TransactionID() As String
        Get
            Return mTransactionID
        End Get
        Set(ByVal value As String)
            mTransactionID = value
        End Set
    End Property
    Public Property SMessageType() As MessageType
        Get
            Return mMessageType
        End Get
        Set(ByVal value As MessageType)
            mMessageType = value
        End Set
    End Property
    Public Property MessageID() As String
        Get
            Return mMessageId
        End Get
        Set(ByVal value As String)
            mMessageId = value
        End Set
    End Property
    Public Property MessageName() As String
        Get
            Return mMessageName
        End Get
        Set(ByVal value As String)
            mMessageName = value
        End Set
    End Property
    Public Property RelatesToMessageId() As String
        Get
            Return mRelatesToMessageId
        End Get
        Set(ByVal value As String)
            mRelatesToMessageId = value
        End Set
    End Property
    Public Property MessageFrom() As String
        Get
            Return mMessageFrom
        End Get
        Set(ByVal value As String)
            mMessageFrom = value
        End Set
    End Property
    Public Property MessageTo() As String
        Get
            Return mMessageTo
        End Get
        Set(ByVal value As String)
            mMessageTo = value
        End Set
    End Property
    Public Property DateTimeStamp() As String
        Get
            Return mDateTimeStamp
        End Get
        Set(ByVal value As String)
            mDateTimeStamp = value
        End Set
    End Property
    Public Property DateReceived() As DateTime
        Get
            Return mDateReceived
        End Get
        Set(ByVal value As DateTime)
            mDateReceived = value
        End Set
    End Property
End Class
Public Class StatusMessage
    Inherits SureScriptMessage
    Implements IDisposable
    Private mstatuscode As String
    Private mDescription As String
    Public Property StatusCode() As String
        Get
            Return mstatuscode
        End Get
        Set(ByVal value As String)
            mstatuscode = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal value As String)
            mDescription = value
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
Public Class SureScriptErrorMessage
    Inherits SureScriptMessage
    Implements IDisposable
    Private mErrorcode As String
    Private mDescriptionCode As String
    Private mDescription As String
    Private sRelatesToMessageName As String
    Private oPrescription As EPrescription
    Public Property PrescriptionObject() As EPrescription
        Get
            If IsNothing(oPrescription) Then
                oPrescription = New EPrescription
            End If
            Return oPrescription
        End Get
        Set(ByVal value As EPrescription)
            If (IsNothing(oPrescription) = False) Then
                oPrescription.Dispose()
                oPrescription = Nothing
            End If
            oPrescription = value
        End Set
    End Property
    Public Property RelatesToMessageName() As String
        Get
            Return sRelatesToMessageName
        End Get
        Set(ByVal value As String)
            sRelatesToMessageName = value
        End Set
    End Property
    Public Property ErrorCode() As String
        Get
            Return mErrorcode
        End Get
        Set(ByVal value As String)
            mErrorcode = value
        End Set
    End Property
    Public Property DescriptionCode() As String
        Get
            Return mDescriptionCode
        End Get
        Set(ByVal value As String)
            mDescriptionCode = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal value As String)
            mDescription = value
        End Set
    End Property
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                If Not IsNothing(oPrescription) Then
                    oPrescription.Dispose()
                    oPrescription = Nothing
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
Public Class SureScriptResponseMessage
    Inherits SureScriptMessage
    Implements IDisposable
    Private oPrescription As EPrescription
    Private mDenialcode As String
    Private mDenialReason As String
    Private blnIsApproved As Boolean
    Private mNotes As String
    Private sStatusMessageType As String
    Private nProviderId As Int64
    Private nRefReqPatientId As Int64
    Public Property PrescriptionObject() As EPrescription
        Get
            If IsNothing(oPrescription) Then
                oPrescription = New EPrescription
            End If
            Return oPrescription
        End Get
        Set(ByVal value As EPrescription)
            If (IsNothing(oPrescription) = False) Then
                oPrescription.Dispose()
                oPrescription = Nothing
            End If
            oPrescription = value
        End Set
    End Property
    Public Property Notes() As String
        Get
            Return mNotes
        End Get
        Set(ByVal value As String)
            mNotes = value
        End Set
    End Property
    Public Property StatusMessageType() As String
        Get
            Return sStatusMessageType
        End Get
        Set(ByVal value As String)
            sStatusMessageType = value
        End Set
    End Property
    Public Property ProviderId() As Int64
        Get
            Return nProviderId
        End Get
        Set(ByVal value As Int64)
            nProviderId = value
        End Set
    End Property
    Public Property RefReqPatientId() As Int64
        Get
            Return nRefReqPatientId
        End Get
        Set(ByVal value As Int64)
            nRefReqPatientId = value
        End Set
    End Property
    Public Property ApprovalStatus() As Boolean
        Get
            Return blnIsApproved
        End Get
        Set(ByVal value As Boolean)
            blnIsApproved = value
        End Set
    End Property
    Public Property Denialcode() As String
        Get
            Return mDenialcode
        End Get
        Set(ByVal value As String)
            mDenialcode = value
        End Set
    End Property
    Public Property DenialReason() As String
        Get
            Return mDenialReason
        End Get
        Set(ByVal value As String)
            mDenialReason = value
        End Set
    End Property

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                If Not IsNothing(oPrescription) Then
                    oPrescription.Dispose()
                    oPrescription = Nothing
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
Public Class Pharmacies
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
    Public ReadOnly Property Item(ByVal index As Integer) As Pharmacy
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Pharmacy)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Pharmacy)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                If (IsNothing(List) = False) Then
                    List.Clear()
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class

