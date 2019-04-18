Public Class EPrescription

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mDrugsCol As EDrugs
    Private mPrescriber As Prescriber
    Private mRxReferenceNumber As String
    Private mPharmacy As Pharmacy
    Private mPatient As Patient
    Private mPrescriptiondate As String
    Private mRxTransactionID As String
    Private mVisitId As Int64
    Private mPatientID As Int64
    Private mProviderID As Int64 'Provider ID

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
    Public Property RxPrescriber() As Prescriber
        Get
            Return mPrescriber
        End Get
        Set(ByVal value As Prescriber)
            mPrescriber = value
        End Set
    End Property
    Public Sub New()
        MyBase.New()
        mDrugsCol = New EDrugs
        mPharmacy = New Pharmacy
        mPrescriber = New Prescriber
        mPatient = New Patient
    End Sub
End Class
Public Class EDrug
    Inherits SureScriptMessage
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mDrugName As String
    Private mDrugForm As String
    Private mDosage As String
    Private mDrugRoute As String
    Private mDrugFrequency As String
    Private mDrugDuration As String
    Private mDrugStrength As String
    Private mDrugStrengthUnits As String
    Private mDrugQuantity As String
    Private mDrugQuantityQualifier As String
    Private mRefillQuantity As String
    Private mDirections As String
    Private mMaySubstitute As Boolean
    Private mWrittendate As String
    Private mRefillsQualifier As String
    Private mPrescriptionID As String
    Private mRxReferenceNumber As String
    Private mDrugID As String
    Private mStatus As String
    Private mNotes As String
    Private mLastFillDate As String
    Private mProductCode As String
    Private mProductCodeQualifier As String
    Private mDosageForm As String
    Private mDrugDBCode As String
    Private mDrugDBCodeQualifier As String
    Private mClinicalInformationQualifier1 As String
    Private mPrimaryQualifier1 As String
    Private mPrimaryValue1 As String
    Private mSecondaryQualifier1 As String
    Private mSecondaryValue1 As String
    Private mClinicalInformationQualifier2 As String
    Private mPrimaryQualifier2 As String
    Private mPrimaryValue2 As String
    Private mSecondaryQualifier2 As String
    Private mSecondaryValue2 As String
    Private mPriorAuthorizationQualifier As String
    Private mPriorAuthorizationValue As String
    'Private mDaysSupply As String

    'Public Property DaysSupply() As String
    '    Get
    '        Return mDaysSupply
    '    End Get
    '    Set(ByVal value As String)
    '        mDaysSupply = value
    '    End Set
    'End Property
    Public Property ProductCode() As String
        Get
            Return mProductCode
        End Get
        Set(ByVal value As String)
            mProductCode = value
        End Set
    End Property
    Public Property ProductCodeQualifier() As String
        Get
            Return mProductCodeQualifier
        End Get
        Set(ByVal value As String)
            mProductCodeQualifier = value
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
    Public Property ClinicalInformationQualifier1() As String
        Get
            Return mClinicalInformationQualifier1
        End Get
        Set(ByVal value As String)
            mClinicalInformationQualifier1 = value
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
    Public Property SecondaryQualifier1() As String
        Get
            Return mSecondaryQualifier1
        End Get
        Set(ByVal value As String)
            mSecondaryQualifier1 = value
        End Set
    End Property
    Public Property SecondaryValue1() As String
        Get
            Return mSecondaryValue1
        End Get
        Set(ByVal value As String)
            mSecondaryValue1 = value
        End Set
    End Property

    Public Property ClinicalInformationQualifier2() As String
        Get
            Return mClinicalInformationQualifier2
        End Get
        Set(ByVal value As String)
            mClinicalInformationQualifier2 = value
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
    Public Property SecondaryQualifier2() As String
        Get
            Return mSecondaryQualifier2
        End Get
        Set(ByVal value As String)
            mSecondaryQualifier2 = value
        End Set
    End Property
    Public Property SecondaryValue2() As String
        Get
            Return mSecondaryValue2
        End Get
        Set(ByVal value As String)
            mSecondaryValue2 = value
        End Set
    End Property
    Public Property PriorAuthorizationQualifier() As String
        Get
            Return mPriorAuthorizationQualifier
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
    Public Property Notes() As String
        Get
            Return mNotes
        End Get
        Set(ByVal value As String)
            mNotes = value
        End Set
    End Property
    Public Property LastFillDate() As String
        Get
            Return mLastFillDate
        End Get
        Set(ByVal value As String)
            mLastFillDate = value
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
    Public Property DrugStrengthUnits() As String
        Get
            Return mDrugStrengthUnits
        End Get
        Set(ByVal value As String)
            mDrugStrengthUnits = value
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
    Public Property DrugQuantityQualifier() As String
        Get
            Return mDrugQuantityQualifier
        End Get
        Set(ByVal value As String)
            mDrugQuantityQualifier = value
        End Set
    End Property
    Public Property Directions() As String
        Get
            Return mDirections
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


    'MD Drug
    Public Property MDDrugName() As String
    Public Property MDDrugQuantity() As String
    Public Property MDDrugQualifier() As String
    Public Property MDRefillQuantity() As String
    Public Property MDRefillsQualifier() As String
    Public Property MDdtWrittenDate() As String

    Public Property MDDrugDuration() As String
    Public Property MDDrugDirections() As String
    Public Property MDbIsSubstituitons() As Boolean
    Public Property MDNotes() As String
    Public Property MDdtLastFillDate() As String

    Public Property MDProductCode() As String
    Public Property MDProductCodeQualifier() As String
    Public Property MDDosageForm() As String
    Public Property MDDrugStrength() As String
    Public Property MDDrugStrengthUnits() As String

    Public Property MDCodeListQualifier() As String
    Public Property MDUnitSourceCode() As String
    Public Property MDPotencyUnitCode() As String
    Public Property MDDrugDBCode() As String
    Public Property MDDrugDBCodeQualifier() As String
    Public Property DrugCoverageStatusCode() As String
    Public Property MDDrugCoverageStatusCode() As String
    Public Property PharmacySpecialty() As String
    Public Property PatientRelationship() As String
    Public Property PrescriberSSN() As String
    Public Property FileData() As Byte()
    '-----


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
    Private mPrescriberID As String
    Private mPrescriberAgentName As PersonName
    Private mPrescriberSpecialtyQualifier As String
    Private mPrescriberSpecialtyCode As String
    Private mPrescriberClinic As String
    Public Property PrescriberClinic() As String
        Get
            Return mPrescriberClinic
        End Get
        Set(ByVal value As String)
            mPrescriberClinic = value
        End Set
    End Property
    Public Property PrescriberSpecialtyQualifier() As String
        Get
            Return mPrescriberSpecialtyQualifier
        End Get
        Set(ByVal value As String)
            mPrescriberSpecialtyQualifier = value
        End Set
    End Property
    Public Property PrescriberSpecialtyCode() As String
        Get
            Return mPrescriberSpecialtyCode
        End Get
        Set(ByVal value As String)
            mPrescriberSpecialtyCode = value
        End Set
    End Property

    Public Property PrescriberAgentName() As PersonName
        Get
            Return mPrescriberAgentName
        End Get
        Set(ByVal value As PersonName)
            mPrescriberAgentName = value
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
    Public Property PrescriberDEA() As String

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

        mPrescriberName = New PersonName
        mPrescriberAddress = New AddressDetail
        mPrescriberAgentName = New PersonName
    End Sub
End Class
Public Class Pharmacy
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mPharmacyAddress As AddressDetail
    Private mPharmacyname As String
    Private mPharmacyID As String
    Private mPharmacistName As PersonName
    Private mPharmacistAgentName As PersonName
    Private mActiveStartTime As DateTime
    Private mActiveEndTime As DateTime
    Private mServicelevel As String
    Private mStatus As String

    Public Property PharmacistName() As PersonName
        Get
            Return mPharmacistName
        End Get
        Set(ByVal value As PersonName)
            mPharmacistName = value
        End Set
    End Property

    Public Property PharmacistAgentName() As PersonName
        Get
            Return mPharmacistAgentName
        End Get
        Set(ByVal value As PersonName)
            mPharmacistAgentName = value
        End Set
    End Property
    Public Property CodeListQualifier() As String
    Public Property UnitSourceCode() As String
    Public Property PotencyUnitCode() As String

    Public Property PharmacyID() As String
        Get
            Return mPharmacyID
        End Get
        Set(ByVal value As String)
            mPharmacyID = value
        End Set
    End Property
    Public Property PharmacyNPI() As String

    Public Property PharmacyAddress() As AddressDetail
        Get
            Return mPharmacyAddress
        End Get
        Set(ByVal value As AddressDetail)
            mPharmacyAddress = value
        End Set
    End Property

    Public Property Pharmacyname() As String
        Get
            Return mPharmacyname
        End Get
        Set(ByVal value As String)
            mPharmacyname = value
        End Set
    End Property
    Public Property PharmacyStatus() As String
        Get
            Return mStatus
        End Get
        Set(ByVal value As String)
            mStatus = value
        End Set
    End Property
    Public Property Servicelevel() As String
        Get
            Return mServicelevel
        End Get
        Set(ByVal value As String)
            mServicelevel = value
        End Set
    End Property
    Public Property ActiveStartTime() As DateTime
        Get
            Return mActiveStartTime
        End Get
        Set(ByVal value As DateTime)
            mActiveStartTime = value
        End Set
    End Property
    Public Property ActiveEndTime() As DateTime
        Get
            Return mActiveEndTime
        End Get
        Set(ByVal value As DateTime)
            mActiveEndTime = value
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

    Public Sub New()
        MyBase.New()
        mPharmacyAddress = New AddressDetail
        mPharmacistName = New PersonName
        mPharmacistAgentName = New PersonName
    End Sub
End Class
Public Class PersonName

    Private _ID As String
    Private _FirstName As String
    Private _MiddleName As String
    Private _LastName As String
    Private _Code As String
    Private _Suffix As String
    Private _Prefix As String
    Private _IDType As String
    Private _CodeType As String
    Public Property IDType() As String
        Get
            Return _IdType
        End Get
        Set(ByVal Value As String)
            _IdType = Value
        End Set
    End Property
    Public Property CodeType() As String
        Get
            Return _CodeType
        End Get
        Set(ByVal Value As String)
            _CodeType = Value
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
    Public Property Prefix() As String
        Get
            Return _Prefix
        End Get
        Set(ByVal Value As String)
            _Prefix = Value
        End Set
    End Property
    Public Property ID() As String
        Get
            Return _ID
        End Get
        Set(ByVal Value As String)
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
End Class
Public Class AddressDetail
    Private _Address1 As String
    Private _Address2 As String
    Private _City As String
    Private _State As String
    Private _Zip As String
    Private _County As String
    Private _Phone As String
    Private _PhQualifier As String
    Private _Email As String
    Private _Fax As String
    Private _WorkPhone As String
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

    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal Value As String)
            _Phone = Value
        End Set
    End Property

    Public Property PhQualifier() As String
        Get
            Return _PhQualifier
        End Get
        Set(ByVal Value As String)
            _PhQualifier = Value
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
    Private _Street As String
    Private _City As String
    Private _State As String
    Private _Zip As String
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

    Private _Phone As String
    Private _Mobile As String
    Private _Email As String
    Private _Fax As String
    Private _Pager As String

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
End Class
Public Class Patient
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mPatientName As PersonName
    Private mDateofBBirth As String
    Private mGender As String
    Private mPatientAddress As AddressDetail
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
        MyBase.New()
        mPatientName = New PersonName
        mPatientAddress = New AddressDetail
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
End Enum
Public Class SureScriptMessage
    Private mMessageId As String
    Private mMessageName As String
    Private mRelatesToMessageId As String
    Private mMessageFrom As String
    Private mMessageTo As String
    Private mDateTimeStamp As String
    Private mDateReceived As String
    Private mMessageType As MessageType
    Private mTransactionID As String
    Private sReferenceNumber As String
    Private bIsAlertCheck As Boolean
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

    Public Property ReferenceNumber() As String
        Get
            Return sReferenceNumber
        End Get
        Set(ByVal value As String)
            sReferenceNumber = value
        End Set
    End Property
    Public Property IsAlertCheck() As Boolean
        Get
            Return bIsAlertCheck
        End Get
        Set(ByVal value As Boolean)
            bIsAlertCheck = value
        End Set
    End Property

    Public Property SenderSoftwareVersion() As String
    Public Property SenderSoftwareDeveloper() As String
    Public Property SenderSoftwareProduct() As String

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
    Public Property PrescriptionObject() As EPrescription
        Get
            If IsNothing(oPrescription) Then
                oPrescription = New EPrescription
            End If
            Return oPrescription
        End Get
        Set(ByVal value As EPrescription)
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
