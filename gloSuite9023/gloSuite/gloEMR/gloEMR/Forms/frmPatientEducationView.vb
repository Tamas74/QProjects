Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices
Imports gloWord

Public Class frmPatientEducationView
    Implements IPatientContext


    Dim objWord As clsWordDocument


    Private WithEvents oCurDoc As Wd.Document
    Private WithEvents oWordApp As Wd.Application

    Dim _EducationID As Long
    Dim _PatientID As Long

    '
    'Public Sub New(ByVal EducationID As Long)
    '    _EducationID = EducationID
    '    _PatientID = gnPatientID
    '    InitializeComponent()
    'End Sub
    Public Sub New(ByVal EducationID As Long, ByVal PatientID As Long)
        _EducationID = EducationID
        _PatientID = PatientID
        InitializeComponent()
    End Sub

    Private Sub Fill_PatientEducation(ByVal dtEducation As DataTable)
        Try

            If Not IsNothing(dtEducation) Then
                If dtEducation.Rows.Count > 0 Then

                    objWord = New clsWordDocument
                    Dim strFileName As String
                    strFileName = ExamNewDocumentName
                    strFileName = objWord.GenerateFile(dtEducation.Rows(0)(1), strFileName)
                    objWord = Nothing

                    '    wdPatientEducation.Open(strFileName)
                    ' Dim oWordApp As Wd.Application = Nothing
                    Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdPatientEducation, strFileName, oCurDoc, oWordApp)
                    If (strError <> String.Empty) Then
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, strError, gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(strError, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        wdPatientEducation.Close()
                    Else


                        wdPatientEducation.Menubar = False
                        wdPatientEducation.Toolbars = False
                        wdPatientEducation.Titlebar = False
                        'objWord = Nothing


                        objWord = New clsWordDocument
                        objWord.CurDocument = oCurDoc
                        objWord.HighlightColor()
                        oCurDoc = objWord.CurDocument
                        objWord = Nothing

                        oCurDoc.ActiveWindow.SetFocus()

                        If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdAllowOnlyComments Then
                            oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
                        End If

                        If Not oCurDoc Is Nothing Then
                            oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
                        End If
                    End If
                Else
                    wdPatientEducation.Close()
                End If
            Else
                wdPatientEducation.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            wdPatientEducation.Close()

        End Try
    End Sub

    Private Sub frmPatientEducationView_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmPatientEducationView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Dim dtEducation As DataTable
        Dim objclsPatientEducation As New clsPatientEducation
        dtEducation = objclsPatientEducation.GetEducation(_EducationID, _PatientID)
        objclsPatientEducation.Dispose()
        objclsPatientEducation = Nothing
        'wdPatientEducation.CreateNew("Word.Application")
        If (IsNothing(dtEducation) = False) Then
            Fill_PatientEducation(dtEducation)
            dtEducation.Dispose()
            dtEducation = Nothing
        End If
        
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnPrint.Click
        'oCurDoc.Application.Options.PrintBackground = False
        'oCurDoc.PrintOut(Background:=False)
        If (IsNothing(oCurDoc) = False) Then
            LoadAndCloseWord.PrintWordDocument(oCurDoc, False, False, _PatientID)
        End If

        'oCurDoc.PrintOut()
    End Sub

    Private Sub wdPatientEducation_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdPatientEducation.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                Catch ex As Exception

                End Try

                frmPatientExam.blnIsHandlers = True
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdPatientEducation_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdPatientEducation.OnDocumentClosed
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    '    Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
    End Sub

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
                    ' r.SetRange(Sel.Start, Sel.End + 1)

                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then

                        ' Dim om As Object = System.Reflection.Missing.Value

                        Dim f As Wd.FormField = Nothing



                        Try
                            Dim o As Object = 1
                            f = r.FormFields.Item(o)
                            o = Nothing
                        Catch

                        End Try

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
                        If (IsNothing(f) = False) Then
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
            End If

        Catch excp As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, excp.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub ts_btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnExport.Click
        ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
        Dim objword1 As clsWordDocument
        objword1 = New clsWordDocument
        Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Patient Exam", Me)
        If Result = True Then
            MessageBox.Show(" Patient Exam Document Exported Successfully ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        objword1 = Nothing
        ' Export Function for Word Docs Integrated by dipak  as on 26 oct 2010
    End Sub

    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class