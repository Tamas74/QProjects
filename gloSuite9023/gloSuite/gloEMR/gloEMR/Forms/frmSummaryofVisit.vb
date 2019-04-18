Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
'Imports System.Text
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
'Imports gloAuditTrail
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRPrescription
'Imports gloEMRGeneralLibrary.glogeneral
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Runtime.InteropServices
Imports gloTaskMail
Imports gloEMRReports
Imports C1.Win.C1FlexGrid
Imports System.Collections.Specialized



Public Class frmSummaryofVisit
    Inherits System.Windows.Forms.Form
    Implements IExamChildEvents
    Implements IPatientContext
    Implements ISignature
    Implements IHotKey
    Implements IDisposable

    Public Event GetMicONOff(ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
    Friend Event ActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Implements mdlgloVoice.IExamChildEvents.ActivateExamChild
    Friend Event DeActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Implements mdlgloVoice.IExamChildEvents.DeActivateExamChild

    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    Private strSelectedFileNotes As String = Nothing
    Private _ListType As String = ""
    Private NotesFileName As String
    Private ImagePath As String
    Private m_status As String
    Private _TemplateName As String
    Private _PrinterName As String = ""
    Dim strExamNamebeforvisit As String = ""
    Dim strExamName As String
    Dim _ExamID As String = ""
    Dim strSelectNode As String
    Dim strICD9Code As String = ""
    Dim strICD9Desc As String = ""
    Dim strRefFileName As String

    Public isNewRefferalFromViewRefferal As Boolean = False
    Private blnIsReferrals As Boolean
    Dim _IsDXCPT As Boolean = False
    Dim blnRb As Boolean = False
    Dim blnFollowUpempty As Boolean = False
    Dim bIsRefferallettersave As Boolean = False


    Public dtDos As DateTime
    Private ReferralDate As DateTime
    Dim _VisitDate As Date

    Private m_PatientId As Int64 = 0
    Private m_ProviderId As Int64 = 0
    Private TemplateId As Int64 = 0

    Private m_ExamId As Long = 0
    Private m_VisitId As Long = 0
    Private _referralID As Long = 0
    Dim CurrentDocReferralID As Long = 0

    Private Savestatus As Integer

    Public WithEvents oCurDoc As Wd.Document
    ' Private WithEvents oTempDoc As Wd.Document

    Private gReferralArrlist As ArrayList
    Dim arrlist As ArrayList
    Public Shared ArrlistDiagonsis As ArrayList 'Added by Rahul patel on 25-11-2010 For Resolving Case no : GLO2010-0007091 i.e for Problem list Not get Populated 
    Public Shared ArrListTreament As ArrayList

    Public Shared PatientTemplateID As Int64 = 0     'Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
    Private LoginId As Int64
    Dim _AccountID As Int64


    Dim myidx As Int32

    Private WithEvents oWordApp As Wd.Application
    Dim r As Wd.Range
    'Private WithEvents wdPrint As AxDSOFramer.AxFramerControl
    'Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Dim objCriteria As DocCriteria

    Private WithEvents _PatientStrip As gloUC_PatientStrip

    Public myCaller As frmPatientExam

    Private ReferralsVoicecol As DNSTools.DgnStrings

    Private objReferralsDBLayer As New ClsReferralsDBLayer
    Private objclsMessage As New clsMessage


    Dim ObjWord As clsWordDocument
    Dim oProvider As New clsProvider


    Dim WithEvents gloUCTreatment As New gloUC_Treatment()

    Private Col_ICD9Code_Description As Int32 = 0
    Private Col_ICD9Code As Int32 = 1
    Private Col_ICD9Desc As Int32 = 2
    Private COl_CPTCode As Int32 = 3
    Private Col_CPTDesc As Int32 = 4
    Private Col_ModCode As Int32 = 5
    Private Col_ModDesc As Int32 = 6
    Private Col_Units As Int32 = 7
    Private Col_ICD9Count As Int32 = 8
    Private Col_CPTCount As Int32 = 9
    Private Col_ModCount As Int32 = 10
    Private Col_ICDRevision As Int32 = 11
    Private Col_Count As Int32 = 12

    Private Col_Drug As Int32 = 0 'new column added in 6030 to show drug name ,dosage,drugform, route together 
    Private Col_Dosage As Int32 = 1
    Private Col_Route As Int32 = 2
    Private Col_Frequency As Int32 = 3
    Private COl_Duration As Int32 = 4
    Private Col_Amount As Int32 = 5
    Private Col_Refills As Int32 = 6
    Private Col_StartDate As Int32 = 7
    Private Col_EndDate As Int32 = 8
    Private Col_NDCCOde As Int32 = 9 ''''will use NDCCode for futher reference
    Private Col_DrugName As Int32 = 10

    Private Col_RxSummaryCount As Int32 = 11

    Private Col_ID As Int32 = 0
    Private Col_Select As Int32 = 1
    Private Col_Task As Int32 = 2
    Private Col_User As Int32 = 3
    Private Col_Priority As Int32 = 4
    Private Col_DueDate As Int32 = 5
    Private Col_FollowupCount As Int32 = 6

    Private WithEvents _RxBusinessLayer As RxBusinesslayer
    Dim strFileName As String = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "PrescriptionReport.xml"
    Dim ReportHeaderCol As New Collection
    Dim PageHeaderCol As New Collection
    Dim PageFooterCol As New Collection
    Dim ReportFooterCol As New Collection
    Dim SectionsCol As Collection

    Dim pnlPresciptionReport As Panel
    Dim obj As ClsRxReportDictionary
    Dim objDataDictionary As IDataDictionary

    ' Dim oRpt As ReportDocument

    Dim myNode As myTreeNode
    Dim myNodeList As myList

    Dim Refresh_UCobjCriteria As DocCriteria

    'Friend WithEvents UiPanelManager1 As Janus.Windows.UI.Dock.UIPanelManager
    Friend WithEvents uiPanSplitScreen_SummaryofVisit As Janus.Windows.UI.Dock.UIPanelGroup
    Dim clsSplit_SummaryofVisit As New gloEMRGeneralLibrary.clsSplitScreen

    Dim oDetails As DataTable
    Dim dtddTemplate As DataTable
    Dim dtcmbTemplate As DataTable
    Private bnlIsFaxOpened As Boolean
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private blnSignClick As Boolean = False
    Dim WDocViewType As Wd.WdViewType = Nothing
    'Dim wordOptimizer As WordRefresh = Nothing
    Dim dtUser As DataTable
    Dim bIsMoreUsersExpanded As Boolean = False
    Dim Dashboardform As MainMenu = Nothing
    Private dia_activeWindow As IntPtr = IntPtr.Zero
    Private InsertReferralLetterFirstForFax As Boolean = 1      'CAS-19703-K4N5K5 – Some finished exams will have some parts missing when faxing with a cover page/Referral Letter.

    Public Property ExamName() As String
        Get
            Return strExamName
        End Get
        Set(ByVal Value As String)
            strExamName = Value
        End Set
    End Property

    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(m_PatientId)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If
        End Try
    End Sub
    Private m_NotToCheckFile As Boolean = False
    Public Sub New(ByVal intPatientId As Int64, ByVal intVisitId As Long, ByVal toCheckFile As Boolean, Optional ByVal nExamId As Long = 0, Optional ByVal strNotesFileName As String = "", Optional ByVal ExamProviderId As Int64 = 0, Optional ByVal isEXAMFinished As Boolean = False, Optional ByVal isReferrals As Boolean = False, Optional ByVal strExam As String = "", Optional ByVal ReferralID As Long = 0, Optional ByVal TemplateName As String = "")
        _referralID = ReferralID
        _TemplateName = TemplateName
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Savestatus = 1
        ' Add any initialization after the InitializeComponent() call.
        m_VisitId = intVisitId
        'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'If intPatientId = 0 Then
        '    m_PatientId = gnPatientID
        'Else
        m_PatientId = intPatientId
        'End If
        'end modification by dipak 
        m_NotToCheckFile = Not toCheckFile
        m_ProviderId = ExamProviderId
        m_ExamId = nExamId
        'line commented by dipak 20090918 because variable nowhere used
        'blnExamFinished = isEXAMFinished
        'end comment
        blnIsReferrals = isReferrals
        NotesFileName = strNotesFileName
        strExamName = strExam
        strExamNamebeforvisit = strExam
    End Sub

    Private Sub frmSummaryofVisit_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            If IsNothing(ParentForm) = False Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Dashboardform IsNot Nothing Then
                Dashboardform.RegisterMyHotKey()
                Dashboardform.ActiveDSO = wdReferrals
            End If
        End Try
        ShowMicrophone()

        If Not IsNothing(uiPanSplitScreen_SummaryofVisit) Then
            If Not IsNothing(uiPanSplitScreen_SummaryofVisit.Parent) Then
                If uiPanSplitScreen_SummaryofVisit.Parent.Text = "Split Screen" Then
                    uiPanSplitScreen_SummaryofVisit.Parent.Visible = True
                ElseIf uiPanSplitScreen_SummaryofVisit.Text = "Split Screen" Then
                    uiPanSplitScreen_SummaryofVisit.Visible = True
                End If
            End If
        End If

    End Sub

    Private Sub OpenPlanOfTreatment()
        Dim oPlanOfTreatment As New frmTreatmentPlan(m_PatientId, "Exam")
        oPlanOfTreatment.ShowInTaskbar = False
        oPlanOfTreatment.ShowDialog(Me)
    End Sub

    Private Sub ShowReconciliation()
        Dim ogloCCDReconcile As New gloCCDLibrary.gloCCDReconcilation
        Dim frmReconcilation As frmReconcileList = Nothing
        Try

            frmReconcilation = New frmReconcileList(m_PatientId, _ListType)
            frmReconcilation.LoginUser = gstrLoginName
            frmReconcilation.LoginID = gnLoginID
            '    If frmReconcilation.ShowDialog() = DialogResult.OK Then

            frmReconcilation.ShowDialog(IIf(IsNothing(frmReconcilation.Parent), Me, frmReconcilation.Parent))
            ' RefershHistoryAfterReconciliation()
            '  End If
            If IsNothing(Me.ParentForm) = False Then
                CType(Me.ParentForm, MainMenu).ShowReconciliationAlert()
            End If
            Dim _dtUnfinishedReconcile As DataTable


            _ListType = ""
            _dtUnfinishedReconcile = ogloCCDReconcile.GetUnFinishedReconcileList(m_PatientId)
            If IsNothing(_dtUnfinishedReconcile) = False Then
                If _dtUnfinishedReconcile.Rows.Count > 0 Then


                    _ListType = Convert.ToString(_dtUnfinishedReconcile.Rows(0)("sListType"))


                End If
            End If
            If IsNothing(_dtUnfinishedReconcile) = False Then
                _dtUnfinishedReconcile.Dispose()
                _dtUnfinishedReconcile = Nothing
            End If
            Dim _isReadyLists As Boolean = False

            _isReadyLists = ogloCCDReconcile.IsReadyListsPresent(m_PatientId, _ListType)
            If _isReadyLists = True Then
                tlsReconcile.Enabled = True
            Else
                tlsReconcile.Enabled = False
            End If
            ShowRecommendationsAlert()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            If IsNothing(frmReconcilation) = False Then
                frmReconcilation.Dispose()
                frmReconcilation = Nothing
            End If

            If IsNothing(ogloCCDReconcile) = False Then
                ogloCCDReconcile = Nothing
            End If
        End Try

    End Sub
    Private Sub frmSummaryofVisit_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        TurnOffMicrophone()

        If Not IsNothing(Me.Parent) Then
            If Not IsNothing(uiPanSplitScreen_SummaryofVisit) Then
                If Not IsNothing(uiPanSplitScreen_SummaryofVisit.Parent) Then
                    If uiPanSplitScreen_SummaryofVisit.Parent IsNot Me Then
                        uiPanSplitScreen_SummaryofVisit.Parent.Visible = False
                        uiPanSplitScreen_SummaryofVisit.Parent.Hide()
                        uiPanSplitScreen_SummaryofVisit.Parent.Update()
                    End If
                End If
            End If
        End If

        'If Not IsNothing(Me.Parent) Then
        '    If Not IsNothing(uiPanSplitScreen_SummaryofVisit) Then
        '        If Not IsNothing(uiPanSplitScreen_SummaryofVisit.Parent) Then
        '            If uiPanSplitScreen_SummaryofVisit.Parent.Text = "Split Screen" Then
        '                uiPanSplitScreen_SummaryofVisit.Parent.Visible = False
        '            End If
        '        End If
        '    End If
        'End If

    End Sub

    Private Sub frmSummaryofVisit_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dim _cnt As Integer = 0
        Try
            UpdateVoiceLog("frmSummaryofVisit_FormClosed started")
            If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                TurnOffMicrophone()

            End If
            RaiseEvent DeActivateExamChild(Me)
            myCaller = Nothing
            ''Check count of c1treament grid-Added on 20111012
            ''To check whtehr count <=1 then get examname
            ''becoz if ther are no items in grid then exam name was not saving as template name
            ''instead it was saving as previous disgnosis which was deleted.
            _cnt = gloUCTreatment.GetC1TreatementCount()
            'Clipboard.Clear()
            UpdateVoiceLog("frmSummaryofVisit_FormClosed is completed")
            Dim blnIsFound As Boolean = False
            If ExamName = strExamNamebeforvisit AndAlso C1Diagnosis.Rows.Count > 1 Then
                For i As Integer = 0 To C1Diagnosis.Rows.Count - 1
                    If C1Diagnosis.GetData(i, Col_ICD9Code_Description) = strExamNamebeforvisit Then
                        blnIsFound = True
                        Exit For
                    End If
                Next
                If blnIsFound = False Then
                    ''
                    'strICD9Code = C1Diagnosis.GetData(1, Col_ICD9Code).ToString().Trim()
                    'strICD9Desc = C1Diagnosis.GetData(1, Col_ICD9Desc).ToString().Trim()
                    'ExamName = strICD9Code & " " & strICD9Desc
                    ''
                    '  ExamName = C1Diagnosis.GetData(1, Col_ICD9Code_Description)
                End If

            Else
                If gblnICD9Driven = True Then
                    If C1Diagnosis.Rows.Count <= 1 Then
                        ExamName = ""
                    End If
                ElseIf gblnICD9Driven = False Then
                    If _cnt <= 1 Then
                        ExamName = ""
                    End If
                End If
            End If
            MyMDIParent.MdiExamChildDeActivate(Me, True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not myNodeList Is Nothing Then myNodeList = Nothing
            _cnt = Nothing
            If Not oProvider Is Nothing Then
                oProvider.Dispose()
                oProvider = Nothing
            End If
            If Not objclsMessage Is Nothing Then
                objclsMessage.Dispose()
                objclsMessage = Nothing
            End If
          
            If Not objReferralsDBLayer Is Nothing Then
                objReferralsDBLayer.Dispose()
                objReferralsDBLayer = Nothing
            End If
            If Not gloUCTreatment Is Nothing Then
                gloUCTreatment.Dispose()
                gloUCTreatment = Nothing
            End If
            appSettings = Nothing
            If (IsNothing(Dashboardform) = False) Then
                Dashboardform.ActiveDSO = Nothing
            End If

            If (IsNothing(C1Followup) = False) Then
                C1Followup.Styles.Clear()
                C1Followup.Dispose()
            End If


            If (IsNothing(C1ProblemList) = False) Then
                C1ProblemList.Styles.Clear()
                C1ProblemList.Dispose()
            End If

            If (IsNothing(C1RxSummary) = False) Then
                C1RxSummary.Styles.Clear()
                C1RxSummary.Dispose()
            End If

            If (IsNothing(C1Diagnosis) = False) Then
                C1Diagnosis.Styles.Clear()
                C1Diagnosis.Dispose()
            End If

            If (IsNothing(mdlFAX.Owner) = False) Then
                mdlFAX.Owner = Nothing
            End If



        End Try
    End Sub

    Private Sub frmSummaryofVisit_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            If (IsNothing(myCaller) = False) Then
                If myCaller.blnSummaryFinish = False Then
                    If (IsNothing(myCaller.pnlToolStrip_32_New) = False) Then
                        myCaller.pnlToolStrip_32_New.Enabled = True
                    End If
                End If
            End If

            If (IsNothing(clsSplit_SummaryofVisit) = False) Then
                'Added by Amit to avoid memory leaks 
                clsSplit_SummaryofVisit.SaveControlDisplaySettings()
            End If
            
            If (IsNothing(uiPanSplitScreen_SummaryofVisit) = False) Then
                uiPanSplitScreen_SummaryofVisit.Dispose()
                uiPanSplitScreen_SummaryofVisit = Nothing
            End If

            If (IsNothing(clsSplit_SummaryofVisit) = False) Then
                clsSplit_SummaryofVisit.Dispose()
                clsSplit_SummaryofVisit = Nothing
            End If

            If IsNothing(dtddTemplate) = False Then
                dtddTemplate.Dispose()
                dtddTemplate = Nothing
            End If

            If IsNothing(dtcmbTemplate) = False Then
                dtcmbTemplate.Dispose()
                dtcmbTemplate = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub frmSummaryofVisit_HandleDestroyed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleDestroyed

    End Sub
    Private AfterLoad As Boolean = False

    
    Private Sub frmSummaryofVisit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''Resolved case #00001002: Referral Letter Fax issue
            GloUC_AddRefreshDic1.ShowRefreshButton(False)
            MyMDIParent.MdiExamChildActivate(Me, True, Dashboardform)
            'Dim myScreenWidth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth
            'Dim myScreenHeight As Integer = System.Windows.SystemParameters.PrimaryScreenHeight
            'If (Me.Width > myScreenWidth) OrElse (Me.Height > myScreenHeight) Then
            '    Me.MaximumSize = New System.Drawing.Size(myScreenWidth, myScreenHeight)
            '    Me.AutoScroll = True
            'End If

            'Dim DesignScreenWidth As Integer = 1280
            'Dim DesignScreenHeight As Integer = 1024

            'Added for Screen Resolution case on 7/24/2014

            Dim DesignScreenWidth As Integer = Me.Width
            Dim DesignScreenHeight As Integer = Me.Height
            Dim CurrentScreenWidth As Integer = Screen.PrimaryScreen.Bounds.Width - 20
            Dim CurrentScreenHeight As Integer = Screen.PrimaryScreen.Bounds.Height - 50
            Dim RatioX As Double = CurrentScreenWidth / DesignScreenWidth
            Dim RatioY As Double = CurrentScreenHeight / DesignScreenHeight

            'Dim myWidth As Integer = Me.Width * RatioX
            'Dim myHeight As Integer = Me.Height * RatioY

            Me.Width = Me.Width * RatioX
            Me.Height = (Me.Height * RatioY)
            'Me.MaximumSize = New Size(myWidth, myHeight)
            Me.Top = Me.Height - Screen.PrimaryScreen.Bounds.Height + 50
            Me.Left = Me.Width - Screen.PrimaryScreen.Bounds.Width + 20
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.AutoScroll = True
            Dim myHeight As Integer = Me.pnlMainSummary.Location.Y + Me.pnlMainSummary.Height
            If Me.Height > myHeight Then
                Me.Height = myHeight
                Me.StartPosition = FormStartPosition.CenterScreen
                Me.AutoScroll = True
            End If

            If Not objReferralsDBLayer Is Nothing Then
                objReferralsDBLayer.Dispose()
                objReferralsDBLayer = Nothing
            End If
            objReferralsDBLayer = New ClsReferralsDBLayer   'Change made to solve memory Leak and word crash issue

            If Not blnIsReferrals Then

                ShowMessageForPendingReconciliation()

                ShowRecommendationsAlert()

                _VisitDate = DateTime.Now
                _VisitDate = Format(_VisitDate, "MM/dd/yyyy") & " " & Format(_VisitDate, "Short Time")
                Call Get_PatientDetails()

                SetICD9CPTGridStyle()
                If gblnICD9Driven Then
                    FillICDCPTMOD()
                Else
                    FillCPTICDMOD()
                End If

                SetRxGridStyle()
                FillRxSummary()

                SetFollowupGridStyle()

                FillFollowupGrid()
                Fill_ProblemList()


                LoadReferralTemplate()

                'Retrieve Default Referral Letter
                ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
                If PatientTemplateID <> 0 Then
                    TemplateId = PatientTemplateID
                    cmbTemplate.SelectedValue = TemplateId
                    PatientTemplateID = 0
                Else
                    If Trim(gstrLoginProviderName) <> "All" AndAlso Trim(gstrLoginProviderName) <> "" Then

                        TemplateId = objReferralsDBLayer.GetReferralTemplateBYProviderID(gnLoginProviderID)

                        ddTemplate.SelectedValue = TemplateId
                    End If
                End If
                ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013

                If ddTemplate.SelectedValue Is Nothing AndAlso ddTemplate.Items.Count > 0 Then
                    ddTemplate.SelectedIndex = 0
                    TemplateId = ddTemplate.SelectedValue
                End If

                loadPatientStrip(False)

                pnlMainSummary.Visible = True
                pnlToolStrp.Visible = True
                pnlMainReferrals.Visible = False
                pnltlsReferrals.Visible = False

                ExamNotesSelection(False)


            Else
                loadPatientStrip(True)
            End If

            Dim Node As myList = Nothing


            If gbShowviewRecommendation Then

                tlsRecommendation.Visible = True

                ''Date: 22May2014 Sagar Ghodke, Recommendation Alert panel was showing even if no 
                ''recommendations are present. In ShowRecommendationsAlert() function called above we 
                ''set the panel visible true/false accordingly. Now, if no recommendations are present
                ''it will be set to visible false and if present set to visible true
                '' hence no need to make it visible true here.

                'pnlRecomendationAlert.Visible = True
                'lblRecomendationAlert.Visible = True

            Else

                tlsRecommendation.Visible = False
                pnlRecomendationAlert.Visible = False
                lblRecomendationAlert.Visible = False

            End If

            If gblnIsSecureMsgEnable = False OrElse gblnSecureUserrights = False Then
                tls_SendRefLtr.Visible = False
                tlsPrintReferrals.Visible = True
                tlsFaxReferrals.Visible = True
                tlsFaxReferralsClose.Visible = True
                tlsPrintReferralsClose.Visible = True
            Else
                tls_SendRefLtr.Visible = True
                tlsPrintReferrals.Visible = False
                tlsFaxReferrals.Visible = False
                tlsFaxReferralsClose.Visible = False
                tlsPrintReferralsClose.Visible = False
            End If

            ''Resolved Bug No.68907::gloEMR> Patient Exam > Referral Letter > application shows KB311765 Office Framer Control Sample and File menu.
            If Not IsNothing(wdReferrals) Then
                wdReferrals.Titlebar = False
                wdReferrals.Menubar = False
            End If
            ''
            fillDischargeDetails()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            Try
                UpdateVoiceLog("Add Voice collection Started at Referrals Load")
                ReferralsVoicecol = New DNSTools.DgnStrings

                'Fill Voice Collections
                Call AddBasicVoiceCommands()

                UpdateVoiceLog("Add Voice collection Completed at Referrals Load")

                If Not myCaller Is Nothing Then
                    Dim frm As MainMenu
                    frm = CType(MyMDIParent, MainMenu)
                    'Add Voice Commands

                    UpdateVoiceLog("SetRecognition Started at Referrals Load")
                    frm.Vcmd.ExecuteScript("SetRecognitionMode 0", 0)
                    UpdateVoiceLog("SetRecognition Completed at Referrals Load")
                    frm = Nothing

                ElseIf Not Me.Owner Is Nothing Then

                    Dim frm As MainMenu

                    '11-Jun-13 Aniket: Resolving Bug 52108 
                    If TypeOf (Me.Owner) Is MainMenu Then
                        frm = CType(Me.Owner, MainMenu)
                        'Add Voice Commands

                        UpdateVoiceLog("SetRecognition Started at Referrals Load")
                        frm.Vcmd.ExecuteScript("SetRecognitionMode 0", 0)
                        UpdateVoiceLog("SetRecognition Completed at Referrals Load")

                        frm = Nothing
                    End If


                End If





            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, "Error Initializing Voice in Patient Referrals load " & ex.Message, gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show("Error Initializing Voice in Patient Referrals", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ex = Nothing
            End Try
        End If
        RaiseEvent ActivateExamChild(Me)
        Try
            trPatientReferrals.AllowDrop = True

            '' Load Template to Combo
            LoadTemplate()

            'Retrieve Default Referral Letter
            ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
            If PatientTemplateID <> 0 Then
                TemplateId = PatientTemplateID
                cmbTemplate.SelectedValue = TemplateId
                PatientTemplateID = 0
            Else
                If Trim(gstrLoginProviderName) <> "All" AndAlso Trim(gstrLoginProviderName) <> "" Then
                    TemplateId = objReferralsDBLayer.GetReferralTemplateBYProviderID(gnLoginProviderID)
                    cmbTemplate.SelectedValue = TemplateId
                    '' if Default template is not associated with Parovider then select the template from combobox
                    '' will be the 1st template in the combo
                    If cmbTemplate.SelectedValue = 0 AndAlso cmbTemplate.Items.Count > 0 Then
                        cmbTemplate.SelectedIndex = 0
                        TemplateId = cmbTemplate.SelectedValue
                    End If
                End If
                If cmbTemplate.SelectedValue Is Nothing AndAlso cmbTemplate.Items.Count > 0 Then
                    cmbTemplate.SelectedIndex = 0
                    TemplateId = cmbTemplate.SelectedValue
                End If
            End If
            If (_referralID = 0) Then
                Dim rootnode As myTreeNode
                rootnode = New myTreeNode("Referrals", -1)
                rootnode.ImageIndex = 0
                rootnode.SelectedImageIndex = 0
                trReferrals.Nodes.Add(rootnode)
                '''''
                ' rootnode.Dispose() : 
                rootnode = Nothing 'Change made to solve memory Leak and word crash issue

                FillReferrals()

                ReferralDate = DateTime.Now.Date

                PopulatePatientReferrals()
            Else
                'else case added by dipak 20100106 for implement fubctionality open referral letters from view->referals
                Dim rootnode As myTreeNode
                rootnode = New myTreeNode("Referrals", -1)
                rootnode.ImageIndex = 0
                rootnode.SelectedImageIndex = 0
                trReferrals.Nodes.Add(rootnode)
                '''''
                'rootnode.Dispose() : 
                rootnode = Nothing 'Change made to solve memory Leak and word crash issue

                FillReferrals()
                ReferralDate = DateTime.Now.Date
                PopulatePatientReferrals(_referralID)

            End If
            Call ResetSearch()

            txtSearchReferrals.Select()



            ExamNotesSelection(True)


            If gblnCoSignFlag Then
                tlsCoSign.Visible = True
            Else
                tlsCoSign.Visible = False
            End If

            If gblnExamSelection = 2 Then
                If IsNothing(strSelectedFileNotes) Then
                    UpdateVoiceLog("CreateSelectedNotes process started ")
                    strSelectedFileNotes = CreateSelectedNotes(NotesFileName)
                    UpdateVoiceLog("CreateSelectedNotes process completed ")
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

        If blnIsReferrals Then
            pnlMainSummary.Visible = False
            pnlToolStrp.Visible = False
            pnlMainReferrals.Visible = True
            pnltlsReferrals.Visible = True

            If (_referralID = 0) Then
                If (isNewRefferalFromViewRefferal = True) Then
                    rbNone.Checked = True
                    rbNotes.Visible = False
                    rbSelect.Visible = False
                    rbNone.Visible = False
                End If
            Else
                'else case added by dipak 20100106 for implement fubctionality open referral letters from view->referals
                'hide/disable controls
                cmbTemplate.Text = _TemplateName
                rbNone.Checked = True
                rbNotes.Visible = False
                rbSelect.Visible = False
                rbNone.Visible = False
                cmbTemplate.Enabled = False
                pnltrReferrals.Visible = False
                ToolStripButton9.Visible = False
                ToolStripButton8.Visible = False

                ''Sandip Darade 20100211
                ''hide fax all & close button 
                ''button fax & close will do the function
                ToolStripButton6.Visible = False
                ToolStripButton22.Tag = ToolStripButton6.Tag
                ToolStripButton22.ToolTipText = ToolStripButton6.ToolTipText

                ''hide print all & close button 
                ''button ptint & close will do the function
                ToolStripButton10.Visible = False
                ToolStripButton19.Tag = ToolStripButton10.Tag
                ToolStripButton19.ToolTipText = ToolStripButton10.ToolTipText

            End If
        End If
        If blnIsReferrals Then
            calltoAddRefreshButtonControl()
        End If
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, m_PatientId, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        Try
            If oCurDoc Is Nothing Then
                GloUC_AddRefreshDic1.Visible = False
            Else
                GloUC_AddRefreshDic1.Visible = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try


        objCriteria = New DocCriteria
        ObjWord = New clsWordDocument

        clsSplit_SummaryofVisit.clsUCLabControl = New gloUserControlLibrary.gloUC_TransactionHistory()
        clsSplit_SummaryofVisit.clsPatientExams = New clsPatientExams()
        clsSplit_SummaryofVisit.clsPatientLetters = New clsPatientLetters()
        clsSplit_SummaryofVisit.clsPatientMessages = New clsMessage()
        clsSplit_SummaryofVisit.clsNurseNotes = New clsNurseNotes()
        clsSplit_SummaryofVisit.clsHistory = New clsPatientHistory()
        clsSplit_SummaryofVisit.clsLabs = New clsDoctorsDashBoard()
        clsSplit_SummaryofVisit.clsDMS = New gloEDocumentV3.eDocManager.eDocGetList()
        clsSplit_SummaryofVisit.clsRxmed = New clsPatientDetails()
        clsSplit_SummaryofVisit.clsOrders = New clsPatientDetails()
        clsSplit_SummaryofVisit.clsProblemList = New clsPatientProblemList()
        clsSplit_SummaryofVisit.blnShowSmokingStatusCol = gblnShowSmokingColumn


        uiPanSplitScreen_SummaryofVisit = clsSplit_SummaryofVisit.LoadSplitControl(Me, m_PatientId, m_VisitId, "SummaryofVisit", objCriteria, ObjWord, gnClinicID, gnLoginID)
        uiPanSplitScreen_SummaryofVisit.BringToFront()

        'Issue::On the DMS screen the split control is always expanded. It does not remember its collapsed state 
        'If IsNothing(Me.Parent) Then
        '    uiPanSplitScreen_SummaryofVisit.AutoHide = False
        '    'Else
        '    '    ' If clsSplit_SummaryofVisit.dck = 0 Then
        '    '    uiPanSplitScreen_SummaryofVisit.AutoHide = True
        '    '    'End If
        'End If

        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False OrElse gblnSecureUserrights = False Then
            tblbtn_SecureMsg.Visible = False

        End If
        AfterLoad = True
    End Sub

    

    Private Sub ShowMessageForPendingReconciliation()
        Dim _objReconciliation As New gloCCDLibrary.gloCCDReconcilation
        Dim _dtUnfinishedReconcile As DataTable

        Try
            _dtUnfinishedReconcile = _objReconciliation.GetUnFinishedReconcileList(m_PatientId)
            If IsNothing(_dtUnfinishedReconcile) = False Then
                If _dtUnfinishedReconcile.Rows.Count > 0 Then
                    _ListType = Convert.ToString(_dtUnfinishedReconcile.Rows(0)("sListType"))
                End If
            End If
            If IsNothing(_dtUnfinishedReconcile) = False Then
                _dtUnfinishedReconcile.Dispose()
                _dtUnfinishedReconcile = Nothing
            End If
            tlsReconcile.Enabled = False
            Dim _isReadyLists As Boolean = False
            Dim ogloCCDReconcile As New gloCCDLibrary.gloCCDReconcilation
            _isReadyLists = ogloCCDReconcile.IsReadyListsPresent(m_PatientId, _ListType)
            If IsNothing(ogloCCDReconcile) = False Then
                ogloCCDReconcile = Nothing
            End If
            If _isReadyLists = True Then
                tlsReconcile.Enabled = True
                MessageBox.Show("Patient has Pending Clinical Reconciliations. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
            Else
                tlsReconcile.Enabled = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If IsNothing(_objReconciliation) = False Then
                _objReconciliation = Nothing
            End If


        End Try
    End Sub
    Private Sub uiPanSplitScreen_SelectedPanelChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.UI.Dock.PanelActionEventArgs) Handles uiPanSplitScreen_SummaryofVisit.SelectedPanelChanged
        'clsSplit.loadSplitControlData(m_PatientId, m_VisitId, "PatientLetter", objCriteria, ObjWord)
    End Sub

    Private Sub ShowRecommendationsAlert()
        Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement
        Dim _dt As DataTable = Nothing
        Try
            pnlRecomendationAlert.Visible = False
            oDM.GetRecommendationsAlerts(m_PatientId)
            _dt = oDM.GetRecommendationsCountAndName(m_PatientId)
            lblRecomendationAlert.Text = ""
            lblLastRecomendationName.Text = ""
            If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then

                If Convert.ToInt32(_dt.Rows(0)("RecommendationCount")) > 0 Then

                    lblRecomendationAlert.Text = "Recommendations (" & _dt.Rows(0)("RecommendationCount") & ") :" 'set the count of recommendations here 
                    lblLastRecomendationName.Text = _dt.Rows(0)("RecommendationName").ToString()  'set the last announced recommendation name here
                    pnlRecomendationAlert.Visible = True

                End If

            End If

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            pnlRecomendationAlert.Visible = False

            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If

            If _dt IsNot Nothing Then
                _dt.Dispose()
                _dt = Nothing
            End If

            ex = Nothing
        Finally

            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If

            If _dt IsNot Nothing Then
                _dt.Dispose()
                _dt = Nothing
            End If
        End Try
    End Sub

    Private Sub ResetVisitSummary()
        'pnlMainSummary.BringToFront()
        Me.Text = "Summary of Visit"
        'pnlMainReferrals.SendToBack()

        pnlMainSummary.Visible = True
        pnlToolStrp.Visible = True
        pnlMainReferrals.Visible = False
        pnltlsReferrals.Visible = False

        Try
            gloPatient.gloPatient.GetWindowTitle(Me, m_PatientId, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub loadPatientStrip(ByVal blnReferrals As Boolean)


        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Name = "PtCtrl"
        _PatientStrip.DTP.CustomFormat = "MM/dd/yyyy"

        If blnReferrals Then
            _PatientStrip.ShowDetail(m_PatientId, gloUC_PatientStrip.enumFormName.ReferralLetter)
            _PatientStrip.DTPValue = Format(Now, "MM/dd/yyyy")
            Me.pnlMainReferrals.Controls.Add(_PatientStrip)
            _PatientStrip.Padding = New Padding(3, 0, 3, 0)
            _PatientStrip.BringToFront()
            Panel2.BringToFront()
            _PatientStrip.DTPEnabled = True
        Else
            '' SUDHIR 20090828 '' TO SHOW RX PROVIDER ASSOCIATION BUTTON ON PATIENT STRIP ''
            oProvider = New clsProvider
            If IsNothing(_RxBusinessLayer) = False Then
                If oProvider.IsProvider_Senior(_RxBusinessLayer.ProviderID) = False Then
                    gblnMultipleSupervisorsforPaperRx = RxBusinesslayer.GetRxProviderAssociationSettings
                    _PatientStrip.ShowRxProviderAssociation = gblnMultipleSupervisorsforPaperRx
                Else
                    _PatientStrip.ShowRxProviderAssociation = False
                End If
            Else
                _PatientStrip.ShowRxProviderAssociation = False
            End If

            '' END SUDHIR ''
            _PatientStrip.ShowDetail(m_PatientId, gloUC_PatientStrip.enumFormName.SummaryOfVisit)
            _PatientStrip.DTPValue = Format(dtDos, "MM/dd/yyyy")
            Me.pnlMainSummary.Controls.Add(_PatientStrip)
            AddHandler _PatientStrip.RxProviderAssociation_Click, AddressOf _PatientStrip_RxProviderAssociation_Click
            _PatientStrip.Padding = New Padding(3, 0, 3, 0)
            _PatientStrip.SendToBack()
            pnlToolStrp.SendToBack()
            _PatientStrip.DTPEnabled = False
        End If

    End Sub
    Private Sub _PatientStrip_RxProviderAssociation_Click() 'Handles _PatientStrip.RxProviderAssociation_Click
        Try
            '' SUDHIR 20090817 '' 
            Dim ofrm As New frmRxProviderAssociation(m_PatientId, m_VisitId, dtDos, m_ProviderId)
            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
            ofrm.Close()
            ofrm.Dispose()
            ofrm = Nothing  'Change made to solve memory Leak and word crash issue
            '' END SUDHIR ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub tlsVisitSummary_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsVisitSummary.ItemClicked

        Select Case e.ClickedItem.Tag

            Case "HCFARpt"
                PrintHCFAReport()

            Case "Complete"
                UpdateExamLog("B1: Complete Note Process Started", m_PatientId, m_ExamId)

                'Aniket: 08-Feb-17 Aniket:  Exam Note Should not be Finished completed without Dx and CPT codes
                If ValidateDxCPT(m_ExamId, m_VisitId) = True Then
                    CompleteNotes()
                End If

                UpdateExamLog("B1: Complete Note Process Completed", m_PatientId, m_ExamId)

            Case "FaxReferrals"
                UpdateExamLog("B2: FAX Referral Process Started", m_PatientId, m_ExamId)
                bnlIsFaxOpened = True
                GeneratePrintFaxDocument(False, False)
                bnlIsFaxOpened = False
                UpdateExamLog("B2: FAX Referral Process Completed", m_PatientId, m_ExamId)

            Case "PrintReferrals"
                UpdateExamLog("B3: Print Referral Process Started", m_PatientId, m_ExamId)
                GeneratePrintFaxDocument(False)
                UpdateExamLog("B3: Print Referral Process Completed", m_PatientId, m_ExamId)

            Case "FaxReferralsClose"
                UpdateExamLog("B4: Fax Referrals and Close Process Started", m_PatientId, m_ExamId)

                'Aniket: 08-Feb-17 Aniket:  Exam Note Should not be Finished completed without Dx and CPT codes
                If ValidateDxCPT(m_ExamId, m_VisitId) = True Then
                    bnlIsFaxOpened = True
                    If GeneratePrintFaxDocument(True, False) Then
                        CompleteNotes()
                    End If
                    bnlIsFaxOpened = False
                End If

                UpdateExamLog("B4: Fax Referrals and Close Process Completed", m_PatientId, m_ExamId)

            Case "PrintReferralsClose"
                UpdateExamLog("B5: Print Referrals and Close Process Started", m_PatientId, m_ExamId)

                'Aniket: 08-Feb-17 Aniket:  Exam Note Should not be Finished completed without Dx and CPT codes
                If ValidateDxCPT(m_ExamId, m_VisitId) = True Then
                    If GeneratePrintFaxDocument(True) Then
                        CompleteNotes()
                    End If
                End If

                UpdateExamLog("B5: Print Referrals and Close Process Completed", m_PatientId, m_ExamId)

            Case "PrintRx"
                PrintRxSummary()

            Case "Close"
                myCaller.pnlToolStrip_32_New.Enabled = True
                myCaller.blnSummaryFinish = False
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)

            Case "Summmary"
                GetPatientEducation()

            Case "Reconcile"
                ShowReconciliation()
            Case "PlanOfTreatment"
                OpenPlanOfTreatment()
            Case Else
                Me.TopMost = True
                Me.TopMost = False

        End Select

    End Sub


    Private Sub mnuItem_PrintRefLtr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItem_PrintRefLtr.Click
        UpdateExamLog("B6: Print Referral Process Started", m_PatientId, m_ExamId)
        GeneratePrintFaxDocument(False)
        UpdateExamLog("B6: Print Referral Process Completed", m_PatientId, m_ExamId)
    End Sub

    Private Sub mnuItem_FaxRefLtr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItem_FaxRefLtr.Click
        UpdateExamLog("B7: FAX Referral Process Started", m_PatientId, m_ExamId)
        bnlIsFaxOpened = True
        GeneratePrintFaxDocument(False, False)
        bnlIsFaxOpened = False
        UpdateExamLog("B7: FAX Referral Process Completed", m_PatientId, m_ExamId)
    End Sub

    Private Sub mnuItem_SendRefLtr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItem_SendRefLtr.Click
        SendSecureMessage(False)
    End Sub

    Private Sub mnuItem_FaxRefcom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItem_FaxRefcom.Click

        UpdateExamLog("B8: FAX Referral and Complete Process Started", m_PatientId, m_ExamId)

        'Aniket: 08-Feb-17 Aniket:  Exam Note Should not be Finished completed without Dx and CPT codes
        If ValidateDxCPT(m_ExamId, m_VisitId) = True Then
            bnlIsFaxOpened = True
            If GeneratePrintFaxDocument(True, False) Then
                CompleteNotes()
            End If
            bnlIsFaxOpened = False
        End If
        
        UpdateExamLog("B8: FAX Referral and Complete Process Completed", m_PatientId, m_ExamId)
    End Sub

    Private Sub mnuItem_PrintRefCom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItem_PrintRefCom.Click

        UpdateExamLog("B9: Print Referral and Complete Process Started", m_PatientId, m_ExamId)

        'Aniket: 08-Feb-17 Aniket:  Exam Note Should not be Finished completed without Dx and CPT codes
        If ValidateDxCPT(m_ExamId, m_VisitId) = True Then
            If GeneratePrintFaxDocument(True) Then
                CompleteNotes()
            End If
        End If
        
        UpdateExamLog("B9: Print Referral and Complete Process Completed", m_PatientId, m_ExamId)
    End Sub

    Private Sub mnuItem_SendRefCom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItem_SendRefCom.Click

        'Aniket: 08-Feb-17 Aniket:  Exam Note Should not be Finished completed without Dx and CPT codes
        If ValidateDxCPT(m_ExamId, m_VisitId) = True Then
            If SendSecureMessage(True) Then
                CompleteNotes()
            End If
        End If
        
    End Sub

    Public Sub GetPatientEducation()
        Dim dtWord As New DataTable
        Try

            If Trim(strPatientFirstName) <> "" Then
                If m_VisitId = 0 Then
                    m_VisitId = GenerateVisitID(m_PatientId)
                End If
                ''''check Template is present
                ObjWord = New clsWordDocument
                dtWord = ObjWord.FillTemplates(enumTemplateFlag.PatientEducation)
                ObjWord = Nothing   'Change made to solve memory Leak and word crash issue
                If dtWord.Rows.Count = 0 Then
                    ''''If not present then exit from sub
                    MessageBox.Show("No Template is associated for Patient Education. Please associate Template for Patient Education category", "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub

                Else
                    Dim frmPatientPE As New frmPatientEducation(m_VisitId, m_PatientId, m_ExamId)
                    Call TurnOffMicrophone()
                    With frmPatientPE
                        ''''Code Add by Pramod to set Property enable false on exam form when Patient education form is open.
                        .myCaller = Me.myCaller
                        .WindowState = FormWindowState.Normal
                        .StartPosition = FormStartPosition.CenterScreen
                        .ShowDialog(IIf(IsNothing(frmPatientPE.Parent), Me, frmPatientPE.Parent))
                        'Change made to solve memory Leak and word crash issue
                        .Close()
                        .Dispose()
                        ' GetdataFromOtherForms(enumDocType.PatientEducation)
                    End With
                    frmPatientPE = Nothing 'Change made to solve memory Leak and word crash issue
                    Call ShowMicrophone()

                End If
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not dtWord Is Nothing Then
                dtWord.Dispose()
                dtWord = Nothing
            End If
        End Try
    End Sub

    Private Sub CompleteNotes()

        '27-May-14 Aniket: Added Problem List SnoMed validation on SummaryOfVisit screen
        Dim _dttable As DataTable = Nothing
        Dim oclsProblemListV2 As clsPatientProblemList = New clsPatientProblemList
        Dim _ISSmonedCodeMandatory As Boolean = False
        Dim _cntProblem As Int64 = 0

        Try

            _ISSmonedCodeMandatory = oclsProblemListV2.IsSnomedMandatory()

            If _ISSmonedCodeMandatory Then
                _cntProblem = oclsProblemListV2.getSnomedCodemissingProblem(m_PatientId) ', m_ExamId)
                If _cntProblem > 0 Then
                    Dim strMessage As String

                    strMessage = "SNOMED-CT is required for all new Problems." & vbNewLine & vbNewLine & "Exam cannot be completed until a SNOMED-CT has been selected for each Problem."

                    '30-Apr-14 Aniket: As per mail 'Another issue for the v8002 hot fix' do not let the user finish the exam if problem list does not have SnoMed.
                    MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If

            'Dim frmPatientExam As New frmPatientExam
            'frmPatientExam.dischargeDisposition = ddDischargeDisposition.SelectedValue
            'frmPatientExam.diagnosisType = ddDiagnosisType.SelectedValue
            ''changes done for memory optimization  CAS-19567-R8J8D9
            frmPatientExam.dischargeDisposition = ddDischargeDisposition.SelectedValue
            frmPatientExam.diagnosisType = ddDiagnosisType.SelectedValue

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            If oclsProblemListV2 IsNot Nothing Then
                oclsProblemListV2.Dispose()
                oclsProblemListV2 = Nothing

            End If

        End Try


        myCaller.pnlToolStrip_32_New.Enabled = False
        AddTasks()
        myCaller.blnSummaryFinish = True

        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)

    End Sub

    'Aniket: 08-Feb-17 Aniket:  Exam Note Should not be Finished completed without Dx and CPT codes
    Private Function ValidateDxCPT(ExamID As Long, VisitID As Long) As Boolean

        Dim blnFinishExam As Boolean = True

        Try
            If gblnIsICD9CPTRequired = True Then

                Dim objICD9CPT As ClsDiagnosisDBLayer
                Dim blnIsICD9CPTRequired As Boolean

                objICD9CPT = New ClsDiagnosisDBLayer
                blnIsICD9CPTRequired = objICD9CPT.GetICD9CPT(ExamID, VisitID)

                If IsNothing(objICD9CPT) = False Then
                    objICD9CPT.Dispose()
                    objICD9CPT = Nothing
                End If

                If blnIsICD9CPTRequired = False Then
                    If (MessageBox.Show("Dx/CPT are not added for this Exam. Do you still want to finish the exam?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) = Windows.Forms.DialogResult.No Then
                        blnFinishExam = False
                    End If
                End If
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        Return blnFinishExam

    End Function

#Region "ICD9 - CPT"

    Public Sub GetDiagnosis()

        Dim blnSaveClicked As Boolean

        If m_ExamId > 0 Then

            If gblnICD9Driven Then
                dia_activeWindow = gloWord.WordDialogBoxBackgroundCloser.GetForegroundWindow()
                Dim frm As New frm_Diagnosis(m_VisitId, m_ExamId, m_PatientId, , , dia_activeWindow)
                frm.BringToFront()
                frm.StartPosition = FormStartPosition.CenterParent
                frm.ShowInTaskbar = False
                If frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent)) = Windows.Forms.DialogResult.Yes Then

                    blnSaveClicked = True
                    frmPatientExam.blnChangesMade = True
                    'Bug #47406: 00000415 : Word Modules
                    'Following condition added
                    If frmPatientExam._PrimaryDiaForICD9Driven <> "" Then
                        strExamName = frmPatientExam._PrimaryDiaForICD9Driven
                    End If
                End If
                frm.Dispose()
                frm = Nothing
                SetICD9CPTGridStyle()
                FillICDCPTMOD()
            Else
                Dim oTreatment As New frm_Treatment(m_ExamId, m_VisitId, dtDos, strExamName, m_PatientId)
                oTreatment.ShowDialog(IIf(IsNothing(oTreatment.Parent), Me, oTreatment.Parent))

                '19-May-15 Aniket: Diagnosis liquid link not refreshing in CPT Driven mode
                frmPatientExam.blnChangesMade = True
                strExamName = oTreatment.PrimaryDiagnosis
                oTreatment.Dispose()

                FillCPTICDMOD()
                'Added By Rahul patel on 30-11-2010
                'For Resolving Case no : GLO2010-0007091 i.e for Problem list Not get Populated 
                FillICD9Code(ArrListTreament)


            End If
            'Added by Rahul Patel on 25-11-2010
            'For Resolving Case no : GLO2010-0007091 i.e for Problem list Not get Populated 

            '03-Jan-13 Aniket: Fixing Bug #61786
            If blnSaveClicked = True Then
                FillProblemList()
            End If

            'End of Code Added by rahul patel on 25-11-2010
            ShowRecommendationsAlert()

        End If
    End Sub

    'Added by Rahul patel on 30-11-2010
    'For Resolving Case no : GLO2010-0007091 i.e for Problem list Not get Populated 
    Public Sub FillICD9Code(ByVal arrListICD9 As ArrayList)
        Dim oList As gloEMRGeneralLibrary.gloGeneral.myList = Nothing
        '  Dim oItem As gloGeneralItem.gloItem = Nothing
        Try
            If arrListICD9 IsNot Nothing Then
                If arrListICD9.Count > 0 Then
                    'C1Treatment.Rows.Count = 1
                    'Dim _FoundRow As Integer = -1
                    ArrlistDiagonsis = New ArrayList
                    For i As Integer = 0 To arrListICD9.Count - 1
                        oList = CType(arrListICD9(i), gloEMRGeneralLibrary.gloGeneral.myList)

                        If arrListICD9.Count > oList.ICD9No Then
                            If oList.Code <> "" Then
                                '' ADD ICD9
                                'oItem = New gloGeneralItem.gloItem
                                'oItem.Code = oList.Code
                                'oItem.Description = oList.Description

                                If oList.Code <> "" AndAlso oList.Description <> "" Then
                                    ArrlistDiagonsis.Add(New mytable(oList.Description, oList.Code))
                                End If

                            End If

                        End If
                        'oItem = Nothing 'Change made to solve memory Leak and word crash issue
                    Next
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            If Not oList Is Nothing Then oList = Nothing
            'If Not oItem Is Nothing Then oItem = Nothing
        End Try
    End Sub

    'Added by Rahul patel on 25-11-2010
    'For Resolving Case no : GLO2010-0007091 i.e for Problem list Not get Populated 
    Public Sub FillProblemList()
        Dim oclsDiagnosis As New ClsDiagnosisDBLayer
        If _IsDXCPT = True Then
            oclsDiagnosis.SaveDxCPTProblem(m_PatientId, m_VisitId, dtDos, ArrlistDiagonsis, m_ExamId)
            _IsDXCPT = False
        Else
            Dim _strDiagnosis As String = ""
            Dim _strCode As String = ""
            Dim _strDescription As String = ""
            Dim _strSnomedCode As String = ""
            Dim _strSnomedDesc As String = ""
            Dim _nICDRevision As Int16
            Dim blnIsOneToOne As Boolean

            If IsNothing(ArrlistDiagonsis) = False Then
                If ArrlistDiagonsis.Count > 0 Then
                    For i As Integer = 0 To ArrlistDiagonsis.Count - 1
                        _strDiagnosis = CType(ArrlistDiagonsis.Item(i), mytable).Code.ToString & " " & CType(ArrlistDiagonsis.Item(i), mytable).Description.ToString
                        _strCode = CType(ArrlistDiagonsis.Item(i), mytable).Code.ToString
                        _strDescription = CType(ArrlistDiagonsis.Item(i), mytable).Description.ToString
                        _strSnomedCode = CType(ArrlistDiagonsis.Item(i), mytable).SnoCode.ToString
                        _strSnomedDesc = CType(ArrlistDiagonsis.Item(i), mytable).snomeddescription.ToString
                        _nICDRevision = Convert.ToInt16(CType(ArrlistDiagonsis.Item(i), mytable).nICDRevision)
                        blnIsOneToOne = (CType(ArrlistDiagonsis.Item(i), mytable).blnIsSnoMedOneToOneMapping)

                        'Line commented and modifid by dipak 20100826 for case UC5070.003
                        'oclsDiagnosis.FillProblemList(m_PatientID, gnVisitID, dtDOS, _strCode, _strDescription)
                        oclsDiagnosis.FillProblemList(m_PatientId, m_VisitId, dtDos, _strCode, _strDescription, _strSnomedCode, _strSnomedDesc, _nICDRevision, blnIsOneToOne, m_ExamId)
                        'end modification
                    Next
                End If
            End If
        End If
        oclsDiagnosis = Nothing 'Change made to solve memory Leak and word crash issue
    End Sub
    'End of Code Added by rahul patel 25-11-2010
    Public Sub SetICD9CPTGridStyle()
        Try
            gloC1FlexStyle.Style(C1Diagnosis)
            With C1Diagnosis
                .Rows.Count = 1
                Dim _TotalWidth As Single = .Width
                .Cols.Fixed = 0
                .Rows.Fixed = 1
                .Cols.Count = Col_Count
                .AllowResizing = False

                'for ICD9
                .Cols(Col_ICD9Code_Description).Width = 800
                .SetData(0, Col_ICD9Code_Description, "ICD9")
                .Cols(Col_ICD9Code_Description).AllowEditing = False
                .Cols(Col_ICD9Code_Description).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Code).Width = 0
                .SetData(0, Col_ICD9Code, "ICD9CODE")
                .Cols(Col_ICD9Code).AllowEditing = True
                .Cols(Col_ICD9Code).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Desc).Width = 0
                .SetData(0, Col_ICD9Desc, "ICD9Description")
                .Cols(Col_ICD9Desc).AllowEditing = True
                .Cols(Col_ICD9Desc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(COl_CPTCode).Width = 0
                .SetData(0, COl_CPTCode, "CPTCODE")
                .Cols(COl_CPTCode).AllowEditing = True
                .Cols(COl_CPTCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_CPTDesc).Width = 0
                .SetData(0, Col_CPTDesc, "CPTDescription")
                .Cols(Col_CPTDesc).AllowEditing = True
                .Cols(Col_CPTDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModCode).Width = 0
                .SetData(0, Col_ModCode, "MODCODE")
                .Cols(Col_ModCode).AllowEditing = True
                .Cols(Col_ModCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModDesc).Width = 0
                .SetData(0, Col_ModDesc, "MODDescription")
                .Cols(Col_ModDesc).AllowEditing = True
                .Cols(Col_ModDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


                .Cols(Col_Units).Width = 100
                .SetData(0, Col_Units, "Units")
                .Cols(Col_Units).DataType = GetType(System.Decimal)
                .Cols(Col_Units).Format = "###.####"
                .Cols(Col_Units).AllowEditing = False
                .Cols(Col_Units).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Count).Width = 0
                .SetData(0, Col_ICD9Count, "ICD9 Count")
                .Cols(Col_ICD9Count).DataType = GetType(System.Int64)
                .Cols(Col_ICD9Count).AllowEditing = True
                .Cols(Col_ICD9Count).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_CPTCount).Width = 0
                .SetData(0, Col_CPTCount, "CPT Count")
                .Cols(Col_CPTCount).DataType = GetType(System.Int64)
                .Cols(Col_CPTCount).AllowEditing = True
                .Cols(Col_CPTCount).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModCount).Width = 0
                .SetData(0, Col_ModCount, "Mod Count")
                .Cols(Col_ModCount).DataType = GetType(System.Int64)
                .Cols(Col_ModCount).AllowEditing = True
                .Cols(Col_ModCount).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                'Bug #96645: 00001101 : Providers workflow: Creates an exam > adds DX codes (smart dx) > completes notes > On this screen marks primary code. Clicks smart DX and it defaults to ICD9 he clicks ICD10 tries to save codes and they get the mismatch error
                .Cols(Col_ICDRevision).Width = 0
                .SetData(0, Col_ICDRevision, "ICD Revision")
                .Cols(Col_ICDRevision).DataType = GetType(System.Int32)
                .Cols(Col_ICDRevision).AllowEditing = True
                .Cols(Col_ICDRevision).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub FillICDCPTMOD()
        Try
            Dim _Row As Integer
            'set properties of treeview in flexgrid
            With C1Diagnosis
                .Tree.Column = Col_ICD9Code_Description
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15
            End With
            Dim dtICD9 As DataTable = Nothing

            Dim nICD9 As Int16
            Dim nCPT As Int16
            Dim nMOD As Int16
            Dim objDiagnosisDBLayer As New ClsDiagnosisDBLayer()
            'objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
            ' flag = 0 - ICD9   flag = 1 - CPT flag = 2 -MOD
            dtICD9 = objDiagnosisDBLayer.FetchICD9CPTMod(m_ExamId, m_VisitId, "", "", "", 0)
            If (IsNothing(objDiagnosisDBLayer) = False) Then
                objDiagnosisDBLayer.Dispose()
                objDiagnosisDBLayer = Nothing
            End If
            If Not IsNothing(dtICD9) Then
                Dim dvICD9 As DataView = Nothing
                dvICD9 = New DataView(dtICD9.Copy())

                Dim strICD9(dtICD9.Columns.Count - 1) As String

                For i As Integer = 0 To dtICD9.Columns.Count - 1
                    strICD9.SetValue(dtICD9.Columns(i).ColumnName, i)
                Next
                'Change made to solve memory Leak and word crash issue
                If Not IsNothing(dtICD9) Then
                    dtICD9.Dispose()
                    dtICD9 = Nothing
                End If
                dtICD9 = dvICD9.ToTable(True, strICD9)
                ''''Pramod 04232009 End

                With dtICD9
                    If IsNothing(dtICD9) = False Then
                        For nICD9 = 0 To .Rows.Count - 1
                            If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                                C1Diagnosis.Rows.Add()
                                _Row = C1Diagnosis.Rows.Count - 1
                                'set the properties for newly added row
                                With C1Diagnosis.Rows(_Row)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 0
                                    .Node.Data = dtICD9.Rows(nICD9)("sICD9Code") & "-" & dtICD9.Rows(nICD9)("sICD9Description")

                                    If Convert.ToString(dtICD9.Rows(nICD9)("nICDRevision")) = "10" Then
                                        .Node.Image = Global.gloEMR.My.Resources.Resources.ICD10GalleryGreen1
                                    Else
                                        .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
                                    End If

                                End With
                                'nextICD = _Row 'Change made to solve memory Leak and word crash issue
                                With C1Diagnosis
                                    .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                    .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                    .SetData(_Row, Col_ICDRevision, dtICD9.Rows(nICD9)("nICDRevision"))
                                    .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                End With
                                Dim strCurrentICD9 As String = dtICD9.Rows(nICD9)("sICD9Code")


                                objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
                                Dim dtCPT As DataTable = Nothing
                                dtCPT = objDiagnosisDBLayer.FetchICD9CPTMod(m_ExamId, m_VisitId, strCurrentICD9, "", "", 1)
                                If (IsNothing(objDiagnosisDBLayer) = False) Then
                                    objDiagnosisDBLayer.Dispose()
                                    objDiagnosisDBLayer = Nothing
                                End If
                                If Not IsNothing(dtCPT) Then
                                    Dim dvCPT As DataView = Nothing
                                    dvCPT = New DataView(dtCPT.Copy())

                                    Dim strCPT(dtCPT.Columns.Count - 1) As String
                                    For i As Integer = 0 To dtCPT.Columns.Count - 1
                                        strCPT.SetValue(dtCPT.Columns(i).ColumnName, i)
                                    Next
                                    'Change made to solve memory Leak and word crash issue
                                    If Not IsNothing(dtCPT) Then
                                        dtCPT.Dispose()
                                        dtCPT = Nothing
                                    End If
                                    dtCPT = dvCPT.ToTable(True, strCPT)
                                    With dtCPT
                                        If IsNothing(dtCPT) = False Then
                                            For nCPT = 0 To .Rows.Count - 1
                                                Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPTcode")
                                                If strCurrentCPT.Trim <> "" Then
                                                    C1Diagnosis.Rows.Add()
                                                    _Row = C1Diagnosis.Rows.Count - 1
                                                    'set the properties for newly added row
                                                    With C1Diagnosis.Rows(_Row)
                                                        .AllowEditing = True
                                                        .ImageAndText = True
                                                        .Height = 24
                                                        .IsNode = True
                                                        .Node.Level = 1
                                                        .Node.Data = dtCPT.Rows(nCPT)("sCPTcode") & "-" & dtCPT.Rows(nCPT)("sCPTDescription")
                                                        .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1
                                                    End With

                                                    With C1Diagnosis
                                                        .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                        .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                        .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                        .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                        .SetData(_Row, Col_Units, dtCPT.Rows(nCPT)("nUnit"))
                                                        .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                                        .SetData(_Row, Col_CPTCount, nCPT + 1)
                                                        .SetData(_Row, Col_ICDRevision, dtICD9.Rows(nICD9)("nICDRevision"))
                                                    End With


                                                End If

                                                objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
                                                Dim dtMOD As DataTable = Nothing

                                                dtMOD = objDiagnosisDBLayer.FetchICD9CPTMod(m_ExamId, m_VisitId, strCurrentICD9, strCurrentCPT, "", 2)
                                                If (IsNothing(objDiagnosisDBLayer) = False) Then
                                                    objDiagnosisDBLayer.Dispose()
                                                    objDiagnosisDBLayer = Nothing
                                                End If

                                                With dtMOD
                                                    If IsNothing(dtMOD) = False Then
                                                        For nMOD = 0 To .Rows.Count - 1

                                                            Dim strCurrentMod As String = dtMOD.Rows(nMOD)("sModCode")

                                                            If strCurrentMod.Trim <> "" Then
                                                                C1Diagnosis.Rows.Add()
                                                                _Row = C1Diagnosis.Rows.Count - 1
                                                                'set the properties for newly added row
                                                                With C1Diagnosis.Rows(_Row)
                                                                    .AllowEditing = False
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 2
                                                                    .Node.Data = dtMOD.Rows(nMOD)("sModCode") & "-" & dtMOD.Rows(nMOD)("sModDescription")
                                                                    .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                                                End With

                                                                With C1Diagnosis
                                                                    .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                                    .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                                    .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                                    .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                                    .SetData(_Row, Col_ModCode, dtMOD.Rows(nMOD)("sModCode"))
                                                                    .SetData(_Row, Col_ModDesc, dtMOD.Rows(nMOD)("sModDescription"))
                                                                    .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                                                    .SetData(_Row, Col_CPTCount, nCPT + 1)
                                                                    .SetData(_Row, Col_ModCount, nMOD + 1)
                                                                    .SetData(_Row, Col_ICDRevision, dtICD9.Rows(nICD9)("nICDRevision"))
                                                                End With
                                                            End If
                                                        Next
                                                    End If
                                                End With '' With dtMOD
                                                If Not dtMOD Is Nothing Then
                                                    dtMOD.Dispose()
                                                    dtMOD = Nothing
                                                End If
                                            Next '' For nCPT = 0 To .Rows.Count - 1
                                        End If

                                    End With '' With dtCPT
                                    If Not dtCPT Is Nothing Then
                                        dtCPT.Dispose()
                                        dtCPT = Nothing
                                    End If
                                    If Not dvCPT Is Nothing Then
                                        dvCPT.Dispose()
                                        dvCPT = Nothing
                                    End If

                                End If
                            End If  '' If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                        Next ''For nICD9 = 0 To .Rows.Count - 1

                    End If  '' If IsNothing(dtICD9) = False Then
                    If Not dvICD9 Is Nothing Then
                        dvICD9.Dispose()
                        dvICD9 = Nothing
                    End If
                End With '' With dtICD9
                If Not dtICD9 Is Nothing Then
                    dtICD9.Dispose()
                    dtICD9 = Nothing
                End If

            End If
            'Change made to solve memory Leak and word crash issue

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Summary Of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub FillCPTICDMOD()
        Try
            Dim oDiagnosis As New ClsDiagnosisDBLayer()
            Dim arrTreatment As New ArrayList
            arrTreatment = oDiagnosis.GetCPTDrivenDiagnosis(m_ExamId, m_VisitId, m_PatientId)

            If pnlICD9CPT.Controls.Contains(gloUCTreatment) = False Then
                pnlICD9CPT.Controls.Add(gloUCTreatment)

                gloUCTreatment.DOS = dtDos
                gloUCTreatment.Dock = DockStyle.Fill
                gloUCTreatment.BringToFront()
                C1Diagnosis.Visible = False
            End If

            gloUCTreatment.DisableGrid = False
            gloUCTreatment.FillTreatment(arrTreatment)

            '    End If
            'End If
            'Change made to solve memory Leak and word crash issue
            oDiagnosis.Dispose()
            oDiagnosis = Nothing
            arrTreatment = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnDiagnosis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiagnosis.Click, ToolStripButton18.Click
        _IsDXCPT = True
        GetDiagnosis()
    End Sub

#End Region

#Region "Follow-up Tasks"

    Private Function GetFollowUpUsers(Optional ByVal sUserName As String = "") As gloGeneralItem.gloItems
        Try
            Dim oItems As New gloGeneralItem.gloItems
            Dim oItem As gloGeneralItem.gloItem
            Dim dtUsers As DataTable = Nothing
            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
            Dim _Query As String

            If sUserName = "" Then
                _Query = "SELECT sSettingsValue FROM Settings WHERE sSettingsName = 'Followup User' "
            Else
                _Query = "SELECT nUserID FROM User_MST WHERE sLoginName = '" & sUserName.Trim.Replace("'", "''") & "'"
            End If

            oDB.Connect(False)
            oDB.Retrive_Query(_Query, dtUsers)
            oDB.Disconnect()
            If dtUsers IsNot Nothing Then
                For iRow As Integer = 0 To dtUsers.Rows.Count - 1
                    oItem = New gloGeneralItem.gloItem
                    If sUserName <> "" Then
                        oItem.ID = Convert.ToInt64(dtUsers.Rows(iRow)("nUserID"))
                        oItem.Description = sUserName
                    Else
                        oItem.ID = Convert.ToInt64(dtUsers.Rows(iRow)("sSettingsValue"))
                        oItem.Description = GetUserName(oItem.ID)
                    End If
                    oItems.Add(oItem)
                    oItem.Dispose()
                    oItem = Nothing 'Change made to solve memory Leak and word crash issue
                Next
            End If
            'Change made to solve memory Leak and word crash issue
            _Query = Nothing
            If Not dtUsers Is Nothing Then
                dtUsers.Dispose()
                dtUsers = Nothing
            End If
            If Not oDB Is Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If

            Return oItems

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        End Try
    End Function

    Private Function GetUserName(ByVal nUserID As String) As String

        Dim oDB As New DataBaseLayer
        Dim _UserName As String

        Try
            Dim strQuery As String = "select sLoginName from User_MST WHERE nUserID = " & nUserID & ""
            _UserName = oDB.GetRecord_Query(strQuery)

            If Not IsDBNull(_UserName) AndAlso _UserName IsNot Nothing Then
                Return _UserName
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally
            oDB.Dispose()   'Change made to solve memory Leak and word crash issue
            oDB = Nothing
        End Try

    End Function
    Private Sub AddTasks()
        Try

            Dim ogloTask As New gloTaskMail.gloTask(GetConnectionString)
            Dim oTask As Task = Nothing
            Dim oTaskProgress As New gloTaskMail.TaskProgress
            Dim oTaskAssign As TaskAssign = Nothing
            Dim oItems As gloGeneralItem.gloItems = Nothing

            'Dim _arrUsers As Array
            Dim _User As String
            Dim _Subject As String
            Dim _DueDate As DateTime
            Dim _Priority As String
            Dim _IsShowMessage As Boolean = True ''Variable Added by Mayuri:20100102-To fix issue:#5566-Showing twice warning popup at time finishing any exam
            For i As Integer = C1Followup.Rows.Count - 1 To 1 Step -1

                If C1Followup.GetCellCheck(i, Col_Select) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    ''Sanjog-2011 jan 12 to generate task 
                    oTask = New Task
                    'oItems = New gloGeneralItem.gloItems
                    ''Sanjog-2010 jan 12 to generate task 
                    _Subject = C1Followup.GetData(i, Col_Task)
                    _User = C1Followup.GetData(i, Col_User)
                    _DueDate = C1Followup.GetData(i, Col_DueDate)
                    _DueDate = Format(_DueDate, "MM/dd/yyyy") & " " & Format(_DueDate, "Short Time")
                    _Priority = C1Followup.GetData(i, Col_Priority)
                    Dim ObjTasksDBLayer As ClsTasksDBLayer
                    ObjTasksDBLayer = New ClsTasksDBLayer
                    LoginId = ObjTasksDBLayer.GetLoginId
                    ObjTasksDBLayer.Dispose()
                    ObjTasksDBLayer = Nothing
                    'Code Changes Because Datamap returns of Key Value which is UserID 
                    If _User <> "0" AndAlso _User <> "Default" Then
                        _User = GetUserName(_User)
                    End If


                    If _User = "Default" OrElse _User = "0" Then
                        oItems = GetFollowUpUsers()
                    Else
                        oItems = GetFollowUpUsers(_User)
                    End If
                    If oItems Is Nothing Then
                        ''Condition Added by Mayuri:20100102-To fix issue:#5566-Showing twice warning popup at time finishing any exam
                        If (_IsShowMessage = True) Then
                            MessageBox.Show("No Followup Users have been associated, please configure using gloEMR Admin", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            _IsShowMessage = False
                        End If
                    ElseIf oItems.Count = 0 Then
                        ''Condition Added by Mayuri:20100102-To fix issue:#5566-Showing twice warning popup at time finishing any exam
                        If (_IsShowMessage = True) Then
                            MessageBox.Show("No Followup Users have been associated, please configure using gloEMR Admin", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            _IsShowMessage = False
                        End If
                    Else
                        '' ADDING TASKS ''
                        For iUser As Integer = 0 To oItems.Count - 1
                            ''Sanjog-2011 jan 12 to generate task 
                            oTaskAssign = New TaskAssign
                            ''Sanjog-2011 jan 12 to generate task 
                            oTaskAssign.AssignFromID = LoginId
                            oTaskAssign.AssignFromName = gstrLoginName
                            oTaskAssign.AssignToID = oItems(iUser).ID
                            If oTaskAssign.AssignFromID = oTaskAssign.AssignToID Then
                                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self
                                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept
                            Else
                                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned
                                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold
                            End If
                            oTaskAssign.AssignToName = oItems(iUser).Description
                            oTaskAssign.ClinicID = gnClinicID
                            oTask.Assignment.Add(oTaskAssign)
                            ''Sanjog-2010 jan 12 to generate task 
                            oTaskAssign = Nothing
                            ''Sanjog-2010 jan 12 to generate task 
                        Next

                        oTaskProgress.ClinicID = gnClinicID
                        oTaskProgress.Complete = 0
                        oTaskProgress.DateTime = _VisitDate
                        oTaskProgress.Description = _Subject
                        oTaskProgress.StatusID = 1 '' Not Started
                        oTaskProgress.TaskID = 0

                        '' 
                        oTask.UserID = gnLoginID
                        oTask.TaskType = TaskType.Exam
                        oTask.PatientID = m_PatientId
                        oTask.Subject = "Exam : " & _Subject
                        oTask.ReferenceID1 = m_ExamId
                        oTask.ClinicID = gnClinicID
                        oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(_VisitDate)
                        oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(_VisitDate)
                        oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(_DueDate)
                        oTask.IsPrivate = False
                        oTask.MachineName = gstrClientMachineName
                        oTask.Progress = oTaskProgress
                        '' oTask.PriorityID = 1
                        ''dhruv 20100113 
                        '' to check the Priority 
                        If (_Priority = "Low") Then
                            oTask.PriorityID = 1
                        ElseIf (_Priority = "Medium") Then
                            oTask.PriorityID = 2
                        ElseIf (_Priority = "High") Then
                            oTask.PriorityID = 3
                        End If
                        'START line of code added for Problem:00000121
                        ''added if condition for bugid 98692
                        If (oItems.Count > 1) Then
                            oTask.TaskGroupID = ogloTask.GetUniqueueId()
                        Else
                            oTask.TaskGroupID = 0
                        End If
                        'END line of code added for Problem:00000121
                        ogloTask.Add(oTask)
                    End If
                End If
                ''Sanjog-2010 jan 12 to generate task 
                If Not oItems Is Nothing Then
                    oItems.Dispose()
                    oItems = Nothing
                End If
                oTask = Nothing
                ''Sanjog-2010 jan 12 to generate task 
            Next

            'Change made to solve memory Leak and word crash issue
            _User = Nothing
            'If Not oTask Is Nothing Then
            '    oTask.Dispose()
            '    oTask = Nothing
            'End If
            oTaskProgress.Dispose()
            oTaskProgress = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try


    End Sub

    Private Sub SetFollowupGridStyle()
        gloC1FlexStyle.Style(C1Followup)
        With C1Followup
            Dim _TotalWidth As Single = .Width
            .Rows.Fixed = 1
            .Cols.Count = Col_FollowupCount

            'for ICD9
            .SetData(0, Col_ID, "ID")
            .Cols(Col_ID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_ID).Width = _TotalWidth * 0
            .Cols(Col_Select).AllowEditing = False


            .SetData(0, Col_Select, "Select")
            .Cols(Col_Select).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
            .Cols(Col_Select).Width = _TotalWidth * 0.1
            .Cols(Col_Select).AllowEditing = True


            .SetData(0, Col_Task, "Task Description")
            .Cols(Col_Task).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_Task).Width = _TotalWidth * 0.43
            .Cols(Col_Task).AllowEditing = False


            .SetData(0, Col_User, "Follow-up User")
            .Cols(Col_User).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_User).Width = _TotalWidth * 0.15
            .Cols(Col_User).AllowEditing = True


            .SetData(0, Col_Priority, "Priority")
            .Cols(Col_Priority).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_Priority).Width = _TotalWidth * 0.15
            .Cols(Col_Priority).AllowEditing = True

            .SetData(0, Col_DueDate, "Due date")
            .Cols(Col_DueDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_DueDate).DataType = Type.GetType("System.DateTime")
            .Cols(Col_DueDate).Format = "MM/dd/yyyy"
            .Cols(Col_DueDate).Width = _TotalWidth * 0.15
            .Cols(Col_DueDate).AllowEditing = True


        End With

    End Sub


    Private Sub FillFollowupGrid()
        Try
            Dim NonGloCollectUsers As ListDictionary = New ListDictionary

            'Dim cellStyle As CellStyle = C1Followup.Styles.Add("cellStyle")
            Dim cellStyle As CellStyle

            Try
                If (C1Followup.Styles.Contains("cellStyle")) Then
                    cellStyle = C1Followup.Styles("cellStyle")
                Else
                    cellStyle = C1Followup.Styles.Add("cellStyle")
                End If
            Catch ex As Exception
                cellStyle = C1Followup.Styles.Add("cellStyle")
            End Try

            Dim dtGlocollect, dtNonGloCollect As DataTable
            Dim dtTasks As DataTable = GetTasks()
            If Not IsNothing(dtTasks) Then
                If dtTasks.Rows.Count > 0 Then
                    With C1Followup
                        Dim blnFlag As Boolean = False
                        For i As Int32 = 0 To dtTasks.Rows.Count - 1
                            .Rows.Add()
                            blnFlag = True
                            .SetData(i + 1, Col_ID, dtTasks.Rows(i)("nCategoryID"))
                            .SetCellCheck(i + 1, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                            .SetData(i + 1, Col_Task, dtTasks.Rows(i)("sDescription"))

                        Next
                        If blnFlag Then
                            Dim strPriority As String = "Low|Normal|High"
                            'Dim strUsers As String = "Default"

                            dtUser = BindUsers()

                            Dim dr As DataRow = dtUser.NewRow()
                            dr(0) = 0
                            dr(1) = "Default"
                            dr(2) = 0

                            dtUser.Rows.InsertAt(dr, 0)
                            dtUser.DefaultView.RowFilter = "bIsGloCollect = 0"
                            dtNonGloCollect = dtUser.DefaultView.ToTable()
                            If IsNothing(dtNonGloCollect) = False Then
                                For i As Int32 = 0 To dtNonGloCollect.Rows.Count - 1
                                    'strUsers = strUsers & "|" & dtNonGloCollect.Rows(i)(1)
                                    NonGloCollectUsers.Add(dtNonGloCollect.Rows(i)(0), dtNonGloCollect.Rows(i)(1))
                                Next
                            End If

                            dtUser.DefaultView.RowFilter = ""
                            dtUser.DefaultView.RowFilter = "bIsGloCollect = 1"
                            dtGlocollect = dtUser.DefaultView.ToTable()
                            If dtGlocollect.Rows.Count > 0 Then
                                'strUsers = strUsers & "|" & "More Users...."
                                NonGloCollectUsers.Add(dtNonGloCollect.Rows.Count + 1, "More Users....")
                            End If


                            cellStyle.DataMap = NonGloCollectUsers
                            C1Followup.Cols(Col_User).Style = cellStyle


                            dtUser.DefaultView.RowFilter = ""

                            'If Not IsNothing(dtUser) Then
                            '    dtUser.Dispose()
                            '    dtUser = Nothing
                            'End If
                            If Not IsNothing(dtNonGloCollect) Then
                                dtNonGloCollect.Dispose()
                                dtNonGloCollect = Nothing
                            End If
                            If Not IsNothing(dtGlocollect) Then
                                dtGlocollect.Dispose()
                                dtGlocollect = Nothing
                            End If
                            ' .Cols(Col_User).ComboList = strUsers
                            .Cols(Col_Priority).ComboList = strPriority
                            For i As Int32 = 1 To C1Followup.Rows.Count - 1
                                .SetData(i, Col_User, "Default")
                                .SetData(i, Col_Priority, "Low")
                                .SetData(i, Col_DueDate, DateTime.Now)
                            Next
                        End If
                        blnFlag = Nothing   'Change made to solve memory Leak and word crash issue
                    End With
                Else
                    blnFollowUpempty = True
                End If
            Else
                blnFollowUpempty = True
            End If
            'Change made to solve memory Leak and word crash issue
            If Not dtTasks Is Nothing Then
                dtTasks.Dispose()
                dtTasks = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Summary Of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Function GetTasks() As DataTable
        Dim oDB As New DataBaseLayer
        Dim oResultTasks As DataTable
        Try
            Dim strQuery As String = "select nCategoryId, sDescription from Category_MST WHERE sCategoryType='Followup'"
            oResultTasks = oDB.GetDataTable_Query(strQuery)
            If Not IsNothing(oResultTasks) Then
                If oResultTasks.Rows.Count > 0 Then
                    Return oResultTasks
                Else
                    MessageBox.Show("No Followup Categories have been associated, please create Followup tasks", "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally
            oDB.Dispose()   'Change made to solve memory Leak and word crash issue
            oDB = Nothing
        End Try
    End Function

    Private Function BindUsers() As DataTable
        Dim dt As DataTable
        dt = GetShortListUser()
        If Not IsNothing(dt) Then
            Return dt
        Else
            Return Nothing
        End If
    End Function

    Private Sub btnAddCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCategory.Click, ToolStripButton20.Click
        Dim frm As New CategoryMaster("Followup")
        Try
            frm.Text = "Add Category"
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            GetLatestFollowupTask()
            'Change made to solve memory Leak and word crash issue
            frm.Close()
            frm.Dispose()
            frm = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally

        End Try
    End Sub

    Private Sub GetLatestFollowupTask()
        Dim oDB As New DataBaseLayer
        Dim oResultTasks As DataTable = Nothing
        Dim blnNull As Boolean = False
        Try
            Dim strQuery As String = "select top 1 nCategoryId, sDescription from Category_MST WHERE sCategoryType='Followup' order by nCategoryID desc"
            oResultTasks = oDB.GetDataTable_Query(strQuery)
            If Not IsNothing(oResultTasks) Then
                If oResultTasks.Rows.Count = 1 Then
                    With C1Followup
                        For i As Integer = .Rows.Count - 1 To 1 Step -1
                            If IsDBNull(oResultTasks.Rows(0)(0)) = False AndAlso IsDBNull(oResultTasks.Rows(0)(1)) = False Then
                                If oResultTasks.Rows(0)(0) = .GetData(i, Col_ID) AndAlso oResultTasks.Rows(0)(1) = .GetData(i, Col_Task) Then
                                    Exit Sub
                                End If
                            Else
                                blnNull = True
                            End If

                        Next
                        If Not blnNull Then
                            Dim cnt As Int32 = .Rows.Count
                            .Rows.Add()
                            .SetData(cnt, Col_ID, oResultTasks.Rows(0)(0))
                            .SetCellCheck(cnt, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                            If blnFollowUpempty = True Then
                                Dim strPriority As String = "Low|Normal|High"
                                Dim strUsers As String = "Default"
                                Dim dtUser As DataTable
                                dtUser = BindUsers()
                                If IsNothing(dtUser) = False Then
                                    For i As Int32 = 0 To dtUser.Rows.Count - 1
                                        strUsers = strUsers & "|" & dtUser.Rows(i)(1)
                                    Next
                                End If
                                If Not IsNothing(dtUser) Then
                                    dtUser.Dispose()
                                    dtUser = Nothing
                                End If
                                .Cols(Col_User).ComboList = strUsers
                                .Cols(Col_Priority).ComboList = strPriority
                                blnFollowUpempty = False
                            End If
                            .SetData(cnt, Col_Task, oResultTasks.Rows(0)(1))
                            .SetData(cnt, Col_User, "Default")
                            .SetData(cnt, Col_Priority, "Low")
                            .SetData(cnt, Col_DueDate, DateTime.Now)

                        End If

                    End With

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not oDB Is Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not oResultTasks Is Nothing Then
                oResultTasks.Dispose()
                oResultTasks = Nothing
            End If
        End Try
    End Sub

    Private Function GetShortListUser() As DataTable
        Dim oDB As New DataBaseLayer


        Dim oResultTable As DataTable  ''Slr new not needed 
        Try
            Dim strQuery As String = "SELECT  User_MST.nUserId ,User_MST.sLoginName ,ISNULL(bIsGloCollect, 0) AS bIsGloCollect FROM User_MST  WHERE   ISNULL(nBlockStatus,0) = 0 ORDER BY ISNULL(bIsGloCollect, 0), User_MST.sLoginName"
            oResultTable = oDB.GetDataTable_Query(strQuery)

            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

#End Region

#Region "Print HCFA Report"

    Private Sub PrintHIPPAReport()
        Dim oRpt As ReportDocument = New ReportDocument()
        Try

            Me.Cursor = Cursors.WaitCursor
            FillReportdetails()
            'oRpt.PrintToPrinter(1, False, 0, 0)

            If gblnUseDefaultPrinter = False Then
                'sarika Show Print Dialog 20080923
                '                        oRpt.PrintToPrinter(1, False, 0, 0)
                'PrintDialog1.UseEXDialog = True
                '  PrintDialog1 = New PrintDialog()
                '            PrintDialog1.ShowDialog()
                'If PrintDialog1.Then Then
                'If Not IsNothing(oRpt) Then
                '    oRpt.Close()
                'End If
                If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then

                    oRpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                    '     ApplyRptParameters(oRpt, sCheckstate, sPrescriptionDate, blnIsFax, dtPrescriptiondate, strPrescriptiondate)
                    oRpt.Load(Application.StartupPath & "\Reports\rptHIPPA.rpt")


                    oRpt.PrintToPrinter(1, False, 0, 0)


                End If
                ' PrintDialog1.Dispose()
                'PrintDialog1 = Nothing
            Else
                oRpt.PrintToPrinter(1, False, 0, 0)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to load Report", "summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            Me.Cursor = Cursors.Default
            If Not IsNothing(oRpt) Then
                oRpt.Close()
                oRpt.Dispose()
                oRpt = Nothing
            End If

        End Try

    End Sub
    Private Sub FillReportdetails()
        'Dim nCount As Int16
        Dim arrlist As New ArrayList
        Dim oRpt As ReportDocument = New ReportDocument()
        Try
            arrlist.Clear()

            arrlist.Add(m_ExamId)

            ' oRpt = New ReportDocument
            oRpt.Load(Application.StartupPath & "\Reports\rptHIPPA.rpt")

            MapDatabaseInfo(oRpt)


            Dim prm As ParameterValues
            Dim discreteval As ParameterDiscreteValue

            prm = oRpt.DataDefinition.ParameterFields.Item(0).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = m_PatientId
            prm.Add(discreteval)
            oRpt.DataDefinition.ParameterFields.Item(0).ApplyCurrentValues(prm)

            prm = oRpt.DataDefinition.ParameterFields.Item(1).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = CType(dtDos, String)
            prm.Add(discreteval)
            oRpt.DataDefinition.ParameterFields.Item(1).ApplyCurrentValues(prm)

            prm = oRpt.DataDefinition.ParameterFields.Item(2).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = CType(dtDos, String)
            prm.Add(discreteval)
            oRpt.DataDefinition.ParameterFields.Item(2).ApplyCurrentValues(prm)

            '{sp_HCFAReport;1.ExamID}
            If arrlist.Count > 0 Then
                Dim selectFormula As String = ""
                Dim i As Int16
                For i = 0 To arrlist.Count - 1
                    selectFormula = selectFormula & "{sp_RptHIPAA;1.ExamID} = " & CType(arrlist.Item(i), Int64) & " or "
                    If i = arrlist.Count - 1 Then
                        oRpt.DataDefinition.GroupSelectionFormula = Mid(selectFormula, 1, Len(selectFormula) - 4)
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            arrlist = Nothing   'Change made to solve memory Leak and word crash issue
            If Not IsNothing(oRpt) Then
                oRpt.Close()
                oRpt.Dispose()
                oRpt = Nothing
            End If

        End Try
    End Sub

    Private Sub MapDatabaseInfo(ByVal rpt As ReportDocument)

        Dim crConnectionInfo As New ConnectionInfo
        Try
            With crConnectionInfo
                .ServerName = gstrSQLServerName

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                .DatabaseName = gstrDatabaseName

                .IntegratedSecurity = True

                '.UserID = "Your User ID"
                '.Password = "Your Password"
            End With
            MapTableInfo(crConnectionInfo, rpt)
            Dim objsubrpt As SubreportObject
            Dim objrpt As ReportDocument

            objsubrpt = rpt.ReportDefinition.Sections.Item(3).ReportObjects(0)
            'objrpt = New ReportDocument
            objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
            MapTableInfo(crConnectionInfo, objrpt)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            crConnectionInfo = Nothing  'Change made to solve memory Leak and word crash issue

        End Try
    End Sub

    Private Sub MapTableInfo(ByVal crConnectionInfo As ConnectionInfo, ByVal rpt As ReportDocument)
        Dim crtableLogoninfo As New TableLogOnInfo

        Dim CrTables As Tables
        Dim CrTable As Table
        'Dim TableCounter
        'This code works for both user tables and stored 
        'procedures. Set the CrTables to the Tables collection 
        'of the report 
        Try
            CrTables = rpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            crtableLogoninfo = Nothing  'Change made to solve memory Leak and word crash issue
        End Try
    End Sub

#End Region

#Region "Prescription"

    Private Sub btnPrescription_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrescription.Click, ToolStripButton12.Click
        GetPrescription()
    End Sub

    Public Sub SetRxGridStyle()
        gloC1FlexStyle.Style(C1RxSummary)
        Try
            With C1RxSummary
                .Rows.Fixed = 1
                .Cols.Count = Col_RxSummaryCount

                Dim _TotalWidth As Single = .Width

                .SetData(0, Col_Drug, "Drug")
                .Cols(Col_Drug).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_Drug).Width = _TotalWidth * 0.35
                .Cols(Col_Drug).AllowEditing = False

                .SetData(0, Col_DrugName, "Drug Name")
                .Cols(Col_DrugName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_DrugName).Width = 0 '_TotalWidth * 0.35
                .Cols(Col_DrugName).AllowEditing = False

                .SetData(0, Col_Dosage, "Dosage")
                .Cols(Col_Dosage).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_Dosage).Width = 0 '_TotalWidth * 0.2
                .Cols(Col_Dosage).AllowEditing = False

                .SetData(0, Col_Route, "Route")
                .Cols(Col_Route).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_Route).Width = 0
                .Cols(Col_Route).AllowEditing = False

                .SetData(0, Col_Frequency, "Patient Directions")
                .Cols(Col_Frequency).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_Frequency).Width = _TotalWidth * 0.2
                .Cols(Col_Frequency).AllowEditing = False

                .SetData(0, COl_Duration, "Duration")
                .Cols(COl_Duration).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(COl_Duration).Width = 0
                .Cols(COl_Duration).AllowEditing = False

                .SetData(0, Col_Amount, "Amount")
                .Cols(Col_Amount).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_Amount).Width = 0
                .Cols(Col_Amount).AllowEditing = False

                .SetData(0, Col_Refills, "Refills")
                .Cols(Col_Refills).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_Refills).Width = 0
                .Cols(Col_Refills).AllowEditing = False

                .SetData(0, Col_StartDate, "Start Date")
                .Cols(Col_StartDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_StartDate).Width = _TotalWidth * 0.14
                .Cols(Col_StartDate).AllowEditing = False

                .SetData(0, Col_EndDate, "End Date")
                .Cols(Col_EndDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_EndDate).Width = _TotalWidth * 0.14
                .Cols(Col_EndDate).AllowEditing = False

                .SetData(0, Col_NDCCOde, "NDCCODE")
                .Cols(Col_NDCCOde).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                .Cols(Col_NDCCOde).Width = 0 '_TotalWidth * 0.14
                .Cols(Col_NDCCOde).AllowEditing = False

            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub FillRxSummary()

        Dim oDB As New DataBaseLayer
        Dim oResultRx As DataTable = Nothing
        Try

            Dim strQuery As String = "select isnull(sMedication,'') as sMedication, isnull(sDosage,'') as sDosage,isnull(sRoute,'') as sRoute,isnull(sDrugForm,'')as sDrugForm,isnull(sFrequency,'') as sFrequency, isnull(sDuration,'') as sDuration, isnull(sAmount,'') as sAmount, isnull(sRefills,'') as sRefills, dtStartDate,dtEndDate,isnull(sNDCCode,'') as sNDCCode  from Prescription WHERE nVisitID = " & m_VisitId & "  and nPatientID = " & m_PatientId
            oResultRx = oDB.GetDataTable_Query(strQuery)
            If Not IsNothing(oResultRx) Then
                With C1RxSummary
                    .Rows.Count = 1
                    For i As Int32 = 0 To oResultRx.Rows.Count - 1
                        .Rows.Add()
                        .SetData(i + 1, Col_Drug, oResultRx.Rows(i)("sMedication") & " " & oResultRx.Rows(i)("sDosage") & " " & oResultRx.Rows(i)("sDrugForm") & " " & oResultRx.Rows(i)("sRoute"))
                        .SetData(i + 1, Col_DrugName, oResultRx.Rows(i)("sMedication"))
                        .SetData(i + 1, Col_Dosage, oResultRx.Rows(i)("sDosage"))
                        .SetData(i + 1, Col_Route, oResultRx.Rows(i)("sRoute"))
                        .SetData(i + 1, Col_Frequency, oResultRx.Rows(i)("sFrequency"))
                        .SetData(i + 1, COl_Duration, oResultRx.Rows(i)("sDuration"))
                        .SetData(i + 1, Col_Amount, oResultRx.Rows(i)("sAmount"))
                        .SetData(i + 1, Col_Refills, oResultRx.Rows(i)("sRefills"))
                        .SetData(i + 1, Col_StartDate, oResultRx.Rows(i)("dtStartDate"))
                        .SetData(i + 1, Col_EndDate, oResultRx.Rows(i)("dtEndDate"))
                        .SetData(i + 1, Col_NDCCOde, oResultRx.Rows(i)("sNDCCode"))
                    Next

                End With
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Summary Of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not oDB Is Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not oResultRx Is Nothing Then
                oResultRx.Dispose()
                oResultRx = Nothing
            End If
        End Try
    End Sub

    Public Sub GetPrescription()

        'code added in 6040  6040 to fix bug 8402
        Dim frmRxMeds As frmPrescription
        Try
            frmRxMeds = frmPrescription.GetInstance(m_VisitId, m_PatientId)
            If IsNothing(frmRxMeds) = True Then
                Exit Sub
            End If
            If frmPrescription.IsOpen = False Then
                frmRxMeds.ShowMedication()

                If frmRxMeds.blncancel = True Then
                    With frmRxMeds
                        .WindowState = FormWindowState.Maximized
                        .myCaller = Me.myCaller
                        .ShowInTaskbar = False
                        .ShowDialog(Me.myCaller)
                        .Dispose()
                    End With
                End If
            Else
                MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            SetRxGridStyle()
            FillRxSummary()
            ShowRecommendationsAlert()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'If Not frmPatPre Is Nothing Then
            '    frmPatPre.Close()
            '    frmPatPre.Dispose()
            '    frmPatPre = Nothing
            'End If
        End Try
    End Sub

#Region "Print prescription"

    ''* function added by suraj 20080725
    Private Function RetrieveFAXDocumentName() As String
        'Set FAX Settings
        Dim strTIFFFileName As String = ""
        'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
        strTIFFFileName = gnClientMachineID & "-" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") 'Format(_dtCurrentDateTime, "yyyyMMddhhmmss") & _dtCurrentDateTime.Millisecond
        Return strTIFFFileName
    End Function
    'private sub PrintRxReport modified to Function for return value for printdialog box cancel  click
    Private Function PrintRxReport(ByVal sCheckstate As String, ByVal sPrescriptionDate As String, ByVal blnIsFax As Boolean, ByVal dtPrescriptiondate As DateTime)


        If gblnMultipleSupervisorsforPaperRx Then
            If oProvider.IsProvider_Senior(_RxBusinessLayer.ProviderID) = False Then

                '' IF PROVIDER ASSOCIATION NOT DONE BEFORE PRINT '' 
                '' THEN FETCH COMMON PROVIDERS AND SAVE FOR THIS PRISCRIPTION ''
                Dim dtAssociation As DataTable = RxBusinesslayer.GetRxProviderAssociation(m_PatientId, m_VisitId, dtPrescriptiondate)
                If (IsNothing(dtAssociation) = False) Then
                    If dtAssociation.Rows.Count = 0 Then
                        _RxBusinessLayer.SaveRxProviderAssociation(m_PatientId, m_VisitId, dtPrescriptiondate, m_ProviderId)
                    End If
                    dtAssociation.Dispose()
                    dtAssociation = Nothing
                End If
                

            End If
        End If
        Try
            Using oPrescriptionForm As New frmPrescription(m_PatientId)
                Dim strPrescriptiondate As String = oPrescriptionForm.GetDatetimeString(dtPrescriptiondate)
                If Not IsNothing(strPrescriptiondate) Then
                    If strPrescriptiondate <> "" Then
                        Dim bIsprint As Boolean = (Not blnIsFax)
                        Dim _ShowPrintDialog As Boolean = False

                        If gblnUseDefaultPrinter = False AndAlso _PrinterName = "" Then
                            _ShowPrintDialog = True
                        Else
                            _ShowPrintDialog = False
                        End If
                        If gstrPrintMultipleRx_PerScriptPage = True Then
                            If gblnIsCustomizeReport = False Then
                                oPrescriptionForm.PrintPrescription_SSRS(bIsprint, "rptMultipleRx", m_PatientId, sCheckstate, strPrescriptiondate, "", False, _RxBusinessLayer.PharmacyId, gbIsProviderEPCSEnable, "")
                            Else
                                oPrescriptionForm.PrintPrescription_SSRS(bIsprint, gstrMultipleRxCustomizeReport, m_PatientId, sCheckstate, strPrescriptiondate, "", False, _RxBusinessLayer.PharmacyId, gbIsProviderEPCSEnable, "")
                            End If
                        Else
                            If gblnIsCustomizeReport = False Then
                                oPrescriptionForm.PrintPrescription_SSRS(bIsprint, "rptSingleRx", m_PatientId, sCheckstate, strPrescriptiondate, "", False, _RxBusinessLayer.PharmacyId, gbIsProviderEPCSEnable, "")
                            Else
                                oPrescriptionForm.PrintPrescription_SSRS(bIsprint, gstrSingleRxCustomizeReport, m_PatientId, sCheckstate, strPrescriptiondate, "", False, _RxBusinessLayer.PharmacyId, gbIsProviderEPCSEnable, "")
                            End If
                        End If

                    End If
                End If
            End Using
            'Dim objSettings As New clsSettings
            'objSettings.GetSettings_Rx()
            'Dim oRpt As ReportDocument = Nothing


            '    Dim strPrescriptiondate As String = _RxBusinessLayer.GetPrescriptiondateString(dtPrescriptiondate)
            '    oRpt = New ReportDocument
            '    If Not IsNothing(strPrescriptiondate) Then
            '        If strPrescriptiondate <> "" Then

            '            'give the prescription report name here
            '            If gstrPrintMultipleRx_PerScriptPage = True Then
            '                oRpt.Load(Application.StartupPath & "\Reports\Rpt_KozmaryPrescription.rpt")
            '            Else
            '                oRpt.Load(Application.StartupPath & "\Reports\Rpt_Prescription.rpt")
            '            End If
            '            oRpt.Refresh()

            '            Dim crtableLogoninfo As New TableLogOnInfo
            '            Dim crConnectionInfo As New ConnectionInfo
            '            Dim CrTables As Tables
            '            Dim CrTable As Table

            '            With crConnectionInfo
            '                .ServerName = gstrSQLServerName

            '                'If you are connecting to Oracle there is no 
            '                'DatabaseName. Use an empty string. 
            '                'For example, .DatabaseName = "" 

            '                .DatabaseName = gstrDatabaseName
            '                .IntegratedSecurity = True

            '                '.UserID = "Your User ID"
            '                '.Password = "Your Password"

            '                If gblnSQLAuthentication = True Then 'bug no: 4653
            '                    .IntegratedSecurity = False
            '                    .UserID = gstrSQLUserEMR
            '                    .Password = gstrSQLPasswordEMR
            '                End If
            '            End With

            '            'This code works for both user tables and stored 
            '            'procedures. Set the CrTables to the Tables collection 
            '            'of the report 

            '            CrTables = oRpt.Database.Tables

            '            'Loop through each table in the report and apply the 
            '            'LogonInfo information 

            '            For Each CrTable In CrTables
            '                crtableLogoninfo = CrTable.LogOnInfo
            '                crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '                CrTable.ApplyLogOnInfo(crtableLogoninfo)

            '                'If your DatabaseName is changing at runtime, specify 
            '                'the table location. 
            '                'For example, when you are reporting off of a 
            '                'Northwind database on SQL server you 
            '                'should have the following line of code: 

            '                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            '            Next

            '            Dim prm As ParameterValues
            '            Dim discreteval As ParameterDiscreteValue


            '            ''//patientid
            '            prm = oRpt.DataDefinition.ParameterFields.Item(0).CurrentValues()
            '            prm.Clear()
            '            discreteval = New ParameterDiscreteValue
            '            discreteval.Value = CType(m_PatientId, String)
            '            prm.Add(discreteval)
            '            oRpt.DataDefinition.ParameterFields.Item(0).ApplyCurrentValues(prm)

            '            prm = oRpt.DataDefinition.ParameterFields.Item(1).CurrentValues()
            '            prm.Clear()
            '            discreteval = New ParameterDiscreteValue
            '            ''give the checkstate value here (drugid string)
            '            discreteval.Value = sCheckstate
            '            prm.Add(discreteval)
            '            oRpt.DataDefinition.ParameterFields.Item(1).ApplyCurrentValues(prm)

            '            prm = oRpt.DataDefinition.ParameterFields.Item(2).CurrentValues()
            '            prm.Clear()
            '            discreteval = New ParameterDiscreteValue

            '            ''give the Prescription date
            '            discreteval.Value = strPrescriptiondate
            '            prm.Add(discreteval)
            '            oRpt.DataDefinition.ParameterFields.Item(2).ApplyCurrentValues(prm)

            '            If blnIsFax = False Then

            '                If gblnUseDefaultPrinter = False And _PrinterName = "" Then
            '                    PrintDialog1 = New PrintDialog()

            '                    If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then

            '                        oRpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
            '                        _PrinterName = PrintDialog1.PrinterSettings.PrinterName
            '                        If gstrPrintMultipleRx_PerScriptPage = True Then
            '                            oRpt.Load(Application.StartupPath & "\Reports\Rpt_KozmaryPrescription.rpt")
            '                        Else
            '                            oRpt.Load(Application.StartupPath & "\Reports\Rpt_Prescription.rpt")
            '                        End If
            '                        oRpt.PrintToPrinter(1, False, 0, 0)
            '                        PrintRxReport = DialogResult.OK
            '                        If Not IsNothing(oRpt) Then
            '                            oRpt.Close()
            '                        End If

            '                        '----------------------
            '                    Else
            '                        PrintRxReport = DialogResult.Cancel
            '                    End If
            '                    PrintDialog1.Dispose()
            '                    PrintDialog1 = Nothing
            '                Else
            '                    'code added by dipak 20090820 for solving bug:#1773# Summary of visit>Print Rx--Printer Dialogue Opens 2 times  
            '                    If (_PrinterName <> "") Then
            '                        oRpt.PrintOptions.PrinterName = _PrinterName
            '                    End If
            '                    'end add
            '                    oRpt.PrintToPrinter(1, False, 0, 0)
            '                    PrintRxReport = DialogResult.OK
            '                End If
            '            Else
            '                mdlFAX.gstrFAXContactPerson = ""
            '                mdlFAX.gstrFAXContactPersonFAXNo = ""
            '                mdlFAX.multipleRecipients = False
            '                mdlFAX.gstrFAXContacts = Nothing

            '                'sarika internet fax
            '                If gblnInternetFax = False Then

            '                    'Check FAX Printer settings are set or not
            '                    If isPrinterSettingsSet(True) = False Then
            '                        Return Nothing
            '                        Exit Function
            '                    End If
            '                    Try
            '                        Call MainMenu.SetFAXPrinterDefaultSettings1()
            '                    Catch ex As Exception
            '                        MessageBox.Show("Error in Prescription : " & ex.ToString, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '                    End Try

            '                    Dim objmytable As mytable
            '                    Dim objFAX As New clsFAX

            '                    'sarika 26th nov 07
            '                    objmytable = objFAX.GetPharmacyFaxNoForRx(_RxBusinessLayer.PharmacyId)
            '                    If Not IsNothing(objmytable) Then
            '                        gstrFAXContactPersonFAXNo = objmytable.Description
            '                        gstrFAXContactPerson = objmytable.Code
            '                    End If
            '                    'Change made to solve memory Leak and word crash issue
            '                    If Not objmytable Is Nothing Then
            '                        objmytable.Dispose()
            '                        objmytable = Nothing
            '                    End If

            '                    If Trim(gstrFAXContactPerson) = "" Then
            '                        gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
            '                    End If

            '                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
            '                        gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
            '                    End If

            '                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
            '                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                        'code added by sagar because it use to sent fax even after clicked the Ok button of the warning
            '                        Return Nothing
            '                        Exit Function
            '                    End If

            '                    Dim strFAXDocumentName As String
            '                    strFAXDocumentName = RetrieveFAXDocumentName()

            '                    If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then
            '                        Return Nothing
            '                        Exit Function
            '                    End If

            '                    objFAX.AddPendingFAX(m_PatientId, gstrFAXContactPerson, "Prescription", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
            '                    objFAX.Dispose()    'Change made to solve memory Leak and word crash issue
            '                    objFAX = Nothing
            '                    strFAXDocumentName = Nothing 'Change made to solve memory Leak and word crash issue
            '                    oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
            '                    oRpt.PrintToPrinter(1, False, 0, 0)

            '                Else
            '                    Dim objmytable As mytable
            '                    Dim objFAX As New clsFAX
            '                    'sarika 26th nov 07
            '                    objmytable = objFAX.GetPharmacyFaxNoForRx(_RxBusinessLayer.PharmacyId)
            '                    If Not IsNothing(objmytable) Then
            '                        gstrFAXContactPersonFAXNo = objmytable.Description
            '                        gstrFAXContactPerson = objmytable.Code
            '                    End If
            '                    'Change made to solve memory Leak and word crash issue
            '                    If Not objmytable Is Nothing Then
            '                        objmytable.Dispose()
            '                        objmytable = Nothing
            '                    End If
            '                    If Not objFAX Is Nothing Then
            '                        objFAX.Dispose()
            '                        objFAX = Nothing
            '                    End If
            '                    If Trim(gstrFAXContactPerson) = "" Then
            '                        gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
            '                    End If

            '                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
            '                        gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
            '                    End If

            '                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
            '                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                        'code added by sagar because it use to sent fax even after clicked the Ok button of the warning
            '                        Return Nothing
            '                        Exit Function
            '                    End If

            '                    Dim objFaxReport As New clsPrintFaxReport(m_PatientId)
            '                    objFaxReport.FaxReport(oRpt)
            '                    objFaxReport = Nothing
            '                    'sarika internet fax
            '                End If
            '            End If
            '        End If
            '    End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to print Prescription Report", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally
            'If Not IsNothing(oRpt) Then
            '    oRpt.Close()
            '    oRpt.Dispose()
            '    oRpt = Nothing
            'End If

        End Try
        Return Nothing
    End Function

    Private Sub PrintRxSummary()
        Dim oDB As New DataBaseLayer
        Dim oResultRx As DataTable = Nothing
        'Dim arrListNonNarco As New ArrayList
        'Dim arrListNarco As New ArrayList
        _RxBusinessLayer = New RxBusinesslayer(m_PatientId)
        Try

            Dim strQuery As String = "select distinct dtPrescriptionDate from Prescription WHERE nVisitID = " & m_VisitId & "  and nPatientID = " & m_PatientId
            oResultRx = oDB.GetDataTable_Query(strQuery)
            If Not IsNothing(oResultRx) AndAlso oResultRx.Rows.Count > 0 Then

                For cnt As Int32 = 0 To oResultRx.Rows.Count - 1
                    If Not IsDBNull(oResultRx.Rows(cnt)(0)) Then
                        Dim drgsids As String = ""
                        drgsids = _RxBusinessLayer.GetPrescriptionIdsforPrint(CType(oResultRx.Rows(cnt)(0), DateTime))
                        If drgsids <> "" Then

                            Dim DrugsIDArry As String() = SplitDrugID(drgsids)
                            Dim str As String = String.Empty
                            If Not IsNothing(DrugsIDArry) Then
                                If DrugsIDArry.Length > 0 Then
                                    For i As Integer = 0 To DrugsIDArry.Length - 1
                                        If i = 0 Then
                                            str = DrugsIDArry(i).ToString()
                                        Else
                                            str = str & "," & DrugsIDArry(i).ToString()
                                        End If
                                    Next
                                    PrintRxReport(str, CType(CType(oResultRx.Rows(cnt)(0), DateTime), String), False, CType(oResultRx.Rows(cnt)(0), DateTime))
                                End If
                            End If

                        End If
                    End If
                Next
                _PrinterName = ""
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to send the Print prescription due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ex = Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not oDB Is Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not oResultRx Is Nothing Then
                oResultRx.Dispose()
                oResultRx = Nothing
            End If
            If Not _RxBusinessLayer Is Nothing Then
                _RxBusinessLayer.Dispose()
                _RxBusinessLayer = Nothing
            End If
            Cursor.Current = Cursors.Default
            DeleteXML()
        End Try


    End Sub

    Private Function SplitDrugID(ByVal DrugsID As String) As Array
        Try
            Dim _result As String()
            _result = DrugsID.Split("|")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try

    End Function
    Private Sub DeleteXML()
        If File.Exists(strFileName) Then
            File.Delete(strFileName)
        End If

    End Sub

    Private Function GetReport() As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As DataTable = Nothing
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sReportType"
            oParamater.Value = "RxReport" '' to Fill All Users
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("ScanReports_MST")

            If Not oResultTable Is Nothing Then

                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not oDB Is Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Private Sub ReadSection(ByVal oData As DataSet)
        Dim otable As New DataTable
        For Each otable In oData.Tables
            Select Case otable.TableName
                Case "ReportHeader"
                    SectionDetails(otable, otable.TableName)
                Case "PageHeader"
                    SectionDetails(otable, otable.TableName)
                Case "Details"
                    SectionDetails(otable, otable.TableName)
                Case "PageFooter"
                    SectionDetails(otable, otable.TableName)
                Case "ReportFooter"
                    SectionDetails(otable, otable.TableName)

            End Select
        Next

        otable = Nothing 'Change made to solve memory Leak and word crash issue
    End Sub

    Private Sub SectionDetails(ByVal oTable As DataTable, ByVal sectiontype As String)
        Dim objSection As New gloEMR.gloRxReports.Section
        'Dim dv As New DataView
        'dv = oTable.DefaultView
        objSection.SectionType = sectiontype
        objSection.SectionWidth = oTable.Rows(0).Item("Width")
        objSection.SectionHeight = oTable.Rows(0).Item("Height")
        SectionsCol.Add(objSection)
    End Sub

    Private Function GetData(ByVal strfield As String) As String
        Dim oDB As New DataBaseLayer
        Dim oResult As String
        Try
            Dim strQuery As String
            strQuery = "select " & strfield & " from RxPrintFaxReport"

            oResult = oDB.GetRecord_Query(strQuery).ToString

            If Not IsDBNull(oResult) Then
                Return oResult
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not oDB Is Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Private Sub ClearCollection()
        Dim i As Int16
        'Make sure to Clear the collection before u can populate
        'it with new items
        If Not IsNothing(SectionsCol) Then

            For i = SectionsCol.Count To 1 Step -1
                SectionsCol.Remove(i)
            Next
        End If
        If Not IsNothing(ReportHeaderCol) Then
            For i = ReportHeaderCol.Count To 1 Step -1
                ReportHeaderCol.Remove(i)
            Next
        End If
        If Not IsNothing(PageHeaderCol) Then
            For i = PageHeaderCol.Count To 1 Step -1
                PageHeaderCol.Remove(i)
            Next
        End If
        If Not IsNothing(PageFooterCol) Then
            For i = PageFooterCol.Count To 1 Step -1
                PageFooterCol.Remove(i)
            Next
        End If
        If Not IsNothing(ReportFooterCol) Then
            For i = ReportFooterCol.Count To 1 Step -1
                ReportFooterCol.Remove(i)
            Next
        End If
    End Sub

    'code commented by supriya 19/7/2008
    Private Function BindGrid(ByVal oTable As DataTable, Optional ByVal drgId As Long = 0) As DataTable
        Try
            Dim strQuery As String
            obj = New ClsRxReportDictionary
            Dim oView As New DataView
            oView = oTable.DefaultView
            Dim strsort = oTable.Columns(1).ColumnName
            oView.Sort = "[" & strsort & "]"
            strQuery = "select "
            If oView.Count > 1 Then
                For cnt As Int16 = 0 To oView.Count - 1
                    Dim strtemp As String = ""
                    If oView.Item(cnt).Item("Text") = "May Substitute" Then
                        strtemp = "[May Substitute]"
                        strQuery &= strtemp
                    ElseIf oView.Item(cnt).Item("Text") = "May" Then
                        strtemp = "[May Substitute]"
                        strQuery &= strtemp
                    Else
                        strQuery &= oView.Item(cnt).Item("Text")
                    End If
                    If cnt < oView.Count - 1 Then
                        strQuery &= ","
                    End If
                Next
            End If
            Dim strwhereclause As String = ""
            If m_status = "FaxNC" Then
                strwhereclause = " where NarcoticCategory= 'C1' "
            ElseIf m_status = "PrintN" Then
                If drgId <> 0 Then
                    strwhereclause = " where NarcoticCategory <>'C1' and substring(NarcoticCategory,3,len(NarcoticCategory)) ='" & drgId & "'"
                Else
                    strwhereclause = " where NarcoticCategory ='C1'"
                End If

            End If

            strQuery &= " from RxPrintFaxReport" & strwhereclause 'DetailsField(DetailsField.Count - 1) 
            Dim objTable As DataTable
            objTable = obj.getReportData(strQuery)
            'Change made to solve memory Leak and word crash issue
            strQuery = Nothing
            If Not oView Is Nothing Then
                oView.Dispose()
                oView = Nothing
            End If
            obj = Nothing
            Return objTable
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function
   

#End Region

#End Region

#Region "Referrals"

    Private Sub ExamNotesSelection(ByVal blnReferrals As Boolean)
        If blnReferrals = False Then
            If gblnExamSelection = 0 Then
                rbtnNone.Checked = True
            ElseIf gblnExamSelection = 1 Then
                rbtnNotes.Checked = True
            ElseIf gblnExamSelection = 2 Then
                rbtnSelect.Checked = True
            End If
        Else

            If gblnExamSelection = 0 Then
                rbNone.Checked = True
            ElseIf gblnExamSelection = 1 Then
                rbNotes.Checked = True
            ElseIf gblnExamSelection = 2 Then
                rbSelect.Checked = True
            End If
        End If


    End Sub

    Private Sub LoadReferralTemplate()
        Try

            dtddTemplate = objReferralsDBLayer.FillControls("T", m_PatientId)

            If Not IsNothing(dtddTemplate) Then
                If dtddTemplate.Rows.Count > 0 Then
                    ddTemplate.DataSource = dtddTemplate
                    ddTemplate.DisplayMember = dtddTemplate.Columns(1).ColumnName
                    ddTemplate.ValueMember = dtddTemplate.Columns(0).ColumnName
                    ddTemplate.SelectedIndex = 0
                    TemplateId = CType(dtddTemplate.Rows(0).Item(0), System.Int64)
                Else
                    MessageBox.Show("No Template is associated for Referral Letter. Please associate any template first", "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("No Template is associated for Referral Letter. Please associate any template first", "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Function GeneratePrintFaxDocument(ByVal blnCompleteNotes As Boolean, Optional ByVal IsPrintFlag As Boolean = True) As Boolean


        Try
            ''check if Referrals exists against given m_VisitId
            If Not objReferralsDBLayer.CheckReferral(m_VisitId, m_ExamId, m_PatientId) Then
                Dim dtVisitRef As DataTable
                dtVisitRef = objReferralsDBLayer.FetchReferralsForUpdate(m_VisitId, m_PatientId, m_ExamId)
                If Not dtVisitRef Is Nothing Then
                    If dtVisitRef.Rows.Count > 0 Then
                        If IsPrintFlag Then
                            bIsRefferallettersave = True
                            SaveReferrals()
                            PrintRefContents(dtVisitRef, m_PatientId, m_VisitId, m_ExamId, True)
                        Else
                            bIsRefferallettersave = True
                            SaveReferrals()
                            FaxRefContents(dtVisitRef, m_PatientId, m_VisitId, m_ExamId, True, True)
                        End If
                        dtVisitRef.Dispose()
                        dtVisitRef = Nothing

                        Return True
                    Else
                        dtVisitRef.Dispose()
                        dtVisitRef = Nothing

                        Return ConfirmCompleteNotes(blnCompleteNotes)
                    End If

                Else

                    Return ConfirmCompleteNotes(blnCompleteNotes)
                End If


            Else
                'if Referral Details do not exist for that m_VisitId,
                'Populate Referrals Details from Patient_Dtl Table
                Dim dtPatRef As DataTable

                dtPatRef = objReferralsDBLayer.FillControls("R", m_PatientId)

                If Not dtPatRef Is Nothing Then
                    If dtPatRef.Rows.Count > 0 Then
                        If IsPrintFlag Then
                            bIsRefferallettersave = True
                            SaveReferrals()
                            PrintRefContents(dtPatRef, m_PatientId, m_VisitId, m_ExamId, False)
                        Else
                            bIsRefferallettersave = True

                            SaveReferrals()


                            FaxRefContents(dtPatRef, m_PatientId, m_VisitId, m_ExamId, True, False)

                        End If
                        dtPatRef.Dispose()
                        dtPatRef = Nothing

                        Return True
                    Else
                        dtPatRef.Dispose()
                        dtPatRef = Nothing

                        Return ConfirmCompleteNotes(blnCompleteNotes)
                    End If

                Else

                    Return ConfirmCompleteNotes(blnCompleteNotes)
                End If
            End If

            bIsRefferallettersave = False

            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            'dtVisitRef.Dispose()
            'dtVisitRef = Nothing
            'dtPatRef.Dispose()
            'dtPatRef = Nothing
        End Try

    End Function


    Private Function SendSecureMessage(ByVal blnCompleteNotes As Boolean) As Boolean


        Try

            If Not strProviderDirectAddress.Any() AndAlso gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation Is Nothing Then

                MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                SendSecureMessage = Nothing
                Exit Function
            End If

            Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(m_PatientId)
            If sError <> "" Then
                MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                SendSecureMessage = Nothing
                Exit Function
            End If

            ''check if Referrals exists against given m_VisitId
            If Not objReferralsDBLayer.CheckReferral(m_VisitId, m_ExamId, m_PatientId) Then
                Dim dtVisitRef As DataTable
                dtVisitRef = objReferralsDBLayer.FetchReferralsForUpdate(m_VisitId, m_PatientId, m_ExamId)
                If Not dtVisitRef Is Nothing Then
                    If dtVisitRef.Rows.Count > 0 Then
                        ''Open Send Mail Form
                        ''*******************************
                        'PrintRefContents(dtVisitRef, m_PatientId, m_VisitId, m_ExamId, True, True)
                        Call SendDocs()
                        dtVisitRef.Dispose()
                        dtVisitRef = Nothing
                        Return True
                    Else
                        dtVisitRef.Dispose()
                        dtVisitRef = Nothing
                        Return ConfirmCompleteNotes(blnCompleteNotes)
                    End If

                Else
                    Return ConfirmCompleteNotes(blnCompleteNotes)
                End If


            Else
                'if Referral Details do not exist for that m_VisitId,
                'Populate Referrals Details from Patient_Dtl Table
                Dim dtPatRef As DataTable
                dtPatRef = objReferralsDBLayer.FillControls("R", m_PatientId)
                If Not dtPatRef Is Nothing Then
                    If dtPatRef.Rows.Count > 0 Then
                        ''Open Send Mail Form
                        ''*******************************
                        ' PrintRefContents(dtPatRef, m_PatientId, m_VisitId, m_ExamId, False, True)
                        Call SendDocs()
                        dtPatRef.Dispose()
                        dtPatRef = Nothing
                        Return True
                    Else
                        Return ConfirmCompleteNotes(blnCompleteNotes)
                    End If

                Else
                    Return ConfirmCompleteNotes(blnCompleteNotes)
                End If
            End If

            bIsRefferallettersave = False
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            'dtVisitRef.Dispose()
            'dtVisitRef = Nothing
            'dtPatRef.Dispose()
            'dtPatRef = Nothing
        End Try
    End Function

    Private Function ConfirmCompleteNotes(ByVal blnFinish As Boolean) As Boolean
        Dim dgConfirm As DialogResult

        If blnFinish Then
            dgConfirm = MessageBox.Show("There is no primary care physician or referring doctor or other care team associated with this patient. Do you want to complete the Notes?", "Summary of Visit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dgConfirm = Windows.Forms.DialogResult.Yes Then
                Return True
            Else
                Return False
            End If
        Else
            MessageBox.Show("There is no primary care physician or referring doctor or other care team associated with this patient.", "Summary of Visit", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        End If


    End Function
    Private Sub PrintRefContents(ByVal objTable2 As DataTable, ByVal m_PatientId As Int64, ByVal m_VisitId As Int64, ByVal m_ExamId As Int64, ByVal blnRef As Boolean, Optional ByVal IsSecureMessage As Boolean = False)
        Try
            Dim strNotesFile As String
            strNotesFile = SelectedFileNotes()
            'variable oPrint moved from loop to out of loop for preserving variable _sPrivioususedPrinter by Dipak 20090825
            Dim oPrint As New clsPrintFAX
            'variable added by dipak 20090825 for track printdialogbox's cancel button click
            Dim blnPrintCancel As Boolean = False

            '' Problem : 00000185
            '' Description : Exam prints twice if the referring provider is same as PCP.
            '' Reason for change : Filter the Table to take only distinct records on the basis of nPCPID and PCPName.
            Dim objTable As DataTable = Nothing
            If Not blnRef Then
                objTable = objTable2.DefaultView.ToTable(True, "nPCPID", "PCPName")
            Else
                objTable = objTable2.Copy()
            End If
            If objTable.Rows.Count > 0 Then
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Dim oCurDoc As Wd.Document = Nothing
                Try
                    For j As Int32 = 0 To objTable.Rows.Count - 1
                        Dim strRefName As String
                        Dim m_ContactId As Int64

                        If blnRef Then
                            strRefName = objTable.Rows(j)("ContactName").ToString
                            If Not IsDBNull(objTable.Rows(j)("ReferralToFrom")) Then
                                'm_ContactId = CType(objTable.Rows(j)(2), Int64)
                                m_ContactId = CType(objTable.Rows(j)("ReferralToFrom"), Int64)
                            End If

                        Else
                            strRefName = objTable.Rows(j)("PCPName").ToString
                            If Not IsDBNull(objTable.Rows(j)("nPCPID")) Then
                                m_ContactId = CType(objTable.Rows(j)("nPCPID"), Int64)
                            End If

                        End If
                        strRefName = Nothing    'Change made to solve memory Leak and word crash issue
                        Dim strFileName As String
                        ObjWord = New clsWordDocument
                        objCriteria = New DocCriteria
                        objCriteria.DocCategory = enumDocCategory.Template
                        objCriteria.PrimaryID = TemplateId
                        ObjWord.DocumentCriteria = objCriteria
                        strFileName = ObjWord.RetrieveDocumentFile()
                        objCriteria.Dispose()
                        objCriteria = Nothing
                        ObjWord = Nothing
                        If (IsNothing(strFileName) = False) Then


                            If strFileName <> "" Then
                                ObjWord = New clsWordDocument
                                objCriteria = New DocCriteria
                                objCriteria.DocCategory = enumDocCategory.Referrals
                                objCriteria.PatientID = m_PatientId
                                objCriteria.VisitID = m_VisitId
                                objCriteria.PrimaryID = m_ContactId
                                ObjWord.DocumentCriteria = objCriteria

                               
                                oCurDoc = myLoadWord.LoadWordApplication(strFileName)
                                ObjWord.CurDocument = oCurDoc
                                ObjWord.GetFormFieldData(enumDocType.None)
                                oCurDoc = ObjWord.CurDocument
                                objCriteria.Dispose()
                                objCriteria = Nothing
                                ObjWord = Nothing
                                If strNotesFile <> "" Then
                                    If File.Exists(strNotesFile) Then
                                        UpdateVoiceLog("Inserting Exam Notes in PrintRefContents")
                                        oCurDoc.ActiveWindow.SetFocus()
                                        oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                                        oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                                        oCurDoc.ActiveWindow.Selection.InsertFile(strNotesFile)

                                        Try
                                            oCurDoc.Save()
                                        Catch ex As Exception
                                            gloWord.LoadAndCloseWord.ClearWordGarbage(oCurDoc.Application)
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                            ex = Nothing
                                        End Try


                                        UpdateVoiceLog("Exam Notes Inserted in PrintRefContents")

                                    End If
                                End If

                                'If IsSecureMessage Then
                                '    ''Read Secure Messages settings and call Inbox form
                                '    If File.Exists(strFileName) Then
                                '        Dim oSendDoc As New clsPrintFAX
                                '        Dim osenddox As String
                                '        oTempDoc = oCurDoc
                                '        osenddox = oSendDoc.SendDoc(oCurDoc, m_PatientId)
                                '        If Not IsNothing(oSendDoc) Then
                                '            oSendDoc.Dispose()
                                '        End If

                                '        wdTemp.Close()
                                '        Me.Controls.Remove(wdTemp)
                                '        wdTemp.Dispose()
                                '        wdTemp = Nothing

                                '        'Dim ofrmSendNewMail As New InBox.NewMail(m_PatientId, osenddox)
                                '        Dim ofrmSendNewMail As New InBox.NewMail(m_PatientId, osenddox, m_ContactId)
                                '        If gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                                '            gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(m_ProviderId)
                                '            ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation
                                '        End If

                                '        ofrmSendNewMail.ShowInTaskbar = True
                                '        ofrmSendNewMail.ShowDialog()
                                '        If Not IsNothing(ofrmSendNewMail) Then
                                '            ofrmSendNewMail.Close()
                                '        End If
                                '        ofrmSendNewMail = Nothing

                                '    Else
                                '        MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                '    End If

                                'Else

                                If (oPrint.PrintDoc(oCurDoc, m_PatientId, Not CType(j, Boolean)) = DialogResult.Cancel) Then
                                    blnPrintCancel = True
                                End If
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, "Referral Printed.", gloAuditTrail.ActivityOutCome.Success)
                                'wdTemp.Close()
                                'Me.Controls.Remove(wdTemp)
                                'wdTemp.Dispose()
                                'oCurDoc = Nothing
                                myLoadWord.CloseWordOnly(oCurDoc)
                                'End If
                            End If
                        End If
                        strFileName = Nothing   'Change made to solve memory Leak and word crash issue
                        'code added by dipak 20050825 to exit from for loop when user click on cancel of print dialogbox

                        If (blnPrintCancel = True) Then
                            Exit For
                        End If

                    Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
            End If
            objTable.Dispose()
            objTable = Nothing
            oPrint.Dispose()
            oPrint = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub FaxRefContents(ByVal objTable As DataTable, ByVal m_PatientId As Int64, ByVal m_VisitId As Int64, ByVal m_ExamId As Int64, ByVal blnFinish As Boolean, ByVal blnRef As Boolean)
        Dim arrFaxNos As New ArrayList
        Dim objList As myList = Nothing
        Dim oSetting As New gloSettings.GeneralSettings(GetConnectionString)            'CAS-19703-K4N5K5 – Some finished exams will have some parts missing when faxing with a cover page/Referral Letter.
        oSetting.GetSetting("InsertReferralLetterFirstForFax", InsertReferralLetterFirstForFax)
        '   Dim wdTempFax As Wd.Application
        Try
            'wdTempFax = New Wd.Application

            UpdateVoiceLog("In FaxRefContents method")
            'Dim mstream As ADODB.Stream
            Dim strFileName As String
            Dim strReferalletter As String

            Dim blnFAXPrinterHasToSet As Boolean = True
            Dim blnDSODefaultPrinterHasToSet As Boolean = True

            'If Not objTable Is Nothing Then

            '    If objTable.Rows.Count > 0 Then
            Dim strNotesFile As String
            strNotesFile = SelectedFileNotes()

            clsPrintFAX.IsBlackIceSettingsSet = False
            If (objTable.Rows.Count > 0) Then


                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Dim oCurDoc As Wd.Document = Nothing
                Try
                    For j As Int32 = 0 To objTable.Rows.Count - 1
                        Dim strRefName As String
                        Dim m_ContactId As Int64

                        '' Collection reset
                        gstrfaxCollection = New Collection()

                        If blnRef Then
                            strRefName = objTable.Rows(j)("ContactName").ToString
                            If Not IsDBNull(objTable.Rows(j)("ReferralToFrom")) Then
                                'm_ContactId = CType(objTable.Rows(j)(2), Int64)
                                m_ContactId = CType(objTable.Rows(j)("ReferralToFrom"), Int64)
                            End If

                        Else
                            strRefName = objTable.Rows(j)("PCPName").ToString
                            If Not IsDBNull(objTable.Rows(j)("nPCPID")) Then
                                m_ContactId = CType(objTable.Rows(j)("nPCPID"), Int64)
                            End If

                        End If
                        UpdateVoiceLog("Sending Referral Letter - " & j + 1)

                        ObjWord = New clsWordDocument
                        objCriteria = New DocCriteria
                        objCriteria.DocCategory = enumDocCategory.Template
                        objCriteria.PrimaryID = TemplateId
                        ObjWord.DocumentCriteria = objCriteria
                        UpdateVoiceLog("Retrieving Referral Letter Contents from Database & Save it to Physical File")
                        strFileName = ObjWord.RetrieveDocumentFile()
                        UpdateLog("******* strFileName latest- " & strFileName & "********")
                        objCriteria.Dispose()
                        objCriteria = Nothing
                        ObjWord = Nothing
                        If (IsNothing(strFileName) = False) Then


                            If strFileName <> "" Then
                                ObjWord = New clsWordDocument
                                objCriteria = New DocCriteria
                                objCriteria.DocCategory = enumDocCategory.Referrals
                                objCriteria.PatientID = m_PatientId
                                objCriteria.VisitID = m_VisitId
                                objCriteria.PrimaryID = m_ContactId
                                ObjWord.DocumentCriteria = objCriteria

                              
                                UpdateLog("*****  wdTemp.Open(strFileName) latest  Start******")




                                '   wdTemp.Open(strFileName)


                                UpdateLog("*****  wdTemp.Open(strFileName) latest end******")

                                oCurDoc = myLoadWord.LoadWordApplication(strFileName)
                                '  oCurDoc = wdTempFax.Documents.Open(strFileName)
                                '   oCurDoc = wdTemp.ActiveDocument
                                ObjWord.CurDocument = oCurDoc
                                ObjWord.GetFormFieldData(enumDocType.None)
                                oCurDoc = ObjWord.CurDocument

                                ObjWord = Nothing
                                objCriteria.Dispose()
                                objCriteria = Nothing
                                If (IsNothing(strNotesFile) = False) Then

                                    If strNotesFile <> "" Then
                                        If File.Exists(strNotesFile) Then

                                            If InsertReferralLetterFirstForFax = True Then      'if settings(InsertReferralLetterFirstForFax)='True' then Regular code
                                                UpdateVoiceLog("Inserting Exam Notes in FaxRefContents")
                                                oCurDoc.ActiveWindow.SetFocus()
                                                oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                                                oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                                                oCurDoc.ActiveWindow.Selection.InsertFile(strNotesFile)

                                                Try
                                                    oCurDoc.Save()
                                                Catch ex As Exception
                                                    gloWord.LoadAndCloseWord.ClearWordGarbage(oCurDoc.Application)
                                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                                    ex = Nothing
                                                End Try


                                                UpdateVoiceLog("Exam Notes Inserted in FaxRefContents")
                                            Else
                                                oCurDoc.Save()                                  'if settings(InsertReferralLetterFirstForFax)='False' then New code
                                                myLoadWord.CloseWordOnly(oCurDoc)

                                                strReferalletter = ExamNewDocumentName
                                                FileCopy(strNotesFile, strReferalletter)
                                                oCurDoc = myLoadWord.LoadWordApplication(strReferalletter)
                                                oCurDoc.ActiveWindow.SetFocus()
                                                oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
                                                oCurDoc.ActiveWindow.Selection.InsertFile(strFileName)
                                                oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)

                                                Try
                                                    oCurDoc.Save()
                                                Catch ex As Exception
                                                    gloWord.LoadAndCloseWord.ClearWordGarbage(oCurDoc.Application)
                                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                                    ex = Nothing
                                                End Try
                                                UpdateVoiceLog("Inserting Exam Notes in FaxRefContents")
                                            End If

                                        End If
                                    End If
                                End If
                                Dim myWordFile As String = myLoadWord.SaveCurrentWord(oCurDoc, gloSettings.FolderSettings.AppTempFolderPath)
                                myLoadWord.CloseWordOnly(oCurDoc)
                                UpdateVoiceLog("Calling RetrieveFAXDetails method to retrieve FAX Details")

                                Dim blnFirstReferral As Boolean
                                If j = 0 Then
                                    blnFirstReferral = True
                                Else
                                    blnFirstReferral = False
                                End If

                                mdlFAX.Owner = Me

                                'sarika Fax from Referrals 20081121
                                mdlFAX.gstrFAXContactPerson = ""
                                mdlFAX.gstrFAXContactPersonFAXNo = ""
                                mdlFAX.gstrFAXContacts = Nothing
                                mdlFAX.multipleRecipients = False
                                'Added code for Fax type against the Problem #00000874
                                mdlFAX.gstrFAXType = "Referral Letter"
                                '-------------

                                If RetrieveFAXDetails(mdlFAX.enmFAXType.ReferralLetter, CStr(m_PatientId), strRefName, "", ddTemplate.Text, m_ContactId, m_VisitId, m_ExamId, blnFirstReferral, Me) Then
                                    '   If RetrieveFAXDetails(mdlFAX.enmFAXType.ReferralLetter, CStr(m_PatientId), strRefName, "", strReferralLetter, m_ContactId, m_VisitId, m_ExamId, blnFirstReferral) Then

                                    UpdateVoiceLog("FAX Details retrieved")
                                    'If j >= 1 Then
                                    '    blnFAXPrinterHasToSet = False
                                    'End If
                                    If j >= objTable.Rows.Count - 1 Then
                                        blnDSODefaultPrinterHasToSet = True
                                    Else
                                        blnDSODefaultPrinterHasToSet = False
                                    End If

                                    UpdateVoiceLog("Creating object of clsPrintFAX class")
                                    Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
                                    UpdateVoiceLog("Calling FAX Document method")

                                    If j <> 0 Then
                                        clsPrintFAX.IsBlackIceSettingsSet = True
                                    End If


                                    If ChkSameContact(gstrFAXContactPerson, gstrFAXContactPersonFAXNo, arrFaxNos) = False Then

                                        UpdateLog("*****   FAXDocument in FaxRefContents  Start******")
                                        If objPrintFAX.FAXDocument(myLoadWord, myWordFile, CStr(m_PatientId), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, ddTemplate.SelectedItem(1), clsPrintFAX.enmFAXType.ReferralLetter, True, blnFAXPrinterHasToSet, blnDSODefaultPrinterHasToSet) = False Then
                                            'TIFF File has not been created
                                            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                                                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                            End If


                                        End If
                                        UpdateLog("*****   FAXDocument in FaxRefContents  end******")
                                        objList = New myList
                                        objList.ContactName = gstrFAXContactPerson
                                        objList.ContactPersonFaxNo = gstrFAXContactPersonFAXNo
                                        ' arrFaxNos.Add(gstrFAXContactPersonFAXNo)
                                        arrFaxNos.Add(objList)
                                        objList = Nothing
                                        UpdateVoiceLog(" Document Faxed ")
                                    End If
                                    objPrintFAX.Dispose()
                                    objPrintFAX = Nothing

                                End If

                                'UpdateLog("*****   wdTemp.Close() in FaxRefContents  Start******")
                                'wdTemp.Close()
                                'UpdateLog("*****   wdTemp.Close() in FaxRefContents  end******")
                                'UpdateLog("*****   Me.Controls.Remove(wdTemp) in FaxRefContents  Start******")
                                'Me.Controls.Remove(wdTemp)
                                'UpdateLog("*****   Me.Controls.Remove(wdTemp) in FaxRefContents  end******")



                                'wdTemp.Dispose()
                                '   wdTemp = Nothing
                                'oCurDoc.Close(False)
                                'If Not IsNothing(oCurDoc) Then
                                '    System.Runtime.InteropServices.Marshal.ReleaseComObject(oCurDoc) '  'SLR: marshall free
                                'End If

                                'oCurDoc = Nothing

                            Else

                            End If
                        End If
                    Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
            End If
            'If Not wdTempFax Is Nothing Then
            '    wdTempFax.Quit()
            '    ''Commeneted 0n 20141031 To Resolve issue:# Target of invocation
            '    If Not IsNothing(wdTempFax) Then
            '        System.Runtime.InteropServices.Marshal.ReleaseComObject(wdTempFax) '  'SLR: marshall free
            '    End If
            '    wdTempFax = Nothing
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not objList Is Nothing Then objList = Nothing
            arrFaxNos = Nothing
            If (IsNothing(oSetting) = False) Then
                oSetting.Dispose() : oSetting = Nothing
            End If
        End Try
    End Sub

    Private Function SelectedFileNotes() As String

        If rbtnSelect.Checked OrElse rbtnNotes.Checked Then

            If NotesFileName <> "" Then
                If File.Exists(NotesFileName) Then
                    If rbtnNotes.Checked Then
                        Return NotesFileName
                    ElseIf rbtnSelect.Checked Then
                        If (IsNothing(strSelectedFileNotes)) Then
                            Return ""
                        Else
                            Return strSelectedFileNotes
                        End If

                    Else
                        Return ""
                    End If
                Else
                    Return ""

                End If
            Else
                Return ""

            End If
        Else
            Return ""

        End If

    End Function

    Private Sub ddTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddTemplate.SelectionChangeCommitted
        TemplateId = ddTemplate.SelectedValue
        'Added to resolve Bug #76350 ( Modified): 00000816: Patient Exams
        RefreshPatientReferrals()
    End Sub

    Private Sub btnReferrals_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferrals.Click, ToolStripButton21.Click

        UpdateExamLog("Referral Button Clicked", m_PatientId, m_ExamId)
        Me.Text = "Patient Referrals"


        pnlMainSummary.Visible = False
        pnlToolStrp.Visible = False
        pnlMainReferrals.Visible = True
        pnltlsReferrals.Visible = True

        Try
            gloPatient.gloPatient.GetWindowTitle(Me, m_PatientId, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#Region "Referrals Implementation"

    Private Function CreateSelectedNotes(ByVal strFilename As String) As String


        ''If condition removed for issue while selectednotes 
       
        Dim strFile As String = String.Empty

        Try

            'wdPrint = New AxDSOFramer.AxFramerControl



            'Me.Controls.Add(wdPrint)
            ''Bug #71063: #00000729: Refferal Letter
            'wdPrint.Location = New System.Drawing.Point(-50, -50)
            'wdPrint.CreateNew("Word.Document")
            'oCurDoc = wdPrint.ActiveDocument
            If (strFilename <> "") Then
                
                If (m_NotToCheckFile OrElse File.Exists(strFilename)) Then


                    Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                    Try
                        Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(strFilename)

                        'wdTemp = New AxDSOFramer.AxFramerControl

                        'Me.Controls.Add(wdTemp)
                        'wdTemp.Location = New System.Drawing.Point(-50, -50)
                        'If Not IsNothing(strFilename) Then
                        '    If strFilename.Trim().Length > 1 Then
                        '        wdTemp.Open(strFilename)
                        '    End If
                        'End If
                        'oTempDoc = wdTemp.ActiveDocument

                        '' oWordApp = oTempDoc.Application
                        ' oTempDoc.ActiveWindow.SetFocus()
                        Try
                            Dim oNewDoc As Wd.Document = myLoadWord.LoadWordApplication(Nothing, False)

                            'If oTempDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                            '    oTempDoc.Application.ActiveDocument.Unprotect()
                            'End If

                            oTempDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
                            oTempDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
                            Dim setClipBoardSemaphore As Boolean = False
                            Dim gotClip As Boolean = False
                            Dim strBM1End As Integer = 0
                            Dim strBM2 As Wd.Bookmark = Nothing

                            Dim blnFlag As Boolean = False
                            For i As Int32 = 1 To oTempDoc.Range.Bookmarks.Count

                                strBM2 = oTempDoc.Range.Bookmarks.Item(i)
                                'If InStr(strBM2, "BM") Then
                                If strBM2.Name.StartsWith("BM") Then
                                    If Not blnFlag Then
                                        blnFlag = True
                                        strBM1End = strBM2.End
                                    Else 'If (strBM1<>"") Then
                                        blnFlag = False
                                        If (setClipBoardSemaphore = False) Then
                                            Try
                                                Dim strEx As String = ""
                                                gotClip = Global.gloWord.gloWord.GetClipBoardWithRetry(5, strEx)
                                            Catch ex As Exception

                                            End Try
                                            setClipBoardSemaphore = True
                                        End If
                                        Call SelectBetweenBookmarks(oTempDoc, oNewDoc, strBM1End, strBM2.Start)

                                        'oTempDoc.ActiveWindow.SetFocus()
                                        'strBM1 = ""
                                        'strBM2 = ""
                                    End If
                                End If
                            Next

                            strBM2 = Nothing
                            If ((setClipBoardSemaphore = True) AndAlso (gotClip = True)) Then
                                Try
                                    Global.gloWord.gloWord.SetClipboardData()
                                Catch ex As Exception

                                End Try

                            End If
                            oNewDoc.ActiveWindow.SetFocus()
                            oNewDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)

                            If oNewDoc.Content.Text.Trim() = "" Then
                                strFile = ""
                            Else
                                strFile = ExamNewDocumentName
                                ' wdPrint.Save(strFile, True, "", "")
                                oNewDoc.SaveAs(strFile, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                            End If

                            'wdTemp.Close()
                            'Me.Controls.Remove(wdTemp)
                            'wdTemp.Dispose()
                            'oTempDoc = Nothing

                            'wdPrint.Close()
                            'Me.Controls.Remove(wdPrint)
                            'wdPrint.Dispose()
                            'oCurDoc = Nothing
                            If Not IsNothing(oCurDoc) Then
                                oCurDoc.ActiveWindow.SetFocus()
                            End If
                            myLoadWord.CloseWordOnly(oNewDoc)


                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try
                        If Not IsNothing(oCurDoc) Then
                            oCurDoc.ActiveWindow.SetFocus()
                        End If
                        myLoadWord.CloseWordOnly(oTempDoc)


                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    If Not IsNothing(oCurDoc) Then
                        oCurDoc.ActiveWindow.SetFocus()
                    End If
                    myLoadWord.CloseApplicationOnly()
                    myLoadWord = Nothing
                End If

            End If
            Return strFile


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            Try
                Me.Activate()
                wdReferrals.Activate()
                If Not IsNothing(oCurDoc) Then
                    oCurDoc.ActiveWindow.SetFocus()
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

        End Try

    End Function


    Private Sub SelectBetweenBookmarks(ByRef oTempDoc As Wd.Document, ByRef oCurDoc As Wd.Document, ByVal strBM1 As Integer, ByVal strBM2 As Integer)
        'try catch block added by dipak20091105 to fix bug 4988:Inconsisten error when finishing an exam note
        Try
            r = oTempDoc.Range(strBM1, strBM2)
            'if condition :"If r.Start <> r.End" added by dipak 20091105to fix bug 4988:Inconsisten error when finishing an exam note
            If (IsNothing(r) = False) Then


                If r.Start <> r.End Then
                    'Global.gloWord.gloWord.GetClipboardData()
                    r.Select()
                    r.Copy()

                    oCurDoc.ActiveWindow.SetFocus()
                    oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                    Try
                        oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                        oCurDoc.ActiveWindow.Selection.Range.Paste()
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.General, ex.ToString(), m_PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                        ex = Nothing
                    End Try


                    oTempDoc.ActiveWindow.SetFocus()

                    ' Clipboard.Clear()
                    'Global.gloWord.gloWord.SetClipboardData()
                End If


                r = Nothing
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.General, "No data in Clipboard No portion of text is selected :", m_PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ex = Nothing
            ''
        End Try
    End Sub

    Private Sub FillReferrals()
        Dim dt As DataTable
        Dim i As Integer

        Dim mychildnode As myTreeNode

        dt = GetReferrals()
        trReferrals.Nodes(0).Nodes.Clear()
        If Not IsNothing(dt) Then
            For i = 0 To dt.Rows.Count - 1
                myNodeList = New myList
                If Not IsNothing(arrlist) Then
                    arrlist = Nothing
                End If

                arrlist = New ArrayList
                myNodeList.ContactID = dt.Rows(i)("nContactID")
                myNodeList.ContactName = dt.Rows(i)("Name")
                myNodeList.ContactFirstName = dt.Rows(i)("FirstName")
                myNodeList.ContactMiddleName = dt.Rows(i)("MiddleName")
                myNodeList.ContactLastName = dt.Rows(i)("LastName")
                myNodeList.ContactGender = dt.Rows(i)("Gender")
                myNodeList.ContactDegree = dt.Rows(i)("Degree")
                myNodeList.ContactAddressLine1 = dt.Rows(i)("Street")
                myNodeList.ContactAddressLine2 = dt.Rows(i)("AddressLine2")
                myNodeList.ContactCity = dt.Rows(i)("City")
                myNodeList.ContactState = dt.Rows(i)("State")
                myNodeList.ContactZip = dt.Rows(i)("ZIP")
                myNodeList.ContactPhone = dt.Rows(i)("Phone")
                myNodeList.ContactFax = dt.Rows(i)("Fax")
                myNodeList.ContactMobile = dt.Rows(i)("Mobile")
                myNodeList.ContactExternalCode = dt.Rows(i)("ExternalCode")

                arrlist.Add(myNodeList)
                mychildnode = New myTreeNode '(dt.Rows(i)(1), dt.Rows(i)(0), dt.Rows(i)(2), CType(dt.Rows(i)(3), String))
                mychildnode.Key = dt.Rows(i)("nContactID")
                mychildnode.arrRefferalDetails = arrlist
                mychildnode.Text = dt.Rows(i)("Name")
                mychildnode.NodeName = dt.Rows(i)("Name")
                mychildnode.ImageIndex = 3
                mychildnode.SelectedImageIndex = 3
                trReferrals.Nodes(0).Nodes.Add(mychildnode)
                'Change made to solve memory Leak and word crash issue

                '  mychildnode.Dispose() : 
                mychildnode = Nothing

                myNodeList = Nothing
                arrlist = Nothing
            Next
            dt.Dispose()
            dt = Nothing
        End If

        trReferrals.ExpandAll()
        txtSearchReferrals.Focus()
    End Sub

    Private Sub PopulatePatientReferrals(Optional ByVal ReferralID As Long = 0)
        Dim rootnode As myTreeNode
        rootnode = New myTreeNode("Patient Referrals", -1)
        rootnode.ImageIndex = 1
        rootnode.SelectedImageIndex = 1
        trPatientReferrals.Nodes.Add(rootnode)

        'rootnode.Dispose() : 
        rootnode = Nothing 'Change made to solve memory Leak and word crash issue

        Dim rootnode1 As myTreeNode
        rootnode1 = New myTreeNode("Primary Care Physician", 0)
        rootnode1.ImageIndex = 2
        rootnode1.SelectedImageIndex = 2
        trPatientReferrals.Nodes.Item(0).Nodes.Add(rootnode1)
        'rootnode1.Dispose() : 
        rootnode1 = Nothing 'Change made to solve memory Leak and word crash issue

        Dim rootnode2 As myTreeNode
        rootnode2 = New myTreeNode("Referrals", 1)
        rootnode2.ImageIndex = 0
        rootnode2.SelectedImageIndex = 0
        trPatientReferrals.Nodes.Item(0).Nodes.Add(rootnode2)
        'rootnode2.Dispose() : 
        rootnode2 = Nothing 'Change made to solve memory Leak and word crash issue

        ''Added other care team in referrals on 7-july-2014
        Dim rootnode3 As myTreeNode
        rootnode3 = New myTreeNode("Other Care Team", 2)
        rootnode3.ImageIndex = 6
        rootnode3.SelectedImageIndex = 6
        trPatientReferrals.Nodes.Item(0).Nodes.Add(rootnode3)
        'rootnode3.Dispose() : 
        rootnode3 = Nothing 'Change made to solve memory Leak and word crash issue

        'check if Referrals exists against given m_VisitId
        If Not objReferralsDBLayer.CheckReferral(m_VisitId, m_ExamId, m_PatientId) Then

            If (ReferralID = 0) Then
                FillReferralDetails()
            Else
                OpenReffrelLetter(ReferralID)
            End If
            Savestatus = 2
        Else
            'if Referral Details do not exist for that m_VisitId,
            'Populate Referrals Details from Patient_Dtl Table
            FillPatientReferrals()
        End If

        trPatientReferrals.ExpandAll()
        trPatientReferrals.SelectedNode = trPatientReferrals.Nodes.Item(0)
    End Sub

    Private Sub FillPatientReferrals()
        Try
            Dim dt As DataTable
            dt = objReferralsDBLayer.FillControls("R", m_PatientId)
            Dim i As Integer
            If Not IsNothing(dt) Then
                Dim strRefLetter As String
                If Trim(gstrLoginProviderName) <> "All" AndAlso Trim(gstrLoginProviderName) <> "" Then
                    strRefLetter = objReferralsDBLayer.DefaultReferralLetter(gstrLoginProviderName)
                    '' if Default template is not associated with Parovider then select the template from combobox
                    '' templete in combo will be the 1st template in the combo
                    If strRefLetter.Trim = "" Then
                        strRefLetter = objReferralsDBLayer.GetReferralLetter(TemplateId)
                    End If
                Else
                    strRefLetter = objReferralsDBLayer.GetReferralLetter(TemplateId)
                End If


                For i = 0 To dt.Rows.Count - 1
                    Dim pcpnode As myTreeNode = Nothing

                    If CType(dt.Rows(i)(2), String) = "P" Then
                        'IF PCP is not null for that patient then add 
                        'node to pcp sub node
                        If Not dt.Rows(i).IsNull(0) Then

                            pcpnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), TemplateId)
                            pcpnode.ImageIndex = 3
                            pcpnode.SelectedImageIndex = 3
                            pcpnode.NodeName = GetUniqueKey()
                            If strRefLetter <> "" Then
                                pcpnode.Text = dt.Rows(i)(1).ToString & " - " & strRefLetter
                            Else
                                pcpnode.Text = dt.Rows(i)(1).ToString
                            End If
                            ''Sandip Darade 20090729
                            Dim dtDetail As DataTable
                            Dim PCPID As Int64 = Convert.ToInt64(dt.Rows(i)(0))
                            dtDetail = GetReferralDetail(PCPID)
                            PCPID = Nothing 'Change made to solve memory Leak and word crash issue
                            myNodeList = New myList
                            If Not IsNothing(arrlist) Then
                                arrlist = Nothing
                            End If
                            arrlist = New ArrayList
                            myNodeList.ContactName = dtDetail.Rows(0)("sContact")
                            myNodeList.ContactFlag = Convert.ToInt16(dtDetail.Rows(0)("nContactFlag"))
                            myNodeList.ContactFirstName = dtDetail.Rows(0)("sFirstName")
                            myNodeList.ContactMiddleName = dtDetail.Rows(0)("sMiddleName")
                            myNodeList.ContactLastName = dtDetail.Rows(0)("sLastName")
                            myNodeList.ContactGender = dtDetail.Rows(0)("sGender")
                            myNodeList.ContactDegree = dtDetail.Rows(0)("sDegree")
                            myNodeList.ContactAddressLine1 = dtDetail.Rows(0)("sAddressLine1")
                            myNodeList.ContactAddressLine2 = dtDetail.Rows(0)("sAddressLine2")
                            myNodeList.ContactCity = dtDetail.Rows(0)("sCity")
                            myNodeList.ContactState = dtDetail.Rows(0)("sState")
                            myNodeList.ContactZip = dtDetail.Rows(0)("sZIP")
                            myNodeList.ContactPhone = dtDetail.Rows(0)("sPhone")
                            myNodeList.ContactFax = dtDetail.Rows(0)("sFax")
                            myNodeList.ContactMobile = dtDetail.Rows(0)("sMobile")
                            myNodeList.ContactExternalCode = dtDetail.Rows(0)("sExternalCode")
                            arrlist.Add(myNodeList)
                            pcpnode.DMTemplateName = strRefLetter

                            pcpnode.arrRefferalDetails = arrlist
                            ''end 
                            'sarika Fax from Referrals 20081121
                            pcpnode.FaxReferralName = dt.Rows(i)("PCPName")
                            '---
                            trPatientReferrals.Nodes.Item(0).Nodes.Item(0).Nodes.Add(pcpnode)
                            'Change made to solve memory Leak and word crash issue
                            dtDetail.Dispose()
                            dtDetail = Nothing
                            myNodeList = Nothing

                            ' pcpnode.Dispose() : 
                            pcpnode = Nothing

                        End If
                        'if referral then add referrals for that patient against referrals
                        'subnode
                    Else
                        pcpnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), TemplateId)
                        pcpnode.ImageIndex = 3
                        pcpnode.SelectedImageIndex = 3
                        pcpnode.NodeName = dt.Rows(i)(1) ' GetUniqueKey()

                        Dim dtDetail As DataTable
                        Dim PCPID As Int64 = Convert.ToInt64(dt.Rows(i)(0))
                        dtDetail = GetReferralDetail(PCPID)
                        PCPID = Nothing 'Change made to solve memory Leak and word crash issue
                        myNodeList = New myList
                        If Not IsNothing(arrlist) Then
                            arrlist = Nothing
                        End If
                        arrlist = New ArrayList
                        myNodeList.ContactName = dtDetail.Rows(0)("sContact")
                        'If CType(dt.Rows(i)(2), String) = "R" Then
                        myNodeList.ContactFlag = Convert.ToInt16(dt.Rows(i)("nContactFlag"))
                        '    ElseIf CType(dt.Rows(i)(2), String) = "O" Then
                        '    myNodeList.ContactFlag = 4

                        'End If

                        myNodeList.ContactFirstName = dtDetail.Rows(0)("sFirstName")
                        myNodeList.ContactMiddleName = dtDetail.Rows(0)("sMiddleName")
                        myNodeList.ContactLastName = dtDetail.Rows(0)("sLastName")
                        myNodeList.ContactGender = dtDetail.Rows(0)("sGender")
                        myNodeList.ContactDegree = dtDetail.Rows(0)("sDegree")
                        myNodeList.ContactAddressLine1 = dtDetail.Rows(0)("sAddressLine1")
                        myNodeList.ContactAddressLine2 = dtDetail.Rows(0)("sAddressLine2")
                        myNodeList.ContactCity = dtDetail.Rows(0)("sCity")
                        myNodeList.ContactState = dtDetail.Rows(0)("sState")
                        myNodeList.ContactZip = dtDetail.Rows(0)("sZIP")
                        myNodeList.ContactPhone = dtDetail.Rows(0)("sPhone")
                        myNodeList.ContactFax = dtDetail.Rows(0)("sFax")
                        myNodeList.ContactMobile = dtDetail.Rows(0)("sMobile")
                        myNodeList.ContactExternalCode = dtDetail.Rows(0)("sExternalCode")
                        arrlist.Add(myNodeList)


                        pcpnode.DMTemplateName = strRefLetter
                        pcpnode.arrRefferalDetails = arrlist
                        ''''''''

                        If strRefLetter <> "" Then
                            pcpnode.Text = dt.Rows(i)(1).ToString & " - " & strRefLetter
                        Else
                            pcpnode.Text = dt.Rows(i)(1).ToString
                        End If
                        pcpnode.FaxReferralName = dt.Rows(i)(1).ToString
                        If CType(dt.Rows(i)("nContactFlag"), Int16) = 3 Then
                            trPatientReferrals.Nodes.Item(0).Nodes.Item(1).Nodes.Add(pcpnode)
                        ElseIf CType(dt.Rows(i)("nContactFlag"), Int16) = 4 Then
                            trPatientReferrals.Nodes.Item(0).Nodes.Item(2).Nodes.Add(pcpnode)
                        End If

                        'Change made to solve memory Leak and word crash issue
                        dtDetail.Dispose()
                        dtDetail = Nothing
                        myNodeList = Nothing

                        'pcpnode.Dispose() :
                        pcpnode = Nothing

                    End If
                    'Change made to solve memory Leak and word crash issue
                    If Not pcpnode Is Nothing Then
                        ' pcpnode.Dispose() : 
                        pcpnode = Nothing
                    End If
                Next
            End If
            'Change made to solve memory Leak and word crash issue
            If Not dt Is Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub FillReferralDetails()
        Dim dt As DataTable = Nothing
        Try
            dt = objReferralsDBLayer.FetchReferralsForUpdate(m_VisitId, m_PatientId, m_ExamId)

            ''''''''============------------
            '''''m_VisitId - 0
            '''''dtReferralDate - 1
            '''''ReferralToFrom - 2
            '''''bIsPCP - 3
            '''''ContactName - 4
            '''''Diagnosis - 5
            '''''NumberofVisit - 6 
            '''''TemplateID -7
            '''''Templatename -8
            '''''Template -9
            ''''''''============------------
            Dim ObjWord As New clsWordDocument
            Dim i As Integer
            Dim RefNote As String
            If Not IsNothing(dt) Then
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Try
                    For i = 0 To dt.Rows.Count - 1
                        'Check if entered Id is PCP
                        If i = 0 Then
                            ReferralDate = dt.Rows(i)("dtReferralDate")
                        End If
                        'Primary care Physician
                        Dim refNode As New myTreeNode
                        ' refNode.Text = dt.Rows(i)("ContactName")
                        refNode.Key = dt.Rows(i)("ReferralToFrom")
                        'refNode.Tag = dt.Rows(i)("TemplateId")
                        If IsDBNull(dt.Rows(i)("Template")) Then
                            refNode.TemplateResult = Nothing
                        ElseIf IsNothing(dt.Rows(i)("Template")) Then
                            refNode.TemplateResult = Nothing
                        Else
                            refNode.TemplateResult = dt.Rows(i)("Template")

                            RefNote = GetSavedRefLetter(myLoadWord, dt.Rows(i)("Template"))


                            refNode.ReferralLetter = CType(ObjWord.ConvertFiletoBinary(RefNote), Object)
                        End If
                        refNode.ImageIndex = 3
                        refNode.SelectedImageIndex = 3

                        'refNode.NodeName = GetUniqueKey()
                        Dim strRefletter As String = ""
                        ''Start :: Added the 'and' Condition if the Template is Blank
                        If Not IsDBNull(dt.Rows(i)("TemplateName")) AndAlso (dt.Rows(i)("TemplateName") <> "") Then
                            'strRefletter = objReferralsDBLayer.GetReferralLetter(dt.Rows(i)("TemplateId"))
                            strRefletter = dt.Rows(i)("TemplateName").ToString()
                            refNode.DMTemplateName = dt.Rows(i)("TemplateName").ToString()
                        End If
                        ''End  :: Added the 'and' Condition if the Template is Blank

                        'If strRefletter = "" Then
                        '    strRefletter = cmbTemplate.Text
                        'End If
                        If strRefletter = "" Then
                            refNode.Text = dt.Rows(i)("ContactName")

                        Else
                            refNode.Text = dt.Rows(i)("ContactName") & " - " & strRefletter

                        End If

                        ''''Fill Contacts other Info
                        myNodeList = New myList
                        If Not IsNothing(arrlist) Then
                            arrlist = Nothing
                        End If
                        arrlist = New ArrayList
                        'myNodeList.ContactID = dt.Rows(i)("nContactID")
                        myNodeList.ContactName = dt.Rows(i)("ContactName")
                        myNodeList.ContactFlag = dt.Rows(i)("nContactFlag")
                        myNodeList.ContactFirstName = dt.Rows(i)("FirstName")
                        myNodeList.ContactMiddleName = dt.Rows(i)("MiddleName")
                        myNodeList.ContactLastName = dt.Rows(i)("LastName")
                        myNodeList.ContactGender = dt.Rows(i)("Gender")
                        myNodeList.ContactDegree = dt.Rows(i)("Degree")
                        myNodeList.ContactAddressLine1 = dt.Rows(i)("Street")
                        myNodeList.ContactAddressLine2 = dt.Rows(i)("AddressLine2")
                        myNodeList.ContactCity = dt.Rows(i)("City")
                        myNodeList.ContactState = dt.Rows(i)("State")
                        myNodeList.ContactZip = dt.Rows(i)("ZIP")
                        myNodeList.ContactPhone = dt.Rows(i)("Phone")
                        myNodeList.ContactFax = dt.Rows(i)("Fax")
                        myNodeList.ContactMobile = dt.Rows(i)("Mobile")
                        myNodeList.ContactExternalCode = dt.Rows(i)("ExternalCode")
                        arrlist.Add(myNodeList)
                        myNodeList = Nothing     'Change made to solve memory Leak and word crash issue
                        'sarika Fax from Referrals 20081121
                        refNode.FaxReferralName = dt.Rows(i)("ContactName")
                        refNode.NodeName = dt.Rows(i)("ContactName")
                        refNode.arrRefferalDetails = arrlist
                        arrlist = Nothing     'Change made to solve memory Leak and word crash issue
                        '---
                        ''Start :: Added the If Condition if the template name is null or blank
                        If Not IsDBNull(dt.Rows(i)("TemplateName")) AndAlso (dt.Rows(i)("TemplateName") <> "") Then
                            If CType(dt.Rows(i)("nContactFlag"), Int16) = 2 Then
                                'check if templateID is null
                                If Not IsDBNull(dt.Rows(i)("TemplateID")) Then
                                    refNode.Tag = dt.Rows(i)("TemplateId")
                                    trPatientReferrals.Nodes(0).Nodes(0).Nodes.Add(refNode)

                                    'if null add default templateid 
                                Else
                                    refNode.Tag = CType(cmbTemplate.SelectedValue, System.Int64)
                                    trPatientReferrals.Nodes(0).Nodes(0).Nodes.Add(refNode)
                                End If
                                'Referrals(other than PCP)
                            ElseIf CType(dt.Rows(i)("nContactFlag"), Int16) = 3 Then
                                'check if templateid is null
                                If Not dt.Rows(i).IsNull("TemplateID") Then
                                    refNode.Tag = dt.Rows(i)("TemplateId")
                                    trPatientReferrals.Nodes(0).Nodes(1).Nodes.Add(refNode)
                                    'if null add default template id
                                Else
                                    refNode.Tag = CType(cmbTemplate.SelectedValue, System.Int64)
                                    trPatientReferrals.Nodes(0).Nodes(1).Nodes.Add(refNode)
                                End If
                            ElseIf CType(dt.Rows(i)("nContactFlag"), Int16) = 4 Then
                                If Not dt.Rows(i).IsNull("TemplateID") Then
                                    refNode.Tag = dt.Rows(i)("TemplateId")
                                    trPatientReferrals.Nodes(0).Nodes(2).Nodes.Add(refNode)
                                    'if null add default template id
                                Else
                                    refNode.Tag = CType(cmbTemplate.SelectedValue, System.Int64)
                                    trPatientReferrals.Nodes(0).Nodes(2).Nodes.Add(refNode)
                                End If
                            End If
                        End If
                        ''End :: Added the If Condition if the template name is null or blank
                        '  refNode.Dispose() : 
                        refNode = Nothing     'Change made to solve memory Leak and word crash issue
                    Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
            End If
            ObjWord = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not dt Is Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub
    Private Function GetSavedRefLetter(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal Template As Object) As String
        Dim strRefFileName As String
        Dim strFullNote As String
        Dim strFileName As String


        ObjWord = New clsWordDocument
        strFileName = ExamNewDocumentName
        strFileName = ObjWord.GenerateFile(Template, strFileName)

        'wdTemp = New AxDSOFramer.AxFramerControl
        'Me.Controls.Add(wdTemp)
        ''Bug #71063: #00000729: Refferal Letter
        'wdTemp.Location = New System.Drawing.Point(-50, -50)
        'wdTemp.Open(strFileName)
        'oCurDoc = wdTemp.ActiveDocument
        Dim oCurDoc As Wd.Document = myLoadWord.LoadWordApplication(strFileName)

        oCurDoc.ActiveWindow.View.ShowFieldCodes = False


        If Not oCurDoc Is Nothing AndAlso blnRb Then
            blnRb = False
            Dim strFile As String = NotesFileName
            Dim ExamRange As Wd.Range
            oCurDoc.ActiveWindow.SetFocus()
            oCurDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
            Dim notExisitngcnt As Int32 = 1 'SLR initialized to 1
            For j As Int32 = 1 To oCurDoc.Bookmarks.Count + 1
                If oCurDoc.Bookmarks.Exists("BMRL" & j.ToString) = False Then
                    notExisitngcnt = j
                    Exit For
                End If
            Next
            Dim cnt As Int32 = notExisitngcnt - 1
            If cnt = 2 Then ''If Bookmarks are available then
                ExamRange = oCurDoc.Range(oCurDoc.Bookmarks("BMRL1").End, oCurDoc.Bookmarks("BMRL2").Start)
                ExamRange.Select()
                ' ExamRange.Cut()
                ExamRange.Delete()
                If strFile <> "" AndAlso File.Exists(strFile) Then
                    RemoveAllBookMarks(oCurDoc)
                    oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                    AddBookMark(oCurDoc)
                    oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                    oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                    oCurDoc.ActiveWindow.Selection.InsertFile(strFile)
                    oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                    AddBookMark(oCurDoc)
                    UpdateVoiceLog("Exam Inserted in InsertExamNotes Method")
                ElseIf rbNone.Checked = True Then
                    RemoveAllBookMarks(oCurDoc)
                End If
                ExamRange = Nothing

            Else
                If strFile <> "" AndAlso File.Exists(strFile) Then
                    RemoveAllBookMarks(oCurDoc)
                    oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                    AddBookMark(oCurDoc)
                    oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                    oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                    oCurDoc.ActiveWindow.Selection.InsertFile(strFile)
                    oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                    AddBookMark(oCurDoc)
                    UpdateVoiceLog("Exam Inserted  for No Book Marks in InsertExamNotes Method")
                End If
            End If

        End If
        strFullNote = ExamNewDocumentName
        oCurDoc.SaveAs(strFullNote, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
        'wdTemp.Close()
        'Me.Controls.Remove(wdTemp)
        'wdTemp.Dispose()
        'wdTemp = Nothing

        'oCurDoc = Nothing
        myLoadWord.CloseWordOnly(oCurDoc)
        strRefFileName = GenerateReferralLetter(myLoadWord, strFullNote)

        Return strRefFileName
    End Function
    Private Sub OpenReffrelLetter(ByVal ReferralID As Long)
        Dim dt As DataTable = Nothing
        Try
            dt = objReferralsDBLayer.FetchReferralsForUpdate(m_VisitId, m_PatientId, m_ExamId)
            ''''''''============------------
            '''''m_VisitId - 0
            '''''dtReferralDate - 1
            '''''ReferralToFrom - 2
            '''''bIsPCP - 3
            '''''ContactName - 4
            '''''Diagnosis - 5
            '''''NumberofVisit - 6 
            '''''TemplateID -7
            '''''Templatename -8
            '''''Template -9
            ''''''''============------------
            'dt.DefaultView.RowFilter = "bIsPCP=true"
            Dim i As Integer
            If Not IsNothing(dt) Then
                For i = 0 To dt.Rows.Count - 1

                    'Check if entered Id is PCP
                    If i = 0 Then
                        ReferralDate = dt.Rows(i)("dtReferralDate")
                    End If
                    'Primary care Physician
                    Dim refNode As New myTreeNode
                    ' refNode.Text = dt.Rows(i)("ContactName")
                    refNode.Key = dt.Rows(i)("ReferralToFrom")
                    'refNode.Tag = dt.Rows(i)("TemplateId")
                    If IsDBNull(dt.Rows(i)("Template")) Then
                        refNode.TemplateResult = Nothing
                    ElseIf IsNothing(dt.Rows(i)("Template")) Then
                        refNode.TemplateResult = Nothing
                    Else
                        refNode.TemplateResult = dt.Rows(i)("Template")
                    End If
                    refNode.ImageIndex = 3
                    refNode.SelectedImageIndex = 3

                    'refNode.NodeName = GetUniqueKey()
                    Dim strRefletter As String = ""
                    If Not IsDBNull(dt.Rows(i)("TemplateName")) Then
                        'strRefletter = objReferralsDBLayer.GetReferralLetter(dt.Rows(i)("TemplateId"))
                        strRefletter = dt.Rows(i)("TemplateName").ToString()
                        refNode.DMTemplateName = dt.Rows(i)("TemplateName").ToString()
                    End If
                    'If strRefletter = "" Then
                    '    strRefletter = cmbTemplate.Text
                    'End If
                    If strRefletter = "" Then
                        refNode.Text = dt.Rows(i)("ContactName")

                    Else
                        refNode.Text = dt.Rows(i)("ContactName") & " - " & strRefletter

                    End If

                    ''''Fill Contacts other Info
                    myNodeList = New myList
                    If Not IsNothing(arrlist) Then
                        arrlist = Nothing
                    End If
                    arrlist = New ArrayList
                    'myNodeList.ContactID = dt.Rows(i)("nContactID")
                    myNodeList.ContactName = dt.Rows(i)("ContactName")
                    myNodeList.ContactFirstName = dt.Rows(i)("FirstName")
                    myNodeList.ContactMiddleName = dt.Rows(i)("MiddleName")
                    myNodeList.ContactLastName = dt.Rows(i)("LastName")
                    myNodeList.ContactGender = dt.Rows(i)("Gender")
                    myNodeList.ContactDegree = dt.Rows(i)("Degree")
                    myNodeList.ContactAddressLine1 = dt.Rows(i)("Street")
                    myNodeList.ContactAddressLine2 = dt.Rows(i)("AddressLine2")
                    myNodeList.ContactCity = dt.Rows(i)("City")
                    myNodeList.ContactState = dt.Rows(i)("State")
                    myNodeList.ContactZip = dt.Rows(i)("ZIP")
                    myNodeList.ContactPhone = dt.Rows(i)("Phone")
                    myNodeList.ContactFax = dt.Rows(i)("Fax")
                    myNodeList.ContactMobile = dt.Rows(i)("Mobile")
                    myNodeList.ContactExternalCode = dt.Rows(i)("ExternalCode")
                    arrlist.Add(myNodeList)
                    myNodeList = Nothing    'Change made to solve memory Leak and word crash issue
                    'sarika Fax from Referrals 20081121
                    refNode.FaxReferralName = dt.Rows(i)("ContactName")
                    refNode.NodeName = dt.Rows(i)("ContactName")
                    refNode.arrRefferalDetails = arrlist
                    arrlist = Nothing   'Change made to solve memory Leak and word crash issue
                    '---
                    Dim refid As Long = dt.Rows(i)("ReferralID")
                    If CType(dt.Rows(i)("bIsPCP"), Boolean) = True Then
                        'check if templateID is null
                        If (ReferralID = refid) Then
                            If Not IsDBNull(dt.Rows(i)("TemplateID")) Then
                                refNode.Tag = dt.Rows(i)("TemplateId")
                                trPatientReferrals.Nodes(0).Nodes(0).Nodes.Add(refNode)

                                'if null add default templateid 
                            Else
                                refNode.Tag = CType(cmbTemplate.SelectedValue, System.Int64)
                                trPatientReferrals.Nodes(0).Nodes(0).Nodes.Add(refNode)
                            End If
                            LoadDocument(refNode)
                        End If

                    Else
                        'check if templateid is null

                        If (ReferralID = refid) Then
                            If Not dt.Rows(i).IsNull("TemplateID") Then
                                refNode.Tag = dt.Rows(i)("TemplateId")
                                trPatientReferrals.Nodes(0).Nodes(1).Nodes.Add(refNode)
                                'if null add default template id
                            Else
                                refNode.Tag = CType(cmbTemplate.SelectedValue, System.Int64)
                                trPatientReferrals.Nodes(0).Nodes(1).Nodes.Add(refNode)
                            End If

                            '' trPatientReferrals.Nodes(0).Nodes(1).Nodes.Add(refNode)
                            LoadDocument(refNode)
                        End If
                    End If
                    '  refNode.Dispose() : 
                    refNode = Nothing   'Change made to solve memory Leak and word crash issue
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not dt Is Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub
    Public Sub SaveReferrals(Optional ByRef ReferralArrlist As ArrayList = Nothing) '''''''' Made as Public function for Smart Diagnosis Changes
        'Get node count of child nodes in trICD9Associates
        Try
            If trPatientReferrals.Nodes.Item(0).GetNodeCount(True) > 2 Then
                If Not oCurDoc Is Nothing Then
                    Dim strFileName As String
                    strFileName = ExamNewDocumentName
                    ' wdReferrals.Save(strFileName, True, "", "")
                    oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    wdReferrals.Close()
                    SaveTreeNode(strSelectNode, strFileName)
                End If

                Dim arrlist As New ArrayList
                UpdateVoiceLog("Calling PopulateArrlist(object) Procedure in SaveReferrals")
                ''If (_referralID = 0) Then
                ''Sandip Darade 20100211

                PopulateArrlist(arrlist)


                ''End If
                If arrlist.Count > 0 Then
                    '' If Referrals are not assigned during Patient Registration
                    If Savestatus = 1 Then
                        'Check for duplicate Referral Entry
                        If Not objReferralsDBLayer.CheckReferral(m_VisitId, m_ExamId, m_PatientId) Then
                            If MessageBox.Show("Referrals Already Exists ,Do you want to Replace Existing Referrals", "Patient Referrals", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                                objReferralsDBLayer.AddData(arrlist, m_VisitId, ReferralDate, m_PatientId, 2, _referralID)
                                If Not myCaller Is Nothing Then
                                    frmPatientExam.blnRefChangesMade = True
                                End If

                            Else
                                Exit Sub
                            End If
                        Else
                            objReferralsDBLayer.AddData(arrlist, m_VisitId, ReferralDate, m_PatientId, m_ExamId, Savestatus, _referralID)
                            If Not myCaller Is Nothing Then
                                frmPatientExam.blnRefChangesMade = True
                            End If
                        End If
                    Else
                        '' If Referrals are assigned during Patient Registration
                        objReferralsDBLayer.AddData(arrlist, m_VisitId, ReferralDate, m_PatientId, m_ExamId, Savestatus, _referralID)
                        If Not myCaller Is Nothing Then
                            frmPatientExam.blnRefChangesMade = True
                        End If
                    End If

                    'If Not IsNothing(ReferralArrlist) Then
                    ReferralArrlist = arrlist
                    'End If

                    If (blnIsSingleFaxorPrint = True) Then
                        arrlist.Clear()
                        PopulateArrlist_forSinglecontact(arrlist)
                    End If

                    If Not IsNothing(gReferralArrlist) Then
                        gReferralArrlist.Clear()
                    End If
                    gReferralArrlist = arrlist
                    Savestatus = 2
                End If
                'RefreshPatientReferrals()
                arrlist = Nothing   'Change made to solve memory Leak and word crash issue
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub PopulateArrlist(ByRef Arrlist As ArrayList)

        Dim i As Integer
        Try
            _AccountID = 0
            Dim ObjWord = New clsWordDocument
            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            Try
                For i = 0 To trPatientReferrals.Nodes.Item(0).GetNodeCount(False) - 1
                    Dim ReferralNode As myTreeNode
                    'get the ICD9Node associated sequentially
                    ReferralNode = trPatientReferrals.Nodes(0).Nodes(i)
                    'trPatientReferrals.Nodes(0).Nodes(i).Text
                    If ReferralNode.GetNodeCount(True) > 0 Then
                        Dim j As Integer

                        For j = 0 To ReferralNode.GetNodeCount(False) - 1
                            Dim myNode As myTreeNode
                            myNode = ReferralNode.Nodes.Item(j)

                            If IsNothing(myNode.TemplateResult) Then
                                If IsNothing(myNode.Tag) Then
                                    myNode.Tag = TemplateId
                                ElseIf CType(myNode.Tag, System.Int64) = 0 Then
                                    myNode.Tag = TemplateId
                                End If
                                myNode.TemplateResult = GetTemplate(myLoadWord, myNode.Tag, myNode.Key, _AccountID)
                                '''' Bind Referral letter to send for secure message
                                myNode.ReferralLetter = CType(ObjWord.ConvertFiletoBinary(strRefFileName), Object)
                            ElseIf IsDBNull(myNode.TemplateResult) Then
                                If IsNothing(myNode.Tag) Then
                                    myNode.Tag = TemplateId
                                ElseIf CType(myNode.Tag, System.Int64) = 0 Then
                                    myNode.Tag = TemplateId
                                End If
                                myNode.TemplateResult = GetTemplate(myLoadWord, myNode.Tag, myNode.Key, _AccountID)
                                '''' Bind Referral letter to send for secure message
                                myNode.ReferralLetter = CType(ObjWord.ConvertFiletoBinary(strRefFileName), Object)
                            End If

                            Dim lst As New myList
                            If i = 0 Then
                                'need to save templateid stored in mynode.tag
                                'against every referral entry
                                If bIsRefferallettersave Then
                                    lst.ID = ddTemplate.SelectedValue
                                Else
                                    lst.ID = myNode.Tag  'TemplateID
                                End If

                                lst.Index = myNode.Key 'ReferralID
                                'sarika Fax from Referrals 20081121
                                '   lst.Description = myNode.Text '' ReferralName
                                lst.Description = myNode.FaxReferralName

                                '---
                                lst.Type = True
                                lst.ContactFlag = 2
                                'sarika referral letter 20081125
                                lst.ReferralLetterName = myNode.FaxReferralLetter
                                lst.TemplateResult = myNode.TemplateResult '' Template(Object)
                                lst.ContactTemplateName = myNode.DMTemplateName
                                lst.ReferralLetter = myNode.ReferralLetter
                                '''' Fill Contact details of the contact
                                FillList(lst, myNode)

                                Arrlist.Add(lst)
                            Else
                                'need to save templateid stored in mynode.tag
                                'against every referral entry

                                If bIsRefferallettersave Then
                                    lst.ID = ddTemplate.SelectedValue
                                Else
                                    lst.ID = myNode.Tag  'TemplateID
                                End If
                                'lst.ID = myNode.Tag
                                lst.Index = myNode.Key

                                'sarika Fax from Referrals 20081121
                                '   lst.Description = myNode.Text '' ReferralName
                                lst.Description = myNode.FaxReferralName

                                '---
                                If (i = 1) Then
                                    lst.ContactFlag = 3
                                ElseIf (i = 2) Then
                                    lst.ContactFlag = 4
                                End If
                                lst.Type = False
                                'sarika referral letter 20081125
                                lst.ReferralLetterName = myNode.FaxReferralLetter
                                lst.TemplateResult = myNode.TemplateResult
                                lst.ContactTemplateName = myNode.DMTemplateName
                                lst.ReferralLetter = myNode.ReferralLetter
                                lst.DMTemplateName = myNode.DMTemplateName
                                '''' Fill Contact details of the contact
                                FillList(lst, myNode)
                                ''Code Added by Mayuri:20091215-
                                ''To fix issue-#2197-If the practice chooses the same provider for both referring physician and the
                                ''PCP then the system sends out two faxes.
                                If trPatientReferrals.Nodes(0).Nodes(0).Nodes.Count > 0 Then
                                    If ((trPatientReferrals.Nodes(0).Nodes(0).Nodes(0).Text) = (myNode.Text)) Then
                                    Else
                                        Arrlist.Add(lst)
                                    End If
                                Else
                                    Arrlist.Add(lst)
                                End If
                            End If
                            'Change made to solve memory Leak and word crash issue
                            If Not lst Is Nothing Then lst = Nothing
                            'If Not myNode Is Nothing Then myNode.Dispose() :
                            myNode = Nothing
                        Next
                    End If
                    'If Not ReferralNode Is Nothing Then ReferralNode.Dispose() : 
                    ReferralNode = Nothing 'Change made to solve memory Leak and word crash issue
                Next
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing
            ObjWord = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub


    Private Sub PopulateArrlist_forSinglecontact(ByRef Arrlist As ArrayList)
        Try
            If (trPatientReferrals.SelectedNode.Level <> 2) Then
                Exit Sub
            End If
            Dim myNode As myTreeNode
            myNode = trPatientReferrals.SelectedNode
            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            Try
                If IsNothing(myNode.TemplateResult) Then
                    If IsNothing(myNode.Tag) Then
                        myNode.Tag = TemplateId
                    ElseIf CType(myNode.Tag, System.Int64) = 0 Then
                        myNode.Tag = TemplateId
                    End If
                    myNode.TemplateResult = GetTemplate(myLoadWord, myNode.Tag, myNode.Key)

                ElseIf IsDBNull(myNode.TemplateResult) Then
                    If IsNothing(myNode.Tag) Then
                        myNode.Tag = TemplateId
                    ElseIf CType(myNode.Tag, System.Int64) = 0 Then
                        myNode.Tag = TemplateId
                    End If
                    myNode.TemplateResult = GetTemplate(myLoadWord, myNode.Tag, myNode.Key)
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing
            Dim lst As New myList
            'If i = 0 Then
            'need to save templateid stored in mynode.tag
            'against every referral entry
            If bIsRefferallettersave Then
                lst.ID = ddTemplate.SelectedValue
            Else
                lst.ID = myNode.Tag  'TemplateID
            End If

            lst.Index = myNode.Key 'ReferralID
            'sarika Fax from Referrals 20081121
            '   lst.Description = myNode.Text '' ReferralName
            lst.Description = myNode.FaxReferralName

            '---
            lst.Type = True
            'sarika referral letter 20081125
            lst.ReferralLetterName = myNode.FaxReferralLetter
            lst.TemplateResult = myNode.TemplateResult '' Template(Object)
            lst.ContactTemplateName = myNode.DMTemplateName

            '''' Fill Contact details of the contact
            FillList(lst, myNode)

            Arrlist.Add(lst)
            'Change made to solve memory Leak and word crash issue
            lst = Nothing
            '   If Not myNode Is Nothing Then myNode.Dispose() : 
            myNode = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub FillList(ByVal _lst As myList, ByVal _mynode As myTreeNode)

        If Not IsNothing(_mynode.arrRefferalDetails) Then
            Dim arrlist As ArrayList = CType(_mynode.arrRefferalDetails, ArrayList)

            If (arrlist.Count > 0) Then
                Dim i As Integer = arrlist.Count - 1

                '_lst.ContactID = ctype(arrlist,myList).
                _lst.ContactName = CType(arrlist(i), myList).ContactName
                _lst.ContactFirstName = CType(arrlist(i), myList).ContactFirstName
                _lst.ContactMiddleName = CType(arrlist(i), myList).ContactMiddleName
                _lst.ContactLastName = CType(arrlist(i), myList).ContactLastName
                _lst.ContactGender = CType(arrlist(i), myList).ContactGender
                _lst.ContactDegree = CType(arrlist(i), myList).ContactDegree
                _lst.ContactAddressLine1 = CType(arrlist(i), myList).ContactAddressLine1
                _lst.ContactAddressLine2 = CType(arrlist(i), myList).ContactAddressLine2
                _lst.ContactCity = CType(arrlist(i), myList).ContactCity
                _lst.ContactState = CType(arrlist(i), myList).ContactState
                _lst.ContactZip = CType(arrlist(i), myList).ContactZip
                _lst.ContactPhone = CType(arrlist(i), myList).ContactPhone
                _lst.ContactFax = CType(arrlist(i), myList).ContactFax
                _lst.ContactMobile = CType(arrlist(i), myList).ContactMobile
                _lst.ContactExternalCode = CType(arrlist(i), myList).ContactExternalCode
                '_lst.ContactTemplateName = CType(arrlist(i), myList).ContactTemplateName
            End If
            arrlist = Nothing   'Change made to solve memory Leak and word crash issue
        End If

    End Sub

    Private Function GetTemplate(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal TemplateID As Long, ByVal ReferralID As Long, Optional ByVal AccountID As Int64 = 0) As Object
        Dim oSetting As New gloSettings.GeneralSettings(GetConnectionString)         'CAS-19703-K4N5K5 – Some finished exams will have some parts missing when faxing with a cover page/Referral Letter.
        Try
            UpdateVoiceLog("GetTemplate started")

            oSetting.GetSetting("InsertReferralLetterFirstForFax", InsertReferralLetterFirstForFax)

            Dim strReferalletter As String
            Dim strFileName As String
            Dim IsFormHide As Boolean = False
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Template
            objCriteria.PrimaryID = TemplateID

            ObjWord.DocumentCriteria = objCriteria
            '' Retrieve the Template from DB
            strFileName = ObjWord.RetrieveDocumentFile()
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
            UpdateVoiceLog("RetrieveDocumentFile completed")

            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Referrals
            objCriteria.PatientID = m_PatientId
            objCriteria.VisitID = m_VisitId
            objCriteria.PrimaryID = ReferralID
            ObjWord.DocumentCriteria = objCriteria



            'If Me.Visible = False Then
            '    Me.Visible = True
            '    IsFormHide = True
            'End If
            ''wdTemp.Open(strFileName)

            'If IsFormHide = True Then
            '    Me.Visible = False
            '    IsFormHide = False
            'End If
            'oCurDoc = wdTemp.ActiveDocument
            If (IsNothing(strFileName) = False) Then


                Dim oCurDoc As Wd.Document = myLoadWord.LoadWordApplication(strFileName)


                'Added Code against Bug #79824: gloEMR: Referral Letter- Application does not print associated data against referrals 
                ObjWord.CurDocument = oCurDoc
                ObjWord.GetFormFieldData(enumDocType.None, "", _AccountID)

                _AccountID = ObjWord.DocumentCriteria.FieldID1
                oCurDoc = ObjWord.CurDocument

                strRefFileName = ExamNewDocumentName
                oCurDoc.SaveAs(strRefFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                Dim strNotesFile As String = SelectNotesFile()
                Dim objTemplate As Object = ""

                If InsertReferralLetterFirstForFax = True Then      'if settings(InsertReferralLetterFirstForFax)='True' then Regular code  
                    If strNotesFile <> "" Then
                        If File.Exists(strNotesFile) Then
                            UpdateVoiceLog("Inserting Exam in GetTemplate ")
                            oCurDoc.ActiveWindow.SetFocus()
                            RemoveAllBookMarks(oCurDoc)
                            oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                            AddBookMark(oCurDoc)
                            oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                            oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                            oCurDoc.ActiveWindow.Selection.InsertFile(strNotesFile)
                            oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                            AddBookMark(oCurDoc)
                            UpdateVoiceLog("Exam Inserted in GetTemplate")
                        End If
                    End If

                    oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

                    myLoadWord.CloseWordOnly(oCurDoc)

                    objTemplate = CType(ObjWord.ConvertFiletoBinary(strFileName), Object)
                    UpdateVoiceLog("GetTemplate completed")
                Else
                    strReferalletter = ExamNewDocumentName      'if settings(InsertReferralLetterFirstForFax)='False' then New code  
                    If strNotesFile <> "" Then
                        If File.Exists(strNotesFile) Then
                            'oCurDoc.Save()
                            oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                            myLoadWord.CloseWordOnly(oCurDoc)

                            FileCopy(strNotesFile, strReferalletter)
                            oCurDoc = myLoadWord.LoadWordApplication(strReferalletter)
                            oCurDoc.ActiveWindow.SetFocus()
                            RemoveAllBookMarks(oCurDoc)
                            oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                            AddBookMark(oCurDoc)
                            oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
                            oCurDoc.ActiveWindow.Selection.InsertFile(strFileName)
                            oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                            AddBookMark(oCurDoc)
                        End If
                    End If

                    oCurDoc.SaveAs(strReferalletter, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

                    myLoadWord.CloseWordOnly(oCurDoc)

                    objTemplate = CType(ObjWord.ConvertFiletoBinary(strReferalletter), Object)
                    UpdateVoiceLog("GetTemplate completed")
                End If

                Return objTemplate
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not ObjWord Is Nothing Then
                ObjWord = Nothing
            End If
            If Not objCriteria Is Nothing Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If
            If (IsNothing(oSetting) = False) Then
                oSetting.Dispose() : oSetting = Nothing
            End If
        End Try
    End Function

    Private Sub RefreshPatientReferrals()
        trPatientReferrals.Nodes.Item(0).Nodes.Item(0).Nodes.Clear()
        trPatientReferrals.Nodes.Item(0).Nodes.Item(1).Nodes.Clear()
        trPatientReferrals.Nodes.Item(0).Nodes.Item(2).Nodes.Clear()
        FillPatientReferrals()
    End Sub

    Private Sub trReferrals_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trReferrals.ItemDrag
        'Set the drag node and initiate the DragDrop
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub trReferrals_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trReferrals.DragEnter
        'See if there is a TreeNode being dragged
        If e.Data.GetDataPresent("gloEMR.myTreeNode", True) Then
            'TreeNode found allow move effect
            e.Effect = DragDropEffects.Move
        Else
            'No TreeNode found, prevent move
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub trPatientReferrals_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trPatientReferrals.DragOver
        'Check that there is a TreeNode being dragged
        'commented on 30/8/2005 If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
        If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedTreeview As TreeView = CType(sender, TreeView)

        'As the mouse moves over nodes, provide feedback to the user
        'by highlighting the node that is the current drop target
        Dim pt As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))

        'commented on 30/8/2005 Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)
        Dim targetNode As myTreeNode = selectedTreeview.GetNodeAt(pt)

        'See if the targetNode is currently selected, if so no need to validate again
        If Not (selectedTreeview Is targetNode) Then
            'Select the node currently under the cursor
            selectedTreeview.SelectedNode = targetNode

            'Check that the selected node is not the dropNode and also that it
            'is not a child of the dropNode and therefore an invalid target
            Dim dropNode As TreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)
            Do Until targetNode Is Nothing
                If targetNode Is dropNode Then
                    e.Effect = DragDropEffects.None
                    Exit Sub
                End If
                targetNode = targetNode.Parent
            Loop
            dropNode = Nothing    'Change made to solve memory Leak and word crash issue
        End If

        'Currently selected node is a suitable target, allow the move
        e.Effect = DragDropEffects.Move
        targetNode = Nothing    'Change made to solve memory Leak and word crash issue

    End Sub

    Private Sub trReferrals_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trReferrals.DragDrop, trPatientReferrals.DragDrop
        Try
            'Check that there is a TreeNode being dragged

            'commented on 30/08/2005 If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
            If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

            'Get the TreeView raising the event (incase multiple on form)
            Dim selectedTreeview As TreeView = CType(sender, TreeView)

            'Get the TreeNode being dragged
            'commented on 30/08/2005 Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            Dim dropNode As myTreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)

            'The target node should be selected from the DragOver event

            'commented on 30/08/2005 Dim targetNode As TreeNode = selectedTreeview.SelectedNode

            Dim targetNode As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)

            If Not dropNode Is trReferrals.Nodes.Item(0) Then
                AddNode(dropNode, targetNode)
                Call ResetSearch()
            End If
            'Change made to solve memory Leak and word crash issue

            ' If Not dropNode Is Nothing Then dropNode.Dispose() : 
            dropNode = Nothing

            targetNode = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Fax, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub AddNode(ByVal myNode As myTreeNode, ByVal TargetNode As myTreeNode)
        Try
            If Not IsNothing(myNode) AndAlso Not IsNothing(TargetNode) Then
                If myNode.TreeView Is trReferrals Then
                    Dim strContactName As String
                    strContactName = myNode.NodeName

                    If Not TargetNode Is trPatientReferrals.Nodes.Item(0) Then
                        'check if node is PCP or Referrals
                        Dim strRefLetter As String
                        If Trim(gstrLoginProviderName) <> "All" AndAlso Trim(gstrLoginProviderName) <> "" Then
                            strRefLetter = objReferralsDBLayer.DefaultReferralLetter(gstrLoginProviderName)
                        Else
                            strRefLetter = objReferralsDBLayer.GetReferralLetter(TemplateId)
                        End If

                        If TargetNode.Parent Is trPatientReferrals.Nodes.Item(0) Then
                            Dim myTargetNode As myTreeNode = Nothing
                            For Each myTargetNode In TargetNode.Nodes
                                If myTargetNode.NodeName = strContactName Then
                                    Exit Sub
                                End If
                            Next

                            ' If Not myTargetNode Is Nothing Then myTargetNode.Dispose() : 
                            myTargetNode = Nothing 'Change made to solve memory Leak and word crash issue

                            'dropNode.Remove()

                            Dim associatenode As myTreeNode

                            associatenode = myNode.Clone
                            associatenode.Text = myNode.Text

                            '  associatenode.Text = myNode.Text
                            ''''Pramod
                            'associatenode.NodeName = GetUniqueKey()
                            If Not IsNothing(cmbTemplate.Items.Item(0)) Then
                                'while adding new referral add default templateid
                                associatenode.Tag = TemplateId
                                associatenode.TemplateResult = Nothing

                                'associatenode.NodeName = cmbTemplate.Text
                            End If
                            associatenode.Text = myNode.Text & " - " & strRefLetter
                            associatenode.FaxReferralName = myNode.Text
                            associatenode.NodeName = myNode.Text
                            associatenode.DMTemplateName = strRefLetter
                            associatenode.arrRefferalDetails = myNode.arrRefferalDetails
                            associatenode.Key = myNode.Key  '' ContactID
                            associatenode.ForeColor = TargetNode.ForeColor
                            If CType(TargetNode, myTreeNode).Text = "Primary Care Physician" Then
                                TargetNode.Nodes.Clear()
                            End If
                            associatenode.arrRefferalDetails = myNode.arrRefferalDetails
                            TargetNode.Nodes.Add(associatenode)
                            cmbTemplate.SelectedValue = TemplateId
                            'treeindex = -1
                            'End If

                            'Ensure the newley created node is visible to the user and select it
                            associatenode.EnsureVisible()
                            trPatientReferrals.ExpandAll()
                            trPatientReferrals.SelectedNode = associatenode

                            ' If Not associatenode Is Nothing Then associatenode.Dispose() : 
                            associatenode = Nothing 'Change made to solve memory Leak and word crash issue

                        Else
                            Dim myTargetNode As myTreeNode = Nothing
                            For Each myTargetNode In TargetNode.Parent.Nodes
                                'If myTargetNode.Key = intKey Then
                                If myTargetNode.NodeName = strContactName Then
                                    Exit Sub
                                End If
                            Next

                            '  If Not myTargetNode Is Nothing Then myTargetNode.Dispose() : 
                            myTargetNode = Nothing 'Change made to solve memory Leak and word crash issue

                            'dropNode.Remove()
                            Dim associatenode As myTreeNode

                            associatenode = myNode.Clone
                            associatenode.Key = myNode.Key
                            ' associatenode.Text = myNode.Text
                            'Pramod
                            'associatenode.NodeName = GetUniqueKey()

                            If Not IsNothing(cmbTemplate.Items.Item(0)) Then
                                'while adding new referral add default templateid
                                associatenode.Tag = TemplateId
                                associatenode.TemplateResult = Nothing
                                'associatenode.NodeName = cmbTemplate.Text
                            End If
                            associatenode.Text = myNode.Text & " - " & strRefLetter
                            associatenode.NodeName = myNode.Text
                            associatenode.FaxReferralName = myNode.Text
                            associatenode.arrRefferalDetails = myNode.arrRefferalDetails
                            associatenode.ForeColor = TargetNode.Parent.ForeColor
                            associatenode.DMTemplateName = strRefLetter
                            associatenode.Key = myNode.Key
                            associatenode.arrRefferalDetails = myNode.arrRefferalDetails
                            If CType(TargetNode.Parent, myTreeNode).Text = "Primary Care Physician" Then
                                TargetNode.Parent.Nodes.Clear()
                                trPatientReferrals.Nodes.Item(0).Nodes.Item(0).Nodes.Add(associatenode)
                            Else
                                TargetNode.Parent.Nodes.Add(associatenode)
                            End If
                            'Savestatus = 1
                            cmbTemplate.SelectedValue = TemplateId
                            associatenode.EnsureVisible()
                            trPatientReferrals.ExpandAll()
                            trPatientReferrals.SelectedNode = associatenode

                            '  If Not associatenode Is Nothing Then associatenode.Dispose() : 
                            associatenode = Nothing 'Change made to solve memory Leak and word crash issue

                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub trReferrals_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trReferrals.DoubleClick
        Try
            If Not IsNothing(trReferrals.SelectedNode) Then
                If Not (trReferrals.SelectedNode Is trReferrals.Nodes.Item(0)) AndAlso Not (trPatientReferrals.SelectedNode Is trPatientReferrals.Nodes.Item(0)) Then
                    AddNode(trReferrals.SelectedNode, trPatientReferrals.SelectedNode)
                    Call ResetSearch()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub trReferrals_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trReferrals.NodeMouseClick

        Try
            trReferrals.SelectedNode = e.Node
            If e.Button = Windows.Forms.MouseButtons.Right Then

                If Not IsNothing(trReferrals.SelectedNode) Then
                    Dim oMenuItem As MenuItem

                    If trReferrals.Nodes(0) Is trReferrals.SelectedNode Then

                        CntReferrals.MenuItems.Clear()
                        'Try
                        '    If (IsNothing(trReferrals.ContextMenu) = False) Then
                        '        trReferrals.ContextMenu.Dispose()
                        '        trReferrals.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trReferrals.ContextMenu = CntReferrals
                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Add Referral"
                        End With
                        AddHandler oMenuItem.Click, AddressOf AddReferrals
                        CntReferrals.MenuItems.Add(oMenuItem)
                        oMenuItem = Nothing 'Change made to solve memory Leak and word crash issue

                    Else
                        CntReferrals.MenuItems.Clear()
                        'Try
                        '    If (IsNothing(trReferrals.ContextMenu) = False) Then
                        '        trReferrals.ContextMenu.Dispose()
                        '        trReferrals.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trReferrals.ContextMenu = CntReferrals
                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Add Referral"
                        End With
                        AddHandler oMenuItem.Click, AddressOf AddReferrals
                        CntReferrals.MenuItems.Add(oMenuItem)
                        oMenuItem = Nothing 'Change made to solve memory Leak and word crash issue
                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Edit Referral"
                        End With
                        AddHandler oMenuItem.Click, AddressOf EditReferrals

                        CntReferrals.MenuItems.Add(oMenuItem)
                        oMenuItem = Nothing 'Change made to solve memory Leak and word crash issue
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Public Sub SetMenus(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
        Try
            If oCurrentMenu.Shortcut = Shortcut.CtrlShiftD Then
                ReAssociateTemplate(oCurrentMenu.Text)
            ElseIf oCurrentMenu.Shortcut = Shortcut.CtrlShiftE Then
                Try
                    If Not trPatientReferrals.SelectedNode Is trPatientReferrals.Nodes.Item(0) Then
                        If trPatientReferrals.SelectedNode.Text <> trPatientReferrals.Nodes.Item(0).Nodes.Item(0).Text Then
                            If trPatientReferrals.SelectedNode.Text <> trPatientReferrals.Nodes.Item(0).Nodes.Item(1).Text Then
                                If trPatientReferrals.SelectedNode.Text <> trPatientReferrals.Nodes.Item(0).Nodes.Item(2).Text Then


                                    Dim mychildnode As myTreeNode
                                    Dim key As Int64 = 0
                                    mychildnode = CType(trPatientReferrals.SelectedNode, myTreeNode)
                                    If Not IsNothing(mychildnode) Then
                                        mychildnode.Remove()
                                        'delete from PatientReferrals treeview
                                        'Savestatus = 1
                                    End If

                                    If Not mychildnode Is Nothing Then mychildnode.Dispose() : mychildnode = Nothing 'Change made to solve memory Leak and word crash issue

                                    trPatientReferrals.SelectedNode = trPatientReferrals.Nodes.Item(0)
                                    trPatientReferrals.ContextMenu = Nothing
                                End If
                            End If
                        End If
                    End If
                Catch ex As SqlClient.SqlException
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ex = Nothing
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ex = Nothing
                End Try

            End If

        Catch oError As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, oError.ToString, gloAuditTrail.ActivityOutCome.Failure)
            oError = Nothing
        Finally
            oCurrentMenu = Nothing
        End Try
    End Sub

    Private Sub trReferrals_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trReferrals.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                If Not IsNothing(trReferrals.SelectedNode) Then
                    If Not (trReferrals.SelectedNode Is trReferrals.Nodes.Item(0)) AndAlso Not (trPatientReferrals.SelectedNode Is trPatientReferrals.Nodes.Item(0)) Then
                        AddNode(trReferrals.SelectedNode, trPatientReferrals.SelectedNode)
                    End If
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ex = Nothing
            End Try
        End If
    End Sub

    Private Sub txtSearchReferrals_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchReferrals.KeyPress
        'If (e.KeyChar = ChrW(13)) Then
        '    'trReferrals.Select()
        '    'Else
        '    '    trReferrals.SelectedNode = trReferrals.Nodes.Item(0)
        'End If
    End Sub

    Private Sub RefreshReferralLetters(ByVal arrlist As ArrayList)
        Dim lst As New myList

        For i As Integer = 0 To arrlist.Count - 1
            'lst = New myList
            lst = CType(arrlist(i), myList)
            For j As Integer = 0 To trPatientReferrals.Nodes(0).Nodes(0).GetNodeCount(False) - 1
                Dim node As myTreeNode
                node = CType(trPatientReferrals.Nodes(0).Nodes(0).Nodes(j), myTreeNode)
                If node.Tag = lst.ID AndAlso node.Key = lst.Index Then
                    node.TemplateResult = lst.TemplateResult
                    Exit For
                End If

                'node.Dispose() : node = Nothing 'Change made to solve memory Leak and word crash issue

            Next

            For j As Integer = 0 To trPatientReferrals.Nodes(0).Nodes(1).GetNodeCount(False) - 1
                Dim node As myTreeNode
                node = CType(trPatientReferrals.Nodes(0).Nodes(1).Nodes(j), myTreeNode)
                If node.Tag = lst.ID AndAlso node.Key = lst.Index Then
                    node.TemplateResult = lst.TemplateResult
                    Exit For
                End If

                'node.Dispose() : node = Nothing 'Change made to solve memory Leak and word crash issue

            Next
            lst = Nothing 'Change made to solve memory Leak and word crash issue
        Next
    End Sub

    Private Sub FAXAll()
        UpdateVoiceLog("In FAX All Procedure")
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim arrlist As New ArrayList
            If Savestatus = 2 Then
                UpdateVoiceLog("Calling SaveReferrals(object) Procedure")

                SaveReferrals(arrlist)

                'UpdateVoiceLog("Referrals Saved")
                If Not IsNothing(gReferralArrlist) Then
                    UpdateVoiceLog("Calling FAXAll(object) Procedure")

                    FAXAll(gReferralArrlist)
                End If
            ElseIf Savestatus = 1 Then
                If trPatientReferrals.Nodes.Item(0).GetNodeCount(True) > 2 Then
                    UpdateVoiceLog("Calling SaveReferrals(object) Procedure")

                    SaveReferrals(arrlist)

                    If arrlist.Count > 0 Then
                        UpdateVoiceLog("Calling FAXAll(object) Procedure")

                        FAXAll(arrlist)
                    End If
                End If
            End If
            arrlist = Nothing 'Change made to solve memory Leak and word crash issue
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.FaxAll, "Fax All Error: " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            UpdateVoiceLog("Fax All Error: " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ex = Nothing
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub PrintAll()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim arrlist As New ArrayList
            If Savestatus = 2 Then

                SaveReferrals(arrlist)
                If Not IsNothing(gReferralArrlist) Then
                    PrintAll(gReferralArrlist)
                End If
            ElseIf Savestatus = 1 Then
                If trPatientReferrals.Nodes.Item(0).GetNodeCount(True) > 2 Then
                    SaveReferrals(arrlist)
                    If arrlist.Count > 0 Then
                        PrintAll(arrlist)
                    End If
                End If
            End If
            arrlist = Nothing 'Change made to solve memory Leak and word crash issue
        Catch ex As Exception
            'UpdateVoiceLog("Print All: " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.PrintAll, "Print All: " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SendDocs()
        Dim arrlist As New ArrayList
        Try
            Me.Cursor = Cursors.WaitCursor

            If Savestatus = 2 Then
                SaveReferrals(arrlist)
                If Not IsNothing(gReferralArrlist) Then
                    SendDocs(gReferralArrlist)
                End If
            ElseIf Savestatus = 1 Then
                If trPatientReferrals.Nodes.Item(0).GetNodeCount(True) > 2 Then
                    SaveReferrals(arrlist)
                    If arrlist.Count > 0 Then
                        SendDocs(arrlist)
                    End If
                End If
            End If

        Catch ex As Exception
            'UpdateVoiceLog("Print All: " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.PrintAll, "Print All: " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            arrlist = Nothing 'Change made to solve memory Leak and word crash issue
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub LoadTemplate()
        Try

            dtcmbTemplate = objReferralsDBLayer.FillControls("T", m_PatientId)

            If Not IsNothing(dtcmbTemplate) Then
                If dtcmbTemplate.Rows.Count > 0 Then
                    cmbTemplate.DataSource = dtcmbTemplate
                    cmbTemplate.DisplayMember = dtcmbTemplate.Columns(1).ColumnName
                    cmbTemplate.ValueMember = dtcmbTemplate.Columns(0).ColumnName
                    cmbTemplate.SelectedIndex = 0
                    TemplateId = CType(dtcmbTemplate.Rows(0).Item(0), System.Int64)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectionChangeCommitted
        TemplateId = cmbTemplate.SelectedValue

        ''Checking wheather the Patient Referral is Selected or not.
        If Not IsNothing(trPatientReferrals.SelectedNode) Then
            If Not trPatientReferrals.SelectedNode Is trPatientReferrals.Nodes.Item(0) Then
                If Not trPatientReferrals.SelectedNode.Parent Is trPatientReferrals.Nodes.Item(0) Then
                    ReAssociateTemplate()
                End If
            End If
        End If
    End Sub

    Private Sub ReAssociateTemplate(Optional ByVal strRef As String = "")
        Dim myNode As myTreeNode
        myNode = CType(trPatientReferrals.SelectedNode, myTreeNode)
        Dim dr As DialogResult
        dr = MessageBox.Show("Referral Letter Template has already been associated for " & vbNewLine & "'" & objReferralsDBLayer.GetReferralName(myNode.Key) & "'" & vbNewLine & "Do you want to re-associate?", "Patient Referrals", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If dr = Windows.Forms.DialogResult.Yes Then
            ChangeLetter(myNode, strRef)
        Else
            Exit Sub
        End If


        ' myNode.Dispose() : 
        myNode = Nothing 'Change made to solve memory Leak and word crash issue

    End Sub

    Private Sub ChangeLetter(ByVal myNode As myTreeNode, ByVal strRef As String)
        If strRef <> "" Then
            cmbTemplate.Text = strRef
        End If
        CType(trPatientReferrals.SelectedNode, myTreeNode).Tag = cmbTemplate.SelectedValue
        'CType(trPatientReferrals.SelectedNode, myTreeNode).NodeName = cmbTemplate.Text
        myNode.Text = objReferralsDBLayer.GetReferralName(myNode.Key) & " - " & cmbTemplate.Text
        'sarika Referral Letter 20081125
        myNode.FaxReferralLetter = cmbTemplate.Text
        myNode.DMTemplateName = cmbTemplate.Text
        '---
        myNode.TemplateResult = Nothing
        trPatientReferrals.SelectedNode = myNode
        If Not oCurDoc Is Nothing Then
            If strSelectNode <> "" Then
                If strSelectNode = myNode.NodeName Then
                    wdReferrals.Close()
                    LoadDocument(CType(trPatientReferrals.SelectedNode, myTreeNode))
                End If
            End If
        End If
    End Sub

    Private Sub txtSearchReferrals_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchReferrals.TextChanged
        Try
            FillReferrals()
            trReferrals.SelectedNode = trReferrals.Nodes.Item(0)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub PrintAll(ByVal Arrlst)

        Try
            If Arrlst.Count > 0 Then

                Dim blnPrintCancel As Boolean = False
                Dim strFileName As String
                Dim oPrint As New clsPrintFAX
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Dim strPreviousPrinter As String = ""

                Try
                    For i As Int32 = 0 To Arrlst.Count - 1
                        UpdateVoiceLog("Sending Referral Letter " & i + 1)
                        ObjWord = New clsWordDocument
                        strFileName = ExamNewDocumentName
                        strFileName = ObjWord.GenerateFile(CType(Arrlst(i), myList).TemplateResult, strFileName)
                        ObjWord = Nothing 'Change made to solve memory Leak and word crash issue
                        
                        Dim oCurDoc As Wd.Document = myLoadWord.LoadWordApplication(strFileName)

                        '23-Oct-15 Aniket: Resolving Bug #90528: gloEMR>New Exam>RefLtr>One Print is going to the selected printer and Other is going to the default printer
                        If (oPrint.PrintDoc(oCurDoc, m_PatientId, Not CType(i, Boolean), , , strPreviousPrinter) = DialogResult.Cancel) Then
                            blnPrintCancel = True
                            LoadDocument(myNode)
                        End If

                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.PrintAll, "Referral Printed.", gloAuditTrail.ActivityOutCome.Success)
                        
                        myLoadWord.CloseWordOnly(oCurDoc)

                        If (blnPrintCancel = True) Then
                            Exit For
                        End If
                    Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
                oPrint.Dispose()
                oPrint = Nothing

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub SendDocs(ByVal Arrlst)
        Try
            If Arrlst.Count > 0 Then

                Dim blnPrintCancel As Boolean = False
                Dim strFileName As String

                Dim osend As New clsPrintFAX
                Dim ofile As File
                Dim strReffLetterDocx As String
                Dim strReffLettertxt As String
                Dim strDocumentSend As String = String.Empty
                Dim strClenDocsend As String
                Dim nSendPhysicanID As Int64 = 0
                Dim strSubject As String = String.Empty
                'strDocumentSend = ExamNewDocumentName
                'File.Copy(NotesFileName, strDocumentSend)
                ofile = Nothing
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Dim oCurDoc As Wd.Document = Nothing
                Try
                    For i As Int32 = 0 To Arrlst.Count - 1
                        UpdateVoiceLog("Sending Referral Letter " & i + 1)
                        ''''Get Referral letter Note Start
                        ObjWord = New clsWordDocument
                        nSendPhysicanID = CType(Arrlst(i), myList).Index
                        strSubject = CType(Arrlst(i), myList).DMTemplateName
                        strFileName = ExamNewDocumentName
                        strFileName = ObjWord.GenerateFile(CType(Arrlst(i), myList).TemplateResult, strFileName)
                        ObjWord = Nothing 'Change made to solve memory Leak and word crash issue

                        'wdTemp = New AxDSOFramer.AxFramerControl
                        'Me.Controls.Add(wdTemp)
                        'wdTemp.Location = New System.Drawing.Point(-50, -50)
                        'wdTemp.Open(strFileName)
                        'oCurDoc = wdTemp.ActiveDocument
                        oCurDoc = myLoadWord.LoadWordApplication(strFileName)
                        'oWordApp = oCurDoc.Application
                        ''''Get Referral letter Note End

                        If gblnIsReferalNoteadd = True Then '''' If Referral note has to be added in message body
                            '''' Send Document for clean up
                            strReffLetterDocx = osend.SendDoc(oCurDoc, m_PatientId)
                            'Me.Controls.Remove(wdTemp)
                            'wdTemp.Close()
                            'wdTemp.Dispose()
                            'oCurDoc = Nothing
                            ''myLoadWord.CloseWordOnly(oCurDoc)

                            '''' Save Document as text start
                            'wdTemp = New AxDSOFramer.AxFramerControl
                            'Me.Controls.Add(wdTemp)
                            'wdTemp.Location = New System.Drawing.Point(-50, -50)
                            'wdTemp.Open(strReffLetterDocx)
                            'oCurDoc = wdTemp.ActiveDocument
                            ''oCurDoc = myLoadWord.LoadWordApplication(strReffLetterDocx)
                            'oWordApp = oCurDoc.Application


                            'Added by Ashish with help from Laxman Sir on 6-June-2014
                            'to prevent format conversion warning from appearing.
                            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll

                            Try
                                thisAlertLevel = oCurDoc.Application.DisplayAlerts
                                oCurDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                            Catch ex As Exception

                            End Try
                            strReffLettertxt = ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".txt")

                            oCurDoc.SaveAs(strReffLettertxt, Wd.WdSaveFormat.wdFormatText, False, "", False, "", False, False, False, False, False, 1252, False, False, Wd.WdLineEndingType.wdCRLF, 0)
                            Try
                                oCurDoc.Application.DisplayAlerts = thisAlertLevel
                            Catch ex As Exception

                            End Try
                            'Me.Controls.Remove(wdTemp)
                            'wdTemp.Close()
                            'wdTemp.Dispose()
                            'oCurDoc = Nothing
                            myLoadWord.CloseWordOnly(oCurDoc)
                            '''' Save Document as text End

                            '''' Get Exam Note and Clean up start
                            'wdTemp = New AxDSOFramer.AxFramerControl
                            'Me.Controls.Add(wdTemp)
                            'wdTemp.Location = New System.Drawing.Point(-50, -50)
                            'wdTemp.Open(strFileName)
                            'oCurDoc = wdTemp.ActiveDocument
                            ''oCurDoc = myLoadWord.LoadWordApplication(strFileName)
                            '  oWordApp = oCurDoc.Application
                            strClenDocsend = strReffLetterDocx 'ExamNewDocumentName
                            'strClenDocsend = osend.SendDoc(oCurDoc, m_PatientId)
                            'Me.Controls.Remove(wdTemp)
                            'wdTemp.Close()
                            'wdTemp.Dispose()
                            'oCurDoc = Nothing
                            ''myLoadWord.CloseWordOnly(oCurDoc)
                            '''' Get Exam Note and Clean up End 
                            If File.Exists(strClenDocsend) Then
                                Dim objCls As New ClsReferralsDBLayer()
                                Dim address As String = ""
                                address = objCls.CheckDirectAddress(nSendPhysicanID)
                                'If (address <> "") Then
                                Dim ofrmSendNewMail As New InBox.NewMail(m_PatientId, strClenDocsend, nSendPhysicanID, strReffLettertxt, strSubject)
                                AddHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromSummaryOfVisit
                                If gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                                    gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(gloEMR.gnPatientProviderID)
                                    ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation
                                End If

                                ofrmSendNewMail.ShowInTaskbar = True
                                ofrmSendNewMail.ShowDialog()

                                RemoveHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromSummaryOfVisit
                                ofrmSendNewMail.Close()
                                ofrmSendNewMail = Nothing
                                objCls = Nothing
                                'Else
                                '    MessageBox.Show("Direct Address Missing for Referral Provider. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                'End If
                            Else
                                MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If


                        Else '''' If referral letter is part of attachment

                            '''' Insert exam note in referral letter start
                            oCurDoc.ActiveWindow.SetFocus()
                            RemoveAllBookMarks(oCurDoc)
                            oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                            AddBookMark(oCurDoc)

                            '00000779: exception while Provider Direct Message at Referral Letter
                            If File.Exists(NotesFileName) Then
                                oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                                oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                                oCurDoc.ActiveWindow.Selection.InsertFile(NotesFileName)
                                oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                                AddBookMark(oCurDoc)
                            End If

                            strDocumentSend = osend.SendDoc(oCurDoc, m_PatientId)
                            'Me.Controls.Remove(wdTemp)
                            'wdTemp.Close()
                            'wdTemp.Dispose()
                            'oCurDoc = Nothing
                            myLoadWord.CloseWordOnly(oCurDoc)
                            '''' Insert exam note in referral letter End
                            If File.Exists(strDocumentSend) Then
                                Dim objCls As New ClsReferralsDBLayer()
                                Dim address As String = ""
                                address = objCls.CheckDirectAddress(nSendPhysicanID)
                                'If (address <> "") Then
                                Dim ofrmSendNewMail As New InBox.NewMail(m_PatientId, strDocumentSend, nSendPhysicanID, "", strSubject)
                                AddHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromSummaryOfVisit
                                If gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                                    gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(gloEMR.gnPatientProviderID)
                                    ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation
                                End If

                                ofrmSendNewMail.ShowInTaskbar = True
                                ofrmSendNewMail.ShowDialog()
                                RemoveHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromSummaryOfVisit
                                ofrmSendNewMail.Close()
                                ofrmSendNewMail = Nothing
                                'Else
                                '    MessageBox.Show("Direct Address Missing for Referral Provider. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                'End If
                                objCls = Nothing
                            Else
                                MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If

                        End If

                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.ClinicalExchange, "Referral Send.", gloAuditTrail.ActivityOutCome.Success)

                    Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
                osend.Dispose()
                osend = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub FaxAll(ByVal Arrlst)
        Dim arrFaxNos As New ArrayList
        Dim objList As myList = Nothing
        Try
            UpdateVoiceLog("In FAX All with Object method")
            Dim strFileName As String

            Dim blnFAXPrinterHasToSet As Boolean = True
            Dim blnDSODefaultPrinterHasToSet As Boolean = True
            If Arrlst.Count > 0 Then

                clsPrintFAX.IsBlackIceSettingsSet = False
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()

                'Dim oCurDoc As Wd.Document = Nothing
                Try
                    For i As Int32 = 0 To Arrlst.Count - 1

                        UpdateVoiceLog("Sending Referral Letter - " & i + 1)
                        ObjWord = New clsWordDocument
                        strFileName = ExamNewDocumentName
                        strFileName = ObjWord.GenerateFile(CType(Arrlst(i), myList).TemplateResult, strFileName)
                        
                        ObjWord = Nothing

                        UpdateVoiceLog("Calling RetrieveFAXDetails method to retrieve FAX Details")
                        Dim blnFirstReferral As Boolean
                        If i = 0 Then
                            blnFirstReferral = True
                        Else
                            blnFirstReferral = False
                        End If
                        mdlFAX.Owner = Me


                        'sarika Fax from Referrals 20081121
                        mdlFAX.gstrFAXContactPerson = ""
                        mdlFAX.gstrFAXContactPersonFAXNo = ""
                        mdlFAX.gstrFAXContacts = Nothing
                        mdlFAX.multipleRecipients = False
                        'Added code for Fax type against the Problem #00000874
                        mdlFAX.gstrFAXType = "Referral Letter"
                        '-------------

                        If RetrieveFAXDetails(mdlFAX.enmFAXType.ReferralLetter, CStr(m_PatientId), CType(Arrlst(i), myList).Description, "", CType(Arrlst(i), myList).ReferralLetterName, CType(Arrlst(i), myList).Index, m_VisitId, 0, blnFirstReferral, Me) Then

                            UpdateVoiceLog("FAX Details retrieved")
                            'If i >= 1 Then
                            '    blnFAXPrinterHasToSet = False
                            'End If
                            If i >= Arrlst.Count - 1 Then
                                blnDSODefaultPrinterHasToSet = True
                            Else
                                blnDSODefaultPrinterHasToSet = False
                            End If

                            UpdateVoiceLog("Creating object of clsPrintFAX class")
                            Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
                            UpdateVoiceLog("Calling FAX Document method")

                            If i <> 0 Then
                                clsPrintFAX.IsBlackIceSettingsSet = True
                            End If
                            If ChkSameContact(gstrFAXContactPerson, gstrFAXContactPersonFAXNo, arrFaxNos) = False Then
                                If objPrintFAX.FAXDocument(myLoadWord, strFileName, CStr(m_PatientId), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, cmbTemplate.SelectedItem(1), clsPrintFAX.enmFAXType.ReferralLetter, True, blnFAXPrinterHasToSet, blnDSODefaultPrinterHasToSet) = False Then
                                    'TIFF File has not been created
                                    If Trim(objPrintFAX.ErrorMessage) <> "" Then
                                        MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    End If
                                End If
                                objList = New myList
                                objList.ContactName = gstrFAXContactPerson
                                objList.ContactPersonFaxNo = gstrFAXContactPersonFAXNo
                                ' arrFaxNos.Add(gstrFAXContactPersonFAXNo)
                                arrFaxNos.Add(objList)
                                objList = Nothing
                                UpdateVoiceLog("Document faxed")

                            End If
                            objPrintFAX.Dispose()
                            objPrintFAX = Nothing
                        End If
                        'wdTemp.Close()
                        'Me.Controls.Remove(wdTemp)
                        'wdTemp.Dispose()
                        'wdTemp = Nothing
                        'oCurDoc = Nothing
                        '       myLoadWord.CloseWordOnly(oCurDoc)
                    Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.FaxAll, "Fax All(object) Error: " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not objList Is Nothing Then objList = Nothing 'Change made to solve memory Leak and word crash issue
        End Try

    End Sub

    Private Function ChkSameContact(ByVal gstrFAXContactPerson As String, ByVal gstrFAXContactPersonFAXNo As String, ByVal arrFaxNos As ArrayList) As Boolean
        Dim objList As myList
        Dim _result As Boolean
        _result = False

        Try

            For i As Integer = 0 To arrFaxNos.Count - 1
                'objList = New myList
                objList = CType(arrFaxNos.Item(i), myList)
                If objList.ContactName = gstrFAXContactPerson AndAlso objList.ContactPersonFaxNo = gstrFAXContactPersonFAXNo Then
                    If gstrFAXContactPerson.Trim() <> "" Then
                        _result = True
                        Exit For
                    End If
                End If
                If Not objList Is Nothing Then objList = Nothing 'Change made to solve memory Leak and word crash issue
            Next


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try

        Return _result
    End Function

    Private Sub trPatientReferrals_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trPatientReferrals.AfterSelect
        'If e.Action <> TreeViewAction.Unknown Then
        If Not IsNothing(e.Node) Then
            If Not e.Node Is trPatientReferrals.Nodes.Item(0) Then
                If Not e.Node.Parent Is trPatientReferrals.Nodes.Item(0) Then
                    If Not IsNothing(CType(e.Node, myTreeNode).Tag) Then
                        cmbTemplate.SelectedValue = CType(e.Node, myTreeNode).Tag
                    Else
                        cmbTemplate.SelectedValue = TemplateId
                    End If

                End If
            End If
        End If
        ' End If
    End Sub

    Private Sub AddReferrals(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
        'Commented by shubhangi 20091125
        'We want to call Setup Physician form of gloPM
        '  Dim frm As frmContactMst
        'shubhangi 20091125
        'Call frmSetupPhysician form of gloPM        
        Dim ofrmContact As New gloContacts.frmSetupPhysician(GetConnectionString)
        ofrmContact.Text = "Add Contacts for Physician"
        ofrmContact.ShowDialog(IIf(IsNothing(ofrmContact.Parent), Me, ofrmContact.Parent))
        'Change made to solve memory Leak and word crash issue
        ofrmContact.Close()
        ofrmContact.Dispose()
        ofrmContact = Nothing

        trReferrals.Nodes.Clear()
        Dim rootnode As myTreeNode
        rootnode = New myTreeNode("Referrals", -1)
        rootnode.ImageIndex = 0
        rootnode.SelectedImageIndex = 0
        trReferrals.Nodes.Add(rootnode)

        'rootnode.Dispose() : 
        rootnode = Nothing   'Change made to solve memory Leak and word crash issue
        '''''
        FillReferrals()

        Call ResetSearch()
        txtSearchReferrals.Select()
    End Sub

    Private Sub EditReferrals(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Id As Long
        Try
            Dim mytreenode As myTreeNode
            mytreenode = CType(trReferrals.SelectedNode, myTreeNode)
            Id = mytreenode.Key
            Dim ofrmContact As New gloContacts.frmSetupPhysician(Id, GetConnectionString)
            ofrmContact.Text = "Update Contacts for Physician"
            ofrmContact.ShowDialog(IIf(IsNothing(ofrmContact.Parent), Me, ofrmContact.Parent))
            trReferrals.Nodes.Clear()
            Dim rootnode As myTreeNode
            rootnode = New myTreeNode("Referrals", -1)
            rootnode.ImageIndex = 0
            rootnode.SelectedImageIndex = 0
            trReferrals.Nodes.Add(rootnode)

            '    rootnode.Dispose() : 
            rootnode = Nothing 'Change made to solve memory Leak and word crash issue
            ' mytreenode.Dispose() : 
            mytreenode = Nothing 'Change made to solve memory Leak and word crash issue
            '''''
            FillReferrals()
            ofrmContact.Dispose()
            ofrmContact = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.FaxAll, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Edit Referrals", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            ex = Nothing
        End Try
    End Sub

    Private Sub ResetSearch()
        txtSearchReferrals.Text = ""
        txtSearchReferrals.Focus()
    End Sub

    Private Sub mnuSendAllNormalPriority_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSendAllNormalPriority.Click
        Try
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
            UpdateVoiceLog("FaxAll Referrals Started from Referrals form ")
            Call FAXAll()
            UpdateVoiceLog("FaxAll Referrals Completed from Referrals form ")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub mnuSendAllImmediately_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSendAllImmediately.Click
        Try
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
            UpdateVoiceLog("FaxAll Referrals Started from Referrals form ")
            Call FAXAll()
            UpdateVoiceLog("FaxAll Referrals Completed from Referrals form ")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub mnuSendFAXWithNormal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSendFAXWithNormal.Click
        Try
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
            UpdateVoiceLog("FaxAll and Close Referrals Started from Referrals form ")
            Call FAXAll()
            Me.Opacity = 0
            UpdateVoiceLog("FaxAll and CloseReferrals Completed from Referrals form ")
            '  Me.Close()
            gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub mnuSendFAXImmediately_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSendFAXImmediately.Click
        Try
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
            UpdateVoiceLog("FaxAll and Close Referrals Started from Referrals form ")
            Call FAXAll()
            Me.Opacity = 0
            UpdateVoiceLog("FaxAll and CloseReferrals Completed from Referrals form ")
            'Me.Close()
            gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Dim blnIsSingleFaxorPrint As Boolean = False

    Private Sub tlsReferrals_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsReferrals.ItemClicked

        Try
            Select Case e.ClickedItem.Tag
                Case "On"

                    UpdateVoiceLog("SwitchOff Mic started from tlsReferrals_ItemClicked in Referrals is invoked")
                    If Not MyMDIParent Is Nothing Then

                        If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                            MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                            e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                            e.ClickedItem.ToolTipText = "Microphone Off"
                            e.ClickedItem.Text = "Mic"

                        ElseIf MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                            MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                            e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                            e.ClickedItem.ToolTipText = "Microphone On"
                            e.ClickedItem.Text = "Mic"
                            If (IsNothing(Dashboardform) = False) Then
                                Dashboardform.ActiveDSO = wdReferrals
                            End If


                        End If
                        

                        '11-Jun-13 Aniket: Resolving Bug 52108 
                    ElseIf TypeOf (Me.Owner) Is MainMenu Then

                        If Not Me.Owner Is Nothing Then
                            'Debug.WriteLine("Owner is " & Me.Owner.Name)
                            If CType(Me.Owner, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                                CType(Me.Owner, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                                e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                                e.ClickedItem.ToolTipText = "Microphone Off"
                                e.ClickedItem.Text = "Mic"
                                'Debug.WriteLine("tlsReferrals_ItemClicked If 3")
                            ElseIf CType(Me.Owner, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                                CType(Me.Owner, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                                e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                                e.ClickedItem.ToolTipText = "Microphone On"
                                e.ClickedItem.Text = "Mic"
                                Try
                                    CType(Me.Owner, MainMenu).ActiveDSO = wdReferrals
                                Catch ex As Exception

                                End Try


                            End If

                        End If

                    End If

                    UpdateVoiceLog("SwitchOff Mic Completed from tlsReferrals_ItemClicked in Referrals is invoked")

                Case "Insert Sign"
                    'Call InsertProviderSignature() 'if user is provider then insert provider sign lese user's sign
                    ''Added on 20131113-gloEMR - Referral Letter - Application is inserting Provder Signature on Sign button.
                    If IsNothing(oCurDoc) = False Then
                        'If else condition added by dipak as allow user to add sign
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

                Case "Insert CoSign"
                    Call InsertCoSignature()

                Case "Capture Sign"
                    Call CaptureSignature()
                Case "Undo"
                    Call UnDoChanges()
                Case "Redo"
                    Call ReDoChanges()

                Case "Insert File"
                    ImportDocument(1)

                Case "Scan Documents"
                    ImportDocument(2)

                Case "New"
                    RefreshPatientReferrals()
                    Savestatus = 1

                Case "Save & Close"
                    UpdateExamLog("B10: Referral Save and Close Process Started", m_PatientId, m_ExamId)
                    TurnOffMicrophone()
                    UpdateVoiceLog("Save and Close Referrals Started from Referrals form ")
                    SaveReferrals()
                    UpdateVoiceLog("Save and Close Referrals Started from Referrals form ")
                    'End If
                    If blnIsReferrals Then
                        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    Else
                        ResetVisitSummary()
                    End If

                    UpdateExamLog("B10: Referral Save and Close Process Completed", m_PatientId, m_ExamId)

                Case "Print All"
                    UpdateExamLog("B12: Print All Process Started", m_PatientId, m_ExamId)
                    TurnOffMicrophone()
                    UpdateVoiceLog("PrintAll Referrals Started from Referrals form ")
                    Me.Opacity = 50
                    Call PrintAll()
                    Me.Opacity = 100
                    UpdateVoiceLog("PrintAll Referrals Completed from Referrals form ")
                    UpdateExamLog("B12: Print All Process Completed", m_PatientId, m_ExamId)

                Case "Fax All"
                    UpdateExamLog("B13: FAX All Process Started", m_PatientId, m_ExamId)
                    TurnOffMicrophone()
                    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
                    UpdateVoiceLog("FaxAll Referrals Started from Referrals form ")
                    Me.Opacity = 50
                    bnlIsFaxOpened = True
                    Call FAXAll()
                    bnlIsFaxOpened = False
                    Me.Opacity = 100
                    UpdateVoiceLog("FaxAll Referrals Completed from Referrals form ")
                    UpdateExamLog("B13: FAX All Process Completed", m_PatientId, m_ExamId)

                Case "PrintAll_n_Close"
                    UpdateExamLog("B14: Print All and Close Process Started", m_PatientId, m_ExamId)
                    TurnOffMicrophone()
                    UpdateVoiceLog("PrintAll and Close Referrals Started from Referrals form ")
                    Call PrintAll()
                    UpdateVoiceLog("PrintAll and Close Referrals Completed from Referrals form ")

                    If blnIsReferrals Then
                        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    Else
                        ResetVisitSummary()
                    End If
                    UpdateExamLog("B14: Print All and Close Process Started", m_PatientId, m_ExamId)

                Case "FaxAll_n_Close"
                    UpdateExamLog("B15: FAX All and Close Process Started", m_PatientId, m_ExamId)
                    TurnOffMicrophone()
                    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
                    UpdateVoiceLog("FaxAll and Close Referrals Started from Referrals form ")
                    Me.Opacity = 50
                    bnlIsFaxOpened = True
                    Call FAXAll()
                    bnlIsFaxOpened = False
                    Me.Opacity = 100
                    UpdateVoiceLog("FaxAll and CloseReferrals Completed from Referrals form ")
                    If blnIsReferrals Then

                        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    Else
                        ResetVisitSummary()
                    End If
                    UpdateExamLog("B15: FAX All and Close Process Completed", m_PatientId, m_ExamId)

                Case "Close"
                    UpdateExamLog("B16: Close Referral Screen Process Started", m_PatientId, m_ExamId)
                    TurnOffMicrophone()
                    CloseReferrals()
                    UpdateExamLog("B16: Close Referral Screen Process Completed", m_PatientId, m_ExamId)

                Case "Fax_n_Close"
                    UpdateExamLog("B17: FAX and Close Process Started", m_PatientId, m_ExamId)
                    If (trPatientReferrals.SelectedNode.Level <> 2) Then
                        MessageBox.Show("Please select a referral . ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Select
                    End If
                    blnIsSingleFaxorPrint = True
                    TurnOffMicrophone()
                    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
                    UpdateVoiceLog("FaxAll and Close Referrals Started from Referrals form ")
                    Me.Opacity = 50
                    bnlIsFaxOpened = True
                    Call FAXAll()
                    bnlIsFaxOpened = False
                    Me.Opacity = 100
                    UpdateVoiceLog("FaxAll and CloseReferrals Completed from Referrals form ")
                    If blnIsReferrals Then
                        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    Else
                        ResetVisitSummary()
                    End If
                    blnIsSingleFaxorPrint = False
                    UpdateExamLog("B17: FAX and Close Process Completed", m_PatientId, m_ExamId)

                Case "Print_n_Close"
                    UpdateExamLog("B18: Print and Close Process Started", m_PatientId, m_ExamId)
                    If (trPatientReferrals.SelectedNode.Level <> 2) Then
                        MessageBox.Show("Please select a referral. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Select
                    End If
                    blnIsSingleFaxorPrint = True
                    TurnOffMicrophone()
                    UpdateVoiceLog("PrintAll and Close Referrals Started from Referrals form ")
                    Call PrintAll()
                    UpdateVoiceLog("PrintAll and Close Referrals Completed from Referrals form ")

                    If blnIsReferrals Then
                        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    Else
                        ResetVisitSummary()
                    End If
                    blnIsSingleFaxorPrint = False
                    UpdateExamLog("B18: Print and Close Process Completed", m_PatientId, m_ExamId)

                    '' chetan added for strike through on oct 23
                Case "StrikeThrough"
                    InsertStrike()

                Case "Export"
                    ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Referral letter", Me)
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    objword1 = Nothing
                    ' Export Function for Word Docs Integrated by dipak  as on 26 oct 2010

                Case "SecureMsg"
                    If Not IsNothing(trPatientReferrals.Nodes(0)) Then
                        If trPatientReferrals.Nodes(0).Nodes.Count = 3 Then
                            If trPatientReferrals.Nodes(0).Nodes(0).Nodes.Count = 0 AndAlso trPatientReferrals.Nodes(0).Nodes(1).Nodes.Count = 0 AndAlso trPatientReferrals.Nodes(0).Nodes(2).Nodes.Count = 0 Then
                                MessageBox.Show("Please select Primary Care physician / Referrals / Other Care Team to whom you want to send DIRECT Message.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return
                            End If
                        Else
                            Return
                        End If
                    Else
                        Return
                    End If

                    'If strProviderDirectAddress <> "" Then
                    If strProviderDirectAddress <> "" OrElse gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                        Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(m_PatientId)
                        If sError <> "" Then
                            MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                            Return
                        Else
                            TurnOffMicrophone()
                            UpdateVoiceLog("Send secure Referrals Started from Referrals form ")
                            Call SendDocs()
                            'LoadDocument(CType(trPatientReferrals.SelectedNode, myTreeNode))
                            UpdateVoiceLog("Send secure Referrals Completed from Referrals form ")
                        End If

                    Else
                        MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub
    Private Sub InsertStrike()
        Try
            Dim strThrough As String
            If Not IsNothing(oCurDoc) Then
                If Not IsNothing(oCurDoc.ActiveWindow.Selection) Then
                    If oCurDoc.ActiveWindow.Selection.Characters.Count - 1 > 0 Then
                        strThrough = "Strikethrough by " & gstrLoginName & " on " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time")
                        ' tmrDocProtect.Enabled = False

                        If oCurDoc.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyComments Then
                            oCurDoc.Unprotect()
                        End If
                        oCurDoc.ActiveWindow.Selection.Range.Font.DoubleStrikeThrough = True
                        oCurDoc.ActiveWindow.Selection.Move(1)
                        oCurDoc.ActiveWindow.Selection.TypeParagraph()
                        oCurDoc.ActiveWindow.Selection.Font.DoubleStrikeThrough = False
                        oCurDoc.ActiveWindow.Selection.TypeText(Text:=strThrough)
                        oCurDoc.ActiveWindow.Selection.Move(1)
                        oCurDoc.ActiveWindow.Selection.TypeParagraph()
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub CloseReferrals()
        If Not oCurDoc Is Nothing Then
            If oCurDoc.Saved = False Then
                Dim Result As Integer
                Dim strFileName As String
                'Check if Yes no and cancel
                Result = MsgBox("Do you want to save the changes to this Referral letter?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
                If Result = MsgBoxResult.Yes Then
                    strFileName = ExamNewDocumentName
                    If (IsNothing(oCurDoc) = False) Then
                        oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    Else
                        wdReferrals.Save(strFileName, True, "", "")
                    End If

                    wdReferrals.Close()
                    oCurDoc = Nothing
                    SaveTreeNode(strSelectNode, strFileName)
                    SaveReferrals()
                    If blnIsReferrals Then
                        '  Me.Close()
                        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    Else
                        ResetVisitSummary()
                    End If
                ElseIf Result = MsgBoxResult.Cancel Then

                    Exit Sub
                ElseIf Result = MsgBoxResult.No Then
                    wdReferrals.Close()
                    oCurDoc = Nothing
                    If blnIsReferrals Then
                        ' Me.Close()
                        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    Else
                        ResetVisitSummary()
                    End If
                End If
            Else
                wdReferrals.Close()
                oCurDoc = Nothing
                If blnIsReferrals Then
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                Else
                    ResetVisitSummary()
                End If
            End If
        Else
            If blnIsReferrals Then
                '  Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            Else
                ResetVisitSummary()
            End If

        End If

    End Sub

#Region "Voice implementation"

    Private Sub AddBasicVoiceCommands()
        ReferralsVoicecol.Clear()
        'ReferralsVoicecol.Add("Save Referrals")
        ReferralsVoicecol.Add("Print All")
        ReferralsVoicecol.Add("Fax All")
        ReferralsVoicecol.Add("Print and Close")
        ReferralsVoicecol.Add("Fax and Close")
        ReferralsVoicecol.Add("Insert Signature")
        ReferralsVoicecol.Add("Close Referrals")
        ReferralsVoicecol.Add("Save and Close")
        ReferralsVoicecol.Add("Save and Close Referrals")

    End Sub

    Private Sub TurnOffMicrophone()

        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            Try
                Dim myParent As MainMenu = Nothing
                If (IsNothing(myCaller) = False) Then
                    Try
                        If (IsNothing(myCaller.MdiParent) = False) Then
                            myParent = CType(myCaller.MdiParent, MainMenu)
                        End If

                    Catch ex As Exception

                    End Try
                End If
                If (IsNothing(myParent) = False) Then
                    If myParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        myParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        tlsMic.Image = Global.gloEMR.My.Resources.Mic_OFF
                        'Debug.WriteLine("TurnOffMicrophone If 1")
                    End If
                    'CType(Me.MdiParent, MainMenu).tlbbtnMicrophone.ImageIndex = 33
                    ' CType(Me.MdiParent, MainMenu).tlbbtn_Microphone.Visible = False
                End If


            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                'Debug.WriteLine("TurnOffMicrophone Exception")
            End Try
        Else
            tlsMic.Visible = False
            'Debug.WriteLine("TurnOffMicrophone If 2")
        End If

    End Sub

    Private Sub ShowMicrophone()
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            Try
                tlsMic.Visible = True
                Dim myParent As MainMenu = Nothing
                If (IsNothing(myCaller) = False) Then
                    Try
                        If (IsNothing(myCaller.MdiParent) = False) Then
                            myParent = CType(myCaller.MdiParent, MainMenu)
                        End If

                    Catch ex As Exception

                    End Try
                End If
                If (IsNothing(myParent) = False) Then
                    If myParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        myParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        tlsMic.Image = Global.gloEMR.My.Resources.Mic_OFF
                        'Debug.WriteLine("ShowMicrophone If 1")
                    End If
                End If


            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                'Debug.WriteLine("ShowMicrophone Exception")
            End Try
        Else
            tlsMic.Visible = False
            tlsMic.Image = Global.gloEMR.My.Resources.Mic_OFF
            'Debug.WriteLine("ShowMicrophone If 2")
        End If
    End Sub

    Public Sub Navigate1(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate

        If strstring = "ON" Then
            If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                tlsMic.Visible = True
                tlsMic.Image = Global.gloEMR.My.Resources.Mic_ON
            End If
        ElseIf strstring = "OFF" Then
            If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                tlsMic.Visible = True
            Else
                tlsMic.Visible = False
            End If
            tlsMic.Image = Global.gloEMR.My.Resources.Mic_OFF
        Else

            If bnlIsFaxOpened = False Then
                oCurDoc.ActiveWindow.SetFocus()
                Try
                    If Not oCurDoc Is Nothing Then
                        gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                    End If
                Catch ex2 As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex2 = Nothing
                End Try

            Else
                For Each frm As Form In Application.OpenForms
                    If frm.Name = "frmSelectContactFAXWithFAXCoverPage" Then
                        If Not IsNothing(DirectCast(frm, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc) Then
                            Try
                                DirectCast(frm, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc.ActiveWindow.SetFocus()
                                gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=DirectCast(frm, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                                Exit For
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                ex = Nothing
                            End Try
                        End If
                    End If
                Next
            End If

        End If

    End Sub

    Friend WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            ImagePath = Value
        End Set
    End Property

    Public Sub ActivateBasicVoiceCmds(ByVal myVoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds
        If myVoiceCol.Count > 0 Then
            Dim objSender As Object = Nothing
            Dim objtblbtn As New ToolStripButton

            Select Case myVoiceCol.Item(1)
                Case "Save and Close", "Save and Close Referrals"
                    objtblbtn.Tag = "Save & Close"
                    Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
                    tlsReferrals_ItemClicked(objSender, objtbl)
                    objtbl = Nothing

                Case "Insert Signature"
                    objtblbtn.Tag = "Insert Sign"
                    Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
                    tlsReferrals_ItemClicked(objSender, objtbl)
                    objtbl = Nothing
                Case "Print All"
                    objtblbtn.Tag = "Print All"
                    Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
                    tlsReferrals_ItemClicked(objSender, objtbl)
                    objtbl = Nothing
                Case "Print and Close"
                    objtblbtn.Tag = "Print_n_Close"
                    Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
                    tlsReferrals_ItemClicked(objSender, objtbl)
                    objtbl = Nothing
                Case "Fax All"
                    objtblbtn.Tag = "Fax All"
                    Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
                    tlsReferrals_ItemClicked(objSender, objtbl)
                    objtbl = Nothing
                Case "Fax and Close"
                    objtblbtn.Tag = "Fax_n_Close"
                    Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
                    tlsReferrals_ItemClicked(objSender, objtbl)
                    objtbl = Nothing
                Case "Close Referrals"
                    objtblbtn.Tag = "Close"
                    Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
                    tlsReferrals_ItemClicked(objSender, objtbl)
                    objtbl = Nothing
            End Select
            objtblbtn.Dispose()
            objtblbtn = Nothing 'Change made to solve memory Leak and word crash issue
        End If
    End Sub

    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds

    End Sub

    Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
        vVoiceMenu.Remove(1)
        If IsNothing(ReferralsVoicecol) Then
            ReferralsVoicecol = New DNSTools.DgnStrings
            Call AddBasicVoiceCommands()
        End If
        vVoiceMenu.ListSetStrings("Referrals", ReferralsVoicecol)
        vVoiceMenu.Add(1, "<Referrals>", "", "")
    End Sub

    Public Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent) Implements mdlgloVoice.gloVoice.CustomGetchanges

    End Sub

    Public Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent) Implements mdlgloVoice.gloVoice.CustomMakechanges

    End Sub

    Public Property Handle1() As Integer Implements mdlgloVoice.IExamChildEvents.Handle
        Get
            Return Me.Handle.ToInt32
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property


#End Region

    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        Try
            'Developer:Yatin N. Bhagat Date:01/31/2012 Bug ID/PRD Name/Salesforce Case:Provider Signature Format Case Reason: Comman Fucntionality is added 
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            Dim objWord As New clsWordDocument
            '   Dim oclsProvider As New clsProvider   slr not used 
            Dim clsExam As New clsPatientExams
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, m_PatientId, m_VisitId, blnSignClick)
            objCriteria = Nothing
            objWord = Nothing
            If pSign(2) = "1" Then
                If File.Exists(pSign(0)) Then
                    oCurDoc.ActiveWindow.SetFocus()

                    'SUDHIR 20090619
                    Dim oWord As New clsWordDocument
                    oWord.CurDocument = oCurDoc
                    Dim myType As Wd.WdViewType = Nothing
                    Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                    oWord.InsertImage(pSign(0))
                    oWord = Nothing
                    'END SUDHIR 

                    'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                    Dim wdRng As Wd.Range = oCurDoc.ActiveWindow.Selection.Range
                    If wdRng.Tables.Count > 0 Then
                        oCurDoc.ActiveWindow.Selection.EndKey()
                    End If
                    'end code added by dipak 

                    oCurDoc.ActiveWindow.Selection.TypeParagraph()
                    'By Mahesh Signature With Date - 20070113 Add Date Time When Signature is Inserted
                    oCurDoc.ActiveWindow.Selection.TypeText(Text:=pSign(1))
                    gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", m_PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
            'Dispose object by mitesh
            If Not IsNothing(clsExam) Then

                '08-May-13 Aniket: Resolving Memory Leaks
                clsExam.Dispose()
                clsExam = Nothing
            End If

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            objErr = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' to insert user's signature
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InsertUserSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            Dim objWord As New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = m_PatientID
            'end modification

            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID
            objWord.DocumentCriteria = objCriteria

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
                'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                Dim wdRng As Wd.Range = oCurDoc.ActiveWindow.Selection.Range
                If wdRng.Tables.Count > 0 Then
                    oCurDoc.ActiveWindow.Selection.EndKey()
                End If
                'end code added by dipak 
                oCurDoc.ActiveWindow.Selection.TypeParagraph()
                'Dim clsExam As New clsPatientExams
                'clsExam.Dispose()
                'clsExam = Nothing
                oCurDoc.ActiveWindow.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Summary of visit", m_PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try
    End Sub
    Public Sub InsertCoSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = m_PatientId
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID '' For inserting coSignature
            ObjWord.DocumentCriteria = objCriteria

            ImagePath = ObjWord.getData_FromDB("User_MST.imgSignature", "Co-Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
            ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)

            If System.IO.File.Exists(ImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()

                '' SUDHIR 20090619 '' 
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(ImagePath)
                oWord = Nothing
                'oCurDoc.ActiveWindow.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
                '' END SUDHIR ''

                oCurDoc.ActiveWindow.Selection.TypeParagraph()
                '' By Mahesh Signature With Date - 20070113
                '''' Add Date Time When Signature is Inserted
                oCurDoc.ActiveWindow.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                ''''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Co-Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try
    End Sub

    Private Sub UnDoChanges()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ReDoChanges()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ImportDocument(ByVal nInsertScan As Int16)
        'Insert File - 1
        'Scan Images - 2
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        uiPanSplitScreen_SummaryofVisit.Enabled = False

        Try
            If nInsertScan = 1 Then
                Dim oFileDialogWindow As New System.Windows.Forms.OpenFileDialog
                oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word Documents (*.doc*)|*.doc*|Rich Text Format (*.rtf)|*.rtf"

                oFileDialogWindow.FilterIndex = 2
                oFileDialogWindow.Title = "Insert External Documents"
                oFileDialogWindow.Multiselect = False
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)
                    If oFile.Extension.ToUpper = UCase(".Doc") OrElse oFile.Extension.ToUpper = UCase(".Docx") OrElse oFile.Extension.ToUpper = UCase(".txt") OrElse oFile.Extension.ToUpper = UCase(".rtf") Then
                        'Set focus to Wd object
                        oCurDoc.ActiveWindow.SetFocus()

                        'Insert file in Wd dobject
                        oCurDoc.ActiveWindow.Selection.InsertFile(oFile.FullName)
                    End If
                End If
                oFileDialogWindow.Dispose()
                oFileDialogWindow = Nothing
            ElseIf nInsertScan = 2 Then
                Dim oFiles As New ArrayList()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()

                'Commented BY Rahul Patel on 26-10-2010
                'gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'Added by Rahul Patel on 26-10-2010
                'For changing the DMS Connection String.
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'End of code added by Rahul Patel on 26-10-2010
                oEDocument.ShowEScannerForImages(m_PatientId, oFiles)
                oEDocument.Dispose()
                oEDocument = Nothing    'Change made to solve memory Leak and word crash issue

                Dim firstFlag As Boolean = True
                Dim i As Integer
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                For i = 0 To oFiles.Count - 1
                    If File.Exists(oFiles.Item(i)) Then
                        oCurDoc.ActiveWindow.SetFocus()

                        '' SUDHIR 20090619 '' 
                        Dim oWord As New clsWordDocument
                        oWord.GetandSetMyFirstFlag(True, firstFlag)
                        oWord.CurDocument = oCurDoc
                        oWord.InsertImage(oFiles.Item(i))
                        firstFlag = oWord.GetandSetMyFirstFlag(False, False)
                        oWord = Nothing
                        'oCurDoc.ActiveWindow.Selection.InlineShapes.AddPicture(FileName:=oFiles.Item(i), LinkToFile:=False, SaveWithDocument:=True)
                        '' END SUDHIR ''
                        'ResolvedBug :41969
                        If oCurDoc.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdNoProtection Then
                            oCurDoc.ActiveWindow.Selection.EndKey()
                            oCurDoc.ActiveWindow.Selection.InsertBreak()
                        End If
                    End If
                Next
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                oCurDoc.ActiveWindow.SetFocus()
                For i = oFiles.Count - 1 To 0 Step -1
                    If File.Exists(oFiles.Item(i)) Then
                        Try
                            Kill(oFiles.Item(i))
                        Catch

                        End Try

                    End If
                Next
                i = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            uiPanSplitScreen_SummaryofVisit.Enabled = True
        End Try
    End Sub

    Public Sub CaptureSignature()
        Try
            ImagePath = ""
            Dim frm As New FrmSignature
            frm.Owner = Me
            ' frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.ShowDialog(frm.Parent)
            'Change made to solve memory Leak and word crash issue
            frm.Close()
            frm.Dispose()
            frm = Nothing

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
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
                oCurDoc.ActiveWindow.Selection.TypeParagraph()
                oCurDoc.ActiveWindow.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        End If
    End Sub

    Private Function SelectNotesFile() As String
        If rbSelect.Checked Then
            If (IsNothing(strSelectedFileNotes)) Then
                Return ""
            Else
         
                If strSelectedFileNotes <> "" Then
                    If File.Exists(strSelectedFileNotes) Then
                        Return strSelectedFileNotes
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            End If

        ElseIf rbNotes.Checked Then
            If File.Exists(NotesFileName) Then
                Return NotesFileName
            Else
                Return ""
            End If
        ElseIf rbNone.Checked Then
            Return ""
        Else
            Return ""
        End If
    End Function


    Private Sub trPatientReferrals_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trPatientReferrals.NodeMouseClick
        Try
            trPatientReferrals.SelectedNode = e.Node
            If e.Button = Windows.Forms.MouseButtons.Right Then

                If Not IsNothing(trPatientReferrals.SelectedNode) Then
                    If trPatientReferrals.Nodes.Item(0) Is trPatientReferrals.SelectedNode Then
                        'Try
                        '    If (IsNothing(trPatientReferrals.ContextMenu) = False) Then
                        '        trPatientReferrals.ContextMenu.Dispose()
                        '        trPatientReferrals.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trPatientReferrals.ContextMenu = Nothing
                    ElseIf trPatientReferrals.SelectedNode.Text = trPatientReferrals.Nodes.Item(0).Nodes.Item(0).Text Then
                        'Try
                        '    If (IsNothing(trPatientReferrals.ContextMenu) = False) Then
                        '        trPatientReferrals.ContextMenu.Dispose()
                        '        trPatientReferrals.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trPatientReferrals.ContextMenu = Nothing
                    ElseIf trPatientReferrals.SelectedNode.Text = trPatientReferrals.Nodes.Item(0).Nodes.Item(1).Text Then
                        'Try
                        '    If (IsNothing(trPatientReferrals.ContextMenu) = False) Then
                        '        trPatientReferrals.ContextMenu.Dispose()
                        '        trPatientReferrals.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trPatientReferrals.ContextMenu = Nothing
                    ElseIf trPatientReferrals.SelectedNode.Text = trPatientReferrals.Nodes.Item(0).Nodes.Item(2).Text Then
                        trPatientReferrals.ContextMenu = Nothing
                    Else
                        CntPatientReferrals.MenuItems.Clear()
                        'Try
                        '    If (IsNothing(trPatientReferrals.ContextMenu) = False) Then
                        '        trPatientReferrals.ContextMenu.Dispose()
                        '        trPatientReferrals.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try

                        trPatientReferrals.ContextMenu = CntPatientReferrals

                        Dim oMenuItem As MenuItem
                        Dim oChildItem As MenuItem

                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Delete Referral"
                            .Shortcut = Shortcut.CtrlShiftE
                            .ShowShortcut = False
                        End With
                        AddHandler oMenuItem.Click, AddressOf SetMenus

                        CntPatientReferrals.MenuItems.Add(oMenuItem)
                        oMenuItem = Nothing 'Change made to solve memory Leak and word crash issue

                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "-"
                        End With
                        CntPatientReferrals.MenuItems.Add(oMenuItem)
                        oMenuItem = Nothing 'Change made to solve memory Leak and word crash issue

                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Change Referral Letter"
                        End With
                        CntPatientReferrals.MenuItems.Add(oMenuItem)

                        oChildItem = New MenuItem
                        With oChildItem
                            .Text = "Referral Letter - " & cmbTemplate.Text
                        End With
                        oMenuItem.MenuItems.Add(oChildItem)
                        oChildItem = Nothing

                        oChildItem = New MenuItem
                        With oChildItem
                            .Text = "-"
                        End With
                        oMenuItem.MenuItems.Add(oChildItem)
                        oChildItem = Nothing


                        Dim dt As DataTable
                        dt = objReferralsDBLayer.FillControls("T", m_PatientId)

                        If Not IsNothing(dt) Then
                            If dt.Rows.Count > 0 Then

                                For nCount As Int32 = 0 To dt.Rows.Count - 1
                                    If Trim(cmbTemplate.Text) <> Trim(dt.Rows(nCount)(1).ToString) Then
                                        oChildItem = New MenuItem
                                        With oChildItem
                                            .Text = Trim(dt.Rows(nCount)(1).ToString)
                                            .Shortcut = Shortcut.CtrlShiftD
                                            .ShowShortcut = False
                                        End With
                                        AddHandler oChildItem.Click, AddressOf SetMenus
                                        oMenuItem.MenuItems.Add(oChildItem)
                                        oChildItem = Nothing
                                    End If
                                Next
                            End If
                        End If
                        'Change made to solve memory Leak and word crash issue
                        If Not oMenuItem Is Nothing Then
                            oMenuItem = Nothing
                        End If
                        If Not dt Is Nothing Then
                            dt.Dispose()
                            dt = Nothing
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' To open Referral letter in the DSo Control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub trPatientReferrals_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trPatientReferrals.NodeMouseDoubleClick
        myNode = e.Node

        If (myNode Is trPatientReferrals.Nodes.Item(0)) OrElse (myNode.NodeName = "Primary Care Physician") OrElse (myNode.NodeName = "Referrals") OrElse (myNode.NodeName = "Other Care Team") Then
            Exit Sub
        End If

        Dim strFileName As String
        If Not oCurDoc Is Nothing Then
            If strSelectNode <> "" Then
                If strSelectNode = myNode.NodeName Then
                    Exit Sub
                End If
            End If
            If oCurDoc.Saved = False Then
                Dim Result As Integer
                'Check if Yes no and cancel
                Result = MsgBox("Do you want to save the changes to this Referral letter?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
                If Result = MsgBoxResult.Yes Then
                    strFileName = ExamNewDocumentName
                    ' wdReferrals.Save(strFileName, True, "", "")
                    oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    wdReferrals.Close()

                    SaveTreeNode(strSelectNode, strFileName)
                ElseIf Result = MsgBoxResult.Cancel Then
                    Exit Sub
                ElseIf Result = MsgBoxResult.No Then

                End If
            End If
        End If

        LoadDocument(myNode)
        InsertExamNotes()
    End Sub

    Private Sub LoadDocument(ByVal myNode As myTreeNode)

        Dim strFileName As String
        strSelectNode = myNode.NodeName
        If IsNothing(myNode.TemplateResult) OrElse IsDBNull(myNode.TemplateResult) Then

            If IsNothing(myNode.Tag) Then
                myNode.Tag = TemplateId
            ElseIf CType(myNode.Tag, System.Int64) = 0 Then
                myNode.Tag = TemplateId
            End If
            'myNode.TemplateResult = GetTemplate(myNode.Tag, myNode.Key)

            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Template
            objCriteria.PrimaryID = myNode.Tag

            ObjWord.DocumentCriteria = objCriteria
            '' Retrieve the Template from DB
            strFileName = ObjWord.RetrieveDocumentFile()
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
            If (IsNothing(strFileName) = False) Then


                ''//Open Template for processing in user Ctrl
                If strFileName <> "" Then
                    'Panel1.Visible = False
                    'wordOptimizer = New WordRefresh()
                    Try
                        'wordOptimizer.ShowPanel(Me.Panel1)

                        ' wdReferrals.Open(strFileName)
                        ' Dim oWordApp As Wd.Application = Nothing

                        gloWord.LoadAndCloseWord.OpenDSO(wdReferrals, strFileName, oCurDoc, oWordApp)

                        '  oCurDoc = wdReferrals.ActiveDocument

                        ''//To retrieve the Form fields for the Word document
                        ObjWord = New clsWordDocument
                        objCriteria = New DocCriteria
                        objCriteria.DocCategory = enumDocCategory.Referrals
                        objCriteria.PatientID = m_PatientId
                        objCriteria.VisitID = m_VisitId
                        objCriteria.PrimaryID = myNode.Key
                        CurrentDocReferralID = myNode.Key
                        ObjWord.DocumentCriteria = objCriteria
                        ObjWord.CurDocument = oCurDoc


                        oCurDoc = ObjWord.CurDocument
                        oWordApp = oCurDoc.Application
                        ''Panel1.Visible = True

                        WDocViewType = oCurDoc.ActiveWindow.View.Type

                        'wordOptimizer.OptimizePerformance(False, oCurDoc, 0)

                        ''Replace Form fields with Concerned data
                        ObjWord.DisableWordRefresh = True
                        ObjWord.GetFormFieldData(enumDocType.None)
                        objCriteria.Dispose()
                        objCriteria = Nothing
                        ObjWord = Nothing
                        Dim strNotesFile As String = SelectNotesFile()

                        If strNotesFile <> "" Then
                            If File.Exists(strNotesFile) Then
                                UpdateVoiceLog("Inserting Exam at LoadDocument")
                                oCurDoc.ActiveWindow.SetFocus()
                                RemoveAllBookMarks(oCurDoc)
                                oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                                AddBookMark(oCurDoc)
                                'oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                                oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdSectionBreakNextPage)
                                oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                                oCurDoc.ActiveWindow.Selection.InsertFile(strNotesFile)
                                oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                                AddBookMark(oCurDoc)
                                UpdateVoiceLog("Exam Inserted at LoadDocument")
                            End If
                        End If

                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    Finally
                        'If wordOptimizer IsNot Nothing Then
                        '    wordOptimizer.HidePanel(Me.Panel1)
                        '    wordOptimizer.OptimizePerformance(True, oCurDoc, WDocViewType)
                        '    wordOptimizer.Dispose()
                        '    wordOptimizer = Nothing
                        'End If
                    End Try
                End If

            End If

        Else
            ObjWord = New clsWordDocument
            strFileName = ExamNewDocumentName
            strFileName = ObjWord.GenerateFile(myNode.TemplateResult, strFileName)
            ''Open Template for processing in Temp user Ctrl
            '     wdReferrals.Open(strFileName)
            ' Dim oWordApp As Wd.Application = Nothing

            gloWord.LoadAndCloseWord.OpenDSO(wdReferrals, strFileName, oCurDoc, oWordApp)
            oCurDoc = wdReferrals.ActiveDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            ObjWord = Nothing
        End If
        calltoAddRefreshButtonControl()
    End Sub



    Private Sub InsertExamNotes()

        'If IsNothing(oCurDoc) AndAlso Not IsNothing(myNode) Then
        'LoadDocument(myNode)
        'End If

        If Not IsNothing(myNode) Then
            LoadDocument(myNode)
        End If

        If Not oCurDoc Is Nothing AndAlso blnRb Then
            blnRb = False
            Dim strFile As String = SelectNotesFile()
            Dim ExamRange As Wd.Range
            oCurDoc.ActiveWindow.SetFocus()
            Dim notExistingcnt As Int32 = 1 'SLR: initialized to 1 to make if anything found
            oCurDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
            For i As Int32 = 1 To oCurDoc.Bookmarks.Count + 1
                If oCurDoc.Bookmarks.Exists("BMRL" & i.ToString) = False Then
                    notExistingcnt = i
                    Exit For
                End If
            Next
            Dim cnt As Int32 = notExistingcnt - 1
            If cnt = 2 Then ''If Bookmarks are available then
                ExamRange = oCurDoc.Range(oCurDoc.Bookmarks("BMRL1").End, oCurDoc.Bookmarks("BMRL2").Start)
                ExamRange.Select()
                ' ExamRange.Cut()
                ExamRange.Delete()
                If strFile <> "" AndAlso File.Exists(strFile) Then
                    RemoveAllBookMarks(oCurDoc)
                    oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                    AddBookMark(oCurDoc)
                    'oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                    oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdSectionBreakNextPage)
                    oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                    oCurDoc.ActiveWindow.Selection.InsertFile(strFile)
                    oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                    AddBookMark(oCurDoc)
                    UpdateVoiceLog("Exam Inserted in InsertExamNotes Method")
                ElseIf rbNone.Checked = True Then
                    RemoveAllBookMarks(oCurDoc)
                End If
                ExamRange = Nothing

            Else
                If strFile <> "" AndAlso File.Exists(strFile) Then
                    RemoveAllBookMarks(oCurDoc)
                    oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                    AddBookMark(oCurDoc)
                    'oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                    oCurDoc.ActiveWindow.Selection.InsertBreak(Type:=Wd.WdBreakType.wdSectionBreakNextPage)
                    oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                    oCurDoc.ActiveWindow.Selection.InsertFile(strFile)
                    oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                    AddBookMark(oCurDoc)
                    UpdateVoiceLog("Exam Inserted  for No Book Marks in InsertExamNotes Method")
                End If
            End If

        End If
    End Sub

    Private Sub AddBookMark(ByRef oCurDoc As Wd.Document)
        If Not oCurDoc Is Nothing Then

            Dim cnt As Int32 = 1
            oCurDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
            For i As Int32 = 1 To oCurDoc.Bookmarks.Count + 1
                If oCurDoc.Bookmarks.Exists("BMRL" & i.ToString) = False Then
                    cnt = i
                    Exit For
                End If
            Next

            With oCurDoc.Bookmarks
                .Add(Range:=oCurDoc.ActiveWindow.Selection.Range, Name:="BMRL" & cnt.ToString)
                UpdateVoiceLog("Adding Bookmark - BMRL" & cnt.ToString)
                .DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
                .ShowHidden = False
            End With
        End If
    End Sub

    Private Sub RemoveAllBookMarks(ByRef oCurDoc As Wd.Document)
        If Not oCurDoc Is Nothing Then
            ' oCurDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByName
            'SLR: Changed to avoid deleting from beginning..
            oCurDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation 'Wd.WdBookmarkSortBy.wdSortByName

            For i As Int32 = oCurDoc.Bookmarks.Count To 1 Step -1
                Dim bmBookMark As Wd.Bookmark = oCurDoc.Bookmarks.Item(i)
                'If InStr(bmBookMark.Name, "BM") Then
                If bmBookMark.Name.StartsWith("BMRL") Then
                    bmBookMark.Delete()
                End If
            Next

            'For Each bmBookMark As Wd.Bookmark In oCurDoc.Range.Bookmarks
            '    If InStr(bmBookMark.Name, "BMRL") Then
            '        bmBookMark.Delete()
            '    End If
            'Next
            UpdateVoiceLog("RemoveAllBookMarks completed")

        End If
    End Sub
    Private Sub RemoveAllBookMarksforReferrals(ByRef oRefBok As Wd.Document)
        If Not oRefBok Is Nothing Then
            'oRefBok.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByName
            'SLR: Changed to avoid deleting from beginning..
            oRefBok.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation 'Wd.WdBookmarkSortBy.wdSortByName

            For i As Int32 = oRefBok.Bookmarks.Count To 1 Step -1
                Dim bmBookMark As Wd.Bookmark = oRefBok.Bookmarks.Item(i)
                'If InStr(bmBookMark.Name, "BM") Then
                If bmBookMark.Name.StartsWith("BMRL") Then
                    bmBookMark.Delete()
                End If
            Next

            'For Each bmBookMark As Wd.Bookmark In oRefBok.Range.Bookmarks
            '    If InStr(bmBookMark.Name, "BMRL") Then
            '        bmBookMark.Delete()
            '    End If
            'Next
            ''UpdateVoiceLog("RemoveAllBookMarks completed")

        End If
    End Sub
    ''' <summary>
    ''' To implemt the Dropdown and check Box selection change event
    ''' </summary>
    ''' <param name="Sel"></param>
    ''' <remarks></remarks>
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
                    '     r.SetRange(Sel.Start, Sel.End + 1)
                    'Bug #71063: #00000729: Refferal Letter
                    If r.FormFields IsNot Nothing Then
                        If r.FormFields.Count >= 1 Then

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
                        End If
                        Exit Sub
                    End If
                End If
            End If
        Catch excp As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, excp.ToString, gloAuditTrail.ActivityOutCome.Failure)
            excp = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' To raise the click event for drop down list
    ''' </summary>
    ''' <param name="btn"></param>
    ''' <param name="Cancel"></param>
    ''' <remarks></remarks>
    Private Sub btn_Click(ByVal btn As oOffice.CommandBarButton, ByRef Cancel As Boolean)
        myidx = btn.Index
    End Sub

    Private Sub SaveTreeNode(ByVal strNodeName As String, ByVal strRefLetter As String)
        For i As Int32 = 0 To trPatientReferrals.Nodes.Item(0).GetNodeCount(False) - 1
            Dim ReferralNode As myTreeNode

            Dim strOnlyRefLetter As String = Nothing

            'get the ICD9Node associated sequentially
            ReferralNode = trPatientReferrals.Nodes(0).Nodes(i)
            If ReferralNode.GetNodeCount(True) > 0 Then

                For j As Int32 = 0 To ReferralNode.GetNodeCount(False) - 1
                    Dim myNode As myTreeNode
                    myNode = ReferralNode.Nodes.Item(j)
                    If myNode.NodeName = strNodeName Then
                        ObjWord = New clsWordDocument
                        myNode.TemplateResult = CType(ObjWord.ConvertFiletoBinary(strRefLetter), Object)
                        Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                        Try
                            strOnlyRefLetter = GenerateReferralLetter(myLoadWord, strRefLetter)
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try

                        myLoadWord.CloseApplicationOnly()
                        myLoadWord = Nothing
                        myNode.ReferralLetter = CType(ObjWord.ConvertFiletoBinary(strOnlyRefLetter), Object)
                        myNode.SelectedImageKey = "Yes"
                        ObjWord = Nothing
                        Exit Sub
                    End If
                Next
            End If
            'Change made to solve memory Leak and word crash issue
            If Not ReferralNode Is Nothing Then
                ' ReferralNode.Dispose() : 
                ReferralNode = Nothing
            End If
        Next

    End Sub
    Private Function GenerateReferralLetter(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByRef _strRefLetter As String) As String
        Dim ofile As File
        Dim strwithRefLetter As String
        Dim _strOnlyRefLetter As String
        Try


            strwithRefLetter = ExamNewDocumentName
            File.Copy(_strRefLetter, strwithRefLetter)
            'wdTemp = New AxDSOFramer.AxFramerControl
            'Me.Controls.Add(wdTemp)
            'wdTemp.Location = New System.Drawing.Point(-50, -50)
            'wdTemp.Open(strwithRefLetter)
            'oCurDoc = wdTemp.ActiveDocument

            Dim oCurDoc = myLoadWord.LoadWordApplication(strwithRefLetter)

            ' oWordApp = oCurDoc.Application
            GetReferralLetter(oCurDoc)
            _strOnlyRefLetter = ExamNewDocumentName
            oCurDoc.SaveAs(_strOnlyRefLetter, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            Return _strOnlyRefLetter
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return ""
        Finally
            ofile = Nothing
            'Me.Controls.Remove(wdTemp)
            'wdTemp.Close()
            'wdTemp.Dispose()
            'oCurDoc = Nothing
            myLoadWord.CloseWordOnly(oCurDoc)
        End Try

    End Function
    Private Sub GetReferralLetter(ByRef oRefDoc As Wd.Document)
        'If IsNothing(oCurDoc) And Not IsNothing(myNode) Then
        '    LoadDocument(myNode)
        'End If
        If Not oRefDoc Is Nothing Then


            Dim ExamRange As Wd.Range
            oRefDoc.ActiveWindow.SetFocus()
            oRefDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
            Dim notExistingcnt As Int32 = 1 'SLR: initialized to 1 to get anything...
            For i As Int32 = 1 To oRefDoc.Bookmarks.Count + 1
                If oRefDoc.Bookmarks.Exists("BMRL" & i.ToString) = False Then
                    notExistingcnt = i
                    Exit For
                End If
            Next
            Dim cnt As Int32 = notExistingcnt - 1
            If cnt = 2 Then ''If Bookmarks are available then
                ExamRange = oRefDoc.Range(oRefDoc.Bookmarks("BMRL1").End, oRefDoc.Bookmarks("BMRL2").Start)
                ExamRange.Select()
                ' ExamRange.Cut()
                ExamRange.Delete()

                RemoveAllBookMarksforReferrals(oRefDoc)

                ExamRange = Nothing


            End If
        End If
    End Sub

    Private Sub rbNotes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbNotes.CheckedChanged
        If rbNotes.Checked Then
            rbNotes.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rbSelect.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            rbNone.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            blnRb = True

            InsertExamNotes()
            'Bug #80350: gloEMR: Referral letter- Application brigs focus to word document
            Try
                gloWord.WordDialogBoxBackgroundCloser.bringToTop(Me, True)

                Dashboardform.ActiveDSO = wdReferrals
            Catch ex As Exception

            End Try
        Else
            rbNotes.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            ''rbNotes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        End If

    End Sub

    Private Sub rbSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSelect.CheckedChanged
        If rbSelect.Checked Then
            rbSelect.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rbNotes.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            rbNone.Font = gloGlobal.clsgloFont.gFont ' Font("Tahoma", 9, FontStyle.Regular)
            blnRb = True

            If (IsNothing(strSelectedFileNotes)) Then


                UpdateVoiceLog("CreateSelectedNotes process started ")
                If Not IsNothing(NotesFileName) Then
                    If NotesFileName.Trim() <> "" Then

                        strSelectedFileNotes = CreateSelectedNotes(NotesFileName)

                    End If
                End If
                UpdateVoiceLog("CreateSelectedNotes process completed ")
            End If
            InsertExamNotes()
            'Bug #80350: gloEMR: Referral letter- Application brigs focus to word document
            Try
                gloWord.WordDialogBoxBackgroundCloser.bringToTop(Me, True)

                'Dashboardform.ActiveDSO = wdReferrals
            Catch ex As Exception

            End Try
        Else
            rbSelect.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub rbNone_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbNone.CheckedChanged
        If rbNone.Checked Then
            rbSelect.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            rbNotes.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            rbNone.Font = gloGlobal.clsgloFont.gFont_BOLD  'New Font("Tahoma", 9, FontStyle.Bold)

            blnRb = True
            InsertExamNotes()
            'Bug #80350: gloEMR: Referral letter- Application brigs focus to word document
            Try
                gloWord.WordDialogBoxBackgroundCloser.bringToTop(Me, True)
                Dashboardform.ActiveDSO = wdReferrals
            Catch ex As Exception

            End Try
        Else
            rbNone.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub wdReferrals_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdReferrals.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                'Bug #71063: #00000729: Refferal Letter: Added Try Catch to handle exception please do not write code in catch
                Try

                    For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                        If (IsNothing(oFile) = False) Then
                            Try
                                If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                    Try
                                        oFile.Delete()
                                    Catch ex As Exception
                     gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                        ex = Nothing
                                    End Try
                                End If
                            Catch ex As Exception
                                

                            End Try
                        End If
                    Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
            ex = Nothing
        End Try
    End Sub

    Private Sub wdReferrals_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdReferrals.OnDocumentClosed
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing

                '20120726:: Bug no 31598 
                GloUC_AddRefreshDic1.Visible = False


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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Close, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
            ex = Nothing
        End Try
    End Sub

#End Region

#End Region

    Private Sub PrintHCFAReport()

        'Added By Shweta 20091205 
        'To print the report as per the setting in admin 
        If gblnICD9Driven = True Then
            'Added By Shweta 20091231
            Try
                ToolStripButton2.Enabled = False
                'Create report object to retrive the report 


                _ExamID = m_ExamId
                If _ExamID <> "" Then
                    'retrive related information to represent in report
                    Dim oICD9 As Rpt_HCFA_ICD9Driven = Nothing
                    Dim objClsHCFAReport As ClsHCFAReport = New ClsHCFAReport
                    oICD9 = objClsHCFAReport.CreateICD9Report(_ExamID)
                    objClsHCFAReport.Dispose()
                    objClsHCFAReport = Nothing
                    If oICD9 IsNot Nothing Then
                        'To print the report 
                        If gblnUseDefaultPrinter = False Then
                            '  PrintDialog1 = New PrintDialog()
                            If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                                oICD9.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                                oICD9.PrintToPrinter(1, False, 0, 0)
                            End If
                            ' PrintDialog1.Dispose()
                            'PrintDialog1 = Nothing
                        Else
                            oICD9.PrintToPrinter(1, False, 0, 0)
                        End If
                        oICD9.Dispose()
                        oICD9 = Nothing
                        ''
                    End If
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Print, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show("Error occured while printing the HCFA report." & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ex = Nothing
            Finally
                ToolStripButton2.Enabled = True
            End Try

            'End 20091231
        Else
            Try
                ToolStripButton2.Enabled = False
                'Added By Shweta 20091205


                _ExamID = m_ExamId
                'retrive related information to represent in report
                If _ExamID <> "" Then
                    Dim oCpt As rpt_CptDriven = Nothing
                    Dim objClsHCFAReport As ClsHCFAReport = New ClsHCFAReport
                    oCpt = objClsHCFAReport.CreateReport(_ExamID)
                    objClsHCFAReport.Dispose()
                    objClsHCFAReport = Nothing
                    'Added by Shweta 20091223
                    'Against the Bugzilla Id:1282
                    If oCpt IsNot Nothing Then
                        'To print the report
                        If gblnUseDefaultPrinter = False Then
                            ' PrintDialog1 = New PrintDialog()
                            If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                                oCpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                                oCpt.PrintToPrinter(1, False, 0, 0)
                                If Not IsNothing(oCpt) Then
                                    oCpt.Close()
                                End If
                            End If
                            'PrintDialog1.Dispose()
                            'PrintDialog1 = Nothing
                        Else
                            oCpt.PrintToPrinter(1, False, 0, 0)
                        End If
                        oCpt.Dispose()
                        oCpt = Nothing
                    End If
                End If
                'objClsHCFAReport = Nothing 'Change made to solve memory Leak and word crash issue
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Print, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show("Error occured while printing the HCFA report." & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ex = Nothing
            Finally
                ToolStripButton2.Enabled = True
            End Try
            'End Shweta 
        End If
    End Sub
    '--------------------------

    Private Sub C1Diagnosis_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Diagnosis.MouseDown
        If C1Diagnosis.Rows.Count > 1 Then
            Dim r As Integer = C1Diagnosis.HitTest(e.X, e.Y).Row
            If r > 0 Then
                C1Diagnosis.Select(r, True)
            End If
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If IsNothing(C1Diagnosis.GetData(C1Diagnosis.Row, Col_CPTCount)) Then
                    'Try
                    '    If (IsNothing(C1Diagnosis.ContextMenuStrip) = False) Then
                    '        C1Diagnosis.ContextMenuStrip.Dispose()
                    '        C1Diagnosis.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1Diagnosis.ContextMenuStrip = cntDiagnosis
                Else
                    'Try
                    '    If (IsNothing(C1Diagnosis.ContextMenuStrip) = False) Then
                    '        C1Diagnosis.ContextMenuStrip.Dispose()
                    '        C1Diagnosis.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1Diagnosis.ContextMenuStrip = Nothing
                End If
            Else
                'Try
                '    If (IsNothing(C1Diagnosis.ContextMenuStrip) = False) Then
                '        C1Diagnosis.ContextMenuStrip.Dispose()
                '        C1Diagnosis.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                C1Diagnosis.ContextMenuStrip = Nothing
            End If
        Else
            'Try
            '    If (IsNothing(C1Diagnosis.ContextMenuStrip) = False) Then
            '        C1Diagnosis.ContextMenuStrip.Dispose()
            '        C1Diagnosis.ContextMenuStrip = Nothing
            '    End If
            'Catch ex As Exception

            'End Try
            C1Diagnosis.ContextMenuStrip = Nothing
        End If


    End Sub

    Private Function fillDischargeDetails()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtDischargeDisposition As New DataTable
        Dim dtDiagnosisType As New DataTable
        Try
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@CodeSystemName", "DischargeDisposition", ParameterDirection.Input, SqlDbType.Text)

            oDB.Retrive("gsp_ShowDischargeDetails", oParam, dtDischargeDisposition)

            If dtDischargeDisposition.Rows.Count > 0 Then
                ddDischargeDisposition.DataSource = dtDischargeDisposition
                ddDischargeDisposition.ValueMember = "sCode"
                ddDischargeDisposition.DisplayMember = "sEMRDisplayName"
            End If

            oParam.Clear()
            oParam.Add("@CodeSystemName", "DiagnosisType", ParameterDirection.Input, SqlDbType.Text)
            oDB.Retrive("gsp_ShowDischargeDetails", oParam, dtDiagnosisType)

            If dtDiagnosisType.Rows.Count > 0 Then
                ddDiagnosisType.DataSource = dtDiagnosisType
                ddDiagnosisType.ValueMember = "sCode"
                ddDiagnosisType.DisplayMember = "sEMRDisplayName"

                ddDiagnosisType.Text = "Final"
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
        Return Nothing
    End Function

    Private Function GetDiagnosisCodesforExam() As DataTable
        Dim oDataBase As New DataBaseLayer
        Dim oResultICD9 As DataTable
        Dim StrSQL As String
        Try
            'Query for selecting ICD9 for current exam 
            StrSQL = "SELECT Distinct isnull(ExamICD9CPT.sICD9Code,'') as sICD9Code,  isnull(ExamICD9CPT.sICD9Description,'') as sICD9Description FROM ExamICD9CPT WHERE  ExamICD9CPT.nExamID = " & m_ExamId & " AND ExamICD9CPT.nVisitID = " & m_VisitId
            oResultICD9 = oDataBase.GetDataTable_Query(StrSQL)
            If Not oResultICD9 Is Nothing Then
                If oResultICD9.Rows.Count > 0 Then
                    Return oResultICD9
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            StrSQL = Nothing
            oDataBase.Dispose()
            oDataBase = Nothing
        End Try
    End Function
    Private Sub AssociateDiagnosis(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
        Try
            strExamName = oCurrentMenu.Text
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub wdReferrals_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdReferrals.OnDocumentOpened
        oCurDoc = wdReferrals.ActiveDocument
        oWordApp = oCurDoc.Application
        Try
            If oCurDoc Is Nothing Then
                GloUC_AddRefreshDic1.Visible = False
            Else
                GloUC_AddRefreshDic1.Visible = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        Try
            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        Try
            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
    End Sub

    Private Sub rbLastName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbLastName.CheckedChanged
        If rbLastName.Checked = True Then
            rbLastName.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rbFirstName.Font = gloGlobal.clsgloFont.gFont ' New Font("Tahoma", 9, FontStyle.Regular)
        Else
            rbLastName.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbFirstName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFirstName.CheckedChanged
        If rbFirstName.Checked = True Then
            rbFirstName.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rbLastName.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        Else
            rbFirstName.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtnNotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnNotes.CheckedChanged
        If rbtnNotes.Checked = True Then
            rbtnNotes.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rbtnNone.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            rbtnSelect.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        Else
            rbtnNotes.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtnSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSelect.CheckedChanged
        Try

            If (IsNothing(strSelectedFileNotes)) Then

                rbtnSelect.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                rbtnNotes.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
                rbtnNone.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)

                UpdateVoiceLog("CreateSelectedNotes process started ")

                strSelectedFileNotes = CreateSelectedNotes(NotesFileName)
                'Bug #80350: gloEMR: Referral letter- Application brigs focus to word document
                Try
                    'gloWord.WordDialogBoxBackgroundCloser.bringToTop(Me, True)
                    If (AfterLoad) Then
                        gloWord.WordDialogBoxBackgroundCloser.ForceWindowIntoForeground(Me.Handle)
                    End If

                    Dashboardform.ActiveDSO = wdReferrals
                Catch ex As Exception

                End Try


                UpdateVoiceLog("CreateSelectedNotes process completed ")
            Else
                rbtnSelect.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try


    End Sub
    Private Sub rbtnNone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnNone.CheckedChanged
        If rbtnNone.Checked = True Then
            rbtnNone.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rbtnNotes.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            rbtnSelect.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        Else
            rbtnNone.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)

        End If
    End Sub


    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub mnusetasPrimary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnusetasPrimary.Click
        Try
            C1Diagnosis.Rows(C1Diagnosis.Row).Node.Move(C1.Win.C1FlexGrid.NodeMoveEnum.First, C1Diagnosis.Rows(0).Node)
            Dim ICD9Count As Integer = 0
            For i As Integer = 1 To C1Diagnosis.Rows.Count - 1
                If IsNothing(C1Diagnosis.GetData(i, Col_CPTCount)) Then
                    ICD9Count = ICD9Count + 1
                    C1Diagnosis.SetData(i, Col_ICD9Count, ICD9Count)
                Else
                    C1Diagnosis.SetData(i, Col_ICD9Count, ICD9Count)
                End If
            Next
            saveDiagnosis()
            frmPatientExam.blnChangesMade = True
            If C1Diagnosis.Rows.Count > 1 Then
                strExamName = C1Diagnosis.GetData(1, Col_ICD9Code) & " " & C1Diagnosis.GetData(1, Col_ICD9Desc)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub saveDiagnosis()
        Try
            Dim arrExam As New ArrayList
            With C1Diagnosis
                .Col = Col_ICD9Code
                .Select()
                Dim i As Integer
                Dim lst As myList
                'Dim lstExam As myList

                Dim arrList As New ArrayList

                Dim strICD9Code As String = ""
                Dim strICD9Desc As String = ""
                Dim strCPTCode As String = ""
                Dim strCPTDesc As String = ""
                Dim strMODCode As String = ""
                Dim strMODDesc As String = ""
                Dim nICD9Count As Integer = 0
                Dim nCPTCount As Integer = 0
                Dim nModCount As Integer = 0
                Dim intUnits As System.Int64

                For i = 1 To .Rows.Count - 1
                    lst = New myList
                    Dim _Node As C1.Win.C1FlexGrid.Node

                    _Node = .Rows(i).Node


                    If _Node.Level = 1 Then
                        intUnits = .GetData(i, Col_Units)
                    End If
                    strICD9Code = .GetData(_Node.Row.Index, Col_ICD9Code)
                    strICD9Desc = .GetData(_Node.Row.Index, Col_ICD9Desc)
                    If _Node.Level = 0 Then
                        arrExam.Add(New mytable(strICD9Desc, strICD9Code))
                    End If
                    If _Node.Children = 0 Then
                        _Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)
                        Dim rowno As Integer = _Node.Row.Index

                        strICD9Code = .GetData(rowno, Col_ICD9Code)
                        strICD9Desc = .GetData(rowno, Col_ICD9Desc)
                        nICD9Count = .GetData(rowno, Col_ICD9Count)

                        strCPTCode = .GetData(rowno, COl_CPTCode)
                        strCPTDesc = .GetData(rowno, Col_CPTDesc)
                        nCPTCount = .GetData(rowno, Col_CPTCount)

                        strMODCode = .GetData(rowno, Col_ModCode)
                        strMODDesc = .GetData(rowno, Col_ModDesc)
                        nModCount = .GetData(rowno, Col_ModCount)

                        'list for ICD9,CPT and Modifier in ExamICD9CPT Table
                        lst.Code = strICD9Code
                        lst.Description = strICD9Desc
                        lst.HistoryCategory = strCPTCode
                        lst.HistoryItem = strCPTDesc
                        lst.Value = strMODCode
                        lst.ParameterName = strMODDesc
                        lst.TemplateResult = intUnits
                        lst.ICD9Count = nICD9Count
                        lst.CPTCount = nCPTCount
                        lst.ModCount = nModCount

                        'Bug #96645: 00001101 : Providers workflow: Creates an exam > adds DX codes (smart dx) > completes notes > On this screen marks primary code. Clicks smart DX and it defaults to ICD9 he clicks ICD10 tries to save codes and they get the mismatch error
                        lst.nICDRevision = .GetData(rowno, Col_ICDRevision)

                        arrList.Add(lst)
                    End If
                    'Change made to solve memory Leak and word crash issue
                    If Not lst Is Nothing Then
                        lst = Nothing
                    End If
                Next

                Dim oclsDiagnosis As New ClsDiagnosisDBLayer
                'save data in ExamICDCPT Table
                'oclsDiagnosis.SaveDiagTreatmentAssociation(m_ExamId, gnPatientID, m_VisitId, arrList)
                oclsDiagnosis.SaveDiagTreatmentAssociation(m_ExamId, m_PatientId, m_VisitId, arrList, Me)
                'end modification by dipak
                oclsDiagnosis.Dispose()
                oclsDiagnosis = Nothing
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub C1Followup_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Followup.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1RxSummary_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1RxSummary.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1Diagnosis_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Diagnosis.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Public Sub ShowMicroPhone1() Implements mdlgloVoice.gloVoice.ShowMicroPhone

    End Sub

    Public Sub TurnoffMicrophone1() Implements mdlgloVoice.gloVoice.TurnoffMicrophone

    End Sub

    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return CType(myCaller.MdiParent, MainMenu)
        End Get
    End Property


    Private Function GetReferralDetail(ByVal ID As Int64) As DataTable
        Dim odb As New DataBaseLayer
        Dim dt As DataTable
        Try
            Dim _Sql As String = " SELECT ISNULL(sContact,'') as sContact ,ISNULL(sAddressLine1,'') AS sAddressLine1, " _
    & " ISNULL(sAddressLine2,'') AS sAddressLine2, ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState ,ISNULL(sZIP,'') AS sZIP, " _
    & " ISNULL(sPhone,'') AS sPhone,ISNULL(sMobile,'') AS sMobile,ISNULL(sFirstName,'') AS sFirstName, " _
    & "ISNULL(sMiddleName,'') As sMiddleName,ISNULL(sLastName,'') AS sLastName,ISNULL(sFax,'') AS sFax,ISNULL(sExternalCode,'') AS sExternalCode, " _
    & "ISNULL(sGender,'') AS sGender ,ISNULL(sDegree,'') AS  sDegree,nContactFlag FROM Patient_DTL WHERE nContactId = " & ID & " "
            dt = odb.GetDataTable_Query(_Sql)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            odb.Dispose()
            odb = Nothing
        End Try

    End Function

    Private Sub ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton9.Click

    End Sub

    Private Sub gloUCTreatment_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gloUCTreatment.MouseDown
        Try
            Try

                If IsNothing(gloUCTreatment.ContextMenu) = False Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(gloUCTreatment.ContextMenu)
                    If (IsNothing(gloUCTreatment.ContextMenu.MenuItems) = False) Then
                        gloUCTreatment.ContextMenu.MenuItems.Clear()
                    End If
                    gloUCTreatment.ContextMenu.Dispose()
                    gloUCTreatment.ContextMenu = Nothing
                End If




            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            gloUCTreatment.ContextMenu = Nothing
            If gloUCTreatment.RowSel > 0 AndAlso e.Button = Windows.Forms.MouseButtons.Right Then
                If gloUCTreatment.isDiagnosisColumn(gloUCTreatment.ColSel) Then

                    If gloUCTreatment.SelectedDiagnosis.Trim <> "" Then
                        Dim oContextMenu As New ContextMenu
                        Dim oMenuItem As New MenuItem
                        oMenuItem.Text = "Set as primary diagnosis"
                        AddHandler oMenuItem.Click, AddressOf OnPrimaryDiagnosis_Click
                        oContextMenu.MenuItems.Add(oMenuItem)
                        Try
                            If IsNothing(gloUCTreatment.ContextMenu) = False Then
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(gloUCTreatment.ContextMenu)
                                If (IsNothing(gloUCTreatment.ContextMenu.MenuItems) = False) Then
                                    gloUCTreatment.ContextMenu.MenuItems.Clear()
                                End If
                                gloUCTreatment.ContextMenu.Dispose()
                                gloUCTreatment.ContextMenu = Nothing
                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try
                        gloUCTreatment.ContextMenu = oContextMenu

                        'Change made to solve memory Leak and word crash issue
                        oContextMenu = Nothing
                        oMenuItem = Nothing
                    End If

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub OnPrimaryDiagnosis_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If gloUCTreatment.SelectedDiagnosis.Trim <> "" Then strExamName = gloUCTreatment.SelectedDiagnosis.Trim

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Public Function GetReferrals() As DataTable
        Dim dt As DataTable
        Dim oDB As New DataBaseLayer
        Try
            Dim strSQL As String
            Dim strsearch As String = txtSearchReferrals.Text.Trim()
            Dim strSearchArray As String() = Nothing
            Dim sFilter As String = ""

            strsearch = strsearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]")

            If strsearch.StartsWith("*") = True Then
                strsearch = strsearch.Replace("*", "%")
            End If

            strsearch = strsearch.Replace("*", "[*]")

            If strsearch.Length > 1 Then
                Dim str As String = strsearch.Substring(1)
                strsearch = strsearch.Substring(0, 1) + str
            End If
            If strsearch.Trim() <> "" Then
                strSearchArray = strsearch.Split(",")
            End If

            If (strsearch <> "") Then

                'ADDED BY SHUBHANGI COZ WE ARE DISPLAYING THE PREFIX & SUFFIX IF THEY ARE PRESENT 20100608
                strSQL = " SELECT TOP 40  Contacts_MST.nContactID," _
                       & "REPLACE(LTRIM(CASE WHEN Contacts_Physician_DTL.sPrefix <>   '' THEN ISNULL(Contacts_Physician_DTL.sPrefix,'') +SPACE(1) " _
                       & "WHEN Contacts_Physician_DTL.sPrefix <>  Null THEN ISNULL(Contacts_Physician_DTL.sPrefix,'') +SPACE(1) ELSE '' END+ISNULL(Contacts_MST.sFirstName + SPACE(1), '')) + LTRIM(ISNULL(Contacts_MST.sMiddleName + SPACE(1), '')) + LTRIM(ISNULL(Contacts_MST.sLastName + SPACE(1), ''))+SPACE(1)+ CASE WHEN Contacts_Physician_DTL.sDegree <>   '' THEN ISNULL(Contacts_Physician_DTL.sDegree,'') +SPACE(1) " _
                       & "WHEN Contacts_Physician_DTL.sDegree <>  Null THEN ISNULL(Contacts_Physician_DTL.sDegree,'') ELSE '' END  + ', ' + ISNULL(Contacts_MST.sDegree + SPACE(1), ' '), ',  ', '') AS 'Name',  " _
                       & "ISNULL(Contacts_MST.sFirstName, '') AS FirstName, " _
                       & "ISNULL(Contacts_MST.sMiddleName, '') AS MiddleName, ISNULL(Contacts_MST.sLastName, '') AS LastName,  ISNULL(Contacts_MST.sGender,'') AS Gender, " _
                       & "ISNULL(Contacts_MST.sDegree,'') AS Degree,ISNULL(Contacts_MST.sStreet,'') AS Street,ISNULL(Contacts_MST.sAddressLine2,'') AS AddressLine2, " _
                       & "ISNULL(Contacts_MST.sCity,'') AS City,ISNULL(Contacts_MST.sState,'') AS State,ISNULL(Contacts_MST.sZIP,'') AS ZIP,ISNULL(Contacts_MST.sPhone,'') AS Phone, " _
                       & "ISNULL(Contacts_MST.sFax,'') AS Fax,ISNULL(Contacts_MST.sMobile,'') AS Mobile,ISNULL(Contacts_MST.sExternalCode,'') AS ExternalCode  " _
                       & " FROM Contacts_MST left outer join Contacts_Physician_DTL " _
                       & "ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID WHERE (Contacts_MST.sContactType = 'Physician') AND  (IsNull(Contacts_MST.bIsBlocked,0) = 'false') AND isnull(Contacts_MST.sSPI,'') ='' " '16-Apr-13 Aniket: Fixing Bug 49364


                If strSearchArray.Length = 1 Then
                    sFilter = "AND (Contacts_Physician_DTL.sPrefix like ('" & strsearch & "%') OR Contacts_MST.sFirstName like ('" & strsearch & "%')OR Contacts_MST.sMiddleName like ('" & strsearch & "%') OR Contacts_MST.sLastName like ('" & strsearch & "%') OR Contacts_MST.sDegree like ('" & strsearch & "%') OR Contacts_Physician_DTL.sDegree like ('" & strsearch & "%'))"
                Else
                    'For Comma separated value search
                    For i As Integer = 0 To strSearchArray.Length - 1
                        strsearch = strSearchArray(i).Trim()
                        If strsearch.Trim() <> "" Then
                            If i = 0 Then
                                sFilter = sFilter + " AND "
                                sFilter = sFilter + " ( " & " Contacts_Physician_DTL.sPrefix like ('" & strsearch & "%') OR Contacts_MST.sFirstName like ('" & strsearch & "%')OR Contacts_MST.sMiddleName like ('" & strsearch & "%') OR Contacts_MST.sLastName like ('" & strsearch & "%') OR Contacts_MST.sDegree like ('" & strsearch & "%') OR Contacts_Physician_DTL.sDegree like ('" & strsearch & "%') )"
                            Else
                                If sFilter <> "" Then

                                    sFilter = sFilter + " AND "
                                    sFilter = sFilter + " (" & "  Contacts_Physician_DTL.sPrefix like ('" & strsearch & "%') OR Contacts_MST.sFirstName like ('" & strsearch & "%')OR Contacts_MST.sMiddleName like ('" & strsearch & "%') OR Contacts_MST.sLastName like ('" & strsearch & "%') OR Contacts_MST.sDegree like ('" & strsearch & "%') OR Contacts_Physician_DTL.sDegree like ('" & strsearch & "%'))"
                                End If
                            End If
                        End If
                    Next

                End If
                strSQL = strSQL + sFilter
            Else
                'ADDED BY SHUBHANGI COZ WE ARE DISPLAYING THE PREFIX & SUFFIX IF THEY ARE PRESENT 20100608
                strSQL = " SELECT TOP 40  Contacts_MST.nContactID," _
                        & "REPLACE(LTRIM(CASE WHEN Contacts_Physician_DTL.sPrefix <>   '' THEN ISNULL(Contacts_Physician_DTL.sPrefix,'') +SPACE(1) " _
                        & "WHEN Contacts_Physician_DTL.sPrefix <>  Null THEN ISNULL(Contacts_Physician_DTL.sPrefix,'') +SPACE(1) ELSE '' END+ISNULL(Contacts_MST.sFirstName + SPACE(1), '')) + LTRIM(ISNULL(Contacts_MST.sMiddleName + SPACE(1), '')) + LTRIM(ISNULL(Contacts_MST.sLastName + SPACE(1), ''))+SPACE(1)+ CASE WHEN Contacts_Physician_DTL.sDegree <>   '' THEN ISNULL(Contacts_Physician_DTL.sDegree,'') +SPACE(1) " _
                        & "WHEN Contacts_Physician_DTL.sDegree <>  Null THEN ISNULL(Contacts_Physician_DTL.sDegree,'') ELSE '' END  + ', ' + ISNULL(Contacts_MST.sDegree + SPACE(1), ' '), ',  ', '') AS 'Name',  " _
                        & "ISNULL(Contacts_MST.sFirstName, '') AS FirstName, " _
                        & "ISNULL(Contacts_MST.sMiddleName, '') AS MiddleName, ISNULL(Contacts_MST.sLastName, '') AS LastName,  ISNULL(Contacts_MST.sGender,'') AS Gender, " _
                        & "ISNULL(Contacts_MST.sDegree,'') AS Degree,ISNULL(Contacts_MST.sStreet,'') AS Street,ISNULL(Contacts_MST.sAddressLine2,'') AS AddressLine2, " _
                        & "ISNULL(Contacts_MST.sCity,'') AS City,ISNULL(Contacts_MST.sState,'') AS State,ISNULL(Contacts_MST.sZIP,'') AS ZIP,ISNULL(Contacts_MST.sPhone,'') AS Phone, " _
                        & "ISNULL(Contacts_MST.sFax,'') AS Fax,ISNULL(Contacts_MST.sMobile,'') AS Mobile,ISNULL(Contacts_MST.sExternalCode,'') AS ExternalCode  " _
                        & " FROM Contacts_MST left outer join Contacts_Physician_DTL " _
                        & "ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID WHERE (Contacts_MST.sContactType = 'Physician') AND  (IsNull(Contacts_MST.bIsBlocked,0) = 'false') AND isnull(Contacts_MST.sSPI,'') =''      " '16-Apr-13 Aniket: Fixing Bug 49364
            End If
            dt = oDB.GetDataTable_Query(strSQL)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientId  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
    Public Sub calltoAddRefreshButtonControl()
        Dim dtReferrals As New DateTimePicker()
        dtReferrals.Value = GetVisitdate(m_VisitId)
        ObjWord = New clsWordDocument

        Refresh_UCobjCriteria = New DocCriteria
        Refresh_UCobjCriteria.DocCategory = enumDocCategory.Referrals
        Refresh_UCobjCriteria.PatientID = m_PatientId
        Refresh_UCobjCriteria.VisitID = m_VisitId
        Refresh_UCobjCriteria.PrimaryID = CurrentDocReferralID
        GloUC_AddRefreshDic1.CONNECTIONSTRINGs = GetConnectionString()
        ObjWord.DocumentCriteria = Refresh_UCobjCriteria
        ObjWord.WaitControlPanel = Me.Panel1
        GloUC_AddRefreshDic1.OBJWORDs = ObjWord
        Try
            If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                    DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                    GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
                End If
            End If

        Catch
        End Try
        GloUC_AddRefreshDic1.OBJCRITERIAs = Refresh_UCobjCriteria
        GloUC_AddRefreshDic1.M_PATIENTIDs = m_PatientId
        GloUC_AddRefreshDic1.ObjFrom = Me

        If (GloUC_AddRefreshDic1.dtLetterAllocated) Then
            Try
                If (IsNothing(GloUC_AddRefreshDic1.DTLETTERDATEs) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_AddRefreshDic1.DTLETTERDATEs)
                    Catch ex As Exception

                    End Try


                    GloUC_AddRefreshDic1.DTLETTERDATEs.Dispose()
                    GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing

                End If
            Catch
            End Try
            GloUC_AddRefreshDic1.dtLetterAllocated = False
        End If

        GloUC_AddRefreshDic1.DTLETTERDATEs = dtReferrals
        GloUC_AddRefreshDic1.dtLetterAllocated = True
        GloUC_AddRefreshDic1.OWORDAPPs = oWordApp
        GloUC_AddRefreshDic1.wdPatientWordDocs = wdReferrals

        ObjWord = Nothing   'Change made to solve memory Leak and word crash issue
    End Sub

    Private Sub tlsRecommendation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlsRecommendation.Click
        Try
            If m_PatientId <= 0 Then
                Exit Sub
            End If

            If IsAccess(False) = False Then
                Exit Sub
            End If

            Try
                Dim _toSendPatLst As New Collection
                _toSendPatLst.Add(m_PatientId)
                Dim frmTemplate As New frmDM_DisplayRecommendations(_toSendPatLst, True, m_VisitId)

                With frmTemplate
                    .ShowInTaskbar = False
                    .WindowState = FormWindowState.Maximized
                    .ShowDialog(IIf(IsNothing(frmTemplate.Parent), Me, frmTemplate.Parent))
                    .Dispose()
                End With
                ShowRecommendationsAlert()
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK)
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Public Function IsAccess(Optional ByVal bSkpMsg As Boolean = False, Optional ByVal nPatientID As Long = 0, Optional ByVal AllowScreenAccess As Boolean = False, Optional ByVal bGetLatestStatus As Boolean = False) As Boolean
        If nPatientID = 0 Then
            nPatientID = m_PatientId
        End If
        IsAccess = True
        If gbAllowEmergencyAccess = False Then

            Dim oclsPatReg As New ClsPatientRegistrationDBLayer
            Dim _sPatientStatus As String = oclsPatReg.PatientStatus(nPatientID, "").ToString()
            IsAccess = CheckPatientStatus_new(_sPatientStatus, bSkpMsg, AllowScreenAccess)
            oclsPatReg.Dispose()
            oclsPatReg = Nothing
        End If
        Return IsAccess
    End Function


    Private blnDisposed As Boolean

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not (Me.blnDisposed) Then
            If (disposing) Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If (GloUC_AddRefreshDic1.dtLetterAllocated) Then
                    Try
                        Dim dtpControls As DateTimePicker() = {GloUC_AddRefreshDic1.DTLETTERDATEs}
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                            gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                        Catch ex As Exception

                        End Try
                    Catch
                    End Try
                    GloUC_AddRefreshDic1.dtLetterAllocated = False
                End If
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                Dim dtpContextMenu As ContextMenu() = {CntReferrals, CntPatientReferrals, mnuFAXClose, mnuFAXAllPriority, CntICD9}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenu)

                Catch ex As Exception

                End Try
                Dim dtpContextMenustrip As ContextMenuStrip() = {cntDiagnosis}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenustrip)

                Catch ex As Exception

                End Try

                Try
                    gloGlobal.cEventHelper.DisposeContextMenu(dtpContextMenu)
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(dtpContextMenustrip)
                Catch ex As Exception

                End Try


                Try
                    If (IsNothing(_PatientStrip) = False) Then
                        _PatientStrip.Dispose()
                        _PatientStrip = Nothing
                    End If
                Catch ex As Exception

                End Try
                DisposeDeclaredMembers()
                Try
                    If (IsNothing(PrintDialog1) = False) Then
                        PrintDialog1.Dispose()
                        PrintDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                        If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                            DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                            GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
                        End If
                    End If

                Catch
                End Try

            End If
        End If
        Me.blnDisposed = True
        Try
            MyBase.Dispose(disposing)
        Catch ex As Exception

        End Try
    End Sub


    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue to prevent finalization code for this object from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Private Sub DisposeDeclaredMembers()

        strPatientCode = Nothing
        strPatientFirstName = Nothing
        strPatientLastName = Nothing
        strPatientDOB = Nothing
        strPatientAge = Nothing
        strPatientGender = Nothing
        strPatientMaritalStatus = Nothing
        strSelectedFileNotes = Nothing
        _ListType = Nothing
        NotesFileName = Nothing
        ImagePath = Nothing
        m_status = Nothing
        _TemplateName = Nothing
        _PrinterName = Nothing

        strExamNamebeforvisit = Nothing
        strExamName = Nothing
        _ExamID = Nothing
        strSelectNode = Nothing
        strICD9Code = Nothing
        strICD9Desc = Nothing
        strRefFileName = Nothing

        'gReferralArrlist.Clear()
        'gReferralArrlist = Nothing

        'arrlist.Clear()
        'arrlist = Nothing

        'objReferralsDBLayer = Nothing
        If IsNothing(objReferralsDBLayer) = False Then
            objReferralsDBLayer.Dispose()
            objReferralsDBLayer = Nothing
        End If
        If IsNothing(objclsMessage) = False Then
            objclsMessage.Dispose()
            objclsMessage = Nothing
        End If

        

         

        If IsNothing(ObjWord) = False Then
            ObjWord = Nothing
        End If

        If IsNothing(oProvider) = False Then
            oProvider.Dispose()
            oProvider = Nothing
        End If

        If IsNothing(_PatientStrip) = False Then
            _PatientStrip.Dispose() : _PatientStrip = Nothing
        End If

        'If Not IsNothing(oRpt) Then
        '    oRpt.Close()
        '    oRpt.Dispose()
        '    oRpt = Nothing
        'End If

        '  If Not myNode Is Nothing Then myNode.Dispose() : 
        myNode = Nothing


    End Sub
    Private Sub Fill_ProblemList()
        Dim dtPatientProblemListDetails As DataTable
        Dim objProblemList As New clsPatientProblemList

        dtPatientProblemListDetails = objProblemList.Fill_ActiveProblemLists(m_PatientId)



        objProblemList.Dispose()
        objProblemList = Nothing

        With C1ProblemList
            .Visible = True
            .BringToFront()
            .Cols.Count = 20 '8
            .Rows.Count = 1
            .Rows.Fixed = 1
            '' Set Fixed Rows
            .SetData(0, 0, "ProblemID")
            .SetData(0, 1, "DOS")
            '.SetData(0, 2, "Chief Complaint")
            .SetData(0, 2, "Description")
            .SetData(0, 3, "SnoMed CT ID")
            .SetData(0, 4, "Diagnosis")

            .SetData(0, 5, "VisitID")
            .SetData(0, 6, "Status")
            ''.SetData(0, 7, "User")
            .SetData(0, 7, "Immediacy")
            .SetData(0, 8, "Resolved Date")
            .SetData(0, 9, "UserID")
            '' .SetData(0, 10, "Immediacy")
            .SetData(0, 10, "User")

            .SetData(0, 11, "Provider")
            .SetData(0, 12, "Location")
            .SetData(0, 13, "Last Update")

            .SetData(0, 14, "ExamID")

            .SetData(0, 15, "Problem Type")
            .SetData(0, 16, "SnoMed CT ID")
            .SetData(0, 17, "SnoMed ID")
            .SetData(0, 18, "Description ID")
            .SetData(0, 19, "Defination")

            Dim _width As Integer = pnlProblemList.Width

            .Cols(0).Width = 0
            .Cols(1).Width = _width * 0.1
            .Cols(2).Width = _width * 0.45
            ''.Cols(3).Width = _width * 0.05
            '' .Cols(4).Width = 0 ''_width * 0.3
            .Cols(5).Width = 0
            .Cols(6).Width = _width * 0.08
            ''.Cols(7).Width = 0 ''_width * 0.1
            .Cols(8).Width = 0 ''_width * 0.15
            .Cols(9).Width = 0 ''_width * 0
            .Cols(10).Width = _width * 0.1
            .Cols(11).Width = 0 ''_width * 0.15
            .Cols(12).Width = 0 '' _width * 0.15
            .Cols(13).Width = 0 ''_width * 0.11
            .Cols(14).Width = 0 '' _width * 0.0
            .Cols(15).Width = 0 ''_width * 0.1
            .Cols(16).Width = 0 '' _width * 0.1
            .Cols(17).Width = _width * 0
            .Cols(18).Width = 0 ''_width * 0
            .Cols(19).Width = 0 '' _width * 0

            _width = Nothing


            .Cols(0).TextAlign = TextAlignEnum.LeftCenter
            .Cols(1).TextAlign = TextAlignEnum.LeftCenter
            .Cols(2).TextAlign = TextAlignEnum.LeftCenter
            .Cols(3).TextAlign = TextAlignEnum.LeftCenter
            .Cols(4).TextAlign = TextAlignEnum.LeftCenter
            .Cols(5).TextAlign = TextAlignEnum.LeftCenter
            .Cols(6).TextAlign = TextAlignEnum.LeftCenter
            .Cols(7).TextAlign = TextAlignEnum.LeftCenter
            .Cols(8).TextAlign = TextAlignEnum.LeftCenter
            .Cols(9).TextAlign = TextAlignEnum.LeftCenter
            .Cols(10).TextAlign = TextAlignEnum.LeftCenter
            .Cols(11).TextAlign = TextAlignEnum.LeftCenter
            .Cols(12).TextAlign = TextAlignEnum.LeftCenter
            .Cols(13).TextAlign = TextAlignEnum.LeftCenter
            .Cols(14).TextAlign = TextAlignEnum.LeftCenter
            .Cols(15).TextAlign = TextAlignEnum.LeftCenter
            .Cols(16).TextAlign = TextAlignEnum.LeftCenter

            If IsNothing(dtPatientProblemListDetails) = False Then
                For i As Int16 = 0 To dtPatientProblemListDetails.Rows.Count - 1
                    Dim forecolor As Color
                    'Dim backcolor As Color
                    Dim status As String = ""

                    ''If dtPatientProblemListDetails.Rows(i)("Status") = frmProblemList.Status.Active Then
                    '    forecolor = Color.Red
                    '    status = "Active"
                    'ElseIf dtPatientProblemListDetails.Rows(i)("Status") = frmProblemList.Status.Resolved Then
                    '    forecolor = Color.Green
                    '    status = "Resolved"
                    'ElseIf dtPatientProblemListDetails.Rows(i)("Status") = frmProblemList.Status.Inactive Then
                    '    forecolor = Color.Blue
                    '    status = "Inactive"
                    'ElseIf dtPatientProblemListDetails.Rows(i)("Status") = frmProblemList.Status.Chronic Then
                    '    forecolor = Color.Black
                    '    status = "Chronic"
                    'End If

                    If dtPatientProblemListDetails.Rows(i)("VisitID") IsNot Nothing Then

                        If Convert.ToInt64(dtPatientProblemListDetails.Rows(i)("VisitID")) = m_VisitId Then
                            forecolor = Color.FromArgb(215, 226, 253)
                            status = "New"
                        Else
                            forecolor = Color.White
                            status = "Active"
                        End If
                    End If


                    Dim r As C1.Win.C1FlexGrid.Row
                    r = .Rows.Add()
                    r.StyleNew.BackColor = forecolor
                    r.Height = 20
                    .SetData(r.Index, 0, dtPatientProblemListDetails.Rows(i)("nProblemID"))
                    .SetData(r.Index, 1, dtPatientProblemListDetails.Rows(i)("dtDOS"))
                    .SetData(r.Index, 3, dtPatientProblemListDetails.Rows(i)("sConceptID"))
                    .SetData(r.Index, 4, dtPatientProblemListDetails.Rows(i)("Diagnosis"))
                    .SetData(r.Index, 5, dtPatientProblemListDetails.Rows(i)("VisitID"))
                    .SetData(r.Index, 6, status)
                    .SetData(r.Index, 7, dtPatientProblemListDetails.Rows(i)("Immediacy"))
                    '' .SetData(r.Index, 7, dtPatientProblemListDetails.Rows(i)("UserName"))
                    .SetData(r.Index, 8, dtPatientProblemListDetails.Rows(i)("ResolvedDt"))
                    .SetData(r.Index, 9, dtPatientProblemListDetails.Rows(i)("nUserID"))
                    .SetData(r.Index, 10, dtPatientProblemListDetails.Rows(i)("UserName"))
                    '' .SetData(r.Index, 10, dtPatientProblemListDetails.Rows(i)("Immediacy"))
                    .SetData(r.Index, 11, dtPatientProblemListDetails.Rows(i)("Provider"))
                    .SetData(r.Index, 12, dtPatientProblemListDetails.Rows(i)("Location"))

                    If Not IsNothing(dtPatientProblemListDetails.Rows(i)("ModifiedDate")) Then
                        If Not dtPatientProblemListDetails.Rows(i)("ModifiedDate").ToString().Contains("4/12/1900") Then
                            .SetData(r.Index, 13, dtPatientProblemListDetails.Rows(i)("ModifiedDate"))
                        End If
                    End If

                    .SetData(r.Index, 14, dtPatientProblemListDetails.Rows(i)("ExamID"))
                    .SetData(r.Index, 15, dtPatientProblemListDetails.Rows(i)("sTransactionID1"))
                    .SetData(r.Index, 16, dtPatientProblemListDetails.Rows(i)("sConceptID"))
                    .SetData(r.Index, 17, dtPatientProblemListDetails.Rows(i)("sSnoMedID"))
                    .SetData(r.Index, 18, dtPatientProblemListDetails.Rows(i)("sDescriptionID"))
                    .SetData(r.Index, 19, dtPatientProblemListDetails.Rows(i)("sDescription"))

                    'added for showing chief complaint
                    Dim _strComplaints As String = ""
                    Dim _Comments() As String
                    Dim _nCommentCount As Integer = 0

                    If Not IsNothing(dtPatientProblemListDetails.Rows(i)("Comments")) Then
                        If dtPatientProblemListDetails.Rows(i)("Comments") <> "" Then
                            _strComplaints = dtPatientProblemListDetails.Rows(i)("Complaint") & vbNewLine & dtPatientProblemListDetails.Rows(i)("Comments")
                            _Comments = Split(dtPatientProblemListDetails.Rows(i)("Comments"), vbNewLine)
                            _nCommentCount = _Comments.Length + 1
                        Else
                            _strComplaints = dtPatientProblemListDetails.Rows(i)("Complaint")
                        End If
                    Else
                        _strComplaints = dtPatientProblemListDetails.Rows(i)("Complaint")
                    End If
                    .Rows(.Rows.Count - 1).AllowResizing = AllowDraggingEnum.Both
                    .Rows(.Rows.Count - 1).AllowDragging = DrawModeEnum.OwnerDraw
                    If _nCommentCount <> 0 Then
                        .Rows(.Rows.Count - 1).Height = .Rows.DefaultSize * _nCommentCount
                    End If
                    .SetData(r.Index, 2, _strComplaints)

                    _strComplaints = Nothing
                    _Comments = Nothing
                    _nCommentCount = Nothing

                    r = Nothing
                    forecolor = Nothing
                    status = Nothing

                Next
            End If
        End With
        C1ProblemList.Sort(SortFlags.Descending, 6)
        With C1ProblemList

            For i As Int16 = 0 To .Cols.Count - 1
                .Cols(i).AllowEditing = False
            Next

            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None

        End With

        If Not IsNothing(dtPatientProblemListDetails) Then
            dtPatientProblemListDetails.Dispose()
            dtPatientProblemListDetails = Nothing
        End If

    End Sub

#Region "Call CDA Function from Dashboard"
    'Public Delegate Sub GenerateCDAFromSummaryOfVisit(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromSummaryOfVisit(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromSummaryOfVisit(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromSummaryOfVisit(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
            ex = Nothing
        End Try
    End Sub
#End Region



    Private Sub C1Followup_ValidateEdit(sender As System.Object, e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles C1Followup.ValidateEdit

        Dim comboStyle As CellStyle

        Try

            If C1Followup.Rows.Count > 0 Then
                If e.Col = Col_User Then

                    Try

                        If (C1Followup.Styles.Contains("comboStyle")) Then
                            comboStyle = C1Followup.Styles("comboStyle")
                        Else
                            comboStyle = C1Followup.Styles.Add("comboStyle")
                        End If

                    Catch ex As Exception
                        comboStyle = C1Followup.Styles.Add("comboStyle")
                    End Try

                    'Dim comboStyle As CellStyle = C1Followup.Styles.Add("comboStyle")

                    Dim comboVal As Object = (CType(Me.C1Followup.Editor, ComboBox)).SelectedItem
                    If Convert.ToString(comboVal) = "More Users...." Then
                        With C1Followup
                            dtUser.DefaultView.RowFilter = ""
                            Dim UserList As New ListDictionary
                            For i As Int32 = 0 To dtUser.Rows.Count - 1
                                '  strUsers = strUsers & "|" & dtUser.Rows(i)(1)
                                UserList.Add(dtUser.Rows(i)(0), dtUser.Rows(i)(1))
                            Next
                            comboStyle.DataMap = UserList
                            .SetCellStyle(e.Row, e.Col, comboStyle)
                        End With
                        dtUser.DefaultView.RowFilter = ""
                        bIsMoreUsersExpanded = True
                    End If
                End If
                End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
            ex = Nothing
        End Try
    End Sub

    Private Sub C1Followup_AfterEdit(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Followup.AfterEdit
        Try
            If C1Followup.Rows.Count > 0 Then
                If e.Col = Col_User Then
                    If bIsMoreUsersExpanded Then
                        C1Followup.SetData(e.Row, Col_User, "Default")
                        bIsMoreUsersExpanded = False
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
            ex = Nothing
        End Try
    End Sub
End Class

