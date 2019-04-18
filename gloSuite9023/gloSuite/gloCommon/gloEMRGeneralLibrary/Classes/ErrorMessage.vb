Imports gloSureScript
Imports gloEMRGeneralLibrary.gloGeneral


Public Class ErrorMessage
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    '''''variable for creating property procedures
    Private mMessageName As String
    Private mRelatesToMsgId As String
    Private mMessageId As String
    Private mDateSent As String = ""


    '''''variable for Patient Information property procedures
    Private mPatientName As String
    Private mPatientDOB As String
    Private mPatientGender As String
    Private mPatientAddress As String
    Private mPatientAddress2 As String
    Private mPatientPhone As String
    Private mPatientCity As String
    Private mPatientState As String
    Private mPatientZip As String

    '''''variable for Pharmacy Information property procedures
    Private mPharmacyID As String
    Private mPharmacyName As String
    Private mPharmacyAddress As String
    Private mPharmacyAddress2 As String
    Private mPharmacyPhone As String
    Private mPharmacyCity As String
    Private mPharmacyState As String
    Private mPharmacyZip As String
    Private mPharmacyFax As String
    Private mPharmacyNPI As String

    '''''''variable for Provider (means Prescriber)Information property procedures
    Private mProviderID As String
    Private mProviderName As String
    Private mProviderAddress As String
    Private mProviderAddress2 As String
    Private mProviderPhone As String
    Private mProviderCity As String
    Private mProviderState As String
    Private mProviderZip As String
    Private mProviderFax As String
    Private mProviderNPI As String

    Private mMessageStatus As String = ""   'if refillresponse then whether message is "Approved"/"Denied"....
    Private mSourceMessageName As String = "" 'it can be refill response/newrx ,message for which error generated
    Private mRxTransactionID As String = "0" 'is the PrescriptionID of NewRx/RefillRes (Approved/Approvedwithchanges)
    Private mSourceRelatesToMessageID As String = ""  'if refill response messageid of refreq that generated refill response
    Private mSourceRxReferenceNumber As String = "" 'Will come in to picture for error for refillresponse
    'shall be RxReferencenumber of refreq for which this refillresponse  has been generated


    Private mDrugName As String = ""
    Private mDrugForm As String = ""
    Private mDosage As String = ""
    Private mDrugRoute As String = ""
    Private mDrugFrequency As String = ""
    Private mDrugDuration As String = ""
    Private mDrugStrength As String = ""
    Private mDrugQuantity As String = ""
    Private mRefillQuantity As String = ""
    Private mDirections As String = ""
    Private mMaySubstitute As Boolean = "False"
    Private mWrittendate As String = ""
    Private mRefillsQualifier As String = "R"
    Private mPrescriptionID As String = ""
    Private mRxReferenceNumber As String = ""
    Private mDrugID As String = ""
    Private mStatus As String = ""
    Private mNotes As String = ""

    Private mDosageDescription As String = ""
    Private mMDDosageDescription As String = ""

    Private mProdCode As String = ""
    Private mProdCodeQualifier As String = ""
    Private mDosageForm As String = ""
    Private mStrength As String = ""
    Private mStrengthUnits As String = ""
    Private mDrugDBCode As String = ""
    Private mDrugDBCodeQualifier As String = ""
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







    Public Property SourceRxReferenceNumber() As String
        Get
            Return mSourceRxReferenceNumber
        End Get
        Set(ByVal value As String)
            mSourceRxReferenceNumber = value
        End Set
    End Property
    Public Property SourceRelatesToMessageID() As String
        Get
            Return mSourceRelatesToMessageID
        End Get
        Set(ByVal value As String)
            mSourceRelatesToMessageID = value
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
    Public Property MessageStatus() As String
        Get
            Return mMessageStatus
        End Get
        Set(ByVal value As String)
            mMessageStatus = value
        End Set
    End Property
    Public Property SourceMessageName() As String
        Get
            Return mSourceMessageName
        End Get
        Set(ByVal value As String)
            mSourceMessageName = value
        End Set
    End Property
    '****************Patient Information property procedures**********************************
    Public Property PatientName() As String
        Get
            Return mPatientName
        End Get
        Set(ByVal value As String)
            mPatientName = value
        End Set
    End Property


    Public Property PatientDOB() As String
        Get
            Return mPatientDOB
        End Get
        Set(ByVal value As String)
            mPatientDOB = value
        End Set
    End Property


    Public Property PatientGender() As String
        Get
            Return mPatientGender
        End Get
        Set(ByVal value As String)
            mPatientGender = value
        End Set
    End Property


    Public Property PatientAddress() As String
        Get
            Return mPatientAddress
        End Get
        Set(ByVal value As String)
            mPatientAddress = value
        End Set
    End Property
    Public Property PatientAddress2() As String
        Get
            Return mPatientAddress2
        End Get
        Set(ByVal value As String)
            mPatientAddress2 = value
        End Set
    End Property

    Public Property PatientPhone() As String
        Get
            Return mPatientPhone
        End Get
        Set(ByVal value As String)
            mPatientPhone = value
        End Set
    End Property


    Public Property PatientCity() As String
        Get
            Return mPatientCity
        End Get
        Set(ByVal value As String)
            mPatientCity = value
        End Set
    End Property


    Public Property PatientState() As String
        Get
            Return mPatientState
        End Get
        Set(ByVal value As String)
            mPatientState = value
        End Set
    End Property


    Public Property PatientZip() As String
        Get
            Return mPatientZip
        End Get
        Set(ByVal value As String)
            mPatientZip = value
        End Set
    End Property
    '***********Patient Information property procedures***************************************

    '****************Pharmacy Information property procedures**********************************
    Public Property PharmacyID() As String
        Get
            Return mPharmacyID
        End Get
        Set(ByVal value As String)
            mPharmacyID = value
        End Set
    End Property


    Public Property PharmacyName() As String
        Get
            Return mPharmacyName
        End Get
        Set(ByVal value As String)
            mPharmacyName = value
        End Set
    End Property


    Public Property PharmacyAddress() As String
        Get
            Return mPharmacyAddress
        End Get
        Set(ByVal value As String)
            mPharmacyAddress = value
        End Set
    End Property
    Public Property PharmacyAddress2() As String
        Get
            Return mPharmacyAddress2
        End Get
        Set(ByVal value As String)
            mPharmacyAddress2 = value
        End Set
    End Property

    Public Property PharmacyPhone() As String
        Get
            Return mPharmacyPhone
        End Get
        Set(ByVal value As String)
            mPharmacyPhone = value
        End Set
    End Property


    Public Property PharmacyCity() As String
        Get
            Return mPharmacyCity
        End Get
        Set(ByVal value As String)
            mPharmacyCity = value
        End Set
    End Property


    Public Property PharmacyState() As String
        Get
            Return mPharmacyState
        End Get
        Set(ByVal value As String)
            mPharmacyState = value
        End Set
    End Property


    Public Property PharmacyZip() As String
        Get
            Return mPharmacyZip
        End Get
        Set(ByVal value As String)
            mPharmacyZip = value
        End Set
    End Property
    Public Property PharmacyFax() As String
        Get
            Return mPharmacyFax
        End Get
        Set(ByVal value As String)
            mPharmacyFax = value
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
    '****************Pharmacy Information property procedures**********************************

    '****************Provider (means Prescriber) Information property procedures**********************************
    Public Property ProviderID() As String
        Get
            Return mProviderID
        End Get
        Set(ByVal value As String)
            mProviderID = value
        End Set

    End Property

    Public Property ProviderName() As String
        Get
            Return mProviderName
        End Get
        Set(ByVal value As String)
            mProviderName = value
        End Set
    End Property


    Public Property ProviderAddress() As String
        Get
            Return mProviderAddress
        End Get
        Set(ByVal value As String)
            mProviderAddress = value
        End Set
    End Property
    Public Property ProviderAddress2() As String
        Get
            Return mProviderAddress2
        End Get
        Set(ByVal value As String)
            mProviderAddress2 = value
        End Set
    End Property

    Public Property ProviderPhone() As String
        Get
            Return mProviderPhone
        End Get
        Set(ByVal value As String)
            mProviderPhone = value
        End Set
    End Property


    Public Property ProviderCity() As String
        Get
            Return mProviderCity
        End Get
        Set(ByVal value As String)
            mProviderCity = value
        End Set
    End Property


    Public Property ProviderState() As String
        Get
            Return mProviderState
        End Get
        Set(ByVal value As String)
            mProviderState = value
        End Set
    End Property


    Public Property ProviderZip() As String
        Get
            Return mProviderZip
        End Get
        Set(ByVal value As String)
            mProviderZip = value
        End Set
    End Property
    Public Property ProviderFax() As String
        Get
            Return mProviderFax
        End Get
        Set(ByVal value As String)
            mProviderFax = value
        End Set
    End Property
    Public Property ProviderNPI() As String
        Get
            Return mProviderNPI
        End Get
        Set(ByVal value As String)
            mProviderNPI = value
        End Set
    End Property
    '*********************Property Procedures *****************
    Public Property MessageName() As String
        Get
            Return mMessageName
        End Get
        Set(ByVal value As String)
            mMessageName = value
        End Set
    End Property

    Public Property RelatesToMsgId() As String
        Get
            Return mRelatesToMsgId
        End Get
        Set(ByVal value As String)
            mRelatesToMsgId = value
        End Set
    End Property


    Public Property MessageId() As String
        Get
            Return mMessageId
        End Get
        Set(ByVal value As String)
            mMessageId = value
        End Set
    End Property


    Public Property DateSent() As String
        Get
            Return mDateSent
        End Get
        Set(ByVal value As String)
            mDateSent = value
        End Set
    End Property

    '*** Prescription
    'Public Property DDID() As String
    '    Get
    '        Return mDDID
    '    End Get
    '    Set(ByVal value As String)
    '        mDDID = value
    '    End Set
    'End Property

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

    Public Property DosageForm() As String
        Get
            Return mDosageForm
        End Get
        Set(ByVal value As String)
            mDosageForm = value
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
    '***  End ******





    '*********************Property Procedures for Error Transaction*****************


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


    Public Function GetErrorDetails(ByVal PrescriberID As String) As DataTable
        Dim objSureScriptDBLayer As New gloSureScriptDBLayer
        Dim dt As New DataTable
        Try
            '"select distinct e.nTransactionID ,s.sRelatesToMessageID,sErrorCode,sDescriptionCode,sDescription from ErrorTransaction e inner join SureScriptMessageTransaction s on Convert(varchar(50),e.nTransactionID)=s.sReferenceNumber where s.sMessageName='Error'"
            If Not IsNothing(PrescriberID) Then
                dt = objSureScriptDBLayer.GetErrorTransaction_Details(PrescriberID)
            End If
           
            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return dt
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
            Return dt
        Catch ex As Exception
            Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
            Throw obj
            Return dt
        Finally
            If Not IsNothing(objSureScriptDBLayer) Then
                objSureScriptDBLayer.Dispose()
                objSureScriptDBLayer = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function



    Public Sub GetErrorMessageDetails(ByVal TransactionID, ByVal RelatestoMessageID) 'As DataTable
        Dim objSureScriptDBLayer As New gloSureScriptDBLayer
        Dim oErrorMessage As SureScriptErrorMessage
        Try
            oErrorMessage = New SureScriptErrorMessage
            oErrorMessage.TransactionID = TransactionID
            oErrorMessage.RelatesToMessageId = RelatestoMessageID
            objSureScriptDBLayer.GetErrorMessageDetails(oErrorMessage)

            'assign the oErrorMessage object to the Property Procedure
            mMessageName = oErrorMessage.MessageName
            mRelatesToMsgId = oErrorMessage.RelatesToMessageId
            mMessageId = oErrorMessage.MessageID
            If Not IsNothing(oErrorMessage.DateReceived) Then
                mDateSent = CType(oErrorMessage.DateReceived, DateTime)
            End If

            If Not IsNothing(oErrorMessage.PrescriptionObject) Then
                SetData(oErrorMessage.PrescriptionObject)
            End If

        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)

        Catch ex As Exception
            Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
            Throw obj
        Finally
            If Not IsNothing(objSureScriptDBLayer) Then
                objSureScriptDBLayer.Dispose()
                objSureScriptDBLayer = Nothing
            End If

        End Try
    End Sub
    Public Sub UpdateMessageTransaction(ByVal TransactionID As String) 'As DataTable
        Dim objSureScriptDBLayer As New gloSureScriptDBLayer

        Try
            objSureScriptDBLayer.UpdateMessageTransaction(TransactionID)
        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)

        Catch ex As Exception
            Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
            Throw obj
        Finally
            If Not IsNothing(objSureScriptDBLayer) Then
                objSureScriptDBLayer.Dispose()
                objSureScriptDBLayer = Nothing
            End If

        End Try
    End Sub
    Public Sub SetData(ByVal ogloPrescription As EPrescription)
        Try
            'get data from ogloPrescription and set it to Property procedures
            If Not IsNothing(ogloPrescription) Then

                'assign values to patient information property proc
                mPatientName = ogloPrescription.RxPatient.PatientName.FirstName & " " & ogloPrescription.RxPatient.PatientName.LastName
                mPatientDOB = ogloPrescription.RxPatient.DateofBirth
                mPatientGender = ogloPrescription.RxPatient.Gender
                mPatientAddress = ogloPrescription.RxPatient.PatientAddress.Address1
                mPatientAddress2 = ogloPrescription.RxPatient.PatientAddress.Address2
                mPatientPhone = ogloPrescription.RxPatient.PatientPhone.Phone
                mPatientCity = ogloPrescription.RxPatient.PatientAddress.City
                mPatientState = ogloPrescription.RxPatient.PatientAddress.State
                mPatientZip = ogloPrescription.RxPatient.PatientAddress.Zip


                'assign values to Pharmacy information property proc
                mPharmacyName = ogloPrescription.RxPharmacy.PharmacyName
                mPharmacyAddress = ogloPrescription.RxPharmacy.PharmacyAddress.Address1
                mPharmacyAddress2 = ogloPrescription.RxPharmacy.PharmacyAddress.Address2
                mPharmacyPhone = ogloPrescription.RxPharmacy.PharmacyPhone.Phone
                mPharmacyCity = ogloPrescription.RxPharmacy.PharmacyAddress.City
                mPharmacyState = ogloPrescription.RxPharmacy.PharmacyAddress.State
                mPharmacyZip = ogloPrescription.RxPharmacy.PharmacyAddress.Zip
                mPharmacyFax = ogloPrescription.RxPharmacy.PharmacyPhone.Fax
                mPharmacyNPI = ogloPrescription.RxPharmacy.PharmacyNPI


                'assign values to Provider (means Prescriber) information property proc
                mProviderName = ogloPrescription.RxPrescriber.PrescriberName.FirstName & " " & ogloPrescription.RxPrescriber.PrescriberName.LastName
                mProviderAddress = ogloPrescription.RxPrescriber.PrescriberAddress.Address1
                mProviderAddress2 = ogloPrescription.RxPrescriber.PrescriberAddress.Address2
                mProviderPhone = ogloPrescription.RxPrescriber.PrescriberPhone.Phone
                mProviderCity = ogloPrescription.RxPrescriber.PrescriberAddress.City
                mProviderState = ogloPrescription.RxPrescriber.PrescriberAddress.State
                mProviderZip = ogloPrescription.RxPrescriber.PrescriberAddress.Zip
                mProviderFax = ogloPrescription.RxPrescriber.PrescriberPhone.Fax
                mProviderNPI = ogloPrescription.RxPrescriber.PrescriberNPI
                mSourceRxReferenceNumber = ogloPrescription.RxReferenceNumber



                'ogloPrescription.DgClQualifier1()
                'objdrug.PrimaryQualifier1()
                'objdrug.PrimaryValue1()
                'objdrug.SecQualifier1()
                'objdrug.SecValue1()
                'objdrug.DgClQualifier2()
                'objdrug.ProdCode = dt.Rows(icnt)("ProductCode")
                'objdrug.ProdCodeQualifier = dt.Rows(icnt)("ProductCodeQualifier")
                'objdrug.DosageForm = dt.Rows(icnt)("DosageForm")
                'objdrug.StrengthUnits = dt.Rows(icnt)("StrengthUnits")
                'objdrug.DrugDBCode = dt.Rows(icnt)("DrugDBCode")
                'objdrug.DrugDBCodeQualifier = dt.Rows(icnt)("DrugDBCodeQualifier")
                'objdrug.PriorAuthorizationQualifier = dt.Rows(icnt)("PriorAuthorizationQualifier")
                'objdrug.PriorAuthorizationValue = dt.Rows(icnt)("PriorAuthorizationValue")

                


                'objdrug.MDStrength = dt.Rows(icnt)("MDStrength")
                'objdrug.MDDosage = dt.Rows(icnt)("MDDosage")
                'objdrug.MDDosageForm = dt.Rows(icnt)("MDDosageForm")
                'objdrug.MDRoute = dt.Rows(icnt)("MDRoute")
                'objdrug.MDFrequency = dt.Rows(icnt)("MDFrequency")
                'objdrug.MDDuration = dt.Rows(icnt)("MDDuration")
                'objdrug.MDQuantity = dt.Rows(icnt)("MDQuantity")
                'objdrug.MDRefillQuantity = dt.Rows(icnt)("MDRefillQuantity")
                'objdrug.MDRefillQualifier = dt.Rows(icnt)("MDRefillQualifier")

                'If dt.Rows(icnt)("MDbMaySubstitutions") = False Then
                '    objdrug.MDbMaySubstitutions = True
                'Else
                '    objdrug.MDbMaySubstitutions = False
                'End If
                'objdrug.MDdtWrittendate = dt.Rows(icnt)("MDdtWrittendate")
                'objdrug.MDProductCode = dt.Rows(icnt)("MDProductCode")
                'objdrug.MDPotencyUnitCode = dt.Rows(icnt)("MDPotencyUnitCode")
                'objdrug.MDDosageForm = GetDescriptionFromPotency(dt.Rows(icnt)("MDPotencyUnitCode"))
                'objdrug.MDdtlastdate = dt.Rows(icnt)("MDdtlastdate")
                'objdrug.MDProductCodeQualifier = dt.Rows(icnt)("MDProductCodeQualifier")
                'objdrug.MDStrengthUnits = dt.Rows(icnt)("MDStrengthUnits")
                'objdrug.MDDrugDBCode = dt.Rows(icnt)("MDDrugDBCode")
                'objdrug.MDDrugDBCodeQualifier = dt.Rows(icnt)("MDDrugDBCodeQualifier")
                'objdrug.MDNotes = dt.Rows(icnt)("sMDNotes")
                'objdrug.MDDrugCoverageStatusCode = dt.Rows(icnt)("MDDrugCoverageStatusCode")







                If Not IsNothing(ogloPrescription.RxPrescriber) Then
                    mMessageStatus = ogloPrescription.MessageStatus
                    mRxTransactionID = ogloPrescription.RxTransactionID 'PrescriptionID
                    If Not IsNothing(ogloPrescription.DrugsCol) Then
                        If ogloPrescription.DrugsCol.Count > 0 Then
                            mSourceMessageName = ogloPrescription.DrugsCol.Item(0).MessageName 'either newrx/refillresponse
                            mSourceRelatesToMessageID = ogloPrescription.DrugsCol.Item(0).RelatesToMessageId 'MessageID of Refreq


                            mDrugName = ogloPrescription.DrugsCol.Item(0).DrugName
                            mDrugForm = ogloPrescription.DrugsCol.Item(0).Drugform
                            mDrugStrength = ogloPrescription.DrugsCol.Item(0).DrugStrength
                            mDosage = ogloPrescription.DrugsCol.Item(0).Dosage
                            'objdrug.DrugRoute = dt.Rows(icnt)("DrugRoute")
                            mDrugFrequency = ogloPrescription.DrugsCol.Item(0).DrugFrequency
                            mDirections = ogloPrescription.DrugsCol.Item(0).Directions
                            mDrugDuration = ogloPrescription.DrugsCol.Item(0).DrugDuration
                            mDrugQuantity = ogloPrescription.DrugsCol.Item(0).DrugQuantity
                            mRefillQuantity = ogloPrescription.DrugsCol.Item(0).RefillQuantity

                            mRefillsQualifier = ogloPrescription.DrugsCol.Item(0).RefillsQualifier

                            mMaySubstitute = ogloPrescription.DrugsCol.Item(0).MaySubstitute

                            mDosageDescription = ogloPrescription.DrugsCol.Item(0).DosageDescription
                            mMDDosageDescription = ogloPrescription.DrugsCol.Item(0).MDDosageDescription

                            mWrittendate = ogloPrescription.DrugsCol.Item(0).WrittenDate
                            mLastfillDate = ogloPrescription.DrugsCol.Item(0).LastfillDate

                            'objdrug.RxReferenceNumber = dt.Rows(icnt)("RxReferenceNumber")
                            'objdrug.TransactionID = objdrug.RxReferenceNumber 'TransactionId for Individual Message Object
                            'objdrug.RelatesToMessageId = dt.Rows(icnt)("MessageID")
                            mNotes = ogloPrescription.DrugsCol.Item(0).Notes

                            mPotencyCode = ogloPrescription.DrugsCol.Item(0).PotencyCode
                            mDosageForm = ogloPrescription.DrugsCol.Item(0).DosageForm
                            ' objdrug.DrugCoverageStatusCode = dt.Rows(icnt)("DrugCoverageStatusCode")

                            mMDDrugName = ogloPrescription.DrugsCol.Item(0).MDDrugName
                            mMDDrugForm = ogloPrescription.DrugsCol.Item(0).MDDrugForm
                            mMDStrength = ogloPrescription.DrugsCol.Item(0).MDStrength
                            mMDDosage = ogloPrescription.DrugsCol.Item(0).MDDosage
                            'objdrug.DrugRoute = dt.Rows(icnt)("DrugRoute")
                            mMDFrequency = ogloPrescription.DrugsCol.Item(0).MDFrequency
                            ' mMDDirections = objdrug.MDDirections
                            mMDDuration = ogloPrescription.DrugsCol.Item(0).MDDuration
                            mMDQuantity = ogloPrescription.DrugsCol.Item(0).MDQuantity
                            'mMDQuantityQualifier = objdrug.MDDrugQuantityQualifier
                            mMDRefillQuantity = ogloPrescription.DrugsCol.Item(0).MDRefillQuantity

                            mMDRefillQualifier = ogloPrescription.DrugsCol.Item(0).MDRefillQualifier

                            mMDbMaySubstitutions = ogloPrescription.DrugsCol.Item(0).MDbMaySubstitutions


                            mMDdtWrittendate = ogloPrescription.DrugsCol.Item(0).MDdtWrittendate
                            mMDdtlastdate = ogloPrescription.DrugsCol.Item(0).MDdtlastdate

                            'objdrug.RxReferenceNumber = dt.Rows(icnt)("RxReferenceNumber")
                            'objdrug.TransactionID = objdrug.RxReferenceNumber 'TransactionId for Individual Message Object
                            'objdrug.RelatesToMessageId = dt.Rows(icnt)("MessageID")
                            sMDNotes = ogloPrescription.DrugsCol.Item(0).Notes

                            mMDPotencyUnitCode = ogloPrescription.DrugsCol.Item(0).MDPotencyUnitCode
                            mMDDosage = ogloPrescription.DrugsCol.Item(0).MDDosageForm

                        End If
                    End If
                End If
            End If
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As Exception
            Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
            Throw obj
        Finally

        End Try
    End Sub
    
    
    Public Sub New(Optional ByVal SQLUserNameEMR As String = "", Optional ByVal SQLPasswordEMR As String = "")
        gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
        gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName
        ''gloSurescriptGeneral.sUserName = globalSecurity.gstrLoginName
        ''gloSurescriptGeneral.sPassword = globalSecurity.gstrLoginPassword
        If SQLUserNameEMR <> "" Then
            gloSurescriptGeneral.sUserName = SQLUserNameEMR
            gloSurescriptGeneral.sPassword = SQLPasswordEMR
        End If
        gloSurescriptGeneral.RootPath = clsgeneral.StartUpPath
        gloSurescriptGeneral.blnIsStagingServer = clsgeneral.gblnIsStagingServer
    End Sub
    'C
End Class
