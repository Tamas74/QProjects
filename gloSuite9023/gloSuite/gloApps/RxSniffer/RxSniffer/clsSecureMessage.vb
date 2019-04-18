Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.IO
Imports System.ServiceModel

Namespace gloSecureMessage
    Public Class SecureMessage
        Implements IDisposable

#Region "Constructor & Destructor"



        Public Sub New()
        End Sub

        Private disposed As Boolean = False

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposed Then

                If disposing Then
                End If
            End If
            disposed = True
        End Sub

        Protected Overrides Sub Finalize()
            Try

                Dispose(False)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#End Region

#Region "Properties for SecureMessage Object"

        'nSecureMessageInboxID
        Private _secureMessageInboxID As Int64 = 0
        Public Property secureMessageInboxID() As Int64
            Get
                Return _secureMessageInboxID
            End Get
            Set(value As Int64)
                _secureMessageInboxID = value
            End Set
        End Property

        'Message ID
        Private _messageID As String = ""
        Public Property messageID() As String
            Get
                Return _messageID
            End Get
            Set(value As String)
                _messageID = value
            End Set
        End Property

        'Relates To Message ID
        Private _relateMessageID As String = ""
        Public Property relateMessageID() As String
            Get
                Return _relateMessageID
            End Get
            Set(value As String)
                _relateMessageID = value
            End Set
        End Property

        'Version No
        Private _version As String = ""
        Public Property version() As String
            Get
                Return _version
            End Get
            Set(value As String)
                _version = value
            End Set
        End Property

        'Release No
        Private _release As String = ""
        Public Property release() As String
            Get
                Return _release
            End Get
            Set(value As String)
                _release = value
            End Set
        End Property


        'Highest Version No
        Private _highVersion As String = ""
        Public Property highVersion() As String
            Get
                Return _highVersion
            End Get
            Set(value As String)
                _highVersion = value
            End Set
        End Property

        'SenderID
        Private _senderId As Int64 = 0
        Public Property senderID() As Int64
            Get
                Return _senderId
            End Get
            Set(value As Int64)
                _senderId = value
            End Set
        End Property

        'ReceiverID
        Private _receiverId As Int64 = 0
        Public Property receiverID() As Int64
            Get
                Return _receiverId
            End Get
            Set(value As Int64)
                _receiverId = value
            End Set
        End Property

        'From
        Private _from As String = ""
        Public Property From() As String
            Get
                Return _from
            End Get
            Set(value As String)
                _from = value
            End Set
        End Property

        'FromQualifier
        Private _fromQualifier As String = ""
        Public Property FromQualifier() As String
            Get
                Return _fromQualifier
            End Get
            Set(value As String)
                _fromQualifier = value
            End Set
        End Property

        'To
        Private _to As String = ""
        Public Property [To]() As String
            Get
                Return _to
            End Get
            Set(value As String)
                _to = value
            End Set
        End Property

        'ToQualifier
        Private _toQualifier As String = ""
        Public Property ToQualifier() As String
            Get
                Return _toQualifier
            End Get
            Set(value As String)
                _toQualifier = value
            End Set
        End Property

        'Subject
        Private _subject As String = ""
        Public Property subject() As String
            Get
                Return _subject
            End Get
            Set(value As String)
                _subject = value
            End Set
        End Property

        'MessageBody
        Private _messageBody As String = ""
        Public Property messageBody() As String
            Get
                Return _messageBody
            End Get
            Set(value As String)
                _messageBody = value
            End Set
        End Property

        'DateTimeUTC
        Private _dateTimeUTC As String = ""
        Public Property dateTimeUTC() As String
            Get
                Return _dateTimeUTC
            End Get
            Set(value As String)
                _dateTimeUTC = value
            End Set
        End Property


        'DateTimeUTC
        Private _dateUTC As DateTime
        Public Property dateUTC() As DateTime
            Get
                Return _dateUTC
            End Get
            Set(value As DateTime)
                _dateUTC = value
            End Set
        End Property

        'DateTimeNormal
        Public Property dateTimeNormal() As DateTime
            Get
                Return m_dateTimeNormal
            End Get
            Set(value As DateTime)
                m_dateTimeNormal = value
            End Set
        End Property
        Private m_dateTimeNormal As DateTime

        'IsUnRead
        Private _isRead As Int16 = 0
        Public Property isRead() As Int16
            Get
                Return _isRead
            End Get
            Set(value As Int16)
                _isRead = value
            End Set
        End Property

        'PatientID
        Private _patientId As Int64 = 0
        Public Property patientID() As Int64
            Get
                Return _patientId
            End Get
            Set(value As Int64)
                _patientId = value
            End Set
        End Property

        'No ofAttachments
        Private _noOfAttachements As Int16 = 0
        Public Property noofAttachements() As Int16
            Get
                Return _noOfAttachements
            End Get
            Set(value As Int16)
                _noOfAttachements = value
            End Set
        End Property

        'Message Status
        Private _messageStatus As Int16 = 0
        Public Property MessageStatus() As Int16
            Get
                Return _messageStatus
            End Get
            Set(value As Int16)
                _messageStatus = value
            End Set
        End Property

        'Message Type
        Private _messageType As Int16 = 0
        Public Property messageType() As Int16
            Get
                Return _messageType
            End Get
            Set(value As Int16)
                _messageType = value
            End Set
        End Property

        'Associated
        Private _associated As Int16 = 0
        Public Property associated() As Int16
            Get
                Return _associated
            End Get
            Set(value As Int16)
                _associated = value
            End Set
        End Property

        'Delivery status code
        Private _deliveryStatusCode As String = ""
        Public Property deliveryStatusCode() As String
            Get
                Return _deliveryStatusCode
            End Get
            Set(value As String)
                _deliveryStatusCode = value
            End Set
        End Property

        'Delivery status code
        Private _deliveryStatusDescription As String = ""
        Public Property deliveryStatusDescription() As String
            Get
                Return _deliveryStatusDescription
            End Get
            Set(value As String)
                _deliveryStatusDescription = value
            End Set
        End Property

        'SoftWare Version
        Private _softwareVersion As String = ""
        Public Property softwareVersion() As String
            Get
                Return _softwareVersion
            End Get
            Set(value As String)
                _softwareVersion = value
            End Set
        End Property

        'SoftWare Product
        Private _softwareProduct As String = ""
        Public Property softwareProduct() As String
            Get
                Return _softwareProduct
            End Get
            Set(value As String)
                _softwareProduct = value
            End Set
        End Property

        'Company Name
        Private _companyName As String = ""
        Public Property companyName() As String
            Get
                Return _companyName
            End Get
            Set(value As String)
                _companyName = value
            End Set
        End Property

        'User Name
        Private _userName As String = ""
        Public Property userName() As String
            Get
                Return _userName
            End Get
            Set(value As String)
                _userName = value
            End Set
        End Property

        'Machine Name
        Private _machineName As String = ""
        Public Property machineName() As String
            Get
                Return _machineName
            End Get
            Set(value As String)
                _machineName = value
            End Set
        End Property

        'Deleted
        Private _deleted As Int16 = 0
        Public Property deleted() As Int16
            Get
                Return _deleted
            End Get
            Set(value As Int16)
                _deleted = value
            End Set
        End Property

        'PatientDemographics
        Private _firstName As String = ""
        Public Property firstName() As String
            Get
                Return _firstName
            End Get
            Set(value As String)
                _firstName = value
            End Set
        End Property

        Private _lastName As String = ""
        Public Property lastName() As String
            Get
                Return _lastName
            End Get
            Set(value As String)
                _lastName = value
            End Set
        End Property

        Private _Dob As String = ""
        Public Property Dob() As String
            Get
                Return _Dob
            End Get
            Set(value As String)
                _Dob = value
            End Set
        End Property

        Private _gender As String = ""
        Public Property gender() As String
            Get
                Return _gender
            End Get
            Set(value As String)
                _gender = value
            End Set
        End Property

        Private _zip As String = ""
        Public Property zip() As String
            Get
                Return _zip
            End Get
            Set(value As String)
                _zip = value
            End Set
        End Property

        Public Property DocumentReferenceID() As Int64
            Get
                Return m_DocumentReferenceID
            End Get
            Set(value As Int64)
                m_DocumentReferenceID = Value
            End Set
        End Property
        Private m_DocumentReferenceID As Int64


        Public Property sModuleName() As String
            Get
                Return _sModuleName
            End Get
            Set(value As String)
                _sModuleName = value
            End Set
        End Property
        Private _sModuleName As String
        'Added for patient Saving 
        Private _nUseCase As Int16 = 0
        Public Property UseCase() As Int16
            Get
                Return _nUseCase
            End Get
            Set(value As Int16)
                _nUseCase = value
            End Set
        End Property

        Private sDelegatedUser As String
        Public Property DelegatedUser() As String
            Get
                Return sDelegatedUser
            End Get
            Set(value As String)
                sDelegatedUser = value
            End Set
        End Property



#End Region

    End Class

    Public Class Attachment
        Implements IDisposable
#Region "Constructor & Destructor"

        Public Sub New()
        End Sub

        Private disposed As Boolean = False

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposed Then

                If disposing Then
                End If
            End If
            disposed = True
        End Sub

        Protected Overrides Sub Finalize()
            Try

                Dispose(False)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#End Region

#Region "Properties for Attachment Object"

        'nID
        Private _nSecureMessageInboxID As Int64 = 0
        Public Property nSecureMessageInboxID() As Int64
            Get
                Return _nSecureMessageInboxID
            End Get
            Set(value As Int64)
                _nSecureMessageInboxID = value
            End Set
        End Property

        'Message ID
        Private _attachmentID As Int64 = 0
        Public Property attachmentID() As Int64
            Get
                Return _attachmentID
            End Get
            Set(value As Int64)
                _attachmentID = value
            End Set
        End Property

        'ModuleName
        Private _moduleName As Int16 = 0
        Public Property moduleName() As Int16
            Get
                Return _moduleName
            End Get
            Set(value As Int16)
                _moduleName = value
            End Set
        End Property

        'FileExtension
        Private _fileExtension As Int16 = 0
        Public Property fileExtension() As Int16
            Get
                Return _fileExtension
            End Get
            Set(value As Int16)
                _fileExtension = value
            End Set
        End Property

        'DocumentName
        Private _documentName As String = ""
        Public Property documentName() As String
            Get
                Return _documentName
            End Get
            Set(value As String)
                _documentName = value
            End Set
        End Property

        'Content
        Private _iContent As Byte() = Nothing
        Public Property iContent() As Byte()
            Get
                Return _iContent
            End Get
            Set(value As Byte())
                _iContent = value
            End Set
        End Property

        'sCDAConfidentiality
        Private _sCDAConfidentiality As String = ""
        Public Property sCDAConfidentiality() As String
            Get
                Return _sCDAConfidentiality
            End Get
            Set(value As String)
                _sCDAConfidentiality = value
            End Set
        End Property

        'Base64String
        Private _base64 As String = ""
        Public Property base64() As String
            Get
                Return _base64
            End Get
            Set(value As String)
                _base64 = value
            End Set
        End Property

        'MineType
        Private _mimeType As String = ""
        Public Property mimeType() As String
            Get
                Return _mimeType
            End Get
            Set(value As String)
                _mimeType = value
            End Set
        End Property


#End Region

    End Class
End Namespace
