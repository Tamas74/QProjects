
Public Class Prescriber
    Inherits SureScriptMessage

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mPrescriberName As PersonName
    Private mPrescriberAddress As AddressDetail
    Private mPrescriberPhone As PhoneNumber
    Private mPrescriberID As String
    Private mProviderID As String
    Private mActiveStartDate As String
    Private mActiveEndDate As String
    Private mServiceLevel As String
    Private mClinicName As String
    Private mDEA As String
    Private mNADEAN As String
    Private mNPI As String
    'sarika for removing Provider validations
    Private mPrescriberLocation As Boolean = False

    Private mDirectAddress As String
    Private mProviderSSN As String ''added for SS 10.6 change
    '--

    Public Property ProviderSSN() As String
        Get
            Return mProviderSSN
        End Get
        Set(ByVal value As String)
            mProviderSSN = value
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
    Public Property DEA() As String
        Get
            Return mDEA
        End Get
        Set(ByVal value As String)
            mDEA = value
        End Set
    End Property
    Public Property NADEAN() As String
        Get
            Return mNADEAN
        End Get
        Set(ByVal value As String)
            mNADEAN = value
        End Set
    End Property
    Public Property NPI() As String
        Get
            Return mNPI
        End Get
        Set(ByVal value As String)
            mNPI = value
        End Set
    End Property
    Public Property ActiveStartDate() As String
        Get
            Return mActiveStartDate
        End Get
        Set(ByVal value As String)
            mActiveStartDate = value
        End Set
    End Property
    Public Property ActiveEndDate() As String
        Get
            Return mActiveEndDate
        End Get
        Set(ByVal value As String)
            mActiveEndDate = value
        End Set
    End Property
    Public Property ServiceLevel() As String
        Get
            Return mServiceLevel
        End Get
        Set(ByVal value As String)
            mServiceLevel = value
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
            Return mPrescriberPhone
        End Get
        Set(ByVal value As PhoneNumber)
            mPrescriberPhone = value
        End Set
    End Property


    'sarika For removing Provider validations
    Public Property PrescriberLocation() As Boolean
        Get
            Return mPrescriberLocation
        End Get
        Set(ByVal value As Boolean)
            mPrescriberLocation = value
        End Set
    End Property

    Public Property DirectAddress() As String
        Get
            Return mDirectAddress
        End Get
        Set(ByVal value As String)
            mDirectAddress = value
        End Set
    End Property
    '---

    ' IDisposable

    Public Property Gender As String
    Public Property AUSID As String
    Public Property IsDirectMessageEnabled As Boolean
    Public Property IsServiceLevelDisabled As Boolean
    Public Property IsNewRxEnabled As Boolean
    Public Property IsControlledSubstance As Boolean
    Public Property IsePAEnabled As Boolean
    Public Property IsRefillEnabled As Boolean
    Public Property DatabaseName As String
    Public Property gloSuiteVersion As String
    Public Property CreatedDateTime As Date
    Public Property ModifiedDateTime As Date


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
        mPrescriberPhone = New PhoneNumber
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
            Return mPharmacyPhone
        End Get
        Set(ByVal value As PhoneNumber)
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
        mPharmacyPhone = New PhoneNumber
    End Sub
End Class
Public Class PersonName

    Private _ID As Long
    Private _FirstName As String = ""
    Private _MiddleName As String = ""
    Private _LastName As String = ""
    Private _Code As String = ""
    Private _prefix As String = ""
    Private _suffix As String = ""

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
    Public Property Prefix() As String
        Get
            Return _prefix
        End Get
        Set(ByVal Value As String)
            _prefix = Value
        End Set
    End Property

    Public Property Suffix() As String
        Get
            Return _suffix
        End Get
        Set(ByVal Value As String)
            _suffix = Value
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
    Private mMessageId As String = ""
    Private mMessageName As String = ""
    Private mRelatesToMessageId As String = ""
    Private mMessageFrom As String = ""
    Private mMessageTo As String = ""
    Private mDateTimeStamp As String = ""
    Private mDateReceived As String = ""
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
    Private oPrescription As Prescriber
    Public Property PrescriptionObject() As Prescriber
        Get
            If IsNothing(oPrescription) Then
                oPrescription = New Prescriber
            End If
            Return oPrescription
        End Get
        Set(ByVal value As Prescriber)
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
    Private oPrescription As Prescriber
    Private mDenialcode As String
    Private mDenialReason As String
    Private blnIsApproved As Boolean
    Private mNotes As String
    Public Property PrescriptionObject() As Prescriber
        Get
            If IsNothing(oPrescription) Then
                oPrescription = New Prescriber
            End If
            Return oPrescription
        End Get
        Set(ByVal value As Prescriber)
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

