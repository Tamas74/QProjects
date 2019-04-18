Imports System.Data.SqlClient
Imports System.IO

Public Class clsUsers

#Region "Private Variables"

    Dim _nUserID As Int64
    Dim _sUserName As String
    Dim _sPassword As String
    Dim _sNickName As String
    Dim _sFirstName As String
    Dim _sMiddleName As String
    Dim _sLastName As String
    Dim _sSSNno As String
    Dim _dtDOB As Date
    Dim _sGender As String
    Dim _sMaritalStatus As String
    Dim _sAddress As String
    Dim _sAddress2 As String
    Dim _sStreet As String
    Dim _sCity As String
    Dim _sState As String
    Dim _sZIP As String

    Dim _sCountry As String     ''country
    Dim _sCounty As String      ''county

    Dim _sPhoneNo As String
    Dim _sMobileNo As String
    Dim _sFAX As String
    Dim _sEmail As String
    Dim _nBlockStatus As Boolean = False
    Dim _blnAdministrator As Boolean

    Dim _ISCountforCPOE As Boolean

    Dim _nProviderID As Int64 = 0
    Dim _sProviderName As String
    Dim _clGroups As New Collection
    Dim _clRights As New Collection

    'Added Rahul for AuditRights on 20101019
    Dim _clAuditRights As New Collection

    'Added Rahul for Load Speciality on 20101020
    Dim _sSpeciality As String = ""

    Dim _imgSignature As Image
    Dim _IsPasswordReset As Boolean = False
    Dim _IsAuditTrail As Boolean

    'Added by Ujwala Atre as on 20101004
    Dim _blnPChartAccess As Boolean = False
    Dim _sEAPassword As String
    Dim _ValidDt As Date

    'Added by Ujwala Atre as on 20101004

    Dim _blnAccessDenied As Boolean = False
    Dim _blnCoSign As Boolean
    Dim _bIsSecurity As Boolean

    'sarika 11th sept 07
    Dim _sConnectionString As String = ""
    Dim _sErrorMessage As String = ""

    Dim _bIsExchangeUser As Boolean = False
    Dim _sExchangeLogin As String = ""
    Dim _sExchangePassword As String = ""

    Dim _sAbilityEmail As String = String.Empty
    Dim _sAbilityPassword As String = String.Empty

    'Task #67507: gloEMR & Patient Portal Send Message screen changes
    Dim _sPortalDisplayName As String = String.Empty
    Dim _bIsFromEMR As Boolean = False

    '--------------------------
    Dim _WindowLoginName As String

    Enum enmUsersType
        All
        Active
        NonActive
    End Enum

#End Region

#Region "Properties"

    Public Property ProviderID() As Int64
        Get
            Return _nProviderID
        End Get
        Set(ByVal Value As Int64)
            _nProviderID = Value
        End Set
    End Property

    Public Property Speciality() As String
        Get
            Return _sSpeciality
        End Get
        Set(ByVal value As String)
            _sSpeciality = value
        End Set
    End Property

    Public Property ProviderName() As String
        Get
            Return _sProviderName
        End Get
        Set(ByVal Value As String)
            _sProviderName = Value
        End Set
    End Property

    Public Property gloEMRAdministrator() As Boolean
        Get
            Return _blnAdministrator
        End Get
        Set(ByVal Value As Boolean)
            _blnAdministrator = Value
        End Set
    End Property

    Public Property ISCountforCPOE() As Boolean
        Get
            Return _ISCountforCPOE
        End Get
        Set(ByVal Value As Boolean)
            _ISCountforCPOE = Value
        End Set
    End Property

    Public Property UserRights() As Collection
        Get
            Return _clRights
        End Get
        Set(ByVal Value As Collection)
            _clRights = Value
        End Set
    End Property

    Public Property UserGroups() As Collection
        Get
            Return _clGroups
        End Get
        Set(ByVal Value As Collection)
            _clGroups = Value
        End Set
    End Property

    Public Property UserID() As Int64
        Get
            Return _nUserID
        End Get
        Set(ByVal Value As Int64)
            _nUserID = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _sUserName
        End Get
        Set(ByVal Value As String)
            _sUserName = Value
        End Set
    End Property

    Public Property WindowLoaginName() As String
        Get
            Return _WindowLoginName
        End Get
        Set(ByVal Value As String)
            _WindowLoginName = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _sPassword
        End Get
        Set(ByVal Value As String)
            _sPassword = Value
        End Set
    End Property

    Public Property NickName() As String
        Get
            Return _sNickName
        End Get
        Set(ByVal Value As String)
            _sNickName = Value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return _sFirstName
        End Get
        Set(ByVal Value As String)
            _sFirstName = Value
        End Set
    End Property

    Public Property MiddleName() As String
        Get
            Return _sMiddleName
        End Get
        Set(ByVal Value As String)
            _sMiddleName = Value
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return _sLastName
        End Get
        Set(ByVal Value As String)
            _sLastName = Value
        End Set
    End Property

    Public Property SSNNo() As String
        Get
            Return _sSSNno
        End Get
        Set(ByVal Value As String)
            _sSSNno = Value
        End Set
    End Property

    Public Property DOB() As Date
        Get
            Return _dtDOB
        End Get
        Set(ByVal Value As Date)
            _dtDOB = Value
        End Set
    End Property

    Public Property Gender() As String
        Get
            Return _sGender
        End Get
        Set(ByVal Value As String)
            _sGender = Value
        End Set
    End Property

    Public Property MaritalStatus() As String
        Get
            Return _sMaritalStatus
        End Get
        Set(ByVal Value As String)
            _sMaritalStatus = Value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _sAddress
        End Get
        Set(ByVal Value As String)
            _sAddress = Value
        End Set
    End Property

    Public Property Address2() As String
        Get
            Return _sAddress2
        End Get
        Set(ByVal Value As String)
            _sAddress2 = Value
        End Set
    End Property

    Public Property Street() As String
        Get
            Return _sStreet
        End Get
        Set(ByVal Value As String)
            _sStreet = Value
        End Set
    End Property

    Public Property City() As String
        Get
            Return _sCity
        End Get
        Set(ByVal Value As String)
            _sCity = Value
        End Set
    End Property

    Public Property State() As String
        Get
            Return _sState
        End Get
        Set(ByVal Value As String)
            _sState = Value
        End Set
    End Property

    Public Property ZIP() As String
        Get
            Return _sZIP
        End Get
        Set(ByVal Value As String)
            _sZIP = Value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return _sCountry
        End Get
        Set(ByVal Value As String)
            _sCountry = Value
        End Set
    End Property

    Public Property County() As String
        Get
            Return _sCounty
        End Get
        Set(ByVal Value As String)
            _sCounty = Value
        End Set
    End Property

    Public Property PhoneNo() As String
        Get
            Return _sPhoneNo
        End Get
        Set(ByVal Value As String)
            _sPhoneNo = Value
        End Set
    End Property

    Public Property MobileNo() As String
        Get
            Return _sMobileNo
        End Get
        Set(ByVal Value As String)
            _sMobileNo = Value
        End Set
    End Property

    Public Property FAX() As String
        Get
            Return _sFAX
        End Get
        Set(ByVal Value As String)
            _sFAX = Value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _sEmail
        End Get
        Set(ByVal Value As String)
            _sEmail = Value
        End Set
    End Property

    Public Property BlockStatus() As Boolean
        Get
            Return _nBlockStatus
        End Get
        Set(ByVal Value As Boolean)
            _nBlockStatus = Value
        End Set
    End Property

    Public Property Signature() As Image
        Get
            Return _imgSignature
        End Get
        Set(ByVal Value As Image)
            _imgSignature = Value
        End Set
    End Property

    Public Property IsPasswordReset() As Boolean
        Get
            Return _IsPasswordReset
        End Get
        Set(ByVal Value As Boolean)
            _IsPasswordReset = Value
        End Set
    End Property

    'Added by Ujwala Atre as on 20101004
    Public Property EAPassword() As String
        Get
            Return _sEAPassword
        End Get
        Set(ByVal Value As String)
            _sEAPassword = Value
        End Set
    End Property

    Public Property ValidDt() As Date
        Get
            Return _ValidDt
        End Get
        Set(ByVal Value As Date)
            _ValidDt = Value
        End Set
    End Property

    Public Property EAPChart() As Boolean
        Get
            Return _blnPChartAccess
        End Get
        Set(ByVal Value As Boolean)
            _blnPChartAccess = Value
        End Set
    End Property

    Public Property IsAuditTrail() As Boolean
        Get
            Return _IsAuditTrail
        End Get
        Set(ByVal Value As Boolean)
            _IsAuditTrail = Value
        End Set
    End Property

    Public Property IsCoSign() As Boolean
        Get
            Return _blnCoSign
        End Get
        Set(ByVal Value As Boolean)
            _blnCoSign = Value
        End Set
    End Property

    Public Property IsSecurityUser() As Boolean
        Get
            Return _bIsSecurity
        End Get
        Set(ByVal value As Boolean)
            _bIsSecurity = value
        End Set
    End Property

    Public Property AccessDenied() As Boolean
        Get
            Return _blnAccessDenied
        End Get
        Set(ByVal Value As Boolean)
            _blnAccessDenied = Value
        End Set
    End Property

    Public ReadOnly Property ConnectionString() As String
        Get
            Return _sConnectionString
        End Get
    End Property

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return _sErrorMessage
        End Get
    End Property
  
    Public Property AbilityEmail() As String
        Get
            Return _sAbilityEmail
        End Get
        Set(ByVal value As String)
            _sAbilityEmail = value
        End Set
    End Property

    Public Property AbilityPassword() As String
        Get
            Return _sAbilityPassword
        End Get
        Set(ByVal value As String)
            _sAbilityPassword = value
        End Set
    End Property

    Public Property IsExchangeUser() As Boolean
        Get
            Return _bIsExchangeUser
        End Get
        Set(ByVal value As Boolean)
            _bIsExchangeUser = value
        End Set
    End Property

    Public Property ExchangeLogin() As String
        Get
            Return _sExchangeLogin
        End Get
        Set(ByVal value As String)
            _sExchangeLogin = value
        End Set
    End Property

    Public Property ExchangePassward() As String
        Get
            Return _sExchangePassword
        End Get
        Set(ByVal value As String)
            _sExchangePassword = value
        End Set
    End Property

    'Added Rahul for AuditRights on 20101019
    Public Property AuditRights() As Collection
        Get
            Return _clAuditRights
        End Get
        Set(ByVal Value As Collection)
            _clAuditRights = Value
        End Set
    End Property

    'Task #67507: gloEMR & Patient Portal Send Message screen changes
    Public Property PortalDisplayName() As String
        Get
            Return _sPortalDisplayName
        End Get
        Set(ByVal Value As String)
            _sPortalDisplayName = Value
        End Set
    End Property

    Public Property IsFromEMR() As Boolean
        Get
            Return _bIsFromEMR
        End Get
        Set(ByVal value As Boolean)
            _bIsFromEMR = value
        End Set
    End Property

#End Region

#Region "Constructor"

    Public Sub New()

    End Sub

    Public Sub New(ByVal sConnectionString As String)
        _sConnectionString = sConnectionString
    End Sub

#End Region


#Region "Methods"


    Public Function PopulateUsers(ByVal enmUser As enmUsersType) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand

        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanUsers"
        objCmd.Connection = objCon

        Dim objParaUserType As New SqlParameter
        With objParaUserType
            .ParameterName = "@UserType"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        If enmUser = enmUsersType.All Then
            objParaUserType.Value = 2
        ElseIf enmUser = enmUsersType.Active Then
            objParaUserType.Value = 1
        Else
            objParaUserType.Value = 0
        End If
        objCmd.Parameters.Add(objParaUserType)
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function

    Public Function AddUser() As Int64
        Return AddUser(_sUserName, _sPassword, _sEAPassword, _blnPChartAccess, _ValidDt, _sNickName, _dtDOB, _nProviderID, _imgSignature, _IsPasswordReset, _IsAuditTrail, _blnCoSign, _bIsSecurity, _blnAccessDenied, _sPortalDisplayName, _sFirstName, _sMiddleName, _sLastName, _sSSNno, _sGender, _sMaritalStatus, _sAddress, _sAddress2, _sStreet, _sCity, _sState, _sZIP, _sPhoneNo, _sMobileNo, _sFAX, _sEmail, _blnAdministrator, _sCountry, _sCounty, _sSpeciality, _sAbilityEmail, _sAbilityPassword, _ISCountforCPOE, _WindowLoginName)
    End Function

    Public Function AddUser(ByVal strUserName As String, ByVal strPassword As String, ByVal strEAPassword As String, ByVal blnEAPchart As Boolean, ByVal dtValidDt As Date, ByVal strNickName As String, ByVal dtDOB As Date, ByVal nProviderID As Int64, ByVal imgSign As Image, ByVal blnPasswordReset As Boolean, ByVal blnAuditTrail As Boolean, ByVal blnCosign As Boolean, ByVal bIssecurityUser As Boolean, ByVal blnAccessDenied As Boolean, ByVal strPortalDisplayName As String, Optional ByVal strFirstName As String = "", Optional ByVal strMiddleName As String = "", Optional ByVal strLastName As String = "", Optional ByVal strSSNNo As String = "", Optional ByVal strGender As String = "", Optional ByVal strMaritalStatus As String = "", Optional ByVal strAddress As String = "", Optional ByVal strAddress2 As String = "", Optional ByVal strStreet As String = "", Optional ByVal strCity As String = "", Optional ByVal strState As String = "", Optional ByVal strZIP As String = "", Optional ByVal strPhoneNo As String = "", Optional ByVal strMobileNo As String = "", Optional ByVal strFAX As String = "", Optional ByVal strEmail As String = "", Optional ByVal blnAdministrator As Boolean = False, Optional ByVal sCountry As String = "", Optional ByVal sCounty As String = "", Optional ByVal strspeciality As String = "", Optional ByVal _sAbilityEmail As String = "", Optional ByVal _sAbilityPassword As String = "", Optional ByVal _ISCountforCPOE As Boolean = False, Optional ByVal strWindowLoginName As String = "", Optional ByVal nBlockUserStatus As Boolean = False) As Int64

        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim _nuserid As Int64
        'objCmd.CommandType = CommandType.StoredProcedure
        'objCmd.CommandText = "gsp_InsertUser"

        Dim objParaUserID As New SqlParameter
        With objParaUserID
            .ParameterName = "@UserID"
            .Value = 0
            .Direction = ParameterDirection.InputOutput
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaUserID)

        Dim objParaUserName As New SqlParameter
        With objParaUserName
            .ParameterName = "@LoginName"
            .Value = strUserName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaUserName)

        Dim objParaPassword As New SqlParameter
        With objParaPassword
            .ParameterName = "@Password"
            .Value = strPassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaPassword)

        'Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart
        Dim objEAPassword As New SqlParameter
        With objEAPassword
            .ParameterName = "@sAccessPassword"
            .Value = strEAPassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objEAPassword)

        Dim objEAPchart As New SqlParameter
        With objEAPchart
            .ParameterName = "@IsPatientChartAccess"
            .Value = blnEAPchart
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objEAPchart)

        'Added Valid upto Date for Emergency Access Password as on 12032010
        Dim objParaValidDt As New SqlParameter
        With objParaValidDt
            .ParameterName = "@dtValidupto"
            .Value = dtValidDt
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaValidDt)

        'Added Valid upto Date for Emergency Access Password as on 12032010
        'Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart

        Dim objParaNickName As New SqlParameter
        With objParaNickName
            .ParameterName = "@NickName"
            .Value = strNickName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNickName)

        Dim objParaFirstName As New SqlParameter
        With objParaFirstName
            .ParameterName = "@FirstName"
            .Value = strFirstName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaFirstName)

        Dim objParaMiddleName As New SqlParameter
        With objParaMiddleName
            .ParameterName = "@MiddleName"
            .Value = strMiddleName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaMiddleName)

        Dim objParaLastName As New SqlParameter
        With objParaLastName
            .ParameterName = "@LastName"
            .Value = strLastName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaLastName)

        Dim objParaSSN As New SqlParameter
        With objParaSSN
            .ParameterName = "@SSNNo"
            .Value = strSSNNo
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSSN)

        Dim objParaDOB As New SqlParameter
        With objParaDOB
            .ParameterName = "@DOB"
            .Value = dtDOB
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaDOB)

        Dim objParaGender As New SqlParameter
        With objParaGender
            .ParameterName = "@Gender"
            .Value = strGender
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaGender)

        Dim objParaMaritalStatus As New SqlParameter
        With objParaMaritalStatus
            .ParameterName = "@MaritalStatus"
            .Value = strMaritalStatus
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaMaritalStatus)

        Dim objParaAddress As New SqlParameter
        With objParaAddress
            .ParameterName = "@Address"
            .Value = strAddress
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaAddress)

        Dim objParaAddress2 As New SqlParameter
        With objParaAddress2
            .ParameterName = "@Address2"
            .Value = strAddress2
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaAddress2)

        Dim objParaStreet As New SqlParameter
        With objParaStreet
            .ParameterName = "@Street"
            .Value = strStreet
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaStreet)

        Dim objParaCity As New SqlParameter
        With objParaCity
            .ParameterName = "@City"
            .Value = strCity
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaCity)

        Dim objParaState As New SqlParameter
        With objParaState
            .ParameterName = "@State"
            .Value = strState
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaState)

        Dim objParaZIP As New SqlParameter
        With objParaZIP
            .ParameterName = "@ZIP"
            .Value = strZIP
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaZIP)

        Dim objParaCountry As New SqlParameter
        With objParaCountry
            .ParameterName = "@sCountry"
            .Value = sCountry
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaCountry)

        Dim objParaCounty As New SqlParameter
        With objParaCounty
            .ParameterName = "@sCounty"
            .Value = sCounty
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaCounty)

        Dim objParaPhoneNo As New SqlParameter
        With objParaPhoneNo
            .ParameterName = "@PhoneNo"
            .Value = strPhoneNo
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaPhoneNo)

        Dim objParaMobileNo As New SqlParameter
        With objParaMobileNo
            .ParameterName = "@MobileNo"
            .Value = strMobileNo
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaMobileNo)

        Dim objParaFAX As New SqlParameter
        With objParaFAX
            .ParameterName = "@FAX"
            .Value = strFAX
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaFAX)

        Dim objParaEmail As New SqlParameter
        With objParaEmail
            .ParameterName = "@Email"
            .Value = strEmail
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaEmail)

        Dim objParaAdministrator As New SqlParameter
        With objParaAdministrator
            .ParameterName = "@Administrator"
            If blnAdministrator = False Then
                .Value = 0
            Else
                .Value = 1
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaAdministrator)

        Dim objCountforCPOE As New SqlParameter
        With objCountforCPOE
            .ParameterName = "@CountforCPOE"
            If _ISCountforCPOE = False Then
                .Value = 0
            Else
                .Value = 1
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objCountforCPOE)

        Dim objParaProviderID As New SqlParameter
        With objParaProviderID
            .ParameterName = "@ProviderID"
            .Value = nProviderID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaProviderID)

        If IsNothing(_imgSignature) = False Then
            Dim ms As New MemoryStream
            _imgSignature.Save(ms, Imaging.ImageFormat.Bmp)
            Dim arrImage() As Byte = ms.GetBuffer
            ms.Close()
            Dim objParaSignature As New SqlParameter
            With objParaSignature
                .ParameterName = "@Signature"
                .Value = arrImage
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Image
            End With
            objCmd.Parameters.Add(objParaSignature)
        End If

        Dim objResetPwd As New SqlParameter
        With objResetPwd
            .ParameterName = "@IsPasswordReset"
            .Value = blnPasswordReset
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objResetPwd)

        Dim objAuditTrail As New SqlParameter
        With objAuditTrail
            .ParameterName = "@IsAuditTrail"
            .Value = blnAuditTrail
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objAuditTrail)

        Dim objAccessFlag As New SqlParameter
        With objAccessFlag
            .ParameterName = "@nAccessDenied"
            .Value = blnAccessDenied
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objAccessFlag)

        Dim objCosignFlag As New SqlParameter
        With objCosignFlag
            .ParameterName = "@bCoSign"
            .Value = blnCosign
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objCosignFlag)

        Dim objSecurityUserFlag As New SqlParameter
        With objSecurityUserFlag
            .ParameterName = "@bIsSecurityUser"
            .Value = bIssecurityUser
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objSecurityUserFlag)

        Dim objIsExchangeuser As New SqlParameter
        With objIsExchangeuser
            .ParameterName = "@IsExchangeUser"
            .Value = _bIsExchangeUser
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objIsExchangeuser)

        Dim objParaExchangelogin As New SqlParameter
        With objParaExchangelogin
            .ParameterName = "@sExchangeLogin"
            .Value = _sExchangeLogin
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaExchangelogin)

        Dim objParaExchangePassword As New SqlParameter
        With objParaExchangePassword
            .ParameterName = "@sExchangePassword"
            .Value = _sExchangePassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaExchangePassword)

        'Dim oMachineID As New SqlParameter
        'With oMachineID
        '    .ParameterName = "@MachineID"
        '    .Value = mdlGeneral.GetPrefixTransactionID
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.BigInt
        'End With
        'objCmd.Parameters.Add(oMachineID)

        Dim oSpeciality As New SqlParameter
        With oSpeciality
            .ParameterName = "@Speciality"
            .Value = _sSpeciality
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(oSpeciality)

        Dim objParaAbilityEmail As New SqlParameter
        With objParaAbilityEmail
            .ParameterName = "@sAbilityEmail"
            .Value = _sAbilityEmail
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaAbilityEmail)

        Dim objParaAbilityPassword As New SqlParameter
        With objParaAbilityPassword
            .ParameterName = "@sAbilityPassword"
            .Value = _sAbilityPassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaAbilityPassword)

        'Task #67507: gloEMR & Patient Portal Send Message screen changes
        Dim objParaPortalDisplayName As New SqlParameter
        With objParaPortalDisplayName
            .ParameterName = "@sPortalDisplayName"
            .Value = strPortalDisplayName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaPortalDisplayName)

        '--------------------------------------------------------------
        Dim objParaWindowLoginName As New SqlParameter
        With objParaWindowLoginName
            .ParameterName = "@WindowLoginName"
            .Value = strWindowLoginName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaWindowLoginName)

        Dim objParaBlockUser As New SqlParameter
        With objParaBlockUser
            .ParameterName = "@BlockUser"
            If nBlockUserStatus = True Then
                .Value = 1
            Else
                .Value = 0
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaBlockUser)

        Dim objFromEMR As New SqlParameter
        With objFromEMR
            .ParameterName = "@IsFromEMR"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objFromEMR)


        Dim objModifiedByUserID As New SqlParameter
        With objModifiedByUserID
            .ParameterName = "@nModifiedByUserId"
            .Value = gnLoginID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objModifiedByUserID)

        Dim objHistoryId As New SqlParameter
        With objHistoryId
            .ParameterName = "@HistoryId"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objHistoryId)
        '--------------------------------------------------------------

        'objCmd.Connection = objCon
        'objCon.Open()
        'objCmd.ExecuteNonQuery()
        'objCon.Close()

        'For TVP_UserRights Changed by Ashish on 19-Nov-2013 from multiple database calls to a single call
        Dim dtUserRights As New DataTable("TVP_UserRights")
        With dtUserRights
            .Columns.Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
            .Columns.Add(New DataColumn("nRightsID", System.Type.GetType("System.Int64")))
        End With

        Dim drUserRight As DataRow = Nothing

        Dim nCount As Integer
        For nCount = 1 To _clRights.Count
            drUserRight = dtUserRights.NewRow
            drUserRight("nUserID") = 0
            drUserRight("nRightsID") = Convert.ToInt32(_clRights.Item(nCount))
            dtUserRights.Rows.Add(drUserRight)
        Next
        drUserRight = Nothing

        Dim objTVP_UserRights As New SqlParameter
        With objTVP_UserRights
            .ParameterName = "TVP_UserRights"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Structured
            .Value = dtUserRights
        End With

        'For TVP_UserRights For TVP_UserAuditRights
        Dim dtUserAuditRights As New DataTable("TVP_UserAuditRights")
        With dtUserAuditRights
            .Columns.Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
            .Columns.Add(New DataColumn("nModuleID", System.Type.GetType("System.Int64")))
        End With

        Dim drUserAuditRight As DataRow = Nothing
        ''Added Rahul for AuditRights on 20101019

        Dim nCnt As Integer
        For nCnt = 1 To _clAuditRights.Count
            drUserAuditRight = dtUserAuditRights.NewRow
            drUserAuditRight("nUserID") = 0
            drUserAuditRight("nModuleID") = _clAuditRights.Item(nCnt)
            dtUserAuditRights.Rows.Add(drUserAuditRight)
        Next
        drUserAuditRight = Nothing

        Dim objTVP_UserAuditRights As New SqlParameter
        With objTVP_UserAuditRights
            .ParameterName = "TVP_UserAuditRights"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Structured
            .Value = dtUserAuditRights
        End With


        'Dim objParaGroupUserID As New SqlParameter
        'With objParaGroupUserID
        '    .ParameterName = "@UserID"
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.BigInt
        '    .Value = objParaUserID.Value
        'End With
        'objCmd.Parameters.Add(objParaGroupUserID)
        ''For loop on Clgroups commented beacause at a time we can select only1 group
        Dim objParaGroupName As New SqlParameter
        With objParaGroupName
            .ParameterName = "@GroupName"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
            If _clGroups.Count > 0 Then
                .Value = _clGroups.Item(1)
            Else
                .Value = ""
            End If

        End With
        objCmd.Parameters.Add(objParaGroupName)
        'objCmd.CommandType = CommandType.StoredProcedure
        'objCmd.CommandText = "gsp_InsertUsersRights"
        'objCmd.Connection = objCon
        'objCmd.Parameters.Add(objTVP_UserRights)
        'objCmd.Parameters.Add(objTVP_UserAuditRights)
        'objCmd.Connection.Open()
        'objCmd.ExecuteNonQuery()
        'objCmd.Connection.Close()
        'objCmd.Parameters.Clear()
        'objCmd.Dispose()
    

        'Dim sqlCommand As New SqlCommand
        With objCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "gsp_InsertUsersRights"
            .Connection = objCon
            .Parameters.Add(objTVP_UserRights)
            .Parameters.Add(objTVP_UserAuditRights)
            .Connection.Open()
            _nuserid = .ExecuteNonQuery()
            .Connection.Close()
            .Parameters.Clear()
            .Dispose()
        End With
        If objParaUserID IsNot Nothing Then
            _nuserid = Convert.ToInt64(objParaUserID.Value)
        End If
        If dtUserRights IsNot Nothing Then
            dtUserRights.Clear()
            dtUserRights.Dispose()
            dtUserRights = Nothing
        End If

        If dtUserAuditRights IsNot Nothing Then
            dtUserAuditRights.Clear()
            dtUserAuditRights.Dispose()
            dtUserAuditRights = Nothing
        End If

        objTVP_UserAuditRights = Nothing
        objTVP_UserRights = Nothing

        'For nCount = 1 To _clGroups.Count
        '    Dim objcmdChild As New SqlCommand
        '    objcmdChild.CommandType = CommandType.StoredProcedure
        '    objcmdChild.CommandText = "gsp_InsertUserGroups"

        '    Dim objParaGroupUserID As New SqlParameter
        '    With objParaGroupUserID
        '        .ParameterName = "@UserID"
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.BigInt
        '        .Value = objParaUserID.Value
        '    End With
        '    objcmdChild.Parameters.Add(objParaGroupUserID)

        '    Dim objParaGroupName As New SqlParameter
        '    With objParaGroupName
        '        .ParameterName = "@GroupName"
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.VarChar
        '        .Value = _clGroups.Item(nCount)
        '    End With

        '    With objcmdChild
        '        .Parameters.Add(objParaGroupName)
        '        .Connection = objCon
        '        .Connection.Open()
        '        .ExecuteNonQuery()
        '        .Connection.Close()
        '        .Parameters.Clear()
        '        .Dispose()
        '    End With

        '    objCon.Dispose()
        '    objcmdChild = Nothing
        '    objParaGroupUserID = Nothing
        '    objParaGroupName = Nothing
        'Next

        objCmd = Nothing
        objCon = Nothing
        Return _nuserid
    End Function

    Public Function BlockUnblockUser(ByVal nUserID As Int64, ByVal blnBlock As Boolean) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand

        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_BlockUnblockUser"

        Dim objParaUserID As New SqlParameter
        With objParaUserID
            .ParameterName = "@UserID"
            .Value = nUserID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaUserID)

        Dim objParaBlockUnBlockUser As New SqlParameter
        With objParaBlockUnBlockUser
            .ParameterName = "@BlockUser"
            If blnBlock = True Then
                .Value = 1
            Else
                .Value = 0
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaBlockUnBlockUser)

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function

    Public Function EditUser(ByVal nUserID As Int64) As Boolean
        Return EditUser(nUserID, _sUserName, _sPassword, _sEAPassword, _blnPChartAccess, _ValidDt, _sNickName, _dtDOB, _imgSignature, _IsPasswordReset, _blnAccessDenied, _IsAuditTrail, _blnCoSign, _bIsSecurity, _sPortalDisplayName, _bIsFromEMR, _sFirstName, _sMiddleName, _sLastName, _sSSNno, _sGender, _sMaritalStatus, _sAddress, _sAddress2, _sStreet, _sCity, _sState, _sZIP, _sPhoneNo, _sMobileNo, _sFAX, _sEmail, _blnAdministrator, _nBlockStatus, _nProviderID, _sCountry, _sCounty, _sSpeciality, _sAbilityEmail, _sAbilityPassword, _ISCountforCPOE, _WindowLoginName)
    End Function

    Public Function EditUser(ByVal nUserID As Int64, ByVal strUserName As String, ByVal strPassword As String, ByVal strEAPassword As String, ByVal blnEAPchart As Boolean, ByVal dtValidDt As Date, ByVal strNickName As String, ByVal dtDOB As Date, ByVal imgSignature As Image, ByVal blnPasswordReset As Boolean, ByVal blnAccessDenied As Boolean, ByVal blnAuditTrail As Boolean, ByVal blnCoSign As Boolean, ByVal blnIssecurityUser As Boolean, ByVal strPortalDisplayName As String, ByVal blnIsFromEMR As Boolean, Optional ByVal strFirstName As String = "", Optional ByVal strMiddleName As String = "", Optional ByVal strLastName As String = "", Optional ByVal strSSNNo As String = "", Optional ByVal strGender As String = "", Optional ByVal strMaritalStatus As String = "", Optional ByVal strAddress As String = "", Optional ByVal strAddress2 As String = "", Optional ByVal strStreet As String = "", Optional ByVal strCity As String = "", Optional ByVal strState As String = "", Optional ByVal strZIP As String = "", Optional ByVal strPhoneNo As String = "", Optional ByVal strMobileNo As String = "", Optional ByVal strFAX As String = "", Optional ByVal strEmail As String = "", Optional ByVal blnAdministrator As Boolean = False, Optional ByVal nBlockUserStatus As Boolean = False, Optional ByVal nProviderID As Int64 = 0, Optional ByVal sCountry As String = "", Optional ByVal sCounty As String = "", Optional ByVal sSpecialty As String = "", Optional ByVal _sAbilityEmail As String = "", Optional ByVal _sAbilityPassword As String = "", Optional ByVal _ISCountforCPOE As Boolean = False, Optional ByVal strWindowLoginName As String = "") As Boolean

        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand

        'objCmd.CommandType = CommandType.StoredProcedure
        'objCmd.CommandText = "gsp_UpdateUser"

        Dim objParaUserID As New SqlParameter
        With objParaUserID
            .ParameterName = "@UserID"
            .Value = nUserID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaUserID)

        Dim objParaUserName As New SqlParameter
        With objParaUserName
            .ParameterName = "@LoginName"
            .Value = strUserName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaUserName)

        Dim objParaPassword As New SqlParameter
        With objParaPassword
            .ParameterName = "@Password"
            .Value = strPassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaPassword)

        'Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart
        Dim objEAPassword As New SqlParameter
        With objEAPassword
            .ParameterName = "@sAccessPassword"
            .Value = strEAPassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objEAPassword)

        Dim objEAPchart As New SqlParameter
        With objEAPchart
            .ParameterName = "@IsPatientChartAccess"
            .Value = blnEAPchart
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objEAPchart)

        'Added Valid upto Date for Emergency Access Password as on 12032010
        Dim objParaValidDt As New SqlParameter
        With objParaValidDt
            .ParameterName = "@dtValidupto"
            .Value = dtValidDt
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaValidDt)

        Dim objParaNickName As New SqlParameter
        With objParaNickName
            .ParameterName = "@NickName"
            .Value = strNickName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNickName)

        Dim objParaFirstName As New SqlParameter
        With objParaFirstName
            .ParameterName = "@FirstName"
            .Value = strFirstName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaFirstName)

        Dim objParaMiddleName As New SqlParameter
        With objParaMiddleName
            .ParameterName = "@MiddleName"
            .Value = strMiddleName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaMiddleName)

        Dim objParaLastName As New SqlParameter
        With objParaLastName
            .ParameterName = "@LastName"
            .Value = strLastName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaLastName)

        Dim objParaSSN As New SqlParameter
        With objParaSSN
            .ParameterName = "@SSNNo"
            .Value = strSSNNo
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSSN)

        Dim objParaDOB As New SqlParameter
        With objParaDOB
            .ParameterName = "@DOB"
            .Value = dtDOB
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaDOB)

        Dim objParaGender As New SqlParameter
        With objParaGender
            .ParameterName = "@Gender"
            .Value = strGender
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaGender)

        Dim objParaMaritalStatus As New SqlParameter
        With objParaMaritalStatus
            .ParameterName = "@MaritalStatus"
            .Value = strMaritalStatus
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaMaritalStatus)

        Dim objParaAddress As New SqlParameter
        With objParaAddress
            .ParameterName = "@Address"
            .Value = strAddress
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaAddress)

        Dim objParaAddress2 As New SqlParameter
        With objParaAddress2
            .ParameterName = "@Address2"
            .Value = strAddress2
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaAddress2)

        Dim objParaStreet As New SqlParameter
        With objParaStreet
            .ParameterName = "@Street"
            .Value = strStreet
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaStreet)

        Dim objParaCity As New SqlParameter
        With objParaCity
            .ParameterName = "@City"
            .Value = strCity
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaCity)

        Dim objParaState As New SqlParameter
        With objParaState
            .ParameterName = "@State"
            .Value = strState
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaState)

        Dim objParaZIP As New SqlParameter
        With objParaZIP
            .ParameterName = "@ZIP"
            .Value = strZIP
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaZIP)

        Dim objParaCountry As New SqlParameter
        With objParaCountry
            .ParameterName = "@sCountry"
            .Value = sCountry
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaCountry)

        Dim objParaCounty As New SqlParameter
        With objParaCounty
            .ParameterName = "@sCounty"
            .Value = sCounty
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaCounty)

        Dim objParaPhoneNo As New SqlParameter
        With objParaPhoneNo
            .ParameterName = "@PhoneNo"
            .Value = strPhoneNo
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaPhoneNo)

        Dim objParaMobileNo As New SqlParameter
        With objParaMobileNo
            .ParameterName = "@MobileNo"
            .Value = strMobileNo
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaMobileNo)

        Dim objParaFAX As New SqlParameter
        With objParaFAX
            .ParameterName = "@FAX"
            .Value = strFAX
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaFAX)

        Dim objParaEmail As New SqlParameter
        With objParaEmail
            .ParameterName = "@Email"
            .Value = strEmail
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaEmail)

        Dim objParaAdministrator As New SqlParameter
        With objParaAdministrator
            .ParameterName = "@Administrator"
            If blnAdministrator = False Then
                .Value = 0
            Else
                .Value = 1
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaAdministrator)

        Dim objCountforCPOE As New SqlParameter
        With objCountforCPOE
            .ParameterName = "@CountforCPOE"
            If _ISCountforCPOE = False Then
                .Value = 0
            Else
                .Value = 1
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objCountforCPOE)

        Dim objParaBlockUser As New SqlParameter
        With objParaBlockUser
            .ParameterName = "@BlockUser"
            If nBlockUserStatus = True Then
                .Value = 1
            Else
                .Value = 0
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaBlockUser)

        Dim objParaProviderID As New SqlParameter
        With objParaProviderID
            .ParameterName = "@ProviderID"
            .Value = nProviderID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaProviderID)

        If IsNothing(_imgSignature) = False Then
            Dim ms As New MemoryStream
            _imgSignature.Save(ms, Imaging.ImageFormat.Bmp)
            Dim arrImage() As Byte = ms.GetBuffer
            ms.Close()
            Dim objParaSignature As New SqlParameter
            With objParaSignature
                .ParameterName = "@imgSignature"
                .Value = arrImage
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Image
            End With
            objCmd.Parameters.Add(objParaSignature)
        End If

        Dim objResetPwd As New SqlParameter
        With objResetPwd
            .ParameterName = "@IsPasswordReset"
            .Value = blnPasswordReset
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objResetPwd)

        Dim objAuditTrail As New SqlParameter
        With objAuditTrail
            .ParameterName = "@IsAuditTrail"
            .Value = blnAuditTrail
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objAuditTrail)

        Dim objAccessFlag As New SqlParameter
        With objAccessFlag
            .ParameterName = "@nAccessDenied"
            .Value = blnAccessDenied
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objAccessFlag)

        Dim objCosignFlag As New SqlParameter
        With objCosignFlag
            .ParameterName = "@bCoSign"
            .Value = blnCoSign
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objCosignFlag)

        Dim objIsSecurityUserFlag As New SqlParameter
        With objIsSecurityUserFlag
            .ParameterName = "@bIsSecurityUser"
            .Value = blnIssecurityUser
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With

        Dim objIsExchangeuser As New SqlParameter
        With objIsExchangeuser
            .ParameterName = "@IsExchangeUser"
            .Value = _bIsExchangeUser
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objIsExchangeuser)

        Dim objParaExchangelogin As New SqlParameter
        With objParaExchangelogin
            .ParameterName = "@sExchangeLogin"
            .Value = _sExchangeLogin
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaExchangelogin)

        Dim objParaExchangePassword As New SqlParameter
        With objParaExchangePassword
            .ParameterName = "@sExchangePassword"
            .Value = _sExchangePassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaExchangePassword)

        Dim objParaSpecialty As New SqlParameter
        With objParaSpecialty
            .ParameterName = "@Speciality"
            .Value = _sSpeciality
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSpecialty)

        objCmd.Parameters.Add(objIsSecurityUserFlag)

        Dim objParaAbilityEmail As New SqlParameter
        With objParaAbilityEmail
            .ParameterName = "@sAbilityEmail"
            .Value = _sAbilityEmail
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaAbilityEmail)

        Dim objParaAbilityPassword As New SqlParameter
        With objParaAbilityPassword
            .ParameterName = "@sAbilityPassword"
            .Value = _sAbilityPassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaAbilityPassword)

        'Task #67507: gloEMR & Patient Portal Send Message screen changes
        Dim objParaPortalDisplayName As New SqlParameter
        With objParaPortalDisplayName
            .ParameterName = "@sPortalDisplayName"
            .Value = strPortalDisplayName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaPortalDisplayName)

        'Pass _bIsFromEMR from EMRAdmin only as true default is false i.e. update PMAdmin.
        Dim objFromEMR As New SqlParameter
        With objFromEMR
            .ParameterName = "@IsFromEMR"
            .Value = blnIsFromEMR
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objFromEMR)

        '--------------------------------------------------------------
        Dim objParaWindowLoginName As New SqlParameter
        With objParaWindowLoginName
            .ParameterName = "@WindowLoginName"
            .Value = strWindowLoginName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaWindowLoginName)
        '--------------------------------------------------------------
        Dim objParaGroupName As New SqlParameter
        With objParaGroupName
            .ParameterName = "@GroupName"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
            If _clGroups.Count > 0 Then
                .Value = _clGroups.Item(1)
            Else
                .Value = ""
            End If


        End With
        objCmd.Parameters.Add(objParaGroupName)

        'objCmd.Connection = objCon
        'objCon.Open()
        'objCmd.ExecuteNonQuery()
        'objCon.Close()

        'Delete All User Groups
        'Dim objcmdDelete As New SqlCommand
        'objcmdDelete.CommandType = CommandType.StoredProcedure
        'objcmdDelete.CommandText = "gsp_DeleteUserGroups"
        'Dim objParaDeleteUserGroupID As New SqlParameter
        'With objParaDeleteUserGroupID
        '    .ParameterName = "@UserID"
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.BigInt
        '    .Value = objParaUserID.Value
        'End With
        'objcmdDelete.Parameters.Add(objParaDeleteUserGroupID)
        'objcmdDelete.Connection = objCon
        'objCon.Open()
        'objcmdDelete.ExecuteNonQuery()
        'objCon.Close()


        'Delete All User Rights

        '20091102 New Paramerter added to get the Application Type 
        Dim objParaDeleteAppType As New SqlParameter
        With objParaDeleteAppType
            .ParameterName = "@ApplicationType"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
            .Value = gnApplicationType
        End With
        objCmd.Parameters.Add(objParaDeleteAppType)


        Dim objModifiedByUserID As New SqlParameter
        With objModifiedByUserID
            .ParameterName = "@nModifiedByUserId"
            .Value = gnLoginID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objModifiedByUserID)

        Dim objHistoryId As New SqlParameter
        With objHistoryId
            .ParameterName = "@HistoryId"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objHistoryId)

        'Dim objParaGroupName As New SqlParameter
        'With objParaGroupName
        '    .ParameterName = "@GroupName"
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.VarChar
        '    .Value = _clGroups.Item(nCount)
        'End With
        'objcmdChild.Parameters.Add(objParaGroupName)
        'will delete the user right of the user for selected Application viz gloEMR / gloPM
        'objcmdDelete.CommandText = "gsp_DeleteUserRights"
        'objCon.Open()
        'objcmdDelete.ExecuteNonQuery()
        'objCon.Close()

        'Added Rahul for AuditRights on 20101019 Delete All AuditRights

        'Dim objcmdAuditRightsDelete As New SqlCommand
        'objcmdAuditRightsDelete.CommandType = CommandType.StoredProcedure
        'objcmdAuditRightsDelete.CommandText = "gsp_DeleteAuditRights"
        'Dim objParaDeleteUserID As New SqlParameter
        'With objParaDeleteUserID
        '    .ParameterName = "@nUserId"
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.BigInt
        '    .Value = objParaUserID.Value
        'End With
        'objcmdAuditRightsDelete.Parameters.Add(objParaDeleteUserID)
        'objcmdAuditRightsDelete.Connection = objCon
        'objCon.Open()
        'objcmdAuditRightsDelete.ExecuteNonQuery()
        'objCon.Close()

        Dim dtUserRights As New DataTable("TVP_UserRights")
        With dtUserRights
            .Columns.Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
            .Columns.Add(New DataColumn("nRightsID", System.Type.GetType("System.Int64")))
        End With

        Dim drUserRight As DataRow = Nothing

        Dim nCount As Integer

        For nCount = 1 To _clRights.Count
            drUserRight = dtUserRights.NewRow
            drUserRight("nUserID") = objParaUserID.Value
            drUserRight("nRightsID") = Convert.ToInt32(_clRights.Item(nCount))
            dtUserRights.Rows.Add(drUserRight)
        Next

        drUserRight = Nothing

        Dim objTVP_UserRights As New SqlParameter
        With objTVP_UserRights
            .ParameterName = "TVP_UserRights"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Structured
            .Value = dtUserRights
        End With

        'For TVP_UserAuditRights
        Dim dtUserAuditRights As New DataTable("TVP_UserAuditRights")
        With dtUserAuditRights
            .Columns.Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
            .Columns.Add(New DataColumn("nModuleID", System.Type.GetType("System.Int64")))
        End With

        Dim drUserAuditRight As DataRow = Nothing

        'Added Rahul for AuditRights on 20101019
        Dim nCnt As Integer
        For nCnt = 1 To _clAuditRights.Count
            drUserAuditRight = dtUserAuditRights.NewRow
            drUserAuditRight("nUserID") = objParaUserID.Value
            drUserAuditRight("nModuleID") = _clAuditRights.Item(nCnt)
            dtUserAuditRights.Rows.Add(drUserAuditRight)
        Next

        drUserAuditRight = Nothing

        Dim objTVP_UserAuditRights As New SqlParameter
        With objTVP_UserAuditRights
            .ParameterName = "TVP_UserAuditRights"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Structured
            .Value = dtUserAuditRights
        End With

        'Dim sqlCommand As New SqlCommand
        With objCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "gsp_InsertUsersRights"
            .Connection = objCon
            .Parameters.Add(objTVP_UserRights)
            .Parameters.Add(objTVP_UserAuditRights)
            .Connection.Open()
            .ExecuteNonQuery()
            .Connection.Close()
            .Parameters.Clear()
            .Dispose()
        End With
        'If objHistoryId IsNot Nothing Then
        '    _nHistoryId = Convert.ToInt64(objHistoryId.Value)
        'End If

        If dtUserRights IsNot Nothing Then
            dtUserRights.Clear()
            dtUserRights.Dispose()
            dtUserRights = Nothing
        End If

        If dtUserAuditRights IsNot Nothing Then
            dtUserAuditRights.Clear()
            dtUserAuditRights.Dispose()
            dtUserAuditRights = Nothing
        End If

        objTVP_UserAuditRights = Nothing
        objTVP_UserRights = Nothing

        'For nCount = 1 To _clGroups.Count
        '    Dim objcmdChild As New SqlCommand
        '    objcmdChild.CommandType = CommandType.StoredProcedure

        '    Dim objParaGroupUserID As New SqlParameter
        '    With objParaGroupUserID
        '        .ParameterName = "@UserID"
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.BigInt
        '        .Value = objParaUserID.Value
        '    End With
        '    objcmdChild.Parameters.Add(objParaGroupUserID)
        '    objcmdChild.Connection = objCon

        '    objcmdChild.CommandText = "gsp_UpdateUserGroups"
        '    Dim objParaGroupName As New SqlParameter
        '    With objParaGroupName
        '        .ParameterName = "@GroupName"
        '        .Direction = ParameterDirection.Input
        '        .SqlDbType = SqlDbType.VarChar
        '        .Value = _clGroups.Item(nCount)
        '    End With
        '    objcmdChild.Parameters.Add(objParaGroupName)
        '    objCon.Open()
        '    objcmdChild.ExecuteNonQuery()
        '    objCon.Close()

        '    objCon.Dispose()

        '    objcmdChild = Nothing
        '    objParaGroupUserID = Nothing
        '    objParaGroupName = Nothing
        'Next
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function


    Public Sub SearchUser(ByVal nUserID As Int64)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveUser"

        Dim objParaUserID As New SqlParameter
        With objParaUserID
            .ParameterName = "@UserID"
            .Value = nUserID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaUserID)
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()

            If IsDBNull(objSQLDataReader.Item("UserName")) = False Then
                _sUserName = objSQLDataReader.Item("UserName")
            Else
                _sUserName = ""
            End If

            If IsDBNull(objSQLDataReader.Item("UserPassword")) = False Then
                _sPassword = objSQLDataReader.Item("UserPassword")
            Else
                _sPassword = ""
            End If

            If IsDBNull(objSQLDataReader.Item("NickName")) = False Then
                _sNickName = objSQLDataReader.Item("NickName")
            Else
                _sNickName = ""
            End If

            If IsDBNull(objSQLDataReader.Item("FirstName")) = False Then
                _sFirstName = objSQLDataReader.Item("FirstName")
            Else
                _sFirstName = ""
            End If

            If IsDBNull(objSQLDataReader.Item("MiddleName")) = False Then
                _sMiddleName = objSQLDataReader.Item("MiddleName")
            Else
                _sMiddleName = ""
            End If

            If IsDBNull(objSQLDataReader.Item("LastName")) = False Then
                _sLastName = objSQLDataReader.Item("LastName")
            Else
                _sLastName = ""
            End If

            If IsDBNull(objSQLDataReader.Item("SSNNo")) = False Then
                _sSSNno = objSQLDataReader.Item("SSNNo")
            Else
                _sSSNno = ""
            End If

            If IsDBNull(objSQLDataReader.Item("DOB")) = False Then
                _dtDOB = objSQLDataReader.Item("DOB")
            Else
                _dtDOB = Date.Now.Date
            End If

            If IsDBNull(objSQLDataReader.Item("Gender")) = False Then
                _sGender = objSQLDataReader.Item("Gender")
            Else
                _sGender = "Unknown"
            End If

            If IsDBNull(objSQLDataReader.Item("MaritalStatus")) = False Then
                _sMaritalStatus = objSQLDataReader.Item("MaritalStatus")
            Else
                _sMaritalStatus = "Single"
            End If

            If IsDBNull(objSQLDataReader.Item("Address")) = False Then
                _sAddress = objSQLDataReader.Item("Address")
            Else
                _sAddress = ""
            End If

            If IsDBNull(objSQLDataReader.Item("Address2")) = False Then
                _sAddress2 = objSQLDataReader.Item("Address2")
            Else
                _sAddress2 = ""
            End If

            If IsDBNull(objSQLDataReader.Item("Street")) = False Then
                _sStreet = objSQLDataReader.Item("Street")
            Else
                _sStreet = ""
            End If

            If IsDBNull(objSQLDataReader.Item("City")) = False Then
                _sCity = objSQLDataReader.Item("City")
            Else
                _sCity = ""
            End If

            If IsDBNull(objSQLDataReader.Item("State")) = False Then
                _sState = objSQLDataReader.Item("State")
            Else
                _sState = ""
            End If

            If IsDBNull(objSQLDataReader.Item("ZIP")) = False Then
                _sZIP = objSQLDataReader.Item("ZIP")
            Else
                _sZIP = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sCountry")) = False Then  ''Country
                _sCountry = objSQLDataReader.Item("sCountry")
            Else
                _sCountry = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sCounty")) = False Then   ''county 
                _sCounty = objSQLDataReader.Item("sCounty")
            Else
                _sCounty = ""
            End If

            If IsDBNull(objSQLDataReader.Item("PhoneNo")) = False Then
                _sPhoneNo = objSQLDataReader.Item("PhoneNo")
            Else
                _sPhoneNo = ""
            End If

            If IsDBNull(objSQLDataReader.Item("MobileNo")) = False Then
                _sMobileNo = objSQLDataReader.Item("MobileNo")
            Else
                _sMobileNo = ""
            End If

            If IsDBNull(objSQLDataReader.Item("FAX")) = False Then
                _sFAX = objSQLDataReader.Item("FAX")
            Else
                _sFAX = ""
            End If

            If IsDBNull(objSQLDataReader.Item("Email")) = False Then
                _sEmail = objSQLDataReader.Item("Email")
            Else
                _sEmail = ""
            End If

            _blnAdministrator = False

            'Developer: Mitesh Patel Date:03-Jan-2012 PRD: Direct Ability
            If IsDBNull(objSQLDataReader.Item("sAbilityEmail")) = False Then
                _sAbilityEmail = objSQLDataReader.Item("sAbilityEmail")
            Else
                _sAbilityEmail = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sAbilityPassword")) = False Then
                _sAbilityPassword = objSQLDataReader.Item("sAbilityPassword")
            Else
                _sAbilityPassword = ""
            End If

            'Task #67507: gloEMR & Patient Portal Send Message screen changes
            If IsDBNull(objSQLDataReader.Item("sPortalDisplayName")) = False Then
                _sPortalDisplayName = objSQLDataReader.Item("sPortalDisplayName")
            Else
                _sPortalDisplayName = ""
            End If

            If IsDBNull(objSQLDataReader.Item("gloEMRAdministrator")) = False Then
                _blnAdministrator = objSQLDataReader.Item("gloEMRAdministrator")
            End If

            If IsDBNull(objSQLDataReader.Item("ISCountforCPOE")) = False Then
                _ISCountforCPOE = objSQLDataReader.Item("ISCountforCPOE")
            End If

            _nBlockStatus = 0
            If IsDBNull(objSQLDataReader.Item("BlockStatus")) = False Then
                _nBlockStatus = objSQLDataReader.Item("BlockStatus")
            End If

            If IsDBNull(objSQLDataReader.Item("ProviderID")) = False Then
                _nProviderID = objSQLDataReader.Item("ProviderID")
            Else
                _nProviderID = 0
            End If

            If IsDBNull(objSQLDataReader.Item("Signature")) = False Then
                Dim arrPicture() As Byte = CType(objSQLDataReader.Item("Signature"), Byte())
                Dim ms As New MemoryStream(arrPicture)
                _imgSignature = Image.FromStream(ms)
                ms.Close()
            End If

            If IsDBNull(objSQLDataReader.Item("IsPasswordReset")) = False Then
                _IsPasswordReset = objSQLDataReader.Item("IsPasswordReset")
            End If

            If IsDBNull(objSQLDataReader.Item("IsAuditTrail")) = False Then
                _IsAuditTrail = objSQLDataReader.Item("IsAuditTrail")
            End If

            If IsDBNull(objSQLDataReader.Item("nAccessDenied")) = False Then
                _blnAccessDenied = objSQLDataReader.Item("nAccessDenied")
            Else
                _blnAccessDenied = False
            End If

            If IsDBNull(objSQLDataReader.Item("IsExchangeUser")) = False Then
                _bIsExchangeUser = objSQLDataReader.Item("IsExchangeUser")
            Else
                _bIsExchangeUser = False
            End If

            If (_bIsExchangeUser = True) Then
                If IsDBNull(objSQLDataReader.Item("sExchangeLogin")) = False Then
                    _sExchangeLogin = objSQLDataReader.Item("sExchangeLogin")
                Else
                    _sExchangeLogin = ""
                End If
                If IsDBNull(objSQLDataReader.Item("sExchangePassword")) = False Then
                    _sExchangePassword = objSQLDataReader.Item("sExchangePassword")
                Else
                    _sExchangePassword = ""
                End If
            End If

            'Added by Ujwala Atre as on 20101004 - Added Emergency Access to patient chart
            If IsDBNull(objSQLDataReader.Item("IsPatientChartAccess")) = False Then
                _blnPChartAccess = objSQLDataReader.Item("IsPatientChartAccess")
                If IsDBNull(objSQLDataReader.Item("sAccessPassword")) = False Then
                    _sEAPassword = objSQLDataReader.Item("sAccessPassword")
                Else
                    _sEAPassword = ""
                End If
                'Added Valid upto Date for Emergency Access Password as on 12032010
                If IsDBNull(objSQLDataReader.Item("dtValidupto")) = False Then
                    _ValidDt = objSQLDataReader.Item("dtValidupto")
                Else
                    _ValidDt = Date.Now.Date
                End If
            Else
                _blnPChartAccess = False
                _sEAPassword = ""
                'Added Valid upto Date for Emergency Access Password as on 12032010
                _ValidDt = Date.Now.Date
            End If

            '-------------------------------
            If IsDBNull(objSQLDataReader.Item("WindowsLoginName")) = False Then
                _WindowLoginName = objSQLDataReader.Item("WindowsLoginName")
            Else
                _WindowLoginName = ""
            End If
            '------------------------------------------
        End If

        objSQLDataReader.Close()

        Dim objCmdUserGroups As New SqlCommand
        Dim objSQLDataReaderUserGroups As SqlDataReader
        objCmdUserGroups.CommandType = CommandType.StoredProcedure
        objCmdUserGroups.CommandText = "gsp_RetrieveUserGroups"

        Dim objParaGroupUserID As New SqlParameter
        With objParaGroupUserID
            .ParameterName = "@UserID"
            .Value = nUserID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With

        objCmdUserGroups.Parameters.Add(objParaGroupUserID)
        objCmdUserGroups.Connection = objCon
        objSQLDataReaderUserGroups = objCmdUserGroups.ExecuteReader
        If objSQLDataReaderUserGroups.HasRows = True Then
            Dim clUserGroups As New Collection
            While objSQLDataReaderUserGroups.Read
                clUserGroups.Add(objSQLDataReaderUserGroups.Item(0))
            End While
            _clGroups = clUserGroups
        End If
        objSQLDataReaderUserGroups.Close()
        objCmdUserGroups = Nothing
        objParaGroupUserID = Nothing
        objSQLDataReaderUserGroups = Nothing

        Dim objCmdUserRights As New SqlCommand
        Dim objSQLDataReaderUserRights As SqlDataReader
        objCmdUserRights.CommandType = CommandType.StoredProcedure

        'the following stored procedure used will retrieve the rights IDs
        objCmdUserRights.CommandText = "gsp_RetrieveUserRights_Admin"

        Dim objParaRightsUserID As New SqlParameter
        With objParaRightsUserID
            .ParameterName = "@UserID"
            .Value = nUserID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmdUserRights.Parameters.Add(objParaRightsUserID)

        'Parameter Added for ApplicationType 0=gloEMR 1=gloPM
        Dim objParaApplicatioType As New SqlParameter
        With objParaApplicatioType
            .ParameterName = "@ApplicationType"
            .Value = gnApplicationType
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmdUserRights.Parameters.Add(objParaApplicatioType)

        objCmdUserRights.Connection = objCon
        objSQLDataReaderUserRights = objCmdUserRights.ExecuteReader
        If objSQLDataReaderUserRights.HasRows = True Then
            Dim clUserRights As New Collection
            While objSQLDataReaderUserRights.Read
                clUserRights.Add(objSQLDataReaderUserRights.Item(0))
            End While
            _clRights = clUserRights
        End If
        objSQLDataReaderUserRights.Close()
        objCmdUserRights = Nothing
        objParaRightsUserID = Nothing
        objSQLDataReaderUserRights = Nothing

        'Added Rahul for AuditRights on 20101019
        Dim objCmdAuditRights As New SqlCommand
        Dim objSQLDataReaderAuditRights As SqlDataReader
        objCmdAuditRights.CommandType = CommandType.StoredProcedure

        objCmdAuditRights.CommandText = "gsp_RetrieveAuditRights_Admin"

        Dim objParaAuditRightsUserID As New SqlParameter
        With objParaAuditRightsUserID
            .ParameterName = "@nUserId"
            .Value = nUserID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With

        objCmdAuditRights.Parameters.Add(objParaAuditRightsUserID)
        objCmdAuditRights.Connection = objCon
        objSQLDataReaderAuditRights = objCmdAuditRights.ExecuteReader

        If objSQLDataReaderAuditRights.HasRows = True Then
            Dim clUserRights As New Collection
            While objSQLDataReaderAuditRights.Read
                clUserRights.Add(objSQLDataReaderAuditRights.Item(1))
            End While
            _clAuditRights = clUserRights
        End If

        objSQLDataReaderAuditRights.Close()
        objCmdAuditRights = Nothing
        objParaAuditRightsUserID = Nothing
        objSQLDataReaderAuditRights = Nothing

        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
    End Sub

    Public Function CheckUserExists(ByVal strUserName As String, Optional ByVal nUserID As Int64 = 0, Optional ByVal strWindowLoginName As String = "") As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_CheckUserExists"
        Dim objParaUserName As New SqlParameter
        With objParaUserName
            .ParameterName = "@UserName"
            .Value = strUserName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaUserName)

        If nUserID <> 0 Then
            Dim objParaUserID As New SqlParameter
            With objParaUserID
                .ParameterName = "@UserID"
                .Value = nUserID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaUserID)
        End If

        If strWindowLoginName <> "" Then
            Dim objParaWindowLoginName As New SqlParameter
            With objParaWindowLoginName
                .ParameterName = "@WindowLoginName"
                .Value = strWindowLoginName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaWindowLoginName)
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

    Public Function AddUser(ByVal strUserName As String, ByVal strPassword As String, ByVal strNickName As String, ByVal dtDOB As Date, ByVal nProviderID As Int64, Optional ByVal strFirstName As String = "", Optional ByVal strMiddleName As String = "", Optional ByVal strLastName As String = "", Optional ByVal strSSNNo As String = "", Optional ByVal strGender As String = "", Optional ByVal strMaritalStatus As String = "", Optional ByVal strAddress As String = "", Optional ByVal strStreet As String = "", Optional ByVal strCity As String = "", Optional ByVal strState As String = "", Optional ByVal strZIP As String = "", Optional ByVal strPhoneNo As String = "", Optional ByVal strMobileNo As String = "", Optional ByVal strFAX As String = "", Optional ByVal strEmail As String = "", Optional ByVal blnAdministrator As Boolean = False) As Boolean
        Try
            _sErrorMessage = ""
            Dim objCon As New SqlConnection
            objCon.ConnectionString = _sConnectionString
            Dim objCmd As New SqlCommand
            Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InsertUser"

            Dim objParaUserID As New SqlParameter
            With objParaUserID
                .ParameterName = "@UserID"
                .Direction = ParameterDirection.Output
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaUserID)

            Dim objParaUserName As New SqlParameter
            With objParaUserName
                .ParameterName = "@LoginName"
                .Value = strUserName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaUserName)

            Dim objParaPassword As New SqlParameter
            With objParaPassword
                .ParameterName = "@Password"
                .Value = strPassword
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaPassword)

            Dim objParaNickName As New SqlParameter
            With objParaNickName
                .ParameterName = "@NickName"
                .Value = strNickName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaNickName)

            Dim objParaFirstName As New SqlParameter
            With objParaFirstName
                .ParameterName = "@FirstName"
                .Value = strFirstName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFirstName)

            Dim objParaMiddleName As New SqlParameter
            With objParaMiddleName
                .ParameterName = "@MiddleName"
                .Value = strMiddleName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMiddleName)

            Dim objParaLastName As New SqlParameter
            With objParaLastName
                .ParameterName = "@LastName"
                .Value = strLastName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLastName)

            Dim objParaSSN As New SqlParameter
            With objParaSSN
                .ParameterName = "@SSNNo"
                .Value = strSSNNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaSSN)

            Dim objParaDOB As New SqlParameter
            With objParaDOB
                .ParameterName = "@DOB"
                .Value = dtDOB
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaDOB)

            Dim objParaGender As New SqlParameter
            With objParaGender
                .ParameterName = "@Gender"
                .Value = strGender
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaGender)

            Dim objParaMaritalStatus As New SqlParameter
            With objParaMaritalStatus
                .ParameterName = "@MaritalStatus"
                .Value = strMaritalStatus
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMaritalStatus)

            Dim objParaAddress As New SqlParameter
            With objParaAddress
                .ParameterName = "@Address"
                .Value = strAddress
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaAddress)

            Dim objParaStreet As New SqlParameter
            With objParaStreet
                .ParameterName = "@Street"
                .Value = strStreet
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaStreet)

            Dim objParaCity As New SqlParameter
            With objParaCity
                .ParameterName = "@City"
                .Value = strCity
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCity)

            Dim objParaState As New SqlParameter
            With objParaState
                .ParameterName = "@State"
                .Value = strState
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaState)

            Dim objParaZIP As New SqlParameter
            With objParaZIP
                .ParameterName = "@ZIP"
                .Value = strZIP
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaZIP)

            Dim objParaPhoneNo As New SqlParameter
            With objParaPhoneNo
                .ParameterName = "@PhoneNo"
                .Value = strPhoneNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaPhoneNo)

            Dim objParaMobileNo As New SqlParameter
            With objParaMobileNo
                .ParameterName = "@MobileNo"
                .Value = strMobileNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMobileNo)

            Dim objParaFAX As New SqlParameter
            With objParaFAX
                .ParameterName = "@FAX"
                .Value = strFAX
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaFAX)


            Dim objParaEmail As New SqlParameter
            With objParaEmail
                .ParameterName = "@Email"
                .Value = strEmail
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaEmail)


            Dim objParaAdministrator As New SqlParameter
            With objParaAdministrator
                .ParameterName = "@Administrator"
                If blnAdministrator = False Then
                    .Value = 0
                Else
                    .Value = 1
                End If
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaAdministrator)

            Dim objParaProviderID As New SqlParameter
            With objParaProviderID
                .ParameterName = "@ProviderID"
                .Value = nProviderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaProviderID)

            Dim oMachineID As New SqlParameter
            With oMachineID
                .ParameterName = "@MachineID"
                .Value = mdlGeneral.GetPrefixTransactionID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(oMachineID)

            'Changed by Ashish on 14-Nov-2013 By default Audit Logging should be enabled for new user.
            Dim objbIsAuditTrail As New SqlParameter
            With objbIsAuditTrail
                .ParameterName = "@IsAuditTrail"
                .Value = 1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objbIsAuditTrail)

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()

            'For TVP_UserRights Changed by Ashish on 14-Nov-2013 from multiple database calls to a single call
            Dim dtUserRights As New DataTable("TVP_UserRights")
            With dtUserRights
                .Columns.Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
                .Columns.Add(New DataColumn("nRightsID", System.Type.GetType("System.Int64")))
            End With

            Dim drUserRight As DataRow = Nothing
            Dim nCount As Integer
            For nCount = 1 To _clRights.Count
                drUserRight = dtUserRights.NewRow
                drUserRight("nUserID") = objParaUserID.Value
                drUserRight("nRightsID") = Convert.ToInt32(_clRights.Item(nCount))
                dtUserRights.Rows.Add(drUserRight)
            Next

            Dim objTVP_UserRights As New SqlParameter
            With objTVP_UserRights
                .ParameterName = "TVP_UserRights"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Structured
                .Value = dtUserRights
            End With

            'For TVP_UserAuditRights
            Dim dtUserAuditRights As New DataTable("TVP_UserAuditRights")
            With dtUserAuditRights
                .Columns.Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
                .Columns.Add(New DataColumn("nModuleID", System.Type.GetType("System.Int64")))
            End With

            Dim drUserAuditRight As DataRow = Nothing

            'Added Rahul for AuditRights on 20101019
            Dim nCnt As Integer

            For nCnt = 1 To _clAuditRights.Count
                drUserAuditRight = dtUserAuditRights.NewRow
                drUserAuditRight("nUserID") = objParaUserID.Value
                drUserAuditRight("nModuleID") = _clAuditRights.Item(nCnt)
                dtUserAuditRights.Rows.Add(drUserAuditRight)
            Next

            drUserAuditRight = Nothing

            Dim objTVP_UserAuditRights As New SqlParameter
            With objTVP_UserAuditRights
                .ParameterName = "TVP_UserAuditRights"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Structured
                .Value = dtUserAuditRights
            End With

            Dim sqlCommand As New SqlCommand
            With sqlCommand
                .CommandType = CommandType.StoredProcedure
                .CommandText = "gsp_InsertDefaultUserSettings"
                .Connection = objCon

                .Parameters.Add(objTVP_UserRights)
                .Parameters.Add(objTVP_UserAuditRights)

                .Connection.Open()
                .ExecuteNonQuery()
                .Connection.Close()
            End With

            dtUserAuditRights.Clear()
            dtUserAuditRights.Dispose()
            dtUserAuditRights = Nothing

            dtUserRights.Clear()
            dtUserRights.Dispose()
            dtUserRights = Nothing
            For nCount = 1 To _clGroups.Count
                Dim objcmdChild As New SqlCommand
                objcmdChild.CommandType = CommandType.StoredProcedure
                objcmdChild.CommandText = "gsp_InsertUserGroups"

                Dim objParaGroupUserID As New SqlParameter
                With objParaGroupUserID
                    .ParameterName = "@UserID"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                    .Value = objParaUserID.Value
                End With
                objcmdChild.Parameters.Add(objParaGroupUserID)

                Dim objParaGroupName As New SqlParameter
                With objParaGroupName
                    .ParameterName = "@GroupName"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                    .Value = _clGroups.Item(nCount)
                End With
                objcmdChild.Parameters.Add(objParaGroupName)
                objcmdChild.Connection = objCon
                objcmdChild.ExecuteNonQuery()

                objcmdChild = Nothing
                objParaGroupUserID = Nothing
                objParaGroupName = Nothing
            Next

            objCon.Close()
            objCmd = Nothing
            objCon = Nothing
            Return True
        Catch ex As Exception
            _sErrorMessage = ex.Message
            Return False
        End Try
    End Function

#End Region


End Class
