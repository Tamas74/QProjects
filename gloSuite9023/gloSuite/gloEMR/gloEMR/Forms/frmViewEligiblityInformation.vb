Imports System.Xml

Public Class frmViewEligiblityInformation
    Private _dtEligiblityInformation As DataTable
    Private _blnShowMsgPnl As Boolean

    Public Sub New(ByVal _dt As DataTable, ByVal ShowMessagePnl As Boolean)
        _dtEligiblityInformation = _dt
        _blnShowMsgPnl = ShowMessagePnl
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "C1Column"

    Private COL_PaitentID As Integer = 0
    Private COL_MessageID As Integer = 1
    Private COL_ResquestDateTimeStamp As Integer = 2
    Private COL_PayerName As Integer = 3
    Private COL_DependentName As Integer = 4
    Private COL_DependentGender As Integer = 5
    Private COL_DependentDOB As Integer = 6
    Private COL_DependentSSN As Integer = 7
    Private COL_DependentAddress1 As Integer = 8
    Private COL_DependentAddress2 As Integer = 9
    Private COL_DependentCity As Integer = 10
    Private COL_DependentState As Integer = 11
    Private COL_DepenedentZip As Integer = 12


    Private COL_SuscriberName As Integer = 13
    Private COL_SubscriberGender As Integer = 14
    Private COL_SubscriberDOB As Integer = 15
    Private COL_SubscriberSSN As Integer = 16
    Private COL_SubscriberAddress1 As Integer = 17
    Private COL_SubscriberAddress2 As Integer = 18
    Private COL_SubscriberCity As Integer = 19
    Private COL_SubscriberState As Integer = 20
    Private COL_SubscriberZip As Integer = 21
    Private COl_Count As Integer = 22


    Private COL_ChangePatientID As Integer = 0
    Private COL_ChangeMessageID As Integer = 1
    Private COL_ChangedPayerName As Integer = 2
    Private COL_ChaneResquestDateTimeStamp As Integer = 3
    Private COL_DependentdemochgName As Integer = 4
    Private COL_DependentdemoChgGender As Integer = 5
    Private COL_DependentdemoChgDOB As Integer = 6
    Private COL_DependentdemoChgSSN As Integer = 7
    Private COL_DependentdemoChgAddress1 As Integer = 8
    Private COL_DependentdemoChgAddress2 As Integer = 9
    Private COL_DependentdemoChgCity As Integer = 10
    Private COL_DependentdemoChgState As Integer = 11
    Private COL_DependentdemoChgZip As Integer = 12


    Private COL_SubscriberChangeName As Integer = 13
    Private COL_SubscriberChangeGender As Integer = 14
    Private COL_SubscriberChangeDOB As Integer = 15
    Private COL_SubscriberChangeSSN As Integer = 16
    Private COL_SubscriberChangeAddress1 As Integer = 17
    Private COL_SubscriberChangeAddress2 As Integer = 18
    Private COL_SubscriberChangeCity As Integer = 19
    Private COL_SubscriberChangeState As Integer = 20
    Private COL_SubscriberChangeZip As Integer = 21
    Private COL_ChangeCount As Integer = 22

#End Region

    'Code Start Added by kanchan on 20120410 for rxEligibility
    Private Function GetPatientAddress(ByVal _PatientID As Int64) As DataTable
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim strSQL As String
        Dim PataddrDT As DataTable
        strSQL = "SELECT (sFirstName + ' ' + sMiddleName + ' ' + sLastName) as PatName," _
        & "Case sGender when 'Male' then 'M' when 'Female' then 'F' when 'Other' then 'U' end " _
        & "as PatGender,convert (char(10), convert (datetime,dtDOB), 101) as PatDOB,isnull(nSSN,'') as PatSSN," _
        & "isnull(sAddressLine1,'')as PatAddressLine1, isnull(sAddressLine2,'') as PatAddressLine2," _
        & "isnull(sCity,'') as PatCity, isnull(sState ,'')as PatState, isnull( sZIP,'') as PatZip FROM Patient " _
        & "WHERE nPatientID = " & _PatientID & ""
        oDB.Connect(GetConnectionString)
        PataddrDT = oDB.ReadQueryDataTable(strSQL)

        oDB.Disconnect()
        'SLR: oDB.dispose and then
        oDB.Dispose()
        oDB = Nothing

        Return PataddrDT

    End Function

    Private Sub frmViewEligiblityInformation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Code start-Added & commented by kanchan on 20120404 to apply stylesheet changes
        Try
            Dim _PatientID As Int64
            Try
                _PatientID = _dtEligiblityInformation.Rows(0)("nPatientID")
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            If _dtEligiblityInformation.Rows.Count > 0 Then

                If _blnShowMsgPnl = True Then
                    Dim strBldrMsg = New System.Text.StringBuilder
                    'strBldrMsg.Append("Eligibility information is already available for this patient. You may request another request after 72 hours of the last request!" & vbCrLf)
                    'strBldrMsg.Append("Last eligibility request date time: " & _dtEligiblityInformation.Rows(0)("dtResquestDateTimeStamp"))
                    strBldrMsg.Append("Eligibility information may be updated every 72 hours. The displayed information was last updated " & _dtEligiblityInformation.Rows(0)("dtResquestDateTimeStamp"))
                    lbl72HrsMessage.Text = strBldrMsg.ToString
                    strBldrMsg = Nothing

                    pnl72HrsMessage.Visible = True
                Else
                    pnl72HrsMessage.Visible = False
                End If

            


                Dim dtPatient As DataTable = GetPatientAddress(_PatientID)
                dtPatient.TableName = "Patient"
                'Dim ds As DataSet = _dtEligiblityInformation.DataSet
                For i As Integer = 0 To _dtEligiblityInformation.Rows.Count - 1
                    Dim sMessageType As String = _dtEligiblityInformation.Rows(i)("sRespDtlMessageType").ToString()
                    If sMessageType.Contains("|") Then
                        Dim sErrMessage As String = GetEDIErrorDescription(sMessageType)
                        _dtEligiblityInformation.Rows(i)("sRespDtlMessageType") = sErrMessage
                    Else
                        _dtEligiblityInformation.Rows(i)("sRespDtlMessageType") = ""
                    End If

                Next
                _dtEligiblityInformation.TableName = "eligibility"
                Dim ds As New DataSet
                ds.Tables.Add(_dtEligiblityInformation.Copy())
                ds.Tables.Add(dtPatient)

                Dim _FileName As String = GenerateFileName()
                ds.WriteXml(_FileName)
                'SLR: Free dtPatient, remove ds.tables, free ds
                If Not dtPatient Is Nothing Then
                    dtPatient.Dispose()
                    dtPatient = Nothing
                End If
                If Not ds Is Nothing Then
                    ds.Clear()
                    ds.Dispose()
                    ds = Nothing
                End If

                Dim myXslTransform As Xsl.XslTransform
                Dim _strfileName As String = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssffff") & ".html" ' gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html" 'New document name is slower due to checking for existance of file
                myXslTransform = New Xml.Xsl.XslTransform()
                myXslTransform.Load(System.Windows.Forms.Application.StartupPath & "\gloRxeligible.xsl")
                myXslTransform.Transform(_FileName, _strfileName)
                'SLR: Free myXslTransofrm
                If Not myXslTransform Is Nothing Then
                    myXslTransform = Nothing
                End If
                WebBrowser1.Navigate(_strfileName)
            End If
        Catch ex As Exception

        End Try


        'gloC1FlexStyle.Style(C1271demoInfo)

        'Dim textLengthBefore As Integer
        'Dim textLengthAfter As Integer
        'Dim strSpcace As String = "                                                                                 "
        ''Dim strSpLine As String = "_______________________________________________________________" & vbCrLf & vbCrLf
        'Dim strSpLine As String = "-----------------------------------------------------------------------------------------------------------------------------------------------" & vbCrLf
        'Dim retailoutFlag As Boolean = False
        'Dim MailorderOutFlag As Boolean = False
        'Dim phMailorderOutFlag As Boolean = False
        'Dim DependentPatientFlag As Boolean = False
        'Dim strPatientFullName As String = ""
        'Dim dtPatienAddr As New DataTable




        'Try


        '    If Not IsNothing(_dtEligiblityInformation) Then
        '        If _dtEligiblityInformation.Rows.Count > 0 Then


        '            Try
        '                Dim _PatientID = _dtEligiblityInformation.Rows(0)("nPatientID")
        '                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
        '            Catch ex As Exception
        '                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '            End Try
        '            'Set270GridStyle()
        '            Set271GridStyle()

        '            Dim strBldr As New System.Text.StringBuilder
        '            Dim sFormattedDate As String = ""
        '            'Dim strDt As String = ""

        '            If _dtEligiblityInformation.Rows.Count > 0 Then

        '                Dim myFntHdr_TahomaBld As New Font("Tahoma", 12.0F, FontStyle.Bold, GraphicsUnit.Point) ', CByte(0))
        '                Dim myFntHdr_TahomaReg As New Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point) ', CByte(0))
        '                Dim myFntLblCptn_TahomaBld As New Font("Tahoma", 9.0F, FontStyle.Bold, GraphicsUnit.Point) ', CByte(0))
        '                Dim myFontLblVal_TahomaReg As New Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point) ', CByte(0))

        '                Dim sPhysicianName As String = _dtEligiblityInformation.Rows(0)("sPhysicianName").ToString()
        '                Dim sPhySuffix As String = _dtEligiblityInformation.Rows(0)("sPhysicianSuffix").ToString()

        '                'strBldr.Append("Physician name: " & sPhysicianName & " " & sPhySuffix & vbCrLf)
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Physician Name: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = myFntLblCptn_TahomaBld 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(sPhysicianName & " " & sPhySuffix & vbCrLf & vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = myFontLblVal_TahomaReg ' New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'strBldr.Append("NPI no: " & _dtEligiblityInformation.Rows(0)("sNPINumber").ToString() & vbCrLf)
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("NPI no: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = myFntLblCptn_TahomaBld

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(0)("sNPINumber").ToString() & vbCrLf & vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = myFontLblVal_TahomaReg

        '                strBldr.Append(Environment.NewLine)

        '                ''Added for ANSI 5010
        '                For Cnt As Integer = 0 To _dtEligiblityInformation.Rows.Count - 1

        '                    rtfEligiliblityinfo.ForeColor = Color.FromArgb(31, 73, 125)

        '                    'strBldr.Append("                                        Pharmacy Benefit Source Response: " & _dtEligiblityInformation.Rows(Cnt)("sSTLoopControlID").ToString())
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("                           Pharmacy Benefit Source Response: " & _dtEligiblityInformation.Rows(Cnt)("sSTLoopControlID").ToString() & "                        " & vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = myFntHdr_TahomaBld
        '                    'rtfEligiliblityinfo.SelectionAlignment = HorizontalAlignment.Center
        '                    rtfEligiliblityinfo.SelectionColor = Color.White

        '                    rtfEligiliblityinfo.SelectionBackColor = Color.FromArgb(123, 157, 204)

        '                    'textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    'rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(Cnt)("sSTLoopControlID").ToString() & vbCrLf)
        '                    'textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    'rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    'rtfEligiliblityinfo.SelectionFont = myFontLblVal_TahomaReg

        '                    strBldr.Append(Environment.NewLine)


        '                    Dim sMessageType As String = _dtEligiblityInformation.Rows(Cnt)("sRespDtlMessageType").ToString()
        '                    If sMessageType.Contains("|") Then
        '                        Dim sErrMessage As String = GetEDIErrorDescription(sMessageType)
        '                        'strBldr.Append("*Note: PBM - " & _dtEligiblityInformation.Rows(Cnt)("sPBM_PayerName").ToString() & " recieved rejection message  " & """" & sErrMessage & """" & " " & vbCrLf)
        '                        rtfEligiliblityinfo.AppendText(vbCrLf)
        '                        textLengthBefore = rtfEligiliblityinfo.TextLength
        '                        rtfEligiliblityinfo.AppendText("*Note: PBM " & _dtEligiblityInformation.Rows(Cnt)("sPBM_PayerName").ToString() & " recieved rejection message  " & """" & sErrMessage & """" & " " & vbCrLf)
        '                        textLengthAfter = rtfEligiliblityinfo.TextLength
        '                        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                        rtfEligiliblityinfo.SelectionFont = myFontLblVal_TahomaReg
        '                        rtfEligiliblityinfo.SelectionColor = Color.Red
        '                        textLengthBefore = rtfEligiliblityinfo.TextLength



        '                        strBldr.Append(Environment.NewLine)
        '                    End If


        '                    strBldr.Append("PBM name: " & _dtEligiblityInformation.Rows(Cnt)("sPBM_PayerName").ToString() & vbCrLf)
        '                    strBldr.Append("PBM participant ID : " & _dtEligiblityInformation.Rows(Cnt)("sPBM_PayerParticipantID").ToString() & vbCrLf)

        '                    Dim sSubsTranIdentification As String = _dtEligiblityInformation.Rows(Cnt)("sTRNReferenceIdentification").ToString().Trim()
        '                    Dim sSubsTransOrigCompIdentifier As String = _dtEligiblityInformation.Rows(Cnt)("sTRNOrignationCompanyIdentifier").ToString().Trim()
        '                    Dim sSubsTransDivorGrp As String = _dtEligiblityInformation.Rows(Cnt)("sTRNDivisionorGroup").ToString().Trim()
        '                    If sSubsTranIdentification <> "" Or sSubsTransOrigCompIdentifier <> "" Or sSubsTransDivorGrp <> "" Then
        '                        If sSubsTranIdentification.Length > 0 Then
        '                            strBldr.Append("Subscriber Transaction Identifier: " & _dtEligiblityInformation.Rows(Cnt)("sTRNReferenceIdentification").ToString() & vbCrLf)
        '                        End If
        '                        If sSubsTransOrigCompIdentifier.Length > 0 Then
        '                            ''Originating Company Identifier 
        '                            ''A unique identifier designating the company initiating the funds transfer instructions. The first character is one-digit ANSI identification code designation
        '                            ''(ICD) followed by the nine-digit identification number which may be an IRS employer identification number (EIN), data universal numbering system
        '                            ''(DUNS), or a user assigned number the lCD for an EIN is 1, DUNS is 3, user assigned(number Is 9) If TRNO1 is 2, this is the value received in the original 270 transaction
        '                            ''If TRNO1 is  use this infomiation to identify the organization that assigned this trace number. 
        '                            ''The first position must be either a “1’ if an EIN is used. a 3’ ¡f a DUNS is used or a  if a user assigned identifier is used.
        '                            Dim s1stPosition As String = sSubsTransOrigCompIdentifier.Substring(0, 1)
        '                            Dim sRemainingStr As String = sSubsTransOrigCompIdentifier.Substring(1)
        '                            Select Case s1stPosition
        '                                Case 1 ''EIN - Employee ndentification no
        '                                    strBldr.Append("Employee ndentification no: " & _dtEligiblityInformation.Rows(Cnt)("sTRNOrignationCompanyIdentifier").ToString().Substring(1) & vbCrLf)
        '                                Case 3 ''DUNS - Data universal numbering system
        '                                    strBldr.Append("Employee ndentification no: " & _dtEligiblityInformation.Rows(Cnt)("sTRNOrignationCompanyIdentifier").ToString().Substring(1) & vbCrLf)
        '                                Case 9 ''user assigned no
        '                                    strBldr.Append("Employee ndentification no: " & _dtEligiblityInformation.Rows(Cnt)("sTRNOrignationCompanyIdentifier").ToString().Substring(1) & vbCrLf)
        '                            End Select
        '                        End If
        '                        If sSubsTransDivorGrp.Length > 0 Then
        '                            strBldr.Append("Subscriber transaction division or group: " & _dtEligiblityInformation.Rows(Cnt)("sTRNDivisionorGroup").ToString().Substring(1) & vbCrLf)
        '                        End If
        '                    End If

        '                    strBldr.Append(strSpLine)


        '                    ''''''''PBM Member Information
        '                    Dim sEligdt As String = _dtEligiblityInformation.Rows(Cnt)("sEligiblityDate").ToString().Trim()
        '                    Dim sServdt As String = _dtEligiblityInformation.Rows(Cnt)("sServiceDate").ToString().Trim()
        '                    Dim sMemIdno As String = _dtEligiblityInformation.Rows(Cnt)("sPBM_PayerMemberID").ToString().Trim()
        '                    Dim sMemName As String = _dtEligiblityInformation.Rows(Cnt)("SubscriberName").ToString().Trim()
        '                    Dim sMemDOB As String = _dtEligiblityInformation.Rows(Cnt)("sSubscriberDOB").ToString().Trim()
        '                    Dim sMemGender As String = _dtEligiblityInformation.Rows(Cnt)("sSubscriberGender").ToString().Trim()
        '                    Dim sMemAddrln1 As String = _dtEligiblityInformation.Rows(Cnt)("sSubscriberAddress1").ToString().Trim()
        '                    Dim sMemAddrln2 As String = _dtEligiblityInformation.Rows(Cnt)("sSubscriberAddress2").ToString().Trim()
        '                    Dim sMemCity As String = _dtEligiblityInformation.Rows(Cnt)("sSubscriberCity").ToString().Trim()
        '                    Dim sMemState As String = _dtEligiblityInformation.Rows(Cnt)("sSubscriberState").ToString().Trim()
        '                    Dim sMemZipCode As String = _dtEligiblityInformation.Rows(Cnt)("sSubscriberZip").ToString().Trim()
        '                    Dim sRelshpcd As String = _dtEligiblityInformation.Rows(Cnt)("sRelationshipCode").ToString().Trim()
        '                    Dim sMemSSN As String = _dtEligiblityInformation.Rows(Cnt)("sSubscriberSSN").ToString().Trim()
        '                    '_dtEligiblityInformation.Rows(i)("SubscriberName") & ", " & _dtEligiblityInformation.Rows(i)("SubscriberSuffix")
        '                    If sEligdt <> "" Or sServdt <> "" Or sMemIdno <> "" Or sMemName <> "" Or sMemDOB <> "" Or sMemGender <> "" Or sMemAddrln1 <> "" Or sMemAddrln2 <> "" Or sMemCity <> "" Or sMemState <> "" Or sMemZipCode <> "" Or sRelshpcd <> "" Or sMemSSN <> "" Then 'only create the PBM Member Information block
        '                        strBldr.Append("                                        PBM Member Information" & vbCrLf)
        '                        strBldr.Append(Environment.NewLine)

        '                        If sEligdt.Length > 0 Then
        '                            '_Dt = _dtEligiblityInformation.Rows(Cnt)("sEligiblityDate").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("sEligiblityDate").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("sEligiblityDate").ToString().Substring(0, 4)
        '                            'strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
        '                            sFormattedDate = GetEDIFormattedDate(sEligdt)
        '                            strBldr.Append("Eligibility date: " & sFormattedDate & vbCrLf)
        '                        End If
        '                        If sServdt.Length > 0 Then
        '                            '_Dt = _dtEligiblityInformation.Rows(Cnt)("sServiceDate").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("sServiceDate").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("sServiceDate").ToString().Substring(0, 4)
        '                            'strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
        '                            sFormattedDate = GetEDIFormattedDate("", sServdt)
        '                            strBldr.Append("Service date: " & sFormattedDate & vbCrLf)
        '                        End If
        '                        If sMemIdno.Length > 0 Then
        '                            strBldr.Append("Member identification no: " & _dtEligiblityInformation.Rows(Cnt)("sPBM_PayerMemberID").ToString() & vbCrLf)
        '                        End If
        '                        If sMemName.Length > 0 Then
        '                            Dim sSubSuffix As String = _dtEligiblityInformation.Rows(Cnt)("SubscriberSuffix")
        '                            If sSubSuffix <> "" Then
        '                                strBldr.Append("Member name: " & _dtEligiblityInformation.Rows(Cnt)("SubscriberName") & ", " & _dtEligiblityInformation.Rows(Cnt)("SubscriberSuffix") & vbCrLf)
        '                            Else
        '                                strBldr.Append("Member name: " & _dtEligiblityInformation.Rows(Cnt)("SubscriberName") & vbCrLf)
        '                            End If

        '                        End If
        '                        If sMemDOB.Length > 0 Then
        '                            '_Dt = _dtEligiblityInformation.Rows(Cnt)("sSubscriberDOB").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("sSubscriberDOB").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("sSubscriberDOB").ToString().Substring(0, 4)
        '                            'strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
        '                            sFormattedDate = GetEDIFormattedDate(sMemDOB)
        '                            strBldr.Append("Member DOB: " & sFormattedDate & vbCrLf)
        '                        End If
        '                        If sMemGender.Length > 0 Then
        '                            If sMemGender = "M" Then
        '                                sMemGender = "Male"
        '                            ElseIf sMemGender = "F" Then
        '                                sMemGender = "Female"
        '                            ElseIf sMemGender = "U" Then
        '                                sMemGender = "Unknown"
        '                            ElseIf sMemGender = "O" Then
        '                                sMemGender = "Others"
        '                            End If
        '                            strBldr.Append("Member gender: " & sMemGender & vbCrLf)
        '                        End If
        '                        If sMemAddrln1.Length > 0 Then
        '                            strBldr.Append("Member address line1: " & _dtEligiblityInformation.Rows(Cnt)("sSubscriberAddress1").ToString() & vbCrLf)
        '                        End If
        '                        If sMemAddrln2.Length > 0 Then
        '                            strBldr.Append("Member address line2: " & _dtEligiblityInformation.Rows(Cnt)("sSubscriberAddress2").ToString() & vbCrLf)
        '                        End If
        '                        If sMemCity.Length > 0 Then
        '                            strBldr.Append("Member city: " & _dtEligiblityInformation.Rows(Cnt)("sSubscriberCity").ToString() & vbCrLf)
        '                        End If
        '                        If sMemState.Length > 0 Then
        '                            strBldr.Append("Member state: " & _dtEligiblityInformation.Rows(Cnt)("sSubscriberState").ToString & vbCrLf)
        '                        End If
        '                        If sMemZipCode.Length > 0 Then
        '                            strBldr.Append("Member Zip: " & _dtEligiblityInformation.Rows(Cnt)("sSubscriberZip").ToString() & vbCrLf)
        '                        End If

        '                        If sRelshpcd.Length > 0 Then ''if code is there then only description will come
        '                            strBldr.Append("Relationship code: " & _dtEligiblityInformation.Rows(Cnt)("sRelationshipCode").ToString() & vbCrLf)
        '                            strBldr.Append("Relationship description: " & _dtEligiblityInformation.Rows(Cnt)("sRelationshipDescription").ToString() & vbCrLf)
        '                            If _dtEligiblityInformation.Rows(Cnt)("IsSubscriberdemoChange") = "True" Then
        '                                strBldr.Append("Subscriber demographics changed: " & "Yes" & vbCrLf)
        '                            Else
        '                                strBldr.Append("Subscriber demographics changed: " & "No" & vbCrLf)
        '                            End If


        '                        End If

        '                        If sMemSSN.Length > 0 Then
        '                            strBldr.Append("Member SSN: " & _dtEligiblityInformation.Rows(Cnt)("sSubscriberSSN") & vbCrLf)
        '                        End If

        '                        strBldr.Append(strSpLine)
        '                    End If

        '                    '''''''''PBM Member Details
        '                    Dim sIdCardno As String = _dtEligiblityInformation.Rows(Cnt)("sCardHolderID").ToString().Trim
        '                    Dim sCardHoldername As String = _dtEligiblityInformation.Rows(Cnt)("sCardHolderName").ToString().Trim
        '                    Dim sPersoncode As String = _dtEligiblityInformation.Rows(Cnt)("sPersonCode").ToString().Trim
        '                    Dim sSSNno As String = _dtEligiblityInformation.Rows(Cnt)("sSocialSecurityNumber").ToString().Trim
        '                    Dim sPatAccNo As String = _dtEligiblityInformation.Rows(Cnt)("sPatientAccountNumber").ToString().Trim
        '                    If sIdCardno <> "" Or sCardHoldername <> "" Or sPersoncode <> "" Or sSSNno <> "" Or sPatAccNo <> "" Then ''only create the PBM Member Details block
        '                        strBldr.Append("                                      PBM Member Details" & vbCrLf)
        '                        strBldr.Append(Environment.NewLine)

        '                        If sIdCardno.Length > 0 Then
        '                            strBldr.Append("Identity card no (*Card holder ID): " & _dtEligiblityInformation.Rows(Cnt)("sCardHolderID").ToString() & vbCrLf)
        '                        End If
        '                        If sCardHoldername.Length > 0 Then
        '                            strBldr.Append("Card holder name: " & _dtEligiblityInformation.Rows(Cnt)("sCardHolderName").ToString() & vbCrLf)
        '                        End If
        '                        If sPersoncode.Length > 0 Then
        '                            strBldr.Append("Familiy unit member (*Person code): " & _dtEligiblityInformation.Rows(Cnt)("sPersonCode").ToString() & vbCrLf)
        '                        End If
        '                        If sSSNno.Length > 0 Then
        '                            strBldr.Append("Social security No: " & _dtEligiblityInformation.Rows(Cnt)("sSocialSecurityNumber").ToString() & vbCrLf)
        '                        End If
        '                        If sPatAccNo.Length > 0 Then
        '                            strBldr.Append("Patient account No: " & _dtEligiblityInformation.Rows(Cnt)("sPatientAccountNumber").ToString() & vbCrLf)
        '                        End If

        '                        strBldr.Append(strSpLine)
        '                    End If


        '                    ''''''''''''''''Health Plan Benefit Coverage Details
        '                    Dim sHPBStatus As String = _dtEligiblityInformation.Rows(Cnt)("sHealthPlanBenefitEligibilityInfo").ToString().Trim
        '                    Dim sHPBCovname As String = _dtEligiblityInformation.Rows(Cnt)("sHealthPlanBenefitCoverageName").ToString().Trim
        '                    Dim sHPBInsname As String = _dtEligiblityInformation.Rows(Cnt)("sHlthPlnCovInsTypeCode").ToString().Trim
        '                    Dim sHPBEligdt As String = _dtEligiblityInformation.Rows(Cnt)("HtlthPlnCovBenftEligDate").ToString().Trim
        '                    Dim sHPBServdt As String = _dtEligiblityInformation.Rows(Cnt)("HtlthPlnCovBenftServiceDate").ToString().Trim

        '                    If sHPBStatus <> "" Or sHPBCovname <> "" Or sHPBInsname <> "" Or sHPBEligdt <> "" Or sHPBServdt <> "" Then '''only create the Health Plan Benefit Coverage Details block
        '                        strBldr.Append("                                      Health Plan Benefit Coverage Details" & vbCrLf)
        '                        strBldr.Append(Environment.NewLine)

        '                        If sHPBStatus.Length > 0 Then
        '                            strBldr.Append("Health plan benefit status: " & _dtEligiblityInformation.Rows(Cnt)("sHealthPlanBenefitEligibilityInfo").ToString() & vbCrLf)
        '                        End If
        '                        If sHPBCovname.Length > 0 Then
        '                            strBldr.Append("Health plan benefit plan coverage name: " & _dtEligiblityInformation.Rows(Cnt)("sHealthPlanBenefitCoverageName").ToString() & vbCrLf)
        '                        End If
        '                        If sHPBInsname.Length > 0 Then
        '                            strBldr.Append("Health plan benefit insurance name: " & _dtEligiblityInformation.Rows(Cnt)("sHlthPlnCovInsTypeCode").ToString() & vbCrLf)
        '                        End If
        '                        If sHPBEligdt.Length > 0 Then
        '                            '_Dt = _dtEligiblityInformation.Rows(Cnt)("HtlthPlnCovBenftEligDate").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("HtlthPlnCovBenftEligDate").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("HtlthPlnCovBenftEligDate").ToString().Substring(0, 4)
        '                            'strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
        '                            sFormattedDate = GetEDIFormattedDate(sHPBEligdt)
        '                            strBldr.Append("Health plan benefit eligibility date: " & sFormattedDate & vbCrLf)
        '                        End If
        '                        If sHPBServdt.Length > 0 Then


        '                            sFormattedDate = GetEDIFormattedDate("", sHPBServdt)
        '                            strBldr.Append("Health plan benefit service date: " & sFormattedDate & vbCrLf)
        '                        End If
        '                        strBldr.Append(strSpLine)
        '                    End If


        '                    '''''''''''Subscriber information more details
        '                    Dim sHlthplnName As String = _dtEligiblityInformation.Rows(Cnt)("sHealthPlanName").ToString().Trim
        '                    Dim sHlthplnNo As String = _dtEligiblityInformation.Rows(Cnt)("sHealthPlanNumber").ToString().Trim
        '                    Dim sGroupname As String = _dtEligiblityInformation.Rows(Cnt)("sGroupName").ToString().Trim
        '                    Dim sGroupId As String = _dtEligiblityInformation.Rows(Cnt)("sGroupID").ToString().Trim
        '                    Dim sAltId As String = _dtEligiblityInformation.Rows(Cnt)("sAlternativeListID").ToString().Trim
        '                    Dim sCovId As String = _dtEligiblityInformation.Rows(Cnt)("sCoverageID").ToString().Trim
        '                    Dim sFormLyId As String = _dtEligiblityInformation.Rows(Cnt)("sFormularyListID").ToString().Trim
        '                    Dim sCopId As String = _dtEligiblityInformation.Rows(Cnt)("sCoPayID").ToString().Trim
        '                    Dim sBINPCNno As String = _dtEligiblityInformation.Rows(Cnt)("sBINNumberPCNNumber").ToString().Trim
        '                    Dim sEmpId As String = _dtEligiblityInformation.Rows(Cnt)("sEmployeeID").ToString().Trim

        '                    If sHlthplnName <> "" Or sHlthplnNo <> "" Or sGroupname <> "" Or sGroupId <> "" Or sAltId <> "" Or sCovId <> "" Or sFormLyId <> "" Or sCopId <> "" Or sBINPCNno <> "" Or sEmpId <> "" Then '''only create the Subscriber information more details block
        '                        strBldr.Append("                                      Subscriber Information More Details" & vbCrLf)
        '                        strBldr.Append(Environment.NewLine)

        '                        If sHlthplnName.Length > 0 Then
        '                            strBldr.Append("Health plan name: " & _dtEligiblityInformation.Rows(Cnt)("sHealthPlanName").ToString() & vbCrLf)
        '                        End If
        '                        If sHlthplnNo.Length > 0 Then
        '                            strBldr.Append("Health plan no: " & _dtEligiblityInformation.Rows(Cnt)("sHealthPlanNumber").ToString() & vbCrLf)
        '                        End If
        '                        If sGroupname.Length > 0 Then
        '                            strBldr.Append("Group name: " & _dtEligiblityInformation.Rows(Cnt)("sGroupName").ToString() & vbCrLf)
        '                        End If
        '                        If sGroupId.Length > 0 Then
        '                            strBldr.Append("Group no: " & _dtEligiblityInformation.Rows(Cnt)("sGroupID").ToString() & vbCrLf)
        '                        End If
        '                        If sAltId.Length > 0 Then
        '                            strBldr.Append("Alternate list ID: " & _dtEligiblityInformation.Rows(Cnt)("sAlternativeListID").ToString() & vbCrLf)
        '                        End If
        '                        If sCovId.Length > 0 Then
        '                            strBldr.Append("Coverage List ID: " & _dtEligiblityInformation.Rows(Cnt)("sCoverageID").ToString() & vbCrLf)
        '                        End If
        '                        If sFormLyId.Length > 0 Then
        '                            strBldr.Append("Drug formulary number ID: " & _dtEligiblityInformation.Rows(Cnt)("sFormularyListID").ToString() & vbCrLf)
        '                        End If
        '                        If sCopId.Length > 0 Then
        '                            strBldr.Append("Insurance Policy number (*Copay ID): " & _dtEligiblityInformation.Rows(Cnt)("sCoPayID").ToString() & vbCrLf)
        '                        End If
        '                        If sBINPCNno.Length > 0 Then
        '                            strBldr.Append("Plan Network Id(*BCN/PCN): " & _dtEligiblityInformation.Rows(Cnt)("sBINNumberPCNNumber").ToString() & vbCrLf)
        '                        End If
        '                        If sEmpId.Length > 0 Then
        '                            strBldr.Append("Employee ID: " & _dtEligiblityInformation.Rows(Cnt)("sEmployeeID").ToString() & vbCrLf)
        '                        End If

        '                        strBldr.Append(strSpLine)
        '                    End If



        '                    '''''Retail Pharmacy -- 88
        '                    Dim sRetPhStatus As String = _dtEligiblityInformation.Rows(Cnt)("sRetailPhEligiblityorBenefitInfo").ToString().Trim
        '                    Dim sRetPhCovname As String = _dtEligiblityInformation.Rows(Cnt)("sRetailPharmacyCoverageName").ToString().Trim
        '                    Dim sRetPhInsname As String = _dtEligiblityInformation.Rows(Cnt)("sRetailInsTypeCode").ToString().Trim
        '                    Dim sRetmontamt As String = _dtEligiblityInformation.Rows(Cnt)("sRetailMonetaryAmount").ToString().Trim
        '                    Dim sRetEligdt As String = _dtEligiblityInformation.Rows(Cnt)("RetailPharmEligDate").ToString().Trim
        '                    Dim sRetServdt As String = _dtEligiblityInformation.Rows(Cnt)("RetailPharmServiceDate").ToString().Trim

        '                    If sRetPhStatus <> "" Or sRetPhCovname <> "" Or sRetPhInsname <> "" Or sRetmontamt <> "" Or sRetEligdt <> "" Or sRetServdt <> "" Then '''only create the Retail Pharmacy block
        '                        '''''''Pharmacy Coverage Information
        '                        strBldr.Append("                                      Pharmacy Coverage Information" & vbCrLf)
        '                        strBldr.Append(Environment.NewLine)

        '                        'strBldr.Append("**********Retail Pharmacy**************" & vbCrLf)
        '                        If sRetPhStatus.Length > 0 Then
        '                            strBldr.Append("Retail pharmacy status: " & _dtEligiblityInformation.Rows(Cnt)("sRetailPhEligiblityorBenefitInfo").ToString() & vbCrLf)
        '                        End If
        '                        If sRetPhCovname.Length > 0 Then
        '                            strBldr.Append("Retail pharmacy plan coverage name: " & _dtEligiblityInformation.Rows(Cnt)("sRetailPharmacyCoverageName").ToString() & vbCrLf)
        '                        End If
        '                        If sRetPhInsname.Length > 0 Then
        '                            strBldr.Append("Retail pharmacy insurance name: " & _dtEligiblityInformation.Rows(Cnt)("sRetailInsTypeCode").ToString() & vbCrLf)
        '                        End If
        '                        If sRetmontamt.Length > 0 Then
        '                            strBldr.Append("Retail pharmacy monetary ammount: " & _dtEligiblityInformation.Rows(Cnt)("sRetailMonetaryAmount").ToString() & vbCrLf)
        '                        End If
        '                        If sRetEligdt.Length > 0 Then
        '                            '_Dt = _dtEligiblityInformation.Rows(Cnt)("RetailPharmEligDate").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("RetailPharmEligDate").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("RetailPharmEligDate").ToString().Substring(0, 4)
        '                            'strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
        '                            sFormattedDate = GetEDIFormattedDate(sRetEligdt)
        '                            strBldr.Append("Retail pharmacy eligibility date: " & sFormattedDate & vbCrLf)
        '                        End If
        '                        If sRetServdt.Length > 0 Then
        '                            '_Dt = _dtEligiblityInformation.Rows(Cnt)("RetailPharmServiceDate").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("RetailPharmServiceDate").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("RetailPharmServiceDate").ToString().Substring(0, 4)
        '                            'strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
        '                            sFormattedDate = GetEDIFormattedDate("", sRetServdt)
        '                            strBldr.Append("Retail pharmacy service date: " & sFormattedDate & vbCrLf)
        '                        End If

        '                        strBldr.Append(Environment.NewLine)
        '                    End If


        '                    ''''Mail Pharmacy - 99
        '                    Dim sMailPhStatus As String = _dtEligiblityInformation.Rows(Cnt)("sMailOrdEligiblityorBenefitInfo").ToString().Trim
        '                    Dim sMailPhCovname As String = _dtEligiblityInformation.Rows(Cnt)("sMailOrderRxDrugCoverageName").ToString().Trim
        '                    Dim sMailPhInsname As String = _dtEligiblityInformation.Rows(Cnt)("sMailOrderInsTypeCode").ToString().Trim
        '                    Dim sMailmontamt As String = _dtEligiblityInformation.Rows(Cnt)("sMailOrderMonetaryAmount").ToString().Trim
        '                    Dim sMailEligdt As String = _dtEligiblityInformation.Rows(Cnt)("MailOrdPharmEligDate").ToString().Trim
        '                    Dim sMailServdt As String = _dtEligiblityInformation.Rows(Cnt)("MailOrdPharmServiceDate").ToString().Trim

        '                    If sMailPhStatus <> "" Or sMailPhCovname <> "" Or sMailPhInsname <> "" Or sMailmontamt <> "" Or sMailEligdt <> "" Or sMailServdt <> "" Then '''only create the Mail Pharmacy block
        '                        'strBldr.Append("**********Mail Pharmacy**************" & vbCrLf)
        '                        If sMailPhStatus.Length > 0 Then
        '                            strBldr.Append("Mail pharmacy status: " & _dtEligiblityInformation.Rows(Cnt)("sMailOrdEligiblityorBenefitInfo").ToString() & vbCrLf)
        '                        End If
        '                        If sMailPhCovname.Length > 0 Then
        '                            strBldr.Append("Mail pharmacy plan coverage name: " & _dtEligiblityInformation.Rows(Cnt)("sMailOrderRxDrugCoverageName").ToString() & vbCrLf)
        '                        End If
        '                        If sMailPhInsname.Length > 0 Then
        '                            strBldr.Append("Mail pharmacy insurance name: " & _dtEligiblityInformation.Rows(Cnt)("sMailOrderInsTypeCode").ToString() & vbCrLf)
        '                        End If
        '                        If sMailmontamt.Length > 0 Then
        '                            strBldr.Append("Mail pharmacy monetary ammount: " & _dtEligiblityInformation.Rows(Cnt)("sMailOrderMonetaryAmount").ToString() & vbCrLf)
        '                        End If
        '                        If sMailEligdt.Length > 0 Then
        '                            '_Dt = _dtEligiblityInformation.Rows(Cnt)("MailOrdPharmEligDate").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("MailOrdPharmEligDate").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("MailOrdPharmEligDate").ToString().Substring(0, 4)
        '                            'strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
        '                            sFormattedDate = GetEDIFormattedDate(sMailEligdt)
        '                            strBldr.Append("Mail pharmacy eligibility date: " & sFormattedDate & vbCrLf)
        '                        End If
        '                        If sMailServdt.Length > 0 Then

        '                            sFormattedDate = GetEDIFormattedDate("", sMailServdt)
        '                            strBldr.Append("Mail pharmacy service date: " & sFormattedDate & vbCrLf)
        '                        End If

        '                        strBldr.Append(Environment.NewLine)
        '                    End If

        '                    '''''LTC Pharmacy
        '                    Dim sLTCCovname As String = _dtEligiblityInformation.Rows(Cnt)("LTCPharmCovName").ToString().Trim
        '                    Dim sLTCInsname As String = _dtEligiblityInformation.Rows(Cnt)("sLTCPharmacyInsTypeCode").ToString().Trim
        '                    Dim sLTCEligdt As String = _dtEligiblityInformation.Rows(Cnt)("LTCPharmEligDate").ToString().Trim
        '                    Dim sLTCServdt As String = _dtEligiblityInformation.Rows(Cnt)("LTCPharmServiceDate").ToString().Trim
        '                    Dim sLTCCovStatus As String = _dtEligiblityInformation.Rows(Cnt)("LTCPhEligiblityorBenefitInfo").ToString().Trim
        '                    If sLTCCovStatus.Length > 0 Then
        '                        strBldr.Append("LTC pharmacy status: " & _dtEligiblityInformation.Rows(Cnt)("LTCPhEligiblityorBenefitInfo").ToString() & vbCrLf)
        '                    End If
        '                    If sLTCCovname <> "" Or sLTCInsname <> "" Or sLTCEligdt <> "" Or sLTCServdt <> "" Then '''only create the LTC Pharmacy block
        '                        'strBldr.Append("**********LTC Pharmacy**************" & vbCrLf)
        '                        If sLTCCovname.Length > 0 Then
        '                            strBldr.Append("LTC pharmacy plan coverage name: " & _dtEligiblityInformation.Rows(Cnt)("LTCPharmCovName").ToString() & vbCrLf)
        '                        End If
        '                        If sLTCInsname.Length > 0 Then
        '                            strBldr.Append("LTC pharmacy insurance name: " & _dtEligiblityInformation.Rows(Cnt)("sLTCPharmacyInsTypeCode").ToString() & vbCrLf)
        '                        End If
        '                        If sLTCEligdt.Length > 0 Then
        '                            '_Dt = _dtEligiblityInformation.Rows(Cnt)("LTCPharmEligDate").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("LTCPharmEligDate").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("LTCPharmEligDate").ToString().Substring(0, 4)
        '                            'strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
        '                            sFormattedDate = GetEDIFormattedDate(sLTCEligdt)
        '                            strBldr.Append("LTC pharmacy eligibility date: " & sFormattedDate & vbCrLf)
        '                        End If
        '                        If sLTCServdt.Length > 0 Then
        '                            '_Dt = _dtEligiblityInformation.Rows(Cnt)("LTCPharmServiceDate").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("LTCPharmServiceDate").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(Cnt)("LTCPharmServiceDate").ToString().Substring(0, 4)
        '                            'strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
        '                            sFormattedDate = GetEDIFormattedDate("", sLTCServdt)
        '                            strBldr.Append("LTC pharmacy service date: " & sFormattedDate & vbCrLf)
        '                        End If

        '                        strBldr.Append(Environment.NewLine)
        '                    End If

        '                    ''''Specialty Pharmacy
        '                    Dim sSpecCovname As String = _dtEligiblityInformation.Rows(Cnt)("SpecialtyPharmCovName").ToString().Trim
        '                    Dim sSpecInsname As String = _dtEligiblityInformation.Rows(Cnt)("sSpecialtyPharmacyInsTypeCode").ToString().Trim
        '                    Dim sSpecEligdt As String = _dtEligiblityInformation.Rows(Cnt)("SpecialtyPharmEligDate").ToString().Trim
        '                    Dim sSpecServdt As String = _dtEligiblityInformation.Rows(Cnt)("SpecialtyPharmServiceDate").ToString().Trim
        '                    Dim sSpecCovStatus As String = _dtEligiblityInformation.Rows(Cnt)("SpecialityPhEligiblityorBenefitInfo").ToString().Trim
        '                    If sSpecCovStatus.Length > 0 Then
        '                        strBldr.Append("Specialty pharmacy status: " & _dtEligiblityInformation.Rows(Cnt)("SpecialityPhEligiblityorBenefitInfo").ToString() & vbCrLf)
        '                    End If
        '                    If sSpecCovname <> "" Or sSpecInsname <> "" Or sSpecEligdt <> "" Or sSpecServdt <> "" Then '''only create the LTC Pharmacy block
        '                        'strBldr.Append("**********Specialty Pharmacy**************" & vbCrLf)
        '                        If sSpecCovname.Length > 0 Then
        '                            strBldr.Append("Specialty pharmacy plan coverage name: " & _dtEligiblityInformation.Rows(Cnt)("SpecialtyPharmCovName").ToString() & vbCrLf)
        '                        End If
        '                        If sSpecInsname.Length > 0 Then
        '                            strBldr.Append("Specialty pharmacy insurance name: " & _dtEligiblityInformation.Rows(Cnt)("sSpecialtyPharmacyInsTypeCode").ToString() & vbCrLf)
        '                        End If
        '                        If sSpecEligdt.Length > 0 Then

        '                            sFormattedDate = GetEDIFormattedDate(sSpecEligdt)
        '                            strBldr.Append("Specialty pharmacy eligibility date: " & sFormattedDate & vbCrLf)
        '                        End If
        '                        If sSpecServdt.Length > 0 Then

        '                            sFormattedDate = GetEDIFormattedDate("", sSpecServdt)
        '                            strBldr.Append("Specialty pharmacy service date: " & sFormattedDate & vbCrLf)
        '                        End If

        '                        strBldr.Append(Environment.NewLine)
        '                    End If

        '                    ''''Retail/Mail Pharmacy Payer Information
        '                    Dim sIsPriPayer As String = _dtEligiblityInformation.Rows(Cnt)("sIsPrimaryPayer").ToString().Trim
        '                    Dim sPriPayername As String = _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerName").ToString().Trim
        '                    Dim sPriPayerno As String = _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerNumber").ToString().Trim

        '                    If sIsPriPayer <> "" Or sPriPayername <> "" Or sPriPayerno <> "" Then '''only create the Retail/Mail Pharmacy Payer Information block
        '                        strBldr.Append("                              Retail/Mail Pharmacy Payer Information" & vbCrLf)
        '                        strBldr.Append(Environment.NewLine)

        '                        If sIsPriPayer.Length > 0 Then
        '                            strBldr.Append("IsPrimary payer: " & _dtEligiblityInformation.Rows(Cnt)("sIsPrimaryPayer").ToString() & vbCrLf)
        '                        End If
        '                        If sPriPayername.Length > 0 Then
        '                            strBldr.Append("Payer name: " & _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerName").ToString() & vbCrLf)
        '                        End If
        '                        If sPriPayerno.Length > 0 Then
        '                            strBldr.Append("Payer no: " & _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerNumber").ToString() & vbCrLf)
        '                        End If

        '                        '''''''''Retail Pharmacy payer information 
        '                        Dim sPriPayerRetElig As String = _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerRetailsEligible").ToString().Trim
        '                        Dim sPriPayerRetCoveInfo As String = _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerRetailCoverageInfo").ToString().Trim
        '                        Dim sPriPayerRetInsName As String = _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerRetailInsTypeCode").ToString().Trim
        '                        Dim sPriPayerRetMontAmt As String = _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerRetailMonetaryAmt").ToString().Trim

        '                        If sPriPayerRetElig <> "" Or sPriPayerRetCoveInfo <> "" Or sPriPayerRetInsName <> "" Or sPriPayerRetMontAmt <> "" Then '''only create the Retail Pharmacy Payer Information block
        '                            'strBldr.Append("**********Retail Pharmacy**************" & vbCrLf)
        '                            If sPriPayerRetElig.Length > 0 Then
        '                                strBldr.Append("Retail pharmacy primary payer eligible : " & _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerRetailsEligible").ToString() & vbCrLf)
        '                            End If
        '                            If sPriPayerRetCoveInfo.Length > 0 Then
        '                                strBldr.Append("Retail Pharmacy primary payer coverage status: " & _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerRetailCoverageInfo").ToString() & vbCrLf)
        '                            End If
        '                            If sPriPayerRetInsName.Length > 0 Then
        '                                strBldr.Append("Retail Pharmacy primary payer insurance name: " & _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerRetailInsTypeCode").ToString() & vbCrLf)
        '                            End If
        '                            If sPriPayerRetMontAmt.Length > 0 Then
        '                                strBldr.Append("Retail Pharmacy primary payer monetary ammount: " & _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerRetailMonetaryAmt").ToString() & vbCrLf)
        '                            End If

        '                            strBldr.Append(Environment.NewLine)
        '                        End If

        '                        '''''''''Mail Pharmacy payer information 
        '                        Dim sPriPayerMailElig As String = _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerMailOrderEligible").ToString().Trim
        '                        Dim sPriPayerMailCoveInfo As String = _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerMailOrderCoverageInfo").ToString().Trim
        '                        Dim sPriPayerMailInsName As String = _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerMailOrderInsTypeCode").ToString().Trim
        '                        Dim sPriPayerMailMontAmt As String = _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerMailOrderMonetaryAmt").ToString().Trim
        '                        If sPriPayerMailElig <> "" Or sPriPayerMailCoveInfo <> "" Or sPriPayerMailInsName <> "" Or sPriPayerMailMontAmt <> "" Then '''only create the Mail Pharmacy Payer Information block
        '                            'strBldr.Append("**********Mail Pharmacy**************" & vbCrLf)
        '                            If sPriPayerMailElig.Length > 0 Then
        '                                strBldr.Append("Mail pharmacy primary payer eligible : " & _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerMailOrderEligible").ToString() & vbCrLf)
        '                            End If
        '                            If sPriPayerMailCoveInfo.Length > 0 Then
        '                                strBldr.Append("Mail Pharmacy primary payer coverage status: " & _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerMailOrderCoverageInfo").ToString() & vbCrLf)
        '                            End If
        '                            If sPriPayerMailInsName.Length > 0 Then
        '                                strBldr.Append("Mail Pharmacy primary payer insurance name: " & _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerMailOrderInsTypeCode").ToString() & vbCrLf)
        '                            End If
        '                            If sPriPayerMailMontAmt.Length > 0 Then
        '                                strBldr.Append("Mail Pharmacy primary payer monetary ammount: " & _dtEligiblityInformation.Rows(Cnt)("sPrimaryPayerMailOrderMonetaryAmt").ToString() & vbCrLf)
        '                            End If

        '                            strBldr.Append(Environment.NewLine)
        '                        End If

        '                    End If ''''''Retail/Mail Pharmacy Payer Information

        '                    ''''Retail/Mail Pharmacy Contract Provider Information
        '                    Dim sIsContProv As String = _dtEligiblityInformation.Rows(Cnt)("sIsContractedProvider").ToString().Trim
        '                    Dim sPriContProvname As String = _dtEligiblityInformation.Rows(Cnt)("sContractedProviderName").ToString().Trim
        '                    Dim sPriContProvno As String = _dtEligiblityInformation.Rows(Cnt)("sContractedProviderNumber").ToString().Trim

        '                    If sIsContProv <> "" Or sPriContProvname <> "" Or sPriContProvno <> "" Then '''only create the Retail/Mail Pharmacy Contracted provider block
        '                        strBldr.Append("                                  Retail/Mail Pharmacy Contract Provider Information" & vbCrLf)
        '                        strBldr.Append(Environment.NewLine)

        '                        If sIsContProv.Length > 0 Then
        '                            strBldr.Append("IsContracted provider: " & _dtEligiblityInformation.Rows(Cnt)("sIsContractedProvider").ToString() & vbCrLf)
        '                        End If
        '                        If sPriContProvname.Length > 0 Then
        '                            strBldr.Append("Contract provider name: " & _dtEligiblityInformation.Rows(Cnt)("sContractedProviderName").ToString() & vbCrLf)
        '                        End If
        '                        If sPriContProvno.Length > 0 Then
        '                            strBldr.Append("Contract provider no: " & _dtEligiblityInformation.Rows(Cnt)("sContractedProviderNumber").ToString() & vbCrLf)
        '                        End If

        '                        '''''''''Retail Pharmacy Contracted provider information 
        '                        Dim sContProvRetElig As String = _dtEligiblityInformation.Rows(Cnt)("sContProvRetailsEligible").ToString().Trim
        '                        Dim sContProvRetCoveInfo As String = _dtEligiblityInformation.Rows(Cnt)("sContProvRetailCoverageInfo").ToString().Trim
        '                        Dim sContProvRetInsName As String = _dtEligiblityInformation.Rows(Cnt)("sContProvRetailInsTypeCode").ToString().Trim
        '                        Dim sContProvRetMontAmt As String = _dtEligiblityInformation.Rows(Cnt)("sContProvRetailMonetaryAmt").ToString().Trim

        '                        If sContProvRetElig <> "" Or sContProvRetCoveInfo <> "" Or sContProvRetInsName <> "" Or sContProvRetMontAmt <> "" Then ''''only create the Retail Pharmacy Contracted provider information block
        '                            'strBldr.Append("**********Retail Pharmacy**************" & vbCrLf)
        '                            If sContProvRetElig.Length > 0 Then
        '                                strBldr.Append("Retail pharmacy contract provider eligible : " & _dtEligiblityInformation.Rows(Cnt)("sContProvRetailsEligible").ToString() & vbCrLf)
        '                            End If
        '                            If sContProvRetCoveInfo.Length > 0 Then
        '                                strBldr.Append("Retail Pharmacy contract provider coverage status: " & _dtEligiblityInformation.Rows(Cnt)("sContProvRetailCoverageInfo").ToString() & vbCrLf)
        '                            End If
        '                            If sContProvRetInsName.Length > 0 Then
        '                                strBldr.Append("Retail Pharmacy contract provider insurance name: " & _dtEligiblityInformation.Rows(Cnt)("sContProvRetailInsTypeCode").ToString() & vbCrLf)
        '                            End If
        '                            If sContProvRetMontAmt.Length > 0 Then
        '                                strBldr.Append("Retail Pharmacy contract provider monetary ammount: " & _dtEligiblityInformation.Rows(Cnt)("sContProvRetailMonetaryAmt").ToString() & vbCrLf)
        '                            End If

        '                            strBldr.Append(Environment.NewLine)
        '                        End If

        '                        '''''''''Mail Pharmacy Contracted provider information 
        '                        Dim sContProvMailElig As String = _dtEligiblityInformation.Rows(Cnt)("sContProvMailOrderEligible").ToString().Trim
        '                        Dim sContProvMailCoveInfo As String = _dtEligiblityInformation.Rows(Cnt)("sContProvMailOrderCoverageInfo").ToString().Trim
        '                        Dim sContProvMailInsName As String = _dtEligiblityInformation.Rows(Cnt)("sContProvMailOrderInsTypeCode").ToString().Trim
        '                        Dim sContProvMailMontAmt As String = _dtEligiblityInformation.Rows(Cnt)("sContProvMailOrderMonetaryAmt").ToString().Trim

        '                        If sContProvMailElig <> "" Or sContProvMailCoveInfo <> "" Or sContProvMailInsName <> "" Or sContProvMailMontAmt <> "" Then ''''only create the Mail Pharmacy Contracted provider information block
        '                            'strBldr.Append("**********Mail Pharmacy**************" & vbCrLf)
        '                            If sContProvMailElig.Length > 0 Then
        '                                strBldr.Append("Mail pharmacy contract provider eligible : " & _dtEligiblityInformation.Rows(Cnt)("sContProvMailOrderEligible").ToString() & vbCrLf)
        '                            End If
        '                            If sContProvMailCoveInfo.Length > 0 Then
        '                                strBldr.Append("Mail Pharmacy contract provider coverage status: " & _dtEligiblityInformation.Rows(Cnt)("sContProvMailOrderCoverageInfo").ToString() & vbCrLf)
        '                            End If
        '                            If sContProvMailInsName.Length > 0 Then
        '                                strBldr.Append("Mail Pharmacy contract provider insurance name: " & _dtEligiblityInformation.Rows(Cnt)("sContProvMailOrderInsTypeCode").ToString() & vbCrLf)
        '                            End If
        '                            If sContProvMailMontAmt.Length > 0 Then
        '                                strBldr.Append("Mail Pharmacy contract provider monetary ammount: " & _dtEligiblityInformation.Rows(Cnt)("sContProvMailOrderMonetaryAmt").ToString() & vbCrLf)
        '                            End If

        '                            strBldr.Append(Environment.NewLine)
        '                        End If

        '                    End If

        '                    strBldr.Append("************************ End of Pharmacy Benefit Response: " & _dtEligiblityInformation.Rows(Cnt)("sSTLoopControlID").ToString() & " ************************" & vbCrLf)
        '                    strBldr.Append(Environment.NewLine)
        '                    'strBldr.Append("Retail Pharmacy : " & _dtEligiblityInformation.Rows(Cnt)("").ToString() & vbCrLf)

        '                    Dim _Row As Integer
        '                    If _dtEligiblityInformation.Rows(Cnt)("IsSubscriberdemoChange") = "True" Or _dtEligiblityInformation.Rows(Cnt)("IsDependentdemoChange") = "True" Then
        '                        With C1271demoInfo
        '                            .Rows.Add()
        '                            _Row = .Rows.Count - 1
        '                            .SetData(_Row, COL_ChangeMessageID, _dtEligiblityInformation.Rows(Cnt)("sMessageID"))
        '                            .SetData(_Row, COL_ChangePatientID, _dtEligiblityInformation.Rows(Cnt)("nPatientID"))
        '                            .SetData(_Row, COL_PayerName, _dtEligiblityInformation.Rows(Cnt)("sPBM_PayerName"))
        '                            .SetData(_Row, COL_DependentdemochgName, _dtEligiblityInformation.Rows(Cnt)("DependentdemoChgName"))
        '                            .SetData(_Row, COL_DependentdemoChgGender, _dtEligiblityInformation.Rows(Cnt)("sDependentdemoChgGender"))
        '                            .SetData(_Row, COL_DependentdemoChgDOB, _dtEligiblityInformation.Rows(Cnt)("sDependentdemoChgDOB"))
        '                            .SetData(_Row, COL_DependentdemoChgSSN, _dtEligiblityInformation.Rows(Cnt)("sDependentdemoChgSSN"))
        '                            .SetData(_Row, COL_DependentdemoChgAddress1, _dtEligiblityInformation.Rows(Cnt)("sDependentdemoChgAddress1"))
        '                            .SetData(_Row, COL_DependentdemoChgAddress2, _dtEligiblityInformation.Rows(Cnt)("sDependentdemoChgAddress2"))
        '                            .SetData(_Row, COL_DependentdemoChgCity, _dtEligiblityInformation.Rows(Cnt)("sDependentdemoChgCity"))
        '                            .SetData(_Row, COL_DependentdemoChgState, _dtEligiblityInformation.Rows(Cnt)("sDependentdemoChgState"))
        '                            .SetData(_Row, COL_DependentdemoChgZip, _dtEligiblityInformation.Rows(Cnt)("sDependentdemoChgZip"))

        '                            Dim sSubSuffix As String = _dtEligiblityInformation.Rows(Cnt)("SubscriberSuffix")
        '                            If sSubSuffix <> "" Then
        '                                .SetData(_Row, COL_SubscriberChangeName, _dtEligiblityInformation.Rows(Cnt)("SubscriberName") & " " & sSubSuffix)
        '                            Else
        '                                .SetData(_Row, COL_SubscriberChangeName, _dtEligiblityInformation.Rows(Cnt)("SubscriberName"))
        '                            End If
        '                            '.SetData(_Row, COL_SubscriberChangeName, _dtEligiblityInformation.Rows(Cnt)("SubscriberDemoChgName"))

        '                            .SetData(_Row, COL_SubscriberChangeGender, _dtEligiblityInformation.Rows(Cnt)("sSubscriberDemoChgGender"))
        '                            .SetData(_Row, COL_SubscriberChangeDOB, _dtEligiblityInformation.Rows(Cnt)("sSubscriberDemoChgDOB"))
        '                            .SetData(_Row, COL_SubscriberChangeSSN, _dtEligiblityInformation.Rows(Cnt)("sSubscriberDemoChgSSN"))
        '                            .SetData(_Row, COL_SubscriberChangeAddress1, _dtEligiblityInformation.Rows(Cnt)("sSubscriberDemoChgAddress1"))
        '                            .SetData(_Row, COL_SubscriberChangeAddress2, _dtEligiblityInformation.Rows(Cnt)("sSubscriberDemoChgAddress2"))
        '                            .SetData(_Row, COL_SubscriberChangeCity, _dtEligiblityInformation.Rows(Cnt)("sSubscriberDemoChgCity"))
        '                            .SetData(_Row, COL_SubscriberChangeState, _dtEligiblityInformation.Rows(Cnt)("sSubscriberDemoChgState"))
        '                            .SetData(_Row, COL_SubscriberChangeZip, _dtEligiblityInformation.Rows(Cnt)("sSubscriberDemoChgZip"))
        '                            .SetData(_Row, COL_ResquestDateTimeStamp, _dtEligiblityInformation.Rows(Cnt)("dtResquestDateTimeStamp"))
        '                        End With

        '                        'With C1270demoInfo
        '                        '    .Rows.Add()
        '                        '    _Row = .Rows.Count - 1
        '                        '    .SetData(_Row, COL_DependentName, _dtEligiblityInformation.Rows(Cnt)("DependentName"))
        '                        '    .SetData(_Row, COL_DependentGender, _dtEligiblityInformation.Rows(Cnt)("sDGender"))
        '                        '    .SetData(_Row, COL_DependentDOB, _dtEligiblityInformation.Rows(Cnt)("sDDOB"))
        '                        '    .SetData(_Row, COL_DependentSSN, _dtEligiblityInformation.Rows(Cnt)("sDSSN"))
        '                        '    .SetData(_Row, COL_DependentAddress1, _dtEligiblityInformation.Rows(Cnt)("sDAddress1"))
        '                        '    .SetData(_Row, COL_DependentAddress2, _dtEligiblityInformation.Rows(Cnt)("sDAddress2"))
        '                        '    .SetData(_Row, COL_DependentCity, _dtEligiblityInformation.Rows(Cnt)("sDCity"))
        '                        '    .SetData(_Row, COL_DependentState, _dtEligiblityInformation.Rows(Cnt)("sDState"))
        '                        '    .SetData(_Row, COL_DepenedentZip, _dtEligiblityInformation.Rows(Cnt)("sDZip"))
        '                        '    .SetData(_Row, COL_PaitentID, _dtEligiblityInformation.Rows(Cnt)("nPatientID"))
        '                        '    .SetData(_Row, COL_MessageID, _dtEligiblityInformation.Rows(Cnt)("sMessageID"))
        '                        '    .SetData(_Row, COL_ResquestDateTimeStamp, _dtEligiblityInformation.Rows(Cnt)("dtResquestDateTimeStamp"))


        '                        '    .SetData(_Row, COL_SuscriberName, _dtEligiblityInformation.Rows(Cnt)("SubscriberName"))
        '                        '    .SetData(_Row, COL_SubscriberGender, _dtEligiblityInformation.Rows(Cnt)("sSubscriberGender"))
        '                        '    .SetData(_Row, COL_SubscriberDOB, _dtEligiblityInformation.Rows(Cnt)("sSubscriberDOB"))
        '                        '    .SetData(_Row, COL_SubscriberSSN, _dtEligiblityInformation.Rows(Cnt)("sSubscriberSSN"))
        '                        '    .SetData(_Row, COL_SubscriberAddress1, _dtEligiblityInformation.Rows(Cnt)("sSubscriberAddress1"))
        '                        '    .SetData(_Row, COL_SubscriberAddress2, _dtEligiblityInformation.Rows(Cnt)("sSubscriberAddress2"))
        '                        '    .SetData(_Row, COL_SubscriberCity, _dtEligiblityInformation.Rows(Cnt)("sSubscriberCity"))
        '                        '    .SetData(_Row, COL_SubscriberState, _dtEligiblityInformation.Rows(Cnt)("sSubscriberState"))
        '                        '    .SetData(_Row, COL_SubscriberZip, _dtEligiblityInformation.Rows(Cnt)("sSubscriberZip"))

        '                        'End With
        '                    Else


        '                    End If
        '                Next

        '                rtfEligiliblityinfo.AppendText(strBldr.ToString())
        '                strBldr = Nothing

        'Code End-Added & commented by kanchan on 20120404 to apply stylesheet changes
        ''Added for ANSI 5010



        'For i As Integer = 0 To _dtEligiblityInformation.Rows.Count - 1
        '    If i <> 0 Then
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '    End If





        '    '''' Plan/Request Information
        '    rtfEligiliblityinfo.ForeColor = Color.FromArgb(31, 73, 125)

        '    If _dtEligiblityInformation.Rows(i)("sPBM_PayerName").ToString().Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(" Benefit Source " & i + 1 & strSpcace)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 11.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        rtfEligiliblityinfo.SelectionAlignment = HorizontalAlignment.Left
        '        rtfEligiliblityinfo.SelectionColor = Color.White

        '        rtfEligiliblityinfo.SelectionBackColor = Color.FromArgb(123, 157, 204)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '    End If


        '    If _dtEligiblityInformation.Rows(i)("sPBM_PayerName").ToString().Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("PBM Payer Name: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sPBM_PayerName"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        'rtfEligiliblityinfo.SelectionColor = Color.FromArgb(0, 0, 0)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        rtfEligiliblityinfo.AppendText("     ")
        '    End If

        '    If _dtEligiblityInformation.Rows(i)("sHealthPlanName").ToString.Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Health Plan Name: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength

        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        'rtfEligiliblityinfo.SelectionColor = Color.FromArgb(31, 73, 125)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sHealthPlanName"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    Else
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '    End If
        '    ''rtfEligiliblityinfo.AppendText(strSpLine)

        '    If _dtEligiblityInformation.Rows(i)("sPhysicianName").ToString().Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Physician Name: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sPhysicianName"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        rtfEligiliblityinfo.AppendText(" ")
        '    Else
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'textLengthAfter = rtfEligiliblityinfo.TextLength
        '        'rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        'rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    End If

        '    If _dtEligiblityInformation.Rows(i)("sPhysicianSuffix").ToString().Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Physician Suffix: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sPhysicianSuffix"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '    Else
        '        rtfEligiliblityinfo.AppendText("     ")
        '        'textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'textLengthAfter = rtfEligiliblityinfo.TextLength
        '        'rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        'rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    End If

        '    '\\ health Plan No
        '    If _dtEligiblityInformation.Rows(i)("sHealthPlanNumber").ToString.Trim.Length Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Health Plan Number: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sHealthPlanNumber"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        'rtfEligiliblityinfo.AppendText("     ")
        '        textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    Else
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    End If

        '    '\\ Card holder Name
        '    If _dtEligiblityInformation.Rows(i)("sCardHolderName").ToString.Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Card Holder Name: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sCardHolderName"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        'textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'textLengthAfter = rtfEligiliblityinfo.TextLength
        '        'rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        'rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        rtfEligiliblityinfo.AppendText("     ")
        '    End If

        '    '\\ relationship              
        '    If _dtEligiblityInformation.Rows(i)("sRelationshipDescription").ToString.Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Relationship: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sRelationshipDescription"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    Else
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Relationship: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(("Unknown"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    End If




        '    rtfEligiliblityinfo.AppendText(strSpLine)


        '    '''' Depenent Information 
        '    If _dtEligiblityInformation.Rows(i)("DependentName").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sDGender").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sDDOB").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sDSSN").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sDAddress1").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sDAddress2").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sDCity").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sDState").ToString.Trim() <> "" And _dtEligiblityInformation.Rows(i)("sDZip").ToString.Trim() <> "" Then
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'textLengthBefore = rtfEligiliblityinfo.TextLength
        '        'rtfEligiliblityinfo.AppendText("Dependent Information: ")
        '        'textLengthAfter = rtfEligiliblityinfo.TextLength
        '        'rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        'rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 11.0F, System.Drawing.FontStyle.Bold + System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)

        '        If _dtEligiblityInformation.Rows(i)("DependentName").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Dependent Name: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("DependentName"))
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        End If
        '        'Depenent DOB
        '        If _dtEligiblityInformation.Rows(i)("sDDOB").ToString.Trim.Length > 0 Then
        '            'rtfEligiliblityinfo.AppendText("Dependent Date of Birth: " & _dtEligiblityInformation.Rows(i)("sDDOB"))

        '            Dim _Dt As String = _dtEligiblityInformation.Rows(i)("sDDOB").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(i)("sDDOB").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(i)("sDDOB").ToString().Substring(0, 4)
        '            Dim strDt As String = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Date of Birth: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            textLengthAfter = rtfEligiliblityinfo.TextLength

        '            'rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(strDt)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            rtfEligiliblityinfo.AppendText("     ")
        '        End If
        '        'Depenent Gender
        '        If _dtEligiblityInformation.Rows(i)("sDGender").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Gender: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            Dim gd As String = _dtEligiblityInformation.Rows(i)("sDGender")
        '            If gd = "M" Then
        '                gd = "Male"
        '            ElseIf gd = "F" Then
        '                gd = "Female"
        '            End If

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(gd)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            rtfEligiliblityinfo.AppendText("     ")
        '        End If
        '        'Depenent SSN
        '        If _dtEligiblityInformation.Rows(i)("sDSSN").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("SSN: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sDSSN"))
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        Else
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        End If
        '        ' Dependent Address:
        '        If _dtEligiblityInformation.Rows(i)("sDAddress1").ToString.Trim.Length > 0 Or _dtEligiblityInformation.Rows(i)("sDAddress2").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Address: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'rtfEligiliblityinfo.AppendText(vbCrLf)

        '            If _dtEligiblityInformation.Rows(i)("sDAddress1").ToString.Trim.Length > 0 Then
        '                'if present show Address Line 1
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sDAddress1").ToString())
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '                'rtfEligiliblityinfo.AppendText(vbCrLf)
        '            End If

        '            If _dtEligiblityInformation.Rows(i)("sDAddress2").ToString.Trim.Length > 0 Then
        '                'if present show Address Line 2
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(" " & _dtEligiblityInformation.Rows(i)("sDAddress2").ToString())
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '                ' rtfEligiliblityinfo.AppendText(vbCrLf)
        '            End If

        '            Dim _sCityStateZip As String = ""
        '            If _dtEligiblityInformation.Rows(i)("sDCity").ToString.Trim.Length > 0 Then
        '                _sCityStateZip = " " & _dtEligiblityInformation.Rows(i)("sDCity").ToString()
        '                If _dtEligiblityInformation.Rows(i)("sDState").ToString.Trim.Length > 0 Then
        '                    _sCityStateZip = _sCityStateZip & " " & _dtEligiblityInformation.Rows(i)("sDState").ToString()
        '                    If _dtEligiblityInformation.Rows(i)("sDZip").ToString.Trim.Length > 0 Then
        '                        _sCityStateZip = _sCityStateZip & " " & _dtEligiblityInformation.Rows(i)("sDZip").ToString()
        '                    End If
        '                End If
        '            End If

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(_sCityStateZip)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            rtfEligiliblityinfo.AppendText(vbCrLf)

        '        End If
        '        rtfEligiliblityinfo.AppendText(strSpLine)
        '    End If
        '    '''Dependent info--


        '    ''' Subscriber Information
        '    If _dtEligiblityInformation.Rows(i)("SubscriberName").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sSubscriberGender").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sSubscriberDOB").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sSubscriberSSN").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sSubscriberAddress1").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sSubscriberAddress2").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sSubscriberCity").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sSubscriberState").ToString.Trim() <> "" Or _dtEligiblityInformation.Rows(i)("sSubscriberZip").ToString.Trim() <> "" Then

        '        ''rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'textLengthBefore = rtfEligiliblityinfo.TextLength
        '        'rtfEligiliblityinfo.AppendText("Subscriber Information: ")
        '        'textLengthAfter = rtfEligiliblityinfo.TextLength
        '        'rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        'rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 11.0F, System.Drawing.FontStyle.Bold + System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)

        '        '\\ Subscriber name
        '        If _dtEligiblityInformation.Rows(i)("SubscriberName").ToString.Trim.Length > 0 Then
        '            If _dtEligiblityInformation.Rows(i)("SubscriberSuffix").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Subscriber Name: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("SubscriberName") & ", " & _dtEligiblityInformation.Rows(i)("SubscriberSuffix"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            Else
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Subscriber Name: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("SubscriberName"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            End If
        '        End If



        '        If _dtEligiblityInformation.Rows(i)("sSubscriberDOB").ToString.Trim.Length > 0 Then
        '            'rtfEligiliblityinfo.AppendText("Subscriber Date of Birth: " & _dtEligiblityInformation.Rows(i)("sSubscriberDOB"))
        '            Dim _Dt As String = _dtEligiblityInformation.Rows(i)("sSubscriberDOB").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(i)("sSubscriberDOB").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(i)("sSubscriberDOB").ToString().Substring(0, 4)
        '            Dim strDt As String = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Date of Birth: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(strDt)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            rtfEligiliblityinfo.AppendText("     ")
        '        End If
        '        'Subscriber gender
        '        If _dtEligiblityInformation.Rows(i)("sSubscriberGender").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Gender: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            Dim g As String = _dtEligiblityInformation.Rows(i)("sSubscriberGender")
        '            If g = "M" Then
        '                g = "Male"
        '            ElseIf g = "F" Then
        '                g = "Female"
        '            End If

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(g)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            rtfEligiliblityinfo.AppendText("     ")
        '        End If

        '        If _dtEligiblityInformation.Rows(i)("sSubscriberSSN").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("SSN: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sSubscriberSSN"))
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        Else
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        End If

        '        ''Subscriber Address:
        '        If _dtEligiblityInformation.Rows(i)("sSubscriberAddress1").ToString.Trim.Length > 0 Or _dtEligiblityInformation.Rows(i)("sSubscriberAddress2").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Address: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            If _dtEligiblityInformation.Rows(i)("sSubscriberAddress1").ToString.Trim.Length > 0 Then
        '                'if present show Address Line 1
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sSubscriberAddress1").ToString)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            End If

        '            If _dtEligiblityInformation.Rows(i)("sSubscriberAddress2").ToString.Trim.Length > 0 Then
        '                'if present show Address Line 2
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(" " & _dtEligiblityInformation.Rows(i)("sSubscriberAddress2"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            End If

        '            Dim _sCityStateZip As String = ""
        '            If _dtEligiblityInformation.Rows(i)("sSubscriberCity").ToString.Trim.Length > 0 Then
        '                _sCityStateZip = " " & _dtEligiblityInformation.Rows(i)("sSubscriberCity").ToString()
        '                If _dtEligiblityInformation.Rows(i)("sSubscriberState").ToString.Trim.Length > 0 Then
        '                    _sCityStateZip = _sCityStateZip & " " & _dtEligiblityInformation.Rows(i)("sSubscriberState").ToString()
        '                    If _dtEligiblityInformation.Rows(i)("sSubscriberZip").ToString.Trim.Length > 0 Then
        '                        _sCityStateZip = _sCityStateZip & " " & _dtEligiblityInformation.Rows(i)("sSubscriberZip").ToString()
        '                    End If
        '                End If
        '            End If
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(_sCityStateZip)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '        End If

        '        rtfEligiliblityinfo.AppendText(strSpLine)
        '    End If



        '    '\\Pharamacy eligibility information
        '    '\\ Service Date
        '    If _dtEligiblityInformation.Rows(i)("sServiceDate").ToString.Trim.Length = 8 Then
        '        'rtfEligiliblityinfo.AppendText("Service Date : " & _dtEligiblityInformation.Rows(i)("sServiceDate"))
        '        Dim _SrvDt As String = _dtEligiblityInformation.Rows(i)("sServiceDate").ToString().Substring(4, 2) & "/" & _dtEligiblityInformation.Rows(i)("sServiceDate").ToString().Substring(6, 2) & "/" & _dtEligiblityInformation.Rows(i)("sServiceDate").ToString().Substring(0, 4)
        '        'Dim culture As System.Globalization.CultureInfo
        '        'culture = New System.Globalization.CultureInfo("en-US")
        '        Dim strDt As String = DateTime.ParseExact(_SrvDt, "MM/dd/yyyy", Nothing).ToShortDateString
        '        'Dim strDt As String = DateTime.ParseExact(_SrvDt, "MM/dd/yyyy", culture).ToLongDateString
        '        'Dim SrvDt As String = format(Convert.ToDateTime(_SrvDt).
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Service Date: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(strDt)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        'textLengthAfter = rtfEligiliblityinfo.TextLength
        '        'rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        'rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 1.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        rtfEligiliblityinfo.AppendText("     ")
        '    End If

        '    '\\ Profile Status
        '    Dim sprofilestatus As String = ""
        '    'If _dtEligiblityInformation.Rows(i)(" ").ToString.Trim.Length > 0 Then
        '    textLengthBefore = rtfEligiliblityinfo.TextLength
        '    rtfEligiliblityinfo.AppendText("Profile Status: ")
        '    textLengthAfter = rtfEligiliblityinfo.TextLength
        '    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    If _dtEligiblityInformation.Rows(i)("IsSubscriberdemoChange") = "True" Or _dtEligiblityInformation.Rows(i)("IsDependentdemochange") = "True" Then
        '        sprofilestatus = "Patient Profile Modified"
        '    Else
        '        sprofilestatus = "Patient Profile Not Modified"
        '    End If
        '    textLengthBefore = rtfEligiliblityinfo.TextLength
        '    rtfEligiliblityinfo.AppendText(sprofilestatus)
        '    textLengthAfter = rtfEligiliblityinfo.TextLength
        '    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    textLengthBefore = rtfEligiliblityinfo.TextLength
        '    rtfEligiliblityinfo.AppendText(vbCrLf)
        '    rtfEligiliblityinfo.AppendText(vbCrLf)
        '    textLengthAfter = rtfEligiliblityinfo.TextLength
        '    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    ' End If

        '    '\\ group name
        '    If _dtEligiblityInformation.Rows(i)("sGroupName").ToString.Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Group Name: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        'rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sGroupName"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        'rtfEligiliblityinfo.AppendText("     ")
        '        textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '    End If
        '    '\\BIN/PCN Number
        '    If _dtEligiblityInformation.Rows(i)("sBINNumber").ToString.Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("BIN/PCN Number: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sBINNumber"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        'Else
        '        '    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        '    rtfEligiliblityinfo.AppendText(vbCrLf)
        '        '    rtfEligiliblityinfo.AppendText(vbCrLf)
        '        '    textLengthAfter = rtfEligiliblityinfo.TextLength
        '        '    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        '    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    End If
        '    '\\EmployeeID
        '    If _dtEligiblityInformation.Rows(i)("sEmployeeID").ToString.Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Employee ID: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sEmployeeID"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        ''rtfEligiliblityinfo.AppendText("     ")
        '        textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        'Else
        '        '    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        '    rtfEligiliblityinfo.AppendText(vbCrLf)
        '        '    rtfEligiliblityinfo.AppendText(vbCrLf)
        '        '    textLengthAfter = rtfEligiliblityinfo.TextLength
        '        '    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        '    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    End If


        '    If _dtEligiblityInformation.Rows(i)("sPharmacyCoverageName").ToString.Trim.Length > 0 Then
        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText("Retail Pharmacy Coverage Name: ")
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sPharmacyCoverageName"))
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        rtfEligiliblityinfo.AppendText(vbCrLf)
        '        textLengthAfter = rtfEligiliblityinfo.TextLength
        '        rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '        rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '    End If

        '    ''rtfEligiliblityinfo.AppendText(strSpLine)

        '    '\\ Pharmacy retail info
        '    If _dtEligiblityInformation.Rows(i)("sPhEligiblityorBenefitInfo").ToString.Trim.Length > 0 Then

        '        If _dtEligiblityInformation.Rows(i)("sPhEligiblityorBenefitInfo") = "Out of Pocket (Stop Loss)" Then
        '            retailoutFlag = True
        '        Else
        '            retailoutFlag = False
        '        End If

        '        If _dtEligiblityInformation.Rows(i)("sPhEligiblityorBenefitInfo") = "Active Coverage" Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Retail Pharmacy Benefit: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Covered")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        ElseIf _dtEligiblityInformation.Rows(i)("sPhEligiblityorBenefitInfo") = "Inactive" Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Retail Pharmacy Benefit: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Not Covered")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        ElseIf _dtEligiblityInformation.Rows(i)("sPhEligiblityorBenefitInfo") = "Out of Pocket (Stop Loss)" Then
        '            'retailoutFlag = True

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Retail Pharmacy Benefit: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Out of Pocket")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        ElseIf _dtEligiblityInformation.Rows(i)("sPhEligiblityorBenefitInfo") = "Cannot Process" Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Retail Pharmacy Benefit: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Cannot Process")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        End If



        '        'Medicare Flag/Insurance Type Code
        '        If _dtEligiblityInformation.Rows(i)("sRetailInsTypeCode").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Retail Pharmacy Insurance Type: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sRetailInsTypeCode"))
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        End If
        '        'retail pharmacy Monetary Amount
        '        If _dtEligiblityInformation.Rows(i)("sRetailMonetaryAmount").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Retail Pharmacy Monetary Amount: $")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))


        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sRetailMonetaryAmount"))
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '        End If

        '        ' Mail order pharmacy coverage name
        '        If _dtEligiblityInformation.Rows(i)("sMailOrderRxDrugCoverageName").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Mail Order Pharmacy Coverage Name: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sMailOrderRxDrugCoverageName"))
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'Else
        '            '    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            '    rtfEligiliblityinfo.AppendText(vbCrLf)
        '            '    rtfEligiliblityinfo.AppendText(vbCrLf)
        '            '    textLengthAfter = rtfEligiliblityinfo.TextLength
        '            '    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            '    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        End If

        '        '\\ Mail order pharmacy
        '        If _dtEligiblityInformation.Rows(i)("sMailOrdEligiblityorBenefitInfo").ToString.Trim.Length > 0 Then
        '            'If _dtEligiblityInformation.Rows(i)("sMailOrdEligiblityorBenefitInfo") = "Out of Pocket (Stop Loss)" Then
        '            '    phMailorderOutFlag = True
        '            'End If

        '            If _dtEligiblityInformation.Rows(i)("sMailOrdEligiblityorBenefitInfo") = "Active Coverage" Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Mail Order Pharmacy: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Covered")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            ElseIf _dtEligiblityInformation.Rows(i)("sMailOrdEligiblityorBenefitInfo") = "Inactive" Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Mail Order  Pharmacy: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Not Covered")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            ElseIf _dtEligiblityInformation.Rows(i)("sMailOrdEligiblityorBenefitInfo") = "Out of Pocket (Stop Loss)" Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Mail Order  Pharmacy: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Out of Pocket")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '                '  rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            ElseIf _dtEligiblityInformation.Rows(i)("sMailOrdEligiblityorBenefitInfo") = "Cannot Process" Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Mail Order  Pharmacy: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Cannot Process")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                ' rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            End If


        '        End If

        '        'Medicare Flag/Insurance Type Code
        '        If _dtEligiblityInformation.Rows(i)("sMailOrderInsTypeCode").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Mail Order Insurance Type: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sMailOrderInsTypeCode"))
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 1.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        Else
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'rtfEligiliblityinfo.AppendText("     ")
        '        End If

        '        If _dtEligiblityInformation.Rows(i)("sMailOrderMonetaryAmount").ToString.Trim.Length > 0 Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Mail Order Monetary Amount: $")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sMailOrderMonetaryAmount"))
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'Else
        '            '    rtfEligiliblityinfo.AppendText("     ")
        '        End If
        '        rtfEligiliblityinfo.AppendText(strSpLine)
        '    End If

        '    '---contract
        '    'check if the Contracted payer or Primary Payer information is present if yes then show it
        '    'Contracted Payer
        '    If _dtEligiblityInformation.Rows(i)("sIsContractedProvider").ToString.Trim.Length > 0 Then
        '        If _dtEligiblityInformation.Rows(i)("sIsContractedProvider").ToString.Trim = "Yes" Then
        '            'rtfEligiliblityinfo.AppendText(vbCrLf)
        '            'textLengthBefore = rtfEligiliblityinfo.TextLength
        '            'rtfEligiliblityinfo.AppendText("Contracted Provider Information:")
        '            'textLengthAfter = rtfEligiliblityinfo.TextLength
        '            'rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            'rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold + System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'rtfEligiliblityinfo.AppendText(vbCrLf)

        '            If _dtEligiblityInformation.Rows(i)("sContractedProviderName").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Contracted Service Provider Name: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sContractedProviderName"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            End If

        '            If _dtEligiblityInformation.Rows(i)("sContractedProviderNumber").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Contracted Service Provider Number: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '                'rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sContractedProviderNumber"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            End If
        '            If _dtEligiblityInformation.Rows(i)("sContProvRetailCoverageInfo").ToString.Trim.Length > 0 Then
        '                If _dtEligiblityInformation.Rows(i)("sContProvRetailCoverageInfo") = "Active Coverage" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Retail Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Covered")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                ElseIf _dtEligiblityInformation.Rows(i)("sContProvRetailCoverageInfo") = "Inactive" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Retail Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Not Covered")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                ElseIf _dtEligiblityInformation.Rows(i)("sContProvRetailCoverageInfo") = "Out of Pocket (Stop Loss)" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Retail Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Out of Pocket")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                ElseIf _dtEligiblityInformation.Rows(i)("sContProvRetailCoverageInfo") = "Cannot Process" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Retail Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Cannot Process")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                End If
        '            End If
        '            'COMMENTED
        '            If _dtEligiblityInformation.Rows(i)("sContProvRetailInsTypeCode").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Retail Pharmacy Insurance Type: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sContProvRetailInsTypeCode"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            End If

        '            If _dtEligiblityInformation.Rows(i)("sContProvRetailMonetaryAmt").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Retail Pharmacy Monetary Amount: $" & _dtEligiblityInformation.Rows(i)("sContProvRetailMonetaryAmt"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sContProvRetailInsTypeCode"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            End If


        '            If _dtEligiblityInformation.Rows(i)("sContProvMailOrderCoverageInfo").ToString.Trim.Length > 0 Then
        '                If _dtEligiblityInformation.Rows(i)("sContProvMailOrderCoverageInfo") = "Active Coverage" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Mail Order  Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '                    'rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Covered")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '                ElseIf _dtEligiblityInformation.Rows(i)("sContProvMailOrderCoverageInfo") = "Inactive" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Mail Order  Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Not Covered")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '                ElseIf _dtEligiblityInformation.Rows(i)("sContProvMailOrderCoverageInfo") = "Out of Pocket (Stop Loss)" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Mail Order  Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Out of Pocket")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                ElseIf _dtEligiblityInformation.Rows(i)("sContProvMailOrderCoverageInfo") = "Cannot Process" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Mail Order  Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Cannot Process")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                End If
        '            End If

        '            If _dtEligiblityInformation.Rows(i)("sContProvMailOrderInsTypeCode").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Mail Order Ins Type: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sContProvMailOrderInsTypeCode"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            End If

        '            If _dtEligiblityInformation.Rows(i)("sContProvMailOrderMonetaryAmt").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Mail Order Monetary Amount: $")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sContProvMailOrderMonetaryAmt"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            End If

        '        End If
        '        rtfEligiliblityinfo.AppendText(strSpLine)
        '    End If
        '    'Contracted Payer
        '    ' rtfEligiliblityinfo.AppendText(strSpLine)
        '    'Primary Payer
        '    If _dtEligiblityInformation.Rows(i)("sIsPrimaryPayer").ToString.Trim.Length > 0 Then
        '        If _dtEligiblityInformation.Rows(i)("sIsPrimaryPayer").ToString.Trim = "Yes" Then
        '            'rtfEligiliblityinfo.AppendText(vbCrLf)
        '            'textLengthBefore = rtfEligiliblityinfo.TextLength
        '            'rtfEligiliblityinfo.AppendText("Primary Payer Information:")
        '            'textLengthAfter = rtfEligiliblityinfo.TextLength
        '            'rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            'rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold + System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CByte(0))
        '            'rtfEligiliblityinfo.AppendText(vbCrLf)

        '            If _dtEligiblityInformation.Rows(i)("sPrimaryPayerName").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Primary Payer Name: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sPrimaryPayerName"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                rtfEligiliblityinfo.AppendText("     ")
        '            End If

        '            If _dtEligiblityInformation.Rows(i)("sPrimaryPayerNumber").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Primary Payer Number: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sPrimaryPayerNumber"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            End If
        '            If _dtEligiblityInformation.Rows(i)("sPrimaryPayerRetailCoverageInfo").ToString.Trim.Length > 0 Then
        '                If _dtEligiblityInformation.Rows(i)("sPrimaryPayerRetailCoverageInfo") = "Active Coverage" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Retail Pharmacy: Covered")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Covered")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                ElseIf _dtEligiblityInformation.Rows(i)("sPrimaryPayerRetailCoverageInfo") = "Inactive" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Retail Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Not Covered")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                ElseIf _dtEligiblityInformation.Rows(i)("sPrimaryPayerRetailCoverageInfo") = "Out of Pocket (Stop Loss)" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Retail Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Out of Pocket")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                ElseIf _dtEligiblityInformation.Rows(i)("sPrimaryPayerRetailCoverageInfo") = "Cannot Process" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Retail Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Cannot Process")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                End If
        '            End If
        '            'COMMENTED
        '            If _dtEligiblityInformation.Rows(i)("sPrimaryPayerRetailInsTypeCode").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Retail Pharmacy Ins Type: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sPrimaryPayerRetailInsTypeCode"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            End If

        '            If _dtEligiblityInformation.Rows(i)("sPrimaryPayerRetailMonetaryAmt").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Retail Pharmacy Monetary Amount: $")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sPrimaryPayerRetailMonetaryAmt"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            End If
        '            'Mail Order

        '            If _dtEligiblityInformation.Rows(i)("sPrimaryPayerMailOrderCoverageInfo").ToString.Trim.Length > 0 Then
        '                If _dtEligiblityInformation.Rows(i)("sPrimaryPayerMailOrderCoverageInfo") = "Active Coverage" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Mail Order Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Covered")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                ElseIf _dtEligiblityInformation.Rows(i)("sPrimaryPayerMailOrderCoverageInfo") = "Inactive" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Mail Order Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Not Covered")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))


        '                ElseIf _dtEligiblityInformation.Rows(i)("sPrimaryPayerMailOrderCoverageInfo") = "Cannot Process" Then
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Mail Order Pharmacy: ")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    textLengthBefore = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.AppendText("Cannot Process")
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                    'rtfEligiliblityinfo.AppendText("     ")
        '                    textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    rtfEligiliblityinfo.AppendText(vbCrLf)
        '                    textLengthAfter = rtfEligiliblityinfo.TextLength
        '                    rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                    rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                End If
        '            End If
        '            'COMMENTED

        '            If _dtEligiblityInformation.Rows(i)("sPrimaryPayerMailOrderInsTypeCode").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Mail Order Ins Type: ")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        '                ' rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sPrimaryPayerMailOrderInsTypeCode"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            End If

        '            If _dtEligiblityInformation.Rows(i)("sPrimaryPayerMailOrderMonetaryAmt").ToString.Trim.Length > 0 Then
        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText("Mail Order Monetary Amount: $")
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                textLengthBefore = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.AppendText(_dtEligiblityInformation.Rows(i)("sPrimaryPayerMailOrderMonetaryAmt"))
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '                'rtfEligiliblityinfo.AppendText("     ")
        '                textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                rtfEligiliblityinfo.AppendText(vbCrLf)
        '                textLengthAfter = rtfEligiliblityinfo.TextLength
        '                rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '                rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            End If


        '            'Retail Pharmacy
        '        End If
        '        rtfEligiliblityinfo.AppendText(strSpLine)
        '    End If
        '    'Primary Payer

        '    If _dtEligiblityInformation.Rows(i)("sMessageType").ToString.Trim.Length > 0 Then
        '        Dim MessageType As String = _dtEligiblityInformation.Rows(i)("sMessageType").ToString
        '        If MessageType.Contains("PNF") Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Notes: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Patient not found")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        ElseIf MessageType.Contains("NCP") Then
        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("Notes: ")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            textLengthBefore = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.AppendText("No contract with payer's client")
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))

        '            'rtfEligiliblityinfo.AppendText("     ")
        '            textLengthBefore = rtfEligiliblityinfo.TextLength 'Space
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            rtfEligiliblityinfo.AppendText(vbCrLf)
        '            textLengthAfter = rtfEligiliblityinfo.TextLength
        '            rtfEligiliblityinfo.Select(textLengthBefore, textLengthAfter)
        '            rtfEligiliblityinfo.SelectionFont = New System.Drawing.Font("Tahoma", 2.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        '        End If



        '    End If



        '    rtfEligiliblityinfo.Select()


        '    'Bind the Subscriber grid if the Subscriber Demographic flag is set to true

        '    Dim _Row As Integer
        '    If _dtEligiblityInformation.Rows(i)("IsSubscriberdemoChange") = "True" Or _dtEligiblityInformation.Rows(i)("IsDependentdemoChange") = "True" Then
        '        With C1271demoInfo
        '            .Rows.Add()
        '            _Row = .Rows.Count - 1
        '            .SetData(_Row, COL_ChangeMessageID, _dtEligiblityInformation.Rows(i)("sMessageID"))
        '            .SetData(_Row, COL_ChangePatientID, _dtEligiblityInformation.Rows(i)("nPatientID"))
        '            .SetData(_Row, COL_PayerName, _dtEligiblityInformation.Rows(i)("sPBM_PayerName"))
        '            .SetData(_Row, COL_DependentdemochgName, _dtEligiblityInformation.Rows(i)("DependentdemoChgName"))
        '            .SetData(_Row, COL_DependentdemoChgGender, _dtEligiblityInformation.Rows(i)("sDependentdemoChgGender"))
        '            .SetData(_Row, COL_DependentdemoChgDOB, _dtEligiblityInformation.Rows(i)("sDependentdemoChgDOB"))
        '            .SetData(_Row, COL_DependentdemoChgSSN, _dtEligiblityInformation.Rows(i)("sDependentdemoChgSSN"))
        '            .SetData(_Row, COL_DependentdemoChgAddress1, _dtEligiblityInformation.Rows(i)("sDependentdemoChgAddress1"))
        '            .SetData(_Row, COL_DependentdemoChgAddress2, _dtEligiblityInformation.Rows(i)("sDependentdemoChgAddress2"))
        '            .SetData(_Row, COL_DependentdemoChgCity, _dtEligiblityInformation.Rows(i)("sDependentdemoChgCity"))
        '            .SetData(_Row, COL_DependentdemoChgState, _dtEligiblityInformation.Rows(i)("sDependentdemoChgState"))
        '            .SetData(_Row, COL_DependentdemoChgZip, _dtEligiblityInformation.Rows(i)("sDependentdemoChgZip"))

        '            .SetData(_Row, COL_SubscriberChangeName, _dtEligiblityInformation.Rows(i)("SubscriberDemoChgName"))

        '            .SetData(_Row, COL_SubscriberChangeGender, _dtEligiblityInformation.Rows(i)("sSubscriberDemoChgGender"))
        '            .SetData(_Row, COL_SubscriberChangeDOB, _dtEligiblityInformation.Rows(i)("sSubscriberDemoChgDOB"))
        '            .SetData(_Row, COL_SubscriberChangeSSN, _dtEligiblityInformation.Rows(i)("sSubscriberDemoChgSSN"))
        '            .SetData(_Row, COL_SubscriberChangeAddress1, _dtEligiblityInformation.Rows(i)("sSubscriberDemoChgAddress1"))
        '            .SetData(_Row, COL_SubscriberChangeAddress2, _dtEligiblityInformation.Rows(i)("sSubscriberDemoChgAddress2"))
        '            .SetData(_Row, COL_SubscriberChangeCity, _dtEligiblityInformation.Rows(i)("sSubscriberDemoChgCity"))
        '            .SetData(_Row, COL_SubscriberChangeState, _dtEligiblityInformation.Rows(i)("sSubscriberDemoChgState"))
        '            .SetData(_Row, COL_SubscriberChangeZip, _dtEligiblityInformation.Rows(i)("sSubscriberDemoChgZip"))
        '            .SetData(_Row, COL_ResquestDateTimeStamp, _dtEligiblityInformation.Rows(i)("dtResquestDateTimeStamp"))
        '        End With

        '        With C1270demoInfo
        '            .Rows.Add()
        '            _Row = .Rows.Count - 1
        '            .SetData(_Row, COL_DependentName, _dtEligiblityInformation.Rows(i)("DependentName"))
        '            .SetData(_Row, COL_DependentGender, _dtEligiblityInformation.Rows(i)("sDGender"))
        '            .SetData(_Row, COL_DependentDOB, _dtEligiblityInformation.Rows(i)("sDDOB"))
        '            .SetData(_Row, COL_DependentSSN, _dtEligiblityInformation.Rows(i)("sDSSN"))
        '            .SetData(_Row, COL_DependentAddress1, _dtEligiblityInformation.Rows(i)("sDAddress1"))
        '            .SetData(_Row, COL_DependentAddress2, _dtEligiblityInformation.Rows(i)("sDAddress2"))
        '            .SetData(_Row, COL_DependentCity, _dtEligiblityInformation.Rows(i)("sDCity"))
        '            .SetData(_Row, COL_DependentState, _dtEligiblityInformation.Rows(i)("sDState"))
        '            .SetData(_Row, COL_DepenedentZip, _dtEligiblityInformation.Rows(i)("sDZip"))
        '            .SetData(_Row, COL_PaitentID, _dtEligiblityInformation.Rows(i)("nPatientID"))
        '            .SetData(_Row, COL_MessageID, _dtEligiblityInformation.Rows(i)("sMessageID"))
        '            .SetData(_Row, COL_ResquestDateTimeStamp, _dtEligiblityInformation.Rows(i)("dtResquestDateTimeStamp"))

        '            .SetData(_Row, COL_SuscriberName, _dtEligiblityInformation.Rows(i)("SubscriberName"))
        '            .SetData(_Row, COL_SubscriberGender, _dtEligiblityInformation.Rows(i)("sSubscriberGender"))
        '            .SetData(_Row, COL_SubscriberDOB, _dtEligiblityInformation.Rows(i)("sSubscriberDOB"))
        '            .SetData(_Row, COL_SubscriberSSN, _dtEligiblityInformation.Rows(i)("sSubscriberSSN"))
        '            .SetData(_Row, COL_SubscriberAddress1, _dtEligiblityInformation.Rows(i)("sSubscriberAddress1"))
        '            .SetData(_Row, COL_SubscriberAddress2, _dtEligiblityInformation.Rows(i)("sSubscriberAddress2"))
        '            .SetData(_Row, COL_SubscriberCity, _dtEligiblityInformation.Rows(i)("sSubscriberCity"))
        '            .SetData(_Row, COL_SubscriberState, _dtEligiblityInformation.Rows(i)("sSubscriberState"))
        '            .SetData(_Row, COL_SubscriberZip, _dtEligiblityInformation.Rows(i)("sSubscriberZip"))

        '        End With
        '    Else


        '    End If
        'Next

        ''------to set the scroll bar to top if there is more data binded to the control
        'If rtfEligiliblityinfo.Text.Trim.Length > 0 Then
        '    rtfEligiliblityinfo.Select(0, 0)
        '    rtfEligiliblityinfo.ScrollToCaret()
        'End If
        ''------

        'With C1271demoInfo
        '    Dim IsFound As Boolean = False
        '    For i As Integer = 0 To .Cols.Count - 1
        '        IsFound = False
        '        For j As Integer = 1 To .Rows.Count - 1
        '            If C1271demoInfo.GetData(j, i).ToString().Trim() <> "" Then
        '                IsFound = True
        '                Exit For
        '            End If
        '        Next
        '        If IsFound = False Then
        '            .Cols(i).Visible = False
        '        End If
        '    Next
        'End With
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub


    ''' <summary>
    ''' returns eligibility date (20100801) or service date (20100801-20991231)  date in MM/dd/yyyy format
    ''' </summary>
    ''' <param name="EligibilityDt"></param>
    ''' <param name="ServiceDt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Private Function GetEDIFormattedDate(ByVal EligibilityDt As String, Optional ByVal ServiceDt As String = "")
        Dim sFormattedDate As String = ""
        Dim _Dt As String = ""
        Dim strDt As String = ""
        Dim sServDtRannge As String() = Nothing
        Try


            If EligibilityDt.Length >= 8 Then ''20100801
                _Dt = EligibilityDt.Substring(4, 2) & "/" & EligibilityDt.Substring(6, 2) & "/" & EligibilityDt.Substring(0, 4)
                strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString

                sFormattedDate = strDt

                Return sFormattedDate
            ElseIf ServiceDt.Length >= 17 Then ''20100801-20991231
                sServDtRannge = Split(ServiceDt, "-")

                Dim sFromDt As String = ""
                Dim sToDt As String = ""



                sFromDt = sServDtRannge(0)
                _Dt = sFromDt.Substring(4, 2) & "/" & sFromDt.Substring(6, 2) & "/" & sFromDt.Substring(0, 4)
                strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
                sFromDt = strDt

                sToDt = sServDtRannge(1)
                _Dt = sToDt.Substring(4, 2) & "/" & sToDt.Substring(6, 2) & "/" & sToDt.ToString.Substring(0, 4)
                strDt = DateTime.ParseExact(_Dt, "MM/dd/yyyy", Nothing).ToShortDateString
                sToDt = strDt

                sFormattedDate = sFromDt & " - " & sToDt
                'SLR: sServDtRange.Clear and then
                If Not sServDtRannge Is Nothing Then                    
                    sServDtRannge = Nothing
                End If


                Return sFormattedDate

            End If
            Return sFormattedDate
        Catch ex As Exception
            Return sFormattedDate
        Finally
            sServDtRannge = Nothing
        End Try
    End Function


    Private Function GetEDIErrorDescription(ByVal EDIErrorCode As String)
        Dim sErrDesc As String = ""
        Dim sErrorMessage() As String = Nothing
        Try
            sErrorMessage = Split(EDIErrorCode, "|")
            If sErrorMessage.Length >= 2 Then
                sErrDesc = gloRxHub.ClsgloRxHubGeneral.getRejecttionDescription(sErrorMessage(1).ToString, "", "")
            End If
            sErrorMessage = Nothing

            Return sErrDesc
        Catch ex As Exception
            sErrorMessage = Nothing
            Return sErrDesc
        End Try
    End Function



    'Public Sub Set270GridStyle()
    '    With C1270demoInfo
    '        .Rows.Count = 1
    '        .Rows.Fixed = 1
    '        .Cols.Count = COl_Count
    '        Dim _TotalWidth As Integer
    '        _TotalWidth = .Width / 10
    '        .SetData(0, COL_PaitentID, "PatientID")
    '        .Cols(COL_PaitentID).Width = 0
    '        .Cols(COL_PaitentID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_MessageID, "MessageID")
    '        .Cols(COL_MessageID).Width = 0
    '        .Cols(COL_MessageID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_PayerName, "Payer Name")
    '        .Cols(COL_PayerName).Width = 141
    '        .Cols(COL_PayerName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_DependentName, "Dependent Name")
    '        .Cols(COL_DependentName).Width = 141
    '        .Cols(COL_DependentName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_DependentGender, "Dependent Gender")
    '        .Cols(COL_DependentGender).Width = 141
    '        .Cols(COL_DependentGender).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


    '        .SetData(0, COL_DependentDOB, "Dependent DOB")
    '        .Cols(COL_DependentDOB).Width = 141
    '        .Cols(COL_DependentDOB).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


    '        .SetData(0, COL_DependentSSN, "Dependent SSN")
    '        .Cols(COL_DependentSSN).Width = 141
    '        .Cols(COL_DependentSSN).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_DependentAddress1, "Dependent Address1")
    '        .Cols(COL_DependentAddress1).Width = 141
    '        .Cols(COL_DependentAddress1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_DependentAddress2, "Dependent Address2")
    '        .Cols(COL_DependentAddress2).Width = 141
    '        .Cols(COL_DependentAddress2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_DependentCity, "Dependent City")
    '        .Cols(COL_DependentCity).Width = 141
    '        .Cols(COL_DependentCity).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_DependentState, "Dependent State")
    '        .Cols(COL_DependentState).Width = 141
    '        .Cols(COL_DependentState).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_DepenedentZip, "Dependent Zip")
    '        .Cols(COL_DepenedentZip).Width = 141
    '        .Cols(COL_DepenedentZip).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_ResquestDateTimeStamp, "Request Date-Time")
    '        .Cols(COL_ResquestDateTimeStamp).Width = 141
    '        .Cols(COL_ResquestDateTimeStamp).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_SuscriberName, "Subscriber Name")
    '        .Cols(COL_SuscriberName).Width = 141
    '        .Cols(COL_SuscriberName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_SubscriberDOB, "Subscriber DOB")
    '        .Cols(COL_SubscriberDOB).Width = 141
    '        .Cols(COL_SubscriberDOB).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_SubscriberGender, "Subscriber Gender")
    '        .Cols(COL_SubscriberGender).Width = 141
    '        .Cols(COL_SubscriberGender).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_SubscriberSSN, "Subscriber SSN")
    '        .Cols(COL_SubscriberSSN).Width = 141
    '        .Cols(COL_SubscriberSSN).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_SubscriberAddress1, "Subscriber Address1")
    '        .Cols(COL_SubscriberAddress1).Width = 141
    '        .Cols(COL_SubscriberAddress1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_SubscriberAddress2, "Subscriber Address2")
    '        .Cols(COL_SubscriberAddress2).Width = 141
    '        .Cols(COL_SubscriberAddress2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_SubscriberCity, "Subscriber City")
    '        .Cols(COL_SubscriberCity).Width = 141
    '        .Cols(COL_SubscriberCity).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_SubscriberState, "Subscriber State")
    '        .Cols(COL_SubscriberState).Width = 141
    '        .Cols(COL_SubscriberState).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        .SetData(0, COL_SubscriberZip, "Subscriber Zip")
    '        .Cols(COL_SubscriberZip).Width = 141
    '        .Cols(COL_SubscriberZip).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '    End With

    'End Sub

    Public Sub Set271GridStyle()
        With C1271demoInfo
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COl_Count

            Dim _TotalWidth As Integer
            _TotalWidth = .Width / 10

            .SetData(0, COL_PaitentID, "PatientID")
            .Cols(COL_PaitentID).Width = 0
            .Cols(COL_PaitentID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_MessageID, "MessageID")
            .Cols(COL_MessageID).Width = 0
            .Cols(COL_MessageID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_PayerName, "Payer Name")
            .Cols(COL_PayerName).Width = 141
            .Cols(COL_PayerName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_DependentName, "Dependent Name")
            .Cols(COL_DependentName).Width = 141
            .Cols(COL_DependentName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_DependentGender, "Dependent Gender")
            .Cols(COL_DependentGender).Width = 141
            .Cols(COL_DependentGender).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_DependentDOB, "Dependent DOB")
            .Cols(COL_DependentDOB).Width = 141
            .Cols(COL_DependentDOB).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


            .SetData(0, COL_DependentSSN, "Dependent SSN")
            .Cols(COL_DependentSSN).Width = 141
            .Cols(COL_DependentSSN).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_DependentAddress1, "Dependent Address1")
            .Cols(COL_DependentAddress1).Width = 141
            .Cols(COL_DependentAddress1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_DependentAddress2, "Dependent Address2")
            .Cols(COL_DependentAddress2).Width = 141
            .Cols(COL_DependentAddress2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_DependentCity, "Dependent City")
            .Cols(COL_DependentCity).Width = 141
            .Cols(COL_DependentCity).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_DependentState, "Dependent State")
            .Cols(COL_DependentState).Width = 141
            .Cols(COL_DependentState).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_DepenedentZip, "Dependent Zip")
            .Cols(COL_DepenedentZip).Width = 141
            .Cols(COL_DepenedentZip).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_ResquestDateTimeStamp, "Request Date-Time")
            .Cols(COL_ResquestDateTimeStamp).Width = 141
            .Cols(COL_ResquestDateTimeStamp).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_SuscriberName, "Subscriber Name")
            .Cols(COL_SuscriberName).Width = 141
            .Cols(COL_SuscriberName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_SubscriberDOB, "Subscriber DOB")
            .Cols(COL_SubscriberDOB).Width = 141
            .Cols(COL_SubscriberDOB).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_SubscriberGender, "Subscriber Gender")
            .Cols(COL_SubscriberGender).Width = 141
            .Cols(COL_SubscriberGender).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_SubscriberSSN, "Subscriber SSN")
            .Cols(COL_SubscriberSSN).Width = 141
            .Cols(COL_SubscriberSSN).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_SubscriberAddress1, "Subscriber Address1")
            .Cols(COL_SubscriberAddress1).Width = 141
            .Cols(COL_SubscriberAddress1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_SubscriberAddress2, "Subscriber Address2")
            .Cols(COL_SubscriberAddress2).Width = 141
            .Cols(COL_SubscriberAddress2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_SubscriberCity, "Subscriber City")
            .Cols(COL_SubscriberCity).Width = 141
            .Cols(COL_SubscriberCity).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_SubscriberState, "Subscriber State")
            .Cols(COL_SubscriberState).Width = 141
            .Cols(COL_SubscriberState).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .SetData(0, COL_SubscriberZip, "Subscriber Zip")
            .Cols(COL_SubscriberZip).Width = 141
            .Cols(COL_SubscriberZip).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

        End With

    End Sub


    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "SaveandClose"

            Case "Close"
                Me.Close()
        End Select
    End Sub

    Private Sub C1271demoInfo_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1271demoInfo.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Public Function GenerateFileName(Optional ByVal PatientCode As String = "") As String
        Dim strfilename As String = ""
        strfilename = "rxelg_"
        Dim dtdate As DateTime = Date.UtcNow
        Dim strtemp As String
        If PatientCode = "" Then
            strtemp = strfilename & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
        Else
            strtemp = strfilename & PatientCode & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
        End If

        strfilename = gloSettings.FolderSettings.AppTempFolderPath & strtemp & ".xml"

        Return strfilename
    End Function
End Class