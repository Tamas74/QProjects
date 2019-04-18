Imports oOffice = Microsoft.Office.Core
Imports System.IO
Imports Wd = Microsoft.Office.Interop.Word
Imports C1.Win.C1FlexGrid
Imports gloEMR.gloEMRWord
Imports System.Runtime.InteropServices
Imports gloTaskMail
Imports System.Threading


Public Class frm_LM_Orders

    Inherits System.Windows.Forms.Form
    Implements ISignature
    Implements IHotKey
    Implements gloVoice
    Implements IPatientContext


    'Added by kanchan on 20100618 for SamrtOrder
    Public Delegate Sub SaveOrderHandler()
    Public Event EvnSaveOrderHandler As SaveOrderHandler

    'Voice Code added by ttSupriya 29/05/2008
    Private ogloVoice As ClsVoice

    Private bIsNewOrder As Boolean = False
    'Addendum user control defined here 
    Private blnIsAddendum As Boolean
    Private WithEvents ouctlgloUC_Addendum As gloUC_Addendum

    Dim objclsPatientOrders As New clsLM_Orders
    Public Shared blnModify As Boolean  ' Sets on Order Open in Modifyed Mode      
    Dim WithEvents oViewDocument As gloEDocumentV3.gloEDocV3Management
    Public blnChangesMade As Boolean  ' Sets if Changes Made In Orders 
    Private WithEvents dgCustomGrid As CustomTask
    Private Referralcount As Int16
    Private ImagePath As String
    '' Used By Supriya
    Private LoginId As Int64
    Private ObjTasksDBLayer As New ClsTasksDBLayer
    Dim ObjWord As clsWordDocument
    Dim objCriteria As DocCriteria

    Dim TaskID As Long
    ''

    Dim BtnText As String
    Dim trvSearchNode As myTreeNode

    '''''</Mahesh>
    Public _VisitID As Long
    Public _VisitDate As Date
    Dim _ProviderID As Long
    Dim _ProviderName As String

    '' TO Store the Diagnosis of Visit
    Dim strDia As String = " "

    Dim _OpenfromMainGrid As Int32 = 0

    '' to Record Level Lock
    Dim _blnRecordLock As Boolean

    '' To Keep Task Notes
    Public Shared _strTasksNotes As String

    '' To  keep Addendum of Finished Order
    Public Shared strAddendum As String

    '' To Insert Signature
    Dim blnSaved As Boolean
    Private WithEvents oCurDoc As Wd.Document
    ' Private WithEvents oTempDoc As Wd.Document
    Dim myidx As Int32
    Dim ArrLst As New ArrayList
    Public myCaller As frmPatientExam
    'varaiable added by dipak 200919 and used for liquid data for other forms
    Public myCaller1 As Object
    Public Shared IsopenfrmTask As Boolean = False
    'Boolean variable to check that, form is open from Main form or from Patient Exam
    'This variable is used for voice purpose
    Public blnOpenFromExam As Boolean = False
    Private WithEvents oWordApp As Wd.Application
    Public blnIsFinish As Boolean = False
    Public Event On_OrderClose()
    ''Added to for Audittrail purpose
    Private _IsOrderDeleted As Boolean = False
    Private _IsGroupDeleted As Boolean = False
    Private _IsTestDeleted As Boolean = False
    Private _IsFormClose As Boolean = False
    Public Shared IsOpen As Boolean = False
    Dim blnSignClick As Boolean = False

    '10-May-13 Aniket: Resolving Memory Leaks
    Private toolTipClearUsers As New System.Windows.Forms.ToolTip
    Private toolTipSelectUser As New System.Windows.Forms.ToolTip
    Private tooltipTaskNotes As New System.Windows.Forms.ToolTip

    '16-May-13 Aniket: Resolving Memory Leaks
    Private dtUsers As DataTable
    Private bnlIsFaxOpened As Boolean
    Dim frm_diag As frm_Diagnosis = Nothing  ''added for bugid 91852
    Dim oTreatment As frm_Treatment = Nothing
#Region " C1 Constants "
    Private Const COLUM_NAME = 0
    Private Const COLUM_IDENTITY = 1
    Private Const COLUM_NUMVALUE = 2
    Private Const COLUM_UNIT = 3
    Private Const COLUM_ID = 4
    Private Const COLUM_TESTGROUPFLAG = 5
    Private Const COLUM_LEVELNO = 6
    Private Const COLUM_GROUPNO = 7
    Private Const COLUM_TEMPLATEID = 8
    ''''
    Private Const COLUM_DIAGNOSIS = 9
    Private Const COLUM_DIAGNOSISBUTTON = 10
    ''''
    Private Const COLUM_BUTTON = 11
    Private Const COLUM_ISFINISHED = 12
    ''''
    Private Const COLUM_DMSID = 13
    '' No of Columns

    'sarika 20090211 DICOM
    Private Const COLUM_DICOMID = 14

    ''Added Rahul for LOINC Code,Status,TextComment on 20101021.
    Private Const COLUM_STAUS = 15
    Private Const COLUM_TEXT_COMMENTS = 16
    Private Const COLUM_LOINC_ID = 17
    ''

    Private Const COLUM_COUNT = 18
#End Region
#Region "previous Width of Column"
    ''GLO2011-0015782 : One client workstation does not have diagnosis icon under orders menu
    'used when user tries to minimize width column contains dropdown list in before and after column resize event
    ''Start
    Dim prevWidth_COLUM_DIAGNOSIS As Int16
    Dim prevWidth_COLUM_STAUS As Int16
    ''End
#End Region

    Dim ofrmList As frmViewListControl
    Private oListUsers As gloListControl.gloListControl
    Private ToList As gloGeneralItem.gloItems
    Public Shared LMDICOMID As Long = 0
    Dim WithEvents oImportDICOM As frmgloDICOM
    Public Shared sDICOMIDs As String = ""
    Dim _IsExMsgshown As Boolean    '' 20091026 to fix bug 4696
    Dim strStatus As String = "Ordered|In Progress|Completed|Discontinue"
    '----
#Region " Windows Cotrols "

    'Private Const _History As String = "History"
    'Private Const _Medication As String = "Medication"
    'Private Const _Referrals As String = "Referrals"
    'Private Const _Prescription As String = "Prescription"
    'Private Const _Order As String = "Order"
    'Private Const _Diagnosis As String = "Diagnosis"
    'Private Const _Treatment As String = "Treatment"
    'Private Const _Vitals As String = "Vitals"
    'Private Const _ROS As String = "ROS"
    'Private Const _PatientEducation As String = "ExamEducation"
    'Private Const _Flowsheet As String = "Flowsheet"
    'Private Const _ProblemList As String = "ProblemList"
    'Private Const _Tasks As String = "Tasks_MST"
    'Private Const _CheifComplaints As String = "sCheifComplaints"
    'Private Const _PatientDemographics As String = "Patient"
    'Private Const _PatientGuideline As String = "PatientGuideline"
    'Private Const _Contacts As String = "Contacts_MST"
    Friend WithEvents ts_LM_Orders As gloToolStrip.gloToolStrip
    Friend WithEvents tsbtn_New As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Finish As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Delete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_ShowHide As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Export As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    '  Private Const _Narration As String = "Narration"

    Private Col_Check As Integer = 5
    Private Col_UserID As Integer = 0
    Private Col_LoginName As Integer = 1
    Private Col_Column1 As Integer = 2
    Private Col_Column2 As Integer = 3
    Private Col_ProviderID As Integer = 4

    ' Friend WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents wdOrders As AxDSOFramer.AxFramerControl
    Private Col_Count As Integer = 6
    Private WithEvents tlsOrders As WordToolStrip.gloWordToolStrip
    Dim _IsOrderPresent As Boolean = False
    Friend WithEvents lblDueDate As System.Windows.Forms.Label
    Friend WithEvents pnlDueDate As System.Windows.Forms.Panel
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Dim _IsNewOrder As Boolean = False
    '_patientID make share as need to use in shared function.
    'Shared _patientID As Long
    Public _patientID As Long
    Public _ArrRadi As New ArrayList
    Public _ExamID As Long
    Friend WithEvents mnuScanDocument As System.Windows.Forms.MenuItem
    Public ArrNotsavedRadioTest As New ArrayList
    Friend WithEvents pnlc1Grid As System.Windows.Forms.Panel
    Friend WithEvents C1OrderDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Private _IsOpenForView As Boolean = False
    Dim OrderDate As DateTime
    Private TestID As Int64
    Private TestName As String
    Friend WithEvents pnlToolstrip As System.Windows.Forms.Panel
    Friend WithEvents pnlTaskUseLst As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tls_LM_Orders As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtn_OK As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtn_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents pnl_btnLabs As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents pnl_btnRadiology As System.Windows.Forms.Panel
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnl_wdOrders As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents pnl_lblHeading As System.Windows.Forms.Panel
    Friend WithEvents pnl_C1OrderDetails As System.Windows.Forms.Panel
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents pnl_Header As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents mnuDICOM As System.Windows.Forms.MenuItem
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents txtSearchTest As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Cmnu_DeleteTestOrder As System.Windows.Forms.ContextMenu
    Friend WithEvents mnu_DeleteTestOrder As System.Windows.Forms.MenuItem
    Friend WithEvents Cmnu_ExpClps As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Splitter4 As System.Windows.Forms.Splitter
    Friend WithEvents pnlGloUC_TemplateTreeControl As System.Windows.Forms.Panel
    Friend WithEvents GloUC_TemplateTreeControl_Orders As gloUserControlLibrary.gloUC_TemplateTreeControl
    Friend WithEvents pnl_cmdPastExam As System.Windows.Forms.Panel
    Friend WithEvents cmdPastExam As System.Windows.Forms.Button
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents Label59 As System.Windows.Forms.Label
    Private WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents GloUC_AddRefreshDic1 As gloUserControlLibrary.gloUC_AddRefreshDic
    Friend WithEvents pnl_txtSearch As System.Windows.Forms.Panel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtClearSearch As System.Windows.Forms.Button
    Private WithEvents Label63 As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
#End Region

    Dim OrderId As Int64
    Enum enmPreviousOrders
        Current
        Yesterday
        LastWeek
        LastMonth
        Older
    End Enum

#Region " Windows Form Designer generated code "



    'Public Sub New()
    '    MyBase.New()
    '    'm_hotKeys = New HotKeyCollection(Me)
    '    'This call is required by the Windows Form Designer.
    '    'AddHandler Application.ThreadException, AddressOf OnUnhandledException
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Private Sub New(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal PatientID As Long, Optional ByVal OpenfromMainGrid As Int32 = 0, Optional ByVal blnRecordLock As Boolean = False, Optional ByVal IsOpenforView As Boolean = False)

        MyBase.New()
        'm_hotKeys = New HotKeyCollection(Me)
        _patientID = PatientID
        _VisitID = VisitID
        If _VisitID = 0 Then
            _VisitID = GenerateVisitID(VisitDate, PatientID)
        End If

        _VisitDate = VisitDate '' GetVisitdate(_VisitID)
        _OpenfromMainGrid = OpenfromMainGrid
        _blnRecordLock = blnRecordLock
        _IsOpenForView = IsOpenforView
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Call Set_PatientDetailStrip()
        'Add any initialization after the InitializeComponent() call

    End Sub

    'm_VisitID, gnPatientID, m_ExamID, arrPE

    Private Sub New()

        MyBase.New()
        'm_hotKeys = New HotKeyCollection(Me)
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Call Set_PatientDetailStrip()
        'Add any initialization after the InitializeComponent() call

    End Sub
#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    '' Private Shared _mu As New Mutex
    Private Shared frm As frm_LM_Orders

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)

        If Not (Me.blnDisposed) Then

            If (disposing) Then
                Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpDueDate}
                Dim cntControls() As System.Windows.Forms.Control = {dtpDueDate}
                Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {Cmnu_ExpClps}
                Dim CmpMControls() As System.Windows.Forms.ContextMenu = {ContextMenu1}

                If Not (components Is Nothing) Then
                    components.Dispose()
                End If


                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try




                If (IsNothing(dtpControls) = False) Then
                    If dtpControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    End If
                End If


                If (IsNothing(cntControls) = False) Then
                    If cntControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                    End If
                End If




                If (IsNothing(CmppControls) = False) Then
                    If CmppControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(CmppControls)
                    End If
                End If

                If (IsNothing(CmppControls) = False) Then
                    If CmppControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeContextMenuStrip(CmppControls)
                    End If
                End If



                If (IsNothing(CmpMControls) = False) Then
                    If CmpMControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpMControls)
                    End If
                End If

                If (IsNothing(CmpMControls) = False) Then
                    If CmpMControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeContextMenu(CmpMControls)
                    End If
                End If


                Try
                    If IsNothing(ToList) = False Then
                        ToList.Clear()
                        ToList.Dispose() : ToList = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(gloUC_PatientStrip1) = False) Then
                        gloUC_PatientStrip1.Dispose()
                        gloUC_PatientStrip1 = Nothing
                    End If
                Catch ex As Exception

                End Try
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.Dispose()
                    ogloVoice = Nothing
                End If
                Try
                    If (IsNothing(GloUC_TemplateTreeControl_Orders) = False) Then
                        If (IsNothing(GloUC_TemplateTreeControl_Orders.DocCriteria) = False) Then
                            DirectCast(GloUC_TemplateTreeControl_Orders.DocCriteria, DocCriteria).Dispose()
                            GloUC_TemplateTreeControl_Orders.DocCriteria = Nothing
                        End If
                    End If

                Catch

                End Try
                Try
                    If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                        DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                        GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
                    End If
                Catch

                End Try
                'frm = Nothing
            End If
            ' Release unmanaged resources. If disposing is false,
            ' only the following code is executed.

            ' Note that this is not thread safe.
            ' Another thread could start disposing the object
            ' after the managed resources are disposed,
            ' but before the disposed flag is set to true.
            ' If thread safety is necessary, it must be
            ' implemented by the client.
        End If

        frm = Nothing
        Me.blnDisposed = True

    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Shared Function GetInstance(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal PatientID As Long, Optional ByVal OpenfromMainGrid As Int16 = 0, Optional ByVal blnRecordLock As Boolean = False) As frm_LM_Orders
        '_mu.WaitOne()
        Try

            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frm_LM_Orders" Then
                    If CType(f, frm_LM_Orders)._patientID = PatientID Then
                        IsOpen = True
                        frm = f
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frm_LM_Orders(VisitID, VisitDate, PatientID, OpenfromMainGrid, blnRecordLock)
            End If

            'If frm Is Nothing Then
            '    frm = New frm_LM_Orders(VisitID, VisitDate, PatientID, OpenfromMainGrid, blnRecordLock)
            'End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function

#End Region


    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.

    Friend WithEvents trvPreviousOrders As System.Windows.Forms.TreeView
    Friend WithEvents pnlPrevOrders As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlOrderDetails As System.Windows.Forms.Panel
    Friend WithEvents cmbTasks As System.Windows.Forms.ComboBox
    Friend WithEvents btnClearTasks As System.Windows.Forms.Button
    Friend WithEvents btnTasks As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ImgOrders As System.Windows.Forms.ImageList
    Friend WithEvents btnRadiology As System.Windows.Forms.Button
    Friend WithEvents btnLabs As System.Windows.Forms.Button
    Friend WithEvents pnlOrders As System.Windows.Forms.Panel
    Friend WithEvents trvOrders As System.Windows.Forms.TreeView
    Friend WithEvents pnlOrderComments As System.Windows.Forms.Panel
    Friend WithEvents btnNotes As System.Windows.Forms.Button
    Friend WithEvents pnlOrderNotes As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents picNotesClose As System.Windows.Forms.PictureBox
    Friend WithEvents txtOrderNotes As System.Windows.Forms.TextBox
    Friend WithEvents pnlNotesText As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tmrDocProtect As System.Windows.Forms.Timer
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents C1Orders As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents txtSearchOrders As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_LM_Orders))
        Me.pnlPrevOrders = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.trvPreviousOrders = New System.Windows.Forms.TreeView()
        Me.ImgOrders = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.txtSearchTest = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlOrders = New System.Windows.Forms.Panel()
        Me.C1Orders = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.trvOrders = New System.Windows.Forms.TreeView()
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.pnl_btnLabs = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnLabs = New System.Windows.Forms.Button()
        Me.pnl_btnRadiology = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnRadiology = New System.Windows.Forms.Button()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearchOrders = New System.Windows.Forms.TextBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.mnuDelete = New System.Windows.Forms.MenuItem()
        Me.mnuScanDocument = New System.Windows.Forms.MenuItem()
        Me.mnuDICOM = New System.Windows.Forms.MenuItem()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlOrderDetails = New System.Windows.Forms.Panel()
        Me.pnlc1Grid = New System.Windows.Forms.Panel()
        Me.pnl_C1OrderDetails = New System.Windows.Forms.Panel()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.C1OrderDetails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnl_Header = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnl_txtSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtClearSearch = New System.Windows.Forms.Button()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbTasks = New System.Windows.Forms.ComboBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.btnTasks = New System.Windows.Forms.Button()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.btnClearTasks = New System.Windows.Forms.Button()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnNotes = New System.Windows.Forms.Button()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.lblDueDate = New System.Windows.Forms.Label()
        Me.pnlDueDate = New System.Windows.Forms.Panel()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.pnlTaskUseLst = New System.Windows.Forms.Panel()
        Me.pnlOrderNotes = New System.Windows.Forms.Panel()
        Me.pnlNotesText = New System.Windows.Forms.Panel()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtOrderNotes = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.picNotesClose = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.pnlToolstrip = New System.Windows.Forms.Panel()
        Me.tls_LM_Orders = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsbtn_OK = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtn_Cancel = New System.Windows.Forms.ToolStripButton()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.pnlOrderComments = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.pnl_wdOrders = New System.Windows.Forms.Panel()
        Me.wdOrders = New AxDSOFramer.AxFramerControl()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.pnl_lblHeading = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GloUC_AddRefreshDic1 = New gloUserControlLibrary.gloUC_AddRefreshDic()
        Me.lblHeading = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Splitter4 = New System.Windows.Forms.Splitter()
        Me.pnlGloUC_TemplateTreeControl = New System.Windows.Forms.Panel()
        Me.GloUC_TemplateTreeControl_Orders = New gloUserControlLibrary.gloUC_TemplateTreeControl()
        Me.pnl_cmdPastExam = New System.Windows.Forms.Panel()
        Me.cmdPastExam = New System.Windows.Forms.Button()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.tmrDocProtect = New System.Windows.Forms.Timer(Me.components)
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.ts_LM_Orders = New gloToolStrip.gloToolStrip()
        Me.tsbtn_Save = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_New = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_ShowHide = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Delete = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Export = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Finish = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Cmnu_DeleteTestOrder = New System.Windows.Forms.ContextMenu()
        Me.mnu_DeleteTestOrder = New System.Windows.Forms.MenuItem()
        Me.Cmnu_ExpClps = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.pnlPrevOrders.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLeft.SuspendLayout()
        Me.pnlOrders.SuspendLayout()
        CType(Me.C1Orders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_btnLabs.SuspendLayout()
        Me.pnl_btnRadiology.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlOrderDetails.SuspendLayout()
        Me.pnlc1Grid.SuspendLayout()
        Me.pnl_C1OrderDetails.SuspendLayout()
        CType(Me.C1OrderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_Header.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnl_txtSearch.SuspendLayout()
        Me.pnlDueDate.SuspendLayout()
        Me.pnlOrderNotes.SuspendLayout()
        Me.pnlNotesText.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.picNotesClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolstrip.SuspendLayout()
        Me.tls_LM_Orders.SuspendLayout()
        Me.pnlOrderComments.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnl_wdOrders.SuspendLayout()
        CType(Me.wdOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_lblHeading.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlGloUC_TemplateTreeControl.SuspendLayout()
        Me.pnl_cmdPastExam.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.ts_LM_Orders.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlPrevOrders
        '
        Me.pnlPrevOrders.Controls.Add(Me.Panel7)
        Me.pnlPrevOrders.Controls.Add(Me.Panel6)
        Me.pnlPrevOrders.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlPrevOrders.Location = New System.Drawing.Point(1073, 57)
        Me.pnlPrevOrders.Name = "pnlPrevOrders"
        Me.pnlPrevOrders.Size = New System.Drawing.Size(197, 817)
        Me.pnlPrevOrders.TabIndex = 4
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.lbl_pnlTop)
        Me.Panel7.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel7.Controls.Add(Me.lbl_pnlRight)
        Me.Panel7.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel7.Controls.Add(Me.trvPreviousOrders)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 26)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel7.Size = New System.Drawing.Size(197, 791)
        Me.Panel7.TabIndex = 1
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(1, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(192, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 787)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(193, 0)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 787)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(0, 787)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(194, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'trvPreviousOrders
        '
        Me.trvPreviousOrders.BackColor = System.Drawing.Color.White
        Me.trvPreviousOrders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvPreviousOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvPreviousOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvPreviousOrders.ForeColor = System.Drawing.Color.Black
        Me.trvPreviousOrders.HideSelection = False
        Me.trvPreviousOrders.ImageIndex = 3
        Me.trvPreviousOrders.ImageList = Me.ImgOrders
        Me.trvPreviousOrders.Indent = 20
        Me.trvPreviousOrders.ItemHeight = 20
        Me.trvPreviousOrders.Location = New System.Drawing.Point(0, 0)
        Me.trvPreviousOrders.Name = "trvPreviousOrders"
        Me.trvPreviousOrders.SelectedImageIndex = 3
        Me.trvPreviousOrders.ShowLines = False
        Me.trvPreviousOrders.Size = New System.Drawing.Size(194, 788)
        Me.trvPreviousOrders.TabIndex = 0
        '
        'ImgOrders
        '
        Me.ImgOrders.ImageStream = CType(resources.GetObject("ImgOrders.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgOrders.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgOrders.Images.SetKeyName(0, "")
        Me.ImgOrders.Images.SetKeyName(1, "")
        Me.ImgOrders.Images.SetKeyName(2, "")
        Me.ImgOrders.Images.SetKeyName(3, "Bullet06.ico")
        Me.ImgOrders.Images.SetKeyName(4, "Current.ico")
        Me.ImgOrders.Images.SetKeyName(5, "Current_Disable.ico")
        Me.ImgOrders.Images.SetKeyName(6, "Yesterdays.ico")
        Me.ImgOrders.Images.SetKeyName(7, "Yesterdays_Disable.ico")
        Me.ImgOrders.Images.SetKeyName(8, "Last Week.ico")
        Me.ImgOrders.Images.SetKeyName(9, "Last Week_Disable.ico")
        Me.ImgOrders.Images.SetKeyName(10, "LastMonth.ico")
        Me.ImgOrders.Images.SetKeyName(11, "LastMonth_Disable.ico")
        Me.ImgOrders.Images.SetKeyName(12, "Olders.ico")
        Me.ImgOrders.Images.SetKeyName(13, "Olders_Disable.ico")
        Me.ImgOrders.Images.SetKeyName(14, "Small Arrow.ico")
        Me.ImgOrders.Images.SetKeyName(15, "Current.ico")
        Me.ImgOrders.Images.SetKeyName(16, "Diamond.ico")
        Me.ImgOrders.Images.SetKeyName(17, "Minus.ico")
        Me.ImgOrders.Images.SetKeyName(18, "Plus.ico")
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel6.Controls.Add(Me.txtSearchTest)
        Me.Panel6.Controls.Add(Me.Label49)
        Me.Panel6.Controls.Add(Me.Label50)
        Me.Panel6.Controls.Add(Me.PictureBox2)
        Me.Panel6.Controls.Add(Me.Label51)
        Me.Panel6.Controls.Add(Me.Label52)
        Me.Panel6.Controls.Add(Me.Label53)
        Me.Panel6.Controls.Add(Me.Label54)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.ForeColor = System.Drawing.Color.Black
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel6.Size = New System.Drawing.Size(197, 26)
        Me.Panel6.TabIndex = 0
        '
        'txtSearchTest
        '
        Me.txtSearchTest.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchTest.ForeColor = System.Drawing.Color.Black
        Me.txtSearchTest.Location = New System.Drawing.Point(29, 5)
        Me.txtSearchTest.Name = "txtSearchTest"
        Me.txtSearchTest.Size = New System.Drawing.Size(164, 15)
        Me.txtSearchTest.TabIndex = 0
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.White
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label49.Location = New System.Drawing.Point(29, 1)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(164, 4)
        Me.Label49.TabIndex = 37
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.White
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label50.Location = New System.Drawing.Point(29, 20)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(164, 2)
        Me.Label50.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label51.Location = New System.Drawing.Point(1, 22)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(192, 1)
        Me.Label51.TabIndex = 35
        Me.Label51.Text = "label1"
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label52.Location = New System.Drawing.Point(1, 0)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(192, 1)
        Me.Label52.TabIndex = 36
        Me.Label52.Text = "label1"
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label53.Location = New System.Drawing.Point(0, 0)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1, 23)
        Me.Label53.TabIndex = 39
        Me.Label53.Text = "label4"
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label54.Location = New System.Drawing.Point(193, 0)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(1, 23)
        Me.Label54.TabIndex = 40
        Me.Label54.Text = "label4"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLeft.Controls.Add(Me.pnlOrders)
        Me.pnlLeft.Controls.Add(Me.pnl_btnLabs)
        Me.pnlLeft.Controls.Add(Me.pnl_btnRadiology)
        Me.pnlLeft.Controls.Add(Me.pnlSearch)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 57)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(220, 817)
        Me.pnlLeft.TabIndex = 1
        '
        'pnlOrders
        '
        Me.pnlOrders.Controls.Add(Me.C1Orders)
        Me.pnlOrders.Controls.Add(Me.trvOrders)
        Me.pnlOrders.Controls.Add(Me.Label36)
        Me.pnlOrders.Controls.Add(Me.Label37)
        Me.pnlOrders.Controls.Add(Me.Label38)
        Me.pnlOrders.Controls.Add(Me.Label39)
        Me.pnlOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOrders.Location = New System.Drawing.Point(0, 26)
        Me.pnlOrders.Name = "pnlOrders"
        Me.pnlOrders.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlOrders.Size = New System.Drawing.Size(220, 731)
        Me.pnlOrders.TabIndex = 1
        '
        'C1Orders
        '
        Me.C1Orders.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Orders.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Orders.ColumnInfo = "1,0,0,0,0,95,Columns:"
        Me.C1Orders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Orders.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.C1Orders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Orders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Orders.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1Orders.Location = New System.Drawing.Point(4, 1)
        Me.C1Orders.Name = "C1Orders"
        Me.C1Orders.Rows.Count = 1
        Me.C1Orders.Rows.DefaultSize = 19
        Me.C1Orders.Rows.Fixed = 0
        Me.C1Orders.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Orders.Size = New System.Drawing.Size(215, 671)
        Me.C1Orders.StyleInfo = resources.GetString("C1Orders.StyleInfo")
        Me.C1Orders.TabIndex = 0
        Me.C1Orders.Tree.NodeImageCollapsed = CType(resources.GetObject("C1Orders.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1Orders.Tree.NodeImageExpanded = CType(resources.GetObject("C1Orders.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'trvOrders
        '
        Me.trvOrders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvOrders.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.trvOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvOrders.ForeColor = System.Drawing.Color.Black
        Me.trvOrders.ImageIndex = 3
        Me.trvOrders.ImageList = Me.imgTreeView
        Me.trvOrders.ItemHeight = 20
        Me.trvOrders.Location = New System.Drawing.Point(4, 672)
        Me.trvOrders.Name = "trvOrders"
        Me.trvOrders.SelectedImageIndex = 3
        Me.trvOrders.ShowLines = False
        Me.trvOrders.Size = New System.Drawing.Size(215, 55)
        Me.trvOrders.TabIndex = 1
        Me.trvOrders.Visible = False
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Orders.ico")
        Me.imgTreeView.Images.SetKeyName(1, "Drugs.ico")
        Me.imgTreeView.Images.SetKeyName(2, "Radiology.ico")
        Me.imgTreeView.Images.SetKeyName(3, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(4, "Comment.ico")
        Me.imgTreeView.Images.SetKeyName(5, "")
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label36.Location = New System.Drawing.Point(4, 727)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(215, 1)
        Me.Label36.TabIndex = 12
        Me.Label36.Text = "label2"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(3, 1)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(1, 727)
        Me.Label37.TabIndex = 11
        Me.Label37.Text = "label4"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label38.Location = New System.Drawing.Point(219, 1)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 727)
        Me.Label38.TabIndex = 10
        Me.Label38.Text = "label3"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(3, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(217, 1)
        Me.Label39.TabIndex = 9
        Me.Label39.Text = "label1"
        '
        'pnl_btnLabs
        '
        Me.pnl_btnLabs.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_btnLabs.Controls.Add(Me.Label1)
        Me.pnl_btnLabs.Controls.Add(Me.Label4)
        Me.pnl_btnLabs.Controls.Add(Me.Label5)
        Me.pnl_btnLabs.Controls.Add(Me.Label7)
        Me.pnl_btnLabs.Controls.Add(Me.btnLabs)
        Me.pnl_btnLabs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnLabs.Location = New System.Drawing.Point(0, 757)
        Me.pnl_btnLabs.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_btnLabs.Name = "pnl_btnLabs"
        Me.pnl_btnLabs.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_btnLabs.Size = New System.Drawing.Size(220, 30)
        Me.pnl_btnLabs.TabIndex = 2
        Me.pnl_btnLabs.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(215, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 26)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(219, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 26)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "label3"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(217, 1)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "label1"
        '
        'btnLabs
        '
        Me.btnLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnLabs.BackgroundImage = CType(resources.GetObject("btnLabs.BackgroundImage"), System.Drawing.Image)
        Me.btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLabs.FlatAppearance.BorderSize = 0
        Me.btnLabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLabs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnLabs.Location = New System.Drawing.Point(3, 0)
        Me.btnLabs.Name = "btnLabs"
        Me.btnLabs.Size = New System.Drawing.Size(217, 27)
        Me.btnLabs.TabIndex = 0
        Me.btnLabs.Text = "Labs"
        Me.btnLabs.UseVisualStyleBackColor = False
        '
        'pnl_btnRadiology
        '
        Me.pnl_btnRadiology.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnRadiology.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_btnRadiology.Controls.Add(Me.Label8)
        Me.pnl_btnRadiology.Controls.Add(Me.Label9)
        Me.pnl_btnRadiology.Controls.Add(Me.btnRadiology)
        Me.pnl_btnRadiology.Controls.Add(Me.Label34)
        Me.pnl_btnRadiology.Controls.Add(Me.Label35)
        Me.pnl_btnRadiology.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnRadiology.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_btnRadiology.Location = New System.Drawing.Point(0, 787)
        Me.pnl_btnRadiology.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_btnRadiology.Name = "pnl_btnRadiology"
        Me.pnl_btnRadiology.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_btnRadiology.Size = New System.Drawing.Size(220, 30)
        Me.pnl_btnRadiology.TabIndex = 3
        Me.pnl_btnRadiology.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(4, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(215, 1)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 26)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "label4"
        '
        'btnRadiology
        '
        Me.btnRadiology.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnRadiology.BackgroundImage = CType(resources.GetObject("btnRadiology.BackgroundImage"), System.Drawing.Image)
        Me.btnRadiology.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRadiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRadiology.FlatAppearance.BorderSize = 0
        Me.btnRadiology.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRadiology.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRadiology.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRadiology.Location = New System.Drawing.Point(3, 1)
        Me.btnRadiology.Name = "btnRadiology"
        Me.btnRadiology.Size = New System.Drawing.Size(216, 26)
        Me.btnRadiology.TabIndex = 0
        Me.btnRadiology.Text = "Orders"
        Me.btnRadiology.UseVisualStyleBackColor = False
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label34.Location = New System.Drawing.Point(219, 1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 26)
        Me.Label34.TabIndex = 6
        Me.Label34.Text = "label3"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(3, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(217, 1)
        Me.Label35.TabIndex = 5
        Me.Label35.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.txtSearchOrders)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(220, 26)
        Me.pnlSearch.TabIndex = 0
        '
        'txtSearchOrders
        '
        Me.txtSearchOrders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchOrders.ForeColor = System.Drawing.Color.Black
        Me.txtSearchOrders.Location = New System.Drawing.Point(29, 5)
        Me.txtSearchOrders.Name = "txtSearchOrders"
        Me.txtSearchOrders.Size = New System.Drawing.Size(165, 15)
        Me.txtSearchOrders.TabIndex = 0
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(194, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(25, 15)
        Me.btnClear.TabIndex = 49
        Me.C1SuperTooltip1.SetToolTip(Me.btnClear, "Clear Search")
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(29, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(190, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(29, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(190, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(4, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(25, 21)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(4, 22)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(215, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(215, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(219, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDelete, Me.mnuScanDocument, Me.mnuDICOM})
        '
        'mnuDelete
        '
        Me.mnuDelete.Index = 0
        Me.mnuDelete.Text = "&Delete Test"
        '
        'mnuScanDocument
        '
        Me.mnuScanDocument.Index = 1
        Me.mnuScanDocument.Text = "Scan Document"
        '
        'mnuDICOM
        '
        Me.mnuDICOM.Index = 2
        Me.mnuDICOM.Text = "Import DICOM File"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(220, 57)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 817)
        Me.Splitter1.TabIndex = 12
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(1070, 57)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 817)
        Me.Splitter2.TabIndex = 13
        Me.Splitter2.TabStop = False
        '
        'pnlOrderDetails
        '
        Me.pnlOrderDetails.BackColor = System.Drawing.Color.Transparent
        Me.pnlOrderDetails.Controls.Add(Me.pnlc1Grid)
        Me.pnlOrderDetails.Controls.Add(Me.pnl_Header)
        Me.pnlOrderDetails.Controls.Add(Me.pnlTaskUseLst)
        Me.pnlOrderDetails.Controls.Add(Me.pnlOrderNotes)
        Me.pnlOrderDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlOrderDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlOrderDetails.Location = New System.Drawing.Point(223, 57)
        Me.pnlOrderDetails.Name = "pnlOrderDetails"
        Me.pnlOrderDetails.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlOrderDetails.Size = New System.Drawing.Size(847, 338)
        Me.pnlOrderDetails.TabIndex = 2
        '
        'pnlc1Grid
        '
        Me.pnlc1Grid.Controls.Add(Me.pnl_C1OrderDetails)
        Me.pnlc1Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlc1Grid.Location = New System.Drawing.Point(0, 30)
        Me.pnlc1Grid.Name = "pnlc1Grid"
        Me.pnlc1Grid.Size = New System.Drawing.Size(847, 305)
        Me.pnlc1Grid.TabIndex = 0
        '
        'pnl_C1OrderDetails
        '
        Me.pnl_C1OrderDetails.Controls.Add(Me.Label45)
        Me.pnl_C1OrderDetails.Controls.Add(Me.Label46)
        Me.pnl_C1OrderDetails.Controls.Add(Me.Label47)
        Me.pnl_C1OrderDetails.Controls.Add(Me.Label48)
        Me.pnl_C1OrderDetails.Controls.Add(Me.C1OrderDetails)
        Me.pnl_C1OrderDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_C1OrderDetails.Location = New System.Drawing.Point(0, 0)
        Me.pnl_C1OrderDetails.Name = "pnl_C1OrderDetails"
        Me.pnl_C1OrderDetails.Size = New System.Drawing.Size(847, 305)
        Me.pnl_C1OrderDetails.TabIndex = 0
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(1, 304)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(845, 1)
        Me.Label45.TabIndex = 41
        Me.Label45.Text = "label2"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(0, 1)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 304)
        Me.Label46.TabIndex = 40
        Me.Label46.Text = "label4"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label47.Location = New System.Drawing.Point(846, 1)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 304)
        Me.Label47.TabIndex = 39
        Me.Label47.Text = "label3"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(0, 0)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(847, 1)
        Me.Label48.TabIndex = 38
        Me.Label48.Text = "label1"
        '
        'C1OrderDetails
        '
        Me.C1OrderDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1OrderDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1OrderDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1OrderDetails.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1OrderDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1OrderDetails.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.C1OrderDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1OrderDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1OrderDetails.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1OrderDetails.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1OrderDetails.Location = New System.Drawing.Point(0, 0)
        Me.C1OrderDetails.Name = "C1OrderDetails"
        Me.C1OrderDetails.Rows.Count = 1
        Me.C1OrderDetails.Rows.DefaultSize = 19
        Me.C1OrderDetails.Rows.Fixed = 0
        Me.C1OrderDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1OrderDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1OrderDetails.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1OrderDetails.Size = New System.Drawing.Size(847, 305)
        Me.C1OrderDetails.StyleInfo = resources.GetString("C1OrderDetails.StyleInfo")
        Me.C1OrderDetails.TabIndex = 0
        Me.C1OrderDetails.Tree.NodeImageCollapsed = CType(resources.GetObject("C1OrderDetails.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1OrderDetails.Tree.NodeImageExpanded = CType(resources.GetObject("C1OrderDetails.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'pnl_Header
        '
        Me.pnl_Header.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_Header.Controls.Add(Me.Panel3)
        Me.pnl_Header.Controls.Add(Me.Label19)
        Me.pnl_Header.Controls.Add(Me.Label17)
        Me.pnl_Header.Controls.Add(Me.Label18)
        Me.pnl_Header.Controls.Add(Me.Label20)
        Me.pnl_Header.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Header.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Header.Location = New System.Drawing.Point(0, 0)
        Me.pnl_Header.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_Header.Name = "pnl_Header"
        Me.pnl_Header.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnl_Header.Size = New System.Drawing.Size(847, 30)
        Me.pnl_Header.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.pnl_txtSearch)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.cmbTasks)
        Me.Panel3.Controls.Add(Me.Label56)
        Me.Panel3.Controls.Add(Me.btnTasks)
        Me.Panel3.Controls.Add(Me.Label55)
        Me.Panel3.Controls.Add(Me.btnClearTasks)
        Me.Panel3.Controls.Add(Me.Label27)
        Me.Panel3.Controls.Add(Me.btnNotes)
        Me.Panel3.Controls.Add(Me.Label33)
        Me.Panel3.Controls.Add(Me.lblDueDate)
        Me.Panel3.Controls.Add(Me.pnlDueDate)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(1, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(845, 22)
        Me.Panel3.TabIndex = 31
        '
        'pnl_txtSearch
        '
        Me.pnl_txtSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnl_txtSearch.Controls.Add(Me.txtSearch)
        Me.pnl_txtSearch.Controls.Add(Me.Label77)
        Me.pnl_txtSearch.Controls.Add(Me.Label61)
        Me.pnl_txtSearch.Controls.Add(Me.Label62)
        Me.pnl_txtSearch.Controls.Add(Me.txtClearSearch)
        Me.pnl_txtSearch.Controls.Add(Me.Label63)
        Me.pnl_txtSearch.Controls.Add(Me.Label64)
        Me.pnl_txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_txtSearch.ForeColor = System.Drawing.Color.Black
        Me.pnl_txtSearch.Location = New System.Drawing.Point(59, 0)
        Me.pnl_txtSearch.Name = "pnl_txtSearch"
        Me.pnl_txtSearch.Size = New System.Drawing.Size(241, 22)
        Me.pnl_txtSearch.TabIndex = 50
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(212, 15)
        Me.txtSearch.TabIndex = 5
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Location = New System.Drawing.Point(5, 17)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(212, 5)
        Me.Label77.TabIndex = 43
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.White
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label61.Location = New System.Drawing.Point(5, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(212, 3)
        Me.Label61.TabIndex = 37
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.White
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label62.Location = New System.Drawing.Point(1, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(4, 22)
        Me.Label62.TabIndex = 38
        '
        'txtClearSearch
        '
        Me.txtClearSearch.BackgroundImage = CType(resources.GetObject("txtClearSearch.BackgroundImage"), System.Drawing.Image)
        Me.txtClearSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.txtClearSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.txtClearSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.txtClearSearch.FlatAppearance.BorderSize = 0
        Me.txtClearSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.txtClearSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.txtClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.txtClearSearch.Image = CType(resources.GetObject("txtClearSearch.Image"), System.Drawing.Image)
        Me.txtClearSearch.Location = New System.Drawing.Point(217, 0)
        Me.txtClearSearch.Name = "txtClearSearch"
        Me.txtClearSearch.Size = New System.Drawing.Size(23, 22)
        Me.txtClearSearch.TabIndex = 41
        Me.txtClearSearch.UseVisualStyleBackColor = True
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label63.Location = New System.Drawing.Point(0, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 22)
        Me.Label63.TabIndex = 39
        Me.Label63.Text = "label4"
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label64.Location = New System.Drawing.Point(240, 0)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 22)
        Me.Label64.TabIndex = 40
        Me.Label64.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 22)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = " Search :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(318, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 22)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = " Select Users :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbTasks
        '
        Me.cmbTasks.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmbTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTasks.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTasks.ForeColor = System.Drawing.Color.Black
        Me.cmbTasks.Location = New System.Drawing.Point(413, 0)
        Me.cmbTasks.Name = "cmbTasks"
        Me.cmbTasks.Size = New System.Drawing.Size(158, 22)
        Me.cmbTasks.TabIndex = 0
        '
        'Label56
        '
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(571, 0)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(5, 22)
        Me.Label56.TabIndex = 43
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnTasks
        '
        Me.btnTasks.BackgroundImage = CType(resources.GetObject("btnTasks.BackgroundImage"), System.Drawing.Image)
        Me.btnTasks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTasks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTasks.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnTasks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTasks.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnTasks.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTasks.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTasks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTasks.Image = CType(resources.GetObject("btnTasks.Image"), System.Drawing.Image)
        Me.btnTasks.Location = New System.Drawing.Point(576, 0)
        Me.btnTasks.Name = "btnTasks"
        Me.btnTasks.Size = New System.Drawing.Size(24, 22)
        Me.btnTasks.TabIndex = 2
        '
        'Label55
        '
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(600, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(4, 22)
        Me.Label55.TabIndex = 42
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClearTasks
        '
        Me.btnClearTasks.BackgroundImage = CType(resources.GetObject("btnClearTasks.BackgroundImage"), System.Drawing.Image)
        Me.btnClearTasks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearTasks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearTasks.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearTasks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearTasks.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearTasks.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearTasks.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearTasks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearTasks.Image = CType(resources.GetObject("btnClearTasks.Image"), System.Drawing.Image)
        Me.btnClearTasks.Location = New System.Drawing.Point(604, 0)
        Me.btnClearTasks.Name = "btnClearTasks"
        Me.btnClearTasks.Size = New System.Drawing.Size(24, 22)
        Me.btnClearTasks.TabIndex = 1
        '
        'Label27
        '
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(628, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(8, 22)
        Me.Label27.TabIndex = 40
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnNotes
        '
        Me.btnNotes.BackgroundImage = CType(resources.GetObject("btnNotes.BackgroundImage"), System.Drawing.Image)
        Me.btnNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNotes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNotes.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnNotes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnNotes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnNotes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNotes.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnNotes.Image = CType(resources.GetObject("btnNotes.Image"), System.Drawing.Image)
        Me.btnNotes.Location = New System.Drawing.Point(636, 0)
        Me.btnNotes.Name = "btnNotes"
        Me.btnNotes.Size = New System.Drawing.Size(24, 22)
        Me.btnNotes.TabIndex = 3
        '
        'Label33
        '
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(660, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(4, 22)
        Me.Label33.TabIndex = 41
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDueDate
        '
        Me.lblDueDate.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblDueDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDueDate.Location = New System.Drawing.Point(664, 0)
        Me.lblDueDate.Name = "lblDueDate"
        Me.lblDueDate.Size = New System.Drawing.Size(73, 22)
        Me.lblDueDate.TabIndex = 38
        Me.lblDueDate.Text = "Due Date :"
        Me.lblDueDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDueDate
        '
        Me.pnlDueDate.Controls.Add(Me.dtpDueDate)
        Me.pnlDueDate.Controls.Add(Me.Label15)
        Me.pnlDueDate.Controls.Add(Me.Label14)
        Me.pnlDueDate.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDueDate.Location = New System.Drawing.Point(737, 0)
        Me.pnlDueDate.Name = "pnlDueDate"
        Me.pnlDueDate.Size = New System.Drawing.Size(103, 22)
        Me.pnlDueDate.TabIndex = 37
        '
        'dtpDueDate
        '
        Me.dtpDueDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpDueDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpDueDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDueDate.Location = New System.Drawing.Point(0, -1)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(99, 22)
        Me.dtpDueDate.TabIndex = 4
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Location = New System.Drawing.Point(0, 20)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(103, 2)
        Me.Label15.TabIndex = 39
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(103, 2)
        Me.Label14.TabIndex = 38
        '
        'Label23
        '
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(840, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(5, 22)
        Me.Label23.TabIndex = 39
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(846, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 22)
        Me.Label19.TabIndex = 32
        Me.Label19.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(1, 26)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(846, 1)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 23)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(847, 1)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "label1"
        '
        'pnlTaskUseLst
        '
        Me.pnlTaskUseLst.Location = New System.Drawing.Point(5, 6)
        Me.pnlTaskUseLst.Name = "pnlTaskUseLst"
        Me.pnlTaskUseLst.Size = New System.Drawing.Size(470, 266)
        Me.pnlTaskUseLst.TabIndex = 39
        Me.pnlTaskUseLst.Visible = False
        '
        'pnlOrderNotes
        '
        Me.pnlOrderNotes.BackColor = System.Drawing.Color.Transparent
        Me.pnlOrderNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlOrderNotes.Controls.Add(Me.pnlNotesText)
        Me.pnlOrderNotes.Controls.Add(Me.Panel1)
        Me.pnlOrderNotes.Controls.Add(Me.pnlToolstrip)
        Me.pnlOrderNotes.Location = New System.Drawing.Point(700, 110)
        Me.pnlOrderNotes.Name = "pnlOrderNotes"
        Me.pnlOrderNotes.Size = New System.Drawing.Size(330, 200)
        Me.pnlOrderNotes.TabIndex = 1
        '
        'pnlNotesText
        '
        Me.pnlNotesText.BackColor = System.Drawing.Color.Transparent
        Me.pnlNotesText.Controls.Add(Me.Label31)
        Me.pnlNotesText.Controls.Add(Me.txtOrderNotes)
        Me.pnlNotesText.Controls.Add(Me.Label42)
        Me.pnlNotesText.Controls.Add(Me.Label43)
        Me.pnlNotesText.Controls.Add(Me.Label44)
        Me.pnlNotesText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNotesText.Location = New System.Drawing.Point(0, 89)
        Me.pnlNotesText.Name = "pnlNotesText"
        Me.pnlNotesText.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnlNotesText.Size = New System.Drawing.Size(330, 111)
        Me.pnlNotesText.TabIndex = 33
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(4, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(322, 1)
        Me.Label31.TabIndex = 46
        Me.Label31.Text = "label2"
        '
        'txtOrderNotes
        '
        Me.txtOrderNotes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOrderNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOrderNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtOrderNotes.Location = New System.Drawing.Point(4, 1)
        Me.txtOrderNotes.MaxLength = 1000
        Me.txtOrderNotes.Multiline = True
        Me.txtOrderNotes.Name = "txtOrderNotes"
        Me.txtOrderNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOrderNotes.Size = New System.Drawing.Size(322, 106)
        Me.txtOrderNotes.TabIndex = 0
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label42.Location = New System.Drawing.Point(4, 107)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(322, 1)
        Me.Label42.TabIndex = 45
        Me.Label42.Text = "label2"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(3, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 107)
        Me.Label43.TabIndex = 44
        Me.Label43.Text = "label4"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label44.Location = New System.Drawing.Point(326, 1)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 107)
        Me.Label44.TabIndex = 43
        Me.Label44.Text = "label3"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Label41)
        Me.Panel1.Controls.Add(Me.Label40)
        Me.Panel1.Controls.Add(Me.Label32)
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(0, 59)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(330, 30)
        Me.Panel1.TabIndex = 32
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.picNotesClose)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(4, 4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(322, 22)
        Me.Panel5.TabIndex = 45
        '
        'picNotesClose
        '
        Me.picNotesClose.BackColor = System.Drawing.Color.Transparent
        Me.picNotesClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.picNotesClose.Image = CType(resources.GetObject("picNotesClose.Image"), System.Drawing.Image)
        Me.picNotesClose.Location = New System.Drawing.Point(299, 0)
        Me.picNotesClose.Name = "picNotesClose"
        Me.picNotesClose.Size = New System.Drawing.Size(23, 22)
        Me.picNotesClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picNotesClose.TabIndex = 0
        Me.picNotesClose.TabStop = False
        Me.picNotesClose.Visible = False
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(322, 22)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Task Notes :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label41.Location = New System.Drawing.Point(326, 4)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 22)
        Me.Label41.TabIndex = 44
        Me.Label41.Text = "label1"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label40.Location = New System.Drawing.Point(3, 4)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 22)
        Me.Label40.TabIndex = 43
        Me.Label40.Text = "label1"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label32.Location = New System.Drawing.Point(3, 26)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(324, 1)
        Me.Label32.TabIndex = 42
        Me.Label32.Text = "label1"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Location = New System.Drawing.Point(3, 3)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(324, 1)
        Me.Label30.TabIndex = 41
        Me.Label30.Text = "label1"
        '
        'pnlToolstrip
        '
        Me.pnlToolstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolstrip.Controls.Add(Me.tls_LM_Orders)
        Me.pnlToolstrip.Controls.Add(Me.Label29)
        Me.pnlToolstrip.Controls.Add(Me.Label16)
        Me.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolstrip.Name = "pnlToolstrip"
        Me.pnlToolstrip.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlToolstrip.Size = New System.Drawing.Size(330, 59)
        Me.pnlToolstrip.TabIndex = 34
        '
        'tls_LM_Orders
        '
        Me.tls_LM_Orders.BackColor = System.Drawing.Color.Transparent
        Me.tls_LM_Orders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_LM_Orders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_LM_Orders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_LM_Orders.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_LM_Orders.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_LM_Orders.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtn_OK, Me.tlsbtn_Cancel})
        Me.tls_LM_Orders.Location = New System.Drawing.Point(4, 4)
        Me.tls_LM_Orders.Name = "tls_LM_Orders"
        Me.tls_LM_Orders.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.tls_LM_Orders.Size = New System.Drawing.Size(323, 53)
        Me.tls_LM_Orders.TabIndex = 1
        Me.tls_LM_Orders.Text = "ToolStrip1"
        '
        'tlsbtn_OK
        '
        Me.tlsbtn_OK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_OK.Image = CType(resources.GetObject("tlsbtn_OK.Image"), System.Drawing.Image)
        Me.tlsbtn_OK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtn_OK.Name = "tlsbtn_OK"
        Me.tlsbtn_OK.Size = New System.Drawing.Size(66, 50)
        Me.tlsbtn_OK.Tag = "OK"
        Me.tlsbtn_OK.Text = "&Save&&Cls"
        Me.tlsbtn_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtn_OK.ToolTipText = "Save and Close"
        '
        'tlsbtn_Cancel
        '
        Me.tlsbtn_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_Cancel.Image = CType(resources.GetObject("tlsbtn_Cancel.Image"), System.Drawing.Image)
        Me.tlsbtn_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtn_Cancel.Name = "tlsbtn_Cancel"
        Me.tlsbtn_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtn_Cancel.Tag = "Cancel"
        Me.tlsbtn_Cancel.Text = "&Close"
        Me.tlsbtn_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Location = New System.Drawing.Point(4, 3)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(323, 1)
        Me.Label29.TabIndex = 45
        Me.Label29.Text = "label1"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Location = New System.Drawing.Point(3, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 53)
        Me.Label16.TabIndex = 46
        Me.Label16.Text = "label1"
        '
        'pnlOrderComments
        '
        Me.pnlOrderComments.Controls.Add(Me.Panel4)
        Me.pnlOrderComments.Controls.Add(Me.Splitter4)
        Me.pnlOrderComments.Controls.Add(Me.pnlGloUC_TemplateTreeControl)
        Me.pnlOrderComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOrderComments.Location = New System.Drawing.Point(223, 398)
        Me.pnlOrderComments.Name = "pnlOrderComments"
        Me.pnlOrderComments.Size = New System.Drawing.Size(847, 476)
        Me.pnlOrderComments.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.pnl_wdOrders)
        Me.Panel4.Controls.Add(Me.pnl_lblHeading)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(220, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(627, 476)
        Me.Panel4.TabIndex = 47
        '
        'pnl_wdOrders
        '
        Me.pnl_wdOrders.BackColor = System.Drawing.Color.Transparent
        Me.pnl_wdOrders.Controls.Add(Me.wdOrders)
        Me.pnl_wdOrders.Controls.Add(Me.Label10)
        Me.pnl_wdOrders.Controls.Add(Me.Label24)
        Me.pnl_wdOrders.Controls.Add(Me.Label25)
        Me.pnl_wdOrders.Controls.Add(Me.Label26)
        Me.pnl_wdOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_wdOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_wdOrders.Location = New System.Drawing.Point(0, 27)
        Me.pnl_wdOrders.Name = "pnl_wdOrders"
        Me.pnl_wdOrders.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnl_wdOrders.Size = New System.Drawing.Size(627, 449)
        Me.pnl_wdOrders.TabIndex = 0
        '
        'wdOrders
        '
        Me.wdOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdOrders.Enabled = True
        Me.wdOrders.Location = New System.Drawing.Point(1, 1)
        Me.wdOrders.Name = "wdOrders"
        Me.wdOrders.OcxState = CType(resources.GetObject("wdOrders.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdOrders.Size = New System.Drawing.Size(625, 444)
        Me.wdOrders.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(1, 445)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(625, 1)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "label2"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 445)
        Me.Label24.TabIndex = 11
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(626, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 445)
        Me.Label25.TabIndex = 10
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(627, 1)
        Me.Label26.TabIndex = 9
        Me.Label26.Text = "label1"
        '
        'pnl_lblHeading
        '
        Me.pnl_lblHeading.Controls.Add(Me.Panel2)
        Me.pnl_lblHeading.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_lblHeading.Location = New System.Drawing.Point(0, 0)
        Me.pnl_lblHeading.Name = "pnl_lblHeading"
        Me.pnl_lblHeading.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnl_lblHeading.Size = New System.Drawing.Size(627, 27)
        Me.pnl_lblHeading.TabIndex = 48
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.GloUC_AddRefreshDic1)
        Me.Panel2.Controls.Add(Me.lblHeading)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(627, 24)
        Me.Panel2.TabIndex = 37
        '
        'GloUC_AddRefreshDic1
        '
        Me.GloUC_AddRefreshDic1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GloUC_AddRefreshDic1.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_AddRefreshDic1.CONNECTIONSTRINGs = Nothing
        Me.GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing
        Me.GloUC_AddRefreshDic1.Location = New System.Drawing.Point(572, 2)
        Me.GloUC_AddRefreshDic1.M_PATIENTIDs = CType(0, Long)
        Me.GloUC_AddRefreshDic1.Name = "GloUC_AddRefreshDic1"
        Me.GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
        Me.GloUC_AddRefreshDic1.ObjFrom = Nothing
        Me.GloUC_AddRefreshDic1.OBJWORDs = Nothing
        Me.GloUC_AddRefreshDic1.OCURDOCs = Nothing
        Me.GloUC_AddRefreshDic1.OWORDAPPs = Nothing
        Me.GloUC_AddRefreshDic1.Size = New System.Drawing.Size(48, 20)
        Me.GloUC_AddRefreshDic1.TabIndex = 47
        Me.GloUC_AddRefreshDic1.wdPatientWordDocs = Nothing
        '
        'lblHeading
        '
        Me.lblHeading.BackColor = System.Drawing.Color.Transparent
        Me.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHeading.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblHeading.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.Location = New System.Drawing.Point(1, 1)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(625, 22)
        Me.lblHeading.TabIndex = 0
        Me.lblHeading.Tag = "  "
        Me.lblHeading.Text = "  Order Comments :"
        Me.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Location = New System.Drawing.Point(1, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(625, 1)
        Me.Label13.TabIndex = 41
        Me.Label13.Text = "label1"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Location = New System.Drawing.Point(1, 23)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(625, 1)
        Me.Label22.TabIndex = 46
        Me.Label22.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 24)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(626, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 24)
        Me.Label12.TabIndex = 42
        Me.Label12.Text = "label3"
        '
        'Splitter4
        '
        Me.Splitter4.Location = New System.Drawing.Point(217, 0)
        Me.Splitter4.Name = "Splitter4"
        Me.Splitter4.Size = New System.Drawing.Size(3, 476)
        Me.Splitter4.TabIndex = 49
        Me.Splitter4.TabStop = False
        '
        'pnlGloUC_TemplateTreeControl
        '
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.GloUC_TemplateTreeControl_Orders)
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.pnl_cmdPastExam)
        Me.pnlGloUC_TemplateTreeControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_TemplateTreeControl.Location = New System.Drawing.Point(0, 0)
        Me.pnlGloUC_TemplateTreeControl.Name = "pnlGloUC_TemplateTreeControl"
        Me.pnlGloUC_TemplateTreeControl.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGloUC_TemplateTreeControl.Size = New System.Drawing.Size(217, 476)
        Me.pnlGloUC_TemplateTreeControl.TabIndex = 48
        '
        'GloUC_TemplateTreeControl_Orders
        '
        Me.GloUC_TemplateTreeControl_Orders.DocCriteria = Nothing
        Me.GloUC_TemplateTreeControl_Orders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_TemplateTreeControl_Orders.ExpandConsent = False
        Me.GloUC_TemplateTreeControl_Orders.Location = New System.Drawing.Point(0, 27)
        Me.GloUC_TemplateTreeControl_Orders.Name = "GloUC_TemplateTreeControl_Orders"
        Me.GloUC_TemplateTreeControl_Orders.ObjClsWord = Nothing
        Me.GloUC_TemplateTreeControl_Orders.ProviderId = CType(0, Long)
        Me.GloUC_TemplateTreeControl_Orders.Size = New System.Drawing.Size(217, 446)
        Me.GloUC_TemplateTreeControl_Orders.TabIndex = 0
        '
        'pnl_cmdPastExam
        '
        Me.pnl_cmdPastExam.BackColor = System.Drawing.Color.Transparent
        Me.pnl_cmdPastExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_cmdPastExam.Controls.Add(Me.cmdPastExam)
        Me.pnl_cmdPastExam.Controls.Add(Me.Label57)
        Me.pnl_cmdPastExam.Controls.Add(Me.Label58)
        Me.pnl_cmdPastExam.Controls.Add(Me.Label59)
        Me.pnl_cmdPastExam.Controls.Add(Me.Label60)
        Me.pnl_cmdPastExam.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_cmdPastExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_cmdPastExam.Location = New System.Drawing.Point(0, 0)
        Me.pnl_cmdPastExam.Name = "pnl_cmdPastExam"
        Me.pnl_cmdPastExam.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnl_cmdPastExam.Size = New System.Drawing.Size(217, 27)
        Me.pnl_cmdPastExam.TabIndex = 1
        '
        'cmdPastExam
        '
        Me.cmdPastExam.BackColor = System.Drawing.Color.Transparent
        Me.cmdPastExam.BackgroundImage = CType(resources.GetObject("cmdPastExam.BackgroundImage"), System.Drawing.Image)
        Me.cmdPastExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdPastExam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmdPastExam.FlatAppearance.BorderSize = 0
        Me.cmdPastExam.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.cmdPastExam.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.cmdPastExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPastExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPastExam.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPastExam.Location = New System.Drawing.Point(1, 1)
        Me.cmdPastExam.Name = "cmdPastExam"
        Me.cmdPastExam.Size = New System.Drawing.Size(215, 22)
        Me.cmdPastExam.TabIndex = 3
        Me.cmdPastExam.Text = "Orders"
        Me.cmdPastExam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPastExam.UseVisualStyleBackColor = False
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label57.Location = New System.Drawing.Point(1, 23)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(215, 1)
        Me.Label57.TabIndex = 12
        Me.Label57.Text = "label2"
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(0, 1)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(1, 23)
        Me.Label58.TabIndex = 11
        Me.Label58.Text = "label4"
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label59.Location = New System.Drawing.Point(216, 1)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(1, 23)
        Me.Label59.TabIndex = 10
        Me.Label59.Text = "label3"
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(0, 0)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(217, 1)
        Me.Label60.TabIndex = 9
        Me.Label60.Text = "label1"
        '
        'tmrDocProtect
        '
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBottom.Controls.Add(Me.ts_LM_Orders)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBottom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlBottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1270, 57)
        Me.pnlBottom.TabIndex = 0
        '
        'ts_LM_Orders
        '
        Me.ts_LM_Orders.AddSeparatorsBetweenEachButton = False
        Me.ts_LM_Orders.BackColor = System.Drawing.Color.Transparent
        Me.ts_LM_Orders.BackgroundImage = CType(resources.GetObject("ts_LM_Orders.BackgroundImage"), System.Drawing.Image)
        Me.ts_LM_Orders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_LM_Orders.ButtonsToHide = CType(resources.GetObject("ts_LM_Orders.ButtonsToHide"), System.Collections.ArrayList)
        Me.ts_LM_Orders.ConnectionString = Nothing
        Me.ts_LM_Orders.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.ts_LM_Orders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_LM_Orders.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_LM_Orders.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_LM_Orders.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtn_Save, Me.tsbtn_New, Me.tsbtn_ShowHide, Me.tsbtn_Delete, Me.tsbtn_Export, Me.tsbtn_Finish, Me.tsbtn_Close})
        Me.ts_LM_Orders.Location = New System.Drawing.Point(0, 0)
        Me.ts_LM_Orders.ModuleName = Nothing
        Me.ts_LM_Orders.Name = "ts_LM_Orders"
        Me.ts_LM_Orders.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ts_LM_Orders.Size = New System.Drawing.Size(1270, 53)
        Me.ts_LM_Orders.TabIndex = 0
        Me.ts_LM_Orders.Text = "ToolStrip1"
        Me.ts_LM_Orders.UserID = CType(0, Long)
        '
        'tsbtn_Save
        '
        Me.tsbtn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_Save.Image = CType(resources.GetObject("tsbtn_Save.Image"), System.Drawing.Image)
        Me.tsbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Save.Name = "tsbtn_Save"
        Me.tsbtn_Save.Size = New System.Drawing.Size(40, 50)
        Me.tsbtn_Save.Tag = "Save"
        Me.tsbtn_Save.Text = "&Save"
        Me.tsbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_Save.ToolTipText = "Save "
        Me.tsbtn_Save.Visible = False
        '
        'tsbtn_New
        '
        Me.tsbtn_New.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_New.Image = CType(resources.GetObject("tsbtn_New.Image"), System.Drawing.Image)
        Me.tsbtn_New.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_New.Name = "tsbtn_New"
        Me.tsbtn_New.Size = New System.Drawing.Size(37, 50)
        Me.tsbtn_New.Tag = "New "
        Me.tsbtn_New.Text = "&New"
        Me.tsbtn_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_New.ToolTipText = "New "
        '
        'tsbtn_ShowHide
        '
        Me.tsbtn_ShowHide.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_ShowHide.Image = Global.gloEMR.My.Resources.Resources.Show
        Me.tsbtn_ShowHide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_ShowHide.Name = "tsbtn_ShowHide"
        Me.tsbtn_ShowHide.Size = New System.Drawing.Size(46, 50)
        Me.tsbtn_ShowHide.Tag = "Show/Hide"
        Me.tsbtn_ShowHide.Text = "Sh&ow"
        Me.tsbtn_ShowHide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_ShowHide.ToolTipText = "Show"
        '
        'tsbtn_Delete
        '
        Me.tsbtn_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_Delete.Image = CType(resources.GetObject("tsbtn_Delete.Image"), System.Drawing.Image)
        Me.tsbtn_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Delete.Name = "tsbtn_Delete"
        Me.tsbtn_Delete.Size = New System.Drawing.Size(50, 50)
        Me.tsbtn_Delete.Tag = " Delete "
        Me.tsbtn_Delete.Text = "&Delete"
        Me.tsbtn_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_Delete.Visible = False
        '
        'tsbtn_Export
        '
        Me.tsbtn_Export.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_Export.Image = CType(resources.GetObject("tsbtn_Export.Image"), System.Drawing.Image)
        Me.tsbtn_Export.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Export.Name = "tsbtn_Export"
        Me.tsbtn_Export.Size = New System.Drawing.Size(52, 50)
        Me.tsbtn_Export.Tag = "Export "
        Me.tsbtn_Export.Text = "&Export"
        Me.tsbtn_Export.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_Export.ToolTipText = "Export "
        Me.tsbtn_Export.Visible = False
        '
        'tsbtn_Finish
        '
        Me.tsbtn_Finish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_Finish.Image = CType(resources.GetObject("tsbtn_Finish.Image"), System.Drawing.Image)
        Me.tsbtn_Finish.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Finish.Name = "tsbtn_Finish"
        Me.tsbtn_Finish.Size = New System.Drawing.Size(66, 50)
        Me.tsbtn_Finish.Tag = "Save&Close"
        Me.tsbtn_Finish.Text = "&Save&&Cls"
        Me.tsbtn_Finish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_Finish.ToolTipText = "Save and Close"
        '
        'tsbtn_Close
        '
        Me.tsbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_Close.Image = CType(resources.GetObject("tsbtn_Close.Image"), System.Drawing.Image)
        Me.tsbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Close.Name = "tsbtn_Close"
        Me.tsbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tsbtn_Close.Tag = " Close "
        Me.tsbtn_Close.Text = "&Close"
        Me.tsbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_Close.ToolTipText = "Close "
        '
        'Splitter3
        '
        Me.Splitter3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Location = New System.Drawing.Point(223, 395)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(847, 3)
        Me.Splitter3.TabIndex = 20
        Me.Splitter3.TabStop = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'Cmnu_DeleteTestOrder
        '
        Me.Cmnu_DeleteTestOrder.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnu_DeleteTestOrder})
        '
        'mnu_DeleteTestOrder
        '
        Me.mnu_DeleteTestOrder.Index = 0
        Me.mnu_DeleteTestOrder.Text = "&Delete Test"
        '
        'Cmnu_ExpClps
        '
        Me.Cmnu_ExpClps.Name = "Cmnu_ExpClps"
        Me.Cmnu_ExpClps.Size = New System.Drawing.Size(61, 4)
        '
        'frm_LM_Orders
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1270, 874)
        Me.Controls.Add(Me.pnlOrderComments)
        Me.Controls.Add(Me.Splitter3)
        Me.Controls.Add(Me.pnlOrderDetails)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlPrevOrders)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_LM_Orders"
        Me.ShowInTaskbar = False
        Me.Text = "Order Templates "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlPrevOrders.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlOrders.ResumeLayout(False)
        CType(Me.C1Orders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_btnLabs.ResumeLayout(False)
        Me.pnl_btnRadiology.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlOrderDetails.ResumeLayout(False)
        Me.pnlc1Grid.ResumeLayout(False)
        Me.pnl_C1OrderDetails.ResumeLayout(False)
        CType(Me.C1OrderDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_Header.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnl_txtSearch.ResumeLayout(False)
        Me.pnl_txtSearch.PerformLayout()
        Me.pnlDueDate.ResumeLayout(False)
        Me.pnlOrderNotes.ResumeLayout(False)
        Me.pnlNotesText.ResumeLayout(False)
        Me.pnlNotesText.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.picNotesClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolstrip.ResumeLayout(False)
        Me.pnlToolstrip.PerformLayout()
        Me.tls_LM_Orders.ResumeLayout(False)
        Me.tls_LM_Orders.PerformLayout()
        Me.pnlOrderComments.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnl_wdOrders.ResumeLayout(False)
        CType(Me.wdOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_lblHeading.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlGloUC_TemplateTreeControl.ResumeLayout(False)
        Me.pnl_cmdPastExam.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.ts_LM_Orders.ResumeLayout(False)
        Me.ts_LM_Orders.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected Overrides Sub OnClosed(ByVal e As System.EventArgs)
        'HotKeys.Clear()
        MyBase.OnClosed(e)
    End Sub

    'SET THIS VARIABLE AS A TRUE BYDEFALUT BECAUSE gloUC_PatientStrip1 IS MAXIMIZED ON FORM LOAD
    Dim IsSizeMaximized As Boolean = True
#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub gloUC_PatientStrip1_btnUpOrDownclick() Handles gloUC_PatientStrip1.btnUpOrDownclick
        IsSizeMaximized = gloUC_PatientStrip1.IsSizeMaximized
    End Sub

    'Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
    '    Try
    '        '' Sudhir 20090226 ''

    '        'If pnlOrderNotes.Visible = True Then
    '        '    pnlOrderNotes.Location = New Point(btnTasks.Left - 35, C1OrderDetails.Top + gloUC_PatientStrip1.Height + 35)
    '        'End If
    '        '' -- ''
    '        '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub Set_PatientDetailStrip()
       
        If Not IsNothing(gloUC_PatientStrip1) Then
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(0, 0, 0, 0)
           
            .ShowDetail(_patientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder)

            .SendToBack()
           
            If frmPatientExam._IsExam = True Then
                .DTPValue = frmPatientExam.dtpvaluefrmexam
            End If
           

        End With
        pnlOrderDetails.Controls.Add(gloUC_PatientStrip1)
       

        '16-May-13 Aniket: Resolving Memory Leaks
        AddHandler gloUC_PatientStrip1.btnUpOrDownclick, AddressOf gloUC_PatientStrip1_btnUpOrDownclick

    End Sub

#End Region

    Private Sub InitializeToolStrip()
        ts_LM_Orders.ConnectionString = GetConnectionString()
        ts_LM_Orders.ModuleName = Me.Name
        ts_LM_Orders.UserID = gnLoginID
        ts_LM_Orders.ButtonsToHide.Add(tsbtn_Save.Name)
        ts_LM_Orders.ButtonsToHide.Add(tsbtn_Export.Name)
        ts_LM_Orders.ButtonsToHide.Add(tsbtn_Delete.Name)
    End Sub

    Private Sub frm_LM_Orders_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            'CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            Else
                If (IsNothing(Me.myCaller) = False) Then
                    CType(Me.myCaller.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Me.ParentForm IsNot Nothing Then
                CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
                CType(Me.ParentForm, MainMenu).ActiveDSO = wdOrders
            End If
        End Try
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ShowMicroPhone()
            End If
        End If

        'Problem # 00000101, 00000142 & 00000179 resolved
        'Skip following code if form is open from dashboard > Task > Show Order
        If ((IsNothing(Me.ParentForm) = False) OrElse (IsNothing(Me.myCaller) = False)) Then
            'Developer: Yatin N.Bhagat
            'Date:12/26/2011
            'Bug ID/PRD Name/Salesforce Case:Bug No. 17246:Patient Consent >> Checkbox in the template are not working once you finish exam
            'Reason: Handler For DDLCBEvent is Not Added while activating the form
            If Not (IsNothing(wdOrders.DocumentName)) Then
                If Not (IsNothing(wdOrders.ActiveDocument)) Then
                    oCurDoc = wdOrders.ActiveDocument
                    oWordApp = oCurDoc.Application
                    Try
                        RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    Try
                        AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    isHandlerRemoved = False
                    oCurDoc.ActiveWindow.SetFocus()
                    wdOrders.Focus()
                End If
            End If
        End If
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub frm_LM_Orders_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                TurnoffMicrophone()
            End If
        End If
        'Problem # 00000101, 00000142 & 00000179 resolved
        'Skip following code if form is open from dashboard > Task > Show Order
        If ((IsNothing(Me.ParentForm) = False) OrElse (IsNothing(Me.myCaller) = False)) Then
            'Developer: Yatin N.Bhagat
            'Date:12/26/2011
            'Bug ID/PRD Name/Salesforce Case:Bug No. 17246:Patient Consent >> Checkbox in the template are not working once you finish exam
            'Reason: Handler For DDLCBEvent is Not Added while activating the form

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
        End If
    End Sub


    Private Sub frm_LM_Orders_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If _blnRecordLock = False Then
            Call UnLock_Transaction(TrnType.Radiology, _patientID, _VisitID, _VisitDate)
        End If

        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                TurnoffMicrophone()
                'ogloVoice.UnInitializeVoiceComponents()
                If Me.IsMdiChild = False Then
                    MyMDIParent.MdiExamChildDeActivate(Me)
                End If
            End If
        End If
        If Not IsNothing(myCaller1) Then
            If myCaller1.canselect = True Then
                myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.RadiologyOrders)   ''("LM_Orders")
                myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.Tasks)  ''("Tasks_MST")
                'CType(myCaller1, frmPatientConsent).Select()
            End If
        End If
        If Not IsNothing(ObjTasksDBLayer) Then
            ObjTasksDBLayer.Dispose()
            ObjTasksDBLayer = Nothing
        End If
        If Not IsNothing(objclsPatientOrders) Then
            objclsPatientOrders.Dispose()
            objclsPatientOrders = Nothing
        End If



        If Not IsNothing(gloUC_PatientStrip1) Then
            '16-May-13 Aniket: Resolving Memory Leaks
            RemoveHandler gloUC_PatientStrip1.btnUpOrDownclick, AddressOf gloUC_PatientStrip1_btnUpOrDownclick
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        If Not IsNothing(oListUsers) Then
            oListUsers.Dispose()
            oListUsers = Nothing
        End If

        'Memory Leak
        If Not IsNothing(oMenu) Then
            oMenu.Dispose()
            oMenu = Nothing
        End If
        'Memory Leak
        If Not IsNothing(ofrmList) Then
            ofrmList.Dispose()
            ofrmList = Nothing
        End If
        'Memory Leak
        If Not IsNothing(ToList) Then
            ToList.Dispose()
            ToList = Nothing
        End If
        'Memory Leak
        If Not IsNothing(oListUsers) Then
            oListUsers.Dispose()
            oListUsers = Nothing
        End If
        'Memory Leak
        If Not IsNothing(ArrLst) Then
            ArrLst.Clear()
            ArrLst = Nothing
        End If
        'Memory Leak
        If Not IsNothing(ArrNotsavedRadioTest) Then
            ArrNotsavedRadioTest.Clear()
            ArrNotsavedRadioTest = Nothing
        End If

        '10-May-13 Aniket: Resolving Memory Leaks
        If IsNothing(toolTipClearUsers) = False Then
            toolTipClearUsers.Dispose()
            toolTipClearUsers = Nothing
        End If

        '10-May-13 Aniket: Resolving Memory Leaks
        If IsNothing(toolTipSelectUser) = False Then
            toolTipSelectUser.Dispose()
            toolTipSelectUser = Nothing
        End If

        '10-May-13 Aniket: Resolving Memory Leaks
        If IsNothing(tooltipTaskNotes) = False Then
            tooltipTaskNotes.Dispose()
            tooltipTaskNotes = Nothing
        End If

        '16-May-13 Aniket: Resolving Memory Leaks
        If IsNothing(dtUsers) = False Then
            dtUsers.Dispose()
            dtUsers = Nothing
        End If

        If (IsNothing(Me.ParentForm) = False) Then
            CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
        End If

        RaiseEvent On_OrderClose()
        If (IsNothing(GloUC_TemplateTreeControl_Orders) = False) Then
            GloUC_TemplateTreeControl_Orders.FinalizeControlParameter("")
        End If
    End Sub

    Private Sub frm_LM_Orders_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            _IsFormClose = True
            If blnChangesMade = True And _blnRecordLock = False Then
                Dim Result As Integer
                Result = MessageBox.Show("Would you like to save your changes before closing? If you do not save changes, all tests and comments added since the last save will be lost.", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                If Result = Windows.Forms.DialogResult.Yes Then
                    SaveOrders()

                    If Me.IsMdiChild = True Then
                        Dim frm As MainMenu
                        frm = Me.MdiParent
                        frm.Fill_Tasks()
                        frm = Nothing
                    Else
                    End If
                ElseIf Result = Windows.Forms.DialogResult.No Then
                    If Me.IsMdiChild = True Then
                        Dim frm As MainMenu
                        frm = Me.MdiParent
                        frm.Fill_Tasks()
                        frm = Nothing
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Radiology order closed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    Else
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Radiology order closed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                    ''To UPdate DICOM Ids in LM_Order 
                    UpdataeOrdersForDICOM()
                ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                    ''To UPdate DICOM Ids in LM_Order 
                    UpdataeOrdersForDICOM()
                End If
            Else
                If Me.IsMdiChild = True Then
                    Dim frm As MainMenu
                    frm = Me.MdiParent
                    frm.Fill_Tasks()
                    frm = Nothing
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Radiology order closed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Radiology order closed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub frm_LM_Orders_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SuspendLayout()
        InitializeToolStrip()
        If (IsOpen) Then
            '10-May-13 Aniket: Resolving Memory Leaks
            Me.ResumeLayout()
            Exit Sub
        End If

        '08-May-13 Aniket: Resolving Memory Leaks
        Dim dt As DataTable = Nothing


        objclsPatientOrders.fill_widthofExam(pnlGloUC_TemplateTreeControl) ''''added to load width of DB


        Try
            ''Fixed case #00000174 : EMR Settings
            C1Orders.ExtendLastCol = True
            '16-May-13 Aniket: Resolving Memory Leaks
            'gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip
            Dim dateNode As New myTreeNode

            'dtorderTime = Format(gloUC_PatientStrip1.DTPValue, "MM/dd/yyyy hh:mm tt")
            _VisitDate = Format(_VisitDate, "MM/dd/yyyy") & " " & Format(_VisitDate, "Short Time")

            dateNode.Text = _VisitDate

            If _VisitID = 0 Then
                Try
                    _VisitID = GenerateVisitID(dateNode.Text, _patientID)
                Catch ex As Exception

                End Try

            End If

            '' To Get the Providers Information 
            '26-May-14 Aniket: Resolving Bug #67061. Get Patient Provider instead of Visit Provider
            Call Fill_ProviderInfo(_patientID)
            ''
            gloUC_PatientStrip1.DTPValue = _VisitDate
            'dtOrderTime.Value = _VisitDate
            dateNode.Tag = _VisitID

            '''''Root Node of trvOrderDetails contains VisitID 
            dateNode.ImageIndex = 14
            dateNode.SelectedImageIndex = 14


            ''''' Initally Order is open in New Mode 
            blnModify = False
            trvOrders.AllowDrop = True

            ''Initially Previous Orders Treeview is hidden
            pnlPrevOrders.Visible = False
            '' Orders Comments panel is Hidden
            pnlOrderComments.Visible = False

            '' TaskNotes Panel is Hidden and 
            pnlOrderNotes.Visible = False
            _strTasksNotes = ""

            '' By Mahesh 20090129
            '' Fill Diagnosis Of the Patient for the Visit  in strDia
            Call Fill_Diagnosis(_VisitID)

            gloC1FlexStyle.Style(C1OrderDetails)
            gloC1FlexStyle.Style(C1Orders)

            '' To load all CAtegory & Test from Master
            Call Fill_ALL_CategoryTestGroups()

            If _OpenfromMainGrid > 0 Then
                '' If Radiology Order is Opened from Patient Details on Main Menu
                Call FillOrderFromMain()
            Else
                OrderDate = objclsPatientOrders.GetOrderFromVisitID(_VisitID)

                If OrderDate = "12:00:00 AM" Then
                    OrderDate = Now
                End If

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070724 
                If gblnRecordLocking = True Then
                    'Memory Leak
                    Dim mydt As mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.Radiology, _patientID, _VisitID, OrderDate)
                    If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                        MessageBox.Show("This Radioloy Order is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        _blnRecordLock = True
                    End If
                    'Memory Leak
                    If Not IsNothing(mydt) Then
                        mydt.Dispose()
                        mydt = Nothing
                    End If
                End If
                ' '' <><><> Record Level Locking <><><><> 

                Call LoadArrayListOnformLoad(OrderDate)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Radiology order record viewed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            If _ArrRadi.Count > 0 Then
                For i As Integer = 0 To _ArrRadi.Count - 1
                    'Memory Leak
                    Dim lst As myList
                    lst = CType(_ArrRadi(i), myList)
                    FillTest(lst.ID, lst.Value)
                    blnChangesMade = True
                    lst = Nothing 'Change made to solve memory Leak and word crash issue
                Next
            End If

            ''''Add By Pramod For CCHIT 2007 To get Radiolgy Cateogry from Admin Start
            gDMSCategory_Radiology = ""
            Dim objSettings As New clsSettings
            dt = objSettings.GetSetting("Radiology Category")
            If IsNothing(dt) = False AndAlso dt.Rows.Count > 0 Then
                gDMSCategory_Radiology = dt.Rows(0)(0).ToString
            End If
            'Memory Leak
            objSettings.Dispose()
            objSettings = Nothing
            ''''Add By Pramod For CCHIT 2007 To get Radiolgy Cateogry from Admin END
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                InitializeVoiceObject()
                If Me.IsMdiChild = False Then
                    MyMDIParent.MdiExamChildActivate(Me)
                End If
            End If
            'Sanjog - Added on 2011 May 17 for Patient Safety
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _patientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            'Sanjog - Added on 2011 May 17 for Patient Safety

            InitiliseTemplateTreeControl()
            calltoAddRefreshButtonControl()
            'Change made to solve memory Leak and word crash issue
            If Not dateNode Is Nothing Then
                dateNode = Nothing
            End If

            toolTipClearUsers.SetToolTip(Me.btnClearTasks, "Clear Users")
            toolTipSelectUser.SetToolTip(Me.btnTasks, "Select User")
            tooltipTaskNotes.SetToolTip(Me.btnNotes, "Task Notes")

            If C1OrderDetails.Rows.Count = 1 Then
                bIsNewOrder = True
            End If



            '16-Jul-14 Aniket: Resolving Bug #71077:
            Try
                If Not IsNothing(wdOrders) Then
                    wdOrders.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoKeepUIActiveOnAppDeactive
                    wdOrders.FrameHookPolicy = DSOFramer.dsoFrameHookPolicy.dsoSetOnFirstOpen
                    'wdOrders.Toolbars = True
                    wdOrders.Titlebar = False
                    wdOrders.Menubar = False
                End If
            Catch ex As Exception

            End Try


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing

        Finally
            '' TO Make Enable/Disable the Buttons according to the Record Lock Status
            Set_RecordLock(_blnRecordLock)
            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If
            Me.ResumeLayout(True)
        End Try

    End Sub

    ''' <summary>
    ''' Fill Test in the Flexgrid under Category and Group
    ''' </summary>
    ''' <param name="TestID"></param>
    ''' <param name="TestName"></param>
    ''' <remarks>Pramod</remarks>
    Public Sub FillTest(ByVal TestID As Long, ByVal TestName As String)

        Dim CategoryRow As C1.Win.C1FlexGrid.Row
        Dim GroupRow As C1.Win.C1FlexGrid.Row
        Dim TestRow As C1.Win.C1FlexGrid.Row

        Try

            For j As Integer = 1 To C1Orders.Rows.Count - 1
                If C1Orders.GetData(j, COLUM_TESTGROUPFLAG) = "T" Then
                    If C1Orders.GetData(j, COLUM_NAME) = TestName Then
                        CategoryRow = C1Orders.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root).Row
                        GroupRow = C1Orders.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row
                        TestRow = C1Orders.Rows(j).Node.Row
                        Call AddTest(CategoryRow, GroupRow, TestRow)
                    End If
                End If
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub btn_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnClearTasks.MouseHover, btnNotes.MouseHover, btnTasks.MouseHover
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_LongYellow
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub btn_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnClearTasks.MouseLeave, btnNotes.MouseLeave, btnTasks.MouseLeave
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_LongButton
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub Set_RecordLock(ByVal locked As Boolean)
        If locked = True Then
            '' Record is Locked 
            tsbtn_Finish.Enabled = False
            tsbtn_Delete.Enabled = False
            tsbtn_Save.Enabled = False
            tsbtn_New.Enabled = False

        Else
            '' Record is Not Locked
            tsbtn_Finish.Enabled = True
            tsbtn_Delete.Enabled = True
            tsbtn_Save.Enabled = True
            tsbtn_New.Enabled = True

        End If
    End Sub

    '26-May-14 Aniket: Resolving Bug #67061. Get Patient Provider instead of Visit Provider
    Private Sub Fill_ProviderInfo(ByVal PatientID As Long)
        '' to Get  Provider Information of the Visit

        '08-May-13 Aniket: Resolving Memory Leaks
        Dim dt As DataTable
        dt = objclsPatientOrders.Get_ProviderInfo(PatientID)

        If IsNothing(dt) = False Then
            If dt.Rows.Count > 0 Then
                _ProviderID = dt.Rows(0)("ProviderID")
                _ProviderName = dt.Rows(0)("sFirstName") & " " & dt.Rows(0)("sLastName")
            End If
        End If

        '08-May-13 Aniket: Resolving Memory Leaks
        If IsNothing(dt) = False Then
            dt.Dispose()
            dt = Nothing
        End If


    End Sub


    Private Sub Fill_Diagnosis(ByVal VisitID As Long)

        Dim objclsProblist As New clsPatientProblemList
        strDia = " "
        Dim i As Integer

        '08-May-13 Aniket: Resolving Memory Leaks
        Dim dtDia As DataTable

        '' Fill Diagnosis Of the Patient for the Visit  in strDia
        'parameter _patientID pass for case UC5070.003 by dipak.
        dtDia = objclsProblist.Get_ProblemListDiagnosis(VisitID, _patientID)
        'end modification
        If IsNothing(dtDia) = False Then
            For i = 0 To dtDia.Rows.Count - 1
                If dtDia.Rows(i)("Flag") = 1 Then
                    ''                          ICD9Code                        ICD9Description
                    strDia = strDia & "|" & dtDia.Rows(i)("Field1") & "-" & dtDia.Rows(i)("Field2")
                End If
            Next
        End If

        If IsNothing(dtDia) = False Then
            dtDia.Dispose()
            dtDia = Nothing
        End If


        objclsProblist.Dispose()
        objclsProblist = Nothing
    End Sub

    Private Sub Fill_Tests()

        '08-May-13 Aniket: Resolving Memory Leaks
        Dim dt As DataTable
        dt = objclsPatientOrders.GetAllTests

        Dim Node_0 As TreeNode = Nothing '' Group Level   '' LABS
        Dim Node_1 As TreeNode = Nothing '' MainTest Level    '' BLOOD
        Dim Node_2 As TreeNode = Nothing '' SubTest Level         '' HEMOGLOBIN

        With trvOrders
            If IsNothing(dt) = False Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim blnISExists As Boolean = False
                    For j As Integer = 0 To .GetNodeCount(False) - 1
                        If dt.Rows(i)("Category") = .Nodes(j).Text Then
                            blnISExists = True
                            'SLR: Added code to get the memory of Node_0
                            Node_0 = .Nodes(j)
                            Exit For
                        End If
                    Next
                    If blnISExists = False Then
                        '' If Category is Not Exist then Add Category node
                        Node_0 = New TreeNode
                        Node_0.Text = dt.Rows(i)("Category")
                        Node_0.Tag = dt.Rows(i)("CategoryID")
                        .Nodes.Add(Node_0)
                    End If

                    If dt.Rows(i)("LevelNo") = 1 Then
                        '' Add Test Group
                        Node_1 = New TreeNode
                        Node_1.Text = dt.Rows(i)("Test_Name")
                        Node_1.Tag = dt.Rows(i)
                        Node_0.Nodes.Add(Node_1)
                    ElseIf dt.Rows(i)("LevelNo") = 2 Then
                        '' Add Tests
                        For j As Integer = 0 To Node_0.GetNodeCount(False) - 1
                            '' Add Tests to respective 
                            If dt.Rows(i)("GroupNo") = CType(Node_0.Nodes(j).Tag, DataRow)("TestID") Then
                                Node_2 = New TreeNode
                                Node_2.Text = dt.Rows(i)("Test_Name")
                                Node_2.Tag = dt.Rows(i)
                                Node_0.Nodes(j).Nodes.Add(Node_2)
                                Exit For
                            End If
                        Next
                    End If
                Next
            End If
        End With

        If IsNothing(dt) = False Then
            dt.Dispose()
            dt = Nothing
        End If


        If IsNothing(objclsPatientOrders) = False Then
            objclsPatientOrders.Dispose()
            objclsPatientOrders = Nothing
        End If


    End Sub

    Private Sub Fill_ALL_CategoryTestGroups()
        '' To Load All Category & Tests [Load Masters]
        Dim oDB As gloStream.gloDataBase.gloDataBase
        Dim oDataReader As SqlClient.SqlDataReader
        Dim _strSQL As String
        Dim _Categories As New Collection
        Dim oFindNode As C1.Win.C1FlexGrid.Node
        Dim _tmpRow As Integer
        Try
            With C1Orders
                .BeginUpdate()
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Count = COLUM_COUNT
                .Cols.Fixed = 0
                .Rows(.Rows.Count - 1).Height = 19

                .Tree.Column = COLUM_NAME
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15

                'Fill Categories
                oDB = New gloStream.gloDataBase.gloDataBase
                oDB.Connect(GetConnectionString)
                oDataReader = oDB.ReadQueryRecords("SELECT lm_category_ID,lm_category_Description,lm_category_CategoryType FROM LM_Category WHERE lm_category_CategoryType = '1' AND lm_category_Description IS NOT NULL  ORDER BY lm_category_Description ")
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        If Not IsDBNull(oDataReader.Item("lm_category_Description") & "") Then
                            .Rows.Add()
                            With .Rows(.Rows.Count - 1)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .TextAlign = TextAlignEnum.LeftCenter
                                .IsNode = True
                                .Node.Level = 0
                                .Node.Data = oDataReader.Item("lm_category_Description")
                                .Node.Key = oDataReader.Item("lm_category_ID")
                            End With

                            .SetData(.Rows.Count - 1, COLUM_IDENTITY, "C" & oDataReader.Item("lm_category_Description"))
                            .SetData(.Rows.Count - 1, COLUM_NUMVALUE, Nothing)
                            .SetData(.Rows.Count - 1, COLUM_ID, oDataReader.Item("lm_category_ID"))
                            .SetData(.Rows.Count - 1, COLUM_TESTGROUPFLAG, "C")
                            .SetData(.Rows.Count - 1, COLUM_LEVELNO, 0)
                            .SetData(.Rows.Count - 1, COLUM_GROUPNO, 0)
                            _Categories.Add(oDataReader.Item("lm_category_Description").ToString().Replace("'", "''"))
                        End If
                    End While
                End If

                '10-May-13 Aniket: Resolving Memory Leaks
                oDataReader.Close()
                oDataReader = Nothing

                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing


                'Fill Tests
                For i As Int16 = 1 To _Categories.Count
                    _strSQL = "SELECT LM_Test.lm_test_ID, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_CategoryID, " _
                        & " LM_Category.lm_category_Description, LM_Test.lm_test_GroupNo, ISNULL(LM_Test_1.lm_test_Name,'') AS GroupName, " _
                        & " LM_Test.lm_test_LevelNo, LM_Test.lm_test_Dimension, LM_Test.lm_test_Template_ID,ISNULL(LM_Test.lm_test_sLonicID,'') as lm_test_sLonicID FROM LM_Test " _
                        & " LEFT OUTER JOIN LM_Test AS LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID " _
                        & " LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
                        & " WHERE LM_Category.lm_category_Description = '" & _Categories(i).ToString.Replace("'", "''") & "' AND LM_Test.lm_test_Name IS NOT NULL " _
                        & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_ID, LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Name"

                    oDB = New gloStream.gloDataBase.gloDataBase
                    oDB.Connect(GetConnectionString)
                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If oDataReader.Item("lm_test_GroupNo") = 0 Then
                                    oFindNode = GetC1UniqueNode("C" & oDataReader.Item("lm_category_Description"), C1Orders)

                                    If Not oFindNode Is Nothing Then
                                        oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDataReader.Item("lm_test_Name"))
                                        _tmpRow = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                        If Not _tmpRow = -1 Then
                                            .Rows(_tmpRow).AllowEditing = False
                                            .Rows(_tmpRow).ImageAndText = True
                                            .Rows(_tmpRow).Height = 24
                                            .Rows(_tmpRow).TextAlign = TextAlignEnum.LeftCenter

                                            'code start by nilesh on date 20101227 for case GLO2010-0005385
                                            .SetData(_tmpRow, COLUM_IDENTITY, oDataReader.Item("lm_test_CategoryID").ToString & oDataReader.Item("lm_test_TestGroupFlag") & oDataReader.Item("lm_test_Name"))

                                            .SetData(_tmpRow, COLUM_NUMVALUE, Nothing)
                                            .SetData(_tmpRow, COLUM_ID, oDataReader.Item("lm_test_ID"))
                                            .SetData(_tmpRow, COLUM_TESTGROUPFLAG, oDataReader.Item("lm_test_TestGroupFlag"))
                                            .SetData(_tmpRow, COLUM_LEVELNO, oDataReader.Item("lm_test_LevelNo"))
                                            .SetData(_tmpRow, COLUM_GROUPNO, oDataReader.Item("lm_test_GroupNo"))

                                            If oDataReader.Item("lm_test_TestGroupFlag") = "T" Then
                                                ''  IF is Test then 
                                                ''  SET TemplateID
                                                .SetData(_tmpRow, COLUM_TEMPLATEID, oDataReader.Item("lm_test_Template_ID"))
                                                .SetData(_tmpRow, COLUM_UNIT, oDataReader.Item("lm_test_Dimension"))
                                                .Rows(_tmpRow).AllowEditing = False

                                            End If

                                            _tmpRow = -1
                                        End If
                                    End If
                                Else
                                    'code start by nilesh on date 20101227 for case GLO2010-0005385
                                    oFindNode = GetC1UniqueNode(oDataReader.Item("lm_test_CategoryID").ToString & "G" & oDataReader.Item("GroupName"), C1Orders)
                                    'code end by nilesh on date 20101227 for case GLO2010-0005385

                                    If Not oFindNode Is Nothing Then
                                        oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDataReader.Item("lm_test_Name"))
                                        _tmpRow = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                        If Not _tmpRow = -1 Then
                                            .Rows(_tmpRow).AllowEditing = False
                                            .Rows(_tmpRow).ImageAndText = True
                                            .Rows(_tmpRow).Height = 24
                                            .Rows(_tmpRow).TextAlign = TextAlignEnum.LeftCenter
                                            .SetData(_tmpRow, COLUM_IDENTITY, oDataReader.Item("lm_test_TestGroupFlag") & oDataReader.Item("lm_test_Name"))
                                            .SetData(_tmpRow, COLUM_NUMVALUE, Nothing)
                                            .SetData(_tmpRow, COLUM_ID, oDataReader.Item("lm_test_ID"))
                                            .SetData(_tmpRow, COLUM_TESTGROUPFLAG, oDataReader.Item("lm_test_TestGroupFlag"))
                                            .SetData(_tmpRow, COLUM_LEVELNO, oDataReader.Item("lm_test_LevelNo"))
                                            .SetData(_tmpRow, COLUM_GROUPNO, oDataReader.Item("lm_test_GroupNo"))
                                            .SetData(_tmpRow, COLUM_LOINC_ID, oDataReader.Item("lm_test_sLonicID"))

                                            If oDataReader.Item("lm_test_TestGroupFlag") = "T" Then
                                                ''  IF is Test then 
                                                ''  SET TemplateID
                                                .SetData(_tmpRow, COLUM_TEMPLATEID, oDataReader.Item("lm_test_Template_ID"))
                                                .SetData(_tmpRow, COLUM_UNIT, oDataReader.Item("lm_test_Dimension"))
                                                .Rows(_tmpRow).AllowEditing = True

                                            End If

                                            _tmpRow = -1
                                        End If
                                    End If
                                End If

                            End While
                        End If
                    End If

                    '10-May-13 Aniket: Resolving Memory Leaks
                    oDataReader.Close()
                    oDataReader = Nothing

                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                Next

                .Cols(COLUM_NAME).AllowEditing = False
                .Cols(COLUM_NAME).AllowDragging = False
                .Cols(COLUM_IDENTITY).AllowEditing = False
                .Cols(COLUM_NUMVALUE).AllowEditing = False
                .Cols(COLUM_ID).AllowEditing = False
                .Cols(COLUM_TESTGROUPFLAG).AllowEditing = False
                .Cols(COLUM_LEVELNO).AllowEditing = False
                .Cols(COLUM_GROUPNO).AllowEditing = False
                .Cols(COLUM_BUTTON).AllowEditing = False
                .Cols(COLUM_DIAGNOSIS).AllowEditing = False
                .Cols(COLUM_DIAGNOSISBUTTON).AllowEditing = False
                .Cols(COLUM_DMSID).AllowEditing = False
                .Cols(COLUM_DICOMID).AllowEditing = False

                .SetData(0, COLUM_NAME, "Tests")
                .SetData(0, COLUM_IDENTITY, "Identity")
                .SetData(0, COLUM_NUMVALUE, "Numeric Result")
                .SetData(0, COLUM_BUTTON, "Add Comments")
                .SetData(0, COLUM_ID, "ID")
                .SetData(0, COLUM_TESTGROUPFLAG, "Flag")
                .SetData(0, COLUM_LEVELNO, "Level No")
                .SetData(0, COLUM_GROUPNO, "Group No")
                .SetData(0, COLUM_DIAGNOSIS, "Diagnosis")
                .SetData(0, COLUM_DIAGNOSISBUTTON, "  ")
                .SetData(0, COLUM_DMSID, "DMS ID")
                .SetData(0, COLUM_DICOMID, "DICOM ID")
                .SetData(0, COLUM_TEXT_COMMENTS, "Text Comments")
                .SetData(0, COLUM_LOINC_ID, "LOINC ID")


                .Cols(COLUM_NAME).Width = .Width - 5 ''((.Width / 5) * 2.5) - 20

                .Cols(COLUM_NAME).Visible = True
                .Cols(COLUM_IDENTITY).Visible = False
                .Cols(COLUM_NUMVALUE).Visible = False
                .Cols(COLUM_UNIT).Visible = False
                .Cols(COLUM_ID).Visible = False
                .Cols(COLUM_TESTGROUPFLAG).Visible = False
                .Cols(COLUM_LEVELNO).Visible = False
                .Cols(COLUM_GROUPNO).Visible = False
                .Cols(COLUM_TEMPLATEID).Visible = False
                .Cols(COLUM_BUTTON).Visible = False
                .Cols(COLUM_ISFINISHED).Visible = False
                .Cols(COLUM_DIAGNOSIS).Visible = False
                .Cols(COLUM_DIAGNOSISBUTTON).Visible = False
                .Cols(COLUM_DMSID).Visible = False
                .Cols(COLUM_DICOMID).Visible = False
                .Cols(COLUM_STAUS).Visible = False
                .Cols(COLUM_TEXT_COMMENTS).Visible = False
                .Cols(COLUM_LOINC_ID).Visible = False
                .EndUpdate()
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            '08-May-13 Aniket: Resolving Memory Leaks
            _Categories.Clear()
            _Categories = Nothing

        End Try

    End Sub

    Private Sub FillCategoryTestGroups()

        Dim oDB As gloStream.gloDataBase.gloDataBase

        '08-May-13 Aniket: Resolving Memory Leaks
        Dim ds As DataSet = Nothing
        '08-May-13 Aniket: Resolving Memory Leaks
        Dim dsTest As DataSet = Nothing
        Dim _strSQL As String
        Dim _Categories As New Collection
        Dim _Groups As New Collection
        Dim _Tests As New Collection
        Try



            Dim oFindNode As C1.Win.C1FlexGrid.Node
            Dim oTempNode As C1.Win.C1FlexGrid.Node
            Dim _tmpRow As Integer
            Dim cStyle As C1.Win.C1FlexGrid.CellStyle

            With C1OrderDetails
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Count = COLUM_COUNT
                .Cols.Fixed = 0
                .Rows(.Rows.Count - 1).Height = 19

                .Tree.Column = COLUM_NAME
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15

                'GLO2011-0015782 One client workstation does not have diagnosis icon under orders menu
                'Not allow to resize the column where button is present in.
                ''Start''
                .Cols(COLUM_DIAGNOSISBUTTON).AllowResizing = False
                .Cols(COLUM_BUTTON).AllowResizing = False
                ''End''

                oDB = New gloStream.gloDataBase.gloDataBase
                oDB.Connect(GetConnectionString)

                _strSQL = "SELECT DISTINCT lm_sCategoryName FROM LM_Orders WHERE lm_OrderDate = '" & _VisitDate & "' AND lm_Patient_ID = " & _patientID & "  ORDER BY lm_sCategoryName"

                ds = oDB.ReadCatRecords(_strSQL)
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing

                If IsNothing(ds) = False Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If IsDBNull(ds.Tables(0).Rows(i)("lm_sCategoryName")) = False Then
                            .Rows.Add()
                            With .Rows(.Rows.Count - 1)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 0
                                .Node.Data = ds.Tables(0).Rows(i)("lm_sCategoryName")
                            End With
                            .SetData(.Rows.Count - 1, COLUM_IDENTITY, "C" & ds.Tables(0).Rows(i)("lm_sCategoryName"))
                            .SetData(.Rows.Count - 1, COLUM_NUMVALUE, Nothing)
                            .SetData(.Rows.Count - 1, COLUM_ID, 0)
                            .SetData(.Rows.Count - 1, COLUM_TESTGROUPFLAG, "C")
                            .SetData(.Rows.Count - 1, COLUM_LEVELNO, 0)
                            .SetData(.Rows.Count - 1, COLUM_GROUPNO, 0)
                            _Categories.Add(ds.Tables(0).Rows(i)("lm_sCategoryName"))
                        End If
                    Next
                End If


                'Fill Groups
                For i As Int16 = 1 To _Categories.Count
                    _strSQL = "SELECT lm_sCategoryName, lm_sGroupName FROM LM_Orders WHERE lm_sCategoryName = '" & _Categories(i).ToString.Replace("'", "''") & "' AND lm_OrderDate = '" & _VisitDate & "' AND lm_Patient_ID = " & _patientID & " ORDER BY lm_sCategoryName,lm_sGroupName"
                    oDB = New gloStream.gloDataBase.gloDataBase
                    oDB.Connect(GetConnectionString)

                    '08-May-13 Aniket: Resolving Memory Leaks
                    If IsNothing(ds) = False Then
                        ds.Dispose()
                        ds = Nothing
                    End If

                    ds = oDB.ReadQueryRecordAsDataSet(_strSQL)

                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing

                    If IsNothing(ds) = False Then
                        For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            oFindNode = GetC1UniqueNode("C" & ds.Tables(0).Rows(j)("lm_sCategoryName").ToString, C1OrderDetails)
                            If Not oFindNode Is Nothing Then

                                'code start by nilesh on date 20110113
                                'get the category id from database and pass it to retrieve unique node
                                _strSQL = "SELECT lm_category_ID FROM LM_Category WHERE lm_category_Description = '" & _Categories(i).ToString.Replace("'", "''") & "'"

                                oDB = New gloStream.gloDataBase.gloDataBase
                                oDB.Connect(GetConnectionString)
                                Dim _lm_category_ID As String = oDB.ExecuteQueryScaler(_strSQL)
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing

                                oTempNode = GetC1UniqueNode(_lm_category_ID & "G" & ds.Tables(0).Rows(j)("lm_sGroupName").ToString, C1OrderDetails)

                                If IsNothing(oTempNode) = True Then
                                    '' If Group Node is Not Exist then Add the Group Node
                                    oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, ds.Tables(0).Rows(j)("lm_sGroupName"))
                                    _tmpRow = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index

                                    Dim _node As Node = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)

                                    If Not _tmpRow = -1 Then
                                        .Rows(_tmpRow).AllowEditing = False
                                        .Rows(_tmpRow).ImageAndText = True
                                        .Rows(_tmpRow).Height = 24

                                        'code start by nilesh on date 20110113
                                        'category id is also set
                                        '.SetData(_tmpRow, COLUM_IDENTITY, "G" & ds.Tables(0).Rows(j)("lm_sGroupName").ToString)
                                        .SetData(_tmpRow, COLUM_IDENTITY, _lm_category_ID & "G" & ds.Tables(0).Rows(j)("lm_sGroupName").ToString)
                                        'code end by nilesh on date 20110113

                                        .SetData(_tmpRow, COLUM_NUMVALUE, Nothing)
                                        .SetData(_tmpRow, COLUM_ID, 0)
                                        .SetData(_tmpRow, COLUM_TESTGROUPFLAG, "G")

                                        _Groups.Add(ds.Tables(0).Rows(j)("lm_sGroupName"))

                                        If ds.Tables(0).Rows(j)("lm_sGroupName") = "T" Then
                                            ''  IF is Test then 
                                            .Rows(_tmpRow).AllowEditing = True
                                            .Cols(COLUM_NAME).AllowEditing = False
                                            ''  SET TemplateID
                                            .SetData(_tmpRow, COLUM_TEMPLATEID, ds.Tables(0).Rows(j)("lm_test_Template_ID"))
                                            '' For the Numeric Value
                                            C1OrderDetails.Cols(COLUM_NUMVALUE).Format = Format("##0.000")
                                            ''  Insert CheckBox
                                            '.SetCellCheck(_tmpRow, COLUM_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                            .SetCellImage(_tmpRow, COLUM_BUTTON, imgTreeView.Images(4))
                                            ''  SET ShowComment Button
                                            .SetData(_tmpRow, COLUM_BUTTON, "")
                                            ' cStyle = .Styles.Add("BubbleValues")
                                            Try
                                                If (.Styles.Contains("BubbleValues")) Then
                                                    cStyle = .Styles("BubbleValues")
                                                Else
                                                    cStyle = .Styles.Add("BubbleValues")
                                                 End If
                                            Catch ex As Exception
                                                cStyle = .Styles.Add("BubbleValues")
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                                ex = Nothing
                                             End Try
                                            cStyle.ComboList = "..."
                                            '.CellButtonImage = imgTreeView.Images(0)
                                            Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_BUTTON, _tmpRow, COLUM_BUTTON)
                                            rgBubbleValues.Style = cStyle

                                            ''''' 20070129 For Fill Diagnosis '
                                            Dim csDia As CellStyle '= .Styles.Add("Dia")
                                            Try
                                                If (.Styles.Contains("Dia")) Then
                                                    csDia = .Styles("Dia")
                                                Else
                                                    csDia = .Styles.Add("Dia")
                                                End If
                                            Catch ex As Exception
                                                csDia = .Styles.Add("Dia")
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                                ex = Nothing
                                            End Try
                                            '' Fill Values In ComboBox
                                            csDia.ComboList = strDia
                                            '''''
                                            .Cols(COLUM_DIAGNOSIS).Style = csDia

                                            Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_DIAGNOSISBUTTON, _tmpRow, COLUM_DIAGNOSISBUTTON)
                                            rgDig.Style = cStyle

                                        End If

                                        _tmpRow = -1
                                    End If
                                End If

                                '''''' Add Test 


                                _strSQL = " SELECT lm_sTestName, lm_Order_ID, lm_Visit_ID, lm_Patient_ID, lm_Provider_ID, lm_test_ID, " _
                                    & " lm_OrderDate, lm_NumericResult, lm_Result, lm_IsFinished, lm_Status, lm_sICD9Code, lm_sICD9Description ,lm_sStatus" _
                                    & " FROM LM_Orders WHERE lm_Patient_ID = " & _patientID & " AND lm_OrderDate = '" & _VisitDate & "' " _
                                    & " AND lm_sCategoryName = '" & _Categories(i).ToString.Replace("'", "''") & "' AND lm_sGroupName = '" & ds.Tables(0).Rows(j)("lm_sGroupName").ToString.Replace("'", "''") & "' ORDER BY  lm_sTestName "


                                oDB = New gloStream.gloDataBase.gloDataBase
                                oDB.Connect(GetConnectionString)
                                dsTest = oDB.ReadQueryRecordAsDataSet(_strSQL)
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing

                                If IsNothing(dsTest) = False Then
                                    For l As Integer = 0 To dsTest.Tables(0).Rows.Count - 1
                                        'If ds.Tables(0).Rows(j)("lm_test_GroupNo").ToString = 0 Then
                                        'oFindNode = GetC1UniqueNode("G" & ds.Tables(0).Rows(j)("lm_test_GroupNo"), C1OrderDetails)

                                        'code start by nilesh on date 20110113
                                        'oFindNode = GetC1UniqueNode("G" & ds.Tables(0).Rows(j)("lm_sGroupName"), C1OrderDetails)
                                        oFindNode = GetC1UniqueNode(_lm_category_ID & "G" & ds.Tables(0).Rows(j)("lm_sGroupName"), C1OrderDetails)
                                        'code end by nilesh on date 20110113

                                        If Not oFindNode Is Nothing Then
                                            '' Check For Duplicate Nodes Under the same Group
                                            'oTempNode = GetC1UniqueNode(dsTest.Tables(0).Rows(l)("lm_test_TestGroupFlag").ToString & dsTest.Tables(0).Rows(l)("lm_test_ID").ToString, C1OrderDetails)
                                            oTempNode = GetC1UniqueNode("T" & dsTest.Tables(0).Rows(l)("lm_sTestName").ToString, C1OrderDetails)
                                            If IsNothing(oTempNode) = False Then
                                                '' If Node is Alredy Exixst then Exit For
                                                ' Problem : 00001008: LM Order Dispay issue
                                                'Exit For
                                                Continue For
                                            End If
                                            '''''

                                            oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dsTest.Tables(0).Rows(l)("lm_sTestName"))
                                            '//.Style = FillControl.Styles("CS_Category")
                                            _tmpRow = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                            If Not _tmpRow = -1 Then
                                                .Rows(_tmpRow).AllowEditing = False
                                                .Rows(_tmpRow).ImageAndText = True
                                                .Rows(_tmpRow).Height = 24
                                                .SetData(_tmpRow, COLUM_IDENTITY, "T" & dsTest.Tables(0).Rows(l)("lm_sTestName").ToString)
                                                .SetData(_tmpRow, COLUM_NUMVALUE, Nothing)
                                                .SetData(_tmpRow, COLUM_ID, 0)
                                                .SetData(_tmpRow, COLUM_TESTGROUPFLAG, "T")
                                                _Tests.Add(dsTest.Tables(0).Rows(l)("lm_sTestName"))

                                                .Rows(_tmpRow).AllowEditing = True
                                                .Cols(COLUM_NAME).AllowEditing = False
                                                '' For the Numeric Value
                                                C1OrderDetails.Cols(COLUM_NUMVALUE).Format = Format("##0.000")
                                                .SetCellImage(_tmpRow, COLUM_BUTTON, imgTreeView.Images(4))

                                                ''  SET ShowComment Button
                                                .SetData(_tmpRow, COLUM_BUTTON, "")
                                                '  cStyle = .Styles.Add("BubbleValues")
                                                Try
                                                    If (.Styles.Contains("BubbleValues")) Then
                                                        cStyle = .Styles("BubbleValues")
                                                    Else
                                                        cStyle = .Styles.Add("BubbleValues")
                                                    End If
                                                Catch ex As Exception
                                                    cStyle = .Styles.Add("BubbleValues")
                                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                                    ex = Nothing
                                                End Try
                                                cStyle.ComboList = "..."

                                                Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_BUTTON, _tmpRow, COLUM_BUTTON)
                                                rgBubbleValues.Style = cStyle

                                                ''''' 20070129 For Fill Diagnosis
                                                Dim csDia As CellStyle '= .Styles.Add("Dia")
                                                Try
                                                    If (.Styles.Contains("Dia")) Then
                                                        csDia = .Styles("Dia")
                                                    Else
                                                        csDia = .Styles.Add("Dia")
                                                    End If
                                                Catch ex As Exception
                                                    csDia = .Styles.Add("Dia")
                                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                                    ex = Nothing
                                                End Try
                                                '' Fill Values In ComboBox
                                                csDia.ComboList = strDia
                                                '''''
                                                .Cols(COLUM_DIAGNOSIS).Style = csDia

                                                'Status Column 
                                                Dim csSta As CellStyle '= .Styles.Add("Sta")
                                                Try
                                                    If (.Styles.Contains("Sta")) Then
                                                        csSta = .Styles("Sta")
                                                    Else
                                                        csSta = .Styles.Add("Sta")
                                                    End If
                                                Catch ex As Exception
                                                    csSta = .Styles.Add("Sta")
                                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                                    ex = Nothing
                                                End Try
                                                '' Fill Values In ComboBox
                                                csSta.ComboList = strStatus
                                                '''''
                                                .Cols(COLUM_STAUS).Style = csSta

                                                Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_DIAGNOSISBUTTON, _tmpRow, COLUM_DIAGNOSISBUTTON)
                                                rgDig.Style = cStyle
                                                '' Set Associated Diagnosis with this Order
                                                .SetData(_tmpRow, COLUM_DIAGNOSIS, dsTest.Tables(0).Rows(l)("lm_sICD9Code") & "-" & dsTest.Tables(0).Rows(l)("lm_sICD9Description"))
                                                ''''''''''''''
                                                'Status Column 
                                                .SetData(_tmpRow, COLUM_STAUS, dsTest.Tables(0).Rows(l)("lm_sStatus"))

                                                If IsDBNull(dsTest.Tables(0).Rows(l)("lm_Result")) = False Then
                                                    If IsNothing(dsTest.Tables(0).Rows(l)("lm_Result")) = False AndAlso CType(dsTest.Tables(0).Rows(l)("lm_Status"), Integer) = myList.enumOrderComment.Assigned.GetHashCode Then
                                                        ''''' If Order comments are entered then Indicate it by ForeColor as RED
                                                        .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Red
                                                    Else
                                                        ''''' If Order comments are NOT entered then Indicate it by ForeColor as GREEN
                                                        .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Green
                                                    End If
                                                Else
                                                    .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Green
                                                End If

                                                'End If

                                                _tmpRow = -1
                                            End If
                                        End If
                                        ' End If
                                    Next
                                End If
                                '' Add Test Close
                            End If
                            ' End If
                        Next
                    End If
                Next

                .Cols(COLUM_NAME).AllowEditing = False
                .Cols(COLUM_IDENTITY).AllowEditing = False
                .Cols(COLUM_NUMVALUE).AllowEditing = True
                .Cols(COLUM_ID).AllowEditing = False
                .Cols(COLUM_TESTGROUPFLAG).AllowEditing = False
                .Cols(COLUM_LEVELNO).AllowEditing = False
                .Cols(COLUM_GROUPNO).AllowEditing = False
                .Cols(COLUM_ISFINISHED).AllowEditing = False
                .Cols(COLUM_UNIT).AllowEditing = False
                .Cols(COLUM_DIAGNOSIS).AllowEditing = True
                .Cols(COLUM_DMSID).AllowEditing = False
                .Cols(COLUM_DICOMID).AllowEditing = False
                .Cols(COLUM_STAUS).AllowEditing = True

                .SetData(0, COLUM_NAME, "Tests")
                .SetData(0, COLUM_IDENTITY, "Identity")
                .SetData(0, COLUM_NUMVALUE, "Value")
                .SetData(0, COLUM_BUTTON, "Comments")
                .SetData(0, COLUM_ID, "ID")
                .SetData(0, COLUM_TESTGROUPFLAG, "Flag")
                .SetData(0, COLUM_LEVELNO, "Level No")
                .SetData(0, COLUM_GROUPNO, "Group No")
                .SetData(0, COLUM_UNIT, "Unit")
                .SetData(0, COLUM_DIAGNOSIS, "Diagnosis")
                .SetData(0, COLUM_DMSID, "DMS ID")
                .SetData(0, COLUM_DICOMID, "DICOM ID")
                .SetData(0, COLUM_STAUS, "Status")
                .SetData(0, COLUM_TEXT_COMMENTS, "Text Comments")
                .SetData(0, COLUM_LOINC_ID, "LOINC Code")

                .Cols(COLUM_NAME).Width = .Width * 0.3 '((.Width / 5) * 2.5) - 20
                .Cols(COLUM_NUMVALUE).Width = .Width * 0.0  '((.Width / 5) * 0.5)
                .Cols(COLUM_BUTTON).Width = 100 ''.Width * 0.2
                .Cols(COLUM_UNIT).Width = .Width * 0  '((.Width / 5) * 0.2)
                .Cols(COLUM_DIAGNOSIS).Width = .Width * 0.4
                .Cols(COLUM_DIAGNOSISBUTTON).Width = 20 ''.Width * 0.03
                .Cols(COLUM_STAUS).Width = .Width * 0.25 ''.Width * 0.03
                .Cols(COLUM_TEXT_COMMENTS).Width = .Width * 0.25 ''.Width * 0.03
                .Cols(COLUM_LOINC_ID).Width = .Width * 0.25 ''.Width * 0.03

                ''GLO2011-0015782 : One client workstation does not have diagnosis icon under orders menu
                'used when user tries to minimize width column contains dropdown list in before and after column resize event
                ''Start
                prevWidth_COLUM_DIAGNOSIS = .Cols(COLUM_DIAGNOSIS).Width
                prevWidth_COLUM_STAUS = .Cols(COLUM_STAUS).Width
                ''End

                .Cols(COLUM_NAME).Visible = True
                .Cols(COLUM_IDENTITY).Visible = False
                .Cols(COLUM_NUMVALUE).Visible = False
                .Cols(COLUM_ID).Visible = False
                .Cols(COLUM_TESTGROUPFLAG).Visible = False
                .Cols(COLUM_LEVELNO).Visible = False
                .Cols(COLUM_GROUPNO).Visible = False
                .Cols(COLUM_TEMPLATEID).Visible = False
                .Cols(COLUM_ISFINISHED).Visible = False
                .Cols(COLUM_DIAGNOSIS).Visible = True
                .Cols(COLUM_DMSID).Visible = False
                .Cols(COLUM_DICOMID).Visible = False
                .Cols(COLUM_TEXT_COMMENTS).Visible = True
                .Cols(COLUM_STAUS).Visible = True
                'c1OrderDetails.getData(0, 2)

                '' For the Numeric Value
                .Cols(COLUM_NUMVALUE).Format = Format("##0.000")
                .Cols(COLUM_NUMVALUE).DataType = System.Type.GetType("System.Decimal")
            End With

            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            '08-May-13 Aniket: Resolving Memory Leaks
            If IsNothing(dsTest) = False Then
                dsTest.Dispose()
                dsTest = Nothing
            End If
            'Memory Leak
            If IsNothing(_Tests) = False Then
                _Tests.Clear()
                _Tests = Nothing
            End If
            If IsNothing(_Groups) = False Then
                _Groups.Clear()
                _Groups = Nothing
            End If
            If IsNothing(_Categories) = False Then
                _Categories.Clear()
                _Categories = Nothing
            End If


        End Try
    End Sub

    Private Function GetC1UniqueNode(ByVal FindData As String, ByVal _C1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid) As C1.Win.C1FlexGrid.Node
        Dim _Node As C1.Win.C1FlexGrid.Node = Nothing
        Dim _FindRow As Integer = _C1FlexGrid.FindRow(FindData, 0, COLUM_IDENTITY, False, True, True)
        If _FindRow > 0 Then
            _Node = _C1FlexGrid.Rows(_FindRow).Node
        End If
        Return _Node
    End Function
    ''GLO2011-0015782 : One client workstation does not have diagnosis icon under orders menu
    'called when user tries to minimize width column contains dropdown listbox. 
    ''Start
    Private Sub C1OrderDetails_AfterResizeColumn(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1OrderDetails.AfterResizeColumn
        Try
            With C1OrderDetails
                If e.Col = COLUM_DIAGNOSIS Or e.Col = COLUM_STAUS Then
                    If .Cols(COLUM_DIAGNOSIS).Width > prevWidth_COLUM_DIAGNOSIS Then
                        .Cols(COLUM_DIAGNOSIS).Width = .Cols(COLUM_DIAGNOSIS).Width
                    Else
                        .Cols(COLUM_DIAGNOSIS).Width = prevWidth_COLUM_DIAGNOSIS
                    End If

                    If .Cols(COLUM_STAUS).Width > prevWidth_COLUM_STAUS Then
                        .Cols(COLUM_STAUS).Width = .Cols(COLUM_STAUS).Width
                    Else
                        .Cols(COLUM_STAUS).Width = prevWidth_COLUM_STAUS
                    End If
                End If

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.LabOrderRequest, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub
    ''End

    Private Sub C1OrderDetails_CellChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1OrderDetails.CellChanged
        If e.Row > 0 Then
            With C1OrderDetails
                If e.Col = COLUM_NUMVALUE Then
                    If .GetData(e.Row, COLUM_TESTGROUPFLAG) <> "T" Then
                        .SetData(e.Row, COLUM_NUMVALUE, Nothing)
                    End If
                End If
            End With
        End If
    End Sub

    Private Sub LoadArrayList(ByVal VisitID As Long, ByVal VisitDate As Date)

        Dim dt As DataTable
        dt = objclsPatientOrders.GetOrders(_patientID, VisitID, VisitDate)

        ''dt(0) = nVisitID,
        ''dt(1) = nLabsorRadID,
        ''dt(2) = sOrderType, 
        ''dt(3) = sResult,
        ''dt(4) = dtOrderDate ''For Time OrderTime
        ''dt(5) = nNumericResult '' For Status of Order Finish =1/ Unfinish=0 
        '''''  NEW
        ''dt(0)= lm_Visit_ID,
        ''dt(1) = lm_test_ID
        ''dt(2)= lm_NumericResult, 
        ''dt(3) = lm_Result
        ''dt(4) = lm_IsFinished 
        ''dt(5) = lm_sICD9Code
        ''dt(6) = lm_sICD9Description
        If IsNothing(dt) = False Then
            ArrLst.Clear()
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Dim lst As New myList
                    'lst.ID = VisitID
                    lst.Index = CType(dt.Rows(i)("lm_test_ID"), Long) '' TestID
                    lst.Value = CType(dt.Rows(i)("lm_NumericResult"), String) '' Numeris Result
                    If IsDBNull(CType(dt.Rows(i)("lm_Result"), Object)) = True Then
                        lst.TemplateResult = Nothing '' Template 
                    Else
                        lst.TemplateResult = CType(dt.Rows(i)("lm_Result"), Object) '' Template 
                    End If

                    If dt.Rows(i)("lm_sICD9Description") = "" Then
                        lst.Description = dt.Rows(i)("lm_sICD9Code")
                    Else
                        lst.Description = dt.Rows(i)("lm_sICD9Code") & " - " & dt.Rows(i)("lm_sICD9Description")
                    End If

                    If dt.Rows(i)("lm_IsFinished") = True Then '' Order Status
                        lst.IsFinished = True  '' Finshed
                    Else
                        lst.IsFinished = False  '' Not Finsh
                    End If
                    If Not IsDBNull(dt.Rows(i)("lm_DMSID")) Then
                        lst.DMSID = dt.Rows(i)("lm_DMSID")
                    End If
                    'sarika 20090211 DICOM
                    If Not IsDBNull(dt.Rows(i)("lm_DICOMID")) Then
                        lst.DICOMID = dt.Rows(i)("lm_DICOMID")
                    End If
                    '----
                    If Not IsDBNull(dt.Rows(i)("lm_Order_ID")) Then
                        OrderId = dt.Rows(i)("lm_Order_ID")
                    End If

                    '' SUDHIR 20090421 '' FOR DENORMALIZATION ''
                    lst.HistoryCategory = dt.Rows(i)("lm_sCategoryName")
                    lst.Group = dt.Rows(i)("lm_sGroupName")
                    lst.HistoryItem = dt.Rows(i)("lm_sTestName")
                    lst.OrderComment = CType(dt.Rows(i)("lm_Status"), myList.enumOrderComment) '' STATUS FLAG FOR ORDER COMMENT, WHETHER ASSIGNED OR NOT
                    '' ''

                    ''Added Rahul for Status,LonicID,TextComment on 20101021.
                    If Not IsDBNull(dt.Rows(i)("lm_sStatus")) Then
                        lst.Status = dt.Rows(i)("lm_sStatus")
                    End If

                    If Not IsDBNull(dt.Rows(i)("lm_sLonicID")) Then
                        lst.LoincCode = dt.Rows(i)("lm_sLonicID")
                    End If

                    If Not IsDBNull(dt.Rows(i)("lm_sTextComment")) Then
                        lst.TextComment = dt.Rows(i)("lm_sTextComment")
                    End If
                    ''End

                    ArrLst.Add(lst)
                    lst = Nothing
                Next

                blnModify = True
            Else
                blnModify = False
            End If
        Else
            blnModify = False
        End If
        dt.Dispose()
        dt = Nothing
        '  Call Load_Grid()
        Call FillCategoryTestGroups()
        Call Load_Grid()
        Call Load_users()
    End Sub

    Private Sub LoadArrayListOnformLoad(ByVal OrderDate As DateTime)
        Try
            '' By Pramod On 20070704
            Dim dt As DataTable
            dt = objclsPatientOrders.GetTodayOrder(OrderDate, _patientID)

            If IsNothing(dt) = False Then
                ArrLst.Clear()
                Dim i As Integer
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        Dim lst As New myList
                        'lst.ID = VisitID
                        lst.Index = CType(dt.Rows(i)("lm_test_ID"), Long) '' TestID
                        lst.Value = CType(dt.Rows(i)("lm_NumericResult"), String) '' Numeris Result
                        If IsDBNull(CType(dt.Rows(i)("lm_Result"), Object)) = True Then
                            lst.TemplateResult = Nothing '' Template 
                        Else
                            lst.TemplateResult = CType(dt.Rows(i)("lm_Result"), Object) '' Template 
                        End If

                        If dt.Rows(i)("lm_sICD9Description") = "" Then
                            lst.Description = dt.Rows(i)("lm_sICD9Code")
                        Else
                            lst.Description = dt.Rows(i)("lm_sICD9Code") & " - " & dt.Rows(i)("lm_sICD9Description")
                        End If

                        If dt.Rows(i)("lm_IsFinished") = True Then '' Order Status
                            lst.IsFinished = True  '' Finshed
                        Else
                            lst.IsFinished = False  '' Not Finsh
                        End If

                        If Not IsDBNull(dt.Rows(i)("lm_DMSID")) Then
                            lst.DMSID = dt.Rows(i)("lm_DMSID")
                        End If
                        ''Sandip Darade 20090314
                        If Not IsDBNull(dt.Rows(i)("lm_DICOMID")) Then
                            lst.DICOMID = dt.Rows(i)("lm_DICOMID")
                        End If

                        '' SUDHIR 20090420 '' ORDER DENORMALIZATION ''

                        lst.HistoryCategory = dt.Rows(i)("lm_sCategoryName")
                        lst.Group = dt.Rows(i)("lm_sGroupName")
                        lst.HistoryItem = dt.Rows(i)("lm_sTestName")
                        lst.OrderComment = CType(dt.Rows(i)("lm_Status"), myList.enumOrderComment)

                        '' END SUDHIR '' 

                        ''Added Rahul on 20101021
                        If Not IsDBNull(dt.Rows(i)("lm_sStatus")) Then
                            lst.Status = dt.Rows(i)("lm_sStatus").ToString()
                        End If

                        If Not IsDBNull(dt.Rows(i)("lm_sLonicID")) Then
                            lst.LoincCode = dt.Rows(i)("lm_sLonicID")
                        End If

                        If Not IsDBNull(dt.Rows(i)("lm_sTextComment")) Then
                            lst.TextComment = dt.Rows(i)("lm_sTextComment")
                        End If
                        ''End

                        ArrLst.Add(lst)
                        If Not IsDBNull(dt.Rows(i)("lm_Order_ID")) Then
                            OrderId = dt.Rows(i)("lm_Order_ID")
                            lst.OrderID = dt.Rows(i)("lm_Order_ID")  '''' solving bug id-10247 (6020)
                        End If
                        _VisitDate = dt.Rows(i)("lm_OrderDate")
                        _VisitID = dt.Rows(i)("lm_Visit_ID")
                        '_ProviderID, lm_Patient_ID, lm_test_ID, lm_Provider_ID, lm_OrderDate, lm_NumericResult, lm_Result, lm_IsFinished, lm_Status, lm_sICD9Code, lm_sICD9Description

                        lst = Nothing 'Change made to solve memory Leak and word crash issue
                    Next

                    blnModify = True
                Else
                    blnModify = False
                End If
            Else
                blnModify = False
            End If
            dt.Dispose()
            dt = Nothing
            '  Call Load_Grid()
            Call FillCategoryTestGroups()
            Call Load_Grid()
            If IsopenfrmTask = False Then
                Call Load_users()
            Else
                IsopenfrmTask = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub Load_Grid()
        Try
            '' Make Check-Uncheck Values of C1Order Grid For Update
            With C1OrderDetails
                .Redraw = False
                Dim i As Integer
                Dim rCount As Integer
                Dim lst As myList
                '' Check in array list
                For i = 0 To ArrLst.Count - 1
                    'Memory Leak
                    'lst = New myList
                    lst = CType(ArrLst(i), myList)
                    For rCount = 1 To .Rows.Count - 1
                        '' if Test Found then Check it
                        If C1OrderDetails.GetData(rCount, COLUM_NAME) = lst.HistoryItem And C1OrderDetails.GetData(rCount, COLUM_IDENTITY) = "T" & lst.HistoryItem Then '' lst.HistoryItem for TestName ''
                            If Val(lst.Value) = 0 Then
                                .SetData(rCount, COLUM_NUMVALUE, Nothing)
                            Else
                                .SetData(rCount, COLUM_NUMVALUE, lst.Value)
                            End If

                            C1OrderDetails.SetData(rCount, COLUM_ISFINISHED, lst.IsFinished)
                            ''''Pramod
                            C1OrderDetails.SetData(rCount, COLUM_DMSID, lst.DMSID)
                            'sarika 20090211 DICOM
                            C1OrderDetails.SetData(rCount, COLUM_DICOMID, lst.DICOMID)
                            '---
                            C1OrderDetails.SetData(rCount, COLUM_LOINC_ID, lst.LoincCode)
                            C1OrderDetails.SetData(rCount, COLUM_TEXT_COMMENTS, lst.TextComment)
                            Exit For
                        End If
                    Next
                Next
                .Cols(COLUM_NAME).TextAlign = TextAlignEnum.LeftCenter
                .Cols(COLUM_DIAGNOSIS).TextAlign = TextAlignEnum.LeftCenter
                .Cols(COLUM_GROUPNO).TextAlign = TextAlignEnum.LeftCenter
                .Cols(COLUM_STAUS).TextAlign = TextAlignEnum.LeftCenter
                .Redraw = True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub Load_users()
        '    ''''For Tasks
        Try
            Dim arrlist As ArrayList = Nothing

            _strTasksNotes = ""

            cmbTasks.DataSource = Nothing
            cmbTasks.Items.Clear()
            If IsNothing(ToList) = False Then
                ToList.Clear()
            End If

            TaskID = objclsPatientOrders.GetOrderTaskID(_patientID, _VisitDate)

            If TaskID > 0 Then
                arrlist = ObjTasksDBLayer.FetchTasksDetailsForUpdate(TaskID)  'fetch referrals against patient
                txtOrderNotes.Text = _strTasksNotes
                If Not IsNothing(arrlist) Then
                    SetTaskDetails(arrlist)
                End If
            End If
            'Memory Leak
            If Not IsNothing(arrlist) Then
                arrlist.Clear()
                arrlist = Nothing
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub gloUC_PatientStrip1_Date_Validated() Handles gloUC_PatientStrip1.Date_Validated

    End Sub

    Private Sub FillOrderFromMain()
        Call LoadArrayList(_VisitID, _VisitDate)
        Try
            Dim arrlist As ArrayList = Nothing
            TaskID = objclsPatientOrders.GetOrderTaskID(_patientID, CType(_VisitDate, DateTime))
            'end modification
            If TaskID > 0 Then
                arrlist = ObjTasksDBLayer.FetchTasksDetailsForUpdate(TaskID)  'fetch referrals against patient
                txtOrderNotes.Text = _strTasksNotes
                If Not IsNothing(arrlist) Then
                    SetTaskDetails(arrlist)
                End If
            End If
            'Memory Leak
            If Not IsNothing(arrlist) Then
                arrlist.Clear()
                arrlist = Nothing
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Order is  Open In Modify Mode
        blnModify = True
        '   When Order is  Open In Modify Mode, Initially Changes Made in Orders are None
        '   i.e blnChangesMade is Not Set
        blnChangesMade = False

    End Sub

    Private Function Searchnode(ByVal node As myTreeNode, ByVal parentnode As myTreeNode) As Boolean

        Dim i As Integer

        'If parentnode Is TrvOrderDetails.Nodes(0) Then
        For i = 0 To parentnode.GetNodeCount(False) - 1
            If node.Text = parentnode.Nodes.Item(i).Text Then
                Return False  ' Already exists
                Exit Function
            End If
        Next

        Return True  ' Not Exists


    End Function

    Private Sub trvPreviousOrders_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvPreviousOrders.AfterSelect
        Try
            trvPreviousOrders.BeginUpdate()
            If trvPreviousOrders.SelectedNode.Text = "Orders" Then
                Exit Sub
            End If

            If trvPreviousOrders.SelectedNode.Level > 1 Then
                trvPreviousOrders.SelectedNode.ExpandAll()
                Exit Sub
            End If
            Dim dt As DataTable = Nothing
            'Memory Leak
            'dt = New DataTable
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'gnPatientID replaced by _patientID
            If trvPreviousOrders.SelectedNode.Text = "Current" Then
                trvPreviousOrders.SelectedNode.Nodes.Clear()
                dt = objclsPatientOrders.GetPrevOrders("C", _patientID, Now)
            ElseIf trvPreviousOrders.SelectedNode.Text = "YesterDay" Then
                trvPreviousOrders.SelectedNode.Nodes.Clear()
                dt = objclsPatientOrders.GetPrevOrders("Y", _patientID, Now)
            ElseIf trvPreviousOrders.SelectedNode.Text = "Last Week" Then
                trvPreviousOrders.SelectedNode.Nodes.Clear()
                dt = objclsPatientOrders.GetPrevOrders("W", _patientID, Now)
            ElseIf trvPreviousOrders.SelectedNode.Text = "Last Month" Then
                trvPreviousOrders.SelectedNode.Nodes.Clear()
                dt = objclsPatientOrders.GetPrevOrders("M", _patientID, Now)
            ElseIf trvPreviousOrders.SelectedNode.Text = "Older" Then
                trvPreviousOrders.SelectedNode.Nodes.Clear()
                dt = objclsPatientOrders.GetPrevOrders("O", _patientID, Now)
            End If
            'end modification
            ' Dim historyNode As myTreeNode
            Dim i As Integer
            'Memory Leak
            If Not IsNothing(dt) Then
                For i = 0 To dt.Rows.Count - 1
                    Dim node As New myTreeNode     'VisitNode
                    node.Tag = dt.Rows.Item(i).Item(0).ToString 'id     'visitid
                    ''node.Text = Format(dt.Rows.Item(i).Item(1), "MM/dd/yyyy")  'text  'OrderDate
                    node.Text = Format(dt.Rows.Item(i).Item(1), "Short Date") & " " & Format(dt.Rows.Item(i).Item(1), "Short Time")
                    trvPreviousOrders.SelectedNode.Nodes.Add(node)
                    Select Case trvPreviousOrders.SelectedNode.Text
                        Case "Current"
                            trvPreviousOrders.SelectedNode.Nodes.Item(i).ForeColor = Color.Blue
                        Case "Yesterday"
                            trvPreviousOrders.SelectedNode.Nodes.Item(i).ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                        Case "Last Week"
                            trvPreviousOrders.SelectedNode.Nodes.Item(i).ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                        Case "Last Month"
                            trvPreviousOrders.SelectedNode.Nodes.Item(i).ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                        Case "Older"
                            trvPreviousOrders.SelectedNode.Nodes.Item(i).ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
                    End Select
                Next
            End If

            ''Sudhir 20090213
            ''To Show OrderDetails for Respective OrderNode.           

            If trvPreviousOrders.SelectedNode.Level = 1 Then

                For index As Integer = 0 To trvPreviousOrders.SelectedNode.Nodes.Count - 1
                    Dim TempOrderNode As New TreeNode
                    TempOrderNode = trvPreviousOrders.SelectedNode ''This is Selected Node
                    'Memory Leak
                    'dt = New DataTable
                    If Not IsNothing(dt) Then
                        dt.Dispose()
                        dt = Nothing
                    End If
                    dt = GetOrderDetail(CType(TempOrderNode.Nodes.Item(index).Text, DateTime))

                    Dim OrderTree As New TreeNode ''To Create Own OrderDetail Tree
                    Dim GroupNode As TreeNode
                    Dim TestNode As TreeNode

                    Dim IsGroupPresent As Boolean = False

                    Dim TestName As String = ""
                    Dim GroupName As String = ""
                    Dim Isfinished As String = ""

                    If dt.Rows.Count > 0 Then

                        For i = 0 To dt.Rows.Count - 1
                            TestName = dt.Rows(i)("TestName").ToString
                            GroupName = dt.Rows(i)("GroupName").ToString
                            Isfinished = dt.Rows(i)("IsFinished").ToString
                            IsGroupPresent = False

                            '' TO ADD FIRST GROUP WHEN NODE IS EMPTY '' EXECUTES FIRST TIME ONLY
                            If OrderTree.Nodes.Count = 0 Then
                                GroupNode = New TreeNode
                                GroupNode.Text = GroupName
                                GroupNode.Tag = "GroupNode"
                                GroupNode.ImageIndex = 14
                                GroupNode.SelectedImageIndex = 14
                                OrderTree.Nodes.Add(GroupNode)
                                GroupNode = Nothing
                            End If
                            ''


                            '' SEARCH FOR RESPECTIVE GROUP AND ADD CHILD NODE TO IT..
                            For k As Integer = 0 To OrderTree.Nodes.Count - 1
                                If OrderTree.Nodes.Item(k).Text = GroupName Then
                                    TestNode = New TreeNode
                                    TestNode.Text = TestName
                                    ' TestNode.Tag = "TestNode"
                                    TestNode.Tag = Isfinished
                                    TestNode.ImageIndex = 16
                                    TestNode.SelectedImageIndex = 16
                                    OrderTree.Nodes.Item(k).Nodes.Add(TestNode)
                                    TestNode = Nothing
                                    IsGroupPresent = True
                                End If
                            Next
                            ''

                            '' IF GROUP NOT PRESENT, ADD GROUP WITH ITS CURRENT CHILD.
                            If Not IsGroupPresent Then
                                ''ADDING GROUP
                                GroupNode = New TreeNode
                                GroupNode.Text = GroupName
                                GroupNode.Tag = "GroupNode"
                                GroupNode.ImageIndex = 14
                                GroupNode.SelectedImageIndex = 14
                                OrderTree.Nodes.Add(GroupNode)
                                GroupNode = Nothing

                                ''ADDING CURRENT (i)th CHILD 
                                TestNode = New TreeNode
                                TestNode.Text = TestName
                                TestNode.ImageIndex = 16
                                TestNode.SelectedImageIndex = 16
                                'TestNode.Tag = "TestNode"
                                TestNode.Tag = Isfinished
                                OrderTree.Nodes.Item(OrderTree.Nodes.Count - 1).Nodes.Add(TestNode)
                                TestNode = Nothing
                            End If
                            ''
                        Next
                    End If

                    ''This is To add Whole Tree in (index)th  LabOrder Node. 
                    For m As Integer = 0 To OrderTree.Nodes.Count - 1
                        TempOrderNode.Nodes.Item(index).Nodes.Add(OrderTree.Nodes(m))
                    Next
                Next
            End If
            ''
            dt.Dispose()
            dt = Nothing
            trvPreviousOrders.SelectedNode.Expand()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            trvPreviousOrders.EndUpdate()
        End Try
    End Sub

    Private Sub trvPreviousOrders_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvPreviousOrders.DoubleClick
        Try
            If IsNothing(trvPreviousOrders.SelectedNode) Then
                Exit Sub
            End If

            If trvPreviousOrders.SelectedNode.Text = "Orders" Then
                Exit Sub
            End If

            If trvPreviousOrders.SelectedNode.Parent.Text = "Orders" Then
                Exit Sub
            End If

            If trvPreviousOrders.SelectedNode.Level > 2 Then
                Exit Sub
            End If

            Dim blnOrderExist As Boolean = False
            With C1OrderDetails
                For i As Integer = 1 To .Rows.Count - 1
                    If .GetCellCheck(i, COLUM_NAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                        '' there Exists an order
                        blnOrderExist = True
                        Exit For
                    End If
                Next
            End With

            If blnOrderExist = True Then
                '' If Order Exists then ask for switch
                If MessageBox.Show("Do you want to switch to Edit mode", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If


            ' '' <><><> Record Level Locking <><><><> 

            If gblnRecordLocking = True Then
                'Memory Leak
                Dim mydt As mytable

                ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'mydt = Scan_n_Lock_Transaction(TrnType.Radiology, gnPatientID, trvPreviousOrders.SelectedNode.Tag, trvPreviousOrders.SelectedNode.Text)
                mydt = Scan_n_Lock_Transaction(TrnType.Radiology, _patientID, trvPreviousOrders.SelectedNode.Tag, trvPreviousOrders.SelectedNode.Text)
                'end modification

                If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                    If MessageBox.Show("This Order is being modified by " & mydt.Code & " on " & mydt.Description & ". You can not modify it. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        '' Open For view 
                        _blnRecordLock = True
                    Else
                        '' 
                        Exit Sub
                    End If
                Else
                    '' If Patient ROS is not locked 
                    If _blnRecordLock = True Then
                        '' Currently Opened Radiology is locked by some other User on other Machine
                        '' do nothing
                    Else
                        '' Currently Opened Radiology is locked by current User on same Machine
                        '' Unlock Currently Opened Radiology  , Pass Currently Opened 

                        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        'Call UnLock_Transaction(TrnType.Radiology, gnPatientID, _VisitID, _VisitDate)
                        Call UnLock_Transaction(TrnType.Radiology, _patientID, _VisitID, _VisitDate)
                        'end modification
                    End If
                    _blnRecordLock = False
                End If

                Call Set_RecordLock(_blnRecordLock)
                'Memory Leak
                If Not IsNothing(mydt) Then
                    mydt.Dispose()
                    mydt = Nothing
                End If


            End If
            '''' <><><> Record Level Locking <><><><> 

            ''Sandip Darade 20090930
            ''Ask user if an order opened using  treeview in show panel and user trying to switch among orders
            ''save the changes made to the order if user  clicks 'Yes' 
            If C1OrderDetails.Rows.Count > 1 And blnChangesMade = True Then
                Dim Result As DialogResult
                Result = MessageBox.Show("Order details have been changed.Do you want to save changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                If Result = Windows.Forms.DialogResult.Yes Then
                    ''Save current order
                    SaveOrders()
                    '' Set Date Value to Patient Details Strip
                    gloUC_PatientStrip1.DTPValue = trvPreviousOrders.SelectedNode.Text
                    gloUC_PatientStrip1.DTPEnabled = False
                    _VisitID = trvPreviousOrders.SelectedNode.Tag
                    _VisitDate = trvPreviousOrders.SelectedNode.Text
                    dtpDueDate.Value = trvPreviousOrders.SelectedNode.Text
                    Call LoadArrayList(_VisitID, _VisitDate)
                    '        ' Order is  Open In Modify Mode
                    blnModify = True

                ElseIf Result = Windows.Forms.DialogResult.No Then
                    '' Set Date Value to Patient Details Strip
                    gloUC_PatientStrip1.DTPValue = trvPreviousOrders.SelectedNode.Text
                    gloUC_PatientStrip1.DTPEnabled = False
                    _VisitID = trvPreviousOrders.SelectedNode.Tag
                    _VisitDate = trvPreviousOrders.SelectedNode.Text
                    dtpDueDate.Value = trvPreviousOrders.SelectedNode.Text
                    Call LoadArrayList(_VisitID, _VisitDate)
                    '        ' Order is  Open In Modify Mode
                    blnModify = True


                ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            Else
                '' Set Date Value to Patient Details Strip
                gloUC_PatientStrip1.DTPValue = trvPreviousOrders.SelectedNode.Text
                gloUC_PatientStrip1.DTPEnabled = False

                _VisitID = trvPreviousOrders.SelectedNode.Tag
                _VisitDate = trvPreviousOrders.SelectedNode.Text
                dtpDueDate.Value = trvPreviousOrders.SelectedNode.Text
                Call LoadArrayList(_VisitID, _VisitDate)

                '        ' Order is  Open In Modify Mode
                blnModify = True

            End If


            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Radiology order open for modify", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            C1OrderDetails.Cols(COLUM_NAME).Width = 824 * 0.3
            '((C1OrderDetails.Width / 5) * 2.5) - 20
            '824 * 0.3 '

            C1OrderDetails.Cols(COLUM_NUMVALUE).Width = 824 * 0.0  '((.Width / 5) * 0.5)
            C1OrderDetails.Cols(COLUM_BUTTON).Width = 100 ''824 * 0.2 '((.Width / 5) * 0.2)
            C1OrderDetails.Cols(COLUM_UNIT).Width = 824 * 0  '((.Width / 5) * 0.2)
            C1OrderDetails.Cols(COLUM_DIAGNOSIS).Width = 824 * 0.4
            C1OrderDetails.Cols(COLUM_DIAGNOSISBUTTON).Width = 20 ''.Width * 0.03
            C1OrderDetails.Cols(COLUM_STAUS).Width = 824 * 0.25 ''.Width * 0.03
            C1OrderDetails.Cols(COLUM_TEXT_COMMENTS).Width = 824 * 0.25 ''.Width * 0.03
            C1OrderDetails.Cols(COLUM_LOINC_ID).Width = 824 * 0.25 ''.Width * 0.03

            'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Modify, "Radiology Order Open for Modify", gstrLoginName, gstrClientMachineName, gnPatientID)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SetTaskDetails(ByRef arrlist As ArrayList)
        Dim i As Integer
        If Not IsNothing(arrlist) Then
            For i = 0 To arrlist.Count - 1
                cmbTasks.Items.Add(arrlist.Item(i))
                If i = 0 Then
                    Dim objmylist As myList
                    objmylist = (CType(arrlist.Item(0), myList))
                    cmbTasks.Text = objmylist.Description
                    dtpDueDate.Value = objmylist.VisitDate
                End If

            Next
        End If
    End Sub

    Private Sub ShowHide()
        Try
            If pnlPrevOrders.Visible = True Then
                pnlPrevOrders.Visible = False
                ''tsbtn_ShowHide.Text = "  Show"
                tsbtn_ShowHide.Text = "  Sh&ow"

                tsbtn_ShowHide.Image = Global.gloEMR.My.Resources.Resources.Show
                tsbtn_ShowHide.ToolTipText = "Show Patient Orders"
                txtSearchTest.Select()
            Else
                pnlPrevOrders.Visible = True
                ''tsbtn_ShowHide.Text = "  Hide"
                tsbtn_ShowHide.Text = "  Hi&de"
                tsbtn_ShowHide.Image = Global.gloEMR.My.Resources.Resources.Hide
                tsbtn_ShowHide.ToolTipText = "Hide Patient Orders"
                txtSearchOrders.Select()


                If trvPreviousOrders.GetNodeCount(False) <= 0 Then
                    trvPreviousOrders.Nodes.Add("Orders")
                    trvPreviousOrders.Nodes(0).ImageIndex = 3
                    trvPreviousOrders.Nodes(0).SelectedImageIndex = 3

                    With trvPreviousOrders.Nodes.Item(0)
                        .Nodes.Add("Current")
                        .Nodes.Item(0).ForeColor = Color.Blue
                        .Nodes.Add("YesterDay")
                        .Nodes.Item(1).ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                        .Nodes.Add("Last Week")
                        .Nodes.Item(2).ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                        .Nodes.Add("Last Month")
                        .Nodes.Item(3).ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                        .Nodes.Add("Older")
                        .Nodes.Item(4).ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)

                    End With
                End If
                trvPreviousOrders.ExpandAll()
                Call RefreshOrderHistory()
            End If

            '' Sudhir 20090226 ''
            If pnlOrderNotes.Visible = True Then
                pnlOrderNotes.Location = New Point(btnTasks.Left - 35, C1OrderDetails.Top + gloUC_PatientStrip1.Height + 35)
            End If

            Show_PrevOrders()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''Sandip Darade 20090902
    ''expand the first node having previous orders
    Dim _blnPrevOrdersExpanded As Boolean
    Private Sub Show_PrevOrders()

        ''If (tsbtn_ShowHide.Text = "  Hide  " And _blnPrevOrdersExpanded = False) Then
        If (tsbtn_ShowHide.Text = "  Hi&de  " And _blnPrevOrdersExpanded = False) Then
            For Each n As TreeNode In trvPreviousOrders.Nodes.Item(0).Nodes
                Dim dt As DataTable = Nothing
                Select Case n.Text

                    Case "Current"
                        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        dt = objclsPatientOrders.GetPrevOrders("C", _patientID, Now)
                        'end modification
                        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                            trvPreviousOrders.SelectedNode = n
                            trvPreviousOrders_AfterSelect(Nothing, Nothing)
                            _blnPrevOrdersExpanded = True

                        End If

                    Case "YesterDay"
                        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        dt = objclsPatientOrders.GetPrevOrders("Y", _patientID, Now)
                        'end modification
                        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                            trvPreviousOrders.SelectedNode = n
                            _blnPrevOrdersExpanded = True
                        End If
                    Case "Last Week"
                        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        dt = objclsPatientOrders.GetPrevOrders("W", _patientID, Now)
                        'end modification
                        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                            trvPreviousOrders.SelectedNode = n
                            _blnPrevOrdersExpanded = True
                        End If
                    Case "Last Month"
                        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        dt = objclsPatientOrders.GetPrevOrders("M", _patientID, Now)
                        'end modification
                        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                            trvPreviousOrders.SelectedNode = n
                            _blnPrevOrdersExpanded = True
                        End If
                    Case "Older"
                        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        dt = objclsPatientOrders.GetPrevOrders("O", _patientID, Now)
                        'end modification
                        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                            trvPreviousOrders.SelectedNode = n
                            _blnPrevOrdersExpanded = True
                        End If
                End Select
                'Memory Leak
                dt.Dispose()
                dt = Nothing 'Change made to solve memory Leak and word crash issue
            Next

        End If
    End Sub

    Private Sub RefreshOrderHistory()
        If objclsPatientOrders.CheckRecordCount("C", _patientID) Then
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(0).ImageIndex = 4
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 4
        Else
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(0).ImageIndex = 5
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 5
        End If
        If objclsPatientOrders.CheckRecordCount("Y", _patientID) Then
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(1).ImageIndex = 6
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 6
        Else
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(1).ImageIndex = 7
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 7
        End If
        If objclsPatientOrders.CheckRecordCount("W", _patientID) Then
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(2).ImageIndex = 8
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 8
        Else
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(2).ImageIndex = 9
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 9
        End If
        If objclsPatientOrders.CheckRecordCount("M", _patientID) Then
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(3).ImageIndex = 10
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 10
        Else
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(3).ImageIndex = 11
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 11
        End If
        If objclsPatientOrders.CheckRecordCount("O", _patientID) Then
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(4).ImageIndex = 12
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 12
        Else
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(4).ImageIndex = 13
            trvPreviousOrders.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 13
        End If
    End Sub

    Public Sub SaveOrders()  ''''''Made as Public function for Smart Diagnosis Changes - By Ujwala as on 20101013
        Try

            Dim blnOrderExists As Boolean = False
            If ArrLst.Count = 0 Then
                If MessageBox.Show("There is no Test selected for the Patient." & vbCrLf & "Saving this order may cause delete the previous Tests of visit '" & _VisitDate & "', Still you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            If C1OrderDetails.Rows.Count >= 0 Then
                C1OrderDetails.Select(0, 0, 0, 0, True)
            End If

            objclsPatientOrders.Add_LM_Orders(_VisitID, _patientID, _VisitDate, _ProviderID, ArrLst)

            If Not _ArrRadi Is Nothing Then
                If _ArrRadi.Count > 0 Then
                    For i As Integer = 0 To _ArrRadi.Count - 1
                        'Memory Leak
                        Dim lst As myList
                        lst = CType(_ArrRadi(i), myList)
                        For j As Integer = 0 To ArrLst.Count - 1
                            'Memory Leak
                            Dim _lst As myList
                            _lst = CType(ArrLst(j), myList)
                            If Convert.ToString(lst.Value) = Convert.ToString(_lst.HistoryItem) Then
                                lst.IsFinished = True
                                Exit For
                            End If
                            _lst = Nothing 'Change made to solve memory Leak and word crash issue
                        Next
                        lst = Nothing 'Change made to solve memory Leak and word crash issue
                    Next
                End If
            End If

            ''''' if 
            If pnlPrevOrders.Visible = True Then
                Call RefreshOrderHistory()
            End If

            If IsNothing(ToList) = False Then
                If ToList.Count > 0 Then
                    ''To Generate TaskSubject ''
                    Dim sTaskSubject As String = ""
                    For i As Int16 = 1 To C1OrderDetails.Rows.Count - 2
                        If C1OrderDetails.Rows(i).Node.Level = 2 Then
                            sTaskSubject = sTaskSubject + C1OrderDetails.GetData(i, COLUM_NAME) + ", "
                        End If
                    Next

                    sTaskSubject = sTaskSubject + C1OrderDetails.GetData(C1OrderDetails.Rows.Count - 1, COLUM_NAME) ''For Last row.

                    Call AddTasks(sTaskSubject, txtOrderNotes.Text.Trim, _VisitDate, dtpDueDate.Value, TaskType.OrderRadiology)
                End If
            End If

            blnModify = True

            ''''' Changes are Saved, Reset the Changes In Order Flag
            blnChangesMade = False
            'If _IsFormClose = False Then


            ''added by Mayuri:20100401-added createlog audittrails case No:#GLO2010-0004829 

            If _IsOrderDeleted = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Delete, "Order deleted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf _IsGroupDeleted = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Delete, "Group deleted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf _IsTestDeleted = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Delete, "Test deleted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                If bIsNewOrder Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Add, "Order created", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Modify, "Order modified", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If

            End If

            '
            'Added by kanchan on 20100618 for samrt order
            RaiseEvent EvnSaveOrderHandler()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub RemoveTask()
        ObjTasksDBLayer.DeleteData(TaskID)
    End Sub

    Private Sub AddTasks(ByVal Subject As String, ByVal Notes As String, ByVal TaskDate As DateTime, ByVal DueDate As DateTime, ByVal TaskType As gloTaskMail.TaskType, Optional ByVal FaxTiffFileName As String = "")
        Dim arrlist As New ArrayList
        '' fill Value to arraylist 
        '' Like Taskid ,Date, etc Task Related Values
        Call GetData(arrlist)


        Dim ogloTask As New gloTaskMail.gloTask(GetConnectionString)
        Dim oTask As New Task()
        Dim oTaskProgress As New gloTaskMail.TaskProgress
        Dim i As Integer
        ''Comment by sudhir 20090209
        'For i = 0 To ArrTasks.Length - 1
        For i = 0 To ToList.Count - 1

            Dim oTaskAssign As New TaskAssign

            oTaskAssign.AssignFromID = gnLoginID
            oTaskAssign.AssignFromName = gstrLoginName
            oTaskAssign.AssignToID = ToList(i).ID
            If oTaskAssign.AssignFromID = oTaskAssign.AssignToID Then
                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self
                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept
            Else
                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned
                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold
            End If
            oTaskAssign.AssignToName = ToList(i).Description
            oTaskAssign.ClinicID = gnClinicID
            oTask.Assignment.Add(oTaskAssign)
            oTaskAssign.Dispose()
            oTaskAssign = Nothing
        Next

        oTaskProgress.ClinicID = gnClinicID
        oTaskProgress.Complete = 0
        oTaskProgress.DateTime = TaskDate
        oTaskProgress.Description = Notes
        oTaskProgress.StatusID = 1 '' Not Started
        oTaskProgress.TaskID = 0

        '' 
        oTask.UserID = gnLoginID
        oTask.TaskType = TaskType
        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'oTask.PatientID = gnPatientID
        oTask.PatientID = _patientID
        'end modification
        oTask.Subject = "Order : " & Subject
        oTask.ClinicID = gnClinicID
        oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(TaskDate)
        oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(TaskDate)
        oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(DueDate)
        oTask.FaxTiffFileName = TaskDate  ''OrderDate 
        oTask.IsPrivate = False
        oTask.MachineName = gstrClientMachineName
        oTask.Progress = oTaskProgress
        oTask.PriorityID = 1
        oTask.ProviderID = gnPatientProviderID
        ''added if condition for bugid 98692 if more than 1 user then set groupid else set it to 0
        If (ToList.Count > 1) Then
            oTask.TaskGroupID = ogloTask.GetUniqueueId()
        Else
            oTask.TaskGroupID = 0
        End If
        ogloTask.Add(oTask)
        ToList.Dispose()
        ToList = Nothing
        oTaskProgress.Dispose()
        oTaskProgress = Nothing
        oTask.Dispose()
        oTask = Nothing
        ogloTask.Dispose()
        ogloTask = Nothing



    End Sub

    Private Sub DeleteOrder()
        Dim _UnfinishedTests As String = GetunFinishedTests()

        If Not IsNothing(C1OrderDetails.Rows.Count) Then
            If C1OrderDetails.RowSel >= 1 Then
                With C1OrderDetails
                    If MessageBox.Show("Are you sure to Delete this Patient Order Details?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        If objclsPatientOrders.DeleteOrders(_VisitID, _patientID, _VisitDate, TaskID, _UnfinishedTests) = True Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Order deleted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ''
                            Call NewOrder()
                            If pnlPrevOrders.Visible = True Then
                                Call RefreshOrderHistory()
                                trvPreviousOrders.CollapseAll()
                                trvPreviousOrders.ExpandAll()
                            End If
                        End If
                    End If
                End With
            End If
        End If

    End Sub

    Private Sub NewOrder()
        '' SUDHIR 20090521 '' CHECK PROVIDER ''
        If gblnProviderDisable = True Then
            If ShowAssociateProvider(_patientID, Me) = True Then
                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
            End If
        End If
        '' END SUDHIR

        _VisitDate = Now
        _VisitDate = Format(_VisitDate, "MM/dd/yyyy") & " " & Format(_VisitDate, "Short Time")
        _VisitID = GenerateVisitID(_VisitDate, _patientID)

        '' To Get the Providers Information 
        '26-May-14 Aniket: Resolving Bug #67061. Get Patient Provider instead of Visit Provider
        Call Fill_ProviderInfo(_patientID)
        gloUC_PatientStrip1.DTPValue = _VisitDate

        gloUC_PatientStrip1.DTPEnabled = True

        ''''' Initally Order is open in New Mode 
        blnModify = False

        '' Orders Comments panel is Hidden
        pnlOrderComments.Visible = False
        '' TaskNotes Panel is Hidden and 
        pnlOrderNotes.Visible = False
        _strTasksNotes = ""

        Call Fill_ALL_CategoryTestGroups()

        '' Add all Orders for VisitID, Order Date to Arraylst
        Call LoadArrayList(_VisitID, _VisitDate)

        ''''' Initials the Tasks
        cmbTasks.Items.Clear()
        pnlOrderNotes.Visible = False
        _strTasksNotes = ""
        TaskID = 0
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "New radiology order open", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        ''
    End Sub
    ''Sudhir 20090213 -- Used to Show Order Details in Previous Order Tree. ''

    Private Function GetOrderDetail(ByVal VisitDate As DateTime) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim Query As String = ""
        Dim dt As New DataTable
        Try
            Query = "SELECT ISNULL(lm_sTestName,'') AS TestName, ISNULL(lm_sGroupName,'') AS GroupName,ISNULL(lm_IsFinished,0) AS IsFinished FROM LM_Orders WHERE lm_OrderDate = '" & VisitDate.ToString() & "'"

            oDB.Connect(False)
            oDB.Retrive_Query(Query, dt)
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            '10-May-13 Aniket: Resolving Memory Leaks
            'Finally
            '    If Not IsNothing(dt) Then
            '        dt.Dispose()
            '        dt = Nothing
            '    End If
            Return Nothing
        End Try
    End Function

    Public Sub btnTasks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTasks.Click

        ''New ListControl on Form.
        Try

            ofrmList = New frmViewListControl

            oListUsers = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.Users, True, ofrmList.Width)
            oListUsers.ControlHeader = "Users"

            AddHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick

            ''To Select already Added Users.
            If IsNothing(ToList) = False Then
                For i As Integer = 0 To ToList.Count - 1
                    oListUsers.SelectedItems.Add(ToList(i))
                Next
            End If
            ''

            ofrmList.Controls.Add(oListUsers)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()
            oListUsers.ShowHeaderPanel(False)
            oListUsers.OpenControl()
            ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmList.Text = "Users"
            ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

            If IsNothing(ofrmList) = False Then
                'Memory Leak
                ofrmList.Controls.Remove(oListUsers)
                RemoveHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
                RemoveHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
                oListUsers.Dispose()
                oListUsers = Nothing
                ofrmList.Close() 'Change made to solve memory Leak and word crash issue
                ofrmList.Dispose()
                ofrmList = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub oListUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try

            '16-May-13 Aniket: Resolving Memory Leaks
            If IsNothing(dtUsers) = False Then
                dtUsers.Dispose()
                dtUsers = Nothing
            End If

            dtUsers = New DataTable
            Dim dcId As New DataColumn("ID")
            Dim dcDescription As New DataColumn("Description")
            dtUsers.Columns.Add(dcId)
            dtUsers.Columns.Add(dcDescription)
            If IsNothing(ToList) = False Then
                ToList.Clear()
                ToList.Dispose() : ToList = Nothing
            End If
            ToList = New gloGeneralItem.gloItems()
            Dim ToItem As gloGeneralItem.gloItem

            If oListUsers.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                    Dim drTemp As DataRow = dtUsers.NewRow()
                    drTemp("ID") = oListUsers.SelectedItems(i).ID
                    drTemp("Description") = oListUsers.SelectedItems(i).Description
                    dtUsers.Rows.Add(drTemp)

                    ToItem = New gloGeneralItem.gloItem()

                    ToItem.ID = oListUsers.SelectedItems(i).ID
                    ToItem.Description = oListUsers.SelectedItems(i).Description

                    ToList.Add(ToItem)
                    ToItem.Dispose()
                    ToItem = Nothing
                Next
            End If
            cmbTasks.DataSource = dtUsers
            cmbTasks.ValueMember = dtUsers.Columns("ID").ColumnName
            cmbTasks.DisplayMember = dtUsers.Columns("Description").ColumnName

            ofrmList.Close()

            '10-May-13 Aniket: Resolving Memory Leaks
            ofrmList.Dispose()
            ofrmList = Nothing

            ''To Open TaskNote Panel
            If ToList.Count > 0 Then
                btnNotes_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub oListUsers_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
    End Sub

    Private Sub LoadGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Top = pnlTaskUseLst.Top + 100
                dgCustomGrid.Left = pnlTaskUseLst.Left
                dgCustomGrid.Height = pnlTaskUseLst.Height - 20  '+ Panel2.Height + pnlWordObject.Height
                dgCustomGrid.Visible = True
                dgCustomGrid.Width = pnlTaskUseLst.Width
                dgCustomGrid.BringToFront()
                BindGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
                dgCustomGrid.Label1.Visible = True
                dgCustomGrid.txtsearch.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            Dim dt As DataTable

            Referralcount = 0
            Dim col As New DataColumn


            dt = ObjTasksDBLayer.FillControls(1)
            ''''' Add One Column to the Datatable For CheckBOX
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")
            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                '' For DataBinding Users
                dgCustomGrid.datasource(ObjTasksDBLayer.DsDataview)
                ' Sort data view on Login Name
                ObjTasksDBLayer.SortDataview(ObjTasksDBLayer.DsDataview.Table.Columns(1).ColumnName)
            End If
            Referralcount = dt.Rows.Count
            HideColumns()
            'Memory Leak
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub HideColumns()
        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
        ' '' Show User Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = 6
            .AllowEditing = True

            .SetData(0, Col_Check, "Select")
            .Cols(Col_Check).Width = _TotalWidth * 0.09
            .Cols(Col_Check).AllowEditing = True


            .SetData(0, Col_UserID, "UserID")
            .Cols(Col_UserID).Width = _TotalWidth * 0
            .Cols(Col_UserID).AllowEditing = False

            .SetData(0, Col_LoginName, "Login Name")
            .Cols(Col_LoginName).Width = _TotalWidth * 0.44
            .Cols(Col_LoginName).AllowEditing = False

            .SetData(0, Col_Column1, "Name")
            .Cols(Col_Column1).Width = _TotalWidth * 0.47
            .Cols(Col_Column1).AllowEditing = False

            .Cols(Col_ProviderID).Width = 0
            .Cols(Col_Column2).Width = 0

            'Move the last column select to first column
            .Cols.Move(.Cols.Count - 1, 0)
        End With
    End Sub

    Private Sub AddControl()
        'Memeory Leak
        If Not IsNothing(dgCustomGrid) Then
            If Me.pnlTaskUseLst.Controls.Contains(dgCustomGrid) Then
                Me.pnlTaskUseLst.Controls.Remove(dgCustomGrid)

            End If
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
        End If

        dgCustomGrid = New CustomTask
        Me.pnlTaskUseLst.Controls.Add(dgCustomGrid)
        dgCustomGrid.Panel2.Visible = False
        dgCustomGrid.Dock = DockStyle.Fill

        '''''dgcustomGrid.BringToFront()
        dgCustomGrid.SetVisible = False
    End Sub

    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            Me.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Visible = False
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
        End If
    End Sub

    Private Sub SetGridValues(Optional ByVal dblstatus As System.Int16 = 0)
        Try

            If dblstatus = 0 Then
                Dim i As Integer

                'Pramod 05122007
                ''Bind the checked user in the combo box
                For i = 1 To dgCustomGrid.C1Task.Rows.Count - 1
                    If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                        If FindDuplicateTo(CType(dgCustomGrid.C1Task.GetData(i, 1), Long)) Then
                            cmbTasks.Items.Add(New myList(CType(dgCustomGrid.C1Task.GetData(i, 1), Long), CType(dgCustomGrid.C1Task.GetData(i, 2), System.String)))
                            cmbTasks.Text = CType(dgCustomGrid.GetItem(i, 2), System.String)
                        End If
                    End If
                Next

            End If

            RemoveControl()
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            RemoveControl()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            RemoveControl()
        Finally
            'RemoveControl()
        End Try
    End Sub

    Private Function FindDuplicateTo(ByVal Id As Long) As Boolean
        Dim i As Integer
        For i = 0 To cmbTasks.Items.Count - 1
            Dim objmylist As myList
            objmylist = (CType(cmbTasks.Items.Item(i), myList))
            If Id = objmylist.Index Then
                Return False
                Exit Function
            End If
        Next
        Return True
    End Function

    Private Sub dgCustomGrid_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.SearchChanged
        Try

            Dim mystr As String
            mystr = Replace(Trim(dgCustomGrid.SearchText), "'", "''")

            ObjTasksDBLayer.SetRowFilter(dgCustomGrid.SearchText)

            Referralcount = ObjTasksDBLayer.DsDataview.Count


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        RemoveControl()
        pnlTaskUseLst.Visible = False

    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        SetGridValues(0)
        pnlTaskUseLst.SendToBack()
        pnlTaskUseLst.Visible = False
        If Referralcount > 0 Then
            Call btnNotes_Click(sender, e)
        End If
    End Sub

    Private Sub dgCustomGrid_Dblclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.Dblclick
        SetGridValues(1)
    End Sub

    Public Sub btnClearTasks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTasks.Click
        If cmbTasks.Items.Count > 0 Then
            If MessageBox.Show("Are you sure you want to clear selected user?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then

                ''Above code commented by Sandip Darade 20090620
                ''Added code below to remove the selected user only
                Try
                    If cmbTasks.SelectedIndex >= 0 Then
                        Dim _userId As Int64 = 0
                        'Remove item from ToList
                        Dim i As Integer
                        _userId = Convert.ToInt64(cmbTasks.SelectedValue)
                        'SLR: Changed on 4/2/2014
                        For i = ToList.Count - 1 To 0 Step -1

                            If (ToList(i).ID = _userId) Then

                                ToList.RemoveAt(i)
                                ''Refresh rhe datasource of the combobox
                                ''Remove the row containg  the selected user from the table 
                                Dim dr As DataRow
                                For Each dr In dtUsers.Rows
                                    If (dr.Item(0) = _userId) Then
                                        dtUsers.Rows.Remove(dr)
                                        Exit For
                                    End If
                                Next

                                Exit For
                            End If
                        Next

                        If cmbTasks.Items.Count = 0 Then
                            cmbTasks.Text = ""
                        End If

                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing

                End Try
                blnChangesMade = True
            End If
        End If
    End Sub

    Private Sub dgCustomGrid_MouseUpClick(ByVal sender As Object, ByVal e As Object) Handles dgCustomGrid.MouseUpClick
        If dgCustomGrid.GetCurrentrowIndex >= 0 Then
            dgCustomGrid.GetSelect(dgCustomGrid.GetCurrentrowIndex)
        End If
    End Sub




    Private Sub GetData(ByRef Arrlist As ArrayList)
        '@nTaskId	numeric(18,0),
        '@nFromId	numeric(18,0),
        '@nToId		numeric(18,0),
        '@dtTaskDate	numeric(18,0),
        '@sSubject	varchar(100),
        '@dtDuedate	datetime,
        '@sPriority	varchar(50),
        '@sStatus	varchar(50),
        '@sNotes	
        LoginId = ObjTasksDBLayer.GetLoginId
        ''Arrlist.Add(0)
        Arrlist.Add(TaskID)
        Arrlist.Add(LoginId)
        Arrlist.Add(_VisitDate)   ''dtTaskDate

        'sarika 21st june 07

        '' Orders Radiology Chenged to "Radiology Orders" on 20071003 - Anil 
        '' Ref BugNo - 83
        Dim strSubject As String = "Orders"
        '---------

        Arrlist.Add(strSubject)   '' sSubject
        Arrlist.Add(dtpDueDate.Value)    '' dtDuedate
        Arrlist.Add("High")   '' sPriority
        Arrlist.Add("Not Started")   '' sStatus
        _strTasksNotes = Trim(txtOrderNotes.Text)
        Arrlist.Add(_strTasksNotes)   '' sNotes
        Arrlist.Add(_patientID)
    End Sub

    Public Sub ToolBar_NewOrders()
        If C1OrderDetails.Rows.Count > 1 And blnChangesMade = True Then
            Dim Result As DialogResult
            Result = MessageBox.Show("Would you like to save your changes before closing? If you do not save changes, all tests and comments added since the last save will be lost.", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Result = Windows.Forms.DialogResult.Yes Then
                SaveOrders()
                NewOrder()
            ElseIf Result = Windows.Forms.DialogResult.No Then
                NewOrder()
            ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        Else
            NewOrder()
        End If
    End Sub

    Public Sub ToolBar_DeleteOrders()
        Call DeleteOrder()
    End Sub

    Public Sub ToolBar_SaveOrders()
        Try
            If blnChangesMade = True Then
                SaveOrders()
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Add, "Order viewed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.LabOrderRequest, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub ToolBar_FinishOrders()
        Try
            If blnChangesMade = True Then
                SaveOrders()
            Else
                If _IsOrderDeleted = True Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Delete, "Order deleted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf _IsGroupDeleted = True Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Delete, "Group deleted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf _IsTestDeleted = True Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Delete, "Test deleted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.View, "Order viewed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If

            End If

            If Me.IsMdiChild = True Then
                Dim frm As MainMenu
                frm = Me.MdiParent
                frm.Fill_Tasks()
                frm = Nothing
                Me.Close()
            Else
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.LabOrderRequest, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally


        End Try

    End Sub

    Public Sub ToolBar_ShowHideOrders()

        Call ShowHide()
    End Sub

    Public Sub PreviousOrders(ByVal Trv As TreeView, ByVal enmOrders As enmPreviousOrders)
        Select Case enmOrders
            Case enmPreviousOrders.Current
                VoiceSearchNode(trvPreviousOrders, "Current")
            Case enmPreviousOrders.Yesterday
                VoiceSearchNode(trvPreviousOrders, "YesterDay")
            Case enmPreviousOrders.LastWeek
                VoiceSearchNode(trvPreviousOrders, "Last Week")
            Case enmPreviousOrders.LastMonth
                VoiceSearchNode(trvPreviousOrders, "Last Month")
            Case enmPreviousOrders.Older
                VoiceSearchNode(trvPreviousOrders, "Older")
        End Select
        If IsNothing(trvSearchNode) = False Then
            trvPreviousOrders.SelectedNode = trvSearchNode
        End If
    End Sub

    Private Sub VoiceSearchNode(ByVal Trv As TreeView, ByVal strText As String)
        Dim trvNde As TreeNode
        For Each trvNde In Trv.Nodes
            VoiceSearchNode(trvNde, strText)
        Next
    End Sub

    Private Sub VoiceSearchNode(ByVal rootNode As TreeNode, ByVal strText As String)
        For Each childNode As TreeNode In rootNode.Nodes
            If LCase(Trim(childNode.Text)) = LCase(Trim(strText)) Then
                trvSearchNode = childNode
                Exit Sub
            End If
            VoiceSearchNode(childNode, strText)
        Next
    End Sub

    Private Sub frm_LM_Orders_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try

            If blnOpenFromExam = True Then

                If myCaller.CanSelect = True Then
                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.RadiologyOrders)   ''("LM_Orders")
                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.Tasks)  ''("Tasks_MST")
                    ''''''''''''''''''''Integrated by Mayuri:20100802 - for EM coding changes
                    If (gblnEMEnable) Then  ''flag check before loading enm data
                        myCaller.FillEMData()
                        '''''''''''''''To count checked values
                        myCaller._PnlName = "PnlManagementOption"

                        myCaller.DisplayEMCodeValueOfCheckonPanelTop()

                        myCaller._PnlName = "PnlLabs"
                        myCaller.DisplayEMCodeValueOfCheckonPanelTop()
                        myCaller._PnlName = "PnlXrayRadiology"
                        myCaller.DisplayEMCodeValueOfCheckonPanelTop()
                        myCaller._PnlName = "PnlOtherDiagno"
                        myCaller.DisplayEMCodeValueOfCheckonPanelTop()
                    End If
                    '''''''''''''''To count checked values
                    ''''''''''''''''''''Integrated by Mayuri:20100802  - for EM coding changes

                End If

            End If
            'code added by dipak 20090921  to reflect changes made on RadiologyOrders form to  liqiud data field's word document
            If Not IsNothing(myCaller1) Then
                If myCaller1.canselect = True Then
                    myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.RadiologyOrders)   ''("LM_Orders")
                    myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.Tasks)  ''("Tasks_MST")
                End If
            End If
            'end code added by dipak 20090921
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Patient orders closed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            Try
                'Application.DoEvents()
                Me.Dispose()
            Catch exdispose As Exception

            End Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Function Fill_TemplateGallery(ByVal TemplateID As Long) As Boolean
        ''''' Initialization
        Fill_TemplateGallery = True
        Try
            Dim strFileName As String
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Template
            objCriteria.PrimaryID = TemplateID
            ObjWord.DocumentCriteria = objCriteria
            strFileName = ObjWord.RetrieveDocumentFile()
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
            'UpdateLog("Fetch template from DB done")
            If (IsNothing(strFileName) = False) Then
                If strFileName <> "" Then
                    LoadWordUserControl(strFileName, True)
                    ''Set Cursor at start Postion of Documents
                    oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
            
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
    End Function

    Private Sub SaveOrderComment(ByVal IsFinished As Boolean, Optional ByVal bIsClose As Boolean = False)
        Try

            '''''  Save Order Comments TO ArrayList
            Dim isExceptionWhileCopingFile As Boolean = False
            wdOrders.Focus()
            If IsFinished = True Then

                If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                    oCurDoc.Application.ActiveDocument.Unprotect()
                End If
                'ObjWord = New clsWordDocument
                'ObjWord.CurDocument = oCurDoc
                'ObjWord.CleanupDoc()
                'oCurDoc = ObjWord.CurDocument
                'ObjWord = Nothing
                gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)
                gloWord.LoadAndCloseWord.LockFields(oCurDoc)
            End If


            If pnlGloUC_TemplateTreeControl.Width.ToString <> "" Then
                objclsPatientOrders.SaveWidthInDatabase(gnLoginID, pnlGloUC_TemplateTreeControl.Width) ''''save width of panel in DB
            End If


            ''  wdOrders.Save()
            'gloWord.LoadAndCloseWord.SaveDSO(wdOrders, oCurDoc, oWordApp)
            'Dim strFileName As String
            'strFileName = ExamNewDocumentName
            ' '' Instead of saving it using dso framer control ,using ocurdoc to save  #gloEMR - Order templates - Application gives an exception when user clicks on Save&Cls button.
            'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            'Try
            '    FileSystem.FileCopy(oCurDoc.FullName, strFileName)
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    ex = Nothing
            '    isExceptionWhileCopingFile = True
            'End Try
            'If (isExceptionWhileCopingFile) Then
            '    oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            '    wdOrders.Close()
            'End If
            Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdOrders, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, bIsClose)

            Dim myBinaray As Object = Nothing
            If (IsNothing(myByte) = False) Then
                myBinaray = CType(myByte, Object)
            End If

            With C1OrderDetails
                Dim lst As New myList
                lst.Index = .GetData(.RowSel, COLUM_ID)     '' TestID
                Dim objword As New clsWordDocument

                lst.TemplateResult = myBinaray 'CType(objword.ConvertFiletoBinary(strFileName), Object) '' Template 
                objword = Nothing
                lst.IsFinished = IsFinished

                '' Also Put the order Status in Grid 
                .SetData(.RowSel, COLUM_ISFINISHED, IsFinished)
                If IsFinished = True Then

                    .Rows(.RowSel).AllowEditing = True
                    .Cols(COLUM_NUMVALUE).AllowEditing = True

                End If

                '' Numeric Value
                lst.Value = .GetData(.RowSel, COLUM_NUMVALUE)     ''  Numeric Value

                Dim i As Integer
                Dim blnTemplateExist As Boolean
                If ArrLst.Count > 0 Then
                    ''''' if Arraylst already contains some records then check for duplicate & Overwrite it
                    For i = 0 To ArrLst.Count - 1

                        'If CType(ArrLst(i), myList).Index = C1OrderDetails.GetData(C1OrderDetails.Row, COLUM_ID) Then
                        If CType(ArrLst(i), myList).HistoryItem = C1OrderDetails.GetData(C1OrderDetails.Row, COLUM_NAME) Then
                            ''''' Update Order Comment
                            CType(ArrLst(i), myList).TemplateResult = lst.TemplateResult
                            If IsNothing(lst.TemplateResult) = False Then
                                ''''' If Order comments are enter then Indicate it by ForeColor as RED
                                .Rows(.RowSel).StyleDisplay.ForeColor = Color.Red
                            Else
                                ''''' If Order comments are not enter then Indicate it by ForeColor as GREEN
                                .Rows(.RowSel).StyleDisplay.ForeColor = Color.Green
                            End If

                            ''''' Update Status of Order 
                            CType(ArrLst(i), myList).IsFinished = IsFinished
                            ''''' Status for Whether comment has assigned or not.
                            CType(ArrLst(i), myList).OrderComment = myList.enumOrderComment.Assigned
                            ''''' Update Numeric Value
                            CType(ArrLst(i), myList).Value = .GetData(.RowSel, COLUM_NUMVALUE)

                            blnTemplateExist = True
                            Exit For
                        Else
                            blnTemplateExist = False
                        End If
                    Next
                    If blnTemplateExist = False Then
                        ArrLst.Add(lst)
                        '''''blnFormExist = True
                    End If
                Else
                    ArrLst.Add(lst)
                    '''''blnFormExist = True
                End If
                blnSaved = True
                blnChangesMade = True
                If Not bIsClose Then
                    'If (isExceptionWhileCopingFile) Then
                    '    LoadWordUserControl(strFileName, True)
                    'End If
                    tmrDocProtect.Enabled = False
                    ''''' to Hide Protection Bar when Document is Finished
                    If IsFinished Then
                        '' Save Btn is Always INVisible when doc is Finished(Protested)
                        '' Initalise Timer
                        oCurDoc.Application.ActiveDocument.Protect(Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyComments)
                        tmrDocProtect.Enabled = True
                        tmrDocProtect.Interval = 10  'change made against problem 00000602
                    End If
                    oCurDoc.Saved = True
                Else
                    pnlOrderComments.Visible = False
                    pnlOrderDetails.Visible = True

                    If (IsNothing(oCurDoc) = False) Then
                        Try
                            Marshal.ReleaseComObject(oCurDoc)
                        Catch ex As Exception


                        End Try
                        oCurDoc = Nothing

                    End If

                End If
            End With
            'If IO.File.Exists(strFileName) Then
            '    Try
            '        IO.File.Delete(strFileName)
            '    Catch ex As Exception
            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '        ex = Nothing
            '    End Try
            'End If
            ''tsbtn_ShowHide.Text = "  Show  "                         ''''''''Added by Anil on 03/10/2007 at 5:59 p.m.
            tsbtn_ShowHide.Text = "  Sh&ow  "                         ''''''''Added by Anil on 03/10/2007 at 5:59 p.m.

            tsbtn_ShowHide.ToolTipText = "Show Patient Orders"       '''''''' These two code lines are added to get "Show"  caption at Show/Hide button after closing the Order Comment window at toolstrip .

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
    End Sub

    Private Sub CloseOrderComment()

        If IsNothing(oCurDoc) = False Then
            If oCurDoc.Saved = False Then 'And CType(TrvOrderDetails.SelectedNode, myTreeNode).IsFinished = False Then
                Dim result As Integer
                result = MessageBox.Show("Do you want to save the changes in Order Comments?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If result = Windows.Forms.DialogResult.Yes Then
                    With C1OrderDetails
                        If .GetData(.RowSel, COLUM_ISFINISHED) Then
                            Call SaveOrderComment(True, True)
                        Else
                            Call SaveOrderComment(False, True)
                        End If

                    End With


                    'Developer:Yatin N. Bhagat
                    'Date:12/20/2011
                    'Bug ID/PRD Name/Salesforce Case:GLO2011-0015304 Check boxes not consistently working in patient exam module
                    'oCurDoc = Nothing
                    'oWordApp = Nothing
                    'wdOrders.Close()

                    pnlLeft.Visible = True
                    pnlOrders.Visible = True
                    pnl_txtSearch.Enabled = True
                ElseIf result = Windows.Forms.DialogResult.No Then
                    blnSaved = False
                    pnlOrderComments.Visible = False
                    pnlOrderDetails.Visible = True

                    'oCurDoc = Nothing
                    'oWordApp = Nothing
                    'wdOrders.Close()
                    pnlLeft.Visible = True
                    pnlOrders.Visible = True
                    pnl_txtSearch.Enabled = True
                ElseIf result = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            Else
                blnSaved = False
                pnlOrderComments.Visible = False
                pnlOrderDetails.Visible = True
                pnlLeft.Visible = True
                pnlOrders.Visible = True
                pnl_txtSearch.Enabled = True
                oCurDoc = Nothing
                oWordApp = Nothing
                wdOrders.Close()

            End If
        Else
            blnSaved = False
            pnlOrderComments.Visible = False
            pnlOrderDetails.Visible = True
            pnlLeft.Visible = True
            pnlOrders.Visible = True
            pnl_txtSearch.Enabled = True
            oCurDoc = Nothing
            oWordApp = Nothing
            wdOrders.Close()
        End If
        ''tsbtn_ShowHide.Text = "  Show  "                         ''''''''Added by Anil on 03/10/2007 at 5:59 p.m.
        tsbtn_ShowHide.Text = "  Sh&ow  "                         ''''''''Added by Anil on 03/10/2007 at 5:59 p.m.

        tsbtn_ShowHide.ToolTipText = "Show Patient Orders"       '''''''' These two code lines are added to get "Show"  caption at Show/Hide button after closing the Order Comment window at toolstrip .
    End Sub

    Private Sub pnlOrderComments_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlOrderComments.VisibleChanged

        '10-May-13 Aniket: Do not uncomment the following as it does not open the Orders screen from Task. Bug 50452
        ''Memory Leak
        'If Not IsNothing(gloUC_PatientStrip1) Then
        '    gloUC_PatientStrip1.Dispose()
        '    gloUC_PatientStrip1 = Nothing
        'End If

        '16-May-13 Aniket: Resolving Memory Leaks
        'gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip Aniket: This is a test

        '16-May-13 Aniket: Resolving Memory Leaks
        If IsNothing(gloUC_PatientStrip1) = False Then


            If pnlOrderComments.Visible = True Then
                pnlBottom.Visible = False

                pnlOrderDetails.Dock = DockStyle.Top

                C1OrderDetails.Enabled = False

                pnlPrevOrders.Visible = False
                Splitter3.Visible = True
                ''''' 
                gloUC_PatientStrip1.DTPEnabled = False

            Else
                pnlBottom.Visible = True
                C1OrderDetails.Enabled = True
                pnlOrderDetails.Dock = DockStyle.Fill
                Splitter3.Visible = False

                gloUC_PatientStrip1.DTPEnabled = True
            End If

            If blnSaved = True Then

                blnChangesMade = True
            End If

        End If

    End Sub



    Public Sub InsertTest(ByVal strTestName As String)
        'Memory Leak
        'Dim objSender As Object = Nothing
        'Dim obje As EventArgs = Nothing
        Searchnode(trvOrders, strTestName)
        If IsNothing(trvSearchNode) = False Then
            trvOrders.SelectedNode = trvSearchNode
            'Call trvOrders_DoubleClick(objSender, obje)
        End If
    End Sub

    Private Sub SearchNode(ByVal Trv As TreeView, ByVal strText As String)
        Dim trvNde As myTreeNode
        For Each trvNde In Trv.Nodes
            Searchnode(trvNde, strText)
        Next
    End Sub

    Private Sub SearchNode(ByVal rootNode As myTreeNode, ByVal strText As String)
        If LCase(Trim(rootNode.Text)) = LCase(Trim(strText)) Then
            trvSearchNode = rootNode
            Exit Sub
        Else
            For Each childNode As myTreeNode In rootNode.Nodes
                If LCase(Trim(childNode.Text)) = LCase(Trim(strText)) Then
                    trvSearchNode = childNode
                    Exit Sub
                End If
                Searchnode(childNode, strText)
            Next
        End If
    End Sub


    Private Sub btnNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotes.Click
        If pnlOrderNotes.Visible = False Then
            pnlOrderNotes.Visible = True
            pnlOrderNotes.BringToFront()
            txtOrderNotes.Text = ""
            txtOrderNotes.Text = _strTasksNotes
            txtOrderNotes.Focus()
            ''added for bugid 90753
            If (IsSizeMaximized = False) Then
                pnlOrderNotes.Location = New Point(btnNotes.Location.X - 130, 50)
            Else
                pnlOrderNotes.Location = New Point(btnNotes.Location.X - 140, 110)

            End If


        End If

    End Sub

    Private Sub txtOrderNotes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderNotes.TextChanged
        If blnChangesMade = False Then
            blnChangesMade = True
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtn_OK.Click
        Try
            pnlOrderNotes.Visible = False
            If _strTasksNotes <> Trim(txtOrderNotes.Text) Then
                _strTasksNotes = Trim(txtOrderNotes.Text)

                blnChangesMade = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtn_Cancel.Click
        pnlOrderNotes.Visible = False
    End Sub
  
    Private Sub C1OrderDetails_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1OrderDetails.CellButtonClick
        Try

            With C1OrderDetails
                'OrderId = 0
                If .GetData(e.Row, COLUM_TESTGROUPFLAG) = "T" Then
                    If .ColSel = COLUM_BUTTON Then
                        '' Test is Seleced
                        Dim IsTemplateExists As Boolean = False
                        Dim lst As myList = Nothing
                        For i As Integer = 0 To ArrLst.Count - 1
                            'Memory Leak
                            'lst = New myList
                            lst = CType(ArrLst(i), myList)
                            'If lst.Index = .GetData(e.Row, COLUM_ID) Then
                            If lst.HistoryItem = .GetData(e.Row, COLUM_NAME) Then

                                '' Find if the Template is already Saved 
                                If IsNothing(lst.TemplateResult) = False Then
                                    '' Template is Saved for the Test
                                    IsTemplateExists = True

                                    Exit For
                                End If
                            End If
                            lst = Nothing 'Change made to solve memory Leak and word crash issue
                        Next

                        If IsTemplateExists = False Then


                            '******Shweta 20090828 *********'
                            'To check exeception related to word
                            If CheckWordForException() = False Then
                                Exit Sub
                            End If
                            'End Shweta

                            '' Template is Not Saved for the test then Open Template from TemplateGallery
                            If CType(C1OrderDetails.GetData(e.Row, COLUM_TEMPLATEID), Long) > 0 Then
                                '' Template is Assigned to the Test
                                Try
                                    Me.Cursor = Cursors.WaitCursor
                                    If Fill_TemplateGallery(CType(.GetData(e.Row, COLUM_TEMPLATEID), Long)) = True Then
                                        blnIsFinish = lst.IsFinished
                                        loadToolStrip()
                                        pnlOrders.Visible = False
                                        pnlLeft.Visible = False
                                        pnl_txtSearch.Enabled = False
                                        txtSearch.BackColor = Color.White
                                        pnlOrderComments.Visible = True
                                        pnlOrderComments.BringToFront()
                                    Else
                                        Me.Cursor = Cursors.Default
                                        MessageBox.Show("The template is not associated with selected Order.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    End If

                                    Me.Cursor = Cursors.Default

                                    lblHeading.Text = "  Order Comments For '" & .GetData(e.Row, COLUM_NAME) & "'"

                                Catch ex As Exception
                                    Me.Cursor = Cursors.Default
                                    'MessageBox.Show("There is some problem in Template. Please check it.",gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            Else
                                MessageBox.Show("The template is not associated with selected Order.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If

                        Else
                            Try
                                'UpdateLog("Open Template from arraylist ")
                                Me.Cursor = Cursors.WaitCursor

                                Dim objWord As New clsWordDocument
                                Dim strFileName As String
                                blnIsFinish = lst.IsFinished
                                loadToolStrip()
                                strFileName = ExamNewDocumentName
                                strFileName = objWord.GenerateFile(lst.TemplateResult, strFileName)

                                'wdOrders.Open(strFileName)
                                ' Dim oWordApp As Wd.Application = Nothing

                                Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdOrders, strFileName, oCurDoc, oWordApp)
                                If (strError <> String.Empty) Then
                                    Me.Cursor = Cursors.Default
                                    MessageBox.Show("Unable to open the Order Comments.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                End If

                                ''16-Jul-14 Aniket: Resolving Bug #71077:
                                'If Not IsNothing(wdOrders) Then

                                '    wdOrders.Titlebar = False
                                '    wdOrders.Menubar = False
                                'End If

                                If Not blnIsFinish And lst.OrderComment = myList.enumOrderComment.UnAssigned Then
                                    objWord.CurDocument = oCurDoc
                                    objCriteria = New DocCriteria
                                    ''//Mapping values for retrieving data from DB
                                    objCriteria.DocCategory = enumDocCategory.Orders

                                    objCriteria.PatientID = _patientID

                                    objCriteria.VisitID = _VisitID
                                    objCriteria.PrimaryID = lst.ID
                                    ''//Replace the Form Fields with data in the Word Document
                                    objWord.DocumentCriteria = objCriteria
                                    objWord.GetFormFieldData(enumDocType.None)
                                    oCurDoc = objWord.CurDocument
                                    objCriteria.Dispose()
                                    objCriteria = Nothing
                                End If
                                objWord = Nothing

                                Call SetWordObject(blnIsFinish)

                                ''''' already Existing Template is Opened 
                                blnSaved = False
                                ''''' No changes have been done yet
                                oCurDoc.Saved = True

                                Me.Cursor = Cursors.Default
                                'UpdateLog("Template Opened ")
                                pnlOrders.Visible = False
                                pnlLeft.Visible = False
                                pnl_txtSearch.Enabled = False
                                txtSearch.BackColor = Color.White
                                pnlOrderComments.Visible = True
                                lblHeading.Text = "   Order Comments For '" & .GetData(e.Row, COLUM_NAME) & "'"

                                If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                                    ogloVoice.MyWordToolStrip = tlsOrders
                                    ShowMicroPhone()
                                End If

                            Catch ex As Exception
                                Me.Cursor = Cursors.Default
                                MessageBox.Show("Unable to open the Order Comments.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                ex = Nothing
                            End Try
                        End If

                    ElseIf .ColSel = COLUM_DIAGNOSISBUTTON Then
                        ''''' BY Mahesh - 20070129 to Get Diagnosis 
                        If gblnICD9Driven Then
                            ''''added for bugid 91852 multiple instance of form opening

                            Dim LM_activeWindow As IntPtr = gloWord.WordDialogBoxBackgroundCloser.GetForegroundWindow()
                            If frm_diag Is Nothing Then
                                frm_diag = New frm_Diagnosis(_VisitID, 0, _patientID, , , LM_activeWindow)
                                frm_diag.StartPosition = FormStartPosition.CenterScreen
                                frm_diag.ShowInTaskbar = False
                                frm_diag.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                frm_diag.Close() 'Change made to solve memory Leak and word crash issue
                                frm_diag.Dispose()
                                frm_diag = Nothing
                            End If
                        Else
                            ''''added for bugid 91852 multiple instance of form opening
                            If (oTreatment) Is Nothing Then
                                oTreatment = New frm_Treatment(0, _VisitID, Now.Date, "", _patientID)
                                oTreatment.ShowDialog(IIf(IsNothing(oTreatment.Parent), Me, oTreatment.Parent))
                                oTreatment.Close() 'Change made to solve memory Leak and word crash issue
                                oTreatment.Dispose()
                                oTreatment = Nothing
                            End If
                        End If
                        Call Fill_Diagnosis(_VisitID)

                        ' '' 20071003 For Fill Diagnosis '
                        Dim csDia As CellStyle = Nothing 'C1OrderDetails.Styles.Add("Dia")
                        Try
                            If (C1OrderDetails.Styles.Contains("Dia")) Then
                                csDia = C1OrderDetails.Styles("Dia")
                            Else
                                csDia = C1OrderDetails.Styles.Add("Dia")
                            End If
                        Catch ex As Exception
                            csDia = C1OrderDetails.Styles.Add("Dia")
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try
                        '' Fill Values In ComboList of The Cell Style
                        csDia.ComboList = strDia
                        '''''
                        'C1OrderDetails.Cols(COLUM_DIAGNOSIS).Style = csDia
                        '' 
                        '' Assign Cell Style To Cell Range of Selected Cell
                        Dim rgDig As C1.Win.C1FlexGrid.CellRange = C1OrderDetails.GetCellRange(e.Row, COLUM_DIAGNOSIS, e.Row, COLUM_DIAGNOSIS)
                        rgDig.Style = csDia
                    End If

                End If
            End With

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'RemoveHandler gloUC_PatientStrip1.btnUpOrDownclick, AddressOf gloUC_PatientStrip1_btnUpOrDownclick
            'AddHandler gloUC_PatientStrip1.btnUpOrDownclick, AddressOf gloUC_PatientStrip1_btnUpOrDownclick
        End Try
    End Sub
    ''Integrated on 20101022 by Mayuri for signature
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(oProvider) Then
                oProvider.Dispose()
                oProvider = Nothing
            End If
        End Try
    End Function

    Private Sub C1OrderDetails_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1OrderDetails.AfterEdit
        Try

            If e.Col = COLUM_ISFINISHED Or e.Col = COLUM_NUMVALUE Or e.Col = COLUM_DIAGNOSIS Or e.Col = COLUM_STAUS Or e.Col = COLUM_LOINC_ID Or e.Col = COLUM_TEXT_COMMENTS Then
                With C1OrderDetails

                    If C1OrderDetails.GetData(e.Row, COLUM_TESTGROUPFLAG) = "T" Then

                        Dim i As Integer
                        Dim blnTemplateExist As Boolean

                        '13-May-13 Aniket: Resolving Bug 50516
                        If IsNothing(ArrLst) = False Then

                            If ArrLst.Count > 0 Then
                                ''''' if Arraylst already contains some records then check for duplicate & Overwrite it
                                For i = 0 To ArrLst.Count - 1

                                    'If CType(ArrLst(i), myList).Index = .GetData(.RowSel, COLUM_ID) Then
                                    If CType(ArrLst(i), myList).HistoryItem = .GetData(.RowSel, COLUM_NAME) Then
                                        ''''' Update Order Comment
                                        'CType(ArrLst(i), myList).TemplateResult = lst.TemplateResult
                                        ''''' Update Status of Order 
                                        CType(ArrLst(i), myList).IsFinished = .GetData(.RowSel, COLUM_ISFINISHED)
                                        ''''' Update Numeric Value
                                        CType(ArrLst(i), myList).Value = .GetData(.RowSel, COLUM_NUMVALUE)
                                        ''''' Update Diagnosis
                                        CType(ArrLst(i), myList).Description = .GetData(.RowSel, COLUM_DIAGNOSIS)

                                        CType(ArrLst(i), myList).DMSID = .GetData(.RowSel, COLUM_DMSID)
                                        CType(ArrLst(i), myList).DICOMID = .GetData(.RowSel, COLUM_DICOMID)
                                        CType(ArrLst(i), myList).Status = .GetData(.RowSel, COLUM_STAUS)
                                        CType(ArrLst(i), myList).TextComment = .GetData(.RowSel, COLUM_TEXT_COMMENTS)

                                        If CType(ArrLst(i), myList).LoincCode.Length > 50 Then
                                            MessageBox.Show("Please Enter valid Loinc code", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            CType(ArrLst(i), myList).LoincCode = .GetData(.RowSel, COLUM_LOINC_ID).ToString.Substring(0, 49)
                                        Else
                                            CType(ArrLst(i), myList).LoincCode = .GetData(.RowSel, COLUM_LOINC_ID)
                                        End If

                                        ''e.Row.StyleNew.BackColor = 

                                        blnTemplateExist = True
                                        Exit For
                                    Else
                                        blnTemplateExist = False
                                    End If
                                Next
                                '    If blnTemplateExist = False Then
                                '        ArrLst.Add(lst)
                                '        '''''blnFormExist = True
                                '    End If
                                'Else
                                '    ArrLst.Add(lst)
                                '    '''''blnFormExist = True
                            End If
                        End If

                        blnChangesMade = True
                    End If

                End With
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try

            Dim strSearch As String
            With txtSearch
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            With C1OrderDetails
                .Row = .FindRow(strSearch, 1, COLUM_NAME, False, False, True)
                If .Row > 0 Then
                    Exit Sub
                End If

                '' 20070921 - Mahesh - InString Search 
                Dim strNode As String = ""
                For i As Int16 = 1 To .Rows.Count - 1
                    strNode = ""
                    strNode = UCase(.GetData(i, COLUM_NAME).ToString.Trim)
                    If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
                        .Row = i
                        Exit Sub
                    End If
                Next
                '' ---
            End With


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub txtSearchOrders_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchOrders.TextChanged
        Try

            Dim strSearch As String
            With txtSearchOrders
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            With C1Orders
                .Row = .FindRow(strSearch, 1, COLUM_NAME, False, False, True)
                If .Row > 0 Then
                    Exit Sub
                End If

                '' 20070921 - Mahesh - InString Search 
                Dim strNode As String = ""
                For i As Int16 = 1 To .Rows.Count - 1
                    strNode = ""
                    strNode = UCase(.GetData(i, COLUM_NAME).ToString.Trim)
                    If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
                        .Row = i
                        Exit Sub
                    End If
                Next
                '' ---
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                With C1OrderDetails
                    If .RowSel >= 0 Then
                        .Select()
                        'CurrentRowIndex = 0
                    End If
                End With

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub txtSearchOrders_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchOrders.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                With C1Orders
                    If .RowSel >= 0 Then
                        .Select()
                        'CurrentRowIndex = 0
                    End If
                End With

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub C1Orders_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Orders.DoubleClick

        Try
            'gloC1FlexStyle.Style(C1Orders)
            If C1Orders.Row > 0 Then
                With C1Orders
                    Dim row As Integer = C1Orders.Row()
                    Dim rowsel As Integer = C1Orders.RowSel()

                    If .GetData(C1Orders.Row, COLUM_TESTGROUPFLAG) = "T" Then

                        Dim CategoryRow As C1.Win.C1FlexGrid.Row = C1Orders.Rows(C1Orders.Row).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root).Row
                        Dim GroupRow As C1.Win.C1FlexGrid.Row = C1Orders.Rows(C1Orders.Row).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row
                        Dim TestRow As C1.Win.C1FlexGrid.Row = C1Orders.Rows(C1Orders.Row).Node.Row

                        Call AddTest(CategoryRow, GroupRow, TestRow)
                    End If

                End With
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub AddTest(ByVal CategoryRow As C1.Win.C1FlexGrid.Row, ByVal GroupRow As C1.Win.C1FlexGrid.Row, ByVal TestRow As C1.Win.C1FlexGrid.Row)
        Try

            With C1OrderDetails
                Dim oCATNode As C1.Win.C1FlexGrid.Node
                Dim oGRNode As C1.Win.C1FlexGrid.Node
                Dim oTESTNode As C1.Win.C1FlexGrid.Node
                Dim _tmpRow As Integer


                '' Check For Category Node Is Added Or Not
                oCATNode = GetC1UniqueNode("C" & CategoryRow(COLUM_NAME), C1OrderDetails)
                If IsNothing(oCATNode) = False Then
                    oGRNode = GetC1UniqueNode(Convert.ToString(CategoryRow(COLUM_ID)) & "G" & Convert.ToString(GroupRow(COLUM_NAME)), C1OrderDetails)
                    If IsNothing(oGRNode) = False Then
                        oTESTNode = GetC1UniqueNode("T" & TestRow(COLUM_NAME), C1OrderDetails)
                        If IsNothing(oTESTNode) = False Then
                            '' If Test Is Already Exits then Exit The Function
                            Exit Sub
                        ElseIf IsNothing(oTESTNode) = True Then
                            '' If Test is not Exists then add Test node
                            oTESTNode = oGRNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, TestRow(COLUM_NAME))
                            _tmpRow = oGRNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                            If _tmpRow <> -1 Then
                                '' 
                                Call Add_Node(TestRow, _tmpRow)
                                _tmpRow = -1
                            End If
                        End If
                    ElseIf IsNothing(oGRNode) = True Then
                        '' If Group is not Exists then add Group node
                        oGRNode = oCATNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, GroupRow(COLUM_NAME))
                        _tmpRow = oCATNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                        If _tmpRow <> -1 Then
                            '' 
                            Call Add_Node(GroupRow, _tmpRow)
                            _tmpRow = -1
                        End If

                        '' Add Test Node to the Newly added Group Node
                        oTESTNode = oGRNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, TestRow(COLUM_NAME))
                        _tmpRow = oGRNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                        If _tmpRow <> -1 Then
                            '' Set Data 
                            Call Add_Node(TestRow, _tmpRow)
                            _tmpRow = -1
                        End If

                    End If

                    '//.Style = FillControl.Styles("CS_Category")

                ElseIf IsNothing(oCATNode) = True Then
                    ''''''''''
                    ''''' If Category is not Exists then add Category node
                    .Rows.Add()
                    _tmpRow = .Rows.Count - 1

                    With .Rows(.Rows.Count - 1)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        '' Make as Tree Node
                        .IsNode = True
                        .Node.Level = 0
                        .Node.Data = CategoryRow(COLUM_NAME)
                        oCATNode = .Node
                    End With

                    .SetData(.Rows.Count - 1, COLUM_IDENTITY, "C" & CategoryRow(COLUM_NAME))
                    .SetData(.Rows.Count - 1, COLUM_NUMVALUE, Nothing)
                    .SetData(.Rows.Count - 1, COLUM_ID, 0)
                    .SetData(.Rows.Count - 1, COLUM_TESTGROUPFLAG, "C")
                    .SetData(.Rows.Count - 1, COLUM_LEVELNO, 0)
                    .SetData(.Rows.Count - 1, COLUM_GROUPNO, 0)

                    '' If Group is not Exists then add Group node
                    oGRNode = oCATNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, GroupRow(COLUM_NAME))
                    _tmpRow = oCATNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                    If _tmpRow <> -1 Then
                        '' 
                        Call Add_Node(GroupRow, _tmpRow)
                        _tmpRow = -1
                    End If

                    '' Add Test Node to the Newly added Group Node
                    oTESTNode = oGRNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, TestRow(COLUM_NAME))
                    _tmpRow = oGRNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                    If _tmpRow <> -1 Then
                        '' Set Data 
                        Call Add_Node(TestRow, _tmpRow)
                        _tmpRow = -1
                    End If
                End If

            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub Add_Node(ByVal _ROW As C1.Win.C1FlexGrid.Row, ByVal _RowNo As Integer)
        Try
            Dim cStyle As C1.Win.C1FlexGrid.CellStyle
            With C1OrderDetails
                .Rows(_RowNo).AllowEditing = False
                .Rows(_RowNo).ImageAndText = True
                .Rows(_RowNo).Height = 24
                .SetData(_RowNo, COLUM_IDENTITY, _ROW(COLUM_IDENTITY))
                .SetData(_RowNo, COLUM_NUMVALUE, Nothing)
                '.SetData(_RowNo, COLUM_ID, _ROW(COLUM_ID))
                .SetData(_RowNo, COLUM_ID, 0)
                .SetData(_RowNo, COLUM_TESTGROUPFLAG, _ROW(COLUM_TESTGROUPFLAG))
                .SetData(_RowNo, COLUM_LEVELNO, _ROW(COLUM_LEVELNO))
                .SetData(_RowNo, COLUM_GROUPNO, _ROW(COLUM_GROUPNO))
                .SetData(_RowNo, COLUM_LOINC_ID, _ROW(COLUM_LOINC_ID))

                If _ROW(COLUM_TESTGROUPFLAG) = "T" Then
                    ''  IF is Test then 
                    .Rows(_RowNo).AllowEditing = True
                    .Cols(COLUM_NAME).AllowEditing = False
                    ''  SET TemplateID
                    .SetData(_RowNo, COLUM_TEMPLATEID, _ROW(COLUM_TEMPLATEID))
                    C1OrderDetails.Cols(COLUM_NUMVALUE).Format = Format("##0.000")
                    .SetCellImage(_RowNo, COLUM_BUTTON, imgTreeView.Images(4))
                    ''  SET ShowComment Button
                    .SetData(_RowNo, COLUM_BUTTON, "")
                    'cStyle = .Styles.Add("BubbleValues")
                    Try
                        If (.Styles.Contains("BubbleValues")) Then
                            cStyle = .Styles("BubbleValues")
                        Else
                            cStyle = .Styles.Add("BubbleValues")
                        End If
                    Catch ex As Exception
                        cStyle = .Styles.Add("BubbleValues")
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    cStyle.ComboList = "..."
                    '.CellButtonImage = imgTreeView.Images(0)
                    Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_RowNo, COLUM_BUTTON, _RowNo, COLUM_BUTTON)
                    rgBubbleValues.Style = cStyle


                    ''''' 20070129 For Fill Diagnosis '
                    Dim csDia As CellStyle '= .Styles.Add("Dia")
                    Try
                        If (.Styles.Contains("Dia")) Then
                            csDia = .Styles("Dia")
                        Else
                            csDia = .Styles.Add("Dia")
                        End If
                    Catch ex As Exception
                        csDia = .Styles.Add("Dia")
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    '' Fill Values In ComboBox
                    csDia.ComboList = strDia
                    '''''
                    .Cols(COLUM_DIAGNOSIS).Style = csDia
                    Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_RowNo, COLUM_DIAGNOSISBUTTON, _RowNo, COLUM_DIAGNOSISBUTTON)
                    rgDig.Style = cStyle
                    .Cols(COLUM_DIAGNOSIS).AllowEditing = True
                    ''''''''''''''
                    .SetData(_RowNo, COLUM_STAUS, "Ordered")
                    Dim csDia2 As CellStyle '= .Styles.Add("Sta")
                    Try
                        If (.Styles.Contains("Sta")) Then
                            csDia2 = .Styles("Sta")
                        Else
                            csDia2 = .Styles.Add("Sta")
                        End If
                    Catch ex As Exception
                        csDia2 = .Styles.Add("Sta")
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    '' Fill Values In ComboBox
                    csDia2.ComboList = strStatus                    '''''
                    .Cols(COLUM_STAUS).Style = csDia2
                    .Cols(COLUM_STAUS).AllowEditing = True
                    ''''''''''''''

                    ''''''''''''''''''''''
                    ''''' to Validate & Add Tests to Array list
                    '' Add to array list
                    Dim lst As New myList

                    lst.Index = .GetData(_RowNo, COLUM_ID)  '' TestID '' will be Zero due to Denormalization 
                    lst.TemplateResult = Nothing '' Template 
                    lst.IsFinished = False ''''' Order Status
                    lst.Value = .GetData(_RowNo, COLUM_NUMVALUE) ''  Numeric Value
                    lst.HistoryCategory = .Rows(_RowNo).Node.GetNode(NodeTypeEnum.Root).Data
                    lst.Group = .Rows(_RowNo).Node.GetNode(NodeTypeEnum.Parent).Data
                    lst.HistoryItem = .Rows(_RowNo).Node.Data
                    lst.TemplateResult = GetTemplateImage(CType(_ROW(COLUM_TEMPLATEID), Int64)) '' TEMPLATE FETCHED BEFORE IT SAVE.. AND HANDLED BY ABOVE FLAG.. WHEHTER ASSIGNED / UNASSIGNED
                    lst.OrderComment = myList.enumOrderComment.UnAssigned
                    lst.Status = .GetData(_RowNo, COLUM_STAUS)
                    lst.LoincCode = .GetData(_RowNo, COLUM_LOINC_ID)

                    Dim i As Integer
                    Dim blnTemplateExist As Boolean
                    If ArrLst.Count > 0 Then
                        ''''' if Arraylst already contains some records then check for duplicate & Overwrite it
                        For i = 0 To ArrLst.Count - 1
                            'If CType(ArrLst(i), myList).Index = .GetData(_RowNo, COLUM_ID) Then
                            If CType(ArrLst(i), myList).HistoryItem = .GetData(_RowNo, COLUM_NAME) Then
                                '' Found in ArrayList then Set Values in ArrayList to the Grid
                                ''''' Update Status of Order 
                                .SetData(_RowNo, COLUM_ISFINISHED, CType(ArrLst(i), myList).IsFinished)
                                ''''' Update Numeric Value
                                .SetData(_RowNo, COLUM_NUMVALUE, CType(ArrLst(i), myList).Value)

                                If IsDBNull(CType(ArrLst(i), myList).TemplateResult) = False Then
                                    If IsNothing(CType(ArrLst(i), myList).TemplateResult) = False Then
                                        ''''' If Order comments are entered then Indicate it by ForeColor as RED
                                        .Rows(_RowNo).StyleDisplay.ForeColor = Color.Red
                                    Else
                                        ''''' If Order comments are NOT entered then Indicate it by ForeColor as GREEN
                                        .Rows(_RowNo).StyleDisplay.ForeColor = Color.Green
                                    End If
                                Else
                                    .Rows(_RowNo).StyleDisplay.ForeColor = Color.Green
                                End If
                                blnTemplateExist = True
                                Exit For
                            Else
                                blnTemplateExist = False
                            End If
                        Next

                        '' 
                        If blnTemplateExist = False Then
                            ArrLst.Add(lst)
                            .Rows(_RowNo).StyleDisplay.ForeColor = Color.Green
                            '''''blnFormExist = True
                        End If
                    Else
                        ArrLst.Add(lst)
                        .Rows(_RowNo).StyleDisplay.ForeColor = Color.Green
                        '''''blnFormExist = True
                    End If
                    '''''''''''''''''''''
                End If

                '' New Test Added 
                blnChangesMade = True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub C1OrderDetails_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1OrderDetails.MouseDown
        Try

            With C1OrderDetails
                If C1OrderDetails.Rows.Count = 1 Then
                    ''Sandip  Darade 20090325
                    ''Remove context menu
                    If Not IsNothing(C1OrderDetails.ContextMenu) Then
                        'Try
                        '    If (IsNothing(C1OrderDetails.ContextMenu) = False) Then
                        '        C1OrderDetails.ContextMenu.Dispose()
                        '        C1OrderDetails.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        C1OrderDetails.ContextMenu = Nothing
                    End If
                    Exit Sub
                End If
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    .Select(r, True)
                    If r > 0 Then
                        If .GetData(r, COLUM_TESTGROUPFLAG) = "T" Then

                            If C1OrderDetails.GetData(r, COLUM_ISFINISHED) <> "True" Then
                                ContextMenu1.MenuItems.Item(0).Visible = True
                                If .GetData(r, COLUM_DMSID) = Nothing OrElse .GetData(r, COLUM_DMSID) = "0" Then
                                    ContextMenu1.MenuItems.Item(1).Text = "Scan Document"
                                Else
                                    ContextMenu1.MenuItems.Item(1).Text = "View Document"
                                End If
                            Else
                                ContextMenu1.MenuItems.Item(0).Visible = False
                                If .GetData(r, COLUM_DMSID) = Nothing OrElse .GetData(r, COLUM_DMSID) = "0" Then
                                    ContextMenu1.MenuItems.Item(1).Text = "Scan Document"
                                Else
                                    ContextMenu1.MenuItems.Item(1).Text = "View Document"
                                End If
                            End If
                            If .GetData(r, COLUM_DICOMID) = "0" OrElse .GetData(r, COLUM_DICOMID) = "" Then
                                ContextMenu1.MenuItems.Item(2).Text = "Import DICOM File"

                            Else
                                ContextMenu1.MenuItems.Item(2).Text = "View DICOM File"
                            End If
                            'Try
                            '    If (IsNothing(.ContextMenu) = False) Then
                            '        .ContextMenu.Dispose()
                            '        .ContextMenu = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            .ContextMenu = ContextMenu1
                        Else
                            'Try
                            '    If (IsNothing(.ContextMenu) = False) Then
                            '        .ContextMenu.Dispose()
                            '        .ContextMenu = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            .ContextMenu = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(.ContextMenu) = False) Then
                        '        .ContextMenu.Dispose()
                        '        .ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        .ContextMenu = Nothing
                    End If
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub mnuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDelete.Click
        Try
            With C1OrderDetails
                If .GetData(.Row, COLUM_ISFINISHED) <> False Then ''IF test is finished 
                    MessageBox.Show("The status of test is finished, you cannot delete this test.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    Exit Sub
                End If
                Dim i As Integer

                '' SUDHIR 20090717 ''
                Dim _HistoryCategory As String = .Rows(.Row).Node.GetNode(NodeTypeEnum.Root).Data
                Dim _Group As String = .Rows(.Row).Node.GetNode(NodeTypeEnum.Parent).Data
                Dim _HistoryItem As String = .Rows(.Row).Node.Data
                '' END SUDHIR ''

                For i = 0 To ArrLst.Count - 1
                    'If .GetData(.Row, COLUM_ID) = CType(ArrLst(i), myList).Index Then
                    If CType(ArrLst(i), myList).HistoryCategory = _HistoryCategory And CType(ArrLst(i), myList).Group = _Group And CType(ArrLst(i), myList).HistoryItem = _HistoryItem Then
                        ArrLst.RemoveAt(i)
                        Exit For
                    End If
                Next

                ''
                Dim _Node As C1.Win.C1FlexGrid.Node
                Dim _ParentNode As C1.Win.C1FlexGrid.Node
                Dim _CategoryNode As C1.Win.C1FlexGrid.Node
                _Node = .Rows(.Row).Node

                ''''' 
                '' If the Node have Subling nodes then Remove only that node
                If IsNothing(_Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.PreviousSibling)) = True AndAlso IsNothing(_Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.NextSibling)) = True Then
                    ''''' the node have not sublingnodes then check for its parentnode
                    _ParentNode = _Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)
                    '' If the ParentNode have Subling nodes then Remove only that node
                    If IsNothing(_ParentNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.PreviousSibling)) = True AndAlso IsNothing(_ParentNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.NextSibling)) = True Then
                        ''''' the _ParentNode have not sublingnodes then check for its parentnode i.e Category Node
                        _CategoryNode = _ParentNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)
                        ''''' If the ParentNode have Subling nodes then Remove only that node
                        ''''' Remove the Category Node
                        _CategoryNode.RemoveNode()
                    Else
                        ''''' If the parent Node have Sublings then Remove Only that Parent Node
                        _ParentNode.RemoveNode()
                    End If

                Else
                    ''''' If the Test Node have Sublings then Remove Only that Test Node
                    .RemoveItem(.Row)
                End If
                _IsTestDeleted = True

                '' as removed Tests i.e Record has been changed
                blnChangesMade = True '' Flag set

            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub C1Orders_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles C1Orders.KeyPress
        Try
            'gloC1FlexStyle.Style(C1Orders)
            If (e.KeyChar = ChrW(13)) Then   'If enter key is pressed
                Dim ex As System.EventArgs = Nothing
                C1Orders_DoubleClick(C1Orders, ex)
                '06-Mar-15 Aniket: Bug #80106: gloEMR>New Exam>Order Template>Focus is not coming back to the search box
                txtSearchOrders.Focus()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub C1OrderDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles C1OrderDetails.KeyDown

        With C1OrderDetails
            ''ascii code of rightclidk btnon KeyBoard  
            If e.KeyCode = 93 Then

                If .Row > 0 And .GetData(.Row, COLUM_TESTGROUPFLAG) = "T" Then
                    Dim rect As Rectangle = .GetCellRect(.Row, .Col)
                    Dim pt As New Point(rect.X + 100, rect.Y + 10)

                    ContextMenu1.Show(C1OrderDetails, pt)
                    'Try
                    '    If (IsNothing(.ContextMenu) = False) Then
                    '        .ContextMenu.Dispose()
                    '        .ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    .ContextMenu = ContextMenu1
                Else
                    'Try
                    '    If (IsNothing(.ContextMenu) = False) Then
                    '        .ContextMenu.Dispose()
                    '        .ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    .ContextMenu = Nothing
                End If
            Else
                'Try
                '    If (IsNothing(.ContextMenu) = False) Then
                '        .ContextMenu.Dispose()
                '        .ContextMenu = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                .ContextMenu = Nothing
            End If
        End With
    End Sub

    Private Sub C1OrderDetails_ComboDropDown(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1OrderDetails.ComboDropDown
        Try
            Call Fill_Diagnosis(_VisitID)

            With C1OrderDetails
                ''''' 20070129 For Fill Diagnosis '
                Dim csDia As CellStyle '= .Styles.Add("Dia")
                Try
                    If (.Styles.Contains("Dia")) Then
                        csDia = .Styles("Dia")
                    Else
                        csDia = .Styles.Add("Dia")
                    End If
                Catch ex As Exception
                    csDia = .Styles.Add("Dia")
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                '' Fill Values In ComboBox
                csDia.ComboList = strDia
                '''''
                .Cols(COLUM_DIAGNOSIS).AllowEditing = True
                .Cols(COLUM_DIAGNOSIS).Style = csDia

                Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(.Row, COLUM_DIAGNOSIS, .Row, COLUM_DIAGNOSIS)
                rgDig.Style = csDia
                .Refresh()
                '''''
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub gloUC_PatientStrip1_Date_Validating() Handles gloUC_PatientStrip1.Date_Validating
        Try
            'gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip
            If _VisitDate = Format(gloUC_PatientStrip1.DTPValue, "MM/dd/yyyy") & " " & Format(gloUC_PatientStrip1.DTPValue, "Short Time") Then
                Exit Sub
            End If

            _VisitDate = Format(gloUC_PatientStrip1.DTPValue, "MM/dd/yyyy") & " " & Format(gloUC_PatientStrip1.DTPValue, "Short Time")

            _VisitID = GenerateVisitID(_VisitDate, _patientID)

            '' To Get the Providers Information 
            '26-May-14 Aniket: Resolving Bug #67061. Get Patient Provider instead of Visit Provider
            Call Fill_ProviderInfo(_patientID)
            ''

            Call LoadArrayList(_VisitID, _VisitDate)

            If ArrLst.Count > 0 Then
                blnModify = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub mnuScanDocument_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuScanDocument.Click
        If mnuScanDocument.Text = "Scan Document" Then
            ScanDoucment()
        Else
            ViewDocument()
        End If

    End Sub

    Public Sub ScanDoucment()


        Dim _ScanContainerID As Int64 = 0
        Dim _ScanDocumentID As Int64 = 0
        Dim _result As Boolean = False




        Dim _ScanDocFlag As Boolean = True
        If _ScanDocFlag = True Then
            If gloEDocumentV3.eDocManager.eDocValidator.IsCategoryExists(0, gDMSCategory_Radiology, gClinicID) = False Then
                MessageBox.Show("DMS Category for orders has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _ScanDocFlag = False
            End If
        End If
        '//Check for DMS Path & Patient Directive Category


        If _ScanDocFlag = True Then
            'Memory Leak
            'Dim arrDocumentInfo As New ArrayList

            Dim strDocumentInfo As String = ""
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '_result = Set_ScanDocumentEvent(gnPatientID, gDMSCategory_Radiology, _ScanContainerID, _ScanDocumentID)
            _result = Set_ScanDocumentEvent(_patientID, gDMSCategory_Radiology, _ScanContainerID, _ScanDocumentID)
            'end modification

            If _ScanDocumentID <> 0 Then
                With C1OrderDetails

                    .SetData(C1OrderDetails.Row, COLUM_DMSID, _ScanDocumentID)
                    Dim i As Integer
                    Dim blnTemplateExist As Boolean
                    If ArrLst.Count > 0 Then
                        ''''' if Arraylst already contains some records then check for duplicate & Overwrite it
                        '' SUDHIR 20090717 ''
                        Dim _HistoryCategory As String = .Rows(.RowSel).Node.GetNode(NodeTypeEnum.Root).Data
                        Dim _Group As String = .Rows(.RowSel).Node.GetNode(NodeTypeEnum.Parent).Data
                        Dim _HistoryItem As String = .Rows(.RowSel).Node.Data
                        '' END SUDHIR ''

                        For i = 0 To ArrLst.Count - 1

                            'If CType(ArrLst(i), myList).Index = .GetData(.RowSel, COLUM_ID) Then
                            If CType(ArrLst(i), myList).HistoryCategory = _HistoryCategory And CType(ArrLst(i), myList).Group = _Group And CType(ArrLst(i), myList).HistoryItem = _HistoryItem Then
                                ''''' Update Order Comment
                                'CType(ArrLst(i), myList).TemplateResult = lst.TemplateResult
                                ''''' Update Status of Order 
                                CType(ArrLst(i), myList).IsFinished = .GetData(.RowSel, COLUM_ISFINISHED)
                                ''''' Update Numeric Value
                                CType(ArrLst(i), myList).Value = .GetData(.RowSel, COLUM_NUMVALUE)
                                ''''' Update Diagnosis
                                CType(ArrLst(i), myList).Description = .GetData(.RowSel, COLUM_DIAGNOSIS)

                                CType(ArrLst(i), myList).DMSID = .GetData(.RowSel, COLUM_DMSID) '.ToString()

                                CType(ArrLst(i), myList).DICOMID = .GetData(.RowSel, COLUM_DICOMID) '.ToString()
                                ''e.Row.StyleNew.BackColor = 

                                blnTemplateExist = True
                                Exit For
                            Else
                                blnTemplateExist = False
                            End If
                        Next
                        blnChangesMade = True
                    End If
                End With
            End If
        End If


    End Sub

    Public Sub ViewDocument()

        Dim DocumentID As Int64 = Convert.ToInt64(C1OrderDetails.GetData(C1OrderDetails.RowSel, COLUM_DMSID).ToString())

        If _patientID = 0 Then
            MessageBox.Show("Select patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010
        If MainMenu.IsAccess(False, _patientID) = False Then
            Exit Sub
        End If
        ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010


        If (DocumentID > 0) Then
            If IsNothing(oViewDocument) Then
                oViewDocument = New gloEDocumentV3.gloEDocV3Management()
            End If
            ''Added by Mayuri:20121205-Split Screen functionality
            oViewDocument.oPatientExam = New clsPatientExams

            oViewDocument.oPatientMessages = New clsMessage
            oViewDocument.oPatientLetters = New clsPatientLetters
            oViewDocument.oNurseNotes = New clsNurseNotes
            oViewDocument.oHistory = New clsPatientHistory
            oViewDocument.oLabs = New clsLabs
            oViewDocument.oDMS = New gloEDocumentV3.eDocManager.eDocGetList()
            oViewDocument.oRxmed = New clsPatientDetails
            oViewDocument.oOrders = New clsPatientDetails
            oViewDocument.oProblemList = New clsPatientProblemList

            oViewDocument.oCriteria = New DocCriteria
            oViewDocument.oWord = New clsWordDocument
            ''End 20121205
            Dim isItDialog As Boolean = oViewDocument.ShowEDocument(_patientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, Me.ParentForm, gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder, DocumentID)
            If (isItDialog = True) Then
                If Not IsNothing(oViewDocument) Then
                    'SLR: Dipose and then   

                    If (IsNothing(oViewDocument.oPatientExam) = False) Then
                        DirectCast(oViewDocument.oPatientExam, clsPatientExams).Dispose()
                        oViewDocument.oPatientExam = Nothing
                    End If
                    If (IsNothing(oViewDocument.oPatientMessages) = False) Then
                        DirectCast(oViewDocument.oPatientMessages, clsMessage).Dispose()
                        oViewDocument.oPatientMessages = Nothing
                    End If
                    If (IsNothing(oViewDocument.oPatientLetters) = False) Then
                        DirectCast(oViewDocument.oPatientLetters, clsPatientLetters).Dispose()
                        oViewDocument.oPatientLetters = Nothing
                    End If
                    If (IsNothing(oViewDocument.oNurseNotes) = False) Then
                        DirectCast(oViewDocument.oNurseNotes, clsNurseNotes).Dispose()
                        oViewDocument.oNurseNotes = Nothing
                    End If
                    If (IsNothing(oViewDocument.oHistory) = False) Then
                        DirectCast(oViewDocument.oHistory, clsPatientHistory).Dispose()
                        oViewDocument.oHistory = Nothing
                    End If
                    If (IsNothing(oViewDocument.oLabs) = False) Then
                        DirectCast(oViewDocument.oLabs, clsLabs).Dispose()
                        oViewDocument.oLabs = Nothing
                    End If
                    If (IsNothing(oViewDocument.oDMS) = False) Then
                        DirectCast(oViewDocument.oDMS, gloEDocumentV3.eDocManager.eDocGetList).Dispose()
                        oViewDocument.oDMS = Nothing
                    End If
                    If (IsNothing(oViewDocument.oRxmed) = False) Then
                        DirectCast(oViewDocument.oRxmed, clsPatientDetails).Dispose()
                        oViewDocument.oRxmed = Nothing
                    End If
                    If (IsNothing(oViewDocument.oOrders) = False) Then
                        DirectCast(oViewDocument.oOrders, clsPatientDetails).Dispose()
                        oViewDocument.oOrders = Nothing
                    End If
                    If (IsNothing(oViewDocument.oProblemList) = False) Then
                        DirectCast(oViewDocument.oProblemList, clsPatientProblemList).Dispose()
                        oViewDocument.oProblemList = Nothing
                    End If

                    If (IsNothing(oViewDocument.oCriteria) = False) Then
                        DirectCast(oViewDocument.oCriteria, DocCriteria).Dispose()
                        oViewDocument.oCriteria = Nothing
                    End If




                    oViewDocument.Dispose()
                End If
                oViewDocument = Nothing
            End If
            'oViewDocument.Dispose()

        End If
    End Sub

    Private Function Set_ScanDocumentEvent(ByVal PatientID As Int64, ByVal LabCategory As String, ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64) As Boolean
        Dim oScanDocument As New gloEDocumentV3.gloEDocV3Management()
        Dim _result As Boolean = False
        Try
            '_result = oScanDocument.ShowEScanner(PatientID, LabCategory, DateTime.Now.Year.ToString(), MonthName(Month(Date.Now)), gClinicID, gloEDocument.enum_DocumentEventType.ScanDocument, ScanContainerID, ScanDocumentID)
            _result = oScanDocument.ShowEScanner(PatientID, LabCategory, DateTime.Now.Year.ToString(), MonthName(Month(Date.Now)), gClinicID, gloEDocumentV3.Enumeration.enum_DocumentEventType.ScanDocument, ScanContainerID, ScanDocumentID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            oScanDocument.Dispose()
        End Try
        Return _result
    End Function

#Region "Word Template Functionalities"
    Private Sub FaxOrder(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)
        If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientOrders, _patientID, "", "", C1OrderDetails.GetData(C1OrderDetails.Row, COLUM_NAME), 0, _VisitID, 0, True, Me) = False Then
            Exit Sub
        End If
        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

        ''Unprotoct the document
        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If


        'Send the document for Printing i.e. to generate the TIFF File
        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, _patientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, C1OrderDetails.GetData(C1OrderDetails.Row, COLUM_NAME), clsPrintFAX.enmFAXType.PatientOrders) = False Then
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If

        'oTempDoc = Nothing
        If Not IsNothing(objPrintFAX) Then
            objPrintFAX.Dispose()
            objPrintFAX = Nothing
        End If


    End Sub

    Public Sub InsertSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            ImagePath = ""

            Dim frm As New FrmSignature
            frm.Owner = Me
            'frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.ShowDialog(frm.Parent)
            frm.Close() 'Change made to solve memory Leak and word crash issue
            frm.Dispose()
            frm = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''Dhruv 20091214 To add the signature into the Word document
    Public Sub AddSignature(ByVal sImagePath As String) Implements ISignature.AddSignature

        If Not IsNothing(oCurDoc) Then
            If File.Exists(sImagePath) Then
                'Memory Leak
                Dim oWord As New clsWordDocument
                oCurDoc.ActiveWindow.SetFocus()
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
    Public WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            ImagePath = Value
        End Set
    End Property

    Private Sub PrintOrder()

        If Not oCurDoc Is Nothing Then
            GeneratePrintFaxDocument()
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Patient order printed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        End If

    End Sub
    Private Sub GeneratePrintFaxDocument(Optional ByVal IsPrintFlag As Boolean = True)
        'Dim WDocViewType As Wd.WdViewType
        'Dim wordRefresh As New WordRefresh()
        If Not oCurDoc Is Nothing Then
            Dim PageNo As Integer = 0
            Dim totalPages As Integer = 0
            Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
            Dim Missing As Object = System.Reflection.Missing.Value

            Try

                '******Shweta 20090828 *********'
                'To check exeception related to word
                'If CheckWordForException() = False Then
                '    Exit Sub
                'End If
                'End Shweta

                Dim _SaveFlag As Boolean = False
                If oCurDoc.Saved Then
                    _SaveFlag = True
                End If
                'Dim sFileName As String = ExamNewDocumentName
                'Ashish added on 1st October 
                'to prevent screen from refreshing            
                'If IsPrintFlag Then
                '    'Ashish added on 31st October 
                '    'to prevent screen from refreshing
                '    'WDocViewType = oCurDoc.ActiveWindow.View.Type
                '    'wordRefresh.OptimizePerformance(False, oCurDoc, 0)
                'End If
                If IsNothing(wdOrders) = False AndAlso IsNothing(oWordApp) = False Then
                    Try
                        gloWord.LoadAndCloseWord.SaveDSO(wdOrders, oCurDoc, oWordApp)
                    Catch ex As Exception

                    End Try
                    ' wdOrders.Save(sFileName, True, "", "")
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


                    'oTempDoc = wdTemp.ActiveDocument
                    Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                    'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)
                    Try
                        PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, _patientID, AddressOf FaxOrder, totalPages, PageNo:=PageNo, iOwner:=Me)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    'If IsPrintFlag Then
                    '    'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                    '    '    oTempDoc.Unprotect()
                    '    'End If
                    '    Dim oPrint As New clsPrintFAX
                    '    ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    '    'oPrint.PrintDoc(oTempDoc, gnPatientID)
                    '    oPrint.PrintDoc(oTempDoc, _patientID)
                    '    'end modification
                    '    'Memory Leak
                    '    oPrint.Dispose()
                    '    oPrint = Nothing
                    'Else
                    '    Call FaxOrder(myLoadWord, oTempDoc)
                    'End If


                    'wdTemp.Close()


                    ''Memory Leak
                    'Me.Controls.Remove(wdTemp)
                    'wdTemp.Dispose()
                    myLoadWord.CloseApplicationOnly()
                    myLoadWord = Nothing
                    If Not IsNothing(oCurDoc) Then
                        oCurDoc.Saved = _SaveFlag
                    End If
                End If

                'LoadWordUserControl(sFileName, False)
                ' ''Set Cursor at start Postion of Documents
                'oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            Finally
                If (IsNothing(oCurDoc) = False) Then
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                End If

                If IsPrintFlag Then
                    'Ashish added on 31st October 
                    'to prevent screen from refreshing
                    'wordRefresh.OptimizePerformance(True, oCurDoc, WDocViewType)
                    'wordRefresh.Dispose()
                    'wordRefresh = Nothing
                    'WDocViewType = Nothing
                End If

            End Try
        End If

    End Sub

    Private Sub SendSecureMessage()
        Try
            If Not strProviderDirectAddress.Any() AndAlso gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation Is Nothing Then

                MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(_patientID)
            If sError <> "" Then
                MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                Exit Sub
            End If

            'If CheckWordForException() = False Then
            '    Exit Sub
            'End If

            Dim _SaveFlag As Boolean = False
            If oCurDoc.Saved Then
                _SaveFlag = True
            End If
            Try
                gloWord.LoadAndCloseWord.SaveDSO(wdOrders, oCurDoc, oWordApp)
            Catch ex As Exception

            End Try
            'Try
            '    oCurDoc.SaveAs(oCurDoc.FullName)
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    Try
            '        oCurDoc.Save()
            '    Catch ex1 As Exception

            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            '    End Try
            'End Try
            '        Dim sFileName As String = ExamNewDocumentName
            '       oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

            'wdOrders.Close()
            'wdTemp = New AxDSOFramer.AxFramerControl
            'Me.Controls.Add(wdTemp)
            'wdTemp.Location = New System.Drawing.Point(-50, -50)
            'wdTemp.Open(sFileName)
            'oTempDoc = wdTemp.ActiveDocument
            'oTempDoc.ActiveWindow.SetFocus()
            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            Dim osenddox As String = String.Empty
            Try
                osenddox = SendWord.MdlSendWord.SendWordDocument(myLoadWord, oCurDoc.FullName, _patientID)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

            'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
            '    oTempDoc.Unprotect()
            'End If

            'Dim oSendDoc As New clsPrintFAX
            'Dim osenddox As String
            'osenddox = oSendDoc.SendDoc(oTempDoc, _patientID)

            'If Not IsNothing(oSendDoc) Then
            '    oSendDoc.Dispose()
            'End If

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
                    Dim ofrmSendNewMail As New InBox.NewMail(_patientID, osenddox)
                    AddHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromLMOrders
                    If gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                        gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(Me.gloUC_PatientStrip1.ProviderID)
                        ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation
                    End If

                    ofrmSendNewMail.ShowInTaskbar = True
                    ofrmSendNewMail.ShowDialog()

                    RemoveHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromLMOrders
                    If Not IsNothing(ofrmSendNewMail) Then
                        ofrmSendNewMail.Close()
                    End If
                    ofrmSendNewMail = Nothing
                Else
                    MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            'LoadWordUserControl(sFileName, False)
            'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
            'oCurDoc.Saved = _SaveFlag

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
        End Try

    End Sub

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate
        If strstring = "ON" Then
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsFinish = False And pnlOrderComments.Visible = True Then
                If Not IsNothing(tlsOrders) Then
                    tlsOrders.MyToolStrip.Items("Mic").Visible = True
                    tlsOrders.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                    tlsOrders.MyToolStrip.ButtonsToHide.Remove(tlsOrders.MyToolStrip.Items("Mic").Name)
                End If
            End If
        ElseIf strstring = "OFF" Then
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsFinish = False Then
                If Not IsNothing(tlsOrders) Then
                    tlsOrders.MyToolStrip.Items("Mic").Visible = True
                    tlsOrders.MyToolStrip.ButtonsToHide.Remove(tlsOrders.MyToolStrip.Items("Mic").Name)
                End If
            Else
                If Not IsNothing(tlsOrders) Then
                    tlsOrders.MyToolStrip.Items("Mic").Visible = False
                    If tlsOrders.MyToolStrip.ButtonsToHide.Contains(tlsOrders.MyToolStrip.Items("Mic").Name) = False Then
                        tlsOrders.MyToolStrip.ButtonsToHide.Add(tlsOrders.MyToolStrip.Items("Mic").Name)
                    End If
                End If
            End If
            If Not IsNothing(tlsOrders) Then
                tlsOrders.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
            End If

        Else
            If bnlIsFaxOpened = False Then
                If Not IsNothing(ouctlgloUC_Addendum) Then
                    ouctlgloUC_Addendum.Navigate(strstring)
                Else

                    Try
                        If Not oCurDoc Is Nothing Then
                            oCurDoc.ActiveWindow.SetFocus()
                            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    'oCurDoc.ActiveWindow.SetFocus()
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
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                ex = Nothing
                            End Try
                        End If
                    End If
                Next
            End If

        End If
    End Sub

    Public Sub ImportDocument(ByVal nInsertScan As Int16)
        ''Insert File - 1
        ''Scan Images - 2
        ''Set focus to Wd object
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        oCurDoc.ActiveWindow.SetFocus()
        Try
            If nInsertScan = 1 Then
                Dim oFileDialogWindow As New System.Windows.Forms.OpenFileDialog
                '    oFileDialogWindow.Filter = "Text Files|*.txt|Wd Documents|*.doc|*.docx|Rich Text Format|*.rtf"
                oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf"
                '//oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Wd Documents (*.doc)|*.doc|Rich Text Format (*.rtf)|*.rtf"
                oFileDialogWindow.FilterIndex = 3
                oFileDialogWindow.Title = "Insert External Documents"
                oFileDialogWindow.Multiselect = False
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)
                    If oFile.Extension.ToUpper = UCase(".Doc") Or oFile.Extension.ToUpper = UCase(".Docx") Or oFile.Extension.ToUpper = UCase(".txt") Or oFile.Extension.ToUpper = UCase(".rtf") Then

                        'Insert file in Wd dobject
                        oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                    End If
                    oFile = Nothing
                End If
                'Memory Leak
                oFileDialogWindow.Dispose()
                oFileDialogWindow = Nothing

            ElseIf nInsertScan = 2 Then

                Dim oFiles As New ArrayList()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, Convert.ToInt64(gnLoginID), gClinicID, Application.StartupPath)
                oEDocument.ShowEScannerForImages(_patientID, oFiles)
                oEDocument.Dispose()

                Dim firstFlag As Boolean = True
                Dim i As Integer
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                For i = 0 To oFiles.Count - 1
                    If File.Exists(oFiles.Item(i)) Then

                        '' SUDHIR 20090619 '' 
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
                'Memory Leak
                If Not IsNothing(oFiles) Then
                    oFiles.Clear()
                    oFiles = Nothing
                End If

            End If
            'Bug #53440: gloEMR - Order Templates - Application does not allow text selection to user by mouse drag and drop.
            'Focus set to wdOrders to resolve the issue.                    
            wdOrders.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
    End Sub

    Private Sub ts_LM_Orders_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_LM_Orders.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case tsbtn_New.Tag
                    ToolBar_NewOrders()
                Case tsbtn_Save.Tag
                    ToolBar_SaveOrders()
                Case tsbtn_Finish.Tag
                    ToolBar_FinishOrders()
                Case tsbtn_Close.Tag
                    If _IsOrderDeleted = True Then
                        '  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Add, "Order deleted", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    ElseIf _IsGroupDeleted = True Then
                        ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Add, "Group deleted", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    ElseIf _IsTestDeleted = True Then
                        ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.Add, "Test deleted", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    Else
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.View, "Order viewed", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.View, "Order viewed", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    Me.Close()

                Case tsbtn_Delete.Tag
                    ToolBar_DeleteOrders()
                Case tsbtn_ShowHide.Tag
                    ToolBar_ShowHideOrders()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub loadToolStrip()
        If Not tlsOrders Is Nothing Then
            Me.pnlOrderComments.Controls.Remove(tlsOrders)

            tlsOrders.Dispose()
            tlsOrders = Nothing
        End If
        tlsOrders = New WordToolStrip.gloWordToolStrip
        tlsOrders.Dock = DockStyle.Top

        tlsOrders.ConnectionString = GetConnectionString()
        tlsOrders.UserID = gnLoginID

        tlsOrders.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider
        tlsOrders.ptProvider = oclsProvider.GetPatientProviderName(_patientID)
        tlsOrders.ptProviderId = oclsProvider.GetPatientProvider(_patientID)
        'Memory Leak
        If Not IsNothing(oclsProvider) Then
            oclsProvider.Dispose()
            oclsProvider = Nothing
        End If

        If blnIsFinish Then
            tlsOrders.FormType = WordToolStrip.enumControlType.OrdersAddendum
        Else

            tlsOrders.IsCoSignEnabled = gblnCoSignFlag
            tlsOrders.FormType = WordToolStrip.enumControlType.Orders

        End If
        If gblnAssociatedProviderSignature = True And blnIsFinish = False Then
            If (tlsOrders.MyToolStrip.Items.Contains(tlsOrders.MyToolStrip.Items("Insert Associated Provider Signature")) = True) Then
                tlsOrders.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            End If

            If (tlsOrders.MyToolStrip.ButtonsToHide.Contains(tlsOrders.MyToolStrip.Items("Save")) = True) Then
                tlsOrders.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            End If

        Else
            If (tlsOrders.MyToolStrip.Items.Contains(tlsOrders.MyToolStrip.Items("Insert Associated Provider Signature")) = True) Then
                tlsOrders.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            End If
            If (tlsOrders.MyToolStrip.ButtonsToHide.Contains(tlsOrders.MyToolStrip.Items("Save")) = True) Then
                tlsOrders.MyToolStrip.Items("Save").Visible = False
            End If

            If (tlsOrders.MyToolStrip.ButtonsToHide.Contains(tlsOrders.MyToolStrip.Items("Insert Associated Provider Signature")) = False) Then
                tlsOrders.MyToolStrip.ButtonsToHide.Add("Insert Associated Provider Signature")
            End If
            If (tlsOrders.MyToolStrip.ButtonsToHide.Contains(tlsOrders.MyToolStrip.Items("Save")) = False) And blnIsFinish = True Then
                tlsOrders.MyToolStrip.ButtonsToHide.Add("Save")
            End If

            If (tlsOrders.MyToolStrip.Items.Contains(tlsOrders.MyToolStrip.Items("Insert Associated Provider Signature")) = True) Then
                tlsOrders.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            End If
            If (tlsOrders.MyToolStrip.Items.Contains(tlsOrders.MyToolStrip.Items("Save")) = True) Then
                tlsOrders.MyToolStrip.Items("Save").Visible = False
            End If

        End If

        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = "False" Then
            tlsOrders.IsSecureMsgEnabled = False
        Else
            tlsOrders.IsSecureMsgEnabled = True
        End If



        ' ''
        If _blnRecordLock = True Then
            tlsOrders.FormType = WordToolStrip.enumControlType.Orders '' TODO: OrederCommments

        End If
        ' ''
        tlsOrders.Visible = True
        Me.pnlOrderComments.Controls.Add(tlsOrders)

        Me.pnlOrderComments.BringToFront()
        Me.Panel2.SendToBack()
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsFinish = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsOrders
                ShowMicroPhone()
            End If
        End If

        'ShowMicrophone()
    End Sub
    Private Sub tlsOrders_ToolStripButtonClick1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsOrders.ToolStripButtonClick
        Try
            'Select Case _Tag
            '    Case "Insert Associated Provider Signature"
            If IsNothing(oCurDoc) = False Then
                InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
            End If
            'End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        'Dim TestID As Long = C1OrderDetails.GetData(C1OrderDetails.Row, COLUM_ID)
        loadToolStrip()

        'wdOrders.Open(strFileName)
        ' Dim oWordApp As Wd.Application = Nothing

        gloWord.LoadAndCloseWord.OpenDSO(wdOrders, strFileName, oCurDoc, oWordApp)


        If blnGetData Then
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Orders

            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = _patientID
            'end modification

            objCriteria.VisitID = _VisitID
            objCriteria.PrimaryID = 0 'TestID
            ObjWord.DocumentCriteria = objCriteria
            ObjWord.CurDocument = oCurDoc
            ObjWord.GetFormFieldData(enumDocType.None)
            oCurDoc = ObjWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objCriteria.Dispose()
            objCriteria = Nothing
        Else
            ObjWord = New clsWordDocument
            ObjWord.CurDocument = oCurDoc
            ObjWord.HighlightColor()
            oCurDoc = ObjWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            ObjWord = Nothing
        End If
        'SetWordObject(False) '' COMMENT BY SUDHIR 20090529 '' IT WAS ALLOWING TO EDIT EVEN IF FINISHED ''
        SetWordObject(blnIsFinish)
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            ogloVoice.MyWordToolStrip = tlsOrders
            ShowMicroPhone()
        End If

    End Sub

    Private Sub wdOrders_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdOrders.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            'UpdateVoiceLog(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub wdOrders_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdOrders.OnDocumentClosed
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    '   Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If


        Catch ex As Exception
            'UpdateVoiceLog(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'GC.Collect()  ''code change for problem 00000591
            'GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub wdOrders_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdOrders.OnDocumentOpened
        oCurDoc = wdOrders.ActiveDocument
        oWordApp = oCurDoc.Application
        Try
            Try
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
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
                    '        r.SetRange(Sel.Start, Sel.End + 1)
                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then
                        '  Dim om As Object = System.Reflection.Missing.Value
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
                                'Memory Leak
                                oUnit = Nothing
                                oCnt = Nothing
                                oMove = Nothing
                            End If
                        End If

                        '  om = Nothing
                        f = Nothing
                        'o = Nothing
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
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

    Private Sub tlsOrders_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsOrders.ToolStripClick
        Try

            '******Shweta 20090828 *********'
            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If
            'End Shweta

            Select Case e.ClickedItem.Name

                Case "Mic"
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic started from tblButtons_ButtonClick in Patient Orders when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
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
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "witchOff Mic Completed from tblButtons_ButtonClick in Patient Orders when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)

                Case "Save"
                    TurnoffMicrophone()
                    If blnIsFinish Then
                        Call SaveOrderComment(True)
                    Else
                        Call SaveOrderComment(False)
                    End If

                Case "Save & Close"

                    TurnoffMicrophone()
                    Call SaveOrderComment(False, True)


                    'Developer:Yatin N. Bhagat
                    'Date:12/20/2011
                    'Bug ID/PRD Name/Salesforce Case:GLO2011-0015304 Check boxes not consistently working in patient exam module

                    wdOrders.Close()
                    'Bug #68698: 00000699: exception at Order template
                    oCurDoc = Nothing
                    oWordApp = Nothing
                    pnlLeft.Visible = True
                    pnlOrders.Visible = True
                    pnl_txtSearch.Enabled = True

                Case "Capture Sign"
                    Call InsertSignature()

                Case "Insert Sign"
                    'Call InsertProviderSignature()
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
                Case "Insert Associated Provider Signature"
                    If IsNothing(oCurDoc) = False Then
                        InsertProviderSignature()
                    End If

                Case "Insert CoSign"
                    Call InsertCoSignature()

                Case "Save & Finish"
                    TurnoffMicrophone()
                    Call SaveOrderComment(True, True)


                    'Developer:Yatin N. Bhagat
                    'Date:12/20/2011
                    'Bug ID/PRD Name/Salesforce Case:GLO2011-0015304 Check boxes not consistently working in patient exam module
                    'oCurDoc = Nothing
                    'oWordApp = Nothing
                    'wdOrders.Close()


                    pnlLeft.Visible = True
                    pnlOrders.Visible = True
                    pnl_txtSearch.Enabled = True
                Case "Print"
                    'NewOrder()
                    TurnoffMicrophone()
                    PrintOrder()

                Case "FAX"
                    bnlIsFaxOpened = True
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        TurnoffMicrophone()
                        GeneratePrintFaxDocument(False)
                        Me.Cursor = Cursors.Default

                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    bnlIsFaxOpened = False
                    ''''' New
                Case "Undo"
                    Call Undo()

                Case "Redo"
                    Call Redo()

                Case "Close"
                    TurnoffMicrophone()
                    CloseOrderComment()

                Case "Scan Documents"
                    TurnoffMicrophone()
                    ImportDocument(2)

                Case "Insert File"
                    TurnoffMicrophone()
                    ImportDocument(1)

                Case "Add Addendum"

                    Call InsertAddendum()

                Case "tblbtn_StrikeThrough"
                    '' chetan added on 25-oct-2010 for Strike Through
                    InsertStrike()
                Case "Export"
                    ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument

                    'If (IsNothing(oCurDoc) = False) Then
                    '    oCurDoc.Save()
                    'Else
                    '  wdOrders.Save()
                    gloWord.LoadAndCloseWord.SaveDSO(wdOrders, oCurDoc, oWordApp)
                    'End If

                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Order Comment", Me)
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    objword1 = Nothing
                    '' Export Function for Word Docs Integrated by dipak  as on 26 oct 2010
                Case "SecureMsg"
                    SendSecureMessage()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    '' chetan added on 25-oct-2010 for Strike Through
    Private Sub InsertStrike()
        Try
            Dim strThrough As String
            If Not IsNothing(oCurDoc) Then
                ' Bug #53361: gloEMR - Order Templates - Application does not strike through selected content in template.                 
                'oCurDoc.ActiveWindow.SetFocus() added to resolve Bug.
                oCurDoc.ActiveWindow.SetFocus()
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
                        If blnIsFinish = True Then
                            oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
                        End If
                        'Bug #53440: gloEMR - Order Templates - Application does not allow text selection to user by mouse drag and drop.
                        'Focus set to wdOrders to resolve the issue.                    
                        wdOrders.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            If blnIsFinish = True Then

                tmrDocProtect.Enabled = True
            End If

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
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = _patientID
            'end modification
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
                'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
                '' END SUDHIR ''

                oCurDoc.Application.Selection.TypeParagraph()
                '' By Mahesh Signature With Date - 20070113
                '''' Add Date Time When Signature is Inserted
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                ''''
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Co-Signature inserted", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Co-Signature inserted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Co-Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
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
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = _patientID
            'end modification
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID
            objWord.DocumentCriteria = objCriteria

            ImagePath = objWord.getData_FromDB("User_MST.imgSignature", "Provider Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
            ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)

            If File.Exists(ImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(ImagePath)
                oWord = Nothing

                oCurDoc.Application.Selection.TypeParagraph()
                'Dim clsExam As New clsPatientExams
                ''Memory Leak
                'clsExam.Dispose()
                'clsExam = Nothing
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from LMOrders", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            'Bug #53440: gloEMR - Order Templates - Application does not allow text selection to user by mouse drag and drop.
            'Focus set to wdOrders to resolve the issue.                    
            wdOrders.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub

    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        'Developer:Yatin N. Bhagat
        'Date:01/31/2012
        'Bug ID/PRD Name/Salesforce Case:Provider Signature Format Case
        'Reason: Comman Fucntionality is added 
        Try

            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            Dim objWord As New clsWordDocument
            Dim oclsProvider As New clsProvider
            Dim clsExam As New clsPatientExams
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, _patientID, _VisitID, blnSignClick)
            objCriteria = Nothing
            objWord = Nothing
            If pSign(2) = "1" Then
                If File.Exists(pSign(0)) Then
                    oCurDoc.ActiveWindow.SetFocus()

                    '' SUDHIR 20090619 '' 
                    Dim oWord As New clsWordDocument
                    oWord.CurDocument = oCurDoc
                    Dim myType As Wd.WdViewType = Nothing
                    Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                    oWord.InsertImage(pSign(0))
                    oWord = Nothing
                    'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
                    '' END SUDHIR ''
                    'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                    Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                    If wdRng.Tables.Count > 0 Then
                        'oCurDoc.Application.Selection.Move(1)
                        oCurDoc.Application.Selection.EndKey()
                    End If
                    'end code added by dipak 
                    oCurDoc.Application.Selection.TypeParagraph()
                    '' By Mahesh Signature With Date - 20070113
                    '' Add Date Time When Signature is Inserted
                    ''oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                    oCurDoc.Application.Selection.TypeText(Text:=pSign(1))
                    gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
            'Dispose object by mitesh
            If Not IsNothing(clsExam) Then
                'Memory Leak
                clsExam.Dispose()
                clsExam = Nothing
            End If
            'Memory Leak
            If Not IsNothing(oclsProvider) Then
                oclsProvider.Dispose()
                oclsProvider = Nothing
            End If
            'Bug #53440: gloEMR - Order Templates - Application does not allow text selection to user by mouse drag and drop.
            'Focus set to wdOrders to resolve the issue.                    
            wdOrders.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub InsertAddendum()
        Try
            If blnIsFinish Then

                pnlToolstrip.Visible = False
                pnlGloUC_TemplateTreeControl.Visible = False
                GloUC_TemplateTreeControl_Orders.Visible = False

                'memory Leak
                If Not IsNothing(ouctlgloUC_Addendum) Then
                    Me.Controls.Remove(ouctlgloUC_Addendum)
                    ouctlgloUC_Addendum.Dispose()
                    ouctlgloUC_Addendum = Nothing
                End If

                ouctlgloUC_Addendum = New gloUC_Addendum(_VisitID, OrderId, _patientID)
                blnIsAddendum = True
                Me.Controls.Add(ouctlgloUC_Addendum)
                ouctlgloUC_Addendum.Dock = DockStyle.Fill
                ouctlgloUC_Addendum.BringToFront()
                If gblnSpeakerExists = True And gblnVoiceEnabled = True Then
                    InitializeVoiceObjectForAddendum()
                    ShowMicroPhone()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub Undo()
        Try
            If IsNothing(oCurDoc) = False Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub Redo()
        Try
            If IsNothing(oCurDoc) = False Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SetWordObject(ByVal IsFinished As Boolean)
        If (IsNothing(oCurDoc)) Then
            Return
        End If
        oCurDoc.ActiveWindow.SetFocus()
        If IsFinished Then
            oCurDoc.FormFields.Shaded = False
        End If

        If IsFinished = True Or _blnRecordLock = True Then
            pnlGloUC_TemplateTreeControl.Visible = False
            GloUC_TemplateTreeControl_Orders.Visible = False
            GloUC_AddRefreshDic1.Visible = False
            If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
            End If
            ProtectDDL()
        Else
            pnlGloUC_TemplateTreeControl.Visible = True
            GloUC_TemplateTreeControl_Orders.Visible = True
            GloUC_AddRefreshDic1.Visible = True
        End If

        tmrDocProtect.Enabled = False
        ''''' to Hide Protection Bar when Document is Finished
        If IsFinished Then
            '' Save Btn is Always INVisible when doc is Finished(Protested)
            '' Initalise Timer
            tmrDocProtect.Enabled = True
            tmrDocProtect.Interval = 10  'change made against problem 00000602
        End If
        'UpdateLog("SetWordObject - E N D ")
    End Sub
    Public Sub ProtectDDL()
        For Each cntCtrl As Wd.ContentControl In oCurDoc.ContentControls
            If cntCtrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                cntCtrl.LockContents = True
            End If
        Next
    End Sub
    Private Sub tmrDocProtect_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDocProtect.Tick
        Try
            tmrDocProtect.Enabled = False  'change made against problem 00000602
            If (blnIsFinish Or _blnRecordLock) AndAlso blnIsAddendum = False Then
                If Not oCurDoc Is Nothing Then
                    'Bug #85105: 00000876: Document in procted mode while faxing
                    'oCurDoc.ActiveWindow.SetFocus()
                    Dim protectPane As Wd.TaskPane
                    protectPane = oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection)
                    protectPane.Visible = False

                    Marshal.ReleaseComObject(protectPane)
                    protectPane = Nothing
                    ' oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            tmrDocProtect.Enabled = True 'change made against problem 00000602

        End Try

    End Sub
#End Region

    Private Sub cmbTasks_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    ''Sandip Darade 20090316 
    ''To set the values for TestID and DICOMIDs 

    Private Sub C1OrderDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1OrderDetails.Click

        Try
            If C1OrderDetails.Rows.Count = 1 Then
                Exit Sub
            End If
            If C1OrderDetails.RowSel > 0 Then
                TestID = Convert.ToInt64(C1OrderDetails.GetData(C1OrderDetails.RowSel, COLUM_ID))
                If (Convert.ToString(C1OrderDetails.GetData(C1OrderDetails.RowSel, COLUM_DICOMID)) <> "") Then
                    sDICOMIDs = Convert.ToString(C1OrderDetails.GetData(C1OrderDetails.RowSel, COLUM_DICOMID))
                Else
                    sDICOMIDs = ""
                End If
            End If
            If C1OrderDetails.ColSel = COLUM_LOINC_ID Or C1OrderDetails.ColSel = COLUM_TEXT_COMMENTS Then
                blnChangesMade = True
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.ToString())
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub oViewDocument_EvnRefreshDocuments() Handles oViewDocument.EvnRefreshDocuments
        '''' Do any activity after closing view DMS form.
    End Sub


#Region "DICOM Methods"

    'sarika 20090211 DICOM
    Private Sub ViewDICOMFile()
        Dim tempDICOMID As String = Convert.ToString(C1OrderDetails.GetData(C1OrderDetails.RowSel, COLUM_DICOMID).ToString())
        ''Dim DocumentID As Int64 = Convert.ToInt64(C1OrderDetails.GetData(C1OrderDetails.RowSel, COLUM_DICOMID).ToString()) commented Sandip Darade 
        Dim DocumentID As Int64 = 0
        Dim _arrSpliter As String()
        _arrSpliter = tempDICOMID.Split(",") ''Split the string 

        If _arrSpliter(0).Length > 0 Then

            Dim DICOMID As String = _arrSpliter(0) ''First value
            DocumentID = Convert.ToInt64(DICOMID)
        End If
        'Dim DocumentID As Int64 = 
        Try
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'If gnPatientID = 0 Then
            '    MessageBox.Show("Select patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If
            If _patientID = 0 Then
                MessageBox.Show("Select patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            'end modification
            'If CheckPatientStatus(gnPatientID) = False Then
            '    Exit Sub
            'End If

            'check for DICOM Path
            If DICOMPath = "" Then
                MessageBox.Show("Set the DICOM path from Tool->Settings->Server Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim objDICOM As New clsDICOM
            Dim strFileName As String = ""
            Dim strFileExtn As String = ""

            If (DocumentID > 0) Then
                'get the dicom filename
                strFileName = objDICOM.GetDICOMFileName(DocumentID)
                strFileExtn = objDICOM.getFileExtn(DocumentID)

                Dim oViewDICOMFile As New frmgloDICOM(strFileName & strFileExtn, _patientID)
                Try


                    ''sandip Darade 20090826
                    ' ' to catch exception if the ocx is not registered
                    AddHandler Application.ThreadException, AddressOf OnUnhandledException

                    ''Sandip Darade 20090313
                    oViewDICOMFile.TestID = TestID
                    oViewDICOMFile.OrderID = OrderId
                    oViewDICOMFile.IsForTest = True
                    oViewDICOMFile.DICOMids = sDICOMIDs
                    oViewDICOMFile.ShowDialog(IIf(IsNothing(oViewDICOMFile.Parent), Me, oViewDICOMFile.Parent))
                    'oViewDICOMFile.Dispose()
                    'oViewDICOMFile = Nothing
                Catch ex As COMException
                    '   MessageBox.Show(comex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    MessageBox.Show("The required components for DICOM are missing. You need to install this component.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _IsExMsgshown = True
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing

                Catch ex As Exception
                    If (_IsExMsgshown = False) Then
                        MessageBox.Show("The required components for DICOM are missing. You need to install this component.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        _IsExMsgshown = True
                    End If
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing

                Finally
                    ' oViewDICOMFile = Nothing
                    If Not oViewDICOMFile Is Nothing Then
                        'Memory Leak
                        RemoveHandler Application.ThreadException, AddressOf OnUnhandledException
                        oViewDICOMFile.Dispose()
                        oViewDICOMFile = Nothing
                    End If

                    'Memory Leak
                    If Not IsNothing(objDICOM) Then
                        objDICOM = Nothing
                    End If
                End Try
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
    End Sub
    '---

    Public Sub ScanDICOMFile()
        'Dim _ScanContainerID As Int64 = 0
        'Dim _ScanDocumentID As Int64 = 0




        'Dim _result As Boolean = False

        ''//Check for DMS Path & Patient Directive Category

        'If _ScanDocFlag = True Then
        '    Dim arrDocumentInfo As New ArrayList
        '    Dim strDocumentInfo As String = ""
        '    _result = Set_ScanDocumentEvent(gnPatientID, gDMSCategory_Radiology, _ScanContainerID, _ScanDocumentID)

        '    If _ScanDocumentID <> 0 Then
        '        With C1OrderDetails

        '            .SetData(C1OrderDetails.Row, COLUM_DMSID, _ScanDocumentID)
        '            Dim i As Integer
        '            Dim blnTemplateExist As Boolean
        '            If ArrLst.Count > 0 Then
        '                ''''' if Arraylst already contains some records then check for duplicate & Overwrite it
        '                For i = 0 To ArrLst.Count - 1

        '                    If CType(ArrLst(i), myList).Index = .GetData(.RowSel, COLUM_ID) Then
        '                        ''''' Update Order Comment
        '                        'CType(ArrLst(i), myList).TemplateResult = lst.TemplateResult
        '                        ''''' Update Status of Order 
        '                        CType(ArrLst(i), myList).IsFinished = .GetData(.RowSel, COLUM_ISFINISHED)
        '                        ''''' Update Numeric Value
        '                        CType(ArrLst(i), myList).Value = .GetData(.RowSel, COLUM_NUMVALUE)
        '                        ''''' Update Diagnosis
        '                        CType(ArrLst(i), myList).Description = .GetData(.RowSel, COLUM_DIAGNOSIS)

        '                        CType(ArrLst(i), myList).DMSID = .GetData(.RowSel, COLUM_DMSID).ToString()

        '                        CType(ArrLst(i), myList).DICOMID = .GetData(.RowSel, COLUM_DICOMID).ToString()
        '                        ''e.Row.StyleNew.BackColor = 

        '                        blnTemplateExist = True
        '                        Exit For
        '                    Else
        '                        blnTemplateExist = False
        '                    End If
        '                Next
        '                blnChangesMade = True
        '            End If
        '        End With
        '    End If
        'End If



        Dim _DICOMID As Int64 = 0
        Dim _result As Boolean = False

        Try
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '_result = Set_ScanDICOMFile(gnPatientID, _DICOMID)
            _result = Set_ScanDICOMFile(_patientID, _DICOMID)
            'end modification

            'If LMDICOMID <> 0 Then
            'If sDICOMIDs <> "" Then
            With C1OrderDetails

                ''  .SetData(C1OrderDetails.Row, COLUM_DICOMID, LMDICOMID.ToString())'' Sandip Darade
                .SetData(C1OrderDetails.Row, COLUM_DICOMID, sDICOMIDs) '' Sandip Darade
                Dim i As Integer
                Dim blnTemplateExist As Boolean
                If ArrLst.Count > 0 Then
                    ''''' if Arraylst already contains some records then check for duplicate & Overwrite it

                    '' SUDHIR 20090717 ''
                    Dim _HistoryCategory As String = .Rows(.RowSel).Node.GetNode(NodeTypeEnum.Root).Data
                    Dim _Group As String = .Rows(.RowSel).Node.GetNode(NodeTypeEnum.Parent).Data
                    Dim _HistoryItem As String = .Rows(.RowSel).Node.Data
                    '' END SUDHIR ''

                    For i = 0 To ArrLst.Count - 1

                        'If CType(ArrLst(i), myList).Index = .GetData(.RowSel, COLUM_ID) Then
                        If CType(ArrLst(i), myList).HistoryCategory = _HistoryCategory And CType(ArrLst(i), myList).Group = _Group And CType(ArrLst(i), myList).HistoryItem = _HistoryItem Then
                            ''''' Update Order Comment
                            'CType(ArrLst(i), myList).TemplateResult = lst.TemplateResult
                            ''''' Update Status of Order 
                            CType(ArrLst(i), myList).IsFinished = .GetData(.RowSel, COLUM_ISFINISHED)
                            ''''' Update Numeric Value
                            CType(ArrLst(i), myList).Value = .GetData(.RowSel, COLUM_NUMVALUE)
                            ''''' Update Diagnosis
                            CType(ArrLst(i), myList).Description = .GetData(.RowSel, COLUM_DIAGNOSIS)

                            CType(ArrLst(i), myList).DMSID = .GetData(.RowSel, COLUM_DMSID) '.ToString()

                            CType(ArrLst(i), myList).DICOMID = .GetData(.RowSel, COLUM_DICOMID) '.ToString()
                            ''e.Row.StyleNew.BackColor = 

                            blnTemplateExist = True
                            Exit For
                        Else
                            blnTemplateExist = False
                        End If
                    Next
                    blnChangesMade = True
                End If
            End With
            'End If
        Catch ex As COMException
            'MessageBox.Show("The required components for DICOM are missing.  Please install.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try

    End Sub


    Private Function Set_ScanDICOMFile(ByVal PatientID As Int64, ByRef ScanDICOMID As Int64) As Boolean
        Dim _result As Boolean = False
        _IsExMsgshown = False
        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'oImportDICOM = New frmgloDICOM(gnPatientID, "Radiology Orders", ScanDICOMID)
        oImportDICOM = New frmgloDICOM(_patientID, "Radiology Orders", ScanDICOMID)
        'end modification
        Try

            '  ScanDICOMID = oImportDICOM.ImportDicomFile()
            ''Sandip Darade 20090826
            ' ' to catch exception if the ocx is not registered
            AddHandler Application.ThreadException, AddressOf OnUnhandledException
            ''Sandip Darade 20090313
            oImportDICOM.TestID = TestID
            oImportDICOM.OrderID = OrderId
            oImportDICOM.DICOMids = sDICOMIDs
            oImportDICOM.IsForTest = True
            ''Added 'Me' parameter to 'ShowDialog' method for show dialog box on widows 8 os as on 20121203
            oImportDICOM.ShowDialog(IIf(IsNothing(oImportDICOM.Parent), Me, oImportDICOM.Parent))
            ''End

            '   oImportDICOM.tlsDICOM.Items("tlsbtnOpen").PerformClick()
            ' ScanDICOMID = oImportDICOM._LMDICOMID
            _result = True
        Catch ex As COMException
            '  MessageBox.Show("my scnn dicom message", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '''' MessageBox.Show("The required components for DICOM are missing.  Please install.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If (_IsExMsgshown = False) Then
                MessageBox.Show("The required components for DICOM are missing. You need to install this component.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsExMsgshown = True
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        Catch ex As Exception
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '''''MessageBox.Show("The required components for DICOM are missing.  Please install.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If (_IsExMsgshown = False) Then
                MessageBox.Show("The required components for DICOM are missing. You need to install this component.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsExMsgshown = True
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        Finally
            '  _result = False
            'oImportDICOM = Nothing
            If Not oImportDICOM Is Nothing Then
                'Memory Leak
                RemoveHandler Application.ThreadException, AddressOf OnUnhandledException
                oImportDICOM.Dispose()
                oImportDICOM = Nothing
            End If

        End Try


        Return _result
    End Function


    Private Sub mnuDICOM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDICOM.Click
        'If mnuDICOM.Text = "View DICOM File" Then
        '    ViewDICOMFile()
        'Else
        ''Sandip Darade 20090824
        ''show message if no DICOM path set
        If DICOMPath = "" Then

            MessageBox.Show("Set the DICOM path from Tool->Settings->Server Path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        ScanDICOMFile()
        'End If

    End Sub


#End Region

#Region " Search TestNode in Previous Order Tree "
    ''Sudhir - 20090214 Searching Order Tests.
    Private Sub txtSearchTest_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchTest.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvPreviousOrders.Select()
        Else
            trvPreviousOrders.SelectedNode = trvPreviousOrders.Nodes.Item(0)
        End If
    End Sub

    Private Sub txtSearchTest_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchTest.TextChanged
        Try
            If Trim(txtSearchTest.Text) <> "" Then
                Dim MyNode As TreeNode  'MyNode is current/yesterday/last week/last month
                For Each MyNode In trvPreviousOrders.Nodes.Item(0).Nodes
                    Dim MyOrderNode As TreeNode  'MyOrderNode is OrderDate&Time
                    For Each MyOrderNode In MyNode.Nodes
                        Dim MyGroupNode As TreeNode   'MyGroupNode is Group Node
                        For Each MyGroupNode In MyOrderNode.Nodes
                            Dim MyTestNode As TreeNode  'MyTestNode is Test Node Which is tobe searched.
                            For Each MyTestNode In MyGroupNode.Nodes
                                If Trim(MyTestNode.Text) <> "" Then
                                    Dim str As String
                                    str = UCase(MyTestNode.Text)
                                    If Mid(str, 1, Len(UCase(Trim(txtSearchTest.Text)))) = UCase(Trim(txtSearchTest.Text)) Then
                                        MyNode.Parent.Expand()
                                        trvPreviousOrders.SelectedNode = trvPreviousOrders.SelectedNode.LastNode
                                        trvPreviousOrders.SelectedNode = MyTestNode
                                        txtSearchTest.Focus()
                                        Exit Sub
                                    End If
                                End If
                            Next
                        Next
                    Next
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
    End Sub

#End Region



    Private Sub mnuAddDICOM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ScanDICOMFile()
    End Sub

    ''Sandip Darade
    ''Reset the DICOMIds in grid    

    Private Sub oImportDICOM_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles oImportDICOM.FormClosing
        Try
            C1OrderDetails.SetData(C1OrderDetails.RowSel, COLUM_DICOMID, sDICOMIDs)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
    End Sub

    ''Update DICOMIds in LM_Order
    Private Sub UpdataeOrdersForDICOM()
        Try
            objclsPatientOrders.Update_LM_Orders(_patientID, ArrLst)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
    End Sub

    Private Function GetunFinishedTests() As String
        Dim _UnfinishedTests As String = ""
        If Not ArrLst Is Nothing Then

            If ArrLst.Count > 0 Then
                For i As Integer = 0 To ArrLst.Count - 1
                    'Memory Leak
                    Dim lst As myList
                    lst = CType(ArrLst(i), myList)
                    If (lst.IsFinished = False) Then
                        If (i = 0) Then
                            _UnfinishedTests = lst.HistoryItem
                        Else
                            _UnfinishedTests += "," + lst.HistoryItem
                        End If
                    End If

                Next

            End If
        End If
        Return _UnfinishedTests
    End Function


    '' SUDHIR 20090420 '' TO GET TEMPLATE IMAGE FROM DB
    Private Function GetTemplateImage(ByVal templateID As Int64) As Object
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim query As String = ""
        Dim dtResult As DataTable = Nothing

        Try
            oDB.Connect(False)
            query = "SELECT sDescription FROM TemplateGallery_MST WHERE nTemplateID = " & templateID & ""
            oDB.Retrive_Query(query, dtResult)
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            If IsNothing(dtResult) = False Then
                If dtResult.Rows.Count > 0 Then
                    Return CType(dtResult.Rows(0)("sDescription"), Object)
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

            Return Nothing
        Finally
            If IsNothing(dtResult) = False Then
                dtResult.Dispose()
                dtResult = Nothing
            End If
        End Try
    End Function

    '' END SUDHIR ''

    Private Sub C1OrderDetails_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1OrderDetails.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
#Region " '' Sandip Darade 20090930  expand or collapse all orders "

    '' Sandip Darade 20090930  expand or collapse all orders   
    Dim oMenu As New ToolStripMenuItem

    Private Sub C1Orders_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Orders.MouseDown



        Dim _Iscollapsed As Boolean = False
        Dim _HasRows As Boolean = False
        For Each c As C1.Win.C1FlexGrid.Row In C1Orders.Rows

            If (C1Orders.GetData(c.Index(), COLUM_TESTGROUPFLAG) = "C") Then

                If (c.Node.Children > 0) Then
                    _HasRows = True
                End If
                '' check if C1 grid has rows 
                _Iscollapsed = c.Node.Collapsed
                If (_Iscollapsed = True) Then '' check current status  if collapsed or expanded 
                    Exit For
                End If
            End If
        Next

        'Memory Leak
        If Not IsNothing(C1Orders.ContextMenuStrip) Then
            'For i As Integer = C1Orders.ContextMenuStrip.Items.Count - 1 To 0 Step -1
            '    C1Orders.ContextMenuStrip.Items.RemoveAt(i)
            'Next
            Try
                If (IsNothing(Cmnu_ExpClps) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Cmnu_ExpClps)
                    If (IsNothing(Cmnu_ExpClps.Items) = False) Then
                        Cmnu_ExpClps.Items.Clear()
                    End If
                    'Cmnu_ExpClps.Dispose()
                    'Cmnu_ExpClps = Nothing
                End If
            Catch

            End Try
            Try
                If (IsNothing(C1Orders.ContextMenuStrip) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(C1Orders.ContextMenuStrip)
                    If (IsNothing(C1Orders.ContextMenuStrip.Items) = False) Then
                        C1Orders.ContextMenuStrip.Items.Clear()
                    End If
                    'C1Orders.ContextMenuStrip.Dispose()
                    'C1Orders.ContextMenuStrip = Nothing
                End If
            Catch

            End Try

        End If
        'Try
        '    If (IsNothing(C1Orders.ContextMenuStrip) = False) Then
        '        C1Orders.ContextMenuStrip.Dispose()
        '        C1Orders.ContextMenuStrip = Nothing
        '    End If
        'Catch ex As Exception

        'End Try

        C1Orders.ContextMenuStrip = Nothing

        If (_HasRows = True) Then

            C1Orders.ContextMenuStrip = Cmnu_ExpClps
            If (_Iscollapsed = True) Then
                oMenu.Text = "Expand All"
                oMenu.Tag = True
                oMenu.Image = ImgOrders.Images(18)
            Else
                oMenu.Text = "Collapse All"
                oMenu.Tag = False
                oMenu.Image = ImgOrders.Images(17)
            End If

            oMenu.ForeColor = Color.FromArgb(31, 73, 125)
            Cmnu_ExpClps.Items.Add(oMenu)
            AddHandler oMenu.Click, AddressOf oMenuExpClpsClicked

        End If

    End Sub

    '' Sandip Darade 20090930  expand or collapse all orders 

    Private Sub oMenuExpClpsClicked(ByVal sender As Object, ByVal e As EventArgs)
        ExpndClpsTests(oMenu.Tag)
    End Sub

    '' Sandip Darade 20090930  
    ''expand or collapse all orders 

    Private Sub ExpndClpsTests(ByVal _IsCollapse As Boolean)

        For Each c As C1.Win.C1FlexGrid.Row In C1Orders.Rows

            If (C1Orders.GetData(c.Index(), COLUM_TESTGROUPFLAG) = "C") Then '' if catagory row 
                c.Node.Expanded = (_IsCollapse)
            End If
        Next

    End Sub

#End Region
    Private Sub C1Orders_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Orders.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    ''' <summary>
    ''' Trigger Voice commands
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
    ''' 
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsFinish = False Then
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
    ''' Add voice commands from custom collection to DgnStrings
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsFinish = False Then
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
    ''' Add Basic Voice commands to hashtable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBasicVoiceCommands() As Hashtable
        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save Comments", "Save")
        oHashtable.Add("Print Comments", "Print")
        oHashtable.Add("Fax Comments", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close Comments", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close Comments", "Close")
        oHashtable.Add("Finish Comments", "Save & Finish")
        Return oHashtable
    End Function

    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True AndAlso blnIsFinish = False AndAlso pnlOrderComments.Visible = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        End If
    End Sub

    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsFinish = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Initialise glovoice class
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeVoiceObject()
        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If
        Dim oHashtable As Hashtable = AddBasicVoiceCommands()
        ogloVoice = New ClsVoice(oHashtable)
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Orders
        ogloVoice.MyWordToolStrip = Me.tlsOrders
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "Orders"
        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsOrders_ToolStripClick)
        'ShowMicroPhone()

        'Memory Leak
        oHashtable.Clear()
        oHashtable = Nothing

    End Sub
    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
        End Get
    End Property
    ''' <summary>
    ''' Initialise Voice for Addendum
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeVoiceObjectForAddendum()
        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If
        Dim oAddendumHashtable As ArrayList = ouctlgloUC_Addendum.FillTemplateCommands(True)
        ogloVoice = New ClsVoice(oAddendumHashtable)
        ogloVoice.gloTreeView = ouctlgloUC_Addendum.trvTemplates
        ogloVoice.eVoiceAddendum = VoiceAddendum.eAddendum
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Orders
        ogloVoice.MyWordToolStrip = Me.ouctlgloUC_Addendum.tlsAddendum
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "Orders"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf Me.ouctlgloUC_Addendum.onToolStripClick)
        ogloVoice.AddVoiceCommands()

        'Memory Leak
        oAddendumHashtable.Clear()
        oAddendumHashtable = Nothing
    End Sub
    ''' <summary>
    ''' Close Addendum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ouctlgloUC_Addendum_OnAddendumClose(ByVal sender As Object, ByVal e As System.EventArgs) Handles ouctlgloUC_Addendum.OnAddendumClose
        Me.Controls.Remove(ouctlgloUC_Addendum)
        'Memory Leak
        ouctlgloUC_Addendum.Dispose()
        ouctlgloUC_Addendum = Nothing
        pnlToolstrip.Visible = True
        '_PatientStrip.Visible = True
        blnIsAddendum = False
    End Sub
    ''' <summary>
    ''' Save Addendum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ouctlgloUC_Addendum_OnAddendumSaved(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ouctlgloUC_Addendum.OnAddendumSaved
        Try
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
        Catch ex As Exception
            '20120806:: bug no:32437
            If ex.Message = "You cannot insert this selection into a table." Then
                MessageBox.Show("File selected for insertion is not supported and cannot be saved.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                oCurDoc.Application.Selection.TypeText(Text:="File selected for insertion is not supported and cannot be saved.")
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try

        Me.Controls.Remove(ouctlgloUC_Addendum)

        'Memory Leak
        ouctlgloUC_Addendum.Dispose()
        ouctlgloUC_Addendum = Nothing

        pnlToolstrip.Visible = True
        'm_PatientStrip.Visible = True
        blnIsAddendum = False

    End Sub


    ''Not to let the user delete finished tests
#Region "Delete Test"

    Private Sub mnu_DeleteTestOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_DeleteTestOrder.Click
        Dim Visitdate As Date
        Dim VisitID As Long
        Dim _TaskID As Long
        Dim _UnfinishedTests As String = ""
        Dim _bShowMessage As Boolean = False
        Dim _delrec As Boolean = False
        'Dim _bIsDeleted As Boolean = False


        ''If Order selected to delete 
        If trvPreviousOrders.SelectedNode.Level = 2 Then
            Dim oNode1 As TreeNode
            ''check if the order contains any finished tests
            For Each oNode1 In trvPreviousOrders.SelectedNode.Nodes
                Dim node As TreeNode
                For Each node In oNode1.Nodes
                    If node.Tag = "True" Then
                        _bShowMessage = True
                        Exit For
                    End If
                Next
                ''If order contains any finished test ask the user to coninue or not 

                If (_bShowMessage = True) Then
                    If MessageBox.Show("I see you are trying to delete order which has finished test(s). " & vbCrLf & "Deleting  finished test is not allowed. Do you want me to delete unfinished test(s)?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        ''if yes then exit for and coninue to delete
                        Exit For
                    Else
                        Exit Sub
                    End If
                End If
            Next
            ''''
            If (_bShowMessage = False) Then
                If MessageBox.Show("Are you sure to delete this order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    ''if yes then exit for and coninue to delete
                    _delrec = True '' chetan added on 29-10-2010
                    Visitdate = trvPreviousOrders.SelectedNode.Text
                    VisitID = trvPreviousOrders.SelectedNode.Tag
                    ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    '_TaskID = objclsPatientOrders.GetOrderTaskID(gnPatientID, Visitdate)
                    _TaskID = objclsPatientOrders.GetOrderTaskID(_patientID, Visitdate)
                    'end modification
                    Dim oNode As TreeNode
                    For Each oNode In trvPreviousOrders.SelectedNode.Nodes

                        Dim n As TreeNode
                        For Each n In oNode.Nodes
                            If n.Tag = "False" Then
                                _UnfinishedTests = n.Text
                                ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                                'objclsPatientOrders.DeleteOrders(_VisitID, gnPatientID, Visitdate, _TaskID, _UnfinishedTests)
                                objclsPatientOrders.DeleteOrders(_VisitID, _patientID, Visitdate, _TaskID, _UnfinishedTests)
                                'end modification
                                '_bIsDeleted = True
                            End If
                        Next
                    Next
                    _IsOrderDeleted = True
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Order deleted", gloAuditTrail.ActivityOutCome.Success)
                Else
                    Exit Sub
                End If
            Else
                Visitdate = trvPreviousOrders.SelectedNode.Text
                VisitID = trvPreviousOrders.SelectedNode.Tag

                ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                '_TaskID = objclsPatientOrders.GetOrderTaskID(gnPatientID, Visitdate)
                _TaskID = objclsPatientOrders.GetOrderTaskID(_patientID, Visitdate)
                'end modification

                Dim oNode As TreeNode
                For Each oNode In trvPreviousOrders.SelectedNode.Nodes

                    Dim n As TreeNode
                    For Each n In oNode.Nodes
                        If n.Tag = "False" Then
                            _UnfinishedTests = n.Text
                            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                            'objclsPatientOrders.DeleteOrders(_VisitID, gnPatientID, Visitdate, _TaskID, _UnfinishedTests)
                            objclsPatientOrders.DeleteOrders(_VisitID, _patientID, Visitdate, _TaskID, _UnfinishedTests)
                            'end modification
                            '_bIsDeleted = True

                        End If
                    Next
                Next
            End If
            ''''''

            ''If group selected to delete 
        ElseIf (trvPreviousOrders.SelectedNode.Level = 3) Then

            ''check if the order contains any finished tests
            Dim node As TreeNode
            For Each node In trvPreviousOrders.SelectedNode.Nodes
                If node.Tag = "True" Then
                    _bShowMessage = True
                    Exit For
                End If
            Next

            ''If order contains any finished test ask the user to coninue or not 
            If (_bShowMessage = True) Then
                ' "I see you are trying to delete Order/Group which has finished test(s). " & vbCrLf & "Deleting  finished test is not allowed. Do you want me to delete unfinished test(s)?" 

                If MessageBox.Show("I see you are trying to delete group which has finished test(s). " & vbCrLf & "Deleting  finished test is not allowed. Do you want me to delete unfinished test(s)?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    _delrec = True '' chetan added on 29-10-2010
                    Visitdate = trvPreviousOrders.SelectedNode.Parent.Text
                    VisitID = trvPreviousOrders.SelectedNode.Parent.Tag
                    ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    '_TaskID = objclsPatientOrders.GetOrderTaskID(gnPatientID, Visitdate)
                    _TaskID = objclsPatientOrders.GetOrderTaskID(_patientID, Visitdate)
                    'end modification
                    Dim oNode As TreeNode
                    For Each oNode In trvPreviousOrders.SelectedNode.Nodes
                        If oNode.Tag = "False" Then
                            _UnfinishedTests = oNode.Text
                            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                            'objclsPatientOrders.DeleteOrders(_VisitID, gnPatientID, Visitdate, _TaskID, _UnfinishedTests)
                            objclsPatientOrders.DeleteOrders(_VisitID, _patientID, Visitdate, _TaskID, _UnfinishedTests)
                            'end modification
                            '_bIsDeleted = True

                        End If
                    Next
                    _IsGroupDeleted = True
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "unfinished test(s) deleted", gloAuditTrail.ActivityOutCome.Success)
                Else

                    Exit Sub
                End If
            Else
                If MessageBox.Show("Are you sure to delete this group?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    _delrec = True '' chetan added on 29-10-2010
                    Visitdate = trvPreviousOrders.SelectedNode.Parent.Text
                    VisitID = trvPreviousOrders.SelectedNode.Parent.Tag
                    ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    '_TaskID = objclsPatientOrders.GetOrderTaskID(gnPatientID, Visitdate)
                    _TaskID = objclsPatientOrders.GetOrderTaskID(_patientID, Visitdate)
                    'end modification
                    Dim oNode As TreeNode
                    For Each oNode In trvPreviousOrders.SelectedNode.Nodes
                        If oNode.Tag = "False" Then
                            _UnfinishedTests = oNode.Text
                            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                            'objclsPatientOrders.DeleteOrders(_VisitID, gnPatientID, Visitdate, _TaskID, _UnfinishedTests)
                            objclsPatientOrders.DeleteOrders(_VisitID, _patientID, Visitdate, _TaskID, _UnfinishedTests)
                            'end modification
                            '_bIsDeleted = True

                        End If
                    Next
                    _IsGroupDeleted = True
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Group deleted", gloAuditTrail.ActivityOutCome.Success)
                Else

                    Exit Sub
                End If
            End If


            ''If single test selected to delete
        Else


            If (trvPreviousOrders.SelectedNode.Tag = "False") Then
                If MessageBox.Show("Are you sure to delete this test?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    _delrec = True '' chetan added on 29-10-2010
                    Visitdate = trvPreviousOrders.SelectedNode.Parent.Parent.Text
                    VisitID = trvPreviousOrders.SelectedNode.Parent.Parent.Tag
                    ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    '_TaskID = objclsPatientOrders.GetOrderTaskID(gnPatientID, Visitdate)
                    _TaskID = objclsPatientOrders.GetOrderTaskID(_patientID, Visitdate)
                    'end modification
                    _UnfinishedTests = trvPreviousOrders.SelectedNode.Text
                    ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    'objclsPatientOrders.DeleteOrders(_VisitID, gnPatientID, Visitdate, _TaskID, _UnfinishedTests)
                    objclsPatientOrders.DeleteOrders(_VisitID, _patientID, Visitdate, _TaskID, _UnfinishedTests)
                    'end modification
                    _IsTestDeleted = True
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Test deleted", gloAuditTrail.ActivityOutCome.Success)
                End If
            Else
                MessageBox.Show("The status of test is finished, you cannot delete this test.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Exit Sub
            End If
        End If

        'If (_bIsDeleted = True) Then
        RefreshTreeView()
        ' End If

        '' chetan added on 29-10-2010 for refreshing grid if record is deleted
        If _delrec = True Then
            LoadArrayList(_VisitID, _VisitDate)
        End If
        '' chetan added on 29-10-2010 for refreshing grid if record is deleted
    End Sub

    ''Reload the treeview after deleting the test
    Private Sub RefreshTreeView()
        trvPreviousOrders.Nodes.Clear()
        If trvPreviousOrders.GetNodeCount(False) <= 0 Then
            trvPreviousOrders.Nodes.Add("Orders")
            trvPreviousOrders.Nodes(0).ImageIndex = 3
            trvPreviousOrders.Nodes(0).SelectedImageIndex = 3

            With trvPreviousOrders.Nodes.Item(0)
                .Nodes.Add("Current")
                .Nodes.Item(0).ForeColor = Color.Blue
                .Nodes.Add("YesterDay")
                .Nodes.Item(1).ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
                .Nodes.Add("Last Week")
                .Nodes.Item(2).ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
                .Nodes.Add("Last Month")
                .Nodes.Item(3).ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
                .Nodes.Add("Older")
                .Nodes.Item(4).ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)

            End With
        End If
        trvPreviousOrders.ExpandAll()
        Call RefreshOrderHistory()
    End Sub

    Private Sub trvPreviousOrders_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvPreviousOrders.MouseDown
        Try
            If e.Button = MouseButtons.Right Then
                'Try
                '    If (IsNothing(trvPreviousOrders.ContextMenu) = False) Then
                '        trvPreviousOrders.ContextMenu.Dispose()
                '        trvPreviousOrders.ContextMenu = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                trvPreviousOrders.ContextMenu = Nothing

                Dim trvNode As TreeNode
                trvNode = trvPreviousOrders.GetNodeAt(e.X, e.Y)
                If IsNothing(trvNode) = False Then
                    trvPreviousOrders.SelectedNode = trvNode

                    If trvPreviousOrders.SelectedNode.Level < 2 Then
                        Exit Sub
                    End If


                    If IsNothing(trvPreviousOrders.SelectedNode) = False Then

                        'Try
                        '    If (IsNothing(trvPreviousOrders.ContextMenu) = False) Then
                        '        trvPreviousOrders.ContextMenu.Dispose()
                        '        trvPreviousOrders.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvPreviousOrders.ContextMenu = Cmnu_DeleteTestOrder



                        If trvPreviousOrders.SelectedNode.Level = 2 Then

                            Cmnu_DeleteTestOrder.MenuItems(0).Text = "Delete Order"

                        ElseIf trvPreviousOrders.SelectedNode.Level = 3 Then

                            Cmnu_DeleteTestOrder.MenuItems(0).Text = "Delete Group"

                        Else

                            Cmnu_DeleteTestOrder.MenuItems(0).Text = "Delete Test"

                        End If

                    Else
                        'Try
                        '    If (IsNothing(trvPreviousOrders.ContextMenu) = False) Then
                        '        trvPreviousOrders.ContextMenu.Dispose()
                        '        trvPreviousOrders.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvPreviousOrders.ContextMenu = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

#End Region

    ' ' to catch exception if the ocx is not registered
    Private Sub OnUnhandledException(ByVal sender As Object, ByVal t As ThreadExceptionEventArgs)

        If (_IsExMsgshown = False) Then
            MessageBox.Show("The required components for DICOM are missing. You need to install this component.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            oImportDICOM.Dispose()
            _IsExMsgshown = True
        End If
    End Sub

    Private Sub gloUC_PatientStrip1_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gloUC_PatientStrip1.EnabledChanged

    End Sub

    Private Sub gloUC_PatientStrip1_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gloUC_PatientStrip1.LocationChanged

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearchOrders.Text = ""
    End Sub


    Private Sub txtClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClearSearch.Click
        txtSearch.Text = ""
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _patientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub InitiliseTemplateTreeControl()
        Try
            Dim objDocriteria As New DocCriteria
            Dim objClsWord_TemplateTree As New clsWordDocument
            GloUC_TemplateTreeControl_Orders.InitiliseControlParameter(GetConnectionString())
            GloUC_TemplateTreeControl_Orders.DocCriteria = objDocriteria
            GloUC_TemplateTreeControl_Orders.ObjClsWord = objClsWord_TemplateTree
            GloUC_TemplateTreeControl_Orders.ProviderId = gnSelectedProviderID
            GloUC_TemplateTreeControl_Orders.Fill_ExamTemplates(0)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub Treeview_NodeMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs, sFilename As System.String) Handles GloUC_TemplateTreeControl_Orders.Treeview_NodeMouseDoubleClick
        Try

            oCurDoc = wdOrders.ActiveDocument
            'oCurDoc.Application.ScreenUpdating = False
            oCurDoc.ActiveWindow.SetFocus()
            oCurDoc.Application.Selection.InsertFile(sFilename, "", False, False, False)
            wdOrders.Select()
            'GetdataFromOtherForms(enumDocType.None)
            ' oCurDoc.Application.ScreenUpdating = True

            Dim objclsworddoc As New clsWordDocument()  ''added for bugid 75024
            Dim objdoccriteria As New DocCriteria()
            objdoccriteria.DocCategory = enumDocCategory.Orders
            objdoccriteria.VisitID = GetVisitID(System.DateTime.Now, _patientID)
            objdoccriteria.PrimaryID = 0
            objdoccriteria.PatientID = _patientID
            objclsworddoc.DocumentCriteria = objdoccriteria
            objclsworddoc.PatientId = _patientID
            objclsworddoc.CurDocument = oCurDoc
            objclsworddoc.GetFormFieldData(gloEMRWord.enumDocType.None)
            oCurDoc = objclsworddoc.CurDocument
            objdoccriteria.Dispose()
            objdoccriteria = Nothing
            objclsworddoc = Nothing
            If IO.File.Exists(sFilename) Then
                IO.File.Delete(sFilename)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
    End Sub

    Public Sub calltoAddRefreshButtonControl()
        Try
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            ObjWord.WaitControlPanel = Me.pnl_wdOrders
            GloUC_AddRefreshDic1.CONNECTIONSTRINGs = GetConnectionString()
            GloUC_AddRefreshDic1.OBJWORDs = ObjWord
            Try
                If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                    DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                End If
            Catch

            End Try
            GloUC_AddRefreshDic1.OBJCRITERIAs = objCriteria
            GloUC_AddRefreshDic1.M_PATIENTIDs = _patientID
            GloUC_AddRefreshDic1.ObjFrom = Me
            ' Bug #52423: 00000486 : Orders
            ' to resolve issue order date passed to refresh control instead of due date    

            GloUC_AddRefreshDic1.DTLETTERDATEs = gloUC_PatientStrip1.DTP 'dtpDueDate
            GloUC_AddRefreshDic1.OWORDAPPs = oWordApp
            GloUC_AddRefreshDic1.wdPatientWordDocs = wdOrders
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    ''GLO2011-0015782 : One client workstation does not have diagnosis icon under orders menu
    'called when user tries to minimize width column contains dropdown listbox. 
    ''Start
    Private Sub C1OrderDetails_BeforeResizeColumn(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1OrderDetails.BeforeResizeColumn
        Try
            With C1OrderDetails
                If e.Col = COLUM_DIAGNOSIS Or e.Col = COLUM_STAUS Then
                    If .Cols(COLUM_DIAGNOSIS).Width < prevWidth_COLUM_DIAGNOSIS Then
                        e.Cancel = True
                    End If

                    If .Cols(COLUM_STAUS).Width < prevWidth_COLUM_STAUS Then
                        e.Cancel = True
                    End If
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.LabOrderRequest, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    'Shifted CDA Form calling functionality from Dashboard
    'to mdlGeneral.OpenCDA()
#Region "Call CDA Function from mdlGeneral"
    'Public Delegate Sub GenerateCDAFromLMOrders(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromLMOrders(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromLMOrders(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromLMOrders(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
#End Region

End Class

