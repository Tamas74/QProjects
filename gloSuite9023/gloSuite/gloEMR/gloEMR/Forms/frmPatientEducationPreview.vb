Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports System.Runtime.InteropServices
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Reflection
Imports System.Data.SqlClient
Imports gloPatientPortalCommon
Imports System.Timers



Public Class frmPatientEducationPreview
    Inherits System.Windows.Forms.Form
    Implements ISignature
    Implements gloVoice, IExamChildEvents
    Implements IHotKey
    Implements IPatientContext


#Region " Private Variables "

    Private ogloVoice As ClsVoice
    Private m_VisitID As Long
    Private m_patientID As Long
    Private m_ExamID As Long
    Private m_arrTemplateID As ArrayList
    Private oPatientEducation As New clsPatientEducation
    Public WithEvents oCurDoc As Wd.Document
    Private blnTemplateExist As Boolean = False
    Private objCriteria As DocCriteria
    Private oWord As clsWordDocument
    Private myidx As Int32
    Private arrTemplates As New ArrayList
    'Private dictionaryExistingTemplates As Dictionary(Of Long, myList)
    'Private hashNewTemplates As HashSet(Of Long)
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


    Dim _speNotes As Object
    Dim _nOldEducationID As Int64

    Dim _ISTreeviewOpen = False
    Dim _FromGrid As Boolean = False


    Private TempExamID As Long = 0

    Private nPatientEducationID As Int64 = 0
    Public bnlIsFaxOpened As Boolean
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
    Public isModify As Boolean = False ''00000803: Patient Education. New variable added for modify education scenario.
#End Region

#Region " C1 Constants "
    Const COL_TEMPLATENAME = 1
    Const COL_ID = 2
    Const COL_TOTAL = 3
#End Region

#Region "All Property"


    'Is Close Click
    Private _closeClick As Boolean = True
    Dim patEducationID As Long = 0

    Public Property CloseClick As Boolean
        Get
            Return _closeClick
        End Get
        Set(ByVal value As Boolean)
            _closeClick = value
        End Set
    End Property

    'ArrayList
    Private _arrList As ArrayList = Nothing
    Public Property ArrList As ArrayList
        Get
            Return _arrList
        End Get
        Set(ByVal value As ArrayList)
            _arrList = value
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

    'FROM OutSide
    Private _FromOutSide As Boolean = False
    Public Property FromOutSide As Boolean
        Get
            Return _FromOutSide
        End Get
        Set(ByVal value As Boolean)
            _FromOutSide = value
        End Set
    End Property


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
    Friend WithEvents pnlcombo As System.Windows.Forms.Panel
    Friend WithEvents txtSelectedTemplates As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents wdPatientEducation As AxDSOFramer.AxFramerControl
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnAddFields As System.Windows.Forms.Button
    Friend WithEvents GloUC_AddRefreshDic1 As gloUserControlLibrary.gloUC_AddRefreshDic
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlBibliographic As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents txtBibliographyDeveloper As System.Windows.Forms.RichTextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtBibliography As System.Windows.Forms.RichTextBox
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
        InitializeTimer()
        'pnlTreeViewMain.Visible = isPnlHide

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
        'pnlTreeViewMain.Visible = isPnlHide
        pnlcombo.Visible = isPnlHide
        'PnlGridToolStrip.Visible = isPnlHide
        'pnlEduGrid.Visible = isPnlHide
        _isPnlHidden = isPnlHide
        'pnlPreview.Visible = isPnlHide
        nEducationID = EducationID
        sDocumentName = templateName
        m_patientID = nPatientID
        Source = src
        ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
        ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
        _isOpenforInfobutton = fromInfobutton
        ' _FromOutside = True

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
            _FromOutSide = True
        End If



        'This call is required by the Windows Form Designer.
        InitializeComponent()
        InitializeTimer()
        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New()

        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        InitializeTimer()
        'Add any initialization after the InitializeComponent() call

    End Sub
    Private tTimer As System.Timers.Timer
    Private Sub InitializeTimer()
        tTimer = New System.Timers.Timer()
        tTimer.Interval = 2000
        AddHandler tTimer.Elapsed, New ElapsedEventHandler(AddressOf timerHandlerToGetWordApplication)
        StopTimer()
    End Sub
    Private Sub StopTimer()
        If Not IsNothing(tTimer) Then
            tTimer.Enabled = False
        End If
        'tTimer.Stop() : both are same
    End Sub
    Private ActivatedCalled As Boolean = True
    Private Sub StartTimer(ByVal bActivate As Boolean)
        If Not IsNothing(tTimer) Then
            tTimer.Enabled = True
        End If
        ActivatedCalled = bActivate
        'tTimer.Start() : both are same
    End Sub
    Private Sub DisposeTimer()
        Try
            StopTimer()

            If Not IsNothing(tTimer) Then
                Try
                    RemoveHandler tTimer.Elapsed, New Timers.ElapsedEventHandler(AddressOf timerHandlerToGetWordApplication)

                Catch ex As Exception

                End Try

                tTimer.Dispose()
            End If
            tTimer = Nothing

        Catch ex As Exception

        End Try
    End Sub
    Private Sub RemoveHandlers()
        If (IsNothing(oWordApp) = False) Then
            Try
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            isHandlerRemoved = True
        End If
    End Sub
    Private Sub AddHandlers()
        If (IsNothing(oWordApp) = False) Then
            Try
                RemoveHandlers()  ''added for Bug #104664 
                AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                AddHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked '' added to hide word dialog box bugid 104596
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            isHandlerRemoved = False
        End If
    End Sub
    Public Sub OnFormClicked(ByVal Sel As Wd.Selection, ByRef Cancel As Boolean)
        Try

            ''commented for Double-click on words issue
            'If Sel.Type = 1 Then '' added to hide word dialog box bugid 104596
            '    Cancel = True
            '    Return
            'End If
        Catch ex As Exception

        End Try
        ''  Cancel = True
    End Sub
    Private Sub timerHandlerToGetWordApplication(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        StopTimer()
        If (ActivatedCalled) Then
            If (IsNothing(oCurDoc) = False) Then
                Try
                    oWordApp = oCurDoc.Application
                    RemoveHandlers()
                    AddHandlers()
                    Try
                        oCurDoc.ActiveWindow.SetFocus()
                        wdPatientEducation.Focus()
                    Catch
                    End Try
                Catch ex2 As Exception
                    StartTimer(ActivatedCalled)
                End Try
            End If
        Else
            RemoveHandlers()
        End If

    End Sub


    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            'Added IsNothing to as GloUC_AddRefreshDic1 was giving object reference error
            If IsNothing(GloUC_AddRefreshDic1) = False Then
                If (GloUC_AddRefreshDic1.dtLetterAllocated) Then

                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                    Catch
                    End Try
                    Dim dtpControls As DateTimePicker() = {GloUC_AddRefreshDic1.DTLETTERDATEs}
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                        gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                    Catch ex As Exception

                    End Try

                    GloUC_AddRefreshDic1.dtLetterAllocated = False
                End If
            End If




            RemoveHandlers()
            DisposeTimer()
            Try
                If (IsNothing(_PatientStrip) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(_PatientStrip)
                    Catch ex As Exception

                    End Try

                    _PatientStrip.Dispose()
                    _PatientStrip = Nothing
                End If
            Catch ex As Exception

            End Try
            If Not IsNothing(ogloVoice) Then
                ogloVoice.Dispose()
                ogloVoice = Nothing
            End If
            Try
                If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                    If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                        DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                        GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
                    End If
                End If

            Catch

            End Try
            Dispose_Object()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    ' Friend WithEvents WdPatientEdu As AxDSOFramer.AxFramerControl
    Friend WithEvents trvEducation As System.Windows.Forms.TreeView



    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientEducationPreview))
        Me.pnlWord = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.wdPatientEducation = New AxDSOFramer.AxFramerControl()
        Me.pnlcombo = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GloUC_AddRefreshDic1 = New gloUserControlLibrary.gloUC_AddRefreshDic()
        Me.btnAddFields = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSelectedTemplates = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PnlToolStrip = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
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
        Me.pnlWord.SuspendLayout()
        CType(Me.wdPatientEducation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlcombo.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PnlToolStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlBibliographic.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.pnlWord.Location = New System.Drawing.Point(3, 31)
        Me.pnlWord.Name = "pnlWord"
        Me.pnlWord.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlWord.Size = New System.Drawing.Size(1015, 702)
        Me.pnlWord.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 697)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(0, 698)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1011, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(1011, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 698)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1012, 1)
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
        Me.wdPatientEducation.Size = New System.Drawing.Size(1012, 699)
        Me.wdPatientEducation.TabIndex = 3
        '
        'pnlcombo
        '
        Me.pnlcombo.BackColor = System.Drawing.Color.Transparent
        Me.pnlcombo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlcombo.Controls.Add(Me.Panel2)
        Me.pnlcombo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlcombo.Location = New System.Drawing.Point(3, 3)
        Me.pnlcombo.Name = "pnlcombo"
        Me.pnlcombo.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlcombo.Size = New System.Drawing.Size(1015, 28)
        Me.pnlcombo.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.btnAddFields)
        Me.Panel2.Controls.Add(Me.btnRefresh)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.txtSelectedTemplates)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1012, 25)
        Me.Panel2.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GloUC_AddRefreshDic1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(933, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(78, 23)
        Me.Panel1.TabIndex = 36
        '
        'GloUC_AddRefreshDic1
        '
        Me.GloUC_AddRefreshDic1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GloUC_AddRefreshDic1.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_AddRefreshDic1.CONNECTIONSTRINGs = Nothing
        Me.GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing
        Me.GloUC_AddRefreshDic1.Location = New System.Drawing.Point(19, 2)
        Me.GloUC_AddRefreshDic1.M_PATIENTIDs = CType(0, Long)
        Me.GloUC_AddRefreshDic1.Name = "GloUC_AddRefreshDic1"
        Me.GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
        Me.GloUC_AddRefreshDic1.ObjFrom = Nothing
        Me.GloUC_AddRefreshDic1.OBJWORDs = Nothing
        Me.GloUC_AddRefreshDic1.OCURDOCs = Nothing
        Me.GloUC_AddRefreshDic1.OWORDAPPs = Nothing
        Me.GloUC_AddRefreshDic1.Size = New System.Drawing.Size(48, 19)
        Me.GloUC_AddRefreshDic1.TabIndex = 35
        Me.GloUC_AddRefreshDic1.Visible = False
        Me.GloUC_AddRefreshDic1.wdPatientWordDocs = Nothing
        '
        'btnAddFields
        '
        Me.btnAddFields.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddFields.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddFields.FlatAppearance.BorderSize = 0
        Me.btnAddFields.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnAddFields.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnAddFields.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddFields.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddFields.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAddFields.Location = New System.Drawing.Point(725, 2)
        Me.btnAddFields.Name = "btnAddFields"
        Me.btnAddFields.Size = New System.Drawing.Size(20, 20)
        Me.btnAddFields.TabIndex = 29
        Me.btnAddFields.UseVisualStyleBackColor = True
        Me.btnAddFields.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRefresh.Location = New System.Drawing.Point(699, 2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(20, 20)
        Me.btnRefresh.TabIndex = 28
        Me.btnRefresh.UseVisualStyleBackColor = True
        Me.btnRefresh.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(1011, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 23)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "label3"
        '
        'txtSelectedTemplates
        '
        Me.txtSelectedTemplates.BackColor = System.Drawing.Color.White
        Me.txtSelectedTemplates.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSelectedTemplates.ForeColor = System.Drawing.Color.Black
        Me.txtSelectedTemplates.Location = New System.Drawing.Point(162, 1)
        Me.txtSelectedTemplates.Multiline = True
        Me.txtSelectedTemplates.Name = "txtSelectedTemplates"
        Me.txtSelectedTemplates.ReadOnly = True
        Me.txtSelectedTemplates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSelectedTemplates.Size = New System.Drawing.Size(528, 23)
        Me.txtSelectedTemplates.TabIndex = 0
        Me.txtSelectedTemplates.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "  Selected Template's :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label1.Visible = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Location = New System.Drawing.Point(1, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1011, 1)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 24)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(0, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1012, 1)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "label2"
        '
        'PnlToolStrip
        '
        Me.PnlToolStrip.Controls.Add(Me.Label16)
        Me.PnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.PnlToolStrip.Name = "PnlToolStrip"
        Me.PnlToolStrip.Size = New System.Drawing.Size(1018, 24)
        Me.PnlToolStrip.TabIndex = 40
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(12, 5)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 14)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "ToolStrip"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label16.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlWord)
        Me.pnlMain.Controls.Add(Me.pnlcombo)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 24)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnlMain.Size = New System.Drawing.Size(1018, 733)
        Me.pnlMain.TabIndex = 9
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
        Me.pnlBibliographic.Location = New System.Drawing.Point(285, 283)
        Me.pnlBibliographic.Name = "pnlBibliographic"
        Me.pnlBibliographic.Size = New System.Drawing.Size(647, 426)
        Me.pnlBibliographic.TabIndex = 42
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
        Me.Panel5.Size = New System.Drawing.Size(645, 398)
        Me.Panel5.TabIndex = 26
        '
        'txtBibliographyDeveloper
        '
        Me.txtBibliographyDeveloper.BackColor = System.Drawing.Color.Azure
        Me.txtBibliographyDeveloper.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBibliographyDeveloper.Location = New System.Drawing.Point(155, 239)
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
        Me.txtBibliography.Location = New System.Drawing.Point(155, 25)
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
        Me.Label24.Location = New System.Drawing.Point(0, 397)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(645, 1)
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
        Me.Panel7.Size = New System.Drawing.Size(645, 27)
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
        Me.Label33.Size = New System.Drawing.Size(360, 1)
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
        Me.btnBibClose.Location = New System.Drawing.Point(611, 0)
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
        Me.Label35.Location = New System.Drawing.Point(646, 1)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 425)
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
        Me.Label36.Size = New System.Drawing.Size(1, 425)
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
        Me.Label37.Size = New System.Drawing.Size(647, 1)
        Me.Label37.TabIndex = 27
        '
        'frmPatientEducationPreview
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1018, 757)
        Me.Controls.Add(Me.pnlBibliographic)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.PnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientEducationPreview"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Education"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlWord.ResumeLayout(False)
        CType(Me.wdPatientEducation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlcombo.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.PnlToolStrip.ResumeLayout(False)
        Me.PnlToolStrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlBibliographic.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Form Events "

    Private Sub frmPatientEducationPreview_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

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
                        'oCurDoc.FormFields.Shaded = False
                        'SLR: moved following code to timer function as per the link for System.Runtime.InteropServices.COMException (0x8001010D): An outgoing call cannot be made since the application is dispatching an input-synchronous call. (Exception from HRESULT: 0x8001010D (RPC_E_CANTCALLOUT_ININPUTSYNCCALL) : http://www.experts-exchange.com/Programming/Languages/Visual_Basic/Q_20152915.html
                        'oWordApp = oCurDoc.Application
                        'Try
                        '    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        'Catch ex As Exception
                        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        '    ex = Nothing
                        'End Try

                        'Try
                        '    AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        'Catch ex As Exception
                        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        '    ex = Nothing
                        'End Try
                        'isHandlerRemoved = False

                        StartTimer(True)
                        Try
                            oCurDoc.ActiveWindow.SetFocus()
                            wdPatientEducation.Focus()
                        Catch
                        End Try

                    End If
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

    'Private Sub frmPatientEducation_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    '    Try
    '        If (IsNothing(Me.ParentForm) = False) Then
    '            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
    '        Else
    '            If (IsNothing(Me.myCaller) = False) Then
    '                CType(Me.myCaller.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
    '            End If

    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try

    '    If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
    '        If Not IsNothing(ogloVoice) Then
    '            ogloVoice.ShowMicroPhone()
    '        End If
    '    End If




    '    'Developer: Yatin N.Bhagat
    '    'Date:12/26/2011
    '    'Bug ID/PRD Name/Salesforce Case:Bug No. 17246:Patient Consent >> Checkbox in the template are not working once you finish exam
    '    'Reason: Handler For DDLCBEvent is Not Added while activating the form
    '    Try
    '        If Not (IsNothing(wdPatientEducation)) Then
    '            If Not (IsNothing(wdPatientEducation.DocumentName)) Then
    '                If Not (IsNothing(wdPatientEducation.ActiveDocument)) Then
    '                    oCurDoc = wdPatientEducation.ActiveDocument
    '                    oWordApp = oCurDoc.Application
    '                    Try
    '                        RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
    '                    Catch ex As Exception

    '                    End Try

    '                    Try
    '                        AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
    '                    Catch ex As Exception

    '                    End Try

    '                    isHandlerRemoved = False
    '                    oCurDoc.ActiveWindow.SetFocus()
    '                    wdPatientEducation.Focus()
    '                End If
    '            End If
    '        End If


    '    Catch ex As Exception
    '    End Try
    '    'Shubhangi 20100120
    '    'To resolve the issue: When we right click the form gets disappear
    '    For Each myForm As Form In Application.OpenForms
    '        If (myForm.TopMost) Then
    '            myForm.TopMost = False
    '        End If
    '    Next
    '    Me.TopMost = True
    '    Me.WindowState = FormWindowState.Maximized
    'End Sub

    Private Sub frmPatientEducation_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        StopTimer()

        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If


        'Developer: Yatin N.Bhagat
        'Date:12/26/2011
        'Bug ID/PRD Name/Salesforce Case:Bug No. 17246:Patient Consent >> Checkbox in the template are not working once you finish exam
        'Reason: Handler For DDLCBEvent is Not Added while activating the form
        'SLR: move to timer function, because unable to remove handler.
        StartTimer(False)
        'Try
        '    If Not oWordApp Is Nothing Then
        '        RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        '        isHandlerRemoved = True
        '        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick  for oWordApp", gloAuditTrail.ActivityOutCome.Success)
        '    End If
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    ex = Nothing
        'End Try



        'Shubhangi 20100120
        'To resolve the issue: When we right click the form gets disappear
        Me.TopMost = False
    End Sub

    Private Sub frmPatientEducation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try
            If Not IsNothing(wdPatientEducation) Then
                wdPatientEducation.Close()
            End If
            _FromOutSide = False

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
                        MyMDIParent.MdiExamChildDeActivate(Me)
                    End If

                End If
            End If
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

            If (IsNothing(mdlFAX.Owner) = False) Then
                mdlFAX.Owner = Nothing
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub


    Private Sub frmPatientEducation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If IsNothing(oCurDoc) = False Then
            If oCurDoc.Saved = False Then
                'Word document
                If ResourceType <> gloEMRGeneralLibrary.clsInfobutton.enumResourceType.ProviderReferenceMaterial Then
                    If ResourceCategory = 1 Then
                        Dim Result As DialogResult
                        Result = MessageBox.Show("Do you want to save the changes to Patient Education?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        If Result = Windows.Forms.DialogResult.Yes Then
                            _closeClick = False
                            If (IsNothing(oCurDoc) = False) Then
                                oCurDoc.SaveAs(_DocumentName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                            Else
                                wdPatientEducation.Save(_DocumentName, True, "", "")
                            End If
                            'If Not IsSaving And Not isClosed Then
                            '    GenerateAuditLogForSave()
                            'End If

                            _isEducationChanged = False

                            'oCurDocTemp = oCurDoc
                            oCurDoc = Nothing
                            wdPatientEducation.Close()

                            oWord = New clsWordDocument
                            _speNotes = CType(oWord.ConvertFiletoBinary(_DocumentName), Object)
                            oWord = Nothing

                            ''00000803: Patient Education. Use exam visit Id if available.
                            Dim Prev_VisitId As Long = m_VisitID
                            If Not _PatientStrip Is Nothing Then
                                m_VisitID = GenerateVisitID(_PatientStrip.DTPValue, m_patientID)
                            Else
                                m_VisitID = GenerateVisitID(m_patientID)
                            End If

                            _visID = m_VisitID
                            _spe = _speNotes
                            _tempName = _CurrentTemplateName

                            ''00000803: Patient Education. Used examId property during save.
                            If isModify Then
                                If Prev_VisitId <> 0 And Prev_VisitId <> m_VisitID Then
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Patient Education Date Modified. Old Date - " & GetVisitdate(Prev_VisitId), m_patientID, EducationID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                End If
                                oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, _examID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper, EducationID)
                            Else
                                oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, _examID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)
                            End If
                            nPatientEducationID = oPatientEducation._nPatientEducationID
                            ' FillGrid()
                            'SaveExamEducation(True, True)
                            'GenerateAuditLogForSave()
                        ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                            e.Cancel = True
                        ElseIf Result = Windows.Forms.DialogResult.No Then
                            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Patient Education viewed", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                            e.Cancel = False
                        End If
                    End If
                End If
            End If
        End If
        Me.TopMost = False
        'If dictionaryExistingTemplates IsNot Nothing Then
        '    dictionaryExistingTemplates.Clear()
        '    dictionaryExistingTemplates = Nothing
        'End If

        'If hashNewTemplates IsNot Nothing Then
        '    hashNewTemplates.Clear()
        '    hashNewTemplates = Nothing
        'End If
    End Sub

    'Private Sub frmPatientEducation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Try
    '        ShowFromDoubleClick()

    '        ' objReferralsDBLayer = New ClsReferralsDBLayer
    '        Try
    '            gloPatient.gloPatient.GetWindowTitle(Me, m_patientID, GetConnectionString(), gstrMessageBoxCaption)
    '        Catch ex As Exception
    '            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        End Try
    '        'txtSearch.Focus()
    '        '_GridWidth = grdPatienEducation.Width
    '        pnlcombo.Visible = False
    '        'FillGrid()

    '        'If grdPatienEducation.Rows.Count > 0 Then
    '        '    Dim erg As New DataGridViewCellEventArgs(0, 0)
    '        '    grdPatienEducation.Rows(0).Selected = True
    '        '    grdPatienEducation_CellClick(Nothing, erg)
    '        'End If



    '        'FillGeneralTemplate()

    '        ''//To Load the Patient Strip Details
    '        LoadPatientStrip()

    '        Dim dAge As Decimal = ageDetail.Years

    '        If ageDetail.Months > 0 Then
    '            dAge = dAge + 0.1
    '        ElseIf ageDetail.Days > 1 Then
    '            dAge = dAge + 0.1
    '        End If






    '        If _FromOutside = True Then

    '            If m_arrTemplateID IsNot Nothing Then
    '                If m_arrTemplateID.Count > 0 Then
    '                    'FillSmartDx(m_arrTemplateID)
    '                End If
    '            End If

    '            If pnlPreview.Visible = False Then
    '                CheckAndSetExam(nEducationID, sDocumentName)
    '            End If

    '        End If


    '        'FillTemplates()

    '        'If m_arrTemplateID IsNot Nothing Then
    '        '    For i = 0 To m_arrTemplateID.Count - 1

    '        '        '' OPEN TEMPLATE ''
    '        '        Dim _ID As Int64 = CType(m_arrTemplateID(i), myList).Index
    '        '        Dim _Name As String = CType(m_arrTemplateID(i), myList).Description
    '        '        OpenEducationTemplate(_ID, _Name)

    '        '        '' ADD TEMPLATE IN GRID IF NOT PRESENT ''
    '        '        'Dim _Row As Integer = C1Template.FindRow(_ID, 0, COL_ID, False, True, False)
    '        '        Dim _Row As Integer = C1Template1.FindRow(_Name, 0, COL_TEMPLATENAME, False, True, False)
    '        '        If _Row < 0 Then
    '        '            C1Template.Rows.Add()
    '        '            _Row = C1Template.Rows.Count - 1
    '        '            C1Template.SetData(_Row, COL_TEMPLATENAME, _Name)
    '        '            C1Template.SetData(_Row, COL_ID, _ID)
    '        '        End If
    '        '        'Change made to solve memory Leak and word crash issue
    '        '        _Row = Nothing
    '        '        _ID = Nothing
    '        '        _Name = Nothing
    '        '    Next


    '        'End If

    '        'If IsNothing(m_arrTemplateID) = False Then
    '        '    ' Fill Patient Education Templates asscocated with ICD9 in Smart Diagnosis
    '        '    Call Fill_PatientEducation()
    '        'Else
    '        '    ' By Mahesh, 20070323
    '        '    ' to Open Patient Education for Update (From Patient Exam)
    '        '    Call Fill_EducationForUpdate()
    '        'End If


    '        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
    '            InitializeVoiceObject()
    '            If Me.IsMdiChild = False Then
    '                MyMDIParent.MdiExamChildActivate(Me)
    '            End If
    '        End If



    '        ''dhruv 20100611 - 5975 ''View > patient Education > modify button is missing on this screen
    '        'If _isPnlHidden = False Then
    '        '    ShowDocument(nEducationID, sDocumentName)
    '        'End If

    '        ''Dhruv -> End 
    '        'C1Template_DoubleClick(sender, e)
    '        ''gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Patient Education Opened", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Load, "Patient Education Opened", gloAuditTrail.ActivityOutCome.Success)
    '        ''Added Rahul P on 20101011

    '        If ResourceType = 2 Then
    '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Load, "Provider Reference Document Opened", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Load, "Provider Reference Document Opened", gloAuditTrail.ActivityOutCome.Success)
    '        Else
    '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Load, "Patient Education Opened", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            ''
    '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Load, "Patient Education Opened", gloAuditTrail.ActivityOutCome.Success)
    '        End If


    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub frmPatientEducationPreview_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.TopMost = True

        wdPatientEducation.Titlebar = False
        wdPatientEducation.Menubar = False

        'Assign Value and Show
        _visID = VISID
        m_VisitID = _visID

        _tmpID = TMPID

        _patID = PATID
        m_patientID = _patID

        _tempName = TempName
        _CurrentTemplateName = _tempName

        _src = Sourc
        Source = _src

        _recCat = ResourcCat
        ResourceCategory = _recCat

        _resTyp = ResourcTyp
        ResourceType = ResourcTyp

        _ISGrid = ISGRID

        _FromOutSide = FromOutSide

        _arrList = ArrList

        SetPatientRegistrationStatusOnPortal()

        If Not IsNothing(_arrList) Then
            If _arrList.Count > 0 Then
                SaveExamEducationsWithArrayList(False)
            End If
        Else
            LoadPatientStrip()
            ShowFromDoubleClick()
        End If
        ''00000803: Patient Education. Show visit date on patient strip
        If (Not _PatientStrip Is Nothing) AndAlso (m_VisitID <> 0) Then
            _PatientStrip.DTPValue = GetVisitdate(m_VisitID)
        End If




    End Sub

#End Region

#Region " Voice Events "
    Private Event ActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Implements mdlgloVoice.IExamChildEvents.ActivateExamChild

    Private Event DeActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Implements mdlgloVoice.IExamChildEvents.DeActivateExamChild
#End Region

#Region " Private Methods "

    'Private Sub FillTemplates()
    '    'DesignGrid()

    '    arrTemplates = oPatientEducation.GetPatientEductionArray(m_patientID, m_ExamID, m_VisitID)

    '    'If arrTemplates.Count > 0 Then

    '    '    If dictionaryExistingTemplates Is Nothing Then
    '    '        dictionaryExistingTemplates = New Dictionary(Of Long, myList)
    '    '    End If

    '    '    For Each MyListElement As myList In arrTemplates
    '    '        dictionaryExistingTemplates.Add(MyListElement.ID, MyListElement)
    '    '    Next

    '    '    'Dim _Row As Integer
    '    '    'For i As Integer = 0 To arrTemplates.Count - 1
    '    '    '    C1Template.Rows.Add()
    '    '    '    _Row = C1Template.Rows.Count - 1

    '    '    '    C1Template.SetData(_Row, COL_TEMPLATENAME, CType(arrTemplates(i), myList).Description)
    '    '    '    C1Template.SetData(_Row, COL_ID, CType(arrTemplates(i), myList).ID)
    '    '    'Next
    '    '    '_Row = Nothing  'Change made to solve memory Leak and word crash issue
    '    'End If

    'End Sub

    ''' <summary>
    '''  To Fill Patient Education for update 
    ''' </summary>
    ''' <remarks></remarks>
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
                txtSelectedTemplates.Text = dt.Rows(0)("sTemplateName")
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


    Private Sub Fill_TemplateGallery(ByVal TemplateID As Long)

        Dim strFileName As String = ""
        Dim oList As New myList
        Dim dtTemplate As DataTable = Nothing
        oWord = New clsWordDocument
        objCriteria = New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Template
        objCriteria.PrimaryID = TemplateID
        oWord.DocumentCriteria = objCriteria


        If Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList Or Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication Or Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders Then
            dtTemplate = clsInfobutton_Education.GetEducationTemplate(m_patientID, TemplateID, Source, ResourceCategory, ResourceType, sDocumentName)
            If Not IsNothing(dtTemplate) Then
                If dtTemplate.Rows.Count > 0 Then
                    strFileName = ExamNewDocumentName
                    strFileName = oWord.GenerateFile(CType(dtTemplate.Rows(0)("sDescription"), Object), strFileName)
                    'txtBibliography.Text = Convert.ToString(dtTemplate.Rows(0)("bibliography"))
                    'txtBibliographyDeveloper.Text = Convert.ToString(dtTemplate.Rows(0)("sbDeveloper"))

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

        If IsNothing(strFileName) AndAlso strFileName <> "" Then
            LoadWordUserControl(strFileName, True)

            oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        End If

        strFileName = Nothing   'Change made to solve memory Leak and word crash issue

        If dtTemplate IsNot Nothing Then
            dtTemplate.Dispose()
            dtTemplate = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Insert the Concerned template in the document
    ''' </summary>
    ''' <param name="TemplateID"></param>
    ''' <remarks></remarks>
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


                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
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
                    'wdTemp.Close()

                    ''24-Apr-13 Aniket: Resolving Memory Leaks
                    'Me.Controls.Remove(wdTemp)
                    'wdTemp.Dispose()
                    myLoadWord.CloseWordOnly(oTempDoc)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing

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



    Private Sub loadToolStrip()

        If Not tlsPatientEducation Is Nothing Then
            tlsPatientEducation.Dispose()
        End If

        tlsPatientEducation = New WordToolStrip.gloWordToolStrip
        tlsPatientEducation.Dock = DockStyle.Top
        tlsPatientEducation.ConnectionString = GetConnectionString()
        tlsPatientEducation.UserID = gnLoginID
        ''Integrated ON 20101020 BY Mayuri FOR SIGNATURE
        tlsPatientEducation.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider
        tlsPatientEducation.ptProvider = oclsProvider.GetPatientProviderName(m_patientID)
        tlsPatientEducation.ptProviderId = oclsProvider.GetPatientProvider(m_patientID)
        'Change made to solve memory Leak and word crash issue
        oclsProvider.Dispose()
        oclsProvider = Nothing

        ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
        tlsPatientEducation.IsCoSignEnabled = gblnCoSignFlag
        tlsPatientEducation.FormType = WordToolStrip.enumControlType.PatientEducation

        Me.Controls.Add(tlsPatientEducation)
        Me.PnlToolStrip.Controls.Add(tlsPatientEducation)
        Me.PnlToolStrip.Size = New System.Drawing.Size(940, 56)
        PnlToolStrip.SendToBack()
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsPatientEducation
                ShowMicroPhone()
            End If
        End If
        If gblnAssociatedProviderSignature Then
            tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            tlsPatientEducation.MyToolStrip.ButtonsToHide.Remove(tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        Else
            tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsPatientEducation.MyToolStrip.ButtonsToHide.Contains(tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsPatientEducation.MyToolStrip.ButtonsToHide.Add(tlsPatientEducation.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If

        End If
        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
            If tlsPatientEducation.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                If (tlsPatientEducation.MyToolStrip.ButtonsToHide.Contains(tlsPatientEducation.MyToolStrip.Items("SecureMsg").Name) = False) Then
                    tlsPatientEducation.MyToolStrip.ButtonsToHide.Add(tlsPatientEducation.MyToolStrip.Items("SecureMsg").Name)
                End If
            End If

            If tlsPatientEducation.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                tlsPatientEducation.MyToolStrip.Items("SecureMsg").Visible = False
            End If
        End If



        If ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.ProviderReferenceMaterial Then
            tlsPatientEducation.MyToolStrip.Items("Reference Information").Visible = True
            tlsPatientEducation.MyToolStrip.Items("Save").Enabled = False
            tlsPatientEducation.MyToolStrip.Items("Save & Close").Enabled = False
            tlsPatientEducation.MyToolStrip.Items("SEND TO PORTAL").Visible = False
        Else
            If tlsPatientEducation.MyToolStrip.Items("SEND TO PORTAL").Visible = False Then
            Else
                tlsPatientEducation.MyToolStrip.Items("SEND TO PORTAL").Visible = False

                If gblnUSEINTUITINTERFACE = True And gblnIntuitCommunication = True Then
                    tlsPatientEducation.MyToolStrip.Items("SEND TO PORTAL").Visible = True


                ElseIf gblnPatientPortalEnabled = True And gblnIntuitCommunication = True Then
                    tlsPatientEducation.MyToolStrip.Items("SEND TO PORTAL").Visible = True
                Else
                    tlsPatientEducation.MyToolStrip.Items("SEND TO PORTAL").Visible = False

                End If
            End If


            tlsPatientEducation.MyToolStrip.Items("Reference Information").Visible = False
            If (tlsPatientEducation.MyToolStrip.ButtonsToHide.Contains(tlsPatientEducation.MyToolStrip.Items("Reference Information").Name) = False) Then
                tlsPatientEducation.MyToolStrip.ButtonsToHide.Add(tlsPatientEducation.MyToolStrip.Items("Reference Information").Name)
            End If
        End If





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
            If Not IsNothing(_DocumentName) AndAlso (_DocumentName <> "") Then
                LoadWordUserControl(_DocumentName, True)
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
        ''00000803: Patient Education. Get visit Id for selected date.
        Dim Prev_VisitId As Long = m_VisitID
        If Not _PatientStrip Is Nothing Then
            m_VisitID = GenerateVisitID(_PatientStrip.DTPValue, m_patientID)
        Else
            m_VisitID = GenerateVisitID(m_patientID)
        End If


        _visID = m_VisitID
        _spe = _speNotes
        _tempName = _CurrentTemplateName
        'End If

        _closeClick = False
        ''00000803: Patient Education. Used examId property during save.
        If isModify Then
            If Prev_VisitId <> 0 And Prev_VisitId <> m_VisitID Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Patient Education Date Modified. Old Date - " & GetVisitdate(Prev_VisitId), m_patientID, EducationID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, _examID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper, EducationID)
        Else
            oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, _examID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)
        End If


        nPatientEducationID = oPatientEducation._nPatientEducationID

        If _FromOutSide = True Then
            'SaveSmartEducation()
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

        If _ISGrid Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Modify, "Patient Education Modified", m_patientID, EducationID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Else
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Patient Education Added", m_patientID, nPatientEducationID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If

    End Sub

    Public Sub SaveExamEducationsWithArrayList(Optional ByVal IsClose As Boolean = False, Optional ByVal IsSaving As Boolean = False)

        'If oCurDoc Is Nothing Then
        '    Exit Sub
        'End If

        'AddTemplateAndSave(IsClose, IsSaving)

        If _ISGrid = False Then
            m_VisitID = GenerateVisitID(m_patientID)
        End If

        Dim i As Integer
        Dim dt As DataTable = Nothing

        If Not IsNothing(_arrList) Then
            For i = 0 To _arrList.Count - 1
                _tmpID = CType(_arrList(i), myList).Index
                _CurrentTemplateName = CType(_arrList(i), myList).Description

                dt = GetSPE(m_patientID, _tmpID, _CurrentTemplateName, m_VisitID, Source, ResourceCategory, ResourceType)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        _speNotes = CType(dt.Rows(0)("sDescription"), Object)
                        strBibliography = Convert.ToString(dt.Rows(0)("bibliography"))
                        strBibliographyDeveloper = Convert.ToString(dt.Rows(0)("sbDeveloper"))
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If

                _visID = m_VisitID
                _spe = _speNotes
                _tempName = _CurrentTemplateName
                ''00000803: Patient Education. Used examId property during save.
                oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, _examID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)
                nPatientEducationID = oPatientEducation._nPatientEducationID
            Next
        End If

        'Open From TreeView
        If _ISTreeviewOpen = True Then
            _ISTreeviewOpen = False
        End If

        'Open From Grid
        If _FromGrid = True Then
            _FromGrid = False
        End If

        If dt IsNot Nothing Then
            dt.Dispose()
            dt = Nothing
        End If

    End Sub


    Private Sub Print()
        If Not oCurDoc Is Nothing Then
            GeneratePrintFaxDocument()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Print, "Patient Education '" & txtSelectedTemplates.Text & "' Printed.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            If ResourceType = 2 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Print, "Provider Reference Document'" & txtSelectedTemplates.Text & "' Printed.", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Print, "Patient Education '" & txtSelectedTemplates.Text & "' Printed.", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

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

        Dim _SaveFlag As Boolean = False

        If oCurDoc Is Nothing Then
            Exit Sub
        End If

        If oCurDoc.Saved Then
            _SaveFlag = True
        End If

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


            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()

            Try
                '27-May-16 Aniket: Resolving Bug #96514: gloEMR : Patient Education( background printing) : When user click on modify button of printer profile window , printer setting window goes to backgeound side
                PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, m_patientID, AddressOf FaxPatientEducation, totalPages, PageNo:=PageNo, blnPrintCancel:=False, _PreviousUsedPrinter:="", UseDirectFaxName:=False, iOwner:=Me)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing

            If Not IsNothing(oCurDoc) Then
                oCurDoc.Saved = _SaveFlag
            End If
        End If


    End Sub

    ''' <summary>
    ''' Fax the Guildeline Material
    ''' </summary>
    ''' <param name="oTempDoc"></param>
    ''' <remarks></remarks>
    Private Sub FaxPatientEducation(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)

        mdlFAX.Owner = Me

        If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientMaterials, m_patientID, "", "", txtSelectedTemplates.Text, 0, 0, 0, True, Me) = False Then
            Exit Sub
        End If

        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)

        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, m_patientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, txtSelectedTemplates.Text, clsPrintFAX.enmFAXType.PatientMaterials) = False Then

            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If

        objPrintFAX.Dispose()
        objPrintFAX = Nothing


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
                If oFileDialogWindow.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
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
                For i = 0 To oFiles.Count - 1
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
        AddHandler _PatientStrip.eventSendPortalPatientEducation, AddressOf SendPatientEducationToPortal
        _PatientStrip.ShowDetail(m_patientID, gloUC_PatientStrip.enumFormName.PatientEducation, IsSendEducationMaterial:=False)

        If gblnUSEINTUITINTERFACE = True And gblnIntuitCommunication = True Then

            If Not IsPatientRegisteredOrNotOnPortal Then
                _PatientStrip.ValidateSecureMessageIcon(False)
            End If
        ElseIf gblnPatientPortalEnabled = True And gblnIntuitCommunication = True Then

            If Not IsPatientRegisteredOrNotOnPortal Then
                _PatientStrip.ValidateSecureMessageIcon(False)

            End If
        End If


        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.BringToFront()
        '  _PatientStrip.Padding = New Padding(0, 0, 0, 3)
        'Me.Controls.Add(_PatientStrip)
        pnlMain.Controls.Add(_PatientStrip)
        '  pnlTreeView.BringToFront()
        '  Splitter1.BringToFront()
        ' pnlMain.BringToFront()
        loadToolStrip()

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
            'Start ->dhruv 20100625 -> Check for the file exists
            If System.IO.File.Exists(strFileName) = True Then
                'wdPatientEducation.Open(strFileName)
                '  Dim oWordApp As Wd.Application = Nothing
                gloWord.LoadAndCloseWord.OpenDSO(wdPatientEducation, strFileName, oCurDoc, oWordApp)
            Else
                Return
            End If
            'End->Start ->dhruv 20100625 -> Check for the file exists

            ''//To retrieve the Form fields for the Word document
            If blnGetData Then
                oWord = New clsWordDocument
                objCriteria = New DocCriteria
                '00000498 : Liquid Link
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
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "BeforeDocumentClosed - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    'UpdateVoiceLog(ex.ToString)
                    ex = Nothing
                End Try
                objCriteria.Dispose()
                objCriteria = Nothing
                oWord = Nothing
            Else
                oWord = New clsWordDocument
                oWord.CurDocument = oCurDoc
                oWord.HighlightColor()
                oCurDoc = oWord.CurDocument
                Try
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "BeforeDocumentClosed - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    'UpdateVoiceLog(ex.ToString)
                    ex = Nothing
                End Try
                oWord = Nothing
            End If

            GloUC_AddRefreshDic1.Visible = True
            calltoAddRefreshButtonControl()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "BeforeDocumentClosed - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
            ex = Nothing
        End Try
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
                    '  r.SetRange(Sel.Start, Sel.End + 1)
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
        Catch excp As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, excp.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

                RemoveHandlers()

                frmPatientExam.blnIsHandlers = True
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "BeforeDocumentClosed - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "BeforeDocumentClosed - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
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
            'UpdateVoiceLog(ex.ToString)
            ex = Nothing
        End Try

    End Sub

    Private Sub wdPatientEducation_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdPatientEducation.OnDocumentOpened
        oCurDoc = wdPatientEducation.ActiveDocument
        oWordApp = oCurDoc.Application


        RemoveHandlers()
        AddHandlers()

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
                    'FillGrid()
                    'SetGrid()
                    'GenerateAuditLogForSave()
                Case "Save & Close"
                    Call SaveExamEducations(True)
                    'FillGrid()
                    ' GenerateAuditLogForSave()
                    Me.Close()
                Case "Print"
                    Call Print()
                Case "Send To Portal"
                    Call SendToPortal()
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
                    If _closeClick = True Then
                        Me.CloseClick = True
                    Else
                        Me.CloseClick = False
                    End If


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
        End If
    End Sub

    Private Sub showBiblography()
        pnlBibliographic.Visible = True
        txtBibliography.Text = strBibliography
        txtBibliographyDeveloper.Text = strBibliographyDeveloper
        txtBibliography.Visible = True
        txtBibliographyDeveloper.Visible = True
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
        '  Dim sFileName As String = ExamNewDocumentName

        '  wdPTProtocols.Save(sFileName, True, "", "")
        'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

        'wdPatientEducation.Close()

        'wdTemp = New AxDSOFramer.AxFramerControl

        'Me.Controls.Add(wdTemp)


        'wdTemp.Open(sFileName)
        'oTempDoc = wdTemp.ActiveDocument
        'oTempDoc.ActiveWindow.SetFocus()
        Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
        Dim osenddox As String = String.Empty
        Try
            osenddox = SendWord.MdlSendWord.SendWordDocument(myLoadWord, oCurDoc.FullName, m_patientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        ' Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

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
        'myLoadWord.CloseWordApplication(oTempDoc)
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
    'Private Sub GenerateAuditLogForSave()
    '    Try
    '        If hashNewTemplates IsNot Nothing Then
    '            If hashNewTemplates.Count > 0 Then
    '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Patient Education Added", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '                hashNewTemplates.Clear()
    '            Else
    '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Modify, "Patient Education Modified", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            End If
    '        Else
    '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Modify, "Patient Education Modified", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Close, "AuditLogging - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '    End Try

    'End Sub





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
        End If

        'Set the Start postion of the cursor in documents
        oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
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
                'If Not IsSaving And Not isClosed Then
                '    GenerateAuditLogForSave()
                'End If

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
                'If Not IsSaving And Not isClosed Then
                '    GenerateAuditLogForSave()
                'End If

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
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                ''''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Co-Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "PatientEducation: Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "PatientEducation: Co-Signature Inserted", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "PatientEducation: Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, " Patient Eduction : " & objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Patient Education Preview", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
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

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
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
            'frm.Owner = Me
            frm.ShowDialog(Me)
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

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Patient Education " & objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try
    End Sub

    ''Dhruv 20091214 To add the signature into the Word document
    Public Sub AddSignature(ByVal sImagePath As String) Implements ISignature.AddSignature
        Dim oWord As New clsWordDocument
        If Not IsNothing(oCurDoc) Then
            If File.Exists(sImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(sImagePath)
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        End If
        If Not oWord Is Nothing Then
            oWord = Nothing
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
            If bnlIsFaxOpened = False Then

                Try
                    If Not oCurDoc Is Nothing Then
                        oCurDoc.ActiveWindow.SetFocus()
                        gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                    End If
                Catch ex2 As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex2 = Nothing
                End Try
                ' oCurDoc.ActiveWindow.SetFocus()
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

    Public Sub calltoAddRefreshButtonControl()
        Try
            objWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.VisitID = m_VisitID
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = m_patientID
            objWord.DocumentCriteria = objCriteria
            objWord.CurDocument = wdPatientEducation.ActiveDocument
            objWord.WaitControlPanel = Me.pnlWord
            objCriteria.PrimaryID = m_ExamID
            'objCriteria.VisitID = dtMessage.Tag
            objCriteria.PrimaryID = 0
            Dim dtReferrals As New DateTimePicker()
            dtReferrals.Value = GetVisitdate(m_VisitID)
            GloUC_AddRefreshDic1.CONNECTIONSTRINGs = GetConnectionString()
            GloUC_AddRefreshDic1.OBJWORDs = objWord
            Try
                If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                    If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                        CType(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                    End If
                End If

            Catch

            End Try
            GloUC_AddRefreshDic1.OBJCRITERIAs = objCriteria
            GloUC_AddRefreshDic1.M_PATIENTIDs = m_patientID
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

            GloUC_AddRefreshDic1.OWORDAPPs = oCurDoc.Application
            GloUC_AddRefreshDic1.wdPatientWordDocs = wdPatientEducation

            'Change made to solve memory Leak and word crash issue
            If Not dtReferrals Is Nothing Then
                'dtReferrals.Dispose()
                dtReferrals = Nothing
            End If

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(objCriteria) = False Then
                objCriteria = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'Change made to solve memory Leak and word crash issue
            If Not objWord Is Nothing Then
                objWord = Nothing
            End If

        End Try
    End Sub

    Private Sub Dispose_Object()
        Try
            If Not IsNothing(objCriteria) Then
                objCriteria = Nothing
            End If
            If Not IsNothing(GloUC_AddRefreshDic1) Then
                Me.Controls.Remove(GloUC_AddRefreshDic1)
                GloUC_AddRefreshDic1.Dispose()
                GloUC_AddRefreshDic1 = Nothing
            End If
            'Change made to solve memory Leak and word crash issue
            ''Start

            '03-May-13 Aniket: Resolving Memory Leaks
            If Not oPatientEducation Is Nothing Then
                oPatientEducation.Dispose()
                oPatientEducation = Nothing
            End If



            If Not tlsPatientEducation Is Nothing Then
                '03-May-13 Aniket: Resolving Memory Leaks
                tlsPatientEducation.MyToolStrip.Items.Clear()
                tlsPatientEducation.Dispose()
                tlsPatientEducation = Nothing
            End If

            If Not _PatientStrip Is Nothing Then
                Me.Controls.Remove(_PatientStrip)
                Try
                    RemoveHandler _PatientStrip.eventSendPortalPatientEducation, AddressOf SendPatientEducationToPortal
                Catch ex As Exception

                End Try

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
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If

            'Change made to solve memory Leak and word crash issue
            ''Start

            '03-May-13 Aniket: Resolving Memory Leaks
            If Not oPatientEducation Is Nothing Then
                oPatientEducation.Dispose()
                oPatientEducation = Nothing
            End If



            If Not tlsPatientEducation Is Nothing Then
                '03-May-13 Aniket: Resolving Memory Leaks
                tlsPatientEducation.MyToolStrip.Items.Clear()
                tlsPatientEducation.Dispose()
                tlsPatientEducation = Nothing
            End If

            If Not _PatientStrip Is Nothing Then
                Me.Controls.Remove(_PatientStrip)
                Try
                    RemoveHandler _PatientStrip.eventSendPortalPatientEducation, AddressOf SendPatientEducationToPortal
                Catch ex As Exception

                End Try

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



            If Not IsNothing(ogloVoice) Then

                ogloVoice.Dispose()
                ogloVoice = Nothing
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

    Private Sub txtBibliography_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs)
        Dim p As Process = Process.Start("IExplore.exe", e.LinkText)

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

    'Private Sub ModifyCategorys()

    '    Cursor.Current = Cursors.WaitCursor
    '    Try

    '        If MainMenu.IsAccess(False, m_patientID) = False Then
    '            Exit Sub
    '        End If

    '        '' SUDHIR 20090521 '' CHECK PROVIDER ''
    '        If gblnProviderDisable = True Then
    '            If ShowAssociateProvider(m_patientID, Me) = True Then
    '                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
    '            End If
    '        End If
    '        '' END SUDHIR

    '        'To check exeception related to word
    '        If CheckWordForException() = False Then
    '            Exit Sub
    '        End If

    '        ''check for same Template id
    '        'Dim grdindexx As Int16
    '        'If IsNothing(grdPatienEducation.CurrentCell) Then
    '        '    If grdPatienEducation.Rows.Count > 0 Then
    '        '        Dim i As Integer
    '        '        For i = 0 To grdPatienEducation.Rows.Count - 1
    '        '            If grdPatienEducation.Rows(i).Selected = True Then
    '        '                grdindexx = i
    '        '                Exit For
    '        '            End If
    '        '        Next
    '        '    End If
    '        'Else
    '        '    grdindexx = grdPatienEducation.CurrentCell.RowIndex
    '        'End If


    '        'If _CurrentTemplateID = Convert.ToInt64(grdPatienEducation.Item(0, grdindexx).Value) Then
    '        '    Exit Sub
    '        'End If

    '        Dim result As Int16

    '        'oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, m_ExamID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)

    '        Dim grdEducationID As Int64
    '        '  Dim grdDocumentName As String
    '        Dim grdSource As String
    '        Dim grdResourceCategory As String
    '        Dim grdResourcetype As String
    '        Dim grdDocumentUrl As String

    '        If _ISTreeviewOpen = True Then

    '            If ResourceCategory = 1 Then

    '                result = MessageBox.Show("Do you want to save the changes to Template?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    '                wdPatientEducation.Focus()

    '                If result = Windows.Forms.DialogResult.Cancel Then
    '                    Exit Sub
    '                End If

    '                If (result = Windows.Forms.DialogResult.Yes) Then
    '                    wdPatientEducation.Save(_DocumentName, True, "", "")
    '                    'If Not IsSaving And Not isClosed Then
    '                    '    GenerateAuditLogForSave()
    '                    'End If

    '                    _isEducationChanged = False

    '                    oCurDoc = Nothing
    '                    wdPatientEducation.Close()

    '                    oWord = New clsWordDocument
    '                    _speNotes = CType(oWord.ConvertFiletoBinary(_DocumentName), Object)
    '                    oWord = Nothing

    '                    oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, TempExamID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)
    '                    FillGrid()
    '                    ' _ISTreeviewOpen = False
    '                    ' ModifyCategorys()



    '                ElseIf (result = Windows.Forms.DialogResult.No) Then
    '                    _isEducationChanged = True

    '                End If
    '                result = Nothing    'Change made to solve memory Leak and word crash issue
    '                _ISTreeviewOpen = False

    '            End If

    '        End If


    '        If _FromGrid = True Then

    '            If ResourceCategory = 1 Then


    '                result = MessageBox.Show("Do you want to save the changes to Template?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    '                wdPatientEducation.Focus()

    '                If result = Windows.Forms.DialogResult.Cancel Then
    '                    Exit Sub
    '                End If

    '                If (result = Windows.Forms.DialogResult.Yes) Then
    '                    wdPatientEducation.Save(_DocumentName, True, "", "")
    '                    'If Not IsSaving And Not isClosed Then
    '                    '    GenerateAuditLogForSave()
    '                    'End If

    '                    _isEducationChanged = False

    '                    oCurDoc = Nothing
    '                    wdPatientEducation.Close()

    '                    oWord = New clsWordDocument
    '                    _speNotes = CType(oWord.ConvertFiletoBinary(_DocumentName), Object)
    '                    oWord = Nothing

    '                    oPatientEducation.SaveExamEducation(m_VisitID, m_patientID, TempExamID, _speNotes, _CurrentTemplateName, Source, ResourceCategory, ResourceType, "", strBibliography, strBibliographyDeveloper)
    '                    FillGrid()
    '                    ' _FromGrid = False
    '                    'ModifyCategorys()



    '                ElseIf (result = Windows.Forms.DialogResult.No) Then
    '                    _isEducationChanged = True

    '                End If

    '                result = Nothing    'Change made to solve memory Leak and word crash issue

    '                _FromGrid = False
    '            End If


    '        End If

    '        'If grdPatienEducation.Rows.Count > 0 Then
    '        '    Dim grdIndex As Integer

    '        '    If m_patientID = 0 Then
    '        '        _VisitID = GenerateVisitID(m_patientID) ''gn_VisitID replaced by _VisitID
    '        '    End If
    '        '    If IsNothing(grdPatienEducation.CurrentCell) Then
    '        '        If grdPatienEducation.Rows.Count > 0 Then
    '        '            Dim i As Integer
    '        '            For i = 0 To grdPatienEducation.Rows.Count - 1
    '        '                If grdPatienEducation.Rows(i).Selected = True Then
    '        '                    grdIndex = i
    '        '                    Exit For
    '        '                End If
    '        '            Next
    '        '        End If
    '        '    Else
    '        '        grdIndex = grdPatienEducation.CurrentCell.RowIndex

    '        '    End If
    '        '    ' Dim grdIndex As Integer = grdPatienEducation.CurrentCell.RowIndex
    '        '    grdEducationID = Convert.ToInt64(grdPatienEducation.Item(0, grdIndex).Value)
    '        '    _CurrentTemplateName = grdPatienEducation.Item(3, grdIndex).Value.ToString()
    '        '    grdSource = grdPatienEducation.Item(4, grdIndex).Value.ToString()
    '        '    grdResourceCategory = grdPatienEducation.Item(5, grdIndex).Value.ToString()
    '        '    grdResourcetype = grdPatienEducation.Item(6, grdIndex).Value.ToString()
    '        '    grdDocumentUrl = grdPatienEducation.Item(7, grdIndex).Value.ToString()

    '        '    Source = Convert.ToInt16(grdPatienEducation.Item(8, grdIndex).Value)
    '        '    ResourceCategory = Convert.ToInt16(grdPatienEducation.Item(9, grdIndex).Value)
    '        '    ResourceType = Convert.ToInt16(grdPatienEducation.Item(10, grdIndex).Value)


    '        ' Get other details from education id
    '        Dim dt As DataTable = Nothing
    '        dt = GetEducationMaterialUsingEducationID(grdEducationID)
    '        If Not IsNothing(dt) Then
    '            If dt.Rows.Count > 0 Then
    '                SetSPENotesAndBiblographyOtherMaterial(dt)
    '                TempExamID = Convert.ToInt64(dt.Rows(0)("examID"))
    '            End If
    '        End If


    '        _VisitID = Convert.ToInt64(grdPatienEducation.Item(1, grdIndex).Value)
    '        m_VisitID = _VisitID
    '        _VisitDate = grdPatienEducation.Item(2, grdIndex).Value.ToString

    '        _FromGrid = True

    '        If grdResourceCategory = "Online Library" Then

    '            If pnlWord.Visible = True Then
    '                ShowInfobutton()
    '            End If

    '            HomeUrl = grdDocumentUrl
    '            EducationID = grdEducationID
    '            InfoButtonWebBrowser.Navigate(grdDocumentUrl)
    '            If Source = 1 Then
    '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            ElseIf Source = 2 Then
    '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            ElseIf Source = 3 Then
    '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            End If
    '            Try
    '                AddHandler InfoButtonWebBrowser.DocumentCompleted, AddressOf navigation_complete
    '            Catch ex As Exception

    '            End Try
    '        Else

    '            If pnlInfoBrowser.Visible = True Then
    '                ShowWordDocument()
    '            End If
    '            If grdSource = "Problem List" Then
    '                Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
    '            ElseIf grdSource = "Medication" Then
    '                Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
    '            ElseIf grdSource = "Orders" Then
    '                Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
    '            ElseIf grdSource = "" Then
    '                Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.None
    '            End If
    '            _isOpenforInfobutton = 0

    '            OpenEducationTemplate(grdEducationID, _CurrentTemplateName, _speNotes)
    '            'ShowDocument(grdEducationID, grdDocumentName)

    '        End If



    '        End If
    '    Catch ex As Exception
    '    Finally
    '        Cursor.Current = Cursors.Default
    '    End Try
    'End Sub

    Public DocumentTitle As String = ""
    Public DocumentCompleted As Boolean = False

    'Private Sub navigation_complete(ByVal sender As System.Object, _
    '         ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs)



    '    DocumentCompleted = True

    '    If Not IsNothing(InfoButtonWebBrowser.Document) Then
    '        DocumentTitle = Convert.ToString(InfoButtonWebBrowser.Document.Title)
    '    End If

    '    If IsNothing(DocumentTitle) Or DocumentTitle = "" Then
    '        DocumentTitle = Convert.ToString(InfoButtonWebBrowser.DocumentTitle)
    '    End If
    '    If DocumentTitle = "Health Information for You: MedlinePlus Connect" Or DocumentTitle = "Información de salud para usted: MedlinePlus Connect" Then
    '        Try
    '            For Each element As HtmlElement In InfoButtonWebBrowser.Document.All
    '                Dim HeaderElement() As String
    '                If element.TagName().ToString.ToUpper() = "H2" Then
    '                    HeaderElement = element.InnerText.ToString().Split("[")
    '                    If HeaderElement.Length > 0 Then
    '                        DocumentTitle = HeaderElement(0)
    '                    End If
    '                End If
    '                HeaderElement = Nothing
    '            Next
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End If



    'End Sub









    'Private Sub tls_gloCommunityDashboard_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs)

    '    If e.ClickedItem.Tag = "Home" Then
    '        InfoButtonWebBrowser.Navigate(HomeUrl)
    '    ElseIf e.ClickedItem.Tag = "Refresh" Then
    '        InfoButtonWebBrowser.Navigate(BrowserLink)
    '    ElseIf e.ClickedItem.Tag = "Next" Then
    '        InfoButtonWebBrowser.GoForward()
    '    ElseIf e.ClickedItem.Tag = "Previous" Then
    '        InfoButtonWebBrowser.GoBack()
    '    ElseIf e.ClickedItem.Tag = "Print" Then
    '        'Print Html Document
    '        If DocumentCompleted Then
    '            Me.Cursor = Cursors.WaitCursor
    '            If gblnUseDefaultPrinter Then
    '                InfoButtonWebBrowser.Print()
    '            Else
    '                InfoButtonWebBrowser.ShowPrintDialog()
    '            End If
    '            Me.Cursor = Cursors.Default
    '            If Source = 1 Then
    '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document Print", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            ElseIf Source = 2 Then
    '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document Print", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            ElseIf Source = 3 Then
    '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document Print", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            End If
    '        End If
    '    ElseIf e.ClickedItem.Tag = "Save&Close" Then
    '        If DocumentCompleted Then
    '            'Save Link to Patient Education Table
    '            'If Not isViewed Then
    '            Dim oInfo As New gloEMRGeneralLibrary.clsInfobutton()

    '            Dim oDocUrl As String
    '            If DocumentTitle = "" Then
    '                DocumentTitle = InfoButtonWebBrowser.DocumentTitle
    '            End If

    '            If Not IsNothing(InfoButtonWebBrowser.Document) Then
    '                oDocUrl = InfoButtonWebBrowser.Document.Url.ToString()
    '            Else
    '                oDocUrl = InfoButtonWebBrowser.Url.OriginalString()
    '            End If

    '            oInfo.SavePatientEducation(m_VisitID, m_patientID, 0, Nothing, DocumentTitle, Source, ResourceCategory, ResourceType, oDocUrl, EducationID)
    '            'End If
    '            Me.Close()
    '        End If
    '    ElseIf e.ClickedItem.Tag = "Close" Then
    '        Me.Close()
    '    End If



    'End Sub

    'Private Sub InfoButtonWebBrowser_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs)
    '    BrowserLink = InfoButtonWebBrowser.Url.ToString
    'End Sub

    'Private Sub ts_ViewButtons_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs)
    '    Select Case e.ClickedItem.Tag
    '        Case "Modify"
    '            ''ViewEducation()
    '            ModifyCategorys()
    '        Case "Delete"
    '            Call DeleteCategory()
    '        Case "Refresh"
    '            Call RefreshCategory()

    '    End Select
    'End Sub


    'Private Sub DeleteCategory()
    '    Try
    '        If grdPatienEducation.Rows.Count >= 1 Then

    '            If MainMenu.IsAccess(False, m_patientID) = False Then
    '                Exit Sub
    '            End If

    '            If grdPatienEducation.SelectedRows.Count <= 0 Then
    '                MessageBox.Show("Select record to delete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If

    '            If MessageBox.Show("Are you sure you want to delete this Record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
    '                Dim ID As Long
    '                Dim VisitDate As String
    '                ID = CType(grdPatienEducation.Item(0, grdPatienEducation.CurrentCell.RowIndex).Value, Long)
    '                'VisitDate = CType(grdPatienEducation.Item(3, grdPatienEducation.CurrentCell.RowIndex).Value, String)
    '                oPatientEducation.DeleteEducations(ID, "", m_patientID)
    '                grdPatienEducation.Enabled = False
    '                grdPatienEducation.DataSource = oPatientEducation.GetAllEducations(m_patientID)
    '                grdPatienEducation.Enabled = True

    '                'SortOrder = CType(grdPatienEducation.DataSource, DataView).Sort
    '                'strSearchstring = txtSearch.Text.Trim
    '                'arrcolumnsort = Split(SortOrder, "]")
    '                'If arrcolumnsort.Length > 1 Then
    '                '    strcolumnName = arrcolumnsort.GetValue(0)
    '                '    strsortorder = arrcolumnsort.GetValue(1)
    '                'End If

    '                ' CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
    '                'CustomGridStyle("", "", "")

    '            End If
    '        End If
    '    Catch ex As SqlClient.SqlException
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub RefreshCategory()

    '    txtGridSearch.Text = ""
    '    FillGrid()
    '    If grdPatienEducation.Rows.Count > 0 Then
    '        Dim erg As New DataGridViewCellEventArgs(0, 0)
    '        grdPatienEducation.Rows(0).Selected = True
    '        grdPatienEducation_CellClick(Nothing, erg)
    '    End If

    '    'Try
    '    '    Me.Cursor = Cursors.WaitCursor
    '    '    Call RefreshEducation()
    '    '    If grdPatienEducation.VisibleRowCount > 0 Then
    '    '        grdPatienEducation.CurrentRowIndex = 0
    '    '        grdPatienEducation.Select(0)
    '    '    End If
    '    '    _blnSearch = True
    '    '    Me.Cursor = Cursors.Default
    '    'Catch ex As Exception
    '    '    'MessageBox.Show(ex.Message, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '    Me.Cursor = Cursors.Default
    '    'End Try
    'End Sub

    Private Function CheckISPresentInExamEducation(PatientID As Long, TemplateName As String, VisitID As Long, Source As Int16, ResourceCategory As Int16, ResourceType As Int16) As Boolean
        Return oPatientEducation.CheckISPresentInExamEducation(PatientID, TemplateName, VisitID, Source, ResourceCategory, ResourceType)
    End Function

    Private Function GetEducationMaterialUsingTempalteID(TemplateID As Long) As DataTable
        Return oPatientEducation.GetEducationMaterialUsingTempalteID(TemplateID)
    End Function

    Private Function GetEducationMaterialUsingEducationID(EducationID As Long) As DataTable
        Return oPatientEducation.GetEducationMaterialUsingEducationID(EducationID)
    End Function

    Private Function GetEducationMaterialUsingTempalteID(PatientID As Long, TemplateName As String, VisitID As Long, Source As Integer, ResourceCategory As Integer, ResourceType As Integer) As DataTable
        Return oPatientEducation.GetEducationMaterialUsingTempalteID(PatientID, TemplateName, VisitID, Source, ResourceCategory, ResourceType)
    End Function

    Private Sub SetSPENotesAndBiblographyOtherMaterial(dtTemplate As DataTable)

        Dim strFileName As String
        strFileName = ExamNewDocumentName
        'strFileName = oWord.GenerateFile(CType(dtTemplate.Rows(0)("sDescription"), Object), strFileName)

        _speNotes = CType(dtTemplate.Rows(0)("sDescription"), Object)

        'txtBibliography.Text = Convert.ToString(dtTemplate.Rows(0)("bibliography"))
        ' txtBibliographyDeveloper.Text = Convert.ToString(dtTemplate.Rows(0)("sbDeveloper"))

        strBibliography = Convert.ToString(dtTemplate.Rows(0)("bibliography"))
        strBibliographyDeveloper = Convert.ToString(dtTemplate.Rows(0)("sbDeveloper"))



        Dim EduID As Long = Convert.ToInt64(dtTemplate.Rows(0)("eduID"))
        _EduID = EduID

        TempExamID = Convert.ToInt64(dtTemplate.Rows(0)("examID"))

        ' grdPatienEducation.ClearSelection()



    End Sub

    Private Sub SetEducationIDForGrid(dtTemplate As DataTable)

        Dim EduID As Long = Convert.ToInt64(dtTemplate.Rows(0)("eduID"))
        _EduID = EduID
        TempExamID = Convert.ToInt64(dtTemplate.Rows(0)("examID"))




    End Sub
    Private Sub SetGrid()

        Dim Ispresent As Boolean = CheckISPresentInExamEducation(m_patientID, _CurrentTemplateName, m_VisitID, Source, ResourceCategory, ResourceType)
        Dim dt As DataTable = Nothing
        If Ispresent = True Then

            dt = GetEducationMaterialUsingTempalteID(m_patientID, _CurrentTemplateName, m_VisitID, Source, ResourceCategory, ResourceType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    SetEducationIDForGrid(dt)
                End If
            End If

        End If

        If dt IsNot Nothing Then
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



    Private Sub ShowInfobutton()
        pnlWord.Visible = False
        pnlcombo.Visible = False
        PnlToolStrip.Visible = False

        ' pnlInfoBrowser.Visible = True
        ' pnlBrowserToolStrip.Visible = True
        ' pnlBrowserToolStrip.SendToBack()
    End Sub

    Private Sub ShowWordDocument()
        pnlWord.Visible = True
        pnlcombo.Visible = True
        PnlToolStrip.Visible = True

        ' pnlInfoBrowser.Visible = False
        ' pnlBrowserToolStrip.Visible = False

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
                If dt IsNot Nothing Then
                    dt.Dispose()
                    dt = Nothing
                End If
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
                If dt IsNot Nothing Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End If
        End If


        _ISTreeviewOpen = True

        'Load speNotes in DsoFramer and openEducationTemplate
        OpenEducationTemplate(CType(_ID, Int64), _Name, _speNotes)
        pnlcombo.Visible = True
    End Sub

    Private Function GetSPE(ByVal patienID As Long, ByVal ID As Long, ByVal TName As String, ByVal VsitID As Long, ByVal Surce As Integer, ByVal ResoCategory As Integer, ByVal ResoType As Integer) As DataTable

        Dim Ispresent As Boolean = CheckISPresentInExamEducation(patienID, TName, VsitID, Surce, ResoCategory, ResoType)
        Dim spEE As Object = Nothing
        Dim dt As DataTable = Nothing
        Try
            If Ispresent = False Then
                'Fetch Details form TemplateGallery_Mst
                dt = GetEducationMaterialUsingTempalteID(ID)
            End If

            If Ispresent = True Then
                'Fetch Details form ExamEducation
                dt = GetEducationMaterialUsingTempalteID(patienID, TName, VsitID, Surce, ResoCategory, ResoType)
            End If

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return dt
        Finally
            'If dt IsNot Nothing Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try

    End Function


    Private Sub ShowFromDoubleClick()
        If m_VisitID = 0 Then
            m_VisitID = GenerateVisitID(m_patientID)
        End If


        Dim Ispresent As Boolean = CheckISPresentInExamEducation(m_patientID, _CurrentTemplateName, m_VisitID, Source, ResourceCategory, ResourceType)

        If Ispresent = False Then
            'Fetch Details form TemplateGallery_Mst
            Dim dt As DataTable = Nothing
            dt = GetEducationMaterialUsingTempalteID(_tmpID)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    SetSPENotesAndBiblographyOtherMaterial(dt)
                    'TempExamID = m_ExamID
                End If
                If dt IsNot Nothing Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End If
        End If

        If Ispresent = True Then
            'Fetch Details form ExamEducation
            Dim dt As DataTable = Nothing
            dt = GetEducationMaterialUsingTempalteID(m_patientID, _CurrentTemplateName, m_VisitID, Source, ResourceCategory, ResourceType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    SetSPENotesAndBiblographyOtherMaterial(dt)
                    'TempExamID = m_ExamID
                End If
                If dt IsNot Nothing Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End If
        End If


        _ISTreeviewOpen = True

        'Load speNotes in DsoFramer and openEducationTemplate
        If _tmpID <> 0 Then
            OpenEducationTemplate(CType(_tmpID, Int64), _CurrentTemplateName, _speNotes)
        End If
        pnlcombo.Visible = True

    End Sub

    Private Sub SendPatientEducationToPortal()

        '_speNotes

        If Not IsNothing(_speNotes) Then
            Dim strFileName As String
            strFileName = GenerateAndGetFilePath(_speNotes)

            Dim DocumentName = _CurrentTemplateName & ".docx"

            Dim info As New FileInfo(strFileName)
            ' Get the size of the file in bytes.
            Dim Bytes As Long = info.Length

            Dim dblConvertSize As Double
            dblConvertSize = GetConvertedBytesSize(Bytes)

            If dblConvertSize > 2.0 Then
                _PatientStrip.nPatientEducationID = 0
                MessageBox.Show("File size of '" & DocumentName & "' is " & SetBytes(Bytes) & ". File size should not exceed 2MB.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Call SaveExamEducations(, True)
                _PatientStrip.nPatientEducationID = nPatientEducationID
            End If



        End If

    End Sub

    Function SetBytes(ByVal Bytes) As String

        Dim dblValue As Double
        Dim strarray As String()
        Dim strreturn As String = "0 Bytes"

        Try

            If Bytes >= 1073741824 Then
                dblValue = Bytes / 1024 / 1024 / 1024
                strarray = CStr(dblValue).ToString.Split(".")
                If strarray.Length = 2 Then
                    strreturn = strarray(0) & "." & strarray(1).Substring(0, 2) & " GB"
                Else
                    strreturn = strarray(0) & " GB"
                End If

            ElseIf Bytes >= 1048576 Then
                dblValue = Bytes / 1024 / 1024
                strarray = CStr(dblValue).ToString.Split(".")
                If strarray.Length = 2 Then
                    strreturn = strarray(0) & "." & strarray(1).Substring(0, 2) & " MB"
                Else
                    strreturn = strarray(0) & " MB"
                End If
            End If

            Return strreturn

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            dblValue = Nothing
            strarray = Nothing
            strreturn = Nothing
        End Try

    End Function

    Function GetConvertedBytesSize(ByVal Bytes) As Decimal

        Dim dblValue As Double
        Dim strarray As String()
        Dim strreturn As String = "0"

        Try

            If Bytes >= 1073741824 Then
                dblValue = Bytes / 1024 / 1024 / 1024
                strarray = CStr(dblValue).ToString.Split(".")
                If strarray.Length = 2 Then
                    strreturn = strarray(0) & "." & strarray(1).Substring(0, 2)
                Else
                    strreturn = strarray(0)
                End If

            ElseIf Bytes >= 1048576 Then
                dblValue = Bytes / 1024 / 1024
                strarray = CStr(dblValue).ToString.Split(".")
                If strarray.Length = 2 Then
                    strreturn = strarray(0) & "." & strarray(1).Substring(0, 2)
                Else
                    strreturn = strarray(0)
                End If
            End If

            Return CDbl(strreturn)

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            dblValue = Nothing
            strarray = Nothing
            strreturn = Nothing
        End Try

    End Function

    Private Function GenerateAndGetFilePath(binaryData As Byte()) As String


        Dim strFileName As String
        'Dim stream As MemoryStream = Nothing
        Dim oFile As FileStream = Nothing
        Try
            strFileName = ExamNewDocumentName

            If binaryData IsNot Nothing Then
                'stream = New MemoryStream(binaryData)
                oFile = New FileStream(strFileName, System.IO.FileMode.Create)
                oFile.Write(binaryData, 0, binaryData.Length)
                'stream.WriteTo(oFile)
                oFile.Close()
            Else
                Return ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return ""

        Finally
            If Not IsNothing(oFile) Then
                oFile.Dispose()
                oFile = Nothing

            End If
            'If Not IsNothing(stream) Then
            '    stream.Dispose()
            '    stream = Nothing

            'End If

        End Try

        Return strFileName
    End Function



    Private Sub SendToPortal()


        If gblnUSEINTUITINTERFACE = True And gblnIntuitCommunication = True Then
            If Not IsPatientRegisteredOrNotOnPortal Then
                MessageBox.Show("Patient does not have an active portal account." + System.Environment.NewLine + "No portal message can be sent.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        ElseIf gblnPatientPortalEnabled = True And gblnIntuitCommunication = True Then
            If Not IsPatientRegisteredOrNotOnPortal Then
                MessageBox.Show("Patient does not have an active portal account." + System.Environment.NewLine + "No portal message can be sent.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If

        'Dim myAssembly As System.Reflection.Assembly = Nothing
        'Dim myType As Type = Nothing
        'Dim magicConstructor As ConstructorInfo = Nothing
        'Dim magicClassObject As Object = Nothing
        'Dim Exampleb As MethodInfo = Nothing
        'Dim obj(1) As Object

        Try
            If Not IsNothing(_speNotes) Then
                Dim strFileName As String
                strFileName = GenerateAndGetFilePath(_speNotes)

                Dim DocumentName = _CurrentTemplateName & ".docx"

                Dim info As New FileInfo(strFileName)
                ' Get the size of the file in bytes.
                Dim Bytes As Long = info.Length

                Dim dblConvertSize As Double
                dblConvertSize = GetConvertedBytesSize(Bytes)

                If dblConvertSize > 2.0 Then
                    nPatientEducationID = 0
                    MessageBox.Show("File size of '" & DocumentName & "' is " & SetBytes(Bytes) & ", which is too large." + System.Environment.NewLine + " Message attachments are limited to 2 MB each.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                Else
                    Call SaveExamEducations(, True)
                End If

            End If

            If nPatientEducationID > 0 Then
                If (IsNothing(Me.ParentForm) = False) Then
                    CType(Me.ParentForm, MainMenu).OpenIntuitSendNewMessage(0, nPatientEducationID)
                Else
                    If (IsNothing(Me.myCaller) = False) Then
                        CType(Me.myCaller.ParentForm, MainMenu).OpenIntuitSendNewMessage(0, nPatientEducationID)
                    Else
                        gloGlobal.LoadFromAssembly.OpenIntuitSendMessage("0", nPatientEducationID)
                    End If
                End If

                'myAssembly = System.Reflection.Assembly.LoadFrom("gloEMR.exe")
                'myType = myAssembly.GetType("gloEMR.MainMenu")
                'magicConstructor = myType.GetConstructor(Type.EmptyTypes)
                'magicClassObject = magicConstructor.Invoke(New Object() {})
                'Exampleb = myType.GetMethod("OpenIntuitSendNewMessage")
                'obj(0) = "0"
                'obj(1) = nPatientEducationID
                'Exampleb.Invoke(magicClassObject, obj)

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'If Not IsNothing(Exampleb) Then
            '    Exampleb = Nothing
            'End If
            'If Not IsNothing(magicClassObject) Then
            '    Exampleb = myType.GetMethod("Dispose")
            '    Exampleb.Invoke(magicClassObject, Nothing)
            '    magicClassObject = Nothing
            'End If
            'If Not IsNothing(magicConstructor) Then
            '    magicConstructor = Nothing
            'End If
            'If Not IsNothing(myType) Then
            '    myType = Nothing
            'End If
            'If Not IsNothing(myAssembly) Then
            '    myAssembly = Nothing
            'End If
        End Try

    End Sub

#Region "patient Portal"

    Dim IsPatientRegisteredOrNotOnPortal As Boolean = False

    Private Sub SetPatientRegistrationStatusOnPortal()
        IsPatientRegisteredOrNotOnPortal = False

        Dim clsPatientPortal As New clsgloPatientPortalEmail(GetConnectionString())
        IsPatientRegisteredOrNotOnPortal = clsPatientPortal.IsPatientRegisteredOnPortal(m_patientID, False)


    End Sub


#End Region


End Class
