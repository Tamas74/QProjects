Imports System.Data.SqlClient
Imports System.Net

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
    Public gstrMessageBoxCaption As String = "gloEMR Admin"
    Public gstrLoginName As String
    Public gstrLoginPassword As String
    Public Const DTFORMAT As String = "MM/dd/yyyy"
    Public gstrSQLServerName As String
    Public gstrDatabaseName As String
    Public gstrArchiveDatabaseName As String
    Public gblnSQLAuthentication As Boolean
    Public gstrSQLUserEMR As String
    Public gstrSQLPasswordEMR As String
    Public gstrFormularyUserID As String
    Public gstrFormularyPassword As String
    Public gstrFormularySQLServer As String
    Public gstrFormalayrDatabase As String

    ''Added ServicesDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table        
    Public gstrServicesDBName As String
    Public gstrServicesServerName As String
    Public gstrServicesUserID As String = ""
    Public gstrServicesPassWord As String = ""
    Public gbServicesIsSQLAUTHEN As Boolean
    ''Added ServicesDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table        
   

    '-- added by rahul patel on 29-10-2010
    '-- For Dms Database setting -------'
    Public gDmsServerName As String = ""
    Public gDmsDatabaseName As String = ""
    Public gDmsUserID As String = ""
    Public gDmsPassWord As String = ""
    Public gDmsIsSQLAUTHEN As Boolean
    '-----End of code added by rahul patel on 29-10-2010

    'sarika 22nd may 07
    Public gstrLoginTime As String

    '' ApplicatioType 
    Public gnApplicationType As Int64 = 0

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
    Public gIscommunitystaging As Boolean = True

    'Code Start-Added by kanchan on 20120731 for gloCommunity
    Public gstrGCUserName As String = String.Empty
    Public gstrGCPassword As String = String.Empty
    Public gstr_Layouts As String = "_layouts"
    Public gstrSharepointSrvNm As String = String.Empty
    Public gstrSharepointSiteNm As String = String.Empty
    Public SharepointAuthentication As String = String.Empty
    Public gstrClinicName As String = String.Empty
    Public gstrClinicExternalCode As String = String.Empty
    Public oFormCookie As Cookie = Nothing
    'Code End-Added by kanchan on 20120731 for gloCommunity


    Public gstrgloAusPortalURL As String = String.Empty
    Public gstrDemoNPIs As String = String.Empty

    'sarika 13th sept 07
    Public gstrAutoUpdateSourcePath As String = ""
    Public gstrAutoUpdateInterval As String = ""
    Public gstrExitAutoUpdate As Integer = 0
    Public gloHL7RootPath As String = ""

    'code by supriya 11/7/2008
    Public gblnIsSurescriptEnabled As Boolean
    Public gblnIsStagingServer As Boolean
    Public gbln10dot6Version As Boolean = False
    Public gblnSecureMessage4dot5 As Boolean = False
    'code by supriya 11/7/2008

    Public gstrSTAGING10DOT6ACCOUNTID As String = ""
    Public gstrSTAGING10DOT6PORTALID As String = ""
    Public gstrSTAGING8DOT1PORTALID As String = ""


    Public gstrSSPRODUCTIONACCOUNTID As String = ""
    Public gstrSSPRODUCTIONPORTALID As String = ""
    Public gstrSSPRODUCTION10dot6PORTALID As String = ""

    Public gblnIsSecureMsgEnabled As Boolean
    Public gblnIsSecureMsgStagingServer As Boolean
    '----------------------------------------------------------------------------------------------

    ''Eight Alphabet Key for Encrypt/Decrypt FTP User's Password
    ' Public Const constEncryptDecryptKey As String = "12345678"
    '' This Encryption Key will be use for gloEMR User Pasword Encrption/Decription
    ''for any user wherther it will be og gloEMR or any outside user like here Fax User we have to have to use gloEMR Encription Key
    Public Const constEncryptDecryptKey As String = "12345678"

    '' This Encryption Key will be use for Services & SQL Password
    Public Const constEncryptDecryptKey_Services As String = "20gloStreamInc08"
    ''

    ''Sandip Darade 20090420
    Public gnLoginID As Long
    Public gstrVersion As String = "5.0.2.0"
    Public gstrMktngVersion As String

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
    Public gstrCountry As String = ""

    ''
    Public gstrLoginBanner As String = ""
    'commented by manoj jadhav for providing common UI for Device Activation on 20111003

    'Public strDiveceName As String = String.Empty 'added by RK
    'Public blnActivation As Boolean = False  'added by RK
    ''
    'commented by manoj jadhav for providing common UI for Device Activation on 20111003
    'Shubhangi 20100105
    'Enum related to EMExamtype
    Public Enum enumExamControlType
        None = 0
        GeneralMultiSystem = 1
        Cardiovascular = 2
        EarsNoseThroat = 3
        Eye = 4
        Genitourinary = 5
        HemaLymphImmuno = 6
        Musculoskeletal = 7
        Neurological = 8
        Psychiatric = 9
        Respiratory = 10
        Skin = 11
        Pre97Guidelines = 12
    End Enum
    'End

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
                ''MessageBox.Show("Please set the archive database from settings and try again.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ''Sandip  Darade 200091217
                MessageBox.Show("Archive database not set. Set archive database from Startup settings.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
    Public Sub fillStates(ByVal cmbBox As ComboBox)
        'function is added by dipak 20090914 for fil state State combobox 
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        oDB.Connect(False)
        Try
            Dim dtStates As New DataTable()
            Dim _sqlQuery As String = "SELECT distinct ST FROM CSZ_MST order by ST"
            oDB.Retrive_Query(_sqlQuery, dtStates)
            oDB.Disconnect()

            If dtStates IsNot Nothing Then
                Dim dr As DataRow = dtStates.NewRow()
                dr("ST") = ""
                dtStates.Rows.InsertAt(dr, 0)
                dtStates.AcceptChanges()

                cmbBox.DataSource = dtStates
                cmbBox.DisplayMember = "ST"
            End If
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If

        End Try
        'end dipak 20090912
    End Sub
    Public Sub SetListBoxToolTip(ByVal LstBox As CheckedListBox, ByVal C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip, ByVal Position As Point)
        ''' ''''''''''''' Integrated by Mayuri:20100731 to give tooltip to Listbox 
        Dim MousePositionInClientCoords As Point = LstBox.PointToClient(Position)
        Dim indexUnderTheMouse As Integer = LstBox.IndexFromPoint(MousePositionInClientCoords)
        If indexUnderTheMouse > -1 Then
            LstBox.SelectedIndex = indexUnderTheMouse
            Dim s As String = CType(LstBox.Items(LstBox.SelectedIndex), myList).AssociatedCategory
            Dim g As Graphics = LstBox.CreateGraphics
            If g.MeasureString(s, LstBox.Font).Width > LstBox.ClientRectangle.Width Then
                C1SuperTooltip1.SetToolTip(LstBox, s)
            Else
                C1SuperTooltip1.SetToolTip(LstBox, "")
            End If
            g.Dispose()
        End If
        ''' ''''''''''''' Integrated by Mayuri :20100731-to give tooltip to Listbox
    End Sub
    Public Sub Cleanup_TempFolder()
        Dim _ExamTempFolder As String = gloSettings.FolderSettings.AppTempFolderPath
        If System.IO.Directory.Exists(_ExamTempFolder) = True Then            
            System.IO.Directory.Delete(_ExamTempFolder, True)
        End If
    End Sub
    ''Added for gloCommunity Form authentication on 20120806
    Public Sub Fill_Clinic()
        Dim oCon As New SqlConnection(GetConnectionString())
        Dim oCmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strQuery As String = String.Empty
        Dim dtClinics As New DataTable()
        Try
            '' Get the Clinic Information
            strQuery = "select ISNULL(nClinicID,0) AS nClinicID ,ISNULL(sClinicName,'') AS sClinicName  from Clinic_MST"
            oCmd.Connection = oCon
            oCmd.CommandText = strQuery
            da.SelectCommand = oCmd
            da.Fill(dtClinics)
            If dtClinics IsNot Nothing AndAlso dtClinics.Rows.Count > 0 Then
                gstrClinicName = dtClinics.Rows(0)("sClinicName")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            'Code opti
            If Not IsNothing(dtClinics) Then
                dtClinics.Dispose()
                dtClinics = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(oCmd) Then
                oCmd.Dispose()
                oCmd = Nothing
            End If
            If Not IsNothing(oCon) Then
                oCon.Dispose()
                oCon = Nothing
            End If
        End Try
    End Sub
    ''End
End Module
