Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices

Public Class frmPatientLetter

    Inherits System.Windows.Forms.Form
    Implements ISignature
    Implements IHotKey
    Implements gloVoice
    Implements IWord
    Implements IPatientContext

    Public CancelClick As Boolean
    Public IsModify As Boolean = False
    Public Shared strAddendum As String
    Public m_IsFinished As Boolean
    Public MyCaller As frmVWPatientLetters
    Public myCallerMain As MainMenu 'Sets when Caller Form is MainMenu
    Dim bIsPastExamClick As Boolean = False
    Dim nVisitId As Long
    Dim objWord As clsWordDocument
    Dim objCriteria As DocCriteria

    '23-Apr-13 Aniket: Resolving Memory Leaks


    Private WithEvents oCurDoc As Wd.Document
    '  Private WithEvents oTempDoc As Wd.Document
    Private WithEvents ouctlgloUC_Addendum As gloUC_Addendum

    Private ogloVoice As ClsVoice     'Instantiate voice class
    Private _Arrlist As ArrayList
    Private _strNotes As String
    Private ImagePath As String
    Private m_LetterID As Long
    Private m_TemplateID As Long
    Private m_PatientID As Long
    Private m_visitID As Long
    Private blnIsAddendum As Boolean 'Addendum user control defined here 
    Private blnCmbSelTemplate As Boolean = False
    Private _strPatientWorkStatus As String = ""      '20060731 - If Form is Open from MainMenu Patient WorkStatus - With/ WithOut Restriction 

    Private WithEvents _PatientStrip As gloUC_PatientStrip
    Private WithEvents tlsPatientLetter As WordToolStrip.gloWordToolStrip
    Friend WithEvents pnlPatientLetter As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtLetterdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents oWordApp As Wd.Application
    Friend WithEvents pnlWDPatientLetter As System.Windows.Forms.Panel
    ' Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Friend WithEvents wdPatientLetter As AxDSOFramer.AxFramerControl
    Dim myidx As Int32
    Dim m_IsRecordLock As Boolean
    Private LettersVoicecol As DNSTools.DgnStrings
    Dim blnOpenHistory As Boolean = False
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pnlPatientLetterHeader As System.Windows.Forms.Panel
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Public Shared intflag As Integer  'to keep a trace of whether medication was opened from history or not
    Private blnSignClick As Boolean = False
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Friend WithEvents pnlGloUC_PastWordNotes As System.Windows.Forms.Panel
    Friend WithEvents pnlGloUC_TemplateTreeControl As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GloUC_TemplateTreeControl_PatientLetters As gloUserControlLibrary.gloUC_TemplateTreeControl
    Friend WithEvents GloUC_PastWordNotes1 As gloUserControlLibrary.gloUC_PastWordNotes
    Friend WithEvents pnl_cmdPastExam As System.Windows.Forms.Panel
    Friend WithEvents cmdPastExam As System.Windows.Forms.Button
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents pnl_chkShowPreview As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GloUC_AddRefreshDic1 As gloUserControlLibrary.gloUC_AddRefreshDic
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnlwdPatLetter As System.Windows.Forms.Panel
    Private strPatientMaritalStatus As String

    ' Friend WithEvents UiPanelManager1_PatientLetters As Janus.Windows.UI.Dock.UIPanelManager
    Public WithEvents uiPanSplitScreen_PatientLetters As Janus.Windows.UI.Dock.UIPanelGroup
    Dim clsSplit_PatientLetters As New gloEMRGeneralLibrary.clsSplitScreen

    '23-Apr-13 Aniket: Resolving Memory Leaks
    Private dtTemplate As DataTable
    Private dtReminderList As DataTable
    Private bnlIsFaxOpened As Boolean

#Region "variable Declaration for UC treeview"

    '23-Apr-13 Aniket: Resolving Memory Leaks
    'Dim obj_UCclsPatientLetters As clsPatientLetters
    'Dim Obj_UCWord As clsWordDocument
    'Dim obj_UCCriteria As DocCriteria
    Friend WithEvents pnlgloUC_Addendum As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents ChkRemiderForUnSchedle As System.Windows.Forms.CheckBox
    Friend WithEvents CmbCommunicationPref As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblPatientPrefers As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    ' Dim clsPatientExams As clsPatientExams

#End Region

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        m_PatientID = PatientID
    End Sub

    Public Sub New(ByVal LetterID As Long, ByVal TemplateID As Long, ByVal IsFinished As Boolean, ByVal IsRecordLock As Boolean, ByVal nPatientID As Long)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        m_LetterID = LetterID
        m_TemplateID = TemplateID
        m_IsFinished = IsFinished
        m_PatientID = nPatientID
        m_IsRecordLock = IsRecordLock

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
            Try
                If (IsNothing(dtLetterdate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtLetterdate)
                    Catch ex As Exception

                    End Try


                    dtLetterdate.Dispose()
                    dtLetterdate = Nothing
                End If
            Catch
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
            'If Not IsNothing(objclsPatientLetters) Then
            '    objclsPatientLetters.Dispose()
            '    objclsPatientLetters = Nothing
            'End If
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If
            Try
                If (IsNothing(GloUC_TemplateTreeControl_PatientLetters) = False) Then
                    If (IsNothing(GloUC_TemplateTreeControl_PatientLetters.DocCriteria) = False) Then
                        DirectCast(GloUC_TemplateTreeControl_PatientLetters.DocCriteria, DocCriteria).Dispose()
                        GloUC_TemplateTreeControl_PatientLetters.DocCriteria = Nothing
                    End If
                End If
            Catch

            End Try

            Try ''added to solve memory leak issues
                If (IsNothing(GloUC_PastWordNotes1) = False) Then
                    If (IsNothing(GloUC_PastWordNotes1.CLSPATIENTEXAMSs) = False) Then
                        CType(GloUC_PastWordNotes1.CLSPATIENTEXAMSs, clsPatientExams).Dispose()
                        GloUC_PastWordNotes1.CLSPATIENTEXAMSs = Nothing
                    End If
                End If

            Catch ex As Exception
                ex = Nothing
            End Try
            Try  ''added to solve memory leak issues
                If (IsNothing(GloUC_PastWordNotes1) = False) Then
                    If (IsNothing(GloUC_PastWordNotes1.OBJCLSPATIENTLETTERSs) = False) Then
                        CType(GloUC_PastWordNotes1.OBJCLSPATIENTLETTERSs, clsPatientLetters).Dispose()
                        GloUC_PastWordNotes1.OBJCLSPATIENTLETTERSs = Nothing
                    End If
                End If
            Catch ex As Exception
                ex = Nothing
            End Try

            Try
                If (IsNothing(GloUC_PastWordNotes1) = False) Then
                    If (IsNothing(GloUC_PastWordNotes1.OBJCRITERIAs) = False) Then
                        DirectCast(GloUC_PastWordNotes1.OBJCRITERIAs, DocCriteria).Dispose()
                        GloUC_PastWordNotes1.OBJCRITERIAs = Nothing
                    End If
                End If
            Catch

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
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    ' Friend WithEvents WdPatientLetter As AxDSOFramer.AxFramerControl
    'Friend WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Friend WithEvents tmrDocProtect As System.Windows.Forms.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientLetter))
        Me.tmrDocProtect = New System.Windows.Forms.Timer(Me.components)
        Me.pnlPatientLetter = New System.Windows.Forms.Panel()
        Me.GloUC_AddRefreshDic1 = New gloUserControlLibrary.gloUC_AddRefreshDic()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtLetterdate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.ChkRemiderForUnSchedle = New System.Windows.Forms.CheckBox()
        Me.CmbCommunicationPref = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.pnlWDPatientLetter = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.wdPatientLetter = New AxDSOFramer.AxFramerControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlPatientLetterHeader = New System.Windows.Forms.Panel()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlGloUC_PastWordNotes = New System.Windows.Forms.Panel()
        Me.GloUC_PastWordNotes1 = New gloUserControlLibrary.gloUC_PastWordNotes()
        Me.pnl_chkShowPreview = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnlGloUC_TemplateTreeControl = New System.Windows.Forms.Panel()
        Me.GloUC_TemplateTreeControl_PatientLetters = New gloUserControlLibrary.gloUC_TemplateTreeControl()
        Me.pnl_cmdPastExam = New System.Windows.Forms.Panel()
        Me.cmdPastExam = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlwdPatLetter = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblPatientPrefers = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.pnlgloUC_Addendum = New System.Windows.Forms.Panel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlPatientLetter.SuspendLayout()
        Me.pnlWDPatientLetter.SuspendLayout()
        CType(Me.wdPatientLetter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPatientLetterHeader.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.pnlGloUC_PastWordNotes.SuspendLayout()
        Me.pnl_chkShowPreview.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlGloUC_TemplateTreeControl.SuspendLayout()
        Me.pnl_cmdPastExam.SuspendLayout()
        Me.pnlwdPatLetter.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmrDocProtect
        '
        '
        'pnlPatientLetter
        '
        Me.pnlPatientLetter.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPatientLetter.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button1
        Me.pnlPatientLetter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPatientLetter.Controls.Add(Me.GloUC_AddRefreshDic1)
        Me.pnlPatientLetter.Controls.Add(Me.Label18)
        Me.pnlPatientLetter.Controls.Add(Me.Label4)
        Me.pnlPatientLetter.Controls.Add(Me.dtLetterdate)
        Me.pnlPatientLetter.Controls.Add(Me.Label8)
        Me.pnlPatientLetter.Controls.Add(Me.Label20)
        Me.pnlPatientLetter.Controls.Add(Me.cmbTemplate)
        Me.pnlPatientLetter.Controls.Add(Me.Label5)
        Me.pnlPatientLetter.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlPatientLetter.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlPatientLetter.Controls.Add(Me.lbl_TopBrd)
        Me.pnlPatientLetter.Controls.Add(Me.lbl_RightBrd)
        Me.pnlPatientLetter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientLetter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatientLetter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlPatientLetter.Location = New System.Drawing.Point(0, 3)
        Me.pnlPatientLetter.Name = "pnlPatientLetter"
        Me.pnlPatientLetter.Size = New System.Drawing.Size(841, 24)
        Me.pnlPatientLetter.TabIndex = 3
        '
        'GloUC_AddRefreshDic1
        '
        Me.GloUC_AddRefreshDic1.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_AddRefreshDic1.CONNECTIONSTRINGs = Nothing
        Me.GloUC_AddRefreshDic1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing
        Me.GloUC_AddRefreshDic1.Location = New System.Drawing.Point(293, 1)
        Me.GloUC_AddRefreshDic1.M_PATIENTIDs = CType(0, Long)
        Me.GloUC_AddRefreshDic1.Name = "GloUC_AddRefreshDic1"
        Me.GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
        Me.GloUC_AddRefreshDic1.ObjFrom = Nothing
        Me.GloUC_AddRefreshDic1.OBJWORDs = Nothing
        Me.GloUC_AddRefreshDic1.OCURDOCs = Nothing
        Me.GloUC_AddRefreshDic1.OWORDAPPs = Nothing
        Me.GloUC_AddRefreshDic1.Size = New System.Drawing.Size(43, 22)
        Me.GloUC_AddRefreshDic1.TabIndex = 34
        Me.GloUC_AddRefreshDic1.wdPatientWordDocs = Nothing
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(290, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(3, 22)
        Me.Label18.TabIndex = 37
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(635, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 22)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Letter Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtLetterdate
        '
        Me.dtLetterdate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtLetterdate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtLetterdate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtLetterdate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtLetterdate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtLetterdate.Dock = System.Windows.Forms.DockStyle.Right
        Me.dtLetterdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtLetterdate.Location = New System.Drawing.Point(724, 1)
        Me.dtLetterdate.Name = "dtLetterdate"
        Me.dtLetterdate.Size = New System.Drawing.Size(103, 22)
        Me.dtLetterdate.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(827, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(5, 22)
        Me.Label8.TabIndex = 33
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(832, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(8, 22)
        Me.Label20.TabIndex = 39
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.Location = New System.Drawing.Point(119, 1)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(171, 22)
        Me.cmbTemplate.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(118, 22)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Select Template :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(839, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(840, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(840, 0)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 24)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'ChkRemiderForUnSchedle
        '
        Me.ChkRemiderForUnSchedle.AutoSize = True
        Me.ChkRemiderForUnSchedle.BackColor = System.Drawing.Color.Transparent
        Me.ChkRemiderForUnSchedle.Checked = True
        Me.ChkRemiderForUnSchedle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkRemiderForUnSchedle.Dock = System.Windows.Forms.DockStyle.Right
        Me.ChkRemiderForUnSchedle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkRemiderForUnSchedle.Location = New System.Drawing.Point(478, 1)
        Me.ChkRemiderForUnSchedle.Name = "ChkRemiderForUnSchedle"
        Me.ChkRemiderForUnSchedle.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.ChkRemiderForUnSchedle.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkRemiderForUnSchedle.Size = New System.Drawing.Size(228, 22)
        Me.ChkRemiderForUnSchedle.TabIndex = 35
        Me.ChkRemiderForUnSchedle.Text = "Reminder for Unscheduled Care"
        Me.ChkRemiderForUnSchedle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkRemiderForUnSchedle.UseVisualStyleBackColor = False
        '
        'CmbCommunicationPref
        '
        Me.CmbCommunicationPref.Dock = System.Windows.Forms.DockStyle.Right
        Me.CmbCommunicationPref.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCommunicationPref.Location = New System.Drawing.Point(706, 1)
        Me.CmbCommunicationPref.Name = "CmbCommunicationPref"
        Me.CmbCommunicationPref.Size = New System.Drawing.Size(126, 22)
        Me.CmbCommunicationPref.TabIndex = 36
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(832, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(8, 22)
        Me.Label19.TabIndex = 38
        '
        'pnlWDPatientLetter
        '
        Me.pnlWDPatientLetter.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlWDPatientLetter.Controls.Add(Me.Label6)
        Me.pnlWDPatientLetter.Controls.Add(Me.wdPatientLetter)
        Me.pnlWDPatientLetter.Controls.Add(Me.Label1)
        Me.pnlWDPatientLetter.Controls.Add(Me.Label2)
        Me.pnlWDPatientLetter.Controls.Add(Me.Label3)
        Me.pnlWDPatientLetter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWDPatientLetter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlWDPatientLetter.ForeColor = System.Drawing.Color.Black
        Me.pnlWDPatientLetter.Location = New System.Drawing.Point(0, 54)
        Me.pnlWDPatientLetter.Name = "pnlWDPatientLetter"
        Me.pnlWDPatientLetter.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlWDPatientLetter.Size = New System.Drawing.Size(844, 524)
        Me.pnlWDPatientLetter.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(839, 1)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "label2"
        '
        'wdPatientLetter
        '
        Me.wdPatientLetter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPatientLetter.Enabled = True
        Me.wdPatientLetter.Location = New System.Drawing.Point(1, 3)
        Me.wdPatientLetter.Name = "wdPatientLetter"
        Me.wdPatientLetter.OcxState = CType(resources.GetObject("wdPatientLetter.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPatientLetter.Size = New System.Drawing.Size(839, 517)
        Me.wdPatientLetter.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 520)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(839, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 518)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(840, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 518)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'pnlPatientLetterHeader
        '
        Me.pnlPatientLetterHeader.Controls.Add(Me.pnlPatientLetter)
        Me.pnlPatientLetterHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientLetterHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientLetterHeader.Name = "pnlPatientLetterHeader"
        Me.pnlPatientLetterHeader.Padding = New System.Windows.Forms.Padding(0, 3, 3, 0)
        Me.pnlPatientLetterHeader.Size = New System.Drawing.Size(844, 27)
        Me.pnlPatientLetterHeader.TabIndex = 10
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.Label7)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1284, 30)
        Me.pnlToolStrip.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(227, 30)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "pnl Toolstrip"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.Visible = False
        '
        'pnlGloUC_PastWordNotes
        '
        Me.pnlGloUC_PastWordNotes.Controls.Add(Me.GloUC_PastWordNotes1)
        Me.pnlGloUC_PastWordNotes.Controls.Add(Me.pnl_chkShowPreview)
        Me.pnlGloUC_PastWordNotes.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_PastWordNotes.Location = New System.Drawing.Point(0, 0)
        Me.pnlGloUC_PastWordNotes.Name = "pnlGloUC_PastWordNotes"
        Me.pnlGloUC_PastWordNotes.Size = New System.Drawing.Size(217, 578)
        Me.pnlGloUC_PastWordNotes.TabIndex = 12
        '
        'GloUC_PastWordNotes1
        '
        Me.GloUC_PastWordNotes1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GloUC_PastWordNotes1.CLSPATIENTEXAMSs = Nothing
        Me.GloUC_PastWordNotes1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_PastWordNotes1.EXAMNEWDOCUMENTNAMEs = Nothing
        Me.GloUC_PastWordNotes1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GloUC_PastWordNotes1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GloUC_PastWordNotes1.FromForms = Nothing
        Me.GloUC_PastWordNotes1.Location = New System.Drawing.Point(0, 27)
        Me.GloUC_PastWordNotes1.M_LETTERIDs = CType(0, Long)
        Me.GloUC_PastWordNotes1.M_VISITIDs = CType(0, Long)
        Me.GloUC_PastWordNotes1.Name = "GloUC_PastWordNotes1"
        Me.GloUC_PastWordNotes1.OBJCLSDISCLOSUREs = Nothing
        Me.GloUC_PastWordNotes1.OBJCLSNOTESs = Nothing
        Me.GloUC_PastWordNotes1.OBJCLSPATIENTCONSENTs = Nothing
        Me.GloUC_PastWordNotes1.OBJCLSPATIENTLETTERSs = Nothing
        Me.GloUC_PastWordNotes1.OBJCLSPTPROTOCOLs = Nothing
        Me.GloUC_PastWordNotes1.OBJCRITERIAs = Nothing
        Me.GloUC_PastWordNotes1.OBJWORDs = Nothing
        Me.GloUC_PastWordNotes1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.GloUC_PastWordNotes1.PATIENTIDs = CType(0, Long)
        Me.GloUC_PastWordNotes1.SHOWPASTs = False
        Me.GloUC_PastWordNotes1.Size = New System.Drawing.Size(217, 551)
        Me.GloUC_PastWordNotes1.STRFORMNAMEs = "0"
        Me.GloUC_PastWordNotes1.TabIndex = 0
        Me.GloUC_PastWordNotes1.TLSMESSAGESs = Nothing
        '
        'pnl_chkShowPreview
        '
        Me.pnl_chkShowPreview.BackColor = System.Drawing.Color.Transparent
        Me.pnl_chkShowPreview.Controls.Add(Me.Panel2)
        Me.pnl_chkShowPreview.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_chkShowPreview.Location = New System.Drawing.Point(0, 0)
        Me.pnl_chkShowPreview.Name = "pnl_chkShowPreview"
        Me.pnl_chkShowPreview.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnl_chkShowPreview.Size = New System.Drawing.Size(217, 27)
        Me.pnl_chkShowPreview.TabIndex = 36
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(217, 24)
        Me.Panel2.TabIndex = 34
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(146, 22)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Past Patient Letters"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(1, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(215, 1)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 23)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(216, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 23)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(217, 1)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "label1"
        '
        'pnlGloUC_TemplateTreeControl
        '
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.GloUC_TemplateTreeControl_PatientLetters)
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.pnl_cmdPastExam)
        Me.pnlGloUC_TemplateTreeControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_TemplateTreeControl.Location = New System.Drawing.Point(220, 0)
        Me.pnlGloUC_TemplateTreeControl.Name = "pnlGloUC_TemplateTreeControl"
        Me.pnlGloUC_TemplateTreeControl.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGloUC_TemplateTreeControl.Size = New System.Drawing.Size(217, 578)
        Me.pnlGloUC_TemplateTreeControl.TabIndex = 12
        '
        'GloUC_TemplateTreeControl_PatientLetters
        '
        Me.GloUC_TemplateTreeControl_PatientLetters.DocCriteria = Nothing
        Me.GloUC_TemplateTreeControl_PatientLetters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_TemplateTreeControl_PatientLetters.ExpandConsent = False
        Me.GloUC_TemplateTreeControl_PatientLetters.Location = New System.Drawing.Point(0, 27)
        Me.GloUC_TemplateTreeControl_PatientLetters.Name = "GloUC_TemplateTreeControl_PatientLetters"
        Me.GloUC_TemplateTreeControl_PatientLetters.ObjClsWord = Nothing
        Me.GloUC_TemplateTreeControl_PatientLetters.ProviderId = CType(0, Long)
        Me.GloUC_TemplateTreeControl_PatientLetters.Size = New System.Drawing.Size(217, 548)
        Me.GloUC_TemplateTreeControl_PatientLetters.TabIndex = 0
        '
        'pnl_cmdPastExam
        '
        Me.pnl_cmdPastExam.BackColor = System.Drawing.Color.Transparent
        Me.pnl_cmdPastExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_cmdPastExam.Controls.Add(Me.cmdPastExam)
        Me.pnl_cmdPastExam.Controls.Add(Me.Label11)
        Me.pnl_cmdPastExam.Controls.Add(Me.Label12)
        Me.pnl_cmdPastExam.Controls.Add(Me.Label13)
        Me.pnl_cmdPastExam.Controls.Add(Me.Label14)
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
        Me.cmdPastExam.Text = "Show Past Patient Letters"
        Me.cmdPastExam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPastExam.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(1, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(215, 1)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 23)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(216, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 23)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(217, 1)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(437, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 578)
        Me.Splitter1.TabIndex = 13
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(217, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 578)
        Me.Splitter2.TabIndex = 14
        Me.Splitter2.TabStop = False
        '
        'pnlwdPatLetter
        '
        Me.pnlwdPatLetter.Controls.Add(Me.pnlWDPatientLetter)
        Me.pnlwdPatLetter.Controls.Add(Me.Panel1)
        Me.pnlwdPatLetter.Controls.Add(Me.pnlgloUC_Addendum)
        Me.pnlwdPatLetter.Controls.Add(Me.pnlPatientLetterHeader)
        Me.pnlwdPatLetter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlwdPatLetter.Location = New System.Drawing.Point(440, 0)
        Me.pnlwdPatLetter.Name = "pnlwdPatLetter"
        Me.pnlwdPatLetter.Size = New System.Drawing.Size(844, 578)
        Me.pnlwdPatLetter.TabIndex = 15
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 3, 3, 0)
        Me.Panel1.Size = New System.Drawing.Size(844, 27)
        Me.Panel1.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button1
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.lblPatientPrefers)
        Me.Panel3.Controls.Add(Me.ChkRemiderForUnSchedle)
        Me.Panel3.Controls.Add(Me.CmbCommunicationPref)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Label27)
        Me.Panel3.Controls.Add(Me.Label28)
        Me.Panel3.Controls.Add(Me.Label29)
        Me.Panel3.Controls.Add(Me.Label30)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(0, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(841, 24)
        Me.Panel3.TabIndex = 3
        '
        'lblPatientPrefers
        '
        Me.lblPatientPrefers.AutoSize = True
        Me.lblPatientPrefers.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientPrefers.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPatientPrefers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientPrefers.ForeColor = System.Drawing.Color.Red
        Me.lblPatientPrefers.Location = New System.Drawing.Point(1, 1)
        Me.lblPatientPrefers.Name = "lblPatientPrefers"
        Me.lblPatientPrefers.Padding = New System.Windows.Forms.Padding(4)
        Me.lblPatientPrefers.Size = New System.Drawing.Size(8, 22)
        Me.lblPatientPrefers.TabIndex = 39
        Me.lblPatientPrefers.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPatientPrefers.Visible = False
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(1, 23)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(839, 1)
        Me.Label27.TabIndex = 8
        Me.Label27.Text = "label2"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(0, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 23)
        Me.Label28.TabIndex = 7
        Me.Label28.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(840, 1)
        Me.Label29.TabIndex = 5
        Me.Label29.Text = "label1"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label30.Location = New System.Drawing.Point(840, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 24)
        Me.Label30.TabIndex = 6
        Me.Label30.Text = "label3"
        '
        'pnlgloUC_Addendum
        '
        Me.pnlgloUC_Addendum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlgloUC_Addendum.Location = New System.Drawing.Point(0, 27)
        Me.pnlgloUC_Addendum.Name = "pnlgloUC_Addendum"
        Me.pnlgloUC_Addendum.Padding = New System.Windows.Forms.Padding(0, 3, 3, 0)
        Me.pnlgloUC_Addendum.Size = New System.Drawing.Size(844, 551)
        Me.pnlgloUC_Addendum.TabIndex = 10
        Me.pnlgloUC_Addendum.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlwdPatLetter)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlGloUC_TemplateTreeControl)
        Me.pnlMain.Controls.Add(Me.Splitter2)
        Me.pnlMain.Controls.Add(Me.pnlGloUC_PastWordNotes)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 30)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1284, 578)
        Me.pnlMain.TabIndex = 37
        '
        'frmPatientLetter
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1284, 608)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientLetter"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Letter"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlPatientLetter.ResumeLayout(False)
        Me.pnlWDPatientLetter.ResumeLayout(False)
        CType(Me.wdPatientLetter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPatientLetterHeader.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlGloUC_PastWordNotes.ResumeLayout(False)
        Me.pnl_chkShowPreview.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlGloUC_TemplateTreeControl.ResumeLayout(False)
        Me.pnl_cmdPastExam.ResumeLayout(False)
        Me.pnlwdPatLetter.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected Overrides Sub OnClosed(ByVal e As System.EventArgs)
        MyBase.OnClosed(e)
    End Sub

    Private Sub AddWordHandlers()
        If (IsNothing(oWordApp) = False) Then
            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            AddHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick
        End If

    End Sub

    Private Sub RemoveWordHandlers()
        Try
            If (IsNothing(oWordApp) = False) Then
                RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Remove, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmPatientLetter_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Try
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            Catch ex As Exception
                'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.ShowMicroPhone()
                End If
            End If

            If Not IsNothing(wdPatientLetter.DocumentName) Then
                oCurDoc = wdPatientLetter.ActiveDocument
                oWordApp = oCurDoc.Application

                ''20121218::Bug No 40859::As Per discussion If Condition removed 
                'If (isHandlerRemoved) Then
                RemoveWordHandlers()
                AddWordHandlers()
                isHandlerRemoved = False
                'End If
            End If
            If (IsNothing(oCurDoc) = False) Then
                If (IsNothing(oCurDoc.ActiveWindow) = False) Then
                    oCurDoc.ActiveWindow.SetFocus()
                End If
            End If


            'Condition Added by Amit to resolve issue 42027 in 7020
            'below code should run only in case of Patient Letter not finished
            If m_IsFinished = False Then
                oCurDoc.FormFields.Shaded = False
            End If


            Me.WindowState = FormWindowState.Maximized


            If Not IsNothing(uiPanSplitScreen_PatientLetters) Then
                If Not IsNothing(uiPanSplitScreen_PatientLetters.Parent) Then
                    If uiPanSplitScreen_PatientLetters.Parent.Text = "Split Screen" Then
                        uiPanSplitScreen_PatientLetters.Parent.Visible = True
                    ElseIf uiPanSplitScreen_PatientLetters.Text = "Split Screen" Then
                        uiPanSplitScreen_PatientLetters.Visible = True
                    End If
                End If
            End If



            'If Not IsNothing(uiPanSplitScreen_PatientLetters) Then
            '    If Not IsNothing(uiPanSplitScreen_PatientLetters.Parent) Then
            '        If uiPanSplitScreen_PatientLetters.Parent.Text = "Split Screen" Then
            '            uiPanSplitScreen_PatientLetters.Parent.Visible = True
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "ERROR:At frmPatientLetter_Activated \n" & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Me.ParentForm IsNot Nothing Then
                CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
                CType(Me.ParentForm, MainMenu).ActiveDSO = wdPatientLetter
            End If
        End Try
    End Sub


    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function ShowWindow(ByVal hwnd As IntPtr, ByVal nCmdShow As ShowWindowCommands) As Boolean
    End Function

    Enum ShowWindowCommands As Integer
        ''' <summary>
        ''' Hides the window and activates another window.
        ''' </summary>
        Hide = 0
        ''' <summary>
        ''' Activates and displays a window. If the window is minimized or 
        ''' maximized, the system restores it to its original size and position.
        ''' An application should specify this flag when displaying the window 
        ''' for the first time.
        ''' </summary>
        Normal = 1
        ''' <summary>
        ''' Activates the window and displays it as a minimized window.
        ''' </summary>
        ShowMinimized = 2
        ''' <summary>
        ''' Maximizes the specified window.
        ''' </summary>
        Maximize = 3
        ' is this the right value?
        ''' <summary>
        ''' Activates the window and displays it as a maximized window.
        ''' </summary>       
        ShowMaximized = 3
        ''' <summary>
        ''' Displays a window in its most recent size and position. This value 
        ''' is similar to 
        ''' the window is not actived.
        ''' </summary>
        ShowNoActivate = 4
        ''' <summary>
        ''' Activates the window and displays it in its current size and position. 
        ''' </summary>
        Show = 5
        ''' <summary>
        ''' Minimizes the specified window and activates the next top-level 
        ''' window in the Z order.
        ''' </summary>
        Minimize = 6
        ''' <summary>
        ''' Displays the window as a minimized window. This value is similar to
        ''' 
        ''' window is not activated.
        ''' </summary>
        ShowMinNoActive = 7
        ''' <summary>
        ''' Displays the window in its current size and position. This value is 
        ''' similar to 
        ''' window is not activated.
        ''' </summary>
        ShowNA = 8
        ''' <summary>
        ''' Activates and displays the window. If the window is minimized or 
        ''' maximized, the system restores it to its original size and position. 
        ''' An application should specify this flag when restoring a minimized window.
        ''' </summary>
        Restore = 9
        ''' <summary>
        ''' Sets the show state based on the SW_* value specified in the 
        ''' STARTUPINFO structure passed to the CreateProcess function by the 
        ''' program that started the application.
        ''' </summary>
        ShowDefault = 10
        ''' <summary>
        '''  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread 
        ''' that owns the window is not responding. This flag should only be 
        ''' used when minimizing windows from a different thread.
        ''' </summary>
        ForceMinimize = 11
    End Enum

    Private Sub frmPatientLetter_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Try
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.TurnoffMicrophone()
                End If
            End If

            If Not oCurDoc Is Nothing Then
                RemoveWordHandlers()
                isHandlerRemoved = True
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "RemoveHandler for WindowBeforeDoubleClick  for oWordApp", gloAuditTrail.ActivityOutCome.Success)
            End If

            ''20121217::Hide Split Control On Form Deactivation
            If Not IsNothing(Me.Parent) Then
                If Not IsNothing(uiPanSplitScreen_PatientLetters) Then
                    If Not IsNothing(uiPanSplitScreen_PatientLetters.Parent) Then
                        If uiPanSplitScreen_PatientLetters.Parent IsNot Me Then
                            uiPanSplitScreen_PatientLetters.Parent.Visible = False
                            uiPanSplitScreen_PatientLetters.Parent.Hide()
                            uiPanSplitScreen_PatientLetters.Parent.Update()
                        End If
                    End If
                End If
            End If

            'If Not IsNothing(Me.Parent) Then
            '    If Not IsNothing(uiPanSplitScreen_PatientLetters) Then
            '        If Not IsNothing(uiPanSplitScreen_PatientLetters.Parent) Then
            '            If uiPanSplitScreen_PatientLetters.Parent.Text = "Split Screen" Then
            '                uiPanSplitScreen_PatientLetters.Parent.Visible = False
            '            End If
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "ERROR:At frmPatientLetter_Deactivate \n" & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmPatientLetter_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
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

            'Developer:Yatin N. Bhagat
            'Date:12/13/2011
            'Bug ID/PRD Name/Salesforce Case:GLO2011-0015304 Check boxes not consistently working in patient exam module
            'Reason: Invalid Code
            'Me.Hide()

            If IsNothing(MyCaller) = False Then
                If MyCaller.CanSelect = True Then
                    Call MyCaller.RefreshLetters(m_LetterID)
                End If
            End If

            'Unlock the Record
            'Mahesh - 20070718
            'If record is lock then unlock it before close

            If m_IsRecordLock = False Then
                UnLock_Transaction(TrnType.Letters, m_LetterID, 0, Now)
            End If

            '23-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(dtTemplate) = False Then
                dtTemplate.Dispose()
                dtTemplate = Nothing
            End If

            If IsNothing(dtReminderList) = False Then
                dtReminderList.Dispose()
                dtReminderList = Nothing
            End If

            GloUC_PastWordNotes1.ControlDispose()
            GloUC_PastWordNotes1.Dispose()
            'Unlock the Record
            'If (IsNothing(objclsPatientLetters) = False) Then
            '    objclsPatientLetters.Dispose()
            '    objclsPatientLetters = Nothing
            'End If

            If (IsNothing(mdlFAX.Owner) = False) Then
                mdlFAX.Owner = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Close, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            wdPatientLetter.Close()
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
            End If
            Dispose_Object()

        End Try
    End Sub

    Private Sub frmPatientLetter_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If m_IsRecordLock Then
            Exit Sub
        End If

        If IsNothing(oCurDoc) = False Then
            If oCurDoc.Saved = False Then
                Dim Result As Int16
                Result = MessageBox.Show("Do you want to save the changes to Patient Letter?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                If Result = MsgBoxResult.Yes Then
                    If m_IsFinished Then
                        Call SavePatientLetter(True, True)
                    Else
                        Call SavePatientLetter(False, True)
                    End If
                    e.Cancel = False
                ElseIf Result = MsgBoxResult.Cancel Then
                    'Nothing to here
                    e.Cancel = True
                ElseIf Result = MsgBoxResult.No Then
                    e.Cancel = False
                End If
            Else
                e.Cancel = False
            End If
        Else
            e.Cancel = False
        End If
        'Split Screen implementation
        If Not e.Cancel Then
            clsSplit_PatientLetters.SaveControlDisplaySettings()
            If (IsNothing(uiPanSplitScreen_PatientLetters) = False) Then
                uiPanSplitScreen_PatientLetters.Dispose()
                uiPanSplitScreen_PatientLetters = Nothing
            End If

            If (IsNothing(clsSplit_PatientLetters) = False) Then
                clsSplit_PatientLetters.Dispose()
                clsSplit_PatientLetters = Nothing
            End If
        End If
    End Sub

    Private Sub frmPatientLetter_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles Me.Layout

    End Sub

    Private Sub frmPatientLetter_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub

    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            ' dtPatient = New DataTable
            dtPatient = GetPatientInfo(m_PatientID)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If
        End Try
    End Sub




    Private Sub frmPatientLetter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''slr free previous memory
        'If Not IsNothing(objclsPatientLetters) Then
        '    objclsPatientLetters.Dispose()
        '    objclsPatientLetters = Nothing
        'End If
        Dim objclsPatientLetters As clsPatientLetters
        objclsPatientLetters = New clsPatientLetters
        Try
            objclsPatientLetters.fill_widthofExam(pnlGloUC_TemplateTreeControl)

            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "Patient Letters Load Started", gloAuditTrail.ActivityOutCome.Success)

            '23-Apr-13 Aniket: Resolving Memory Leaks
            Call Get_PatientDetails()
            dtLetterdate.Format = DateTimePickerFormat.Custom
            dtLetterdate.CustomFormat = Format("MM/dd/yyyy hh:mm tt")
            dtLetterdate.Value = Now

            'Added by Mayuri:20100517-To fix issue:#6641-Delete nurse note template = lost names of created nurse notes associated with template
            ''slr free dttemplates
            If Not IsNothing(dtTemplate) Then
                dtTemplate.Dispose()
                dtTemplate = Nothing
            End If

            If m_LetterID <> 0 Then
                Dim objclsNotes As New clsNurseNotes
                dtTemplate = objclsNotes.FillTemplate(enumTemplateFlag.PatientLetters, m_LetterID, m_TemplateID).Copy()
                objclsNotes.Dispose()
                objclsNotes = Nothing
            Else
                dtTemplate = objclsPatientLetters.FillTemplates()
            End If

            If Not IsNothing(dtTemplate) Then
                If dtTemplate.Rows.Count > 0 Then
                    cmbTemplate.DataSource = dtTemplate
                    cmbTemplate.DisplayMember = dtTemplate.Columns(1).ColumnName
                    cmbTemplate.ValueMember = dtTemplate.Columns(0).ColumnName
                    If _strPatientWorkStatus = "" Then

                        cmbTemplate.SelectedIndex = 0
                    Else
                        'Select Patient Work Status
                        cmbTemplate.Text = _strPatientWorkStatus
                        If _strPatientWorkStatus = "With Restriction" Then
                            cmbTemplate.Enabled = False
                        End If
                    End If
                End If
            End If

            'To load the PatientStrip User Control 
            loadPatientStrip()

            Call FillReminderforUnscheduledCare()

            If m_LetterID <> 0 Then
                'for Update the exisitng letter
                Dim dtLetter As DataTable
                'SLR: Free previous memory
                If Not IsNothing(objclsPatientLetters) Then
                    objclsPatientLetters.Dispose()
                    objclsPatientLetters = Nothing
                End If
                objclsPatientLetters = New clsPatientLetters
                dtLetter = objclsPatientLetters.ScanPatientLetter(m_LetterID)
                Fill_PatientLetter(dtLetter)
                dtLetter.Dispose()
                dtLetter = Nothing

                If Not IsNothing(objclsPatientLetters) Then
                    '23-Apr-13 Aniket: Resolving Memory Leaks
                    objclsPatientLetters.Dispose()
                    objclsPatientLetters = Nothing
                End If
            Else
                'for New Letter fill by default one letter Template  
                If cmbTemplate.SelectedValue > 0 Then
                    'To Add a new Letter
                    Call Fill_TemplateGallery()
                Else
                    tlsPatientLetter.FormType = WordToolStrip.enumControlType.PatientLetters
                    wdPatientLetter.CreateNew("Word.Document")
                End If
            End If

            If m_IsRecordLock Then

                'code Added by Mayuri - 20090827 - To Disable Buttons if user doesn't have access to modify PatientLetter

                If tlsPatientLetter.MyToolStrip.Items.ContainsKey("Save") = True Then
                    tlsPatientLetter.MyToolStrip.Items("Save").Enabled = False
                End If

                If tlsPatientLetter.MyToolStrip.Items.ContainsKey("Save & Close") = True Then
                    tlsPatientLetter.MyToolStrip.Items("Save & Close").Enabled = False
                End If
                If tlsPatientLetter.MyToolStrip.Items.ContainsKey("Save & Finish") = True Then
                    tlsPatientLetter.MyToolStrip.Items("Save & Finish").Enabled = False
                End If

                If tlsPatientLetter.MyToolStrip.Items.ContainsKey("Add Addendum") = True Then
                    tlsPatientLetter.MyToolStrip.Items("Add Addendum").Enabled = False
                End If

                'end code

            End If

            If IsModify Then
                cmbTemplate.Enabled = False
                dtLetterdate.Enabled = False
            End If
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
                InitializeVoiceObject()
                ShowMicroPhone()
            End If

            'line added by dipak 20090922 for set mycallingform value which is helpful to track clsworddocunent is called by which from 
            InitialiseWordObject()
            'end line added by dipak 20090922

            pnlGloUC_PastWordNotes.Hide()
            InitiliseTemplateTreeControl()
            CallPastData()




            clsSplit_PatientLetters.clsUCLabControl = New gloUserControlLibrary.gloUC_TransactionHistory()
            clsSplit_PatientLetters.clsPatientExams = New clsPatientExams()
            clsSplit_PatientLetters.clsPatientLetters = New clsPatientLetters()
            clsSplit_PatientLetters.clsPatientMessages = New clsMessage()
            clsSplit_PatientLetters.clsNurseNotes = New clsNurseNotes()
            clsSplit_PatientLetters.clsHistory = New clsPatientHistory()
            clsSplit_PatientLetters.clsLabs = New clsDoctorsDashBoard()
            clsSplit_PatientLetters.clsDMS = New gloEDocumentV3.eDocManager.eDocGetList()
            clsSplit_PatientLetters.clsRxmed = New clsPatientDetails()
            clsSplit_PatientLetters.clsOrders = New clsPatientDetails()
            clsSplit_PatientLetters.clsProblemList = New clsPatientProblemList()
            clsSplit_PatientLetters.blnShowSmokingStatusCol = gblnShowSmokingColumn
            uiPanSplitScreen_PatientLetters = clsSplit_PatientLetters.LoadSplitControl(Me, m_PatientID, nVisitId, "PatientLetter", objCriteria, objWord, gnClinicID, gnLoginID)
            uiPanSplitScreen_PatientLetters.BringToFront()


            calltoAddRefreshButtonControl()

            If m_IsRecordLock Or m_IsFinished Then
                GloUC_AddRefreshDic1.Visible = False
                pnlGloUC_TemplateTreeControl.Hide()
                pnlGloUC_PastWordNotes.Show()
                Splitter1.Hide()
            End If
            'Issue::On the DMS screen the split control is always expanded. It does not remember its collapsed state 
            'If IsNothing(Me.Parent) Then
            '    uiPanSplitScreen_PatientLetters.AutoHide = False
            '    'Else
            '    '    If clsSplit_PatientLetters.dck = 0 Then
            '    '        uiPanSplitScreen_PatientLetters.AutoHide = True
            '    '    End If
            'End If

            ''Bug No. 42498::20130103::Focus Set on Word Document after loading Split Control
            If Not IsNothing(oCurDoc) Then
                oCurDoc.ActiveWindow.SetFocus()
                oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
            End If
            ''

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Load, ex, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Load, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(objclsPatientLetters) Then
                '23-Apr-13 Aniket: Resolving Memory Leaks
                objclsPatientLetters.Dispose()
                objclsPatientLetters = Nothing
            End If
        End Try
    End Sub





    '''' <summary>
    ''''  For exiting Letters    
    '''' </summary>
    '''' <param name="dtLetter"></param>
    '''' <remarks></remarks>
    Private Sub Fill_PatientLetter(ByVal dtLetter As DataTable)
        cmbTemplate.SelectedValue = m_TemplateID
        If Not IsNothing(dtLetter) Then
            If dtLetter.Rows.Count > 0 Then
                dtLetterdate.Value = Format(dtLetter.Rows(0)(0), "MM/dd/yyyy hh:mm tt")
                ChkRemiderForUnSchedle.Checked = dtLetter.Rows(0)("bisUnscheduledCare")
                If (dtLetter.Rows(0)("nCommunicationType") > 0) Then
                    CmbCommunicationPref.SelectedValue = dtLetter.Rows(0)("nCommunicationType")
                    'Else
                    '    CmbCommunicationPref.SelectedIndex = -1
                End If
                ''slr free objword
                If Not IsNothing(objWord) Then
                    objWord = Nothing
                End If
                objWord = New clsWordDocument
                Dim strFileName As String
                strFileName = ExamNewDocumentName
                strFileName = objWord.GenerateFile(dtLetter.Rows(0)(1), strFileName)
                'objWord = Nothing
                LoadWordUserControl(strFileName, False)
                strFileName = Nothing
                'Set the Start postion of the cursor in documents
                oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                oCurDoc.Saved = True
            Else
                wdPatientLetter.Close()
            End If
        Else
            wdPatientLetter.Close()
        End If
    End Sub

    ''to open the Concerned patient Letter frpm DB in DSO Control
    Private Sub Fill_TemplateGallery()
        Try
            Dim strFileName As String
            ''slr free objword
            If Not IsNothing(objWord) Then
                objWord = Nothing
            End If
            If (IsNothing(objCriteria) = False) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If

            objWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Template
            objCriteria.PrimaryID = cmbTemplate.SelectedValue
            objWord.DocumentCriteria = objCriteria

            'Retrieving the Patient Education from DB and Save it as Physical File
            strFileName = objWord.RetrieveDocumentFile()
            ' objCriteria = Nothing
            ' objWord = Nothing
            If (IsNothing(strFileName) = False) Then
                If strFileName <> "" Then
                    LoadWordUserControl(strFileName, True)
                    If (IsNothing(oCurDoc) = False) Then
                        'Set the Start postion of the cursor in documents
                        oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    End If
                Else
                    wdPatientLetter.Close()
                End If
            Else
                wdPatientLetter.Close()
            End If

            strFileName = Nothing
        Catch ex As Exception
            ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SetWordObjectEntry(ByVal IsFinished As Boolean)
        If (IsNothing(oCurDoc)) Then
            Return
        End If
        Try
            'code added by :Dipak 20090922 for set instance of clsWordDocument class if objword is nothing
            If (isHandlerRemoved) Then
                RemoveWordHandlers()
                AddWordHandlers()
                isHandlerRemoved = False
            End If
        Catch ex As Exception
            '   gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try

        oCurDoc.FormFields.Shaded = False
        oCurDoc.ActiveWindow.SetFocus()

        If IsFinished = True Then
            If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
            End If

            tmrDocProtect.Enabled = True
            tmrDocProtect.Interval = 1
        Else
            If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Unprotect()
            End If

            tmrDocProtect.Enabled = False
        End If

    End Sub

    Private Sub tmrDocProtect_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDocProtect.Tick
        Try
            tmrDocProtect.Enabled = False
            If m_IsFinished = True And blnIsAddendum = False Then
                If Not oCurDoc Is Nothing Then
                    ''oCurDoc.Application.ActiveWindow.SetFocus()
                    Dim protectPane As Wd.TaskPane = oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection)
                    If (IsNothing(protectPane) = False) Then
                        protectPane.Visible = False
                        Marshal.ReleaseComObject(protectPane)
                        protectPane = Nothing
                    End If
                    'oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            tmrDocProtect.Enabled = True
        End Try
    End Sub

    Private Function SavePatientLetter(ByVal IsFinished As Boolean, Optional ByVal IsClose As Boolean = False) As Boolean
        ''slr free previous memory
        'If Not IsNothing(objclsPatientLetters) Then
        '    objclsPatientLetters.Dispose()
        '    objclsPatientLetters = Nothing
        'End If
        Dim objclsPatientLetters As clsPatientLetters
        objclsPatientLetters = New clsPatientLetters
        If pnlGloUC_TemplateTreeControl.Width.ToString <> "" Then
            objclsPatientLetters.SaveWidthInDatabase(gnLoginID, pnlGloUC_TemplateTreeControl.Width) ''''save width of panel in DB
        End If

        Try
            If oCurDoc Is Nothing Then
                Return False
            End If
            TurnoffMicrophone()
            SavePatientLetter = False

            'IF IsFinished = True Then Remove the Unused Fields 
            If IsFinished = True Then


                '   gloWord.LoadAndCloseWord.LockFields(oCurDoc)


                oCurDoc.ActiveWindow.SetFocus()
                If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                    oCurDoc.Application.ActiveDocument.Unprotect()
                End If
                ''slr free previous memory


                'objWord = Nothing

                'objWord = New clsWordDocument
                'objWord.CurDocument = oCurDoc
                'objWord.CleanupDoc()
                'oCurDoc = objWord.CurDocument
                'objWord = Nothing
                gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)
                gloWord.LoadAndCloseWord.LockFields(oCurDoc)
            End If
            'If (IsNothing(oCurDoc) = False) Then
            '    oCurDoc.Save()
            'Else
            '   wdPatientLetter.Save()
            'gloWord.LoadAndCloseWord.SaveDSO(wdPatientLetter, oCurDoc, oWordApp)
            ''End If


            'Dim strFileName As String
            'Dim isExceptionWhileCopingFile As Boolean = False
            'strFileName = ExamNewDocumentName

            'Try
            '    FileSystem.FileCopy(oCurDoc.FullName, strFileName)
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Save, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    ex = Nothing
            '    isExceptionWhileCopingFile = True
            'End Try

            'If (isExceptionWhileCopingFile) Then
            '    oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            '    wdPatientLetter.Close()
            'End If
            Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdPatientLetter, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, IsClose)

            Dim myBinaray As Object = Nothing
            If (IsNothing(myByte) = False) Then
                myBinaray = CType(myByte, Object)
            End If
            m_LetterID = objclsPatientLetters.SavePatientLetterBytes(m_LetterID, m_PatientID, cmbTemplate.SelectedValue, Format(dtLetterdate.Value, "MM/dd/yyyy hh:mm tt"), myBinaray, cmbTemplate.Text, IsFinished, ChkRemiderForUnSchedle.Checked, CmbCommunicationPref.SelectedValue)

            If m_LetterID > 0 Then
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
                    dtLetterdate.Enabled = False

                    'sarika 20081201 template name editable after save Set the Start postion of the cursor in documents
                    ' oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    oCurDoc.Saved = True
                    clsSplit_PatientLetters.loadSplitControlData(m_PatientID, m_visitID, uiPanSplitScreen_PatientLetters.SelectedPanel.Name, objCriteria, objWord, gnClinicID)
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
                    'oCurDoc.Saved = False

                End If

            End If
            'If IO.File.Exists(strFileName) Then
            '    Try
            '        IO.File.Delete(strFileName)
            '    Catch ex As Exception
            '        'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Save, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '        ex = Nothing
            '    End Try
            'End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Save, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        Finally
            If Not IsNothing(objclsPatientLetters) Then
                '23-Apr-13 Aniket: Resolving Memory Leaks
                objclsPatientLetters.Dispose()
                objclsPatientLetters = Nothing
            End If
        End Try
    End Function

    Private Sub Print()
        If Not oCurDoc Is Nothing Then
            GeneratePrintFaxDocument()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PrintReceipt, gloAuditTrail.ActivityType.Print, "Patient Letter Printed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If
    End Sub

    Private Sub GeneratePrintFaxDocument(Optional ByVal IsPrintFlag As Boolean = True)

        Dim PageNo As Integer = 0
        Dim totalPages As Integer = 0
        Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
        Dim Missing As Object = System.Reflection.Missing.Value

        Dim _SaveFlag As Boolean = False

        Try

            If Not oCurDoc Is Nothing Then


                If oCurDoc.Saved Then
                    _SaveFlag = True
                End If

                If IsNothing(wdPatientLetter) = False AndAlso IsNothing(oWordApp) = False Then


                    Try
                        gloWord.LoadAndCloseWord.SaveDSO(wdPatientLetter, oCurDoc, oWordApp)
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
                        PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, m_PatientID, AddressOf FaxPatientLetters, totalPages, PageNo:=PageNo, iOwner:=Me)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Generate, ex, gloAuditTrail.ActivityOutCome.Failure)
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
            MessageBox.Show(ex.ToString, "Patient Letters", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Public Sub GetPrescription()
        TurnoffMicrophone()
        Try
            dtLetterdate.Tag = GenerateVisitID(dtLetterdate.Value, m_PatientID)
        Catch ex As Exception

        End Try


        Dim frmRxMeds As frmPrescription
        frmRxMeds = frmPrescription.GetInstance(CType(dtLetterdate.Tag, Long), CType(m_PatientID, Long))

        If IsNothing(frmRxMeds) = True Then
            Exit Sub
        End If

        If frmPrescription.IsOpen = False Then
            frmRxMeds.ShowMedication()

            If frmRxMeds.blncancel = True Then
                With frmRxMeds
                    .WindowState = FormWindowState.Maximized
                    .myCallerL = Me
                    .blnOpenFromLetter = True
                    .ShowDialog(IIf(IsNothing(frmRxMeds.Parent), Me, frmRxMeds.Parent))
                End With
                If Not IsNothing(frmRxMeds) Then
                    frmRxMeds.Dispose()
                    frmRxMeds = Nothing
                End If
            End If
        Else
            MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    '''' <summary>
    '''' Replace the Form fields in document with Uopdated DB
    '''' </summary>
    '''' <param name="_DocType"></param>
    '''' <remarks></remarks>

    'Public Sub GetdataFromOtherForms(ByVal ListDoctType As List(Of gloEMRWord.enumDocType)) Implements IWord.GetdataFromOtherForms
    '    If Not oCurDoc Is Nothing Then
    '        oCurDoc.ActiveWindow.SetFocus()

    '        ObjWord = Nothing
    '        InitialiseWordObject()


    '        objWord.WaitControlPanel = Me.pnlWDPatientLetter

    '        ObjWord.CurDocument = oCurDoc
    '        objWord.DisableWordRefresh = True
    '        Try
    '            objWord.DisableDSORefresh()

    '            For Each element As gloEMRWord.enumDocType In ListDoctType
    '                objWord.GetFormFieldData(element)
    '            Next
    '        Catch ex As Exception
    '            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '            ex = Nothing
    '        Finally
    '            objWord.EnableDSORefresh()
    '        End Try

    '        oCurDoc = ObjWord.CurDocument
    '        objCriteria = Nothing
    '        ObjWord.Dispose()
    '        ObjWord = Nothing

    '        oWordApp = oCurDoc.Application
    '        If Not oWordApp Is Nothing Then
    '            Try
    '                If (isHandlerRemoved) Then
    '                    RemoveWordHandlers()
    '                    AddWordHandlers()
    '                    isHandlerRemoved = False
    '                End If
    '            Catch ex As Exception
    '                UpdateVoiceLog(ex.ToString)
    '                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '                ex = Nothing
    '            Finally
    '                objCriteria = Nothing
    '                objWord = Nothing
    '            End Try
    '        End If
    '    End If
    'End Sub
    Public Sub GetdataFromOtherForms(ByVal _DocType As gloEMRWord.enumDocType) Implements IWord.GetdataFromOtherForms
        If Not IsNothing(oCurDoc) Then

            oCurDoc.ActiveWindow.SetFocus()
            InitialiseWordObject()
            If Not IsNothing(objWord) Then
                If objWord.CurDocument Is Nothing Then '' SUDHIR 20091027 ''
                    objWord.CurDocument = oCurDoc
                End If
                objWord.GetFormFieldData(_DocType)
                oCurDoc = objWord.CurDocument
            End If


            'Added beacuse of handlers are removed when other word related forms are opened
            oWordApp = oCurDoc.Application
            If Not oWordApp Is Nothing Then
                Try
                    If (isHandlerRemoved) Then
                        RemoveWordHandlers()
                        AddWordHandlers()
                        isHandlerRemoved = False
                    End If
                Catch ex As Exception
                    UpdateVoiceLog(ex.ToString)
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                Finally
                    ' objCriteria = Nothing
                    '  objWord = Nothing
                End Try
            End If
        End If
    End Sub

    Public Sub GetOrders()
        TurnoffMicrophone()
        Try
            dtLetterdate.Tag = GenerateVisitID(dtLetterdate.Value, m_PatientID)
        Catch ex As Exception

        End Try

        If Trim(strPatientFirstName) <> "" Then
            Dim frmOrders As frm_LM_Orders
            frmOrders = frm_LM_Orders.GetInstance(dtLetterdate.Tag, dtLetterdate.Value, m_PatientID, 0, False)
            If IsNothing(frmOrders) Then
                Exit Sub
            End If
            With frmOrders
                .MdiParent = Me.MdiParent
                .WindowState = FormWindowState.Maximized
                .myCaller1 = Me
                .Show()
            End With
        Else
            MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
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
            objCriteria.PatientID = m_PatientID
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
                Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                If wdRng.Tables.Count > 0 Then
                    oCurDoc.Application.Selection.EndKey()
                End If
                'end

                oCurDoc.Application.Selection.TypeParagraph()
                'Dim clsExam As New clsPatientExams
                'clsExam = Nothing
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Patient Letters", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, objErr, gloAuditTrail.ActivityOutCome.Failure)
            objErr = Nothing
        End Try

    End Sub

    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            Dim objWord As New clsWordDocument
            '' Dim oclsProvider As New clsProvider slr not used
            Dim clsExam As New clsPatientExams
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, m_PatientID, 0, blnSignClick)

            'objCriteria = Nothing
            'objWord = Nothing
            If pSign(2) = "1" Then
                If File.Exists(pSign(0)) Then
                    oCurDoc.ActiveWindow.SetFocus()

                    Dim oWord As New clsWordDocument
                    oWord.CurDocument = oCurDoc
                    Dim myType As Wd.WdViewType = Nothing
                    Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                    oWord.InsertImage(pSign(0))
                    oWord = Nothing

                    'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                    Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                    If wdRng.Tables.Count > 0 Then
                        oCurDoc.Application.Selection.EndKey()
                    End If
                    'end code added by dipak 

                    oCurDoc.Application.Selection.TypeParagraph()
                    '' By Mahesh Signature With Date - 20070113
                    '' Add Date Time When Signature is Inserted
                    oCurDoc.Application.Selection.TypeText(Text:=pSign(1))
                    gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
            'Dispose object by mitesh
            If Not IsNothing(clsExam) Then
                clsExam.Dispose()
                clsExam = Nothing

            End If

        Catch objErr As Exception
            ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr, gloAuditTrail.ActivityOutCome.Failure)
            objErr = Nothing
        End Try
    End Sub

    Public Sub InsertCoSignature()
        Try
            If (IsNothing(objWord) = False) Then
                objWord = Nothing
            End If
            If (IsNothing(objCriteria) = False) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If
            objWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = m_PatientID
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID '' For inserting coSignature
            objWord.DocumentCriteria = objCriteria

            ImagePath = objWord.getData_FromDB("User_MST.imgSignature", "Co-Signature")
            '  objCriteria = Nothing
            ' objWord = Nothing
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
                'By Mahesh Signature With Date - 20070113 Add Date Time When Signature is Inserted
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Co-Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr, gloAuditTrail.ActivityOutCome.Failure)
            objErr = Nothing
        End Try
    End Sub

    Private Sub FaxPatientLetters(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)
        mdlFAX.Owner = Me
        If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientLetters, m_PatientID, "", "", cmbTemplate.SelectedItem(1), 0, 0, 0, True, Me) = False Then
            Exit Sub
        End If
        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
        'oTempDoc.ActiveWindow.SetFocus()
        ''Unprotect the document

        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If

        'Send the document for Printing i.e. to generate the TIFF File
        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, m_PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, cmbTemplate.Text, clsPrintFAX.enmFAXType.PatientLetters) = False Then
            'TIFF File has not been created
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        ''slr free it
        If Not IsNothing(objPrintFAX) Then
            objPrintFAX.Dispose()
        End If
        objPrintFAX = Nothing
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
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr, gloAuditTrail.ActivityOutCome.Failure)
            objErr = Nothing
        End Try
    End Sub

    Public Sub AddSignature(ByVal sImagePath As String) Implements ISignature.AddSignature
        'Dhruv 20091214 To add the signature into the Word document
        If Not IsNothing(oCurDoc) Then
            If File.Exists(sImagePath) Then
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

    Private Sub loadPatientStrip()
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.PatientLetter)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(0, 0, 3, 0)
        _PatientStrip.BringToFront()
        pnlwdPatLetter.Controls.Add(_PatientStrip)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Undo, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Redo, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ImportDocument(ByVal nInsertScan As Int16)
        'Insert File - 1
        'Scan Images - 2
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        uiPanSplitScreen_PatientLetters.Enabled = False
        oCurDoc.ActiveWindow.SetFocus()
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
                        'Set focus to Wd object
                        oCurDoc.ActiveWindow.SetFocus()
                        'Statement to Go end of Selelcted Wd Document

                        'Insert file in Wd dobject
                        oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                    End If
                End If
                If Not IsNothing(oFileDialogWindow) Then
                    oFileDialogWindow.Dispose()
                    oFileDialogWindow = Nothing
                End If
            ElseIf nInsertScan = 2 Then

                Dim oFiles As New ArrayList()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()

                'Commented BY Rahul Patel on 26-10-2010
                'gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'Added by Rahul Patel on 26-10-2010
                'For changing the DMS Hybrid database change.

                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)

                'End of code added by Rahul Patel on 26-10-2010.

                oEDocument.ShowEScannerForImages(m_PatientID, oFiles)
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
                        '' END SUDHIR ''

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
                ''slr free ofiles
                oFiles.Clear()
                oFiles = Nothing

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            uiPanSplitScreen_PatientLetters.Enabled = True
        End Try
    End Sub

    Private Sub InsertAddendum()
        Try
            If m_IsFinished = True Then
                ''slr free previous memory
                If Not IsNothing(ouctlgloUC_Addendum) Then
                    ouctlgloUC_Addendum.Dispose()
                    ouctlgloUC_Addendum = Nothing
                End If
                ouctlgloUC_Addendum = New gloUC_Addendum(0, m_LetterID, m_PatientID)
                blnIsAddendum = True
                pnlgloUC_Addendum.Controls.Add(ouctlgloUC_Addendum)

                ouctlgloUC_Addendum.Dock = DockStyle.Fill
                ouctlgloUC_Addendum.BringToFront()

                pnlgloUC_Addendum.Visible = True
                pnlgloUC_Addendum.BringToFront()

                Me.SuspendLayout()
                GloUC_TemplateTreeControl_PatientLetters.Visible = False
                pnlGloUC_PastWordNotes.Visible = False
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Visible = False
                End If

                pnlToolStrip.Visible = False
                CmbCommunicationPref.Visible = False
                ChkRemiderForUnSchedle.Visible = False
                Me.ResumeLayout()
                If gblnSpeakerExists = True And gblnVoiceEnabled = True Then
                    InitializeVoiceObjectForAddendum()
                    ShowMicroPhone()
                End If
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, objErr, gloAuditTrail.ActivityOutCome.Failure)
            objErr = Nothing
        End Try
    End Sub

    Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectionChangeCommitted
        'Dim WDocViewType As Wd.WdViewType
        'Dim wordOptimizer As New WordRefresh()

        Try
            TurnoffMicrophone()
            FillReminderforUnscheduledCare()
            If cmbTemplate.SelectedValue > 0 Then
                blnCmbSelTemplate = True
                'Ashish added on 1st October 
                'to prevent screen from refreshing

                'WDocViewType = oCurDoc.ActiveWindow.View.Type

                'wordOptimizer.OptimizePerformance(False, oCurDoc, 0)
                'wordOptimizer.ShowPanel(pnlWDPatientLetter)
                Fill_TemplateGallery()

                blnCmbSelTemplate = False


            Else
                wdPatientLetter.Close()
            End If
        Catch ex As Exception
            blnCmbSelTemplate = False
            ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'Ashish added on 31st October 
            'to prevent screen from refreshing

            'wordOptimizer.OptimizePerformance(True, oCurDoc, WDocViewType)
            'wordOptimizer.HidePanel(pnlWDPatientLetter)
            'wordOptimizer.Dispose()
            'WDocViewType = Nothing
        End Try
    End Sub

    Private Sub loadToolStrip()
        If Not tlsPatientLetter Is Nothing Then
            tlsPatientLetter.Dispose()
        End If

        tlsPatientLetter = New WordToolStrip.gloWordToolStrip
        tlsPatientLetter.Dock = DockStyle.Top
        tlsPatientLetter.ConnectionString = GetConnectionString()
        tlsPatientLetter.UserID = gnLoginID

        ''Added on 20101007 by sanjog for signature
        tlsPatientLetter.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider
        tlsPatientLetter.ptProvider = oclsProvider.GetPatientProviderName(m_PatientID)
        tlsPatientLetter.ptProviderId = oclsProvider.GetPatientProvider(m_PatientID)
        ''Added on 20101007 by sanjog for signature
        If Not IsNothing(oclsProvider) Then
            oclsProvider.Dispose()
            oclsProvider = Nothing
        End If

        If m_IsFinished Then
            tlsPatientLetter.FormType = WordToolStrip.enumControlType.Addendum
            cmbTemplate.Enabled = False
            dtLetterdate.Enabled = False
        Else
            tlsPatientLetter.IsCoSignEnabled = gblnCoSignFlag
            tlsPatientLetter.FormType = WordToolStrip.enumControlType.PatientLetters
            cmbTemplate.Enabled = True
            dtLetterdate.Enabled = True
        End If

        Me.pnlToolStrip.Controls.Add(tlsPatientLetter)
        Me.pnlToolStrip.Size = New System.Drawing.Size(940, 56)
        pnlToolStrip.SendToBack()

        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsPatientLetter
                ShowMicroPhone()
            End If
        End If

        If gblnAssociatedProviderSignature And m_IsFinished = False Then
            tlsPatientLetter.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            tlsPatientLetter.MyToolStrip.ButtonsToHide.Remove(tlsPatientLetter.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        ElseIf m_IsFinished = True Then

            tlsPatientLetter.MyToolStrip.Items("Save").Visible = False
            If (tlsPatientLetter.MyToolStrip.ButtonsToHide.Contains(tlsPatientLetter.MyToolStrip.Items("Save").Name) = False) Then
                tlsPatientLetter.MyToolStrip.ButtonsToHide.Add(tlsPatientLetter.MyToolStrip.Items("Save").Name)
            End If
            tlsPatientLetter.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsPatientLetter.MyToolStrip.ButtonsToHide.Contains(tlsPatientLetter.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsPatientLetter.MyToolStrip.ButtonsToHide.Add(tlsPatientLetter.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If

        ElseIf gblnAssociatedProviderSignature = False Then
            tlsPatientLetter.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsPatientLetter.MyToolStrip.ButtonsToHide.Contains(tlsPatientLetter.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsPatientLetter.MyToolStrip.ButtonsToHide.Add(tlsPatientLetter.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If
        End If
        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
            If tlsPatientLetter.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                If (tlsPatientLetter.MyToolStrip.ButtonsToHide.Contains(tlsPatientLetter.MyToolStrip.Items("SecureMsg").Name) = False) Then
                    tlsPatientLetter.MyToolStrip.ButtonsToHide.Add(tlsPatientLetter.MyToolStrip.Items("SecureMsg").Name)
                End If
            End If
            If tlsPatientLetter.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                tlsPatientLetter.MyToolStrip.Items("SecureMsg").Visible = False
            End If
        End If
    End Sub

    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
    Private Sub tlsPatientLetter_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsPatientLetter.ToolStripButtonClick
        If IsNothing(oCurDoc) = False Then
            InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
        End If
    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        Try
            If blnCmbSelTemplate = False Then
                loadToolStrip()
            End If
            '  wdPatientLetter.Open(strFileName)
            ' Dim oWordApp As Wd.Application = Nothing
            Dim strError = gloWord.LoadAndCloseWord.OpenDSO(wdPatientLetter, strFileName, oCurDoc, oWordApp)
            If (strError <> String.Empty) Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, strError, gloAuditTrail.ActivityOutCome.Failure)

            Else


                If blnGetData Then
                    'To retrieve the Form fields for the Word document
                    ''slr free previous memory
                    objWord = Nothing
                    objWord = New clsWordDocument
                    If (IsNothing(objCriteria) = False) Then
                        objCriteria.Dispose()
                        objCriteria = Nothing
                    End If
                    objCriteria = New DocCriteria
                    objCriteria.DocCategory = enumDocCategory.Others

                    objCriteria.PatientID = m_PatientID
                    Try
                        m_visitID = GenerateVisitID(dtLetterdate.Value, m_PatientID)
                        dtLetterdate.Tag = GenerateVisitID(dtLetterdate.Value, m_PatientID)
                        objCriteria.VisitID = dtLetterdate.Tag
                    Catch ex As Exception

                    End Try
                  

                    objCriteria.PrimaryID = 0
                    objWord.DocumentCriteria = objCriteria
                    objWord.CurDocument = oCurDoc

                    'Replace Form fields with Concerned data
                    'objWord.GetFormFieldDataDBCalls(enumDocType.None)
                    objWord.GetFormFieldData(enumDocType.None)
                    oCurDoc = objWord.CurDocument
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                    'objCriteria = Nothing
                    ' objWord = Nothing
                Else
                    ''slr free previous memory
                    objWord = Nothing
                    objWord = New clsWordDocument
                    objWord.CurDocument = oCurDoc
                    objWord.HighlightColor()
                    oCurDoc = objWord.CurDocument
                    oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                    '  objWord = Nothing
                End If

                SetWordObjectEntry(m_IsFinished)
            End If
        Catch ex As Exception
            '  gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub GetHistory()
        TurnoffMicrophone()

        If Trim(strPatientFirstName) <> "" Then
            Try
                m_visitID = GenerateVisitID(dtLetterdate.Value, m_PatientID)
                dtLetterdate.Tag = m_visitID
            Catch ex As Exception

            End Try
           

            Dim blnRecordLock As Boolean = False

            Dim frmPatHis As New frmHistory(dtLetterdate.Tag, dtLetterdate.Value, m_PatientID, blnRecordLock)
            With frmPatHis
                .WindowState = FormWindowState.Maximized
                .blnOpenFromExam = True
                .myLetter = Me
                '.MdiParent = Me.ParentForm
                .PopulatePatientHistory_Final()
                If frmPatHis.blncancel Then
                    intflag = 1
                    .MdiParent = Me.ParentForm
                    .Show()
                    .BringToFront()
                    .WindowState = FormWindowState.Maximized
                Else
                    frmPatHis.Close() 'Change made to solve memory Leak and word crash issue
                    frmPatHis.Dispose()
                    frmPatHis = Nothing
                End If
            End With
        Else
            MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
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
                    ' r.SetRange(Sel.Start, Sel.End + 1)

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, excp, gloAuditTrail.ActivityOutCome.Failure)
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

    Private Sub wdPatientLetter_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdPatientLetter.BeforeDocumentClosed
        Try
            '  oWordApp = oCurDoc.Application
            'CODE ADDED BY DIPAK 20091224 ''' TO HANDLE EMR-PM WORD INSTANCE
            garrOpenDocument.Remove(oCurDoc.FullName)
            'END CODE ADDED BY DIPAK 

            If (objWord Is Nothing) Then
                InitialiseWordObject()
            End If

            If Not oWordApp Is Nothing Then
                RemoveWordHandlers()
                isHandlerRemoved = True
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Close, "ERROR: At wdPatientLetter_BeforeDocumentClosed" & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Next
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Close, "ERROR: At wdPatientLetter_BeforeDocumentClosed" & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub wdPatientLetter_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdPatientLetter.OnDocumentClosed
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If

            'If Not oWordApp Is Nothing Then
            '    '  Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If
            ' ''slr free oTempdoc
            'If Not IsNothing(oTempDoc) Then
            '    Marshal.ReleaseComObject(oTempDoc)
            '    oTempDoc = Nothing
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'GC.Collect()  ''slr for memory management
            'GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub wdPatientLetter_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdPatientLetter.OnDocumentOpened
        oCurDoc = wdPatientLetter.ActiveDocument
        oWordApp = oCurDoc.Application
        'CODE ADDED BY DIPAK 20091224 ''' TO HANDLE EMR-PM WORD INSTANCE
        If Not (garrOpenDocument.Contains(oCurDoc.FullName)) Then
            garrOpenDocument.Add(oCurDoc.FullName)
        End If
        'END CODE ADDED BY DIPAK 
        Try
            If (isHandlerRemoved) Then
                InitialiseWordObject()
                RemoveWordHandlers()
                AddWordHandlers()
                isHandlerRemoved = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "ERROR:At wdPatientLetter_OnDocumentOpened \n" & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
    End Sub

    Private Sub tlsPatientLetter_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsPatientLetter.ToolStripClick
        Try

            Select Case e.ClickedItem.Name
                'Case "Show/Hide"
                '    ShowHide_PastExam()
                Case "Mic"
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Open, "SwitchOff Mic started from tblButtons_ButtonClick in Patient Letters when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                    'UpdateVoiceLog("--------------SwitchOff Mic started from tblButtons_ButtonClick in Patient Letters when " & e.ClickedItem.Name & " is invoked------------")
                    If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                        e.ClickedItem.ToolTipText = "Microphone Off"
                    ElseIf MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                        e.ClickedItem.ToolTipText = "Microphone On"
                    End If
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Open, "SwitchOff Mic Completed from tblButtons_ButtonClick in Patient Letters when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)

                Case "Save"
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        TurnoffMicrophone()
                        If m_IsFinished Then
                            Call SavePatientLetter(True)
                        Else
                            Call SavePatientLetter(False)
                        End If

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                Case "Save & Close"
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        Call SavePatientLetter(False, True)
                        Me.Close()

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                Case "Print"
                    TurnoffMicrophone()
                    Call Print()
                    Try
                        cmbTemplate.Enabled = False
                        dtLetterdate.Enabled = False
                    Catch ex As Exception

                    End Try
                   

                Case "FAX"
                    bnlIsFaxOpened = True
                    TurnoffMicrophone()
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        Call GeneratePrintFaxDocument(False)
                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    Try
                        cmbTemplate.Enabled = False
                        dtLetterdate.Enabled = False
                    Catch ex As Exception

                    End Try
                   
                    bnlIsFaxOpened = False
                Case "Insert Sign"

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
                    TurnoffMicrophone()
                    ImportDocument(1)
                Case "Scan Documents"
                    TurnoffMicrophone()
                    ImportDocument(2)
                Case "Close"
                    TurnoffMicrophone()
                    Me.Close()
                Case "Prescription"
                    TurnoffMicrophone()
                    Call GetPrescription()

                Case "OrderTemplates"
                    TurnoffMicrophone()
                    Call GetOrders()
                Case "Save & Finish"
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        Call SavePatientLetter(True, True)
                        Me.Close()

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                Case "Add Addendum"
                    Call InsertAddendum()
                    '' chetan integrated for strike through on 23-oct-2010
                Case "tblbtn_StrikeThrough"
                    InsertStrike()
                Case "Export"
                    ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    If (IsNothing(oCurDoc) = False) Then
                        Dim objword1 As clsWordDocument
                        objword1 = New clsWordDocument
                        Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Patient letter", Me)
                        If Result = True Then
                            MessageBox.Show("Document Exported Successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        objword1 = Nothing

                    End If
                    ' Export Function for Word Docs Integrated by dipak  as on 26 oct 2010
                Case "SecureMsg"
                    If strProviderDirectAddress <> "" Then
                        Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(m_PatientID)
                        If sError <> "" Then
                            MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                            Return
                        Else
                            TurnoffMicrophone()
                            Call SendSecureMsg()
                            Try
                                cmbTemplate.Enabled = False
                                dtLetterdate.Enabled = False

                            Catch ex As Exception

                            End Try
                        End If

                    Else
                        MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub SendSecureMsg()

        If Not oCurDoc Is Nothing Then
            GenerateSecureMsgDocument()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.ClinicalExchange, "Send Patient Letter using Secure Message", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        End If

    End Sub
    Private Sub GenerateSecureMsgDocument()
        Dim _SaveFlag As Boolean = False
        If oCurDoc.Saved Then
            _SaveFlag = True
        End If
        Try
            gloWord.LoadAndCloseWord.SaveDSO(wdPatientLetter, oCurDoc, oWordApp)
        Catch ex As Exception

        End Try
        'Try
        '    oCurDoc.SaveAs(oCurDoc.FullName)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    Try
        '        oCurDoc.Save()
        '    Catch ex1 As Exception

        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        '    End Try
        'End Try
        'Dim sFileName As String = ExamNewDocumentName

        '  wdPTProtocols.Save(sFileName, True, "", "")
        'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
        '        oCurDoc.Saved = _SaveFlag
        ' wdPatientLetter.Close()

        'wdTemp = New AxDSOFramer.AxFramerControl

        'Me.Controls.Add(wdTemp)
        'wdTemp.Location = New System.Drawing.Point(-50, -50)

        'wdTemp.Open(sFileName)
        'oTempDoc = wdTemp.ActiveDocument
        'oTempDoc.ActiveWindow.SetFocus()
        Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
        Dim osenddox As String = String.Empty
        Try
            osenddox = SendWord.MdlSendWord.SendWordDocument(myLoadWord, oCurDoc.FullName, m_PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If

        'Dim oSendDoc As New clsPrintFAX
        'Dim osenddox As String
        'osenddox = oSendDoc.SendDoc(oTempDoc, m_PatientID)
        ' ''slr free osenddoc
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
                Dim ofrmSendNewMail As New InBox.NewMail(m_PatientID, osenddox)
                ofrmSendNewMail.ShowInTaskbar = True
                ofrmSendNewMail.ShowDialog()
                'ofrmInbox.Dispose()
                ''slr close it
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
        'Set the Start postion of the cursor in documents
        'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        'oCurDoc.Saved = _SaveFlag
    End Sub
    'chetan integrated for strike through on 23-oct-2010
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If m_IsFinished = True Then
                tmrDocProtect.Enabled = True
            End If
        End Try
    End Sub

    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
    Private Function AddChildMenu() As DataTable
        Dim oProvider As New clsProvider
        Try

            Dim rslt As Boolean
            rslt = oProvider.CheckSignDelegateStatus()
            If rslt Then
                Dim dt As DataTable
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(oProvider) Then
                oProvider.Dispose()
                oProvider = Nothing
            End If
        End Try
    End Function

#Region "Voice implementation"

    Public Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent) Implements mdlgloVoice.gloVoice.CustomGetchanges

    End Sub

    Public Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent) Implements mdlgloVoice.gloVoice.CustomMakechanges

    End Sub

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate

        Try

            If strstring = "ON" Then
                If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsPatientLetter.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsPatientLetter.MyToolStrip.Items("Mic").Visible = True
                        tlsPatientLetter.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                        tlsPatientLetter.MyToolStrip.ButtonsToHide.Remove(tlsPatientLetter.MyToolStrip.Items("Mic").Name)
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
                    If Not tlsPatientLetter.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsPatientLetter.MyToolStrip.Items("Mic").Visible = True
                        tlsPatientLetter.MyToolStrip.ButtonsToHide.Remove(tlsPatientLetter.MyToolStrip.Items("Mic").Name)
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
                    If Not tlsPatientLetter.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsPatientLetter.MyToolStrip.Items("Mic").Visible = False

                        If tlsPatientLetter.MyToolStrip.ButtonsToHide.Contains(tlsPatientLetter.MyToolStrip.Items("Mic").Name) = False Then
                            tlsPatientLetter.MyToolStrip.ButtonsToHide.Add(tlsPatientLetter.MyToolStrip.Items("Mic").Name)
                        End If

                    End If



                End If

                '04-Jul-14 Aniket: Resolving Bug #67037
                If Not tlsPatientLetter.MyToolStrip.Items("Mic") Is Nothing Then
                    tlsPatientLetter.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
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
                        Catch ex2 As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex2, gloAuditTrail.ActivityOutCome.Failure)
                            ex2 = Nothing
                        End Try
                        '  oCurDoc.ActiveWindow.SetFocus()
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
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex, gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        End If
                    Next
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#End Region

    Private Sub frmPatientLetter_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If IsModify = False Then
                'History Alerts
                'Check if History Of the Date is Exists or Not
                'SLR: Free previous memory
                objWord = Nothing
                objWord = New clsWordDocument(m_PatientID)
                'If objWord.getHistoryAfterDate(dtLetterdate.Value, dtLetterdate.Tag) = True Then

                If GetPatientHistory(dtLetterdate.Value) = True Then
                    'If History of Patient is Not Entered then 
                    'Create History Alert

                    'Open History Form
                    blnOpenHistory = True
                End If

                If blnOpenHistory = True Then
                    GetHistory()
                End If

                blnOpenHistory = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'If Not IsNothing(objWord) Then
            '    objWord = Nothing

            'End If
        End Try
    End Sub

    Public Function GetPatientHistory(ByVal dtDOS As Date) As Boolean
        Dim cls As New clsPatientExams
        Dim dt As DataTable = Nothing
        Try



            dt = cls.getPatientHistory(m_PatientID)
            If dt.Rows.Count > 0 Then
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                dt = cls.getHistoryVisitsAfterDate(dtDOS, m_PatientID)
                If dt.Rows.Count > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(cls) Then
                cls.Dispose()
                cls = Nothing
            End If
        End Try

    End Function

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
    ''' Activate Voice Commands
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
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
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.AddVoiceCommands()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.AddVoiceCommands()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Show microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone, IWord.ShowMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Turnoff microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone, IWord.TurnOffMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
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
    ''' Add Basic Voice commands to hashtable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBasicVoiceCommands() As Hashtable
        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save Letter", "Save")
        oHashtable.Add("Print Letter", "Print")
        oHashtable.Add("Fax Letter", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close Letter", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close Letter", "Close")
        oHashtable.Add("Finish Letter", "Save & Finish")
        oHashtable.Add("Prescription", "Prescription")
        Return oHashtable
    End Function

    ''' <summary>
    ''' Initialise glovoice class
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeVoiceObject()
        If Not IsNothing(ogloVoice) Then
            ogloVoice.DelWordToolStripClick = Nothing
            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If
        Dim oHashtable As Hashtable = AddBasicVoiceCommands()
        ogloVoice = New ClsVoice(oHashtable)

        oHashtable = Nothing

        ogloVoice.MyWordToolStrip = Me.tlsPatientLetter
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "Letter"
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Letters
        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsPatientLetter_ToolStripClick)
    End Sub

    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
        End Get
    End Property

    Private Sub ouctlgloUC_Addendum_OnAddendumClose(ByVal sender As Object, ByVal e As System.EventArgs) Handles ouctlgloUC_Addendum.OnAddendumClose
        pnlgloUC_Addendum.Controls.Remove(ouctlgloUC_Addendum)
        pnlToolStrip.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        pnlgloUC_Addendum.Visible = False
        pnlWDPatientLetter.Visible = True
        pnlWDPatientLetter.BringToFront()
        pnlGloUC_PastWordNotes.Visible = True
        ChkRemiderForUnSchedle.Visible = True
        If ChkRemiderForUnSchedle.Checked Then
            CmbCommunicationPref.Visible = True
        End If
        blnIsAddendum = False
        TurnoffMicrophone()
        If (IsNothing(ouctlgloUC_Addendum) = False) Then
            ouctlgloUC_Addendum.Dispose()
            ouctlgloUC_Addendum = Nothing
        End If
    End Sub

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
        pnlToolStrip.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        pnlgloUC_Addendum.Visible = False
        pnlWDPatientLetter.Visible = True
        pnlWDPatientLetter.BringToFront()
        pnlGloUC_PastWordNotes.Visible = True

        blnIsAddendum = False
        TurnoffMicrophone()
        If (IsNothing(ouctlgloUC_Addendum) = False) Then
            ouctlgloUC_Addendum.Dispose()
            ouctlgloUC_Addendum = Nothing
        End If
    End Sub

    Private Sub InitializeVoiceObjectForAddendum()
        If Not IsNothing(ogloVoice) Then

            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If

        Dim oAddendumHashtable As ArrayList = ouctlgloUC_Addendum.FillTemplateCommands(True)
        ogloVoice = New ClsVoice(oAddendumHashtable)
        ogloVoice.gloTreeView = ouctlgloUC_Addendum.trvTemplates
        ogloVoice.eVoiceAddendum = VoiceAddendum.eAddendum
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Letters
        ogloVoice.MyWordToolStrip = Me.ouctlgloUC_Addendum.tlsAddendum
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "PatientLetter"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf Me.ouctlgloUC_Addendum.onToolStripClick)
        ogloVoice.AddVoiceCommands()
    End Sub

#Region "Liquid Data fields implementation"

    Private Sub InitialiseWordObject()
        ''slr free objword
        objWord = Nothing
        objWord = New clsWordDocument
        If (IsNothing(objCriteria) = False) Then
            objCriteria.Dispose()
            objCriteria = Nothing
        End If

        objCriteria = New DocCriteria
        objCriteria.PatientID = m_PatientID
        If (IsNothing(dtLetterdate) = False) Then
            objCriteria.VisitID = GenerateVisitID(dtLetterdate.Value, m_PatientID)
        End If
        objWord.myCallingForm = Me
        Try
            objWord.CurDocument = wdPatientLetter.ActiveDocument
        Catch ex As Exception

        End Try

        objWord.DocumentCriteria = objCriteria
        '   objWord = Nothing
        ' objCriteria = Nothing
    End Sub

    ''' <summary>
    ''' Event handler for double click on liquid link field oWordApp_WindowBeforeDoubleClick
    ''' </summary>
    ''' <param name="Sel"></param>
    ''' <param name="Cancel"></param>
    ''' <remarks>added dipak 20091009</remarks>
    Private Sub oWordApp_WindowBeforeDoubleClick(ByVal Sel As Microsoft.Office.Interop.Word.Selection, ByRef Cancel As Boolean)
        If (m_IsFinished = False And tmrDocProtect.Enabled = False) Then '' Protected conditon 
            Cancel = False
            'Initilise objWordObject
            InitialiseWordObject()
            'call Event procedure OnFormClicked whre actual operation performed.
            objWord.PatientId = m_PatientID
            objWord.Get_PatientDetails(m_PatientID)
            objWord.OnFormClicked(Sel, Cancel)
        End If
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
            Return m_PatientID    'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub InitiliseTemplateTreeControl()
        Try
            Dim objDocriteria As New DocCriteria
            Dim objClsWord_TemplateTree As New clsWordDocument
            GloUC_TemplateTreeControl_PatientLetters.InitiliseControlParameter(GetConnectionString())

            GloUC_TemplateTreeControl_PatientLetters.DocCriteria = objDocriteria
            GloUC_TemplateTreeControl_PatientLetters.ObjClsWord = objClsWord_TemplateTree
            GloUC_TemplateTreeControl_PatientLetters.ProviderId = gnSelectedProviderID
            GloUC_TemplateTreeControl_PatientLetters.Fill_ExamTemplates(0)

            objDocriteria = Nothing
            objClsWord_TemplateTree = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub CallPastData()
        Try
            ''SLR: FRee previous memory
            'If Not IsNothing(obj_UCclsPatientLetters) Then
            '    obj_UCclsPatientLetters.Dispose()
            '    obj_UCclsPatientLetters = Nothing
            'End If
            ''SLR: FRee previous memory
            'If Not IsNothing(Obj_UCWord) Then
            '    Obj_UCWord = Nothing
            'End If
            ''SLR: FRee previous memory
            'If Not IsNothing(obj_UCCriteria) Then
            '    obj_UCCriteria.Dispose()
            '    obj_UCCriteria = Nothing
            'End If
            ''SLR: FRee previous memory
            'If Not IsNothing(clsPatientExams) Then
            '    clsPatientExams.Dispose()
            '    clsPatientExams = Nothing
            'End If


            Dim obj_UCclsPatientLetters As clsPatientLetters = New clsPatientLetters
            Dim Obj_UCWord As clsWordDocument = New clsWordDocument
            Dim obj_UCCriteria As DocCriteria = New DocCriteria
            Dim clsPatientExams As clsPatientExams = New clsPatientExams



            If (IsNothing(GloUC_PastWordNotes1.OBJCRITERIAs) = False) Then
                CType(GloUC_PastWordNotes1.OBJCRITERIAs, DocCriteria).Dispose()
                GloUC_PastWordNotes1.OBJCRITERIAs = Nothing
            End If

            If (IsNothing(GloUC_PastWordNotes1.OBJWORDs) = False) Then
                GloUC_PastWordNotes1.OBJWORDs = Nothing
            End If


            If (IsNothing(GloUC_PastWordNotes1.OBJCLSDISCLOSUREs) = False) Then

                GloUC_PastWordNotes1.OBJCLSDISCLOSUREs = Nothing
            End If

            If (IsNothing(GloUC_PastWordNotes1.CLSPATIENTEXAMSs) = False) Then
                CType(GloUC_PastWordNotes1.CLSPATIENTEXAMSs, clsPatientExams).Dispose()
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

          

            GloUC_PastWordNotes1.OBJCRITERIAs = obj_UCCriteria
            GloUC_PastWordNotes1.OBJWORDs = Obj_UCWord
            GloUC_PastWordNotes1.PATIENTIDs = m_PatientID
            GloUC_PastWordNotes1.OBJCLSPATIENTLETTERSs = obj_UCclsPatientLetters
            GloUC_PastWordNotes1.FromForms = "PatientLetter"
            GloUC_PastWordNotes1.CLSPATIENTEXAMSs = clsPatientExams
            GloUC_PastWordNotes1.ShowHide_PastExam()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'If Not IsNothing(obj_UCclsPatientLetters) Then
            '    '23-Apr-13 Aniket: Resolving Memory Leaks
            '    'Resolved bug No 50518:: Patient Letters - Application does not display past patient letters::20130514
            '    'obj_UCclsPatientLetters.Dispose()
            '    obj_UCclsPatientLetters = Nothing

            'End If
            'If Not IsNothing(Obj_UCWord) Then
            '    Obj_UCWord = Nothing
            'End If
            'If Not IsNothing(obj_UCCriteria) Then
            '    obj_UCCriteria = Nothing
            'End If
            'If Not IsNothing(clsPatientExams) Then
            '    clsPatientExams.Dispose()
            '    clsPatientExams = Nothing
            'End If
        End Try
    End Sub

    Private Sub Treeview_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs, ByVal sFilename As System.String) Handles GloUC_TemplateTreeControl_PatientLetters.Treeview_NodeMouseDoubleClick
        Dim WDocViewType As Wd.WdViewType
        'Dim wordOptimizer As New WordRefresh()
        Dim Node As TreeNode = Nothing
        Dim TreeView As TreeView = Nothing
        Dim blnRefreshDocument As Boolean
        Try

            oCurDoc = wdPatientLetter.ActiveDocument
            '  oCurDoc.Application.ScreenUpdating = False
            oCurDoc.ActiveWindow.SetFocus()

            If TypeOf (sender) Is TreeView Then
                TreeView = DirectCast(sender, TreeView)

                If TreeView.SelectedNode IsNot Nothing Then
                    Node = TreeView.SelectedNode

                    If Node IsNot Nothing AndAlso Node.Parent IsNot Nothing AndAlso Node.Parent.Text.ToLower() <> "tags" Then
                        WDocViewType = oCurDoc.ActiveWindow.View.Type
                        'wordOptimizer.OptimizePerformance(False, oCurDoc, 0)
                        'wordOptimizer.ShowPanel(Me.pnlWDPatientLetter)
                    End If
                End If
            End If

            '04-Jan-2016 Aniket: Warn the user if the full document is being replaced by tags
            If gloWord.LoadAndCloseWord.ValidateTagsSelectedRange(oCurDoc) = True Then
                oCurDoc.Application.Selection.InsertFile(sFilename, "", False, False, False)
                blnRefreshDocument = True
            End If

            wdPatientLetter.Select()
            If blnRefreshDocument = True Then
                GetdataFromOtherForms(enumDocType.None)
            End If

            ' oCurDoc.Application.ScreenUpdating = True
            If IO.File.Exists(sFilename) Then
                IO.File.Delete(sFilename)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If TypeOf (sender) Is TreeView Then
                TreeView = DirectCast(sender, TreeView)

                If TreeView.SelectedNode IsNot Nothing Then
                    Node = TreeView.SelectedNode

                    'If Node IsNot Nothing AndAlso Node.Parent IsNot Nothing AndAlso Node.Parent.Text.ToLower() <> "tags" Then
                    '    wordOptimizer.HidePanel(Me.pnlWDPatientLetter)
                    '    wordOptimizer.OptimizePerformance(True, oCurDoc, WDocViewType)
                    'End If
                End If


            End If

            TreeView = Nothing
            Node = Nothing
            'wordOptimizer.Dispose()
            'wordOptimizer = Nothing
        End Try
    End Sub

    Public Sub calltoAddRefreshButtonControl()
        Try
            'SLR: FRee previous memory
            If Not IsNothing(objWord) Then
                objWord = Nothing
            End If
            'SLR: FRee previous memory
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If

            objWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.PatientID = m_PatientID
            Try
                objCriteria.VisitID = GenerateVisitID(dtLetterdate.Value, m_PatientID)
            Catch ex As Exception

            End Try

            objWord.DocumentCriteria = objCriteria
            objWord.WaitControlPanel = pnlWDPatientLetter
            GloUC_AddRefreshDic1.CONNECTIONSTRINGs = GetConnectionString()
            GloUC_AddRefreshDic1.OBJWORDs = objWord
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
            GloUC_AddRefreshDic1.M_PATIENTIDs = m_PatientID
            GloUC_AddRefreshDic1.ObjFrom = Me
            GloUC_AddRefreshDic1.DTLETTERDATEs = dtLetterdate
            GloUC_AddRefreshDic1.OWORDAPPs = oWordApp
            GloUC_AddRefreshDic1.wdPatientWordDocs = wdPatientLetter
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            'If Not IsNothing(objWord) Then
            '    objWord = Nothing
            'End If
            'If Not IsNothing(objCriteria) Then
            '    objCriteria = Nothing
            'End If

        End Try
    End Sub

    Private Sub cmdPastExam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPastExam.Click
        Try
            If cmdPastExam.Text = "Show Past Patient Letters" Then
                pnlGloUC_PastWordNotes.Show()
                cmdPastExam.Text = "Hide Past Patient Letters"
                bIsPastExamClick = True
            ElseIf cmdPastExam.Text = "Hide Past Patient Letters" Then
                pnlGloUC_PastWordNotes.Hide()
                cmdPastExam.Text = "Show Past Patient Letters"
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub Dispose_Object()
        Try
            'If Not IsNothing(obj_UCclsPatientLetters) Then
            '    '23-Apr-13 Aniket: Resolving Memory Leaks
            '    obj_UCclsPatientLetters.Dispose()
            '    obj_UCclsPatientLetters = Nothing
            'End If
            'If Not IsNothing(Obj_UCWord) Then

            '    Obj_UCWord = Nothing
            'End If
            'If Not IsNothing(obj_UCCriteria) Then
            '    obj_UCCriteria = Nothing
            'End If
            'If Not IsNothing(clsPatientExams) Then
            '    clsPatientExams.Dispose()
            '    clsPatientExams = Nothing
            'End If
            If Not IsNothing(objWord) Then
                objWord = Nothing
            End If
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If

            If Not IsNothing(_PatientStrip) Then
                Me.Controls.Remove(_PatientStrip)
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If
            If (IsNothing(GloUC_TemplateTreeControl_PatientLetters) = False) Then
                GloUC_TemplateTreeControl_PatientLetters.FinalizeControlParameter("")
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub



    Private Sub FillReminderforUnscheduledCare()

        Dim oclspatient As New clsPatient
        Dim ds As DataSet = Nothing
        'If (IsNothing(objclsPatientLetters) = False) Then
        '    objclsPatientLetters.Dispose()
        '    objclsPatientLetters = Nothing
        'End If
        Dim objclsPatientLetters As clsPatientLetters
        objclsPatientLetters = New clsPatientLetters
        Dim dtCheckUnscheduledCare As DataTable = Nothing

        Try

            Dim _strPatitentCommunicationPrefers As String


            If Not IsNothing(dtReminderList) Then
                dtReminderList.Dispose()
                dtReminderList = Nothing
            End If


            _strPatitentCommunicationPrefers = oclspatient.CheckPatientCommunicationPrefers(m_PatientID)

            If _strPatitentCommunicationPrefers.Length > 0 Then

                lblPatientPrefers.Text = "Patient Prefers Communication via """ + _strPatitentCommunicationPrefers + """"
                lblPatientPrefers.Tag = _strPatitentCommunicationPrefers

            End If

            ds = objclsPatientLetters.FillReminderforUnscheduledCare(Convert.ToInt64(cmbTemplate.SelectedValue))

            CmbCommunicationPref.DataSource = Nothing
            CmbCommunicationPref.Items.Clear()

            If IsNothing(ds) = False Then
                dtReminderList = ds.Tables(0)
                dtCheckUnscheduledCare = ds.Tables(1)
            End If

            If IsNothing(dtReminderList) = False Then
                CmbCommunicationPref.DataSource = dtReminderList
                CmbCommunicationPref.ValueMember = dtReminderList.Columns(0).ColumnName
                CmbCommunicationPref.DisplayMember = dtReminderList.Columns(1).ColumnName
                If dtReminderList.Rows.Count > 0 Then
                    Dim row() As DataRow = dtReminderList.Select(" isSelected=1")
                    If (row.Length > 0) Then
                        CmbCommunicationPref.SelectedValue = row(0)("nCategoryID")
                    End If
                End If
            End If

            If IsDBNull(dtCheckUnscheduledCare) = False AndAlso dtCheckUnscheduledCare.Rows.Count > 0 Then
                ChkRemiderForUnSchedle.Checked = True
            Else
                ChkRemiderForUnSchedle.Checked = False
            End If


        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            If Not IsNothing(oclspatient) Then

                oclspatient = Nothing
            End If

            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If

            If Not IsNothing(dtCheckUnscheduledCare) Then
                dtCheckUnscheduledCare.Dispose()
                dtCheckUnscheduledCare = Nothing
            End If

            If Not IsNothing(objclsPatientLetters) Then
                objclsPatientLetters.Dispose()
                objclsPatientLetters = Nothing
            End If

        Finally

            If Not IsNothing(oclspatient) Then

                oclspatient = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If


            If Not IsNothing(dtCheckUnscheduledCare) Then
                dtCheckUnscheduledCare.Dispose()
                dtCheckUnscheduledCare = Nothing
            End If
            If Not IsNothing(objclsPatientLetters) Then
                '23-Apr-13 Aniket: Resolving Memory Leaks
                objclsPatientLetters.Dispose()
                objclsPatientLetters = Nothing
            End If
        End Try

    End Sub

    Private Sub ChkRemiderForUnSchedle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkRemiderForUnSchedle.CheckedChanged
        If ChkRemiderForUnSchedle.Checked = True Then
            CmbCommunicationPref.Visible = True
            If lblPatientPrefers.Tag IsNot Nothing Then
                If lblPatientPrefers.Tag.Trim() <> "" And lblPatientPrefers.Tag.Trim() <> CmbCommunicationPref.Text.Trim() Then
                    lblPatientPrefers.Visible = True
                Else
                    lblPatientPrefers.Visible = False
                End If
            End If
        Else
            CmbCommunicationPref.Visible = False
            lblPatientPrefers.Visible = False
        End If
    End Sub


    Private Sub CmbCommunicationPref_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCommunicationPref.SelectedIndexChanged
        If ChkRemiderForUnSchedle.Checked = True Then
            CmbCommunicationPref.Visible = True
            If lblPatientPrefers.Tag IsNot Nothing Then
                If lblPatientPrefers.Tag.Trim() <> "" And lblPatientPrefers.Tag.Trim() <> CmbCommunicationPref.Text.Trim() Then
                    lblPatientPrefers.Visible = True
                Else
                    lblPatientPrefers.Visible = False
                End If
            End If
        Else
            CmbCommunicationPref.Visible = False
            lblPatientPrefers.Visible = False
        End If
    End Sub
End Class
