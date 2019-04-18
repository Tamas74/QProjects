Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports gloEMRGeneralLibrary
Imports gloGlobal
'Imports gloEMR.gloEMRWord

Public Class frmInfoDocument
    Private WithEvents _PatientStrip As gloUserControlLibrary.gloUC_PatientStrip
    Private WithEvents tlsPatientEducation As WordToolStrip.gloWordToolStrip

    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _dataBaseConnectionString As String = ""
    Private _LoginProviderId As Int64 = 0
    Private _LoginUserID As Int64 = 0
    Private _ClinicID As Long = 1
    Private _PatientProviderID As Int64 = 0
    Private _LabProviderID As Int64 = 0
    Private _visitID As Long = 0
    Private _DocumentName As String
    Private Source As Integer
    Private ResourceCategory As Integer
    Private ResourceType As Integer

    Private _blnClosed As Boolean = False
    Private blnSaved As [Boolean] = True
    Private IsClosed As Boolean = False
    Private gstrMessageBoxCaption As [String] = String.Empty

    Public PatientID As Long
    Public nTemplateID As Long
    Public TemplateName As String
    Public isHandlerRemoved As Boolean = True

    Private objCriteria As gloEMRWord.DocCriteria
    Private oWord As gloEMRWord.clsWordDocument

    Private oCurDoc As Wd.Document
    Private oTempDoc As Wd.Document
    Private oWordApp As Wd.Application
    Friend wdTemp As AxDSOFramer.AxFramerControl
    Private clsinfobutton_Infodocument As New gloEMRGeneralLibrary.clsInfobutton()






#Region "Yatin -> Added new Constructer for opening Educaion form from Problem List"

    Public Sub New(ByVal nTempID As Int64, ByVal nPatientID As Long, ByVal openFor As String)
        MyBase.New()

        InitializeComponent()
        nTemplateID = nTempID
        PatientID = nPatientID
        Dim strTemp() = openFor.Split("-")
        If strTemp.Length > 0 Then
            Me.Text = strTemp(0)
            TemplateName = strTemp(1)
        End If
        Me.Text = openFor
      
        If openFor = "Provider Reference Material" Then
            ResourceType = clsInfobutton.enumResourceType.ProviderReferenceMaterial
        ElseIf openFor = "Patient Reference Material" Then
            ResourceType = clsInfobutton.enumResourceType.PatientReferenceMaterial
        End If

        ResourceCategory = clsInfobutton.enumResourceCategory.InternalLibrary

        Me.WindowState = FormWindowState.Normal
        If appSettings IsNot Nothing Then
            _dataBaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
            _LoginUserID = Convert.ToInt64(appSettings("UserID"))
        End If

        If appSettings("MessageBOXCaption") IsNot Nothing Then
            If appSettings("MessageBOXCaption") <> "" Then
                gstrMessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                gstrMessageBoxCaption = "gloEMR"
            End If
        Else
            gstrMessageBoxCaption = "gloEMR"
        End If

    End Sub

#End Region

    Private Sub frmInfoDocument_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            If Not (IsNothing(wdPatientEducation)) Then
                If Not (IsNothing(wdPatientEducation.DocumentName)) Then
                    If Not (IsNothing(wdPatientEducation.ActiveDocument)) Then
                        oCurDoc = wdPatientEducation.ActiveDocument
                        oWordApp = oCurDoc.Application
                        Try
                            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        Catch ex As Exception

                        End Try

                        Try
                            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        Catch ex As Exception

                        End Try

                        isHandlerRemoved = False
                        oCurDoc.ActiveWindow.SetFocus()
                        wdPatientEducation.Focus()
                    End If
                End If
            End If


        Catch ex As Exception
        End Try
        For Each myForm As Form In Application.OpenForms
            If (myForm.TopMost) Then
                myForm.TopMost = False
            End If
        Next
        Me.TopMost = True
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmInfoDocument_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
     Try
            If Not oWordApp Is Nothing Then
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                isHandlerRemoved = True
                'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick  for oWordApp", gloAuditTrail.ActivityOutCome.Success)
            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Me.TopMost = False
    End Sub

    Private Sub frmInfoDocument_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If IsNothing(oCurDoc) = False Then
            If oCurDoc.Saved = False Then
                Dim Result As DialogResult
                Result = MessageBox.Show("Do you want to save the changes to " + Me.Text + "?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Result = Windows.Forms.DialogResult.Yes Then
                    ' SaveExamEducation(True, True)
                ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                ElseIf Result = Windows.Forms.DialogResult.No Then
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Patient Education viewed", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    e.Cancel = False
                End If
            End If
        End If
    End Sub

    Private Sub frmInfoDocument_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadPatientStrip()

        pnlSelectedTemplate.Visible = False
        _visitID = GetVisitID(System.DateTime.Now.Date, PatientID)
        LoadEducationDocument()
    End Sub

    Public ReadOnly Property ExamNewDocumentName() As String
        Get

            'Dim _Path As String = gloSettings.FolderSettings.AppTempFolderPath
            'Dim _NewDocumentName As String = ""
            'Dim _Extension As String = ".docx"
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            'Dim i As Integer = 0
            '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & _Extension
            'While File.Exists(_Path & "\" & _NewDocumentName) = True
            '    i = i + 1
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & "-" & i & _Extension
            'End While
            'Return _Path & "\" & _NewDocumentName
            Return gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".docx", "MMddyyyyHHmmssffff")
        End Get
    End Property

    Private Sub LoadEducationDocument()

        Dim dtVisit As DataTable = clsinfobutton_Infodocument.GetLastVisitedDocument(PatientID, nTemplateID)
        _DocumentName = ExamNewDocumentName
        oWord = New gloEMRWord.clsWordDocument


        If dtVisit.Rows.Count > 0 Then
            _DocumentName = oWord.GenerateFile(dtVisit.Rows(0)("sPENotes"), _DocumentName)
        Else
            Dim dtTemplate As DataTable = clsinfobutton_Infodocument.GeteducationTemplate(nTemplateID)
            _DocumentName = oWord.GenerateFile(dtTemplate.Rows(0)("sDescription"), _DocumentName)
        End If

        oWord = Nothing
        LoadWordUserControl(_DocumentName, False)
        oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)


    End Sub

    Private Sub tlsPatientEducation_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsPatientEducation.ToolStripClick
        Try

            Select Case e.ClickedItem.Name
                Case "Mic"
                    ''UpdateVoiceLog("SwitchOff Mic started from tlsReferrals_ItemClicked in Referrals is invoked")
                    'If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                    '    MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                    '    e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                    '    e.ClickedItem.ToolTipText = "Microphone Off"
                    '    e.ClickedItem.Text = "Mic"
                    'ElseIf MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                    '    MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                    '    e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                    '    e.ClickedItem.ToolTipText = "Microphone On"
                    '    e.ClickedItem.Text = "Mic"
                    'End If
                Case "Save"
                    SaveInformationDocument()
                Case "Save & Close"
                    SaveInformationDocument()
                    Me.Close()
                Case "Print"
                    Call PrintEducationMaterial()
                Case "FAX"
                    Call FaxEducationMaterial()
                Case "Insert Sign"
                    Call InsertProviderSignature()
                    If IsNothing(oCurDoc) = False Then
                        If gnLoginProviderID > 0 Then
                            InsertProviderSignature(gnLoginProviderID)
                        Else
                            InsertUserSignature()
                        End If
                    End If
                Case "Insert Associated Provider Signature"
                    If IsNothing(oCurDoc) = False Then
                        InsertProviderSignature()
                    End If
                Case "Insert CoSign"
                    Call InsertCoSignature()
                    'Case "Capture Sign"
                    '    Call InsertSignature()
                Case "Undo"
                    Call Undo()
                Case "Redo"
                    Call Redo()
                Case "Insert File"
                    ImportDocument(1)
                Case "Scan Documents"
                    ImportDocument(2)
                Case "tblbtn_StrikeThrough"
                    InsertStrike()
                Case "Close"
                    Me.Close()
                    'Case "&Close"
                    'If IsNothing(oCurDoc) = False Then
                    '    If oCurDoc.Saved = False Or _isEducationChanged Then
                    '        Dim Result As Int16
                    '        Result = MessageBox.Show("Do you want to save the changes to Patient Education?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    '        If Result = Windows.Forms.DialogResult.Yes Then
                    '            Call SaveExamEducation(True)
                    '            Me.Close()
                    '        ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                    '            '' Nothing to here
                    '        ElseIf Result = Windows.Forms.DialogResult.No Then
                    '            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Patient Education viewed", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    '            wdPatientEducation.Close()
                    '            Me.Close()
                    '        End If
                    '    Else
                    '        wdPatientEducation.Close()
                    '        Me.Close()
                    '    End If
                    'Else
                    '    Me.Close()
                    'End If
                    Me.Close()
                Case "Export"
                    Dim objword1 As gloEMRWord.clsWordDocument
                    objword1 = New gloEMRWord.clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", "Patient Education")
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    Result = Nothing
                    objword1 = Nothing

                Case "SecureMsg"
                    'If strProviderDirectAddress <> "" Then
                    '    Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(m_patientID)
                    '    If sError <> "" Then
                    '        MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                    '        Return
                    '    Else
                    '        Call SendSecureMsg()
                    '    End If
                    'Else
                    '    MessageBox.Show("Direct Address Not Set To Login Provider. Please Set Direct Address From gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' Load patient Strip details
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub LoadPatientStrip()
        _PatientStrip = New gloUserControlLibrary.gloUC_PatientStrip
        _PatientStrip.ShowDetail(PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.PatientEducation)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.BringToFront()
        '  _PatientStrip.Padding = New Padding(0, 0, 0, 3)
        'Me.Controls.Add(_PatientStrip)
        pnlWord.Controls.Add(_PatientStrip)
        'PnlToolStrip.Controls.Add(_PatientStrip)
        loadToolStrip()

    End Sub

    Private Sub loadToolStrip()

        If Not tlsPatientEducation Is Nothing Then
            tlsPatientEducation.Dispose()
        End If

        tlsPatientEducation = New WordToolStrip.gloWordToolStrip
        tlsPatientEducation.Dock = DockStyle.Top
        tlsPatientEducation.ConnectionString = GetConnectionString()
        tlsPatientEducation.UserID = gnLoginID
        ''Integrated ON 20101020 BY Mayuri FOR SIGNATURE
        tlsPatientEducation.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider
        tlsPatientEducation.ptProvider = oclsProvider.GetPatientProviderName(PatientID)
        tlsPatientEducation.ptProviderId = oclsProvider.GetPatientProvider(PatientID)
        'Change made to solve memory Leak and word crash issue
        oclsProvider.Dispose()
        oclsProvider = Nothing

        ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE

        ' tlsPatientEducation.IsCoSignEnabled = gblnCoSignFlag
        tlsPatientEducation.FormType = WordToolStrip.enumControlType.PatientEducation

        Me.Controls.Add(tlsPatientEducation)
        Me.PnlToolStrip.Controls.Add(tlsPatientEducation)
        Me.PnlToolStrip.Size = New System.Drawing.Size(940, 56)
        PnlToolStrip.SendToBack()
        'If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
        '    If Not IsNothing(ogloVoice) Then
        '        ogloVoice.MyWordToolStrip = tlsPatientEducation
        '        ShowMicroPhone()
        '    End If
        'End If
        'If gblnAssociatedProviderSignature Then
        '    tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
        '    tlsPatientEducation.MyToolStrip.ButtonsToHide.Remove(tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        'Else
        '    tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
        '    If (tlsPatientEducation.MyToolStrip.ButtonsToHide.Contains(tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
        '        tlsPatientEducation.MyToolStrip.ButtonsToHide.Add(tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        '    End If

        'End If
        ' '''' Check Secure Messaging is enable and User has rights to access it
        'If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
        '    If tlsPatientEducation.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
        '        If (tlsPatientEducation.MyToolStrip.ButtonsToHide.Contains(tlsPatientEducation.MyToolStrip.Items("SecureMsg").Name) = False) Then
        '            tlsPatientEducation.MyToolStrip.ButtonsToHide.Add(tlsPatientEducation.MyToolStrip.Items("SecureMsg").Name)
        '        End If
        '    End If

        '    If tlsPatientEducation.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
        '        tlsPatientEducation.MyToolStrip.Items("SecureMsg").Visible = False
        '    End If
        'End If
    End Sub

    Private Function AddChildMenu() As DataTable
        Try
            Dim oProvider As New clsProvider
            Dim rslt As Boolean

            rslt = oProvider.CheckSignDelegateStatus()

            If rslt Then
                Dim dt As DataTable
                dt = oProvider.GetAllAssignProviders(gnLoginID)
                oProvider.Dispose()
                oProvider = Nothing
                If dt.Rows.Count > 0 Then
                    Return dt
                Else
                    Return Nothing
                End If
            Else
                oProvider.Dispose()
                oProvider = Nothing
                Return Nothing
            End If

            oProvider.Dispose()
            oProvider = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Sub InsertCoSignature()
        Try
            If oCurDoc Is Nothing Then
                Return
            End If
            Dim objWord As New gloEMRWord.clsWordDocument()
            Dim objCriteria As New gloEMRWord.DocCriteria()
            objCriteria.DocCategory = gloEMRWord.enumDocCategory.Others
            objCriteria.PatientID = PatientID
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = _LoginUserID
            '' For inserting coSignature
            objWord.DocumentCriteria = objCriteria

            Dim ImagePath As String = ""
            ImagePath = objWord.getImagePath("User_MST.imgSignature", "Co-Signature")
            objCriteria = Nothing
            objWord = Nothing
            ImagePath = Strings.Mid(ImagePath, 1, Strings.Len(ImagePath) - 2)

            If System.IO.File.Exists(ImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New gloEMRWord.clsWordDocument()
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(ImagePath)
                oWord = Nothing
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Strings.Format(DateAndTime.Now, "MM/dd/yyyy") & " " & Strings.Format(DateAndTime.Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Co-Signature inserted", PatientID, 0, _
                 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Public Sub InsertUserSignature()
        Try
            If oCurDoc Is Nothing Then
                Return
            End If
            Dim objWord As New gloEMRWord.clsWordDocument()
            Dim objCriteria As New gloEMRWord.DocCriteria()
            objCriteria.DocCategory = gloEMRWord.enumDocCategory.Exam
            objCriteria.PatientID = PatientID
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = _LoginUserID
            objWord.DocumentCriteria = objCriteria
            Dim ImagePath As [String]
            Dim dtTable As New DataTable()
            ImagePath = objWord.getImagePath("User_MST.imgSignature", "Provider Signature")
            objCriteria = Nothing
            objWord = Nothing
            ImagePath = Strings.Mid(ImagePath, 1, Strings.Len(ImagePath) - 2)

            If File.Exists(ImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New gloEMRWord.clsWordDocument()
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(ImagePath)
                oWord = Nothing
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & GetLoginUserName(_LoginUserID) & "'. " & Strings.Format(DateAndTime.Now, "MM/dd/yyyy") & " " & Strings.Format(DateAndTime.Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Info Document", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        Try
            If oCurDoc Is Nothing Then
                Return
            End If
            Dim objWord As New gloEMRWord.clsWordDocument()
            Dim objCriteria As New gloEMRWord.DocCriteria()
            objCriteria.DocCategory = gloEMRWord.enumDocCategory.Exam
            objCriteria.PatientID = PatientID
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = _LoginUserID
            objCriteria.ProviderID = _LoginProviderId
            GetLoginUserName(_LoginUserID)
            objWord.DocumentCriteria = objCriteria
            'Dim pSign As String() = objWord.GetProviderSignature(ProviderID, _patientID, GetVisitID(System.DateTime.Now, _patientID), blnSignClick)
            objCriteria = Nothing
            objWord = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", PatientID, 0, _
                     0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            'If pSign(2) = "1" Then
            '    If File.Exists(pSign(0)) Then
            '        oCurDoc.ActiveWindow.SetFocus()
            '        Dim oWord As New gloEMRWord.clsWordDocument()
            '        oWord.CurDocument = oCurDoc
            '        oWord.InsertImage(pSign(0))
            '        oWord = Nothing
            '        Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
            '        If wdRng.Tables.Count > 0 Then
            '            'oCurDoc.Application.Selection.Move(1)
            '            oCurDoc.Application.Selection.EndKey()
            '        End If
            '        oCurDoc.Application.Selection.TypeParagraph()
            '        oCurDoc.Application.Selection.TypeText(Text:=pSign(1))
            '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Signature Inserted", 0, 0, _
            '         0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '    End If
            'End If
        Catch objErr As Exception
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub Undo()
        Try
            If (oCurDoc Is Nothing) = False Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Redo()
        Try
            If (oCurDoc Is Nothing) = False Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub InsertStrike()
        Try
            Dim strThrough As String = Nothing
            If (oCurDoc IsNot Nothing) Then
                If (oCurDoc.Application.Selection IsNot Nothing) Then
                    If oCurDoc.Application.Selection.Characters.Count - 1 > 0 Then
                        strThrough = "Strikethrough by " & GetLoginUserName(_LoginUserID) & " on " & Strings.Format(DateAndTime.Now, "MM/dd/yyyy") & " " & Strings.Format(DateAndTime.Now, "Medium Time")
                        If oCurDoc.Application.ActiveDocument.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyComments Then
                            oCurDoc.Application.ActiveDocument.Unprotect()
                        End If
                        oCurDoc.Application.Selection.Range.Font.DoubleStrikeThrough = 1
                        oCurDoc.Application.Selection.Move(1)
                        oCurDoc.Application.Selection.TypeParagraph()
                        oCurDoc.Application.Selection.Font.DoubleStrikeThrough = 0
                        oCurDoc.Application.Selection.TypeText(Text:=strThrough)
                        oCurDoc.Application.Selection.Move(1)
                        oCurDoc.Application.Selection.TypeParagraph()
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PrintEducationMaterial()
        If (oCurDoc IsNot Nothing) Then
            GeneratePrintFaxDocument(True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Patient order printed", PatientID, 0, _
             0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If
    End Sub

    Private Sub FaxEducationMaterial()
        If (oCurDoc IsNot Nothing) Then
            GeneratePrintFaxDocument(False)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Patient order printed", PatientID, 0, _
             0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If
    End Sub

    Private Sub GeneratePrintFaxDocument(Optional ByVal IsPrintFlag As Boolean = True)
        Try
            Dim clsLabWord As New gloEMRWord.clsWordDocument()
            If clsLabWord.CheckWordForException() = False Then
                Return
            End If
            Dim _SaveFlag As Boolean = False
            If oCurDoc.Saved Then
                _SaveFlag = True
            End If

            Dim sFileName As String = ExamNewDocumentName
            oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            wdPatientEducation.Close()
            wdTemp = New AxDSOFramer.AxFramerControl()
            Me.Controls.Add(wdTemp)
            Try
                wdTemp.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoKeepUIActiveOnAppDeactive
                wdTemp.FrameHookPolicy = DSOFramer.dsoFrameHookPolicy.dsoSetOnFirstOpen
            Catch ex As Exception

            End Try

            wdTemp.Open(sFileName)

            gloWord.LoadAndCloseWord.OpenDSO(wdTemp, sFileName, oTempDoc, oWordApp)

            oTempDoc = DirectCast(wdTemp.ActiveDocument, Wd.Document)
            If IsPrintFlag Then
                If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                    oTempDoc.Unprotect()
                End If
                Dim oPrint As New clsPrintFAX()
                oPrint.PrintDoc(oTempDoc, PatientID)

                oPrint = Nothing
            Else
                FaxOrder(oTempDoc)
            End If
            wdTemp.Close()
            'Memory Leak
            Me.Controls.Remove(wdTemp)
            wdTemp.Dispose()

            LoadWordUserControl(sFileName, False)
            ''Set Cursor at start Postion of Documents
            If Not IsNothing(oCurDoc) Then
                oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
            End If
        Catch ex As Exception
        Finally
            If Not IsNothing(oCurDoc) Then
                oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            End If
        End Try
    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        Dim ObjWord As New gloEMRWord.clsWordDocument()
        Dim objCriteria As New gloEMRWord.DocCriteria()

        wdPatientEducation.Open(strFileName)

        gloWord.LoadAndCloseWord.OpenDSO(wdPatientEducation, strFileName, oCurDoc, oWordApp)

        If blnGetData Then
            objCriteria.DocCategory = gloEMRWord.enumDocCategory.Orders
            objCriteria.PatientID = PatientID
            objCriteria.VisitID = GetVisitID(System.DateTime.Now, PatientID)
            objCriteria.PrimaryID = 0
            ObjWord.DocumentCriteria = objCriteria
            ObjWord.CurDocument = oCurDoc
            ObjWord.GetFormFieldData(gloEMRWord.enumDocType.None)
            oCurDoc = ObjWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objCriteria = Nothing
        Else
            ObjWord.CurDocument = oCurDoc
            ObjWord.HighlightColor()
            oCurDoc = ObjWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            ObjWord = Nothing
        End If
        'SetWordObject(False) '' COMMENT BY SUDHIR 20090529 '' IT WAS ALLOWING TO EDIT EVEN IF FINISHED ''
        ' SetWordObject(blnIsFinish);
    End Sub

    Private Sub FaxOrder(ByVal oTempDoc As Wd.Document)
        If mdlFAX.RetrieveFAXDetails(mdlFAX.enmFAXType.PatientOrders, PatientID, "", "", "Test Order", 0, GetVisitID(System.DateTime.Now, PatientID), 0) = False Then
            Return
        End If
        mdlFAX.CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
        'Unprotoct the document
        If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
            oTempDoc.Unprotect()
        End If
        'Send the document for Printing i.e. to generate the TIFF File
        Dim objPrintFAX As New clsPrintFAX()
        If objPrintFAX.FAXDocument(oTempDoc, PatientID, mdlFAX.gstrFAXContactPerson, mdlFAX.gstrFAXContactPersonFAXNo, GetLoginUserName(_LoginUserID), System.DateTime.Now, _
         "Test Order", clsPrintFAX.enmFAXType.PatientOrders, True, True, True, Me) = False Then
            If Not String.IsNullOrEmpty(Strings.Trim(objPrintFAX.ErrorMessage)) Then
                MessageBox.Show("Unable to send the FAX due to " + objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        objPrintFAX = Nothing
    End Sub

    Public Function GetVisitID(ByVal VisitDate As System.DateTime, ByVal patientID As Long) As Long
        Dim _retvisitID As Long = 0
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(_dataBaseConnectionString)
        Dim oDBparams As New gloDatabaseLayer.DBParameters()
        Dim objRet As Object = Nothing
        Try
            oDBLayer.Connect(False)
            oDBparams.Clear()

            oDBparams.Add("@visitDate", VisitDate, ParameterDirection.Input, SqlDbType.DateTime)
            oDBparams.Add("@PatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt, 18)

            objRet = oDBLayer.ExecuteScalar("gsp_GetVisitID", oDBparams)
            If objRet IsNot Nothing Then
                _retvisitID = Convert.ToInt64(objRet)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If oDBLayer IsNot Nothing Then
                oDBLayer.Dispose()
            End If
        End Try
        Return _retvisitID
    End Function

    Public Function GetLoginUserName(ByVal UserID As Int64) As String

        Dim oDB As New gloDatabaseLayer.DBLayer(_dataBaseConnectionString)
        Dim _strLoginUserName As String = String.Empty
        Try
            oDB.Connect(False)
            'ProID = Trim(oDB.ExecuteScaler)
            _strLoginUserName = Convert.ToString(oDB.ExecuteScalar_Query("Select sLoginName from dbo.User_MST where nUserID =" & Convert.ToString(UserID) & ""))
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)

            _strLoginUserName = ""
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
        Return _strLoginUserName
    End Function

    Private Function SaveInformationDocument()
        Try


            Dim TemplateResult As Object
            wdPatientEducation.Save(_DocumentName, True, "", "")
            oCurDoc = Nothing
            wdPatientEducation.Close()
            oWord = New gloEMRWord.clsWordDocument()
            TemplateResult = CType(oWord.ConvertFiletoBinary(_DocumentName), Object)

            oWord = Nothing

            wdPatientEducation.Open(_DocumentName)

            gloWord.LoadAndCloseWord.OpenDSO(wdPatientEducation, _DocumentName, oCurDoc, oWordApp)

            clsinfobutton_Infodocument.SavePatientEducation(_visitID, PatientID, nTemplateID, TemplateResult, TemplateName, clsInfobutton.enumSource.Orders, ResourceCategory, ResourceType, "")
        Catch ex As Exception

        End Try
        Return Nothing
    End Function


    Public gDMSV2TempPath As String = gloSettings.FolderSettings.AppTempFolderPath + "DMSV2Temp"

    Public Sub ImportDocument(ByVal nInsertScan As Int16)
        ''Insert File - 1
        ''Scan Images - 2
        ''Set focus to Wd object
        If oCurDoc Is Nothing Then
            Return
        End If
        oCurDoc.ActiveWindow.SetFocus()
        Try
            If nInsertScan = 1 Then
                Dim oFileDialogWindow As New System.Windows.Forms.OpenFileDialog()
                oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf"
                oFileDialogWindow.FilterIndex = 3
                oFileDialogWindow.Title = "Insert External Documents"
                oFileDialogWindow.Multiselect = False
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = System.Windows.Forms.DialogResult.OK Then
                    Dim oFile As New FileInfo(oFileDialogWindow.FileName)
                    If oFile.Extension.ToUpper() = (".Doc").ToUpper() OrElse oFile.Extension.ToUpper() = (".Docx").ToUpper() OrElse oFile.Extension.ToUpper() = (".txt").ToUpper() OrElse oFile.Extension.ToUpper() = (".rtf").ToUpper() Then
                        oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                    End If
                    oFile = Nothing
                End If
                'Memory Leak
                oFileDialogWindow.Dispose()
                oFileDialogWindow = Nothing
            ElseIf nInsertScan = 2 Then
                Dim oFiles As New System.Collections.ArrayList()
                'Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()
                'gloEDocumentV3.gloEDocV3Admin.Connect(_dataBaseConnectionString, GetDMSConnectionString(), gDMSV2TempPath, _LoginUserID, _ClinicID, Application.StartupPath)
                'oEDocument.ShowEScannerForImages(_patientID, oFiles)
                'oEDocument.Dispose()

                Dim firstFlag As Boolean = True
                Dim i As Integer = 0
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                For i = 0 To oFiles.Count - 1
                    If File.Exists(oFiles(i).ToString()) Then
                        '' SUDHIR 20090619 '' 
                        Dim oWord As New gloEMRWord.clsWordDocument()
                        oWord.GetandSetMyFirstFlag(True, firstFlag)
                        oWord.CurDocument = oCurDoc
                        oWord.InsertImage(oFiles(i).ToString())
                        firstFlag = oWord.GetandSetMyFirstFlag(False, False)
                        oWord = Nothing
                        oCurDoc.Application.Selection.EndKey()
                        oCurDoc.Application.Selection.InsertBreak()
                    End If
                Next
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                For i = oFiles.Count - 1 To 0 Step -1
                    If File.Exists(oFiles(i).ToString()) Then
                        Try
                            FileSystem.Kill(oFiles(i).ToString())
                        Catch
                        End Try
                    End If
                Next
                'Memory Leak
                If (oFiles IsNot Nothing) Then
                    oFiles.Clear()
                    oFiles = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
        End Try
    End Sub

    Public Function GetDMSConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
        Dim strConnectionString As String = Nothing
        If isSQLAuthentication = False Then
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
        Else

            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
        End If

        Return strConnectionString
    End Function

    Public Function GetDMSConnectionString() As String
        Return GetDMSConnectionString(gloEMRGeneralLibrary.Glogeneral.clsgeneral.sDmsServerName, gloEMRGeneralLibrary.Glogeneral.clsgeneral.sDmsDatabaseName, gloEMRGeneralLibrary.Glogeneral.clsgeneral.sDmsIsSqlAuthentication, gloEMRGeneralLibrary.Glogeneral.clsgeneral.sDmsUserId, gloEMRGeneralLibrary.Glogeneral.clsgeneral.sDmsPassword)
    End Function


    Private Sub DDLCBEvent(ByVal Sel As Wd.Selection)
        Try
            If IsNothing(Sel) Then
                Return
            End If
            If (Sel.Type <> Microsoft.Office.Interop.Word.WdSelectionType.wdNoSelection) Then
                If Sel.Start = Sel.End Then
                    Dim r As Wd.Range = Nothing
                    Try
                        r = Sel.Range
                    Catch ex As Exception

                    End Try
                    If (IsNothing(r)) Then
                        Exit Sub
                    End If
                    Try
                        r.SetRange(Sel.Start, Sel.End + 1)
                    Catch ex As Exception

                    End Try
                    If (IsNothing(r)) Then
                        Exit Sub
                    End If
                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then
                        Dim f As Wd.FormField = Nothing



                        Try
                            Dim o As Object = 1
                            f = r.FormFields.Item(o)
                            o = Nothing
                        Catch

                        End Try
                        If (IsNothing(f) = False) Then
                            If f.Type = Wd.WdFieldType.wdFieldFormCheckBox Then
                                f.CheckBox.Value = Not f.CheckBox.Value
                                Dim oUnit As Object = Wd.WdUnits.wdCharacter
                                Dim oCnt As Object = 1
                                Dim oMove As Object = Wd.WdMovementType.wdMove
                                Sel.MoveRight(oUnit, oCnt, oMove)
                            End If
                        End If

                        If Not f Is Nothing Then
                            f = Nothing
                        End If
                        'If Not o Is Nothing Then
                        '    o = Nothing
                        'End If
                    End If
                    If Not r Is Nothing Then
                        r = Nothing
                    End If
                End If
            End If

        Catch excp As Exception
        End Try
    End Sub

    Private Sub wdPatientEducation_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdPatientEducation.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    isHandlerRemoved = True
                Catch ex As Exception
                End Try

                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    'UpdateVoiceLog(ex.ToString)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "BeforeDocumentClosed - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub wdPatientEducation_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdPatientEducation.OnDocumentClosed

        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            If Not oWordApp Is Nothing Then
                '   Marshal.FinalReleaseComObject(oWordApp)
                oWordApp = Nothing
            End If
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        Catch ex As Exception
            ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "OnDocumentClosed - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub wdPatientEducation_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdPatientEducation.OnDocumentOpened
        oCurDoc = wdPatientEducation.ActiveDocument
        oWordApp = oCurDoc.Application
        Try
            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
        End Try

        Try
            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
        End Try
        oCurDoc.ActiveWindow.SetFocus()
    End Sub



End Class