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
Public Class clsClinic

#Region " Private Variables"
    Dim _nClinicID As Integer
    Dim _sClinicName As String
    Dim _sAddress1 As String
    Dim _sAddress2 As String
    Dim _sStreet As String
    Dim _sCity As String
    Dim _sState As String
    Dim _sZIP As String
    Dim _sAreaCode As String
    Dim _sCountry As String     ''Country
    Dim _sCounty As String      ''County
    Dim _sContactName As String

    Dim _sPhoneNo As String
    Dim _sMobileNo As String
    Dim _sFAX As String
    Dim _sEmail As String
    Dim _sURL As String
    Dim _sTAXID As String
    Dim _sTaxonomyCode As String
    Dim _imgClinicLogo As Image
    Dim _sContactPersonName As String
    Dim _sContactPersonAddress1 As String
    Dim _sContactPersonAddress2 As String
    Dim _sContactPersonPhone As String
    Dim _sContactPersonFAX As String
    Dim _sContactPersonEmail As String
    Dim _sContactPersonMobile As String

    Dim _sErrorMessage As String = ""

    'sarika SiteId 20090708
    Dim _sSiteID As String = ""
    ''Sandip Darade 20091113
    Dim _sClinicNPI As String = ""
    Dim _bIsSameImage As Boolean = True
    Dim _sClinicLabel As String = ""
    Private Const COL_QUALIFIERIDVALUE_NPI As Int64 = 1
    'Private Const COL_QUALIFIERIDVALUE_TAXID As Int64 = 5
    Private Const COL_QUALIFIERIDVALUE_TAXID As Int64 = 3
    Private Const COL_ISSYSTEM_VALUE As Boolean = 1

    'Clinic physical location address
    Private _sPLContactName As String = ""
    Private _sPLAddressline1 As String = ""
    Private _sPLAddressline2 As String = ""
    Private _sPLCity As String = ""
    Private _sPLState As String = ""
    Private _sPLZIP As String = ""
    Private _sPLAreaCode As String = ""
    Private _sPLCountry As String = ""
    Private _sPLCounty As String = ""
    Private _sPLPhoneNo As String = ""
    Private _sPLFAX As String = ""
    Private _sPLPagerNo As String = ""
    Private _sPLEmail As String = ""
    Private _sPLURL As String = ""

#End Region

#Region " Public Properties"
    Public Property ClinicID() As Integer
        Get
            Return _nClinicID
        End Get
        Set(ByVal Value As Integer)
            _nClinicID = Value
        End Set
    End Property
    Public Property ClinicName() As String
        Get
            Return _sClinicName
        End Get
        Set(ByVal Value As String)
            _sClinicName = Value
        End Set
    End Property

   

    Public Property ContactName() As String
        Get
            Return _sContactName
        End Get
        Set(ByVal Value As String)
            _sContactName = Value
        End Set
    End Property
    Public Property ClinicAddress1() As String
        Get
            Return _sAddress1
        End Get
        Set(ByVal Value As String)
            _sAddress1 = Value
        End Set
    End Property
    Public Property ClinicAddress2() As String
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
    Public Property AreaCode() As String
        Get
            Return _sAreaCode
        End Get
        Set(ByVal Value As String)
            _sAreaCode = Value
        End Set
    End Property
    Public Property Country() As String     ''Country
        Get
            Return _sCountry
        End Get
        Set(ByVal Value As String)
            _sCountry = Value
        End Set
    End Property

    Public Property County() As String      ''County
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
    Public Property URL() As String
        Get
            Return _sURL
        End Get
        Set(ByVal Value As String)
            _sURL = Value
        End Set
    End Property
    Public Property TAXID() As String
        Get
            Return _sTAXID
        End Get
        Set(ByVal Value As String)
            _sTAXID = Value
        End Set
    End Property
    Public Property TaxonomyCode() As String
        Get
            Return _sTaxonomyCode
        End Get
        Set(ByVal Value As String)
            _sTaxonomyCode = Value
        End Set
    End Property

    Public Property ClinicLogo() As Image
        Get
            Return _imgClinicLogo
        End Get
        Set(ByVal Value As Image)
            _imgClinicLogo = Value
        End Set
    End Property
    Public Property ContactPersonName() As String
        Get
            Return _sContactPersonName
        End Get
        Set(ByVal Value As String)
            _sContactPersonName = Value
        End Set
    End Property
    Public Property ContactPersonAddress1() As String
        Get
            Return _sContactPersonAddress1
        End Get
        Set(ByVal Value As String)
            _sContactPersonAddress1 = Value
        End Set
    End Property
    Public Property ContactPersonAddress2() As String
        Get
            Return _sContactPersonAddress2
        End Get
        Set(ByVal Value As String)
            _sContactPersonAddress2 = Value
        End Set
    End Property
    Public Property ContactPersonPhone() As String
        Get
            Return _sContactPersonPhone
        End Get
        Set(ByVal Value As String)
            _sContactPersonPhone = Value
        End Set
    End Property
    Public Property ContactPersonFAX() As String
        Get
            Return _sContactPersonFAX
        End Get
        Set(ByVal Value As String)
            _sContactPersonFAX = Value
        End Set
    End Property
    Public Property ContactPersonEmail() As String
        Get
            Return _sContactPersonEmail
        End Get
        Set(ByVal Value As String)
            _sContactPersonEmail = Value
        End Set
    End Property
    Public Property ContactPersonMobile() As String
        Get
            Return _sContactPersonMobile
        End Get
        Set(ByVal Value As String)
            _sContactPersonMobile = Value
        End Set
    End Property

    'sarika SiteID 20090708
    Public Property SiteID() As String
        Get
            Return _sSiteID
        End Get
        Set(ByVal Value As String)
            _sSiteID = Value
        End Set
    End Property
    ''Sandip Darade 20091113
    Public Property ClinicNPI() As String
        Get
            Return _sClinicNPI
        End Get
        Set(ByVal Value As String)
            _sClinicNPI = Value
        End Set
    End Property

    Public Property bISSameImage() As Boolean
        Get
            Return _bIsSameImage
        End Get
        Set(ByVal Value As Boolean)
            _bIsSameImage = Value
        End Set
    End Property

    Public Property ClinicLabel() As String
        Get
            Return _sClinicLabel
        End Get
        Set(ByVal Value As String)
            _sClinicLabel = Value
        End Set
    End Property
#Region "Provider Company Physical Address "

    Public Property ClinicPLContactName() As String
        Get
            Return _sPLContactName
        End Get
        Set(ByVal value As String)
            _sPLContactName = value
        End Set
    End Property

    Public Property ClinicPLAddressline1() As String
        Get
            Return _sPLAddressline1
        End Get
        Set(ByVal value As String)
            _sPLAddressline1 = value
        End Set
    End Property
    Public Property ClinicPLAddressline2() As String
        Get
            Return _sPLAddressline2
        End Get
        Set(ByVal value As String)
            _sPLAddressline2 = value
        End Set
    End Property
    Public Property ClinicPLCity() As String
        Get
            Return _sPLCity
        End Get
        Set(ByVal value As String)
            _sPLCity = value
        End Set
    End Property
    Public Property ClinicPLState() As String
        Get
            Return _sPLState
        End Get
        Set(ByVal value As String)
            _sPLState = value
        End Set
    End Property
    Public Property ClinicPLZIP() As String
        Get
            Return _sPLZIP
        End Get
        Set(ByVal value As String)
            _sPLZIP = value
        End Set
    End Property
    Public Property ClinicPLAreaCode() As String
        Get
            Return _sPLAreaCode
        End Get
        Set(ByVal value As String)
            _sPLAreaCode = value
        End Set
    End Property
    Public Property ClinicPLCountry() As String
        Get
            Return _sPLCountry
        End Get
        Set(ByVal value As String)
            _sPLCountry = value
        End Set
    End Property

    Public Property ClinicPLCounty() As String
        Get
            Return _sPLCounty
        End Get
        Set(ByVal value As String)
            _sPLCounty = value
        End Set
    End Property

    Public Property ClinicPLPhoneNo() As String
        Get
            Return _sPLPhoneNo
        End Get
        Set(ByVal value As String)
            _sPLPhoneNo = value
        End Set
    End Property
    Public Property ClinicPLFAX() As String
        Get
            Return _sPLFAX
        End Get
        Set(ByVal value As String)
            _sPLFAX = value
        End Set
    End Property
    Public Property ClinicPLPagerNo() As String
        Get
            Return _sPLPagerNo
        End Get
        Set(ByVal value As String)
            _sPLPagerNo = value
        End Set
    End Property
    Public Property ClinicPLEmail() As String
        Get
            Return _sPLEmail
        End Get
        Set(ByVal value As String)
            _sPLEmail = value
        End Set
    End Property
    Public Property ClinicPLURL() As String
        Get
            Return _sPLURL
        End Get
        Set(ByVal value As String)
            _sPLURL = value
        End Set
    End Property

#End Region

#End Region

#Region " Public Functions"
    Public Sub sp_ScanClinic()
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanClinic"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()
            _nClinicID = objSQLDataReader.Item("ClinicID")

            If IsDBNull(objSQLDataReader.Item("ClinicName")) = False Then
                _sClinicName = objSQLDataReader.Item("ClinicName")
            Else
                _sClinicName = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ClinicAddress")) = False Then
                _sAddress1 = objSQLDataReader.Item("ClinicAddress")
            Else
                _sAddress1 = ""
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
            If IsDBNull(objSQLDataReader.Item("URL")) = False Then
                _sURL = objSQLDataReader.Item("URL")
            Else
                _sURL = ""
            End If
            If IsDBNull(objSQLDataReader.Item("TAXID")) = False Then
                _sTAXID = objSQLDataReader.Item("TAXID")
            Else
                _sTAXID = ""
            End If

            If IsDBNull(objSQLDataReader.Item("ClinicLogo")) = False Then
                Dim arrPicture() As Byte = CType(objSQLDataReader.Item("ClinicLogo"), Byte())
                Dim ms As New MemoryStream(arrPicture)
                _imgClinicLogo = Image.FromStream(ms)
                ms.Close()
            Else

            End If


            If IsDBNull(objSQLDataReader.Item("ContactPersonName")) = False Then
                _sContactPersonName = objSQLDataReader.Item("ContactPersonName")
            Else
                _sContactPersonName = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonAddress")) = False Then
                _sContactPersonAddress1 = objSQLDataReader.Item("ContactPersonAddress")
            Else
                _sContactPersonAddress1 = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonPhone")) = False Then
                _sContactPersonPhone = objSQLDataReader.Item("ContactPersonPhone")
            Else
                _sContactPersonPhone = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonFAX")) = False Then
                _sContactPersonFAX = objSQLDataReader.Item("ContactPersonFAX")
            Else
                _sContactPersonFAX = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonEmail")) = False Then
                _sContactPersonEmail = objSQLDataReader.Item("ContactPersonEmail")
            Else
                _sContactPersonEmail = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonMobile")) = False Then
                _sContactPersonMobile = objSQLDataReader.Item("ContactPersonMobile")
            Else
                _sContactPersonMobile = ""
            End If
            If IsDBNull(objSQLDataReader.Item("sSiteID")) = False Then
                _sSiteID = objSQLDataReader.Item("sSiteID")
            Else
                _sSiteID = ""
            End If
        End If
        objCon.Close()
        objCon = Nothing
    End Sub
    'Public Function UpdateClinicDetails(ByVal nClinicID As Integer) As Boolean
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_InUpClinic"
    '    objCmd.Connection = objCon

    '    Dim objParaClinicID As New SqlParameter
    '    With objParaClinicID
    '        .ParameterName = "@ClinicID"
    '        .Value = nClinicID
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaClinicID)


    '    Dim objParaClinicName As New SqlParameter
    '    With objParaClinicName
    '        .ParameterName = "@ClinicName"
    '        .Value = _sClinicName
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicName)

    '    Dim objParaClinicAddress1 As New SqlParameter
    '    With objParaClinicAddress1
    '        .ParameterName = "@Address1"
    '        .Value = _sAddress1
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicAddress1)

    '    Dim objParaClinicAddress2 As New SqlParameter
    '    With objParaClinicAddress2
    '        .ParameterName = "@Address2"
    '        .Value = _sAddress2
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicAddress2)

    '    Dim objParaClinicStreet As New SqlParameter
    '    With objParaClinicStreet
    '        .ParameterName = "@Street"
    '        .Value = _sStreet
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicStreet)

    '    Dim objParaClinicCity As New SqlParameter
    '    With objParaClinicCity
    '        .ParameterName = "@City"
    '        .Value = _sCity
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicCity)

    '    Dim objParaClinicState As New SqlParameter
    '    With objParaClinicState
    '        .ParameterName = "@State"
    '        .Value = _sState
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicState)

    '    Dim objParaClinicZIP As New SqlParameter
    '    With objParaClinicZIP
    '        .ParameterName = "@ZIP"
    '        .Value = _sZIP
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicZIP)



    '    Dim objParaClinicCountry As New SqlParameter        '' Country
    '    With objParaClinicCountry
    '        .ParameterName = "@Country"
    '        .Value = _sCountry
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicCountry)


    '    Dim objParaClinicCounty As New SqlParameter        '' County
    '    With objParaClinicCounty
    '        .ParameterName = "@County"
    '        .Value = _sCounty
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicCounty)


    '    Dim objParaClinicPhone As New SqlParameter
    '    With objParaClinicPhone
    '        .ParameterName = "@PhoneNo"
    '        .Value = _sPhoneNo
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicPhone)

    '    Dim objParaClinicMobile As New SqlParameter
    '    With objParaClinicMobile
    '        .ParameterName = "@MobileNo"
    '        .Value = _sMobileNo
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicMobile)

    '    Dim objParaClinicFAX As New SqlParameter
    '    With objParaClinicFAX
    '        .ParameterName = "@FAX"
    '        .Value = _sFAX
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicFAX)

    '    Dim objParaClinicEmail As New SqlParameter
    '    With objParaClinicEmail
    '        .ParameterName = "@Email"
    '        .Value = _sEmail
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicEmail)

    '    Dim objParaClinicURL As New SqlParameter
    '    With objParaClinicURL
    '        .ParameterName = "@URL"
    '        .Value = _sURL
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicURL)

    '    Dim objParaClinicTAXID As New SqlParameter
    '    With objParaClinicTAXID
    '        .ParameterName = "@TAXID"
    '        .Value = _sTAXID
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicTAXID)


    '    Dim objParaClinicLogo As New SqlParameter
    '    With objParaClinicLogo
    '        .ParameterName = "@ClinicLogo"
    '        If IsNothing(_imgClinicLogo) = False Then
    '            Dim ms As New MemoryStream
    '            _imgClinicLogo.Save(ms, _imgClinicLogo.RawFormat)
    '            Dim arrImage() As Byte = ms.GetBuffer
    '            ms.Close()
    '            .Value = arrImage
    '        Else
    '            .Value = Nothing
    '        End If
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Image
    '    End With
    '    objCmd.Parameters.Add(objParaClinicLogo)

    '    Dim objParaClinicContactPersonName As New SqlParameter
    '    With objParaClinicContactPersonName
    '        .ParameterName = "@ContactPersonName"
    '        .Value = _sContactPersonName
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicContactPersonName)

    '    Dim objParaClinicContactPersonAddress1 As New SqlParameter
    '    With objParaClinicContactPersonAddress1
    '        .ParameterName = "@ContactPersonAddress1"
    '        .Value = _sContactPersonAddress1
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicContactPersonAddress1)

    '    Dim objParaClinicContactPersonAddress2 As New SqlParameter
    '    With objParaClinicContactPersonAddress2
    '        .ParameterName = "@ContactPersonAddress2"
    '        .Value = _sContactPersonAddress2
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicContactPersonAddress2)

    '    Dim objParaClinicContactPersonPhone As New SqlParameter
    '    With objParaClinicContactPersonPhone
    '        .ParameterName = "@ContactPersonPhone"
    '        .Value = _sContactPersonPhone
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicContactPersonPhone)

    '    Dim objParaClinicContactPersonMobile As New SqlParameter
    '    With objParaClinicContactPersonMobile
    '        .ParameterName = "@ContactPersonMobile"
    '        .Value = _sContactPersonMobile
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicContactPersonMobile)

    '    Dim objParaClinicContactPersonEmail As New SqlParameter
    '    With objParaClinicContactPersonEmail
    '        .ParameterName = "@ContactPersonEmail"
    '        .Value = _sContactPersonEmail
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicContactPersonEmail)

    '    Dim objParaClinicContactPersonFAX As New SqlParameter
    '    With objParaClinicContactPersonFAX
    '        .ParameterName = "@ContactPersonFAX"
    '        .Value = _sContactPersonFAX
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicContactPersonFAX)


    '    'sarika siteid 20090708
    '    Dim objParaClinicSiteID As New SqlParameter
    '    With objParaClinicSiteID
    '        .ParameterName = "@SiteID"
    '        .Value = _sSiteID
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicSiteID)
    '    '--
    '    'Sandip Darade ClinicNPI 20091113
    '    Dim objParaClinicNPI As New SqlParameter
    '    With objParaClinicNPI
    '        .ParameterName = "@ClinicNPI"
    '        .Value = _sClinicNPI
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClinicNPI)

    '    Dim objParaAreaCode As New SqlParameter
    '    With objParaAreaCode
    '        .ParameterName = "@AreaCode"
    '        .Value = DBNull.Value
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaAreaCode)

    '    objCon.Open()
    '    objCmd.ExecuteNonQuery()
    '    objCon.Close()
    '    Return True
    'End Function

    Public Function UpdateClinicDetails(ByVal nClinicID As Integer, ByVal dt As DataTable, ByVal userID As Int64) As Boolean

        Dim objCon As New SqlConnection
        Dim SqlTrans As SqlTransaction = Nothing
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Try
            objCon.Open()
            SqlTrans = objCon.BeginTransaction(IsolationLevel.ReadCommitted)
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpClinic"
            objCmd.Connection = objCon
            objCmd.Transaction = SqlTrans
            Dim objParaClinicID As New SqlParameter
            With objParaClinicID
                .ParameterName = "@ClinicID"
                .Value = nClinicID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaClinicID)


            Dim objParaClinicName As New SqlParameter
            With objParaClinicName
                .ParameterName = "@ClinicName"
                .Value = _sClinicName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicName)
            Dim objParaContactName As New SqlParameter
            With objParaContactName
                .ParameterName = "@ContactName"
                .Value = _sContactName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaContactName)

            Dim objParaClinicAddress1 As New SqlParameter
            With objParaClinicAddress1
                .ParameterName = "@Address1"
                .Value = _sAddress1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicAddress1)

            Dim objParaClinicAddress2 As New SqlParameter
            With objParaClinicAddress2
                .ParameterName = "@Address2"
                .Value = _sAddress2
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicAddress2)

            Dim objParaClinicStreet As New SqlParameter
            With objParaClinicStreet
                .ParameterName = "@Street"
                .Value = _sStreet
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicStreet)

            Dim objParaClinicCity As New SqlParameter
            With objParaClinicCity
                .ParameterName = "@City"
                .Value = _sCity
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicCity)

            Dim objParaClinicState As New SqlParameter
            With objParaClinicState
                .ParameterName = "@State"
                .Value = _sState
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicState)

            Dim objParaClinicZIP As New SqlParameter
            With objParaClinicZIP
                .ParameterName = "@ZIP"
                .Value = _sZIP
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicZIP)


            Dim objParaClinicCountry As New SqlParameter        '' Country
            With objParaClinicCountry
                .ParameterName = "@Country"
                .Value = _sCountry
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicCountry)


            Dim objParaClinicCounty As New SqlParameter        '' County
            With objParaClinicCounty
                .ParameterName = "@County"
                .Value = _sCounty
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicCounty)


            Dim objParaClinicAreaCode As New SqlParameter
            With objParaClinicAreaCode
                .ParameterName = "@AreaCode"
                .Value = _sAreaCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicAreaCode)


            Dim objParaClinicPhone As New SqlParameter
            With objParaClinicPhone
                .ParameterName = "@PhoneNo"
                .Value = _sPhoneNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicPhone)

            Dim objParaClinicMobile As New SqlParameter
            With objParaClinicMobile
                .ParameterName = "@MobileNo"
                .Value = _sMobileNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicMobile)

            Dim objParaClinicFAX As New SqlParameter
            With objParaClinicFAX
                .ParameterName = "@FAX"
                .Value = _sFAX
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicFAX)

            Dim objParaClinicEmail As New SqlParameter
            With objParaClinicEmail
                .ParameterName = "@Email"
                .Value = _sEmail
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicEmail)

            Dim objParaClinicURL As New SqlParameter
            With objParaClinicURL
                .ParameterName = "@URL"
                .Value = _sURL
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicURL)

            Dim objParaClinicTAXID As New SqlParameter
            With objParaClinicTAXID
                .ParameterName = "@TAXID"
                .Value = _sTAXID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicTAXID)

            Dim objParasTaxonomyCode As New SqlParameter
            With objParasTaxonomyCode
                .ParameterName = "@sTaxonomyCode"
                .Value = _sTaxonomyCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParasTaxonomyCode)

        If IsNothing(_imgClinicLogo) = False Then
            Dim ms As New MemoryStream
                _imgClinicLogo.Save(ms, _imgClinicLogo.RawFormat)
            Dim arrImage() As Byte = ms.GetBuffer

                Dim objParaClinicLogo As New SqlParameter
                With objParaClinicLogo
                    .ParameterName = "@ClinicLogo"
                    .Value = arrImage
                    .Direction = System.Data.ParameterDirection.Input
                    .SqlDbType = System.Data.SqlDbType.Image
                End With
                objCmd.Parameters.Add(objParaClinicLogo)
                objParaClinicLogo = Nothing
            End If

            Dim objParaClinicContactPersonName As New SqlParameter
            With objParaClinicContactPersonName
                .ParameterName = "@ContactPersonName"
                .Value = _sContactPersonName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonName)

            Dim objParaClinicContactPersonAddress1 As New SqlParameter
            With objParaClinicContactPersonAddress1
                .ParameterName = "@ContactPersonAddress1"
                .Value = _sContactPersonAddress1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonAddress1)

            Dim objParaClinicContactPersonAddress2 As New SqlParameter
            With objParaClinicContactPersonAddress2
                .ParameterName = "@ContactPersonAddress2"
                .Value = _sContactPersonAddress2
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonAddress2)

            Dim objParaClinicContactPersonPhone As New SqlParameter
            With objParaClinicContactPersonPhone
                .ParameterName = "@ContactPersonPhone"
                .Value = _sContactPersonPhone
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonPhone)

            Dim objParaClinicContactPersonMobile As New SqlParameter
            With objParaClinicContactPersonMobile
                .ParameterName = "@ContactPersonMobile"
                .Value = _sContactPersonMobile
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonMobile)

            Dim objParaClinicContactPersonEmail As New SqlParameter
            With objParaClinicContactPersonEmail
                .ParameterName = "@ContactPersonEmail"
                .Value = _sContactPersonEmail
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonEmail)

            Dim objParaClinicContactPersonFAX As New SqlParameter
            With objParaClinicContactPersonFAX
                .ParameterName = "@ContactPersonFAX"
                .Value = _sContactPersonFAX
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonFAX)


            'sarika siteid 20090708
            Dim objParaClinicSiteID As New SqlParameter
            With objParaClinicSiteID
                .ParameterName = "@SiteID"
                .Value = _sSiteID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicSiteID)
            '--
            'Sandip Darade ClinicNPI 20091113
            Dim objParaClinicNPI As New SqlParameter
            With objParaClinicNPI
                .ParameterName = "@ClinicNPI"
                .Value = _sClinicNPI
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicNPI)

            'ISSameImage abhisekh
            Dim objParabIsSameImage As New SqlParameter
            With objParabIsSameImage
                .ParameterName = "@bIsSameImage"
                .Value = _bIsSameImage
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParabIsSameImage)

            'Epcschange Kishor
            Dim objParaContactLabel As New SqlParameter
            With objParaContactLabel
                .ParameterName = "@ClinicLabel"
                .Value = _sClinicLabel
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaContactLabel)

            objCmd.ExecuteNonQuery()

            objCmd.Parameters.Clear()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "delete from Clinic_ID_Qualifiers where nClinicID=" + Convert.ToString(nClinicID)
            objCmd.CommandTimeout = 0
            objCmd.ExecuteNonQuery()

            If dt.Rows.Count > 0 Then

                Dim i As Int32
                For i = 0 To dt.Rows.Count - 1
                    If ((dt.Rows(i)("sValue") <> Nothing And dt.Rows(i)("sValue").trim <> String.Empty) Or (dt.Rows(i)("nQualifierID") = COL_QUALIFIERIDVALUE_NPI) Or (dt.Rows(i)("nQualifierID") = COL_QUALIFIERIDVALUE_TAXID)) Then
                        objCmd.Parameters.Clear()
                        objCmd.CommandType = CommandType.StoredProcedure
                        objCmd.CommandText = "gsp_InUpClinicQualifierIDs"
                        objCmd.CommandTimeout = 0
                        Dim objClinicID As New SqlParameter
                        With objClinicID
                            .ParameterName = "@nClinicID"
                            .Value = nClinicID
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.BigInt
                        End With
                        objCmd.Parameters.Add(objClinicID)
                        Dim objQualifierID As New SqlParameter
                        With objQualifierID
                            .ParameterName = "@nQualifierID"
                            .Value = dt.Rows(i)("nQualifierID")
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.BigInt
                        End With
                        objCmd.Parameters.Add(objQualifierID)
                        Dim objParaValue As New SqlParameter
                        With objParaValue
                            .ParameterName = "@sValue"
                            .Value = dt.Rows(i)("sValue")
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.NVarChar
                        End With
                        objCmd.Parameters.Add(objParaValue)
                        Dim objParaIsSystem As New SqlParameter
                        With objParaIsSystem
                            .ParameterName = "@bIsSystem"
                            .Value = dt.Rows(i)("bIsSystem")
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.Bit
                        End With
                        objCmd.Parameters.Add(objParaIsSystem)
                        Dim objParaUserID As New SqlParameter
                        With objParaUserID
                            .ParameterName = "@nUserID"
                            .Value = userID
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.BigInt
                        End With
                        objCmd.Parameters.Add(objParaUserID)
                        objCmd.ExecuteNonQuery()
                    End If
                Next
            End If

            'NPI

            objCmd.Parameters.Clear()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpClinicQualifierIDs"
            objCmd.CommandTimeout = 0
            Dim objClinicIDNPI As New SqlParameter
            With objClinicIDNPI
                .ParameterName = "@nClinicID"
                .Value = nClinicID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objClinicIDNPI)
            Dim objQualifierIDNPI As New SqlParameter
            With objQualifierIDNPI
                .ParameterName = "@nQualifierID"
                .Value = COL_QUALIFIERIDVALUE_NPI
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objQualifierIDNPI)
            Dim objParaValueNPI As New SqlParameter
            With objParaValueNPI
                .ParameterName = "@sValue"
                .Value = _sClinicNPI
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.NVarChar
            End With
            objCmd.Parameters.Add(objParaValueNPI)
            Dim objParaIsSystemNPI As New SqlParameter
            With objParaIsSystemNPI
                .ParameterName = "@bIsSystem"
                .Value = COL_ISSYSTEM_VALUE
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaIsSystemNPI)
            Dim objParaUserIDNPI As New SqlParameter
            With objParaUserIDNPI
                .ParameterName = "@nUserID"
                .Value = userID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaUserIDNPI)
            objCmd.ExecuteNonQuery()

            'TAX ID

            objCmd.Parameters.Clear()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpClinicQualifierIDs"
            objCmd.CommandTimeout = 0
            Dim objClinicIDTAXID As New SqlParameter
            With objClinicIDTAXID
                .ParameterName = "@nClinicID"
                .Value = nClinicID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objClinicIDTAXID)
            Dim objQualifierIDTAXID As New SqlParameter
            With objQualifierIDTAXID
                .ParameterName = "@nQualifierID"
                .Value = COL_QUALIFIERIDVALUE_TAXID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objQualifierIDTAXID)
            Dim objParaValueTAXID As New SqlParameter
            With objParaValueTAXID
                .ParameterName = "@sValue"
                .Value = _sTAXID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.NVarChar
            End With
            objCmd.Parameters.Add(objParaValueTAXID)
            Dim objParaIsSystemTAXID As New SqlParameter
            With objParaIsSystemTAXID
                .ParameterName = "@bIsSystem"
                .Value = COL_ISSYSTEM_VALUE
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaIsSystemTAXID)
            Dim objParaUserIDTAXID As New SqlParameter
            With objParaUserIDTAXID
                .ParameterName = "@nUserID"
                .Value = userID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaUserIDTAXID)
            objCmd.ExecuteNonQuery()




            'Clinic Physical Location Address
            objCmd.Parameters.Clear()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "InupClinicPLAddress"
            objCmd.CommandTimeout = 0

            Dim objPLClinicID As New SqlParameter
            With objPLClinicID
                .ParameterName = "@nClinicID"
                .Value = nClinicID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objPLClinicID)

            Dim objContactName As New SqlParameter
            With objContactName
                .ParameterName = "@sContactName"
                .Value = ClinicPLContactName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objContactName)

            Dim objClinicPLAddressline1 As New SqlParameter
            With objClinicPLAddressline1
                .ParameterName = "@sPLAddressline1"
                .Value = ClinicPLAddressline1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLAddressline1)

            Dim objClinicPLAddressline2 As New SqlParameter
            With objClinicPLAddressline2
                .ParameterName = "@sPLAddressline2"
                .Value = ClinicPLAddressline2
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLAddressline2)

            Dim objClinicPLCity As New SqlParameter
            With objClinicPLCity
                .ParameterName = "@sPLCity"
                .Value = ClinicPLCity
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLCity)

            Dim objClinicPLState As New SqlParameter
            With objClinicPLState
                .ParameterName = "@sPLState"
                .Value = ClinicPLState
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLState)

            Dim objClinicPLZIP As New SqlParameter
            With objClinicPLZIP
                .ParameterName = "@sPLZIP"
                .Value = ClinicPLZIP
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLZIP)

            Dim objClinicPLAreaCode As New SqlParameter
            With objClinicPLAreaCode
                .ParameterName = "@sPLAreaCode"
                .Value = ClinicPLAreaCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLAreaCode)

            Dim objClinicPLCountry As New SqlParameter
            With objClinicPLCountry
                .ParameterName = "@sPLCountry"
                .Value = ClinicPLCountry
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLCountry)

            Dim objClinicPLCounty As New SqlParameter
            With objClinicPLCounty
                .ParameterName = "@sPLCounty"
                .Value = ClinicPLCounty
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLCounty)

            Dim objClinicPLPhoneNo As New SqlParameter
            With objClinicPLPhoneNo
                .ParameterName = "@sPLPhoneNo"
                .Value = ClinicPLPhoneNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLPhoneNo)

            Dim objClinicPLFAX As New SqlParameter
            With objClinicPLFAX
                .ParameterName = "@sPLFAX"
                .Value = ClinicPLFAX
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLFAX)

            Dim objClinicPLPagerNo As New SqlParameter
            With objClinicPLPagerNo
                .ParameterName = "@sPLPagerNo"
                .Value = ClinicPLPagerNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLPagerNo)

            Dim objClinicPLEmail As New SqlParameter
            With objClinicPLEmail
                .ParameterName = "@sPLEmail"
                .Value = ClinicPLEmail
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLEmail)

            Dim objClinicPLURL As New SqlParameter
            With objClinicPLURL
                .ParameterName = "@sPLURL"
                .Value = ClinicPLURL
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objClinicPLURL)

            objCmd.ExecuteNonQuery()

            'Clinic Physical Location Address

            SqlTrans.Commit()
            objCmd.Dispose()
            objCon.Close()
            Return True
        Catch ex As Exception
            SqlTrans.Rollback()
            objCmd.Dispose()
            objCon.Close()
            Return False
        End Try
    End Function
    Public Function RetrieveClinicBasicInformation(ByVal strClinicName As String) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanClinicBasicInfo"
        objCmd.Connection = objCon
        Dim objParaClinicName As New SqlParameter
        With objParaClinicName
            .ParameterName = "@ClinicName"
            .Value = strClinicName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaClinicName)
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function PopulateClinic() As Collection
        Dim clClinics As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillClinic"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            While objSQLDataReader.Read
                clClinics.Add(objSQLDataReader.Item(0))
            End While
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return clClinics
    End Function
    Public Sub SearchClinic(ByVal nClinicID As Integer)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanClinic"

        Dim objParaClinicID As New SqlParameter
        With objParaClinicID
            .ParameterName = "@ClinicID"
            .Value = nClinicID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaClinicID)
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()
            If IsDBNull(objSQLDataReader.Item("ClinicName")) = False Then
                _sClinicName = objSQLDataReader.Item("ClinicName")
            Else
                _sClinicName = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactName")) = False Then
                _sContactName = objSQLDataReader.Item("ContactName")
            Else
                _sContactName = ""
            End If
            '' SUDHIR 20090414 - CLINIC MST CHANGE FOR ADDRESS ''
            If IsDBNull(objSQLDataReader.Item("ClinicAddress1")) = False Then
                _sAddress1 = objSQLDataReader.Item("ClinicAddress1")
            Else
                _sAddress1 = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ClinicAddress2")) = False Then
                _sAddress2 = objSQLDataReader.Item("ClinicAddress2")
            Else
                _sAddress2 = ""
            End If
            '' ''
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
            If IsDBNull(objSQLDataReader.Item("Country")) = False Then
                _sCountry = objSQLDataReader.Item("Country")
            Else
                _sCountry = ""
            End If
            If IsDBNull(objSQLDataReader.Item("County")) = False Then
                _sCounty = objSQLDataReader.Item("County")
            Else
                _sCounty = ""
            End If
            If IsDBNull(objSQLDataReader.Item("AreaCode")) = False Then           ''County
                _sAreaCode = objSQLDataReader.Item("AreaCode")
            Else
                _sAreaCode = ""
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
            If IsDBNull(objSQLDataReader.Item("URL")) = False Then
                _sURL = objSQLDataReader.Item("URL")
            Else
                _sURL = ""
            End If
            If IsDBNull(objSQLDataReader.Item("TAXID")) = False Then
                _sTAXID = objSQLDataReader.Item("TAXID")
            Else
                _sTAXID = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sTaxonomyCode")) = False Then
                _sTaxonomyCode = objSQLDataReader.Item("sTaxonomyCode")
            Else
                _sTaxonomyCode = ""
            End If

            If IsDBNull(objSQLDataReader.Item("ClinicLogo")) = False Then
                Dim arrPicture() As Byte = CType(objSQLDataReader.Item("ClinicLogo"), Byte())
                Dim ms As New MemoryStream(arrPicture)
                If ms.Length > 0 Then
                    _imgClinicLogo = Image.FromStream(ms)
                End If
                ms.Close()
            Else

            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonName")) = False Then
                _sContactPersonName = objSQLDataReader.Item("ContactPersonName")
            Else
                _sContactPersonName = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonAddress1")) = False Then
                _sContactPersonAddress1 = objSQLDataReader.Item("ContactPersonAddress1")
            Else
                _sContactPersonAddress1 = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonAddress2")) = False Then
                _sContactPersonAddress2 = objSQLDataReader.Item("ContactPersonAddress2")
            Else
                _sContactPersonAddress2 = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonPhone")) = False Then
                _sContactPersonPhone = objSQLDataReader.Item("ContactPersonPhone")
            Else
                _sContactPersonPhone = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonFAX")) = False Then
                _sContactPersonFAX = objSQLDataReader.Item("ContactPersonFAX")
            Else
                _sContactPersonFAX = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonEmail")) = False Then
                _sContactPersonEmail = objSQLDataReader.Item("ContactPersonEmail")
            Else
                _sContactPersonEmail = ""
            End If
            If IsDBNull(objSQLDataReader.Item("ContactPersonMobile")) = False Then
                _sContactPersonMobile = objSQLDataReader.Item("ContactPersonMobile")
            Else
                _sContactPersonMobile = ""
            End If

            If IsDBNull(objSQLDataReader.Item("SiteId")) = False Then
                _sSiteID = objSQLDataReader.Item("SiteId")
            Else
                _sSiteID = ""
            End If
            ''Sandip Darade 200091113
            If IsDBNull(objSQLDataReader.Item("sClinicNPI")) = False Then
                _sClinicNPI = objSQLDataReader.Item("sClinicNPI")
            Else
                _sClinicNPI = ""
            End If


            'Patient physical address
            If IsDBNull(objSQLDataReader.Item("sPLContactName")) = False Then
                _sPLContactName = objSQLDataReader.Item("sPLContactName")
            Else
                _sPLContactName = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLAddressline1")) = False Then
                _sPLAddressline1 = objSQLDataReader.Item("sPLAddressline1")
            Else
                _sPLAddressline1 = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLAddressline2")) = False Then
                _sPLAddressline2 = objSQLDataReader.Item("sPLAddressline2")
            Else
                _sPLAddressline2 = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLCity")) = False Then
                _sPLCity = objSQLDataReader.Item("sPLCity")
            Else
                _sPLCity = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLState")) = False Then
                _sPLState = objSQLDataReader.Item("sPLState")
            Else
                _sPLState = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLZIP")) = False Then
                _sPLZIP = objSQLDataReader.Item("sPLZIP")
            Else
                _sPLZIP = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLAreaCode")) = False Then
                _sPLAreaCode = objSQLDataReader.Item("sPLAreaCode")
            Else
                _sPLAreaCode = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLCountry")) = False Then
                _sPLCountry = objSQLDataReader.Item("sPLCountry")
            Else
                _sPLCountry = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLCounty")) = False Then
                _sPLCounty = objSQLDataReader.Item("sPLCounty")
            Else
                _sPLCounty = ""
            End If


            If IsDBNull(objSQLDataReader.Item("sPLPhoneNo")) = False Then
                _sPLPhoneNo = objSQLDataReader.Item("sPLPhoneNo")
            Else
                _sPLPhoneNo = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLFAX")) = False Then
                _sPLFAX = objSQLDataReader.Item("sPLFAX")
            Else
                _sPLFAX = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLPagerNo")) = False Then
                _sPLPagerNo = objSQLDataReader.Item("sPLPagerNo")
            Else
                _sPLPagerNo = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLEmail")) = False Then
                _sPLEmail = objSQLDataReader.Item("sPLEmail")
            Else
                _sPLEmail = ""
            End If

            If IsDBNull(objSQLDataReader.Item("sPLURL")) = False Then
                _sPLURL = objSQLDataReader.Item("sPLURL")
            Else
                _sPLURL = ""
            End If

            'EpcsChange
            If IsDBNull(objSQLDataReader.Item("ClinicLabel")) = False Then
                _sClinicLabel = objSQLDataReader.Item("ClinicLabel")
            Else
                _sClinicLabel = ""
            End If

        End If
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing

    End Sub
    Public Function CheckClinicExists(ByVal strClinicName As String, Optional ByVal nClinicID As Integer = 0) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_CheckClinicExists"
        Dim objParaClinicName As New SqlParameter
        With objParaClinicName
            .ParameterName = "@ClinicName"
            .Value = strClinicName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaClinicName)

        If nClinicID <> 0 Then
            Dim objParaClinicID As New SqlParameter
            With objParaClinicID
                .ParameterName = "@ClinicID"
                .Value = nClinicID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaClinicID)
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


    'sarika 11th sept 07
    'Public Function InsertClinicDetails() As Boolean
    '    Try
    '        _sErrorMessage = ""
    '        Dim objCon As New SqlConnection
    '        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    '        Dim objCmd As New SqlCommand
    '        objCmd.CommandType = CommandType.StoredProcedure
    '        objCmd.CommandText = "gsp_InUpClinic"
    '        objCmd.Connection = objCon


    '        Dim objParaClinicName As New SqlParameter
    '        With objParaClinicName
    '            .ParameterName = "@ClinicName"
    '            .Value = _sClinicName
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicName)

    '        Dim objParaClinicAddress1 As New SqlParameter
    '        With objParaClinicAddress1
    '            .ParameterName = "@Address1"
    '            .Value = _sAddress1
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicAddress1)

    '        Dim objParaClinicAddress2 As New SqlParameter
    '        With objParaClinicAddress2
    '            .ParameterName = "@Address2"
    '            .Value = _sAddress2
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicAddress2)

    '        Dim objParaClinicStreet As New SqlParameter
    '        With objParaClinicStreet
    '            .ParameterName = "@Street"
    '            .Value = _sStreet
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicStreet)

    '        Dim objParaClinicCity As New SqlParameter
    '        With objParaClinicCity
    '            .ParameterName = "@City"
    '            .Value = _sCity
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicCity)

    '        Dim objParaClinicState As New SqlParameter
    '        With objParaClinicState
    '            .ParameterName = "@State"
    '            .Value = _sState
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicState)

    '        Dim objParaClinicZIP As New SqlParameter
    '        With objParaClinicZIP
    '            .ParameterName = "@ZIP"
    '            .Value = _sZIP
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicZIP)

    '        Dim objParaClinicCountry As New SqlParameter        '' Country
    '        With objParaClinicCountry
    '            .ParameterName = "@Country"
    '            .Value = _sCountry
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicCountry)


    '        Dim objParaClinicCounty As New SqlParameter        '' County
    '        With objParaClinicCounty
    '            .ParameterName = "@County"
    '            .Value = _sCounty
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicCounty)



    '        Dim objParaClinicPhone As New SqlParameter
    '        With objParaClinicPhone
    '            .ParameterName = "@PhoneNo"
    '            .Value = _sPhoneNo
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicPhone)

    '        Dim objParaClinicMobile As New SqlParameter
    '        With objParaClinicMobile
    '            .ParameterName = "@MobileNo"
    '            .Value = _sMobileNo
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicMobile)

    '        Dim objParaClinicFAX As New SqlParameter
    '        With objParaClinicFAX
    '            .ParameterName = "@FAX"
    '            .Value = _sFAX
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicFAX)

    '        Dim objParaClinicEmail As New SqlParameter
    '        With objParaClinicEmail
    '            .ParameterName = "@Email"
    '            .Value = _sEmail
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicEmail)

    '        Dim objParaClinicURL As New SqlParameter
    '        With objParaClinicURL
    '            .ParameterName = "@URL"
    '            .Value = _sURL
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicURL)

    '        Dim objParaClinicTAXID As New SqlParameter
    '        With objParaClinicTAXID
    '            .ParameterName = "@TAXID"
    '            .Value = _sTAXID
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicTAXID)

    '        If IsNothing(_imgClinicLogo) = False Then
    '            Dim ms As New MemoryStream
    '            _imgClinicLogo.Save(ms, _imgClinicLogo.RawFormat)
    '            Dim arrImage() As Byte = ms.GetBuffer
    '            ms.Close()
    '            Dim objParaClinicLogo As New SqlParameter
    '            With objParaClinicLogo
    '                .ParameterName = "@ClinicLogo"
    '                .Value = arrImage
    '                .Direction = ParameterDirection.Input
    '                .SqlDbType = SqlDbType.Image
    '            End With
    '            objCmd.Parameters.Add(objParaClinicLogo)
    '        End If


    '        Dim objParaClinicContactPersonName As New SqlParameter
    '        With objParaClinicContactPersonName
    '            .ParameterName = "@ContactPersonName"
    '            .Value = _sContactPersonName
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicContactPersonName)

    '        Dim objParaClinicContactPersonAddress1 As New SqlParameter
    '        With objParaClinicContactPersonAddress1
    '            .ParameterName = "@ContactPersonAddress1"
    '            .Value = _sContactPersonAddress1
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicContactPersonAddress1)

    '        Dim objParaClinicContactPersonAddress2 As New SqlParameter
    '        With objParaClinicContactPersonAddress2
    '            .ParameterName = "@ContactPersonAddress2"
    '            .Value = _sContactPersonAddress2
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicContactPersonAddress2)

    '        Dim objParaClinicContactPersonPhone As New SqlParameter
    '        With objParaClinicContactPersonPhone
    '            .ParameterName = "@ContactPersonPhone"
    '            .Value = _sContactPersonPhone
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicContactPersonPhone)

    '        Dim objParaClinicContactPersonMobile As New SqlParameter
    '        With objParaClinicContactPersonMobile
    '            .ParameterName = "@ContactPersonMobile"
    '            .Value = _sContactPersonMobile
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicContactPersonMobile)

    '        Dim objParaClinicContactPersonEmail As New SqlParameter
    '        With objParaClinicContactPersonEmail
    '            .ParameterName = "@ContactPersonEmail"
    '            .Value = _sContactPersonEmail
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicContactPersonEmail)

    '        Dim objParaClinicContactPersonFAX As New SqlParameter
    '        With objParaClinicContactPersonFAX
    '            .ParameterName = "@ContactPersonFAX"
    '            .Value = _sContactPersonFAX
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicContactPersonFAX)


    '        'sarika siteid 20090708
    '        Dim objParaClinicSiteID As New SqlParameter
    '        With objParaClinicSiteID
    '            .ParameterName = "@SiteID"
    '            .Value = _sSiteID
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaClinicSiteID)
    '        '--

    '        Dim objParaAreaCode As New SqlParameter
    '        With objParaAreaCode
    '            .ParameterName = "@AreaCode"
    '            .Value = "EMRAdmin"
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaAreaCode)

    '        objCon.Open()
    '        objCmd.ExecuteNonQuery()
    '        objCon.Close()
    '        Return True
    '    Catch ex As Exception
    '        _sErrorMessage = ex.Message
    '        Return False
    '    End Try
    'End Function
    Public Function InsertClinicDetails(ByVal dt As DataTable, ByVal userID As Int64) As Boolean
        Dim nclinicID As Int64 = 0
        _sErrorMessage = ""
        Dim objCon As New SqlConnection
        Dim SqlTrans As SqlTransaction = Nothing
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Try

            objCon.Open()
            SqlTrans = objCon.BeginTransaction(IsolationLevel.ReadCommitted)
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpClinic"
            objCmd.Connection = objCon
            objCmd.Transaction = SqlTrans


            Dim objParaClinicName As New SqlParameter
            With objParaClinicName
                .ParameterName = "@ClinicName"
                .Value = _sClinicName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicName)

            Dim objParaClinicAddress1 As New SqlParameter
            With objParaClinicAddress1
                .ParameterName = "@Address1"
                .Value = _sAddress1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicAddress1)

            Dim objParaClinicAddress2 As New SqlParameter
            With objParaClinicAddress2
                .ParameterName = "@Address2"
                .Value = _sAddress2
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicAddress2)

            Dim objParaClinicStreet As New SqlParameter
            With objParaClinicStreet
                .ParameterName = "@Street"
                .Value = _sStreet
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicStreet)

            Dim objParaClinicCity As New SqlParameter
            With objParaClinicCity
                .ParameterName = "@City"
                .Value = _sCity
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicCity)

            Dim objParaClinicState As New SqlParameter
            With objParaClinicState
                .ParameterName = "@State"
                .Value = _sState
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicState)

            Dim objParaClinicZIP As New SqlParameter
            With objParaClinicZIP
                .ParameterName = "@ZIP"
                .Value = _sZIP
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicZIP)


            Dim objParaClinicCountry As New SqlParameter        '' Country
            With objParaClinicCountry
                .ParameterName = "@Country"
                .Value = _sCountry
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicCountry)


            Dim objParaClinicCounty As New SqlParameter        '' County
            With objParaClinicCounty
                .ParameterName = "@County"
                .Value = _sCounty
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicCounty)

            Dim objParaClinicAreaCode As New SqlParameter
            With objParaClinicAreaCode
                .ParameterName = "@AreaCode"
                .Value = _sAreaCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicAreaCode)


            Dim objParaClinicPhone As New SqlParameter
            With objParaClinicPhone
                .ParameterName = "@PhoneNo"
                .Value = _sPhoneNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicPhone)

            Dim objParaClinicMobile As New SqlParameter
            With objParaClinicMobile
                .ParameterName = "@MobileNo"
                .Value = _sMobileNo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicMobile)

            Dim objParaClinicFAX As New SqlParameter
            With objParaClinicFAX
                .ParameterName = "@FAX"
                .Value = _sFAX
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicFAX)

            Dim objParaClinicEmail As New SqlParameter
            With objParaClinicEmail
                .ParameterName = "@Email"
                .Value = _sEmail
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicEmail)

            Dim objParaClinicURL As New SqlParameter
            With objParaClinicURL
                .ParameterName = "@URL"
                .Value = _sURL
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicURL)

            Dim objParaClinicTAXID As New SqlParameter
            With objParaClinicTAXID
                .ParameterName = "@TAXID"
                .Value = _sTAXID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicTAXID)

            If IsNothing(_imgClinicLogo) = False Then
                Dim ms As New MemoryStream
                _imgClinicLogo.Save(ms, _imgClinicLogo.RawFormat)
                Dim arrImage() As Byte = ms.GetBuffer
                ms.Close()
                Dim objParaClinicLogo As New SqlParameter
                With objParaClinicLogo
                    .ParameterName = "@ClinicLogo"
                    .Value = arrImage
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.Image
                End With
                objCmd.Parameters.Add(objParaClinicLogo)
            End If


            Dim objParaClinicContactPersonName As New SqlParameter
            With objParaClinicContactPersonName
                .ParameterName = "@ContactPersonName"
                .Value = _sContactPersonName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonName)

            Dim objParaClinicContactPersonAddress1 As New SqlParameter
            With objParaClinicContactPersonAddress1
                .ParameterName = "@ContactPersonAddress1"
                .Value = _sContactPersonAddress1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonAddress1)

            Dim objParaClinicContactPersonAddress2 As New SqlParameter
            With objParaClinicContactPersonAddress2
                .ParameterName = "@ContactPersonAddress2"
                .Value = _sContactPersonAddress2
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonAddress2)

            Dim objParaClinicContactPersonPhone As New SqlParameter
            With objParaClinicContactPersonPhone
                .ParameterName = "@ContactPersonPhone"
                .Value = _sContactPersonPhone
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonPhone)

            Dim objParaClinicContactPersonMobile As New SqlParameter
            With objParaClinicContactPersonMobile
                .ParameterName = "@ContactPersonMobile"
                .Value = _sContactPersonMobile
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonMobile)

            Dim objParaClinicContactPersonEmail As New SqlParameter
            With objParaClinicContactPersonEmail
                .ParameterName = "@ContactPersonEmail"
                .Value = _sContactPersonEmail
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonEmail)

            Dim objParaClinicContactPersonFAX As New SqlParameter
            With objParaClinicContactPersonFAX
                .ParameterName = "@ContactPersonFAX"
                .Value = _sContactPersonFAX
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicContactPersonFAX)


            'sarika siteid 20090708
            Dim objParaClinicSiteID As New SqlParameter
            With objParaClinicSiteID
                .ParameterName = "@SiteID"
                .Value = _sSiteID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicSiteID)
            '--
            'Sandip Darade ClinicNPI 20091113
            Dim objParaClinicNPI As New SqlParameter
            With objParaClinicNPI
                .ParameterName = "@ClinicNPI"
                .Value = _sClinicNPI
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClinicNPI)

            'ISSameImage abhisekh
            Dim objParabIsSameImage As New SqlParameter
            With objParabIsSameImage
                .ParameterName = "@bIsSameImage"
                .Value = _bIsSameImage
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParabIsSameImage)


            Dim objParasTaxonomyCode As New SqlParameter
            With objParasTaxonomyCode
                .ParameterName = "@sTaxonomyCode"
                .Value = _sTaxonomyCode
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParasTaxonomyCode)

            'EpcsChange Kishor
            Dim objParaContactLabel As New SqlParameter
            With objParaContactLabel
                .ParameterName = "@ClinicLabel"
                .Value = _sClinicLabel
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaContactLabel)

            'objCon.Open()
            objCmd.ExecuteNonQuery()

            If dt.Rows.Count > 0 Then
                Dim i As Int32
                For i = 0 To dt.Rows.Count - 1
                    If ((dt.Rows(i)("sValue") <> Nothing And dt.Rows(i)("sValue").trim <> String.Empty) Or (dt.Rows(i)("nQualifierID") = COL_QUALIFIERIDVALUE_NPI) Or (dt.Rows(i)("nQualifierID") = COL_QUALIFIERIDVALUE_TAXID)) Then
                        objCmd.Parameters.Clear()
                        objCmd.CommandType = CommandType.StoredProcedure
                        objCmd.CommandText = "gsp_InUpClinicQualifierIDs"
                        objCmd.CommandTimeout = 0
                        Dim objClinicID As New SqlParameter
                        With objClinicID
                            .ParameterName = "@nClinicID"
                            .Value = nclinicID
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.BigInt
                        End With
                        objCmd.Parameters.Add(objClinicID)
                        Dim objQualifierID As New SqlParameter
                        With objQualifierID
                            .ParameterName = "@nQualifierID"
                            .Value = dt.Rows(i)("nQualifierID")
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.BigInt
                        End With
                        objCmd.Parameters.Add(objQualifierID)
                        Dim objParaValue As New SqlParameter
                        With objParaValue
                            .ParameterName = "@sValue"
                            .Value = dt.Rows(i)("sValue")
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.NVarChar
                        End With
                        objCmd.Parameters.Add(objParaValue)
                        Dim objParaIsSystem As New SqlParameter
                        With objParaIsSystem
                            .ParameterName = "@bIsSystem"
                            .Value = dt.Rows(i)("bIsSystem")
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.Bit
                        End With
                        objCmd.Parameters.Add(objParaIsSystem)
                        Dim objParaUserID As New SqlParameter
                        With objParaUserID
                            .ParameterName = "@nUserID"
                            .Value = userID
                            .Direction = ParameterDirection.Input
                            .SqlDbType = SqlDbType.BigInt
                        End With
                        objCmd.Parameters.Add(objParaUserID)
                        objCmd.ExecuteNonQuery()
                    End If
                Next
            End If

            'NPI

            objCmd.Parameters.Clear()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpClinicQualifierIDs"
            objCmd.CommandTimeout = 0
            Dim objClinicIDNPI As New SqlParameter
            With objClinicIDNPI
                .ParameterName = "@nClinicID"
                .Value = nclinicID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objClinicIDNPI)
            Dim objQualifierIDNPI As New SqlParameter
            With objQualifierIDNPI
                .ParameterName = "@nQualifierID"
                .Value = COL_QUALIFIERIDVALUE_NPI
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objQualifierIDNPI)
            Dim objParaValueNPI As New SqlParameter
            With objParaValueNPI
                .ParameterName = "@sValue"
                .Value = _sClinicNPI
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.NVarChar
            End With
            objCmd.Parameters.Add(objParaValueNPI)
            Dim objParaIsSystemNPI As New SqlParameter
            With objParaIsSystemNPI
                .ParameterName = "@bIsSystem"
                .Value = COL_ISSYSTEM_VALUE
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaIsSystemNPI)
            Dim objParaUserIDNPI As New SqlParameter
            With objParaUserIDNPI
                .ParameterName = "@nUserID"
                .Value = userID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaUserIDNPI)
            objCmd.ExecuteNonQuery()

            'TAX ID

            objCmd.Parameters.Clear()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InUpClinicQualifierIDs"
            objCmd.CommandTimeout = 0
            Dim objClinicIDTAXID As New SqlParameter
            With objClinicIDTAXID
                .ParameterName = "@nClinicID"
                .Value = nclinicID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objClinicIDTAXID)
            Dim objQualifierIDTAXID As New SqlParameter
            With objQualifierIDTAXID
                .ParameterName = "@nQualifierID"
                .Value = COL_QUALIFIERIDVALUE_TAXID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objQualifierIDTAXID)
            Dim objParaValueTAXID As New SqlParameter
            With objParaValueTAXID
                .ParameterName = "@sValue"
                .Value = _sTAXID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.NVarChar
            End With
            objCmd.Parameters.Add(objParaValueTAXID)
            Dim objParaIsSystemTAXID As New SqlParameter
            With objParaIsSystemTAXID
                .ParameterName = "@bIsSystem"
                .Value = COL_ISSYSTEM_VALUE
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaIsSystemTAXID)
            Dim objParaUserIDTAXID As New SqlParameter
            With objParaUserIDTAXID
                .ParameterName = "@nUserID"
                .Value = userID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaUserIDTAXID)
            objCmd.ExecuteNonQuery()

            SqlTrans.Commit()
            objCmd.Dispose()
            objCon.Close()
            Return True
        Catch ex As Exception
            _sErrorMessage = ex.Message
            SqlTrans.Rollback()
            objCmd.Dispose()
            objCon.Close()
            Return False
        End Try
    End Function

    Public Function DeleteClinic(ByVal nClinicID As Int16) As Boolean

        _sErrorMessage = ""
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim SqlTrans As SqlTransaction = Nothing
        Dim objCmd As New SqlCommand
        Try
            objCon.Open()
            SqlTrans = objCon.BeginTransaction(IsolationLevel.ReadCommitted)
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeleteClinic"
            objCmd.Connection = objCon
            objCmd.Transaction = SqlTrans

            Dim objParaClinicID As New SqlParameter
            With objParaClinicID
                .ParameterName = "@ClinicID"
                .Value = nClinicID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaClinicID)

            objCmd.ExecuteNonQuery()

            objCmd.Parameters.Clear()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "delete from Clinic_ID_Qualifiers where nClinicID=" + Convert.ToString(nClinicID)
            objCmd.CommandTimeout = 0
            objCmd.ExecuteNonQuery()

            SqlTrans.Commit()
            objCmd.Dispose()
            objCon.Close()
            Return True
        Catch ex As Exception
            _sErrorMessage = ex.Message
            SqlTrans.Rollback()
            objCmd.Dispose()
            objCon.Close()
            Return False
        End Try
    End Function
    '---------------


    'sarika AUS User name 20080922
    Public Function CheckColumnExists() As Boolean

        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        conn = New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())

        Try

            conn.Open()

            cmd = New SqlCommand

            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            cmd.CommandText = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Clinic_MST' AND COLUMN_NAME = 'sExternalCode') alter table Clinic_MST add sExternalCode varchar(50)  null"


            cmd.ExecuteNonQuery()



            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DMS Migration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

        End Try
    End Function


    Public Function GetAUSUserName() As String
        Dim strAUSUsername As String = ""

        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        conn = New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())

        Try

            conn.Open()

            cmd = New SqlCommand

            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            cmd.CommandText = "select top 1 isnull(sExternalCode,'') as sExternalCode from Clinic_MST"


            strAUSUsername = cmd.ExecuteScalar()


        Catch ex As Exception
            MessageBox.Show(ex.Message, "DMS Migration", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

        End Try

        Return strAUSUsername
    End Function

    Public Function UpdateAUSUsername(ByVal sAUSUsername As String) As Boolean

        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        conn = New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())

        Try

            conn.Open()

            cmd = New SqlCommand

            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            cmd.CommandText = "Update Clinic_MST set sExternalCode = '" & sAUSUsername.Replace("'", "''") & "'"


            '  strAUSUsername = cmd.ExecuteScalar()
            cmd.ExecuteNonQuery()
            gstrClinicExternalCode = sAUSUsername
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DMS Migration", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

        End Try
    End Function

    '----------------------------------
#End Region
End Class
