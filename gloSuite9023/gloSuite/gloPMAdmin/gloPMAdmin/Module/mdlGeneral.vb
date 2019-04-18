'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Module mdlGeneral

    Public gstrClientMachineName As String
    Public gstrMessageBoxCaption As String = "gloPM Admin"
    Public gstrLoginName As String
    Public gstrLoginPassword As String
    Public Const DTFORMAT As String = "MM/dd/yyyy"
    Public gstrSQLServerName As String
    Public gstrDatabaseName As String
    Public gstrArchiveDatabaseName As String
    Public gblnSQLAuthentication As Boolean
    Public gstrSQLUserEMR As String
    Public gstrSQLPasswordEMR As String

    'sarika 22nd may 07
    Public gstrLoginTime As String


    ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table        
    Public gstrServicesDBName As String
    Public gstrServicesServerName As String
    Public gstrServicesUserID As String = ""
    Public gstrServicesPassWord As String = ""
    Public gbServicesIsSQLAUTHEN As Boolean
    ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table        


    Public gstrDomainName As String
    Public gstrWindowsServerName As String

    Public gstrConnectionString As String
    Private _gstrRxReportpath As String

    Public gintNoOfAttempts As Integer = 0
    Public gblnAdmin As Boolean

    'sarika 11th sept 07
    Public gblnErrorLogged As Boolean = False
    Public gstrCategory As String = ""

    'sarika 12th sept 07
    Public gstrgloDataPath As String = ""
    '----------------
    Public DMSRootPath As String = ""

    'sarika 13th sept 07
    Public gstrAutoUpdateSourcePath As String = ""
    Public gstrAutoUpdateInterval As String = ""
    Public gstrExitAutoUpdate As Integer = 0
    Public gloHL7RootPath As String = ""

    'code by supriya 11/7/2008
    Public gblnIsSurescriptEnabled As Boolean
    Public gblnIsStagingServer As Boolean
    'code by supriya 11/7/2008
    '----------------------------------------------------------------------------------------------

    ''Eight Alphabet Key for Encrypt/Decrypt FTP User's Password
    ' Public Const constEncryptDecryptKey As String = "12345678"
    '' This Encryption Key will be use for gloEMR User Pasword Encrption/Decription
    Public Const constEncryptDecryptKey As String = "12345678"

    '' This Encryption Key will be use for Services & SQL Password
    Public Const constEncryptDecryptKey_Services As String = "20gloStreamInc08"
    ''

    ''Sandip Darade 20090420
    Public gnLoginID As Long
    Public gstrVersion As String = ""

    Public gnClinicID As Long = 1
    ''Sandip Darade 20090725
    ''value that will decide whether Admin is PM Admin or EMR Admin 
    Public gstrAdminFor As String = ""
    ''Sandip Darade 20090708
    Public gstrSQLUser As String
    Public gstrSQLPassword As String
    Public gblnWindowsAuthentication As Boolean

    ''Sandip Darade 20090830
    Public gstrGeniusPath As String = ""
    Public gstrGeniusCode As String = ""
    Public gstrgloAusPortalURL As String = String.Empty
    Public gstrDemoNPIs As String = String.Empty
    Public gstrClinicExternalCode As String = String.Empty
    Public Function GetConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
        Dim strConnectionString As String
        If isSQLAuthentication = False Then
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
        Else
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
        End If
        Return strConnectionString
    End Function

    'sarika PM DB Credentials 20081128
    Public Function GetConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal strUserID As String, ByVal strPwd As String) As String
        Dim strConnectionString As String
        strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & strUserID & ";pwd=" & strPwd
        Return strConnectionString
    End Function

    '---

    'sarika 22nd May 07
    Public Function RetrieveVersion() As String
        'Return Trim(GetSetting("gloEMR", "gloEMR Settings", "Version"))
        Return "5.0"
    End Function

    Public Function GetConnectionString() As String
        Return GetConnectionString(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
    End Function

    'Public Function GetArchiveConnectionString() As String
    '    Return GetConnectionString(gstrSQLServerName, gstrArchiveDatabaseName)
    'End Function

    Public Function GetArchiveConnectionString() As String
        Try
            If gstrArchiveDatabaseName = "" Then
                MessageBox.Show("Please set the archive database from settings and try again.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return ""
            End If
            Return GetConnectionString(gstrSQLServerName, gstrArchiveDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try

    End Function

    Interface IDataDictionary
        Function GetDictionary(ByVal m_flag As Boolean) As DataTable
        Function getReportData(ByVal strselect As String) As System.Data.DataTable
        Function GetClinicLogo() As DataTable
        Function GetProviderSign() As DataTable
        Function GetProviders() As ArrayList
    End Interface

    Interface ISectionDetails
        Property Suppress() As Boolean
        Property Height() As System.Single
    End Interface

    Public Property gstrRxReportpath() As String
        Get
            Return _gstrRxReportpath
        End Get
        Set(ByVal Value As String)
            _gstrRxReportpath = Value
        End Set
    End Property

    'sarika Audit Log Instr Search
    Public Function ReplaceSpecialCharacters(ByVal strSpecialChar As String) As String
        Try

            strSpecialChar = Replace(strSpecialChar, "#", "") & ""
            strSpecialChar = Replace(strSpecialChar, "$", "") & ""
            strSpecialChar = Replace(strSpecialChar, "%", "") & ""
            strSpecialChar = Replace(strSpecialChar, "^", "") & ""
            strSpecialChar = Replace(strSpecialChar, "&", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "~", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "!", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "*", "") & ""
            'strSpecialChar = Replace(strSpecialChar, ";", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "/", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "?", "") & ""
            'strSpecialChar = Replace(strSpecialChar, ">", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "<", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "\", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "|", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "{", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "}", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "-", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "_", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "'", "") & ""
            Return strSpecialChar
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    '------------
    Public Function GetPrefixTransactionID() As Long
        Dim strID As String
        Dim dtDate As DateTime
        dtDate = System.DateTime.Now
        strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), Now.Date)
        Return CLng(strID)
    End Function

End Module
