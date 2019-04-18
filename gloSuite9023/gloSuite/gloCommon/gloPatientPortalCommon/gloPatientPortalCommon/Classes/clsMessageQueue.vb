Imports System.Data.SqlClient
Imports gloDatabaseLayer
Public Class ClsMessageQueue
    Public Sub New(ByVal connstring As String, LinkDate As DateTime, nPatID As Int64)
        ConnectionString = connstring
        dtLinkDate = LinkDate
        nPatientID = nPatID
    End Sub
    Public Sub New(ByVal connstring As String, LinkDate As DateTime)
        ConnectionString = connstring
        dtLinkDate = LinkDate
    End Sub
    Public Sub New()

    End Sub

    Public Function InsertInMessageQueue(ByVal OtherID As Int64, ByVal strClientMachineID As String, ByVal strClientMachineName As String, ByVal blnPatientPortalSendActivationEmail As Boolean, ByVal blnPatientPortalActivationEmailAlreadySent As Boolean, Optional ByVal blnPatientPortalRegistrationEmail As Boolean = False, Optional ByVal nPRid As Int64 = 0, Optional ByVal MessageName As String = "", Optional ByVal LoginUserID As Int64 = 0) As String
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim sMessageId As String = String.Empty
        Try
            oDBLayer.Connect(False)
            Dim _value As Object = Nothing
            oDBParameters.Clear()
            oDBParameters.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@MessageName", MessageName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachineID", strClientMachineID, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachinename", strClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nOtherID", OtherID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@sField1", " ", ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sServiceName", "GENERAL", ParameterDirection.Input, SqlDbType.VarChar)
            'Patient Portal
            oDBParameters.Add("@PatientPortalSendActivationEmail", blnPatientPortalSendActivationEmail, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@PatientPortalResentEmail", blnPatientPortalActivationEmailAlreadySent, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@PatientPortalRegistrationEmail", blnPatientPortalRegistrationEmail, ParameterDirection.Input, SqlDbType.Bit)
            'Patient Portal
            oDBParameters.Add("@nPRid", nPRid, ParameterDirection.Input, SqlDbType.BigInt)


            oDBParameters.Add("@nMessageId", 0, ParameterDirection.Output, SqlDbType.BigInt)
            oDBParameters.Add("@nLoginUserID", LoginUserID, ParameterDirection.Input, SqlDbType.BigInt)

            oDBLayer.Execute("Portal_Gl_InsertMessageQueue", oDBParameters, _value)

            sMessageId = Convert.ToString(_value)

            oDBLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDBLayer IsNot Nothing Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try
        Return sMessageId
    End Function

    Public Function InsertBatchInMessageQueue(ByVal dtAccountId As DataTable, ByVal OtherID As Int64, ByVal strClientMachineID As String, ByVal strClientMachineName As String, ByVal blnPatientPortalSendActivationEmail As Boolean, ByVal blnPatientPortalActivationEmailAlreadySent As Boolean, Optional ByVal blnPatientPortalRegistrationEmail As Boolean = False, Optional ByVal nPRid As Int64 = 0, Optional ByVal MessageName As String = "") As String
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim sMessageId As String = String.Empty
        Try
            oDBLayer.Connect(False)
            Dim _value As Object = Nothing
            oDBParameters.Clear()
            oDBParameters.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@MessageName", MessageName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachineID", strClientMachineID, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachinename", strClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nOtherID", OtherID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@sField1", " ", ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sServiceName", "GENERAL", ParameterDirection.Input, SqlDbType.VarChar)
            'oDBParameters.Add("@TVP_Patients", dtPatientId, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@TVP_AccountIds", dtAccountId, ParameterDirection.Input, SqlDbType.Structured)
            oDBLayer.Execute("Portal_Gl_InsertBatchMessageQueue", oDBParameters, _value)
            sMessageId = Convert.ToString(_value)
            oDBLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDBLayer IsNot Nothing Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try
        Return sMessageId
    End Function
    Public Function InsertInExternalCode(ByVal ExternalType As String, ByVal dtLinkDateTime As DateTime) As String
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Dim sPatientPortalGUID As Guid
        sPatientPortalGUID = Guid.NewGuid()

        Dim sGuid As String = String.Empty
        Try
            oDBLayer.Connect(False)
            Dim _value As Object = Nothing
            oDBParameters.Clear()
            oDBParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sExternalType", ExternalType, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sExternalSubType", "PatientInvitation", ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@dtAccessDate", dtLinkDateTime, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@sPatientPortalGUID", sPatientPortalGUID.ToString(), ParameterDirection.InputOutput, SqlDbType.VarChar)


            oDBLayer.Execute("gsp_INUP_PatientPortalExternalCodes", oDBParameters, _value)

            sGuid = Convert.ToString(_value)

            oDBLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDBLayer IsNot Nothing Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
            sPatientPortalGUID = Nothing
        End Try
        Return sGuid
    End Function

    Public Function InsertInMessageQueueForAPI(ByVal OtherID As Int64, ByVal strClientMachineID As String, ByVal strClientMachineName As String, ByVal blnPatientPortalSendActivationEmail As Boolean, ByVal blnPatientPortalActivationEmailAlreadySent As Boolean, Optional ByVal blnPatientPortalRegistrationEmail As Boolean = False, Optional ByVal nPRid As Int64 = 0, Optional ByVal MessageName As String = "") As String
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim sMessageId As String = String.Empty
        Try
            oDBLayer.Connect(False)
            Dim _value As Object = Nothing
            oDBParameters.Clear()
            oDBParameters.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@MessageName", MessageName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachineID", strClientMachineID, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachinename", strClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nOtherID", OtherID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@sField1", " ", ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sServiceName", "APIAccess", ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nPRid", nPRid, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nMessageId", 0, ParameterDirection.Output, SqlDbType.BigInt)
            oDBLayer.Execute("API_Gl_InsertMessageQueue", oDBParameters, _value)
            sMessageId = Convert.ToString(_value)
            oDBLayer.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDBLayer IsNot Nothing Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try
        Return sMessageId
    End Function

    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim _MachineName As String = System.Windows.Forms.SystemInformation.ComputerName
    Dim ClientMachineID As String = ""
    Dim strPatientPortalEmailService As String = ""
    Dim strPatientPortalSiteNm As String = ""
    Dim _ClinicID As Int64 = 1
    Dim strPatientPortalINTUITFEATURE As String = ""
    Dim strPatientPortalEnabled As String = ""
    Private _dtLinkDate As DateTime
    Public Property dtLinkDate() As DateTime
        Get
            Return _dtLinkDate
        End Get
        Set(ByVal value As DateTime)
            _dtLinkDate = value
        End Set
    End Property
    Private _nPatientID As Int64
    Public Property nPatientID() As Int64
        Get
            Return _nPatientID
        End Get
        Set(ByVal value As Int64)
            _nPatientID = value
        End Set
    End Property

    Private _ConnectionString As String
    Public Property ConnectionString() As String
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
        End Set
    End Property

    Private _bIsSendEmail As Boolean = True
    Public Property bIsSendEmail() As Boolean
        Get
            Return _bIsSendEmail
        End Get
        Set(ByVal value As Boolean)
            _bIsSendEmail = value
        End Set
    End Property


    Public Function SendPortalEmails(ByVal ExternalType As String, ByVal blnPatientPortalSendActivationEmail As Boolean, ByVal blnPatientPortalActivationEmailAlreadySent As Boolean, Optional ByVal blnPatientPortalRegistrationEmail As Boolean = False) As Boolean
        Dim IsMailSend As Boolean = False
        Dim sMessageID As String = String.Empty
        Dim nUserId As Long = 0
        If appSettings("UserID") <> Nothing Then
            If appSettings("UserID") <> "" Then
                nUserId = Convert.ToInt64(appSettings("UserID"))
            End If
        End If
        IsClientAccess(_MachineName)
        getPatientPortalSettings()

        Dim sPortalGUID As String = InsertInExternalCode("PatientPortal", dtLinkDate)

        sMessageID = InsertInMessageQueue(nUserId, ClientMachineID, _MachineName, blnPatientPortalSendActivationEmail, blnPatientPortalActivationEmailAlreadySent, LoginUserID:=nUserId)

        If _bIsSendEmail Then
            IsMailSend = GetMessageQueue(sPortalGUID, dtLinkDate, sMessageID)
        End If

        sMessageID = Nothing
        sPortalGUID = Nothing

        Return IsMailSend
    End Function

    Public Function IsClientAccess(ByVal strClientMachineName As String) As String

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand

        Try
            objCon.ConnectionString = ConnectionString
            objCmd.CommandType = CommandType.StoredProcedure

            'Aniket Renamed gsp_CheckClientMachinePermission to sp_CheckClientMachinePermission as it is necessary for backward compatibility in multiple databases
            objCmd.CommandText = "sp_CheckClientMachinePermission"
            objCmd.Connection = objCon

            Dim objParaClientMachineName As New SqlParameter
            With objParaClientMachineName
                .ParameterName = "@MachineName"
                .Value = System.Windows.Forms.SystemInformation.ComputerName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClientMachineName)
            ''Sandip Darade 20091113
            Dim objParaProductCode As New SqlParameter
            With objParaProductCode
                .ParameterName = "@sProductCode"
                .Value = "1"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProductCode)

            ClientMachineID = 0
            objCon.Open()
            ClientMachineID = objCmd.ExecuteScalar
            objCon.Close()
            If IsNothing(ClientMachineID) Then
                ClientMachineID = 0
            End If

            objParaClientMachineName = Nothing
            objParaProductCode = Nothing


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try

        If ClientMachineID = 0 Then
            Return ClientMachineID
        Else
            Return ClientMachineID
        End If
    End Function

    Private Sub getPatientPortalSettings()

        Dim _dt As DataTable = Nothing
        Try
            _dt = GetSetting("PatientPortalEmailService")
            If (IsNothing(_dt) = False) Then
                If _dt.Rows.Count > 0 Then
                    strPatientPortalEmailService = _dt.Rows(0)("sSettingsValue").ToString()
                End If
                _dt.Dispose()
                _dt = Nothing
            End If
           

            _dt = GetSetting("PatientPortalSiteName")
            If (IsNothing(_dt) = False) Then
                If _dt.Rows.Count > 0 Then
                    strPatientPortalSiteNm = _dt.Rows(0)("sSettingsValue").ToString()
                End If
                _dt.Dispose()
                _dt = Nothing
            End If
          
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(_dt) Then
                _dt.Dispose()
                _dt = Nothing
            End If
        End Try
        
    End Sub

    Public Function GetSetting(ByVal SettingName As String) As DataTable
        Dim objCon As SqlConnection = New SqlConnection()
        Dim objCmd As SqlCommand = New SqlCommand()
        Dim dtTable As New DataTable

        Try
            objCon.ConnectionString = ConnectionString
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" + SettingName + "'  AND nClinicID = " + _ClinicID.ToString() + ""
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dtTable)
            objCon.Close()

            objDA.Dispose()
            objDA = Nothing
            Return dtTable
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            'If IsNothing(dtTable) Then
            '    dtTable.Dispose()
            '    dtTable = Nothing
            'End If
        End Try
    End Function

    Private Function GetMessageQueue(ByVal sPortalGUID As String, ByVal dtLinkDateTime As Date, ByVal sMessageID As String) As Boolean
        Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oParamater As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters()

        Dim oDataTable As DataTable = Nothing

        Dim nMessageID As Int64 = 0
        Dim str As String = ""
        Dim strUpdateExternalcode As String = ""
        Dim sEmail As String = ""
        Dim sMessageName As String = ""
        Dim bIsPR As Boolean = False
        Dim sZip As String = ""
        Dim IsEmailSend As Boolean = False

        If Not IsNothing(oDB) Then
            Try
                If oDB.Connect(False) Then

                    'oDB.Retrive_Query("SELECT gl_MessageQueue.nMessageID,gl_MessageQueue.npatientID,isnull(patient.sEmail,'') sEmail,isnull(patient.sZip,'') sZip   FROM gl_MessageQueue inner join patient on gl_MessageQueue.npatientID = patient.npatientID WHERE (smessagename='PATIENTINVITATION' or smessagename='PATIENTINVITATION - Resent') and sservicename = 'PatientPortal' and (nStatus = 1) and gl_MessageQueue.npatientid = " + nPatientID.ToString(), oDataTable)
                    Dim strSelect As String = String.Format("SELECT gl_MessageQueue.nMessageID,gl_MessageQueue.npatientID,isnull(patient.sEmail,'') sEmail,isnull(patient.sZip,'') sZip , Gl_Messagequeue.sMessageName, CASE WHEN ISNULL(nPatientPortalAccessID,0) = 0 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END bIsPR  FROM gl_MessageQueue inner join patient on gl_MessageQueue.npatientID = patient.npatientID WHERE (smessagename='PATIENTINVITATION' or smessagename='PATIENTINVITATION - Resent') and sservicename = 'PatientPortal' and (nStatus = 1) and gl_MessageQueue.npatientid = {0} and gl_MessageQueue.nMessageId={1}", nPatientID.ToString(), sMessageID)

                    oDB.Retrive_Query(strSelect, oDataTable)
                    strSelect = Nothing

                    If Not IsNothing(oDataTable) Then
                        For i = 0 To oDataTable.Rows.Count - 1
                            nMessageID = Convert.ToInt64(oDataTable.Rows(i)("nMessageID"))
                            sEmail = oDataTable.Rows(i)("sEmail").ToString().Trim()
                            sZip = oDataTable.Rows(i)("sZip").ToString().Trim()

                            sMessageName = oDataTable.Rows(i)("sMessageName").ToString().Trim()
                            bIsPR = CType(oDataTable.Rows(i)("bIsPR"), Boolean)

                            If (String.IsNullOrEmpty(sEmail) Or String.IsNullOrEmpty(sZip)) Then
                                str = " update gl_MessageQueue set nstatus = 2  WHERE nMessageID=  " + nMessageID.ToString()
                                strUpdateExternalcode = String.Format(" UPDATE PatientExternalCodes SET nExternalStatus='{0}' , dtAccessDate= '{1}' WHERE nPatientId= {2} AND sExternalType = 'PatientPortal' AND nExternalStatus<> 0", 2, dtLinkDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"), nPatientID)
                            Else
                                Dim clsgloPatientPortalEmail As clsgloPatientPortalEmail = New clsgloPatientPortalEmail(ConnectionString)

                                Dim strServiceURI As String = strPatientPortalEmailService

                                If clsgloPatientPortalEmail.CreateMail(sEmail, nPatientID, sMessageName, strServiceURI, strPatientPortalSiteNm + "/PatientActivation.html", _ClinicID, sPortalGUID, dtLinkDateTime.ToString(), bIsPR) Then
                                    str = " update gl_MessageQueue set nstatus = 0,dtEmailSent = '" + dtLinkDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff") + "'  WHERE nMessageID=  " + nMessageID.ToString()
                                    strUpdateExternalcode = String.Format(" UPDATE PatientExternalCodes SET nExternalStatus='{0}', dtAccessDate= '{1}' WHERE nPatientId= {2} AND sExternalType = 'PatientPortal' AND nExternalStatus<> 0", 0, dtLinkDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"), nPatientID)
                                    IsEmailSend = True
                                Else
                                    str = " update gl_MessageQueue set nstatus = 2  WHERE nMessageID=  " + nMessageID.ToString()
                                    strUpdateExternalcode = String.Format(" UPDATE PatientExternalCodes SET nExternalStatus='{0}' , dtAccessDate= '{1}' WHERE nPatientId= {2} AND sExternalType = 'PatientPortal' AND nExternalStatus<> 0", 2, dtLinkDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"), nPatientID)
                                End If

                                clsgloPatientPortalEmail = Nothing

                            End If
                            oDB.Execute_Query(str)
                            oDB.Execute_Query(strUpdateExternalcode)
                        Next
                    End If
                End If

                Catch ex As gloDatabaseLayer.DBException

                ex.ERROR_Log(ex.ToString())

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Finally
                If Not IsNothing(oDataTable) Then
                    oDataTable.Dispose()
                    oDataTable = Nothing
                End If
                If Not IsNothing(oParamater) Then
                    oParamater.Dispose()
                    oParamater = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If

                str = Nothing
                strUpdateExternalcode = Nothing
                sEmail = Nothing
                sMessageName = Nothing
                bIsPR = Nothing
                sZip = Nothing

            End Try

        End If
        Return IsEmailSend
    End Function

    Public Function IsPatientPortalEnabled() As Boolean
        Dim bIsPatientPortalEnabled As Boolean = False
        Dim _dt As DataTable = Nothing
        Try
            _dt = GetSetting("INTUIT FEATURE ENABLE SETTING")
            If (IsNothing(_dt) = False) Then
                If _dt.Rows.Count > 0 Then
                    strPatientPortalINTUITFEATURE = _dt.Rows(0)("sSettingsValue").ToString()
                End If
                _dt.Dispose()
                _dt = Nothing
            End If
         

            _dt = GetSetting("PatientPortalEnabled")
            If (IsNothing(_dt) = False) Then
                If _dt.Rows.Count > 0 Then
                    strPatientPortalEnabled = _dt.Rows(0)("sSettingsValue").ToString()
                End If
                _dt.Dispose()
                _dt = Nothing
            End If
         
            If strPatientPortalINTUITFEATURE.ToLower() = "true" And strPatientPortalEnabled.ToLower() = "true" Then
                bIsPatientPortalEnabled = True
            Else
                bIsPatientPortalEnabled = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(_dt) Then
                _dt.Dispose()
                _dt = Nothing
            End If
        End Try
        
        Return bIsPatientPortalEnabled
    End Function

End Class
