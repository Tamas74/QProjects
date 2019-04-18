Imports gloSureScript
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Xml.Serialization
Imports System.IO

Public Class AddUpdatePrescriber
    Implements IDisposable
    Private oProvider As clsProvider
    Private disposedValue As Boolean = False        ' To detect redundant calls

    Private bCIMessage As Boolean = False
    Private bCIEvent As Boolean = False

    'sarika for removing provider validations
    Public strMessage As String = ""
    Public Event SurescriptError()
    '--
    Private blnSendSecureMessage As Boolean = False
    Public Property IsSecureMessage() As Boolean
        Get
            Return blnSendSecureMessage
        End Get
        Set(ByVal value As Boolean)
            blnSendSecureMessage = value
        End Set
    End Property

    Public Property ProviderObject() As clsProvider
        Get
            Return oProvider
        End Get
        Set(ByVal value As clsProvider)
            oProvider = value
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
        gloSurescriptGeneral.ServerName = gstrSQLServerName
        gloSurescriptGeneral.DatabaseName = gstrDatabaseName
        gloSurescriptGeneral.RootPath = System.Windows.Forms.Application.StartupPath
        gloSurescriptGeneral.blnStagingServer = gblnIsStagingServer
        gloSurescriptGeneral.blnSQLAuthentication = gblnSQLAuthentication
        gloSurescriptGeneral.UserName = gstrSQLUserEMR
        gloSurescriptGeneral.Password = gstrSQLPasswordEMR
        gloSurescriptGeneral.blnNCPDP10dot6 = gbln10dot6Version
        gloSurescriptGeneral.checkDownloadVersion()
        gloSurescriptGeneral.blnSecureMessage4dot5 = gblnSecureMessage4dot5

        ''production 1 account 2 portals
        gloSurescriptGeneral.sSSPRODUCTIONACCOUNTID = gstrSSPRODUCTIONACCOUNTID ''261 common for both 10.6 and 8.1 portals
        gloSurescriptGeneral.sSSPRODUCTIONPORTALID = gstrSSPRODUCTIONPORTALID ''264 for 8.1 portal
        gloSurescriptGeneral.sSSPRODUCTION10dot6PORTALID = gstrSSPRODUCTION10dot6PORTALID ''1018 for 10.6 portal

        gloSurescriptGeneral.sSTAGING10DOT6ACCOUNTID = gstrSTAGING10DOT6ACCOUNTID ''338 common for both 10.6 and 8.1 portals
        gloSurescriptGeneral.sSTAGING10DOT6PORTALID = gstrSTAGING10DOT6PORTALID ''2273 for 10.6 portal
        gloSurescriptGeneral.sSTAGING8DOT1PORTALID = gstrSTAGING8DOT1PORTALID ''422 for 8.1 portal
    End Sub
    Public Function AddPrescriber() As Boolean
        Dim ogloInterface As gloSureScriptInterface = New gloSureScriptInterface

        Try
            If Not IsNothing(oProvider) Then
                Dim oPrescriber As Prescriber = SetPrescriber()
                'sarika for removing Provider validations
                If gloSurescriptGeneral.blnNCPDP10dot6 = True Then
                    strMessage = ogloInterface.ValidateAddPrescriber4dot4(oPrescriber)
                Else
                    strMessage = ogloInterface.ValidateAddPrescriber(oPrescriber)
                End If

                If strMessage <> "" Then
                    Exit Function
                End If

                If gloSurescriptGeneral.blnNCPDP10dot6 = True Then
                    If oPrescriber.IsDirectMessageEnabled Or oPrescriber.IsServiceLevelDisabled Then
                        If (gloSurescriptGeneral.DirectAddressExistsInOphit(oPrescriber.DirectAddress)) Then
                            strMessage = "Provider with Direct Address already exists. Please choose different Direct Address."
                            Exit Function
                        End If
                    End If
                End If

                '---
                If Not IsNothing(oPrescriber) Then
                    'oPrescriber.ClinicName = GetClinicname()
                    Dim blnStatus As Boolean = False
                    blnStatus = ogloInterface.GenerateAddPrescriber(oPrescriber)

                    If blnStatus = True Then
                        strMessage = ogloInterface.strMessage
                        oProvider.SPI = ogloInterface.SPI 'this is the SPI returned by surescript
                        oProvider.DirectAddress = ogloInterface.DirectAddress 'this is the direct address returned by surescript

                        If ogloInterface.StatusMessageType <> "" Then

                            gloSurescriptGeneral.InformationMessage(ogloInterface.StatusMessageType)
                            If ogloInterface.MessageName = "Error" Then
                                RaiseEvent SurescriptError()
                            Else
                                If gloSurescriptGeneral.blnNCPDP10dot6 = True Then
                                    If oPrescriber.IsDirectMessageEnabled Or oPrescriber.IsServiceLevelDisabled Then
                                        gloSurescriptGeneral.AddUpdateProviderInOphit(oPrescriber, False)
                                    End If
                                End If
                            End If
                        End If
                        Return True
                    End If

                End If

            End If

        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
        End Try
    End Function
    Public Function AddPrescriberLocation() As Boolean
        Dim ogloInterface As gloSureScriptInterface = New gloSureScriptInterface
        Dim strMessage As String = ""

        Try
            If Not IsNothing(oProvider) Then
                Dim oPrescriber As Prescriber = SetPrescriber()
                oPrescriber.PrescriberID = oProvider.RootSPI 'The SPI which will be used to retrive the new SPI
                ' sarika for removing Provider validations
                strMessage = ogloInterface.ValidateAddPrescriberLocation(oPrescriber)
                If strMessage <> "" Then
                    Exit Function
                End If
                '--
                If Not IsNothing(oPrescriber) Then
                    oPrescriber.ClinicName = GetClinicname()
                    Dim blnStatus As Boolean = False



                    blnStatus = ogloInterface.GenerateAddPrescriberLocation(oPrescriber)
                    If blnStatus Then
                        oProvider.SPI = ogloInterface.SPI 'this is the SPI returned by surescript
                        oProvider.DirectAddress = ogloInterface.DirectAddress 'this is the direct address returned by surescript
                        If ogloInterface.StatusMessageType <> "" Then
                            gloSurescriptGeneral.InformationMessage(ogloInterface.StatusMessageType)
                            If ogloInterface.MessageName = "Error" Then
                                RaiseEvent SurescriptError()
                            End If
                        End If
                        Return True
                    End If
                End If

            End If

        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
        End Try
    End Function

    Public Function UpdatePrescriberLocation(bIsServiceLevelDisabled As Boolean, ByVal IsUpdating As Boolean) As Boolean
        Dim ogloInterface As gloSureScriptInterface = New gloSureScriptInterface


        Try
            If Not IsNothing(oProvider) Then
                Dim oPrescriber As Prescriber = SetPrescriber()
                'sarika for removing Provider validations
                strMessage = ogloInterface.ValidateAddPrescriberLocation(oPrescriber)
                If strMessage <> "" Then
                    'MessageBox.Show(strMessage, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Function
                End If
                '-----
                If Not IsNothing(oPrescriber) Then
                    oPrescriber.ClinicName = GetClinicname()
                    Dim blnStatus As Boolean = False

                    If Not IsUpdating Then
                        If Not gloSurescriptGeneral.DirectAddressExistsInOphit(oPrescriber.DirectAddress) Then
                            If oProvider.IsCIMessageEnabled AndAlso oProvider.IsDirectMessageEnabled Then
                                'gloSurescriptGeneral.AddUpdateProviderInOphit(oPrescriber, IsUpdating)
                                blnStatus = ogloInterface.GenerateUpdatePrescriberLocation(oPrescriber)
                            End If
                        Else
                            'Message box which shows Direct Address exists in Ophit
                            strMessage = "Provider with Direct Address already exists. Please choose different Direct Address."
                            Exit Function
                        End If
                    Else
                        'It is logical update. Direct Address exists in Ophit
                        blnStatus = ogloInterface.GenerateUpdatePrescriberLocation(oPrescriber)
                        'gloSurescriptGeneral.AddUpdateProviderInOphit(oPrescriber, True)
                    End If

                    If gloSurescriptGeneral.blnNCPDP10dot6 = True Then
                        If blnStatus AndAlso ogloInterface.StatusMessageType <> "" AndAlso ogloInterface.MessageName <> "Error" Then
                            gloSurescriptGeneral.AddUpdateProviderInOphit(oPrescriber, IsUpdating)
                        End If
                    End If

                    If blnStatus Then 'Used to Update the existing Prescriber
                        If ogloInterface.StatusMessageType <> "" Then
                            gloSurescriptGeneral.InformationMessage(ogloInterface.StatusMessageType)
                            If ogloInterface.MessageName = "Error" Then
                                RaiseEvent SurescriptError()
                            ElseIf bIsServiceLevelDisabled Then
                                UpdateDirectAddress(oPrescriber.ProviderID, "", oProvider.ServiceLevel)
                            Else
                                UpdateDirectAddress(oPrescriber.ProviderID, oPrescriber.DirectAddress, oProvider.ServiceLevel)
                            End If
                            Return True
                        End If
                    End If
                End If

            End If

        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
        End Try
    End Function


    Public Function UpdateDirectAddress(nProviderID As Int64, sDirectAddress As String, sServiceLevel As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As SqlCommand

        Try
            objCon.Open()

            objCmd = New SqlCommand
            objCmd.Connection = objCon
            objCmd.CommandType = CommandType.Text
            Dim _strsql As String = "Update Provider_mst set sServiceLevel = '" & sServiceLevel & "' ,sDirectAddress = '" & sDirectAddress & "' where nProviderId = " & nProviderID & ""
            objCmd.CommandText = _strsql

            objCmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            Return False
        Finally
            objCon.Close()
        End Try

    End Function

    Private Function GetClinicname() As String
        Dim ogloDBLayer As gloSureScriptDBLayer = New gloSureScriptDBLayer

        Try
            Dim clinicname As String = ogloDBLayer.GetClinicName()
            Return clinicname

        Catch ex As GloSurescriptException
            Return ""
            'gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(ogloDBLayer) Then
                ogloDBLayer.Dispose()
                ogloDBLayer = Nothing
            End If
        End Try
    End Function
    Public Function SetPrescriber() As Prescriber
        Dim oPrescriber As New Prescriber

        Try
            oPrescriber.AUSID = Convert.ToString(oProvider.AUSID)
            oPrescriber.ClinicName = Convert.ToString(oProvider.ClinicName)

            oPrescriber.PrescriberName.FirstName = Convert.ToString(oProvider.FirstName)
            oPrescriber.PrescriberName.MiddleName = Convert.ToString(oProvider.MiddleName)
            oPrescriber.PrescriberName.LastName = Convert.ToString(oProvider.LastName)
            oPrescriber.Gender = Convert.ToString(oProvider.Gender)
            oPrescriber.DEA = Convert.ToString(oProvider.DEA)

            oPrescriber.PrescriberAddress.Address1 = oProvider.BMAddress1
            oPrescriber.PrescriberAddress.Address2 = oProvider.BMAddress2
            oPrescriber.PrescriberAddress.City = oProvider.BMCity
            oPrescriber.PrescriberAddress.State = oProvider.BMState
            oPrescriber.PrescriberAddress.Zip = oProvider.BMZIP
            oPrescriber.PrescriberPhone.Phone = oProvider.BMPhone
            oPrescriber.DEA = oProvider.DEA
            oPrescriber.NPI = oProvider.NPI
            oPrescriber.ProviderID = oProvider.ProviderID
            oPrescriber.ServiceLevel = Convert.ToInt32(oProvider.ServiceLevel, 2)

            oPrescriber.PrescriberPhone.Fax = oProvider.BMFAX
            oPrescriber.ActiveStartDate = getUTCDate(oProvider.ActiveStartTime)
            oPrescriber.ActiveEndDate = getUTCDate(oProvider.ActiveEndTime)
            oPrescriber.PrescriberID = oProvider.SPI


            'sarika For Removing Provider validations
            oPrescriber.PrescriberLocation = oProvider.PrescriberLocation
            '--
            oPrescriber.ProviderSSN = oProvider.SSNno '' added for SS 10.6 change

            oPrescriber.NPI = Convert.ToString(oProvider.NPI)
            oPrescriber.DirectAddress = Convert.ToString(oProvider.DirectAddress)
            oPrescriber.IsDirectMessageEnabled = Convert.ToString(oProvider.IsDirectMessageEnabled)
            oPrescriber.IsNewRxEnabled = Convert.ToString(oProvider.IsNewRxEnabled)
            oPrescriber.IsControlledSubstance = Convert.ToString(oProvider.IsControlledSubstance)
            oPrescriber.IsePAEnabled = Convert.ToString(oProvider.IsePAEnabled)
            oPrescriber.DatabaseName = Convert.ToString(oProvider.DatabaseName)
            oPrescriber.gloSuiteVersion = Convert.ToString(oProvider.gloSuiteVersion)

            'Need to add PrescriberID
            Return oPrescriber
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function getUTCDate(ByVal dtactivedate As DateTime) As String
        Dim oUTCTime As DateTime
        Dim localZone As TimeZone = TimeZone.CurrentTimeZone
        oUTCTime = localZone.ToUniversalTime(dtactivedate)

        Dim dtdate As DateTime = oUTCTime
        Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
        Dim strtime As String = Format(dtdate, "hh:mm:ss")
        Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
        strUTCFormat = strdate & "T" & strtime & ".0Z"
        'strUTCFormat = strdate & "T" & "00:00:00" & ":0Z"
        Return strUTCFormat
    End Function


    ''' <summary>
    ''' Gets the Prescriber information for the requested SPI
    ''' </summary>
    ''' <param name="objPrescriber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Function GetPrescriber(ByVal objPrescriber As Prescriber) As String

        Dim bError As Boolean = False

        Dim oMessageType As New DirectProvider.MessageType()
        Dim oHeaderType As New DirectProvider.HeaderType()
        Dim oSecurityType As New DirectProvider.SecurityType()
        Dim oUsernameTokenType As New DirectProvider.UsernameTokenType()
        Dim oBodyType As New DirectProvider.BodyType()
        Dim oTo As New DirectProvider.AddressType()
        Dim oFrom As New DirectProvider.AddressType()
        Dim oPrescriber As New DirectProvider.GetPrescriber()

        Dim dtdate As DateTime = Date.UtcNow
        Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
        Dim strtime As String = Format(dtdate, "hh:mm:ss")
        Dim strmillisec As String = Format(dtdate, "mmm")
        Dim strtemp As String = dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString

        Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
        strUTCFormat = strdate & "T" & strtime & ".0Z"


        Dim sErrorObject As DirectProvider.Error = Nothing
        Dim sErrorMessage As String = String.Empty

        Try

            'Message Type
            oMessageType.release = "005"
            oMessageType.version = "004"

            'Header
            oHeaderType.From = "mailto:GLOSTM.dp@surescripts.com"
            'oHeaderType.To = "SSSDIR.dp@surescripts.com"
            oHeaderType.To = "mailto:SSDR45.dp@surescripts.com"
            oHeaderType.MessageID = "GetPr" & strtemp
            oHeaderType.SentTime = strUTCFormat


           

            Dim sUserNamePassword() As String = gloSurescriptGeneral.GetLoginCredentials().Split("|")

            oUsernameTokenType.Username = sUserNamePassword(0)
            oUsernameTokenType.Password = getPwdHash(sUserNamePassword(1))
            oUsernameTokenType.Nonce = strtemp
            oUsernameTokenType.Created = strUTCFormat
            oSecurityType.UsernameToken = oUsernameTokenType

            oHeaderType.Security = oSecurityType

            oPrescriber.SPI = objPrescriber.PrescriberID

            oBodyType.Item = oPrescriber
            oMessageType.Header = oHeaderType
            oMessageType.Body = oBodyType

            Dim byteArray As Byte() = Nothing
            byteArray = GenerateXML(oMessageType)

            Dim strfilename As String

            Dim ogloInterface As New gloSureScriptInterface
            strfilename = ogloInterface.ExtractXML(byteArray)

            If strfilename <> "" Then

                Dim xs As XmlSerializer = Nothing
                Dim fs As FileStream = Nothing
                Dim fileLength As Double = 0.0
                Dim isVaild As Boolean = True
                Dim byteArray2 As Byte() = Nothing
                Dim oPrescriberResponse As New DirectProvider.MessageType
                Try
                    Dim strFileNameResp As String = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
                    'strfilename ="C:\\1.xml"
                    xs = New XmlSerializer(GetType(DirectProvider.MessageType))
                    fs = New FileStream(strfilename, FileMode.Open)
                    oPrescriberResponse = DirectCast(xs.Deserialize(fs), DirectProvider.MessageType)
                    'oPrescriberResponse = xs.Deserialize(fs)
                    fs.Close()

                    Dim oGetPrescriberResponse As DirectProvider.GetPrescriberResponse


                    If TypeOf (oPrescriberResponse.Body.Item) Is gloSureScript.DirectProvider.Error Then
                        sErrorObject = DirectCast(oPrescriberResponse.Body.Item, DirectProvider.Error)
                        bError = True
                        sErrorMessage = sErrorObject.Description

                        Return sErrorMessage
                        Exit Function
                    End If

                    oGetPrescriberResponse = DirectCast(oPrescriberResponse.Body.Item, DirectProvider.GetPrescriberResponse)

                    If oGetPrescriberResponse.Prescriber Is Nothing Then
                        Return False
                        Exit Function
                    End If

                    Dim oDirectoryPrescriberType As DirectProvider.DirectoryPrescriberType
                    oDirectoryPrescriberType = DirectCast(oGetPrescriberResponse.Prescriber, DirectProvider.DirectoryPrescriberType)

                    If oDirectoryPrescriberType.Identification Is Nothing Then
                        Return False
                        Exit Function
                    End If

                    Dim oDirectoryPrescriberIDType As DirectProvider.DirectoryPrescriberIDType
                    oDirectoryPrescriberIDType = DirectCast(oDirectoryPrescriberType.Identification, DirectProvider.DirectoryPrescriberIDType)


                    sErrorMessage = String.Empty
                    If oDirectoryPrescriberIDType IsNot Nothing Then
                        Dim sDirectAddress As String = oDirectoryPrescriberIDType.Items(2)

                        If sDirectAddress <> "" Then
                            'Dim bReturned As Boolean = False
                            If oPrescriber IsNot Nothing Then
                                If Not String.IsNullOrWhiteSpace(objPrescriber.DirectAddress) Then
                                    If objPrescriber.DirectAddress.ToLower = sDirectAddress.ToLower Then
                                        'bReturned = False
                                        'sErrorMessage = "Provider with Direct Address already exists. Please choose different Direct Address."
                                        sErrorMessage = "direct_address_exists"
                                    Else
                                        sErrorMessage = "True"
                                        'bReturned = True
                                    End If
                                Else
                                    sErrorMessage = "direct_address_exists"
                                End If
                            End If
                            Return sErrorMessage
                        Else
                            Return True
                        End If
                    End If

                    Using oSurescriptDBLayer As New gloSureScriptDBLayer
                        oSurescriptDBLayer.InsertintoMessageTransaction(CType(objPrescriber, SureScriptMessage))
                    End Using
                Catch ex As Exception
                Finally
                    If fs IsNot Nothing Then
                        fs = Nothing
                    End If
                    If xs IsNot Nothing Then
                        xs = Nothing
                    End If
                End Try

            Else
                Return False
            End If

            Return (sErrorMessage)

        Catch ex As gloSurescriptDBException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As GloSurescriptException
            gloSurescriptGeneral.ErrorMessage(ex.Message)
        Catch ex As Exception
            Throw New GloSurescriptException(ex.Message)
        End Try

        'End If
    End Function
    Private Function GenerateXML(message As DirectProvider.MessageType) As Byte()

        Dim xs As XmlSerializer = Nothing
        Dim fs As FileStream = Nothing
        Dim fileLength As Double = 0.0
        Dim isVaild As Boolean = True
        Dim byteArray As Byte() = Nothing
        Try
            Dim strFileName As String = GetFileName(gloSettings.FolderSettings.AppTempFolderPath)
            xs = New XmlSerializer(GetType(DirectProvider.MessageType))

            fs = New FileStream(strFileName, FileMode.Create)
            xs.Serialize(fs, message)
            fs.Close()
            byteArray = System.IO.File.ReadAllBytes(strFileName)
            Dim Response As Byte() = Nothing
            Dim key As String = String.Empty

            byteArray = gloSurescriptGeneral.ConvertFiletoBinary(strFileName)
           

            Return byteArray

        Catch ex As Exception
            Return byteArray
        Finally
            If fs IsNot Nothing Then
                fs = Nothing
            End If
            If xs IsNot Nothing Then
                xs = Nothing
            End If
        End Try

    End Function

    Public Shared Function GetFileName(strAppPath As [String]) As [String]
        Try
            Dim _NewDocumentName As String = ""
            Dim _Extension As String = ".xml"
            Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
            Dim i As Integer = 0

            _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") & _Extension

            While File.Exists(Convert.ToString(strAppPath) & "\" & _NewDocumentName) = True
                i = i + 1
                _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") & "-" & i & _Extension
            End While

            Return Convert.ToString(strAppPath) & _NewDocumentName

        Catch ex As Exception
            Return ""
        Finally
        End Try

    End Function


    Private Function getPwdHash(ByVal strpwd As String) As String
        Dim bytHash As Byte()
        Dim uEncode As New System.Text.UnicodeEncoding()
        'Store the source string in a byte array     
        Dim bytSource() As Byte = uEncode.GetBytes(strpwd.ToUpper)
        Dim pwdsha1 As New SHA1CryptoServiceProvider()
        'Create the hash         
        bytHash = pwdsha1.ComputeHash(bytSource)
        'return as a base64 encoded string       
        Return Convert.ToBase64String(bytHash)
    End Function

End Class

