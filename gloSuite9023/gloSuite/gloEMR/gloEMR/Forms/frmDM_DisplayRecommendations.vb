Imports gloUserControlLibrary
Imports gloEMR.gloStream.DiseaseManagement.Supporting
Imports gloEMR.gloStream.DiseaseManagement
Imports System.Drawing
Imports gloEMRGeneralLibrary

Public Class frmDM_DisplayRecommendations
    Inherits System.Windows.Forms.Form
    Implements IPatientContext

#Region " Private and Public variable declarations "

    Private m_PatientID As Long
    Private m_VisitID As Long

    Private m_CriteriaCol As New Collection
    Private m_PatientCol As New Collection

    Dim oclsDM As New clsDM_Template
    Private WithEvents _PatientStrip As New gloUC_PatientStrip
    Dim oclsDM_PatientSpecific As New clsDM_Template

    Dim oPatientCriterias As DataTable
    Dim oPatTriggers As DataTable
    Dim dtOtherHeathPlans As DataTable

    Dim oDM As New DiseaseManagement
    Dim arrLabs As New ArrayList
    Dim arrRadiology As New ArrayList
    Dim arrGuidline As New ArrayList
    Dim arrDrug As New ArrayList
    Dim arrCommonCriterias As New ArrayList

    Dim lst As myList
    Dim objSender As Object = Nothing

    Dim nPatAge As Int32 = 0

    Dim _TemplateID As Int64 = 0
    Dim _IsModifyCriteria As Boolean
    Dim _SelectedCriteriaID As Int64 = 0
    Dim _IsDeleteCriteria As Boolean
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    Private bParentTrigger As Boolean = True        ''for the trvHeathplan after 
    Private bChildTrigger As Boolean = True

    Private COL_Announced As Int16 = 0
    Private COL_Infobutton As Int16 = 1
    Private COL_Name As Int16 = 2
    Private COL_Description As Int16 = 3
    Private COL_Status As Int16 = 4
    Private COL_Criteria As Int16 = 5
    Private COL_RecommendationID As Int16 = 6
    Private COL_nStatus As Int16 = 7
    Private COL_sNote As Int16 = 8

    Private _c1 As C1.Win.C1FlexGrid.C1FlexGrid

#End Region ' Private and Public variable declarations '

#Region " Property Procedures "

    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID   'Curent patient variable(Local variable) for this module 
        End Get
    End Property

#End Region ' Property Procedures '

#Region " Constructor "

    Public Sub New(ByVal COL_PatientID As Collection, ByVal blnIsSinglePatient As Boolean, ByVal VisitID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_PatientCol = COL_PatientID
        m_PatientID = COL_PatientID(1)
        m_VisitID = VisitID

    End Sub

#End Region ' Constructor '

#Region " Form load, activated and closing event "

    Private Sub frmDM_PatientSpecific_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''  ts_btnHealthPlan.Visible = True


            loadPatientStrip()
            BindHealthPlan()

            If gblnShowHealthPlan = True And isOldDmAlertPresent = True Then
                ts_btnHealthPlan.Visible = True
                pnlRecomendationAlert.Visible = True
                lblRecomendationAlert.Visible = True
                lblRecomendationAlert.Text = "Patient has pending Orders to Be Given"
            Else
                pnlRecomendationAlert.Visible = False
                ts_btnHealthPlan.Visible = False
                lblRecomendationAlert.Visible = False
                lblRecomendationAlert.Text = ""
            End If

            'For Each f As Form In Application.OpenForms
            '    If f.Name = "frmDM_PatientSpecific" Then
            '        ts_btnHealthPlan.Visible = False
            '    End If
            'Next


            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmDM_DisplayRecommendations_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        'Memory Leaks
        If Not IsNothing(m_CriteriaCol) Then
            m_CriteriaCol.Clear()
            m_CriteriaCol = Nothing
        End If
        If Not IsNothing(m_PatientCol) Then
            m_PatientCol.Clear()
            m_PatientCol = Nothing
        End If
        If Not IsNothing(oclsDM) Then

            oclsDM = Nothing
        End If


        If Not IsNothing(_PatientStrip) Then
            _PatientStrip.Dispose()
            _PatientStrip = Nothing
        End If
        If Not IsNothing(oDM) Then
            oDM.Dispose()
            oDM = Nothing
        End If
        If Not IsNothing(arrLabs) Then
            arrLabs.Clear()
            arrLabs = Nothing
        End If
        If Not IsNothing(arrRadiology) Then
            arrRadiology.Clear()
            arrRadiology = Nothing
        End If
        If Not IsNothing(arrGuidline) Then
            arrGuidline.Clear()
            arrGuidline = Nothing
        End If
        If Not IsNothing(arrDrug) Then
            arrDrug.Clear()
            arrDrug = Nothing
        End If
        If Not IsNothing(arrCommonCriterias) Then
            arrCommonCriterias.Clear()
            arrCommonCriterias = Nothing
        End If
        If Not IsNothing(oclsDM_PatientSpecific) Then

            oclsDM_PatientSpecific = Nothing
        End If


        If Not IsNothing(oPatientCriterias) Then
            oPatientCriterias.Dispose()
            oPatientCriterias = Nothing
        End If

        If Not IsNothing(oPatTriggers) Then
            oPatTriggers.Dispose()
            oPatTriggers = Nothing
        End If

        If Not IsNothing(dtOtherHeathPlans) Then
            dtOtherHeathPlans.Dispose()
            dtOtherHeathPlans = Nothing
        End If

    End Sub

    Private Sub frmDM_PatientSpecific_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            'CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#End Region ' Form load, activated and closing event '

#Region " Form Toolstrip button click events "

    Private Sub ts_btnHealthPlan_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnHealthPlan.Click
        OpenHealthPlan()
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        BindHealthPlan()
        Me.Cursor = Cursors.Default
    End Sub

#End Region

#Region " Patient strip load "

    Private Sub loadPatientStrip()
        ''Show the patient details based on id passed
        _PatientStrip.ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.HealthPlan)
        _PatientStrip.Dock = DockStyle.Top
        ''Add the patient Strip control to the panel
        If pnlContainer.Controls.Contains(_PatientStrip) = False Then
            Me.pnlContainer.Controls.Add(_PatientStrip)
            'pnlToolStrip.SendToBack()
            _PatientStrip.Padding = New Padding(0, 0, 0, 0)
        End If
    End Sub

#End Region ' Patient strip load '

#Region " Context Menu Item click Event and Operation code "

    Private Sub cntMnu_ViewHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cntMnu_ViewHistory.Click

        If tabCtrl_Recommendations.SelectedTab.Tag = tbpg_CurrentRecommendation.Tag Then
            _c1 = c1CurrentRecommendation
        ElseIf tabCtrl_Recommendations.SelectedTab.Tag = tbpg_PastRecommendation.Tag Then
            _c1 = C1PastRecommendation
        End If

        If Not _c1.GetData(_c1.RowSel, COL_Criteria) Is Nothing Then

            Dim _Recommendationid As Int64 = 0
            _Recommendationid = Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID))
            Dim _CriteriaID As Int64 = 0
            _CriteriaID = Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria))
            Me.Cursor = Cursors.WaitCursor
            Dim oForm As New frmDM_RecommendationHistory(_Recommendationid, _CriteriaID, m_PatientID)
            oForm.WindowState = FormWindowState.Normal
            oForm.StartPosition = FormStartPosition.CenterScreen
            oForm.ShowInTaskbar = False
            oForm.ShowDialog(IIf(IsNothing(oForm.Parent), Me, oForm.Parent))
            Me.Cursor = Cursors.Arrow
            oForm.Dispose()
            oForm = Nothing
        End If
    End Sub

    Private Sub cntMnu_MarkSatisfied_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cntMnu_MarkSatisfied.Click
        UpdateRecommendation(sender, DiseaseManagement.RecommendationStatus.Satisfied)
    End Sub

    Private Sub cntMnu_InProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cntMnu_InProcess.Click

        UpdateRecommendation(sender, DiseaseManagement.RecommendationStatus.InProcess)

    End Sub

    Private Sub cntMnu_Cancel_NA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cntMnu_Cancel_NA.Click
        UpdateRecommendation(sender, DiseaseManagement.RecommendationStatus.CancelAsNotAppilcable)
    End Sub

    Private Sub cntMnu_Reopen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cntMnu_Reopen.Click
        UpdateRecommendation(sender, DiseaseManagement.RecommendationStatus.Reopened)
    End Sub

    Private Sub cntMnu_UpdateNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cntMnu_UpdateNote.Click

        UpdateRecommendation(sender, DiseaseManagement.RecommendationStatus.None)

    End Sub

    Private Sub cntMnu_ViewRefInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cntMnu_ViewRefInfo.Click

        Try
            If tabCtrl_Recommendations.SelectedTab.Tag = tbpg_CurrentRecommendation.Tag Then
                _c1 = c1CurrentRecommendation
            ElseIf tabCtrl_Recommendations.SelectedTab.Tag = tbpg_PastRecommendation.Tag Then
                _c1 = C1PastRecommendation
            End If

            If _c1.Rows.Count > 1 Then

                If Not IsNothing(_c1.GetData(_c1.RowSel, COL_Criteria)) AndAlso Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)) > 0 Then

                    Dim _frmDM_ViewRuleReference As New frmDM_ViewRuleReference(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)))
                    _frmDM_ViewRuleReference.StartPosition = FormStartPosition.CenterParent
                    _frmDM_ViewRuleReference.ShowDialog(IIf(IsNothing(_frmDM_ViewRuleReference.Parent), Me, _frmDM_ViewRuleReference.Parent))
                    _frmDM_ViewRuleReference.Dispose()


                End If

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

        End Try

    End Sub

    Private Sub cntMnu_Snooze_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cntMnu_Snooze.Click

    End Sub
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function DestroyIcon(ByVal hIcon As IntPtr) As Boolean
    End Function

    Private Function UpdateRecommendation(ByVal sender As Object, ByVal recommendationStatus As gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus) As Boolean

        Dim _isOperationCommited As Boolean = False
        Dim _Recommendationid As Int64 = 0
        Dim _CriteriaID As Int64 = 0
        Dim ofrmRecommendationNotes As frmDM_RecommendationNotes = Nothing
        Dim _sNote As String = ""
        Dim _nNoteUserid As Int64 = 0
        Dim _sNoteUserName As String = ""
        Dim _dtSatisfiedDateTime As DateTime = DateTime.MinValue

        Try

            If tabCtrl_Recommendations.SelectedTab.Tag = tbpg_CurrentRecommendation.Tag Then
                _c1 = c1CurrentRecommendation
            ElseIf tabCtrl_Recommendations.SelectedTab.Tag = tbpg_PastRecommendation.Tag Then
                _c1 = C1PastRecommendation
            End If

            If Not _c1.GetData(_c1.RowSel, COL_Criteria) Is Nothing Then

                _Recommendationid = Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID))


                If _Recommendationid > 0 Then

                    _CriteriaID = Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria))

                    If _CriteriaID > 0 Then

                        'Special case : If updating status of recommendation from past tab to Inprocess or Reopened then do not allow 
                        'if already same recommendation present in current with new, ip or reopened status
                        If Not IsNothing(oDM) AndAlso oDM.IsRecommendationInCurrentTabWithIP_RO_New_Status(_CriteriaID, m_PatientID, _Recommendationid) AndAlso (recommendationStatus = DiseaseManagement.RecommendationStatus.InProcess OrElse recommendationStatus = DiseaseManagement.RecommendationStatus.nNew OrElse recommendationStatus = DiseaseManagement.RecommendationStatus.Reopened) Then

                            MessageBox.Show("Cannot mark recommendation as " & recommendationStatus.ToString() & ". " &
                                            " " & Environment.NewLine & "The recommendation your trying to mark " & recommendationStatus.ToString() & " is already present in current recommendation tab with either New, InProcess OR Reopened status. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            Return _isOperationCommited

                        End If

                        ofrmRecommendationNotes = New frmDM_RecommendationNotes(_Recommendationid, _CriteriaID, m_PatientID)
                        ofrmRecommendationNotes.WindowState = FormWindowState.Normal
                        ofrmRecommendationNotes.StartPosition = FormStartPosition.CenterScreen
                        ofrmRecommendationNotes.ShowInTaskbar = False
                        ofrmRecommendationNotes.Text = CType(sender, ToolStripMenuItem).Text

                        Try
                            'Create a Bitmap object from an image file.
                            Dim bmp As New Bitmap(CType(sender, ToolStripMenuItem).Image)
                            'Get an Hicon for myBitmap.
                            Dim Hicon As IntPtr = bmp.GetHicon()
                            'Create a new icon from the handle.
                            Dim newIcon As Icon = Icon.FromHandle(Hicon)
                            ofrmRecommendationNotes.Icon = newIcon.Clone()
                            bmp.Dispose()
                            DestroyIcon(Hicon)
                            Hicon = Nothing
                            newIcon.Dispose()
                        Catch ex As Exception
                            'Blank
                        End Try


                        If Convert.ToString(CType(sender, ToolStripMenuItem).Tag) = "MARK_SATISFIED" And _c1.Name = "c1CurrentRecommendation" Then
                            ofrmRecommendationNotes.lblSatisFiedDate.Visible = True
                            ofrmRecommendationNotes.dtpSatisFiedDate.Visible = True
                        Else
                            ofrmRecommendationNotes.lblSatisFiedDate.Visible = False
                            ofrmRecommendationNotes.dtpSatisFiedDate.Visible = False
                        End If
                        ofrmRecommendationNotes.ShowDialog(IIf(IsNothing(ofrmRecommendationNotes.Parent), Me, ofrmRecommendationNotes.Parent))

                        If ofrmRecommendationNotes.FormDialogResult = Windows.Forms.DialogResult.OK Then

                            _sNote = ofrmRecommendationNotes.Note
                            _nNoteUserid = ofrmRecommendationNotes.NoteUserID
                            _sNoteUserName = ofrmRecommendationNotes.NoteUserName
                            _dtSatisfiedDateTime = ofrmRecommendationNotes.SatisfiedDate

                            '_c1.SetData(_c1.RowSel, COL_sNote, gloEMR.mdlGeneral.gstrLoginName + "  " + System.DateTime.Now.Date.ToString("MM/dd/yyyy") + "    " + ofrmRecommendationNotes.Note)

                            Select Case recommendationStatus

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.Reopened

                                    oDM.UpdateRecommendation(_Recommendationid, _CriteriaID, m_PatientID, _sNote, _nNoteUserid, _sNoteUserName, DateTime.MinValue.Date, DiseaseManagement.RecommendationStatus.Reopened, "Reopened", _c1.GetData(_c1.RowSel, COL_Name).ToString())

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.Satisfied

                                    oDM.UpdateRecommendation(_Recommendationid, _CriteriaID, m_PatientID, _sNote, _nNoteUserid, _sNoteUserName, _dtSatisfiedDateTime, DiseaseManagement.RecommendationStatus.Satisfied, "Satisfied", _c1.GetData(_c1.RowSel, COL_Name).ToString())

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.InProcess

                                    oDM.UpdateRecommendation(_Recommendationid, _CriteriaID, m_PatientID, _sNote, _nNoteUserid, _sNoteUserName, DateTime.MinValue.Date, DiseaseManagement.RecommendationStatus.InProcess, "InProcess", _c1.GetData(_c1.RowSel, COL_Name).ToString())

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.CancelAsNotAppilcable

                                    oDM.UpdateRecommendation(_Recommendationid, _CriteriaID, m_PatientID, _sNote, _nNoteUserid, _sNoteUserName, DateTime.MinValue.Date, DiseaseManagement.RecommendationStatus.CancelAsNotAppilcable, "Cancel\Not Applicable", _c1.GetData(_c1.RowSel, COL_Name).ToString())

                            End Select

                            If sender.ToString().Trim() = "Update Note" Then
                                oDM.RecommendationNoteUpdate(_Recommendationid, _CriteriaID, m_PatientID, _sNote, _nNoteUserid, _sNoteUserName, Date.MinValue.Date)
                            End If

                            _isOperationCommited = True

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & _c1.GetData(_c1.RowSel, COL_Name).ToString() & "' Recommendation Note Updated", m_PatientID, _CriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            BindHealthPlan()

                        End If
                        ofrmRecommendationNotes.Dispose()
                        ofrmRecommendationNotes = Nothing
                    End If

                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(ofrmRecommendationNotes) Then
                ofrmRecommendationNotes.Dispose()
                ofrmRecommendationNotes = Nothing
            End If

        End Try

        Return _isOperationCommited

    End Function

#End Region ' Context Menu Item click Event and Operation code '

#Region " c1 Events "

    Private Sub ShowContextMenu(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PastRecommendation.MouseDown, c1CurrentRecommendation.MouseDown

        Dim _dtQuickOrders As DataTable = Nothing
        Dim _colIndex As Integer = 0
        Dim _rowIndex As Integer = 0
        Dim _recommendationId As Int64 = 0
        Dim _quickOrderItem As ToolStripMenuItem = Nothing
        Dim _categoryType As Int16 = 0

        Try
            If e.Button = Windows.Forms.MouseButtons.Left Then
                If Not IsNothing(sender) Then
                    _colIndex = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).HitTest(e.X, e.Y).Column
                    _rowIndex = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).HitTest(e.X, e.Y).Row

                    If _colIndex = COL_Infobutton Then
                        Try
                            If tabCtrl_Recommendations.SelectedTab.Tag = tbpg_CurrentRecommendation.Tag Then
                                _c1 = c1CurrentRecommendation
                            ElseIf tabCtrl_Recommendations.SelectedTab.Tag = tbpg_PastRecommendation.Tag Then
                                _c1 = C1PastRecommendation
                            End If
                            If _c1.Rows.Count > 1 Then
                                Dim RuleId As Long = Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria))
                                Dim oInfobutton As New frmDM_Infobutton(RuleId, m_PatientID, _PatientStrip.PatientAge, _PatientStrip.PatientGender, "", m_VisitID)
                                oInfobutton.ShowDialog()
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
                'Try
                '    If (IsNothing(c1CurrentRecommendation.ContextMenuStrip) = False) Then
                '        c1CurrentRecommendation.ContextMenuStrip.Dispose()
                '        c1CurrentRecommendation.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                'Try
                '    If (IsNothing(C1PastRecommendation.ContextMenuStrip) = False) Then
                '        C1PastRecommendation.ContextMenuStrip.Dispose()
                '        C1PastRecommendation.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                c1CurrentRecommendation.ContextMenuStrip = Nothing
                C1PastRecommendation.ContextMenuStrip = Nothing
                cntMnu_QuickOrders.DropDownItems.Clear()


                If Not IsNothing(sender) Then
                    _rowIndex = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).HitTest(e.X, e.Y).Row

                    If _rowIndex > 0 Then

                        If CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).Name = c1CurrentRecommendation.Name Then

                            c1CurrentRecommendation.Select(_rowIndex, COL_nStatus, True)

                            cntMnu_Snooze.Visible = False
                            cntMnu_Reopen.Visible = False
                            cntMnu_MarkSatisfied.Visible = True
                            cntMnu_Cancel_NA.Visible = True
                            cntMnu_QuickOrders.Visible = True

                            If Not IsNothing(CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).GetData(_rowIndex, COL_RecommendationID)) AndAlso Convert.ToInt64(CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).GetData(_rowIndex, COL_RecommendationID)) > 0 Then

                                _recommendationId = Convert.ToInt64(CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).GetData(_rowIndex, COL_RecommendationID))
                                _dtQuickOrders = oDM.GetRecommendationOrders(_recommendationId, m_PatientID)

                                If Not IsNothing(_dtQuickOrders) AndAlso _dtQuickOrders.Rows.Count > 0 Then

                                    cntMnu_QuickOrders.Enabled = True

                                    For rIndex As Integer = 0 To _dtQuickOrders.Rows.Count - 1

                                        _categoryType = 0
                                        _quickOrderItem = New ToolStripMenuItem()
                                        _quickOrderItem.Text = Convert.ToString(_dtQuickOrders.Rows(rIndex)("OrderName"))
                                        _quickOrderItem.Tag = Convert.ToString(_dtQuickOrders.Rows(rIndex)("trigerId"))
                                        _categoryType = Convert.ToInt16(_dtQuickOrders.Rows(rIndex)("dm_Templatedtl_CategoryID"))
                                        _quickOrderItem.ForeColor = Color.FromArgb(31, 73, 125)
                                        _quickOrderItem.BackColor = Color.PapayaWhip


                                        Select Case _categoryType

                                            Case gloStream.DiseaseManagement.DiseaseManagement.TemplateCategoryID.Guidelines
                                                _quickOrderItem.Image = Global.gloEMR.My.Resources.Guidelines
                                                _quickOrderItem.ToolTipText = "Guidelines"
                                            Case gloStream.DiseaseManagement.DiseaseManagement.TemplateCategoryID.Labs
                                                _quickOrderItem.Image = Global.gloEMR.My.Resources.Labs
                                                _quickOrderItem.ToolTipText = "Labs"
                                            Case gloStream.DiseaseManagement.DiseaseManagement.TemplateCategoryID.Radiology
                                                _quickOrderItem.Image = Global.gloEMR.My.Resources.orders
                                                _quickOrderItem.ToolTipText = "Orders"
                                            Case gloStream.DiseaseManagement.DiseaseManagement.TemplateCategoryID.Referrals
                                                _quickOrderItem.Image = Global.gloEMR.My.Resources.Referrals
                                                _quickOrderItem.ToolTipText = "Referrals"
                                            Case gloStream.DiseaseManagement.DiseaseManagement.TemplateCategoryID.Rx
                                                _quickOrderItem.Image = Global.gloEMR.My.Resources.Rx
                                                _quickOrderItem.ToolTipText = "Rx"
                                            Case gloStream.DiseaseManagement.DiseaseManagement.TemplateCategoryID.IM
                                                _quickOrderItem.Image = Global.gloEMR.My.Resources.IM
                                                _quickOrderItem.ToolTipText = "Immunization"
                                            Case Else
                                                _quickOrderItem.Image = Global.gloEMR.My.Resources.Bullet_PNG

                                        End Select



                                        _quickOrderItem.BackgroundImageLayout = ImageLayout.None
                                        AddHandler _quickOrderItem.Click, AddressOf OpenOrder
                                        cntMnu_QuickOrders.DropDownItems.Add(_quickOrderItem)
                                        _quickOrderItem = Nothing

                                    Next
                                Else
                                    cntMnu_QuickOrders.Enabled = False
                                End If

                            End If

                        ElseIf CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).Name = C1PastRecommendation.Name Then

                            C1PastRecommendation.Select(_rowIndex, COL_nStatus, True)
                            cntMnu_Reopen.Visible = True
                            cntMnu_MarkSatisfied.Visible = False
                            cntMnu_Cancel_NA.Visible = False
                            cntMnu_QuickOrders.Visible = False
                            cntMnu_Snooze.Visible = False

                        End If
                        Dim myFlexGrid As C1.Win.C1FlexGrid.C1FlexGrid = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid)
                        'Try
                        '    If (IsNothing(myFlexGrid.ContextMenuStrip) = False) Then
                        '        myFlexGrid.ContextMenuStrip.Dispose()
                        '        myFlexGrid.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        myFlexGrid.ContextMenuStrip = cntMnu_RecommendationOptions

                    End If '..If _rowIndex > 0 Then





                End If '..If Not IsNothing(sender) Then

            End If '...If e.Button = Windows.Forms.MouseButtons.Right Then

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(_dtQuickOrders) Then
                _dtQuickOrders.Dispose()
                _dtQuickOrders = Nothing
            End If

            If Not IsNothing(_quickOrderItem) Then
                _quickOrderItem.Dispose()
                _quickOrderItem = Nothing
            End If

        End Try

    End Sub

    Private Sub c1CurrentRecommendation_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1CurrentRecommendation.MouseMove
        Try

            If c1CurrentRecommendation.HitTest(e.X, e.Y).Column = COL_sNote Or c1CurrentRecommendation.HitTest(e.X, e.Y).Column = COL_Description Then
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub

    Private Sub C1PastRecommendation_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1PastRecommendation.MouseMove
        Try

            If C1PastRecommendation.HitTest(e.X, e.Y).Column = COL_sNote Or C1PastRecommendation.HitTest(e.X, e.Y).Column = COL_Description Then
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub

#End Region ' c1 Events '


    Private Sub BindHealthPlan()

        Dim _dtRecommendation As DataTable = Nothing

        Try

            '''''''''''''''''''''''''''''Bind Current Recommendation-----------------------------------------------------
            _dtRecommendation = oDM.GetRecommendations(m_PatientID, DiseaseManagement.RecommendationFlag.Current)
            oDM.GetRecommendationsAlerts(m_PatientID) ''''''''''Get Latest recommendation alert
            c1CurrentRecommendation.Rows.Count = 1
            If c1CurrentRecommendation.Rows.Count > 0 Then
                c1CurrentRecommendation.BeginUpdate()
                For _row As Int16 = 0 To _dtRecommendation.Rows.Count - 1
                    c1CurrentRecommendation.Rows.Add()
                    If _dtRecommendation.Rows(_row)("dtAnnoncedDate").ToString() = Today.Date.ToString("MM/dd/yyyy") Then
                        c1CurrentRecommendation.SetData(c1CurrentRecommendation.Rows.Count - 1, COL_Announced, "Today")
                    Else
                        c1CurrentRecommendation.SetData(c1CurrentRecommendation.Rows.Count - 1, COL_Announced, _dtRecommendation.Rows(_row)("dtAnnoncedDate"))
                    End If

                    If gblnEducationMaterialEnabled Then
                        c1CurrentRecommendation.SetCellImage(c1CurrentRecommendation.Rows.Count - 1, COL_Infobutton, My.Resources.infobutton)
                    End If

                    c1CurrentRecommendation.SetData(c1CurrentRecommendation.Rows.Count - 1, COL_Name, _dtRecommendation.Rows(_row)("sRecommendationName").ToString())
                    c1CurrentRecommendation.SetData(c1CurrentRecommendation.Rows.Count - 1, COL_Description, _dtRecommendation.Rows(_row)("sRecommendationDescription").ToString())
                    c1CurrentRecommendation.SetData(c1CurrentRecommendation.Rows.Count - 1, COL_Status, _dtRecommendation.Rows(_row)("sRecommendationStatus").ToString())
                    c1CurrentRecommendation.SetData(c1CurrentRecommendation.Rows.Count - 1, COL_Criteria, _dtRecommendation.Rows(_row)("CriteriaID").ToString())
                    c1CurrentRecommendation.SetData(c1CurrentRecommendation.Rows.Count - 1, COL_RecommendationID, _dtRecommendation.Rows(_row)("RecommendationId").ToString())
                    c1CurrentRecommendation.SetData(c1CurrentRecommendation.Rows.Count - 1, COL_nStatus, _dtRecommendation.Rows(_row)("nRecommnedationStatus").ToString())
                    c1CurrentRecommendation.SetData(c1CurrentRecommendation.Rows.Count - 1, COL_sNote, _dtRecommendation.Rows(_row)("Note").ToString())


                Next

                If gblnEducationMaterialEnabled Then
                    c1CurrentRecommendation.Cols(COL_Infobutton).Visible = True
                Else
                    c1CurrentRecommendation.Cols(COL_Infobutton).Visible = False
                End If
                c1CurrentRecommendation.EndUpdate()
                ''DesignGrid()
            End If
            '''''''''''''''''''''''''''''END Bind Current Recommendation-----------------------------------------------------

            '''''''''''''''''''''''''''''Bind Current Recommendation-----------------------------------------------------
            _dtRecommendation.Dispose()

            _dtRecommendation = oDM.GetRecommendations(m_PatientID, DiseaseManagement.RecommendationFlag.Past)
            C1PastRecommendation.Rows.Count = 1

            If C1PastRecommendation.Rows.Count > 0 Then
                C1PastRecommendation.BeginUpdate()
                For _row As Int16 = 0 To _dtRecommendation.Rows.Count - 1
                    C1PastRecommendation.Rows.Add()
                    If _dtRecommendation.Rows(_row)("dtAnnoncedDate").ToString() = Today.Date.ToString("MM/dd/yyyy") Then
                        C1PastRecommendation.SetData(C1PastRecommendation.Rows.Count - 1, COL_Announced, "Today")
                    Else
                        C1PastRecommendation.SetData(C1PastRecommendation.Rows.Count - 1, COL_Announced, _dtRecommendation.Rows(_row)("dtAnnoncedDate"))
                    End If
                    C1PastRecommendation.SetData(C1PastRecommendation.Rows.Count - 1, COL_Name, _dtRecommendation.Rows(_row)("sRecommendationName").ToString())
                    C1PastRecommendation.SetData(C1PastRecommendation.Rows.Count - 1, COL_Description, _dtRecommendation.Rows(_row)("sRecommendationDescription").ToString())
                    C1PastRecommendation.SetData(C1PastRecommendation.Rows.Count - 1, COL_Status, _dtRecommendation.Rows(_row)("sRecommendationStatus").ToString())
                    C1PastRecommendation.SetData(C1PastRecommendation.Rows.Count - 1, COL_Criteria, _dtRecommendation.Rows(_row)("CriteriaID").ToString())
                    C1PastRecommendation.SetData(C1PastRecommendation.Rows.Count - 1, COL_RecommendationID, _dtRecommendation.Rows(_row)("RecommendationId").ToString())
                    C1PastRecommendation.SetData(C1PastRecommendation.Rows.Count - 1, COL_nStatus, _dtRecommendation.Rows(_row)("nRecommnedationStatus").ToString())
                    C1PastRecommendation.SetData(C1PastRecommendation.Rows.Count - 1, COL_sNote, _dtRecommendation.Rows(_row)("Note").ToString())
                Next
                C1PastRecommendation.EndUpdate()
                DesignGrid()



            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(_dtRecommendation) Then
                _dtRecommendation.Dispose()
                _dtRecommendation = Nothing
            End If

            'If oDM IsNot Nothing Then
            '    oDM.Dispose()
            '    oDM = Nothing
            'End If
        End Try
        'For _index As Integer = 1 To m_CriteriaCol.Count
        '    AddHealthPlanNode(CType(m_CriteriaCol(_index), Int64), False)
        'Next

    End Sub

    Private Sub DesignGrid()
        '' c1CurrentRecommendation.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        c1CurrentRecommendation.Cols(COL_Announced).Format = "MM/dd/yyyy"
        C1PastRecommendation.Cols(COL_Announced).Format = "MM/dd/yyyy"
        c1CurrentRecommendation.Cols(COL_RecommendationID).Visible = False
        c1CurrentRecommendation.Cols(COL_RecommendationID).Width = 0
        c1CurrentRecommendation.Cols(COL_Criteria).Visible = False
        c1CurrentRecommendation.Cols(COL_Criteria).Width = 0
        c1CurrentRecommendation.Cols(COL_nStatus).Width = 0
        C1PastRecommendation.Cols(COL_RecommendationID).Visible = False
        C1PastRecommendation.Cols(COL_RecommendationID).Width = 0
        C1PastRecommendation.Cols(COL_Criteria).Visible = False
        C1PastRecommendation.Cols(COL_Criteria).Width = 0

        C1PastRecommendation.Cols(COL_nStatus).Width = 0


        With c1CurrentRecommendation
            For _rowIndex As Int16 = 1 To c1CurrentRecommendation.Rows.Count - 1
                .Rows(_rowIndex).Height = 50
            Next
        End With

        'With c1CurrentRecommendation
        '    For _ColIndex As Int16 = 0 To c1CurrentRecommendation.Cols.Count - 1
        '        .Cols(_ColIndex).Style.WordWrap = True
        '    Next
        'End With

        With c1CurrentRecommendation

            If c1CurrentRecommendation.Rows.Count > 1 Then

                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                ' cStyle = .Styles.Add("WordWrap")
                Try
                    If (.Styles.Contains("WordWrap")) Then
                        cStyle = .Styles("WordWrap")
                    Else
                        cStyle = .Styles.Add("WordWrap")

                    End If
                Catch ex As Exception
                    cStyle = .Styles.Add("WordWrap")

                End Try
                cStyle.WordWrap = True
                cStyle.Trimming = StringTrimming.EllipsisCharacter

                Dim rgOperator As C1.Win.C1FlexGrid.CellRange = Nothing
                rgOperator = .GetCellRange(1, .Cols(COL_Status).Index, .Rows.Count - 1, .Cols(COL_Status).Index)
                rgOperator.Style = cStyle

                rgOperator = .GetCellRange(1, .Cols(COL_sNote).Index, .Rows.Count - 1, .Cols(COL_sNote).Index)
                rgOperator.Style = cStyle

                rgOperator = .GetCellRange(1, .Cols(COL_Name).Index, .Rows.Count - 1, .Cols(COL_Name).Index)
                rgOperator.Style = cStyle

                rgOperator = .GetCellRange(1, .Cols(COL_Description).Index, .Rows.Count - 1, .Cols(COL_Description).Index)
                rgOperator.Style = cStyle

            End If

        End With

        With C1PastRecommendation
            For _rowIndex As Int16 = 1 To C1PastRecommendation.Rows.Count - 1
                .Rows(_rowIndex).Height = 50
            Next
        End With

        With C1PastRecommendation

            If C1PastRecommendation.Rows.Count > 1 Then

                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                '  cStyle = .Styles.Add("WordWrap")
                Try
                    If (.Styles.Contains("WordWrap")) Then
                        cStyle = .Styles("WordWrap")
                    Else
                        cStyle = .Styles.Add("WordWrap")

                    End If
                Catch ex As Exception
                    cStyle = .Styles.Add("WordWrap")

                End Try
                cStyle.WordWrap = True
                cStyle.Trimming = StringTrimming.EllipsisCharacter

                Dim rgOperator As C1.Win.C1FlexGrid.CellRange = Nothing
                rgOperator = .GetCellRange(1, .Cols(COL_Status).Index, .Rows.Count - 1, .Cols(COL_Status).Index)
                rgOperator.Style = cStyle

                rgOperator = .GetCellRange(1, .Cols(COL_sNote).Index, .Rows.Count - 1, .Cols(COL_sNote).Index)
                rgOperator.Style = cStyle

                rgOperator = .GetCellRange(1, .Cols(COL_Name).Index, .Rows.Count - 1, .Cols(COL_Name).Index)
                rgOperator.Style = cStyle

                rgOperator = .GetCellRange(1, .Cols(COL_Description).Index, .Rows.Count - 1, .Cols(COL_Description).Index)
                rgOperator.Style = cStyle

            End If


        End With



    End Sub

    Private Sub OpenOrder(ByVal sender As Object, ByVal e As EventArgs)
        Dim str() As String = sender.tag.ToString().Split(",")

        ValidateHealthPlan(Convert.ToInt64(str(0)), If(str.Length > 1, Convert.ToInt64(str(1)), 0), sender.Text())
        BindHealthPlan()
        str = Nothing
    End Sub

    Private Sub ValidateHealthPlan(ByVal RuleId As Long, ByVal CatId As Long, ByVal SelectedOrder As String)


        Dim oCriteria As New Criteria
        arrLabs = New ArrayList
        arrRadiology = New ArrayList
        arrGuidline = New ArrayList
        arrDrug = New ArrayList
        Dim arrIM As ArrayList = New ArrayList
        Dim gloArrLabs As ArrayList = New ArrayList()
        Dim objList As myList
        Dim obj As New clsProvider


        Dim dmmstid As Long = 0
        Try

            If tabCtrl_Recommendations.SelectedTab.Tag = tbpg_CurrentRecommendation.Tag Then
                _c1 = c1CurrentRecommendation
            ElseIf tabCtrl_Recommendations.SelectedTab.Tag = tbpg_PastRecommendation.Tag Then
                _c1 = C1PastRecommendation
            End If

            dmmstid = Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria))

            oCriteria = oDM.GetCriteria(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID)

            If CatId = DiseaseManagement.TemplateCategoryID.Rx Then
                ''
                If oCriteria.RxDrugs.Count > 0 Then
                    For i As Integer = 1 To oCriteria.RxDrugs.Count
                        'objList = New myList
                        objList = CType(oCriteria.RxDrugs.Item(i), myList)
                        If RuleId = objList.Index Then
                            arrDrug.Add(objList)
                        End If
                        objList = Nothing
                    Next
                End If

                If arrDrug.Count > 0 Then
                    Dim DrugList As gloEMRActors.Drug = Nothing
                    Dim localArrayList As New ArrayList
                    For Each localItem As myList In arrDrug
                        DrugList = New gloEMRActors.Drug
                        DrugList.mpid = localItem.mpid
                        DrugList.NDCCode = localItem.NDCCode
                        localArrayList.Add(DrugList)
                    Next
                    If IsNothing(DrugList) Then
                        DrugList.Dispose()
                        DrugList = Nothing
                    End If

                    Dim frm As frmPrescription
                    frm = frmPrescription.GetInstance(localArrayList, CType(obj.GetPatientProvider(m_PatientID), Long), 0, m_PatientID)
                    If IsNothing(frm) = True Then
                        If Not IsNothing(obj) Then
                            obj.Dispose()
                            obj = Nothing
                        End If
                        Exit Sub
                    End If
                    If frm.blncancel = True Then
                        If frmPrescription.IsOpen = False Then
                            'Incident #00013567 : Medication carry forward case
                            'following changes done to resolve incident
                            'If frm.LockForm(m_PatientID) = False Then
                            '    frm.Dispose()
                            '    frm = Nothing
                            'Else                            
                            With frm
                                '.ShowReconcileMessage()
                                .IsfrmDM = True
                                .StartPosition = FormStartPosition.CenterParent
                                .ShowInTaskbar = False
                                Try
                                    .Size = New System.Drawing.Size(.Size.Width + 76, .Size.Height)
                                    .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                Catch ex As InvalidOperationException
                                    MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Finally
                                    If Not IsNothing(frm) = True Then
                                        frm.Dispose()
                                        frm = Nothing
                                    End If
                                End Try
                            End With
                            'End If
                        Else
                            MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
                ''
            ElseIf CatId = DiseaseManagement.TemplateCategoryID.Radiology Then
                ''
                If oCriteria.RadiologyOrders.Count > 0 Then
                    For i As Integer = 1 To oCriteria.RadiologyOrders.Count
                        'objList = New myList
                        objList = CType(oCriteria.RadiologyOrders.Item(i), myList)
                        If RuleId = objList.Index Then
                            arrRadiology.Add(objList)
                        End If
                        objList = Nothing
                    Next
                End If

                If arrRadiology.Count > 0 Then
                    Dim frm As frm_LM_Orders
                    frm = frm_LM_Orders.GetInstance(m_VisitID, Now, m_PatientID)
                    If IsNothing(frm) = True Then
                        If Not IsNothing(obj) Then
                            obj.Dispose()
                            obj = Nothing
                        End If
                        Exit Sub
                    End If
                    With frm
                        ._ArrRadi = arrRadiology
                        .StartPosition = FormStartPosition.CenterScreen
                        ._VisitID = m_VisitID
                        ._VisitDate = Now
                        .ShowInTaskbar = False
                        .BringToFront()
                        Try
                            .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                        Catch
                            MessageBox.Show("Orders screen cannot be opened.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Finally
                            If Not IsNothing(frm) = True Then
                                frm.Dispose()
                                frm = Nothing
                            End If
                        End Try
                    End With
                End If

            ElseIf CatId = DiseaseManagement.TemplateCategoryID.Labs Then
                Dim Emdeonlst As gloEmdeonCommon.myList

                Try
                    If oCriteria.LabOrders.Count > 0 Then
                        For i As Integer = 1 To oCriteria.LabOrders.Count
                            'objList = New myList
                            'Emdeonlst = New gloEmdeonCommon.myList
                            objList = CType(oCriteria.LabOrders.Item(i), myList)
                            If RuleId = objList.Index Then
                                Emdeonlst = New gloEmdeonCommon.myList
                                Emdeonlst.Value = objList.Value
                                Emdeonlst.DMTemplateName = objList.DMTemplateName
                                Emdeonlst.ID = objList.ID
                                Emdeonlst.IsFinished = False
                                Emdeonlst.Index = objList.Index
                                arrLabs.Add(objList)
                                gloArrLabs.Add(Emdeonlst)
                                Emdeonlst = Nothing
                            End If
                            objList = Nothing

                        Next
                    End If

                    If arrLabs.Count > 0 Then
                        Dim _TestList As String = String.Empty
                        Dim _MyTestList As gloEmdeonCommon.myList = Nothing
                        ''End of by Abhijeet on 20100625
                        _TestList += "Lab Tests:" & vbNewLine
                        For index As Integer = 0 To gloArrLabs.Count - 1
                            _MyTestList = gloArrLabs(index)
                            If index = 0 Then
                                _TestList += _MyTestList.Value
                            Else
                                _TestList += ", " + _MyTestList.Value
                            End If
                        Next

                        If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage <> "" Then
                            Select Case gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage
                                Case "TASK"
                                    gloLabSettings("TASK", _TestList, arrLabs)
                                Case "LABORDER"
                                    gloLabSettings("LABORDER")
                                Case "RECORDRESULTS"
                                    gloLabSettings("RECORDRESULTS", "", gloArrLabs)
                                Case "ASK"
                                    Dim frmAskform As New gloEmdeonInterface.Forms.frmCnfrmLabFlow()
                                    frmAskform.ShowInTaskbar = False
                                    frmAskform.BringToFront()
                                    frmAskform.ShowDialog(IIf(IsNothing(frmAskform.Parent), Me, frmAskform.Parent))
                                    gloLabSettings(frmAskform.LabFlowConfirm, _TestList, gloArrLabs)
                                    frmAskform.Dispose()
                                    frmAskform = Nothing
                                Case Else
                                    MessageBox.Show("Please configure Default user for Task in EMR Admin - Lab Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Select
                            End Select
                        End If
                        _MyTestList = Nothing
                        _TestList = Nothing
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                End Try

            ElseIf CatId = DiseaseManagement.TemplateCategoryID.IM Then
                If oCriteria.IMlst.Count > 0 Then
                    For i As Integer = 1 To oCriteria.IMlst.Count
                        'objList = New myList
                        objList = CType(oCriteria.IMlst.Item(i), myList)
                        If RuleId = objList.Index Then
                            arrIM.Add(objList)
                        End If
                        objList = Nothing
                    Next
                End If

                If arrIM.Count > 0 Then
                    'Bug No 49189::20130415::CDS- Recommendation-Application is opening immunization form multiple times
                    'For i As Integer = 0 To arrIM.Count - 1
                    Dim lst As myList
                    lst = CType(arrIM(0), myList)
                    Dim frm As New frmImTransaction(0, m_PatientID, lst)
                    frm.Text = "Add Immunization"
                    frm.ShowInTaskbar = False
                    frm.StartPosition = FormStartPosition.CenterParent
                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

                    frm.Dispose()
                    frm = Nothing
                    lst = Nothing
                    'Next

                End If
            ElseIf CatId = DiseaseManagement.TemplateCategoryID.Guidelines Then
                If oCriteria.Guidelines.Count > 0 Then
                    For i As Integer = 1 To oCriteria.Guidelines.Count
                        'objList = New myList
                        objList = CType(oCriteria.Guidelines.Item(i), myList)
                        If RuleId = objList.Index And SelectedOrder = objList.DMTemplateName Then
                            arrGuidline.Add(objList)
                        End If
                        objList = Nothing
                    Next
                End If

                If arrGuidline.Count > 0 Then
                    Dim frm As New frmDM_Template(arrGuidline, m_PatientID)
                    With frm
                        .StartPosition = FormStartPosition.CenterScreen
                        .ShowInTaskbar = False
                        .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                    End With
                    frm.Dispose()
                    frm = Nothing
                End If
            End If

            For i As Integer = 0 To gloArrLabs.Count - 1
                Dim lst As New myList
                lst.Value = CType(gloArrLabs(i), gloEmdeonCommon.myList).Value
                lst.DMTemplateName = CType(gloArrLabs(i), gloEmdeonCommon.myList).DMTemplateName
                lst.ID = CType(gloArrLabs(i), gloEmdeonCommon.myList).ID
                lst.IsFinished = CType(gloArrLabs(i), gloEmdeonCommon.myList).IsFinished
                lst.Index = CType(gloArrLabs(i), gloEmdeonCommon.myList).Index
                SaveGivenTriggers(dmmstid, lst, DiseaseManagement.TemplateCategoryID.Labs, lst.IsFinished)
                If lst.IsFinished Then
                    _c1.SetData(_c1.RowSel, COL_Status, "InProcess")
                    'oDM.RecommendationStatusUpdate(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID)), Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID, Convert.ToInt16(_c1.GetData(_c1.RowSel, COL_nStatus)), Convert.ToString(_c1.GetData(_c1.RowSel, COL_Status)))
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.QuickOrdersLab, """Lab :" & lst.DMTemplateName & """Quick ordered performed", m_PatientID, dmmstid, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    oDM.UpdateRecommendation(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID)), Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID, "System : Auto status change to InProcess for quick orders performed  ""Lab :" & lst.DMTemplateName & """", gloEMR.mdlGeneral.gnLoginID, gloEMR.mdlGeneral.gstrLoginName, DateTime.MinValue.Date, DiseaseManagement.RecommendationStatus.InProcess, "InProcess", _c1.GetData(_c1.RowSel, COL_Name).ToString())
                End If
                lst = Nothing
            Next

            For i As Integer = 0 To arrRadiology.Count - 1
                Dim lst As myList
                lst = CType(arrRadiology(i), myList)
                SaveGivenTriggers(dmmstid, lst, DiseaseManagement.TemplateCategoryID.Radiology, lst.IsFinished)
                If lst.IsFinished Then
                    _c1.SetData(_c1.RowSel, COL_Status, "InProcess")
                    'oDM.RecommendationStatusUpdate(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID)), Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID, Convert.ToInt16(_c1.GetData(_c1.RowSel, COL_nStatus)), Convert.ToString(_c1.GetData(_c1.RowSel, COL_Status)))
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.QuickOrdersLab, """Orders :" & lst.DMTemplateName & """Quick ordered performed", m_PatientID, dmmstid, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    oDM.UpdateRecommendation(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID)), Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID, "System : Auto status change to InProcess for quick orders performed  ""Orders :" & lst.DMTemplateName & """", gloEMR.mdlGeneral.gnLoginID, gloEMR.mdlGeneral.gstrLoginName, DateTime.MinValue.Date, DiseaseManagement.RecommendationStatus.InProcess, "InProcess", _c1.GetData(_c1.RowSel, COL_Name).ToString())
                End If
                lst = Nothing
            Next

            For i As Integer = 0 To arrGuidline.Count - 1
                Dim lst As myList
                lst = CType(arrGuidline(i), myList)
                SaveGivenTriggers(dmmstid, lst, DiseaseManagement.TemplateCategoryID.Guidelines, lst.IsFinished)
                If lst.IsFinished Then
                    _c1.SetData(_c1.RowSel, COL_Status, "InProcess")
                    'oDM.RecommendationStatusUpdate(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID)), Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID, Convert.ToInt16(_c1.GetData(_c1.RowSel, COL_nStatus)), Convert.ToString(_c1.GetData(_c1.RowSel, COL_Status)))
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.QuickOrdersLab, """Guideline :" & lst.DMTemplateName & """Quick ordered performed", m_PatientID, dmmstid, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    oDM.UpdateRecommendation(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID)), Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID, "System : Auto status change to InProcess for quick orders performed  ""Guideline :" & lst.DMTemplateName & """", gloEMR.mdlGeneral.gnLoginID, gloEMR.mdlGeneral.gstrLoginName, DateTime.MinValue.Date, DiseaseManagement.RecommendationStatus.InProcess, "InProcess", _c1.GetData(_c1.RowSel, COL_Name).ToString())
                End If
                lst = Nothing
            Next

            For i As Integer = 0 To arrDrug.Count - 1
                Dim lst As myList
                lst = CType(arrDrug(i), myList)
                SaveGivenTriggers(dmmstid, lst, DiseaseManagement.TemplateCategoryID.Rx, lst.IsFinished)
                If lst.IsFinished Then
                    _c1.SetData(_c1.RowSel, COL_Status, "InProcess")
                    'oDM.RecommendationStatusUpdate(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID)), Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID, Convert.ToInt16(_c1.GetData(_c1.RowSel, COL_nStatus)), Convert.ToString(_c1.GetData(_c1.RowSel, COL_Status)))
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.QuickOrdersLab, """RX :" & lst.DMTemplateName & """Quick ordered performed", m_PatientID, dmmstid, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    oDM.UpdateRecommendation(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID)), Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID, "System : Auto status change to InProcess for quick orders performed  ""RX :" & lst.DMTemplateName & """", gloEMR.mdlGeneral.gnLoginID, gloEMR.mdlGeneral.gstrLoginName, DateTime.MinValue.Date, DiseaseManagement.RecommendationStatus.InProcess, "InProcess", _c1.GetData(_c1.RowSel, COL_Name).ToString())
                End If
                lst = Nothing
            Next

            For i As Integer = 0 To arrIM.Count - 1
                Dim lst As myList
                lst = CType(arrIM(i), myList)
                SaveGivenTriggers(dmmstid, lst, DiseaseManagement.TemplateCategoryID.IM, lst.IsFinished)
                If lst.IsFinished Then
                    _c1.SetData(_c1.RowSel, COL_Status, "InProcess")
                    'oDM.RecommendationStatusUpdate(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID)), Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID, Convert.ToInt16(_c1.GetData(_c1.RowSel, COL_nStatus)), Convert.ToString(_c1.GetData(_c1.RowSel, COL_Status)))
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.QuickOrdersLab, """IM :" & lst.DMTemplateName & """Quick ordered performed", m_PatientID, dmmstid, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    oDM.UpdateRecommendation(Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_RecommendationID)), Convert.ToInt64(_c1.GetData(_c1.RowSel, COL_Criteria)), m_PatientID, "System : Auto status change to InProcess for quick orders performed  ""IM :" & lst.DMTemplateName & """", gloEMR.mdlGeneral.gnLoginID, gloEMR.mdlGeneral.gstrLoginName, DateTime.MinValue.Date, DiseaseManagement.RecommendationStatus.InProcess, "InProcess", _c1.GetData(_c1.RowSel, COL_Name).ToString())
                End If
                lst = Nothing
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            oCriteria = Nothing
            arrLabs = Nothing
            arrRadiology = Nothing
            arrGuidline = Nothing
            arrDrug = Nothing
            arrIM = Nothing
            gloArrLabs = Nothing
            objList = Nothing
            If Not IsNothing(obj) Then
                obj.Dispose()
                obj = Nothing
            End If

        End Try


    End Sub

    Private Sub gloLabSettings(ByVal _TaskType As String, Optional ByVal _TestList As String = "", Optional ByVal _arrLabs As ArrayList = Nothing)

        Select Case _TaskType.ToString().ToUpper()
            Case "TASK"
                Dim objClsGeneral As New gloEmdeonInterface.Classes.clsGeneral()
                Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
                Dim objpatient As New gloPatient.Patient()
                Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)
                Dim _LoginProviderId As Int64 = 0
                Dim strlabs As String = ""
                Dim strlab As String = ""
                Dim ncnt As Integer = 1
                strlab = ""

                Try
                    objpatient = objgloPatient.GetPatient(m_PatientID)
                    _LoginProviderId = GetProviderIDForUser(gnLoginID)
                    If Not IsNothing(_arrLabs) Then
                        For olab As Integer = 0 To arrLabs.Count - 1
                            strlab = (arrLabs.Item(olab)).ID & "~" & (arrLabs.Item(olab)).Value
                            If olab = 0 Then
                                strlabs = strlab
                                ncnt = ncnt + 1
                            Else
                                strlabs = strlabs & "|" & strlab
                                ncnt = ncnt + 1
                            End If
                        Next
                    End If
                    objClsGeneral.TestlistOnly = strlabs
                    objClsGeneral.TestList = _TestList
                    Dim nTaskID As Long = objClsGeneral.AssignTaskToUser(m_PatientID, objpatient.DemographicsDetail.PatientProviderID, _LoginProviderId, 0, gloTaskMail.TaskType.PlaceLabOrder)
                    If nTaskID > 0 Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.LabOrderRequest, "Task assigned for placing lab order", m_PatientID, 0, _LoginProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                    _LoginProviderId = 0
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                Finally

                    If Not IsNothing(objClsGeneral) Then
                        objClsGeneral.Dispose()
                        objClsGeneral = Nothing
                    End If

                    If Not IsNothing(objClsgloLabPatientLayer) Then
                        objClsgloLabPatientLayer.Dispose()
                        objClsgloLabPatientLayer = Nothing
                    End If

                    If Not IsNothing(objpatient) Then
                        objpatient.Dispose()
                        objpatient = Nothing
                    End If

                    If Not IsNothing(objpatient) Then
                        objpatient.Dispose()
                        objpatient = Nothing
                    End If

                    _LoginProviderId = Nothing
                    strlabs = Nothing
                    strlab = Nothing

                End Try

            Case "LABORDER"

                gloLabOrderScreen()

            Case "RECORDRESULTS"
                Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(m_PatientID)

                AddHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                frmNormalLab.ArrLabs = _arrLabs '' Added by Abhijeet on 20100626
                frmNormalLab.WindowState = FormWindowState.Maximized
                frmNormalLab.ShowInTaskbar = False
                frmNormalLab.BringToFront()
                frmNormalLab.ShowDialog(IIf(IsNothing(frmNormalLab.Parent), Me, frmNormalLab.Parent))
                RemoveHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                frmNormalLab.Dispose()
                frmNormalLab = Nothing
            Case Else
                MessageBox.Show("Please configure Default user for Task in EMR Admin - Lab Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Select
        End Select
    End Sub

    Private Sub gloLabOrderScreen()

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim objclsgeneral As New gloEmdeonInterface.Classes.clsGeneral()
        Dim _LoginUserProviderID As Long = 0
        Dim _PatientProviderID As Long = 0
        Dim loopcnt As Int16 = 0
        Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
        Dim objpatient As New gloPatient.Patient()
        Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)
        Dim LabConncetionAvailable As gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity



        Try
            objpatient = objgloPatient.GetPatient(m_PatientID)
            _LoginUserProviderID = GetProviderIDForUser(gnLoginID)
            _PatientProviderID = objpatient.DemographicsDetail.PatientProviderID

            If Not gloEmdeonInterface.Classes.clsEmdeonGeneral.CheckConnectionParameters(GetConnectionString) Then
                MessageBox.Show("Lab Settings have not been configured in gloEMR Admin." + vbCrLf + "Please complete Lab Settings before ordering.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            LabConncetionAvailable = objclsgeneral.IsInternetConnectionAvailable()

            If LabConncetionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.Success Then

                If Not compareProvider(_PatientProviderID, _LoginUserProviderID) Then
                    Return
                End If

                Dim _billingStatus As Boolean = False
                'Dim objGloPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
                _billingStatus = objClsgloLabPatientLayer.CheckBillingType(objpatient)
                If _billingStatus = True Then
                    If gloEmdeonInterface.Classes.clsEmdeonGeneral.IsDemoLab = True Then
                        Dim frmLabDemo As New gloEmdeonInterface.Forms.frmLabDemonstration(m_PatientID)
                        frmLabDemo.WindowState = FormWindowState.Maximized
                        frmLabDemo.BringToFront()
                        frmLabDemo.ShowDialog(IIf(IsNothing(frmLabDemo.Parent), Me, frmLabDemo.Parent))
                        frmLabDemo.Dispose()
                        frmLabDemo = Nothing

                    Else

                        Dim strQry As String = String.Empty
                        Dim boolPatientReg As [Boolean] = False

                        If ConfirmNull(objpatient.DemographicsDetail.PatientCode.ToString()) Then
                            strQry = "SELECT COUNT(*) FROM PatientExternalCodes INNER JOIN Patient ON PatientExternalCodes.nPatientId = Patient.nPatientID  where PatientExternalCodes.sExternalType = 'EMDEON' AND    Patient.sPatientCode='" & objpatient.DemographicsDetail.PatientCode.ToString().Trim() & "'"
                        End If
                        oDB.Connect(False)
                        For loopcnt = 1 To 3
                            Dim cnt As Int32 = 0
                            cnt = Convert.ToInt32(oDB.ExecuteScalar_Query(strQry))
                            If cnt < 1 Then
                                Application.DoEvents()
                                gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_patID = m_PatientID
                                boolPatientReg = objClsgloLabPatientLayer.RegisterGloPatient(objpatient, GetConnectionString)
                                If boolPatientReg Then
                                    Exit For
                                End If
                            Else
                                boolPatientReg = True
                                Exit For
                            End If
                        Next

                        If boolPatientReg = True Then
                            Dim objfrmEmdonInterface As New gloEmdeonInterface.Forms.frmEmdeonInterface(m_PatientID)
                            objfrmEmdonInterface.LoginProviderID = gnLoginProviderID
                            objfrmEmdonInterface.WindowState = FormWindowState.Maximized
                            objfrmEmdonInterface.ShowDialog(IIf(IsNothing(objfrmEmdonInterface.Parent), Me, objfrmEmdonInterface.Parent))
                            objfrmEmdonInterface.Dispose()
                            objfrmEmdonInterface = Nothing
                        Else

                            If ConfirmNull(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Identifier.ToString()) Then
                                MessageBox.Show(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Identifier.ToString().Trim(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                MessageBox.Show("Patient is not registered With Emdeon,please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                    End If
                End If
            Else
                If LabConncetionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.NoInternet Then
                    Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(True)
                    objFrmConnectionConfirm.ShowInTaskbar = False
                    objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                    objFrmConnectionConfirm.Dispose()
                    objFrmConnectionConfirm = Nothing
                ElseIf LabConncetionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.ServerNotresponding Then
                    Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(False)
                    objFrmConnectionConfirm.ShowInTaskbar = False
                    objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                    objFrmConnectionConfirm.Dispose()
                    objFrmConnectionConfirm = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oDB) Then
                objclsgeneral.Dispose()
                objclsgeneral = Nothing
            End If
            If Not IsNothing(oDB) Then
                objClsgloLabPatientLayer.Dispose()
                objClsgloLabPatientLayer = Nothing
            End If
            If Not IsNothing(oDB) Then
                objpatient.Dispose()
                objpatient = Nothing
            End If
            If Not IsNothing(oDB) Then
                objgloPatient.Dispose()
                objgloPatient = Nothing
            End If
            LabConncetionAvailable = Nothing
        End Try

    End Sub

    Private Function compareProvider(ByVal _PatientProviderID As Int64, ByVal _LoginUserProviderID As Int64) As Boolean
        Dim objClsGeneral As New gloEmdeonInterface.Classes.clsGeneral()
        Dim strProviderName As String = String.Empty
        Dim strLoginUserName As String = String.Empty
        Dim strLabID As String = String.Empty

        Try
            If _PatientProviderID <> 0 Then
                strProviderName = objClsGeneral.GetProviderName(_PatientProviderID, gnClinicID)
            End If
            If _LoginUserProviderID <> 0 Then
                strLoginUserName = objClsGeneral.GetProviderName(_LoginUserProviderID, gnClinicID)
            End If
            If _LoginUserProviderID = 0 Then
                Dim drMesgResult As DialogResult = MessageBox.Show(("The user you are using is not set up as a provider. If you proceed, the lab order " & vbCr & vbLf & "provider will be defaulted to the current patient’s provider '") + strProviderName & "'." & vbCr & vbLf & vbCr & vbLf & "Would you like to proceed with creating a new order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If drMesgResult = DialogResult.Yes Then
                    strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
                    If ConfirmNull(strLabID.ToString()) Then
                        Return True
                    Else
                        If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                Else
                    Return False
                End If
            End If

            If _LoginUserProviderID <> _PatientProviderID Then
                Dim dgResult As DialogResult = MessageBox.Show((("This patient is currently assigned to the provider '" & strProviderName & "'.Would " & vbCr & vbLf & "you like to change the patient provider to '") + strLoginUserName & "' ? " & vbCr & vbLf & vbCr & vbLf & "If you select 'No', the lab order will be created for '") + strProviderName & "'.", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If dgResult = DialogResult.Yes Then
                    If objClsGeneral.changePatientProvider(_LoginUserProviderID, m_PatientID) Then
                        strLabID = objClsGeneral.GetProvidergloLabId(_LoginUserProviderID)
                        If ConfirmNull(strLabID.ToString()) Then
                            Return True
                        Else
                            If MessageBox.Show("The current provider '" & strLoginUserName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    Else
                        Return False
                    End If
                ElseIf dgResult = DialogResult.No Then
                    strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
                    If ConfirmNull(strLabID.ToString()) Then
                        Return True
                    Else
                        If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                ElseIf dgResult = DialogResult.Cancel Then
                    Return False
                End If
            End If

            If _LoginUserProviderID = _PatientProviderID Then
                strLabID = objClsGeneral.GetProvidergloLabId(_LoginUserProviderID)
                If ConfirmNull(strLabID.ToString()) Then
                    Return True
                Else
                    If MessageBox.Show("The current provider '" & strLoginUserName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return False
        Finally
            If Not IsNothing(objClsGeneral) Then
                objClsGeneral.Dispose()
                objClsGeneral = Nothing
            End If
            strProviderName = Nothing
            strLoginUserName = Nothing
            strLabID = Nothing
        End Try
    End Function

    Protected Function ConfirmNull(ByVal strValue As String) As Boolean
        Dim blnCheck As Boolean = False
        Try
            If strValue IsNot Nothing AndAlso strValue.ToString().Trim().Length <> 0 AndAlso strValue.ToString() <> "" Then
                blnCheck = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            'objclsGeneral.UpdateLog("Null reference value!" + ex.ToString());
        End Try
        Return blnCheck
    End Function

    Public Function GetProviderIDForUser(ByVal UserID As Int64) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim ProID As Int64 = 0
        Try
            oDB.Connect(False)

            ProID = Convert.ToInt64(oDB.ExecuteScalar_Query("SELECT nProviderID from user_mst where nUserID = " & UserID & ""))
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ProID = 0
        Finally
            oDB.Dispose()
        End Try
        Return ProID
    End Function

    Private Sub SaveGivenTriggers(ByVal _CriteriaID As Int64, ByVal objList As myList, ByVal nType As DiseaseManagement.TemplateCategoryID, ByVal bIsgiven As Boolean)
        Try
            If bIsgiven Then
                ''If not selected check whether Reason for not giving is documented or not
                If objList.TemplateResult Is Nothing Then
                    ''Delete the Exisiting record if present
                    oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, objList.ID, m_PatientID)
                    oclsDM_PatientSpecific.Save_Trigger_NewRule(0, m_PatientID, _CriteriaID, objList.ID, objList.DMTemplateName, objList.Value, objList.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, objList, , , , , , True, False, objList.Dosage)
                Else
                    ''Get the Trigger details as object and validate for reason field
                    Dim oTrigger As TriggerDetails = CType(objList.TemplateResult, TriggerDetails)
                    If oTrigger.Recurring Then
                        ''Delete the Exisiting record if present
                        Dim TrnsId As Int64
                        oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, objList.ID, m_PatientID)
                        TrnsId = oclsDM_PatientSpecific.Save_Trigger_NewRule(0, m_PatientID, _CriteriaID, objList.ID, objList.DMTemplateName, objList.Value, objList.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, objList, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, True, objList.Dosage)
                        ''Here u need to insert the recurring value details
                        If TrnsId <> 0 Then
                            oclsDM_PatientSpecific.Save_TemplateDetail(TrnsId, oTrigger.StartDate, oTrigger.EndDate, oTrigger.DurationType, oTrigger.DurationPeriod)
                        End If
                    Else
                        ''Delete the Exisiting record if present
                        oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, objList.ID, m_PatientID)
                        oclsDM_PatientSpecific.Save_Trigger_NewRule(0, m_PatientID, _CriteriaID, objList.ID, objList.DMTemplateName, objList.Value, objList.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, objList, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, True, False, objList.Dosage)
                        'End
                    End If
                End If
            Else
                'mynode.Checked = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OpenHealthPlan()
        Try

            If m_PatientID <= 0 Then
                Exit Sub
            End If

            If MainMenu.IsAccess(False, m_PatientID) = False Then
                Exit Sub
            End If
            'end modification 
            Try
                Dim _toSendPatLst As New Collection
                '_toSendPatLst.Add(gnPatientID)
                _toSendPatLst.Add(m_PatientID)
                Dim frmTemplate As New frmDM_PatientSpecific(_toSendPatLst, True, m_PatientID)
                With frmTemplate
                    .ShowInTaskbar = False
                    .WindowState = FormWindowState.Normal
                    .ShowDialog(IIf(IsNothing(frmTemplate.Parent), Me, frmTemplate.Parent))
                    .Close() 'Change made to solve memory Leak and word crash issue
                End With
                frmTemplate.Dispose()
                frmTemplate = Nothing
                _toSendPatLst = Nothing
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.findhealthplan, "Find health plan opened", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ' DisplayHealthPlanAlerts()
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.findhealthplan, "unsucessfully Find health plan opened", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.findhealthplan, "unsucessfully Find health plan opened", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub




End Class
