Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows
Imports System.Windows.Forms
Imports System.ServiceModel


Public Class clsgloPatientPortalEmail
    Dim dbConnectionstring As String = ""
    Dim gstrMessageBoxCaption As String = ""
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

    Dim PatientEmailId As String = ""
    Dim nClinicID As Long = 0
    Public gblnIntuitCommunication As Boolean = False
    Public gblnUSEINTUITINTERFACE As Boolean = False
    Public gblnPatientPortalEnabled As Boolean = False
    Dim nClientMachineID As Integer

    Public strPatientPortalEmailService As String = ""
    Public strPatientPortalSiteNm As String = ""
    Public strPatientPortalINTUITFEATURE As String = ""
    Public strPatientPortalEnabled As String = ""
    Public _ClinicID As Int64 = 1

    Public Sub New(ByVal Connectionstring As String)
        dbConnectionstring = Connectionstring
        If appSettings("MessageBOXCaption") IsNot Nothing Then
            If appSettings("MessageBOXCaption") <> "" Then
                gstrMessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            End If
        End If
    End Sub

    Public Function CreateMail(ToEmailAddress As String, nPatientID As Long, sTemplateName As String, sServiceUri As String, sPatientPortalSiteNm As String, gnClinicID As Long, sPortalGUID As String, sLinkDate As String, bIsPR As Boolean) As Boolean
        Dim _IsMailSend As Boolean = False
        Dim dtEmailTemp As DataTable = Nothing
        Dim strMsgBody As String = String.Empty
        ''http://localhost/gloEmail/EmailService.svc
        If sServiceUri = String.Empty Then
            MessageBox.Show("Unable to send Email." + Environment.NewLine + "Patient Portal Email service is not configured in" + Environment.NewLine + " gloEMR Admin -> Settings -> Interface Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return _IsMailSend
            Exit Function
        End If

        Dim ogloEmailService As gloPatientPortalCommon.gloPatientPortalEmailService.ServiceEmailClient = ServiceClass.GetWCFSvc(sServiceUri)

        Dim strKey As String = String.Empty
        If appSettings("sourcekey") IsNot Nothing Then
            If appSettings("sourcekey") <> "" Then
                strKey = Convert.ToString(appSettings("sourcekey")).Trim()
            End If
        End If

        Dim ogloSecurity As New gloSecurity.ClsEncryption
        Dim _sDecryptKey As String = "12345678"
        Try
            'added for newly added line in email template for patient activation only.
            Dim sLoginURL As String = ""
            sLoginURL = sPatientPortalSiteNm.Substring(0, sPatientPortalSiteNm.LastIndexOf("/"))
            'sLoginURL = sLoginURL + "/index.html"

            dtEmailTemp = GetPatientPortalEmailTemplate(nPatientID, sTemplateName, bIsPR)
            If dtEmailTemp IsNot Nothing AndAlso dtEmailTemp.Rows.Count > 0 Then

                If sTemplateName.ToLower() = "patientinvitation".ToLower() Or sTemplateName.ToLower() = "patientinvitation - resent".ToLower() Then
                    Dim strEncrypted As String = String.Empty
                    Dim objrc4 As New RC4("test")
                    Dim strg As String = Convert.ToString(sPortalGUID).ToLower()

                    'strEncrypted = strg & "," & Convert.ToString(nPatientID) & "," & System.DateTime.Now.ToString("yyyy MM dd") & "," & Convert.ToString(dtEmailTemp.Rows(0)("sExternalcode"))
                    strEncrypted = strg & "," & Convert.ToString(nPatientID) & "," & Convert.ToDateTime(sLinkDate).ToString("yyyy MM dd") & "," & Convert.ToString(dtEmailTemp.Rows(0)("sExternalcode"))
                    objrc4.Password = "test"
                    objrc4.Text = strEncrypted
                    strEncrypted = RC4.StrToHexStr(objrc4.EnDeCrypt())
                    sPatientPortalSiteNm += "?Id=" + strEncrypted
                    objrc4 = Nothing
                    strEncrypted = Nothing
                    strg = Nothing
                End If

                Dim sPassword As String
                sPassword = ogloSecurity.DecryptFromBase64String(Convert.ToString(dtEmailTemp.Rows(0)("sPassword")), _sDecryptKey)

                strMsgBody = dtEmailTemp.Rows(0)("sMsgBody").ToString()
                strMsgBody = strMsgBody.Replace("{#ClinicName#}", Convert.ToString(dtEmailTemp.Rows(0)("sClinicName")))
                strMsgBody = strMsgBody.Replace("{#PatientName#}", Convert.ToString(dtEmailTemp.Rows(0)("sPatientName")))

                strMsgBody = strMsgBody.Replace("{#Link#}", sPatientPortalSiteNm)
                strMsgBody = strMsgBody.Replace("{#PhoneNo#}", Convert.ToString(dtEmailTemp.Rows(0)("sPhoneNo")))
                strMsgBody = strMsgBody.Replace("{#EmailId#}", Convert.ToString(dtEmailTemp.Rows(0)("sEmail")))
                strMsgBody = strMsgBody.Replace("{#UserId#}", "UserId")
                strMsgBody = strMsgBody.Replace("{#Link_Login#}", sLoginURL)
                strMsgBody = strMsgBody.Replace("{#UserName#}", Convert.ToString(dtEmailTemp.Rows(0)("sUserName")))
                strMsgBody = strMsgBody.Replace("{#TempPassword#}", sPassword)
                '' api portal email combine
                strMsgBody = strMsgBody.Replace("{#APIDocURL#}", Convert.ToString(dtEmailTemp.Rows(0)("APIDocURL")))

                _IsMailSend = ogloEmailService.SendEmail(ToEmailAddress, Convert.ToString(dtEmailTemp.Rows(0)("sSubject")).Replace("{#ClinicName#}", Convert.ToString(dtEmailTemp.Rows(0)("sClinicName"))), strMsgBody, Convert.ToString(dtEmailTemp.Rows(0)("sExternalcode")), Convert.ToString(dtEmailTemp.Rows(0)("sClinicName")), Convert.ToString(dtEmailTemp.Rows(0)("sClinicName")).Trim() + strKey)

                sPassword = Nothing


            End If
            sLoginURL = Nothing

        Catch generatedExceptionName As Exception
            _IsMailSend = False
        Finally
            If (IsNothing(dtEmailTemp) = False) Then
                dtEmailTemp.Dispose()
                dtEmailTemp = Nothing

            End If
            ogloEmailService.Close()
            ogloEmailService = Nothing

            If (IsNothing(ogloSecurity) = False) Then
                ogloSecurity.Dispose()
                ogloSecurity = Nothing
            End If
            strMsgBody = Nothing
            strKey = Nothing
            _sDecryptKey = Nothing
        End Try
        Return _IsMailSend
    End Function
    Public Function CreateMail_PR(ToEmailAddress As String, nPatientID As Long, sTemplateName As String, sServiceUri As String, sPatientPortalSiteNm As String, gnClinicID As Long, sPortalGUID As String, sLinkDate As String, nPRId As Long, bIsPR As Boolean) As Boolean
        Dim _IsMailSend As Boolean = False
        Dim dtEmailTemp As DataTable = Nothing
        Dim strMsgBody As String = String.Empty
        ''http://localhost/gloEmail/EmailService.svc
        If sServiceUri = String.Empty Then
            MessageBox.Show("Unable to send Email." + Environment.NewLine + "Patient Portal Email service is not configured in" + Environment.NewLine + " gloEMR Admin -> Settings -> Interface Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return _IsMailSend
            Exit Function
        End If

        Dim ogloEmailService As gloPatientPortalCommon.gloPatientPortalEmailService.ServiceEmailClient = ServiceClass.GetWCFSvc(sServiceUri)

        Dim strKey As String = String.Empty
        If appSettings("sourcekey") IsNot Nothing Then
            If appSettings("sourcekey") <> "" Then
                strKey = Convert.ToString(appSettings("sourcekey")).Trim()
            End If
        End If

        Dim ogloSecurity As New gloSecurity.ClsEncryption
        Dim _sDecryptKey As String = "12345678"
        Try
            'added for newly added line in email template for patient activation only.
            Dim sLoginURL As String = ""
            sLoginURL = sPatientPortalSiteNm.Substring(0, sPatientPortalSiteNm.LastIndexOf("/"))
            'sLoginURL = sLoginURL + "/index.html"

            dtEmailTemp = GetPatientPortalEmailTemplate(nPatientID, sTemplateName, bIsPR)
            If dtEmailTemp IsNot Nothing AndAlso dtEmailTemp.Rows.Count > 0 Then
                If _dataset IsNot Nothing AndAlso _dataset.Tables(0).Rows.Count > 0 Then
                    Dim i As Integer = 0
                    For i = 0 To _dataset.Tables(0).Rows.Count - 1
                        If (_dataset.Tables(0).Rows(i)("isPR").ToString() = "1") Then



                            Dim sPassword As String
                            sPassword = ogloSecurity.DecryptFromBase64String(Convert.ToString(dtEmailTemp.Rows(0)("sPassword")), _sDecryptKey)

                            strMsgBody = dtEmailTemp.Rows(0)("sMsgBody").ToString()
                            strMsgBody = strMsgBody.Replace("{#ClinicName#}", Convert.ToString(dtEmailTemp.Rows(0)("sClinicName")))
                            strMsgBody = strMsgBody.Replace("{#PatientName#}", Convert.ToString(dtEmailTemp.Rows(0)("sPatientName")))
                            strMsgBody = strMsgBody.Replace("{#PatientRepresentativeName#}", Convert.ToString(_dataset.Tables(0).Rows(i)("Name")))
                            strMsgBody = strMsgBody.Replace("{#Link#}", sPatientPortalSiteNm)
                            strMsgBody = strMsgBody.Replace("{#PhoneNo#}", Convert.ToString(dtEmailTemp.Rows(0)("sPhoneNo")))
                            strMsgBody = strMsgBody.Replace("{#EmailId#}", Convert.ToString(dtEmailTemp.Rows(0)("sEmail")))
                            strMsgBody = strMsgBody.Replace("{#UserId#}", "UserId")
                            strMsgBody = strMsgBody.Replace("{#Link_Login#}", sLoginURL)
                            strMsgBody = strMsgBody.Replace("{#UserName#}", Convert.ToString(dtEmailTemp.Rows(0)("sUserName")))
                            strMsgBody = strMsgBody.Replace("{#TempPassword#}", sPassword)

                            Dim strsubject As String
                            strsubject = Convert.ToString(dtEmailTemp.Rows(0)("sSubject").Replace("{#ClinicName#}", Convert.ToString(dtEmailTemp.Rows(0)("sClinicName"))))
                            strsubject = strsubject.Replace("{#PatientName#}", Convert.ToString(dtEmailTemp.Rows(0)("sPatientName")))

                            _IsMailSend = ogloEmailService.SendEmail(Convert.ToString(_dataset.Tables(0).Rows(i)("sEmail")), strsubject, strMsgBody, Convert.ToString(dtEmailTemp.Rows(0)("sExternalcode")), Convert.ToString(dtEmailTemp.Rows(0)("sClinicName")), Convert.ToString(dtEmailTemp.Rows(0)("sClinicName")).Trim() + strKey)

                            strsubject = Nothing
                            sPassword = Nothing

                        End If
                    Next
                End If
            End If
            sLoginURL = Nothing
        Catch generatedExceptionName As Exception
            _IsMailSend = False
        Finally
            If (IsNothing(dtEmailTemp) = False) Then
                dtEmailTemp.Dispose()
                dtEmailTemp = Nothing

            End If
            ogloEmailService.Close()
            ogloEmailService = Nothing

            If (IsNothing(ogloSecurity) = False) Then
                ogloSecurity.Dispose()
                ogloSecurity = Nothing
            End If
            strMsgBody = Nothing
            strKey = Nothing
            _sDecryptKey = Nothing
        End Try
        Return _IsMailSend
    End Function
    Public Function SendEmail(nPatientID As Long, strServiceURI As String, strPatientPortalSiteNm As String, nClinicID As Long, Optional ByVal sTemplateName As String = "Portal Secure Message", Optional ByVal IsPR As Boolean = False) As Boolean
        GetPatientDetails(nPatientID)
        strPatientPortalSiteNm = strPatientPortalSiteNm + "/index.html"
        If CreateMail(PatientEmailId, nPatientID, sTemplateName, strServiceURI, strPatientPortalSiteNm, nClinicID, "", "", IsPR) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SendEmail_PR(nPatientID As Long, strServiceURI As String, strPatientPortalSiteNm As String, nClinicID As Long, Optional ByVal sTemplateName As String = "Portal Secure Message", Optional nPRId As Long = 0, Optional ByVal IsPR As Boolean = False) As Boolean
        GetPatientDetails_PR(nPatientID, nPRId)
        strPatientPortalSiteNm = strPatientPortalSiteNm + "/index.html"
        If CreateMail_PR(PatientEmailId, nPatientID, sTemplateName, strServiceURI, strPatientPortalSiteNm, nClinicID, "", "", nPRId, IsPR) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub GetPatientDetails(ByVal nPatientID As Long)
        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim _dataAdapter As SqlDataAdapter = Nothing
        Dim _dataset As DataSet = Nothing

        Try
            Con = New SqlConnection(dbConnectionstring)
            cmd = New SqlCommand("select isnull(semail,'') sEmail from patient where npatientid = " + nPatientID.ToString, Con)
            cmd.CommandType = CommandType.Text

            sqlParam = New SqlParameter


            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientID


            _dataAdapter = New SqlDataAdapter(cmd)
            _dataset = New DataSet()

            _dataAdapter.Fill(_dataset)

            If _dataset.Tables(0) IsNot Nothing Then
                If _dataset.Tables(0).Rows.Count > 0 Then
                    PatientEmailId = _dataset.Tables(0)(0)("sEmail").ToString

                End If
            End If



        Catch ex As SqlException
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)


        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)


        Finally

            If Not IsNothing(_dataset) Then
                _dataset.Dispose()
                _dataset = Nothing
            End If

            If Not IsNothing(_dataAdapter) Then
                _dataAdapter.Dispose()
                _dataAdapter = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If



            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Sub
    Dim _dataset As DataSet = Nothing
    Private Sub GetPatientDetails_PR(ByVal nPatientID As Long, Optional ByVal nPRId As Long = 0)
        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim _dataAdapter As SqlDataAdapter = Nothing

        Try
            Con = New SqlConnection(dbConnectionstring)
            cmd = New SqlCommand("GetPortalEmailToDetails", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = New SqlParameter


            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientID

            sqlParam = cmd.Parameters.Add("@nPRId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPRId


            _dataAdapter = New SqlDataAdapter(cmd)
            _dataset = New DataSet()

            _dataAdapter.Fill(_dataset)

            If _dataset.Tables(0) IsNot Nothing Then
                If _dataset.Tables(0).Rows.Count > 0 Then
                    PatientEmailId = _dataset.Tables(0)(0)("sEmail").ToString

                End If
            End If



        Catch ex As SqlException
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)


        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)


        Finally



            If Not IsNothing(_dataAdapter) Then
                _dataAdapter.Dispose()
                _dataAdapter = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If



            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Sub
    Private Function GetPatientPortalEmailTemplate(ByVal nPatientID As Long, ByVal sTemplateName As String, ByVal IsPR As Boolean) As DataTable
        Dim Con As SqlConnection = Nothing
        Dim dt As DataTable = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim _dataAdapter As SqlDataAdapter = Nothing
        Dim _dataset As DataSet = Nothing

        Try
            Con = New SqlConnection(dbConnectionstring)
            cmd = New SqlCommand("WS_GetPortalEmailTemplatesByName", Con)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = New SqlParameter


            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientID

            sqlParam = cmd.Parameters.Add("@sTemplateName", SqlDbType.NVarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sTemplateName

            sqlParam = cmd.Parameters.Add("@bIsPR", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = IsPR

            _dataAdapter = New SqlDataAdapter(cmd)
            _dataset = New DataSet()

            _dataAdapter.Fill(_dataset)

            If _dataset.Tables(0) IsNot Nothing Then
                dt = _dataset.Tables(0).Copy()
            End If

            Return dt

        Catch ex As SqlException
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            If Not IsNothing(_dataset) Then
                _dataset.Dispose()
                _dataset = Nothing
            End If

            If Not IsNothing(_dataAdapter) Then
                _dataAdapter.Dispose()
                _dataAdapter = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If

            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Function

    Public Sub GetSettings()
        Dim dtTable As New DataTable
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = dbConnectionstring
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetSettingsForPortal"
            objCmd.Connection = objCon

            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dtTable)
            objCon.Close()

            objCon.Dispose() : objCon = Nothing

            objCmd.Parameters.Clear()
            objCmd.Dispose() : objCmd = Nothing

            objDA.Dispose() : objDA = Nothing

            Dim nCount As Integer
            For nCount = 0 To dtTable.Rows.Count - 1

                Select Case dtTable.Rows(nCount).Item(1).ToString.ToUpper
                    'MU2 Features Setting


                    'Case Added For Checking Intuit Communication
                    Case "INTUIT FEATURE ENABLE SETTING".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIntuitCommunication = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        End If


                        ''Added for MU2 Patient portal implementation on 20130702
                    Case "USEINTUITINTERFACE".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnUSEINTUITINTERFACE = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnUSEINTUITINTERFACE = False
                        End If
                    Case "PatientPortalEnabled".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnPatientPortalEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnPatientPortalEnabled = False
                        End If

                End Select
            Next


        Catch ex As Exception
            MessageBox.Show("Unable to get all settings. Please do the settings through gloEMR-Admin ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            If IsNothing(dtTable) = False Then
                dtTable.Dispose() : dtTable = Nothing
            End If
        End Try
    End Sub


    Public Function IsPatientRegisteredOnPortal(ByVal nPatientId As Long, ByVal ShowMessage As String) As Boolean

        Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
        Dim _MessageBoxCaption As String = String.Empty
        Dim UserName As String = String.Empty

        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR"
            End If
        End If

        If appSettings("UserName") IsNot Nothing Then

            If appSettings("UserName") <> "" Then

                UserName = Convert.ToString(appSettings("UserName"))
            Else
                UserName = ""
            End If
        End If

        appSettings = Nothing

        GetSettings()
        IsClientAccess()
        If gblnUSEINTUITINTERFACE = True And gblnIntuitCommunication = True Then
            Dim clsIntuit As New gloPatientPortalCommon.ClsIntuitSecureMessage(dbConnectionstring, nPatientId, nClientMachineID, System.Windows.Forms.SystemInformation.ComputerName, UserName)
            Dim PatientStatus As Int16
            PatientStatus = clsIntuit.CheckValidUser(Convert.ToBoolean(ShowMessage))
            If PatientStatus > 0 Then
                clsIntuit.Dispose()
                Return True
            Else
                clsIntuit.Dispose()

            End If
        ElseIf gblnPatientPortalEnabled = True And gblnIntuitCommunication = True Then
            Dim dtPortalValidUser As DataTable = ToCheckPatientRegisterOrNotOnPortal(nPatientId)
            If dtPortalValidUser IsNot Nothing AndAlso dtPortalValidUser.Rows.Count > 0 Then
                dtPortalValidUser.Dispose()
                dtPortalValidUser = Nothing

                Return True
            Else
                If dtPortalValidUser IsNot Nothing Then
                    dtPortalValidUser.Dispose()
                    dtPortalValidUser = Nothing
                End If
                If Convert.ToBoolean(ShowMessage) = True Then
                    MessageBox.Show("Patient does not have an active portal account." + System.Environment.NewLine + "No portal message can be sent.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return False

            End If
        Else
            Return False
        End If
        _MessageBoxCaption = Nothing
        UserName = Nothing

        Return False



    End Function


    Public Function IsClientAccess() As Boolean

        Dim objCon As New SqlConnection
        objCon.ConnectionString = dbConnectionstring
        Dim objCmd As New SqlCommand
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

        nClientMachineID = 0
        objCon.Open()
        nClientMachineID = objCmd.ExecuteScalar
        objCon.Close()
        If IsNothing(nClientMachineID) Then
            nClientMachineID = 0
        End If
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaClientMachineName = Nothing
        objParaProductCode = Nothing
        objCon.Dispose()
        objCon = Nothing
        If nClientMachineID = 0 Then
            Return False
        Else
            Return True
        End If
    End Function



    Public Function ToCheckPatientRegisterOrNotOnPortal(ByVal PatID As Long) As DataTable
        Dim Con As SqlConnection = Nothing
        Dim dt As DataTable = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim _dataAdapter As SqlDataAdapter = Nothing
        Dim _dataset As DataSet = Nothing

        Try
            Con = New SqlConnection(dbConnectionstring)
            cmd = New SqlCommand("WS_CheckPatientRegisterOrNotOnPortal", Con)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = New SqlParameter


            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatID

            _dataAdapter = New SqlDataAdapter(cmd)
            _dataset = New DataSet()

            _dataAdapter.Fill(_dataset)

            If _dataset.Tables(0) IsNot Nothing Then
                dt = _dataset.Tables(0).Copy()
            End If

            Return dt



        Catch ex As SqlException
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            If Not IsNothing(_dataset) Then
                _dataset.Dispose()
                _dataset = Nothing
            End If

            If Not IsNothing(_dataAdapter) Then
                _dataAdapter.Dispose()
                _dataAdapter = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If

            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try


    End Function

    Public Function GetPatRepresentativeList(ByVal PatientID As Long) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(dbConnectionstring)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim ds As New DataSet

        Try
            oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Connect(False)
            oDB.Retrive("WS_GetPatRepresentativeByPatID", oDBParameters, ds)

            If IsNothing(ds) = False AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(ds) Then
                ds.Dispose() : ds = Nothing
            End If
        End Try

    End Function

    Public Function SendPortalEmail(ByVal _nPatientId As Int64, ByVal strServiceURI As String, ByVal gstrPatientPortalSiteNm As String, ByVal gnClinicID As Int64, ByVal MessageType As String, Optional ByVal SendEmailInstantly As Boolean = False) As Boolean
        Dim oclsMessageQueue As New ClsMessageQueue(dbConnectionstring, DateTime.Now, _nPatientId)
        Dim nUserId As Long = 0
        Dim sMessageID As String = ""
        Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
        Dim _MachineName As String = System.Windows.Forms.SystemInformation.ComputerName
        Dim ClientMachineID As String = ""
        Dim IsMailSend As Boolean = False
        Try

            ClientMachineID = oclsMessageQueue.IsClientAccess(_MachineName)

            If appSettings("UserID") <> Nothing Then
                If appSettings("UserID") <> "" Then
                    nUserId = Convert.ToInt64(appSettings("UserID"))
                End If
            End If

            sMessageID = oclsMessageQueue.InsertInMessageQueue(nUserId, ClientMachineID, _MachineName, False, False, False, 0, MessageType)
            If SendEmailInstantly = True Then

                If Convert.ToUInt64(sMessageID) > 0 Then
                    IsMailSend = SendEmail(_nPatientId, strServiceURI, gstrPatientPortalSiteNm, gnClinicID, MessageType, False)
                    Dim oDB As New gloDatabaseLayer.DBLayer(dbConnectionstring)
                    If oDB IsNot Nothing Then
                        If oDB.Connect(False) Then
                            Try
                                Dim str As String = ""
                                If IsMailSend Then
                                    str = " update gl_MessageQueue set nstatus = 0,dtEmailSent = '" + DateTime.Now.ToString() + "'  WHERE nMessageID=  " + sMessageID.ToString()
                                Else
                                    str = " update gl_MessageQueue set nstatus = 2  WHERE nMessageID=  " + sMessageID.ToString()
                                End If
                                oDB.Execute_Query(str)
                            Catch ex As gloDatabaseLayer.DBException
                                ex.ERROR_Log(ex.ToString())
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                            Finally
                                If oDB IsNot Nothing Then
                                    oDB.Disconnect()
                                    oDB.Dispose()
                                    oDB = Nothing
                                End If
                            End Try
                        End If
                    End If
                End If
            End If

            Dim dtPR As DataTable = GetPatRepresentativeList(_nPatientId)
            If dtPR IsNot Nothing Then

                For Each dr As DataRow In dtPR.Rows
                    sMessageID = ""
                    IsMailSend = False

                    sMessageID = oclsMessageQueue.InsertInMessageQueue(nUserId, ClientMachineID, _MachineName, False, False, False, dr("PRID"), MessageType)

                    If SendEmailInstantly = True Then
                        If Convert.ToUInt64(sMessageID) > 0 Then
                            IsMailSend = SendEmail_PR(_nPatientId, strServiceURI, gstrPatientPortalSiteNm, gnClinicID, MessageType, dr("PRID"), True)
                            Dim oDB As New gloDatabaseLayer.DBLayer(dbConnectionstring)
                            If oDB IsNot Nothing Then
                                If oDB.Connect(False) Then
                                    Dim str As String = ""
                                    Try
                                        If IsMailSend Then
                                            str = " update gl_MessageQueue set nstatus = 0,dtEmailSent = '" + DateTime.Now.ToString() + "'  WHERE nMessageID=  " + sMessageID.ToString()
                                        Else
                                            str = " update gl_MessageQueue set nstatus = 2  WHERE nMessageID=  " + sMessageID.ToString()
                                        End If
                                        oDB.Execute_Query(str)
                                    Catch ex As gloDatabaseLayer.DBException
                                        ex.ERROR_Log(ex.ToString())
                                    Catch ex As Exception
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                                    Finally
                                        If oDB IsNot Nothing Then
                                            oDB.Disconnect()
                                            oDB.Dispose()
                                            oDB = Nothing
                                        End If
                                        str = Nothing
                                    End Try
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If Not IsNothing(dtPR) Then
                dtPR.Dispose() : dtPR = Nothing
            End If

            Return True
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oclsMessageQueue = Nothing
            sMessageID = Nothing
            appSettings = Nothing
            _MachineName = Nothing
            ClientMachineID = Nothing
        End Try
    End Function
    Public Function SendBatchPortalEmail(ByVal dtAccountId As DataTable, ByVal gnClinicID As Int64, ByVal MessageType As String) As Boolean
        Dim oclsMessageQueue As New ClsMessageQueue(dbConnectionstring, DateTime.Now)
        Dim nUserId As Long = 0

        Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
        Dim _MachineName As String = System.Windows.Forms.SystemInformation.ComputerName
        Dim ClientMachineID As String = ""
        Try
            ClientMachineID = oclsMessageQueue.IsClientAccess(_MachineName)
            If appSettings("UserID") <> Nothing Then
                If appSettings("UserID") <> "" Then
                    nUserId = Convert.ToInt64(appSettings("UserID"))
                End If
            End If
            oclsMessageQueue.InsertBatchInMessageQueue(dtAccountId, nUserId, ClientMachineID, _MachineName, False, False, False, 0, MessageType)
            Return True
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oclsMessageQueue = Nothing
            appSettings = Nothing
            _MachineName = Nothing
            ClientMachineID = Nothing
        End Try
    End Function


    Public Function GetSetting(ByVal SettingName As String) As DataTable
        Dim objCon As SqlConnection = New SqlConnection()
        Dim objCmd As SqlCommand = New SqlCommand()
        Dim dtTable As New DataTable

        Try
            objCon.ConnectionString = dbConnectionstring
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


    Public Function GetPortalStatmentNotificationStatus() As DataTable
        Dim objCon As SqlConnection = New SqlConnection()
        Dim objCmd As SqlCommand = New SqlCommand()
        Dim dtTable As New DataTable

        Try
            objCon.ConnectionString = dbConnectionstring
            objCmd.CommandText = "GetPortalStatementNotificationSetting"
            objCmd.CommandType = CommandType.StoredProcedure

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
        End Try
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

    Public Function IsNotifyLabAcknowledgement() As Boolean
        Dim bIsNotifyLabAcknowledgement As Boolean = False
        Dim _dt As DataTable = Nothing
        Dim strLabAckNotify As String = ""
        Try
            _dt = GetSetting("PATIENTPORTALLABACKNOTIFICATION")
            If (IsNothing(_dt) = False) Then
                If _dt.Rows.Count > 0 Then
                    strLabAckNotify = _dt.Rows(0)("sSettingsValue").ToString()
                End If
                _dt.Dispose()
                _dt = Nothing
            End If


            If strLabAckNotify.ToLower() = "true" Then
                bIsNotifyLabAcknowledgement = True
            Else
                bIsNotifyLabAcknowledgement = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(_dt) Then
                _dt.Dispose()
                _dt = Nothing
            End If
            strLabAckNotify = Nothing
        End Try

        Return bIsNotifyLabAcknowledgement
    End Function


    Public Sub getPatientPortalSettings()

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




    Public Class ServiceClass
        Private Sub New()
        End Sub

        Friend Shared Function GetWCFSvc(ByVal siteUrl As String) As gloPatientPortalEmailService.ServiceEmailClient
            Dim client As gloPatientPortalEmailService.ServiceEmailClient = Nothing
            Try
                Dim serviceUri As New Uri(siteUrl)
                Dim endpointAddress As New ServiceModel.EndpointAddress(serviceUri)
                'Create the binding here
                Dim binding As WSHttpBinding = BindingFactory.CreateInstance()
                client = New gloPatientPortalEmailService.ServiceEmailClient(binding, endpointAddress)
                serviceUri = Nothing
                endpointAddress = Nothing
                binding = Nothing

                Return client
            Catch ex As Exception
                Throw ex
                Return client
            End Try
        End Function
    End Class

    Friend NotInheritable Class BindingFactory
        Private Sub New()
        End Sub
        Friend Shared Function CreateInstance() As WSHttpBinding
            Dim binding As New WSHttpBinding()
            Try
                binding.Security.Mode = SecurityMode.Transport
                ''binding.ReliableSession.Enabled = False
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None
                binding.UseDefaultWebProxy = True
                binding.OpenTimeout = New TimeSpan(0, 10, 0)
                binding.CloseTimeout = New TimeSpan(0, 10, 0)
                binding.SendTimeout = New TimeSpan(0, 10, 10)
                binding.ReceiveTimeout = New TimeSpan(0, 10, 0)
                binding.MaxBufferPoolSize = 99999999999999
                binding.MaxReceivedMessageSize = 2147483647


                binding.ReaderQuotas.MaxArrayLength = 2147483647
                binding.ReaderQuotas.MaxDepth = 64
                binding.ReaderQuotas.MaxStringContentLength = 2147483647
                binding.ReaderQuotas.MaxBytesPerRead = 568556652
                binding.ReaderQuotas.MaxNameTableCharCount = 568556652
                Return binding
            Catch ex As Exception
                Throw ex
                Return binding
            Finally
                If Not IsNothing(binding) Then
                    binding = Nothing
                End If
            End Try
        End Function

    End Class
End Class
