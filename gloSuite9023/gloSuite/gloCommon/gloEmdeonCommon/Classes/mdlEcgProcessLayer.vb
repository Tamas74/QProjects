Imports ECGMgmtCom
Imports QuintonECG
Imports gloDatabaseLayer
Public Module mdlEcgProcessLayer

#Region "ECG Variables"
    Public sECGUserName As String = String.Empty
    Public sECGPassword As String = String.Empty
    Public sECGProviderId As String = String.Empty
    Public sECGProviderName As String = String.Empty
    Public sECGInstitutionId As String = String.Empty
    Public sECGUrl As String = String.Empty
#End Region

    Public sConnectionString As String = String.Empty
    ''' <summary>
    ''' Method to validate EcgUser Information.
    ''' </summary>
    ''' <param name="nUserID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateUserSettings(ByVal nUserID As Int64) As Boolean
        Dim objUser As clsECGUsers
        Dim blnResult As Boolean = False
        Try

            If nUserID <= 0 Then
                Return False
            End If

            'objUser = New clsECGUsers()
            objUser = GetDeviceSettings(nUserID)

            If Not IsNothing(objUser) Then

                If objUser.IsECGActivated = False Then
                    Return False
                End If

                If objUser.sECGUrl.Length > 0 Then
                    sECGUrl = objUser.sECGUrl
                Else
                    Return False
                End If

                If objUser.sEcgUserId.Length > 0 Then
                    sECGProviderId = objUser.sEcgUserId
                Else
                    Return False
                End If

                If objUser.sInstutionId.Length > 0 Then
                    sECGInstitutionId = objUser.sInstutionId
                Else
                    Return False
                End If

                If objUser.sPassword.Length > 0 Then
                    sECGPassword = objUser.sPassword
                Else
                    Return False
                End If

                If objUser.sUserName.Length > 0 Then
                    sECGUserName = objUser.sUserName
                Else
                    Return False
                End If
                blnResult = True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        Finally
            objUser = Nothing
        End Try
        Return blnResult
    End Function
    Private Function GetDeviceSettings(ByVal nUserID As Int64) As clsECGUsers
        Dim objUser As clsECGUsers
        Dim objFinalUser As New clsECGUsers

        Try
            'objUser = New clsECGUsers()
            objUser = GetECGDeviceSettings()

            If Not IsNothing(objUser) Then
                objFinalUser.sInstutionId = objUser.sInstutionId
                objFinalUser.sECGUrl = objUser.sECGUrl
                objFinalUser.sEcgUserId = objUser.sEcgUserId
                objFinalUser.IsECGActivated = objUser.IsECGActivated
            Else
                Return Nothing
            End If

            'objUser = New clsECGUsers()
            objUser = GetUserSettings(nUserID)
            If Not IsNothing(objUser) Then
                objFinalUser.sUserName = objUser.sUserName
                objFinalUser.sPassword = objUser.sPassword
            Else
                Return Nothing
            End If
            objUser = Nothing


        Catch ex As Exception
            Return Nothing
        Finally
            objUser = Nothing
        End Try
        Return objFinalUser
    End Function
    ''' <summary>
    ''' Method to GetECGUsers.
    ''' </summary>
    ''' <param name="nUserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUserSettings(ByVal nUserId As Int64) As clsECGUsers
        Dim objEcgUsers As New clsECGUsers()
        Dim odbLayer As New DBLayer(sConnectionString)
        Dim sQuery As String = String.Empty
        Dim dtUSers As New DataTable
        Dim oDecryption As New gloSecurity.ClsEncryption()
        Dim _encryptionKey As String = "12345678"

        Try
            'start of code commented by manoj jadhav on 20111102
            'sQuery = "SELECT     ISNULL(User_MST.sLoginName,'') as UserName, ISNULL(User_ExternalCodes.sPassword,'') as sPassword,ISNULL(IsEncrypted,'') as IsEncrypted " & _
            '        "FROM User_MST INNER JOIN  User_ExternalCodes ON User_MST.nUserID = User_ExternalCodes.nUserId Where User_ExternalCodes.sModulename='ECG' AND User_MST.nUserID= " & nUserId
            'start of code commented by manoj jadhav on 20111102 

            sQuery = "SELECT TOP 1 [sDeviceUserName] as UserName ,[sPassword] as sPassword ,[IsEncrypted] as IsEncrypted FROM  [dbo].[User_ExternalCodes]  Where nUserId =" & nUserId & " AND sModulename='ECG'" 'added new line code code commented by manoj jadhav on 20111102 

            odbLayer.Connect(False)
            odbLayer.Retrive_Query(sQuery, dtUSers)
            odbLayer.Disconnect()

            If IsNothing(dtUSers) OrElse dtUSers.Rows.Count <= 0 Then
                Return Nothing
            End If

            If String.IsNullOrEmpty(Convert.ToString(dtUSers.Rows(0)("IsEncrypted"))) Or Convert.ToBoolean(dtUSers.Rows(0)("IsEncrypted")) = False Then
                objEcgUsers.sPassword = Convert.ToString(dtUSers.Rows(0)("sPassword"))
            Else
                objEcgUsers.sPassword = oDecryption.DecryptFromBase64String(Convert.ToString(dtUSers.Rows(0)("sPassword")), _encryptionKey)
            End If

            objEcgUsers.sUserName = Convert.ToString(dtUSers.Rows(0)("UserName"))

        Catch ex As Exception
            objEcgUsers = Nothing
        Finally
            If Not IsNothing(odbLayer) Then
                odbLayer.Dispose()
            End If
            If Not IsNothing(dtUSers) Then
                dtUSers.Dispose()
            End If
            sQuery = String.Empty
            oDecryption = Nothing
            _encryptionKey = String.Empty
        End Try
        Return objEcgUsers
    End Function

    ''' <summary>
    ''' Method to getDeviceSettings
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetECGDeviceSettings() As clsECGUsers
        Dim objEcgUsers As New clsECGUsers()
        Dim odbLayer As New DBLayer(sConnectionString)
        Dim sQuery As String = String.Empty
        Dim dtSettings As New DataTable
        Try
            sQuery = "SELECT ISNULL(sSettingsName,'') as SettingName,ISNULL(sSettingsValue,'') as SettingValue from settings where sSettingsName in ('ECGINSTUTIONID','ECGINTERFACEURL','ECGUSERPROVIDERID','ECGENABLED')"

            odbLayer.Connect(False)
            odbLayer.Retrive_Query(sQuery, dtSettings)
            odbLayer.Disconnect()

            If IsNothing(dtSettings) OrElse dtSettings.Rows.Count <= 0 Then
                Return Nothing
            End If

            For index As Integer = 0 To dtSettings.Rows.Count - 1

                Select Case Convert.ToString(dtSettings.Rows(index)("SettingName"))
                    Case "ECGINSTUTIONID"
                        objEcgUsers.sInstutionId = Convert.ToString(dtSettings.Rows(index)("SettingValue"))
                        Exit Select
                    Case "ECGINTERFACEURL"
                        objEcgUsers.sECGUrl = Convert.ToString(dtSettings.Rows(index)("SettingValue"))
                        Exit Select
                    Case "ECGUSERPROVIDERID"
                        objEcgUsers.sEcgUserId = Convert.ToString(dtSettings.Rows(index)("SettingValue"))
                        Exit Select
                    Case "ECGENABLED"
                        If Convert.ToString(dtSettings.Rows(index)("SettingValue")) = "True" Then
                            objEcgUsers.IsECGActivated = True
                        Else
                            objEcgUsers.IsECGActivated = False
                        End If
                        Exit Select

                    Case Else
                End Select
            Next

        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(odbLayer) Then
                odbLayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
        Return objEcgUsers
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nErrorCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetErrorString(ByVal nErrorCode As Integer) As String
        Dim sExceptionString As String = String.Empty

        Select Case nErrorCode
            Case CInt(ErrorExceptions.ERR_ALREADY_LOGGED_IN)
                sExceptionString = "Already Login"
                Exit Select
            Case CInt(ErrorExceptions.ERR_BACKUP_IN_PROGRESS)
                sExceptionString = "Backup progress"
                Exit Select
            Case CInt(ErrorExceptions.ERR_CALL_CANCELLED)
                sExceptionString = "Call Cancelled"
                Exit Select
            Case CInt(ErrorExceptions.ERR_CANNOT_CONNECT_TO_SERVER)
                sExceptionString = "Cannot connect server"
                Exit Select
            Case CInt(ErrorExceptions.ERR_CONTROL_BUSY)
                sExceptionString = "Control busy"
                Exit Select
            Case CInt(ErrorExceptions.ERR_DATA_NOT_AVAILABLE)
                sExceptionString = "Data not available"
                Exit Select
            Case CInt(ErrorExceptions.ERR_DEVICE_UNAVAILABLE)
                sExceptionString = "Device unavailable"
                Exit Select
            Case CInt(ErrorExceptions.ERR_EXPORT_FAILED)
                sExceptionString = "Export failed"
                Exit Select
            Case CInt(ErrorExceptions.ERR_GENERAL_PRINT_FAILURE)
                sExceptionString = "Print failure"
                Exit Select
            Case CInt(ErrorExceptions.ERR_INVALID_ORDER_ID)
                sExceptionString = "Invalid orderId"
                Exit Select
            Case CInt(ErrorExceptions.ERR_INVALID_TEST_ID)
                sExceptionString = "Invalid TestId"
                Exit Select
            Case CInt(ErrorExceptions.ERR_INVALID_TICKET)
                sExceptionString = "Invalid Ticket"
                Exit Select
            Case CInt(ErrorExceptions.ERR_INVALID_USERID_PASSWORD)
                sExceptionString = "Unable to connect to ECG device, Please check ECG device credentials in Admin."
                Exit Select
            Case CInt(ErrorExceptions.ERR_INVALID_VALUE)
                sExceptionString = "Invalid Value"
                Exit Select
            Case CInt(ErrorExceptions.ERR_LOCAL_DEVICE_ERROR)
                sExceptionString = "Device Error"
                Exit Select
            Case CInt(ErrorExceptions.ERR_MISSING_REQUIRED_DATA)
                sExceptionString = "Missing required data"
                Exit Select
            Case CInt(ErrorExceptions.ERR_MORE_DATA_AVAILABLE)
                sExceptionString = "More Data available"
                Exit Select
            Case CInt(ErrorExceptions.ERR_MULTIPLE_TESTS_FOUND)
                sExceptionString = "Error Multiple tests found"
                Exit Select
            Case CInt(ErrorExceptions.ERR_NO_ACCESS_TO_INSTITUTION)
                sExceptionString = "Error no access to institution, Please verify you have correct id in admin settings."
                Exit Select
            Case CInt(ErrorExceptions.ERR_NO_PERMISSION)
                sExceptionString = "No Permission"
                Exit Select
            Case CInt(ErrorExceptions.ERR_NOT_FINDING_TEST)
                sExceptionString = "Error not finding test"
                Exit Select
            Case CInt(ErrorExceptions.ERR_NOT_LOGGED_IN)
                sExceptionString = "Error not logged in"
                Exit Select
            Case CInt(ErrorExceptions.ERR_NULL_OBJECT_REFERENCE)
                sExceptionString = "Null object reference "
                Exit Select
            Case CInt(ErrorExceptions.ERR_ORDER_ATTACHED)
                sExceptionString = "Error order attached"
                Exit Select
            Case CInt(ErrorExceptions.ERR_SINGLE_INSTITUTION)
                sExceptionString = "Error in single instution"
                Exit Select
            Case CInt(ErrorExceptions.ERR_TEST_CANCELLED)
                sExceptionString = "Erro test cancelled"
                Exit Select
            Case CInt(ErrorExceptions.ERR_TEST_CHECKED_OUT)
                sExceptionString = "Test checked out"
                Exit Select
            Case CInt(ErrorExceptions.ERR_TEST_DELETED)
                sExceptionString = "Test deleted"
                Exit Select
            Case CInt(ErrorExceptions.ERR_TEST_LOCKED)
                sExceptionString = "Test locked"
                Exit Select
            Case CInt(ErrorExceptions.ERR_UNEXPECTED)
                sExceptionString = "Error unexpected"
                Exit Select
            Case CInt(ErrorExceptions.ERR_UPLOAD_FAILED)
                sExceptionString = "Upload Failed"
                Exit Select
            Case CInt(ErrorExceptions.ERR_XML_PARSE_ERROR)
                sExceptionString = "Error while parsing XML"
                Exit Select
            Case Else
                sExceptionString = "UnKnown error generated"
                Exit Select
        End Select

        Return sExceptionString
    End Function

    Public Class clsECGUsers
        Public sUserName As String = String.Empty
        Public sPassword As String = String.Empty
        Public sEcgUserId As String = String.Empty
        Public sInstutionId As String = String.Empty
        Public sECGUrl As String = String.Empty
        Public IsECGActivated As Boolean = False
    End Class
End Module
