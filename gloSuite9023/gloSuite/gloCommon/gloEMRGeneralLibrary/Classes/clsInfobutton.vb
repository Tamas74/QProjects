Imports System.Net
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports gloSettings
Imports System.Xml
Imports System.Xml.Linq
Imports System.ComponentModel
Imports System.Threading
Imports System.Linq

Public Class clsInfobutton


    Private Property pageready As Boolean = False
    Private Property isDataFound As Boolean

    Public Property OpeningInfosource As Boolean = False
    Dim strLang As String
    Dim DummyBrow As WebBrowser = Nothing
    Dim CoreUrl As String = ""
    Public nPatientEducationID As Long = 0

    Private strPatientCode As String = ""
    Private strPatientLanguage As String = ""
    Private strPatientGender As String = ""
    Private strPatientAgeUnit As String = ""
    Private strPatientAgeValue As String = ""


#Region "Page Loading Functions"

    Private Sub WaitForPageLoad()
        AddHandler DummyBrow.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        While Not pageready
            Application.DoEvents()
        End While
        pageready = False
    End Sub

    Private Sub PageWaiter(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
        If DummyBrow.ReadyState = WebBrowserReadyState.Complete Then
            pageready = True
            If Not DummyBrow.DocumentText.Contains("<input type=""submit"" value=""Search"" />") Then
                isDataFound = True
            Else
                isDataFound = False
            End If
            RemoveHandler DummyBrow.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        End If
    End Sub

#End Region

    Private Function GetURL(ByVal sGender As String, ByVal sAge As String, ByVal sAgeUnit As String, ByVal Code As String, ByVal CodeSystem As String)
        Dim sURL As String = ""
        Dim strSql As String = "select sSettingsValue from Settings where sSettingsName='INFOBUTTON_URL'"
        Dim con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As New SqlCommand(strSql, con)
        Dim dbURL As String = ""

        Try
            con.Open()
            Dim da As New SqlDataAdapter(cmd)
            Dim dtSetting As New DataTable
            da.Fill(dtSetting)
            da.Dispose()
            da = Nothing

            If Not IsNothing(dtSetting) Then
                dbURL = Convert.ToString(dtSetting.Rows(0)("sSettingsValue"))
                dtSetting.Dispose()
                dtSetting = Nothing
            End If

            sURL = dbURL + "patientPerson.administrativeGenderCode.c=" + sGender + "&" +
                                "age.v.v=" + sAge + "&age.v.u=" + sAgeUnit + "&" + "mainSearchCriteria.v.c=" + Code + "&mainSearchCriteria.v.cs=" + CodeSystem + "&" +
                                 "performer.languageCode.c=en&informationRecipient.languageCode.c=" + strLang + "&knowledgeResponseType=text/XML"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
        Finally

            If Not IsNothing(con) Then  ''connection state close
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                    con.Dispose()
                End If
            End If
        End Try

        Return sURL
    End Function

    Public Sub Openinfosource(ByVal code As String, ByVal Codesystem As String, ByVal PatientLanguage As String, ByVal patId As Long, ByVal src As Integer, ByVal vId As Long, ByVal sAge As String, ByVal sAgeUnit As String, ByVal sGender As String, ByVal ProviderID As Long)
        'Getting Infobutton Url from Admin setting        
        Try

            If Not OpeningInfosource Then
                OpeningInfosource = True

                If PatientLanguage = "English" Then
                    strLang = "en"
                ElseIf PatientLanguage = "Spanish" OrElse PatientLanguage = "Spanish; Castilian" Then
                    strLang = "sp"
                Else
                    strLang = "en"
                End If

                Dim oCode As String = code
                Dim oCodeSystem As String = Codesystem
                isDataFound = False
                'If CodeSystem is NDC
                If Codesystem = "2.16.840.1.113883.6.69" Then

                    If IsNothing(DummyBrow) Then
                        DummyBrow = New WebBrowser()
                    End If

                    Dim sCode As String = ""
                    Dim rCode As String = ""
                    ' dtCode = GetRxNormCode(code, 1) 'Getting RxNorm Codes
                    Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                        Dim RxNormResponse As gloGlobal.DIB.RxnormFlagInfo = oGSHelper.GetRxNormCode(code)
                        If RxNormResponse IsNot Nothing Then
                            sCode = RxNormResponse.Code
                            RxNormResponse = Nothing
                        End If
                    End Using
                    If Not IsNothing(sCode) Then
                        rCode = Convert.ToString(sCode)
                        If rCode <> "" Then
                            code = rCode
                            'CodeSystem RXCUI
                            Codesystem = "2.16.840.1.113883.6.88"
                            DummyBrow.Navigate(Me.GetURL(sGender, sAge, sAgeUnit, code, Codesystem))
                            WaitForPageLoad()
                        End If

                        sCode = Nothing
                    End If

                    If Not isDataFound Then
                        code = oCode
                        'Switch back to NDC Code system
                        Codesystem = oCodeSystem
                        DummyBrow.Navigate(Me.GetURL(sGender, sAge, sAgeUnit, code, Codesystem)) 'Checking Original Value
                        WaitForPageLoad()

                        If Not isDataFound Then
                            'dtCode = GetRxNormCode(code, 2) 'Getting Parent NDC Codes
                            Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                                Dim RxNormResponse As gloGlobal.DIB.RxnormFlagInfo = oGSHelper.GetRxNormCode(code)
                                If RxNormResponse IsNot Nothing Then
                                    sCode = RxNormResponse.Code
                                    RxNormResponse = Nothing
                                End If

                            End Using
                            If Not IsNothing(sCode) Then

                                rCode = Convert.ToString(sCode)
                                If rCode <> "" Then
                                    code = rCode
                                    Codesystem = oCodeSystem
                                    DummyBrow.Navigate(Me.GetURL(sGender, sAge, sAgeUnit, code, Codesystem))
                                    WaitForPageLoad()
                                End If
                            End If
                        End If

                        If Not IsNothing(sCode) Then
                            sCode = Nothing

                        End If
                        rCode = Nothing
                    End If

                    If Not IsNothing(DummyBrow) Then
                        DummyBrow.Dispose()
                        DummyBrow = Nothing
                    End If

                    If Not isDataFound Then
                        code = oCode
                        Codesystem = oCodeSystem
                    End If
                End If

                Dim blnUseDefaultPrinter As Boolean
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                If IsNothing(gloRegistrySetting.GetRegistryValue("UseDefaultPrinter")) = False Then
                    If gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") = 1 Then
                        blnUseDefaultPrinter = True
                    Else
                        blnUseDefaultPrinter = False
                    End If
                Else
                    blnUseDefaultPrinter = True
                End If
                gloRegistrySetting.CloseRegistryKey()

                Dim Newurl As String = ""
                '   If Codesystem = "2.16.840.1.113883.6.1" Then
                Newurl = GetURL(Me.GetURL(sGender, sAge, sAgeUnit, code, Codesystem), strLang)

                'End If
                Dim InfoButtonForm As frmInfoButtonBrowser = frmInfoButtonBrowser.GetInstance
                With InfoButtonForm
                    .LoginProviderID = ProviderID
                    .PatientId = patId
                    .VisitID = vId
                    .Source = src
                    .EducationID = 0
                    .gblnUseDefaultPrinter = blnUseDefaultPrinter
                    .ResourceCategory = enumResourceCategory.OnlineLibrary
                    .ResourceType = enumResourceType.PatientReferenceMaterial
                    .ValidatePortalFeatures()
                    .NavigateTo(Newurl)
                    .CoreURL = CoreUrl
                    .BringToFront()
                    .Select()
                    .ShowDialog()

                End With
                OpeningInfosource = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
        End Try
    End Sub

    Public Function Geturl(ByVal strUrl As String, ByVal _PatientLanguage As String) As String

        Dim oclient As New WebClient
        Dim urlstring As String = ""
        
        Dim FinalUrl As String = strUrl
        Dim informationStringen As String = ""
        Dim informationStringSP As String = ""
        Dim NotHavingStringen As String = ""
        Dim NotHavingStringsp As String = ""
        Dim FootterString As String = ""
        Dim tochkTitle As String = ""
        Dim informationString As String = ""
        Dim NotHavingString As String = ""

        'below Added for Bug #93626: 00001080: Problem List info button
        ' and new settings value inserted in database setting table
        Dim infoLogoStringEN As String = ""
        Dim infoSingleResultStringEN As String = ""
        Dim infoMultiResultStringEN As String = ""
        Dim infoLogoStringSP As String = ""
        Dim infoSingleResultStringSP As String = ""
        Dim infoMultiResultStringSP As String = ""
        Try
            urlstring = oclient.DownloadString(strUrl)
            Dim dtSettings As DataTable = GetInfobuttonSettings()
            If Not IsNothing(dtSettings) Then
                If dtSettings.Rows.Count > 0 Then
                    For i As Integer = 0 To dtSettings.Rows.Count - 1 Step 1
                        Select Case Convert.ToString(dtSettings.Rows(i)("sSettingsName"))
                            Case "Infobutton_Information_StringEN"
                                informationStringen = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_Information_StringSP"
                                informationStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_NotHaving_StringEN"
                                NotHavingStringen = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_NotHaving_StringSP"
                                NotHavingStringsp = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_FooterString"
                                FootterString = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_tochkTitle"
                                tochkTitle = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))

                                'below Added for Bug #93626: 00001080: Problem List info button
                            Case "INFOBUTTON_LOGOTEXT_EN"
                                infoLogoStringEN = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_LOGOTEXT_SP"
                                infoLogoStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultSingle_EN"
                                infoSingleResultStringEN = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultSingle_SP"
                                infoSingleResultStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultMulti_EN"
                                infoMultiResultStringEN = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultMulti_SP"
                                infoMultiResultStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                        End Select
                    Next
                End If
                dtSettings.Dispose()
                dtSettings = Nothing
            End If

            ' Added by hemant for Bug #93626: 00001080: Problem List info button
            Dim startSearchString As String = ""
            Dim endSearchString As String = ""
            If _PatientLanguage = "sp" Then
                informationString = informationStringSP
                NotHavingString = NotHavingStringsp
                startSearchString = infoLogoStringSP
                endSearchString = infoMultiResultStringSP
            Else
                informationString = informationStringen
                NotHavingString = NotHavingStringen
                startSearchString = infoLogoStringEN
                endSearchString = infoMultiResultStringEN
            End If

            'if setting does not exist then continue to execute as per old implementation
            If Not (String.IsNullOrEmpty(startSearchString) OrElse String.IsNullOrEmpty(endSearchString)) Then
                Dim startSearchIndex As Integer = urlstring.IndexOf(startSearchString)
                If startSearchIndex <> -1 Then
                    Dim startSearchIndexFinal As Integer = startSearchIndex + startSearchString.Length
                    ' if single results found singular and plural for end serach string
                    Dim endSearchStringIndex As Integer = urlstring.IndexOf(endSearchString)
                    If endSearchStringIndex = -1 Then
                        If _PatientLanguage = "en" Then
                            endSearchString = infoSingleResultStringEN
                        ElseIf _PatientLanguage = "sp" Then
                            endSearchString = infoSingleResultStringSP
                        End If
                        endSearchStringIndex = urlstring.IndexOf(endSearchString)
                    End If
                    If endSearchStringIndex <> -1 AndAlso endSearchStringIndex > startSearchIndexFinal AndAlso startSearchIndexFinal >= 0 Then
                        Dim strBetween As String = urlstring.Substring(startSearchIndexFinal, endSearchStringIndex - startSearchIndexFinal + endSearchString.Length)
                        If (String.IsNullOrEmpty(strBetween) = False) Then
                            Dim strFinalBetween As String = strBetween.Substring(strBetween.LastIndexOf(">") + 1)

                            If Not String.IsNullOrEmpty(strFinalBetween) Then
                                Dim strArr() As String
                                strArr = strFinalBetween.Trim().Split()
                                Dim cnt As Integer
                                If strArr.Length >= 3 Then
                                    If Integer.TryParse(strArr(0), cnt) Then
                                        If cnt = 0 OrElse cnt > 1 Then
                                            Return FinalUrl
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            '   Added by hemant for Bug #93626: 00001080: Problem List info button Ends here 
            Dim myInformationValue As Integer = urlstring.IndexOf(informationString)
            Dim mySubstring As String = urlstring
            If (myInformationValue = -1) Then
                If (urlstring.IndexOf(NotHavingString) > -1) Then
                    Return FinalUrl
                End If
            End If
            If (myInformationValue > -1) Then
                Dim totIndex As Integer = myInformationValue + Len(informationString)
                If (totIndex >= 0) Then
                    mySubstring = urlstring.Substring(totIndex)
                End If
            End If
            '  Dim myArticleValue As Integer = mySubstring.IndexOf(toFind)
            Dim myTitleValue As Integer = mySubstring.IndexOf(tochkTitle)
            '  Dim myFooterValue As Integer = mySubstring.IndexOf(FootterString)
            Dim resultstr As String = ""
            If myTitleValue > -1 Then
                resultstr = mySubstring.Substring(myTitleValue)
                Dim myhtm As Integer = resultstr.IndexOf(".htm")
                Dim myLess As Integer = resultstr.IndexOf("<")
                Do
                    If myLess < myhtm Then
                        Dim lenChkTile As String = Len(tochkTitle)
                        If (tochkTitle >= 0) Then
                            mySubstring = resultstr.Substring(lenChkTile)
                            If (String.IsNullOrEmpty(mySubstring) = False) Then
                                myTitleValue = mySubstring.IndexOf(tochkTitle)
                                If (myTitleValue >= 0) Then
                                    resultstr = mySubstring.Substring(myTitleValue)
                                    myhtm = resultstr.IndexOf(".htm")
                                    myLess = resultstr.IndexOf("<")
                                Else
                                    Exit Do
                                End If
                            Else
                                Exit Do
                            End If
                        Else
                            Exit Do
                        End If
                    Else
                        Exit Do
                    End If
                Loop

                myhtm = resultstr.IndexOf(".htm""")
                Dim totalLength As Integer = 0
                If myhtm = -1 OrElse myhtm > 200 Then
                    myhtm = resultstr.IndexOf(".html")
                    totalLength = 5
                Else

                    totalLength = 4
                End If
                If myhtm <> -1 Then
                    FinalUrl = resultstr.Substring(0, myhtm + totalLength)
                End If
            End If
        Catch ex As WebException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message + "INFOBUTTON GET URL Inner Execption : " + Convert.ToString(ex.InnerException), False)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message, False)
            Return Nothing
        Finally
            If Not IsNothing(oclient) Then
                oclient.Dispose()
                oclient = Nothing
            End If
            informationStringen = String.Empty
            informationStringSP = String.Empty
            NotHavingStringen = String.Empty
            NotHavingStringsp = String.Empty
            FootterString = String.Empty
            tochkTitle = String.Empty
            informationString = String.Empty
            NotHavingString = String.Empty

            infoLogoStringEN = String.Empty
            infoSingleResultStringEN = String.Empty
            infoMultiResultStringEN = String.Empty
            infoLogoStringSP = String.Empty
            infoSingleResultStringSP = String.Empty
            infoMultiResultStringSP = String.Empty

        End Try

        Return FinalUrl
    End Function

    Public Function GetUserRightsForEducationMaterial(ByVal userid As Long) As ArrayList

        Dim objCon As New SqlConnection
        Dim arrUserRights As New ArrayList()
        objCon.ConnectionString = gloEMRDatabase.DataBaseLayer.ConnectionString
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        Dim objParaUserId As New SqlParameter
        Dim objParaApplicationType As New SqlParameter
        '28-Apr-14 Aniket: Try catch added as it was giving design time exception on the Orders screen.
        Try



            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveUserRights"




            With objParaUserId
                .ParameterName = "@UserID"
                .Value = userid
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaUserId)

            With objParaApplicationType
                .ParameterName = "@ApplicationType"
                .Value = 0
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaApplicationType)


            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader()

            Dim arrLst As New ArrayList
            While objSQLDataReader.Read
                If IsNothing(objSQLDataReader.Item(0)) = False Then
                    arrLst.Add(Trim(objSQLDataReader.Item(0)))
                End If
            End While
            objSQLDataReader.Close()
            objCon.Close()
            objSQLDataReader.Dispose()
            objSQLDataReader = Nothing
            objCon.Dispose()
            objCon = Nothing
            ''
            Dim isRights As Boolean = False
            If arrLst.Contains("Education Material") = True Then
                arrUserRights.Add(True)
            Else
                arrUserRights.Add(False)
            End If

            If arrLst.Contains("Education Material - Advanced Provider Reference") = True Then
                arrUserRights.Add(True)
            Else
                arrUserRights.Add(False)
            End If
            arrLst.Clear()

            Return arrUserRights

        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            If Not IsNothing(objParaUserId) Then
                objParaUserId = Nothing
            End If
            If Not IsNothing(objParaApplicationType) Then
                objParaApplicationType = Nothing
            End If
        End Try

    End Function

    Public Function GetEducationMaterial(ByVal nCode As String, ByVal nCodesystem As String, ByVal nAge As Decimal, ByVal sGender As String, Optional ByVal nICDRevision As Integer = 9) As DataTable

        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dt As New DataTable()

        Try

            '' Insert Or Update problem List
            cmd = New SqlCommand("gsp_GetEducationMaterial", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@Code", SqlDbType.VarChar, 100)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nCode

            sqlParam = cmd.Parameters.Add("@CodeSystem", SqlDbType.VarChar, 255)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nCodesystem

            sqlParam = cmd.Parameters.Add("@Age", SqlDbType.Decimal)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nAge

            sqlParam = cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sGender


            sqlParam = cmd.Parameters.Add("@nICDRevision", SqlDbType.Int) '' added for ICD 10 implementation
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nICDRevision


            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)

            'Con.Close()
            Return dt

        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Public Function GetInfobuttonSettings() As DataTable
        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As SqlCommand = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dtBibliographicinfo As New DataTable()
        Try
            cmd = New SqlCommand("SELECT nSettingsID,sSettingsName,sSettingsValue  FROM Settings WHERE sSettingsName like '%Infobutton%'", Con)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dtBibliographicinfo)
            Return dtBibliographicinfo
        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Public Function GetLastVisitedDocument(ByVal _patientiD As Long, ByVal TemplateId As Long) As DataTable
        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dt As New DataTable()

        Try


            '' Insert Or Update problem List
            cmd = New SqlCommand("gsp_GetLastVisitedDocument", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@PatientId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _patientiD

            sqlParam = cmd.Parameters.Add("@TemplateId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TemplateId

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)

            Con.Close()
            Return dt

        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Public Function GetEducationTemplate(ByVal patientID As Long, ByVal templateID As Long, ByVal Source As Integer, ByVal ResourceCategory As Integer, ByVal ResourceType As Integer, ByVal TemplateName As String) As DataTable
        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As SqlCommand = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dtExamEducation As New DataTable()
        Dim dtTemplateMst As New DataTable()
        Try
            Dim strSQl = "select sPENotes as sDescription from ExamEducation where nPatientID=" & patientID & " and nSource=" & Source & "and nResourceCategory=" & ResourceCategory & " and nResourceType=" & ResourceType & "and sTemplateName =" & TemplateName & ""

            cmd = New SqlCommand("select sPENotes as sDescription,sbiblographicinfo as bibliography,sBibliographicDeveloper as sbDeveloper   from ExamEducation where nPatientID=" & patientID & " and nSource=" & Source & "and nResourceCategory=" & ResourceCategory & " and nResourceType=" & ResourceType & "and sTemplateName ='" & TemplateName & "'", Con)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dtExamEducation)
            sqladpt.Dispose()
            sqladpt = Nothing

            If Not IsNothing(dtExamEducation) Then
                If dtExamEducation.Rows.Count <= 0 Then
                    dtExamEducation.Dispose()
                    dtExamEducation = Nothing
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = New SqlCommand("select sDescription,sBibliographicinfo as bibliography ,sBibliographicDeveloper as sbDeveloper from TemplateGallery_MST where nTemplateID =" & templateID & "", Con)
                    sqladpt = New SqlDataAdapter
                    sqladpt.SelectCommand = cmd
                    sqladpt.Fill(dtTemplateMst)
                    Con.Close()
                    Return dtTemplateMst
                Else
                    Con.Close()
                    If (IsNothing(dtTemplateMst) = False) Then
                        dtTemplateMst.Dispose()
                        dtTemplateMst = Nothing
                    End If

                    Return dtExamEducation
                End If
            Else
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = New SqlCommand("select sDescription,sBibliographicinfo as bibliography ,sBibliographicDeveloper as sbDeveloper from TemplateGallery_MST where nTemplateID =" & templateID & "", Con)
                sqladpt = New SqlDataAdapter
                sqladpt.SelectCommand = cmd
                sqladpt.Fill(dtTemplateMst)
                Con.Close()
                Return dtTemplateMst
            End If
            Return Nothing

        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try

    End Function

    Public Function GeteducationTemplate(ByVal templateID As Long) As DataTable
        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As SqlCommand = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dt As New DataTable()
        Try
            cmd = New SqlCommand("select nTemplateID,sTemplateName,sDescription  from TemplateGallery_MST where nTemplateID =" & templateID & "", Con)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            Con.Close()
            Return dt
        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Public Function GetPatientInfo(ByVal patientid As Int64) As DataTable

        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dt As New DataTable()

        Try
            '' Insert Or Update problem List
            cmd = New SqlCommand("gsp_PatientInfo", Con)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = patientid
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            Con.Close()
            Return dt
        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Public Function SavePatientEducation(ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal nExamID As Int64, ByVal oTemplateResult As Object, ByVal sTemplateName As String, Optional ByVal Source As Integer = 0, Optional ByVal Resourcecategory As Integer = 1, Optional ByVal ResourceType As Integer = 0, Optional ByVal DocumentURL As String = "", Optional ByVal EducationId As Long = 0, Optional ProviderID As Long = 0) As Boolean

        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        Dim Result As Integer = 0

        Try
            '' Insert Or Update problem List
            cmd = New SqlCommand("gsp_InUpExamEducation", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nVisitID

            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientID

            sqlParam = cmd.Parameters.Add("@ExamID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nExamID

            sqlParam = cmd.Parameters.Add("@TemplateName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sTemplateName

            sqlParam = cmd.Parameters.Add("@sPENotes", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(oTemplateResult) Then
                sqlParam.Value = oTemplateResult
            Else
                sqlParam.Value = DBNull.Value
            End If


            sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@Source", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Source

            sqlParam = cmd.Parameters.Add("@ResourceCategory", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Resourcecategory

            sqlParam = cmd.Parameters.Add("@ResourceType", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ResourceType

            sqlParam = cmd.Parameters.Add("@DocumentURL", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DocumentURL

            sqlParam = cmd.Parameters.Add("@Bibliography", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@BibliographyDeveloper", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@EducationID", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = EducationId

            sqlParam = cmd.Parameters.Add("@ProviderID", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ProviderID

            sqlParam = cmd.Parameters.Add("@nPatientEducationID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Output




            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            Result = cmd.ExecuteNonQuery()
            nPatientEducationID = cmd.Parameters("@nPatientEducationID").Value
            Con.Close()
            If Result > 0 Then
                If Source = 1 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Online Education Document Added", nPatientID, nPatientEducationID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf Source = 2 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Online Education Document Added", nPatientID, nPatientEducationID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf Source = 3 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Online Education Document Added", nPatientID, nPatientEducationID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
                Return True
            Else
                Return False
            End If

        Catch ex As SqlException
            Return False
        Catch ex As Exception
            Return False
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try



    End Function

    Public Function GetBibliographicinfo(ByVal nTemplateID As Long) As DataTable

        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As SqlCommand = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dtBibliographicinfo As New DataTable()

        Try
            cmd = New SqlCommand("select sBibliographicinfo from TemplateGallery_MST where nTemplateID=" & nTemplateID & "", Con)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dtBibliographicinfo)
            Return dtBibliographicinfo
        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    'Public Function getICDTemplates(ByVal nIcdcode As String, ByVal nAge As Integer, ByVal sGender As String) As DataTable
    '    Dim oDB As New DataBaseLayer
    '    Dim strSQL As String
    '    Dim oResult As DataTable
    '    Try

    '        strSQL = "select Education_ICD9.nTemplateId,sTemplateName,Education_ICD9.nResourceType from Education_ICD9 inner join " +
    '                 "TemplateGallery_MST on Education_ICD9.nTemplateId=TemplateGallery_MST.nTemplateID  where Education_ICD9.sICD9Code=" + nIcdcode + " " +
    '                 "and Education_ICD9.nAgeMin <=" & nAge & " and Education_ICD9.nAgeMax>=" & nAge & " and Education_ICD9.sGender='" & sGender & "'"

    '        oResult = oDB.GetDataTable_Query(strSQL)
    '        If Not oResult Is Nothing Then
    '            Return oResult
    '        Else
    '            Return Nothing
    '        End If

    '    Catch ex As Exception
    '        Return Nothing
    '    Finally
    '        oDB.Dispose()
    '        oDB = Nothing
    '        If Not IsNothing(oResult) Then
    '            oResult.Dispose()
    '            oResult = Nothing
    '        End If
    '    End Try
    'End Function


    'Public Function getSnomedctTemplates(ByVal nSnomedctcode As String, ByVal nAge As Integer, ByVal sGender As String) As DataTable
    '    Dim oDB As New DataBaseLayer
    '    Dim strSQL As String
    '    Dim oResult As DataTable
    '    Try

    '        strSQL = "select Education_ICD9.nTemplateId,sTemplateName,Education_ICD9.nResourceType from Education_ICD9 inner join " +
    '                 "TemplateGallery_MST on Education_ICD9.nTemplateId=TemplateGallery_MST.nTemplateID  where sConceptID=" + nSnomedctcode + " " +
    '                 "and Education_ICD9.nAgeMin <=" & nAge & " and Education_ICD9.nAgeMax>=" & nAge & " and Education_ICD9.sGender='" & sGender & "'"
    '        oResult = oDB.GetDataTable_Query(strSQL)
    '        If Not oResult Is Nothing Then
    '            Return oResult
    '        Else
    '            Return Nothing
    '        End If

    '    Catch ex As Exception
    '        Return Nothing
    '    Finally
    '        oDB.Dispose()
    '        oDB = Nothing
    '        If Not IsNothing(oResult) Then
    '            oResult.Dispose()
    '            oResult = Nothing
    '        End If
    '    End Try
    'End Function

    Public Enum MedReconciliationenumSource
        None = 0
        ProblemList = 1
        Medication = 2
        History = 3
        Orders = 4

    End Enum

    Public Enum enumSource
        None = 0
        ProblemList = 1
        Medication = 2
        Orders = 3
        EncounterDiagnosis = 4
        EncounterTreatment = 5
    End Enum

    Public Enum enumResourceCategory
        InternalLibrary = 1
        OnlineLibrary = 2
    End Enum

    Public Enum enumResourceType
        PatientEducation = 0
        PatientReferenceMaterial = 1
        ProviderReferenceMaterial = 2
        General = 3
    End Enum

#Region "Open Infobutton"

    Public Function GetAgeGroup(ByVal ageValue As Integer, ByVal ageUnit As String) As String
        Dim AgeGroup As String = ""

        Select Case ageUnit
            Case "a"
                Select Case ageValue
                    Case 1
                        AgeGroup = "D007223" '"infant" '(1-23 months)
                    Case 2 To 5
                        AgeGroup = "D002675" ' '"child,preschool" '(2-5 Years)
                    Case 6 To 12
                        AgeGroup = "D002648" '"child" '(6-12 Years)
                    Case 13 To 18
                        AgeGroup = "D000293" '"adolescent" '(13-18 Years)
                    Case 19 To 24
                        AgeGroup = "D055815" '"young adult" '(19-24 Years)
                    Case 19 To 44
                        AgeGroup = "D000328" '"adult" '(19-44 Years)
                    Case 45 To 64
                        AgeGroup = "D008875" '"middle aged" '(45-64 Years)
                    Case 56 To 79
                        AgeGroup = "D000368" '"aged" '(56-79 Years)
                    Case Else
                        AgeGroup = "D000369" '"aged,80 and older" '(>=80 Years)
                End Select
            Case "mo"
                AgeGroup = "D007223" '"infant" '(1-23 months)
            Case "d"
                AgeGroup = "D007231" ' "infant,newborn" '(birth to 1 month)
        End Select

        Return AgeGroup

    End Function

    Public Sub GetEducationMaterial_OpenInfobutton(ByVal IncludeGender As Boolean,
                                                   ByVal Gender As String,
                                                   ByVal IncludeAge As Boolean,
                                                   ByVal AgeUnit As String,
                                                   ByVal AgeValue As String,
                                                   ByVal Language As String,
                                                   ByVal Code As String,
                                                   ByVal CodeSystem As String,
                                                   ByVal CodeDescription As String,
                                                   ByVal Audience As String,
                                                   ByVal ProviderId As Long,
                                                   ByVal PatientId As Long,
                                                   ByVal VisitId As Long,
                                                   ByVal CallingForm As Form)

        Dim Source As Integer
        Dim TaskContext As String = ""
        Dim ParameterString As String = ""
        Dim _ISSmonedCodeMandatory As Boolean = False
        Dim blnUseDefaultPrinter As Boolean
        Dim dtEducation As DataTable
        Dim strNDCCode As String = ""
        Dim RxNormResponse As gloGlobal.DIB.RxnormFlagInfo = Nothing
        Dim AgeGroup As String = ""

        Try

            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            If IsNothing(gloRegistrySetting.GetRegistryValue("UseDefaultPrinter")) = False Then
                If gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") = 1 Then
                    blnUseDefaultPrinter = True
                Else
                    blnUseDefaultPrinter = False
                End If
            Else
                blnUseDefaultPrinter = True
            End If
            gloRegistrySetting.CloseRegistryKey()

            _ISSmonedCodeMandatory = IsSnomedMandatory()

            'Check settings to Include Age and Gender,if Calling form is other than Recommendation
            If CallingForm.Name <> "frmDM_Infobutton" Then
                Dim objSettings As New gloSettings.GeneralSettings(gloEMRDatabase.DataBaseLayer.ConnectionString)
                Dim dtIncludeAge As DataTable = objSettings.GetSetting("IncludeAgeForOpenInfobuttonResources")
                Dim dtIncludeGender As DataTable = objSettings.GetSetting("IncludeGenderForOpenInfobuttonResources")

                If Not IsNothing(dtIncludeAge) Then
                    If dtIncludeAge.Rows.Count > 0 Then
                        IncludeAge = Convert.ToByte(dtIncludeAge.Rows(0)("sSettingsValue"))
                    End If
                End If

                If Not IsNothing(dtIncludeGender) Then
                    If dtIncludeGender.Rows.Count > 0 Then
                        IncludeGender = Convert.ToByte(dtIncludeGender.Rows(0)("sSettingsValue"))
                    End If
                End If

                If Not IsNothing(dtIncludeAge) Then
                    dtIncludeAge.Dispose()
                    dtIncludeAge = Nothing
                End If
                If Not IsNothing(dtIncludeGender) Then
                    dtIncludeGender.Dispose()
                    dtIncludeGender = Nothing
                End If
                If Not IsNothing(objSettings) Then
                    objSettings.Dispose()
                    objSettings = Nothing
                End If
            End If

            Get_PatientDetails(PatientId)

            If Language = "" Then
                Language = strPatientLanguage
            End If

            'Language = GetLanguageCode(Language)

            If IncludeAge = True Then
                If AgeValue = "" OrElse AgeUnit = "" Then
                    AgeValue = strPatientAgeValue
                    AgeUnit = strPatientAgeUnit
                End If
                AgeGroup = GetAgeGroup(Convert.ToInt32(AgeValue), AgeUnit)
            End If

            If Audience = "Provider" Then
                Audience = "PROV"
            Else
                Audience = "PAT"
            End If

            If Gender.ToUpper() = "Male".ToUpper() Then
                Gender = "M"
            ElseIf Gender.ToUpper() = "Female".ToUpper() Then
                Gender = "F"
            ElseIf Gender.ToUpper() = "Other".ToUpper() Then
                Gender = "UN"
            ElseIf Gender.ToUpper() = "Unknown".ToUpper() Then
                Gender = "UNK"
            End If

            If Language = "English" Then
                Language = "en"
            ElseIf Language = "Spanish" OrElse Language = "Spanish; Castilian" Then
                Language = "sp"
            Else
                Language = "en"
            End If

            'For Medication always find resources using RXNORM CODE only
            If CodeSystem = "2.16.840.1.113883.6.69" Then
                Dim NDCCode = Code
                Try
                    Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                        RxNormResponse = oGSHelper.GetRxNormCode(Code)
                        If RxNormResponse IsNot Nothing Then
                            Code = RxNormResponse.Code
                            CodeSystem = "2.16.840.1.113883.6.88" 'RxNorm Code System
                            RxNormResponse = Nothing
                        End If
                    End Using
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Infobutton::Get RxNorm Code:" + ex.ToString(), False)
                    CodeSystem = "2.16.840.1.113883.6.69"
                    Code = NDCCode
                End Try
            End If

            If CodeSystem = "2.16.840.1.113883.6.96" Or
               CodeSystem = "2.16.840.1.113883.6.90" Or
               CodeSystem = "2.16.840.1.113883.6.103" Then       'SnomedCT/ICD10/ICD9
                TaskContext = "PROBLISTREV"
                Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
            ElseIf CodeSystem = "2.16.840.1.113883.6.69" Or
                   CodeSystem = "2.16.840.1.113883.6.88" Then   'NDC/RxNorm
                TaskContext = "MLREV"
                Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
            ElseIf CodeSystem = "2.16.840.1.113883.6.1" Then    'Loinc
                TaskContext = "LABRREV"
                Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
            End If

            ParameterString = "taskContext.c.c=" + TaskContext +
                                          "&mainSearchCriteria.v.c=" + Code +
                                          "&mainSearchCriteria.v.cs=" + CodeSystem + ""
            If CodeDescription <> "" Then
                ParameterString = ParameterString + "&mainSearchCriteria.v.dn=" + CodeDescription + ""
            End If


            If IncludeGender Then
                ParameterString = ParameterString + "&patientPerson.administrativeGenderCode.c=" + Gender + "" '&patientPerson.administrativeGenderCode.cs=2.16.840.1.113883.5.1"
            End If
            If IncludeAge Then
                ParameterString = ParameterString + "&age.v.v=" + AgeValue +
                                                    "&age.v.u=" + AgeUnit + ""
                If AgeGroup <> "" Then
                    ParameterString = ParameterString + "&ageGroup.v.c=" + AgeGroup + "&ageGroup.v.cs=2.16.840.1.113883.6.177"
                End If
            End If
            ParameterString = ParameterString + "&informationRecipient.languageCode.c=" + Language + ""
            ParameterString = ParameterString + "&informationRecipient=" + Audience + ""
            ParameterString = ParameterString + "&performer=PROV"
            ParameterString = ParameterString + "&knowledgeResponseType=text/xml"

            dtEducation = OpenInfobuttonLinks(ParameterString)

            If IsNothing(dtEducation) Then
                dtEducation = New DataTable()
                Dim colSource As New DataColumn("Source")
                Dim colTitle As New DataColumn("Title")
                Dim colLink As New DataColumn("Link")
                dtEducation.Columns.Add(colSource)
                dtEducation.Columns.Add(colTitle)
                dtEducation.Columns.Add(colLink)

                Dim MedlineURL As String = GetURL(Gender, AgeValue, AgeUnit, Code, CodeSystem, Language)
                Dim NewMedlineURL As String = GetURL(MedlineURL, Language)
                Dim dRow As DataRow = dtEducation.NewRow()
                dRow("Source") = "Medline Plus"
                dRow("Title") = CodeDescription
                dRow("Link") = NewMedlineURL
                dtEducation.Rows.Add(dRow)
                CoreUrl = NewMedlineURL
                MedlineURL = String.Empty
                NewMedlineURL = String.Empty
                dRow = Nothing
                colSource = Nothing
                colTitle = Nothing
                colLink = Nothing
            ElseIf dtEducation.Rows.Count <= 0 Then
                Dim MedlineURL As String = GetURL(Gender, AgeValue, AgeUnit, Code, CodeSystem, Language)
                Dim NewMedlineURL As String = GetURL(MedlineURL, Language)
                Dim dRow As DataRow = dtEducation.NewRow()
                dRow("Source") = "Medline Plus"
                dRow("Title") = CodeDescription
                dRow("Link") = NewMedlineURL
                dtEducation.Rows.Add(dRow)
                CoreUrl = NewMedlineURL
                MedlineURL = String.Empty
                NewMedlineURL = String.Empty
                dRow = Nothing
            ElseIf dtEducation.Rows.Count > 0 Then
                Try
                    Dim count As Integer = (From row In dtEducation.Rows Where row("Source") = "MedlinePlus").Count()
                    If count = 0 Then
                        Dim MedlineURL As String = GetURL(Gender, AgeValue, AgeUnit, Code, CodeSystem, Language)
                        Dim NewMedlineURL As String = GetURL(MedlineURL, Language)
                        Dim dRow As DataRow = dtEducation.NewRow()
                        dRow("Source") = "Medline Plus"
                        dRow("Title") = CodeDescription
                        dRow("Link") = NewMedlineURL
                        dtEducation.Rows.InsertAt(dRow, 0)
                        CoreUrl = NewMedlineURL
                        MedlineURL = String.Empty
                        NewMedlineURL = String.Empty
                        dRow = Nothing
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
                End Try
            End If


            Dim InfoButtonForm As frmInfoButtonBrowser = frmInfoButtonBrowser.GetInstance
            With InfoButtonForm
                .LoginProviderID = ProviderId
                .PatientId = PatientId
                .VisitID = VisitId
                .Source = Source
                .EducationID = 0
                .gblnUseDefaultPrinter = blnUseDefaultPrinter
                .ResourceCategory = enumResourceCategory.OnlineLibrary
                .ResourceType = enumResourceType.PatientReferenceMaterial
                .ValidatePortalFeatures()
                .dtLinks = dtEducation
                .CoreURL = CoreUrl
                .BringToFront()
                .Select()
                .ShowDialog()
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
        Finally

            Audience = Nothing
            TaskContext = Nothing
            ParameterString = Nothing
        End Try

    End Sub

    Public Function OpenInfobuttonLinks(ByVal ParameterString As String) As DataTable
        Dim arrLinks As New ArrayList()
        Dim serviceURL As String = ""
        Dim blnUseDefaultPrinter As Boolean
        Dim sURL As String = ""
        Dim Newurl As String = ""
        Dim strPath As String = ""


        Dim strSql As String = "select sSettingsValue from Settings where sSettingsName='OPENINFOBUTTON_URL'"
        Dim con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As New SqlCommand(strSql, con)
        Dim da As New SqlDataAdapter(cmd)
        Dim dtSetting As New DataTable
        Dim xDoc As New XmlDocument()


        Try
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            If IsNothing(gloRegistrySetting.GetRegistryValue("UseDefaultPrinter")) = False Then
                If gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") = 1 Then
                    blnUseDefaultPrinter = True
                Else
                    blnUseDefaultPrinter = False
                End If
            Else
                blnUseDefaultPrinter = True
            End If
            gloRegistrySetting.CloseRegistryKey()

            con.Open()
            da.Fill(dtSetting)
            da.Dispose()
            da = Nothing

            If Not IsNothing(dtSetting) Then
                serviceURL = Convert.ToString(dtSetting.Rows(0)("sSettingsValue"))
                dtSetting.Dispose()
                dtSetting = Nothing
            End If

            Newurl = serviceURL + ParameterString

            CoreUrl = Newurl

            Dim oclient As New WebClient
            xDoc.LoadXml(oclient.DownloadString(Newurl))
            Dim dtEducation As New DataTable
            Dim colSource As New DataColumn("Source")
            Dim colTitle As New DataColumn("Title")
            Dim colLink As New DataColumn("Link")

            dtEducation.Columns.Add(colSource)
            dtEducation.Columns.Add(colTitle)
            dtEducation.Columns.Add(colLink)


            Dim xnl As XmlNodeList = xDoc.GetElementsByTagName("entry")
            Dim xnlFeed As XmlNodeList = xDoc.GetElementsByTagName("feed")
            Dim strSource As String = ""
            Dim strTitle As String = ""
            Dim strSubTitle As String = ""
            Dim strLink As String = ""

            For Each node As XmlNode In xnlFeed
                For Each childNode As XmlNode In node.ChildNodes
                    If childNode.Name = "title" Then
                        strSource = childNode.InnerText
                    ElseIf childNode.Name = "entry" Then
                        xnl = childNode.ChildNodes
                        For Each ccNode As XmlNode In xnl
                            If ccNode.Name = "link" Then
                                strLink = ccNode.Attributes("href").Value

                                Dim dRow As DataRow = dtEducation.NewRow()
                                dRow("Source") = strSource
                                dRow("Title") = strTitle
                                'dRow("SubTitle") = strSubTitle
                                dRow("Link") = strLink
                                dtEducation.Rows.Add(dRow)

                            ElseIf ccNode.Name = "title" Then
                                strTitle = Convert.ToString(ccNode.InnerText)
                            ElseIf ccNode.Name = "subtitle" Then
                                strSubTitle = Convert.ToString(ccNode.InnerText)
                            End If

                        Next
                    End If
                Next
            Next

            oclient.Dispose()
            oclient = Nothing
            Return dtEducation
        Catch ex As WebException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message + "OPENINFOBUTTON URL Inner Execption : " + Convert.ToString(ex.InnerException), False)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message, False)
            Return Nothing
        Finally


            sURL = Nothing
            strSql = Nothing
            Newurl = Nothing
            strPath = Nothing

            If Not IsNothing(xDoc) Then
                xDoc = Nothing
            End If

            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If

            If Not IsNothing(dtSetting) Then
                dtSetting.Dispose()
                dtSetting = Nothing
            End If
        End Try

    End Function

    Private Function GetURL(ByVal sGender As String, ByVal sAge As String, ByVal sAgeUnit As String, ByVal Code As String, ByVal CodeSystem As String, ByVal strLang As String)
        Dim sURL As String = ""
        Dim strSql As String = "select sSettingsValue from Settings where sSettingsName='INFOBUTTON_URL'"
        Dim con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As New SqlCommand(strSql, con)
        Dim dbURL As String = ""

        Try
            con.Open()
            Dim da As New SqlDataAdapter(cmd)
            Dim dtSetting As New DataTable
            da.Fill(dtSetting)
            da.Dispose()
            da = Nothing

            If Not IsNothing(dtSetting) Then
                dbURL = Convert.ToString(dtSetting.Rows(0)("sSettingsValue"))
                dtSetting.Dispose()
                dtSetting = Nothing
            End If

            sURL = dbURL + "patientPerson.administrativeGenderCode.c=" + sGender + "&" +
                                "age.v.v=" + sAge + "&age.v.u=" + sAgeUnit + "&" + "mainSearchCriteria.v.c=" + Code + "&mainSearchCriteria.v.cs=" + CodeSystem + "&" +
                                 "performer.languageCode.c=en&informationRecipient.languageCode.c=" + strLang + "&knowledgeResponseType=text/XML"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
        Finally
            If Not IsNothing(con) Then
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                    con.Dispose()
                End If
            End If
        End Try

        Return sURL
    End Function

    Public Function GetLanguageCode(ByVal language As String) As String
        Dim cmd As SqlCommand = Nothing
        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim languageCode As String = ""
        Try
            Dim strQry As String = "Select sCode from Category_mst where sCategoryType='Language' and sDescription='" + language + "'"

            cmd = New SqlCommand(strQry, Con)
            Con.Open()
            languageCode = cmd.ExecuteScalar()
            Con.Close()
            Return languageCode
        Catch ex As Exception
            Return Nothing
        Finally
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
    End Function

    Public Function IsSnomedMandatory() As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim _IsSnomedMandatory As Boolean = False
        Try
            Dim strQry As String = "SELECT ISNULL(sSettingsValue,0) as IsSnomedCTMandatory FROM dbo.Settings WHERE sSettingsName ='REQUIRESNOMEDCT'"

            cmd = New SqlCommand(strQry, Con)
            Con.Open()
            _IsSnomedMandatory = cmd.ExecuteScalar()
            Con.Close()
            Return _IsSnomedMandatory
        Catch ex As Exception
            Return Nothing
        Finally
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
    End Function

    Private Sub Get_PatientDetails(ByVal PatientID As Long)
        Dim dtPatient As DataTable = Nothing
        Try
            dtPatient = GetPatientInfo(PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientLanguage = Convert.ToString(dtPatient.Rows(0)("sLang"))
                    GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If
        End Try
    End Sub

    Public Sub GetAge(ByVal BirthDate As DateTime)

        Dim _BDate As DateTime = BirthDate
        ' Compute the difference between BirthDate 'CODE FROM gloPM
        'year and end year. 
        Dim IsBirthDateLeap As Boolean = False
        Dim years As Integer = Now.Year - BirthDate.Year
        Dim months As Integer = 0
        Dim days As Integer = 0
        'Test if BirthDay for LeapYear.
        If BirthDate.Day = 29 AndAlso BirthDate.Month = 2 Then
            IsBirthDateLeap = True
        End If
        ' Check if the last year was a full year. 
        If Now < BirthDate.AddYears(years) AndAlso years <> 0 Then
            years -= 1
        End If
        BirthDate = BirthDate.AddYears(years)
        ' Now we know BirthDate <= end and the diff between them 
        ' is < 1 year. 
        If BirthDate.Year = Now.Year Then
            months = Now.Month - BirthDate.Month
        Else
            months = (12 - BirthDate.Month) + Now.Month
        End If
        ' Check if the last month was a full month. 
        If Now < BirthDate.AddMonths(months) AndAlso months <> 0 Then
            months -= 1
        End If
        BirthDate = BirthDate.AddMonths(months)
        ' Now we know that BirthDate < end and is within 1 month 
        ' of each other. 
        days = (Now - BirthDate).Days

        'To Adjust Age if BirthDate is 29th Feb in leap year
        If IsBirthDateLeap = True Then   ''Sequence of following IF code is too important.. DON'T MODIFY
            days -= 1
            If Now.Day = 29 AndAlso Now.Month = 2 Then
                days += 1
            ElseIf Now.Year Mod 4 = 0 Then
                days += 1
            End If
            If days < 0 AndAlso Now.Year Mod 4 <> 0 Then
                days = 30
                months = months - 1
                If months < 0 Then
                    months = 11
                    years = years - 1
                End If
            End If
            If months = 12 Then
                days = 30
                months = 11
            End If
        End If

        If years <> 0 Then
            strPatientAgeUnit = "a"
            strPatientAgeValue = Convert.ToString(years)
        ElseIf months <> 0 Then
            strPatientAgeUnit = "mo"
            strPatientAgeValue = Convert.ToString(months)
        ElseIf days <> 0 Then
            strPatientAgeUnit = "d"
            strPatientAgeValue = Convert.ToString(days)
        End If
    End Sub

    Public Sub DownloadFile(uri As Uri, desintaion As String)
        Using wc = New WebClient()
            Dim syncObj = New [Object]()
            SyncLock syncObj
                wc.DownloadFileAsync(uri, desintaion, syncObj)
                'This would block the thread until download completes
                Monitor.Wait(syncObj, 5000)
            End SyncLock
        End Using

    End Sub

#End Region

End Class

