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

Public Class clsUsers

#Region "private variables"


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
    Dim _nProviderID As Int64 = 0
    Dim _sProviderName As String
    Dim _clGroups As New Collection
    Dim _clRights As New Collection
    Dim _sSpeciality As String = ""
    Dim _imgSignature As Image
    Dim _IsPasswordReset As Boolean = False
    Dim _IsAuditTrail As Boolean
    Dim _blnAccessDenied As Boolean = False
    Dim _blnCoSign As Boolean
    Dim _bIsSecurity As Boolean

    'sarika 11th sept 07
    Dim _sConnectionString As String = ""
    Dim _sErrorMessage As String = ""
    '------------------------------
    Dim _bIsExchangeUser As Boolean = False
    Dim _sExchangeLogin As String = ""
    Dim _sExchangePassword As String = ""

    '  Added by Mahendra - For Emergency Access Password 
    Dim _blnPChartAccess As Boolean = False
    Dim _sEAPassword As String
    Dim _ValidDt As Date
    Dim _BusinessCenterID As Int64 = 0
    Dim _WindowLoginName As String

    ''OCP Portal Details
    Dim _sOCPLoginName As String
    Dim _sOCPLoginPassword As String
    Dim _sOCPLoginType As String
    Dim _bIsAllowPortalAccess As Boolean = False
    Dim _bIsSameAsUserDetails As Boolean = False
    ''OCP Portal Details
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
            'If _nBlockStatus = 0 Then
            '    Return True
            'Else
            '    Return False
            'End If
            Return _nBlockStatus
        End Get
        Set(ByVal Value As Boolean)
            'If Value = True Then
            '    _nBlockStatus = 0
            'Else
            '    _nBlockStatus = 1
            'End If
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

    '_IsAuditTrail

    Public Property IsAuditTrail() As Boolean
        Get
            Return _IsAuditTrail
        End Get
        Set(ByVal Value As Boolean)
            _IsAuditTrail = Value
        End Set
    End Property
    '  Added by Mahendra - For Emergency Access Password 
    Public Property EAPChart() As Boolean
        Get
            Return _blnPChartAccess
        End Get
        Set(ByVal Value As Boolean)
            _blnPChartAccess = Value
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
    Public Property EAPassword() As String
        Get
            Return _sEAPassword
        End Get
        Set(ByVal Value As String)
            _sEAPassword = Value
        End Set
    End Property
    '********

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
            'If _blnAccessDenied = 0 Then
            '    Return False
            'Else
            '    Return True
            'End If

        End Get
        Set(ByVal Value As Boolean)
            _blnAccessDenied = Value
            'If Value = True Then
            '    _blnAccessDenied = 1
            'Else
            '    _blnAccessDenied = 0
            'End If
        End Set
    End Property

    'sarika 11th sept 07 
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



    '------------------------
    ''Sandip Darade 20090722
    ''Add exchange user 
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

    Public Property BusinessCenterID() As Int64
        Get
            Return _BusinessCenterID
        End Get
        Set(ByVal value As Int64)
            _BusinessCenterID = value
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

    ''OCP Portal Details
    Public Property OCPLoginName() As String
        Get
            Return _sOCPLoginName
        End Get
        Set(ByVal Value As String)
            _sOCPLoginName = Value
        End Set
    End Property

    Public Property OCPLoginPassword() As String
        Get
            Return _sOCPLoginPassword
        End Get
        Set(ByVal Value As String)
            _sOCPLoginPassword = Value
        End Set
    End Property

    Public Property OCPLoginType() As String
        Get
            Return _sOCPLoginType
        End Get
        Set(ByVal Value As String)
            _sOCPLoginType = Value
        End Set
    End Property

    Public Property IsAllowPortalAccess() As Boolean
        Get
            Return _bIsAllowPortalAccess
        End Get
        Set(ByVal Value As Boolean)
            _bIsAllowPortalAccess = Value
        End Set
    End Property

    Public Property IsSameAsUserDetails() As Boolean
        Get
            Return _bIsSameAsUserDetails
        End Get
        Set(ByVal value As Boolean)
            _bIsSameAsUserDetails = value
        End Set
    End Property
    ''OCP Portal Details

#End Region


    Enum enmUsersType
        All
        Active
        NonActive
    End Enum


#Region "Constructor"

    'sarika 11th sept 07

    Public Sub New()

    End Sub
    Public Sub New(ByVal sConnectionString As String)
        _sConnectionString = sConnectionString
    End Sub
    '-------------------------------
#End Region

#Region "methods"


    Public Function PopulateUsers(ByVal enmUser As enmUsersType) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand

        ' Dim objSQLDataReader As SqlDataReader
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
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        Return dsData.Tables(0)
    End Function

    Public Function AddUser() As Boolean
        Return AddUser(_sUserName, _sPassword, _sEAPassword, _blnPChartAccess, _ValidDt, _sNickName, _dtDOB, _nProviderID, _imgSignature, _IsPasswordReset, _IsAuditTrail, _blnCoSign, _bIsSecurity, _blnAccessDenied, _BusinessCenterID, _sFirstName, _sMiddleName, _sLastName, _sSSNno, _sGender, _sMaritalStatus, _sAddress, _sAddress2, _sStreet, _sCity, _sState, _sZIP, _sPhoneNo, _sMobileNo, _sFAX, _sEmail, _blnAdministrator, _sCountry, _sCounty, _sSpeciality, _WindowLoginName, _bIsSameAsUserDetails, _bIsAllowPortalAccess, _sOCPLoginName, _sOCPLoginPassword, _sOCPLoginType)
    End Function

    Public Function AddUser(ByVal strUserName As String, ByVal strPassword As String, ByVal strEAPassword As String, ByVal blnEAPchart As Boolean, ByVal dtValidDt As Date, ByVal strNickName As String, ByVal dtDOB As Date, ByVal nProviderID As Int64, ByVal imgSign As Image, ByVal blnPasswordReset As Boolean, ByVal blnAuditTrail As Boolean, ByVal blnCosign As Boolean, ByVal bIssecurityUser As Boolean, ByVal blnAccessDenied As Boolean, ByVal BusinessCenterID As Int64, Optional ByVal strFirstName As String = "", Optional ByVal strMiddleName As String = "", Optional ByVal strLastName As String = "", Optional ByVal strSSNNo As String = "", Optional ByVal strGender As String = "", Optional ByVal strMaritalStatus As String = "", Optional ByVal strAddress As String = "", Optional ByVal strAddress2 As String = "", Optional ByVal strStreet As String = "", Optional ByVal strCity As String = "", Optional ByVal strState As String = "", Optional ByVal strZIP As String = "", Optional ByVal strPhoneNo As String = "", Optional ByVal strMobileNo As String = "", Optional ByVal strFAX As String = "", Optional ByVal strEmail As String = "", Optional ByVal blnAdministrator As Boolean = False, Optional ByVal sCountry As String = "", Optional ByVal sCounty As String = "", Optional ByVal strspeciality As String = "", Optional ByVal strWindowLoginName As String = "", Optional ByVal bIsSameAsUserDetails As Boolean = False, Optional ByVal bIsAllowPortalAccess As Boolean = False, Optional ByVal strOCPLoginName As String = "", Optional ByVal strOCPLoginPassword As String = "", Optional ByVal strOCPLoginType As String = "") As Boolean

        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '-----
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
        '  Added by Mahendra - For Emergency Access Password 
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


        Dim objParaValidDt As New SqlParameter
        With objParaValidDt
            .ParameterName = "@dtValidupto"
            .Value = dtValidDt
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaValidDt)
        'end
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

        Dim objParaCountry As New SqlParameter          ''country
        With objParaCountry
            .ParameterName = "@sCountry"
            .Value = sCountry
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaCountry)

        Dim objParaCounty As New SqlParameter           ''county
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


        '''''''''''''
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

        'IsExchangeUser=@IsExchangeUser, sExchangeLogin=@sExchangeLogin , sExchangePassword=@sExchangePassword 
        Dim objParaExchangePassword As New SqlParameter
        With objParaExchangePassword
            .ParameterName = "@sExchangePassword"
            .Value = _sExchangePassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaExchangePassword)
        '''''''''''

        '' SUDHIR 20090624 '' MachineID ''
        Dim oMachineID As New SqlParameter
        With oMachineID
            .ParameterName = "@MachineID"
            .Value = mdlGeneral.GetPrefixTransactionID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(oMachineID)
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

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        Dim nCount As Integer
        For nCount = 1 To _clRights.Count
            Dim objcmdChild As New SqlCommand
            objcmdChild.CommandType = CommandType.StoredProcedure
            objcmdChild.CommandText = "gsp_InsertUserRights"

            Dim objParaRightsUserID As New SqlParameter
            With objParaRightsUserID
                .ParameterName = "@UserID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
                .Value = objParaUserID.Value
            End With
            objcmdChild.Parameters.Add(objParaRightsUserID)

            Dim objParaRightsName As New SqlParameter
            With objParaRightsName
                .ParameterName = "@RightsID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                .Value = _clRights.Item(nCount)
            End With
            objcmdChild.Parameters.Add(objParaRightsName)
            objcmdChild.Connection = objCon
            objcmdChild.ExecuteNonQuery()

            objcmdChild = Nothing
            objParaRightsUserID = Nothing
            objParaRightsName = Nothing
        Next

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

        'Insert Business Center Associated with User
        Dim objcmdInsert As New SqlCommand
        Dim objInsertBusinessCenter As New SqlCommand
        objcmdInsert.CommandType = CommandType.StoredProcedure
        objcmdInsert.CommandText = "BL_INUP_BusinessCenter_UsersAssociation"
        Dim objParaInsertBusinessCenterUserID As New SqlParameter
        With objParaInsertBusinessCenterUserID
            .ParameterName = "@nUserID"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
            .Value = objParaUserID.Value
        End With
        objcmdInsert.Parameters.Add(objParaInsertBusinessCenterUserID)

        Dim objParanID As New SqlParameter
        With objParanID
            .ParameterName = "@nID"
            .Direction = ParameterDirection.Output
            .SqlDbType = SqlDbType.BigInt
        End With
        objcmdInsert.Parameters.Add(objParanID)

        Dim objParaInsertBusinessCenterID As New SqlParameter
        With objParaInsertBusinessCenterID
            .ParameterName = "@nBusinessCenterID"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
            .Value = BusinessCenterID
        End With
        objcmdInsert.Parameters.Add(objParaInsertBusinessCenterID)

        objcmdInsert.Connection = objCon
        objcmdInsert.ExecuteNonQuery()


        objCon.Close()
        'objCmd = Nothing
        'objCon = Nothing

        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If

        Dim bResult As Boolean
        If strOCPLoginName <> "" And strOCPLoginPassword <> "" Then
            bResult = UpdateUserPortalAccess(objParaUserID.Value, UserName, bIsSameAsUserDetails, bIsAllowPortalAccess, strOCPLoginName, strOCPLoginPassword, strOCPLoginType, False)
        Else
            bResult = True
        End If

        If bResult Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function UpdateUserPortalAccess(UserID As Object, UserName As String, SameAsUser As Boolean, AllowPortalAccess As Boolean, OCPLoginName As String, OCPLoginPassword As String, OCPLoginType As String, bIsModifyCalled As Boolean) As Boolean
        Dim Result As Boolean = False
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()

            '---
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_INUP_UserPortalAccess"

            Dim objParaUserID As New SqlParameter
            With objParaUserID
                .ParameterName = "@nUserID"
                .Value = UserID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaUserID)


            Dim objParaLoginName As New SqlParameter
            With objParaLoginName
                .ParameterName = "@sLoginName"
                .Value = OCPLoginName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLoginName)

            Dim objParaLoginPassword As New SqlParameter
            With objParaLoginPassword
                .ParameterName = "@sLoginPassword"
                .Value = OCPLoginPassword
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLoginPassword)

            Dim objParaLoginType As New SqlParameter
            With objParaLoginType
                .ParameterName = "@sLoginType"
                .Value = OCPLoginType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLoginType)

            Dim objParaAllowAccess As New SqlParameter
            With objParaAllowAccess
                .ParameterName = "@bIsAllowPortalAccess"
                .Value = AllowPortalAccess
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaAllowAccess)

            Dim objParaSameAsUser As New SqlParameter
            With objParaSameAsUser
                .ParameterName = "@bIsSameAsUser"
                .Value = SameAsUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaSameAsUser)

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            Result = True
            Dim objAudit As New clsAudit
            objAudit.CreateLog(If(bIsModifyCalled = False, clsAudit.enmActivityType.Add, clsAudit.enmActivityType.Modify), If(SameAsUser = True, UserName + " is using same credentials as User Details for Online Charge Posting", UserName + " is using different credentials for Online Charge Posting"), gstrLoginName, gstrClientMachineName)
            objAudit.CreateLog(If(bIsModifyCalled = False, clsAudit.enmActivityType.Add, clsAudit.enmActivityType.Modify), If(AllowPortalAccess = True, "Access is allowed to " + UserName + " for Online Charge Posting", "Access is blocked to " + UserName + " for Online Charge Posting"), gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
        Catch ex As Exception
            Result = False
        Finally
            objCon.Close()
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
        End Try

        Return Result
    End Function
    Public Function BlockUnblockUser(ByVal nUserID As Int64, ByVal blnBlock As Boolean) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        ' Dim objSQLDataReader As SqlDataReader
        '----
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
            'If blnBlock = True Then
            '    .Value = 0
            'Else
            '    .Value = 1
            'End If

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
        'objCmd = Nothing
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        Return True
    End Function

    Public Function EditUser(ByVal nUserID As Int64) As Boolean
        Return EditUser(nUserID, _sUserName, _sPassword, _sEAPassword, _blnPChartAccess, _ValidDt, _sNickName, _dtDOB, _imgSignature, _IsPasswordReset, _blnAccessDenied, _IsAuditTrail, _blnCoSign, _bIsSecurity, _BusinessCenterID, _sFirstName, _sMiddleName, _sLastName, _sSSNno, _sGender, _sMaritalStatus, _sAddress, _sAddress2, _sStreet, _sCity, _sState, _sZIP, _sPhoneNo, _sMobileNo, _sFAX, _sEmail, _blnAdministrator, _nBlockStatus, _nProviderID, _sCountry, _sCounty, _sSpeciality, _WindowLoginName, _bIsSameAsUserDetails, _bIsAllowPortalAccess, _sOCPLoginName, _sOCPLoginPassword, _sOCPLoginType)
    End Function

    Public Function EditUser(ByVal nUserID As Int64, ByVal strUserName As String, ByVal strPassword As String, ByVal strEAPassword As String, ByVal blnEAPchart As Boolean, ByVal dtValidDt As Date, ByVal strNickName As String, ByVal dtDOB As Date, ByVal imgSignature As Image, ByVal blnPasswordReset As Boolean, ByVal blnAccessDenied As Boolean, ByVal blnAuditTrail As Boolean, ByVal blnCoSign As Boolean, ByVal blnIssecurityUser As Boolean, ByVal BusinessCenterID As Int64, Optional ByVal strFirstName As String = "", Optional ByVal strMiddleName As String = "", Optional ByVal strLastName As String = "", Optional ByVal strSSNNo As String = "", Optional ByVal strGender As String = "", Optional ByVal strMaritalStatus As String = "", Optional ByVal strAddress As String = "", Optional ByVal strAddress2 As String = "", Optional ByVal strStreet As String = "", Optional ByVal strCity As String = "", Optional ByVal strState As String = "", Optional ByVal strZIP As String = "", Optional ByVal strPhoneNo As String = "", Optional ByVal strMobileNo As String = "", Optional ByVal strFAX As String = "", Optional ByVal strEmail As String = "", Optional ByVal blnAdministrator As Boolean = False, Optional ByVal nBlockUserStatus As Boolean = False, Optional ByVal nProviderID As Int64 = 0, Optional ByVal sCountry As String = "", Optional ByVal sCounty As String = "", Optional ByVal sSpecialty As String = "", Optional ByVal strWindowLoginName As String = "", Optional ByVal bIsSameAsUserDetails As Boolean = False, Optional ByVal bIsAllowPortalAccess As Boolean = False, Optional ByVal strOCPLoginName As String = "", Optional ByVal strOCPLoginPassword As String = "", Optional ByVal strOCPLoginType As String = "") As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '---
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_UpdateUser"

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

        '****************
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
        '' Added Valid upto Date for Emergency Access Password as on 12032010
        Dim objParaValidDt As New SqlParameter
        With objParaValidDt
            .ParameterName = "@dtValidupto"
            .Value = dtValidDt
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaValidDt)
        '****************
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


        Dim objParaCountry As New SqlParameter              ''Country
        With objParaCountry
            .ParameterName = "@sCountry"
            .Value = sCountry
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaCountry)

        Dim objParaCounty As New SqlParameter               ''County
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

        Dim objParaBlockUser As New SqlParameter
        With objParaBlockUser
            .ParameterName = "@BlockUser"
            'sarika 20090518
            If nBlockUserStatus = True Then
                .Value = 1
            Else
                .Value = 0
            End If
            '---
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

        'Dim objfrm As New frmUserMGMT
        If IsNothing(_imgSignature) = False Then
            Dim ms As New MemoryStream
            '_imgSignature.Save(ms, _imgSignature.RawFormat)
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


        '''''''''''''
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

        'IsExchangeUser=@IsExchangeUser, sExchangeLogin=@sExchangeLogin , sExchangePassword=@sExchangePassword 
        Dim objParaExchangePassword As New SqlParameter
        With objParaExchangePassword
            .ParameterName = "@sExchangePassword"
            .Value = _sExchangePassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaExchangePassword)
        '''''''''''
        Dim objParaSpecialty As New SqlParameter
        With objParaSpecialty
            .ParameterName = "@sSpecialty"
            .Value = _sSpeciality
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSpecialty)
        '''''''''
        Dim objParaWindowLoginName As New SqlParameter
        With objParaWindowLoginName
            .ParameterName = "@WindowLoginName"
            .Value = strWindowLoginName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaWindowLoginName)

        objCmd.Parameters.Add(objIsSecurityUserFlag)

        '''''''''''
        ''If IsNothing(_imgSignature) = False Then
        ''    Dim ms As New MemoryStream
        ''    If objfrm.blnlogochanged = True Then
        ''        ' _imgSignature.Save(ms, Imaging.ImageFormat.Bmp)
        ''        _imgSignature.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp)
        ''        Dim arrImage() As Byte = ms.GetBuffer
        ''        ms.Close()

        ''        Dim objParaSignature As New SqlParameter
        ''        With objParaSignature
        ''            .ParameterName = "@imgSignature"
        ''            .Value = arrImage
        ''            .Direction = ParameterDirection.Input
        ''            .SqlDbType = SqlDbType.Image
        ''        End With
        ''        objCmd.Parameters.Add(objParaSignature)
        ''    Else
        ''        ' '_imgSignature = objfrm.picSignature.Image
        ''        'Dim objParaSignature As New SqlParameter
        ''        'With objParaSignature
        ''        '    .ParameterName = "@imgSignature"
        ''        '    ' .Value = objfrm.picSignature.Image
        ''        '    .Value = _imgSignature
        ''        '    .Direction = ParameterDirection.Input
        ''        '    .SqlDbType = SqlDbType.Image
        ''        'End With
        ''        ' _imgSignature.Save(ms, Imaging.ImageFormat.Bmp)
        ''        _imgSignature.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp)
        ''        Dim arrImage() As Byte = ms.GetBuffer
        ''        ms.Close()

        ''        Dim objParaSignature As New SqlParameter
        ''        With objParaSignature
        ''            .ParameterName = "@imgSignature"
        ''            .Value = arrImage
        ''            .Direction = ParameterDirection.Input
        ''            .SqlDbType = SqlDbType.Image
        ''        End With

        ''        objCmd.Parameters.Add(objParaSignature)

        ''    End If
        ''End If
        '''''''''''''''''''''''''

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()

        'Delete All User Groups
        Dim objcmdDelete As New SqlCommand
        objcmdDelete.CommandType = CommandType.StoredProcedure
        objcmdDelete.CommandText = "gsp_DeleteUserGroups"
        Dim objParaDeleteUserGroupID As New SqlParameter
        With objParaDeleteUserGroupID
            .ParameterName = "@UserID"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
            .Value = objParaUserID.Value
        End With
        objcmdDelete.Parameters.Add(objParaDeleteUserGroupID)
        objcmdDelete.Connection = objCon
        objcmdDelete.ExecuteNonQuery()

        'Delete All User Rights
        Dim objApplicationType As New SqlParameter
        With objApplicationType
            .ParameterName = "@ApplicationType"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
            .Value = 1
        End With
        objcmdDelete.Parameters.Add(objApplicationType)
        objcmdDelete.CommandText = "gsp_DeleteUserRights"
        objcmdDelete.ExecuteNonQuery()

        Dim nCount As Integer
        For nCount = 1 To _clRights.Count
            Dim objcmdChild As New SqlCommand
            objcmdChild.CommandType = CommandType.StoredProcedure

            Dim objParaRightsUserID As New SqlParameter
            With objParaRightsUserID
                .ParameterName = "@UserID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
                .Value = objParaUserID.Value
            End With
            objcmdChild.Parameters.Add(objParaRightsUserID)
            objcmdChild.Connection = objCon
            ''Sandip Darade  20090818
            objcmdChild.CommandText = "gsp_InsertUserRights"
            Dim objParaRightsName As New SqlParameter
            With objParaRightsName
                .ParameterName = "@RightsID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
                .Value = _clRights.Item(nCount)
            End With
            objcmdChild.Parameters.Add(objParaRightsName)

            objcmdChild.ExecuteNonQuery()

            objcmdChild = Nothing
            objParaRightsUserID = Nothing
            objParaRightsName = Nothing
        Next

        For nCount = 1 To _clGroups.Count
            Dim objcmdChild As New SqlCommand
            objcmdChild.CommandType = CommandType.StoredProcedure

            Dim objParaGroupUserID As New SqlParameter
            With objParaGroupUserID
                .ParameterName = "@UserID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
                .Value = objParaUserID.Value
            End With
            objcmdChild.Parameters.Add(objParaGroupUserID)
            objcmdChild.Connection = objCon

            objcmdChild.CommandText = "gsp_UpdateUserGroups"
            Dim objParaGroupName As New SqlParameter
            With objParaGroupName
                .ParameterName = "@GroupName"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                .Value = _clGroups.Item(nCount)
            End With
            objcmdChild.Parameters.Add(objParaGroupName)
            objcmdChild.ExecuteNonQuery()

            objcmdChild = Nothing
            objParaGroupUserID = Nothing
            objParaGroupName = Nothing
        Next

        'Insert Business Center Associated with User
        Dim objcmdInsert As New SqlCommand
        Dim objInsertBusinessCenter As New SqlCommand
        objcmdInsert.CommandType = CommandType.StoredProcedure
        objcmdInsert.CommandText = "BL_INUP_BusinessCenter_UsersAssociation"
        Dim objParaInsertBusinessCenterUserID As New SqlParameter
        With objParaInsertBusinessCenterUserID
            .ParameterName = "@nUserID"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
            .Value = objParaUserID.Value
        End With
        objcmdInsert.Parameters.Add(objParaInsertBusinessCenterUserID)

        Dim objParanID As New SqlParameter
        With objParanID
            .ParameterName = "@nID"
            .Direction = ParameterDirection.Output
            .SqlDbType = SqlDbType.BigInt
        End With
        objcmdInsert.Parameters.Add(objParanID)

        Dim objParaInsertBusinessCenterID As New SqlParameter
        With objParaInsertBusinessCenterID
            .ParameterName = "@nBusinessCenterID"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
            .Value = BusinessCenterID
        End With
        objcmdInsert.Parameters.Add(objParaInsertBusinessCenterID)

        objcmdInsert.Connection = objCon
        objcmdInsert.ExecuteNonQuery()





        objCon.Close()
        'objCmd = Nothing
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If

        Dim bResult As Boolean
        If strOCPLoginName <> "" And strOCPLoginPassword <> "" Then
            bResult = UpdateUserPortalAccess(objParaUserID.Value,UserName, bIsSameAsUserDetails, bIsAllowPortalAccess, strOCPLoginName, strOCPLoginPassword, strOCPLoginType, True)
        Else
            bResult = True
        End If

        If bResult Then
            Return True
        Else
            Return False
        End If

    End Function


    Public Sub SearchUser(ByVal nUserID As Int64)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
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
            '' SUDHIR 20090703 ''
            If IsDBNull(objSQLDataReader.Item("Address2")) = False Then
                _sAddress2 = objSQLDataReader.Item("Address2")
            Else
                _sAddress2 = ""
            End If
            '' END SUDHIR ''
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
            If IsDBNull(objSQLDataReader.Item("gloEMRAdministrator")) = False Then
                _blnAdministrator = objSQLDataReader.Item("gloEMRAdministrator")
            End If
            '_nBlockStatus = 1
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
            ''''''''''''
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

            '  Added by Mahendra - check Valid Date  for Emergency Access Password 
            If IsDBNull(objSQLDataReader.Item("IsPatientChartAccess")) = False Then
                _blnPChartAccess = objSQLDataReader.Item("IsPatientChartAccess")
                If IsDBNull(objSQLDataReader.Item("sAccessPassword")) = False Then
                    _sEAPassword = objSQLDataReader.Item("sAccessPassword")
                Else
                    _sEAPassword = ""
                End If
                If IsDBNull(objSQLDataReader.Item("dtValidupto")) = False Then
                    _ValidDt = objSQLDataReader.Item("dtValidupto")
                Else
                    _ValidDt = Date.Now.Date
                End If
            Else
                _blnPChartAccess = False
                _sEAPassword = ""
                _ValidDt = Date.Now.Date

            End If

            If IsDBNull(objSQLDataReader.Item("nBusinessCenterID")) = False Then
                _BusinessCenterID = objSQLDataReader.Item("nBusinessCenterID")
            Else
                _BusinessCenterID = 0
            End If

            If IsDBNull(objSQLDataReader.Item("WindowsLoginName")) = False Then
                _WindowLoginName = objSQLDataReader.Item("WindowsLoginName")
            Else
                _WindowLoginName = ""
            End If

            If IsDBNull(objSQLDataReader.Item("bIsSameAsUserDetails")) = False Then
                _bIsSameAsUserDetails = objSQLDataReader.Item("bIsSameAsUserDetails")
            Else
                _bIsSameAsUserDetails = False
            End If

            If IsDBNull(objSQLDataReader.Item("bIsAllowPortalAccess")) = False Then
                _bIsAllowPortalAccess = objSQLDataReader.Item("bIsAllowPortalAccess")
            Else
                _bIsAllowPortalAccess = False
            End If

            If IsDBNull(objSQLDataReader.Item("sOCPLoginName")) = False Then
                _sOCPLoginName = objSQLDataReader.Item("sOCPLoginName")
            Else
                _sOCPLoginName = ""
            End If

            If IsDBNull(objSQLDataReader.Item("_sOCPLoginPassword")) = False Then
                _sOCPLoginPassword = objSQLDataReader.Item("_sOCPLoginPassword")
            Else
                _sOCPLoginPassword = ""
            End If

            If IsDBNull(objSQLDataReader.Item("_sOCPLoginType")) = False Then
                _sOCPLoginType = objSQLDataReader.Item("_sOCPLoginType")
            Else
                _sOCPLoginType = ""
            End If
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
        '  objCmdUserRights.CommandText = "gsp_RetrieveUserRights"
        ''Sandip Darade 20090818 
        ''the following stored procedure used will retrieve the rights IDs
        objCmdUserRights.CommandText = "gsp_RetrieveUserRights_Admin"

        Dim objParaRightsUserID As New SqlParameter
        With objParaRightsUserID
            .ParameterName = "@UserID"
            .Value = nUserID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmdUserRights.Parameters.Add(objParaRightsUserID)

        ''Sandip Darade 20091103
        ''Added new parameter ApplicationType
        Dim objApplicationType As New SqlParameter
        With objApplicationType
            .ParameterName = "@ApplicationType"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmdUserRights.Parameters.Add(objApplicationType)


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


        objCon.Close()
        'objCmd = Nothing
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If

    End Sub

    Public Function CheckUserExists(ByVal strUserName As String, Optional ByVal nUserID As Int64 = 0, Optional ByVal strWindowLoginName As String = "") As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
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
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        If nCount = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Function OCPCheckUserExists(ByVal strUserName As String, Optional ByVal nUserID As Int64 = 0) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_OCPCheckUserNamePasswordExists"
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
        objCmd.Connection = objCon
        Dim nCount As Integer
        objCon.Open()
        nCount = objCmd.ExecuteScalar
        objCon.Close()
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        If nCount = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    'sarika 11th sept 07
    'for doctor
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

            '' SUDHIR 20090624 '' MachineID ''
            Dim oMachineID As New SqlParameter
            With oMachineID
                .ParameterName = "@MachineID"
                .Value = mdlGeneral.GetPrefixTransactionID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(oMachineID)

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            Dim nCount As Integer
            For nCount = 1 To _clRights.Count
                Dim objcmdChild As New SqlCommand
                objcmdChild.CommandType = CommandType.StoredProcedure
                objcmdChild.CommandText = "gsp_InsertUserRights"

                Dim objParaRightsUserID As New SqlParameter
                With objParaRightsUserID
                    .ParameterName = "@UserID"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                    .Value = objParaUserID.Value
                End With
                objcmdChild.Parameters.Add(objParaRightsUserID)

                Dim objParaRightsName As New SqlParameter
                With objParaRightsName
                    .ParameterName = "@RightsID"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                    .Value = _clRights.Item(nCount)
                End With
                objcmdChild.Parameters.Add(objParaRightsName)
                objcmdChild.Connection = objCon
                objcmdChild.ExecuteNonQuery()

                objcmdChild = Nothing
                objParaRightsUserID = Nothing
                objParaRightsName = Nothing
            Next

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
            'objCmd = Nothing
            'objCon = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
            Return True
        Catch ex As Exception
            _sErrorMessage = ex.Message
            Return False
        End Try
    End Function

    '------------------------------------------
    Public Shared Function GetBusinessCenter() As DataTable
        Dim dt As DataTable = Nothing


        '#Region " Get From DB & Add to Cache "
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim strQuery As String = ""

        Try
            strQuery = "SELECT nBusinessCenterID, sBusinessCenterCode, sDescription,sBusinessCenterCode + ' - ' + sDescription AS BusinessCenter , bIsActive " & " FROM dbo.BL_BusinessCenterCodes ORDER BY sBusinessCenterCode "
            oDB.Connect(False)
            oDB.Retrive_Query(strQuery, dt)
            oDB.Disconnect()

        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
            Return Nothing
        Catch generatedExceptionName As Exception
            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
            '#End Region
        End Try

        Return dt
    End Function



#End Region

    


End Class
