Public Class clsEFaxSettings

#Region "private variables"
    Private user_id As String = gstrEFaxUserID
    Private user_password As String = gstrEFaxUserPassword
    Private fax_recipient_number As String = "18668828796"
    Private tiff_image_flag As String = "false"
    Private billing_code As String = "gloEMR"
    Private from_name As String = "gloEMR"
    Private resolution As String = ""
    Private fax_recipient_name As String = ""
    Private document_content_type As String = ""
    Private document_encoding_type As String = "base64"
    Private document_extension As String = ""

    'variables used by the modules sending fax to store the faxes  sent as binary data till they r sent
    Private BinaryData As Byte()
    Private BinaryDataforCoverPage As Byte()
    'for returning the faxid
    Private nFaxID As Int64 = 0

    'variables for fax cover page and the fax file name
    Private blnFaxCoverPage As Boolean = False
    Private coverpagefilepath As String = ""
    Private faxfilepath As String = ""

    Private blnSendFaxPriority As Boolean = False
    Private _nPatientID As Int64 = 0

#End Region

#Region "Properties"
    Public Property EFax_UserID() As String
        Get
            Return user_id
        End Get
        Set(ByVal value As String)
            user_id = value
        End Set
    End Property

    Public Property EFax_UserPassword() As String
        Get
            Return user_password
        End Get
        Set(ByVal value As String)
            user_password = value
        End Set
    End Property


    Public Property EFax_FaxRecipientNumber() As String
        Get
            Return fax_recipient_number
        End Get
        Set(ByVal value As String)
            fax_recipient_number = value
        End Set
    End Property


    Public Property EFax_Tiff_image_flag() As String
        Get
            Return tiff_image_flag
        End Get
        Set(ByVal value As String)
            tiff_image_flag = value
        End Set
    End Property



    Public Property EFax_BillingCode() As String
        Get
            Return billing_code
        End Get
        Set(ByVal value As String)
            billing_code = value
        End Set
    End Property

    Public Property EFax_FromName() As String
        Get
            Return from_name
        End Get
        Set(ByVal value As String)
            from_name = value
        End Set
    End Property

    Public Property EFax_Resolution() As String
        Get
            Return resolution
        End Get
        Set(ByVal value As String)
            resolution = value
        End Set
    End Property

    Public Property EFax_FaxRecipientName() As String
        Get
            Return fax_recipient_name
        End Get
        Set(ByVal value As String)
            fax_recipient_name = value
        End Set
    End Property

    Public Property EFax_DocumentContentType() As String
        Get
            Return document_content_type
        End Get
        Set(ByVal value As String)
            document_content_type = value
        End Set
    End Property

    Public Property EFax_DocumentEncodingType() As String
        Get
            Return document_encoding_type
        End Get
        Set(ByVal value As String)
            document_encoding_type = value
        End Set
    End Property

    Public Property EFax_DocumentExtension() As String
        Get
            Return document_extension
        End Get
        Set(ByVal value As String)
            document_extension = value
        End Set
    End Property

    '// properties used by the modules sending fax to store the faxes  sent as binary data till they r sent

    Public Property FaxFileBinaryData() As Byte()
        Get
            Return BinaryData
        End Get
        Set(ByVal value As Byte())
            BinaryData = value
        End Set
    End Property

    Public Property FaxCoverPageBinaryData() As Byte()
        Get
            Return BinaryDataforCoverPage
        End Get
        Set(ByVal value As Byte())
            BinaryDataforCoverPage = value
        End Set
    End Property

    '//Property for FaxId
    Public Property FaxID() As Int64
        Get
            Return nFaxID
        End Get
        Set(ByVal value As Int64)
            nFaxID = value
        End Set
    End Property

    Public Property SendFaxPriority() As Boolean
        Get
            Return blnSendFaxPriority
        End Get
        Set(ByVal value As Boolean)
            blnSendFaxPriority = value
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

    '//'properties for fax cover page and the fax file name

    '//the coverpage is a word template
    Public Property EFax_FaxCoverpagefilepath() As String
        Get
            Return coverpagefilepath
        End Get
        Set(ByVal value As String)
            coverpagefilepath = value
        End Set
    End Property

    Public Property EFax_Faxfilepath() As String
        Get
            Return faxfilepath
        End Get
        Set(ByVal value As String)
            faxfilepath = value
        End Set
    End Property


    Public Property EFax_FaxCoverPage() As Boolean
        Get
            Return blnFaxCoverPage
        End Get
        Set(ByVal value As Boolean)
            blnFaxCoverPage = value
        End Set
    End Property
#End Region

End Class
