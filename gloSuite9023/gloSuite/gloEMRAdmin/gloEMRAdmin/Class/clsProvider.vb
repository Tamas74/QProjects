'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************

Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq


Public Class clsProvider
    Public gstrSQLError As String = "Error while establishing connection with the server"
    Public gstrMessageBoxCaption As String = "gloEMR Admin"

#Region " Private Variables"

    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings

    Private _nProviderID As Int64 = 0
    Private _sFirstName As String = ""
    Private _sMiddleName As String = ""
    Private _sLastName As String = ""
    Private _sSuffix As String = ""
    Private _sGender As String = ""
    Private _sDEA As String = ""
    Private _sNADEAN As String = ""
    Private _sDPSNumber As String = ""
    Private _NPI As String = ""
    Private _UPIN As String = ""
    Private _StateMedicalNo As String = ""

    Private _sBMContactName As String = ""
    Private _sBMAddress1 As String = ""
    Private _sBMAddress2 As String = ""
    Private _sBMCity As String = ""
    Private _sBMState As String = ""
    Private _sBMZIP As String = ""
    Private _sBMCountry As String = ""          ''Country
    Private _sBMCounty As String = ""           ''county


    Private _sBPracContactName As String = ""
    Private _sBPracAddress1 As String = ""
    Private _sBPracAddress2 As String = ""
    Private _sBPracCity As String = ""
    Private _sBPracState As String = ""
    Private _sBPracZIP As String = ""
    Private _sBPracCountry As String = ""           ''Country
    Private _sBPracCounty As String = ""            ''County


    Private _sBMPhone As String = ""
    Private _sBMFAX As String = ""
    Private _sBMPager As String = ""
    Private _sBMEmail As String = ""
    Private _sBMURL As String = ""

    Private _sDirectAddress As String = ""

    Private _sBpracPhone As String = ""
    Private _sBpracFAX As String = ""
    Private _sBPracPager As String = ""
    Private _sBPracEmail As String = ""
    Private _sBPracURL As String = ""

    Private _sComapanyName As String = ""
    Private _sComapanyContactName As String = ""
    Private _sComapanyAddress1 As String = ""
    Private _sComapanyAddress2 As String = ""
    Private _sComapanyCity As String = ""
    Private _sComapanyState As String = ""
    Private _sComapanyZip As String = ""
    Private _sComapanyCountry As String = ""            ''Country
    Private _sComapanyCounty As String = ""             ''County

    Private _sComapanyPhone As String = ""
    Private _sComapanyFax As String = ""
    Private _sComapanyEmail As String = ""
    Private _sComapanyNPI As String = ""
    Private _sCompanyTaxonomyCode As String = ""
    Private _sComapanyTaxID As String = ""

    Private _sSSNno As String = ""
    Private _sEmployerID As String = ""

    Private _sMobile As String = ""
    Private _imgSignature As Image = Nothing

    Private _sSignFont As String = ""
    Private _nSignFontSize As Integer = 0
    Private _nSignWidth As Integer = 0
    Private _nMaxSignScale As Decimal = 0.0
    Private _nMinSignScale As Decimal = 0.0

    Private _Taxonomy As String = ""
    Private _TaxonomyDesc As String = ""
    Private _Prefix As String = ""
    Private _nProviderTypeID As Int64 = 0
    Private _nUserID As Int64 = 0
    Private _sUserName As String = ""
    Private _sPassword As String = ""
    Private _sNickName As String = ""

    Private _sExternalCode As String = ""
    Private _nClinicID As Int64 = 1

    Private _sCode As String = ""
    Private _sDescription As String = ""

    Private _nResourceTypeID As Int64 = 0
    Private _ProviderDetails As New ProviderDetails

    Private _bIsblocked As Boolean = False

    Private _sConnectionString As String = ""
    Private _sServiceLevel As String = String.Empty
    Private _dtActiveStartTime As DateTime = Nothing
    Private _dtActiveEndTime As DateTime = Nothing
    Private _sSPI As String = String.Empty
    Private _sRootSPI As String = String.Empty
    Private _nPARequired As Int16
    Private mPrescriberLocation As Boolean = False
    Private _blnRequireSupervisingProviderforeRx As Boolean = False
    Private _sErrorMessage As String
    'Provider Physical Address

    Private _sPhysicalAddContactName As String = ""
    Private _sPhysicalAddressline1 As String = ""
    Private _sPhysicalAddressline2 As String = ""
    Private _sPhysicalCity As String = ""
    Private _sPhysicalState As String = ""
    Private _sPhysicalZIP As String = ""
    Private _sPhysicalAreaCode As String = ""
    Private _sPhysicalCountry As String = ""
    Private _sPhysicalCounty As String = ""
    Private _sPhysicalPhoneNo As String = ""
    Private _sPhysicalFAX As String = ""
    Private _sPhysicalPagerNo As String = ""
    Private _sPhysicalEmail As String = ""
    Private _sPhysicalURL As String = ""

    'Provider Company Physical Address

    Private _sCompanyPhysicalAddContactName As String = ""
    Private _sCompanyPhysicalAddressline1 As String = ""
    Private _sCompanyPhysicalAddressline2 As String = ""
    Private _sCompanyPhysicalCity As String = ""
    Private _sCompanyPhysicalState As String = ""
    Private _sCompanyPhysicalZIP As String = ""
    Private _sCompanyPhysicalAreaCode As String = ""
    Private _sCompanyPhysicalCountry As String = ""
    Private _sCompanyPhysicalCounty As String = ""
    Private _sCompanyPhysicalPhoneNo As String = ""
    Private _sCompanyPhysicalFAX As String = ""
    Private _sCompanyPhysicalPagerNo As String = ""
    Private _sCompanyPhysicalEmail As String = ""
    Private _sCompanyPhysicalURL As String = ""
    Private _dtDOB As DateTime
    '--
    Public Enum enmAUSStatus
        Active = 0
        PendingForLicense = 1
        PendingForReview = 2
        DisabledProviders = 3
        BlockedProviders = 4
    End Enum
#End Region

#Region " Public Properties"

    Public Property ProviderID() As Int64
        Get
            Return _nProviderID
        End Get
        Set(ByVal value As Int64)
            _nProviderID = value
        End Set
    End Property
    Public Property FirstName() As String
        Get
            Return _sFirstName
        End Get
        Set(ByVal value As String)
            _sFirstName = value
        End Set
    End Property
    Public Property MiddleName() As String
        Get
            Return _sMiddleName
        End Get
        Set(ByVal value As String)
            _sMiddleName = value
        End Set
    End Property
    Public Property LastName() As String
        Get
            Return _sLastName
        End Get
        Set(ByVal value As String)
            _sLastName = value
        End Set
    End Property

    Public Property Suffix() As String
        Get
            Return _sSuffix
        End Get
        Set(ByVal value As String)
            _sSuffix = value
        End Set
    End Property

    Public Property Gender() As String
        Get
            Return _sGender
        End Get
        Set(ByVal value As String)
            _sGender = value
        End Set
    End Property
    Public Property DEA() As String
        Get
            Return _sDEA
        End Get
        Set(ByVal value As String)
            _sDEA = value
        End Set
    End Property
    Public Property NADEAN() As String
        Get
            Return _sNADEAN
        End Get
        Set(ByVal value As String)
            _sNADEAN = value
        End Set
    End Property
    Public Property DPSNumber() As String
        Get
            Return _sDPSNumber
        End Get
        Set(ByVal value As String)
            _sDPSNumber = value
        End Set
    End Property

    Public Property BMContactName() As String
        Get
            Return _sBMContactName
        End Get
        Set(ByVal value As String)
            _sBMContactName = value
        End Set
    End Property
    Public Property BMAddress1() As String
        Get
            Return _sBMAddress1
        End Get
        Set(ByVal value As String)
            _sBMAddress1 = value
        End Set
    End Property
    Public Property BMAddress2() As String
        Get
            Return _sBMAddress2
        End Get
        Set(ByVal value As String)
            _sBMAddress2 = value
        End Set
    End Property
    Public Property BMCity() As String
        Get
            Return _sBMCity
        End Get
        Set(ByVal value As String)
            _sBMCity = value
        End Set
    End Property
    Public Property BMState() As String
        Get
            Return _sBMState
        End Get
        Set(ByVal value As String)
            _sBMState = value
        End Set
    End Property
    Public Property BMZIP() As String
        Get
            Return _sBMZIP
        End Get
        Set(ByVal value As String)
            _sBMZIP = value
        End Set
    End Property

    Public Property BMCountry() As String           ''dhruv ''Againt Country
        Get
            Return _sBMCountry
        End Get
        Set(ByVal value As String)
            _sBMCountry = value
        End Set
    End Property
    Public Property BMCounty() As String           ''dhruv ''Againt County
        Get
            Return _sBMCounty
        End Get
        Set(ByVal value As String)
            _sBMCounty = value
        End Set
    End Property


    Public Property BPracContactName() As String
        Get
            Return _sBPracContactName
        End Get
        Set(ByVal value As String)
            _sBPracContactName = value
        End Set
    End Property
    Public Property BPracAddress1() As String
        Get
            Return _sBPracAddress1
        End Get
        Set(ByVal value As String)
            _sBPracAddress1 = value
        End Set
    End Property
    Public Property BPracAddress2() As String
        Get
            Return _sBPracAddress2
        End Get
        Set(ByVal value As String)
            _sBPracAddress2 = value
        End Set
    End Property
    Public Property BPracCity() As String
        Get
            Return _sBPracCity
        End Get
        Set(ByVal value As String)
            _sBPracCity = value
        End Set
    End Property
    Public Property BPracState() As String
        Get
            Return _sBPracState
        End Get
        Set(ByVal value As String)
            _sBPracState = value
        End Set
    End Property
    Public Property BPracZIP() As String
        Get
            Return _sBPracZIP
        End Get
        Set(ByVal value As String)
            _sBPracZIP = value
        End Set
    End Property

    Public Property BPracCountry() As String            ''Dhruv '' country
        Get
            Return _sBPracCountry
        End Get
        Set(ByVal value As String)
            _sBPracCountry = value
        End Set
    End Property
    Public Property BPracCounty() As String            ''Dhruv '' county
        Get
            Return _sBPracCounty
        End Get
        Set(ByVal value As String)
            _sBPracCounty = value
        End Set
    End Property

    Public Property BMPhone() As String
        Get
            Return _sBMPhone
        End Get
        Set(ByVal value As String)
            _sBMPhone = value
        End Set
    End Property
    Public Property BMFAX() As String
        Get
            Return _sBMFAX
        End Get
        Set(ByVal value As String)
            _sBMFAX = value
        End Set
    End Property
    Public Property BMPager() As String
        Get
            Return _sBMPager
        End Get
        Set(ByVal value As String)
            _sBMPager = value
        End Set
    End Property
    Public Property BMEmail() As String
        Get
            Return _sBMEmail
        End Get
        Set(ByVal value As String)
            _sBMEmail = value
        End Set
    End Property
    Public Property DirectAddress() As String
        Get
            Return _sDirectAddress
        End Get
        Set(ByVal value As String)
            _sDirectAddress = value
        End Set
    End Property

    Public Property BMURL() As String
        Get
            Return _sBMURL
        End Get
        Set(ByVal value As String)
            _sBMURL = value
        End Set
    End Property

    Public Property BPracPhone() As String
        Get
            Return _sBpracPhone
        End Get
        Set(ByVal value As String)
            _sBpracPhone = value
        End Set
    End Property
    Public Property BPracFAX() As String
        Get
            Return _sBpracFAX
        End Get
        Set(ByVal value As String)
            _sBpracFAX = value
        End Set
    End Property
    Public Property BPracPager() As String
        Get
            Return _sBPracPager
        End Get
        Set(ByVal value As String)
            _sBPracPager = value
        End Set
    End Property
    Public Property BPracEmail() As String
        Get
            Return _sBPracEmail
        End Get
        Set(ByVal value As String)
            _sBPracEmail = value
        End Set
    End Property
    Public Property BPracURL() As String
        Get
            Return _sBPracURL
        End Get
        Set(ByVal value As String)
            _sBPracURL = value
        End Set
    End Property

    Public Property SSNno() As String
        Get
            Return _sSSNno
        End Get
        Set(ByVal value As String)
            _sSSNno = value
        End Set
    End Property
    Public Property EmployerID() As String
        Get
            Return _sEmployerID
        End Get
        Set(ByVal value As String)
            _sEmployerID = value
        End Set
    End Property

    Public Property Taxonomy() As String
        Get
            Return _Taxonomy
        End Get
        Set(ByVal value As String)
            _Taxonomy = value
        End Set
    End Property
    Public Property TaxonomyDesc() As String
        Get
            Return _TaxonomyDesc
        End Get
        Set(ByVal value As String)
            _TaxonomyDesc = value
        End Set
    End Property

    Public Property Mobile() As String
        Get
            Return _sMobile
        End Get
        Set(ByVal value As String)
            _sMobile = value
        End Set
    End Property
    Public Property Signature() As Image
        Get
            Return _imgSignature
        End Get
        Set(ByVal value As Image)
            _imgSignature = value
        End Set
    End Property
    Public Property SignWidth() As Integer ''signature Width
        Get
            Return _nSignWidth
        End Get
        Set(ByVal value As Integer)
            _nSignWidth = value
        End Set
    End Property
    Public Property SignFont() As String
        Get
            Return _sSignFont
        End Get
        Set(ByVal value As String)
            _sSignFont = value
        End Set
    End Property
    Public Property SignFontSize() As Integer
        Get
            Return _nSignFontSize
        End Get
        Set(ByVal value As Integer)
            _nSignFontSize = value
        End Set
    End Property
    Public Property MaxSignScale() As Decimal
        Get
            Return _nMaxSignScale
        End Get
        Set(ByVal value As Decimal)
            _nMaxSignScale = value
        End Set
    End Property
    Public Property MinSignScale() As Decimal
        Get
            Return _nMinSignScale
        End Get
        Set(ByVal value As Decimal)
            _nMinSignScale = value
        End Set
    End Property

    Public Property NPI() As String
        Get
            Return _NPI
        End Get
        Set(ByVal value As String)
            _NPI = value
        End Set
    End Property
    Public Property UPIN() As String

        Get
            Return _UPIN
        End Get
        Set(ByVal value As String)
            _UPIN = value
        End Set
    End Property
    Public Property StateMedicalNo() As String

        Get
            Return _StateMedicalNo
        End Get
        Set(ByVal value As String)
            _StateMedicalNo = value
        End Set
    End Property

    Public Property Prefix() As String

        Get
            Return _Prefix
        End Get
        Set(ByVal value As String)
            _Prefix = value
        End Set
    End Property
    Public Property dtDOB() As DateTime
        Get
            Return _dtDOB
        End Get
        Set(ByVal value As DateTime)
            _dtDOB = value
        End Set
    End Property
    Public Property ProviderTypeID() As Int64

        Get
            Return _nProviderTypeID
        End Get
        Set(ByVal value As Int64)
            _nProviderTypeID = value
        End Set
    End Property

    Public Property UserID() As Int64

        Get
            Return _nUserID
        End Get
        Set(ByVal value As Int64)
            _nUserID = value
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
    Public Property Password() As String
        Get
            Return _sPassword
        End Get
        Set(ByVal value As String)
            _sPassword = value
        End Set
    End Property
    Public Property NickName() As String
        Get
            Return _sNickName
        End Get
        Set(ByVal value As String)
            _sNickName = value
        End Set
    End Property

    Public Property ExternalCode() As String
        Get
            Return _sExternalCode
        End Get
        Set(ByVal value As String)
            _sExternalCode = value
        End Set
    End Property

    Public Property ClinicID() As Int64
        Get
            Return _nClinicID
        End Get
        Set(ByVal value As Int64)
            _nClinicID = value
        End Set
    End Property


    'for Equipment
    Public Property Code() As String
        Get
            Return _sCode
        End Get
        Set(ByVal value As String)
            _sCode = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _sDescription
        End Get
        Set(ByVal value As String)
            _sDescription = value
        End Set
    End Property
    Public Property ResourceTypeID() As Int64
        Get
            Return _nResourceTypeID
        End Get
        Set(ByVal value As Int64)
            _nResourceTypeID = value
        End Set
    End Property


    Public Property ComapanyName() As String
        Get
            Return _sComapanyName
        End Get
        Set(ByVal value As String)
            _sComapanyName = value
        End Set
    End Property

    Public Property ComapanyContactName() As String
        Get
            Return _sComapanyContactName
        End Get
        Set(ByVal value As String)
            _sComapanyContactName = value
        End Set
    End Property
    Public Property ComapanyAddress1() As String
        Get
            Return _sComapanyAddress1
        End Get
        Set(ByVal value As String)
            _sComapanyAddress1 = value
        End Set
    End Property
    Public Property ComapanyAddress2() As String
        Get
            Return _sComapanyAddress2
        End Get
        Set(ByVal value As String)
            _sComapanyAddress2 = value
        End Set
    End Property
    Public Property ComapanyCity() As String
        Get
            Return _sComapanyCity
        End Get
        Set(ByVal value As String)
            _sComapanyCity = value
        End Set
    End Property
    Public Property ComapanyState() As String
        Get
            Return _sComapanyState
        End Get
        Set(ByVal value As String)
            _sComapanyState = value
        End Set
    End Property
    Public Property ComapanyZip() As String
        Get
            Return _sComapanyZip
        End Get
        Set(ByVal value As String)
            _sComapanyZip = value
        End Set
    End Property
    Public Property ComapanyCountry() As String '' dhruv ''Country
        Get
            Return _sComapanyCountry
        End Get
        Set(ByVal value As String)
            _sComapanyCountry = value
        End Set
    End Property
    Public Property ComapanyCounty() As String '' dhruv ''County
        Get
            Return _sComapanyCounty
        End Get
        Set(ByVal value As String)
            _sComapanyCounty = value
        End Set
    End Property


    Public Property ComapanyPhone() As String
        Get
            Return _sComapanyPhone
        End Get
        Set(ByVal value As String)
            _sComapanyPhone = value
        End Set
    End Property
    Public Property ComapanyFax() As String
        Get
            Return _sComapanyFax
        End Get
        Set(ByVal value As String)
            _sComapanyFax = value
        End Set
    End Property
    Public Property ComapanyEmail() As String
        Get
            Return _sComapanyEmail
        End Get
        Set(ByVal value As String)
            _sComapanyEmail = value
        End Set
    End Property

    Public Property ComapanyNPI() As String
        Get
            Return _sComapanyNPI
        End Get
        Set(ByVal value As String)
            _sComapanyNPI = value
        End Set
    End Property
    Public Property ComapanyTaxID() As String
        Get
            Return _sComapanyTaxID
        End Get
        Set(ByVal value As String)
            _sComapanyTaxID = value
        End Set
    End Property

    Public Property CompanyTaxonomyCode() As String
        Get
            Return _sCompanyTaxonomyCode
        End Get
        Set(ByVal value As String)
            _sCompanyTaxonomyCode = value
        End Set
    End Property
    '

    Public Property ProviderDetails() As ProviderDetails
        Get
            Return _ProviderDetails
        End Get
        Set(ByVal value As ProviderDetails)
            _ProviderDetails = value
        End Set
    End Property
    Public Property Isblocked() As Boolean
        Get
            Return _bIsblocked
        End Get
        Set(ByVal value As Boolean)
            _bIsblocked = value
        End Set
    End Property


    Public Property ServiceLevel() As String
        Get
            Return _sServiceLevel
        End Get
        Set(ByVal Value As String)
            _sServiceLevel = Value
        End Set
    End Property
    Public Property ActiveStartTime() As DateTime
        Get
            Return _dtActiveStartTime
        End Get
        Set(ByVal Value As DateTime)
            _dtActiveStartTime = Value
        End Set
    End Property
    Public Property ActiveEndTime() As DateTime
        Get
            Return _dtActiveEndTime
        End Get
        Set(ByVal Value As DateTime)
            _dtActiveEndTime = Value
        End Set
    End Property
    Public Property SPI() As String
        Get
            Return _sSPI
        End Get
        Set(ByVal Value As String)
            _sSPI = Value
        End Set
    End Property
    Public Property RootSPI() As String
        Get
            Return _sRootSPI
        End Get
        Set(ByVal Value As String)
            _sRootSPI = Value
        End Set
    End Property

    Public ReadOnly Property ConnectionString() As String
        Get
            Return _sConnectionString
        End Get
    End Property

    Public Property PrescriberLocation() As Boolean
        Get
            Return mPrescriberLocation
        End Get
        Set(ByVal value As Boolean)
            mPrescriberLocation = value
        End Set
    End Property

    Public Property PARequired() As Int16
        Get
            Return _nPARequired
        End Get
        Set(ByVal value As Int16)

            _nPARequired = value
        End Set
    End Property
    Public Property RequireSupervisingProviderforeRx() As Boolean
        Get
            Return _blnRequireSupervisingProviderforeRx
        End Get
        Set(ByVal value As Boolean)
            _blnRequireSupervisingProviderforeRx = value
        End Set
    End Property
    Public Property LicenceKey() As String
    Public Property AUSStatus() As Int16
    Public Property AUSPortalID() As Long


#Region "Provider Physical Address "

    Public Property PhysicalAddContactName() As String
        Get
            Return _sPhysicalAddContactName
        End Get
        Set(ByVal value As String)
            _sPhysicalAddContactName = value
        End Set
    End Property

    Public Property PhysicalAddressline1() As String
        Get
            Return _sPhysicalAddressline1
        End Get
        Set(ByVal value As String)
            _sPhysicalAddressline1 = value
        End Set
    End Property
    Public Property PhysicalAddressline2() As String
        Get
            Return _sPhysicalAddressline2
        End Get
        Set(ByVal value As String)
            _sPhysicalAddressline2 = value
        End Set
    End Property
    Public Property PhysicalCity() As String
        Get
            Return _sPhysicalCity
        End Get
        Set(ByVal value As String)
            _sPhysicalCity = value
        End Set
    End Property
    Public Property PhysicalState() As String
        Get
            Return _sPhysicalState
        End Get
        Set(ByVal value As String)
            _sPhysicalState = value
        End Set
    End Property
    Public Property PhysicalZIP() As String
        Get
            Return _sPhysicalZIP
        End Get
        Set(ByVal value As String)
            _sPhysicalZIP = value
        End Set
    End Property
    Public Property PhysicalAreaCode() As String
        Get
            Return _sPhysicalAreaCode
        End Get
        Set(ByVal value As String)
            _sPhysicalAreaCode = value
        End Set
    End Property
    Public Property PhysicalCountry() As String
        Get
            Return _sPhysicalCountry
        End Get
        Set(ByVal value As String)
            _sPhysicalCountry = value
        End Set
    End Property
    Public Property PhysicalCounty() As String
        Get
            Return _sPhysicalCounty
        End Get
        Set(ByVal value As String)
            _sPhysicalCounty = value
        End Set
    End Property
    Public Property PhysicalPhoneNo() As String
        Get
            Return _sPhysicalPhoneNo
        End Get
        Set(ByVal value As String)
            _sPhysicalPhoneNo = value
        End Set
    End Property
    Public Property PhysicalFAX() As String
        Get
            Return _sPhysicalFAX
        End Get
        Set(ByVal value As String)
            _sPhysicalFAX = value
        End Set
    End Property
    Public Property PhysicalPagerNo() As String
        Get
            Return _sPhysicalPagerNo
        End Get
        Set(ByVal value As String)
            _sPhysicalPagerNo = value
        End Set
    End Property
    Public Property PhysicalEmail() As String
        Get
            Return _sPhysicalEmail
        End Get
        Set(ByVal value As String)
            _sPhysicalEmail = value
        End Set
    End Property
    Public Property PhysicalURL() As String
        Get
            Return _sPhysicalURL
        End Get
        Set(ByVal value As String)
            _sPhysicalURL = value
        End Set
    End Property

#End Region

#Region "Provider Company Physical Address "

    Public Property CompanyPhysicalAddContactName() As String
        Get
            Return _sCompanyPhysicalAddContactName
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalAddContactName = value
        End Set
    End Property

    Public Property CompanyPhysicalAddressline1() As String
        Get
            Return _sCompanyPhysicalAddressline1
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalAddressline1 = value
        End Set
    End Property
    Public Property CompanyPhysicalAddressline2() As String
        Get
            Return _sCompanyPhysicalAddressline2
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalAddressline2 = value
        End Set
    End Property
    Public Property CompanyPhysicalCity() As String
        Get
            Return _sCompanyPhysicalCity
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalCity = value
        End Set
    End Property
    Public Property CompanyPhysicalState() As String
        Get
            Return _sCompanyPhysicalState
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalState = value
        End Set
    End Property
    Public Property CompanyPhysicalZIP() As String
        Get
            Return _sCompanyPhysicalZIP
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalZIP = value
        End Set
    End Property
    Public Property CompanyPhysicalAreaCode() As String
        Get
            Return _sCompanyPhysicalAreaCode
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalAreaCode = value
        End Set
    End Property
    Public Property CompanyPhysicalCountry() As String
        Get
            Return _sCompanyPhysicalCountry
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalCountry = value
        End Set
    End Property
    Public Property CompanyPhysicalCounty() As String
        Get
            Return _sCompanyPhysicalCounty
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalCounty = value
        End Set
    End Property
    Public Property CompanyPhysicalPhoneNo() As String
        Get
            Return _sCompanyPhysicalPhoneNo
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalPhoneNo = value
        End Set
    End Property
    Public Property CompanyPhysicalFAX() As String
        Get
            Return _sCompanyPhysicalFAX
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalFAX = value
        End Set
    End Property
    Public Property CompanyPhysicalPagerNo() As String
        Get
            Return _sCompanyPhysicalPagerNo
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalPagerNo = value
        End Set
    End Property
    Public Property CompanyPhysicalEmail() As String
        Get
            Return _sCompanyPhysicalEmail
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalEmail = value
        End Set
    End Property
    Public Property CompanyPhysicalURL() As String
        Get
            Return _sCompanyPhysicalURL
        End Get
        Set(ByVal value As String)
            _sCompanyPhysicalURL = value
        End Set
    End Property

#End Region

    Public Property AUSID As String
    Public Property ClinicName As String
    Public Property IsDirectMessageEnabled As Boolean
    Public Property IsCIMessageEnabled As Boolean
    Public Property IsServiceLevelDisabled As Boolean
    Public Property IsNewRxEnabled As Boolean
    Public Property IsRefillEnabled As Boolean
    Public Property IsRxChangeEnabled As Boolean
    Public Property IsRxCancelEnabled As Boolean
    Public Property IsRxFillEnabled As Boolean
    Public Property IsControlledSubstance As Boolean
    Public Property IsePAEnabled As Boolean
    Public Property DatabaseName As String
    Public Property gloSuiteVersion As String
    Public Property CreatedDateTime As Date
    Public Property ModifiedDateTime As Date
    Public Property IsDirectAddressExists As Boolean
    Public Property IsPDREnabled As Boolean
    Public Property IsPDMPEnabled As Boolean
#End Region



    Public Enum ProviderIdentificationType
        None = 0
        Medicare = 1
        Medicaid = 2
        Other = 3
    End Enum

    Public Enum PriorAuthorizationRequired
        No = 0
        Always = 1
        UsePlanSetting = 2
    End Enum

#Region "constructor"

    Public Sub New()

    End Sub
    Public Sub New(ByVal sConnectionString As String)
        _sConnectionString = sConnectionString
    End Sub
#End Region
    '---------------------------

#Region " Public Functions"



    Public Function Fill_Providers() As Collection
        Dim clProviders As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanProviderName"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            While objSQLDataReader.Read
                clProviders.Add(objSQLDataReader.Item(0))
            End While
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return clProviders
    End Function

    Public Function ScanProvider(ByVal nProviderID As Int64) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        '  Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanProvider"
        objCmd.Connection = objCon
        Dim objParaProviderName As New SqlParameter
        With objParaProviderName
            .ParameterName = "@nProviderID"
            .Value = nProviderID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaProviderName)
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function

    Public Function RetrieveProviderName(ByVal nProviderID As String) As String
        Dim strProviderName As String = ""
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        '     Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveProviderName"
        objCmd.Connection = objCon
        Dim objParaProviderID As New SqlParameter
        With objParaProviderID
            .ParameterName = "@ProviderID"
            .Value = nProviderID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaProviderID)
        objCmd.Connection = objCon
        objCon.Open()
        If IsNothing(objCmd.ExecuteScalar) = False Then
            strProviderName = objCmd.ExecuteScalar
        End If
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return strProviderName
    End Function

    Public Function RetrieveProviderID(ByVal strProviderName As String) As Int64
        Dim nProviderID As Int64 = 0
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        ' Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveProviderID"
        objCmd.Connection = objCon
        Dim objParaProviderName As New SqlParameter
        With objParaProviderName
            .ParameterName = "@ProviderName"
            .Value = strProviderName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProviderName)
        objCmd.Connection = objCon
        objCon.Open()
        If IsNothing(objCmd.ExecuteScalar) = False Then
            nProviderID = objCmd.ExecuteScalar
        End If
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return nProviderID
    End Function


    'sarika 11th sept 07
    Public Function CheckProviderExists(ByVal strProviderName As String, Optional ByVal nProviderID As Int64 = 0) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = _sConnectionString
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_CheckProviderExists"
        Dim objParaProviderName As New SqlParameter
        With objParaProviderName
            .ParameterName = "@ProviderName"
            .Value = strProviderName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProviderName)

        If nProviderID <> 0 Then
            Dim objParaProviderID As New SqlParameter
            With objParaProviderID
                .ParameterName = "@ProviderID"
                .Value = nProviderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaProviderID)
        End If
        objCmd.Connection = objCon
        Dim nCount As Integer
        objCon.Open()
        nCount = objCmd.ExecuteScalar
        objCon.Close()
        objCon = Nothing
        If nCount = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function CheckLicenseIsInUse(ByVal strLicenseKey As String, Optional ByVal nProviderID As Int64 = 0) As Boolean
        ''Dim objCon As New SqlConnection
        ''objCon.ConnectionString = _sConnectionString
        ''Dim objCmd As New SqlCommand
        ''objCmd.CommandType = CommandType.StoredProcedure
        ''objCmd.CommandText = "gsp_CheckLicenseIsInUse"
        ''Dim objParaProviderName As New SqlParameter
        ''With objParaProviderName
        ''    .ParameterName = "@LicenseKey"
        ''    .Value = strLicenseKey
        ''    .Direction = ParameterDirection.Input
        ''    .SqlDbType = SqlDbType.VarChar
        ''End With
        ''objCmd.Parameters.Add(objParaProviderName)

        ''If nProviderID <> 0 Then
        ''    Dim objParaProviderID As New SqlParameter
        ''    With objParaProviderID
        ''        .ParameterName = "@ProviderID"
        ''        .Value = nProviderID
        ''        .Direction = ParameterDirection.Input
        ''        .SqlDbType = SqlDbType.BigInt
        ''    End With
        ''    objCmd.Parameters.Add(objParaProviderID)
        ''End If

        ''objCmd.Connection = objCon
        ''Dim nCount As Integer
        ''objCon.Open()
        ''nCount = objCmd.ExecuteScalar
        ''objCon.Close()
        ''objCon = Nothing
        ''If nCount = 0 Then
        ''    Return False
        ''Else
        ''    Return True
        ''End If
        Return True
    End Function


#Region "Dump Code For Provider Association"
    'Check If Provider Is Active Senior Provider For Jr Provider
    'Dim query1 As String = "select * from Rx_ProviderAssociation where nPatientID = 0 And nVisitID = 0 And " _
    '                        & "nProviderID=" & ProviderID & " And nJuniorProviderID in ( "
    'For i As Integer = 0 To dtProvider.Rows.Count - 1
    '    If i = dtProvider.Rows.Count - 1 Then
    '        query1 = query1 + dtProvider.Rows(i)(0).ToString() + ")"
    '    Else
    '        query1 = query1 + dtProvider.Rows(i)(0).ToString() + ","
    '    End If
    'Next
    'Dim cmd1 As New SqlCommand(query1, con)
    'Dim adp1 As New SqlDataAdapter(cmd1)
    'Dim dtProvider1 As New DataTable
    'adp1.Fill(dtProvider1)
    'If IsNothing(dtProvider1) = False And dtProvider1.Rows.Count > 0 Then
    '    isCheckedAssociate = True
    'Else
    '    isCheckedAssociate = False
    'End If

    'Dim jrId As String = ""
    'For i As Integer = 0 To dtProvider.Rows.Count - 1
    '    If i = dtProvider.Rows.Count - 1 Then
    '        jrId = jrId + dtProvider.Rows(i)(0).ToString() + ")"
    '    Else
    '        jrId = jrId + dtProvider.Rows(i)(0).ToString() + ","
    '    End If
    'Next
    'Dim query1 As String = " delete from ProviderSettings where sSettingsType = 'ProviderSeniorAssignment' and " _
    '    & "nOthersID = " & ProviderID & " And nProviderID in ( " & jrId & ";" _
    '    & "delete from Rx_ProviderAssociation where nProviderID=" & ProviderID & " and nPatientID=0 and nVisitID=0 " _
    '    & "and nJuniorProviderID in ( " & jrId
#End Region


    'Yatin 04/17/2012
    Public Function CheckAssociation(ByVal ProviderID As Int64) As Boolean

        Dim dt As New DataTable()
        Dim isCheckedAssociate As Boolean = False
        dt = GetDataFromDb(ProviderID, 0, 2)

        If IsNothing(dt) = False Then
            If dt.Rows.Count > 0 Then
                isCheckedAssociate = True
            ElseIf dt.Rows.Count = 0 Then
                isCheckedAssociate = False
            End If
        Else
            isCheckedAssociate = False
        End If
        Return isCheckedAssociate



        'Try
        '    Dim con As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
        '    Dim query As String

        '    If Not isJunior Then
        '        query = " select nProviderID from ProviderSettings where sSettingsType = 'ProviderSeniorAssignment' and " _
        '                & "nOthersID = " & ProviderID
        '    Else
        '        query = "select nOthersID from ProviderSettings where sSettingsType = 'ProviderSeniorAssignment' and " _
        '                & " nProviderID = " & ProviderID
        '    End If

        '    Dim cmd As New SqlCommand(query, con)
        '    Dim adp As New SqlDataAdapter(cmd)
        '    Dim dtProvider As New DataTable
        '    adp.Fill(dtProvider)

        '    If IsNothing(dtProvider) = False Then
        '        If dtProvider.Rows.Count > 0 Then
        '            isCheckedAssociate = True
        '        End If

        '    ElseIf IsNothing(dtProvider) = False Then
        '        If dtProvider.Rows.Count = 0 Then
        '            isCheckedAssociate = True
        '        End If
        '    End If
        'Catch ex As Exception
        '    _sErrorMessage = ex.Message
        'End Try
        'Return isCheckedAssociate

    End Function


    ''Yatin 04/17/2012
    'Public Function DeleteAssociation(ByVal ProviderID As Int64, ByVal isJunior As Boolean) As Boolean
    '    Dim con As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
    '    con.Open()
    '    Dim tx As SqlTransaction = con.BeginTransaction()
    '    Dim cmd1 As SqlCommand = con.CreateCommand()
    '    cmd1.Transaction = tx
    '    Try

    '        Dim query1 As String = ""
    '        If Not isJunior Then
    '            query1 = " delete from ProviderSettings where sSettingsType = 'ProviderSeniorAssignment' and " _
    '                     & "nOthersID = " & ProviderID & ";" _
    '                     & "delete from Rx_ProviderAssociation where nProviderID=" & ProviderID & " and nPatientID=0 and nVisitID=0 "
    '        Else
    '            query1 = "delete from ProviderSettings where sSettingsType = 'ProviderSeniorAssignment' and  " _
    '                     & "nProviderID = " & ProviderID & "; delete from Rx_ProviderAssociation where nJuniorProviderID =" & ProviderID & "  " _
    '                     & "and nPatientID=0 and nVisitID=0  "
    '        End If

    '        cmd1.CommandText = query1
    '        cmd1.ExecuteNonQuery()
    '        tx.Commit()
    '    Catch ex As SqlException
    '        tx.Rollback()
    '        _sErrorMessage = ex.Message
    '    Finally
    '        con.Close()
    '    End Try

    'End Function

    Public Function GetDataFromDb(ByVal srProviderID As Int64, ByVal jrProviderID As Int64, ByVal sFlag As Int32) As DataTable
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ProviderAssociation"

            Dim objParasrProviderId As New SqlParameter
            With objParasrProviderId
                .ParameterName = "@srProviderId"
                .Direction = ParameterDirection.Input
                .Value = srProviderID
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParasrProviderId)

            Dim objParajrProviderId As New SqlParameter
            With objParajrProviderId
                .ParameterName = "@jrProviderId"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
                .Value = jrProviderID
            End With
            objCmd.Parameters.Add(objParajrProviderId)

            Dim objParasFlag As New SqlParameter
            With objParasFlag
                .ParameterName = "@sFlag"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
                .Value = sFlag
            End With
            objCmd.Parameters.Add(objParasFlag)


            If sFlag = 1 Or sFlag = 2 Or sFlag = 4 Then
                objCmd.Connection = objCon
                objCon.Open()
                Dim objDA As New SqlDataAdapter(objCmd)
                Dim dsData As New DataSet
                objDA.Fill(dsData)
                objCon.Close()
                If IsNothing(dsData) = False Then
                    If dsData.Tables.Count > 0 Then
                        Return dsData.Tables(0)
                    Else
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If

            ElseIf sFlag = 3 Then
                objCmd.Connection = objCon
                objCon.Open()
                objCmd.ExecuteNonQuery()
                objCon.Close()
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function



    Public Function Block_Unblock_Provider(ByVal ProviderID As Int64, ByVal blockProvider As Boolean) As Boolean
        _sErrorMessage = ""

        Try


            'Yatin 04/17/2012
            Dim isOkToBlock As Boolean = False
            Dim isAssociated As Boolean = False
            Dim isJunior As Boolean = False
            Dim dtProvider As New DataTable
            dtProvider = GetDataFromDb(ProviderID, 0, 4)
            If IsNothing(dtProvider) = False Then
                If dtProvider.Rows.Count > 0 Then
                    If CType(dtProvider.Rows(0)(0).ToString(), Boolean) Then
                        isJunior = True
                    End If
                End If
            End If

            If blockProvider Then
                If CheckAssociation(ProviderID) Then
                    Dim Msg As String = ""
                    If Not isJunior Then
                        Msg = "This provider is associated with at least one junior provider. If you block this provider then his/her junior provider associations will also be removed automatically. Are you sure you want to block this provider?"
                    Else
                        Msg = "This provider is associated with at least one senior provider. If you block this provider then his/her senior provider associations will also be removed automatically. Are you sure you want to block this provider?"
                    End If
                    Dim dResult As DialogResult = MessageBox.Show(Msg, "Block Provider", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If dResult.ToString() = "Yes" Then
                        GetDataFromDb(ProviderID, 0, 3) 'Delete Association
                        'DeleteAssociation(ProviderID, isJunior)
                        isAssociated = True
                        isOkToBlock = True
                    ElseIf dResult.ToString() = "No" Then
                        isOkToBlock = False
                    End If
                Else
                    Dim dResult As DialogResult = MessageBox.Show("Are you sure you want to block this provider?", "Block Provider", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If dResult.ToString() = "Yes" Then
                        isOkToBlock = True
                    ElseIf dResult.ToString() = "No" Then
                        isOkToBlock = False
                    End If
                End If
            Else
                isOkToBlock = True
            End If




            '''
            If isOkToBlock Then
                'Try
                Dim conn As New SqlConnection
                conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
                conn.Open()
                ' Start a local transaction
                Dim tx As SqlTransaction = conn.BeginTransaction()
                ' By default, commands run in auto-commit mode; 
                Dim cmd1 As SqlCommand = conn.CreateCommand()
                ' You may create multiple commands on the same connection
                Dim cmd2 As SqlCommand = conn.CreateCommand()
                ' To enlist a command in a transaction, set the Transaction property
                cmd1.Transaction = tx
                cmd2.Transaction = tx
                Dim nausStatus As Int16
                If blockProvider = False Then
                    '' verify License key
                    nausStatus = enmAUSStatus.Active.GetHashCode
                Else
                    nausStatus = enmAUSStatus.BlockedProviders.GetHashCode
                End If
                Try
                    Dim query As String = " UPDATE User_MST SET nBlockStatus = '" & blockProvider & "' WHERE nProviderID = " & ProviderID & "; " _
                                        & " UPDATE Provider_MST SET bIsblocked = '" & blockProvider & "' , nAusStatus = " & nausStatus & "  WHERE nProviderID = " & ProviderID & ""
                    '' COMMENT BY SUDHIR 20090515 '
                    'cmd1.CommandText = " DELETE FROM User_MST WHERE nproviderID = " & ProviderID
                    cmd1.CommandText = query
                    cmd1.ExecuteNonQuery()
                    'cmd2.CommandText = " DELETE FROM provider_mst WHERE nproviderID =  " & ProviderID
                    'cmd2.ExecuteNonQuery()
                    ' Commit the changes to disk if everything above succeeded
                    tx.Commit()
                    If isAssociated Then
                        Dim frmJrSrAso As New frmJrSrAssociation()
                        frmJrSrAso.ShowDialog()
                    End If
                    Return True
                Catch ex As SqlException
                    tx.Rollback()
                    _sErrorMessage = ex.Message
                    Return False
                Finally
                    conn.Close()
                End Try


            End If

            '**********
            '' ''Catch ex As Exception
            '' ''    _sErrorMessage = ex.Message
            '' ''    Return False
            '' ''End Try
            '******22 Oct 2007 5.00 PM Bug # 331
        Catch ex As Exception

        End Try
    End Function

    '' COMMENT BY SUDHIR 20090625 '' 
    'Public Sub SearchProvider(ByVal strProviderName As String)
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = _sConnectionString
    '    Dim objCmd As New SqlCommand
    '    Dim objSQLDataReader As SqlDataReader
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_ScanProviderDetails"

    '    Dim objParaProviderName As New SqlParameter
    '    With objParaProviderName
    '        .ParameterName = "@ProviderName"
    '        .Value = strProviderName
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaProviderName)
    '    objCmd.Connection = objCon
    '    objCon.Open()
    '    objSQLDataReader = objCmd.ExecuteReader()
    '    If objSQLDataReader.HasRows = True Then
    '        objSQLDataReader.Read()
    '        If IsDBNull(objSQLDataReader.Item("ProviderID")) = False Then
    '            _nProviderID = objSQLDataReader.Item("ProviderID")
    '        Else
    '            _nProviderID = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("FirstName")) = False Then
    '            _sFirstName = objSQLDataReader.Item("FirstName")
    '        Else
    '            _sFirstName = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("MiddleName")) = False Then
    '            _sMiddleName = objSQLDataReader.Item("MiddleName")
    '        Else
    '            _sMiddleName = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("LastName")) = False Then
    '            _sLastName = objSQLDataReader.Item("LastName")
    '        Else
    '            _sLastName = ""
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("Gender")) = False Then
    '            _sGender = objSQLDataReader.Item("Gender")
    '        Else
    '            _sGender = ""
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("DEA")) = False Then
    '            _sDEA = objSQLDataReader.Item("DEA")
    '        Else
    '            _sDEA = ""
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("Address")) = False Then
    '            _sAddress = objSQLDataReader.Item("Address")
    '        Else
    '            _sAddress = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("Street")) = False Then
    '            _sStreet = objSQLDataReader.Item("Street")
    '        Else
    '            _sStreet = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("City")) = False Then
    '            _sCity = objSQLDataReader.Item("City")
    '        Else
    '            _sCity = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("State")) = False Then
    '            _sState = objSQLDataReader.Item("State")
    '        Else
    '            _sState = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("ZIP")) = False Then
    '            _sZIP = objSQLDataReader.Item("ZIP")
    '        Else
    '            _sZIP = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("PhoneNo")) = False Then
    '            _sPhone = objSQLDataReader.Item("PhoneNo")
    '        Else
    '            _sPhone = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("MobileNo")) = False Then
    '            _sMobile = objSQLDataReader.Item("MobileNo")
    '        Else
    '            _sMobile = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("PagerNo")) = False Then
    '            _sPager = objSQLDataReader.Item("PagerNo")
    '        Else
    '            _sPager = ""
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("FAX")) = False Then
    '            _sFAX = objSQLDataReader.Item("FAX")
    '        Else
    '            _sFAX = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("Email")) = False Then
    '            _sEmail = objSQLDataReader.Item("Email")
    '        Else
    '            _sEmail = ""
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("URL")) = False Then
    '            _sURL = objSQLDataReader.Item("URL")
    '        Else
    '            _sURL = ""
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("ProviderTypeID")) = False Then
    '            _nProviderTypeID = objSQLDataReader.Item("ProviderTypeID")
    '        Else
    '            _nProviderTypeID = 0
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("sNPI")) = False Then
    '            _NPI = objSQLDataReader.Item("sNPI")
    '        Else
    '            _NPI = ""
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("sUPIN")) = False Then
    '            _UPIN = objSQLDataReader.Item("sUPIN")
    '        Else
    '            _UPIN = ""
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("sMedicalLicenseNo")) = False Then
    '            _StateMedicalNo = objSQLDataReader.Item("sMedicalLicenseNo")
    '        Else
    '            _StateMedicalNo = ""
    '        End If

    '        'sarika 7th june 07
    '        '------------------------
    '        If IsDBNull(objSQLDataReader.Item("Prefix")) = False Then
    '            _Prefix = objSQLDataReader.Item("Prefix")
    '        Else
    '            _Prefix = ""
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("ServiceLevelCode")) = False Then
    '            _sServiceLevel = objSQLDataReader.Item("ServiceLevelCode")
    '        Else
    '            _sServiceLevel = String.Empty
    '        End If
    '        If IsDBNull(objSQLDataReader.Item("ActiveStartTime")) = False Then
    '            _dtActiveStartTime = objSQLDataReader.Item("ActiveStartTime")
    '        Else
    '            _dtActiveStartTime = Nothing
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("ActiveEndTime")) = False Then
    '            _dtActiveEndTime = objSQLDataReader.Item("ActiveEndTime")
    '        Else
    '            _dtActiveEndTime = Nothing
    '        End If

    '        If IsDBNull(objSQLDataReader.Item("SPI")) = False Then
    '            _sSPI = objSQLDataReader.Item("SPI")
    '        Else
    '            _sSPI = String.Empty
    '        End If
    '        '------------------------

    '        If IsDBNull(objSQLDataReader.Item("Signature")) = False Then
    '            Dim arrPicture() As Byte = CType(objSQLDataReader.Item("Signature"), Byte())
    '            Dim ms As New MemoryStream(arrPicture)
    '            _imgSignature = Image.FromStream(ms)
    '            ms.Close()
    '        End If

    '    End If
    '    objCon.Close()
    '    objCmd = Nothing
    '    objCon = Nothing
    'End Sub
    '' END SUDHIR COMMENT ''

    'sarika 17th sept 07
    Public Function GetProviderType(ByVal nProviderID As Int64) As String
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As SqlCommand

        Try
            objCon.Open()

            objCmd = New SqlCommand
            objCmd.Connection = objCon
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "select isnull(sProviderType,'') as sProviderType from ProviderType_mst where nProviderTypeID = " & nProviderID

            Dim ProviderType As String = objCmd.ExecuteScalar()

            Return ProviderType
        Catch ex As Exception
            Return ""
        Finally
            objCon.Close()
        End Try
    End Function

    Public Function UpdateSPIDetails(ByVal AddDefaultRoles As Boolean) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDBParameters.Add("@nProviderID", _nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sDirectAddress", _sDirectAddress, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sSPI", _sSPI, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sServiceLevel", _sServiceLevel, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@bAddRoles", Convert.ToInt16(AddDefaultRoles), ParameterDirection.Input, SqlDbType.Bit)

            oDB.Connect(False)
            oDB.Execute("gsp_InUpServiceCode", oDBParameters)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Public Function GetProviders(ByVal activeProviders As Boolean) As DataTable
        Try
            Dim con As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
            Dim query As String
            query = " SELECT nProviderID, ISNULL(sFirstName,'') + ' ' + ISNULL(sMiddleName,'') + ' ' + ISNULL(sLastName,'') AS ProviderName " _
         & " FROM Provider_MST WHERE ISNULL(bIsblocked,'false') = '" & (Not activeProviders) & "' ORDER BY ProviderName"
            'query = "SELECT isnull(nProviderID,0) as nProviderID,(ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST WHERE ISNULL(bIsblocked,'false') = '" & (Not activeProviders) & "' ORDER BY ProviderName"

            Dim cmd As New SqlCommand(query, con)
            Dim adp As New SqlDataAdapter(cmd)
            Dim dtProvider As New DataTable
            adp.Fill(dtProvider)
            If IsNothing(dtProvider) = False Then
                If dtProvider.Rows.Count > 0 Then
                    Return dtProvider
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Public Function GetProviderLicenseDetail(ByVal ProviderID As Long) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim oParameter As SqlParameter = Nothing
        Try
            objCmd.CommandType = CommandType.StoredProcedure

            objCmd.CommandText = "GetProviderLicenseDetails"

            oParameter = New SqlParameter
            With oParameter
                .ParameterName = "@nProviderID"
                .Direction = ParameterDirection.Input
                .Value = ProviderID
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(oParameter)

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dsData As New DataSet
            objDA.Fill(dsData)
            objCon.Close()
            If IsNothing(dsData) = False Then
                If dsData.Tables.Count > 0 Then
                    Return dsData.Tables(0)
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''Added by Mayuri:20100517-case No:#4942
   

    Public Function GetProvider_withENM(ByVal Ausstatus As enmAUSStatus) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim oParameter As SqlParameter = Nothing
        Try
            objCmd.CommandType = CommandType.StoredProcedure

            objCmd.CommandText = "GetProvidersByCategory"


            oParameter = New SqlParameter
            With oParameter
                .ParameterName = "@AusStatus"
                .Direction = ParameterDirection.Input
                .Value = Ausstatus.GetHashCode
                .SqlDbType = SqlDbType.SmallInt
            End With
            objCmd.Parameters.Add(oParameter)

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dsData As New DataSet
            objDA.Fill(dsData)
            objCon.Close()
            If IsNothing(dsData) = False Then
                If dsData.Tables.Count > 0 Then
                    Return dsData.Tables(0)
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function IsDemoNPI(ByVal sNpi As String) As Boolean
        '  Dim oProvider As New clsProvider(gstrConnectionString)
        Dim isValid As Boolean
        ''Try
        ''    Dim objEncryption As New clsEncryption
        ''    Dim sTempNpis As String = objEncryption.DecryptFromBase64String(gstrDemoNPIs, "triarq@1981!")


        ''    Dim NPIs() As String = sTempNpis.Split(",")
        ''    Dim result As Integer = (From demonpi In NPIs Where demonpi.Trim = sNpi.Trim Select demonpi).Count()

        ''    If result > 0 Then
        ''        isValid = True
        ''    End If

        ''Catch ex As Exception
        ''    isValid = False
        ''Finally
        ''    ' oProvider = Nothing
        ''End Try
        Return isValid
    End Function
    Public Function ValidateLicenseKey(ByVal strLicensekey As String, ByVal strFirstName As String, ByVal strMiddleName As String, ByVal strLastName As String, ByVal ClinicExternalCode As String, ByVal ProviderID As Int64, ByVal AUSPortalID As Int64, Optional ByVal dtLicenceData As DataTable = Nothing) As String
        Dim message As String = String.Empty
        ''Try
        ''    If gstrgloAusPortalURL <> "" Then
        ''        Using ausService As New AUSLicenseService.gloAUSService
        ''            ausService.Credentials = System.Net.CredentialCache.DefaultCredentials
        ''            ausService.Url = gstrgloAusPortalURL
        ''            Dim _Key As String = ausService.Login("sarika@ophit.net", "spX12ss@!!21nasik")
        ''            message = ausService.ValidateLicenseKey(strLicensekey, strFirstName, strMiddleName, strLastName, ClinicExternalCode, ProviderID, AUSPortalID, _Key)
        ''        End Using
        ''    Else
        ''        MessageBox.Show("AUS Portal URL Not set in gloEMRAdmin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''    message = ""
        ''End Try
        Return message
    End Function

    Public Function SendProviderOnAUS(ByVal dtLicenceData As DataTable) As Boolean
        ''Dim message As Boolean = False
        ''Dim _ResponseData As Byte()
        ''Try
        ''    If gstrgloAusPortalURL <> "" Then
        ''        Using ausService As New AUSLicenseService.gloAUSService
        ''            ausService.Credentials = System.Net.CredentialCache.DefaultCredentials
        ''            ausService.Url = gstrgloAusPortalURL
        ''            Dim _Key As String = ausService.Login("sarika@ophit.net", "spX12ss@!!21nasik")
        ''            _ResponseData = ReadDatatableToxmlFileBytes(dtLicenceData)
        ''            If Not IsNothing(_ResponseData) Then
        ''                message = ausService.SendProviderLicenseData(_ResponseData, _Key)
        ''            End If

        ''        End Using
        ''    Else
        ''        MessageBox.Show("AUS Portal URL Not set in gloEMRAdmin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''    message = ""
        ''Finally
        ''    _ResponseData = Nothing
        ''End Try
        ''Return message
    End Function
    Private Function ReadDatatableToxmlFileBytes(ByVal dtData As DataTable) As Byte()
        ''Dim bResponseData As Byte() = Nothing
        ''Dim strResponseXml As String = String.Empty
        ''Try
        ''    Using dsProvider As New DataSet
        ''        If dtData.Rows.Count > 0 Then
        ''            dsProvider.Tables.Add(dtData.Copy())
        ''        End If
        ''        If dsProvider IsNot Nothing Then
        ''            Using oLicense As New gloAUSLibrary.Class.clsgloLicence
        ''                If dsProvider.Tables.Count > 0 Then

        ''                    strResponseXml = oLicense.GetFileName(gloAUSLibrary.enumFileType.XMLFile)
        ''                    dsProvider.WriteXml(strResponseXml)

        ''                    If strResponseXml IsNot Nothing Then
        ''                        bResponseData = oLicense.ConvertFiletoBinary(strResponseXml)
        ''                    End If
        ''                End If
        ''            End Using
        ''        End If
        ''    End Using

        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''Finally
        ''    If File.Exists(strResponseXml) = True Then
        ''        File.Delete(strResponseXml)
        ''    End If
        ''End Try
        ''Return bResponseData
    End Function
    Public Function AssociatePatients(ByVal old_ProviderID As Int64, ByVal new_ProviderID As Int64) As Boolean
        Try
            Dim con As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
            Dim cmd As New SqlCommand("UPDATE Patient SET nProviderID = " & new_ProviderID & " WHERE nProviderID = " & old_ProviderID & "", con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    '' SUDHIR 20090624 '' 
    Public Function SaveProvider(ByVal _ISDEMOLicensce As Boolean, ByVal ProviderUserID As Int64) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        oDB.Connect(False)
        Dim Output As Object
        Dim _IsModify As Boolean = False
        Try

            If _nProviderID > 0 Then
                _IsModify = True
            End If

            oDBParameters.Add("@ProviderID", _nProviderID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBParameters.Add("@FirstName", _sFirstName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@MiddleName", _sMiddleName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@LastName", _sLastName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@Gender", _sGender, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@DEA", _sDEA, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@NADEAN", _sNADEAN, ParameterDirection.Input, SqlDbType.VarChar)

            '''''''''''''''''''-------------we will save the business infor of the patient since the below fields are used in Sure script validation in EMR     
            oDBParameters.Add("@sAddress", _sBMAddress1, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sStreet", _sBMAddress2, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCity", _sBMCity, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sState", _sBMState, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sZIP", _sBMZIP, ParameterDirection.Input, SqlDbType.VarChar)


            oDBParameters.Add("@sPhoneNo", _sBMPhone, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sFAX", _sBMFAX, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPagerNo", _sBMPager, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sEmail", _sBMEmail, ParameterDirection.Input, SqlDbType.VarChar)
            'If _sSPI <> "" Then
            '    oDBParameters.Add("@sDirectAddress", _sDirectAddress, ParameterDirection.Input, SqlDbType.VarChar)  ''Direct Address
            'Else
            oDBParameters.Add("@sDirectAddress", "", ParameterDirection.Input, SqlDbType.VarChar)  ''Direct Address
            'End If

            oDBParameters.Add("@sURL", _sBMURL, ParameterDirection.Input, SqlDbType.VarChar)
            '''''''''''''''''''-------------

            oDBParameters.Add("@BMContactName", _sBMContactName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BMAddress1", _sBMAddress1, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BMAddress2", _sBMAddress2, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BMCity", _sBMCity, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BMState", _sBMState, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BMZIP", _sBMZIP, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BMCountry", _sBMCountry, ParameterDirection.Input, SqlDbType.VarChar)        ''Country
            oDBParameters.Add("@BMCounty", _sBMCounty, ParameterDirection.Input, SqlDbType.VarChar)          ''County
            oDBParameters.Add("@BMAreaCode", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar)      ''Area Code

            oDBParameters.Add("@BPracContactName", _sBPracContactName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BPracAddress1", _sBPracAddress1, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BPracAddress2", _sBPracAddress2, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BPracCity", _sBPracCity, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BPracState", _sBPracState, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BPracZIP", _sBPracZIP, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BPracCountry", _sBPracCountry, ParameterDirection.Input, SqlDbType.VarChar)    ''Country
            oDBParameters.Add("@BPracCounty", _sBPracCounty, ParameterDirection.Input, SqlDbType.VarChar)       ''County
            oDBParameters.Add("@BPracAreaCode", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar)           ''Prac Area Code


            oDBParameters.Add("@BmPhoneNo", _sBMPhone, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BMFAX", _sBMFAX, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BMPagerNo", _sBMPager, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BMEmail", _sBMEmail, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BMURL", _sBMURL, ParameterDirection.Input, SqlDbType.VarChar)

            oDBParameters.Add("@BPracPhoneNo", _sBpracPhone, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BPracFAX", _sBpracFAX, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BPracPagerNo", _sBPracPager, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BPracEmail", _sBPracEmail, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@BPracURL", _sBPracURL, ParameterDirection.Input, SqlDbType.VarChar)


            oDBParameters.Add("@MobileNo", _sMobile, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@Taxonomy", _Taxonomy, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@TaxonomyDesc", _TaxonomyDesc, ParameterDirection.Input, SqlDbType.VarChar)

            oDBParameters.Add("@sSSN", _sSSNno, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sEmployerID", _sEmployerID, ParameterDirection.Input, SqlDbType.VarChar)

            'if signature image is null then it would take null value by default else pass the value
            If _imgSignature IsNot Nothing Then
                Dim ms As New MemoryStream()
                _imgSignature.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp)
                Dim arrImage As Byte() = ms.GetBuffer()
                ms.Close()
                oDBParameters.Add("@Signature", arrImage, ParameterDirection.Input, SqlDbType.Image)
            End If

            oDBParameters.Add("@ProviderTypeID", _nProviderTypeID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sNPI", _NPI, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sUPIN", _UPIN, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMedicalLicenseNo", _StateMedicalNo, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sExternalCode", _sExternalCode, ParameterDirection.Input, SqlDbType.VarChar) '' External Code ''
            oDBParameters.Add("@Prefix", _Prefix, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@Suffix", _sSuffix, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyName", _sComapanyName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyAddressline1", _sComapanyAddress1, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyAddressline2", _sComapanyAddress2, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyCity", _sComapanyCity, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyState", _sComapanyState, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyZIP", _sComapanyZip, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyCountry", _sComapanyCountry, ParameterDirection.Input, SqlDbType.VarChar)       ''country
            oDBParameters.Add("@sCompanyCounty", _sComapanyCounty, ParameterDirection.Input, SqlDbType.VarChar)         ''county
            oDBParameters.Add("@sCompanyAreaCode", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar)               ''Company Area Code

            oDBParameters.Add("@sCompanyPhone", _sComapanyPhone, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyContactName", _sComapanyContactName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyFax", _sComapanyFax, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyEmail", _sComapanyEmail, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyNPI", _sComapanyNPI, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyTaxID", _sComapanyTaxID, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyTaxonomyCode", _sCompanyTaxonomyCode, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@MachineID", mdlGeneral.GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt)
            If _sSPI <> "" Then
                oDBParameters.Add("@sSPI", _sSPI, ParameterDirection.Input, SqlDbType.VarChar)
            End If

            'If _sServiceLevel = "" Then
            '    _sServiceLevel = "0000000000000011"
            'End If


            If _ISDEMOLicensce = False Then
                If LicenceKey.Trim = "" Then
                    oDBParameters.Add("@bIsblocked", 1, ParameterDirection.Input, SqlDbType.Bit)
                    If AUSStatus = enmAUSStatus.BlockedProviders.GetHashCode Then
                        oDBParameters.Add("@sServiceLevel", "0000000000000000", ParameterDirection.Input, SqlDbType.VarChar)
                        oDBParameters.Add("@AUSStatus", enmAUSStatus.BlockedProviders.GetHashCode, ParameterDirection.Input, SqlDbType.VarChar)
                    Else
                        oDBParameters.Add("@sServiceLevel", _sServiceLevel, ParameterDirection.Input, SqlDbType.VarChar)
                        oDBParameters.Add("@AUSStatus", enmAUSStatus.PendingForLicense.GetHashCode, ParameterDirection.Input, SqlDbType.VarChar)
                    End If
                Else
                    If AUSStatus = enmAUSStatus.BlockedProviders.GetHashCode Then
                        oDBParameters.Add("@bIsblocked", 1, ParameterDirection.Input, SqlDbType.Bit)
                        oDBParameters.Add("@AUSStatus", enmAUSStatus.BlockedProviders.GetHashCode, ParameterDirection.Input, SqlDbType.VarChar)
                    ElseIf AUSStatus = enmAUSStatus.DisabledProviders.GetHashCode Then
                        oDBParameters.Add("@bIsblocked", 1, ParameterDirection.Input, SqlDbType.Bit)
                        oDBParameters.Add("@AUSStatus", enmAUSStatus.DisabledProviders.GetHashCode, ParameterDirection.Input, SqlDbType.VarChar)
                    Else
                        oDBParameters.Add("@bIsblocked", 0, ParameterDirection.Input, SqlDbType.Bit)
                        oDBParameters.Add("@AUSStatus", enmAUSStatus.Active.GetHashCode, ParameterDirection.Input, SqlDbType.VarChar)
                    End If
                    oDBParameters.Add("@sServiceLevel", "0000000000000000", ParameterDirection.Input, SqlDbType.VarChar)
                End If
            Else
                If AUSStatus = enmAUSStatus.BlockedProviders.GetHashCode Then
                    oDBParameters.Add("@bIsblocked", 1, ParameterDirection.Input, SqlDbType.Bit)
                    oDBParameters.Add("@AUSStatus", enmAUSStatus.BlockedProviders.GetHashCode, ParameterDirection.Input, SqlDbType.VarChar)
                Else
                    oDBParameters.Add("@bIsblocked", 0, ParameterDirection.Input, SqlDbType.Bit)
                    oDBParameters.Add("@AUSStatus", enmAUSStatus.Active.GetHashCode, ParameterDirection.Input, SqlDbType.VarChar)
                End If
                oDBParameters.Add("@sServiceLevel", "0000000000000000", ParameterDirection.Input, SqlDbType.VarChar)
            End If
            If IsDBNull(_dtActiveStartTime) Or _dtActiveStartTime = Nothing Then
                _dtActiveStartTime = Now
            End If
            oDBParameters.Add("@dtActiveStartTime", _dtActiveStartTime, ParameterDirection.Input, SqlDbType.DateTime)

            If IsDBNull(_dtActiveEndTime) Or _dtActiveEndTime = Nothing Then
                _dtActiveEndTime = Now.AddYears(1)
            End If
            oDBParameters.Add("@dtActiveEndTime", _dtActiveEndTime, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@nPARequired", _nPARequired, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@DPSNumber", _sDPSNumber, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@blnRequireSupervisingProviderforeRx", _blnRequireSupervisingProviderforeRx, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@sSignFont", _sSignFont, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nSignFontSize", _nSignFontSize, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nMaxSignScale", _nMaxSignScale, ParameterDirection.Input, SqlDbType.Decimal)
            oDBParameters.Add("@nMinSignScale", _nMinSignScale, ParameterDirection.Input, SqlDbType.Decimal)
            oDBParameters.Add("@nIMGWidth", _nSignWidth, ParameterDirection.Input, SqlDbType.Int)
            If _dtDOB <> "#12:00:00 AM#" Then
                oDBParameters.Add("@dtProviderDOB", _dtDOB, ParameterDirection.Input, SqlDbType.DateTime)
            End If
            oDBParameters.Add("@LicenceKey", LicenceKey.Trim, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@AUSPortalID", AUSPortalID, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@bIsUpdated", 1, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@bIsPDREnabled", IsPDREnabled, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@bIsPDMPEnabled", IsPDMPEnabled, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@nProviderUserID", ProviderUserID, ParameterDirection.Input, SqlDbType.Bit)
            'execute the Insert Provider stored procedure
            'If _IsModify = False Then
            '    oDBParameters.Add("@bIsblocked", 1, ParameterDirection.Input, SqlDbType.Bit)
            'Else
            '    oDBParameters.Add("@bIsblocked", 0, ParameterDirection.Input, SqlDbType.Bit)
            'End If


            oDB.Execute("gsp_InUpProvider", oDBParameters, Output)

            If Output IsNot Nothing Then
                _nProviderID = Convert.ToInt64(Output)
            End If

            'Add Provider Details
            'If _nProviderID > 0 Then
            '    Dim _sqlDeleteDetails As String = "DELETE FROM  Provider_DTL WHERE nProviderID = " & _nProviderID & " AND nClinicID = " & _nClinicID & ""
            '    oDB.Execute_Query(_sqlDeleteDetails)

            '    oDBParameters.Clear()
            '    oDBParameters.Add("@nProviderID", _nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            '    oDBParameters.Add("@sCode", "", ParameterDirection.Input, SqlDbType.VarChar)
            '    oDBParameters.Add("@sDescription", "Medicare", ParameterDirection.Input, SqlDbType.VarChar)
            '    oDBParameters.Add("@sValue", ProviderDetails.MedicareID, ParameterDirection.Input, SqlDbType.VarChar)
            '    oDBParameters.Add("@nType", ProviderIdentificationType.Medicare.GetHashCode(), ParameterDirection.Input, SqlDbType.Int)
            '    oDBParameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            '    'execute the Insert Provider stored procedure
            '    oDB.Execute("gsp_INSERT_Provider_DTL", oDBParameters)

            '    oDBParameters.Clear()
            '    oDBParameters.Add("@nProviderID", _nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            '    oDBParameters.Add("@sCode", "", ParameterDirection.Input, SqlDbType.VarChar)
            '    oDBParameters.Add("@sDescription", "Medicaid", ParameterDirection.Input, SqlDbType.VarChar)
            '    oDBParameters.Add("@sValue", ProviderDetails.MedicaidID, ParameterDirection.Input, SqlDbType.VarChar)
            '    oDBParameters.Add("@nType", ProviderIdentificationType.Medicaid.GetHashCode(), ParameterDirection.Input, SqlDbType.Int)
            '    oDBParameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            '    'execute the Insert Provider stored procedure
            '    oDB.Execute("gsp_INSERT_Provider_DTL", oDBParameters)

            '    Dim _Desc As String = ""
            '    Dim _Value As String = ""

            '    For i As Integer = 1 To ProviderDetails.OtherIdentifications.Count

            '        _Desc = CType(ProviderDetails.OtherIdentifications(i), ProviderDetails.structOtherIdentifications).Description
            '        _Value = CType(ProviderDetails.OtherIdentifications(i), ProviderDetails.structOtherIdentifications).Code

            '        oDBParameters.Clear()
            '        oDBParameters.Add("@nProviderID", _nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            '        oDBParameters.Add("@sCode", "", ParameterDirection.Input, SqlDbType.VarChar)
            '        oDBParameters.Add("@sDescription", _Desc, ParameterDirection.Input, SqlDbType.VarChar)
            '        oDBParameters.Add("@sValue", _Value, ParameterDirection.Input, SqlDbType.VarChar)
            '        oDBParameters.Add("@nType", ProviderIdentificationType.Other.GetHashCode(), ParameterDirection.Input, SqlDbType.Int)
            '        oDBParameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            '        'execute the Insert Provider stored procedure

            '        oDB.Execute("gsp_INSERT_Provider_DTL", oDBParameters)
            '    Next
            'End If
            '--------------------
            SaveProviderPhysicalAddress()
            SaveProviderCompanyPhysicalAddress()
            SaveProvIDQualifier()

            'Add Provider As User Only For new Provider & not for modify
            If _nProviderID <> 0 And _IsModify = False Then

                Dim dtRights As New DataTable
                oDB.Retrive("gsp_RetrieveAllChildRights", dtRights)

                Dim clRights As New Collection
                Dim nCount As Int16
                For nCount = 0 To dtRights.Rows.Count - 1
                    If IsDBNull(dtRights.Rows(nCount).Item(1)) = False Then
                        clRights.Add(dtRights.Rows(nCount)(0).ToString)
                    End If
                Next
                dtRights.Clear()
                dtRights.Dispose()

                Dim dtAuditRights As New DataTable
                oDB.Retrive("gsp_FillAuditRights", dtAuditRights)

                Dim clAuditRights As New Collection
                Dim nAuditCount As Int16

                For nAuditCount = 0 To dtAuditRights.Rows.Count - 1
                    If IsDBNull(dtAuditRights.Rows(nAuditCount).Item(1)) = False Then
                        clAuditRights.Add(dtAuditRights.Rows(nAuditCount)("nModuleId").ToString)
                    End If
                Next
                dtAuditRights.Clear()
                dtAuditRights.Dispose()

                Dim objUser As New clsUsers(_sConnectionString)

                objUser.AuditRights = clAuditRights
                objUser.UserRights = clRights

                If _sUserName <> "" Then
                    objUser.AddUser(_sUserName, _sPassword, _sNickName, System.DateTime.Now.Date, _nProviderID)
                End If
                objUser = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try

        Return _nProviderID
    End Function
    Private Function FormatLicensekey(ByVal sLicenseKey As String) As String
        Dim LicenseKey As String = String.Empty
        Try
            LicenseKey = Convert.ToString(System.Text.RegularExpressions.Regex.Replace(sLicenseKey, ".{6}", "$0-")).TrimEnd("-"c)

            'LicenceKey = LicenseKey.TrimEnd("-"c)
        Catch ex As Exception
            MessageBox.Show("Error while Formating License key :" + ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return LicenseKey
    End Function

    Public Sub GetProvider(ByVal ProviderID As Int64)
        Dim oDB As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim _strSQL As String = ""
        Dim dtProvider As New DataTable()

        Try

            '_strSQL = " SELECT nProviderID, sFirstName, sMiddleName, sLastName, ISNULL(sSuffix,'') As sSuffix, sGender, " _
            '        & " sDEA,sBusinessContactName, sBusinessAddressline1, sBusinessAddressline2, sBusinessCity, sBusinessState, " _
            '        & " sBusinessZIP,sPracContactName, sPracticeAddressline1,sPracticeAddressline2, sPracticeCity, sPracticeState, " _
            '        & " sPracticeZIP,sBusPhoneNo, sBusFAX, sBusPagerNo, sBusEmail, sBusURL,sPracPhoneNo, sPracFAX, sPracPagerNo, " _
            '        & " sPracEmail, sPracURL,sMobileNo, " & " nProviderType, sNPI, imgSignature ,sUPIN, sMedicalLicenseNo, sPrefix, " _
            '        & " nClinicID, sSPIID, sExternalCode,sTaxonomy,sTaxonomyDesc,sSSN,sEmployerID, sCompanyName,sCompanyAddressline1," _
            '        & " sCompanyAddressline2,sCompanyCity,sCompanyState,sCompanyZIP,sCompanyPhone,sCompanyContactName,sCompanyFax," _
            '        & " sCompanyEmail,sCompanyTaxID,sCompanyNPI, sServiceLevel as ServiceLevelCode, dtActiveStartTime as ActiveStartTime, " _
            '        & " dtActiveEndTime as ActiveEndTime, isnull(sSPIID,'') as SPI " _
            '        & " ,isnull(sBussinessCountry,'') as sBussinessCountry, isnull(sBussinessCounty,'') as sBussinessCounty,isnull(sPracticeCountry,'') as sPracticeCountry,isnull(sPracticeCounty,'') as sPracticeCounty,isnull(sCompanyCountry,'') as sCompanyCountry,isnull(sCompanyCounty,'') as sCompanyCounty " _
            '        & " ,ISNULL(Provider_MST.nIsPARequired," + clsProvider.PriorAuthorizationRequired.Always.GetHashCode().ToString() + ") AS nPARequired " _
            '        & " ,ISNULL(BL_Provider_PhysicalAddress.sContactName,'') as sProviderPhyContactName,ISNULL(BL_Provider_PhysicalAddress.sPhysicalAddressline1,'') as sPhysicalAddressline1,ISNULL(BL_Provider_PhysicalAddress.sPhysicalAddressline2,'') as sPhysicalAddressline2,ISNULL(BL_Provider_PhysicalAddress.sPhysicalCity,'') as sPhysicalCity,ISNULL(BL_Provider_PhysicalAddress.sPhysicalState,'') as sPhysicalState,ISNULL(BL_Provider_PhysicalAddress.sPhysicalZIP,'') as sPhysicalZIP,ISNULL(BL_Provider_PhysicalAddress.sPhysicalAreaCode,'') as sPhysicalAreaCode,ISNULL(BL_Provider_PhysicalAddress.sPhysicalCountry,'') as sPhysicalCountry,ISNULL(BL_Provider_PhysicalAddress.sPhysicalCounty,'') as sPhysicalCounty,ISNULL(BL_Provider_PhysicalAddress.sPhysicalPhoneNo,'') as sPhysicalPhoneNo,ISNULL(BL_Provider_PhysicalAddress.sPhysicalFAX,'') as sPhysicalFAX,ISNULL(BL_Provider_PhysicalAddress.sPhysicalPagerNo,'') as sPhysicalPagerNo,ISNULL(BL_Provider_PhysicalAddress.sPhysicalEmail,'') as sPhysicalEmail,ISNULL(BL_Provider_PhysicalAddress.sPhysicalURL,'') as sPhysicalURL " _
            '        & " ,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyContactName,'') as sProviderPhyCompanyContactName,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalAddressline1,'') as sCompanyPhysicalAddressline1,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalAddressline2,'') as sCompanyPhysicalAddressline2,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalCity,'') as sCompanyPhysicalCity,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalState,'') as sCompanyPhysicalState,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalZIP,'') as sCompanyPhysicalZIP,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalAreaCode,'') as sCompanyPhysicalAreaCode,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalCountry,'') as sCompanyPhysicalCountry,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalCounty,'') as sCompanyPhysicalCounty,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalPhoneNo,'') as sCompanyPhysicalPhoneNo,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalFAX,'') as sCompanyPhysicalFAX,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalPagerNo,'') as sCompanyPhysicalPagerNo,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalEmail,'') as sCompanyPhysicalEmail,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalURL,'') as sCompanyPhysicalURL " _
            '        & " ,sDPSNumber FROM Provider_MST LEFT OUTER JOIN BL_Provider_PhysicalAddress ON Provider_MST.nProviderID=BL_Provider_PhysicalAddress.nProviderID LEFT OUTER JOIN BL_Provider_CompanyPhysicalAddress ON Provider_MST.nProviderID=BL_Provider_CompanyPhysicalAddress.nProviderID " _
            '        & " WHERE  Provider_MST.nProviderID = " & ProviderID & " AND Provider_MST.nClinicID = " & _nClinicID & " Order By Provider_MST.sFirstName "

            _strSQL = " SELECT Provider_MST.nProviderID, Provider_MST.sFirstName, Provider_MST.sMiddleName, Provider_MST.sLastName, ISNULL(Provider_MST.sSuffix,'') As sSuffix, Provider_MST.sGender, " _
                    & " Provider_MST.sDEA,Provider_MST.sNADEAN,Provider_MST.sBusinessContactName, Provider_MST.sBusinessAddressline1, Provider_MST.sBusinessAddressline2, Provider_MST.sBusinessCity, Provider_MST.sBusinessState, " _
                    & " Provider_MST.sBusinessZIP,Provider_MST.sPracContactName, Provider_MST.sPracticeAddressline1,Provider_MST.sPracticeAddressline2, Provider_MST.sPracticeCity, Provider_MST.sPracticeState, " _
                    & " Provider_MST.sPracticeZIP,Provider_MST.sBusPhoneNo, Provider_MST.sBusFAX, Provider_MST.sBusPagerNo, Provider_MST.sBusEmail,sDirectAddress, Provider_MST.sBusURL,Provider_MST.sPracPhoneNo, Provider_MST.sPracFAX, Provider_MST.sPracPagerNo, " _
                    & " Provider_MST.sPracEmail, Provider_MST.sPracURL,Provider_MST.sMobileNo, " & " Provider_MST.nProviderType, Provider_MST.sNPI, Provider_MST.imgSignature ,Provider_MST.sUPIN, Provider_MST.sMedicalLicenseNo, Provider_MST.sPrefix, " _
                    & " Provider_MST.nClinicID, Provider_MST.sSPIID, Provider_MST.sExternalCode,Provider_MST.sTaxonomy,Provider_MST.sTaxonomyDesc,Provider_MST.sSSN,Provider_MST.sEmployerID, Provider_MST.sCompanyName,Provider_MST.sCompanyAddressline1," _
                    & " Provider_MST.sCompanyAddressline2,Provider_MST.sCompanyCity,Provider_MST.sCompanyState,Provider_MST.sCompanyZIP,Provider_MST.sCompanyPhone,Provider_MST.sCompanyContactName,Provider_MST.sCompanyFax," _
                    & " Provider_MST.sCompanyEmail,Provider_MST.sCompanyTaxID,Provider_MST.sCompanyNPI, Case isnull(Provider_MST.sCompanyTaxonomyCode,'')  WHEN '' THEN '' ELSE (Ltrim(RTrim(Specialty_MST.sTaxonomyCode))  +'-'+  Specialty_MST.sTaxonomyDesc) END as sCompanyTaxonomyCode, Provider_MST.sServiceLevel as ServiceLevelCode, Provider_MST.dtActiveStartTime as ActiveStartTime, " _
                    & " Provider_MST.dtActiveEndTime as ActiveEndTime, isnull(Provider_MST.sSPIID,'') as SPI " _
                    & " ,isnull(Provider_MST.sBussinessCountry,'') as sBussinessCountry, isnull(Provider_MST.sBussinessCounty,'') as sBussinessCounty,isnull(Provider_MST.sPracticeCountry,'') as sPracticeCountry,isnull(Provider_MST.sPracticeCounty,'') as sPracticeCounty,isnull(Provider_MST.sCompanyCountry,'') as sCompanyCountry,isnull(Provider_MST.sCompanyCounty,'') as sCompanyCounty " _
                    & " ,ISNULL(Provider_MST.nIsPARequired," + clsProvider.PriorAuthorizationRequired.Always.GetHashCode().ToString() + ") AS nPARequired,Provider_MST.sCompanyAreaCode,Provider_MST.sPracticeAreaCode,Provider_MST.sBusinessAreaCode,Provider_MST.dtProviderDOB " _
                    & " ,ISNULL(BL_Provider_PhysicalAddress.sContactName,'') as sProviderPhyContactName,ISNULL(BL_Provider_PhysicalAddress.sPhysicalAddressline1,'') as sPhysicalAddressline1,ISNULL(BL_Provider_PhysicalAddress.sPhysicalAddressline2,'') as sPhysicalAddressline2,ISNULL(BL_Provider_PhysicalAddress.sPhysicalCity,'') as sPhysicalCity,ISNULL(BL_Provider_PhysicalAddress.sPhysicalState,'') as sPhysicalState,ISNULL(BL_Provider_PhysicalAddress.sPhysicalZIP,'') as sPhysicalZIP,ISNULL(BL_Provider_PhysicalAddress.sPhysicalAreaCode,'') as sPhysicalAreaCode,ISNULL(BL_Provider_PhysicalAddress.sPhysicalCountry,'') as sPhysicalCountry,ISNULL(BL_Provider_PhysicalAddress.sPhysicalCounty,'') as sPhysicalCounty,ISNULL(BL_Provider_PhysicalAddress.sPhysicalPhoneNo,'') as sPhysicalPhoneNo,ISNULL(BL_Provider_PhysicalAddress.sPhysicalFAX,'') as sPhysicalFAX,ISNULL(BL_Provider_PhysicalAddress.sPhysicalPagerNo,'') as sPhysicalPagerNo,ISNULL(BL_Provider_PhysicalAddress.sPhysicalEmail,'') as sPhysicalEmail,ISNULL(BL_Provider_PhysicalAddress.sPhysicalURL,'') as sPhysicalURL " _
                    & " ,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyContactName,'') as sProviderPhyCompanyContactName,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalAddressline1,'') as sCompanyPhysicalAddressline1,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalAddressline2,'') as sCompanyPhysicalAddressline2,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalCity,'') as sCompanyPhysicalCity,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalState,'') as sCompanyPhysicalState,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalZIP,'') as sCompanyPhysicalZIP,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalAreaCode,'') as sCompanyPhysicalAreaCode,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalCountry,'') as sCompanyPhysicalCountry,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalCounty,'') as sCompanyPhysicalCounty,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalPhoneNo,'') as sCompanyPhysicalPhoneNo,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalFAX,'') as sCompanyPhysicalFAX,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalPagerNo,'') as sCompanyPhysicalPagerNo,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalEmail,'') as sCompanyPhysicalEmail,ISNULL(BL_Provider_CompanyPhysicalAddress.sCompanyPhysicalURL,'') as sCompanyPhysicalURL " _
                    & " ,sDPSNumber,IsNULL(nIsRequireSupervisingProviderforeRx,0) as nIsRequireSupervisingProviderforeRx,Provider_mst.sSignFont,Provider_mst.nSignFontSize,Provider_mst.nMaxSignScale, IsNull(Provider_mst.nMinSignScale,0.3) as nMinSignScale, Isnull(Provider_mst.nImgWidth,0) as nImgWidth,Provider_MST.LicenceKey as LicenceKey,ISNULL(Provider_MST.nAUSStatus,0) as AUSStatus,ISNULL(Provider_MST.nAUSPortalID,0) as AUSPortalID FROM Provider_MST LEFT OUTER JOIN BL_Provider_PhysicalAddress ON Provider_MST.nProviderID=BL_Provider_PhysicalAddress.nProviderID LEFT OUTER JOIN BL_Provider_CompanyPhysicalAddress ON Provider_MST.nProviderID=BL_Provider_CompanyPhysicalAddress.nProviderID " _
                    & "  LEFT OUTER JOIN Specialty_MST  ON Provider_MST.sCompanyTaxonomyCode=Specialty_MST.sTaxonomyCode WHERE  Provider_MST.nProviderID = " & ProviderID & " AND Provider_MST.nClinicID = " & _nClinicID & " Order By Provider_MST.sFirstName "
            oDB.Connect(False)

            oDB.Retrive_Query(_strSQL, dtProvider)

            If dtProvider IsNot Nothing Then
                If dtProvider.Rows.Count > 0 Then
                    _nProviderID = Convert.ToInt64(dtProvider.Rows(0)("nProviderID"))
                    _Prefix = Convert.ToString(dtProvider.Rows(0)("sPrefix"))
                    _sFirstName = Convert.ToString(dtProvider.Rows(0)("sFirstName"))
                    _sMiddleName = Convert.ToString(dtProvider.Rows(0)("sMiddleName"))
                    _sLastName = Convert.ToString(dtProvider.Rows(0)("sLastName"))
                    _sSuffix = Convert.ToString(dtProvider.Rows(0)("sSuffix"))

                    _sGender = Convert.ToString(dtProvider.Rows(0)("sGender"))
                    _sDEA = Convert.ToString(dtProvider.Rows(0)("sDEA"))
                    _sNADEAN = Convert.ToString(dtProvider.Rows(0)("sNADEAN"))
                    _sDPSNumber = Convert.ToString(dtProvider.Rows(0)("sDPSNumber"))
                    _sBMContactName = Convert.ToString(dtProvider.Rows(0)("sBusinessContactName"))

                    _sBMAddress1 = Convert.ToString(dtProvider.Rows(0)("sBusinessAddressline1"))
                    _sBMAddress2 = Convert.ToString(dtProvider.Rows(0)("sBusinessAddressline2"))
                    _sBMCity = Convert.ToString(dtProvider.Rows(0)("sBusinessCity"))
                    _sBMState = Convert.ToString(dtProvider.Rows(0)("sBusinessState"))
                    _sBMZIP = Convert.ToString(dtProvider.Rows(0)("sBusinessZIP"))

                    _sBMCountry = Convert.ToString(dtProvider.Rows(0)("sBussinessCountry"))         ''Country
                    _sBMCounty = Convert.ToString(dtProvider.Rows(0)("sBussinessCounty"))           ''County


                    _nSignWidth = Convert.ToInt16(dtProvider.Rows(0)("nImgWidth"))

                    _sBPracContactName = Convert.ToString(dtProvider.Rows(0)("sPracContactName"))

                    _sBPracAddress1 = Convert.ToString(dtProvider.Rows(0)("sPracticeAddressline1"))
                    _sBPracAddress2 = Convert.ToString(dtProvider.Rows(0)("sPracticeAddressline2"))
                    _sBPracCity = Convert.ToString(dtProvider.Rows(0)("sPracticeCity"))
                    _sBPracState = Convert.ToString(dtProvider.Rows(0)("sPracticeState"))
                    _sBPracZIP = Convert.ToString(dtProvider.Rows(0)("sPracticeZIP"))

                    _sBPracCountry = Convert.ToString(dtProvider.Rows(0)("sPracticeCountry"))       ''Country
                    _sBPracCounty = Convert.ToString(dtProvider.Rows(0)("sPracticeCounty"))         ''County


                    _sBMPhone = Convert.ToString(dtProvider.Rows(0)("sBusPhoneNo"))
                    _sBMFAX = Convert.ToString(dtProvider.Rows(0)("sBusFAX"))
                    _sMobile = Convert.ToString(dtProvider.Rows(0)("sMobileNo"))
                    _sBMPager = Convert.ToString(dtProvider.Rows(0)("sBusPagerNo"))
                    _sBMEmail = Convert.ToString(dtProvider.Rows(0)("sBusEmail"))
                    _sDirectAddress = Convert.ToString(dtProvider.Rows(0)("sDirectAddress"))  'Direct Address
                    _sBMURL = Convert.ToString(dtProvider.Rows(0)("sBusURL"))

                    _sBpracPhone = Convert.ToString(dtProvider.Rows(0)("sPracPhoneNo"))
                    _sBpracFAX = Convert.ToString(dtProvider.Rows(0)("sPracFAX"))
                    _sBPracPager = Convert.ToString(dtProvider.Rows(0)("sPracPagerNo"))
                    _sBPracEmail = Convert.ToString(dtProvider.Rows(0)("sPracEmail"))
                    _sBPracURL = Convert.ToString(dtProvider.Rows(0)("sPracURL"))

                    _sComapanyName = Convert.ToString(dtProvider.Rows(0)("sCompanyName"))
                    _sComapanyContactName = Convert.ToString(dtProvider.Rows(0)("sCompanyContactName"))
                    _sComapanyAddress1 = Convert.ToString(dtProvider.Rows(0)("sCompanyAddressline1"))
                    _sComapanyAddress2 = Convert.ToString(dtProvider.Rows(0)("sCompanyAddressline2"))
                    _sComapanyCity = Convert.ToString(dtProvider.Rows(0)("sCompanyCity"))
                    _sComapanyState = Convert.ToString(dtProvider.Rows(0)("sCompanyState"))
                    _sComapanyZip = Convert.ToString(dtProvider.Rows(0)("sCompanyZIP"))

                    _sComapanyCountry = Convert.ToString(dtProvider.Rows(0)("sCompanyCountry"))  ''Country
                    _sComapanyCounty = Convert.ToString(dtProvider.Rows(0)("sCompanyCounty"))      ''County

                    _sComapanyPhone = Convert.ToString(dtProvider.Rows(0)("sCompanyPhone"))
                    _sComapanyFax = Convert.ToString(dtProvider.Rows(0)("sCompanyFax"))
                    _sComapanyEmail = Convert.ToString(dtProvider.Rows(0)("sCompanyEmail"))

                    _sComapanyNPI = Convert.ToString(dtProvider.Rows(0)("sCompanyNPI"))
                    _sComapanyTaxID = Convert.ToString(dtProvider.Rows(0)("sCompanyTaxID"))
                    _sCompanyTaxonomyCode = Convert.ToString(dtProvider.Rows(0)("sCompanyTaxonomyCode")).Trim

                    _sEmployerID = Convert.ToString(dtProvider.Rows(0)("sEmployerID"))
                    _sSSNno = Convert.ToString(dtProvider.Rows(0)("sSSN"))

                    _Taxonomy = Convert.ToString(dtProvider.Rows(0)("sTaxonomy"))
                    _TaxonomyDesc = Convert.ToString(dtProvider.Rows(0)("sTaxonomyDesc"))
                    If Not dtProvider.Rows(0)("imgSignature") Is System.DBNull.Value Then
                        Dim arrImage As Byte() = DirectCast(dtProvider.Rows(0)("imgSignature"), Byte())
                        Dim ms As New MemoryStream(arrImage)
                        _imgSignature = Image.FromStream(ms)
                    End If
                    _sSignFont = Convert.ToString(dtProvider.Rows(0)("sSignFont"))
                    _nSignFontSize = Convert.ToString(dtProvider.Rows(0)("nSignFontSize"))
                    _nMaxSignScale = Convert.ToString(dtProvider.Rows(0)("nMaxSignScale"))
                    _nMinSignScale = Convert.ToString(dtProvider.Rows(0)("nMinSignScale"))

                    If dtProvider.Rows(0)("nProviderType") IsNot Nothing Then
                        _nProviderTypeID = Convert.ToInt64(dtProvider.Rows(0)("nProviderType"))
                    End If

                    _NPI = Convert.ToString(dtProvider.Rows(0)("sNPI"))
                    _UPIN = Convert.ToString(dtProvider.Rows(0)("sUPIN"))
                    _StateMedicalNo = Convert.ToString(dtProvider.Rows(0)("sMedicalLicenseNo"))
                    _sExternalCode = Convert.ToString(dtProvider.Rows(0)("sExternalCode")) ''External code ''
                    _nClinicID = Convert.ToInt64(dtProvider.Rows(0)("nClinicID"))

                    _sServiceLevel = Convert.ToString(dtProvider.Rows(0)("ServiceLevelCode"))
                    If Not dtProvider.Rows(0)("ActiveStartTime") Is System.DBNull.Value Then

                        _dtActiveStartTime = CType(dtProvider.Rows(0)("ActiveStartTime"), DateTime)
                    End If
                    If Not dtProvider.Rows(0)("ActiveEndTime") Is System.DBNull.Value Then

                        _dtActiveEndTime = CType(dtProvider.Rows(0)("ActiveEndTime"), DateTime)
                    End If
                    _sSPI = dtProvider.Rows(0)("SPI")

                    LicenceKey = Convert.ToString(dtProvider.Rows(0)("LicenceKey"))
                    AUSStatus = Convert.ToInt16(dtProvider.Rows(0)("AUSStatus"))
                    AUSPortalID = Convert.ToInt64(dtProvider.Rows(0)("AUSPortalID"))

                    _nUserID = 0
                    _sUserName = ""
                    _sPassword = ""
                    _nPARequired = Convert.ToInt16(dtProvider.Rows(0)("nPARequired"))

                    _sPhysicalAddContactName = Convert.ToString(dtProvider.Rows(0)("sProviderPhyContactName"))
                    _sPhysicalAddressline1 = Convert.ToString(dtProvider.Rows(0)("sPhysicalAddressline1"))
                    _sPhysicalAddressline2 = Convert.ToString(dtProvider.Rows(0)("sPhysicalAddressline2"))
                    _sPhysicalCity = Convert.ToString(dtProvider.Rows(0)("sPhysicalCity"))
                    _sPhysicalState = Convert.ToString(dtProvider.Rows(0)("sPhysicalState"))
                    _sPhysicalZIP = Convert.ToString(dtProvider.Rows(0)("sPhysicalZIP"))
                    _sPhysicalAreaCode = Convert.ToString(dtProvider.Rows(0)("sPhysicalAreaCode"))
                    _sPhysicalCountry = Convert.ToString(dtProvider.Rows(0)("sPhysicalCountry"))
                    _sPhysicalCounty = Convert.ToString(dtProvider.Rows(0)("sPhysicalCounty"))
                    _sPhysicalPhoneNo = Convert.ToString(dtProvider.Rows(0)("sPhysicalPhoneNo"))
                    _sPhysicalPagerNo = Convert.ToString(dtProvider.Rows(0)("sPhysicalPagerNo"))
                    _sPhysicalFAX = Convert.ToString(dtProvider.Rows(0)("sPhysicalFAX"))
                    _sPhysicalEmail = Convert.ToString(dtProvider.Rows(0)("sPhysicalEmail"))
                    _sPhysicalURL = Convert.ToString(dtProvider.Rows(0)("sPhysicalURL"))

                    _sCompanyPhysicalAddContactName = Convert.ToString(dtProvider.Rows(0)("sProviderPhyCompanyContactName"))
                    _sCompanyPhysicalAddressline1 = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalAddressline1"))
                    _sCompanyPhysicalAddressline2 = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalAddressline2"))
                    _sCompanyPhysicalCity = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalCity"))
                    _sCompanyPhysicalState = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalState"))
                    _sCompanyPhysicalZIP = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalZIP"))
                    _sCompanyPhysicalAreaCode = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalAreaCode"))
                    _sCompanyPhysicalCountry = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalCountry"))
                    _sCompanyPhysicalCounty = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalCounty"))
                    _sCompanyPhysicalPhoneNo = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalPhoneNo"))
                    _sCompanyPhysicalPagerNo = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalPagerNo"))
                    _sCompanyPhysicalFAX = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalFAX"))
                    _sCompanyPhysicalEmail = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalEmail"))
                    _sCompanyPhysicalURL = Convert.ToString(dtProvider.Rows(0)("sCompanyPhysicalURL"))
                    _blnRequireSupervisingProviderforeRx = (dtProvider.Rows(0)("nIsRequireSupervisingProviderforeRx"))
                    If Not IsNothing(dtProvider.Rows(0)("dtProviderDOB")) AndAlso Not IsDBNull(dtProvider.Rows(0)("dtProviderDOB")) Then

                        _dtDOB = Convert.ToDateTime(dtProvider.Rows(0)("dtProviderDOB"))
                    Else
                        _dtDOB = Nothing
                    End If

                    Dim dtProviderDetails As New DataTable()
                    _strSQL = " SELECT nProviderID, ISNULL(sCode,'') AS sCode, ISNULL(sDescription,'') AS sDescription, " _
                            & " ISNULL(sValue,'') AS sValue, ISNULL(nType,0) AS nType  FROM Provider_DTL " _
                            & " WHERE nProviderID = " & _nProviderID & " AND nClinicID = " & _nClinicID & ""
                    oDB.Retrive_Query(_strSQL, dtProviderDetails)

                    If dtProviderDetails IsNot Nothing AndAlso dtProviderDetails.Rows.Count > 0 Then
                        For i As Integer = 0 To dtProviderDetails.Rows.Count - 1
                            If CType(dtProviderDetails.Rows(i)("nType"), ProviderIdentificationType) = ProviderIdentificationType.Medicare Then
                                _ProviderDetails.MedicareID = Convert.ToString(dtProviderDetails.Rows(i)("sValue"))
                            ElseIf CType(dtProviderDetails.Rows(i)("nType"), ProviderIdentificationType) = ProviderIdentificationType.Medicaid Then
                                _ProviderDetails.MedicaidID = Convert.ToString(dtProviderDetails.Rows(i)("sValue"))
                            Else
                                Dim oOtherDetails As ProviderDetails.structOtherIdentifications
                                oOtherDetails.Code = Convert.ToString(dtProviderDetails.Rows(i)("sValue"))
                                oOtherDetails.Description = Convert.ToString(dtProviderDetails.Rows(i)("sDescription"))
                                _ProviderDetails.OtherIdentifications.Add(oOtherDetails)
                            End If

                        Next

                    End If

                    Dim dtPDRDetails As DataSet = Nothing
                    Dim dbParameters As New gloDatabaseLayer.DBParameters()
                    Dim dbParameter As New gloDatabaseLayer.DBParameter("@nProviderID", _nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
                    dbParameters.Add(dbParameter)

                    oDB.Retrive("PDR_PC_GetProviderPCSetting", dbParameters, dtPDRDetails)

                    If dtPDRDetails IsNot Nothing AndAlso dtPDRDetails.Tables(0).Rows.Count > 0 Then
                        IsPDREnabled = dtPDRDetails.Tables(0).Rows(0)("sValue")
                    End If

                    If dtPDRDetails IsNot Nothing AndAlso dtPDRDetails.Tables(1).Rows.Count > 0 Then
                        IsPDMPEnabled = dtPDRDetails.Tables(1).Rows(0)("sValue")
                    End If

                    If dtPDRDetails IsNot Nothing Then
                        dtPDRDetails.Dispose()
                        dtPDRDetails = Nothing
                    End If

                    If dbParameters IsNot Nothing Then
                        dbParameters.Clear()
                        dbParameters.Dispose()
                        dbParameters = Nothing
                    End If

                    dbParameter = Nothing


                 

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub

    Public Function GetProviderTypes() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim _strSQL As [String] = ""
        Dim dtProviderType As New DataTable()

        Try
            '_strSQL = "SELECT nProviderTypeID,sProviderType FROM AB_Resource_ProviderType WHERE bIsBlocked = 0 AND nClinicID = " & _nClinicID & " ORDER BY nProviderTypeID"
            _strSQL = "SELECT nProviderTypeID,sProviderType FROM ProviderType_MST ORDER BY nProviderTypeID"


            oDB.Connect(False)
            'get all the Provider types

            oDB.Retrive_Query(_strSQL, dtProviderType)

            oDB.Disconnect()

            Return dtProviderType
        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
            Return Nothing
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)

            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function
    Public Function checkclinicNPI() As Integer

        Dim oDB As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim _strSQL As [String] = ""
        Dim icnt As Integer = 0
        Try
            '_strSQL = "SELECT nProviderTypeID,sProviderType FROM AB_Resource_ProviderType WHERE bIsBlocked = 0 AND nClinicID = " & _nClinicID & " ORDER BY nProviderTypeID"
            _strSQL = "select len(sclinicNPI) From Clinic_MST"


            oDB.Connect(False)
            'get all the Provider types



            icnt = oDB.ExecuteScalar_Query(_strSQL)
            oDB.Disconnect()

            Return icnt
        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
            Return Nothing
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)

            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function
    '' END SUDHIR ''
    Private Sub SaveProvIDQualifier()

        Dim oDB As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        oDB.Connect(False)
        Try
            Dim _Desc As String = ""
            Dim _Value As String = ""
            Dim _nQualifierID As Int64 = 0
            Dim _bIsSystem As Boolean = False


            '''''''''Save Provider Id qualifiers'''''''''
            Dim _strQry As String = " DELETE FROM Provider_ID_Qualifiers WHERE nProviderID = " & _nProviderID & ""
            oDB.Execute_Query(_strQry)

            For i As Integer = 1 To ProviderDetails.OtherIdentifications.Count
                _nQualifierID = CType(ProviderDetails.OtherIdentifications(i), ProviderDetails.structOtherIdentifications).nQualifierID
                _Value = CType(ProviderDetails.OtherIdentifications(i), ProviderDetails.structOtherIdentifications).sValue
                _bIsSystem = CType(ProviderDetails.OtherIdentifications(i), ProviderDetails.structOtherIdentifications).bIsSystem
                _nUserID = Convert.ToInt64(appSettings("userID"))

                oDBParameters.Clear()
                oDBParameters.Add("@QualifierType", 1, ParameterDirection.Input, SqlDbType.Int)
                oDBParameters.Add("@nProviderID", _nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameters.Add("@nQualifierID", _nQualifierID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameters.Add("@sValue", _Value, ParameterDirection.Input, SqlDbType.VarChar)
                oDBParameters.Add("@bIsSystem", _bIsSystem, ParameterDirection.Input, SqlDbType.Bit)
                oDBParameters.Add("@nUserID", _nUserID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt)

                'execute the Insert Provider stored procedure
                oDB.Execute("gsp_INSERT_Provider_ID_Qualifiers", oDBParameters)
            Next


            '''''''''Save Provider Id company qualifiers'''''''''
            _strQry = ""
            ''Bug #65084: 00000644 : Tax Id missing for other provider Company
            _strQry = " DELETE FROM Provider_ID_CompanyQualifiers WHERE nProviderID = " & _nProviderID & " and nProviderCompanyID is null"
            oDB.Execute_Query(_strQry)
            For j As Integer = 1 To ProviderDetails.CompanyIdentifications.Count

                _nQualifierID = CType(ProviderDetails.CompanyIdentifications(j), ProviderDetails.structCompanyIdentifications).nQualifierID
                _Value = CType(ProviderDetails.CompanyIdentifications(j), ProviderDetails.structCompanyIdentifications).sValue
                _bIsSystem = CType(ProviderDetails.CompanyIdentifications(j), ProviderDetails.structCompanyIdentifications).bIsSystem
                _nUserID = Convert.ToInt64(appSettings("userID"))

                oDBParameters.Clear()
                oDBParameters.Add("@QualifierType", 2, ParameterDirection.Input, SqlDbType.Int)
                oDBParameters.Add("@nProviderID", _nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameters.Add("@nQualifierID", _nQualifierID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameters.Add("@sValue", _Value, ParameterDirection.Input, SqlDbType.VarChar)
                oDBParameters.Add("@bIsSystem", _bIsSystem, ParameterDirection.Input, SqlDbType.Bit)
                oDBParameters.Add("@nUserID", _nUserID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt)
                'execute the Insert Provider stored procedure
                oDB.Execute("gsp_INSERT_Provider_ID_Qualifiers", oDBParameters)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub

    Private Sub SaveProviderPhysicalAddress()

        Dim oDB As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        oDB.Connect(False)
        Try

            oDBParameters.Add("@ProviderID", _nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sContactName", _sPhysicalAddContactName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalAddressline1", _sPhysicalAddressline1, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalAddressline2", _sPhysicalAddressline2, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalCity", _sPhysicalCity, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalState", _sPhysicalState, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalZIP", _sPhysicalZIP, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalAreaCode", _sPhysicalAreaCode, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalCountry", _sPhysicalCountry, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalCounty", _sPhysicalCounty, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalPhoneNo", _sPhysicalPhoneNo, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalFAX", _sPhysicalFAX, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalPagerNo", _sPhysicalPagerNo, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalEmail", _sPhysicalEmail, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sPhysicalURL", _sPhysicalURL, ParameterDirection.Input, SqlDbType.VarChar)
            'execute the Insert/Update Provider Physical Address stored procedure
            oDB.Execute("BL_InUpProvider_PhysicalAddress", oDBParameters)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub

    Private Sub SaveProviderCompanyPhysicalAddress()

        Dim oDB As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        oDB.Connect(False)
        Try

            oDBParameters.Add("@ProviderID", _nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sContactName", _sCompanyPhysicalAddContactName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalAddressline1", _sCompanyPhysicalAddressline1, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalAddressline2", _sCompanyPhysicalAddressline2, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalCity", _sCompanyPhysicalCity, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalState", _sCompanyPhysicalState, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalZIP", _sCompanyPhysicalZIP, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalAreaCode", _sCompanyPhysicalAreaCode, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalCountry", _sCompanyPhysicalCountry, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalCounty", _sCompanyPhysicalCounty, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalPhoneNo", _sCompanyPhysicalPhoneNo, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalFAX", _sCompanyPhysicalFAX, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalPagerNo", _sCompanyPhysicalPagerNo, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalEmail", _sCompanyPhysicalEmail, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sCompanyPhysicalURL", _sCompanyPhysicalURL, ParameterDirection.Input, SqlDbType.VarChar)
            'execute the Insert/Update Provider Physical Address stored procedure
            oDB.Execute("BL_InUpProvider_CompanyPhysicalAddress", oDBParameters)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub

#End Region
End Class

'' SUDHIR 20090624 ''
Public Class ProviderDetails

    Public Structure structOtherIdentifications
        Dim nQualifierID As Int64
        Dim sValue As String
        Dim Code As String
        Dim Description As String
        Dim bIsSystem As Boolean
    End Structure

    Public Structure structCompanyIdentifications
        Dim nQualifierID As Int64
        Dim sValue As String
        Dim Code As String
        Dim Description As String
        Dim bIsSystem As Boolean
    End Structure

    Private _sMedicareID As String = ""
    Private _sMedicaidID As String = ""
    Private _OtherIdentifications As New Collection
    Private _CompanyIdentification As New Collection

    Public Property MedicareID() As String
        Get
            Return _sMedicareID
        End Get
        Set(ByVal value As String)
            _sMedicareID = value
        End Set
    End Property

    Public Property MedicaidID() As String
        Get
            Return _sMedicaidID
        End Get
        Set(ByVal value As String)
            _sMedicaidID = value
        End Set
    End Property

    Public Property OtherIdentifications() As Collection
        Get
            Return _OtherIdentifications
        End Get
        Set(ByVal value As Collection)
            _OtherIdentifications = value
        End Set
    End Property

    Public Property CompanyIdentifications() As Collection
        Get
            Return _CompanyIdentification
        End Get
        Set(ByVal value As Collection)
            _CompanyIdentification = value
        End Set
    End Property
End Class

