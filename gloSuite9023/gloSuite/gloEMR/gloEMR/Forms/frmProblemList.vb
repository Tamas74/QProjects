Imports C1.Win.C1FlexGrid
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMR.gloEMRWord
Imports System.Data.SqlClient
Imports System.Collections.Generic


Public Class frmProblemList
    Inherits frmBaseForm

    Implements IPatientContext
    '  Dim _isSavedClicked As Boolean = False
    ''SalesForceCase #11597 : GLO2011-0012477 : Problem list not updating on exam''''''''''''''''''''
    ''Provided the delegate to update the Problem List when form is closed
    Public Delegate Sub OnProblemList_Closed()
    Public Event ProblemListClosed As OnProblemList_Closed
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public _isFormClose As Boolean = False ''new variable taken to resolve bug 10324, set this variable on form closed event
    Dim Row_Cnt As Integer = 0
    Dim _PatientID As Long = 0
    Dim nReconcillationType As Int16 = 1
    Public Shared _chkInstPatientId As Long = 0
    Dim _ProblemID As Long = 0
    Dim _VisitID As Long = 0
    Dim _blnRecordLock As Boolean
    Dim _TemplateName As String
    Dim _TemplateID As Int64 = 0
    '''' Column No
    Dim COL_SELECT As Integer = 0 '' Chetan Added Change Value From Here by 1 for Drug
    Dim COL_PROBLEMID As Integer = 1
    Dim COL_VISITID As Integer = 2
    Dim COL_ProblemSatus As Integer = 3
    Dim COL_DOS As Integer = 4
    Dim COL_COMPLAINTS As Integer = 5
    Dim COL_ConceptID As Integer = 6
    Dim COL_DIAGNOSIS As Integer = 7
    Dim COL_INFOBUTTON As Integer = 8
    'Dim COL_PROVIDEREDUCATIONBUTTON As Integer = 8
    'Dim COL_PATIENTEDUCATIONBUTTON As Integer = 9
    Dim COL_DIAGNOSISBUTTON = 9
    Dim COL_PRESCRIPTION As Integer = 10
    Dim COL_RxBUTTON = 11

    Dim Col_UserName As Integer = 12 '' chetan added on 25-nov-2010 for disp user name
    ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 130110
    Dim COL_RsDt As Integer = 13
    Dim IsOpenNewExam As Boolean = False
    ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 130110
    Dim COL_USER As Integer = 14
    Dim COL_EXAMNAMEBUTTON As Integer = 15

    Dim COL_Immediacy As Integer = 16
    Dim COL_Provider As Integer = 17
    Dim COL_Location As Integer = 18
    Dim COL_LastModified As Integer = 19
    Dim COL_ExamID As Integer = 20
    Dim COL_ProblemType As Integer = 21

    Dim COL_DescriptionID As Integer = 22
    Dim COL_SnoMedID As Integer = 23
    Dim COL_Defination As Integer = 24
    Dim Col_HiddedPrescription As Integer = 25
    Dim Col_EncounterDiagnosis As Integer = 26
    Dim Col_IsModifed As Integer = 27
    Dim Col_ICDRevision As Int16 = 28  ''added for ICD10 Implementation 
    Dim Col_ReasonConceptID As Integer = 29
    Dim Col_ReasonConceptDesc As Integer = 30

    Dim Col_DischargeDate As Integer = 31
    Private col_ConcernStatus As Integer = 32
    Private Col_CDAProblemType As Integer = 33


    Dim COLUMN_COUNT As Int16 = 34
    Dim _isFormLoad As Boolean = False
    '' 
    Dim DeletedProblemlist As ArrayList
    Dim nExamId As Long = 0
    Dim STR_ICD9DESC As String = String.Empty
    Private nodesubtypetext As String = ""
    'Memory Leak
    'Dim arrICD9 As New ArrayList
    Dim strDia, strRx As String
    Private blnMoving As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tblPraoblemList As gloToolStrip.gloToolStrip  ''swaraj 20100612
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblAge As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblPatientCode As System.Windows.Forms.Label
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents pnlpatientControl As System.Windows.Forms.Panel
    Friend WithEvents tlsHealthPlan As System.Windows.Forms.ToolStripButton

    Private WithEvents dgCustomGrid As CustomTask
    Private WithEvents dgCustomGridselectExam As CustomTask
    Friend WithEvents pnlcustomTask As System.Windows.Forms.Panel
    Private Col_Check As Integer = 2
    Private Col_DrugName As Integer = 0
    Private Col_Dosage As Integer = 1
    Private Col_Count As Integer = 3
    Dim _Temprow As Int32
    Dim _TempRx As String
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Dim cmbboxSatusInt As Integer = 0
    Private WithEvents oMenu As System.Windows.Forms.ToolStripItem

    'Dim datetimeFromStr As String
    ' Dim datetimeToStr As String
    Public Shared blnOpenFromExam As Boolean
    Public Shared blnRxMedFromExam As Boolean
    Public Shared ArrRx_Problem As Dictionary(Of String, String) = Nothing
    Public Shared blnRxMedToProblem As Boolean = False '' Added For Maintaining status if this form Open From Prescription
    ' Dim frm As frmAddProblemList
    Dim strDMAlert As String = ""
    Friend WithEvents pnlAlert As System.Windows.Forms.Panel
    Friend WithEvents lblAlert As System.Windows.Forms.Label
    Friend WithEvents picAlert As System.Windows.Forms.PictureBox
    Dim dtProblems As DataTable
    Friend WithEvents pnlc1ProblemList As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip

    Public Shared Diagonsis As String = ""
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlb_New As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlb_Edit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlb_Delete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlb_Annotate As System.Windows.Forms.ToolStripButton
    Private objclsgeneral As gloSnoMed.ClsGeneral

    Private Shared frm_prb As frmProblemList
    Dim _SelectedStatus As Status

    'Memory Leak
    Dim dtRx As DataTable
    Dim strProblem As String = String.Empty
    Dim strICD9 As String = String.Empty
    Dim onSiteDate As String = String.Empty
    Dim DischargeDate As String = String.Empty
    Dim strConceptID As String = String.Empty
    Dim strDescriptionID As String = String.Empty
    Dim strSnoMedID As String = String.Empty
    Dim strDescription As String = String.Empty
    Dim strLateralityCode As String = String.Empty
    Dim strLateralityDesc As String = String.Empty
    Dim strConcernstatus As String = String.Empty
    Dim strCDAProblemType As String = String.Empty
    'Added Rahul 20100825
    Dim sResolveDate As String = String.Empty
    Dim bEncounterDiagnosis As Boolean = False
    Private _ExamID As Long
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    Private strPatientLanguage As String
    Private nICDRevision As Integer = 9 ''added for ICD10 implementation
    Dim strLocation As String = String.Empty
    Dim nLocationId As Int64 = 0
    Dim PExamID As String = "0"
    Dim strProvider As String = String.Empty
    Dim strPrecription As String = String.Empty
    Dim strHiddenPrecription As String = String.Empty
    Dim strExamID As String = String.Empty
    Dim nProivderID As Int64 = 0
    Dim nStatus As String
    Dim nImmediacy As String
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents c1ProblemList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlLeftMenu As System.Windows.Forms.Panel
    Friend WithEvents btn_Remove As System.Windows.Forms.Button
    Friend WithEvents btn_Both As System.Windows.Forms.Button
    Friend WithEvents btn_Inactive As System.Windows.Forms.Button
    Friend WithEvents btn_ActiveProblem As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents tlb_OpenExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblCCD As System.Windows.Forms.ToolStripButton
    Friend WithEvents cntAssociateExams As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuNewExam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPastExam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlb_RxMed As System.Windows.Forms.ToolStripButton
    Dim dgCommnents As New DataGridView
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btn_Inactive2 As System.Windows.Forms.Button
    Friend WithEvents pnltrvDefination As System.Windows.Forms.Panel
    Friend WithEvents trvDefination As System.Windows.Forms.TreeView
    Friend WithEvents imgTreeVIew As System.Windows.Forms.ImageList
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents tlb_Reconcile As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsRecommendation As System.Windows.Forms.ToolStripButton
    Dim oDB As gloStream.gloDataBase.gloDataBase
    Friend WithEvents mnuPatientEducation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProviderReference As System.Windows.Forms.ToolStripMenuItem

    Dim clsinfobutton_Problemlist As New gloEMRGeneralLibrary.clsInfobutton
    Friend WithEvents mnuSendtoExam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlSelectExam As System.Windows.Forms.Panel
    Dim _ISSmonedCodeMandatory As Boolean = False
    Dim PrevGridWidth As Long = 0
    Dim _strbtnClicked As String = String.Empty
    Dim dtRecProblist As DataTable = Nothing

    Dim nProviderAssociationID As Int64 = 0
    Dim sProviderTaxID As String = ""
    Dim blnmedRecupdated As Boolean = False

#Region "Send To Selected Exam"
    Private Col_eExamID As Integer = 0
    Private Col_eVistitID As Integer = 1
    Private Col_eDos As Integer = 2
    Private Col_eExamName As Integer = 3
    Private Col_eTemplateName As Integer = 4
    Private Col_eFinished As Integer = 5
    Private Col_eProviderName As Integer = 6
    Private Col_eReviewedBy As Integer = 7
    Private Col_eCheck As Integer = 8
    Friend WithEvents mnuMedlineInfobutton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlb_ProbReconciliation As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlb_NKProblems As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblRecHistory As System.Windows.Forms.ToolStripButton
    Private Col_eCount As Integer = 9
#End Region

    Public Property ExamID() As Long
        Get
            Return _ExamID
        End Get
        Set(ByVal value As Long)
            _ExamID = value
        End Set
    End Property
#Region " Windows Form Designer generated code "
    Public Sub New(ByVal ProblemID As Long, ByVal VisitID As Long, ByVal m_PatientID As Long, Optional ByVal blnRecordLock As Boolean = False)
        MyBase.New()
        '' ProblemID is Zero when Problem List is Open from Patient Exam
        _ProblemID = ProblemID
        '' VisitID is Zero when Problem List is Open from Patient Problem List
        _VisitID = VisitID
        _PatientID = m_PatientID
        '' 
        _blnRecordLock = blnRecordLock
        'This call is required by the Windows Form Designer.
        _chkInstPatientId = m_PatientID
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    ' Chetan Added Sept 02 2010
    Public Shared Function GetInstance(ByVal ProblemID As Long, ByVal VisitID As Long, ByVal m_PatientID As Long, Optional ByVal blnRecordLock As Boolean = False) As frmProblemList

        Dim IsOpen As Boolean = False
        Try
            IsOpen = False

            For Each f As Form In Application.OpenForms
                If f.Name = "frmProblemList" Then
                    If CType(f, frmProblemList)._PatientID = m_PatientID Then
                        IsOpen = True
                        frm_prb = f
                        Exit For
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm_prb = New frmProblemList(ProblemID, VisitID, m_PatientID, blnRecordLock)
            End If
        Finally
        End Try
        Return frm_prb
    End Function
    ' Chetan Added Sept 02 2010
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try


            If Not (objclsgeneral Is Nothing) Then
                objclsgeneral.Dispose()
                objclsgeneral = Nothing
            End If
            If Not (oDB Is Nothing) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            'Memory Leak
            If Not IsNothing(gloUC_PatientStrip1) Then
                'Me.Controls.Remove(gloUC_PatientStrip1)
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If
            Dim dtpContextMenuStrip As ContextMenuStrip() = {cntAssociateExams}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenuStrip)

            Catch ex As Exception

            End Try

            Dim dtpControls As DateTimePicker() = {datetimeFrom, datetimeTo}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)

            Catch ex As Exception

            End Try
            Try
                gloGlobal.cEventHelper.DisposeAllControls(dtpContextMenuStrip)
                gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
            Catch ex As Exception

            End Try


        End If

        MyBase.Dispose(disposing)

        frm_prb = Nothing
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents datetimeFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents datetimeTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProblemList))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlc1ProblemList = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.c1ProblemList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.pnlcustomTask = New System.Windows.Forms.Panel()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.pnltrvDefination = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.trvDefination = New System.Windows.Forms.TreeView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlb_New = New System.Windows.Forms.ToolStripButton()
        Me.tlb_Edit = New System.Windows.Forms.ToolStripButton()
        Me.tlb_Delete = New System.Windows.Forms.ToolStripButton()
        Me.tlb_OpenExam = New System.Windows.Forms.ToolStripButton()
        Me.tlb_Annotate = New System.Windows.Forms.ToolStripButton()
        Me.tlb_RxMed = New System.Windows.Forms.ToolStripButton()
        Me.tlb_ProbReconciliation = New System.Windows.Forms.ToolStripButton()
        Me.tlb_NKProblems = New System.Windows.Forms.ToolStripButton()
        Me.tblRecHistory = New System.Windows.Forms.ToolStripButton()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.pnlLeftMenu = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btn_Remove = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btn_Both = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btn_Inactive = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.btn_Inactive2 = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btn_ActiveProblem = New System.Windows.Forms.Button()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.datetimeTo = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.datetimeFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblAge = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblPatientName = New System.Windows.Forms.Label()
        Me.pnlpatientControl = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblPatientCode = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.pnlAlert = New System.Windows.Forms.Panel()
        Me.lblAlert = New System.Windows.Forms.Label()
        Me.picAlert = New System.Windows.Forms.PictureBox()
        Me.tblPraoblemList = New gloToolStrip.gloToolStrip()
        Me.tlsRecommendation = New System.Windows.Forms.ToolStripButton()
        Me.tlsHealthPlan = New System.Windows.Forms.ToolStripButton()
        Me.tblCCD = New System.Windows.Forms.ToolStripButton()
        Me.tlb_Reconcile = New System.Windows.Forms.ToolStripButton()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.cntAssociateExams = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuNewExam = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPastExam = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMedlineInfobutton = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientEducation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProviderReference = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSendtoExam = New System.Windows.Forms.ToolStripMenuItem()
        Me.imgTreeVIew = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlSelectExam = New System.Windows.Forms.Panel()
        Me.pnlMain.SuspendLayout()
        Me.pnlc1ProblemList.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.c1ProblemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnltrvDefination.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlLeftMenu.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlpatientControl.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.pnlAlert.SuspendLayout()
        CType(Me.picAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblPraoblemList.SuspendLayout()
        Me.cntAssociateExams.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.pnlc1ProblemList)
        Me.pnlMain.Controls.Add(Me.pnlSearch)
        Me.pnlMain.Controls.Add(Me.pnlTop)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1019, 662)
        Me.pnlMain.TabIndex = 0
        '
        'pnlc1ProblemList
        '
        Me.pnlc1ProblemList.Controls.Add(Me.Panel2)
        Me.pnlc1ProblemList.Controls.Add(Me.Panel10)
        Me.pnlc1ProblemList.Controls.Add(Me.pnltrvDefination)
        Me.pnlc1ProblemList.Controls.Add(Me.Panel1)
        Me.pnlc1ProblemList.Controls.Add(Me.pnlLeftMenu)
        Me.pnlc1ProblemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlc1ProblemList.Location = New System.Drawing.Point(0, 76)
        Me.pnlc1ProblemList.Name = "pnlc1ProblemList"
        Me.pnlc1ProblemList.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlc1ProblemList.Size = New System.Drawing.Size(1019, 586)
        Me.pnlc1ProblemList.TabIndex = 8
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.c1ProblemList)
        Me.Panel2.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel2.Controls.Add(Me.pnlcustomTask)
        Me.Panel2.Controls.Add(Me.lbl_TopBrd)
        Me.Panel2.Controls.Add(Me.lbl_RightBrd)
        Me.Panel2.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(187, 57)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(829, 301)
        Me.Panel2.TabIndex = 21
        '
        'c1ProblemList
        '
        Me.c1ProblemList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1ProblemList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1ProblemList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1ProblemList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1ProblemList.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1ProblemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1ProblemList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1ProblemList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1ProblemList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1ProblemList.Location = New System.Drawing.Point(1, 4)
        Me.c1ProblemList.Name = "c1ProblemList"
        Me.c1ProblemList.Rows.Count = 1
        Me.c1ProblemList.Rows.DefaultSize = 19
        Me.c1ProblemList.Rows.Fixed = 0
        Me.c1ProblemList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1ProblemList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1ProblemList.ShowCellLabels = True
        Me.c1ProblemList.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1ProblemList.Size = New System.Drawing.Size(827, 293)
        Me.c1ProblemList.StyleInfo = resources.GetString("c1ProblemList.StyleInfo")
        Me.c1ProblemList.TabIndex = 9
        Me.c1ProblemList.Tree.NodeImageCollapsed = CType(resources.GetObject("c1ProblemList.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.c1ProblemList.Tree.NodeImageExpanded = CType(resources.GetObject("c1ProblemList.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 297)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(827, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'pnlcustomTask
        '
        Me.pnlcustomTask.Location = New System.Drawing.Point(110, 52)
        Me.pnlcustomTask.Name = "pnlcustomTask"
        Me.pnlcustomTask.Size = New System.Drawing.Size(320, 209)
        Me.pnlcustomTask.TabIndex = 7
        Me.pnlcustomTask.Visible = False
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(1, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(827, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(828, 3)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 295)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 3)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 295)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Transparent
        Me.Panel10.Controls.Add(Me.Panel9)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel10.Location = New System.Drawing.Point(187, 358)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel10.Size = New System.Drawing.Size(829, 26)
        Me.Panel10.TabIndex = 33
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Transparent
        Me.Panel9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.btnUp)
        Me.Panel9.Controls.Add(Me.btnDown)
        Me.Panel9.Controls.Add(Me.Label25)
        Me.Panel9.Controls.Add(Me.Label31)
        Me.Panel9.Controls.Add(Me.Label30)
        Me.Panel9.Controls.Add(Me.Label29)
        Me.Panel9.Controls.Add(Me.Label28)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(829, 23)
        Me.Panel9.TabIndex = 32
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Location = New System.Drawing.Point(780, 1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(24, 21)
        Me.btnUp.TabIndex = 37
        Me.btnUp.UseVisualStyleBackColor = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(804, 1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(24, 21)
        Me.btnDown.TabIndex = 36
        Me.btnDown.UseVisualStyleBackColor = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(5, 5)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(83, 14)
        Me.Label25.TabIndex = 35
        Me.Label25.Text = "  Definition :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(1, 22)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(827, 1)
        Me.Label31.TabIndex = 34
        Me.Label31.Text = "label2"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label30.Location = New System.Drawing.Point(1, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(827, 1)
        Me.Label30.TabIndex = 33
        Me.Label30.Text = "label2"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 23)
        Me.Label29.TabIndex = 32
        Me.Label29.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(828, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 23)
        Me.Label28.TabIndex = 31
        Me.Label28.Text = "label4"
        '
        'pnltrvDefination
        '
        Me.pnltrvDefination.Controls.Add(Me.Label24)
        Me.pnltrvDefination.Controls.Add(Me.Label23)
        Me.pnltrvDefination.Controls.Add(Me.Label22)
        Me.pnltrvDefination.Controls.Add(Me.Label21)
        Me.pnltrvDefination.Controls.Add(Me.trvDefination)
        Me.pnltrvDefination.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnltrvDefination.Location = New System.Drawing.Point(187, 384)
        Me.pnltrvDefination.Name = "pnltrvDefination"
        Me.pnltrvDefination.Size = New System.Drawing.Size(829, 199)
        Me.pnltrvDefination.TabIndex = 23
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(828, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 197)
        Me.Label24.TabIndex = 12
        Me.Label24.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 197)
        Me.Label23.TabIndex = 11
        Me.Label23.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(829, 1)
        Me.Label22.TabIndex = 10
        Me.Label22.Text = "label2"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(0, 198)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(829, 1)
        Me.Label21.TabIndex = 9
        Me.Label21.Text = "label2"
        '
        'trvDefination
        '
        Me.trvDefination.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvDefination.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvDefination.Indent = 20
        Me.trvDefination.ItemHeight = 20
        Me.trvDefination.Location = New System.Drawing.Point(0, 0)
        Me.trvDefination.Name = "trvDefination"
        Me.trvDefination.Size = New System.Drawing.Size(829, 199)
        Me.trvDefination.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ToolStrip1)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(187, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(829, 54)
        Me.Panel1.TabIndex = 20
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_New, Me.tlb_Edit, Me.tlb_Delete, Me.tlb_OpenExam, Me.tlb_Annotate, Me.tlb_RxMed, Me.tlb_ProbReconciliation, Me.tlb_NKProblems, Me.tblRecHistory})
        Me.ToolStrip1.Location = New System.Drawing.Point(1, 1)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(828, 53)
        Me.ToolStrip1.TabIndex = 19
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tlb_New
        '
        Me.tlb_New.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_New.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_New.Image = CType(resources.GetObject("tlb_New.Image"), System.Drawing.Image)
        Me.tlb_New.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_New.Name = "tlb_New"
        Me.tlb_New.Size = New System.Drawing.Size(37, 50)
        Me.tlb_New.Tag = "New"
        Me.tlb_New.Text = "&New"
        Me.tlb_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_New.ToolTipText = "New"
        '
        'tlb_Edit
        '
        Me.tlb_Edit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Edit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_Edit.Image = CType(resources.GetObject("tlb_Edit.Image"), System.Drawing.Image)
        Me.tlb_Edit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Edit.Name = "tlb_Edit"
        Me.tlb_Edit.Size = New System.Drawing.Size(36, 50)
        Me.tlb_Edit.Tag = "Edit"
        Me.tlb_Edit.Text = "&Edit"
        Me.tlb_Edit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Edit.ToolTipText = "Edit"
        '
        'tlb_Delete
        '
        Me.tlb_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Delete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_Delete.Image = CType(resources.GetObject("tlb_Delete.Image"), System.Drawing.Image)
        Me.tlb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Delete.Name = "tlb_Delete"
        Me.tlb_Delete.Size = New System.Drawing.Size(50, 50)
        Me.tlb_Delete.Tag = "Delete"
        Me.tlb_Delete.Text = "&Delete"
        Me.tlb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Delete.ToolTipText = "Delete"
        '
        'tlb_OpenExam
        '
        Me.tlb_OpenExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_OpenExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_OpenExam.Image = CType(resources.GetObject("tlb_OpenExam.Image"), System.Drawing.Image)
        Me.tlb_OpenExam.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_OpenExam.Name = "tlb_OpenExam"
        Me.tlb_OpenExam.Size = New System.Drawing.Size(79, 50)
        Me.tlb_OpenExam.Tag = "OpenExam"
        Me.tlb_OpenExam.Text = "&Open Exam"
        Me.tlb_OpenExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_OpenExam.ToolTipText = "Open Exam"
        '
        'tlb_Annotate
        '
        Me.tlb_Annotate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Annotate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_Annotate.Image = CType(resources.GetObject("tlb_Annotate.Image"), System.Drawing.Image)
        Me.tlb_Annotate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Annotate.Name = "tlb_Annotate"
        Me.tlb_Annotate.Size = New System.Drawing.Size(70, 50)
        Me.tlb_Annotate.Tag = "Annotate"
        Me.tlb_Annotate.Text = "&Annotate"
        Me.tlb_Annotate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Annotate.ToolTipText = "Close"
        Me.tlb_Annotate.Visible = False
        '
        'tlb_RxMed
        '
        Me.tlb_RxMed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_RxMed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_RxMed.Image = CType(resources.GetObject("tlb_RxMed.Image"), System.Drawing.Image)
        Me.tlb_RxMed.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_RxMed.Name = "tlb_RxMed"
        Me.tlb_RxMed.Size = New System.Drawing.Size(64, 50)
        Me.tlb_RxMed.Tag = "Annotate"
        Me.tlb_RxMed.Text = "&Rx-Meds"
        Me.tlb_RxMed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_RxMed.ToolTipText = "Prescription and Medication"
        '
        'tlb_ProbReconciliation
        '
        Me.tlb_ProbReconciliation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_ProbReconciliation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_ProbReconciliation.Image = CType(resources.GetObject("tlb_ProbReconciliation.Image"), System.Drawing.Image)
        Me.tlb_ProbReconciliation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_ProbReconciliation.Name = "tlb_ProbReconciliation"
        Me.tlb_ProbReconciliation.Size = New System.Drawing.Size(133, 50)
        Me.tlb_ProbReconciliation.Tag = "ProbReconciliation"
        Me.tlb_ProbReconciliation.Text = "&Prob. Reconciliation"
        Me.tlb_ProbReconciliation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_ProbReconciliation.ToolTipText = "Problem List Reconciliation"
        '
        'tlb_NKProblems
        '
        Me.tlb_NKProblems.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_NKProblems.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlb_NKProblems.Image = CType(resources.GetObject("tlb_NKProblems.Image"), System.Drawing.Image)
        Me.tlb_NKProblems.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_NKProblems.Name = "tlb_NKProblems"
        Me.tlb_NKProblems.Size = New System.Drawing.Size(95, 50)
        Me.tlb_NKProblems.Tag = "NKProblems"
        Me.tlb_NKProblems.Text = "&N.K. Problems"
        Me.tlb_NKProblems.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_NKProblems.ToolTipText = "No Known Problems"
        '
        'tblRecHistory
        '
        Me.tblRecHistory.BackColor = System.Drawing.Color.Transparent
        Me.tblRecHistory.BackgroundImage = CType(resources.GetObject("tblRecHistory.BackgroundImage"), System.Drawing.Image)
        Me.tblRecHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblRecHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblRecHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblRecHistory.Image = CType(resources.GetObject("tblRecHistory.Image"), System.Drawing.Image)
        Me.tblRecHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblRecHistory.Name = "tblRecHistory"
        Me.tblRecHistory.Size = New System.Drawing.Size(177, 50)
        Me.tblRecHistory.Tag = "Rechist"
        Me.tblRecHistory.Text = "&View Reconciliation History"
        Me.tblRecHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblRecHistory.ToolTipText = "View Reconciliation History"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 53)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "label1"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(829, 1)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "label2"
        '
        'pnlLeftMenu
        '
        Me.pnlLeftMenu.Controls.Add(Me.Label20)
        Me.pnlLeftMenu.Controls.Add(Me.Label19)
        Me.pnlLeftMenu.Controls.Add(Me.Label18)
        Me.pnlLeftMenu.Controls.Add(Me.Label17)
        Me.pnlLeftMenu.Controls.Add(Me.Panel6)
        Me.pnlLeftMenu.Controls.Add(Me.Panel5)
        Me.pnlLeftMenu.Controls.Add(Me.Panel4)
        Me.pnlLeftMenu.Controls.Add(Me.Panel7)
        Me.pnlLeftMenu.Controls.Add(Me.Panel3)
        Me.pnlLeftMenu.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeftMenu.Location = New System.Drawing.Point(3, 3)
        Me.pnlLeftMenu.Name = "pnlLeftMenu"
        Me.pnlLeftMenu.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.pnlLeftMenu.Size = New System.Drawing.Size(184, 580)
        Me.pnlLeftMenu.TabIndex = 22
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(1, 579)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(179, 1)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1, 135)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(179, 1)
        Me.Label19.TabIndex = 10
        Me.Label19.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(180, 135)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 445)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 135)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 445)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label4"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btn_Remove)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 108)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(181, 27)
        Me.Panel6.TabIndex = 5
        Me.Panel6.Visible = False
        '
        'btn_Remove
        '
        Me.btn_Remove.BackgroundImage = CType(resources.GetObject("btn_Remove.BackgroundImage"), System.Drawing.Image)
        Me.btn_Remove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Remove.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Remove.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btn_Remove.Location = New System.Drawing.Point(0, 0)
        Me.btn_Remove.Name = "btn_Remove"
        Me.btn_Remove.Size = New System.Drawing.Size(181, 24)
        Me.btn_Remove.TabIndex = 3
        Me.btn_Remove.Text = "Removed"
        Me.btn_Remove.UseVisualStyleBackColor = True
        Me.btn_Remove.Visible = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.btn_Both)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 81)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(181, 27)
        Me.Panel5.TabIndex = 5
        '
        'btn_Both
        '
        Me.btn_Both.BackgroundImage = CType(resources.GetObject("btn_Both.BackgroundImage"), System.Drawing.Image)
        Me.btn_Both.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Both.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Both.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Both.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btn_Both.Location = New System.Drawing.Point(0, 0)
        Me.btn_Both.Name = "btn_Both"
        Me.btn_Both.Size = New System.Drawing.Size(181, 24)
        Me.btn_Both.TabIndex = 2
        Me.btn_Both.Text = "All"
        Me.btn_Both.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btn_Inactive)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 54)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(181, 27)
        Me.Panel4.TabIndex = 5
        '
        'btn_Inactive
        '
        Me.btn_Inactive.BackgroundImage = CType(resources.GetObject("btn_Inactive.BackgroundImage"), System.Drawing.Image)
        Me.btn_Inactive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Inactive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Inactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Inactive.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btn_Inactive.Location = New System.Drawing.Point(0, 0)
        Me.btn_Inactive.Name = "btn_Inactive"
        Me.btn_Inactive.Size = New System.Drawing.Size(181, 24)
        Me.btn_Inactive.TabIndex = 1
        Me.btn_Inactive.Tag = "Resolved"
        Me.btn_Inactive.Text = "Resolved"
        Me.btn_Inactive.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.btn_Inactive2)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 27)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel7.Size = New System.Drawing.Size(181, 27)
        Me.Panel7.TabIndex = 12
        '
        'btn_Inactive2
        '
        Me.btn_Inactive2.BackgroundImage = CType(resources.GetObject("btn_Inactive2.BackgroundImage"), System.Drawing.Image)
        Me.btn_Inactive2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Inactive2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Inactive2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Inactive2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btn_Inactive2.Location = New System.Drawing.Point(0, 0)
        Me.btn_Inactive2.Name = "btn_Inactive2"
        Me.btn_Inactive2.Size = New System.Drawing.Size(181, 24)
        Me.btn_Inactive2.TabIndex = 2
        Me.btn_Inactive2.Text = "Inactive"
        Me.btn_Inactive2.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btn_ActiveProblem)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(181, 27)
        Me.Panel3.TabIndex = 4
        '
        'btn_ActiveProblem
        '
        Me.btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btn_ActiveProblem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ActiveProblem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_ActiveProblem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ActiveProblem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btn_ActiveProblem.Location = New System.Drawing.Point(0, 0)
        Me.btn_ActiveProblem.Name = "btn_ActiveProblem"
        Me.btn_ActiveProblem.Size = New System.Drawing.Size(181, 24)
        Me.btn_ActiveProblem.TabIndex = 0
        Me.btn_ActiveProblem.Tag = "Active"
        Me.btn_ActiveProblem.Text = "Active"
        Me.btn_ActiveProblem.UseVisualStyleBackColor = True
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.Label7)
        Me.pnlSearch.Controls.Add(Me.Label8)
        Me.pnlSearch.Controls.Add(Me.Label9)
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.lblSearch)
        Me.pnlSearch.Controls.Add(Me.cmbStatus)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.datetimeTo)
        Me.pnlSearch.Controls.Add(Me.Label1)
        Me.pnlSearch.Controls.Add(Me.datetimeFrom)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlSearch.Location = New System.Drawing.Point(0, 37)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlSearch.Size = New System.Drawing.Size(1019, 39)
        Me.pnlSearch.TabIndex = 3
        Me.pnlSearch.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1011, 1)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 38)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(1015, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 38)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1013, 1)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "label1"
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(655, 9)
        Me.txtSearch.MaxLength = 500
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(179, 22)
        Me.txtSearch.TabIndex = 20
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(580, 13)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(73, 14)
        Me.lblSearch.TabIndex = 19
        Me.lblSearch.Text = "Diagonsis :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbStatus.Location = New System.Drawing.Point(418, 9)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(136, 22)
        Me.cmbStatus.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(360, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 14)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Status :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(210, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "To"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'datetimeTo
        '
        Me.datetimeTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.datetimeTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.datetimeTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.datetimeTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.datetimeTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.datetimeTo.CustomFormat = ""
        Me.datetimeTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datetimeTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.datetimeTo.Location = New System.Drawing.Point(236, 9)
        Me.datetimeTo.Name = "datetimeTo"
        Me.datetimeTo.Size = New System.Drawing.Size(98, 22)
        Me.datetimeTo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(41, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'datetimeFrom
        '
        Me.datetimeFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.datetimeFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.datetimeFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.datetimeFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.datetimeFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.datetimeFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datetimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.datetimeFrom.Location = New System.Drawing.Point(80, 9)
        Me.datetimeFrom.Name = "datetimeFrom"
        Me.datetimeFrom.Size = New System.Drawing.Size(98, 22)
        Me.datetimeFrom.TabIndex = 0
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.Label11)
        Me.pnlTop.Controls.Add(Me.Label12)
        Me.pnlTop.Controls.Add(Me.Label13)
        Me.pnlTop.Controls.Add(Me.Label14)
        Me.pnlTop.Controls.Add(Me.lblAge)
        Me.pnlTop.Controls.Add(Me.Label10)
        Me.pnlTop.Controls.Add(Me.lblPatientName)
        Me.pnlTop.Controls.Add(Me.pnlpatientControl)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTop.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(1019, 37)
        Me.pnlTop.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 33)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1011, 1)
        Me.Label11.TabIndex = 35
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 30)
        Me.Label12.TabIndex = 34
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1015, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 30)
        Me.Label13.TabIndex = 33
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1013, 1)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "label1"
        '
        'lblAge
        '
        Me.lblAge.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAge.AutoSize = True
        Me.lblAge.BackColor = System.Drawing.Color.Transparent
        Me.lblAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAge.Location = New System.Drawing.Point(873, 12)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(31, 14)
        Me.lblAge.TabIndex = 29
        Me.lblAge.Text = "Age"
        Me.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(218, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 14)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "Patient Name :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatientName.Location = New System.Drawing.Point(317, 11)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(40, 14)
        Me.lblPatientName.TabIndex = 27
        Me.lblPatientName.Text = "Name"
        Me.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlpatientControl
        '
        Me.pnlpatientControl.BackColor = System.Drawing.Color.Transparent
        Me.pnlpatientControl.Controls.Add(Me.Label6)
        Me.pnlpatientControl.Controls.Add(Me.lblPatientCode)
        Me.pnlpatientControl.Controls.Add(Me.Label4)
        Me.pnlpatientControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlpatientControl.Location = New System.Drawing.Point(3, 3)
        Me.pnlpatientControl.Name = "pnlpatientControl"
        Me.pnlpatientControl.Size = New System.Drawing.Size(1013, 31)
        Me.pnlpatientControl.TabIndex = 31
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(825, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 14)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Age :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientCode
        '
        Me.lblPatientCode.AutoSize = True
        Me.lblPatientCode.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatientCode.Location = New System.Drawing.Point(104, 8)
        Me.lblPatientCode.Name = "lblPatientCode"
        Me.lblPatientCode.Size = New System.Drawing.Size(38, 14)
        Me.lblPatientCode.TabIndex = 25
        Me.lblPatientCode.Text = "Code"
        Me.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(9, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 14)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Patient Code :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.AutoSize = True
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.Panel8)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1019, 54)
        Me.pnlToolStrip.TabIndex = 17
        '
        'Panel8
        '
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.pnlAlert)
        Me.Panel8.Controls.Add(Me.tblPraoblemList)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1019, 54)
        Me.Panel8.TabIndex = 13
        '
        'pnlAlert
        '
        Me.pnlAlert.BackColor = System.Drawing.Color.Transparent
        Me.pnlAlert.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnlAlert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlAlert.Controls.Add(Me.lblAlert)
        Me.pnlAlert.Controls.Add(Me.picAlert)
        Me.pnlAlert.Location = New System.Drawing.Point(465, -1)
        Me.pnlAlert.Name = "pnlAlert"
        Me.pnlAlert.Size = New System.Drawing.Size(542, 53)
        Me.pnlAlert.TabIndex = 1
        Me.pnlAlert.Visible = False
        '
        'lblAlert
        '
        Me.lblAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAlert.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAlert.Location = New System.Drawing.Point(30, 0)
        Me.lblAlert.Name = "lblAlert"
        Me.lblAlert.Size = New System.Drawing.Size(512, 53)
        Me.lblAlert.TabIndex = 1
        Me.lblAlert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picAlert
        '
        Me.picAlert.BackColor = System.Drawing.Color.Transparent
        Me.picAlert.Dock = System.Windows.Forms.DockStyle.Left
        Me.picAlert.Image = Global.gloEMR.My.Resources.Resources.NewRed_Alert
        Me.picAlert.Location = New System.Drawing.Point(0, 0)
        Me.picAlert.Name = "picAlert"
        Me.picAlert.Size = New System.Drawing.Size(30, 53)
        Me.picAlert.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picAlert.TabIndex = 0
        Me.picAlert.TabStop = False
        '
        'tblPraoblemList
        '
        Me.tblPraoblemList.AddSeparatorsBetweenEachButton = False
        Me.tblPraoblemList.BackColor = System.Drawing.Color.Transparent
        Me.tblPraoblemList.BackgroundImage = CType(resources.GetObject("tblPraoblemList.BackgroundImage"), System.Drawing.Image)
        Me.tblPraoblemList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblPraoblemList.ButtonsToHide = CType(resources.GetObject("tblPraoblemList.ButtonsToHide"), System.Collections.ArrayList)
        Me.tblPraoblemList.ConnectionString = Nothing
        Me.tblPraoblemList.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.tblPraoblemList.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblPraoblemList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblPraoblemList.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblPraoblemList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsRecommendation, Me.tlsHealthPlan, Me.tblCCD, Me.tlb_Reconcile, Me.tblSave, Me.tblClose})
        Me.tblPraoblemList.Location = New System.Drawing.Point(0, 0)
        Me.tblPraoblemList.ModuleName = Nothing
        Me.tblPraoblemList.Name = "tblPraoblemList"
        Me.tblPraoblemList.Size = New System.Drawing.Size(1019, 53)
        Me.tblPraoblemList.TabIndex = 0
        Me.tblPraoblemList.Text = "ToolStrip1"
        Me.tblPraoblemList.UserID = CType(0, Long)
        '
        'tlsRecommendation
        '
        Me.tlsRecommendation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsRecommendation.Image = CType(resources.GetObject("tlsRecommendation.Image"), System.Drawing.Image)
        Me.tlsRecommendation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsRecommendation.Name = "tlsRecommendation"
        Me.tlsRecommendation.Size = New System.Drawing.Size(118, 50)
        Me.tlsRecommendation.Tag = "Recommendation"
        Me.tlsRecommendation.Text = "&Recommendation"
        Me.tlsRecommendation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsHealthPlan
        '
        Me.tlsHealthPlan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsHealthPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsHealthPlan.Image = CType(resources.GetObject("tlsHealthPlan.Image"), System.Drawing.Image)
        Me.tlsHealthPlan.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsHealthPlan.Name = "tlsHealthPlan"
        Me.tlsHealthPlan.Size = New System.Drawing.Size(81, 50)
        Me.tlsHealthPlan.Tag = "HealthPlan"
        Me.tlsHealthPlan.Text = "&Health Plan"
        Me.tlsHealthPlan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsHealthPlan.ToolTipText = "Find Health Plan"
        '
        'tblCCD
        '
        Me.tblCCD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblCCD.Image = CType(resources.GetObject("tblCCD.Image"), System.Drawing.Image)
        Me.tblCCD.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tblCCD.Name = "tblCCD"
        Me.tblCCD.Size = New System.Drawing.Size(63, 50)
        Me.tblCCD.Tag = "Generate CCD"
        Me.tblCCD.Text = "&Gen CCD"
        Me.tblCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblCCD.ToolTipText = "Generate CCD"
        '
        'tlb_Reconcile
        '
        Me.tlb_Reconcile.Enabled = False
        Me.tlb_Reconcile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Reconcile.Image = CType(resources.GetObject("tlb_Reconcile.Image"), System.Drawing.Image)
        Me.tlb_Reconcile.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tlb_Reconcile.Name = "tlb_Reconcile"
        Me.tlb_Reconcile.Size = New System.Drawing.Size(68, 50)
        Me.tlb_Reconcile.Tag = "Reconcile"
        Me.tlb_Reconcile.Text = "&Reconcile"
        Me.tlb_Reconcile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Reconcile.ToolTipText = "Reconcile"
        '
        'tblSave
        '
        Me.tblSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblSave.Image = CType(resources.GetObject("tblSave.Image"), System.Drawing.Image)
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(66, 50)
        Me.tblSave.Tag = "Save"
        Me.tblSave.Text = "&Save&&Cls"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Save and Close"
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Tag = "Close"
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'cntAssociateExams
        '
        Me.cntAssociateExams.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewExam, Me.mnuPastExam, Me.mnuMedlineInfobutton, Me.mnuPatientEducation, Me.mnuProviderReference, Me.mnuSendtoExam})
        Me.cntAssociateExams.Name = "cntAssociateExams"
        Me.cntAssociateExams.Size = New System.Drawing.Size(220, 136)
        '
        'mnuNewExam
        '
        Me.mnuNewExam.Name = "mnuNewExam"
        Me.mnuNewExam.Size = New System.Drawing.Size(219, 22)
        Me.mnuNewExam.Text = "New Exam"
        '
        'mnuPastExam
        '
        Me.mnuPastExam.Name = "mnuPastExam"
        Me.mnuPastExam.Size = New System.Drawing.Size(219, 22)
        Me.mnuPastExam.Text = "Past Exam"
        '
        'mnuMedlineInfobutton
        '
        Me.mnuMedlineInfobutton.Image = Global.gloEMR.My.Resources.Resources.infobutton
        Me.mnuMedlineInfobutton.Name = "mnuMedlineInfobutton"
        Me.mnuMedlineInfobutton.Size = New System.Drawing.Size(219, 22)
        Me.mnuMedlineInfobutton.Text = "Infobutton"
        '
        'mnuPatientEducation
        '
        Me.mnuPatientEducation.Image = Global.gloEMR.My.Resources.Resources.Patient_reference_material_img
        Me.mnuPatientEducation.Name = "mnuPatientEducation"
        Me.mnuPatientEducation.Size = New System.Drawing.Size(219, 22)
        Me.mnuPatientEducation.Text = "Patient Reference Material"
        '
        'mnuProviderReference
        '
        Me.mnuProviderReference.Image = Global.gloEMR.My.Resources.Resources.Provider_reference_material_img
        Me.mnuProviderReference.Name = "mnuProviderReference"
        Me.mnuProviderReference.Size = New System.Drawing.Size(219, 22)
        Me.mnuProviderReference.Text = "Provider Reference Material"
        '
        'mnuSendtoExam
        '
        Me.mnuSendtoExam.Name = "mnuSendtoExam"
        Me.mnuSendtoExam.Size = New System.Drawing.Size(219, 22)
        Me.mnuSendtoExam.Text = "Send to Exam"
        '
        'imgTreeVIew
        '
        Me.imgTreeVIew.ImageStream = CType(resources.GetObject("imgTreeVIew.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeVIew.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeVIew.Images.SetKeyName(0, "Category_New.ico")
        Me.imgTreeVIew.Images.SetKeyName(1, "Drugs.ico")
        Me.imgTreeVIew.Images.SetKeyName(2, "Radiology_01.ico")
        Me.imgTreeVIew.Images.SetKeyName(3, "Small Arrow.ico")
        Me.imgTreeVIew.Images.SetKeyName(4, "Bullet06.ico")
        Me.imgTreeVIew.Images.SetKeyName(5, "Defination.ico")
        Me.imgTreeVIew.Images.SetKeyName(6, "ICD 09.ico")
        Me.imgTreeVIew.Images.SetKeyName(7, "btnbr.png")
        '
        'pnlSelectExam
        '
        Me.pnlSelectExam.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSelectExam.Location = New System.Drawing.Point(208, 192)
        Me.pnlSelectExam.Name = "pnlSelectExam"
        Me.pnlSelectExam.Size = New System.Drawing.Size(603, 332)
        Me.pnlSelectExam.TabIndex = 0
        Me.pnlSelectExam.Visible = False
        '
        'frmProblemList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1019, 716)
        Me.Controls.Add(Me.pnlSelectExam)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmProblemList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Problem List"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlc1ProblemList.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.c1ProblemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.pnltrvDefination.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlLeftMenu.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlpatientControl.ResumeLayout(False)
        Me.pnlpatientControl.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.pnlAlert.ResumeLayout(False)
        CType(Me.picAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tblPraoblemList.ResumeLayout(False)
        Me.tblPraoblemList.PerformLayout()
        Me.cntAssociateExams.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        If IsNothing(gloUC_PatientStrip1) Then
            gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip


            With gloUC_PatientStrip1
                .Dock = DockStyle.Top
                .Padding = New Padding(3, 0, 3, 0)
                '' Pass Paarameters Type of Form
                .ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.None)
                .BringToFront()
                '.DTPValue = Format(m_VisitDate, "MM/dd/yyyy")
                ''If form is open from Exam then set date

                pnlMain.BringToFront()

                .DTPEnabled = False
            End With
            Me.Controls.Add(gloUC_PatientStrip1)
            ''''
            pnlToolStrip.SendToBack()
            c1ProblemList.BringToFront()
            '' Hide Previous Patient Details
            pnlTop.Visible = False
        End If
        ' ''
    End Sub

#End Region

    Enum Status
        Resolved = 1
        'Pending = 2
        Active = 2
        Inactive = 3
        Chronic = 4
        All = 5
    End Enum
    Enum EnmImmediacy
        Acute = 1
        Chronic = 2
        unknown = 3
    End Enum

    Private Sub frmProblemList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            If Not IsNothing(CType(Me.ParentForm, MainMenu)) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try


        Me.TopMost = True
        If blnOpenFromExam Or blnRxMedFromExam Then
            Exit Sub
        End If

        Try
            Me.WindowState = FormWindowState.Maximized

            If Not IsNothing(CType(Me.ParentForm, MainMenu)) Then

                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
                CType(Me.ParentForm, MainMenu).ShowHideMainMenu(False, False)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmProblemList_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Me.TopMost = False
    End Sub

    Private Sub frmProblemList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070723
            If _blnRecordLock = False Then
                '' if the Locked by by the Current User & on Current Machine only
                UnLock_Transaction(TrnType.ProblemList, _PatientID, _VisitID, Now)
            End If
            '' <><><> Unlock the Record <><><>
            _isFormClose = True ''''set this variable to true whenever the form is closed, code added to resolve bug 10324
            ''SalesForceCase #11597 : GLO2011-0012477 : Problem list not updating on exam
            ''Raise the event when form is closed
            ''This event will only raised from PatientExam
            RaiseEvent ProblemListClosed()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If Not IsNothing(tblPraoblemList) Then
                tblPraoblemList.Dispose()
                tblPraoblemList = Nothing
            End If

        Catch ex As Exception
        Finally
            blnRxMedToProblem = False
        End Try
    End Sub

    Private Sub frmProblemList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try

            blnRxMedToProblem = False
            If Not IsNothing(dtRecProblist) Then ''
                dtRecProblist.Dispose()
                dtRecProblist = Nothing
            End If

        Catch ex As Exception
        Finally
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Close, "Patient Problem List Closed", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)



        End Try
    End Sub
    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try

            'Memory Leak
            'dtPatient = New DataTable

            dtPatient = GetPatientInfo(_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))
                    strPatientLanguage = Convert.ToString(dtPatient.Rows(0)("sLang"))
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

    Private Sub frmProblemList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.WaitCursor
        mnuSendtoExam.Visible = False
        btnUp.Visible = True
        btnUp.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
        btnUp.BackgroundImageLayout = ImageLayout.Center
        btnDown.Visible = False
        pnltrvDefination.Visible = False
        frmAddProblemList.htIcdSnomed.Clear()
        gloC1FlexStyle.Style(c1ProblemList)
        InitializeToolStrip()

        Try

            Call Get_PatientDetails()
            CheckSnomedSetting_Infobutton()
            'Sanjog -Added on 11012011 to dnt allow to open exam from Rx-Med from
            If blnOpenFromExam = True Or blnRxMedFromExam = True Then 'if open from exam then making openexam false 
                'Sanjog -Added on 11012011 to dnt allow to open exam from Rx-Med from
                tlb_OpenExam.Visible = False
            End If

            If blnRxMedFromExam = True Then 'code added to resolve bug 10324, hide the RxMeds button if the problem list form is opened from Rx-Meds form to avoid cyclic redundancy as done from Exams from
                tlb_RxMed.Visible = False
            End If

            objclsgeneral = New gloSnoMed.ClsGeneral
            objclsgeneral.IsConnect(gstrSMDBConnstr, GetConnectionString())

            'Patient Information
            lblPatientCode.Text = strPatientCode
            lblPatientName.Text = strPatientFirstName & "  " & strPatientLastName
            lblAge.Text = strPatientAge

            'Sanjog-Added on 12-jan-2011 to show problem list 
            If _VisitID = 0 Then
                _VisitID = GenerateVisitID(Date.Now, _PatientID)
            End If


            If _ProblemID > 0 Or _VisitID > 0 Then
                Dim objProblemList As New clsPatientProblemList

                dtProblems = objProblemList.Get_PatientProblemList(_PatientID)
                objProblemList.Dispose()
                objProblemList = Nothing

                If IsNothing(dtProblems) = False Then
                    Call SetGridStyle(dtProblems)
                    btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                    btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center
                    dtProblems.Dispose()
                    dtProblems = Nothing
                End If
            Else
                '' For New Problem List
                _VisitID = GenerateVisitID(Date.Now, _PatientID)
                Call Fill_Status()
            End If

            Call Set_PatientDetailStrip()

            DisplayHealthPlanAlerts()
            If Not gbShowviewRecommendation Then
                tblPraoblemList.Items.Remove(tlsRecommendation)
            End If
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Problem list opened", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If _blnRecordLock = True Then
                tblSave.Enabled = False
            End If
        End Try

        btn_ActiveProblem_Click(sender, e)

        If blnRxMedToProblem = True Then
            If Not IsNothing(ArrRx_Problem) Then

                For i As Integer = 0 To c1ProblemList.Rows.Count - 1
                    Try
                        Dim ToLine As Integer = 0
                        ToLine = c1ProblemList.Rows(i)(COL_COMPLAINTS).ToString().IndexOf(vbNewLine)
                        If ToLine >= 1 Then
                            If ArrRx_Problem.Values.Contains(c1ProblemList.Rows(i)(COL_COMPLAINTS).ToString().Substring(0, ToLine)) = True Then
                                c1ProblemList.SetCellCheck(i, COL_SELECT, CheckEnum.Checked)
                            End If
                        Else
                            If ArrRx_Problem.Values.Contains(c1ProblemList.Rows(i)(COL_COMPLAINTS).ToString()) = True Then
                                c1ProblemList.SetCellCheck(i, COL_SELECT, CheckEnum.Checked)
                            End If
                        End If

                    Catch ex As Exception
                        Cursor.Current = Cursors.Default
                    End Try
                Next
            End If
        End If

        'Aniket: Hide the Health Plan Button if the setting is false
        If gblnShowHealthPlan = False Then
            tblPraoblemList.Items.Remove(tlsHealthPlan)
        End If

        ''Reconcilation for problem list
        If Not IsNothing(dtRecProblist) Then
            dtRecProblist.Dispose()
            dtRecProblist = Nothing
        End If
        dtRecProblist = New DataTable()


        Dim dPatientID As New DataColumn("PatientID", GetType(Long))
        Dim dVisitID As New DataColumn("VisitID", GetType(Long))
        Dim dSummaryCheckBox As New DataColumn("SummaryCheckBox", GetType(Boolean))
        Dim dMedicationCheckBox As New DataColumn("MedicationCheckBox", GetType(Boolean))
        Dim dMedDate As New DataColumn("MedDate", GetType(DateTime))
        Dim dNotes As New DataColumn("Notes", GetType(String))
        Dim dRowState As New DataColumn("RowState", GetType(String))
        Dim dReconcillationType As New DataColumn("ReconcillationType", GetType(Int16))

        dtRecProblist.Columns.Add(dPatientID)
        dtRecProblist.Columns.Add(dVisitID)
        dtRecProblist.Columns.Add(dSummaryCheckBox)
        dtRecProblist.Columns.Add(dMedicationCheckBox)
        dtRecProblist.Columns.Add(dMedDate)
        dtRecProblist.Columns.Add(dNotes)
        dtRecProblist.Columns.Add(dRowState)
        dtRecProblist.Columns.Add(dReconcillationType)

        dtRecProblist = (GetProblemListMedicationReconcillationDetails(_VisitID, _PatientID, nReconcillationType))
        If dtRecProblist.Rows.Count > 0 Then
            If (dtRecProblist.Rows(0)("MedicationCheckBox") = True) Then

            End If
        End If


        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Cursor.Current = Cursors.Default
    End Sub
    Private Function GetProblemListMedicationReconcillationDetails(nVisitID As Long, nPatientID As Long, nReconcillationType As Int16) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As New DataTable()

        Try
            oDB.Connect(False)
            oParameters.Add("@VisitID", nVisitID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@PatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@ReconcillationType", nReconcillationType, ParameterDirection.Input, SqlDbType.SmallInt) ''Medication Reconcillation for problem list( 2015 certification code)
            oDB.Retrive("GetMedicationReconcillation", oParameters, dt)
            Return dt
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            dbEx = Nothing
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try


    End Function


    Public Sub ShowMessageForPendingReconciliation()
        If _blnRecordLock = False Then

            tlb_Reconcile.Enabled = False
            Dim _isReadyLists As Boolean = False
            Dim ogloCCDReconcile As New gloCCDLibrary.gloCCDReconcilation
            _isReadyLists = ogloCCDReconcile.IsReadyListsPresent(_PatientID, "Problem")
            If _isReadyLists = True Then
                tlb_Reconcile.Enabled = True
                MessageBox.Show("Patient has Pending Clinical Reconciliations. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
            Else
                tlb_Reconcile.Enabled = False
            End If

            'Memory Leak
            ogloCCDReconcile = Nothing
            '

        End If
    End Sub
    Public Sub FillProblemListForExam()
        Try
            '' Patient Information
            lblPatientCode.Text = strPatientCode
            lblPatientName.Text = strPatientFirstName & "  " & strPatientLastName
            lblAge.Text = strPatientAge
            'Memory Leak
            ' dtProblems = New DataTable
            '
            If _ProblemID > 0 Or _VisitID > 0 Then
                Dim objProblemList As New clsPatientProblemList

                Dim _strSQL As String = ""
                oDB = New gloStream.gloDataBase.gloDataBase
                oDB.Connect(GetConnectionString)

                ''''''''''''' Added by Sanjog -As on 28-05-2010
                _strSQL = "SELECT nProblemID as ProblemID, nVisitID AS VisitID ,dtDOS AS dtDOS,dtDischargeDate AS dtDischargeDate ," _
                & " RTRIM(LTRIM(sICD9Code)) + '-' + RTRIM(LTRIM(sICD9Desc)) AS Diagnosis,  " _
                & "sCheifComplaint AS Complaint ,nProblemStatus AS Status, (case when dtResolvedDate='01/01/1900' then space(10)" _
                & "else convert(varchar,dtResolvedDate,101) end) as ResolvedDt, isnull(sResolvedComment,'') as Comment," _
                & " IsNull(sPrescription,'') as Prescription, ISNULL(User_MST.nUserID,0) AS nUserID,ISNULL(nImmediacy,3) As Immediacy,ISNULL(sComments,'')" _
                & " as Comments,ISNULL(nProblemStatus,0) As nStatus,ISNULL(sProvider,'') as Provider,ISNULL(sLocation,'') " _
                & "as Location,ISNULL(dtModifiedDate,101) as ModifiedDate,ISNULL(sExamID,0) as ExamID,ISNULL(sConceptID,'') as sConceptID,ISNULL(sSnoMedID,'') as sSnoMedID,ISNULL(sDescriptionID,'') as sDescriptionID,ISNULL(sDescription,'') as sDescription,ISNULL(sTransactionID1,'') as sTransactionID1, ISNULL(User_MST.sLoginName,'') AS UserName,bIsEncounterDiagnosis as IsEncounterDiagnosis ,ISNULL(nICDRevision,9) as nICDRevision " _
                & " FROM User_MST RIGHT OUTER JOIN ProblemList ON User_MST.nUserID = problemlist.nUserID   WHERE  ProblemList.nPatientID  = " & _PatientID & "  ORDER BY dtDOS DESC"
                ''''''''''''' Added by Sanjog -As on 28-05-2010
                ''nICdrevision added for ICD10 implementation
                dtProblems = oDB.ReadQueryDataTable(_strSQL)
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing

                '' Table dt Contains following Columns
                '' ProblemID, VisitID , dtDOS, Diagnosis, Complaint ,Status

                'Memory Leak
                If Not IsNothing(objProblemList) Then
                    objProblemList.Dispose()
                    objProblemList = Nothing
                End If
                '

                If IsNothing(dtProblems) = False Then
                    Call SetGridStyle(dtProblems)
                    btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                    btn_Remove.BackgroundImageLayout = ImageLayout.Center

                    btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                    btn_Inactive.BackgroundImageLayout = ImageLayout.Center

                    btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                    btn_Both.BackgroundImageLayout = ImageLayout.Center

                    btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                    btn_Inactive2.BackgroundImageLayout = ImageLayout.Center

                    btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                    btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center

                End If
            Else
                '' For New Problem List
                _VisitID = GenerateVisitID(Date.Now, _PatientID)
                Call Fill_Status()
            End If
            DisplayHealthPlanAlerts()
            If IsNothing(dtProblems) = False AndAlso dtProblems.Rows.Count > 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Problem list opened", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Problem list opened", gloAuditTrail.ActivityOutCome.Success)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If IsNothing(dtProblems) = False Then
                dtProblems.Dispose()
                dtProblems = Nothing
            End If
        End Try
        FillActiveProblem()

        If blnRxMedToProblem = True Then
            If Not IsNothing(ArrRx_Problem) Then

                For i As Integer = 0 To c1ProblemList.Rows.Count - 1
                    Try
                        Dim ToLine As Integer = 0
                        ToLine = c1ProblemList.Rows(i)(COL_COMPLAINTS).ToString().IndexOf(vbNewLine)
                        If ToLine >= 1 Then
                            If ArrRx_Problem.Values.Contains(c1ProblemList.Rows(i)(COL_COMPLAINTS).ToString().Substring(0, ToLine)) = True Then
                                c1ProblemList.SetCellCheck(i, COL_SELECT, CheckEnum.Checked)
                            End If
                        Else
                            If ArrRx_Problem.Values.Contains(c1ProblemList.Rows(i)(COL_COMPLAINTS).ToString()) = True Then
                                c1ProblemList.SetCellCheck(i, COL_SELECT, CheckEnum.Checked)

                            End If

                        End If


                    Catch ex As Exception

                    End Try

                Next
            End If
        End If

    End Sub
    Private Sub InitializeToolStrip()
        tblPraoblemList.ConnectionString = GetConnectionString()
        tblPraoblemList.ModuleName = Me.Name
        tblPraoblemList.UserID = gnLoginID

    End Sub
    Private Sub Fill_StatusCombo()
        With cmbStatus
            .Items.Add("All")
            .Items.Add("Resolved")
            .Items.Add("Active")
            .Items.Add("Inactive")
            .Items.Add("Chronic")
            .Items.Add("Removed")
            .SelectedIndex = 0
        End With

    End Sub

    Private Sub Fill_Status()
        Try
            _isFormLoad = True
            With c1ProblemList
                .Redraw = False
                .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = (.Width - 20) / 11
                .Cols.Count = COLUMN_COUNT
                .AllowSorting = True
                .Rows.Fixed = 1
                .AllowEditing = False

                .Styles.ClearUnused()

                .Cols(COL_VISITID).Width = .Width * 0
                .Cols(COL_VISITID).AllowEditing = False
                .SetData(0, COL_VISITID, "VisitID")
                .Cols(COL_VISITID).TextAlignFixed = TextAlignEnum.CenterCenter

                .Cols(COL_PROBLEMID).Width = .Width * 0
                .Cols(COL_PROBLEMID).AllowEditing = False

                .Cols(COL_DOS).Width = _TotalWidth * 1.2
                .SetData(0, COL_DOS, "Date")
                If (gblnEnableCQMCypressTesting) Then
                    .Cols(COL_DOS).DataType = GetType(DateTime)
                Else
                    .Cols(COL_DOS).DataType = GetType(Date)
                End If

                .Cols(COL_DOS).AllowEditing = False

                .Cols(Col_DischargeDate).Width = _TotalWidth * 0
                .SetData(0, Col_DischargeDate, "Date")
                If (gblnEnableCQMCypressTesting) Then
                    .Cols(Col_DischargeDate).DataType = GetType(DateTime)
                Else
                    .Cols(Col_DischargeDate).DataType = GetType(Date)
                End If

                .Cols(Col_DischargeDate).AllowEditing = False
                .Cols(Col_DischargeDate).Visible = False


                .Cols(COL_COMPLAINTS).Width = _TotalWidth * 2.7
                .SetData(0, COL_COMPLAINTS, "Description")
                .Cols(COL_COMPLAINTS).AllowEditing = False

                .Cols(COL_DIAGNOSIS).Width = _TotalWidth * 2.7
                .SetData(0, COL_DIAGNOSIS, "Diagnosis")
                .Cols(COL_DIAGNOSIS).AllowEditing = False

                .Cols(COL_DIAGNOSISBUTTON).Width = _TotalWidth * 0.4

                .Cols(COL_PRESCRIPTION).Width = _TotalWidth * 2.7
                .SetData(0, COL_PRESCRIPTION, "Prescription")
                .Cols(COL_PRESCRIPTION).AllowEditing = False
                .Cols(COL_RxBUTTON).Width = _TotalWidth * 0.4

                ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 130110

                .Cols(COL_RsDt).Width = _TotalWidth * 1.2
                .SetData(0, COL_RsDt, "Resolved Date")
                .Cols(COL_RsDt).DataType = GetType(Date)
                .Cols(COL_RsDt).AllowEditing = False
                .Cols(COL_RsDt).Visible = True

                ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 130110

                .Cols(COL_USER).Width = .Width * 0
                .SetData(0, COL_USER, "UserID")
                .Cols(COL_USER).AllowEditing = False
                .Cols(COL_EXAMNAMEBUTTON).Width = _TotalWidth * 1.0
                .SetData(0, COL_EXAMNAMEBUTTON, "Open Exam")
                .Cols(COL_EXAMNAMEBUTTON).Width = Width * 0
                '''' 
                If blnOpenFromExam Then
                    .Cols(COL_EXAMNAMEBUTTON).Width = Width * 0
                End If

                .Cols(COL_Immediacy).Width = _TotalWidth * 1.2
                .SetData(0, COL_Immediacy, "Immediacy")
                .Cols(COL_Immediacy).AllowEditing = False
                .Cols(COL_ProblemSatus).Width = _TotalWidth * 1.2
                .SetData(0, COL_ProblemSatus, "Status")
                .Cols(COL_ProblemSatus).AllowEditing = True

                .Cols(COL_Provider).Width = _TotalWidth * 1.2
                .SetData(0, COL_Provider, "Provider")
                .Cols(COL_Provider).AllowEditing = False
                .Cols(COL_Location).Width = _TotalWidth * 1.2
                .SetData(0, COL_Location, "Location")
                .Cols(COL_Location).AllowEditing = False
                .Cols(COL_LastModified).Width = _TotalWidth * 1.2
                .SetData(0, COL_LastModified, "Last Update")
                .Cols(COL_LastModified).AllowEditing = False

                .Cols(COL_DIAGNOSISBUTTON).Visible = False

                .Cols(COL_RxBUTTON).Visible = False
                .Cols(COL_ExamID).Visible = False
                '' chetan added on 07-sept-2010 for displaying column if coming from drugs else set it to false 
                If blnRxMedToProblem = True Then
                    .Cols(COL_SELECT).Width = _TotalWidth * 0.6
                    .SetData(0, COL_SELECT, "Select")
                    .Cols(COL_SELECT).DataType = GetType(Boolean)
                    .Cols(COL_SELECT).AllowEditing = True
                Else
                    .Cols(COL_SELECT).Visible = False
                    .Cols(COL_SELECT).Width = _TotalWidth * 0.6
                End If
                .Cols(COL_ProblemType).Width = _TotalWidth * 1.2
                .SetData(0, COL_ProblemType, "Problem Type")
                .Cols(COL_ProblemType).AllowEditing = False
                '' chetan changed
                .Cols(COL_ConceptID).Width = _TotalWidth * 1.2
                .SetData(0, COL_ConceptID, "SnoMed CT ID")
                .Cols(COL_ConceptID).AllowEditing = False

                .Cols(COL_SnoMedID).Width = _TotalWidth * 0
                .SetData(0, COL_SnoMedID, "SnoMed ID")
                .Cols(COL_SnoMedID).AllowEditing = False

                .Cols(COL_DescriptionID).Width = _TotalWidth * 0
                .SetData(0, COL_DescriptionID, "Description ID")
                .Cols(COL_DescriptionID).AllowEditing = False

                .Cols(COL_Defination).Width = _TotalWidth * 0
                .SetData(0, COL_Defination, "Defination")
                .Cols(COL_Defination).AllowEditing = False

                .Cols(Col_ReasonConceptID).Width = _TotalWidth * 0
                .SetData(0, Col_ReasonConceptID, "Reason Concept ID")
                .Cols(Col_ReasonConceptID).AllowEditing = False

                .Cols(Col_ReasonConceptDesc).Width = _TotalWidth * 0
                .SetData(0, Col_ReasonConceptDesc, "Reason Concept Description")
                .Cols(Col_ReasonConceptDesc).AllowEditing = False
                .Redraw = True




            End With
        Catch ex As Exception
        Finally
            _isFormLoad = False
        End Try
    End Sub

    Private Sub SetGridStyle(ByVal dt As DataTable)

        Try
            _isFormLoad = True
            Dim _strComplaints As String
            Dim _Comments() As String
            Dim _nCommentCount As Integer
            With c1ProblemList

                Dim i As Int16
                .Redraw = False
                .Dock = DockStyle.Fill

                Dim _TotalWidth As Single = 0
                PrevGridWidth = .Width
                _TotalWidth = (.Width - 20) / 12
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing

                .Cols.Count = COLUMN_COUNT
                .Rows.Count = 1
                .Rows.Fixed = 1

                .AllowResizing = AllowDraggingEnum.Both ''True

                .Styles.ClearUnused()

                .Cols(COL_VISITID).Width = .Width * 0
                .Cols(COL_VISITID).AllowEditing = False
                .Cols(COL_VISITID).Visible = False
                .SetData(0, COL_VISITID, "VisitID")
                .Cols(COL_VISITID).TextAlignFixed = TextAlignEnum.CenterCenter

                .Cols(COL_PROBLEMID).Width = .Width * 0
                .Cols(COL_PROBLEMID).AllowEditing = False
                .Cols(COL_PROBLEMID).Visible = False
                .Cols(COL_DOS).Width = _TotalWidth * 1.2 ''1.2
                .SetData(0, COL_DOS, "Date")
                If (gblnEnableCQMCypressTesting) Then
                    .Cols(COL_DOS).DataType = GetType(DateTime)
                Else
                    .Cols(COL_DOS).DataType = GetType(Date)
                End If



                .Cols(COL_DOS).AllowEditing = False
                .Cols(COL_COMPLAINTS).Width = _TotalWidth * 3.0  ''2.7
                .SetData(0, COL_COMPLAINTS, "Description")

                .Cols(COL_COMPLAINTS).AllowEditing = False

                If gblnEducationMaterialEnabled Then
                    .Cols(COL_INFOBUTTON).Width = _TotalWidth * 0.3
                Else
                    .Cols(COL_INFOBUTTON).Width = 0
                End If

                .Cols(Col_DischargeDate).Width = .Width * 0
                .SetData(0, Col_DischargeDate, "Discharge Date")
                If (gblnEnableCQMCypressTesting) Then
                    .Cols(Col_DischargeDate).DataType = GetType(DateTime)
                Else
                    .Cols(Col_DischargeDate).DataType = GetType(Date)
                End If
                .Cols(Col_DischargeDate).Visible = False
                .Cols(Col_DischargeDate).AllowEditing = False

                '.Cols(COL_PROVIDEREDUCATIONBUTTON).Width = _TotalWidth * 0.3
                '.Cols(COL_PATIENTEDUCATIONBUTTON).Width = _TotalWidth * 0.3

                .Cols(COL_DIAGNOSIS).Width = _TotalWidth * 4.5  ''2.7
                .SetData(0, COL_DIAGNOSIS, "Diagnosis")

                .Cols(COL_DIAGNOSIS).AllowEditing = False

                .Cols(COL_DIAGNOSISBUTTON).Width = _TotalWidth * 0.4


                .Cols(COL_PRESCRIPTION).Width = _TotalWidth * 3.5  ''2.7
                .SetData(0, COL_PRESCRIPTION, "Prescription")

                .Cols(COL_PRESCRIPTION).AllowEditing = False

                .Cols(COL_RxBUTTON).Width = _TotalWidth * 0.4

                .Cols(Col_UserName).Width = _TotalWidth * 1.5  ''2.7
                .SetData(0, Col_UserName, "User")

                .Cols(Col_UserName).AllowEditing = False

                .Cols(COL_RsDt).Width = _TotalWidth * 1.4
                .SetData(0, COL_RsDt, "Resolved Date")
                .Cols(COL_RsDt).DataType = GetType(Date)
                .Cols(COL_RsDt).AllowEditing = False
                .Cols(COL_RsDt).Visible = True


                .Cols(COL_USER).Width = .Width * 0
                .SetData(0, COL_USER, "UserID")

                .Cols(COL_USER).AllowEditing = False
                .Cols(COL_USER).Visible = False
                .Cols(COL_EXAMNAMEBUTTON).Width = _TotalWidth * 1.0
                .SetData(0, COL_EXAMNAMEBUTTON, "Open Exam")
                .Cols(COL_EXAMNAMEBUTTON).Width = Width * 0
                '''' 
                If blnOpenFromExam Then
                    .Cols(COL_EXAMNAMEBUTTON).Width = Width * 0
                End If


                .Cols(COL_Immediacy).Width = _TotalWidth * 1.2
                .SetData(0, COL_Immediacy, "Immediacy")

                .Cols(COL_Immediacy).AllowEditing = False

                .Cols(COL_ProblemSatus).Width = _TotalWidth * 1.0  ''1.2
                .SetData(0, COL_ProblemSatus, "Status")

                .Cols(COL_ProblemSatus).AllowEditing = False
                .Cols(COL_Provider).Width = _TotalWidth * 1.2
                .SetData(0, COL_Provider, "Provider")

                .Cols(COL_Provider).AllowEditing = False

                .Cols(COL_Location).Width = _TotalWidth * 1.2
                .SetData(0, COL_Location, "Location")

                .Cols(COL_Location).AllowEditing = False

                .Cols(COL_LastModified).Width = _TotalWidth * 1.2
                .SetData(0, COL_LastModified, "Last Update")

                .Cols(COL_LastModified).AllowEditing = False

                .Cols(COL_ExamID).Width = 0
                .SetData(0, COL_ExamID, "ExamID")
                .Cols(COL_ExamID).AllowEditing = False
                .Cols(COL_ExamID).Visible = False
                .Cols(Col_HiddedPrescription).Width = 0
                .SetData(0, Col_HiddedPrescription, "HiddenPres")
                .Cols(Col_HiddedPrescription).AllowEditing = False
                .Cols(Col_HiddedPrescription).Visible = False
                .Cols(COL_DIAGNOSISBUTTON).Visible = False

                .Cols(COL_RxBUTTON).Visible = False


                If blnRxMedToProblem = True Then
                    .Cols(COL_SELECT).Width = _TotalWidth * 0.6
                    .SetData(0, COL_SELECT, "Select")
                    .Cols(COL_SELECT).DataType = GetType(Boolean)

                    .Cols(COL_SELECT).AllowEditing = True
                Else
                    .Cols(COL_SELECT).Visible = False

                End If

                .Cols(COL_ProblemType).Width = _TotalWidth * 1.2
                .SetData(0, COL_ProblemType, "Problem Type")

                .Cols(COL_ProblemType).AllowEditing = False

                .Cols(COL_ConceptID).Width = _TotalWidth * 1.4

                .SetData(0, COL_ConceptID, "SnoMed CT ID") ' Header changed to 

                .Cols(COL_ConceptID).AllowEditing = False

                .Cols(COL_SnoMedID).Width = _TotalWidth * 0
                .SetData(0, COL_SnoMedID, "SnoMed ID")

                .Cols(COL_SnoMedID).AllowEditing = False

                .Cols(COL_DescriptionID).Width = _TotalWidth * 1.2
                .SetData(0, COL_DescriptionID, "Desc. ID")

                .Cols(COL_DescriptionID).AllowEditing = False
                .Cols(COL_DescriptionID).Visible = False

                .Cols(COL_Defination).Width = _TotalWidth * 0
                .SetData(0, COL_Defination, "Defination")

                .Cols(COL_Defination).AllowEditing = False

                .Cols(Col_ReasonConceptID).Width = _TotalWidth * 0
                .SetData(0, Col_ReasonConceptID, "Reason Concept ID")
                .Cols(Col_ReasonConceptID).AllowEditing = False
                .Cols(Col_ReasonConceptID).Visible = False

                .Cols(Col_ReasonConceptDesc).Width = _TotalWidth * 1.2
                .SetData(0, Col_ReasonConceptDesc, "Reason Concept Description")
                .Cols(Col_ReasonConceptDesc).AllowEditing = False
                .Cols(Col_ReasonConceptDesc).Visible = False

                .Cols(Col_EncounterDiagnosis).DataType = GetType(Boolean)
                .SetData(0, Col_EncounterDiagnosis, "Encounter/Diagnosis")
                .Cols(Col_EncounterDiagnosis).Width = _TotalWidth * 1.2
                .Cols(Col_EncounterDiagnosis).Visible = False
                .Cols(Col_IsModifed).Width = _TotalWidth * 0
                .SetData(0, Col_IsModifed, "IsModified")
                .Cols(Col_ICDRevision).Width = _TotalWidth * 0
                .SetData(0, Col_ICDRevision, "ICDRevision")

                .Cols(col_ConcernStatus).Width = _TotalWidth * 0
                .SetData(0, col_ConcernStatus, "Concern Status")


                .Cols(Col_CDAProblemType).Width = _TotalWidth * 0
                .SetData(0, Col_CDAProblemType, "Problem Type")
                For i = 0 To dt.Rows.Count - 1
                    .Rows.Add()



                    .SetData(i + 1, COL_VISITID, dt.Rows(i)("VisitID"))

                    If (gblnEnableCQMCypressTesting) Then
                        .SetData(i + 1, COL_DOS, Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy hh:mm:ss tt"))
                    Else
                        .SetData(i + 1, COL_DOS, Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy"))
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(i)("dtDischargeDate"))) Then
                        If (gblnEnableCQMCypressTesting) Then
                            .SetData(i + 1, Col_DischargeDate, Format(dt.Rows(i)("dtDischargeDate"), "MM/dd/yyyy hh:mm:ss tt"))
                        Else
                            .SetData(i + 1, Col_DischargeDate, Format(dt.Rows(i)("dtDischargeDate"), "MM/dd/yyyy"))
                        End If
                    End If

                    .SetData(i + 1, COL_PROBLEMID, dt.Rows(i)("ProblemID"))

                    .SetData(i + 1, COL_DIAGNOSIS, dt.Rows(i)("Diagnosis").ToString.Trim() & "")
                    .SetData(i + 1, Col_UserName, dt.Rows(i)("UserName").ToString.Trim() & "")

                    'Added Infobutton
                    .SetCellImage(i + 1, COL_INFOBUTTON, My.Resources.infobutton)
                    '.SetCellImage(i + 1, COL_PROVIDEREDUCATIONBUTTON, My.Resources.I)
                    '.SetCellImage(i + 1, COL_PATIENTEDUCATIONBUTTON, My.Resources.I)

                    .SetData(i + 1, COL_PRESCRIPTION, dt.Rows(i)("Prescription").ToString().Replace("!", ","))

                    .SetData(i + 1, Col_HiddedPrescription, dt.Rows(i)("Prescription"))


                    If Trim(dt.Rows(i)("ResolvedDt").ToString()) <> "" Then
                        .SetData(i + 1, COL_RsDt, dt.Rows(i)("ResolvedDt"))
                    Else
                        .SetData(i + 1, COL_RsDt, DBNull.Value)
                    End If

                    .SetData(i + 1, COL_USER, dt.Rows(i)("nUserID"))


                    .SetData(i + 1, COL_DIAGNOSISBUTTON, "")
                    Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_DIAGNOSISBUTTON, i + 1, COL_DIAGNOSISBUTTON)
                    rgDig.Style = cStyle

                    .SetData(i + 1, COL_DIAGNOSIS, dt.Rows(i)("Diagnosis").ToString.Trim() & "")

                    .SetData(i + 1, COL_RxBUTTON, "")
                    Dim rgRx As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_RxBUTTON, i + 1, COL_RxBUTTON)
                    rgRx.Style = cStyle


                    If dt.Rows(i)("Immediacy") = EnmImmediacy.Acute Then
                        .SetData(i + 1, COL_Immediacy, "Acute")

                    ElseIf dt.Rows(i)("Immediacy") = EnmImmediacy.Chronic Then
                        .SetData(i + 1, COL_Immediacy, "Chronic")
                    ElseIf dt.Rows(i)("Immediacy") = EnmImmediacy.unknown Then
                        .SetData(i + 1, COL_Immediacy, "unknown")
                    End If


                    If dt.Rows(i)("nStatus") = Status.Resolved Then
                        .SetData(i + 1, COL_ProblemSatus, "Resolved")
                    ElseIf dt.Rows(i)("nStatus") = Status.Active Then
                        .SetData(i + 1, COL_ProblemSatus, "Active")
                    ElseIf dt.Rows(i)("nStatus") = Status.Inactive Then
                        .SetData(i + 1, COL_ProblemSatus, "Inactive")
                    ElseIf dt.Rows(i)("nStatus") = Status.Resolved Then
                        .SetData(i + 1, COL_ProblemSatus, "Resolved")
                    ElseIf dt.Rows(i)("nStatus") = Status.Chronic Then
                        .SetData(i + 1, COL_ProblemSatus, "Chronic")
                    End If

                    .SetData(i + 1, COL_Provider, dt.Rows(i)("Provider"))
                    .SetData(i + 1, COL_Location, dt.Rows(i)("Location"))
                    If Not IsNothing(dt.Rows(i)("ModifiedDate")) Then
                        If Not dt.Rows(i)("ModifiedDate").ToString().Contains("4/12/1900") Then
                            .SetData(i + 1, COL_LastModified, dt.Rows(i)("ModifiedDate"))
                        End If
                    End If
                    .SetData(i + 1, COL_ExamID, dt.Rows(i)("ExamID"))
                    If Not IsNothing(dt.Rows(i)("Comments")) Then
                        If dt.Rows(i)("Comments") <> "" Then
                            _strComplaints = dt.Rows(i)("Complaint") & vbNewLine & dt.Rows(i)("Comments")
                            _Comments = Split(dt.Rows(i)("Comments"), vbNewLine)
                            _nCommentCount = _Comments.Length + 1
                        Else
                            _strComplaints = dt.Rows(i)("Complaint")
                        End If
                    Else
                        _strComplaints = dt.Rows(i)("Complaint")
                    End If
                    .Rows(.Row).AllowResizing = AllowDraggingEnum.Both
                    .Rows(.Row).AllowDragging = DrawModeEnum.OwnerDraw
                    If _nCommentCount <> 0 Then
                        .Rows(.Rows.Count - 1).Height = c1ProblemList.Rows.DefaultSize * _nCommentCount
                    End If
                    .Cols(COL_COMPLAINTS).TextAlign = TextAlignEnum.LeftCenter
                    .Cols(COL_DIAGNOSIS).TextAlign = TextAlignEnum.LeftCenter
                    .Cols(COL_ConceptID).TextAlign = TextAlignEnum.LeftCenter

                    .SetData(i + 1, COL_COMPLAINTS, _strComplaints.Trim)
                    .SetData(i + 1, COL_ProblemType, dt.Rows(i)("sTransactionID1").ToString.Trim())
                    .SetData(i + 1, COL_ConceptID, dt.Rows(i)("sConceptID").ToString.Trim())
                    .SetData(i + 1, COL_SnoMedID, dt.Rows(i)("sSnoMedID").ToString.Trim())
                    .SetData(i + 1, COL_DescriptionID, dt.Rows(i)("sDescriptionID").ToString.Trim())
                    .SetData(i + 1, COL_Defination, dt.Rows(i)("sDescription").ToString.Trim())
                    .SetData(i + 1, Col_IsModifed, False)
                    .SetData(i + 1, Col_ICDRevision, dt.Rows(i)("nICDRevision")) ''added for ICD10 implementation
                    .SetData(i + 1, Col_EncounterDiagnosis, dt.Rows(i)("IsEncounterDiagnosis"))
                    .SetData(i + 1, Col_ReasonConceptID, dt.Rows(i)("sReasonConceptID").ToString.Trim())
                    .SetData(i + 1, Col_ReasonConceptDesc, dt.Rows(i)("sReasonConceptDesc").ToString.Trim())
                    .SetData(i + 1, col_ConcernStatus, dt.Rows(i)("sConcernStatus").ToString.Trim())
                    .SetData(i + 1, Col_CDAProblemType, dt.Rows(i)("sProblemType").ToString.Trim())
                Next

                .Redraw = True
                c1ProblemList.SelectionMode = SelectionModeEnum.Row  'swaraj 20100629
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally
            _isFormLoad = False
        End Try

    End Sub

    Public Function ValidateProblemList() As Boolean
        Try
            For i As Int16 = 1 To c1ProblemList.Rows.Count - 1
                If Trim(c1ProblemList.GetData(i, COL_ProblemSatus)) = "Resolved" Then
                    If c1ProblemList.GetData(i, COL_RsDt) Is Nothing Then
                        MessageBox.Show(" Enter resolve date", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1ProblemList.SelectionMode = SelectionModeEnum.Cell
                        c1ProblemList.Select(i, COL_RsDt, True)

                        Return False
                        ''                 
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub SaveProblemList(Optional ByVal _IsMouseDown As Boolean = False)
        Try
            Dim objProblemList As New clsPatientProblemList
            Dim ProblemStatus As Integer
            Dim ICD9Code As String = ""
            Dim ICD9Desc As String = ""
            Dim nStatus As Integer
            Dim Arrlist As New ArrayList

            Arrlist.Clear()
            With c1ProblemList
                .Select(0, 0, False)
                For i As Int16 = 1 To .Rows.Count - 1
                    ICD9Code = ""
                    ICD9Desc = ""
                    If Trim(.GetData(i, COL_COMPLAINTS)) = "" And Convert.ToInt64(.GetData(i, COL_PROBLEMID)) = 0 Then

                    Else

                        If Trim(.GetData(i, COL_ProblemSatus)) = "Active" Then
                            ProblemStatus = Status.Active
                        ElseIf Trim(.GetData(i, COL_ProblemSatus)) = "Resolved" Then
                            ProblemStatus = Status.Resolved

                        ElseIf Trim(.GetData(i, COL_ProblemSatus)) = "Inactive" Then
                            ProblemStatus = Status.Inactive
                        ElseIf Trim(.GetData(i, COL_ProblemSatus)) = "Chronic" Then
                            ProblemStatus = Status.Chronic

                        Else

                            ProblemStatus = Status.Active
                        End If

                        _ProblemID = .GetData(i, COL_PROBLEMID)
                        Dim str() As String
                        str = Split(.GetData(i, COL_DIAGNOSIS), "-", 2)
                        If str.Length > 1 Then
                            ICD9Code = str(0)
                            ICD9Desc = str(1)
                        End If
                        Dim lst As New myList
                        With lst
                            .ID = _ProblemID
                            .Code = ICD9Code.Trim
                            .Description = ICD9Desc.Trim
                            Dim rg As C1.Win.C1FlexGrid.CellRange = c1ProblemList.GetCellRange(i, COL_PRESCRIPTION, i, COL_PRESCRIPTION)
                            Dim RxStyle As CellStyle = rg.Style()
                            .HistoryCategory = c1ProblemList.GetData(i, Col_HiddedPrescription)

                            .Index = CLng(c1ProblemList.GetData(i, COL_USER))
                            If Not c1ProblemList.GetData(i, COL_DOS) Is Nothing Then
                                If (gblnEnableCQMCypressTesting) Then
                                    .VisitDate = Format(Convert.ToDateTime(c1ProblemList.GetData(i, COL_DOS)), "MM/dd/yyyy hh:mm:ss tt")
                                Else
                                    .VisitDate = Format(Convert.ToDateTime(c1ProblemList.GetData(i, COL_DOS)), "MM/dd/yyyy")
                                End If
                            Else
                                If (gblnEnableCQMCypressTesting) Then
                                    .VisitDate = Format(Now, "MM/dd/yyyy hh:mm:ss tt")
                                Else
                                    .VisitDate = Format(Now, "MM/dd/yyyy")
                                End If
                            End If

                            If Not c1ProblemList.GetData(i, Col_DischargeDate) Is Nothing Then
                                If (gblnEnableCQMCypressTesting) Then
                                    .DischargeDate = Format(Convert.ToDateTime(c1ProblemList.GetData(i, Col_DischargeDate)), "MM/dd/yyyy hh:mm:ss tt")
                                    '      Else
                                    '      .DischargeDate = Format(Convert.ToDateTime(c1ProblemList.GetData(i, Col_DischargeDate)), "MM/dd/yyyy")
                                End If
                            Else
                                If (gblnEnableCQMCypressTesting) Then
                                    .DischargeDate = Format(Now, "MM/dd/yyyy hh:mm:ss tt")
                                    '    Else
                                    '        .DischargeDate = Format(Now, "MM/dd/yyyy")
                                Else
                                    .DischargeDate = Nothing
                                End If
                            End If

                            ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110                            
                            Try
                                If Not IsNothing(c1ProblemList.GetData(i, COL_RsDt)) AndAlso Convert.ToString(c1ProblemList.GetData(i, COL_RsDt)).Trim() <> "" Then
                                    If c1ProblemList.GetData(i, COL_RsDt) <> "1/1/0001" Then
                                        .ResolvedDate = Format(c1ProblemList.GetData(i, COL_RsDt), "MM/dd/yyyy")
                                    Else
                                        .ResolvedDate = Format(Convert.ToDateTime("01/01/1900"), "MM/dd/yyyy")
                                    End If
                                Else
                                    .ResolvedDate = Format(Convert.ToDateTime("01/01/1900"), "MM/dd/yyyy")

                                End If

                            Catch ex As Exception
                                .ResolvedDate = Format(Convert.ToDateTime("01/01/1900"), "MM/dd/yyyy")
                            End Try

                            If Trim(c1ProblemList.GetData(i, COL_ProblemSatus)) = "Active" Then
                                nStatus = Status.Active
                            ElseIf Trim(c1ProblemList.GetData(i, COL_ProblemSatus)) = "Inactive" Then
                                nStatus = Status.Inactive
                            ElseIf Trim(c1ProblemList.GetData(i, COL_ProblemSatus)) = "Resolved" Then
                                nStatus = Status.Resolved
                            ElseIf Trim(c1ProblemList.GetData(i, COL_ProblemSatus)) = "Chronic" Then
                                nStatus = Status.Chronic
                            End If
                            .Status = nStatus

                            .Value = nStatus '' chetan added
                            If Trim(c1ProblemList.GetData(i, COL_Immediacy)) = "Acute" Then
                                nImmediacy = EnmImmediacy.Acute
                            ElseIf Trim(c1ProblemList.GetData(i, COL_Immediacy)) = "Chronic" Then
                                nImmediacy = EnmImmediacy.Chronic
                            ElseIf Trim(c1ProblemList.GetData(i, COL_Immediacy)) = "unknown" Then
                                nImmediacy = EnmImmediacy.unknown
                                'Sanjog Added on 2011 may 27 to handle is problem come from chief Compaint = its dnt have any Immediacy out of three
                            Else
                                nImmediacy = 0
                            End If
                            'Sanjog Added on 2011 may 27 to handle is problem come from chief Compaint = its dnt have any Immediacy out of three


                            ''For Problem list

                            If Not IsNothing(c1ProblemList.GetData(i, COL_COMPLAINTS)) Then
                                Dim comp() As String = Split(c1ProblemList.GetData(i, COL_COMPLAINTS).ToString(), vbNewLine, 2)
                                .ParameterName = comp.GetValue(0)
                                If comp.Length > 1 Then
                                    .Comments = comp.GetValue(1)
                                Else
                                    .Comments = ""
                                End If
                            End If

                            ''For Problem list

                            .Immediacy = nImmediacy
                            .Provider = c1ProblemList.GetData(i, COL_Provider)
                            .Location = c1ProblemList.GetData(i, COL_Location)
                            .LastModified = CType(c1ProblemList.GetData(i, COL_LastModified), DateTime)
                            .ExamID = c1ProblemList.GetData(i, COL_ExamID)
                            ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110

                            .HistoryItem = c1ProblemList.GetData(i, COL_VISITID)
                            .AssociatedProperty = c1ProblemList.GetData(i, COL_ProblemType)
                            .ConceptId = c1ProblemList.GetData(i, COL_ConceptID)
                            .DescId = c1ProblemList.GetData(i, COL_DescriptionID)
                            .SnowMadeID = c1ProblemList.GetData(i, COL_SnoMedID)
                            .SnoDescription = c1ProblemList.GetData(i, COL_Defination)
                            .ReasonConceptID = c1ProblemList.GetData(i, Col_ReasonConceptID)
                            .ReasonConceptDesc = c1ProblemList.GetData(i, Col_ReasonConceptDesc)
                            If .HistoryItem.Trim() = "" Then
                                .HistoryItem = "0"
                            End If
                            If Trim(c1ProblemList.GetData(i, COL_ProblemSatus)) = "Removed" Then
                                lst.ParameterName = ""
                            End If

                            If Not IsNothing(c1ProblemList.GetData(i, Col_EncounterDiagnosis)) Then
                                .IsEncounterDiagnosis = (c1ProblemList.GetData(i, Col_EncounterDiagnosis))
                            End If
                            .IsModified = c1ProblemList.GetData(i, Col_IsModifed)

                            .nICDRevision = c1ProblemList.GetData(i, Col_ICDRevision)   ''added for ICD10 implementation
                            .sConcernStatus = c1ProblemList.GetData(i, col_ConcernStatus)
                            .sCDAProblemType = c1ProblemList.GetData(i, Col_CDAProblemType) ''(2015 Certification)
                        End With
                        Arrlist.Add(lst)
                        lst = Nothing 'Change made to solve memory Leak and word crash issue
                    End If

                Next
            End With

            '' Save (Insert / Update / Delete Problem List)
            SaveProblemMedicationReconcillation()
            objProblemList.SaveProblemList(_PatientID, _VisitID, Arrlist, dtRecProblist)
            If IsNothing(dtRecProblist) = False AndAlso dtRecProblist.Rows.Count > 0 AndAlso blnmedRecupdated = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ClinicalReconciliation, gloAuditTrail.ActivityType.Save, "Problem Reconciliation Added From Problem List", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            'objProblemList.InsertICD9(_PatientID, _VisitID, Arrlist)
            objProblemList.DeleteProblemList(_PatientID, DeletedProblemlist)
            objProblemList.Dispose()
            objProblemList = Nothing

            If blnRxMedFromExam = True Then
                BindProblemID(Arrlist)
                FillRx_Problemlist()
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
            If _IsMouseDown = False Then
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function getProviderTaxID(Optional ByVal nProviderID As Int64 = 0) As Boolean
        sProviderTaxID = ""
        nProviderAssociationID = 0
        Try
            Dim oResult As DialogResult = System.Windows.Forms.DialogResult.OK
            Dim oForm As New gloGlobal.frmSelectProviderTaxID(Convert.ToInt64(nProviderID))
            If oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count > 1 Then
                oForm.ShowDialog(Me)
                oResult = oForm.DialogResult
                nProviderAssociationID = oForm.nAssociationID
                sProviderTaxID = oForm.sProviderTaxID

                oForm = Nothing
            ElseIf oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count = 1 Then
                ''oResult = oForm.DialogResult
                nProviderAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows(0)("nAssociationID"))
                sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows(0)("sTIN"))
                oForm = Nothing
            Else
                nProviderAssociationID = 0
                sProviderTaxID = ""
            End If

            If oResult = Windows.Forms.DialogResult.OK Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return False

        Finally
        End Try
    End Function

    Private Function GetMedicalReconcillationID(ByVal CurrentVisitId As Long) As Long
        Dim MedicalReconcillationId As Long = 0
        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            conn = New SqlConnection
            Dim _strSQL As String = ""
            _strSQL = "SELECT ISNULL(MedicationReconcillationId,0) FROM MedicationReconcillation " &
                      "WHERE nVisitId= " & CurrentVisitId & " and nPatientId=" & _PatientID & " and nReconcillationType=" & 1 & ""
            conn.ConnectionString = GetConnectionString()
            conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            _strSQL = cmd.ExecuteScalar & ""
            If _strSQL <> "" Then
                MedicalReconcillationId = Convert.ToInt64(_strSQL)
            End If
            conn.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
        Return MedicalReconcillationId
    End Function


    Public Function BindProblemID(ByVal arr As ArrayList) As Boolean
        Dim isProblemID As Boolean = False
        Try
            Dim lstSetProblemID As myList
            For i As Int16 = 0 To arr.Count - 1
                lstSetProblemID = CType(arr(i), myList)
                If c1ProblemList.Rows.Count > 1 Then
                    Dim strProblemId As String = c1ProblemList.GetData(i + 1, COL_PROBLEMID)
                    Dim strProblemDesc As String = c1ProblemList.GetData(i + 1, COL_COMPLAINTS).ToString()
                    If String.IsNullOrWhiteSpace(strProblemId) Then
                        c1ProblemList.SetData(i + 1, COL_PROBLEMID, lstSetProblemID.ID)
                    End If
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return isProblemID
    End Function
    Public Sub FillRx_Problemlist()

        ArrRx_Problem = New Dictionary(Of String, String)
        With c1ProblemList
            For i As Integer = 0 To .Rows.Count - 1
                If .GetCellCheck(i, COL_SELECT) = CheckEnum.Checked Then
                    Dim ToIndex As Integer = 0
                    ToIndex = .GetData(i, COL_COMPLAINTS).ToString().IndexOf(vbNewLine)
                    If ToIndex > 1 Then
                        ArrRx_Problem.Add(Convert.ToString(.GetData(i, COL_PROBLEMID)), .GetData(i, COL_COMPLAINTS).ToString().Substring(0, ToIndex))
                    Else
                        ArrRx_Problem.Add(Convert.ToString(.GetData(i, COL_PROBLEMID)), .GetData(i, COL_COMPLAINTS).ToString())
                    End If
                End If
            Next
        End With
        'ArrRx_Problem = Nothing
    End Sub

    Private Sub c1ProblemList_BeforeRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles c1ProblemList.BeforeRowColChange
        'If pnlSelectExam.Visible = True Then
        '    pnlSelectExam.Visible = False
        'End If
        RemoveControlExam()
    End Sub

    Private Sub c1ProblemList_EnterCell(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c1ProblemList.EnterCell
        ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110        
        Dim objMessages As New clsMessage
        Dim UserID As Long
        UserID = objMessages.GetUserID(gstrLoginName)
        objMessages.Dispose()
        objMessages = Nothing
        Try
            If c1ProblemList.Col = COL_RsDt Then
                c1ProblemList.SelectionMode = SelectionModeEnum.Cell
                Try
                    If c1ProblemList.GetData(c1ProblemList.Row, COL_ProblemSatus).ToString() = "Resolved" Then
                        c1ProblemList.Cols(COL_RsDt).AllowEditing = True
                    Else
                        c1ProblemList.Cols(COL_RsDt).AllowEditing = False
                    End If
                Catch ex1 As Exception
                    c1ProblemList.Selection.Clear(ClearFlags.Content)
                End Try
            End If
            If _isFormLoad = False Then
                If c1ProblemList.RowSel > 0 Then
                    If c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination) <> "" Then

                        FillDefination(c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination))
                        trvDefination.ExpandAll()
                    Else
                        trvDefination.Nodes.Clear()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110
    End Sub

    Public Sub FillDefination(ByVal strDescription As String)
        Dim strHeader() As String
        Dim strDefination() As String
        Dim strHead As String
        Dim oIsNode As myTreeNode
        Dim oDescr As myTreeNode
        If strDescription <> "" And strDescription <> "Defination" Then
            trvDefination.Nodes.Clear()
            trvDefination.ImageList = Nothing
            trvDefination.ImageList = imgTreeVIew
            strHeader = Split(strDescription, "|")
            If strHeader.Length > 0 Then
                strHead = strHeader.GetValue(0)
                Dim oParenetNode As New myTreeNode
                oParenetNode.Text = strHead
                oParenetNode.ImageIndex = 5
                oParenetNode.SelectedImageIndex = 5

                For i As Integer = 1 To strHeader.Length - 1
                    strDefination = Split(strHeader.GetValue(i), ":")
                    oIsNode = New myTreeNode
                    oIsNode.Text = strDefination.GetValue(0)
                    oIsNode.ImageIndex = 4
                    oIsNode.SelectedImageIndex = 4

                    oDescr = New myTreeNode
                    oDescr.Text = strDefination.GetValue(1)
                    oDescr.ImageIndex = 3
                    oDescr.SelectedImageIndex = 3
                    oIsNode.Nodes.Add(oDescr)

                    oParenetNode.Nodes.Add(oIsNode)

                    ''Memory Leak
                    If Not IsNothing(oIsNode) Then
                        ' oIsNode.Dispose()
                        oIsNode = Nothing
                    End If
                    'If Not IsNothing(oDescr) Then
                    '    oDescr.Dispose()
                    '    oDescr = Nothing 'Change made to solve memory Leak and word crash issue
                    'End If

                Next

                trvDefination.Nodes.Add(oParenetNode)
                If Not IsNothing(oParenetNode) Then
                    'oParenetNode.Dispose()
                    oParenetNode = Nothing
                End If

            End If
        End If
    End Sub
    Private Sub c1ProblemList_LeaveCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1ProblemList.LeaveCell
        ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 150110
        Dim objMessages As New clsMessage
        Dim UserID As Long
        UserID = objMessages.GetUserID(gstrLoginName)
        objMessages.Dispose()
        objMessages = Nothing
        Try

            If c1ProblemList.Col = COL_ProblemSatus Then
                If c1ProblemList.Rows.Count > 1 Then
                    If c1ProblemList.Row > 0 Then
                        If c1ProblemList.GetData(c1ProblemList.Row, COL_ProblemSatus) <> Nothing AndAlso c1ProblemList.GetData(c1ProblemList.Row, COL_COMPLAINTS) <> Nothing Then
                            If c1ProblemList.GetData(c1ProblemList.Row, COL_ProblemSatus).ToString() = "Resolved" AndAlso c1ProblemList.GetData(c1ProblemList.Row, COL_COMPLAINTS).ToString().Trim() <> "" Then
                                c1ProblemList.Cols(COL_RsDt).AllowEditing = True
                                If c1ProblemList.GetData(c1ProblemList.Row, COL_RsDt) = Nothing Then
                                    c1ProblemList.SetData(c1ProblemList.Row, COL_RsDt, Now.Date.ToString)
                                End If
                            End If
                        End If
                    End If
                End If
            ElseIf c1ProblemList.Col = COL_RsDt Then
                c1ProblemList.SelectionMode = SelectionModeEnum.Cell
                Try
                    If c1ProblemList.GetData(c1ProblemList.Row, COL_ProblemSatus).ToString() = "Resolved" Then
                        c1ProblemList.Cols(COL_RsDt).AllowEditing = True
                    Else
                        c1ProblemList.Cols(COL_RsDt).AllowEditing = False
                        '' c1ProblemList.Selection.Clear(ClearFlags.Content)  
                    End If
                Catch ex1 As Exception
                    c1ProblemList.Selection.Clear(ClearFlags.Content)
                    ' c1ProblemList.Rows.Remove(c1ProblemList.Row)
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 150110
    End Sub

    Private Sub c1ProblemList_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ProblemList.AfterEdit
        c1ProblemList.AllowEditing = True
        c1ProblemList.Row = e.Row
        'Dim i As Integer
        Try

            With c1ProblemList

                Dim objMessages As New clsMessage
                Dim UserID As Long
                UserID = objMessages.GetUserID(gstrLoginName)
                objMessages.Dispose()
                objMessages = Nothing

                .SetData(e.Row, COL_USER, UserID)
                ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 130110                                
                Try
                    If e.Col = COL_ProblemSatus Then
                        '' MessageBox.Show(Trim(.GetData(e.Row, COL_COMPLAINTS).ToString()))
                        If .GetData(e.Row, COL_ProblemSatus).ToString() = "Resolved" AndAlso (.GetData(e.Row, COL_COMPLAINTS).ToString().Trim()) <> "" Then
                            .Cols(COL_RsDt).AllowEditing = True
                            If .GetData(e.Row, COL_RsDt) = Nothing Then
                                .SetData(e.Row, COL_RsDt, Now.Date.ToString)
                            End If
                        Else
                            .Cols(COL_RsDt).AllowEditing = False
                            .SetData(e.Row, COL_RsDt, Nothing)
                            If (.GetData(e.Row, COL_COMPLAINTS).ToString().Trim()) = "" Then
                                .Selection.Clear(ClearFlags.Content)
                            End If
                        End If
                    ElseIf e.Col = COL_RsDt Then
                        If .GetData(e.Row, COL_ProblemSatus).ToString() = "Resolved" Then
                            .Cols(COL_RsDt).AllowEditing = True
                        Else
                            .Cols(COL_RsDt).AllowEditing = False
                            .Selection.Clear(ClearFlags.Content)
                        End If
                    End If
                Catch
                    .Selection.Clear(ClearFlags.Content)
                End Try
                ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 130110
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub c1ProblemList_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ProblemList.CellButtonClick
        Try
            '''' BY Mahesh - 20070326 to Get Diagnosis 
            ' tls_SM

            If e.Col = COL_DIAGNOSISBUTTON Then


                If gblnICD9Driven Then
                    Dim frm As New frm_Diagnosis(_VisitID, 0, _PatientID)
                    With frm
                        Diagonsis = c1ProblemList.GetData(c1ProblemList.Row, COL_DIAGNOSIS)
                        .ShowInTaskbar = False
                        .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                        .Close() 'Change made to solve memory Leak and word crash issue
                        .Dispose()
                        c1ProblemList.SetData(c1ProblemList.Row, COL_DIAGNOSIS, Diagonsis)
                    End With
                    frm = Nothing
                Else
                    Dim oTreatment As New frm_Treatment(0, _VisitID, Now.Date, "", _PatientID)
                    oTreatment.ShowDialog(IIf(IsNothing(oTreatment.Parent), Me, oTreatment.Parent))
                    'Change made to solve memory Leak and word crash issue
                    oTreatment.Close()
                    oTreatment.Dispose()
                    oTreatment = Nothing
                End If


                With c1ProblemList
                    strDia = Fill_Diagnosis(_VisitID)
                    '''' 20070129 For Fill Diagnosis '
                    Dim csDia As CellStyle '= .Styles.Add("Dia")
                    Try
                        If (.Styles.Contains("Dia")) Then
                            csDia = .Styles("Dia")
                        Else
                            csDia = .Styles.Add("Dia")

                        End If
                    Catch ex As Exception
                        csDia = .Styles.Add("Dia")

                    End Try
                    '' Fill Values In ComboBox
                    'line added by dipak 20091021 to fix 4140#Exam -> Problem List
                    If (strDia = "") Then
                        strDia = " |"
                    End If
                    'end dipak 20091021
                    csDia.ComboList = strDia
                    ''''
                    .Cols(COL_DIAGNOSIS).Style = csDia
                End With
            ElseIf e.Col = COL_RxBUTTON Then
                'Variables Added by Mayuri:20091006
                'To check the position on Drugs Control which opens on Prescription (...)button.
                Dim RowNumber As Int16
                Dim ColNumber As Int16
                RowNumber = e.Row
                ColNumber = e.Col
                LoadUserGrid(RowNumber, ColNumber)
                dgCustomGrid.Label1.Visible = False
                dgCustomGrid.txtsearch.Visible = False
                dgCustomGrid.Panel2.Visible = False
                pnlcustomTask.Visible = True

                pnlcustomTask.BringToFront()
                _Temprow = e.Row

                Dim rg As C1.Win.C1FlexGrid.CellRange = c1ProblemList.GetCellRange(_Temprow, COL_PRESCRIPTION, _Temprow, COL_PRESCRIPTION)
                Dim RxStyle As CellStyle = rg.Style()

                '.HistoryCategory = Trim(c1ProblemList.GetData(i, COL_PRESCRIPTION))
                If Not IsNothing(RxStyle) Then
                    If RxStyle.ComboList <> "" Then
                        _TempRx = RxStyle.ComboList

                    Else
                        _TempRx = String.Empty
                    End If
                Else
                    _TempRx = String.Empty
                End If

                SetDrugValues()
            ElseIf e.Col = COL_EXAMNAMEBUTTON Then
                Dim objclsProblist As New clsPatientProblemList
                'Memory Leak
                Dim dtExam As DataTable = Nothing

                If c1ProblemList.GetData(c1ProblemList.Row, COL_DIAGNOSIS) <> "" Then
                    Dim strDiagnosis As String = c1ProblemList.GetData(c1ProblemList.Row, COL_DIAGNOSIS).ToString()
                    Dim dtDOS As DateTime = c1ProblemList.GetData(c1ProblemList.Row, COL_DOS).ToString()
                    Dim sep As Char() = {"-"}
                    Dim ICD9 As String() = strDiagnosis.Split(sep, 2)
                    Dim ICD9Code As String = ""
                    Dim ICD9Desc As String = ""
                    If ICD9.Length = 1 Then
                        ICD9Code = ICD9(0).Trim
                    ElseIf ICD9.Length = 2 Then
                        ICD9Code = ICD9(0).Trim
                        ICD9Desc = ICD9(1).Trim
                    End If
                    dtExam = objclsProblist.GetExamDetails(_VisitID, ICD9Code, ICD9Desc, dtDOS, _PatientID)
                    If Not IsNothing(dtExam) Then
                        If dtExam.Rows.Count > 0 Then
                            ShowPastExam(CType(dtExam.Rows(0)(0), Int64), _PatientID, CType(dtExam.Rows(0)(1), Int64), CType(dtExam.Rows(0)(3), DateTime).Date, dtExam.Rows(0)(2).ToString, CType(dtExam.Rows(0)(4), Boolean), dtExam.Rows(0)(6).ToString)

                        Else
                            MessageBox.Show("No exam is associated for this diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        MessageBox.Show("No exam is associated for this diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    MessageBox.Show("No diagnosis and Exam is associated for this Problem.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                'Change made to solve memory Leak and word crash issue
                objclsProblist.Dispose()
                objclsProblist = Nothing

                If Not IsNothing(dtExam) Then
                    dtExam.Dispose()
                    dtExam = Nothing
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Check Snomed setting for problem list from admin
    Private Sub CheckSnomedSetting_Infobutton()
        Dim _dttable As DataTable = Nothing
        Dim oclsProblemListV2 As clsPatientProblemList = New clsPatientProblemList

        Try
            _ISSmonedCodeMandatory = oclsProblemListV2.IsSnomedMandatory()

        Catch ex As Exception
        Finally

            If oclsProblemListV2 IsNot Nothing Then
                oclsProblemListV2.Dispose()
                oclsProblemListV2 = Nothing
            End If
        End Try
    End Sub
    Private Sub c1ProblemList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1ProblemList.Click

        If c1ProblemList.Col = COL_INFOBUTTON Then
            ''Display Online Information Document
            ''Patient Education from Online Resource like NLM
            'Dim ICDCodeAndDescription As String = Convert.ToString(c1ProblemList.GetData(c1ProblemList.Row, COL_DIAGNOSIS))
            'Dim value As Int16
            'Dim sICDCode As String = ""
            'Dim sDescription As String = ""
            'If ICDCodeAndDescription <> "" Then
            '    value = ICDCodeAndDescription.IndexOf("-")
            '    sICDCode = ICDCodeAndDescription.Remove(value)
            '    sDescription = ICDCodeAndDescription.Remove(0, value + 1)
            'End If


            'Dim vId As Long = 0
            'If _VisitID = 0 Then
            '    vId = GenerateVisitID(_PatientID)
            'Else
            '    vId = _VisitID
            'End If

            'Dim SnoMedctCode As String = Convert.ToString(c1ProblemList.GetData(c1ProblemList.Row, COL_ConceptID))
            'If SnoMedctCode = "" And sICDCode = "" Then
            '    MessageBox.Show("Code not avilable for selected problem", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Else
            '    If _ISSmonedCodeMandatory Then
            '        If SnoMedctCode <> "" Then
            '            clsinfobutton_Problemlist.Openinfosource(SnoMedctCode, "2.16.840.1.113883.6.96", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId)
            '        Else
            '            clsinfobutton_Problemlist.Openinfosource(sICDCode, "2.16.840.1.113883.6.103", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId)
            '        End If
            '    Else
            '        If sICDCode <> "" Then
            '            clsinfobutton_Problemlist.Openinfosource(sICDCode, "2.16.840.1.113883.6.103", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId)
            '        ElseIf SnoMedctCode <> "" Then
            '            clsinfobutton_Problemlist.Openinfosource(SnoMedctCode, "2.16.840.1.113883.6.96", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId)
            '        End If
            '    End If
            'End If


            'c1ProblemList.Col = COL_SELECT
            ''ElseIf c1ProblemList.Col = COL_PROVIDEREDUCATIONBUTTON Then
            ''    'Display Provider Specific Offline Information Document
            ''    'Patient Education word form
            ''ElseIf c1ProblemList.Col = COL_PATIENTEDUCATIONBUTTON Then
            ''    'Display Patient Specific Offline Information Document
            ''    'Patient Education word form
        End If


    End Sub
    Private Sub ShowPastExam(ByVal ExamID As Long, ByVal PatientId As Int64, ByVal VisitID As Long, ByVal DOS As String, ByVal ExamName As String, ByVal TemplateName As String, ByVal blnFinished As Boolean, Optional ByVal PatientCode As String = "")
        Try

            _PatientID = PatientId

            If Trim(strPatientFirstName) <> "" Then

                If MainMenu.IsAccess(False, _PatientID) = False Then
                    Exit Sub
                End If
                '''''<><><><><> Check Patient Status <><><><><><>''''

                If Not blnFinished Then
                    Dim objExam As New clsPatientExams
                    objExam.SetProviderExam(ExamID)
                    'Memory Leak
                    If Not IsNothing(objExam) Then
                        objExam.Dispose()
                        objExam = Nothing
                    End If
                End If

                Me.Cursor = Cursors.WaitCursor

                Dim frm As New frmPatientExam(_PatientID, _VisitID)

                With frm
                    .Hide()
                    .blnModify = True
                    .Text = "Past Exams"
                    Dim sender As Object = Nothing
                    Dim e As System.EventArgs = Nothing
                    .cmdPastExam_Click(sender, e)
                    .PatientID = _PatientID
                    ' .chkShowPreview.Visible = True
                    .pnlPastExam.Visible = True
                    If .OpenPastExam(ExamID, VisitID, Convert.ToDateTime(DOS), ExamName.Trim, blnFinished, TemplateName) = True Then


                        If blnOpenFromExam = False Then
                            CType(Me.MdiParent, MainMenu).pnlMainToolBar.Visible = False
                            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)

                        End If
                        '''' User Want to Open Exam
                        .MdiParent = CType(Me.MdiParent, MainMenu)
                        .IsPastExam = True
                        ''Sanjog-Added on 20101206 to show problem list from exam form
                        .blnOpenedFromProblemList = True
                        ''Sanjog-Added on 20101206 to show problem list from exam form
                        .Show()
                        If .ExamViewMode Then
                            .ViewExam(ExamID)
                        Else
                            .OpenPastExamContents(ExamID, blnFinished)
                        End If
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Exam opened", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    Else


                        frm.Dispose()
                        frm = Nothing
                    End If
                End With
                Me.Cursor = Cursors.Default
            Else
                Me.Cursor = Cursors.Default
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    '' By Mahesh 20090129
    Private Function Fill_Diagnosis(ByVal VisitID As Long) As String
        Dim objclsProblist As New clsPatientProblemList
        strDia = String.Empty
        ' strDia = " |"
        'Memory Leak
        Dim dtDia As DataTable = Nothing
        Try

            '' Fill Diagnosis Of the Patient for the Visit in strDia
            dtDia = objclsProblist.Get_ProblemListDiagnosis(VisitID, _PatientID)
            If IsNothing(dtDia) = False Then

                For i As Int32 = 0 To dtDia.Rows.Count - 1
                    If dtDia.Rows(i)("Flag") = 1 Then
                        ''                          ICD9Code  
                        If strDia = "" Then
                            ' If strDia = "|" Then
                            If (Convert.ToString(dtDia.Rows(i)("Field1") <> "")) Then
                                strDia = dtDia.Rows(i)("Field1").ToString.Trim & ""
                                'strDia &= " - " & dtDia.Rows(i)("Field1")

                                If Not IsDBNull(dtDia.Rows(i)("Field2")) AndAlso dtDia.Rows(i)("Field2") <> " " Then
                                    strDia &= "-" & dtDia.Rows(i)("Field2").ToString.Trim & ""
                                End If
                            End If
                        Else
                            strDia &= "|" & dtDia.Rows(i)("Field1").ToString.Trim & "" '& "-" & dtDia.Rows(i)("Field2")
                            If Not IsDBNull(dtDia.Rows(i)("Field2")) AndAlso dtDia.Rows(i)("Field2") <> " " Then

                                strDia &= "-" & dtDia.Rows(i)("Field2").ToString.Trim & ""
                            End If
                        End If
                        'End If
                    End If

                Next

            End If
        Catch ex As Exception

        Finally
            objclsProblist.Dispose()
            objclsProblist = Nothing
            If Not IsNothing(dtDia) Then
                dtDia.Dispose()
                dtDia = Nothing
            End If
        End Try
        Return strDia & ""
    End Function


    Private Sub frmProblemList_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        'Try
        '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Close, "Patient Problem List Closed", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        'Catch ex As Exception
        'End Try
    End Sub

    Public Sub FillGrid(ByVal _strSQL As String)
        Try

            Dim cmbboxSatusInt As Integer = 0

            'set index for combobox
            If cmbStatus.SelectedItem = "Active" Then
                cmbboxSatusInt = 3
            ElseIf cmbStatus.SelectedItem = "Resolved" Then
                cmbboxSatusInt = 2
            ElseIf cmbStatus.SelectedItem = "Inactive" Then
                cmbboxSatusInt = 4
            ElseIf cmbStatus.SelectedItem = "Chronic" Then
                cmbboxSatusInt = 5
            ElseIf cmbStatus.SelectedItem = "All" Then
                cmbboxSatusInt = 1
            End If


            '' Id Problem List is For update
            If _ProblemID > 0 Or _VisitID > 0 Then

                Dim myDT As DataTable
                oDB = New gloStream.gloDataBase.gloDataBase
                oDB.Connect(GetConnectionString)
                myDT = oDB.ReadQueryDataTable(_strSQL)
                oDB.Disconnect()
                oDB.Dispose()
                myDT.Dispose()
                myDT = Nothing
                oDB = Nothing
                '' Table dt Contains following Columns
                '' ProblemID, VisitID , dtDOS, Diagnosis, Complaint ,Status                
                If IsNothing(myDT) = False Then
                    Call SetGridStyle(myDT)
                End If
            Else
                _VisitID = GenerateVisitID(Date.Now, _PatientID)
                Call Fill_Status()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        Try
            Call Filter_PrblemList()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Filter_PrblemList()
        Dim strSelcetQry As String
        '' For all show 
        If cmbStatus.SelectedIndex = 0 Then
            strSelcetQry = "SELECT nProblemID as ProblemID, nVisitID AS VisitID ,dtDOS AS dtDOS,dtDischargeDate AS dtDischargeDate, sICD9Code + '-' + sICD9Desc AS Diagnosis, " _
           & " sCheifComplaint AS Complaint ,nProblemStatus AS Status," _
           & " (case when dtResolvedDate='01/01/1900' then space(10) else convert(varchar,dtResolvedDate,101) end)" _
           & " as ResolvedDt, isnull(sResolvedComment,'') as Comment," _
           & " IsNull(sPrescription,'') as Prescription, ISNULL(nUserID,0) AS nUserID " _
           & " FROM ProblemList WHERE  ProblemList.nPatientID  = " & _PatientID & " ORDER BY dtDOS DESC"
            ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 130110            
            Call FillGrid(strSelcetQry)
        Else
            strSelcetQry = "SELECT nProblemID as ProblemID, nVisitID AS VisitID ,dtDOS AS dtDOS,dtDischargeDate AS dtDischargeDate, sICD9Code + '-' + sICD9Desc AS Diagnosis, " _
           & " sCheifComplaint AS Complaint ,nProblemStatus AS Status," _
           & " (case when dtResolvedDate='01/01/1900' then space(10) else convert(varchar,dtResolvedDate,101) end)" _
           & " as ResolvedDt, isnull(sResolvedComment,'') as Comment," _
           & " IsNull(sPrescription,'') as Prescription, ISNULL(nUserID,0) AS nUserID " _
           & " FROM ProblemList WHERE ProblemList.nPatientID  = " & _PatientID & " and ProblemList.dtDOS >= '" & datetimeFrom.Text & "' and ProblemList.dtDOS <= '" & datetimeTo.Text & "' and ProblemList.nProblemStatus = " & cmbStatus.SelectedIndex & "  ORDER BY dtDOS DESC"
            ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 130110            
            Call FillGrid(strSelcetQry)
        End If
    End Sub


    Private Sub datetimeFrom_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles datetimeFrom.ValueChanged
        Try
            Call Filter_PrblemList()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub datetimeTo_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles datetimeTo.ValueChanged
        Try
            Call Filter_PrblemList()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblPraoblemList_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblPraoblemList.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    ''''''''''''' Added by Ujwala Atre  - Resolved Date Validation 
                    _isSaveClicked = True
                    If ValidateProblemList() = False Then
                        '' chetan added for  having no resolved date record  on  22-nov-2010
                        ResolvedStatus()
                        btn_Inactive.Focus()
                        btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_Remove.BackgroundImageLayout = ImageLayout.Center

                        btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                        btn_Inactive.BackgroundImageLayout = ImageLayout.Center

                        btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_Both.BackgroundImageLayout = ImageLayout.Center

                        btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center

                        btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_Inactive2.BackgroundImageLayout = ImageLayout.Center
                        '' chetan added for  having no resolved date record  on  22-nov-2010
                        Exit Sub
                    End If
                    ''''''''''''' Added by Ujwala Atre  - Resolved Date Validation 
                    '' If Arrlist.Count > 0 Then
                    If Not dtRecProblist Is Nothing Then
                        If dtRecProblist.Rows.Count > 0 Then
                            If Not getProviderTaxID(gnPatientProviderID) Then
                                Exit Sub
                            End If
                        End If
                    End If
                    ''End If

                    Call SaveProblemList()
                    ''If Arrlist.Count > 0 Then
                    If Not dtRecProblist Is Nothing Then
                        If dtRecProblist.Rows.Count > 0 Then
                            Dim MedicalReconcilationId As Long = GetMedicalReconcillationID(_VisitID)
                            If MedicalReconcilationId > 0 Then
                                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(gnPatientProviderID)
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MedicalReconcilationId, sProviderTaxID, gnPatientProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ManualReconciliationProblemList.GetHashCode())
                                oclsselectProviderTaxID = Nothing
                            End If

                        End If
                    End If
                    '' End If
                    'Add Close Audit here instaed of formclosing as form closing get triggered twice.Bug ID Bug #118093 
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Close, "Patient Problem List Closed", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)

                Case "Close"
                    ' _isSavedClicked = False
                    If blnRxMedFromExam = True Then
                        Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    End If
                    ''Bug : 00000828: Record locking
                    If _blnRecordLock Then
                        _isChanges = False
                    End If

                    'Add Close Audit here instaed of formclosing as form closing get triggered twice.Bug ID Bug #118093 
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Close, "Patient Problem List Closed", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    '  Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                Case "HealthPlan"
                    FindHealthPlan()
                    'Code Start - Added by kanchan on 20100601 for CCD
                Case "Generate CCD"
                    Dim objfrm As New frmCCDGenerateList(_PatientID)
                    objfrm.chkProblems.Checked = True
                    With objfrm
                        .WindowState = FormWindowState.Normal
                        .BringToFront()
                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog(IIf(IsNothing(objfrm.Parent), Me, objfrm.Parent))
                        .Close() 'Change made to solve memory Leak and word crash issue
                    End With
                    objfrm.Dispose()
                    objfrm = Nothing
                    'Code End - Added by kanchan on 20100601 for CCD
                Case "Reconcile"
                    ShowReconciliation()
                Case "Recommendation"
                    ShowRecommendation()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Problem List", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowRecommendation()
        Try

            If _PatientID <= 0 Then
                Exit Sub
            End If

            Try
                Dim _toSendPatLst As New Collection
                _toSendPatLst.Add(_PatientID)
                Dim frmTemplate As New frmDM_DisplayRecommendations(_toSendPatLst, True, _VisitID)

                With frmTemplate
                    .ShowInTaskbar = False
                    .ShowDialog(IIf(IsNothing(frmTemplate.Parent), Me, frmTemplate.Parent))
                End With

                'Memory Leak
                If Not IsNothing(frmTemplate) Then
                    frmTemplate.Dispose()
                    frmTemplate = Nothing
                End If
                _toSendPatLst.Clear()
                _toSendPatLst = Nothing
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK)
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowReconciliation()
        Dim ogloCCDReconcile As New gloCCDLibrary.gloCCDReconcilation
        Dim objProblemList As clsPatientProblemList = New clsPatientProblemList
        Dim frmReconcilation As frmReconcileList = Nothing
        Dim _issaved As Boolean = True
        Dim Result As Integer

        Try
            ''''''
            _issaved = checkProblemListSaved()
            If _issaved = False Or _isChanges = True Then
                Result = MessageBox.Show("Reconcile List cannot accessed without saving the Problem List. Do you want to save the Problem List?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If Result = MsgBoxResult.Yes Then
                    _isChanges = False
                    If ValidateProblemList() = False Then
                        ResolvedStatus()
                        btn_Inactive.Focus()
                        btn_Inactive_MouseClick(Nothing, Nothing)
                        Exit Sub
                    End If

                    Call SaveProblemList(True)

                    Dim dtProblems As DataTable
                    Dim oclsProblemList As clsPatientProblemList
                    oclsProblemList = New clsPatientProblemList
                    dtProblems = oclsProblemList.Get_PatientProblemList(_PatientID)
                    If Not IsNothing(oclsProblemList) Then
                        oclsProblemList.Dispose()
                        oclsProblemList = Nothing
                    End If
                    If IsNothing(dtProblems) = False Then
                        Call SetGridStyle(dtProblems)
                        btn_ActiveProblem_MouseClick(Nothing, Nothing)
                        dtProblems.Dispose()
                        dtProblems = Nothing
                    End If
                    btn_ActiveProblem.Focus()
                    btn_ActiveProblem_Click(Nothing, Nothing)
                    btn_ActiveProblem_MouseClick(Nothing, Nothing)

                ElseIf Result = MsgBoxResult.No Then
                    Exit Sub

                End If
            End If
            ''''''
            frmReconcilation = New frmReconcileList(_PatientID, "Problem")
            frmReconcilation.LoginUser = gstrLoginName
            frmReconcilation.LoginID = gnLoginID
            ' If frmReconcilation.ShowDialog(Me) = DialogResult.OK Then
            frmReconcilation.ShowDialog(IIf(IsNothing(frmReconcilation.Parent), Me, frmReconcilation.Parent))

            dtProblems = objProblemList.Get_PatientProblemList(_PatientID)
            If IsNothing(objProblemList) = False Then
                objProblemList.Dispose()
                objProblemList = Nothing
            End If


            If IsNothing(dtProblems) = False Then
                Call SetGridStyle(dtProblems)

                dtProblems.Dispose()
                dtProblems = Nothing
            End If
            btn_ActiveProblem.Focus()
            btn_ActiveProblem_Click(Nothing, Nothing)
            btn_ActiveProblem_MouseClick(Nothing, Nothing)
            ''
            ' End If
            If IsNothing(Me.ParentForm) = False Then
                CType(Me.ParentForm, MainMenu).ShowReconciliationAlert()
            End If

            Dim _isReadyLists As Boolean = False

            _isReadyLists = ogloCCDReconcile.IsReadyListsPresent(_PatientID, "Problem")
            If _isReadyLists = True Then
                tlb_Reconcile.Enabled = True
            Else
                tlb_Reconcile.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Private Sub FindHealthPlan()
        Try

            If _PatientID <= 0 Then
                Exit Sub
            End If

            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If
            'end modification 
            Try
                Dim _toSendPatLst As New Collection
                '_toSendPatLst.Add(gnPatientID)
                _toSendPatLst.Add(_PatientID)
                Dim frmTemplate As New frmDM_PatientSpecific(_toSendPatLst, True, _VisitID)
                With frmTemplate
                    .ShowInTaskbar = False
                    .WindowState = FormWindowState.Normal
                    .ShowDialog(IIf(IsNothing(frmTemplate.Parent), Me, frmTemplate.Parent))
                    .Close() 'Change made to solve memory Leak and word crash issue
                End With
                frmTemplate.Dispose()
                frmTemplate = Nothing
                _toSendPatLst.Clear()
                _toSendPatLst = Nothing
                DisplayHealthPlanAlerts()
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            'pnlWordObj.Controls.Remove(dgCustomGrid)
            pnlcustomTask.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Visible = False
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
        End If
    End Sub
    Private Sub RemoveControlExam()
        If Not IsNothing(dgCustomGridselectExam) Then
            pnlSelectExam.Visible = False
            pnlSelectExam.Controls.Remove(dgCustomGridselectExam)
            dgCustomGridselectExam.Visible = False
            dgCustomGridselectExam.Dispose()
            dgCustomGridselectExam = Nothing
        End If
    End Sub

    'Add customGrid control to form 
    Private Sub AddControl(ByVal RowNumber As Int16, ByVal ColNumber As Int16)

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomTask

        pnlcustomTask.Controls.Add(dgCustomGrid)
        pnlcustomTask.BringToFront()
        ' For RowNumber = 1 To c1ProblemList.Rows.Count
        'Code Added by Mayuri:20091006
        'To change the position on Drugs Control which opens on Prescription (...) button. Bug ID:#4140
        Dim y As Int64
        y = 85 + (RowNumber * 17) - 15
        Dim x As Int64
        x = 500
        pnlcustomTask.Location = New Point(x, y)

        If RowNumber < 7 Then
            pnlcustomTask.Location = New Point(x, y)
        ElseIf RowNumber = 7 Then
            y = y - pnlcustomTask.Height + 35
            pnlcustomTask.Location = New Point(x, y)
        Else

            y = y - pnlcustomTask.Height + 20
            pnlcustomTask.Location = New Point(x, y)

        End If
        'End Code Added by Mayuri:20091006


        pnlcustomTask.Visible = True
        dgCustomGrid.Visible = True
        pnlcustomTask.BringToFront()
        dgCustomGrid.BringToFront()

    End Sub

    Public Sub CustomDrugsGridStyle()

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_Count
            .AllowEditing = True
            .Redraw = False
            .SetData(0, Col_Check, "Select")
            .Cols(Col_Check).Width = _TotalWidth * 0.1
            .Cols(Col_Check).AllowEditing = True
            .Cols(Col_Check).DataType = System.Type.GetType("System.Boolean")

            .SetData(0, Col_DrugName, "Drug Name")
            .Cols(Col_DrugName).Width = _TotalWidth * 0.45

            .SetData(0, Col_Dosage, "Dosage")
            .Cols(Col_Dosage).Width = _TotalWidth * 0.45
            .Cols(Col_Dosage).AllowEditing = False
            .Redraw = True

        End With

    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        Try

            'User Selelcted 
            strRx = String.Empty

            For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    If strRx = "" Then
                        strRx = dgCustomGrid.GetItem(i, 1).ToString
                        dgCustomGrid.C1Task.SetCellCheck(i, 0, CheckEnum.Checked)
                        If Not IsDBNull(dgCustomGrid.GetItem(i, 2)) AndAlso dgCustomGrid.GetItem(i, 2).ToString.Trim <> "" Then
                            'Replaced character " - " by  " ~ " 
                            strRx &= " ~ " & dgCustomGrid.GetItem(i, 2).ToString
                        End If
                    Else
                        strRx &= "|" & dgCustomGrid.GetItem(i, 1).ToString '& "-" & dtDia.Rows(i)("Field2")
                        If Not IsDBNull(dgCustomGrid.GetItem(i, 2)) AndAlso dgCustomGrid.GetItem(i, 2).ToString.Trim <> "" Then
                            strRx &= " ~ " & dgCustomGrid.GetItem(i, 2).ToString
                        End If
                    End If

                End If



            Next



            With c1ProblemList

                Dim csRx As CellStyle '= .Styles.Add("Rx" & _Temprow)
                Try
                    If (.Styles.Contains("Rx" & _Temprow)) Then
                        csRx = .Styles("Rx" & _Temprow)
                    Else
                        csRx = .Styles.Add("Rx" & _Temprow)

                    End If
                Catch ex As Exception
                    csRx = .Styles.Add("Rx" & _Temprow)

                End Try
                '' Fill Values In ComboBox
                'line added by dipak 20091021 to fix 4140#Exam -> Problem List
                If (strRx = "") Then
                    strRx = " |"
                End If
                'end dipak 20091021
                csRx.ComboList = strRx
                ''''
                Dim rg As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Temprow, COL_PRESCRIPTION, _Temprow, COL_PRESCRIPTION)
                rg.Style = csRx
                _Temprow = 0

            End With
            pnlcustomTask.Visible = False


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dgCustomGrid.Visible = False
        End Try
    End Sub

    Private Sub LoadUserGrid(ByVal RowNumber As Int16, ByVal ColNumber As Int16)
        Try
            AddControl(RowNumber, ColNumber)
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                dgCustomGrid.Width = pnlcustomTask.Width
                dgCustomGrid.Height = pnlcustomTask.Height
                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False
                BindUserGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BindUserGrid()
        Try
            Dim dt As DataTable
            Dim objclsProblist As New clsPatientProblemList
            '' Fill Diagnosis&Rx Of the Patient for the Visit  in strDia
            dt = objclsProblist.Get_ProblemListRx(_PatientID)
            objclsProblist.Dispose()
            objclsProblist = Nothing
            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)
            col.Dispose()
            col = Nothing
            If Not IsNothing(dt) Then
                '' For DataBinding Users
                ' If dt.Rows.Count > 0 Then ''commented by Sandip Darade 20090527 to fix the issue regarding prescriptions  
                dt.Columns("DrugName").Caption = "Drug Name"
                dgCustomGrid.datasource(dt.DefaultView)
                ''End If

                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End If


            ''Sandip Darade 20090410
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.1
            dgCustomGrid.C1Task.Cols(1).AllowEditing = False
            dgCustomGrid.C1Task.Cols(1).Width = _TotalWidth * 0.45
            'dgCustomGrid.C1Task.Cols(2).AllowEditing = True
            dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0.45
            '  UserCount = dt.Rows.Count
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Problem list", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Problem list", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SetDrugValues()
        If _TempRx <> "" Then
            Dim _Drugs As String() = Split(_TempRx, "|")
            For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                For _index As Int32 = 0 To _Drugs.Length - 1
                    Dim _drugname As String() = Split(_Drugs(_index), "~")
                    If dgCustomGrid.GetItem(i, 1).ToString.Trim = _drugname(0).Trim Then
                        dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    End If
                Next
            Next
        End If

    End Sub
    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        dgCustomGrid.Visible = False
        pnlcustomTask.Visible = False
    End Sub
    Private Sub DisplayHealthPlanAlerts()
        Dim strDMAlert As String = ""
        Dim _showDMAlert As Boolean = False
        Dim objSettings As New clsSettings
        Dim dt As DataTable
        dt = objSettings.GetSetting("ShowDMAlert")
        objSettings.Dispose()
        objSettings = Nothing
        If IsNothing(dt) = False Then
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("sSettingsValue") <> "" Then
                    _showDMAlert = dt.Rows(0)("sSettingsValue")
                Else
                    _showDMAlert = False
                End If
            End If
        End If
        dt.Dispose()
        dt = Nothing
        If _showDMAlert = True And gbShowDMAlert Then

            Dim oDM As New gloEMR.gloStream.DiseaseManagement.DiseaseManagement
            strDMAlert = oDM.GetDMAlerts(_PatientID)

            'Memory Leak
            If Not IsNothing(oDM) Then
                oDM.Dispose()
                oDM = Nothing
            End If

        End If
        If strDMAlert <> "" Then
            lblAlert.Text = strDMAlert
            pnlAlert.Visible = True
        Else
            pnlAlert.Visible = False
        End If
        Application.DoEvents()

    End Sub
    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Dim dvList As DataView
            dvList = dtProblems.DefaultView ' CType(dt, DataView)

            If IsNothing(dvList) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If


            Dim strPatientSearchDetails As String
            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            dvList.RowFilter = dvList.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"

            Dim dtAftersort As New DataTable
            dtAftersort = dvList.ToTable
            dvList.Dispose()
            dvList = Nothing
            SetGridStyle(dtAftersort)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Problem list", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
    'Resolving bug no.71129:: Patient Exam - Application associates ICD9 code and ICD10 code to single exam from problem list.
    Dim nProblemICDRevForExam As Int16 = 9
    '
    Private Sub c1ProblemList_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1ProblemList.MouseDown
        'oclsProblemList = New clsPatientProblemList
        Dim strExamID As String
        Dim strIcd9Code As String
        Dim strSnomedctCode As String
        Dim strExam() As String
        'Dim nICDRevision As Int16 = 9  ''added for ICD10 implementation
        Dim nExamID As Long

        Dim dtExamDetails As DataTable = Nothing
        Dim dtEduTemplates As DataTable = Nothing
        Dim oclsProblemList As clsPatientProblemList
        oclsProblemList = New clsPatientProblemList
        Dim _issaved As Boolean = True
        Dim Result As Integer
        Try

            mnuMedlineInfobutton.Visible = False
            mnuProviderReference.DropDownItems.Clear()
            mnuPatientEducation.DropDownItems.Clear()

            _issaved = checkProblemListSaved()
            If _issaved = False Or _isChanges = True Then
                If e.Button = MouseButtons.Right Then
                    Result = MessageBox.Show("Patient Exam cannot accessed without saving the Problem List. Do you want to save the Problem List?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                ElseIf e.Button = MouseButtons.Left Then
                    Dim c As Integer = c1ProblemList.HitTest(e.X, e.Y).Column
                    If c = COL_INFOBUTTON Then
                        Result = MessageBox.Show("Patient Education cannot accessed without saving the Problem List. Do you want to save the Problem List?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    End If
                End If

                If Result = MsgBoxResult.Yes Then
                    _isChanges = False
                    If ValidateProblemList() = False Then
                        ResolvedStatus()
                        btn_Inactive.Focus()
                        btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_Remove.BackgroundImageLayout = ImageLayout.Center

                        btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                        btn_Inactive.BackgroundImageLayout = ImageLayout.Center

                        btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_Both.BackgroundImageLayout = ImageLayout.Center

                        btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center

                        btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_Inactive2.BackgroundImageLayout = ImageLayout.Center

                        Exit Sub
                    End If

                    Call SaveProblemList(True)

                    Dim dtProblems As DataTable

                    dtProblems = oclsProblemList.Get_PatientProblemList(_PatientID)

                    If IsNothing(dtProblems) = False Then
                        Call SetGridStyle(dtProblems)
                        btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_Remove.BackgroundImageLayout = ImageLayout.Center

                        btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_Inactive.BackgroundImageLayout = ImageLayout.Center

                        btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_Both.BackgroundImageLayout = ImageLayout.Center

                        btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_Inactive2.BackgroundImageLayout = ImageLayout.Center

                        btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                        btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center
                        dtProblems.Dispose()
                        dtProblems = Nothing
                    End If
                    ''''added for bugid 92441 setting focus to current tab
                    If (_strbtnClicked.Trim() <> "") Then
                        Select Case _strbtnClicked
                            Case "InActive"
                                btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                                btn_Inactive2.BackgroundImageLayout = ImageLayout.Center
                                btn_Inactive2_Click(sender, e)
                            Case "Active"
                                btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                                btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center
                                btn_ActiveProblem_Click(sender, e)
                            Case "Resolved"
                                btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                                btn_Inactive.BackgroundImageLayout = ImageLayout.Center

                                btn_Inactive_Click(sender, e)
                            Case "All"
                                btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                                btn_Both.BackgroundImageLayout = ImageLayout.Center
                                btn_Both_Click(sender, e)
                        End Select
                    Else
                        btn_ActiveProblem_Click(sender, e)
                        btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                        btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center
                    End If
                    Exit Sub
                ElseIf Result = MsgBoxResult.No Then
                    Exit Sub
                    'ElseIf Result = MsgBoxResult.Cancel Then
                    '    Exit Sub
                End If
            End If


            If e.Button = MouseButtons.Right Then
                mnuNewExam.Visible = True
                mnuPastExam.Visible = True
                mnuSendtoExam.Visible = True
                '  pnlSelectExam.Location = New Point(e.Location.X, e.Location.Y)
                pnlSelectExam.Top = (Me.ClientSize.Height / 2) - (pnlSelectExam.Height / 2)
                pnlSelectExam.Left = (Me.ClientSize.Width / 2) - (pnlSelectExam.Width / 2)
                RemoveControlExam()

                If tlb_OpenExam.Visible = False Or tlb_RxMed.Visible = False Then ''resolve bug 10324, instead of checking flags checked the visibility of the buttons
                    mnuNewExam.Visible = False
                    mnuPastExam.Visible = False
                    'Exit Sub
                End If
                mnuNewExam.DropDownItems.Clear()
                mnuPastExam.DropDownItems.Clear()
                'mnuNewExam.Visible = True
                'mnuPastExam.Visible = True
                Dim ProblemStatus As String
                With c1ProblemList

                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    If r > 0 Then
                        .Select(r, True)
                        'Try
                        '    If (IsNothing(.ContextMenuStrip) = False) Then
                        '        .ContextMenuStrip.Dispose()
                        '        .ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        .ContextMenuStrip = cntAssociateExams
                        'nProblemID = 
                        strExamID = c1ProblemList.GetData(c1ProblemList.RowSel, COL_ExamID)
                        ProblemStatus = c1ProblemList.GetData(c1ProblemList.RowSel, COL_ProblemSatus)
                        'Resolving bug no.71129:: Patient Exam - Application associates ICD9 code and ICD10 code to single exam from problem list.
                        nICDRevision = c1ProblemList.GetData(c1ProblemList.RowSel, Col_ICDRevision)
                        nProblemICDRevForExam = nICDRevision
                        '
                        If ProblemStatus = "Active" Then
                            mnuSendtoExam.Visible = True
                        Else
                            mnuSendtoExam.Visible = False
                        End If

                        If tlb_OpenExam.Visible = True Or tlb_RxMed.Visible = True Then ''resolve bug 10324, instead of checking flags checked the visibility of the buttons
                            If Not IsNothing(strExamID) Then
                                If strExamID <> "" Then
                                    strExam = Split(strExamID, ",")
                                    For i As Integer = 0 To strExam.Length - 1
                                        nExamID = CType(strExam.GetValue(i), Long)
                                        dtExamDetails = oclsProblemList.GetExamName(nExamID)
                                        If Not IsNothing(dtExamDetails) Then
                                            If dtExamDetails.Rows.Count > 0 Then
                                                oMenu = New ToolStripMenuItem
                                                oMenu.Text = dtExamDetails.Rows(0)(2) & " - " & dtExamDetails.Rows(0)(4)
                                                oMenu.Tag = dtExamDetails.Rows(0)(0)
                                                AddHandler oMenu.Click, AddressOf OpenPastExam
                                                mnuPastExam.DropDownItems.Add(oMenu)
                                            Else
                                                mnuPastExam.Visible = False
                                            End If
                                        End If
                                        dtExamDetails.Dispose()
                                        dtExamDetails = Nothing
                                    Next
                                    FillNewExam()
                                Else
                                    mnuPastExam.Visible = False
                                    FillNewExam()
                                End If
                            Else
                                FillNewExam()
                            End If
                        End If

                        mnuMedlineInfobutton.Visible = False
                        mnuProviderReference.Visible = False
                        mnuPatientEducation.Visible = False
                    Else
                        'Try
                        '    If (IsNothing(.ContextMenuStrip) = False) Then
                        '        .ContextMenuStrip.Dispose()
                        '        .ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        .ContextMenuStrip = Nothing
                    End If
                End With
            ElseIf e.Button = MouseButtons.Left Then
                With c1ProblemList
                    Dim c As Integer = .HitTest(e.X, e.Y).Column
                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    If c = COL_INFOBUTTON Then
                        If r > 0 Then
                            strIcd9Code = Convert.ToString(c1ProblemList.GetData(c1ProblemList.RowSel, COL_DIAGNOSIS))
                            If strIcd9Code <> "" Then
                                Dim s() As String = strIcd9Code.Split("-")
                                If s.Length > 1 Then
                                    strIcd9Code = s(0)
                                End If
                            End If
                            strSnomedctCode = Convert.ToString(c1ProblemList.GetData(c1ProblemList.RowSel, COL_ConceptID))
                            nICDRevision = Convert.ToInt16(c1ProblemList.GetData(c1ProblemList.RowSel, Col_ICDRevision))  ''added for ICD10 implementation
                            'Dim Age() As String = strPatientAge.Split(" ")
                            'Dim AgeinYears As Integer
                            'If Age.Length > 1 Then
                            '    AgeinYears = Convert.ToInt32(Age(0))
                            'End If

                            Dim ageDetail As gloUserControlLibrary.AgeDetail = gloUC_PatientStrip1.PatientAge
                            Dim AgeinYears As Decimal = ageDetail.Years

                            If ageDetail.Months > 0 Then
                                AgeinYears = AgeinYears + (ageDetail.Months / 10)
                            ElseIf ageDetail.Days > 1 Then
                                AgeinYears = AgeinYears + 0.1
                            End If

                            Dim dtEduSnomed As DataTable = Nothing
                            Dim dtEduIcd9 As DataTable = Nothing
                            If strSnomedctCode <> "" Then
                                dtEduSnomed = clsinfobutton_Problemlist.GetEducationMaterial(strSnomedctCode, "2.16.840.1.113883.6.96", AgeinYears, strPatientGender)
                            Else
                                dtEduSnomed = Nothing
                            End If
                            If strIcd9Code <> "" Then
                                'dtEduIcd9 = clsinfobutton_Problemlist.GetEducationMaterial(strIcd9Code, "2.16.840.1.113883.6.103", AgeinYears, strPatientGender, nICDRevision)
                                If nICDRevision = 9 Then
                                    dtEduIcd9 = clsinfobutton_Problemlist.GetEducationMaterial(strIcd9Code, "2.16.840.1.113883.6.103", AgeinYears, strPatientGender, nICDRevision)  ''added for ICD10 implementation
                                ElseIf nICDRevision = 10 Then
                                    dtEduIcd9 = clsinfobutton_Problemlist.GetEducationMaterial(strIcd9Code, "2.16.840.1.113883.6.90", AgeinYears, strPatientGender, nICDRevision)   ''added for ICD10 implementation
                                End If
                            Else
                                dtEduIcd9 = Nothing
                            End If

                            If _ISSmonedCodeMandatory Then
                                If Not IsNothing(dtEduSnomed) Then
                                    If dtEduSnomed.Rows.Count > 0 Then
                                        dtEduTemplates = dtEduSnomed
                                    Else
                                        'If No any template is mapped with selected snomed code,then get education material by ICD9 code
                                        dtEduTemplates = dtEduIcd9
                                    End If
                                Else
                                    'If Problem Do not have snomed code,then get education material by ICD9 code
                                    dtEduTemplates = dtEduIcd9
                                End If
                            Else
                                If Not IsNothing(dtEduIcd9) Then
                                    If dtEduIcd9.Rows.Count > 0 Then
                                        dtEduTemplates = dtEduIcd9
                                    Else
                                        'If No any template is mapped with selected ICD9 code ,then get education material by snomed code
                                        dtEduTemplates = dtEduSnomed
                                    End If
                                Else
                                    'If Problem Do not have ICD9 code ,then get education material by snomed code
                                    dtEduTemplates = dtEduSnomed
                                End If
                            End If

                            If Not IsNothing(dtEduTemplates) Then
                                If dtEduTemplates.Rows.Count > 0 Then
                                    For i As Integer = 0 To dtEduTemplates.Rows.Count - 1
                                        oMenu = New ToolStripMenuItem
                                        oMenu.Text = Convert.ToString(dtEduTemplates.Rows(i)("sTemplateName"))
                                        AddHandler oMenu.Click, AddressOf OpenInfodocument
                                        If Convert.ToInt32(dtEduTemplates.Rows(i)("nResourceType")) = 1 Then
                                            oMenu.Tag = Convert.ToString(dtEduTemplates.Rows(i)("nTemplateID")) + "-Patient Reference Material"
                                            mnuPatientEducation.DropDownItems.Add(oMenu)
                                        ElseIf Convert.ToInt32(dtEduTemplates.Rows(i)("nResourceType")) = 2 Then
                                            If Convert.ToBoolean(dtEduTemplates.Rows(i)("bIsAdvancedProviderReference")) Then
                                                If gblnAdvancedReferenceEnabled = True Then
                                                    oMenu.Tag = Convert.ToString(dtEduTemplates.Rows(i)("nTemplateID")) + "-Provider Reference Material"
                                                    mnuProviderReference.DropDownItems.Add(oMenu)
                                                End If
                                            Else
                                                oMenu.Tag = Convert.ToString(dtEduTemplates.Rows(i)("nTemplateID")) + "-Provider Reference Material"
                                                mnuProviderReference.DropDownItems.Add(oMenu)
                                            End If
                                        End If
                                    Next
                                End If
                            End If

                            mnuNewExam.Visible = True
                            mnuPastExam.Visible = True
                            mnuSendtoExam.Visible = True
                            cntAssociateExams.Visible = True
                            nProblemICDRevForExam = nICDRevision
                            'Resolving bug no.71129:: Patient Exam - Application associates ICD9 code and ICD10 code to single exam from problem list.
                            cntAssociateExams.Show(CType(sender, Control), e.Location)
                            '
                            'Try
                            '    If (IsNothing(.ContextMenuStrip) = False) Then
                            '        .ContextMenuStrip.Dispose()
                            '        .ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception
                            'End Try
                            .ContextMenuStrip = cntAssociateExams
                            .ContextMenuStrip.Visible = True
                            If gblnEducationMaterialEnabled Then
                                If mnuPatientEducation.DropDownItems.Count > 0 Or mnuProviderReference.DropDownItems.Count > 0 Then
                                    mnuMedlineInfobutton.Visible = True
                                    mnuProviderReference.Visible = True
                                    mnuPatientEducation.Visible = True
                                    mnuNewExam.Visible = False
                                    mnuPastExam.Visible = False
                                    mnuSendtoExam.Visible = False
                                    If mnuProviderReference.DropDownItems.Count <= 0 Then
                                        mnuProviderReference.Visible = False
                                    End If
                                    If mnuPatientEducation.DropDownItems.Count <= 0 Then
                                        mnuPatientEducation.Visible = False
                                    End If
                                Else
                                    mnuMedlineInfobutton.Visible = False
                                    mnuProviderReference.Visible = False
                                    mnuPatientEducation.Visible = False
                                    mnuNewExam.Visible = False
                                    mnuPastExam.Visible = False
                                    mnuSendtoExam.Visible = False
                                    cntAssociateExams.Visible = False
                                    ''Display Online Information Document
                                    'Patient Education from Online Resource like NLM
                                    Dim vId As Long = 0
                                    vId = GenerateVisitID(_PatientID)
                                    'If _VisitID = 0 Then
                                    '    vId = GenerateVisitID(_PatientID)
                                    'Else
                                    '    vId = _VisitID
                                    'End If

                                    Dim patientAgeDetail As gloUserControlLibrary.AgeDetail = gloUC_PatientStrip1.PatientAge
                                    Dim sAgeUnit As String = ""
                                    Dim sAgeValue As String = ""

                                    If patientAgeDetail.Years <> 0 Then
                                        sAgeUnit = "a"
                                        sAgeValue = patientAgeDetail.Years
                                    ElseIf patientAgeDetail.Months <> 0 Then
                                        sAgeUnit = "mo"
                                        sAgeValue = patientAgeDetail.Months
                                    ElseIf patientAgeDetail.Days <> 0 Then
                                        sAgeUnit = "d"
                                        sAgeValue = patientAgeDetail.Days
                                    End If

                                    If strSnomedctCode = "" And strIcd9Code = "" Then
                                        MessageBox.Show("Code not avilable for selected problem", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Else
                                        If _ISSmonedCodeMandatory Then
                                            If strSnomedctCode <> "" Then
                                                'clsinfobutton_Problemlist.Openinfosource(strSnomedctCode, "2.16.840.1.113883.6.96", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                                                clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strSnomedctCode, "2.16.840.1.113883.6.96", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                                            Else
                                                If nICDRevision = 9 Then   ''added for ICD10 implementation                                                    
                                                    'clsinfobutton_Problemlist.Openinfosource(strIcd9Code, "2.16.840.1.113883.6.103", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                                                    clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strIcd9Code, "2.16.840.1.113883.6.103", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                                                ElseIf nICDRevision = 10 Then ''added for ICD10 implementation                                                    
                                                    'clsinfobutton_Problemlist.Openinfosource(strIcd9Code, "2.16.840.1.113883.6.90", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                                                    clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strIcd9Code, "2.16.840.1.113883.6.90", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                                                End If
                                            End If
                                        Else
                                            If strIcd9Code <> "" Then
                                                If nICDRevision = 9 Then
                                                    ' clsinfobutton_Problemlist.Openinfosource(strIcd9Code, "2.16.840.1.113883.6.103", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                                                    clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strIcd9Code, "2.16.840.1.113883.6.103", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                                                ElseIf nICDRevision = 10 Then ''added for ICD10 implementation                                                    
                                                    ' clsinfobutton_Problemlist.Openinfosource(strIcd9Code, "2.16.840.1.113883.6.90", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                                                    clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strIcd9Code, "2.16.840.1.113883.6.90", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                                                End If
                                            ElseIf strSnomedctCode <> "" Then
                                                'clsinfobutton_Problemlist.Openinfosource(strSnomedctCode, "2.16.840.1.113883.6.96", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                                                clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strSnomedctCode, "2.16.840.1.113883.6.96", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                                            End If
                                        End If
                                    End If
                                End If

                            Else
                                cntAssociateExams.Visible = False
                                mnuMedlineInfobutton.Visible = False
                                mnuProviderReference.Visible = False
                                mnuPatientEducation.Visible = False
                            End If
                        Else
                            cntAssociateExams.Visible = False
                            'Try
                            '    If (IsNothing(.ContextMenuStrip) = False) Then
                            '        .ContextMenuStrip.Dispose()
                            '        .ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            .ContextMenuStrip = Nothing
                        End If
                    Else
                        cntAssociateExams.Visible = False
                        'Try
                        '    If (IsNothing(.ContextMenuStrip) = False) Then
                        '        .ContextMenuStrip.Dispose()
                        '        .ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        .ContextMenuStrip = Nothing
                    End If
                End With

            End If

            'Change made to solve memory Leak and word crash issue
            If Not IsNothing(oclsProblemList) Then
                oclsProblemList.Dispose()
                oclsProblemList = Nothing
            End If

            If Not IsNothing(dtEduTemplates) Then
                dtEduTemplates.Dispose()
                dtEduTemplates = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(oclsProblemList) Then
                oclsProblemList.Dispose()
                oclsProblemList = Nothing
            End If
        End Try

    End Sub

    'Public Sub OpenInfodocument(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim oCurrentMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
    '    Dim tag() As String = oCurrentMenu.Tag.ToString().Split("-")
    '    Dim TemplateName As String = oCurrentMenu.Text
    '    Dim nTempId As Int64 = CType(tag(0), Int64)
    '    Dim OpenFor As String = tag(1).ToString()
    '    Dim objWord As New clsWordDocument
    '    Dim dtPtEducation As New DataTable
    '    dtPtEducation = objWord.FillTemplates(enumTemplateFlag.PatientEducation)
    '    Dim ofrmPatientEducation As New frmPatientEducation(False, nTempId, _PatientID, True, TemplateName, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList)
    '    Try
    '        ofrmPatientEducation.Text = OpenFor + "-" + TemplateName
    '        If Not IsNothing(Me.MdiParent) Then
    '            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
    '            ofrmPatientEducation.MdiParent = Me.ParentForm
    '        End If

    '        ofrmPatientEducation.Show()
    '        If Not IsNothing(Me.MdiParent) Then
    '            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '        End If

    '        ofrmPatientEducation.WindowState = FormWindowState.Maximized

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        If Not IsNothing(Me.MdiParent) Then
    '            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
    '        End If
    '        If Not IsNothing(dtPtEducation) Then
    '            ofrmPatientEducation.Dispose()
    '            ofrmPatientEducation = Nothing
    '        End If
    '    Finally

    '    End Try
    'End Sub
    Public Sub OpenInfodocument(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim tag() As String = oCurrentMenu.Tag.ToString().Split("-")
        Dim TemplateName As String = oCurrentMenu.Text
        Dim nTempId As Int64 = CType(tag(0), Int64)
        Dim OpenFor As String = tag(1).ToString()
        Dim objWord As New clsWordDocument
        Dim dtPtEducation As New DataTable
        dtPtEducation = objWord.FillTemplates(enumTemplateFlag.PatientEducation)
        Dim ofrmPatientEducation As New frmPatientEducationPreview()
        Try
            ofrmPatientEducation.Text = OpenFor + "-" + TemplateName

            'If Not IsNothing(Me.MdiParent) Then
            '    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            '    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
            '    ofrmPatientEducation.MdiParent = Me.ParentForm
            'End If
            ofrmPatientEducation.PATID = _PatientID
            ofrmPatientEducation.TempName = TemplateName
            ofrmPatientEducation.Sourc = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
            ofrmPatientEducation.ResourcCat = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
            If tag(1) = "Provider Reference Material" Then
                ofrmPatientEducation.ResourcTyp = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.ProviderReferenceMaterial
            Else
                ofrmPatientEducation.ResourcTyp = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
            End If


            ofrmPatientEducation.TMPID = nTempId
            ofrmPatientEducation.ISGRID = False
            ofrmPatientEducation.ShowDialog()
            'If Not IsNothing(Me.MdiParent) Then
            '    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
            'End If

            ofrmPatientEducation.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not IsNothing(Me.MdiParent) Then
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            End If
            If Not IsNothing(ofrmPatientEducation) Then
                ofrmPatientEducation.Dispose()
                ofrmPatientEducation = Nothing
            End If
        Finally
            If Not IsNothing(ofrmPatientEducation) Then
                ofrmPatientEducation.Close()
            End If
            If Not IsNothing(ofrmPatientEducation) Then
                ofrmPatientEducation.Dispose()
                ofrmPatientEducation = Nothing
            End If
        End Try
    End Sub
    Private Function checkProblemListSaved() As Boolean
        Dim _IsSaved As Boolean = True
        Try
            With c1ProblemList
                '  .Select(0, 0, False)
                For i As Int16 = 1 To .Rows.Count - 1
                    _ProblemID = .GetData(i, COL_PROBLEMID)
                    If _ProblemID = 0 Then
                        _IsSaved = False
                        Exit For
                    End If
                Next
            End With
            Return _IsSaved
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally

        End Try
    End Function

    Public Sub FillNewExam()
        Try
            Dim clsPatDet As New clsPatientDetails
            Dim dtProviders As DataTable
            dtProviders = clsPatDet.Fill_Providers()
            Dim oChildItem As ToolStripMenuItem
            Dim oNextChildItem As ToolStripMenuItem
            Dim dt As DataTable
            Dim i As Integer
            For p As Integer = 0 To dtProviders.Rows.Count - 1
                oChildItem = New ToolStripMenuItem
                With oChildItem
                    .Text = Trim(dtProviders.Rows(p).Item(1))
                    .Tag = Trim(dtProviders.Rows(p).Item(0)) ' enmContextMenu.ProviderExam

                    ' .Name = Trim(dtProviders.Rows(p).Item(0))
                    '.Shortcut = Shortcut.CtrlShiftP
                    '.ShowShortcut = False
                End With

                mnuNewExam.DropDownItems.Add(oChildItem)

                dt = clsPatDet.Fill_ProviderTemplates(Trim(dtProviders.Rows(p).Item(0)))

                If dt.Rows.Count > 0 Then

                    For i = 0 To dt.Rows.Count - 1
                        oNextChildItem = New ToolStripMenuItem
                        With oNextChildItem
                            .Text = Trim(dt.Rows(i).Item(1))
                            .Tag = "New Exam" & "-" & dt.Rows(i).Item("nTemplateID")

                        End With
                        AddHandler oNextChildItem.Click, AddressOf OpenNexExam
                        oChildItem.DropDownItems.Add(oNextChildItem)

                        '02-May-13 Aniket: Cannot dispose the following item as it removes it from the DropDownItems also. Resolving Bug #50117
                        'oNextChildItem.Dispose()
                        oNextChildItem = Nothing
                    Next
                End If 'dt.Rows.Count > 0 
                If (IsNothing(dt) = False) Then
                    dt.Dispose()
                    dt = Nothing
                End If
            Next
            If IsNothing(dtProviders) = False Then
                dtProviders.Dispose()
                dtProviders = Nothing
            End If
            oChildItem = New ToolStripMenuItem
            With oChildItem
                .Text = "All"
                .Tag = 0 ' enmContextMenu.ProviderExam

            End With


            dt = clsPatDet.Fill_Templates()
            clsPatDet.Dispose()
            clsPatDet = Nothing
            If dt.Rows.Count > 0 Then

                For i = 0 To dt.Rows.Count - 1
                    oNextChildItem = New ToolStripMenuItem
                    With oNextChildItem
                        .Text = Trim(dt.Rows(i).Item(1))
                        .Tag = "New Exam" & "-" & dt.Rows(i).Item("nTemplateID")

                    End With
                    AddHandler oNextChildItem.Click, AddressOf OpenNexExam
                    oChildItem.DropDownItems.Add(oNextChildItem)
                    oNextChildItem.Dispose()
                    oNextChildItem = Nothing
                Next
            End If 'dt.Rows.Count > 0 

            mnuNewExam.DropDownItems.Add(oChildItem)
            oChildItem.Dispose()
            oChildItem = Nothing
            dt.Dispose()
            dt = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Public Sub OpenPastExam(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oCurrentMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
            Dim nExamId As Int64 = CType(oCurrentMenu.Tag, Int64)
            If ExamID = nExamId Then
                Exit Sub
            End If
            Dim dt As DataTable
            Dim oclsProblemList As clsPatientProblemList
            oclsProblemList = New clsPatientProblemList
            'oclsProblemList = New clsPatientProblemList
            dt = oclsProblemList.GetExamName(nExamId)
            oclsProblemList.Dispose()
            oclsProblemList = Nothing
            Dim VisitID As Long
            Dim DOS As String
            Dim ExamName As String
            Dim TemplateName As String
            Dim blnFinished As Boolean

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    VisitID = CType(dt.Rows(0)("nVisitID"), Long)
                    ExamName = CType(dt.Rows(0)("sExamName"), String)
                    TemplateName = CType(dt.Rows(0)("sTemplateName"), String)
                    DOS = CType(dt.Rows(0)("dtDOS"), String)
                    blnFinished = CType(dt.Rows(0)("bIsFinished"), Boolean)
                    ShowPastExam(nExamId, _PatientID, VisitID, DOS, ExamName, TemplateName, blnFinished)
                End If
            End If
            dt.Dispose()
            dt = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Public Sub OpenNexExam(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        _TemplateName = oCurrentMenu.Text

        Dim a As Array
        If (oCurrentMenu.Tag.ToString().Contains("New Exam")) Then
            a = oCurrentMenu.Tag.ToString.Split("-")
            ' _TemplateID = a(1)
            If (a.Length >= 2) Then
                _TemplateID = a(1)
            End If
            oCurrentMenu.Tag = "New Exam"
        End If
        Try
            ' UpdateLog("picnewExam click start")
            If Trim(strPatientFirstName) <> "" Then

                '******Shweta 20090828 *********'
                'To check exeception related to word
                If CheckWordForException() = False Then
                    Exit Sub
                End If
                'End Shweta

                Dim strMessage As String = ""
                Dim Result As DialogResult
                Dim objExam As New clsPatientExams

                If gnLoginProviderID <> 0 Then
                    Dim objProvider As New clsProvider
                    Dim nPatientProvider As Int64
                    Dim strPatientProviderName As String
                    nPatientProvider = objProvider.GetPatientProvider(_PatientID)
                    If nPatientProvider <> 0 Then
                        strPatientProviderName = objExam.GetProvidernameforExam(nPatientProvider)
                    Else
                        strPatientProviderName = ""

                    End If

                    strMessage = GetPatientExamProviderMismatchMessage(strPatientProviderName) ' "This patient belongs to '" & strPatientProviderName & "'. Do you want to change the Provider?"

                    If gnLoginProviderID <> nPatientProvider Then
                        Result = MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If Result = Windows.Forms.DialogResult.Yes Then
                            If objProvider.ChangePatientProvider(_PatientID, gnLoginProviderID) = True Then
                                'Call Load_PatientControl()
                                ''DB Call ShowDefaultPatientDetails()
                            End If
                        ElseIf Result = Windows.Forms.DialogResult.No Then
                        ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                            If Not IsNothing(objExam) Then
                                objExam.Dispose()
                                objExam = Nothing
                            End If
                            If Not IsNothing(objProvider) Then
                                objProvider.Dispose()
                                objProvider = Nothing
                            End If
                            Exit Sub
                        End If
                    End If
                    If Not IsNothing(objProvider) Then
                        objProvider.Dispose()
                        objProvider = Nothing
                    End If

                End If

                '''''<><><><><> Check Patient Status <><><><><><>''''
                Dim objResultExam As DataTable
                objResultExam = objExam.GetUnfinshedExams(_PatientID)
                If Not objResultExam Is Nothing Then

                    strMessage = "Unfinished Exam(s) exists for the selected patient. Do you still want to open a New Exam?" & vbNewLine & vbNewLine & vbTab & "YES - To Open New Exam " & vbNewLine & vbNewLine & vbTab & "NO  - To Open the latest Unfinished Exam"

                    Result = MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    If Result = Windows.Forms.DialogResult.Yes Then
                        _VisitID = 0 ''when we open new exam from problem list set visit id = 0, implemented for 7031 medication carry forward case. 
                        OpenExam(True)
                    ElseIf Result = Windows.Forms.DialogResult.No Then
                        Dim nPastExamID As Long
                        Dim nVisitID As Long
                        Dim dtDOS As DateTime
                        Dim strExamName As String = String.Empty
                        'Added By Shweta 20091128
                        Dim strTemplateName As String = String.Empty
                        'End 20091128

                        ''Dim em As System.Windows.Forms.MouseEventArgs
                        If Not IsDBNull(objResultExam.Rows(0)("nExamID")) Then
                            nPastExamID = CType(objResultExam.Rows(0)("nExamID"), Int64)
                        End If
                        If Not IsDBNull(objResultExam.Rows(0)("nVisitID")) Then
                            nVisitID = CType(objResultExam.Rows(0)("nVisitID"), Int64)
                        End If
                        If Not IsDBNull(objResultExam.Rows(0)("dtDOS")) Then
                            dtDOS = CType(objResultExam.Rows(0)("dtDOS"), Date)
                        End If
                        If Not IsDBNull(objResultExam.Rows(0)("sExamName")) Then
                            strExamName = objResultExam.Rows(0)("sExamName").ToString
                        End If
                        'Added By Shweta 20091128
                        If Not IsDBNull(objResultExam.Rows(0)("sTemplateName")) Then
                            strTemplateName = objResultExam.Rows(0)("sTemplateName")
                        End If
                        'Commented by Shweta 20091128
                        'ShowPastExam(nPastExamID, _PatientID, nVisitID, dtDOS, strExamName, False)
                        ShowPastExam(nPastExamID, _PatientID, nVisitID, dtDOS.ToString(), strExamName, strTemplateName, False)

                        'End 20091128
                    End If
                Else
                    _VisitID = 0 ''when we open new exam from problem list set visit id = 0, implemented for 7031 medication carry forward case.
                    OpenExam(True)
                End If

                'Memory Leak
                If Not IsNothing(objResultExam) Then
                    objResultExam.Dispose()
                    objResultExam = Nothing
                End If

                If Not IsNothing(objExam) Then
                    objExam.Dispose()
                    objExam = Nothing
                End If
            Else

            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            '''' Show Tool Bar Mahesh 20070424
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Cursor = Cursors.Default
        _TemplateName = ""
        _TemplateID = 0
    End Sub


    Private Sub OpenExam(Optional ByVal SwitchOffSavingFlags As Boolean = False)

        Try

            Windows.Forms.Cursor.Current = Cursors.WaitCursor


            Dim frm As New frmPatientExam(_PatientID, _VisitID)

            With frm
                .Hide()
                '      UpdateLog("Filling Patient details")
                .IsNewExam = True
                .TemplateName = _TemplateName
                .TemplateID = _TemplateID
                .blnModify = False
                .Text = "New Exam"
                .PatientID = _PatientID
                .OpenExam()
                .Splitter2.Visible = True
                .MdiParent = CType(Me.MdiParent, MainMenu)

                nExamId = .examid
                If nExamId <> 0 Then
                    IsOpenNewExam = True
                End If
                .Show()

                If SwitchOffSavingFlags Then
                    frm.SaveExam(0, True)
                End If

                If frm._blnOpenHistory = True Then
                    .SendToBack()
                End If
            End With



        Catch ex As Exception
        Finally
            Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub c1ProblemList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1ProblemList.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    Dim dttoday As Date = Nothing
    Private Sub tlb_New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_New.Click

        Dim frm As frmAddProblemList = Nothing
        Try
            dttoday = Nothing

            frm = New frmAddProblemList(_PatientID, _VisitID, c1ProblemList)
            'frm.htIcdSnomed.Clear()
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowInTaskbar = False
            frm.IsNewProblem = True
            ' frm._ProviderID = gloUC_PatientStrip1.ProviderID

            Dim arrIcd() As String
            Dim dtProblemsIcd As DataTable
            Dim oclsProblemList As clsPatientProblemList
            oclsProblemList = New clsPatientProblemList
            'oclsProblemList = New clsPatientProblemList
            dtProblemsIcd = oclsProblemList.Get_PatientProblemList(_PatientID)
            Try
                For icd As Integer = 0 To dtProblemsIcd.Rows.Count - 1 Step 1
                    arrIcd = Convert.ToString(dtProblemsIcd.Rows(icd)("Diagnosis")).Split("-")
                    If arrIcd.Length > 1 Then
                        If arrIcd(0) <> "" Then
                            If frmAddProblemList.htIcdSnomed.Count > 0 Then
                                If frmAddProblemList.htIcdSnomed.ContainsKey(arrIcd(0)) Then
                                    If Convert.ToString(frmAddProblemList.htIcdSnomed.Item(arrIcd(0))) <> Convert.ToString(dtProblemsIcd.Rows(icd)("sConceptId")) Then
                                        frmAddProblemList.htIcdSnomed.Item(arrIcd(0)) = Convert.ToString(dtProblemsIcd.Rows(icd)("sConceptId"))
                                    End If
                                Else
                                    frmAddProblemList.htIcdSnomed.Add(arrIcd(0), dtProblemsIcd.Rows(icd)("sConceptId"))
                                End If
                            Else
                                frmAddProblemList.htIcdSnomed.Add(arrIcd(0), dtProblemsIcd.Rows(icd)("sConceptId"))
                            End If
                        Else
                            frmAddProblemList.htIcdSnomed.Add(System.Guid.NewGuid.ToString(), dtProblemsIcd.Rows(icd)("sConceptId"))
                        End If
                    Else
                        frmAddProblemList.htIcdSnomed.Add(System.Guid.NewGuid.ToString(), dtProblemsIcd.Rows(icd)("sConceptId"))
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            If Not IsNothing(dtProblemsIcd) Then
                dtProblemsIcd.Dispose()
                dtProblemsIcd = Nothing
            End If
            oclsProblemList.Dispose()
            oclsProblemList = Nothing
            'frm.btnBrowseICD9_Click(Nothing, Nothing)

            If frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent)) = Windows.Forms.DialogResult.OK Then
                _isChanges = True
                'If gblnSMDBSetting = True And gstrSMProblem = "Problem" Then
                '    strProblem = frm.strSelectedProblem
                'Else
                '    strProblem = frm.txt_Problem.Text
                'End If
                strProblem = frm.txt_Problem.Text
                nICDRevision = frm.nICDRevision ''added for ICD10 implementation
                'If gblnSMDBSetting = True And gstrSMProblem = "Problem" Then
                '    If frm.strSelectedICD9 <> "" Then
                '        strICD9 = frm.strSelectedICD9.Replace(":", "-")
                '    Else
                '        ''Condition Added by kanchan on 20100701-To fix case No:#0004696-Diagnosis code is automatically assigned to new problems
                '        strICD9 = ""
                '    End If
                'Else
                '    strICD9 = frm.cmbICD9.Text.Trim()
                'End If
                strICD9 = frm.txtICD9.Text.Trim()
                strConcernstatus = frm.ConcernStatus
                strCDAProblemType = frm.CDAProblemType
                onSiteDate = frm.dtpOnsetDate.Text
                DischargeDate = frm.dtpDischargeDate.Text
                strLocation = frm.txtlocation.Text
                'nLocationId = frm.cmbLoacation.SelectedValue
                strProvider = frm.cmb_Provider.Text
                nProivderID = frm.cmb_Provider.SelectedValue
                'Added Rahul 20100825
                sResolveDate = frm.dtResolved.Text
                ''Condition Added by kanchan on 20100701-To fix case No:#0004696-Diagnosis code is automatically assigned to new problems
                strPrecription = ""
                strHiddenPrecription = ""
                strConceptID = frm.strConceptID
                strDescriptionID = frm.strDescriptionID
                strSnoMedID = frm.strSnoMedId
                strDescription = frm.strDescription
                strLateralityCode = frm.strLateralityCode
                strLateralityDesc = frm.strLateralityDesc
                bEncounterDiagnosis = frm.bEncounterDiagnosis
                Dim strhidpres As String = ""
                Dim strpres As String = ""
                strPrecription = ""
                strHiddenPrecription = ""
                If frm.cmb_Priscription.Items.Count > 0 Then
                    strhidpres = frm.cmb_Priscription.Items(0)
                    strpres = frm.cmb_Priscription.Items(0)
                    strPrecription = strpres
                    strHiddenPrecription = strhidpres
                End If
                For j As Integer = 0 To frm.cmb_Priscription.Items.Count - 1
                    If j = 0 Then
                        frm.cmb_Priscription.SelectedIndex = j
                        If strpres.Trim() <> frm.cmb_Priscription.SelectedItem.ToString.Trim() Then
                            strPrecription = strpres & "," & frm.cmb_Priscription.SelectedItem.ToString()
                            strHiddenPrecription = strhidpres & "|" & frm.cmb_Priscription.SelectedItem.ToString()
                        End If
                    Else
                        frm.cmb_Priscription.SelectedIndex = j
                        If strpres.Trim() <> frm.cmb_Priscription.SelectedItem.ToString.Trim() Then

                            strPrecription = strPrecription & "," & frm.cmb_Priscription.SelectedItem
                            strHiddenPrecription = strHiddenPrecription & "|" & frm.cmb_Priscription.SelectedItem
                        End If
                    End If
                Next

                For k As Integer = 0 To frm.cmbExams.Items.Count - 1
                    If k = 0 Then
                        frm.cmbExams.SelectedIndex = k
                        strExamID = frm.cmbExams.SelectedValue.ToString()
                    Else
                        frm.cmbExams.SelectedIndex = k
                        strExamID = strExamID & "," & frm.cmbExams.SelectedValue.ToString()
                    End If
                Next

                If frm.rbtn_Active.Checked = True Then
                    nStatus = Status.Active.ToString()
                ElseIf frm.rbInactive.Checked Then
                    nStatus = Status.Inactive.ToString()
                Else
                    nStatus = Status.Resolved.ToString()
                    dttoday = DateTime.Now
                End If

                If frm.rbt_Acute.Checked = True Then
                    nImmediacy = EnmImmediacy.Acute.ToString()
                ElseIf frm.rbtn_Chronic.Checked = True Then
                    nImmediacy = EnmImmediacy.Chronic.ToString()
                ElseIf frm.rbtn_Unknown.Checked = True Then
                    nImmediacy = EnmImmediacy.unknown.ToString()
                End If
                dgCommnents = frm.dgComments
                FillGrid()

                If _SelectedStatus = Status.Resolved Then
                    btn_Inactive_Click(sender, e)
                End If
                If _SelectedStatus = Status.Active Then
                    btn_ActiveProblem_Click(sender, e)
                End If

                If _SelectedStatus = Status.All Then
                    btn_Both_Click(sender, e)
                End If

                If _SelectedStatus = Status.Inactive Then
                    btn_Inactive2_Click(sender, e)
                End If

            End If

        Catch ex As Exception

        Finally
            If (IsNothing(frm) = False) Then
                frm.Close() 'Change made to solve memory Leak and word crash issue
                frm.Dispose()
                frm = Nothing
            End If
            SetNKProbVisibility()
        End Try


    End Sub
    Private Sub SetNKProbVisibility()
        Try


            If (c1ProblemList.Rows.Count <= 1) Then
                tlb_NKProblems.Visible = True
            Else
                tlb_NKProblems.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub FillGrid()
        Dim ParentRow As Integer
        Dim strComplaints As String
        Dim strComp() As String
        With c1ProblemList
            .Redraw = False
            .Rows.Add()

            strComplaints = strProblem

            .SetData(.Rows.Count - 1, COL_DOS, onSiteDate)
            .SetData(.Rows.Count - 1, Col_DischargeDate, DischargeDate)

            .SetData(.Rows.Count - 1, COL_USER, gnLoginID)


            .SetData(.Rows.Count - 1, COL_DIAGNOSIS, strICD9)
            .SetData(.Rows.Count - 1, COL_Immediacy, nImmediacy)
            .SetData(.Rows.Count - 1, COL_ProblemSatus, nStatus)
            .SetData(.Rows.Count - 1, COL_Provider, strProvider)
            .SetData(.Rows.Count - 1, COL_Location, strLocation)
            .SetData(.Rows.Count - 1, COL_LastModified, onSiteDate)
            .SetData(.Rows.Count - 1, Col_DischargeDate, DischargeDate)

            .SetData(.Rows.Count - 1, COL_PRESCRIPTION, strPrecription)
            .SetData(.Rows.Count - 1, Col_HiddedPrescription, strHiddenPrecription)
            .SetData(.Rows.Count - 1, COL_ExamID, strExamID)
            .SetData(.Rows.Count - 1, Col_UserName, gstrLoginName)
            .SetData(.Rows.Count - 1, Col_ICDRevision, nICDRevision)  ''added for ICD10 implementation
            'Added Infobutton
            .SetCellImage(.Rows.Count - 1, COL_INFOBUTTON, My.Resources.infobutton)
            '.SetCellImage(.Rows.Count - 1, COL_PROVIDEREDUCATIONBUTTON, My.Resources.I)
            '.SetCellImage(.Rows.Count - 1, COL_PATIENTEDUCATIONBUTTON, My.Resources.I)

            'Added Rahul 20100825

            If nStatus = "Resolved" Then
                .SetData(.Rows.Count - 1, COL_RsDt, sResolveDate)

            End If

            .SetData(.Rows.Count - 1, COL_VISITID, _VisitID)
            If Not IsNothing(dttoday) Then
                If dttoday.ToString().Trim() <> "1/1/0001 12:00:00 AM" Then

                    .SetData(.Rows.Count - 1, COL_RsDt, sResolveDate)

                End If

            End If

            For i As Integer = 0 To dgCommnents.Rows.Count - 1
                'strComplaints = strComplaints & vbNewLine & vbTab & "Comment" & i + 1 & " :" & dgCommnents.Item(0, i).Value & "-" & dgCommnents.Item(1, i).Value
                'Added Rahul 20100826
                strComplaints = strComplaints & vbNewLine & vbTab & "Comment" & i + 1 & " :" & dgCommnents.Item(1, i).Value & " - " & dgCommnents.Item(0, i).Value

            Next
            strComp = Split(strComplaints, vbNewLine)
            ParentRow = strComp.Length
            If ParentRow >= 0 Then
                .Rows(.Rows.Count - 1).Height = c1ProblemList.Rows.DefaultSize * ParentRow + 1
            End If
            .SetData(.Rows.Count - 1, COL_COMPLAINTS, strComplaints.Trim)
            .SetData(.Rows.Count - 1, COL_ConceptID, strConceptID)
            .SetData(.Rows.Count - 1, COL_DescriptionID, strDescriptionID)
            .SetData(.Rows.Count - 1, COL_SnoMedID, strSnoMedID)

            ''Dim splstrdescription As String()
            '' splstrdescription = strDescription.Split("(")
            If Not IsNothing(strDescription) Then  ''added for bugid 75107:
                If strDescription.Length >= 1 Then
                    If strDescription <> "" Then
                        .SetData(.Rows.Count - 1, COL_ProblemType, strDescription)

                    Else
                        .SetData(.Rows.Count - 1, COL_ProblemType, "")
                    End If

                Else
                    .SetData(.Rows.Count - 1, COL_ProblemType, "")
                End If
            Else
                .SetData(.Rows.Count - 1, COL_ProblemType, "")
            End If

            .SetData(.Rows.Count - 1, Col_IsModifed, False)
            .SetData(.Rows.Count - 1, Col_EncounterDiagnosis, bEncounterDiagnosis)
            .SetData(.Rows.Count - 1, Col_ReasonConceptID, strLateralityCode)
            .SetData(.Rows.Count - 1, Col_ReasonConceptDesc, strLateralityDesc)
            .SetData(.Rows.Count - 1, col_ConcernStatus, strConcernstatus)
            .SetData(.Rows.Count - 1, Col_CDAProblemType, strCDAProblemType)
            .Redraw = True

        End With

    End Sub

    Public Sub FillRow()
        Dim ParentRow As Integer
        Dim strComplaints As String = String.Empty
        Dim strComp() As String
        Dim SelectedRow As Integer
        With c1ProblemList
            .Redraw = False
            SelectedRow = .RowSel
            Dim ostyle As C1.Win.C1FlexGrid.CellStyle


            strComplaints = strProblem
            '' chetan added on 22 nov 2010
            .Cols(COL_DOS).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_USER).TextAlign = TextAlignEnum.CenterCenter
            .Cols(COL_DIAGNOSIS).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_Immediacy).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_ProblemSatus).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_Provider).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_Location).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_LastModified).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_PRESCRIPTION).TextAlign = TextAlignEnum.LeftCenter
            .Cols(Col_HiddedPrescription).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_ExamID).TextAlign = TextAlignEnum.LeftCenter
            '' chetan added on 22 nov 2010
            .SetData(SelectedRow, COL_DOS, onSiteDate)
            .SetData(SelectedRow, COL_USER, gnLoginID)
            .SetData(SelectedRow, COL_DIAGNOSIS, strICD9)
            .SetData(SelectedRow, COL_Immediacy, nImmediacy)
            .SetData(SelectedRow, COL_ProblemSatus, nStatus)
            .SetData(SelectedRow, COL_Provider, strProvider)
            .SetData(SelectedRow, COL_Location, strLocation)
            .SetData(SelectedRow, COL_LastModified, onSiteDate)
            .SetData(SelectedRow, Col_DischargeDate, DischargeDate)
            .SetData(SelectedRow, COL_PRESCRIPTION, strPrecription)
            .SetData(SelectedRow, Col_HiddedPrescription, strHiddenPrecription)
            .SetData(SelectedRow, COL_ExamID, strExamID)
            .SetData(SelectedRow, Col_UserName, gstrLoginName)
            .SetData(SelectedRow, Col_ICDRevision, nICDRevision) ''added for ICD10 implementation
            .SetData(SelectedRow, Col_IsModifed, True)
            'Added Rahul 20100825
            .Cols(COL_RsDt).TextAlign = TextAlignEnum.LeftCenter

            .SetData(SelectedRow, COL_RsDt, sResolveDate)

            If Not IsNothing(dttoday) Then
                If dttoday.ToString().Trim() <> "1/1/0001 12:00:00 AM" Then
                    If nStatus = "Active" Then
                        'ostyle = .Styles.Add("DataType")
                        Try
                            If (.Styles.Contains("DataType")) Then
                                ostyle = .Styles("DataType")
                            Else
                                ostyle = .Styles.Add("DataType")

                            End If
                        Catch ex As Exception
                            ostyle = .Styles.Add("DataType")

                        End Try
                        ostyle.DataType = GetType(String)
                        .SetCellStyle(SelectedRow, COL_RsDt, ostyle)
                        .SetData(SelectedRow, COL_RsDt, "")
                    Else
                        'ostyle = .Styles.Add("DataType")
                        Try
                            If (.Styles.Contains("DataType")) Then
                                ostyle = .Styles("DataType")
                            Else
                                ostyle = .Styles.Add("DataType")

                            End If
                        Catch ex As Exception
                            ostyle = .Styles.Add("DataType")

                        End Try
                        ostyle.DataType = GetType(Date)
                        .SetCellStyle(SelectedRow, COL_RsDt, ostyle)
                        '.SetData(SelectedRow, COL_RsDt, dttoday)
                        'Added Rahul 20100825
                        .SetData(SelectedRow, COL_RsDt, sResolveDate)
                    End If
                Else
                    ' ostyle = .Styles.Add("DataType")
                    Try
                        If (.Styles.Contains("DataType")) Then
                            ostyle = .Styles("DataType")
                        Else
                            ostyle = .Styles.Add("DataType")

                        End If
                    Catch ex As Exception
                        ostyle = .Styles.Add("DataType")

                    End Try
                    ostyle.DataType = GetType(String)
                    .SetCellStyle(SelectedRow, COL_RsDt, ostyle)
                    .SetData(SelectedRow, COL_RsDt, "")
                End If
            End If
            For i As Integer = 0 To dgCommnents.Rows.Count - 1
                'strComplaints = strComplaints & vbNewLine & vbTab & "Comment" & i + 1 & " :" & dgCommnents.Item(0, i).Value & "-" & dgCommnents.Item(1, i).Value
                'Added Rahul 20100826
                strComplaints = strComplaints & vbNewLine & vbTab & "Comment" & i + 1 & " :" & dgCommnents.Item(1, i).Value & " - " & dgCommnents.Item(0, i).Value

            Next
            strComp = Split(strComplaints, vbNewLine)
            ParentRow = strComp.Length
            '.Rows(.Row).AllowResizing = AllowDraggingEnum.Both
            '.Rows(.Row).AllowDragging = DrawModeEnum.OwnerDraw
            If ParentRow >= 0 Then
                .Rows(SelectedRow).Height = c1ProblemList.Rows.DefaultSize * ParentRow + 1
            End If

            .Cols(COL_COMPLAINTS).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_ConceptID).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_DescriptionID).TextAlign = TextAlignEnum.CenterCenter
            .Cols(COL_SnoMedID).TextAlign = TextAlignEnum.CenterCenter
            .Cols(Col_ReasonConceptID).TextAlign = TextAlignEnum.CenterCenter
            .Cols(Col_ReasonConceptDesc).TextAlign = TextAlignEnum.LeftCenter

            .SetData(SelectedRow, COL_COMPLAINTS, "")
            .SetData(SelectedRow, COL_COMPLAINTS, strComplaints.Trim)
            .SetData(SelectedRow, COL_ConceptID, strConceptID)
            .SetData(SelectedRow, COL_DescriptionID, strDescriptionID)
            .SetData(SelectedRow, COL_SnoMedID, strSnoMedID)
            .SetData(SelectedRow, Col_ReasonConceptID, strLateralityCode)
            .SetData(SelectedRow, Col_ReasonConceptDesc, strLateralityDesc)
            .SetData(SelectedRow, col_ConcernStatus, strConcernstatus)
            .SetData(SelectedRow, Col_CDAProblemType, strCDAProblemType)
            If strDescription <> "" Then
                .Cols(COL_ProblemType).TextAlign = TextAlignEnum.LeftCenter
                .SetData(SelectedRow, COL_ProblemType, strDescription)
            Else
                .SetData(SelectedRow, COL_ProblemType, "")
            End If

            .SetData(SelectedRow, Col_EncounterDiagnosis, bEncounterDiagnosis)
            .Redraw = True
        End With

    End Sub


    Private Sub tlb_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Edit.Click
        Dim i As Integer
        Dim probs As String = String.Empty
        Dim comm As String = String.Empty
        Dim ProblemID As String = String.Empty
        Dim sStatus As String = String.Empty
        Dim Diagnosis As String = String.Empty
        Dim Immediacy As String = String.Empty
        Dim Provider As String = String.Empty
        Dim Location As String = String.Empty
        Dim LastModified As String = String.Empty
        Dim ProblemStatus As String = String.Empty
        Dim strhPres() As String
        Dim p_VisitID As Long = 0
        Try

            dttoday = Nothing
            If c1ProblemList.Rows.Count > 0 Then
                i = c1ProblemList.RowSel
                If i <= 0 Then
                    Exit Sub
                End If
                If Not IsNothing(c1ProblemList.GetData(i, COL_COMPLAINTS)) Then
                    comm = c1ProblemList.GetData(i, COL_COMPLAINTS)
                End If
                ProblemID = c1ProblemList.GetData(i, COL_PROBLEMID)

            End If

            'gblnEnableCQMCypressTesting
            sStatus = c1ProblemList.GetData(i, COL_ProblemSatus)
            Diagnosis = c1ProblemList.GetData(i, COL_DIAGNOSIS)
            Immediacy = c1ProblemList.GetData(i, COL_Immediacy)
            Provider = c1ProblemList.GetData(i, COL_Provider)
            Location = c1ProblemList.GetData(i, COL_Location)
            LastModified = c1ProblemList.GetData(i, COL_DOS)
            strConcernstatus = Convert.ToString(c1ProblemList.GetData(i, col_ConcernStatus))
            strCDAProblemType = Convert.ToString(c1ProblemList.GetData(i, Col_CDAProblemType))

            If (gblnEnableCQMCypressTesting) Then
                DischargeDate = c1ProblemList.GetData(i, Col_DischargeDate)
            Else
                DischargeDate = Nothing
            End If

            strPrecription = c1ProblemList.GetData(i, COL_PRESCRIPTION)
            strHiddenPrecription = c1ProblemList.GetData(i, Col_HiddedPrescription)
            ProblemStatus = c1ProblemList.GetData(i, COL_ProblemSatus)
            PExamID = c1ProblemList.GetData(i, COL_ExamID)
            p_VisitID = c1ProblemList.GetData(i, COL_VISITID)
            'Added Rahul 20100825
            sResolveDate = c1ProblemList.GetData(i, COL_RsDt)

            Dim frm As frmAddProblemList
            frm = New frmAddProblemList(_PatientID, p_VisitID, c1ProblemList)
            frm.txt_Problem.Text = probs.Trim()
            frm.dtpOnsetDate.Text = LastModified

            frm.dtpDischargeDate.Text = DischargeDate
            'frm.strSnomedCode = comm
            If frm.rbt_Inactive.Checked <> False Then
                frm.dtResolved.Text = sResolveDate
            End If


            frm.Location = Location
            frm.Provider = Provider
            frm._ProblemId = ProblemID
            frm.ConcernStatus = strConcernstatus
            frm.CDAProblemType = strCDAProblemType
            If ProblemStatus = "Active" Then
                frm.rbtn_Active.Checked = True 'Active
                frm.rbInactive.Checked = False 'Inactive
                frm.rbt_Inactive.Checked = False ' Resolved
            ElseIf ProblemStatus = "Inactive" Then
                frm.rbtn_Active.Checked = False 'Active
                frm.rbInactive.Checked = True 'Inactive
                frm.rbt_Inactive.Checked = False ' Resolved
            Else
                frm.rbtn_Active.Checked = False 'Active
                frm.rbInactive.Checked = False 'Inactive
                frm.rbt_Inactive.Checked = True ' Resolved
            End If

            strhPres = Split(strHiddenPrecription, "|")

            For k As Integer = 0 To strhPres.Length - 1
                frm.cmb_Priscription.Items.Add(strhPres.GetValue(k))
                frm.cmb_Priscription.Text = strhPres.GetValue(0)
            Next

            Dim arrIcd() As String
            Dim dtProblemsIcd As DataTable
            Dim oclsProblemList As clsPatientProblemList
            oclsProblemList = New clsPatientProblemList
            'oclsProblemList = New clsPatientProblemList
            dtProblemsIcd = oclsProblemList.Get_PatientProblemList(_PatientID)
            Try
                For icd As Integer = 0 To dtProblemsIcd.Rows.Count - 1 Step 1
                    arrIcd = Convert.ToString(dtProblemsIcd.Rows(icd)("Diagnosis")).Split("-")
                    If arrIcd.Length > 1 Then
                        If arrIcd(0) <> "" Then
                            If frmAddProblemList.htIcdSnomed.Count > 0 Then
                                If frmAddProblemList.htIcdSnomed.ContainsKey(arrIcd(0)) Then
                                    If Convert.ToString(frmAddProblemList.htIcdSnomed.Item(arrIcd(0))) <> Convert.ToString(dtProblemsIcd.Rows(icd)("sConceptId")) Then
                                        frmAddProblemList.htIcdSnomed.Item(arrIcd(0)) = Convert.ToString(dtProblemsIcd.Rows(icd)("sConceptId"))
                                    End If
                                Else
                                    frmAddProblemList.htIcdSnomed.Add(arrIcd(0), dtProblemsIcd.Rows(icd)("sConceptId"))
                                End If
                            Else
                                frmAddProblemList.htIcdSnomed.Add(arrIcd(0), dtProblemsIcd.Rows(icd)("sConceptId"))
                            End If
                        Else
                            frmAddProblemList.htIcdSnomed.Add(System.Guid.NewGuid.ToString(), dtProblemsIcd.Rows(icd)("sConceptId"))
                        End If
                    Else
                        frmAddProblemList.htIcdSnomed.Add(System.Guid.NewGuid.ToString(), dtProblemsIcd.Rows(icd)("sConceptId"))
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            If Not IsNothing(dtProblemsIcd) Then
                dtProblemsIcd.Dispose()
                dtProblemsIcd = Nothing
            End If
            oclsProblemList.Dispose()
            oclsProblemList = Nothing
            arrIcd = Diagnosis.Split("-")
            'frm.cmbICD9.SelectedValue = arrICD9.GetValue(0).ToString().Trim()
            If Diagnosis <> "" And Diagnosis <> "-" Then
                'frm.txtICD9.Text = Diagnosis.Trim()
                If arrIcd.Length > 1 Then
                    frm.strCurrentICDCode = arrIcd(0).ToString().Trim()
                    frm.strCurrentICDDesc = arrIcd(1).ToString().Trim()
                    frm.strCurrentICDRevision = c1ProblemList.GetData(i, Col_ICDRevision)
                End If
                'If arrICD9.Length = 2 Then
                '    If Not IsNothing(frm.cmbICD9.DataSource) Then
                '        Dim dt As DataTable
                '        dt = frm.cmbICD9.DataSource
                '        dt.DefaultView.RowFilter = "ICD9Code='" + frm.cmbICD9.SelectedValue + "'"
                '        'frm.cmbICD9.DataSource = Nothing
                '        'frm.cmbICD9.ValueMember = arrICD9.GetValue(0).ToString().Trim()
                '        'Dim myStr As String = arrICD9.GetValue(0).ToString().Trim() & "-" & arrICD9.GetValue(1).ToString().Trim()
                '        'frm.cmbICD9.DisplayMember = myStr
                '        'frm.cmbICD9.Items.Add(myStr)
                '        'frm.cmbICD9.SelectedIndex = 0
                '    End If
                'End If
            End If
            frm.Fill_Diagnosis()

            'Dim strExamName As String
            Dim _strSQL As String = ""
            Dim dtExams As DataTable
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            If PExamID <> "" Then
                _strSQL = "SELECT sExamName,nExamID from PatientExams WHERE nExamID IN (" & PExamID & ")"
                ''''''''''''' Added by Sanjog -As on 28-05-2010
                dtExams = oDB.ReadQueryDataTable(_strSQL)
                If Not IsNothing(dtExams) Then
                    If dtExams.Rows.Count > 0 Then
                        frm.cmbExams.DataSource = dtExams
                        frm.cmbExams.DisplayMember = "sExamName"
                        frm.cmbExams.ValueMember = "nExamID"

                    End If
                End If
            End If
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            If Immediacy = "Acute" Then
                frm.rbt_Acute.Checked = True
            ElseIf Immediacy = "Chronic" Then
                frm.rbtn_Chronic.Checked = True
            ElseIf Immediacy = "unknown" Then
                frm.rbtn_Unknown.Checked = True
            End If
            frm.comm = comm
            If Diagnosis = "-" Then
                frm.ICD9 = ""
            Else
                frm.ICD9 = Diagnosis
            End If
            frm.strConceptID = c1ProblemList.GetData(i, COL_ConceptID)

            frm.strDescription = c1ProblemList.GetData(i, COL_ProblemType)

            frm.strConceptID_Old = c1ProblemList.GetData(i, COL_ConceptID)
            frm.strDescriptionID = c1ProblemList.GetData(i, COL_DescriptionID)
            frm.strSnoMedId = c1ProblemList.GetData(i, COL_SnoMedID)
            frm.strLateralityCode = c1ProblemList.GetData(i, Col_ReasonConceptID)
            frm.strLateralityDesc = c1ProblemList.GetData(i, Col_ReasonConceptDesc)

            If c1ProblemList.GetData(i, COL_RsDt) IsNot Nothing Then
                If Convert.ToString(c1ProblemList.GetData(i, COL_RsDt)) <> "" Then
                    frm.dtResolved.Value = c1ProblemList.GetData(i, COL_RsDt)
                End If
            End If
            frm.lblConceptID.Text = c1ProblemList.GetData(i, COL_ConceptID)
            frm.lblDescriptionID.Text = c1ProblemList.GetData(i, COL_DescriptionID)
            'frm.lblSnoMedID.Text = c1ProblemList.GetData(i, COL_SnoMedID)
            'frm.Fill_SnomedCode()
            'frm.txtSnomed.Text = c1ProblemList.GetData(i, COL_SnoMedID) + "-" + c1ProblemList.GetData(i, COL_ProblemType)
            frm.ShowInTaskbar = False
            frm.Text = "Edit Problem List"
            frm.IsNewProblem = False
            'If PExamID = "" Then
            '    frm.nProblemExamId = 0
            'Else
            '    'Code Change For bug no 70671:: Problem List-> Problem with two exams giving exception(Code change are done as per discussion)
            '    If PExamID.Contains(",") Then
            '        frm.nProblemExamId = 0
            '    Else
            '        frm.nProblemExamId = PExamID
            '    End If
            'End If
            frm.bEncounterDiagnosis = c1ProblemList.GetData(i, Col_EncounterDiagnosis)
            If frm.txtSnomed.Text <> "" Then
                Dim strTemp() As String = frm.txtSnomed.Text.Split("-")
                If strTemp.Length > 1 Then
                    frm.txt_Problem.Text = strTemp(1).Trim()
                End If
            End If
            If frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent)) = Windows.Forms.DialogResult.OK Then
                _isChanges = True
                strProblem = frm.strSelectedProblem
                'If gblnSMDBSetting = True And gstrSMProblem = "Problem" Then
                '    If frm.strSelectedICD9 <> "" Then
                '        strICD9 = frm.strSelectedICD9.Replace(":", "-")
                '    Else
                '        ''Condition Added by kanchan on 20100701-To fix case No:#0004696-Diagnosis code is automatically assigned to new problems
                '        ' strICD9 = ""
                '        'SHUBHANGI 20101112
                '        strICD9 = frm.cmbICD9.Text.Trim()
                '    End If
                'Else
                strICD9 = frm.txtICD9.Text.Trim()
                strProblem = frm.txt_Problem.Text
                'End If
                onSiteDate = frm.dtpOnsetDate.Text
                If (gblnEnableCQMCypressTesting) Then
                    DischargeDate = frm.dtpDischargeDate.Text
                Else
                    DischargeDate = Nothing
                End If

                strLocation = frm.txtlocation.Text
                'nLocationId = frm.cmbLoacation.SelectedValue
                strProvider = frm.cmb_Provider.Text
                nProivderID = frm.cmb_Provider.SelectedValue
                'Added Rahul 20100825
                nICDRevision = frm.nICDRevision  ''added for ICD10 implementation
                sResolveDate = frm.dtResolved.Text
                Dim selstrpres As String = String.Empty '' used to set first item inside dropdown of  prescription as item in proplemlist prescription 
                '' chetan added on 1-nov -2010 
                strHiddenPrecription = ""
                strPrecription = ""
                If frm.cmb_Priscription.Items.Count > 0 Then
                    selstrpres = frm.cmb_Priscription.SelectedItem
                    strHiddenPrecription = selstrpres
                    strPrecription = selstrpres
                End If
                '' chetan ended  on 1-nov -2010 
                For i = 0 To frm.cmb_Priscription.Items.Count - 1
                    If i = 0 Then
                        frm.cmb_Priscription.SelectedIndex = i
                        If selstrpres.Trim() <> frm.cmb_Priscription.SelectedItem.ToString.Trim() Then
                            strPrecription = strPrecription & "," & frm.cmb_Priscription.SelectedItem

                            strHiddenPrecription = strHiddenPrecription & "|" & frm.cmb_Priscription.SelectedItem
                        End If
                    Else
                        frm.cmb_Priscription.SelectedIndex = i
                        'condition commented to fix bug 9771 in6020
                        'condition uncommented to fix bug 14541 in 6030
                        If selstrpres.Trim() <> frm.cmb_Priscription.SelectedItem.ToString.Trim() Then

                            strPrecription = strPrecription & "," & frm.cmb_Priscription.SelectedItem
                            strHiddenPrecription = strHiddenPrecription & "|" & frm.cmb_Priscription.SelectedItem
                        End If
                    End If
                Next
                If frm.cmb_Priscription.Items.Count <= 0 Then
                    strPrecription = ""
                    strHiddenPrecription = ""
                End If

                For k As Integer = 0 To frm.cmbExams.Items.Count - 1
                    If k = 0 Then
                        frm.cmbExams.SelectedIndex = k
                        strExamID = frm.cmbExams.SelectedValue.ToString()
                    Else
                        frm.cmbExams.SelectedIndex = k
                        strExamID = strExamID & "," & frm.cmbExams.SelectedValue.ToString()
                    End If
                Next
                If frm.cmbExams.Items.Count <= 0 Then
                    strExamID = ""
                End If

                If frm.rbtn_Active.Checked = True Then
                    nStatus = Status.Active.ToString()
                ElseIf frm.rbInactive.Checked Then
                    nStatus = Status.Inactive.ToString()
                Else
                    nStatus = Status.Resolved.ToString()
                    dttoday = DateTime.Now
                End If

                If frm.rbt_Acute.Checked = True Then
                    nImmediacy = EnmImmediacy.Acute.ToString()
                ElseIf frm.rbtn_Chronic.Checked = True Then
                    nImmediacy = EnmImmediacy.Chronic.ToString()
                ElseIf frm.rbtn_Unknown.Checked = True Then
                    nImmediacy = EnmImmediacy.unknown.ToString()
                End If
                dgCommnents = frm.dgComments
                strConceptID = frm.strConceptID
                strDescriptionID = frm.strDescriptionID
                strSnoMedID = frm.strSnoMedId
                strDescription = frm.strDescription
                bEncounterDiagnosis = frm.bEncounterDiagnosis
                strLateralityCode = frm.strLateralityCode
                strLateralityDesc = frm.strLateralityDesc
                strConcernstatus = frm.ConcernStatus
                strCDAProblemType = frm.CDAProblemType
                FillRow()
                c1ProblemList.SetData(c1ProblemList.RowSel, COL_LastModified, Date.Now.ToString("MM/dd/yyyy"))
                '   If _SelectedStatus = Status.Removed Then
                'btn_Remove_Click(sender, e)
                If _SelectedStatus = Status.Resolved Then
                    btn_Inactive_Click(sender, e)
                ElseIf _SelectedStatus = Status.Active Then
                    btn_ActiveProblem_Click(sender, e)
                ElseIf _SelectedStatus = Status.Inactive Then
                    btn_Inactive2_Click(sender, e)
                ElseIf _SelectedStatus = Status.All Then
                    btn_Both_Click(sender, e)
                End If
                Dim rowIndex As Int64
                rowIndex = c1ProblemList.FindRow(ProblemID, 1, COL_PROBLEMID, False, True, False)
                If rowIndex > 0 Then
                    c1ProblemList.Select(rowIndex, 0, True)
                End If



            Else
                c1ProblemList.SetData(c1ProblemList.RowSel, Col_IsModifed, False)
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

        End Try

    End Sub

    Private Sub c1ProblemList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c1ProblemList.DoubleClick

        If Not IsNothing(c1ProblemList) Then
            If c1ProblemList.Rows.Count > 0 Then
                If c1ProblemList.ColSel > 0 And c1ProblemList.RowSel > 0 Then
                    c1ProblemList.Select(c1ProblemList.RowSel, 1)
                    tlb_Edit_Click(sender, e)
                End If
            End If
        End If
    End Sub

    Private Sub btn_ActiveProblem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ActiveProblem.Click


        Dim i As Integer
        _SelectedStatus = Status.Active
        _strbtnClicked = "Active"  ''added for bugid 92441 setting focus to current tab
        ' tlb_Delete.Visible = True
        Dim FirsRowIndex As Integer = 0
        Row_Cnt = 0
        With c1ProblemList
            For i = 1 To .Rows.Count - 1

                If .GetData(i, COL_ProblemSatus) = "Active" Then

                    If FirsRowIndex = 0 Then
                        FirsRowIndex = i
                    End If
                    .Rows(i).Visible = True

                    Row_Cnt += 1

                Else
                    .Rows(i).Visible = False
                End If
            Next
            trvDefination.Nodes.Clear()
            If .Rows.Count > 0 Then
                .Select(FirsRowIndex, .Cols.Count - 1, FirsRowIndex, 0, True)
                If c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination) <> "" Then

                    FillDefination(c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination))
                    trvDefination.ExpandAll()
                End If
            End If

        End With
    End Sub


    Public Sub FillActiveProblem()
        Dim i As Integer
        _SelectedStatus = Status.Active
        ' tlb_Delete.Visible = True
        Dim FirsRowIndex As Integer = 0
        Row_Cnt = 0
        With c1ProblemList
            For i = 1 To .Rows.Count - 1
                '  If .GetData(i, COL_ProblemSatus) = "Active" And .GetData(i, COL_STATUS) <> "Removed" Then
                If .GetData(i, COL_ProblemSatus) = "Active" Then

                    If FirsRowIndex = 0 Then
                        FirsRowIndex = i
                    End If
                    .Rows(i).Visible = True

                    Row_Cnt += 1

                Else
                    .Rows(i).Visible = False
                End If
            Next
            trvDefination.Nodes.Clear()
            If .Rows.Count > 0 Then
                .Select(FirsRowIndex, .Cols.Count - 1, FirsRowIndex, 0, True)
                If c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination) <> "" Then

                    FillDefination(c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination))
                    trvDefination.ExpandAll()
                End If
            End If

        End With
    End Sub

    Private Sub btn_Inactive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Inactive.Click
        Dim i As Integer
        Dim FirsRowIndex As Integer = 0
        c1ProblemList.RowSel = 0
        _SelectedStatus = Status.Resolved
        _strbtnClicked = "Resolved"
        Row_Cnt = 0
        With c1ProblemList
            For i = 1 To .Rows.Count - 1
                If .GetData(i, COL_ProblemSatus) = "Resolved" Then 'And .GetData(i, COL_STATUS) <> "Removed" Then
                    .Rows(i).Visible = True
                    Row_Cnt += 1

                    If FirsRowIndex = 0 Then
                        FirsRowIndex = i
                    End If

                Else
                    .Rows(i).Visible = False
                End If
            Next
            trvDefination.Nodes.Clear()
            If .Rows.Count > 0 Then
                .Select(FirsRowIndex, .Cols.Count - 1, FirsRowIndex, 0, True)
                If c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination) <> "" Then

                    FillDefination(c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination))
                    trvDefination.ExpandAll()
                End If
            End If

        End With
    End Sub
    Private Sub ResolvedStatus()
        Dim i As Integer
        Dim FirsRowIndex As Integer = 0
        c1ProblemList.RowSel = 0
        _SelectedStatus = Status.Resolved
        ' tlb_Delete.Visible = True
        Row_Cnt = 0
        With c1ProblemList
            For i = 1 To .Rows.Count - 1
                If .GetData(i, COL_ProblemSatus) = "Resolved" Then 'And .GetData(i, COL_STATUS) <> "Removed" Then
                    .Rows(i).Visible = True
                    Row_Cnt += 1

                    If FirsRowIndex = 0 Then
                        FirsRowIndex = i
                    End If

                Else
                    .Rows(i).Visible = False
                End If
            Next
            trvDefination.Nodes.Clear()
            If .Rows.Count > 0 Then
                .Select(FirsRowIndex, .Cols.Count - 1, FirsRowIndex, 0, True)
                If c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination) <> "" Then

                    FillDefination(c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination))
                    trvDefination.ExpandAll()
                End If
            End If

        End With
        btn_Inactive.Focus()
    End Sub

    Private Sub btn_Both_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Both.Click
        Dim i As Integer
        Dim FirsRowIndex As Integer = 0
        c1ProblemList.RowSel = 0
        ' c1ProblemList.SelectionMode = SelectionModeEnum.Default
        _SelectedStatus = Status.All
        _strbtnClicked = "All"
        '  tlb_Delete.Visible = True
        Row_Cnt = 0
        With c1ProblemList
            For i = 1 To .Rows.Count - 1
                If .GetData(i, COL_ProblemSatus) = "Active" Or .GetData(i, COL_ProblemSatus) = "Resolved" Or .GetData(i, COL_ProblemSatus) = "Inactive" Then
                    '' And .GetData(i, COL_STATUS) <> "Removed" Or _'And .GetData(i, COL_STATUS) <> "Removed" Or _ 'And .GetData(i, COL_STATUS) <> "Inactive" Then
                    .Rows(i).Visible = True
                    Row_Cnt += 1
                    If FirsRowIndex = 0 Then
                        FirsRowIndex = i
                    End If

                Else
                    .Rows(i).Visible = False
                End If
            Next
            trvDefination.Nodes.Clear()
            If .Rows.Count > 0 Then
                .Select(FirsRowIndex, .Cols.Count - 1, FirsRowIndex, 0, True)
                If c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination) <> "" Then

                    FillDefination(c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination))
                    trvDefination.ExpandAll()
                End If
            End If

        End With
    End Sub

    Private Sub btn_Inactive2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Inactive2.Click
        Dim i As Integer
        Dim FirsRowIndex As Integer = 0
        c1ProblemList.RowSel = 0
        _SelectedStatus = Status.Inactive
        _strbtnClicked = "InActive"
        ' tlb_Delete.Visible = True
        Row_Cnt = 0
        With c1ProblemList
            For i = 1 To .Rows.Count - 1
                If .GetData(i, COL_ProblemSatus) = "Inactive" Then 'And .GetData(i, COL_STATUS) <> "Removed" Then
                    .Rows(i).Visible = True
                    Row_Cnt += 1

                    If FirsRowIndex = 0 Then
                        FirsRowIndex = i
                    End If

                Else
                    .Rows(i).Visible = False
                End If
            Next
            trvDefination.Nodes.Clear()
            If .Rows.Count > 0 Then
                .Select(FirsRowIndex, .Cols.Count - 1, FirsRowIndex, 0, True)
                If c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination) <> "" Then

                    FillDefination(c1ProblemList.GetData(c1ProblemList.RowSel, COL_Defination))
                    trvDefination.ExpandAll()
                End If
            End If

        End With
    End Sub


    Private Sub btn_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Remove.Click
        Dim i As Integer

        With c1ProblemList
            For i = 1 To .Rows.Count - 1
                If .GetData(i, COL_ProblemSatus) = "Removed" Then
                    .Rows(i).Visible = True
                Else
                    .Rows(i).Visible = False
                End If
            Next
        End With
    End Sub

    Private Sub btn_Remove_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btn_Remove.MouseClick
        btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btn_Remove.BackgroundImageLayout = ImageLayout.Center

        btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Inactive.BackgroundImageLayout = ImageLayout.Center

        btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Both.BackgroundImageLayout = ImageLayout.Center

        btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center

        btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Inactive2.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btn_Inactive_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btn_Inactive.MouseClick
        btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Remove.BackgroundImageLayout = ImageLayout.Center

        btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btn_Inactive.BackgroundImageLayout = ImageLayout.Center

        btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Both.BackgroundImageLayout = ImageLayout.Center

        btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center

        btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Inactive2.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btn_Both_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btn_Both.MouseClick
        btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Remove.BackgroundImageLayout = ImageLayout.Center

        btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Inactive.BackgroundImageLayout = ImageLayout.Center

        btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btn_Both.BackgroundImageLayout = ImageLayout.Center

        btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center

        btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Inactive2.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btn_InactiveProblem_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btn_Inactive2.MouseClick
        btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Remove.BackgroundImageLayout = ImageLayout.Center

        btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Inactive.BackgroundImageLayout = ImageLayout.Center

        btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Both.BackgroundImageLayout = ImageLayout.Center

        btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center

        btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btn_Inactive2.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btn_ActiveProblem_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btn_ActiveProblem.MouseClick
        btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Remove.BackgroundImageLayout = ImageLayout.Center

        btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Inactive.BackgroundImageLayout = ImageLayout.Center

        btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Both.BackgroundImageLayout = ImageLayout.Center

        btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btn_Inactive2.BackgroundImageLayout = ImageLayout.Center

        btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center
    End Sub


    Private Sub tlb_OpenExam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_OpenExam.Click
        Dim objclsProblist As New clsPatientProblemList
        Dim dtExam As DataTable = Nothing
        Dim _issaved As Boolean = True
        Dim Result As Integer
        Try


            If c1ProblemList.RowSel > 0 Then

                If c1ProblemList.Rows.Count > 1 Then
                    _issaved = checkProblemListSaved()
                    If _issaved = False Or _isChanges = True Then
                        Result = MessageBox.Show("Patient Exam cannot accessed without saving the Problem List. Do you want to save the Problem List?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If Result = MsgBoxResult.Yes Then
                            _isChanges = False
                            If ValidateProblemList() = False Then
                                ResolvedStatus()
                                btn_Inactive.Focus()
                                btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                                btn_Remove.BackgroundImageLayout = ImageLayout.Center

                                btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                                btn_Inactive.BackgroundImageLayout = ImageLayout.Center

                                btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                                btn_Both.BackgroundImageLayout = ImageLayout.Center

                                btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                                btn_Inactive2.BackgroundImageLayout = ImageLayout.Center

                                btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                                btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center

                                Exit Sub
                            End If

                            Call SaveProblemList(True)

                            Dim dtProblems As New DataTable
                            dtProblems = objclsProblist.Get_PatientProblemList(_PatientID)

                            If IsNothing(dtProblems) = False Then
                                Call SetGridStyle(dtProblems)
                                btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                                btn_Remove.BackgroundImageLayout = ImageLayout.Center

                                btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                                btn_Inactive.BackgroundImageLayout = ImageLayout.Center

                                btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                                btn_Both.BackgroundImageLayout = ImageLayout.Center

                                btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                                btn_Inactive2.BackgroundImageLayout = ImageLayout.Center

                                btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                                btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center
                                dtProblems.Dispose()
                                dtProblems = Nothing
                            End If
                            btn_ActiveProblem_Click(sender, e)
                            Exit Sub
                        ElseIf Result = MsgBoxResult.No Then
                            Exit Sub
                        End If
                    End If
                    If c1ProblemList.GetData(c1ProblemList.Row, COL_DIAGNOSIS) <> "" Then
                        Dim strDiagnosis As String = c1ProblemList.GetData(c1ProblemList.Row, COL_DIAGNOSIS).ToString()
                        Dim dtDOS As DateTime = c1ProblemList.GetData(c1ProblemList.Row, COL_DOS).ToString()
                        Dim sep As Char() = {"-"}
                        Dim ICD9 As String() = strDiagnosis.Split(sep, 2)
                        Dim ICD9Code As String = ""
                        Dim ICD9Desc As String = ""
                        If ICD9.Length = 1 Then
                            ICD9Code = ICD9(0).Trim
                        ElseIf ICD9.Length = 2 Then
                            ICD9Code = ICD9(0).Trim
                            ICD9Desc = ICD9(1).Trim
                        End If
                        dtExam = objclsProblist.GetExamDetails(_VisitID, ICD9Code, ICD9Desc, dtDOS, _PatientID)
                        objclsProblist.Dispose()
                        objclsProblist = Nothing
                        If Not IsNothing(dtExam) Then
                            If dtExam.Rows.Count > 0 Then
                                If CType(dtExam.Rows(0)(0), Int64) <> ExamID Then
                                    ShowPastExam(CType(dtExam.Rows(0)(0), Int64), _PatientID, CType(dtExam.Rows(0)(1), Int64), CType(dtExam.Rows(0)(3), DateTime).Date, dtExam.Rows(0)(2).ToString, dtExam.Rows(0)(6).ToString, CType(dtExam.Rows(0)(4), Boolean))
                                End If

                            Else
                                MessageBox.Show("No exam is associated for this diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No exam is associated for this diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        MessageBox.Show("No diagnosis and Exam is associated for this Problem.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            If Not IsNothing(objclsProblist) Then
                objclsProblist.Dispose()
                objclsProblist = Nothing
            End If
            If Not IsNothing(dtExam) Then
                dtExam.Dispose()
                dtExam = Nothing
            End If

        End Try

    End Sub

    Private Sub c1ProblemList_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1ProblemList.GotFocus
        Try
            If IsOpenNewExam = True Then
                Dim strExamcount As String
                Dim strProblemExam As String
                Dim strNewExam As String
                Dim oclsProblemList As clsPatientProblemList
                oclsProblemList = New clsPatientProblemList
                'oclsProblemList = New clsPatientProblemList
                strExamcount = oclsProblemList.GetExamCount(nExamId)
                oclsProblemList.Dispose()
                oclsProblemList = Nothing
                If Not IsNothing(strExamcount) Then
                    If strExamcount <> "" Then
                        strProblemExam = c1ProblemList.GetData(c1ProblemList.RowSel, COL_ExamID)
                        If strProblemExam = "" Or strProblemExam = "0" Then
                            strNewExam = CType(nExamId, String)

                            c1ProblemList.SetData(c1ProblemList.RowSel, COL_ExamID, strNewExam)
                        Else
                            strProblemExam = strProblemExam & "," & CType(nExamId, String)
                            c1ProblemList.SetData(c1ProblemList.RowSel, COL_ExamID, "")
                            c1ProblemList.SetData(c1ProblemList.RowSel, COL_ExamID, strProblemExam)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            IsOpenNewExam = False
        End Try
    End Sub

    Private Sub tlb_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Delete.Click
        Dim rowNo As Integer = -1
        'If c1ProblemList.Rows.Count > 1 Then
        If Row_Cnt > 0 Then


            rowNo = c1ProblemList.RowSel
            If rowNo <= 0 Then
                Exit Sub
            End If
            If rowNo > 0 Then
                If MessageBox.Show("Are you sure you want to delete the selected record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    c1ProblemList.Select(rowNo, COL_DIAGNOSIS)

                    _ProblemID = c1ProblemList.GetData(c1ProblemList.RowSel, COL_PROBLEMID)
                    Dim lst As New myList
                    With lst
                        .ID = _ProblemID
                        If Not c1ProblemList.GetData(c1ProblemList.RowSel, COL_DOS) Is Nothing Then
                            .VisitDate = Format(Convert.ToDateTime(c1ProblemList.GetData(c1ProblemList.RowSel, COL_DOS)), "MM/dd/yyyy hh:mm:ss tt")
                        Else
                            .VisitDate = Format(Now, "MM/dd/yyyy hh:mm:ss tt")
                        End If
                    End With
                    If IsNothing(DeletedProblemlist) Then
                        DeletedProblemlist = New ArrayList
                    End If

                    DeletedProblemlist.Add(lst)
                    c1ProblemList.Rows.Remove(rowNo)
                    c1ProblemList.RowSel = -1


                End If
            End If
        End If



        SetNKProbVisibility()
    
    End Sub


    Private Sub tlb_RxMed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_RxMed.Click

        Dim rowNo As Integer = -1

        If Row_Cnt > 0 Then
            rowNo = c1ProblemList.RowSel
            If rowNo <= 0 Then
                Exit Sub
            End If
            If rowNo > 0 Then

                Dim frmRxMeds As frmPrescription
                frmRxMeds = frmPrescription.GetInstance(_PatientID, True)
                If IsNothing(frmRxMeds) = True Then
                    Exit Sub
                End If

                If frmPrescription.IsOpen = False Then
                    frmRxMeds.ShowMedication()
                End If

                If frmRxMeds.blncancel = True Then
                    With frmRxMeds
                        .WindowState = FormWindowState.Maximized
                        .mycallerpre = Me
                        .MdiParent = Me.ParentForm
                        .TopMost = True
                        .blnOpenPre = True
                        .Show()
                    End With
                End If
            End If
        End If

    End Sub

    '' swaraj 20100611 - for storing prescription values under prescription in c1ProblemList
    Public Sub GetPrescription() 'As DataTable
        Dim oDB As New DataBaseLayer
        'Dim strSQL As String
        Dim i As Integer
        Dim strDrugName As String = String.Empty
        'Dim strDosage As String

        Try

            Dim objclsProblist As New clsPatientProblemList
            '' Fill Diagnosis&Rx Of the Patient for the Visit  in strDia
            dtRx = objclsProblist.Get_ProblemListRx(_PatientID)
            objclsProblist.Dispose()
            objclsProblist = Nothing
            For i = 0 To dtRx.Rows.Count - 1
                'If IsNothing(strDrugName) Then
                '    strDrugName = dtRx.Rows(i)("DrugName").ToString() & "~" & dtRx.Rows(i)("Dosage").ToString()
                'Else
                strDrugName = strDrugName + "," + dtRx.Rows(i)("DrugName").ToString() & "~" & dtRx.Rows(i)("Dosage").ToString()
                'End If
            Next
            If (strDrugName <> String.Empty) Then
                strDrugName = strDrugName.Substring(1, strDrugName.Length - 1)
            End If
            If c1ProblemList.Rows.Count > 0 Then
                If c1ProblemList.RowSel > 0 Then
                    c1ProblemList.SetData(c1ProblemList.RowSel, COL_PRESCRIPTION, strDrugName)
                End If
            End If

        Catch ex As Exception
            'Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing

            If Not IsNothing(dtRx) Then
                dtRx.Dispose()
                dtRx = Nothing
            End If

        End Try
    End Sub


    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click

        pnltrvDefination.Visible = True
        btnUp.Visible = False
        btnDown.Visible = True
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.Resources.Down
        btnDown.BackgroundImageLayout = ImageLayout.Center

    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click

        pnltrvDefination.Visible = False
        btnUp.Visible = True
        btnDown.Visible = False
        btnUp.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
        btnUp.BackgroundImageLayout = ImageLayout.Center

    End Sub

    Private Sub btnDown_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseHover
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.Resources.DownHover
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnDown_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseLeave
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.Resources.Down
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnUp_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.MouseHover
        btnUp.BackgroundImage = Global.gloEMR.My.Resources.Resources.UPHover
        btnUp.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnUp_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.MouseLeave
        btnUp.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
        btnUp.BackgroundImageLayout = ImageLayout.Center
    End Sub


    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID 'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub



    Private Sub c1ProblemList_CellChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ProblemList.CellChanged
        If _isFormLoad = False Then
            _isChanges = True
        End If
    End Sub

    Private Sub frmProblemList_SaveFunction() Handles Me.SaveFunction
        If ValidateProblemList() = False Then
            '' chetan added for  having no resolved date record  on  22-nov-2010
            ResolvedStatus()
            btn_Inactive.Focus()
            btn_Remove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btn_Remove.BackgroundImageLayout = ImageLayout.Center

            btn_Inactive.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btn_Inactive.BackgroundImageLayout = ImageLayout.Center

            btn_Both.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btn_Both.BackgroundImageLayout = ImageLayout.Center

            btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center

            btn_Inactive2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btn_Inactive2.BackgroundImageLayout = ImageLayout.Center
            '' chetan added for  having no resolved date record  on  22-nov-2010
            Exit Sub
        End If
        ''''''''''''' Added by Ujwala Atre  - Resolved Date Validation 
        If Not dtRecProblist Is Nothing Then
            If dtRecProblist.Rows.Count > 0 Then
                If Not getProviderTaxID(gnPatientProviderID) Then
                    _IsValidationFailed = True
                    Exit Sub
                End If
            End If
        End If
        Call SaveProblemList(True)
        ''If Arrlist.Count > 0 Then
        If Not dtRecProblist Is Nothing Then
            If dtRecProblist.Rows.Count > 0 Then
                Dim MedicalReconcilationId As Long = GetMedicalReconcillationID(_VisitID)
                If MedicalReconcilationId > 0 Then
                    Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(gnPatientProviderID)
                    oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MedicalReconcilationId, sProviderTaxID, gnPatientProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ManualReconciliationProblemList.GetHashCode())
                    oclsselectProviderTaxID = Nothing
                End If

            End If
        End If
        '' End If
    End Sub






    Private Sub mnuSendtoExam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSendtoExam.Click
        LoadUserGridExam()
        dgCustomGridselectExam.Label1.Visible = False
        dgCustomGridselectExam.txtsearch.Visible = False
        dgCustomGridselectExam.Panel2.Visible = False
        pnlSelectExam.Visible = True

        pnlSelectExam.BringToFront()
    End Sub
    Private Sub LoadUserGridExam()
        Try
            AddControlExam()
            If Not IsNothing(dgCustomGridselectExam) Then
                dgCustomGridselectExam.Visible = True
                dgCustomGridselectExam.Width = pnlSelectExam.Width
                dgCustomGridselectExam.Height = pnlSelectExam.Height
                '  dgCustomGridselectExam.txtsearch.Width = 120
                dgCustomGridselectExam.SetVisible = False
                dgCustomGridselectExam.BringToFront()
                BindUserGridExam()
                dgCustomGridselectExam.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddControlExam()

        If Not IsNothing(dgCustomGridselectExam) Then
            RemoveControlExam()
        End If
        dgCustomGridselectExam = New CustomTask
        dgCustomGridselectExam.Dock = DockStyle.Fill
        pnlSelectExam.Controls.Add(dgCustomGridselectExam)
        pnlSelectExam.BringToFront()

        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250
        ''''''''''''''''''''''

        ' pnlSelectExam.Location = New Point(500, pnlSelectExam.Location.Y)
        pnlSelectExam.Visible = True
        dgCustomGridselectExam.Visible = True
        pnlSelectExam.BringToFront()
        dgCustomGridselectExam.BringToFront()

    End Sub

    Private Sub BindUserGridExam()
        Try
            Dim dt As DataTable
            Dim objPatientDetail As New clsPatientDetails
            Dim nProblemIcdRevision As Integer = 9
            'Resolving bug no.71129:: Patient Exam - Application associates ICD9 code and ICD10 code to single exam from problem list.
            nProblemIcdRevision = nProblemICDRevForExam
            'Resolving bug no.71126::Problem List>Send to Exam> It is showing finished as well as unfinished Exam list
            dt = objPatientDetail.Fill_PastExams(_PatientID, 0, nProblemIcdRevision)
            ''

            objPatientDetail.Dispose()
            objPatientDetail = Nothing
            CustomDrugsGridStyleExam()
            If Not IsNothing(dt) Then
                Dim col As New DataColumn
                col.ColumnName = "Select"
                col.DataType = System.Type.GetType("System.Boolean")

                col.DefaultValue = CBool("False")
                dt.Columns.Add(col)
                col.Dispose()
                col = Nothing

                dgCustomGridselectExam.datasource(dt.DefaultView)
            End If

            Dim _TotalWidth As Single = dgCustomGridselectExam.C1Task.Width - 5
            dgCustomGridselectExam.C1Task.Cols.Move(dgCustomGridselectExam.C1Task.Cols.Count - 1, 0)
            dgCustomGridselectExam.C1Task.AllowEditing = True
            dgCustomGridselectExam.C1Task.Cols(0).Visible = False
            dgCustomGridselectExam.C1Task.Cols(Col_eExamID + 1).AllowEditing = True
            dgCustomGridselectExam.C1Task.Cols(Col_eExamID + 1).Width = _TotalWidth * 0
            dgCustomGridselectExam.C1Task.Cols(Col_eVistitID + 1).AllowEditing = False
            dgCustomGridselectExam.C1Task.Cols(Col_eVistitID + 1).Width = _TotalWidth * 0

            dgCustomGridselectExam.C1Task.Cols(Col_eReviewedBy + 1).AllowEditing = False
            dgCustomGridselectExam.C1Task.Cols(Col_eReviewedBy + 1).Width = _TotalWidth * 0


            dgCustomGridselectExam.C1Task.Cols(Col_eDos + 1).AllowEditing = False
            dgCustomGridselectExam.C1Task.Cols(Col_eExamName + 1).AllowEditing = False
            dgCustomGridselectExam.C1Task.Cols(Col_eTemplateName + 1).AllowEditing = False
            dgCustomGridselectExam.C1Task.Cols(Col_eFinished + 1).AllowEditing = False
            dgCustomGridselectExam.C1Task.Cols(Col_eProviderName + 1).AllowEditing = False


            dgCustomGridselectExam.C1Task.Cols("Specialty").AllowEditing = False

            '  setExamValues()
            '  UserCount = dt.Rows.Count
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Public Sub setExamValues()
    '    For j As Integer = 0 To cmbExams.Items.Count - 1
    '        cmbExams.Text = ""
    '        cmbExams.SelectedIndex = j

    '        For i As Int32 = 0 To dgCustomGridExam.C1Task.Rows.Count - 1
    '            If dgCustomGridExam.GetItem(i, Col_eExamID + 1).ToString.Trim = Convert.ToString(cmbExams.SelectedValue) Then
    '                dgCustomGridExam.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
    '            End If

    '        Next
    '    Next
    'End Sub

    Public Sub CustomDrugsGridStyleExam()

        Dim _TotalWidth As Single = dgCustomGridselectExam.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGridselectExam.C1Task
            .Redraw = False
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_eCount
            .AllowEditing = True

            .SetData(0, Col_eCheck, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_eCheck).Width = 0
            .Cols(Col_eCheck).AllowEditing = True
            .Cols(Col_eCheck).DataType = System.Type.GetType("System.Boolean")
            .Cols(Col_eCheck).Visible = False
            .SetData(0, Col_eExamID, "ExamID")
            .Cols(Col_eExamID).Width = _TotalWidth * 0.0
            .Cols(Col_eExamID).Visible = False
            .Cols(Col_eExamID).AllowEditing = False

            .SetData(0, Col_eVistitID, "VisitID")
            .Cols(Col_eVistitID).Width = _TotalWidth * 0
            .Cols(Col_eVistitID).AllowEditing = False
            .Cols(Col_eVistitID).Visible = False

            .SetData(0, Col_eDos, "DOS")
            .Cols(Col_eDos).Width = _TotalWidth * 0.45
            .Cols(Col_eDos).AllowEditing = False


            .SetData(0, Col_eExamName, "Exam Name")
            .Cols(Col_eExamName).Width = _TotalWidth * 0.45
            .Cols(Col_eExamName).AllowEditing = False


            .SetData(0, Col_eTemplateName, "Template Name")
            .Cols(Col_eTemplateName).Width = _TotalWidth * 0.45
            .Cols(Col_eTemplateName).AllowEditing = False


            .SetData(0, Col_eFinished, "Finished")
            .Cols(Col_eFinished).Width = _TotalWidth * 0.45
            .Cols(Col_eFinished).AllowEditing = False


            .SetData(0, Col_eProviderName, "Provider Name")
            .Cols(Col_eProviderName).Width = _TotalWidth * 0.45
            .Cols(Col_eProviderName).AllowEditing = False


            .SetData(0, Col_eReviewedBy, "ReviewedBy")
            .Cols(Col_eReviewedBy).Width = _TotalWidth * 0
            .Cols(Col_eReviewedBy).AllowEditing = False
            .Cols(Col_eReviewedBy).Visible = False
            .Redraw = True

        End With

    End Sub

    Private Sub dgCustomGridselectExam_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectExam.CloseClick
        dgCustomGridselectExam.Visible = False
        pnlSelectExam.Visible = False

    End Sub

    Private Sub dgCustomGridselectExam_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectExam.OKClick
        Try

            'Dim dtExam As New DataTable
            'dtExam.TableName = "Exam"
            'Dim Row1 As DataRow

            'dtExam.Columns.Add("ExamID")
            'dtExam.Columns.Add("VisitID")
            'dtExam.Columns.Add("eExamName")


            'For i As Int32 = 0 To dgCustomGridselectExam.C1Task.Rows.Count - 1

            '    If dgCustomGridselectExam.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

            '        Dim dr As DataRow = dtExam.NewRow()

            '        dr(0) = dgCustomGridselectExam.C1Task.GetData(i, Col_eExamID + 1).ToString()

            '        dr(1) = dgCustomGridselectExam.C1Task.GetData(i, Col_eVistitID + 1).ToString()
            '        dr(2) = dgCustomGridselectExam.C1Task.GetData(i, Col_eExamName + 1).ToString()

            '        dtExam.Rows.Add(dr)

            '    End If
            'Next

            'If dtExam.Rows.Count > 0 Then

            'End If
            'oclsProblemList = New clsPatientProblemList
            Dim ICD9Code As String = ""
            Dim ICD9Desc As String = ""
            Dim SnomedCode As String = ""
            Dim SnomedDesc As String = ""
            Dim IcdRevision As String = ""
            Dim ProblemId As String = ""
            Dim ICD9() As String
            Dim _ExamID As String
            Dim _ExamVisitID As String
            _ExamID = Convert.ToString(dgCustomGridselectExam.C1Task.GetData(dgCustomGridselectExam.C1Task.Row, Col_eExamID + 1))
            _ExamVisitID = Convert.ToString(dgCustomGridselectExam.C1Task.GetData(dgCustomGridselectExam.C1Task.Row, Col_eVistitID + 1))
            IcdRevision = Convert.ToString(c1ProblemList.GetData(c1ProblemList.Row, Col_ICDRevision))
            ICD9 = Convert.ToString(c1ProblemList.GetData(c1ProblemList.Row, COL_DIAGNOSIS)).Trim.Split("-")
            If ICD9.Length > 2 Then
                Dim subStr As String = Trim(Convert.ToString(c1ProblemList.GetData(c1ProblemList.Row, COL_DIAGNOSIS)))
                ICD9Code = subStr.Substring(0, subStr.IndexOf("-"))
                ICD9Desc = subStr.Substring(subStr.IndexOf("-") + 1, (subStr.Length - ICD9Code.Length) - 1)
            Else
                If ICD9.Length > 1 Then
                    ICD9Code = Convert.ToString(ICD9.GetValue(0))
                    ICD9Desc = Convert.ToString(ICD9.GetValue(1))
                End If
            End If
            SnomedCode = Convert.ToString(c1ProblemList.GetData(c1ProblemList.Row, COL_ConceptID)).Trim
            SnomedDesc = Convert.ToString(c1ProblemList.GetData(c1ProblemList.Row, COL_ProblemType)).Trim
            ProblemId = Convert.ToString(c1ProblemList.GetData(c1ProblemList.Row, COL_PROBLEMID))
            If dgCustomGridselectExam.C1Task.Rows.Count > 0 Then
                If dgCustomGridselectExam.C1Task.Row > 0 Then
                    'Resolving bug no.71129:: Patient Exam - Application associates ICD9 code and ICD10 code to single exam from problem list.
                    'Problem Not updating ExamId when Problem Send To Exam
                    Dim oclsProblemList As clsPatientProblemList
                    oclsProblemList = New clsPatientProblemList
                    If oclsProblemList.SaveExamICD9Snomed(_PatientID, _ExamVisitID, ICD9Code, ICD9Desc, SnomedCode, SnomedDesc, _ExamID, IcdRevision) Then
                        Dim Conn As New SqlConnection(GetConnectionString)
                        Dim cmd As SqlCommand = Nothing
                        Dim strQRY As String = ""
                        Try
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            strQRY = "Update ProblemList Set sExamId ='" & Convert.ToString(_ExamID) & "' where nPatientID=" & _PatientID & " and nProblemID=" & ProblemId & ""
                            cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            If Not IsNothing(cmd) Then
                                cmd.Dispose()
                                cmd = Nothing
                            End If
                            If Conn.State = ConnectionState.Open Then
                                Conn.Close()
                            End If
                            If Not IsNothing(Conn) Then
                                Conn.Dispose()
                                Conn = Nothing
                            End If
                            strQRY = Nothing
                        End Try
                    End If
                    oclsProblemList.Dispose()
                    oclsProblemList = Nothing
                End If
            End If

        Catch ex As Exception

        Finally
            pnlSelectExam.Visible = False
        End Try
    End Sub

    Private Sub mnuMedlineInfobutton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMedlineInfobutton.Click
        ''Display Online Information Document
        'Patient Education from Online Resource like NLM
        Dim ICDCodeAndDescription As String = Convert.ToString(c1ProblemList.GetData(c1ProblemList.Row, COL_DIAGNOSIS))
        Dim value As Int16
        Dim strIcd9Code As String = ""
        Dim sDescription As String = ""
        If ICDCodeAndDescription <> "" Then
            value = ICDCodeAndDescription.IndexOf("-")
            strIcd9Code = ICDCodeAndDescription.Remove(value)
            sDescription = ICDCodeAndDescription.Remove(0, value + 1)
        End If
        Dim strSnomedctCode As String = Convert.ToString(c1ProblemList.GetData(c1ProblemList.Row, COL_ConceptID))

        Dim vId As Long = 0
        'If _VisitID = 0 Then
        '    vId = GenerateVisitID(_PatientID)
        'Else
        '    vId = _VisitID
        'End If
        vId = GenerateVisitID(_PatientID)
        'If _VisitID = 0 Then
        '    vId = GenerateVisitID(_PatientID)
        'Else
        '    vId = _VisitID
        'End If

        Dim patientAgeDetail As gloUserControlLibrary.AgeDetail = gloUC_PatientStrip1.PatientAge
        Dim sAgeUnit As String = ""
        Dim sAgeValue As String = ""

        If patientAgeDetail.Years <> 0 Then
            sAgeUnit = "a"
            sAgeValue = patientAgeDetail.Years
        ElseIf patientAgeDetail.Months <> 0 Then
            sAgeUnit = "mo"
            sAgeValue = patientAgeDetail.Months
        ElseIf patientAgeDetail.Days <> 0 Then
            sAgeUnit = "d"
            sAgeValue = patientAgeDetail.Days
        End If


        If strSnomedctCode = "" And strIcd9Code = "" Then
            MessageBox.Show("Code not avilable for selected problem", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If _ISSmonedCodeMandatory Then
                If strSnomedctCode <> "" Then
                    'clsinfobutton_Problemlist.Openinfosource(strSnomedctCode, "2.16.840.1.113883.6.96", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                    clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strSnomedctCode, "2.16.840.1.113883.6.96", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                Else
                    If nICDRevision = 9 Then
                        'clsinfobutton_Problemlist.Openinfosource(strIcd9Code, "2.16.840.1.113883.6.103", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                        clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strIcd9Code, "2.16.840.1.113883.6.103", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                    ElseIf nICDRevision = 10 Then  ''added for ICD10 implementation                        
                        'clsinfobutton_Problemlist.Openinfosource(strIcd9Code, "2.16.840.1.113883.6.90", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                        clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strIcd9Code, "2.16.840.1.113883.6.90", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                    End If
                End If
            Else
                If strIcd9Code <> "" Then
                    'clsinfobutton_Problemlist.Openinfosource(strIcd9Code, "2.16.840.1.113883.6.103", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId)
                    If nICDRevision = 9 Then
                        'clsinfobutton_Problemlist.Openinfosource(strIcd9Code, "2.16.840.1.113883.6.103", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                        clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strIcd9Code, "2.16.840.1.113883.6.103", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                    ElseIf nICDRevision = 10 Then  ''added for ICD10 implementation
                        'clsinfobutton_Problemlist.Openinfosource(strIcd9Code, "2.16.840.1.113883.6.90", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                        clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strIcd9Code, "2.16.840.1.113883.6.90", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                    End If
                ElseIf strSnomedctCode <> "" Then
                    'clsinfobutton_Problemlist.Openinfosource(strSnomedctCode, "2.16.840.1.113883.6.96", strPatientLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, vId, sAgeValue, sAgeUnit, gloUC_PatientStrip1.PatientGender, gnLoginProviderID)
                    clsinfobutton_Problemlist.GetEducationMaterial_OpenInfobutton(False, gloUC_PatientStrip1.PatientGender, False, sAgeUnit, sAgeValue, strPatientLanguage, strSnomedctCode, "2.16.840.1.113883.6.96", "", "Provider", gnLoginProviderID, _PatientID, vId, Me)
                End If
            End If
        End If
    End Sub
    '21-Apr-15 Aniket: Commented to resolve Bug #82461: 00000908: Problem List shows ALL problems
    'Private Sub c1ProblemList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1ProblemList.Resize
    '    If PrevGridWidth < c1ProblemList.Width And c1ProblemList.Width > 1000 Then
    '        Dim objProblemList As New clsPatientProblemList
    '        dtProblems = objProblemList.Get_PatientProblemList(_PatientID).Copy()
    '        objProblemList.Dispose()
    '        objProblemList = Nothing
    '        If IsNothing(dtProblems) = False Then
    '            Call SetGridStyle(dtProblems)
    '            btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
    '            btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center
    '            dtProblems.Dispose()
    '            dtProblems = Nothing
    '        End If
    '    End If
    'End Sub

    Private Sub SaveProblemMedicationReconcillation() ''MedicationReconcillation for Problem list(2015 Certification)
        Try
            If Not IsNothing(dtRecProblist) Then
                If dtRecProblist.Rows.Count > 0 Then

                    dtRecProblist.Rows(0)("PatientID") = _PatientID


                    dtRecProblist.Rows(0)("VisitID") = _VisitID
                    dtRecProblist.AcceptChanges()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub







    Private Sub tlb_ProbReconciliation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_ProbReconciliation.Click
        Dim frmMedRec As frmMedReconciliation = Nothing

        Try
            frmMedRec = New frmMedReconciliation(_PatientID)
            frmMedRec.dtMed = dtRecProblist
            frmMedRec.Reconcialtiontype = 1
            If frmMedRec.Reconcialtiontype = 1 Then

                frmMedRec.Text = "Problem Reconciliation"
                frmMedRec.ShowDialog(IIf(IsNothing(frmMedRec.Parent), Me, frmMedRec.Parent))
                blnmedRecupdated = frmMedRec.RecUpdated
                dtRecProblist = frmMedRec.dtMed
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(frmMedRec) Then
                frmMedRec.Dispose()
                frmMedRec = Nothing
            End If

        End Try

    End Sub

    Private Sub tlb_NKProblems_Click(sender As System.Object, e As System.EventArgs) Handles tlb_NKProblems.Click
        Dim PatspecCDAspc As gloCCDLibrary.frmPatspecCDAspc
        PatspecCDAspc = New gloCCDLibrary.frmPatspecCDAspc()
        PatspecCDAspc.OpenfrmProblem = True
        PatspecCDAspc.patientID = _PatientID
        PatspecCDAspc.ShowDialog(Me)
        PatspecCDAspc.Dispose()
        PatspecCDAspc = Nothing
    End Sub

    Private Sub frmProblemList_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        SetNKProbVisibility()
        
    End Sub

   
    Private Sub tblRecHistory_Click(sender As System.Object, e As System.EventArgs) Handles tblRecHistory.Click
        Dim objrechist As New FrmReconsile_History
        objrechist.PatientID = _PatientID
        objrechist.RecType = 1  ''Problemlist
        objrechist.ShowDialog(Me)
        objrechist.Dispose()
        objrechist = Nothing
    End Sub
End Class
