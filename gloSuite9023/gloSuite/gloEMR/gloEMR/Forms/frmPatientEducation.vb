Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports System.Runtime.InteropServices
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary

Public Class frmPatientEducation
    Inherits System.Windows.Forms.Form
    Implements ISignature
    Implements gloVoice, IExamChildEvents
    Implements IHotKey
    Implements IPatientContext


#Region " Private Variables "

    'Added by Ashish
#Region "Treeview Search functionality"

    Private WithEvents TemplateSearchTimer As New Timer
    Private lst_Template_Nodes As List(Of TreeNode) = Nothing
    'Private lst_LabResults_Nodes As List(Of TreeNode) = Nothing

#End Region

    Private ogloVoice As ClsVoice
    Private m_VisitID As Long
    Private m_patientID As Long
    Private m_ExamID As Long
    Private m_arrTemplateID As ArrayList
    Private oPatientEducation As New clsPatientEducation
    Private WithEvents oCurDoc As Wd.Document
    Private blnTemplateExist As Boolean = False
    Private objCriteria As DocCriteria
    Private oWord As clsWordDocument
    Private myidx As Int32
    Private arrTemplates As New ArrayList
    Private dictionaryExistingTemplates As Dictionary(Of Long, myList)
    Private hashNewTemplates As HashSet(Of Long)
    Private _DocumentName As String
    Private _CurrentTemplateID As Int64
    Private _CurrentTemplateName As String
    Private _isEducationChanged As Boolean
    Private _isPnlHidden As Boolean = True
    Private _isOpenforInfobutton As Boolean = False
    Dim _VisitDate As Date
    Dim _VisitID As Int64

    Private ageDetail As AgeDetail

    Private nEducationID As Int64
    Private sDocumentName As String = ""
    Private blnSignClick As Boolean = False
    Private objWord As clsWordDocument

    '26-Apr-13 Aniket: Resolving Memory Leaks
    Private oContextMenu As ContextMenu
    Private oItem As MenuItem

    Private Source As Integer = 0
    Private ResourceType As Integer
    Private ResourceCategory As Integer
    Private clsInfobutton_Education As New gloEMRGeneralLibrary.clsInfobutton
    Private strBibliography As String
    Private strBibliographyDeveloper As String

    Dim _GridWidth As Integer
    Dim strsortorder As String
    Dim dvPatientEducationList As DataTable = Nothing

    Dim _speNotes As Object
    Dim _nOldEducationID As Int64

    Dim _ISTreeviewOpen = False
    Dim _FromGrid As Boolean = False

    Dim _FromOutside As Boolean = False
    Private TempExamID As Long = 0

    Private bnlIsFaxOpened As Boolean
    Private bnlIsEduPreviewOpened As Boolean
    Dim _IsModify As Boolean = False
#End Region

#Region " Public Variables "
    '' To Insert Signature
    Public Shared Imagepath As String
    Public Shared Formopen_Info As Boolean = False
    Public myCaller As frmPatientExam
    Public blnOpenFromExam As Boolean = False
    Public HomeUrl As String = ""
    Public BrowserLink As String = ""
    Public EducationID As Long

#End Region

#Region "All Property"

    'Is Close Click
    Private _closeClick As Boolean = Nothing
    Public Property CloseClick As Boolean
        Get
            Return _closeClick
        End Get
        Set(ByVal value As Boolean)
            _closeClick = value
        End Set
    End Property

    'VisitID
    Private _visID As Long
    Public Property VISID As Long
        Get
            Return _visID
        End Get
        Set(ByVal value As Long)
            _visID = value
        End Set
    End Property

    'TemplateID
    Private _tmpID As Long
    Public Property TMPID As Long
        Get
            Return _tmpID
        End Get
        Set(ByVal value As Long)
            _tmpID = value
        End Set
    End Property

    'VisitDate
    Private _visDate As Date
    Public Property VisDate As Date
        Get
            Return _visDate
        End Get
        Set(ByVal value As Date)
            _visDate = value
        End Set
    End Property

    'PatientID
    Private _patID As Long
    Public Property PATID As Long
        Get
            Return _patID
        End Get
        Set(ByVal value As Long)
            _patID = value
        End Set
    End Property

    'Exam ID
    Private _examID As Long
    Public Property EXAMID As Long
        Get
            Return _examID
        End Get
        Set(ByVal value As Long)
            _examID = value
        End Set
    End Property

    ''00000803: Patient Education. New Property added for exam visit Id
    Private _exam_VisitID As Long
    Public Property EXAM_VISITID() As Long
        Get
            Return _exam_VisitID
        End Get
        Set(ByVal value As Long)
            _exam_VisitID = value
        End Set
    End Property


    'SPE Notes
    Private _spe As Object
    Public Property SPE As Object
        Get
            Return _spe
        End Get
        Set(ByVal value As Object)
            _spe = value
        End Set
    End Property

    'Template Name
    Private _tempName As String
    Public Property TempName As String
        Get
            Return _tempName
        End Get
        Set(ByVal value As String)
            _tempName = value
        End Set
    End Property

    'Source
    Private _src As Integer
    Public Property Sourc As Integer
        Get
            Return _src
        End Get
        Set(ByVal value As Integer)
            _src = value
        End Set
    End Property

    'ResourceCategory
    Private _recCat As Integer
    Public Property ResourcCat As Integer
        Get
            Return _recCat
        End Get
        Set(ByVal value As Integer)
            _recCat = value
        End Set
    End Property

    'ResourceType
    Private _resTyp As Integer
    Public Property ResourcTyp As Integer
        Get
            Return _resTyp
        End Get
        Set(ByVal value As Integer)
            _resTyp = value
        End Set
    End Property

    'DocumentURL
    Private _docURL As String
    Public Property DOCURL As String
        Get
            Return _docURL
        End Get
        Set(ByVal value As String)
            _docURL = value
        End Set
    End Property

    'strBibliography
    Private _Biblo As String
    Public Property Biblo As String
        Get
            Return _Biblo
        End Get
        Set(ByVal value As String)
            _Biblo = value
        End Set
    End Property

    'strBibliographyDeveloper
    Private _BibloDev As String
    Public Property BibloDev As String
        Get
            Return _BibloDev
        End Get
        Set(ByVal value As String)
            _BibloDev = value
        End Set
    End Property

    'IS FROM GRID OR TreeView
    Private _ISGrid As Boolean = True
    Public Property ISGRID As Boolean
        Get
            Return _ISGrid
        End Get
        Set(ByVal value As Boolean)
            _ISGrid = value
        End Set
    End Property

    'Education ID
    Private _EduID As Long = 0
    Public Property EduID As Long
        Get
            Return _EduID
        End Get
        Set(ByVal value As Long)
            _EduID = value
        End Set
    End Property

    'IS DoubleClick
    Public _ISDoubleClick As Boolean = False

    'IS FROM GRID OR TreeView
    Private _FromExam As Boolean = False
    Public Property FromExam As Boolean
        Get
            Return _FromExam
        End Get
        Set(ByVal value As Boolean)
            _FromExam = value
        End Set
    End Property

#End Region

#Region " C1 Constants "
    Const COL_TEMPLATENAME = 1
    Const COL_ID = 2
    Const COL_TOTAL = 3
#End Region

#Region " Public Properties "
    Public WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            Imagepath = Value
        End Set
    End Property

    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
        End Get
    End Property

    Public Property Handle1() As Integer Implements mdlgloVoice.IExamChildEvents.Handle
        Get
            Return Me.Handle
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property
    'Protected Overrides ReadOnly Property CreateParams() As CreateParams
    '    Get
    '        Dim cp As CreateParams = MyBase.CreateParams
    '        cp.ExStyle = cp.ExStyle Or &H2000000
    '        Return cp
    '    End Get
    'End Property


#End Region

#Region " Windows Controls "
    Private WithEvents oWordApp As Wd.Application
    ' Private WithEvents oTempDoc As Wd.Document
    ' Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Private WithEvents _PatientStrip As gloUC_PatientStrip
    Friend WithEvents pnlWord As System.Windows.Forms.Panel
    Friend WithEvents wdPatientEducation As AxDSOFramer.AxFramerControl
    Friend WithEvents wdWordOptimizerDso As AxDSOFramer.AxFramerControl
    Friend WithEvents imgPatientEducation As System.Windows.Forms.ImageList
    Friend WithEvents pnlTreeviewSearch As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnlTreeviewList As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents PnlToolStrip As System.Windows.Forms.Panel
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlBibliographic As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnBibClose As System.Windows.Forms.Button
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtBibliographyDeveloper As System.Windows.Forms.RichTextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtBibliography As System.Windows.Forms.RichTextBox
    Friend WithEvents pnlEduGrid As System.Windows.Forms.Panel
    Friend WithEvents PnlGrid As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents pnlInfoBrowser As System.Windows.Forms.Panel
    Friend WithEvents InfoButtonWebBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents pnlTreeviewHdr As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents PnlGridSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRightMain As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtGridSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents PnlGridHdr As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents pnlPreview As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents grdPatienEducation As System.Windows.Forms.DataGridView
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnView As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents miniToolStrip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents homeButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents backButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents forwardButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents refreshButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_Saveandclose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_gloCommunityDashboard As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClosee As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents tlsInfobutton As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbHome As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbRefresh As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents tlsPatientEducation As WordToolStrip.gloWordToolStrip
#End Region

#Region " Windows Form Designer generated code "

    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

#Region "Dhruv -> For Show the Document and Modifying it while opening from View -> PatientEducation - Modify"

    Public Sub New(ByVal isPnlHide As Boolean, ByVal EducationID As Int64, ByVal TemplateName As String, ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal nExamID As Long, Optional ByVal nSrc As Long = 0, Optional ByVal ArrTemplateID As ArrayList = Nothing)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        pnlTreeViewMain.Visible = isPnlHide

        _isPnlHidden = isPnlHide
        nEducationID = EducationID
        sDocumentName = TemplateName
        m_VisitID = nVisitID
        m_patientID = nPatientID
        m_ExamID = nExamID
        m_arrTemplateID = ArrTemplateID
        Source = nSrc
        'Add any initialization after the InitializeComponent() call

    End Sub
#End Region

#Region "Added new Constructer for opening Education Form from Problem List and Medication"

    Public Sub New(ByVal isPnlHide As Boolean, ByVal EducationID As Int64, ByVal nPatientID As Long, ByVal fromInfobutton As Boolean, ByVal templateName As String, ByVal src As Integer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        pnlTreeViewMain.Visible = isPnlHide

        'PnlGridToolStrip.Visible = isPnlHide
        pnlEduGrid.Visible = isPnlHide
        _isPnlHidden = isPnlHide
        pnlPreview.Visible = isPnlHide
        nEducationID = EducationID
        sDocumentName = templateName
        m_patientID = nPatientID
        Source = src
        ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
        ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
        _isOpenforInfobutton = fromInfobutton
        _FromOutside = True

        'Add any initialization after the InitializeComponent() call
    End Sub

#End Region

    Public Sub New(ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal nExamID As Long, Optional ByVal ArrTemplateID As ArrayList = Nothing)
        MyBase.New()
        m_VisitID = nVisitID
        m_patientID = nPatientID
        m_ExamID = nExamID
        m_arrTemplateID = ArrTemplateID

        If m_ExamID > 0 Then
            _FromOutside = True
        End If



        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

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

            Dim dtpControls As ContextMenu() = {oContextMenu}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                gloGlobal.cEventHelper.DisposeContextMenu(dtpControls)
            Catch ex As Exception

            End Try

            Try
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Dispose()
                    _PatientStrip = Nothing
                End If
            Catch ex As Exception

            End Try
            If Not IsNothing(ogloVoice) Then
                ogloVoice.Dispose()
                ogloVoice = Nothing
            End If

            Dispose_Object()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlTreeViewMain As System.Windows.Forms.Panel
    ' Friend WithEvents WdPatientEdu As AxDSOFramer.AxFramerControl
    Friend WithEvents trvTemplate As System.Windows.Forms.TreeView
    Friend WithEvents trvEducation As System.Windows.Forms.TreeView
    Friend WithEvents txtTemplateSearch As System.Windows.Forms.TextBox

    Private pLoadWordApplication As gloWord.LoadAndCloseWord = Nothing

    Private Function GetMyLoadWordApplication() As gloWord.LoadAndCloseWord
        If (IsNothing(pLoadWordApplication)) Then
            pLoadWordApplication = New gloWord.LoadAndCloseWord()
            pLoadWordApplication.LoadApplicationOnly()
        Else
            If (pLoadWordApplication.CheckWordApplicationLocked()) Then
                pLoadWordApplication = New gloWord.LoadAndCloseWord()
                pLoadWordApplication.LoadApplicationOnly()
            End If
        End If
        Return pLoadWordApplication
    End Function

    Private Sub CloseMyLoadWordApplication()
        If (IsNothing(pLoadWordApplication) = False) Then
            pLoadWordApplication.CloseApplicationOnly()
            pLoadWordApplication = Nothing
        End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientEducation))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlTreeViewMain = New System.Windows.Forms.Panel()
        Me.pnlTreeviewList = New System.Windows.Forms.Panel()
        Me.trvTemplate = New System.Windows.Forms.TreeView()
        Me.imgPatientEducation = New System.Windows.Forms.ImageList(Me.components)
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pnlTreeviewSearch = New System.Windows.Forms.Panel()
        Me.txtTemplateSearch = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlTreeviewHdr = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.pnlWord = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.wdPatientEducation = New AxDSOFramer.AxFramerControl()
        Me.wdWordOptimizerDso = New AxDSOFramer.AxFramerControl()
        Me.pnlBibliographic = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txtBibliographyDeveloper = New System.Windows.Forms.RichTextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtBibliography = New System.Windows.Forms.RichTextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.btnBibClose = New System.Windows.Forms.Button()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.PnlToolStrip = New System.Windows.Forms.Panel()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnView = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClosee = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlInfoBrowser = New System.Windows.Forms.Panel()
        Me.InfoButtonWebBrowser = New System.Windows.Forms.WebBrowser()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tlsInfobutton = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbHome = New System.Windows.Forms.ToolStripButton()
        Me.tlbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tlbNext = New System.Windows.Forms.ToolStripButton()
        Me.tlbRefresh = New System.Windows.Forms.ToolStripButton()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.pnlPreview = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlEduGrid = New System.Windows.Forms.Panel()
        Me.PnlGrid = New System.Windows.Forms.Panel()
        Me.grdPatienEducation = New System.Windows.Forms.DataGridView()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.PnlGridSearch = New System.Windows.Forms.Panel()
        Me.pnlTopRightMain = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.txtGridSearch = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.PnlGridHdr = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.miniToolStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.homeButton = New System.Windows.Forms.ToolStripButton()
        Me.backButton = New System.Windows.Forms.ToolStripButton()
        Me.forwardButton = New System.Windows.Forms.ToolStripButton()
        Me.refreshButton = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ts_Saveandclose = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.tls_gloCommunityDashboard = New gloGlobal.gloToolStripIgnoreFocus()
        Me.pnlTreeViewMain.SuspendLayout()
        Me.pnlTreeviewList.SuspendLayout()
        Me.pnlTreeviewSearch.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTreeviewHdr.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnlWord.SuspendLayout()
        CType(Me.wdPatientEducation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wdWordOptimizerDso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBibliographic.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlInfoBrowser.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.tlsInfobutton.SuspendLayout()
        Me.pnlPreview.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.pnlEduGrid.SuspendLayout()
        Me.PnlGrid.SuspendLayout()
        CType(Me.grdPatienEducation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlGridSearch.SuspendLayout()
        Me.pnlTopRightMain.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.PnlGridHdr.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.tls_gloCommunityDashboard.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTreeViewMain
        '
        Me.pnlTreeViewMain.Controls.Add(Me.pnlTreeviewList)
        Me.pnlTreeViewMain.Controls.Add(Me.pnlTreeviewSearch)
        Me.pnlTreeViewMain.Controls.Add(Me.pnlTreeviewHdr)
        Me.pnlTreeViewMain.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlTreeViewMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlTreeViewMain.Name = "pnlTreeViewMain"
        Me.pnlTreeViewMain.Size = New System.Drawing.Size(230, 701)
        Me.pnlTreeViewMain.TabIndex = 2
        '
        'pnlTreeviewList
        '
        Me.pnlTreeviewList.Controls.Add(Me.trvTemplate)
        Me.pnlTreeviewList.Controls.Add(Me.Label14)
        Me.pnlTreeviewList.Controls.Add(Me.Label17)
        Me.pnlTreeviewList.Controls.Add(Me.Label18)
        Me.pnlTreeviewList.Controls.Add(Me.Label19)
        Me.pnlTreeviewList.Controls.Add(Me.Label22)
        Me.pnlTreeviewList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTreeviewList.Location = New System.Drawing.Point(0, 58)
        Me.pnlTreeviewList.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlTreeviewList.Name = "pnlTreeviewList"
        Me.pnlTreeviewList.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlTreeviewList.Size = New System.Drawing.Size(230, 643)
        Me.pnlTreeviewList.TabIndex = 21
        '
        'trvTemplate
        '
        Me.trvTemplate.BackColor = System.Drawing.Color.White
        Me.trvTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvTemplate.ForeColor = System.Drawing.Color.Black
        Me.trvTemplate.HideSelection = False
        Me.trvTemplate.ImageIndex = 0
        Me.trvTemplate.ImageList = Me.imgPatientEducation
        Me.trvTemplate.ItemHeight = 21
        Me.trvTemplate.Location = New System.Drawing.Point(4, 5)
        Me.trvTemplate.Name = "trvTemplate"
        Me.trvTemplate.SelectedImageIndex = 0
        Me.trvTemplate.ShowLines = False
        Me.trvTemplate.Size = New System.Drawing.Size(225, 634)
        Me.trvTemplate.TabIndex = 2
        '
        'imgPatientEducation
        '
        Me.imgPatientEducation.ImageStream = CType(resources.GetObject("imgPatientEducation.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgPatientEducation.TransparentColor = System.Drawing.Color.Transparent
        Me.imgPatientEducation.Images.SetKeyName(0, "Pat Education.ico")
        Me.imgPatientEducation.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgPatientEducation.Images.SetKeyName(2, "Small Arrow.ico")
        Me.imgPatientEducation.Images.SetKeyName(3, "Patient Education.ico")
        Me.imgPatientEducation.Images.SetKeyName(4, "Arrow_02.ico")
        Me.imgPatientEducation.Images.SetKeyName(5, "arrow_01.ico")
        Me.imgPatientEducation.Images.SetKeyName(6, "Internal Library.ico")
        Me.imgPatientEducation.Images.SetKeyName(7, "Suggested Education material.ico")
        Me.imgPatientEducation.Images.SetKeyName(8, "Medication List.ico")
        Me.imgPatientEducation.Images.SetKeyName(9, "Problem List.ico")
        Me.imgPatientEducation.Images.SetKeyName(10, "LabResults.ico")
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.White
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Location = New System.Drawing.Point(4, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(225, 4)
        Me.Label14.TabIndex = 38
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(4, 639)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(225, 1)
        Me.Label17.TabIndex = 43
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 639)
        Me.Label18.TabIndex = 42
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(229, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 639)
        Me.Label19.TabIndex = 41
        Me.Label19.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(227, 1)
        Me.Label22.TabIndex = 40
        Me.Label22.Text = "label1"
        '
        'pnlTreeviewSearch
        '
        Me.pnlTreeviewSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTreeviewSearch.Controls.Add(Me.txtTemplateSearch)
        Me.pnlTreeviewSearch.Controls.Add(Me.Label20)
        Me.pnlTreeviewSearch.Controls.Add(Me.Label21)
        Me.pnlTreeviewSearch.Controls.Add(Me.PictureBox1)
        Me.pnlTreeviewSearch.Controls.Add(Me.label9)
        Me.pnlTreeviewSearch.Controls.Add(Me.Label12)
        Me.pnlTreeviewSearch.Controls.Add(Me.Label11)
        Me.pnlTreeviewSearch.Controls.Add(Me.Label13)
        Me.pnlTreeviewSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTreeviewSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTreeviewSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlTreeviewSearch.Location = New System.Drawing.Point(0, 30)
        Me.pnlTreeviewSearch.Name = "pnlTreeviewSearch"
        Me.pnlTreeviewSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlTreeviewSearch.Size = New System.Drawing.Size(230, 28)
        Me.pnlTreeviewSearch.TabIndex = 16
        '
        'txtTemplateSearch
        '
        Me.txtTemplateSearch.BackColor = System.Drawing.Color.White
        Me.txtTemplateSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTemplateSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTemplateSearch.Location = New System.Drawing.Point(32, 5)
        Me.txtTemplateSearch.Name = "txtTemplateSearch"
        Me.txtTemplateSearch.Size = New System.Drawing.Size(197, 15)
        Me.txtTemplateSearch.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(32, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(197, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(32, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(197, 4)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 23)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Location = New System.Drawing.Point(4, 24)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(225, 1)
        Me.label9.TabIndex = 35
        Me.label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(225, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Location = New System.Drawing.Point(229, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 25)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 25)
        Me.Label13.TabIndex = 40
        Me.Label13.Text = "label3"
        '
        'pnlTreeviewHdr
        '
        Me.pnlTreeviewHdr.Controls.Add(Me.Panel9)
        Me.pnlTreeviewHdr.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTreeviewHdr.Location = New System.Drawing.Point(0, 0)
        Me.pnlTreeviewHdr.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlTreeviewHdr.Name = "pnlTreeviewHdr"
        Me.pnlTreeviewHdr.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlTreeviewHdr.Size = New System.Drawing.Size(230, 30)
        Me.pnlTreeviewHdr.TabIndex = 25
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Transparent
        Me.Panel9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.Label26)
        Me.Panel9.Controls.Add(Me.Label31)
        Me.Panel9.Controls.Add(Me.Label32)
        Me.Panel9.Controls.Add(Me.Label38)
        Me.Panel9.Controls.Add(Me.Label39)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(3, 3)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(3, 0, 2, 3)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(227, 24)
        Me.Panel9.TabIndex = 4
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(1, 23)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(225, 1)
        Me.Label26.TabIndex = 12
        Me.Label26.Text = "label2"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(0, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 23)
        Me.Label31.TabIndex = 11
        Me.Label31.Text = "label4"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label32.Location = New System.Drawing.Point(226, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1, 23)
        Me.Label32.TabIndex = 10
        Me.Label32.Text = "label3"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(0, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(227, 1)
        Me.Label38.TabIndex = 9
        Me.Label38.Text = "label1"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.White
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(227, 24)
        Me.Label39.TabIndex = 0
        Me.Label39.Text = "  Select New Materials"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlWord
        '
        Me.pnlWord.BackColor = System.Drawing.Color.Transparent
        Me.pnlWord.Controls.Add(Me.Label3)
        Me.pnlWord.Controls.Add(Me.Label2)
        Me.pnlWord.Controls.Add(Me.Label4)
        Me.pnlWord.Controls.Add(Me.Label5)
        Me.pnlWord.Controls.Add(Me.wdPatientEducation)
        Me.pnlWord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWord.ForeColor = System.Drawing.Color.Black
        Me.pnlWord.Location = New System.Drawing.Point(233, 217)
        Me.pnlWord.Name = "pnlWord"
        Me.pnlWord.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlWord.Size = New System.Drawing.Size(785, 484)
        Me.pnlWord.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 479)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(0, 480)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(781, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(781, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 480)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(782, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'wdPatientEducation
        '
        Me.wdPatientEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPatientEducation.Enabled = True
        Me.wdPatientEducation.Location = New System.Drawing.Point(0, 0)
        Me.wdPatientEducation.Name = "wdPatientEducation"
        Me.wdPatientEducation.OcxState = CType(resources.GetObject("wdPatientEducation.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPatientEducation.Size = New System.Drawing.Size(782, 481)
        Me.wdPatientEducation.TabIndex = 3
        '
        'wdWordOptimizerDso
        '
        Me.wdWordOptimizerDso.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdWordOptimizerDso.Enabled = True
        Me.wdWordOptimizerDso.Location = New System.Drawing.Point(0, 0)
        Me.wdWordOptimizerDso.Name = "wdWordOptimizerDso"
        Me.wdWordOptimizerDso.Size = New System.Drawing.Size(782, 481)
        Me.wdWordOptimizerDso.TabIndex = 0
        '
        'pnlBibliographic
        '
        Me.pnlBibliographic.BackColor = System.Drawing.Color.Transparent
        Me.pnlBibliographic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBibliographic.Controls.Add(Me.Panel5)
        Me.pnlBibliographic.Controls.Add(Me.Panel7)
        Me.pnlBibliographic.Controls.Add(Me.Label35)
        Me.pnlBibliographic.Controls.Add(Me.Label36)
        Me.pnlBibliographic.Controls.Add(Me.Label37)
        Me.pnlBibliographic.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlBibliographic.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlBibliographic.Location = New System.Drawing.Point(174, 123)
        Me.pnlBibliographic.Name = "pnlBibliographic"
        Me.pnlBibliographic.Size = New System.Drawing.Size(449, 190)
        Me.pnlBibliographic.TabIndex = 41
        Me.pnlBibliographic.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Azure
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.txtBibliographyDeveloper)
        Me.Panel5.Controls.Add(Me.Label23)
        Me.Panel5.Controls.Add(Me.txtBibliography)
        Me.Panel5.Controls.Add(Me.Label24)
        Me.Panel5.Controls.Add(Me.Label25)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(1, 28)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(447, 162)
        Me.Panel5.TabIndex = 26
        '
        'txtBibliographyDeveloper
        '
        Me.txtBibliographyDeveloper.BackColor = System.Drawing.Color.Azure
        Me.txtBibliographyDeveloper.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBibliographyDeveloper.Location = New System.Drawing.Point(155, 236)
        Me.txtBibliographyDeveloper.Name = "txtBibliographyDeveloper"
        Me.txtBibliographyDeveloper.ReadOnly = True
        Me.txtBibliographyDeveloper.Size = New System.Drawing.Size(467, 143)
        Me.txtBibliographyDeveloper.TabIndex = 31
        Me.txtBibliographyDeveloper.Text = ""
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(7, 239)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(146, 13)
        Me.Label23.TabIndex = 30
        Me.Label23.Text = "Intervention Developer :"
        '
        'txtBibliography
        '
        Me.txtBibliography.BackColor = System.Drawing.Color.Azure
        Me.txtBibliography.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBibliography.Location = New System.Drawing.Point(155, 22)
        Me.txtBibliography.Name = "txtBibliography"
        Me.txtBibliography.ReadOnly = True
        Me.txtBibliography.Size = New System.Drawing.Size(467, 201)
        Me.txtBibliography.TabIndex = 29
        Me.txtBibliography.Text = ""
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(0, 161)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(447, 1)
        Me.Label24.TabIndex = 28
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Location = New System.Drawing.Point(23, 25)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(130, 13)
        Me.Label25.TabIndex = 9
        Me.Label25.Text = "Bibliography Citation :"
        '
        'Panel7
        '
        Me.Panel7.BackgroundImage = Global.gloEMR.My.Resources.Resources.bluebtn1
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label33)
        Me.Panel7.Controls.Add(Me.Label34)
        Me.Panel7.Controls.Add(Me.PictureBox2)
        Me.Panel7.Controls.Add(Me.btnBibClose)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(1, 1)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(447, 27)
        Me.Panel7.TabIndex = 13
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Location = New System.Drawing.Point(251, 26)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(162, 1)
        Me.Label33.TabIndex = 29
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.White
        Me.Label34.Location = New System.Drawing.Point(26, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(225, 27)
        Me.Label34.TabIndex = 20
        Me.Label34.Text = "Reference Information"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(26, 27)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 19
        Me.PictureBox2.TabStop = False
        '
        'btnBibClose
        '
        Me.btnBibClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBibClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnBibClose.FlatAppearance.BorderSize = 0
        Me.btnBibClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBibClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBibClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBibClose.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBibClose.Image = CType(resources.GetObject("btnBibClose.Image"), System.Drawing.Image)
        Me.btnBibClose.Location = New System.Drawing.Point(413, 0)
        Me.btnBibClose.Name = "btnBibClose"
        Me.btnBibClose.Size = New System.Drawing.Size(34, 27)
        Me.btnBibClose.TabIndex = 0
        Me.btnBibClose.UseVisualStyleBackColor = True
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Location = New System.Drawing.Point(448, 1)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 189)
        Me.Label35.TabIndex = 30
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Location = New System.Drawing.Point(0, 1)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1, 189)
        Me.Label36.TabIndex = 29
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Location = New System.Drawing.Point(0, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(449, 1)
        Me.Label37.TabIndex = 27
        '
        'PnlToolStrip
        '
        Me.PnlToolStrip.Controls.Add(Me.Label40)
        Me.PnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.PnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.PnlToolStrip.Name = "PnlToolStrip"
        Me.PnlToolStrip.Size = New System.Drawing.Size(1018, 56)
        Me.PnlToolStrip.TabIndex = 40
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(0, 55)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1018, 1)
        Me.Label40.TabIndex = 10
        Me.Label40.Text = "label1"
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnView, Me.ts_btnRefresh, Me.ts_btnDelete, Me.ts_btnClosee})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1018, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnView
        '
        Me.ts_btnView.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnView.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnView.Image = CType(resources.GetObject("ts_btnView.Image"), System.Drawing.Image)
        Me.ts_btnView.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnView.Name = "ts_btnView"
        Me.ts_btnView.Size = New System.Drawing.Size(49, 50)
        Me.ts_btnView.Tag = "Modify"
        Me.ts_btnView.Text = "&Modify"
        Me.ts_btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(55, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(48, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClosee
        '
        Me.ts_btnClosee.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClosee.Image = CType(resources.GetObject("ts_btnClosee.Image"), System.Drawing.Image)
        Me.ts_btnClosee.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClosee.Name = "ts_btnClosee"
        Me.ts_btnClosee.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClosee.Tag = "Close"
        Me.ts_btnClosee.Text = "&Close"
        Me.ts_btnClosee.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlInfoBrowser)
        Me.pnlMain.Controls.Add(Me.pnlWord)
        Me.pnlMain.Controls.Add(Me.pnlPreview)
        Me.pnlMain.Controls.Add(Me.Splitter2)
        Me.pnlMain.Controls.Add(Me.pnlEduGrid)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlTreeViewMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1018, 701)
        Me.pnlMain.TabIndex = 9
        '
        'pnlInfoBrowser
        '
        Me.pnlInfoBrowser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlInfoBrowser.Controls.Add(Me.pnlBibliographic)
        Me.pnlInfoBrowser.Controls.Add(Me.InfoButtonWebBrowser)
        Me.pnlInfoBrowser.Controls.Add(Me.Panel2)
        Me.pnlInfoBrowser.Controls.Add(Me.Label58)
        Me.pnlInfoBrowser.Controls.Add(Me.Label57)
        Me.pnlInfoBrowser.Controls.Add(Me.Label56)
        Me.pnlInfoBrowser.Controls.Add(Me.Label55)
        Me.pnlInfoBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInfoBrowser.Location = New System.Drawing.Point(233, 217)
        Me.pnlInfoBrowser.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlInfoBrowser.Name = "pnlInfoBrowser"
        Me.pnlInfoBrowser.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlInfoBrowser.Size = New System.Drawing.Size(785, 484)
        Me.pnlInfoBrowser.TabIndex = 6
        Me.pnlInfoBrowser.Visible = False
        '
        'InfoButtonWebBrowser
        '
        Me.InfoButtonWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InfoButtonWebBrowser.Location = New System.Drawing.Point(1, 45)
        Me.InfoButtonWebBrowser.Margin = New System.Windows.Forms.Padding(2)
        Me.InfoButtonWebBrowser.MinimumSize = New System.Drawing.Size(15, 16)
        Me.InfoButtonWebBrowser.Name = "InfoButtonWebBrowser"
        Me.InfoButtonWebBrowser.ScriptErrorsSuppressed = True
        Me.InfoButtonWebBrowser.Size = New System.Drawing.Size(780, 435)
        Me.InfoButtonWebBrowser.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.tlsInfobutton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(1, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(780, 44)
        Me.Panel2.TabIndex = 47
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(0, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(780, 1)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "label2"
        '
        'tlsInfobutton
        '
        Me.tlsInfobutton.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsInfobutton.BackgroundImage = CType(resources.GetObject("tlsInfobutton.BackgroundImage"), System.Drawing.Image)
        Me.tlsInfobutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsInfobutton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlsInfobutton.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsInfobutton.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsInfobutton.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.tlsInfobutton.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbHome, Me.tlbPrevious, Me.tlbNext, Me.tlbRefresh})
        Me.tlsInfobutton.Location = New System.Drawing.Point(0, 0)
        Me.tlsInfobutton.Name = "tlsInfobutton"
        Me.tlsInfobutton.Size = New System.Drawing.Size(780, 44)
        Me.tlsInfobutton.TabIndex = 1
        Me.tlsInfobutton.Text = "ToolStrip1"
        '
        'tlbHome
        '
        Me.tlbHome.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbHome.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbHome.Image = CType(resources.GetObject("tlbHome.Image"), System.Drawing.Image)
        Me.tlbHome.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbHome.Name = "tlbHome"
        Me.tlbHome.Size = New System.Drawing.Size(44, 41)
        Me.tlbHome.Tag = "Home"
        Me.tlbHome.Text = "&Home"
        Me.tlbHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbPrevious
        '
        Me.tlbPrevious.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbPrevious.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbPrevious.Image = CType(resources.GetObject("tlbPrevious.Image"), System.Drawing.Image)
        Me.tlbPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbPrevious.Name = "tlbPrevious"
        Me.tlbPrevious.Size = New System.Drawing.Size(60, 41)
        Me.tlbPrevious.Tag = "Previous"
        Me.tlbPrevious.Text = "&Previous"
        Me.tlbPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbNext
        '
        Me.tlbNext.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbNext.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbNext.Image = CType(resources.GetObject("tlbNext.Image"), System.Drawing.Image)
        Me.tlbNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbNext.Name = "tlbNext"
        Me.tlbNext.Size = New System.Drawing.Size(37, 41)
        Me.tlbNext.Tag = "Next"
        Me.tlbNext.Text = "&Next"
        Me.tlbNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbRefresh
        '
        Me.tlbRefresh.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbRefresh.Image = CType(resources.GetObject("tlbRefresh.Image"), System.Drawing.Image)
        Me.tlbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbRefresh.Name = "tlbRefresh"
        Me.tlbRefresh.Size = New System.Drawing.Size(55, 41)
        Me.tlbRefresh.Tag = "Refresh"
        Me.tlbRefresh.Text = "&Refresh"
        Me.tlbRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label58.Location = New System.Drawing.Point(1, 480)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(780, 1)
        Me.Label58.TabIndex = 46
        Me.Label58.Text = "label1"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label57.Location = New System.Drawing.Point(1, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(780, 1)
        Me.Label57.TabIndex = 45
        Me.Label57.Text = "label1"
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(781, 0)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(1, 481)
        Me.Label56.TabIndex = 44
        Me.Label56.Text = "label4"
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(0, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(1, 481)
        Me.Label55.TabIndex = 43
        Me.Label55.Text = "label4"
        '
        'pnlPreview
        '
        Me.pnlPreview.Controls.Add(Me.Panel10)
        Me.pnlPreview.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPreview.Location = New System.Drawing.Point(233, 189)
        Me.pnlPreview.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPreview.Name = "pnlPreview"
        Me.pnlPreview.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlPreview.Size = New System.Drawing.Size(785, 28)
        Me.pnlPreview.TabIndex = 43
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Transparent
        Me.Panel10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label45)
        Me.Panel10.Controls.Add(Me.Label46)
        Me.Panel10.Controls.Add(Me.Label47)
        Me.Panel10.Controls.Add(Me.Label48)
        Me.Panel10.Controls.Add(Me.Label49)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(3, 0, 2, 3)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(785, 25)
        Me.Panel10.TabIndex = 4
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(1, 24)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(783, 1)
        Me.Label45.TabIndex = 12
        Me.Label45.Text = "label2"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(0, 1)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 24)
        Me.Label46.TabIndex = 11
        Me.Label46.Text = "label4"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label47.Location = New System.Drawing.Point(784, 1)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 24)
        Me.Label47.TabIndex = 10
        Me.Label47.Text = "label3"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(0, 0)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(785, 1)
        Me.Label48.TabIndex = 9
        Me.Label48.Text = "label1"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.Color.White
        Me.Label49.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label49.Location = New System.Drawing.Point(0, 0)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(785, 25)
        Me.Label49.TabIndex = 0
        Me.Label49.Text = "  Preview"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(233, 186)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(785, 3)
        Me.Splitter2.TabIndex = 44
        Me.Splitter2.TabStop = False
        '
        'pnlEduGrid
        '
        Me.pnlEduGrid.BackColor = System.Drawing.Color.Transparent
        Me.pnlEduGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlEduGrid.Controls.Add(Me.PnlGrid)
        Me.pnlEduGrid.Controls.Add(Me.PnlGridSearch)
        Me.pnlEduGrid.Controls.Add(Me.PnlGridHdr)
        Me.pnlEduGrid.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEduGrid.Location = New System.Drawing.Point(233, 0)
        Me.pnlEduGrid.Name = "pnlEduGrid"
        Me.pnlEduGrid.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.pnlEduGrid.Size = New System.Drawing.Size(785, 186)
        Me.pnlEduGrid.TabIndex = 5
        '
        'PnlGrid
        '
        Me.PnlGrid.Controls.Add(Me.grdPatienEducation)
        Me.PnlGrid.Controls.Add(Me.lbl_BottomBrd)
        Me.PnlGrid.Controls.Add(Me.lbl_LeftBrd)
        Me.PnlGrid.Controls.Add(Me.lbl_RightBrd)
        Me.PnlGrid.Controls.Add(Me.lbl_TopBrd)
        Me.PnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlGrid.Location = New System.Drawing.Point(0, 57)
        Me.PnlGrid.Name = "PnlGrid"
        Me.PnlGrid.Size = New System.Drawing.Size(782, 129)
        Me.PnlGrid.TabIndex = 13
        '
        'grdPatienEducation
        '
        Me.grdPatienEducation.AllowUserToAddRows = False
        Me.grdPatienEducation.AllowUserToResizeColumns = False
        Me.grdPatienEducation.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.grdPatienEducation.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdPatienEducation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.grdPatienEducation.BackgroundColor = System.Drawing.Color.White
        Me.grdPatienEducation.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(217, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPatienEducation.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdPatienEducation.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdPatienEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPatienEducation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdPatienEducation.EnableHeadersVisualStyles = False
        Me.grdPatienEducation.GridColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdPatienEducation.Location = New System.Drawing.Point(1, 1)
        Me.grdPatienEducation.MultiSelect = False
        Me.grdPatienEducation.Name = "grdPatienEducation"
        Me.grdPatienEducation.RowHeadersVisible = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.grdPatienEducation.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.grdPatienEducation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPatienEducation.Size = New System.Drawing.Size(780, 127)
        Me.grdPatienEducation.TabIndex = 10
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 128)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(780, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 128)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(781, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 128)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(782, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'PnlGridSearch
        '
        Me.PnlGridSearch.Controls.Add(Me.pnlTopRightMain)
        Me.PnlGridSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlGridSearch.Location = New System.Drawing.Point(0, 27)
        Me.PnlGridSearch.Name = "PnlGridSearch"
        Me.PnlGridSearch.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.PnlGridSearch.Size = New System.Drawing.Size(782, 30)
        Me.PnlGridSearch.TabIndex = 25
        '
        'pnlTopRightMain
        '
        Me.pnlTopRightMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRightMain.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRightMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRightMain.Controls.Add(Me.panel4)
        Me.pnlTopRightMain.Controls.Add(Me.lblSearch)
        Me.pnlTopRightMain.Controls.Add(Me.Label41)
        Me.pnlTopRightMain.Controls.Add(Me.Label42)
        Me.pnlTopRightMain.Controls.Add(Me.Label43)
        Me.pnlTopRightMain.Controls.Add(Me.Label44)
        Me.pnlTopRightMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRightMain.Location = New System.Drawing.Point(0, 3)
        Me.pnlTopRightMain.Name = "pnlTopRightMain"
        Me.pnlTopRightMain.Size = New System.Drawing.Size(782, 24)
        Me.pnlTopRightMain.TabIndex = 1
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.White
        Me.panel4.Controls.Add(Me.txtGridSearch)
        Me.panel4.Controls.Add(Me.Label7)
        Me.panel4.Controls.Add(Me.Label6)
        Me.panel4.Controls.Add(Me.btnClear)
        Me.panel4.Controls.Add(Me.Label8)
        Me.panel4.Controls.Add(Me.Label10)
        Me.panel4.Controls.Add(Me.Label16)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel4.ForeColor = System.Drawing.Color.Black
        Me.panel4.Location = New System.Drawing.Point(65, 1)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(241, 22)
        Me.panel4.TabIndex = 50
        '
        'txtGridSearch
        '
        Me.txtGridSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGridSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGridSearch.ForeColor = System.Drawing.Color.Black
        Me.txtGridSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtGridSearch.Name = "txtGridSearch"
        Me.txtGridSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtGridSearch.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(5, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(214, 3)
        Me.Label7.TabIndex = 37
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(5, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(214, 5)
        Me.Label6.TabIndex = 43
        '
        'btnClear
        '
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(219, 0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 48
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Location = New System.Drawing.Point(1, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(4, 22)
        Me.Label8.TabIndex = 38
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 22)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Location = New System.Drawing.Point(240, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 22)
        Me.Label16.TabIndex = 40
        Me.Label16.Text = "label4"
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 22)
        Me.lblSearch.TabIndex = 1
        Me.lblSearch.Text = "Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(1, 23)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(780, 1)
        Me.Label41.TabIndex = 8
        Me.Label41.Text = "label2"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(0, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 23)
        Me.Label42.TabIndex = 7
        Me.Label42.Text = "label4"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(781, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 23)
        Me.Label43.TabIndex = 6
        Me.Label43.Text = "label3"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(782, 1)
        Me.Label44.TabIndex = 5
        Me.Label44.Text = "label1"
        '
        'PnlGridHdr
        '
        Me.PnlGridHdr.Controls.Add(Me.Panel11)
        Me.PnlGridHdr.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlGridHdr.Location = New System.Drawing.Point(0, 0)
        Me.PnlGridHdr.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.PnlGridHdr.Name = "PnlGridHdr"
        Me.PnlGridHdr.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.PnlGridHdr.Size = New System.Drawing.Size(782, 27)
        Me.PnlGridHdr.TabIndex = 27
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.Transparent
        Me.Panel11.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel11.Controls.Add(Me.Label15)
        Me.Panel11.Controls.Add(Me.Label27)
        Me.Panel11.Controls.Add(Me.Label28)
        Me.Panel11.Controls.Add(Me.Label29)
        Me.Panel11.Controls.Add(Me.Label30)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(0, 3)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(3, 0, 2, 3)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(782, 24)
        Me.Panel11.TabIndex = 4
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1, 23)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(780, 1)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "label2"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(0, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 23)
        Me.Label27.TabIndex = 11
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(781, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 23)
        Me.Label28.TabIndex = 10
        Me.Label28.Text = "label3"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(782, 1)
        Me.Label29.TabIndex = 9
        Me.Label29.Text = "label1"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.White
        Me.Label30.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(782, 24)
        Me.Label30.TabIndex = 0
        Me.Label30.Text = "  History of Issued Education"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(230, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 701)
        Me.Splitter1.TabIndex = 42
        Me.Splitter1.TabStop = False
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.miniToolStrip.BackgroundImage = CType(resources.GetObject("miniToolStrip.BackgroundImage"), System.Drawing.Image)
        Me.miniToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.miniToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(1018, 58)
        Me.miniToolStrip.TabIndex = 1
        '
        'homeButton
        '
        Me.homeButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.homeButton.Image = CType(resources.GetObject("homeButton.Image"), System.Drawing.Image)
        Me.homeButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.homeButton.Name = "homeButton"
        Me.homeButton.Size = New System.Drawing.Size(46, 55)
        Me.homeButton.Tag = "Home"
        Me.homeButton.Text = "&Home"
        Me.homeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'backButton
        '
        Me.backButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.backButton.Image = CType(resources.GetObject("backButton.Image"), System.Drawing.Image)
        Me.backButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.backButton.Name = "backButton"
        Me.backButton.Size = New System.Drawing.Size(63, 55)
        Me.backButton.Tag = "Previous"
        Me.backButton.Text = "&Previous"
        Me.backButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'forwardButton
        '
        Me.forwardButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.forwardButton.Image = CType(resources.GetObject("forwardButton.Image"), System.Drawing.Image)
        Me.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.forwardButton.Name = "forwardButton"
        Me.forwardButton.Size = New System.Drawing.Size(39, 55)
        Me.forwardButton.Tag = "Next"
        Me.forwardButton.Text = "&Next"
        Me.forwardButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'refreshButton
        '
        Me.refreshButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.refreshButton.Image = CType(resources.GetObject("refreshButton.Image"), System.Drawing.Image)
        Me.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.refreshButton.Name = "refreshButton"
        Me.refreshButton.Size = New System.Drawing.Size(58, 55)
        Me.refreshButton.Tag = "Refresh"
        Me.refreshButton.Text = "&Refresh"
        Me.refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnPrint
        '
        Me.ts_btnPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnPrint.Image = CType(resources.GetObject("ts_btnPrint.Image"), System.Drawing.Image)
        Me.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPrint.Name = "ts_btnPrint"
        Me.ts_btnPrint.Size = New System.Drawing.Size(41, 55)
        Me.ts_btnPrint.Tag = "Print"
        Me.ts_btnPrint.Text = "&Print"
        Me.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_Saveandclose
        '
        Me.ts_Saveandclose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_Saveandclose.Image = CType(resources.GetObject("ts_Saveandclose.Image"), System.Drawing.Image)
        Me.ts_Saveandclose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_Saveandclose.Name = "ts_Saveandclose"
        Me.ts_Saveandclose.Size = New System.Drawing.Size(66, 55)
        Me.ts_Saveandclose.Tag = "Save&Close"
        Me.ts_Saveandclose.Text = "&Save&&Cls"
        Me.ts_Saveandclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_Saveandclose.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 55)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_gloCommunityDashboard
        '
        Me.tls_gloCommunityDashboard.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tls_gloCommunityDashboard.BackgroundImage = CType(resources.GetObject("tls_gloCommunityDashboard.BackgroundImage"), System.Drawing.Image)
        Me.tls_gloCommunityDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_gloCommunityDashboard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tls_gloCommunityDashboard.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_gloCommunityDashboard.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_gloCommunityDashboard.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_gloCommunityDashboard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.homeButton, Me.backButton, Me.forwardButton, Me.refreshButton, Me.ts_btnPrint, Me.ts_Saveandclose, Me.ts_btnClose})
        Me.tls_gloCommunityDashboard.Location = New System.Drawing.Point(0, 0)
        Me.tls_gloCommunityDashboard.Name = "tls_gloCommunityDashboard"
        Me.tls_gloCommunityDashboard.Size = New System.Drawing.Size(1018, 58)
        Me.tls_gloCommunityDashboard.TabIndex = 1
        Me.tls_gloCommunityDashboard.Text = "ToolStrip1"
        '
        'frmPatientEducation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1018, 757)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.PnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientEducation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Education"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTreeViewMain.ResumeLayout(False)
        Me.pnlTreeviewList.ResumeLayout(False)
        Me.pnlTreeviewSearch.ResumeLayout(False)
        Me.pnlTreeviewSearch.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTreeviewHdr.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.pnlWord.ResumeLayout(False)
        CType(Me.wdPatientEducation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wdWordOptimizerDso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBibliographic.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlToolStrip.ResumeLayout(False)
        Me.PnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlInfoBrowser.ResumeLayout(False)
        Me.pnlInfoBrowser.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.tlsInfobutton.ResumeLayout(False)
        Me.tlsInfobutton.PerformLayout()
        Me.pnlPreview.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.pnlEduGrid.ResumeLayout(False)
        Me.PnlGrid.ResumeLayout(False)
        CType(Me.grdPatienEducation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlGridSearch.ResumeLayout(False)
        Me.pnlTopRightMain.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.PnlGridHdr.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.tls_gloCommunityDashboard.ResumeLayout(False)
        Me.tls_gloCommunityDashboard.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Form Events "
    Private Sub frmPatientEducation_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        Try
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            Else
                If (IsNothing(Me.myCaller) = False) Then
                    CType(Me.myCaller.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Me.ParentForm IsNot Nothing Then
                CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
                CType(Me.ParentForm, MainMenu).ActiveDSO = wdPatientEducation
            End If
        End Try

        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        End If




        'Developer: Yatin N.Bhagat
        'Date:12/26/2011
        'Bug ID/PRD Name/Salesforce Case:Bug No. 17246:Patient Consent >> Checkbox in the template are not working once you finish exam
        'Reason: Handler For DDLCBEvent is Not Added while activating the form
        Try
            If Not (IsNothing(wdPatientEducation)) Then
                If Not (IsNothing(wdPatientEducation.DocumentName)) Then
                    If Not (IsNothing(wdPatientEducation.ActiveDocument)) Then
                        oCurDoc = wdPatientEducation.ActiveDocument
                        oWordApp = oCurDoc.Application
                        Try
                            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try

                        Try
                            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try

                        isHandlerRemoved = False
                        oCurDoc.ActiveWindow.SetFocus()
                        wdPatientEducation.Focus()
                    End If
                End If
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        'Shubhangi 20100120
        'To resolve the issue: When we right click the form gets disappear
        For Each myForm As Form In Application.OpenForms
            If (myForm.TopMost) Then
                myForm.TopMost = False
            End If
        Next
        Me.TopMost = True
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmPatientEducation_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If


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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try



        'Shubhangi 20100120
        'To resolve the issue: When we right click the form gets disappear
        Me.TopMost = False
    End Sub

    Private Sub frmPatientEducation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            'wdWordOptimizerDso.Close()
            'wdWordOptimizerDso.Dispose()
            'wdWordOptimizerDso = Nothing
            gloWord.LoadAndCloseWord.CloseAndDisposeDSO(wdWordOptimizerDso)
        Catch ex As Exception

        End Try
        Try
            CloseMyLoadWordApplication()
        Catch ex As Exception

        End Try

        Try
            If Not IsNothing(wdPatientEducation) Then ''added for bugid 86970
                wdPatientEducation.Close()
            End If
            _FromOutside = False

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(oContextMenu) = False Then
                gloGlobal.cEventHelper.RemoveAllEventHandlers(oContextMenu)
                If (IsNothing(oContextMenu.MenuItems) = False) Then
                    oContextMenu.MenuItems.Clear()
                End If
                oContextMenu.Dispose()
                oContextMenu = Nothing
            End If

            If IsNothing(oItem) = False Then
                oItem.Dispose()
                oItem = Nothing
            End If





            Formopen_Info = False
            If blnOpenFromExam = True Then
                Me.Cursor = Cursors.WaitCursor
                myCaller.UnprotectFrmPdEdu()
                myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.PatientEducation)


                'Dim frm As MainMenu
                'frm = MyMDIParent
                'frm.MenuStrip1.Enabled = True
                CType(myCaller.ParentForm, MainMenu).MenuStrip1.Enabled = True
                blnOpenFromExam = False

                Me.Cursor = Cursors.Default
            End If
            Me.Cursor = Cursors.Default
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                If Not IsNothing(ogloVoice) Then
                    If Me.IsMdiChild = True Then
                        TurnoffMicrophone()
                        ogloVoice.UnInitializeVoiceComponents()
                    ElseIf Me.IsMdiChild = False Then
                        TurnoffMicrophone()
                        ' MyMDIParent.MdiExamChildDeActivate(Me)
                    End If

                End If
            End If
            MyMDIParent.MdiExamChildDeActivate(Me)
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
            End If
            Dispose_Object()

            ''Dim objAudit As New clsAudit
            ''objAudit.CreateLog(gloAuditTrail.enmActivityType.Other, "Patient Education Closed", gstrLoginName, gstrClientMachineName, gnPatientID)
            ''objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "Patient Education Closed", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011

            If ResourceType = 2 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Close, "Provider Reference Document Closed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Close, "Provider Reference Document Closed", gloAuditTrail.ActivityOutCome.Success)

            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "Patient Education Closed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "Patient Education Closed", gloAuditTrail.ActivityOutCome.Success)
            End If


        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub frmPatientEducation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If IsNothing(oCurDoc) = False Then
        '    If oCurDoc.Saved = False Then
        '        'Word document
        '        If ResourceCategory = 1 Then
        '            Dim Result As DialogResult
        '            Result = MessageBox.Show("Do you want to save the changes to Patient Education?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        '            If Result = Windows.Forms.DialogResult.Yes Then

        '                wdPatientEducation.Save(_DocumentName, True, "", "")
        '                'If Not IsSaving And Not isClosed Then
        '                '    GenerateAuditLogForSave()
        '                'End If

        '                _isEducationChanged = False

        '                'oCurDocTemp = oCurDoc
        '                oCurDoc = Nothing
        '                wdPatientEducation.Close()

        '                oWord = New clsWordDocument
        '                _speNotes = CType(oWord.ConvertFiletoBinary(_DocumentName), Object)
        '                oWord = Nothing

        '                oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, TempExamID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)
        '                FillGrid()
        '                'SaveExamEducation(True, True)
        '                'GenerateAuditLogForSave()
        '            ElseIf Result = Windows.Forms.DialogResult.Cancel Then
        '                e.Cancel = True
        '            ElseIf Result = Windows.Forms.DialogResult.No Then
        '                'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Patient Education viewed", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
        '                e.Cancel = False
        '            End If
        '        End If
        '    End If
        'End If

        If Not e.Cancel Then
            If dictionaryExistingTemplates IsNot Nothing Then
                dictionaryExistingTemplates.Clear()
                dictionaryExistingTemplates = Nothing
            End If

            If lst_Template_Nodes IsNot Nothing Then
                lst_Template_Nodes.Clear()
                lst_Template_Nodes = Nothing
            End If

            If dvPatientEducationList IsNot Nothing Then
                dvPatientEducationList.Dispose()
                dvPatientEducationList = Nothing
            End If
            ' TemplateSearchTimer = Nothing
        End If
    End Sub

    Private Sub frmPatientEducation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_patientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            lst_Template_Nodes = New List(Of TreeNode)
            TemplateSearchTimer.Interval = 300

            txtTemplateSearch.Focus()
            _GridWidth = grdPatienEducation.Width

            FillGrid()



            If grdPatienEducation.Rows.Count > 0 Then
                '            Dim erg As New DataGridViewCellEventArgs(0, 0)
                grdPatienEducation.Rows(0).Selected = True
                ShowSingleClick(0)

            End If


            ''//To Load the Patient Strip Details
            LoadPatientStrip()

            Dim dAge As Decimal = ageDetail.Years

            If ageDetail.Months > 0 Then
                dAge = dAge + 0.1
            ElseIf ageDetail.Days > 1 Then
                dAge = dAge + 0.1
            End If

            FillPatientEducationTemplate(m_patientID, dAge)

            trvTemplate.CollapseAll()
            trvTemplate.Nodes(0).Expand()


            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                InitializeVoiceObject()
            End If
            If Me.IsMdiChild = False Then
                MyMDIParent.MdiExamChildActivate(Me)
            End If


            If ResourceType = 2 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Load, "Provider Reference Document Opened", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Load, "Provider Reference Document Opened", gloAuditTrail.ActivityOutCome.Success)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Load, "Patient Education Opened", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Load, "Patient Education Opened", gloAuditTrail.ActivityOutCome.Success)
            End If

            ' ''Resolved Bug No.68907::gloEMR> Patient Exam > Referral Letter > application shows KB311765 Office Framer Control Sample and File menu.
            'If Not IsNothing(wdTemp) Then
            '    wdTemp.Titlebar = False
            '    wdTemp.Menubar = False
            'End If

            ''Resolved Bug #72610:gloEMR: Patient Education - Application is showing an office framer control window
            If Not IsNothing(wdPatientEducation) Then
                wdPatientEducation.Titlebar = False
                wdPatientEducation.Menubar = False
            End If
            Try
                wdWordOptimizerDso.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoKeepUIActiveOnAppDeactive
            Catch ex As Exception

            End Try
            Try
                wdWordOptimizerDso.CreateNew("Word.Document")
            Catch ex As Exception

            End Try


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
#End Region

#Region " Voice Events "
    Private Event ActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Implements mdlgloVoice.IExamChildEvents.ActivateExamChild

    Private Event DeActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Implements mdlgloVoice.IExamChildEvents.DeActivateExamChild
#End Region

#Region " Private Methods "

    Private Sub FillTemplates()
        'DesignGrid()

        arrTemplates = oPatientEducation.GetPatientEductionArray(m_patientID, m_ExamID, m_VisitID)

        'If arrTemplates.Count > 0 Then

        '    If dictionaryExistingTemplates Is Nothing Then
        '        dictionaryExistingTemplates = New Dictionary(Of Long, myList)
        '    End If

        '    For Each MyListElement As myList In arrTemplates
        '        dictionaryExistingTemplates.Add(MyListElement.ID, MyListElement)
        '    Next

        '    'Dim _Row As Integer
        '    'For i As Integer = 0 To arrTemplates.Count - 1
        '    '    C1Template.Rows.Add()
        '    '    _Row = C1Template.Rows.Count - 1

        '    '    C1Template.SetData(_Row, COL_TEMPLATENAME, CType(arrTemplates(i), myList).Description)
        '    '    C1Template.SetData(_Row, COL_ID, CType(arrTemplates(i), myList).ID)
        '    'Next
        '    '_Row = Nothing  'Change made to solve memory Leak and word crash issue
        'End If

    End Sub

    ''' <summary>
    '''  To Fill Patient Education for update 
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Private Sub Fill_EducationForUpdate()

        '24-Apr-13 Aniket: Resolving Memory Leaks
        Dim dt As DataTable
        dt = oPatientEducation.GetSelectedExamEducation(m_patientID, m_VisitID)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                oWord = New clsWordDocument
                Dim strFileName As String
                strFileName = ExamNewDocumentName
                strFileName = oWord.GenerateFile(dt.Rows(0)("sPENotes"), strFileName)
                oWord = Nothing
                ''// Load the Word user Control
                If Not IsNothing(strFileName) AndAlso strFileName <> "" Then
                    LoadWordUserControl(strFileName, False)
                    oCurDoc.ActiveWindow.Application.ActiveDocument.SpellingChecked = True
                    oCurDoc.ActiveWindow.Application.ActiveDocument.ShowGrammaticalErrors = False
                    oCurDoc.ActiveWindow.View.WrapToWindow = True

                    '''' Statement to Go start of Selelcted Wd Document
                    oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
                    oCurDoc.ActiveWindow.SetFocus()
                    oCurDoc.Application.Selection.MoveRight(1, 1)
                    oCurDoc.Application.Selection.MoveLeft(1, 1)
                End If
                '' Name of the Patient Educations Given 

                '' 
                blnTemplateExist = True

            End If
            'Change made to solve memory Leak and word crash issue
            dt.Dispose()
            dt = Nothing
        End If
    End Sub

    ''' <summary>
    '''  To Fill Patient Educations associated with ICD9 
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Private Sub Fill_PatientEducation()

        For i As Int16 = 0 To m_arrTemplateID.Count - 1

            '24-Apr-13 Aniket: Resolving Memory Leaks
            Dim lst As myList
            lst = CType(m_arrTemplateID(i), myList)

            If lst.Index > 0 Then
                If blnTemplateExist = False Then

                    Call Fill_TemplateGallery(lst.Index)
                    blnTemplateExist = True
                Else

                    Call InsertTemplate(lst.Index)
                End If

                oCurDoc.ActiveWindow.Application.ActiveDocument.SpellingChecked = True
                oCurDoc.ActiveWindow.Application.ActiveDocument.ShowGrammaticalErrors = False
                oCurDoc.ActiveWindow.View.WrapToWindow = True
                '''' Statement to Go start of Selelcted Wd Document
                oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
                oCurDoc.ActiveWindow.SetFocus()
                oCurDoc.Application.Selection.MoveRight(1, 1)
                oCurDoc.Application.Selection.MoveLeft(1, 1)


                For Each myNode As TreeNode In trvTemplate.Nodes(0).Nodes
                    If CType(lst.Index, Long) = CType(myNode, myTreeNode).Key Then
                        trvTemplate.SelectedNode = myNode
                        Exit For
                    End If
                Next myNode


            End If

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(lst) = False Then
                '            lst.Dispose()
                lst = Nothing   'Change made to solve memory Leak and word crash issue
            End If

        Next

    End Sub

    Private Sub Fill_TemplateGallery(ByVal TemplateID As Long)

        Dim strFileName As String = ""
        Dim oList As New myList

        oWord = New clsWordDocument
        objCriteria = New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Template
        objCriteria.PrimaryID = TemplateID
        oWord.DocumentCriteria = objCriteria
        Dim dtTemplate As DataTable = Nothing

        If Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList Or Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication Or Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders Then
            dtTemplate = clsInfobutton_Education.GetEducationTemplate(m_patientID, TemplateID, Source, ResourceCategory, ResourceType, sDocumentName)
            If Not IsNothing(dtTemplate) Then
                If dtTemplate.Rows.Count > 0 Then
                    strFileName = ExamNewDocumentName
                    strFileName = oWord.GenerateFile(CType(dtTemplate.Rows(0)("sDescription"), Object), strFileName)
                    txtBibliography.Text = Convert.ToString(dtTemplate.Rows(0)("bibliography"))
                    txtBibliographyDeveloper.Text = Convert.ToString(dtTemplate.Rows(0)("sbDeveloper"))

                    strBibliography = Convert.ToString(dtTemplate.Rows(0)("bibliography"))
                    strBibliographyDeveloper = Convert.ToString(dtTemplate.Rows(0)("sbDeveloper"))
                End If
            End If
        Else
            strFileName = oWord.RetrieveDocumentFile()
        End If


        oList.ID = _CurrentTemplateID
        oList.Description = _CurrentTemplateName
        If (IsNothing(strFileName) = False) Then
            oList.TemplateResult = CType(oWord.ConvertFiletoBinary(strFileName), Object)
        Else
            oList.TemplateResult = Nothing
        End If

        arrTemplates.Add(oList)
        objCriteria.Dispose()
        objCriteria = Nothing

        '24-Apr-13 Aniket: Resolving Memory Leaks
        'oWord.Dispose()
        oWord = Nothing

        If Not IsNothing(strFileName) AndAlso strFileName <> "" Then
            LoadWordUserControl(strFileName, True)

            oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        End If

        strFileName = Nothing   'Change made to solve memory Leak and word crash issue

        If Not dtTemplate Is Nothing Then
            dtTemplate.Dispose()
            dtTemplate = Nothing
        End If


    End Sub

    ''' <summary>
    ''' Insert the Concerned template in the document
    ''' </summary>
    ''' <param name="TemplateID"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub InsertTemplate(ByVal TemplateID As Long)

        If Not oCurDoc Is Nothing Then

            Dim strFileName As String
            oWord = New clsWordDocument

            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Template
            objCriteria.PrimaryID = TemplateID
            ''//Get the Template from DB as physical document
            oWord.DocumentCriteria = objCriteria
            strFileName = oWord.RetrieveDocumentFile()
            objCriteria.Dispose()
            objCriteria = Nothing
            oWord = Nothing



            'wdTemp = New AxDSOFramer.AxFramerControl
            'Me.Controls.Add(wdTemp)
            'wdTemp.Open(strFileName)
            'oTempDoc = wdTemp.ActiveDocument
            If (IsNothing(strFileName) = False) Then


                Dim myLoadWord As gloWord.LoadAndCloseWord = GetMyLoadWordApplication()
                Try
                    Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(strFileName)

                    oWord = New clsWordDocument
                    objCriteria = New DocCriteria
                    objCriteria.DocCategory = enumDocCategory.Exam
                    objCriteria.PatientID = m_patientID
                    objCriteria.VisitID = m_VisitID
                    objCriteria.PrimaryID = m_ExamID
                    oWord.DocumentCriteria = objCriteria
                    oWord.CurDocument = oTempDoc
                    oWord.GetFormFieldData(enumDocType.None)
                    oTempDoc = oWord.CurDocument
                    objCriteria.Dispose()
                    objCriteria = Nothing
                    oWord = Nothing

                    oTempDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    myLoadWord.CloseWordOnly(oTempDoc)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                'wdTemp.Close()

                ''24-Apr-13 Aniket: Resolving Memory Leaks
                'Me.Controls.Remove(wdTemp)
                'wdTemp.Dispose()
                'myLoadWord.CloseApplicationOnly()
                'myLoadWord = Nothing

                oCurDoc.ActiveWindow.SetFocus()
                '''' Statement to Go end of Selelcted Wd Document
                oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)

                ''// Statement to Append Document from Path strFileName to Activate Wd window
                oCurDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                oCurDoc.Application.Selection.InsertFile(strFileName)
            End If
            oCurDoc.ActiveWindow.SetFocus()
            strFileName = Nothing   'Change made to solve memory Leak and word crash issue
        End If

    End Sub

    'Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If cmbTemplate.SelectedValue > 0 Then

    '            Fill_TemplateGallery(cmbTemplate.SelectedValue)
    '        Else
    '            wdPatientEducation.Close()
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    'Private Sub loadToolStrip()

    '    If Not tlsPatientEducation Is Nothing Then
    '        tlsPatientEducation.Dispose()
    '    End If

    '    tlsPatientEducation = New WordToolStrip.gloWordToolStrip
    '    tlsPatientEducation.Dock = DockStyle.Top
    '    tlsPatientEducation.ConnectionString = GetConnectionString()
    '    tlsPatientEducation.UserID = gnLoginID
    '    ''Integrated ON 20101020 BY Mayuri FOR SIGNATURE
    '    tlsPatientEducation.dtInput = AddChildMenu()
    '    Dim oclsProvider As New clsProvider
    '    tlsPatientEducation.ptProvider = oclsProvider.GetPatientProviderName(m_patientID)
    '    tlsPatientEducation.ptProviderId = oclsProvider.GetPatientProvider(m_patientID)
    '    'Change made to solve memory Leak and word crash issue
    '    oclsProvider.Dispose()
    '    oclsProvider = Nothing

    '    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
    '    tlsPatientEducation.IsCoSignEnabled = gblnCoSignFlag
    '    tlsPatientEducation.FormType = WordToolStrip.enumControlType.PatientEducation

    '    Me.Controls.Add(tlsPatientEducation)
    '    Me.PnlToolStrip.Controls.Add(tlsPatientEducation)
    '    Me.PnlToolStrip.Size = New System.Drawing.Size(940, 56)
    '    PnlToolStrip.SendToBack()
    '    If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
    '        If Not IsNothing(ogloVoice) Then
    '            ogloVoice.MyWordToolStrip = tlsPatientEducation
    '            ShowMicroPhone()
    '        End If
    '    End If
    '    If gblnAssociatedProviderSignature Then
    '        tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
    '        tlsPatientEducation.MyToolStrip.ButtonsToHide.Remove(tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Name)
    '    Else
    '        tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
    '        If (tlsPatientEducation.MyToolStrip.ButtonsToHide.Contains(tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
    '            tlsPatientEducation.MyToolStrip.ButtonsToHide.Add(tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Name)
    '        End If

    '    End If
    '    '''' Check Secure Messaging is enable and User has rights to access it
    '    If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
    '        If tlsPatientEducation.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
    '            If (tlsPatientEducation.MyToolStrip.ButtonsToHide.Contains(tlsPatientEducation.MyToolStrip.Items("SecureMsg").Name) = False) Then
    '                tlsPatientEducation.MyToolStrip.ButtonsToHide.Add(tlsPatientEducation.MyToolStrip.Items("SecureMsg").Name)
    '            End If
    '        End If

    '        If tlsPatientEducation.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
    '            tlsPatientEducation.MyToolStrip.Items("SecureMsg").Visible = False
    '        End If
    '    End If

    '    If ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.ProviderReferenceMaterial Then
    '        tlsPatientEducation.MyToolStrip.Items("Reference Information").Visible = True
    '    Else
    '        tlsPatientEducation.MyToolStrip.Items("Reference Information").Visible = False
    '        If (tlsPatientEducation.MyToolStrip.ButtonsToHide.Contains(tlsPatientEducation.MyToolStrip.Items("Reference Information").Name) = False) Then
    '            tlsPatientEducation.MyToolStrip.ButtonsToHide.Add(tlsPatientEducation.MyToolStrip.Items("Reference Information").Name)
    '        End If
    '    End If





    'End Sub

    Private Sub showBiblography()
        'Dim dtBiblography As DataTable
        'Dim strBiblography As String = ""
        'dtBiblography = clsInfobutton_Education.GetBibliographicinfo(nEducationID)
        'If Not IsNothing(dtBiblography) Then
        '    If dtBiblography.Rows.Count > 0 Then
        '        strBiblography = Convert.ToString(dtBiblography.Rows(0)("sBibliographicinfo"))
        pnlBibliographic.Visible = True
        txtBibliography.Text = strBibliography
        txtBibliographyDeveloper.Text = strBibliographyDeveloper
        txtBibliography.Visible = True
        txtBibliographyDeveloper.Visible = True
        'Dim oBilio As New frmBibliography(2)
        'oBilio.sBibliography = strBibliography
        'oBilio.sDeveloper = strBibliographyDeveloper
        'oBilio.ShowDialog()

        '    End If
        'End If
    End Sub

    ''Integrated ON 20101020 BY Mayuri FOR SIGNATURE
    Private Function AddChildMenu() As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim oProvider As New clsProvider
            Dim rslt As Boolean

            rslt = oProvider.CheckSignDelegateStatus()

            If rslt Then
                '24-Apr-13 Aniket: Resolving Memory Leaks

                dt = oProvider.GetAllAssignProviders(gnLoginID)
                oProvider.Dispose()
                oProvider = Nothing
                If (IsNothing(dt) = False) Then
                    If dt.Rows.Count > 0 Then
                        Return dt
                    Else
                        dt.Dispose()
                        dt = Nothing
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            Else
                oProvider.Dispose()
                oProvider = Nothing
                Return Nothing
            End If

            'oProvider.Dispose()
            'oProvider = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            'If dt IsNot Nothing Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If

        End Try
    End Function

    Public Sub SaveExamEducation(Optional ByVal IsClose As Boolean = False, Optional ByVal IsSaving As Boolean = False) ''''''''''''''' by Ujwala - Making it Public for Smart Diagnosis Changes Integration - as on 20101013
        ''Private Sub SaveExamEducation(Optional ByVal IsClose As Boolean = False)  '''''''''''''''Commented by Ujwala - Making it Public for Smart Diagnosis Changes Integration - as on 20101013
        If oCurDoc Is Nothing Then
            Exit Sub
        End If

        '' IF ANY TEMPLATE IS OPEN THEN SAVE IT FIRST IN ARRAYLIST ''
        AddTemplateInArray(IsClose, IsSaving)

        ''condition was commented by Sandip Darade for the the flow to not to use incorrect visit id 
        ''Check if Visit Id is selected or not
        '' 20101026 - Condition added - bug 5256 is fixed  
        If m_VisitID = 0 Then '' 
            m_VisitID = GenerateVisitID(m_patientID)
            'm_VisitID = gnVisitID
        End If

        If arrTemplates.Count > 0 Then
            Dim oList As myList
            For i As Int32 = 0 To arrTemplates.Count - 1
                oList = CType(arrTemplates(i), myList)
                oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, m_ExamID, oList.TemplateResult, oList.Description, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)

            Next
            oList = Nothing 'Change made to solve memory Leak and word crash issue
        End If


        If Not IsClose Then
            If Not IsNothing(_DocumentName) AndAlso _DocumentName <> "" Then
                LoadWordUserControl(_DocumentName, False)
                'Set the Start postion of the cursor in documents
                oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                oCurDoc.Saved = True
            End If
            ' _isEducationChanged = False
        End If

        If Not myCaller Is Nothing Then
            frmPatientExam.blnPEChangesMade = True
        End If

    End Sub

    Public Sub SaveExamEducations(Optional ByVal IsClose As Boolean = False, Optional ByVal IsSaving As Boolean = False) ''''''''''''''' by Ujwala - Making it Public for Smart Diagnosis Changes Integration - as on 20101013

        If oCurDoc Is Nothing Then
            Exit Sub
        End If

        AddTemplateAndSave(IsClose, IsSaving)

        If m_VisitID = 0 Then '' 
            m_VisitID = GenerateVisitID(m_patientID)

        End If



        oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, TempExamID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)

        If _FromOutside = True And pnlPreview.Visible = True Then
            SaveSmartEducation()
        End If


        If Not IsClose Then
            If Not IsNothing(_DocumentName) AndAlso _DocumentName <> "" Then
                LoadWordUserControl(_DocumentName, False)
                oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                oCurDoc.Saved = True
            End If
        End If

        If Not myCaller Is Nothing Then
            frmPatientExam.blnPEChangesMade = True
        End If

        'Open From TreeView
        If _ISTreeviewOpen = True Then
            _ISTreeviewOpen = False
        End If

        'Open From Grid
        If _FromGrid = True Then
            _FromGrid = False
        End If

    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTemplateSearch.TextChanged
        Try
            If TemplateSearchTimer IsNot Nothing Then
                With TemplateSearchTimer
                    .Stop()
                    .Interval = 1200
                    .Start()
                End With
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SearchEvent() Handles TemplateSearchTimer.Tick
        Dim sSearchedText As String = Convert.ToString(txtTemplateSearch.Text.ToLower)
        TemplateSearchTimer.Stop()
        If sSearchedText <> " " Then
            Dim enumMatchedNodes As IEnumerable(Of TreeNode) = Nothing
            enumMatchedNodes = lst_Template_Nodes.Where(Function(p) Convert.ToString(p.Text.ToLower.Contains(sSearchedText)))

            BindNodesToTreeView(enumMatchedNodes.ToList)

            enumMatchedNodes = Nothing
            sSearchedText = Nothing
        End If
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTemplateSearch.KeyPress

        If trvTemplate.GetNodeCount(False) > 0 Then
            If (e.KeyChar = ChrW(13) Or e.KeyChar = ChrW(10)) Then
                trvTemplate.Nodes(1).Expand()
            End If
        End If
    End Sub


    Private Sub Print()
        If Not oCurDoc Is Nothing Then
            GeneratePrintFaxDocument()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Print, "Patient Education '" & txtSelectedTemplates.Text & "' Printed.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011


            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Patient Education '" & txtSelectedTemplates.Text & "' Printed.", gstrLoginName, gstrClientMachineName, gnPatientID)
        End If

    End Sub

    ''' <summary>
    ''' To print or fax the Patient Education
    ''' </summary>
    ''' <param name="IsPrintFlag">Flag to be set False for Fax, by default is true for print</param>
    ''' <remarks></remarks>
    Private Sub GeneratePrintFaxDocument(Optional ByVal IsPrintFlag As Boolean = True)
        Dim PageNo As Integer = 0
        Dim totalPages As Integer = 0
        Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
        Dim Missing As Object = System.Reflection.Missing.Value

        If oCurDoc Is Nothing Then
            Exit Sub
        End If

        Dim _SaveFlag As Boolean = False

        If oCurDoc.Saved Then
            _SaveFlag = True
        End If

        'Dim sFileName As String = ExamNewDocumentName

        'Ashish added on 31st October 
        'to prevent screen from refreshing
        'Dim wordRefresh As New WordRefresh()
        'Dim WDocViewType As Wd.WdViewType
        'If IsPrintFlag Then
        '    'Ashish added on 31st October 
        '    'to prevent screen from refreshing
        '    'WDocViewType = oCurDoc.ActiveWindow.View.Type
        '    'wordRefresh.OptimizePerformance(False, oCurDoc, 0)
        'End If
        If IsNothing(wdPatientEducation) = False AndAlso IsNothing(oWordApp) = False Then
            Try
                gloWord.LoadAndCloseWord.SaveDSO(wdPatientEducation, oCurDoc, oWordApp)
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
            'Try

            '    oCurDoc.SaveAs(oCurDoc.FullName)
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    Try
            '        oCurDoc.Save()
            '    Catch ex1 As Exception

            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            '    End Try
            'End Try
            '  oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            'wdPatientEducation.Close()

            'If Not File.Exists(sFileName) Then
            '    Try
            '        File.Copy(oCurDoc.FullName, sFileName)
            '    Catch ex As Exception
            '        MessageBox.Show("Error while printing or faxing. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '        ex = Nothing
            '    End Try
            'End If

            'If Not File.Exists(sFileName) Then
            '    Exit Sub
            'End If

            'wdTemp = New AxDSOFramer.AxFramerControl
            'Me.Controls.Add(wdTemp)

            'wdTemp.Open(sFileName)  'Open Template for processing in Temp user Ctrl
            'oTempDoc = wdTemp.ActiveDocument
            Dim myLoadWord As gloWord.LoadAndCloseWord = GetMyLoadWordApplication()
            'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)
            Try
                PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, m_patientID, Nothing, totalPages, PageNo:=PageNo, iOwner:=Me)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try



            'If IsPrintFlag Then
            '    'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
            '    '    oTempDoc.Unprotect()
            '    'End If
            '    Dim oPrint As New clsPrintFAX

            '    oPrint.PrintDoc(oTempDoc, m_patientID)
            '    oPrint.Dispose()
            '    oPrint = Nothing
            'Else

            '    If ResourceType = 2 Then
            '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Fax, "Provider Reference Document Fax", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '    Else
            '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Fax, "Patient Education Fax", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '    End If
            'End If

            'wdTemp.Close()

            ''24-Apr-13 Aniket: Resolving Memory Leaks
            'Me.Controls.Remove(wdTemp)
            'wdTemp.Dispose()
            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Saved = _SaveFlag
            End If
        End If

        'LoadWordUserControl(sFileName, False)
        'oCurDoc.ActiveWindow.View.ShowFieldCodes = False

        'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)


        'If IsPrintFlag Then
        '    'Ashish added on 31st October 
        '    'to prevent screen from refreshing
        '    'WordRefresh.OptimizePerformance(True, oCurDoc, WDocViewType)
        '    'WDocViewType = Nothing
        'End If

        'wordRefresh.Dispose()
        'wordRefresh = Nothing

    End Sub


    ''' <summary>
    ''' Undo changes in the Word Document
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UnDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Redo Changes in the Word document
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Scan the Document for insert image or file
    ''' </summary>
    ''' <param name="nInsertScan"></param>
    ''' <remarks></remarks>
    Private Sub ImportDocument(ByVal nInsertScan As Int16)
        'Insert File - 1
        'Scan Images - 2
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            If nInsertScan = 1 Then
                Dim oFileDialogWindow As New System.Windows.Forms.OpenFileDialog
                oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf"


                oFileDialogWindow.FilterIndex = 3
                oFileDialogWindow.Title = "Insert External Documents"
                oFileDialogWindow.Multiselect = False
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)
                    If oFile.Extension.ToUpper = UCase(".Doc") Or oFile.Extension.ToUpper = UCase(".Docx") Or oFile.Extension.ToUpper = UCase(".txt") Or oFile.Extension.ToUpper = UCase(".rtf") Then

                        oCurDoc.ActiveWindow.SetFocus()

                        oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                    End If

                    '26-Apr-13 Aniket: Resolving Memory Leaks
                    oFile = Nothing
                End If

                '26-Apr-13 Aniket: Resolving Memory Leaks
                oFileDialogWindow.Dispose()
                oFileDialogWindow = Nothing

            ElseIf nInsertScan = 2 Then

                Dim oFiles As New ArrayList()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()


                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)

                oEDocument.ShowEScannerForImages(m_patientID, oFiles)

                oEDocument.Dispose()

                Dim firstFlag As Boolean = True
                Dim i As Integer
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                For i = 0 To oFiles.Count - 1
                    If File.Exists(oFiles.Item(i)) Then

                        oCurDoc.ActiveWindow.SetFocus()


                        Dim oWord As New clsWordDocument
                        oWord.GetandSetMyFirstFlag(True, firstFlag)
                        oWord.CurDocument = oCurDoc
                        oWord.InsertImage(oFiles.Item(i))
                        firstFlag = oWord.GetandSetMyFirstFlag(False, False)
                        oWord = Nothing

                        If oCurDoc.Application.ActiveDocument.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdNoProtection Then
                            Try
                                oCurDoc.Application.Selection.EndKey()
                                oCurDoc.Application.Selection.InsertBreak()
                            Catch
                            End Try
                        End If


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

                firstFlag = Nothing
                oFiles = Nothing
                i = Nothing


            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, "Import Document " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' Load patient Strip details
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadPatientStrip()
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(m_patientID, gloUC_PatientStrip.enumFormName.PatientEducation)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.BringToFront()
        '  _PatientStrip.Padding = New Padding(0, 0, 0, 3)
        'Me.Controls.Add(_PatientStrip)
        pnlMain.Controls.Add(_PatientStrip)
        '  pnlTreeView.BringToFront()
        '  Splitter1.BringToFront()
        ' pnlMain.BringToFront()
        'loadToolStrip()

        ageDetail = _PatientStrip.FormatAge(_PatientStrip.PatientDateOfBirth)


        'dAge = _PatientStrip.FormatAge(_PatientStrip.PatientDateOfBirth).days

        If frmPatientExam._IsExam = True Then
            _PatientStrip.DTPValue = frmPatientExam.dtpvaluefrmexam
        End If
    End Sub


    ''' <summary>
    ''' Load the  Word Document in the Dso control
    ''' </summary>
    ''' <param name="strFileName"></param>
    ''' <param name="blnGetData"></param>
    ''' <remarks></remarks>
    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        Try
            wdPatientEducation.Visible = True
            Application.DoEvents()
            'Start ->dhruv 20100625 -> Check for the file exists
            If System.IO.File.Exists(strFileName) = True Then
                'wdPatientEducation.Open(strFileName)
                ' Dim oWordApp As Wd.Application = Nothing
                gloWord.LoadAndCloseWord.OpenDSO(wdPatientEducation, strFileName, oCurDoc, oWordApp)
            Else
                Return
            End If
            'End->Start ->dhruv 20100625 -> Check for the file exists

            ''//To retrieve the Form fields for the Word document
            If blnGetData Then
                oWord = New clsWordDocument
                objCriteria = New DocCriteria
                'Bug #498 : Liquid Link
                'Document category set as others instead of exams
                objCriteria.DocCategory = enumDocCategory.Others
                objCriteria.PatientID = m_patientID
                objCriteria.VisitID = m_VisitID
                objCriteria.PrimaryID = m_ExamID
                oWord.DocumentCriteria = objCriteria
                oWord.CurDocument = oCurDoc
                ''//Replace Form fields with Concerned data from DB
                oWord.GetFormFieldData(enumDocType.None)
                oCurDoc = oWord.CurDocument
                Try
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                If Not IsNothing(objCriteria) Then
                    objCriteria.Dispose()
                    objCriteria = Nothing
                End If
               
                oWord = Nothing
            Else
                oWord = New clsWordDocument
                oWord.CurDocument = oCurDoc
                oWord.HighlightColor()
                oCurDoc = oWord.CurDocument
                Try
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing

                End Try
                oWord = Nothing
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
        ' oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)


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
                    '   r.SetRange(Sel.Start, Sel.End + 1)
                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then
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
                        '        ''  oOffice.CommandBarComboBox oDD = oPU.Controls.Add(oOffice.MsoControlType.msoControlDropdown, om, om, om, true) as oOffice.CommandBarComboBox;

                        '        Dim oDD As oOffice.CommandBarComboBox = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlDropdown, om, om, om, True), oOffice.CommandBarComboBox)
                        '        oDD.Style = oOffice.MsoComboStyle.msoComboLabel
                        '        oDD.DropDownLines = dd.ListEntries.Count
                        '        For Each le As Wd.ListEntry In dd.ListEntries
                        '            oDD.AddItem(le.Name, om)
                        '        Next
                        '        oDD.ListIndex = iCurSel
                        '        'CType(oPU, oOffice.CommandBar).ShowPopup(om, om)
                        '        oPU.ShowPopup(om, om)
                        '        dd.Value = oDD.ListIndex
                        '    Else
                        '        myidx = dd.Value
                        '        Dim iter As Integer = 1
                        '        For Each le As Wd.ListEntry In dd.ListEntries
                        '            Dim btn As oOffice.CommandBarButton
                        '            btn = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), oOffice.CommandBarButton)
                        '            '   btn = CType(ConversionHelpers.AsWorkaround(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), GetType(oOffice.CommandBarButton)), oOffice.CommandBarButton)
                        '            btn.Style = oOffice.MsoButtonStyle.msoButtonAutomatic
                        '            btn.Caption = le.Name
                        '            btn.Enabled = True
                        '            If iter = myidx Then
                        '                btn.State = oOffice.MsoButtonState.msoButtonDown
                        '            End If
                        '            System.Math.Min(System.Threading.Interlocked.Increment(iter), iter - 1)
                        '            AddHandler btn.Click, AddressOf btn_Click
                        '        Next
                        '        'CType(oPU, oOffice.CommandBar).ShowPopup(om, om)
                        '        oPU.ShowPopup(om, om)
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
                        'Change made to solve memory Leak and word crash issue


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
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

    ''' <summary>
    ''' To Load the Word User Control with New Document
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadNewDocument()
        '  ReInitUserControl()
        ''// To open the new document in the Word user control
        wdPatientEducation.CreateNew("Word.Document")
    End Sub

    Private Sub wdPatientEducation_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdPatientEducation.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then

                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    isHandlerRemoved = True
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "BeforeDocumentClosed - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                           
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "BeforeDocumentClosed - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub wdPatientEducation_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdPatientEducation.OnDocumentClosed

        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    '  Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "OnDocumentClosed - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub wdPatientEducation_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdPatientEducation.OnDocumentOpened
        oCurDoc = wdPatientEducation.ActiveDocument
        oWordApp = oCurDoc.Application


        Try
            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        Try
            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        oCurDoc.ActiveWindow.SetFocus()
    End Sub


    Private Sub tlsPatientEducation_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsPatientEducation.ToolStripButtonClick
        If IsNothing(oCurDoc) = False Then
            InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
        End If
    End Sub

    ''' <summary>
    ''' To Implement tool strip items click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tlsPatientEducation_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsPatientEducation.ToolStripClick
        Try
            ''******Shweta 20090828 *********'
            ''To check exeception related to word
            'If CheckWordForException() = False Then
            '    Exit Sub
            'End If
            ''End Shweta

            Select Case e.ClickedItem.Name
                Case "Mic"
                    'UpdateVoiceLog("SwitchOff Mic started from tlsReferrals_ItemClicked in Referrals is invoked")
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
                    End If
                Case "Save"
                    Call SaveExamEducations(, True)
                    FillGrid()
                    SetGrid()
                    GenerateAuditLogForSave()
                Case "Save & Close"
                    Call SaveExamEducation(True)
                    'FillGrid()
                    GenerateAuditLogForSave()
                    Me.Close()
                Case "Print"
                    Call Print()
                Case "FAX"
                    bnlIsFaxOpened = True
                    Call GeneratePrintFaxDocument(False)
                    bnlIsFaxOpened = False
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
                Case "Capture Sign"
                    Call InsertSignature()
                Case "Undo"
                    Call UnDoChanges()
                Case "Redo"
                    Call ReDoChanges()
                Case "Insert File"
                    ImportDocument(1)
                Case "Scan Documents"
                    ImportDocument(2)

                Case "tblbtn_StrikeThrough"
                    '' chetan added on 25-oct-2010 for Strike Through
                    InsertStrike()
                Case "Close"

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
                Case "Reference Information"
                    showBiblography()
                Case "Export"
                    ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Patient Education", Me)
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    Result = Nothing    'Change made to solve memory Leak and word crash issue
                    objword1 = Nothing
                    ' Export Function for Word Docs Integrated by dipak  as on 26 oct 2010
                Case "SecureMsg"
                    If strProviderDirectAddress <> "" Then
                        Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(m_patientID)
                        If sError <> "" Then
                            MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                            Return
                        Else
                            Call SendSecureMsg()
                        End If

                    Else
                        MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SendSecureMsg()

        If Not oCurDoc Is Nothing Then
            GenerateSecureMsgDocument()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.ClinicalExchange, "Send Patient Education using Secure Message", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        End If

    End Sub

    Private Sub GenerateSecureMsgDocument()
        Dim _SaveFlag As Boolean = False
        If oCurDoc.Saved Then
            _SaveFlag = True
        End If
        Try
            gloWord.LoadAndCloseWord.SaveDSO(wdPatientEducation, oCurDoc, oWordApp)
        Catch ex As Exception

        End Try
        'Try
        '    oCurDoc.SaveAs(oCurDoc.FullName)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    Try
        '        oCurDoc.Save()
        '    Catch ex1 As Exception

        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        '    End Try
        'End Try
        '      Dim sFileName As String = ExamNewDocumentName

        '  wdPTProtocols.Save(sFileName, True, "", "")
        'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
        '   wdPatientEducation.Close()

        'wdTemp = New AxDSOFramer.AxFramerControl

        'Me.Controls.Add(wdTemp)


        'wdTemp.Open(sFileName)
        'oTempDoc = wdTemp.ActiveDocument
        'oTempDoc.ActiveWindow.SetFocus()
        Dim myLoadWord As gloWord.LoadAndCloseWord = GetMyLoadWordApplication()
        Dim osenddox As String = String.Empty
        Try
            osenddox = SendWord.MdlSendWord.SendWordDocument(myLoadWord, oCurDoc.FullName, m_patientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        '  Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If

        'Dim oSendDoc As New clsPrintFAX
        'Dim osenddox As String
        'osenddox = oSendDoc.SendDoc(oTempDoc, m_patientID)
        'oSendDoc.Dispose()
        'oSendDoc = Nothing
        'wdTemp.Close()
        'Me.Controls.Remove(wdTemp)
        'wdTemp.Dispose()
        'wdTemp = Nothing
        ' myLoadWord.CloseWordApplication(oTempDoc)
        myLoadWord.CloseApplicationOnly()
        myLoadWord = Nothing
        oCurDoc.Saved = _SaveFlag

        'gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString = GetConnectionString()
        'gloSurescriptSecureMessage.SecureMessageProperties.UserID = System.Convert.ToInt64(appSettings("UserID"))
        'gloSurescriptSecureMessage.SecureMessageProperties.UserName = appSettings("UserName")
        'gloSurescriptSecureMessage.SecureMessageProperties.ProviderID = appSettings("ProviderID")
        'gloSurescriptSecureMessage.SecureMessageProperties.IsStagingServerEnable = gblnIsSecureStagingsever
        'gloSurescriptSecureMessage.SecureMessageProperties.StagingServerUrl = gstrSecureStagingUrl
        'gloSurescriptSecureMessage.SecureMessageProperties.ProductionServerUrl = gstrSecureProductionUrl

        ''Read Secure Messages settings and call Inbox form
        If (osenddox.Length > 0) Then
            If File.Exists(osenddox) Then
                Dim ofrmSendNewMail As New InBox.NewMail(m_patientID, osenddox)
                ofrmSendNewMail.ShowInTaskbar = True
                ofrmSendNewMail.ShowDialog()
                'ofrmInbox.Dispose()
                ofrmSendNewMail.Close()
                ofrmSendNewMail = Nothing
            Else
                MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        'LoadWordUserControl(sFileName, False)
        'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
        'Set the Start postion of the cursor in documents
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


                    End If
                End If
            End If
            strThrough = Nothing    'Change made to solve memory Leak and word crash issue
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    'Ashish added on 25-Oct-2013 for Audit Logging
    Private Sub GenerateAuditLogForSave()
        Try
            If hashNewTemplates IsNot Nothing Then
                If hashNewTemplates.Count > 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Patient Education Added", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    hashNewTemplates.Clear()
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Modify, "Patient Education Modified", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Modify, "Patient Education Modified", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "AuditLogging - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub frmPatientEducation_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged

    End Sub

    Private Sub frmPatientEducation_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown

    End Sub

    ''' <summary>
    ''' Add voice commands from custom collection to DgnStrings
    ''' </summary>
    ''' <remarks></remarks>
    Private Function AddBasicVoiceCommands() As Hashtable
        'OrdersVoicecol.Clear()
        'OrdersVoicecol.Add("Save Orders")
        'OrdersVoicecol.Add("Print Orders")
        'OrdersVoicecol.Add("Fax Orders")
        'OrdersVoicecol.Add("Save and Close")
        'OrdersVoicecol.Add("Save and Close Orders")
        'OrdersVoicecol.Add("Insert Signature")
        'OrdersVoicecol.Add("Close Orders")
        'OrdersVoicecol.Add("Finish Orders")
        'OrdersVoicecol.Add("Orders")
        'OrdersVoicecol.Add("Radiology Orders")
        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save Education", "Save")
        oHashtable.Add("Print Education", "Print")
        oHashtable.Add("Fax Education", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close Education", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close Education", "Close")
        oHashtable.Add("Finish Education", "Save & Finish")
        Return oHashtable

    End Function

    ''' <summary>
    ''' Initialise glovoice class
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

        ogloVoice.MyWordToolStrip = Me.tlsPatientEducation
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Exam
        ogloVoice.MessageName = "Education"
        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsPatientEducation_ToolStripClick)
        'Change made to solve memory Leak and word crash issue
        If Not oHashtable Is Nothing Then
            oHashtable = Nothing
        End If
    End Sub

    'Private Sub DesignGrid()
    '    C1Template.Rows.Count = 0
    '    C1Template.Cols.Count = COL_TOTAL
    '    C1Template.Rows.Fixed = 0
    '    C1Template.Cols(0).Visible = False
    '    C1Template.Cols(COL_ID).Visible = False

    '    C1Template.Cols(COL_TEMPLATENAME).Width = C1Template.Width
    '    C1Template.AllowEditing = False
    'End Sub

    Private Sub OpenEducationTemplate(ByVal nTemplateID As Int64, ByVal sTemplateName As String)

        '' SET CURRENT OPEN TEMPLATE DETAILS ''
        _CurrentTemplateID = nTemplateID
        _CurrentTemplateName = sTemplateName

        '' FIRST FIND TEMLATE IN ARRAYLIST ''
        If arrTemplates.Count > 0 Then
            For i As Int32 = 0 To arrTemplates.Count - 1
                If CType(arrTemplates(i), myList).ID = nTemplateID Then

                    _DocumentName = ExamNewDocumentName ''Application.StartupPath & "\Temp\Temp9.doc"
                    oWord = New clsWordDocument
                    _DocumentName = oWord.GenerateFile(CType(arrTemplates(i), myList).TemplateResult, _DocumentName)
                    oWord = Nothing
                    If Not IsNothing(_DocumentName) AndAlso _DocumentName <> "" Then
                        LoadWordUserControl(_DocumentName, False)
                        'Set the Start postion of the cursor in documents
                        oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    End If
                    Exit Sub
                End If
            Next
        End If




        '' IF TEMPLATE NOT FOUND IN ARRALIST, THEN FETCH IT FROM TEMLPLATE GALLERY ''
        Fill_TemplateGallery(nTemplateID)

    End Sub

    Private Sub OpenEducationTemplate(ByVal nTemplateID As Int64, ByVal sTemplateName As String, ByVal sPeNotes As Object)

        '' SET CURRENT OPEN TEMPLATE DETAILS ''
        _CurrentTemplateID = nTemplateID
        _CurrentTemplateName = sTemplateName

        '' FIRST FIND TEMLATE IN ARRAYLIST ''
        _DocumentName = ExamNewDocumentName ''Application.StartupPath & "\Temp\Temp9.doc"
        oWord = New clsWordDocument
        _DocumentName = oWord.GenerateFile(sPeNotes, _DocumentName)
        oWord = Nothing

        If Not IsNothing(_DocumentName) AndAlso _DocumentName <> "" Then
            LoadWordUserControl(_DocumentName, True)
            'Set the Start postion of the cursor in documents
            oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        End If
        Exit Sub

    End Sub

    Private Function AddTemplateInArray(Optional ByVal isClosed As Boolean = False, Optional ByVal IsSaving As Boolean = False) As Boolean  '' IF ANY TEMPLATE IS OPEN THEN SAVE IT FIRST IN ARRAYLIST ''
        If _CurrentTemplateID > 0 AndAlso oCurDoc IsNot Nothing Then
            _DocumentName = ExamNewDocumentName
            Dim Result As Int16
            If IsSaving = True Then
                Result = Windows.Forms.DialogResult.Yes
            Else

                If oCurDoc.Saved = False Then
                    If isClosed Then
                        Result = Windows.Forms.DialogResult.Yes
                    Else
                        Result = MessageBox.Show("Do you want to save the changes to Template?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    End If
                    wdPatientEducation.Focus()

                    If Result = Windows.Forms.DialogResult.Cancel Then
                        Return False
                        Exit Function
                    End If
                End If

            End If
            If (Result = Windows.Forms.DialogResult.Yes) Then

                If (IsNothing(oCurDoc) = False) Then
                    oCurDoc.SaveAs(_DocumentName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                Else
                    wdPatientEducation.Save(_DocumentName, True, "", "")
                End If

                If Not IsSaving AndAlso Not isClosed Then
                    GenerateAuditLogForSave()
                End If

                _isEducationChanged = False


                oCurDoc = Nothing
                wdPatientEducation.Close()
                For i As Integer = 0 To arrTemplates.Count - 1
                    If CType(arrTemplates(i), myList).ID = _CurrentTemplateID Then
                        oWord = New clsWordDocument
                        CType(arrTemplates(i), myList).TemplateResult = CType(oWord.ConvertFiletoBinary(_DocumentName), Object)
                        oWord = Nothing
                    End If
                Next
            ElseIf (Result = Windows.Forms.DialogResult.No) Then
                _isEducationChanged = True
            End If
            Result = Nothing    'Change made to solve memory Leak and word crash issue
        End If
        Return True
    End Function

    Private Function AddTemplateAndSave(Optional ByVal isClosed As Boolean = False, Optional ByVal IsSaving As Boolean = False) As Boolean  '' IF ANY TEMPLATE IS OPEN THEN SAVE IT FIRST IN ARRAYLIST ''
        If _CurrentTemplateID > 0 AndAlso oCurDoc IsNot Nothing Then
            _DocumentName = ExamNewDocumentName
            Dim Result As Int16
            If IsSaving = True Then
                Result = Windows.Forms.DialogResult.Yes
            Else

                If oCurDoc.Saved = False Then
                    If isClosed Then
                        Result = Windows.Forms.DialogResult.Yes
                    Else
                        Result = MessageBox.Show("Do you want to save the changes to Template?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    End If
                    wdPatientEducation.Focus()

                    If Result = Windows.Forms.DialogResult.Cancel Then
                        Return False
                        Exit Function
                    End If
                End If

            End If
            If (Result = Windows.Forms.DialogResult.Yes) Then
                If (IsNothing(oCurDoc) = False) Then
                    oCurDoc.SaveAs(_DocumentName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                Else
                    wdPatientEducation.Save(_DocumentName, True, "", "")
                End If
                If Not IsSaving And Not isClosed Then
                    GenerateAuditLogForSave()
                End If

                _isEducationChanged = False


                oCurDoc = Nothing
                wdPatientEducation.Close()

                oWord = New clsWordDocument
                _speNotes = CType(oWord.ConvertFiletoBinary(_DocumentName), Object)
                oWord = Nothing


            ElseIf (Result = Windows.Forms.DialogResult.No) Then
                _isEducationChanged = True
            End If
            Result = Nothing    'Change made to solve memory Leak and word crash issue
        End If
        Return True
    End Function



    'Private Sub On_RemoveTemplate_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Dim _Row As Int32 = C1Template.Row
    '        If _Row >= 0 Then
    '            Dim _ID As Int64 = CType(C1Template.GetData(_Row, COL_ID), Int64)

    '            '' REMOVE TEMPLATE FROM ARRAYLIST ''
    '            If arrTemplates.Count > 0 Then
    '                '26-Apr-13 Aniket: Resolving Memory Leaks
    '                For i As Int32 = arrTemplates.Count - 1 To 0 Step -1
    '                    If CType(arrTemplates(i), myList).ID = _ID Then
    '                        arrTemplates.RemoveAt(i)
    '                        Exit For
    '                    End If
    '                Next
    '            End If

    '            '' REMOVE TEMLATE FROM C1 ''
    '            C1Template.Rows.Remove(_Row)
    '        End If
    '        _Row = Nothing  'Change made to solve memory Leak and word crash issue

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
#End Region

#Region " Public Methods "

    Public Sub InsertCoSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            oWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = m_patientID
            'end modification
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID '' For inserting coSignature
            oWord.DocumentCriteria = objCriteria

            Imagepath = oWord.getData_FromDB("User_MST.imgSignature", "Co-Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            oWord = Nothing
            Imagepath = Mid(Imagepath, 1, Len(Imagepath) - 2)
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            If System.IO.File.Exists(Imagepath) Then
                oCurDoc.ActiveWindow.SetFocus()

                '' SUDHIR 20090619 '' 
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(Imagepath)
                oWord = Nothing
                'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=Imagepath, LinkToFile:=False, SaveWithDocument:=True)
                '' END SUDHIR ''

                oCurDoc.Application.Selection.TypeParagraph()
                '' By Mahesh Signature With Date - 20070113
                '''' Add Date Time When Signature is Inserted
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                ''''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Co-Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "PatientEducation: Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "PatientEducation: Co-Signature Inserted", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "PatientEducation: Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, " Patient Eduction : " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

            objWord = New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = m_patientID
            'end modification
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID
            objWord.DocumentCriteria = objCriteria

            Imagepath = objWord.getData_FromDB("User_MST.imgSignature", "Provider Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
            Imagepath = Mid(Imagepath, 1, Len(Imagepath) - 2)

            If File.Exists(Imagepath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(Imagepath)
                oWord = Nothing
                'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                If wdRng.Tables.Count > 0 Then
                    'oCurDoc.Application.Selection.Move(1)
                    oCurDoc.Application.Selection.EndKey()
                End If
                'end code added by dipak 
                oCurDoc.Application.Selection.TypeParagraph()
                '' oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Patient Education", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub
    ''' <summary>
    ''' Insert Resppective provider Signature
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        'Try
        '    Dim rslt As Boolean
        '    Dim oclsProvider As New clsProvider
        '    rslt = oclsProvider.CheckSignDelegateStatus()
        '    Dim _ProviderID As Int64
        '    If ProviderID <> 0 Then

        '        Dim blnResult As Boolean
        '        Dim Pat_Provider As String
        '        Dim SelectedProvider As String
        '        Dim dResult As DialogResult
        '        blnResult = oclsProvider.CheckpatientProviderStatus(m_patientID, ProviderID)
        '        If blnSignClick = False Then
        '            If blnResult Then
        '                ''Selected Provider Is Exam Provider
        '            Else
        '                Pat_Provider = oclsProvider.GetPatientProviderName(m_patientID)
        '                SelectedProvider = oclsProvider.GetProviderName(ProviderID)
        '                dResult = MessageBox.Show("Patient provider '" & Pat_Provider & "' does not match provider selected for signature '" & SelectedProvider & "'.  Would you like to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '                If dResult = Windows.Forms.DialogResult.Yes Then
        '                    ''Insert The Selectedd Provider Sign
        '                Else
        '                    Return
        '                End If
        '            End If
        '        End If
        '    Else
        '        If rslt Then

        '            _ProviderID = oclsProvider.GetPatientProvider(m_patientID)
        '            Dim dt As DataTable
        '            Dim _IsSignRight As Boolean = False
        '            Dim i As Int16
        '            dt = New DataTable
        '            dt = oclsProvider.GetAllAssignProviders(gnLoginID)
        '            If dt.Rows.Count = 0 Then
        '                If _ProviderID <> gnLoginProviderID Then
        '                    MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                    Return
        '                End If
        '            Else
        '                If dt.Rows.Count > 0 Then
        '                    For i = 0 To dt.Rows.Count - 1
        '                        If _ProviderID = dt.Rows(i)("nProviderId").ToString().Trim() Or _ProviderID = gnLoginProviderID Then
        '                            _IsSignRight = True
        '                        End If
        '                    Next
        '                    If _IsSignRight = False Then
        '                        'MessageBox.Show("Current user is not designated as a Signature Delegate for selected patient's provider. Signature delegates may be added in Provider setup in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                        Dim strName As String = oclsProvider.GetPatientProviderNameWithPrefix(m_patientID)
        '                        MessageBox.Show("User '" & gstrLoginName & "' is not designated as a Signature Delegate for '" & strName & "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                        oclsProvider = Nothing
        '                        Return
        '                    End If
        '                End If
        '            End If
        '        End If
        '    End If
        '    '''''End integrated by Mayuri:20101020
        '    Dim objWord As New clsWordDocument
        '    Dim objCriteria As DocCriteria
        '    objCriteria = New DocCriteria
        '    objCriteria.DocCategory = enumDocCategory.Exam
        '    objCriteria.PatientID = m_patientID
        '    objCriteria.VisitID = m_VisitID
        '    objCriteria.ProviderID = ProviderID
        '    If (m_ExamID <> 0) Then
        '        objCriteria.PrimaryID = m_ExamID
        '    Else
        '        objCriteria.PrimaryID = 0
        '        objCriteria.DocCategory = enumDocCategory.Others
        '    End If

        '    objWord.DocumentCriteria = objCriteria
        '    ' Dim ExamProviderId As Long
        '    Dim clsExam As New clsPatientExams
        '    ' ExamProviderId = clsExam.GetProviderIdforExam(m_ExamID)

        '    Dim strProviderName As String
        '    ''Added on 20101008 by snajog for signature
        '    If ProviderID <> 0 Then
        '        strProviderName = clsExam.GetProvidernameforExam(ProviderID)
        '    Else
        '        strProviderName = clsExam.GetProvidernameforExam(_ProviderID)
        '    End If


        '    Imagepath = objWord.getData_FromDB("Provider_MST.imgSignature", "Provider Signature")
        '    objCriteria = Nothing
        '    objWord = Nothing
        '    Imagepath = Mid(Imagepath, 1, Len(Imagepath) - 2)
        '    If Imagepath = "" Then
        '        If blnSignClick = True Then
        '            MessageBox.Show("Current user has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Else
        '            MessageBox.Show("Selected Provider has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        End If
        '        Return
        '    End If

        '    If oCurDoc Is Nothing Then
        '        Exit Sub
        '    End If
        '    If File.Exists(Imagepath) Then

        '        '' SUDHIR 20090619 '' 
        '        Dim oWord As New clsWordDocument
        '        oWord.CurDocument = oCurDoc
        '        oWord.InsertImage(Imagepath)
        '        oWord = Nothing
        '        'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=Imagepath, LinkToFile:=False, SaveWithDocument:=True)
        '        '' END SUDHIR ''
        '        'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
        '        Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
        '        If wdRng.Tables.Count > 0 Then
        '            'oCurDoc.Application.Selection.Move(1)
        '            oCurDoc.Application.Selection.EndKey()
        '        End If
        '        'end code added by dipak 
        '        oCurDoc.Application.Selection.TypeParagraph()
        '        '' By Mahesh Signature With Date - 20070113
        '        ' Add Date Time When Signature is Inserted
        '        'oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
        '        'oCurDoc.Application.Selection.TypeParagraph()


        '        'Developer: Yatin N.Bhagat
        '        'Date:01/20/2012
        '        'Bug ID/PRD Name/Salesforce Case:Salesforce Case No.GLO2010-0009688 
        '        'Reason: If Condition is added to check the Setting
        '        If oclsProvider.AddUserNameInProviderSignature() Then
        '            oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
        '        Else
        '            oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time")) '& " (" & gstrLoginName & ")"
        '        End If

        '        ''oCurDoc.Application.Selection.TypeParagraph()
        '        ''gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
        '        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "PatientEducation: Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
        '        ''Added Rahul P on 20101011
        '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "PatientEducation: Signature Inserted", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        '        ''
        '        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "PatientEducation: Signature Inserted", gloAuditTrail.ActivityOutCome.Success)

        '    End If

        'Catch objErr As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Patient Education " & objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try



        'Developer:Yatin N. Bhagat
        'Date:01/31/2012
        'Bug ID/PRD Name/Salesforce Case:Provider Signature Format Case
        'Reason: Comman Fucntionality is added 
        Try


            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            'dtMessage.Tag = GenerateVisitID(dtMessage.Value, m_PatientID)

            'Change made to solve memory Leak and word crash issue
            objWord = New clsWordDocument
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, m_patientID, 0, blnSignClick)
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
                    wdRng = Nothing 'Change made to solve memory Leak and word crash issue
                    'end code added by dipak 
                    oCurDoc.Application.Selection.TypeParagraph()
                    '' By Mahesh Signature With Date - 20070113
                    '' Add Date Time When Signature is Inserted
                    ''oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                    oCurDoc.Application.Selection.TypeText(Text:=pSign(1))
                    gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Capture signature and insert in the document
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InsertSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            Imagepath = ""

            Dim frm As New FrmSignature
            frm.Owner = Me
            ' frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.parent))
            frm.ShowDialog(frm.Parent)
            'Change made to solve memory Leak and word crash issue
            frm.Close()
            frm.Dispose()
            frm = Nothing

            ''commented by Dhruv 20091214 
            ''To not to save on form closing
            'If File.Exists(Imagepath) Then
            '    If Not oCurDoc Is Nothing Then

            '        oCurDoc.ActiveWindow.SetFocus()

            '        '' SUDHIR 20090619 '' 
            '        Dim oWord As New clsWordDocument
            '        oWord.CurDocument = oCurDoc
            '        oWord.InsertImage(Imagepath)
            '        oWord = Nothing
            '        'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=Imagepath, LinkToFile:=False, SaveWithDocument:=True)
            '        '' END SUDHIR ''

            '        oCurDoc.Application.Selection.TypeParagraph()
            '        '' By Mahesh Signature With Date - 20070113
            '        '' Add Date Time When Signature is Inserted
            '        oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))

            '    End If
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Patient Education " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        End If

    End Sub

    ''' <summary>
    ''' Trigger Voice commands
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    Public Sub ActivateBasicVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds

    End Sub

    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
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
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
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
    ''' Show microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If

        End If
    End Sub

    ''' <summary>
    ''' Turnoff microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If

        End If
    End Sub

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate
        If strstring = "ON" Then
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                tlsPatientEducation.MyToolStrip.Items("Mic").Visible = True
                tlsPatientEducation.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                tlsPatientEducation.MyToolStrip.ButtonsToHide.Remove(tlsPatientEducation.MyToolStrip.Items("Mic").Name)
            End If
        ElseIf strstring = "OFF" Then
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                tlsPatientEducation.MyToolStrip.Items("Mic").Visible = True
                tlsPatientEducation.MyToolStrip.ButtonsToHide.Remove(tlsPatientEducation.MyToolStrip.Items("Mic").Name)
            Else
                tlsPatientEducation.MyToolStrip.Items("Mic").Visible = False
                If tlsPatientEducation.MyToolStrip.ButtonsToHide.Contains(tlsPatientEducation.MyToolStrip.Items("Mic").Name) = False Then
                    tlsPatientEducation.MyToolStrip.ButtonsToHide.Add(tlsPatientEducation.MyToolStrip.Items("Mic").Name)
                End If
            End If
            tlsPatientEducation.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
        Else


            If bnlIsEduPreviewOpened Then 'Check if Education Preview is open
                For Each frm As Form In Application.OpenForms
                    If frm.Name = "frmPatientEducationPreview" Then
                        If DirectCast(frm, gloEMR.frmPatientEducationPreview).bnlIsFaxOpened Then 'Check If Fax form is open from Education Preview
                            For Each frm1 As Form In Application.OpenForms
                                If frm1.Name = "frmSelectContactFAXWithFAXCoverPage" Then
                                    If Not IsNothing(DirectCast(frm1, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc) Then
                                        Try
                                            DirectCast(frm1, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc.ActiveWindow.SetFocus()
                                            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=DirectCast(frm1, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                                            Exit For
                                        Catch ex As Exception
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                            ex = Nothing
                                        End Try
                                    End If
                                End If
                            Next
                            Exit For
                        Else 'else handle F5 on Education Preview form
                            If Not IsNothing(DirectCast(frm, gloEMR.frmPatientEducationPreview).oCurDoc) Then
                                Try
                                    DirectCast(frm, gloEMR.frmPatientEducationPreview).oCurDoc.ActiveWindow.SetFocus()
                                    gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=DirectCast(frm, gloEMR.frmPatientEducationPreview).oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                                    Exit For
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        End If
                    End If
                Next
            Else 'else handle F5 on Education form
                Try
                    If Not oCurDoc Is Nothing Then
                        oCurDoc.ActiveWindow.SetFocus()
                        gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                ' oCurDoc.ActiveWindow.SetFocus()
            End If
        End If
    End Sub
#End Region

    '#Region " C1 Events "
    '    Private Sub C1Template_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            Dim _Row As Integer = C1Template.Row
    '            If _Row >= 0 Then

    '                '' IF DOUBLE CLICKED TEMLPLATE IS ALREADY OPEN, THEN DON'T REOPEN IT ''
    '                If _CurrentTemplateID = CType(C1Template.GetData(_Row, COL_ID), Int64) Then
    '                    Exit Sub
    '                End If

    '                If CheckWordForException() = False Then
    '                    Exit Sub
    '                End If
    '                '' IF ANY TEMPLATE IS OPEN THEN SAVE IT FIRST IN ARRAYLIST ''
    '                If AddTemplateInArray() Then
    '                Else
    '                    Exit Sub
    '                End If



    '                '' NOW OPEN DOUBLE CLICKED TEMLPATE ''
    '                OpenEducationTemplate(CType(C1Template.GetData(_Row, COL_ID), Int64), C1Template.GetData(_Row, COL_TEMPLATENAME).ToString)

    '            End If
    '            _Row = Nothing  'Change made to solve memory Leak and word crash issue
    '        Catch ex As Exception
    '            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    End Sub

    '    Private Sub C1Template_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    '        Try

    '            If e.Button = Windows.Forms.MouseButtons.Right Then
    '                C1Template.ContextMenu = Nothing

    '                Dim oHitInfo As C1.Win.C1FlexGrid.HitTestInfo
    '                oHitInfo = C1Template.HitTest(e.X, e.Y)

    '                If oHitInfo.Row >= 0 Then
    '                    C1Template.Row = oHitInfo.Row

    '                    '26-Apr-13 Aniket: Resolving Memory Leaks
    '                    If IsNothing(oContextMenu) = False Then
    '                        oContextMenu.Dispose()
    '                        oContextMenu = Nothing
    '                    End If

    '                    If IsNothing(oItem) = False Then
    '                        oItem.Dispose()
    '                        oItem = Nothing
    '                    End If

    '                    oContextMenu = New ContextMenu
    '                    oItem = New MenuItem("Remove Template")


    '                    '26-Apr-13 Aniket: Resolving Memory Leaks
    '                    RemoveHandler oItem.Click, AddressOf On_RemoveTemplate_Click
    '                    AddHandler oItem.Click, AddressOf On_RemoveTemplate_Click

    '                    oContextMenu.MenuItems.Add(oItem)
    '                    C1Template.ContextMenu = oContextMenu
    '                    oItem = Nothing 'Change made to solve memory Leak and word crash issue
    '                End If

    '                oHitInfo = Nothing  'Change made to solve memory Leak and word crash issue
    '            End If

    '        Catch ex As Exception
    '            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try

    '    End Sub
    '#End Region


#Region "Dhruv -> For Show the Document and Modifying it while opening from View -> PatientEducation - Modify"
    Private Sub ShowDocument(ByVal EducationID As Int64, ByVal DocumentName As String)

        Try

            If CheckWordForException() = False Then
                Exit Sub
            End If
            '' IF ANY TEMPLATE IS OPEN THEN SAVE IT FIRST IN ARRAYLIST ''
            If AddTemplateInArray() Then
            Else
                Exit Sub
            End If
            '' NOW OPEN DOUBLE CLICKED TEMLPATE ''
            OpenEducationTemplate(EducationID, DocumentName)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
#End Region


    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_patientID 'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Public Sub GetdataFromOtherForms(ByVal _DocType As gloEMRWord.enumDocType) 'Implements IWord.GetdataFromOtherForms

        If Not oCurDoc Is Nothing Then
            oWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = m_patientID
            objCriteria.VisitID = m_VisitID
            ' objCriteria.PrimaryID = m_ExamID
            oWord.DocumentCriteria = objCriteria
            oWord.CurDocument = oCurDoc
            oWord.GetFormFieldData(enumDocType.None)
            objCriteria.Dispose()
            objCriteria = Nothing
            oWord = Nothing
            'wdTemp.Dispose()
            oCurDoc.ActiveWindow.SetFocus()
        End If

    End Sub



    Private Sub Dispose_Object()
        Try
            If Not IsNothing(objCriteria) Then
                objCriteria = Nothing
            End If

            'Change made to solve memory Leak and word crash issue
            ''Start

            '03-May-13 Aniket: Resolving Memory Leaks
            If Not oPatientEducation Is Nothing Then
                oPatientEducation.Dispose()
                oPatientEducation = Nothing
            End If

            If Not trvTemplate Is Nothing Then
                trvTemplate.Dispose()
                trvTemplate = Nothing
            End If

            If Not tlsPatientEducation Is Nothing Then
                '03-May-13 Aniket: Resolving Memory Leaks
                tlsPatientEducation.MyToolStrip.Items.Clear()
                tlsPatientEducation.Dispose()
                tlsPatientEducation = Nothing
            End If

            If Not _PatientStrip Is Nothing Then
                Me.Controls.Remove(_PatientStrip)
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If

            If Not wdPatientEducation Is Nothing Then
                wdPatientEducation.Dispose()
                wdPatientEducation = Nothing
            End If

            If Not arrTemplates Is Nothing Then
                '26-Apr-13 Aniket: Resolving Memory Leaks
                arrTemplates.Clear()
                arrTemplates = Nothing
            End If


            If Not IsNothing(TemplateSearchTimer) Then
                TemplateSearchTimer.Stop()
                TemplateSearchTimer.Dispose()
                TemplateSearchTimer = Nothing
            End If
            If Not IsNothing(ogloVoice) Then

                ogloVoice.Dispose()
                ogloVoice = Nothing
            End If
            If Not IsNothing(lst_Template_Nodes) Then
                lst_Template_Nodes.Clear()
                lst_Template_Nodes = Nothing
            End If
            If Not IsNothing(m_arrTemplateID) Then
                m_arrTemplateID.Clear()
                m_arrTemplateID = Nothing
            End If
            If Not IsNothing(oPatientEducation) Then
                oPatientEducation.Dispose()
                oPatientEducation = Nothing
            End If
            If Not IsNothing(arrTemplates) Then
                arrTemplates.Clear()
                arrTemplates = Nothing
            End If
            If Not IsNothing(dictionaryExistingTemplates) Then
                dictionaryExistingTemplates.Clear()
                dictionaryExistingTemplates = Nothing
            End If
            If Not IsNothing(hashNewTemplates) Then
                hashNewTemplates.Clear()
                hashNewTemplates = Nothing
            End If
            If IsNothing(oContextMenu) = False Then
                gloGlobal.cEventHelper.RemoveAllEventHandlers(oContextMenu)
                If (IsNothing(oContextMenu.MenuItems) = False) Then
                    oContextMenu.MenuItems.Clear()
                End If
                oContextMenu.Dispose()
                oContextMenu = Nothing
            End If
            If Not IsNothing(oItem) Then
                oItem.Dispose()
                oItem = Nothing
            End If
            If Not IsNothing(clsInfobutton_Education) Then

                clsInfobutton_Education = Nothing
            End If
            If Not IsNothing(dvPatientEducationList) Then
                dvPatientEducationList.Dispose()
                dvPatientEducationList = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnBibClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBibClose.Click
        pnlBibliographic.Visible = False
        txtBibliography.Text = ""
        txtBibliographyDeveloper.Text = ""
    End Sub

    Private Sub txtBibliography_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles txtBibliography.LinkClicked
        Dim p As Process = Process.Start("IExplore.exe", e.LinkText)
    End Sub


    Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Dim dv As DataView
        dv = oPatientEducation.DsDataview
        '' nEducationID, sTemplateName, nVisitID, dtVisitDate
        'Dim ts As New clsDataGridTableStyle(dv.Table.TableName())


        'DataGridViewTextBoxColumn isEdited = new DataGridViewTextBoxColumn();
        '            DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
        '            isEdited.CellTemplate = txtcell;
        '            isEdited.Name = "isEdited";
        '            isEdited.HeaderText = "Is Edited";
        '            isEdited.Visible = True;
        '            dgv.Columns.Add(isEdited);

        'grdPatienEducation.AutoGenerateColumns = False



        'grdPatienEducation.Columns[0].HeaderText = "Select";
        'dgListSubView.Columns[1].HeaderText = "Id";
        'dgListSubView.Columns[2].HeaderText = "Code";
        'dgListSubView.Columns[3].HeaderText = "Description";

        'dgListSubView.Columns[0].Visible = true;
        'dgListSubView.Columns[1].Visible = false;
        'dgListSubView.Columns[2].Visible = true;
        'dgListSubView.Columns[3].Visible = true;

        'int _width = (_thiswidth - 17) / 9;
        'dgListSubView.Columns[0].Width = _width * 1;
        'dgListSubView.Columns[2].Width = _width * 3;
        'dgListSubView.Columns[3].Width = _width * 5;
        If (IsNothing(dv) = False) Then
            Dim colEduID As New DataGridViewTextBoxColumn
            colEduID.Name = dv.Table.Columns(0).ColumnName
            colEduID.Width = 0
            colEduID.HeaderText = "Education ID"
            colEduID.Visible = False
            grdPatienEducation.Columns.Add(colEduID)

            Dim colVisitID As New DataGridViewTextBoxColumn
            colVisitID.Width = 0
            colVisitID.Name = dv.Table.Columns(1).ColumnName
            colVisitID.HeaderText = "VisitID"
            colVisitID.Visible = False
            grdPatienEducation.Columns.Add(colVisitID)

            Dim colVisitDate As New DataGridViewTextBoxColumn

            ' .Width = 0.3 * grdPatienEducation.Width
            colVisitDate.Width = 0.21 * _GridWidth
            colVisitDate.Name = dv.Table.Columns(2).ColumnName
            colVisitDate.HeaderText = "Visit Date"
            colVisitDate.Visible = True
            grdPatienEducation.Columns.Add(colVisitDate)

            'dgListSubView.Columns[0].Visible = true;
            'dgListSubView.Columns[1].Visible = false;
            'dgListSubView.Columns[2].Visible = true;

            'Dim colTemplateName As New DataGridViewTextBoxColumn
            'With colTemplateName
            '    '.Width = 0.7 * grdPatienEducation.Width
            '    .Width = 0.7 * _GridWidth
            '    .MappingName = dv.Table.Columns(3).ColumnName
            '    .HeaderText = "Educations"
            '    .NullText = ""
            'End With

            'Dim colSource As New DataGridViewTextBoxColumn
            'With colSource
            '    '.Width = 0.7 * grdPatienEducation.Width
            '    .Width = 0.36 * _GridWidth
            '    .MappingName = dv.Table.Columns(4).ColumnName
            '    .HeaderText = "Source"
            '    .NullText = ""
            'End With

            'Dim colResourceCategory As New DataGridViewTextBoxColumn
            'With colResourceCategory
            '    '.Width = 0.7 * grdPatienEducation.Width
            '    .Width = 0 * _GridWidth
            '    .MappingName = dv.Table.Columns(5).ColumnName
            '    .HeaderText = "Resource Category"
            '    .NullText = ""
            'End With

            'Dim colResourceType As New DataGridViewTextBoxColumn
            'With colResourceType
            '    '.Width = 0.7 * grdPatienEducation.Width
            '    .Width = 0 * _GridWidth
            '    .MappingName = dv.Table.Columns(6).ColumnName
            '    .HeaderText = "Resource Type"
            '    .NullText = ""
            'End With


            'Dim colDocumentUrl As New DataGridViewTextBoxColumn
            'With colDocumentUrl
            '    '.Width = 0.7 * grdPatienEducation.Width
            '    .Width = 0 * _GridWidth
            '    .MappingName = dv.Table.Columns(7).ColumnName
            '    .HeaderText = "Document URL"
            '    .NullText = ""
            'End With

            'Dim colnSource As New DataGridViewTextBoxColumn
            'With colnSource
            '    '.Width = 0.7 * grdPatienEducation.Width
            '    .Width = 0 * _GridWidth
            '    .MappingName = dv.Table.Columns(8).ColumnName
            '    .HeaderText = "nSource"
            '    .NullText = ""
            'End With

            'Dim colnResCat As New DataGridViewTextBoxColumn
            'With colnResCat
            '    '.Width = 0.7 * grdPatienEducation.Width
            '    .Width = 0 * _GridWidth
            '    .MappingName = dv.Table.Columns(9).ColumnName
            '    .HeaderText = "nResCat"
            '    .NullText = ""
            'End With

            'Dim colnResType As New DataGridViewTextBoxColumn
            'With colnResType
            '    '.Width = 0.7 * grdPatienEducation.Width
            '    .Width = 0 * _GridWidth
            '    .Name = dv.Table.Columns(10).ColumnName
            '    .HeaderText = "nResType"
            'End With


            'grdPatienEducation.Columns.Add(colnResType)


            '''''''Code is added by Anil on 20071105
            txtTemplateSearch.Text = ""
            txtTemplateSearch.Text = strsearchtxt
            If IsNothing(strcolumnName) OrElse strcolumnName = "" Then
                dv.Sort = "[" & dv.Table.Columns(1).ColumnName & "]" & strsortorder
            Else
                Dim strColumn As String = Replace(strcolumnName, "[", "")
                dv.Sort = "[" & strColumn & "]" & strSortBy
            End If
            ''''''''''''''''''''''''''''''''
            'ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {colEduID, colVisitID, colVisitDate, colTemplateName, colSource, colResourceCategory, colResourceType, colDocumentUrl, colnSource, colnResCat, colnResType})

            grdPatienEducation.Columns(0).Visible = False
            grdPatienEducation.Columns(1).Visible = False

            'grdPatienEducation.TableStyles.Clear()
            'grdPatienEducation.TableStyles.Add(ts)

            'Dim dt As New DataTable
            'dt = dv.ToTable()
            'If (dt.Rows.Count >= 1) Then
            '    ' grdPatienEducation.Select(0)
            'End If
        End If
       


    End Sub

  
    Private Sub trvTemplate_DoubleClick(sender As System.Object, e As System.EventArgs) Handles trvTemplate.DoubleClick
        Try
            If IsNothing(trvTemplate.SelectedNode) = True Then
                Exit Sub
            End If

            'If _CurrentTemplateID = CType(trvTemplate.SelectedNode, myTreeNode).Key Then
            '    Exit Sub
            'End If


            If trvTemplate.SelectedNode Is trvTemplate.Nodes(0) Or trvTemplate.SelectedNode Is trvTemplate.Nodes(1) Or trvTemplate.SelectedNode Is trvTemplate.Nodes(0).Nodes(0) Or trvTemplate.SelectedNode Is trvTemplate.Nodes(0).Nodes(1) Or trvTemplate.SelectedNode Is trvTemplate.Nodes(0).Nodes(2) Then
                Exit Sub
            End If
            Dim thisNode As myTreeNode = CType(trvTemplate.SelectedNode, myTreeNode)
            Dim _TemplateID As Int64 = thisNode.Key
            Dim _TemplateName As String = thisNode.Text
            Dim _source As String = thisNode.Tag



            If _source = "Other" Then
                Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.None
                ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
                ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientEducation
            Else
                If (_source = "ProblemList") Then
                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                    ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial

                ElseIf (_source = "Medication") Then
                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                    ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial

                ElseIf (_source = "LabResults") Then
                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
                    ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial

                ElseIf (_source = "Encounter") Then
                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.EncounterDiagnosis
                    ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientEducation
                End If

                ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary

            End If



            If pnlInfoBrowser.Visible = True Then
                ShowWordDocument()
            End If

            _isOpenforInfobutton = 0




            If _TemplateID > 0 Then
                Try
                    If CheckWordForException() = False Then
                        Exit Sub
                    End If

                    'If pnlInfoBrowser.Visible = True Then
                    '    ShowWordDocument()
                    'End If

                    ''Check Is Present for this patient from table "ExamEducation" 
                    'Dim Ispresent As Boolean = CheckISPresentInExamEducation(m_patientID, _TemplateName, m_VisitID, Source, ResourceCategory, ResourceType)

                    'If Ispresent = False Then
                    '    'Fetch Details form TemplateGallery_Mst
                    '    Dim dt As DataTable = Nothing
                    '    dt = GetEducationMaterialUsingTempalteID(_TemplateID)
                    '    If Not IsNothing(dt) Then
                    '        If dt.Rows.Count > 0 Then
                    '            SetSPENotesAndBiblographyOtherMaterial(dt)
                    '        End If
                    '    End If

                    'End If

                    'If Ispresent = True Then
                    '    'Fetch Details form ExamEducation
                    '    Dim dt As DataTable = Nothing
                    '    dt = GetEducationMaterialUsingTempalteID(m_patientID, _TemplateName, m_VisitID, Source, ResourceCategory, ResourceType)
                    '    If Not IsNothing(dt) Then
                    '        If dt.Rows.Count > 0 Then
                    '            SetSPENotesAndBiblographyOtherMaterial(dt)
                    '        End If
                    '    End If
                    'End If

                    _ISTreeviewOpen = True

                    'Load speNotes in DsoFramer and openEducationTemplate
                    ' OpenEducationTemplate(CType(_TemplateID, Int64), _TemplateName, _speNotes)
                    'pnlcombo.Visible = True

                    ''00000803: Patient Education. Use exam visit id if available.
                    If _exam_VisitID <> 0 Then
                        m_VisitID = _exam_VisitID
                    Else
                        m_VisitID = GenerateVisitID(m_patientID)
                    End If


                    _visID = m_VisitID
                    _patID = m_patientID
                    _tempName = _TemplateName
                    _CurrentTemplateName = _tempName

                    _tmpID = _TemplateID
                    _src = Source
                    _recCat = ResourceCategory
                    _resTyp = ResourceType


                    Dim Ispresent As Boolean = CheckISPresentInExamEducation(m_patientID, _tempName, m_VisitID, Source, ResourceCategory, ResourceType)

                    If Ispresent = True Then
                        _ISGrid = True
                    Else
                        _ISGrid = False
                    End If

                    ShowPreview(ResourceCategory)


                    'Dim frm As frmPatientEducationPreview
                    'frm = New frmPatientEducationPreview()
                    'frm.VISID = _visID
                    ''frm.VisDate = _visDate
                    'frm.PATID = _patID
                    'frm.TempName = _tempName
                    'frm.Sourc = _src
                    'frm.ResourcCat = _recCat
                    'frm.ResourcTyp = _resTyp
                    'frm.ISGRID = _ISGrid
                    'frm.TMPID = _tmpID
                    'frm.ShowDialog()


                    'Select The Row from Grid using EducationID


                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

            End If





        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally


        End Try



    End Sub

    'Private Sub trvTemplate1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If IsNothing(trvtemplate1.SelectedNode) = True Then
    '            Exit Sub
    '        End If

    '        If trvtemplate1.SelectedNode Is trvtemplate1.Nodes(0) Then
    '            Exit Sub
    '        End If
    '        Dim _TemplateID As Int64 = CType(trvtemplate1.SelectedNode, myTreeNode).Key
    '        Dim _TemplateName As String = CType(trvtemplate1.SelectedNode, myTreeNode).Text
    '        Dim _source As String = CType(trvTemplate.SelectedNode, myTreeNode).Tag

    '        'Source
    '        'If (_source = "ProblemList") Then
    '        '    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
    '        'ElseIf (_source = "Medication") Then
    '        '    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
    '        'ElseIf (_source = "LabResults") Then
    '        '    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
    '        'End If

    '        Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.None

    '        'Category
    '        ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary

    '        'Type
    '        ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientEducation

    '        If _TemplateID > 0 Then
    '            '' ADD ROW IN C1 TO ADD TEMPLATE ''
    '            ' Dim _FoundRow As Integer = C1Template.FindRow(trvTemplate.SelectedNode.Text, 0, COL_TEMPLATENAME, False, True, False)
    '            'If _FoundRow < 0 Then
    '            Dim _Row As Integer
    '            'C1Template.Rows.Add()
    '            '_Row = C1Template.Rows.Count - 1

    '            'C1Template.SetData(_Row, COL_TEMPLATENAME, trvTemplate.SelectedNode.Text)
    '            'C1Template.SetData(_Row, COL_ID, _TemplateID)
    '            'C1Template.Row = _Row
    '            'C1Template_DoubleClick(Nothing, Nothing)
    '            Try
    '                'Dim _Row As Integer = C1Template.Row
    '                'If _Row >= 0 Then

    '                '' IF DOUBLE CLICKED TEMLPLATE IS ALREADY OPEN, THEN DON'T REOPEN IT ''
    '                If _CurrentTemplateID = CType(_TemplateID, Int64) Then
    '                    Exit Sub
    '                End If

    '                If CheckWordForException() = False Then
    '                    Exit Sub
    '                End If
    '                '' IF ANY TEMPLATE IS OPEN THEN SAVE IT FIRST IN ARRAYLIST ''
    '                If AddTemplateInArray() Then
    '                Else
    '                    Exit Sub
    '                End If



    '                '' NOW OPEN DOUBLE CLICKED TEMLPATE ''
    '                OpenEducationTemplate(CType(_TemplateID, Int64), _TemplateName)

    '                pnlcombo.Visible = True

    '                'End If
    '                '_Row = Nothing  'Change made to solve memory Leak and word crash issue
    '            Catch ex As Exception
    '                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End Try

    '            _isEducationChanged = True

    '            If hashNewTemplates Is Nothing Then
    '                hashNewTemplates = New HashSet(Of Long)
    '            End If

    '            If Not hashNewTemplates.Contains(_TemplateID) Then
    '                hashNewTemplates.Add(_TemplateID)
    '            End If
    '        End If


    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    Finally

    '        If grdPatienEducation.VisibleRowCount > 0 Then
    '            grdPatienEducation.UnSelect(grdPatienEducation.CurrentRowIndex)
    '        End If

    '    End Try
    'End Sub

    Private Sub FillGrid()
        dvPatientEducationList = oPatientEducation.GetAllEducationByDataTable(m_patientID)
        If (IsNothing(dvPatientEducationList) = False) Then
            grdPatienEducation.Enabled = False
            ' grdPatienEducation.AutoGenerateColumns = False
            BindToGrid(dvPatientEducationList.DefaultView)
        End If
    End Sub

    Private Sub BindToGrid(ByVal BindingDataView As DataView)

        grdPatienEducation.Columns.Clear()
        grdPatienEducation.DataSource = BindingDataView

        grdPatienEducation.Enabled = True

        Dim obutton As DataGridViewButtonColumn

        obutton = New DataGridViewButtonColumn
        obutton.FlatStyle = FlatStyle.Flat
        obutton.Text = "...."
        'obutton.ToolTipText = "Modify Template"
        obutton.UseColumnTextForButtonValue = True

        grdPatienEducation.Columns.Add(obutton)
        'CustomGridStyle()

        CustomGriid()
    End Sub

    'Private Sub FillGeneralTemplate()
    '    Dim RootNode As New myTreeNode("General", -1)
    '    RootNode.ImageIndex = 0
    '    RootNode.SelectedImageIndex = 0
    '    trvtemplate1.Nodes.Add(RootNode)
    '    RootNode = Nothing  'Change made to solve memory Leak and word crash issue



    '    Dim RootNode1 As New myTreeNode("Patient Education", 0)
    '    RootNode1.ImageIndex = 0
    '    RootNode1.SelectedImageIndex = 0
    '    trvtemplate1.Nodes(0).Nodes.Add(RootNode1)
    '    RootNode1 = Nothing  'Change made to solve memory Leak and word crash issue


    '    Dim RootNode2 As New myTreeNode("MU Patient Education", 1)
    '    RootNode2.ImageIndex = 0
    '    RootNode2.SelectedImageIndex = 0
    '    trvtemplate1.Nodes(0).Nodes.Add(RootNode2)
    '    RootNode2 = Nothing

    '    txtSelectedTemplates.ForeColor = Color.Black
    '    txtSelectedTemplates.BackColor = Color.White

    '    Dim dt As DataTable
    '    Dim i As Integer

    '    '' to Get all Templates(ID & Name) of Patient Education
    '    dt = oPatientEducation.FillTemplates

    '    If Not IsNothing(dt) Then
    '        '' Fill Templates in Tree View
    '        For i = 0 To dt.Rows.Count - 1
    '            Dim MyNode As New myTreeNode(dt.Rows(i)("sTemplateName"), dt.Rows(i)("nTemplateID"))
    '            MyNode.ImageIndex = 1
    '            MyNode.SelectedImageIndex = 1
    '            MyNode.Tag = "Other"

    '            If (dt.Rows(i)("sCategoryName") = "Patient Education") Then
    '                trvtemplate1.Nodes(0).Nodes(0).Nodes.Add(MyNode)
    '            Else
    '                trvtemplate1.Nodes(0).Nodes(1).Nodes.Add(MyNode)

    '            End If

    '            'trvTemplate.Nodes(0).Nodes.Add(MyNode)
    '            MyNode = Nothing    'Change made to solve memory Leak and word crash issue
    '        Next
    '        'Change made to solve memory Leak and word crash issue
    '        dt.Dispose()
    '        dt = Nothing
    '    End If



    '    If Me.Text.Contains("Patient Education") Then
    '        ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientEducation
    '    ElseIf Me.Text.Contains("Patient Reference Material") Then
    '        ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
    '    ElseIf Me.Text.Contains("Provider Reference Material") Then
    '        ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.ProviderReferenceMaterial
    '    End If

    '    ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary

    '    trvtemplate1.Sort()
    '    trvtemplate1.ExpandAll()
    'End Sub

    Private Sub BindNodesToTreeView(ByRef BindingList As List(Of TreeNode))

        With trvTemplate
            .BeginUpdate()

            With .Nodes(0)
                .Nodes(0).Nodes.Clear()
                .Nodes(1).Nodes.Clear()
                .Nodes(2).Nodes.Clear()
            End With

            .Nodes(1).Nodes.Clear()

            With .Nodes(0)
                .Nodes(0).Nodes.AddRange(BindingList.Where(Function(p) Convert.ToString(p.Tag = "ProblemList")).ToArray())
                .Nodes(1).Nodes.AddRange(BindingList.Where(Function(p) Convert.ToString(p.Tag = "Medication")).ToArray())
                .Nodes(2).Nodes.AddRange(BindingList.Where(Function(p) Convert.ToString(p.Tag = "LabResults")).ToArray())
            End With

            .Nodes(1).Nodes.AddRange(BindingList.Where(Function(p) Convert.ToString(p.Tag = "Other")).ToArray())
            If .Nodes(1).GetNodeCount(True) > 0 Then
                .Nodes(1).Expand()
                .SelectedNode = .Nodes(1).Nodes(0)
            End If
            .EndUpdate()
        End With
    End Sub

    Private Sub FillPatientEducationTemplate(PatientID As Long, Age As Decimal)

        'Dim RootNode As New myTreeNode("Education`", -1)
        'RootNode.ImageIndex = 0
        'RootNode.SelectedImageIndex = 0
        'trvTemplate.Nodes.Add(RootNode)
        'RootNode = Nothing  'Change made to solve memory Leak and word crash issue

        Dim ParentNode1 As New myTreeNode("Suggested Education Material", 0)
        ParentNode1.ImageIndex = 7
        ParentNode1.SelectedImageIndex = 7
        trvTemplate.Nodes.Add(ParentNode1)
        ParentNode1 = Nothing  'Change made to solve memory Leak and word crash issue

        Dim ParentNode2 As New myTreeNode("Internal Library", 1)
        ParentNode2.ImageIndex = 6
        ParentNode2.SelectedImageIndex = 6
        trvTemplate.Nodes.Add(ParentNode2)
        ParentNode2 = Nothing  'Change made to solve memory Leak and word crash issue

        Dim ChildParent1Node1 As New myTreeNode()
        ChildParent1Node1.Text = "Problem List"
        ChildParent1Node1.ImageIndex = 9
        ChildParent1Node1.SelectedImageIndex = 9
        trvTemplate.Nodes(0).Nodes.Add(ChildParent1Node1)
        ChildParent1Node1 = Nothing  'Change made to solve memory Leak and word crash issue

        Dim ChildParent1Node2 As New myTreeNode()
        ChildParent1Node2.Text = "Medication List"
        ChildParent1Node2.ImageIndex = 8
        ChildParent1Node2.SelectedImageIndex = 8
        trvTemplate.Nodes(0).Nodes.Add(ChildParent1Node2)
        ChildParent1Node2 = Nothing  'Change made to solve memory Leak and word crash issue

        Dim ChildParent1Node3 As New myTreeNode()
        ChildParent1Node3.Text = "Lab Results"
        ChildParent1Node3.ImageIndex = 10
        ChildParent1Node3.SelectedImageIndex = 10
        trvTemplate.Nodes(0).Nodes.Add(ChildParent1Node3)
        ChildParent1Node3 = Nothing  'Change made to solve memory Leak and word crash issue

        'Dim ChildParent2Node1 As New myTreeNode()
        'ChildParent2Node1.Text = "Patient Education"
        'ChildParent2Node1.ImageIndex = 0
        'ChildParent2Node1.SelectedImageIndex = 0
        'trvTemplate.Nodes(1).Nodes.Add(ChildParent2Node1)
        'ChildParent2Node1 = Nothing  'Change made to solve memory Leak and word crash issue

        'Dim ChildParent2Node2 As New myTreeNode()
        'ChildParent2Node2.Text = "MU Patient Education"
        'ChildParent2Node2.ImageIndex = 0
        'ChildParent2Node2.SelectedImageIndex = 0
        'trvTemplate.Nodes(1).Nodes.Add(ChildParent2Node2)
        'ChildParent2Node2 = Nothing  'Change made to solve memory Leak and word crash issue

        Dim dt As DataTable
        Dim i As Integer

        '' to Get all Templates(ID & Name) of Patient Education
        dt = oPatientEducation.FillPatientEducationTemplates(PatientID, Age)

        If Not IsNothing(dt) Then
            '' Fill Templates in Tree View
            For i = 0 To dt.Rows.Count - 1
                Dim MyNode As New myTreeNode(dt.Rows(i)("sTemplateName"), dt.Rows(i)("nTemplateID"))
                MyNode.ImageIndex = 1
                MyNode.SelectedImageIndex = 1
                MyNode.Tag = dt.Rows(i)("sSource")
                lst_Template_Nodes.Add(MyNode)

                'trvTemplate.Nodes(0).Nodes.Add(MyNode)
                MyNode = Nothing    'Change made to solve memory Leak and word crash issue
            Next
            'Change made to solve memory Leak and word crash issue


            dt.Dispose()
            dt = Nothing
        End If

        '  dt = New DataTable
        dt = oPatientEducation.FillTemplates

        If Not IsNothing(dt) Then
            '' Fill Templates in Tree View
            For i = 0 To dt.Rows.Count - 1
                Dim MyNode As New myTreeNode(dt.Rows(i)("sTemplateName"), dt.Rows(i)("nTemplateID"))
                MyNode.ImageIndex = 1
                MyNode.SelectedImageIndex = 1
                MyNode.Tag = "Other"
                'trvTemplate.Nodes(1).Nodes.Add(MyNode)
                lst_Template_Nodes.Add(MyNode)
                'If (dt.Rows(i)("sCategoryName") = "Patient Education") Then
                '    trvTemplate.Nodes(1).Nodes(0).Nodes.Add(MyNode)
                'Else
                '    trvTemplate.Nodes(1).Nodes(1).Nodes.Add(MyNode)

                'End If

                'trvTemplate.Nodes(0).Nodes.Add(MyNode)
                MyNode = Nothing    'Change made to solve memory Leak and word crash issue
            Next
            'Change made to solve memory Leak and word crash issue
            dt.Dispose()
            dt = Nothing

        End If

        'trvTemplate.Sort()
        BindNodesToTreeView(lst_Template_Nodes)
        trvTemplate.ExpandAll()


    End Sub

    'Private Sub ModifyCategory()

    '    If MainMenu.IsAccess(False, m_patientID) = False Then
    '        Exit Sub
    '    End If

    '    '' SUDHIR 20090521 '' CHECK PROVIDER ''
    '    If gblnProviderDisable = True Then
    '        If ShowAssociateProvider(m_patientID, Me) = True Then
    '            CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
    '        End If
    '    End If
    '    '' END SUDHIR

    '    'To check exeception related to word
    '    If CheckWordForException() = False Then
    '        Exit Sub
    '    End If

    '    'dtWord = New DataTable

    '    Dim objWord As New clsWordDocument
    '    Dim dtPtEducation As New DataTable
    '    dtPtEducation = objWord.FillTemplates(enumTemplateFlag.PatientEducation)
    '    If Not IsNothing(dtPtEducation) Then
    '        Dim grdEducationID As Int64
    '        Dim grdDocumentName As String
    '        Dim grdSource As String
    '        Dim grdResourceCategory As String
    '        Dim grdResourcetype As String
    '        Dim grdDocumentUrl As String


    '        If dtPtEducation.Rows.Count = 0 Then
    '            ''''If not present then exit from sub
    '            MessageBox.Show("No Template is associated for Patient Education. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            objWord = Nothing
    '            dtPtEducation = Nothing
    '            Exit Sub
    '        Else
    '            _VisitID = GenerateVisitID(m_patientID) ''gn_VisitID replaced by _VisitID
    '            If grdPatienEducation.Rows.Count > 0 Then
    '                Dim grdIndex As Integer = grdPatienEducation.CurrentCell.RowIndex
    '                grdEducationID = grdPatienEducation.Item(grdIndex, 0).ToString
    '                grdDocumentName = grdPatienEducation.Item(grdIndex, 3).ToString()
    '                grdSource = grdPatienEducation.Item(grdIndex, 4).ToString()
    '                grdResourceCategory = grdPatienEducation.Item(grdIndex, 5).ToString()
    '                grdResourcetype = grdPatienEducation.Item(grdIndex, 6).ToString()
    '                grdDocumentUrl = grdPatienEducation.Item(grdIndex, 7).ToString()
    '                _VisitID = grdPatienEducation.Item(grdIndex, 1).ToString
    '                m_VisitID = _VisitID
    '                _VisitDate = grdPatienEducation.Item(grdIndex, 2).ToString
    '            Else
    '                MessageBox.Show("No Template is associated for Patient Education. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                If Not IsNothing(objWord) Then
    '                    objWord = Nothing
    '                End If
    '                If Not IsNothing(dtPtEducation) Then
    '                    dtPtEducation.Dispose()
    '                    dtPtEducation = Nothing
    '                End If
    '                Return
    '            End If


    '            If grdResourceCategory = "Online Library" Then
    '                ShowInfobutton()

    '                HomeUrl = grdDocumentUrl
    '                EducationID = grdEducationID
    '                InfoButtonWebBrowser.Navigate(grdDocumentUrl)
    '                If Source = 1 Then
    '                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '                ElseIf Source = 2 Then
    '                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '                ElseIf Source = 3 Then
    '                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '                End If
    '                Try
    '                    AddHandler InfoButtonWebBrowser.DocumentCompleted, AddressOf navigation_complete
    '                Catch ex As Exception

    '                End Try

    '            Else

    '                ShowWordDocument()
    '                If grdSource = "Problem List" Then
    '                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
    '                ElseIf grdSource = "Medication" Then
    '                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
    '                ElseIf grdSource = "Orders" Then
    '                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
    '                ElseIf grdSource = "" Then
    '                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.None
    '                End If
    '                _isOpenforInfobutton = 0
    '                FillTemplates()
    '                ShowDocument(grdEducationID, grdDocumentName)

    '            End If

    '            If grdResourceCategory = "Online Library" Then



    '            Else


    '                ''Line commented and modified by dipak for case UC5070.003 replace gnPatientID by local variable
    '                ''Dim ofrmPatientEducation As New frmPatientEducation(False, grdEducationID, grdDocumentName, _VisitID, gnPatientID, 0)
    '                'Dim src As Integer = 0
    '                'If grdSource = "Problem List" Then
    '                '    src = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
    '                'ElseIf grdSource = "Medication" Then
    '                '    src = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
    '                'ElseIf grdSource = "Orders" Then
    '                '    src = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
    '                'ElseIf grdSource = "" Then
    '                '    src = gloEMRGeneralLibrary.clsInfobutton.enumSource.None
    '                'End If


    '                'Dim ofrmPatientEducation As New frmPatientEducation(False, grdEducationID, grdDocumentName, m_VisitID, m_patientID, 0, src)
    '                ''end modification by dipak
    '                'AddHandler ofrmPatientEducation.FormClosed, AddressOf On_PatientEducation_Closed
    '                'Try
    '                '    If grdResourcetype = "Patient Education" Then
    '                '        ofrmPatientEducation.Text = "Modify Patient Education"
    '                '    ElseIf grdResourcetype = "Patient Reference Material" Then
    '                '        ofrmPatientEducation.Text = "Modify Patient Reference Material"
    '                '    ElseIf grdResourcetype = "Provider Reference Material" Then
    '                '        ofrmPatientEducation.Text = "Modify Provider Reference Material"
    '                '    End If

    '                '    'ofrmPatientEducation.Text = "Modify Patient Education"
    '                '    ' .MdiParent = Me.ParentForm
    '                '    '.MyCaller = Me
    '                '    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '                '    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
    '                '    ofrmPatientEducation.MdiParent = Me.ParentForm
    '                '    'Me.Hide()
    '                '    'ofrmPatientEducation.BringToFront()
    '                '    ofrmPatientEducation.Show()
    '                '    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '                '    ofrmPatientEducation.WindowState = FormWindowState.Maximized


    '                '    'SHUBHANGI 20110331
    '                '    Dim i As Integer
    '                '    For i = 0 To CType(grdPatienEducation.DataSource, DataView).Table.Rows.Count - 1
    '                '        '''' when ID Found select that matching Row
    '                '        If grdEducationID = grdPatienEducation.Item(i, 0) Then
    '                '            grdPatienEducation.CurrentRowIndex = i
    '                '            grdPatienEducation.Select(i)
    '                '            Exit For
    '                '        End If
    '                '    Next

    '                'Catch ex As Exception
    '                '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                '    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '                '    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
    '                '    If Not IsNothing(dtPtEducation) Then
    '                '        ofrmPatientEducation.Dispose()
    '                '        ofrmPatientEducation = Nothing
    '                '    End If
    '                'Finally

    '                'End Try
    '            End If

    '        End If '' Checking for the nothing
    '    End If
    'End Sub

    Private Sub ShowSingleClick(ByVal grdindex As Int16)

        ' Cursor.Current = Cursors.WaitCursor
        Dim dt As DataTable = Nothing
        Try

            'If _ISDoubleClick = True Then
            '    Exit Sub
            'End If

            _patID = m_patientID

            grdPatienEducation.Rows(grdindex).Selected = True

            If MainMenu.IsAccess(False, m_patientID) = False Then
                Exit Sub
            End If

            '' SUDHIR 20090521 '' CHECK PROVIDER ''
            If gblnProviderDisable = True Then
                If ShowAssociateProvider(m_patientID, Me) = True Then
                    CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                End If
            End If
            '' END SUDHIR

            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If

            'check for same Template id

            'If IsNothing(grdPatienEducation.CurrentCell) Then
            '    If grdPatienEducation.Rows.Count > 0 Then
            '        Dim i As Integer
            '        For i = 0 To grdPatienEducation.Rows.Count - 1
            '            If grdPatienEducation.Rows(i).Selected = True Then
            '                grdindexx = i
            '                Exit For
            '            End If
            '        Next
            '    End If
            'Else
            '    grdindexx = grdPatienEducation.CurrentCell.RowIndex
            'End If


            'If _CurrentTemplateID = Convert.ToInt64(grdPatienEducation.Item(0, grdindex).Value) Then
            '    Exit Sub
            'End If

            Dim grdSource As String
            Dim grdResourceCategory As String
            Dim grdResourcetype As String


            If m_patientID = 0 Then
                _VisitID = GenerateVisitID(m_patientID) ''gn_VisitID replaced by _VisitID

            End If

            EduID = Convert.ToInt64(grdPatienEducation.Item("nEducationID", grdindex).Value)
            _CurrentTemplateName = grdPatienEducation.Item("sTemplateName", grdindex).Value.ToString()
            _tempName = _CurrentTemplateName
            grdSource = grdPatienEducation.Item("Source", grdindex).Value.ToString()
            grdResourceCategory = grdPatienEducation.Item("Resource_Category", grdindex).Value.ToString()
            grdResourcetype = grdPatienEducation.Item("Resource_Type", grdindex).Value.ToString()
            _docURL = grdPatienEducation.Item("sDocumentURL", grdindex).Value.ToString()


            Source = Convert.ToInt16(grdPatienEducation.Item("Src", grdindex).Value)
            ResourceCategory = Convert.ToInt16(grdPatienEducation.Item("ResCat", grdindex).Value)
            ResourceType = Convert.ToInt16(grdPatienEducation.Item("ResType", grdindex).Value)
            '_EduID = Convert.ToInt64(grdPatienEducation.Item(1, grdindex).Value)
            '_CurrentTemplateName = grdPatienEducation.Item(3, grdindex).Value.ToString()
            '_tempName = _CurrentTemplateName
            'grdSource = grdPatienEducation.Item(4, grdindex).Value.ToString()
            'grdResourceCategory = grdPatienEducation.Item(5, grdindex).Value.ToString()
            'grdResourcetype = grdPatienEducation.Item(6, grdindex).Value.ToString()
            '_docURL = grdPatienEducation.Item(7, grdindex).Value.ToString()

            _ISGrid = True

            'Source = Convert.ToInt16(grdPatienEducation.Item(8, grdindex).Value)
            'ResourceCategory = Convert.ToInt16(grdPatienEducation.Item(9, grdindex).Value)
            'ResourceType = Convert.ToInt16(grdPatienEducation.Item(10, grdindex).Value)


            ' Get other details from education id

            dt = GetEducationMaterialUsingEducationID(_EduID)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    SetSPENotesAndBiblographyOtherMaterial(dt)
                    TempExamID = Convert.ToInt64(dt.Rows(0)("examID"))
                End If
            End If


            _visID = Convert.ToInt64(grdPatienEducation.Item("nVisitID", grdindex).Value)
            m_VisitID = _visID

            _VisitDate = grdPatienEducation.Item("VisitDate", grdindex).Value.ToString

            _src = Source
            _recCat = ResourceCategory
            _resTyp = ResourceType

            _tmpID = _EduID

            If grdResourceCategory = "Online Library" Then

                If pnlWord.Visible = True Then
                    ShowInfobutton()
                End If

                HomeUrl = _docURL
                EducationID = _EduID
                InfoButtonWebBrowser.Navigate(_docURL)
                If Source = 1 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf Source = 2 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf Source = 3 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
                Try
                    AddHandler InfoButtonWebBrowser.DocumentCompleted, AddressOf navigation_complete
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
            Else

                If pnlInfoBrowser.Visible = True Then
                    ShowWordDocument()
                End If
                If grdSource = "Problem List" Then
                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                ElseIf grdSource = "Medication" Then
                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                ElseIf grdSource = "Orders" Then
                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
                ElseIf grdSource = "" Then
                    Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.None
                End If
                _isOpenforInfobutton = 0

                OpenEducationTemplate(_EduID, _tempName, _speNotes)

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            '   Cursor.Current = Cursors.Default
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub


    Private Sub ShowDoubleClick()


        Cursor.Current = Cursors.WaitCursor
        Try

            _patID = m_patientID

            If MainMenu.IsAccess(False, m_patientID) = False Then
                Exit Sub
            End If

            '' SUDHIR 20090521 '' CHECK PROVIDER ''
            If gblnProviderDisable = True Then
                If ShowAssociateProvider(m_patientID, Me) = True Then
                    CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                End If
            End If
            '' END SUDHIR

            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If


            If ResourceCategory = 2 Then

                If pnlWord.Visible = True Then
                    ShowInfobutton()
                End If

                ShowPreview(ResourceCategory)

                'If Source = 1 Then
                '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                'ElseIf Source = 2 Then
                '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                'ElseIf Source = 3 Then
                '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                'End If
                'Try
                'AddHandler InfoButtonWebBrowser.DocumentCompleted, AddressOf navigation_complete
                'Catch ex As Exception

                'End Try
            Else

                If pnlInfoBrowser.Visible = True Then
                    ShowWordDocument()
                End If

                ShowPreview(ResourceCategory)




            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub On_InfoButton_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim frm As gloEMRGeneralLibrary.frmInfoButtonBrowser = Nothing

        Try
            frm = DirectCast(sender, gloEMRGeneralLibrary.frmInfoButtonBrowser)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf On_InfoButton_Closed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try
        Try
            RefreshCategory()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ModifyCategorys()

        Cursor.Current = Cursors.WaitCursor
        Dim dt As DataTable = Nothing
        Try

            If MainMenu.IsAccess(False, m_patientID) = False Then
                Exit Sub
            End If

            '' SUDHIR 20090521 '' CHECK PROVIDER ''
            If gblnProviderDisable = True Then
                If ShowAssociateProvider(m_patientID, Me) = True Then
                    CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                End If
            End If
            '' END SUDHIR

            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If

            'check for same Template id
            Dim grdindexx As Int16
            If IsNothing(grdPatienEducation.CurrentCell) Then
                If grdPatienEducation.Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To grdPatienEducation.Rows.Count - 1
                        If grdPatienEducation.Rows(i).Selected = True Then
                            grdindexx = i
                            Exit For
                        End If
                    Next
                End If
            Else
                grdindexx = grdPatienEducation.CurrentCell.RowIndex
            End If


            If _CurrentTemplateID = Convert.ToInt64(grdPatienEducation.Item(0, grdindexx).Value) Then
                Exit Sub
            End If

            Dim result As Int16

            'oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, m_ExamID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)

            Dim grdEducationID As Int64
            '  Dim grdDocumentName As String
            Dim grdSource As String
            Dim grdResourceCategory As String
            Dim grdResourcetype As String
            Dim grdDocumentUrl As String

            If _ISTreeviewOpen = True Then

                If ResourceCategory = 1 Then

                    result = MessageBox.Show("Do you want to save the changes to Template?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    wdPatientEducation.Focus()

                    If result = Windows.Forms.DialogResult.Cancel Then
                        Exit Sub
                    End If

                    If (result = Windows.Forms.DialogResult.Yes) Then
                        If (IsNothing(oCurDoc) = False) Then
                            oCurDoc.SaveAs(_DocumentName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                        Else
                            wdPatientEducation.Save(_DocumentName, True, "", "")
                        End If
                        'If Not IsSaving And Not isClosed Then
                        '    GenerateAuditLogForSave()
                        'End If

                        _isEducationChanged = False

                        oCurDoc = Nothing
                        wdPatientEducation.Close()

                        oWord = New clsWordDocument
                        _speNotes = CType(oWord.ConvertFiletoBinary(_DocumentName), Object)
                        oWord = Nothing

                        oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, TempExamID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)
                        FillGrid()
                        ' _ISTreeviewOpen = False
                        ' ModifyCategorys()



                    ElseIf (result = Windows.Forms.DialogResult.No) Then
                        _isEducationChanged = True

                    End If
                    result = Nothing    'Change made to solve memory Leak and word crash issue
                    _ISTreeviewOpen = False

                End If

            End If


            If _FromGrid = True Then

                If ResourceCategory = 1 Then


                    result = MessageBox.Show("Do you want to save the changes to Template?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    wdPatientEducation.Focus()

                    If result = Windows.Forms.DialogResult.Cancel Then
                        Exit Sub
                    End If

                    If (result = Windows.Forms.DialogResult.Yes) Then
                        If (IsNothing(oCurDoc) = False) Then
                            oCurDoc.SaveAs(_DocumentName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                        Else
                            wdPatientEducation.Save(_DocumentName, True, "", "")
                        End If
                        'If Not IsSaving And Not isClosed Then
                        '    GenerateAuditLogForSave()
                        'End If

                        _isEducationChanged = False

                        oCurDoc = Nothing
                        wdPatientEducation.Close()

                        oWord = New clsWordDocument
                        _speNotes = CType(oWord.ConvertFiletoBinary(_DocumentName), Object)
                        oWord = Nothing

                        oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, TempExamID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)
                        FillGrid()
                        ' _FromGrid = False
                        'ModifyCategorys()



                    ElseIf (result = Windows.Forms.DialogResult.No) Then
                        _isEducationChanged = True

                    End If

                    result = Nothing    'Change made to solve memory Leak and word crash issue

                    _FromGrid = False
                End If

                If ResourceCategory = 2 Then

                    If result = Windows.Forms.DialogResult.Cancel Then
                        Exit Sub
                    End If

                    If (result = Windows.Forms.DialogResult.Yes) Then

                        'If Not IsSaving And Not isClosed Then
                        '    GenerateAuditLogForSave()
                        'End If


                        If DocumentCompleted Then
                            'Save Link to Patient Education Table
                            'If Not isViewed Then
                            Dim oInfo As New gloEMRGeneralLibrary.clsInfobutton()

                            Dim oDocUrl As String
                            If DocumentTitle = "" Then
                                DocumentTitle = InfoButtonWebBrowser.DocumentTitle
                            End If

                            If Not IsNothing(InfoButtonWebBrowser.Document) Then
                                oDocUrl = InfoButtonWebBrowser.Document.Url.ToString()
                            Else
                                oDocUrl = InfoButtonWebBrowser.Url.OriginalString()
                            End If

                            oInfo.SavePatientEducation(m_VisitID, m_patientID, 0, Nothing, DocumentTitle, Source, ResourceCategory, ResourceType, oDocUrl, EducationID, gnLoginProviderID)
                            oInfo = Nothing
                            _FromGrid = False
                            FillGrid()

                        End If
                    End If
                End If
            End If

            If grdPatienEducation.Rows.Count > 0 Then
                Dim grdIndex As Integer

                If m_patientID = 0 Then
                    _VisitID = GenerateVisitID(m_patientID) ''gn_VisitID replaced by _VisitID
                End If
                If IsNothing(grdPatienEducation.CurrentCell) Then
                    If grdPatienEducation.Rows.Count > 0 Then
                        Dim i As Integer
                        For i = 0 To grdPatienEducation.Rows.Count - 1
                            If grdPatienEducation.Rows(i).Selected = True Then
                                grdIndex = i
                                Exit For
                            End If
                        Next
                    End If
                Else
                    grdIndex = grdPatienEducation.CurrentCell.RowIndex

                End If
                ' Dim grdIndex As Integer = grdPatienEducation.CurrentCell.RowIndex
                grdEducationID = Convert.ToInt64(grdPatienEducation.Item(0, grdIndex).Value)
                _CurrentTemplateName = grdPatienEducation.Item(3, grdIndex).Value.ToString()
                grdSource = grdPatienEducation.Item(4, grdIndex).Value.ToString()
                grdResourceCategory = grdPatienEducation.Item(5, grdIndex).Value.ToString()
                grdResourcetype = grdPatienEducation.Item(6, grdIndex).Value.ToString()
                grdDocumentUrl = grdPatienEducation.Item(7, grdIndex).Value.ToString()

                Source = Convert.ToInt16(grdPatienEducation.Item(8, grdIndex).Value)
                ResourceCategory = Convert.ToInt16(grdPatienEducation.Item(9, grdIndex).Value)
                ResourceType = Convert.ToInt16(grdPatienEducation.Item(10, grdIndex).Value)


                ' Get other details from education id

                dt = GetEducationMaterialUsingEducationID(grdEducationID)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        SetSPENotesAndBiblographyOtherMaterial(dt)
                        TempExamID = Convert.ToInt64(dt.Rows(0)("examID"))
                    End If
                End If


                _VisitID = Convert.ToInt64(grdPatienEducation.Item(1, grdIndex).Value)
                m_VisitID = _VisitID
                _VisitDate = grdPatienEducation.Item(2, grdIndex).Value.ToString

                _FromGrid = True

                If grdResourceCategory = "Online Library" Then

                    If pnlWord.Visible = True Then
                        ShowInfobutton()
                    End If

                    HomeUrl = grdDocumentUrl
                    EducationID = grdEducationID
                    InfoButtonWebBrowser.Navigate(grdDocumentUrl)
                    If Source = 1 Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ElseIf Source = 2 Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ElseIf Source = 3 Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                    Try
                        AddHandler InfoButtonWebBrowser.DocumentCompleted, AddressOf navigation_complete
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                Else

                    If pnlInfoBrowser.Visible = True Then
                        ShowWordDocument()
                    End If
                    If grdSource = "Problem List" Then
                        Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                    ElseIf grdSource = "Medication" Then
                        Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                    ElseIf grdSource = "Orders" Then
                        Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
                    ElseIf grdSource = "" Then
                        Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.None
                    End If
                    _isOpenforInfobutton = 0

                    OpenEducationTemplate(grdEducationID, _CurrentTemplateName, _speNotes)
                    'ShowDocument(grdEducationID, grdDocumentName)

                End If



            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            Cursor.Current = Cursors.Default
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub

    Public DocumentTitle As String = ""
    Public DocumentCompleted As Boolean = False

    Private Sub navigation_complete(ByVal sender As System.Object, _
             ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs)



        DocumentCompleted = True

        If Not IsNothing(InfoButtonWebBrowser.Document) Then
            DocumentTitle = Convert.ToString(InfoButtonWebBrowser.Document.Title)
        End If

        If IsNothing(DocumentTitle) OrElse DocumentTitle = "" Then
            DocumentTitle = Convert.ToString(InfoButtonWebBrowser.DocumentTitle)
        End If
        If DocumentTitle = "Health Information for You: MedlinePlus Connect" Or DocumentTitle = "Información de salud para usted: MedlinePlus Connect" Then
            Try
                For Each element As HtmlElement In InfoButtonWebBrowser.Document.All
                    Dim HeaderElement() As String
                    If element.TagName().ToString.ToUpper() = "H2" Then
                        HeaderElement = element.InnerText.ToString().Split("[")
                        If HeaderElement.Length > 0 Then
                            DocumentTitle = HeaderElement(0)
                        End If
                    End If
                    HeaderElement = Nothing
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
        End If



    End Sub

    Private Sub grdPatienEducation_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPatienEducation.CellContentClick



    End Sub

    'Private Sub grdPatienEducation_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPatienEducation.CellClick
    '    Try
    '        If CheckWordForException() = False Then
    '            Exit Sub
    '        End If

    '        If (e.RowIndex >= 0) Then
    '            ShowSingleClick(e.RowIndex)
    '            grdPatienEducation.Rows(e.RowIndex).Selected = True

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    'Private Sub grdPatienEducation_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPatienEducation.CellDoubleClick
    '    'To check exeception related to word



    '    'If CheckWordForException() = False Then
    '    '    Exit Sub
    '    'End If

    '    'If (e.RowIndex >= 0) Then
    '    '    ShowDoubleClick()

    '    'End If


    'End Sub

    Private Sub grdPatienEducation_CellMouseDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdPatienEducation.CellMouseDoubleClick
        'If CheckWordForException() = False Then
        '    Exit Sub
        'End If

        'If (e.RowIndex >= 0) Then
        '    '_ISDoubleClick = True
        '    ShowDoubleClick()

        'End If


    End Sub



    'Private Sub grdPatienEducation_CellMouseDown(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdPatienEducation.CellMouseDown
    '    'If CheckWordForException() = False Then
    '    '    Exit Sub
    '    'End If

    '    'If (e.RowIndex >= 0) Then
    '    '    ShowSingleClick(e.RowIndex)
    '    'End If
    '    'If e.Clicks = 2 Then
    '    '    If (e.RowIndex >= 0) Then
    '    '        ShowDoubleClick()

    '    '    End If
    '    'End If
    'End Sub





    'Private Sub grdPatienEducation_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles grdPatienEducation.MouseUp
    '    'Try
    '    '    Dim ptPoint As Point = New Point(e.X, e.Y)

    '    '    'grdPatienEducation.Select(htInfo.Row)
    '    '    'grdPatienEducation.CurrentCell.RowIndex = htInfo.Row

    '    '    Dim htInfo As DataGridView.HitTestInfo = grdPatienEducation.HitTest(e.X, e.Y)
    '    '    If htInfo.Type = DataGridViewHitTestType.Cell Then
    '    '        grdPatienEducation.Rows(htInfo.RowIndex).Selected = True
    '    '        ''grdPatienEducation.CurrentCell.RowIndex = htInfo.Row

    '    '    Else
    '    '        Exit Sub
    '    '    End If
    '    'Catch ex As Exception
    '    '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    'End Try
    'End Sub

    Private Sub tls_gloCommunityDashboard_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_gloCommunityDashboard.ItemClicked

        If e.ClickedItem.Tag = "Home" Then
            InfoButtonWebBrowser.Navigate(HomeUrl)
        ElseIf e.ClickedItem.Tag = "Refresh" Then
            InfoButtonWebBrowser.Navigate(BrowserLink)
        ElseIf e.ClickedItem.Tag = "Next" Then
            InfoButtonWebBrowser.GoForward()
        ElseIf e.ClickedItem.Tag = "Previous" Then
            InfoButtonWebBrowser.GoBack()
        ElseIf e.ClickedItem.Tag = "Print" Then
            'Print Html Document
            If DocumentCompleted Then
                Me.Cursor = Cursors.WaitCursor
                If gblnUseDefaultPrinter Then
                    InfoButtonWebBrowser.Print()
                Else
                    InfoButtonWebBrowser.ShowPrintDialog()
                End If
                Me.Cursor = Cursors.Default
                If Source = 1 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document Print", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf Source = 2 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document Print", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf Source = 3 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document Print", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        ElseIf e.ClickedItem.Tag = "Save&Close" Then
            If DocumentCompleted Then
                'Save Link to Patient Education Table
                'If Not isViewed Then
                Dim oInfo As New gloEMRGeneralLibrary.clsInfobutton()

                Dim oDocUrl As String
                If DocumentTitle = "" Then
                    DocumentTitle = InfoButtonWebBrowser.DocumentTitle
                End If

                If Not IsNothing(InfoButtonWebBrowser.Document) Then
                    oDocUrl = InfoButtonWebBrowser.Document.Url.ToString()
                Else
                    oDocUrl = InfoButtonWebBrowser.Url.OriginalString()
                End If

                oInfo.SavePatientEducation(m_VisitID, m_patientID, 0, Nothing, DocumentTitle, Source, ResourceCategory, ResourceType, oDocUrl, EducationID, gnLoginProviderID)
                oInfo = Nothing
                'End If
                Me.Close()
            End If
        ElseIf e.ClickedItem.Tag = "Close" Then
            Me.Close()
        End If



    End Sub

    Private Sub InfoButtonWebBrowser_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles InfoButtonWebBrowser.Navigated
        BrowserLink = InfoButtonWebBrowser.Url.ToString
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Modify"
                If grdPatienEducation.Rows.Count > 0 Then
                    If grdPatienEducation.SelectedRows.Count <= 0 Then
                        MessageBox.Show("Select record to Modify", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    ''00000803: Patient Education. Changes to modify date of old educations if required
                    _IsModify = True
                    Int64.TryParse(grdPatienEducation.SelectedRows.Item(0).Cells("nEducationID").Value.ToString(), nEducationID)
                    ShowDoubleClick()
                    _IsModify = False
                    nEducationID = 0
                End If
            Case "Delete"
                Call DeleteCategory()
            Case "Refresh"
                Call RefreshCategory()
            Case "Close"
                Me.Close()

        End Select
    End Sub


    Private Sub DeleteCategory()
        Try
            Dim grdindex As Integer
            If grdPatienEducation.Rows.Count >= 1 Then

                If MainMenu.IsAccess(False, m_patientID) = False Then
                    Exit Sub
                End If

                If grdPatienEducation.SelectedRows.Count <= 0 Then
                    MessageBox.Show("Select record to delete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                Else
                    For grdindex = 0 To grdPatienEducation.Rows.Count - 1
                        '''' when ID Found select that matching Row
                        If grdPatienEducation.Rows(grdindex).Selected = True Then
                            Exit For
                        End If
                    Next

                End If


                If MessageBox.Show("Are you sure you want to delete this Record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim ID As Long
                    'Dim VisitDate As String

                    ID = Convert.ToInt64(grdPatienEducation.Item("nEducationID", grdindex).Value)
                    'VisitDate = CType(grdPatienEducation.Item(3, grdPatienEducation.CurrentCell.RowIndex).Value, String)
                    oPatientEducation.DeleteEducations(ID, "", m_patientID)
                    grdPatienEducation.Enabled = False

                    If dvPatientEducationList IsNot Nothing Then
                        dvPatientEducationList.Rows.Clear()
                    End If

                    dvPatientEducationList = oPatientEducation.GetAllEducations(m_patientID).ToTable()
                    grdPatienEducation.DataSource = dvPatientEducationList.AsDataView()
                    grdPatienEducation.Enabled = True
                    If grdPatienEducation.Rows.Count > 0 Then
                        ShowSingleClick(0)
                    Else
                        wdPatientEducation.Close()
                    End If

                    'SortOrder = CType(grdPatienEducation.DataSource, DataView).Sort
                    'strSearchstring = txtSearch.Text.Trim
                    'arrcolumnsort = Split(SortOrder, "]")
                    'If arrcolumnsort.Length > 1 Then
                    '    strcolumnName = arrcolumnsort.GetValue(0)
                    '    strsortorder = arrcolumnsort.GetValue(1)
                    'End If

                    ' CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                    'CustomGridStyle("", "", "")

                End If
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RefreshCategory()

        txtGridSearch.Text = ""
        FillGrid()
        If grdPatienEducation.Rows.Count > 0 Then
            Dim erg As New DataGridViewCellEventArgs(0, 0)
            grdPatienEducation.Rows(0).Selected = True
            ShowSingleClick(0)
        End If

        'Try
        '    Me.Cursor = Cursors.WaitCursor
        '    Call RefreshEducation()
        '    If grdPatienEducation.VisibleRowCount > 0 Then
        '        grdPatienEducation.CurrentRowIndex = 0
        '        grdPatienEducation.Select(0)
        '    End If
        '    _blnSearch = True
        '    Me.Cursor = Cursors.Default
        'Catch ex As Exception
        '    'MessageBox.Show(ex.Message, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Me.Cursor = Cursors.Default
        'End Try
    End Sub

    Private Function CheckISPresentInExamEducation(PatientID As Long, TemplateName As String, VisitID As Long, Source As Int16, ResourceCategory As Int16, ResourceType As Int16) As Boolean
        Return oPatientEducation.CheckISPresentInExamEducation(PatientID, TemplateName, VisitID, Source, ResourceCategory, ResourceType)
    End Function

    Private Function GetEducationMaterialUsingTempalteID(TemplateID As Long) As DataTable
        Dim objdt As DataTable = Nothing
        Try
            objdt = oPatientEducation.GetEducationMaterialUsingTempalteID(TemplateID)
            Return objdt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return objdt
        Finally
            'If objdt IsNot Nothing Then
            '    objdt.Dispose()
            '    objdt = Nothing
            'End If

        End Try


    End Function

    Private Function GetEducationMaterialUsingEducationID(EducationID As Long) As DataTable
        Dim objdt As DataTable = Nothing
        Try
            objdt = oPatientEducation.GetEducationMaterialUsingEducationID(EducationID)
            Return objdt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return objdt
        Finally
            'If objdt IsNot Nothing Then
            '    objdt.Dispose()
            '    objdt = Nothing
            'End If
        End Try
    End Function

    Private Function GetEducationMaterialUsingTempalteID(PatientID As Long, TemplateName As String, VisitID As Long, Source As Integer, ResourceCategory As Integer, ResourceType As Integer) As DataTable
        Return oPatientEducation.GetEducationMaterialUsingTempalteID(PatientID, TemplateName, VisitID, Source, ResourceCategory, ResourceType)
    End Function

    Private Sub SetSPENotesAndBiblographyOtherMaterial(dtTemplate As DataTable)

        Dim strFileName As String
        strFileName = ExamNewDocumentName
        'strFileName = oWord.GenerateFile(CType(dtTemplate.Rows(0)("sDescription"), Object), strFileName)

        _speNotes = CType(dtTemplate.Rows(0)("sDescription"), Object)

        txtBibliography.Text = Convert.ToString(dtTemplate.Rows(0)("bibliography"))
        txtBibliographyDeveloper.Text = Convert.ToString(dtTemplate.Rows(0)("sbDeveloper"))

        strBibliography = Convert.ToString(dtTemplate.Rows(0)("bibliography"))
        strBibliographyDeveloper = Convert.ToString(dtTemplate.Rows(0)("sbDeveloper"))

        Dim EduID As Long = Convert.ToInt64(dtTemplate.Rows(0)("eduID"))

        TempExamID = Convert.ToInt64(dtTemplate.Rows(0)("examID"))


        If EduID > 0 Then
            grdPatienEducation.ClearSelection()

            If grdPatienEducation.Rows.Count > 0 Then
                Dim i As Integer
                'For i = 0 To grdPatienEducation.Rows.Count - 1
                '    grdPatienEducation.Rows(i).Selected = False
                'Next


                'grdPatienEducation.CurrentCell = Nothing

                For i = 0 To grdPatienEducation.Rows.Count - 1
                    '''' when ID Found select that matching Row
                    If EduID = grdPatienEducation.Rows(i).Cells(0).Value Then
                        'grdPatienEducation.CurrentCell.RowIndex = i
                        grdPatienEducation.Rows(i).Selected = True
                        Exit For
                    End If
                Next
            End If
        End If

        'If dtTemplate IsNot Nothing Then
        '    dtTemplate.Dispose()
        '    dtTemplate = Nothing
        'End If

    End Sub

    Private Sub SetEducationIDForGrid(dtTemplate As DataTable)

        Dim EduID As Long = Convert.ToInt64(dtTemplate.Rows(0)("eduID"))
        TempExamID = Convert.ToInt64(dtTemplate.Rows(0)("examID"))

        'grdPatienEducation.ClearSelection()

        If EduID > 0 Then
            grdPatienEducation.ClearSelection()
            EducationID = EduID
            If grdPatienEducation.Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To grdPatienEducation.Rows.Count - 1
                    '''' when ID Found select that matching Row
                    If EduID = grdPatienEducation.Rows(i).Cells(0).Value Then
                        'grdPatienEducation.CurrentCell.RowIndex = i
                        grdPatienEducation.Rows(i).Selected = True
                        'Dim erg As New DataGridViewCellEventArgs(0, i)
                        'grdPatienEducation_CellClick(Nothing, erg)
                        ShowSingleClick(i)
                        _FromGrid = True
                        Exit For
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub SetEducationIDForGridReadonly(dtTemplate As DataTable)

        Dim EduID As Long = Convert.ToInt64(dtTemplate.Rows(0)("eduID"))
        TempExamID = Convert.ToInt64(dtTemplate.Rows(0)("examID"))

        grdPatienEducation.ClearSelection()

        If EduID > 0 Then
            EducationID = EduID
            If grdPatienEducation.Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To grdPatienEducation.Rows.Count - 1
                    '''' when ID Found select that matching Row
                    If EduID = grdPatienEducation.Rows(i).Cells(0).Value Then
                        'grdPatienEducation.CurrentCell.RowIndex = i
                        grdPatienEducation.Rows(i).Selected = True
                        'Dim erg As New DataGridViewCellEventArgs(0, i)
                        'grdPatienEducation_CellClick(Nothing, erg)
                        'ShowSingleClick(i)
                        _FromGrid = True
                        Exit For
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub SetGrid()

        Dim Ispresent As Boolean = CheckISPresentInExamEducation(m_patientID, _CurrentTemplateName, m_VisitID, Source, ResourceCategory, ResourceType)
        Dim dt As DataTable = Nothing
        If Ispresent = True Then

            dt = GetEducationMaterialUsingTempalteID(m_patientID, _CurrentTemplateName, m_VisitID, Source, ResourceCategory, ResourceType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    SetEducationIDForGridReadonly(dt)
                End If
            End If
        End If
        If Not IsNothing(dt) Then
            dt.Dispose()
            dt = Nothing
        End If
        ''open from grid
        'If _FromGrid = True Then
        '    If EducationID > 0 Then
        '        If grdPatienEducation.Rows.Count > 0 Then
        '            Dim i As Integer

        '            grdPatienEducation.ClearSelection()


        '            'For i = 0 To grdPatienEducation.Rows.Count - 1
        '            '    grdPatienEducation.Rows(i).Selected = False
        '            'Next


        '            For i = 0 To grdPatienEducation.Rows.Count - 1
        '                '''' when ID Found select that matching Row
        '                If EducationID = grdPatienEducation.Rows(i).Cells(0).Value Then
        '                    'grdPatienEducation.CurrentCell.RowIndex = i
        '                    grdPatienEducation.Rows(i).Selected = True
        '                    Exit For
        '                End If
        '            Next
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub CustomGriid()


        Dim dv As DataView
        dv = oPatientEducation.DsDataview




        grdPatienEducation.Columns(0).HeaderText = "Education ID"
        grdPatienEducation.Columns(1).HeaderText = "VisitID"
        grdPatienEducation.Columns(2).HeaderText = "Visit Date"
        grdPatienEducation.Columns(3).HeaderText = "Educations"
        grdPatienEducation.Columns(4).HeaderText = "Source"
        grdPatienEducation.Columns(5).HeaderText = "Resource Category"
        grdPatienEducation.Columns(6).HeaderText = "Resource Type"
        grdPatienEducation.Columns(7).HeaderText = "Document URL"
        grdPatienEducation.Columns(8).HeaderText = "nResource"
        grdPatienEducation.Columns(9).HeaderText = "nResCat"
        grdPatienEducation.Columns(10).HeaderText = "nResType"
        grdPatienEducation.Columns(11).HeaderText = ""


        grdPatienEducation.Columns(0).Visible = False
        grdPatienEducation.Columns(1).Visible = False
        grdPatienEducation.Columns(2).Visible = True
        grdPatienEducation.Columns(3).Visible = True
        grdPatienEducation.Columns(4).Visible = True
        grdPatienEducation.Columns(5).Visible = False
        grdPatienEducation.Columns(6).Visible = False
        grdPatienEducation.Columns(7).Visible = False
        grdPatienEducation.Columns(8).Visible = False
        grdPatienEducation.Columns(9).Visible = False
        grdPatienEducation.Columns(10).Visible = False
        grdPatienEducation.Columns(11).Visible = True

        grdPatienEducation.Columns(2).ReadOnly = True
        grdPatienEducation.Columns(3).ReadOnly = True
        grdPatienEducation.Columns(4).ReadOnly = True


        grdPatienEducation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        grdPatienEducation.Columns(0).Width = 0
        grdPatienEducation.Columns(1).Width = 0
        grdPatienEducation.Columns(2).Width = 0.2 * _GridWidth
        grdPatienEducation.Columns(3).Width = 0.75 * _GridWidth
        grdPatienEducation.Columns(4).Width = 0.25 * _GridWidth
        grdPatienEducation.Columns(5).Width = 0
        grdPatienEducation.Columns(6).Width = 0
        grdPatienEducation.Columns(7).Width = 0
        grdPatienEducation.Columns(8).Width = 0
        grdPatienEducation.Columns(9).Width = 0
        grdPatienEducation.Columns(10).Width = 0
        grdPatienEducation.Columns(11).Width = 30



    End Sub

    Private Sub ShowInfobutton()
        pnlWord.Visible = False

        PnlToolStrip.Visible = True

        pnlInfoBrowser.Visible = True
        'pnlBrowserToolStrip.Visible = True
        'pnlBrowserToolStrip.SendToBack()
    End Sub

    Private Sub ShowWordDocument()
        pnlWord.Visible = True

        PnlToolStrip.Visible = True

        pnlInfoBrowser.Visible = False
        'pnlBrowserToolStrip.Visible = False

    End Sub

    Private Sub SaveSmartEducation()
        Dim i As Integer
        Dim myNode As myTreeNode
        Dim myChildNode As myTreeNode
        Dim _ID As Int64
        Dim _Name As String

        If trvTemplate.Nodes.Count = 3 Then
            myNode = CType(trvTemplate.Nodes(2), myTreeNode)
            If Not IsNothing(myNode) Then
                If myNode.GetNodeCount(False) > 0 Then
                    For i = 0 To myNode.GetNodeCount(False) - 1
                        '   myChildNode = New myTreeNode()
                        myChildNode = CType(myNode.Nodes(i), myTreeNode)
                        If Not IsNothing(myChildNode) Then
                            _ID = myChildNode.Key
                            _Name = myChildNode.Text

                            If m_VisitID = 0 Then '' 
                                m_VisitID = GenerateVisitID(m_patientID)
                            End If

                            Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.EncounterDiagnosis
                            ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
                            ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientEducation
                            oPatientEducation.SaveExamEducationSmart(m_VisitID, m_patientID, m_ExamID, _speNotes, _Name, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub CheckAndSetExam(_ID As Long, _Name As String)
        If m_VisitID = 0 Then
            m_VisitID = GenerateVisitID(m_patientID)
        End If

        Dim Ispresent As Boolean = CheckISPresentInExamEducation(m_patientID, _Name, m_VisitID, Source, ResourceCategory, ResourceType)

        If Ispresent = False Then
            'Fetch Details form TemplateGallery_Mst
            Dim dt As DataTable = Nothing
            dt = GetEducationMaterialUsingTempalteID(_ID)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    SetSPENotesAndBiblographyOtherMaterial(dt)
                    'TempExamID = m_ExamID
                End If
                dt.Dispose()
                dt = Nothing
            End If

        End If

        If Ispresent = True Then
            'Fetch Details form ExamEducation
            Dim dt As DataTable = Nothing
            dt = GetEducationMaterialUsingTempalteID(m_patientID, _Name, m_VisitID, Source, ResourceCategory, ResourceType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    SetSPENotesAndBiblographyOtherMaterial(dt)
                    'TempExamID = m_ExamID
                End If
                dt.Dispose()
                dt = Nothing
            End If
        End If


        _ISTreeviewOpen = True

        'Load speNotes in DsoFramer and openEducationTemplate
        OpenEducationTemplate(CType(_ID, Int64), _Name, _speNotes)

    End Sub

    Private Sub txtGridSearch_TextChanged(sender As Object, e As System.EventArgs) Handles txtGridSearch.TextChanged
        Dim enumerableTemplates As EnumerableRowCollection(Of DataRow) = Nothing

        'grdPatienEducation.ClearSelection()

        Try
            Dim sSearchText As String = txtGridSearch.Text.ToLower()

            enumerableTemplates = From row As DataRow In dvPatientEducationList
                                  Where row("sTemplateName").ToString.ToLower.Contains(sSearchText) Or row("Source").ToString.ToLower.Contains(sSearchText) Or row("VisitDate").ToString.ToLower.Contains(sSearchText)
                                  Select row

            BindToGrid(enumerableTemplates.AsDataView)
            grdPatienEducation.ClearSelection()
            enumerableTemplates = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try





    End Sub

    Private Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        txtGridSearch.Clear()
    End Sub

    Private Sub ShowPreview(ResCat As Integer)
        If ResCat = 2 Then
            Dim InfoButtonForm As gloEMRGeneralLibrary.frmInfoButtonBrowser = gloEMRGeneralLibrary.frmInfoButtonBrowser.GetInstance
            Try
                RemoveHandler InfoButtonForm.FormClosed, AddressOf On_InfoButton_Closed
            Catch ex As Exception

            End Try
            AddHandler InfoButtonForm.FormClosed, AddressOf On_InfoButton_Closed
            With InfoButtonForm
                .LoginProviderID = gnLoginProviderID
                .PatientId = _patID
                .VisitID = _visID
                .EducationID = _EduID
                .isViewed = True
                .Source = _src
                .ResourceCategory = _recCat
                .ResourceType = _resTyp
                .NavigateTo(_docURL)
                .ValidatePortalFeatures()
                .ShowDialog()
                m_VisitID = InfoButtonForm.VISID
                m_VisitID = _visID
                FillGrid()
                SetGrid()
                .Dispose()
            End With
        Else

            ' wdPatientEducation.Close()
            bnlIsEduPreviewOpened = True
            Dim frm As frmPatientEducationPreview
            frm = New frmPatientEducationPreview()
            frm.VISID = _visID
            'frm.VisDate = _visDate
            frm.PATID = _patID
            frm.TempName = _tempName
            frm.Sourc = _src
            frm.ResourcCat = _recCat
            frm.ResourcTyp = _resTyp
            frm.ISGRID = _ISGrid
            frm.TMPID = _tmpID
            frm.FromOutSide = False
            frm.EXAMID = _examID
            ''00000803: Patient Education. New variables added for modify education scenario.
            frm.isModify = _IsModify
            frm.EducationID = nEducationID
            Try ''added for Bug #104664  When user check first Checkbox another checkbox automatically get checked
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            Catch ex As Exception
                ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

           frm.ShowDialog()

            Try  ''added for Bug #104664 
                AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            Catch ex As Exception
                ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            _closeClick = frm.CloseClick

            If _closeClick = False Then

                m_VisitID = frm.VISID
                m_VisitID = _visID
                _spe = frm.SPE
                _tempName = frm.TempName

                FillGrid()
                SetGrid()

                OpenEducationTemplate(0, _tempName, _spe)

            Else
                Dim grdindex As Integer

                If grdPatienEducation.SelectedRows.Count > 0 Then
                    For grdindex = 0 To grdPatienEducation.Rows.Count - 1
                        '''' when ID Found select that matching Row
                        If grdPatienEducation.Rows(grdindex).Selected = True Then
                            ShowSingleClick(grdindex)
                            Exit For
                        End If
                    Next

                    '_patID = m_patientID


                    'Dim grdSource As String
                    'Dim grdResourceCategory As String
                    'Dim grdResourcetype As String


                    'If m_patientID = 0 Then
                    '    _VisitID = GenerateVisitID(m_patientID) ''gn_VisitID replaced by _VisitID
                    'End If

                    'EduID = Convert.ToInt64(grdPatienEducation.Item("nEducationID", grdindex).Value)
                    '_CurrentTemplateName = grdPatienEducation.Item("sTemplateName", grdindex).Value.ToString()
                    '_tempName = _CurrentTemplateName
                    'grdSource = grdPatienEducation.Item("Source", grdindex).Value.ToString()
                    'grdResourceCategory = grdPatienEducation.Item("Resource_Category", grdindex).Value.ToString()
                    'grdResourcetype = grdPatienEducation.Item("Resource_Type", grdindex).Value.ToString()
                    '_docURL = grdPatienEducation.Item("sDocumentURL", grdindex).Value.ToString()


                    'Source = Convert.ToInt16(grdPatienEducation.Item("Src", grdindex).Value)
                    'ResourceCategory = Convert.ToInt16(grdPatienEducation.Item("ResCat", grdindex).Value)
                    'ResourceType = Convert.ToInt16(grdPatienEducation.Item("ResType", grdindex).Value)
                    '_ISGrid = True



                End If


            End If
            bnlIsEduPreviewOpened = False
            txtTemplateSearch.Text = String.Empty
            If (IsNothing(frm) = False) Then
                frm.Close()

            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If
        End If




    End Sub

    Private Sub grdPatienEducation_CellMouseClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdPatienEducation.CellMouseClick
        Try

            If CheckWordForException() = False Then
                Exit Sub
            End If

            If (e.RowIndex >= 0) Then
                ShowSingleClick(e.RowIndex)
                If (grdPatienEducation.Rows.Count > e.RowIndex) Then
                    grdPatienEducation.Rows(e.RowIndex).Selected = True
                End If
                If e.ColumnIndex = 0 Or e.ColumnIndex = 11 Then
                    ShowDoubleClick()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub grdPatienEducation_RowEnter(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPatienEducation.RowEnter

    End Sub

    'Private Sub grdPatienEducation_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles grdPatienEducation.SelectionChanged
    '    Try
    '        If CheckWordForException() = False Then
    '            Exit Sub
    '        End If

    '        Dim selectedIndex As Integer = DirectCast(sender, DataGridViewRow).Index
    '        grdPatienEducation.Rows(selectedIndex).Selected = True


    '        If (grdPatienEducation.SelectedRows.Count >= 0) Then
    '            Application.DoEvents()
    '            ShowSingleClick(grdPatienEducation.SelectedRows(0).Index)
    '            grdPatienEducation.Rows(selectedIndex).Selected = True

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub grdPatienEducation_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles grdPatienEducation.MouseClick

    End Sub

    Private Sub grdPatienEducation_CellToolTipTextNeeded(sender As System.Object, e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles grdPatienEducation.CellToolTipTextNeeded
        If e.ColumnIndex = 0 Or e.ColumnIndex = 11 Then
            e.ToolTipText = "Modify"
        End If
    End Sub


    Private Sub tlsInfobutton_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsInfobutton.ItemClicked
        If e.ClickedItem.Tag = "Home" Then
            InfoButtonWebBrowser.Navigate(HomeUrl)
        ElseIf e.ClickedItem.Tag = "Refresh" Then
            InfoButtonWebBrowser.Navigate(BrowserLink)
        ElseIf e.ClickedItem.Tag = "Next" Then
            InfoButtonWebBrowser.GoForward()
        ElseIf e.ClickedItem.Tag = "Previous" Then
            InfoButtonWebBrowser.GoBack()
        End If
    End Sub

    Private Sub InfoButtonWebBrowser_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles InfoButtonWebBrowser.NewWindow
        'This is to prevent the WebBrowser control from opening
        'new widows.
        e.Cancel = True
        BrowserLink = InfoButtonWebBrowser.StatusText
        InfoButtonWebBrowser.Navigate(BrowserLink)
    End Sub

    'Added Code for Bug #90792: Key Press on "Internal library"
    Private Sub trvTemplate_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles trvTemplate.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Dim obje As System.EventArgs = Nothing
            Call trvTemplate_DoubleClick(sender, obje)
        End If
    End Sub
End Class
