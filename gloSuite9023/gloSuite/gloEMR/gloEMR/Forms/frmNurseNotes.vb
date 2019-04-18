'Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices
Public Class frmNurseNotes
    Implements IHotKey
    Implements ISignature
    Implements gloVoice
    Implements IPatientContext

    Private ogloVoice As ClsVoice
    Private blnIsAddendum As Boolean
    Private WithEvents ouctlgloUC_Addendum As gloUC_Addendum
    Public MyCaller As frmVWNurseNotes
    Private ImagePath As String
    Private m_NurseNotesID As Long
    Private m_TemplateID As Long
    Private m_PatientID As Long
    Private m_visitID As Long
    Public m_IsFinished As Boolean = False
    Dim objCriteria As DocCriteria
    Dim objword As clsWordDocument
    Private blnCmbSelTemplate As Boolean = False
    Private WithEvents tlsNurseNotes As WordToolStrip.gloWordToolStrip
    ' Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Dim m_IsRecordLock As Boolean
    Public IsModify As Boolean = False
    Private _strPatientWorkStatus As String = ""
    Private _IsSave As Boolean = False

    Dim objclsNotes As New clsNurseNotes
    Private WithEvents oCurDoc As Wd.Document

    Dim blnSignClick As Boolean = False
    Private WithEvents _PatientStrip As gloUC_PatientStrip
    Private WithEvents oWordApp As Wd.Application
    Public Shared intflag As Integer
    Dim blnOpenHistory As Boolean = False
    Dim bIsPastExamClick As Boolean = False
    Public CancelClick As Boolean
    Private bnlIsFaxOpened As Boolean
    Private dtNurseNotesTemplates As DataTable


    Public Property NurseNoteTemplates As DataTable
        Get
            Return dtNurseNotesTemplates
        End Get
        Set(ByVal value As DataTable)
            dtNurseNotesTemplates = value
        End Set
    End Property


    'sanjog
    ''' <summary>
    ''' Show microphone when form is activated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNurseNotes_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Me.ParentForm IsNot Nothing Then
                CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
                CType(Me.ParentForm, MainMenu).ActiveDSO = wdNurseNotes
            End If
        End Try
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        End If
        If Not (IsNothing(wdNurseNotes)) Then
            Try
                If Not (IsNothing(wdNurseNotes.DocumentName)) Then
                    If Not (IsNothing(wdNurseNotes.ActiveDocument)) Then
                        oCurDoc = wdNurseNotes.ActiveDocument
                        oWordApp = oCurDoc.Application
                        Try
                            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try

                        Try
                            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try
                        isHandlerRemoved = False
                        oCurDoc.ActiveWindow.SetFocus()
                        wdNurseNotes.Focus()
                    End If
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
        End If

        Me.WindowState = FormWindowState.Maximized
    End Sub
    ''' <summary>
    ''' Turnoff microphone when form is deactivated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNurseNotes_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If
        Try
            If Not oWordApp Is Nothing Then
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                isHandlerRemoved = True
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick  for oWordApp", gloAuditTrail.ActivityOutCome.Success)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub



    Private Sub frmNurseNotes_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
          
            If m_IsRecordLock = False Then
                UnLock_Transaction(TrnType.NurseNotes, m_NurseNotesID, 0, Now)
            End If


            If IsNothing(MyCaller) = False Then
                If MyCaller.CanSelect = True Then
                    MyCaller.RefreshNotes(m_NurseNotesID)
                    MyCaller.SetGridSelection()
                End If
            End If
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
            End If
            Dispose_Object()

            If (IsNothing(mdlFAX.Owner) = False) Then
                mdlFAX.Owner = Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Close, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub frmNurseNotes_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
          

            'By Shweta 20090827
            If CheckWordForException() = False Then
                Exit Sub
            End If
            'End Shweta

            If m_IsRecordLock Then
                wdNurseNotes.Close()
                Exit Sub
            End If

            If IsNothing(oCurDoc) = False Then
                If oCurDoc.Saved = False Then
                    Dim Result As DialogResult
                    Result = MessageBox.Show("Do you want to save the changes to Nurses Notes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If Result = DialogResult.Yes Then
                        If m_IsFinished Then
                            Call SaveNurseNotes(True, True)
                        Else
                            Call SaveNurseNotes(False, True)
                        End If
                        e.Cancel = False
                    ElseIf Result = DialogResult.Cancel Then

                        e.Cancel = True
                        Exit Sub
                    ElseIf Result = DialogResult.No Then
                        e.Cancel = False
                    End If
                Else
                    e.Cancel = False
                End If
            Else
                e.Cancel = False
            End If

            wdNurseNotes.Close()


            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                If Not IsNothing(ogloVoice) Then
                    TurnoffMicrophone()
                    ogloVoice.UnInitializeVoiceComponents()
                End If
            ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
                If Not IsNothing(ogloVoice) Then
                    TurnoffMicrophone()
                    ogloVoice.UnInitializeVoiceComponents()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try



    End Sub


    Public Sub GetHistory()
        Dim frmPatHis As frmHistory
        Try


            TurnoffMicrophone()

            'If Trim(strPatientFirstName) <> "" Then
            If m_PatientID <> 0 Then
                Try
                    m_visitID = GenerateVisitID(dtLetterDate.Value, m_PatientID)
                    dtLetterDate.Tag = m_visitID
                Catch ex As Exception

                End Try
             

                Dim blnRecordLock As Boolean = False


                frmPatHis = New frmHistory(dtLetterDate.Tag, dtLetterDate.Value, m_PatientID, blnRecordLock)
                With frmPatHis

                    .WindowState = FormWindowState.Maximized
                    .blnOpenFromExam = True
                    .myNurse = Me
                    '.MdiParent = Me.ParentForm
                    .PopulatePatientHistory_Final()
                    If frmPatHis.blncancel Then
                        intflag = 1
                        .MdiParent = Me.ParentForm
                        .Show()
                        .BringToFront()
                        .WindowState = FormWindowState.Maximized
                    Else
                        If IsNothing(frmPatHis) = False Then
                            frmPatHis.Close()
                            frmPatHis.Dispose()
                            frmPatHis = Nothing
                        End If
                    End If

                End With
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub frmNurseNotes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Cursor = Cursors.WaitCursor
        Me.SuspendLayout()


        Dim dt As DataTable

        Try

            '29_oct-14 Aniket: Resolving Nurse Note performance issues
            'Call Get_PatientDetails()

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try


            objclsNotes.fill_widthofExam(pnlGloUC_TemplateTreeControl) ''''added to load width of DB

            dtLetterDate.Format = DateTimePickerFormat.Custom
            dtLetterDate.CustomFormat = Format("MM/dd/yyyy hh:mm tt")
            dtLetterDate.Value = Now

            If m_NurseNotesID <> 0 Then
                dt = objclsNotes.FillTemplate(enumTemplateFlag.NurseNotes, m_NurseNotesID, m_TemplateID)
            Else

                If IsNothing(dtNurseNotesTemplates) = True Then
                    dt = objclsNotes.FillTemplates()
                Else
                    dt = dtNurseNotesTemplates 'objclsNotes.FillTemplates()
                End If

            End If

            'Sanjog - Added on 2011 May 17 for Patient Safety
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then

                    cmbTemplate.DataSource = dt
                    cmbTemplate.DisplayMember = dt.Columns(1).ColumnName
                    cmbTemplate.ValueMember = dt.Columns(0).ColumnName
                    If _strPatientWorkStatus = "" Then
                        cmbTemplate.SelectedIndex = 0
                    Else
                        '' Select Patient Work Status
                        cmbTemplate.Text = _strPatientWorkStatus
                        If _strPatientWorkStatus = "With Restriction" Then
                            cmbTemplate.Enabled = False
                        End If
                    End If
                End If
            End If

            loadPatientStrip()

            If m_NurseNotesID <> 0 Then
                Dim dtLetter As DataTable
                dtLetter = objclsNotes.ScanNurseNotes(m_NurseNotesID)
                Fill_NurseNotes(dtLetter)
                If IsNothing(dtLetter) = False Then
                    dtLetter.Dispose()
                    dtLetter = Nothing
                End If
            Else
                '' for New Consent fill by default one Consent Template  
                If cmbTemplate.SelectedValue > 0 Then
                    Call Fill_TemplateGallery()

                Else
                    wdNurseNotes.CreateNew("Word.Application")

                End If
            End If

            If IsModify Then
                cmbTemplate.Enabled = False
                dtLetterDate.Enabled = False
            End If

            'Initialise Voice if nurse notes are unfinished
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
                InitializeVoiceObject()
                ShowMicroPhone()
            End If
            Me.Cursor = Cursors.Default
            Me.ResumeLayout()


            ''Added Code For Getting Template Tree,Past Data And Add Refresh Button's Fucntionality 
            pnlGloUC_PastWordNotes.Hide()
            InitiliseTemplateTreeControl()
            CallPastData()
            calltoAddRefreshButtonControl()

            If m_IsRecordLock Or m_IsFinished Then
                GloUC_AddRefreshDic1.Visible = False
                pnlGloUC_TemplateTreeControl.Hide()
                pnlGloUC_PastWordNotes.Show()
                Splitter2.Hide()
            End If



        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.Visible = True
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub InitiliseTemplateTreeControl()
        Try
            Dim objDocriteria As New DocCriteria
            ''slr free objword
            If Not IsNothing(objword) Then
                objword = Nothing
            End If
            objword = New clsWordDocument
            GloUC_TemplateTreeControl_NursesNotes.InitiliseControlParameter(GetConnectionString())
            GloUC_TemplateTreeControl_NursesNotes.DocCriteria = objDocriteria
            GloUC_TemplateTreeControl_NursesNotes.ObjClsWord = objword
            GloUC_TemplateTreeControl_NursesNotes.ProviderId = gnSelectedProviderID
            GloUC_TemplateTreeControl_NursesNotes.Fill_ExamTemplates(0)
            If Not IsNothing(objDocriteria) Then
                objDocriteria.Dispose()
                objDocriteria = Nothing
            End If
            objword = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub CallPastData()
        Try
            ''slr free previous memory objclsNotes,objword,objCriteria,clsPatientExams

            'If Not IsNothing(objclsNotes) Then

            '    objclsNotes = Nothing
            'End If
            'If Not IsNothing(objword) Then

            '    objword = Nothing
            'End If
            'If Not IsNothing(objCriteria) Then
            '    objCriteria.Dispose()
            '    objCriteria = Nothing
            'End If
            'If Not IsNothing(clsPatientExams) Then
            '    clsPatientExams.Dispose()
            '    clsPatientExams = Nothing
            'End If


            'Dim clsPatientExams As clsPatientExams = New clsPatientExams
            Dim objclsNotes As clsNurseNotes = New clsNurseNotes
            Dim objword As clsWordDocument = New clsWordDocument
            Dim objCriteria As DocCriteria = New DocCriteria
            Dim clsPatientExams2 As clsPatientExams = New clsPatientExams

            If (IsNothing(GloUC_PastWordNotes1.OBJCRITERIAs) = False) Then
                DirectCast(GloUC_PastWordNotes1.OBJCRITERIAs, DocCriteria).Dispose()
                GloUC_PastWordNotes1.OBJCRITERIAs = Nothing
            End If

            If (IsNothing(GloUC_PastWordNotes1.OBJWORDs) = False) Then
                GloUC_PastWordNotes1.OBJWORDs = Nothing
            End If


            If (IsNothing(GloUC_PastWordNotes1.OBJCLSDISCLOSUREs) = False) Then

                GloUC_PastWordNotes1.OBJCLSDISCLOSUREs = Nothing
            End If

            If (IsNothing(GloUC_PastWordNotes1.CLSPATIENTEXAMSs) = False) Then
                DirectCast(GloUC_PastWordNotes1.CLSPATIENTEXAMSs, clsPatientExams).Dispose()
            End If
            Try
                If (IsNothing(GloUC_PastWordNotes1) = False) Then
                    If (IsNothing(GloUC_PastWordNotes1.OBJCRITERIAs) = False) Then
                        DirectCast(GloUC_PastWordNotes1.OBJCRITERIAs, DocCriteria).Dispose()
                        GloUC_PastWordNotes1.OBJCRITERIAs = Nothing
                    End If
                End If
            Catch

            End Try
            GloUC_PastWordNotes1.OBJCRITERIAs = objCriteria
            GloUC_PastWordNotes1.OBJWORDs = objword
            GloUC_PastWordNotes1.PATIENTIDs = m_PatientID
            GloUC_PastWordNotes1.OBJCLSNOTESs = objclsNotes
            GloUC_PastWordNotes1.FromForms = "NursesNotes"
            GloUC_PastWordNotes1.CLSPATIENTEXAMSs = clsPatientExams2
            GloUC_PastWordNotes1.ShowHide_PastExam()

            '15-May-13 Aniket: Do not dispose the following objects as it causes issues while saving Nurse Notes.
            'Change made to solve memory Leak and word crash issue
            'objword = Nothing

            ''Memory Leak
            'objCriteria = Nothing
            'clsPatientExams = Nothing
            'objclsNotes = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub calltoAddRefreshButtonControl()
        Try
            'SLR: Added new memory for refresh control..

            Dim objCriteria As DocCriteria
            Dim objword As clsWordDocument

            objword = New clsWordDocument

            objCriteria = New DocCriteria
            objCriteria.PatientID = m_PatientID
            objCriteria.VisitID = dtLetterDate.Tag
            objCriteria.PrimaryID = 0
            objword.DocumentCriteria = objCriteria
            objword.CurDocument = oCurDoc
            objword.WaitControlPanel = Me.pnlwdNurseNotes
            GloUC_AddRefreshDic1.CONNECTIONSTRINGs = GetConnectionString()
            GloUC_AddRefreshDic1.OBJWORDs = objword
            'GloUC_AddRefreshDic1.OCURDOCs = oCurDoc
            If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                Try
                    DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()

                Catch

                End Try
                GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
            End If
            GloUC_AddRefreshDic1.OBJCRITERIAs = objCriteria
            GloUC_AddRefreshDic1.M_PATIENTIDs = m_PatientID
            GloUC_AddRefreshDic1.ObjFrom = Me
            GloUC_AddRefreshDic1.DTLETTERDATEs = dtLetterdate
            GloUC_AddRefreshDic1.OWORDAPPs = oWordApp
            GloUC_AddRefreshDic1.wdPatientWordDocs = wdNurseNotes
            objword = Nothing
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub Fill_NurseNotes(ByVal dtLetter As DataTable)

        cmbTemplate.SelectedValue = m_TemplateID
        If Not IsNothing(dtLetter) Then
            If dtLetter.Rows.Count > 0 Then
                dtLetterDate.Value = Format(dtLetter.Rows(0)(0), "MM/dd/yyyy hh:mm tt")
                Dim objWord As New clsWordDocument
                Dim strFileName As String
                strFileName = ExamNewDocumentName
                strFileName = objWord.GenerateFile(dtLetter.Rows(0)(1), strFileName)
                objWord = Nothing
                LoadWordUserControl(strFileName, False)
                'Set the Start postion of the cursor in documents

                oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                oCurDoc.Saved = True
            Else

                wdNurseNotes.Close()
            End If
        Else
            wdNurseNotes.Close()
        End If

    End Sub

    Private Sub Dispose_Object()
        Try

            Try
                If (IsNothing(GloUC_TemplateTreeControl_NursesNotes) = False) Then
                    If (IsNothing(GloUC_TemplateTreeControl_NursesNotes.DocCriteria) = False) Then
                        DirectCast(GloUC_TemplateTreeControl_NursesNotes.DocCriteria, gloEMR.gloEMRWord.DocCriteria).Dispose()
                        GloUC_TemplateTreeControl_NursesNotes.DocCriteria = Nothing
                    End If
                End If
            Catch

            End Try
            If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                Try
                    DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, gloEMR.gloEMRWord.DocCriteria).Dispose()

                Catch

                End Try
                GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
            End If
            Try
                If (IsNothing(GloUC_PastWordNotes1) = False) Then
                    If (IsNothing(GloUC_PastWordNotes1.OBJCRITERIAs) = False) Then
                        DirectCast(GloUC_PastWordNotes1.OBJCRITERIAs, gloEMR.gloEMRWord.DocCriteria).Dispose()
                        GloUC_PastWordNotes1.OBJCRITERIAs = Nothing
                    End If
                End If
            Catch

            End Try
            If Not IsNothing(GloUC_PastWordNotes1) Then
                GloUC_PastWordNotes1.Dispose()
                GloUC_PastWordNotes1 = Nothing
            End If


            'If Not IsNothing(objword) Then

            '    objword = Nothing
            'End If
            'If Not IsNothing(objCriteria) Then
            '    objCriteria = Nothing
            'End If
            'If Not IsNothing(clsPatientExams) Then
            '    clsPatientExams.Dispose()
            '    clsPatientExams = Nothing
            'End If
            If Not IsNothing(_PatientStrip) Then
                pnlMain.Controls.Remove(_PatientStrip)
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If

            If Not IsNothing(tlsNurseNotes) Then
                Me.pnlWordToolStrip.Controls.Remove(tlsNurseNotes)
                tlsNurseNotes.Dispose()
                tlsNurseNotes = Nothing
            End If

            If (IsNothing(GloUC_TemplateTreeControl_NursesNotes) = False) Then
                GloUC_TemplateTreeControl_NursesNotes.FinalizeControlParameter("")
            End If
            If Not IsNothing(ogloVoice) Then
                ogloVoice.Dispose()
                ogloVoice = Nothing
            End If
            If Not IsNothing(objclsNotes) Then
                objclsNotes.Dispose()
                objclsNotes = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub Fill_TemplateGallery()
        Try


            Dim strFileName As String
            ''slr free previous memory
            If Not IsNothing(objword) Then
                objword = Nothing
            End If

            ''slr free previous memory
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If

            objword = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Template
            objCriteria.PrimaryID = cmbTemplate.SelectedValue

            objword.DocumentCriteria = objCriteria
            ''//Retrieving the Patient Education from DB and Save it as Physical File
            strFileName = objword.RetrieveDocumentFile()
            objCriteria.Dispose()
            objCriteria = Nothing
            objword = Nothing
            If (IsNothing(strFileName) = False) Then
                If strFileName <> "" Then
                    LoadWordUserControl(strFileName, True)
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                Else
                    wdNurseNotes.Close()
                End If
            Else
                wdNurseNotes.Close()
            End If
           
            objword = Nothing
            objCriteria = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SetWordObjectEntry(ByVal IsFinished As Boolean)
        If (IsNothing(oCurDoc)) Then
            Return
        End If
        oCurDoc.ActiveWindow.SetFocus()
        If IsFinished = True Then
            If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
            End If
            If Not IsNothing(tmrDocProtect) Then
                tmrDocProtect.Enabled = True
                ''Bug #59396: 00000567 : Exam note not close instantly after opening up a finished exam note and than immediately closing the note Close Editor  
                ''Incident # 00020829: Closing Nurse notes taking time on terminal server.
                ''Comment the code.
                'tmrDocProtect.Interval = 1
            End If
        Else
            If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Unprotect()
            End If
            If Not IsNothing(tmrDocProtect) Then
                tmrDocProtect.Enabled = False
            End If

        End If

    End Sub

    Public Sub GetdataFromOtherForms(ByVal _DocType As gloEMRWord.enumDocType)
        oCurDoc.ActiveWindow.SetFocus()
        Dim objword As New clsWordDocument
        Dim objCriteria As New DocCriteria
        '00000498 : Liquid Link
        'Document category set as others instead of exams
        objCriteria.DocCategory = enumDocCategory.Others
        objCriteria.PatientID = m_PatientID
        If (IsNothing(dtLetterDate) = False) Then
            If IsNothing(dtLetterDate.Tag) Then ''condition added for bugid 87061  
                objCriteria.VisitID = GenerateVisitID(dtLetterDate.Value, m_PatientID)
            Else
                objCriteria.VisitID = dtLetterDate.Tag
            End If
        End If
      

        objCriteria.PrimaryID = 0
        objword.DocumentCriteria = objCriteria
        objword.CurDocument = oCurDoc
        objword.WaitControlPanel = Me.pnlwdNurseNotes
        objword.GetFormFieldData(_DocType)
        oCurDoc = objword.CurDocument
        'wdCtrlPatientConsent.document = objword.CurDocument
        objCriteria.Dispose()
        objCriteria = Nothing
        objword = Nothing


    End Sub
    Public Sub GetOrders()
        '' VisitID 
        Try
            dtLetterDate.Tag = GenerateVisitID(dtLetterDate.Value, m_PatientID)
        Catch ex As Exception

        End Try


        'If mgnVisitID <> 0 Then
        '        If Trim(strPatientFirstName) <> "" Then
        If m_PatientID <> 0 Then
            'Dim frmOrders As New frmVWOrders
            Dim frmOrders As frm_LM_Orders

            frmOrders = frm_LM_Orders.GetInstance(dtLetterDate.Tag, dtLetterDate.Value, m_PatientID, 0, False)
            If IsNothing(frmOrders) = True Then
                Exit Sub
            End If
            With frmOrders
                .MdiParent = Me.MdiParent
                .WindowState = FormWindowState.Maximized

                .myCaller1 = Me
                .Show()

            End With
            ''Added by Mayuri-Word Crash Issue-20120719
            If IsNothing(frmOrders) = False Then
                ' frmOrders.Dispose()
                frmOrders = Nothing
            End If
        Else
            MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    ''Bug #59396: 00000567 : Exam note not close instantly after opening up a finished exam note and than immediately closing the note Close Editor 
    ''Incident # 00020829: Closing Nurse notes taking time on terminal server.
    ''Comment the original code and added new code.
    ''Changes make to release the memory taken by TaskPanes by using Marshal.ReleaseComObject
    Private Sub tmrDocProtect_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDocProtect.Tick
        'Try
        '    If m_IsFinished = True And blnIsAddendum = False Then
        '        If Not oCurDoc Is Nothing Then
        '            oCurDoc.ActiveWindow.SetFocus()
        '            oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
        Try
            tmrDocProtect.Enabled = False
            If m_IsFinished = True And blnIsAddendum = False Then
                If Not oCurDoc Is Nothing Then
                    'Bug #85105: 00000876: Comment code for Document in procted mode while faxing
                    'oCurDoc.ActiveWindow.SetFocus()
                    Dim protectPane As Wd.TaskPane = oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection)
                    If (IsNothing(protectPane) = False) Then
                        protectPane.Visible = False
                        Marshal.ReleaseComObject(protectPane)
                        protectPane = Nothing
                    End If


                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            tmrDocProtect.Enabled = True

        End Try
    End Sub

    ''To load dynamically Patient Details and form specific Controls
    Private Sub loadPatientStrip()
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.NurseNotes)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(0, 0, 0, 0)
        pnlMain.Controls.Add(_PatientStrip)
        If m_IsFinished = True Then
            If (IsNothing(_PatientStrip) = False) Then
                _PatientStrip.DTPEnabled = False
            End If

        End If

    End Sub
    Private Sub UnDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub ImportDocument(ByVal nInsertScan As Int16)

        Try
            If nInsertScan = 1 Then
                Dim oFileDialogWindow As New System.Windows.Forms.OpenFileDialog
                oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf"
                oFileDialogWindow.FilterIndex = 3
                oFileDialogWindow.Title = "Insert External Documents"
                oFileDialogWindow.Multiselect = False
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)
                    If oFile.Extension.ToUpper = UCase(".Doc") Or oFile.Extension.ToUpper = UCase(".Docx") Or oFile.Extension.ToUpper = UCase(".txt") Or oFile.Extension.ToUpper = UCase(".rtf") Then

                        oCurDoc.ActiveWindow.SetFocus()

                        oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                    End If

                    'Memory Leak
                    oFile = Nothing
                End If
                'Memory Leak
                oFileDialogWindow.Dispose()
                oFileDialogWindow = Nothing


            ElseIf nInsertScan = 2 Then


                Dim oFiles As New ArrayList()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                oEDocument.ShowEScannerForImages(m_PatientID, oFiles)
                oEDocument.Dispose()
                Dim firstFlag As Boolean = True
                Dim i As Integer
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                For i = 0 To oFiles.Count - 1
                    If File.Exists(oFiles.Item(i)) Then
                        Dim oWord As New clsWordDocument
                        oWord.GetandSetMyFirstFlag(True, firstFlag)
                        oWord.CurDocument = oCurDoc
                        oWord.InsertImage(oFiles.Item(i))
                        firstFlag = oWord.GetandSetMyFirstFlag(False, False)
                        oWord = Nothing
                        oCurDoc.Application.Selection.EndKey()
                        oCurDoc.Application.Selection.InsertBreak()
                    End If
                Next
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                For i = oFiles.Count - 1 To 0 Step -1
                    If File.Exists(oFiles.Item(i)) Then
                        Try
                            Kill(oFiles.Item(i))
                        Catch

                        End Try
                    End If
                Next

                i = Nothing
                ''Memory Leak
                oFiles.Clear()
                oFiles = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
    End Sub

    Public Sub GetPrescription()
        Dim frmRxMeds As frmPrescription
        Try
            Try
                dtLetterDate.Tag = GenerateVisitID(dtLetterDate.Value, m_PatientID)
            Catch ex As Exception

            End Try


            'Incident #55315: 00016572 : Carry forward issue
            'Change done to make same behavior as Patient Exam.
            frmRxMeds = frmPrescription.GetInstance(CType(dtLetterDate.Tag, Long), CType(m_PatientID, Long))
            If IsNothing(frmRxMeds) = True Then
                Exit Sub
            End If

            If frmPrescription.IsOpen = False Then
                frmRxMeds.ShowMedication()

                If frmRxMeds.blncancel = True Then
                    With frmRxMeds
                        .WindowState = FormWindowState.Maximized
                        .myCallerN = Me
                        .blnOpenFromNurseNotes = True
                        .ShowDialog(IIf(IsNothing(frmRxMeds.Parent), Me, frmRxMeds.Parent))

                    End With
                    If IsNothing(frmRxMeds) = False Then ''slr free after showdialog
                        frmRxMeds.Dispose()
                        frmRxMeds = Nothing
                    End If
                End If
            Else
                MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            ''Added by Mayuri-Word Crash Issue-20120719
            'If IsNothing(frmP) = False Then  'SLR: This code should be after showdialog.. Not here.. Otherwise it will close somebody else opened form?
            '    frmP.Dispose()
            '    frmP = Nothing
            'End If
        End Try
        ''END'GLO2010-0004511 :: The patient meds seemed to have alter from 2 exmas
    End Sub

    Public Sub InsertSignature()
        Try
            ImagePath = ""

            Dim frm As New FrmSignature
            frm.ShowInTaskbar = False
            frm.Owner = Me
            ' frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.ShowDialog(frm.Parent)
            frm.Dispose()
            frm = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''Dhruv 20091214 To add the signature into the Word document
    Public Sub AddSignature(ByVal sImagePath As String) Implements ISignature.AddSignature

        If Not IsNothing(oCurDoc) Then
            If File.Exists(sImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(sImagePath)
                oWord = Nothing
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        End If

    End Sub
    Public Sub InsertCoSignature()
        Try
            ''slr free previous memory
            If Not IsNothing(objword) Then
                objword = Nothing
            End If

            ''slr free previous memory
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If

            objword = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = m_PatientID


            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID '' For inserting coSignature
            objword.DocumentCriteria = objCriteria

            ImagePath = objword.getData_FromDB("User_MST.imgSignature", "Co-Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            objword = Nothing
            ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            If System.IO.File.Exists(ImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()

                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(ImagePath)
                oWord = Nothing


                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' to insert user's signature
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InsertUserSignature(Optional ByVal ProviderID As Int64 = 0)
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            Dim objWord As New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            objCriteria.PatientID = m_PatientID

            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID
            objWord.DocumentCriteria = objCriteria

            objCriteria.ProviderID = ProviderID

            ImagePath = objWord.getData_FromDB("User_MST.imgSignature", "Provider Signature")

            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
            ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)
            If ImagePath = "" Then
                MessageBox.Show("Current user has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If File.Exists(ImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(ImagePath)
                oWord = Nothing

                Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                If wdRng.Tables.Count > 0 Then

                    oCurDoc.Application.Selection.EndKey()
                End If


                oCurDoc.Application.Selection.TypeParagraph()

                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Nurse Notes", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub
    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
    Private Function AddChildMenu() As DataTable
        Dim oProvider As New clsProvider
        Try

            Dim rslt As Boolean
            rslt = oProvider.CheckSignDelegateStatus()
            If rslt Then
                'Memory Leak
                Dim dt As DataTable
                dt = oProvider.GetAllAssignProviders(gnLoginID)
                'Memory Leak
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
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            ''Added by Mayuri-Word Crash Issue-20120719
            If IsNothing(oProvider) = False Then
                oProvider.Dispose()
                oProvider = Nothing
            End If
        End Try
    End Function
    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            Dim objWord As clsWordDocument
            objWord = New clsWordDocument
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, m_PatientID, 0, blnSignClick)
            If pSign(2) = "1" Then
                If File.Exists(pSign(0)) Then
                    oCurDoc.ActiveWindow.SetFocus()

                    objWord.CurDocument = oCurDoc
                    Dim myType As Wd.WdViewType = Nothing
                    Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                    objWord.InsertImage(pSign(0))
                    objWord = Nothing
                    Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                    If wdRng.Tables.Count > 0 Then

                        oCurDoc.Application.Selection.EndKey()
                    End If

                    oCurDoc.Application.Selection.TypeParagraph()
                    oCurDoc.Application.Selection.TypeText(Text:=pSign(1))
                    gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            ''Added by Mayuri-Word Crash Issue-20120719
            If IsNothing(objword) = False Then
                objword = Nothing
            End If

        End Try
    End Sub

    Private Sub FaxNurseNotes(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)
        mdlFAX.Owner = Me
        If RetrieveFAXDetails(mdlFAX.enmFAXType.NurseNotes, m_PatientID, "", "", cmbTemplate.SelectedItem(1), 0, 0, 0, True, Me) = False Then
            Exit Sub
        End If
        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

        ''Unprotect the document
        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If




        'Send the document for Printing i.e. to generate the TIFF File
        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, m_PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, cmbTemplate.Text, clsPrintFAX.enmFAXType.NurseNotes) = False Then
            'TIFF File has not been created
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        'Memory Leak
        objPrintFAX.Dispose()
        objPrintFAX = Nothing
    End Sub

    Private Function SaveNurseNotes(ByVal IsFinished As Boolean, Optional ByVal IsClose As Boolean = False) As Boolean

        Try

            If oCurDoc Is Nothing Then
                Return False
            End If

            SaveNurseNotes = False
            ' oCurDoc.Application.ScreenUpdating = False

            '' IF IsFinished = True Then Remove the Unused Fields 
            oCurDoc.ActiveWindow.SetFocus()
            If IsFinished Then

                'gloWord.LoadAndCloseWord.LockFields(oCurDoc)

                If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                    oCurDoc.Application.ActiveDocument.Unprotect()
                End If
                'objword = New clsWordDocument
                'objword.CurDocument = oCurDoc
                'objword.CleanupDoc()
                'oCurDoc = objword.CurDocument
                'objword = Nothing
                gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)
                gloWord.LoadAndCloseWord.LockFields(oCurDoc)

            End If

            If pnlGloUC_TemplateTreeControl.Width.ToString <> "" Then
                objclsNotes.SaveWidthInDatabase(gnLoginID, pnlGloUC_TemplateTreeControl.Width) ''''save width of panel in DB
            End If


            'If (IsNothing(oCurDoc) = False) Then
            '    oCurDoc.Save()
            'Else
            ' wdNurseNotes.Save()
            'gloWord.LoadAndCloseWord.SaveDSO(wdNurseNotes, oCurDoc, oWordApp)
            ''End If

            'Dim strFileName As String
            'Dim isExceptionWhileCopingFile As Boolean = False
            'strFileName = ExamNewDocumentName

            'Try
            '    FileSystem.FileCopy(oCurDoc.FullName, strFileName)
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    ex = Nothing
            '    isExceptionWhileCopingFile = True
            'End Try
            'If (isExceptionWhileCopingFile) Then
            '    oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            '    wdNurseNotes.Close()
            'End If
            Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdNurseNotes, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, IsClose)

            Dim myBinaray As Object = Nothing
            If (IsNothing(myByte) = False) Then
                myBinaray = CType(myByte, Object)
            End If
            m_NurseNotesID = objclsNotes.SaveNurseNotesBytes(m_NurseNotesID, m_PatientID, cmbTemplate.SelectedValue, Format(dtLetterDate.Value, "MM/dd/yyyy hh:mm tt"), myBinaray, cmbTemplate.Text, IsFinished, cmbTemplate.Text)

            If m_NurseNotesID > 0 Then
                If IsClose Then


                    If (IsNothing(oCurDoc) = False) Then
                        Try
                            Marshal.ReleaseComObject(oCurDoc)
                        Catch ex As Exception


                        End Try
                        oCurDoc = Nothing

                    End If
                Else
                    'If (isExceptionWhileCopingFile) Then
                    '    LoadWordUserControl(strFileName, False)
                    'End If

                    cmbTemplate.Enabled = False
                    dtLetterDate.Enabled = False

                    'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    oCurDoc.Saved = True

                End If
            Else
                If IsClose Then

                    If (IsNothing(oCurDoc) = False) Then
                        Try
                            Marshal.ReleaseComObject(oCurDoc)
                        Catch ex As Exception


                        End Try
                        oCurDoc = Nothing

                    End If
                Else
                    If (IsNothing(oCurDoc) = False) Then
                        oCurDoc.Saved = False
                    End If
                End If

            End If
            If IsFinished Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.Add, "Nurse notes finished", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                'Else
                '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.Add, "Nurse notes saved", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            'If IO.File.Exists(strFileName) Then
            '    Try
            '        IO.File.Delete(strFileName)
            '    Catch ex As Exception
            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '        ex = Nothing
            '    End Try
            'End If
            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        End Try
    End Function

    Private Sub Print()

        If Not oCurDoc Is Nothing Then

            '   oCurDoc.Application.ScreenUpdating = False
            GeneratePrintFaxDocument()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.Print, "Nurses Notes'" & cmbTemplate.Text & "' Printed.", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            '  oCurDoc.Application.ScreenUpdating = True

        End If

    End Sub

    Private Sub GeneratePrintFaxDocument(Optional ByVal IsPrintFlag As Boolean = True)

        Dim _SaveFlag As Boolean = False
        Dim PageNo As Integer = 0
        Dim totalPages As Integer = 0
        Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
        Dim Missing As Object = System.Reflection.Missing.Value

        Try

            If Not oCurDoc Is Nothing Then

                If oCurDoc.Saved Then
                    _SaveFlag = True
                End If

                If IsNothing(wdNurseNotes) = False AndAlso IsNothing(oWordApp) = False Then

                    Try
                        gloWord.LoadAndCloseWord.SaveDSO(wdNurseNotes, oCurDoc, oWordApp)
                    Catch ex As Exception

                    End Try
                    If (IsPrintFlag) Then
                        Try
                            PageNo = oCurDoc.Application.Selection.Information(Microsoft.Office.Interop.Word.WdInformation.wdActiveEndPageNumber)
                        Catch ex As Exception

                        End Try
                        Try
                            totalPages = oCurDoc.ComputeStatistics(PageCountStat, Missing)
                        Catch ex As Exception

                        End Try

                    End If

                    Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()

                    Try
                        PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, m_PatientID, AddressOf FaxNurseNotes, totalPages, PageNo:=PageNo, iOwner:=Me)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try


                    myLoadWord.CloseApplicationOnly()
                    myLoadWord = Nothing
                    If Not IsNothing(oCurDoc) Then
                        oCurDoc.Saved = _SaveFlag
                    End If

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Nurses Notes", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub InsertAddendum()
        Try
            If m_IsFinished = True Then
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Visible = False
                End If

                pnlWordToolStrip.Visible = False
                pnlGloUC_PastWordNotes.Visible = False

                ouctlgloUC_Addendum = New gloUC_Addendum(0, m_NurseNotesID, m_PatientID)
                blnIsAddendum = True
                pnlgloUC_Addendum.Controls.Add(ouctlgloUC_Addendum)
                ouctlgloUC_Addendum.Dock = DockStyle.Fill
                ouctlgloUC_Addendum.BringToFront()

                pnlgloUC_Addendum.Visible = True
                pnlgloUC_Addendum.BringToFront()
                GloUC_TemplateTreeControl_NursesNotes.Visible = False

                If gblnSpeakerExists = True And gblnVoiceEnabled = True Then
                    InitializeVoiceObjectForAddendum()
                    ShowMicroPhone()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        'oCurDoc1.Application.Selection.InsertRows(1)

    End Sub

    Public Sub Navigate1(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate

        Try


            If strstring = "ON" Then

                If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsNurseNotes.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsNurseNotes.MyToolStrip.Items("Mic").Visible = True
                        tlsNurseNotes.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                        tlsNurseNotes.MyToolStrip.ButtonsToHide.Remove(tlsNurseNotes.MyToolStrip.Items("Mic").Name)
                    End If


                ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic") Is Nothing Then
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Visible = True
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.ButtonsToHide.Remove(Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Name)
                    End If

                End If

            ElseIf strstring = "OFF" Then
                If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsNurseNotes.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsNurseNotes.MyToolStrip.Items("Mic").Visible = True
                        tlsNurseNotes.MyToolStrip.ButtonsToHide.Remove(tlsNurseNotes.MyToolStrip.Items("Mic").Name)
                    End If


                ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic") Is Nothing Then
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Visible = True
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.ButtonsToHide.Remove(Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Name)
                    End If

                    Exit Sub
                Else

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsNurseNotes.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsNurseNotes.MyToolStrip.Items("Mic").Visible = False
                        If tlsNurseNotes.MyToolStrip.ButtonsToHide.Contains(tlsNurseNotes.MyToolStrip.Items("Mic").Name) = False Then
                            tlsNurseNotes.MyToolStrip.ButtonsToHide.Add(tlsNurseNotes.MyToolStrip.Items("Mic").Name)
                        End If
                    End If


                End If

                '04-Jul-14 Aniket: Resolving Bug #67037
                If Not tlsNurseNotes.MyToolStrip.Items("Mic") Is Nothing Then
                    tlsNurseNotes.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
                End If

            Else
                If bnlIsFaxOpened = False Then
                    If Not IsNothing(ouctlgloUC_Addendum) Then
                        ouctlgloUC_Addendum.Navigate(strstring)
                    Else
                        oCurDoc.ActiveWindow.SetFocus()
                        Try
                            If Not oCurDoc Is Nothing Then
                                gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try

                    End If
                Else
                    For Each frm As Form In Application.OpenForms
                        If frm.Name = "frmSelectContactFAXWithFAXCoverPage" Then
                            If Not IsNothing(DirectCast(frm, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc) Then
                                Try
                                    DirectCast(frm, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc.ActiveWindow.SetFocus()
                                    gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=DirectCast(frm, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                                    Exit For
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        End If
                    Next
                End If


            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        '  Dim wordOptimizer As New WordRefresh()
        ' Dim WDocViewType As Wd.WdViewType
        Try
            ' wordOptimizer.ShowPanel(Me.pnlwdNurseNotes)

            If Not blnCmbSelTemplate Then
                loadToolStrip()
            End If

            ' wdNurseNotes.Open(strFileName)
            '  Dim oWordApp As Wd.Application = Nothing
            Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdNurseNotes, strFileName, oCurDoc, oWordApp)
            If (strError <> String.Empty) Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, strError, gloAuditTrail.ActivityOutCome.Failure)

            Else

                '   oCurDoc.Application.ScreenUpdating = False

                If blnGetData Then

                    ''//To retrieve the Form fields for the Word document
                    ''slr free previous memory
                    If Not IsNothing(objword) Then
                        objword = Nothing
                    End If

                    ''slr free previous memory
                    If Not IsNothing(objCriteria) Then
                        objCriteria.Dispose()
                        objCriteria = Nothing
                    End If

                    objword = New clsWordDocument
                    objCriteria = New DocCriteria
                    objCriteria.DocCategory = enumDocCategory.Others

                    objCriteria.PatientID = m_PatientID

                    m_visitID = GenerateVisitID(m_PatientID)
                    Try
                        dtLetterDate.Tag = GenerateVisitID(dtLetterDate.Value, m_PatientID)
                        objCriteria.VisitID = dtLetterDate.Tag
                    Catch ex As Exception

                    End Try
                   

                    objCriteria.PrimaryID = 0
                    objword.DocumentCriteria = objCriteria
                    objword.CurDocument = oCurDoc


                    'WDocViewType = oCurDoc.ActiveWindow.View.Type
                    'wordOptimizer.OptimizePerformance(False, oCurDoc, 0)
                    objword.GetFormFieldData(enumDocType.None)
                    oCurDoc = objword.CurDocument
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                    objCriteria.Dispose()
                    objCriteria = Nothing
                    ''Added by Mayuri-Word Crash Issue-20120719
                    objword = Nothing
                Else
                    ''slr free previous memory
                    If Not IsNothing(objword) Then
                        objword = Nothing
                    End If

                    objword = New clsWordDocument
                    objword.CurDocument = oCurDoc
                    objword.HighlightColor()
                    oCurDoc = objword.CurDocument
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                    objword = Nothing
                End If

                SetWordObjectEntry(m_IsFinished)

              

                '     oCurDoc.Application.ScreenUpdating = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            'UpdateLog("aField.StatusText - " & aField.StatusText & " - " & ex.ToString)
        Finally
            'wordOptimizer.OptimizePerformance(True, oCurDoc, WDocViewType)
            'wordOptimizer.HidePanel(Me.pnlwdNurseNotes)
            'wordOptimizer.Dispose()
            'wordOptimizer = Nothing
        End Try

    End Sub

    Private Sub loadToolStrip()

        Dim dsProviderBasicDetails As DataSet

        If Not tlsNurseNotes Is Nothing Then
            tlsNurseNotes.Dispose()
        End If

        tlsNurseNotes = New WordToolStrip.gloWordToolStrip
        tlsNurseNotes.Dock = DockStyle.Top
        tlsNurseNotes.ConnectionString = GetConnectionString()
        tlsNurseNotes.UserID = gnLoginID
        ''
        tlsNurseNotes.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider

        dsProviderBasicDetails = oclsProvider.GetPatientProviderDetails(m_PatientID)

        If dsProviderBasicDetails.Tables("PatientProviderBasicDetails").Rows.Count > 0 Then
            tlsNurseNotes.ptProvider = dsProviderBasicDetails.Tables("PatientProviderBasicDetails").Rows(0)("sProviderName") ''oclsProvider.GetPatientProviderName(m_PatientID)
            tlsNurseNotes.ptProviderId = dsProviderBasicDetails.Tables("PatientProviderBasicDetails").Rows(0)("nProviderID") ' oclsProvider.GetPatientProvider(m_PatientID)
        End If

        If IsNothing(dsProviderBasicDetails) = False Then
            dsProviderBasicDetails.Dispose()
            dsProviderBasicDetails = Nothing
        End If


        tlsNurseNotes.BringToFront()
        pnlCombo.BringToFront()
        pnlwdNurseNotes.BringToFront()

        If m_IsFinished Then

            tlsNurseNotes.FormType = WordToolStrip.enumControlType.Addendum
            cmbTemplate.Enabled = False
            dtLetterDate.Enabled = False
        Else
            tlsNurseNotes.IsCoSignEnabled = gblnCoSignFlag
            tlsNurseNotes.FormType = WordToolStrip.enumControlType.PatientConsent
            cmbTemplate.Enabled = True
            dtLetterDate.Enabled = True
        End If


        Me.pnlWordToolStrip.Controls.Add(tlsNurseNotes)
        Me.pnlWordToolStrip.Size = New System.Drawing.Size(940, 56)
        pnlWordToolStrip.SendToBack()
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsNurseNotes
                ShowMicroPhone()
            End If
        End If
        'Memory Leak
        Dim dt As DataTable
        dt = oclsProvider.GetAllAssignProviders(gnLoginID)
        ''Added by Mayuri-Word Crash Issue-20120719
        oclsProvider.Dispose()
        oclsProvider = Nothing
        If gblnAssociatedProviderSignature And m_IsFinished = False And m_IsRecordLock = False Then
            tlsNurseNotes.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            If dt.Rows.Count > 0 Then
                tlsNurseNotes.MyToolStrip.Items("Insert Associated Provider Signature").Enabled = True
            Else

            End If
            tlsNurseNotes.MyToolStrip.ButtonsToHide.Remove(tlsNurseNotes.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        Else
            tlsNurseNotes.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False

            If (tlsNurseNotes.MyToolStrip.ButtonsToHide.Contains(tlsNurseNotes.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsNurseNotes.MyToolStrip.ButtonsToHide.Add(tlsNurseNotes.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If
            ''When it is finished it should hide the save button and when it is not finished it should show the button
            If m_IsFinished = True Then
                tlsNurseNotes.MyToolStrip.ButtonsToHide.Add(tlsNurseNotes.MyToolStrip.Items("Save").Name) ''Using Only add it will shown on the toolstrip
                tlsNurseNotes.MyToolStrip.Items("Save").Visible = False
            Else
                If (tlsNurseNotes.MyToolStrip.ButtonsToHide.Contains(tlsNurseNotes.MyToolStrip.Items("Save").Name) = False) Then
                    '' tlsNurseNotes.MyToolStrip.ButtonsToHide.Add(tlsNurseNotes.MyToolStrip.Items("Save").Name) ''Using Only add it will shown on the toolstrip
                    tlsNurseNotes.MyToolStrip.Items("Save").Visible = True
                    tlsNurseNotes.MyToolStrip.ButtonsToHide.Remove(tlsNurseNotes.MyToolStrip.Items("Save").Name) '' Using this Code it will add into Customized tool strip control
                End If
            End If
        End If
        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
            If tlsNurseNotes.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                If (tlsNurseNotes.MyToolStrip.ButtonsToHide.Contains(tlsNurseNotes.MyToolStrip.Items("SecureMsg").Name) = False) Then
                    tlsNurseNotes.MyToolStrip.ButtonsToHide.Add(tlsNurseNotes.MyToolStrip.Items("SecureMsg").Name)
                End If
            End If
            If tlsNurseNotes.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                tlsNurseNotes.MyToolStrip.Items("SecureMsg").Visible = False
            End If
        End If
        isRecordLocked()
        ''Added by Mayuri-Word Crash Issue-20120719
        If IsNothing(dt) = False Then
            dt.Dispose()
            dt = Nothing
        End If
    End Sub
    Private Sub tlsNurseNotes_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsNurseNotes.ToolStripButtonClick
        If IsNothing(oCurDoc) = False Then
            InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
        End If
    End Sub

    Private Sub wdNurseNotes_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdNurseNotes.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    isHandlerRemoved = True
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                frmPatientExam.blnIsHandlers = True
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdNurseNotes_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdNurseNotes.OnDocumentClosed
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    '     Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            'UpdateVoiceLog(ex.ToString)
        Finally
            'GC.Collect()  ''slr for memory management
            'GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub wdNurseNotes_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdNurseNotes.OnDocumentOpened

        oCurDoc = wdNurseNotes.ActiveDocument
        oWordApp = oCurDoc.Application

        Try
            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        Try
            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        isHandlerRemoved = False
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False

    End Sub

    '''' <summary>
    '''' To implemt the Dropdown and check Box selection change event
    '''' </summary>
    '''' <param name="Sel"></param>
    '''' <remarks></remarks>
    Private Sub DDLCBEvent(ByVal Sel As Wd.Selection)
        'Memory Leak
        Dim r As Wd.Range = Nothing
        '  Dim om As Object = System.Reflection.Missing.Value
        Dim f As Wd.FormField = Nothing
        Dim o As Object = 1
        Dim oUnit As Object = Wd.WdUnits.wdCharacter
        Dim oCnt As Object = 1
        Dim oMove As Object = Wd.WdMovementType.wdMove

        Try
            If IsNothing(Sel) Then
                Return
            End If
            If (Sel.Type <> Microsoft.Office.Interop.Word.WdSelectionType.wdNoSelection) Then
                If Sel.Start = Sel.End Then
                    '  Dim r As Wd.Range = Nothing
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
                    '  r.SetRange(Sel.Start, Sel.End + 1)
                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then





                        Try
                            'Dim o As Object = 1
                            f = r.FormFields.Item(o)
                            'o = Nothing
                        Catch

                        End Try

                        If (IsNothing(f) = False) Then
                            If f.Type = Wd.WdFieldType.wdFieldFormCheckBox Then
                                f.CheckBox.Value = Not f.CheckBox.Value
                                Sel.MoveRight(oUnit, oCnt, oMove)
                            End If
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            r = Nothing
            ' om = Nothing
            f = Nothing
            o = Nothing
            oUnit = Nothing
            oCnt = Nothing
            oMove = Nothing
        End Try

    End Sub

    '''' <summary>
    '''' To raise the click event for drop down list
    '''' </summary>
    '''' <param name="btn"></param>
    '''' <param name="Cancel"></param>
    '''' <remarks></remarks>
    'Private Sub btn_Click(ByVal btn As oOffice.CommandBarButton, ByRef Cancel As Boolean)
    '    myidx = btn.Index
    'End Sub

    Private Sub tlsPatientConsent_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsNurseNotes.ToolStripClick
        Try


            Select Case e.ClickedItem.Name
                Case "Mic"
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic started from tblButtons_ButtonClick in NurseNotes when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                    'UpdateVoiceLog("--------------SwitchOff Mic started from tblButtons_ButtonClick in Patient Messages when " & e.ClickedItem.Name & " is invoked------------")
                    If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                        e.ClickedItem.ToolTipText = "Microphone Off"
                    ElseIf MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                        e.ClickedItem.ToolTipText = "Microphone On"
                    End If
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic Completed from tblButtons_ButtonClick in NurseNotes when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                Case "Save"
                    Try
                        _IsSave = True

                        Me.Cursor = Cursors.WaitCursor
                        TurnoffMicrophone()
                        If m_IsFinished Then
                            Call SaveNurseNotes(True)
                        Else
                            Call SaveNurseNotes(False)
                        End If

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                Case "Save & Close"
                    Try
                        _IsSave = True
                        Me.Cursor = Cursors.WaitCursor

                        Call SaveNurseNotes(False, True)
                        Me.Close()

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                Case "Print"
                    TurnoffMicrophone()
                    Call Print()
                    cmbTemplate.Enabled = False
                    dtLetterDate.Enabled = False

                Case "FAX"
                    bnlIsFaxOpened = True
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        TurnoffMicrophone()
                        Call GeneratePrintFaxDocument(False)
                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    cmbTemplate.Enabled = False
                    dtLetterDate.Enabled = False
                    bnlIsFaxOpened = False
                Case "Insert Sign"
                    'Call InsertProviderSignature()
                    If IsNothing(oCurDoc) = False Then
                        blnSignClick = True
                        If gnLoginProviderID > 0 Then
                            InsertProviderSignature(gnLoginProviderID)
                        Else
                            InsertUserSignature()
                        End If
                        blnSignClick = False
                        'end code added by dipak 20100105
                    End If
                    'case added by dipak 20100105 for ProviderSign 
                Case "Insert Associated Provider Signature"
                    If IsNothing(oCurDoc) = False Then
                        InsertProviderSignature()
                    End If
                Case "Insert CoSign"
                    Call InsertCoSignature()
                Case "Capture Sign"
                    Call InsertSignature()
                Case "Undo"
                    Call UnDoChanges()
                Case "Redo"
                    Call ReDoChanges()
                Case "Insert File"
                    TurnoffMicrophone()
                    ImportDocument(1)
                Case "Scan Documents"
                    TurnoffMicrophone()
                    ImportDocument(2)
                Case "Close"
                    If _IsSave = False Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.View, "Nurse notes viewed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                    Me.Close()
                Case "Prescription"
                    ' MsgBox("Prescription")
                    TurnoffMicrophone()
                    Call GetPrescription()

                Case "OrderTemplates"
                    ' MsgBox("Orders")
                    TurnoffMicrophone()
                    Call GetOrders()
                Case "Save & Finish"
                    Try
                        Me.Cursor = Cursors.WaitCursor

                        Call SaveNurseNotes(True, True)
                        Me.Close()

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                Case "Add Addendum"
                    Call InsertAddendum()

                Case "tblbtn_StrikeThrough"
                    '' chetan added on 25-oct-2010 for Strike Through
                    InsertStrike()
                Case "Export"

                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Nurses Note", Me)
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    objword1 = Nothing

                Case "SecureMsg"
                    If strProviderDirectAddress <> "" Then
                        Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(m_PatientID)
                        If sError <> "" Then
                            MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                            Return
                        Else
                            TurnoffMicrophone()
                            Call SendSecureMsg()
                            cmbTemplate.Enabled = False
                            dtLetterDate.Enabled = False
                        End If

                    Else
                        MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Nurses Notes", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub SendSecureMsg()

        If Not oCurDoc Is Nothing Then
            GenerateSecureMsgDocument()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.ClinicalExchange, "Send Nurse Note using Secure Message", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        End If

    End Sub
    Private Sub GenerateSecureMsgDocument()
        Dim _SaveFlag As Boolean = False
        If oCurDoc.Saved Then
            _SaveFlag = True
        End If
        Try
            gloWord.LoadAndCloseWord.SaveDSO(wdNurseNotes, oCurDoc, oWordApp)
        Catch ex As Exception

        End Try

        'Try
        '    oCurDoc.SaveAs(oCurDoc.FullName)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    Try
        '        oCurDoc.Save()
        '    Catch ex1 As Exception

        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        '    End Try
        'End Try
        '   Dim sFileName As String = ExamNewDocumentName

        '  wdPTProtocols.Save(sFileName, True, "", "")
        'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

        'wdNurseNotes.Close()

        'wdTemp = New AxDSOFramer.AxFramerControl

        'Me.Controls.Add(wdTemp)


        'wdTemp.Open(sFileName)
        '  oTempDoc = wdTemp.ActiveDocument
        ' oTempDoc.ActiveWindow.SetFocus()
        Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
        Dim osenddox As String = String.Empty
        Try
            osenddox = SendWord.MdlSendWord.SendWordDocument(myLoadWord, oCurDoc.FullName, m_PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

        'Dim oSendDoc As New clsPrintFAX
        'Dim osenddox As String
        'osenddox = oSendDoc.SendDoc(oTempDoc, m_PatientID)
        ''Memory Leak
        'oSendDoc.Dispose()
        'oSendDoc = Nothing
        'wdTemp.Close()
        'Me.Controls.Remove(wdTemp)
        'wdTemp.Dispose()
        'wdTemp = Nothing
        'myLoadWord.CloseWordApplication(oTempDoc)
        myLoadWord.CloseApplicationOnly()
        myLoadWord = Nothing
        oCurDoc.Saved = _SaveFlag
        ''Read Secure Messages settings and call Inbox form
        If osenddox.Length > 0 Then
            If File.Exists(osenddox) Then
                Dim ofrmSendNewMail As New InBox.NewMail(m_PatientID, osenddox)
                ofrmSendNewMail.ShowInTaskbar = True
                ofrmSendNewMail.ShowDialog()
                'ofrmInbox.Dispose()
                If Not IsNothing(ofrmSendNewMail) Then
                    ofrmSendNewMail.Close() ''slr close it
                End If
                ofrmSendNewMail = Nothing
            Else
                MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        'LoadWordUserControl(sFileName, False)
        'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
        ''Set the Start postion of the cursor in documents
        'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        'oCurDoc.Saved = _SaveFlag
    End Sub
    '' chetan added on 25-oct-2010 for Strike Through
    Private Sub InsertStrike()
        Try
            Dim strThrough As String
            If Not IsNothing(oCurDoc) Then
                If Not IsNothing(oCurDoc.Application.Selection) Then
                    If oCurDoc.Application.Selection.Characters.Count - 1 > 0 Then
                        strThrough = "Strikethrough by " & gstrLoginName & " on " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time")
                        tmrDocProtect.Enabled = False
                        If oCurDoc.Application.ActiveDocument.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyComments Then
                            oCurDoc.Application.ActiveDocument.Unprotect()
                        End If
                        oCurDoc.Application.Selection.Range.Font.DoubleStrikeThrough = True
                        oCurDoc.Application.Selection.Move(1)
                        oCurDoc.Application.Selection.TypeParagraph()
                        oCurDoc.Application.Selection.Font.DoubleStrikeThrough = False
                        oCurDoc.Application.Selection.TypeText(Text:=strThrough)
                        oCurDoc.Application.Selection.Move(1)
                        oCurDoc.Application.Selection.TypeParagraph()
                        If m_IsFinished = True Then
                            oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)

                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If m_IsFinished = True Then
                tmrDocProtect.Enabled = True
            End If

        End Try
    End Sub


    Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectionChangeCommitted
        Try
            If cmbTemplate.SelectedValue > 0 Then
                blnCmbSelTemplate = True
                Fill_TemplateGallery()
                blnCmbSelTemplate = False
            Else
                wdNurseNotes.Close()
            End If
        Catch ex As Exception
            blnCmbSelTemplate = False
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            ImagePath = Value
        End Set
    End Property

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()


        InitializeComponent()

        m_PatientID = PatientID

    End Sub


    Public Sub New(ByVal NotesID As Long, ByVal TemplateID As Long, ByVal IsFinished As Boolean, ByVal IsRecordLock As Boolean, ByVal PatientID As Int64)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_NurseNotesID = NotesID
        m_TemplateID = TemplateID
        m_IsFinished = IsFinished
        m_PatientID = PatientID
        m_IsRecordLock = IsRecordLock
    End Sub
    ''' <summary>
    ''' Activate Template Voice Commands
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    Public Sub ActivateBasicVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateBasicVoiceCmds(VoiceCol)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Activate Voice Commands
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateVoiceCmds(VoiceCol)
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateVoiceCmds(VoiceCol)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Add Voice Commands
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.AddVoiceCommands()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.AddVoiceCommands()

            End If
        End If
    End Sub

    Public Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent) Implements mdlgloVoice.gloVoice.CustomGetchanges

    End Sub

    Public Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent) Implements mdlgloVoice.gloVoice.CustomMakechanges

    End Sub

    ''' <summary>
    ''' Turn ON Microphone
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone
        Try
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.ShowMicroPhone()
                End If
            ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.ShowMicroPhone()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub


    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone
        Try
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.TurnoffMicrophone()
                End If

            ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.TurnoffMicrophone()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Initialise Voice Components
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Private Sub InitializeVoiceObject()
        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If
        Dim oHashtable As Hashtable = AddBasicVoiceCommands()
        ogloVoice = New ClsVoice(oHashtable)
        ogloVoice.eVoiceAddendum = VoiceAddendum.eVoice
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.NurseNotes
        ogloVoice.MyWordToolStrip = Me.tlsNurseNotes
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "NurseNotes"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsPatientConsent_ToolStripClick)

        oHashtable.Clear()
        oHashtable = Nothing

    End Sub

    ''' <summary>
    ''' Initialise Voice Componenets for Addendum
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Private Sub InitializeVoiceObjectForAddendum()
        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If


        Dim oAddendumHashtable As ArrayList = ouctlgloUC_Addendum.FillTemplateCommands(True)
        ogloVoice = New ClsVoice(oAddendumHashtable)
        ogloVoice.gloTreeView = ouctlgloUC_Addendum.trvTemplates
        ogloVoice.eVoiceAddendum = VoiceAddendum.eAddendum
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.NurseNotes
        ogloVoice.MyWordToolStrip = Me.ouctlgloUC_Addendum.tlsAddendum
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "NurseNotes"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf Me.ouctlgloUC_Addendum.onToolStripClick)
        ogloVoice.AddVoiceCommands()

        'Memory Leak
        oAddendumHashtable.Clear()
        oAddendumHashtable = Nothing
    End Sub

    ''' <summary>
    ''' Add Basic Voice Commands
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Private Function AddBasicVoiceCommands() As Hashtable

        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save NurseNotes", "Save")
        oHashtable.Add("Print NurseNotes", "Print")
        oHashtable.Add("Fax NurseNotes", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close NurseNotes", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close NurseNotes", "Close")
        oHashtable.Add("Finish NurseNotes", "Save & Finish")
        oHashtable.Add("Prescription", "Prescription")


        Return oHashtable
    End Function

    ''' <summary>
    ''' Close Addendum user control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub ouctlgloUC_Addendum_OnAddendumClose(ByVal sender As Object, ByVal e As System.EventArgs) Handles ouctlgloUC_Addendum.OnAddendumClose
        pnlgloUC_Addendum.Controls.Remove(ouctlgloUC_Addendum)
        pnlWordToolStrip.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        pnlGloUC_PastWordNotes.Visible = True
        'GloUC_PastWordNotes1.Visible = True
        pnlgloUC_Addendum.Visible = False
        blnIsAddendum = False
        TurnoffMicrophone()
        If (IsNothing(ouctlgloUC_Addendum) = False) Then
            ouctlgloUC_Addendum.Dispose()
            ouctlgloUC_Addendum = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Save Addendum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub ouctlgloUC_Addendum_OnAddendumSaved(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ouctlgloUC_Addendum.OnAddendumSaved
        If File.Exists(ouctlgloUC_Addendum.FilePath) Then
            oCurDoc.ActiveWindow.SetFocus()
            If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Unprotect()
                oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:="Addendum - '" & gstrLoginName & "' " & Now)
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.ActiveDocument.Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=1, NumColumns:=1)
                oCurDoc.Application.Selection.InsertFile(ouctlgloUC_Addendum.FilePath)
                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
            End If
        End If

        pnlgloUC_Addendum.Controls.Remove(ouctlgloUC_Addendum)
        pnlWordToolStrip.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        pnlgloUC_Addendum.Visible = False
        pnlGloUC_PastWordNotes.Visible = True
        'GloUC_PastWordNotes1.Visible = True
        blnIsAddendum = False
        TurnoffMicrophone()
        If (IsNothing(ouctlgloUC_Addendum) = False) Then
            ouctlgloUC_Addendum.Dispose()
            ouctlgloUC_Addendum = Nothing
        End If
    End Sub

    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
        End Get
    End Property

    Private Sub pnlWordToolStrip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlWordToolStrip.Click

    End Sub

    '
    'Sanjog - Added on 2011 March 14 for History Confirmation box

    Private Sub frmNurseNotes_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If IsModify = False Then
                If Not IsNothing(objword) Then  ''slr free prev memory
                    objword = Nothing
                End If
                objword = New clsWordDocument(m_PatientID)

                If GetPatientHistory(dtLetterDate.Value) = True Then

                    blnOpenHistory = True
                    'End If
                End If
                If blnOpenHistory = True Then
                    GetHistory()
                End If
                blnOpenHistory = False

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Function GetPatientHistory(ByVal dtDOS As Date) As Boolean
        Dim cls As New clsPatientExams
        'Memory Leak
        Dim dt As DataTable
        dt = cls.getPatientHistory(m_PatientID)
        If dt.Rows.Count > 0 Then
            dt = cls.getHistoryVisitsAfterDate(dtDOS, m_PatientID)
            If dt.Rows.Count > 0 Then
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(cls) Then
                    cls.Dispose()
                    cls = Nothing
                End If
                Return False
            Else
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(cls) Then
                    cls.Dispose()
                    cls = Nothing
                End If
                Return True
            End If
        Else
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(cls) Then
                cls.Dispose()
                cls = Nothing
            End If
            Return False
        End If
        'If Not IsNothing(dt) Then
        '    dt.Dispose()
        '    dt = Nothing
        'End If
        'If Not IsNothing(cls) Then
        '    cls.Dispose()
        '    cls = Nothing
        'End If
    End Function
    'Sanjog - Added on 2011 March 14 for History Confirmation box

    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    'Private Function Get_PatientDetails()
    '    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
    '    Dim oParameters As New gloDatabaseLayer.DBParameters()
    '    Dim dtPatient As DataTable = Nothing
    '    Try
    '        oDB.Connect(False)
    '        oParameters.Add("@PatientID", m_PatientID, ParameterDirection.Input, SqlDbType.BigInt)
    '        oDB.Retrive("gsp_PatientInfo", oParameters, dtPatient)
    '        oDB.Disconnect()

    '        If IsNothing(dtPatient) = False Then
    '            If dtPatient.Rows.Count > 0 Then
    '                strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Finally
    '        If Not IsNothing(oParameters) Then
    '            oParameters.Dispose() : oParameters = Nothing
    '        End If
    '        If oDB IsNot Nothing Then
    '            oDB.Dispose() : oDB = Nothing
    '        End If
    '        If IsNothing(dtPatient) = False Then
    '            dtPatient.Dispose()
    '            dtPatient = Nothing
    '        End If
    '    End Try
    '    Return Nothing
    'End Function

    Private Sub btnPastExams_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPastExams.Click
        Try
            If btnPastExams.Text = "Show Past Nurses Notes" Then
                pnlGloUC_PastWordNotes.Show()
                btnPastExams.Text = "Hide Past Nurses Notes"
                bIsPastExamClick = True
            ElseIf btnPastExams.Text = "Hide Past Nurses Notes" Then
                pnlGloUC_PastWordNotes.Hide()
                btnPastExams.Text = "Show Past Nurses Notes"
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            ' MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Treeview_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs, ByVal sFilename As System.String) Handles GloUC_TemplateTreeControl_NursesNotes.Treeview_NodeMouseDoubleClick
        'Added Code To Open Templete in Word Document
        Dim blnRefreshDocument As Boolean

        Try
            oCurDoc = wdNurseNotes.ActiveDocument
            ' oCurDoc.Application.ScreenUpdating = False
            oCurDoc.ActiveWindow.SetFocus()

            '04-Jan-2016 Aniket: Warn the user if the full document is being replaced by tags
            If gloWord.LoadAndCloseWord.ValidateTagsSelectedRange(oCurDoc) = True Then
                oCurDoc.Application.Selection.InsertFile(sFilename, "", False, False, False)
                blnRefreshDocument = True
            End If

            wdNurseNotes.Select()
            If blnRefreshDocument = True Then
                GetdataFromOtherForms(enumDocType.None)
            End If

            '  oCurDoc.Application.ScreenUpdating = True
            If (IO.File.Exists(sFilename) = True) Then
                IO.File.Delete(sFilename)
            End If           
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''GLO2011-0015182 : Nurse Note 
    ''It is created into the function , its main purpose is to load the tool strip data after refresh also 
    ''Call is provided in LoadToolStrip in LoadWordUserControl.
    Private Sub isRecordLocked()
        If m_IsRecordLock Then
            If tlsNurseNotes.MyToolStrip.Items.ContainsKey("Save") = True Then
                tlsNurseNotes.MyToolStrip.Items("Save").Enabled = False
            End If
            If tlsNurseNotes.MyToolStrip.Items.ContainsKey("Save & Close") = True Then
                tlsNurseNotes.MyToolStrip.Items("Save & Close").Enabled = False
            End If
            If tlsNurseNotes.MyToolStrip.Items.ContainsKey("Save & Finish") = True Then
                tlsNurseNotes.MyToolStrip.Items("Save & Finish").Enabled = False
            End If

            If tlsNurseNotes.MyToolStrip.Items.ContainsKey("Add Addendum") = True Then
                tlsNurseNotes.MyToolStrip.Items("Add Addendum").Enabled = False
            End If
        End If
    End Sub
   
End Class
