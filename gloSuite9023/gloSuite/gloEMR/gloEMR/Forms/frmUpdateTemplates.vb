
Imports System.IO
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMR.gloEMRWord
Imports System.Runtime.InteropServices

Public Class frmUpdateTemplates


    Dim objclsPatientExams As New clsPatientExams
    Dim objclsTemplateGallery As New clsTemplateGallery

    Private oCurDoc As Wd.Document
    Private oTempDoc As Wd.Document
    Dim objWord As clsWordDocument
    Private Arrlist As ArrayList
    Dim dtDataDictionary As DataTable
    Dim oWordApp As Wd.Application


    Private ImagePath As String

    '' To Update Templates with only for given Fields, not to update whole template 
    Dim _OldFieldName As String
    Dim _NewFieldName As String
    Dim _IsHeaderUpdate As Boolean = True
    ''----

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    ''to take the oldfilename + newfilename
    Public Sub New(ByVal OldFieldName As String, ByVal NewFieldName As String, ByVal IsHeaderUpdated As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _OldFieldName = OldFieldName
        _NewFieldName = NewFieldName
        _IsHeaderUpdate = IsHeaderUpdated
    End Sub
    ''-----------------

    Private Sub frmUpdateTemplates_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'If Not oWordApp Is Nothing Then
        '    oWordApp.RecentFiles.Maximum = 0
        '    oWordApp.DisplayRecentFiles = False
        '    Marshal.FinalReleaseComObject(oWordApp)
        'End If
        If (IsNothing(dtDataDictionary) = False) Then
            dtDataDictionary.Dispose()
            dtDataDictionary = Nothing
        End If

        If (IsNothing(objclsPatientExams) = False) Then
            objclsPatientExams.Dispose()
            objclsPatientExams = Nothing
        End If

        If (IsNothing(objclsTemplateGallery) = False) Then
            objclsTemplateGallery.Dispose()
            objclsTemplateGallery = Nothing
        End If
    End Sub

    Private Sub frmUpdateExistingTemplates_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim dtCatgories As DataTable
            dtCatgories = objclsPatientExams.Fill_TemplatesCategory()
            ''To add the new row
            Dim _Row As DataRow
            If Not IsNothing(dtCatgories) Then
                _Row = dtCatgories.NewRow()
                _Row(0) = 0
                'Commented as per Incident #00044237
                '_Row(1) = "All"

                ' dtCatgories.Rows.Add(_Row)
                cmbCategory.DataSource = dtCatgories
                cmbCategory.ValueMember = dtCatgories.Columns(0).ColumnName
                cmbCategory.DisplayMember = dtCatgories.Columns(1).ColumnName



            End If

            chckHeader.Visible = _IsHeaderUpdate
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnUpdate.Click


        '******Shweta 20090828 *********'
        'To check exeception related to word
        If CheckWordForException() = False Then
            Exit Sub
        End If
        'End Shweta

        On Error Resume Next
        ts_btnCancel.Enabled = False
        ts_btnUpdate.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        objWord = New clsWordDocument
        If (IsNothing(dtDataDictionary) = False) Then
            dtDataDictionary.Dispose()
            dtDataDictionary = Nothing
        End If
        dtDataDictionary = objWord.GetDatadictionary
        objWord = Nothing
        lblStatus.Text = "Ready ..."
        lblStatus.Visible = True

        If chckHeader.Checked Then
            Dim objWord As New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            objCriteria.PatientID = 0
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = 0
            objWord.DocumentCriteria = objCriteria

            ImagePath = objWord.getData_FromDB("Clinic_MST.imgClinicLogo", "Clinic Logo")
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
            ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)
        End If

        'If chckAllTemplates.Checked Then
        '    If Not IsNothing(dtCatgories) Then
        '        pgrbarStatus.Minimum = 1
        '        pgrbarStatus.Maximum = dtCatgories.Rows.Count
        '        pgrbarStatus.Step = 1
        '        pgrbarStatus.Visible = True
        '        WdTemplate = New AxDSOFramer.AxFramerControl
        '        Application.DoEvents()
        '        Me.Controls.Add(WdTemplate)
        '        For i As Int32 = 0 To dtCatgories.Rows.Count - 1
        '            Application.DoEvents()
        '            lblStatus.Text = "Updating ..." '& i + 1 & " of " & dt.Rows.Count
        '            Dim dt As New DataTable
        '            dt = objclsPatientExams.Fill_ExamTemplateNames(dtCatgories.Rows(i).Item(0), 0)
        '            If IsNothing(dt) = False Then
        '                For j As Int32 = 0 To dt.Rows.Count - 1
        '                    Application.DoEvents()
        '                    Call Fill_TemplateContents(CType(dt.Rows(i)(0), Long))
        '                Next
        '            End If
        '            dt = Nothing
        '            Application.DoEvents()
        '            pgrbarStatus.Value = i + 1
        '        Next
        '        WdTemplate.Dispose()
        '        lblStatus.Text = "Done ..."
        '    End If

        'Else

        Dim dt As DataTable
        dt = objclsPatientExams.Fill_ExamTemplateNames(cmbCategory.SelectedValue, 0)
        pgrbarStatus.Minimum = 1
        pgrbarStatus.Maximum = dt.Rows.Count
        pgrbarStatus.Step = 1
        lblStatus.Visible = True
        pgrbarStatus.Visible = True


        If IsNothing(dt) = False Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                Application.DoEvents()
                lblStatus.Text = "Updating ..." '& i + 1 & " of " & dt.Rows.Count
                Call Fill_TemplateContents(CType(dt.Rows(i)(0), Long))
                pgrbarStatus.Value = i + 1
            Next
            dt.Dispose()
            dt = Nothing
        End If
        lblStatus.Text = "Done ..."
       

        'End If
        Me.Cursor = Cursors.Default
        ts_btnCancel.Enabled = True
        ts_btnUpdate.Enabled = True
        'Catch ex As Exception
        '   MsgBox(ex.Message)
        '  End Try
    End Sub

    'Procedure to get the contents of Template of given TemplateID
    Private Sub Fill_TemplateContents(ByVal TemplateID As Long)
        Try
            Dim dv As DataView

            '' Get Exam Contents of selected Exam
            objclsTemplateGallery.SelectTemplateGallery(TemplateID)

            dv = objclsTemplateGallery.GetDataview

            Dim strReqFileName As String = ExamNewDocumentName
            objWord = New clsWordDocument
            strReqFileName = objWord.GenerateFile(dv(0)(3), strReqFileName)
            objWord = Nothing

            '  wdTemplate.Open(strReqFileName)
            '  Dim oWordApp As Wd.Application = Nothing
            Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdTemplate, strReqFileName, oCurDoc, oWordApp)
            If (strError <> String.Empty) Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, strError, gloAuditTrail.ActivityOutCome.Failure)

            Else

                oCurDoc = wdTemplate.ActiveDocument
                oWordApp = oCurDoc.Application

                oCurDoc.ActiveWindow.SetFocus()
                If chckHeader.Checked Then
                    Call UpdateHeader()
                End If

                If _OldFieldName = "" Then          ''Dhruv 20100216 If old field name is nothing or blank                 
                    Call UpdateFormFields()         ''call the full update means it will check for all the updation
                Else
                    Call UpdateSelectedFormField()  ''Dhruv---Selected update is for the ROS + FlowSheet + history (mainly against history)--End
                End If

                'Try
                '    oCurDoc.Application.Selection.Find.Execute(FindText:="Orders:", ReplaceWith:="Order Radiology:", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll)

                'Catch ex As Exception
                Try

                    gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:="Orders:", ReplaceWith:="Order Radiology:", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=False)
                Catch ex2 As Exception

                End Try
                'End Try

                'Update bookmarks 
                ' Call UpdateFields()

                '' For Replacing Old Check Boxes With New 
                'Call DeleteAndInsertCheckBoxes_new()

                ''Call UpdateFields()
                '' oCurDoc.ActiveWindow.Selection.HomeKey(Word.WdUnits.wdStory)
                ''Call ReplaceFields()
                ''Call ReplaceCheckBoxes()

                '' oCurDoc.ActiveWindow.Selection.HomeKey(Word.WdUnits.wdStory)
                ''Call ReplaceFields_bb()  '''' 'bb'
                ''oCurDoc.ActiveWindow.Selection.HomeKey(Word.WdUnits.wdStory)
                ''Call ReplaceFields_bbbb()  '' "bb*bb"

                ''Call ReplaceFields_Brakets() ''"[   ]"

                ' WdTemplate.Save(strFileName, True, "", "")
                'Dim strConvertedFileName As String = ExamNewDocumentName
                'oCurDoc.SaveAs(strConvertedFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

                'wdTemplate.Close()
                Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdTemplate, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, True)

                Dim myBinaray As Object = Nothing
                If (IsNothing(myByte) = False) Then
                    myBinaray = CType(myByte, Object)
                End If
                'oCurDoc = Nothing
                objclsTemplateGallery.UpdateTemplateGalleryBytes(TemplateID, myBinaray)
                If File.Exists(strReqFileName) Then
                    File.Delete(strReqFileName)
                End If
                'If File.Exists(strConvertedFileName) Then
                '    File.Delete(strConvertedFileName)
                'End If

                If (IsNothing(oCurDoc) = False) Then
                    Try
                        Marshal.ReleaseComObject(oCurDoc)
                    Catch ex As Exception


                    End Try
                    oCurDoc = Nothing

                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Private Sub UpdateHeader()

        Try

            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            'If oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
            '    oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView


            '    ' If oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintView Then

            '    'If oCurDoc.Application.Selection.HeaderFooter.IsHeader = True Then
            '    oCurDoc.Application.ActiveWindow.ActivePane.Application.View.SeekView = Wd.WdSeekView.wdSeekCurrentPageHeader
            '    ' End If
            '    oCurDoc.Application.Selection.Select()
            '    oCurDoc.Application.Selection.HeaderFooter.Range.Delete()
            '    ' End If
            'End If
            ''Added on 20100518
            'If oCurDoc.Application.Selection.HeaderFooter.IsHeader = True Then
            '    oCurDoc.Application.ActiveWindow.ActivePane.Application.View.SeekView = Wd.WdSeekView.wdSeekCurrentPageHeader
            'End If
            If oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
                oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
            End If
            Try
                'If Not IsNothing(oCurDoc.Application.Selection.HeaderFooter) Then
                oCurDoc.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekCurrentPageHeader
                oCurDoc.Application.Selection.Select()
                oCurDoc.Application.Selection.HeaderFooter.Range.Delete()
                'oCurDoc.Application.Selection.HeaderFooter.Rang()
                'End If
                'Catch ex As Exception
                '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                'End Try
                ''END 20100518

                With oCurDoc.Application.ActiveDocument.PageSetup
                    .DifferentFirstPageHeaderFooter = True
                End With
                If File.Exists(ImagePath) Then
                    oCurDoc.Application.Selection.ParagraphFormat.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter

                    '' SUDHIR 20090619 '' 
                    Dim oWord As New clsWordDocument
                    oWord.CurDocument = oCurDoc
                    If oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView Then
                        oWord.InsertImage(ImagePath)
                    End If
                    oWord = Nothing
                    'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
                    '' END SUDHIR ''

                    oCurDoc.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            End Try

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Procedure to update fields
    Public Sub UpdateFields()
        'On Error Resume Next
        Dim i As Integer
        For i = 1 To oCurDoc.Application.ActiveDocument.FormFields.Count
            If oCurDoc.Application.ActiveDocument.FormFields.Item(i).Type = Wd.WdFieldType.wdFieldFormTextInput Then
                If oCurDoc.Application.ActiveDocument.FormFields.Item(i).StatusText <> "" And oCurDoc.Application.ActiveDocument.FormFields.Item(i).StatusText <> "1" Then
                    oCurDoc.Application.ActiveDocument.FormFields.Item(i).HelpText = Mid(oCurDoc.Application.ActiveDocument.FormFields.Item(i).StatusText, 1, InStr(oCurDoc.Application.ActiveDocument.FormFields.Item(i).StatusText, ".") - 1)
                End If
            End If
        Next
    End Sub

    Private Sub DeleteAndInsertCheckBoxes_new()
        'On Error Resume Next
        'Try
        'Dim iShape As Word.InlineShapes
        'For Each iShape In wdDescription.ActiveDocument.InlineShapes
        'iShape.Item(1).Delete()
        'Next iShape
        Dim i As Integer
        For i = oCurDoc.Content.InlineShapes.Count To 1 Step -1
            Dim rng As Wd.Range
            rng = oCurDoc.Content.InlineShapes.Item(i).Range

            oCurDoc.Content.InlineShapes.Item(i).Delete()

            'AddCheckBox()
            Dim oNameField As Wd.FormField

            oNameField = oCurDoc.FormFields.Add(rng, Wd.WdFieldType.wdFieldFormCheckBox)

            'oCurDoc.Application.Selection.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
        Next
        oCurDoc.VBProject.VBComponents.Item(1).CodeModule.DeleteLines(1, oCurDoc.VBProject.VBComponents.Item(1).CodeModule.CountOfLines())

        'Catch ex As Exception
        '    MsgBox(ex.Message.ToString)
        '   End Try

    End Sub
    '''' <summary>
    '''' Updation for Form Fields with New Data dictionary fields
    '''' </summary>
    '''' <remarks>Same Function used in All template Updation Utility if any change in function then please same changes shoud be in utility.(Dipak 20100130) </remarks>
    Private Sub UpdateFormFields()
        Dim objField As Wd.FormField 'Form field Variable

        For Each objField In oCurDoc.FormFields
            If objField.Type = Wd.WdFieldType.wdFieldFormTextInput Then

                Dim strFindText = Split(objField.StatusText, ".") ' To Get Diagnosis
                ''Updating Vitals with New structure
                If objField.StatusText = "Vitals.sHeight+Vitals.dWeightinlbs+Vitals.dTemperature+Vitals.dRespiratoryRate" Then
                    Find_n_ReplaceForVitals(objField.StatusText)
                ElseIf objField.StatusText = "Prescription.sMedication+Prescription.sDosage+Prescription.sFrequency+Prescription.sRoute+Prescription.sAmount+Prescription.sRefills" Then
                    Find_n_ReplaceForPrescription(objField.StatusText)
                    'else if condition added by dipak 20091104 to fix 5009:Chief complaint liquid link not working 
                ElseIf (objField.StatusText = "Patient.sChiefComplaints") Then
                    objField.StatusText = "PatientChiefComplaint.sChiefComplaint"
                ElseIf (objField.StatusText = "Patient_DTL.sFirstName+Patient_DTL.sMiddleName+Patient_DTL.sLastName|Referral") Then '' REFERRAL NAME UPDATE AT 20100519 ''
                    objField.StatusText = "Patient_DTL.sPrefix+Patient_DTL.sFirstName+Patient_DTL.sMiddleName+Patient_DTL.sLastName+Patient_DTL.sDegree|Referral"
                ElseIf (objField.StatusText = "Patient.dtInjuryDate") Then
                    objField.StatusText = "PatientChiefComplaint.dtInjuryDate"
                ElseIf (objField.StatusText = "Patient.dtSurgeryDate") Then
                    objField.StatusText = "PatientChiefComplaint.dtSurgeryDate"
                ElseIf strFindText(0) = "Diagnosis" Then
                    objField.Result = "Diagnosis"
                    objField.StatusText = "ExamICD9CPT.sICD9Code+ExamICD9CPT.sICD9Description"
                    objField.HelpText = "Diagnosis"
                ElseIf strFindText(0) = "Treatment" Then
                    objField.Result = "Treatment"
                    objField.StatusText = "ExamICD9CPT.sCPTcode+ExamICD9CPT.sCPTDescription"
                    objField.HelpText = "Treatment"
                    '' Sudhir 20090331
                ElseIf objField.StatusText = "Clinic_MST.sAddress" Then
                    objField.StatusText = "Clinic_MST.sAddress1+Clinic_MST.sAddress2"
                ElseIf objField.StatusText = "Tasks_MST.sNotes" Then
                    objField.StatusText = "TM_Task_Progress.sDescription"
                ElseIf strFindText(0) = "FlowSheet" Then
                    Dim str As String
                    str = objField.StatusText
                    str = str.Remove(0, 9)
                    objField.StatusText = "FlowSheet1" + str
                ElseIf strFindText(0) = "Patient" Then
                    Select Case strFindText(1)
                        Case "bIsWorkersComp"
                            objField.StatusText = "Patient_WorkersComp.sClaimno|bWorkersComp"
                        Case "sWorkersCompClaimNo"
                            objField.StatusText = "Patient_WorkersComp.sClaimno|WorkersComp"
                        Case "bIsAuto"
                            objField.StatusText = "Patient_WorkersComp.sClaimno|bAutoClaim"
                        Case "sAutoClaimNo"
                            objField.StatusText = "Patient_WorkersComp.sClaimno|AutoClaim"
                        Case Else
                    End Select
                ElseIf strFindText(0) = "Provider_MST" Then
                    Select Case objField.StatusText
                        Case "Provider_MST.sAddress+Provider_MST.sStreet"
                            objField.StatusText = "Provider_MST.sBusinessAddressline1+Provider_MST.sBusinessAddressline2"
                        Case "Provider_MST.sCity+Provider_MST.sState+Provider_MST.sZIP"
                            objField.StatusText = "Provider_MST.sBusinessCity+Provider_MST.sBusinessState+Provider_MST.sBusinessZIP"
                        Case "Provider_MST.sFirstName+Provider_MST.sLastName"
                            objField.StatusText = "Provider_MST.sFirstName+Provider_MST.sMiddleName+Provider_MST.sLastName"
                        Case "Provider_MST.sPhoneNo"
                            objField.StatusText = "Provider_MST.sBusPhoneNo"
                        Case "Provider_MST.sPhoneNo"
                            objField.StatusText = "Provider_MST.sBusPhoneNo"
                        Case "Provider_MST.sFAX"
                            objField.StatusText = "Provider_MST.sBusFAX"
                        Case "Provider_MST.sPagerNo"
                            objField.StatusText = "Provider_MST.sBusPagerNo"
                        Case "Provider_MST.sEmail"
                            objField.StatusText = "Provider_MST.sBusEmail"
                        Case Else
                    End Select
                ElseIf strFindText(0) = "Contacts_MST" Then
                    Dim strDetails = Split(strFindText(1), "|")
                    Select Case strDetails(1)
                        Case "Pharmacy"
                            Select Case strDetails(0)
                                Case "sName"
                                    objField.StatusText = "Patient_DTL.sName|Pharmacy"
                                Case "sPhone"
                                    objField.StatusText = "Patient_DTL.sPhone|Pharmacy"
                                Case "sFax"
                                    objField.StatusText = "Patient_DTL.sFax|Pharmacy"
                                Case Else
                            End Select

                        Case "Insurance"
                            Select Case strDetails(0)
                                Case "sName"
                                    objField.StatusText = "PatientInsurance_DTL.sInsuranceName|All"
                                Case Else
                            End Select

                        Case "PCP"
                            Select Case strDetails(0)
                                Case "sName"
                                    objField.StatusText = "Patient_DTL.sFirstName+Patient_DTL.sMiddleName+Patient_DTL.sLastName|PCP"
                                Case "sStreet"
                                    objField.StatusText = "Patient_DTL.sAddressLine1+Patient_DTL.sAddressLine2|PCP"
                                Case "sCSZ"
                                    objField.StatusText = "Patient_DTL.sCity+Patient_DTL.sState+Patient_DTL.sZIP|PCP"
                                Case "sPhone"
                                    objField.StatusText = "Patient_DTL.sPhone|PCP"
                                Case "sEmail"
                                    objField.StatusText = "Patient_DTL.sEmail|PCP"
                                Case "sFax"
                                    objField.StatusText = "Patient_DTL.sFax|PCP"
                                Case Else
                            End Select

                        Case "Physician"
                            Select Case strDetails(0)
                                Case "sName"
                                    'objField.StatusText = "Patient_DTL.sFirstName+Patient_DTL.sMiddleName+Patient_DTL.sLastName|Referral"
                                    objField.StatusText = "Patient_DTL.sPrefix+Patient_DTL.sFirstName+Patient_DTL.sMiddleName+Patient_DTL.sLastName+Patient_DTL.sDegree|Referral"
                                Case "sStreet"
                                    objField.StatusText = "Patient_DTL.sAddressLine1+Patient_DTL.sAddressLine2|Referral"
                                Case "sCSZ"
                                    objField.StatusText = "Patient_DTL.sCity+Patient_DTL.sState+Patient_DTL.sZIP|Referral"
                                Case "sPhone"
                                    objField.StatusText = "Patient_DTL.sPhone|Referral"
                                Case "sEmail"
                                    objField.StatusText = "Patient_DTL.sEmail|Referral"
                                Case "sFax"
                                    objField.StatusText = "Patient_DTL.sFax|Referral"
                                Case Else
                            End Select

                        Case Else

                    End Select
                    '' End Sudhir
                Else
                    If InStr(objField.StatusText, "History.sHistoryItem+History.sComments") Then

                    Else
                        If InStr(objField.StatusText, "History.sHistoryItem") Then
                            objField.StatusText = objField.StatusText.Replace("History.sHistoryItem", "History.sHistoryItem+History.sComments")
                        End If
                    End If
                    If InStr(objField.StatusText, "ROS.sROSItem+ROS.sComments") Then
                    Else
                        If InStr(objField.StatusText, "ROS.sROSItem") Then
                            objField.StatusText = objField.StatusText.Replace("ROS.sROSItem", "ROS.sROSItem+ROS.sComments")
                        End If
                    End If


                    Dim blnExpiredField As Boolean = True
                    ' Updating all the form fields with new FormFiled display result
                    For cnt As Int32 = 0 To dtDataDictionary.Rows.Count - 1
                        If dtDataDictionary.Rows(cnt)("sFieldName") = objField.StatusText Then
                            blnExpiredField = False
                            objField.Result = dtDataDictionary.Rows(cnt)("sCaption")
                            objField.HelpText = dtDataDictionary.Rows(cnt)("sCaption")
                        End If
                    Next
                    If blnExpiredField Then
                        Find_n_RemoveExpiredFormField(objField.StatusText)
                        blnExpiredField = False
                    End If
                End If
            End If
        Next


    End Sub

    ''' <summary>
    ''' Dhruv 20100218
    ''' To Update only given fields in the template, not all Templates
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateSelectedFormField()
        Dim objField As Wd.FormField 'Form field Variable                       


        Try
            For Each objField In oCurDoc.FormFields                             ''To check against the current field
                If objField.Type = Wd.WdFieldType.wdFieldFormTextInput Then
                    Dim strFindText = Split(objField.StatusText, ".")           ''spliting the value of the status field 
                    ''Updating Vitals with New structure

                    If strFindText(0).Trim.ToUpper = "History".Trim.ToUpper Or strFindText(0).Trim.ToUpper = "ROS".Trim.ToUpper Then  ''checking against the text history + ROS
                        If objField.Type = Wd.WdFieldType.wdFieldFormTextInput Then
                            ''Updating old field with new
                            If objField.StatusText.Trim = _OldFieldName.Trim Then
                                objField.StatusText = _NewFieldName

                                Dim strDetails() As String = Split(objField.StatusText, "|")
                                objField.Result = strDetails(1).ToString
                                objField.HelpText = strDetails(1).ToString
                            End If
                        End If
                    ElseIf strFindText(0).ToString.Trim.ToUpper = "flowsheet1".Trim.ToUpper Then ''Checking against the Flowsheet
                        Dim strText() As String
                        strText = Split(_NewFieldName, "|")

                        If objField.StatusText.Trim.Contains(_OldFieldName.Trim) = True Then        ''
                            objField.Result = strText(1)
                            objField.HelpText = strText(1)
                            If objField.StatusText.Split("|").Length = 2 Then       ''Updating the oldfield to new ones
                                '' whole flowsheet
                                objField.StatusText = _NewFieldName

                            ElseIf objField.StatusText.Split("|").Length = 3 Then       ''if contains the singlerow then add the single row
                                '' Single row of flowsheet
                                objField.StatusText = _NewFieldName & "|SingleRow"

                            End If
                        End If
                    End If
                End If
            Next
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    ''

    Private Sub ReplaceFields()
        Dim strFindText As String
        Dim strReplaceText As String
        Dim nCount As Integer
        Dim i As Integer

        'Read file
        Dim strFILE_NAME As String = gloSettings.FolderSettings.AppTempFolderPath & "datadictionary.txt"
        Dim strfields As Array
        If Not File.Exists(strFILE_NAME) Then
            MsgBox("File Does not exists")
        End If
        'Read file
        Dim sr As StreamReader = File.OpenText(strFILE_NAME)
        'Put String in variable 
        Dim strinput As String
        strinput = sr.ReadLine()
        'Read file line by line

        Do
            '            strinput = sr.ReadLine()
            'Spit Fields by | character and (First Item is Killer Field and other in gloEMR Field
            strfields = strinput.Split("|")

            strFindText = strfields(0)
            'Check how many | character are there in the string
            'If > 1 means the field name history is concated with its type

            If strFindText = "~Vitals~" Then
                'Dim arrVitals() As String = {"Height: Vitals.sHeight", "Weight: Vitals.dWeightinlbs", "Blood Pressure(min/max): Vitals.dBloodPressureSittingMin / Vitals.dBloodPressureSittingMax", "Temperature: Vitals.dTemperature"}
                Find_n_ReplaceForVitals(strFindText)
                ''Height: Vitals.sHeight  Weight: Vitals.dWeightinlbs Blood Pressure: Vitals.dBloodPressureSittingMin Vitals.dBloodPressureSittingMax Temperature: Vitals.dTemperature
            Else
                If UBound(strfields) > 1 Then
                    strReplaceText = strfields(1) & "|" & strfields(2)
                Else
                    strReplaceText = strfields(1)
                End If

                ''strReplaceText = strfields(1)
                ''            'Find field and replace it
                'The macro is recorded from Wd macro
                'Count Fields
                nCount = 0
                If strReplaceText <> "" Then
                    'wdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
                    'wdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
                    'With oCurDoc.Content.Find
                    Try
                        '  Do While .Execute(FindText:=strFindText, Forward:=True, Format:=True, MatchWholeWord:=True) = True
                        Do While gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strFindText, ReplaceWith:="", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=True, Format:=True) = True
                            nCount = nCount + 1
                        Loop
                    Catch ex As Exception

                    End Try

                    'End With
                    ''Replace all fields
                    For i = 1 To nCount
                        Find_n_ReplaceText(strfields(0), strReplaceText)
                    Next
                End If
            End If
            'Till Here
            'Next field
            'line = sr.ReadLine()
            strinput = sr.ReadLine()


        Loop Until strinput Is Nothing
        sr.Close()
    End Sub

    Private Sub ReplaceFields_bbbb()

        wdTemplate.ActiveDocument.Application.Selection.HomeKey(Unit:=Wd.WdUnits.wdStory)
        'wdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        'wdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        'Try
        '    With wdTemplate.ActiveDocument.Application.Selection.Find
        '        .Text = "<bb*bb>"
        '        .Replacement.Text = "[]"
        '        .Forward = True
        '        .Wrap = Wd.WdFindWrap.wdFindContinue
        '        .Format = False
        '        .MatchCase = False
        '        .MatchWholeWord = False
        '        .MatchAllWordForms = False
        '        .MatchSoundsLike = False
        '        .MatchWildcards = True
        '    End With
        '    wdTemplate.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        'Catch ex As Exception
        Try

            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdTemplate.ActiveDocument.Application, FindText:="<bb*bb>", ReplaceWith:="[]", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=True, MatchWholeWord:=False)
        Catch ex2 As Exception

        End Try
        'End Try



    End Sub

    Private Sub ReplaceFields_bb()

        wdTemplate.ActiveDocument.Application.Selection.HomeKey(Unit:=Wd.WdUnits.wdStory)
        'wdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        'wdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        'Try
        '    With wdTemplate.ActiveDocument.Application.Selection.Find
        '        .Text = "bb"
        '        .Replacement.Text = "[]"
        '        .Forward = True
        '        .Wrap = Wd.WdFindWrap.wdFindContinue
        '        .Format = False
        '        .MatchCase = False
        '        .MatchWholeWord = True
        '        .MatchAllWordForms = False
        '        .MatchSoundsLike = False
        '        .MatchWildcards = False
        '    End With
        '    wdTemplate.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        'Catch ex As Exception
        Try

            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdTemplate.ActiveDocument.Application, FindText:="bb", ReplaceWith:="[]", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=True)
        Catch ex2 As Exception

        End Try
        'End Try


        'Dim strFindText As String
        'Dim strReplaceText As String
        'Dim nCount As Integer
        'Dim i As Integer

        'strFindText = "bb"
        'strReplaceText = "[]"

        'nCount = 0
        'If strReplaceText <> "" Then
        '    WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        '    WdPatientExam.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        '    With oCurDoc.Content.Find
        '        Do While .Execute(FindText:=strFindText, Forward:=True, Format:=True, MatchWholeWord:=True, MatchWildcards:=False) = True
        '            nCount = nCount + 1
        '        Loop
        '    End With

        '    ''Replace all fields
        '    For i = 1 To nCount
        '        WdPatientExam.ActiveDocument.Application.Selection.Find.ClearFormatting()
        '        WdPatientExam.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()

        '        Find_n_Replacebb(strFindText, strReplaceText)
        '    Next
        'End If

    End Sub

    Private Sub ReplaceFields_Brakets()

        wdTemplate.ActiveDocument.Application.Selection.HomeKey(Unit:=Wd.WdUnits.wdStory)
        'wdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        'wdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        'Try
        '    With wdTemplate.ActiveDocument.Application.Selection.Find
        '        .Text = "[ ]"
        '        .Replacement.Text = "[]"
        '        .Forward = True
        '        .Wrap = Wd.WdFindWrap.wdFindContinue
        '        .Format = False
        '        .MatchCase = False
        '        .MatchWholeWord = True
        '        .MatchAllWordForms = False
        '        .MatchSoundsLike = False
        '        .MatchWildcards = False
        '    End With
        '    wdTemplate.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        'Catch ex As Exception
        Try

            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdTemplate.ActiveDocument.Application, FindText:="[ ]", ReplaceWith:="[]", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=True)
        Catch ex2 As Exception

        End Try
        'End Try




    End Sub

    Private Sub ReplaceCheckBoxes()
        Dim strFindText As String
        ' Dim strReplaceText As String
        Dim nCount As Integer
        Dim i As Integer

        'oCurDoc.Application.Selection.Find.ClearFormatting()
        'oCurDoc.Application.Selection.Find.Replacement.ClearFormatting()
        'wdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        'wdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()

        strFindText = "~(X)~"
        'Count Fields
        ' With wdTemplate.ActiveDocument.Content.Find
        Try
            '    Do While .Execute(FindText:=strFindText, Forward:=True, Format:=True, MatchWholeWord:=True) = True
            Do While gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdTemplate.ActiveDocument.Application, FindText:=strFindText, ReplaceWith:="", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=True, Format:=True) = True

                nCount = nCount + 1
            Loop
        Catch ex As Exception

        End Try

        'End With
        'Replace all fields
        For i = 1 To nCount
            'wdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
            'wdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
            'oCurDoc.Application.Selection.Find.ClearFormatting()
            'oCurDoc.Application.Selection.Find.Execute(FindText:=strFindText, ReplaceWith:=" ", Replace:=Wd.WdReplace.wdReplaceOne, Forward:=True)
            ''Call AddCheckBox()
            Call Find_n_ReplaceCheckBox(strFindText)
        Next

        nCount = 0
        'oCurDoc.Application.Selection.Find.ClearFormatting()
        'oCurDoc.Application.Selection.Find.Replacement.ClearFormatting()
        'wdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        'wdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()

        strFindText = "~x~"
        'Count Fields
        ' With wdTemplate.ActiveDocument.Content.Find
        Try
            '               Do While .Execute(FindText:=strFindText, Forward:=True, Format:=True, MatchWholeWord:=True) = True
            Do While gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdTemplate.ActiveDocument.Application, FindText:=strFindText, ReplaceWith:="", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=True, Format:=True) = True

                nCount = nCount + 1
            Loop
        Catch ex As Exception

        End Try

        '  End With
        'Replace all fields
        For i = 1 To nCount
            'oCurDoc.Application.Selection.Find.ClearFormatting()
            'oCurDoc.Application.Selection.Find.Execute(FindText:=strFindText, ReplaceWith:=" ", Replace:=Wd.WdReplace.wdReplaceOne, Forward:=True)
            'wdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
            'wdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
            'Call AddCheckBox()
            Call Find_n_ReplaceCheckBox(strFindText)
        Next

    End Sub


    'Procedure to replace navigation icons for print & fax
    Private Sub Find_n_ReplaceText(ByVal strSearchText As String, ByVal strReplaceText As String)
        'With wdTemplate.ActiveDocument.Application.Selection.Find
        '    .Text = strSearchText
        '    .Replacement.Text = strReplaceText
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindContinue
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = False
        '    .MatchWildcards = False
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        '    '.MatchWildcards = True
        '    Try
        '        .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
        '    Catch ex As Exception
        Dim yesFound As Boolean = False
        Try

            yesFound = gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdTemplate.ActiveDocument.Application, FindText:=strSearchText, ReplaceWith:=strReplaceText, Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceOne, MatchWildCards:=False, MatchWholeWord:=False)
        Catch ex2 As Exception

        End Try
        '  End Try


        If yesFound = True Then
            Dim oNameField As Wd.FormField
            oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
            oNameField.Result = strReplaceText
            oNameField.StatusText = strReplaceText
            oNameField.HelpText = Mid(strReplaceText, 1, InStr(strReplaceText, ".") - 1)
        End If
        '  End With
        'WdTemplate.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
    End Sub

    ''To remove expired Form fields from existing Tempaltes
    Private Sub Find_n_RemoveExpiredFormField(ByVal strSearchText As String)
        'With oCurDoc.Application.Selection.Find
        '    .Text = strSearchText
        '    .Replacement.Text = String.Empty
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindContinue
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = True
        '    .MatchWildcards = False
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        '    '.MatchWildcards = True
        '    Try
        '        .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
        '    Catch ex As Exception
        Try

            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.ActiveDocument.Application, FindText:=strSearchText, ReplaceWith:=String.Empty, Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceOne, MatchWildCards:=False, MatchWholeWord:=True)
        Catch ex2 As Exception

        End Try

        'End Try


        oCurDoc.Application.Selection.MoveRight(1, 1)
        'End With
    End Sub
    ''For prescription
    Private Sub Find_n_ReplaceForPrescription(ByVal strSearchText As String)
        'With oCurDoc.Application.Selection.Find
        '    .Text = strSearchText
        '    .Replacement.Text = String.Empty
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindContinue
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = True
        '    .MatchWildcards = False
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        '    '.MatchWildcards = True
        '    Try
        '        .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
        '    Catch ex As Exception
        Dim yesFound As Boolean = False
        Try

            yesFound = gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.ActiveDocument.Application, FindText:=strSearchText, ReplaceWith:=String.Empty, Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceOne, MatchWildCards:=False, MatchWholeWord:=True)
        Catch ex2 As Exception

        End Try
        'End Try


        oCurDoc.Application.Selection.MoveRight(1, 1)
        If yesFound = True Then
            Dim oNameField As Wd.FormField
            oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
            oNameField.Result = "Prescription"
            oNameField.StatusText = "Prescription.sMedication+Prescription.sDosage"
            oNameField.HelpText = "Prescription"
        End If
        'End With
    End Sub

    'For Vitals only
    Private Sub Find_n_ReplaceForVitals(ByVal strSearchText As String)
        'With oCurDoc.Application.Selection.Find
        '    .Text = strSearchText
        '    .Replacement.Text = String.Empty
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindContinue
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = True
        '    .MatchWildcards = False
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        '    '.MatchWildcards = True
        '    Try
        '        .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
        '    Catch ex As Exception
        Dim yesFound As Boolean = False
        Try

            yesFound = gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.ActiveDocument.Application, FindText:=strSearchText, ReplaceWith:=String.Empty, Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceOne, MatchWildCards:=False, MatchWholeWord:=True)
        Catch ex2 As Exception

        End Try
        'End Try


        oCurDoc.Application.Selection.MoveRight(1, 1)
        If yesFound = True Then
            Dim oNameField As Wd.FormField
            '' "Height: Vitals.sHeight"
            oCurDoc.Application.Selection.TypeText("Height: ")
            oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
            oNameField.Result = "Height"
            oNameField.StatusText = "Vitals.sHeight"
            oNameField.HelpText = "Height"

            ''40	Vitals.dWeightinlbs -	Weight in lbs	
            oCurDoc.Application.Selection.TypeText(vbTab)
            oCurDoc.Application.Selection.TypeText(" Weight: ")
            oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
            oNameField.Result = "Weight in lbs"
            oNameField.StatusText = "Vitals.dWeightinlbs"
            oNameField.HelpText = "Weight in lbs"

            ' ''42	Vitals.dWeightChange	Vitals	Weight Change	Vitals
            'oCurDoc.Application.Selection.TypeText(vbTab)
            'oCurDoc.Application.Selection.TypeText(" Weight Change: ")
            'oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
            'oNameField.Result = "Weight Change"
            'oNameField.StatusText = "Vitals.dWeightChange"
            'oNameField.HelpText = "Weight Change"


            ''43	Vitals.dRespiratoryRate	Vitals	Respiratory Rate	Vitals
            oCurDoc.Application.Selection.TypeText(vbNewLine)
            oCurDoc.Application.Selection.TypeText(" Respiratory Rate: ")
            oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
            oNameField.Result = "Respiratory Rate"
            oNameField.StatusText = "Vitals.dRespiratoryRate"
            oNameField.HelpText = "Respiratory Rate"

            '' 44	Vitals.dTemperature	Vitals	Temperature	Vitals
            oCurDoc.Application.Selection.TypeText(vbTab)
            oCurDoc.Application.Selection.TypeText("Temperature: ")
            oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
            oNameField.Result = "Temperature"
            oNameField.StatusText = "Vitals.dTemperature"
            oNameField.HelpText = "Temperature"

            ''45	Vitals.dBloodPressureSitting	Vitals	BP Sitting	Vitals
            oCurDoc.Application.Selection.TypeText(vbNewLine)
            oCurDoc.Application.Selection.TypeText("BP Sitting(max/min): ")
            oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
            oNameField.Result = "BP Sitting"
            oNameField.StatusText = "Vitals.dBloodPressureSitting"
            oNameField.HelpText = "BP Sitting"

            ''46	Vitals.dBloodPressureStanding	Vitals	BP Standing	Vitals
            oCurDoc.Application.Selection.TypeText(vbTab)
            oCurDoc.Application.Selection.TypeText("BP Standing(max/min): ")
            oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
            oNameField.Result = "BP Standing"
            oNameField.StatusText = "Vitals.dBloodPressureStanding"
            oNameField.HelpText = "BP Standing"

            ''47	Vitals.dPulsePerMinute	Vitals	Pulse	Vitals

            ''48	Vitals.dBMI	Vitals	BMI	Vitals

            ''49	Vitals.dPulseOx	Vitals	PulseOx	Vitals

            ''50	Vitals.dStature	Vitals	Stature	Vitals

            ''51	Vitals.dHeadCircumferance	Vitals	HeadCircumferance	Vitals

        End If
        'End With

        'WdPatientExam.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
    End Sub

    Private Sub Find_n_Replacebbbb(ByVal strSearchText As String, ByVal strReplaceText As String)
        'With wdTemplate.ActiveDocument.Application.Selection.Find
        '    .Text = strSearchText
        '    .Replacement.Text = strReplaceText
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindStop
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = True
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        '    .MatchWildcards = True
        '    Try
        '        .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
        '    Catch ex As Exception
        Try

            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdTemplate.ActiveDocument.Application, FindText:=strSearchText, ReplaceWith:=String.Empty, Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceOne, MatchWildCards:=True, MatchWholeWord:=True)
        Catch ex2 As Exception

        End Try
        'End Try



        'End With
    End Sub

    Private Sub Find_n_Replacebb(ByVal strSearchText As String, ByVal strReplaceText As String)
        'With wdTemplate.ActiveDocument.Application.Selection.Find
        '    .Text = strSearchText
        '    .Replacement.Text = strReplaceText
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindStop
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = True
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        '    .MatchWildcards = False
        '    Try
        '        .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
        '    Catch ex As Exception
        Try

            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdTemplate.ActiveDocument.Application, FindText:=strSearchText, ReplaceWith:=String.Empty, Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceOne, MatchWildCards:=False, MatchWholeWord:=True)
        Catch ex2 As Exception

        End Try
        '    End Try


        'End With

    End Sub

    'Procedure to replace navigation icons for print & fax
    Private Sub Find_n_ReplaceCheckBox(ByVal strSearchText As String)

        'With wdTemplate.ActiveDocument.Application.Selection.Find
        '    .Text = strSearchText
        '    .Replacement.Text = ""
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindContinue
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = False
        '    .MatchWildcards = False
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        '    Try
        '        .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
        '    Catch ex As Exception
        Dim yesFound As Boolean = False
        Try

            yesFound = gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdTemplate.ActiveDocument.Application, FindText:=strSearchText, ReplaceWith:=String.Empty, Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceOne, MatchWildCards:=False, MatchWholeWord:=False)
        Catch ex2 As Exception

        End Try
        'End Try


        If yesFound = True Then
            Call AddCheckBoxNew()
        End If
        'End With
        'WdPatientExam.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
    End Sub

    Public Sub AddCheckBoxNew()
        Dim oNameField As Wd.FormField
        oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
    End Sub

    Public Sub AddCheckBoxOld()
        ' Dim i As Integer
        'For i = 1 To 200
        Dim sName As String
        Dim x As Wd.InlineShape
        Dim strName As String
        ' Dim gd As New Guid
        strName = Guid.NewGuid().ToString

        'x = oCurDoc.Application.Selection.InlineShapes.AddOLEControl("Forms.Label.1")
        x = wdTemplate.ActiveDocument.Application.Selection.InlineShapes.AddOLEControl("Forms.Label.1")
        'nRndNumber = CLng(Rnd() * 10000000)
        'sName = x.OLEFormat.Object.Name
        x.OLEFormat.Object.AutoSize = True
        x.OLEFormat.Object.Font.Name = "Wingdings 2"
        x.OLEFormat.Object.FontSize = 14
        x.OLEFormat.Object.Caption = "T"
        x.OLEFormat.Object.BackColor = &HFFFFFF  '&HC0C0C0    'Gray color
        'x.OLEFormat.Object.Name = "L" & Replace(gd.ToString, "-", "_")
        x.OLEFormat.Object.Name = "L" & Replace(strName, "-", "_")
        sName = x.OLEFormat.Object.Name

        Dim sCode As String
        sCode = "Private Sub Label1_Click() " & vbNewLine
        'sCode = sCode + "     On Error Resume Next " & vbNewLine
        sCode = sCode + "     If Label1.Caption = ""R"" Then" & vbNewLine
        sCode = sCode + "         Label1.Caption = ""T"" " & vbNewLine
        sCode = sCode + "     Else" & vbNewLine
        sCode = sCode + "         Label1.Caption = ""R"" " & vbNewLine
        sCode = sCode + "     End If" & vbNewLine
        sCode = sCode + "End Sub"


        sCode = Replace(sCode, "Label1", sName)
        wdTemplate.ActiveDocument.VBProject.VBComponents.Item(1).CodeModule.AddFromString(sCode)

        'WdTemplate.ActiveDocument.Selection.MoveLeft(Unit:=WdTemplate, Count:=1, Extend:=WdPatientExam)
        'Call AddCheckBox_()

        wdTemplate.ActiveDocument.UndoClear()
        wdTemplate.Select()
        wdTemplate.ActiveDocument.Application.Selection.MoveRight(1, 1)
        'oCurDoc.Application.Selection.MoveRight(1, 1)
        'Next

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnCancel.Click
        Me.Close()
    End Sub

    Private Sub wdTemplate_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdTemplate.BeforeDocumentClosed
        Try

            If Not oWordApp Is Nothing Then
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub WdTemplate_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdTemplate.OnDocumentClosed
        Try
            ''Release the Document and application object References
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    ' Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    'Private Sub chckAllTemplates_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If chckAllTemplates.Checked = True Then
    '        cmbCategory.Enabled = False
    '    Else
    '        cmbCategory.Enabled = True
    '    End If
    'End Sub

    Private Sub WdTemplate_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdTemplate.OnDocumentOpened
        'oCurDoc = e.document
        oCurDoc = wdTemplate.ActiveDocument
        oWordApp = oCurDoc.Application

    End Sub
End Class