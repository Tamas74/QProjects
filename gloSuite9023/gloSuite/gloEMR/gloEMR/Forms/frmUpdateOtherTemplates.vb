Imports System.IO
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMR.gloEMRWord
Imports System.Runtime.InteropServices
Public Class frmUpdateOtherTemplates


    Dim objclsPatientExams As New clsPatientExams
    Dim objclsTemplateGallery As New clsTemplateGallery

    Private oCurDoc As Wd.Document
    Private oTempDoc As Wd.Document
    Dim objWord As clsWordDocument
    Private Arrlist As ArrayList
    Dim dtDataDictionary As DataTable
    Dim oWordApp As Wd.Application
    Private WithEvents WdTemplate As AxDSOFramer.AxFramerControl
    Dim dtCatgories As DataTable
    Private ImagePath As String


    Private Sub UpdateTemplate()

        '******Shweta 20090828 *********'
        'To check exeception related to word
        If CheckWordForException() = False Then
            Exit Sub
        End If
        'End Shweta

        On Error Resume Next
        ts_btnCancel.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        objWord = New clsWordDocument
        dtDataDictionary = objWord.GetDatadictionary
        objWord = Nothing
        lblStatus.Text = "Ready ..."
        lblStatus.Visible = True

        If chckHeader.Checked Then
            lblStatus.Text = "Initializing ..."
            Dim objWord As New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            objCriteria.PatientID = 0
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = 0
            objWord.DocumentCriteria = objCriteria

            ImagePath = objWord.getData_FromDB("Clinic_MST.imgClinicLogo", "Clinic Logo")
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
        Application.DoEvents()
        Dim dt As DataTable
        dt = objclsPatientExams.Fill_ExamTemplateNames(cmbCategory.SelectedValue, 0)
        lblStatus.Text = "Initializing ..."
        pgrbarStatus.Minimum = 1
        pgrbarStatus.Maximum = dt.Rows.Count
        pgrbarStatus.Step = 1
        lblStatus.Visible = True
        pgrbarStatus.Visible = True
        Application.DoEvents()
        WdTemplate = New AxDSOFramer.AxFramerControl
        Me.Controls.Add(WdTemplate)
        Application.DoEvents()
        'RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        If IsNothing(dt) = False Then
            For i As Int32 = 0 To dt.Rows.Count - 1
                Application.DoEvents()
                lblStatus.Text = "Updating ..." & i + 1 & " of " & dt.Rows.Count
                Call Fill_TemplateContents(CType(dt.Rows(i)(0), Long))
                pgrbarStatus.Value = i + 1
            Next
        End If
        lblStatus.Text = "Done ..."
        dt = Nothing
        WdTemplate.Dispose()
        'End If
        Me.Cursor = Cursors.Default
        ts_btnCancel.Enabled = True
        'Catch ex As Exception
        '   MsgBox(ex.Message)
        '  End Try
    End Sub

    'Procedure to get the contents of Past exam by passimg exam id
    Private Sub Fill_TemplateContents(ByVal TemplateID As Long)

        On Error Resume Next

        'Dim dsData As New DataSet
        Dim dv As New DataView

        '' Get Exam Contents of selected Exam
        objclsTemplateGallery.SelectTemplateGallery(TemplateID)

        dv = objclsTemplateGallery.GetDataview

        Dim strFileName As String
        objWord = New clsWordDocument
        strFileName = ExamNewDocumentName
        strFileName = objWord.GenerateFile(dv(0)(3), strFileName)
        objWord = Nothing

        WdTemplate.Open(strFileName)
        oCurDoc = WdTemplate.ActiveDocument
        oWordApp = oCurDoc.Application

        oCurDoc.ActiveWindow.SetFocus()
        If chckHeader.Checked Then
            Call UpdateHeader()
        End If
        'Update bookmarks 
        ' Call UpdateFields()

        '' For Replacing Old Check Boxes With New 
        'Call DeleteAndInsertCheckBoxes_new()

        ''Call UpdateFields()
        '' oCurDoc.ActiveWindow.Selection.HomeKey(Word.WdUnits.wdStory)
        Call ReplaceFields()
        Call ReplaceCheckBoxes()

        '' oCurDoc.ActiveWindow.Selection.HomeKey(Word.WdUnits.wdStory)
        Call ReplaceFields_bb()  '''' 'bb'
        ''oCurDoc.ActiveWindow.Selection.HomeKey(Word.WdUnits.wdStory)
        Call ReplaceFields_bbbb()  '' "bb*bb"

        Call ReplaceFields_Brakets() ''"[   ]"
        oCurDoc.Application.Selection.Find.Execute(FindText:="Orders:", ReplaceWith:="Order Radiology:", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll)


        Call UpdateFormFields()
        ' WdTemplate.Save(strFileName, True, "", "")
        oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
        WdTemplate.Close()
        oCurDoc = Nothing
        objclsTemplateGallery.UpdateTemplateGallery(TemplateID, strFileName)

    End Sub
    Private Sub UpdateHeader()

        Try

            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            If oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
                oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
            End If
            oCurDoc.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekCurrentPageHeader

            oCurDoc.Application.Selection.Select()
            oCurDoc.Application.Selection.HeaderFooter.Range.Delete()

            With oCurDoc.Application.ActiveDocument.PageSetup
                .DifferentFirstPageHeaderFooter = True
            End With
            If File.Exists(ImagePath) Then
                oCurDoc.Application.Selection.ParagraphFormat.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter

                '' SUDHIR 20090619 '' 
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                oWord.InsertImage(ImagePath)
                oWord = Nothing
                'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
                '' END SUDHIR ''

                oCurDoc.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
            End If

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
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
    '''' <remarks></remarks>
    Private Sub UpdateFormFields()
        Dim objField As Wd.FormField 'Form field Variable

        For Each objField In oCurDoc.FormFields
            If objField.Type = Wd.WdFieldType.wdFieldFormTextInput Then

                Dim strFindText = Split(objField.StatusText, ".") ' To Get Diagnosis
                ''Updating Vitals with New structure
                'If objField.StatusText = "Vitals.sHeight+Vitals.dWeightinlbs+Vitals.dTemperature+Vitals.dRespiratoryRate" Then
                '    Find_n_ReplaceForVitals(objField.StatusText)
                'ElseIf objField.StatusText = "Prescription.sMedication+Prescription.sDosage+Prescription.sFrequency+Prescription.sRoute+Prescription.sAmount+Prescription.sRefills" Then
                '    Find_n_ReplaceForPrescription(objField.StatusText)
                'ElseIf strFindText(0) = "Diagnosis" Then
                '    objField.Result = "Diagnosis"
                '    objField.StatusText = "ExamICD9CPT.sICD9Code+ExamICD9CPT.sICD9Description"
                '    objField.HelpText = "Diagnosis"
                'ElseIf strFindText(0) = "Treatment" Then
                '    objField.Result = "Treatment"
                '    objField.StatusText = "ExamICD9CPT.sCPTcode+ExamICD9CPT.sCPTDescription"
                '    objField.HelpText = "Treatment"
                'Else
                Dim blnExpiredField As Boolean = True
                ' Updating all the form fields with new FormFiled display result
                For cnt As Int32 = 0 To dtDataDictionary.Rows.Count - 1
                    If dtDataDictionary.Rows(cnt)(1) = objField.StatusText Then
                        blnExpiredField = False
                        objField.Result = dtDataDictionary.Rows(cnt)(3)
                        objField.HelpText = dtDataDictionary.Rows(cnt)(3)
                    End If
                Next
                If blnExpiredField Then
                    Find_n_RemoveExpiredFormField(objField.StatusText)
                    blnExpiredField = False
                End If
            ElseIf objField.Type = Microsoft.Office.Interop.Word.WdFieldType.wdFieldFormCheckBox Then
                objField.CheckBox.Value = False
            End If
            ' End If
        Next


    End Sub
    Private Sub ReplaceFields()
        Dim strFindText As String
        Dim strResultText As String
        Dim strStatusText As String
        Dim nCount As Integer
        Dim i As Integer

        'Read file
        Dim strFILE_NAME As String = Application.StartupPath & "\DataDictionary.txt"
        Dim strFields As Array
        If Not File.Exists(strFILE_NAME) Then
            MsgBox("File Does not exists")
            Exit Sub
        End If
        'Read file
        Dim sr As StreamReader
        sr = File.OpenText(strFILE_NAME)
        'Put String in variable 
        Dim strInput As String
        strInput = sr.ReadLine()
        strInput = strInput.Trim
        'Read file line by line

        Do
            If strInput = "" Then
                Exit Sub
            End If
            'MsgBox(strInput)
            If strInput = "~Vitals~" Then
                Find_n_ReplaceForVitals(strInput)
            ElseIf strInput = "~Click Date~" Then
                Find_n_ReplaceForSelectDate(strInput)
            Else

                'Spit Fields by $ character 
                strFields = strInput.Split("$")
                If strFields.Length >= 3 Then
                    strFindText = strFields(0)
                    strResultText = strFields(1)
                    strStatusText = strFields(2)
                    nCount = 0
                    If strResultText <> "" Then
                        WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
                        WdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
                        With oCurDoc.Content.Find
                            Do While .Execute(FindText:=strFindText, Forward:=True, Format:=True, MatchWholeWord:=True) = True
                                nCount = nCount + 1
                            Loop
                        End With
                        ''Replace all fields
                        For i = 1 To nCount
                            Find_n_ReplaceText(strFindText, strResultText, strStatusText)
                        Next
                    End If
                End If
            End If
            'Till Here
            'Next field
            'line = sr.ReadLine()
            strInput = sr.ReadLine()

        Loop Until strInput Is Nothing
        sr.Close()

    End Sub
    Private Sub Find_n_ReplaceForSelectDate(ByVal strText As String)
        ' Dim ncount, i As Int32
        If strText <> "" Then
            WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
            WdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
            With WdTemplate.ActiveDocument.Application.Selection.Find
                .Text = strText
                .Replacement.Text = "[]"
                .Forward = True
                .Wrap = Wd.WdFindWrap.wdFindContinue
                .Format = True
                .MatchCase = False
                .MatchWholeWord = True
                .MatchWildcards = False
                .MatchSoundsLike = False
                .MatchAllWordForms = False
                '.MatchWildcards = True
                .Execute(Replace:=Wd.WdReplace.wdReplaceAll)
            End With


            'With oCurDoc.Content.Find
            '    Do While .Execute(FindText:=strText, Forward:=True, Format:=True, MatchWholeWord:=True) = True
            '        ncount = ncount + 1
            '    Loop
            'End With
            ' ''Replace all fields
            'For i = 1 To ncount
            '    ReplaceSelectDate(strText)
            'Next
        End If
    End Sub
    Private Sub ReplaceSelectDate(ByVal strText As String)
        With WdTemplate.ActiveDocument.Application.Selection.Find
            .Text = strText
            .Replacement.Text = ""
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = True
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
            '.MatchWildcards = True
            .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
            If .Found = True Then
                Dim objcnt As Wd.ContentControl
                objcnt = oCurDoc.ContentControls.Add(Wd.WdContentControlType.wdContentControlDate, oCurDoc.Application.Selection.Range)
                objcnt.LockContentControl = False
                'Dim oNameField As Wd.FormField
                'oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                'oNameField.Result = strResultText
                'oNameField.StatusText = strResultText
                'oNameField.HelpText = strResultText
            End If
        End With
    End Sub
    Private Sub ReplaceFields_bbbb()

        WdTemplate.ActiveDocument.Application.Selection.HomeKey(Unit:=Wd.WdUnits.wdStory)
        WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        WdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        With WdTemplate.ActiveDocument.Application.Selection.Find
            .Text = "<bb*bb>"
            .Replacement.Text = "[]"
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = False
            .MatchAllWordForms = False
            .MatchSoundsLike = False
            .MatchWildcards = True
        End With
        WdTemplate.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)

    End Sub

    Private Sub ReplaceFields_bb()

        WdTemplate.ActiveDocument.Application.Selection.HomeKey(Unit:=Wd.WdUnits.wdStory)
        WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        WdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        With WdTemplate.ActiveDocument.Application.Selection.Find
            .Text = "bb"
            .Replacement.Text = "[]"
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = True
            .MatchAllWordForms = False
            .MatchSoundsLike = False
            .MatchWildcards = False
        End With
        WdTemplate.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)

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

        WdTemplate.ActiveDocument.Application.Selection.HomeKey(Unit:=Wd.WdUnits.wdStory)
        WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        WdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        With WdTemplate.ActiveDocument.Application.Selection.Find
            .Text = "[ ]"
            .Replacement.Text = "[]"
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = True
            .MatchAllWordForms = False
            .MatchSoundsLike = False
            .MatchWildcards = False
        End With
        WdTemplate.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)


    End Sub

    Private Sub ReplaceCheckBoxes()
        Dim strFindText As String
        Dim nCount As Integer
        Dim i As Integer

        'oCurDoc.Application.Selection.Find.ClearFormatting()
        'oCurDoc.Application.Selection.Find.Replacement.ClearFormatting()
        WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        WdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()

        strFindText = "~(X)~"
        'Count Fields
        With WdTemplate.ActiveDocument.Content.Find
            Do While .Execute(FindText:=strFindText, Forward:=True, Format:=True, MatchWholeWord:=True) = True
                nCount = nCount + 1
            Loop
        End With
        'Replace all fields
        For i = 1 To nCount
            WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
            WdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
            'oCurDoc.Application.Selection.Find.ClearFormatting()
            'oCurDoc.Application.Selection.Find.Execute(FindText:=strFindText, ReplaceWith:=" ", Replace:=Wd.WdReplace.wdReplaceOne, Forward:=True)
            ''Call AddCheckBox()
            Call Find_n_ReplaceCheckBox(strFindText)
        Next

        nCount = 0
        'oCurDoc.Application.Selection.Find.ClearFormatting()
        'oCurDoc.Application.Selection.Find.Replacement.ClearFormatting()
        WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        WdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()

        strFindText = "~x~"
        'Count Fields
        With WdTemplate.ActiveDocument.Content.Find
            Do While .Execute(FindText:=strFindText, Forward:=True, Format:=True, MatchWholeWord:=True) = True
                nCount = nCount + 1
            Loop
        End With
        'Replace all fields
        For i = 1 To nCount
            'oCurDoc.Application.Selection.Find.ClearFormatting()
            'oCurDoc.Application.Selection.Find.Execute(FindText:=strFindText, ReplaceWith:=" ", Replace:=Wd.WdReplace.wdReplaceOne, Forward:=True)
            WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
            WdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
            'Call AddCheckBox()
            Call Find_n_ReplaceCheckBox(strFindText)
        Next

    End Sub


    'Procedure to replace navigation icons for print & fax
    Private Sub Find_n_ReplaceText(ByVal strSearchText As String, ByVal strResultText As String, ByVal strStatusText As String)
        With WdTemplate.ActiveDocument.Application.Selection.Find
            .Text = strSearchText
            .Replacement.Text = strResultText
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = True
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
            '.MatchWildcards = True
            .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
            If .Found = True Then
                Dim oNameField As Wd.FormField
                oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                oNameField.Result = strResultText
                oNameField.StatusText = strResultText
                oNameField.HelpText = strResultText
            End If
        End With
        'WdTemplate.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
    End Sub

    ''To remove expired Form fields from existing Tempaltes
    Private Sub Find_n_RemoveExpiredFormField(ByVal strSearchText As String)
        With oCurDoc.Application.Selection.Find
            .Text = strSearchText
            .Replacement.Text = String.Empty
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = True
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
            '.MatchWildcards = True
            .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
            oCurDoc.Application.Selection.MoveRight(1, 1)
        End With
    End Sub
    ''For prescription
    Private Sub Find_n_ReplaceForPrescription(ByVal strSearchText As String)
        With oCurDoc.Application.Selection.Find
            .Text = strSearchText
            .Replacement.Text = String.Empty
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = True
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
            '.MatchWildcards = True
            .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
            oCurDoc.Application.Selection.MoveRight(1, 1)
            If .Found = True Then
                Dim oNameField As Wd.FormField
                oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                oNameField.Result = "Prescription"
                oNameField.StatusText = "Prescription.sMedication+Prescription.sDosage"
                oNameField.HelpText = "Prescription"
            End If
        End With
    End Sub

    'For Vitals only
    Private Sub Find_n_ReplaceForVitals(ByVal strSearchText As String)
        WdTemplate.ActiveDocument.Application.Selection.Find.ClearFormatting()
        WdTemplate.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        With oCurDoc.ActiveWindow.Application.Selection.Find
            .Text = strSearchText
            .Replacement.Text = String.Empty
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = True
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
            '.MatchWildcards = True
            .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
            If .Found = True Then
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


                ' ''43	Vitals.dRespiratoryRate	Vitals	Respiratory Rate	Vitals
                'oCurDoc.Application.Selection.TypeText(vbNewLine)
                'oCurDoc.Application.Selection.TypeText(" Respiratory Rate: ")
                'oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                'oNameField.Result = "Respiratory Rate"
                'oNameField.StatusText = "Vitals.dRespiratoryRate"
                'oNameField.HelpText = "Respiratory Rate"

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
        End With

        'WdPatientExam.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
    End Sub

    Private Sub Find_n_Replacebbbb(ByVal strSearchText As String, ByVal strReplaceText As String)
        With WdTemplate.ActiveDocument.Application.Selection.Find
            .Text = strSearchText
            .Replacement.Text = strReplaceText
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindStop
            .Format = False
            .MatchCase = False
            .MatchWholeWord = True
            .MatchSoundsLike = False
            .MatchAllWordForms = False
            .MatchWildcards = True
            .Execute(Replace:=Wd.WdReplace.wdReplaceOne)

        End With
    End Sub

    Private Sub Find_n_Replacebb(ByVal strSearchText As String, ByVal strReplaceText As String)
        With WdTemplate.ActiveDocument.Application.Selection.Find
            .Text = strSearchText
            .Replacement.Text = strReplaceText
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindStop
            .Format = False
            .MatchCase = False
            .MatchWholeWord = True
            .MatchSoundsLike = False
            .MatchAllWordForms = False
            .MatchWildcards = False
            .Execute(Replace:=Wd.WdReplace.wdReplaceOne)

        End With

    End Sub

    'Procedure to replace navigation icons for print & fax
    Private Sub Find_n_ReplaceCheckBox(ByVal strSearchText As String)

        With WdTemplate.ActiveDocument.Application.Selection.Find
            .Text = strSearchText
            .Replacement.Text = ""
            .Forward = True
            .Wrap = Wd.WdFindWrap.wdFindContinue
            .Format = True
            .MatchCase = False
            .MatchWholeWord = False
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
            .Execute(Replace:=Wd.WdReplace.wdReplaceOne)
            If .Found = True Then
                Call AddCheckBoxNew()
            End If
        End With
        'WdPatientExam.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
    End Sub

    Public Sub AddCheckBoxNew()
        Dim oNameField As Wd.FormField
        oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

    End Sub

    Public Sub AddCheckBoxOld()
        'Dim i As Integer
        'For i = 1 To 200
        Dim sName As String
        Dim x As Wd.InlineShape
        Dim strName As String
        ' Dim gd As New Guid
        strName = Guid.NewGuid().ToString

        'x = oCurDoc.Application.Selection.InlineShapes.AddOLEControl("Forms.Label.1")
        x = WdTemplate.ActiveDocument.Application.Selection.InlineShapes.AddOLEControl("Forms.Label.1")
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
        WdTemplate.ActiveDocument.VBProject.VBComponents.Item(1).CodeModule.AddFromString(sCode)

        'WdTemplate.ActiveDocument.Selection.MoveLeft(Unit:=WdTemplate, Count:=1, Extend:=WdPatientExam)
        'Call AddCheckBox_()

        WdTemplate.ActiveDocument.UndoClear()
        WdTemplate.Select()
        WdTemplate.ActiveDocument.Application.Selection.MoveRight(1, 1)
        'oCurDoc.Application.Selection.MoveRight(1, 1)
        'Next

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmUpdateOtherTemplates_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'If Not oWordApp Is Nothing Then
        '    oWordApp.RecentFiles.Maximum = 0
        '    oWordApp.DisplayRecentFiles = False
        '    Marshal.FinalReleaseComObject(oWordApp)
        'End If
    End Sub



    Private Sub frmUpdateOtherTemplates_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dtCatgories = objclsPatientExams.Fill_TemplatesCategory()
            If Not IsNothing(dtCatgories) Then
                cmbCategory.DataSource = dtCatgories
                cmbCategory.ValueMember = dtCatgories.Columns(0).ColumnName
                cmbCategory.DisplayMember = dtCatgories.Columns(1).ColumnName
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub WdTemplate_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles WdTemplate.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then

                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If oFile.Path = gstrgloEMRStartupPath & "\Temp" Then
                        oFile.Delete()
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub WdTemplate_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles WdTemplate.OnDocumentClosed
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            If Not oWordApp Is Nothing Then
                Marshal.FinalReleaseComObject(oWordApp)
                oWordApp = Nothing
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub ts_UpdateTemplate_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_UpdateTemplate.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Update"
                    UpdateTemplate()

                Case "Cancel"
                    Me.Close()

            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
End Class