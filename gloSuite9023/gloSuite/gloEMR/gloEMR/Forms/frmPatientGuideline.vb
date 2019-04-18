Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.IO
Imports System.Runtime.InteropServices
Public Class frmPatientGuideline
    Inherits System.Windows.Forms.Form
    Implements IPatientContext

    Implements ISignature
    Implements IExamChildEvents

    Implements IHotKey

    'instance of frmPatientExam
    Public myCaller As frmPatientExam

    'Instantiate voice class
    Private ogloVoice As ClsVoice
    'implement interface for Voice --supriya 03/06/2009

    Private _VisitID As Long
    ' Private m_patientID As Long
    Private _Type As MaterialType

    Private _TemplateID As Long = 0
    Private _TransID As Long = 0

    Private _PatientID As Long
    Private m_ExamID As Long
    Private _blnIsSinglePatient As Boolean

    Dim oclsPatientGuideLine As New clsPatientGuideLine
    Public WithEvents oCurDoc As Wd.Document
    'Private WithEvents oTempDoc As Wd.Document
    Dim objCriteria As DocCriteria
    Dim ObjWord As clsWordDocument
    ' Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Dim myidx As Int32

    Private bIsNewTemplate As Boolean = True
    Private Arrlist As ArrayList

    '' To Insert Signature
    Public Shared Imagepath As String
    Dim blnTemplateExist As Boolean = False

    Dim oWordApp As Wd.Application

    Dim strAarryOfFields() As Int16

    Private WithEvents _PatientStrip As gloUC_PatientStrip
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlComboMain As System.Windows.Forms.Panel
    Friend WithEvents txtSelectedTemplates As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents wdPatientGuideline As AxDSOFramer.AxFramerControl
    Friend WithEvents wdWordOptimizerDso As AxDSOFramer.AxFramerControl
    Friend WithEvents imgPatientGuidelines As System.Windows.Forms.ImageList
    Dim _arrTemplateID As New ArrayList
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents trvTemplate As System.Windows.Forms.TreeView
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlCombo As System.Windows.Forms.Panel
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents tlsPatientGuideline As WordToolStrip.gloWordToolStrip
    'Events to initialise voice and uninitialise voice
    Private Event ActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Implements mdlgloVoice.IExamChildEvents.ActivateExamChild

    Private Event DeActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Implements mdlgloVoice.IExamChildEvents.DeActivateExamChild
    'variable added by dipak 20091011 to track patient Guidline open from Exam.
    Public Shared IsOpenFormExam As Boolean = False
    Friend WithEvents GloUC_AddRefreshDic1 As gloUserControlLibrary.gloUC_AddRefreshDic
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private blnSignClick As Boolean = False
    Private bnlIsFaxOpened As Boolean

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

    Public Enum MaterialType
        GuideLine = 1
        DiseaseManagement = 2
    End Enum

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal VisitID As Long, ByVal PatientID As Long, ByVal Type As MaterialType, ByVal nExamId As Long)
        MyBase.New()
        _VisitID = VisitID
        _PatientID = PatientID
        '_arrTemplateID = ArrTemplateID
        _Type = Type
        m_ExamID = nExamId

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then

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
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
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
            Try
                If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                    If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                        DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                        GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
                    End If
                End If

            Catch

            End Try
            If (IsNothing(oclsPatientGuideLine) = False) Then

                oclsPatientGuideLine = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlTreeView As System.Windows.Forms.Panel
    'Friend WithEvents WdTemplate As AxDSOFramer.AxFramerControl
    '  Friend WithEvents wdTemp As AxDSOFramer.AxFramerControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientGuideline))
        Me.pnlTreeView = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.trvTemplate = New System.Windows.Forms.TreeView()
        Me.imgPatientGuidelines = New System.Windows.Forms.ImageList(Me.components)
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.wdPatientGuideline = New AxDSOFramer.AxFramerControl()
        Me.wdWordOptimizerDso = New AxDSOFramer.AxFramerControl()
        Me.pnlComboMain = New System.Windows.Forms.Panel()
        Me.GloUC_AddRefreshDic1 = New gloUserControlLibrary.gloUC_AddRefreshDic()
        Me.txtSelectedTemplates = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlCombo = New System.Windows.Forms.Panel()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlTreeView.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.wdPatientGuideline, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wdWordOptimizerDso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlComboMain.SuspendLayout()
        Me.pnlCombo.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTreeView
        '
        Me.pnlTreeView.Controls.Add(Me.Panel1)
        Me.pnlTreeView.Controls.Add(Me.pnlSearch)
        Me.pnlTreeView.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlTreeView.Location = New System.Drawing.Point(0, 27)
        Me.pnlTreeView.Name = "pnlTreeView"
        Me.pnlTreeView.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlTreeView.Size = New System.Drawing.Size(232, 689)
        Me.pnlTreeView.TabIndex = 15
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.trvTemplate)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 29)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(232, 660)
        Me.Panel1.TabIndex = 9
        '
        'trvTemplate
        '
        Me.trvTemplate.BackColor = System.Drawing.Color.White
        Me.trvTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvTemplate.ForeColor = System.Drawing.Color.Black
        Me.trvTemplate.HideSelection = False
        Me.trvTemplate.ImageIndex = 0
        Me.trvTemplate.ImageList = Me.imgPatientGuidelines
        Me.trvTemplate.ItemHeight = 21
        Me.trvTemplate.Location = New System.Drawing.Point(4, 5)
        Me.trvTemplate.Name = "trvTemplate"
        Me.trvTemplate.SelectedImageIndex = 0
        Me.trvTemplate.ShowLines = False
        Me.trvTemplate.Size = New System.Drawing.Size(227, 651)
        Me.trvTemplate.TabIndex = 5
        '
        'imgPatientGuidelines
        '
        Me.imgPatientGuidelines.ImageStream = CType(resources.GetObject("imgPatientGuidelines.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgPatientGuidelines.TransparentColor = System.Drawing.Color.Transparent
        Me.imgPatientGuidelines.Images.SetKeyName(0, "Patient Guideline.ico")
        Me.imgPatientGuidelines.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgPatientGuidelines.Images.SetKeyName(2, "Guidelines.ico")
        Me.imgPatientGuidelines.Images.SetKeyName(3, "Arrow_02.ico")
        Me.imgPatientGuidelines.Images.SetKeyName(4, "arrow_01.ico")
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Location = New System.Drawing.Point(4, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(227, 4)
        Me.Label10.TabIndex = 38
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 656)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(227, 1)
        Me.lbl_BottomBrd.TabIndex = 9
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 656)
        Me.lbl_LeftBrd.TabIndex = 8
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(231, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 656)
        Me.lbl_RightBrd.TabIndex = 7
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(229, 1)
        Me.lbl_TopBrd.TabIndex = 6
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtSearch)
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
        Me.pnlSearch.Location = New System.Drawing.Point(0, 3)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(232, 26)
        Me.pnlSearch.TabIndex = 16
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(32, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(199, 15)
        Me.txtSearch.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(199, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(199, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(4, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 21)
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
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(227, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(227, 1)
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
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(231, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.wdPatientGuideline)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(0, 28)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(754, 658)
        Me.Panel2.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 657)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(752, 1)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 654)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(753, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 654)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(754, 1)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "label1"
        '
        'wdPatientGuideline
        '
        Me.wdPatientGuideline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPatientGuideline.Enabled = True
        Me.wdPatientGuideline.Location = New System.Drawing.Point(0, 3)
        Me.wdPatientGuideline.Name = "wdPatientGuideline"
        Me.wdPatientGuideline.OcxState = CType(resources.GetObject("wdPatientGuideline.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPatientGuideline.Size = New System.Drawing.Size(754, 655)
        Me.wdPatientGuideline.TabIndex = 25
        '

        'wdWordOptimizerDso
        '
        Me.wdWordOptimizerDso.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdWordOptimizerDso.Enabled = True
        Me.wdWordOptimizerDso.Location = New System.Drawing.Point(0, 3)
        Me.wdWordOptimizerDso.Name = "wdWordOptimizerDso"
        Me.wdWordOptimizerDso.OcxState = CType(resources.GetObject("wdWordOptimizerDso.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdWordOptimizerDso.Size = New System.Drawing.Size(754, 655)

        'pnlComboMain
        '
        Me.pnlComboMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlComboMain.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnlComboMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlComboMain.Controls.Add(Me.GloUC_AddRefreshDic1)
        Me.pnlComboMain.Controls.Add(Me.txtSelectedTemplates)
        Me.pnlComboMain.Controls.Add(Me.Label3)
        Me.pnlComboMain.Controls.Add(Me.Label1)
        Me.pnlComboMain.Controls.Add(Me.Label2)
        Me.pnlComboMain.Controls.Add(Me.Label4)
        Me.pnlComboMain.Controls.Add(Me.Label5)
        Me.pnlComboMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlComboMain.Location = New System.Drawing.Point(0, 3)
        Me.pnlComboMain.Name = "pnlComboMain"
        Me.pnlComboMain.Size = New System.Drawing.Size(754, 25)
        Me.pnlComboMain.TabIndex = 24
        '
        'GloUC_AddRefreshDic1
        '
        Me.GloUC_AddRefreshDic1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GloUC_AddRefreshDic1.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_AddRefreshDic1.CONNECTIONSTRINGs = Nothing
        Me.GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing
        Me.GloUC_AddRefreshDic1.Location = New System.Drawing.Point(353, 2)
        Me.GloUC_AddRefreshDic1.M_PATIENTIDs = CType(0, Long)
        Me.GloUC_AddRefreshDic1.Name = "GloUC_AddRefreshDic1"
        Me.GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
        Me.GloUC_AddRefreshDic1.ObjFrom = Nothing
        Me.GloUC_AddRefreshDic1.OBJWORDs = Nothing
        Me.GloUC_AddRefreshDic1.OCURDOCs = Nothing
        Me.GloUC_AddRefreshDic1.OWORDAPPs = Nothing
        Me.GloUC_AddRefreshDic1.Size = New System.Drawing.Size(48, 21)
        Me.GloUC_AddRefreshDic1.TabIndex = 9
        Me.GloUC_AddRefreshDic1.wdPatientWordDocs = Nothing
        '
        'txtSelectedTemplates
        '
        Me.txtSelectedTemplates.BackColor = System.Drawing.Color.White
        Me.txtSelectedTemplates.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSelectedTemplates.ForeColor = System.Drawing.Color.Black
        Me.txtSelectedTemplates.Location = New System.Drawing.Point(129, 1)
        Me.txtSelectedTemplates.Multiline = True
        Me.txtSelectedTemplates.Name = "txtSelectedTemplates"
        Me.txtSelectedTemplates.ReadOnly = True
        Me.txtSelectedTemplates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSelectedTemplates.Size = New System.Drawing.Size(214, 23)
        Me.txtSelectedTemplates.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(128, 23)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Selected Material :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(752, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 24)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(753, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 24)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(754, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(232, 27)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 689)
        Me.Splitter1.TabIndex = 25
        Me.Splitter1.TabStop = False
        '
        'pnlCombo
        '
        Me.pnlCombo.Controls.Add(Me.pnlComboMain)
        Me.pnlCombo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCombo.Location = New System.Drawing.Point(0, 0)
        Me.pnlCombo.Name = "pnlCombo"
        Me.pnlCombo.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlCombo.Size = New System.Drawing.Size(754, 28)
        Me.pnlCombo.TabIndex = 26
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.Label11)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(992, 27)
        Me.pnlToolStrip.TabIndex = 39
        '
        'Label11
        '
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(128, 27)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "pnlToolStrip"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label11.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Panel2)
        Me.pnlMain.Controls.Add(Me.pnlCombo)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(235, 27)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(757, 689)
        Me.pnlMain.TabIndex = 40
        '
        'frmPatientGuideline
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(992, 716)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlTreeView)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientGuideline"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Guideline"
        Me.pnlTreeView.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.wdPatientGuideline, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wdWordOptimizerDso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlComboMain.ResumeLayout(False)
        Me.pnlComboMain.PerformLayout()
        Me.pnlCombo.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    ''' <summary>
    ''' Turnon Microphone when voice enabled and speaker exists
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub frmPatientGuideline_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Try
                If Me.ParentForm IsNot Nothing Then
                    CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.ShowMicroPhone()
                End If
            End If
            For Each myForm As Form In Application.OpenForms
                If (myForm.TopMost) Then
                    myForm.TopMost = False
                End If
            Next
            Me.TopMost = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "ERROR:At frmPatientLetter_Activated \n" & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Me.ParentForm IsNot Nothing Then
                CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
                CType(Me.ParentForm, MainMenu).ActiveDSO = wdPatientGuideline
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Turnoff Microphone when voice enabled and speaker exists
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Private Sub frmPatientGuideline_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If
        Me.TopMost = False
        'Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub frmPatientGuideline_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                TurnoffMicrophone()

            End If
        End If
        If (IsNothing(Me.ParentForm) = False) Then
            CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
        End If
        MyMDIParent.MdiExamChildDeActivate(Me)

        If (IsNothing(mdlFAX.Owner) = False) Then
            mdlFAX.Owner = Nothing
        End If
    End Sub

    Private Sub frmPatientGuideline_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        wdPatientGuideline.Close()
        If IsOpenFormExam = True Then
            Me.Cursor = Cursors.WaitCursor
            myCaller.UnprotectFrmPdEdu()
            myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.PatientGuideline)
            'Dim frm As MainMenu
            'frm = MyMDIParent
            'frm.MenuStrip1.Enabled = True
            CType(myCaller.ParentForm, MainMenu).MenuStrip1.Enabled = True
            IsOpenFormExam = False
            Me.Cursor = Cursors.Default
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub frmPatientGuideline_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim dt As DataTable
            Dim i As Integer

            ''''' to Get all Templates(ID & Name) of 
            dt = oclsPatientGuideLine.FillTemplates(_Type)

            Dim RootNode As New myTreeNode("Patient Guidelines", -1)
            RootNode.ImageIndex = 0
            RootNode.SelectedImageIndex = 0
            trvTemplate.Nodes.Add(RootNode)

            If Not IsNothing(dt) Then
                ''''' Fill Templates in Tree View
                For i = 0 To dt.Rows.Count - 1
                    Dim MyNode As New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0))
                    MyNode.ImageIndex = 1
                    MyNode.SelectedImageIndex = 1
                    trvTemplate.Nodes.Add(MyNode)
                Next
            End If

            trvTemplate.ExpandAll()
            '' Load Patient Strip Details
            loadPatientStrip()
            ''Fetch Guideline for update
            Call Fill_GuidelinesForUpdate()

            'Update to Hide AddRefresh Button
            If oCurDoc Is Nothing Then
                ''// If there is no guideline associated then Load New document
                ' LoadNewDocument()
                GloUC_AddRefreshDic1.Visible = False
            Else
                GloUC_AddRefreshDic1.Visible = True
            End If

            If _Type = MaterialType.GuideLine Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Load, "Patient Guidline Opened", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Load, "Patient Guidline Opened", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Patient Guidline Opened", gstrLoginName, gstrClientMachineName, gnPatientID)
            Else
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Load, "Patient Disease Management Opened", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Load, "Patient Disease Management Opened", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Patient Disease Management Opened", gstrLoginName, gstrClientMachineName, gnPatientID)
            End If
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                InitializeVoiceObject()

            End If
            MyMDIParent.MdiExamChildActivate(Me)
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.ShowMicroPhone()
                End If
            End If
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            Try
                wdWordOptimizerDso.CreateNew("Word.Document")
            Catch ex As Exception

            End Try


            'Added Code To Get Fucntionality for Add refresh Button
            calltoAddRefreshButtonControl()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub calltoAddRefreshButtonControl()
        Try
            ObjWord = New clsWordDocument
            ObjWord.WaitControlPanel = Me.Panel2
            objCriteria = New DocCriteria
            objCriteria.PatientID = _PatientID
            Dim dtLetterdate As New DateTimePicker()
            dtLetterdate.Format = DateTimePickerFormat.Custom
            dtLetterdate.CustomFormat = Format("MM/dd/yyyy hh:mm tt")
            dtLetterdate.Value = Now

            Try
                objCriteria.VisitID = GenerateVisitID(dtLetterdate.Value, _PatientID)
            Catch ex As Exception

            End Try

            objCriteria.DocCategory = enumDocCategory.Exam ''added for bugid 81159
            objCriteria.PrimaryID = m_ExamID  ''added for bugid 81159
            ObjWord.DocumentCriteria = objCriteria
            GloUC_AddRefreshDic1.CONNECTIONSTRINGs = GetConnectionString()
            GloUC_AddRefreshDic1.OBJWORDs = ObjWord
            Try
                If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                    If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                        DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                        GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
                    End If
                End If

            Catch

            End Try
            GloUC_AddRefreshDic1.OBJCRITERIAs = objCriteria
            GloUC_AddRefreshDic1.M_PATIENTIDs = _PatientID
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

            GloUC_AddRefreshDic1.DTLETTERDATEs = dtLetterdate
            GloUC_AddRefreshDic1.dtLetterAllocated = True

            GloUC_AddRefreshDic1.OWORDAPPs = oWordApp
            GloUC_AddRefreshDic1.wdPatientWordDocs = wdPatientGuideline
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    '''' <summary>
    ''''  To Fill Patient Guidelines for update 
    '''' </summary>
    '''' <remarks></remarks>

    Private Sub Fill_GuidelinesForUpdate()
        Dim dt As New DataTable
        dt = oclsPatientGuideLine.FetchTemplateforGuideLine(_PatientID, _VisitID, _Type)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then

                ObjWord = New clsWordDocument
                Dim strFileName As String
                strFileName = ExamNewDocumentName
                ObjWord.GenerateFile(dt.Rows(0)("sTemplate"), strFileName)
                ObjWord = Nothing
                LoadWordUserControl(strFileName, False)
                oCurDoc.ActiveWindow.Application.ActiveDocument.SpellingChecked = True
                oCurDoc.ActiveWindow.Application.ActiveDocument.ShowGrammaticalErrors = False
                oCurDoc.ActiveWindow.View.WrapToWindow = True
                ''''' Statement to Go start of Selelcted Wd Document
                oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
                oCurDoc.ActiveWindow.SetFocus()
                oCurDoc.Application.Selection.MoveRight(1, 1)
                oCurDoc.Application.Selection.MoveLeft(1, 1)

                '' Name of the Patient Guideline Given 
                txtSelectedTemplates.Text = dt.Rows(0)("sTemplateName")
                _TransID = dt.Rows(0)("nID")

                blnTemplateExist = True
                bIsNewTemplate = False
            End If
        End If

    End Sub

    Private Sub Fill_TemplateGallery(ByVal TemplateID As Long)
        Try


            Dim strFileName As String
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Template
            objCriteria.PrimaryID = TemplateID

            ObjWord.DocumentCriteria = objCriteria
            ''//Retrieving the Patient Education from DB and Save it as Physical File
            strFileName = ObjWord.RetrieveDocumentFile()
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
            If (IsNothing(strFileName) = False) Then
                If strFileName <> "" Then
                    ''//Open Template for processing in user Ctrl
                    LoadWordUserControl(strFileName, True)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    '''' <summary>
    '''' Insert the Concerned template in the document
    '''' </summary>
    '''' <param name="TemplateID"></param>
    '''' <remarks></remarks>

    Private Sub InsertTemplate(ByVal TemplateID As Long)


        Try
            If Not oCurDoc Is Nothing Then



                Dim strFileName As String
                Dim ObjWord As New clsWordDocument
                Dim objCriteria As DocCriteria
                objCriteria = New DocCriteria
                objCriteria.DocCategory = enumDocCategory.Template

                objCriteria.PrimaryID = TemplateID
                ObjWord.DocumentCriteria = objCriteria
                ''//Get the Template from DB as physical document
                strFileName = ObjWord.RetrieveDocumentFile()
                objCriteria.Dispose()
                objCriteria = Nothing
                ObjWord = Nothing


               

                'wdTemp.Open(strFileName)
                'oTempDoc = wdTemp.ActiveDocument
                If (IsNothing(strFileName) = False) Then

                    Dim myLoadWord As gloWord.LoadAndCloseWord = GetMyLoadWordApplication()
                    Try
                        Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(strFileName)

                        ''// Replace Form Fields with Data
                        ObjWord = New clsWordDocument
                        objCriteria = New DocCriteria
                        objCriteria.DocCategory = enumDocCategory.Exam
                        objCriteria.PatientID = _PatientID
                        objCriteria.VisitID = _VisitID
                        objCriteria.PrimaryID = m_ExamID  ''added for bugid 81159
                        ObjWord.DocumentCriteria = objCriteria
                        ObjWord.CurDocument = oTempDoc

                        ObjWord.GetFormFieldData(enumDocType.None)

                        oTempDoc = ObjWord.CurDocument
                        objCriteria.Dispose()
                        objCriteria = Nothing
                        ObjWord = Nothing

                        ''//Save Document & Dispose the Temp Control
                        ' wdTemp.Save(strFileName, True, "", "")
                        oTempDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                        'wdTemp.Close()
                        'Me.Controls.Remove(wdTemp)
                        'wdTemp.Dispose()
                        myLoadWord.CloseWordOnly(oTempDoc)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    'myLoadWord.CloseApplicationOnly()
                    'myLoadWord = Nothing

                    oCurDoc.ActiveWindow.SetFocus()
                    ''''' Statement to Go end of Selelcted Wd Document
                    oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)

                    ''// Statement to Append Document from Path strFileName to Activate Wd window
                    oCurDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                    oCurDoc.Application.Selection.InsertFile(strFileName)
                End If

                oCurDoc.ActiveWindow.SetFocus()


            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
    End Sub

    Private Sub trvTemplate_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvTemplate.DoubleClick
        Try
            If trvTemplate.SelectedNode.Index = 0 Then
                Exit Sub
            End If
            If IsNothing(trvTemplate.SelectedNode) = True Then
                Exit Sub
            End If

            'If trvTemplate.SelectedNode Is trvTemplate.Nodes(0) Then
            '    Exit Sub
            'End If
            ' If Not oCurDoc Is Nothing Then
            Dim thisNode As myTreeNode = CType(trvTemplate.SelectedNode, myTreeNode)
            If thisNode.Key > 0 Then
                If blnTemplateExist = False Then
                    Call Fill_TemplateGallery(thisNode.Key)
                    blnTemplateExist = True
                Else
                    Call InsertTemplate(thisNode.Key)
                End If

                oCurDoc.ActiveWindow.Application.ActiveDocument.SpellingChecked = True
                oCurDoc.ActiveWindow.Application.ActiveDocument.ShowGrammaticalErrors = False
                oCurDoc.ActiveWindow.View.WrapToWindow = True
                ''''' Statement to Go start of Selelcted Wd Document
                oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
                oCurDoc.ActiveWindow.Activate()
                oCurDoc.Application.Selection.MoveRight(1, 1)
                oCurDoc.Application.Selection.MoveLeft(1, 1)

                If Trim(txtSelectedTemplates.Text) = "" Then
                    txtSelectedTemplates.Text = trvTemplate.SelectedNode.Text
                Else
                    txtSelectedTemplates.Text = txtSelectedTemplates.Text & ", " & trvTemplate.SelectedNode.Text
                End If

                oCurDoc.Saved = False
                If oCurDoc Is Nothing Then
                    GloUC_AddRefreshDic1.Visible = False
                Else
                    GloUC_AddRefreshDic1.Visible = True
                End If
            Else
                wdPatientGuideline.Close()
            End If
            ' End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' to insert user's signature
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
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
            objCriteria.PatientID = _PatientID
            'end modification
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID
            objWord.DocumentCriteria = objCriteria

            Imagepath = objWord.getData_FromDB("User_MST.imgSignature", "Provider Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
            Imagepath = Mid(Imagepath, 1, Len(Imagepath) - 2)
            If Imagepath = "" Then
                MessageBox.Show("Current user has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
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
                'Dim clsExam As New clsPatientExams
                'clsExam.Dispose()
                'clsExam = Nothing
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Patient Guideline", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try

    End Sub

    '''' <summary>
    '''' Insert Resppective provider Signature
    '''' </summary>
    '''' <remarks></remarks>
    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        'Developer:Yatin N. Bhagat
        'Date:01/31/2012
        'Bug ID/PRD Name/Salesforce Case:Provider Signature Format Case
        'Reason: Comman Fucntionality is added 
        Try

            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            'dtMessage.Tag = GenerateVisitID(dtMessage.Value, m_PatientID)
            Dim objWord As New clsWordDocument
            Dim oclsProvider As New clsProvider
            Dim clsExam As New clsPatientExams
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, _PatientID, _VisitID, blnSignClick)
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
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
            'Dispose object by mitesh
            If Not IsNothing(clsExam) Then
                clsExam.Dispose()
                clsExam = Nothing
            End If
            If Not IsNothing(oclsProvider) Then
                oclsProvider.Dispose()
                oclsProvider = Nothing
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try

    End Sub

    '' chetan added on 25-oct-2010 for Strike Through
    Private Sub InsertStrike()
        Try

            Dim strThrough As String
            If Not IsNothing(oCurDoc) Then
                oCurDoc.ActiveWindow.SetFocus()
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
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SavePatientMaterial(Optional ByVal IsClose As Boolean = False)
        'Dim strFileName As String
        'Dim isExceptionWhileCopingFile As Boolean = False
        'strFileName = ExamNewDocumentName
        'Try
        '    'Updated Code For Bug 13523
        '    If Not IsNothing(wdPatientGuideline.ActiveDocument) Then
        '        oCurDoc.ActiveWindow.SetFocus()
        '        ' oCurDoc.Save()
        '        ' wdPatientGuideline.Save()
        '        gloWord.LoadAndCloseWord.SaveDSO(wdPatientGuideline, oCurDoc, oWordApp)
        '    End If
        '    FileSystem.FileCopy(oCurDoc.FullName, strFileName)

        '    '' save the document for further processing
        '    '  wdPatientGuideline.Save(strFileName, True, "", "")
        'Catch ex As Exception
        '    'UpdateLog("ERROR WHILE COPING FILE IN MESSAGE :" & ex.ToString())
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    isExceptionWhileCopingFile = True
        '    ex = Nothing
        'End Try
        'If (isExceptionWhileCopingFile) Then
        '    If Not IsNothing(oCurDoc) Then
        '        oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
        '        wdPatientGuideline.Close()
        '    End If
        'End If
        Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdPatientGuideline, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, IsClose)

        Dim myBinaray As Object = Nothing
        If (IsNothing(myByte) = False) Then
            myBinaray = CType(myByte, Object)
        End If
        oclsPatientGuideLine.SaveExamGuidelineBytes(_TransID, _VisitID, _PatientID, myBinaray, txtSelectedTemplates.Text, _Type)
        If bIsNewTemplate Then
            If _Type = MaterialType.DiseaseManagement Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Disease Management Material Added", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf _Type = MaterialType.GuideLine Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Material Added", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Else
            If _Type = MaterialType.DiseaseManagement Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Disease Management Material Modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf _Type = MaterialType.GuideLine Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Material Modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        End If
        If IsClose Then
            'If (isExceptionWhileCopingFile) Then
            '    LoadWordUserControl(strFileName, False)

            If (IsNothing(oCurDoc) = False) Then
                Try
                    Marshal.ReleaseComObject(oCurDoc)
                Catch ex As Exception


                End Try
                oCurDoc = Nothing

            End If
        Else
            'End If
            If (IsNothing(oCurDoc) = False) Then
                oCurDoc.Saved = True
            End If
            'oCurDoc.Saved = True

        End If

        wdPatientGuideline.Focus()

        'If IO.File.Exists(strFileName) Then
        '    Try
        '        IO.File.Delete(strFileName)
        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '        ex = Nothing
        '    End Try
        'End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            If Trim(txtSearch.Text) <> "" Then
                If trvTemplate.GetNodeCount(False) > 0 Then
                    Dim mychildnode As TreeNode
                    'child node collection

                    For Each mychildnode In trvTemplate.Nodes
                        Dim str As String
                        str = UCase(Trim(mychildnode.Text))
                        If Mid(str, 1, Len(Trim(txtSearch.Text))) = UCase(Trim(txtSearch.Text)) Then
                            '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                            If Not IsNothing(trvTemplate.SelectedNode) Then
                                If Not IsNothing(trvTemplate.SelectedNode.LastNode) Then
                                    trvTemplate.SelectedNode = trvTemplate.SelectedNode.LastNode
                                End If
                            End If
                            '*************
                            trvTemplate.SelectedNode = mychildnode
                            txtSearch.Focus()
                            Exit Sub
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If trvTemplate.GetNodeCount(False) > 0 Then
            If (e.KeyChar = ChrW(13)) Then
                trvTemplate.Select()

            End If
        End If
    End Sub

    Public WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            Imagepath = Value
        End Set
    End Property

    '''' <summary>
    '''' Capture signature and insert in the document
    '''' </summary>
    '''' <remarks></remarks>
    Public Sub InsertSignature()
        Try
            Imagepath = ""
            ''show signature pad Form to the user to capture the signature
            Dim frm As New FrmSignature
            frm.Owner = Me
            '   frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.ShowDialog(frm.Parent)
            frm.Dispose()

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        End If
    End Sub
    Private Sub frmPatientGuideline_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            If _Type = MaterialType.GuideLine Then

                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Close, "Patient Guidline Closed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Close, "Patient Guideline Closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Patient Guidline Closed", gstrLoginName, gstrClientMachineName, gnPatientID)

            Else
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Close, "Patient Disease Management Closed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Close, "Patient Disease Management Closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Patient Disease Management Closed", gstrLoginName, gstrClientMachineName, gnPatientID)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    '''' <summary>
    '''' Load Patient details
    '''' </summary>
    '''' <remarks></remarks>
    Private Sub loadPatientStrip()
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.PatientGuideline)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(0, 0, 0, 0)
        pnlMain.Controls.Add(_PatientStrip)
        loadToolStrip()

    End Sub


    Private Sub loadToolStrip()
        If Not tlsPatientGuideline Is Nothing Then
            tlsPatientGuideline.Dispose()
        End If

        tlsPatientGuideline = New WordToolStrip.gloWordToolStrip
        tlsPatientGuideline.Dock = DockStyle.Top
        tlsPatientGuideline.ConnectionString = GetConnectionString()
        tlsPatientGuideline.UserID = gnLoginID
        tlsPatientGuideline.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider
        tlsPatientGuideline.ptProvider = oclsProvider.GetPatientProviderName(_PatientID)
        tlsPatientGuideline.ptProviderId = oclsProvider.GetPatientProvider(_PatientID)
        oclsProvider.Dispose()
        oclsProvider = Nothing

        tlsPatientGuideline.IsCoSignEnabled = gblnCoSignFlag
        tlsPatientGuideline.FormType = WordToolStrip.enumControlType.PatientGuidelines
        Me.pnlToolStrip.Size = New System.Drawing.Size(940, 56)
        Me.pnlToolStrip.Controls.Add(tlsPatientGuideline)
        pnlToolStrip.SendToBack()
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsPatientGuideline
                ShowMicroPhone()
            End If
        End If
        If gblnAssociatedProviderSignature Then
            tlsPatientGuideline.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            tlsPatientGuideline.MyToolStrip.ButtonsToHide.Remove(tlsPatientGuideline.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        Else
            tlsPatientGuideline.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsPatientGuideline.MyToolStrip.ButtonsToHide.Contains(tlsPatientGuideline.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsPatientGuideline.MyToolStrip.ButtonsToHide.Add(tlsPatientGuideline.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If
        End If

    End Sub

    ''sanjog 20101015 
    Private Function AddChildMenu() As DataTable
        Try
            Dim oProvider As New clsProvider
            Dim rslt As Boolean
            rslt = oProvider.CheckSignDelegateStatus()
            If rslt Then
                Dim dt As New DataTable
                dt = oProvider.GetAllAssignProviders(gnLoginID)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function
    Private Sub tlsPatientGuideline_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsPatientGuideline.ToolStripButtonClick
        Try
            If IsNothing(oCurDoc) = False Then
                InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub InsertCoSignature()
        Try
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = _PatientID
            'end modification 
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID '' For inserting coSignature
            ObjWord.DocumentCriteria = objCriteria

            Imagepath = ObjWord.getData_FromDB("User_MST.imgSignature", "Co-Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
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
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Load, "Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Load, "Co-Signature Inserted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Co-Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            objErr = Nothing
        End Try
    End Sub


    Private Sub Print()
        If Not oCurDoc Is Nothing Then
            GeneratePrintFaxDocument()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Material '" & txtSelectedTemplates.Text & "' Printed.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Material '" & txtSelectedTemplates.Text & "' Printed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Patient Material '" & txtSelectedTemplates.Text & "' Printed.", gstrLoginName, gstrClientMachineName, gnPatientID)
        End If
    End Sub

    '''' <summary>
    '''' To print or fax the Patient Guideline
    '''' </summary>
    '''' <param name="IsPrintFlag">Flag to be set False for Fax, by default is true for print</param>
    '''' <remarks></remarks>
    Private Sub GeneratePrintFaxDocument(Optional ByVal IsPrintFlag As Boolean = True)
        If Not oCurDoc Is Nothing Then
            Dim PageNo As Integer = 0
            Dim totalPages As Integer = 0
            Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
            Dim Missing As Object = System.Reflection.Missing.Value

            Dim _SaveFlag As Boolean = False
            If oCurDoc.Saved Then
                _SaveFlag = True
            End If
            'Dim sFileName As String = ExamNewDocumentName
            ''//Save the Document and close
            'wdPatientGuideline.Save(sFileName, True, "", "")
            'Ashish added on 31st October 
            'to prevent screen from refreshing
            'Dim wordRefresh As New WordRefresh()
            'Dim WDocViewType As Wd.WdViewType
            'If IsPrintFlag Then
            '    'Ashish added on 1st November
            '    'to prevent screen from refreshing
            '    'WDocViewType = oCurDoc.ActiveWindow.View.Type
            '    'wordRefresh.OptimizePerformance(False, oCurDoc, 0)
            'End If
            If IsNothing(wdPatientGuideline) = False AndAlso IsNothing(oWordApp) = False Then
                Try
                    gloWord.LoadAndCloseWord.SaveDSO(wdPatientGuideline, oCurDoc, oWordApp)
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
                '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '    Try
                '        oCurDoc.Save()
                '    Catch ex1 As Exception

                '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                '    End Try
                'End Try
                '    oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                'wdPatientGuideline.Close()

                'If Not File.Exists(sFileName) Then
                '    Try
                '        File.Copy(oCurDoc.FullName, sFileName)
                '    Catch ex As Exception
                '        MessageBox.Show("Error while printing or faxing. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '        ex = Nothing
                '    End Try
                'End If

                'If Not File.Exists(sFileName) Then
                '    Exit Sub
                'End If




                ' ''Open Template for processing in Temp user Ctrl
                'wdTemp.Open(sFileName)
                Dim myLoadWord As gloWord.LoadAndCloseWord = GetMyLoadWordApplication()
                'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)
                Try
                    PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, _PatientID, AddressOf FaxPatientGuideline, totalPages, PageNo:=PageNo, iOwner:=Me)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                ''oTempDoc = wdTemp.ActiveDocument
                'If IsPrintFlag Then
                '    'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                '    '    oTempDoc.Unprotect()
                '    'End If
                '    Dim oPrint As New clsPrintFAX
                '    ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                '    'oPrint.PrintDoc(oTempDoc, gnPatientID)
                '    oPrint.PrintDoc(oTempDoc, _PatientID)
                '    oPrint.Dispose()
                '    'end modification
                '    oPrint = Nothing
                'Else
                '    Call FaxPatientGuideline(myLoadWord, oTempDoc)
                'End If

                'wdTemp.Close()
                'Me.Controls.Remove(wdTemp)
                'wdTemp.Dispose()
                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
                If Not IsNothing(oCurDoc) Then
                    oCurDoc.Saved = _SaveFlag
                End If

                'LoadWordUserControl(sFileName, False)
                'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                'oCurDoc.Saved = _SaveFlag

                'If IsPrintFlag Then
                '    'Ashish added on 1st November
                '    'to prevent screen from refreshing
                '    'WordRefresh.OptimizePerformance(True, oCurDoc, WDocViewType)
                '    'WDocViewType = Nothing
                'End If
            End If
        End If
    End Sub

    '''' <summary>
    '''' Fax the Guildeline Material
    '''' </summary>
    '''' <param name="oTempDoc"></param>
    '''' <remarks></remarks>
    Private Sub FaxPatientGuideline(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)
        mdlFAX.Owner = Me
        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientMaterials, gnPatientID, "", "", txtSelectedTemplates.Text, 0, 0, 0) = False Then
        '    Exit Sub
        'End If
        If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientMaterials, _PatientID, "", "", txtSelectedTemplates.Text, 0, 0, 0, True, Me) = False Then
            Exit Sub
        End If
        'end modification
        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

        ''Unprotect the document

        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If

        'Check the FAX Cover Page is enabled or not.
        'If the FAX Cover Page is enabled then Delete the Page Header from Exam
        'If gblnFAXCoverPage Then
        '    'To Delete Header
        '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Delete, "Deleting Patient Guideline Page Header", gloAuditTrail.ActivityOutCome.Success)
        '    'UpdateLog("Deleting Patient Guideline Page Header")
        '    Try

        '        If oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
        '            oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
        '        End If
        '        oTempDoc.Activate()
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryHeader
        '        If oTempDoc.Application.Selection.HeaderFooter.IsHeader Then
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Select()
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Delete()
        '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Delete, "Patient Guideline Page Header deleted", gloAuditTrail.ActivityOutCome.Success)
        '            'UpdateLog("Patient Guideline Page Header deleted")
        '        End If

        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Delete, "Error Deleting Patient Guideline Page Header - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        '        'UpdateVoiceLog("Error Deleting Patient Guideline Page Header - " & ex.ToString)
        '    Finally
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        '    End Try
        'End If

        'Send the document for Printing i.e. to generate the TIFF File
        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'If objPrintFAX.FAXDocument(oTempDoc, gnPatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, txtSelectedTemplates.Text, clsPrintFAX.enmFAXType.PatientMaterials) = False Then
        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, _PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, txtSelectedTemplates.Text, clsPrintFAX.enmFAXType.PatientMaterials) = False Then
            'end modification
            'TIFF File has not been created
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        objPrintFAX.Dispose()
        objPrintFAX = Nothing

    End Sub

    '''' <summary>
    '''' Undo changes in the Word Document
    '''' </summary>
    '''' <remarks></remarks>
    Private Sub UnDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    '''' <summary>
    '''' Redo Changes in the Word document
    '''' </summary>
    '''' <remarks></remarks>
    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    '''' <summary>
    '''' Scan the Document for insert image or file
    '''' </summary>
    '''' <param name="nInsertScan"></param>
    '''' <remarks></remarks>
    Private Sub ImportDocument(ByVal nInsertScan As Int16)
        'Insert File - 1
        'Scan Images - 2
        Try
            If nInsertScan = 1 Then
                Dim oFileDialogWindow As New System.Windows.Forms.OpenFileDialog
                oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf"
                ' oFileDialogWindow.Filter = "Text Files|*.txt|Wd Documents|*.doc|Rich Text Format|*.rtf"
                '//oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Wd Documents (*.doc)|*.doc|Rich Text Format (*.rtf)|*.rtf"
                oFileDialogWindow.FilterIndex = 3
                oFileDialogWindow.Title = "Insert External Documents"
                oFileDialogWindow.Multiselect = False
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)
                    If oFile.Extension.ToUpper = UCase(".Doc") Or oFile.Extension.ToUpper = UCase(".Docx") Or oFile.Extension.ToUpper = UCase(".txt") Or oFile.Extension.ToUpper = UCase(".rtf") Then
                        'Set focus to Wd object
                        oCurDoc.ActiveWindow.SetFocus()

                        'Insert file in Wd dobject
                        oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                    End If
                End If
                oFileDialogWindow.Dispose()
                oFileDialogWindow = Nothing
            ElseIf nInsertScan = 2 Then
                'Dim frmObj As New frmDMS_ScannedDocumentEvent_TwainPro
                'Dim _Files As New Collection
                'frmObj.blnDMSScan = False
                'frmObj.ShowDialog(Me)
                '_Files = frmObj._NonDMSFileCollection


                Dim oFiles As New ArrayList()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()

                'Commented BY Rahul Patel on 26-10-2010
                'gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'Added by Rahul Patel on 26-10-2010
                'For changing the DMS Hybrid database change.
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'End of code added by Rahul Patel on 26-10-2010.

                'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'oEDocument.ShowEScannerForImages(gnPatientID, oFiles)
                oEDocument.ShowEScannerForImages(_PatientID, oFiles)
                'end modification
                oEDocument.Dispose()

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
                        'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=oFiles.Item(i), LinkToFile:=False, SaveWithDocument:=True)
                        '' END SUDHIR ''
                        'ResolvedBug :41969
                        If oCurDoc.Application.ActiveDocument.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdNoProtection Then
                            Try
                                oCurDoc.Application.Selection.EndKey()
                                oCurDoc.Application.Selection.InsertBreak()
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                ex = Nothing
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

                ''frmObj = Nothing
                i = Nothing
                ''_Files = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
    End Sub

    '''' <summary>
    '''' Load the  Word Document in the Dso control
    '''' </summary>
    '''' <param name="strFileName"></param>
    '''' <param name="blnGetData"></param>
    '''' <remarks></remarks>
    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        Try


            '  wdPatientGuideline.Open(strFileName)
            ' Dim oWordApp As Wd.Application = Nothing

            Dim strError = gloWord.LoadAndCloseWord.OpenDSO(wdPatientGuideline, strFileName, oCurDoc, oWordApp)
            If (strError <> String.Empty) Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, strError, gloAuditTrail.ActivityOutCome.Failure)
            Else


                ''//To retrieve the Form fields for the Word document
                If blnGetData Then
                    ObjWord = New clsWordDocument
                    objCriteria = New DocCriteria
                    objCriteria.DocCategory = enumDocCategory.Exam
                    objCriteria.PatientID = _PatientID
                    objCriteria.VisitID = _VisitID
                    objCriteria.PrimaryID = m_ExamID  ''added for bugid 81159
                    ObjWord.DocumentCriteria = objCriteria
                    ObjWord.CurDocument = oCurDoc
                    ''//Replace Form fields with Concerned data from DB
                    ObjWord.GetFormFieldData(enumDocType.None)
                    oCurDoc = ObjWord.CurDocument
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                    objCriteria.Dispose()
                    objCriteria = Nothing
                    ObjWord = Nothing
                Else
                    ObjWord = New clsWordDocument
                    ObjWord.CurDocument = oCurDoc
                    ObjWord.HighlightColor()
                    oCurDoc = ObjWord.CurDocument
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                    ObjWord = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    '''' <summary>
    '''' To implemt the Dropdown and check Box selection change event
    '''' </summary>
    '''' <param name="Sel"></param>
    '''' <remarks></remarks>
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
                        '   Dim om As Object = System.Reflection.Missing.Value
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
                End If
            End If
        Catch excp As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, excp.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            excp = Nothing
        End Try
    End Sub

    '''' <summary>
    '''' To raise the click event for drop down list
    '''' </summary>
    '''' <param name="btn"></param>
    '''' <param name="Cancel"></param>
    '''' <remarks></remarks>
    Private Sub btn_Click(ByVal btn As oOffice.CommandBarButton, ByRef Cancel As Boolean)
        myidx = btn.Index
    End Sub

    '''' <summary>
    '''' To Load the Word User Control with New Document
    '''' </summary>
    '''' <remarks></remarks>
    Private Sub LoadNewDocument()
        '  ReInitUserControl()
        ''// To open the new document in the Word user control
        wdPatientGuideline.CreateNew("Word.Document")
        oCurDoc = wdPatientGuideline.ActiveDocument
        'oCurDoc = ObjWord.CurDocument
    End Sub

    Private Sub wdPatientGuideline_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdPatientGuideline.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                frmPatientExam.blnIsHandlers = True
                'line added by dipak 20090929 to solve problem of opening liquid link dialog boxes(when Liquid link forms and PatientGuideline used Simultaneously ) is solve
                isHandlerRemoved = True
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Close, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Close, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
            ex = Nothing
        End Try
    End Sub

    Private Sub wdPatientGuideline_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdPatientGuideline.OnDocumentClosed
        'Try
        '    If Not oWordApp.ActiveDocument Is Nothing Then
        '        RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        '        oWordApp = Nothing
        '    End If
        'Catch ex As Exception
        '    Exit Sub
        'End Try
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Close, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
            ex = Nothing
        End Try
    End Sub

    '''' <summary>
    '''' To Implement tool strip items click
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    Private Sub tlsPatientGuideline_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsPatientGuideline.ToolStripClick
        Try

            '04-Nov-14 Aniket: Resolving Bug #75615: gloEMR:Patient Guideline- Word options are not working
            ''To check exeception related to word
            'If CheckWordForException() = False Then
            '    Exit Sub
            'End If


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
                    'UpdateVoiceLog("SwitchOff Mic Completed from tlsPatientGuideline_ToolStripClick in Referrals is invoked")
                Case "Save"
                    TurnoffMicrophone()
                    Call SavePatientMaterial()
                Case "Save & Close"
                    Call SavePatientMaterial(True)
                    Me.Close()
                Case "Print"
                    TurnoffMicrophone()
                    Call Print()
                Case "FAX"
                    bnlIsFaxOpened = True
                    TurnoffMicrophone()
                    '' For Resolving Bug ID no :7651 i.e.Patient Guidelines >> IF click on FAX button without selecting template, then it open the Select Contact window. 
                    If Not IsNothing(trvTemplate.SelectedNode) Then
                        If trvTemplate.SelectedNode.Index > 0 And txtSelectedTemplates.Text.Trim() <> "" Then
                            Call GeneratePrintFaxDocument(False)
                        End If
                    End If
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
                    'Bug #31489 : New Exam>>Patient Guild line>>Open Sca imgs winodw without adding template>>Import Images>>Save&cls>>It give Exception 
                    If Not oCurDoc Is Nothing Then
                        Call InsertCoSignature()
                    End If
                Case "Capture Sign"
                    'Bug #31489 : New Exam>>Patient Guild line>>Open Sca imgs winodw without adding template>>Import Images>>Save&cls>>It give Exception 
                    If Not oCurDoc Is Nothing Then
                        Call InsertSignature()
                    End If
                Case "Undo"
                    Call UnDoChanges()
                Case "Redo"
                    Call ReDoChanges()
                Case "Insert File"
                    'Bug #31489 : New Exam>>Patient Guild line>>Open Sca imgs winodw without adding template>>Import Images>>Save&cls>>It give Exception 
                    If Not oCurDoc Is Nothing Then
                        TurnoffMicrophone()
                        ImportDocument(1)
                    End If
                Case "Scan Documents"
                    'Bug #31489 : New Exam>>Patient Guild line>>Open Sca imgs winodw without adding template>>Import Images>>Save&cls>>It give Exception 
                    If Not oCurDoc Is Nothing Then
                        TurnoffMicrophone()
                        ImportDocument(2)
                    End If
                Case "Close"

                    If IsNothing(oCurDoc) = False Then
                        If oCurDoc.Saved = False Then
                            Dim Result As DialogResult
                            Result = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                            If Result = Windows.Forms.DialogResult.Yes Then
                                Call SavePatientMaterial(True)
                                Me.Close()
                            ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                                '' Nothing to here
                            ElseIf Result = Windows.Forms.DialogResult.No Then
                                wdPatientGuideline.Close()
                                Me.Close()
                            End If
                        Else
                            wdPatientGuideline.Close()
                            Me.Close()
                        End If
                    Else
                        Me.Close()
                    End If
                Case "Export"
                    ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Patient Exam", Me)
                    If Result = True Then
                        MessageBox.Show(" Patient Exam Document Exported Successfully ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    objword1 = Nothing
                    'Export Function for Word Docs Integrated by dipak  as on 26 oct 2010
                Case "tblbtn_StrikeThrough"
                    '' chetan added on 25-oct-2010 for Strike Through
                    InsertStrike()
            End Select
        Catch
        End Try
    End Sub

    Private Sub wdPatientGuideline_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdPatientGuideline.OnDocumentOpened
        oCurDoc = wdPatientGuideline.ActiveDocument
        oWordApp = oCurDoc.Application
        'If oCurDoc Is Nothing Then
        '    GloUC_AddRefreshDic1.Visible = False
        'Else
        '    GloUC_AddRefreshDic1.Visible = True
        'End If
        Try
            Try
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            Catch ex As Exception

            End Try

            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        oCurDoc.ActiveWindow.SetFocus()
    End Sub

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
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Exam
        ogloVoice.MyWordToolStrip = Me.tlsPatientGuideline
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "PatientGuideline"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsPatientGuideline_ToolStripClick)
    End Sub

    ''' <summary>
    ''' Add Basic Voice commands to hashtable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Private Function AddBasicVoiceCommands() As Hashtable

        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save PatientGuideLine", "Save")
        oHashtable.Add("Print PatientGuideLine", "Print")
        oHashtable.Add("Fax PatientGuideLine", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close PatientGuideLine", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close PatientGuideLine", "Close")
        oHashtable.Add("Finish PatientGuideLine", "Save & Finish")
        Return oHashtable
    End Function

    ''' <summary>
    ''' Trigger Voice commands from custom collection to DgnStrings
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    ''' 

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
    ''' show microphone button if voice enabled and speaker loaded
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
    ''' turn off microphone button if voice enabled and speaker loaded
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If

        End If
    End Sub

    'Handle of form needed to initialise the Vcmd voice command object
    Public Property Handle1() As Integer Implements mdlgloVoice.IExamChildEvents.Handle
        Get
            Return Me.Handle.ToInt32
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate
        If strstring = "ON" Then
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                tlsPatientGuideline.MyToolStrip.Items("Mic").Visible = True
                tlsPatientGuideline.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                tlsPatientGuideline.MyToolStrip.ButtonsToHide.Remove(tlsPatientGuideline.MyToolStrip.Items("Mic").Name)
            End If
        ElseIf strstring = "OFF" Then
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                tlsPatientGuideline.MyToolStrip.Items("Mic").Visible = True
                tlsPatientGuideline.MyToolStrip.ButtonsToHide.Remove(tlsPatientGuideline.MyToolStrip.Items("Mic").Name)
            Else
                tlsPatientGuideline.MyToolStrip.Items("Mic").Visible = False
                If tlsPatientGuideline.MyToolStrip.ButtonsToHide.Contains(tlsPatientGuideline.MyToolStrip.Items("Mic").Name) = False Then
                    tlsPatientGuideline.MyToolStrip.ButtonsToHide.Add(tlsPatientGuideline.MyToolStrip.Items("Mic").Name)
                End If

            End If
            tlsPatientGuideline.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
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
    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
        End Get
    End Property
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
    Public ReadOnly Property GetTypeOfForm() As MaterialType
        Get
            Return _Type
        End Get
    End Property
End Class
