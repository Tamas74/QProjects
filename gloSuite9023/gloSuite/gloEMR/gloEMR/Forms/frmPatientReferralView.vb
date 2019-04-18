Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices

Public Class frmPatientReferralView

    Dim objWord As clsWordDocument
    Dim objclsReferrals As New ClsReferralsDBLayer

    Private WithEvents oCurDoc As Wd.Document
    Private WithEvents oWordApp As Wd.Application

    Dim _ReferralID As Long
    Dim _PatientID As Long

    Public Sub New(ByVal ReferralID As Long)
        _ReferralID = ReferralID
        _PatientID = gnPatientID
        InitializeComponent()
    End Sub
    Public Sub New(ByVal ReferralID As Long, ByVal PatientID As Long)
        _ReferralID = ReferralID
        _PatientID = PatientID
        InitializeComponent()
    End Sub

    Private Sub Fill_Referral(ByVal dtReferral As DataTable)
        Try

            If Not IsNothing(dtReferral) Then
                If dtReferral.Rows.Count > 0 Then

                    objWord = New clsWordDocument
                    Dim strFileName As String
                    strFileName = ExamNewDocumentName
                    strFileName = objWord.GenerateFile(dtReferral.Rows(0)(1), strFileName)
                    objWord = Nothing

                    wdPatientReferral.Open(strFileName)
                    wdPatientReferral.Menubar = False
                    wdPatientReferral.Toolbars = False
                    wdPatientReferral.Titlebar = False
                    'objWord = Nothing


                    objWord = New clsWordDocument
                    objWord.CurDocument = oCurDoc
                    objWord.HighlightColor()
                    oCurDoc = objWord.CurDocument
                    objWord = Nothing
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                    oCurDoc.ActiveWindow.SetFocus()

                    If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdAllowOnlyComments Then
                        oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
                    End If

                    If Not oCurDoc Is Nothing Then
                        oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
                    End If

                Else
                    wdPatientReferral.Close()
                End If
            Else
                wdPatientReferral.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Referral", MessageBoxButtons.OK, MessageBoxIcon.Error)
            wdPatientReferral.Close()

        End Try
    End Sub

    Private Sub frmPatientReferralView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtReferral As DataTable
        dtReferral = objclsReferrals.GetReferral(_ReferralID, _PatientID)
        'wdPatientEducation.CreateNew("Word.Application")
        Fill_Referral(dtReferral)
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnPrint.Click

        'Dim wdTemp As New AxDSOFramer.AxFramerControl
        'Dim oTempDoc As New Wd.Document

        'Try
        '    ' TO SAVE DOCUMENT FOR TEMPORARY.. TO PRINT.. '
        '    Dim sFileName As String = gloOffice.Supporting.NewDocumentName()
        '    Dim oFileName As Object = DirectCast(sFileName, Object)
        '    Dim missing As Object = System.Reflection.Missing.Value
        '    Dim oFileFormat As Object = DirectCast(Wd.WdSaveFormat.wdFormatXMLDocument, Object)
        '    oCurDoc.SaveAs(oFileName, oFileFormat, missing, missing, missing, missing, _
        '    missing, missing, missing, missing, missing, missing, _
        '    missing, missing, missing, missing)

        '    ' OPEN FILE IN TEMPORARY WORD CONTROL. '
        '    Me.Controls.Add(wdTemp)
        '    wdTemp.Open(sFileName)
        '    oTempDoc = DirectCast(wdTemp.ActiveDocument, Microsoft.Office.Interop.Word.Document)
        '    oWordApp = oTempDoc.Application
        '    gloOffice.Supporting.CurrentDocument = oTempDoc

        '    ' TO CLEANUP EMPTY FORMFIELDS '
        '    gloWord.gloWord.CurrentDoc = oTempDoc
        '    gloWord.gloWord.CleanupDocument()
        '    wdTemp.PrintOut()
        '    wdTemp.Close()

        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        'Finally
        '    Me.Controls.Remove(wdTemp)
        '    oCurDoc = wdPatientReferral.ActiveDocument
        '    oWordApp = oCurDoc.Application
        'End Try

        oCurDoc.PrintOut()

    End Sub

    Private Sub wdPatientReferral_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdPatientReferral.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                frmPatientExam.blnIsHandlers = True
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If oFile.Path = gstrgloEMRStartupPath & "\Temp" Then
                        oFile.Delete()
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdPatientReferral_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdPatientReferral.OnDocumentClosed
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            If Not oWordApp Is Nothing Then
                Marshal.FinalReleaseComObject(oWordApp)
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdPatientReferral_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdPatientReferral.OnDocumentOpened
        oCurDoc = wdPatientReferral.ActiveDocument
        oWordApp = oCurDoc.Application

        Try
            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
    End Sub

    Private Sub DDLCBEvent(ByVal Sel As Wd.Selection)

        Try

            If Sel.Start = Sel.End Then

                Dim r As Wd.Range = Sel.Range

                r.SetRange(Sel.Start, Sel.End + 1)

                If r.FormFields.Count >= 1 Then

                    Dim om As Object = System.Reflection.Missing.Value

                    Dim f As Wd.FormField

                    Dim o As Object = 1

                    f = r.FormFields.Item(o)

                    'If f.Type = Wd.WdFieldType.wdFieldFormDropDown Then

                    '    Dim dd As Wd.DropDown = f.DropDown

                    '    Dim iCurSel As Integer = dd.Value

                    '    Dim oPU As oOffice.CommandBar = oWordApp.CommandBars.Add("CustomFormFieldPopup", oOffice.MsoBarPosition.msoBarPopup, om, True)

                    '    If False Then

                    '        Dim oDD As oOffice.CommandBarComboBox = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlDropdown, om, om, om, True), oOffice.CommandBarComboBox)

                    '        oDD.Style = oOffice.MsoComboStyle.msoComboLabel

                    '        oDD.DropDownLines = dd.ListEntries.Count

                    '        Dim le As Wd.ListEntry
                    '        For Each le In dd.ListEntries

                    '            oDD.AddItem(le.Name, om)

                    '        Next

                    '        oDD.ListIndex = iCurSel

                    '        CType(oPU, oOffice.CommandBar).ShowPopup(om, om)

                    '        dd.Value = oDD.ListIndex

                    '    Else

                    '        myidx = dd.Value

                    '        Dim iter As Integer = 1

                    '        Dim le As Wd.ListEntry
                    '        For Each le In dd.ListEntries

                    '            Dim btn As oOffice.CommandBarButton
                    '            '     Dim oDD As oOffice.CommandBarComboBox = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), oOffice.CommandBarButton)

                    '            btn = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), oOffice.CommandBarButton)

                    '            btn.Style = oOffice.MsoButtonStyle.msoButtonAutomatic

                    '            btn.Caption = le.Name

                    '            btn.Enabled = True

                    '            If iter = myidx Then

                    '                btn.State = oOffice.MsoButtonState.msoButtonDown
                    '            End If

                    '            iter = iter + 1

                    '            ' btn.Click += New Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(btn_Click)
                    '            AddHandler btn.Click, AddressOf btn_Click
                    '        Next

                    '        CType(oPU, oOffice.CommandBar).ShowPopup(om, om)

                    '        dd.Value = myidx

                    '    End If

                    'End If
                    If (Not IsNothing(f)) Then
                        If f.Type = Wd.WdFieldType.wdFieldFormCheckBox Then

                            f.CheckBox.Value = Not f.CheckBox.Value

                            Dim oUnit As Object = Wd.WdUnits.wdCharacter

                            Dim oCnt As Object = 1

                            Dim oMove As Object = Wd.WdMovementType.wdMove

                            Sel.MoveRight(oUnit, oCnt, oMove)

                        End If
                    End If

                End If

            End If

        Catch excp As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.General, excp.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub ts_btnFax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnFax.Click
        GenerateFaxDocument()
    End Sub
    Dim wdTemp As New AxDSOFramer.AxFramerControl
    Dim objCriteria As DocCriteria

    Private Function GenerateFaxDocument() As Wd.Document
        Dim _SaveFlag As Boolean = False
        If oCurDoc.Saved Then
            _SaveFlag = True
        End If
        Dim sFileName As String = ExamNewDocumentName

        oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

        wdPatientReferral.Close()

        wdTemp = New AxDSOFramer.AxFramerControl
        'Commented by dipak
        'currently we are not using this property because it throws exception
        'wdTemp.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoKeepUIActiveOnAppDeactive
        'end comment
        Me.Controls.Add(wdTemp)
        wdTemp.Open(sFileName)  'Open Template for processing in Temp user Ctrl
        oCurDoc = wdTemp.ActiveDocument
        oCurDoc.ActiveWindow.SetFocus()
      
        Call FaxPatientReferrals(oCurDoc)
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Patient Referral Fax", gloAuditTrail.ActivityOutCome.Success)
        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Patient Letter Fax", gstrLoginName, gstrClientMachineName, gnPatientID)

        wdTemp.Close()
        wdTemp.Dispose()

        LoadWordUserControl(sFileName, False)
        oCurDoc.ActiveWindow.View.ShowFieldCodes = False
        'Set the Start postion of the cursor in documents
        oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        oCurDoc.Saved = _SaveFlag

        Return oCurDoc
    End Function

    Private Sub FaxPatientReferrals(ByVal oTempDoc As Wd.Document)
        mdlFAX.Owner = Me
        'If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientLetters, m_PatientID, "", "", cmbTemplate.SelectedItem(1), 0, 0, 0) = False Then
        If RetrieveFAXDetails(mdlFAX.enmFAXType.ReferralLetter, _PatientID, "", "", "", 0, 0, 0) = False Then
            Exit Sub
        End If
        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
        oTempDoc.ActiveWindow.SetFocus()
        'Unprotect the document

        If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
            oTempDoc.Unprotect()
        End If


        'Commented by Shweta 20100201
        '''''''Against the bug id:5260 '''''''
        'Check the FAX Cover Page is enabled or not.
        'If the FAX Cover Page is enabled then Delete the Page Header from Exam
        'If gblnFAXCoverPage Then
        '    'To Delete Header
        '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Deleting Patient Referral Letter1 Page Header", gloAuditTrail.ActivityOutCome.Success)
        '    'UpdateLog("Deleting Patient Letters Page Header")
        '    Try

        '        If oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
        '            oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
        '        End If
        '        oTempDoc.Activate()
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekFirstPageHeader

        '        If oTempDoc.Application.Selection.HeaderFooter.IsHeader Then
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Select()
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Delete()
        '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Patient Referral Letter Page Header deleted", gloAuditTrail.ActivityOutCome.Success)
        '            'UpdateLog("Patient Letters Page Header deleted")
        '        End If

        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Error Deleting Patient Referral Letter Page Header - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        '        'UpdateVoiceLog("Error Deleting Patient Letters Page Header - " & ex.ToString)
        '    Finally
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        '    End Try
        'End If
        'End Commenting

        'Send the document for Printing i.e. to generate the TIFF File
        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        ' If objPrintFAX.FAXDocument(oTempDoc, m_PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, cmbTemplate.Text, clsPrintFAX.enmFAXType.PatientLetters) = False Then
        If objPrintFAX.FAXDocument(oTempDoc, _PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, "", clsPrintFAX.enmFAXType.ReferralLetter) = False Then
            'TIFF File has not been created
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        objPrintFAX = Nothing

    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        
        wdPatientReferral.Open(strFileName)
        If blnGetData Then
            ''//To retrieve the Form fields for the Word document
            objWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = _PatientID
            '''
            'If gnVisitID = 0 Then
            '    gnVisitID = GenerateVisitID(dt.Value)
            'End If

            'dtLetterdate.Tag = gnVisitID
            '''
            objCriteria.VisitID = gnVisitID
            objCriteria.PrimaryID = 0
            objWord.DocumentCriteria = objCriteria
            objWord.CurDocument = oCurDoc
            ''Replace Form fields with Concerned data
            objWord.GetFormFieldData(enumDocType.None)
            oCurDoc = objWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objCriteria = Nothing
        Else
            objWord = New clsWordDocument
            objWord.CurDocument = oCurDoc
            objWord.HighlightColor()
            oCurDoc = objWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objWord = Nothing
        End If
        '  SetWordObjectEntry(m_IsFinished)
    End Sub

    Private Sub SetWordObjectEntry(ByVal IsFinished As Boolean)

        oCurDoc.ActiveWindow.SetFocus()
        If IsFinished = True Then
            If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
            End If

        Else
            If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Unprotect()
            End If


        End If

    End Sub
End Class